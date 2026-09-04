namespace ElenFontEditor.Forms
{
    partial class FFontManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bSave = new ExControls.ExButton();
            this.bStorno = new ExControls.ExButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvFonts = new System.Windows.Forms.DataGridView();
            this.dgveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgveMaxWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgveHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgveIsProportional = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgveMatricies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableFontBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableFontBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(117, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(76, 27);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Uložiť";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bStorno
            // 
            this.bStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bStorno.Location = new System.Drawing.Point(199, 3);
            this.bStorno.Name = "bStorno";
            this.bStorno.Size = new System.Drawing.Size(81, 27);
            this.bStorno.TabIndex = 1;
            this.bStorno.Text = "Zrušiť";
            this.bStorno.UseVisualStyleBackColor = true;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvFonts, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.bStorno);
            this.flowLayoutPanel1.Controls.Add(this.bSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(291, 326);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(283, 33);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // dgvFonts
            // 
            this.dgvFonts.AutoGenerateColumns = false;
            this.dgvFonts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFonts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgveName,
            this.dgveMaxWidth,
            this.dgveHeight,
            this.dgveIsProportional,
            this.dgveMatricies});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvFonts, 2);
            this.dgvFonts.DataSource = this.tableFontBindingSource;
            this.dgvFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFonts.Location = new System.Drawing.Point(8, 25);
            this.dgvFonts.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.dgvFonts.Name = "dgvFonts";
            this.dgvFonts.RowHeadersWidth = 51;
            this.dgvFonts.Size = new System.Drawing.Size(561, 290);
            this.dgvFonts.TabIndex = 1;
            this.dgvFonts.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvFonts_CellValidating);
            this.dgvFonts.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvFonts_RowsAdded);
            this.dgvFonts.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvFonts_RowsRemoved);
            // 
            // dgveName
            // 
            this.dgveName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgveName.DataPropertyName = "Char";
            this.dgveName.HeaderText = "Názov";
            this.dgveName.MaxInputLength = 20;
            this.dgveName.MinimumWidth = 125;
            this.dgveName.Name = "dgveName";
            this.dgveName.Width = 125;
            // 
            // dgveMaxWidth
            // 
            this.dgveMaxWidth.DataPropertyName = "MaxWidth";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.dgveMaxWidth.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgveMaxWidth.HeaderText = "Maximálna šírka";
            this.dgveMaxWidth.MinimumWidth = 6;
            this.dgveMaxWidth.Name = "dgveMaxWidth";
            this.dgveMaxWidth.Width = 125;
            // 
            // dgveHeight
            // 
            this.dgveHeight.DataPropertyName = "Height";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.dgveHeight.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgveHeight.HeaderText = "Výška";
            this.dgveHeight.MinimumWidth = 6;
            this.dgveHeight.Name = "dgveHeight";
            this.dgveHeight.Width = 125;
            // 
            // dgveIsProportional
            // 
            this.dgveIsProportional.DataPropertyName = "IsProportional";
            this.dgveIsProportional.HeaderText = "Proporčné";
            this.dgveIsProportional.MinimumWidth = 6;
            this.dgveIsProportional.Name = "dgveIsProportional";
            this.dgveIsProportional.Width = 80;
            // 
            // dgveMatricies
            // 
            this.dgveMatricies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgveMatricies.DataPropertyName = "MatriciesCount";
            this.dgveMatricies.HeaderText = "Počet mriežok";
            this.dgveMatricies.MinimumWidth = 6;
            this.dgveMatricies.Name = "dgveMatricies";
            this.dgveMatricies.ReadOnly = true;
            // 
            // tableFontBindingSource
            // 
            this.tableFontBindingSource.DataSource = typeof(ElenFontEditor.Entities.TableFont);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Písma:";
            // 
            // FFontManager
            // 
            this.AcceptButton = this.bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bStorno;
            this.ClientSize = new System.Drawing.Size(577, 362);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FFontManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Správa písiem";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableFontBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ExControls.ExButton bSave;
        private ExControls.ExButton bStorno;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFonts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource tableFontBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgveName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgveMaxWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgveHeight;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgveIsProportional;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgveMatricies;
    }
}