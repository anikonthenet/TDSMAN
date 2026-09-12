namespace TDSMAN.FormMst
{
    partial class MstEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstEmployee));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkHideEmployee = new System.Windows.Forms.CheckBox();
            this.btnVerifyPAN = new System.Windows.Forms.Button();
            this.lnkPANVerification = new System.Windows.Forms.LinkLabel();
            this.txtEmployeeRefNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmployeeCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmployeePAN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txtEmployeeNameSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtPANSearch = new System.Windows.Forms.TextBox();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCategorySearch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrGridRefresh = new System.Windows.Forms.Timer(this.components);
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.dgvGrid = new DGVControl.DGVControl();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
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
            this.BtnCancel.Location = new System.Drawing.Point(374, 13);
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(290, 13);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.cmbCategorySearch);
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtEmployeeNameSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Controls.Add(this.txtPANSearch);
            this.grpSearch.Location = new System.Drawing.Point(605, 450);
            this.grpSearch.Size = new System.Drawing.Size(320, 143);
            this.grpSearch.Controls.SetChildIndex(this.txtPANSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtEmployeeNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbCategorySearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.label1, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(236, 109);
            this.BtnSearchCancel.TabIndex = 4;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(165, 109);
            this.BtnSearchOK.TabIndex = 3;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(206, 13);
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(122, 12);
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(714, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(798, 13);
            this.BtnPrint.Size = new System.Drawing.Size(83, 25);
            this.BtnPrint.Text = "Export";
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            this.BtnPrint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnPrint_MouseClick);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(626, 13);
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(542, 13);
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(458, 13);
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
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
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.txtEmail);
            this.grpBasicInformation.Controls.Add(this.label8);
            this.grpBasicInformation.Controls.Add(this.chkHideEmployee);
            this.grpBasicInformation.Controls.Add(this.btnVerifyPAN);
            this.grpBasicInformation.Controls.Add(this.lnkPANVerification);
            this.grpBasicInformation.Controls.Add(this.txtEmployeeRefNo);
            this.grpBasicInformation.Controls.Add(this.label6);
            this.grpBasicInformation.Controls.Add(this.cmbEmployeeCategory);
            this.grpBasicInformation.Controls.Add(this.label5);
            this.grpBasicInformation.Controls.Add(this.txtDesignation);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Controls.Add(this.txtEmployeePAN);
            this.grpBasicInformation.Controls.Add(this.label3);
            this.grpBasicInformation.Controls.Add(this.txtEmployeeName);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(150, 88);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 219);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(221, 159);
            this.txtEmail.MaxLength = 75;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(318, 21);
            this.txtEmail.TabIndex = 5;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(174, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 186;
            this.label8.Text = "E-mail";
            // 
            // chkHideEmployee
            // 
            this.chkHideEmployee.AutoSize = true;
            this.chkHideEmployee.Location = new System.Drawing.Point(434, 186);
            this.chkHideEmployee.Name = "chkHideEmployee";
            this.chkHideEmployee.Size = new System.Drawing.Size(171, 17);
            this.chkHideEmployee.TabIndex = 184;
            this.chkHideEmployee.Text = "Hide Employee from Entry";
            this.chkHideEmployee.UseVisualStyleBackColor = true;
            this.chkHideEmployee.Visible = false;
            // 
            // btnVerifyPAN
            // 
            this.btnVerifyPAN.BackColor = System.Drawing.Color.White;
            this.btnVerifyPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnVerifyPAN.Image")));
            this.btnVerifyPAN.Location = new System.Drawing.Point(350, 26);
            this.btnVerifyPAN.Name = "btnVerifyPAN";
            this.btnVerifyPAN.Size = new System.Drawing.Size(28, 25);
            this.btnVerifyPAN.TabIndex = 27;
            this.btnVerifyPAN.TabStop = false;
            this.btnVerifyPAN.UseVisualStyleBackColor = false;
            this.btnVerifyPAN.Click += new System.EventHandler(this.lnkPANVerification_LinkClicked);
            this.btnVerifyPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnVerifyPAN_MouseMove);
            // 
            // lnkPANVerification
            // 
            this.lnkPANVerification.AutoSize = true;
            this.lnkPANVerification.Location = new System.Drawing.Point(446, 17);
            this.lnkPANVerification.Name = "lnkPANVerification";
            this.lnkPANVerification.Size = new System.Drawing.Size(39, 13);
            this.lnkPANVerification.TabIndex = 3;
            this.lnkPANVerification.TabStop = true;
            this.lnkPANVerification.Text = "Verify";
            this.lnkPANVerification.Visible = false;
            this.lnkPANVerification.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPANVerification_LinkClicked);
            // 
            // txtEmployeeRefNo
            // 
            this.txtEmployeeRefNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmployeeRefNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeRefNo.Location = new System.Drawing.Point(221, 106);
            this.txtEmployeeRefNo.MaxLength = 10;
            this.txtEmployeeRefNo.Name = "txtEmployeeRefNo";
            this.txtEmployeeRefNo.Size = new System.Drawing.Size(126, 21);
            this.txtEmployeeRefNo.TabIndex = 3;
            this.txtEmployeeRefNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Employee Ref.No.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbEmployeeCategory
            // 
            this.cmbEmployeeCategory.BackColor = System.Drawing.Color.White;
            this.cmbEmployeeCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployeeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmployeeCategory.FormattingEnabled = true;
            this.cmbEmployeeCategory.Location = new System.Drawing.Point(221, 78);
            this.cmbEmployeeCategory.Name = "cmbEmployeeCategory";
            this.cmbEmployeeCategory.Size = new System.Drawing.Size(363, 23);
            this.cmbEmployeeCategory.TabIndex = 2;
            this.cmbEmployeeCategory.SelectedIndexChanged += new System.EventHandler(this.cmbEmployeeCategory_SelectedIndexChanged);
            this.cmbEmployeeCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(100, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Employee Category";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDesignation
            // 
            this.txtDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesignation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignation.Location = new System.Drawing.Point(221, 133);
            this.txtDesignation.MaxLength = 20;
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(363, 21);
            this.txtDesignation.TabIndex = 4;
            this.txtDesignation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(141, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Designation";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmployeePAN
            // 
            this.txtEmployeePAN.BackColor = System.Drawing.Color.White;
            this.txtEmployeePAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmployeePAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeePAN.Location = new System.Drawing.Point(221, 28);
            this.txtEmployeePAN.MaxLength = 10;
            this.txtEmployeePAN.Name = "txtEmployeePAN";
            this.txtEmployeePAN.Size = new System.Drawing.Size(126, 21);
            this.txtEmployeePAN.TabIndex = 0;
            this.txtEmployeePAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAN_KeyPress);
            this.txtEmployeePAN.Leave += new System.EventHandler(this.txtPAN_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(125, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Employee PAN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.BackColor = System.Drawing.Color.White;
            this.txtEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeName.Location = new System.Drawing.Point(221, 53);
            this.txtEmployeeName.MaxLength = 75;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(363, 21);
            this.txtEmployeeName.TabIndex = 1;
            this.txtEmployeeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(118, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Employee Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(15, 50);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(84, 13);
            this.label114.TabIndex = 159;
            this.label114.Text = "Employee Name";
            // 
            // txtEmployeeNameSearch
            // 
            this.txtEmployeeNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeNameSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtEmployeeNameSearch.Location = new System.Drawing.Point(114, 47);
            this.txtEmployeeNameSearch.Name = "txtEmployeeNameSearch";
            this.txtEmployeeNameSearch.Size = new System.Drawing.Size(192, 20);
            this.txtEmployeeNameSearch.TabIndex = 1;
            this.txtEmployeeNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(15, 25);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(29, 13);
            this.label117.TabIndex = 157;
            this.label117.Text = "PAN";
            // 
            // txtPANSearch
            // 
            this.txtPANSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPANSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPANSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPANSearch.Location = new System.Drawing.Point(114, 22);
            this.txtPANSearch.MaxLength = 10;
            this.txtPANSearch.Name = "txtPANSearch";
            this.txtPANSearch.Size = new System.Drawing.Size(113, 20);
            this.txtPANSearch.TabIndex = 0;
            this.txtPANSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
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
            // cmbCategorySearch
            // 
            this.cmbCategorySearch.BackColor = System.Drawing.Color.White;
            this.cmbCategorySearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategorySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategorySearch.FormattingEnabled = true;
            this.cmbCategorySearch.Location = new System.Drawing.Point(114, 71);
            this.cmbCategorySearch.Name = "cmbCategorySearch";
            this.cmbCategorySearch.Size = new System.Drawing.Size(192, 23);
            this.cmbCategorySearch.TabIndex = 2;
            this.cmbCategorySearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(15, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 161;
            this.label1.Text = "Category";
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
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(82, 32);
            this.pctVideoDemo.TabIndex = 209;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
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
            this.dgvGrid.Location = new System.Drawing.Point(9, 80);
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
            this.dgvGrid.Size = new System.Drawing.Size(1001, 516);
            this.dgvGrid.TabIndex = 192;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.Click += new System.EventHandler(this.dgvGrid_Click);
            this.dgvGrid.DoubleClick += new System.EventHandler(this.dgvGrid_DoubleClick);
            this.dgvGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvGrid_MouseUp);
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(844, 53);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(32, 13);
            this.lnkSearchByTAN.TabIndex = 193;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // ctxtmnuExport
            // 
            this.ctxtmnuExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpMnuCSV,
            this.tlStrpMnuExcel});
            this.ctxtmnuExport.Name = "ctxtmnuExport";
            this.ctxtmnuExport.Size = new System.Drawing.Size(181, 70);
            // 
            // tlStrpMnuCSV
            // 
            this.tlStrpMnuCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tlStrpMnuCSV.Name = "tlStrpMnuCSV";
            this.tlStrpMnuCSV.Size = new System.Drawing.Size(180, 22);
            this.tlStrpMnuCSV.Text = "CSV";
            this.tlStrpMnuCSV.Click += new System.EventHandler(this.TlStrpMnuCSV_Click);
            // 
            // tlStrpMnuExcel
            // 
            this.tlStrpMnuExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tlStrpMnuExcel.Name = "tlStrpMnuExcel";
            this.tlStrpMnuExcel.Size = new System.Drawing.Size(180, 22);
            this.tlStrpMnuExcel.Text = "Excel";
            this.tlStrpMnuExcel.Click += new System.EventHandler(this.TlStrpMnuExcel_Click);
            // 
            // MstEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.lnkSearchByTAN);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.cmbCompanyName);
            this.Controls.Add(this.label7);
            this.Name = "MstEmployee";
            this.Load += new System.EventHandler(this.MstEmployee_Load);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbCompanyName, 0);
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
            this.ctxtmnuExport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.ComboBox cmbEmployeeCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDesignation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmployeePAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtEmployeeNameSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtPANSearch;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCategorySearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrGridRefresh;
        private System.Windows.Forms.TextBox txtEmployeeRefNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkPANVerification;
        private System.Windows.Forms.Button btnVerifyPAN;
        private System.Windows.Forms.CheckBox chkHideEmployee;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.ContextMenuStrip ctxtmnuExport;
        private System.Windows.Forms.ToolStripMenuItem tlStrpMnuCSV;
        private System.Windows.Forms.ToolStripMenuItem tlStrpMnuExcel;
    }
}
