
#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TDSMAN.Classes;
using TDSMAN.FormPar;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysTrialRegistration : Form
    {
        #region System Generated Code
        public SysTrialRegistration()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        //--
        string strDestinationPath = "";
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        //JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        // WEB CLASS
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--           
        private DataSet dsDataGrid;
        private DataSet dsDataPrint;

        private DataGridView ViewGrid;
        private ComboBox SearchCombo;
        //----
        string strLicenseeName = "";
        string strCompanyName = "";
        string strEmail = "";
        string strMobile = "";
        string strActivationNumber = "";
        string strSQL = "";
        //
        int intInstallType = 0;
        //----
        bool J_blnConnected = true;
        bool blIs_FirstTime_Reg = true;
        //
        string strUserMessage = "Internet Connectivity not found";
        #endregion

        #region User Defined Events

        #region SysPage1_Load
        private void SysPage1_Load(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE)
            {
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (BETA) Registration";
                lblHeaderMessage.Text = "\n Welcome to TDSMAN (BETA) \n Registration";
            }
            else
            {
                this.Text = "TDSMAN " + (TDSMAN.Classes.TDSMAN.T_pPackageFAYear - 1) + "-" + TDSMAN.Classes.TDSMAN.T_pPackageFAYear.ToString() + " (trial) Registration";
                lblHeaderMessage.Text = "\n Welcome to TDSMAN (trial) \n Registration";
            }
            //
            lblUpdtDate.Text = "Version date : " + TDSMAN.Classes.TDSMAN.T_pLastUpdtDate;
            //
            txtLicenseeNameOnline.Select();
            //
        }
        #endregion

        #region BtnNext_Click
        private void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbcRegistration.SelectedTab == tbpOnline2)
                {
                    if (ValidateFields() == false) return;
                    //
                    this.Cursor = Cursors.WaitCursor;
                    //

                    try
                    {
                        if (TdsMan.T_CheckInternetConnectivty() == true)
                        {
                            //
                            if (TdsMan.T_CheckServerTDSMAN() == true)
                            {
                                //if (TDSMAN.Classes.TDSMAN.T_pTrialRunningTime == 0)
                                //{
                                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.COMMERCIAL_LAW_HOUSE) //-2022/04/11
                                {
                                }
                                else
                                {
                                    if (Registration.Save_Trial_Records_FAYear(cmnService.J_ReturnInt32Value(TDSMAN.Classes.TDSMAN.T_pTrialRandomNumber), txtLicenseeNameOnline.Text,
                                                                           txtCompanyNameOnline.Text, txtLicenseeEmailOnline.Text,
                                                                           txtLicenseeMobileOnline.Text, "", "", "", txtLicenseeCityOnline.Text, "", "", false, TDSMAN.Classes.TDSMAN.T_pPackageFAYear) == false)
                                        tbcRegistration.SelectTab(tbpSucessfullyRegistered);  //return;
                                                                                              //-- 2025/04/22
                                    ////TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                                    ////objBrow.Email = txtLicenseeEmailOnline.Text;
                                    ////objBrow.Show();
                                    //////--
                                    ////Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), txtLicenseeEmailOnline.Text.Trim());
                                    ////if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                                    ////{
                                    ////    //--
                                    ////    string strTrialVersionRegistry = TdsMan.GetRegistryFolder() + "(Trial)";
                                    ////    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistry,
                                    ////                                    T_RegistrationInfo.Blog_email_flag.ToString(),
                                    ////                                    T_YES_NO.YES.ToString());
                                    ////}
                                    ////else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                                    ////{
                                    ////    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                    ////                                    T_RegistrationInfo.Blog_email_flag.ToString(),
                                    ////                                    T_YES_NO.YES.ToString());
                                    ////}
                                    //--
                                }
                                //}
                            }
                        }
                    }
                    catch
                    {
                        //ANY EXCEPTION AT INTERNET
                    }
                    //
                    //tbcRegistration.SelectTab(tbpSucessfullyRegistered);
                    //-------------------------------------------------
                    // ONLINE ACTIVATION
                    //-------------------------------------------------
                    //
                    //this.Cursor = Cursors.Default;
                    //
                    string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                    //
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                    T_RegistrationInfo.Activ_Code.ToString(), TDSMAN.Classes.TDSMAN.T_pTrialRandomNumber);
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                    T_RegistrationInfo.Licensee_Name.ToString(), txtLicenseeNameOnline.Text);
                    //-- ANIK 2015/01/11
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                    T_RegistrationInfo.Reg_Date.ToString(), 
                                                    Convert.ToString(dtService.J_ConvertddMMyyyy(System.DateTime.Now.ToString())));                    
                    //
                    this.Cursor = Cursors.Default;
                    //
                    cmnService.J_UserMessage("Sucessfully Registered");
                    //////////-- 2025/04/22
                    ////////TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
                    ////////objBrow.Email = txtLicenseeEmailOnline.Text;
                    ////////objBrow.Show();
                    //////////--
                    ////////Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), txtLicenseeEmailOnline.Text.Trim());
                    ////////if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    ////////{
                    ////////    //--
                    ////////    strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                    ////////    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                    ////////                                    T_RegistrationInfo.Blog_email_flag.ToString(),
                    ////////                                    T_YES_NO.YES.ToString());
                    ////////}
                    ////////else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                    ////////{
                    ////////    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                    ////////                                    T_RegistrationInfo.Blog_email_flag.ToString(),
                    ////////                                    T_YES_NO.YES.ToString());
                    ////////}
                    //////////--
                    //
                    this.Close();
                    this.Dispose();
                    //
                    TDSMAN.Classes.TDSMAN.T_pShowUpdateMessage = true;
                    //
                    J_Var.frmMain = new mdiTDSMAN();
                    J_Var.frmMain.ShowDialog();
                    //

                }
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        #region BtnBack_Click
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //if (tbcRegistration.SelectedTab == tbpOffline1)
            //{
            //    tbcRegistration.SelectTab(tbpSelectType);
            //    //
            //    lblHeaderMessage.Text = "\n Welcome to TDSMAN \n Registration";
            //    //
            //    lblPleaseWaitMessage1.Visible = false;
            //    //
            //    BtnBack.Enabled = false;
            //    BtnBack.BackColor = Color.LightGray;
            //    //    
            //    BtnNext.Text = "&Next >";
            //}
            //else if (tbcRegistration.SelectedTab == tbpOffline2)
            //{
            //    tbcRegistration.SelectTab(tbpOffline1);
            //    //
            //    lblHeaderMessage.Text = "\n Offline Registration Process";
            //    //
            //    BtnNext.Text = "&Next >";
            //}
            //else if (tbcRegistration.SelectedTab == tbpSucessfullyRegistered)
            //{
            //    //tbcRegistration.SelectTab(tbpOffline2);
            //    //BtnNext.Text = "&Next >";
            //}
            //else if (tbcRegistration.SelectedTab == tbpOnline1)
            //{
            //    tbcRegistration.SelectTab(tbpSelectType);
            //    //
            //    lblHeaderMessage.Text = "\n Welcome to TDSMAN \n Registration";
            //    //
            //    lblPleaseWaitMessage1.Visible = false;
            //    //
            //    BtnBack.Enabled = false;
            //    BtnBack.BackColor = Color.LightGray;
            //    //
            //    BtnNext.Text = "&Next >";
            //}
            //else if (tbcRegistration.SelectedTab == tbpOnline2)
            //{
            //    tbcRegistration.SelectTab(tbpOnline1);
            //    //
            //    lblHeaderMessage.Text = "\n Online Registration Process";
            //    BtnNext.Text = "&Next >";
            //}
            //else if (tbcRegistration.SelectedTab == tbpOnline3)
            //{
            //    tbcRegistration.SelectTab(tbpOnline1);
            //    //
            //    lblHeaderMessage.Text = "\n Online Registration Process";
            //    BtnNext.Text = "&Next >";
            //}
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region txtActivationCodeOffline_KeyPress
        private void txtActivationCodeOffline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnNext_Click(sender, e);
        }

        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtEnterSerialNoOnline_KeyPress
        private void txtEnterSerialNoOnline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnNext_Click(sender, e);
        }
        #endregion


        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion        

        #endregion

        #region User Defined Functions
        
        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (tbcRegistration.SelectedTab == tbpOnline2)
                {
                    if (txtLicenseeNameOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Licensee Name");
                        txtLicenseeNameOnline.Select();
                        return false;
                    }
                    //
                    if (txtCompanyNameOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Company Name");
                        txtCompanyNameOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeEmailOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Email");
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //-- ANIK @ 2017/04/17
                    if (TdsMan.T_CheckEmailFormat(txtLicenseeEmailOnline.Text.Trim()) == false)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //
                    if (TdsMan.T_ValidateEmail(txtLicenseeEmailOnline.Text) == false)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter valid Email");
                        txtLicenseeEmailOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeMobileOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Mobile No.");
                        txtLicenseeMobileOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeMobileOnline.Text.Length < 10)
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter valid Mobile No.");
                        txtLicenseeMobileOnline.Select();
                        return false;
                    }
                    //
                    if (txtLicenseeCityOnline.Text.Trim() == "")
                    {
                        lblPleaseWaitMessage3.Visible = false;
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the City");
                        txtLicenseeCityOnline.Select();
                        return false;
                    }
                }
                else if (tbcRegistration.SelectedTab == tbpSucessfullyRegistered)
                {
                    if (txtTrialActivationNumber.Text.Trim() == "")
                    {
                        //
                        this.Cursor = Cursors.Default;
                        //
                        cmnService.J_UserMessage("Enter the Activation Number");
                        txtTrialActivationNumber.Select();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion
        
        #region T_WriteToRegistry
        public bool T_WriteToRegistry(string SerialNo, string LicenseeName, string Date, string ActivationCode)
        {
            try
            {
                // CREATE REGISTRY
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName);
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TDSMAN.Classes.TDSMAN.T_pPackageName);
                // SET SERIAL NO
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TDSMAN.Classes.TDSMAN.T_pPackageName,
                                                T_RegistrationInfo.Serial_No.ToString(), SerialNo);

                if (LicenseeName != "")
                    // SET LICENSEE NAME
                    cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TDSMAN.Classes.TDSMAN.T_pPackageName,
                                                   T_RegistrationInfo.Licensee_Name.ToString(), LicenseeName);

                // SET SYSTEM DATE
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TDSMAN.Classes.TDSMAN.T_pPackageName,
                                                T_RegistrationInfo.Reg_Date.ToString(), Date);

                // SET ACTIVATION CODE
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TDSMAN.Classes.TDSMAN.T_pPackageName,
                                                T_RegistrationInfo.Activ_Code.ToString(), ActivationCode);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion       

        private void lblHelpLine_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}