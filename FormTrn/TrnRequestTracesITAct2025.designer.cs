namespace TDSMAN.FormTrn
{
    partial class TrnRequestTracesITAct2025
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnRequestTracesITAct2025));
            this.grpReturnInfo = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkPre2627 = new System.Windows.Forms.LinkLabel();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTAN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bgwWorker = new System.ComponentModel.BackgroundWorker();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblFileRequest = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.btnCaptcha = new System.Windows.Forms.Button();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.lblCaptcha = new System.Windows.Forms.Label();
            this.grpTextMessage = new System.Windows.Forms.GroupBox();
            this.lblTextMessage6 = new System.Windows.Forms.Label();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lnkRequestPAN = new System.Windows.Forms.LinkLabel();
            this.grpPANLists = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearchPAN = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblPANHeaderText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.chklstPANs = new System.Windows.Forms.CheckedListBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.lnkDownloadUtility = new System.Windows.Forms.LinkLabel();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpReturnInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpTextMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.grpPANLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(115, 494);
            this.grpSort.Size = new System.Drawing.Size(5, 26);
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
            this.BtnSave.Location = new System.Drawing.Point(401, 15);
            this.BtnSave.Size = new System.Drawing.Size(101, 23);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&Submit";
            this.BtnSave.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(50, 493);
            this.grpSearch.Size = new System.Drawing.Size(5, 26);
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
            this.BtnExit.Location = new System.Drawing.Point(502, 15);
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 529);
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
            this.grpButton.Controls.Add(this.lnkDownloadUtility);
            this.grpButton.Controls.Add(this.lnkRequestPAN);
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Location = new System.Drawing.Point(9, 529);
            this.grpButton.Size = new System.Drawing.Size(1002, 46);
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
            this.grpButton.Controls.SetChildIndex(this.lnkRequestPAN, 0);
            this.grpButton.Controls.SetChildIndex(this.lnkDownloadUtility, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpPANLists);
            this.pnlControls.Controls.Add(this.grpTextMessage);
            this.pnlControls.Controls.Add(this.lblCaptcha);
            this.pnlControls.Controls.Add(this.txtCaptcha);
            this.pnlControls.Controls.Add(this.btnCaptcha);
            this.pnlControls.Controls.Add(this.picCaptcha);
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.lstDeducteeHelp);
            this.pnlControls.Controls.Add(this.grpReturnInfo);
            this.pnlControls.Size = new System.Drawing.Size(1003, 486);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 528);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 3);
            this.ViewGrid.Visible = false;
            // 
            // grpReturnInfo
            // 
            this.grpReturnInfo.Controls.Add(this.groupBox2);
            this.grpReturnInfo.Controls.Add(this.groupBox1);
            this.grpReturnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReturnInfo.Location = new System.Drawing.Point(167, 2);
            this.grpReturnInfo.Name = "grpReturnInfo";
            this.grpReturnInfo.Size = new System.Drawing.Size(670, 108);
            this.grpReturnInfo.TabIndex = 0;
            this.grpReturnInfo.TabStop = false;
            this.grpReturnInfo.Text = "Step 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbQtr);
            this.groupBox2.Controls.Add(this.cmbFormNo);
            this.groupBox2.Controls.Add(this.cmbFAYear);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(19, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Your Return";
            // 
            // cmbQtr
            // 
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(491, 17);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(92, 21);
            this.cmbQtr.TabIndex = 2;
            this.cmbQtr.SelectedIndexChanged += new System.EventHandler(this.cmbFAYear_SelectedIndexChanged);
            this.cmbQtr.Enter += new System.EventHandler(this.txtUserId_Enter);
            this.cmbQtr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(326, 17);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(96, 21);
            this.cmbFormNo.TabIndex = 1;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbFAYear_SelectedIndexChanged);
            this.cmbFormNo.Enter += new System.EventHandler(this.txtUserId_Enter);
            this.cmbFormNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(91, 17);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(162, 21);
            this.cmbFAYear.TabIndex = 0;
            this.cmbFAYear.SelectedIndexChanged += new System.EventHandler(this.cmbFAYear_SelectedIndexChanged);
            this.cmbFAYear.Enter += new System.EventHandler(this.txtUserId_Enter);
            this.cmbFAYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quarter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Form No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tax Year";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkPre2627);
            this.groupBox1.Controls.Add(this.chkRememberMe);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTAN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter TRACES User Details";
            // 
            // lnkPre2627
            // 
            this.lnkPre2627.AutoSize = true;
            this.lnkPre2627.Location = new System.Drawing.Point(512, 18);
            this.lnkPre2627.Name = "lnkPre2627";
            this.lnkPre2627.Size = new System.Drawing.Size(103, 13);
            this.lnkPre2627.TabIndex = 22;
            this.lnkPre2627.TabStop = true;
            this.lnkPre2627.Text = "goto IT Act 1961";
            this.lnkPre2627.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkPre2627_LinkClicked);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.Location = new System.Drawing.Point(533, 44);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(95, 17);
            this.chkRememberMe.TabIndex = 7;
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            this.chkRememberMe.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(312, 17);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(126, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Enter += new System.EventHandler(this.txtUserId_Enter);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(298, 17);
            this.txtUserId.MaxLength = 50;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(11, 20);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.Visible = false;
            this.txtUserId.Enter += new System.EventHandler(this.txtUserId_Enter);
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Id";
            this.label2.Visible = false;
            // 
            // txtTAN
            // 
            this.txtTAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTAN.Location = new System.Drawing.Point(89, 17);
            this.txtTAN.MaxLength = 10;
            this.txtTAN.Name = "txtTAN";
            this.txtTAN.Size = new System.Drawing.Size(126, 20);
            this.txtTAN.TabIndex = 0;
            this.txtTAN.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtTAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtTAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTAN.Leave += new System.EventHandler(this.txtTAN_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAN";
            // 
            // bgwWorker
            // 
            this.bgwWorker.WorkerReportsProgress = true;
            this.bgwWorker.WorkerSupportsCancellation = true;
            this.bgwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorker_DoWork);
            this.bgwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorker_RunWorkerCompleted);
            // 
            // progressTimer
            // 
            this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(276, 57);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(539, 116);
            this.lstDeducteeHelp.TabIndex = 172;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.lblFileRequest);
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(168, 382);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(670, 44);
            this.grpProgress.TabIndex = 173;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            // 
            // lblFileRequest
            // 
            this.lblFileRequest.AutoSize = true;
            this.lblFileRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileRequest.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFileRequest.Location = new System.Drawing.Point(72, 12);
            this.lblFileRequest.Name = "lblFileRequest";
            this.lblFileRequest.Size = new System.Drawing.Size(223, 13);
            this.lblFileRequest.TabIndex = 9;
            this.lblFileRequest.Text = "File has been Requested Successfully";
            this.lblFileRequest.Visible = false;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 30);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 8;
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(221, 428);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(234, 55);
            this.picCaptcha.TabIndex = 174;
            this.picCaptcha.TabStop = false;
            // 
            // btnCaptcha
            // 
            this.btnCaptcha.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptcha.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptcha.BackgroundImage")));
            this.btnCaptcha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptcha.Location = new System.Drawing.Point(458, 432);
            this.btnCaptcha.Name = "btnCaptcha";
            this.btnCaptcha.Size = new System.Drawing.Size(38, 40);
            this.btnCaptcha.TabIndex = 175;
            this.btnCaptcha.TabStop = false;
            this.btnCaptcha.UseVisualStyleBackColor = false;
            this.btnCaptcha.Click += new System.EventHandler(this.btnCaptcha_Click);
            this.btnCaptcha.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCaptcha_MouseMove);
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptcha.Location = new System.Drawing.Point(675, 443);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(136, 20);
            this.txtCaptcha.TabIndex = 2;
            // 
            // lblCaptcha
            // 
            this.lblCaptcha.AutoSize = true;
            this.lblCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCaptcha.Location = new System.Drawing.Point(519, 446);
            this.lblCaptcha.Name = "lblCaptcha";
            this.lblCaptcha.Size = new System.Drawing.Size(152, 13);
            this.lblCaptcha.TabIndex = 178;
            this.lblCaptcha.Text = "Enter text as in the image";
            // 
            // grpTextMessage
            // 
            this.grpTextMessage.Controls.Add(this.lblTextMessage6);
            this.grpTextMessage.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextMessage.Location = new System.Drawing.Point(167, 329);
            this.grpTextMessage.Name = "grpTextMessage";
            this.grpTextMessage.Size = new System.Drawing.Size(673, 53);
            this.grpTextMessage.TabIndex = 220;
            this.grpTextMessage.TabStop = false;
            this.grpTextMessage.Visible = false;
            // 
            // lblTextMessage6
            // 
            this.lblTextMessage6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTextMessage6.Location = new System.Drawing.Point(5, 12);
            this.lblTextMessage6.Name = "lblTextMessage6";
            this.lblTextMessage6.Size = new System.Drawing.Size(663, 35);
            this.lblTextMessage6.TabIndex = 207;
            this.lblTextMessage6.Text = "Please click \'Logout\' after completing the task.";
            this.lblTextMessage6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(41, 32);
            this.pctVideoDemo.TabIndex = 210;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Demo";
            this.pctVideoDemo.Visible = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(956, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 211;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Visible = false;
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // lnkRequestPAN
            // 
            this.lnkRequestPAN.AutoSize = true;
            this.lnkRequestPAN.Location = new System.Drawing.Point(597, 20);
            this.lnkRequestPAN.Name = "lnkRequestPAN";
            this.lnkRequestPAN.Size = new System.Drawing.Size(105, 13);
            this.lnkRequestPAN.TabIndex = 212;
            this.lnkRequestPAN.TabStop = true;
            this.lnkRequestPAN.Text = "Selective PANs Only";
            this.lnkRequestPAN.Visible = false;
            this.lnkRequestPAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkRequestPAN_LinkClicked);
            // 
            // grpPANLists
            // 
            this.grpPANLists.BackColor = System.Drawing.Color.DarkGray;
            this.grpPANLists.Controls.Add(this.label20);
            this.grpPANLists.Controls.Add(this.lblSearch);
            this.grpPANLists.Controls.Add(this.txtSearchPAN);
            this.grpPANLists.Controls.Add(this.label19);
            this.grpPANLists.Controls.Add(this.lblCounter);
            this.grpPANLists.Controls.Add(this.lblPANHeaderText);
            this.grpPANLists.Controls.Add(this.btnClose);
            this.grpPANLists.Controls.Add(this.chklstPANs);
            this.grpPANLists.Location = new System.Drawing.Point(220, 75);
            this.grpPANLists.Name = "grpPANLists";
            this.grpPANLists.Size = new System.Drawing.Size(544, 299);
            this.grpPANLists.TabIndex = 224;
            this.grpPANLists.TabStop = false;
            this.grpPANLists.Visible = false;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(176, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(343, 18);
            this.label20.TabIndex = 232;
            this.label20.Text = "Maximum 10 PAN(s) can be selected for request.";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 231;
            this.lblSearch.Text = "Search";
            // 
            // txtSearchPAN
            // 
            this.txtSearchPAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchPAN.Location = new System.Drawing.Point(55, 9);
            this.txtSearchPAN.MaxLength = 10;
            this.txtSearchPAN.Name = "txtSearchPAN";
            this.txtSearchPAN.Size = new System.Drawing.Size(113, 20);
            this.txtSearchPAN.TabIndex = 230;
            this.txtSearchPAN.TextChanged += new System.EventHandler(this.TxtSearchPAN_TextChanged);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(156, 277);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(380, 18);
            this.label19.TabIndex = 229;
            this.label19.Text = "Select the PAN(s) and click Request button below.";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCounter
            // 
            this.lblCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounter.ForeColor = System.Drawing.Color.Blue;
            this.lblCounter.Location = new System.Drawing.Point(13, 277);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(140, 17);
            this.lblCounter.TabIndex = 228;
            // 
            // lblPANHeaderText
            // 
            this.lblPANHeaderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPANHeaderText.ForeColor = System.Drawing.Color.Blue;
            this.lblPANHeaderText.Location = new System.Drawing.Point(13, 10);
            this.lblPANHeaderText.Name = "lblPANHeaderText";
            this.lblPANHeaderText.Size = new System.Drawing.Size(508, 0);
            this.lblPANHeaderText.TabIndex = 226;
            this.lblPANHeaderText.Text = "Download request can be submitted for maximum of 10 PANs.";
            this.lblPANHeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(524, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 225;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // chklstPANs
            // 
            this.chklstPANs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chklstPANs.CheckOnClick = true;
            this.chklstPANs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chklstPANs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.chklstPANs.FormattingEnabled = true;
            this.chklstPANs.Location = new System.Drawing.Point(9, 31);
            this.chklstPANs.Name = "chklstPANs";
            this.chklstPANs.Size = new System.Drawing.Size(527, 242);
            this.chklstPANs.TabIndex = 224;
            this.chklstPANs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChklstPANs_ItemCheck);
            this.chklstPANs.Click += new System.EventHandler(this.ChklstPANs_Click);
            this.chklstPANs.SelectedValueChanged += new System.EventHandler(this.ChklstPANs_SelectedValueChanged);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.WorkerReportsProgress = true;
            this.bgWorkerLoadCaptcha.WorkerSupportsCancellation = true;
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // lnkDownloadUtility
            // 
            this.lnkDownloadUtility.AutoSize = true;
            this.lnkDownloadUtility.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownloadUtility.Location = new System.Drawing.Point(725, 20);
            this.lnkDownloadUtility.Name = "lnkDownloadUtility";
            this.lnkDownloadUtility.Size = new System.Drawing.Size(186, 13);
            this.lnkDownloadUtility.TabIndex = 213;
            this.lnkDownloadUtility.TabStop = true;
            this.lnkDownloadUtility.Text = "Download PDF Converter Utility";
            this.lnkDownloadUtility.Visible = false;
            this.lnkDownloadUtility.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkDownloadUtility_LinkClicked);
            // 
            // TrnRequestTracesITAct2025
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1016, 577);
            this.Name = "TrnRequestTracesITAct2025";
            this.Activated += new System.EventHandler(this.TrnRequestConsolidatedFile_Activated);
            this.Load += new System.EventHandler(this.TrnViewReturnStatusOnline_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.grpButton.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpReturnInfo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpTextMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.grpPANLists.ResumeLayout(false);
            this.grpPANLists.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpReturnInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTAN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgwWorker;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblFileRequest;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.Button btnCaptcha;
        private System.Windows.Forms.Label lblCaptcha;
        private System.Windows.Forms.GroupBox grpTextMessage;
        private System.Windows.Forms.Label lblTextMessage6;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel lnkRequestPAN;
        private System.Windows.Forms.GroupBox grpPANLists;
        private System.Windows.Forms.CheckedListBox chklstPANs;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPANHeaderText;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtSearchPAN;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label20;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.LinkLabel lnkPre2627;
        private System.Windows.Forms.LinkLabel lnkDownloadUtility;
    }
}
