namespace TDSMAN.FormTrn
{
    partial class TrnBulkChallanVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnBulkChallanVerification));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.grpRegularReturn = new System.Windows.Forms.GroupBox();
            this.lblAIN = new System.Windows.Forms.Label();
            this.chkValidationType = new System.Windows.Forms.CheckBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.lblCategoryCode = new System.Windows.Forms.Label();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpFromToDate = new System.Windows.Forms.GroupBox();
            this.lblToDate = new System.Windows.Forms.Label();
            this.mskToDate = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.mskFromDate = new System.Windows.Forms.MaskedTextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnPrintMatchedChallan = new System.Windows.Forms.Button();
            this.btnPrintUnmatched = new System.Windows.Forms.Button();
            this.lblUnmatchedNo = new System.Windows.Forms.Label();
            this.lblUnmatched = new System.Windows.Forms.Label();
            this.btnPrintUnmatchedChallan = new System.Windows.Forms.Button();
            this.lblNotVerifiedNos = new System.Windows.Forms.Label();
            this.lblNotVerified = new System.Windows.Forms.Label();
            this.lblUnmatchedChallanNos = new System.Windows.Forms.Label();
            this.lblUnmatchedChallan = new System.Windows.Forms.Label();
            this.lblVerifiedChallanNos = new System.Windows.Forms.Label();
            this.lblVerified = new System.Windows.Forms.Label();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnVerification = new System.Windows.Forms.Button();
            this.dgvChallanDetails = new DGVControl.DGVControl();
            this.bgwChallanVerification = new System.ComponentModel.BackgroundWorker();
            this.grpNSDLLoginDetails = new System.Windows.Forms.GroupBox();
            this.grpCaptcha = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNSDLCaptchaRefresh = new System.Windows.Forms.Button();
            this.picNSDLCaptcha = new System.Windows.Forms.PictureBox();
            this.txtNSDLCaptchaCode = new System.Windows.Forms.TextBox();
            this.btnNSDLClose = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnNSDLLogging = new System.Windows.Forms.Button();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.lblFooterCaption = new System.Windows.Forms.Label();
            this.grpTraces = new System.Windows.Forms.GroupBox();
            this.btnCloseTraces = new System.Windows.Forms.Button();
            this.btnGoTraces = new System.Windows.Forms.Button();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTracesCaptchaRefresh = new System.Windows.Forms.Button();
            this.pctTracesCaptcha = new System.Windows.Forms.PictureBox();
            this.txtTracesCaptcha = new System.Windows.Forms.TextBox();
            this.dgvTracesData = new System.Windows.Forms.DataGridView();
            this.bgwTracesChallanVerification = new System.ComponentModel.BackgroundWorker();
            this.pgTimerChallanVerification = new System.Windows.Forms.Timer(this.components);
            this.pgTimerMatchingRecords = new System.Windows.Forms.Timer(this.components);
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.pnlTitle.SuspendLayout();
            this.grpRegularReturn.SuspendLayout();
            this.grpFromToDate.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallanDetails)).BeginInit();
            this.grpNSDLLoginDetails.SuspendLayout();
            this.grpCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNSDLCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.grpTraces.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTracesCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracesData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(5, 37);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1019, 3);
            this.pnlHeader.TabIndex = 52;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(179, 7);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(670, 24);
            this.pnlTitle.TabIndex = 50;
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
            this.lblTitle.Text = "Online Challan Verification";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(5, 7);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 51;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(849, 7);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(175, 24);
            this.lblSearchMode.TabIndex = 53;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpRegularReturn
            // 
            this.grpRegularReturn.Controls.Add(this.lblAIN);
            this.grpRegularReturn.Controls.Add(this.chkValidationType);
            this.grpRegularReturn.Controls.Add(this.lnkSearchByTAN);
            this.grpRegularReturn.Controls.Add(this.lblCategoryCode);
            this.grpRegularReturn.Controls.Add(this.cmbFormNo);
            this.grpRegularReturn.Controls.Add(this.label4);
            this.grpRegularReturn.Controls.Add(this.cmbQuarter);
            this.grpRegularReturn.Controls.Add(this.label6);
            this.grpRegularReturn.Controls.Add(this.cmbFinancialYear);
            this.grpRegularReturn.Controls.Add(this.label7);
            this.grpRegularReturn.Controls.Add(this.cmbCompany);
            this.grpRegularReturn.Controls.Add(this.label8);
            this.grpRegularReturn.Controls.Add(this.grpFromToDate);
            this.grpRegularReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRegularReturn.Location = new System.Drawing.Point(159, 43);
            this.grpRegularReturn.Name = "grpRegularReturn";
            this.grpRegularReturn.Size = new System.Drawing.Size(710, 99);
            this.grpRegularReturn.TabIndex = 192;
            this.grpRegularReturn.TabStop = false;
            // 
            // lblAIN
            // 
            this.lblAIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAIN.ForeColor = System.Drawing.Color.Black;
            this.lblAIN.Location = new System.Drawing.Point(697, 53);
            this.lblAIN.Name = "lblAIN";
            this.lblAIN.Size = new System.Drawing.Size(6, 16);
            this.lblAIN.TabIndex = 216;
            this.lblAIN.Visible = false;
            // 
            // chkValidationType
            // 
            this.chkValidationType.AutoSize = true;
            this.chkValidationType.Location = new System.Drawing.Point(138, 78);
            this.chkValidationType.Name = "chkValidationType";
            this.chkValidationType.Size = new System.Drawing.Size(154, 19);
            this.chkValidationType.TabIndex = 215;
            this.chkValidationType.Text = "Book Entry Challans";
            this.chkValidationType.UseVisualStyleBackColor = true;
            this.chkValidationType.CheckedChanged += new System.EventHandler(this.chkValidationType_CheckedChanged);
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(606, 54);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 214;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // lblCategoryCode
            // 
            this.lblCategoryCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryCode.ForeColor = System.Drawing.Color.Black;
            this.lblCategoryCode.Location = new System.Drawing.Point(699, 57);
            this.lblCategoryCode.Name = "lblCategoryCode";
            this.lblCategoryCode.Size = new System.Drawing.Size(6, 16);
            this.lblCategoryCode.TabIndex = 211;
            this.lblCategoryCode.Visible = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.ForeColor = System.Drawing.Color.Black;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(402, 18);
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
            this.label4.Location = new System.Drawing.Point(304, 22);
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
            this.cmbQuarter.Location = new System.Drawing.Point(627, 18);
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
            this.label6.Location = new System.Drawing.Point(534, 23);
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
            this.cmbFinancialYear.Location = new System.Drawing.Point(136, 18);
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
            this.label7.Location = new System.Drawing.Point(34, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 207;
            this.label7.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(136, 49);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(464, 23);
            this.cmbCompany.TabIndex = 205;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(34, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 206;
            this.label8.Text = "Select Company";
            // 
            // grpFromToDate
            // 
            this.grpFromToDate.Controls.Add(this.lblToDate);
            this.grpFromToDate.Controls.Add(this.mskToDate);
            this.grpFromToDate.Controls.Add(this.label2);
            this.grpFromToDate.Controls.Add(this.lblFromDate);
            this.grpFromToDate.Controls.Add(this.mskFromDate);
            this.grpFromToDate.Controls.Add(this.label53);
            this.grpFromToDate.Location = new System.Drawing.Point(320, 70);
            this.grpFromToDate.Name = "grpFromToDate";
            this.grpFromToDate.Size = new System.Drawing.Size(385, 43);
            this.grpFromToDate.TabIndex = 217;
            this.grpFromToDate.TabStop = false;
            this.grpFromToDate.Visible = false;
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(201, 17);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(22, 17);
            this.lblToDate.TabIndex = 185;
            this.lblToDate.Text = "To";
            // 
            // mskToDate
            // 
            this.mskToDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskToDate.Location = new System.Drawing.Point(227, 14);
            this.mskToDate.Mask = "00/00/0000";
            this.mskToDate.Name = "mskToDate";
            this.mskToDate.Size = new System.Drawing.Size(77, 20);
            this.mskToDate.TabIndex = 184;
            this.mskToDate.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(306, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 186;
            this.label2.Text = "DD/MM/YYYY";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(4, 17);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(34, 17);
            this.lblFromDate.TabIndex = 182;
            this.lblFromDate.Text = "From";
            // 
            // mskFromDate
            // 
            this.mskFromDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskFromDate.Location = new System.Drawing.Point(40, 15);
            this.mskFromDate.Mask = "00/00/0000";
            this.mskFromDate.Name = "mskFromDate";
            this.mskFromDate.Size = new System.Drawing.Size(76, 20);
            this.mskFromDate.TabIndex = 181;
            this.mskFromDate.ValidatingType = typeof(System.DateTime);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Blue;
            this.label53.Location = new System.Drawing.Point(117, 18);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(77, 14);
            this.label53.TabIndex = 183;
            this.label53.Text = "DD/MM/YYYY";
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.btnPrintMatchedChallan);
            this.grpStatus.Controls.Add(this.btnPrintUnmatched);
            this.grpStatus.Controls.Add(this.lblUnmatchedNo);
            this.grpStatus.Controls.Add(this.lblUnmatched);
            this.grpStatus.Controls.Add(this.btnPrintUnmatchedChallan);
            this.grpStatus.Controls.Add(this.lblNotVerifiedNos);
            this.grpStatus.Controls.Add(this.lblNotVerified);
            this.grpStatus.Controls.Add(this.lblUnmatchedChallanNos);
            this.grpStatus.Controls.Add(this.lblUnmatchedChallan);
            this.grpStatus.Controls.Add(this.lblVerifiedChallanNos);
            this.grpStatus.Controls.Add(this.lblVerified);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.ForeColor = System.Drawing.Color.Black;
            this.grpStatus.Location = new System.Drawing.Point(165, 561);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(505, 41);
            this.grpStatus.TabIndex = 195;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Total Record :";
            // 
            // btnPrintMatchedChallan
            // 
            this.btnPrintMatchedChallan.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintMatchedChallan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintMatchedChallan.Enabled = false;
            this.btnPrintMatchedChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintMatchedChallan.ForeColor = System.Drawing.Color.Black;
            this.btnPrintMatchedChallan.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintMatchedChallan.Image")));
            this.btnPrintMatchedChallan.Location = new System.Drawing.Point(158, 9);
            this.btnPrintMatchedChallan.Name = "btnPrintMatchedChallan";
            this.btnPrintMatchedChallan.Size = new System.Drawing.Size(30, 27);
            this.btnPrintMatchedChallan.TabIndex = 195;
            this.btnPrintMatchedChallan.UseVisualStyleBackColor = false;
            this.btnPrintMatchedChallan.Visible = false;
            // 
            // btnPrintUnmatched
            // 
            this.btnPrintUnmatched.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintUnmatched.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintUnmatched.Enabled = false;
            this.btnPrintUnmatched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintUnmatched.ForeColor = System.Drawing.Color.Black;
            this.btnPrintUnmatched.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintUnmatched.Image")));
            this.btnPrintUnmatched.Location = new System.Drawing.Point(826, 10);
            this.btnPrintUnmatched.Name = "btnPrintUnmatched";
            this.btnPrintUnmatched.Size = new System.Drawing.Size(32, 27);
            this.btnPrintUnmatched.TabIndex = 194;
            this.btnPrintUnmatched.UseVisualStyleBackColor = false;
            this.btnPrintUnmatched.Visible = false;
            // 
            // lblUnmatchedNo
            // 
            this.lblUnmatchedNo.BackColor = System.Drawing.Color.Tan;
            this.lblUnmatchedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnmatchedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatchedNo.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatchedNo.Location = new System.Drawing.Point(781, 13);
            this.lblUnmatchedNo.Name = "lblUnmatchedNo";
            this.lblUnmatchedNo.Size = new System.Drawing.Size(45, 21);
            this.lblUnmatchedNo.TabIndex = 193;
            this.lblUnmatchedNo.Text = "0";
            this.lblUnmatchedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnmatched
            // 
            this.lblUnmatched.AutoSize = true;
            this.lblUnmatched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatched.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatched.Location = new System.Drawing.Point(693, 17);
            this.lblUnmatched.Name = "lblUnmatched";
            this.lblUnmatched.Size = new System.Drawing.Size(85, 13);
            this.lblUnmatched.TabIndex = 192;
            this.lblUnmatched.Text = "Name difference";
            // 
            // btnPrintUnmatchedChallan
            // 
            this.btnPrintUnmatchedChallan.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintUnmatchedChallan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintUnmatchedChallan.Enabled = false;
            this.btnPrintUnmatchedChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintUnmatchedChallan.ForeColor = System.Drawing.Color.Black;
            this.btnPrintUnmatchedChallan.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintUnmatchedChallan.Image")));
            this.btnPrintUnmatchedChallan.Location = new System.Drawing.Point(352, 10);
            this.btnPrintUnmatchedChallan.Name = "btnPrintUnmatchedChallan";
            this.btnPrintUnmatchedChallan.Size = new System.Drawing.Size(30, 27);
            this.btnPrintUnmatchedChallan.TabIndex = 191;
            this.btnPrintUnmatchedChallan.UseVisualStyleBackColor = false;
            this.btnPrintUnmatchedChallan.Visible = false;
            // 
            // lblNotVerifiedNos
            // 
            this.lblNotVerifiedNos.BackColor = System.Drawing.Color.Tan;
            this.lblNotVerifiedNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotVerifiedNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerifiedNos.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerifiedNos.Location = new System.Drawing.Point(447, 12);
            this.lblNotVerifiedNos.Name = "lblNotVerifiedNos";
            this.lblNotVerifiedNos.Size = new System.Drawing.Size(45, 21);
            this.lblNotVerifiedNos.TabIndex = 22;
            this.lblNotVerifiedNos.Text = "0";
            this.lblNotVerifiedNos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNotVerified
            // 
            this.lblNotVerified.AutoSize = true;
            this.lblNotVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerified.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerified.Location = new System.Drawing.Point(382, 16);
            this.lblNotVerified.Name = "lblNotVerified";
            this.lblNotVerified.Size = new System.Drawing.Size(62, 13);
            this.lblNotVerified.TabIndex = 21;
            this.lblNotVerified.Text = "Not Verified";
            // 
            // lblUnmatchedChallanNos
            // 
            this.lblUnmatchedChallanNos.BackColor = System.Drawing.Color.Red;
            this.lblUnmatchedChallanNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnmatchedChallanNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatchedChallanNos.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatchedChallanNos.Location = new System.Drawing.Point(307, 13);
            this.lblUnmatchedChallanNos.Name = "lblUnmatchedChallanNos";
            this.lblUnmatchedChallanNos.Size = new System.Drawing.Size(45, 21);
            this.lblUnmatchedChallanNos.TabIndex = 20;
            this.lblUnmatchedChallanNos.Text = "0";
            this.lblUnmatchedChallanNos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnmatchedChallan
            // 
            this.lblUnmatchedChallan.AutoSize = true;
            this.lblUnmatchedChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnmatchedChallan.ForeColor = System.Drawing.Color.Black;
            this.lblUnmatchedChallan.Location = new System.Drawing.Point(205, 17);
            this.lblUnmatchedChallan.Name = "lblUnmatchedChallan";
            this.lblUnmatchedChallan.Size = new System.Drawing.Size(99, 13);
            this.lblUnmatchedChallan.TabIndex = 19;
            this.lblUnmatchedChallan.Text = "Unmatched challan";
            // 
            // lblVerifiedChallanNos
            // 
            this.lblVerifiedChallanNos.BackColor = System.Drawing.Color.SpringGreen;
            this.lblVerifiedChallanNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifiedChallanNos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifiedChallanNos.ForeColor = System.Drawing.Color.Black;
            this.lblVerifiedChallanNos.Location = new System.Drawing.Point(113, 13);
            this.lblVerifiedChallanNos.Name = "lblVerifiedChallanNos";
            this.lblVerifiedChallanNos.Size = new System.Drawing.Size(45, 21);
            this.lblVerifiedChallanNos.TabIndex = 18;
            this.lblVerifiedChallanNos.Text = "0";
            this.lblVerifiedChallanNos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerified
            // 
            this.lblVerified.AutoSize = true;
            this.lblVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerified.ForeColor = System.Drawing.Color.Black;
            this.lblVerified.Location = new System.Drawing.Point(24, 17);
            this.lblVerified.Name = "lblVerified";
            this.lblVerified.Size = new System.Drawing.Size(87, 13);
            this.lblVerified.TabIndex = 17;
            this.lblVerified.Text = "Amount matched";
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnXit);
            this.grpButtons.Controls.Add(this.btnVerification);
            this.grpButtons.Location = new System.Drawing.Point(673, 561);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(196, 41);
            this.grpButtons.TabIndex = 194;
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
            this.btnXit.Size = new System.Drawing.Size(63, 23);
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
            this.btnVerification.Location = new System.Drawing.Point(5, 11);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(121, 23);
            this.btnVerification.TabIndex = 189;
            this.btnVerification.Text = "&Start Verifying";
            this.btnVerification.UseVisualStyleBackColor = false;
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // dgvChallanDetails
            // 
            this.dgvChallanDetails.AllowUserToAddRows = false;
            this.dgvChallanDetails.AllowUserToDeleteRows = false;
            this.dgvChallanDetails.AllowUserToOrderColumns = true;
            this.dgvChallanDetails.AllowUserToResizeRows = false;
            this.dgvChallanDetails.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvChallanDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChallanDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChallanDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvChallanDetails.GridColor = System.Drawing.SystemColors.Control;
            this.dgvChallanDetails.Location = new System.Drawing.Point(159, 147);
            this.dgvChallanDetails.MultiSelect = false;
            this.dgvChallanDetails.Name = "dgvChallanDetails";
            this.dgvChallanDetails.RowHeadersWidth = 20;
            this.dgvChallanDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvChallanDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChallanDetails.Size = new System.Drawing.Size(710, 411);
            this.dgvChallanDetails.TabIndex = 193;
            this.dgvChallanDetails.TabStop = false;
            // 
            // bgwChallanVerification
            // 
            this.bgwChallanVerification.WorkerSupportsCancellation = true;
            this.bgwChallanVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwChallanVerification_DoWork);
            this.bgwChallanVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwChallanVerification_RunWorkerCompleted);
            // 
            // grpNSDLLoginDetails
            // 
            this.grpNSDLLoginDetails.BackColor = System.Drawing.Color.LightGray;
            this.grpNSDLLoginDetails.Controls.Add(this.grpCaptcha);
            this.grpNSDLLoginDetails.Controls.Add(this.btnNSDLClose);
            this.grpNSDLLoginDetails.Controls.Add(this.pBar);
            this.grpNSDLLoginDetails.Controls.Add(this.btnNSDLLogging);
            this.grpNSDLLoginDetails.Location = new System.Drawing.Point(159, 188);
            this.grpNSDLLoginDetails.Name = "grpNSDLLoginDetails";
            this.grpNSDLLoginDetails.Size = new System.Drawing.Size(711, 211);
            this.grpNSDLLoginDetails.TabIndex = 196;
            this.grpNSDLLoginDetails.TabStop = false;
            this.grpNSDLLoginDetails.Visible = false;
            // 
            // grpCaptcha
            // 
            this.grpCaptcha.Controls.Add(this.label3);
            this.grpCaptcha.Controls.Add(this.btnNSDLCaptchaRefresh);
            this.grpCaptcha.Controls.Add(this.picNSDLCaptcha);
            this.grpCaptcha.Controls.Add(this.txtNSDLCaptchaCode);
            this.grpCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCaptcha.Location = new System.Drawing.Point(7, 27);
            this.grpCaptcha.Name = "grpCaptcha";
            this.grpCaptcha.Size = new System.Drawing.Size(696, 126);
            this.grpCaptcha.TabIndex = 1;
            this.grpCaptcha.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(207, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 204;
            this.label3.Text = "Enter text as per image";
            // 
            // btnNSDLCaptchaRefresh
            // 
            this.btnNSDLCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnNSDLCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNSDLCaptchaRefresh.BackgroundImage")));
            this.btnNSDLCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNSDLCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNSDLCaptchaRefresh.Location = new System.Drawing.Point(423, 18);
            this.btnNSDLCaptchaRefresh.Name = "btnNSDLCaptchaRefresh";
            this.btnNSDLCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnNSDLCaptchaRefresh.TabIndex = 203;
            this.btnNSDLCaptchaRefresh.TabStop = false;
            this.btnNSDLCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnNSDLCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picNSDLCaptcha
            // 
            this.picNSDLCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNSDLCaptcha.Location = new System.Drawing.Point(175, 15);
            this.picNSDLCaptcha.Name = "picNSDLCaptcha";
            this.picNSDLCaptcha.Size = new System.Drawing.Size(239, 70);
            this.picNSDLCaptcha.TabIndex = 202;
            this.picNSDLCaptcha.TabStop = false;
            // 
            // txtNSDLCaptchaCode
            // 
            this.txtNSDLCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNSDLCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNSDLCaptchaCode.Location = new System.Drawing.Point(349, 93);
            this.txtNSDLCaptchaCode.Name = "txtNSDLCaptchaCode";
            this.txtNSDLCaptchaCode.Size = new System.Drawing.Size(140, 20);
            this.txtNSDLCaptchaCode.TabIndex = 0;
            // 
            // btnNSDLClose
            // 
            this.btnNSDLClose.BackColor = System.Drawing.Color.Lavender;
            this.btnNSDLClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNSDLClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNSDLClose.ForeColor = System.Drawing.Color.Black;
            this.btnNSDLClose.Location = new System.Drawing.Point(660, 182);
            this.btnNSDLClose.Name = "btnNSDLClose";
            this.btnNSDLClose.Size = new System.Drawing.Size(47, 25);
            this.btnNSDLClose.TabIndex = 204;
            this.btnNSDLClose.Text = "&Close";
            this.btnNSDLClose.UseVisualStyleBackColor = false;
            this.btnNSDLClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(35, 165);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 202;
            // 
            // btnNSDLLogging
            // 
            this.btnNSDLLogging.BackColor = System.Drawing.Color.Lavender;
            this.btnNSDLLogging.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNSDLLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNSDLLogging.ForeColor = System.Drawing.Color.Black;
            this.btnNSDLLogging.Location = new System.Drawing.Point(613, 182);
            this.btnNSDLLogging.Name = "btnNSDLLogging";
            this.btnNSDLLogging.Size = new System.Drawing.Size(47, 25);
            this.btnNSDLLogging.TabIndex = 201;
            this.btnNSDLLogging.Text = "&Go";
            this.btnNSDLLogging.UseVisualStyleBackColor = false;
            this.btnNSDLLogging.Click += new System.EventHandler(this.btnLogging_Click);
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(793, 605);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 204;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // lblFooterCaption
            // 
            this.lblFooterCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooterCaption.ForeColor = System.Drawing.Color.Blue;
            this.lblFooterCaption.Location = new System.Drawing.Point(166, 606);
            this.lblFooterCaption.Name = "lblFooterCaption";
            this.lblFooterCaption.Size = new System.Drawing.Size(616, 31);
            this.lblFooterCaption.TabIndex = 205;
            this.lblFooterCaption.Text = "label1";
            this.lblFooterCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooterCaption.Visible = false;
            // 
            // grpTraces
            // 
            this.grpTraces.BackColor = System.Drawing.Color.Silver;
            this.grpTraces.Controls.Add(this.btnCloseTraces);
            this.grpTraces.Controls.Add(this.btnGoTraces);
            this.grpTraces.Controls.Add(this.lstDeducteeHelp);
            this.grpTraces.Controls.Add(this.groupBox2);
            this.grpTraces.Controls.Add(this.label10);
            this.grpTraces.Controls.Add(this.btnTracesCaptchaRefresh);
            this.grpTraces.Controls.Add(this.pctTracesCaptcha);
            this.grpTraces.Controls.Add(this.txtTracesCaptcha);
            this.grpTraces.Location = new System.Drawing.Point(146, 190);
            this.grpTraces.Name = "grpTraces";
            this.grpTraces.Size = new System.Drawing.Size(737, 209);
            this.grpTraces.TabIndex = 206;
            this.grpTraces.TabStop = false;
            this.grpTraces.Visible = false;
            // 
            // btnCloseTraces
            // 
            this.btnCloseTraces.BackColor = System.Drawing.Color.Lavender;
            this.btnCloseTraces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseTraces.ForeColor = System.Drawing.Color.Black;
            this.btnCloseTraces.Location = new System.Drawing.Point(684, 178);
            this.btnCloseTraces.Name = "btnCloseTraces";
            this.btnCloseTraces.Size = new System.Drawing.Size(47, 25);
            this.btnCloseTraces.TabIndex = 221;
            this.btnCloseTraces.Text = "&Close";
            this.btnCloseTraces.UseVisualStyleBackColor = false;
            this.btnCloseTraces.Click += new System.EventHandler(this.btnCloseTraces_Click);
            // 
            // btnGoTraces
            // 
            this.btnGoTraces.BackColor = System.Drawing.Color.Lavender;
            this.btnGoTraces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTraces.ForeColor = System.Drawing.Color.Black;
            this.btnGoTraces.Location = new System.Drawing.Point(637, 178);
            this.btnGoTraces.Name = "btnGoTraces";
            this.btnGoTraces.Size = new System.Drawing.Size(47, 25);
            this.btnGoTraces.TabIndex = 220;
            this.btnGoTraces.Text = "&Go";
            this.btnGoTraces.UseVisualStyleBackColor = false;
            this.btnGoTraces.Click += new System.EventHandler(this.btnGoTraces_Click);
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(105, 61);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 20);
            this.lstDeducteeHelp.TabIndex = 219;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTANNo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtUserID);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(710, 67);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enter TRACES Login Details";
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(91, 22);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.ReadOnly = true;
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
            this.label5.Location = new System.Drawing.Point(55, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(267, 22);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(10, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(281, 22);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 208;
            this.label1.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(213, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 206;
            this.label9.Text = "User ID";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(143, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 13);
            this.label10.TabIndex = 200;
            this.label10.Text = "Enter text as in above image";
            // 
            // btnTracesCaptchaRefresh
            // 
            this.btnTracesCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnTracesCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTracesCaptchaRefresh.BackgroundImage")));
            this.btnTracesCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTracesCaptchaRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTracesCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTracesCaptchaRefresh.Location = new System.Drawing.Point(481, 112);
            this.btnTracesCaptchaRefresh.Name = "btnTracesCaptchaRefresh";
            this.btnTracesCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnTracesCaptchaRefresh.TabIndex = 199;
            this.btnTracesCaptchaRefresh.TabStop = false;
            this.btnTracesCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnTracesCaptchaRefresh.Click += new System.EventHandler(this.btnTracesCaptchaRefresh_Click);
            // 
            // pctTracesCaptcha
            // 
            this.pctTracesCaptcha.Location = new System.Drawing.Point(289, 97);
            this.pctTracesCaptcha.Name = "pctTracesCaptcha";
            this.pctTracesCaptcha.Size = new System.Drawing.Size(187, 55);
            this.pctTracesCaptcha.TabIndex = 197;
            this.pctTracesCaptcha.TabStop = false;
            // 
            // txtTracesCaptcha
            // 
            this.txtTracesCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTracesCaptcha.Location = new System.Drawing.Point(317, 166);
            this.txtTracesCaptcha.Name = "txtTracesCaptcha";
            this.txtTracesCaptcha.Size = new System.Drawing.Size(144, 20);
            this.txtTracesCaptcha.TabIndex = 1;
            // 
            // dgvTracesData
            // 
            this.dgvTracesData.AllowUserToAddRows = false;
            this.dgvTracesData.AllowUserToDeleteRows = false;
            this.dgvTracesData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTracesData.Location = new System.Drawing.Point(886, 113);
            this.dgvTracesData.Name = "dgvTracesData";
            this.dgvTracesData.Size = new System.Drawing.Size(115, 70);
            this.dgvTracesData.TabIndex = 223;
            this.dgvTracesData.Visible = false;
            // 
            // bgwTracesChallanVerification
            // 
            this.bgwTracesChallanVerification.WorkerSupportsCancellation = true;
            this.bgwTracesChallanVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTracesChallanVerification_DoWork);
            this.bgwTracesChallanVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTracesChallanVerification_RunWorkerCompleted);
            // 
            // pgTimerChallanVerification
            // 
            this.pgTimerChallanVerification.Tick += new System.EventHandler(this.pgTimerChallanVerification_Tick);
            // 
            // pgTimerMatchingRecords
            // 
            this.pgTimerMatchingRecords.Tick += new System.EventHandler(this.pgTimerMatchingRecords_Tick);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(830, 605);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 224;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.WorkerSupportsCancellation = true;
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnBulkChallanVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.pctUserManual);
            this.Controls.Add(this.grpNSDLLoginDetails);
            this.Controls.Add(this.dgvTracesData);
            this.Controls.Add(this.grpTraces);
            this.Controls.Add(this.lblFooterCaption);
            this.Controls.Add(this.pctVideoDemo);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.dgvChallanDetails);
            this.Controls.Add(this.grpRegularReturn);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TrnBulkChallanVerification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TrnBulkChallanVerification";
            this.Activated += new System.EventHandler(this.TrnBulkChallanVerification_Activated);
            this.Deactivate += new System.EventHandler(this.TrnBulkChallanVerification_Deactivate);
            this.Load += new System.EventHandler(this.TrnBulkChallanVerification_Load);
            this.pnlTitle.ResumeLayout(false);
            this.grpRegularReturn.ResumeLayout(false);
            this.grpRegularReturn.PerformLayout();
            this.grpFromToDate.ResumeLayout(false);
            this.grpFromToDate.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallanDetails)).EndInit();
            this.grpNSDLLoginDetails.ResumeLayout(false);
            this.grpCaptcha.ResumeLayout(false);
            this.grpCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNSDLCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.grpTraces.ResumeLayout(false);
            this.grpTraces.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTracesCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracesData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Label lblSearchMode;
        private System.Windows.Forms.GroupBox grpRegularReturn;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Button btnPrintUnmatched;
        private System.Windows.Forms.Label lblUnmatchedNo;
        private System.Windows.Forms.Label lblUnmatched;
        private System.Windows.Forms.Button btnPrintUnmatchedChallan;
        private System.Windows.Forms.Label lblNotVerifiedNos;
        private System.Windows.Forms.Label lblNotVerified;
        private System.Windows.Forms.Label lblUnmatchedChallanNos;
        private System.Windows.Forms.Label lblUnmatchedChallan;
        private System.Windows.Forms.Label lblVerifiedChallanNos;
        private System.Windows.Forms.Label lblVerified;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnVerification;
        private DGVControl.DGVControl dgvChallanDetails;
        private System.Windows.Forms.Label lblCategoryCode;
        private System.ComponentModel.BackgroundWorker bgwChallanVerification;
        private System.Windows.Forms.GroupBox grpNSDLLoginDetails;
        private System.Windows.Forms.Button btnNSDLClose;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnNSDLLogging;
        private System.Windows.Forms.GroupBox grpCaptcha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNSDLCaptchaRefresh;
        private System.Windows.Forms.PictureBox picNSDLCaptcha;
        private System.Windows.Forms.TextBox txtNSDLCaptchaCode;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.CheckBox chkValidationType;
        private System.Windows.Forms.Label lblAIN;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.GroupBox grpFromToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.MaskedTextBox mskFromDate;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.MaskedTextBox mskToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFooterCaption;
        private System.Windows.Forms.GroupBox grpTraces;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTracesCaptchaRefresh;
        private System.Windows.Forms.PictureBox pctTracesCaptcha;
        private System.Windows.Forms.TextBox txtTracesCaptcha;
        private System.Windows.Forms.Button btnCloseTraces;
        private System.Windows.Forms.Button btnGoTraces;
        private System.Windows.Forms.DataGridView dgvTracesData;
        private System.ComponentModel.BackgroundWorker bgwTracesChallanVerification;
        private System.Windows.Forms.Timer pgTimerChallanVerification;
        private System.Windows.Forms.Timer pgTimerMatchingRecords;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.Button btnPrintMatchedChallan;
    }
}