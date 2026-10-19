namespace ElenFontEditor.Entities;

public class TableFont
{
    /// <summary>Najdlhsi nazov pisma - v subore ma 20 bajtov.</summary>
    public const int NameMaxLength = 20;

    /// <summary>
    /// Najvacsia sirka pisma v editore - tri matice po 8 bodoch. Subor sirku uklada v 1 bajte, realne pisma maju
    /// najviac 16 bodov.
    /// </summary>
    public const int MaxWidthLimit = 24;

    /// <summary>Najvacsia vyska pisma v sprave pisiem (realne pisma maju najviac 20 bodov).</summary>
    public const int HeightLimit = 20;

    public string Name
    {
        get;
        set
        {
            if (value == field)
                return;
            ArgumentNullException.ThrowIfNull(value);
            if (value.Length > NameMaxLength)
                throw new ArgumentException($"Font name must be less than {NameMaxLength + 1} characters.");
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
            if (value > MaxWidthLimit)
                throw new ArgumentException($"Font max width cannot be more than {MaxWidthLimit}.");
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

    private TableFont(TableFont original)
    {
        Name = original.Name;
        MaxWidth = original.MaxWidth;
        Height = original.Height;
        IsProportional = original.IsProportional;
        Symbols = (FontSymbol[]) original.Symbols.Clone();
    }

    private TableFont(TableFont original, string name, int maxWidth, int height, bool isProportional)
    {
        Name = name;
        MaxWidth = maxWidth;
        Height = height;
        IsProportional = isProportional;

        var shrinks = maxWidth < original.MaxWidth;
        Symbols = new FontSymbol[256];
        for (var i = 0; i < Symbols.Length; i++)
        {
            var source = original.Symbols[i];
            var symbol = new FontSymbol(i, source is null ? maxWidth : NewWidth(source.Width, original), this);
            if (source is not null)
            {
                for (var r = 0; r < Math.Min(symbol.Rows, source.Rows); r++)
                {
                    for (var c = 0; c < Math.Min(symbol.Columns, source.Columns); c++)
                    {
                        // body za novou sirkou sa pri zuzeni zmazu, inak sa prenesu vsetky (aj za sirkou pisma)
                        for (var b = 0; b < 8; b++)
                            symbol[r, c, b] = source.Data[r][c][b] && (!shrinks || c * 8 + b < maxWidth);
                    }
                }
            }

            Symbols[i] = symbol;
        }
    }

    /// <summary>
    /// Nove pismo s prazdnymi znakmi, kazdy so sirkou pisma.
    /// </summary>
    public static TableFont CreateEmpty(string name, int maxWidth, int height, bool isProportional)
    {
        var font = new TableFont { Name = name, MaxWidth = maxWidth, Height = height, IsProportional = isProportional };
        for (var i = 0; i < font.Symbols.Length; i++)
            font.Symbols[i] = new FontSymbol(i, maxWidth, font);
        return font;
    }

    /// <summary>
    /// Kopia pisma, ktora zdiela jeho znaky.
    /// </summary>
    public TableFont Clone() => new(this);

    /// <summary>
    /// Kopia pisma s inou sirkou a vyskou - znaky su nove objekty, body sa prenesu, kolko sa ich zmesti.
    /// </summary>
    public TableFont Clone(int newMaxWidth, int newHeight) => Clone(Name, newMaxWidth, newHeight, IsProportional);

    /// <summary>
    /// Kopia pisma s novymi vlastnostami - znaky su nove objekty, body sa prenesu, kolko sa ich zmesti. Sirky znakov
    /// ostanu (aj vacsie ako sirka pisma), pri zuzeni pisma sa orezu; pevna sirka neproporcionalneho pisma ide so
    /// sirkou pisma.
    /// </summary>
    public TableFont Clone(string name, int maxWidth, int height, bool isProportional) => new(this, name, maxWidth, height, isProportional);

    public override string ToString() => Name;

    /// <summary>Sirka znaku v kopii pisma (<see cref="MaxWidth" /> a <see cref="IsProportional" /> uz su nove).</summary>
    private int NewWidth(int width, TableFont original)
    {
        // znak sirky 0 v pisme nie je
        if (width == 0)
            return 0;
        // neproporcionalne pismo ma pri vsetkych znakoch sirku pisma
        if (!IsProportional && (original.IsProportional || width == original.MaxWidth))
            return MaxWidth;
        return MaxWidth < original.MaxWidth ? Math.Min(width, MaxWidth) : width;
    }
}
