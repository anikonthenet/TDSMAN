namespace TDSMAN.FormTrn
{
    partial class TrnTDSFile_BulkImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnTDSFile_BulkImport));
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
            this.tmrLoginRefresh = new System.Windows.Forms.Timer(this.components);
            this.grpSelect = new System.Windows.Forms.GroupBox();
            this.lblAddDeducteesToRegularMaster = new System.Windows.Forms.Label();
            this.chkAddDeducteesToRegularMaster = new System.Windows.Forms.CheckBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.grpSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
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
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Controls.Add(this.pctVideoDemo);
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
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpSelect);
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
            this.groupBox4.Location = new System.Drawing.Point(164, 190);
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
            this.btnSelectTDSPath.Location = new System.Drawing.Point(641, 17);
            this.btnSelectTDSPath.Name = "btnSelectTDSPath";
            this.btnSelectTDSPath.Size = new System.Drawing.Size(42, 24);
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
            this.txtFVUPath.TabIndex = 0;
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
            this.grpNotes.Location = new System.Drawing.Point(164, 44);
            this.grpNotes.Name = "grpNotes";
            this.grpNotes.Size = new System.Drawing.Size(707, 135);
            this.grpNotes.TabIndex = 67;
            this.grpNotes.TabStop = false;
            this.grpNotes.Text = "Notes";
            this.grpNotes.Enter += new System.EventHandler(this.grpNotes_Enter);
            // 
            // lnkLabel2
            // 
            this.lnkLabel2.Location = new System.Drawing.Point(8, 82);
            this.lnkLabel2.Name = "lnkLabel2";
            this.lnkLabel2.Size = new System.Drawing.Size(644, 22);
            this.lnkLabel2.TabIndex = 1;
            this.lnkLabel2.TabStop = true;
            this.lnkLabel2.Text = "linkLabel1";
            this.lnkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabel2_LinkClicked);
            // 
            // lblNotes1
            // 
            this.lblNotes1.ForeColor = System.Drawing.Color.Black;
            this.lblNotes1.Location = new System.Drawing.Point(8, 31);
            this.lblNotes1.Name = "lblNotes1";
            this.lblNotes1.Size = new System.Drawing.Size(693, 38);
            this.lblNotes1.TabIndex = 0;
            this.lblNotes1.Text = "1";
            // 
            // tmrLoginRefresh
            // 
            this.tmrLoginRefresh.Interval = 5000;
            this.tmrLoginRefresh.Tick += new System.EventHandler(this.tmrLoginRefresh_Tick);
            // 
            // grpSelect
            // 
            this.grpSelect.Controls.Add(this.lblAddDeducteesToRegularMaster);
            this.grpSelect.Controls.Add(this.chkAddDeducteesToRegularMaster);
            this.grpSelect.Location = new System.Drawing.Point(164, 254);
            this.grpSelect.Name = "grpSelect";
            this.grpSelect.Size = new System.Drawing.Size(707, 56);
            this.grpSelect.TabIndex = 69;
            this.grpSelect.TabStop = false;
            // 
            // lblAddDeducteesToRegularMaster
            // 
            this.lblAddDeducteesToRegularMaster.ForeColor = System.Drawing.Color.Blue;
            this.lblAddDeducteesToRegularMaster.Location = new System.Drawing.Point(381, 12);
            this.lblAddDeducteesToRegularMaster.Name = "lblAddDeducteesToRegularMaster";
            this.lblAddDeducteesToRegularMaster.Size = new System.Drawing.Size(309, 38);
            this.lblAddDeducteesToRegularMaster.TabIndex = 1;
            this.lblAddDeducteesToRegularMaster.Text = "While importing data from conso file, if deductees/employee not available in Regu" +
    "lar Return Master data then it will add to it.";
            this.lblAddDeducteesToRegularMaster.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAddDeducteesToRegularMaster
            // 
            this.chkAddDeducteesToRegularMaster.AutoSize = true;
            this.chkAddDeducteesToRegularMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddDeducteesToRegularMaster.Location = new System.Drawing.Point(17, 14);
            this.chkAddDeducteesToRegularMaster.Name = "chkAddDeducteesToRegularMaster";
            this.chkAddDeducteesToRegularMaster.Size = new System.Drawing.Size(356, 17);
            this.chkAddDeducteesToRegularMaster.TabIndex = 0;
            this.chkAddDeducteesToRegularMaster.Text = "Add deductees/employees to Regular Return Master data.";
            this.chkAddDeducteesToRegularMaster.UseVisualStyleBackColor = true;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(38, 32);
            this.pctVideoDemo.TabIndex = 209;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(955, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(38, 32);
            this.pctUserManual.TabIndex = 210;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // TrnTDSFile_BulkImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "TrnTDSFile_BulkImport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrnTDSFile_BulkImport_FormClosing);
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
            this.grpSelect.ResumeLayout(false);
            this.grpSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
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
        private System.Windows.Forms.Timer tmrLoginRefresh;
        private System.Windows.Forms.GroupBox grpSelect;
        private System.Windows.Forms.Label lblAddDeducteesToRegularMaster;
        private System.Windows.Forms.CheckBox chkAddDeducteesToRegularMaster;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
