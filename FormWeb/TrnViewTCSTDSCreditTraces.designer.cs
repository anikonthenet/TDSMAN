namespace TDSMAN.FormWeb
{
    partial class TrnViewTCSTDSCreditTraces
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnViewTCSTDSCreditTraces));
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.grpDetucteeDetails = new System.Windows.Forms.GroupBox();
            this.lblNameOfDeductee = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPAN = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dgvDeducteeDetails = new System.Windows.Forms.DataGridView();
            this.grpStatementDetails = new System.Windows.Forms.GroupBox();
            this.lblFormType = new System.Windows.Forms.Label();
            this.lblTAN = new System.Windows.Forms.Label();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.lblLatestCorrectionStatement = new System.Windows.Forms.Label();
            this.lblTokenNumber = new System.Windows.Forms.Label();
            this.lblAssesmentYear = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPAN = new System.Windows.Forms.TextBox();
            this.PA = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
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
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpDetucteeDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeducteeDetails)).BeginInit();
            this.grpStatementDetails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpLoginDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 437);
            this.grpSort.Size = new System.Drawing.Size(10, 1);
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
            this.grpSearch.Location = new System.Drawing.Point(95, 423);
            this.grpSearch.Size = new System.Drawing.Size(13, 9);
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
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 484);
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
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Location = new System.Drawing.Point(12, 487);
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
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1005, 497);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 470);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.grpDetucteeDetails);
            this.grpDownloadList.Controls.Add(this.grpStatementDetails);
            this.grpDownloadList.Controls.Add(this.groupBox2);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Location = new System.Drawing.Point(31, -1);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 376);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // grpDetucteeDetails
            // 
            this.grpDetucteeDetails.Controls.Add(this.lblNameOfDeductee);
            this.grpDetucteeDetails.Controls.Add(this.label17);
            this.grpDetucteeDetails.Controls.Add(this.lblPAN);
            this.grpDetucteeDetails.Controls.Add(this.label15);
            this.grpDetucteeDetails.Controls.Add(this.dgvDeducteeDetails);
            this.grpDetucteeDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDetucteeDetails.Location = new System.Drawing.Point(14, 148);
            this.grpDetucteeDetails.Name = "grpDetucteeDetails";
            this.grpDetucteeDetails.Size = new System.Drawing.Size(922, 221);
            this.grpDetucteeDetails.TabIndex = 198;
            this.grpDetucteeDetails.TabStop = false;
            this.grpDetucteeDetails.Text = "Deductee Details";
            // 
            // lblNameOfDeductee
            // 
            this.lblNameOfDeductee.AutoSize = true;
            this.lblNameOfDeductee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfDeductee.Location = new System.Drawing.Point(360, 29);
            this.lblNameOfDeductee.Name = "lblNameOfDeductee";
            this.lblNameOfDeductee.Size = new System.Drawing.Size(103, 13);
            this.lblNameOfDeductee.TabIndex = 248;
            this.lblNameOfDeductee.Text = "lblNameOfDeductee";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(245, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 13);
            this.label17.TabIndex = 247;
            this.label17.Text = "Name of Deductee";
            // 
            // lblPAN
            // 
            this.lblPAN.AutoSize = true;
            this.lblPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAN.Location = new System.Drawing.Point(90, 29);
            this.lblPAN.Name = "lblPAN";
            this.lblPAN.Size = new System.Drawing.Size(39, 13);
            this.lblPAN.TabIndex = 246;
            this.lblPAN.Text = "lblPAN";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(23, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 245;
            this.label15.Text = "PAN";
            // 
            // dgvDeducteeDetails
            // 
            this.dgvDeducteeDetails.AllowUserToAddRows = false;
            this.dgvDeducteeDetails.AllowUserToDeleteRows = false;
            this.dgvDeducteeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeducteeDetails.Location = new System.Drawing.Point(13, 63);
            this.dgvDeducteeDetails.Name = "dgvDeducteeDetails";
            this.dgvDeducteeDetails.ReadOnly = true;
            this.dgvDeducteeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeducteeDetails.Size = new System.Drawing.Size(895, 142);
            this.dgvDeducteeDetails.TabIndex = 14;
            // 
            // grpStatementDetails
            // 
            this.grpStatementDetails.Controls.Add(this.lblFormType);
            this.grpStatementDetails.Controls.Add(this.lblTAN);
            this.grpStatementDetails.Controls.Add(this.lblQuarter);
            this.grpStatementDetails.Controls.Add(this.lblLatestCorrectionStatement);
            this.grpStatementDetails.Controls.Add(this.lblTokenNumber);
            this.grpStatementDetails.Controls.Add(this.lblAssesmentYear);
            this.grpStatementDetails.Controls.Add(this.label13);
            this.grpStatementDetails.Controls.Add(this.label12);
            this.grpStatementDetails.Controls.Add(this.label11);
            this.grpStatementDetails.Controls.Add(this.label10);
            this.grpStatementDetails.Controls.Add(this.label9);
            this.grpStatementDetails.Controls.Add(this.label8);
            this.grpStatementDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatementDetails.Location = new System.Drawing.Point(14, 56);
            this.grpStatementDetails.Name = "grpStatementDetails";
            this.grpStatementDetails.Size = new System.Drawing.Size(922, 90);
            this.grpStatementDetails.TabIndex = 197;
            this.grpStatementDetails.TabStop = false;
            this.grpStatementDetails.Text = "Statement Details";
            // 
            // lblFormType
            // 
            this.lblFormType.AutoSize = true;
            this.lblFormType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormType.Location = new System.Drawing.Point(89, 56);
            this.lblFormType.Name = "lblFormType";
            this.lblFormType.Size = new System.Drawing.Size(64, 13);
            this.lblFormType.TabIndex = 245;
            this.lblFormType.Text = "lblFormType";
            // 
            // lblTAN
            // 
            this.lblTAN.AutoSize = true;
            this.lblTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTAN.Location = new System.Drawing.Point(89, 27);
            this.lblTAN.Name = "lblTAN";
            this.lblTAN.Size = new System.Drawing.Size(39, 13);
            this.lblTAN.TabIndex = 244;
            this.lblTAN.Text = "lblTAN";
            // 
            // lblQuarter
            // 
            this.lblQuarter.AutoSize = true;
            this.lblQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuarter.Location = new System.Drawing.Point(358, 56);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(52, 13);
            this.lblQuarter.TabIndex = 243;
            this.lblQuarter.Text = "lblQuarter";
            // 
            // lblLatestCorrectionStatement
            // 
            this.lblLatestCorrectionStatement.AutoSize = true;
            this.lblLatestCorrectionStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestCorrectionStatement.Location = new System.Drawing.Point(764, 56);
            this.lblLatestCorrectionStatement.Name = "lblLatestCorrectionStatement";
            this.lblLatestCorrectionStatement.Size = new System.Drawing.Size(142, 13);
            this.lblLatestCorrectionStatement.TabIndex = 242;
            this.lblLatestCorrectionStatement.Text = "lblLatestCorrectionStatement";
            // 
            // lblTokenNumber
            // 
            this.lblTokenNumber.AutoSize = true;
            this.lblTokenNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTokenNumber.Location = new System.Drawing.Point(766, 27);
            this.lblTokenNumber.Name = "lblTokenNumber";
            this.lblTokenNumber.Size = new System.Drawing.Size(85, 13);
            this.lblTokenNumber.TabIndex = 241;
            this.lblTokenNumber.Text = "lblTokenNumber";
            // 
            // lblAssesmentYear
            // 
            this.lblAssesmentYear.AutoSize = true;
            this.lblAssesmentYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssesmentYear.Location = new System.Drawing.Point(356, 27);
            this.lblAssesmentYear.Name = "lblAssesmentYear";
            this.lblAssesmentYear.Size = new System.Drawing.Size(90, 13);
            this.lblAssesmentYear.TabIndex = 240;
            this.lblAssesmentYear.Text = "lblAssesmentYear";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(494, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(214, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Token Number of Regular Statement";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(496, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(267, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Token Number of Latest Correction Statement";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(242, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Quarter";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(22, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Form Type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(244, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Assessment Year";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(22, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "TAN";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPAN);
            this.groupBox2.Controls.Add(this.PA);
            this.groupBox2.Controls.Add(this.btnGo);
            this.groupBox2.Controls.Add(this.cmbQtr);
            this.groupBox2.Controls.Add(this.cmbFormNo);
            this.groupBox2.Controls.Add(this.cmbFAYear);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(869, 43);
            this.groupBox2.TabIndex = 196;
            this.groupBox2.TabStop = false;
            // 
            // txtPAN
            // 
            this.txtPAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPAN.Location = new System.Drawing.Point(104, 14);
            this.txtPAN.MaxLength = 10;
            this.txtPAN.Name = "txtPAN";
            this.txtPAN.Size = new System.Drawing.Size(96, 20);
            this.txtPAN.TabIndex = 213;
            // 
            // PA
            // 
            this.PA.AutoSize = true;
            this.PA.Location = new System.Drawing.Point(65, 16);
            this.PA.Name = "PA";
            this.PA.Size = new System.Drawing.Size(32, 13);
            this.PA.TabIndex = 212;
            this.PA.Text = "PAN";
            // 
            // btnGo
            // 
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Location = new System.Drawing.Point(802, 11);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(40, 23);
            this.btnGo.TabIndex = 211;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbQtr
            // 
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(682, 13);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(92, 21);
            this.cmbQtr.TabIndex = 2;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(517, 13);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(96, 21);
            this.cmbFormNo.TabIndex = 1;
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(282, 13);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(162, 21);
            this.cmbFAYear.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(627, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quarter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Form No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tax Year";
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(893, 12);
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
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(107, 72);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 293);
            this.grpLoginDetails.TabIndex = 0;
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
            this.txtUserID.Size = new System.Drawing.Size(10, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(264, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 21);
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
            this.label3.Location = new System.Drawing.Point(344, 167);
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
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(526, 106);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 199;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(334, 91);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(187, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(362, 189);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(31, 380);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(949, 49);
            this.grpProgress.TabIndex = 214;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(20, 22);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(909, 12);
            this.pBar.TabIndex = 8;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(252, 172);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 148);
            this.lstDeducteeHelp.TabIndex = 213;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(82, 32);
            this.pctVideoDemo.TabIndex = 209;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnViewTCSTDSCreditTraces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1019, 542);
            this.Controls.Add(this.lstDeducteeHelp);
            this.Name = "TrnViewTCSTDSCreditTraces";
            this.Activated += new System.EventHandler(this.TrnViewTCSTDSCreditTraces_Activated);
            this.Load += new System.EventHandler(this.TrnStatementStatusTraces_Load);
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
            this.Controls.SetChildIndex(this.lstDeducteeHelp, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpDetucteeDetails.ResumeLayout(false);
            this.grpDetucteeDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeducteeDetails)).EndInit();
            this.grpStatementDetails.ResumeLayout(false);
            this.grpStatementDetails.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox grpStatementDetails;
        private System.Windows.Forms.GroupBox grpDetucteeDetails;
        private System.Windows.Forms.DataGridView dgvDeducteeDetails;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFormType;
        private System.Windows.Forms.Label lblTAN;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.Label lblLatestCorrectionStatement;
        private System.Windows.Forms.Label lblTokenNumber;
        private System.Windows.Forms.Label lblAssesmentYear;
        private System.Windows.Forms.Label lblNameOfDeductee;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPAN;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPAN;
        private System.Windows.Forms.Label PA;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
