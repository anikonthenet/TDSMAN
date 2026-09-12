namespace TDSMAN.FormTrn
{
    partial class TrnPrintChallan281
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPrintChallan281));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDisplaySection = new System.Windows.Forms.Label();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbForm = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbMinorHead = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDrawnOn = new System.Windows.Forms.TextBox();
            this.mskDateOfPayment = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPaidCashOrDebitOrChequeNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.grpPaymentDetails = new System.Windows.Forms.GroupBox();
            this.txtTotalAmtFormat = new System.Windows.Forms.TextBox();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPenalty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInterest = new System.Windows.Forms.TextBox();
            this.txtEduCess = new System.Windows.Forms.TextBox();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.txtFeeUnderSec234E = new System.Windows.Forms.TextBox();
            this.txtIncomeTax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTypeofPayment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbAssessmentYear = new System.Windows.Forms.ComboBox();
            this.cmbCompanyDetails = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pctVideoDemo = new System.Windows.Forms.PictureBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpPaymentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(379, 647);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(34, 13);
            this.BtnCancel.Size = new System.Drawing.Size(9, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(25, 13);
            this.BtnSave.Size = new System.Drawing.Size(9, 23);
            this.BtnSave.Visible = false;
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 647);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(13, 13);
            this.BtnEdit.Size = new System.Drawing.Size(9, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(3, 13);
            this.BtnAdd.Size = new System.Drawing.Size(9, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(418, 13);
            this.BtnPrint.Size = new System.Drawing.Size(83, 23);
            this.BtnPrint.Visible = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(63, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(55, 13);
            this.BtnDelete.Size = new System.Drawing.Size(9, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(44, 13);
            this.BtnSearch.Size = new System.Drawing.Size(9, 23);
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Controls.Add(this.pctVideoDemo);
            this.grpButton.Location = new System.Drawing.Point(9, 608);
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
            this.grpButton.Controls.SetChildIndex(this.pctUserManual, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.groupBox2);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 39);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDisplaySection);
            this.groupBox2.Controls.Add(this.lnkSearchByTAN);
            this.groupBox2.Controls.Add(this.cmbForm);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.cmbMinorHead);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.grpPaymentDetails);
            this.groupBox2.Controls.Add(this.cmbTypeofPayment);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbSection);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmbAssessmentYear);
            this.groupBox2.Controls.Add(this.cmbCompanyDetails);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(130, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 498);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // lblDisplaySection
            // 
            this.lblDisplaySection.AutoSize = true;
            this.lblDisplaySection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDisplaySection.ForeColor = System.Drawing.Color.Blue;
            this.lblDisplaySection.Location = new System.Drawing.Point(278, 101);
            this.lblDisplaySection.Name = "lblDisplaySection";
            this.lblDisplaySection.Size = new System.Drawing.Size(0, 13);
            this.lblDisplaySection.TabIndex = 215;
            this.lblDisplaySection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDisplaySection.UseMnemonic = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(559, 48);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 214;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbForm
            // 
            this.cmbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbForm.FormattingEnabled = true;
            this.cmbForm.Location = new System.Drawing.Point(169, 71);
            this.cmbForm.Name = "cmbForm";
            this.cmbForm.Size = new System.Drawing.Size(103, 21);
            this.cmbForm.TabIndex = 2;
            this.cmbForm.SelectedIndexChanged += new System.EventHandler(this.cmbForm_SelectedIndexChanged);
            this.cmbForm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(32, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 16);
            this.label16.TabIndex = 39;
            this.label16.Text = "Select Form";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbMinorHead
            // 
            this.cmbMinorHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMinorHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbMinorHead.FormattingEnabled = true;
            this.cmbMinorHead.Location = new System.Drawing.Point(169, 149);
            this.cmbMinorHead.Name = "cmbMinorHead";
            this.cmbMinorHead.Size = new System.Drawing.Size(384, 21);
            this.cmbMinorHead.TabIndex = 5;
            this.cmbMinorHead.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(32, 150);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 16);
            this.label12.TabIndex = 37;
            this.label12.Text = "Minor Head";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtDrawnOn);
            this.groupBox1.Controls.Add(this.mskDateOfPayment);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtPaidCashOrDebitOrChequeNo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(35, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 97);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Info";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(10, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(208, 14);
            this.label15.TabIndex = 42;
            this.label15.Text = "Drawn on";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDrawnOn
            // 
            this.txtDrawnOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDrawnOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtDrawnOn.Location = new System.Drawing.Point(224, 69);
            this.txtDrawnOn.MaxLength = 100;
            this.txtDrawnOn.Name = "txtDrawnOn";
            this.txtDrawnOn.Size = new System.Drawing.Size(453, 20);
            this.txtDrawnOn.TabIndex = 2;
            this.txtDrawnOn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // mskDateOfPayment
            // 
            this.mskDateOfPayment.BackColor = System.Drawing.SystemColors.Window;
            this.mskDateOfPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskDateOfPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mskDateOfPayment.Location = new System.Drawing.Point(224, 44);
            this.mskDateOfPayment.Mask = "00/00/0000";
            this.mskDateOfPayment.Name = "mskDateOfPayment";
            this.mskDateOfPayment.Size = new System.Drawing.Size(80, 20);
            this.mskDateOfPayment.TabIndex = 1;
            this.mskDateOfPayment.ValidatingType = typeof(System.DateTime);
            this.mskDateOfPayment.TextChanged += new System.EventHandler(this.mskDateOfPayment_TextChanged);
            this.mskDateOfPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(10, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(208, 14);
            this.label14.TabIndex = 39;
            this.label14.Text = "Dated";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPaidCashOrDebitOrChequeNo
            // 
            this.txtPaidCashOrDebitOrChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaidCashOrDebitOrChequeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtPaidCashOrDebitOrChequeNo.Location = new System.Drawing.Point(224, 19);
            this.txtPaidCashOrDebitOrChequeNo.MaxLength = 20;
            this.txtPaidCashOrDebitOrChequeNo.Name = "txtPaidCashOrDebitOrChequeNo";
            this.txtPaidCashOrDebitOrChequeNo.Size = new System.Drawing.Size(153, 20);
            this.txtPaidCashOrDebitOrChequeNo.TabIndex = 0;
            this.txtPaidCashOrDebitOrChequeNo.TextChanged += new System.EventHandler(this.txtPaidCashOrDebitOrChequeNo_TextChanged);
            this.txtPaidCashOrDebitOrChequeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(10, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(208, 14);
            this.label13.TabIndex = 0;
            this.label13.Text = "Paid in Cash/Debit to A/c /Cheque No.";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpPaymentDetails
            // 
            this.grpPaymentDetails.Controls.Add(this.txtTotalAmtFormat);
            this.grpPaymentDetails.Controls.Add(this.txtTotalAmt);
            this.grpPaymentDetails.Controls.Add(this.label10);
            this.grpPaymentDetails.Controls.Add(this.txtPenalty);
            this.grpPaymentDetails.Controls.Add(this.label9);
            this.grpPaymentDetails.Controls.Add(this.txtInterest);
            this.grpPaymentDetails.Controls.Add(this.txtEduCess);
            this.grpPaymentDetails.Controls.Add(this.txtSurcharge);
            this.grpPaymentDetails.Controls.Add(this.txtFeeUnderSec234E);
            this.grpPaymentDetails.Controls.Add(this.txtIncomeTax);
            this.grpPaymentDetails.Controls.Add(this.label8);
            this.grpPaymentDetails.Controls.Add(this.label7);
            this.grpPaymentDetails.Controls.Add(this.label6);
            this.grpPaymentDetails.Controls.Add(this.label5);
            this.grpPaymentDetails.Controls.Add(this.label4);
            this.grpPaymentDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpPaymentDetails.Location = new System.Drawing.Point(35, 182);
            this.grpPaymentDetails.Name = "grpPaymentDetails";
            this.grpPaymentDetails.Size = new System.Drawing.Size(518, 205);
            this.grpPaymentDetails.TabIndex = 6;
            this.grpPaymentDetails.TabStop = false;
            this.grpPaymentDetails.Text = "Payment Details";
            // 
            // txtTotalAmtFormat
            // 
            this.txtTotalAmtFormat.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalAmtFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmtFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmtFormat.Location = new System.Drawing.Point(272, 169);
            this.txtTotalAmtFormat.MaxLength = 0;
            this.txtTotalAmtFormat.Name = "txtTotalAmtFormat";
            this.txtTotalAmtFormat.ReadOnly = true;
            this.txtTotalAmtFormat.Size = new System.Drawing.Size(132, 20);
            this.txtTotalAmtFormat.TabIndex = 37;
            this.txtTotalAmtFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmtFormat.Visible = false;
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmt.Location = new System.Drawing.Point(134, 169);
            this.txtTotalAmt.MaxLength = 0;
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(132, 20);
            this.txtTotalAmt.TabIndex = 6;
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmt.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtTotalAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.txtTotalAmt.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(15, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 16);
            this.label10.TabIndex = 36;
            this.label10.Text = "Total";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPenalty
            // 
            this.txtPenalty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtPenalty.Location = new System.Drawing.Point(134, 144);
            this.txtPenalty.MaxLength = 9;
            this.txtPenalty.Name = "txtPenalty";
            this.txtPenalty.Size = new System.Drawing.Size(132, 20);
            this.txtPenalty.TabIndex = 5;
            this.txtPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPenalty.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtPenalty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPenalty_KeyPress);
            this.txtPenalty.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(15, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 16);
            this.label9.TabIndex = 34;
            this.label9.Text = "Penalty";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtInterest
            // 
            this.txtInterest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtInterest.Location = new System.Drawing.Point(134, 119);
            this.txtInterest.MaxLength = 9;
            this.txtInterest.Name = "txtInterest";
            this.txtInterest.Size = new System.Drawing.Size(132, 20);
            this.txtInterest.TabIndex = 4;
            this.txtInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInterest.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtInterest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterest_KeyPress);
            this.txtInterest.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtEduCess
            // 
            this.txtEduCess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEduCess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtEduCess.Location = new System.Drawing.Point(134, 95);
            this.txtEduCess.MaxLength = 9;
            this.txtEduCess.Name = "txtEduCess";
            this.txtEduCess.Size = new System.Drawing.Size(132, 20);
            this.txtEduCess.TabIndex = 3;
            this.txtEduCess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEduCess.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtEduCess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEduCess_KeyPress);
            this.txtEduCess.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSurcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtSurcharge.Location = new System.Drawing.Point(134, 71);
            this.txtSurcharge.MaxLength = 9;
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(132, 20);
            this.txtSurcharge.TabIndex = 2;
            this.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSurcharge.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtSurcharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSurcharge_KeyPress);
            this.txtSurcharge.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtFeeUnderSec234E
            // 
            this.txtFeeUnderSec234E.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFeeUnderSec234E.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtFeeUnderSec234E.Location = new System.Drawing.Point(134, 46);
            this.txtFeeUnderSec234E.MaxLength = 9;
            this.txtFeeUnderSec234E.Name = "txtFeeUnderSec234E";
            this.txtFeeUnderSec234E.Size = new System.Drawing.Size(132, 20);
            this.txtFeeUnderSec234E.TabIndex = 1;
            this.txtFeeUnderSec234E.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFeeUnderSec234E.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtFeeUnderSec234E.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeeUnderSec234E_KeyPress);
            this.txtFeeUnderSec234E.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // txtIncomeTax
            // 
            this.txtIncomeTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIncomeTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtIncomeTax.Location = new System.Drawing.Point(134, 22);
            this.txtIncomeTax.MaxLength = 9;
            this.txtIncomeTax.Name = "txtIncomeTax";
            this.txtIncomeTax.Size = new System.Drawing.Size(132, 20);
            this.txtIncomeTax.TabIndex = 0;
            this.txtIncomeTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIncomeTax.TextChanged += new System.EventHandler(this.txtIncomeTax_TextChanged);
            this.txtIncomeTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIncomeTax_KeyPress);
            this.txtIncomeTax.Leave += new System.EventHandler(this.NumericCurrencyControl_Leave);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(15, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "Interest";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(15, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = "Education Cess";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(15, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "Surcharge";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(15, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 16);
            this.label5.TabIndex = 25;
            this.label5.Text = "Fee under sec. 234E";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(15, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Income Tax";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbTypeofPayment
            // 
            this.cmbTypeofPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeofPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbTypeofPayment.FormattingEnabled = true;
            this.cmbTypeofPayment.Location = new System.Drawing.Point(169, 123);
            this.cmbTypeofPayment.Name = "cmbTypeofPayment";
            this.cmbTypeofPayment.Size = new System.Drawing.Size(384, 21);
            this.cmbTypeofPayment.TabIndex = 4;
            this.cmbTypeofPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(32, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "Type of Payment";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(169, 97);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(103, 21);
            this.cmbSection.TabIndex = 3;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            this.cmbSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(32, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 16);
            this.label11.TabIndex = 31;
            this.label11.Text = "Section";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbAssessmentYear
            // 
            this.cmbAssessmentYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssessmentYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbAssessmentYear.FormattingEnabled = true;
            this.cmbAssessmentYear.Location = new System.Drawing.Point(169, 20);
            this.cmbAssessmentYear.Name = "cmbAssessmentYear";
            this.cmbAssessmentYear.Size = new System.Drawing.Size(147, 21);
            this.cmbAssessmentYear.TabIndex = 0;
            this.cmbAssessmentYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbCompanyDetails
            // 
            this.cmbCompanyDetails.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbCompanyDetails.FormattingEnabled = true;
            this.cmbCompanyDetails.Location = new System.Drawing.Point(169, 45);
            this.cmbCompanyDetails.Name = "cmbCompanyDetails";
            this.cmbCompanyDetails.Size = new System.Drawing.Size(384, 21);
            this.cmbCompanyDetails.TabIndex = 1;
            this.cmbCompanyDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(32, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Select Company";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(32, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Select Tax Year";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pctVideoDemo
            // 
            this.pctVideoDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctVideoDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctVideoDemo.Image = ((System.Drawing.Image)(resources.GetObject("pctVideoDemo.Image")));
            this.pctVideoDemo.Location = new System.Drawing.Point(916, 8);
            this.pctVideoDemo.Name = "pctVideoDemo";
            this.pctVideoDemo.Size = new System.Drawing.Size(40, 32);
            this.pctVideoDemo.TabIndex = 193;
            this.pctVideoDemo.TabStop = false;
            this.pctVideoDemo.Tag = "Video Help";
            this.pctVideoDemo.Click += new System.EventHandler(this.pctVideoDemo_Click);
            this.pctVideoDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctVideoDemo_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(955, 8);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 194;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            this.pctUserManual.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctUserManual_MouseMove);
            // 
            // TrnPrintChallan281
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1036, 689);
            this.Name = "TrnPrintChallan281";
            this.Load += new System.EventHandler(this.TrnPrintChallan281_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpPaymentDetails.ResumeLayout(false);
            this.grpPaymentDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctVideoDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbMinorHead;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpPaymentDetails;
        private System.Windows.Forms.TextBox txtTotalAmt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPenalty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtInterest;
        private System.Windows.Forms.TextBox txtEduCess;
        private System.Windows.Forms.TextBox txtSurcharge;
        private System.Windows.Forms.TextBox txtFeeUnderSec234E;
        private System.Windows.Forms.TextBox txtIncomeTax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTypeofPayment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbAssessmentYear;
        private System.Windows.Forms.ComboBox cmbCompanyDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPaidCashOrDebitOrChequeNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox mskDateOfPayment;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDrawnOn;
        private System.Windows.Forms.ComboBox cmbForm;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.TextBox txtTotalAmtFormat;
        private System.Windows.Forms.PictureBox pctVideoDemo;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.Label lblDisplaySection;
    }
}
