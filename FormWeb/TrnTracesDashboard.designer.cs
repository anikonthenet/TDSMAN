namespace TDSMAN.FormWeb
{
    partial class TrnTracesDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnTracesDashboard));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.lblCaptchaId = new System.Windows.Forms.Label();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
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
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.lnkViewUnconsumedChallan = new System.Windows.Forms.LinkLabel();
            this.grpTRACESActivities = new System.Windows.Forms.GroupBox();
            this.lblTRACESActivities = new System.Windows.Forms.TextBox();
            this.lblOutstandingDemand = new System.Windows.Forms.Label();
            this.grpAlerts = new System.Windows.Forms.GroupBox();
            this.lblAlerts = new System.Windows.Forms.TextBox();
            this.lnkDownloadRequests = new System.Windows.Forms.LinkLabel();
            this.lnkInbox = new System.Windows.Forms.LinkLabel();
            this.grpStatementStatus = new System.Windows.Forms.GroupBox();
            this.lblClickDefaults = new System.Windows.Forms.Label();
            this.dgvStatementStatus = new System.Windows.Forms.DataGridView();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.ctxtMnuStripInbox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxtMnuInboxAction = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtMnuNoInboxAction = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpLoginDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpTRACESActivities.SuspendLayout();
            this.grpAlerts.SuspendLayout();
            this.grpStatementStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.ctxtMnuStripInbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 608);
            this.grpSort.Size = new System.Drawing.Size(280, 8);
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
            this.grpSearch.Location = new System.Drawing.Point(95, 609);
            this.grpSearch.Size = new System.Drawing.Size(280, 8);
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
            this.pnlFooter.Location = new System.Drawing.Point(9, 552);
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
            this.grpButton.Location = new System.Drawing.Point(9, 559);
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
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1012, 569);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 543);
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
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.lblCaptchaId);
            this.grpLoginDetails.Controls.Add(this.lstDeducteeHelp);
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(151, 19);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(710, 58);
            this.grpLoginDetails.TabIndex = 0;
            this.grpLoginDetails.TabStop = false;
            // 
            // lblCaptchaId
            // 
            this.lblCaptchaId.AutoSize = true;
            this.lblCaptchaId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptchaId.Location = new System.Drawing.Point(113, 81);
            this.lblCaptchaId.Name = "lblCaptchaId";
            this.lblCaptchaId.Size = new System.Drawing.Size(61, 13);
            this.lblCaptchaId.TabIndex = 218;
            this.lblCaptchaId.Text = "Password";
            this.lblCaptchaId.Visible = false;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(93, 52);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 84);
            this.lstDeducteeHelp.TabIndex = 217;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
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
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(677, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Login Details";
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(194, 18);
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
            this.label5.Location = new System.Drawing.Point(149, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(258, 18);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(14, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(382, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(309, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 21);
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
            this.label3.Location = new System.Drawing.Point(144, 133);
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
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(500, 73);
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
            this.picCaptcha.Location = new System.Drawing.Point(198, 66);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(280, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(319, 129);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(31, 455);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(949, 37);
            this.grpProgress.TabIndex = 214;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 17);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(925, 13);
            this.pBar.TabIndex = 8;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(911, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 206;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(894, 11);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 195;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLogOff_LinkClicked);
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.lnkViewUnconsumedChallan);
            this.grpDownloadList.Controls.Add(this.grpTRACESActivities);
            this.grpDownloadList.Controls.Add(this.lblOutstandingDemand);
            this.grpDownloadList.Controls.Add(this.grpAlerts);
            this.grpDownloadList.Controls.Add(this.lnkDownloadRequests);
            this.grpDownloadList.Controls.Add(this.lnkInbox);
            this.grpDownloadList.Controls.Add(this.grpStatementStatus);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 4);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 450);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // lnkViewUnconsumedChallan
            // 
            this.lnkViewUnconsumedChallan.AutoSize = true;
            this.lnkViewUnconsumedChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkViewUnconsumedChallan.Location = new System.Drawing.Point(386, 311);
            this.lnkViewUnconsumedChallan.Name = "lnkViewUnconsumedChallan";
            this.lnkViewUnconsumedChallan.Size = new System.Drawing.Size(191, 16);
            this.lnkViewUnconsumedChallan.TabIndex = 203;
            this.lnkViewUnconsumedChallan.TabStop = true;
            this.lnkViewUnconsumedChallan.Text = "View Unconsumed Challan";
            this.lnkViewUnconsumedChallan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkViewUnconsumedChallan_LinkClicked);
            // 
            // grpTRACESActivities
            // 
            this.grpTRACESActivities.Controls.Add(this.lblTRACESActivities);
            this.grpTRACESActivities.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTRACESActivities.ForeColor = System.Drawing.Color.Blue;
            this.grpTRACESActivities.Location = new System.Drawing.Point(58, 354);
            this.grpTRACESActivities.Name = "grpTRACESActivities";
            this.grpTRACESActivities.Size = new System.Drawing.Size(830, 88);
            this.grpTRACESActivities.TabIndex = 202;
            this.grpTRACESActivities.TabStop = false;
            this.grpTRACESActivities.Text = "Activities";
            // 
            // lblTRACESActivities
            // 
            this.lblTRACESActivities.Location = new System.Drawing.Point(5, 19);
            this.lblTRACESActivities.Multiline = true;
            this.lblTRACESActivities.Name = "lblTRACESActivities";
            this.lblTRACESActivities.ReadOnly = true;
            this.lblTRACESActivities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblTRACESActivities.Size = new System.Drawing.Size(819, 62);
            this.lblTRACESActivities.TabIndex = 1;
            // 
            // lblOutstandingDemand
            // 
            this.lblOutstandingDemand.AutoSize = true;
            this.lblOutstandingDemand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutstandingDemand.ForeColor = System.Drawing.Color.Blue;
            this.lblOutstandingDemand.Location = new System.Drawing.Point(61, 311);
            this.lblOutstandingDemand.Name = "lblOutstandingDemand";
            this.lblOutstandingDemand.Size = new System.Drawing.Size(160, 16);
            this.lblOutstandingDemand.TabIndex = 201;
            this.lblOutstandingDemand.Text = "Outstanding Demand :";
            // 
            // grpAlerts
            // 
            this.grpAlerts.Controls.Add(this.lblAlerts);
            this.grpAlerts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAlerts.ForeColor = System.Drawing.Color.Blue;
            this.grpAlerts.Location = new System.Drawing.Point(58, 12);
            this.grpAlerts.Name = "grpAlerts";
            this.grpAlerts.Size = new System.Drawing.Size(830, 88);
            this.grpAlerts.TabIndex = 200;
            this.grpAlerts.TabStop = false;
            this.grpAlerts.Text = "Alerts";
            // 
            // lblAlerts
            // 
            this.lblAlerts.Location = new System.Drawing.Point(6, 18);
            this.lblAlerts.Multiline = true;
            this.lblAlerts.Name = "lblAlerts";
            this.lblAlerts.ReadOnly = true;
            this.lblAlerts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblAlerts.Size = new System.Drawing.Size(818, 63);
            this.lblAlerts.TabIndex = 0;
            // 
            // lnkDownloadRequests
            // 
            this.lnkDownloadRequests.AutoSize = true;
            this.lnkDownloadRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownloadRequests.Location = new System.Drawing.Point(688, 331);
            this.lnkDownloadRequests.Name = "lnkDownloadRequests";
            this.lnkDownloadRequests.Size = new System.Drawing.Size(147, 16);
            this.lnkDownloadRequests.TabIndex = 199;
            this.lnkDownloadRequests.TabStop = true;
            this.lnkDownloadRequests.Text = "Download Requests";
            this.lnkDownloadRequests.Visible = false;
            // 
            // lnkInbox
            // 
            this.lnkInbox.AutoSize = true;
            this.lnkInbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkInbox.Location = new System.Drawing.Point(688, 311);
            this.lnkInbox.Name = "lnkInbox";
            this.lnkInbox.Size = new System.Drawing.Size(45, 16);
            this.lnkInbox.TabIndex = 198;
            this.lnkInbox.TabStop = true;
            this.lnkInbox.Text = "Inbox";
            this.lnkInbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LnkInbox_MouseClick);
            // 
            // grpStatementStatus
            // 
            this.grpStatementStatus.Controls.Add(this.lblClickDefaults);
            this.grpStatementStatus.Controls.Add(this.dgvStatementStatus);
            this.grpStatementStatus.Location = new System.Drawing.Point(58, 110);
            this.grpStatementStatus.Name = "grpStatementStatus";
            this.grpStatementStatus.Size = new System.Drawing.Size(830, 182);
            this.grpStatementStatus.TabIndex = 197;
            this.grpStatementStatus.TabStop = false;
            this.grpStatementStatus.Visible = false;
            // 
            // lblClickDefaults
            // 
            this.lblClickDefaults.AutoSize = true;
            this.lblClickDefaults.Location = new System.Drawing.Point(9, 161);
            this.lblClickDefaults.Name = "lblClickDefaults";
            this.lblClickDefaults.Size = new System.Drawing.Size(181, 13);
            this.lblClickDefaults.TabIndex = 1;
            this.lblClickDefaults.Text = "Click \'Defaults\'/\'Proc (Def)\' for details";
            // 
            // dgvStatementStatus
            // 
            this.dgvStatementStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementStatus.Location = new System.Drawing.Point(6, 23);
            this.dgvStatementStatus.Name = "dgvStatementStatus";
            this.dgvStatementStatus.Size = new System.Drawing.Size(818, 133);
            this.dgvStatementStatus.TabIndex = 0;
            this.dgvStatementStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatementStatus_CellClick);
            this.dgvStatementStatus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStatementStatus_CellFormatting);
            this.dgvStatementStatus.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatementStatus_CellMouseLeave);
            this.dgvStatementStatus.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStatementStatus_CellMouseMove);
            this.dgvStatementStatus.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvStatementStatus_CellPainting);
            this.dgvStatementStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvStatementStatus_Paint);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(948, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 212;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // ctxtMnuStripInbox
            // 
            this.ctxtMnuStripInbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxtMnuInboxAction,
            this.ctxtMnuNoInboxAction});
            this.ctxtMnuStripInbox.Name = "ctxtMnuStripInbox";
            this.ctxtMnuStripInbox.Size = new System.Drawing.Size(129, 48);
            // 
            // ctxtMnuInboxAction
            // 
            this.ctxtMnuInboxAction.Name = "ctxtMnuInboxAction";
            this.ctxtMnuInboxAction.Size = new System.Drawing.Size(128, 22);
            this.ctxtMnuInboxAction.Text = "Action";
            this.ctxtMnuInboxAction.Click += new System.EventHandler(this.CtxtMnuInboxAction_Click);
            // 
            // ctxtMnuNoInboxAction
            // 
            this.ctxtMnuNoInboxAction.Name = "ctxtMnuNoInboxAction";
            this.ctxtMnuNoInboxAction.Size = new System.Drawing.Size(128, 22);
            this.ctxtMnuNoInboxAction.Text = "No Action";
            this.ctxtMnuNoInboxAction.Click += new System.EventHandler(this.CtxtMnuNoInboxAction_Click);
            // 
            // TrnTracesDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1033, 594);
            this.Name = "TrnTracesDashboard";
            this.Activated += new System.EventHandler(this.TrnStatementStatusTraces_Activated);
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
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpTRACESActivities.ResumeLayout(false);
            this.grpTRACESActivities.PerformLayout();
            this.grpAlerts.ResumeLayout(false);
            this.grpAlerts.PerformLayout();
            this.grpStatementStatus.ResumeLayout(false);
            this.grpStatementStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ctxtMnuStripInbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.GroupBox grpStatementStatus;
        private System.Windows.Forms.DataGridView dgvStatementStatus;
        private System.Windows.Forms.LinkLabel lnkInbox;
        private System.Windows.Forms.LinkLabel lnkDownloadRequests;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStripInbox;
        private System.Windows.Forms.ToolStripMenuItem ctxtMnuInboxAction;
        private System.Windows.Forms.ToolStripMenuItem ctxtMnuNoInboxAction;
        private System.Windows.Forms.GroupBox grpTRACESActivities;
        private System.Windows.Forms.Label lblOutstandingDemand;
        private System.Windows.Forms.GroupBox grpAlerts;
        private System.Windows.Forms.TextBox lblAlerts;
        private System.Windows.Forms.LinkLabel lnkViewUnconsumedChallan;
        private System.Windows.Forms.TextBox lblTRACESActivities;
        private System.Windows.Forms.Label lblClickDefaults;
        private System.Windows.Forms.Label lblCaptchaId;
    }
}
