namespace TDSMAN.FormTrn
{
    partial class TrnTrialMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnTrialMessageBox));
            this.lnkOrderNow = new System.Windows.Forms.LinkLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblOffers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lnkOrderNow
            // 
            this.lnkOrderNow.AutoSize = true;
            this.lnkOrderNow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkOrderNow.Location = new System.Drawing.Point(7, 74);
            this.lnkOrderNow.Name = "lnkOrderNow";
            this.lnkOrderNow.Size = new System.Drawing.Size(286, 16);
            this.lnkOrderNow.TabIndex = 0;
            this.lnkOrderNow.TabStop = true;
            this.lnkOrderNow.Text = "Click here to purchase the Licensed Version";
            this.lnkOrderNow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOrderNow_LinkClicked);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Lavender;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(686, 73);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 22);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblMessage1
            // 
            this.lblMessage1.AutoSize = true;
            this.lblMessage1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage1.ForeColor = System.Drawing.Color.Black;
            this.lblMessage1.Location = new System.Drawing.Point(7, 9);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(208, 16);
            this.lblMessage1.TabIndex = 3;
            this.lblMessage1.Text = "You are using the Trial version. ";
            // 
            // lblMessage2
            // 
            this.lblMessage2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.ForeColor = System.Drawing.Color.Red;
            this.lblMessage2.Location = new System.Drawing.Point(7, 30);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(730, 39);
            this.lblMessage2.TabIndex = 4;
            this.lblMessage2.Text = "lblMessage2";
            this.lblMessage2.UseMnemonic = false;
            // 
            // lblOffers
            // 
            this.lblOffers.AutoSize = true;
            this.lblOffers.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffers.ForeColor = System.Drawing.Color.Blue;
            this.lblOffers.Location = new System.Drawing.Point(538, 77);
            this.lblOffers.Name = "lblOffers";
            this.lblOffers.Size = new System.Drawing.Size(145, 15);
            this.lblOffers.TabIndex = 5;
            this.lblOffers.Text = "* for special offers give us a call ";
            this.lblOffers.UseMnemonic = false;
            // 
            // TrnTrialMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 97);
            this.Controls.Add(this.lblOffers);
            this.Controls.Add(this.lblMessage2);
            this.Controls.Add(this.lblMessage1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lnkOrderNow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnTrialMessageBox";
            this.Text = "TDSMAN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label lblMessage2;
        public System.Windows.Forms.Label lblMessage1;
        public System.Windows.Forms.LinkLabel lnkOrderNow;
        public System.Windows.Forms.Label lblOffers;
    }
}