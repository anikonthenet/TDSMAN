namespace TDSMAN.FormTrn
{
    partial class TrnGetChallanTRACES
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnGetChallanTRACES));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBackUp = new System.Windows.Forms.GroupBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadTo = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mskViewFileDownloadFrom = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.grpCaptcha = new System.Windows.Forms.GroupBox();
            this.btnChallan = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnCaptchaRefresh = new System.Windows.Forms.Button();
            this.txtCaptchaCode = new System.Windows.Forms.TextBox();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.dgvStatementList = new System.Windows.Forms.DataGridView();
            this.btnValidate = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cntxtMnuDownload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuImportChallanToTDSMAN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportChallanToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportChallanToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTracesDetails = new System.Windows.Forms.GroupBox();
            this.chkBIN = new System.Windows.Forms.CheckBox();
            this.btnTracesCaptchaRefresh = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTracesCaptcha = new System.Windows.Forms.TextBox();
            this.btnCloseTraces = new System.Windows.Forms.Button();
            this.btnGoTraces = new System.Windows.Forms.Button();
            this.pctTracesCaptcha = new System.Windows.Forms.PictureBox();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bgwTracesChallanVerification = new System.ComponentModel.BackgroundWorker();
            this.pgTimerGrid = new System.Windows.Forms.Timer(this.components);
            this.pgTimerMatchingRecords = new System.Windows.Forms.Timer(this.components);
            this.pgLoadGrid = new System.Windows.Forms.Timer(this.components);
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpBackUp.SuspendLayout();
            this.grpCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.cntxtMnuDownload.SuspendLayout();
            this.grpTracesDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTracesCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(99, 638);
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
            this.BtnSave.Location = new System.Drawing.Point(356, 13);
            this.BtnSave.Size = new System.Drawing.Size(103, 25);
            this.BtnSave.Text = "&Import/Export";
            this.BtnSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseClick);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 616);
            this.grpSearch.Size = new System.Drawing.Size(280, 4);
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
            this.BtnExit.Location = new System.Drawing.Point(544, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.Lavender;
            this.BtnRefresh.Location = new System.Drawing.Point(460, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(83, 25);
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
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
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpTracesDetails);
            this.pnlControls.Controls.Add(this.panel3);
            this.pnlControls.Controls.Add(this.lblStep2);
            this.pnlControls.Controls.Add(this.lblStep1);
            this.pnlControls.Controls.Add(this.panel2);
            this.pnlControls.Controls.Add(this.panel1);
            this.pnlControls.Controls.Add(this.dgvResult);
            this.pnlControls.Controls.Add(this.btnValidate);
            this.pnlControls.Controls.Add(this.grpCaptcha);
            this.pnlControls.Controls.Add(this.grpBackUp);
            this.pnlControls.Controls.Add(this.dgvStatementList);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpBackUp
            // 
            this.grpBackUp.Controls.Add(this.txtCompany);
            this.grpBackUp.Controls.Add(this.lnkSearchByTAN);
            this.grpBackUp.Controls.Add(this.label4);
            this.grpBackUp.Controls.Add(this.label52);
            this.grpBackUp.Controls.Add(this.label3);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadTo);
            this.grpBackUp.Controls.Add(this.label2);
            this.grpBackUp.Controls.Add(this.mskViewFileDownloadFrom);
            this.grpBackUp.Controls.Add(this.label1);
            this.grpBackUp.Controls.Add(this.cmbCompany);
            this.grpBackUp.Location = new System.Drawing.Point(125, 15);
            this.grpBackUp.Name = "grpBackUp";
            this.grpBackUp.Size = new System.Drawing.Size(752, 65);
            this.grpBackUp.TabIndex = 0;
            this.grpBackUp.TabStop = false;
            // 
            // txtCompany
            // 
            this.txtCompany.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.Location = new System.Drawing.Point(112, 15);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(541, 20);
            this.txtCompany.TabIndex = 214;
            this.txtCompany.TabStop = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(652, 17);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 213;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.Visible = false;
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            this.lnkSearchByTAN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lnkSearchByTAN_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(244, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 201;
            this.label4.Text = "DD/MM/YYYY";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(599, 42);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(89, 13);
            this.label52.TabIndex = 200;
            this.label52.Text = "DD/MM/YYYY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(402, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 195;
            this.label3.Text = "Challan To Date";
            // 
            // mskViewFileDownloadTo
            // 
            this.mskViewFileDownloadTo.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadTo.Location = new System.Drawing.Point(507, 39);
            this.mskViewFileDownloadTo.Mask = "00/00/0000";
            this.mskViewFileDownloadTo.Name = "mskViewFileDownloadTo";
            this.mskViewFileDownloadTo.Size = new System.Drawing.Size(89, 20);
            this.mskViewFileDownloadTo.TabIndex = 1;
            this.mskViewFileDownloadTo.ValidatingType = typeof(System.DateTime);
            this.mskViewFileDownloadTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskViewFileDownloadTo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 193;
            this.label2.Text = "Challan From Date";
            // 
            // mskViewFileDownloadFrom
            // 
            this.mskViewFileDownloadFrom.BackColor = System.Drawing.Color.White;
            this.mskViewFileDownloadFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskViewFileDownloadFrom.Location = new System.Drawing.Point(153, 39);
            this.mskViewFileDownloadFrom.Mask = "00/00/0000";
            this.mskViewFileDownloadFrom.Name = "mskViewFileDownloadFrom";
            this.mskViewFileDownloadFrom.Size = new System.Drawing.Size(88, 20);
            this.mskViewFileDownloadFrom.TabIndex = 0;
            this.mskViewFileDownloadFrom.ValidatingType = typeof(System.DateTime);
            this.mskViewFileDownloadFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskViewFileDownloadFrom_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Company Name";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(643, 14);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(10, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.Visible = false;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            // 
            // grpCaptcha
            // 
            this.grpCaptcha.Controls.Add(this.btnChallan);
            this.grpCaptcha.Controls.Add(this.lblMessage);
            this.grpCaptcha.Controls.Add(this.btnCaptchaRefresh);
            this.grpCaptcha.Controls.Add(this.txtCaptchaCode);
            this.grpCaptcha.Controls.Add(this.picCaptcha);
            this.grpCaptcha.Location = new System.Drawing.Point(125, 79);
            this.grpCaptcha.Name = "grpCaptcha";
            this.grpCaptcha.Size = new System.Drawing.Size(752, 80);
            this.grpCaptcha.TabIndex = 1;
            this.grpCaptcha.TabStop = false;
            this.grpCaptcha.Visible = false;
            // 
            // btnChallan
            // 
            this.btnChallan.BackColor = System.Drawing.Color.Lavender;
            this.btnChallan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChallan.Location = new System.Drawing.Point(590, 51);
            this.btnChallan.Name = "btnChallan";
            this.btnChallan.Size = new System.Drawing.Size(127, 23);
            this.btnChallan.TabIndex = 1;
            this.btnChallan.Text = "View Challan";
            this.btnChallan.UseVisualStyleBackColor = false;
            this.btnChallan.Click += new System.EventHandler(this.btnChallan_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(316, 56);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(138, 13);
            this.lblMessage.TabIndex = 213;
            this.lblMessage.Text = "Enter text as the image";
            // 
            // btnCaptchaRefresh
            // 
            this.btnCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptchaRefresh.BackgroundImage")));
            this.btnCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCaptchaRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptchaRefresh.Location = new System.Drawing.Point(260, 33);
            this.btnCaptchaRefresh.Name = "btnCaptchaRefresh";
            this.btnCaptchaRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnCaptchaRefresh.TabIndex = 210;
            this.btnCaptchaRefresh.TabStop = false;
            this.btnCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnCaptchaRefresh.Click += new System.EventHandler(this.btnCaptchaRefresh_Click);
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCaptchaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaptchaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptchaCode.Location = new System.Drawing.Point(462, 53);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(119, 20);
            this.txtCaptchaCode.TabIndex = 0;
            // 
            // picCaptcha
            // 
            this.picCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCaptcha.Location = new System.Drawing.Point(41, 14);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(216, 59);
            this.picCaptcha.TabIndex = 209;
            this.picCaptcha.TabStop = false;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 10);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(80, 32);
            this.pctVideoDemo.TabIndex = 10;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Visible = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // dgvStatementList
            // 
            this.dgvStatementList.AllowUserToAddRows = false;
            this.dgvStatementList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatementList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStatementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatementList.Location = new System.Drawing.Point(52, 179);
            this.dgvStatementList.Name = "dgvStatementList";
            this.dgvStatementList.Size = new System.Drawing.Size(898, 341);
            this.dgvStatementList.TabIndex = 209;
            // 
            // btnValidate
            // 
            this.btnValidate.BackColor = System.Drawing.Color.Lavender;
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidate.Location = new System.Drawing.Point(930, 345);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(20, 23);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "Validate Amount";
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Visible = false;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(52, 371);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.Size = new System.Drawing.Size(898, 10);
            this.dgvResult.TabIndex = 216;
            this.dgvResult.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(264, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 2);
            this.panel1.TabIndex = 217;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(54, 533);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 2);
            this.panel2.TabIndex = 218;
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1.Location = new System.Drawing.Point(49, 6);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(318, 13);
            this.lblStep1.TabIndex = 219;
            this.lblStep1.Text = "Step 1 - Enter Challan From, To Dates & Traces details";
            this.lblStep1.UseMnemonic = false;
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep2.Location = new System.Drawing.Point(49, 162);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(205, 13);
            this.lblStep2.TabIndex = 220;
            this.lblStep2.Text = "Step 2 - Select Challan(s) to import";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(373, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(577, 2);
            this.panel3.TabIndex = 222;
            // 
            // cntxtMnuDownload
            // 
            this.cntxtMnuDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cntxtMnuDownload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportChallanToTDSMAN,
            this.toolStripSeparator1,
            this.mnuExportChallanToExcel,
            this.mnuExportChallanToCSV});
            this.cntxtMnuDownload.Name = "cntxtMnuDownload";
            this.cntxtMnuDownload.Size = new System.Drawing.Size(246, 76);
            // 
            // mnuImportChallanToTDSMAN
            // 
            this.mnuImportChallanToTDSMAN.Name = "mnuImportChallanToTDSMAN";
            this.mnuImportChallanToTDSMAN.Size = new System.Drawing.Size(245, 22);
            this.mnuImportChallanToTDSMAN.Text = "Import Challan to TDSMAN";
            this.mnuImportChallanToTDSMAN.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // mnuExportChallanToExcel
            // 
            this.mnuExportChallanToExcel.Name = "mnuExportChallanToExcel";
            this.mnuExportChallanToExcel.Size = new System.Drawing.Size(245, 22);
            this.mnuExportChallanToExcel.Text = "Export Challan to Excel";
            this.mnuExportChallanToExcel.Click += new System.EventHandler(this.mnuExportChallanToExcel_Click);
            // 
            // mnuExportChallanToCSV
            // 
            this.mnuExportChallanToCSV.Name = "mnuExportChallanToCSV";
            this.mnuExportChallanToCSV.Size = new System.Drawing.Size(245, 22);
            this.mnuExportChallanToCSV.Text = "Export Challan to CSV";
            this.mnuExportChallanToCSV.Click += new System.EventHandler(this.mnuExportChallanToCSV_Click);
            // 
            // grpTracesDetails
            // 
            this.grpTracesDetails.Controls.Add(this.chkBIN);
            this.grpTracesDetails.Controls.Add(this.btnTracesCaptchaRefresh);
            this.grpTracesDetails.Controls.Add(this.label10);
            this.grpTracesDetails.Controls.Add(this.txtTracesCaptcha);
            this.grpTracesDetails.Controls.Add(this.btnCloseTraces);
            this.grpTracesDetails.Controls.Add(this.btnGoTraces);
            this.grpTracesDetails.Controls.Add(this.pctTracesCaptcha);
            this.grpTracesDetails.Controls.Add(this.txtTANNo);
            this.grpTracesDetails.Controls.Add(this.label6);
            this.grpTracesDetails.Controls.Add(this.txtUserID);
            this.grpTracesDetails.Controls.Add(this.txtPassword);
            this.grpTracesDetails.Controls.Add(this.label7);
            this.grpTracesDetails.Controls.Add(this.label9);
            this.grpTracesDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTracesDetails.Location = new System.Drawing.Point(125, 81);
            this.grpTracesDetails.Name = "grpTracesDetails";
            this.grpTracesDetails.Size = new System.Drawing.Size(751, 78);
            this.grpTracesDetails.TabIndex = 224;
            this.grpTracesDetails.TabStop = false;
            this.grpTracesDetails.Text = "Enter TRACES Login Details";
            // 
            // chkBIN
            // 
            this.chkBIN.AutoSize = true;
            this.chkBIN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBIN.Location = new System.Drawing.Point(49, 50);
            this.chkBIN.Name = "chkBIN";
            this.chkBIN.Size = new System.Drawing.Size(99, 17);
            this.chkBIN.TabIndex = 227;
            this.chkBIN.Text = "BIN Challans";
            this.chkBIN.UseVisualStyleBackColor = true;
            this.chkBIN.Visible = false;
            // 
            // btnTracesCaptchaRefresh
            // 
            this.btnTracesCaptchaRefresh.BackColor = System.Drawing.Color.Lavender;
            this.btnTracesCaptchaRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTracesCaptchaRefresh.BackgroundImage")));
            this.btnTracesCaptchaRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTracesCaptchaRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTracesCaptchaRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTracesCaptchaRefresh.Location = new System.Drawing.Point(632, 11);
            this.btnTracesCaptchaRefresh.Name = "btnTracesCaptchaRefresh";
            this.btnTracesCaptchaRefresh.Size = new System.Drawing.Size(37, 39);
            this.btnTracesCaptchaRefresh.TabIndex = 226;
            this.btnTracesCaptchaRefresh.TabStop = false;
            this.btnTracesCaptchaRefresh.UseVisualStyleBackColor = false;
            this.btnTracesCaptchaRefresh.Click += new System.EventHandler(this.btnTracesCaptchaRefresh_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(158, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 225;
            this.label10.Text = "Enter code";
            // 
            // txtTracesCaptcha
            // 
            this.txtTracesCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTracesCaptcha.Location = new System.Drawing.Point(231, 46);
            this.txtTracesCaptcha.Name = "txtTracesCaptcha";
            this.txtTracesCaptcha.Size = new System.Drawing.Size(102, 20);
            this.txtTracesCaptcha.TabIndex = 3;
            // 
            // btnCloseTraces
            // 
            this.btnCloseTraces.BackColor = System.Drawing.Color.Lavender;
            this.btnCloseTraces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseTraces.ForeColor = System.Drawing.Color.Black;
            this.btnCloseTraces.Location = new System.Drawing.Point(692, 54);
            this.btnCloseTraces.Name = "btnCloseTraces";
            this.btnCloseTraces.Size = new System.Drawing.Size(47, 20);
            this.btnCloseTraces.TabIndex = 5;
            this.btnCloseTraces.Text = "&Close";
            this.btnCloseTraces.UseVisualStyleBackColor = false;
            this.btnCloseTraces.Click += new System.EventHandler(this.btnCloseTraces_Click);
            // 
            // btnGoTraces
            // 
            this.btnGoTraces.BackColor = System.Drawing.Color.Lavender;
            this.btnGoTraces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTraces.ForeColor = System.Drawing.Color.Black;
            this.btnGoTraces.Location = new System.Drawing.Point(645, 54);
            this.btnGoTraces.Name = "btnGoTraces";
            this.btnGoTraces.Size = new System.Drawing.Size(47, 20);
            this.btnGoTraces.TabIndex = 4;
            this.btnGoTraces.Text = "&Go";
            this.btnGoTraces.UseVisualStyleBackColor = false;
            this.btnGoTraces.Click += new System.EventHandler(this.btnGoTraces_Click);
            // 
            // pctTracesCaptcha
            // 
            this.pctTracesCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctTracesCaptcha.Location = new System.Drawing.Point(360, 8);
            this.pctTracesCaptcha.Name = "pctTracesCaptcha";
            this.pctTracesCaptcha.Size = new System.Drawing.Size(271, 57);
            this.pctTracesCaptcha.TabIndex = 210;
            this.pctTracesCaptcha.TabStop = false;
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(60, 21);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.ReadOnly = true;
            this.txtTANNo.Size = new System.Drawing.Size(96, 20);
            this.txtTANNo.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 209;
            this.label6.Text = "TAN";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(60, 47);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(24, 20);
            this.txtUserID.TabIndex = 2;
            this.txtUserID.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(231, 21);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(102, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(166, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 208;
            this.label7.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 206;
            this.label9.Text = "User ID";
            this.label9.Visible = false;
            // 
            // bgwTracesChallanVerification
            // 
            this.bgwTracesChallanVerification.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTracesChallanVerification_DoWork);
            this.bgwTracesChallanVerification.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTracesChallanVerification_RunWorkerCompleted);
            // 
            // pgLoadGrid
            // 
            this.pgLoadGrid.Tick += new System.EventHandler(this.pgLoadGrid_Tick);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.WorkerSupportsCancellation = true;
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnGetChallanTRACES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1025, 658);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnGetChallanTRACES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SysBackup_Load);
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
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpBackUp.ResumeLayout(false);
            this.grpBackUp.PerformLayout();
            this.grpCaptcha.ResumeLayout(false);
            this.grpCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatementList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.cntxtMnuDownload.ResumeLayout(false);
            this.grpTracesDetails.ResumeLayout(false);
            this.grpTracesDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTracesCaptcha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBackUp;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mskViewFileDownloadTo;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpCaptcha;
        private System.Windows.Forms.Button btnCaptchaRefresh;
        private System.Windows.Forms.TextBox txtCaptchaCode;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.DataGridView dgvStatementList;
        private System.Windows.Forms.Button btnChallan;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuDownload;
        private System.Windows.Forms.ToolStripMenuItem mnuImportChallanToTDSMAN;
        private System.Windows.Forms.ToolStripMenuItem mnuExportChallanToExcel;
        private System.Windows.Forms.GroupBox grpTracesDetails;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pctTracesCaptcha;
        private System.Windows.Forms.Button btnCloseTraces;
        private System.Windows.Forms.Button btnGoTraces;
        private System.Windows.Forms.Button btnTracesCaptchaRefresh;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTracesCaptcha;
        private System.ComponentModel.BackgroundWorker bgwTracesChallanVerification;
        private System.Windows.Forms.Timer pgTimerGrid;
        private System.Windows.Forms.Timer pgTimerMatchingRecords;
        private System.Windows.Forms.Timer pgLoadGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuExportChallanToCSV;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chkBIN;
    }
}
