namespace TDSMAN.FormTrn
{
    partial class TrnConsolidatedStatementImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnConsolidatedStatementImport));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.txtTAN = new System.Windows.Forms.TextBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkTransactionImport = new System.Windows.Forms.CheckBox();
            this.chkMasterImport = new System.Windows.Forms.CheckBox();
            this.btnSelectTDSPath = new System.Windows.Forms.Button();
            this.txtFVUPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tmrLoginRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblHeaderMessage = new System.Windows.Forms.Label();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.lblFooterMessage = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpMain.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
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
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Import";
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
            this.grpButton.Controls.SetChildIndex(this.pctUserManual, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblFooterMessage);
            this.pnlControls.Controls.Add(this.lblHeaderMessage);
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Controls.Add(this.groupBox4);
            this.pnlControls.Controls.Add(this.grpMain);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lnkSearchByTAN);
            this.grpMain.Controls.Add(this.txtTAN);
            this.grpMain.Controls.Add(this.cmbFormNo);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.cmbQuarter);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.cmbFinancialYear);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.cmbCompany);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.ForeColor = System.Drawing.Color.Black;
            this.grpMain.Location = new System.Drawing.Point(146, 87);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(710, 142);
            this.grpMain.TabIndex = 1;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Select";
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(613, 109);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 214;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            this.lnkSearchByTAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lnkSearchByTAN_MouseMove);
            // 
            // txtTAN
            // 
            this.txtTAN.Location = new System.Drawing.Point(537, 127);
            this.txtTAN.Name = "txtTAN";
            this.txtTAN.Size = new System.Drawing.Size(4, 20);
            this.txtTAN.TabIndex = 21;
            this.txtTAN.Visible = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(132, 79);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(135, 21);
            this.cmbFormNo.TabIndex = 19;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select Form No.";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(132, 52);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(135, 21);
            this.cmbQuarter.TabIndex = 1;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(132, 25);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select FA Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(132, 106);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(477, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select Company";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkTransactionImport);
            this.groupBox4.Controls.Add(this.chkMasterImport);
            this.groupBox4.Controls.Add(this.btnSelectTDSPath);
            this.groupBox4.Controls.Add(this.txtFVUPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(146, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(710, 72);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select TDS file";
            // 
            // chkTransactionImport
            // 
            this.chkTransactionImport.AutoSize = true;
            this.chkTransactionImport.Checked = true;
            this.chkTransactionImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTransactionImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTransactionImport.Location = new System.Drawing.Point(243, 45);
            this.chkTransactionImport.Name = "chkTransactionImport";
            this.chkTransactionImport.Size = new System.Drawing.Size(132, 17);
            this.chkTransactionImport.TabIndex = 148;
            this.chkTransactionImport.Text = "Transaction Import";
            this.chkTransactionImport.UseVisualStyleBackColor = true;
            // 
            // chkMasterImport
            // 
            this.chkMasterImport.AutoSize = true;
            this.chkMasterImport.Checked = true;
            this.chkMasterImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMasterImport.Enabled = false;
            this.chkMasterImport.Location = new System.Drawing.Point(134, 45);
            this.chkMasterImport.Name = "chkMasterImport";
            this.chkMasterImport.Size = new System.Drawing.Size(103, 17);
            this.chkMasterImport.TabIndex = 147;
            this.chkMasterImport.Text = "Master Import";
            this.chkMasterImport.UseVisualStyleBackColor = true;
            // 
            // btnSelectTDSPath
            // 
            this.btnSelectTDSPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectTDSPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectTDSPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectTDSPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectTDSPath.Location = new System.Drawing.Point(612, 17);
            this.btnSelectTDSPath.Name = "btnSelectTDSPath";
            this.btnSelectTDSPath.Size = new System.Drawing.Size(43, 24);
            this.btnSelectTDSPath.TabIndex = 146;
            this.btnSelectTDSPath.Text = ". . .";
            this.btnSelectTDSPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectTDSPath.UseVisualStyleBackColor = false;
            this.btnSelectTDSPath.Click += new System.EventHandler(this.btnSelectTDSPath_Click);
            // 
            // txtFVUPath
            // 
            this.txtFVUPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtFVUPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFVUPath.Location = new System.Drawing.Point(132, 20);
            this.txtFVUPath.MaxLength = 75;
            this.txtFVUPath.Name = "txtFVUPath";
            this.txtFVUPath.ReadOnly = true;
            this.txtFVUPath.Size = new System.Drawing.Size(478, 20);
            this.txtFVUPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(46, 23);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(82, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "TDS file path";
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(676, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(146, 309);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 64);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // tmrLoginRefresh
            // 
            this.tmrLoginRefresh.Interval = 5000;
            this.tmrLoginRefresh.Tick += new System.EventHandler(this.tmrLoginRefresh_Tick);
            // 
            // lblHeaderMessage
            // 
            this.lblHeaderMessage.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
            this.lblHeaderMessage.Location = new System.Drawing.Point(54, 19);
            this.lblHeaderMessage.Name = "lblHeaderMessage";
            this.lblHeaderMessage.Size = new System.Drawing.Size(894, 40);
            this.lblHeaderMessage.TabIndex = 71;
            this.lblHeaderMessage.Text = "Consolidated Statement import is under maintenance for the incorporation of new s" +
    "tructures as provided in FVU 3.8.";
            this.lblHeaderMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeaderMessage.Visible = false;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(917, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 206;
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
            this.pctUserManual.Location = new System.Drawing.Point(954, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(38, 32);
            this.pctUserManual.TabIndex = 207;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "Manual Help";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // lblFooterMessage
            // 
            this.lblFooterMessage.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooterMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblFooterMessage.Location = new System.Drawing.Point(54, 388);
            this.lblFooterMessage.Name = "lblFooterMessage";
            this.lblFooterMessage.Size = new System.Drawing.Size(894, 40);
            this.lblFooterMessage.TabIndex = 72;
            this.lblFooterMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrnConsolidatedStatementImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "TrnConsolidatedStatementImport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrnConsolidatedStatementImport_FormClosing);
            this.Load += new System.EventHandler(this.TrnFVUImport_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectTDSPath;
        private System.Windows.Forms.TextBox txtFVUPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.CheckBox chkTransactionImport;
        private System.Windows.Forms.CheckBox chkMasterImport;
        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.TextBox txtTAN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Timer tmrLoginRefresh;
        private System.Windows.Forms.Label lblHeaderMessage;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Label lblFooterMessage;
    }
}
