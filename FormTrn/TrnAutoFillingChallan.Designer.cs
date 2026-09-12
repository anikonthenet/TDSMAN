namespace TDSMAN.FormTrn
{
    partial class TrnAutoFillingChallan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnAutoFillingChallan));
            this.grpBackUp = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.ctxtNSDLIT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFromNSDLDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFromITDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.grpIncomeTaxLoginPassword = new System.Windows.Forms.GroupBox();
            this.lblSectionCode = new System.Windows.Forms.Label();
            this.tbcLoginOptions = new System.Windows.Forms.TabControl();
            this.tbpPassword = new System.Windows.Forms.TabPage();
            this.rbnIncometaxAct1961 = new System.Windows.Forms.RadioButton();
            this.rbnIncometaxAct2025 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGoITView = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tbpOTP = new System.Windows.Forms.TabPage();
            this.btnGoOTP = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGetOTP = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDisplaySection = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBackUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.ctxtNSDLIT.SuspendLayout();
            this.grpIncomeTaxLoginPassword.SuspendLayout();
            this.tbcLoginOptions.SuspendLayout();
            this.tbpPassword.SuspendLayout();
            this.tbpOTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 636);
            this.grpSort.Size = new System.Drawing.Size(280, 5);
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
            this.BtnSave.Text = "&Proceed";
            this.BtnSave.Click += new System.EventHandler(this.mnuFromITDownload_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 628);
            this.grpSearch.Size = new System.Drawing.Size(280, 5);
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
            this.BtnExit.Text = "&Cancel";
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
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
            this.pnlControls.Controls.Add(this.grpIncomeTaxLoginPassword);
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
            this.grpBackUp.Controls.Add(this.cmbFinancialYear);
            this.grpBackUp.Controls.Add(this.label2);
            this.grpBackUp.Controls.Add(this.label1);
            this.grpBackUp.Controls.Add(this.cmbCompany);
            this.grpBackUp.Location = new System.Drawing.Point(147, 166);
            this.grpBackUp.Name = "grpBackUp";
            this.grpBackUp.Size = new System.Drawing.Size(761, 77);
            this.grpBackUp.TabIndex = 0;
            this.grpBackUp.TabStop = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(665, 47);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 21;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(120, 16);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            this.cmbFinancialYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFinancialYear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(56, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tax Year";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 46);
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
            this.cmbCompany.Location = new System.Drawing.Point(120, 44);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(539, 21);
            this.cmbCompany.TabIndex = 1;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 11;
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
            this.pctUserManual.Location = new System.Drawing.Point(956, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 12;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // ctxtNSDLIT
            // 
            this.ctxtNSDLIT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFromNSDLDownload,
            this.mnuFromITDownload});
            this.ctxtNSDLIT.Name = "ctxtNSDLIT";
            this.ctxtNSDLIT.Size = new System.Drawing.Size(147, 48);
            // 
            // mnuFromNSDLDownload
            // 
            this.mnuFromNSDLDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromNSDLDownload.Name = "mnuFromNSDLDownload";
            this.mnuFromNSDLDownload.Size = new System.Drawing.Size(146, 22);
            this.mnuFromNSDLDownload.Text = "From NSDL";
            this.mnuFromNSDLDownload.Visible = false;
            this.mnuFromNSDLDownload.Click += new System.EventHandler(this.mnuFromNSDLDownload_Click);
            // 
            // mnuFromITDownload
            // 
            this.mnuFromITDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mnuFromITDownload.Name = "mnuFromITDownload";
            this.mnuFromITDownload.Size = new System.Drawing.Size(146, 22);
            this.mnuFromITDownload.Text = "From e-Filing";
            this.mnuFromITDownload.Click += new System.EventHandler(this.mnuFromITDownload_Click);
            // 
            // grpIncomeTaxLoginPassword
            // 
            this.grpIncomeTaxLoginPassword.Controls.Add(this.lblSectionCode);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.tbcLoginOptions);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.panel1);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.lblDisplaySection);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.cmbSection);
            this.grpIncomeTaxLoginPassword.Controls.Add(this.label3);
            this.grpIncomeTaxLoginPassword.Location = new System.Drawing.Point(263, 252);
            this.grpIncomeTaxLoginPassword.Name = "grpIncomeTaxLoginPassword";
            this.grpIncomeTaxLoginPassword.Size = new System.Drawing.Size(476, 248);
            this.grpIncomeTaxLoginPassword.TabIndex = 1;
            this.grpIncomeTaxLoginPassword.TabStop = false;
            this.grpIncomeTaxLoginPassword.Visible = false;
            // 
            // lblSectionCode
            // 
            this.lblSectionCode.AutoSize = true;
            this.lblSectionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectionCode.Location = new System.Drawing.Point(284, 16);
            this.lblSectionCode.Name = "lblSectionCode";
            this.lblSectionCode.Size = new System.Drawing.Size(0, 13);
            this.lblSectionCode.TabIndex = 247;
            this.lblSectionCode.Visible = false;
            // 
            // tbcLoginOptions
            // 
            this.tbcLoginOptions.Controls.Add(this.tbpPassword);
            this.tbcLoginOptions.Controls.Add(this.tbpOTP);
            this.tbcLoginOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbcLoginOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcLoginOptions.Location = new System.Drawing.Point(19, 99);
            this.tbcLoginOptions.Name = "tbcLoginOptions";
            this.tbcLoginOptions.SelectedIndex = 0;
            this.tbcLoginOptions.Size = new System.Drawing.Size(442, 143);
            this.tbcLoginOptions.TabIndex = 246;
            // 
            // tbpPassword
            // 
            this.tbpPassword.Controls.Add(this.rbnIncometaxAct1961);
            this.tbpPassword.Controls.Add(this.rbnIncometaxAct2025);
            this.tbpPassword.Controls.Add(this.label4);
            this.tbpPassword.Controls.Add(this.panel2);
            this.tbpPassword.Controls.Add(this.btnGoITView);
            this.tbpPassword.Controls.Add(this.label6);
            this.tbpPassword.Controls.Add(this.txtPassword);
            this.tbpPassword.Location = new System.Drawing.Point(4, 22);
            this.tbpPassword.Name = "tbpPassword";
            this.tbpPassword.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPassword.Size = new System.Drawing.Size(434, 117);
            this.tbpPassword.TabIndex = 0;
            this.tbpPassword.Text = "Password";
            this.tbpPassword.UseVisualStyleBackColor = true;
            // 
            // rbnIncometaxAct1961
            // 
            this.rbnIncometaxAct1961.AutoSize = true;
            this.rbnIncometaxAct1961.Location = new System.Drawing.Point(227, 84);
            this.rbnIncometaxAct1961.Name = "rbnIncometaxAct1961";
            this.rbnIncometaxAct1961.Size = new System.Drawing.Size(146, 17);
            this.rbnIncometaxAct1961.TabIndex = 252;
            this.rbnIncometaxAct1961.Text = "Income-tax Act, 1961";
            this.rbnIncometaxAct1961.UseVisualStyleBackColor = true;
            this.rbnIncometaxAct1961.CheckedChanged += new System.EventHandler(this.RbnIncometaxAct2025_CheckedChanged);
            // 
            // rbnIncometaxAct2025
            // 
            this.rbnIncometaxAct2025.AutoSize = true;
            this.rbnIncometaxAct2025.Checked = true;
            this.rbnIncometaxAct2025.Location = new System.Drawing.Point(62, 84);
            this.rbnIncometaxAct2025.Name = "rbnIncometaxAct2025";
            this.rbnIncometaxAct2025.Size = new System.Drawing.Size(146, 17);
            this.rbnIncometaxAct2025.TabIndex = 251;
            this.rbnIncometaxAct2025.TabStop = true;
            this.rbnIncometaxAct2025.Text = "Income-tax Act, 2025";
            this.rbnIncometaxAct2025.UseVisualStyleBackColor = true;
            this.rbnIncometaxAct2025.CheckedChanged += new System.EventHandler(this.RbnIncometaxAct2025_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(8, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(420, 20);
            this.label4.TabIndex = 250;
            this.label4.Text = "Select section and enter the password and click <Go>.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(8, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 2);
            this.panel2.TabIndex = 249;
            // 
            // btnGoITView
            // 
            this.btnGoITView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoITView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoITView.Location = new System.Drawing.Point(363, 16);
            this.btnGoITView.Name = "btnGoITView";
            this.btnGoITView.Size = new System.Drawing.Size(36, 22);
            this.btnGoITView.TabIndex = 247;
            this.btnGoITView.Text = "Go";
            this.btnGoITView.UseVisualStyleBackColor = true;
            this.btnGoITView.Click += new System.EventHandler(this.btnGoITView_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 13);
            this.label6.TabIndex = 248;
            this.label6.Text = "Enter password for your e-Filing account";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(247, 17);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(114, 20);
            this.txtPassword.TabIndex = 246;
            this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // tbpOTP
            // 
            this.tbpOTP.Controls.Add(this.btnGoOTP);
            this.tbpOTP.Controls.Add(this.label8);
            this.tbpOTP.Controls.Add(this.txtOTP);
            this.tbpOTP.Controls.Add(this.label5);
            this.tbpOTP.Controls.Add(this.panel3);
            this.tbpOTP.Controls.Add(this.btnGetOTP);
            this.tbpOTP.Controls.Add(this.label7);
            this.tbpOTP.Controls.Add(this.txtMobileNo);
            this.tbpOTP.Location = new System.Drawing.Point(4, 22);
            this.tbpOTP.Name = "tbpOTP";
            this.tbpOTP.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOTP.Size = new System.Drawing.Size(434, 117);
            this.tbpOTP.TabIndex = 1;
            this.tbpOTP.Text = "OTP";
            this.tbpOTP.UseVisualStyleBackColor = true;
            // 
            // btnGoOTP
            // 
            this.btnGoOTP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoOTP.Location = new System.Drawing.Point(305, 47);
            this.btnGoOTP.Name = "btnGoOTP";
            this.btnGoOTP.Size = new System.Drawing.Size(36, 22);
            this.btnGoOTP.TabIndex = 258;
            this.btnGoOTP.Text = "Go";
            this.btnGoOTP.UseVisualStyleBackColor = true;
            this.btnGoOTP.Visible = false;
            this.btnGoOTP.Click += new System.EventHandler(this.btnGoOTP_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 257;
            this.label8.Text = "Enter OTP";
            this.label8.Visible = false;
            // 
            // txtOTP
            // 
            this.txtOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOTP.Location = new System.Drawing.Point(112, 48);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(187, 20);
            this.txtOTP.TabIndex = 256;
            this.txtOTP.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(7, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(420, 25);
            this.label5.TabIndex = 255;
            this.label5.Text = "Select section and enter the mobile no. and click <Go>.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(7, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(420, 2);
            this.panel3.TabIndex = 254;
            // 
            // btnGetOTP
            // 
            this.btnGetOTP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetOTP.Location = new System.Drawing.Point(305, 21);
            this.btnGetOTP.Name = "btnGetOTP";
            this.btnGetOTP.Size = new System.Drawing.Size(68, 22);
            this.btnGetOTP.TabIndex = 252;
            this.btnGetOTP.Text = "Go";
            this.btnGetOTP.UseVisualStyleBackColor = true;
            this.btnGetOTP.Click += new System.EventHandler(this.btnGetOTP_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 253;
            this.label7.Text = "Enter mobile no.";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(112, 22);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(187, 20);
            this.txtMobileNo.TabIndex = 251;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(15, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 1);
            this.panel1.TabIndex = 243;
            // 
            // lblDisplaySection
            // 
            this.lblDisplaySection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplaySection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDisplaySection.Location = new System.Drawing.Point(16, 37);
            this.lblDisplaySection.Name = "lblDisplaySection";
            this.lblDisplaySection.Size = new System.Drawing.Size(445, 51);
            this.lblDisplaySection.TabIndex = 242;
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(109, 12);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(170, 21);
            this.cmbSection.TabIndex = 0;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            this.cmbSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSection_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 240;
            this.label3.Text = "Select section";
            // 
            // TrnAutoFillingChallan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 660);
            this.Name = "TrnAutoFillingChallan";
            this.Load += new System.EventHandler(this.TrnViewReturnStatusOnline_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ctxtNSDLIT.ResumeLayout(false);
            this.grpIncomeTaxLoginPassword.ResumeLayout(false);
            this.grpIncomeTaxLoginPassword.PerformLayout();
            this.tbcLoginOptions.ResumeLayout(false);
            this.tbpPassword.ResumeLayout(false);
            this.tbpPassword.PerformLayout();
            this.tbpOTP.ResumeLayout(false);
            this.tbpOTP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBackUp;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.ContextMenuStrip ctxtNSDLIT;
        private System.Windows.Forms.ToolStripMenuItem mnuFromNSDLDownload;
        private System.Windows.Forms.ToolStripMenuItem mnuFromITDownload;
        private System.Windows.Forms.GroupBox grpIncomeTaxLoginPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label lblDisplaySection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbcLoginOptions;
        private System.Windows.Forms.TabPage tbpPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGoITView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TabPage tbpOTP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGetOTP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Button btnGoOTP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.Label lblSectionCode;
        private System.Windows.Forms.RadioButton rbnIncometaxAct1961;
        private System.Windows.Forms.RadioButton rbnIncometaxAct2025;
    }
}
