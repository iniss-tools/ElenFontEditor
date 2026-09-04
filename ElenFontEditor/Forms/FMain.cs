using System;
using System.IO;
using System.Windows.Forms;
using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using ToolsCore;
using ToolsCore.Forms;
using ToolsCore.Tools;
using ToolsCore.XML;
using Utils = ToolsCore.Tools.Utils;

namespace ElenFontEditor.Forms;

public partial class FMain : Form
{
    private FontsFile _openedFile;
    private bool _saved = true;
    private FontSymbol _editingSymbol;

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

        InitShortcuts();
        InitColumns();
        SetColumnsAutoWidth();

        tsslArray.Font = GlobData.Config.Fonts.StateRow;
        tsslPosition.Font = GlobData.Config.Fonts.StateRow;

        if (GlobData.UsingStyle.HighlightStatusBar)
        {
            statusStripMain.BackColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            tsslArray.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslPosition.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
        }
    }

    private void InitShortcuts()
    {
        var shortcuts = GlobData.Config.Shortcuts;

        tsmimNew.ShortcutKeys = shortcuts.New;
        tsmimOpen.ShortcutKeys = shortcuts.Open;
        tsmimSave.ShortcutKeys = shortcuts.Save;
        tsmimFontManager.ShortcutKeys = shortcuts.FontManager;
        tsmimRenameFile.ShortcutKeys = shortcuts.RenameFile;
        tsmimAppSettings.ShortcutKeys = shortcuts.AppSettings;
    }

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

        dgvSymbols.Columns[dgvSymbols.Columns.Count - 1].AutoSizeMode = GlobData.Config.FitLastColumn
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
        InitShortcuts();
        SetColumnsAutoWidth();
        Invalidate(true);
        AppInit.MsgBoxStyleInit(GlobData.UsingStyle, GlobData.Config);
        if (GlobData.UsingStyle.HighlightStatusBar)
        {
            statusStripMain.BackColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            tsslArray.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslPosition.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
        }
    }

    private bool Saved
    {
        get => _saved;
        set
        {
            if (_saved == value)
                return;
            if (value) 
                base.Text = base.Text.Replace("*", "");
            else
                base.Text += @"*";
            _saved = value;
        }
    }

    private void SetRecentFiles()
    {
        var recent = AppRegistry.GetOpenedProjects();
        foreach (var item in recent)
        {
            var mitemA = new ToolStripMenuItem(item.Path);
            var mitemB = new ToolStripMenuItem(item.Path);

            mitemA.Click += (_, _) => OpenFile(item.Path);
            mitemB.Click += (_, _) => OpenFile(item.Path);

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

            if (Path.GetFileNameWithoutExtension(dialogSaveNewFile.FileName)?.Length > 32)
            {
                Utils.ShowError(Resources.FMain_FileName_Length);
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
        if (_openedFile != null && !Saved)
        {
            var result = Utils.ShowQuestion(Resources.FMain_SaveChanges, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    DoSave();
                    break;
                case DialogResult.No:
                    break;
                default:
                    return;
            }
        }

        AppRegistry.SetUsageOfProject(pathname);
        AppRegistry.SetLastProject(pathname);

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
            OnOpenFile(_openedFile);
        }
    }

    private void FMain_Load(object sender, EventArgs e)
    {
        if (GlobData.Config.Startup == StartupType.LastProject)
        {
            var path = AppRegistry.GetLastProject();
            if (File.Exists(path)) 
                OpenFile(path);
        }
    }

    private void OnOpenFile(FontsFile file, bool newFile = false)
    {
        InitGUI(true);
        base.Text = Application.ProductName + @" - " + file.PathName;
        tscomboFonts.ComboBox.DataSource = file.Fonts;
        if (newFile) Saved = false;
    }

    private void DoSave()
    {
        SaveCurrentSymbol();
        Saved = true;
        ElenBinParser.SaveFile(_openedFile);
    }

    private void ShowFontManager()
    {
        SaveCurrentSymbol();
        var fmanager = new FFontManager(_openedFile);
        if (fmanager.ShowDialog(this) == DialogResult.OK)
        {
            _openedFile.Fonts.ResetBindings();
            var index = tscomboFonts.SelectedIndex;
            tscomboFonts.SelectedIndex = -1;
            tscomboFonts.SelectedIndex = index;
        }
    }

    private void ShowRenameFile()
    {
        SaveCurrentSymbol();
        var oldPath = _openedFile.PathName;
        var frename = new FRenameFile(_openedFile);
        if (frename.ShowDialog(this) == DialogResult.OK)
        {
            base.Text = Application.ProductName + @" - " + _openedFile.PathName;
            Saved = false;
            File.Delete(oldPath);
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

    private void InitGUI(bool opened)
    {
        tsmimSave.Enabled = opened;
        tsbSave.Enabled = opened;
        tsmimFontManager.Enabled = opened;
        tsbFontManager.Enabled = opened;
        tsmiEdit.Enabled = opened;
        tscomboFonts.Enabled = opened;
        tsmimRenameFile.Enabled = opened;
        tsbRenameFile.Enabled = opened;
    }

    private void tscomboFonts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tscomboFonts.SelectedIndex == -1)
            return;

        dgvSymbols.DataSource = _openedFile.Fonts[tscomboFonts.SelectedIndex].Symbols;
    }

    private void tsmiNew_Click(object sender, EventArgs e) => ShowNew();
    private void tsbNew_Click(object sender, EventArgs e) => ShowNew();
    private void tsmiOpen_Click(object sender, EventArgs e) => OpenFile();
    private void tsbOpen_Click(object sender, EventArgs e) => OpenFile();
    private void tsmiSave_Click(object sender, EventArgs e) => DoSave();
    private void tsbSave_Click(object sender, EventArgs e) => DoSave();
    private void tsmiFontManager_Click(object sender, EventArgs e) => ShowFontManager();
    private void tsbFontManager_Click(object sender, EventArgs e) => ShowFontManager();
    private void tsbRenameFile_Click(object sender, EventArgs e) => ShowRenameFile();
    private void tsmiRenameFile_Click(object sender, EventArgs e) => ShowRenameFile();
    private void tsmiAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();
    private void tsbAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();
    private void tsmiInfoApp_Click(object sender, EventArgs e) => ShowInfoApp();
    private void tsbInfoApp_Click(object sender, EventArgs e) => ShowInfoApp();

    private void FMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_openedFile == null || Saved) 
            return;

        var result = Utils.ShowQuestion(Resources.FMain_SaveChanges, MessageBoxButtons.YesNoCancel);
        switch (result)
        {
            case DialogResult.Yes:
                DoSave();
                e.Cancel = false;
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

        var cell = symbolEditor.GetCellFromPosition(e.X, e.Y);
        if (cell != null) 
            tsslPosition.Text = @$"{cell.Row}:{cell.Column}";
    }

    private void symbolEditor_SymbolChanged(object sender, EventArgs e) => Saved = false;

    private void FMain_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.V)
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