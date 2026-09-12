namespace TDSMAN.FormMst
{
    partial class MstDeductee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstDeductee));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.chkHideDeductee = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDeducteeAddress = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTaxIdentificationNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnVerifyPAN = new System.Windows.Forms.Button();
            this.lnkPANVerification = new System.Windows.Forms.LinkLabel();
            this.txtOldPAN = new System.Windows.Forms.TextBox();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress5 = new System.Windows.Forms.TextBox();
            this.txtAddress4 = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.lblDeducteeCode = new System.Windows.Forms.Label();
            this.cmbDeducteeCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPAN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeducteeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txtDeducteeNameSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtPANSearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDeducteeCodeSearch = new System.Windows.Forms.ComboBox();
            this.tmrGridRefresh = new System.Windows.Forms.Timer(this.components);
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.dgvGrid = new DGVControl.DGVControl();
            this.ctxtmnuExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlStrpMnuCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpMnuExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.ctxtmnuExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 657);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(375, 14);
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(292, 14);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.cmbDeducteeCodeSearch);
            this.grpSearch.Controls.Add(this.label9);
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtDeducteeNameSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Controls.Add(this.txtPANSearch);
            this.grpSearch.Location = new System.Drawing.Point(614, 446);
            this.grpSearch.Size = new System.Drawing.Size(319, 147);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtPANSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtDeducteeNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.label9, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbDeducteeCodeSearch, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(229, 109);
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(158, 109);
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(209, 14);
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(126, 14);
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(711, 14);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(794, 14);
            this.BtnPrint.Size = new System.Drawing.Size(83, 25);
            this.BtnPrint.Text = "Export";
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            this.BtnPrint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnPrint_MouseClick);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(624, 14);
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(541, 14);
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(458, 14);
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
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
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBasicInformation);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 42);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 8);
            this.ViewGrid.Visible = false;
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.chkHideDeductee);
            this.grpBasicInformation.Controls.Add(this.panel2);
            this.grpBasicInformation.Controls.Add(this.panel1);
            this.grpBasicInformation.Controls.Add(this.label14);
            this.grpBasicInformation.Controls.Add(this.panel7);
            this.grpBasicInformation.Controls.Add(this.panel5);
            this.grpBasicInformation.Controls.Add(this.panel3);
            this.grpBasicInformation.Controls.Add(this.txtDeducteeAddress);
            this.grpBasicInformation.Controls.Add(this.label13);
            this.grpBasicInformation.Controls.Add(this.txtTaxIdentificationNumber);
            this.grpBasicInformation.Controls.Add(this.label12);
            this.grpBasicInformation.Controls.Add(this.txtEmail);
            this.grpBasicInformation.Controls.Add(this.label8);
            this.grpBasicInformation.Controls.Add(this.txtMobile);
            this.grpBasicInformation.Controls.Add(this.label7);
            this.grpBasicInformation.Controls.Add(this.txtRefNo);
            this.grpBasicInformation.Controls.Add(this.label10);
            this.grpBasicInformation.Controls.Add(this.btnVerifyPAN);
            this.grpBasicInformation.Controls.Add(this.lnkPANVerification);
            this.grpBasicInformation.Controls.Add(this.txtOldPAN);
            this.grpBasicInformation.Controls.Add(this.txtPIN);
            this.grpBasicInformation.Controls.Add(this.label6);
            this.grpBasicInformation.Controls.Add(this.cmbState);
            this.grpBasicInformation.Controls.Add(this.label1);
            this.grpBasicInformation.Controls.Add(this.txtAddress5);
            this.grpBasicInformation.Controls.Add(this.txtAddress4);
            this.grpBasicInformation.Controls.Add(this.txtAddress3);
            this.grpBasicInformation.Controls.Add(this.txtAddress2);
            this.grpBasicInformation.Controls.Add(this.lblDeducteeCode);
            this.grpBasicInformation.Controls.Add(this.cmbDeducteeCode);
            this.grpBasicInformation.Controls.Add(this.label5);
            this.grpBasicInformation.Controls.Add(this.txtAddress1);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Controls.Add(this.txtPAN);
            this.grpBasicInformation.Controls.Add(this.label3);
            this.grpBasicInformation.Controls.Add(this.txtDeducteeName);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(54, 19);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(895, 408);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // chkHideDeductee
            // 
            this.chkHideDeductee.AutoSize = true;
            this.chkHideDeductee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkHideDeductee.Location = new System.Drawing.Point(523, 372);
            this.chkHideDeductee.Name = "chkHideDeductee";
            this.chkHideDeductee.Size = new System.Drawing.Size(172, 17);
            this.chkHideDeductee.TabIndex = 183;
            this.chkHideDeductee.Text = "Hide Deductee from Entry";
            this.chkHideDeductee.UseVisualStyleBackColor = true;
            this.chkHideDeductee.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Blue;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.ForeColor = System.Drawing.Color.Blue;
            this.panel2.Location = new System.Drawing.Point(542, 357);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(40, 2);
            this.panel2.TabIndex = 182;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Blue;
            this.panel4.ForeColor = System.Drawing.Color.Blue;
            this.panel4.Location = new System.Drawing.Point(0, -91);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(40, 2);
            this.panel4.TabIndex = 165;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.ForeColor = System.Drawing.Color.Blue;
            this.panel1.Location = new System.Drawing.Point(471, 269);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 2);
            this.panel1.TabIndex = 181;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Blue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(584, 285);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 55);
            this.label14.TabIndex = 180;
            this.label14.Text = "These fields are for Form 144(27Q) exclusively";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Blue;
            this.panel7.Controls.Add(this.panel8);
            this.panel7.ForeColor = System.Drawing.Color.Blue;
            this.panel7.Location = new System.Drawing.Point(542, 318);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(40, 2);
            this.panel7.TabIndex = 179;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Blue;
            this.panel8.ForeColor = System.Drawing.Color.Blue;
            this.panel8.Location = new System.Drawing.Point(0, -91);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(40, 2);
            this.panel8.TabIndex = 165;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.ForeColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(542, 294);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(40, 2);
            this.panel5.TabIndex = 178;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Blue;
            this.panel6.ForeColor = System.Drawing.Color.Blue;
            this.panel6.Location = new System.Drawing.Point(0, -91);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(40, 2);
            this.panel6.TabIndex = 165;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Blue;
            this.panel3.ForeColor = System.Drawing.Color.Blue;
            this.panel3.Location = new System.Drawing.Point(582, 269);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 90);
            this.panel3.TabIndex = 177;
            // 
            // txtDeducteeAddress
            // 
            this.txtDeducteeAddress.BackColor = System.Drawing.Color.White;
            this.txtDeducteeAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeAddress.Location = new System.Drawing.Point(222, 341);
            this.txtDeducteeAddress.MaxLength = 150;
            this.txtDeducteeAddress.Name = "txtDeducteeAddress";
            this.txtDeducteeAddress.Size = new System.Drawing.Size(318, 21);
            this.txtDeducteeAddress.TabIndex = 15;
            this.txtDeducteeAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(163, 345);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 176;
            this.label13.Text = "Address";
            // 
            // txtTaxIdentificationNumber
            // 
            this.txtTaxIdentificationNumber.BackColor = System.Drawing.Color.White;
            this.txtTaxIdentificationNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxIdentificationNumber.Location = new System.Drawing.Point(222, 316);
            this.txtTaxIdentificationNumber.MaxLength = 25;
            this.txtTaxIdentificationNumber.Name = "txtTaxIdentificationNumber";
            this.txtTaxIdentificationNumber.Size = new System.Drawing.Size(318, 21);
            this.txtTaxIdentificationNumber.TabIndex = 14;
            this.txtTaxIdentificationNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(85, 320);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 13);
            this.label12.TabIndex = 175;
            this.label12.Text = "Tax Identification No.";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(222, 292);
            this.txtEmail.MaxLength = 75;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(318, 21);
            this.txtEmail.TabIndex = 13;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(174, 296);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 174;
            this.label8.Text = "E-mail";
            // 
            // txtMobile
            // 
            this.txtMobile.BackColor = System.Drawing.Color.White;
            this.txtMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.Location = new System.Drawing.Point(222, 268);
            this.txtMobile.MaxLength = 15;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(247, 21);
            this.txtMobile.TabIndex = 12;
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(140, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 173;
            this.label7.Text = "Contact No.";
            // 
            // txtRefNo
            // 
            this.txtRefNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRefNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNo.Location = new System.Drawing.Point(446, 48);
            this.txtRefNo.MaxLength = 10;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(138, 21);
            this.txtRefNo.TabIndex = 3;
            this.txtRefNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(390, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Ref.No.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnVerifyPAN
            // 
            this.btnVerifyPAN.BackColor = System.Drawing.Color.White;
            this.btnVerifyPAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerifyPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnVerifyPAN.Image")));
            this.btnVerifyPAN.Location = new System.Drawing.Point(358, 46);
            this.btnVerifyPAN.Name = "btnVerifyPAN";
            this.btnVerifyPAN.Size = new System.Drawing.Size(28, 25);
            this.btnVerifyPAN.TabIndex = 28;
            this.btnVerifyPAN.TabStop = false;
            this.btnVerifyPAN.UseVisualStyleBackColor = false;
            this.btnVerifyPAN.Click += new System.EventHandler(this.lnkPANVerification_LinkClicked);
            this.btnVerifyPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnVerifyPAN_MouseMove);
            // 
            // lnkPANVerification
            // 
            this.lnkPANVerification.AutoSize = true;
            this.lnkPANVerification.Location = new System.Drawing.Point(524, 53);
            this.lnkPANVerification.Name = "lnkPANVerification";
            this.lnkPANVerification.Size = new System.Drawing.Size(39, 13);
            this.lnkPANVerification.TabIndex = 2;
            this.lnkPANVerification.TabStop = true;
            this.lnkPANVerification.Text = "Verify";
            this.lnkPANVerification.Visible = false;
            this.lnkPANVerification.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPANVerification_LinkClicked);
            // 
            // txtOldPAN
            // 
            this.txtOldPAN.BackColor = System.Drawing.Color.White;
            this.txtOldPAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOldPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPAN.Location = new System.Drawing.Point(346, 49);
            this.txtOldPAN.MaxLength = 10;
            this.txtOldPAN.Name = "txtOldPAN";
            this.txtOldPAN.Size = new System.Drawing.Size(10, 21);
            this.txtOldPAN.TabIndex = 21;
            this.txtOldPAN.Visible = false;
            // 
            // txtPIN
            // 
            this.txtPIN.BackColor = System.Drawing.Color.White;
            this.txtPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.Location = new System.Drawing.Point(222, 244);
            this.txtPIN.MaxLength = 6;
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(101, 21);
            this.txtPIN.TabIndex = 11;
            this.txtPIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPIN_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(187, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "PIN";
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.White;
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(222, 217);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(247, 23);
            this.cmbState.TabIndex = 10;
            this.cmbState.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "State";
            // 
            // txtAddress5
            // 
            this.txtAddress5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress5.Location = new System.Drawing.Point(222, 193);
            this.txtAddress5.MaxLength = 75;
            this.txtAddress5.Name = "txtAddress5";
            this.txtAddress5.Size = new System.Drawing.Size(318, 21);
            this.txtAddress5.TabIndex = 9;
            this.txtAddress5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtAddress4
            // 
            this.txtAddress4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress4.Location = new System.Drawing.Point(222, 169);
            this.txtAddress4.MaxLength = 75;
            this.txtAddress4.Name = "txtAddress4";
            this.txtAddress4.Size = new System.Drawing.Size(318, 21);
            this.txtAddress4.TabIndex = 8;
            this.txtAddress4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtAddress3
            // 
            this.txtAddress3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress3.Location = new System.Drawing.Point(222, 145);
            this.txtAddress3.MaxLength = 75;
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(318, 21);
            this.txtAddress3.TabIndex = 7;
            this.txtAddress3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtAddress2
            // 
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(222, 121);
            this.txtAddress2.MaxLength = 75;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(318, 21);
            this.txtAddress2.TabIndex = 6;
            this.txtAddress2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // lblDeducteeCode
            // 
            this.lblDeducteeCode.AutoSize = true;
            this.lblDeducteeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeducteeCode.ForeColor = System.Drawing.Color.Blue;
            this.lblDeducteeCode.Location = new System.Drawing.Point(277, 27);
            this.lblDeducteeCode.Name = "lblDeducteeCode";
            this.lblDeducteeCode.Size = new System.Drawing.Size(95, 13);
            this.lblDeducteeCode.TabIndex = 9;
            this.lblDeducteeCode.Text = "Deductee Code";
            // 
            // cmbDeducteeCode
            // 
            this.cmbDeducteeCode.BackColor = System.Drawing.Color.White;
            this.cmbDeducteeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeducteeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDeducteeCode.FormattingEnabled = true;
            this.cmbDeducteeCode.Location = new System.Drawing.Point(223, 22);
            this.cmbDeducteeCode.Name = "cmbDeducteeCode";
            this.cmbDeducteeCode.Size = new System.Drawing.Size(47, 23);
            this.cmbDeducteeCode.TabIndex = 0;
            this.cmbDeducteeCode.SelectedIndexChanged += new System.EventHandler(this.cmbDeducteeCode_SelectedIndexChanged);
            this.cmbDeducteeCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(121, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Deductee Code";
            // 
            // txtAddress1
            // 
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(222, 97);
            this.txtAddress1.MaxLength = 75;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(318, 21);
            this.txtAddress1.TabIndex = 5;
            this.txtAddress1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(163, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Address";
            // 
            // txtPAN
            // 
            this.txtPAN.BackColor = System.Drawing.Color.White;
            this.txtPAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAN.Location = new System.Drawing.Point(222, 49);
            this.txtPAN.MaxLength = 10;
            this.txtPAN.Name = "txtPAN";
            this.txtPAN.Size = new System.Drawing.Size(126, 21);
            this.txtPAN.TabIndex = 1;
            this.txtPAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAN_KeyPress);
            this.txtPAN.Leave += new System.EventHandler(this.txtPAN_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(183, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PAN";
            // 
            // txtDeducteeName
            // 
            this.txtDeducteeName.BackColor = System.Drawing.Color.White;
            this.txtDeducteeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeName.Location = new System.Drawing.Point(222, 73);
            this.txtDeducteeName.MaxLength = 75;
            this.txtDeducteeName.Name = "txtDeducteeName";
            this.txtDeducteeName.Size = new System.Drawing.Size(363, 21);
            this.txtDeducteeName.TabIndex = 4;
            this.txtDeducteeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Deductee Name";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(15, 51);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(98, 15);
            this.label114.TabIndex = 159;
            this.label114.Text = "Deductee Name";
            // 
            // txtDeducteeNameSearch
            // 
            this.txtDeducteeNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeducteeNameSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeNameSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeNameSearch.Location = new System.Drawing.Point(114, 47);
            this.txtDeducteeNameSearch.Name = "txtDeducteeNameSearch";
            this.txtDeducteeNameSearch.Size = new System.Drawing.Size(192, 21);
            this.txtDeducteeNameSearch.TabIndex = 1;
            this.txtDeducteeNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(15, 26);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(28, 15);
            this.label117.TabIndex = 157;
            this.label117.Text = "PAN";
            // 
            // txtPANSearch
            // 
            this.txtPANSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPANSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPANSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPANSearch.Location = new System.Drawing.Point(114, 22);
            this.txtPANSearch.MaxLength = 10;
            this.txtPANSearch.Name = "txtPANSearch";
            this.txtPANSearch.Size = new System.Drawing.Size(113, 21);
            this.txtPANSearch.TabIndex = 0;
            this.txtPANSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(15, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 15);
            this.label9.TabIndex = 161;
            this.label9.Text = "Deductee Code";
            // 
            // cmbDeducteeCodeSearch
            // 
            this.cmbDeducteeCodeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeducteeCodeSearch.FormattingEnabled = true;
            this.cmbDeducteeCodeSearch.Location = new System.Drawing.Point(115, 72);
            this.cmbDeducteeCodeSearch.Name = "cmbDeducteeCodeSearch";
            this.cmbDeducteeCodeSearch.Size = new System.Drawing.Size(191, 23);
            this.cmbDeducteeCodeSearch.TabIndex = 2;
            // 
            // tmrGridRefresh
            // 
            this.tmrGridRefresh.Interval = 5000;
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(915, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(82, 32);
            this.pctUserManual.TabIndex = 208;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Video Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvGrid.Location = new System.Drawing.Point(10, 40);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGrid.RowHeadersWidth = 20;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(1000, 15);
            this.dgvGrid.TabIndex = 191;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.Click += new System.EventHandler(this.dgvGrid_Click);
            this.dgvGrid.DoubleClick += new System.EventHandler(this.dgvGrid_DoubleClick);
            this.dgvGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvGrid_MouseUp);
            // 
            // ctxtmnuExport
            // 
            this.ctxtmnuExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpMnuCSV,
            this.tlStrpMnuExcel});
            this.ctxtmnuExport.Name = "ctxtmnuExport";
            this.ctxtmnuExport.Size = new System.Drawing.Size(106, 48);
            // 
            // tlStrpMnuCSV
            // 
            this.tlStrpMnuCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tlStrpMnuCSV.Name = "tlStrpMnuCSV";
            this.tlStrpMnuCSV.Size = new System.Drawing.Size(105, 22);
            this.tlStrpMnuCSV.Text = "CSV";
            this.tlStrpMnuCSV.Click += new System.EventHandler(this.TlStrpMnuCSV_Click);
            // 
            // tlStrpMnuExcel
            // 
            this.tlStrpMnuExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tlStrpMnuExcel.Name = "tlStrpMnuExcel";
            this.tlStrpMnuExcel.Size = new System.Drawing.Size(105, 22);
            this.tlStrpMnuExcel.Text = "Excel";
            this.tlStrpMnuExcel.Click += new System.EventHandler(this.TlStrpMnuExcel_Click);
            // 
            // MstDeductee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.dgvGrid);
            this.Name = "MstDeductee";
            this.Load += new System.EventHandler(this.MstDeductee_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.dgvGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ctxtmnuExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.ComboBox cmbDeducteeCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeducteeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDeducteeCode;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddress5;
        private System.Windows.Forms.TextBox txtAddress4;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtDeducteeNameSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtPANSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOldPAN;
        private System.Windows.Forms.ComboBox cmbDeducteeCodeSearch;
        private System.Windows.Forms.Timer tmrGridRefresh;
        private System.Windows.Forms.LinkLabel lnkPANVerification;
        private System.Windows.Forms.Button btnVerifyPAN;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtDeducteeAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTaxIdentificationNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkHideDeductee;
        private System.Windows.Forms.PictureBox pctUserManual;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.ContextMenuStrip ctxtmnuExport;
        private System.Windows.Forms.ToolStripMenuItem tlStrpMnuCSV;
        private System.Windows.Forms.ToolStripMenuItem tlStrpMnuExcel;
    }
}
