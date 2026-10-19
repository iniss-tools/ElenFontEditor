using System.ComponentModel;
using System.Xml.Serialization;
using ElenFontEditor.Entities;
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
    public ShortcutMap Shortcuts { get; set; } = new();

    /// <summary>
    ///     Nacitat data v striktnom rezime (vacsia kontrola dat pri otvarani suboru).
    /// </summary>
    [XmlElement("StrictMode"), DefaultValue(false)]
    public bool StrictMode { get; set; }

    /// <summary>
    ///     Kodovanie, podla ktoreho stlpec Znak a popisy krokov spat zobrazia kod znaku.
    /// </summary>
    [XmlElement("CharEncoding"), DefaultValue(SymbolCharset.Win1250)]
    public SymbolCharset CharEncoding { get; set; }

    /// <inheritdoc />
    public override string LinkAppSettingsGuide => "http://iniss.6f.sk/docs/elenfonteditor/nastavenia-programu/";

    /// <summary>
    /// Kopia nastaveni (okno Nastavenia programu) - skratky a stlpce sa nesmu zdielat s povodnymi.
    /// </summary>
    protected ElenFontEditorConfig(ElenFontEditorConfig original) : base(original)
    {
        DesktopCols = original.DesktopCols with { };
        Shortcuts = original.Shortcuts.Clone();
        StrictMode = original.StrictMode;
        CharEncoding = original.CharEncoding;
    }

    /// <summary>
    /// Predvolene nastavenia.
    /// </summary>
    public ElenFontEditorConfig()
    {
    }
}