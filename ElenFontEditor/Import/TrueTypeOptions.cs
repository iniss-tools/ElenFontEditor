namespace ElenFontEditor.Import;

/// <summary>
/// Vykreslenie pisma Windows do bodov pisma ELEN.
/// </summary>
/// <param name="Style">rez (tucne, kurziva)</param>
/// <param name="Size">velkost pisma v bodoch tabule (vyska ciary em)</param>
/// <param name="Baseline">riadok bodov, na ktorom lezi uciarie (od 0 zhora)</param>
/// <param name="Smooth">vyhladene kreslenie a prah; inak bodove kreslenie s hintingom pisma</param>
/// <param name="Threshold">pri vyhladenom kresleni: bod svieti, ked ho znak pokryva aspon z tolkych percent</param>
public sealed record TrueTypeOptions(FontStyle Style, float Size, int Baseline, bool Smooth = true, int Threshold = 50)
{
    /// <summary>
    /// Velkost a uciarie, pri ktorych sa do <paramref name="rows" /> riadkov zmesti cele pismo vratane dlznov.
    /// </summary>
    public static (int Size, int Baseline) Defaults(FontFamily family, FontStyle style, int rows)
    {
        ArgumentNullException.ThrowIfNull(family);
        double em = family.GetEmHeight(style);
        double ascent = family.GetCellAscent(style);
        double descent = family.GetCellDescent(style);
        var size = Math.Max(4, (int)Math.Round(rows * em / Math.Max(1, ascent + descent)));
        var baseline = Math.Clamp((int)Math.Round(size * ascent / em), 1, rows);
        return (size, baseline);
    }
}
