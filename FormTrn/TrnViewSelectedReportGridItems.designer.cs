namespace TDSMAN.FormTrn
{
    partial class TrnViewSelectedReportGridItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnViewSelectedReportGridItems));
            this.pnlViewPreviouslyPrintedCheques = new System.Windows.Forms.Panel();
            this.lblAcDesp = new System.Windows.Forms.Label();
            this.flxgrdPrntdChq = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCloseViewLoadedRecords = new System.Windows.Forms.Button();
            this.pnlViewPreviouslyPrintedCheques.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdPrntdChq)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlViewPreviouslyPrintedCheques
            // 
            this.pnlViewPreviouslyPrintedCheques.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlViewPreviouslyPrintedCheques.Controls.Add(this.lblAcDesp);
            this.pnlViewPreviouslyPrintedCheques.Controls.Add(this.flxgrdPrntdChq);
            this.pnlViewPreviouslyPrintedCheques.Controls.Add(this.panel4);
            this.pnlViewPreviouslyPrintedCheques.Controls.Add(this.btnCloseViewLoadedRecords);
            this.pnlViewPreviouslyPrintedCheques.Location = new System.Drawing.Point(-2, -1);
            this.pnlViewPreviouslyPrintedCheques.Name = "pnlViewPreviouslyPrintedCheques";
            this.pnlViewPreviouslyPrintedCheques.Size = new System.Drawing.Size(796, 476);
            this.pnlViewPreviouslyPrintedCheques.TabIndex = 226;
            // 
            // lblAcDesp
            // 
            this.lblAcDesp.BackColor = System.Drawing.Color.LightCyan;
            this.lblAcDesp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAcDesp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcDesp.ForeColor = System.Drawing.Color.Black;
            this.lblAcDesp.Location = new System.Drawing.Point(4, 1);
            this.lblAcDesp.Name = "lblAcDesp";
            this.lblAcDesp.Size = new System.Drawing.Size(784, 25);
            this.lblAcDesp.TabIndex = 235;
            this.lblAcDesp.Text = "Selected Record(s)";
            this.lblAcDesp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flxgrdPrntdChq
            // 
            this.flxgrdPrntdChq.DataSource = null;
            this.flxgrdPrntdChq.Location = new System.Drawing.Point(4, 27);
            this.flxgrdPrntdChq.Name = "flxgrdPrntdChq";
            this.flxgrdPrntdChq.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flxgrdPrntdChq.OcxState")));
            this.flxgrdPrntdChq.Size = new System.Drawing.Size(784, 413);
            this.flxgrdPrntdChq.TabIndex = 234;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(-1, 441);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(795, 1);
            this.panel4.TabIndex = 231;
            // 
            // btnCloseViewLoadedRecords
            // 
            this.btnCloseViewLoadedRecords.AutoSize = true;
            this.btnCloseViewLoadedRecords.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCloseViewLoadedRecords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseViewLoadedRecords.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseViewLoadedRecords.ForeColor = System.Drawing.Color.Black;
            this.btnCloseViewLoadedRecords.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCloseViewLoadedRecords.Location = new System.Drawing.Point(739, 443);
            this.btnCloseViewLoadedRecords.Name = "btnCloseViewLoadedRecords";
            this.btnCloseViewLoadedRecords.Size = new System.Drawing.Size(50, 25);
            this.btnCloseViewLoadedRecords.TabIndex = 191;
            this.btnCloseViewLoadedRecords.Text = "Close";
            this.btnCloseViewLoadedRecords.UseVisualStyleBackColor = false;
            this.btnCloseViewLoadedRecords.Click += new System.EventHandler(this.btnCloseViewLoadedRecords_Click);
            // 
            // TrnViewSelectedReportGridItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(797, 479);
            this.Controls.Add(this.pnlViewPreviouslyPrintedCheques);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrnViewSelectedReportGridItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.TrnViewPreviousCheques_Load);
            this.pnlViewPreviouslyPrintedCheques.ResumeLayout(false);
            this.pnlViewPreviouslyPrintedCheques.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdPrntdChq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewPreviouslyPrintedCheques;
        private System.Windows.Forms.Label lblAcDesp;
        private AxMSHierarchicalFlexGridLib.AxMSHFlexGrid flxgrdPrntdChq;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnCloseViewLoadedRecords;

    }
}