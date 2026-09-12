namespace TDSMAN.FormWeb
{
    partial class TrnChallanStatusTracesRemoved
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnChallanStatusTraces));
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.grpConsumptionDetails = new System.Windows.Forms.GroupBox();
            this.dgvConsumption = new System.Windows.Forms.DataGridView();
            this.grpChallanDetails = new System.Windows.Forms.GroupBox();
            this.dgvStatementList = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mskChallanDate = new System.Windows.Forms.MaskedTextBox();
            this.txtChallanAmount = new System.Windows.Forms.TextBox();
            this.txtChallanSerialNo = new System.Windows.Forms.TextBox();
            this.txtBSRCode = new System.Windows.Forms.TextBox();
            this.btnSearchOpt2Go = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.mskChallanToDate = new System.Windows.Forms.MaskedTextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.mskChallanFromDate = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSearchOpt1Go = new System.Windows.Forms.Button();
            this.cmbChallanStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpConsumptionDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsumption)).BeginInit();
            this.grpChallanDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.grpLoginDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 666);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(39, 13);
            this.BtnCancel.Size = new System.Drawing.Size(10, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Login";
            this.BtnSave.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(23, 13);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(6, 13);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Click += new System.EventHandler(this.btnLoginCancel_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(86, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(70, 13);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(54, 13);
            this.BtnSearch.Size = new System.Drawing.Size(10, 23);
            this.BtnSearch.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1036, 609);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.grpConsumptionDetails);
            this.grpDownloadList.Controls.Add(this.grpChallanDetails);
            this.grpDownloadList.Controls.Add(this.groupBox3);
            this.grpDownloadList.Controls.Add(this.groupBox2);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 4);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 533);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // grpConsumptionDetails
            // 
            this.grpConsumptionDetails.Controls.Add(this.dgvConsumption);
            this.grpConsumptionDetails.Location = new System.Drawing.Point(17, 355);
            this.grpConsumptionDetails.Name = "grpConsumptionDetails";
            this.grpConsumptionDetails.Size = new System.Drawing.Size(914, 123);
            this.grpConsumptionDetails.TabIndex = 224;
            this.grpConsumptionDetails.TabStop = false;
            this.grpConsumptionDetails.Text = "Consumption Details";
            // 
            // dgvConsumption
            // 
            this.dgvConsumption.AllowUserToAddRows = false;
            this.dgvConsumption.AllowUserToDeleteRows = false;
            this.dgvConsumption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsumption.Location = new System.Drawing.Point(9, 16);
            this.dgvConsumption.Name = "dgvConsumption";
            this.dgvConsumption.ReadOnly = true;
            this.dgvConsumption.Size = new System.Drawing.Size(898, 98);
            this.dgvConsumption.TabIndex = 198;
            // 
            // grpChallanDetails
            // 
            this.grpChallanDetails.Controls.Add(this.dgvStatementList);
            this.grpChallanDetails.Location = new System.Drawing.Point(17, 146);
            this.grpChallanDetails.Name = "grpChallanDetails";
            this.grpChallanDetails.Size = new System.Drawing.Size(914, 184);
            this.grpChallanDetails.TabIndex = 223;
            this.grpChallanDetails.TabStop = false;
            this.grpChallanDetails.Text = "Challan Details";
            // 
            // dgvStatementList
            // 
            this.dgvStatementList.AllowUserToAddRows = false;
            this.dgvStatementList.AllowUserToDeleteRows = false;
            this.dgvStatementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementList.Location = new System.Drawing.Point(7, 18);
            this.dgvStatementList.Name = "dgvStatementList";
            this.dgvStatementList.ReadOnly = true;
            this.dgvStatementList.Size = new System.Drawing.Size(898, 156);
            this.dgvStatementList.TabIndex = 14;
            this.dgvStatementList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatementList_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.mskChallanDate);
            this.groupBox3.Controls.Add(this.txtChallanAmount);
            this.groupBox3.Controls.Add(this.txtChallanSerialNo);
            this.groupBox3.Controls.Add(this.txtBSRCode);
            this.groupBox3.Controls.Add(this.btnSearchOpt2Go);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(17, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(911, 58);
            this.groupBox3.TabIndex = 197;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Option 2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(300, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 15);
            this.label13.TabIndex = 224;
            this.label13.Text = "DD/MM/YYYY";
            // 
            // mskChallanDate
            // 
            this.mskChallanDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskChallanDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskChallanDate.Location = new System.Drawing.Point(297, 25);
            this.mskChallanDate.Mask = "00/00/0000";
            this.mskChallanDate.Name = "mskChallanDate";
            this.mskChallanDate.Size = new System.Drawing.Size(86, 20);
            this.mskChallanDate.TabIndex = 223;
            this.mskChallanDate.ValidatingType = typeof(System.DateTime);
            // 
            // txtChallanAmount
            // 
            this.txtChallanAmount.Location = new System.Drawing.Point(749, 24);
            this.txtChallanAmount.MaxLength = 50;
            this.txtChallanAmount.Name = "txtChallanAmount";
            this.txtChallanAmount.Size = new System.Drawing.Size(80, 20);
            this.txtChallanAmount.TabIndex = 218;
            // 
            // txtChallanSerialNo
            // 
            this.txtChallanSerialNo.Location = new System.Drawing.Point(525, 24);
            this.txtChallanSerialNo.MaxLength = 50;
            this.txtChallanSerialNo.Name = "txtChallanSerialNo";
            this.txtChallanSerialNo.Size = new System.Drawing.Size(103, 20);
            this.txtChallanSerialNo.TabIndex = 217;
            // 
            // txtBSRCode
            // 
            this.txtBSRCode.Location = new System.Drawing.Point(84, 24);
            this.txtBSRCode.MaxLength = 50;
            this.txtBSRCode.Name = "txtBSRCode";
            this.txtBSRCode.Size = new System.Drawing.Size(109, 20);
            this.txtBSRCode.TabIndex = 215;
            // 
            // btnSearchOpt2Go
            // 
            this.btnSearchOpt2Go.Location = new System.Drawing.Point(834, 24);
            this.btnSearchOpt2Go.Name = "btnSearchOpt2Go";
            this.btnSearchOpt2Go.Size = new System.Drawing.Size(36, 21);
            this.btnSearchOpt2Go.TabIndex = 211;
            this.btnSearchOpt2Go.Text = "Go";
            this.btnSearchOpt2Go.UseVisualStyleBackColor = true;
            this.btnSearchOpt2Go.Click += new System.EventHandler(this.btnSearchOpt2Go_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(639, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Challan Amount (Rs. )";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(411, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Challan Serial Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Date Of Deposit";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "BSR Code";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.mskChallanToDate);
            this.groupBox2.Controls.Add(this.label53);
            this.groupBox2.Controls.Add(this.mskChallanFromDate);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnSearchOpt1Go);
            this.groupBox2.Controls.Add(this.cmbChallanStatus);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(17, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(910, 50);
            this.groupBox2.TabIndex = 196;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Option 1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(495, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 15);
            this.label12.TabIndex = 225;
            this.label12.Text = "DD/MM/YYYY";
            // 
            // mskChallanToDate
            // 
            this.mskChallanToDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskChallanToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskChallanToDate.Location = new System.Drawing.Point(405, 21);
            this.mskChallanToDate.Mask = "00/00/0000";
            this.mskChallanToDate.Name = "mskChallanToDate";
            this.mskChallanToDate.Size = new System.Drawing.Size(86, 20);
            this.mskChallanToDate.TabIndex = 224;
            this.mskChallanToDate.ValidatingType = typeof(System.DateTime);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Blue;
            this.label53.Location = new System.Drawing.Point(300, 25);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(77, 15);
            this.label53.TabIndex = 223;
            this.label53.Text = "DD/MM/YYYY";
            // 
            // mskChallanFromDate
            // 
            this.mskChallanFromDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskChallanFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskChallanFromDate.Location = new System.Drawing.Point(208, 21);
            this.mskChallanFromDate.Mask = "00/00/0000";
            this.mskChallanFromDate.Name = "mskChallanFromDate";
            this.mskChallanFromDate.Size = new System.Drawing.Size(86, 20);
            this.mskChallanFromDate.TabIndex = 222;
            this.mskChallanFromDate.ValidatingType = typeof(System.DateTime);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(172, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 212;
            this.label11.Text = "From";
            // 
            // btnSearchOpt1Go
            // 
            this.btnSearchOpt1Go.Location = new System.Drawing.Point(756, 20);
            this.btnSearchOpt1Go.Name = "btnSearchOpt1Go";
            this.btnSearchOpt1Go.Size = new System.Drawing.Size(35, 23);
            this.btnSearchOpt1Go.TabIndex = 211;
            this.btnSearchOpt1Go.Text = "Go";
            this.btnSearchOpt1Go.UseVisualStyleBackColor = true;
            this.btnSearchOpt1Go.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbChallanStatus
            // 
            this.cmbChallanStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChallanStatus.FormattingEnabled = true;
            this.cmbChallanStatus.Location = new System.Drawing.Point(653, 21);
            this.cmbChallanStatus.Name = "cmbChallanStatus";
            this.cmbChallanStatus.Size = new System.Drawing.Size(92, 21);
            this.cmbChallanStatus.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(576, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Challan Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(383, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "To";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Challan Deposit Date";
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(897, 9);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 195;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(124, 490);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(670, 49);
            this.grpProgress.TabIndex = 214;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 22);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 16);
            this.pBar.TabIndex = 8;
            // 
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(100, 168);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 293);
            this.grpLoginDetails.TabIndex = 50;
            this.grpLoginDetails.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTANNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(59, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Login Details";
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(77, 18);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.Size = new System.Drawing.Size(96, 20);
            this.txtTANNo.TabIndex = 0;
            this.txtTANNo.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtTANNo.Leave += new System.EventHandler(this.txtTAN_Leave);
            this.txtTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(253, 18);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(145, 20);
            this.txtUserID.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(484, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(199, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 206;
            this.label1.Text = "User ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(188, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 200;
            this.label3.Text = "Enter text as in above image";
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(526, 134);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 199;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(334, 119);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(187, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(362, 188);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // pgTimerGrid
            // 
            this.pgTimerGrid.Tick += new System.EventHandler(this.pgTimerGrid_Tick);
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(236, 227);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 20);
            this.lstDeducteeHelp.TabIndex = 216;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            // 
            // TrnChallanStatusTraces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 575);
            this.Controls.Add(this.lstDeducteeHelp);
            this.Controls.Add(this.grpLoginDetails);
            this.Name = "TrnChallanStatusTraces";
            this.Load += new System.EventHandler(this.TrnChallanStatusTraces_Load);
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
            this.Controls.SetChildIndex(this.grpLoginDetails, 0);
            this.Controls.SetChildIndex(this.lstDeducteeHelp, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpConsumptionDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsumption)).EndInit();
            this.grpChallanDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpProgress.ResumeLayout(false);
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbChallanStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearchOpt1Go;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSearchOpt2Go;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBSRCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtChallanAmount;
        private System.Windows.Forms.TextBox txtChallanSerialNo;
        private System.Windows.Forms.DataGridView dgvConsumption;
        private System.Windows.Forms.GroupBox grpConsumptionDetails;
        private System.Windows.Forms.GroupBox grpChallanDetails;
        private System.Windows.Forms.DataGridView dgvStatementList;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox mskChallanToDate;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.MaskedTextBox mskChallanFromDate;
        private System.Windows.Forms.MaskedTextBox mskChallanDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
    }
}
