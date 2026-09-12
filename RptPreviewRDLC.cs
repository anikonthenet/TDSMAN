using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

//using Microsoft.Reporting.WinForms;
//using Microsoft.ReportingServices;

namespace TDSMAN.FormRptRDLC
{
    public partial class RptPreviewRDLC : Form
    {
        bool scroll = true;
        public RptPreviewRDLC()
        {
            InitializeComponent();
        }

        private void RptDialogRDLC_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void RptPreviewRDLC_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.WindowState = FormWindowState.Maximized;
        }

        private void RptPreviewRDLC_Resize(object sender, EventArgs e)
        {
            //ReportPageSettings rps = reportViewer1.LocalReport.GetDefaultPageSettings();
            //if (reportViewer1.ParentForm.Width > rps.PaperSize.Width)
            //{
            //    int hPad = (reportViewer1.ParentForm.Width - rps.PaperSize.Width) / 2;
            //    reportViewer1.Padding = new Padding(hPad, 1, hPad, 1);
            //}
        }

        //private void reportViewer1_PageNavigation(object sender, PageNavigationEventArgs e)
        //{
        //    if (!scroll)
        //        e.Cancel = true;

        //    scroll = !scroll;
        //}

        private void reportViewer1_PageNavigation(object sender, PageNavigationEventArgs e)=> e.Cancel = Environment.StackTrace.Contains("DefWndProc");
        
    }
}
