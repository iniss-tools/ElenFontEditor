namespace ElenFontEditor.Forms
{
    partial class FRenameFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRenameFile));
            this.bSave = new ExControls.ExButton();
            this.tbOldName = new ExControls.ExTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbNewName = new ExControls.ExTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bStorno = new ExControls.ExButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.bSave.Name = "bSave";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tbOldName
            // 
            this.tbOldName.BorderColor = System.Drawing.Color.DimGray;
            this.tbOldName.DisabledBackColor = System.Drawing.SystemColors.Control;
            this.tbOldName.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbOldName.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            resources.ApplyResources(this.tbOldName, "tbOldName");
            this.tbOldName.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.tbOldName.HintForeColor = System.Drawing.SystemColors.GrayText;
            this.tbOldName.HintText = null;
            this.tbOldName.Name = "tbOldName";
            this.tbOldName.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOldName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNewName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 2);
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            // 
            // tbNewName
            // 
            this.tbNewName.BorderColor = System.Drawing.Color.DimGray;
            this.tbNewName.DisabledBackColor = System.Drawing.SystemColors.Control;
            this.tbNewName.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbNewName.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            resources.ApplyResources(this.tbNewName, "tbNewName");
            this.tbNewName.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.tbNewName.HintForeColor = System.Drawing.SystemColors.GrayText;
            this.tbNewName.Name = "tbNewName";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.bStorno);
            this.flowLayoutPanel1.Controls.Add(this.bSave);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // bStorno
            // 
            this.bStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bStorno, "bStorno");
            this.bStorno.Name = "bStorno";
            this.bStorno.UseVisualStyleBackColor = true;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // FRenameFile
            // 
            this.AcceptButton = this.bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bStorno;
            this.ClientSize = new System.Drawing.Size(478, 102);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(494, 141);
            this.Name = "FRenameFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            resources.ApplyResources(this, "$this");
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExControls.ExButton bSave;
        private ExControls.ExTextBox tbOldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ExControls.ExTextBox tbNewName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ExControls.ExButton bStorno;
    }
}