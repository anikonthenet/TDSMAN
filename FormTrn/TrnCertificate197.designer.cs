namespace TDSMAN.FormTrn
{
    partial class TrnCertificate197
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnCertificate197));
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.grpStep2 = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCertificateNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPAN = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.dgvCertificateList = new System.Windows.Forms.DataGridView();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.grpStep1 = new System.Windows.Forms.GroupBox();
            this.txtTAN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpStep2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificateList)).BeginInit();
            this.grpLoginDetails.SuspendLayout();
            this.grpStep1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(385, 486);
            this.grpSort.Size = new System.Drawing.Size(36, 28);
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
            this.grpSearch.Location = new System.Drawing.Point(111, 496);
            this.grpSearch.Size = new System.Drawing.Size(28, 17);
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
            this.pnlFooter.Location = new System.Drawing.Point(9, 472);
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
            this.grpButton.Location = new System.Drawing.Point(9, 474);
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
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1002, 480);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 458);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.grpStep2);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Controls.Add(this.dgvCertificateList);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 4);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 360);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // grpStep2
            // 
            this.grpStep2.Controls.Add(this.btnGo);
            this.grpStep2.Controls.Add(this.cmbFAYear);
            this.grpStep2.Controls.Add(this.label7);
            this.grpStep2.Controls.Add(this.txtCertificateNo);
            this.grpStep2.Controls.Add(this.label6);
            this.grpStep2.Controls.Add(this.txtPAN);
            this.grpStep2.Controls.Add(this.label8);
            this.grpStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStep2.Location = new System.Drawing.Point(77, 11);
            this.grpStep2.Name = "grpStep2";
            this.grpStep2.Size = new System.Drawing.Size(795, 47);
            this.grpStep2.TabIndex = 215;
            this.grpStep2.TabStop = false;
            this.grpStep2.Text = "Step 2";
            // 
            // btnGo
            // 
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Location = new System.Drawing.Point(747, 15);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(40, 23);
            this.btnGo.TabIndex = 212;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(573, 17);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(162, 21);
            this.cmbFAYear.TabIndex = 211;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(514, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 210;
            this.label7.Text = "Tax Year";
            // 
            // txtCertificateNo
            // 
            this.txtCertificateNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCertificateNo.Location = new System.Drawing.Point(132, 17);
            this.txtCertificateNo.MaxLength = 10;
            this.txtCertificateNo.Name = "txtCertificateNo";
            this.txtCertificateNo.Size = new System.Drawing.Size(145, 20);
            this.txtCertificateNo.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 209;
            this.label6.Text = "Certificate Number";
            // 
            // txtPAN
            // 
            this.txtPAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPAN.Location = new System.Drawing.Point(343, 17);
            this.txtPAN.MaxLength = 50;
            this.txtPAN.Name = "txtPAN";
            this.txtPAN.Size = new System.Drawing.Size(145, 20);
            this.txtPAN.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(301, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 206;
            this.label8.Text = "PAN ";
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(894, 16);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 195;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // dgvCertificateList
            // 
            this.dgvCertificateList.AllowUserToAddRows = false;
            this.dgvCertificateList.AllowUserToDeleteRows = false;
            this.dgvCertificateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificateList.Location = new System.Drawing.Point(14, 64);
            this.dgvCertificateList.Name = "dgvCertificateList";
            this.dgvCertificateList.ReadOnly = true;
            this.dgvCertificateList.Size = new System.Drawing.Size(921, 284);
            this.dgvCertificateList.TabIndex = 13;
            this.dgvCertificateList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDownloadList_CellClick);
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
            // pgTimerGrid
            // 
            this.pgTimerGrid.Tick += new System.EventHandler(this.pgTimerGrid_Tick);
            // 
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.lstDeducteeHelp);
            this.grpLoginDetails.Controls.Add(this.grpStep1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(54, 52);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(901, 332);
            this.grpLoginDetails.TabIndex = 0;
            this.grpLoginDetails.TabStop = false;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(133, 110);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 116);
            this.lstDeducteeHelp.TabIndex = 218;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // grpStep1
            // 
            this.grpStep1.Controls.Add(this.txtTAN);
            this.grpStep1.Controls.Add(this.label5);
            this.grpStep1.Controls.Add(this.txtUserId);
            this.grpStep1.Controls.Add(this.txtPassword);
            this.grpStep1.Controls.Add(this.label2);
            this.grpStep1.Controls.Add(this.label1);
            this.grpStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStep1.Location = new System.Drawing.Point(58, 68);
            this.grpStep1.Name = "grpStep1";
            this.grpStep1.Size = new System.Drawing.Size(795, 67);
            this.grpStep1.TabIndex = 0;
            this.grpStep1.TabStop = false;
            this.grpStep1.Text = "Enter TRACES Login Details";
            // 
            // txtTAN
            // 
            this.txtTAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTAN.Location = new System.Drawing.Point(77, 18);
            this.txtTAN.MaxLength = 10;
            this.txtTAN.Name = "txtTAN";
            this.txtTAN.Size = new System.Drawing.Size(96, 20);
            this.txtTAN.TabIndex = 0;
            this.txtTAN.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtTAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtTAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTAN.Leave += new System.EventHandler(this.txtTAN_Leave);
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
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(253, 18);
            this.txtUserId.MaxLength = 50;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(20, 20);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(276, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(211, 21);
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
            this.label3.Location = new System.Drawing.Point(188, 240);
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
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(526, 182);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 199;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            this.btnCaptchaRefresh.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCaptchaRefresh_MouseMove);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(334, 167);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(187, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(362, 236);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(31, 365);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(949, 49);
            this.grpProgress.TabIndex = 214;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(18, 19);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(911, 16);
            this.pBar.TabIndex = 8;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 11);
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
            this.pctUserManual.Location = new System.Drawing.Point(954, 11);
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
            // TrnCertificate197
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1017, 525);
            this.Name = "TrnCertificate197";
            this.Activated += new System.EventHandler(this.TrnCertificate197_Activated);
            this.Load += new System.EventHandler(this.TrnViewReturnStatusOnline_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpStep2.ResumeLayout(false);
            this.grpStep2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificateList)).EndInit();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.grpStep1.ResumeLayout(false);
            this.grpStep1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.DataGridView dgvCertificateList;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.GroupBox grpStep1;
        private System.Windows.Forms.TextBox txtTAN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpStep2;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCertificateNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPAN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
