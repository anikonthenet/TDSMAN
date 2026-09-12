using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TDSMAN.FormTrn
{
    public partial class TrnTrialMessageBox : Form
    {
        public TrnTrialMessageBox()
        {
            InitializeComponent();
        }

        #region lnkOrderNow_LinkClicked
        private void lnkOrderNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://pdsinfotech.com/eStore/tdsman/OrderNow.aspx");
            System.Diagnostics.Process.Start("https://www.tdsman.com/pricing.asp");            
        }
        #endregion

        #region btnOK_Click
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion
    }
}