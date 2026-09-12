namespace TDSMAN.FormMst
{
    partial class MstDeducteeMasterComplianceCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstDeducteeMasterComplianceCheck));
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.txtDeducteePAN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeducteeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkHigherRateApplicable = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkWhiteListedDeductee = new System.Windows.Forms.CheckBox();
            this.lstDeducteeNameHelp = new System.Windows.Forms.ListBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.txtDeducteePANSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeducteeNameSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvGrid = new DGVControl.DGVControl();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(33, 620);
            this.grpSort.Size = new System.Drawing.Size(10, 17);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(460, 13);
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(377, 13);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.txtDeducteePANSearch);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.txtDeducteeNameSearch);
            this.grpSearch.Controls.Add(this.label4);
            this.grpSearch.Location = new System.Drawing.Point(721, 487);
            this.grpSearch.Size = new System.Drawing.Size(286, 106);
            this.grpSearch.Visible = false;
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.label4, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtDeducteeNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label1, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtDeducteePANSearch, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(294, 13);
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(211, 13);
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.UseMnemonic = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(709, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 605);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(626, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(83, 25);
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(829, 10);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(543, 13);
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctUserManual, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.dgvGrid);
            this.pnlControls.Controls.Add(this.lstDeducteeNameHelp);
            this.pnlControls.Controls.Add(this.chkWhiteListedDeductee);
            this.pnlControls.Controls.Add(this.panel1);
            this.pnlControls.Controls.Add(this.chkHigherRateApplicable);
            this.pnlControls.Controls.Add(this.txtDeducteePAN);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.txtDeducteeName);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.pnlLine1);
            this.pnlControls.Controls.Add(this.lblTitle1);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            // 
            // pnlLine1
            // 
            this.pnlLine1.BackColor = System.Drawing.Color.Black;
            this.pnlLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLine1.Location = new System.Drawing.Point(5, 208);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(994, 3);
            this.pnlLine1.TabIndex = 0;
            // 
            // txtDeducteePAN
            // 
            this.txtDeducteePAN.BackColor = System.Drawing.Color.White;
            this.txtDeducteePAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteePAN.Location = new System.Drawing.Point(357, 7);
            this.txtDeducteePAN.MaxLength = 10;
            this.txtDeducteePAN.Name = "txtDeducteePAN";
            this.txtDeducteePAN.Size = new System.Drawing.Size(126, 21);
            this.txtDeducteePAN.TabIndex = 3;
            this.txtDeducteePAN.TextChanged += new System.EventHandler(this.txtDeducteePAN_TextChanged);
            this.txtDeducteePAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeducteePAN_KeyDown);
            this.txtDeducteePAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeducteePAN_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(263, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Deductee PAN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDeducteeName
            // 
            this.txtDeducteeName.BackColor = System.Drawing.Color.White;
            this.txtDeducteeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeName.Location = new System.Drawing.Point(357, 32);
            this.txtDeducteeName.MaxLength = 75;
            this.txtDeducteeName.Name = "txtDeducteeName";
            this.txtDeducteeName.Size = new System.Drawing.Size(363, 21);
            this.txtDeducteeName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(256, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Deductee Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkHigherRateApplicable
            // 
            this.chkHigherRateApplicable.AutoSize = true;
            this.chkHigherRateApplicable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkHigherRateApplicable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHigherRateApplicable.Location = new System.Drawing.Point(357, 59);
            this.chkHigherRateApplicable.Name = "chkHigherRateApplicable";
            this.chkHigherRateApplicable.Size = new System.Drawing.Size(387, 17);
            this.chkHigherRateApplicable.TabIndex = 185;
            this.chkHigherRateApplicable.Text = "Person specified u/s 206AB & 206CCA ( higher rate applicable )";
            this.chkHigherRateApplicable.UseMnemonic = false;
            this.chkHigherRateApplicable.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(248, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 1);
            this.panel1.TabIndex = 186;
            // 
            // chkWhiteListedDeductee
            // 
            this.chkWhiteListedDeductee.AutoSize = true;
            this.chkWhiteListedDeductee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkWhiteListedDeductee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWhiteListedDeductee.Location = new System.Drawing.Point(793, 187);
            this.chkWhiteListedDeductee.Name = "chkWhiteListedDeductee";
            this.chkWhiteListedDeductee.Size = new System.Drawing.Size(205, 17);
            this.chkWhiteListedDeductee.TabIndex = 187;
            this.chkWhiteListedDeductee.Text = "Show White Listed Deductee(s)";
            this.chkWhiteListedDeductee.UseMnemonic = false;
            this.chkWhiteListedDeductee.UseVisualStyleBackColor = true;
            this.chkWhiteListedDeductee.CheckedChanged += new System.EventHandler(this.chkWhiteListedDeductee_CheckedChanged);
            // 
            // lstDeducteeNameHelp
            // 
            this.lstDeducteeNameHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeNameHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeNameHelp.FormattingEnabled = true;
            this.lstDeducteeNameHelp.ItemHeight = 16;
            this.lstDeducteeNameHelp.Location = new System.Drawing.Point(357, 30);
            this.lstDeducteeNameHelp.Name = "lstDeducteeNameHelp";
            this.lstDeducteeNameHelp.Size = new System.Drawing.Size(518, 100);
            this.lstDeducteeNameHelp.TabIndex = 188;
            this.lstDeducteeNameHelp.Visible = false;
            this.lstDeducteeNameHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeNameHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle1.Location = new System.Drawing.Point(248, 86);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(507, 35);
            this.lblTitle1.TabIndex = 189;
            this.lblTitle1.Text = "Only existing deductee can be marked for Higher Rate. To mark any new deductee fi" +
    "rst enter in deductee master then mark here.";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle1.UseMnemonic = false;
            // 
            // txtDeducteePANSearch
            // 
            this.txtDeducteePANSearch.BackColor = System.Drawing.Color.White;
            this.txtDeducteePANSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePANSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteePANSearch.Location = new System.Drawing.Point(65, 16);
            this.txtDeducteePANSearch.MaxLength = 10;
            this.txtDeducteePANSearch.Name = "txtDeducteePANSearch";
            this.txtDeducteePANSearch.Size = new System.Drawing.Size(126, 21);
            this.txtDeducteePANSearch.TabIndex = 14;
            this.txtDeducteePANSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "PAN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDeducteeNameSearch
            // 
            this.txtDeducteeNameSearch.BackColor = System.Drawing.Color.White;
            this.txtDeducteeNameSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteeNameSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeducteeNameSearch.Location = new System.Drawing.Point(65, 41);
            this.txtDeducteeNameSearch.MaxLength = 75;
            this.txtDeducteeNameSearch.Name = "txtDeducteeNameSearch";
            this.txtDeducteeNameSearch.Size = new System.Drawing.Size(215, 21);
            this.txtDeducteeNameSearch.TabIndex = 16;
            this.txtDeducteeNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvGrid.Location = new System.Drawing.Point(5, 212);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.RowHeadersWidth = 20;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(993, 328);
            this.dgvGrid.TabIndex = 190;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrid_CellContentClick);
            this.dgvGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrid_CellValueChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.Location = new System.Drawing.Point(32, 584);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(98, 18);
            this.chkSelectAll.TabIndex = 191;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseMnemonic = false;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(957, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 229;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // MstDeducteeMasterComplianceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Controls.Add(this.chkSelectAll);
            this.Name = "MstDeducteeMasterComplianceCheck";
            this.Activated += new System.EventHandler(this.MstDeducteeMasterComplianceCheck_Activated);
            this.Load += new System.EventHandler(this.MstDeducteeMasterComplianceCheck_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.chkSelectAll, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLine1;
        private System.Windows.Forms.TextBox txtDeducteePAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeducteeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHigherRateApplicable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkWhiteListedDeductee;
        private System.Windows.Forms.ListBox lstDeducteeNameHelp;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.TextBox txtDeducteePANSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeducteeNameSearch;
        private System.Windows.Forms.Label label4;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
