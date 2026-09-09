namespace ElenFontEditor.Controls;

public class SymbolEditorCell
{
    public Rectangle Bounds { get; internal set; }
    public bool Selected { get; set; }
    public bool IsRowHeader => Column == -1;
    public bool IsColumnHeader => Row == -1;
    public int Row { get; }
    public int Column { get; }
    public bool Value { get; set; }

    internal SymbolEditorCell(int row, int column)
    {
        Row = row;
        Column = column;
    }
}