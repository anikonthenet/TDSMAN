namespace TDSMAN.FormTrn
{
    partial class TrnKnowYourTAN
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
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpSearchBox = new System.Windows.Forms.GroupBox();
            this.cmbCategoryDeductor = new System.Windows.Forms.ComboBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblNameTAN = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rbnSearchByTAN = new System.Windows.Forms.RadioButton();
            this.rbnSearchByName = new System.Windows.Forms.RadioButton();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.txtName_TAN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnBacktoSearch = new System.Windows.Forms.Button();
            this.lblEmailID = new System.Windows.Forms.Label();
            this.lblBuildingName = new System.Windows.Forms.Label();
            this.lblAODescription = new System.Windows.Forms.Label();
            this.lblAONumber = new System.Windows.Forms.Label();
            this.lblRangeCode = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrintPan = new System.Windows.Forms.Button();
            this.lblEmailID1 = new System.Windows.Forms.Label();
            this.lblStatusofTAN = new System.Windows.Forms.Label();
            this.lblAOType = new System.Windows.Forms.Label();
            this.lblAreaCode = new System.Windows.Forms.Label();
            this.lblEmailID2 = new System.Windows.Forms.Label();
            this.lblCategoryDeductor = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPAN = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTAN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpOTPDetails = new System.Windows.Forms.GroupBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblOTPSentMsg = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.txtMobileOTP = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpSearchBox.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.grpOTPDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 666);
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
            this.BtnSave.Location = new System.Drawing.Point(102, 12);
            this.BtnSave.Size = new System.Drawing.Size(19, 23);
            this.BtnSave.Text = "&Submit";
            this.BtnSave.Visible = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
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
            this.BtnExit.Location = new System.Drawing.Point(460, 13);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
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
            this.grpButton.Location = new System.Drawing.Point(9, 599);
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpOTPDetails);
            this.pnlControls.Controls.Add(this.pBar);
            this.pnlControls.Controls.Add(this.grpDetails);
            this.pnlControls.Controls.Add(this.grpSearchBox);
            this.pnlControls.Size = new System.Drawing.Size(1036, 609);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // grpSearchBox
            // 
            this.grpSearchBox.Controls.Add(this.cmbCategoryDeductor);
            this.grpSearchBox.Controls.Add(this.cmbState);
            this.grpSearchBox.Controls.Add(this.label15);
            this.grpSearchBox.Controls.Add(this.lblNameTAN);
            this.grpSearchBox.Controls.Add(this.label13);
            this.grpSearchBox.Controls.Add(this.rbnSearchByTAN);
            this.grpSearchBox.Controls.Add(this.rbnSearchByName);
            this.grpSearchBox.Controls.Add(this.btnReset);
            this.grpSearchBox.Controls.Add(this.btnSubmit);
            this.grpSearchBox.Controls.Add(this.txtMobileNo);
            this.grpSearchBox.Controls.Add(this.txtName_TAN);
            this.grpSearchBox.Controls.Add(this.label3);
            this.grpSearchBox.Location = new System.Drawing.Point(117, 77);
            this.grpSearchBox.Name = "grpSearchBox";
            this.grpSearchBox.Size = new System.Drawing.Size(741, 259);
            this.grpSearchBox.TabIndex = 0;
            this.grpSearchBox.TabStop = false;
            this.grpSearchBox.Text = "TAN Search";
            // 
            // cmbCategoryDeductor
            // 
            this.cmbCategoryDeductor.BackColor = System.Drawing.Color.White;
            this.cmbCategoryDeductor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryDeductor.ForeColor = System.Drawing.Color.Black;
            this.cmbCategoryDeductor.FormattingEnabled = true;
            this.cmbCategoryDeductor.Location = new System.Drawing.Point(192, 61);
            this.cmbCategoryDeductor.Name = "cmbCategoryDeductor";
            this.cmbCategoryDeductor.Size = new System.Drawing.Size(375, 21);
            this.cmbCategoryDeductor.TabIndex = 219;
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.White;
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(192, 88);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(277, 21);
            this.cmbState.TabIndex = 218;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(57, 146);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 217;
            this.label15.Text = "Mobile";
            // 
            // lblNameTAN
            // 
            this.lblNameTAN.AutoSize = true;
            this.lblNameTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTAN.Location = new System.Drawing.Point(57, 120);
            this.lblNameTAN.Name = "lblNameTAN";
            this.lblNameTAN.Size = new System.Drawing.Size(39, 13);
            this.lblNameTAN.TabIndex = 216;
            this.lblNameTAN.Text = "Name";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(57, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 214;
            this.label13.Text = "State";
            // 
            // rbnSearchByTAN
            // 
            this.rbnSearchByTAN.AutoSize = true;
            this.rbnSearchByTAN.Location = new System.Drawing.Point(303, 20);
            this.rbnSearchByTAN.Name = "rbnSearchByTAN";
            this.rbnSearchByTAN.Size = new System.Drawing.Size(99, 17);
            this.rbnSearchByTAN.TabIndex = 212;
            this.rbnSearchByTAN.TabStop = true;
            this.rbnSearchByTAN.Text = "Search By TAN";
            this.rbnSearchByTAN.UseVisualStyleBackColor = true;
            this.rbnSearchByTAN.CheckedChanged += new System.EventHandler(this.rbnSearchByName_CheckedChanged);
            // 
            // rbnSearchByName
            // 
            this.rbnSearchByName.AutoSize = true;
            this.rbnSearchByName.Location = new System.Drawing.Point(188, 20);
            this.rbnSearchByName.Name = "rbnSearchByName";
            this.rbnSearchByName.Size = new System.Drawing.Size(105, 17);
            this.rbnSearchByName.TabIndex = 211;
            this.rbnSearchByName.TabStop = true;
            this.rbnSearchByName.Text = "Search By Name";
            this.rbnSearchByName.UseVisualStyleBackColor = true;
            this.rbnSearchByName.CheckedChanged += new System.EventHandler(this.rbnSearchByName_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Lavender;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(273, 203);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Lavender;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(192, 203);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(192, 139);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(277, 20);
            this.txtMobileNo.TabIndex = 207;
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtName_TAN
            // 
            this.txtName_TAN.BackColor = System.Drawing.Color.White;
            this.txtName_TAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName_TAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName_TAN.Location = new System.Drawing.Point(192, 112);
            this.txtName_TAN.MaxLength = 10;
            this.txtName_TAN.Name = "txtName_TAN";
            this.txtName_TAN.Size = new System.Drawing.Size(277, 21);
            this.txtName_TAN.TabIndex = 205;
            this.txtName_TAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 206;
            this.label3.Text = "Category of Deductor";
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.btnBacktoSearch);
            this.grpDetails.Controls.Add(this.lblEmailID);
            this.grpDetails.Controls.Add(this.lblBuildingName);
            this.grpDetails.Controls.Add(this.lblAODescription);
            this.grpDetails.Controls.Add(this.lblAONumber);
            this.grpDetails.Controls.Add(this.lblRangeCode);
            this.grpDetails.Controls.Add(this.label20);
            this.grpDetails.Controls.Add(this.label19);
            this.grpDetails.Controls.Add(this.label18);
            this.grpDetails.Controls.Add(this.label17);
            this.grpDetails.Controls.Add(this.label16);
            this.grpDetails.Controls.Add(this.label1);
            this.grpDetails.Controls.Add(this.btnPrintPan);
            this.grpDetails.Controls.Add(this.lblEmailID1);
            this.grpDetails.Controls.Add(this.lblStatusofTAN);
            this.grpDetails.Controls.Add(this.lblAOType);
            this.grpDetails.Controls.Add(this.lblAreaCode);
            this.grpDetails.Controls.Add(this.lblEmailID2);
            this.grpDetails.Controls.Add(this.lblCategoryDeductor);
            this.grpDetails.Controls.Add(this.lblName);
            this.grpDetails.Controls.Add(this.lblAddress);
            this.grpDetails.Controls.Add(this.lblPAN);
            this.grpDetails.Controls.Add(this.label12);
            this.grpDetails.Controls.Add(this.label11);
            this.grpDetails.Controls.Add(this.label10);
            this.grpDetails.Controls.Add(this.label9);
            this.grpDetails.Controls.Add(this.label4);
            this.grpDetails.Controls.Add(this.label8);
            this.grpDetails.Controls.Add(this.label7);
            this.grpDetails.Controls.Add(this.label6);
            this.grpDetails.Controls.Add(this.label5);
            this.grpDetails.Controls.Add(this.lblTAN);
            this.grpDetails.Controls.Add(this.label2);
            this.grpDetails.Location = new System.Drawing.Point(55, 17);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(850, 480);
            this.grpDetails.TabIndex = 226;
            this.grpDetails.TabStop = false;
            this.grpDetails.Visible = false;
            // 
            // btnBacktoSearch
            // 
            this.btnBacktoSearch.BackColor = System.Drawing.Color.Lavender;
            this.btnBacktoSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBacktoSearch.Location = new System.Drawing.Point(365, 442);
            this.btnBacktoSearch.Name = "btnBacktoSearch";
            this.btnBacktoSearch.Size = new System.Drawing.Size(75, 23);
            this.btnBacktoSearch.TabIndex = 257;
            this.btnBacktoSearch.Text = "&Back";
            this.btnBacktoSearch.UseVisualStyleBackColor = false;
            this.btnBacktoSearch.Click += new System.EventHandler(this.btnBacktoSearch_Click);
            // 
            // lblEmailID
            // 
            this.lblEmailID.AutoSize = true;
            this.lblEmailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailID.Location = new System.Drawing.Point(236, 405);
            this.lblEmailID.Name = "lblEmailID";
            this.lblEmailID.Size = new System.Drawing.Size(63, 13);
            this.lblEmailID.TabIndex = 256;
            this.lblEmailID.Text = "lblEmailID";
            // 
            // lblBuildingName
            // 
            this.lblBuildingName.AutoSize = true;
            this.lblBuildingName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuildingName.Location = new System.Drawing.Point(235, 378);
            this.lblBuildingName.Name = "lblBuildingName";
            this.lblBuildingName.Size = new System.Drawing.Size(97, 13);
            this.lblBuildingName.TabIndex = 255;
            this.lblBuildingName.Text = "lblBuildingName";
            // 
            // lblAODescription
            // 
            this.lblAODescription.AutoSize = true;
            this.lblAODescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAODescription.Location = new System.Drawing.Point(236, 353);
            this.lblAODescription.Name = "lblAODescription";
            this.lblAODescription.Size = new System.Drawing.Size(101, 13);
            this.lblAODescription.TabIndex = 254;
            this.lblAODescription.Text = "lblAODescription";
            // 
            // lblAONumber
            // 
            this.lblAONumber.AutoSize = true;
            this.lblAONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAONumber.Location = new System.Drawing.Point(236, 329);
            this.lblAONumber.Name = "lblAONumber";
            this.lblAONumber.Size = new System.Drawing.Size(80, 13);
            this.lblAONumber.TabIndex = 253;
            this.lblAONumber.Text = "lblAONumber";
            // 
            // lblRangeCode
            // 
            this.lblRangeCode.AutoSize = true;
            this.lblRangeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangeCode.Location = new System.Drawing.Point(235, 307);
            this.lblRangeCode.Name = "lblRangeCode";
            this.lblRangeCode.Size = new System.Drawing.Size(86, 13);
            this.lblRangeCode.TabIndex = 252;
            this.lblRangeCode.Text = "lblRangeCode";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(79, 405);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 13);
            this.label20.TabIndex = 251;
            this.label20.Text = "Email ID";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(79, 378);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 250;
            this.label19.Text = "Building Name";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(79, 353);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 13);
            this.label18.TabIndex = 249;
            this.label18.Text = "AO Description";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(79, 329);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 13);
            this.label17.TabIndex = 248;
            this.label17.Text = "AO Number";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(79, 307);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 247;
            this.label16.Text = "Range Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(81, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 246;
            this.label1.Text = "TAN AO Code";
            // 
            // btnPrintPan
            // 
            this.btnPrintPan.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPan.Location = new System.Drawing.Point(676, 12);
            this.btnPrintPan.Name = "btnPrintPan";
            this.btnPrintPan.Size = new System.Drawing.Size(75, 23);
            this.btnPrintPan.TabIndex = 245;
            this.btnPrintPan.Text = "&Print";
            this.btnPrintPan.UseVisualStyleBackColor = false;
            this.btnPrintPan.Visible = false;
            this.btnPrintPan.Click += new System.EventHandler(this.btnPrintPan_Click);
            // 
            // lblEmailID1
            // 
            this.lblEmailID1.AutoSize = true;
            this.lblEmailID1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailID1.Location = new System.Drawing.Point(237, 168);
            this.lblEmailID1.Name = "lblEmailID1";
            this.lblEmailID1.Size = new System.Drawing.Size(70, 13);
            this.lblEmailID1.TabIndex = 244;
            this.lblEmailID1.Text = "lblEmailID1";
            // 
            // lblStatusofTAN
            // 
            this.lblStatusofTAN.AutoSize = true;
            this.lblStatusofTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusofTAN.Location = new System.Drawing.Point(236, 147);
            this.lblStatusofTAN.Name = "lblStatusofTAN";
            this.lblStatusofTAN.Size = new System.Drawing.Size(92, 13);
            this.lblStatusofTAN.TabIndex = 243;
            this.lblStatusofTAN.Text = "lblStatusofTAN";
            // 
            // lblAOType
            // 
            this.lblAOType.AutoSize = true;
            this.lblAOType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAOType.Location = new System.Drawing.Point(236, 284);
            this.lblAOType.Name = "lblAOType";
            this.lblAOType.Size = new System.Drawing.Size(65, 13);
            this.lblAOType.TabIndex = 242;
            this.lblAOType.Text = "lblAOType";
            // 
            // lblAreaCode
            // 
            this.lblAreaCode.AutoSize = true;
            this.lblAreaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaCode.Location = new System.Drawing.Point(236, 260);
            this.lblAreaCode.Name = "lblAreaCode";
            this.lblAreaCode.Size = new System.Drawing.Size(89, 13);
            this.lblAreaCode.TabIndex = 241;
            this.lblAreaCode.Text = "labelAreaCode";
            // 
            // lblEmailID2
            // 
            this.lblEmailID2.AutoSize = true;
            this.lblEmailID2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailID2.Location = new System.Drawing.Point(239, 195);
            this.lblEmailID2.Name = "lblEmailID2";
            this.lblEmailID2.Size = new System.Drawing.Size(70, 13);
            this.lblEmailID2.TabIndex = 240;
            this.lblEmailID2.Text = "lblEmailID2";
            // 
            // lblCategoryDeductor
            // 
            this.lblCategoryDeductor.AutoSize = true;
            this.lblCategoryDeductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryDeductor.Location = new System.Drawing.Point(236, 49);
            this.lblCategoryDeductor.Name = "lblCategoryDeductor";
            this.lblCategoryDeductor.Size = new System.Drawing.Size(122, 13);
            this.lblCategoryDeductor.TabIndex = 239;
            this.lblCategoryDeductor.Text = "lblCategoryDeductor";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(236, 75);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 13);
            this.lblName.TabIndex = 239;
            this.lblName.Text = "lblName";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(236, 99);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(65, 13);
            this.lblAddress.TabIndex = 238;
            this.lblAddress.Text = "lblAddress";
            // 
            // lblPAN
            // 
            this.lblPAN.AutoSize = true;
            this.lblPAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAN.Location = new System.Drawing.Point(236, 123);
            this.lblPAN.Name = "lblPAN";
            this.lblPAN.Size = new System.Drawing.Size(45, 13);
            this.lblPAN.TabIndex = 237;
            this.lblPAN.Text = "lblPAN";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(79, 284);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 235;
            this.label12.Text = "AO Type";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(79, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 234;
            this.label11.Text = "Area Code";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(79, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 233;
            this.label10.Text = "Email ID 2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(79, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 232;
            this.label9.Text = "Email ID 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(79, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 231;
            this.label4.Text = "PAN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(79, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 230;
            this.label8.Text = "Address";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(79, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 229;
            this.label7.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(82, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 228;
            this.label6.Text = "Category of Deductor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(79, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 227;
            this.label5.Text = "TAN";
            // 
            // lblTAN
            // 
            this.lblTAN.AutoSize = true;
            this.lblTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTAN.Location = new System.Drawing.Point(236, 24);
            this.lblTAN.Name = "lblTAN";
            this.lblTAN.Size = new System.Drawing.Size(56, 16);
            this.lblTAN.TabIndex = 226;
            this.lblTAN.Text = "lblTAN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(79, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 225;
            this.label2.Text = "Status of TAN";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(131, 516);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 227;
            // 
            // grpOTPDetails
            // 
            this.grpOTPDetails.Controls.Add(this.btnBack);
            this.grpOTPDetails.Controls.Add(this.lblOTPSentMsg);
            this.grpOTPDetails.Controls.Add(this.btnValidate);
            this.grpOTPDetails.Controls.Add(this.txtMobileOTP);
            this.grpOTPDetails.Controls.Add(this.label21);
            this.grpOTPDetails.Location = new System.Drawing.Point(157, 179);
            this.grpOTPDetails.Name = "grpOTPDetails";
            this.grpOTPDetails.Size = new System.Drawing.Size(617, 134);
            this.grpOTPDetails.TabIndex = 261;
            this.grpOTPDetails.TabStop = false;
            this.grpOTPDetails.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Lavender;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(310, 90);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 248;
            this.btnBack.Text = "&Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblOTPSentMsg
            // 
            this.lblOTPSentMsg.AutoSize = true;
            this.lblOTPSentMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOTPSentMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblOTPSentMsg.Location = new System.Drawing.Point(138, 26);
            this.lblOTPSentMsg.Name = "lblOTPSentMsg";
            this.lblOTPSentMsg.Size = new System.Drawing.Size(302, 13);
            this.lblOTPSentMsg.TabIndex = 247;
            this.lblOTPSentMsg.Text = "Please provide the OTP sent to your mobile number:";
            // 
            // btnValidate
            // 
            this.btnValidate.BackColor = System.Drawing.Color.Lavender;
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidate.Location = new System.Drawing.Point(229, 90);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 246;
            this.btnValidate.Text = "&Validate";
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtMobileOTP
            // 
            this.txtMobileOTP.Location = new System.Drawing.Point(229, 60);
            this.txtMobileOTP.Name = "txtMobileOTP";
            this.txtMobileOTP.Size = new System.Drawing.Size(199, 20);
            this.txtMobileOTP.TabIndex = 236;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Blue;
            this.label21.Location = new System.Drawing.Point(138, 63);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 13);
            this.label21.TabIndex = 235;
            this.label21.Text = "Mobile OTP";
            // 
            // TrnKnowYourTAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 575);
            this.Name = "TrnKnowYourTAN";
            this.Load += new System.EventHandler(this.TrnPanVarification_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpSearchBox.ResumeLayout(false);
            this.grpSearchBox.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpOTPDetails.ResumeLayout(false);
            this.grpOTPDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpSearchBox;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtName_TAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblEmailID1;
        private System.Windows.Forms.Label lblStatusofTAN;
        private System.Windows.Forms.Label lblAOType;
        private System.Windows.Forms.Label lblAreaCode;
        private System.Windows.Forms.Label lblEmailID2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPAN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnPrintPan;
        private System.Windows.Forms.RadioButton rbnSearchByTAN;
        private System.Windows.Forms.RadioButton rbnSearchByName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblNameTAN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblEmailID;
        private System.Windows.Forms.Label lblBuildingName;
        private System.Windows.Forms.Label lblAODescription;
        private System.Windows.Forms.Label lblAONumber;
        private System.Windows.Forms.Label lblRangeCode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.ComboBox cmbCategoryDeductor;
        private System.Windows.Forms.GroupBox grpOTPDetails;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblOTPSentMsg;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.TextBox txtMobileOTP;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnBacktoSearch;
        private System.Windows.Forms.Label lblCategoryDeductor;
    }
}
