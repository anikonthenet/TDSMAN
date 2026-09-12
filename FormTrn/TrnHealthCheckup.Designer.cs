namespace TDSMAN.FormTrn
{
    partial class TrnHealthCheckup
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbcView = new System.Windows.Forms.TabControl();
            this.tbpShortDeductions = new System.Windows.Forms.TabPage();
            this.lblTotalShortPayments = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvShortDeductions = new DGVControl.DGVControl();
            this.tbpLatePayments = new System.Windows.Forms.TabPage();
            this.lblInterestAmountReported = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTotalInterest = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblInterestPayable = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvLatePayments = new DGVControl.DGVControl();
            this.tbpInvalidPAN = new System.Windows.Forms.TabPage();
            this.dgvInvalidPAN = new DGVControl.DGVControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblHideTabs = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.pnlReturnSummary = new System.Windows.Forms.Panel();
            this.grpControlSummary = new System.Windows.Forms.GroupBox();
            this.txtAmountPaid = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtTotalDeducteeTDS = new System.Windows.Forms.Label();
            this.txtTotalDeducteeRecords = new System.Windows.Forms.Label();
            this.txtTotalChallanAmount = new System.Windows.Forms.Label();
            this.txtTotalChallanRecords = new System.Windows.Forms.Label();
            this.lblLabel2 = new System.Windows.Forms.Label();
            this.lblLabel1 = new System.Windows.Forms.Label();
            this.lblLabel4 = new System.Windows.Forms.Label();
            this.lblLabel3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDeducteePAN = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDeducteeName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewInvalidPAN = new System.Windows.Forms.Button();
            this.btnViewLatePayments = new System.Windows.Forms.Button();
            this.btnViewShortDeductions = new System.Windows.Forms.Button();
            this.lblInvalidPANNos = new System.Windows.Forms.Label();
            this.lblLatePaymentsNos = new System.Windows.Forms.Label();
            this.lblShortDeductionsNos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpMain.SuspendLayout();
            this.tbcView.SuspendLayout();
            this.tbpShortDeductions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortDeductions)).BeginInit();
            this.tbpLatePayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLatePayments)).BeginInit();
            this.tbpInvalidPAN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvalidPAN)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.pnlReturnSummary.SuspendLayout();
            this.grpControlSummary.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(122, 645);
            this.grpSort.Size = new System.Drawing.Size(15, 15);
            this.grpSort.Visible = false;
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 646);
            this.grpSearch.Size = new System.Drawing.Size(21, 15);
            this.grpSearch.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Predict Defaults";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 601);
            // 
            // grpButton
            // 
            this.grpButton.Location = new System.Drawing.Point(0, 604);
            this.grpButton.Size = new System.Drawing.Size(10, 48);
            this.grpButton.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.pnlReturnSummary);
            this.pnlControls.Controls.Add(this.lblDisclaimer);
            this.pnlControls.Controls.Add(this.panel4);
            this.pnlControls.Controls.Add(this.lblHideTabs);
            this.pnlControls.Controls.Add(this.groupBox3);
            this.pnlControls.Controls.Add(this.panel3);
            this.pnlControls.Controls.Add(this.tbcView);
            this.pnlControls.Controls.Add(this.grpMain);
            this.pnlControls.Size = new System.Drawing.Size(1003, 623);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(10, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.cmbFormNo);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.cmbQuarter);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.cmbFinancialYear);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.cmbCompany);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.Location = new System.Drawing.Point(5, 2);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(992, 59);
            this.grpMain.TabIndex = 1;
            this.grpMain.TabStop = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.ForeColor = System.Drawing.Color.Black;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(143, 34);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(106, 21);
            this.cmbFormNo.TabIndex = 19;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(45, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select Form No";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.ForeColor = System.Drawing.Color.Black;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(393, 34);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(58, 21);
            this.cmbQuarter.TabIndex = 1;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(300, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(143, 10);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select Financial Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(393, 10);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(580, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(291, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select Company";
            // 
            // tbcView
            // 
            this.tbcView.Controls.Add(this.tbpShortDeductions);
            this.tbcView.Controls.Add(this.tbpLatePayments);
            this.tbcView.Controls.Add(this.tbpInvalidPAN);
            this.tbcView.Location = new System.Drawing.Point(5, 194);
            this.tbcView.Name = "tbcView";
            this.tbcView.SelectedIndex = 0;
            this.tbcView.Size = new System.Drawing.Size(994, 366);
            this.tbcView.TabIndex = 5;
            // 
            // tbpShortDeductions
            // 
            this.tbpShortDeductions.Controls.Add(this.lblTotalShortPayments);
            this.tbpShortDeductions.Controls.Add(this.label9);
            this.tbpShortDeductions.Controls.Add(this.dgvShortDeductions);
            this.tbpShortDeductions.Location = new System.Drawing.Point(4, 22);
            this.tbpShortDeductions.Name = "tbpShortDeductions";
            this.tbpShortDeductions.Padding = new System.Windows.Forms.Padding(3);
            this.tbpShortDeductions.Size = new System.Drawing.Size(986, 340);
            this.tbpShortDeductions.TabIndex = 0;
            this.tbpShortDeductions.Text = "tabPage1";
            this.tbpShortDeductions.UseVisualStyleBackColor = true;
            // 
            // lblTotalShortPayments
            // 
            this.lblTotalShortPayments.BackColor = System.Drawing.SystemColors.Info;
            this.lblTotalShortPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalShortPayments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalShortPayments.Location = new System.Drawing.Point(890, 313);
            this.lblTotalShortPayments.Name = "lblTotalShortPayments";
            this.lblTotalShortPayments.Size = new System.Drawing.Size(90, 20);
            this.lblTotalShortPayments.TabIndex = 190;
            this.lblTotalShortPayments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(728, 316);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 13);
            this.label9.TabIndex = 189;
            this.label9.Text = "Total of Short deductions :";
            // 
            // dgvShortDeductions
            // 
            this.dgvShortDeductions.AllowUserToAddRows = false;
            this.dgvShortDeductions.AllowUserToDeleteRows = false;
            this.dgvShortDeductions.AllowUserToOrderColumns = true;
            this.dgvShortDeductions.AllowUserToResizeRows = false;
            this.dgvShortDeductions.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvShortDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvShortDeductions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShortDeductions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvShortDeductions.GridColor = System.Drawing.SystemColors.Control;
            this.dgvShortDeductions.Location = new System.Drawing.Point(4, 4);
            this.dgvShortDeductions.MultiSelect = false;
            this.dgvShortDeductions.Name = "dgvShortDeductions";
            this.dgvShortDeductions.RowHeadersWidth = 20;
            this.dgvShortDeductions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShortDeductions.Size = new System.Drawing.Size(978, 303);
            this.dgvShortDeductions.TabIndex = 188;
            this.dgvShortDeductions.TabStop = false;
            // 
            // tbpLatePayments
            // 
            this.tbpLatePayments.Controls.Add(this.lblInterestAmountReported);
            this.tbpLatePayments.Controls.Add(this.label16);
            this.tbpLatePayments.Controls.Add(this.lblTotalInterest);
            this.tbpLatePayments.Controls.Add(this.label14);
            this.tbpLatePayments.Controls.Add(this.lblInterestPayable);
            this.tbpLatePayments.Controls.Add(this.label11);
            this.tbpLatePayments.Controls.Add(this.dgvLatePayments);
            this.tbpLatePayments.Location = new System.Drawing.Point(4, 22);
            this.tbpLatePayments.Name = "tbpLatePayments";
            this.tbpLatePayments.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLatePayments.Size = new System.Drawing.Size(986, 340);
            this.tbpLatePayments.TabIndex = 1;
            this.tbpLatePayments.Text = "tabPage2";
            this.tbpLatePayments.UseVisualStyleBackColor = true;
            // 
            // lblInterestAmountReported
            // 
            this.lblInterestAmountReported.BackColor = System.Drawing.SystemColors.Info;
            this.lblInterestAmountReported.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInterestAmountReported.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterestAmountReported.Location = new System.Drawing.Point(692, 314);
            this.lblInterestAmountReported.Name = "lblInterestAmountReported";
            this.lblInterestAmountReported.Size = new System.Drawing.Size(90, 20);
            this.lblInterestAmountReported.TabIndex = 196;
            this.lblInterestAmountReported.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(535, 317);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(154, 13);
            this.label16.TabIndex = 195;
            this.label16.Text = "Interest amount reported :";
            // 
            // lblTotalInterest
            // 
            this.lblTotalInterest.BackColor = System.Drawing.SystemColors.Info;
            this.lblTotalInterest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalInterest.Location = new System.Drawing.Point(441, 314);
            this.lblTotalInterest.Name = "lblTotalInterest";
            this.lblTotalInterest.Size = new System.Drawing.Size(90, 20);
            this.lblTotalInterest.TabIndex = 194;
            this.lblTotalInterest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(347, 317);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 193;
            this.label14.Text = "Total Interest :";
            // 
            // lblInterestPayable
            // 
            this.lblInterestPayable.BackColor = System.Drawing.SystemColors.Info;
            this.lblInterestPayable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInterestPayable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterestPayable.Location = new System.Drawing.Point(892, 314);
            this.lblInterestPayable.Name = "lblInterestPayable";
            this.lblInterestPayable.Size = new System.Drawing.Size(90, 20);
            this.lblInterestPayable.TabIndex = 192;
            this.lblInterestPayable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(783, 317);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 191;
            this.label11.Text = "Interest payable :";
            // 
            // dgvLatePayments
            // 
            this.dgvLatePayments.AllowUserToAddRows = false;
            this.dgvLatePayments.AllowUserToDeleteRows = false;
            this.dgvLatePayments.AllowUserToOrderColumns = true;
            this.dgvLatePayments.AllowUserToResizeRows = false;
            this.dgvLatePayments.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvLatePayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLatePayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLatePayments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLatePayments.GridColor = System.Drawing.SystemColors.Control;
            this.dgvLatePayments.Location = new System.Drawing.Point(4, 4);
            this.dgvLatePayments.MultiSelect = false;
            this.dgvLatePayments.Name = "dgvLatePayments";
            this.dgvLatePayments.RowHeadersWidth = 20;
            this.dgvLatePayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLatePayments.Size = new System.Drawing.Size(978, 303);
            this.dgvLatePayments.TabIndex = 189;
            this.dgvLatePayments.TabStop = false;
            // 
            // tbpInvalidPAN
            // 
            this.tbpInvalidPAN.Controls.Add(this.dgvInvalidPAN);
            this.tbpInvalidPAN.Location = new System.Drawing.Point(4, 22);
            this.tbpInvalidPAN.Name = "tbpInvalidPAN";
            this.tbpInvalidPAN.Padding = new System.Windows.Forms.Padding(3);
            this.tbpInvalidPAN.Size = new System.Drawing.Size(986, 340);
            this.tbpInvalidPAN.TabIndex = 2;
            this.tbpInvalidPAN.Text = "tabPage3";
            this.tbpInvalidPAN.UseVisualStyleBackColor = true;
            // 
            // dgvInvalidPAN
            // 
            this.dgvInvalidPAN.AllowUserToAddRows = false;
            this.dgvInvalidPAN.AllowUserToDeleteRows = false;
            this.dgvInvalidPAN.AllowUserToOrderColumns = true;
            this.dgvInvalidPAN.AllowUserToResizeRows = false;
            this.dgvInvalidPAN.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvInvalidPAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInvalidPAN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvalidPAN.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInvalidPAN.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInvalidPAN.Location = new System.Drawing.Point(3, 5);
            this.dgvInvalidPAN.MultiSelect = false;
            this.dgvInvalidPAN.Name = "dgvInvalidPAN";
            this.dgvInvalidPAN.RowHeadersWidth = 20;
            this.dgvInvalidPAN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvalidPAN.Size = new System.Drawing.Size(979, 333);
            this.dgvInvalidPAN.TabIndex = 188;
            this.dgvInvalidPAN.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 192);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(995, 1);
            this.panel3.TabIndex = 22;
            // 
            // lblHideTabs
            // 
            this.lblHideTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHideTabs.Location = new System.Drawing.Point(4, 192);
            this.lblHideTabs.Name = "lblHideTabs";
            this.lblHideTabs.Size = new System.Drawing.Size(995, 23);
            this.lblHideTabs.TabIndex = 50;
            this.lblHideTabs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnXit);
            this.groupBox3.Controls.Add(this.btnPrintGrid);
            this.groupBox3.Location = new System.Drawing.Point(772, 566);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 40);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(112, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "E&xit";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.ForeColor = System.Drawing.Color.Black;
            this.btnPrintGrid.Location = new System.Drawing.Point(34, 11);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(77, 23);
            this.btnPrintGrid.TabIndex = 189;
            this.btnPrintGrid.Text = "&Print";
            this.btnPrintGrid.UseVisualStyleBackColor = false;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 64);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(995, 1);
            this.panel4.TabIndex = 71;
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.lblDisclaimer.Location = new System.Drawing.Point(9, 569);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(757, 44);
            this.lblDisclaimer.TabIndex = 177;
            this.lblDisclaimer.Text = "Disclaimer";
            this.lblDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlReturnSummary
            // 
            this.pnlReturnSummary.Controls.Add(this.grpControlSummary);
            this.pnlReturnSummary.Controls.Add(this.groupBox2);
            this.pnlReturnSummary.Controls.Add(this.panel2);
            this.pnlReturnSummary.Controls.Add(this.groupBox1);
            this.pnlReturnSummary.Controls.Add(this.panel5);
            this.pnlReturnSummary.Location = new System.Drawing.Point(5, 71);
            this.pnlReturnSummary.Name = "pnlReturnSummary";
            this.pnlReturnSummary.Size = new System.Drawing.Size(992, 124);
            this.pnlReturnSummary.TabIndex = 178;
            // 
            // grpControlSummary
            // 
            this.grpControlSummary.Controls.Add(this.txtAmountPaid);
            this.grpControlSummary.Controls.Add(this.label30);
            this.grpControlSummary.Controls.Add(this.txtTotalDeducteeTDS);
            this.grpControlSummary.Controls.Add(this.txtTotalDeducteeRecords);
            this.grpControlSummary.Controls.Add(this.txtTotalChallanAmount);
            this.grpControlSummary.Controls.Add(this.txtTotalChallanRecords);
            this.grpControlSummary.Controls.Add(this.lblLabel2);
            this.grpControlSummary.Controls.Add(this.lblLabel1);
            this.grpControlSummary.Controls.Add(this.lblLabel4);
            this.grpControlSummary.Controls.Add(this.lblLabel3);
            this.grpControlSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControlSummary.Location = new System.Drawing.Point(2, -2);
            this.grpControlSummary.Name = "grpControlSummary";
            this.grpControlSummary.Size = new System.Drawing.Size(988, 42);
            this.grpControlSummary.TabIndex = 75;
            this.grpControlSummary.TabStop = false;
            this.grpControlSummary.Text = "Control Summary";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaid.Location = new System.Drawing.Point(418, 15);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(100, 20);
            this.txtAmountPaid.TabIndex = 184;
            this.txtAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(357, 18);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 13);
            this.label30.TabIndex = 183;
            this.label30.Text = "Amt Paid";
            // 
            // txtTotalDeducteeTDS
            // 
            this.txtTotalDeducteeTDS.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalDeducteeTDS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDeducteeTDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDeducteeTDS.Location = new System.Drawing.Point(639, 15);
            this.txtTotalDeducteeTDS.Name = "txtTotalDeducteeTDS";
            this.txtTotalDeducteeTDS.Size = new System.Drawing.Size(100, 20);
            this.txtTotalDeducteeTDS.TabIndex = 182;
            this.txtTotalDeducteeTDS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalDeducteeRecords
            // 
            this.txtTotalDeducteeRecords.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalDeducteeRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDeducteeRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDeducteeRecords.Location = new System.Drawing.Point(285, 15);
            this.txtTotalDeducteeRecords.Name = "txtTotalDeducteeRecords";
            this.txtTotalDeducteeRecords.Size = new System.Drawing.Size(68, 20);
            this.txtTotalDeducteeRecords.TabIndex = 181;
            this.txtTotalDeducteeRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalChallanAmount
            // 
            this.txtTotalChallanAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalChallanAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalChallanAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalChallanAmount.Location = new System.Drawing.Point(863, 15);
            this.txtTotalChallanAmount.Name = "txtTotalChallanAmount";
            this.txtTotalChallanAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalChallanAmount.TabIndex = 180;
            this.txtTotalChallanAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalChallanRecords
            // 
            this.txtTotalChallanRecords.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalChallanRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalChallanRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalChallanRecords.Location = new System.Drawing.Point(114, 15);
            this.txtTotalChallanRecords.Name = "txtTotalChallanRecords";
            this.txtTotalChallanRecords.Size = new System.Drawing.Size(68, 20);
            this.txtTotalChallanRecords.TabIndex = 179;
            this.txtTotalChallanRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel2
            // 
            this.lblLabel2.AutoSize = true;
            this.lblLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel2.Location = new System.Drawing.Point(186, 18);
            this.lblLabel2.Name = "lblLabel2";
            this.lblLabel2.Size = new System.Drawing.Size(95, 13);
            this.lblLabel2.TabIndex = 178;
            this.lblLabel2.Text = "Total Deductee";
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel1.Location = new System.Drawing.Point(24, 18);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(86, 13);
            this.lblLabel1.TabIndex = 176;
            this.lblLabel1.Text = "Total Challan ";
            // 
            // lblLabel4
            // 
            this.lblLabel4.AutoSize = true;
            this.lblLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel4.Location = new System.Drawing.Point(521, 18);
            this.lblLabel4.Name = "lblLabel4";
            this.lblLabel4.Size = new System.Drawing.Size(114, 13);
            this.lblLabel4.TabIndex = 174;
            this.lblLabel4.Text = "Tot TDS Deducted";
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel3.Location = new System.Drawing.Point(742, 18);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(118, 13);
            this.lblLabel3.TabIndex = 172;
            this.lblLabel3.Text = "Tot Challan Amount";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDeducteePAN);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtDeducteeName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbSection);
            this.groupBox2.Controls.Add(this.lblSection);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(121, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 37);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // txtDeducteePAN
            // 
            this.txtDeducteePAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN.Location = new System.Drawing.Point(598, 12);
            this.txtDeducteePAN.MaxLength = 10;
            this.txtDeducteePAN.Name = "txtDeducteePAN";
            this.txtDeducteePAN.Size = new System.Drawing.Size(122, 20);
            this.txtDeducteePAN.TabIndex = 22;
            this.txtDeducteePAN.TextChanged += new System.EventHandler(this.txtDeducteeName_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(562, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "PAN";
            // 
            // txtDeducteeName
            // 
            this.txtDeducteeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeName.Location = new System.Drawing.Point(267, 12);
            this.txtDeducteeName.MaxLength = 75;
            this.txtDeducteeName.Name = "txtDeducteeName";
            this.txtDeducteeName.Size = new System.Drawing.Size(288, 20);
            this.txtDeducteeName.TabIndex = 20;
            this.txtDeducteeName.TextChanged += new System.EventHandler(this.txtDeducteeName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(164, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Deductee Name";
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(99, 12);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(57, 21);
            this.cmbSection.TabIndex = 18;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.txtDeducteeName_TextChanged);
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSection.Location = new System.Drawing.Point(44, 16);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(50, 13);
            this.lblSection.TabIndex = 10;
            this.lblSection.Text = "Section";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(-2, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(995, 1);
            this.panel2.TabIndex = 73;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewInvalidPAN);
            this.groupBox1.Controls.Add(this.btnViewLatePayments);
            this.groupBox1.Controls.Add(this.btnViewShortDeductions);
            this.groupBox1.Controls.Add(this.lblInvalidPANNos);
            this.groupBox1.Controls.Add(this.lblLatePaymentsNos);
            this.groupBox1.Controls.Add(this.lblShortDeductionsNos);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(120, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 35);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            // 
            // btnViewInvalidPAN
            // 
            this.btnViewInvalidPAN.BackColor = System.Drawing.Color.Lavender;
            this.btnViewInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewInvalidPAN.ForeColor = System.Drawing.Color.Black;
            this.btnViewInvalidPAN.Location = new System.Drawing.Point(691, 11);
            this.btnViewInvalidPAN.Name = "btnViewInvalidPAN";
            this.btnViewInvalidPAN.Size = new System.Drawing.Size(43, 22);
            this.btnViewInvalidPAN.TabIndex = 191;
            this.btnViewInvalidPAN.Text = "View";
            this.btnViewInvalidPAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewInvalidPAN.UseVisualStyleBackColor = false;
            this.btnViewInvalidPAN.Visible = false;
            this.btnViewInvalidPAN.Click += new System.EventHandler(this.btnViewInvalidPAN_Click);
            // 
            // btnViewLatePayments
            // 
            this.btnViewLatePayments.BackColor = System.Drawing.Color.Lavender;
            this.btnViewLatePayments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewLatePayments.ForeColor = System.Drawing.Color.Black;
            this.btnViewLatePayments.Location = new System.Drawing.Point(581, 10);
            this.btnViewLatePayments.Name = "btnViewLatePayments";
            this.btnViewLatePayments.Size = new System.Drawing.Size(43, 22);
            this.btnViewLatePayments.TabIndex = 190;
            this.btnViewLatePayments.Text = "View";
            this.btnViewLatePayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewLatePayments.UseVisualStyleBackColor = false;
            this.btnViewLatePayments.Click += new System.EventHandler(this.btnViewLatePayments_Click);
            // 
            // btnViewShortDeductions
            // 
            this.btnViewShortDeductions.BackColor = System.Drawing.Color.Lavender;
            this.btnViewShortDeductions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewShortDeductions.ForeColor = System.Drawing.Color.Black;
            this.btnViewShortDeductions.Location = new System.Drawing.Point(333, 10);
            this.btnViewShortDeductions.Name = "btnViewShortDeductions";
            this.btnViewShortDeductions.Size = new System.Drawing.Size(43, 22);
            this.btnViewShortDeductions.TabIndex = 189;
            this.btnViewShortDeductions.Text = "View";
            this.btnViewShortDeductions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewShortDeductions.UseVisualStyleBackColor = false;
            this.btnViewShortDeductions.Click += new System.EventHandler(this.btnViewShortDeductions_Click);
            // 
            // lblInvalidPANNos
            // 
            this.lblInvalidPANNos.BackColor = System.Drawing.SystemColors.Info;
            this.lblInvalidPANNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvalidPANNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidPANNos.ForeColor = System.Drawing.Color.Black;
            this.lblInvalidPANNos.Location = new System.Drawing.Point(594, 11);
            this.lblInvalidPANNos.Name = "lblInvalidPANNos";
            this.lblInvalidPANNos.Size = new System.Drawing.Size(93, 22);
            this.lblInvalidPANNos.TabIndex = 26;
            this.lblInvalidPANNos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInvalidPANNos.Visible = false;
            // 
            // lblLatePaymentsNos
            // 
            this.lblLatePaymentsNos.BackColor = System.Drawing.SystemColors.Info;
            this.lblLatePaymentsNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLatePaymentsNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatePaymentsNos.ForeColor = System.Drawing.Color.Black;
            this.lblLatePaymentsNos.Location = new System.Drawing.Point(484, 10);
            this.lblLatePaymentsNos.Name = "lblLatePaymentsNos";
            this.lblLatePaymentsNos.Size = new System.Drawing.Size(93, 22);
            this.lblLatePaymentsNos.TabIndex = 25;
            this.lblLatePaymentsNos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShortDeductionsNos
            // 
            this.lblShortDeductionsNos.BackColor = System.Drawing.SystemColors.Info;
            this.lblShortDeductionsNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShortDeductionsNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortDeductionsNos.ForeColor = System.Drawing.Color.Black;
            this.lblShortDeductionsNos.Location = new System.Drawing.Point(236, 10);
            this.lblShortDeductionsNos.Name = "lblShortDeductionsNos";
            this.lblShortDeductionsNos.Size = new System.Drawing.Size(93, 22);
            this.lblShortDeductionsNos.TabIndex = 24;
            this.lblShortDeductionsNos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(514, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Invalid PAN";
            this.label4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(126, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Short Deductions";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(389, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Late Payments";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(-1, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(995, 1);
            this.panel5.TabIndex = 71;
            // 
            // TrnHealthCheckup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1019, 669);
            this.Name = "TrnHealthCheckup";
            this.Load += new System.EventHandler(this.TrnHealthCheckup_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tbcView.ResumeLayout(false);
            this.tbpShortDeductions.ResumeLayout(false);
            this.tbpShortDeductions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortDeductions)).EndInit();
            this.tbpLatePayments.ResumeLayout(false);
            this.tbpLatePayments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLatePayments)).EndInit();
            this.tbpInvalidPAN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvalidPAN)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.pnlReturnSummary.ResumeLayout(false);
            this.grpControlSummary.ResumeLayout(false);
            this.grpControlSummary.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tbcView;
        private System.Windows.Forms.TabPage tbpShortDeductions;
        private System.Windows.Forms.TabPage tbpLatePayments;
        private System.Windows.Forms.TabPage tbpInvalidPAN;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblHideTabs;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnPrintGrid;
        private DGVControl.DGVControl dgvInvalidPAN;
        private DGVControl.DGVControl dgvShortDeductions;
        private DGVControl.DGVControl dgvLatePayments;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Panel pnlReturnSummary;
        private System.Windows.Forms.GroupBox grpControlSummary;
        private System.Windows.Forms.Label txtAmountPaid;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label txtTotalDeducteeTDS;
        private System.Windows.Forms.Label txtTotalDeducteeRecords;
        private System.Windows.Forms.Label txtTotalChallanAmount;
        private System.Windows.Forms.Label txtTotalChallanRecords;
        private System.Windows.Forms.Label lblLabel2;
        private System.Windows.Forms.Label lblLabel1;
        private System.Windows.Forms.Label lblLabel4;
        private System.Windows.Forms.Label lblLabel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDeducteePAN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDeducteeName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnViewInvalidPAN;
        private System.Windows.Forms.Button btnViewLatePayments;
        private System.Windows.Forms.Button btnViewShortDeductions;
        private System.Windows.Forms.Label lblInvalidPANNos;
        private System.Windows.Forms.Label lblLatePaymentsNos;
        private System.Windows.Forms.Label lblShortDeductionsNos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalShortPayments;
        private System.Windows.Forms.Label lblInterestPayable;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblInterestAmountReported;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTotalInterest;
        private System.Windows.Forms.Label label14;
    }
}
