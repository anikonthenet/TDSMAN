namespace TDSMAN.FormTrn
{
    partial class TrnDownloadFilesTraces
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDownloadFilesTraces));
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.grpCertExtraction = new System.Windows.Forms.GroupBox();
            this.btnCancelExtraction = new System.Windows.Forms.Button();
            this.btnProceedExtraction = new System.Windows.Forms.Button();
            this.lblCertExtractionHeaderText = new System.Windows.Forms.Label();
            this.tbcCertificateExtraction = new System.Windows.Forms.TabControl();
            this.tbpCertExtraction1 = new System.Windows.Forms.TabPage();
            this.lblCertExtractionText3 = new System.Windows.Forms.Label();
            this.lblCertExtractionPath = new System.Windows.Forms.Label();
            this.lblCertExtractionText2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbnGenCertDSC = new System.Windows.Forms.RadioButton();
            this.rbnGenCert = new System.Windows.Forms.RadioButton();
            this.lblCertExtractionText1 = new System.Windows.Forms.Label();
            this.tbpCertExtraction2 = new System.Windows.Forms.TabPage();
            this.lnkCertExtractionText7 = new System.Windows.Forms.LinkLabel();
            this.lblCertExtractionText6 = new System.Windows.Forms.Label();
            this.lblCertExtractionText5 = new System.Windows.Forms.Label();
            this.pgExtractionBar = new System.Windows.Forms.ProgressBar();
            this.lblCertExtractionText4 = new System.Windows.Forms.Label();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDownloadList = new System.Windows.Forms.DataGridView();
            this.txtRequestNo = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.grpTextMessage = new System.Windows.Forms.GroupBox();
            this.lblTextMessage6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTextMessage5 = new System.Windows.Forms.Label();
            this.lblTextMessage4 = new System.Windows.Forms.Label();
            this.lblTextMessage3 = new System.Windows.Forms.Label();
            this.lblTextMessage2 = new System.Windows.Forms.Label();
            this.lblTextMessage1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCaptchaCaption = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerCertificateExtraction = new System.ComponentModel.BackgroundWorker();
            this.pgTimerExtraction = new System.Windows.Forms.Timer(this.components);
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpCertExtraction.SuspendLayout();
            this.tbcCertificateExtraction.SuspendLayout();
            this.tbpCertExtraction1.SuspendLayout();
            this.tbpCertExtraction2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloadList)).BeginInit();
            this.grpLoginDetails.SuspendLayout();
            this.grpTextMessage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 647);
            this.grpSort.Size = new System.Drawing.Size(280, 6);
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
            this.grpSearch.Location = new System.Drawing.Point(95, 639);
            this.grpSearch.Size = new System.Drawing.Size(280, 6);
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
            this.pnlControls.Controls.Add(this.lstDeducteeHelp);
            this.pnlControls.Controls.Add(this.grpLoginDetails);
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
            this.grpDownloadList.Controls.Add(this.grpCertExtraction);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Controls.Add(this.label4);
            this.grpDownloadList.Controls.Add(this.dgvDownloadList);
            this.grpDownloadList.Controls.Add(this.txtRequestNo);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 16);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 434);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // grpCertExtraction
            // 
            this.grpCertExtraction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.grpCertExtraction.Controls.Add(this.btnCancelExtraction);
            this.grpCertExtraction.Controls.Add(this.btnProceedExtraction);
            this.grpCertExtraction.Controls.Add(this.lblCertExtractionHeaderText);
            this.grpCertExtraction.Controls.Add(this.tbcCertificateExtraction);
            this.grpCertExtraction.Location = new System.Drawing.Point(210, 135);
            this.grpCertExtraction.Name = "grpCertExtraction";
            this.grpCertExtraction.Size = new System.Drawing.Size(549, 212);
            this.grpCertExtraction.TabIndex = 196;
            this.grpCertExtraction.TabStop = false;
            this.grpCertExtraction.Visible = false;
            // 
            // btnCancelExtraction
            // 
            this.btnCancelExtraction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancelExtraction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelExtraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelExtraction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCancelExtraction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelExtraction.Location = new System.Drawing.Point(473, 187);
            this.btnCancelExtraction.Name = "btnCancelExtraction";
            this.btnCancelExtraction.Size = new System.Drawing.Size(70, 23);
            this.btnCancelExtraction.TabIndex = 15;
            this.btnCancelExtraction.Text = "&Cancel";
            this.btnCancelExtraction.UseVisualStyleBackColor = false;
            this.btnCancelExtraction.Click += new System.EventHandler(this.BtnCancelExtraction_Click);
            // 
            // btnProceedExtraction
            // 
            this.btnProceedExtraction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnProceedExtraction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProceedExtraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProceedExtraction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnProceedExtraction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProceedExtraction.Location = new System.Drawing.Point(402, 187);
            this.btnProceedExtraction.Name = "btnProceedExtraction";
            this.btnProceedExtraction.Size = new System.Drawing.Size(70, 23);
            this.btnProceedExtraction.TabIndex = 14;
            this.btnProceedExtraction.Text = "Proceed";
            this.btnProceedExtraction.UseVisualStyleBackColor = false;
            this.btnProceedExtraction.Click += new System.EventHandler(this.BtnProceedExtraction_Click);
            // 
            // lblCertExtractionHeaderText
            // 
            this.lblCertExtractionHeaderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCertExtractionHeaderText.ForeColor = System.Drawing.Color.Black;
            this.lblCertExtractionHeaderText.Location = new System.Drawing.Point(6, 10);
            this.lblCertExtractionHeaderText.Name = "lblCertExtractionHeaderText";
            this.lblCertExtractionHeaderText.Size = new System.Drawing.Size(537, 24);
            this.lblCertExtractionHeaderText.TabIndex = 1;
            this.lblCertExtractionHeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbcCertificateExtraction
            // 
            this.tbcCertificateExtraction.Controls.Add(this.tbpCertExtraction1);
            this.tbcCertificateExtraction.Controls.Add(this.tbpCertExtraction2);
            this.tbcCertificateExtraction.Location = new System.Drawing.Point(6, 13);
            this.tbcCertificateExtraction.Name = "tbcCertificateExtraction";
            this.tbcCertificateExtraction.SelectedIndex = 0;
            this.tbcCertificateExtraction.Size = new System.Drawing.Size(539, 172);
            this.tbcCertificateExtraction.TabIndex = 5;
            // 
            // tbpCertExtraction1
            // 
            this.tbpCertExtraction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.tbpCertExtraction1.Controls.Add(this.lblCertExtractionText3);
            this.tbpCertExtraction1.Controls.Add(this.lblCertExtractionPath);
            this.tbpCertExtraction1.Controls.Add(this.lblCertExtractionText2);
            this.tbpCertExtraction1.Controls.Add(this.panel2);
            this.tbpCertExtraction1.Controls.Add(this.rbnGenCertDSC);
            this.tbpCertExtraction1.Controls.Add(this.rbnGenCert);
            this.tbpCertExtraction1.Controls.Add(this.lblCertExtractionText1);
            this.tbpCertExtraction1.Location = new System.Drawing.Point(4, 22);
            this.tbpCertExtraction1.Name = "tbpCertExtraction1";
            this.tbpCertExtraction1.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCertExtraction1.Size = new System.Drawing.Size(531, 146);
            this.tbpCertExtraction1.TabIndex = 0;
            // 
            // lblCertExtractionText3
            // 
            this.lblCertExtractionText3.ForeColor = System.Drawing.Color.Black;
            this.lblCertExtractionText3.Location = new System.Drawing.Point(3, 112);
            this.lblCertExtractionText3.Name = "lblCertExtractionText3";
            this.lblCertExtractionText3.Size = new System.Drawing.Size(556, 28);
            this.lblCertExtractionText3.TabIndex = 11;
            this.lblCertExtractionText3.Text = "Make sure the DSC is inserted and accessible to the TRACES PDF Convertor";
            this.lblCertExtractionText3.Visible = false;
            // 
            // lblCertExtractionPath
            // 
            this.lblCertExtractionPath.ForeColor = System.Drawing.Color.Blue;
            this.lblCertExtractionPath.Location = new System.Drawing.Point(188, 90);
            this.lblCertExtractionPath.Name = "lblCertExtractionPath";
            this.lblCertExtractionPath.Size = new System.Drawing.Size(333, 15);
            this.lblCertExtractionPath.TabIndex = 10;
            // 
            // lblCertExtractionText2
            // 
            this.lblCertExtractionText2.ForeColor = System.Drawing.Color.Blue;
            this.lblCertExtractionText2.Location = new System.Drawing.Point(3, 90);
            this.lblCertExtractionText2.Name = "lblCertExtractionText2";
            this.lblCertExtractionText2.Size = new System.Drawing.Size(184, 15);
            this.lblCertExtractionText2.TabIndex = 9;
            this.lblCertExtractionText2.Text = "The PDFs will be stored in the folder :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(6, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(518, 1);
            this.panel2.TabIndex = 8;
            // 
            // rbnGenCertDSC
            // 
            this.rbnGenCertDSC.AutoSize = true;
            this.rbnGenCertDSC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnGenCertDSC.Location = new System.Drawing.Point(10, 60);
            this.rbnGenCertDSC.Name = "rbnGenCertDSC";
            this.rbnGenCertDSC.Size = new System.Drawing.Size(267, 17);
            this.rbnGenCertDSC.TabIndex = 7;
            this.rbnGenCertDSC.Text = "Generate digitally signed Certificates (DSC required)";
            this.rbnGenCertDSC.UseVisualStyleBackColor = true;
            this.rbnGenCertDSC.CheckedChanged += new System.EventHandler(this.RbnGenCert_CheckedChanged);
            // 
            // rbnGenCert
            // 
            this.rbnGenCert.AutoSize = true;
            this.rbnGenCert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnGenCert.Location = new System.Drawing.Point(10, 37);
            this.rbnGenCert.Name = "rbnGenCert";
            this.rbnGenCert.Size = new System.Drawing.Size(124, 17);
            this.rbnGenCert.TabIndex = 6;
            this.rbnGenCert.Text = "Generate Certificates";
            this.rbnGenCert.UseVisualStyleBackColor = true;
            this.rbnGenCert.CheckedChanged += new System.EventHandler(this.RbnGenCert_CheckedChanged);
            // 
            // lblCertExtractionText1
            // 
            this.lblCertExtractionText1.ForeColor = System.Drawing.Color.Blue;
            this.lblCertExtractionText1.Location = new System.Drawing.Point(4, 4);
            this.lblCertExtractionText1.Name = "lblCertExtractionText1";
            this.lblCertExtractionText1.Size = new System.Drawing.Size(523, 32);
            this.lblCertExtractionText1.TabIndex = 5;
            this.lblCertExtractionText1.Text = "You may now generate the Certificates through an automated system using the TRACE" +
    "S - PDF Convertor internally.";
            // 
            // tbpCertExtraction2
            // 
            this.tbpCertExtraction2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.tbpCertExtraction2.Controls.Add(this.lnkCertExtractionText7);
            this.tbpCertExtraction2.Controls.Add(this.lblCertExtractionText6);
            this.tbpCertExtraction2.Controls.Add(this.lblCertExtractionText5);
            this.tbpCertExtraction2.Controls.Add(this.pgExtractionBar);
            this.tbpCertExtraction2.Controls.Add(this.lblCertExtractionText4);
            this.tbpCertExtraction2.Location = new System.Drawing.Point(4, 22);
            this.tbpCertExtraction2.Name = "tbpCertExtraction2";
            this.tbpCertExtraction2.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCertExtraction2.Size = new System.Drawing.Size(531, 146);
            this.tbpCertExtraction2.TabIndex = 1;
            // 
            // lnkCertExtractionText7
            // 
            this.lnkCertExtractionText7.Location = new System.Drawing.Point(69, 108);
            this.lnkCertExtractionText7.Name = "lnkCertExtractionText7";
            this.lnkCertExtractionText7.Size = new System.Drawing.Size(456, 33);
            this.lnkCertExtractionText7.TabIndex = 16;
            this.lnkCertExtractionText7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkCertExtractionText7_LinkClicked);
            // 
            // lblCertExtractionText6
            // 
            this.lblCertExtractionText6.ForeColor = System.Drawing.Color.Black;
            this.lblCertExtractionText6.Location = new System.Drawing.Point(6, 107);
            this.lblCertExtractionText6.Name = "lblCertExtractionText6";
            this.lblCertExtractionText6.Size = new System.Drawing.Size(61, 15);
            this.lblCertExtractionText6.TabIndex = 15;
            this.lblCertExtractionText6.Text = "Folder:";
            this.lblCertExtractionText6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCertExtractionText5
            // 
            this.lblCertExtractionText5.ForeColor = System.Drawing.Color.Blue;
            this.lblCertExtractionText5.Location = new System.Drawing.Point(7, 82);
            this.lblCertExtractionText5.Name = "lblCertExtractionText5";
            this.lblCertExtractionText5.Size = new System.Drawing.Size(425, 20);
            this.lblCertExtractionText5.TabIndex = 14;
            this.lblCertExtractionText5.Text = "XXX (Digitally Signed) Certificates generated";
            this.lblCertExtractionText5.Visible = false;
            // 
            // pgExtractionBar
            // 
            this.pgExtractionBar.Location = new System.Drawing.Point(6, 57);
            this.pgExtractionBar.Name = "pgExtractionBar";
            this.pgExtractionBar.Size = new System.Drawing.Size(519, 15);
            this.pgExtractionBar.TabIndex = 13;
            // 
            // lblCertExtractionText4
            // 
            this.lblCertExtractionText4.ForeColor = System.Drawing.Color.Blue;
            this.lblCertExtractionText4.Location = new System.Drawing.Point(3, 25);
            this.lblCertExtractionText4.Name = "lblCertExtractionText4";
            this.lblCertExtractionText4.Size = new System.Drawing.Size(425, 20);
            this.lblCertExtractionText4.TabIndex = 12;
            this.lblCertExtractionText4.Text = "The process will take appx. XXX minutes";
            this.lblCertExtractionText4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(227, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 194;
            this.label4.Text = "Search By Request No.";
            // 
            // dgvDownloadList
            // 
            this.dgvDownloadList.AllowUserToAddRows = false;
            this.dgvDownloadList.AllowUserToDeleteRows = false;
            this.dgvDownloadList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownloadList.Location = new System.Drawing.Point(14, 52);
            this.dgvDownloadList.Name = "dgvDownloadList";
            this.dgvDownloadList.ReadOnly = true;
            this.dgvDownloadList.Size = new System.Drawing.Size(921, 369);
            this.dgvDownloadList.TabIndex = 13;
            this.dgvDownloadList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDownloadList_CellClick);
            // 
            // txtRequestNo
            // 
            this.txtRequestNo.Location = new System.Drawing.Point(369, 20);
            this.txtRequestNo.Name = "txtRequestNo";
            this.txtRequestNo.Size = new System.Drawing.Size(179, 20);
            this.txtRequestNo.TabIndex = 11;
            this.txtRequestNo.TextChanged += new System.EventHandler(this.txtRequestNo_TextChanged);
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
            this.grpLoginDetails.Controls.Add(this.grpTextMessage);
            this.grpLoginDetails.Controls.Add(this.groupBox1);
            this.grpLoginDetails.Controls.Add(this.lblCaptchaCaption);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Controls.Add(this.grpProgress);
            this.grpLoginDetails.Location = new System.Drawing.Point(84, 86);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 293);
            this.grpLoginDetails.TabIndex = 0;
            this.grpLoginDetails.TabStop = false;
            // 
            // grpTextMessage
            // 
            this.grpTextMessage.Controls.Add(this.lblTextMessage6);
            this.grpTextMessage.Controls.Add(this.panel1);
            this.grpTextMessage.Controls.Add(this.lblTextMessage5);
            this.grpTextMessage.Controls.Add(this.lblTextMessage4);
            this.grpTextMessage.Controls.Add(this.lblTextMessage3);
            this.grpTextMessage.Controls.Add(this.lblTextMessage2);
            this.grpTextMessage.Controls.Add(this.lblTextMessage1);
            this.grpTextMessage.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextMessage.Location = new System.Drawing.Point(12, 93);
            this.grpTextMessage.Name = "grpTextMessage";
            this.grpTextMessage.Size = new System.Drawing.Size(804, 35);
            this.grpTextMessage.TabIndex = 219;
            this.grpTextMessage.TabStop = false;
            this.grpTextMessage.Text = "If download fails please do the following steps :";
            this.grpTextMessage.Visible = false;
            // 
            // lblTextMessage6
            // 
            this.lblTextMessage6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTextMessage6.Location = new System.Drawing.Point(19, 138);
            this.lblTextMessage6.Name = "lblTextMessage6";
            this.lblTextMessage6.Size = new System.Drawing.Size(766, 18);
            this.lblTextMessage6.TabIndex = 207;
            this.lblTextMessage6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(194, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 1);
            this.panel1.TabIndex = 206;
            // 
            // lblTextMessage5
            // 
            this.lblTextMessage5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage5.ForeColor = System.Drawing.Color.Blue;
            this.lblTextMessage5.Location = new System.Drawing.Point(21, 109);
            this.lblTextMessage5.Name = "lblTextMessage5";
            this.lblTextMessage5.Size = new System.Drawing.Size(766, 18);
            this.lblTextMessage5.TabIndex = 205;
            this.lblTextMessage5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextMessage4
            // 
            this.lblTextMessage4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage4.ForeColor = System.Drawing.Color.Blue;
            this.lblTextMessage4.Location = new System.Drawing.Point(21, 90);
            this.lblTextMessage4.Name = "lblTextMessage4";
            this.lblTextMessage4.Size = new System.Drawing.Size(766, 18);
            this.lblTextMessage4.TabIndex = 204;
            this.lblTextMessage4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextMessage3
            // 
            this.lblTextMessage3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage3.ForeColor = System.Drawing.Color.Blue;
            this.lblTextMessage3.Location = new System.Drawing.Point(21, 71);
            this.lblTextMessage3.Name = "lblTextMessage3";
            this.lblTextMessage3.Size = new System.Drawing.Size(766, 18);
            this.lblTextMessage3.TabIndex = 203;
            this.lblTextMessage3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextMessage2
            // 
            this.lblTextMessage2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage2.ForeColor = System.Drawing.Color.Blue;
            this.lblTextMessage2.Location = new System.Drawing.Point(21, 52);
            this.lblTextMessage2.Name = "lblTextMessage2";
            this.lblTextMessage2.Size = new System.Drawing.Size(766, 18);
            this.lblTextMessage2.TabIndex = 202;
            this.lblTextMessage2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextMessage1
            // 
            this.lblTextMessage1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage1.ForeColor = System.Drawing.Color.Blue;
            this.lblTextMessage1.Location = new System.Drawing.Point(21, 18);
            this.lblTextMessage1.Name = "lblTextMessage1";
            this.lblTextMessage1.Size = new System.Drawing.Size(766, 33);
            this.lblTextMessage1.TabIndex = 201;
            this.lblTextMessage1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtUserID.Size = new System.Drawing.Size(11, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(268, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(203, 21);
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
            // lblCaptchaCaption
            // 
            this.lblCaptchaCaption.AutoSize = true;
            this.lblCaptchaCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptchaCaption.Location = new System.Drawing.Point(154, 192);
            this.lblCaptchaCaption.Name = "lblCaptchaCaption";
            this.lblCaptchaCaption.Size = new System.Drawing.Size(169, 13);
            this.lblCaptchaCaption.TabIndex = 200;
            this.lblCaptchaCaption.Text = "Enter text as in above image";
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
            this.picCaptcha.InitialImage = ((System.Drawing.Image)(resources.GetObject("picCaptcha.InitialImage")));
            this.picCaptcha.Location = new System.Drawing.Point(287, 119);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(234, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(343, 188);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(163, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(81, 225);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(670, 49);
            this.grpProgress.TabIndex = 213;
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
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(220, 145);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 84);
            this.lstDeducteeHelp.TabIndex = 216;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(917, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 206;
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
            this.pctUserManual.Location = new System.Drawing.Point(955, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 207;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // bgWorkerCertificateExtraction
            // 
            this.bgWorkerCertificateExtraction.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerCertificateExtraction_DoWork);
            this.bgWorkerCertificateExtraction.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorkerCertificateExtraction_ProgressChanged);
            this.bgWorkerCertificateExtraction.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerCertificateExtraction_RunWorkerCompleted);
            // 
            // pgTimerExtraction
            // 
            this.pgTimerExtraction.Interval = 2000;
            this.pgTimerExtraction.Tick += new System.EventHandler(this.PgTimerExtraction_Tick);
            // 
            // TrnDownloadFilesTraces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 622);
            this.Name = "TrnDownloadFilesTraces";
            this.Activated += new System.EventHandler(this.TrnDownloadFilesTraces_Activated);
            this.Load += new System.EventHandler(this.TrnViewReturnStatusOnline_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpCertExtraction.ResumeLayout(false);
            this.tbcCertificateExtraction.ResumeLayout(false);
            this.tbpCertExtraction1.ResumeLayout(false);
            this.tbpCertExtraction1.PerformLayout();
            this.tbpCertExtraction2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloadList)).EndInit();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.grpTextMessage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.DataGridView dgvDownloadList;
        private System.Windows.Forms.TextBox txtRequestNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.Label lblCaptchaCaption;
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
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.GroupBox grpTextMessage;
        private System.Windows.Forms.Label lblTextMessage5;
        private System.Windows.Forms.Label lblTextMessage4;
        private System.Windows.Forms.Label lblTextMessage3;
        private System.Windows.Forms.Label lblTextMessage2;
        private System.Windows.Forms.Label lblTextMessage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTextMessage6;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.ComponentModel.BackgroundWorker bgWorkerCertificateExtraction;
        private System.Windows.Forms.GroupBox grpCertExtraction;
        private System.Windows.Forms.Label lblCertExtractionHeaderText;
        private System.Windows.Forms.TabControl tbcCertificateExtraction;
        private System.Windows.Forms.TabPage tbpCertExtraction1;
        private System.Windows.Forms.TabPage tbpCertExtraction2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbnGenCertDSC;
        private System.Windows.Forms.RadioButton rbnGenCert;
        private System.Windows.Forms.Label lblCertExtractionText1;
        private System.Windows.Forms.Label lblCertExtractionText2;
        private System.Windows.Forms.Label lblCertExtractionPath;
        private System.Windows.Forms.Label lblCertExtractionText3;
        public System.Windows.Forms.Button btnCancelExtraction;
        public System.Windows.Forms.Button btnProceedExtraction;
        private System.Windows.Forms.Label lblCertExtractionText4;
        private System.Windows.Forms.ProgressBar pgExtractionBar;
        private System.Windows.Forms.Label lblCertExtractionText5;
        private System.Windows.Forms.Label lblCertExtractionText6;
        private System.Windows.Forms.Timer pgTimerExtraction;
        private System.Windows.Forms.LinkLabel lnkCertExtractionText7;
    }
}
