namespace TDSMAN.FormWeb
{
    partial class TrnDownloadCSIFilePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDownloadCSIFilePopup));
            this.grpBackUp = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadTo = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadFrom = new System.Windows.Forms.MaskedTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblCaption = new System.Windows.Forms.Label();
            this.grpBackUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBackUp
            // 
            this.grpBackUp.Controls.Add(this.label4);
            this.grpBackUp.Controls.Add(this.label52);
            this.grpBackUp.Controls.Add(this.label3);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadTo);
            this.grpBackUp.Controls.Add(this.label2);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadFrom);
            this.grpBackUp.Location = new System.Drawing.Point(12, 26);
            this.grpBackUp.Name = "grpBackUp";
            this.grpBackUp.Size = new System.Drawing.Size(614, 40);
            this.grpBackUp.TabIndex = 0;
            this.grpBackUp.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(214, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 201;
            this.label4.Text = "DD/MM/YYYY";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(518, 16);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(89, 13);
            this.label52.TabIndex = 200;
            this.label52.Text = "DD/MM/YYYY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(321, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 195;
            this.label3.Text = "Challan To Date";
            // 
            // mskViewFileDownloadTo
            // 
            this.mskViewFileDownloadTo.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadTo.Location = new System.Drawing.Point(426, 12);
            this.mskViewFileDownloadTo.Mask = "00/00/0000";
            this.mskViewFileDownloadTo.Name = "mskViewFileDownloadTo";
            this.mskViewFileDownloadTo.Size = new System.Drawing.Size(89, 20);
            this.mskViewFileDownloadTo.TabIndex = 1;
            this.mskViewFileDownloadTo.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 193;
            this.label2.Text = "Challan From Date";
            // 
            // mskViewFileDownloadFrom
            // 
            this.mskViewFileDownloadFrom.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadFrom.Location = new System.Drawing.Point(123, 12);
            this.mskViewFileDownloadFrom.Mask = "00/00/0000";
            this.mskViewFileDownloadFrom.Name = "mskViewFileDownloadFrom";
            this.mskViewFileDownloadFrom.Size = new System.Drawing.Size(88, 20);
            this.mskViewFileDownloadFrom.TabIndex = 0;
            this.mskViewFileDownloadFrom.ValidatingType = typeof(System.DateTime);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(95, 162);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(169, 13);
            this.lblMessage.TabIndex = 217;
            this.lblMessage.Text = "Enter text as in above image";
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(429, 89);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 216;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(269, 159);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(180, 20);
            this.txtCaptchaCode.TabIndex = 1;
            // 
            // picCaptcha
            // 
            this.picCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCaptcha.Location = new System.Drawing.Point(139, 70);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(287, 84);
            this.picCaptcha.TabIndex = 215;
            this.picCaptcha.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(617, 1);
            this.label5.TabIndex = 218;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(545, 186);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(81, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Tag = "";
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.Blue;
            this.lblCaption.Location = new System.Drawing.Point(35, 6);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(567, 16);
            this.lblCaption.TabIndex = 220;
            this.lblCaption.Text = "Auto download of CSI file has failed. Please re-try by Manual download as under :" +
    "";
            // 
            // TrnDownloadCSIFilePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 212);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCaptchaRefresh);
            this.Controls.Add(this.txtCaptchaCode);
            this.Controls.Add(this.picCaptcha);
            this.Controls.Add(this.grpBackUp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnDownloadCSIFilePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download CSI File";
            this.Activated += new System.EventHandler(this.TrnDownloadCSIFilePopup_Activated);
            this.grpBackUp.ResumeLayout(false);
            this.grpBackUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBackUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadFrom;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblCaption;
    }
}