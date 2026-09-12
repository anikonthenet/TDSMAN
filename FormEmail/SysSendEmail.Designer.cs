namespace TDSMAN.FormEmail
{
    partial class SysSendEmail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysSendEmail));
            this.grpBasicInformation = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.grpPartyDetails = new System.Windows.Forms.GroupBox();
            this.grdvParty = new System.Windows.Forms.DataGridView();
            this.lblEmailCounter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchParty = new System.Windows.Forms.TextBox();
            this.chkSelectAllParty = new System.Windows.Forms.CheckBox();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.lblLoadingButtonDesc = new System.Windows.Forms.Label();
            this.lnkViewFormat = new System.Windows.Forms.LinkLabel();
            this.lnkViewSetup = new System.Windows.Forms.LinkLabel();
            this.cmbEmailFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEmailSetup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadParty = new System.Windows.Forms.Button();
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.lblFormNo = new System.Windows.Forms.Label();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.lblQtr = new System.Windows.Forms.Label();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCertificateID = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txtFromEmailIDSearch = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtEmailDescriptionSearch = new System.Windows.Forms.TextBox();
            this.tmrGridRefresh = new System.Windows.Forms.Timer(this.components);
            this.bgwEmailing = new System.ComponentModel.BackgroundWorker();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.btnViewSentEmail = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpMainScreen = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBasicInformation.SuspendLayout();
            this.grpPartyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.grpMainScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 657);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(502, 13);
            this.BtnCancel.Size = new System.Drawing.Size(83, 23);
            this.BtnCancel.Text = "&Close";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(395, 13);
            this.BtnSave.Size = new System.Drawing.Size(106, 23);
            this.BtnSave.Text = "&Send Email";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.label114);
            this.grpSearch.Controls.Add(this.txtFromEmailIDSearch);
            this.grpSearch.Controls.Add(this.label117);
            this.grpSearch.Controls.Add(this.txtEmailDescriptionSearch);
            this.grpSearch.Location = new System.Drawing.Point(605, 637);
            this.grpSearch.Size = new System.Drawing.Size(320, 109);
            this.grpSearch.Controls.SetChildIndex(this.txtEmailDescriptionSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label117, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtFromEmailIDSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label114, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchCancel.Location = new System.Drawing.Point(236, 77);
            this.BtnSearchCancel.TabIndex = 4;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.BackColor = System.Drawing.Color.Lavender;
            this.BtnSearchOK.Location = new System.Drawing.Point(165, 77);
            this.BtnSearchOK.TabIndex = 3;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.BackColor = System.Drawing.Color.Lavender;
            this.BtnEdit.Location = new System.Drawing.Point(277, 16);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Text = "&View Sent Email";
            this.BtnEdit.Visible = false;
            this.BtnEdit.Click += new System.EventHandler(this.btnExportViewSentEmail_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(228, 13);
            this.BtnExit.Size = new System.Drawing.Size(14, 23);
            this.BtnExit.Visible = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(212, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(15, 23);
            this.BtnRefresh.Visible = false;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(200, 13);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(187, 13);
            this.BtnSearch.Size = new System.Drawing.Size(10, 23);
            this.BtnSearch.Visible = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.btnViewSentEmail);
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
            this.grpButton.Controls.SetChildIndex(this.btnViewSentEmail, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBasicInformation);
            this.pnlControls.Location = new System.Drawing.Point(9, 46);
            this.pnlControls.Size = new System.Drawing.Size(1003, 521);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ViewGrid.Location = new System.Drawing.Point(9, 48);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            // 
            // grpBasicInformation
            // 
            this.grpBasicInformation.Controls.Add(this.lnkSearchByTAN);
            this.grpBasicInformation.Controls.Add(this.grpPartyDetails);
            this.grpBasicInformation.Controls.Add(this.pnlLine);
            this.grpBasicInformation.Controls.Add(this.lblLoadingButtonDesc);
            this.grpBasicInformation.Controls.Add(this.lnkViewFormat);
            this.grpBasicInformation.Controls.Add(this.lnkViewSetup);
            this.grpBasicInformation.Controls.Add(this.cmbEmailFormat);
            this.grpBasicInformation.Controls.Add(this.label5);
            this.grpBasicInformation.Controls.Add(this.cmbEmailSetup);
            this.grpBasicInformation.Controls.Add(this.label4);
            this.grpBasicInformation.Controls.Add(this.btnLoadParty);
            this.grpBasicInformation.Controls.Add(this.btnChoosePath);
            this.grpBasicInformation.Controls.Add(this.label2);
            this.grpBasicInformation.Controls.Add(this.txtPath);
            this.grpBasicInformation.Controls.Add(this.cmbFormNo);
            this.grpBasicInformation.Controls.Add(this.lblFormNo);
            this.grpBasicInformation.Controls.Add(this.cmbQtr);
            this.grpBasicInformation.Controls.Add(this.lblQtr);
            this.grpBasicInformation.Controls.Add(this.cmbCompanyName);
            this.grpBasicInformation.Controls.Add(this.label3);
            this.grpBasicInformation.Controls.Add(this.cmbFAYear);
            this.grpBasicInformation.Controls.Add(this.label1);
            this.grpBasicInformation.Controls.Add(this.cmbCertificateID);
            this.grpBasicInformation.Controls.Add(this.label7);
            this.grpBasicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicInformation.Location = new System.Drawing.Point(6, 3);
            this.grpBasicInformation.Name = "grpBasicInformation";
            this.grpBasicInformation.Size = new System.Drawing.Size(991, 513);
            this.grpBasicInformation.TabIndex = 2;
            this.grpBasicInformation.TabStop = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(862, 24);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(32, 13);
            this.lnkSearchByTAN.TabIndex = 233;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // grpPartyDetails
            // 
            this.grpPartyDetails.Controls.Add(this.grdvParty);
            this.grpPartyDetails.Controls.Add(this.lblEmailCounter);
            this.grpPartyDetails.Controls.Add(this.label6);
            this.grpPartyDetails.Controls.Add(this.txtSearchParty);
            this.grpPartyDetails.Controls.Add(this.chkSelectAllParty);
            this.grpPartyDetails.Location = new System.Drawing.Point(6, 163);
            this.grpPartyDetails.Name = "grpPartyDetails";
            this.grpPartyDetails.Size = new System.Drawing.Size(980, 344);
            this.grpPartyDetails.TabIndex = 232;
            this.grpPartyDetails.TabStop = false;
            // 
            // grdvParty
            // 
            this.grdvParty.AllowUserToAddRows = false;
            this.grdvParty.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvParty.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdvParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvParty.Location = new System.Drawing.Point(6, 19);
            this.grdvParty.Name = "grdvParty";
            this.grdvParty.RowHeadersWidth = 15;
            this.grdvParty.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grdvParty.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.grdvParty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdvParty.Size = new System.Drawing.Size(968, 294);
            this.grdvParty.TabIndex = 243;
            this.grdvParty.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvParty_CellClick);
            this.grdvParty.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvParty_CellValueChanged);
            this.grdvParty.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdvParty_CurrentCellDirtyStateChanged);
            // 
            // lblEmailCounter
            // 
            this.lblEmailCounter.AutoSize = true;
            this.lblEmailCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailCounter.ForeColor = System.Drawing.Color.Blue;
            this.lblEmailCounter.Location = new System.Drawing.Point(97, 321);
            this.lblEmailCounter.Name = "lblEmailCounter";
            this.lblEmailCounter.Size = new System.Drawing.Size(47, 13);
            this.lblEmailCounter.TabIndex = 239;
            this.lblEmailCounter.Text = "Search";
            this.lblEmailCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblEmailCounter.Visible = false;
            this.lblEmailCounter.Click += new System.EventHandler(this.lblEmailCounter_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(568, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 238;
            this.label6.Text = "Search";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label6.Visible = false;
            // 
            // txtSearchParty
            // 
            this.txtSearchParty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchParty.Location = new System.Drawing.Point(620, 317);
            this.txtSearchParty.Name = "txtSearchParty";
            this.txtSearchParty.Size = new System.Drawing.Size(195, 20);
            this.txtSearchParty.TabIndex = 237;
            this.txtSearchParty.Visible = false;
            this.txtSearchParty.TextChanged += new System.EventHandler(this.txtSearchParty_TextChanged);
            // 
            // chkSelectAllParty
            // 
            this.chkSelectAllParty.AutoSize = true;
            this.chkSelectAllParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSelectAllParty.Location = new System.Drawing.Point(10, 320);
            this.chkSelectAllParty.Name = "chkSelectAllParty";
            this.chkSelectAllParty.Size = new System.Drawing.Size(80, 17);
            this.chkSelectAllParty.TabIndex = 236;
            this.chkSelectAllParty.Text = "Select All";
            this.chkSelectAllParty.UseVisualStyleBackColor = true;
            this.chkSelectAllParty.CheckedChanged += new System.EventHandler(this.chkSelectAllParty_CheckedChanged);
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.Black;
            this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLine.Location = new System.Drawing.Point(106, 98);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(778, 2);
            this.pnlLine.TabIndex = 231;
            // 
            // lblLoadingButtonDesc
            // 
            this.lblLoadingButtonDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingButtonDesc.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadingButtonDesc.Location = new System.Drawing.Point(245, 127);
            this.lblLoadingButtonDesc.Name = "lblLoadingButtonDesc";
            this.lblLoadingButtonDesc.Size = new System.Drawing.Size(577, 26);
            this.lblLoadingButtonDesc.TabIndex = 229;
            this.lblLoadingButtonDesc.Text = "This will match the file names (PAN) with the PANs of the selected return and wil" +
    "l load the Deductee/Employee(s) having email ids...";
            // 
            // lnkViewFormat
            // 
            this.lnkViewFormat.AutoSize = true;
            this.lnkViewFormat.Location = new System.Drawing.Point(821, 76);
            this.lnkViewFormat.Name = "lnkViewFormat";
            this.lnkViewFormat.Size = new System.Drawing.Size(34, 13);
            this.lnkViewFormat.TabIndex = 225;
            this.lnkViewFormat.TabStop = true;
            this.lnkViewFormat.Text = "View";
            this.lnkViewFormat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewFormat_LinkClicked);
            // 
            // lnkViewSetup
            // 
            this.lnkViewSetup.AutoSize = true;
            this.lnkViewSetup.Location = new System.Drawing.Point(821, 50);
            this.lnkViewSetup.Name = "lnkViewSetup";
            this.lnkViewSetup.Size = new System.Drawing.Size(34, 13);
            this.lnkViewSetup.TabIndex = 224;
            this.lnkViewSetup.TabStop = true;
            this.lnkViewSetup.Text = "View";
            this.lnkViewSetup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewSetup_LinkClicked);
            // 
            // cmbEmailFormat
            // 
            this.cmbEmailFormat.BackColor = System.Drawing.Color.White;
            this.cmbEmailFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmailFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmailFormat.FormattingEnabled = true;
            this.cmbEmailFormat.Location = new System.Drawing.Point(535, 71);
            this.cmbEmailFormat.Name = "cmbEmailFormat";
            this.cmbEmailFormat.Size = new System.Drawing.Size(281, 23);
            this.cmbEmailFormat.TabIndex = 222;
            this.cmbEmailFormat.SelectedIndexChanged += new System.EventHandler(this.cmbEmailFormat_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(444, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 223;
            this.label5.Text = "Select Format";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbEmailSetup
            // 
            this.cmbEmailSetup.BackColor = System.Drawing.Color.White;
            this.cmbEmailSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmailSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmailSetup.FormattingEnabled = true;
            this.cmbEmailSetup.Location = new System.Drawing.Point(535, 45);
            this.cmbEmailSetup.Name = "cmbEmailSetup";
            this.cmbEmailSetup.Size = new System.Drawing.Size(281, 23);
            this.cmbEmailSetup.TabIndex = 220;
            this.cmbEmailSetup.SelectedIndexChanged += new System.EventHandler(this.cmbEmailSetup_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(449, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 221;
            this.label4.Text = "Select Setup";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnLoadParty
            // 
            this.btnLoadParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadParty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLoadParty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadParty.Location = new System.Drawing.Point(187, 127);
            this.btnLoadParty.Name = "btnLoadParty";
            this.btnLoadParty.Size = new System.Drawing.Size(57, 23);
            this.btnLoadParty.TabIndex = 212;
            this.btnLoadParty.Text = "Load";
            this.btnLoadParty.UseVisualStyleBackColor = false;
            this.btnLoadParty.Click += new System.EventHandler(this.btnLoadParty_Click);
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.BackColor = System.Drawing.Color.Lavender;
            this.btnChoosePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoosePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoosePath.ForeColor = System.Drawing.Color.Black;
            this.btnChoosePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChoosePath.Location = new System.Drawing.Point(818, 103);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(37, 23);
            this.btnChoosePath.TabIndex = 202;
            this.btnChoosePath.Text = "...";
            this.btnChoosePath.UseVisualStyleBackColor = false;
            this.btnChoosePath.Click += new System.EventHandler(this.btnChoosePath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 201;
            this.label2.Text = "Certificate folder";
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(271, 104);
            this.txtPath.MaxLength = 75;
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(545, 20);
            this.txtPath.TabIndex = 200;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(330, 71);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(98, 23);
            this.cmbFormNo.TabIndex = 198;
            // 
            // lblFormNo
            // 
            this.lblFormNo.AutoSize = true;
            this.lblFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormNo.Location = new System.Drawing.Point(267, 76);
            this.lblFormNo.Name = "lblFormNo";
            this.lblFormNo.Size = new System.Drawing.Size(58, 13);
            this.lblFormNo.TabIndex = 199;
            this.lblFormNo.Text = "Form No.";
            this.lblFormNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbQtr
            // 
            this.cmbQtr.BackColor = System.Drawing.Color.White;
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(167, 71);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(94, 23);
            this.cmbQtr.TabIndex = 196;
            // 
            // lblQtr
            // 
            this.lblQtr.AutoSize = true;
            this.lblQtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtr.Location = new System.Drawing.Point(112, 76);
            this.lblQtr.Name = "lblQtr";
            this.lblQtr.Size = new System.Drawing.Size(49, 13);
            this.lblQtr.TabIndex = 197;
            this.lblQtr.Text = "Quarter";
            this.lblQtr.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.BackColor = System.Drawing.Color.White;
            this.cmbCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(535, 19);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(321, 23);
            this.cmbCompanyName.TabIndex = 192;
            this.cmbCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbCompanyName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(435, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 193;
            this.label3.Text = "Company Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.BackColor = System.Drawing.Color.White;
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(167, 45);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(260, 23);
            this.cmbFAYear.TabIndex = 190;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 191;
            this.label1.Text = "Tax Year";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbCertificateID
            // 
            this.cmbCertificateID.BackColor = System.Drawing.Color.White;
            this.cmbCertificateID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCertificateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCertificateID.FormattingEnabled = true;
            this.cmbCertificateID.Location = new System.Drawing.Point(167, 19);
            this.cmbCertificateID.Name = "cmbCertificateID";
            this.cmbCertificateID.Size = new System.Drawing.Size(260, 23);
            this.cmbCertificateID.TabIndex = 188;
            this.cmbCertificateID.SelectedIndexChanged += new System.EventHandler(this.cmbCertificateID_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(81, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 189;
            this.label7.Text = "Certificate Id";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label114.Location = new System.Drawing.Point(15, 50);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(72, 13);
            this.label114.TabIndex = 159;
            this.label114.Text = "From Email ID";
            // 
            // txtFromEmailIDSearch
            // 
            this.txtFromEmailIDSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromEmailIDSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtFromEmailIDSearch.Location = new System.Drawing.Point(114, 47);
            this.txtFromEmailIDSearch.Name = "txtFromEmailIDSearch";
            this.txtFromEmailIDSearch.Size = new System.Drawing.Size(192, 20);
            this.txtFromEmailIDSearch.TabIndex = 1;
            this.txtFromEmailIDSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label117.Location = new System.Drawing.Point(15, 25);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(60, 13);
            this.label117.TabIndex = 157;
            this.label117.Text = "Description";
            // 
            // txtEmailDescriptionSearch
            // 
            this.txtEmailDescriptionSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailDescriptionSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmailDescriptionSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtEmailDescriptionSearch.Location = new System.Drawing.Point(114, 22);
            this.txtEmailDescriptionSearch.MaxLength = 10;
            this.txtEmailDescriptionSearch.Name = "txtEmailDescriptionSearch";
            this.txtEmailDescriptionSearch.Size = new System.Drawing.Size(192, 20);
            this.txtEmailDescriptionSearch.TabIndex = 0;
            this.txtEmailDescriptionSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // tmrGridRefresh
            // 
            this.tmrGridRefresh.Interval = 5000;
            // 
            // bgwEmailing
            // 
            this.bgwEmailing.WorkerSupportsCancellation = true;
            this.bgwEmailing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwEmailing_DoWork);
            this.bgwEmailing.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwEmailing_ProgressChanged);
            this.bgwEmailing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwEmailing_RunWorkerCompleted);
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(80, 32);
            this.pctVideoDemo.TabIndex = 207;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // btnViewSentEmail
            // 
            this.btnViewSentEmail.BackColor = System.Drawing.Color.Lavender;
            this.btnViewSentEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewSentEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewSentEmail.ForeColor = System.Drawing.Color.Black;
            this.btnViewSentEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnViewSentEmail.Location = new System.Drawing.Point(586, 13);
            this.btnViewSentEmail.Name = "btnViewSentEmail";
            this.btnViewSentEmail.Size = new System.Drawing.Size(107, 23);
            this.btnViewSentEmail.TabIndex = 208;
            this.btnViewSentEmail.Text = "&View Sent Email";
            this.toolTip1.SetToolTip(this.btnViewSentEmail, "Export to Excel");
            this.btnViewSentEmail.UseVisualStyleBackColor = false;
            this.btnViewSentEmail.Click += new System.EventHandler(this.btnExportViewSentEmail_Click);
            // 
            // grpMainScreen
            // 
            this.grpMainScreen.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grpMainScreen.Controls.Add(this.label17);
            this.grpMainScreen.Controls.Add(this.label14);
            this.grpMainScreen.Controls.Add(this.label15);
            this.grpMainScreen.Controls.Add(this.label16);
            this.grpMainScreen.Controls.Add(this.panel1);
            this.grpMainScreen.Controls.Add(this.label12);
            this.grpMainScreen.Controls.Add(this.label11);
            this.grpMainScreen.Controls.Add(this.label10);
            this.grpMainScreen.Controls.Add(this.label9);
            this.grpMainScreen.Controls.Add(this.label8);
            this.grpMainScreen.Controls.Add(this.btnContinue);
            this.grpMainScreen.Location = new System.Drawing.Point(52, 152);
            this.grpMainScreen.Name = "grpMainScreen";
            this.grpMainScreen.Size = new System.Drawing.Size(919, 355);
            this.grpMainScreen.TabIndex = 50;
            this.grpMainScreen.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(139, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 16);
            this.label17.TabIndex = 24;
            this.label17.Text = "IMPORTANT";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(181, 244);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(669, 48);
            this.label14.TabIndex = 22;
            this.label14.Text = resources.GetString("label14.Text");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(181, 219);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(550, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "a)      For the selected Company (TAN), the email parameters need to be setup.";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(139, 186);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 16);
            this.label16.TabIndex = 20;
            this.label16.Text = "Setup & Email Body :";
            this.label16.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(28, 169);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 2);
            this.panel1.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(181, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(614, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "3.      The PDF file of the TDS / TCS Certificate of the PAN should be present in" +
    " the folder";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(181, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(447, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "2.      The data as available in the Return that has been selected";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(181, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(311, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "1.      Email ID must be available for the PAN";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(139, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(632, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "The list for sending emails of certificates will be automatically determined by t" +
    "he following :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(139, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(548, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "To send TDS / TCS Certificates over email, the following are the pre-requisite :";
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnContinue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnContinue.Location = new System.Drawing.Point(831, 320);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(70, 23);
            this.btnContinue.TabIndex = 13;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // SysSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.grpMainScreen);
            this.Name = "SysSendEmail";
            this.Load += new System.EventHandler(this.SysEmailSetup_Load);
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
            this.Controls.SetChildIndex(this.grpMainScreen, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBasicInformation.ResumeLayout(false);
            this.grpBasicInformation.PerformLayout();
            this.grpPartyDetails.ResumeLayout(false);
            this.grpPartyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.grpMainScreen.ResumeLayout(false);
            this.grpMainScreen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInformation;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtFromEmailIDSearch;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtEmailDescriptionSearch;
        private System.Windows.Forms.Timer tmrGridRefresh;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCertificateID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.Label lblQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label lblFormNo;
        internal System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        public System.Windows.Forms.Button btnLoadParty;
        private System.Windows.Forms.ComboBox cmbEmailFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEmailSetup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkViewSetup;
        private System.Windows.Forms.LinkLabel lnkViewFormat;
        private System.Windows.Forms.Label lblLoadingButtonDesc;
        private System.ComponentModel.BackgroundWorker bgwEmailing;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.Panel pnlLine;
        internal System.Windows.Forms.Button btnViewSentEmail;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grpMainScreen;
        public System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox grpPartyDetails;
        private System.Windows.Forms.Label lblEmailCounter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchParty;
        private System.Windows.Forms.CheckBox chkSelectAllParty;
        private System.Windows.Forms.DataGridView grdvParty;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
    }
}
