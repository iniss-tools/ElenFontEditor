using ToolsCore.XML;

namespace ElenFontEditor.XML;

/// <summary>
/// Farebna schema ElenFontEditora (len ovladacie prvky).
/// </summary>
public class ElenFontEditorStyle : Style
{
    /// <inheritdoc />
    protected override Style CreateEmpty() => new ElenFontEditorStyle();
}
