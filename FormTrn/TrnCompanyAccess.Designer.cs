namespace TDSMAN.FormTrn
{
    partial class TrnCompanyAccess
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnCompanyAccess));
            this.grpSelectUser = new System.Windows.Forms.GroupBox();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.cmbSelectUser = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpViewRights = new System.Windows.Forms.GroupBox();
            this.pnlNewGrid = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtNewSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.chkSelectDeselect = new System.Windows.Forms.CheckBox();
            this.lblCertificateShowSelected = new System.Windows.Forms.Label();
            this.lblNewGridTitle = new System.Windows.Forms.Label();
            this.grdvChildMenus = new System.Windows.Forms.DataGridView();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpSelectUser.SuspendLayout();
            this.grpViewRights.SuspendLayout();
            this.pnlNewGrid.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvChildMenus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(11, 552);
            this.grpSort.Size = new System.Drawing.Size(22, 13);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(190, 13);
            this.BtnCancel.Size = new System.Drawing.Size(12, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(486, 13);
            this.BtnSave.Size = new System.Drawing.Size(78, 23);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(12, 571);
            this.grpSearch.Size = new System.Drawing.Size(13, 11);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(176, 13);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(566, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(219, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(17, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(211, 13);
            this.BtnDelete.Size = new System.Drawing.Size(12, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(202, 13);
            this.BtnSearch.Size = new System.Drawing.Size(10, 23);
            this.BtnSearch.Visible = false;
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
            this.pnlControls.Controls.Add(this.grpViewRights);
            this.pnlControls.Controls.Add(this.panel1);
            this.pnlControls.Controls.Add(this.grpSelectUser);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpSelectUser
            // 
            this.grpSelectUser.Controls.Add(this.lblSelectUser);
            this.grpSelectUser.Controls.Add(this.cmbSelectUser);
            this.grpSelectUser.Location = new System.Drawing.Point(175, 1);
            this.grpSelectUser.Name = "grpSelectUser";
            this.grpSelectUser.Size = new System.Drawing.Size(654, 43);
            this.grpSelectUser.TabIndex = 0;
            this.grpSelectUser.TabStop = false;
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectUser.Location = new System.Drawing.Point(144, 17);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(73, 13);
            this.lblSelectUser.TabIndex = 1;
            this.lblSelectUser.Text = "Select User";
            // 
            // cmbSelectUser
            // 
            this.cmbSelectUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectUser.FormattingEnabled = true;
            this.cmbSelectUser.Location = new System.Drawing.Point(221, 14);
            this.cmbSelectUser.Name = "cmbSelectUser";
            this.cmbSelectUser.Size = new System.Drawing.Size(289, 21);
            this.cmbSelectUser.TabIndex = 0;
            this.cmbSelectUser.SelectedIndexChanged += new System.EventHandler(this.cmbSelectUser_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(2, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 1);
            this.panel1.TabIndex = 1;
            // 
            // grpViewRights
            // 
            this.grpViewRights.Controls.Add(this.pnlNewGrid);
            this.grpViewRights.Location = new System.Drawing.Point(5, 50);
            this.grpViewRights.Name = "grpViewRights";
            this.grpViewRights.Size = new System.Drawing.Size(994, 493);
            this.grpViewRights.TabIndex = 2;
            this.grpViewRights.TabStop = false;
            // 
            // pnlNewGrid
            // 
            this.pnlNewGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNewGrid.Controls.Add(this.pnlSearch);
            this.pnlNewGrid.Controls.Add(this.chkSelectDeselect);
            this.pnlNewGrid.Controls.Add(this.lblCertificateShowSelected);
            this.pnlNewGrid.Controls.Add(this.lblNewGridTitle);
            this.pnlNewGrid.Controls.Add(this.grdvChildMenus);
            this.pnlNewGrid.Location = new System.Drawing.Point(7, 12);
            this.pnlNewGrid.Name = "pnlNewGrid";
            this.pnlNewGrid.Size = new System.Drawing.Size(980, 476);
            this.pnlNewGrid.TabIndex = 199;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtNewSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(682, 440);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(293, 31);
            this.pnlSearch.TabIndex = 196;
            this.pnlSearch.Visible = false;
            // 
            // txtNewSearch
            // 
            this.txtNewSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewSearch.ForeColor = System.Drawing.Color.Black;
            this.txtNewSearch.Location = new System.Drawing.Point(104, 5);
            this.txtNewSearch.Name = "txtNewSearch";
            this.txtNewSearch.Size = new System.Drawing.Size(186, 21);
            this.txtNewSearch.TabIndex = 195;
            this.txtNewSearch.TextChanged += new System.EventHandler(this.txtNewSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(43, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(56, 16);
            this.lblSearch.TabIndex = 194;
            this.lblSearch.Text = "Search";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSelectDeselect
            // 
            this.chkSelectDeselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectDeselect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSelectDeselect.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectDeselect.ForeColor = System.Drawing.Color.Black;
            this.chkSelectDeselect.Location = new System.Drawing.Point(20, 450);
            this.chkSelectDeselect.Name = "chkSelectDeselect";
            this.chkSelectDeselect.Size = new System.Drawing.Size(195, 16);
            this.chkSelectDeselect.TabIndex = 195;
            this.chkSelectDeselect.Text = "Select / Deselect All";
            this.chkSelectDeselect.UseVisualStyleBackColor = true;
            this.chkSelectDeselect.Visible = false;
            this.chkSelectDeselect.CheckedChanged += new System.EventHandler(this.chkSelectDeselect_CheckedChanged);
            // 
            // lblCertificateShowSelected
            // 
            this.lblCertificateShowSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCertificateShowSelected.AutoSize = true;
            this.lblCertificateShowSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCertificateShowSelected.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCertificateShowSelected.ForeColor = System.Drawing.Color.Blue;
            this.lblCertificateShowSelected.Location = new System.Drawing.Point(866, 446);
            this.lblCertificateShowSelected.Name = "lblCertificateShowSelected";
            this.lblCertificateShowSelected.Size = new System.Drawing.Size(92, 15);
            this.lblCertificateShowSelected.TabIndex = 191;
            this.lblCertificateShowSelected.Text = "Show Selected";
            this.lblCertificateShowSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCertificateShowSelected.Visible = false;
            // 
            // lblNewGridTitle
            // 
            this.lblNewGridTitle.AutoSize = true;
            this.lblNewGridTitle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewGridTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblNewGridTitle.Location = new System.Drawing.Point(17, 1);
            this.lblNewGridTitle.Name = "lblNewGridTitle";
            this.lblNewGridTitle.Size = new System.Drawing.Size(107, 17);
            this.lblNewGridTitle.TabIndex = 0;
            this.lblNewGridTitle.Text = "Company : -";
            // 
            // grdvChildMenus
            // 
            this.grdvChildMenus.AllowUserToAddRows = false;
            this.grdvChildMenus.AllowUserToDeleteRows = false;
            this.grdvChildMenus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvChildMenus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdvChildMenus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdvChildMenus.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdvChildMenus.Location = new System.Drawing.Point(2, 19);
            this.grdvChildMenus.Name = "grdvChildMenus";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvChildMenus.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdvChildMenus.RowHeadersWidth = 15;
            this.grdvChildMenus.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grdvChildMenus.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.grdvChildMenus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdvChildMenus.Size = new System.Drawing.Size(973, 416);
            this.grdvChildMenus.TabIndex = 0;
            this.grdvChildMenus.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvChildMenus_CellContentClick);
            this.grdvChildMenus.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvChildMenus_CellValueChanged);
            this.grdvChildMenus.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdvChildMenus_ColumnHeaderMouseClick);
            this.grdvChildMenus.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdvChildMenus_CurrentCellDirtyStateChanged);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(957, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 236;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnCompanyAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 663);
            this.Name = "TrnCompanyAccess";
            this.Load += new System.EventHandler(this.TrnCompanyAccess_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpSelectUser.ResumeLayout(false);
            this.grpSelectUser.PerformLayout();
            this.grpViewRights.ResumeLayout(false);
            this.pnlNewGrid.ResumeLayout(false);
            this.pnlNewGrid.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvChildMenus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelectUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.ComboBox cmbSelectUser;
        private System.Windows.Forms.GroupBox grpViewRights;
        private System.Windows.Forms.Panel pnlNewGrid;
        private System.Windows.Forms.Label lblCertificateShowSelected;
        private System.Windows.Forms.Label lblNewGridTitle;
        private System.Windows.Forms.DataGridView grdvChildMenus;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.CheckBox chkSelectDeselect;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtNewSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}
