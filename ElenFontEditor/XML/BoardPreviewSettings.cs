using System.Xml.Serialization;

namespace ElenFontEditor.XML;

/// <summary>
/// Nastavenia panela Nahlad na tabuli: ukazkovy text, farba, vysoke cislice a sirka tabule.
/// </summary>
public sealed class BoardPreviewSettings
{
    /// <summary>Ukazkovy text; null = predvoleny text v jazyku programu.</summary>
    [XmlAttribute("Text")]
    public string? Text { get; set; }

    /// <summary>Farba pisma ELEN (0 bez farby, 1 cervena, 2 zelena, 3 zlta).</summary>
    [XmlAttribute("Color")]
    public int Color { get; set; } = 3;

    /// <summary>Medzera, cislice a dvojbodka vysokymi cislicami pisma.</summary>
    [XmlAttribute("TallDigits")]
    public bool TallDigits { get; set; }

    /// <summary>Sirka tabule v bodoch; 0 = podla textu.</summary>
    [XmlAttribute("Width")]
    public int Width { get; set; }

    /// <summary>Kopia.</summary>
    public BoardPreviewSettings Clone() => new() { Text = Text, Color = Color, TallDigits = TallDigits, Width = Width };
}
