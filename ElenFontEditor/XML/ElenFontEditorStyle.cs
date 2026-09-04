using ToolsCore.XML;
// ReSharper disable UnusedMember.Global

namespace ElenFontEditor.XML;

/// <summary>
///     Trieda definujuca farby a pisma pre viacere prvky programu
/// </summary>
public record ElenFontEditorStyle : Style
{
    /// <inheritdoc />
    public ElenFontEditorStyle()
    {
    }

    /// <inheritdoc />
    private ElenFontEditorStyle(Style original) : base(original)
    {
    }

    public new static ElenFontEditorStyle DefaultLightStyle => new(Style.DefaultLightStyle);

    public new static ElenFontEditorStyle DefaultDarkStyle => new(Style.DefaultDarkStyle);

    /// <inheritdoc />
    public override string ToString() => Name;
}