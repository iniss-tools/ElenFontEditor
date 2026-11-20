namespace ElenFontEditor.Import;

/// <summary>
/// Znak pripraveny na import: body v mriezke pisma a sirka znaku.
/// </summary>
/// <param name="Code">kod znaku v pisme</param>
/// <param name="Width">sirka znaku v bodoch (o kolko sa posunie text)</param>
/// <param name="Dots">body [riadok, stlpec]</param>
public sealed record ImportGlyph(int Code, int Width, bool[,] Dots)
{
    /// <summary>Pocet riadkov bodov.</summary>
    public int Rows => Dots.GetLength(0);

    /// <summary>Pocet stlpcov bodov.</summary>
    public int Columns => Dots.GetLength(1);

    /// <summary>Znak ma aspon jeden rozsvieteny bod.</summary>
    public bool HasDots => Dots.Cast<bool>().Any(dot => dot);
}
