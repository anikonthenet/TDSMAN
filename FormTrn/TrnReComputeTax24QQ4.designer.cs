namespace TDSMAN.FormTrn
{
    partial class TrnReComputeTax24QQ4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnReComputeTax24QQ4));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dgvDeductees = new DGVControl.DGVControl();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlBottomLine = new System.Windows.Forms.Panel();
            this.bgwTaxComputation = new System.ComponentModel.BackgroundWorker();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpRegularReturn = new System.Windows.Forms.GroupBox();
            this.btnLoadGrid = new System.Windows.Forms.Button();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.cntxtMnuExportData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntxtMenuExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCntxtMenuExportToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtMnuGrpNameDifference = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntxtMenuPrintNameDifference = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCntxtMenuExportNameDifference = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtMnuGrpValidPAN = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntxtMenuPrintValidPAN = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCntxtMenuExportValidPAN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.btnUpdateSalaryDetails = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDifferenceFound = new System.Windows.Forms.TextBox();
            this.txtTotalRecords = new System.Windows.Forms.TextBox();
            this.grpExportData = new System.Windows.Forms.GroupBox();
            this.btnCloseUpadateMaster = new System.Windows.Forms.Button();
            this.btnGoExport = new System.Windows.Forms.Button();
            this.rbnDifference = new System.Windows.Forms.RadioButton();
            this.rbnAllData = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShortDeductionValue = new System.Windows.Forms.TextBox();
            this.txtShortDeductionRecords = new System.Windows.Forms.TextBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgwTaxUpdation = new System.ComponentModel.BackgroundWorker();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).BeginInit();
            this.grpButtons.SuspendLayout();
            this.grpRegularReturn.SuspendLayout();
            this.cntxtMnuExportData.SuspendLayout();
            this.cntxtMnuGrpNameDifference.SuspendLayout();
            this.cntxtMnuGrpValidPAN.SuspendLayout();
            this.grpSummary.SuspendLayout();
            this.grpExportData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(851, 8);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(175, 24);
            this.lblSearchMode.TabIndex = 48;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(181, 8);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(670, 24);
            this.pnlTitle.TabIndex = 46;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(0, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(670, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Form 16 - Tax Computation (Bulk)";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(7, 8);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 47;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(7, 38);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1019, 3);
            this.pnlHeader.TabIndex = 49;
            // 
            // dgvDeductees
            // 
            this.dgvDeductees.AllowUserToAddRows = false;
            this.dgvDeductees.AllowUserToDeleteRows = false;
            this.dgvDeductees.AllowUserToOrderColumns = true;
            this.dgvDeductees.AllowUserToResizeRows = false;
            this.dgvDeductees.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvDeductees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeductees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeductees.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDeductees.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDeductees.Location = new System.Drawing.Point(12, 99);
            this.dgvDeductees.MultiSelect = false;
            this.dgvDeductees.Name = "dgvDeductees";
            this.dgvDeductees.RowHeadersWidth = 20;
            this.dgvDeductees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDeductees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeductees.Size = new System.Drawing.Size(1011, 477);
            this.dgvDeductees.TabIndex = 185;
            this.dgvDeductees.TabStop = false;
            this.dgvDeductees.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeductees_CellContentClick);
            this.dgvDeductees.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDeductees_CellFormatting);
            this.dgvDeductees.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeductees_CellValueChanged);
            this.dgvDeductees.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDeductees_ColumnAdded);
            this.dgvDeductees.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDeductees_CurrentCellDirtyStateChanged);
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnDownload);
            this.grpButtons.Controls.Add(this.btnXit);
            this.grpButtons.Controls.Add(this.btnStart);
            this.grpButtons.Location = new System.Drawing.Point(739, 585);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(234, 40);
            this.grpButtons.TabIndex = 187;
            this.grpButtons.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Lavender;
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.Location = new System.Drawing.Point(126, 11);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(42, 23);
            this.btnDownload.TabIndex = 191;
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnDownload, "Export to Excel");
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(169, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(59, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "E&xit";
            this.btnXit.UseVisualStyleBackColor = false;
            this.btnXit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Lavender;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(4, 11);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 23);
            this.btnStart.TabIndex = 189;
            this.btnStart.Text = "&Compute";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlBottomLine
            // 
            this.pnlBottomLine.BackColor = System.Drawing.Color.Black;
            this.pnlBottomLine.Location = new System.Drawing.Point(7, 582);
            this.pnlBottomLine.Name = "pnlBottomLine";
            this.pnlBottomLine.Size = new System.Drawing.Size(1019, 1);
            this.pnlBottomLine.TabIndex = 188;
            // 
            // bgwTaxComputation
            // 
            this.bgwTaxComputation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTaxComputation_DoWork);
            this.bgwTaxComputation.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwTaxComputation_ProgressChanged);
            this.bgwTaxComputation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTaxComputation_RunWorkerCompleted);
            // 
            // grpRegularReturn
            // 
            this.grpRegularReturn.Controls.Add(this.btnLoadGrid);
            this.grpRegularReturn.Controls.Add(this.cmbFinancialYear);
            this.grpRegularReturn.Controls.Add(this.label7);
            this.grpRegularReturn.Controls.Add(this.cmbCompany);
            this.grpRegularReturn.Controls.Add(this.label8);
            this.grpRegularReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRegularReturn.Location = new System.Drawing.Point(12, 41);
            this.grpRegularReturn.Name = "grpRegularReturn";
            this.grpRegularReturn.Size = new System.Drawing.Size(1011, 45);
            this.grpRegularReturn.TabIndex = 191;
            this.grpRegularReturn.TabStop = false;
            // 
            // btnLoadGrid
            // 
            this.btnLoadGrid.BackColor = System.Drawing.Color.Lavender;
            this.btnLoadGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGrid.ForeColor = System.Drawing.Color.Black;
            this.btnLoadGrid.Location = new System.Drawing.Point(871, 14);
            this.btnLoadGrid.Name = "btnLoadGrid";
            this.btnLoadGrid.Size = new System.Drawing.Size(59, 23);
            this.btnLoadGrid.TabIndex = 208;
            this.btnLoadGrid.Text = "Load";
            this.btnLoadGrid.UseVisualStyleBackColor = false;
            this.btnLoadGrid.Click += new System.EventHandler(this.btnLoadGrid_Click);
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(136, 14);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 23);
            this.cmbFinancialYear.TabIndex = 203;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 207;
            this.label7.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(393, 14);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(472, 23);
            this.cmbCompany.TabIndex = 205;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbLoadGrid_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(291, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 206;
            this.label8.Text = "Select Company";
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.Black;
            this.pnlLine.Location = new System.Drawing.Point(12, 92);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(1011, 1);
            this.pnlLine.TabIndex = 192;
            // 
            // cntxtMnuExportData
            // 
            this.cntxtMnuExportData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntxtMenuExportToExcel,
            this.mnuCntxtMenuExportToCSV});
            this.cntxtMnuExportData.Name = "cntxtMnuGrpInvalidPAN";
            this.cntxtMnuExportData.Size = new System.Drawing.Size(165, 48);
            // 
            // mnuCntxtMenuExportToExcel
            // 
            this.mnuCntxtMenuExportToExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportToExcel.Name = "mnuCntxtMenuExportToExcel";
            this.mnuCntxtMenuExportToExcel.Size = new System.Drawing.Size(164, 22);
            this.mnuCntxtMenuExportToExcel.Text = "Export To Excel";
            this.mnuCntxtMenuExportToExcel.Click += new System.EventHandler(this.mnuCntxtMenuExportToExcel_Click);
            // 
            // mnuCntxtMenuExportToCSV
            // 
            this.mnuCntxtMenuExportToCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportToCSV.Name = "mnuCntxtMenuExportToCSV";
            this.mnuCntxtMenuExportToCSV.Size = new System.Drawing.Size(164, 22);
            this.mnuCntxtMenuExportToCSV.Text = "Export to CSV";
            this.mnuCntxtMenuExportToCSV.Click += new System.EventHandler(this.mnuCntxtMenuExportToCSV_Click);
            // 
            // cntxtMnuGrpNameDifference
            // 
            this.cntxtMnuGrpNameDifference.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntxtMenuPrintNameDifference,
            this.mnuCntxtMenuExportNameDifference});
            this.cntxtMnuGrpNameDifference.Name = "cntxtMnuGrpInvalidPAN";
            this.cntxtMnuGrpNameDifference.Size = new System.Drawing.Size(154, 48);
            // 
            // mnuCntxtMenuPrintNameDifference
            // 
            this.mnuCntxtMenuPrintNameDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuPrintNameDifference.Name = "mnuCntxtMenuPrintNameDifference";
            this.mnuCntxtMenuPrintNameDifference.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuPrintNameDifference.Text = "Print";
            // 
            // mnuCntxtMenuExportNameDifference
            // 
            this.mnuCntxtMenuExportNameDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportNameDifference.Name = "mnuCntxtMenuExportNameDifference";
            this.mnuCntxtMenuExportNameDifference.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuExportNameDifference.Text = "Export to CSV";
            // 
            // cntxtMnuGrpValidPAN
            // 
            this.cntxtMnuGrpValidPAN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntxtMenuPrintValidPAN,
            this.mnuCntxtMenuExportValidPAN});
            this.cntxtMnuGrpValidPAN.Name = "cntxtMnuGrpInvalidPAN";
            this.cntxtMnuGrpValidPAN.Size = new System.Drawing.Size(154, 48);
            // 
            // mnuCntxtMenuPrintValidPAN
            // 
            this.mnuCntxtMenuPrintValidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuPrintValidPAN.Name = "mnuCntxtMenuPrintValidPAN";
            this.mnuCntxtMenuPrintValidPAN.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuPrintValidPAN.Text = "Print";
            // 
            // mnuCntxtMenuExportValidPAN
            // 
            this.mnuCntxtMenuExportValidPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuCntxtMenuExportValidPAN.Name = "mnuCntxtMenuExportValidPAN";
            this.mnuCntxtMenuExportValidPAN.Size = new System.Drawing.Size(153, 22);
            this.mnuCntxtMenuExportValidPAN.Text = "Export to CSV";
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.btnUpdateSalaryDetails);
            this.grpSummary.Controls.Add(this.label2);
            this.grpSummary.Controls.Add(this.label1);
            this.grpSummary.Controls.Add(this.txtDifferenceFound);
            this.grpSummary.Controls.Add(this.txtTotalRecords);
            this.grpSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSummary.Location = new System.Drawing.Point(12, 585);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(408, 40);
            this.grpSummary.TabIndex = 193;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Total";
            // 
            // btnUpdateSalaryDetails
            // 
            this.btnUpdateSalaryDetails.BackColor = System.Drawing.Color.Yellow;
            this.btnUpdateSalaryDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateSalaryDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSalaryDetails.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateSalaryDetails.Location = new System.Drawing.Point(294, 10);
            this.btnUpdateSalaryDetails.Name = "btnUpdateSalaryDetails";
            this.btnUpdateSalaryDetails.Size = new System.Drawing.Size(103, 23);
            this.btnUpdateSalaryDetails.TabIndex = 209;
            this.btnUpdateSalaryDetails.Text = "&Update";
            this.btnUpdateSalaryDetails.UseVisualStyleBackColor = false;
            this.btnUpdateSalaryDetails.Click += new System.EventHandler(this.BtnUpdateSalaryDetails_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Records";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(129, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 207;
            this.label1.Text = "Difference found";
            // 
            // txtDifferenceFound
            // 
            this.txtDifferenceFound.BackColor = System.Drawing.Color.Yellow;
            this.txtDifferenceFound.Location = new System.Drawing.Point(237, 12);
            this.txtDifferenceFound.Name = "txtDifferenceFound";
            this.txtDifferenceFound.ReadOnly = true;
            this.txtDifferenceFound.Size = new System.Drawing.Size(54, 20);
            this.txtDifferenceFound.TabIndex = 1;
            this.txtDifferenceFound.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalRecords
            // 
            this.txtTotalRecords.BackColor = System.Drawing.Color.White;
            this.txtTotalRecords.Location = new System.Drawing.Point(67, 12);
            this.txtTotalRecords.Name = "txtTotalRecords";
            this.txtTotalRecords.ReadOnly = true;
            this.txtTotalRecords.Size = new System.Drawing.Size(57, 20);
            this.txtTotalRecords.TabIndex = 0;
            this.txtTotalRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpExportData
            // 
            this.grpExportData.Controls.Add(this.btnCloseUpadateMaster);
            this.grpExportData.Controls.Add(this.btnGoExport);
            this.grpExportData.Controls.Add(this.rbnDifference);
            this.grpExportData.Controls.Add(this.rbnAllData);
            this.grpExportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportData.Location = new System.Drawing.Point(391, 280);
            this.grpExportData.Name = "grpExportData";
            this.grpExportData.Size = new System.Drawing.Size(256, 69);
            this.grpExportData.TabIndex = 194;
            this.grpExportData.TabStop = false;
            this.grpExportData.Text = "Export Data";
            this.grpExportData.Visible = false;
            // 
            // btnCloseUpadateMaster
            // 
            this.btnCloseUpadateMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseUpadateMaster.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseUpadateMaster.Image")));
            this.btnCloseUpadateMaster.Location = new System.Drawing.Point(234, 6);
            this.btnCloseUpadateMaster.Name = "btnCloseUpadateMaster";
            this.btnCloseUpadateMaster.Size = new System.Drawing.Size(22, 20);
            this.btnCloseUpadateMaster.TabIndex = 191;
            this.btnCloseUpadateMaster.UseVisualStyleBackColor = true;
            this.btnCloseUpadateMaster.Click += new System.EventHandler(this.btnCloseUpadateMaster_Click);
            // 
            // btnGoExport
            // 
            this.btnGoExport.BackColor = System.Drawing.Color.Lavender;
            this.btnGoExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoExport.ForeColor = System.Drawing.Color.Black;
            this.btnGoExport.Location = new System.Drawing.Point(196, 40);
            this.btnGoExport.Name = "btnGoExport";
            this.btnGoExport.Size = new System.Drawing.Size(54, 23);
            this.btnGoExport.TabIndex = 190;
            this.btnGoExport.Text = "Go";
            this.btnGoExport.UseVisualStyleBackColor = false;
            this.btnGoExport.Click += new System.EventHandler(this.btnGoExport_Click);
            this.btnGoExport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnGoExport_MouseClick);
            // 
            // rbnDifference
            // 
            this.rbnDifference.AutoSize = true;
            this.rbnDifference.Checked = true;
            this.rbnDifference.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnDifference.Location = new System.Drawing.Point(71, 26);
            this.rbnDifference.Name = "rbnDifference";
            this.rbnDifference.Size = new System.Drawing.Size(113, 17);
            this.rbnDifference.TabIndex = 1;
            this.rbnDifference.TabStop = true;
            this.rbnDifference.Text = "Only Difference";
            this.rbnDifference.UseVisualStyleBackColor = true;
            // 
            // rbnAllData
            // 
            this.rbnAllData.AutoSize = true;
            this.rbnAllData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAllData.Location = new System.Drawing.Point(26, 26);
            this.rbnAllData.Name = "rbnAllData";
            this.rbnAllData.Size = new System.Drawing.Size(39, 17);
            this.rbnAllData.TabIndex = 0;
            this.rbnAllData.Text = "All";
            this.rbnAllData.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtShortDeductionValue);
            this.groupBox1.Controls.Add(this.txtShortDeductionRecords);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(426, 585);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 40);
            this.groupBox1.TabIndex = 195;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Short Deduction";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 208;
            this.label3.Text = "Records";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(139, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 207;
            this.label4.Text = "Value";
            // 
            // txtShortDeductionValue
            // 
            this.txtShortDeductionValue.BackColor = System.Drawing.Color.White;
            this.txtShortDeductionValue.Location = new System.Drawing.Point(182, 14);
            this.txtShortDeductionValue.Name = "txtShortDeductionValue";
            this.txtShortDeductionValue.ReadOnly = true;
            this.txtShortDeductionValue.Size = new System.Drawing.Size(109, 20);
            this.txtShortDeductionValue.TabIndex = 1;
            this.txtShortDeductionValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtShortDeductionRecords
            // 
            this.txtShortDeductionRecords.BackColor = System.Drawing.Color.White;
            this.txtShortDeductionRecords.Location = new System.Drawing.Point(67, 14);
            this.txtShortDeductionRecords.Name = "txtShortDeductionRecords";
            this.txtShortDeductionRecords.ReadOnly = true;
            this.txtShortDeductionRecords.Size = new System.Drawing.Size(68, 20);
            this.txtShortDeductionRecords.TabIndex = 0;
            this.txtShortDeductionRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(984, 591);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 210;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgwTaxUpdation
            // 
            this.bgwTaxUpdation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwTaxUpdation_DoWork);
            this.bgwTaxUpdation.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgwTaxUpdation_ProgressChanged);
            this.bgwTaxUpdation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgwTaxUpdation_RunWorkerCompleted);
            // 
            // TrnReComputeTax24QQ4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1038, 629);
            this.Controls.Add(this.pctUserManual);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpExportData);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.pnlBottomLine);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.dgvDeductees);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.grpRegularReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrnReComputeTax24QQ4";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.toolTip1.SetToolTip(this, "Download Excel");
            this.Activated += new System.EventHandler(this.TrnBulkPANNameValidation_Activated);
            this.Load += new System.EventHandler(this.TrnBulkPANNameValidation_Load);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeductees)).EndInit();
            this.grpButtons.ResumeLayout(false);
            this.grpRegularReturn.ResumeLayout(false);
            this.grpRegularReturn.PerformLayout();
            this.cntxtMnuExportData.ResumeLayout(false);
            this.cntxtMnuGrpNameDifference.ResumeLayout(false);
            this.cntxtMnuGrpValidPAN.ResumeLayout(false);
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            this.grpExportData.ResumeLayout(false);
            this.grpExportData.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlHeader;
        private DGVControl.DGVControl dgvDeductees;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlBottomLine;
        private System.ComponentModel.BackgroundWorker bgwTaxComputation;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpRegularReturn;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuExportData;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportToCSV;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuGrpNameDifference;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuPrintNameDifference;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportNameDifference;
        private System.Windows.Forms.ContextMenuStrip cntxtMnuGrpValidPAN;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuPrintValidPAN;
        private System.Windows.Forms.ToolStripMenuItem mnuCntxtMenuExportValidPAN;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnLoadGrid;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.TextBox txtTotalRecords;
        private System.Windows.Forms.TextBox txtDifferenceFound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpExportData;
        private System.Windows.Forms.RadioButton rbnDifference;
        private System.Windows.Forms.RadioButton rbnAllData;
        private System.Windows.Forms.Button btnGoExport;
        private System.Windows.Forms.Button btnCloseUpadateMaster;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtShortDeductionValue;
        private System.Windows.Forms.TextBox txtShortDeductionRecords;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Button btnUpdateSalaryDetails;
        private System.ComponentModel.BackgroundWorker bgwTaxUpdation;
    }
}