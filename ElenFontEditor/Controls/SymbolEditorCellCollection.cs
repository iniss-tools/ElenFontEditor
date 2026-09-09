using System.Collections;

namespace ElenFontEditor.Controls;

public class SymbolEditorCellCollection : ICollection
{
    private readonly SymbolEditorCell[,] _cells;

    internal SymbolEditorCellCollection(int rowsCount, int columnsCount)
    {
        _cells = new SymbolEditorCell[rowsCount, columnsCount];
        Count = rowsCount * columnsCount;
        RowsCount = rowsCount - 1;
        ColumnsCount = columnsCount - 1;
    }

    /// <summary>
    ///     Pocet riadkov (bez riadku hlavicky).
    /// </summary>
    public int RowsCount { get; }

    /// <summary>
    ///     Pocet stlpcov (bez stlpca hlavicky).
    /// </summary>
    public int ColumnsCount { get; }

    /// <inheritdoc />
    public IEnumerator GetEnumerator() => _cells.GetEnumerator();

    /// <inheritdoc />
    public void CopyTo(Array array, int index) => _cells.CopyTo(array,index);

    /// <inheritdoc />
    public int Count { get; }

    /// <inheritdoc />
    public object SyncRoot => _cells.SyncRoot;

    /// <inheritdoc />
    public bool IsSynchronized => _cells.IsSynchronized;
        
    public SymbolEditorCell this[int row, int column]
    {
        get
        {
            CheckBounds(row, column);
            return _cells[row + 1, column + 1];
        }
        internal set => _cells[row + 1, column + 1] = value;
    }

    private void CheckBounds(int row, int col)
    {
        if (row < -1 || row >= RowsCount)
            throw new ArgumentOutOfRangeException($"Index of row must be in the range -1 <= X < {RowsCount}.");

        if (col < -1 || col >= ColumnsCount)
            throw new ArgumentOutOfRangeException($"Index of column must be in the range -1 <= X < {ColumnsCount}.");
    }
}