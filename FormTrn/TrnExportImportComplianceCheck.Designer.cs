namespace TDSMAN.FormTrn
{
    partial class TrnExportImportComplianceCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnExportImportComplianceCheck));
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkReportingPortal = new System.Windows.Forms.LinkLabel();
            this.lblTitleMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.prgExportBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbSelectCompanyName = new System.Windows.Forms.ComboBox();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelFolderPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.prgImportBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSelectExcelFile = new System.Windows.Forms.Button();
            this.txtExcelFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpImport.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(23, 91);
            this.grpSort.Size = new System.Drawing.Size(12, 10);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Visible = false;
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 648);
            this.grpSearch.Size = new System.Drawing.Size(280, 4);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(164, 13);
            this.BtnAdd.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Export / Import - Compliance Check";
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
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
            // lblMode
            // 
            this.lblMode.Text = "Excel Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpImport);
            this.pnlControls.Controls.Add(this.grpExport);
            this.pnlControls.Controls.Add(this.panel1);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 3);
            this.panel1.TabIndex = 0;
            // 
            // grpExport
            // 
            this.grpExport.Controls.Add(this.label3);
            this.grpExport.Controls.Add(this.lnkReportingPortal);
            this.grpExport.Controls.Add(this.lblTitleMessage);
            this.grpExport.Controls.Add(this.groupBox1);
            this.grpExport.Controls.Add(this.groupBox4);
            this.grpExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExport.ForeColor = System.Drawing.Color.Blue;
            this.grpExport.Location = new System.Drawing.Point(8, 6);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(987, 304);
            this.grpExport.TabIndex = 1;
            this.grpExport.TabStop = false;
            this.grpExport.Text = "Check PAN(s) with IT Dept. for higher TDS/TCS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 15);
            this.label3.TabIndex = 74;
            this.label3.Text = "After CSV is ready, follow the steps below : ";
            // 
            // lnkReportingPortal
            // 
            this.lnkReportingPortal.AutoSize = true;
            this.lnkReportingPortal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkReportingPortal.Location = new System.Drawing.Point(236, 172);
            this.lnkReportingPortal.Name = "lnkReportingPortal";
            this.lnkReportingPortal.Size = new System.Drawing.Size(149, 15);
            this.lnkReportingPortal.TabIndex = 73;
            this.lnkReportingPortal.TabStop = true;
            this.lnkReportingPortal.Text = "https://report.insight.gov.in";
            this.lnkReportingPortal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReportingPortal_LinkClicked);
            // 
            // lblTitleMessage
            // 
            this.lblTitleMessage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleMessage.Location = new System.Drawing.Point(29, 174);
            this.lblTitleMessage.Name = "lblTitleMessage";
            this.lblTitleMessage.Size = new System.Drawing.Size(928, 127);
            this.lblTitleMessage.TabIndex = 72;
            this.lblTitleMessage.Text = resources.GetString("lblTitleMessage.Text");
            this.lblTitleMessage.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgExportBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(140, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 30);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // lblPrcnt
            // 
            this.lblPrcnt.AutoSize = true;
            this.lblPrcnt.BackColor = System.Drawing.Color.Transparent;
            this.lblPrcnt.Location = new System.Drawing.Point(356, 27);
            this.lblPrcnt.Name = "lblPrcnt";
            this.lblPrcnt.Size = new System.Drawing.Size(0, 13);
            this.lblPrcnt.TabIndex = 149;
            this.lblPrcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgExportBar
            // 
            this.prgExportBar.Location = new System.Drawing.Point(18, 13);
            this.prgExportBar.Name = "prgExportBar";
            this.prgExportBar.Size = new System.Drawing.Size(672, 9);
            this.prgExportBar.TabIndex = 147;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lnkSearchByTAN);
            this.groupBox4.Controls.Add(this.cmbSelectCompanyName);
            this.groupBox4.Controls.Add(this.btnExportToExcel);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelFolderPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(140, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(707, 80);
            this.groupBox4.TabIndex = 70;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Export list of PAN in CSV format";
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(583, 24);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 152;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbSelectCompanyName
            // 
            this.cmbSelectCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectCompanyName.FormattingEnabled = true;
            this.cmbSelectCompanyName.Location = new System.Drawing.Point(97, 21);
            this.cmbSelectCompanyName.Name = "cmbSelectCompanyName";
            this.cmbSelectCompanyName.Size = new System.Drawing.Size(480, 21);
            this.cmbSelectCompanyName.TabIndex = 150;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportToExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.Location = new System.Drawing.Point(623, 48);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(81, 26);
            this.btnExportToExcel.TabIndex = 149;
            this.btnExportToExcel.Text = "&Create CSV";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(578, 49);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectExcelPath.TabIndex = 146;
            this.btnSelectExcelPath.Text = ". . .";
            this.btnSelectExcelPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelFolderPath
            // 
            this.txtExcelFolderPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFolderPath.Location = new System.Drawing.Point(97, 50);
            this.txtExcelFolderPath.MaxLength = 75;
            this.txtExcelFolderPath.Name = "txtExcelFolderPath";
            this.txtExcelFolderPath.ReadOnly = true;
            this.txtExcelFolderPath.Size = new System.Drawing.Size(480, 20);
            this.txtExcelFolderPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(10, 53);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(71, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Folder path";
            // 
            // grpImport
            // 
            this.grpImport.Controls.Add(this.label5);
            this.grpImport.Controls.Add(this.label4);
            this.grpImport.Controls.Add(this.groupBox3);
            this.grpImport.Controls.Add(this.groupBox2);
            this.grpImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImport.ForeColor = System.Drawing.Color.Blue;
            this.grpImport.Location = new System.Drawing.Point(8, 335);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(987, 214);
            this.grpImport.TabIndex = 2;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "Upload PAN(s) for higher TDS/TCS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(75, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 15);
            this.label5.TabIndex = 75;
            this.label5.Text = "Upload the file :";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(928, 31);
            this.label4.TabIndex = 73;
            this.label4.Text = "The downloaded file will provide you a list for which higher TDS/TCS is applicabl" +
    "e.";
            this.label4.UseMnemonic = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.prgImportBar);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(140, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(707, 37);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(356, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 149;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgImportBar
            // 
            this.prgImportBar.Location = new System.Drawing.Point(16, 17);
            this.prgImportBar.Name = "prgImportBar";
            this.prgImportBar.Size = new System.Drawing.Size(676, 12);
            this.prgImportBar.TabIndex = 147;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnSelectExcelFile);
            this.groupBox2.Controls.Add(this.txtExcelFilePath);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(140, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(707, 47);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Excel file";
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnImport.Location = new System.Drawing.Point(636, 14);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(62, 26);
            this.btnImport.TabIndex = 150;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSelectExcelFile
            // 
            this.btnSelectExcelFile.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelFile.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelFile.Location = new System.Drawing.Point(587, 17);
            this.btnSelectExcelFile.Name = "btnSelectExcelFile";
            this.btnSelectExcelFile.Size = new System.Drawing.Size(43, 22);
            this.btnSelectExcelFile.TabIndex = 146;
            this.btnSelectExcelFile.Text = ". . .";
            this.btnSelectExcelFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelFile.UseVisualStyleBackColor = false;
            this.btnSelectExcelFile.Click += new System.EventHandler(this.btnSelectExcelFile_Click);
            // 
            // txtExcelFilePath
            // 
            this.txtExcelFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFilePath.Location = new System.Drawing.Point(97, 18);
            this.txtExcelFilePath.MaxLength = 75;
            this.txtExcelFilePath.Name = "txtExcelFilePath";
            this.txtExcelFilePath.ReadOnly = true;
            this.txtExcelFilePath.Size = new System.Drawing.Size(488, 20);
            this.txtExcelFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Excel file path";
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(956, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 229;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnExportImportComplianceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Name = "TrnExportImportComplianceCheck";
            this.Activated += new System.EventHandler(this.TrnExportImportComplianceCheck_Activated);
            this.Load += new System.EventHandler(this.TrnExportImportComplianceCheck_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpExport.ResumeLayout(false);
            this.grpExport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpImport.ResumeLayout(false);
            this.grpImport.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.GroupBox grpImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPrcnt;
        private System.Windows.Forms.ProgressBar prgExportBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelFolderPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar prgImportBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectExcelFile;
        private System.Windows.Forms.TextBox txtExcelFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ComboBox cmbSelectCompanyName;
        private System.Windows.Forms.Label lblTitleMessage;
        private System.Windows.Forms.LinkLabel lnkReportingPortal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
