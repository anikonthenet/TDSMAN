namespace TDSMAN.FormSys
{
    partial class SysCommercialPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysCommercialPopup));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDoNotShow = new System.Windows.Forms.CheckBox();
            this.lnkViewMore = new System.Windows.Forms.LinkLabel();
            this.pctBand = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pctBand)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 53);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(489, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(425, 192);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 22);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.Blue;
            this.lblDesc.Location = new System.Drawing.Point(26, 77);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(439, 73);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Desc";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(-1, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 1);
            this.panel1.TabIndex = 6;
            // 
            // chkDoNotShow
            // 
            this.chkDoNotShow.AutoSize = true;
            this.chkDoNotShow.BackColor = System.Drawing.Color.Transparent;
            this.chkDoNotShow.Checked = true;
            this.chkDoNotShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoNotShow.Location = new System.Drawing.Point(4, 195);
            this.chkDoNotShow.Name = "chkDoNotShow";
            this.chkDoNotShow.Size = new System.Drawing.Size(179, 17);
            this.chkDoNotShow.TabIndex = 7;
            this.chkDoNotShow.Text = "Do not show this message again";
            this.chkDoNotShow.UseVisualStyleBackColor = false;
            // 
            // lnkViewMore
            // 
            this.lnkViewMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkViewMore.Location = new System.Drawing.Point(26, 155);
            this.lnkViewMore.Name = "lnkViewMore";
            this.lnkViewMore.Size = new System.Drawing.Size(439, 31);
            this.lnkViewMore.TabIndex = 8;
            this.lnkViewMore.TabStop = true;
            this.lnkViewMore.Text = "View More ..";
            this.lnkViewMore.Visible = false;
            this.lnkViewMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewMore_LinkClicked);
            // 
            // pctBand
            // 
            this.pctBand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pctBand.BackgroundImage")));
            this.pctBand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pctBand.Location = new System.Drawing.Point(0, 0);
            this.pctBand.Name = "pctBand";
            this.pctBand.Size = new System.Drawing.Size(491, 49);
            this.pctBand.TabIndex = 9;
            this.pctBand.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(-1, 190);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 1);
            this.panel2.TabIndex = 11;
            // 
            // SysCommercialPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(491, 216);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pctBand);
            this.Controls.Add(this.lnkViewMore);
            this.Controls.Add(this.chkDoNotShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysCommercialPopup";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDSMAN Alert";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SysCommercialPopup_FormClosing);
            this.Load += new System.EventHandler(this.SysPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctBand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDoNotShow;
        private System.Windows.Forms.LinkLabel lnkViewMore;
        private System.Windows.Forms.PictureBox pctBand;
        private System.Windows.Forms.Panel panel2;
    }
}