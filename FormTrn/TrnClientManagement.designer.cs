namespace TDSMAN.FormTrn
{
    partial class TrnClientManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnClientManagement));
            this.dsClientsView = new System.Windows.Forms.DataGridView();
            this.lblNoClients = new System.Windows.Forms.Label();
            this.btnRefreshGrid = new System.Windows.Forms.Button();
            this.pctUserManual = new System.Windows.Forms.PictureBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClientsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(469, 472);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Size = new System.Drawing.Size(0, 23);
            // 
            // BtnSave
            // 
            this.BtnSave.Size = new System.Drawing.Size(0, 23);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(109, 464);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Size = new System.Drawing.Size(0, 23);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Size = new System.Drawing.Size(0, 23);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(460, 15);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Size = new System.Drawing.Size(0, 23);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Size = new System.Drawing.Size(0, 23);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.pctUserManual);
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
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnRefreshGrid);
            this.pnlControls.Controls.Add(this.lblNoClients);
            this.pnlControls.Controls.Add(this.dsClientsView);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Size = new System.Drawing.Size(1003, 0);
            // 
            // dsClientsView
            // 
            this.dsClientsView.AllowUserToAddRows = false;
            this.dsClientsView.AllowUserToDeleteRows = false;
            this.dsClientsView.AllowUserToOrderColumns = true;
            this.dsClientsView.AllowUserToResizeRows = false;
            this.dsClientsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dsClientsView.Location = new System.Drawing.Point(173, 124);
            this.dsClientsView.Name = "dsClientsView";
            this.dsClientsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dsClientsView.Size = new System.Drawing.Size(656, 300);
            this.dsClientsView.TabIndex = 1;
            this.dsClientsView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dsClientsView_CellClick);
            // 
            // lblNoClients
            // 
            this.lblNoClients.AutoSize = true;
            this.lblNoClients.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoClients.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblNoClients.Location = new System.Drawing.Point(350, 264);
            this.lblNoClients.Name = "lblNoClients";
            this.lblNoClients.Size = new System.Drawing.Size(302, 20);
            this.lblNoClients.TabIndex = 2;
            this.lblNoClients.Text = "No Clients has been registered under this Server";
            this.lblNoClients.Visible = false;
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.BackColor = System.Drawing.Color.LightCyan;
            this.btnRefreshGrid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshGrid.BackgroundImage")));
            this.btnRefreshGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRefreshGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshGrid.Location = new System.Drawing.Point(806, 425);
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(24, 24);
            this.btnRefreshGrid.TabIndex = 3;
            this.btnRefreshGrid.UseVisualStyleBackColor = false;
            this.btnRefreshGrid.Visible = false;
            this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
            this.btnRefreshGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnRefreshGrid_MouseMove);
            // 
            // pctUserManual
            // 
            this.pctUserManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctUserManual.Image = ((System.Drawing.Image)(resources.GetObject("pctUserManual.Image")));
            this.pctUserManual.Location = new System.Drawing.Point(957, 10);
            this.pctUserManual.Name = "pctUserManual";
            this.pctUserManual.Size = new System.Drawing.Size(39, 32);
            this.pctUserManual.TabIndex = 235;
            this.pctUserManual.TabStop = false;
            this.pctUserManual.Tag = "User Manual";
            this.pctUserManual.Click += new System.EventHandler(this.pctUserManual_Click);
            // 
            // TrnClientManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1026, 672);
            this.Name = "TrnClientManagement";
            this.Activated += new System.EventHandler(this.TrnClientManagement_Activated);
            this.Load += new System.EventHandler(this.TrnClientManagement_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClientsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUserManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dsClientsView;
        private System.Windows.Forms.Label lblNoClients;
        private System.Windows.Forms.Button btnRefreshGrid;
        private System.Windows.Forms.PictureBox pctUserManual;
    }
}
