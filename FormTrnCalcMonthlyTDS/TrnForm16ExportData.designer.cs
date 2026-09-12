namespace TDSMAN.FormTrnCalcMonthlyTDS
{
    partial class TrnForm16ExportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnForm16ExportData));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.tbpExportData = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblFileFolderName = new System.Windows.Forms.Label();
            this.txtDestinationFileName = new System.Windows.Forms.TextBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.grpExportOption = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbnExcelOption = new System.Windows.Forms.RadioButton();
            this.rbnCSVOption = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalRecordsReturn = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbcExportData = new System.Windows.Forms.TabControl();
            this.lblGap = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtFinancialYear = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.tbpExportData.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpExportOption.SuspendLayout();
            this.tbcExportData.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(851, 8);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(175, 24);
            this.lblSearchMode.TabIndex = 48;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(181, 8);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(670, 24);
            this.pnlTitle.TabIndex = 46;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(0, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(670, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Form16 Data - Export to CSV/Excel";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(7, 8);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 47;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(7, 38);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1019, 3);
            this.pnlHeader.TabIndex = 49;
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnXit);
            this.grpButtons.Controls.Add(this.btnExportToExcel);
            this.grpButtons.Location = new System.Drawing.Point(163, 524);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(709, 47);
            this.grpButtons.TabIndex = 195;
            this.grpButtons.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(18, 12);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 24);
            this.btnXit.TabIndex = 197;
            this.btnXit.Text = "E&xit";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.Color.Lavender;
            this.btnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportToExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExportToExcel.Location = new System.Drawing.Point(557, 12);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(133, 25);
            this.btnExportToExcel.TabIndex = 196;
            this.btnExportToExcel.Text = "&Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // tbpExportData
            // 
            this.tbpExportData.BackColor = System.Drawing.SystemColors.Control;
            this.tbpExportData.Controls.Add(this.groupBox4);
            this.tbpExportData.Controls.Add(this.groupBox2);
            this.tbpExportData.Controls.Add(this.grpExportOption);
            this.tbpExportData.Controls.Add(this.label3);
            this.tbpExportData.Controls.Add(this.lblTotalRecordsReturn);
            this.tbpExportData.Controls.Add(this.panel3);
            this.tbpExportData.Controls.Add(this.pnlLine1);
            this.tbpExportData.Controls.Add(this.panel1);
            this.tbpExportData.Location = new System.Drawing.Point(4, 22);
            this.tbpExportData.Name = "tbpExportData";
            this.tbpExportData.Padding = new System.Windows.Forms.Padding(3);
            this.tbpExportData.Size = new System.Drawing.Size(702, 394);
            this.tbpExportData.TabIndex = 1;
            this.tbpExportData.Text = "tabPage2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblFileFolderName);
            this.groupBox4.Controls.Add(this.txtDestinationFileName);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(4, 60);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(694, 87);
            this.groupBox4.TabIndex = 220;
            this.groupBox4.TabStop = false;
            // 
            // lblFileFolderName
            // 
            this.lblFileFolderName.AutoSize = true;
            this.lblFileFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileFolderName.Location = new System.Drawing.Point(9, 56);
            this.lblFileFolderName.Name = "lblFileFolderName";
            this.lblFileFolderName.Size = new System.Drawing.Size(57, 13);
            this.lblFileFolderName.TabIndex = 148;
            this.lblFileFolderName.Text = "Filename";
            // 
            // txtDestinationFileName
            // 
            this.txtDestinationFileName.Location = new System.Drawing.Point(97, 53);
            this.txtDestinationFileName.Name = "txtDestinationFileName";
            this.txtDestinationFileName.Size = new System.Drawing.Size(371, 20);
            this.txtDestinationFileName.TabIndex = 147;
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(642, 20);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectExcelPath.TabIndex = 146;
            this.btnSelectExcelPath.Text = ". . .";
            this.btnSelectExcelPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelPath.Location = new System.Drawing.Point(97, 21);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(543, 20);
            this.txtExcelPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(10, 24);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(71, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Folder path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.prgBar);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(5, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(692, 63);
            this.groupBox2.TabIndex = 219;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Progress";
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
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(13, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(669, 21);
            this.prgBar.TabIndex = 147;
            // 
            // grpExportOption
            // 
            this.grpExportOption.Controls.Add(this.panel4);
            this.grpExportOption.Controls.Add(this.rbnExcelOption);
            this.grpExportOption.Controls.Add(this.rbnCSVOption);
            this.grpExportOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportOption.Location = new System.Drawing.Point(74, 307);
            this.grpExportOption.Name = "grpExportOption";
            this.grpExportOption.Size = new System.Drawing.Size(554, 65);
            this.grpExportOption.TabIndex = 214;
            this.grpExportOption.TabStop = false;
            this.grpExportOption.Text = "Export Option";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(93, 46);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(368, 2);
            this.panel4.TabIndex = 2;
            // 
            // rbnExcelOption
            // 
            this.rbnExcelOption.AutoSize = true;
            this.rbnExcelOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnExcelOption.Location = new System.Drawing.Point(311, 21);
            this.rbnExcelOption.Name = "rbnExcelOption";
            this.rbnExcelOption.Size = new System.Drawing.Size(96, 17);
            this.rbnExcelOption.TabIndex = 1;
            this.rbnExcelOption.TabStop = true;
            this.rbnExcelOption.Text = "Excel Export";
            this.rbnExcelOption.UseVisualStyleBackColor = true;
            this.rbnExcelOption.CheckedChanged += new System.EventHandler(this.rbnExcelOption_CheckedChanged);
            // 
            // rbnCSVOption
            // 
            this.rbnCSVOption.AutoSize = true;
            this.rbnCSVOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnCSVOption.Location = new System.Drawing.Point(148, 21);
            this.rbnCSVOption.Name = "rbnCSVOption";
            this.rbnCSVOption.Size = new System.Drawing.Size(89, 17);
            this.rbnCSVOption.TabIndex = 0;
            this.rbnCSVOption.TabStop = true;
            this.rbnCSVOption.Text = "CSV Export";
            this.rbnCSVOption.UseVisualStyleBackColor = true;
            this.rbnCSVOption.CheckedChanged += new System.EventHandler(this.rbnCSVOption_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(115, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(472, 23);
            this.label3.TabIndex = 212;
            this.label3.Text = "Data Export to CSV/Excel";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalRecordsReturn
            // 
            this.lblTotalRecordsReturn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecordsReturn.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalRecordsReturn.Location = new System.Drawing.Point(133, 344);
            this.lblTotalRecordsReturn.Name = "lblTotalRecordsReturn";
            this.lblTotalRecordsReturn.Size = new System.Drawing.Size(436, 8);
            this.lblTotalRecordsReturn.TabIndex = 210;
            this.lblTotalRecordsReturn.Text = "Total Records in this return : ";
            this.lblTotalRecordsReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalRecordsReturn.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(3, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(696, 1);
            this.panel3.TabIndex = 197;
            // 
            // pnlLine1
            // 
            this.pnlLine1.BackColor = System.Drawing.Color.Black;
            this.pnlLine1.Location = new System.Drawing.Point(3, 284);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(696, 1);
            this.pnlLine1.TabIndex = 196;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 1);
            this.panel1.TabIndex = 195;
            // 
            // tbcExportData
            // 
            this.tbcExportData.Controls.Add(this.tbpExportData);
            this.tbcExportData.Location = new System.Drawing.Point(164, 98);
            this.tbcExportData.Name = "tbcExportData";
            this.tbcExportData.SelectedIndex = 0;
            this.tbcExportData.Size = new System.Drawing.Size(710, 420);
            this.tbcExportData.TabIndex = 193;
            // 
            // lblGap
            // 
            this.lblGap.Location = new System.Drawing.Point(163, 98);
            this.lblGap.Name = "lblGap";
            this.lblGap.Size = new System.Drawing.Size(710, 21);
            this.lblGap.TabIndex = 196;
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.txtCompany);
            this.grpMain.Controls.Add(this.txtFinancialYear);
            this.grpMain.Controls.Add(this.label4);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.Location = new System.Drawing.Point(20, 45);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(992, 35);
            this.grpMain.TabIndex = 197;
            this.grpMain.TabStop = false;
            // 
            // txtCompany
            // 
            this.txtCompany.BackColor = System.Drawing.SystemColors.Info;
            this.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompany.Location = new System.Drawing.Point(329, 10);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(643, 20);
            this.txtCompany.TabIndex = 21;
            this.txtCompany.TabStop = false;
            // 
            // txtFinancialYear
            // 
            this.txtFinancialYear.BackColor = System.Drawing.SystemColors.Info;
            this.txtFinancialYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFinancialYear.Location = new System.Drawing.Point(129, 10);
            this.txtFinancialYear.Name = "txtFinancialYear";
            this.txtFinancialYear.ReadOnly = true;
            this.txtFinancialYear.Size = new System.Drawing.Size(116, 20);
            this.txtFinancialYear.TabIndex = 19;
            this.txtFinancialYear.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(39, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tax Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(267, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Company";
            // 
            // TrnForm16ExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1038, 682);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.lblGap);
            this.Controls.Add(this.tbcExportData);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.grpButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrnForm16ExportData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.TrnForm16ExportData_Load);
            this.pnlTitle.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            this.tbpExportData.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpExportOption.ResumeLayout(false);
            this.grpExportOption.PerformLayout();
            this.tbcExportData.ResumeLayout(false);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.TabPage tbpExportData;
        private System.Windows.Forms.Label lblTotalRecordsReturn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlLine1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbcExportData;
        private System.Windows.Forms.Label lblGap;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpExportOption;
        private System.Windows.Forms.RadioButton rbnExcelOption;
        private System.Windows.Forms.RadioButton rbnCSVOption;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblFileFolderName;
        private System.Windows.Forms.TextBox txtDestinationFileName;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtFinancialYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}