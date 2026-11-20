namespace ElenFontEditor.Import;

/// <summary>
/// Ako sa body zdroja (pismo Windows, obrazok) vlozia do znaku pisma ELEN.
/// </summary>
/// <param name="Height">vyska pisma v bodoch</param>
/// <param name="MaxWidth">sirka pisma v bodoch (najviac 16)</param>
/// <param name="Proportional">proporcne pismo - sirka znaku podla jeho bodov</param>
/// <param name="Spacing">medzera za znakom v bodoch</param>
/// <param name="Left">odsadenie bodov zlava</param>
/// <param name="Center">neproporcne pismo: znak na stred sirky (inak vlavo)</param>
public sealed record GlyphLayout(int Height, int MaxWidth, bool Proportional, int Spacing = 1, int Left = 0, bool Center = false)
{
    /// <summary>
    /// Znak z bodov zdroja: prazdne stlpce zlava sa orezu, proporcne pismo dostane sirku podla bodov a medzery,
    /// neproporcne sirku pisma. Co sa do sirky pisma nezmesti, sa odreze.
    /// </summary>
    /// <param name="code">kod znaku</param>
    /// <param name="ink">body zdroja [riadok, stlpec]; riadky nad vyskou pisma sa odrezu</param>
    /// <param name="advance">sirka zdroja v bodoch pre znak bez bodov (medzera)</param>
    public ImportGlyph Fit(int code, bool[,] ink, int advance)
    {
        ArgumentNullException.ThrowIfNull(ink);
        var rows = Math.Min(Height, ink.GetLength(0));
        int first = int.MaxValue, last = -1;
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < ink.GetLength(1); c++)
            {
                if (!ink[r, c])
                    continue;
                first = Math.Min(first, c);
                last = Math.Max(last, c);
            }
        }

        var dots = new bool[Height, MaxWidth];
        if (last < 0)
            return new ImportGlyph(code, Proportional ? Math.Clamp(advance, 0, MaxWidth) : MaxWidth, dots);

        var inkWidth = last - first + 1;
        var offset = Left;
        if (!Proportional && Center)
            offset += Math.Max(0, (MaxWidth - Spacing - Left - inkWidth) / 2);

        for (var r = 0; r < rows; r++)
        {
            for (var c = first; c <= last; c++)
            {
                var x = offset + c - first;
                if (ink[r, c] && x < MaxWidth)
                    dots[r, x] = true;
            }
        }

        var width = Proportional ? Math.Min(Left + inkWidth + Spacing, MaxWidth) : MaxWidth;
        return new ImportGlyph(code, width, dots);
    }
}
