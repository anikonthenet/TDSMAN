namespace TDSMAN.FormSys
{
    partial class SysShowConsumption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysShowConsumption));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlRegular = new System.Windows.Forms.Panel();
            this.lblRegularCapacity = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblForm27EQCount = new System.Windows.Forms.Label();
            this.lblForm27QCount = new System.Windows.Forms.Label();
            this.lblForm26QCount = new System.Windows.Forms.Label();
            this.lblForm24QCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblForm27EQ = new System.Windows.Forms.Label();
            this.lblForm27Q = new System.Windows.Forms.Label();
            this.lblForm26Q = new System.Windows.Forms.Label();
            this.lblForm24Q = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.pnlLine2 = new System.Windows.Forms.Panel();
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpControlSummary = new System.Windows.Forms.GroupBox();
            this.lnkSearchByTAN = new System.Windows.Forms.LinkLabel();
            this.cmbFormNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpRegularReturn = new System.Windows.Forms.GroupBox();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.grpSelectReturn = new System.Windows.Forms.GroupBox();
            this.rbnCorrection = new System.Windows.Forms.RadioButton();
            this.rbnRegular = new System.Windows.Forms.RadioButton();
            this.pnlCorrection = new System.Windows.Forms.Panel();
            this.lbl194PCount = new System.Windows.Forms.Label();
            this.lbl194PImportCount = new System.Windows.Forms.Label();
            this.lblSDCount = new System.Windows.Forms.Label();
            this.lblSDImportCount = new System.Windows.Forms.Label();
            this.lblDDCount = new System.Windows.Forms.Label();
            this.lblDDImportCount = new System.Windows.Forms.Label();
            this.dgcViewBatch = new DGVControl.DGVControl();
            this.lblCorrectionCapacity = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bgCalcTotalConsumption = new System.ComponentModel.BackgroundWorker();
            this.lblTotalConsumed = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.pnlRegular.SuspendLayout();
            this.grpControlSummary.SuspendLayout();
            this.grpRegularReturn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.grpSelectReturn.SuspendLayout();
            this.pnlCorrection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewBatch)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(838, 10);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(175, 24);
            this.lblSearchMode.TabIndex = 51;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(183, 10);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(656, 24);
            this.pnlTitle.TabIndex = 49;
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
            this.lblTitle.Text = "Show Usage - Deductee Records";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(9, 10);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(175, 24);
            this.lblMode.TabIndex = 50;
            this.lblMode.Text = "View Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlRegular
            // 
            this.pnlRegular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRegular.Controls.Add(this.lblRegularCapacity);
            this.pnlRegular.Controls.Add(this.lblTotalCount);
            this.pnlRegular.Controls.Add(this.lblForm27EQCount);
            this.pnlRegular.Controls.Add(this.lblForm27QCount);
            this.pnlRegular.Controls.Add(this.lblForm26QCount);
            this.pnlRegular.Controls.Add(this.lblForm24QCount);
            this.pnlRegular.Controls.Add(this.lblTotal);
            this.pnlRegular.Controls.Add(this.lblForm27EQ);
            this.pnlRegular.Controls.Add(this.lblForm27Q);
            this.pnlRegular.Controls.Add(this.lblForm26Q);
            this.pnlRegular.Controls.Add(this.lblForm24Q);
            this.pnlRegular.Controls.Add(this.lblTotalRecords);
            this.pnlRegular.Controls.Add(this.pnlLine2);
            this.pnlRegular.Controls.Add(this.pnlLine1);
            this.pnlRegular.Controls.Add(this.panel2);
            this.pnlRegular.Controls.Add(this.grpControlSummary);
            this.pnlRegular.Controls.Add(this.grpRegularReturn);
            this.pnlRegular.Location = new System.Drawing.Point(184, 107);
            this.pnlRegular.Name = "pnlRegular";
            this.pnlRegular.Size = new System.Drawing.Size(654, 391);
            this.pnlRegular.TabIndex = 52;
            // 
            // lblRegularCapacity
            // 
            this.lblRegularCapacity.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegularCapacity.ForeColor = System.Drawing.Color.Green;
            this.lblRegularCapacity.Location = new System.Drawing.Point(3, 358);
            this.lblRegularCapacity.Name = "lblRegularCapacity";
            this.lblRegularCapacity.Size = new System.Drawing.Size(646, 27);
            this.lblRegularCapacity.TabIndex = 232;
            this.lblRegularCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCount.Location = new System.Drawing.Point(242, 313);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(137, 27);
            this.lblTotalCount.TabIndex = 231;
            this.lblTotalCount.Text = "0";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblForm27EQCount
            // 
            this.lblForm27EQCount.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm27EQCount.ForeColor = System.Drawing.Color.Blue;
            this.lblForm27EQCount.Location = new System.Drawing.Point(242, 286);
            this.lblForm27EQCount.Name = "lblForm27EQCount";
            this.lblForm27EQCount.Size = new System.Drawing.Size(137, 27);
            this.lblForm27EQCount.TabIndex = 230;
            this.lblForm27EQCount.Text = "0";
            this.lblForm27EQCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblForm27EQCount.Visible = false;
            // 
            // lblForm27QCount
            // 
            this.lblForm27QCount.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm27QCount.ForeColor = System.Drawing.Color.Blue;
            this.lblForm27QCount.Location = new System.Drawing.Point(242, 259);
            this.lblForm27QCount.Name = "lblForm27QCount";
            this.lblForm27QCount.Size = new System.Drawing.Size(137, 27);
            this.lblForm27QCount.TabIndex = 229;
            this.lblForm27QCount.Text = "0";
            this.lblForm27QCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblForm27QCount.Visible = false;
            // 
            // lblForm26QCount
            // 
            this.lblForm26QCount.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm26QCount.ForeColor = System.Drawing.Color.Blue;
            this.lblForm26QCount.Location = new System.Drawing.Point(242, 232);
            this.lblForm26QCount.Name = "lblForm26QCount";
            this.lblForm26QCount.Size = new System.Drawing.Size(137, 27);
            this.lblForm26QCount.TabIndex = 228;
            this.lblForm26QCount.Text = "0";
            this.lblForm26QCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblForm26QCount.Visible = false;
            // 
            // lblForm24QCount
            // 
            this.lblForm24QCount.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm24QCount.ForeColor = System.Drawing.Color.Blue;
            this.lblForm24QCount.Location = new System.Drawing.Point(242, 205);
            this.lblForm24QCount.Name = "lblForm24QCount";
            this.lblForm24QCount.Size = new System.Drawing.Size(137, 27);
            this.lblForm24QCount.TabIndex = 227;
            this.lblForm24QCount.Text = "0";
            this.lblForm24QCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblForm24QCount.Visible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(141, 312);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(96, 27);
            this.lblTotal.TabIndex = 224;
            this.lblTotal.Text = "Total            :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblForm27EQ
            // 
            this.lblForm27EQ.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm27EQ.ForeColor = System.Drawing.Color.Blue;
            this.lblForm27EQ.Location = new System.Drawing.Point(141, 285);
            this.lblForm27EQ.Name = "lblForm27EQ";
            this.lblForm27EQ.Size = new System.Drawing.Size(96, 27);
            this.lblForm27EQ.TabIndex = 223;
            this.lblForm27EQ.Text = "Form 27EQ :";
            this.lblForm27EQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblForm27EQ.Visible = false;
            // 
            // lblForm27Q
            // 
            this.lblForm27Q.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm27Q.ForeColor = System.Drawing.Color.Blue;
            this.lblForm27Q.Location = new System.Drawing.Point(141, 258);
            this.lblForm27Q.Name = "lblForm27Q";
            this.lblForm27Q.Size = new System.Drawing.Size(96, 27);
            this.lblForm27Q.TabIndex = 222;
            this.lblForm27Q.Text = "Form 27Q   :";
            this.lblForm27Q.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblForm27Q.Visible = false;
            // 
            // lblForm26Q
            // 
            this.lblForm26Q.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm26Q.ForeColor = System.Drawing.Color.Blue;
            this.lblForm26Q.Location = new System.Drawing.Point(141, 231);
            this.lblForm26Q.Name = "lblForm26Q";
            this.lblForm26Q.Size = new System.Drawing.Size(96, 27);
            this.lblForm26Q.TabIndex = 221;
            this.lblForm26Q.Text = "Form 26Q   :";
            this.lblForm26Q.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblForm26Q.Visible = false;
            // 
            // lblForm24Q
            // 
            this.lblForm24Q.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm24Q.ForeColor = System.Drawing.Color.Blue;
            this.lblForm24Q.Location = new System.Drawing.Point(141, 204);
            this.lblForm24Q.Name = "lblForm24Q";
            this.lblForm24Q.Size = new System.Drawing.Size(96, 27);
            this.lblForm24Q.TabIndex = 220;
            this.lblForm24Q.Text = "Form 24Q   :";
            this.lblForm24Q.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblForm24Q.Visible = false;
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalRecords.Location = new System.Drawing.Point(95, 177);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(463, 27);
            this.lblTotalRecords.TabIndex = 218;
            this.lblTotalRecords.Text = "11";
            this.lblTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLine2
            // 
            this.pnlLine2.BackColor = System.Drawing.Color.Black;
            this.pnlLine2.Location = new System.Drawing.Point(6, 352);
            this.pnlLine2.Name = "pnlLine2";
            this.pnlLine2.Size = new System.Drawing.Size(639, 3);
            this.pnlLine2.TabIndex = 216;
            // 
            // pnlLine1
            // 
            this.pnlLine1.BackColor = System.Drawing.Color.Black;
            this.pnlLine1.Location = new System.Drawing.Point(6, 173);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(639, 3);
            this.pnlLine1.TabIndex = 215;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(6, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(639, 3);
            this.panel2.TabIndex = 214;
            // 
            // grpControlSummary
            // 
            this.grpControlSummary.Controls.Add(this.lnkSearchByTAN);
            this.grpControlSummary.Controls.Add(this.cmbFormNo);
            this.grpControlSummary.Controls.Add(this.label4);
            this.grpControlSummary.Controls.Add(this.cmbQuarter);
            this.grpControlSummary.Controls.Add(this.label6);
            this.grpControlSummary.Controls.Add(this.cmbCompany);
            this.grpControlSummary.Controls.Add(this.label8);
            this.grpControlSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControlSummary.Location = new System.Drawing.Point(5, 82);
            this.grpControlSummary.Name = "grpControlSummary";
            this.grpControlSummary.Size = new System.Drawing.Size(641, 82);
            this.grpControlSummary.TabIndex = 212;
            this.grpControlSummary.TabStop = false;
            // 
            // lnkSearchByTAN
            // 
            this.lnkSearchByTAN.AutoSize = true;
            this.lnkSearchByTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSearchByTAN.Location = new System.Drawing.Point(529, 52);
            this.lnkSearchByTAN.Name = "lnkSearchByTAN";
            this.lnkSearchByTAN.Size = new System.Drawing.Size(93, 13);
            this.lnkSearchByTAN.TabIndex = 217;
            this.lnkSearchByTAN.TabStop = true;
            this.lnkSearchByTAN.Text = "Search by TAN";
            this.lnkSearchByTAN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchByTAN_LinkClicked);
            // 
            // cmbFormNo
            // 
            this.cmbFormNo.BackColor = System.Drawing.Color.White;
            this.cmbFormNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormNo.ForeColor = System.Drawing.Color.Black;
            this.cmbFormNo.FormattingEnabled = true;
            this.cmbFormNo.Location = new System.Drawing.Point(120, 18);
            this.cmbFormNo.Name = "cmbFormNo";
            this.cmbFormNo.Size = new System.Drawing.Size(166, 21);
            this.cmbFormNo.TabIndex = 215;
            this.cmbFormNo.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(22, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 216;
            this.label4.Text = "Select Form No";
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.BackColor = System.Drawing.Color.White;
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQuarter.ForeColor = System.Drawing.Color.Black;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Location = new System.Drawing.Point(514, 19);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(108, 21);
            this.cmbQuarter.TabIndex = 211;
            this.cmbQuarter.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(421, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 214;
            this.label6.Text = "Select Quarter";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(120, 48);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(403, 21);
            this.cmbCompany.TabIndex = 212;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(18, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 213;
            this.label8.Text = "Select Company";
            // 
            // grpRegularReturn
            // 
            this.grpRegularReturn.Controls.Add(this.cmbFinancialYear);
            this.grpRegularReturn.Controls.Add(this.label7);
            this.grpRegularReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRegularReturn.Location = new System.Drawing.Point(4, 7);
            this.grpRegularReturn.Name = "grpRegularReturn";
            this.grpRegularReturn.Size = new System.Drawing.Size(643, 54);
            this.grpRegularReturn.TabIndex = 211;
            this.grpRegularReturn.TabStop = false;
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(318, 20);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 203;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbMain_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(189, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 207;
            this.label7.Text = "Select Tax Year";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pctUserManual);
            this.groupBox1.Controls.Add(this.BtnExit);
            this.groupBox1.Location = new System.Drawing.Point(183, 612);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 48);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(608, 11);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 232;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnExit.Location = new System.Drawing.Point(286, 15);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.TabIndex = 10;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.btnXit_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(5, 39);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1019, 3);
            this.pnlHeader.TabIndex = 54;
            // 
            // grpSelectReturn
            // 
            this.grpSelectReturn.Controls.Add(this.rbnCorrection);
            this.grpSelectReturn.Controls.Add(this.rbnRegular);
            this.grpSelectReturn.Location = new System.Drawing.Point(351, 49);
            this.grpSelectReturn.Name = "grpSelectReturn";
            this.grpSelectReturn.Size = new System.Drawing.Size(327, 44);
            this.grpSelectReturn.TabIndex = 55;
            this.grpSelectReturn.TabStop = false;
            // 
            // rbnCorrection
            // 
            this.rbnCorrection.AutoSize = true;
            this.rbnCorrection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnCorrection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnCorrection.Location = new System.Drawing.Point(189, 14);
            this.rbnCorrection.Name = "rbnCorrection";
            this.rbnCorrection.Size = new System.Drawing.Size(83, 17);
            this.rbnCorrection.TabIndex = 1;
            this.rbnCorrection.Text = "Correction";
            this.rbnCorrection.UseVisualStyleBackColor = true;
            this.rbnCorrection.CheckedChanged += new System.EventHandler(this.RbnRegular_CheckedChanged);
            // 
            // rbnRegular
            // 
            this.rbnRegular.AutoSize = true;
            this.rbnRegular.Checked = true;
            this.rbnRegular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnRegular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnRegular.Location = new System.Drawing.Point(55, 14);
            this.rbnRegular.Name = "rbnRegular";
            this.rbnRegular.Size = new System.Drawing.Size(69, 17);
            this.rbnRegular.TabIndex = 0;
            this.rbnRegular.TabStop = true;
            this.rbnRegular.Text = "Regular";
            this.rbnRegular.UseVisualStyleBackColor = true;
            this.rbnRegular.CheckedChanged += new System.EventHandler(this.RbnRegular_CheckedChanged);
            // 
            // pnlCorrection
            // 
            this.pnlCorrection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCorrection.Controls.Add(this.lbl194PCount);
            this.pnlCorrection.Controls.Add(this.lbl194PImportCount);
            this.pnlCorrection.Controls.Add(this.lblSDCount);
            this.pnlCorrection.Controls.Add(this.lblSDImportCount);
            this.pnlCorrection.Controls.Add(this.lblDDCount);
            this.pnlCorrection.Controls.Add(this.lblDDImportCount);
            this.pnlCorrection.Controls.Add(this.dgcViewBatch);
            this.pnlCorrection.Controls.Add(this.lblCorrectionCapacity);
            this.pnlCorrection.Controls.Add(this.panel3);
            this.pnlCorrection.Controls.Add(this.panel4);
            this.pnlCorrection.Location = new System.Drawing.Point(185, 107);
            this.pnlCorrection.Name = "pnlCorrection";
            this.pnlCorrection.Size = new System.Drawing.Size(654, 391);
            this.pnlCorrection.TabIndex = 56;
            // 
            // lbl194PCount
            // 
            this.lbl194PCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl194PCount.ForeColor = System.Drawing.Color.Blue;
            this.lbl194PCount.Location = new System.Drawing.Point(282, 279);
            this.lbl194PCount.Name = "lbl194PCount";
            this.lbl194PCount.Size = new System.Drawing.Size(261, 27);
            this.lbl194PCount.TabIndex = 239;
            this.lbl194PCount.Text = "Add/Modify/Delete 194P Count   :";
            this.lbl194PCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl194PCount.Visible = false;
            // 
            // lbl194PImportCount
            // 
            this.lbl194PImportCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl194PImportCount.ForeColor = System.Drawing.Color.Blue;
            this.lbl194PImportCount.Location = new System.Drawing.Point(9, 279);
            this.lbl194PImportCount.Name = "lbl194PImportCount";
            this.lbl194PImportCount.Size = new System.Drawing.Size(198, 27);
            this.lbl194PImportCount.TabIndex = 238;
            this.lbl194PImportCount.Text = "Import 194P Count   :";
            this.lbl194PImportCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl194PImportCount.Visible = false;
            // 
            // lblSDCount
            // 
            this.lblSDCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDCount.ForeColor = System.Drawing.Color.Blue;
            this.lblSDCount.Location = new System.Drawing.Point(282, 243);
            this.lblSDCount.Name = "lblSDCount";
            this.lblSDCount.Size = new System.Drawing.Size(261, 27);
            this.lblSDCount.TabIndex = 237;
            this.lblSDCount.Text = "Add/Modify/Delete Salary Details Count   :";
            this.lblSDCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSDCount.Visible = false;
            // 
            // lblSDImportCount
            // 
            this.lblSDImportCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDImportCount.ForeColor = System.Drawing.Color.Blue;
            this.lblSDImportCount.Location = new System.Drawing.Point(9, 243);
            this.lblSDImportCount.Name = "lblSDImportCount";
            this.lblSDImportCount.Size = new System.Drawing.Size(198, 27);
            this.lblSDImportCount.TabIndex = 236;
            this.lblSDImportCount.Text = "Import Salary Details Count   :";
            this.lblSDImportCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSDImportCount.Visible = false;
            // 
            // lblDDCount
            // 
            this.lblDDCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDDCount.ForeColor = System.Drawing.Color.Blue;
            this.lblDDCount.Location = new System.Drawing.Point(282, 207);
            this.lblDDCount.Name = "lblDDCount";
            this.lblDDCount.Size = new System.Drawing.Size(261, 27);
            this.lblDDCount.TabIndex = 235;
            this.lblDDCount.Text = "Add/Modify/Delete Deductee Details Count   :";
            this.lblDDCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDDImportCount
            // 
            this.lblDDImportCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDDImportCount.ForeColor = System.Drawing.Color.Blue;
            this.lblDDImportCount.Location = new System.Drawing.Point(9, 207);
            this.lblDDImportCount.Name = "lblDDImportCount";
            this.lblDDImportCount.Size = new System.Drawing.Size(198, 27);
            this.lblDDImportCount.TabIndex = 234;
            this.lblDDImportCount.Text = "Import Deductee Details Count   :";
            this.lblDDImportCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgcViewBatch
            // 
            this.dgcViewBatch.AllowUserToAddRows = false;
            this.dgcViewBatch.AllowUserToDeleteRows = false;
            this.dgcViewBatch.AllowUserToOrderColumns = true;
            this.dgcViewBatch.AllowUserToResizeRows = false;
            this.dgcViewBatch.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgcViewBatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgcViewBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgcViewBatch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgcViewBatch.GridColor = System.Drawing.SystemColors.Control;
            this.dgcViewBatch.Location = new System.Drawing.Point(4, 7);
            this.dgcViewBatch.MultiSelect = false;
            this.dgcViewBatch.Name = "dgcViewBatch";
            this.dgcViewBatch.RowHeadersWidth = 20;
            this.dgcViewBatch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgcViewBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgcViewBatch.Size = new System.Drawing.Size(644, 181);
            this.dgcViewBatch.TabIndex = 233;
            this.dgcViewBatch.TabStop = false;
            this.dgcViewBatch.CurrentCellChanged += new System.EventHandler(this.dgcViewBatch_CurrentCellChanged);
            this.dgcViewBatch.Click += new System.EventHandler(this.dgcViewBatch_Click);
            this.dgcViewBatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgcViewBatch_KeyDown);
            // 
            // lblCorrectionCapacity
            // 
            this.lblCorrectionCapacity.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrectionCapacity.ForeColor = System.Drawing.Color.Green;
            this.lblCorrectionCapacity.Location = new System.Drawing.Point(3, 358);
            this.lblCorrectionCapacity.Name = "lblCorrectionCapacity";
            this.lblCorrectionCapacity.Size = new System.Drawing.Size(646, 27);
            this.lblCorrectionCapacity.TabIndex = 232;
            this.lblCorrectionCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(6, 352);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(639, 3);
            this.panel3.TabIndex = 216;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(6, 193);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(639, 3);
            this.panel4.TabIndex = 215;
            // 
            // bgCalcTotalConsumption
            // 
            this.bgCalcTotalConsumption.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgCalcTotalConsumption_DoWork);
            this.bgCalcTotalConsumption.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgCalcTotalConsumption_RunWorkerCompleted);
            // 
            // lblTotalConsumed
            // 
            this.lblTotalConsumed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalConsumed.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalConsumed.Location = new System.Drawing.Point(194, 501);
            this.lblTotalConsumed.Name = "lblTotalConsumed";
            this.lblTotalConsumed.Size = new System.Drawing.Size(639, 27);
            this.lblTotalConsumed.TabIndex = 241;
            this.lblTotalConsumed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalConsumed.Visible = false;
            // 
            // SysShowConsumption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.lblTotalConsumed);
            this.Controls.Add(this.pnlCorrection);
            this.Controls.Add(this.grpSelectReturn);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlRegular);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Name = "SysShowConsumption";
            this.ShowIcon = false;
            this.Activated += new System.EventHandler(this.SysShowUsage_Activated);
            this.Load += new System.EventHandler(this.SysShowUsage_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlRegular.ResumeLayout(false);
            this.grpControlSummary.ResumeLayout(false);
            this.grpControlSummary.PerformLayout();
            this.grpRegularReturn.ResumeLayout(false);
            this.grpRegularReturn.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.grpSelectReturn.ResumeLayout(false);
            this.grpSelectReturn.PerformLayout();
            this.pnlCorrection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewBatch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel pnlRegular;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button BtnExit;
        public System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Panel pnlLine2;
        private System.Windows.Forms.Panel pnlLine1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpControlSummary;
        private System.Windows.Forms.GroupBox grpRegularReturn;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFormNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblForm24Q;
        private System.Windows.Forms.Label lblForm26Q;
        private System.Windows.Forms.Label lblForm27EQ;
        private System.Windows.Forms.Label lblForm27Q;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblForm24QCount;
        private System.Windows.Forms.Label lblForm26QCount;
        private System.Windows.Forms.Label lblForm27EQCount;
        private System.Windows.Forms.Label lblForm27QCount;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.LinkLabel lnkSearchByTAN;
        private System.Windows.Forms.PictureBox pctUserManual;
        private System.Windows.Forms.GroupBox grpSelectReturn;
        private System.Windows.Forms.RadioButton rbnCorrection;
        private System.Windows.Forms.RadioButton rbnRegular;
        private System.Windows.Forms.Label lblRegularCapacity;
        private System.Windows.Forms.Panel pnlCorrection;
        private System.Windows.Forms.Label lblCorrectionCapacity;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private DGVControl.DGVControl dgcViewBatch;
        private System.Windows.Forms.Label lblDDImportCount;
        private System.Windows.Forms.Label lblDDCount;
        private System.Windows.Forms.Label lblSDCount;
        private System.Windows.Forms.Label lblSDImportCount;
        private System.Windows.Forms.Label lbl194PCount;
        private System.Windows.Forms.Label lbl194PImportCount;
        private System.ComponentModel.BackgroundWorker bgCalcTotalConsumption;
        private System.Windows.Forms.Label lblTotalConsumed;
    }
}