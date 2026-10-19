using System.Globalization;
using ElenFontEditor.Entities;
using ElenFontEditor.History;
using ElenFontEditor.Properties;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

/// <summary>
/// Kroky spat a znova: po kazdej dokoncenej uprave suboru pisiem (body znaku, sirka znaku, sprava pisiem, premenovanie)
/// sa zapamata jeho stav. Upravy jednej operacie sa zlucia do jedneho kroku - stav sa zapamata az po spracovani spravy,
/// ktora operaciu vyvolala; klik, ktory nic nezmenil (napr. len vyber bodov), krok nevytvori.
/// </summary>
public partial class FMain
{
    private FontsFileHistory? _history;

    /// <summary>Popis dokoncovanej upravy; null = vseobecna uprava suboru.</summary>
    private string? _changeLabel;

    private bool _commitPending;

    /// <summary>Subor meni krok spat - udalosti okna nie su nova uprava.</summary>
    private bool _restoring;

    /// <summary>Zacne historiu otvoreneho suboru; <paramref name="saved" /> = subor je na disku v tomto stave.</summary>
    private void StartHistory(bool saved)
    {
        _commitPending = false;
        _changeLabel = null;
        _history = new FontsFileHistory(_openedFile!, saved);
        UpdateHistoryUi();
    }

    /// <summary>Subor sa zmenil operaciou <paramref name="label" /> - krok spat sa zapamata po jej dokonceni.</summary>
    private void MarkChanged(string label)
    {
        if (_history is null || _restoring)
            return;

        _changeLabel ??= label;
        if (_commitPending || !IsHandleCreated)
            return;
        _commitPending = true;
        BeginInvoke(FlushCommit);
    }

    /// <summary>Zapamata cakajuci krok hned - pred ulozenim, dialogmi a krokom spat.</summary>
    private void FlushCommit()
    {
        if (!_commitPending)
            return;
        _commitPending = false;
        var label = _changeLabel ?? Resources.History_Change;
        _changeLabel = null;
        if (_history is null)
            return;

        // body upravovaneho znaku su zatial len v editore
        SaveCurrentSymbol();
        _history.Commit(label);
        Saved = _history.IsSaved;
        UpdateHistoryUi();
    }

    /// <summary>Dokonci rozpracovane upravy - rozpisanu bunku a cakajuci krok spat.</summary>
    private void FinishEdits()
    {
        // chybna hodnota rozpisanej bunky sa zahodi - bunka ostane s povodnou
        if (dgvSymbols.IsCurrentCellInEditMode && !dgvSymbols.EndEdit())
        {
            dgvSymbols.CancelEdit();
            dgvSymbols.EndEdit();
        }

        FlushCommit();
    }

    private void Undo() => Undo(1);

    private void Redo() => Redo(1);

    private void Undo(int steps) => MoveInHistory(history => history.Undo(steps));

    private void Redo(int steps) => MoveInHistory(history => history.Redo(steps));

    private void MoveInHistory(Action<FontsFileHistory> move)
    {
        // rozpisana bunka je tiez uprava - najprv sa zapamata
        FinishEdits();
        if (_history is not { } history)
            return;

        var fontIndex = tscomboFonts.SelectedIndex;
        var symbolIndex = dgvSymbols.CurrentCell?.RowIndex ?? -1;
        var firstRow = dgvSymbols.FirstDisplayedScrollingRowIndex;

        // obnova vytvori nove pisma a znaky - editor nesmie zapisat body do starych
        _editingSymbol = null;
        _restoring = true;
        try
        {
            move(history);
            ShowFonts(fontIndex, symbolIndex, firstRow);
        }
        finally
        {
            _restoring = false;
        }

        Saved = history.IsSaved;
        UpdateTitle();
        UpdateHistoryUi();
    }

    /// <summary>
    /// Zobrazi pisma suboru po ich vymene (otvorenie, sprava pisiem, krok spat): vyberie pismo a znak na rovnakom mieste.
    /// </summary>
    private void ShowFonts(int fontIndex, int symbolIndex, int firstRow = -1)
    {
        var column = dgvSymbols.CurrentCell?.ColumnIndex ?? -1;
        var fonts = _openedFile!.Fonts;

        _editingSymbol = null;
        tscomboFonts.SelectedIndex = -1;
        if (fonts.Count == 0)
        {
            dgvSymbols.DataSource = null;
            symbolEditor.ClearSymbol();
            return;
        }

        tscomboFonts.SelectedIndex = Math.Clamp(fontIndex, 0, fonts.Count - 1);
        if (firstRow >= 0 && firstRow < dgvSymbols.RowCount)
            dgvSymbols.FirstDisplayedScrollingRowIndex = firstRow;
        if (symbolIndex < 0 || symbolIndex >= dgvSymbols.RowCount)
            return;

        if (column < 0 || !dgvSymbols.Columns[column].Visible)
            column = dgvSymbols.Columns.GetFirstColumn(DataGridViewElementStates.Visible)?.Index ?? -1;
        if (column >= 0)
            dgvSymbols.CurrentCell = dgvSymbols.Rows[symbolIndex].Cells[column];
    }

    /// <summary>Znak v popise kroku: tlacitelny v uvodzovkach, inak jeho cislo.</summary>
    private static string SymbolLabel(FontSymbol symbol) => SymbolLabel(symbol.Index, GlobData.Config.CharEncoding);

    /// <summary>Znak s kodom <paramref name="code" /> v popise kroku - v kodovani ako stlpec Znak.</summary>
    internal static string SymbolLabel(int code, SymbolCharset charset) =>
        SymbolChars.Printable(code, charset) is { } c
            ? string.Format(CultureInfo.CurrentCulture, Resources.History_SymbolChar, c)
            : string.Format(CultureInfo.CurrentCulture, Resources.History_SymbolIndex, code);

    /// <summary>Popisy prikazov a tlacidiel Spat a Znova podla historie.</summary>
    private void UpdateHistoryUi()
    {
        var undo = _history?.UndoLabel;
        var redo = _history?.RedoLabel;
        tsmimUndo.Text = undo is null ? Resources.Cmd_Undo : string.Format(CultureInfo.CurrentCulture, Resources.History_UndoFormat, undo);
        tsmimRedo.Text = redo is null ? Resources.Cmd_Redo : string.Format(CultureInfo.CurrentCulture, Resources.History_RedoFormat, redo);
        tssbUndo.ToolTipText = tsmimUndo.Text + ShortcutHint(EfeCommands.Undo.Id);
        tssbRedo.ToolTipText = tsmimRedo.Text + ShortcutHint(EfeCommands.Redo.Id);
        _commands[EfeCommands.Undo.Id].UpdateState();
        _commands[EfeCommands.Redo.Id].UpdateState();
    }

    private string ShortcutHint(string id) =>
        _commands[id].ShortcutKeys is var keys and not Keys.None ? $" ({new KeysConverter().ConvertToString(null, CultureInfo.CurrentCulture, keys)})" : "";

    /// <summary>Rozbalovacia ponuka tlacidla Spat alebo Znova - zoznam krokov, klik vrati alebo zopakuje vsetky po vybrany.</summary>
    private static void FillHistoryMenu(ToolStripSplitButton button, IReadOnlyList<string> labels, Action<int> run)
    {
        button.DropDownItems.Clear();
        for (var i = 0; i < labels.Count; i++)
        {
            var steps = i + 1;
            var item = new ToolStripMenuItem(labels[i], null, (_, _) => run(steps));
            item.ApplyThemeAndFont();
            button.DropDownItems.Add(item);
        }
    }

    private void tssbUndo_DropDownOpening(object? sender, EventArgs e)
    {
        FinishEdits();
        FillHistoryMenu(tssbUndo, _history?.UndoLabels ?? [], Undo);
    }

    private void tssbRedo_DropDownOpening(object? sender, EventArgs e) =>
        FillHistoryMenu(tssbRedo, _history?.RedoLabels ?? [], Redo);
}
