namespace TDSMAN.FormTrn
{
    partial class TrnIncomeTaxCalculateSummary
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFinYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPAN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCategory1 = new System.Windows.Forms.Label();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNetTaxAmt = new System.Windows.Forms.Label();
            this.lblSumOfTaxAmt = new System.Windows.Forms.Label();
            this.lblGrossTotal = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalTaxableAmt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTaxCreditAmt = new System.Windows.Forms.Label();
            this.lblSurchargeAmt = new System.Windows.Forms.Label();
            this.lblTotalCessAmt = new System.Windows.Forms.Label();
            this.lblTotalTax = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgcViewRecords = new DGVControl.DGVControl();
            this.lblEduCessText = new System.Windows.Forms.Label();
            this.lblTaxCreditText = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFetchData = new System.Windows.Forms.Button();
            this.lnkTaxCalculationVisitSite = new System.Windows.Forms.LinkLabel();
            this.lblTaxation115BAC = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTaxation115BAC);
            this.groupBox2.Controls.Add(this.lblFinYear);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblPAN);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblCategory);
            this.groupBox2.Controls.Add(this.lblCategory1);
            this.groupBox2.Controls.Add(this.lblEmployeeName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 65);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee Details";
            // 
            // lblFinYear
            // 
            this.lblFinYear.BackColor = System.Drawing.SystemColors.Control;
            this.lblFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinYear.ForeColor = System.Drawing.Color.Blue;
            this.lblFinYear.Location = new System.Drawing.Point(446, 44);
            this.lblFinYear.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblFinYear.Name = "lblFinYear";
            this.lblFinYear.Size = new System.Drawing.Size(94, 13);
            this.lblFinYear.TabIndex = 21;
            this.lblFinYear.Text = "lblFinYear";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(354, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tax Year";
            // 
            // lblPAN
            // 
            this.lblPAN.BackColor = System.Drawing.SystemColors.Control;
            this.lblPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAN.ForeColor = System.Drawing.Color.Blue;
            this.lblPAN.Location = new System.Drawing.Point(46, 44);
            this.lblPAN.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblPAN.Name = "lblPAN";
            this.lblPAN.Size = new System.Drawing.Size(118, 13);
            this.lblPAN.TabIndex = 19;
            this.lblPAN.Text = "lblPAN";
            this.lblPAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "PAN";
            // 
            // lblCategory
            // 
            this.lblCategory.BackColor = System.Drawing.SystemColors.Control;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.Blue;
            this.lblCategory.Location = new System.Drawing.Point(226, 44);
            this.lblCategory.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(123, 13);
            this.lblCategory.TabIndex = 13;
            this.lblCategory.Text = "lblCategory";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCategory1
            // 
            this.lblCategory1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory1.Location = new System.Drawing.Point(163, 44);
            this.lblCategory1.Name = "lblCategory1";
            this.lblCategory1.Size = new System.Drawing.Size(65, 13);
            this.lblCategory1.TabIndex = 12;
            this.lblCategory1.Text = "Category";
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeName.ForeColor = System.Drawing.Color.Blue;
            this.lblEmployeeName.Location = new System.Drawing.Point(118, 18);
            this.lblEmployeeName.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(576, 13);
            this.lblEmployeeName.TabIndex = 11;
            this.lblEmployeeName.Text = "lblEmployeeName";
            this.lblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Employee Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblNetTaxAmt);
            this.groupBox1.Controls.Add(this.lblSumOfTaxAmt);
            this.groupBox1.Controls.Add(this.lblGrossTotal);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblTotalTaxableAmt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.lblTaxCreditAmt);
            this.groupBox1.Controls.Add(this.lblSurchargeAmt);
            this.groupBox1.Controls.Add(this.lblTotalCessAmt);
            this.groupBox1.Controls.Add(this.lblTotalTax);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dgcViewRecords);
            this.groupBox1.Controls.Add(this.lblEduCessText);
            this.groupBox1.Controls.Add(this.lblTaxCreditText);
            this.groupBox1.Location = new System.Drawing.Point(12, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 349);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tax Calculation Summary";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(235, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 13);
            this.label4.TabIndex = 200;
            this.label4.Text = "15. Tax Payable (12 + 13 + 14)";
            // 
            // lblNetTaxAmt
            // 
            this.lblNetTaxAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetTaxAmt.Location = new System.Drawing.Point(235, 217);
            this.lblNetTaxAmt.Name = "lblNetTaxAmt";
            this.lblNetTaxAmt.Size = new System.Drawing.Size(294, 13);
            this.lblNetTaxAmt.TabIndex = 199;
            this.lblNetTaxAmt.Text = "Sum of Tax Amount";
            // 
            // lblSumOfTaxAmt
            // 
            this.lblSumOfTaxAmt.BackColor = System.Drawing.SystemColors.Control;
            this.lblSumOfTaxAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumOfTaxAmt.ForeColor = System.Drawing.Color.Blue;
            this.lblSumOfTaxAmt.Location = new System.Drawing.Point(535, 217);
            this.lblSumOfTaxAmt.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblSumOfTaxAmt.Name = "lblSumOfTaxAmt";
            this.lblSumOfTaxAmt.Size = new System.Drawing.Size(153, 13);
            this.lblSumOfTaxAmt.TabIndex = 198;
            this.lblSumOfTaxAmt.Text = "lblSumOfTaxAmt";
            this.lblSumOfTaxAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrossTotal
            // 
            this.lblGrossTotal.BackColor = System.Drawing.SystemColors.Control;
            this.lblGrossTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblGrossTotal.Location = new System.Drawing.Point(535, 319);
            this.lblGrossTotal.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblGrossTotal.Name = "lblGrossTotal";
            this.lblGrossTotal.Size = new System.Drawing.Size(153, 13);
            this.lblGrossTotal.TabIndex = 196;
            this.lblGrossTotal.Text = "lblGrossTotal";
            this.lblGrossTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(530, 314);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 1);
            this.panel2.TabIndex = 195;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(530, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 1);
            this.panel1.TabIndex = 194;
            // 
            // lblTotalTaxableAmt
            // 
            this.lblTotalTaxableAmt.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalTaxableAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTaxableAmt.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalTaxableAmt.Location = new System.Drawing.Point(535, 32);
            this.lblTotalTaxableAmt.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTotalTaxableAmt.Name = "lblTotalTaxableAmt";
            this.lblTotalTaxableAmt.Size = new System.Drawing.Size(153, 13);
            this.lblTotalTaxableAmt.TabIndex = 193;
            this.lblTotalTaxableAmt.Text = "lblTotalTaxableAmt";
            this.lblTotalTaxableAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(235, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 13);
            this.label6.TabIndex = 192;
            this.label6.Text = "11. Total Taxable Income (8 - 10)";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(235, 280);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(294, 13);
            this.label19.TabIndex = 191;
            this.label19.Text = "13. Add:  Surcharge";
            // 
            // lblTaxCreditAmt
            // 
            this.lblTaxCreditAmt.BackColor = System.Drawing.SystemColors.Control;
            this.lblTaxCreditAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxCreditAmt.ForeColor = System.Drawing.Color.Blue;
            this.lblTaxCreditAmt.Location = new System.Drawing.Point(535, 239);
            this.lblTaxCreditAmt.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTaxCreditAmt.Name = "lblTaxCreditAmt";
            this.lblTaxCreditAmt.Size = new System.Drawing.Size(153, 13);
            this.lblTaxCreditAmt.TabIndex = 39;
            this.lblTaxCreditAmt.Text = "lblTaxCreditAmt";
            this.lblTaxCreditAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSurchargeAmt
            // 
            this.lblSurchargeAmt.BackColor = System.Drawing.SystemColors.Control;
            this.lblSurchargeAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurchargeAmt.ForeColor = System.Drawing.Color.Blue;
            this.lblSurchargeAmt.Location = new System.Drawing.Point(535, 280);
            this.lblSurchargeAmt.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblSurchargeAmt.Name = "lblSurchargeAmt";
            this.lblSurchargeAmt.Size = new System.Drawing.Size(153, 13);
            this.lblSurchargeAmt.TabIndex = 36;
            this.lblSurchargeAmt.Text = "lblSurchargeAmt";
            this.lblSurchargeAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCessAmt
            // 
            this.lblTotalCessAmt.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalCessAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCessAmt.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCessAmt.Location = new System.Drawing.Point(535, 298);
            this.lblTotalCessAmt.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTotalCessAmt.Name = "lblTotalCessAmt";
            this.lblTotalCessAmt.Size = new System.Drawing.Size(153, 13);
            this.lblTotalCessAmt.TabIndex = 41;
            this.lblTotalCessAmt.Text = "lblTotalCessAmt";
            this.lblTotalCessAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalTax
            // 
            this.lblTotalTax.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTax.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalTax.Location = new System.Drawing.Point(535, 261);
            this.lblTotalTax.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTotalTax.Name = "lblTotalTax";
            this.lblTotalTax.Size = new System.Drawing.Size(153, 13);
            this.lblTotalTax.TabIndex = 189;
            this.lblTotalTax.Text = "lblTotalTax";
            this.lblTotalTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(235, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 13);
            this.label8.TabIndex = 188;
            this.label8.Text = "12. Tax on Total Income ";
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
            this.dgcViewRecords.Location = new System.Drawing.Point(11, 48);
            this.dgcViewRecords.MultiSelect = false;
            this.dgcViewRecords.Name = "dgcViewRecords";
            this.dgcViewRecords.RowHeadersWidth = 20;
            this.dgcViewRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewRecords.Size = new System.Drawing.Size(681, 165);
            this.dgcViewRecords.TabIndex = 187;
            this.dgcViewRecords.TabStop = false;
            // 
            // lblEduCessText
            // 
            this.lblEduCessText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEduCessText.Location = new System.Drawing.Point(235, 298);
            this.lblEduCessText.Name = "lblEduCessText";
            this.lblEduCessText.Size = new System.Drawing.Size(294, 13);
            this.lblEduCessText.TabIndex = 20;
            this.lblEduCessText.Text = "14. Add: Education Cess @";
            // 
            // lblTaxCreditText
            // 
            this.lblTaxCreditText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxCreditText.Location = new System.Drawing.Point(235, 239);
            this.lblTaxCreditText.Name = "lblTaxCreditText";
            this.lblTaxCreditText.Size = new System.Drawing.Size(294, 13);
            this.lblTaxCreditText.TabIndex = 26;
            this.lblTaxCreditText.Text = "Less: Tax Credit @";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Lavender;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExit.Location = new System.Drawing.Point(619, 433);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 22);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnFetchData
            // 
            this.btnFetchData.BackColor = System.Drawing.Color.Lavender;
            this.btnFetchData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFetchData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnFetchData.Location = new System.Drawing.Point(444, 433);
            this.btnFetchData.Name = "btnFetchData";
            this.btnFetchData.Size = new System.Drawing.Size(169, 22);
            this.btnFetchData.TabIndex = 18;
            this.btnFetchData.Text = "&Fetch Data";
            this.btnFetchData.UseVisualStyleBackColor = false;
            this.btnFetchData.Click += new System.EventHandler(this.btnFetchData_Click);
            // 
            // lnkTaxCalculationVisitSite
            // 
            this.lnkTaxCalculationVisitSite.AutoSize = true;
            this.lnkTaxCalculationVisitSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTaxCalculationVisitSite.Location = new System.Drawing.Point(12, 438);
            this.lnkTaxCalculationVisitSite.Name = "lnkTaxCalculationVisitSite";
            this.lnkTaxCalculationVisitSite.Size = new System.Drawing.Size(358, 13);
            this.lnkTaxCalculationVisitSite.TabIndex = 255;
            this.lnkTaxCalculationVisitSite.TabStop = true;
            this.lnkTaxCalculationVisitSite.Text = "Click here to check the calculation from Income Tax Website.\r\n";
            this.lnkTaxCalculationVisitSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTaxCalculation_LinkClicked);
            // 
            // lblTaxation115BAC
            // 
            this.lblTaxation115BAC.BackColor = System.Drawing.SystemColors.Control;
            this.lblTaxation115BAC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxation115BAC.ForeColor = System.Drawing.Color.Blue;
            this.lblTaxation115BAC.Location = new System.Drawing.Point(557, 43);
            this.lblTaxation115BAC.MaximumSize = new System.Drawing.Size(604, 23);
            this.lblTaxation115BAC.Name = "lblTaxation115BAC";
            this.lblTaxation115BAC.Size = new System.Drawing.Size(139, 13);
            this.lblTaxation115BAC.TabIndex = 267;
            this.lblTaxation115BAC.Text = "Taxation u/s 115BAC";
            // 
            // TrnIncomeTaxCalculateSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 459);
            this.Controls.Add(this.lnkTaxCalculationVisitSite);
            this.Controls.Add(this.btnFetchData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "TrnIncomeTaxCalculateSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tax Calculate Summary";
            this.Load += new System.EventHandler(this.TrnIncomeTaxCalculateSummary_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblFinYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCategory1;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DGVControl.DGVControl dgcViewRecords;
        private System.Windows.Forms.Label lblTaxCreditText;
        private System.Windows.Forms.Label lblTaxCreditAmt;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTotalTaxableAmt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblSurchargeAmt;
        private System.Windows.Forms.Label lblTotalCessAmt;
        private System.Windows.Forms.Label lblTotalTax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEduCessText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGrossTotal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSumOfTaxAmt;
        private System.Windows.Forms.Label lblNetTaxAmt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFetchData;
        private System.Windows.Forms.LinkLabel lnkTaxCalculationVisitSite;
        private System.Windows.Forms.Label lblTaxation115BAC;
    }
}