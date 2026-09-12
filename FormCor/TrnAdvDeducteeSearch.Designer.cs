namespace TDSMAN.FormTrn
{
    partial class TrnAdvDeducteeSearch
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgcViewRecords = new DGVControl.DGVControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTAN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFormNo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFAYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkInvalidPAN = new System.Windows.Forms.CheckBox();
            this.txtDeducteePAN = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDeducteeName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnPrintAdvDeducteeSearch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgcViewRecords);
            this.groupBox1.Location = new System.Drawing.Point(22, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 281);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Results";
            // 
            // dgcViewRecords
            // 
            this.dgcViewRecords.AllowUserToAddRows = false;
            this.dgcViewRecords.AllowUserToDeleteRows = false;
            this.dgcViewRecords.AllowUserToOrderColumns = true;
            this.dgcViewRecords.AllowUserToResizeRows = false;
            this.dgcViewRecords.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewRecords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgcViewRecords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewRecords.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewRecords.Location = new System.Drawing.Point(13, 19);
            this.dgcViewRecords.MultiSelect = false;
            this.dgcViewRecords.Name = "dgcViewRecords";
            this.dgcViewRecords.RowHeadersWidth = 20;
            this.dgcViewRecords.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewRecords.Size = new System.Drawing.Size(681, 252);
            this.dgcViewRecords.TabIndex = 187;
            this.dgcViewRecords.TabStop = false;
            this.dgcViewRecords.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgcViewRecords_CellFormatting);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTAN);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblQuarter);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblFormNo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblFAYear);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblCompanyName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(22, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 65);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Return Details";
            // 
            // lblTAN
            // 
            this.lblTAN.BackColor = System.Drawing.SystemColors.Control;
            this.lblTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTAN.ForeColor = System.Drawing.Color.Blue;
            this.lblTAN.Location = new System.Drawing.Point(53, 44);
            this.lblTAN.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTAN.Name = "lblTAN";
            this.lblTAN.Size = new System.Drawing.Size(140, 13);
            this.lblTAN.TabIndex = 19;
            this.lblTAN.Text = "lblTAN";
            this.lblTAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "TAN";
            // 
            // lblQuarter
            // 
            this.lblQuarter.BackColor = System.Drawing.SystemColors.Control;
            this.lblQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuarter.ForeColor = System.Drawing.Color.Blue;
            this.lblQuarter.Location = new System.Drawing.Point(456, 44);
            this.lblQuarter.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(52, 13);
            this.lblQuarter.TabIndex = 17;
            this.lblQuarter.Text = "lblQuarter";
            this.lblQuarter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(402, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Quarter";
            // 
            // lblFormNo
            // 
            this.lblFormNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormNo.ForeColor = System.Drawing.Color.Blue;
            this.lblFormNo.Location = new System.Drawing.Point(610, 44);
            this.lblFormNo.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblFormNo.Name = "lblFormNo";
            this.lblFormNo.Size = new System.Drawing.Size(65, 13);
            this.lblFormNo.TabIndex = 15;
            this.lblFormNo.Text = "lblFormNo";
            this.lblFormNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(543, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Form No.";
            // 
            // lblFAYear
            // 
            this.lblFAYear.BackColor = System.Drawing.SystemColors.Control;
            this.lblFAYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFAYear.ForeColor = System.Drawing.Color.Blue;
            this.lblFAYear.Location = new System.Drawing.Point(299, 44);
            this.lblFAYear.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblFAYear.Name = "lblFAYear";
            this.lblFAYear.Size = new System.Drawing.Size(85, 13);
            this.lblFAYear.TabIndex = 13;
            this.lblFAYear.Text = "lblFAYear";
            this.lblFAYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(199, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tax Year";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.SystemColors.Control;
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Blue;
            this.lblCompanyName.Location = new System.Drawing.Point(110, 18);
            this.lblCompanyName.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(581, 13);
            this.lblCompanyName.TabIndex = 11;
            this.lblCompanyName.Text = "lblCompanyName";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Company Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkInvalidPAN);
            this.groupBox3.Controls.Add(this.txtDeducteePAN);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtDeducteeName);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cmbSection);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(22, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(704, 61);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Filters";
            // 
            // chkInvalidPAN
            // 
            this.chkInvalidPAN.AutoSize = true;
            this.chkInvalidPAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInvalidPAN.Location = new System.Drawing.Point(65, 40);
            this.chkInvalidPAN.Name = "chkInvalidPAN";
            this.chkInvalidPAN.Size = new System.Drawing.Size(93, 17);
            this.chkInvalidPAN.TabIndex = 23;
            this.chkInvalidPAN.Text = "Invalid PAN";
            this.chkInvalidPAN.UseVisualStyleBackColor = true;
            this.chkInvalidPAN.CheckedChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // txtDeducteePAN
            // 
            this.txtDeducteePAN.Location = new System.Drawing.Point(570, 16);
            this.txtDeducteePAN.MaxLength = 10;
            this.txtDeducteePAN.Name = "txtDeducteePAN";
            this.txtDeducteePAN.Size = new System.Drawing.Size(122, 20);
            this.txtDeducteePAN.TabIndex = 22;
            this.txtDeducteePAN.TextChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(534, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "PAN";
            // 
            // txtDeducteeName
            // 
            this.txtDeducteeName.Location = new System.Drawing.Point(241, 16);
            this.txtDeducteeName.MaxLength = 75;
            this.txtDeducteeName.Name = "txtDeducteeName";
            this.txtDeducteeName.Size = new System.Drawing.Size(288, 20);
            this.txtDeducteeName.TabIndex = 20;
            this.txtDeducteeName.TextChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(138, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Deductee Name";
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(65, 15);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(57, 21);
            this.cmbSection.TabIndex = 18;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Section";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Lavender;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExit.Location = new System.Drawing.Point(646, 441);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 22);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblRecords.Location = new System.Drawing.Point(35, 446);
            this.lblRecords.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 13);
            this.lblRecords.TabIndex = 14;
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintAdvDeducteeSearch
            // 
            this.btnPrintAdvDeducteeSearch.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintAdvDeducteeSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintAdvDeducteeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAdvDeducteeSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnPrintAdvDeducteeSearch.Location = new System.Drawing.Point(576, 441);
            this.btnPrintAdvDeducteeSearch.Name = "btnPrintAdvDeducteeSearch";
            this.btnPrintAdvDeducteeSearch.Size = new System.Drawing.Size(70, 22);
            this.btnPrintAdvDeducteeSearch.TabIndex = 15;
            this.btnPrintAdvDeducteeSearch.Text = "&Print";
            this.btnPrintAdvDeducteeSearch.UseVisualStyleBackColor = false;
            this.btnPrintAdvDeducteeSearch.Click += new System.EventHandler(this.btnPrintAdvDeducteeSearch_Click);
            // 
            // TrnAdvDeducteeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(749, 475);
            this.Controls.Add(this.btnPrintAdvDeducteeSearch);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnAdvDeducteeSearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Deductee Search";
            this.Load += new System.EventHandler(this.TrnAdvDeducteeSearch_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFormNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFAYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDeducteePAN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDeducteeName;
        private System.Windows.Forms.Label label11;
        private DGVControl.DGVControl dgcViewRecords;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkInvalidPAN;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnPrintAdvDeducteeSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTAN;
    }
}