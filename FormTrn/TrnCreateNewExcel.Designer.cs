namespace TDSMAN.FormTrn
{
    partial class TrnCreateNewExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnCreateNewExcel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbnCSV = new System.Windows.Forms.RadioButton();
            this.rbnExcel = new System.Windows.Forms.RadioButton();
            this.lblImportTypeCaption = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.lblFinancialYear = new System.Windows.Forms.Label();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestinationFileName = new System.Windows.Forms.TextBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblImportType = new System.Windows.Forms.Label();
            this.dlgExelFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCreateExcel = new System.Windows.Forms.Button();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpType.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(438, 487);
            this.grpSort.Size = new System.Drawing.Size(189, 104);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(52, 7);
            this.BtnCancel.Size = new System.Drawing.Size(16, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(415, 13);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(113, 483);
            this.grpSearch.Size = new System.Drawing.Size(270, 104);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(23, 7);
            this.BtnEdit.Size = new System.Drawing.Size(23, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(6, 7);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(498, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(108, 7);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(92, 7);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(74, 7);
            this.BtnSearch.Size = new System.Drawing.Size(12, 23);
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
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnCreateExcel);
            this.pnlControls.Controls.Add(this.groupBox1);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 574);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 24);
            this.ViewGrid.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpType);
            this.groupBox1.Controls.Add(this.lblImportTypeCaption);
            this.groupBox1.Controls.Add(this.cmbFinancialYear);
            this.groupBox1.Controls.Add(this.lblFinancialYear);
            this.groupBox1.Controls.Add(this.lblPleaseWait);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cmbFileType);
            this.groupBox1.Controls.Add(this.cmbFormNo);
            this.groupBox1.Controls.Add(this.lblFileType);
            this.groupBox1.Controls.Add(this.lblImportType);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(144, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 291);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.panel3);
            this.grpType.Controls.Add(this.rbnCSV);
            this.grpType.Controls.Add(this.rbnExcel);
            this.grpType.Location = new System.Drawing.Point(86, 12);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(542, 44);
            this.grpType.TabIndex = 73;
            this.grpType.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(71, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(401, 2);
            this.panel3.TabIndex = 181;
            // 
            // rbnCSV
            // 
            this.rbnCSV.AutoSize = true;
            this.rbnCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnCSV.Location = new System.Drawing.Point(286, 13);
            this.rbnCSV.Name = "rbnCSV";
            this.rbnCSV.Size = new System.Drawing.Size(49, 17);
            this.rbnCSV.TabIndex = 180;
            this.rbnCSV.TabStop = true;
            this.rbnCSV.Text = "CSV";
            this.rbnCSV.UseVisualStyleBackColor = true;
            this.rbnCSV.CheckedChanged += new System.EventHandler(this.rbnExcel_CheckedChanged);
            // 
            // rbnExcel
            // 
            this.rbnExcel.AutoSize = true;
            this.rbnExcel.Checked = true;
            this.rbnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnExcel.Location = new System.Drawing.Point(212, 13);
            this.rbnExcel.Name = "rbnExcel";
            this.rbnExcel.Size = new System.Drawing.Size(56, 17);
            this.rbnExcel.TabIndex = 179;
            this.rbnExcel.TabStop = true;
            this.rbnExcel.Text = "Excel";
            this.rbnExcel.UseVisualStyleBackColor = true;
            this.rbnExcel.CheckedChanged += new System.EventHandler(this.rbnExcel_CheckedChanged);
            // 
            // lblImportTypeCaption
            // 
            this.lblImportTypeCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportTypeCaption.ForeColor = System.Drawing.Color.Blue;
            this.lblImportTypeCaption.Location = new System.Drawing.Point(463, 71);
            this.lblImportTypeCaption.Name = "lblImportTypeCaption";
            this.lblImportTypeCaption.Size = new System.Drawing.Size(246, 73);
            this.lblImportTypeCaption.TabIndex = 72;
            this.lblImportTypeCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(217, 69);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(240, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYear_SelectedIndexChanged);
            // 
            // lblFinancialYear
            // 
            this.lblFinancialYear.AutoSize = true;
            this.lblFinancialYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinancialYear.Location = new System.Drawing.Point(97, 72);
            this.lblFinancialYear.Name = "lblFinancialYear";
            this.lblFinancialYear.Size = new System.Drawing.Size(88, 13);
            this.lblFinancialYear.TabIndex = 71;
            this.lblFinancialYear.Text = "Tax Year";
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblPleaseWait.Location = new System.Drawing.Point(599, 264);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(96, 13);
            this.lblPleaseWait.TabIndex = 70;
            this.lblPleaseWait.Text = "Please wait . . .";
            this.lblPleaseWait.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtDestinationFileName);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(19, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(676, 80);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Destination Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Name";
            // 
            // txtDestinationFileName
            // 
            this.txtDestinationFileName.Location = new System.Drawing.Point(125, 49);
            this.txtDestinationFileName.Name = "txtDestinationFileName";
            this.txtDestinationFileName.Size = new System.Drawing.Size(379, 20);
            this.txtDestinationFileName.TabIndex = 1;
            this.txtDestinationFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinationFileName_KeyPress);
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(605, 17);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(43, 25);
            this.btnSelectExcelPath.TabIndex = 0;
            this.btnSelectExcelPath.Text = ". . .";
            this.btnSelectExcelPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelPath.Location = new System.Drawing.Point(125, 20);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(479, 20);
            this.txtExcelPath.TabIndex = 1;
            this.txtExcelPath.TabStop = false;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(85, 23);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(33, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Path";
            // 
            // cmbFileType
            // 
            this.cmbFileType.BackColor = System.Drawing.Color.White;
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Location = new System.Drawing.Point(217, 125);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(240, 21);
            this.cmbFileType.TabIndex = 2;
            this.cmbFileType.SelectedIndexChanged += new System.EventHandler(this.cmbFileType_SelectedIndexChanged);
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(217, 97);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(240, 21);
            this.cmbFormNo.TabIndex = 1;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbFormNo_SelectedIndexChanged);
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileType.Location = new System.Drawing.Point(97, 128);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(99, 13);
            this.lblFileType.TabIndex = 22;
            this.lblFileType.Text = "Select File Type";
            // 
            // lblImportType
            // 
            this.lblImportType.AutoSize = true;
            this.lblImportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportType.Location = new System.Drawing.Point(97, 100);
            this.lblImportType.Name = "lblImportType";
            this.lblImportType.Size = new System.Drawing.Size(114, 13);
            this.lblImportType.TabIndex = 21;
            this.lblImportType.Text = "Select Import Type";
            // 
            // btnCreateExcel
            // 
            this.btnCreateExcel.Location = new System.Drawing.Point(689, 347);
            this.btnCreateExcel.Name = "btnCreateExcel";
            this.btnCreateExcel.Size = new System.Drawing.Size(82, 27);
            this.btnCreateExcel.TabIndex = 1;
            this.btnCreateExcel.Text = "Create Excel";
            this.btnCreateExcel.UseVisualStyleBackColor = true;
            this.btnCreateExcel.Visible = false;
            this.btnCreateExcel.Click += new System.EventHandler(this.btnCreateExcel_Click);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(915, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(82, 32);
            this.pctUserManual.TabIndex = 211;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Video Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnCreateNewExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Name = "TrnCreateNewExcel";
            this.Load += new System.EventHandler(this.TrnExcelImport_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblImportType;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.FolderBrowserDialog dlgExelFolder;
        private System.Windows.Forms.Button btnCreateExcel;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label lblFinancialYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestinationFileName;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Label lblImportTypeCaption;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbnCSV;
        private System.Windows.Forms.RadioButton rbnExcel;
    }
}
