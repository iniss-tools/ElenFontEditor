using System.Xml.Serialization;

namespace ElenFontEditor.Entities;

/// <summary>
/// Kodovanie, podla ktoreho stlpec Znak zobrazi kod znaku. Subor pisiem kodovanie neuklada - tabula ELEN kresli znak
/// podla kodu bajtu bez prevodu, kodova stranka zavisi od varianty ovladaca v INISSe.
/// </summary>
public enum SymbolCharset
{
    /// <summary>
    /// Windows-1250 (varianty ELEN10, ELEN16, ELEKON).
    /// </summary>
    [XmlEnum(Name = "Windows-1250")]
    Win1250,

    /// <summary>
    /// Kod Kamenickych (varianty ELEN, ELENOLD, ELEN16Kam).
    /// </summary>
    [XmlEnum(Name = "Kamenicky")]
    Kamenicky
}
