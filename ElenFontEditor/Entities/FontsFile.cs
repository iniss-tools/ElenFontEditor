using ExControls;

namespace ElenFontEditor.Entities;

public class FontsFile
{
    /// <summary>Najdlhsi nazov suboru - v hlavicke suboru ma 32 bajtov.</summary>
    public const int NameMaxLength = 32;

    public string PathName { get; set; } = null!;

    public string Name
    {
        get;
        set
        {
            if (value == field)
                return;
            ArgumentNullException.ThrowIfNull(value);
            if (value.Length > NameMaxLength)
                throw new ArgumentException($"File name must be shorter than {NameMaxLength + 1} characters.");
            field = value;
        }
    } = null!;

    public ExBindingList<TableFont> Fonts { get; }

    public FontsFile()
    {
        Fonts = new ExBindingList<TableFont>();
    }
}