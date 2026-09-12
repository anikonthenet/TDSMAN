namespace TDSMAN.FormTrn
{
    partial class TrnPredictDefaultsPopup
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnXit = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.grpGridHolder = new System.Windows.Forms.GroupBox();
            this.dgvShowGrid = new DGVControl.DGVControl();
            this.grpSearchParty = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblHeaderText = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.grpGridHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowGrid)).BeginInit();
            this.grpSearchParty.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnXit);
            this.groupBox3.Controls.Add(this.btnPrintGrid);
            this.groupBox3.Location = new System.Drawing.Point(745, 445);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 40);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnXit
            // 
            this.btnXit.BackColor = System.Drawing.Color.Lavender;
            this.btnXit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXit.ForeColor = System.Drawing.Color.Black;
            this.btnXit.Location = new System.Drawing.Point(112, 11);
            this.btnXit.Name = "btnXit";
            this.btnXit.Size = new System.Drawing.Size(77, 23);
            this.btnXit.TabIndex = 190;
            this.btnXit.Text = "E&xit";
            this.btnXit.UseVisualStyleBackColor = false;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.BackColor = System.Drawing.Color.Lavender;
            this.btnPrintGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.ForeColor = System.Drawing.Color.Black;
            this.btnPrintGrid.Location = new System.Drawing.Point(34, 11);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(77, 23);
            this.btnPrintGrid.TabIndex = 189;
            this.btnPrintGrid.Text = "&Print";
            this.btnPrintGrid.UseVisualStyleBackColor = false;
            // 
            // grpGridHolder
            // 
            this.grpGridHolder.Controls.Add(this.dgvShowGrid);
            this.grpGridHolder.Location = new System.Drawing.Point(12, 65);
            this.grpGridHolder.Name = "grpGridHolder";
            this.grpGridHolder.Size = new System.Drawing.Size(956, 380);
            this.grpGridHolder.TabIndex = 190;
            this.grpGridHolder.TabStop = false;
            // 
            // dgvShowGrid
            // 
            this.dgvShowGrid.AllowUserToAddRows = false;
            this.dgvShowGrid.AllowUserToDeleteRows = false;
            this.dgvShowGrid.AllowUserToOrderColumns = true;
            this.dgvShowGrid.AllowUserToResizeRows = false;
            this.dgvShowGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvShowGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvShowGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvShowGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dgvShowGrid.Location = new System.Drawing.Point(6, 19);
            this.dgvShowGrid.MultiSelect = false;
            this.dgvShowGrid.Name = "dgvShowGrid";
            this.dgvShowGrid.RowHeadersWidth = 20;
            this.dgvShowGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowGrid.Size = new System.Drawing.Size(944, 325);
            this.dgvShowGrid.TabIndex = 190;
            this.dgvShowGrid.TabStop = false;
            // 
            // grpSearchParty
            // 
            this.grpSearchParty.Controls.Add(this.txtSearch);
            this.grpSearchParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSearchParty.ForeColor = System.Drawing.Color.Black;
            this.grpSearchParty.Location = new System.Drawing.Point(115, 26);
            this.grpSearchParty.Name = "grpSearchParty";
            this.grpSearchParty.Size = new System.Drawing.Size(741, 39);
            this.grpSearchParty.TabIndex = 191;
            this.grpSearchParty.TabStop = false;
            this.grpSearchParty.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Location = new System.Drawing.Point(48, 12);
            this.txtSearch.MaxLength = 75;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(687, 20);
            this.txtSearch.TabIndex = 20;
            // 
            // lblHeaderText
            // 
            this.lblHeaderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderText.Location = new System.Drawing.Point(135, 4);
            this.lblHeaderText.Name = "lblHeaderText";
            this.lblHeaderText.Size = new System.Drawing.Size(711, 23);
            this.lblHeaderText.TabIndex = 192;
            this.lblHeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrnPredictDefaultsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 489);
            this.Controls.Add(this.lblHeaderText);
            this.Controls.Add(this.grpSearchParty);
            this.Controls.Add(this.grpGridHolder);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnPredictDefaultsPopup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox3.ResumeLayout(false);
            this.grpGridHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowGrid)).EndInit();
            this.grpSearchParty.ResumeLayout(false);
            this.grpSearchParty.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnXit;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.GroupBox grpGridHolder;
        private DGVControl.DGVControl dgvShowGrid;
        private System.Windows.Forms.GroupBox grpSearchParty;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblHeaderText;
    }
}