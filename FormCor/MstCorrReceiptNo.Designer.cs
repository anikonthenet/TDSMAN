namespace TDSMAN.FormMst
{
    partial class MstCorrReceiptNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstCorrReceiptNo));
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
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
            this.pctManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).BeginInit();
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
            this.grpSearch.Location = new System.Drawing.Point(609, 377);
            this.grpSearch.Size = new System.Drawing.Size(319, 214);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
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
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(229, 169);
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(158, 169);
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(930, 16);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
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
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(672, 13);
            this.BtnPrint.Size = new System.Drawing.Size(83, 25);
            this.BtnPrint.Text = "E&xport";
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(584, 13);
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(947, 16);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctManual, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBasicInformation);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 40);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 97);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            // 
            // grpBasicInformation
            // 
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
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(150, 101);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 302);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // txtScannedImagePathM
            // 
            this.txtScannedImagePathM.BackColor = System.Drawing.SystemColors.Info;
            this.txtScannedImagePathM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScannedImagePathM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannedImagePathM.Location = new System.Drawing.Point(500, 217);
            this.txtScannedImagePathM.MaxLength = 8;
            this.txtScannedImagePathM.Name = "txtScannedImagePathM";
            this.txtScannedImagePathM.ReadOnly = true;
            this.txtScannedImagePathM.Size = new System.Drawing.Size(10, 20);
            this.txtScannedImagePathM.TabIndex = 153;
            this.txtScannedImagePathM.Visible = false;
            // 
            // lblComment
            // 
            this.lblComment.BackColor = System.Drawing.Color.Transparent;
            this.lblComment.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.ForeColor = System.Drawing.Color.Blue;
            this.lblComment.Location = new System.Drawing.Point(38, 219);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(618, 67);
            this.lblComment.TabIndex = 152;
            // 
            // btnPreviewImage
            // 
            this.btnPreviewImage.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviewImage.Image")));
            this.btnPreviewImage.Location = new System.Drawing.Point(546, 191);
            this.btnPreviewImage.Name = "btnPreviewImage";
            this.btnPreviewImage.Size = new System.Drawing.Size(32, 23);
            this.btnPreviewImage.TabIndex = 151;
            this.btnPreviewImage.UseVisualStyleBackColor = true;
            this.btnPreviewImage.Click += new System.EventHandler(this.btnPreviewImage_Click);
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(513, 191);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(32, 23);
            this.btnBrowsePath.TabIndex = 150;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            // 
            // txtScannedImagePath
            // 
            this.txtScannedImagePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtScannedImagePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScannedImagePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannedImagePath.Location = new System.Drawing.Point(257, 193);
            this.txtScannedImagePath.MaxLength = 8;
            this.txtScannedImagePath.Name = "txtScannedImagePath";
            this.txtScannedImagePath.ReadOnly = true;
            this.txtScannedImagePath.Size = new System.Drawing.Size(253, 20);
            this.txtScannedImagePath.TabIndex = 148;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 197);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(245, 13);
            this.label15.TabIndex = 149;
            this.label15.Text = "Tax Invoice cum Provisional Receipt Path";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(123, 146);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 15);
            this.label13.TabIndex = 141;
            this.label13.Text = "Date of Filing";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mskDateOfFiling
            // 
            this.mskDateOfFiling.BackColor = System.Drawing.SystemColors.Window;
            this.mskDateOfFiling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskDateOfFiling.Location = new System.Drawing.Point(221, 144);
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
            this.label19.Location = new System.Drawing.Point(313, 148);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 15);
            this.label19.TabIndex = 140;
            this.label19.Text = "DD/MM/YYYY";
            // 
            // txtPRNNo
            // 
            this.txtPRNNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPRNNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRNNo.Location = new System.Drawing.Point(221, 169);
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
            this.label2.Location = new System.Drawing.Point(92, 173);
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
            this.txtFormNo.Location = new System.Drawing.Point(221, 94);
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
            this.txtCompanyName.Location = new System.Drawing.Point(221, 68);
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
            this.txtQuarter.Location = new System.Drawing.Point(221, 43);
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
            this.txtFinancialYear.Location = new System.Drawing.Point(221, 18);
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
            this.label7.Location = new System.Drawing.Point(167, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Quarter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(122, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Company Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(128, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Tax Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(158, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Form No.";
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiptNo.Location = new System.Drawing.Point(221, 119);
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
            this.label4.Location = new System.Drawing.Point(35, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Receipt No./Acknowledge No.";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(8, 78);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(91, 15);
            this.label114.TabIndex = 159;
            this.label114.Text = "Company Name";
            // 
            // txtCompanyNameSearch
            // 
            this.txtCompanyNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyNameSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyNameSearch.Location = new System.Drawing.Point(116, 74);
            this.txtCompanyNameSearch.Name = "txtCompanyNameSearch";
            this.txtCompanyNameSearch.Size = new System.Drawing.Size(192, 21);
            this.txtCompanyNameSearch.TabIndex = 158;
            this.txtCompanyNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(8, 26);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(105, 15);
            this.label117.TabIndex = 157;
            this.label117.Text = "Tax Year";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(8, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 15);
            this.label9.TabIndex = 161;
            this.label9.Text = "Receipt No";
            // 
            // txtReceiptNoSearch
            // 
            this.txtReceiptNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceiptNoSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNoSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiptNoSearch.Location = new System.Drawing.Point(116, 127);
            this.txtReceiptNoSearch.MaxLength = 20;
            this.txtReceiptNoSearch.Name = "txtReceiptNoSearch";
            this.txtReceiptNoSearch.Size = new System.Drawing.Size(136, 21);
            this.txtReceiptNoSearch.TabIndex = 159;
            this.txtReceiptNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label109
            // 
            this.label109.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbQuarterSearch.FormattingEnabled = true;
            this.cmbQuarterSearch.Location = new System.Drawing.Point(116, 47);
            this.cmbQuarterSearch.Name = "cmbQuarterSearch";
            this.cmbQuarterSearch.Size = new System.Drawing.Size(67, 23);
            this.cmbQuarterSearch.TabIndex = 162;
            this.cmbQuarterSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(8, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 165;
            this.label1.Text = "Form No.";
            // 
            // cmbFormNoSearch
            // 
            this.cmbFormNoSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNoSearch.FormattingEnabled = true;
            this.cmbFormNoSearch.Location = new System.Drawing.Point(116, 99);
            this.cmbFormNoSearch.Name = "cmbFormNoSearch";
            this.cmbFormNoSearch.Size = new System.Drawing.Size(67, 23);
            this.cmbFormNoSearch.TabIndex = 164;
            this.cmbFormNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // cmbFinancialYearSearch
            // 
            this.cmbFinancialYearSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYearSearch.FormattingEnabled = true;
            this.cmbFinancialYearSearch.Location = new System.Drawing.Point(116, 20);
            this.cmbFinancialYearSearch.Name = "cmbFinancialYearSearch";
            this.cmbFinancialYearSearch.Size = new System.Drawing.Size(119, 23);
            this.cmbFinancialYearSearch.TabIndex = 166;
            // 
            // pctManual
            // 
            this.pctManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctManual.Image = ((System.Drawing.Image)(resources.GetObject("pctManual.Image")));
            this.pctManual.Location = new System.Drawing.Point(957, 10);
            this.pctManual.Name = "pctManual";
            this.pctManual.Size = new System.Drawing.Size(39, 32);
            this.pctManual.TabIndex = 221;
            this.pctManual.TabStop = false;
            this.pctManual.Tag = "User Manual";
            this.pctManual.Click += new System.EventHandler(this.pctManual_Click);
            // 
            // MstCorrReceiptNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "MstCorrReceiptNo";
            this.Load += new System.EventHandler(this.MstReceiptNo_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).EndInit();
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
        private System.Windows.Forms.TextBox txtScannedImagePathM;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Button btnPreviewImage;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.TextBox txtScannedImagePath;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pctManual;
    }
}
