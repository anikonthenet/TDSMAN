namespace TDSMAN.FormWeb
{
    partial class TrnDeclarationNonFilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDeclarationNonFilling));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
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
            this.grpInputDetails = new System.Windows.Forms.GroupBox();
            this.txtspecReasn = new System.Windows.Forms.TextBox();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpListStatement = new System.Windows.Forms.GroupBox();
            this.btnAddStatement = new System.Windows.Forms.Button();
            this.btnChangeFilling = new System.Windows.Forms.Button();
            this.dgvStatementList = new System.Windows.Forms.DataGridView();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
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
            this.grpInputDetails.SuspendLayout();
            this.grpListStatement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
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
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpListStatement);
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1012, 607);
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
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.lstDeducteeHelp);
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.label3);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(84, 77);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 293);
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
            this.lstDeducteeHelp.Location = new System.Drawing.Point(136, 60);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 116);
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
            this.txtTANNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTANNo.Leave += new System.EventHandler(this.txtTAN_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(230, 18);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(10, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(275, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(202, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 21);
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
            this.label3.Location = new System.Drawing.Point(166, 192);
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
            this.btnCaptchaRefresh.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCaptchaRefresh_MouseMove);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(242, 119);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(279, 55);
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
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(124, 471);
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
            this.lnkLogOff.Location = new System.Drawing.Point(848, 20);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 195;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.grpInputDetails);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 9);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 448);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // grpInputDetails
            // 
            this.grpInputDetails.Controls.Add(this.txtspecReasn);
            this.grpInputDetails.Controls.Add(this.cmbReason);
            this.grpInputDetails.Controls.Add(this.label8);
            this.grpInputDetails.Controls.Add(this.btnGo);
            this.grpInputDetails.Controls.Add(this.cmbQtr);
            this.grpInputDetails.Controls.Add(this.cmbFormNo);
            this.grpInputDetails.Controls.Add(this.cmbFAYear);
            this.grpInputDetails.Controls.Add(this.label4);
            this.grpInputDetails.Controls.Add(this.label6);
            this.grpInputDetails.Controls.Add(this.label7);
            this.grpInputDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInputDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpInputDetails.Location = new System.Drawing.Point(3, 94);
            this.grpInputDetails.Name = "grpInputDetails";
            this.grpInputDetails.Size = new System.Drawing.Size(946, 231);
            this.grpInputDetails.TabIndex = 196;
            this.grpInputDetails.TabStop = false;
            this.grpInputDetails.Text = "Statement Details";
            // 
            // txtspecReasn
            // 
            this.txtspecReasn.Location = new System.Drawing.Point(494, 95);
            this.txtspecReasn.MaxLength = 50;
            this.txtspecReasn.Name = "txtspecReasn";
            this.txtspecReasn.Size = new System.Drawing.Size(264, 20);
            this.txtspecReasn.TabIndex = 214;
            this.txtspecReasn.Visible = false;
            // 
            // cmbReason
            // 
            this.cmbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReason.FormattingEnabled = true;
            this.cmbReason.Location = new System.Drawing.Point(156, 94);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(326, 21);
            this.cmbReason.TabIndex = 212;
            this.cmbReason.SelectedIndexChanged += new System.EventHandler(this.cmbReason_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(100, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 213;
            this.label8.Text = "Reason";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.Lavender;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(423, 156);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(81, 23);
            this.btnGo.TabIndex = 211;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbQtr
            // 
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(559, 65);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(92, 21);
            this.cmbQtr.TabIndex = 2;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(371, 65);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(96, 21);
            this.cmbFormNo.TabIndex = 1;
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(157, 65);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(124, 21);
            this.cmbFAYear.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(504, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quarter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(313, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Form No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(98, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "FA Year";
            // 
            // grpListStatement
            // 
            this.grpListStatement.Controls.Add(this.btnAddStatement);
            this.grpListStatement.Controls.Add(this.btnChangeFilling);
            this.grpListStatement.Controls.Add(this.dgvStatementList);
            this.grpListStatement.Location = new System.Drawing.Point(33, 36);
            this.grpListStatement.Name = "grpListStatement";
            this.grpListStatement.Size = new System.Drawing.Size(943, 392);
            this.grpListStatement.TabIndex = 198;
            this.grpListStatement.TabStop = false;
            this.grpListStatement.Visible = false;
            // 
            // btnAddStatement
            // 
            this.btnAddStatement.BackColor = System.Drawing.Color.Lavender;
            this.btnAddStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStatement.Location = new System.Drawing.Point(460, 340);
            this.btnAddStatement.Name = "btnAddStatement";
            this.btnAddStatement.Size = new System.Drawing.Size(148, 23);
            this.btnAddStatement.TabIndex = 213;
            this.btnAddStatement.Text = "Add Statements";
            this.btnAddStatement.UseVisualStyleBackColor = false;
            this.btnAddStatement.Click += new System.EventHandler(this.btnAddStatement_Click);
            // 
            // btnChangeFilling
            // 
            this.btnChangeFilling.BackColor = System.Drawing.Color.Lavender;
            this.btnChangeFilling.Enabled = false;
            this.btnChangeFilling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeFilling.Location = new System.Drawing.Point(310, 340);
            this.btnChangeFilling.Name = "btnChangeFilling";
            this.btnChangeFilling.Size = new System.Drawing.Size(148, 23);
            this.btnChangeFilling.TabIndex = 212;
            this.btnChangeFilling.Text = "Change Filling Status";
            this.btnChangeFilling.UseVisualStyleBackColor = false;
            this.btnChangeFilling.Visible = false;
            // 
            // dgvStatementList
            // 
            this.dgvStatementList.AllowUserToAddRows = false;
            this.dgvStatementList.AllowUserToDeleteRows = false;
            this.dgvStatementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementList.Location = new System.Drawing.Point(8, 34);
            this.dgvStatementList.Name = "dgvStatementList";
            this.dgvStatementList.ReadOnly = true;
            this.dgvStatementList.Size = new System.Drawing.Size(924, 281);
            this.dgvStatementList.TabIndex = 15;
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
            // TrnDeclarationNonFilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 480);
            this.Name = "TrnDeclarationNonFilling";
            this.Activated += new System.EventHandler(this.TrnStatementStatusTraces_Activated);
            this.Load += new System.EventHandler(this.TrnStatementStatusTraces_Load);
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
            this.grpInputDetails.ResumeLayout(false);
            this.grpInputDetails.PerformLayout();
            this.grpListStatement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
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
        private System.Windows.Forms.GroupBox grpListStatement;
        private System.Windows.Forms.Button btnAddStatement;
        private System.Windows.Forms.Button btnChangeFilling;
        private System.Windows.Forms.DataGridView dgvStatementList;
        private System.Windows.Forms.GroupBox grpInputDetails;
        private System.Windows.Forms.TextBox txtspecReasn;
        private System.Windows.Forms.ComboBox cmbReason;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
