using System;
using ExControls;

namespace ElenFontEditor.Entities;

public class FontsFile
{
    private string name;

    public string PathName { get; set; }
    public string Name
    {
        get => name;
        set
        {
            if (value == name)
                return;
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.Length > 32)
                throw new ArgumentException("File name must be shorter than 33 characters.");
            name = value;
        }
    }

    public ExBindingList<TableFont> Fonts { get; }

    public FontsFile()
    {
        Fonts = new ExBindingList<TableFont>();
    }
}