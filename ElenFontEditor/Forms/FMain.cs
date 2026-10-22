using System.Globalization;
using ElenFontEditor.Entities;
using ExControls;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using ToolsCore;
using ToolsCore.Commands;
using ToolsCore.Forms;
using ToolsCore.Iniss.Tools;
using ToolsCore.Tools;
using ToolsCore.Updates;
using ToolsCore.XML;
using Utils = ToolsCore.Tools.Utils;

namespace ElenFontEditor.Forms;

public partial class FMain : ExForm
{
    private readonly CommandSet _commands = new();
    private FontsFile? _openedFile;
    private FontSymbol? _editingSymbol;

    /// <summary>
    /// Kde je otvoreny subor na disku (posledne otvoreny alebo ulozeny); null = novy subor este nie je ulozeny.
    /// Premenovany subor sa tu zmaze az po ulozeni pod novym nazvom - premenovanie sa tak da vratit spat.
    /// </summary>
    private string? _diskPath;

    public FMain()
    {
        InitializeComponent();
        SetRecentFiles();
        this.ApplyThemeAndFonts();

        symbolEditor.BackColor = GlobSettings.UsingStyle.ControlsColorScheme.Box.BackColor;
        symbolEditor.SelectedColor = GlobSettings.UsingStyle.ControlsColorScheme.Highlight.BackColor;

        switch (GlobData.Config.DesktopMenuMode)
        {
            case DesktopMenu.MsTs:
                menuStripMain.Visible = true;
                toolStripMain.Visible = true;
                menuStripMain.Items.Remove(tscomboFonts);
                break;
            case DesktopMenu.TsOnly:
                menuStripMain.Visible = false;
                toolStripMain.Visible = true;
                menuStripMain.Items.Remove(tscomboFonts);
                break;
            case DesktopMenu.MsOnly:
                menuStripMain.Visible = true;
                toolStripMain.Visible = false;
                menuStripMain.Items.Add(tscomboFonts);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        CreateCommands();
        _commands.ApplyShortcuts(GlobData.Config.Shortcuts);
        UpdateCommandStates();
        UpdateHistoryUi();
        dgvSymbols.RowHeadersVisible = GlobData.Config.ShowRowsHeader;
        InitColumns();
        SetColumnsAutoWidth();

        tsslArray.Font = GlobData.Config.Fonts.StateRow;
        tsslPosition.Font = GlobData.Config.Fonts.StateRow;
    }

    /// <summary>
    /// Prikazy hlavneho okna naviazane naraz na tlacidlo aj polozku ponuky.
    /// </summary>
    private void CreateCommands()
    {
        Func<bool> opened = () => _openedFile is not null;
        _commands.Add(EfeCommands.New, ShowNew).Bind(tsmimNew, tsbNew);
        _commands.Add(EfeCommands.Open, () => OpenFile()).Bind(tsmimOpen, tsbOpen);
        _commands.Add(EfeCommands.Save, DoSave, opened).Bind(tsmimSave, tsbSave);
        _commands.Add(EfeCommands.Undo, Undo, () => _history?.CanUndo == true).Bind(tsmimUndo, tssbUndo);
        _commands.Add(EfeCommands.Redo, Redo, () => _history?.CanRedo == true).Bind(tsmimRedo, tssbRedo);
        _commands.Add(EfeCommands.FontManager, ShowFontManager, opened).Bind(tsmimFontManager, tsbFontManager);
        _commands.Add(EfeCommands.RenameFile, ShowRenameFile, opened).Bind(tsmimRenameFile, tsbRenameFile);
        _commands.Add(EfeCommands.AppSettings, ShowAppSettings).Bind(tsmimAppSettings, tsbAppSettings);
        _commands.Add(EfeCommands.InfoApp, ShowInfoApp).Bind(tsmimInfoApp, tsbInfoApp);
        _commands.Add(EfeCommands.CheckUpdates, () => AppUpdates.CheckNow(this), () => AppUpdates.Available).Bind(tsmimCheckUpdates);
    }

    /// <summary>
    /// Povoli prikazy podla toho, ci je otvoreny subor pisiem.
    /// </summary>
    private void UpdateCommandStates()
    {
        _commands.UpdateStates();
        tsmiEdit.Enabled = _openedFile is not null;
        tscomboFonts.Enabled = _openedFile is not null;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) =>
        _commands.ProcessShortcut(keyData) || base.ProcessCmdKey(ref msg, keyData);

    private void InitColumns()
    {
        static void SetCol(DataGridViewColumn column, DesktopColumn format)
        {
            column.Visible = format.Visible;
            column.MinimumWidth = format.MinWidth;
            column.DisplayIndex = format.Order;
        }
        
        var columns = GlobData.Config.DesktopCols;

        SetCol(cIndex, columns.Index);
        SetCol(cName, columns.Char);
        SetCol(cSymbol, columns.Symbol);
        SetCol(cWidth, columns.Width);
        SetCol(cRowCount, columns.RowCount);
        SetCol(cColCount, columns.ColCount);

        dgvSymbols.Columns[^1].AutoSizeMode = GlobData.Config.FitLastColumn
            ? DataGridViewAutoSizeColumnMode.Fill
            : DataGridViewAutoSizeColumnMode.None;
    }

    private void SetColumnsAutoWidth()
    {
        for (var i = 0; i < dgvSymbols.Columns.Count - 1; i++)
        {
            dgvSymbols.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            var widthCol = dgvSymbols.Columns[i].Width;
            dgvSymbols.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvSymbols.Columns[i].Width = widthCol;
        }
    }

    private void UpdateMainUI()
    {
        var menu = GlobData.Config.DesktopMenuMode;
        menuStripMain.Visible = menu is DesktopMenu.MsTs or DesktopMenu.MsOnly;
        toolStripMain.Visible = menu is DesktopMenu.MsTs or DesktopMenu.TsOnly;
        dgvSymbols.RowHeadersVisible = GlobData.Config.ShowRowsHeader;
        symbolEditor.BackColor = GlobSettings.UsingStyle.ControlsColorScheme.Box.BackColor;
        symbolEditor.SelectedColor = GlobSettings.UsingStyle.ControlsColorScheme.Highlight.BackColor;

        this.ApplyThemeAndFonts();
        InitColumns();
        _commands.ApplyShortcuts(GlobData.Config.Shortcuts);
        UpdateHistoryUi();
        SetColumnsAutoWidth();
        Invalidate(true);
        AppInit.MsgBoxStyleInit(GlobData.UsingStyle, GlobData.Config);
    }

    private bool Saved
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            UpdateTitle();
        }
    } = true;

    /// <summary>
    /// Titulok okna: cesta k otvorenemu suboru a hviezdicka, ak ma neulozene zmeny.
    /// </summary>
    private void UpdateTitle() =>
        base.Text = _openedFile is null
            ? Application.ProductName
            : Application.ProductName + @" - " + _openedFile.PathName + (Saved ? "" : @"*");

    /// <summary>
    ///     Naplní menu naposledy otvorenými súbormi zoradenými od naposledy otvoreného.
    /// </summary>
    private void SetRecentFiles()
    {
        tsmimRecent.DropDownItems.Clear();
        tssbRecent.DropDownItems.Clear();

        var recent = AppRegistry.GetOpenedProjects();
        foreach (var item in recent)
        {
            var mitemA = new ToolStripMenuItem(item.Path);
            var mitemB = new ToolStripMenuItem(item.Path);

            mitemA.Click += (_, _) => OpenFile(item.Path);
            mitemB.Click += (_, _) => OpenFile(item.Path);

            mitemA.ApplyThemeAndFont();
            mitemB.ApplyThemeAndFont();

            tsmimRecent.DropDownItems.Add(mitemA);
            tssbRecent.DropDownItems.Add(mitemB);
        }
    }

    private void ShowNew()
    {
        while (true)
        {
            var result = dialogSaveNewFile.ShowDialog(this);
            if (result != DialogResult.OK) 
                return;

            var name = Path.GetFileNameWithoutExtension(dialogSaveNewFile.FileName);
            if (name.Length > FontsFile.NameMaxLength)
            {
                Utils.ShowError(Resources.FMain_FileName_Length);
                continue;
            }

            // nazov sa zapisuje aj do hlavicky suboru v ASCII - s diakritikou by sa nezhodoval s menom suboru
            if (!FontRules.IsAscii(name))
            {
                Utils.ShowError(Resources.FileName_Ascii);
                continue;
            }

            if (File.Exists(dialogSaveNewFile.FileName))
            {
                Utils.ShowError(Resources.FMain_FileExists);
                continue;
            }

            OpenFile(dialogSaveNewFile.FileName, true);
            break;
        }
    }

    private void OpenFile()
    {
        var result = dialogOpenBinFile.ShowDialog(this);
        if (result != DialogResult.OK) 
            return;

        OpenFile(dialogOpenBinFile.FileName);
    }

    private void OpenFile(string pathname, bool newFile = false)
    {
        FinishEdits();
        if (_openedFile != null && !Saved)
        {
            var result = Utils.ShowQuestion(Resources.FMain_SaveChanges, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    if (!TrySave())
                        return;
                    break;
                case DialogResult.No:
                    break;
                default:
                    return;
            }
        }

        // novy subor sa do nedavnych dostane az ulozenim
        if (newFile)
        {
            _openedFile = new FontsFile
            {
                PathName = pathname,
                Name = Path.GetFileNameWithoutExtension(pathname)
            };
            OnOpenFile(_openedFile, true);
            return;
        }

        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
        {
            try
            {
                _openedFile = ElenBinParser.LoadFile(pathname);
                RememberFile(pathname);
                OnOpenFile(_openedFile);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                FError.ShowError(GlobData.Config.DebugModeGUI == DebugMode.OnlyMessage ? e.Message : e.ToString());
            }
        }
        else
        {
            _openedFile = ElenBinParser.LoadFile(pathname);
            RememberFile(pathname);
            OnOpenFile(_openedFile);
        }
    }

    /// <summary>
    /// Subor na disku do nedavnych a ako posledny otvoreny - len uspesne otvoreny alebo ulozeny.
    /// </summary>
    private void RememberFile(string pathname)
    {
        AppRegistry.SetUsageOfProject(pathname);
        AppRegistry.SetLastProject(pathname);
        SetRecentFiles();
    }

    private void FMain_Load(object sender, EventArgs e)
    {
        AppRegistry.RegisterJumpList(GlobalResources.RRecentFiles);
        AppUpdates.CheckOnStartup(this, GlobData.Config);

        //cesta k suboru zadana ako argument ma prednost pred poslednym otvorenym suborom
        var path = Utils.GetProjectPathFromArgs();
        if (path is null && GlobData.Config.Startup == StartupType.LastProject)
            path = AppRegistry.GetLastProject();

        if (File.Exists(path))
            OpenFile(path);
    }

    private void OnOpenFile(FontsFile file, bool newFile = false)
    {
        // znak z predchadzajuceho suboru sa uz nezapisuje
        _editingSymbol = null;
        tscomboFonts.ComboBox.DataSource = file.Fonts;
        ShowFonts(0, 0);

        _diskPath = newFile ? null : file.PathName;
        StartHistory(saved: !newFile);
        Saved = !newFile;
        UpdateTitle();
        UpdateCommandStates();
    }

    private void DoSave() => TrySave();

    /// <summary>
    /// Ulozi otvoreny subor. Pri chybe ju ohlasi a zmeny ostanu neulozene.
    /// </summary>
    /// <returns><c>true</c>, ak sa subor ulozil.</returns>
    private bool TrySave()
    {
        FinishEdits();
        SaveCurrentSymbol();
        var file = _openedFile!;
        try
        {
            ElenBinParser.SaveFile(file);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException or FormatException or ArgumentException)
        {
            Log.Exception(e);
            Utils.ShowError(string.Format(CultureInfo.CurrentCulture, Resources.FMain_SaveFailed, e.Message));
            return false;
        }

        // premenovany subor - povodny sa maze az teraz, ked je ulozeny novy
        if (_diskPath is not null && !string.Equals(Path.GetFullPath(_diskPath), Path.GetFullPath(file.PathName), StringComparison.OrdinalIgnoreCase) &&
            File.Exists(_diskPath))
        {
            try
            {
                File.Delete(_diskPath);
            }
            catch (Exception e) when (e is IOException or UnauthorizedAccessException)
            {
                // novy subor je ulozeny - povodny ostane na disku
                Log.Exception(e);
            }
        }

        _diskPath = file.PathName;
        RememberFile(file.PathName);
        _history?.MarkSaved();
        Saved = true;
        return true;
    }

    private void ShowFontManager()
    {
        FinishEdits();
        SaveCurrentSymbol();
        var fontIndex = tscomboFonts.SelectedIndex;
        var symbolIndex = dgvSymbols.CurrentCell?.RowIndex ?? -1;
        var firstRow = dgvSymbols.FirstDisplayedScrollingRowIndex;
        var fmanager = new FFontManager(_openedFile!);
        if (fmanager.ShowDialog(this) == DialogResult.OK)
        {
            _openedFile!.Fonts.ResetBindings();
            ShowFonts(fontIndex, symbolIndex, firstRow);
            MarkChanged(Resources.History_FontManager);
            FlushCommit();
        }
    }

    private void ShowRenameFile()
    {
        FinishEdits();
        SaveCurrentSymbol();
        var frename = new FRenameFile(_openedFile!, _diskPath);
        if (frename.ShowDialog(this) == DialogResult.OK)
        {
            // povodny subor sa zmaze az pri ulozeni (DoSave)
            UpdateTitle();
            MarkChanged(Resources.History_RenameFile);
            FlushCommit();
        }
    }

    private void ShowAppSettings()
    {
        var form = new FAppSettings(GlobData.Config, GlobData.Styles);
        if (form.ShowDialog() == DialogResult.OK)
        {
            UpdateMainUI();
        }
    }

    private void ShowInfoApp()
    {
        var finfo = new FAboutApp(Resources.AboutAppDescription, Resources.fnt);
        finfo.ShowDialog(this);
    }


    private void tscomboFonts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tscomboFonts.SelectedIndex == -1)
            return;

        dgvSymbols.DataSource = _openedFile!.Fonts[tscomboFonts.SelectedIndex].Symbols;
    }


    private void FMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        FinishEdits();
        // chybna bunka (FinishEdits ju zahodil) nebrani zatvoreniu
        e.Cancel = false;
        if (_openedFile == null || Saved)
            return;

        var result = Utils.ShowQuestion(Resources.FMain_SaveChanges, MessageBoxButtons.YesNoCancel);
        switch (result)
        {
            case DialogResult.Yes:
                // neuspesne ulozenie okno nezatvori - zmeny by sa stratili
                e.Cancel = !TrySave();
                break;
            case DialogResult.No:
                e.Cancel = false;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }

    private void SaveCurrentSymbol()
    {
        if (_editingSymbol != null)
            symbolEditor.GetEditedSymbol();
    }

    private void dgvSymbols_SelectionChanged(object sender, EventArgs e)
    {
        SaveCurrentSymbol();

        if (dgvSymbols.SelectedRows.Count == 0)
            return;

        _editingSymbol = dgvSymbols.SelectedRows[0].DataBoundItem as FontSymbol;
        symbolEditor.SetEditingSymbol(_editingSymbol);
    }

    private void symbolEditor_MouseMove(object sender, MouseEventArgs e)
    {
        if (_editingSymbol == null) 
            return;

        // riadok:stlpec bodu ako v hlavickach editora (od 1)
        var cell = symbolEditor.GetCellFromPosition(e.X, e.Y);
        if (cell is { IsRowHeader: false, IsColumnHeader: false })
            tsslPosition.Text = @$"{cell.Row + 1}:{cell.Column + 1}";
    }

    private void symbolEditor_SymbolChanged(object sender, EventArgs e)
    {
        if (_editingSymbol is { } symbol)
            MarkChanged(string.Format(CultureInfo.CurrentCulture, Resources.History_EditSymbol, SymbolLabel(symbol), symbol.Font.Name));
    }

    private void dgvSymbols_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != cWidth.Index)
            return;

        var error = FontRules.SymbolWidthError(e.FormattedValue?.ToString());
        dgvSymbols.Rows[e.RowIndex].ErrorText = error ?? "";
        e.Cancel = error != null;
    }

    private void dgvSymbols_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != cWidth.Index)
            return;

        // ukoncenie upravy prikazom (ulozenie, krok spat) validaciu bunky obide - chybny text sa nezapise,
        // bunka ostane s povodnou sirkou
        var text = e.Value?.ToString();
        e.Value = FontRules.SymbolWidthError(text) is null
            ? int.Parse(text!, NumberStyles.Integer, CultureInfo.CurrentCulture)
            : dgvSymbols.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        e.ParsingApplied = true;
    }

    private void dgvSymbols_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        // stlpec Znak je naviazany na kod znaku - text podla kodovania z nastaveni sa sklada pri kresleni,
        // zmena nastavenia sa tak prejavi hned v otvorenom zozname
        if (e.RowIndex < 0 || e.ColumnIndex != cName.Index || e.Value is not int code)
            return;

        e.Value = SymbolChars.Text(code, GlobData.Config.CharEncoding);
        e.FormattingApplied = true;
    }

    private void dgvSymbols_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            dgvSymbols.Rows[e.RowIndex].ErrorText = "";
    }

    private void dgvSymbols_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != cWidth.Index || dgvSymbols.Rows[e.RowIndex].DataBoundItem is not FontSymbol symbol)
            return;

        MarkChanged(string.Format(CultureInfo.CurrentCulture, Resources.History_SymbolWidth, SymbolLabel(symbol), symbol.Font.Name));
    }

    private void FMain_KeyDown(object sender, KeyEventArgs e)
    {
        // pri pisani do bunky patria Ctrl+V a Ctrl+A textu bunky
        if (dgvSymbols.IsCurrentCellInEditMode)
            return;

        if (e is { Control: true, KeyCode: Keys.V })
        {
            symbolEditor.Paste();
            e.Handled = true;
        }
        else if (e.Control && e.KeyCode == Keys.A)
        {
            symbolEditor.SelectAll();
            e.Handled = true;
        }
    }
}