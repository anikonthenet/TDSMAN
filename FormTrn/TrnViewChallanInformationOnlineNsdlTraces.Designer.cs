namespace TDSMAN.FormTrn
{
    partial class TrnViewChallanInformationOnlineNsdlTraces
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnViewChallanInformationOnlineNsdlTraces));
            this.grpBackUp = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadTo = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadFrom = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.grpNSDLCaptcha = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBottomMessage = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.ctxtMnuStripDownload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFromNSDLDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFromTracesDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.grpIncomeTaxLoginPassword = new System.Windows.Forms.GroupBox();
            this.btnGoITView = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ctxtMnuStripView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFromNSDLView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFromITView = new System.Windows.Forms.ToolStripMenuItem();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBackUp.SuspendLayout();
            this.grpNSDLCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.ctxtMnuStripDownload.SuspendLayout();
            this.grpIncomeTaxLoginPassword.SuspendLayout();
            this.ctxtMnuStripView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 653);
            this.grpSort.Size = new System.Drawing.Size(280, 10);
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
            this.BtnSave.Location = new System.Drawing.Point(376, 13);
            this.BtnSave.Text = "&View";
            this.BtnSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseClick);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Size = new System.Drawing.Size(280, 10);
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
            // BtnSort
            // 
            this.BtnSort.Location = new System.Drawing.Point(979, 16);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(544, 13);
            this.BtnExit.Text = "&Cancel";
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(967, 16);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.Lavender;
            this.BtnRefresh.Location = new System.Drawing.Point(460, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(83, 25);
            this.BtnRefresh.Text = "&Download";
            this.BtnRefresh.Visible = false;
            this.BtnRefresh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnRefresh_MouseClick);
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
            this.pnlControls.Controls.Add(this.grpIncomeTaxLoginPassword);
            this.pnlControls.Controls.Add(this.grpNSDLCaptcha);
            this.pnlControls.Controls.Add(this.grpBackUp);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpBackUp
            // 
            this.grpBackUp.Controls.Add(this.lnkSearchByTAN);
            this.grpBackUp.Controls.Add(this.label4);
            this.grpBackUp.Controls.Add(this.label52);
            this.grpBackUp.Controls.Add(this.label3);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadTo);
            this.grpBackUp.Controls.Add(this.label2);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadFrom);
            this.grpBackUp.Controls.Add(this.label1);
            this.grpBackUp.Controls.Add(this.cmbCompany);
            this.grpBackUp.Location = new System.Drawing.Point(147, 166);
            this.grpBackUp.Name = "grpBackUp";
            this.grpBackUp.Size = new System.Drawing.Size(752, 83);
            this.grpBackUp.TabIndex = 47;
            this.grpBackUp.TabStop = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(652, 24);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 213;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            this.lnkSearchByTAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lnkSearchByTAN_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(244, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 201;
            this.label4.Text = "DD/MM/YYYY";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(599, 51);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(89, 13);
            this.label52.TabIndex = 200;
            this.label52.Text = "DD/MM/YYYY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(402, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 195;
            this.label3.Text = "Challan To Date";
            // 
            // mskViewFileDownloadTo
            // 
            this.mskViewFileDownloadTo.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadTo.Location = new System.Drawing.Point(507, 48);
            this.mskViewFileDownloadTo.Mask = "00/00/0000";
            this.mskViewFileDownloadTo.Name = "mskViewFileDownloadTo";
            this.mskViewFileDownloadTo.Size = new System.Drawing.Size(89, 20);
            this.mskViewFileDownloadTo.TabIndex = 194;
            this.mskViewFileDownloadTo.ValidatingType = typeof(System.DateTime);
            this.mskViewFileDownloadTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskViewFileDownloadTo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 193;
            this.label2.Text = "Challan From Date";
            // 
            // mskViewFileDownloadFrom
            // 
            this.mskViewFileDownloadFrom.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadFrom.Location = new System.Drawing.Point(153, 48);
            this.mskViewFileDownloadFrom.Mask = "00/00/0000";
            this.mskViewFileDownloadFrom.Name = "mskViewFileDownloadFrom";
            this.mskViewFileDownloadFrom.Size = new System.Drawing.Size(88, 20);
            this.mskViewFileDownloadFrom.TabIndex = 192;
            this.mskViewFileDownloadFrom.ValidatingType = typeof(System.DateTime);
            this.mskViewFileDownloadFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskViewFileDownloadFrom_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Company Name";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(109, 21);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(539, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            // 
            // grpNSDLCaptcha
            // 
            this.grpNSDLCaptcha.Controls.Add(this.lblMessage);
            this.grpNSDLCaptcha.Controls.Add(this.label5);
            this.grpNSDLCaptcha.Controls.Add(this.lblBottomMessage);
            this.grpNSDLCaptcha.Controls.Add(this.btnCaptchaRefresh);
            this.grpNSDLCaptcha.Controls.Add(this.txtCaptchaCode);
            this.grpNSDLCaptcha.Controls.Add(this.picCaptcha);
            this.grpNSDLCaptcha.Location = new System.Drawing.Point(146, 262);
            this.grpNSDLCaptcha.Name = "grpNSDLCaptcha";
            this.grpNSDLCaptcha.Size = new System.Drawing.Size(748, 226);
            this.grpNSDLCaptcha.TabIndex = 208;
            this.grpNSDLCaptcha.TabStop = false;
            this.grpNSDLCaptcha.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(162, 112);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(169, 13);
            this.lblMessage.TabIndex = 213;
            this.lblMessage.Text = "Enter text as in above image";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(736, 1);
            this.label5.TabIndex = 212;
            // 
            // lblBottomMessage
            // 
            this.lblBottomMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBottomMessage.ForeColor = System.Drawing.Color.Red;
            this.lblBottomMessage.Location = new System.Drawing.Point(136, 140);
            this.lblBottomMessage.Name = "lblBottomMessage";
            this.lblBottomMessage.Size = new System.Drawing.Size(477, 71);
            this.lblBottomMessage.TabIndex = 211;
            this.lblBottomMessage.UseMnemonic = false;
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(504, 39);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 210;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(336, 109);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(180, 20);
            this.txtCaptchaCode.TabIndex = 208;
            // 
            // picCaptcha
            // 
            this.picCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCaptcha.Location = new System.Drawing.Point(206, 20);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(287, 84);
            this.picCaptcha.TabIndex = 209;
            this.picCaptcha.TabStop = false;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 10;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // ctxtMnuStripDownload
            // 
            this.ctxtMnuStripDownload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFromNSDLDownload,
            this.mnuFromTracesDownload});
            this.ctxtMnuStripDownload.Name = "ctxtMnuStripDownload";
            this.ctxtMnuStripDownload.Size = new System.Drawing.Size(147, 48);
            // 
            // mnuFromNSDLDownload
            // 
            this.mnuFromNSDLDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromNSDLDownload.Name = "mnuFromNSDLDownload";
            this.mnuFromNSDLDownload.Size = new System.Drawing.Size(146, 22);
            this.mnuFromNSDLDownload.Text = "From NSDL";
            this.mnuFromNSDLDownload.Click += new System.EventHandler(this.MnuFromNSDL_Click);
            // 
            // mnuFromTracesDownload
            // 
            this.mnuFromTracesDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromTracesDownload.Name = "mnuFromTracesDownload";
            this.mnuFromTracesDownload.Size = new System.Drawing.Size(146, 22);
            this.mnuFromTracesDownload.Text = "From e-Filing";
            this.mnuFromTracesDownload.Click += new System.EventHandler(this.MnuFromTraces_Click);
            // 
            // grpIncomeTaxLoginPassword
            // 
            this.grpIncomeTaxLoginPassword.Controls.Add(this.btnGoITView);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.label6);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.txtPassword);
            this.grpIncomeTaxLoginPassword.Location = new System.Drawing.Point(311, 271);
            this.grpIncomeTaxLoginPassword.Name = "grpIncomeTaxLoginPassword";
            this.grpIncomeTaxLoginPassword.Size = new System.Drawing.Size(417, 51);
            this.grpIncomeTaxLoginPassword.TabIndex = 209;
            this.grpIncomeTaxLoginPassword.TabStop = false;
            this.grpIncomeTaxLoginPassword.Visible = false;
            // 
            // btnGoITView
            // 
            this.btnGoITView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoITView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoITView.Location = new System.Drawing.Point(370, 18);
            this.btnGoITView.Name = "btnGoITView";
            this.btnGoITView.Size = new System.Drawing.Size(36, 22);
            this.btnGoITView.TabIndex = 239;
            this.btnGoITView.Text = "Go";
            this.btnGoITView.UseVisualStyleBackColor = true;
            this.btnGoITView.Visible = false;
            this.btnGoITView.Click += new System.EventHandler(this.BtnGoITView_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 13);
            this.label6.TabIndex = 238;
            this.label6.Text = "Enter password for your e-Filing account";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(254, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(114, 20);
            this.txtPassword.TabIndex = 237;
            this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // ctxtMnuStripView
            // 
            this.ctxtMnuStripView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFromNSDLView,
            this.mnuFromITView});
            this.ctxtMnuStripView.Name = "ctxtMnuStripDownload";
            this.ctxtMnuStripView.Size = new System.Drawing.Size(147, 48);
            // 
            // mnuFromNSDLView
            // 
            this.mnuFromNSDLView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromNSDLView.Name = "mnuFromNSDLView";
            this.mnuFromNSDLView.Size = new System.Drawing.Size(146, 22);
            this.mnuFromNSDLView.Text = "From NSDL";
            this.mnuFromNSDLView.Click += new System.EventHandler(this.MnuFromNSDLView_Click);
            // 
            // mnuFromITView
            // 
            this.mnuFromITView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromITView.Name = "mnuFromITView";
            this.mnuFromITView.Size = new System.Drawing.Size(146, 22);
            this.mnuFromITView.Text = "From e-Filing";
            this.mnuFromITView.Click += new System.EventHandler(this.MnuFromITView_Click);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(955, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 237;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnViewChallanInformationOnlineNsdlTraces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "TrnViewChallanInformationOnlineNsdlTraces";
            this.Load += new System.EventHandler(this.SysBackup_Load);
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
            this.grpBackUp.ResumeLayout(false);
            this.grpBackUp.PerformLayout();
            this.grpNSDLCaptcha.ResumeLayout(false);
            this.grpNSDLCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.ctxtMnuStripDownload.ResumeLayout(false);
            this.grpIncomeTaxLoginPassword.ResumeLayout(false);
            this.grpIncomeTaxLoginPassword.PerformLayout();
            this.ctxtMnuStripView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBackUp;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadTo;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpNSDLCaptcha;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.Label lblBottomMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStripDownload;
        private System.Windows.Forms.ToolStripMenuItem mnuFromNSDLDownload;
        private System.Windows.Forms.ToolStripMenuItem mnuFromTracesDownload;
        private System.Windows.Forms.GroupBox grpIncomeTaxLoginPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStripView;
        private System.Windows.Forms.ToolStripMenuItem mnuFromNSDLView;
        private System.Windows.Forms.ToolStripMenuItem mnuFromITView;
        private System.Windows.Forms.Button btnGoITView;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
