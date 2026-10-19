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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFontManager));
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
            resources.ApplyResources(this.bSave, "bSave");
            this.bSave.Name = "bSave";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bStorno
            // 
            this.bStorno.CausesValidation = false;
            this.bStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bStorno, "bStorno");
            this.bStorno.Name = "bStorno";
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
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.bStorno);
            this.flowLayoutPanel1.Controls.Add(this.bSave);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
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
            resources.ApplyResources(this.dgvFonts, "dgvFonts");
            this.dgvFonts.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.dgvFonts.Name = "dgvFonts";
            this.dgvFonts.RowHeadersWidth = 51;
            this.dgvFonts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFonts_CellEndEdit);
            this.dgvFonts.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvFonts_CellParsing);
            this.dgvFonts.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvFonts_CellValidating);
            this.dgvFonts.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvFonts_RowsAdded);
            this.dgvFonts.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvFonts_RowsRemoved);
            // 
            // dgveName
            // 
            this.dgveName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgveName.DataPropertyName = "Name";
            resources.ApplyResources(this.dgveName, "dgveName");
            this.dgveName.MaxInputLength = 20;
            this.dgveName.MinimumWidth = 125;
            this.dgveName.Name = "dgveName";
            // 
            // dgveMaxWidth
            // 
            this.dgveMaxWidth.DataPropertyName = "MaxWidth";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.dgveMaxWidth.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dgveMaxWidth, "dgveMaxWidth");
            this.dgveMaxWidth.MinimumWidth = 6;
            this.dgveMaxWidth.Name = "dgveMaxWidth";
            // 
            // dgveHeight
            // 
            this.dgveHeight.DataPropertyName = "Height";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.dgveHeight.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dgveHeight, "dgveHeight");
            this.dgveHeight.MinimumWidth = 6;
            this.dgveHeight.Name = "dgveHeight";
            // 
            // dgveIsProportional
            // 
            this.dgveIsProportional.DataPropertyName = "IsProportional";
            resources.ApplyResources(this.dgveIsProportional, "dgveIsProportional");
            this.dgveIsProportional.MinimumWidth = 6;
            this.dgveIsProportional.Name = "dgveIsProportional";
            // 
            // dgveMatricies
            // 
            this.dgveMatricies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgveMatricies.DataPropertyName = "MatriciesCount";
            resources.ApplyResources(this.dgveMatricies, "dgveMatricies");
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
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            resources.ApplyResources(this, "$this");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FFontManager_FormClosing);
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