using ExControls;

namespace ElenFontEditor.Entities;

public class FontsFile
{
    public string PathName { get; set; } = null!;

    public string Name
    {
        get;
        set
        {
            if (value == field)
                return;
            ArgumentNullException.ThrowIfNull(value);
            if (value.Length > 32)
                throw new ArgumentException("File name must be shorter than 33 characters.");
            field = value;
        }
    } = null!;

    public ExBindingList<TableFont> Fonts { get; }

    public FontsFile()
    {
        Fonts = new ExBindingList<TableFont>();
    }
}