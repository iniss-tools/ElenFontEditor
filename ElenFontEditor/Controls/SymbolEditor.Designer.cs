namespace ElenFontEditor.Controls
{
    sealed partial class SymbolEditor
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

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiChangeState = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAll,
            this.toolStripSeparator2,
            this.tsmiChangeState,
            this.toolStripSeparator1,
            this.tsmiCopy,
            this.tsmiCut,
            this.tsmiPaste});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(195, 148);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
            this.tsmiSelectAll.Size = new System.Drawing.Size(194, 22);
            this.tsmiSelectAll.Text = "Označiť všetko";
            this.tsmiSelectAll.Click += new System.EventHandler(this.TsmiSelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiChangeState
            // 
            this.tsmiChangeState.Name = "tsmiChangeState";
            this.tsmiChangeState.ShortcutKeyDisplayString = "Enter";
            this.tsmiChangeState.Size = new System.Drawing.Size(194, 22);
            this.tsmiChangeState.Text = "Zmeniť stav";
            this.tsmiChangeState.Click += new System.EventHandler(this.TsmiChangeState_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmiCopy.Size = new System.Drawing.Size(194, 22);
            this.tsmiCopy.Text = "Kopírovať";
            this.tsmiCopy.Click += new System.EventHandler(this.TsmiCopy_Click);
            // 
            // tsmiCut
            // 
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.ShortcutKeyDisplayString = "Ctrl+X";
            this.tsmiCut.Size = new System.Drawing.Size(194, 22);
            this.tsmiCut.Text = "Vystrihnúť";
            this.tsmiCut.Click += new System.EventHandler(this.TsmiCut_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.ShortcutKeyDisplayString = "Ctrl+V";
            this.tsmiPaste.Size = new System.Drawing.Size(194, 22);
            this.tsmiPaste.Text = "Prilepiť";
            this.tsmiPaste.Click += new System.EventHandler(this.TsmiPaste_Click);
            // 
            // SymbolEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Name = "SymbolEditor";
            this.Size = new System.Drawing.Size(235, 184);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
