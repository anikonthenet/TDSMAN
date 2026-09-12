#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: SysPreferences
Version			: 1.0
Start Date		: 08/01/2014
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysPreferences : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysPreferences()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        //RptDialog rptDialog = new RptDialog();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        string strSQLGridViewTabPages;
        long lngBasicInfoID;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngDeducteeDetailID;
        long lngDeducteeID;
        string strSQLShowHelpDeductee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";

        //Added by Shrey Kejriwal on 19/01/2011
        string strConsolidatedStatementPath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;
        bool blnSectionDDDisplay = true;
        bool blnRegularStatemnt = true;
        bool blnXit = false;
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--
        string strPassword="";
        //----

        #endregion

        #region User Defined Events

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region SysPreferences_Load
        private void SysPreferences_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //----
            strSQL = " SELECT MINOR_HEAD_ID," +
                 "            MINOR_HEAD_CODE + '-' + MINOR_HEAD_DESC AS MINOR " +
                 "     FROM  MST_MINOR_HEAD " +
                 "     ORDER BY MINOR_HEAD_ID";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbMinorCode) == false) return;
            //----
            ShowRecord();
            //
            //----
            if (TdsMan.CheckAdminUser() == false)
            {
                grpBackupReminder.Visible = false;
            }
            //---- 2020/06/04
            if (TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_LITE_EDITION && TDSMAN.Classes.TDSMAN.T_pEditionType != T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                chkShareRefernceNoF24Q.Visible = false;
                chkEnableReferenceNo.Text = "24. Enable provision to enter Deductee Reference No. for Form 26Q, 27Q & 27EQ.";
                //-- 2024/12/19
                string[] strReferenceNoFormNo = { T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ};
                dmlService.J_PopulateComboBox(strReferenceNoFormNo, ref cmbReferenceNoFormNo);                
            }
            else
            {
                chkEnableReferenceNo.Text = "24. Enable provision to enter Deductee Reference No. for Form 24Q, 26Q, 27Q & 27EQ.";
                //-- 2024/12/19
                string[] strReferenceNoFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                dmlService.J_PopulateComboBox(strReferenceNoFormNo, ref cmbReferenceNoFormNo);
            }
            //----
        }
        #endregion
        
        #region SET PASSWORD

        #region chkSetPassword_CheckedChanged
        private void chkSetPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtReenterPassword.Text = "";
            if (chkSetPassword.Checked == true)
            {
                if (strPassword == "")
                {
                    grpSetPassword.Visible = true;
                    lblOldPassword.Visible = false;
                    txtCurrentPassword.Visible = false;
                    txtNewPassword.Select();
                }
            }
            else if (chkSetPassword.Checked == false)
            {
                if (strPassword == "")
                {
                    grpSetPassword.Visible = false;
                    return;
                }
                //--
                if (cmnService.J_UserMessage("Passsword protection will be removed.\nProceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    strPassword = "";
                    btnChangePassword.Visible = false;
                    grpSetPassword.Visible = false;
                    cmnService.J_UserMessage("For the changes to take place, click on <Save>");
                    return;
                }
                else
                    chkSetPassword.Checked = true;
            }
        }
        #endregion

        #region btnChangePassword_Click
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            grpSetPassword.Visible = true;
            txtCurrentPassword.Select();
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtReenterPassword.Text = "";
        }
        #endregion

        #region btnSavePassword_Click
        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Visible == true)
            {
                if (strPassword != txtCurrentPassword.Text)
                {
                    cmnService.J_UserMessage("Current Password does not matches");
                    txtCurrentPassword.Select();
                    return;
                }
            }
            //--
            if (txtNewPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("New Password can't be blank");
                txtNewPassword.Select();
                return;
            }
            //
            if (txtReenterPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Re-enter Password can't be blank");
                txtReenterPassword.Select();
                return;
            }
            //
            if (txtNewPassword.Text != txtReenterPassword.Text)
            {
                cmnService.J_UserMessage("New Password mismatch with Re-enter Password");
                txtReenterPassword.Select();
                return;
            }
            //--
            strPassword = txtNewPassword.Text;
            //--
            //string MachineName = cmnService.J_GetRegistryKeyValue(CP.T_pCompanyName + "\\" + CP.T_pPackageName, T_RegistrationInfo.Serial_No.ToString());
            //if (CP.T_pClientServerMachine == T_CLIENT_SERVER_MACHINE.CLIENT_MACHINE)
            //    MachineName = cmnService.J_GetRegistryKeyValue(CP.T_pCompanyName + "\\" + CP.T_pPackageName, T_RegistrationInfo.Licensee_Name.ToString());
            //--
            strSQL = "UPDATE MST_SETUP SET PASSWD = '" + cmnService.J_ReplaceQuote(strPassword) + "'";
            //if (CP.T_pPackageType == T_PACKAGE_TYPE.MULTI_USER)
            //{
            //    if (CP.T_pMACHINE_ID > 0)
            //        strSQL = strSQL + " WHERE MACHINE_ID = " + CP.T_pMACHINE_ID + " ";
            //}
            //-- 2014/11/14
            if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
            {
                if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                    strSQL = strSQL + " WHERE SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
            } 
            if (dmlService.J_ExecSql(strSQL) == false) { return; }
            //
            grpSetPassword.Visible = false;
        }
        #endregion

        #region btnCancelPassword_Click
        private void btnCancelPassword_Click(object sender, EventArgs e)
        {
            grpSetPassword.Visible = false;
        }
        #endregion

        #region txtCurrentPassword_KeyPress
        private void txtCurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtNewPassword_KeyPress
        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtReenterPassword_KeyPress
        private void txtReenterPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtCurrentPassword_KeyDown
        private void txtCurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }
        #endregion

        #region txtNewPassword_KeyDown
        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }
        #endregion

        #region txtReenterPassword_KeyDown
        private void txtReenterPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }
        #endregion

        #endregion

        #region PROXY

        #region rbnConnectAutomatically_CheckedChanged
        private void rbnConnectAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnConnectAutomatically.Checked == true)
            {
                txtAddress.Text = "";
                txtPort.Text = "";
                chkProxy.Checked = false;
                txtProxyUsername.Text = "";
                txtProxyPassword.Text = "";
                //
                grpProxyDetails.Enabled = false;
                btnTestConnection.Enabled = false;
            }
            else if (rbnConnectProxy.Checked == true)
            {
                grpProxyDetails.Enabled = true;
                txtAddress.Select();
                btnTestConnection.Enabled = true;
            }
        }
        #endregion

        #region chkProxy_CheckedChanged
        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProxy.Checked == false)
            {
                grpAuthentication.Enabled = false;
                txtProxyUsername.Text = "";
                txtProxyPassword.Text = "";
            }
            else
                grpAuthentication.Enabled = true;
        }
        #endregion

        #region btnTestConnection_Click
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (txtAddress.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Host Address can not be Blank");
                    txtAddress.Select();
                    return;
                }
                //
                if (txtPort.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Port can not be Blank");
                    txtPort.Select();
                    return;
                }
                //
                if (chkProxy.Checked == true)
                {
                    //--
                    if (txtProxyUsername.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Username can not be Blank");
                        txtProxyUsername.Select();
                        return;
                    }
                    //
                    if (txtProxyPassword.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Password can not be Blank");
                        txtProxyPassword.Select();
                        return;
                    }
                }
                //--
                //WebProxy proxyObject = new WebProxy(txtAddress.Text.Trim(), cmnService.J_ReturnInt32Value(txtPort.Text.Trim()));
                WebProxy proxyObject = new WebProxy("http://" + txtAddress.Text.Trim() + "/", cmnService.J_ReturnInt32Value(txtPort.Text.Trim()));
                //
                if (txtProxyUsername.Text.Trim()!="") // (chkProxy.Checked == true)
                    proxyObject.Credentials = new NetworkCredential(txtProxyUsername.Text.Trim(), txtProxyPassword.Text.Trim());
                else
                    proxyObject.UseDefaultCredentials = true;
                //
                WebRequest req = WebRequest.Create("http://www.google.com");
                //WebRequest req = WebRequest.Create("http://www.tdsman.com");
                req.Credentials = new NetworkCredential(txtProxyUsername.Text.Trim(), txtProxyPassword.Text.Trim());
                req.Proxy.Credentials = new NetworkCredential(txtProxyUsername.Text.Trim(), txtProxyPassword.Text.Trim());
                req.Proxy = proxyObject;
                //
                //if(ConnectProxy(txtHost.Text,cmnService.J_ReturnInt32Value(txtPort.Text)) == true)
                cmnService.J_UserMessage("Connection Successful");
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("Connection Failed !! due to : " + err.Message, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            this.Close();
            this.Dispose();
            //
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            int intProxySetttings = 0;
            string strAddress = ""; string strPort = ""; string strProxyUserName = ""; string strProxyPassword = "";
            int intEnableBookmark = 0; int intShowDeductedDate = 0; int intCopyInterestAllocated = 0; int intShowNonSalaryTDSRate = 0;
            int intShowAllocatedFields = 0; int intShowCompnayWiseDeductee = 0; int intStopZeroTaxDeduction = 0; int intShowRegularDeducteeCorrection = 0;
            int intAllotAutomaticRefNo = 0; int intDeducteeEntryFirst = 0; int intEnableRoundOffTaxableAmt = 0; int intAutoCalculateTdsRate = 0;
            int intShowColumnsForm16PartB = 0; int intLockReturn = 0;
            int intEnableReferenceNo = 0; int intShareReferenceNoF24Q = 0; int intShareReferenceNoF26Q = 0; int intShareReferenceNoF27Q = 0, intShareReferenceNoF27EQ = 0;
            int intEmbedDeducteeSearch = 0; int intDeductedDateExcel = 0;
            //-- Added By Abhishek Dey On 13/04/2018 --
            int intDeducteeCode = 0;
            //-- Added By Abhishek Dey On 20/08/2018 --
            int intHidePassword = 0;
            int intBrowserLoginTRACES = 0;
            //
            int intEnableNSDLTextCapsLock = 0;
            //
            int intEnableLoadSectionAuto = 0, intSec194NExcess1CroreExcelImport = 0, intPartySearchPANWise = 0, intAutoPopulateAmountTDS = 0, intSMTPPassword=0, intFetchSection =0, intEnablePasswordITPortalsModule = 0;
            int intEnableRoundOffTaxableAmt26Q27Q27EQ = 0, intEXPORT_REPORTS_CSV = 0;
            //
            try
            {
                if (ValidateFields() == false) return;
                //-----------------------------------------------------------
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //-----------------------------------------------------------
                #region GET VALUES
                if (rbnConnectProxy.Checked == true)
                {
                    intProxySetttings =1;
                    //
                    strAddress = txtAddress.Text.Trim(); strPort = txtPort.Text.Trim();
                    if(txtProxyUsername.Text.Trim() != "")
                        strProxyUserName = txtProxyUsername.Text.Trim();
                    if (txtProxyPassword.Text.Trim() != "")
                        strProxyPassword = txtProxyPassword.Text.Trim();
                }
                //
                if (chkEnableBookmark.Checked == false)
                    intEnableBookmark = 1;
                else
                    intEnableBookmark = 0;
                //
                if (chkShowDeductedDate.Checked == true)
                    intShowDeductedDate = 1;
                //
                if (chkCopyInterestAllocated.Checked == true)
                    intCopyInterestAllocated = 1;
                //
                if (chkShowNonsalaryTDSRate.Checked == false)
                    intShowNonSalaryTDSRate = 1;
                else
                    intShowNonSalaryTDSRate = 0;
                //
                if (chkShowAllocatedFields.Checked == true)
                    intShowAllocatedFields = 1;
                //
                if (chkShowCompanyWiseDeductee.Checked == true)
                    intShowCompnayWiseDeductee = 1;
                //
                if (chkStopZeroTaxDeduction.Checked == false)
                    intStopZeroTaxDeduction = 1;
                else
                    intStopZeroTaxDeduction = 0;
                //
                if (chkShowRegularDeducteesInCorrcetion.Checked == true)
                    intShowRegularDeducteeCorrection = 1;
                else
                    intShowRegularDeducteeCorrection = 0;
                //
                if (chkAllotAutomaticRefNo.Checked == true)
                    intAllotAutomaticRefNo = 1;
                else
                    intAllotAutomaticRefNo = 0;
                //
                if (chkDeducteeEntryFirst.Checked == true)
                    intDeducteeEntryFirst = 1;
                else
                    intDeducteeEntryFirst = 0;
                //
                if (chkEnableRoundOffTaxableAmt.Checked == true)
                    intEnableRoundOffTaxableAmt = 1;
                else
                    intEnableRoundOffTaxableAmt = 0;
                //-- 2015-07-23 @@ DHRUB
                if (chkAutoCalculateTdsRate.Checked == true)
                    intAutoCalculateTdsRate = 1;
                else
                    intAutoCalculateTdsRate = 0;
                //
                //-- 2016-06-15
                if (chkShowLastTwoColumnsForm16PartB.Checked == true)
                    intShowColumnsForm16PartB = 1;
                else
                    intShowColumnsForm16PartB = 0;
                //-- 2016-09-01
                if (chkLockReturn.Checked == true)
                    intLockReturn = 1;
                else
                    intLockReturn = 0;

                //-- 2017/02/13
                if (chkEnableReferenceNo.Checked == true)
                {
                    intEnableReferenceNo = 1;
                    //-- 2017/02/20
                    //if (chkShareRefernceNoF24Q.Checked == true)
                    //    intShareReferenceNoF24Q = 1;
                    //else
                    //    intShareReferenceNoF24Q = 0;
                    //-- 2017/02/20
                    if (chkShareRefernceNoF26Q.Checked == true)
                        intShareReferenceNoF26Q = 1;
                    else
                        intShareReferenceNoF26Q = 0;
                    //-- 2017/02/20
                    if (chkShareRefernceNoF27Q.Checked == true)
                        intShareReferenceNoF27Q = 1;
                    else
                        intShareReferenceNoF27Q = 0;
                    //
                    //-- 2021/07/14
                    if (chkShareRefernceNoF27EQ.Checked == true)
                        intShareReferenceNoF27EQ = 1;
                    else
                        intShareReferenceNoF27EQ = 0;
                    //-- 2020/05/30
                    if (chkShareRefernceNoF24Q.Checked == true)
                        intShareReferenceNoF24Q = 1;
                    else
                        intShareReferenceNoF24Q = 0;
                    //
                }
                else
                {
                    intEnableReferenceNo = 0;
                }
                //-- 2017/05/24
                if (chkEmbeddedSearch.Checked == true)
                    intEmbedDeducteeSearch = 1;
                else
                    intEmbedDeducteeSearch = 0;
                //
                //-- 2017/10/16
                if (chkDeductedDate.Checked == true)
                    intDeductedDateExcel = 1;
                else
                    intDeductedDateExcel = 0;

                //-- Added By Abhishek Dey On 13/04/2018 --
                if (chkSelectDeducteeCode.Checked == true)
                    intDeducteeCode = 1;
                else
                    intDeducteeCode = 0;
                //--
                if (chkTracesUsingBrowser.Checked == true) //-- 2018/09/04
                    intBrowserLoginTRACES = 1;
                else
                    intBrowserLoginTRACES = 0;
                //-- Added By Abhishek Dey On 20/08/2018 --
                if (chkHidePassword.Checked == true)
                    intHidePassword = 1;
                else
                    intHidePassword = 0;
                //--
                if (chkEnableCSIUpperCaseEntry.Checked == true)
                    intEnableNSDLTextCapsLock = 1;
                else
                    intEnableNSDLTextCapsLock = 0;
                //--
                if (chkLoadSectionAuto.Checked == true)
                    intEnableLoadSectionAuto = 1;
                else
                    intEnableLoadSectionAuto = 0;
                //--
                if (chkCashExcess1croreSection194N.Checked == true)
                    intSec194NExcess1CroreExcelImport = 1;
                else
                    intSec194NExcess1CroreExcelImport = 0;
                //--
                if (chkPANWisePartySearch.Checked == true)
                    intPartySearchPANWise = 1;
                else
                    intPartySearchPANWise = 0;
                //--
                if (chkAutoPopulateAmountTDS.Checked == true)
                    intAutoPopulateAmountTDS = 1;
                else
                    intAutoPopulateAmountTDS = 0;
                //-- 
                if (chkHideSMTPPassword.Checked == true) //-- 2022/04/09
                    intSMTPPassword = 1;
                else
                    intSMTPPassword = 0;
                //-- 
                if (chkFetchSection.Checked == true) //-- 2022/08/13
                    intFetchSection = 1;
                else
                    intFetchSection = 0;
                //-- 2024/03/12
                if (chkEnableITPortalsPasswordModule.Checked == true)
                    intEnablePasswordITPortalsModule = 1;
                else
                    intEnablePasswordITPortalsModule = 0;
                //--2025/06/26
                if (chkEnableRoundOffTaxableAmt26Q27Q27EQ.Checked == true)
                    intEnableRoundOffTaxableAmt26Q27Q27EQ = 1;
                else
                    intEnableRoundOffTaxableAmt26Q27Q27EQ = 0;
                //-- 2025/09/04
                if (chkExportTOCSV.Checked == true)
                    intEXPORT_REPORTS_CSV = 1;
                else
                    intEXPORT_REPORTS_CSV = 0;
                //-----------------------------------------
                //--
                #endregion
                //--
                dmlService.J_BeginTransaction();
                //
                strSQL = "UPDATE MST_SETUP " +
                    "     SET    PASSWD                      ='" + strPassword + "'," +
                    "            PROXY_SETTINGS              =" + intProxySetttings + "," +
                    "            HOST_ADDRESS                ='" + strAddress + "'," +
                    "            HOST_PORT                   ='" + strPort + "'," +
                    "            HOST_USER_NAME              ='" + strProxyUserName + "'," +
                    "            HOST_PASSWORD               ='" + strProxyPassword + "'," +
                    "            SHOW_BOOKMARK               = " + intEnableBookmark + "," +
                    "            SHOW_DEDUCTED_DATE          = " + intShowDeductedDate + "," +
                    "            SHOW_NONSALARY_TDS_RATE     = " + intShowNonSalaryTDSRate + "," +
                    "            COPY_INTEREST_ALLOCATED     = " + intCopyInterestAllocated + "," +
                    "            SHOW_ALLOCATED_FIELDS       = " + intShowAllocatedFields + "," +
                    "            SHOW_DEDUCTEE_COMPANY_WISE  = " + intShowCompnayWiseDeductee + "," +
                    "            PROHIBIT_ZERO_TAX_DEDUCTION = " + intStopZeroTaxDeduction + "," +
                    "            SHOW_REGULAR_DEDUCTEE_CORRECTION = " + intShowRegularDeducteeCorrection + "," +
                    "            ALLOT_AUTOMATIC_REF_NO     = " + intAllotAutomaticRefNo + "," +
                    "            ENABLE_DEDUCTEE_FIRST      = " + intDeducteeEntryFirst + "," +
                    "            BACKUP_REMINDER_DAYS       = " + Convert.ToInt16(txtBackupReminderDays.Text) + "," +
                    "            ROUND_OFF_TAXABLE_AMOUNT   = " + intEnableRoundOffTaxableAmt + "," +
                    "            AUTO_CALCULATE_TDS_RATE    = " + intAutoCalculateTdsRate + "," +
                    "            SHOW_COLUMNS_FORM16_PARTB  = " + intShowColumnsForm16PartB + ", " +
                    "            ENABLE_LOCK_RETURN         = " + intLockReturn + ", " +
                    "            ENABLE_REFERENCE_NO        = " + intEnableReferenceNo + ", " +
                    "            SHARE_REFERENCE_NO_F24Q    = " + intShareReferenceNoF24Q + ", " +
                    "            SHARE_REFERENCE_NO_F26Q    = " + intShareReferenceNoF26Q + ", " +
                    "            SHARE_REFERENCE_NO_F27Q    = " + intShareReferenceNoF27Q + ", " +
                    "            EMBEDDED_DEDUCTEE_SEARCH   = " + intEmbedDeducteeSearch + ", " +
                    "            DEDUCTED_DATE_EXCEL        = " + intDeductedDateExcel + ", " +
                    "            ENABLE_AUTO_POPULATE_DEDUCTEE_CODE = " + intDeducteeCode+ ", " +
                    "            BROWSER_LOGIN_TRACES       = " + intBrowserLoginTRACES + ", " +
                    "            ENABLE_HIDE_PASSWORD       = " + intHidePassword + ", " +
                    "            DEFAULT_MINOR_CODE         ='" + cmnService.J_ReplaceQuote(cmbMinorCode.Text) + "', " +
                    "            LOAD_SECTION_AUTO          = " + intEnableLoadSectionAuto + ", " +
                    "            ENABLE_NSDL_TEXT_CAPS_LOCK = " + intEnableNSDLTextCapsLock + ", " +
                    "            MAX_REC_EXCEL_IMPORT_ALERT = " + cmnService.J_ReturnInt32Value(txtMaxNewExcelSheetRecords.Text) + ", " +
                    "            SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION = " + intSec194NExcess1CroreExcelImport + ", " +
                    "            PARTY_SEARCH_PAN_WISE = " + intPartySearchPANWise + ", " +
                    "            SHARE_REFERENCE_NO_F27EQ    = " + intShareReferenceNoF27EQ + ", " +
                    "            AUTO_POPULATE_PAID_AMOUNT_TAX_AMOUNT    = " + intAutoPopulateAmountTDS + "," +
                    "            HIDE_SMTP_PASSWORD          = " + intSMTPPassword + "," +
                    "            FETCH_SECTION_FROM_PAN_NAME = " + intFetchSection + "," +
                    "            ENABLE_PASSWORD_ITPORTALS_MODULE = " + intEnablePasswordITPortalsModule + "," +
                    "            ROUND_OFF_TAXABLE_AMOUNT_26Q_27Q_27EQ = " + intEnableRoundOffTaxableAmt26Q27Q27EQ + "," +
                    "            EXPORT_REPORTS_CSV = " + intEXPORT_REPORTS_CSV;
                //-- 2014/11/14 intEnableLoadSectionAuto
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                        strSQL = strSQL + " WHERE SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
                }
                //
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    BtnSave.Select();
                    dmlService.J_Rollback();
                    return;
                }
                //
                //-----------------------------------------------------------
                dmlService.J_Commit();
                //
                //-------------------------------------
                //--ADDED BY DHRUB ON 2015-10-03 
                //-------------------------------------
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    cmnService.J_SetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder() + "(Trial)",
                                                     T_RegistrationInfo.File_Creation_Path.ToString(),
                                                     txtFvuFileGenerationPath.Text);
                else
                    cmnService.J_SetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                                                     T_RegistrationInfo.File_Creation_Path.ToString(),
                                                     txtFvuFileGenerationPath.Text);
                //-------------------------------------
                TdsMan.GetSetup();
                //
                BtnExit_Click(sender, e);
            }
            catch (Exception err)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region lblHelpShowDeductedDate_MouseMove
        //private void lblHelpShowDeductedDate_MouseMove(object sender, MouseEventArgs e)
        //{
        //    tllTip.SetToolTip(lblHelpShowDeductedDate, "Show deducted date in Add mode");
        //}
        #endregion

        #region lblHelpCopyInterest_MouseMove
        //private void lblHelpCopyInterest_MouseMove(object sender, MouseEventArgs e)
        //{
        //    tllTip.SetToolTip(lblHelpCopyInterest, "Copy interest in interest allocated during Excel import");
        //}
        #endregion



        #region txtBackupReminderDays_KeyPress
        private void txtBackupReminderDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "N,2,0", txtBackupReminderDays, "") == false)
                e.Handled = true;
        }
        #endregion

        #region chkSetBackupReminder_CheckedChanged
        private void chkSetBackupReminder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetBackupReminder.Checked == true)
            {
                txtBackupReminderDays.Enabled = true;
                //txtBackupReminderDays.Text = "0";
            }
            else if (chkSetBackupReminder.Checked == false)
            {
                txtBackupReminderDays.Enabled = false;
                txtBackupReminderDays.Text = "0";
            }

        }
        #endregion

        #region btnOutputFolderChange_Click
        private void btnOutputFolderChange_Click(object sender, EventArgs e)
        {
            string strOutputFolderPath = cmnService.J_OpenFolderDialog();
            if (strOutputFolderPath == "")
            {
                cmnService.J_UserMessage("Select Folder to Save the Output File", MessageBoxIcon.Information);
                btnFvuFileGenerationPathChange.Select();
                return;
            }
            txtFvuFileGenerationPath.Text = strOutputFolderPath;
        }

        #endregion

        #region chkEnableReferenceNo_CheckedChanged
        private void chkEnableReferenceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (blnXit == true) return;
            //--
            if (chkEnableReferenceNo.Checked == true)
            {
                grpFormsRefernceNo.Enabled = true;
                //chkShareRefernceNoF24Q.Checked = true;
                //chkShareRefernceNoF26Q.Checked = true;
                //chkShareRefernceNoF27Q.Checked = true;
                //
                lblCaptionReferenceNoFormNo.Enabled = true;
                cmbReferenceNoFormNo.Enabled = true;
            }
            else if (chkEnableReferenceNo.Checked == false)
            {
                grpFormsRefernceNo.Enabled = false;
                //chkShareRefernceNoF24Q.Checked = false;
                chkShareRefernceNoF24Q.Checked = false;
                chkShareRefernceNoF26Q.Checked = false;
                chkShareRefernceNoF27Q.Checked = false;
                chkShareRefernceNoF27EQ.Checked = false;
                //
                lblCaptionReferenceNoFormNo.Enabled = false;
                cmbReferenceNoFormNo.Enabled = false;
                lblReferenceNoFormNo.Text = "";
            }
        }
        #endregion

        #region chkShareRefernceNo_CheckedChanged
        private void chkShareRefernceNo_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShareRefernceNoF24Q.Checked == false &&
               chkShareRefernceNoF26Q.Checked == false &&
               chkShareRefernceNoF27Q.Checked == false &&
               chkShareRefernceNoF27EQ.Checked == false)
            {
                chkEnableReferenceNo.Checked = false;
                chkEnableReferenceNo.Select();
            }
        }
        #endregion

        #region txtMaxNewExcelSheetRecords_KeyPress
        private void txtMaxNewExcelSheetRecords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtMaxNewExcelSheetRecords, "") == false)
                e.Handled = true;
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericControl_Leave
        private void NumericControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "") txtBox.Text = "0";
        }
        #endregion


        #region ClearControls
        private void ClearControls()
        {
            chkShowDeductedDate.Checked = false;
            chkCopyInterestAllocated.Checked = false;
            chkShowNonsalaryTDSRate.Checked = false;
            //
            chkSetPassword.Checked = false;
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtReenterPassword.Text = "";
            //
            txtAddress.Text = "";
            txtPort.Text = "";
            chkProxy.Checked = false;
            txtProxyUsername.Text = "";
            txtProxyPassword.Text = "";
            //
            chkShowCompanyWiseDeductee.Checked = false;
            chkStopZeroTaxDeduction.Checked = false;
            chkShowRegularDeducteesInCorrcetion.Checked = false;
            chkAllotAutomaticRefNo.Checked = false;
            chkShowLastTwoColumnsForm16PartB.Checked = false;
            chkLockReturn.Checked = false;
            chkEnableReferenceNo.Checked = false;
            //chkShareRefernceNoF24Q.Checked = false;
            chkShareRefernceNoF26Q.Checked = false;
            chkShareRefernceNoF27Q.Checked = false;
            chkEnableRoundOffTaxableAmt26Q27Q27EQ.Checked = false;
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                #region PROXY RELATED
                if (rbnConnectProxy.Checked == true)
                {
                    if (txtAddress.Text == "")
                    {
                        cmnService.J_UserMessage("Address cannot be Blank");
                        txtAddress.Select();
                        return false;
                    }
                    //
                    if (txtPort.Text == "")
                    {
                        cmnService.J_UserMessage("Port cannot be Blank");
                        txtPort.Select();
                        return false;
                    }
                    //
                    if (chkProxy.Checked == true)
                    {
                        if (txtProxyUsername.Text == "")
                        {
                            cmnService.J_UserMessage("Username cannot be Blank");
                            txtProxyUsername.Select();
                            return false;
                        }
                        //
                        if (txtProxyPassword.Text == "")
                        {
                            cmnService.J_UserMessage("Password cannot be Blank");
                            txtProxyPassword.Select();
                            return false;
                        }
                    }
                }
                #endregion
                //

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord()//(long Id)
        {
            IDataReader drdShowRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            try
            {
                //string[,] strShowDeducteeCodeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                //                                {"MST_DEDUCTEE.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"}};
                
                strSQL = "SELECT SetupId," +
                         "       PASSWD," +
                         "       PROXY_SETTINGS," +
                         "       HOST_ADDRESS," +
                         "       HOST_PORT," +
                         "       HOST_USER_NAME," +
                         "       HOST_PASSWORD," +
                         "       SHOW_BOOKMARK," +
                         "       SHOW_DEDUCTED_DATE," +
                         "       COPY_INTEREST_ALLOCATED, " +
                         "       SHOW_NONSALARY_TDS_RATE," +
                         "       SHOW_ALLOCATED_FIELDS," +
                         "       SHOW_DEDUCTEE_COMPANY_WISE," +
                         "       PROHIBIT_ZERO_TAX_DEDUCTION," +
                         "       SHOW_REGULAR_DEDUCTEE_CORRECTION," + 
                         "       ALLOT_AUTOMATIC_REF_NO, " +
                         "       ENABLE_DEDUCTEE_FIRST," +
                         "       BACKUP_REMINDER_DAYS," +
                         "       ROUND_OFF_TAXABLE_AMOUNT,  " +
                         "       AUTO_CALCULATE_TDS_RATE,  " +
                         "       SHOW_COLUMNS_FORM16_PARTB, " +
                         "       ENABLE_LOCK_RETURN," +
                         "       ENABLE_REFERENCE_NO," +
                         "       SHARE_REFERENCE_NO_F24Q," +
                         "       SHARE_REFERENCE_NO_F26Q," +
                         "       SHARE_REFERENCE_NO_F27Q," +
                         "       EMBEDDED_DEDUCTEE_SEARCH," +
                         "       DEDUCTED_DATE_EXCEL," +
                         "       ENABLE_AUTO_POPULATE_DEDUCTEE_CODE," +
                         "       BROWSER_LOGIN_TRACES," +
                         "       ENABLE_HIDE_PASSWORD," +
                         "       DEFAULT_MINOR_CODE," +
                         "       LOAD_SECTION_AUTO, " +
                         "       ENABLE_NSDL_TEXT_CAPS_LOCK, " +
                         "       MAX_REC_EXCEL_IMPORT_ALERT, " +
                         "       SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION," +
                         "       PARTY_SEARCH_PAN_WISE," +
                         "       SHARE_REFERENCE_NO_F27EQ," +
                         "       AUTO_POPULATE_PAID_AMOUNT_TAX_AMOUNT, " +
                         "       HIDE_SMTP_PASSWORD, " +
                         "       FETCH_SECTION_FROM_PAN_NAME, " +
                         "       ENABLE_PASSWORD_ITPORTALS_MODULE," +
                         "       ROUND_OFF_TAXABLE_AMOUNT_26Q_27Q_27EQ, " +
                         "       EXPORT_REPORTS_CSV " +
                         "FROM   MST_SETUP ";
                //-- 2014/11/14
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    if (TDSMAN.Classes.TDSMAN.T_MACHINE_ID > 0)
                        strSQL = strSQL + " WHERE SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + " ";
                }
                //--                
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                    return false;
                //--
                while (drdShowRecord.Read())
                {
                    //lngDeducteeDetailID = Id;

                    if (Convert.ToString(drdShowRecord["SHOW_BOOKMARK"]) == "0")
                        chkEnableBookmark.Checked = true;
                    else
                        chkEnableBookmark.Checked = false;
                    //
                    if (Convert.ToString(drdShowRecord["SHOW_DEDUCTED_DATE"]) == "1")
                        chkShowDeductedDate.Checked = true;
                    //
                    if (Convert.ToString(drdShowRecord["COPY_INTEREST_ALLOCATED"]) == "1")
                        chkCopyInterestAllocated.Checked = true;
                    //
                    if (Convert.ToString(drdShowRecord["SHOW_NONSALARY_TDS_RATE"]) == "0")
                        chkShowNonsalaryTDSRate.Checked = true;
                    else
                        chkShowNonsalaryTDSRate.Checked = false;
                    //
                    #region PROXY
                    if (Convert.ToString(drdShowRecord["PROXY_SETTINGS"]) == "0")
                    {
                        rbnConnectAutomatically.Checked = true;
                        rbnConnectAutomatically.Select();
                        grpAuthentication.Enabled = false;
                    }
                    else
                    {
                        rbnConnectProxy.Checked = true;
                        rbnConnectProxy.Select();
                        //
                        txtAddress.Text = Convert.ToString(drdShowRecord["HOST_ADDRESS"]);
                        txtPort.Text = Convert.ToString(drdShowRecord["HOST_PORT"]);
                        //
                        if (Convert.ToString(drdShowRecord["HOST_USER_NAME"]) != "")
                        {
                            chkProxy.Checked = true;
                            grpAuthentication.Enabled = true;
                            txtProxyUsername.Text = Convert.ToString(drdShowRecord["HOST_USER_NAME"]);
                            txtProxyPassword.Text = Convert.ToString(drdShowRecord["HOST_PASSWORD"]);
                        }
                        else
                        {
                            chkProxy.Checked = false;
                            grpAuthentication.Enabled = false;
                        }
                    }
                    #endregion
                    //
                    #region PASSWORD
                    strPassword = Convert.ToString(drdShowRecord["PASSWD"]);
                    //
                    if (strPassword != "")
                    {
                        chkSetPassword.Checked = true;
                        btnChangePassword.Visible = true;
                    }
                    else
                    {
                        chkSetPassword.Checked = false;
                        btnChangePassword.Visible = false;
                    }
                    //
                    #endregion
                    //
                    if (Convert.ToString(drdShowRecord["SHOW_ALLOCATED_FIELDS"]) == "1")
                        chkShowAllocatedFields.Checked = true;
                    //
                    if (Convert.ToString(drdShowRecord["SHOW_DEDUCTEE_COMPANY_WISE"]) == "1")
                        chkShowCompanyWiseDeductee.Checked = true;
                    //
                    if (Convert.ToString(drdShowRecord["PROHIBIT_ZERO_TAX_DEDUCTION"]) == "0")
                        chkStopZeroTaxDeduction.Checked = true;
                    else
                        chkStopZeroTaxDeduction.Checked = false;
                    //
                    if (Convert.ToString(drdShowRecord["SHOW_REGULAR_DEDUCTEE_CORRECTION"]) == "1")
                        chkShowRegularDeducteesInCorrcetion.Checked = true;
                    else
                        chkShowRegularDeducteesInCorrcetion.Checked = false;
                    //
                    if (Convert.ToString(drdShowRecord["ALLOT_AUTOMATIC_REF_NO"]) == "1")
                        chkAllotAutomaticRefNo.Checked = true;
                    else
                        chkAllotAutomaticRefNo.Checked = false;
                    //
                    if (Convert.ToString(drdShowRecord["ENABLE_DEDUCTEE_FIRST"]) == "1")
                        chkDeducteeEntryFirst.Checked = true;
                    else
                        chkDeducteeEntryFirst.Checked = false;
                    //-- 2015/04/29
                    txtBackupReminderDays.Text = Convert.ToString(drdShowRecord["BACKUP_REMINDER_DAYS"]);
                    if (cmnService.J_ReturnInt16Value(txtBackupReminderDays.Text) > 0)
                    {
                        txtBackupReminderDays.Enabled = true;
                        chkSetBackupReminder.Checked = true;
                    }
                    else
                    {
                        chkSetBackupReminder.Checked = false;
                        txtBackupReminderDays.Text = "0";
                        txtBackupReminderDays.Enabled = false;
                    }
                    //-- 2015/11/16
                    if (Convert.ToString(drdShowRecord["ROUND_OFF_TAXABLE_AMOUNT"]) == "1")
                        chkEnableRoundOffTaxableAmt.Checked = true;
                    else
                        chkEnableRoundOffTaxableAmt.Checked = false;
                    //-- 2015-07-23 @@ DHRUB
                    if (Convert.ToString(drdShowRecord["AUTO_CALCULATE_TDS_RATE"]) == "1")
                        chkAutoCalculateTdsRate.Checked = true;
                    //-- 2016/06/15
                    if (Convert.ToString(drdShowRecord["SHOW_COLUMNS_FORM16_PARTB"]) == "1")
                        chkShowLastTwoColumnsForm16PartB.Checked = true;
                    //-- 2016/09/01
                    if (Convert.ToString(drdShowRecord["ENABLE_LOCK_RETURN"]) == "1")
                        chkLockReturn.Checked = true;
                    //blnShowHelp = true;
                    //blnShowPANHelp = true;
                    //-- 2017/02/11
                    //-- 2017/02/20
                    blnXit = true;
                    //--
                    if (Convert.ToString(drdShowRecord["ENABLE_REFERENCE_NO"]) == "1")
                    {
                        chkEnableReferenceNo.Checked = true;
                        grpFormsRefernceNo.Enabled = true;
                        //-- 2020/05/30
                        if (Convert.ToString(drdShowRecord["SHARE_REFERENCE_NO_F24Q"]) == "1")
                            chkShareRefernceNoF24Q.Checked = true;
                        //--
                        if (Convert.ToString(drdShowRecord["SHARE_REFERENCE_NO_F26Q"]) == "1")
                            chkShareRefernceNoF26Q.Checked = true;
                        //--
                        if (Convert.ToString(drdShowRecord["SHARE_REFERENCE_NO_F27Q"]) == "1")
                            chkShareRefernceNoF27Q.Checked = true;
                        //--
                        if (Convert.ToString(drdShowRecord["SHARE_REFERENCE_NO_F27EQ"]) == "1")
                            chkShareRefernceNoF27EQ.Checked = true;
                        //--
                    }
                    else
                    {
                        grpFormsRefernceNo.Enabled = false;
                        //
                        lblCaptionReferenceNoFormNo.Enabled = false;
                        cmbReferenceNoFormNo.Enabled = false;
                        lblReferenceNoFormNo.Text = "";
                    }
                    //
                    blnXit = false;
                    //--
                    if (Convert.ToString(drdShowRecord["EMBEDDED_DEDUCTEE_SEARCH"]) == "1") //-- 2017/05/24
                    {
                        chkEmbeddedSearch.Checked = true;
                    }
                    else
                    {
                        chkEmbeddedSearch.Checked = false;
                    }
                    //--
                    if (Convert.ToString(drdShowRecord["DEDUCTED_DATE_EXCEL"]) == "1") //-- 2017/10/16
                        chkDeductedDate.Checked = true;
                    else
                        chkDeductedDate.Checked = false;
                    //-- Added By Abhishek Dey On 13/04/2018 --
                    if (Convert.ToString(drdShowRecord["ENABLE_AUTO_POPULATE_DEDUCTEE_CODE"]) == "1") //-- 2017/10/16
                        chkSelectDeducteeCode.Checked = true;
                    else
                        chkSelectDeducteeCode.Checked = false;
                    //--
                    if (Convert.ToString(drdShowRecord["BROWSER_LOGIN_TRACES"]) == "1") //-- 2018/09/04
                        chkTracesUsingBrowser.Checked = true;
                    else
                        chkTracesUsingBrowser.Checked = false;
                    //-- Added By Abhishek Dey On 20/08/2018 
                    if (Convert.ToString(drdShowRecord["ENABLE_HIDE_PASSWORD"]) == "1") //-- 2018/08/20
                        chkHidePassword.Checked = true;
                    else
                        chkHidePassword.Checked = false;
                    //--
                    cmbMinorCode.Text = Convert.ToString(drdShowRecord["DEFAULT_MINOR_CODE"]);
                    //--
                    if (Convert.ToString(drdShowRecord["ENABLE_NSDL_TEXT_CAPS_LOCK"]) == "1") //-- 2018/11/15
                        chkEnableCSIUpperCaseEntry.Checked = true;
                    else
                        chkEnableCSIUpperCaseEntry.Checked = false;
                    //--
                    //--
                    if (Convert.ToString(drdShowRecord["LOAD_SECTION_AUTO"]) == "1") //-- 2018/12/07
                        chkLoadSectionAuto.Checked = true;
                    else
                        chkLoadSectionAuto.Checked = false;
                    //--
                    txtMaxNewExcelSheetRecords.Text = Convert.ToString(drdShowRecord["MAX_REC_EXCEL_IMPORT_ALERT"]); //-- 2019/08/01
                    //--                    
                    if (Convert.ToString(drdShowRecord["SEC194N_EXCESS_1CRORE_EXCEL_IMPORT_OPTION"]) == "1") //-- 2020/03/02
                        chkCashExcess1croreSection194N.Checked = true;
                    else
                        chkCashExcess1croreSection194N.Checked = false;
                    //-----------------------------------------
                    //-- 2021/01/07
                    if (Convert.ToString(drdShowRecord["PARTY_SEARCH_PAN_WISE"]) == "0")
                        chkPANWisePartySearch.Checked = false;
                    else
                        chkPANWisePartySearch.Checked = true;
                    //-- 2021/08/18
                    if (Convert.ToString(drdShowRecord["AUTO_POPULATE_PAID_AMOUNT_TAX_AMOUNT"]) == "0")
                        chkAutoPopulateAmountTDS.Checked = false;
                    else
                        chkAutoPopulateAmountTDS.Checked = true;
                    //-- 2022/04/09                    
                    if (Convert.ToString(drdShowRecord["HIDE_SMTP_PASSWORD"]) == "0")
                        chkHideSMTPPassword.Checked = false;
                    else
                        chkHideSMTPPassword.Checked = true;
                    //-- 2022/08/12                   
                    if (Convert.ToString(drdShowRecord["FETCH_SECTION_FROM_PAN_NAME"]) == "0")
                        chkFetchSection.Checked = false;
                    else
                        chkFetchSection.Checked = true;
                    //-- 2024/03/12
                    if (Convert.ToString(drdShowRecord["ENABLE_PASSWORD_ITPORTALS_MODULE"]) == "0")
                        chkEnableITPortalsPasswordModule.Checked = false;
                    else
                        chkEnableITPortalsPasswordModule.Checked = true;
                    //-- 2025/06/26
                    if (Convert.ToString(drdShowRecord["ROUND_OFF_TAXABLE_AMOUNT_26Q_27Q_27EQ"]) == "0")
                        chkEnableRoundOffTaxableAmt26Q27Q27EQ.Checked = false;
                    else
                        chkEnableRoundOffTaxableAmt26Q27Q27EQ.Checked = true;
                    //-- 2025/09/04
                    if (Convert.ToString(drdShowRecord["EXPORT_REPORTS_CSV"]) == "0")
                        chkExportTOCSV.Checked = false;
                    else
                        chkExportTOCSV.Checked = true;
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //-- 2015-10-31 @@DHRUB
                    //--------------------------------------------
                    //txtFvuFileGenerationPath.Text = cmnService.J_GetRegistryKeyValue(TDSMAN.Classes.TDSMAN.T_pCompanyName + "\\" + TdsMan.GetRegistryFolder(),
                    //                                                                     T_RegistrationInfo.File_Creation_Path.ToString());
                    txtFvuFileGenerationPath.Text = TdsMan.T_GetFileCreationPathFromRegistry();
                    //-----------------------------------------------------------

                    chkShowDeductedDate.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //--
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        private void BtnRestoreDefaults_Click(object sender, EventArgs e)
        {

        }






        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=LSOH55B1R0A&list=PLy1JUN9HgGMyjsqLHVVmZjb6ddyfQPWNo");
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0122", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region CmbReferenceNoFormNo_SelectedIndexChanged
        private void CmbReferenceNoFormNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbReferenceNoFormNo.Text == T_FormNo.F24Q)
            {
                lblReferenceNoFormNo.Text = "Enter 'Reference No' in cell number 'O1' of the 'Deductee Details' Excel Sheet\n" +
                                            "and enter 'Reference No' in cell number 'CE1' of the 'Salary Details' Excel Sheet.";
            }
            else if (cmbReferenceNoFormNo.Text == T_FormNo.F26Q)
            {
                lblReferenceNoFormNo.Text = "Enter 'Reference No' in cell number 'Q1' of the 'Deductee Details' Excel Sheet.";
            }
            else if (cmbReferenceNoFormNo.Text == T_FormNo.F27Q)
            {
                lblReferenceNoFormNo.Text = "Enter 'Reference No' in cell number 'Z1' of the 'Deductee Details' Excel Sheet.";
            }
            else if (cmbReferenceNoFormNo.Text == T_FormNo.F27EQ)
            {
                lblReferenceNoFormNo.Text = "Enter 'Reference No' in cell number 'V1' of the 'Deductee Details' Excel Sheet.";
            }
        }
        #endregion
    }
}