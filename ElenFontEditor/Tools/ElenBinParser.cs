using System.Globalization;
using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Tools;

/// <summary>
/// Prevod suboru pisiem (<see cref="ElenFontFile" /> z ToolsCore.Iniss) na upravovatelne entity editora a spat.
/// </summary>
internal static class ElenBinParser
{
    public static FontsFile LoadFile(string pathname) => LoadFile(pathname, GlobData.Config.StrictMode);

    /// <param name="pathname">cesta k suboru</param>
    /// <param name="strict">prisna kontrola suboru ako INISS2</param>
    internal static FontsFile LoadFile(string pathname, bool strict)
    {
        var source = ElenFontFile.Load(pathname, strict);
        var file = new FontsFile { Name = source.Name, PathName = pathname };

        foreach (var sourceFont in source.Fonts)
        {
            // volne citanie prijme aj sirsie pismo - editor ma bodovu mriezku najviac dvoch matic
            if (sourceFont.MaxWidth > TableFont.MaxWidthLimit)
                throw new FormatException(string.Format(CultureInfo.CurrentCulture, Resources.FMain_FontTooWide, sourceFont.Name, sourceFont.MaxWidth,
                    TableFont.MaxWidthLimit));

            var font = new TableFont
            {
                Name = sourceFont.Name,
                MaxWidth = sourceFont.MaxWidth,
                Height = sourceFont.Height,
                IsProportional = sourceFont.IsProportional
            };

            for (var j = 0; j < ElenFont.GlyphCount; j++)
            {
                var glyph = sourceFont.Glyphs[j];
                var symbol = new FontSymbol(j, glyph.Width, font);
                for (var r = 0; r < symbol.Rows; r++)
                    for (var c = 0; c < symbol.Columns; c++)
                        symbol[r, c] = glyph[r, c];

                font.Symbols[j] = symbol;
            }

            file.Fonts.Add(font);
        }

        return file;
    }

    public static void SaveFile(FontsFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var fonts = file.Fonts.Select(ToElenFont).ToList();

        new ElenFontFile(file.Name, fonts).Save(file.PathName);
        MatchFileNameCase(file);
    }

    /// <summary>
    /// Prepisany subor si na disku necha povodny nazov aj pri inej velkosti pismen (TEST.bin ulozeny ako test.bin) a
    /// nazov v hlavicke by sa s menom suboru nezhodoval. Ak cesta zodpoveda nazvu v hlavicke, subor sa premenuje na nu.
    /// </summary>
    private static void MatchFileNameCase(FontsFile file)
    {
        var name = Path.GetFileName(file.PathName);
        if (Path.GetFileNameWithoutExtension(name) != file.Name || Path.GetDirectoryName(file.PathName) is not { Length: > 0 } dir)
            return;

        var actual = Directory.EnumerateFiles(dir, name)
            .Select(Path.GetFileName)
            .FirstOrDefault(found => string.Equals(found, name, StringComparison.OrdinalIgnoreCase));
        if (actual is not null && actual != name)
            File.Move(Path.Combine(dir, actual), file.PathName);
    }

    /// <summary>
    /// Pismo editora ako pismo suboru - pre zapis aj nahlad na tabuli (s neulozenymi zmenami).
    /// </summary>
    internal static ElenFont ToElenFont(TableFont font) =>
        new(font.Name, font.MaxWidth, font.Height, font.IsProportional,
            font.Symbols.Select(symbol => new ElenGlyph(symbol.Width, symbol.Rows, symbol.Columns, GlyphData(symbol))).ToArray());

    private static byte[] GlyphData(FontSymbol symbol)
    {
        var data = new byte[symbol.Rows * symbol.Columns];
        for (var r = 0; r < symbol.Rows; r++)
            for (var c = 0; c < symbol.Columns; c++)
                data[r * symbol.Columns + c] = symbol[r, c];
        return data;
    }
}
