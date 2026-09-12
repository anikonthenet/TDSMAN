namespace TDSMAN.FormTrn
{
    partial class TrnBulk197CertificateValidation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnBulk197CertificateValidation));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dgvDeductees = new DGVControl.DGVControl();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnVerification = new System.Windows.Forms.Button();
            this.pnlBottomLine = new System.Windows.Forms.Panel();
            this.bgwPANVerification = new System.ComponentModel.BackgroundWorker();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnPrintVerified = new System.Windows.Forms.Button();
            this.lblUnmatchedNo = new System.Windows.Forms.Label();
            this.lblUnmatched = new System.Windows.Forms.Label();
            this.btnPrintInvalidPAN = new System.Windows.Forms.Button();
            this.lblNotVerified = new System.Windows.Forms.Label();
            this.lblInvalidNo = new System.Windows.Forms.Label();
            this.lblVerifiedNo = new System.Windows.Forms.Label();
            this.lblVerified = new System.Windows.Forms.Label();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnLogging = new System.Windows.Forms.Button();
            this.grpEnterLoginDetails = new System.Windows.Forms.GroupBox();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpCaptcha = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpReturnSelection = new System.Windows.Forms.GroupBox();
            this.rbnCorrectionReturn = new System.Windows.Forms.RadioButton();
            this.rbnRegularReturn = new System.Windows.Forms.RadioButton();
            this.grpRegularReturn = new System.Windows.Forms.GroupBox();
            this.btnUpdateMasterData = new System.Windows.Forms.Button();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.grpCorrectionReturn = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgcViewBatch = new DGVControl.DGVControl();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.chkBoxNewEntriesOnly = new System.Windows.Forms.CheckBox();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.txtDestinationFileNameHidden = new System.Windows.Forms.TextBox();
            this.btnExitExportPanel = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblFileFolderName = new System.Windows.Forms.Label();
            this.txtDestinationFileName = new System.Windows.Forms.TextBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.cntxtMnuGrpInvalidPAN = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntxtMenuPrintInvalidPAN = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCntxtMenuExportInvalidPAN = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtMnuGrpNameDifference = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntxtMenuPrintNameDifference = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCntxtMenuExportNameDifference = new System.Windows.Forms.ToolStripMenuItem();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).BeginInit();
            this.grpButtons.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpLoginDetails.SuspendLayout();
            this.grpEnterLoginDetails.SuspendLayout();
            this.grpCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpReturnSelection.SuspendLayout();
            this.grpRegularReturn.SuspendLayout();
            this.grpCorrectionReturn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.grpExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.cntxtMnuGrpInvalidPAN.SuspendLayout();
            this.cntxtMnuGrpNameDifference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(851, 8);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(175, 24);
            this.lblSearchMode.TabIndex = 48;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(181, 8);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(670, 24);
            this.pnlTitle.TabIndex = 46;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(0, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(670, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Verify 197 Certificate (bulk)";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(7, 8);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 47;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(7, 38);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1019, 3);
            this.pnlHeader.TabIndex = 49;
            // 
            // dgvDeductees
            // 
            this.dgvDeductees.AllowUserToAddRows = false;
            this.dgvDeductees.AllowUserToDeleteRows = false;
            this.dgvDeductees.AllowUserToOrderColumns = true;
            this.dgvDeductees.AllowUserToResizeRows = false;
            this.dgvDeductees.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvDeductees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeductees.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDeductees.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDeductees.Location = new System.Drawing.Point(164, 200);
            this.dgvDeductees.MultiSelect = false;
            this.dgvDeductees.Name = "dgvDeductees";
            this.dgvDeductees.RowHeadersWidth = 20;
            this.dgvDeductees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDeductees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeductees.Size = new System.Drawing.Size(710, 391);
            this.dgvDeductees.TabIndex = 185;
            this.dgvDeductees.TabStop = false;
            this.dgvDeductees.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDeductees_ColumnAdded);
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnXit);
            this.grpButtons.Controls.Add(this.btnVerification);
            this.grpButtons.Location = new System.Drawing.Point(664, 597);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(209, 38);
            this.grpButtons.TabIndex = 187;
            this.grpButtons.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(126, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "E&xit";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnVerification
            // 
            this.btnVerification.BackColor = System.Drawing.Color.Lavender;
            this.btnVerification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerification.ForeColor = System.Drawing.Color.Black;
            this.btnVerification.Location = new System.Drawing.Point(4, 11);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(121, 23);
            this.btnVerification.TabIndex = 189;
            this.btnVerification.Text = "&Start Validation";
            this.btnVerification.UseVisualStyleBackColor = false;
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // pnlBottomLine
            // 
            this.pnlBottomLine.BackColor = System.Drawing.Color.Black;
            this.pnlBottomLine.Location = new System.Drawing.Point(165, 595);
            this.pnlBottomLine.Name = "pnlBottomLine";
            this.pnlBottomLine.Size = new System.Drawing.Size(709, 1);
            this.pnlBottomLine.TabIndex = 188;
            // 
            // bgwPANVerification
            // 
            this.bgwPANVerification.WorkerSupportsCancellation = true;
            this.bgwPANVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPANVerification_DoWork);
            this.bgwPANVerification.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwPANVerification_ProgressChanged);
            this.bgwPANVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwPANVerification_RunWorkerCompleted);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.btnPrintVerified);
            this.grpStatus.Controls.Add(this.lblUnmatchedNo);
            this.grpStatus.Controls.Add(this.lblUnmatched);
            this.grpStatus.Controls.Add(this.btnPrintInvalidPAN);
            this.grpStatus.Controls.Add(this.lblNotVerified);
            this.grpStatus.Controls.Add(this.lblInvalidNo);
            this.grpStatus.Controls.Add(this.lblVerifiedNo);
            this.grpStatus.Controls.Add(this.lblVerified);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.ForeColor = System.Drawing.Color.Black;
            this.grpStatus.Location = new System.Drawing.Point(166, 597);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(494, 38);
            this.grpStatus.TabIndex = 189;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Total Records";
            // 
            // btnPrintVerified
            // 
            this.btnPrintVerified.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintVerified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintVerified.Enabled = false;
            this.btnPrintVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintVerified.ForeColor = System.Drawing.Color.Black;
            this.btnPrintVerified.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintVerified.Image")));
            this.btnPrintVerified.Location = new System.Drawing.Point(225, 10);
            this.btnPrintVerified.Name = "btnPrintVerified";
            this.btnPrintVerified.Size = new System.Drawing.Size(32, 27);
            this.btnPrintVerified.TabIndex = 194;
            this.btnPrintVerified.UseVisualStyleBackColor = false;
            this.btnPrintVerified.Click += new System.EventHandler(this.btnPrintUnmatched_Click);
            this.btnPrintVerified.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPrintUnmatched_MouseClick);
            this.btnPrintVerified.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPrintUnmatched_MouseMove);
            // 
            // lblUnmatchedNo
            // 
            this.lblUnmatchedNo.BackColor = System.Drawing.Color.Yellow;
            this.lblUnmatchedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnmatchedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatchedNo.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatchedNo.Location = new System.Drawing.Point(436, 13);
            this.lblUnmatchedNo.Name = "lblUnmatchedNo";
            this.lblUnmatchedNo.Size = new System.Drawing.Size(45, 21);
            this.lblUnmatchedNo.TabIndex = 193;
            this.lblUnmatchedNo.Text = "0";
            this.lblUnmatchedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUnmatchedNo.Visible = false;
            // 
            // lblUnmatched
            // 
            this.lblUnmatched.AutoSize = true;
            this.lblUnmatched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatched.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatched.Location = new System.Drawing.Point(365, 17);
            this.lblUnmatched.Name = "lblUnmatched";
            this.lblUnmatched.Size = new System.Drawing.Size(67, 13);
            this.lblUnmatched.TabIndex = 192;
            this.lblUnmatched.Text = "Amount error";
            this.lblUnmatched.Visible = false;
            // 
            // btnPrintInvalidPAN
            // 
            this.btnPrintInvalidPAN.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintInvalidPAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintInvalidPAN.Enabled = false;
            this.btnPrintInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintInvalidPAN.ForeColor = System.Drawing.Color.Black;
            this.btnPrintInvalidPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintInvalidPAN.Image")));
            this.btnPrintInvalidPAN.Location = new System.Drawing.Point(373, 10);
            this.btnPrintInvalidPAN.Name = "btnPrintInvalidPAN";
            this.btnPrintInvalidPAN.Size = new System.Drawing.Size(32, 27);
            this.btnPrintInvalidPAN.TabIndex = 191;
            this.btnPrintInvalidPAN.UseVisualStyleBackColor = false;
            this.btnPrintInvalidPAN.Click += new System.EventHandler(this.btnPrintInvalidPAN_Click);
            this.btnPrintInvalidPAN.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPrintInvalidPAN_MouseClick);
            this.btnPrintInvalidPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPrintInvalidPAN_MouseMove);
            // 
            // lblNotVerified
            // 
            this.lblNotVerified.AutoSize = true;
            this.lblNotVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerified.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerified.Location = new System.Drawing.Point(283, 16);
            this.lblNotVerified.Name = "lblNotVerified";
            this.lblNotVerified.Size = new System.Drawing.Size(38, 13);
            this.lblNotVerified.TabIndex = 21;
            this.lblNotVerified.Text = "Invalid";
            // 
            // lblInvalidNo
            // 
            this.lblInvalidNo.BackColor = System.Drawing.Color.Red;
            this.lblInvalidNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvalidNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidNo.ForeColor = System.Drawing.Color.Black;
            this.lblInvalidNo.Location = new System.Drawing.Point(326, 13);
            this.lblInvalidNo.Name = "lblInvalidNo";
            this.lblInvalidNo.Size = new System.Drawing.Size(45, 21);
            this.lblInvalidNo.TabIndex = 20;
            this.lblInvalidNo.Text = "0";
            this.lblInvalidNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInvalidNo.TextChanged += new System.EventHandler(this.lblInvalidNo_TextChanged);
            // 
            // lblVerifiedNo
            // 
            this.lblVerifiedNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblVerifiedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifiedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifiedNo.ForeColor = System.Drawing.Color.Black;
            this.lblVerifiedNo.Location = new System.Drawing.Point(168, 13);
            this.lblVerifiedNo.Name = "lblVerifiedNo";
            this.lblVerifiedNo.Size = new System.Drawing.Size(55, 21);
            this.lblVerifiedNo.TabIndex = 18;
            this.lblVerifiedNo.Text = "0";
            this.lblVerifiedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerified
            // 
            this.lblVerified.AutoSize = true;
            this.lblVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerified.ForeColor = System.Drawing.Color.Black;
            this.lblVerified.Location = new System.Drawing.Point(124, 17);
            this.lblVerified.Name = "lblVerified";
            this.lblVerified.Size = new System.Drawing.Size(30, 13);
            this.lblVerified.TabIndex = 17;
            this.lblVerified.Text = "Valid";
            // 
            // grpLoginDetails
            // 
            this.grpLoginDetails.BackColor = System.Drawing.Color.PeachPuff;
            this.grpLoginDetails.Controls.Add(this.lstDeducteeHelp);
            this.grpLoginDetails.Controls.Add(this.btnClose);
            this.grpLoginDetails.Controls.Add(this.pBar);
            this.grpLoginDetails.Controls.Add(this.btnLogging);
            this.grpLoginDetails.Controls.Add(this.grpEnterLoginDetails);
            this.grpLoginDetails.Controls.Add(this.grpCaptcha);
            this.grpLoginDetails.Location = new System.Drawing.Point(164, 255);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(711, 211);
            this.grpLoginDetails.TabIndex = 2;
            this.grpLoginDetails.TabStop = false;
            this.grpLoginDetails.Visible = false;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(104, 49);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 100);
            this.lstDeducteeHelp.TabIndex = 218;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Lavender;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(660, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 25);
            this.btnClose.TabIndex = 204;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(35, 165);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 202;
            // 
            // btnLogging
            // 
            this.btnLogging.BackColor = System.Drawing.Color.Lavender;
            this.btnLogging.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogging.ForeColor = System.Drawing.Color.Black;
            this.btnLogging.Location = new System.Drawing.Point(613, 182);
            this.btnLogging.Name = "btnLogging";
            this.btnLogging.Size = new System.Drawing.Size(47, 25);
            this.btnLogging.TabIndex = 201;
            this.btnLogging.Text = "&Go";
            this.btnLogging.UseVisualStyleBackColor = false;
            this.btnLogging.Click += new System.EventHandler(this.btnLogging_Click);
            // 
            // grpEnterLoginDetails
            // 
            this.grpEnterLoginDetails.Controls.Add(this.txtTANNo);
            this.grpEnterLoginDetails.Controls.Add(this.label5);
            this.grpEnterLoginDetails.Controls.Add(this.txtUserID);
            this.grpEnterLoginDetails.Controls.Add(this.txtPassword);
            this.grpEnterLoginDetails.Controls.Add(this.label2);
            this.grpEnterLoginDetails.Controls.Add(this.label1);
            this.grpEnterLoginDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEnterLoginDetails.Location = new System.Drawing.Point(7, 12);
            this.grpEnterLoginDetails.Name = "grpEnterLoginDetails";
            this.grpEnterLoginDetails.Size = new System.Drawing.Size(698, 41);
            this.grpEnterLoginDetails.TabIndex = 0;
            this.grpEnterLoginDetails.TabStop = false;
            this.grpEnterLoginDetails.Text = "TRACES Login Details";
            // 
            // txtTANNo
            // 
            this.txtTANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(97, 15);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.Size = new System.Drawing.Size(116, 20);
            this.txtTANNo.TabIndex = 0;
            this.txtTANNo.TextChanged += new System.EventHandler(this.txtTANNo_TextChanged);
            this.txtTANNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTANNo_KeyDown);
            this.txtTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTANNo.Leave += new System.EventHandler(this.txtTANNo_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserID.Location = new System.Drawing.Point(282, 15);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(145, 20);
            this.txtUserID.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(504, 15);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(439, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 206;
            this.label1.Text = "User ID";
            // 
            // grpCaptcha
            // 
            this.grpCaptcha.Controls.Add(this.label3);
            this.grpCaptcha.Controls.Add(this.btnCaptchaRefresh);
            this.grpCaptcha.Controls.Add(this.picCaptcha);
            this.grpCaptcha.Controls.Add(this.txtCaptchaCode);
            this.grpCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCaptcha.Location = new System.Drawing.Point(7, 57);
            this.grpCaptcha.Name = "grpCaptcha";
            this.grpCaptcha.Size = new System.Drawing.Size(696, 98);
            this.grpCaptcha.TabIndex = 1;
            this.grpCaptcha.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(207, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 204;
            this.label3.Text = "Enter text as per image";
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(423, 18);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 203;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picCaptcha
            // 
            this.picCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCaptcha.Location = new System.Drawing.Point(236, 15);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(186, 47);
            this.picCaptcha.TabIndex = 202;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(349, 70);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(140, 20);
            this.txtCaptchaCode.TabIndex = 0;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // grpReturnSelection
            // 
            this.grpReturnSelection.Controls.Add(this.rbnCorrectionReturn);
            this.grpReturnSelection.Controls.Add(this.rbnRegularReturn);
            this.grpReturnSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReturnSelection.Location = new System.Drawing.Point(334, 38);
            this.grpReturnSelection.Name = "grpReturnSelection";
            this.grpReturnSelection.Size = new System.Drawing.Size(371, 37);
            this.grpReturnSelection.TabIndex = 190;
            this.grpReturnSelection.TabStop = false;
            // 
            // rbnCorrectionReturn
            // 
            this.rbnCorrectionReturn.AutoSize = true;
            this.rbnCorrectionReturn.Location = new System.Drawing.Point(189, 12);
            this.rbnCorrectionReturn.Name = "rbnCorrectionReturn";
            this.rbnCorrectionReturn.Size = new System.Drawing.Size(138, 19);
            this.rbnCorrectionReturn.TabIndex = 1;
            this.rbnCorrectionReturn.Text = "Correction Return";
            this.rbnCorrectionReturn.UseVisualStyleBackColor = true;
            this.rbnCorrectionReturn.CheckedChanged += new System.EventHandler(this.rbnRegularCorrectionReturn_CheckedChanged);
            // 
            // rbnRegularReturn
            // 
            this.rbnRegularReturn.AutoSize = true;
            this.rbnRegularReturn.Checked = true;
            this.rbnRegularReturn.Location = new System.Drawing.Point(44, 12);
            this.rbnRegularReturn.Name = "rbnRegularReturn";
            this.rbnRegularReturn.Size = new System.Drawing.Size(123, 19);
            this.rbnRegularReturn.TabIndex = 0;
            this.rbnRegularReturn.TabStop = true;
            this.rbnRegularReturn.Text = "Regular Return";
            this.rbnRegularReturn.UseVisualStyleBackColor = true;
            this.rbnRegularReturn.CheckedChanged += new System.EventHandler(this.rbnRegularCorrectionReturn_CheckedChanged);
            // 
            // grpRegularReturn
            // 
            this.grpRegularReturn.Controls.Add(this.btnUpdateMasterData);
            this.grpRegularReturn.Controls.Add(this.lnkSearchByTAN);
            this.grpRegularReturn.Controls.Add(this.cmbFormNo);
            this.grpRegularReturn.Controls.Add(this.label4);
            this.grpRegularReturn.Controls.Add(this.cmbQuarter);
            this.grpRegularReturn.Controls.Add(this.label6);
            this.grpRegularReturn.Controls.Add(this.cmbFinancialYear);
            this.grpRegularReturn.Controls.Add(this.label7);
            this.grpRegularReturn.Controls.Add(this.cmbCompany);
            this.grpRegularReturn.Controls.Add(this.label8);
            this.grpRegularReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRegularReturn.Location = new System.Drawing.Point(164, 73);
            this.grpRegularReturn.Name = "grpRegularReturn";
            this.grpRegularReturn.Size = new System.Drawing.Size(710, 118);
            this.grpRegularReturn.TabIndex = 191;
            this.grpRegularReturn.TabStop = false;
            // 
            // btnUpdateMasterData
            // 
            this.btnUpdateMasterData.BackColor = System.Drawing.Color.Lavender;
            this.btnUpdateMasterData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateMasterData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdateMasterData.Location = new System.Drawing.Point(563, 88);
            this.btnUpdateMasterData.Name = "btnUpdateMasterData";
            this.btnUpdateMasterData.Size = new System.Drawing.Size(136, 26);
            this.btnUpdateMasterData.TabIndex = 219;
            this.btnUpdateMasterData.Text = "Update Master Data";
            this.btnUpdateMasterData.UseVisualStyleBackColor = false;
            this.btnUpdateMasterData.Visible = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(611, 67);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 218;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.ForeColor = System.Drawing.Color.Black;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(402, 33);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(106, 23);
            this.cmbFormNo.TabIndex = 209;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(304, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 210;
            this.label4.Text = "Select Form No";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.ForeColor = System.Drawing.Color.Black;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(627, 33);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(69, 23);
            this.cmbQuarter.TabIndex = 204;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(534, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 208;
            this.label6.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(136, 33);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 23);
            this.cmbFinancialYear.TabIndex = 203;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 207;
            this.label7.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(136, 64);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(472, 23);
            this.cmbCompany.TabIndex = 205;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(34, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 206;
            this.label8.Text = "Select Company";
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.Black;
            this.pnlLine.Location = new System.Drawing.Point(164, 196);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(710, 1);
            this.pnlLine.TabIndex = 192;
            // 
            // grpCorrectionReturn
            // 
            this.grpCorrectionReturn.Controls.Add(this.label9);
            this.grpCorrectionReturn.Controls.Add(this.dgcViewBatch);
            this.grpCorrectionReturn.Location = new System.Drawing.Point(164, 73);
            this.grpCorrectionReturn.Name = "grpCorrectionReturn";
            this.grpCorrectionReturn.Size = new System.Drawing.Size(710, 120);
            this.grpCorrectionReturn.TabIndex = 193;
            this.grpCorrectionReturn.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(2, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 210;
            this.label9.Text = "Select Return";
            // 
            // dgcViewBatch
            // 
            this.dgcViewBatch.AllowUserToAddRows = false;
            this.dgcViewBatch.AllowUserToDeleteRows = false;
            this.dgcViewBatch.AllowUserToOrderColumns = true;
            this.dgcViewBatch.AllowUserToResizeRows = false;
            this.dgcViewBatch.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewBatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewBatch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewBatch.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewBatch.Location = new System.Drawing.Point(5, 26);
            this.dgcViewBatch.MultiSelect = false;
            this.dgcViewBatch.Name = "dgcViewBatch";
            this.dgcViewBatch.RowHeadersWidth = 20;
            this.dgcViewBatch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewBatch.Size = new System.Drawing.Size(700, 89);
            this.dgcViewBatch.TabIndex = 187;
            this.dgcViewBatch.TabStop = false;
            this.dgcViewBatch.CurrentCellChanged += new System.EventHandler(this.dgcViewBatch_CurrentCellChanged);
            this.dgcViewBatch.Click += new System.EventHandler(this.dgcViewBatch_Click);
            this.dgcViewBatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgcViewBatch_KeyDown);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(877, 600);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 211;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Visible = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // chkBoxNewEntriesOnly
            // 
            this.chkBoxNewEntriesOnly.AutoSize = true;
            this.chkBoxNewEntriesOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxNewEntriesOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxNewEntriesOnly.Location = new System.Drawing.Point(716, 51);
            this.chkBoxNewEntriesOnly.Name = "chkBoxNewEntriesOnly";
            this.chkBoxNewEntriesOnly.Size = new System.Drawing.Size(123, 17);
            this.chkBoxNewEntriesOnly.TabIndex = 213;
            this.chkBoxNewEntriesOnly.Text = "New Entries Only";
            this.chkBoxNewEntriesOnly.UseVisualStyleBackColor = true;
            this.chkBoxNewEntriesOnly.Visible = false;
            this.chkBoxNewEntriesOnly.CheckedChanged += new System.EventHandler(this.chkBoxNewEntriesOnly_CheckedChanged);
            // 
            // grpExport
            // 
            this.grpExport.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.grpExport.Controls.Add(this.txtDestinationFileNameHidden);
            this.grpExport.Controls.Add(this.btnExitExportPanel);
            this.grpExport.Controls.Add(this.btnExportToExcel);
            this.grpExport.Controls.Add(this.groupBox1);
            this.grpExport.Controls.Add(this.groupBox4);
            this.grpExport.Location = new System.Drawing.Point(148, 257);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(742, 115);
            this.grpExport.TabIndex = 214;
            this.grpExport.TabStop = false;
            this.grpExport.Visible = false;
            // 
            // txtDestinationFileNameHidden
            // 
            this.txtDestinationFileNameHidden.Location = new System.Drawing.Point(482, 46);
            this.txtDestinationFileNameHidden.Name = "txtDestinationFileNameHidden";
            this.txtDestinationFileNameHidden.Size = new System.Drawing.Size(19, 20);
            this.txtDestinationFileNameHidden.TabIndex = 149;
            this.txtDestinationFileNameHidden.Visible = false;
            // 
            // btnExitExportPanel
            // 
            this.btnExitExportPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExitExportPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnExitExportPanel.Image")));
            this.btnExitExportPanel.Location = new System.Drawing.Point(717, 9);
            this.btnExitExportPanel.Name = "btnExitExportPanel";
            this.btnExitExportPanel.Size = new System.Drawing.Size(22, 20);
            this.btnExitExportPanel.TabIndex = 70;
            this.btnExitExportPanel.UseVisualStyleBackColor = true;
            this.btnExitExportPanel.Click += new System.EventHandler(this.btnExitExportPanel_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnExportToExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.Location = new System.Drawing.Point(642, 76);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(73, 26);
            this.btnExportToExcel.TabIndex = 69;
            this.btnExportToExcel.Text = "&Export";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(8, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 10);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            this.groupBox1.Visible = false;
            // 
            // lblPrcnt
            // 
            this.lblPrcnt.AutoSize = true;
            this.lblPrcnt.BackColor = System.Drawing.Color.Transparent;
            this.lblPrcnt.Location = new System.Drawing.Point(356, 27);
            this.lblPrcnt.Name = "lblPrcnt";
            this.lblPrcnt.Size = new System.Drawing.Size(0, 13);
            this.lblPrcnt.TabIndex = 149;
            this.lblPrcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(676, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblFileFolderName);
            this.groupBox4.Controls.Add(this.txtDestinationFileName);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(8, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(707, 61);
            this.groupBox4.TabIndex = 67;
            this.groupBox4.TabStop = false;
            // 
            // lblFileFolderName
            // 
            this.lblFileFolderName.AutoSize = true;
            this.lblFileFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileFolderName.Location = new System.Drawing.Point(9, 39);
            this.lblFileFolderName.Name = "lblFileFolderName";
            this.lblFileFolderName.Size = new System.Drawing.Size(57, 13);
            this.lblFileFolderName.TabIndex = 148;
            this.lblFileFolderName.Text = "Filename";
            // 
            // txtDestinationFileName
            // 
            this.txtDestinationFileName.Location = new System.Drawing.Point(97, 35);
            this.txtDestinationFileName.Name = "txtDestinationFileName";
            this.txtDestinationFileName.Size = new System.Drawing.Size(371, 20);
            this.txtDestinationFileName.TabIndex = 147;
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(642, 11);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(42, 22);
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
            this.txtExcelPath.Location = new System.Drawing.Point(97, 12);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(543, 20);
            this.txtExcelPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(10, 15);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(71, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Folder path";
            // 
            // cntxtMnuGrpInvalidPAN
            // 
            this.cntxtMnuGrpInvalidPAN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntxtMenuPrintInvalidPAN,
            this.mnuCntxtMenuExportInvalidPAN});
            this.cntxtMnuGrpInvalidPAN.Name = "cntxtMnuGrpInvalidPAN";
            this.cntxtMnuGrpInvalidPAN.Size = new System.Drawing.Size(154, 48);
            // 
            // mnuCntxtMenuPrintInvalidPAN
            // 
            this.mnuCntxtMenuPrintInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuPrintInvalidPAN.Name = "mnuCntxtMenuPrintInvalidPAN";
            this.mnuCntxtMenuPrintInvalidPAN.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuPrintInvalidPAN.Text = "Print";
            this.mnuCntxtMenuPrintInvalidPAN.Click += new System.EventHandler(this.mnuCntxtMenuPrint_Click);
            // 
            // mnuCntxtMenuExportInvalidPAN
            // 
            this.mnuCntxtMenuExportInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportInvalidPAN.Name = "mnuCntxtMenuExportInvalidPAN";
            this.mnuCntxtMenuExportInvalidPAN.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuExportInvalidPAN.Text = "Export to CSV";
            this.mnuCntxtMenuExportInvalidPAN.Click += new System.EventHandler(this.mnuCntxtMenuExport_Click);
            // 
            // cntxtMnuGrpNameDifference
            // 
            this.cntxtMnuGrpNameDifference.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntxtMenuPrintNameDifference,
            this.mnuCntxtMenuExportNameDifference});
            this.cntxtMnuGrpNameDifference.Name = "cntxtMnuGrpInvalidPAN";
            this.cntxtMnuGrpNameDifference.Size = new System.Drawing.Size(154, 48);
            // 
            // mnuCntxtMenuPrintNameDifference
            // 
            this.mnuCntxtMenuPrintNameDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuPrintNameDifference.Name = "mnuCntxtMenuPrintNameDifference";
            this.mnuCntxtMenuPrintNameDifference.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuPrintNameDifference.Text = "Print";
            this.mnuCntxtMenuPrintNameDifference.Click += new System.EventHandler(this.mnuCntxtMenuPrintNameDifference_Click);
            // 
            // mnuCntxtMenuExportNameDifference
            // 
            this.mnuCntxtMenuExportNameDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportNameDifference.Name = "mnuCntxtMenuExportNameDifference";
            this.mnuCntxtMenuExportNameDifference.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuExportNameDifference.Text = "Export to CSV";
            this.mnuCntxtMenuExportNameDifference.Click += new System.EventHandler(this.mnuCntxtMenuExportNameDifference_Click);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(832, 637);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 215;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.WorkerSupportsCancellation = true;
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnBulk197CertificateValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1038, 682);
            this.Controls.Add(this.grpLoginDetails);
            this.Controls.Add(this.pctUserManual);
            this.Controls.Add(this.grpExport);
            this.Controls.Add(this.chkBoxNewEntriesOnly);
            this.Controls.Add(this.pctVideoDemo);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.grpReturnSelection);
            this.Controls.Add(this.grpCorrectionReturn);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.pnlBottomLine);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.dgvDeductees);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.grpRegularReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrnBulk197CertificateValidation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.TrnBulkPANNameValidation_Activated);
            this.Load += new System.EventHandler(this.TrnBulkPANNameValidation_Load);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).EndInit();
            this.grpButtons.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpEnterLoginDetails.ResumeLayout(false);
            this.grpEnterLoginDetails.PerformLayout();
            this.grpCaptcha.ResumeLayout(false);
            this.grpCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpReturnSelection.ResumeLayout(false);
            this.grpReturnSelection.PerformLayout();
            this.grpRegularReturn.ResumeLayout(false);
            this.grpRegularReturn.PerformLayout();
            this.grpCorrectionReturn.ResumeLayout(false);
            this.grpCorrectionReturn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.grpExport.ResumeLayout(false);
            this.grpExport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.cntxtMnuGrpInvalidPAN.ResumeLayout(false);
            this.cntxtMnuGrpNameDifference.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlHeader;
        private DGVControl.DGVControl dgvDeductees;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnVerification;
        private System.Windows.Forms.Panel pnlBottomLine;
        private System.ComponentModel.BackgroundWorker bgwPANVerification;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblVerified;
        private System.Windows.Forms.Label lblVerifiedNo;
        private System.Windows.Forms.Label lblInvalidNo;
        private System.Windows.Forms.Label lblNotVerified;
        private System.Windows.Forms.Button btnPrintInvalidPAN;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.GroupBox grpEnterLoginDetails;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogging;
        private System.Windows.Forms.ProgressBar pBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpCaptcha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.Label lblUnmatchedNo;
        private System.Windows.Forms.Label lblUnmatched;
        private System.Windows.Forms.Button btnPrintVerified;
        private System.Windows.Forms.GroupBox grpReturnSelection;
        private System.Windows.Forms.GroupBox grpRegularReturn;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.RadioButton rbnRegularReturn;
        private System.Windows.Forms.RadioButton rbnCorrectionReturn;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpCorrectionReturn;
        private DGVControl.DGVControl dgcViewBatch;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.CheckBox chkBoxNewEntriesOnly;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.Button btnExitExportPanel;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPrcnt;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblFileFolderName;
        private System.Windows.Forms.TextBox txtDestinationFileName;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuGrpInvalidPAN;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuPrintInvalidPAN;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportInvalidPAN;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuGrpNameDifference;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuPrintNameDifference;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportNameDifference;
        private System.Windows.Forms.TextBox txtDestinationFileNameHidden;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Button btnUpdateMasterData;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}