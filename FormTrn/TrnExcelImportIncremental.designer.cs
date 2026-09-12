namespace TDSMAN.FormTrn
{
    partial class TrnExcelImportIncremental
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnExcelImportIncremental));
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.tbcExcelImport = new System.Windows.Forms.TabControl();
            this.tbpValidateExcelFile = new System.Windows.Forms.TabPage();
            this.grpEnterDeducteeDetailsOnly = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkEnterDeducteeDetailsOnly = new System.Windows.Forms.CheckBox();
            this.grpImportType = new System.Windows.Forms.GroupBox();
            this.lblNoteAdd = new System.Windows.Forms.Label();
            this.rbnIncremental = new System.Windows.Forms.RadioButton();
            this.rbnNewImport = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProgressDisplayMessage = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkColorCodingExcelsheet = new System.Windows.Forms.CheckBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblTaxComputingMessageF24QSDExcel = new System.Windows.Forms.Label();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.txtDeductorType = new System.Windows.Forms.TextBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRoundOffTaxableAmount = new System.Windows.Forms.CheckBox();
            this.tbpImportExcelFile = new System.Windows.Forms.TabPage();
            this.dgcViewChallan = new DGVControl.DGVControl();
            this.lblChallanGridCaption = new System.Windows.Forms.Label();
            this.dgcViewDeductee = new DGVControl.DGVControl();
            this.lblDeducteeGridCaption = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.prgImportBar = new System.Windows.Forms.ProgressBar();
            this.lblMessageLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkVerifyAllPANs = new System.Windows.Forms.LinkLabel();
            this.lnkVerifyOnlyNewPANs = new System.Windows.Forms.LinkLabel();
            this.lblFinancialYearId = new System.Windows.Forms.Label();
            this.btnBulkPANValidation = new System.Windows.Forms.Button();
            this.lblToolTip = new System.Windows.Forms.Label();
            this.btnOpenNewDeducteesFound = new System.Windows.Forms.Button();
            this.lblNewDeducteesFound = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.lblTANNo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblFormNo = new System.Windows.Forms.Label();
            this.lblQtr = new System.Windows.Forms.Label();
            this.lblFinancialYear = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbpImportSalaryDetails = new System.Windows.Forms.TabPage();
            this.dgcViewSD = new DGVControl.DGVControl();
            this.lblSDGridCaption = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.prgSDBar = new System.Windows.Forms.ProgressBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lnkVerifyAllPANsSD = new System.Windows.Forms.LinkLabel();
            this.lnkVerifyOnlyNewPANsSD = new System.Windows.Forms.LinkLabel();
            this.lblSDFormNo = new System.Windows.Forms.Label();
            this.lblSDCompany = new System.Windows.Forms.Label();
            this.btnSDNewDeductees = new System.Windows.Forms.Button();
            this.lblSDNewDeductees = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTotalEmployeeRecords = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSDTAN = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblSDFinancialYear = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.tbpValidateCSVFile = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.grpCSVDeducteeWithoutChallan = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkCsvEnterDeducteeDetailsOnly = new System.Windows.Forms.CheckBox();
            this.grpCSVImportType = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rbnCSVIncremental = new System.Windows.Forms.RadioButton();
            this.rbnCSVNewImport = new System.Windows.Forms.RadioButton();
            this.grpCSVProgress = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.prgBarCsv = new System.Windows.Forms.ProgressBar();
            this.grpSelectCSVFile = new System.Windows.Forms.GroupBox();
            this.btnSelectChallanCSVFile = new System.Windows.Forms.Button();
            this.txtChallanCSVFilePath = new System.Windows.Forms.TextBox();
            this.lblCSVChallanPathCaption = new System.Windows.Forms.Label();
            this.btnSelectDeducteeCSVFile = new System.Windows.Forms.Button();
            this.txtDeducteeCSVFilePath = new System.Windows.Forms.TextBox();
            this.lblCSVDeducteePathCaption = new System.Windows.Forms.Label();
            this.grpMainCSV = new System.Windows.Forms.GroupBox();
            this.lblTaxComputingMessageF24QSDCSV = new System.Windows.Forms.Label();
            this.lnkCSVBlankFormat = new System.Windows.Forms.LinkLabel();
            this.lnkCSVSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.txtCSVDeductorType = new System.Windows.Forms.TextBox();
            this.cmbCSVFormNo = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbCSVQtr = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbCSVFinancialYear = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbCSVCompany = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.chkCSVRoundOffTaxableAmount = new System.Windows.Forms.CheckBox();
            this.lblHide = new System.Windows.Forms.Label();
            this.tmrLoginRefresh = new System.Windows.Forms.Timer(this.components);
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.tbcExcelImport.SuspendLayout();
            this.tbpValidateExcelFile.SuspendLayout();
            this.grpEnterDeducteeDetailsOnly.SuspendLayout();
            this.grpImportType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.tbpImportExcelFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewDeductee)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tbpImportSalaryDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewSD)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tbpValidateCSVFile.SuspendLayout();
            this.grpCSVDeducteeWithoutChallan.SuspendLayout();
            this.grpCSVImportType.SuspendLayout();
            this.grpCSVProgress.SuspendLayout();
            this.grpSelectCSVFile.SuspendLayout();
            this.grpMainCSV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 623);
            this.grpSort.Size = new System.Drawing.Size(41, 19);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(925, 15);
            this.BtnCancel.Size = new System.Drawing.Size(7, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(354, 13);
            this.BtnSave.Size = new System.Drawing.Size(147, 23);
            this.BtnSave.Text = "&Validate Excel file";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 620);
            this.grpSearch.Size = new System.Drawing.Size(71, 23);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(920, 15);
            this.BtnEdit.Size = new System.Drawing.Size(5, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(915, 15);
            this.BtnAdd.Size = new System.Drawing.Size(5, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(946, 15);
            this.BtnRefresh.Size = new System.Drawing.Size(11, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(939, 15);
            this.BtnDelete.Size = new System.Drawing.Size(7, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(932, 15);
            this.BtnSearch.Size = new System.Drawing.Size(7, 23);
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Controls.SetChildIndex(this.BtnAdd, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnEdit, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSave, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnCancel, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSort, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSearch, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnDelete, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnRefresh, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnPrint, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnExit, 0);
            this.grpButton.Controls.SetChildIndex(this.pctVideoDemo, 0);
            this.grpButton.Controls.SetChildIndex(this.pctUserManual, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tbcExcelImport);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // tbcExcelImport
            // 
            this.tbcExcelImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcExcelImport.Controls.Add(this.tbpValidateExcelFile);
            this.tbcExcelImport.Controls.Add(this.tbpImportExcelFile);
            this.tbcExcelImport.Controls.Add(this.tbpImportSalaryDetails);
            this.tbcExcelImport.Controls.Add(this.tbpValidateCSVFile);
            this.tbcExcelImport.Location = new System.Drawing.Point(10, 6);
            this.tbcExcelImport.Name = "tbcExcelImport";
            this.tbcExcelImport.SelectedIndex = 0;
            this.tbcExcelImport.Size = new System.Drawing.Size(990, 534);
            this.tbcExcelImport.TabIndex = 0;
            this.tbcExcelImport.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcExcelImport_Selecting);
            // 
            // tbpValidateExcelFile
            // 
            this.tbpValidateExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpValidateExcelFile.Controls.Add(this.grpEnterDeducteeDetailsOnly);
            this.tbpValidateExcelFile.Controls.Add(this.grpImportType);
            this.tbpValidateExcelFile.Controls.Add(this.label9);
            this.tbpValidateExcelFile.Controls.Add(this.groupBox1);
            this.tbpValidateExcelFile.Controls.Add(this.groupBox4);
            this.tbpValidateExcelFile.Controls.Add(this.grpMain);
            this.tbpValidateExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpValidateExcelFile.Name = "tbpValidateExcelFile";
            this.tbpValidateExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpValidateExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpValidateExcelFile.TabIndex = 0;
            // 
            // grpEnterDeducteeDetailsOnly
            // 
            this.grpEnterDeducteeDetailsOnly.Controls.Add(this.label13);
            this.grpEnterDeducteeDetailsOnly.Controls.Add(this.chkEnterDeducteeDetailsOnly);
            this.grpEnterDeducteeDetailsOnly.Location = new System.Drawing.Point(241, 437);
            this.grpEnterDeducteeDetailsOnly.Name = "grpEnterDeducteeDetailsOnly";
            this.grpEnterDeducteeDetailsOnly.Size = new System.Drawing.Size(500, 61);
            this.grpEnterDeducteeDetailsOnly.TabIndex = 74;
            this.grpEnterDeducteeDetailsOnly.TabStop = false;
            this.grpEnterDeducteeDetailsOnly.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(31, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(438, 13);
            this.label13.TabIndex = 151;
            this.label13.Text = "Put 0 or blank in \'Challan Serial Reference\' cell of \'Deductee Details\' sheet.";
            // 
            // chkEnterDeducteeDetailsOnly
            // 
            this.chkEnterDeducteeDetailsOnly.AutoSize = true;
            this.chkEnterDeducteeDetailsOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkEnterDeducteeDetailsOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnterDeducteeDetailsOnly.Location = new System.Drawing.Point(147, 12);
            this.chkEnterDeducteeDetailsOnly.Name = "chkEnterDeducteeDetailsOnly";
            this.chkEnterDeducteeDetailsOnly.Size = new System.Drawing.Size(206, 17);
            this.chkEnterDeducteeDetailsOnly.TabIndex = 149;
            this.chkEnterDeducteeDetailsOnly.Text = "Enter Deductee without Challan";
            this.chkEnterDeducteeDetailsOnly.UseVisualStyleBackColor = true;
            this.chkEnterDeducteeDetailsOnly.CheckedChanged += new System.EventHandler(this.chkEnterDeducteeDetailsOnly_CheckedChanged);
            // 
            // grpImportType
            // 
            this.grpImportType.Controls.Add(this.lblNoteAdd);
            this.grpImportType.Controls.Add(this.rbnIncremental);
            this.grpImportType.Controls.Add(this.rbnNewImport);
            this.grpImportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImportType.ForeColor = System.Drawing.Color.Black;
            this.grpImportType.Location = new System.Drawing.Point(310, 279);
            this.grpImportType.Name = "grpImportType";
            this.grpImportType.Size = new System.Drawing.Size(363, 90);
            this.grpImportType.TabIndex = 73;
            this.grpImportType.TabStop = false;
            this.grpImportType.Text = "Import Type";
            // 
            // lblNoteAdd
            // 
            this.lblNoteAdd.AutoSize = true;
            this.lblNoteAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteAdd.ForeColor = System.Drawing.Color.Blue;
            this.lblNoteAdd.Location = new System.Drawing.Point(46, 68);
            this.lblNoteAdd.Name = "lblNoteAdd";
            this.lblNoteAdd.Size = new System.Drawing.Size(305, 13);
            this.lblNoteAdd.TabIndex = 149;
            this.lblNoteAdd.Text = "Note : In excel challan serial no. should start from 1.";
            // 
            // rbnIncremental
            // 
            this.rbnIncremental.AutoSize = true;
            this.rbnIncremental.Location = new System.Drawing.Point(49, 43);
            this.rbnIncremental.Name = "rbnIncremental";
            this.rbnIncremental.Size = new System.Drawing.Size(160, 17);
            this.rbnIncremental.TabIndex = 1;
            this.rbnIncremental.Text = "Add to the existing data";
            this.rbnIncremental.UseVisualStyleBackColor = true;
            // 
            // rbnNewImport
            // 
            this.rbnNewImport.AutoSize = true;
            this.rbnNewImport.Checked = true;
            this.rbnNewImport.Location = new System.Drawing.Point(49, 19);
            this.rbnNewImport.Name = "rbnNewImport";
            this.rbnNewImport.Size = new System.Drawing.Size(187, 17);
            this.rbnNewImport.TabIndex = 0;
            this.rbnNewImport.TabStop = true;
            this.rbnNewImport.Text = "New / Replace existing data";
            this.rbnNewImport.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(38, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(894, 21);
            this.label9.TabIndex = 72;
            this.label9.Text = "Excel import is under maintanence for the incorporation of new structures as prov" +
    "ided in FVU 3.8. The update of Excel import will be delivered within 9th of july" +
    ", 2013.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProgressDisplayMessage);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(117, 368);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(744, 67);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblProgressDisplayMessage
            // 
            this.lblProgressDisplayMessage.AutoSize = true;
            this.lblProgressDisplayMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressDisplayMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblProgressDisplayMessage.Location = new System.Drawing.Point(19, 47);
            this.lblProgressDisplayMessage.Name = "lblProgressDisplayMessage";
            this.lblProgressDisplayMessage.Size = new System.Drawing.Size(88, 13);
            this.lblProgressDisplayMessage.TabIndex = 148;
            this.lblProgressDisplayMessage.Text = "Excel file path";
            this.lblProgressDisplayMessage.Visible = false;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 19);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(712, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkColorCodingExcelsheet);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(117, 204);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(749, 73);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Excel file";
            // 
            // chkColorCodingExcelsheet
            // 
            this.chkColorCodingExcelsheet.AutoSize = true;
            this.chkColorCodingExcelsheet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkColorCodingExcelsheet.Location = new System.Drawing.Point(112, 48);
            this.chkColorCodingExcelsheet.Name = "chkColorCodingExcelsheet";
            this.chkColorCodingExcelsheet.Size = new System.Drawing.Size(436, 17);
            this.chkColorCodingExcelsheet.TabIndex = 148;
            this.chkColorCodingExcelsheet.Text = "Color coding in Excel sheet for errors (this may take a few more minutes)";
            this.chkColorCodingExcelsheet.UseVisualStyleBackColor = true;
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(649, 20);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(44, 23);
            this.btnSelectExcelPath.TabIndex = 146;
            this.btnSelectExcelPath.Text = ". . .";
            this.btnSelectExcelPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelPath.Location = new System.Drawing.Point(168, 22);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(479, 20);
            this.txtExcelPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(74, 25);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(88, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Excel file path";
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblTaxComputingMessageF24QSDExcel);
            this.grpMain.Controls.Add(this.lnkSearchByTAN);
            this.grpMain.Controls.Add(this.txtDeductorType);
            this.grpMain.Controls.Add(this.cmbFormNo);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.cmbQuarter);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.cmbFinancialYear);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.cmbCompany);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Controls.Add(this.chkRoundOffTaxableAmount);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.ForeColor = System.Drawing.Color.Black;
            this.grpMain.Location = new System.Drawing.Point(117, 39);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(749, 161);
            this.grpMain.TabIndex = 67;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Select";
            // 
            // lblTaxComputingMessageF24QSDExcel
            // 
            this.lblTaxComputingMessageF24QSDExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxComputingMessageF24QSDExcel.ForeColor = System.Drawing.Color.Blue;
            this.lblTaxComputingMessageF24QSDExcel.Location = new System.Drawing.Point(5, 112);
            this.lblTaxComputingMessageF24QSDExcel.Name = "lblTaxComputingMessageF24QSDExcel";
            this.lblTaxComputingMessageF24QSDExcel.Size = new System.Drawing.Size(740, 42);
            this.lblTaxComputingMessageF24QSDExcel.TabIndex = 217;
            this.lblTaxComputingMessageF24QSDExcel.Text = resources.GetString("lblTaxComputingMessageF24QSDExcel.Text");
            this.lblTaxComputingMessageF24QSDExcel.Visible = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(651, 90);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 215;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            this.lnkSearchByTAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lnkSearchByTAN_MouseMove);
            // 
            // txtDeductorType
            // 
            this.txtDeductorType.Location = new System.Drawing.Point(262, 39);
            this.txtDeductorType.Name = "txtDeductorType";
            this.txtDeductorType.Size = new System.Drawing.Size(24, 20);
            this.txtDeductorType.TabIndex = 21;
            this.txtDeductorType.Visible = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(168, 63);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(210, 21);
            this.cmbFormNo.TabIndex = 2;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbFormNo_SelectedIndexChanged);
            this.cmbFormNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFormNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select Form No.";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(168, 39);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(49, 21);
            this.cmbQuarter.TabIndex = 1;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.cmbQuarter_SelectedIndexChanged);
            this.cmbQuarter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbQuarter_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(168, 15);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYear_SelectedIndexChanged);
            this.cmbFinancialYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFinancialYear_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(168, 87);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(479, 21);
            this.cmbCompany.TabIndex = 3;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select Company";
            // 
            // chkRoundOffTaxableAmount
            // 
            this.chkRoundOffTaxableAmount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRoundOffTaxableAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRoundOffTaxableAmount.Location = new System.Drawing.Point(383, 48);
            this.chkRoundOffTaxableAmount.Name = "chkRoundOffTaxableAmount";
            this.chkRoundOffTaxableAmount.Size = new System.Drawing.Size(362, 39);
            this.chkRoundOffTaxableAmount.TabIndex = 216;
            this.chkRoundOffTaxableAmount.Text = "Total Taxable Income will not be validated - as Rounding off is enabled in Prefer" +
    "ences (no. 21).";
            this.chkRoundOffTaxableAmount.UseVisualStyleBackColor = true;
            this.chkRoundOffTaxableAmount.Visible = false;
            // 
            // tbpImportExcelFile
            // 
            this.tbpImportExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpImportExcelFile.Controls.Add(this.dgcViewChallan);
            this.tbpImportExcelFile.Controls.Add(this.lblChallanGridCaption);
            this.tbpImportExcelFile.Controls.Add(this.dgcViewDeductee);
            this.tbpImportExcelFile.Controls.Add(this.lblDeducteeGridCaption);
            this.tbpImportExcelFile.Controls.Add(this.groupBox3);
            this.tbpImportExcelFile.Controls.Add(this.lblMessageLabel);
            this.tbpImportExcelFile.Controls.Add(this.groupBox2);
            this.tbpImportExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpImportExcelFile.Name = "tbpImportExcelFile";
            this.tbpImportExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpImportExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpImportExcelFile.TabIndex = 1;
            // 
            // dgcViewChallan
            // 
            this.dgcViewChallan.AllowUserToAddRows = false;
            this.dgcViewChallan.AllowUserToDeleteRows = false;
            this.dgcViewChallan.AllowUserToOrderColumns = true;
            this.dgcViewChallan.AllowUserToResizeRows = false;
            this.dgcViewChallan.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewChallan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewChallan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgcViewChallan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewChallan.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewChallan.Location = new System.Drawing.Point(6, 138);
            this.dgcViewChallan.MultiSelect = false;
            this.dgcViewChallan.Name = "dgcViewChallan";
            this.dgcViewChallan.RowHeadersWidth = 20;
            this.dgcViewChallan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewChallan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewChallan.Size = new System.Drawing.Size(969, 326);
            this.dgcViewChallan.TabIndex = 190;
            this.dgcViewChallan.TabStop = false;
            this.dgcViewChallan.DoubleClick += new System.EventHandler(this.dgcViewChallan_DoubleClick);
            this.dgcViewChallan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgcViewChallan_KeyDown);
            this.dgcViewChallan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgcViewChallan_MouseClick);
            this.dgcViewChallan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgcViewChallan_MouseMove);
            // 
            // lblChallanGridCaption
            // 
            this.lblChallanGridCaption.BackColor = System.Drawing.Color.Honeydew;
            this.lblChallanGridCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChallanGridCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChallanGridCaption.Location = new System.Drawing.Point(6, 119);
            this.lblChallanGridCaption.Name = "lblChallanGridCaption";
            this.lblChallanGridCaption.Size = new System.Drawing.Size(969, 17);
            this.lblChallanGridCaption.TabIndex = 189;
            this.lblChallanGridCaption.Text = "Challan Details";
            this.lblChallanGridCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgcViewDeductee
            // 
            this.dgcViewDeductee.AllowUserToAddRows = false;
            this.dgcViewDeductee.AllowUserToDeleteRows = false;
            this.dgcViewDeductee.AllowUserToOrderColumns = true;
            this.dgcViewDeductee.AllowUserToResizeRows = false;
            this.dgcViewDeductee.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewDeductee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewDeductee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgcViewDeductee.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewDeductee.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewDeductee.Location = new System.Drawing.Point(6, 138);
            this.dgcViewDeductee.MultiSelect = false;
            this.dgcViewDeductee.Name = "dgcViewDeductee";
            this.dgcViewDeductee.RowHeadersWidth = 20;
            this.dgcViewDeductee.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewDeductee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewDeductee.Size = new System.Drawing.Size(969, 327);
            this.dgcViewDeductee.TabIndex = 192;
            this.dgcViewDeductee.TabStop = false;
            this.dgcViewDeductee.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgcViewDeductee_MouseClick);
            // 
            // lblDeducteeGridCaption
            // 
            this.lblDeducteeGridCaption.BackColor = System.Drawing.Color.Honeydew;
            this.lblDeducteeGridCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDeducteeGridCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeducteeGridCaption.Location = new System.Drawing.Point(6, 119);
            this.lblDeducteeGridCaption.Name = "lblDeducteeGridCaption";
            this.lblDeducteeGridCaption.Size = new System.Drawing.Size(969, 17);
            this.lblDeducteeGridCaption.TabIndex = 191;
            this.lblDeducteeGridCaption.Text = "Deductee Details";
            this.lblDeducteeGridCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.prgImportBar);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(6, 468);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(969, 32);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // prgImportBar
            // 
            this.prgImportBar.Location = new System.Drawing.Point(16, 16);
            this.prgImportBar.Name = "prgImportBar";
            this.prgImportBar.Size = new System.Drawing.Size(940, 9);
            this.prgImportBar.TabIndex = 147;
            // 
            // lblMessageLabel
            // 
            this.lblMessageLabel.BackColor = System.Drawing.Color.LightCyan;
            this.lblMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageLabel.ForeColor = System.Drawing.Color.Blue;
            this.lblMessageLabel.Location = new System.Drawing.Point(6, 441);
            this.lblMessageLabel.Name = "lblMessageLabel";
            this.lblMessageLabel.Size = new System.Drawing.Size(969, 23);
            this.lblMessageLabel.TabIndex = 78;
            this.lblMessageLabel.Text = "Double click the <Challan Details> grid to view the Deductee Details";
            this.lblMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lnkVerifyAllPANs);
            this.groupBox2.Controls.Add(this.lnkVerifyOnlyNewPANs);
            this.groupBox2.Controls.Add(this.lblFinancialYearId);
            this.groupBox2.Controls.Add(this.btnBulkPANValidation);
            this.groupBox2.Controls.Add(this.lblToolTip);
            this.groupBox2.Controls.Add(this.btnOpenNewDeducteesFound);
            this.groupBox2.Controls.Add(this.lblNewDeducteesFound);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtAmountPaid);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.txtTotalDeducteeTDS);
            this.groupBox2.Controls.Add(this.txtTotalDeducteeRecords);
            this.groupBox2.Controls.Add(this.txtTotalChallanAmount);
            this.groupBox2.Controls.Add(this.txtTotalChallanRecords);
            this.groupBox2.Controls.Add(this.lblLabel2);
            this.groupBox2.Controls.Add(this.lblLabel1);
            this.groupBox2.Controls.Add(this.lblLabel4);
            this.groupBox2.Controls.Add(this.lblLabel3);
            this.groupBox2.Controls.Add(this.lblTANNo);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblCompanyName);
            this.groupBox2.Controls.Add(this.lblFormNo);
            this.groupBox2.Controls.Add(this.lblQtr);
            this.groupBox2.Controls.Add(this.lblFinancialYear);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(6, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(970, 107);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Summary";
            // 
            // lnkVerifyAllPANs
            // 
            this.lnkVerifyAllPANs.AutoSize = true;
            this.lnkVerifyAllPANs.Location = new System.Drawing.Point(854, 86);
            this.lnkVerifyAllPANs.Name = "lnkVerifyAllPANs";
            this.lnkVerifyAllPANs.Size = new System.Drawing.Size(92, 13);
            this.lnkVerifyAllPANs.TabIndex = 203;
            this.lnkVerifyAllPANs.TabStop = true;
            this.lnkVerifyAllPANs.Text = "Verify All PANs";
            this.lnkVerifyAllPANs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVerifyAllPANs_LinkClicked);
            // 
            // lnkVerifyOnlyNewPANs
            // 
            this.lnkVerifyOnlyNewPANs.AutoSize = true;
            this.lnkVerifyOnlyNewPANs.Location = new System.Drawing.Point(634, 86);
            this.lnkVerifyOnlyNewPANs.Name = "lnkVerifyOnlyNewPANs";
            this.lnkVerifyOnlyNewPANs.Size = new System.Drawing.Size(128, 13);
            this.lnkVerifyOnlyNewPANs.TabIndex = 202;
            this.lnkVerifyOnlyNewPANs.TabStop = true;
            this.lnkVerifyOnlyNewPANs.Text = "Verify only new PANs";
            this.lnkVerifyOnlyNewPANs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVerifyOnlyNewPANs_LinkClicked);
            // 
            // lblFinancialYearId
            // 
            this.lblFinancialYearId.BackColor = System.Drawing.SystemColors.Info;
            this.lblFinancialYearId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFinancialYearId.Location = new System.Drawing.Point(954, 16);
            this.lblFinancialYearId.Name = "lblFinancialYearId";
            this.lblFinancialYearId.Size = new System.Drawing.Size(10, 21);
            this.lblFinancialYearId.TabIndex = 201;
            this.lblFinancialYearId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFinancialYearId.Visible = false;
            // 
            // btnBulkPANValidation
            // 
            this.btnBulkPANValidation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBulkPANValidation.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBulkPANValidation.Image = ((System.Drawing.Image)(resources.GetObject("btnBulkPANValidation.Image")));
            this.btnBulkPANValidation.Location = new System.Drawing.Point(252, 78);
            this.btnBulkPANValidation.Name = "btnBulkPANValidation";
            this.btnBulkPANValidation.Size = new System.Drawing.Size(36, 21);
            this.btnBulkPANValidation.TabIndex = 199;
            this.btnBulkPANValidation.UseVisualStyleBackColor = true;
            this.btnBulkPANValidation.Visible = false;
            this.btnBulkPANValidation.Click += new System.EventHandler(this.btnBulkPANValidation_Click);
            this.btnBulkPANValidation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnBulkPANValidation_MouseMove);
            // 
            // lblToolTip
            // 
            this.lblToolTip.AutoSize = true;
            this.lblToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTip.ForeColor = System.Drawing.Color.Red;
            this.lblToolTip.Location = new System.Drawing.Point(406, 86);
            this.lblToolTip.Name = "lblToolTip";
            this.lblToolTip.Size = new System.Drawing.Size(146, 13);
            this.lblToolTip.TabIndex = 198;
            this.lblToolTip.Text = "Total Deductee Records";
            this.lblToolTip.Visible = false;
            // 
            // btnOpenNewDeducteesFound
            // 
            this.btnOpenNewDeducteesFound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenNewDeducteesFound.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenNewDeducteesFound.Location = new System.Drawing.Point(215, 78);
            this.btnOpenNewDeducteesFound.Name = "btnOpenNewDeducteesFound";
            this.btnOpenNewDeducteesFound.Size = new System.Drawing.Size(36, 21);
            this.btnOpenNewDeducteesFound.TabIndex = 197;
            this.btnOpenNewDeducteesFound.Text = ". . .";
            this.btnOpenNewDeducteesFound.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenNewDeducteesFound.UseVisualStyleBackColor = true;
            this.btnOpenNewDeducteesFound.Click += new System.EventHandler(this.btnOpenNewDeducteesFound_Click);
            this.btnOpenNewDeducteesFound.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnOpenNewDeducteesFound_MouseMove);
            // 
            // lblNewDeducteesFound
            // 
            this.lblNewDeducteesFound.BackColor = System.Drawing.SystemColors.Info;
            this.lblNewDeducteesFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNewDeducteesFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewDeducteesFound.Location = new System.Drawing.Point(143, 78);
            this.lblNewDeducteesFound.Name = "lblNewDeducteesFound";
            this.lblNewDeducteesFound.Size = new System.Drawing.Size(70, 20);
            this.lblNewDeducteesFound.TabIndex = 196;
            this.lblNewDeducteesFound.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 13);
            this.label11.TabIndex = 195;
            this.label11.Text = "New Deductees Found";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaid.Location = new System.Drawing.Point(476, 52);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(94, 20);
            this.txtAmountPaid.TabIndex = 194;
            this.txtAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(397, 56);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(78, 13);
            this.label30.TabIndex = 193;
            this.label30.Text = "Amount Paid";
            // 
            // txtTotalDeducteeTDS
            // 
            this.txtTotalDeducteeTDS.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalDeducteeTDS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDeducteeTDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDeducteeTDS.Location = new System.Drawing.Point(856, 52);
            this.txtTotalDeducteeTDS.Name = "txtTotalDeducteeTDS";
            this.txtTotalDeducteeTDS.Size = new System.Drawing.Size(94, 20);
            this.txtTotalDeducteeTDS.TabIndex = 192;
            this.txtTotalDeducteeTDS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalDeducteeRecords
            // 
            this.txtTotalDeducteeRecords.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalDeducteeRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDeducteeRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDeducteeRecords.Location = new System.Drawing.Point(340, 52);
            this.txtTotalDeducteeRecords.Name = "txtTotalDeducteeRecords";
            this.txtTotalDeducteeRecords.Size = new System.Drawing.Size(57, 20);
            this.txtTotalDeducteeRecords.TabIndex = 191;
            this.txtTotalDeducteeRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalChallanAmount
            // 
            this.txtTotalChallanAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalChallanAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalChallanAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalChallanAmount.Location = new System.Drawing.Point(668, 52);
            this.txtTotalChallanAmount.Name = "txtTotalChallanAmount";
            this.txtTotalChallanAmount.Size = new System.Drawing.Size(94, 20);
            this.txtTotalChallanAmount.TabIndex = 190;
            this.txtTotalChallanAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalChallanRecords
            // 
            this.txtTotalChallanRecords.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalChallanRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalChallanRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalChallanRecords.Location = new System.Drawing.Point(147, 52);
            this.txtTotalChallanRecords.Name = "txtTotalChallanRecords";
            this.txtTotalChallanRecords.Size = new System.Drawing.Size(47, 20);
            this.txtTotalChallanRecords.TabIndex = 189;
            this.txtTotalChallanRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel2
            // 
            this.lblLabel2.AutoSize = true;
            this.lblLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel2.Location = new System.Drawing.Point(194, 55);
            this.lblLabel2.Name = "lblLabel2";
            this.lblLabel2.Size = new System.Drawing.Size(146, 13);
            this.lblLabel2.TabIndex = 188;
            this.lblLabel2.Text = "Total Deductee Records";
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel1.Location = new System.Drawing.Point(15, 55);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(133, 13);
            this.lblLabel1.TabIndex = 187;
            this.lblLabel1.Text = "Total Challan Records";
            // 
            // lblLabel4
            // 
            this.lblLabel4.AutoSize = true;
            this.lblLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel4.Location = new System.Drawing.Point(763, 55);
            this.lblLabel4.Name = "lblLabel4";
            this.lblLabel4.Size = new System.Drawing.Size(91, 13);
            this.lblLabel4.TabIndex = 186;
            this.lblLabel4.Text = "TDS Deducted";
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel3.Location = new System.Drawing.Point(572, 56);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(95, 13);
            this.lblLabel3.TabIndex = 185;
            this.lblLabel3.Text = "Challan Amount";
            // 
            // lblTANNo
            // 
            this.lblTANNo.BackColor = System.Drawing.SystemColors.Info;
            this.lblTANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTANNo.Location = new System.Drawing.Point(87, 23);
            this.lblTANNo.Name = "lblTANNo";
            this.lblTANNo.Size = new System.Drawing.Size(95, 21);
            this.lblTANNo.TabIndex = 27;
            this.lblTANNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "TAN No.";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.SystemColors.Info;
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompanyName.Location = new System.Drawing.Point(254, 23);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(298, 21);
            this.lblCompanyName.TabIndex = 25;
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFormNo
            // 
            this.lblFormNo.BackColor = System.Drawing.SystemColors.Info;
            this.lblFormNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFormNo.Location = new System.Drawing.Point(794, 23);
            this.lblFormNo.Name = "lblFormNo";
            this.lblFormNo.Size = new System.Drawing.Size(69, 21);
            this.lblFormNo.TabIndex = 24;
            this.lblFormNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQtr
            // 
            this.lblQtr.BackColor = System.Drawing.SystemColors.Info;
            this.lblQtr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQtr.Location = new System.Drawing.Point(920, 23);
            this.lblQtr.Name = "lblQtr";
            this.lblQtr.Size = new System.Drawing.Size(30, 21);
            this.lblQtr.TabIndex = 23;
            this.lblQtr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinancialYear
            // 
            this.lblFinancialYear.BackColor = System.Drawing.SystemColors.Info;
            this.lblFinancialYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFinancialYear.Location = new System.Drawing.Point(662, 23);
            this.lblFinancialYear.Name = "lblFinancialYear";
            this.lblFinancialYear.Size = new System.Drawing.Size(61, 21);
            this.lblFinancialYear.TabIndex = 22;
            this.lblFinancialYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(733, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Form No.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(867, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Quarter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(568, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Tax Year";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(190, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Company";
            // 
            // tbpImportSalaryDetails
            // 
            this.tbpImportSalaryDetails.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpImportSalaryDetails.Controls.Add(this.dgcViewSD);
            this.tbpImportSalaryDetails.Controls.Add(this.lblSDGridCaption);
            this.tbpImportSalaryDetails.Controls.Add(this.groupBox6);
            this.tbpImportSalaryDetails.Controls.Add(this.groupBox5);
            this.tbpImportSalaryDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpImportSalaryDetails.Name = "tbpImportSalaryDetails";
            this.tbpImportSalaryDetails.Size = new System.Drawing.Size(982, 508);
            this.tbpImportSalaryDetails.TabIndex = 2;
            // 
            // dgcViewSD
            // 
            this.dgcViewSD.AllowUserToAddRows = false;
            this.dgcViewSD.AllowUserToDeleteRows = false;
            this.dgcViewSD.AllowUserToOrderColumns = true;
            this.dgcViewSD.AllowUserToResizeRows = false;
            this.dgcViewSD.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewSD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewSD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgcViewSD.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewSD.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewSD.Location = new System.Drawing.Point(7, 134);
            this.dgcViewSD.MultiSelect = false;
            this.dgcViewSD.Name = "dgcViewSD";
            this.dgcViewSD.RowHeadersWidth = 20;
            this.dgcViewSD.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewSD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewSD.Size = new System.Drawing.Size(969, 332);
            this.dgcViewSD.TabIndex = 192;
            this.dgcViewSD.TabStop = false;
            // 
            // lblSDGridCaption
            // 
            this.lblSDGridCaption.BackColor = System.Drawing.Color.Honeydew;
            this.lblSDGridCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDGridCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDGridCaption.Location = new System.Drawing.Point(7, 115);
            this.lblSDGridCaption.Name = "lblSDGridCaption";
            this.lblSDGridCaption.Size = new System.Drawing.Size(969, 17);
            this.lblSDGridCaption.TabIndex = 191;
            this.lblSDGridCaption.Text = "Salary Details";
            this.lblSDGridCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.prgSDBar);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(7, 471);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(969, 32);
            this.groupBox6.TabIndex = 80;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Progress";
            // 
            // prgSDBar
            // 
            this.prgSDBar.Location = new System.Drawing.Point(16, 16);
            this.prgSDBar.Name = "prgSDBar";
            this.prgSDBar.Size = new System.Drawing.Size(940, 9);
            this.prgSDBar.TabIndex = 147;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lnkVerifyAllPANsSD);
            this.groupBox5.Controls.Add(this.lnkVerifyOnlyNewPANsSD);
            this.groupBox5.Controls.Add(this.lblSDFormNo);
            this.groupBox5.Controls.Add(this.lblSDCompany);
            this.groupBox5.Controls.Add(this.btnSDNewDeductees);
            this.groupBox5.Controls.Add(this.lblSDNewDeductees);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.lblTotalEmployeeRecords);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.lblSDTAN);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.lblSDFinancialYear);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(6, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(970, 107);
            this.groupBox5.TabIndex = 77;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Import Summary";
            // 
            // lnkVerifyAllPANsSD
            // 
            this.lnkVerifyAllPANsSD.AutoSize = true;
            this.lnkVerifyAllPANsSD.Location = new System.Drawing.Point(558, 82);
            this.lnkVerifyAllPANsSD.Name = "lnkVerifyAllPANsSD";
            this.lnkVerifyAllPANsSD.Size = new System.Drawing.Size(92, 13);
            this.lnkVerifyAllPANsSD.TabIndex = 205;
            this.lnkVerifyAllPANsSD.TabStop = true;
            this.lnkVerifyAllPANsSD.Text = "Verify All PANs";
            this.lnkVerifyAllPANsSD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVerifyAllPANsSD_LinkClicked);
            // 
            // lnkVerifyOnlyNewPANsSD
            // 
            this.lnkVerifyOnlyNewPANsSD.AutoSize = true;
            this.lnkVerifyOnlyNewPANsSD.Location = new System.Drawing.Point(338, 82);
            this.lnkVerifyOnlyNewPANsSD.Name = "lnkVerifyOnlyNewPANsSD";
            this.lnkVerifyOnlyNewPANsSD.Size = new System.Drawing.Size(128, 13);
            this.lnkVerifyOnlyNewPANsSD.TabIndex = 204;
            this.lnkVerifyOnlyNewPANsSD.TabStop = true;
            this.lnkVerifyOnlyNewPANsSD.Text = "Verify only new PANs";
            this.lnkVerifyOnlyNewPANsSD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVerifyOnlyNewPANsSD_LinkClicked);
            // 
            // lblSDFormNo
            // 
            this.lblSDFormNo.BackColor = System.Drawing.SystemColors.Info;
            this.lblSDFormNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDFormNo.Location = new System.Drawing.Point(919, 22);
            this.lblSDFormNo.Name = "lblSDFormNo";
            this.lblSDFormNo.Size = new System.Drawing.Size(41, 21);
            this.lblSDFormNo.TabIndex = 198;
            this.lblSDFormNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSDFormNo.Visible = false;
            // 
            // lblSDCompany
            // 
            this.lblSDCompany.BackColor = System.Drawing.SystemColors.Info;
            this.lblSDCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDCompany.Location = new System.Drawing.Point(259, 23);
            this.lblSDCompany.Name = "lblSDCompany";
            this.lblSDCompany.Size = new System.Drawing.Size(414, 21);
            this.lblSDCompany.TabIndex = 25;
            this.lblSDCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSDNewDeductees
            // 
            this.btnSDNewDeductees.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSDNewDeductees.Location = new System.Drawing.Point(239, 78);
            this.btnSDNewDeductees.Name = "btnSDNewDeductees";
            this.btnSDNewDeductees.Size = new System.Drawing.Size(36, 21);
            this.btnSDNewDeductees.TabIndex = 197;
            this.btnSDNewDeductees.Text = ". . .";
            this.btnSDNewDeductees.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSDNewDeductees.UseVisualStyleBackColor = true;
            this.btnSDNewDeductees.Click += new System.EventHandler(this.btnSDNewDeducteesFound_Click);
            // 
            // lblSDNewDeductees
            // 
            this.lblSDNewDeductees.BackColor = System.Drawing.SystemColors.Info;
            this.lblSDNewDeductees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDNewDeductees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDNewDeductees.Location = new System.Drawing.Point(169, 79);
            this.lblSDNewDeductees.Name = "lblSDNewDeductees";
            this.lblSDNewDeductees.Size = new System.Drawing.Size(69, 20);
            this.lblSDNewDeductees.TabIndex = 196;
            this.lblSDNewDeductees.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(23, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 13);
            this.label12.TabIndex = 195;
            this.label12.Text = "New Employees Found";
            // 
            // lblTotalEmployeeRecords
            // 
            this.lblTotalEmployeeRecords.BackColor = System.Drawing.SystemColors.Info;
            this.lblTotalEmployeeRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalEmployeeRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEmployeeRecords.Location = new System.Drawing.Point(169, 52);
            this.lblTotalEmployeeRecords.Name = "lblTotalEmployeeRecords";
            this.lblTotalEmployeeRecords.Size = new System.Drawing.Size(69, 20);
            this.lblTotalEmployeeRecords.TabIndex = 189;
            this.lblTotalEmployeeRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(23, 55);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(145, 13);
            this.label20.TabIndex = 187;
            this.label20.Text = "Total Employee Records";
            // 
            // lblSDTAN
            // 
            this.lblSDTAN.BackColor = System.Drawing.SystemColors.Info;
            this.lblSDTAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDTAN.Location = new System.Drawing.Point(87, 23);
            this.lblSDTAN.Name = "lblSDTAN";
            this.lblSDTAN.Size = new System.Drawing.Size(95, 21);
            this.lblSDTAN.TabIndex = 27;
            this.lblSDTAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(23, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 13);
            this.label24.TabIndex = 26;
            this.label24.Text = "TAN No.";
            // 
            // lblSDFinancialYear
            // 
            this.lblSDFinancialYear.BackColor = System.Drawing.SystemColors.Info;
            this.lblSDFinancialYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSDFinancialYear.Location = new System.Drawing.Point(773, 23);
            this.lblSDFinancialYear.Name = "lblSDFinancialYear";
            this.lblSDFinancialYear.Size = new System.Drawing.Size(61, 21);
            this.lblSDFinancialYear.TabIndex = 22;
            this.lblSDFinancialYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(679, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 13);
            this.label32.TabIndex = 16;
            this.label32.Text = "Tax Year";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(190, 26);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(58, 13);
            this.label33.TabIndex = 14;
            this.label33.Text = "Company";
            // 
            // tbpValidateCSVFile
            // 
            this.tbpValidateCSVFile.Controls.Add(this.label17);
            this.tbpValidateCSVFile.Controls.Add(this.grpCSVDeducteeWithoutChallan);
            this.tbpValidateCSVFile.Controls.Add(this.grpCSVImportType);
            this.tbpValidateCSVFile.Controls.Add(this.grpCSVProgress);
            this.tbpValidateCSVFile.Controls.Add(this.grpSelectCSVFile);
            this.tbpValidateCSVFile.Controls.Add(this.grpMainCSV);
            this.tbpValidateCSVFile.Location = new System.Drawing.Point(4, 22);
            this.tbpValidateCSVFile.Name = "tbpValidateCSVFile";
            this.tbpValidateCSVFile.Size = new System.Drawing.Size(982, 508);
            this.tbpValidateCSVFile.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(339, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(271, 13);
            this.label17.TabIndex = 156;
            this.label17.Text = "Make sure no comma (,) is present in the data.";
            // 
            // grpCSVDeducteeWithoutChallan
            // 
            this.grpCSVDeducteeWithoutChallan.Controls.Add(this.label14);
            this.grpCSVDeducteeWithoutChallan.Controls.Add(this.chkCsvEnterDeducteeDetailsOnly);
            this.grpCSVDeducteeWithoutChallan.Location = new System.Drawing.Point(241, 414);
            this.grpCSVDeducteeWithoutChallan.Name = "grpCSVDeducteeWithoutChallan";
            this.grpCSVDeducteeWithoutChallan.Size = new System.Drawing.Size(500, 61);
            this.grpCSVDeducteeWithoutChallan.TabIndex = 155;
            this.grpCSVDeducteeWithoutChallan.TabStop = false;
            this.grpCSVDeducteeWithoutChallan.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(31, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(438, 13);
            this.label14.TabIndex = 151;
            this.label14.Text = "Put 0 or blank in \'Challan Serial Reference\' cell of \'Deductee Details\' sheet.";
            // 
            // chkCsvEnterDeducteeDetailsOnly
            // 
            this.chkCsvEnterDeducteeDetailsOnly.AutoSize = true;
            this.chkCsvEnterDeducteeDetailsOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCsvEnterDeducteeDetailsOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCsvEnterDeducteeDetailsOnly.Location = new System.Drawing.Point(147, 12);
            this.chkCsvEnterDeducteeDetailsOnly.Name = "chkCsvEnterDeducteeDetailsOnly";
            this.chkCsvEnterDeducteeDetailsOnly.Size = new System.Drawing.Size(206, 17);
            this.chkCsvEnterDeducteeDetailsOnly.TabIndex = 149;
            this.chkCsvEnterDeducteeDetailsOnly.Text = "Enter Deductee without Challan";
            this.chkCsvEnterDeducteeDetailsOnly.UseVisualStyleBackColor = true;
            // 
            // grpCSVImportType
            // 
            this.grpCSVImportType.Controls.Add(this.label15);
            this.grpCSVImportType.Controls.Add(this.rbnCSVIncremental);
            this.grpCSVImportType.Controls.Add(this.rbnCSVNewImport);
            this.grpCSVImportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCSVImportType.ForeColor = System.Drawing.Color.Black;
            this.grpCSVImportType.Location = new System.Drawing.Point(310, 260);
            this.grpCSVImportType.Name = "grpCSVImportType";
            this.grpCSVImportType.Size = new System.Drawing.Size(363, 90);
            this.grpCSVImportType.TabIndex = 154;
            this.grpCSVImportType.TabStop = false;
            this.grpCSVImportType.Text = "Import Type";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(24, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(321, 13);
            this.label15.TabIndex = 149;
            this.label15.Text = "Note : In the CSV challan serial no. should start from 1.";
            // 
            // rbnCSVIncremental
            // 
            this.rbnCSVIncremental.AutoSize = true;
            this.rbnCSVIncremental.Location = new System.Drawing.Point(49, 43);
            this.rbnCSVIncremental.Name = "rbnCSVIncremental";
            this.rbnCSVIncremental.Size = new System.Drawing.Size(160, 17);
            this.rbnCSVIncremental.TabIndex = 1;
            this.rbnCSVIncremental.Text = "Add to the existing data";
            this.rbnCSVIncremental.UseVisualStyleBackColor = true;
            // 
            // rbnCSVNewImport
            // 
            this.rbnCSVNewImport.AutoSize = true;
            this.rbnCSVNewImport.Checked = true;
            this.rbnCSVNewImport.Location = new System.Drawing.Point(49, 19);
            this.rbnCSVNewImport.Name = "rbnCSVNewImport";
            this.rbnCSVNewImport.Size = new System.Drawing.Size(187, 17);
            this.rbnCSVNewImport.TabIndex = 0;
            this.rbnCSVNewImport.TabStop = true;
            this.rbnCSVNewImport.Text = "New / Replace existing data";
            this.rbnCSVNewImport.UseVisualStyleBackColor = true;
            // 
            // grpCSVProgress
            // 
            this.grpCSVProgress.Controls.Add(this.label16);
            this.grpCSVProgress.Controls.Add(this.prgBarCsv);
            this.grpCSVProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCSVProgress.ForeColor = System.Drawing.Color.Black;
            this.grpCSVProgress.Location = new System.Drawing.Point(117, 356);
            this.grpCSVProgress.Name = "grpCSVProgress";
            this.grpCSVProgress.Size = new System.Drawing.Size(744, 54);
            this.grpCSVProgress.TabIndex = 153;
            this.grpCSVProgress.TabStop = false;
            this.grpCSVProgress.Text = "Progress";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(19, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 148;
            this.label16.Text = "Excel file path";
            this.label16.Visible = false;
            // 
            // prgBarCsv
            // 
            this.prgBarCsv.Location = new System.Drawing.Point(16, 19);
            this.prgBarCsv.Name = "prgBarCsv";
            this.prgBarCsv.Size = new System.Drawing.Size(712, 10);
            this.prgBarCsv.TabIndex = 147;
            // 
            // grpSelectCSVFile
            // 
            this.grpSelectCSVFile.Controls.Add(this.btnSelectChallanCSVFile);
            this.grpSelectCSVFile.Controls.Add(this.txtChallanCSVFilePath);
            this.grpSelectCSVFile.Controls.Add(this.lblCSVChallanPathCaption);
            this.grpSelectCSVFile.Controls.Add(this.btnSelectDeducteeCSVFile);
            this.grpSelectCSVFile.Controls.Add(this.txtDeducteeCSVFilePath);
            this.grpSelectCSVFile.Controls.Add(this.lblCSVDeducteePathCaption);
            this.grpSelectCSVFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSelectCSVFile.ForeColor = System.Drawing.Color.Black;
            this.grpSelectCSVFile.Location = new System.Drawing.Point(117, 178);
            this.grpSelectCSVFile.Name = "grpSelectCSVFile";
            this.grpSelectCSVFile.Size = new System.Drawing.Size(749, 73);
            this.grpSelectCSVFile.TabIndex = 152;
            this.grpSelectCSVFile.TabStop = false;
            this.grpSelectCSVFile.Text = "Select CSV file";
            // 
            // btnSelectChallanCSVFile
            // 
            this.btnSelectChallanCSVFile.BackColor = System.Drawing.Color.Blue;
            this.btnSelectChallanCSVFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectChallanCSVFile.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectChallanCSVFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectChallanCSVFile.Location = new System.Drawing.Point(649, 16);
            this.btnSelectChallanCSVFile.Name = "btnSelectChallanCSVFile";
            this.btnSelectChallanCSVFile.Size = new System.Drawing.Size(44, 23);
            this.btnSelectChallanCSVFile.TabIndex = 149;
            this.btnSelectChallanCSVFile.Text = ". . .";
            this.btnSelectChallanCSVFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectChallanCSVFile.UseVisualStyleBackColor = false;
            this.btnSelectChallanCSVFile.Click += new System.EventHandler(this.btnSelectChallanCSVFile_Click);
            // 
            // txtChallanCSVFilePath
            // 
            this.txtChallanCSVFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtChallanCSVFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChallanCSVFilePath.Location = new System.Drawing.Point(168, 18);
            this.txtChallanCSVFilePath.MaxLength = 75;
            this.txtChallanCSVFilePath.Name = "txtChallanCSVFilePath";
            this.txtChallanCSVFilePath.ReadOnly = true;
            this.txtChallanCSVFilePath.Size = new System.Drawing.Size(479, 20);
            this.txtChallanCSVFilePath.TabIndex = 148;
            // 
            // lblCSVChallanPathCaption
            // 
            this.lblCSVChallanPathCaption.AutoSize = true;
            this.lblCSVChallanPathCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSVChallanPathCaption.Location = new System.Drawing.Point(37, 21);
            this.lblCSVChallanPathCaption.Name = "lblCSVChallanPathCaption";
            this.lblCSVChallanPathCaption.Size = new System.Drawing.Size(127, 13);
            this.lblCSVChallanPathCaption.TabIndex = 147;
            this.lblCSVChallanPathCaption.Text = "Challan CSV file path";
            // 
            // btnSelectDeducteeCSVFile
            // 
            this.btnSelectDeducteeCSVFile.BackColor = System.Drawing.Color.Blue;
            this.btnSelectDeducteeCSVFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectDeducteeCSVFile.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectDeducteeCSVFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectDeducteeCSVFile.Location = new System.Drawing.Point(649, 39);
            this.btnSelectDeducteeCSVFile.Name = "btnSelectDeducteeCSVFile";
            this.btnSelectDeducteeCSVFile.Size = new System.Drawing.Size(44, 23);
            this.btnSelectDeducteeCSVFile.TabIndex = 146;
            this.btnSelectDeducteeCSVFile.Text = ". . .";
            this.btnSelectDeducteeCSVFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectDeducteeCSVFile.UseVisualStyleBackColor = false;
            this.btnSelectDeducteeCSVFile.Click += new System.EventHandler(this.btnSelectDeducteeCSVFile_Click);
            // 
            // txtDeducteeCSVFilePath
            // 
            this.txtDeducteeCSVFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtDeducteeCSVFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeCSVFilePath.Location = new System.Drawing.Point(168, 41);
            this.txtDeducteeCSVFilePath.MaxLength = 75;
            this.txtDeducteeCSVFilePath.Name = "txtDeducteeCSVFilePath";
            this.txtDeducteeCSVFilePath.ReadOnly = true;
            this.txtDeducteeCSVFilePath.Size = new System.Drawing.Size(479, 20);
            this.txtDeducteeCSVFilePath.TabIndex = 1;
            // 
            // lblCSVDeducteePathCaption
            // 
            this.lblCSVDeducteePathCaption.AutoSize = true;
            this.lblCSVDeducteePathCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSVDeducteePathCaption.Location = new System.Drawing.Point(24, 44);
            this.lblCSVDeducteePathCaption.Name = "lblCSVDeducteePathCaption";
            this.lblCSVDeducteePathCaption.Size = new System.Drawing.Size(140, 13);
            this.lblCSVDeducteePathCaption.TabIndex = 0;
            this.lblCSVDeducteePathCaption.Text = "Deductee CSV file path";
            // 
            // grpMainCSV
            // 
            this.grpMainCSV.Controls.Add(this.lblTaxComputingMessageF24QSDCSV);
            this.grpMainCSV.Controls.Add(this.lnkCSVBlankFormat);
            this.grpMainCSV.Controls.Add(this.lnkCSVSearchByTAN);
            this.grpMainCSV.Controls.Add(this.txtCSVDeductorType);
            this.grpMainCSV.Controls.Add(this.cmbCSVFormNo);
            this.grpMainCSV.Controls.Add(this.label18);
            this.grpMainCSV.Controls.Add(this.cmbCSVQtr);
            this.grpMainCSV.Controls.Add(this.label19);
            this.grpMainCSV.Controls.Add(this.cmbCSVFinancialYear);
            this.grpMainCSV.Controls.Add(this.label21);
            this.grpMainCSV.Controls.Add(this.cmbCSVCompany);
            this.grpMainCSV.Controls.Add(this.label22);
            this.grpMainCSV.Controls.Add(this.chkCSVRoundOffTaxableAmount);
            this.grpMainCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMainCSV.ForeColor = System.Drawing.Color.Black;
            this.grpMainCSV.Location = new System.Drawing.Point(117, 26);
            this.grpMainCSV.Name = "grpMainCSV";
            this.grpMainCSV.Size = new System.Drawing.Size(749, 150);
            this.grpMainCSV.TabIndex = 151;
            this.grpMainCSV.TabStop = false;
            this.grpMainCSV.Text = "Select";
            // 
            // lblTaxComputingMessageF24QSDCSV
            // 
            this.lblTaxComputingMessageF24QSDCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxComputingMessageF24QSDCSV.ForeColor = System.Drawing.Color.Blue;
            this.lblTaxComputingMessageF24QSDCSV.Location = new System.Drawing.Point(4, 117);
            this.lblTaxComputingMessageF24QSDCSV.Name = "lblTaxComputingMessageF24QSDCSV";
            this.lblTaxComputingMessageF24QSDCSV.Size = new System.Drawing.Size(740, 27);
            this.lblTaxComputingMessageF24QSDCSV.TabIndex = 218;
            this.lblTaxComputingMessageF24QSDCSV.Text = resources.GetString("lblTaxComputingMessageF24QSDCSV.Text");
            this.lblTaxComputingMessageF24QSDCSV.Visible = false;
            // 
            // lnkCSVBlankFormat
            // 
            this.lnkCSVBlankFormat.AutoSize = true;
            this.lnkCSVBlankFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCSVBlankFormat.Location = new System.Drawing.Point(384, 74);
            this.lnkCSVBlankFormat.Name = "lnkCSVBlankFormat";
            this.lnkCSVBlankFormat.Size = new System.Drawing.Size(97, 13);
            this.lnkCSVBlankFormat.TabIndex = 217;
            this.lnkCSVBlankFormat.TabStop = true;
            this.lnkCSVBlankFormat.Text = "Get CSV Format";
            this.lnkCSVBlankFormat.Visible = false;
            this.lnkCSVBlankFormat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCSVBlankFormat_LinkClicked);
            // 
            // lnkCSVSearchByTAN
            // 
            this.lnkCSVSearchByTAN.AutoSize = true;
            this.lnkCSVSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCSVSearchByTAN.Location = new System.Drawing.Point(651, 92);
            this.lnkCSVSearchByTAN.Name = "lnkCSVSearchByTAN";
            this.lnkCSVSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkCSVSearchByTAN.TabIndex = 215;
            this.lnkCSVSearchByTAN.TabStop = true;
            this.lnkCSVSearchByTAN.Text = "Search by TAN";
            this.lnkCSVSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // txtCSVDeductorType
            // 
            this.txtCSVDeductorType.Location = new System.Drawing.Point(262, 42);
            this.txtCSVDeductorType.Name = "txtCSVDeductorType";
            this.txtCSVDeductorType.Size = new System.Drawing.Size(24, 20);
            this.txtCSVDeductorType.TabIndex = 21;
            this.txtCSVDeductorType.Visible = false;
            // 
            // cmbCSVFormNo
            // 
            this.cmbCSVFormNo.BackColor = System.Drawing.Color.White;
            this.cmbCSVFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSVFormNo.FormattingEnabled = true;
            this.cmbCSVFormNo.Location = new System.Drawing.Point(168, 64);
            this.cmbCSVFormNo.Name = "cmbCSVFormNo";
            this.cmbCSVFormNo.Size = new System.Drawing.Size(210, 21);
            this.cmbCSVFormNo.TabIndex = 2;
            this.cmbCSVFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbCSVFormNo_SelectedIndexChanged);
            this.cmbCSVFormNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCSVFormNo_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(65, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Select Form No.";
            // 
            // cmbCSVQtr
            // 
            this.cmbCSVQtr.BackColor = System.Drawing.Color.White;
            this.cmbCSVQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSVQtr.FormattingEnabled = true;
            this.cmbCSVQtr.Location = new System.Drawing.Point(168, 39);
            this.cmbCSVQtr.Name = "cmbCSVQtr";
            this.cmbCSVQtr.Size = new System.Drawing.Size(49, 21);
            this.cmbCSVQtr.TabIndex = 1;
            this.cmbCSVQtr.SelectedIndexChanged += new System.EventHandler(this.cmbCSVQuarter_SelectedIndexChanged);
            this.cmbCSVQtr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCSVQuarter_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(74, 42);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Select Quarter";
            // 
            // cmbCSVFinancialYear
            // 
            this.cmbCSVFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbCSVFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSVFinancialYear.FormattingEnabled = true;
            this.cmbCSVFinancialYear.Location = new System.Drawing.Point(168, 15);
            this.cmbCSVFinancialYear.Name = "cmbCSVFinancialYear";
            this.cmbCSVFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbCSVFinancialYear.TabIndex = 0;
            this.cmbCSVFinancialYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCSVFinancialYear_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(35, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Select Tax Year";
            // 
            // cmbCSVCompany
            // 
            this.cmbCSVCompany.BackColor = System.Drawing.Color.White;
            this.cmbCSVCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSVCompany.FormattingEnabled = true;
            this.cmbCSVCompany.Location = new System.Drawing.Point(168, 89);
            this.cmbCSVCompany.Name = "cmbCSVCompany";
            this.cmbCSVCompany.Size = new System.Drawing.Size(479, 21);
            this.cmbCSVCompany.TabIndex = 3;
            this.cmbCSVCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCSVCompany_SelectedIndexChanged);
            this.cmbCSVCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCSVCompany_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(65, 92);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 13);
            this.label22.TabIndex = 14;
            this.label22.Text = "Select Company";
            // 
            // chkCSVRoundOffTaxableAmount
            // 
            this.chkCSVRoundOffTaxableAmount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCSVRoundOffTaxableAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCSVRoundOffTaxableAmount.Location = new System.Drawing.Point(383, 29);
            this.chkCSVRoundOffTaxableAmount.Name = "chkCSVRoundOffTaxableAmount";
            this.chkCSVRoundOffTaxableAmount.Size = new System.Drawing.Size(362, 42);
            this.chkCSVRoundOffTaxableAmount.TabIndex = 216;
            this.chkCSVRoundOffTaxableAmount.Text = "Total Taxable Income will not be validated - as Rounding off is enabled in Prefer" +
    "ences (no. 21).";
            this.chkCSVRoundOffTaxableAmount.UseVisualStyleBackColor = true;
            this.chkCSVRoundOffTaxableAmount.Visible = false;
            // 
            // lblHide
            // 
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(20, 48);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(195, 21);
            this.lblHide.TabIndex = 50;
            // 
            // tmrLoginRefresh
            // 
            this.tmrLoginRefresh.Interval = 5000;
            this.tmrLoginRefresh.Tick += new System.EventHandler(this.tmrLoginRefresh_Tick);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 212;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Visible = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(956, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 213;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Manual Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // TrnExcelImportIncremental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 655);
            this.Controls.Add(this.lblHide);
            this.Name = "TrnExcelImportIncremental";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrnExcelImport_FormClosing);
            this.Load += new System.EventHandler(this.TrnExcelImport_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.lblHide, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.tbcExcelImport.ResumeLayout(false);
            this.tbpValidateExcelFile.ResumeLayout(false);
            this.grpEnterDeducteeDetailsOnly.ResumeLayout(false);
            this.grpEnterDeducteeDetailsOnly.PerformLayout();
            this.grpImportType.ResumeLayout(false);
            this.grpImportType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tbpImportExcelFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewDeductee)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tbpImportSalaryDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewSD)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tbpValidateCSVFile.ResumeLayout(false);
            this.tbpValidateCSVFile.PerformLayout();
            this.grpCSVDeducteeWithoutChallan.ResumeLayout(false);
            this.grpCSVDeducteeWithoutChallan.PerformLayout();
            this.grpCSVImportType.ResumeLayout(false);
            this.grpCSVImportType.PerformLayout();
            this.grpCSVProgress.ResumeLayout(false);
            this.grpCSVProgress.PerformLayout();
            this.grpSelectCSVFile.ResumeLayout(false);
            this.grpSelectCSVFile.PerformLayout();
            this.grpMainCSV.ResumeLayout(false);
            this.grpMainCSV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.TabControl tbcExcelImport;
        private System.Windows.Forms.TabPage tbpValidateExcelFile;
        private System.Windows.Forms.TabPage tbpImportExcelFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkColorCodingExcelsheet;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.TextBox txtDeductorType;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblFormNo;
        private System.Windows.Forms.Label lblQtr;
        private System.Windows.Forms.Label lblFinancialYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMessageLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar prgImportBar;
        private System.Windows.Forms.Label lblProgressDisplayMessage;
        private System.Windows.Forms.Label lblTANNo;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.Label lblNewDeducteesFound;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOpenNewDeducteesFound;
        private System.Windows.Forms.TabPage tbpImportSalaryDetails;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSDNewDeductees;
        private System.Windows.Forms.Label lblSDNewDeductees;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotalEmployeeRecords;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblSDTAN;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblSDFinancialYear;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ProgressBar prgSDBar;
        private System.Windows.Forms.Timer tmrLoginRefresh;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSDCompany;
        private System.Windows.Forms.GroupBox grpImportType;
        private System.Windows.Forms.RadioButton rbnIncremental;
        private System.Windows.Forms.RadioButton rbnNewImport;
        private System.Windows.Forms.Label lblNoteAdd;
        private System.Windows.Forms.Label lblToolTip;
        private System.Windows.Forms.Label lblSDFormNo;
        private System.Windows.Forms.Button btnBulkPANValidation;
        private System.Windows.Forms.GroupBox grpEnterDeducteeDetailsOnly;
        private System.Windows.Forms.CheckBox chkEnterDeducteeDetailsOnly;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.CheckBox chkRoundOffTaxableAmount;
        private System.Windows.Forms.TabPage tbpValidateCSVFile;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox grpCSVDeducteeWithoutChallan;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkCsvEnterDeducteeDetailsOnly;
        private System.Windows.Forms.GroupBox grpCSVImportType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbnCSVIncremental;
        private System.Windows.Forms.RadioButton rbnCSVNewImport;
        private System.Windows.Forms.GroupBox grpCSVProgress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ProgressBar prgBarCsv;
        private System.Windows.Forms.GroupBox grpSelectCSVFile;
        private System.Windows.Forms.Button btnSelectChallanCSVFile;
        private System.Windows.Forms.TextBox txtChallanCSVFilePath;
        private System.Windows.Forms.Label lblCSVChallanPathCaption;
        private System.Windows.Forms.Button btnSelectDeducteeCSVFile;
        private System.Windows.Forms.TextBox txtDeducteeCSVFilePath;
        private System.Windows.Forms.Label lblCSVDeducteePathCaption;
        private System.Windows.Forms.GroupBox grpMainCSV;
        private System.Windows.Forms.LinkLabel lnkCSVBlankFormat;
        private System.Windows.Forms.LinkLabel lnkCSVSearchByTAN;
        private System.Windows.Forms.TextBox txtCSVDeductorType;
        private System.Windows.Forms.ComboBox cmbCSVFormNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbCSVQtr;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbCSVFinancialYear;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbCSVCompany;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chkCSVRoundOffTaxableAmount;
        private System.Windows.Forms.Label lblFinancialYearId;
        private DGVControl.DGVControl dgcViewChallan;
        private System.Windows.Forms.Label lblChallanGridCaption;
        private DGVControl.DGVControl dgcViewDeductee;
        private System.Windows.Forms.Label lblDeducteeGridCaption;
        private DGVControl.DGVControl dgcViewSD;
        private System.Windows.Forms.Label lblSDGridCaption;
        private System.Windows.Forms.LinkLabel lnkVerifyOnlyNewPANs;
        private System.Windows.Forms.LinkLabel lnkVerifyAllPANs;
        private System.Windows.Forms.LinkLabel lnkVerifyAllPANsSD;
        private System.Windows.Forms.LinkLabel lnkVerifyOnlyNewPANsSD;
        private System.Windows.Forms.Label lblTaxComputingMessageF24QSDExcel;
        private System.Windows.Forms.Label lblTaxComputingMessageF24QSDCSV;
    }
}
