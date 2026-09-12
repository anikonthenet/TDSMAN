namespace TDSMAN.FormTrn
{
    partial class TrnDirectLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDirectLogin));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnView = new System.Windows.Forms.Button();
            this.grpMainBox = new System.Windows.Forms.GroupBox();
            this.grpTraces = new System.Windows.Forms.GroupBox();
            this.txtTracesTANNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTracesUserId = new System.Windows.Forms.TextBox();
            this.txtTracesPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbnIT = new System.Windows.Forms.RadioButton();
            this.rbnTraces = new System.Windows.Forms.RadioButton();
            this.grpIT = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserIDLabel = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.lstUserIdHelpList = new System.Windows.Forms.ListBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpMainBox.SuspendLayout();
            this.grpTraces.SuspendLayout();
            this.grpIT.SuspendLayout();
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
            this.BtnSave.Size = new System.Drawing.Size(10, 25);
            this.BtnSave.Text = "&Submit";
            this.BtnSave.Visible = false;
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
            this.pnlControls.Controls.Add(this.button1);
            this.pnlControls.Controls.Add(this.lstDeducteeHelp);
            this.pnlControls.Controls.Add(this.lstUserIdHelpList);
            this.pnlControls.Controls.Add(this.grpMainBox);
            this.pnlControls.Controls.Add(this.btnView);
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
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(667, 253);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(154, 30);
            this.btnView.TabIndex = 235;
            this.btnView.Text = "Open in Browser";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // grpMainBox
            // 
            this.grpMainBox.Controls.Add(this.grpTraces);
            this.grpMainBox.Controls.Add(this.panel2);
            this.grpMainBox.Controls.Add(this.rbnIT);
            this.grpMainBox.Controls.Add(this.rbnTraces);
            this.grpMainBox.Controls.Add(this.grpIT);
            this.grpMainBox.Location = new System.Drawing.Point(184, 80);
            this.grpMainBox.Name = "grpMainBox";
            this.grpMainBox.Size = new System.Drawing.Size(636, 109);
            this.grpMainBox.TabIndex = 243;
            this.grpMainBox.TabStop = false;
            // 
            // grpTraces
            // 
            this.grpTraces.Controls.Add(this.txtTracesTANNo);
            this.grpTraces.Controls.Add(this.label5);
            this.grpTraces.Controls.Add(this.txtTracesUserId);
            this.grpTraces.Controls.Add(this.txtTracesPassword);
            this.grpTraces.Controls.Add(this.label3);
            this.grpTraces.Controls.Add(this.label4);
            this.grpTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTraces.Location = new System.Drawing.Point(6, 36);
            this.grpTraces.Name = "grpTraces";
            this.grpTraces.Size = new System.Drawing.Size(624, 67);
            this.grpTraces.TabIndex = 248;
            this.grpTraces.TabStop = false;
            // 
            // txtTracesTANNo
            // 
            this.txtTracesTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTracesTANNo.Location = new System.Drawing.Point(74, 18);
            this.txtTracesTANNo.MaxLength = 10;
            this.txtTracesTANNo.Name = "txtTracesTANNo";
            this.txtTracesTANNo.Size = new System.Drawing.Size(96, 20);
            this.txtTracesTANNo.TabIndex = 0;
            this.txtTracesTANNo.TextChanged += new System.EventHandler(this.txtTracesTANNo_TextChanged);
            this.txtTracesTANNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTracesTANNo_KeyDown);
            this.txtTracesTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracesTANNo_KeyPress);
            this.txtTracesTANNo.Leave += new System.EventHandler(this.txtTracesTANNo_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(38, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "TAN";
            // 
            // txtTracesUserId
            // 
            this.txtTracesUserId.Location = new System.Drawing.Point(234, 18);
            this.txtTracesUserId.MaxLength = 50;
            this.txtTracesUserId.Name = "txtTracesUserId";
            this.txtTracesUserId.Size = new System.Drawing.Size(10, 20);
            this.txtTracesUserId.TabIndex = 1;
            this.txtTracesUserId.Visible = false;
            // 
            // txtTracesPassword
            // 
            this.txtTracesPassword.Location = new System.Drawing.Point(284, 18);
            this.txtTracesPassword.MaxLength = 50;
            this.txtTracesPassword.Name = "txtTracesPassword";
            this.txtTracesPassword.Size = new System.Drawing.Size(145, 20);
            this.txtTracesPassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(219, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 208;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(180, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 206;
            this.label4.Text = "User ID";
            this.label4.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(35, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 1);
            this.panel2.TabIndex = 247;
            // 
            // rbnIT
            // 
            this.rbnIT.AutoSize = true;
            this.rbnIT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIT.Location = new System.Drawing.Point(290, 11);
            this.rbnIT.Name = "rbnIT";
            this.rbnIT.Size = new System.Drawing.Size(163, 17);
            this.rbnIT.TabIndex = 246;
            this.rbnIT.Text = "IncomeTax eFiling Login";
            this.rbnIT.UseVisualStyleBackColor = true;
            // 
            // rbnTraces
            // 
            this.rbnTraces.AutoSize = true;
            this.rbnTraces.Checked = true;
            this.rbnTraces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnTraces.Location = new System.Drawing.Point(184, 11);
            this.rbnTraces.Name = "rbnTraces";
            this.rbnTraces.Size = new System.Drawing.Size(95, 17);
            this.rbnTraces.TabIndex = 245;
            this.rbnTraces.TabStop = true;
            this.rbnTraces.Text = "Traces login";
            this.rbnTraces.UseVisualStyleBackColor = true;
            this.rbnTraces.CheckedChanged += new System.EventHandler(this.rbnTraces_CheckedChanged);
            // 
            // grpIT
            // 
            this.grpIT.Controls.Add(this.panel1);
            this.grpIT.Controls.Add(this.label2);
            this.grpIT.Controls.Add(this.label1);
            this.grpIT.Controls.Add(this.lblUserIDLabel);
            this.grpIT.Controls.Add(this.txtPassword);
            this.grpIT.Controls.Add(this.txtUserID);
            this.grpIT.Location = new System.Drawing.Point(6, 30);
            this.grpIT.Name = "grpIT";
            this.grpIT.Size = new System.Drawing.Size(624, 76);
            this.grpIT.TabIndex = 243;
            this.grpIT.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(29, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 2);
            this.panel1.TabIndex = 238;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(109, 43);
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
            this.label1.Location = new System.Drawing.Point(240, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 236;
            this.label1.Text = "Enter password for Incometax account";
            // 
            // lblUserIDLabel
            // 
            this.lblUserIDLabel.AutoSize = true;
            this.lblUserIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserIDLabel.Location = new System.Drawing.Point(41, 23);
            this.lblUserIDLabel.Name = "lblUserIDLabel";
            this.lblUserIDLabel.Size = new System.Drawing.Size(66, 13);
            this.lblUserIDLabel.TabIndex = 235;
            this.lblUserIDLabel.Text = "Enter TAN";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(470, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(114, 20);
            this.txtPassword.TabIndex = 234;
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(112, 19);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(114, 20);
            this.txtUserID.TabIndex = 233;
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyDown);
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserID_KeyPress);
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(264, 155);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(522, 84);
            this.lstDeducteeHelp.TabIndex = 251;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // lstUserIdHelpList
            // 
            this.lstUserIdHelpList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstUserIdHelpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUserIdHelpList.FormattingEnabled = true;
            this.lstUserIdHelpList.Location = new System.Drawing.Point(264, 155);
            this.lstUserIdHelpList.Name = "lstUserIdHelpList";
            this.lstUserIdHelpList.Size = new System.Drawing.Size(385, 95);
            this.lstUserIdHelpList.TabIndex = 250;
            this.lstUserIdHelpList.Visible = false;
            this.lstUserIdHelpList.Click += new System.EventHandler(this.lstUserIdHelpList_Click);
            this.lstUserIdHelpList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstUserIdHelpList_KeyPress);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(956, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 214;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 21);
            this.button1.TabIndex = 252;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TrnDirectLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1016, 666);
            this.Name = "TrnDirectLogin";
            this.Load += new System.EventHandler(this.TrnPanVarification_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpMainBox.ResumeLayout(false);
            this.grpMainBox.PerformLayout();
            this.grpTraces.ResumeLayout(false);
            this.grpTraces.PerformLayout();
            this.grpIT.ResumeLayout(false);
            this.grpIT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.GroupBox grpMainBox;
        private System.Windows.Forms.GroupBox grpIT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserIDLabel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.RadioButton rbnIT;
        private System.Windows.Forms.RadioButton rbnTraces;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpTraces;
        private System.Windows.Forms.TextBox txtTracesTANNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTracesUserId;
        private System.Windows.Forms.TextBox txtTracesPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.ListBox lstUserIdHelpList;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Button button1;
    }
}
