using System.Globalization;
using System.Text.RegularExpressions;
using ToolsCore.Iniss.Elen;
using ToolsCore.Iniss.Tools;

namespace ElenFontEditor.DocScreenshots;

/// <summary>
/// Ukážkové súbory písiem ELEN v kódovaní Windows-1250 pre snímky do dokumentácie. Usporiadanie je ako v bežných
/// súboroch tabúľ (neproporčné, proporčné, tučné a písmo len s číslicami), znaky sú z bodových písiem misc-fixed
/// (public domain), ktoré simulátor tabúľ kreslí bez písma ELEN – skutočné súbory písiem do repozitára nepatria.
/// </summary>
/// <remarks>
/// Proporčné písmo vznikne orezaním prázdnych stĺpcov znaku, tučné rozšírením každého bodu o jeden doprava.
/// </remarks>
internal static partial class DemoFonts
{
    public const string Marker = ".docshots";

    /// <summary>Hlavný ukážkový súbor – písma s výškou 16 bodov.</summary>
    public const string Main = "DEMO_16_1250";

    /// <summary>Druhý súbor s výškou 10 bodov – v zozname nedávnych súborov.</summary>
    public const string Small = "DEMO_10_1250";

    /// <summary>Písma súboru <see cref="Main" />: neproporčné, proporčné, tučné, číslice.</summary>
    public static readonly string[] MainFonts = ["NEPROP_16", "PROP_16", "PROP_BOLD_16", "CISLICE_16"];

    /// <summary>Znaky písma len s číslicami (ako na tabuliach s časom odchodu).</summary>
    private const string DigitChars = " 0123456789:.-+";

    /// <summary>
    /// Zostaví súbory písiem do <paramref name="root" /> (existujúci obsah vytvorený harnessom zmaže) a vráti ich cesty,
    /// hlavný súbor ako prvý.
    /// </summary>
    public static string[] Build(string root, List<string> log)
    {
        var marker = Path.Combine(root, Marker);
        if (Directory.Exists(root))
        {
            if (!File.Exists(marker))
                throw new InvalidOperationException(
                    $"Priečinok {root} existuje a nevytvoril ho harness – nezmaže sa. Zadaj iný cez --work=….");
            Directory.Delete(root, true);
        }

        Directory.CreateDirectory(root);
        File.WriteAllText(marker, "Ukážkové súbory písiem pre ElenFontEditor.DocScreenshots – pri ďalšom spustení sa zmažú.");

        var data = FindPixelFonts();
        var fonts = PixelFont.Load(data);
        log.Add($"znaky písiem: {data}");

        var font7X14 = fonts["7x14"];
        var font9X15 = fonts["9x15"];
        var font6X10 = fonts["6x10"];

        var main = Write(root, Main,
        [
            Make(MainFonts[0], font7X14, height: 16, top: 1, proportional: false, bold: false),
            Make(MainFonts[1], font7X14, height: 16, top: 1, proportional: true, bold: false),
            Make(MainFonts[2], font7X14, height: 16, top: 1, proportional: true, bold: true),
            Make(MainFonts[3], font9X15, height: 16, top: 0, proportional: false, bold: true, DigitChars.Contains),
        ]);
        var small = Write(root, Small,
        [
            Make("NEPROP_10", font6X10, height: 10, top: 0, proportional: false, bold: false),
            Make("PROP_10", font6X10, height: 10, top: 0, proportional: true, bold: false),
            Make("PROP_BOLD_10", font6X10, height: 10, top: 0, proportional: true, bold: true),
        ]);

        // ukážkové súbory musia prejsť aj striktnou kontrolou programu
        foreach (var path in new[] { main, small })
        {
            var file = ElenFontFile.Load(path, strict: true);
            log.Add($"súbor písiem: {path} – " + string.Join(", ", file.Fonts.Select(f =>
                $"{f.Name} {f.MaxWidth}×{f.Height}{(f.IsProportional ? " proporčné" : "")}")));
        }

        return [main, small];
    }

    private static string Write(string root, string name, List<ElenFont> fonts)
    {
        var path = Path.Combine(root, name + ".bin");
        new ElenFontFile(name, fonts).Save(path);
        return path;
    }

    /// <summary>
    /// Písmo ELEN z bodového písma: 256 znakov podľa Windows-1250, znaky bez predlohy prázdne so šírkou 0.
    /// </summary>
    /// <param name="height">výška písma v bodoch</param>
    /// <param name="top">o koľko riadkov nižšie začína znak predlohy</param>
    /// <param name="only">len tieto znaky (null = všetky)</param>
    private static ElenFont Make(string name, PixelFont source, int height, int top, bool proportional, bool bold,
        Func<char, bool>? only = null)
    {
        var shapes = new bool[ElenFont.GlyphCount][,];
        var widths = new int[ElenFont.GlyphCount];
        for (var code = 0; code < ElenFont.GlyphCount; code++)
        {
            shapes[code] = new bool[height, source.Width + 1];
            var ch = CharOf(code);
            if (ch is null || (only is not null && !only(ch.Value)) || source.Glyph(ch.Value) is not { } rows)
                continue;

            // predloha v bunke source.Width × source.Height, prvý bit riadku = ľavý stĺpec
            var shape = shapes[code];
            for (var r = 0; r < source.Height && r + top < height; r++)
                for (var c = 0; c < source.Width; c++)
                    if (source.IsSet(rows, r, c))
                    {
                        shape[r + top, c] = true;
                        if (bold)
                            shape[r + top, c + 1] = true;
                    }

            widths[code] = proportional ? Trim(shape, source.Width) : source.Width + (bold ? 1 : 0);
        }

        var maxWidth = widths.Max();
        var bytesPerRow = ElenFont.BytesPerRowFor(maxWidth);
        var glyphs = new ElenGlyph[ElenFont.GlyphCount];
        for (var code = 0; code < ElenFont.GlyphCount; code++)
        {
            var data = new byte[height * bytesPerRow];
            var shape = shapes[code];
            for (var r = 0; r < height; r++)
                for (var c = 0; c < Math.Min(maxWidth, shape.GetLength(1)); c++)
                    if (shape[r, c])
                        data[r * bytesPerRow + (c >> 3)] |= (byte)(0x80 >> (c & 7));

            glyphs[code] = new ElenGlyph(widths[code], height, bytesPerRow, data);
        }

        return new ElenFont(name, maxWidth, height, proportional, glyphs);
    }

    /// <summary>
    /// Posunie znak k ľavému okraju a vráti jeho šírku s medzerou jedného bodu za znakom; prázdny znak (medzera)
    /// má polovicu šírky bunky.
    /// </summary>
    private static int Trim(bool[,] shape, int cellWidth)
    {
        int rows = shape.GetLength(0), columns = shape.GetLength(1);
        int first = columns, last = -1;
        for (var c = 0; c < columns; c++)
            for (var r = 0; r < rows; r++)
                if (shape[r, c])
                {
                    first = Math.Min(first, c);
                    last = Math.Max(last, c);
                }

        if (last < 0)
            return (cellWidth + 1) / 2;

        for (var c = 0; c < columns; c++)
            for (var r = 0; r < rows; r++)
                shape[r, c] = c + first < columns && shape[r, c + first];

        return last - first + 2;
    }

    /// <summary>Znak kódu vo Windows-1250; riadiace kódy a kódy bez znaku = null, nedeliteľná medzera = medzera.</summary>
    private static char? CharOf(int code)
    {
        if (code < 0x20 || code == 0x7F)
            return null;
        if (code == 0xA0)
            return ' ';

        var text = Encodings.Win1250.GetString([(byte)code]);
        return text.Length == 1 && !char.IsControl(text[0]) && text[0] != '\uFFFD' ? text[0] : null;
    }

    /// <summary>
    /// Súbor so znakmi písiem misc-fixed v repozitári simulátora tabúľ (hľadá sa v nadradených priečinkoch).
    /// </summary>
    private static string FindPixelFonts()
    {
        var relative = Path.Combine("TableSimulator", "web", "src", "render", "pixel-fonts.data.ts");
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            var path = Path.Combine(dir.FullName, relative);
            if (File.Exists(path))
                return path;
        }

        throw new FileNotFoundException($"Nenašiel sa súbor {relative} so znakmi ukážkových písiem.");
    }

    /// <summary>
    /// Bodové písmo z <c>pixel-fonts.data.ts</c>: znaky podľa kódu Unicode, každý riadok bunky v hex.
    /// </summary>
    private sealed partial class PixelFont(int width, int height, Dictionary<int, byte[]> glyphs)
    {
        public int Width { get; } = width;
        public int Height { get; } = height;

        private int BytesPerRow => (Width + 7) / 8;

        public byte[]? Glyph(char ch) => glyphs.GetValueOrDefault(ch);

        public bool IsSet(byte[] rows, int row, int column) =>
            (rows[row * BytesPerRow + (column >> 3)] & (0x80 >> (column & 7))) != 0;

        public static Dictionary<string, PixelFont> Load(string path)
        {
            var text = File.ReadAllText(path);
            var fonts = new Dictionary<string, PixelFont>(StringComparer.Ordinal);
            foreach (Match font in FontPattern().Matches(text))
            {
                var glyphs = GlyphPattern().Matches(font.Groups["glyphs"].Value)
                    .ToDictionary(g => int.Parse(g.Groups["code"].Value, CultureInfo.InvariantCulture),
                        g => Convert.FromHexString(g.Groups["rows"].Value));
                fonts[font.Groups["name"].Value] = new PixelFont(
                    int.Parse(font.Groups["width"].Value, CultureInfo.InvariantCulture),
                    int.Parse(font.Groups["height"].Value, CultureInfo.InvariantCulture),
                    glyphs);
            }

            foreach (var name in new[] { "6x10", "7x14", "9x15" })
                if (!fonts.ContainsKey(name))
                    throw new FormatException($"V súbore {path} chýba písmo {name}.");
            return fonts;
        }

        [GeneratedRegex("""name:\s*"(?<name>[^"]+)",\s*width:\s*(?<width>\d+),\s*height:\s*(?<height>\d+),\s*ascent:\s*\d+,\s*glyphs:\s*\{(?<glyphs>[^}]*)\}""")]
        private static partial Regex FontPattern();

        [GeneratedRegex(@"(?<code>\d+):""(?<rows>[0-9a-fA-F]+)""")]
        private static partial Regex GlyphPattern();
    }
}
