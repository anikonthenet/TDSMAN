namespace TDSMAN.FormTrn
{
    partial class TrnFVUImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnFVUImport));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.txtCITPIN = new System.Windows.Forms.TextBox();
            this.txtCITCity = new System.Windows.Forms.TextBox();
            this.txtCITAddress = new System.Windows.Forms.TextBox();
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
            this.btnSelectFVUPath = new System.Windows.Forms.Button();
            this.txtFVUPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpMain.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
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
            this.grpMain.Controls.Add(this.txtCITPIN);
            this.grpMain.Controls.Add(this.txtCITCity);
            this.grpMain.Controls.Add(this.txtCITAddress);
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
            this.grpMain.Location = new System.Drawing.Point(168, 72);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(669, 147);
            this.grpMain.TabIndex = 1;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Select";
            // 
            // txtCITPIN
            // 
            this.txtCITPIN.Location = new System.Drawing.Point(325, 79);
            this.txtCITPIN.Name = "txtCITPIN";
            this.txtCITPIN.Size = new System.Drawing.Size(24, 20);
            this.txtCITPIN.TabIndex = 24;
            this.txtCITPIN.Visible = false;
            // 
            // txtCITCity
            // 
            this.txtCITCity.Location = new System.Drawing.Point(295, 79);
            this.txtCITCity.Name = "txtCITCity";
            this.txtCITCity.Size = new System.Drawing.Size(24, 20);
            this.txtCITCity.TabIndex = 23;
            this.txtCITCity.Visible = false;
            // 
            // txtCITAddress
            // 
            this.txtCITAddress.Location = new System.Drawing.Point(265, 79);
            this.txtCITAddress.Name = "txtCITAddress";
            this.txtCITAddress.Size = new System.Drawing.Size(24, 20);
            this.txtCITAddress.TabIndex = 22;
            this.txtCITAddress.Visible = false;
            // 
            // txtTAN
            // 
            this.txtTAN.Location = new System.Drawing.Point(355, 79);
            this.txtTAN.Name = "txtTAN";
            this.txtTAN.Size = new System.Drawing.Size(24, 20);
            this.txtTAN.TabIndex = 21;
            this.txtTAN.Visible = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(168, 79);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(91, 21);
            this.cmbFormNo.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 82);
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
            this.cmbQuarter.Location = new System.Drawing.Point(168, 52);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(49, 21);
            this.cmbQuarter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 55);
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
            this.cmbFinancialYear.Location = new System.Drawing.Point(168, 25);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(168, 106);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(475, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select Company";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkTransactionImport);
            this.groupBox4.Controls.Add(this.chkMasterImport);
            this.groupBox4.Controls.Add(this.btnSelectFVUPath);
            this.groupBox4.Controls.Add(this.txtFVUPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(164, 231);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(676, 111);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select FVU file";
            // 
            // chkTransactionImport
            // 
            this.chkTransactionImport.AutoSize = true;
            this.chkTransactionImport.Checked = true;
            this.chkTransactionImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTransactionImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTransactionImport.Location = new System.Drawing.Point(221, 68);
            this.chkTransactionImport.Name = "chkTransactionImport";
            this.chkTransactionImport.Size = new System.Drawing.Size(132, 17);
            this.chkTransactionImport.TabIndex = 148;
            this.chkTransactionImport.Text = "Transaction Import";
            this.chkTransactionImport.UseVisualStyleBackColor = true;
            this.chkTransactionImport.Visible = false;
            // 
            // chkMasterImport
            // 
            this.chkMasterImport.AutoSize = true;
            this.chkMasterImport.Checked = true;
            this.chkMasterImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMasterImport.Enabled = false;
            this.chkMasterImport.Location = new System.Drawing.Point(112, 68);
            this.chkMasterImport.Name = "chkMasterImport";
            this.chkMasterImport.Size = new System.Drawing.Size(103, 17);
            this.chkMasterImport.TabIndex = 147;
            this.chkMasterImport.Text = "Master Import";
            this.chkMasterImport.UseVisualStyleBackColor = true;
            this.chkMasterImport.Visible = false;
            // 
            // btnSelectFVUPath
            // 
            this.btnSelectFVUPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectFVUPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFVUPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFVUPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectFVUPath.Location = new System.Drawing.Point(607, 28);
            this.btnSelectFVUPath.Name = "btnSelectFVUPath";
            this.btnSelectFVUPath.Size = new System.Drawing.Size(43, 22);
            this.btnSelectFVUPath.TabIndex = 146;
            this.btnSelectFVUPath.Text = ". . .";
            this.btnSelectFVUPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectFVUPath.UseVisualStyleBackColor = false;
            this.btnSelectFVUPath.Click += new System.EventHandler(this.btnSelectFVUPath_Click);
            // 
            // txtFVUPath
            // 
            this.txtFVUPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtFVUPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFVUPath.Location = new System.Drawing.Point(112, 28);
            this.txtFVUPath.MaxLength = 75;
            this.txtFVUPath.Name = "txtFVUPath";
            this.txtFVUPath.ReadOnly = true;
            this.txtFVUPath.Size = new System.Drawing.Size(492, 20);
            this.txtFVUPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(25, 31);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(81, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "FVU file path";
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
            this.groupBox1.Location = new System.Drawing.Point(148, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 64);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // TrnFVUImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrnFVUImport";
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
        private System.Windows.Forms.Button btnSelectFVUPath;
        private System.Windows.Forms.TextBox txtFVUPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.CheckBox chkTransactionImport;
        private System.Windows.Forms.CheckBox chkMasterImport;
        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.TextBox txtTAN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.TextBox txtCITPIN;
        private System.Windows.Forms.TextBox txtCITCity;
        private System.Windows.Forms.TextBox txtCITAddress;
    }
}
