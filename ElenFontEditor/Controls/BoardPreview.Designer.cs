namespace ElenFontEditor.Controls
{
    partial class BoardPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardPreview));
            this.tlpPreview = new System.Windows.Forms.TableLayoutPanel();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.lText = new System.Windows.Forms.Label();
            this.tbText = new ExControls.ExTextBox();
            this.lColor = new System.Windows.Forms.Label();
            this.cbColor = new ExControls.ExComboBox();
            this.cbTall = new ExControls.ExCheckBox();
            this.cbWidth = new ExControls.ExCheckBox();
            this.nudWidth = new ExControls.ExNumericUpDown();
            this.board = new ToolsCore.Forms.Elen.ElenBoardView();
            this.lInfo = new System.Windows.Forms.Label();
            this.tlpPreview.SuspendLayout();
            this.flpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpPreview
            // 
            this.tlpPreview.ColumnCount = 1;
            this.tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreview.Controls.Add(this.flpOptions, 0, 0);
            this.tlpPreview.Controls.Add(this.board, 0, 1);
            this.tlpPreview.Controls.Add(this.lInfo, 0, 2);
            resources.ApplyResources(this.tlpPreview, "tlpPreview");
            this.tlpPreview.Name = "tlpPreview";
            this.tlpPreview.RowCount = 3;
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            // 
            // flpOptions
            // 
            this.flpOptions.Controls.Add(this.lText);
            this.flpOptions.Controls.Add(this.tbText);
            this.flpOptions.Controls.Add(this.lColor);
            this.flpOptions.Controls.Add(this.cbColor);
            this.flpOptions.Controls.Add(this.cbTall);
            this.flpOptions.Controls.Add(this.cbWidth);
            this.flpOptions.Controls.Add(this.nudWidth);
            this.flpOptions.SetFlowBreak(this.tbText, true);
            resources.ApplyResources(this.flpOptions, "flpOptions");
            this.flpOptions.Name = "flpOptions";
            // 
            // lText
            // 
            resources.ApplyResources(this.lText, "lText");
            this.lText.Name = "lText";
            // 
            // tbText
            // 
            resources.ApplyResources(this.tbText, "tbText");
            this.tbText.Name = "tbText";
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // lColor
            // 
            resources.ApplyResources(this.lColor, "lColor");
            this.lColor.Name = "lColor";
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbColor, "cbColor");
            this.cbColor.Name = "cbColor";
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // cbTall
            // 
            resources.ApplyResources(this.cbTall, "cbTall");
            this.cbTall.Name = "cbTall";
            this.cbTall.CheckedChanged += new System.EventHandler(this.cbTall_CheckedChanged);
            // 
            // cbWidth
            // 
            resources.ApplyResources(this.cbWidth, "cbWidth");
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.CheckedChanged += new System.EventHandler(this.cbWidth_CheckedChanged);
            // 
            // nudWidth
            // 
            this.nudWidth.Maximum = new decimal(new int[] { 640, 0, 0, 0 });
            this.nudWidth.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            this.nudWidth.Value = new decimal(new int[] { 128, 0, 0, 0 });
            resources.ApplyResources(this.nudWidth, "nudWidth");
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // board
            // 
            resources.ApplyResources(this.board, "board");
            this.board.Name = "board";
            // 
            // lInfo
            // 
            resources.ApplyResources(this.lInfo, "lInfo");
            this.lInfo.Name = "lInfo";
            // 
            // BoardPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpPreview);
            this.Name = "BoardPreview";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.tlpPreview.ResumeLayout(false);
            this.tlpPreview.PerformLayout();
            this.flpOptions.ResumeLayout(false);
            this.flpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPreview;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
        private System.Windows.Forms.Label lText;
        private ExControls.ExTextBox tbText;
        private System.Windows.Forms.Label lColor;
        private ExControls.ExComboBox cbColor;
        private ExControls.ExCheckBox cbTall;
        private ExControls.ExCheckBox cbWidth;
        private ExControls.ExNumericUpDown nudWidth;
        private ToolsCore.Forms.Elen.ElenBoardView board;
        private System.Windows.Forms.Label lInfo;
    }
}
