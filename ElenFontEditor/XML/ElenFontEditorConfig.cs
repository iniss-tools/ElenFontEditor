using System.ComponentModel;
using System.Xml.Serialization;
using ToolsCore.XML;

namespace ElenFontEditor.XML;

[XmlRoot("CONFIG")]
public record ElenFontEditorConfig : ConfigBase
{
    /// <summary>
    ///     Stlpce zobrazujuce sa v tabulke na pracovnej ploche programu.
    /// </summary>
    [XmlElement("DesktopCols")] 
    public DesktopColumns DesktopCols { get; set; } = new();

    /// <summary>
    ///     Klávesové skratky pre akcie na pracovnej ploche programu.
    /// </summary>
    [XmlElement("Shortcuts")] 
    public AppShortcuts Shortcuts { get; set; } = new();

    /// <summary>
    ///     Nacitat data v striktnom rezime (vacsia kontrola dat pri otvarani suboru).
    /// </summary>
    [XmlElement("StrictMode"), DefaultValue(false)]
    public bool StrictMode { get; set; }

    /// <inheritdoc />
    public override string LinkAppSettingsGuide => "";
}