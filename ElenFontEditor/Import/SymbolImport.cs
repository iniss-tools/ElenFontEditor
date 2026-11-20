using ElenFontEditor.Entities;

namespace ElenFontEditor.Import;

/// <summary>
/// Zlozi pismo s importovanymi znakmi. Povodne pismo sa nemeni - vysledok je nova kopia, ktoru okno vlozi do suboru
/// (rovnako ako sprava pisiem) a krok spat ju zachyti ako jednu upravu.
/// </summary>
internal static class SymbolImport
{
    /// <summary>
    /// Pismo s importovanymi znakmi.
    /// </summary>
    /// <param name="file">subor pisiem</param>
    /// <param name="target">cielove pismo</param>
    /// <param name="glyphs">importovane znaky</param>
    /// <param name="imported">kody znakov, ktore sa nahradili</param>
    public static TableFont Build(FontsFile file, ImportTarget target, IEnumerable<ImportGlyph> glyphs, out List<int> imported)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(glyphs);

        TableFont font;
        if (target.IsNewFont)
        {
            font = TableFont.CreateEmpty(target.Name, target.MaxWidth, target.Height, target.Proportional);
            // proporcne pismo: znak, ktory sa neimportuje, v pisme nie je (sirka 0); neproporcne ma vsetky sirku pisma
            if (target.Proportional)
            {
                foreach (var symbol in font.Symbols)
                    symbol.Width = 0;
            }
        }
        else
        {
            var source = file.Fonts[target.FontIndex];
            font = source.Clone(source.Name, source.MaxWidth, source.Height, source.IsProportional);
        }

        imported = [];
        foreach (var glyph in glyphs)
        {
            if (glyph.Code is < 0 or >= 256)
                continue;
            var symbol = font.Symbols[glyph.Code];
            if (!target.IsNewFont && target.OnlyEmpty && HasDots(symbol))
                continue;

            Apply(symbol, glyph, font.IsProportional ? glyph.Width : font.MaxWidth);
            imported.Add(glyph.Code);
        }

        return font;
    }

    /// <summary>Ci ma znak aspon jeden rozsvieteny bod.</summary>
    public static bool HasDots(FontSymbol symbol) => symbol.Data.Any(row => row.Any(column => column.Any(dot => dot)));

    private static void Apply(FontSymbol symbol, ImportGlyph glyph, int width)
    {
        symbol.Width = width;
        for (var r = 0; r < symbol.Rows; r++)
        {
            for (var c = 0; c < symbol.Columns * 8; c++)
                symbol[r, c / 8, c % 8] = r < glyph.Rows && c < glyph.Columns && glyph.Dots[r, c];
        }
    }
}
