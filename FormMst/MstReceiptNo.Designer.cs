namespace TDSMAN.FormMst
{
    partial class MstReceiptNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstReceiptNo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.grpITDetails = new System.Windows.Forms.GroupBox();
            this.btnCloseIT = new System.Windows.Forms.Button();
            this.btnGoIT = new System.Windows.Forms.Button();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lnkFetch = new System.Windows.Forms.LinkLabel();
            this.txtScannedImagePathM = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.btnPreviewImage = new System.Windows.Forms.Button();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.txtScannedImagePath = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.mskDateOfFiling = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPRNNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFormNo = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtQuarter = new System.Windows.Forms.TextBox();
            this.txtFinancialYear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReceiptNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txtCompanyNameSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReceiptNoSearch = new System.Windows.Forms.TextBox();
            this.label109 = new System.Windows.Forms.Label();
            this.cmbQuarterSearch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFormNoSearch = new System.Windows.Forms.ComboBox();
            this.cmbFinancialYearSearch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTANSearch = new System.Windows.Forms.TextBox();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkBlankTokenNo = new System.Windows.Forms.CheckBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbCompanyFilter = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbQuarterFilter = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbFormNoFilter = new System.Windows.Forms.ComboBox();
            this.cmbFinancialYearFilter = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.dgvGrid = new DGVControl.DGVControl();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            this.grpITDetails.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(24, 625);
            this.grpSort.Size = new System.Drawing.Size(14, 4);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(414, 13);
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(330, 13);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Controls.Add(this.txtTANSearch);
            this.grpSearch.Controls.Add(this.cmbFinancialYearSearch);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.cmbFormNoSearch);
            this.grpSearch.Controls.Add(this.label109);
            this.grpSearch.Controls.Add(this.cmbQuarterSearch);
            this.grpSearch.Controls.Add(this.label9);
            this.grpSearch.Controls.Add(this.txtReceiptNoSearch);
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtCompanyNameSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Location = new System.Drawing.Point(609, 559);
            this.grpSearch.Size = new System.Drawing.Size(319, 10);
            this.grpSearch.Visible = false;
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtCompanyNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtReceiptNoSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label9, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbQuarterSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label109, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbFormNoSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label1, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbFinancialYearSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtTANSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label3, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(229, 179);
            this.BtnSearchCancel.TabIndex = 7;
            this.BtnSearchCancel.Text = "Ca&ncel";
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(158, 179);
            this.BtnSearchOK.TabIndex = 6;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(161, 13);
            this.BtnEdit.TabIndex = 0;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(68, 16);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Location = new System.Drawing.Point(113, 16);
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(670, 13);
            this.BtnExit.TabIndex = 6;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(754, 13);
            this.BtnPrint.Size = new System.Drawing.Size(87, 25);
            this.BtnPrint.Text = "E&xport";
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(582, 13);
            this.BtnRefresh.TabIndex = 5;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(245, 13);
            this.BtnDelete.Size = new System.Drawing.Size(84, 25);
            this.BtnDelete.TabIndex = 1;
            this.BtnDelete.Text = "C&lear";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(498, 13);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Controls.SetChildIndex(this.pctVideoDemo, 0);
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
            this.pnlControls.Location = new System.Drawing.Point(9, 108);
            this.pnlControls.Size = new System.Drawing.Size(1003, 488);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 111);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.grpITDetails);
            this.grpBasicInformation.Controls.Add(this.label21);
            this.grpBasicInformation.Controls.Add(this.label20);
            this.grpBasicInformation.Controls.Add(this.label18);
            this.grpBasicInformation.Controls.Add(this.lblStatus);
            this.grpBasicInformation.Controls.Add(this.lnkFetch);
            this.grpBasicInformation.Controls.Add(this.txtScannedImagePathM);
            this.grpBasicInformation.Controls.Add(this.lblComment);
            this.grpBasicInformation.Controls.Add(this.btnPreviewImage);
            this.grpBasicInformation.Controls.Add(this.btnBrowsePath);
            this.grpBasicInformation.Controls.Add(this.txtScannedImagePath);
            this.grpBasicInformation.Controls.Add(this.label15);
            this.grpBasicInformation.Controls.Add(this.label13);
            this.grpBasicInformation.Controls.Add(this.mskDateOfFiling);
            this.grpBasicInformation.Controls.Add(this.label19);
            this.grpBasicInformation.Controls.Add(this.txtPRNNo);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Controls.Add(this.txtFormNo);
            this.grpBasicInformation.Controls.Add(this.txtCompanyName);
            this.grpBasicInformation.Controls.Add(this.txtQuarter);
            this.grpBasicInformation.Controls.Add(this.txtFinancialYear);
            this.grpBasicInformation.Controls.Add(this.label7);
            this.grpBasicInformation.Controls.Add(this.label8);
            this.grpBasicInformation.Controls.Add(this.label6);
            this.grpBasicInformation.Controls.Add(this.label5);
            this.grpBasicInformation.Controls.Add(this.txtReceiptNo);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Controls.Add(this.label22);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(150, 32);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 314);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // grpITDetails
            // 
            this.grpITDetails.BackColor = System.Drawing.SystemColors.ControlDark;
            this.grpITDetails.Controls.Add(this.btnCloseIT);
            this.grpITDetails.Controls.Add(this.btnGoIT);
            this.grpITDetails.Controls.Add(this.txtTANNo);
            this.grpITDetails.Controls.Add(this.label16);
            this.grpITDetails.Controls.Add(this.txtPassword);
            this.grpITDetails.Controls.Add(this.label17);
            this.grpITDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpITDetails.Location = new System.Drawing.Point(86, 144);
            this.grpITDetails.Name = "grpITDetails";
            this.grpITDetails.Size = new System.Drawing.Size(517, 59);
            this.grpITDetails.TabIndex = 225;
            this.grpITDetails.TabStop = false;
            this.grpITDetails.Text = "Enter IT Login Details";
            this.grpITDetails.Visible = false;
            // 
            // btnCloseIT
            // 
            this.btnCloseIT.BackColor = System.Drawing.Color.Lavender;
            this.btnCloseIT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseIT.ForeColor = System.Drawing.Color.Black;
            this.btnCloseIT.Location = new System.Drawing.Point(434, 20);
            this.btnCloseIT.Name = "btnCloseIT";
            this.btnCloseIT.Size = new System.Drawing.Size(47, 23);
            this.btnCloseIT.TabIndex = 5;
            this.btnCloseIT.Text = "&Close";
            this.btnCloseIT.UseVisualStyleBackColor = false;
            this.btnCloseIT.Click += new System.EventHandler(this.BtnCloseIT_Click);
            // 
            // btnGoIT
            // 
            this.btnGoIT.BackColor = System.Drawing.Color.Lavender;
            this.btnGoIT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoIT.ForeColor = System.Drawing.Color.Black;
            this.btnGoIT.Location = new System.Drawing.Point(387, 20);
            this.btnGoIT.Name = "btnGoIT";
            this.btnGoIT.Size = new System.Drawing.Size(47, 23);
            this.btnGoIT.TabIndex = 4;
            this.btnGoIT.Text = "&Go";
            this.btnGoIT.UseVisualStyleBackColor = false;
            this.btnGoIT.Click += new System.EventHandler(this.BtnGoIT_Click);
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(60, 21);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.ReadOnly = true;
            this.txtTANNo.Size = new System.Drawing.Size(96, 20);
            this.txtTANNo.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(24, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 13);
            this.label16.TabIndex = 209;
            this.label16.Text = "TAN";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(231, 21);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(145, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(166, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 13);
            this.label17.TabIndex = 208;
            this.label17.Text = "Password";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(448, 153);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 50);
            this.label21.TabIndex = 228;
            this.label21.Text = "------";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(421, 196);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 227;
            this.label20.Text = "------";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(421, 146);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 226;
            this.label18.Text = "------";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(462, 185);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 149;
            // 
            // lnkFetch
            // 
            this.lnkFetch.AutoSize = true;
            this.lnkFetch.Location = new System.Drawing.Point(458, 167);
            this.lnkFetch.Name = "lnkFetch";
            this.lnkFetch.Size = new System.Drawing.Size(186, 13);
            this.lnkFetch.TabIndex = 148;
            this.lnkFetch.TabStop = true;
            this.lnkFetch.Text = "Fetch details from e-filing portal";
            this.lnkFetch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkFetch_LinkClicked);
            // 
            // txtScannedImagePathM
            // 
            this.txtScannedImagePathM.BackColor = System.Drawing.SystemColors.Info;
            this.txtScannedImagePathM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScannedImagePathM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannedImagePathM.Location = new System.Drawing.Point(496, 218);
            this.txtScannedImagePathM.MaxLength = 8;
            this.txtScannedImagePathM.Name = "txtScannedImagePathM";
            this.txtScannedImagePathM.ReadOnly = true;
            this.txtScannedImagePathM.Size = new System.Drawing.Size(10, 20);
            this.txtScannedImagePathM.TabIndex = 147;
            this.txtScannedImagePathM.Visible = false;
            // 
            // lblComment
            // 
            this.lblComment.BackColor = System.Drawing.Color.Transparent;
            this.lblComment.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.ForeColor = System.Drawing.Color.Blue;
            this.lblComment.Location = new System.Drawing.Point(50, 244);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(618, 60);
            this.lblComment.TabIndex = 146;
            // 
            // btnPreviewImage
            // 
            this.btnPreviewImage.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviewImage.Image")));
            this.btnPreviewImage.Location = new System.Drawing.Point(542, 216);
            this.btnPreviewImage.Name = "btnPreviewImage";
            this.btnPreviewImage.Size = new System.Drawing.Size(32, 23);
            this.btnPreviewImage.TabIndex = 145;
            this.btnPreviewImage.UseVisualStyleBackColor = true;
            this.btnPreviewImage.Click += new System.EventHandler(this.btnPreviewImage_Click);
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(509, 216);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(32, 23);
            this.btnBrowsePath.TabIndex = 144;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            // 
            // txtScannedImagePath
            // 
            this.txtScannedImagePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtScannedImagePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScannedImagePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannedImagePath.Location = new System.Drawing.Point(253, 218);
            this.txtScannedImagePath.MaxLength = 8;
            this.txtScannedImagePath.Name = "txtScannedImagePath";
            this.txtScannedImagePath.ReadOnly = true;
            this.txtScannedImagePath.Size = new System.Drawing.Size(253, 20);
            this.txtScannedImagePath.TabIndex = 142;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(245, 13);
            this.label15.TabIndex = 143;
            this.label15.Text = "Tax Invoice cum Provisional Receipt Path";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(165, 171);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 141;
            this.label13.Text = "Date of Filing";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mskDateOfFiling
            // 
            this.mskDateOfFiling.BackColor = System.Drawing.SystemColors.Window;
            this.mskDateOfFiling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskDateOfFiling.Location = new System.Drawing.Point(253, 168);
            this.mskDateOfFiling.Mask = "00/00/0000";
            this.mskDateOfFiling.Name = "mskDateOfFiling";
            this.mskDateOfFiling.Size = new System.Drawing.Size(89, 20);
            this.mskDateOfFiling.TabIndex = 5;
            this.mskDateOfFiling.ValidatingType = typeof(System.DateTime);
            this.mskDateOfFiling.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(345, 172);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 15);
            this.label19.TabIndex = 140;
            this.label19.Text = "DD/MM/YYYY";
            // 
            // txtPRNNo
            // 
            this.txtPRNNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPRNNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRNNo.Location = new System.Drawing.Point(253, 193);
            this.txtPRNNo.MaxLength = 15;
            this.txtPRNNo.Name = "txtPRNNo";
            this.txtPRNNo.Size = new System.Drawing.Size(167, 20);
            this.txtPRNNo.TabIndex = 6;
            this.txtPRNNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(124, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Token No./RRR No.";
            // 
            // txtFormNo
            // 
            this.txtFormNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtFormNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtFormNo.Location = new System.Drawing.Point(253, 118);
            this.txtFormNo.MaxLength = 8;
            this.txtFormNo.Name = "txtFormNo";
            this.txtFormNo.ReadOnly = true;
            this.txtFormNo.Size = new System.Drawing.Size(167, 20);
            this.txtFormNo.TabIndex = 3;
            this.txtFormNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BackColor = System.Drawing.SystemColors.Info;
            this.txtCompanyName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Location = new System.Drawing.Point(253, 92);
            this.txtCompanyName.MaxLength = 8;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.ReadOnly = true;
            this.txtCompanyName.Size = new System.Drawing.Size(380, 20);
            this.txtCompanyName.TabIndex = 2;
            this.txtCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtQuarter
            // 
            this.txtQuarter.BackColor = System.Drawing.SystemColors.Info;
            this.txtQuarter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtQuarter.Location = new System.Drawing.Point(253, 67);
            this.txtQuarter.MaxLength = 8;
            this.txtQuarter.Name = "txtQuarter";
            this.txtQuarter.ReadOnly = true;
            this.txtQuarter.Size = new System.Drawing.Size(167, 20);
            this.txtQuarter.TabIndex = 1;
            this.txtQuarter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtFinancialYear
            // 
            this.txtFinancialYear.BackColor = System.Drawing.SystemColors.Info;
            this.txtFinancialYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFinancialYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinancialYear.Location = new System.Drawing.Point(253, 42);
            this.txtFinancialYear.MaxLength = 8;
            this.txtFinancialYear.Name = "txtFinancialYear";
            this.txtFinancialYear.ReadOnly = true;
            this.txtFinancialYear.Size = new System.Drawing.Size(167, 20);
            this.txtFinancialYear.TabIndex = 0;
            this.txtFinancialYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(200, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Quarter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(155, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Company Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(191, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Tax Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(191, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Form No.";
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiptNo.Location = new System.Drawing.Point(253, 143);
            this.txtReceiptNo.MaxLength = 15;
            this.txtReceiptNo.Name = "txtReceiptNo";
            this.txtReceiptNo.Size = new System.Drawing.Size(167, 20);
            this.txtReceiptNo.TabIndex = 4;
            this.txtReceiptNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(67, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Receipt No./Acknowledge No.";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(446, 168);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 13);
            this.label22.TabIndex = 229;
            this.label22.Text = "-";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(8, 77);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(82, 13);
            this.label114.TabIndex = 159;
            this.label114.Text = "Company Name";
            // 
            // txtCompanyNameSearch
            // 
            this.txtCompanyNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyNameSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompanyNameSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtCompanyNameSearch.Location = new System.Drawing.Point(116, 74);
            this.txtCompanyNameSearch.Name = "txtCompanyNameSearch";
            this.txtCompanyNameSearch.Size = new System.Drawing.Size(192, 20);
            this.txtCompanyNameSearch.TabIndex = 2;
            this.txtCompanyNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(8, 24);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(74, 13);
            this.label117.TabIndex = 157;
            this.label117.Text = "Tax Year";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(8, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 161;
            this.label9.Text = "Token No";
            // 
            // txtReceiptNoSearch
            // 
            this.txtReceiptNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceiptNoSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNoSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtReceiptNoSearch.Location = new System.Drawing.Point(116, 151);
            this.txtReceiptNoSearch.MaxLength = 20;
            this.txtReceiptNoSearch.Name = "txtReceiptNoSearch";
            this.txtReceiptNoSearch.Size = new System.Drawing.Size(136, 20);
            this.txtReceiptNoSearch.TabIndex = 5;
            this.txtReceiptNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label109
            // 
            this.label109.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label109.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label109.Location = new System.Drawing.Point(8, 51);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(104, 15);
            this.label109.TabIndex = 163;
            this.label109.Text = "Quarter";
            // 
            // cmbQuarterSearch
            // 
            this.cmbQuarterSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarterSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbQuarterSearch.FormattingEnabled = true;
            this.cmbQuarterSearch.Location = new System.Drawing.Point(116, 47);
            this.cmbQuarterSearch.Name = "cmbQuarterSearch";
            this.cmbQuarterSearch.Size = new System.Drawing.Size(67, 21);
            this.cmbQuarterSearch.TabIndex = 1;
            this.cmbQuarterSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(8, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 165;
            this.label1.Text = "Form No.";
            // 
            // cmbFormNoSearch
            // 
            this.cmbFormNoSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNoSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbFormNoSearch.FormattingEnabled = true;
            this.cmbFormNoSearch.Location = new System.Drawing.Point(116, 124);
            this.cmbFormNoSearch.Name = "cmbFormNoSearch";
            this.cmbFormNoSearch.Size = new System.Drawing.Size(67, 21);
            this.cmbFormNoSearch.TabIndex = 4;
            this.cmbFormNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // cmbFinancialYearSearch
            // 
            this.cmbFinancialYearSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbFinancialYearSearch.FormattingEnabled = true;
            this.cmbFinancialYearSearch.Location = new System.Drawing.Point(116, 20);
            this.cmbFinancialYearSearch.Name = "cmbFinancialYearSearch";
            this.cmbFinancialYearSearch.Size = new System.Drawing.Size(119, 21);
            this.cmbFinancialYearSearch.TabIndex = 0;
            this.cmbFinancialYearSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(8, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 168;
            this.label3.Text = "TAN No.";
            // 
            // txtTANSearch
            // 
            this.txtTANSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTANSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtTANSearch.Location = new System.Drawing.Point(116, 99);
            this.txtTANSearch.Name = "txtTANSearch";
            this.txtTANSearch.Size = new System.Drawing.Size(192, 20);
            this.txtTANSearch.TabIndex = 3;
            this.txtTANSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.chkBlankTokenNo);
            this.grpFilter.Controls.Add(this.lnkSearchByTAN);
            this.grpFilter.Controls.Add(this.cmbCompanyFilter);
            this.grpFilter.Controls.Add(this.label14);
            this.grpFilter.Controls.Add(this.label12);
            this.grpFilter.Controls.Add(this.cmbQuarterFilter);
            this.grpFilter.Controls.Add(this.label11);
            this.grpFilter.Controls.Add(this.cmbFormNoFilter);
            this.grpFilter.Controls.Add(this.cmbFinancialYearFilter);
            this.grpFilter.Controls.Add(this.label10);
            this.grpFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilter.Location = new System.Drawing.Point(9, 45);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(1007, 58);
            this.grpFilter.TabIndex = 177;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Create Filter";
            // 
            // chkBlankTokenNo
            // 
            this.chkBlankTokenNo.AutoSize = true;
            this.chkBlankTokenNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBlankTokenNo.Location = new System.Drawing.Point(859, 35);
            this.chkBlankTokenNo.Name = "chkBlankTokenNo";
            this.chkBlankTokenNo.Size = new System.Drawing.Size(122, 17);
            this.chkBlankTokenNo.TabIndex = 217;
            this.chkBlankTokenNo.TabStop = false;
            this.chkBlankTokenNo.Text = "Blank Token No.";
            this.chkBlankTokenNo.UseVisualStyleBackColor = true;
            this.chkBlankTokenNo.CheckedChanged += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(861, 16);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 216;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbCompanyFilter
            // 
            this.cmbCompanyFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompanyFilter.FormattingEnabled = true;
            this.cmbCompanyFilter.Location = new System.Drawing.Point(474, 21);
            this.cmbCompanyFilter.Name = "cmbCompanyFilter";
            this.cmbCompanyFilter.Size = new System.Drawing.Size(366, 21);
            this.cmbCompanyFilter.TabIndex = 184;
            this.cmbCompanyFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label14.Location = new System.Drawing.Point(411, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 16);
            this.label14.TabIndex = 183;
            this.label14.Text = "Company Name";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(312, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 15);
            this.label12.TabIndex = 181;
            this.label12.Text = "Qtr";
            // 
            // cmbQuarterFilter
            // 
            this.cmbQuarterFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarterFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQuarterFilter.FormattingEnabled = true;
            this.cmbQuarterFilter.Location = new System.Drawing.Point(346, 20);
            this.cmbQuarterFilter.Name = "cmbQuarterFilter";
            this.cmbQuarterFilter.Size = new System.Drawing.Size(61, 21);
            this.cmbQuarterFilter.TabIndex = 182;
            this.cmbQuarterFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label11.Location = new System.Drawing.Point(198, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 15);
            this.label11.TabIndex = 179;
            this.label11.Text = "Form No.";
            // 
            // cmbFormNoFilter
            // 
            this.cmbFormNoFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNoFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormNoFilter.FormattingEnabled = true;
            this.cmbFormNoFilter.Location = new System.Drawing.Point(241, 20);
            this.cmbFormNoFilter.Name = "cmbFormNoFilter";
            this.cmbFormNoFilter.Size = new System.Drawing.Size(65, 21);
            this.cmbFormNoFilter.TabIndex = 180;
            this.cmbFormNoFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            this.cmbFormNoFilter.Enter += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            // 
            // cmbFinancialYearFilter
            // 
            this.cmbFinancialYearFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFinancialYearFilter.FormattingEnabled = true;
            this.cmbFinancialYearFilter.Location = new System.Drawing.Point(101, 20);
            this.cmbFinancialYearFilter.Name = "cmbFinancialYearFilter";
            this.cmbFinancialYearFilter.Size = new System.Drawing.Size(94, 21);
            this.cmbFinancialYearFilter.TabIndex = 178;
            this.cmbFinancialYearFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYearFilter_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label10.Location = new System.Drawing.Point(39, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 177;
            this.label10.Text = "Tax Year";
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(917, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 9;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Demo";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(954, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(38, 32);
            this.pctUserManual.TabIndex = 10;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Manual Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvGrid.Location = new System.Drawing.Point(9, 110);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGrid.RowHeadersWidth = 20;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(1003, 24);
            this.dgvGrid.TabIndex = 193;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.dgvGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.dgvGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            // 
            // MstReceiptNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.grpFilter);
            this.Name = "MstReceiptNo";
            this.Load += new System.EventHandler(this.MstReceiptNo_Load);
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
            this.Controls.SetChildIndex(this.grpFilter, 0);
            this.Controls.SetChildIndex(this.dgvGrid, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            this.grpITDetails.ResumeLayout(false);
            this.grpITDetails.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReceiptNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtCompanyNameSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReceiptNoSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFormNo;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TextBox txtQuarter;
        private System.Windows.Forms.TextBox txtFinancialYear;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.ComboBox cmbQuarterSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFormNoSearch;
        private System.Windows.Forms.TextBox txtPRNNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox mskDateOfFiling;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbFinancialYearSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTANSearch;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ComboBox cmbCompanyFilter;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbQuarterFilter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbFormNoFilter;
        private System.Windows.Forms.ComboBox cmbFinancialYearFilter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtScannedImagePath;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnPreviewImage;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtScannedImagePathM;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.CheckBox chkBlankTokenNo;
        private System.Windows.Forms.LinkLabel lnkFetch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpITDetails;
        private System.Windows.Forms.Button btnCloseIT;
        private System.Windows.Forms.Button btnGoIT;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label22;
    }
}
