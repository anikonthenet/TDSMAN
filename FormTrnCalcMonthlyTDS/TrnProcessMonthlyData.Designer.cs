namespace TDSMAN.FormTrnCalcMonthlyTDS
{
    partial class TrnProcessMonthlyData
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnLoadEmployees = new System.Windows.Forms.Button();
            this.cmbMonthSerialNo = new System.Windows.Forms.ComboBox();
            this.lblMonthBatchNo = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.lblSelectMonthCaption = new System.Windows.Forms.Label();
            this.txtDeductorType = new System.Windows.Forms.TextBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlLIne = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvEmployeeList = new DGVControl.DGVControl();
            this.bgwProcessMonthlyTDS = new System.ComponentModel.BackgroundWorker();
            this.pnlProcess = new System.Windows.Forms.Panel();
            this.btnStopProcessing = new System.Windows.Forms.Button();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.lblProcessCounter = new System.Windows.Forms.Label();
            this.lblTotalPaymentAmount = new System.Windows.Forms.Label();
            this.lblTotalTDS = new System.Windows.Forms.Label();
            this.lblProcessedRecords = new System.Windows.Forms.Label();
            this.pnlHideProcess = new System.Windows.Forms.Panel();
            this.chkHideThisProcess = new System.Windows.Forms.CheckBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).BeginInit();
            this.pnlProcess.SuspendLayout();
            this.pnlHideProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(3, 634);
            this.grpSort.Size = new System.Drawing.Size(53, 30);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.Lavender;
            this.BtnCancel.Location = new System.Drawing.Point(566, 13);
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.Text = "E&xit";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(362, 13);
            this.BtnSave.Size = new System.Drawing.Size(127, 23);
            this.BtnSave.Text = "Start &Processing";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(15, 634);
            this.grpSearch.Size = new System.Drawing.Size(72, 30);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(184, 13);
            this.BtnEdit.Size = new System.Drawing.Size(20, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(19, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(275, 16);
            this.BtnExit.Size = new System.Drawing.Size(22, 23);
            this.BtnExit.Visible = false;
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.Text = "Processing Mode";
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.Lavender;
            this.BtnPrint.Location = new System.Drawing.Point(490, 13);
            this.BtnPrint.Size = new System.Drawing.Size(75, 23);
            this.BtnPrint.Text = "&Export";
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(254, 16);
            this.BtnRefresh.Size = new System.Drawing.Size(20, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(234, 13);
            this.BtnDelete.Size = new System.Drawing.Size(14, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(210, 13);
            this.BtnSearch.Size = new System.Drawing.Size(15, 23);
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pnlHideProcess);
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
            this.grpButton.Controls.SetChildIndex(this.pnlHideProcess, 0);
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblProcessedRecords);
            this.pnlControls.Controls.Add(this.lblTotalTDS);
            this.pnlControls.Controls.Add(this.lblTotalPaymentAmount);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.dgvEmployeeList);
            this.pnlControls.Controls.Add(this.pnlLIne);
            this.pnlControls.Controls.Add(this.grpMain);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnLoadEmployees);
            this.grpMain.Controls.Add(this.cmbMonthSerialNo);
            this.grpMain.Controls.Add(this.lblMonthBatchNo);
            this.grpMain.Controls.Add(this.cmbMonth);
            this.grpMain.Controls.Add(this.lblSelectMonthCaption);
            this.grpMain.Controls.Add(this.txtDeductorType);
            this.grpMain.Controls.Add(this.cmbFormNo);
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.cmbQuarter);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.cmbFinancialYear);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.cmbCompany);
            this.grpMain.Controls.Add(this.label5);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.ForeColor = System.Drawing.Color.Black;
            this.grpMain.Location = new System.Drawing.Point(91, 6);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(821, 107);
            this.grpMain.TabIndex = 68;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Select Batch for Processing";
            // 
            // btnLoadEmployees
            // 
            this.btnLoadEmployees.BackColor = System.Drawing.Color.Lavender;
            this.btnLoadEmployees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadEmployees.Location = new System.Drawing.Point(672, 74);
            this.btnLoadEmployees.Name = "btnLoadEmployees";
            this.btnLoadEmployees.Size = new System.Drawing.Size(110, 27);
            this.btnLoadEmployees.TabIndex = 222;
            this.btnLoadEmployees.Text = "Load Employees";
            this.btnLoadEmployees.UseVisualStyleBackColor = false;
            this.btnLoadEmployees.Click += new System.EventHandler(this.btnLoadEmployees_Click);
            // 
            // cmbMonthSerialNo
            // 
            this.cmbMonthSerialNo.BackColor = System.Drawing.Color.White;
            this.cmbMonthSerialNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthSerialNo.FormattingEnabled = true;
            this.cmbMonthSerialNo.Location = new System.Drawing.Point(395, 75);
            this.cmbMonthSerialNo.MaxLength = 10;
            this.cmbMonthSerialNo.Name = "cmbMonthSerialNo";
            this.cmbMonthSerialNo.Size = new System.Drawing.Size(102, 21);
            this.cmbMonthSerialNo.TabIndex = 220;
            // 
            // lblMonthBatchNo
            // 
            this.lblMonthBatchNo.AutoSize = true;
            this.lblMonthBatchNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthBatchNo.Location = new System.Drawing.Point(288, 79);
            this.lblMonthBatchNo.Name = "lblMonthBatchNo";
            this.lblMonthBatchNo.Size = new System.Drawing.Size(103, 13);
            this.lblMonthBatchNo.TabIndex = 221;
            this.lblMonthBatchNo.Text = "Month Batch No.";
            // 
            // cmbMonth
            // 
            this.cmbMonth.BackColor = System.Drawing.Color.White;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(143, 75);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(134, 21);
            this.cmbMonth.TabIndex = 217;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonthBatchNo_SelectedIndexChanged);
            // 
            // lblSelectMonthCaption
            // 
            this.lblSelectMonthCaption.AutoSize = true;
            this.lblSelectMonthCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectMonthCaption.Location = new System.Drawing.Point(58, 79);
            this.lblSelectMonthCaption.Name = "lblSelectMonthCaption";
            this.lblSelectMonthCaption.Size = new System.Drawing.Size(82, 13);
            this.lblSelectMonthCaption.TabIndex = 218;
            this.lblSelectMonthCaption.Text = "Select Month";
            // 
            // txtDeductorType
            // 
            this.txtDeductorType.Location = new System.Drawing.Point(665, 76);
            this.txtDeductorType.Name = "txtDeductorType";
            this.txtDeductorType.Size = new System.Drawing.Size(10, 20);
            this.txtDeductorType.TabIndex = 21;
            this.txtDeductorType.Visible = false;
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(577, 25);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(75, 21);
            this.cmbFormNo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(474, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select Form No.";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(386, 25);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(74, 21);
            this.cmbQuarter.TabIndex = 1;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.cmbQuarter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(292, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select Quarter";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(143, 25);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 0;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbMonthBatchNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(143, 50);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(524, 21);
            this.cmbCompany.TabIndex = 3;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbMonthBatchNo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select Company";
            // 
            // pnlLIne
            // 
            this.pnlLIne.BackColor = System.Drawing.Color.Black;
            this.pnlLIne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLIne.Location = new System.Drawing.Point(24, 117);
            this.pnlLIne.Name = "pnlLIne";
            this.pnlLIne.Size = new System.Drawing.Size(954, 2);
            this.pnlLIne.TabIndex = 69;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Honeydew;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(954, 22);
            this.label8.TabIndex = 187;
            this.label8.Text = "Monthly Data";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvEmployeeList
            // 
            this.dgvEmployeeList.AllowUserToAddRows = false;
            this.dgvEmployeeList.AllowUserToDeleteRows = false;
            this.dgvEmployeeList.AllowUserToOrderColumns = true;
            this.dgvEmployeeList.AllowUserToResizeRows = false;
            this.dgvEmployeeList.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvEmployeeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEmployeeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEmployeeList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvEmployeeList.Location = new System.Drawing.Point(24, 143);
            this.dgvEmployeeList.MultiSelect = false;
            this.dgvEmployeeList.Name = "dgvEmployeeList";
            this.dgvEmployeeList.RowHeadersWidth = 20;
            this.dgvEmployeeList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEmployeeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployeeList.Size = new System.Drawing.Size(954, 371);
            this.dgvEmployeeList.TabIndex = 186;
            this.dgvEmployeeList.TabStop = false;
            // 
            // bgwProcessMonthlyTDS
            // 
            this.bgwProcessMonthlyTDS.WorkerReportsProgress = true;
            this.bgwProcessMonthlyTDS.WorkerSupportsCancellation = true;
            this.bgwProcessMonthlyTDS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcessMonthlyTDS_DoWork);
            this.bgwProcessMonthlyTDS.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwProcessMonthlyTDS_ProgressChanged);
            this.bgwProcessMonthlyTDS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcessMonthlyTDS_RunWorkerCompleted);
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.Color.SeaShell;
            this.pnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProcess.Controls.Add(this.btnStopProcessing);
            this.pnlProcess.Controls.Add(this.lblProcessStatus);
            this.pnlProcess.Controls.Add(this.lblProcessCounter);
            this.pnlProcess.Location = new System.Drawing.Point(352, 263);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(322, 100);
            this.pnlProcess.TabIndex = 50;
            this.pnlProcess.Visible = false;
            // 
            // btnStopProcessing
            // 
            this.btnStopProcessing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnStopProcessing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopProcessing.Location = new System.Drawing.Point(211, 74);
            this.btnStopProcessing.Name = "btnStopProcessing";
            this.btnStopProcessing.Size = new System.Drawing.Size(108, 24);
            this.btnStopProcessing.TabIndex = 2;
            this.btnStopProcessing.Text = "Stop Processing";
            this.btnStopProcessing.UseVisualStyleBackColor = false;
            this.btnStopProcessing.Click += new System.EventHandler(this.btnStopProcessing_Click);
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblProcessStatus.Location = new System.Drawing.Point(5, 4);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(102, 15);
            this.lblProcessStatus.TabIndex = 1;
            this.lblProcessStatus.Text = "Process Status";
            // 
            // lblProcessCounter
            // 
            this.lblProcessCounter.AutoSize = true;
            this.lblProcessCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessCounter.ForeColor = System.Drawing.Color.Red;
            this.lblProcessCounter.Location = new System.Drawing.Point(35, 39);
            this.lblProcessCounter.Name = "lblProcessCounter";
            this.lblProcessCounter.Size = new System.Drawing.Size(47, 15);
            this.lblProcessCounter.TabIndex = 0;
            this.lblProcessCounter.Text = "label4";
            this.lblProcessCounter.Visible = false;
            // 
            // lblTotalPaymentAmount
            // 
            this.lblTotalPaymentAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPaymentAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalPaymentAmount.Location = new System.Drawing.Point(130, 525);
            this.lblTotalPaymentAmount.Name = "lblTotalPaymentAmount";
            this.lblTotalPaymentAmount.Size = new System.Drawing.Size(314, 16);
            this.lblTotalPaymentAmount.TabIndex = 188;
            this.lblTotalPaymentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalTDS
            // 
            this.lblTotalTDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTDS.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalTDS.Location = new System.Drawing.Point(694, 525);
            this.lblTotalTDS.Name = "lblTotalTDS";
            this.lblTotalTDS.Size = new System.Drawing.Size(266, 16);
            this.lblTotalTDS.TabIndex = 189;
            this.lblTotalTDS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProcessedRecords
            // 
            this.lblProcessedRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessedRecords.ForeColor = System.Drawing.Color.Green;
            this.lblProcessedRecords.Location = new System.Drawing.Point(460, 525);
            this.lblProcessedRecords.Name = "lblProcessedRecords";
            this.lblProcessedRecords.Size = new System.Drawing.Size(233, 15);
            this.lblProcessedRecords.TabIndex = 190;
            this.lblProcessedRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHideProcess
            // 
            this.pnlHideProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHideProcess.Controls.Add(this.chkHideThisProcess);
            this.pnlHideProcess.Location = new System.Drawing.Point(855, 12);
            this.pnlHideProcess.Name = "pnlHideProcess";
            this.pnlHideProcess.Size = new System.Drawing.Size(140, 29);
            this.pnlHideProcess.TabIndex = 13;
            this.pnlHideProcess.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHideProcess_MouseMove);
            // 
            // chkHideThisProcess
            // 
            this.chkHideThisProcess.AutoSize = true;
            this.chkHideThisProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkHideThisProcess.Enabled = false;
            this.chkHideThisProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideThisProcess.Location = new System.Drawing.Point(4, 5);
            this.chkHideThisProcess.Name = "chkHideThisProcess";
            this.chkHideThisProcess.Size = new System.Drawing.Size(125, 17);
            this.chkHideThisProcess.TabIndex = 13;
            this.chkHideThisProcess.Text = "Hide this Process";
            this.chkHideThisProcess.UseVisualStyleBackColor = true;
            this.chkHideThisProcess.CheckedChanged += new System.EventHandler(this.chkHideThisProcess_CheckedChanged);
            // 
            // TrnProcessMonthlyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Controls.Add(this.pnlProcess);
            this.Name = "TrnProcessMonthlyData";
            this.Load += new System.EventHandler(this.TrnProcessMonthlyData_Load);
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
            this.Controls.SetChildIndex(this.pnlProcess, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).EndInit();
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            this.pnlHideProcess.ResumeLayout(false);
            this.pnlHideProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbMonthSerialNo;
        private System.Windows.Forms.Label lblMonthBatchNo;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label lblSelectMonthCaption;
        private System.Windows.Forms.TextBox txtDeductorType;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadEmployees;
        private System.Windows.Forms.Panel pnlLIne;
        private System.Windows.Forms.Label label8;
        private DGVControl.DGVControl dgvEmployeeList;
        private System.ComponentModel.BackgroundWorker bgwProcessMonthlyTDS;
        private System.Windows.Forms.Panel pnlProcess;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.Label lblProcessCounter;
        private System.Windows.Forms.Button btnStopProcessing;
        private System.Windows.Forms.Label lblTotalPaymentAmount;
        private System.Windows.Forms.Label lblTotalTDS;
        private System.Windows.Forms.Label lblProcessedRecords;
        private System.Windows.Forms.Panel pnlHideProcess;
        private System.Windows.Forms.CheckBox chkHideThisProcess;
    }
}
