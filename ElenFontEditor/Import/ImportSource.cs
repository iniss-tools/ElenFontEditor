namespace ElenFontEditor.Import;

/// <summary>
/// Odkial sa znaky importuju.
/// </summary>
public enum ImportSource
{
    /// <summary>Pismo Windows (nainstalovane alebo subor .ttf/.otf).</summary>
    Font,

    /// <summary>Obrazok s jednym znakom alebo mriezkou znakov.</summary>
    Image
}
