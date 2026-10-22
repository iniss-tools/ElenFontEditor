using ElenFontEditor.Controls;

namespace ElenFontEditor.Forms
{
    partial class FMain
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.titleBar = new ExControls.ExTitleBar();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimNew = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmimOpen = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmimRecent = new ToolsCore.Icons.IconToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimSave = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimUndo = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmimRedo = new ToolsCore.Icons.IconToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimFontManager = new ToolsCore.Icons.IconToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimRenameFile = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimAppSettings = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimInfoApp = new ToolsCore.Icons.IconToolStripMenuItem();
            this.tsmimCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new ToolsCore.Icons.IconToolStripButton();
            this.tsbOpen = new ToolsCore.Icons.IconToolStripButton();
            this.tssbRecent = new ToolsCore.Icons.IconToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new ToolsCore.Icons.IconToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbUndo = new ToolsCore.Icons.IconToolStripSplitButton();
            this.tssbRedo = new ToolsCore.Icons.IconToolStripSplitButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAppSettings = new ToolsCore.Icons.IconToolStripButton();
            this.tsbInfoApp = new ToolsCore.Icons.IconToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRenameFile = new ToolsCore.Icons.IconToolStripButton();
            this.tsbFontManager = new ToolsCore.Icons.IconToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tslFont = new System.Windows.Forms.ToolStripLabel();
            this.tscomboFonts = new ExControls.ExToolStripComboBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslArray = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSymbols = new System.Windows.Forms.DataGridView();
            this.dialogOpenBinFile = new System.Windows.Forms.OpenFileDialog();
            this.dialogSaveNewFile = new System.Windows.Forms.SaveFileDialog();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSymbol = new System.Windows.Forms.DataGridViewImageColumn();
            this.cWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRowCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableSymbolBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.symbolEditor = new ElenFontEditor.Controls.SymbolEditor();
            this.titleBar.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableSymbolBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBar
            // 
            this.titleBar.Controls.Add(this.menuStripMain);
            resources.ApplyResources(this.titleBar, "titleBar");
            this.titleBar.Name = "titleBar";
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiSettings,
            this.tsmiHelp});
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimNew,
            this.tsmimOpen,
            this.tsmimRecent,
            this.toolStripSeparator2,
            this.tsmimSave});
            this.tsmiFile.Name = "tsmiFile";
            resources.ApplyResources(this.tsmiFile, "tsmiFile");
            // 
            // tsmimNew
            // 
            this.tsmimNew.Name = "tsmimNew";
            this.tsmimNew.IconName = "document";
            this.tsmimNew.IconBadge = "add";
            resources.ApplyResources(this.tsmimNew, "tsmimNew");
            // 
            // tsmimOpen
            // 
            this.tsmimOpen.Name = "tsmimOpen";
            this.tsmimOpen.IconName = "folder-open";
            resources.ApplyResources(this.tsmimOpen, "tsmimOpen");
            // 
            // tsmimRecent
            // 
            this.tsmimRecent.Name = "tsmimRecent";
            this.tsmimRecent.IconName = "history";
            resources.ApplyResources(this.tsmimRecent, "tsmimRecent");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsmimSave
            // 
            this.tsmimSave.Enabled = false;
            this.tsmimSave.Name = "tsmimSave";
            this.tsmimSave.IconName = "save";
            resources.ApplyResources(this.tsmimSave, "tsmimSave");
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimUndo,
            this.tsmimRedo,
            this.toolStripSeparator7,
            this.tsmimFontManager,
            this.toolStripSeparator6,
            this.tsmimRenameFile});
            this.tsmiEdit.Enabled = false;
            this.tsmiEdit.Name = "tsmiEdit";
            resources.ApplyResources(this.tsmiEdit, "tsmiEdit");
            //
            // tsmimUndo
            //
            this.tsmimUndo.Enabled = false;
            this.tsmimUndo.Name = "tsmimUndo";
            this.tsmimUndo.IconName = "undo";
            resources.ApplyResources(this.tsmimUndo, "tsmimUndo");
            //
            // tsmimRedo
            //
            this.tsmimRedo.Enabled = false;
            this.tsmimRedo.Name = "tsmimRedo";
            this.tsmimRedo.IconName = "redo";
            resources.ApplyResources(this.tsmimRedo, "tsmimRedo");
            //
            // toolStripSeparator7
            //
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            //
            // tsmimFontManager
            // 
            this.tsmimFontManager.Enabled = false;
            this.tsmimFontManager.Name = "tsmimFontManager";
            this.tsmimFontManager.IconName = "text-font";
            resources.ApplyResources(this.tsmimFontManager, "tsmimFontManager");
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tsmimRenameFile
            // 
            this.tsmimRenameFile.Enabled = false;
            this.tsmimRenameFile.Name = "tsmimRenameFile";
            this.tsmimRenameFile.IconName = "rename";
            resources.ApplyResources(this.tsmimRenameFile, "tsmimRenameFile");
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimAppSettings});
            this.tsmiSettings.Name = "tsmiSettings";
            resources.ApplyResources(this.tsmiSettings, "tsmiSettings");
            // 
            // tsmimAppSettings
            // 
            this.tsmimAppSettings.Name = "tsmimAppSettings";
            this.tsmimAppSettings.IconName = "settings";
            resources.ApplyResources(this.tsmimAppSettings, "tsmimAppSettings");
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimInfoApp,
            this.tsmimCheckUpdates});
            this.tsmiHelp.Name = "tsmiHelp";
            resources.ApplyResources(this.tsmiHelp, "tsmiHelp");
            // 
            // tsmimInfoApp
            // 
            this.tsmimInfoApp.Name = "tsmimInfoApp";
            this.tsmimInfoApp.IconName = "info";
            resources.ApplyResources(this.tsmimInfoApp, "tsmimInfoApp");
            //
            // tsmimCheckUpdates
            //
            this.tsmimCheckUpdates.Name = "tsmimCheckUpdates";
            resources.ApplyResources(this.tsmimCheckUpdates, "tsmimCheckUpdates");
            //
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tssbRecent,
            this.toolStripSeparator1,
            this.tsbSave,
            this.toolStripSeparator8,
            this.tssbUndo,
            this.tssbRedo,
            this.toolStripSeparator5,
            this.tsbAppSettings,
            this.tsbInfoApp,
            this.toolStripSeparator3,
            this.tsbRenameFile,
            this.tsbFontManager,
            this.toolStripSeparator4,
            this.tslFont,
            this.tscomboFonts});
            resources.ApplyResources(this.toolStripMain, "toolStripMain");
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.IconName = "document";
            this.tsbNew.IconBadge = "add";
            resources.ApplyResources(this.tsbNew, "tsbNew");
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.IconName = "folder-open";
            resources.ApplyResources(this.tsbOpen, "tsbOpen");
            // 
            // tssbRecent
            // 
            this.tssbRecent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbRecent.Name = "tssbRecent";
            this.tssbRecent.IconName = "history";
            resources.ApplyResources(this.tssbRecent, "tssbRecent");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.IconName = "save";
            resources.ApplyResources(this.tsbSave, "tsbSave");
            //
            // toolStripSeparator8
            //
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            //
            // tssbUndo
            //
            this.tssbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbUndo.Enabled = false;
            this.tssbUndo.Name = "tssbUndo";
            this.tssbUndo.IconName = "undo";
            resources.ApplyResources(this.tssbUndo, "tssbUndo");
            this.tssbUndo.DropDownOpening += new System.EventHandler(this.tssbUndo_DropDownOpening);
            //
            // tssbRedo
            //
            this.tssbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbRedo.Enabled = false;
            this.tssbRedo.Name = "tssbRedo";
            this.tssbRedo.IconName = "redo";
            resources.ApplyResources(this.tssbRedo, "tssbRedo");
            this.tssbRedo.DropDownOpening += new System.EventHandler(this.tssbRedo_DropDownOpening);
            //
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tsbAppSettings
            // 
            this.tsbAppSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAppSettings.Name = "tsbAppSettings";
            this.tsbAppSettings.IconName = "settings";
            resources.ApplyResources(this.tsbAppSettings, "tsbAppSettings");
            // 
            // tsbInfoApp
            // 
            this.tsbInfoApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInfoApp.Name = "tsbInfoApp";
            this.tsbInfoApp.IconName = "info";
            resources.ApplyResources(this.tsbInfoApp, "tsbInfoApp");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsbRenameFile
            // 
            this.tsbRenameFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRenameFile.Enabled = false;
            this.tsbRenameFile.Name = "tsbRenameFile";
            this.tsbRenameFile.IconName = "rename";
            resources.ApplyResources(this.tsbRenameFile, "tsbRenameFile");
            // 
            // tsbFontManager
            // 
            this.tsbFontManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFontManager.Enabled = false;
            this.tsbFontManager.Name = "tsbFontManager";
            this.tsbFontManager.IconName = "text-font";
            resources.ApplyResources(this.tsbFontManager, "tsbFontManager");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tslFont
            // 
            this.tslFont.Name = "tslFont";
            resources.ApplyResources(this.tslFont, "tslFont");
            // 
            // tscomboFonts
            // 
            this.tscomboFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscomboFonts.DropDownWidth = 150;
            this.tscomboFonts.Enabled = false;
            this.tscomboFonts.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tscomboFonts.Name = "tscomboFonts";
            resources.ApplyResources(this.tscomboFonts, "tscomboFonts");
            this.tscomboFonts.SelectedIndexChanged += new System.EventHandler(this.tscomboFonts_SelectedIndexChanged);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tsslArray,
            this.tsslPosition});
            resources.ApplyResources(this.statusStripMain, "statusStripMain");
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.SizingGrip = false;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.Spring = true;
            // 
            // tsslArray
            // 
            this.tsslArray.Name = "tsslArray";
            resources.ApplyResources(this.tsslArray, "tsslArray");
            // 
            // tsslPosition
            // 
            this.tsslPosition.Name = "tsslPosition";
            resources.ApplyResources(this.tsslPosition, "tsslPosition");
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSymbols);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.symbolEditor);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.splitContainer1.SplitterDistance = 296;
            // 
            // dgvSymbols
            // 
            this.dgvSymbols.AllowUserToAddRows = false;
            this.dgvSymbols.AllowUserToResizeRows = false;
            this.dgvSymbols.AutoGenerateColumns = false;
            this.dgvSymbols.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSymbols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cIndex,
            this.cName,
            this.cSymbol,
            this.cWidth,
            this.cRowCount,
            this.cColCount});
            this.dgvSymbols.DataSource = this.tableSymbolBindingSource;
            resources.ApplyResources(this.dgvSymbols, "dgvSymbols");
            this.dgvSymbols.MultiSelect = false;
            this.dgvSymbols.Name = "dgvSymbols";
            this.dgvSymbols.RowHeadersVisible = false;
            this.dgvSymbols.RowHeadersWidth = 51;
            this.dgvSymbols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSymbols.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSymbols_CellEndEdit);
            this.dgvSymbols.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSymbols_CellFormatting);
            this.dgvSymbols.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvSymbols_CellParsing);
            this.dgvSymbols.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvSymbols_CellValidating);
            this.dgvSymbols.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSymbols_CellValueChanged);
            this.dgvSymbols.SelectionChanged += new System.EventHandler(this.dgvSymbols_SelectionChanged);
            // 
            // dialogOpenBinFile
            // 
            resources.ApplyResources(this.dialogOpenBinFile, "dialogOpenBinFile");
            // 
            // dialogSaveNewFile
            // 
            this.dialogSaveNewFile.CreatePrompt = true;
            this.dialogSaveNewFile.DefaultExt = "bin";
            resources.ApplyResources(this.dialogSaveNewFile, "dialogSaveNewFile");
            this.dialogSaveNewFile.OverwritePrompt = false;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.DataPropertyName = "Index";
            resources.ApplyResources(this.cName, "cName");
            this.cName.MinimumWidth = 6;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cIndex
            // 
            this.cIndex.DataPropertyName = "Index";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.cIndex.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.cIndex, "cIndex");
            this.cIndex.MinimumWidth = 6;
            this.cIndex.Name = "cIndex";
            this.cIndex.ReadOnly = true;
            // 
            // cSymbol
            // 
            this.cSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSymbol.DataPropertyName = "Image";
            resources.ApplyResources(this.cSymbol, "cSymbol");
            this.cSymbol.MinimumWidth = 6;
            this.cSymbol.Name = "cSymbol";
            this.cSymbol.ReadOnly = true;
            // 
            // cWidth
            // 
            this.cWidth.DataPropertyName = "Width";
            resources.ApplyResources(this.cWidth, "cWidth");
            this.cWidth.MinimumWidth = 6;
            this.cWidth.Name = "cWidth";
            // 
            // cRowCount
            // 
            this.cRowCount.DataPropertyName = "Rows";
            resources.ApplyResources(this.cRowCount, "cRowCount");
            this.cRowCount.MinimumWidth = 6;
            this.cRowCount.Name = "cRowCount";
            this.cRowCount.ReadOnly = true;
            // 
            // cColCount
            // 
            this.cColCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cColCount.DataPropertyName = "Columns";
            resources.ApplyResources(this.cColCount, "cColCount");
            this.cColCount.MinimumWidth = 6;
            this.cColCount.Name = "cColCount";
            this.cColCount.ReadOnly = true;
            // 
            // tableSymbolBindingSource
            // 
            this.tableSymbolBindingSource.DataSource = typeof(ElenFontEditor.Entities.FontSymbol);
            // 
            // symbolEditor
            // 
            this.symbolEditor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.symbolEditor.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.symbolEditor, "symbolEditor");
            this.symbolEditor.HeaderBackColor = System.Drawing.Color.Gray;
            this.symbolEditor.HeaderForeColor = System.Drawing.Color.White;
            this.symbolEditor.Margin = new System.Windows.Forms.Padding(4);
            this.symbolEditor.Name = "symbolEditor";
            this.symbolEditor.PoweredOffColor = System.Drawing.Color.Black;
            this.symbolEditor.PoweredOnColor = System.Drawing.Color.Yellow;
            this.symbolEditor.SelectedColor = System.Drawing.Color.DodgerBlue;
            this.symbolEditor.SymbolChanged += new System.EventHandler(this.symbolEditor_SymbolChanged);
            this.symbolEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.symbolEditor_MouseMove);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 491);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.titleBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FMain";
            resources.ApplyResources(this, "$this");
            this.UseCompositedPainting = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableSymbolBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExControls.ExTitleBar titleBar;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimNew;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimOpen;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimRecent;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimInfoApp;
        private System.Windows.Forms.ToolStripMenuItem tsmimCheckUpdates;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private ToolsCore.Icons.IconToolStripButton tsbNew;
        private ToolsCore.Icons.IconToolStripButton tsbOpen;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimFontManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslFont;
        private ExControls.ExToolStripComboBox tscomboFonts;
        private ToolsCore.Icons.IconToolStripButton tsbFontManager;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSymbols;
        private System.Windows.Forms.ToolStripStatusLabel tsslArray;
        private System.Windows.Forms.ToolStripStatusLabel tsslPosition;
        private SymbolEditor symbolEditor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimSave;
        private ToolsCore.Icons.IconToolStripButton tsbAppSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimAppSettings;
        private ToolsCore.Icons.IconToolStripButton tsbInfoApp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ToolsCore.Icons.IconToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.OpenFileDialog dialogOpenBinFile;
        private System.Windows.Forms.BindingSource tableSymbolBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimRenameFile;
        private ToolsCore.Icons.IconToolStripButton tsbRenameFile;
        private ToolsCore.Icons.IconToolStripDropDownButton tssbRecent;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimUndo;
        private ToolsCore.Icons.IconToolStripMenuItem tsmimRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private ToolsCore.Icons.IconToolStripSplitButton tssbUndo;
        private ToolsCore.Icons.IconToolStripSplitButton tssbRedo;
        private System.Windows.Forms.SaveFileDialog dialogSaveNewFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewImageColumn cSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn cWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRowCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColCount;
    }
}

