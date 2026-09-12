namespace TDSMAN.FormTrn
{
    partial class TrnBulkPANVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnBulkPANVerification));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dgvDeductees = new DGVControl.DGVControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnVerification = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bgwPANVerification = new System.ComponentModel.BackgroundWorker();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnPrintInvalidPAN = new System.Windows.Forms.Button();
            this.lblNotVerifiedNo = new System.Windows.Forms.Label();
            this.lblNotVerified = new System.Windows.Forms.Label();
            this.lblInvalidNo = new System.Windows.Forms.Label();
            this.lblInvalid = new System.Windows.Forms.Label();
            this.lblVerifiedNo = new System.Windows.Forms.Label();
            this.lblVerified = new System.Windows.Forms.Label();
            this.grpDisclaimer = new System.Windows.Forms.GroupBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpDisclaimer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(553, 8);
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
            this.pnlTitle.Size = new System.Drawing.Size(382, 24);
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
            this.lblTitle.Size = new System.Drawing.Size(371, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Bulk PAN Verification";
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
            this.pnlHeader.Size = new System.Drawing.Size(721, 3);
            this.pnlHeader.TabIndex = 49;
            // 
            // dgvDeductees
            // 
            this.dgvDeductees.AllowUserToAddRows = false;
            this.dgvDeductees.AllowUserToDeleteRows = false;
            this.dgvDeductees.AllowUserToOrderColumns = true;
            this.dgvDeductees.AllowUserToResizeRows = false;
            this.dgvDeductees.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvDeductees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeductees.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDeductees.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDeductees.Location = new System.Drawing.Point(12, 44);
            this.dgvDeductees.MultiSelect = false;
            this.dgvDeductees.Name = "dgvDeductees";
            this.dgvDeductees.RowHeadersWidth = 20;
            this.dgvDeductees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDeductees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeductees.Size = new System.Drawing.Size(710, 463);
            this.dgvDeductees.TabIndex = 185;
            this.dgvDeductees.TabStop = false;
            this.dgvDeductees.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDeductees_ColumnAdded);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnXit);
            this.groupBox3.Controls.Add(this.btnVerification);
            this.groupBox3.Location = new System.Drawing.Point(497, 514);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 38);
            this.groupBox3.TabIndex = 187;
            this.groupBox3.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(134, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "&Close";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnVerification
            // 
            this.btnVerification.BackColor = System.Drawing.Color.Lavender;
            this.btnVerification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerification.ForeColor = System.Drawing.Color.Black;
            this.btnVerification.Location = new System.Drawing.Point(12, 11);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(121, 23);
            this.btnVerification.TabIndex = 189;
            this.btnVerification.Text = "&Start Verifying";
            this.btnVerification.UseVisualStyleBackColor = false;
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(13, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 1);
            this.panel1.TabIndex = 188;
            // 
            // bgwPANVerification
            // 
            this.bgwPANVerification.WorkerSupportsCancellation = true;
            this.bgwPANVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPANVerification_DoWork);
            this.bgwPANVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwPANVerification_RunWorkerCompleted);
            this.bgwPANVerification.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwPANVerification_ProgressChanged);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.btnPrintInvalidPAN);
            this.grpStatus.Controls.Add(this.lblNotVerifiedNo);
            this.grpStatus.Controls.Add(this.lblNotVerified);
            this.grpStatus.Controls.Add(this.lblInvalidNo);
            this.grpStatus.Controls.Add(this.lblInvalid);
            this.grpStatus.Controls.Add(this.lblVerifiedNo);
            this.grpStatus.Controls.Add(this.lblVerified);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.ForeColor = System.Drawing.Color.Black;
            this.grpStatus.Location = new System.Drawing.Point(15, 515);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(466, 38);
            this.grpStatus.TabIndex = 189;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Total Records";
            // 
            // btnPrintInvalidPAN
            // 
            this.btnPrintInvalidPAN.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintInvalidPAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintInvalidPAN.Enabled = false;
            this.btnPrintInvalidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintInvalidPAN.ForeColor = System.Drawing.Color.Black;
            this.btnPrintInvalidPAN.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintInvalidPAN.Image")));
            this.btnPrintInvalidPAN.Location = new System.Drawing.Point(288, 9);
            this.btnPrintInvalidPAN.Name = "btnPrintInvalidPAN";
            this.btnPrintInvalidPAN.Size = new System.Drawing.Size(32, 27);
            this.btnPrintInvalidPAN.TabIndex = 191;
            this.btnPrintInvalidPAN.UseVisualStyleBackColor = false;
            this.btnPrintInvalidPAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPrintInvalidPAN_MouseMove);
            this.btnPrintInvalidPAN.Click += new System.EventHandler(this.btnPrintInvalidPAN_Click);
            // 
            // lblNotVerifiedNo
            // 
            this.lblNotVerifiedNo.BackColor = System.Drawing.Color.Silver;
            this.lblNotVerifiedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotVerifiedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerifiedNo.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerifiedNo.Location = new System.Drawing.Point(387, 12);
            this.lblNotVerifiedNo.Name = "lblNotVerifiedNo";
            this.lblNotVerifiedNo.Size = new System.Drawing.Size(50, 21);
            this.lblNotVerifiedNo.TabIndex = 22;
            this.lblNotVerifiedNo.Text = "0";
            this.lblNotVerifiedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNotVerified
            // 
            this.lblNotVerified.AutoSize = true;
            this.lblNotVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotVerified.ForeColor = System.Drawing.Color.Black;
            this.lblNotVerified.Location = new System.Drawing.Point(321, 15);
            this.lblNotVerified.Name = "lblNotVerified";
            this.lblNotVerified.Size = new System.Drawing.Size(62, 13);
            this.lblNotVerified.TabIndex = 21;
            this.lblNotVerified.Text = "Not Verified";
            // 
            // lblInvalidNo
            // 
            this.lblInvalidNo.BackColor = System.Drawing.Color.Red;
            this.lblInvalidNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvalidNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidNo.ForeColor = System.Drawing.Color.Black;
            this.lblInvalidNo.Location = new System.Drawing.Point(237, 11);
            this.lblInvalidNo.Name = "lblInvalidNo";
            this.lblInvalidNo.Size = new System.Drawing.Size(50, 21);
            this.lblInvalidNo.TabIndex = 20;
            this.lblInvalidNo.Text = "0";
            this.lblInvalidNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInvalidNo.TextChanged += new System.EventHandler(this.lblInvalidNo_TextChanged);
            // 
            // lblInvalid
            // 
            this.lblInvalid.AutoSize = true;
            this.lblInvalid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalid.ForeColor = System.Drawing.Color.Black;
            this.lblInvalid.Location = new System.Drawing.Point(196, 16);
            this.lblInvalid.Name = "lblInvalid";
            this.lblInvalid.Size = new System.Drawing.Size(38, 13);
            this.lblInvalid.TabIndex = 19;
            this.lblInvalid.Text = "Invalid";
            // 
            // lblVerifiedNo
            // 
            this.lblVerifiedNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblVerifiedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifiedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifiedNo.ForeColor = System.Drawing.Color.Black;
            this.lblVerifiedNo.Location = new System.Drawing.Point(120, 11);
            this.lblVerifiedNo.Name = "lblVerifiedNo";
            this.lblVerifiedNo.Size = new System.Drawing.Size(76, 21);
            this.lblVerifiedNo.TabIndex = 18;
            this.lblVerifiedNo.Text = "0";
            this.lblVerifiedNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerified
            // 
            this.lblVerified.AutoSize = true;
            this.lblVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerified.ForeColor = System.Drawing.Color.Black;
            this.lblVerified.Location = new System.Drawing.Point(77, 15);
            this.lblVerified.Name = "lblVerified";
            this.lblVerified.Size = new System.Drawing.Size(42, 13);
            this.lblVerified.TabIndex = 17;
            this.lblVerified.Text = "Verified";
            // 
            // grpDisclaimer
            // 
            this.grpDisclaimer.Controls.Add(this.lblDisclaimer);
            this.grpDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.grpDisclaimer.Location = new System.Drawing.Point(15, 553);
            this.grpDisclaimer.Name = "grpDisclaimer";
            this.grpDisclaimer.Size = new System.Drawing.Size(706, 34);
            this.grpDisclaimer.TabIndex = 190;
            this.grpDisclaimer.TabStop = false;
            this.grpDisclaimer.Text = "Disclaimer";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.lblDisclaimer.Location = new System.Drawing.Point(6, 11);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(692, 18);
            this.lblDisclaimer.TabIndex = 18;
            this.lblDisclaimer.Text = "Disclaimer";
            this.lblDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrnBulkPANVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(735, 591);
            this.Controls.Add(this.grpDisclaimer);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvDeductees);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnBulkPANVerification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.TrnBulkPANVerification_Load);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpDisclaimer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlHeader;
        private DGVControl.DGVControl dgvDeductees;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnVerification;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker bgwPANVerification;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblVerified;
        private System.Windows.Forms.Label lblVerifiedNo;
        private System.Windows.Forms.Label lblInvalid;
        private System.Windows.Forms.Label lblInvalidNo;
        private System.Windows.Forms.Label lblNotVerifiedNo;
        private System.Windows.Forms.Label lblNotVerified;
        private System.Windows.Forms.GroupBox grpDisclaimer;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Button btnPrintInvalidPAN;
    }
}