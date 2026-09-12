namespace TDSMAN.FormTrn
{
    partial class TrnDeducteeInvalidPANSummary
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrintData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDeducteePAN = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDeducteeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTAN = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFormNo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFAYear = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgcViewRecords);
            this.groupBox1.Location = new System.Drawing.Point(8, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 315);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deductee Invalid PAN Summary";
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
            this.dgcViewRecords.Location = new System.Drawing.Point(9, 19);
            this.dgcViewRecords.MultiSelect = false;
            this.dgcViewRecords.Name = "dgcViewRecords";
            this.dgcViewRecords.RowHeadersWidth = 20;
            this.dgcViewRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewRecords.Size = new System.Drawing.Size(718, 290);
            this.dgcViewRecords.TabIndex = 187;
            this.dgcViewRecords.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Lavender;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExit.Location = new System.Drawing.Point(638, 463);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 22);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrintData
            // 
            this.btnPrintData.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnPrintData.Location = new System.Drawing.Point(540, 463);
            this.btnPrintData.Name = "btnPrintData";
            this.btnPrintData.Size = new System.Drawing.Size(92, 22);
            this.btnPrintData.TabIndex = 18;
            this.btnPrintData.Text = "&Print";
            this.btnPrintData.UseVisualStyleBackColor = false;
            this.btnPrintData.Click += new System.EventHandler(this.btnPrintData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDeducteePAN);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtDeducteeName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(733, 60);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Filters";
            // 
            // txtDeducteePAN
            // 
            this.txtDeducteePAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN.Location = new System.Drawing.Point(523, 23);
            this.txtDeducteePAN.MaxLength = 10;
            this.txtDeducteePAN.Name = "txtDeducteePAN";
            this.txtDeducteePAN.Size = new System.Drawing.Size(122, 20);
            this.txtDeducteePAN.TabIndex = 22;
            this.txtDeducteePAN.TextChanged += new System.EventHandler(this.txtDeducteePAN_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(487, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "PAN";
            // 
            // txtDeducteeName
            // 
            this.txtDeducteeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeName.Location = new System.Drawing.Point(121, 23);
            this.txtDeducteeName.MaxLength = 75;
            this.txtDeducteeName.Name = "txtDeducteeName";
            this.txtDeducteeName.Size = new System.Drawing.Size(345, 20);
            this.txtDeducteeName.TabIndex = 20;
            this.txtDeducteeName.TextChanged += new System.EventHandler(this.txtDeducteeName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Deductee Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTAN);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblQuarter);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblFormNo);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lblFAYear);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lblCompanyName);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(8, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(733, 65);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Return Details";
            // 
            // lblTAN
            // 
            this.lblTAN.BackColor = System.Drawing.SystemColors.Control;
            this.lblTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTAN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTAN.Location = new System.Drawing.Point(53, 44);
            this.lblTAN.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTAN.Name = "lblTAN";
            this.lblTAN.Size = new System.Drawing.Size(140, 13);
            this.lblTAN.TabIndex = 19;
            this.lblTAN.Text = "lblTAN";
            this.lblTAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "TAN";
            // 
            // lblQuarter
            // 
            this.lblQuarter.BackColor = System.Drawing.SystemColors.Control;
            this.lblQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuarter.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblQuarter.Location = new System.Drawing.Point(456, 44);
            this.lblQuarter.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(52, 13);
            this.lblQuarter.TabIndex = 17;
            this.lblQuarter.Text = "lblQuarter";
            this.lblQuarter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(402, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Quarter";
            // 
            // lblFormNo
            // 
            this.lblFormNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormNo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFormNo.Location = new System.Drawing.Point(610, 44);
            this.lblFormNo.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblFormNo.Name = "lblFormNo";
            this.lblFormNo.Size = new System.Drawing.Size(65, 13);
            this.lblFormNo.TabIndex = 15;
            this.lblFormNo.Text = "lblFormNo";
            this.lblFormNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(543, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Form No.";
            // 
            // lblFAYear
            // 
            this.lblFAYear.BackColor = System.Drawing.SystemColors.Control;
            this.lblFAYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFAYear.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFAYear.Location = new System.Drawing.Point(299, 44);
            this.lblFAYear.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblFAYear.Name = "lblFAYear";
            this.lblFAYear.Size = new System.Drawing.Size(85, 13);
            this.lblFAYear.TabIndex = 13;
            this.lblFAYear.Text = "lblFAYear";
            this.lblFAYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(199, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tax Year";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.SystemColors.Control;
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCompanyName.Location = new System.Drawing.Point(110, 18);
            this.lblCompanyName.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(581, 13);
            this.lblCompanyName.TabIndex = 11;
            this.lblCompanyName.Text = "lblCompanyName";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Company Name";
            // 
            // TrnDeducteeInvalidPANSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 489);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPrintData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Name = "TrnDeducteeInvalidPANSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deductee Deduction";
            this.Load += new System.EventHandler(this.TrnDeducteeInvalidPANSummary_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DGVControl.DGVControl dgcViewRecords;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrintData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDeducteePAN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDeducteeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTAN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFormNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFAYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label label11;
    }
}