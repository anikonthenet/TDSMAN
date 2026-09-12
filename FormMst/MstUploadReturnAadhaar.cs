using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDSMAN.FormMst
{
    public partial class MstUploadReturnAadhaar : Form
    {
        #region System Generated Code
        public MstUploadReturnAadhaar()
        {
            InitializeComponent();
        }
        #endregion

        #region btnProceed_Click
        //private void btnProceed_Click(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start("https://incometaxindiaefiling.gov.in/e-Filing/UserLogin/LoginHome.html?lang=eng");
        //    //
        //    this.Close();
        //}
        #endregion

        #region lnkITR_LinkClicked
        private void lnkITR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://incometaxindiaefiling.gov.in/e-Filing/UserLogin/LoginHome.html?lang=eng");
        }
        #endregion
        

    }
}
