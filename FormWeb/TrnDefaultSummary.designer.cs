namespace TDSMAN.FormWeb
{
    partial class TrnDefaultSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDefaultSummary));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.grpSummaryDetails = new System.Windows.Forms.GroupBox();
            this.btnPrintDetails = new System.Windows.Forms.Button();
            this.lblSelectedTanNo = new System.Windows.Forms.Label();
            this.lblSelectedQtr = new System.Windows.Forms.Label();
            this.lblSelectedFormNo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSelectedFaYear = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPanError = new System.Windows.Forms.DataGridView();
            this.grpConsumptionDetails = new System.Windows.Forms.GroupBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalPayable = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvDefaultSummaryDetails = new System.Windows.Forms.DataGridView();
            this.lblNetPayable = new System.Windows.Forms.Label();
            this.lblCorrectionCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvStatement = new System.Windows.Forms.DataGridView();
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
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.dgvDefaultSummary = new System.Windows.Forms.DataGridView();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.btnPrintDefaultSummary = new System.Windows.Forms.Button();
            this.grpDefaultSummaryList = new System.Windows.Forms.GroupBox();
            this.lblTanNo = new System.Windows.Forms.Label();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpProgress.SuspendLayout();
            this.grpSummaryDetails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPanError)).BeginInit();
            this.grpConsumptionDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaultSummaryDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).BeginInit();
            this.grpLoginDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaultSummary)).BeginInit();
            this.grpDefaultSummaryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(213, 623);
            this.grpSort.Size = new System.Drawing.Size(32, 11);
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
            this.grpSearch.Location = new System.Drawing.Point(113, 621);
            this.grpSearch.Size = new System.Drawing.Size(45, 21);
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
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(963, 17);
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
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lstDeducteeHelp);
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpSummaryDetails);
            this.pnlControls.Controls.Add(this.grpDefaultSummaryList);
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Size = new System.Drawing.Size(1036, 610);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
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
            this.grpProgress.Location = new System.Drawing.Point(124, 486);
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
            // pgTimerGrid
            // 
            this.pgTimerGrid.Tick += new System.EventHandler(this.pgTimerGrid_Tick);
            // 
            // grpSummaryDetails
            // 
            this.grpSummaryDetails.Controls.Add(this.btnPrintDetails);
            this.grpSummaryDetails.Controls.Add(this.lblSelectedTanNo);
            this.grpSummaryDetails.Controls.Add(this.lblSelectedQtr);
            this.grpSummaryDetails.Controls.Add(this.lblSelectedFormNo);
            this.grpSummaryDetails.Controls.Add(this.panel1);
            this.grpSummaryDetails.Controls.Add(this.lblSelectedFaYear);
            this.grpSummaryDetails.Controls.Add(this.btnBack);
            this.grpSummaryDetails.Controls.Add(this.groupBox2);
            this.grpSummaryDetails.Controls.Add(this.grpConsumptionDetails);
            this.grpSummaryDetails.Controls.Add(this.lblNetPayable);
            this.grpSummaryDetails.Controls.Add(this.lblCorrectionCount);
            this.grpSummaryDetails.Controls.Add(this.label6);
            this.grpSummaryDetails.Controls.Add(this.label4);
            this.grpSummaryDetails.Controls.Add(this.dgvStatement);
            this.grpSummaryDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSummaryDetails.Location = new System.Drawing.Point(48, 6);
            this.grpSummaryDetails.Name = "grpSummaryDetails";
            this.grpSummaryDetails.Size = new System.Drawing.Size(949, 544);
            this.grpSummaryDetails.TabIndex = 215;
            this.grpSummaryDetails.TabStop = false;
            this.grpSummaryDetails.Tag = "";
            this.grpSummaryDetails.Text = "Summary Details";
            this.grpSummaryDetails.Visible = false;
            // 
            // btnPrintDetails
            // 
            this.btnPrintDetails.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintDetails.Location = new System.Drawing.Point(864, 514);
            this.btnPrintDetails.Name = "btnPrintDetails";
            this.btnPrintDetails.Size = new System.Drawing.Size(75, 21);
            this.btnPrintDetails.TabIndex = 237;
            this.btnPrintDetails.Text = "Print";
            this.btnPrintDetails.UseVisualStyleBackColor = false;
            this.btnPrintDetails.Click += new System.EventHandler(this.btnPrintDetails_Click);
            // 
            // lblSelectedTanNo
            // 
            this.lblSelectedTanNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedTanNo.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedTanNo.Location = new System.Drawing.Point(34, 16);
            this.lblSelectedTanNo.Name = "lblSelectedTanNo";
            this.lblSelectedTanNo.Size = new System.Drawing.Size(305, 13);
            this.lblSelectedTanNo.TabIndex = 250;
            this.lblSelectedTanNo.Text = "lblSelectedTanNo";
            // 
            // lblSelectedQtr
            // 
            this.lblSelectedQtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedQtr.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedQtr.Location = new System.Drawing.Point(580, 16);
            this.lblSelectedQtr.Name = "lblSelectedQtr";
            this.lblSelectedQtr.Size = new System.Drawing.Size(127, 13);
            this.lblSelectedQtr.TabIndex = 241;
            this.lblSelectedQtr.Text = "lblSelectedQtr";
            // 
            // lblSelectedFormNo
            // 
            this.lblSelectedFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFormNo.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedFormNo.Location = new System.Drawing.Point(764, 16);
            this.lblSelectedFormNo.Name = "lblSelectedFormNo";
            this.lblSelectedFormNo.Size = new System.Drawing.Size(127, 13);
            this.lblSelectedFormNo.TabIndex = 240;
            this.lblSelectedFormNo.Text = "lblSelectedFormNo";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 1);
            this.panel1.TabIndex = 239;
            // 
            // lblSelectedFaYear
            // 
            this.lblSelectedFaYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFaYear.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedFaYear.Location = new System.Drawing.Point(396, 16);
            this.lblSelectedFaYear.Name = "lblSelectedFaYear";
            this.lblSelectedFaYear.Size = new System.Drawing.Size(127, 13);
            this.lblSelectedFaYear.TabIndex = 238;
            this.lblSelectedFaYear.Text = "lblSelectedFaYear";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Lavender;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(437, 514);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 21);
            this.btnBack.TabIndex = 238;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPanError);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(25, 446);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(914, 66);
            this.groupBox2.TabIndex = 236;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Summary of PAN Errors";
            // 
            // dgvPanError
            // 
            this.dgvPanError.AllowUserToAddRows = false;
            this.dgvPanError.AllowUserToDeleteRows = false;
            this.dgvPanError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPanError.Location = new System.Drawing.Point(10, 16);
            this.dgvPanError.Name = "dgvPanError";
            this.dgvPanError.ReadOnly = true;
            this.dgvPanError.Size = new System.Drawing.Size(896, 45);
            this.dgvPanError.TabIndex = 226;
            // 
            // grpConsumptionDetails
            // 
            this.grpConsumptionDetails.Controls.Add(this.lblNetAmount);
            this.grpConsumptionDetails.Controls.Add(this.lblTotalPayable);
            this.grpConsumptionDetails.Controls.Add(this.label8);
            this.grpConsumptionDetails.Controls.Add(this.label7);
            this.grpConsumptionDetails.Controls.Add(this.dgvDefaultSummaryDetails);
            this.grpConsumptionDetails.Location = new System.Drawing.Point(24, 111);
            this.grpConsumptionDetails.Name = "grpConsumptionDetails";
            this.grpConsumptionDetails.Size = new System.Drawing.Size(914, 334);
            this.grpConsumptionDetails.TabIndex = 235;
            this.grpConsumptionDetails.TabStop = false;
            this.grpConsumptionDetails.Text = "Default Summary Details";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(803, 313);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(80, 13);
            this.lblNetAmount.TabIndex = 244;
            this.lblNetAmount.Text = "Net Payable ";
            // 
            // lblTotalPayable
            // 
            this.lblTotalPayable.AutoSize = true;
            this.lblTotalPayable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPayable.Location = new System.Drawing.Point(803, 292);
            this.lblTotalPayable.Name = "lblTotalPayable";
            this.lblTotalPayable.Size = new System.Drawing.Size(81, 13);
            this.lblTotalPayable.TabIndex = 243;
            this.lblTotalPayable.Text = "TotalPayable";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(397, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 13);
            this.label8.TabIndex = 242;
            this.label8.Text = "Net Payable (Rounded-Off) (Rs.)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(395, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 241;
            this.label7.Text = "Total Payable (Rs.)";
            // 
            // dgvDefaultSummaryDetails
            // 
            this.dgvDefaultSummaryDetails.AllowUserToAddRows = false;
            this.dgvDefaultSummaryDetails.AllowUserToDeleteRows = false;
            this.dgvDefaultSummaryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefaultSummaryDetails.Location = new System.Drawing.Point(9, 16);
            this.dgvDefaultSummaryDetails.Name = "dgvDefaultSummaryDetails";
            this.dgvDefaultSummaryDetails.ReadOnly = true;
            this.dgvDefaultSummaryDetails.Size = new System.Drawing.Size(898, 271);
            this.dgvDefaultSummaryDetails.TabIndex = 198;
            // 
            // lblNetPayable
            // 
            this.lblNetPayable.AutoSize = true;
            this.lblNetPayable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetPayable.Location = new System.Drawing.Point(840, 68);
            this.lblNetPayable.Name = "lblNetPayable";
            this.lblNetPayable.Size = new System.Drawing.Size(72, 13);
            this.lblNetPayable.TabIndex = 234;
            this.lblNetPayable.Text = "NetPayable";
            // 
            // lblCorrectionCount
            // 
            this.lblCorrectionCount.AutoSize = true;
            this.lblCorrectionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrectionCount.Location = new System.Drawing.Point(840, 45);
            this.lblCorrectionCount.Name = "lblCorrectionCount";
            this.lblCorrectionCount.Size = new System.Drawing.Size(98, 13);
            this.lblCorrectionCount.TabIndex = 233;
            this.lblCorrectionCount.Text = "CorrectionCount";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(638, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 13);
            this.label6.TabIndex = 232;
            this.label6.Text = "Net Payable (Rounded-Off) (Rs.) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(637, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 13);
            this.label4.TabIndex = 231;
            this.label4.Text = "Count of Correction Statement(s) :";
            // 
            // dgvStatement
            // 
            this.dgvStatement.AllowUserToAddRows = false;
            this.dgvStatement.AllowUserToDeleteRows = false;
            this.dgvStatement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatement.Location = new System.Drawing.Point(37, 42);
            this.dgvStatement.Name = "dgvStatement";
            this.dgvStatement.ReadOnly = true;
            this.dgvStatement.Size = new System.Drawing.Size(593, 63);
            this.dgvStatement.TabIndex = 230;
            // 
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(104, 125);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 225);
            this.grpLoginDetails.TabIndex = 217;
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
            this.groupBox1.Text = "Enter TRACES Login Details";
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
            this.txtTANNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTANNo.Leave += new System.EventHandler(this.txtTAN_Leave);
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
            this.txtUserID.Size = new System.Drawing.Size(15, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(280, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 21);
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
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(346, 170);
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
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(526, 115);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 199;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(334, 100);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(187, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(362, 190);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(241, 183);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 116);
            this.lstDeducteeHelp.TabIndex = 219;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // dgvDefaultSummary
            // 
            this.dgvDefaultSummary.AllowUserToAddRows = false;
            this.dgvDefaultSummary.AllowUserToDeleteRows = false;
            this.dgvDefaultSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefaultSummary.Location = new System.Drawing.Point(25, 41);
            this.dgvDefaultSummary.Name = "dgvDefaultSummary";
            this.dgvDefaultSummary.ReadOnly = true;
            this.dgvDefaultSummary.Size = new System.Drawing.Size(898, 475);
            this.dgvDefaultSummary.TabIndex = 15;
            this.dgvDefaultSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatementList_CellClick);
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(891, 16);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 217;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // btnPrintDefaultSummary
            // 
            this.btnPrintDefaultSummary.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintDefaultSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintDefaultSummary.Location = new System.Drawing.Point(850, 517);
            this.btnPrintDefaultSummary.Name = "btnPrintDefaultSummary";
            this.btnPrintDefaultSummary.Size = new System.Drawing.Size(75, 23);
            this.btnPrintDefaultSummary.TabIndex = 247;
            this.btnPrintDefaultSummary.Text = "Print";
            this.btnPrintDefaultSummary.UseVisualStyleBackColor = false;
            this.btnPrintDefaultSummary.Click += new System.EventHandler(this.btnPrintDefaultSummary_Click);
            // 
            // grpDefaultSummaryList
            // 
            this.grpDefaultSummaryList.Controls.Add(this.lblTanNo);
            this.grpDefaultSummaryList.Controls.Add(this.btnPrintDefaultSummary);
            this.grpDefaultSummaryList.Controls.Add(this.lnkLogOff);
            this.grpDefaultSummaryList.Controls.Add(this.dgvDefaultSummary);
            this.grpDefaultSummaryList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDefaultSummaryList.Location = new System.Drawing.Point(48, 5);
            this.grpDefaultSummaryList.Name = "grpDefaultSummaryList";
            this.grpDefaultSummaryList.Size = new System.Drawing.Size(949, 545);
            this.grpDefaultSummaryList.TabIndex = 48;
            this.grpDefaultSummaryList.TabStop = false;
            this.grpDefaultSummaryList.Text = "Default Summary Details for all FA";
            this.grpDefaultSummaryList.Visible = false;
            // 
            // lblTanNo
            // 
            this.lblTanNo.AutoSize = true;
            this.lblTanNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTanNo.ForeColor = System.Drawing.Color.Gray;
            this.lblTanNo.Location = new System.Drawing.Point(22, 23);
            this.lblTanNo.Name = "lblTanNo";
            this.lblTanNo.Size = new System.Drawing.Size(52, 13);
            this.lblTanNo.TabIndex = 249;
            this.lblTanNo.Text = "TAN No";
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(917, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 208;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(955, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 209;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnDefaultSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 575);
            this.Name = "TrnDefaultSummary";
            this.Activated += new System.EventHandler(this.TrnDefaultSummary_Activated);
            this.Load += new System.EventHandler(this.TrnChallanStatusTraces_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpProgress.ResumeLayout(false);
            this.grpSummaryDetails.ResumeLayout(false);
            this.grpSummaryDetails.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPanError)).EndInit();
            this.grpConsumptionDetails.ResumeLayout(false);
            this.grpConsumptionDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaultSummaryDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).EndInit();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaultSummary)).EndInit();
            this.grpDefaultSummaryList.ResumeLayout(false);
            this.grpDefaultSummaryList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.GroupBox grpSummaryDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPanError;
        private System.Windows.Forms.GroupBox grpConsumptionDetails;
        private System.Windows.Forms.DataGridView dgvDefaultSummaryDetails;
        private System.Windows.Forms.Label lblNetPayable;
        private System.Windows.Forms.Label lblCorrectionCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvStatement;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalPayable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBack;
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
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.GroupBox grpDefaultSummaryList;
        private System.Windows.Forms.Button btnPrintDefaultSummary;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.Windows.Forms.DataGridView dgvDefaultSummary;
        private System.Windows.Forms.Label lblTanNo;
        private System.Windows.Forms.Label lblSelectedFormNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSelectedFaYear;
        private System.Windows.Forms.Label lblSelectedQtr;
        private System.Windows.Forms.Label lblSelectedTanNo;
        private System.Windows.Forms.Button btnPrintDetails;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
