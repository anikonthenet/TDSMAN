namespace TDSMAN.FormTrn
{
    partial class TrnBulkPANVerificationTraces_New_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnBulkPANVerificationTraces_New));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dgvDeductees = new DGVControl.DGVControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnVerification = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bgwPANVerification = new System.ComponentModel.BackgroundWorker();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnPrintInoperativePAN = new System.Windows.Forms.Button();
            this.lblInOperativeNo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrintInvalidPAN = new System.Windows.Forms.Button();
            this.lblNotVerifiedNo = new System.Windows.Forms.Label();
            this.lblNotVerified = new System.Windows.Forms.Label();
            this.lblInvalidNo = new System.Windows.Forms.Label();
            this.lblInvalid = new System.Windows.Forms.Label();
            this.lblVerifiedNo = new System.Windows.Forms.Label();
            this.lblVerified = new System.Windows.Forms.Label();
            this.grpDisclaimer = new System.Windows.Forms.GroupBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.grpCaptcha = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.grpEnterLoginDetails = new System.Windows.Forms.GroupBox();
            this.txtTAN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogging = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpLoginDetails = new System.Windows.Forms.GroupBox();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.chkIgnore = new System.Windows.Forms.CheckBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.pnlTracesMessage = new System.Windows.Forms.Panel();
            this.lblTracesMessage = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpDisclaimer.SuspendLayout();
            this.grpCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.grpEnterLoginDetails.SuspendLayout();
            this.grpLoginDetails.SuspendLayout();
            this.pnlTracesMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(553, 8);
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
            this.pnlTitle.Size = new System.Drawing.Size(382, 24);
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
            this.lblTitle.Size = new System.Drawing.Size(371, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Bulk PAN Verification";
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
            this.pnlHeader.Size = new System.Drawing.Size(721, 3);
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
            this.dgvDeductees.Location = new System.Drawing.Point(12, 44);
            this.dgvDeductees.MultiSelect = false;
            this.dgvDeductees.Name = "dgvDeductees";
            this.dgvDeductees.RowHeadersWidth = 20;
            this.dgvDeductees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDeductees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeductees.Size = new System.Drawing.Size(710, 463);
            this.dgvDeductees.TabIndex = 185;
            this.dgvDeductees.TabStop = false;
            this.dgvDeductees.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDeductees_ColumnAdded);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnXit);
            this.groupBox3.Controls.Add(this.btnVerification);
            this.groupBox3.Location = new System.Drawing.Point(517, 514);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 39);
            this.groupBox3.TabIndex = 187;
            this.groupBox3.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(124, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "&Close";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnVerification
            // 
            this.btnVerification.BackColor = System.Drawing.Color.Lavender;
            this.btnVerification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerification.ForeColor = System.Drawing.Color.Black;
            this.btnVerification.Location = new System.Drawing.Point(2, 11);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(121, 23);
            this.btnVerification.TabIndex = 189;
            this.btnVerification.Tag = "";
            this.btnVerification.Text = "&Start verifying";
            this.btnVerification.UseVisualStyleBackColor = false;
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(13, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 1);
            this.panel1.TabIndex = 188;
            // 
            // bgwPANVerification
            // 
            this.bgwPANVerification.WorkerSupportsCancellation = true;
            this.bgwPANVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPANVerification_DoWork);
            this.bgwPANVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwPANVerification_RunWorkerCompleted);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.btnPrintInoperativePAN);
            this.grpStatus.Controls.Add(this.lblInOperativeNo);
            this.grpStatus.Controls.Add(this.label4);
            this.grpStatus.Controls.Add(this.btnPrintInvalidPAN);
            this.grpStatus.Controls.Add(this.lblNotVerifiedNo);
            this.grpStatus.Controls.Add(this.lblNotVerified);
            this.grpStatus.Controls.Add(this.lblInvalidNo);
            this.grpStatus.Controls.Add(this.lblInvalid);
            this.grpStatus.Controls.Add(this.lblVerifiedNo);
            this.grpStatus.Controls.Add(this.lblVerified);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.ForeColor = System.Drawing.Color.Black;
            this.grpStatus.Location = new System.Drawing.Point(15, 514);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(497, 39);
            this.grpStatus.TabIndex = 189;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Total Records";
            // 
            // btnPrintInoperativePAN
            // 
            this.btnPrintInoperativePAN.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintInoperativePAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintInoperativePAN.Enabled = false;
            this.btnPrintInoperativePAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintInoperativePAN.ForeColor = System.Drawing.Color.Black;
            this.btnPrintInoperativePAN.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintInoperativePAN.Image")));
            this.btnPrintInoperativePAN.Location = new System.Drawing.Point(219, 9);
            this.btnPrintInoperativePAN.Name = "btnPrintInoperativePAN";
            this.btnPrintInoperativePAN.Size = new System.Drawing.Size(32, 27);
            this.btnPrintInoperativePAN.TabIndex = 194;
            this.btnPrintInoperativePAN.UseVisualStyleBackColor = false;
            this.btnPrintInoperativePAN.Click += new System.EventHandler(this.btnPrintInoperativePAN_Click);
            this.btnPrintInoperativePAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPrintInoperativePAN_MouseMove);
            // 
            // lblInOperativeNo
            // 
            this.lblInOperativeNo.BackColor = System.Drawing.Color.Yellow;
            this.lblInOperativeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInOperativeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOperativeNo.ForeColor = System.Drawing.Color.Black;
            this.lblInOperativeNo.Location = new System.Drawing.Point(173, 13);
            this.lblInOperativeNo.Name = "lblInOperativeNo";
            this.lblInOperativeNo.Size = new System.Drawing.Size(45, 21);
            this.lblInOperativeNo.TabIndex = 193;
            this.lblInOperativeNo.Text = "0";
            this.lblInOperativeNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(110, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 192;
            this.label4.Text = "Inoperative";
            // 
            // btnPrintInvalidPAN
            // 
            this.btnPrintInvalidPAN.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintInvalidPAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintInvalidPAN.Enabled = false;
            this.btnPrintInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintInvalidPAN.ForeColor = System.Drawing.Color.Black;
            this.btnPrintInvalidPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintInvalidPAN.Image")));
            this.btnPrintInvalidPAN.Location = new System.Drawing.Point(349, 9);
            this.btnPrintInvalidPAN.Name = "btnPrintInvalidPAN";
            this.btnPrintInvalidPAN.Size = new System.Drawing.Size(32, 27);
            this.btnPrintInvalidPAN.TabIndex = 191;
            this.btnPrintInvalidPAN.UseVisualStyleBackColor = false;
            this.btnPrintInvalidPAN.Click += new System.EventHandler(this.btnPrintInvalidPAN_Click);
            this.btnPrintInvalidPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPrintInvalidPAN_MouseMove);
            // 
            // lblNotVerifiedNo
            // 
            this.lblNotVerifiedNo.BackColor = System.Drawing.Color.Silver;
            this.lblNotVerifiedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotVerifiedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerifiedNo.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerifiedNo.Location = new System.Drawing.Point(448, 13);
            this.lblNotVerifiedNo.Name = "lblNotVerifiedNo";
            this.lblNotVerifiedNo.Size = new System.Drawing.Size(43, 21);
            this.lblNotVerifiedNo.TabIndex = 22;
            this.lblNotVerifiedNo.Text = "0";
            this.lblNotVerifiedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNotVerified
            // 
            this.lblNotVerified.AutoSize = true;
            this.lblNotVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerified.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerified.Location = new System.Drawing.Point(382, 17);
            this.lblNotVerified.Name = "lblNotVerified";
            this.lblNotVerified.Size = new System.Drawing.Size(62, 13);
            this.lblNotVerified.TabIndex = 21;
            this.lblNotVerified.Text = "Not Verified";
            // 
            // lblInvalidNo
            // 
            this.lblInvalidNo.BackColor = System.Drawing.Color.Red;
            this.lblInvalidNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvalidNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidNo.ForeColor = System.Drawing.Color.Black;
            this.lblInvalidNo.Location = new System.Drawing.Point(298, 13);
            this.lblInvalidNo.Name = "lblInvalidNo";
            this.lblInvalidNo.Size = new System.Drawing.Size(50, 21);
            this.lblInvalidNo.TabIndex = 20;
            this.lblInvalidNo.Text = "0";
            this.lblInvalidNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInvalidNo.TextChanged += new System.EventHandler(this.lblInvalidNo_TextChanged);
            // 
            // lblInvalid
            // 
            this.lblInvalid.AutoSize = true;
            this.lblInvalid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalid.ForeColor = System.Drawing.Color.Black;
            this.lblInvalid.Location = new System.Drawing.Point(257, 18);
            this.lblInvalid.Name = "lblInvalid";
            this.lblInvalid.Size = new System.Drawing.Size(38, 13);
            this.lblInvalid.TabIndex = 19;
            this.lblInvalid.Text = "Invalid";
            // 
            // lblVerifiedNo
            // 
            this.lblVerifiedNo.BackColor = System.Drawing.Color.Green;
            this.lblVerifiedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifiedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifiedNo.ForeColor = System.Drawing.Color.Black;
            this.lblVerifiedNo.Location = new System.Drawing.Point(38, 13);
            this.lblVerifiedNo.Name = "lblVerifiedNo";
            this.lblVerifiedNo.Size = new System.Drawing.Size(69, 21);
            this.lblVerifiedNo.TabIndex = 18;
            this.lblVerifiedNo.Text = "0";
            this.lblVerifiedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerified
            // 
            this.lblVerified.AutoSize = true;
            this.lblVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerified.ForeColor = System.Drawing.Color.Black;
            this.lblVerified.Location = new System.Drawing.Point(4, 17);
            this.lblVerified.Name = "lblVerified";
            this.lblVerified.Size = new System.Drawing.Size(30, 13);
            this.lblVerified.TabIndex = 17;
            this.lblVerified.Text = "Valid";
            // 
            // grpDisclaimer
            // 
            this.grpDisclaimer.Controls.Add(this.lblDisclaimer);
            this.grpDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.grpDisclaimer.Location = new System.Drawing.Point(15, 553);
            this.grpDisclaimer.Name = "grpDisclaimer";
            this.grpDisclaimer.Size = new System.Drawing.Size(706, 34);
            this.grpDisclaimer.TabIndex = 190;
            this.grpDisclaimer.TabStop = false;
            this.grpDisclaimer.Text = "Disclaimer";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.lblDisclaimer.Location = new System.Drawing.Point(6, 11);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(692, 18);
            this.lblDisclaimer.TabIndex = 18;
            this.lblDisclaimer.Text = "Disclaimer";
            this.lblDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label3.Location = new System.Drawing.Point(207, 76);
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
            this.picCaptcha.Location = new System.Drawing.Point(145, 13);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(277, 56);
            this.picCaptcha.TabIndex = 202;
            this.picCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(349, 72);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(140, 20);
            this.txtCaptchaCode.TabIndex = 0;
            // 
            // grpEnterLoginDetails
            // 
            this.grpEnterLoginDetails.Controls.Add(this.txtTAN);
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
            // txtTAN
            // 
            this.txtTAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTAN.Location = new System.Drawing.Point(97, 15);
            this.txtTAN.MaxLength = 10;
            this.txtTAN.Name = "txtTAN";
            this.txtTAN.Size = new System.Drawing.Size(116, 20);
            this.txtTAN.TabIndex = 0;
            this.txtTAN.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtTAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTAN.Leave += new System.EventHandler(this.txtTAN_Leave);
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
            this.txtUserID.Size = new System.Drawing.Size(10, 20);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Visible = false;
            this.txtUserID.Enter += new System.EventHandler(this.txtUserId_Enter);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(295, 15);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Enter += new System.EventHandler(this.txtUserId_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(230, 18);
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
            this.label1.Visible = false;
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
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(35, 165);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 202;
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
            // grpLoginDetails
            // 
            this.grpLoginDetails.BackColor = System.Drawing.Color.Silver;
            this.grpLoginDetails.Controls.Add(this.lstDeducteeHelp);
            this.grpLoginDetails.Controls.Add(this.btnClose);
            this.grpLoginDetails.Controls.Add(this.pBar);
            this.grpLoginDetails.Controls.Add(this.btnLogging);
            this.grpLoginDetails.Controls.Add(this.grpEnterLoginDetails);
            this.grpLoginDetails.Controls.Add(this.grpCaptcha);
            this.grpLoginDetails.Location = new System.Drawing.Point(12, 190);
            this.grpLoginDetails.Name = "grpLoginDetails";
            this.grpLoginDetails.Size = new System.Drawing.Size(711, 211);
            this.grpLoginDetails.TabIndex = 191;
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
            this.lstDeducteeHelp.Size = new System.Drawing.Size(552, 20);
            this.lstDeducteeHelp.TabIndex = 207;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // chkIgnore
            // 
            this.chkIgnore.AutoSize = true;
            this.chkIgnore.Checked = true;
            this.chkIgnore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkIgnore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIgnore.Location = new System.Drawing.Point(42, 44);
            this.chkIgnore.Name = "chkIgnore";
            this.chkIgnore.Size = new System.Drawing.Size(240, 17);
            this.chkIgnore.TabIndex = 192;
            this.chkIgnore.Text = "Ignore the pool & Verify from TRACES";
            this.chkIgnore.UseMnemonic = false;
            this.chkIgnore.UseVisualStyleBackColor = true;
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.WorkerSupportsCancellation = true;
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // pnlTracesMessage
            // 
            this.pnlTracesMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTracesMessage.Controls.Add(this.lblTracesMessage);
            this.pnlTracesMessage.Location = new System.Drawing.Point(114, 271);
            this.pnlTracesMessage.Name = "pnlTracesMessage";
            this.pnlTracesMessage.Size = new System.Drawing.Size(506, 49);
            this.pnlTracesMessage.TabIndex = 193;
            this.pnlTracesMessage.Visible = false;
            // 
            // lblTracesMessage
            // 
            this.lblTracesMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTracesMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblTracesMessage.Location = new System.Drawing.Point(3, 4);
            this.lblTracesMessage.Name = "lblTracesMessage";
            this.lblTracesMessage.Size = new System.Drawing.Size(498, 40);
            this.lblTracesMessage.TabIndex = 0;
            this.lblTracesMessage.Text = "<< Verifying PAN from TRACES >> ";
            this.lblTracesMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrnBulkPANVerificationTraces_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(735, 591);
            this.Controls.Add(this.pnlTracesMessage);
            this.Controls.Add(this.grpLoginDetails);
            this.Controls.Add(this.grpDisclaimer);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvDeductees);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.chkIgnore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnBulkPANVerificationTraces_New";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.TrnBulkPANVerificationTraces_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrnBulkPANVerificationTraces_New_FormClosing);
            this.Load += new System.EventHandler(this.TrnBulkPANVerification_Load);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpDisclaimer.ResumeLayout(false);
            this.grpCaptcha.ResumeLayout(false);
            this.grpCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.grpEnterLoginDetails.ResumeLayout(false);
            this.grpEnterLoginDetails.PerformLayout();
            this.grpLoginDetails.ResumeLayout(false);
            this.pnlTracesMessage.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnVerification;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker bgwPANVerification;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblVerified;
        private System.Windows.Forms.Label lblVerifiedNo;
        private System.Windows.Forms.Label lblInvalid;
        private System.Windows.Forms.Label lblInvalidNo;
        private System.Windows.Forms.Label lblNotVerifiedNo;
        private System.Windows.Forms.Label lblNotVerified;
        private System.Windows.Forms.GroupBox grpDisclaimer;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Button btnPrintInvalidPAN;
        private System.Windows.Forms.GroupBox grpCaptcha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.GroupBox grpEnterLoginDetails;
        private System.Windows.Forms.TextBox txtTAN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogging;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpLoginDetails;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInOperativeNo;
        private System.Windows.Forms.CheckBox chkIgnore;
        private System.Windows.Forms.Button btnPrintInoperativePAN;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.Panel pnlTracesMessage;
        private System.Windows.Forms.Label lblTracesMessage;
    }
}