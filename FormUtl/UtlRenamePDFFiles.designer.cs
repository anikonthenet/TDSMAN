namespace TDSMAN.FormUtl
{
    partial class UtlRenamePDFFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtlRenamePDFFiles));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblInputPDFFiles = new System.Windows.Forms.Label();
            this.btnSelectInputFilePDFPath = new System.Windows.Forms.Button();
            this.txtInputFilePDFPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.prgRenameBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRenameProgress = new System.Windows.Forms.Label();
            this.lblPrcnt = new System.Windows.Forms.Label();
            this.grpNotes = new System.Windows.Forms.GroupBox();
            this.lblNotes2 = new System.Windows.Forms.Label();
            this.lblNotes1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectOutputFilePDFPath = new System.Windows.Forms.Button();
            this.txtOutputPDFFolderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpNamingStyle = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSampleName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnNameLast = new System.Windows.Forms.RadioButton();
            this.rbnNameFirst = new System.Windows.Forms.RadioButton();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctManual = new System.Windows.Forms.PictureBox();
            this.btnGetChallan = new System.Windows.Forms.Button();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpNamingStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).BeginInit();
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
            this.BtnSave.Text = "&Rename";
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
            this.grpButton.Controls.Add(this.pctManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctManual, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnGetChallan);
            this.pnlControls.Controls.Add(this.grpNamingStyle);
            this.pnlControls.Controls.Add(this.groupBox2);
            this.pnlControls.Controls.Add(this.grpNotes);
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Controls.Add(this.groupBox4);
            this.pnlControls.Size = new System.Drawing.Size(1004, 554);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblInputPDFFiles);
            this.groupBox4.Controls.Add(this.btnSelectInputFilePDFPath);
            this.groupBox4.Controls.Add(this.txtInputFilePDFPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(145, 211);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(745, 57);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            // 
            // lblInputPDFFiles
            // 
            this.lblInputPDFFiles.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputPDFFiles.ForeColor = System.Drawing.Color.Blue;
            this.lblInputPDFFiles.Location = new System.Drawing.Point(130, 39);
            this.lblInputPDFFiles.Name = "lblInputPDFFiles";
            this.lblInputPDFFiles.Size = new System.Drawing.Size(330, 13);
            this.lblInputPDFFiles.TabIndex = 151;
            // 
            // btnSelectInputFilePDFPath
            // 
            this.btnSelectInputFilePDFPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectInputFilePDFPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectInputFilePDFPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectInputFilePDFPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectInputFilePDFPath.Location = new System.Drawing.Point(688, 15);
            this.btnSelectInputFilePDFPath.Name = "btnSelectInputFilePDFPath";
            this.btnSelectInputFilePDFPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectInputFilePDFPath.TabIndex = 146;
            this.btnSelectInputFilePDFPath.Text = ". . .";
            this.btnSelectInputFilePDFPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectInputFilePDFPath.UseVisualStyleBackColor = false;
            this.btnSelectInputFilePDFPath.Click += new System.EventHandler(this.btnSelectInputFilePDFPath_Click);
            // 
            // txtInputFilePDFPath
            // 
            this.txtInputFilePDFPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtInputFilePDFPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInputFilePDFPath.Location = new System.Drawing.Point(128, 16);
            this.txtInputFilePDFPath.MaxLength = 75;
            this.txtInputFilePDFPath.Name = "txtInputFilePDFPath";
            this.txtInputFilePDFPath.ReadOnly = true;
            this.txtInputFilePDFPath.Size = new System.Drawing.Size(559, 20);
            this.txtInputFilePDFPath.TabIndex = 1;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(5, 19);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(117, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Choose input folder";
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // prgRenameBar
            // 
            this.prgRenameBar.Location = new System.Drawing.Point(13, 21);
            this.prgRenameBar.Name = "prgRenameBar";
            this.prgRenameBar.Size = new System.Drawing.Size(717, 21);
            this.prgRenameBar.TabIndex = 147;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRenameProgress);
            this.groupBox1.Controls.Add(this.lblPrcnt);
            this.groupBox1.Controls.Add(this.prgRenameBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(145, 402);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 69);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblRenameProgress
            // 
            this.lblRenameProgress.Font = new System.Drawing.Font("Courier New", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenameProgress.ForeColor = System.Drawing.Color.Blue;
            this.lblRenameProgress.Location = new System.Drawing.Point(18, 46);
            this.lblRenameProgress.Name = "lblRenameProgress";
            this.lblRenameProgress.Size = new System.Drawing.Size(330, 18);
            this.lblRenameProgress.TabIndex = 150;
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
            this.grpNotes.Controls.Add(this.lblNotes2);
            this.grpNotes.Controls.Add(this.lblNotes1);
            this.grpNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNotes.ForeColor = System.Drawing.Color.Black;
            this.grpNotes.Location = new System.Drawing.Point(145, 33);
            this.grpNotes.Name = "grpNotes";
            this.grpNotes.Size = new System.Drawing.Size(745, 167);
            this.grpNotes.TabIndex = 67;
            this.grpNotes.TabStop = false;
            // 
            // lblNotes2
            // 
            this.lblNotes2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes2.ForeColor = System.Drawing.Color.Black;
            this.lblNotes2.Location = new System.Drawing.Point(4, 108);
            this.lblNotes2.Name = "lblNotes2";
            this.lblNotes2.Size = new System.Drawing.Size(736, 51);
            this.lblNotes2.TabIndex = 1;
            // 
            // lblNotes1
            // 
            this.lblNotes1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes1.ForeColor = System.Drawing.Color.Blue;
            this.lblNotes1.Location = new System.Drawing.Point(7, 16);
            this.lblNotes1.Name = "lblNotes1";
            this.lblNotes1.Size = new System.Drawing.Size(693, 87);
            this.lblNotes1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectOutputFilePDFPath);
            this.groupBox2.Controls.Add(this.txtOutputPDFFolderPath);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(145, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(745, 57);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            // 
            // btnSelectOutputFilePDFPath
            // 
            this.btnSelectOutputFilePDFPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectOutputFilePDFPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectOutputFilePDFPath.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectOutputFilePDFPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectOutputFilePDFPath.Location = new System.Drawing.Point(688, 18);
            this.btnSelectOutputFilePDFPath.Name = "btnSelectOutputFilePDFPath";
            this.btnSelectOutputFilePDFPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectOutputFilePDFPath.TabIndex = 146;
            this.btnSelectOutputFilePDFPath.Text = ". . .";
            this.btnSelectOutputFilePDFPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectOutputFilePDFPath.UseVisualStyleBackColor = false;
            this.btnSelectOutputFilePDFPath.Click += new System.EventHandler(this.btnSelectOututFilePDFPath_Click);
            // 
            // txtOutputPDFFolderPath
            // 
            this.txtOutputPDFFolderPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtOutputPDFFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputPDFFolderPath.Location = new System.Drawing.Point(128, 19);
            this.txtOutputPDFFolderPath.MaxLength = 75;
            this.txtOutputPDFFolderPath.Name = "txtOutputPDFFolderPath";
            this.txtOutputPDFFolderPath.ReadOnly = true;
            this.txtOutputPDFFolderPath.Size = new System.Drawing.Size(559, 20);
            this.txtOutputPDFFolderPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output folder path";
            // 
            // grpNamingStyle
            // 
            this.grpNamingStyle.Controls.Add(this.label2);
            this.grpNamingStyle.Controls.Add(this.txtSampleName);
            this.grpNamingStyle.Controls.Add(this.panel1);
            this.grpNamingStyle.Controls.Add(this.rbnNameLast);
            this.grpNamingStyle.Controls.Add(this.rbnNameFirst);
            this.grpNamingStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNamingStyle.Location = new System.Drawing.Point(145, 347);
            this.grpNamingStyle.Name = "grpNamingStyle";
            this.grpNamingStyle.Size = new System.Drawing.Size(745, 49);
            this.grpNamingStyle.TabIndex = 69;
            this.grpNamingStyle.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(272, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 152;
            this.label2.Text = "for e.g.";
            // 
            // txtSampleName
            // 
            this.txtSampleName.BackColor = System.Drawing.Color.White;
            this.txtSampleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSampleName.Location = new System.Drawing.Point(342, 16);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.ReadOnly = true;
            this.txtSampleName.Size = new System.Drawing.Size(390, 20);
            this.txtSampleName.TabIndex = 3;
            this.txtSampleName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(210, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(56, 3);
            this.panel1.TabIndex = 2;
            // 
            // rbnNameLast
            // 
            this.rbnNameLast.AutoSize = true;
            this.rbnNameLast.Location = new System.Drawing.Point(114, 17);
            this.rbnNameLast.Name = "rbnNameLast";
            this.rbnNameLast.Size = new System.Drawing.Size(93, 17);
            this.rbnNameLast.TabIndex = 1;
            this.rbnNameLast.Text = "Suffix Name";
            this.rbnNameLast.UseVisualStyleBackColor = true;
            this.rbnNameLast.CheckedChanged += new System.EventHandler(this.rbnNameFirstLast_CheckedChanged);
            // 
            // rbnNameFirst
            // 
            this.rbnNameFirst.AutoSize = true;
            this.rbnNameFirst.Checked = true;
            this.rbnNameFirst.Location = new System.Drawing.Point(17, 17);
            this.rbnNameFirst.Name = "rbnNameFirst";
            this.rbnNameFirst.Size = new System.Drawing.Size(93, 17);
            this.rbnNameFirst.TabIndex = 0;
            this.rbnNameFirst.TabStop = true;
            this.rbnNameFirst.Text = "Prefix Name";
            this.rbnNameFirst.UseVisualStyleBackColor = true;
            this.rbnNameFirst.CheckedChanged += new System.EventHandler(this.rbnNameFirstLast_CheckedChanged);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(917, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(39, 32);
            this.pctVideoDemo.TabIndex = 192;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctManual
            // 
            this.pctManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctManual.Image = ((System.Drawing.Image)(resources.GetObject("pctManual.Image")));
            this.pctManual.Location = new System.Drawing.Point(955, 11);
            this.pctManual.Name = "pctManual";
            this.pctManual.Size = new System.Drawing.Size(39, 32);
            this.pctManual.TabIndex = 193;
            this.pctManual.TabStop = false;
            this.pctManual.Tag = "User Manual";
            this.pctManual.Click += new System.EventHandler(this.pctManual_Click);
            this.pctManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctManual_MouseMove);
            // 
            // btnGetChallan
            // 
            this.btnGetChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetChallan.Location = new System.Drawing.Point(565, 490);
            this.btnGetChallan.Name = "btnGetChallan";
            this.btnGetChallan.Size = new System.Drawing.Size(105, 23);
            this.btnGetChallan.TabIndex = 70;
            this.btnGetChallan.Text = "GET CHALLAN";
            this.btnGetChallan.UseVisualStyleBackColor = true;
            this.btnGetChallan.Visible = false;
            this.btnGetChallan.Click += new System.EventHandler(this.BtnGetChallan_Click);
            // 
            // UtlRenamePDFFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Name = "UtlRenamePDFFiles";
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpNamingStyle.ResumeLayout(false);
            this.grpNamingStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectInputFilePDFPath;
        private System.Windows.Forms.TextBox txtInputFilePDFPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgRenameBar;
        private System.Windows.Forms.GroupBox grpNotes;
        private System.Windows.Forms.Label lblNotes1;
        private System.Windows.Forms.Label lblPrcnt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectOutputFilePDFPath;
        private System.Windows.Forms.TextBox txtOutputPDFFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRenameProgress;
        private System.Windows.Forms.GroupBox grpNamingStyle;
        private System.Windows.Forms.RadioButton rbnNameLast;
        private System.Windows.Forms.RadioButton rbnNameFirst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSampleName;
        private System.Windows.Forms.Label lblNotes2;
        private System.Windows.Forms.Label lblInputPDFFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctManual;
        private System.Windows.Forms.Button btnGetChallan;
    }
}
