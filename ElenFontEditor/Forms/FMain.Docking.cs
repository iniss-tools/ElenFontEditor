using ElenFontEditor.Controls;
using ElenFontEditor.Properties;
using ExControls;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

// ukotvitelne panely (ExDockManager): editor znaku v dokumente, zoznam znakov vlavo, nahlad na tabuli dole
public partial class FMain
{
    // kluce panelov v ulozenom rozlozeni (config.xml) - nemenit
    private const string SymbolsPanelKey = "Symbols";
    private const string BoardPreviewPanelKey = "BoardPreview";

    private readonly ExDockManager _dock = AppDock.Create();
    private readonly ExDockPanel _symbolsPanel = new(SymbolsPanelKey, Resources.Panel_Symbols);
    private readonly ExDockPanel _boardPreviewPanel = new(BoardPreviewPanelKey, Resources.Panel_BoardPreview);
    private readonly BoardPreview _boardPreview = new() { Dock = DockStyle.Fill };
    private string _defaultLayout = "";

    /// <summary>
    /// Navrh okna ma zoznam znakov a editor v deliacom liste. Za behu ide zoznam do ukotvitelneho panela, editor do
    /// dokumentu a deliaci list sa zahodi; rozlozenie z nastaveni sa obnovi az po teme a pismach.
    /// </summary>
    private void InitDocking()
    {
        SuspendLayout();
        var designed = splitContainer1;
        dgvSymbols.Parent?.Controls.Remove(dgvSymbols);
        symbolEditor.Parent?.Controls.Remove(symbolEditor);
        dgvSymbols.Dock = DockStyle.Fill;
        _symbolsPanel.Controls.Add(dgvSymbols);
        symbolEditor.Dock = DockStyle.Fill;
        _dock.Document.Controls.Add(symbolEditor);

        // na miesto deliaceho listu - pred panelmi ukotvenymi k okraju, aby vyplnil zvysok okna
        var index = Controls.GetChildIndex(designed);
        Controls.Remove(designed);
        designed.Dispose();
        Controls.Add(_dock);
        Controls.SetChildIndex(_dock, index);

        _boardPreviewPanel.Controls.Add(_boardPreview);
        _boardPreview.SymbolClick += (_, code) => SelectSymbol(code);

        _dock.AddPanel(_symbolsPanel, ExDockSide.Left);
        _dock.AddPanel(_boardPreviewPanel, ExDockSide.Bottom);
        // rozmery zon su v logickych bodoch (96 DPI) - spravca ich prepocita sam
        _dock.SetZoneSize(ExDockSide.Left, 300);
        _dock.SetZoneSize(ExDockSide.Bottom, 170);
        ResumeLayout(true);

        _defaultLayout = _dock.SaveLayout();
        // rozlozenie sa uklada pri zatvoreni okna spolu s ostatnymi nastaveniami (az ked zatvorenie nikto nezrusil)
        FormClosed += (_, _) =>
        {
            GlobData.Config.DockLayout = _dock.SaveLayout();
            SaveConfig();
        };
    }

    private void CreateViewCommands()
    {
        _commands.Add(EfeCommands.ShowSymbols, () => _dock.ShowPanel(_symbolsPanel)).Bind(tsmimShowSymbols);
        _commands.Add(EfeCommands.ShowBoardPreview, () => _dock.ShowPanel(_boardPreviewPanel)).Bind(tsmimShowBoardPreview);
        _commands.Add(EfeCommands.ResetLayout, () => _dock.LoadLayout(_defaultLayout)).Bind(tsmimResetLayout);
    }

    /// <summary>
    /// Nahlad na tabuli upravovanym pismom - po kazdej zmene suboru, vybere pisma alebo kodovania.
    /// </summary>
    private void UpdateBoardPreview()
    {
        var index = tscomboFonts.SelectedIndex;
        if (_openedFile is null)
            _boardPreview.Clear("");
        else if (index >= 0 && index < _openedFile.Fonts.Count)
            _boardPreview.ShowFont(_openedFile.Fonts[index], GlobData.Config.CharEncoding);
        else
            _boardPreview.Clear(Resources.BoardPreview_NoFont);
        _boardPreview.HighlightSymbol(_editingSymbol?.Index ?? -1);
    }

    /// <summary>
    /// Vyberie v zozname znak s kodom <paramref name="code" /> (klik na znak v nahlade).
    /// </summary>
    private void SelectSymbol(int code)
    {
        if (code < 0 || code >= dgvSymbols.RowCount)
            return;

        var column = dgvSymbols.CurrentCell?.ColumnIndex ?? -1;
        if (column < 0 || !dgvSymbols.Columns[column].Visible)
            column = dgvSymbols.Columns.GetFirstColumn(DataGridViewElementStates.Visible)?.Index ?? -1;
        if (column >= 0)
            dgvSymbols.CurrentCell = dgvSymbols.Rows[code].Cells[column];
    }
}
