namespace TDSMAN.FormTrn
{
    partial class TrnCompanyMasterExcelImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnCompanyMasterExcelImport));
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.tbcExcelImport = new System.Windows.Forms.TabControl();
            this.tbpValidateExcelFile = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProgressDisplayMessage = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkColorCodingExcelsheet = new System.Windows.Forms.CheckBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tbpImportExcelFile = new System.Windows.Forms.TabPage();
            this.lblNewRecords = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.prgImportBar = new System.Windows.Forms.ProgressBar();
            this.dgcViewChallan = new DGControl.DGControl();
            this.lblHide = new System.Windows.Forms.Label();
            this.tmrLoginRefresh = new System.Windows.Forms.Timer(this.components);
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.tbcExcelImport.SuspendLayout();
            this.tbpValidateExcelFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.tbpImportExcelFile.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 658);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(925, 15);
            this.BtnCancel.Size = new System.Drawing.Size(7, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(354, 13);
            this.BtnSave.Size = new System.Drawing.Size(147, 23);
            this.BtnSave.Text = "&Validate Excel file";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(920, 15);
            this.BtnEdit.Size = new System.Drawing.Size(5, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(915, 15);
            this.BtnAdd.Size = new System.Drawing.Size(5, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(946, 15);
            this.BtnRefresh.Size = new System.Drawing.Size(11, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(939, 15);
            this.BtnDelete.Size = new System.Drawing.Size(7, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(932, 15);
            this.BtnSearch.Size = new System.Drawing.Size(7, 23);
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
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tbcExcelImport);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // tbcExcelImport
            // 
            this.tbcExcelImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcExcelImport.Controls.Add(this.tbpValidateExcelFile);
            this.tbcExcelImport.Controls.Add(this.tbpImportExcelFile);
            this.tbcExcelImport.Location = new System.Drawing.Point(10, 6);
            this.tbcExcelImport.Name = "tbcExcelImport";
            this.tbcExcelImport.SelectedIndex = 0;
            this.tbcExcelImport.Size = new System.Drawing.Size(990, 534);
            this.tbcExcelImport.TabIndex = 0;
            this.tbcExcelImport.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcExcelImport_Selecting);
            // 
            // tbpValidateExcelFile
            // 
            this.tbpValidateExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpValidateExcelFile.Controls.Add(this.groupBox1);
            this.tbpValidateExcelFile.Controls.Add(this.groupBox4);
            this.tbpValidateExcelFile.Controls.Add(this.grpMain);
            this.tbpValidateExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpValidateExcelFile.Name = "tbpValidateExcelFile";
            this.tbpValidateExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpValidateExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpValidateExcelFile.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProgressDisplayMessage);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(154, 350);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 64);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblProgressDisplayMessage
            // 
            this.lblProgressDisplayMessage.AutoSize = true;
            this.lblProgressDisplayMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressDisplayMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblProgressDisplayMessage.Location = new System.Drawing.Point(19, 47);
            this.lblProgressDisplayMessage.Name = "lblProgressDisplayMessage";
            this.lblProgressDisplayMessage.Size = new System.Drawing.Size(88, 13);
            this.lblProgressDisplayMessage.TabIndex = 148;
            this.lblProgressDisplayMessage.Text = "Excel file path";
            this.lblProgressDisplayMessage.Visible = false;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(647, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkUpdate);
            this.groupBox4.Controls.Add(this.chkColorCodingExcelsheet);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(154, 221);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(676, 120);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Excel file";
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkUpdate.Location = new System.Drawing.Point(112, 57);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(290, 17);
            this.chkUpdate.TabIndex = 149;
            this.chkUpdate.Text = "Update Name && Address of existing Deductees";
            this.chkUpdate.UseVisualStyleBackColor = true;
            this.chkUpdate.Visible = false;
            // 
            // chkColorCodingExcelsheet
            // 
            this.chkColorCodingExcelsheet.AutoSize = true;
            this.chkColorCodingExcelsheet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkColorCodingExcelsheet.Location = new System.Drawing.Point(112, 82);
            this.chkColorCodingExcelsheet.Name = "chkColorCodingExcelsheet";
            this.chkColorCodingExcelsheet.Size = new System.Drawing.Size(436, 17);
            this.chkColorCodingExcelsheet.TabIndex = 148;
            this.chkColorCodingExcelsheet.Text = "Color coding in Excel sheet for errors (this may take a few more minutes)";
            this.chkColorCodingExcelsheet.UseVisualStyleBackColor = true;
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(607, 28);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(43, 22);
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
            this.txtExcelPath.Location = new System.Drawing.Point(112, 28);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(492, 20);
            this.txtExcelPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(21, 31);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(88, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Excel file path";
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblMessage2);
            this.grpMain.Controls.Add(this.lblMessage1);
            this.grpMain.Controls.Add(this.lblMessage);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.ForeColor = System.Drawing.Color.Black;
            this.grpMain.Location = new System.Drawing.Point(154, 99);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(676, 116);
            this.grpMain.TabIndex = 67;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Notes";
            // 
            // lblMessage2
            // 
            this.lblMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.ForeColor = System.Drawing.Color.Black;
            this.lblMessage2.Location = new System.Drawing.Point(28, 75);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(622, 19);
            this.lblMessage2.TabIndex = 2;
            // 
            // lblMessage1
            // 
            this.lblMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage1.ForeColor = System.Drawing.Color.Black;
            this.lblMessage1.Location = new System.Drawing.Point(28, 51);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(622, 18);
            this.lblMessage1.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblMessage.Location = new System.Drawing.Point(28, 26);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(622, 18);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.UseMnemonic = false;
            // 
            // tbpImportExcelFile
            // 
            this.tbpImportExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpImportExcelFile.Controls.Add(this.lblNewRecords);
            this.tbpImportExcelFile.Controls.Add(this.label11);
            this.tbpImportExcelFile.Controls.Add(this.groupBox3);
            this.tbpImportExcelFile.Controls.Add(this.dgcViewChallan);
            this.tbpImportExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpImportExcelFile.Name = "tbpImportExcelFile";
            this.tbpImportExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpImportExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpImportExcelFile.TabIndex = 1;
            // 
            // lblNewRecords
            // 
            this.lblNewRecords.BackColor = System.Drawing.SystemColors.Info;
            this.lblNewRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNewRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewRecords.Location = new System.Drawing.Point(170, 13);
            this.lblNewRecords.Name = "lblNewRecords";
            this.lblNewRecords.Size = new System.Drawing.Size(81, 20);
            this.lblNewRecords.TabIndex = 198;
            this.lblNewRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 13);
            this.label11.TabIndex = 197;
            this.label11.Text = "New Records to be added";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.prgImportBar);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(6, 460);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(969, 45);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // prgImportBar
            // 
            this.prgImportBar.Location = new System.Drawing.Point(16, 16);
            this.prgImportBar.Name = "prgImportBar";
            this.prgImportBar.Size = new System.Drawing.Size(940, 21);
            this.prgImportBar.TabIndex = 147;
            // 
            // dgcViewChallan
            // 
            this.dgcViewChallan.AlternatingBackColor = System.Drawing.Color.White;
            this.dgcViewChallan.BackColor = System.Drawing.Color.White;
            this.dgcViewChallan.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgcViewChallan.CaptionBackColor = System.Drawing.Color.Honeydew;
            this.dgcViewChallan.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewChallan.CaptionForeColor = System.Drawing.Color.Black;
            this.dgcViewChallan.CaptionText = "                                                                                 " +
    "                                                              Company Master";
            this.dgcViewChallan.DataMember = "";
            this.dgcViewChallan.FlatMode = true;
            this.dgcViewChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewChallan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgcViewChallan.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgcViewChallan.Location = new System.Drawing.Point(6, 47);
            this.dgcViewChallan.Name = "dgcViewChallan";
            this.dgcViewChallan.ReadOnly = true;
            this.dgcViewChallan.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dgcViewChallan.Size = new System.Drawing.Size(970, 407);
            this.dgcViewChallan.TabIndex = 76;
            this.dgcViewChallan.Click += new System.EventHandler(this.dgcViewChallan_Click);
            this.dgcViewChallan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgcViewChallan_MouseClick);
            this.dgcViewChallan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgcViewChallan_MouseClick);
            // 
            // lblHide
            // 
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(20, 48);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(164, 21);
            this.lblHide.TabIndex = 50;
            // 
            // tmrLoginRefresh
            // 
            this.tmrLoginRefresh.Interval = 5000;
            this.tmrLoginRefresh.Tick += new System.EventHandler(this.tmrLoginRefresh_Tick);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(951, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 210;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnCompanyMasterExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.lblHide);
            this.Name = "TrnCompanyMasterExcelImport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrnDeducteeMasterExcelImport_FormClosing);
            this.Load += new System.EventHandler(this.TrnExcelImport_Load);
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
            this.Controls.SetChildIndex(this.lblHide, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.tbcExcelImport.ResumeLayout(false);
            this.tbpValidateExcelFile.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.tbpImportExcelFile.ResumeLayout(false);
            this.tbpImportExcelFile.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.TabControl tbcExcelImport;
        private System.Windows.Forms.TabPage tbpValidateExcelFile;
        private System.Windows.Forms.TabPage tbpImportExcelFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkColorCodingExcelsheet;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label lblHide;
        private DGControl.DGControl dgcViewChallan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar prgImportBar;
        private System.Windows.Forms.Label lblProgressDisplayMessage;
        private System.Windows.Forms.Label lblNewRecords;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Timer tmrLoginRefresh;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
