namespace TDSMAN.FormSys
{
    partial class SysReCalculation
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
            this.lblFAYear = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.chkReserialising = new System.Windows.Forms.CheckBox();
            this.chkRecalculation = new System.Windows.Forms.CheckBox();
            this.ckhReIndexing = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblFAYear
            // 
            this.lblFAYear.AutoSize = true;
            this.lblFAYear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFAYear.Location = new System.Drawing.Point(99, 16);
            this.lblFAYear.Name = "lblFAYear";
            this.lblFAYear.Size = new System.Drawing.Size(87, 15);
            this.lblFAYear.TabIndex = 0;
            this.lblFAYear.Text = "Select FA Year";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(189, 12);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(166, 23);
            this.cmbFinancialYear.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(4, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 1);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(443, 1);
            this.panel2.TabIndex = 3;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnExit.Location = new System.Drawing.Point(226, 149);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.TabIndex = 11;
            this.BtnExit.Text = "&Close";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnSave.Location = new System.Drawing.Point(142, 149);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(83, 23);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&Start";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // chkReserialising
            // 
            this.chkReserialising.AutoSize = true;
            this.chkReserialising.Checked = true;
            this.chkReserialising.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReserialising.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReserialising.Location = new System.Drawing.Point(84, 55);
            this.chkReserialising.Name = "chkReserialising";
            this.chkReserialising.Size = new System.Drawing.Size(277, 19);
            this.chkReserialising.TabIndex = 12;
            this.chkReserialising.Text = "Re-Serialising challan and deductee records";
            this.chkReserialising.UseVisualStyleBackColor = true;
            // 
            // chkRecalculation
            // 
            this.chkRecalculation.AutoSize = true;
            this.chkRecalculation.Checked = true;
            this.chkRecalculation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecalculation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecalculation.Location = new System.Drawing.Point(84, 83);
            this.chkRecalculation.Name = "chkRecalculation";
            this.chkRecalculation.Size = new System.Drawing.Size(264, 19);
            this.chkRecalculation.TabIndex = 13;
            this.chkRecalculation.Text = "Re-Calculating deductee totals of challans";
            this.chkRecalculation.UseVisualStyleBackColor = true;
            // 
            // ckhReIndexing
            // 
            this.ckhReIndexing.AutoSize = true;
            this.ckhReIndexing.Checked = true;
            this.ckhReIndexing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckhReIndexing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckhReIndexing.Location = new System.Drawing.Point(84, 111);
            this.ckhReIndexing.Name = "ckhReIndexing";
            this.ckhReIndexing.Size = new System.Drawing.Size(194, 19);
            this.ckhReIndexing.TabIndex = 14;
            this.ckhReIndexing.Text = "Re-Indexing parameter tables";
            this.ckhReIndexing.UseVisualStyleBackColor = true;
            // 
            // SysReCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(451, 182);
            this.Controls.Add(this.ckhReIndexing);
            this.Controls.Add(this.chkRecalculation);
            this.Controls.Add(this.chkReserialising);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbFinancialYear);
            this.Controls.Add(this.lblFAYear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysReCalculation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SysReCalculation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFAYear;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button BtnExit;
        public System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.CheckBox chkReserialising;
        private System.Windows.Forms.CheckBox chkRecalculation;
        private System.Windows.Forms.CheckBox ckhReIndexing;
    }
}