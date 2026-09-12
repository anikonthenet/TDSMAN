namespace TDSMAN.FormSys
{
    partial class SysFVUUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysFVUUpdate));
            this.tbcRegistration = new System.Windows.Forms.TabControl();
            this.tbpOnlineRegistration = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpOnline = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.lblOnlineStatus = new System.Windows.Forms.Label();
            this.lblPleaseWaitMessage1 = new System.Windows.Forms.Label();
            this.lblHeaderMessage = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pctLOGO = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bgwWorkerFVU = new System.ComponentModel.BackgroundWorker();
            this.tbcRegistration.SuspendLayout();
            this.tbpOnlineRegistration.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpOnline.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLOGO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcRegistration
            // 
            this.tbcRegistration.Controls.Add(this.tbpOnlineRegistration);
            this.tbcRegistration.Location = new System.Drawing.Point(2, 53);
            this.tbcRegistration.Name = "tbcRegistration";
            this.tbcRegistration.SelectedIndex = 0;
            this.tbcRegistration.Size = new System.Drawing.Size(717, 330);
            this.tbcRegistration.TabIndex = 1;
            // 
            // tbpOnlineRegistration
            // 
            this.tbpOnlineRegistration.Controls.Add(this.panel1);
            this.tbpOnlineRegistration.Location = new System.Drawing.Point(4, 22);
            this.tbpOnlineRegistration.Name = "tbpOnlineRegistration";
            this.tbpOnlineRegistration.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOnlineRegistration.Size = new System.Drawing.Size(709, 304);
            this.tbpOnlineRegistration.TabIndex = 0;
            this.tbpOnlineRegistration.Text = "SelectType";
            this.tbpOnlineRegistration.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.grpOnline);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 303);
            this.panel1.TabIndex = 1;
            // 
            // grpOnline
            // 
            this.grpOnline.Controls.Add(this.groupBox1);
            this.grpOnline.Controls.Add(this.lblOnlineStatus);
            this.grpOnline.Controls.Add(this.lblPleaseWaitMessage1);
            this.grpOnline.Location = new System.Drawing.Point(9, 9);
            this.grpOnline.Name = "grpOnline";
            this.grpOnline.Size = new System.Drawing.Size(690, 284);
            this.grpOnline.TabIndex = 6;
            this.grpOnline.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(10, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 64);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblPrcnt
            // 
            this.lblPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrcnt.ForeColor = System.Drawing.Color.Maroon;
            this.lblPrcnt.Location = new System.Drawing.Point(11, 39);
            this.lblPrcnt.Name = "lblPrcnt";
            this.lblPrcnt.Size = new System.Drawing.Size(172, 18);
            this.lblPrcnt.TabIndex = 148;
            this.lblPrcnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(9, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(653, 10);
            this.prgBar.TabIndex = 147;
            // 
            // lblOnlineStatus
            // 
            this.lblOnlineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnlineStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblOnlineStatus.Location = new System.Drawing.Point(16, 16);
            this.lblOnlineStatus.Name = "lblOnlineStatus";
            this.lblOnlineStatus.Size = new System.Drawing.Size(532, 29);
            this.lblOnlineStatus.TabIndex = 7;
            this.lblOnlineStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPleaseWaitMessage1
            // 
            this.lblPleaseWaitMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWaitMessage1.ForeColor = System.Drawing.Color.Maroon;
            this.lblPleaseWaitMessage1.Location = new System.Drawing.Point(590, 256);
            this.lblPleaseWaitMessage1.Name = "lblPleaseWaitMessage1";
            this.lblPleaseWaitMessage1.Size = new System.Drawing.Size(94, 25);
            this.lblPleaseWaitMessage1.TabIndex = 6;
            this.lblPleaseWaitMessage1.Text = "Please Wait...";
            this.lblPleaseWaitMessage1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPleaseWaitMessage1.Visible = false;
            // 
            // lblHeaderMessage
            // 
            this.lblHeaderMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeaderMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblHeaderMessage.Location = new System.Drawing.Point(2, 1);
            this.lblHeaderMessage.Name = "lblHeaderMessage";
            this.lblHeaderMessage.Size = new System.Drawing.Size(716, 72);
            this.lblHeaderMessage.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(629, 398);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(83, 23);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(207, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(293, 20);
            this.textBox1.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(88, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "Email";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(207, 94);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(293, 20);
            this.textBox2.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Teal;
            this.label9.Location = new System.Drawing.Point(88, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 21);
            this.label9.TabIndex = 9;
            this.label9.Text = "Licensee Name";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(207, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(176, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Teal;
            this.label10.Location = new System.Drawing.Point(88, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 21);
            this.label10.TabIndex = 7;
            this.label10.Text = "Enter Serial No.";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(6, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(550, 29);
            this.label11.TabIndex = 6;
            // 
            // pctLOGO
            // 
            this.pctLOGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pctLOGO.Image = ((System.Drawing.Image)(resources.GetObject("pctLOGO.Image")));
            this.pctLOGO.Location = new System.Drawing.Point(589, 4);
            this.pctLOGO.Name = "pctLOGO";
            this.pctLOGO.Size = new System.Drawing.Size(128, 52);
            this.pctLOGO.TabIndex = 13;
            this.pctLOGO.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 71);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // bgwWorkerFVU
            // 
            this.bgwWorkerFVU.WorkerReportsProgress = true;
            this.bgwWorkerFVU.WorkerSupportsCancellation = true;
            this.bgwWorkerFVU.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorkerFVU_DoWork);
            this.bgwWorkerFVU.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwWorkerFVU_ProgressChanged);
            this.bgwWorkerFVU.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorkerFVU_RunWorkerCompleted);
            // 
            // SysFVUUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(720, 433);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pctLOGO);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.lblHeaderMessage);
            this.Controls.Add(this.tbcRegistration);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysFVUUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FVU Updation";
            this.Shown += new System.EventHandler(this.SysFVUUpdate_Shown);
            this.tbcRegistration.ResumeLayout(false);
            this.tbpOnlineRegistration.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpOnline.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctLOGO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcRegistration;
        private System.Windows.Forms.TabPage tbpOnlineRegistration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeaderMessage;
        public System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pctLOGO;
        private System.Windows.Forms.GroupBox grpOnline;
        private System.Windows.Forms.Label lblPleaseWaitMessage1;
        private System.Windows.Forms.Label lblOnlineStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker bgwWorkerFVU;
        private System.Windows.Forms.Label lblPrcnt;

    }
}