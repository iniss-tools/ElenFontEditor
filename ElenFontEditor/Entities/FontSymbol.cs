using System.Globalization;
using ElenFontEditor.Tools;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ElenFontEditor.Entities;

public class FontSymbol
{
    /// <summary>
    /// Najvacsia sirka znaku - subor ju uklada v 1 bajte. Moze byt vacsia ako sirka pisma (riadiace kody v suboroch
    /// vyrobcu maju sirku 24).
    /// </summary>
    public const int WidthLimit = byte.MaxValue;

    public TableFont Font { get; }
    public int Index { get; }
    public string Char { get; }
    public int Width { get; set; }
    public bool[][][] Data { get; }
    public int Rows { get; }
    public int Columns { get; }
    public Bitmap Image => CreateBitmap();

    public bool this[int row, int column, int offset]
    {
        get
        {
            CheckBounds(row, column);
            if (offset is < 0 or > 7)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Offset of byte is out of bounds (0 <= X <= 7).");
            return Data[row][column][offset];
        }
        set
        {
            CheckBounds(row, column);
            if (offset is < 0 or > 7)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Offset of byte is out of bounds (0 <= X <= 7).");
            Data[row][column][offset] = value;
        }
    }

    public byte this[int row, int column]
    {
        get => GetRow(row, column);
        set => SetRow(row,column, value);
    }

    public FontSymbol(int index, int width, TableFont font) : this(index, width, font.Height, font.MatriciesCount, font)
    {
    }

    /// <summary>
    /// Znak s danym rozmerom matice (obnova kroku spat).
    /// </summary>
    internal FontSymbol(int index, int width, int rows, int columns, TableFont font)
    {
        Width = width;
        Rows = rows;
        Columns = columns;
        Font = font;
        Index = index;
        Char = ((char) index).ToString(CultureInfo.CurrentCulture);
        Data = new bool[Rows][][];
        for (var r = 0; r < Rows; r++)
        {
            Data[r] = new bool[Columns][];
            for (var c = 0; c < Columns; c++) 
                Data[r][c] = new bool[8];
        }
    }

    private FontSymbol(FontSymbol original)
    {
        Width = original.Width;
        Rows = original.Rows;
        Columns = original.Columns;
        Font = original.Font;
        Index = original.Index;
        Char = original.Char;
        Data = (bool[][][]) original.Data.Clone();
    }

    private Bitmap CreateBitmap()
    {
        var image = new Bitmap(Columns * 8 * 2, Rows * 2);
        using var g = Graphics.FromImage(image);
        g.Clear(Color.Black);
        using var brush = new SolidBrush(Color.Yellow);
        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Columns; c++)
            {
                for (var b = 0; b < 8; b++)
                {
                    if (Data[r][c][b])
                    {
                        var rectangle = new Rectangle((c * 8 + b) * 2, r * 2, 2, 2);
                        g.FillRectangle(brush, rectangle);
                    }
                }
            }
        }
        return image;
    }

    private void SetRow(int row, int col, byte value)
    {
        CheckBounds(row, col);
        Data[row][col] = Utils.ConvertByteToBoolArray(value);
    }

    private byte GetRow(int row, int col)
    {
        CheckBounds(row, col);
        return Utils.ConvertBoolArrayToByte(Data[row][col]);
    }

    private void CheckBounds(int row, int col)
    {
        if (row < 0 || row >= Rows)
            throw new ArgumentOutOfRangeException($"Index of row must be in the range 0 <= X < {Rows}.");

        if (col < 0 || col >= Columns)
            throw new ArgumentOutOfRangeException($"Index of column must be in the range 0 <= X < {Columns}.");
    }

    public FontSymbol Clone() => new(this);
}