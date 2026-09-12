

#region Importing Namespace
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    using TDSMAN.Classes;
#endregion


namespace TDSMAN.FormRpt
{
    public partial class RptPreview : Form
    {
        ResizeForm _form_resize;


        public RptPreview()
        {
            InitializeComponent();
            //--
            //_form_resize = new ResizeForm(this);
            //this.Load += _Load;
            //this.Resize += _Resize;
            //--
        }

        #region RESIZING WINDOW
        //private void _Load(object sender, EventArgs e)
        //{
        //    _form_resize._get_initial_size();
        //}

        //private void _Resize(object sender, EventArgs e)
        //{
        //    _form_resize._resize();
        //    //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        //}
        #endregion

        private void RptPreview_Load(object sender, EventArgs e)
        {
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //
            try
            {
                //this.WindowState = FormWindowState.Maximized;
                CRViewer.ShowLogo = false;

                CRViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception err)
            {

            }
        }
    }
}