using System.ComponentModel;
using System.Globalization;
using ElenFontEditor.Entities;
using ExControls;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace ElenFontEditor.Controls;

public sealed partial class SymbolEditor : UserControl
{
    private FontSymbol? _symbol;

    public event EventHandler? SymbolChanged;

    private readonly Font _defaultHeaderFont = new(@"Segoe UI", 16);

    private bool _mouseDown, _dragging;
    private Point _selectionStart, _selectionEnd;
    private Rectangle _selection;
    private Color _selectedColor;
    private Color _headerBackColor;
    private Color _headerForeColor;
    private Color _poweredOffColor;
    private Color _poweredOnColor;
    private Color _borderColor;
    private Font _headerFont;

    public SymbolEditorCell[]? Clipboard { get; private set; }

    public SymbolEditor()
    {
        InitializeComponent();
        DoubleBuffered = true;
        _selectedColor = Color.DodgerBlue;
        _headerBackColor = Color.Gray;
        _headerForeColor = Color.White;
        _poweredOffColor = Color.Black;
        _poweredOnColor = Color.Yellow;
        _borderColor = Color.White;
        _headerFont = _defaultHeaderFont;
        Invalidate();
    }

    [DefaultValue(typeof(Color), nameof(Color.DodgerBlue))]
    [ExCategory(CategoryType.Appearance)]
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor == value)
                return;
            _selectedColor = value;
            Invalidate();
        }
    }

    [DefaultValue(typeof(Color), nameof(Color.Gray))]
    [ExCategory(CategoryType.Appearance)]
    public Color HeaderBackColor
    {
        get => _headerBackColor;
        set
        {
            if (_headerBackColor == value)
                return;
            _headerBackColor = value;
            Invalidate();
        }
    }

    [DefaultValue(typeof(Color), nameof(Color.White))]
    [ExCategory(CategoryType.Appearance)]
    public Color HeaderForeColor
    {
        get => _headerForeColor;
        set
        {
            if (_headerForeColor == value)
                return;
            _headerForeColor = value;
            Invalidate();
        }
    }

    [DefaultValue(typeof(Color), nameof(Color.Black))]
    [ExCategory(CategoryType.Appearance)]
    public Color PoweredOffColor
    {
        get => _poweredOffColor;
        set
        {
            if (_poweredOffColor == value)
                return;
            _poweredOffColor = value;
            Invalidate();
        }
    }

    [DefaultValue(typeof(Color), nameof(Color.Yellow))]
    [ExCategory(CategoryType.Appearance)]
    public Color PoweredOnColor
    {
        get => _poweredOnColor;
        set
        {
            if (_poweredOnColor == value)
                return;
            _poweredOnColor = value;
            Invalidate();
        }
    }

    [DefaultValue(typeof(Color), nameof(Color.White))]
    [ExCategory(CategoryType.Appearance)]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor == value)
                return;
            _borderColor = value;
            Invalidate();
        }
    }

    [ExCategory(CategoryType.Appearance)]
    public Font HeaderFont
    {
        get => _headerFont;
        set
        {
            if (Equals(_headerFont, value))
                return;
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            _headerFont = value ?? _defaultHeaderFont;
            Invalidate();
        }
    }

    private bool ShouldSerializeHeaderFont()
    {
        return !Equals(_headerFont, _defaultHeaderFont);
    }

    public SymbolEditorCellCollection Cells { get; private set; } = null!;

    public int RowSize => Height / (Cells.RowsCount + 1);
    public int ColumnSize => Width / (Cells.ColumnsCount + 1);

    public void SetEditingSymbol(FontSymbol? original)
    {
        if (original is not null)
            _symbol = original;

        Cells = new SymbolEditorCellCollection(_symbol!.Rows + 1, _symbol.Columns * 8 + 1);

        var rowSize = RowSize;
        var colSize = ColumnSize;

        for (var r = -1; r < Cells.RowsCount; r++)
        {
            for (var c = -1; c < Cells.ColumnsCount; c++)
            {
                Cells[r, c] = new SymbolEditorCell(r, c);
                if (r != -1 && c != -1) 
                    Cells[r, c].Value = _symbol[r, c / 8, c % 8];
                Cells[r, c].Bounds = new Rectangle((c + 1) * colSize, (r + 1) * rowSize, colSize, rowSize);
            }
        }

        Invalidate();
    }

    /// <summary>
    /// Zrusi upravovany znak (subor bez pisiem).
    /// </summary>
    public void ClearSymbol()
    {
        _symbol = null;
        _mouseDown = false;
        _dragging = false;
        Invalidate();
    }

    public void GetEditedSymbol()
    {
        if (_symbol == null)
            return;

        for (var r = 0; r < Cells.RowsCount; r++)
            for (var c = 0; c < Cells.ColumnsCount; c++)
                _symbol[r, c / 8, c % 8] = Cells[r, c].Value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        if (_symbol is null)
            return;

        e.Graphics.FillRectangle(new SolidBrush(_poweredOffColor), 0, 0, (Cells.ColumnsCount + 1) * ColumnSize, (Cells.RowsCount + 1) * RowSize);

        using var header = new SolidBrush(_headerBackColor);
        using var headerSel = new SolidBrush(_selectedColor);
        using var borderString = new SolidBrush(_headerForeColor);
        using var onBrush = new SolidBrush(_poweredOnColor);
        using var borderPen = new Pen(_borderColor);
        using var borderPenSel = new Pen(_selectedColor, 2);

        for (var r = -1; r < Cells.RowsCount; r++)
        {
            for (var c = -1; c < Cells.ColumnsCount; c++)
            {
                Draw(Cells[r,c], e.Graphics);
            }
        }

        foreach (SymbolEditorCell cell in Cells)
        {
            if (cell.Selected) 
                e.Graphics.DrawRectangle(borderPenSel, cell.Bounds);
        }

        void Draw(SymbolEditorCell cell, Graphics g)
        {

            if (cell.IsColumnHeader || cell.IsRowHeader)
            {
                g.FillRectangle(cell.Selected ? headerSel : header, cell.Bounds);
                if (cell.IsColumnHeader ^ cell.IsRowHeader)
                {
                    if (cell.IsColumnHeader)
                        g.DrawString((cell.Column + 1).ToString(CultureInfo.InvariantCulture), _headerFont, borderString, cell.Bounds);
                    else if (cell.IsRowHeader) 
                        g.DrawString((cell.Row + 1).ToString(CultureInfo.InvariantCulture), _headerFont, borderString, cell.Bounds);
                }
                g.DrawRectangle(borderPen, cell.Bounds);
            }
            else
            {
                if (cell.Value)
                {
                    g.FillRectangle(onBrush, cell.Bounds);
                }

                g.DrawRectangle(borderPen, cell.Bounds);
            }
        }
    }

    public SymbolEditorCell? GetCellFromPosition(int x, int y)
    {
        if (_symbol == null)
            return null;

        var r = y / RowSize - 1;
        var c = x / ColumnSize - 1;

        if (r < -1 || r >= Cells.RowsCount || c < -1 || c >= Cells.ColumnsCount)
            return null;

        return Cells[y / RowSize - 1, x / ColumnSize - 1];
    }

    public SymbolEditorCell[] GetCellsFromSelection()
    {
        return Cells.Cast<SymbolEditorCell>().Where(cell => _selection.IntersectsWith(cell.Bounds)).ToArray();
    }

    public void SelectAll()
    {
        if (_symbol is null)
            return;

        foreach (SymbolEditorCell cell in Cells)
            cell.Selected = true;
        Invalidate();
    }

    public void DeselectAll()
    {
        if (_symbol is null)
            return;

        foreach (SymbolEditorCell cell in Cells)
            cell.Selected = false;
        Invalidate();
    }

    public void InvertSelected()
    {
        if (_symbol is null)
            return;

        foreach (SymbolEditorCell cell in Cells)
            if (cell.Selected && cell is { IsColumnHeader: false, IsRowHeader: false })
                cell.Value = !cell.Value;
        OnSymbolChanged();
        Invalidate();
    }

    public void ClearSelection()
    {
        if (_symbol is null)
            return;

        foreach (SymbolEditorCell cell in Cells)
            if (cell.Selected)
                cell.Value = false;
        OnSymbolChanged();
        Invalidate();
    }

    public void Copy() => SetCopiedContent(false);

    public void Cut() => SetCopiedContent(true);

    private void SetCopiedContent(bool resetValue)
    {
        if (_symbol is null)
            return;

        var selectedCells = GetCellsFromSelection();
        var firstCell = selectedCells.FirstOrDefault();
        if (firstCell is null)
            return; //selection is empty

        for (var i = 0; i < selectedCells.Length; i++)
        {
            var cell = selectedCells[i];
            if (cell.IsColumnHeader || cell.IsRowHeader)
                continue;

            var copyCell = new SymbolEditorCell(cell.Row - firstCell.Row, cell.Column - firstCell.Column)
            {
                Selected = true,
                Value = cell.Value
            };

            selectedCells[i] = copyCell;

            if (resetValue)
                cell.Value = false;
        }

        Clipboard = selectedCells;
        if (resetValue)
        {
            OnSymbolChanged();
            Invalidate();
        }
    }

    public void Paste()
    {
        if (_symbol is null)
            return;
        
        if (Clipboard is null)
            return;

        var hotCell = Cells.Cast<SymbolEditorCell>().FirstOrDefault(c => c.Selected, Cells[0, 0]);

        foreach (var cell in Clipboard)
        {
            var row = hotCell.Row + cell.Row;
            var col = hotCell.Column + cell.Column;
            if (row >= Cells.RowsCount || col >= Cells.ColumnsCount)
                continue; // trimming

            var newCell = new SymbolEditorCell(row, col)
            {
                Selected = true,
                Value = cell.Value
            };
            Cells[row, col] = newCell;
        }
        
        RecalulateCellsRectangle();
        OnSymbolChanged();
        Invalidate();
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (_symbol == null || _dragging)
            return;

        if (_mouseDown)
        {
            _mouseDown = false;
            _dragging = false;
            _selectionStart = Point.Empty;
            _selectionEnd = Point.Empty;
        }

        var cell = GetCellFromPosition(e.X, e.Y);
        if (cell is null)
            return;

        var selected = cell.Selected;

        if (ModifierKeys != Keys.Control)
            DeselectAll();

        cell.Selected = selected;
        
        if (cell is {IsRowHeader: false, IsColumnHeader: false})
        {
            cell.Value = !cell.Value;
            cell.Selected = true;
        }
        else
        { 
            OnHeaderSelect(cell);
        }

        OnSymbolChanged();
        Invalidate();
    }

    private void OnSymbolChanged()
    {
        SymbolChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (_symbol == null)
            return;

        if ((e.Button & MouseButtons.Right) != 0)
        {
            tsmiPaste.Enabled = Clipboard != null;
            contextMenu.Show(MousePosition);
            return;
        }

        _mouseDown = true;
        _selectionStart = e.Location;

        if (ModifierKeys != Keys.Control)
        {
            DeselectAll();
            Invalidate();
        }
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (!_mouseDown)
            return;
        _dragging = true;
        _selectionEnd = e.Location;

        if (ModifierKeys != Keys.Control) 
            DeselectAll();
        
        SetSelectionRect();
        foreach (var cell in GetCellsFromSelection())
            cell.Selected = true;

        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (!_mouseDown)
            return;

        _mouseDown = false;
        _dragging = false;
        _selectionStart = Point.Empty;
        _selectionEnd = Point.Empty;

        var cells = GetCellsFromSelection();
        foreach (var cell in cells)
        {
            if (cell is { IsRowHeader: false, IsColumnHeader: false })
            {
                if (cells.Length == 1) 
                    cell.Value = !cell.Value;
                cell.Selected = true;
            }
            else
            {
                OnHeaderSelect(cell);
            }
        }

        OnSymbolChanged();
        Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        if (_symbol != null)
            RecalulateCellsRectangle();
        Invalidate();
    }

    private void OnHeaderSelect(SymbolEditorCell cell)
    {
        cell.Selected = !cell.Selected;
        if (cell is { IsColumnHeader: true, IsRowHeader: true })
        {
            foreach (SymbolEditorCell c in Cells)
                c.Selected = cell.Selected;
        }
        else if (cell.IsColumnHeader)
        {
            for (var r = 0; r < Cells.RowsCount; r++)
                Cells[r, cell.Column].Selected = cell.Selected;
        }
        else if (cell.IsRowHeader)
        {
            for (var c = 0; c < Cells.ColumnsCount; c++)
                Cells[cell.Row, c].Selected = cell.Selected;
        }
    }

    private void SetSelectionRect()
    {
        var x = _selectionStart.X > _selectionEnd.X ? _selectionEnd.X : _selectionStart.X;
        var y = _selectionStart.Y > _selectionEnd.Y ? _selectionEnd.Y : _selectionStart.Y;

        var width = _selectionStart.X > _selectionEnd.X ? _selectionStart.X - _selectionEnd.X : _selectionEnd.X - _selectionStart.X;
        var height = _selectionStart.Y > _selectionEnd.Y ? _selectionStart.Y - _selectionEnd.Y : _selectionEnd.Y - _selectionStart.Y;

        _selection = new Rectangle(x, y, width, height);
    }

    /// <inheritdoc />
    protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (e.KeyCode == Keys.Escape)
        {
            DeselectAll();
        }

        else if (e.KeyCode is Keys.Back or Keys.Delete)
        {
            ClearSelection();
        }
        else if (e.KeyCode is Keys.Enter or Keys.Space)
        {
            InvertSelected();
        }
        else if (e.Control)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    SelectAll();
                    break;
                case Keys.C:
                    Copy();
                    break;
                case Keys.X:
                    Cut();
                    break;
                case Keys.V:
                    Paste();
                    break;
            }
        }
    }

    private void TsmiSelectAll_Click(object sender, EventArgs e) => SelectAll();

    private void TsmiChangeState_Click(object sender, EventArgs e) => InvertSelected();

    private void TsmiCopy_Click(object sender, EventArgs e) => Copy();

    private void TsmiCut_Click(object sender, EventArgs e) => Cut();

    private void TsmiPaste_Click(object sender, EventArgs e) => Paste();

    private void RecalulateCellsRectangle()
    {
        var rowSize = RowSize;
        var colSize = ColumnSize;

        for (var r = -1; r < Cells.RowsCount; r++)
        {
            for (var c = -1; c < Cells.ColumnsCount; c++)
            {
                Cells[r, c].Bounds = new Rectangle((c + 1) * colSize, (r + 1) * rowSize, colSize, rowSize);
            }
        }
    }
}