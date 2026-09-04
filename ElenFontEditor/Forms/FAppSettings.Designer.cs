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
            this.exGroupBox2 = new ExControls.ExGroupBox();
            this.cboxStrictMode = new ExControls.ExCheckBox();
            this.pGeneral.SuspendLayout();
            this.pConcreteGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsView)).BeginInit();
            this.exGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConcreteGeneral
            // 
            this.pConcreteGeneral.Controls.Add(this.exGroupBox2);
            this.pConcreteGeneral.Size = new System.Drawing.Size(521, 270);
            // 
            // optionsView
            // 
            this.optionsView.HeaderNodeNameVisible = true;
            this.optionsView.SearchBoxVisible = true;
            this.optionsView.Size = new System.Drawing.Size(800, 440);
            // 
            // 
            // 
            this.optionsView.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsView.TreeView.FullRowSelect = true;
            this.optionsView.TreeView.HideSelection = false;
            this.optionsView.TreeView.ImageIndex = 0;
            this.optionsView.TreeView.ItemHeight = 20;
            this.optionsView.TreeView.Name = "treeView";
            this.optionsView.TreeView.PathSeparator = " / ";
            this.optionsView.TreeView.SelectedImageIndex = 0;
            this.optionsView.TreeView.ShowLines = false;
            this.optionsView.TreeView.ShowNodeToolTips = true;
            this.optionsView.TreeView.Style = ExControls.ExTreeViewStyle.Light;
            this.optionsView.TreeView.TabIndex = 0;
            // 
            // exGroupBox2
            // 
            this.exGroupBox2.AutoSize = true;
            this.exGroupBox2.Controls.Add(this.cboxStrictMode);
            this.exGroupBox2.Location = new System.Drawing.Point(0, 6);
            this.exGroupBox2.Name = "exGroupBox2";
            this.exGroupBox2.Size = new System.Drawing.Size(521, 56);
            this.exGroupBox2.TabIndex = 0;
            this.exGroupBox2.TabStop = false;
            this.exGroupBox2.Text = "Program";
            // 
            // cboxStrictMode
            // 
            this.cboxStrictMode.AutoSize = true;
            this.cboxStrictMode.BoxBackColor = System.Drawing.Color.White;
            this.cboxStrictMode.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxStrictMode.Location = new System.Drawing.Point(10, 20);
            this.cboxStrictMode.Name = "cboxStrictMode";
            this.cboxStrictMode.Size = new System.Drawing.Size(293, 17);
            this.cboxStrictMode.TabIndex = 0;
            this.cboxStrictMode.Text = "Pri načítaní súboru použiť striktný režim (väčšia kontrola)";
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
    }
}