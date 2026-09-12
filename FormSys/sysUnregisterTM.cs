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
using System.Collections.Generic;
using System.Threading;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;

using System.Xml;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormSys
{
    public partial class sysUnregisterTM : Form
    {

        #region System Generated Code
        public sysUnregisterTM()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        TDSMAN_WEB.Registration Registration = new TDSMAN_WEB.Registration();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnExit = true;
        //
        int intID = 0;
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0, intVerifiedName = 0, intClicked = 0;
        //;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int intLoadGridPAN = 500;
        long lngSelectedGrid = 0;
        //
        int j = 0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        ToolTip tllTipVideoDemo = new ToolTip();

        ToolTip tllTipManual = new ToolTip();


        #endregion

        #region btnUnregister_Click
        private void btnUnregister_Click(object sender, EventArgs e)
        {
            if(cmnService.J_UserMessage("Do you wish to continue with 'Unregister' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,  MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (cmnService.J_UserMessage("You have opted to 'Unregister' TDSMAN. This will stop use of the software on this system till you register again.\n\nPlease re-confirm, if you wish to go ahead.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (TDSMAN.Classes.TDSMAN.T_ClientServerMachine != T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
                    {
                        //
                        string strSerialNo = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                                       T_RegistrationInfo.Serial_No.ToString());
                        // CALL WEB FUNCTION
                        string strOutValue = "";
                        if (TdsMan.T_CheckInternetConnectivty() == true && strSerialNo!= "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            //Registration.Save_User_Detail_Record(strSerialNo, "", TDSMAN.Classes.TDSMAN.T_pEditionType, TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(), 3, out strOutValue);
                            string HardDiskSerialNo = TdsMan.GetHardDiskSerialNo();
                            Registration.Save_User_Header_Detail_Record(strSerialNo,
                                                                        "",
                                                                        TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString(),
                                                                        TDSMAN.Classes.TDSMAN.T_pEditionType,
                                                                        3,
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        "",
                                                                        HardDiskSerialNo,
                                                                        "",
                                                                        out strOutValue);
                            //
                            Thread.Sleep(4000);
                            //
                            cmnService.J_DeleteRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder());
                            //
                            this.Cursor = Cursors.Default;
                            //
                            this.Close();
                            Application.Exit();
                        }
                    }
                }
            }
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
