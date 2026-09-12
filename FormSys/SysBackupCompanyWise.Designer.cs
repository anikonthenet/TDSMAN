namespace TDSMAN.FormSys
{
    partial class SysBackupCompanyWise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysBackupCompanyWise));
            this.grpBackUp = new System.Windows.Forms.GroupBox();
            this.chkReceiptImages = new System.Windows.Forms.CheckBox();
            this.lblPleaseWaitMessage = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.cmbSelectTAN = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.mskDate = new System.Windows.Forms.Label();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrWaitMessage = new System.Windows.Forms.Timer(this.components);
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBackUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 666);
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
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Backup";
            this.BtnSave.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
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
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Text = "&Cancel";
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
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
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBackUp);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpBackUp
            // 
            this.grpBackUp.Controls.Add(this.chkReceiptImages);
            this.grpBackUp.Controls.Add(this.lblPleaseWaitMessage);
            this.grpBackUp.Controls.Add(this.lblCompanyName);
            this.grpBackUp.Controls.Add(this.cmbSelectTAN);
            this.grpBackUp.Controls.Add(this.label1);
            this.grpBackUp.Controls.Add(this.txtFileName);
            this.grpBackUp.Controls.Add(this.txtTime);
            this.grpBackUp.Controls.Add(this.mskDate);
            this.grpBackUp.Controls.Add(this.btnChangePath);
            this.grpBackUp.Controls.Add(this.label7);
            this.grpBackUp.Controls.Add(this.label6);
            this.grpBackUp.Controls.Add(this.label5);
            this.grpBackUp.Controls.Add(this.label4);
            this.grpBackUp.Controls.Add(this.label3);
            this.grpBackUp.Controls.Add(this.label52);
            this.grpBackUp.Controls.Add(this.txtNotes);
            this.grpBackUp.Controls.Add(this.txtPath);
            this.grpBackUp.Controls.Add(this.label2);
            this.grpBackUp.Location = new System.Drawing.Point(175, 119);
            this.grpBackUp.Name = "grpBackUp";
            this.grpBackUp.Size = new System.Drawing.Size(587, 181);
            this.grpBackUp.TabIndex = 47;
            this.grpBackUp.TabStop = false;
            // 
            // chkReceiptImages
            // 
            this.chkReceiptImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReceiptImages.Location = new System.Drawing.Point(461, 49);
            this.chkReceiptImages.Name = "chkReceiptImages";
            this.chkReceiptImages.Size = new System.Drawing.Size(122, 60);
            this.chkReceiptImages.TabIndex = 188;
            this.chkReceiptImages.Text = "Tax Invoice cum Provisional Receipt files";
            this.chkReceiptImages.UseVisualStyleBackColor = true;
            this.chkReceiptImages.CheckedChanged += new System.EventHandler(this.chkReceiptImages_CheckedChanged);
            // 
            // lblPleaseWaitMessage
            // 
            this.lblPleaseWaitMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWaitMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWaitMessage.ForeColor = System.Drawing.Color.Red;
            this.lblPleaseWaitMessage.Location = new System.Drawing.Point(468, 147);
            this.lblPleaseWaitMessage.Name = "lblPleaseWaitMessage";
            this.lblPleaseWaitMessage.Size = new System.Drawing.Size(94, 25);
            this.lblPleaseWaitMessage.TabIndex = 48;
            this.lblPleaseWaitMessage.Text = "Please Wait..";
            this.lblPleaseWaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPleaseWaitMessage.Visible = false;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Blue;
            this.lblCompanyName.Location = new System.Drawing.Point(636, 11);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(13, 18);
            this.lblCompanyName.TabIndex = 187;
            this.lblCompanyName.UseMnemonic = false;
            this.lblCompanyName.Visible = false;
            // 
            // cmbSelectTAN
            // 
            this.cmbSelectTAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectTAN.FormattingEnabled = true;
            this.cmbSelectTAN.Location = new System.Drawing.Point(616, 9);
            this.cmbSelectTAN.Name = "cmbSelectTAN";
            this.cmbSelectTAN.Size = new System.Drawing.Size(14, 21);
            this.cmbSelectTAN.TabIndex = 186;
            this.cmbSelectTAN.Visible = false;
            this.cmbSelectTAN.SelectedIndexChanged += new System.EventHandler(this.cmbSelectTAN_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(591, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 185;
            this.label1.Text = "Select TAN";
            this.label1.Visible = false;
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.Info;
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(97, 70);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(361, 20);
            this.txtFileName.TabIndex = 184;
            this.txtFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.SystemColors.Info;
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(299, 22);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(159, 20);
            this.txtTime.TabIndex = 183;
            this.txtTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTime.Visible = false;
            // 
            // mskDate
            // 
            this.mskDate.BackColor = System.Drawing.SystemColors.Info;
            this.mskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskDate.Location = new System.Drawing.Point(97, 22);
            this.mskDate.Name = "mskDate";
            this.mskDate.Size = new System.Drawing.Size(80, 20);
            this.mskDate.TabIndex = 182;
            this.mskDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnChangePath
            // 
            this.btnChangePath.BackColor = System.Drawing.Color.Lavender;
            this.btnChangePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePath.ForeColor = System.Drawing.Color.Black;
            this.btnChangePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangePath.Location = new System.Drawing.Point(428, 44);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(30, 23);
            this.btnChangePath.TabIndex = 181;
            this.btnChangePath.Text = "...";
            this.btnChangePath.UseVisualStyleBackColor = false;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(461, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 180;
            this.label7.Text = "hh:mm:ss";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(51, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 179;
            this.label6.Text = "Notes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 178;
            this.label5.Text = "File Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 176;
            this.label4.Text = "Output Folder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 174;
            this.label3.Text = "Time";
            this.label3.Visible = false;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(180, 24);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(78, 16);
            this.label52.TabIndex = 173;
            this.label52.Text = "DD/MM/YYYY";
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.Color.White;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(97, 94);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(361, 72);
            this.txtNotes.TabIndex = 4;
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(97, 46);
            this.txtPath.MaxLength = 75;
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(327, 20);
            this.txtPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date";
            // 
            // tmrWaitMessage
            // 
            this.tmrWaitMessage.Interval = 10;
            this.tmrWaitMessage.Tick += new System.EventHandler(this.tmrWaitMessage_Tick);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 209;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(953, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(38, 32);
            this.pctUserManual.TabIndex = 210;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // SysBackupCompanyWise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "SysBackupCompanyWise";
            this.Load += new System.EventHandler(this.SysBackup_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBackUp.ResumeLayout(false);
            this.grpBackUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBackUp;
        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Label mskDate;
        internal System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSelectTAN;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Timer tmrWaitMessage;
        private System.Windows.Forms.Label lblPleaseWaitMessage;
        private System.Windows.Forms.CheckBox chkReceiptImages;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
