using ElenFontEditor.XML;
using ToolsCore;
using ToolsCore.XML;

namespace ElenFontEditor;

public static class GlobData
{
    /// <summary>
    /// Nastavenia programu (konfiguracia a styly), vytvorene pri starte.
    /// </summary>
    public static AppSession<ElenFontEditorConfig, ElenFontEditorStyle> Session { get; set; } = null!;

    public static ElenFontEditorConfig Config { get => Session.Config; set => Session.Config = value; }
    public static Styles<ElenFontEditorStyle> Styles { get => Session.Styles; set => Session.Styles = value; }
    public static ElenFontEditorStyle UsingStyle { get => Session.UsingStyle; set => Session.UsingStyle = value; }
}