namespace ElenFontEditor.Forms
{
    partial class FImportSymbols
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FImportSymbols));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pOptions = new System.Windows.Forms.Panel();
            this.tlpOptions = new System.Windows.Forms.TableLayoutPanel();
            this.gbFont = new ExControls.ExGroupBox();
            this.tlpFont = new System.Windows.Forms.TableLayoutPanel();
            this.rbInstalled = new ExControls.ExRadioButton();
            this.cbFamily = new ExControls.ExComboBox();
            this.rbFontFile = new ExControls.ExRadioButton();
            this.tbFontFile = new ExControls.ExTextBox();
            this.bFontFile = new ExControls.ExButton();
            this.lStyle = new System.Windows.Forms.Label();
            this.flpStyle = new System.Windows.Forms.FlowLayoutPanel();
            this.cbBold = new ExControls.ExCheckBox();
            this.cbItalic = new ExControls.ExCheckBox();
            this.lSize = new System.Windows.Forms.Label();
            this.nudSize = new ExControls.ExNumericUpDown();
            this.lBaseline = new System.Windows.Forms.Label();
            this.nudBaseline = new ExControls.ExNumericUpDown();
            this.bFontDefaults = new ExControls.ExButton();
            this.lRender = new System.Windows.Forms.Label();
            this.cbRender = new ExControls.ExComboBox();
            this.lFontThreshold = new System.Windows.Forms.Label();
            this.nudFontThreshold = new ExControls.ExNumericUpDown();
            this.gbImage = new ExControls.ExGroupBox();
            this.tlpImage = new System.Windows.Forms.TableLayoutPanel();
            this.lImageFile = new System.Windows.Forms.Label();
            this.tbImageFile = new ExControls.ExTextBox();
            this.bImageFile = new ExControls.ExButton();
            this.rbSingle = new ExControls.ExRadioButton();
            this.rbGrid = new ExControls.ExRadioButton();
            this.lScale = new System.Windows.Forms.Label();
            this.nudScale = new ExControls.ExNumericUpDown();
            this.lColumns = new System.Windows.Forms.Label();
            this.nudColumns = new ExControls.ExNumericUpDown();
            this.lCellWidth = new System.Windows.Forms.Label();
            this.nudCellWidth = new ExControls.ExNumericUpDown();
            this.lCellHeight = new System.Windows.Forms.Label();
            this.nudCellHeight = new ExControls.ExNumericUpDown();
            this.lMarginLeft = new System.Windows.Forms.Label();
            this.nudMarginLeft = new ExControls.ExNumericUpDown();
            this.lMarginTop = new System.Windows.Forms.Label();
            this.nudMarginTop = new ExControls.ExNumericUpDown();
            this.lGapX = new System.Windows.Forms.Label();
            this.nudGapX = new ExControls.ExNumericUpDown();
            this.lGapY = new System.Windows.Forms.Label();
            this.nudGapY = new ExControls.ExNumericUpDown();
            this.lFirstCode = new System.Windows.Forms.Label();
            this.nudFirstCode = new ExControls.ExNumericUpDown();
            this.lImageThreshold = new System.Windows.Forms.Label();
            this.nudImageThreshold = new ExControls.ExNumericUpDown();
            this.cbDarkInk = new ExControls.ExCheckBox();
            this.gbRange = new ExControls.ExGroupBox();
            this.tlpRange = new System.Windows.Forms.TableLayoutPanel();
            this.lFrom = new System.Windows.Forms.Label();
            this.nudFrom = new ExControls.ExNumericUpDown();
            this.lTo = new System.Windows.Forms.Label();
            this.nudTo = new ExControls.ExNumericUpDown();
            this.lRangeInfo = new System.Windows.Forms.Label();
            this.gbTarget = new ExControls.ExGroupBox();
            this.tlpTarget = new System.Windows.Forms.TableLayoutPanel();
            this.rbNewFont = new ExControls.ExRadioButton();
            this.tbNewName = new ExControls.ExTextBox();
            this.lNewSize = new System.Windows.Forms.Label();
            this.nudNewWidth = new ExControls.ExNumericUpDown();
            this.lNewX = new System.Windows.Forms.Label();
            this.nudNewHeight = new ExControls.ExNumericUpDown();
            this.cbNewProportional = new ExControls.ExCheckBox();
            this.rbExisting = new ExControls.ExRadioButton();
            this.cbTargetFont = new ExControls.ExComboBox();
            this.cbOnlyEmpty = new ExControls.ExCheckBox();
            this.gbWidth = new ExControls.ExGroupBox();
            this.tlpWidth = new System.Windows.Forms.TableLayoutPanel();
            this.lSpacing = new System.Windows.Forms.Label();
            this.nudSpacing = new ExControls.ExNumericUpDown();
            this.lIndent = new System.Windows.Forms.Label();
            this.nudIndent = new ExControls.ExNumericUpDown();
            this.cbCenter = new ExControls.ExCheckBox();
            this.tlpResult = new System.Windows.Forms.TableLayoutPanel();
            this.lResult = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.lSample = new System.Windows.Forms.Label();
            this.tbSample = new ExControls.ExTextBox();
            this.boardSample = new ToolsCore.Forms.Elen.ElenBoardView();
            this.lSummary = new System.Windows.Forms.Label();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.bCancel = new ExControls.ExButton();
            this.bImport = new ExControls.ExButton();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain.SuspendLayout();
            this.pOptions.SuspendLayout();
            this.tlpOptions.SuspendLayout();
            this.gbFont.SuspendLayout();
            this.tlpFont.SuspendLayout();
            this.flpStyle.SuspendLayout();
            this.gbImage.SuspendLayout();
            this.tlpImage.SuspendLayout();
            this.gbRange.SuspendLayout();
            this.tlpRange.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.tlpTarget.SuspendLayout();
            this.gbWidth.SuspendLayout();
            this.tlpWidth.SuspendLayout();
            this.tlpResult.SuspendLayout();
            this.flpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGapX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGapY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pOptions, 0, 0);
            this.tlpMain.Controls.Add(this.tlpResult, 1, 0);
            this.tlpMain.Controls.Add(this.flpButtons, 0, 1);
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.SetColumnSpan(this.flpButtons, 2);
            // 
            // pOptions
            // 
            this.pOptions.Controls.Add(this.tlpOptions);
            this.pOptions.AutoScroll = true;
            resources.ApplyResources(this.pOptions, "pOptions");
            this.pOptions.Name = "pOptions";
            // 
            // tlpOptions
            // 
            this.tlpOptions.ColumnCount = 1;
            this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOptions.Controls.Add(this.gbFont, 0, 0);
            this.tlpOptions.Controls.Add(this.gbImage, 0, 1);
            this.tlpOptions.Controls.Add(this.gbRange, 0, 2);
            this.tlpOptions.Controls.Add(this.gbTarget, 0, 3);
            this.tlpOptions.Controls.Add(this.gbWidth, 0, 4);
            resources.ApplyResources(this.tlpOptions, "tlpOptions");
            this.tlpOptions.Name = "tlpOptions";
            this.tlpOptions.RowCount = 5;
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            // 
            // gbFont
            // 
            this.gbFont.Controls.Add(this.tlpFont);
            resources.ApplyResources(this.gbFont, "gbFont");
            this.gbFont.Name = "gbFont";
            // 
            // tlpFont
            // 
            this.tlpFont.ColumnCount = 3;
            this.tlpFont.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFont.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFont.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFont.Controls.Add(this.rbInstalled, 0, 0);
            this.tlpFont.Controls.Add(this.cbFamily, 1, 0);
            this.tlpFont.Controls.Add(this.rbFontFile, 0, 1);
            this.tlpFont.Controls.Add(this.tbFontFile, 1, 1);
            this.tlpFont.Controls.Add(this.bFontFile, 2, 1);
            this.tlpFont.Controls.Add(this.lStyle, 0, 2);
            this.tlpFont.Controls.Add(this.flpStyle, 1, 2);
            this.tlpFont.Controls.Add(this.lSize, 0, 3);
            this.tlpFont.Controls.Add(this.nudSize, 1, 3);
            this.tlpFont.Controls.Add(this.lBaseline, 0, 4);
            this.tlpFont.Controls.Add(this.nudBaseline, 1, 4);
            this.tlpFont.Controls.Add(this.bFontDefaults, 1, 5);
            this.tlpFont.Controls.Add(this.lRender, 0, 6);
            this.tlpFont.Controls.Add(this.cbRender, 1, 6);
            this.tlpFont.Controls.Add(this.lFontThreshold, 0, 7);
            this.tlpFont.Controls.Add(this.nudFontThreshold, 1, 7);
            resources.ApplyResources(this.tlpFont, "tlpFont");
            this.tlpFont.Name = "tlpFont";
            this.tlpFont.RowCount = 8;
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFont.SetColumnSpan(this.cbFamily, 2);
            this.tlpFont.SetColumnSpan(this.flpStyle, 2);
            this.tlpFont.SetColumnSpan(this.bFontDefaults, 2);
            this.tlpFont.SetColumnSpan(this.cbRender, 2);
            // 
            // rbInstalled
            // 
            resources.ApplyResources(this.rbInstalled, "rbInstalled");
            this.rbInstalled.Name = "rbInstalled";
            this.rbInstalled.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // cbFamily
            // 
            this.cbFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbFamily, "cbFamily");
            this.cbFamily.Name = "cbFamily";
            this.cbFamily.SelectedIndexChanged += new System.EventHandler(this.Font_Changed);
            // 
            // rbFontFile
            // 
            resources.ApplyResources(this.rbFontFile, "rbFontFile");
            this.rbFontFile.Name = "rbFontFile";
            this.rbFontFile.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // tbFontFile
            // 
            this.tbFontFile.ReadOnly = true;
            resources.ApplyResources(this.tbFontFile, "tbFontFile");
            this.tbFontFile.Name = "tbFontFile";
            // 
            // bFontFile
            // 
            resources.ApplyResources(this.bFontFile, "bFontFile");
            this.bFontFile.Name = "bFontFile";
            this.bFontFile.Click += new System.EventHandler(this.bFontFile_Click);
            // 
            // lStyle
            // 
            resources.ApplyResources(this.lStyle, "lStyle");
            this.lStyle.Name = "lStyle";
            // 
            // flpStyle
            // 
            this.flpStyle.Controls.Add(this.cbBold);
            this.flpStyle.Controls.Add(this.cbItalic);
            resources.ApplyResources(this.flpStyle, "flpStyle");
            this.flpStyle.Name = "flpStyle";
            // 
            // cbBold
            // 
            resources.ApplyResources(this.cbBold, "cbBold");
            this.cbBold.Name = "cbBold";
            this.cbBold.CheckedChanged += new System.EventHandler(this.Font_Changed);
            // 
            // cbItalic
            // 
            resources.ApplyResources(this.cbItalic, "cbItalic");
            this.cbItalic.Name = "cbItalic";
            this.cbItalic.CheckedChanged += new System.EventHandler(this.Font_Changed);
            // 
            // lSize
            // 
            resources.ApplyResources(this.lSize, "lSize");
            this.lSize.Name = "lSize";
            // 
            // nudSize
            // 
            this.nudSize.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            this.nudSize.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            this.nudSize.Value = new decimal(new int[] { 16, 0, 0, 0 });
            resources.ApplyResources(this.nudSize, "nudSize");
            this.nudSize.Name = "nudSize";
            this.nudSize.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lBaseline
            // 
            resources.ApplyResources(this.lBaseline, "lBaseline");
            this.lBaseline.Name = "lBaseline";
            // 
            // nudBaseline
            // 
            this.nudBaseline.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudBaseline.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudBaseline.Value = new decimal(new int[] { 13, 0, 0, 0 });
            resources.ApplyResources(this.nudBaseline, "nudBaseline");
            this.nudBaseline.Name = "nudBaseline";
            this.toolTip.SetToolTip(this.nudBaseline, resources.GetString("nudBaseline.ToolTip"));
            this.nudBaseline.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // bFontDefaults
            // 
            resources.ApplyResources(this.bFontDefaults, "bFontDefaults");
            this.bFontDefaults.Name = "bFontDefaults";
            this.toolTip.SetToolTip(this.bFontDefaults, resources.GetString("bFontDefaults.ToolTip"));
            this.bFontDefaults.Click += new System.EventHandler(this.bFontDefaults_Click);
            // 
            // lRender
            // 
            resources.ApplyResources(this.lRender, "lRender");
            this.lRender.Name = "lRender";
            // 
            // cbRender
            // 
            this.cbRender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbRender, "cbRender");
            this.cbRender.Name = "cbRender";
            this.cbRender.SelectedIndexChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lFontThreshold
            // 
            resources.ApplyResources(this.lFontThreshold, "lFontThreshold");
            this.lFontThreshold.Name = "lFontThreshold";
            // 
            // nudFontThreshold
            // 
            this.nudFontThreshold.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudFontThreshold.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudFontThreshold.Value = new decimal(new int[] { 50, 0, 0, 0 });
            resources.ApplyResources(this.nudFontThreshold, "nudFontThreshold");
            this.nudFontThreshold.Name = "nudFontThreshold";
            this.toolTip.SetToolTip(this.nudFontThreshold, resources.GetString("nudFontThreshold.ToolTip"));
            this.nudFontThreshold.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.tlpImage);
            resources.ApplyResources(this.gbImage, "gbImage");
            this.gbImage.Name = "gbImage";
            // 
            // tlpImage
            // 
            this.tlpImage.ColumnCount = 5;
            this.tlpImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpImage.Controls.Add(this.lImageFile, 0, 0);
            this.tlpImage.Controls.Add(this.tbImageFile, 1, 0);
            this.tlpImage.Controls.Add(this.bImageFile, 1, 1);
            this.tlpImage.Controls.Add(this.rbSingle, 0, 2);
            this.tlpImage.Controls.Add(this.rbGrid, 2, 2);
            this.tlpImage.Controls.Add(this.lScale, 0, 3);
            this.tlpImage.Controls.Add(this.nudScale, 1, 3);
            this.tlpImage.Controls.Add(this.lColumns, 2, 3);
            this.tlpImage.Controls.Add(this.nudColumns, 3, 3);
            this.tlpImage.Controls.Add(this.lCellWidth, 0, 4);
            this.tlpImage.Controls.Add(this.nudCellWidth, 1, 4);
            this.tlpImage.Controls.Add(this.lCellHeight, 2, 4);
            this.tlpImage.Controls.Add(this.nudCellHeight, 3, 4);
            this.tlpImage.Controls.Add(this.lMarginLeft, 0, 5);
            this.tlpImage.Controls.Add(this.nudMarginLeft, 1, 5);
            this.tlpImage.Controls.Add(this.lMarginTop, 2, 5);
            this.tlpImage.Controls.Add(this.nudMarginTop, 3, 5);
            this.tlpImage.Controls.Add(this.lGapX, 0, 6);
            this.tlpImage.Controls.Add(this.nudGapX, 1, 6);
            this.tlpImage.Controls.Add(this.lGapY, 2, 6);
            this.tlpImage.Controls.Add(this.nudGapY, 3, 6);
            this.tlpImage.Controls.Add(this.lFirstCode, 0, 7);
            this.tlpImage.Controls.Add(this.nudFirstCode, 1, 7);
            this.tlpImage.Controls.Add(this.lImageThreshold, 0, 8);
            this.tlpImage.Controls.Add(this.nudImageThreshold, 1, 8);
            this.tlpImage.Controls.Add(this.cbDarkInk, 2, 8);
            resources.ApplyResources(this.tlpImage, "tlpImage");
            this.tlpImage.Name = "tlpImage";
            this.tlpImage.RowCount = 9;
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpImage.SetColumnSpan(this.tbImageFile, 4);
            this.tlpImage.SetColumnSpan(this.bImageFile, 4);
            this.tlpImage.SetColumnSpan(this.rbSingle, 2);
            this.tlpImage.SetColumnSpan(this.rbGrid, 3);
            this.tlpImage.SetColumnSpan(this.cbDarkInk, 3);
            // 
            // lImageFile
            // 
            resources.ApplyResources(this.lImageFile, "lImageFile");
            this.lImageFile.Name = "lImageFile";
            // 
            // tbImageFile
            // 
            this.tbImageFile.ReadOnly = true;
            resources.ApplyResources(this.tbImageFile, "tbImageFile");
            this.tbImageFile.Name = "tbImageFile";
            // 
            // bImageFile
            // 
            resources.ApplyResources(this.bImageFile, "bImageFile");
            this.bImageFile.Name = "bImageFile";
            this.bImageFile.Click += new System.EventHandler(this.bImageFile_Click);
            // 
            // rbSingle
            // 
            resources.ApplyResources(this.rbSingle, "rbSingle");
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // rbGrid
            // 
            resources.ApplyResources(this.rbGrid, "rbGrid");
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lScale
            // 
            resources.ApplyResources(this.lScale, "lScale");
            this.lScale.Name = "lScale";
            // 
            // nudScale
            // 
            this.nudScale.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            this.nudScale.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            resources.ApplyResources(this.nudScale, "nudScale");
            this.nudScale.Name = "nudScale";
            this.nudScale.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lColumns
            // 
            resources.ApplyResources(this.lColumns, "lColumns");
            this.lColumns.Name = "lColumns";
            // 
            // nudColumns
            // 
            this.nudColumns.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            this.nudColumns.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudColumns.Value = new decimal(new int[] { 16, 0, 0, 0 });
            resources.ApplyResources(this.nudColumns, "nudColumns");
            this.nudColumns.Name = "nudColumns";
            this.nudColumns.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lCellWidth
            // 
            resources.ApplyResources(this.lCellWidth, "lCellWidth");
            this.lCellWidth.Name = "lCellWidth";
            // 
            // nudCellWidth
            // 
            this.nudCellWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            this.nudCellWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCellWidth.Value = new decimal(new int[] { 8, 0, 0, 0 });
            resources.ApplyResources(this.nudCellWidth, "nudCellWidth");
            this.nudCellWidth.Name = "nudCellWidth";
            this.nudCellWidth.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lCellHeight
            // 
            resources.ApplyResources(this.lCellHeight, "lCellHeight");
            this.lCellHeight.Name = "lCellHeight";
            // 
            // nudCellHeight
            // 
            this.nudCellHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            this.nudCellHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCellHeight.Value = new decimal(new int[] { 16, 0, 0, 0 });
            resources.ApplyResources(this.nudCellHeight, "nudCellHeight");
            this.nudCellHeight.Name = "nudCellHeight";
            this.nudCellHeight.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lMarginLeft
            // 
            resources.ApplyResources(this.lMarginLeft, "lMarginLeft");
            this.lMarginLeft.Name = "lMarginLeft";
            // 
            // nudMarginLeft
            // 
            this.nudMarginLeft.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            this.nudMarginLeft.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudMarginLeft.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudMarginLeft, "nudMarginLeft");
            this.nudMarginLeft.Name = "nudMarginLeft";
            this.nudMarginLeft.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lMarginTop
            // 
            resources.ApplyResources(this.lMarginTop, "lMarginTop");
            this.lMarginTop.Name = "lMarginTop";
            // 
            // nudMarginTop
            // 
            this.nudMarginTop.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            this.nudMarginTop.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudMarginTop.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudMarginTop, "nudMarginTop");
            this.nudMarginTop.Name = "nudMarginTop";
            this.nudMarginTop.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lGapX
            // 
            resources.ApplyResources(this.lGapX, "lGapX");
            this.lGapX.Name = "lGapX";
            // 
            // nudGapX
            // 
            this.nudGapX.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            this.nudGapX.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudGapX.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudGapX, "nudGapX");
            this.nudGapX.Name = "nudGapX";
            this.nudGapX.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lGapY
            // 
            resources.ApplyResources(this.lGapY, "lGapY");
            this.lGapY.Name = "lGapY";
            // 
            // nudGapY
            // 
            this.nudGapY.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            this.nudGapY.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudGapY.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudGapY, "nudGapY");
            this.nudGapY.Name = "nudGapY";
            this.nudGapY.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lFirstCode
            // 
            resources.ApplyResources(this.lFirstCode, "lFirstCode");
            this.lFirstCode.Name = "lFirstCode";
            // 
            // nudFirstCode
            // 
            this.nudFirstCode.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            this.nudFirstCode.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudFirstCode.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudFirstCode, "nudFirstCode");
            this.nudFirstCode.Name = "nudFirstCode";
            this.nudFirstCode.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lImageThreshold
            // 
            resources.ApplyResources(this.lImageThreshold, "lImageThreshold");
            this.lImageThreshold.Name = "lImageThreshold";
            // 
            // nudImageThreshold
            // 
            this.nudImageThreshold.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudImageThreshold.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudImageThreshold.Value = new decimal(new int[] { 50, 0, 0, 0 });
            resources.ApplyResources(this.nudImageThreshold, "nudImageThreshold");
            this.nudImageThreshold.Name = "nudImageThreshold";
            this.toolTip.SetToolTip(this.nudImageThreshold, resources.GetString("nudImageThreshold.ToolTip"));
            this.nudImageThreshold.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // cbDarkInk
            // 
            resources.ApplyResources(this.cbDarkInk, "cbDarkInk");
            this.cbDarkInk.Name = "cbDarkInk";
            this.cbDarkInk.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // gbRange
            // 
            this.gbRange.Controls.Add(this.tlpRange);
            resources.ApplyResources(this.gbRange, "gbRange");
            this.gbRange.Name = "gbRange";
            // 
            // tlpRange
            // 
            this.tlpRange.ColumnCount = 4;
            this.tlpRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRange.Controls.Add(this.lFrom, 0, 0);
            this.tlpRange.Controls.Add(this.nudFrom, 1, 0);
            this.tlpRange.Controls.Add(this.lTo, 2, 0);
            this.tlpRange.Controls.Add(this.nudTo, 3, 0);
            this.tlpRange.Controls.Add(this.lRangeInfo, 0, 1);
            resources.ApplyResources(this.tlpRange, "tlpRange");
            this.tlpRange.Name = "tlpRange";
            this.tlpRange.RowCount = 2;
            this.tlpRange.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRange.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRange.SetColumnSpan(this.lRangeInfo, 4);
            // 
            // lFrom
            // 
            resources.ApplyResources(this.lFrom, "lFrom");
            this.lFrom.Name = "lFrom";
            // 
            // nudFrom
            // 
            this.nudFrom.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            this.nudFrom.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudFrom.Value = new decimal(new int[] { 32, 0, 0, 0 });
            resources.ApplyResources(this.nudFrom, "nudFrom");
            this.nudFrom.Name = "nudFrom";
            this.nudFrom.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lTo
            // 
            resources.ApplyResources(this.lTo, "lTo");
            this.lTo.Name = "lTo";
            // 
            // nudTo
            // 
            this.nudTo.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            this.nudTo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudTo.Value = new decimal(new int[] { 255, 0, 0, 0 });
            resources.ApplyResources(this.nudTo, "nudTo");
            this.nudTo.Name = "nudTo";
            this.nudTo.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lRangeInfo
            // 
            resources.ApplyResources(this.lRangeInfo, "lRangeInfo");
            this.lRangeInfo.Name = "lRangeInfo";
            // 
            // gbTarget
            // 
            this.gbTarget.Controls.Add(this.tlpTarget);
            resources.ApplyResources(this.gbTarget, "gbTarget");
            this.gbTarget.Name = "gbTarget";
            // 
            // tlpTarget
            // 
            this.tlpTarget.ColumnCount = 4;
            this.tlpTarget.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTarget.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTarget.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTarget.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTarget.Controls.Add(this.rbNewFont, 0, 0);
            this.tlpTarget.Controls.Add(this.tbNewName, 1, 0);
            this.tlpTarget.Controls.Add(this.lNewSize, 0, 1);
            this.tlpTarget.Controls.Add(this.nudNewWidth, 1, 1);
            this.tlpTarget.Controls.Add(this.lNewX, 2, 1);
            this.tlpTarget.Controls.Add(this.nudNewHeight, 3, 1);
            this.tlpTarget.Controls.Add(this.cbNewProportional, 1, 2);
            this.tlpTarget.Controls.Add(this.rbExisting, 0, 3);
            this.tlpTarget.Controls.Add(this.cbTargetFont, 1, 3);
            this.tlpTarget.Controls.Add(this.cbOnlyEmpty, 1, 4);
            resources.ApplyResources(this.tlpTarget, "tlpTarget");
            this.tlpTarget.Name = "tlpTarget";
            this.tlpTarget.RowCount = 5;
            this.tlpTarget.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTarget.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTarget.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTarget.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTarget.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTarget.SetColumnSpan(this.tbNewName, 3);
            this.tlpTarget.SetColumnSpan(this.cbNewProportional, 3);
            this.tlpTarget.SetColumnSpan(this.cbTargetFont, 3);
            this.tlpTarget.SetColumnSpan(this.cbOnlyEmpty, 3);
            // 
            // rbNewFont
            // 
            resources.ApplyResources(this.rbNewFont, "rbNewFont");
            this.rbNewFont.Name = "rbNewFont";
            this.rbNewFont.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // tbNewName
            // 
            resources.ApplyResources(this.tbNewName, "tbNewName");
            this.tbNewName.Name = "tbNewName";
            this.tbNewName.TextChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lNewSize
            // 
            resources.ApplyResources(this.lNewSize, "lNewSize");
            this.lNewSize.Name = "lNewSize";
            // 
            // nudNewWidth
            // 
            this.nudNewWidth.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            this.nudNewWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudNewWidth.Value = new decimal(new int[] { 8, 0, 0, 0 });
            resources.ApplyResources(this.nudNewWidth, "nudNewWidth");
            this.nudNewWidth.Name = "nudNewWidth";
            this.nudNewWidth.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lNewX
            // 
            resources.ApplyResources(this.lNewX, "lNewX");
            this.lNewX.Name = "lNewX";
            // 
            // nudNewHeight
            // 
            this.nudNewHeight.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudNewHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudNewHeight.Value = new decimal(new int[] { 16, 0, 0, 0 });
            resources.ApplyResources(this.nudNewHeight, "nudNewHeight");
            this.nudNewHeight.Name = "nudNewHeight";
            this.nudNewHeight.ValueChanged += new System.EventHandler(this.NewHeight_Changed);
            // 
            // cbNewProportional
            // 
            resources.ApplyResources(this.cbNewProportional, "cbNewProportional");
            this.cbNewProportional.Name = "cbNewProportional";
            this.cbNewProportional.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // rbExisting
            // 
            resources.ApplyResources(this.rbExisting, "rbExisting");
            this.rbExisting.Name = "rbExisting";
            this.rbExisting.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // cbTargetFont
            // 
            this.cbTargetFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbTargetFont, "cbTargetFont");
            this.cbTargetFont.Name = "cbTargetFont";
            this.cbTargetFont.SelectedIndexChanged += new System.EventHandler(this.Option_Changed);
            // 
            // cbOnlyEmpty
            // 
            resources.ApplyResources(this.cbOnlyEmpty, "cbOnlyEmpty");
            this.cbOnlyEmpty.Name = "cbOnlyEmpty";
            this.cbOnlyEmpty.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // gbWidth
            // 
            this.gbWidth.Controls.Add(this.tlpWidth);
            resources.ApplyResources(this.gbWidth, "gbWidth");
            this.gbWidth.Name = "gbWidth";
            // 
            // tlpWidth
            // 
            this.tlpWidth.ColumnCount = 2;
            this.tlpWidth.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpWidth.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWidth.Controls.Add(this.lSpacing, 0, 0);
            this.tlpWidth.Controls.Add(this.nudSpacing, 1, 0);
            this.tlpWidth.Controls.Add(this.lIndent, 0, 1);
            this.tlpWidth.Controls.Add(this.nudIndent, 1, 1);
            this.tlpWidth.Controls.Add(this.cbCenter, 0, 2);
            resources.ApplyResources(this.tlpWidth, "tlpWidth");
            this.tlpWidth.Name = "tlpWidth";
            this.tlpWidth.RowCount = 3;
            this.tlpWidth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWidth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWidth.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWidth.SetColumnSpan(this.cbCenter, 2);
            // 
            // lSpacing
            // 
            resources.ApplyResources(this.lSpacing, "lSpacing");
            this.lSpacing.Name = "lSpacing";
            // 
            // nudSpacing
            // 
            this.nudSpacing.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            this.nudSpacing.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudSpacing.Value = new decimal(new int[] { 1, 0, 0, 0 });
            resources.ApplyResources(this.nudSpacing, "nudSpacing");
            this.nudSpacing.Name = "nudSpacing";
            this.nudSpacing.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // lIndent
            // 
            resources.ApplyResources(this.lIndent, "lIndent");
            this.lIndent.Name = "lIndent";
            // 
            // nudIndent
            // 
            this.nudIndent.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            this.nudIndent.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudIndent.Value = new decimal(new int[] { 0, 0, 0, 0 });
            resources.ApplyResources(this.nudIndent, "nudIndent");
            this.nudIndent.Name = "nudIndent";
            this.nudIndent.ValueChanged += new System.EventHandler(this.Option_Changed);
            // 
            // cbCenter
            // 
            resources.ApplyResources(this.cbCenter, "cbCenter");
            this.cbCenter.Name = "cbCenter";
            this.cbCenter.CheckedChanged += new System.EventHandler(this.Option_Changed);
            // 
            // tlpResult
            // 
            this.tlpResult.ColumnCount = 2;
            this.tlpResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResult.Controls.Add(this.lResult, 0, 0);
            this.tlpResult.Controls.Add(this.dgvResult, 0, 1);
            this.tlpResult.Controls.Add(this.lSample, 0, 2);
            this.tlpResult.Controls.Add(this.tbSample, 1, 2);
            this.tlpResult.Controls.Add(this.boardSample, 0, 3);
            this.tlpResult.Controls.Add(this.lSummary, 0, 4);
            resources.ApplyResources(this.tlpResult, "tlpResult");
            this.tlpResult.Name = "tlpResult";
            this.tlpResult.RowCount = 5;
            this.tlpResult.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResult.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpResult.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpResult.SetColumnSpan(this.lResult, 2);
            this.tlpResult.SetColumnSpan(this.dgvResult, 2);
            this.tlpResult.SetColumnSpan(this.boardSample, 2);
            this.tlpResult.SetColumnSpan(this.lSummary, 2);
            // 
            // lResult
            // 
            resources.ApplyResources(this.lResult, "lResult");
            this.lResult.Name = "lResult";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            resources.ApplyResources(this.dgvResult, "dgvResult");
            this.dgvResult.Name = "dgvResult";
            // 
            // lSample
            // 
            resources.ApplyResources(this.lSample, "lSample");
            this.lSample.Name = "lSample";
            // 
            // tbSample
            // 
            resources.ApplyResources(this.tbSample, "tbSample");
            this.tbSample.Name = "tbSample";
            this.tbSample.TextChanged += new System.EventHandler(this.tbSample_TextChanged);
            // 
            // boardSample
            // 
            resources.ApplyResources(this.boardSample, "boardSample");
            this.boardSample.Name = "boardSample";
            // 
            // lSummary
            // 
            resources.ApplyResources(this.lSummary, "lSummary");
            this.lSummary.Name = "lSummary";
            // 
            // flpButtons
            // 
            this.flpButtons.Controls.Add(this.bCancel);
            this.flpButtons.Controls.Add(this.bImport);
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            resources.ApplyResources(this.flpButtons, "flpButtons");
            this.flpButtons.Name = "flpButtons";
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.Name = "bCancel";
            // 
            // bImport
            // 
            resources.ApplyResources(this.bImport, "bImport");
            this.bImport.Name = "bImport";
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 200;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // toolTip
            // 
            // 
            // FImportSymbols
            // 
            this.AcceptButton = this.bImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Controls.Add(this.tlpMain);
            this.Name = "FImportSymbols";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGapX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGapY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pOptions.ResumeLayout(false);
            this.pOptions.PerformLayout();
            this.tlpOptions.ResumeLayout(false);
            this.tlpOptions.PerformLayout();
            this.gbFont.ResumeLayout(false);
            this.gbFont.PerformLayout();
            this.tlpFont.ResumeLayout(false);
            this.tlpFont.PerformLayout();
            this.flpStyle.ResumeLayout(false);
            this.flpStyle.PerformLayout();
            this.gbImage.ResumeLayout(false);
            this.gbImage.PerformLayout();
            this.tlpImage.ResumeLayout(false);
            this.tlpImage.PerformLayout();
            this.gbRange.ResumeLayout(false);
            this.gbRange.PerformLayout();
            this.tlpRange.ResumeLayout(false);
            this.tlpRange.PerformLayout();
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.tlpTarget.ResumeLayout(false);
            this.tlpTarget.PerformLayout();
            this.gbWidth.ResumeLayout(false);
            this.gbWidth.PerformLayout();
            this.tlpWidth.ResumeLayout(false);
            this.tlpWidth.PerformLayout();
            this.tlpResult.ResumeLayout(false);
            this.tlpResult.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.flpButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pOptions;
        private System.Windows.Forms.TableLayoutPanel tlpOptions;
        private ExControls.ExGroupBox gbFont;
        private System.Windows.Forms.TableLayoutPanel tlpFont;
        private ExControls.ExRadioButton rbInstalled;
        private ExControls.ExComboBox cbFamily;
        private ExControls.ExRadioButton rbFontFile;
        private ExControls.ExTextBox tbFontFile;
        private ExControls.ExButton bFontFile;
        private System.Windows.Forms.Label lStyle;
        private System.Windows.Forms.FlowLayoutPanel flpStyle;
        private ExControls.ExCheckBox cbBold;
        private ExControls.ExCheckBox cbItalic;
        private System.Windows.Forms.Label lSize;
        private ExControls.ExNumericUpDown nudSize;
        private System.Windows.Forms.Label lBaseline;
        private ExControls.ExNumericUpDown nudBaseline;
        private ExControls.ExButton bFontDefaults;
        private System.Windows.Forms.Label lRender;
        private ExControls.ExComboBox cbRender;
        private System.Windows.Forms.Label lFontThreshold;
        private ExControls.ExNumericUpDown nudFontThreshold;
        private ExControls.ExGroupBox gbImage;
        private System.Windows.Forms.TableLayoutPanel tlpImage;
        private System.Windows.Forms.Label lImageFile;
        private ExControls.ExTextBox tbImageFile;
        private ExControls.ExButton bImageFile;
        private ExControls.ExRadioButton rbSingle;
        private ExControls.ExRadioButton rbGrid;
        private System.Windows.Forms.Label lScale;
        private ExControls.ExNumericUpDown nudScale;
        private System.Windows.Forms.Label lColumns;
        private ExControls.ExNumericUpDown nudColumns;
        private System.Windows.Forms.Label lCellWidth;
        private ExControls.ExNumericUpDown nudCellWidth;
        private System.Windows.Forms.Label lCellHeight;
        private ExControls.ExNumericUpDown nudCellHeight;
        private System.Windows.Forms.Label lMarginLeft;
        private ExControls.ExNumericUpDown nudMarginLeft;
        private System.Windows.Forms.Label lMarginTop;
        private ExControls.ExNumericUpDown nudMarginTop;
        private System.Windows.Forms.Label lGapX;
        private ExControls.ExNumericUpDown nudGapX;
        private System.Windows.Forms.Label lGapY;
        private ExControls.ExNumericUpDown nudGapY;
        private System.Windows.Forms.Label lFirstCode;
        private ExControls.ExNumericUpDown nudFirstCode;
        private System.Windows.Forms.Label lImageThreshold;
        private ExControls.ExNumericUpDown nudImageThreshold;
        private ExControls.ExCheckBox cbDarkInk;
        private ExControls.ExGroupBox gbRange;
        private System.Windows.Forms.TableLayoutPanel tlpRange;
        private System.Windows.Forms.Label lFrom;
        private ExControls.ExNumericUpDown nudFrom;
        private System.Windows.Forms.Label lTo;
        private ExControls.ExNumericUpDown nudTo;
        private System.Windows.Forms.Label lRangeInfo;
        private ExControls.ExGroupBox gbTarget;
        private System.Windows.Forms.TableLayoutPanel tlpTarget;
        private ExControls.ExRadioButton rbNewFont;
        private ExControls.ExTextBox tbNewName;
        private System.Windows.Forms.Label lNewSize;
        private ExControls.ExNumericUpDown nudNewWidth;
        private System.Windows.Forms.Label lNewX;
        private ExControls.ExNumericUpDown nudNewHeight;
        private ExControls.ExCheckBox cbNewProportional;
        private ExControls.ExRadioButton rbExisting;
        private ExControls.ExComboBox cbTargetFont;
        private ExControls.ExCheckBox cbOnlyEmpty;
        private ExControls.ExGroupBox gbWidth;
        private System.Windows.Forms.TableLayoutPanel tlpWidth;
        private System.Windows.Forms.Label lSpacing;
        private ExControls.ExNumericUpDown nudSpacing;
        private System.Windows.Forms.Label lIndent;
        private ExControls.ExNumericUpDown nudIndent;
        private ExControls.ExCheckBox cbCenter;
        private System.Windows.Forms.TableLayoutPanel tlpResult;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label lSample;
        private ExControls.ExTextBox tbSample;
        private ToolsCore.Forms.Elen.ElenBoardView boardSample;
        private System.Windows.Forms.Label lSummary;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private ExControls.ExButton bCancel;
        private ExControls.ExButton bImport;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
