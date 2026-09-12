namespace TDSMAN.FormTrn
{
    partial class TrnTabbedDashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnTabbedDashboard));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lnkExport = new System.Windows.Forms.LinkLabel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.lblFilingStatus = new System.Windows.Forms.Label();
            this.lblFiledReturns = new System.Windows.Forms.Label();
            this.lblReturnsUnderProcess = new System.Windows.Forms.Label();
            this.lblReturnsReadyForFiling = new System.Windows.Forms.Label();
            this.chkMakeDashboardDefaultHomeScreen = new System.Windows.Forms.CheckBox();
            this.pnlTopLine = new System.Windows.Forms.Panel();
            this.lblDashboardCaption = new System.Windows.Forms.Label();
            this.tbcTabControl = new System.Windows.Forms.TabControl();
            this.tbpReturnUnderProcess = new System.Windows.Forms.TabPage();
            this.grpRetursUnderProcess = new System.Windows.Forms.GroupBox();
            this.lblReturnsAccessedFooterMessage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalCountUP = new System.Windows.Forms.Label();
            this.txtSortOnReturnsUnderProcess = new System.Windows.Forms.Label();
            this.pnlUP = new System.Windows.Forms.Panel();
            this.dgvReturnsUnderProcessFirstScreen = new DGVControl.DGVControl();
            this.txtSearchHereReturnsUnderProcess = new System.Windows.Forms.TextBox();
            this.tbpReturnsReadyForFiling = new System.Windows.Forms.TabPage();
            this.grpReturnsReadyForFiling = new System.Windows.Forms.GroupBox();
            this.lblReturnsGeneratedFooterMessage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalCountRF = new System.Windows.Forms.Label();
            this.pnlRF = new System.Windows.Forms.Panel();
            this.txtSearchReturnsReadyForFiling = new System.Windows.Forms.TextBox();
            this.dgvReturnsReadyForFiling = new DGVControl.DGVControl();
            this.tbpFiledReturns = new System.Windows.Forms.TabPage();
            this.grpFiledReturns = new System.Windows.Forms.GroupBox();
            this.lblReturnsFiledFooterMessage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalCountFR = new System.Windows.Forms.Label();
            this.pnlFR = new System.Windows.Forms.Panel();
            this.txtSearchFiledReturns = new System.Windows.Forms.TextBox();
            this.dgvFiledReturns = new DGVControl.DGVControl();
            this.tbpFilingStatus = new System.Windows.Forms.TabPage();
            this.grpFilingStatus = new System.Windows.Forms.GroupBox();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReturnsStatusFooterMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvFilingStatus = new DGVControl.DGVControl();
            this.txtSearchFilingStatus = new System.Windows.Forms.TextBox();
            this.ctMenuReturnsUnderProcess = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuLastWorkedOnUP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLatestReturnUP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilingDueDateUP = new System.Windows.Forms.ToolStripMenuItem();
            this.ctMenuReturnReadyForFiling = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuLatestGeneratedRF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLatestReturnRF = new System.Windows.Forms.ToolStripMenuItem();
            this.ctMenuFiledReturns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDateOfFiling = new System.Windows.Forms.ToolStripMenuItem();
            this.tltpHomeScreen = new System.Windows.Forms.ToolTip(this.components);
            this.bgBackGroundWorker = new System.ComponentModel.BackgroundWorker();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tbcTabControl.SuspendLayout();
            this.tbpReturnUnderProcess.SuspendLayout();
            this.grpRetursUnderProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnsUnderProcessFirstScreen)).BeginInit();
            this.tbpReturnsReadyForFiling.SuspendLayout();
            this.grpReturnsReadyForFiling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnsReadyForFiling)).BeginInit();
            this.tbpFiledReturns.SuspendLayout();
            this.grpFiledReturns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiledReturns)).BeginInit();
            this.tbpFilingStatus.SuspendLayout();
            this.grpFilingStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilingStatus)).BeginInit();
            this.ctMenuReturnsUnderProcess.SuspendLayout();
            this.ctMenuReturnReadyForFiling.SuspendLayout();
            this.ctMenuFiledReturns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pctUserManual);
            this.pnlMain.Controls.Add(this.lnkExport);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.chkMakeDashboardDefaultHomeScreen);
            this.pnlMain.Controls.Add(this.pnlTopLine);
            this.pnlMain.Controls.Add(this.lblDashboardCaption);
            this.pnlMain.Controls.Add(this.tbcTabControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1030, 672);
            this.pnlMain.TabIndex = 0;
            // 
            // lnkExport
            // 
            this.lnkExport.AutoSize = true;
            this.lnkExport.Location = new System.Drawing.Point(6, 12);
            this.lnkExport.Name = "lnkExport";
            this.lnkExport.Size = new System.Drawing.Size(37, 13);
            this.lnkExport.TabIndex = 206;
            this.lnkExport.TabStop = true;
            this.lnkExport.Text = "Export";
            this.lnkExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkExport_LinkClicked);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnExit.Location = new System.Drawing.Point(822, 642);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(83, 23);
            this.BtnExit.TabIndex = 205;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Visible = false;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.lblFilingStatus);
            this.pnlButtons.Controls.Add(this.lblFiledReturns);
            this.pnlButtons.Controls.Add(this.lblReturnsUnderProcess);
            this.pnlButtons.Controls.Add(this.lblReturnsReadyForFiling);
            this.pnlButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlButtons.Location = new System.Drawing.Point(8, 40);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1015, 35);
            this.pnlButtons.TabIndex = 49;
            // 
            // lblFilingStatus
            // 
            this.lblFilingStatus.BackColor = System.Drawing.Color.AliceBlue;
            this.lblFilingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilingStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFilingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilingStatus.ForeColor = System.Drawing.Color.Black;
            this.lblFilingStatus.Location = new System.Drawing.Point(750, -1);
            this.lblFilingStatus.Name = "lblFilingStatus";
            this.lblFilingStatus.Size = new System.Drawing.Size(262, 34);
            this.lblFilingStatus.TabIndex = 3;
            this.lblFilingStatus.Text = "Filing Status";
            this.lblFilingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFilingStatus.Click += new System.EventHandler(this.lblFilingStatus_Click);
            // 
            // lblFiledReturns
            // 
            this.lblFiledReturns.BackColor = System.Drawing.Color.AliceBlue;
            this.lblFiledReturns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFiledReturns.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFiledReturns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiledReturns.ForeColor = System.Drawing.Color.Black;
            this.lblFiledReturns.Location = new System.Drawing.Point(489, -1);
            this.lblFiledReturns.Name = "lblFiledReturns";
            this.lblFiledReturns.Size = new System.Drawing.Size(262, 34);
            this.lblFiledReturns.TabIndex = 2;
            this.lblFiledReturns.Text = "Filed Returns";
            this.lblFiledReturns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFiledReturns.Click += new System.EventHandler(this.lblFiledReturns_Click);
            // 
            // lblReturnsUnderProcess
            // 
            this.lblReturnsUnderProcess.BackColor = System.Drawing.Color.AliceBlue;
            this.lblReturnsUnderProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReturnsUnderProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReturnsUnderProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsUnderProcess.ForeColor = System.Drawing.Color.Black;
            this.lblReturnsUnderProcess.Location = new System.Drawing.Point(-1, -1);
            this.lblReturnsUnderProcess.Name = "lblReturnsUnderProcess";
            this.lblReturnsUnderProcess.Size = new System.Drawing.Size(232, 34);
            this.lblReturnsUnderProcess.TabIndex = 1;
            this.lblReturnsUnderProcess.Text = "Returns Under Process";
            this.lblReturnsUnderProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReturnsUnderProcess.Click += new System.EventHandler(this.lblReturnsUnderProcess_Click);
            // 
            // lblReturnsReadyForFiling
            // 
            this.lblReturnsReadyForFiling.BackColor = System.Drawing.Color.AliceBlue;
            this.lblReturnsReadyForFiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReturnsReadyForFiling.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReturnsReadyForFiling.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsReadyForFiling.ForeColor = System.Drawing.Color.Black;
            this.lblReturnsReadyForFiling.Location = new System.Drawing.Point(230, -1);
            this.lblReturnsReadyForFiling.Name = "lblReturnsReadyForFiling";
            this.lblReturnsReadyForFiling.Size = new System.Drawing.Size(260, 34);
            this.lblReturnsReadyForFiling.TabIndex = 0;
            this.lblReturnsReadyForFiling.Text = "Returns Ready for Filing";
            this.lblReturnsReadyForFiling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReturnsReadyForFiling.Click += new System.EventHandler(this.lblReturnsReadyForFiling_Click);
            // 
            // chkMakeDashboardDefaultHomeScreen
            // 
            this.chkMakeDashboardDefaultHomeScreen.AutoSize = true;
            this.chkMakeDashboardDefaultHomeScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMakeDashboardDefaultHomeScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMakeDashboardDefaultHomeScreen.ForeColor = System.Drawing.Color.Blue;
            this.chkMakeDashboardDefaultHomeScreen.Location = new System.Drawing.Point(828, 9);
            this.chkMakeDashboardDefaultHomeScreen.Name = "chkMakeDashboardDefaultHomeScreen";
            this.chkMakeDashboardDefaultHomeScreen.Size = new System.Drawing.Size(193, 19);
            this.chkMakeDashboardDefaultHomeScreen.TabIndex = 46;
            this.chkMakeDashboardDefaultHomeScreen.Text = "Set Dashboard as Home Page";
            this.tltpHomeScreen.SetToolTip(this.chkMakeDashboardDefaultHomeScreen, "Dashboard will show up by default whenever the software starts");
            this.chkMakeDashboardDefaultHomeScreen.UseVisualStyleBackColor = true;
            this.chkMakeDashboardDefaultHomeScreen.CheckedChanged += new System.EventHandler(this.chkMakeDashboardDefaultHomeScreen_CheckedChanged);
            // 
            // pnlTopLine
            // 
            this.pnlTopLine.BackColor = System.Drawing.Color.Black;
            this.pnlTopLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTopLine.Location = new System.Drawing.Point(5, 35);
            this.pnlTopLine.Name = "pnlTopLine";
            this.pnlTopLine.Size = new System.Drawing.Size(1022, 1);
            this.pnlTopLine.TabIndex = 48;
            // 
            // lblDashboardCaption
            // 
            this.lblDashboardCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(94)))), ((int)(((byte)(187)))));
            this.lblDashboardCaption.Location = new System.Drawing.Point(0, 3);
            this.lblDashboardCaption.Name = "lblDashboardCaption";
            this.lblDashboardCaption.Size = new System.Drawing.Size(1030, 29);
            this.lblDashboardCaption.TabIndex = 47;
            this.lblDashboardCaption.Text = "D a s h b o a r d";
            this.lblDashboardCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbcTabControl
            // 
            this.tbcTabControl.Controls.Add(this.tbpReturnUnderProcess);
            this.tbcTabControl.Controls.Add(this.tbpReturnsReadyForFiling);
            this.tbcTabControl.Controls.Add(this.tbpFiledReturns);
            this.tbcTabControl.Controls.Add(this.tbpFilingStatus);
            this.tbcTabControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbcTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcTabControl.Location = new System.Drawing.Point(5, 40);
            this.tbcTabControl.Multiline = true;
            this.tbcTabControl.Name = "tbcTabControl";
            this.tbcTabControl.SelectedIndex = 0;
            this.tbcTabControl.Size = new System.Drawing.Size(1022, 600);
            this.tbcTabControl.TabIndex = 1;
            // 
            // tbpReturnUnderProcess
            // 
            this.tbpReturnUnderProcess.AllowDrop = true;
            this.tbpReturnUnderProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbpReturnUnderProcess.Controls.Add(this.grpRetursUnderProcess);
            this.tbpReturnUnderProcess.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbpReturnUnderProcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbpReturnUnderProcess.Location = new System.Drawing.Point(4, 25);
            this.tbpReturnUnderProcess.Name = "tbpReturnUnderProcess";
            this.tbpReturnUnderProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tbpReturnUnderProcess.Size = new System.Drawing.Size(1014, 571);
            this.tbpReturnUnderProcess.TabIndex = 0;
            this.tbpReturnUnderProcess.ToolTipText = "1";
            this.tbpReturnUnderProcess.UseVisualStyleBackColor = true;
            // 
            // grpRetursUnderProcess
            // 
            this.grpRetursUnderProcess.Controls.Add(this.lblReturnsAccessedFooterMessage);
            this.grpRetursUnderProcess.Controls.Add(this.label3);
            this.grpRetursUnderProcess.Controls.Add(this.lblTotalCountUP);
            this.grpRetursUnderProcess.Controls.Add(this.txtSortOnReturnsUnderProcess);
            this.grpRetursUnderProcess.Controls.Add(this.pnlUP);
            this.grpRetursUnderProcess.Controls.Add(this.dgvReturnsUnderProcessFirstScreen);
            this.grpRetursUnderProcess.Controls.Add(this.txtSearchHereReturnsUnderProcess);
            this.grpRetursUnderProcess.Location = new System.Drawing.Point(3, 3);
            this.grpRetursUnderProcess.Name = "grpRetursUnderProcess";
            this.grpRetursUnderProcess.Size = new System.Drawing.Size(1010, 571);
            this.grpRetursUnderProcess.TabIndex = 1;
            this.grpRetursUnderProcess.TabStop = false;
            // 
            // lblReturnsAccessedFooterMessage
            // 
            this.lblReturnsAccessedFooterMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnsAccessedFooterMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblReturnsAccessedFooterMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsAccessedFooterMessage.ForeColor = System.Drawing.Color.Black;
            this.lblReturnsAccessedFooterMessage.Location = new System.Drawing.Point(747, 533);
            this.lblReturnsAccessedFooterMessage.Name = "lblReturnsAccessedFooterMessage";
            this.lblReturnsAccessedFooterMessage.Size = new System.Drawing.Size(243, 17);
            this.lblReturnsAccessedFooterMessage.TabIndex = 206;
            this.lblReturnsAccessedFooterMessage.Text = "[based on Returns accessed in the last 120 days]";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(11, 512);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(484, 50);
            this.label3.TabIndex = 205;
            this.label3.Text = "* R - Regular | C- Correction\r\nTo open a Return from the grid, double click on th" +
    "e record\r\nThe sort order may be changed by clicking on the column header";
            // 
            // lblTotalCountUP
            // 
            this.lblTotalCountUP.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCountUP.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblTotalCountUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCountUP.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCountUP.Location = new System.Drawing.Point(813, 512);
            this.lblTotalCountUP.Name = "lblTotalCountUP";
            this.lblTotalCountUP.Size = new System.Drawing.Size(179, 17);
            this.lblTotalCountUP.TabIndex = 203;
            // 
            // txtSortOnReturnsUnderProcess
            // 
            this.txtSortOnReturnsUnderProcess.BackColor = System.Drawing.Color.Azure;
            this.txtSortOnReturnsUnderProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSortOnReturnsUnderProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtSortOnReturnsUnderProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSortOnReturnsUnderProcess.ForeColor = System.Drawing.Color.Green;
            this.txtSortOnReturnsUnderProcess.Location = new System.Drawing.Point(898, 14);
            this.txtSortOnReturnsUnderProcess.Name = "txtSortOnReturnsUnderProcess";
            this.txtSortOnReturnsUnderProcess.Size = new System.Drawing.Size(94, 21);
            this.txtSortOnReturnsUnderProcess.TabIndex = 202;
            this.txtSortOnReturnsUnderProcess.Text = "Sort";
            this.txtSortOnReturnsUnderProcess.Visible = false;
            this.txtSortOnReturnsUnderProcess.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSortOnReturnsUnderProcess_MouseClick);
            // 
            // pnlUP
            // 
            this.pnlUP.BackColor = System.Drawing.Color.Black;
            this.pnlUP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUP.Location = new System.Drawing.Point(6, 41);
            this.pnlUP.Name = "pnlUP";
            this.pnlUP.Size = new System.Drawing.Size(998, 1);
            this.pnlUP.TabIndex = 200;
            // 
            // dgvReturnsUnderProcessFirstScreen
            // 
            this.dgvReturnsUnderProcessFirstScreen.AllowUserToAddRows = false;
            this.dgvReturnsUnderProcessFirstScreen.AllowUserToDeleteRows = false;
            this.dgvReturnsUnderProcessFirstScreen.AllowUserToOrderColumns = true;
            this.dgvReturnsUnderProcessFirstScreen.AllowUserToResizeRows = false;
            this.dgvReturnsUnderProcessFirstScreen.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvReturnsUnderProcessFirstScreen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReturnsUnderProcessFirstScreen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReturnsUnderProcessFirstScreen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnsUnderProcessFirstScreen.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvReturnsUnderProcessFirstScreen.GridColor = System.Drawing.SystemColors.Control;
            this.dgvReturnsUnderProcessFirstScreen.Location = new System.Drawing.Point(13, 49);
            this.dgvReturnsUnderProcessFirstScreen.MultiSelect = false;
            this.dgvReturnsUnderProcessFirstScreen.Name = "dgvReturnsUnderProcessFirstScreen";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReturnsUnderProcessFirstScreen.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReturnsUnderProcessFirstScreen.RowHeadersWidth = 20;
            this.dgvReturnsUnderProcessFirstScreen.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReturnsUnderProcessFirstScreen.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReturnsUnderProcessFirstScreen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnsUnderProcessFirstScreen.Size = new System.Drawing.Size(979, 460);
            this.dgvReturnsUnderProcessFirstScreen.TabIndex = 188;
            this.dgvReturnsUnderProcessFirstScreen.TabStop = false;
            this.dgvReturnsUnderProcessFirstScreen.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnsUnderProcessFirstScreen_CellClick);
            this.dgvReturnsUnderProcessFirstScreen.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReturnsUnderProcessFirstScreen_CellFormatting);
            this.dgvReturnsUnderProcessFirstScreen.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnsUnderProcessFirstScreen_CellMouseEnter);
            this.dgvReturnsUnderProcessFirstScreen.DoubleClick += new System.EventHandler(this.dgvReturnsUnderProcessFirstScreen_DoubleClick);
            this.dgvReturnsUnderProcessFirstScreen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvReturnsUnderProcessFirstScreen_KeyPress);
            // 
            // txtSearchHereReturnsUnderProcess
            // 
            this.txtSearchHereReturnsUnderProcess.BackColor = System.Drawing.Color.Azure;
            this.txtSearchHereReturnsUnderProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchHereReturnsUnderProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchHereReturnsUnderProcess.ForeColor = System.Drawing.Color.SlateGray;
            this.txtSearchHereReturnsUnderProcess.Location = new System.Drawing.Point(13, 14);
            this.txtSearchHereReturnsUnderProcess.Name = "txtSearchHereReturnsUnderProcess";
            this.txtSearchHereReturnsUnderProcess.Size = new System.Drawing.Size(979, 21);
            this.txtSearchHereReturnsUnderProcess.TabIndex = 190;
            this.txtSearchHereReturnsUnderProcess.Text = "-- Search here (min 3 chars) --";
            this.txtSearchHereReturnsUnderProcess.TextChanged += new System.EventHandler(this.txtSearchHereReturnsUnderProcess_TextChanged);
            this.txtSearchHereReturnsUnderProcess.Enter += new System.EventHandler(this.txtSearchHereReturnsUnderProcess_Enter);
            this.txtSearchHereReturnsUnderProcess.Leave += new System.EventHandler(this.txtSearchHereReturnsUnderProcess_Leave);
            // 
            // tbpReturnsReadyForFiling
            // 
            this.tbpReturnsReadyForFiling.AllowDrop = true;
            this.tbpReturnsReadyForFiling.BackColor = System.Drawing.Color.Transparent;
            this.tbpReturnsReadyForFiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbpReturnsReadyForFiling.Controls.Add(this.grpReturnsReadyForFiling);
            this.tbpReturnsReadyForFiling.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbpReturnsReadyForFiling.Location = new System.Drawing.Point(4, 25);
            this.tbpReturnsReadyForFiling.Name = "tbpReturnsReadyForFiling";
            this.tbpReturnsReadyForFiling.Padding = new System.Windows.Forms.Padding(3);
            this.tbpReturnsReadyForFiling.Size = new System.Drawing.Size(1014, 571);
            this.tbpReturnsReadyForFiling.TabIndex = 1;
            this.tbpReturnsReadyForFiling.ToolTipText = "2";
            // 
            // grpReturnsReadyForFiling
            // 
            this.grpReturnsReadyForFiling.Controls.Add(this.lblReturnsGeneratedFooterMessage);
            this.grpReturnsReadyForFiling.Controls.Add(this.label6);
            this.grpReturnsReadyForFiling.Controls.Add(this.lblTotalCountRF);
            this.grpReturnsReadyForFiling.Controls.Add(this.pnlRF);
            this.grpReturnsReadyForFiling.Controls.Add(this.txtSearchReturnsReadyForFiling);
            this.grpReturnsReadyForFiling.Controls.Add(this.dgvReturnsReadyForFiling);
            this.grpReturnsReadyForFiling.Location = new System.Drawing.Point(3, 3);
            this.grpReturnsReadyForFiling.Name = "grpReturnsReadyForFiling";
            this.grpReturnsReadyForFiling.Size = new System.Drawing.Size(1010, 567);
            this.grpReturnsReadyForFiling.TabIndex = 2;
            this.grpReturnsReadyForFiling.TabStop = false;
            // 
            // lblReturnsGeneratedFooterMessage
            // 
            this.lblReturnsGeneratedFooterMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnsGeneratedFooterMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblReturnsGeneratedFooterMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsGeneratedFooterMessage.ForeColor = System.Drawing.Color.Black;
            this.lblReturnsGeneratedFooterMessage.Location = new System.Drawing.Point(747, 533);
            this.lblReturnsGeneratedFooterMessage.Name = "lblReturnsGeneratedFooterMessage";
            this.lblReturnsGeneratedFooterMessage.Size = new System.Drawing.Size(243, 17);
            this.lblReturnsGeneratedFooterMessage.TabIndex = 208;
            this.lblReturnsGeneratedFooterMessage.Text = "[based on Returns generated in the last 120 days]";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 513);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(484, 50);
            this.label6.TabIndex = 207;
            this.label6.Text = "* R - Regular | C- Correction\r\nTo open a Return from the grid, double click on th" +
    "e record\r\nThe sort order may be changed by clicking on the column header";
            // 
            // lblTotalCountRF
            // 
            this.lblTotalCountRF.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCountRF.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblTotalCountRF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCountRF.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCountRF.Location = new System.Drawing.Point(813, 512);
            this.lblTotalCountRF.Name = "lblTotalCountRF";
            this.lblTotalCountRF.Size = new System.Drawing.Size(179, 17);
            this.lblTotalCountRF.TabIndex = 205;
            // 
            // pnlRF
            // 
            this.pnlRF.BackColor = System.Drawing.Color.Black;
            this.pnlRF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRF.Location = new System.Drawing.Point(6, 41);
            this.pnlRF.Name = "pnlRF";
            this.pnlRF.Size = new System.Drawing.Size(998, 1);
            this.pnlRF.TabIndex = 201;
            // 
            // txtSearchReturnsReadyForFiling
            // 
            this.txtSearchReturnsReadyForFiling.BackColor = System.Drawing.Color.Azure;
            this.txtSearchReturnsReadyForFiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchReturnsReadyForFiling.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchReturnsReadyForFiling.ForeColor = System.Drawing.Color.SlateGray;
            this.txtSearchReturnsReadyForFiling.Location = new System.Drawing.Point(13, 14);
            this.txtSearchReturnsReadyForFiling.Name = "txtSearchReturnsReadyForFiling";
            this.txtSearchReturnsReadyForFiling.Size = new System.Drawing.Size(979, 21);
            this.txtSearchReturnsReadyForFiling.TabIndex = 193;
            this.txtSearchReturnsReadyForFiling.Text = "-- Search here --";
            this.txtSearchReturnsReadyForFiling.TextChanged += new System.EventHandler(this.txtSearchReturnsReadyForFiling_TextChanged);
            this.txtSearchReturnsReadyForFiling.Enter += new System.EventHandler(this.txtSearchReturnsReadyForFiling_Enter);
            this.txtSearchReturnsReadyForFiling.Leave += new System.EventHandler(this.txtSearchReturnsReadyForFiling_Leave);
            // 
            // dgvReturnsReadyForFiling
            // 
            this.dgvReturnsReadyForFiling.AllowUserToAddRows = false;
            this.dgvReturnsReadyForFiling.AllowUserToDeleteRows = false;
            this.dgvReturnsReadyForFiling.AllowUserToOrderColumns = true;
            this.dgvReturnsReadyForFiling.AllowUserToResizeRows = false;
            this.dgvReturnsReadyForFiling.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvReturnsReadyForFiling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReturnsReadyForFiling.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReturnsReadyForFiling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnsReadyForFiling.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvReturnsReadyForFiling.GridColor = System.Drawing.SystemColors.Control;
            this.dgvReturnsReadyForFiling.Location = new System.Drawing.Point(13, 49);
            this.dgvReturnsReadyForFiling.MultiSelect = false;
            this.dgvReturnsReadyForFiling.Name = "dgvReturnsReadyForFiling";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReturnsReadyForFiling.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReturnsReadyForFiling.RowHeadersWidth = 20;
            this.dgvReturnsReadyForFiling.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReturnsReadyForFiling.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReturnsReadyForFiling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnsReadyForFiling.Size = new System.Drawing.Size(979, 460);
            this.dgvReturnsReadyForFiling.TabIndex = 191;
            this.dgvReturnsReadyForFiling.TabStop = false;
            this.dgvReturnsReadyForFiling.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnsReadyForFiling_CellClick);
            this.dgvReturnsReadyForFiling.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReturnsReadyForFiling_CellFormatting);
            this.dgvReturnsReadyForFiling.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnsReadyForFiling_CellMouseEnter);
            this.dgvReturnsReadyForFiling.DoubleClick += new System.EventHandler(this.dgvReturnsReadyForFiling_DoubleClick);
            // 
            // tbpFiledReturns
            // 
            this.tbpFiledReturns.AllowDrop = true;
            this.tbpFiledReturns.BackColor = System.Drawing.Color.Transparent;
            this.tbpFiledReturns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbpFiledReturns.Controls.Add(this.grpFiledReturns);
            this.tbpFiledReturns.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbpFiledReturns.Location = new System.Drawing.Point(4, 25);
            this.tbpFiledReturns.Name = "tbpFiledReturns";
            this.tbpFiledReturns.Size = new System.Drawing.Size(1014, 571);
            this.tbpFiledReturns.TabIndex = 2;
            this.tbpFiledReturns.ToolTipText = "3";
            // 
            // grpFiledReturns
            // 
            this.grpFiledReturns.Controls.Add(this.lblReturnsFiledFooterMessage);
            this.grpFiledReturns.Controls.Add(this.label8);
            this.grpFiledReturns.Controls.Add(this.lblTotalCountFR);
            this.grpFiledReturns.Controls.Add(this.pnlFR);
            this.grpFiledReturns.Controls.Add(this.txtSearchFiledReturns);
            this.grpFiledReturns.Controls.Add(this.dgvFiledReturns);
            this.grpFiledReturns.Location = new System.Drawing.Point(3, 3);
            this.grpFiledReturns.Name = "grpFiledReturns";
            this.grpFiledReturns.Size = new System.Drawing.Size(1015, 567);
            this.grpFiledReturns.TabIndex = 3;
            this.grpFiledReturns.TabStop = false;
            // 
            // lblReturnsFiledFooterMessage
            // 
            this.lblReturnsFiledFooterMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnsFiledFooterMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblReturnsFiledFooterMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsFiledFooterMessage.ForeColor = System.Drawing.Color.Black;
            this.lblReturnsFiledFooterMessage.Location = new System.Drawing.Point(776, 533);
            this.lblReturnsFiledFooterMessage.Name = "lblReturnsFiledFooterMessage";
            this.lblReturnsFiledFooterMessage.Size = new System.Drawing.Size(215, 17);
            this.lblReturnsFiledFooterMessage.TabIndex = 208;
            this.lblReturnsFiledFooterMessage.Text = "[based on Returns filed in the last 90 days]";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(11, 513);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(484, 50);
            this.label8.TabIndex = 207;
            this.label8.Text = "* R - Regular | C- Correction\r\nTo open a Return from the grid, double click on th" +
    "e record\r\nThe sort order may be changed by clicking on the column header";
            // 
            // lblTotalCountFR
            // 
            this.lblTotalCountFR.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCountFR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTotalCountFR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCountFR.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCountFR.Location = new System.Drawing.Point(813, 512);
            this.lblTotalCountFR.Name = "lblTotalCountFR";
            this.lblTotalCountFR.Size = new System.Drawing.Size(177, 17);
            this.lblTotalCountFR.TabIndex = 204;
            // 
            // pnlFR
            // 
            this.pnlFR.BackColor = System.Drawing.Color.Black;
            this.pnlFR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFR.Location = new System.Drawing.Point(6, 41);
            this.pnlFR.Name = "pnlFR";
            this.pnlFR.Size = new System.Drawing.Size(998, 1);
            this.pnlFR.TabIndex = 201;
            // 
            // txtSearchFiledReturns
            // 
            this.txtSearchFiledReturns.BackColor = System.Drawing.Color.Azure;
            this.txtSearchFiledReturns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchFiledReturns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchFiledReturns.ForeColor = System.Drawing.Color.SlateGray;
            this.txtSearchFiledReturns.Location = new System.Drawing.Point(13, 14);
            this.txtSearchFiledReturns.Name = "txtSearchFiledReturns";
            this.txtSearchFiledReturns.Size = new System.Drawing.Size(979, 21);
            this.txtSearchFiledReturns.TabIndex = 193;
            this.txtSearchFiledReturns.Text = "-- Search here --";
            this.txtSearchFiledReturns.TextChanged += new System.EventHandler(this.txtSearchFiledReturns_TextChanged);
            this.txtSearchFiledReturns.Enter += new System.EventHandler(this.txtSearchFiledReturns_Enter);
            this.txtSearchFiledReturns.Leave += new System.EventHandler(this.txtSearchFiledReturns_Leave);
            // 
            // dgvFiledReturns
            // 
            this.dgvFiledReturns.AllowUserToAddRows = false;
            this.dgvFiledReturns.AllowUserToDeleteRows = false;
            this.dgvFiledReturns.AllowUserToOrderColumns = true;
            this.dgvFiledReturns.AllowUserToResizeRows = false;
            this.dgvFiledReturns.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvFiledReturns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiledReturns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFiledReturns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiledReturns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFiledReturns.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFiledReturns.Location = new System.Drawing.Point(13, 49);
            this.dgvFiledReturns.MultiSelect = false;
            this.dgvFiledReturns.Name = "dgvFiledReturns";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiledReturns.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvFiledReturns.RowHeadersWidth = 20;
            this.dgvFiledReturns.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFiledReturns.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFiledReturns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiledReturns.Size = new System.Drawing.Size(979, 460);
            this.dgvFiledReturns.TabIndex = 191;
            this.dgvFiledReturns.TabStop = false;
            this.dgvFiledReturns.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFiledReturns_CellFormatting);
            this.dgvFiledReturns.DoubleClick += new System.EventHandler(this.dgvFiledReturns_DoubleClick);
            // 
            // tbpFilingStatus
            // 
            this.tbpFilingStatus.Controls.Add(this.grpFilingStatus);
            this.tbpFilingStatus.Location = new System.Drawing.Point(4, 25);
            this.tbpFilingStatus.Name = "tbpFilingStatus";
            this.tbpFilingStatus.Size = new System.Drawing.Size(1014, 571);
            this.tbpFilingStatus.TabIndex = 3;
            this.tbpFilingStatus.Text = "tabPage1";
            this.tbpFilingStatus.UseVisualStyleBackColor = true;
            // 
            // grpFilingStatus
            // 
            this.grpFilingStatus.Controls.Add(this.cmbFinancialYear);
            this.grpFilingStatus.Controls.Add(this.label5);
            this.grpFilingStatus.Controls.Add(this.panel2);
            this.grpFilingStatus.Controls.Add(this.label1);
            this.grpFilingStatus.Controls.Add(this.label2);
            this.grpFilingStatus.Controls.Add(this.lblReturnsStatusFooterMessage);
            this.grpFilingStatus.Controls.Add(this.panel1);
            this.grpFilingStatus.Controls.Add(this.dgvFilingStatus);
            this.grpFilingStatus.Controls.Add(this.txtSearchFilingStatus);
            this.grpFilingStatus.Location = new System.Drawing.Point(2, 0);
            this.grpFilingStatus.Name = "grpFilingStatus";
            this.grpFilingStatus.Size = new System.Drawing.Size(1010, 571);
            this.grpFilingStatus.TabIndex = 2;
            this.grpFilingStatus.TabStop = false;
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.BackColor = System.Drawing.Color.White;
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.ForeColor = System.Drawing.Color.Black;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(148, 19);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(135, 24);
            this.cmbFinancialYear.TabIndex = 208;
            this.cmbFinancialYear.SelectedIndexChanged += new System.EventHandler(this.cmbFinancialYear_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(12, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "Select Tax Year";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(6, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 1);
            this.panel2.TabIndex = 207;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(747, 533);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 17);
            this.label1.TabIndex = 206;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 512);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(484, 50);
            this.label2.TabIndex = 205;
            this.label2.Text = "GREEN  - Filed\r\nBLANK   - Not Done\r\nGRAY     - Under Process";
            // 
            // lblReturnsStatusFooterMessage
            // 
            this.lblReturnsStatusFooterMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnsStatusFooterMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblReturnsStatusFooterMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnsStatusFooterMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblReturnsStatusFooterMessage.Location = new System.Drawing.Point(813, 512);
            this.lblReturnsStatusFooterMessage.Name = "lblReturnsStatusFooterMessage";
            this.lblReturnsStatusFooterMessage.Size = new System.Drawing.Size(179, 17);
            this.lblReturnsStatusFooterMessage.TabIndex = 203;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 1);
            this.panel1.TabIndex = 200;
            // 
            // dgvFilingStatus
            // 
            this.dgvFilingStatus.AllowUserToAddRows = false;
            this.dgvFilingStatus.AllowUserToDeleteRows = false;
            this.dgvFilingStatus.AllowUserToOrderColumns = true;
            this.dgvFilingStatus.AllowUserToResizeRows = false;
            this.dgvFilingStatus.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvFilingStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilingStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvFilingStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilingStatus.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFilingStatus.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFilingStatus.Location = new System.Drawing.Point(13, 81);
            this.dgvFilingStatus.MultiSelect = false;
            this.dgvFilingStatus.Name = "dgvFilingStatus";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilingStatus.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvFilingStatus.RowHeadersWidth = 20;
            this.dgvFilingStatus.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFilingStatus.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFilingStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilingStatus.Size = new System.Drawing.Size(979, 428);
            this.dgvFilingStatus.TabIndex = 188;
            this.dgvFilingStatus.TabStop = false;
            this.dgvFilingStatus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFilingStatus_CellFormatting);
            // 
            // txtSearchFilingStatus
            // 
            this.txtSearchFilingStatus.BackColor = System.Drawing.Color.Azure;
            this.txtSearchFilingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchFilingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchFilingStatus.ForeColor = System.Drawing.Color.SlateGray;
            this.txtSearchFilingStatus.Location = new System.Drawing.Point(13, 47);
            this.txtSearchFilingStatus.Name = "txtSearchFilingStatus";
            this.txtSearchFilingStatus.Size = new System.Drawing.Size(979, 21);
            this.txtSearchFilingStatus.TabIndex = 190;
            this.txtSearchFilingStatus.Text = "-- Search here (min 3 chars) --";
            this.txtSearchFilingStatus.TextChanged += new System.EventHandler(this.txtSearchFilingStatus_TextChanged);
            this.txtSearchFilingStatus.Enter += new System.EventHandler(this.txtSearchFilingStatus_Enter);
            this.txtSearchFilingStatus.Leave += new System.EventHandler(this.txtSearchFilingStatus_Leave);
            // 
            // ctMenuReturnsUnderProcess
            // 
            this.ctMenuReturnsUnderProcess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLastWorkedOnUP,
            this.mnuLatestReturnUP,
            this.mnuFilingDueDateUP});
            this.ctMenuReturnsUnderProcess.Name = "ctMenuReturnsUnderProcess";
            this.ctMenuReturnsUnderProcess.Size = new System.Drawing.Size(159, 70);
            // 
            // mnuLastWorkedOnUP
            // 
            this.mnuLastWorkedOnUP.Name = "mnuLastWorkedOnUP";
            this.mnuLastWorkedOnUP.Size = new System.Drawing.Size(158, 22);
            this.mnuLastWorkedOnUP.Text = "Last Worked On";
            this.mnuLastWorkedOnUP.Click += new System.EventHandler(this.mnuLastWorkedOnUP_Click);
            // 
            // mnuLatestReturnUP
            // 
            this.mnuLatestReturnUP.Name = "mnuLatestReturnUP";
            this.mnuLatestReturnUP.Size = new System.Drawing.Size(158, 22);
            this.mnuLatestReturnUP.Text = "Latest Return";
            this.mnuLatestReturnUP.Click += new System.EventHandler(this.mnuLatestReturnUP_Click);
            // 
            // mnuFilingDueDateUP
            // 
            this.mnuFilingDueDateUP.Name = "mnuFilingDueDateUP";
            this.mnuFilingDueDateUP.Size = new System.Drawing.Size(158, 22);
            this.mnuFilingDueDateUP.Text = "Filing Due Date";
            this.mnuFilingDueDateUP.Click += new System.EventHandler(this.mnuFilingDueDateUP_Click);
            // 
            // ctMenuReturnReadyForFiling
            // 
            this.ctMenuReturnReadyForFiling.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLatestGeneratedRF,
            this.mnuLatestReturnRF});
            this.ctMenuReturnReadyForFiling.Name = "contextMenuStrip2";
            this.ctMenuReturnReadyForFiling.Size = new System.Drawing.Size(163, 48);
            // 
            // mnuLatestGeneratedRF
            // 
            this.mnuLatestGeneratedRF.Name = "mnuLatestGeneratedRF";
            this.mnuLatestGeneratedRF.Size = new System.Drawing.Size(162, 22);
            this.mnuLatestGeneratedRF.Text = "Latest Generated";
            // 
            // mnuLatestReturnRF
            // 
            this.mnuLatestReturnRF.Name = "mnuLatestReturnRF";
            this.mnuLatestReturnRF.Size = new System.Drawing.Size(162, 22);
            this.mnuLatestReturnRF.Text = "Latest Return";
            // 
            // ctMenuFiledReturns
            // 
            this.ctMenuFiledReturns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDateOfFiling});
            this.ctMenuFiledReturns.Name = "ctMenuFiledReturns";
            this.ctMenuFiledReturns.Size = new System.Drawing.Size(145, 26);
            // 
            // mnuDateOfFiling
            // 
            this.mnuDateOfFiling.Name = "mnuDateOfFiling";
            this.mnuDateOfFiling.Size = new System.Drawing.Size(144, 22);
            this.mnuDateOfFiling.Text = "Date of Filing";
            // 
            // bgBackGroundWorker
            // 
            this.bgBackGroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgBackGroundWorker_DoWork);
            this.bgBackGroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgBackGroundWorker_RunWorkerCompleted);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(984, 635);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 209;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnTabbedDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1030, 672);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TrnTabbedDashboard";
            this.Activated += new System.EventHandler(this.TrnTabbedDashboard_Activated);
            this.Load += new System.EventHandler(this.TrnTabbedDashboard_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.tbcTabControl.ResumeLayout(false);
            this.tbpReturnUnderProcess.ResumeLayout(false);
            this.grpRetursUnderProcess.ResumeLayout(false);
            this.grpRetursUnderProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnsUnderProcessFirstScreen)).EndInit();
            this.tbpReturnsReadyForFiling.ResumeLayout(false);
            this.grpReturnsReadyForFiling.ResumeLayout(false);
            this.grpReturnsReadyForFiling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnsReadyForFiling)).EndInit();
            this.tbpFiledReturns.ResumeLayout(false);
            this.grpFiledReturns.ResumeLayout(false);
            this.grpFiledReturns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiledReturns)).EndInit();
            this.tbpFilingStatus.ResumeLayout(false);
            this.grpFilingStatus.ResumeLayout(false);
            this.grpFilingStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilingStatus)).EndInit();
            this.ctMenuReturnsUnderProcess.ResumeLayout(false);
            this.ctMenuReturnReadyForFiling.ResumeLayout(false);
            this.ctMenuFiledReturns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tbcTabControl;
        private System.Windows.Forms.TabPage tbpReturnUnderProcess;
        private System.Windows.Forms.TabPage tbpReturnsReadyForFiling;
        private System.Windows.Forms.TabPage tbpFiledReturns;
        private System.Windows.Forms.GroupBox grpRetursUnderProcess;
        private DGVControl.DGVControl dgvReturnsUnderProcessFirstScreen;
        private System.Windows.Forms.TextBox txtSearchHereReturnsUnderProcess;
        private System.Windows.Forms.GroupBox grpReturnsReadyForFiling;
        private System.Windows.Forms.TextBox txtSearchReturnsReadyForFiling;
        private DGVControl.DGVControl dgvReturnsReadyForFiling;
        private System.Windows.Forms.GroupBox grpFiledReturns;
        private System.Windows.Forms.TextBox txtSearchFiledReturns;
        private DGVControl.DGVControl dgvFiledReturns;
        private System.Windows.Forms.CheckBox chkMakeDashboardDefaultHomeScreen;
        private System.Windows.Forms.Label lblDashboardCaption;
        private System.Windows.Forms.Panel pnlTopLine;
        private System.Windows.Forms.Panel pnlUP;
        private System.Windows.Forms.ContextMenuStrip ctMenuReturnsUnderProcess;
        private System.Windows.Forms.ContextMenuStrip ctMenuReturnReadyForFiling;
        private System.Windows.Forms.ContextMenuStrip ctMenuFiledReturns;
        private System.Windows.Forms.ToolStripMenuItem mnuLastWorkedOnUP;
        private System.Windows.Forms.ToolStripMenuItem mnuLatestReturnUP;
        private System.Windows.Forms.ToolStripMenuItem mnuFilingDueDateUP;
        private System.Windows.Forms.ToolStripMenuItem mnuLatestGeneratedRF;
        private System.Windows.Forms.ToolStripMenuItem mnuLatestReturnRF;
        private System.Windows.Forms.ToolStripMenuItem mnuDateOfFiling;
        private System.Windows.Forms.Label txtSortOnReturnsUnderProcess;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblFiledReturns;
        private System.Windows.Forms.Label lblReturnsUnderProcess;
        private System.Windows.Forms.Label lblReturnsReadyForFiling;
        private System.Windows.Forms.Label lblTotalCountUP;
        private System.Windows.Forms.ToolTip tltpHomeScreen;
        private System.Windows.Forms.Panel pnlRF;
        private System.Windows.Forms.Panel pnlFR;
        private System.Windows.Forms.Label lblTotalCountRF;
        private System.Windows.Forms.Label lblTotalCountFR;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReturnsAccessedFooterMessage;
        private System.Windows.Forms.Label lblReturnsGeneratedFooterMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblReturnsFiledFooterMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tbpFilingStatus;
        private System.Windows.Forms.Label lblFilingStatus;
        private System.Windows.Forms.GroupBox grpFilingStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReturnsStatusFooterMessage;
        private System.Windows.Forms.Panel panel1;
        private DGVControl.DGVControl dgvFilingStatus;
        private System.Windows.Forms.TextBox txtSearchFilingStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lnkExport;
        private System.ComponentModel.BackgroundWorker bgBackGroundWorker;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}