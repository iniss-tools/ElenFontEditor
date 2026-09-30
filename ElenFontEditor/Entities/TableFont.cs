namespace ElenFontEditor.Entities;

public class TableFont
{
    public string Name
    {
        get;
        set
        {
            if (value == field)
                return;
            ArgumentNullException.ThrowIfNull(value);
            if (value.Length > 20)
                throw new ArgumentException("Font name must be less than 21 characters.");
            field = value;
        }
    } = null!;

    public int MaxWidth
    {
        get;
        set
        {
            if (value == field)
                return;
            if (value < 0)
                throw new ArgumentException("Font max width cannot be less than 0.");
            if (value > 20)
                throw new ArgumentException("Font max width cannot be more than 20.");
            field = value;
        }
    }

    public int MatriciesCount => ((MaxWidth + 7) & -8) / 8;
    public int Height { get; set; }
    public bool IsProportional { get; set; }
    public FontSymbol[] Symbols { get; }

    public TableFont()
    {
        Symbols = new FontSymbol[256];
    }

    private TableFont(TableFont original, int newMaxWidth = -1, int newHeight = -1)
    {
        Name = original.Name;
        MaxWidth = newMaxWidth == -1 ? original.MaxWidth : newMaxWidth;
        Height = newHeight == -1 ? original.Height : newHeight;
        IsProportional = original.IsProportional;

        if (newMaxWidth == -1 && newHeight == -1)
        {
            Symbols = (FontSymbol[]) original.Symbols.Clone();
        }
        else
        {
            Symbols = new FontSymbol[256];
            for (var i = 0; i < Symbols.Length; i++)
            {
                var symbol = new FontSymbol(i, newMaxWidth, this);
                for (var r = 0; r < symbol.Rows; r++)
                {
                    for (var c = 0; c < symbol.Columns; c++)
                    {
                        if (r >= original.Symbols[i].Rows || c >= original.Symbols[i].Columns)
                        {
                            symbol[r, c] = 0;
                            continue;
                        }

                        for (var b = 0; b < 8; b++) 
                            symbol[r, c, b] = original.Symbols[i].Data[r][c][b];
                    }
                }

                Symbols[i] = symbol;
            }
        }
    }

    public TableFont Clone() => new(this);

    public TableFont Clone(int newMaxWidth, int newHeight) => new(this, newMaxWidth, newHeight);

    public override string ToString() => Name;
}