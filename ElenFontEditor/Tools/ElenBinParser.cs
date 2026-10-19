using ElenFontEditor.Entities;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Tools;

/// <summary>
/// Prevod suboru pisiem (<see cref="ElenFontFile" /> z ToolsCore.Iniss) na upravovatelne entity editora a spat.
/// </summary>
internal static class ElenBinParser
{
    public static FontsFile LoadFile(string pathname)
    {
        var source = ElenFontFile.Load(pathname, GlobData.Config.StrictMode);
        var file = new FontsFile { Name = source.Name, PathName = pathname };

        foreach (var sourceFont in source.Fonts)
        {
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

        var fonts = file.Fonts
            .Select(font => new ElenFont(font.Name, font.MaxWidth, font.Height, font.IsProportional,
                font.Symbols.Select(symbol => new ElenGlyph(symbol.Width, symbol.Rows, symbol.Columns, GlyphData(symbol))).ToArray()))
            .ToList();

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

    private static byte[] GlyphData(FontSymbol symbol)
    {
        var data = new byte[symbol.Rows * symbol.Columns];
        for (var r = 0; r < symbol.Rows; r++)
            for (var c = 0; c < symbol.Columns; c++)
                data[r * symbol.Columns + c] = symbol[r, c];
        return data;
    }
}
