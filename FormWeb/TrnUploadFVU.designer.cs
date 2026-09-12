namespace TDSMAN.FormTrn
{
    partial class TrnUploadFVU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnUploadFVU));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpLogIn = new System.Windows.Forms.GroupBox();
            this.lstUserIdHelpList = new System.Windows.Forms.ListBox();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpReceiptNo = new System.Windows.Forms.GroupBox();
            this.txtPreviousRRR = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOriginalRRR = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbUploadType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbForm = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserIDLabel = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpLogIn.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.grpReceiptNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 665);
            this.grpSort.Size = new System.Drawing.Size(280, 5);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(39, 13);
            this.BtnCancel.Size = new System.Drawing.Size(10, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(390, 13);
            this.BtnSave.Size = new System.Drawing.Size(18, 25);
            this.BtnSave.Text = "&Submit";
            this.BtnSave.Visible = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 657);
            this.grpSearch.Size = new System.Drawing.Size(280, 5);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(23, 13);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(6, 13);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(467, 13);
            this.BtnExit.Size = new System.Drawing.Size(68, 25);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(86, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(70, 13);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(54, 13);
            this.BtnSearch.Size = new System.Drawing.Size(10, 23);
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Location = new System.Drawing.Point(9, 599);
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
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnGo);
            this.pnlControls.Controls.Add(this.grpLogIn);
            this.pnlControls.Controls.Add(this.pBar);
            this.pnlControls.Size = new System.Drawing.Size(1004, 609);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(131, 516);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 227;
            this.pBar.Visible = false;
            // 
            // grpLogIn
            // 
            this.grpLogIn.Controls.Add(this.lstUserIdHelpList);
            this.grpLogIn.Controls.Add(this.grpMain);
            this.grpLogIn.Controls.Add(this.panel1);
            this.grpLogIn.Controls.Add(this.label2);
            this.grpLogIn.Controls.Add(this.label1);
            this.grpLogIn.Controls.Add(this.lblUserIDLabel);
            this.grpLogIn.Controls.Add(this.txtPassword);
            this.grpLogIn.Controls.Add(this.txtUserID);
            this.grpLogIn.Location = new System.Drawing.Point(187, 83);
            this.grpLogIn.Name = "grpLogIn";
            this.grpLogIn.Size = new System.Drawing.Size(624, 243);
            this.grpLogIn.TabIndex = 233;
            this.grpLogIn.TabStop = false;
            // 
            // lstUserIdHelpList
            // 
            this.lstUserIdHelpList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstUserIdHelpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUserIdHelpList.FormattingEnabled = true;
            this.lstUserIdHelpList.Location = new System.Drawing.Point(9, 42);
            this.lstUserIdHelpList.Name = "lstUserIdHelpList";
            this.lstUserIdHelpList.Size = new System.Drawing.Size(606, 108);
            this.lstUserIdHelpList.TabIndex = 240;
            this.lstUserIdHelpList.Visible = false;
            this.lstUserIdHelpList.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstUserIdHelpList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.grpReceiptNo);
            this.grpMain.Controls.Add(this.cmbUploadType);
            this.grpMain.Controls.Add(this.label7);
            this.grpMain.Controls.Add(this.panel2);
            this.grpMain.Controls.Add(this.cmbForm);
            this.grpMain.Controls.Add(this.label6);
            this.grpMain.Controls.Add(this.cmbQuarter);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.cmbFinancialYear);
            this.grpMain.Controls.Add(this.label4);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.Location = new System.Drawing.Point(6, 70);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(612, 168);
            this.grpMain.TabIndex = 239;
            this.grpMain.TabStop = false;
            // 
            // grpReceiptNo
            // 
            this.grpReceiptNo.Controls.Add(this.txtPreviousRRR);
            this.grpReceiptNo.Controls.Add(this.label9);
            this.grpReceiptNo.Controls.Add(this.txtOriginalRRR);
            this.grpReceiptNo.Controls.Add(this.label8);
            this.grpReceiptNo.Location = new System.Drawing.Point(221, 100);
            this.grpReceiptNo.Name = "grpReceiptNo";
            this.grpReceiptNo.Size = new System.Drawing.Size(379, 63);
            this.grpReceiptNo.TabIndex = 242;
            this.grpReceiptNo.TabStop = false;
            // 
            // txtPreviousRRR
            // 
            this.txtPreviousRRR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviousRRR.Location = new System.Drawing.Point(120, 36);
            this.txtPreviousRRR.Name = "txtPreviousRRR";
            this.txtPreviousRRR.Size = new System.Drawing.Size(241, 20);
            this.txtPreviousRRR.TabIndex = 237;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(6, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 236;
            this.label9.Text = "Previous RRR No.";
            // 
            // txtOriginalRRR
            // 
            this.txtOriginalRRR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginalRRR.Location = new System.Drawing.Point(120, 13);
            this.txtOriginalRRR.Name = "txtOriginalRRR";
            this.txtOriginalRRR.Size = new System.Drawing.Size(241, 20);
            this.txtOriginalRRR.TabIndex = 235;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Original RRR No.";
            // 
            // cmbUploadType
            // 
            this.cmbUploadType.BackColor = System.Drawing.Color.White;
            this.cmbUploadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUploadType.ForeColor = System.Drawing.Color.Black;
            this.cmbUploadType.FormattingEnabled = true;
            this.cmbUploadType.Location = new System.Drawing.Point(96, 123);
            this.cmbUploadType.Name = "cmbUploadType";
            this.cmbUploadType.Size = new System.Drawing.Size(119, 21);
            this.cmbUploadType.TabIndex = 240;
            this.cmbUploadType.SelectedIndexChanged += new System.EventHandler(this.CmbUploadType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 241;
            this.label7.Text = "Upload Type";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(33, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(546, 2);
            this.panel2.TabIndex = 239;
            // 
            // cmbForm
            // 
            this.cmbForm.BackColor = System.Drawing.Color.White;
            this.cmbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForm.ForeColor = System.Drawing.Color.Black;
            this.cmbForm.FormattingEnabled = true;
            this.cmbForm.Location = new System.Drawing.Point(166, 38);
            this.cmbForm.Name = "cmbForm";
            this.cmbForm.Size = new System.Drawing.Size(226, 21);
            this.cmbForm.TabIndex = 19;
            this.cmbForm.SelectedIndexChanged += new System.EventHandler(this.CmbForm_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(86, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Select Form";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.ForeColor = System.Drawing.Color.Black;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(166, 64);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(76, 21);
            this.cmbQuarter.TabIndex = 1;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.CmbQuarter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(71, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(166, 12);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(32, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Select Tax Year";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(39, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 2);
            this.panel1.TabIndex = 238;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(131, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 237;
            this.label2.Text = "PAN/ Aadhaar/ Other User ID";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 236;
            this.label1.Text = "Enter password for Incometax account";
            // 
            // lblUserIDLabel
            // 
            this.lblUserIDLabel.AutoSize = true;
            this.lblUserIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserIDLabel.Location = new System.Drawing.Point(63, 23);
            this.lblUserIDLabel.Name = "lblUserIDLabel";
            this.lblUserIDLabel.Size = new System.Drawing.Size(66, 13);
            this.lblUserIDLabel.TabIndex = 235;
            this.lblUserIDLabel.Text = "Enter TAN";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(492, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(114, 20);
            this.txtPassword.TabIndex = 234;
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(134, 19);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(114, 20);
            this.txtUserID.TabIndex = 233;
            this.txtUserID.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtUserID.Leave += new System.EventHandler(this.txtTAN_Leave);
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(721, 335);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(90, 30);
            this.btnGo.TabIndex = 234;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(957, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 228;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnUploadFVU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1089, 575);
            this.Name = "TrnUploadFVU";
            this.Load += new System.EventHandler(this.TrnPanVarification_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpLogIn.ResumeLayout(false);
            this.grpLogIn.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpReceiptNo.ResumeLayout(false);
            this.grpReceiptNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox grpLogIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserIDLabel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbForm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbUploadType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grpReceiptNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOriginalRRR;
        private System.Windows.Forms.TextBox txtPreviousRRR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox lstUserIdHelpList;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
