namespace TDSMAN.FormWeb
{
    partial class TrnViewFiledTDS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnViewFiledTDS));
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.grpOTPDetails = new System.Windows.Forms.GroupBox();
            this.txtTANPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dgvStatementDetails = new System.Windows.Forms.DataGridView();
            this.dgvUploadDetails = new System.Windows.Forms.DataGridView();
            this.grpStatementDetails = new System.Windows.Forms.GroupBox();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUploadType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblFileRequest = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lnkNewRegister = new System.Windows.Forms.LinkLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTAN = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pgTimerGrid2 = new System.Windows.Forms.Timer(this.components);
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpControls.SuspendLayout();
            this.grpOTPDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUploadDetails)).BeginInit();
            this.grpStatementDetails.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpLoginDetails.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
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
            this.BtnSave.Text = "&Next";
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
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctUserManual, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpLoginDetails);
            this.pnlControls.Controls.Add(this.grpControls);
            this.pnlControls.Controls.Add(this.groupBox3);
            this.pnlControls.Size = new System.Drawing.Size(1036, 609);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.grpOTPDetails);
            this.grpControls.Controls.Add(this.grpStatementDetails);
            this.grpControls.Location = new System.Drawing.Point(31, 11);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(949, 450);
            this.grpControls.TabIndex = 48;
            this.grpControls.TabStop = false;
            // 
            // grpOTPDetails
            // 
            this.grpOTPDetails.Controls.Add(this.txtTANPassword);
            this.grpOTPDetails.Controls.Add(this.label18);
            this.grpOTPDetails.Controls.Add(this.dgvStatementDetails);
            this.grpOTPDetails.Controls.Add(this.dgvUploadDetails);
            this.grpOTPDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOTPDetails.Location = new System.Drawing.Point(10, 95);
            this.grpOTPDetails.Name = "grpOTPDetails";
            this.grpOTPDetails.Size = new System.Drawing.Size(927, 342);
            this.grpOTPDetails.TabIndex = 4;
            this.grpOTPDetails.TabStop = false;
            // 
            // txtTANPassword
            // 
            this.txtTANPassword.BackColor = System.Drawing.SystemColors.Info;
            this.txtTANPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTANPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTANPassword.Location = new System.Drawing.Point(790, 316);
            this.txtTANPassword.MaxLength = 10;
            this.txtTANPassword.Name = "txtTANPassword";
            this.txtTANPassword.ReadOnly = true;
            this.txtTANPassword.Size = new System.Drawing.Size(96, 20);
            this.txtTANPassword.TabIndex = 230;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.OliveDrab;
            this.label18.Location = new System.Drawing.Point(37, 318);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(748, 17);
            this.label18.TabIndex = 229;
            this.label18.Text = "Provisional receipt is password protected. To open the PDF, please enter your TAN" +
    " in lowercase. Your password is :";
            // 
            // dgvStatementDetails
            // 
            this.dgvStatementDetails.AllowUserToAddRows = false;
            this.dgvStatementDetails.AllowUserToDeleteRows = false;
            this.dgvStatementDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementDetails.Location = new System.Drawing.Point(11, 219);
            this.dgvStatementDetails.Name = "dgvStatementDetails";
            this.dgvStatementDetails.Size = new System.Drawing.Size(905, 93);
            this.dgvStatementDetails.TabIndex = 16;
            this.dgvStatementDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatementDetails_CellClick);
            // 
            // dgvUploadDetails
            // 
            this.dgvUploadDetails.AllowUserToAddRows = false;
            this.dgvUploadDetails.AllowUserToDeleteRows = false;
            this.dgvUploadDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUploadDetails.Location = new System.Drawing.Point(11, 22);
            this.dgvUploadDetails.Name = "dgvUploadDetails";
            this.dgvUploadDetails.Size = new System.Drawing.Size(905, 194);
            this.dgvUploadDetails.TabIndex = 15;
            this.dgvUploadDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUploadDetails_CellClick);
            // 
            // grpStatementDetails
            // 
            this.grpStatementDetails.Controls.Add(this.btnView);
            this.grpStatementDetails.Controls.Add(this.txtTANNo);
            this.grpStatementDetails.Controls.Add(this.label2);
            this.grpStatementDetails.Controls.Add(this.cmbUploadType);
            this.grpStatementDetails.Controls.Add(this.label7);
            this.grpStatementDetails.Controls.Add(this.cmbQtr);
            this.grpStatementDetails.Controls.Add(this.cmbFormNo);
            this.grpStatementDetails.Controls.Add(this.cmbFAYear);
            this.grpStatementDetails.Controls.Add(this.label3);
            this.grpStatementDetails.Controls.Add(this.label5);
            this.grpStatementDetails.Controls.Add(this.label6);
            this.grpStatementDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatementDetails.Location = new System.Drawing.Point(8, 30);
            this.grpStatementDetails.Name = "grpStatementDetails";
            this.grpStatementDetails.Size = new System.Drawing.Size(931, 63);
            this.grpStatementDetails.TabIndex = 2;
            this.grpStatementDetails.TabStop = false;
            this.grpStatementDetails.Text = "Search Options";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(857, 18);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(50, 23);
            this.btnView.TabIndex = 228;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTANNo
            // 
            this.txtTANNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtTANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(69, 19);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.ReadOnly = true;
            this.txtTANNo.Size = new System.Drawing.Size(104, 20);
            this.txtTANNo.TabIndex = 226;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 227;
            this.label2.Text = "TAN No";
            // 
            // cmbUploadType
            // 
            this.cmbUploadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUploadType.FormattingEnabled = true;
            this.cmbUploadType.Location = new System.Drawing.Point(753, 19);
            this.cmbUploadType.Name = "cmbUploadType";
            this.cmbUploadType.Size = new System.Drawing.Size(101, 21);
            this.cmbUploadType.TabIndex = 217;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(669, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 216;
            this.label7.Text = "Upload Type";
            // 
            // cmbQtr
            // 
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(571, 18);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(92, 21);
            this.cmbQtr.TabIndex = 213;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(407, 18);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(103, 21);
            this.cmbFormNo.TabIndex = 212;
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(239, 18);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(106, 21);
            this.cmbFAYear.TabIndex = 210;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 215;
            this.label3.Text = "Quarter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Form No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 211;
            this.label6.Text = "FA Year";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblFileRequest);
            this.groupBox3.Controls.Add(this.pBar);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(104, 469);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(670, 60);
            this.groupBox3.TabIndex = 219;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            this.groupBox3.Visible = false;
            // 
            // lblFileRequest
            // 
            this.lblFileRequest.AutoSize = true;
            this.lblFileRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileRequest.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFileRequest.Location = new System.Drawing.Point(14, 20);
            this.lblFileRequest.Name = "lblFileRequest";
            this.lblFileRequest.Size = new System.Drawing.Size(223, 13);
            this.lblFileRequest.TabIndex = 9;
            this.lblFileRequest.Text = "File has been Requested Successfully";
            this.lblFileRequest.Visible = false;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 33);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 23);
            this.pBar.TabIndex = 8;
            // 
            // grpLoginDetails
            // 
            this.grpLoginDetails.Controls.Add(this.label16);
            this.grpLoginDetails.Controls.Add(this.lnkNewRegister);
            this.grpLoginDetails.Controls.Add(this.label20);
            this.grpLoginDetails.Controls.Add(this.panel3);
            this.grpLoginDetails.Controls.Add(this.label19);
            this.grpLoginDetails.Controls.Add(this.panel2);
            this.grpLoginDetails.Controls.Add(this.label1);
            this.grpLoginDetails.Controls.Add(this.lstTAN);
            this.grpLoginDetails.Controls.Add(this.groupBox5);
            this.grpLoginDetails.Controls.Add(this.label14);
            this.grpLoginDetails.Controls.Add(this.btnCaptchaRefresh);
            this.grpLoginDetails.Controls.Add(this.picCaptcha);
            this.grpLoginDetails.Controls.Add(this.txtCaptchaCode);
            this.grpLoginDetails.Location = new System.Drawing.Point(91, 62);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(829, 263);
            this.grpLoginDetails.TabIndex = 220;
            this.grpLoginDetails.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(214, 151);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 237;
            this.label16.Text = "Captcha Code";
            this.label16.Visible = false;
            // 
            // lnkNewRegister
            // 
            this.lnkNewRegister.AutoSize = true;
            this.lnkNewRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNewRegister.Location = new System.Drawing.Point(617, 58);
            this.lnkNewRegister.Name = "lnkNewRegister";
            this.lnkNewRegister.Size = new System.Drawing.Size(153, 13);
            this.lnkNewRegister.TabIndex = 236;
            this.lnkNewRegister.TabStop = true;
            this.lnkNewRegister.Text = "New users register here...";
            this.lnkNewRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNewRegister_LinkClicked);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkBlue;
            this.label20.Location = new System.Drawing.Point(8, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(813, 16);
            this.label20.TabIndex = 235;
            this.label20.Text = "Enter login details (TAN) as used in www.incometaxindiaefiling.gov.in";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(254, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(316, 1);
            this.panel3.TabIndex = 234;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkBlue;
            this.label19.Location = new System.Drawing.Point(362, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(104, 15);
            this.label19.TabIndex = 233;
            this.label19.Text = "View Filed TDS";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(21, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(791, 1);
            this.panel2.TabIndex = 231;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(2, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 15);
            this.label1.TabIndex = 230;
            this.label1.Text = "Kindly ensure that you must have the correct Email ID and Mobile Number for all C" +
    "ommunications from Income Tax Department.";
            // 
            // lstTAN
            // 
            this.lstTAN.BackColor = System.Drawing.Color.MistyRose;
            this.lstTAN.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTAN.FormattingEnabled = true;
            this.lstTAN.ItemHeight = 16;
            this.lstTAN.Location = new System.Drawing.Point(245, 117);
            this.lstTAN.Name = "lstTAN";
            this.lstTAN.Size = new System.Drawing.Size(442, 20);
            this.lstTAN.TabIndex = 219;
            this.lstTAN.Visible = false;
            this.lstTAN.Click += new System.EventHandler(this.lstTAN_Click);
            this.lstTAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTAN_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtUserID);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtPassword);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(59, 76);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(710, 67);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Enter Login Details";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // txtUserID
            // 
            this.txtUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserID.Location = new System.Drawing.Point(186, 18);
            this.txtUserID.MaxLength = 10;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(171, 20);
            this.txtUserID.TabIndex = 0;
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyDown);
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserID_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(97, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 209;
            this.label10.Text = "User Id (TAN)";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(437, 18);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(191, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(372, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 208;
            this.label11.Text = "Password";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(188, 212);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(169, 13);
            this.label14.TabIndex = 200;
            this.label14.Text = "Enter text as in above image";
            this.label14.Visible = false;
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(526, 162);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 199;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Visible = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Location = new System.Drawing.Point(305, 148);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(219, 55);
            this.picCaptcha.TabIndex = 197;
            this.picCaptcha.TabStop = false;
            this.picCaptcha.Visible = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(362, 208);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(144, 20);
            this.txtCaptchaCode.TabIndex = 1;
            this.txtCaptchaCode.Visible = false;
            // 
            // pgTimerGrid2
            // 
            this.pgTimerGrid2.Tick += new System.EventHandler(this.pgTimerGrid2_Tick);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(916, 9);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(82, 32);
            this.pctUserManual.TabIndex = 209;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Video Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnViewFiledTDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 575);
            this.Name = "TrnViewFiledTDS";
            this.Activated += new System.EventHandler(this.TrnViewFiledTDS_Activated);
            this.Load += new System.EventHandler(this.TrnViewFiledTDS_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpControls.ResumeLayout(false);
            this.grpOTPDetails.ResumeLayout(false);
            this.grpOTPDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUploadDetails)).EndInit();
            this.grpStatementDetails.ResumeLayout(false);
            this.grpStatementDetails.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpLoginDetails.ResumeLayout(false);
            this.grpLoginDetails.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpControls;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpStatementDetails;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbUploadType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblFileRequest;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.ListBox lstTAN;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.GroupBox grpOTPDetails;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridView dgvUploadDetails;
        private System.Windows.Forms.DataGridView dgvStatementDetails;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.Timer pgTimerGrid2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTANPassword;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.LinkLabel lnkNewRegister;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
