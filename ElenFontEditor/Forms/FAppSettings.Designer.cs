namespace ElenFontEditor.Forms
{
    partial class FAppSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAppSettings));
            this.exGroupBox2 = new ExControls.ExGroupBox();
            this.cboxStrictMode = new ExControls.ExCheckBox();
            this.lCharEncoding = new System.Windows.Forms.Label();
            this.cbCharEncoding = new ExControls.ExComboBox();
            this.pGeneral.SuspendLayout();
            this.pConcreteGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsView)).BeginInit();
            this.exGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConcreteGeneral
            // 
            this.pConcreteGeneral.Controls.Add(this.exGroupBox2);
            resources.ApplyResources(this.pConcreteGeneral, "pConcreteGeneral");
            // 
            // optionsView
            // 
            this.optionsView.HeaderNodeNameVisible = true;
            this.optionsView.SearchBoxVisible = true;
            resources.ApplyResources(this.optionsView, "optionsView");
            // 
            // 
            // 
            this.optionsView.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsView.TreeView.FullRowSelect = true;
            this.optionsView.TreeView.HideSelection = false;
            this.optionsView.TreeView.ItemHeight = 20;
            this.optionsView.TreeView.Name = "treeView";
            this.optionsView.TreeView.PathSeparator = " / ";
            this.optionsView.TreeView.ShowLines = false;
            this.optionsView.TreeView.ShowNodeToolTips = true;
            this.optionsView.TreeView.Style = ExControls.ExTreeViewStyle.Light;
            this.optionsView.TreeView.TabIndex = 0;
            // 
            // exGroupBox2
            // 
            resources.ApplyResources(this.exGroupBox2, "exGroupBox2");
            this.exGroupBox2.Controls.Add(this.cbCharEncoding);
            this.exGroupBox2.Controls.Add(this.lCharEncoding);
            this.exGroupBox2.Controls.Add(this.cboxStrictMode);
            this.exGroupBox2.Name = "exGroupBox2";
            this.exGroupBox2.TabStop = false;
            // 
            // cboxStrictMode
            // 
            resources.ApplyResources(this.cboxStrictMode, "cboxStrictMode");
            this.cboxStrictMode.BoxBackColor = System.Drawing.Color.White;
            this.cboxStrictMode.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxStrictMode.Name = "cboxStrictMode";
            // 
            // lCharEncoding
            // 
            resources.ApplyResources(this.lCharEncoding, "lCharEncoding");
            this.lCharEncoding.Name = "lCharEncoding";
            // 
            // cbCharEncoding
            // 
            this.cbCharEncoding.DropDownSelectedRowBackColor = System.Drawing.SystemColors.Highlight;
            this.cbCharEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharEncoding.FormattingEnabled = true;
            resources.ApplyResources(this.cbCharEncoding, "cbCharEncoding");
            this.cbCharEncoding.Name = "cbCharEncoding";
            this.cbCharEncoding.StyleDisabled.ArrowColor = null;
            this.cbCharEncoding.StyleDisabled.BackColor = null;
            this.cbCharEncoding.StyleDisabled.BorderColor = null;
            this.cbCharEncoding.StyleDisabled.ButtonBackColor = null;
            this.cbCharEncoding.StyleDisabled.ButtonBorderColor = null;
            this.cbCharEncoding.StyleDisabled.ButtonRenderFirst = null;
            this.cbCharEncoding.StyleDisabled.ForeColor = null;
            this.cbCharEncoding.StyleHighlight.ArrowColor = null;
            this.cbCharEncoding.StyleHighlight.BackColor = null;
            this.cbCharEncoding.StyleHighlight.BorderColor = null;
            this.cbCharEncoding.StyleHighlight.ButtonBackColor = null;
            this.cbCharEncoding.StyleHighlight.ButtonBorderColor = null;
            this.cbCharEncoding.StyleHighlight.ButtonRenderFirst = null;
            this.cbCharEncoding.StyleHighlight.ForeColor = null;
            this.cbCharEncoding.StyleNormal.ArrowColor = null;
            this.cbCharEncoding.StyleNormal.BackColor = null;
            this.cbCharEncoding.StyleNormal.BorderColor = null;
            this.cbCharEncoding.StyleNormal.ButtonBackColor = null;
            this.cbCharEncoding.StyleNormal.ButtonBorderColor = null;
            this.cbCharEncoding.StyleNormal.ButtonRenderFirst = null;
            this.cbCharEncoding.StyleNormal.ForeColor = null;
            this.cbCharEncoding.StyleSelected.ArrowColor = null;
            this.cbCharEncoding.StyleSelected.BackColor = null;
            this.cbCharEncoding.StyleSelected.BorderColor = null;
            this.cbCharEncoding.StyleSelected.ButtonBackColor = null;
            this.cbCharEncoding.StyleSelected.ButtonBorderColor = null;
            this.cbCharEncoding.StyleSelected.ButtonRenderFirst = null;
            this.cbCharEncoding.StyleSelected.ForeColor = null;
            this.cbCharEncoding.UseDarkScrollBar = false;
            // 
            // FAppSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Name = "FAppSettings";
            this.pGeneral.ResumeLayout(false);
            this.pGeneral.PerformLayout();
            this.pConcreteGeneral.ResumeLayout(false);
            this.pConcreteGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsView)).EndInit();
            this.exGroupBox2.ResumeLayout(false);
            this.exGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ExControls.ExGroupBox exGroupBox2;
        private ExControls.ExCheckBox cboxStrictMode;
        private System.Windows.Forms.Label lCharEncoding;
        private ExControls.ExComboBox cbCharEncoding;
    }
}