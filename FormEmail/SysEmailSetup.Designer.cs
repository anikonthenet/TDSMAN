namespace TDSMAN.FormEmail
{
    partial class SysEmailSetup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysEmailSetup));
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.chkDisableTLSSSL = new System.Windows.Forms.CheckBox();
            this.chkServerAuthentication = new System.Windows.Forms.CheckBox();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.txtEmailBCC2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEmailBCC = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEmailCC2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEmailCC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmailReplyTo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbSSL = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSMTPHost = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSMTPPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSMTPUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFromDisplayName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFromEmailId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkInactiveSetup = new System.Windows.Forms.CheckBox();
            this.txtEmailDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txtFromEmailIDSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtEmailDescriptionSearch = new System.Windows.Forms.TextBox();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tmrGridRefresh = new System.Windows.Forms.Timer(this.components);
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.dgvGrid = new DGVControl.DGVControl();
            this.pctManual = new System.Windows.Forms.PictureBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 657);
            this.grpSort.Size = new System.Drawing.Size(280, 14);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtFromEmailIDSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Controls.Add(this.txtEmailDescriptionSearch);
            this.grpSearch.Location = new System.Drawing.Point(605, 487);
            this.grpSearch.Size = new System.Drawing.Size(320, 109);
            this.grpSearch.Controls.SetChildIndex(this.txtEmailDescriptionSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtFromEmailIDSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(236, 77);
            this.BtnSearchCancel.TabIndex = 4;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(165, 77);
            this.BtnSearchOK.TabIndex = 3;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(164, 13);
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctManual, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBasicInformation);
            this.pnlControls.Location = new System.Drawing.Point(9, 77);
            this.pnlControls.Size = new System.Drawing.Size(1003, 521);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 80);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 48);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.chkDisableTLSSSL);
            this.grpBasicInformation.Controls.Add(this.chkServerAuthentication);
            this.grpBasicInformation.Controls.Add(this.btnTestEmail);
            this.grpBasicInformation.Controls.Add(this.txtEmailBCC2);
            this.grpBasicInformation.Controls.Add(this.label14);
            this.grpBasicInformation.Controls.Add(this.txtEmailBCC);
            this.grpBasicInformation.Controls.Add(this.label15);
            this.grpBasicInformation.Controls.Add(this.txtEmailCC2);
            this.grpBasicInformation.Controls.Add(this.label13);
            this.grpBasicInformation.Controls.Add(this.txtEmailCC);
            this.grpBasicInformation.Controls.Add(this.label8);
            this.grpBasicInformation.Controls.Add(this.txtEmailReplyTo);
            this.grpBasicInformation.Controls.Add(this.label12);
            this.grpBasicInformation.Controls.Add(this.cmbSSL);
            this.grpBasicInformation.Controls.Add(this.label9);
            this.grpBasicInformation.Controls.Add(this.txtSMTPHost);
            this.grpBasicInformation.Controls.Add(this.label10);
            this.grpBasicInformation.Controls.Add(this.txtSMTPPort);
            this.grpBasicInformation.Controls.Add(this.label11);
            this.grpBasicInformation.Controls.Add(this.txtSMTPPassword);
            this.grpBasicInformation.Controls.Add(this.label5);
            this.grpBasicInformation.Controls.Add(this.txtSMTPUserName);
            this.grpBasicInformation.Controls.Add(this.label6);
            this.grpBasicInformation.Controls.Add(this.txtFromDisplayName);
            this.grpBasicInformation.Controls.Add(this.label3);
            this.grpBasicInformation.Controls.Add(this.txtFromEmailId);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Controls.Add(this.chkInactiveSetup);
            this.grpBasicInformation.Controls.Add(this.txtEmailDescription);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(150, 123);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 218);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // chkDisableTLSSSL
            // 
            this.chkDisableTLSSSL.AutoSize = true;
            this.chkDisableTLSSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDisableTLSSSL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDisableTLSSSL.Location = new System.Drawing.Point(115, 190);
            this.chkDisableTLSSSL.Name = "chkDisableTLSSSL";
            this.chkDisableTLSSSL.Size = new System.Drawing.Size(118, 17);
            this.chkDisableTLSSSL.TabIndex = 213;
            this.chkDisableTLSSSL.TabStop = false;
            this.chkDisableTLSSSL.Text = "Ignore TLS/SSL";
            this.chkDisableTLSSSL.UseVisualStyleBackColor = true;
            this.chkDisableTLSSSL.CheckedChanged += new System.EventHandler(this.chkDisableTLSSSL_CheckedChanged);
            // 
            // chkServerAuthentication
            // 
            this.chkServerAuthentication.AutoSize = true;
            this.chkServerAuthentication.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkServerAuthentication.Location = new System.Drawing.Point(437, 117);
            this.chkServerAuthentication.Name = "chkServerAuthentication";
            this.chkServerAuthentication.Size = new System.Drawing.Size(253, 17);
            this.chkServerAuthentication.TabIndex = 212;
            this.chkServerAuthentication.Text = "Outgoing Server requires Authentication";
            this.chkServerAuthentication.UseVisualStyleBackColor = true;
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTestEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTestEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTestEmail.Location = new System.Drawing.Point(576, 188);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(117, 23);
            this.btnTestEmail.TabIndex = 211;
            this.btnTestEmail.Text = "Send Test Email";
            this.btnTestEmail.UseVisualStyleBackColor = false;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // txtEmailBCC2
            // 
            this.txtEmailBCC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailBCC2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmailBCC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailBCC2.Location = new System.Drawing.Point(459, 163);
            this.txtEmailBCC2.MaxLength = 255;
            this.txtEmailBCC2.Name = "txtEmailBCC2";
            this.txtEmailBCC2.Size = new System.Drawing.Size(234, 21);
            this.txtEmailBCC2.TabIndex = 210;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(377, 167);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 209;
            this.label14.Text = "Email BCC 2";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailBCC
            // 
            this.txtEmailBCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailBCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmailBCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailBCC.Location = new System.Drawing.Point(112, 163);
            this.txtEmailBCC.MaxLength = 255;
            this.txtEmailBCC.Name = "txtEmailBCC";
            this.txtEmailBCC.Size = new System.Drawing.Size(226, 21);
            this.txtEmailBCC.TabIndex = 208;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(42, 168);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 207;
            this.label15.Text = "Email BCC";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailCC2
            // 
            this.txtEmailCC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailCC2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmailCC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailCC2.Location = new System.Drawing.Point(459, 139);
            this.txtEmailCC2.MaxLength = 255;
            this.txtEmailCC2.Name = "txtEmailCC2";
            this.txtEmailCC2.Size = new System.Drawing.Size(234, 21);
            this.txtEmailCC2.TabIndex = 206;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(385, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 205;
            this.label13.Text = "Email CC 2";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailCC
            // 
            this.txtEmailCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmailCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailCC.Location = new System.Drawing.Point(112, 139);
            this.txtEmailCC.MaxLength = 255;
            this.txtEmailCC.Name = "txtEmailCC";
            this.txtEmailCC.Size = new System.Drawing.Size(226, 21);
            this.txtEmailCC.TabIndex = 204;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 203;
            this.label8.Text = "Email CC";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailReplyTo
            // 
            this.txtEmailReplyTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailReplyTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmailReplyTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailReplyTo.Location = new System.Drawing.Point(112, 115);
            this.txtEmailReplyTo.MaxLength = 255;
            this.txtEmailReplyTo.Name = "txtEmailReplyTo";
            this.txtEmailReplyTo.Size = new System.Drawing.Size(226, 21);
            this.txtEmailReplyTo.TabIndex = 202;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 201;
            this.label12.Text = "Email Reply to";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbSSL
            // 
            this.cmbSSL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSSL.FormattingEnabled = true;
            this.cmbSSL.Location = new System.Drawing.Point(243, 91);
            this.cmbSSL.Name = "cmbSSL";
            this.cmbSSL.Size = new System.Drawing.Size(81, 21);
            this.cmbSSL.TabIndex = 198;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(208, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 197;
            this.label9.Text = "SSL";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSMTPHost
            // 
            this.txtSMTPHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSMTPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPHost.Location = new System.Drawing.Point(459, 91);
            this.txtSMTPHost.MaxLength = 255;
            this.txtSMTPHost.Name = "txtSMTPHost";
            this.txtSMTPHost.Size = new System.Drawing.Size(234, 21);
            this.txtSMTPHost.TabIndex = 196;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(382, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 195;
            this.label10.Text = "SMTP Host";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSMTPPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSMTPPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPPort.Location = new System.Drawing.Point(112, 91);
            this.txtSMTPPort.MaxLength = 20;
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(63, 21);
            this.txtSMTPPort.TabIndex = 194;
            this.txtSMTPPort.Text = "0";
            this.txtSMTPPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSMTPPort.Leave += new System.EventHandler(this.NumericControl_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(40, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 193;
            this.label11.Text = "SMTP Port";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSMTPPassword
            // 
            this.txtSMTPPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSMTPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPPassword.Location = new System.Drawing.Point(459, 67);
            this.txtSMTPPassword.MaxLength = 255;
            this.txtSMTPPassword.Name = "txtSMTPPassword";
            this.txtSMTPPassword.Size = new System.Drawing.Size(234, 21);
            this.txtSMTPPassword.TabIndex = 192;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(354, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 191;
            this.label5.Text = "SMTP Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSMTPUserName
            // 
            this.txtSMTPUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSMTPUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPUserName.Location = new System.Drawing.Point(112, 67);
            this.txtSMTPUserName.MaxLength = 255;
            this.txtSMTPUserName.Name = "txtSMTPUserName";
            this.txtSMTPUserName.Size = new System.Drawing.Size(226, 21);
            this.txtSMTPUserName.TabIndex = 190;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 189;
            this.label6.Text = "SMTP Username";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromDisplayName
            // 
            this.txtFromDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDisplayName.Location = new System.Drawing.Point(459, 43);
            this.txtFromDisplayName.MaxLength = 255;
            this.txtFromDisplayName.Name = "txtFromDisplayName";
            this.txtFromDisplayName.Size = new System.Drawing.Size(234, 21);
            this.txtFromDisplayName.TabIndex = 188;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(338, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 187;
            this.label3.Text = "From Display Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromEmailId
            // 
            this.txtFromEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromEmailId.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtFromEmailId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromEmailId.Location = new System.Drawing.Point(112, 43);
            this.txtFromEmailId.MaxLength = 255;
            this.txtFromEmailId.Name = "txtFromEmailId";
            this.txtFromEmailId.Size = new System.Drawing.Size(226, 21);
            this.txtFromEmailId.TabIndex = 186;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(24, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 185;
            this.label2.Text = "From Email Id";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkInactiveSetup
            // 
            this.chkInactiveSetup.AutoSize = true;
            this.chkInactiveSetup.Location = new System.Drawing.Point(605, 19);
            this.chkInactiveSetup.Name = "chkInactiveSetup";
            this.chkInactiveSetup.Size = new System.Drawing.Size(72, 17);
            this.chkInactiveSetup.TabIndex = 184;
            this.chkInactiveSetup.Text = "Inactive";
            this.chkInactiveSetup.UseVisualStyleBackColor = true;
            this.chkInactiveSetup.Visible = false;
            // 
            // txtEmailDescription
            // 
            this.txtEmailDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmailDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailDescription.Location = new System.Drawing.Point(112, 19);
            this.txtEmailDescription.MaxLength = 255;
            this.txtEmailDescription.Name = "txtEmailDescription";
            this.txtEmailDescription.Size = new System.Drawing.Size(348, 21);
            this.txtEmailDescription.TabIndex = 4;
            this.txtEmailDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(36, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(15, 50);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(72, 13);
            this.label114.TabIndex = 159;
            this.label114.Text = "From Email ID";
            // 
            // txtFromEmailIDSearch
            // 
            this.txtFromEmailIDSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromEmailIDSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtFromEmailIDSearch.Location = new System.Drawing.Point(114, 47);
            this.txtFromEmailIDSearch.Name = "txtFromEmailIDSearch";
            this.txtFromEmailIDSearch.Size = new System.Drawing.Size(192, 20);
            this.txtFromEmailIDSearch.TabIndex = 1;
            this.txtFromEmailIDSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(15, 25);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(60, 13);
            this.label117.TabIndex = 157;
            this.label117.Text = "Description";
            // 
            // txtEmailDescriptionSearch
            // 
            this.txtEmailDescriptionSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailDescriptionSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmailDescriptionSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtEmailDescriptionSearch.Location = new System.Drawing.Point(114, 22);
            this.txtEmailDescriptionSearch.MaxLength = 10;
            this.txtEmailDescriptionSearch.Name = "txtEmailDescriptionSearch";
            this.txtEmailDescriptionSearch.Size = new System.Drawing.Size(192, 20);
            this.txtEmailDescriptionSearch.TabIndex = 0;
            this.txtEmailDescriptionSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.BackColor = System.Drawing.Color.White;
            this.cmbCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(184, 48);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(654, 23);
            this.cmbCompanyName.TabIndex = 147;
            this.cmbCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbCompanyName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(87, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 148;
            this.label7.Text = "Company Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tmrGridRefresh
            // 
            this.tmrGridRefresh.Interval = 5000;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(910, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(40, 32);
            this.pctVideoDemo.TabIndex = 208;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvGrid.Location = new System.Drawing.Point(9, 79);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGrid.RowHeadersWidth = 20;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(1003, 519);
            this.dgvGrid.TabIndex = 192;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.CurrentCellChanged += new System.EventHandler(this.dgvGrid_CurrentCellChanged);
            this.dgvGrid.Click += new System.EventHandler(this.dgvGrid_Click);
            this.dgvGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.dgvGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvGrid_KeyDown);
            this.dgvGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvGrid_MouseClick);
            // 
            // pctManual
            // 
            this.pctManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctManual.Image = ((System.Drawing.Image)(resources.GetObject("pctManual.Image")));
            this.pctManual.Location = new System.Drawing.Point(949, 11);
            this.pctManual.Name = "pctManual";
            this.pctManual.Size = new System.Drawing.Size(39, 32);
            this.pctManual.TabIndex = 222;
            this.pctManual.TabStop = false;
            this.pctManual.Tag = "User Manual";
            this.pctManual.Click += new System.EventHandler(this.pctManual_Click);
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(844, 53);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(32, 13);
            this.lnkSearchByTAN.TabIndex = 194;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // SysEmailSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.lnkSearchByTAN);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.cmbCompanyName);
            this.Controls.Add(this.label7);
            this.Name = "SysEmailSetup";
            this.Load += new System.EventHandler(this.SysEmailSetup_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbCompanyName, 0);
            this.Controls.SetChildIndex(this.dgvGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.lnkSearchByTAN, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.TextBox txtEmailDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtFromEmailIDSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtEmailDescriptionSearch;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer tmrGridRefresh;
        private System.Windows.Forms.CheckBox chkInactiveSetup;
        private System.Windows.Forms.TextBox txtFromEmailId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromDisplayName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSMTPPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSMTPUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSMTPHost;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSMTPPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSSL;
        private System.Windows.Forms.TextBox txtEmailCC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmailReplyTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEmailCC2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtEmailBCC2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtEmailBCC;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Button btnTestEmail;
        private System.Windows.Forms.CheckBox chkServerAuthentication;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.PictureBox pctManual;
        private System.Windows.Forms.CheckBox chkDisableTLSSSL;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
    }
}
