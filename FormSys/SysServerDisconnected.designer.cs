namespace TDSMAN.FormSys
{
    partial class SysServerDisconnected
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysServerDisconnected));
            this.btnOk = new System.Windows.Forms.Button();
            this.pctErr = new System.Windows.Forms.PictureBox();
            this.lblHeaderMessage = new System.Windows.Forms.Label();
            this.lblBodyMessage = new System.Windows.Forms.Label();
            this.lblFooterMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pctErr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Lavender;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(226, 158);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 26);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pctErr
            // 
            this.pctErr.Image = ((System.Drawing.Image)(resources.GetObject("pctErr.Image")));
            this.pctErr.Location = new System.Drawing.Point(3, 17);
            this.pctErr.Name = "pctErr";
            this.pctErr.Size = new System.Drawing.Size(64, 65);
            this.pctErr.TabIndex = 1;
            this.pctErr.TabStop = false;
            // 
            // lblHeaderMessage
            // 
            this.lblHeaderMessage.AutoSize = true;
            this.lblHeaderMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderMessage.Location = new System.Drawing.Point(75, 18);
            this.lblHeaderMessage.Name = "lblHeaderMessage";
            this.lblHeaderMessage.Size = new System.Drawing.Size(41, 15);
            this.lblHeaderMessage.TabIndex = 2;
            this.lblHeaderMessage.Text = "label1";
            // 
            // lblBodyMessage
            // 
            this.lblBodyMessage.AutoSize = true;
            this.lblBodyMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBodyMessage.Location = new System.Drawing.Point(75, 55);
            this.lblBodyMessage.Name = "lblBodyMessage";
            this.lblBodyMessage.Size = new System.Drawing.Size(41, 15);
            this.lblBodyMessage.TabIndex = 3;
            this.lblBodyMessage.Text = "label1";
            // 
            // lblFooterMessage
            // 
            this.lblFooterMessage.AutoSize = true;
            this.lblFooterMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooterMessage.Location = new System.Drawing.Point(6, 128);
            this.lblFooterMessage.Name = "lblFooterMessage";
            this.lblFooterMessage.Size = new System.Drawing.Size(41, 15);
            this.lblFooterMessage.TabIndex = 4;
            this.lblFooterMessage.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 1);
            this.panel1.TabIndex = 5;
            // 
            // SysServerDisconnected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 189);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblFooterMessage);
            this.Controls.Add(this.lblBodyMessage);
            this.Controls.Add(this.lblHeaderMessage);
            this.Controls.Add(this.pctErr);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SysServerDisconnected";
            this.ShowIcon = false;
            this.Text = "Server Disconnected";
            this.Load += new System.EventHandler(this.SysServerDisconnected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctErr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pctErr;
        private System.Windows.Forms.Label lblHeaderMessage;
        private System.Windows.Forms.Label lblBodyMessage;
        private System.Windows.Forms.Label lblFooterMessage;
        private System.Windows.Forms.Panel panel1;
    }
}