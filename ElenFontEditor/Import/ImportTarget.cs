namespace ElenFontEditor.Import;

/// <summary>
/// Kam sa znaky importuju: nove pismo na konci suboru alebo existujuce pismo.
/// </summary>
/// <param name="FontIndex">pismo suboru; -1 = nove pismo</param>
/// <param name="Name">nazov noveho pisma</param>
/// <param name="MaxWidth">sirka noveho pisma</param>
/// <param name="Height">vyska noveho pisma</param>
/// <param name="Proportional">nove pismo je proporcne</param>
/// <param name="OnlyEmpty">v existujucom pisme nahradit len znaky bez bodov</param>
public sealed record ImportTarget(int FontIndex, string Name, int MaxWidth, int Height, bool Proportional, bool OnlyEmpty)
{
    /// <summary>Import do noveho pisma.</summary>
    public bool IsNewFont => FontIndex < 0;
}
