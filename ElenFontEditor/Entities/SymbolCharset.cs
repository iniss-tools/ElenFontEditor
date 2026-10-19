using System.Xml.Serialization;
using ToolsCore.Iniss.Tools;

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

/// <summary>
/// Text znaku s danym kodom v kodovani <see cref="SymbolCharset" />.
/// </summary>
public static class SymbolChars
{
    /// <summary>
    /// Znak s kodom <paramref name="code" />; riadiace kody (pod 0x20, 0x7F) a pozicie, ktore kodova stranka nema,
    /// su prazdny text - neviditelny znak by v zozname vyzeral ako medzera.
    /// </summary>
    public static string Text(int code, SymbolCharset charset)
    {
        if (code is < 0 or > byte.MaxValue)
            return "";

        var b = (byte)code;
        var c = charset == SymbolCharset.Kamenicky ? KamenickyEncoding.Decode(b) : Encodings.Win1250.GetString([b]) is [var single] ? single : '\0';
        // nedefinovane pozicie Windows-1250 sa dekoduju na riadiace znaky C1 alebo na nahradny znak
        if (char.IsControl(c) || c == '�' || (c == '?' && b != '?'))
            return "";
        return c.ToString();
    }

    /// <summary>
    /// Tlacitelny znak s kodom <paramref name="code" /> (nie medzera ani riadiaci kod), inak null.
    /// </summary>
    public static char? Printable(int code, SymbolCharset charset) =>
        Text(code, charset) is [var c] && !char.IsWhiteSpace(c) ? c : null;
}
