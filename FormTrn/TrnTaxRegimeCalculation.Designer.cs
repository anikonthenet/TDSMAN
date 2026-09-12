namespace TDSMAN.FormTrn
{
    partial class TrnTaxRegimeCalculation
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
            this.dgvEmployees = new DGVControl.DGVControl();
            this.grpHeaderDetails = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFAYear = new System.Windows.Forms.Label();
            this.lblCompanyWithTAN = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.grpHeaderDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(16, 561);
            this.grpSort.Size = new System.Drawing.Size(18, 21);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(502, 13);
            this.BtnCancel.Text = "&Close";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(418, 13);
            this.BtnSave.Text = "&Export";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(40, 561);
            this.grpSearch.Size = new System.Drawing.Size(10, 13);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(197, 12);
            this.BtnEdit.Size = new System.Drawing.Size(19, 25);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(27, 25);
            this.BtnAdd.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Tax Calculator – Old / New Regime";
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(864, 17);
            this.BtnExit.Size = new System.Drawing.Size(18, 25);
            this.BtnExit.Visible = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(837, 16);
            this.BtnRefresh.Size = new System.Drawing.Size(21, 25);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(803, 16);
            this.BtnDelete.Size = new System.Drawing.Size(33, 25);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(769, 17);
            this.BtnSearch.Size = new System.Drawing.Size(28, 25);
            this.BtnSearch.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpHeaderDetails);
            this.pnlControls.Controls.Add(this.dgvEmployees);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AllowUserToOrderColumns = true;
            this.dgvEmployees.AllowUserToResizeRows = false;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEmployees.GridColor = System.Drawing.SystemColors.Control;
            this.dgvEmployees.Location = new System.Drawing.Point(29, 79);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.RowHeadersWidth = 20;
            this.dgvEmployees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(945, 463);
            this.dgvEmployees.TabIndex = 186;
            this.dgvEmployees.TabStop = false;
            this.dgvEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvEmployees_CellClick);
            // 
            // grpHeaderDetails
            // 
            this.grpHeaderDetails.Controls.Add(this.btnLoad);
            this.grpHeaderDetails.Controls.Add(this.cmbFinancialYear);
            this.grpHeaderDetails.Controls.Add(this.label1);
            this.grpHeaderDetails.Controls.Add(this.cmbCompany);
            this.grpHeaderDetails.Controls.Add(this.label5);
            this.grpHeaderDetails.Controls.Add(this.lblStatus);
            this.grpHeaderDetails.Controls.Add(this.lblFAYear);
            this.grpHeaderDetails.Controls.Add(this.lblCompanyWithTAN);
            this.grpHeaderDetails.Location = new System.Drawing.Point(174, 5);
            this.grpHeaderDetails.Name = "grpHeaderDetails";
            this.grpHeaderDetails.Size = new System.Drawing.Size(654, 70);
            this.grpHeaderDetails.TabIndex = 187;
            this.grpHeaderDetails.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(581, 38);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(67, 23);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(149, 14);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 21);
            this.cmbFinancialYear.TabIndex = 17;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.CmbFinancialYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Select Tax Year";
            // 
            // cmbCompany
            // 
            this.cmbCompany.BackColor = System.Drawing.Color.White;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(149, 40);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(431, 21);
            this.cmbCompany.TabIndex = 18;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.CmbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(43, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Select Company";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(451, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFAYear
            // 
            this.lblFAYear.BackColor = System.Drawing.Color.White;
            this.lblFAYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFAYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFAYear.Location = new System.Drawing.Point(11, 42);
            this.lblFAYear.Name = "lblFAYear";
            this.lblFAYear.Size = new System.Drawing.Size(11, 23);
            this.lblFAYear.TabIndex = 1;
            this.lblFAYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFAYear.Visible = false;
            // 
            // lblCompanyWithTAN
            // 
            this.lblCompanyWithTAN.BackColor = System.Drawing.Color.White;
            this.lblCompanyWithTAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompanyWithTAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyWithTAN.Location = new System.Drawing.Point(11, 14);
            this.lblCompanyWithTAN.Name = "lblCompanyWithTAN";
            this.lblCompanyWithTAN.Size = new System.Drawing.Size(11, 23);
            this.lblCompanyWithTAN.TabIndex = 0;
            this.lblCompanyWithTAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCompanyWithTAN.Visible = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // TrnTaxRegimeCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Name = "TrnTaxRegimeCalculation";
            this.Activated += new System.EventHandler(this.TrnTaxRegimeCalculation_Activated);
            this.Load += new System.EventHandler(this.TrnTaxRegimeCalculation_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.grpHeaderDetails.ResumeLayout(false);
            this.grpHeaderDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DGVControl.DGVControl dgvEmployees;
        private System.Windows.Forms.GroupBox grpHeaderDetails;
        private System.Windows.Forms.Label lblCompanyWithTAN;
        private System.Windows.Forms.Label lblFAYear;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoad;
    }
}
