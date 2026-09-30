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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimFontManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimRenameFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimAppSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimInfoApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tssbRecent = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAppSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbInfoApp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRenameFile = new System.Windows.Forms.ToolStripButton();
            this.tsbFontManager = new System.Windows.Forms.ToolStripButton();
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
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiSettings,
            this.tsmiHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(1007, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
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
            this.tsmiFile.Size = new System.Drawing.Size(50, 20);
            this.tsmiFile.Text = "&Súbor";
            // 
            // tsmimNew
            // 
            this.tsmimNew.Image = global::ToolsCore.GlobalResources.create;
            this.tsmimNew.Name = "tsmimNew";
            this.tsmimNew.Size = new System.Drawing.Size(121, 22);
            this.tsmimNew.Text = "&Nový...";
            // 
            // tsmimOpen
            // 
            this.tsmimOpen.Image = global::ToolsCore.GlobalResources.open;
            this.tsmimOpen.Name = "tsmimOpen";
            this.tsmimOpen.Size = new System.Drawing.Size(121, 22);
            this.tsmimOpen.Text = "&Otvoriť";
            // 
            // tsmimRecent
            // 
            this.tsmimRecent.Image = global::ToolsCore.GlobalResources.recent_gvds;
            this.tsmimRecent.Name = "tsmimRecent";
            this.tsmimRecent.Size = new System.Drawing.Size(121, 22);
            this.tsmimRecent.Text = "&Nedávne";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
            // 
            // tsmimSave
            // 
            this.tsmimSave.Enabled = false;
            this.tsmimSave.Image = global::ToolsCore.GlobalResources.save;
            this.tsmimSave.Name = "tsmimSave";
            this.tsmimSave.Size = new System.Drawing.Size(121, 22);
            this.tsmimSave.Text = "&Uložiť";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimFontManager,
            this.toolStripSeparator6,
            this.tsmimRenameFile});
            this.tsmiEdit.Enabled = false;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(58, 20);
            this.tsmiEdit.Text = "&Upraviť";
            // 
            // tsmimFontManager
            // 
            this.tsmimFontManager.Enabled = false;
            this.tsmimFontManager.Image = global::ToolsCore.GlobalResources.font;
            this.tsmimFontManager.Name = "tsmimFontManager";
            this.tsmimFontManager.Size = new System.Drawing.Size(181, 22);
            this.tsmimFontManager.Text = "&Správa písiem";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(178, 6);
            // 
            // tsmimRenameFile
            // 
            this.tsmimRenameFile.Enabled = false;
            this.tsmimRenameFile.Image = global::ToolsCore.GlobalResources.rename;
            this.tsmimRenameFile.Name = "tsmimRenameFile";
            this.tsmimRenameFile.Size = new System.Drawing.Size(181, 22);
            this.tsmimRenameFile.Text = "&Premenovať súbor...";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimAppSettings});
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(77, 20);
            this.tsmiSettings.Text = "&Nastavenia";
            // 
            // tsmimAppSettings
            // 
            this.tsmimAppSettings.Image = global::ToolsCore.GlobalResources.app_settings;
            this.tsmimAppSettings.Name = "tsmimAppSettings";
            this.tsmimAppSettings.Size = new System.Drawing.Size(197, 22);
            this.tsmimAppSettings.Text = "Nastavenia &programu...";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimInfoApp});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(73, 20);
            this.tsmiHelp.Text = "&Pomocník";
            // 
            // tsmimInfoApp
            // 
            this.tsmimInfoApp.Image = global::ToolsCore.GlobalResources.info_app;
            this.tsmimInfoApp.Name = "tsmimInfoApp";
            this.tsmimInfoApp.Size = new System.Drawing.Size(138, 22);
            this.tsmimInfoApp.Text = "&O programe";
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
            this.toolStripSeparator5,
            this.tsbAppSettings,
            this.tsbInfoApp,
            this.toolStripSeparator3,
            this.tsbRenameFile,
            this.tsbFontManager,
            this.toolStripSeparator4,
            this.tslFont,
            this.tscomboFonts});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStripMain.Size = new System.Drawing.Size(1007, 27);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStripMain";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::ToolsCore.GlobalResources.create;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(24, 24);
            this.tsbNew.Text = "Nový súbor...";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::ToolsCore.GlobalResources.open;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(24, 24);
            this.tsbOpen.Text = "Otvoriť";
            // 
            // tssbRecent
            // 
            this.tssbRecent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbRecent.Image = global::ToolsCore.GlobalResources.recent_gvds;
            this.tssbRecent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbRecent.Name = "tssbRecent";
            this.tssbRecent.Size = new System.Drawing.Size(33, 24);
            this.tssbRecent.Text = "Nedávne";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = global::ToolsCore.GlobalResources.save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 24);
            this.tsbSave.Text = "Uložiť";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbAppSettings
            // 
            this.tsbAppSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAppSettings.Image = global::ToolsCore.GlobalResources.app_settings;
            this.tsbAppSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAppSettings.Name = "tsbAppSettings";
            this.tsbAppSettings.Size = new System.Drawing.Size(24, 24);
            this.tsbAppSettings.Text = "Nastavenia programu";
            // 
            // tsbInfoApp
            // 
            this.tsbInfoApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInfoApp.Image = global::ToolsCore.GlobalResources.info_app;
            this.tsbInfoApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInfoApp.Name = "tsbInfoApp";
            this.tsbInfoApp.Size = new System.Drawing.Size(24, 24);
            this.tsbInfoApp.Text = "Informácie o programe";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbRenameFile
            // 
            this.tsbRenameFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRenameFile.Enabled = false;
            this.tsbRenameFile.Image = global::ToolsCore.GlobalResources.rename;
            this.tsbRenameFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRenameFile.Name = "tsbRenameFile";
            this.tsbRenameFile.Size = new System.Drawing.Size(24, 24);
            this.tsbRenameFile.Text = "Premenovať súbor";
            // 
            // tsbFontManager
            // 
            this.tsbFontManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFontManager.Enabled = false;
            this.tsbFontManager.Image = global::ToolsCore.GlobalResources.font;
            this.tsbFontManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFontManager.Name = "tsbFontManager";
            this.tsbFontManager.Size = new System.Drawing.Size(24, 24);
            this.tsbFontManager.Text = "Správa písiem";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tslFont
            // 
            this.tslFont.Name = "tslFont";
            this.tslFont.Size = new System.Drawing.Size(43, 24);
            this.tslFont.Text = "Písmo:";
            // 
            // tscomboFonts
            // 
            this.tscomboFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscomboFonts.DropDownWidth = 150;
            this.tscomboFonts.Enabled = false;
            this.tscomboFonts.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tscomboFonts.Name = "tscomboFonts";
            this.tscomboFonts.Size = new System.Drawing.Size(150, 24);
            this.tscomboFonts.SelectedIndexChanged += new System.EventHandler(this.tscomboFonts_SelectedIndexChanged);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tsslArray,
            this.tsslPosition});
            this.statusStripMain.Location = new System.Drawing.Point(0, 469);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1007, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(937, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // tsslArray
            // 
            this.tsslArray.Name = "tsslArray";
            this.tsslArray.Size = new System.Drawing.Size(33, 17);
            this.tsslArray.Text = "Pole:";
            // 
            // tsslPosition
            // 
            this.tsslPosition.Name = "tsslPosition";
            this.tsslPosition.Size = new System.Drawing.Size(22, 17);
            this.tsslPosition.Text = "0:0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
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
            this.splitContainer1.Size = new System.Drawing.Size(1007, 418);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 2;
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
            this.dgvSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSymbols.Location = new System.Drawing.Point(5, 5);
            this.dgvSymbols.MultiSelect = false;
            this.dgvSymbols.Name = "dgvSymbols";
            this.dgvSymbols.RowHeadersVisible = false;
            this.dgvSymbols.RowHeadersWidth = 51;
            this.dgvSymbols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSymbols.Size = new System.Drawing.Size(291, 408);
            this.dgvSymbols.TabIndex = 0;
            this.dgvSymbols.SelectionChanged += new System.EventHandler(this.dgvSymbols_SelectionChanged);
            // 
            // dialogOpenBinFile
            // 
            this.dialogOpenBinFile.Filter = "Bin. súbory|*.bin|All files|*.*";
            this.dialogOpenBinFile.Title = "Otvoriť binárny súbor";
            // 
            // dialogSaveNewFile
            // 
            this.dialogSaveNewFile.CreatePrompt = true;
            this.dialogSaveNewFile.DefaultExt = "bin";
            this.dialogSaveNewFile.Filter = "Bin. súbory|*.bin";
            this.dialogSaveNewFile.OverwritePrompt = false;
            this.dialogSaveNewFile.Title = "Zadajte názov nového súboru";
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.DataPropertyName = "Char";
            this.cName.HeaderText = "Znak";
            this.cName.MinimumWidth = 6;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 57;
            // 
            // cIndex
            // 
            this.cIndex.DataPropertyName = "Index";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.cIndex.DefaultCellStyle = dataGridViewCellStyle1;
            this.cIndex.HeaderText = "Index";
            this.cIndex.MinimumWidth = 6;
            this.cIndex.Name = "cIndex";
            this.cIndex.ReadOnly = true;
            this.cIndex.Width = 50;
            // 
            // cSymbol
            // 
            this.cSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSymbol.DataPropertyName = "Image";
            this.cSymbol.HeaderText = "Symbol";
            this.cSymbol.MinimumWidth = 6;
            this.cSymbol.Name = "cSymbol";
            this.cSymbol.ReadOnly = true;
            this.cSymbol.Width = 47;
            // 
            // cWidth
            // 
            this.cWidth.DataPropertyName = "Width";
            this.cWidth.HeaderText = "Šírka";
            this.cWidth.MinimumWidth = 6;
            this.cWidth.Name = "cWidth";
            this.cWidth.Width = 50;
            // 
            // cRowCount
            // 
            this.cRowCount.DataPropertyName = "Rows";
            this.cRowCount.HeaderText = "Počet riadkov";
            this.cRowCount.MinimumWidth = 6;
            this.cRowCount.Name = "cRowCount";
            this.cRowCount.ReadOnly = true;
            this.cRowCount.Width = 50;
            // 
            // cColCount
            // 
            this.cColCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cColCount.DataPropertyName = "Columns";
            this.cColCount.HeaderText = "Počet stĺpcov";
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
            this.symbolEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.symbolEditor.HeaderBackColor = System.Drawing.Color.Gray;
            this.symbolEditor.HeaderForeColor = System.Drawing.Color.White;
            this.symbolEditor.Location = new System.Drawing.Point(0, 5);
            this.symbolEditor.Margin = new System.Windows.Forms.Padding(4);
            this.symbolEditor.Name = "symbolEditor";
            this.symbolEditor.PoweredOffColor = System.Drawing.Color.Black;
            this.symbolEditor.PoweredOnColor = System.Drawing.Color.Yellow;
            this.symbolEditor.SelectedColor = System.Drawing.Color.DodgerBlue;
            this.symbolEditor.Size = new System.Drawing.Size(702, 408);
            this.symbolEditor.TabIndex = 0;
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
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FMain";
            this.Text = "ElenFontEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
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

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmimNew;
        private System.Windows.Forms.ToolStripMenuItem tsmimOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmimRecent;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmimInfoApp;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmimFontManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslFont;
        private ExControls.ExToolStripComboBox tscomboFonts;
        private System.Windows.Forms.ToolStripButton tsbFontManager;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSymbols;
        private System.Windows.Forms.ToolStripStatusLabel tsslArray;
        private System.Windows.Forms.ToolStripStatusLabel tsslPosition;
        private SymbolEditor symbolEditor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmimSave;
        private System.Windows.Forms.ToolStripButton tsbAppSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmimAppSettings;
        private System.Windows.Forms.ToolStripButton tsbInfoApp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.OpenFileDialog dialogOpenBinFile;
        private System.Windows.Forms.BindingSource tableSymbolBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmimRenameFile;
        private System.Windows.Forms.ToolStripButton tsbRenameFile;
        private System.Windows.Forms.ToolStripDropDownButton tssbRecent;
        private System.Windows.Forms.SaveFileDialog dialogSaveNewFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewImageColumn cSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn cWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRowCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColCount;
    }
}

