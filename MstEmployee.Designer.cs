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
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
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
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 657);
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
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
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
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // grpBasicInformation
            // 
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
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 359);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // btnVerifyPAN
            // 
            this.btnVerifyPAN.BackColor = System.Drawing.Color.White;
            this.btnVerifyPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnVerifyPAN.Image")));
            this.btnVerifyPAN.Location = new System.Drawing.Point(350, 85);
            this.btnVerifyPAN.Name = "btnVerifyPAN";
            this.btnVerifyPAN.Size = new System.Drawing.Size(28, 25);
            this.btnVerifyPAN.TabIndex = 27;
            this.btnVerifyPAN.TabStop = false;
            this.btnVerifyPAN.UseVisualStyleBackColor = false;
            this.btnVerifyPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnVerifyPAN_MouseMove);
            this.btnVerifyPAN.Click += new System.EventHandler(this.lnkPANVerification_LinkClicked);
            // 
            // lnkPANVerification
            // 
            this.lnkPANVerification.AutoSize = true;
            this.lnkPANVerification.Location = new System.Drawing.Point(446, 76);
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
            this.txtEmployeeRefNo.Location = new System.Drawing.Point(221, 165);
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
            this.label6.Location = new System.Drawing.Point(109, 168);
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
            this.cmbEmployeeCategory.Location = new System.Drawing.Point(221, 137);
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
            this.label5.Location = new System.Drawing.Point(102, 141);
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
            this.txtDesignation.Location = new System.Drawing.Point(221, 192);
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
            this.label4.Location = new System.Drawing.Point(143, 195);
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
            this.txtEmployeePAN.Location = new System.Drawing.Point(221, 87);
            this.txtEmployeePAN.MaxLength = 10;
            this.txtEmployeePAN.Name = "txtEmployeePAN";
            this.txtEmployeePAN.Size = new System.Drawing.Size(126, 21);
            this.txtEmployeePAN.TabIndex = 0;
            this.txtEmployeePAN.Leave += new System.EventHandler(this.txtPAN_Leave);
            this.txtEmployeePAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAN_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(127, 91);
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
            this.txtEmployeeName.Location = new System.Drawing.Point(221, 112);
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
            this.label2.Location = new System.Drawing.Point(120, 115);
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
            // MstEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.cmbCompanyName);
            this.Controls.Add(this.label7);
            this.Name = "MstEmployee";
            this.Load += new System.EventHandler(this.MstEmployee_Load);
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
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbCompanyName, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
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
    }
}
