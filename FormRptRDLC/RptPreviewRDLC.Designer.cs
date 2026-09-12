
namespace TDSMAN.FormRptRDLC
{
    partial class RptPreviewRDLC
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.AutoScroll = true;
            this.reportViewer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.reportViewer1.IsDocumentMapWidthFixed = true;
            this.reportViewer1.Location = new System.Drawing.Point(221, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(608, 642);
            this.reportViewer1.TabIndex = 2;
            this.reportViewer1.PageNavigation += new Microsoft.Reporting.WinForms.PageNavigationEventHandler(this.reportViewer1_PageNavigation);
            // 
            // RptPreviewRDLC
            // 
            this.ClientSize = new System.Drawing.Size(1071, 646);
            this.Controls.Add(this.reportViewer1);
            this.Name = "RptPreviewRDLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.RptPreviewRDLC_Load);
            this.Resize += new System.EventHandler(this.RptPreviewRDLC_Resize);
            this.ResumeLayout(false);

        }

        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        #endregion

        //public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}