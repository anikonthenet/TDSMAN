namespace TDSMAN.FormMst
{
    partial class MstEmployeeGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstEmployeeGroup));
            this.grpEmployeeList = new System.Windows.Forms.GroupBox();
            this.grdvEmployees = new System.Windows.Forms.DataGridView();
            this.chkSelectDeselect = new System.Windows.Forms.CheckBox();
            this.lblSelectionValue = new System.Windows.Forms.Label();
            this.pnlVertical2 = new System.Windows.Forms.Panel();
            this.pnlVertical1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearchAll = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpEmployeeGroup = new System.Windows.Forms.GroupBox();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnTagUsingExcel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnEditGroup = new System.Windows.Forms.Button();
            this.btnSaveGroup = new System.Windows.Forms.Button();
            this.txtNewEmployeeGroup = new System.Windows.Forms.TextBox();
            this.cmbEmployeeGroups = new System.Windows.Forms.ComboBox();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.grpTagUsingExcel = new System.Windows.Forms.GroupBox();
            this.grpStepsExcel = new System.Windows.Forms.GroupBox();
            this.grpEmployeeGrid = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgcViewEmployeeExcel = new DGControl.DGControl();
            this.btnTag = new System.Windows.Forms.Button();
            this.grpStep2 = new System.Windows.Forms.GroupBox();
            this.lblValidationMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExcelValidate = new System.Windows.Forms.Button();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.grpTagExcelStep1 = new System.Windows.Forms.GroupBox();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.btnExportData = new System.Windows.Forms.Button();
            this.btnCloseTaggingUsingExcel = new System.Windows.Forms.Button();
            this.lblHeaderText = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpEmployeeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvEmployees)).BeginInit();
            this.grpEmployeeGroup.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.grpCompany.SuspendLayout();
            this.grpTagUsingExcel.SuspendLayout();
            this.grpStepsExcel.SuspendLayout();
            this.grpEmployeeGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewEmployeeExcel)).BeginInit();
            this.grpStep2.SuspendLayout();
            this.grpTagExcelStep1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(19, 552);
            this.grpSort.Size = new System.Drawing.Size(15, 112);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(181, 13);
            this.BtnCancel.Size = new System.Drawing.Size(10, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(0, 548);
            this.grpSearch.Size = new System.Drawing.Size(16, 112);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(173, 13);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(13, 23);
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
            this.BtnRefresh.Location = new System.Drawing.Point(211, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(199, 13);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(190, 13);
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
            this.pnlControls.Controls.Add(this.pnlLine);
            this.pnlControls.Controls.Add(this.lblHeaderText);
            this.pnlControls.Controls.Add(this.grpTagUsingExcel);
            this.pnlControls.Controls.Add(this.grpCompany);
            this.pnlControls.Controls.Add(this.grpEmployeeList);
            this.pnlControls.Controls.Add(this.grpEmployeeGroup);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 595);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 2);
            // 
            // grpEmployeeList
            // 
            this.grpEmployeeList.Controls.Add(this.grdvEmployees);
            this.grpEmployeeList.Controls.Add(this.chkSelectDeselect);
            this.grpEmployeeList.Controls.Add(this.lblSelectionValue);
            this.grpEmployeeList.Controls.Add(this.pnlVertical2);
            this.grpEmployeeList.Controls.Add(this.pnlVertical1);
            this.grpEmployeeList.Controls.Add(this.panel1);
            this.grpEmployeeList.Controls.Add(this.txtSearchAll);
            this.grpEmployeeList.Controls.Add(this.label1);
            this.grpEmployeeList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEmployeeList.Location = new System.Drawing.Point(173, 186);
            this.grpEmployeeList.Name = "grpEmployeeList";
            this.grpEmployeeList.Size = new System.Drawing.Size(656, 361);
            this.grpEmployeeList.TabIndex = 234;
            this.grpEmployeeList.TabStop = false;
            this.grpEmployeeList.Text = "Employee List";
            // 
            // grdvEmployees
            // 
            this.grdvEmployees.AllowUserToAddRows = false;
            this.grdvEmployees.AllowUserToDeleteRows = false;
            this.grdvEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvEmployees.Location = new System.Drawing.Point(12, 46);
            this.grdvEmployees.Name = "grdvEmployees";
            this.grdvEmployees.RowHeadersWidth = 15;
            this.grdvEmployees.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grdvEmployees.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.grdvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdvEmployees.Size = new System.Drawing.Size(633, 308);
            this.grdvEmployees.TabIndex = 243;
            this.grdvEmployees.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvEmployees_CellValueChanged);
            this.grdvEmployees.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdvEmployees_CurrentCellDirtyStateChanged);
            // 
            // chkSelectDeselect
            // 
            this.chkSelectDeselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectDeselect.AutoSize = true;
            this.chkSelectDeselect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSelectDeselect.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectDeselect.ForeColor = System.Drawing.Color.Black;
            this.chkSelectDeselect.Location = new System.Drawing.Point(16, 20);
            this.chkSelectDeselect.Name = "chkSelectDeselect";
            this.chkSelectDeselect.Size = new System.Drawing.Size(40, 19);
            this.chkSelectDeselect.TabIndex = 242;
            this.chkSelectDeselect.Text = "All";
            this.chkSelectDeselect.UseVisualStyleBackColor = true;
            this.chkSelectDeselect.CheckedChanged += new System.EventHandler(this.chkSelectDeselect_CheckedChanged);
            // 
            // lblSelectionValue
            // 
            this.lblSelectionValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectionValue.ForeColor = System.Drawing.Color.Blue;
            this.lblSelectionValue.Location = new System.Drawing.Point(76, 16);
            this.lblSelectionValue.Name = "lblSelectionValue";
            this.lblSelectionValue.Size = new System.Drawing.Size(130, 18);
            this.lblSelectionValue.TabIndex = 241;
            this.lblSelectionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlVertical2
            // 
            this.pnlVertical2.BackColor = System.Drawing.Color.Black;
            this.pnlVertical2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVertical2.Location = new System.Drawing.Point(206, 12);
            this.pnlVertical2.Name = "pnlVertical2";
            this.pnlVertical2.Size = new System.Drawing.Size(3, 27);
            this.pnlVertical2.TabIndex = 240;
            // 
            // pnlVertical1
            // 
            this.pnlVertical1.BackColor = System.Drawing.Color.Black;
            this.pnlVertical1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVertical1.Location = new System.Drawing.Point(73, 13);
            this.pnlVertical1.Name = "pnlVertical1";
            this.pnlVertical1.Size = new System.Drawing.Size(3, 25);
            this.pnlVertical1.TabIndex = 239;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(11, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 2);
            this.panel1.TabIndex = 236;
            // 
            // txtSearchAll
            // 
            this.txtSearchAll.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSearchAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAll.ForeColor = System.Drawing.Color.Silver;
            this.txtSearchAll.Location = new System.Drawing.Point(247, 13);
            this.txtSearchAll.Name = "txtSearchAll";
            this.txtSearchAll.Size = new System.Drawing.Size(395, 21);
            this.txtSearchAll.TabIndex = 235;
            this.txtSearchAll.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(211, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 234;
            this.label1.Text = "&Find";
            // 
            // grpEmployeeGroup
            // 
            this.grpEmployeeGroup.Controls.Add(this.pnlMessage);
            this.grpEmployeeGroup.Controls.Add(this.btnTagUsingExcel);
            this.grpEmployeeGroup.Controls.Add(this.panel2);
            this.grpEmployeeGroup.Controls.Add(this.btnCancelGroup);
            this.grpEmployeeGroup.Controls.Add(this.btnDeleteGroup);
            this.grpEmployeeGroup.Controls.Add(this.btnEditGroup);
            this.grpEmployeeGroup.Controls.Add(this.btnSaveGroup);
            this.grpEmployeeGroup.Controls.Add(this.txtNewEmployeeGroup);
            this.grpEmployeeGroup.Controls.Add(this.cmbEmployeeGroups);
            this.grpEmployeeGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEmployeeGroup.Location = new System.Drawing.Point(174, 74);
            this.grpEmployeeGroup.Name = "grpEmployeeGroup";
            this.grpEmployeeGroup.Size = new System.Drawing.Size(654, 110);
            this.grpEmployeeGroup.TabIndex = 1;
            this.grpEmployeeGroup.TabStop = false;
            this.grpEmployeeGroup.Text = "Employee Group";
            // 
            // pnlMessage
            // 
            this.pnlMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMessage.Controls.Add(this.lblMessage);
            this.pnlMessage.Location = new System.Drawing.Point(11, 78);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(513, 23);
            this.pnlMessage.TabIndex = 239;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblMessage.Location = new System.Drawing.Point(3, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(505, 18);
            this.lblMessage.TabIndex = 235;
            this.lblMessage.Text = "No. of Employee";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTagUsingExcel
            // 
            this.btnTagUsingExcel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnTagUsingExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTagUsingExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTagUsingExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnTagUsingExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTagUsingExcel.Location = new System.Drawing.Point(525, 76);
            this.btnTagUsingExcel.Name = "btnTagUsingExcel";
            this.btnTagUsingExcel.Size = new System.Drawing.Size(121, 27);
            this.btnTagUsingExcel.TabIndex = 238;
            this.btnTagUsingExcel.Text = "Tag using Excel";
            this.btnTagUsingExcel.UseVisualStyleBackColor = false;
            this.btnTagUsingExcel.Click += new System.EventHandler(this.btnTagUsingExcel_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(11, 74);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(633, 2);
            this.panel2.TabIndex = 237;
            // 
            // btnCancelGroup
            // 
            this.btnCancelGroup.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancelGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCancelGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelGroup.Location = new System.Drawing.Point(327, 44);
            this.btnCancelGroup.Name = "btnCancelGroup";
            this.btnCancelGroup.Size = new System.Drawing.Size(83, 23);
            this.btnCancelGroup.TabIndex = 231;
            this.btnCancelGroup.Text = "&Cancel";
            this.btnCancelGroup.UseVisualStyleBackColor = false;
            this.btnCancelGroup.Click += new System.EventHandler(this.btnCancelGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDeleteGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnDeleteGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteGroup.Location = new System.Drawing.Point(410, 44);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(83, 23);
            this.btnDeleteGroup.TabIndex = 230;
            this.btnDeleteGroup.Text = "&Delete";
            this.btnDeleteGroup.UseVisualStyleBackColor = false;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.BackColor = System.Drawing.Color.AliceBlue;
            this.btnEditGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnEditGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditGroup.Location = new System.Drawing.Point(244, 44);
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(83, 23);
            this.btnEditGroup.TabIndex = 229;
            this.btnEditGroup.Text = "&Edit";
            this.btnEditGroup.UseVisualStyleBackColor = false;
            this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
            // 
            // btnSaveGroup
            // 
            this.btnSaveGroup.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSaveGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSaveGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveGroup.Location = new System.Drawing.Point(161, 44);
            this.btnSaveGroup.Name = "btnSaveGroup";
            this.btnSaveGroup.Size = new System.Drawing.Size(83, 23);
            this.btnSaveGroup.TabIndex = 228;
            this.btnSaveGroup.Text = "&Save";
            this.btnSaveGroup.UseVisualStyleBackColor = false;
            this.btnSaveGroup.Click += new System.EventHandler(this.btnSaveGroup_Click);
            // 
            // txtNewEmployeeGroup
            // 
            this.txtNewEmployeeGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewEmployeeGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewEmployeeGroup.Enabled = false;
            this.txtNewEmployeeGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewEmployeeGroup.Location = new System.Drawing.Point(315, 17);
            this.txtNewEmployeeGroup.MaxLength = 25;
            this.txtNewEmployeeGroup.Name = "txtNewEmployeeGroup";
            this.txtNewEmployeeGroup.Size = new System.Drawing.Size(329, 21);
            this.txtNewEmployeeGroup.TabIndex = 216;
            this.txtNewEmployeeGroup.Enter += new System.EventHandler(this.txtNewEmployeeGroup_Enter);
            this.txtNewEmployeeGroup.Leave += new System.EventHandler(this.txtNewEmployeeGroup_Leave);
            // 
            // cmbEmployeeGroups
            // 
            this.cmbEmployeeGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployeeGroups.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmployeeGroups.FormattingEnabled = true;
            this.cmbEmployeeGroups.Location = new System.Drawing.Point(10, 16);
            this.cmbEmployeeGroups.Name = "cmbEmployeeGroups";
            this.cmbEmployeeGroups.Size = new System.Drawing.Size(299, 23);
            this.cmbEmployeeGroups.TabIndex = 0;
            this.cmbEmployeeGroups.SelectedIndexChanged += new System.EventHandler(this.cmbEmployeeGroups_SelectedIndexChanged);
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.lnkSearchByTAN);
            this.grpCompany.Controls.Add(this.cmbCompany);
            this.grpCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCompany.Location = new System.Drawing.Point(175, 28);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(653, 45);
            this.grpCompany.TabIndex = 0;
            this.grpCompany.TabStop = false;
            this.grpCompany.Text = "Select Company";
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(552, 21);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 214;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            this.lnkSearchByTAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lnkSearchByTAN_MouseMove);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(10, 16);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(537, 23);
            this.cmbCompany.TabIndex = 1;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // grpTagUsingExcel
            // 
            this.grpTagUsingExcel.Controls.Add(this.btnCloseTaggingUsingExcel);
            this.grpTagUsingExcel.Controls.Add(this.grpStepsExcel);
            this.grpTagUsingExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTagUsingExcel.Location = new System.Drawing.Point(173, 106);
            this.grpTagUsingExcel.Name = "grpTagUsingExcel";
            this.grpTagUsingExcel.Size = new System.Drawing.Size(656, 436);
            this.grpTagUsingExcel.TabIndex = 237;
            this.grpTagUsingExcel.TabStop = false;
            this.grpTagUsingExcel.Text = "Tag using Excel";
            this.grpTagUsingExcel.Visible = false;
            // 
            // grpStepsExcel
            // 
            this.grpStepsExcel.Controls.Add(this.grpEmployeeGrid);
            this.grpStepsExcel.Controls.Add(this.grpStep2);
            this.grpStepsExcel.Controls.Add(this.grpTagExcelStep1);
            this.grpStepsExcel.Location = new System.Drawing.Point(7, 22);
            this.grpStepsExcel.Name = "grpStepsExcel";
            this.grpStepsExcel.Size = new System.Drawing.Size(641, 407);
            this.grpStepsExcel.TabIndex = 5;
            this.grpStepsExcel.TabStop = false;
            this.grpStepsExcel.Text = "Steps for tagging Employees with Group";
            // 
            // grpEmployeeGrid
            // 
            this.grpEmployeeGrid.Controls.Add(this.label3);
            this.grpEmployeeGrid.Controls.Add(this.dgcViewEmployeeExcel);
            this.grpEmployeeGrid.Controls.Add(this.btnTag);
            this.grpEmployeeGrid.Enabled = false;
            this.grpEmployeeGrid.Location = new System.Drawing.Point(6, 135);
            this.grpEmployeeGrid.Name = "grpEmployeeGrid";
            this.grpEmployeeGrid.Size = new System.Drawing.Size(629, 267);
            this.grpEmployeeGrid.TabIndex = 2;
            this.grpEmployeeGrid.TabStop = false;
            this.grpEmployeeGrid.Text = "Step 3";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(10, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(568, 14);
            this.label3.TabIndex = 231;
            this.label3.Text = "Review the data in grid before importing. Click \'Tag\' to import the Group(s) to t" +
    "he employee(s).";
            // 
            // dgcViewEmployeeExcel
            // 
            this.dgcViewEmployeeExcel.AlternatingBackColor = System.Drawing.Color.White;
            this.dgcViewEmployeeExcel.BackColor = System.Drawing.Color.White;
            this.dgcViewEmployeeExcel.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgcViewEmployeeExcel.CaptionBackColor = System.Drawing.Color.Honeydew;
            this.dgcViewEmployeeExcel.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewEmployeeExcel.CaptionForeColor = System.Drawing.Color.Black;
            this.dgcViewEmployeeExcel.CaptionText = "                                                                         Summary " +
    "information";
            this.dgcViewEmployeeExcel.DataMember = "";
            this.dgcViewEmployeeExcel.FlatMode = true;
            this.dgcViewEmployeeExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewEmployeeExcel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgcViewEmployeeExcel.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgcViewEmployeeExcel.Location = new System.Drawing.Point(5, 16);
            this.dgcViewEmployeeExcel.Name = "dgcViewEmployeeExcel";
            this.dgcViewEmployeeExcel.ReadOnly = true;
            this.dgcViewEmployeeExcel.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dgcViewEmployeeExcel.Size = new System.Drawing.Size(618, 224);
            this.dgcViewEmployeeExcel.TabIndex = 230;
            // 
            // btnTag
            // 
            this.btnTag.BackColor = System.Drawing.Color.AliceBlue;
            this.btnTag.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTag.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnTag.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTag.Location = new System.Drawing.Point(580, 240);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(45, 23);
            this.btnTag.TabIndex = 229;
            this.btnTag.Text = "Tag";
            this.btnTag.UseVisualStyleBackColor = false;
            this.btnTag.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // grpStep2
            // 
            this.grpStep2.Controls.Add(this.lblValidationMessage);
            this.grpStep2.Controls.Add(this.label2);
            this.grpStep2.Controls.Add(this.btnExcelValidate);
            this.grpStep2.Controls.Add(this.btnSelectExcelPath);
            this.grpStep2.Controls.Add(this.txtExcelPath);
            this.grpStep2.Controls.Add(this.lblStep2);
            this.grpStep2.Location = new System.Drawing.Point(6, 71);
            this.grpStep2.Name = "grpStep2";
            this.grpStep2.Size = new System.Drawing.Size(629, 63);
            this.grpStep2.TabIndex = 1;
            this.grpStep2.TabStop = false;
            this.grpStep2.Text = "Step 2";
            // 
            // lblValidationMessage
            // 
            this.lblValidationMessage.AutoSize = true;
            this.lblValidationMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidationMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblValidationMessage.Location = new System.Drawing.Point(432, 40);
            this.lblValidationMessage.Name = "lblValidationMessage";
            this.lblValidationMessage.Size = new System.Drawing.Size(106, 13);
            this.lblValidationMessage.TabIndex = 231;
            this.lblValidationMessage.Text = "lblValidationMessage";
            this.lblValidationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblValidationMessage.Visible = false;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 14);
            this.label2.TabIndex = 230;
            this.label2.Text = "(ii) Click the button to Validate the Excel file";
            // 
            // btnExcelValidate
            // 
            this.btnExcelValidate.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExcelValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelValidate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelValidate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExcelValidate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExcelValidate.Location = new System.Drawing.Point(272, 36);
            this.btnExcelValidate.Name = "btnExcelValidate";
            this.btnExcelValidate.Size = new System.Drawing.Size(140, 23);
            this.btnExcelValidate.TabIndex = 229;
            this.btnExcelValidate.Text = "Validate the Excel file";
            this.btnExcelValidate.UseVisualStyleBackColor = false;
            this.btnExcelValidate.Click += new System.EventHandler(this.btnExcelValidate_Click);
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Lavender;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSelectExcelPath.Location = new System.Drawing.Point(588, 12);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(33, 22);
            this.btnSelectExcelPath.TabIndex = 189;
            this.btnSelectExcelPath.Text = "...";
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelPath.Location = new System.Drawing.Point(206, 14);
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.Size = new System.Drawing.Size(379, 20);
            this.txtExcelPath.TabIndex = 188;
            // 
            // lblStep2
            // 
            this.lblStep2.ForeColor = System.Drawing.Color.Blue;
            this.lblStep2.Location = new System.Drawing.Point(10, 16);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(195, 14);
            this.lblStep2.TabIndex = 187;
            this.lblStep2.Text = "(i) Select the Excel file for import";
            // 
            // grpTagExcelStep1
            // 
            this.grpTagExcelStep1.Controls.Add(this.lblStep1);
            this.grpTagExcelStep1.Controls.Add(this.btnExportData);
            this.grpTagExcelStep1.Location = new System.Drawing.Point(6, 16);
            this.grpTagExcelStep1.Name = "grpTagExcelStep1";
            this.grpTagExcelStep1.Size = new System.Drawing.Size(629, 55);
            this.grpTagExcelStep1.TabIndex = 0;
            this.grpTagExcelStep1.TabStop = false;
            this.grpTagExcelStep1.Text = "Step 1";
            // 
            // lblStep1
            // 
            this.lblStep1.ForeColor = System.Drawing.Color.Blue;
            this.lblStep1.Location = new System.Drawing.Point(10, 13);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(577, 37);
            this.lblStep1.TabIndex = 187;
            this.lblStep1.Text = "Click the button to download the excel format with untagged Employee PAN & Name(s" +
    "). After downloading, enter the Group Name in the particular cell which you need" +
    " to tag.";
            this.lblStep1.UseMnemonic = false;
            // 
            // btnExportData
            // 
            this.btnExportData.BackColor = System.Drawing.Color.Lavender;
            this.btnExportData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExportData.Image = ((System.Drawing.Image)(resources.GetObject("btnExportData.Image")));
            this.btnExportData.Location = new System.Drawing.Point(591, 12);
            this.btnExportData.Name = "btnExportData";
            this.btnExportData.Size = new System.Drawing.Size(30, 29);
            this.btnExportData.TabIndex = 186;
            this.btnExportData.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExportData.UseVisualStyleBackColor = false;
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // btnCloseTaggingUsingExcel
            // 
            this.btnCloseTaggingUsingExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseTaggingUsingExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseTaggingUsingExcel.Image")));
            this.btnCloseTaggingUsingExcel.Location = new System.Drawing.Point(633, 6);
            this.btnCloseTaggingUsingExcel.Name = "btnCloseTaggingUsingExcel";
            this.btnCloseTaggingUsingExcel.Size = new System.Drawing.Size(22, 20);
            this.btnCloseTaggingUsingExcel.TabIndex = 4;
            this.btnCloseTaggingUsingExcel.UseVisualStyleBackColor = true;
            this.btnCloseTaggingUsingExcel.Click += new System.EventHandler(this.btnCloseTaggingUsingExcel_Click);
            this.btnCloseTaggingUsingExcel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCloseTaggingUsingExcel_MouseMove);
            // 
            // lblHeaderText
            // 
            this.lblHeaderText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderText.ForeColor = System.Drawing.Color.Blue;
            this.lblHeaderText.Location = new System.Drawing.Point(139, 4);
            this.lblHeaderText.Name = "lblHeaderText";
            this.lblHeaderText.Size = new System.Drawing.Size(725, 19);
            this.lblHeaderText.TabIndex = 238;
            this.lblHeaderText.Text = "Grouping of Employees is for the purpose of printing Form 16 (TDS Certificates) s" +
    "eparately for each Group under the Company.";
            this.lblHeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.Maroon;
            this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine.Location = new System.Drawing.Point(143, 24);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(721, 2);
            this.pnlLine.TabIndex = 239;
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(957, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 227;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // MstEmployeeGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 665);
            this.Name = "MstEmployeeGroup";
            this.Activated += new System.EventHandler(this.MstEmployeeGroup_Activated);
            this.Load += new System.EventHandler(this.MstEmployeeGroup_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpEmployeeList.ResumeLayout(false);
            this.grpEmployeeList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvEmployees)).EndInit();
            this.grpEmployeeGroup.ResumeLayout(false);
            this.grpEmployeeGroup.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.grpTagUsingExcel.ResumeLayout(false);
            this.grpStepsExcel.ResumeLayout(false);
            this.grpEmployeeGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewEmployeeExcel)).EndInit();
            this.grpStep2.ResumeLayout(false);
            this.grpStep2.PerformLayout();
            this.grpTagExcelStep1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEmployeeList;
        private System.Windows.Forms.GroupBox grpEmployeeGroup;
        public System.Windows.Forms.Button btnCancelGroup;
        public System.Windows.Forms.Button btnDeleteGroup;
        public System.Windows.Forms.Button btnEditGroup;
        public System.Windows.Forms.Button btnSaveGroup;
        private System.Windows.Forms.TextBox txtNewEmployeeGroup;
        private System.Windows.Forms.ComboBox cmbEmployeeGroups;
        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.ComboBox cmbCompany;
        public System.Windows.Forms.TextBox txtSearchAll;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnTagUsingExcel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpTagUsingExcel;
        private System.Windows.Forms.Button btnCloseTaggingUsingExcel;
        private System.Windows.Forms.GroupBox grpStepsExcel;
        private System.Windows.Forms.GroupBox grpTagExcelStep1;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Button btnExportData;
        private System.Windows.Forms.GroupBox grpStep2;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        public System.Windows.Forms.Button btnExcelValidate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpEmployeeGrid;
        public System.Windows.Forms.Button btnTag;
        private DGControl.DGControl dgcViewEmployeeExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlMessage;
        public System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblValidationMessage;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.Panel pnlVertical2;
        private System.Windows.Forms.Panel pnlVertical1;
        public System.Windows.Forms.Label lblSelectionValue;
        public System.Windows.Forms.Label lblHeaderText;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.CheckBox chkSelectDeselect;
        private System.Windows.Forms.DataGridView grdvEmployees;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
