namespace TDSMAN.FormWeb
{
    partial class TrnTracesViewUnconsumedChallan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnTracesViewUnconsumedChallan));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.lnkLogOff = new System.Windows.Forms.LinkLabel();
            this.grpDownloadList = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grpListStatement = new System.Windows.Forms.GroupBox();
            this.dgvStatementList = new System.Windows.Forms.DataGridView();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.grpDownloadList.SuspendLayout();
            this.grpListStatement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 608);
            this.grpSort.Size = new System.Drawing.Size(280, 8);
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
            this.BtnSave.Text = "&Login";
            this.BtnSave.Visible = false;
            this.BtnSave.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 609);
            this.grpSearch.Size = new System.Drawing.Size(280, 8);
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
            this.BtnExit.Click += new System.EventHandler(this.btnLoginCancel_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 552);
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
            this.grpButton.Location = new System.Drawing.Point(9, 559);
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
            this.pnlControls.Controls.Add(this.grpListStatement);
            this.pnlControls.Controls.Add(this.grpProgress);
            this.pnlControls.Controls.Add(this.grpDownloadList);
            this.pnlControls.Size = new System.Drawing.Size(1012, 569);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 543);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(124, 455);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(670, 37);
            this.grpProgress.TabIndex = 214;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            this.grpProgress.Visible = false;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 17);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 16);
            this.pBar.TabIndex = 8;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(911, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 206;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // lnkLogOff
            // 
            this.lnkLogOff.AutoSize = true;
            this.lnkLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogOff.Location = new System.Drawing.Point(894, 11);
            this.lnkLogOff.Name = "lnkLogOff";
            this.lnkLogOff.Size = new System.Drawing.Size(49, 13);
            this.lnkLogOff.TabIndex = 195;
            this.lnkLogOff.TabStop = true;
            this.lnkLogOff.Text = "Log Off";
            this.lnkLogOff.Visible = false;
            this.lnkLogOff.Click += new System.EventHandler(this.lnkLogOff_Click);
            // 
            // grpDownloadList
            // 
            this.grpDownloadList.Controls.Add(this.label9);
            this.grpDownloadList.Controls.Add(this.txtSearch);
            this.grpDownloadList.Controls.Add(this.lnkLogOff);
            this.grpDownloadList.Location = new System.Drawing.Point(31, 0);
            this.grpDownloadList.Name = "grpDownloadList";
            this.grpDownloadList.Size = new System.Drawing.Size(949, 450);
            this.grpDownloadList.TabIndex = 48;
            this.grpDownloadList.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 210;
            this.label9.Text = "Search - ";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(69, 12);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(420, 20);
            this.txtSearch.TabIndex = 197;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // grpListStatement
            // 
            this.grpListStatement.Controls.Add(this.dgvStatementList);
            this.grpListStatement.Location = new System.Drawing.Point(33, 33);
            this.grpListStatement.Name = "grpListStatement";
            this.grpListStatement.Size = new System.Drawing.Size(943, 418);
            this.grpListStatement.TabIndex = 198;
            this.grpListStatement.TabStop = false;
            this.grpListStatement.Visible = false;
            // 
            // dgvStatementList
            // 
            this.dgvStatementList.AllowUserToAddRows = false;
            this.dgvStatementList.AllowUserToDeleteRows = false;
            this.dgvStatementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementList.Location = new System.Drawing.Point(8, 11);
            this.dgvStatementList.Name = "dgvStatementList";
            this.dgvStatementList.ReadOnly = true;
            this.dgvStatementList.Size = new System.Drawing.Size(924, 401);
            this.dgvStatementList.TabIndex = 15;
            this.dgvStatementList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvStatementList_CellContentClick);
            this.dgvStatementList.SelectionChanged += new System.EventHandler(this.DgvStatementList_SelectionChanged);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(948, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 212;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnTracesViewUnconsumedChallan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1033, 594);
            this.Name = "TrnTracesViewUnconsumedChallan";
            this.Activated += new System.EventHandler(this.TrnStatementStatusTraces_Activated);
            this.Load += new System.EventHandler(this.TrnStatementStatusTraces_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.grpDownloadList.ResumeLayout(false);
            this.grpDownloadList.PerformLayout();
            this.grpListStatement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.GroupBox grpDownloadList;
        private System.Windows.Forms.LinkLabel lnkLogOff;
        private System.Windows.Forms.GroupBox grpListStatement;
        private System.Windows.Forms.DataGridView dgvStatementList;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label9;
    }
}
