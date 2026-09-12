using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TDSMAN.Classes;
namespace TDSMAN.FormSys
{
    public partial class SysServerDisconnected : Form
    {
        #region SysServerDisconnected
        public SysServerDisconnected()
        {
            InitializeComponent();
        }
        #endregion

        #region SysServerDisconnected_Load
        private void SysServerDisconnected_Load(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_ServerDisconnectedMessage = false; 
            //
            lblHeaderMessage.Text = "This client can't connect to the Server for one of these reasons :";
            lblBodyMessage.Text = "1) Remote access to the Server is not enabled.\n2) The Server computer is turned off.\n3) The Server computer is not available on the network.";
            lblFooterMessage.Text = "Make sure the Server is turned on and connected to the network, and that remote access is enabled.";
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_ServerDisconnectedMessage = true; 
            GC.Collect();
            //
            this.Close();
            this.Dispose();
        }
        #endregion

    }
}