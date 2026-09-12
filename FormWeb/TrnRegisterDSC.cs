
#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnRegisterDSC : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnRegisterDSC()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        Dictionary<string, string> objNameval = new Dictionary<string, string>();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        enum enmState
        {
            BasicInfo,
            OTP,
            TANDetails
        }


        #endregion


        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            //txtName_TAN.Text = "";

            Clearcontrols();
        }
        #endregion



        #region TrnPanVarification_Load
        private void TrnPanVarification_Load(object sender, EventArgs e)
        {
            try
            {
                //txtPAN.Text = "BROPK6848J";
                //lblTitle.Text = "Know Your TAN"; 
                lblTitle.Text = "Register Digital Signature Certificate (DSC)";


                //--
                Clearcontrols();
            }
            catch (Exception err)
            {
                //grpDetails.Visible = false;
                //
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion
        


        #region Clearcontrols
        private void Clearcontrols()
        {  
            //grpDetails.Visible = false;

        }
        #endregion        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        

        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            //this.pgTimer.Interval = 1000;
            //this.pBar.Step = 5;

            // Update progress bar
            if ((pBar.Value + pBar.Step) > pBar.Maximum)
            {
                pBar.Value = pBar.Minimum;
            }
            else
            {
                pBar.Value += pBar.Step;
            }
        }



        #endregion

        private void lnkIncomeTax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            System.Diagnostics.Process.Start("https://eportal.incometax.gov.in/iec/foservices/#/login");
        }
    }
}

