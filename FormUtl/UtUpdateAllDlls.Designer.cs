namespace TDSMAN.FormUtl
{
    partial class UtUpdateAllDlls
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
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.bgwWorker = new System.ComponentModel.BackgroundWorker();
            this.bgwItextsharpdll = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(12, 29);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(551, 23);
            this.prgBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please wait while the Application is getting loaded...";
            // 
            // bgwWorker
            // 
            this.bgwWorker.WorkerReportsProgress = true;
            this.bgwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorker_DoWork);
            this.bgwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorker_RunWorkerCompleted);
            this.bgwWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwWorker_ProgressChanged);
            // 
            // bgwItextsharpdll
            // 
            this.bgwItextsharpdll.WorkerReportsProgress = true;
            this.bgwItextsharpdll.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwItextsharpdll_DoWork);
            this.bgwItextsharpdll.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwItextsharpdll_RunWorkerCompleted);
            this.bgwItextsharpdll.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwItextsharpdll_ProgressChanged);
            // 
            // UtUpdateAllDlls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 71);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prgBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UtUpdateAllDlls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDSMAN";
            this.Activated += new System.EventHandler(this.UtUpdateApplicationUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bgwWorker;
        private System.ComponentModel.BackgroundWorker bgwItextsharpdll;
    }
}