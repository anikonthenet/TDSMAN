namespace TDSMAN.FormTrn
{
    partial class TrnHRACalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnHRACalculator));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnPrintCalculation = new System.Windows.Forms.Button();
            this.lblTotalSalary = new System.Windows.Forms.Label();
            this.lblEmployeePANName = new System.Windows.Forms.Label();
            this.lnkTaxCalculationVisitSite = new System.Windows.Forms.LinkLabel();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.txtTotalHRATaxable = new System.Windows.Forms.TextBox();
            this.txtTotalHRAExempted = new System.Windows.Forms.TextBox();
            this.txtTotalRentPaid = new System.Windows.Forms.TextBox();
            this.txtTotalHRAReceived = new System.Windows.Forms.TextBox();
            this.txtTotalSalary = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalCommission = new System.Windows.Forms.TextBox();
            this.txtTotalDA = new System.Windows.Forms.TextBox();
            this.txtTotalBasic = new System.Windows.Forms.TextBox();
            this.grpTaxCalculation = new System.Windows.Forms.GroupBox();
            this.dgvViewCalculation = new DGVControl.DGVControl();
            this.grpSalaryDetails = new System.Windows.Forms.GroupBox();
            this.lblExemptedHRA = new System.Windows.Forms.Label();
            this.lblActualRent = new System.Windows.Forms.Label();
            this.lblTaxableHRA = new System.Windows.Forms.Label();
            this.lblMetroHRA = new System.Windows.Forms.Label();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkMetroCity = new System.Windows.Forms.CheckBox();
            this.txtRentPaidAmount = new System.Windows.Forms.TextBox();
            this.txtHRAReceivedAmount = new System.Windows.Forms.TextBox();
            this.lblRentPaid = new System.Windows.Forms.Label();
            this.lblHRAReceived = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCommissionAmount = new System.Windows.Forms.TextBox();
            this.lblCommission = new System.Windows.Forms.Label();
            this.txtDAFormingPartOfSalary = new System.Windows.Forms.TextBox();
            this.txtBasicSalary = new System.Windows.Forms.TextBox();
            this.lblDAFormingPartOfSalary = new System.Windows.Forms.Label();
            this.lblBasicSalary = new System.Windows.Forms.Label();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnSort = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.grpTaxCalculation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewCalculation)).BeginInit();
            this.grpSalaryDetails.SuspendLayout();
            this.grpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(838, 11);
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
            this.pnlTitle.Location = new System.Drawing.Point(183, 11);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(656, 24);
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
            this.lblTitle.Size = new System.Drawing.Size(654, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "HRA Calculator";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(9, 11);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 47;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.btnPrintCalculation);
            this.pnlControls.Controls.Add(this.lblTotalSalary);
            this.pnlControls.Controls.Add(this.lblEmployeePANName);
            this.pnlControls.Controls.Add(this.lnkTaxCalculationVisitSite);
            this.pnlControls.Controls.Add(this.grpMain);
            this.pnlControls.Location = new System.Drawing.Point(9, 38);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1003, 568);
            this.pnlControls.TabIndex = 0;
            // 
            // btnPrintCalculation
            // 
            this.btnPrintCalculation.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintCalculation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintCalculation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintCalculation.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintCalculation.Location = new System.Drawing.Point(847, 538);
            this.btnPrintCalculation.Name = "btnPrintCalculation";
            this.btnPrintCalculation.Size = new System.Drawing.Size(103, 25);
            this.btnPrintCalculation.TabIndex = 315;
            this.btnPrintCalculation.Text = "Print to Excel";
            this.btnPrintCalculation.UseVisualStyleBackColor = false;
            this.btnPrintCalculation.Click += new System.EventHandler(this.btnPrintCalculation_Click);
            // 
            // lblTotalSalary
            // 
            this.lblTotalSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalary.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalSalary.Location = new System.Drawing.Point(699, 89);
            this.lblTotalSalary.Name = "lblTotalSalary";
            this.lblTotalSalary.Size = new System.Drawing.Size(126, 20);
            this.lblTotalSalary.TabIndex = 308;
            this.lblTotalSalary.Text = "0.00";
            this.lblTotalSalary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmployeePANName
            // 
            this.lblEmployeePANName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeePANName.ForeColor = System.Drawing.Color.Blue;
            this.lblEmployeePANName.Location = new System.Drawing.Point(171, 3);
            this.lblEmployeePANName.Name = "lblEmployeePANName";
            this.lblEmployeePANName.Size = new System.Drawing.Size(658, 18);
            this.lblEmployeePANName.TabIndex = 304;
            this.lblEmployeePANName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmployeePANName.Visible = false;
            // 
            // lnkTaxCalculationVisitSite
            // 
            this.lnkTaxCalculationVisitSite.AutoSize = true;
            this.lnkTaxCalculationVisitSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTaxCalculationVisitSite.Location = new System.Drawing.Point(5, 548);
            this.lnkTaxCalculationVisitSite.Name = "lnkTaxCalculationVisitSite";
            this.lnkTaxCalculationVisitSite.Size = new System.Drawing.Size(358, 13);
            this.lnkTaxCalculationVisitSite.TabIndex = 256;
            this.lnkTaxCalculationVisitSite.TabStop = true;
            this.lnkTaxCalculationVisitSite.Text = "Click here to check the calculation from Income Tax Website.\r\n";
            this.lnkTaxCalculationVisitSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTaxCalculationVisitSite_LinkClicked);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.txtTotalHRATaxable);
            this.grpMain.Controls.Add(this.txtTotalHRAExempted);
            this.grpMain.Controls.Add(this.txtTotalRentPaid);
            this.grpMain.Controls.Add(this.txtTotalHRAReceived);
            this.grpMain.Controls.Add(this.txtTotalSalary);
            this.grpMain.Controls.Add(this.panel2);
            this.grpMain.Controls.Add(this.txtTotalCommission);
            this.grpMain.Controls.Add(this.txtTotalDA);
            this.grpMain.Controls.Add(this.txtTotalBasic);
            this.grpMain.Controls.Add(this.grpTaxCalculation);
            this.grpMain.Controls.Add(this.grpSalaryDetails);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.Location = new System.Drawing.Point(50, 11);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(900, 524);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            // 
            // txtTotalHRATaxable
            // 
            this.txtTotalHRATaxable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalHRATaxable.Location = new System.Drawing.Point(765, 498);
            this.txtTotalHRATaxable.Name = "txtTotalHRATaxable";
            this.txtTotalHRATaxable.ReadOnly = true;
            this.txtTotalHRATaxable.Size = new System.Drawing.Size(100, 20);
            this.txtTotalHRATaxable.TabIndex = 319;
            this.txtTotalHRATaxable.Text = "0.00";
            this.txtTotalHRATaxable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalHRAExempted
            // 
            this.txtTotalHRAExempted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalHRAExempted.Location = new System.Drawing.Point(662, 498);
            this.txtTotalHRAExempted.Name = "txtTotalHRAExempted";
            this.txtTotalHRAExempted.ReadOnly = true;
            this.txtTotalHRAExempted.Size = new System.Drawing.Size(101, 20);
            this.txtTotalHRAExempted.TabIndex = 318;
            this.txtTotalHRAExempted.Text = "0.00";
            this.txtTotalHRAExempted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalRentPaid
            // 
            this.txtTotalRentPaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalRentPaid.Location = new System.Drawing.Point(560, 498);
            this.txtTotalRentPaid.Name = "txtTotalRentPaid";
            this.txtTotalRentPaid.ReadOnly = true;
            this.txtTotalRentPaid.Size = new System.Drawing.Size(100, 20);
            this.txtTotalRentPaid.TabIndex = 317;
            this.txtTotalRentPaid.Text = "0.00";
            this.txtTotalRentPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalHRAReceived
            // 
            this.txtTotalHRAReceived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalHRAReceived.Location = new System.Drawing.Point(457, 498);
            this.txtTotalHRAReceived.Name = "txtTotalHRAReceived";
            this.txtTotalHRAReceived.ReadOnly = true;
            this.txtTotalHRAReceived.Size = new System.Drawing.Size(101, 20);
            this.txtTotalHRAReceived.TabIndex = 316;
            this.txtTotalHRAReceived.Text = "0.00";
            this.txtTotalHRAReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalSalary
            // 
            this.txtTotalSalary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalSalary.Location = new System.Drawing.Point(289, 498);
            this.txtTotalSalary.Name = "txtTotalSalary";
            this.txtTotalSalary.ReadOnly = true;
            this.txtTotalSalary.Size = new System.Drawing.Size(103, 20);
            this.txtTotalSalary.TabIndex = 315;
            this.txtTotalSalary.Text = "0.00";
            this.txtTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(35, 495);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 1);
            this.panel2.TabIndex = 314;
            // 
            // txtTotalCommission
            // 
            this.txtTotalCommission.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtTotalCommission.Location = new System.Drawing.Point(308, 473);
            this.txtTotalCommission.Name = "txtTotalCommission";
            this.txtTotalCommission.ReadOnly = true;
            this.txtTotalCommission.Size = new System.Drawing.Size(84, 20);
            this.txtTotalCommission.TabIndex = 313;
            this.txtTotalCommission.Text = "0.00";
            this.txtTotalCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalDA
            // 
            this.txtTotalDA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtTotalDA.Location = new System.Drawing.Point(223, 473);
            this.txtTotalDA.Name = "txtTotalDA";
            this.txtTotalDA.ReadOnly = true;
            this.txtTotalDA.Size = new System.Drawing.Size(84, 20);
            this.txtTotalDA.TabIndex = 312;
            this.txtTotalDA.Text = "0.00";
            this.txtTotalDA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalBasic
            // 
            this.txtTotalBasic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtTotalBasic.Location = new System.Drawing.Point(108, 473);
            this.txtTotalBasic.Name = "txtTotalBasic";
            this.txtTotalBasic.ReadOnly = true;
            this.txtTotalBasic.Size = new System.Drawing.Size(114, 20);
            this.txtTotalBasic.TabIndex = 311;
            this.txtTotalBasic.Text = "0.00";
            this.txtTotalBasic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpTaxCalculation
            // 
            this.grpTaxCalculation.Controls.Add(this.dgvViewCalculation);
            this.grpTaxCalculation.ForeColor = System.Drawing.Color.Blue;
            this.grpTaxCalculation.Location = new System.Drawing.Point(8, 155);
            this.grpTaxCalculation.Name = "grpTaxCalculation";
            this.grpTaxCalculation.Size = new System.Drawing.Size(886, 316);
            this.grpTaxCalculation.TabIndex = 1;
            this.grpTaxCalculation.TabStop = false;
            this.grpTaxCalculation.Text = "Calculation";
            // 
            // dgvViewCalculation
            // 
            this.dgvViewCalculation.AllowUserToAddRows = false;
            this.dgvViewCalculation.AllowUserToDeleteRows = false;
            this.dgvViewCalculation.AllowUserToResizeColumns = false;
            this.dgvViewCalculation.AllowUserToResizeRows = false;
            this.dgvViewCalculation.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvViewCalculation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViewCalculation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvViewCalculation.GridColor = System.Drawing.SystemColors.Control;
            this.dgvViewCalculation.Location = new System.Drawing.Point(5, 15);
            this.dgvViewCalculation.MultiSelect = false;
            this.dgvViewCalculation.Name = "dgvViewCalculation";
            this.dgvViewCalculation.RowHeadersWidth = 20;
            this.dgvViewCalculation.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvViewCalculation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvViewCalculation.Size = new System.Drawing.Size(875, 296);
            this.dgvViewCalculation.TabIndex = 186;
            this.dgvViewCalculation.TabStop = false;
            this.dgvViewCalculation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViewCalculation_CellClick);
            this.dgvViewCalculation.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViewCalculation_CellLeave);
            this.dgvViewCalculation.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViewCalculation_CellValueChanged);
            // 
            // grpSalaryDetails
            // 
            this.grpSalaryDetails.Controls.Add(this.lblExemptedHRA);
            this.grpSalaryDetails.Controls.Add(this.lblActualRent);
            this.grpSalaryDetails.Controls.Add(this.lblTaxableHRA);
            this.grpSalaryDetails.Controls.Add(this.lblMetroHRA);
            this.grpSalaryDetails.Controls.Add(this.btnCalculation);
            this.grpSalaryDetails.Controls.Add(this.btnReset);
            this.grpSalaryDetails.Controls.Add(this.chkMetroCity);
            this.grpSalaryDetails.Controls.Add(this.txtRentPaidAmount);
            this.grpSalaryDetails.Controls.Add(this.txtHRAReceivedAmount);
            this.grpSalaryDetails.Controls.Add(this.lblRentPaid);
            this.grpSalaryDetails.Controls.Add(this.lblHRAReceived);
            this.grpSalaryDetails.Controls.Add(this.panel1);
            this.grpSalaryDetails.Controls.Add(this.txtCommissionAmount);
            this.grpSalaryDetails.Controls.Add(this.lblCommission);
            this.grpSalaryDetails.Controls.Add(this.txtDAFormingPartOfSalary);
            this.grpSalaryDetails.Controls.Add(this.txtBasicSalary);
            this.grpSalaryDetails.Controls.Add(this.lblDAFormingPartOfSalary);
            this.grpSalaryDetails.Controls.Add(this.lblBasicSalary);
            this.grpSalaryDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpSalaryDetails.Location = new System.Drawing.Point(108, 14);
            this.grpSalaryDetails.Name = "grpSalaryDetails";
            this.grpSalaryDetails.Size = new System.Drawing.Size(693, 141);
            this.grpSalaryDetails.TabIndex = 0;
            this.grpSalaryDetails.TabStop = false;
            this.grpSalaryDetails.Text = "Enter Monthly Details";
            // 
            // lblExemptedHRA
            // 
            this.lblExemptedHRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExemptedHRA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExemptedHRA.ForeColor = System.Drawing.Color.Blue;
            this.lblExemptedHRA.Location = new System.Drawing.Point(627, 31);
            this.lblExemptedHRA.Name = "lblExemptedHRA";
            this.lblExemptedHRA.Size = new System.Drawing.Size(30, 18);
            this.lblExemptedHRA.TabIndex = 319;
            this.lblExemptedHRA.Text = "0.00";
            this.lblExemptedHRA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExemptedHRA.Visible = false;
            // 
            // lblActualRent
            // 
            this.lblActualRent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblActualRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualRent.ForeColor = System.Drawing.Color.Blue;
            this.lblActualRent.Location = new System.Drawing.Point(627, 13);
            this.lblActualRent.Name = "lblActualRent";
            this.lblActualRent.Size = new System.Drawing.Size(30, 18);
            this.lblActualRent.TabIndex = 318;
            this.lblActualRent.Text = "0.00";
            this.lblActualRent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblActualRent.Visible = false;
            // 
            // lblTaxableHRA
            // 
            this.lblTaxableHRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaxableHRA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxableHRA.ForeColor = System.Drawing.Color.Blue;
            this.lblTaxableHRA.Location = new System.Drawing.Point(657, 31);
            this.lblTaxableHRA.Name = "lblTaxableHRA";
            this.lblTaxableHRA.Size = new System.Drawing.Size(30, 18);
            this.lblTaxableHRA.TabIndex = 317;
            this.lblTaxableHRA.Text = "0.00";
            this.lblTaxableHRA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTaxableHRA.Visible = false;
            // 
            // lblMetroHRA
            // 
            this.lblMetroHRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMetroHRA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetroHRA.ForeColor = System.Drawing.Color.Blue;
            this.lblMetroHRA.Location = new System.Drawing.Point(657, 13);
            this.lblMetroHRA.Name = "lblMetroHRA";
            this.lblMetroHRA.Size = new System.Drawing.Size(30, 18);
            this.lblMetroHRA.TabIndex = 316;
            this.lblMetroHRA.Text = "0.00";
            this.lblMetroHRA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMetroHRA.Visible = false;
            // 
            // btnCalculation
            // 
            this.btnCalculation.BackColor = System.Drawing.Color.Lavender;
            this.btnCalculation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalculation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCalculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculation.Location = new System.Drawing.Point(555, 111);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(78, 25);
            this.btnCalculation.TabIndex = 315;
            this.btnCalculation.Text = "Calculate";
            this.btnCalculation.UseVisualStyleBackColor = false;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Lavender;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(636, 111);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(51, 25);
            this.btnReset.TabIndex = 314;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkMetroCity
            // 
            this.chkMetroCity.AutoSize = true;
            this.chkMetroCity.ForeColor = System.Drawing.Color.Black;
            this.chkMetroCity.Location = new System.Drawing.Point(540, 93);
            this.chkMetroCity.Name = "chkMetroCity";
            this.chkMetroCity.Size = new System.Drawing.Size(150, 17);
            this.chkMetroCity.TabIndex = 313;
            this.chkMetroCity.Text = "Residing in Metro City";
            this.chkMetroCity.UseVisualStyleBackColor = true;
            this.chkMetroCity.CheckedChanged += new System.EventHandler(this.chkMetroCity_CheckedChanged);
            // 
            // txtRentPaidAmount
            // 
            this.txtRentPaidAmount.Location = new System.Drawing.Point(409, 115);
            this.txtRentPaidAmount.Name = "txtRentPaidAmount";
            this.txtRentPaidAmount.Size = new System.Drawing.Size(129, 20);
            this.txtRentPaidAmount.TabIndex = 310;
            this.txtRentPaidAmount.Text = "0.00";
            this.txtRentPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRentPaidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRentPaidAmount_KeyPress);
            this.txtRentPaidAmount.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtHRAReceivedAmount
            // 
            this.txtHRAReceivedAmount.Location = new System.Drawing.Point(409, 91);
            this.txtHRAReceivedAmount.Name = "txtHRAReceivedAmount";
            this.txtHRAReceivedAmount.Size = new System.Drawing.Size(129, 20);
            this.txtHRAReceivedAmount.TabIndex = 309;
            this.txtHRAReceivedAmount.Text = "0.00";
            this.txtHRAReceivedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHRAReceivedAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHRAReceivedAmount_KeyPress);
            this.txtHRAReceivedAmount.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // lblRentPaid
            // 
            this.lblRentPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentPaid.ForeColor = System.Drawing.Color.Black;
            this.lblRentPaid.Location = new System.Drawing.Point(66, 116);
            this.lblRentPaid.Name = "lblRentPaid";
            this.lblRentPaid.Size = new System.Drawing.Size(329, 18);
            this.lblRentPaid.TabIndex = 312;
            this.lblRentPaid.Text = "Rent Paid";
            // 
            // lblHRAReceived
            // 
            this.lblHRAReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHRAReceived.ForeColor = System.Drawing.Color.Black;
            this.lblHRAReceived.Location = new System.Drawing.Point(66, 92);
            this.lblHRAReceived.Name = "lblHRAReceived";
            this.lblHRAReceived.Size = new System.Drawing.Size(329, 18);
            this.lblHRAReceived.TabIndex = 311;
            this.lblHRAReceived.Text = "HRA Received";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(52, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 1);
            this.panel1.TabIndex = 308;
            // 
            // txtCommissionAmount
            // 
            this.txtCommissionAmount.Location = new System.Drawing.Point(409, 62);
            this.txtCommissionAmount.Name = "txtCommissionAmount";
            this.txtCommissionAmount.Size = new System.Drawing.Size(129, 20);
            this.txtCommissionAmount.TabIndex = 306;
            this.txtCommissionAmount.Text = "0.00";
            this.txtCommissionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCommissionAmount.TextChanged += new System.EventHandler(this.CalcTotalSalary);
            this.txtCommissionAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommissionAmount_KeyPress);
            this.txtCommissionAmount.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // lblCommission
            // 
            this.lblCommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommission.ForeColor = System.Drawing.Color.Black;
            this.lblCommission.Location = new System.Drawing.Point(64, 64);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(329, 18);
            this.lblCommission.TabIndex = 307;
            this.lblCommission.Text = "Commission (as % of turnover achieved by the employee)";
            // 
            // txtDAFormingPartOfSalary
            // 
            this.txtDAFormingPartOfSalary.Location = new System.Drawing.Point(409, 39);
            this.txtDAFormingPartOfSalary.Name = "txtDAFormingPartOfSalary";
            this.txtDAFormingPartOfSalary.Size = new System.Drawing.Size(129, 20);
            this.txtDAFormingPartOfSalary.TabIndex = 3;
            this.txtDAFormingPartOfSalary.Text = "0.00";
            this.txtDAFormingPartOfSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDAFormingPartOfSalary.TextChanged += new System.EventHandler(this.CalcTotalSalary);
            this.txtDAFormingPartOfSalary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDAFormingPartOfSalary_KeyPress);
            this.txtDAFormingPartOfSalary.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtBasicSalary
            // 
            this.txtBasicSalary.Location = new System.Drawing.Point(409, 15);
            this.txtBasicSalary.Name = "txtBasicSalary";
            this.txtBasicSalary.Size = new System.Drawing.Size(129, 20);
            this.txtBasicSalary.TabIndex = 2;
            this.txtBasicSalary.Text = "0.00";
            this.txtBasicSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBasicSalary.TextChanged += new System.EventHandler(this.CalcTotalSalary);
            this.txtBasicSalary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBasicSalary_KeyPress);
            this.txtBasicSalary.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // lblDAFormingPartOfSalary
            // 
            this.lblDAFormingPartOfSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDAFormingPartOfSalary.ForeColor = System.Drawing.Color.Black;
            this.lblDAFormingPartOfSalary.Location = new System.Drawing.Point(64, 40);
            this.lblDAFormingPartOfSalary.Name = "lblDAFormingPartOfSalary";
            this.lblDAFormingPartOfSalary.Size = new System.Drawing.Size(327, 18);
            this.lblDAFormingPartOfSalary.TabIndex = 303;
            this.lblDAFormingPartOfSalary.Text = "DA forming part of Salary";
            // 
            // lblBasicSalary
            // 
            this.lblBasicSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalary.ForeColor = System.Drawing.Color.Black;
            this.lblBasicSalary.Location = new System.Drawing.Point(64, 16);
            this.lblBasicSalary.Name = "lblBasicSalary";
            this.lblBasicSalary.Size = new System.Drawing.Size(329, 18);
            this.lblBasicSalary.TabIndex = 302;
            this.lblBasicSalary.Text = "Basic Salary";
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Controls.Add(this.BtnExit);
            this.grpButton.Controls.Add(this.BtnPrint);
            this.grpButton.Controls.Add(this.BtnSort);
            this.grpButton.Location = new System.Drawing.Point(9, 607);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(1002, 48);
            this.grpButton.TabIndex = 1;
            this.grpButton.TabStop = false;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(918, 11);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(80, 32);
            this.pctVideoDemo.TabIndex = 205;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Visible = false;
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnExit.Location = new System.Drawing.Point(460, 16);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnPrint.Location = new System.Drawing.Point(963, 16);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(13, 23);
            this.BtnPrint.TabIndex = 8;
            this.BtnPrint.Text = "&Print";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Visible = false;
            // 
            // BtnSort
            // 
            this.BtnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnSort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnSort.Location = new System.Drawing.Point(975, 16);
            this.BtnSort.Name = "BtnSort";
            this.BtnSort.Size = new System.Drawing.Size(15, 23);
            this.BtnSort.TabIndex = 4;
            this.BtnSort.Text = "Sor&t";
            this.BtnSort.UseVisualStyleBackColor = false;
            this.BtnSort.Visible = false;
            // 
            // TrnHRACalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 659);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Name = "TrnHRACalculator";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.TrnMonthlyTDSCalculatorSummary_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpTaxCalculation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewCalculation)).EndInit();
            this.grpSalaryDetails.ResumeLayout(false);
            this.grpSalaryDetails.PerformLayout();
            this.grpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlControls;
        public System.Windows.Forms.GroupBox grpButton;
        public System.Windows.Forms.Button BtnPrint;
        public System.Windows.Forms.Button BtnSort;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.GroupBox grpTaxCalculation;
        private System.Windows.Forms.GroupBox grpSalaryDetails;
        private System.Windows.Forms.TextBox txtDAFormingPartOfSalary;
        private System.Windows.Forms.TextBox txtBasicSalary;
        private System.Windows.Forms.Label lblDAFormingPartOfSalary;
        private System.Windows.Forms.Label lblBasicSalary;
        private System.Windows.Forms.LinkLabel lnkTaxCalculationVisitSite;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.Label lblEmployeePANName;
        private System.Windows.Forms.TextBox txtCommissionAmount;
        private System.Windows.Forms.Label lblCommission;
        private System.Windows.Forms.Label lblTotalSalary;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRentPaidAmount;
        private System.Windows.Forms.TextBox txtHRAReceivedAmount;
        private System.Windows.Forms.Label lblRentPaid;
        private System.Windows.Forms.Label lblHRAReceived;
        private System.Windows.Forms.CheckBox chkMetroCity;
        private DGVControl.DGVControl dgvViewCalculation;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCalculation;
        private System.Windows.Forms.Label lblExemptedHRA;
        private System.Windows.Forms.Label lblActualRent;
        private System.Windows.Forms.Label lblTaxableHRA;
        private System.Windows.Forms.Label lblMetroHRA;
        private System.Windows.Forms.TextBox txtTotalCommission;
        private System.Windows.Forms.TextBox txtTotalDA;
        private System.Windows.Forms.TextBox txtTotalBasic;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTotalHRATaxable;
        private System.Windows.Forms.TextBox txtTotalHRAExempted;
        private System.Windows.Forms.TextBox txtTotalRentPaid;
        private System.Windows.Forms.TextBox txtTotalHRAReceived;
        private System.Windows.Forms.TextBox txtTotalSalary;
        private System.Windows.Forms.Button btnPrintCalculation;
    }
}