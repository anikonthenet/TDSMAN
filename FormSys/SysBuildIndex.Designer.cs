namespace TDSMAN.FormSys
{
    partial class SysBuildIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysBuildIndex));
            this.grpOnline = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckhReIndexing = new System.Windows.Forms.CheckBox();
            this.chkRecalculation = new System.Windows.Forms.CheckBox();
            this.chkReserialising = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.lblPleaseWaitMessage1 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.bgwWorkerBuildIndex = new System.ComponentModel.BackgroundWorker();
            this.grpOnline.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOnline
            // 
            this.grpOnline.Controls.Add(this.panel2);
            this.grpOnline.Controls.Add(this.panel1);
            this.grpOnline.Controls.Add(this.ckhReIndexing);
            this.grpOnline.Controls.Add(this.chkRecalculation);
            this.grpOnline.Controls.Add(this.chkReserialising);
            this.grpOnline.Controls.Add(this.groupBox1);
            this.grpOnline.Controls.Add(this.lblPleaseWaitMessage1);
            this.grpOnline.Location = new System.Drawing.Point(11, 12);
            this.grpOnline.Name = "grpOnline";
            this.grpOnline.Size = new System.Drawing.Size(571, 273);
            this.grpOnline.TabIndex = 7;
            this.grpOnline.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(22, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(526, 1);
            this.panel2.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(23, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 1);
            this.panel1.TabIndex = 74;
            // 
            // ckhReIndexing
            // 
            this.ckhReIndexing.AutoSize = true;
            this.ckhReIndexing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckhReIndexing.Location = new System.Drawing.Point(147, 102);
            this.ckhReIndexing.Name = "ckhReIndexing";
            this.ckhReIndexing.Size = new System.Drawing.Size(194, 19);
            this.ckhReIndexing.TabIndex = 73;
            this.ckhReIndexing.Text = "Re-Indexing parameter tables";
            this.ckhReIndexing.UseVisualStyleBackColor = true;
            // 
            // chkRecalculation
            // 
            this.chkRecalculation.AutoSize = true;
            this.chkRecalculation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecalculation.Location = new System.Drawing.Point(147, 74);
            this.chkRecalculation.Name = "chkRecalculation";
            this.chkRecalculation.Size = new System.Drawing.Size(264, 19);
            this.chkRecalculation.TabIndex = 72;
            this.chkRecalculation.Text = "Re-Calculating deductee totals of challans";
            this.chkRecalculation.UseVisualStyleBackColor = true;
            // 
            // chkReserialising
            // 
            this.chkReserialising.AutoSize = true;
            this.chkReserialising.BackColor = System.Drawing.SystemColors.Control;
            this.chkReserialising.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReserialising.Location = new System.Drawing.Point(147, 46);
            this.chkReserialising.Name = "chkReserialising";
            this.chkReserialising.Size = new System.Drawing.Size(277, 19);
            this.chkReserialising.TabIndex = 71;
            this.chkReserialising.Text = "Re-Serialising challan and deductee records";
            this.chkReserialising.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(11, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 64);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblPrcnt
            // 
            this.lblPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrcnt.ForeColor = System.Drawing.Color.Black;
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
            this.prgBar.Size = new System.Drawing.Size(529, 10);
            this.prgBar.TabIndex = 147;
            // 
            // lblPleaseWaitMessage1
            // 
            this.lblPleaseWaitMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWaitMessage1.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblPleaseWaitMessage1.Location = new System.Drawing.Point(464, 245);
            this.lblPleaseWaitMessage1.Name = "lblPleaseWaitMessage1";
            this.lblPleaseWaitMessage1.Size = new System.Drawing.Size(94, 25);
            this.lblPleaseWaitMessage1.TabIndex = 6;
            this.lblPleaseWaitMessage1.Text = "Please Wait...";
            this.lblPleaseWaitMessage1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPleaseWaitMessage1.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(499, 291);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(83, 23);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // bgwWorkerBuildIndex
            // 
            this.bgwWorkerBuildIndex.WorkerReportsProgress = true;
            this.bgwWorkerBuildIndex.WorkerSupportsCancellation = true;
            this.bgwWorkerBuildIndex.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorkerBuildIndex_DoWork);
            this.bgwWorkerBuildIndex.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwWorkerBuildIndex_ProgressChanged);
            this.bgwWorkerBuildIndex.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorkerBuildIndex_RunWorkerCompleted);
            // 
            // SysBuildIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 319);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.grpOnline);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SysBuildIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.SysBuildIndex_Activated);
            this.grpOnline.ResumeLayout(false);
            this.grpOnline.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOnline;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPrcnt;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label lblPleaseWaitMessage1;
        public System.Windows.Forms.Button BtnCancel;
        private System.ComponentModel.BackgroundWorker bgwWorkerBuildIndex;
        private System.Windows.Forms.CheckBox ckhReIndexing;
        private System.Windows.Forms.CheckBox chkRecalculation;
        private System.Windows.Forms.CheckBox chkReserialising;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}