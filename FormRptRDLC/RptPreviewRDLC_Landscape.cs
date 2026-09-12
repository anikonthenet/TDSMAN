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

using TDSMAN.Classes;

namespace TDSMAN.FormRptRDLC
{
    public partial class RptPreviewRDLC_Landscape : Form
    {
        bool scroll = true;
        ResizeForm _form_resize;
        public RptPreviewRDLC_Landscape()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }

        //private void RptDialogRDLC_Load(object sender, EventArgs e)
        //{

        //    //this.reportViewer1.RefreshReport();
        //    //this.reportViewer1.RefreshReport();
        //    //this.reportViewer1.RefreshReport();
        //    //reportViewer1.ShowPageNavigationControls = false;
        //}

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            //if (blResize == true)
                _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region RptPreviewRDLC_Load
        private void RptPreviewRDLC_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            //this.reportViewer1.RefreshReport();
            //this.WindowState = FormWindowState.Maximized;
            //this.reportViewer1.ShowPageNavigationControls = false;
            //this.reportViewer1.DisplayMode = DisplayMode.PrintLayout;
        }
        #endregion

        #region RptPreviewRDLC_Resize
        private void RptPreviewRDLC_Resize(object sender, EventArgs e)
        {
            //ReportPageSettings rps = reportViewer1.LocalReport.GetDefaultPageSettings();
            //if (reportViewer1.ParentForm.Width > rps.PaperSize.Width)
            //{
            //    int hPad = (reportViewer1.ParentForm.Width - rps.PaperSize.Width) / 2;
            //    reportViewer1.Padding = new Padding(hPad, 1, hPad, 1);
            //}
            //this.reportViewer1.ZoomMode = ZoomMode.FullPage;
            //this.reportViewer1.ZoomPercent = 100;
            //this.reportViewer1.Width = 210;
            //this.reportViewer1.Height = 297;
        }
        #endregion

        //private void reportViewer1_PageNavigation(object sender, PageNavigationEventArgs e)
        //{
        //    if (!scroll)
        //        e.Cancel = true;

        //    scroll = !scroll;
        //}

        private void reportViewer1_PageNavigation(object sender, PageNavigationEventArgs e) => e.Cancel = Environment.StackTrace.Contains("DefWndProc");

        private void ReportViewer1_Print(object sender, ReportPrintEventArgs e)
        {

        }

        //private void reportViewer1_PageNavigation(object sender, PageNavigationEventArgs e)
        //{
        //    //if (!scroll)
        //    //    e.Cancel = true;

        //    //scroll = !scroll;
        //}
    }
}
