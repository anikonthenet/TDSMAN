namespace TDSMAN.FormUtl
{
    partial class UtlImportfromPrevYearDB_Restore
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelectTDSPath = new System.Windows.Forms.Button();
            this.txtFVUPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.grpNotes = new System.Windows.Forms.GroupBox();
            this.lnkLabel2 = new System.Windows.Forms.LinkLabel();
            this.lblNotes1 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 658);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(899, 16);
            this.BtnCancel.Size = new System.Drawing.Size(10, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Import";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(920, 15);
            this.BtnEdit.Size = new System.Drawing.Size(5, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(915, 15);
            this.BtnAdd.Size = new System.Drawing.Size(5, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(946, 15);
            this.BtnRefresh.Size = new System.Drawing.Size(11, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(939, 15);
            this.BtnDelete.Size = new System.Drawing.Size(7, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(932, 15);
            this.BtnSearch.Size = new System.Drawing.Size(7, 23);
            this.BtnSearch.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpNotes);
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Controls.Add(this.groupBox4);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSelectTDSPath);
            this.groupBox4.Controls.Add(this.txtFVUPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(164, 242);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(707, 57);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            // 
            // btnSelectTDSPath
            // 
            this.btnSelectTDSPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectTDSPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectTDSPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectTDSPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectTDSPath.Location = new System.Drawing.Point(646, 19);
            this.btnSelectTDSPath.Name = "btnSelectTDSPath";
            this.btnSelectTDSPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectTDSPath.TabIndex = 146;
            this.btnSelectTDSPath.Text = ". . .";
            this.btnSelectTDSPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectTDSPath.UseVisualStyleBackColor = false;
            this.btnSelectTDSPath.Click += new System.EventHandler(this.btnSelectTDSPath_Click);
            // 
            // txtFVUPath
            // 
            this.txtFVUPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtFVUPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFVUPath.Location = new System.Drawing.Point(85, 19);
            this.txtFVUPath.MaxLength = 75;
            this.txtFVUPath.Name = "txtFVUPath";
            this.txtFVUPath.ReadOnly = true;
            this.txtFVUPath.Size = new System.Drawing.Size(555, 20);
            this.txtFVUPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(24, 23);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(56, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "File path";
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(676, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(164, 316);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 64);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblPrcnt
            // 
            this.lblPrcnt.AutoSize = true;
            this.lblPrcnt.BackColor = System.Drawing.Color.Transparent;
            this.lblPrcnt.Location = new System.Drawing.Point(356, 27);
            this.lblPrcnt.Name = "lblPrcnt";
            this.lblPrcnt.Size = new System.Drawing.Size(0, 13);
            this.lblPrcnt.TabIndex = 149;
            this.lblPrcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpNotes
            // 
            this.grpNotes.Controls.Add(this.lnkLabel2);
            this.grpNotes.Controls.Add(this.lblNotes1);
            this.grpNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNotes.ForeColor = System.Drawing.Color.Black;
            this.grpNotes.Location = new System.Drawing.Point(164, 93);
            this.grpNotes.Name = "grpNotes";
            this.grpNotes.Size = new System.Drawing.Size(707, 135);
            this.grpNotes.TabIndex = 67;
            this.grpNotes.TabStop = false;
            this.grpNotes.Text = "Notes";
            // 
            // lnkLabel2
            // 
            this.lnkLabel2.Location = new System.Drawing.Point(8, 82);
            this.lnkLabel2.Name = "lnkLabel2";
            this.lnkLabel2.Size = new System.Drawing.Size(644, 22);
            this.lnkLabel2.TabIndex = 1;
            this.lnkLabel2.TabStop = true;
            this.lnkLabel2.Text = "linkLabel1";
            this.lnkLabel2.Visible = false;
            this.lnkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabel2_LinkClicked);
            // 
            // lblNotes1
            // 
            this.lblNotes1.ForeColor = System.Drawing.Color.Black;
            this.lblNotes1.Location = new System.Drawing.Point(8, 31);
            this.lblNotes1.Name = "lblNotes1";
            this.lblNotes1.Size = new System.Drawing.Size(693, 17);
            this.lblNotes1.TabIndex = 0;
            this.lblNotes1.Text = "This utility will import all the data from the source database to the existing da" +
                "tabase";
            // 
            // UtlImportfromPrevYearDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "UtlImportfromPrevYearDB";
            this.Load += new System.EventHandler(this.TrnFVUImport_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpNotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectTDSPath;
        private System.Windows.Forms.TextBox txtFVUPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox grpNotes;
        private System.Windows.Forms.Label lblNotes1;
        private System.Windows.Forms.LinkLabel lnkLabel2;
        private System.Windows.Forms.Label lblPrcnt;
    }
}
