namespace TDSMAN.FormEmail
{
    partial class SysEmailFormat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysEmailFormat));
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.lnkViewHTML = new System.Windows.Forms.LinkLabel();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkHtmlBody = new System.Windows.Forms.CheckBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmailDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCertificateID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkInactive = new System.Windows.Forms.CheckBox();
            this.label114 = new System.Windows.Forms.Label();
            this.txtDescriptionSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtCompanySearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCertificateSearch = new System.Windows.Forms.ComboBox();
            this.tmrGridRefresh = new System.Windows.Forms.Timer(this.components);
            this.dgvGrid = new DGVControl.DGVControl();
            this.pctManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 615);
            this.grpSort.Size = new System.Drawing.Size(280, 4);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.cmbCertificateSearch);
            this.grpSearch.Controls.Add(this.label9);
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtDescriptionSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Controls.Add(this.txtCompanySearch);
            this.grpSearch.Location = new System.Drawing.Point(614, 444);
            this.grpSearch.Size = new System.Drawing.Size(319, 147);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtCompanySearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtDescriptionSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.label9, 0);
            this.grpSearch.Controls.SetChildIndex(this.cmbCertificateSearch, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(229, 109);
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(158, 109);
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctManual);
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
            this.grpButton.Controls.SetChildIndex(this.pctManual, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBasicInformation);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 42);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 15);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseClick);
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.lnkViewHTML);
            this.grpBasicInformation.Controls.Add(this.txtEmailSubject);
            this.grpBasicInformation.Controls.Add(this.label3);
            this.grpBasicInformation.Controls.Add(this.chkHtmlBody);
            this.grpBasicInformation.Controls.Add(this.txtBody);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Controls.Add(this.txtEmailDescription);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Controls.Add(this.cmbCertificateID);
            this.grpBasicInformation.Controls.Add(this.label1);
            this.grpBasicInformation.Controls.Add(this.cmbCompanyName);
            this.grpBasicInformation.Controls.Add(this.label7);
            this.grpBasicInformation.Controls.Add(this.chkInactive);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(150, 19);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(702, 453);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // lnkViewHTML
            // 
            this.lnkViewHTML.AutoSize = true;
            this.lnkViewHTML.Location = new System.Drawing.Point(216, 420);
            this.lnkViewHTML.Name = "lnkViewHTML";
            this.lnkViewHTML.Size = new System.Drawing.Size(72, 13);
            this.lnkViewHTML.TabIndex = 215;
            this.lnkViewHTML.TabStop = true;
            this.lnkViewHTML.Text = "View HTML";
            this.lnkViewHTML.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewHTML_LinkClicked);
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailSubject.Location = new System.Drawing.Point(120, 92);
            this.txtEmailSubject.MaxLength = 255;
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.Size = new System.Drawing.Size(448, 21);
            this.txtEmailSubject.TabIndex = 214;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(31, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 213;
            this.label3.Text = "Email Subject";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkHtmlBody
            // 
            this.chkHtmlBody.AutoSize = true;
            this.chkHtmlBody.Location = new System.Drawing.Point(120, 418);
            this.chkHtmlBody.Name = "chkHtmlBody";
            this.chkHtmlBody.Size = new System.Drawing.Size(92, 17);
            this.chkHtmlBody.TabIndex = 192;
            this.chkHtmlBody.TabStop = false;
            this.chkHtmlBody.Text = "HTML Body";
            this.chkHtmlBody.UseVisualStyleBackColor = true;
            // 
            // txtBody
            // 
            this.txtBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(120, 116);
            this.txtBody.MaxLength = 0;
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBody.Size = new System.Drawing.Size(543, 296);
            this.txtBody.TabIndex = 191;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(80, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 190;
            this.label2.Text = "Body";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailDescription
            // 
            this.txtEmailDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmailDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailDescription.Location = new System.Drawing.Point(120, 68);
            this.txtEmailDescription.MaxLength = 255;
            this.txtEmailDescription.Name = "txtEmailDescription";
            this.txtEmailDescription.Size = new System.Drawing.Size(448, 21);
            this.txtEmailDescription.TabIndex = 189;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(44, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 188;
            this.label4.Text = "Description";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbCertificateID
            // 
            this.cmbCertificateID.BackColor = System.Drawing.Color.White;
            this.cmbCertificateID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCertificateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCertificateID.FormattingEnabled = true;
            this.cmbCertificateID.Location = new System.Drawing.Point(120, 42);
            this.cmbCertificateID.Name = "cmbCertificateID";
            this.cmbCertificateID.Size = new System.Drawing.Size(448, 23);
            this.cmbCertificateID.TabIndex = 186;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 187;
            this.label1.Text = "Certificate ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.BackColor = System.Drawing.Color.White;
            this.cmbCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(120, 15);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(448, 23);
            this.cmbCompanyName.TabIndex = 184;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 185;
            this.label7.Text = "Company Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkInactive
            // 
            this.chkInactive.AutoSize = true;
            this.chkInactive.Location = new System.Drawing.Point(591, 418);
            this.chkInactive.Name = "chkInactive";
            this.chkInactive.Size = new System.Drawing.Size(72, 17);
            this.chkInactive.TabIndex = 183;
            this.chkInactive.Text = "Inactive";
            this.chkInactive.UseVisualStyleBackColor = true;
            this.chkInactive.Visible = false;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(15, 51);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(84, 15);
            this.label114.TabIndex = 159;
            this.label114.Text = "Description";
            // 
            // txtDescriptionSearch
            // 
            this.txtDescriptionSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescriptionSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescriptionSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescriptionSearch.Location = new System.Drawing.Point(100, 47);
            this.txtDescriptionSearch.Name = "txtDescriptionSearch";
            this.txtDescriptionSearch.Size = new System.Drawing.Size(211, 21);
            this.txtDescriptionSearch.TabIndex = 1;
            this.txtDescriptionSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(15, 26);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(56, 15);
            this.label117.TabIndex = 157;
            this.label117.Text = "Company";
            // 
            // txtCompanySearch
            // 
            this.txtCompanySearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanySearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompanySearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanySearch.Location = new System.Drawing.Point(100, 22);
            this.txtCompanySearch.MaxLength = 10;
            this.txtCompanySearch.Name = "txtCompanySearch";
            this.txtCompanySearch.Size = new System.Drawing.Size(113, 21);
            this.txtCompanySearch.TabIndex = 0;
            this.txtCompanySearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(15, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 15);
            this.label9.TabIndex = 161;
            this.label9.Text = "Certificate";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbCertificateSearch
            // 
            this.cmbCertificateSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCertificateSearch.FormattingEnabled = true;
            this.cmbCertificateSearch.Location = new System.Drawing.Point(101, 72);
            this.cmbCertificateSearch.Name = "cmbCertificateSearch";
            this.cmbCertificateSearch.Size = new System.Drawing.Size(210, 23);
            this.cmbCertificateSearch.TabIndex = 2;
            // 
            // tmrGridRefresh
            // 
            this.tmrGridRefresh.Interval = 5000;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvGrid.Location = new System.Drawing.Point(9, 40);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGrid.RowHeadersWidth = 20;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(1003, 555);
            this.dgvGrid.TabIndex = 193;
            this.dgvGrid.TabStop = false;
            this.dgvGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.dgvGrid.Click += new System.EventHandler(this.dgvGrid_Click);
            this.dgvGrid.DoubleClick += new System.EventHandler(this.dgvGrid_DoubleClick);
            this.dgvGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvGrid_KeyDown);
            this.dgvGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvGrid_MouseClick);
            // 
            // pctManual
            // 
            this.pctManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctManual.Image = ((System.Drawing.Image)(resources.GetObject("pctManual.Image")));
            this.pctManual.Location = new System.Drawing.Point(953, 10);
            this.pctManual.Name = "pctManual";
            this.pctManual.Size = new System.Drawing.Size(39, 32);
            this.pctManual.TabIndex = 223;
            this.pctManual.TabStop = false;
            this.pctManual.Tag = "User Manual";
            this.pctManual.Click += new System.EventHandler(this.pctManual_Click);
            // 
            // SysEmailFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.dgvGrid);
            this.Name = "SysEmailFormat";
            this.Load += new System.EventHandler(this.SysEmailFormat_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.dgvGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtDescriptionSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtCompanySearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCertificateSearch;
        private System.Windows.Forms.Timer tmrGridRefresh;
        private System.Windows.Forms.CheckBox chkInactive;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCertificateID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmailDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHtmlBody;
        private System.Windows.Forms.TextBox txtEmailSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkViewHTML;
        private DGVControl.DGVControl dgvGrid;
        private System.Windows.Forms.PictureBox pctManual;
    }
}
