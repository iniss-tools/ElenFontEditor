using System.ComponentModel;
using System.Xml.Serialization;
using ElenFontEditor.Entities;
using ToolsCore.XML;

namespace ElenFontEditor.XML;

[XmlRoot("CONFIG")]
public record ElenFontEditorConfig : ConfigBase
{


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
    public override string LinkAppSettingsGuide => "https://iniss-tools.fvat.sk/docs/elenfonteditor/nastavenia-programu/";

    /// <summary>
    /// Kopia nastaveni (okno Nastavenia programu) - skratky a stlpce sa nesmu zdielat s povodnymi.
    /// </summary>
    protected ElenFontEditorConfig(ElenFontEditorConfig original) : base(original)
    {
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