namespace TDSMAN.FormWeb
{
    partial class TrnChallanPANCorrection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnChallanPANCorrection));
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.lstDeducteeHelp = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdAddChallanToStatement = new System.Windows.Forms.RadioButton();
            this.rdChallanCorrection = new System.Windows.Forms.RadioButton();
            this.rdPanCorrection = new System.Windows.Forms.RadioButton();
            this.grpChallanDeductee = new System.Windows.Forms.GroupBox();
            this.txtTokenNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grpDeductee = new System.Windows.Forms.GroupBox();
            this.txtTDSDeducted3 = new System.Windows.Forms.TextBox();
            this.txtTDSDeducted2 = new System.Windows.Forms.TextBox();
            this.txtTDSDeducted1 = new System.Windows.Forms.TextBox();
            this.txtDeducteePAN3 = new System.Windows.Forms.TextBox();
            this.txtDeducteePAN2 = new System.Windows.Forms.TextBox();
            this.txtDeducteePAN1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grpChallan = new System.Windows.Forms.GroupBox();
            this.btnChangeCDDDDetails = new System.Windows.Forms.Button();
            this.txtSlNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtChallanID = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.mskChallanDate = new System.Windows.Forms.MaskedTextBox();
            this.txtChallanTax = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBSRCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtChallanNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbQtr = new System.Windows.Forms.ComboBox();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.cmbFAYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTANNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.bgWorkerLoadCaptcha = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpControls.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpChallanDeductee.SuspendLayout();
            this.grpDeductee.SuspendLayout();
            this.grpChallan.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 567);
            this.grpSort.Size = new System.Drawing.Size(280, 6);
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
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Next";
            this.BtnSave.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 567);
            this.grpSearch.Size = new System.Drawing.Size(280, 6);
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
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Click += new System.EventHandler(this.btnLoginCancel_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Location = new System.Drawing.Point(9, 528);
            this.pnlFooter.Size = new System.Drawing.Size(1004, 3);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(86, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
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
            this.grpButton.Controls.Add(this.pctUserManual);
            this.grpButton.Location = new System.Drawing.Point(9, 530);
            this.grpButton.Size = new System.Drawing.Size(1004, 48);
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
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpControls);
            this.pnlControls.Location = new System.Drawing.Point(13, 42);
            this.pnlControls.Size = new System.Drawing.Size(1000, 478);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 510);
            this.ViewGrid.Size = new System.Drawing.Size(1004, 10);
            this.ViewGrid.Visible = false;
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.lstDeducteeHelp);
            this.grpControls.Controls.Add(this.groupBox3);
            this.grpControls.Controls.Add(this.grpChallanDeductee);
            this.grpControls.Controls.Add(this.groupBox2);
            this.grpControls.Controls.Add(this.groupBox1);
            this.grpControls.Location = new System.Drawing.Point(26, 11);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(949, 452);
            this.grpControls.TabIndex = 48;
            this.grpControls.TabStop = false;
            // 
            // lstDeducteeHelp
            // 
            this.lstDeducteeHelp.BackColor = System.Drawing.Color.MistyRose;
            this.lstDeducteeHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeducteeHelp.FormattingEnabled = true;
            this.lstDeducteeHelp.HorizontalScrollbar = true;
            this.lstDeducteeHelp.ItemHeight = 16;
            this.lstDeducteeHelp.Location = new System.Drawing.Point(204, 61);
            this.lstDeducteeHelp.Name = "lstDeducteeHelp";
            this.lstDeducteeHelp.Size = new System.Drawing.Size(539, 116);
            this.lstDeducteeHelp.TabIndex = 174;
            this.lstDeducteeHelp.Visible = false;
            this.lstDeducteeHelp.Click += new System.EventHandler(this.lstDeducteeHelp_Click);
            this.lstDeducteeHelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDeducteeHelp_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdAddChallanToStatement);
            this.groupBox3.Controls.Add(this.rdChallanCorrection);
            this.groupBox3.Controls.Add(this.rdPanCorrection);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(116, 387);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(672, 41);
            this.groupBox3.TabIndex = 179;
            this.groupBox3.TabStop = false;
            // 
            // rdAddChallanToStatement
            // 
            this.rdAddChallanToStatement.AutoSize = true;
            this.rdAddChallanToStatement.Checked = true;
            this.rdAddChallanToStatement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAddChallanToStatement.Location = new System.Drawing.Point(182, 14);
            this.rdAddChallanToStatement.Name = "rdAddChallanToStatement";
            this.rdAddChallanToStatement.Size = new System.Drawing.Size(173, 17);
            this.rdAddChallanToStatement.TabIndex = 181;
            this.rdAddChallanToStatement.TabStop = true;
            this.rdAddChallanToStatement.Text = "Add Challan To Statement";
            this.rdAddChallanToStatement.UseVisualStyleBackColor = true;
            // 
            // rdChallanCorrection
            // 
            this.rdChallanCorrection.AutoSize = true;
            this.rdChallanCorrection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdChallanCorrection.Location = new System.Drawing.Point(363, 14);
            this.rdChallanCorrection.Name = "rdChallanCorrection";
            this.rdChallanCorrection.Size = new System.Drawing.Size(129, 17);
            this.rdChallanCorrection.TabIndex = 180;
            this.rdChallanCorrection.Text = "Challan Correction";
            this.rdChallanCorrection.UseVisualStyleBackColor = true;
            // 
            // rdPanCorrection
            // 
            this.rdPanCorrection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdPanCorrection.Location = new System.Drawing.Point(12, 14);
            this.rdPanCorrection.Name = "rdPanCorrection";
            this.rdPanCorrection.Size = new System.Drawing.Size(13, 17);
            this.rdPanCorrection.TabIndex = 179;
            this.rdPanCorrection.Text = "PAN Correction";
            this.rdPanCorrection.UseVisualStyleBackColor = true;
            this.rdPanCorrection.Visible = false;
            // 
            // grpChallanDeductee
            // 
            this.grpChallanDeductee.Controls.Add(this.txtTokenNo);
            this.grpChallanDeductee.Controls.Add(this.label7);
            this.grpChallanDeductee.Controls.Add(this.grpDeductee);
            this.grpChallanDeductee.Controls.Add(this.grpChallan);
            this.grpChallanDeductee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChallanDeductee.Location = new System.Drawing.Point(116, 137);
            this.grpChallanDeductee.Name = "grpChallanDeductee";
            this.grpChallanDeductee.Size = new System.Drawing.Size(672, 239);
            this.grpChallanDeductee.TabIndex = 175;
            this.grpChallanDeductee.TabStop = false;
            // 
            // txtTokenNo
            // 
            this.txtTokenNo.Location = new System.Drawing.Point(153, 19);
            this.txtTokenNo.MaxLength = 15;
            this.txtTokenNo.Name = "txtTokenNo";
            this.txtTokenNo.Size = new System.Drawing.Size(162, 20);
            this.txtTokenNo.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Provisional Receipt No";
            // 
            // grpDeductee
            // 
            this.grpDeductee.Controls.Add(this.txtTDSDeducted3);
            this.grpDeductee.Controls.Add(this.txtTDSDeducted2);
            this.grpDeductee.Controls.Add(this.txtTDSDeducted1);
            this.grpDeductee.Controls.Add(this.txtDeducteePAN3);
            this.grpDeductee.Controls.Add(this.txtDeducteePAN2);
            this.grpDeductee.Controls.Add(this.txtDeducteePAN1);
            this.grpDeductee.Controls.Add(this.label17);
            this.grpDeductee.Controls.Add(this.label16);
            this.grpDeductee.Controls.Add(this.label15);
            this.grpDeductee.Controls.Add(this.label14);
            this.grpDeductee.Controls.Add(this.label13);
            this.grpDeductee.Controls.Add(this.label12);
            this.grpDeductee.Location = new System.Drawing.Point(303, 63);
            this.grpDeductee.Name = "grpDeductee";
            this.grpDeductee.Size = new System.Drawing.Size(334, 158);
            this.grpDeductee.TabIndex = 5;
            this.grpDeductee.TabStop = false;
            this.grpDeductee.Text = "Provide any 3 Deductee record\'s PAN and its Tax Deducted";
            // 
            // txtTDSDeducted3
            // 
            this.txtTDSDeducted3.Location = new System.Drawing.Point(221, 107);
            this.txtTDSDeducted3.MaxLength = 14;
            this.txtTDSDeducted3.Name = "txtTDSDeducted3";
            this.txtTDSDeducted3.Size = new System.Drawing.Size(86, 20);
            this.txtTDSDeducted3.TabIndex = 5;
            this.txtTDSDeducted3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTDSDeducted2
            // 
            this.txtTDSDeducted2.Location = new System.Drawing.Point(221, 83);
            this.txtTDSDeducted2.MaxLength = 14;
            this.txtTDSDeducted2.Name = "txtTDSDeducted2";
            this.txtTDSDeducted2.Size = new System.Drawing.Size(86, 20);
            this.txtTDSDeducted2.TabIndex = 3;
            this.txtTDSDeducted2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTDSDeducted1
            // 
            this.txtTDSDeducted1.Location = new System.Drawing.Point(221, 59);
            this.txtTDSDeducted1.MaxLength = 14;
            this.txtTDSDeducted1.Name = "txtTDSDeducted1";
            this.txtTDSDeducted1.Size = new System.Drawing.Size(86, 20);
            this.txtTDSDeducted1.TabIndex = 1;
            this.txtTDSDeducted1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDeducteePAN3
            // 
            this.txtDeducteePAN3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN3.Location = new System.Drawing.Point(79, 107);
            this.txtDeducteePAN3.MaxLength = 10;
            this.txtDeducteePAN3.Name = "txtDeducteePAN3";
            this.txtDeducteePAN3.Size = new System.Drawing.Size(106, 20);
            this.txtDeducteePAN3.TabIndex = 4;
            // 
            // txtDeducteePAN2
            // 
            this.txtDeducteePAN2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN2.Location = new System.Drawing.Point(79, 83);
            this.txtDeducteePAN2.MaxLength = 10;
            this.txtDeducteePAN2.Name = "txtDeducteePAN2";
            this.txtDeducteePAN2.Size = new System.Drawing.Size(106, 20);
            this.txtDeducteePAN2.TabIndex = 2;
            // 
            // txtDeducteePAN1
            // 
            this.txtDeducteePAN1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeducteePAN1.Location = new System.Drawing.Point(79, 59);
            this.txtDeducteePAN1.MaxLength = 10;
            this.txtDeducteePAN1.Name = "txtDeducteePAN1";
            this.txtDeducteePAN1.Size = new System.Drawing.Size(106, 20);
            this.txtDeducteePAN1.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 111);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "3.";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(18, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "2.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "1.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Sl no.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(216, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "TDS Deducted";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(79, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Deductee PAN";
            // 
            // grpChallan
            // 
            this.grpChallan.Controls.Add(this.btnChangeCDDDDetails);
            this.grpChallan.Controls.Add(this.txtSlNo);
            this.grpChallan.Controls.Add(this.label18);
            this.grpChallan.Controls.Add(this.txtChallanID);
            this.grpChallan.Controls.Add(this.label53);
            this.grpChallan.Controls.Add(this.mskChallanDate);
            this.grpChallan.Controls.Add(this.txtChallanTax);
            this.grpChallan.Controls.Add(this.label11);
            this.grpChallan.Controls.Add(this.label10);
            this.grpChallan.Controls.Add(this.txtBSRCode);
            this.grpChallan.Controls.Add(this.label9);
            this.grpChallan.Controls.Add(this.txtChallanNo);
            this.grpChallan.Controls.Add(this.label8);
            this.grpChallan.Location = new System.Drawing.Point(11, 61);
            this.grpChallan.Name = "grpChallan";
            this.grpChallan.Size = new System.Drawing.Size(283, 161);
            this.grpChallan.TabIndex = 4;
            this.grpChallan.TabStop = false;
            this.grpChallan.Text = "Provide any 1 Challan information of that return";
            // 
            // btnChangeCDDDDetails
            // 
            this.btnChangeCDDDDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChangeCDDDDetails.BackgroundImage")));
            this.btnChangeCDDDDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnChangeCDDDDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeCDDDDetails.Location = new System.Drawing.Point(235, 14);
            this.btnChangeCDDDDetails.Name = "btnChangeCDDDDetails";
            this.btnChangeCDDDDetails.Size = new System.Drawing.Size(42, 37);
            this.btnChangeCDDDDetails.TabIndex = 191;
            this.btnChangeCDDDDetails.UseVisualStyleBackColor = true;
            this.btnChangeCDDDDetails.Click += new System.EventHandler(this.btnChangeCDDDDetails_Click);
            // 
            // txtSlNo
            // 
            this.txtSlNo.Location = new System.Drawing.Point(109, 34);
            this.txtSlNo.MaxLength = 14;
            this.txtSlNo.Name = "txtSlNo";
            this.txtSlNo.Size = new System.Drawing.Size(87, 20);
            this.txtSlNo.TabIndex = 188;
            this.txtSlNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 13);
            this.label18.TabIndex = 189;
            this.label18.Text = "Sl No.";
            // 
            // txtChallanID
            // 
            this.txtChallanID.Location = new System.Drawing.Point(266, 33);
            this.txtChallanID.MaxLength = 7;
            this.txtChallanID.Name = "txtChallanID";
            this.txtChallanID.Size = new System.Drawing.Size(11, 20);
            this.txtChallanID.TabIndex = 185;
            this.txtChallanID.Visible = false;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Blue;
            this.label53.Location = new System.Drawing.Point(200, 112);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(77, 15);
            this.label53.TabIndex = 184;
            this.label53.Text = "DD/MM/YYYY";
            // 
            // mskChallanDate
            // 
            this.mskChallanDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskChallanDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskChallanDate.Location = new System.Drawing.Point(110, 109);
            this.mskChallanDate.Mask = "00/00/0000";
            this.mskChallanDate.Name = "mskChallanDate";
            this.mskChallanDate.Size = new System.Drawing.Size(86, 20);
            this.mskChallanDate.TabIndex = 2;
            this.mskChallanDate.ValidatingType = typeof(System.DateTime);
            // 
            // txtChallanTax
            // 
            this.txtChallanTax.Location = new System.Drawing.Point(109, 135);
            this.txtChallanTax.MaxLength = 14;
            this.txtChallanTax.Name = "txtChallanTax";
            this.txtChallanTax.Size = new System.Drawing.Size(86, 20);
            this.txtChallanTax.TabIndex = 3;
            this.txtChallanTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 139);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Tax Deposit";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Date of Deposit";
            // 
            // txtBSRCode
            // 
            this.txtBSRCode.Location = new System.Drawing.Point(109, 84);
            this.txtBSRCode.MaxLength = 7;
            this.txtBSRCode.Name = "txtBSRCode";
            this.txtBSRCode.Size = new System.Drawing.Size(86, 20);
            this.txtBSRCode.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "BSR Code";
            // 
            // txtChallanNo
            // 
            this.txtChallanNo.Location = new System.Drawing.Point(109, 58);
            this.txtChallanNo.MaxLength = 7;
            this.txtChallanNo.Name = "txtChallanNo";
            this.txtChallanNo.Size = new System.Drawing.Size(86, 20);
            this.txtChallanNo.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Challan No.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbQtr);
            this.groupBox2.Controls.Add(this.cmbFormNo);
            this.groupBox2.Controls.Add(this.cmbFAYear);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(116, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 49);
            this.groupBox2.TabIndex = 173;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Your Return";
            // 
            // cmbQtr
            // 
            this.cmbQtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtr.FormattingEnabled = true;
            this.cmbQtr.Location = new System.Drawing.Point(491, 21);
            this.cmbQtr.Name = "cmbQtr";
            this.cmbQtr.Size = new System.Drawing.Size(92, 21);
            this.cmbQtr.TabIndex = 2;
            this.cmbQtr.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(326, 21);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(96, 21);
            this.cmbFormNo.TabIndex = 1;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // cmbFAYear
            // 
            this.cmbFAYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFAYear.FormattingEnabled = true;
            this.cmbFAYear.Location = new System.Drawing.Point(91, 21);
            this.cmbFAYear.Name = "cmbFAYear";
            this.cmbFAYear.Size = new System.Drawing.Size(162, 21);
            this.cmbFAYear.TabIndex = 0;
            this.cmbFAYear.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quarter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Form No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "FA Year";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRememberMe);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTANNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(116, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Traces User Details";
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.Location = new System.Drawing.Point(533, 44);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(95, 17);
            this.chkRememberMe.TabIndex = 7;
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            this.chkRememberMe.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(310, 17);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(126, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(298, 17);
            this.txtUserId.MaxLength = 50;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(17, 20);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Id";
            this.label2.Visible = false;
            // 
            // txtTANNo
            // 
            this.txtTANNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTANNo.Location = new System.Drawing.Point(89, 17);
            this.txtTANNo.MaxLength = 10;
            this.txtTANNo.Name = "txtTANNo";
            this.txtTANNo.Size = new System.Drawing.Size(126, 20);
            this.txtTANNo.TabIndex = 0;
            this.txtTANNo.TextChanged += new System.EventHandler(this.txtTAN_TextChanged);
            this.txtTANNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTAN_KeyDown);
            this.txtTANNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTANNo_KeyPress);
            this.txtTANNo.Leave += new System.EventHandler(this.txtTAN_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAN";
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(958, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(40, 32);
            this.pctUserManual.TabIndex = 213;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // bgWorkerLoadCaptcha
            // 
            this.bgWorkerLoadCaptcha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLoadCaptcha_DoWork);
            // 
            // TrnChallanPANCorrection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1053, 577);
            this.Name = "TrnChallanPANCorrection";
            this.Activated += new System.EventHandler(this.TrnChallanPANCorrection_Activated);
            this.Load += new System.EventHandler(this.TrnChallanPANCorrection_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpControls.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpChallanDeductee.ResumeLayout(false);
            this.grpChallanDeductee.PerformLayout();
            this.grpDeductee.ResumeLayout(false);
            this.grpDeductee.PerformLayout();
            this.grpChallan.ResumeLayout(false);
            this.grpChallan.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTANNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbQtr;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.ComboBox cmbFAYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstDeducteeHelp;
        private System.Windows.Forms.GroupBox grpChallanDeductee;
        private System.Windows.Forms.GroupBox grpDeductee;
        private System.Windows.Forms.TextBox txtTDSDeducted3;
        private System.Windows.Forms.TextBox txtTDSDeducted2;
        private System.Windows.Forms.TextBox txtTDSDeducted1;
        private System.Windows.Forms.TextBox txtDeducteePAN3;
        private System.Windows.Forms.TextBox txtDeducteePAN2;
        private System.Windows.Forms.TextBox txtDeducteePAN1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpChallan;
        private System.Windows.Forms.TextBox txtSlNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtChallanID;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.MaskedTextBox mskChallanDate;
        private System.Windows.Forms.TextBox txtChallanTax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBSRCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtChallanNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTokenNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdAddChallanToStatement;
        private System.Windows.Forms.RadioButton rdChallanCorrection;
        private System.Windows.Forms.RadioButton rdPanCorrection;
        private System.Windows.Forms.Button btnChangeCDDDDetails;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.ComponentModel.BackgroundWorker bgWorkerLoadCaptcha;
    }
}
