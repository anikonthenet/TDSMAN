
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
using System.Globalization;
using System.Text.RegularExpressions;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormBrowser;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;


using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;


#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnTracesChallanCorrection : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnTracesChallanCorrection()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion


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

        #region Objects & Variables declaration

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        DateService dtService = new DateService();

        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        enum enmRequestType
        {
            Login,
            ChallanCheklist,
            ProceedChallanCorrection,
            EnterChallanDetails,
            RequestChallanCorrection,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

        private DataTable _dtOriginal;   // backup copy
                                         //private TracesConnect_OneLogin oneLogin;

        private string CurrentCaptchaId = "";
        #endregion

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnStatementStatusTraces_Activated
        private void TrnStatementStatusTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 21/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion
        //-----------------------------------------

        #region TrnStatementStatusTraces_Load
        private void TrnStatementStatusTraces_Load(object sender, EventArgs e)
        {
            ////int h = Screen.PrimaryScreen.WorkingArea.Height;
            ////int w = Screen.PrimaryScreen.WorkingArea.Width;
            ////this.ClientSize = new Size(w, h);

            ////lblTitle.Text = "Request For Submit Declaration of non-filling";
            ////lblTitle.Text = "Communication - Inbox (Action)";

            ////InitializeCaptcha();
            ////picCaptcha.Image = Properties.Resources.captcha_loading;
            ////if (!bgWorkerLoadCaptcha.IsBusy)
            ////    bgWorkerLoadCaptcha.RunWorkerAsync();
            //////--

            ////ClearControls();
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);

            lblTitle.Text = "OLTAS Challan Correction";
            if (TDSMAN.Classes.TDSMAN.T_pPackageFAYear < (int)Software_Version.FY2026_27)
                lnkPost2526.Visible = false;
            //-------------------------------------------------------
            // NEW: OneLogin check
            //-------------------------------------------------------
            //if (TracesSessionManager_OneLogin.IsSessionActive())
            //{
            //    // Hide login controls (captcha, TAN, userId, password, login button)
            //    grpLoginDetails.Visible = false;   // <-- create GroupBox of login UI

            //    //MessageBox.Show("TRACES session active -> Login not required");
            //    //--------------------------------------------
            //    ArrayList objList = new ArrayList();
            //    objList.Add(enmRequestType.CommuncationDetailGrid1);
            //    //-------------------------------------------
            //    pgTimer.Start();
            //    if (!bgWorker.IsBusy)
            //        bgWorker.RunWorkerAsync(objList);
            //    // LATER we will call:
            //    // LoadInboxAction_OneLogin();

            //    return;
            //}

            //-------------------------------------------------------
            // If no session, show login UI
            //-------------------------------------------------------
            grpLoginDetails.Visible = true;
            grpChallanDetails.Visible = true;

            // Load captcha (existing logic)
            //if (!bgWorkerLoadCaptcha.IsBusy)
            //    bgWorkerLoadCaptcha.RunWorkerAsync();

            ClearControls();
            //
            grpLoginDetails.Height = 398;
        }
        #endregion              

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //--
                //TracesLogin objLogin = new TracesLogin();
                //objLogin.UserID = txtUserID.Text;
                //objLogin.Password = txtPassword.Text;
                //objLogin.TAN = txtTANNo.Text;
                //objLogin.CaptchaCode = txtCaptchaCode.Text;
                ////--------------------------------------------
                //ArrayList objList = new ArrayList();
                //objList.Add(enmRequestType.Login);
                //objList.Add(objLogin);
                ////-------------------------------------------
                //pgTimer.Start();
                ////-------------------------------------------
                //if (!bgWorker.IsBusy)
                //    bgWorker.RunWorkerAsync(objList);
                //LoginToTraces(driver, "TANLOGIN", "password");
                //
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
            }
        }

        #endregion

        #region btnLoginCancel_Click
        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            //InitializeCaptcha();
            //InitializeCaptcha_OneLogin();
        }

        #endregion

        #region btnCaptchaRefresh_MouseMove
        private void btnCaptchaRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.Show("Click to refresh image", btnCaptchaRefresh);
        }
        #endregion

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //
                if (txtBSRCode.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("BSR Code - Cannot be Blank");
                    txtBSRCode.Select();
                    return;
                }
                //
                if (dtService.J_IsBlankDateCheck(ref mskDateOfDeposit, "Payment Date - Cannot be Blank") == true)
                    return;
                //----------------------------------------------------------
                //-- VALID DATE CHECK
                //----------------------------------------------------------
                //if (dtService.J_IsDateValid(mskDateOfDeposit) == false)
                //{
                //    cmnService.J_UserMessage("Incorrect Format of the Date of Payment");
                //    mskDateOfDeposit.Select();
                //    return;
                //}
                //DateTime dtDeposit;
                //if (!DateTime.TryParseExact(
                //        mskDateOfDeposit.Text.Trim(),
                //        "dd/mm/yyyy",
                //        CultureInfo.InvariantCulture,
                //        DateTimeStyles.None,
                //        out dtDeposit))
                //{
                //    cmnService.J_UserMessage("Invalid Date format. Use DD/MM/YYYY.");
                //    return;
                //}
                //-----------------------------------------------------------------------
                //-- DATE OF PAYMENT
                //-----------------------------------------------------------------------
                if (dtService.J_IsBlankDateCheck(ref mskDateOfDeposit, "Challan Date - Cannot be Blank") == true)
                    return ;
                //----------------------------------------------------------
                //-- VALID DATE CHECK
                //----------------------------------------------------------
                if (dtService.J_IsDateValid(mskDateOfDeposit) == false)
                {
                    cmnService.J_UserMessage("Incorrect Format of the Date of Payment");
                    mskDateOfDeposit.Select();
                    return;
                }
                //
                if (txtSerialNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Challan Serial No. - Cannot be Blank");
                    txtSerialNo.Select();
                    return;
                }
                // Parse Amount
                decimal decAmount;
                if (!decimal.TryParse(
                        txtAmount.Text.Trim(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out decAmount))
                {
                    cmnService.J_UserMessage("Invalid Amount.");
                    return;
                }
                //--
                #region COMMENTED
                //string strOS = TdsMan.GetOSVersion();
                //string strBrowser = strOS == "XP"
                //                    ? TdsMan.GetSystemDefaultBrowserXP()
                //                    : TdsMan.GetSystemDefaultBrowser();

                //IWebDriver driver = null;
                ////
                //if (strBrowser.Contains("chrome"))
                //{
                //    var service = ChromeDriverService.CreateDefaultService();
                //    service.HideCommandPromptWindow = true;

                //    var options = new ChromeOptions();
                //    options.AddArgument("--start-maximized");
                //    options.AddArgument("--disable-web-security");
                //    options.AddArgument("--no-proxy-server");
                //    options.AddArgument("--no-sandbox");
                //    options.AddArgument("--disable-blink-features=AutomationControlled");
                //    options.AddUserProfilePreference("credentials_enable_service", false);
                //    options.AddUserProfilePreference("profile.password_manager_enabled", false);

                //    // Use a fresh temp profile directory (isolated)
                //    string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                //    options.AddArgument($"--user-data-dir={tempProfileDir}");

                //    driver = new ChromeDriver(service, options);

                //}
                //else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                //{
                //    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                //    serv.HideCommandPromptWindow = true;
                //    var options1 = new InternetExplorerOptions();
                //    driver = new InternetExplorerDriver(serv, options1);
                //}
                //else if (strBrowser.Contains("MSEdge"))
                //{
                //    var service = EdgeDriverService.CreateDefaultService();
                //    service.HideCommandPromptWindow = true;

                //    var options = new EdgeOptions();
                //    options.AddArgument("start-maximized");
                //    options.AddArgument("disable-web-security");
                //    options.AddArgument("no-proxy-server");
                //    options.AddArgument("no-sandbox");
                //    options.AddArgument("disable-blink-features=AutomationControlled");
                //    // Ensure the folder is not already locked or in use
                //    string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                //    if (Directory.Exists(tempProfileDir))
                //    {
                //        try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
                //    }
                //    Directory.CreateDirectory(tempProfileDir); // Forcefully create it

                //    options.AddArgument($"--user-data-dir={tempProfileDir}");

                //    driver = new EdgeDriver(service, options);
                //}
                //else
                //{
                //    cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
                //    return;
                //}

                //OpenOLTASCorrection(driver);

                //bool result = SubmitOLTASChallan(
                //                driver,
                //                txtBSRCode.Text,
                //                mskDateOfDeposit.Text,
                //                txtSerialNo.Text,
                //                txtAmount.Text);

                //if (result)
                //{
                //    MessageBox.Show("Challan loaded successfully.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to load challan.");
                //}

                //LoginToTraces(driver,    txtUserID.Text,    txtPassword.Text,    txtTANNo.Text);

                //OpenOLTASCorrection(driver);

                //SubmitOLTASChallan(
                //    driver,
                //    txtBSRCode.Text,
                //    mskDateOfDeposit.Text,
                //    txtSerialNo.Text,
                //    decAmount.ToString());

                //var resp = objAccount.OLTAS_Challan_Go_OneLogin(txtBSRCode.Text.Trim(), mskDateOfDeposit.Text, txtSerialNo.Text.Trim(), txtAmount.Text.Trim(), strHTML4);

                //if (resp.Respons != enmResponse.Success)
                //{
                //    cmnService.J_UserMessage("Request failed: " + resp.Message);
                //    return;
                //}


                //if (resp.Respons == enmResponse.Success)
                //    cmnService.J_UserMessage(resp.Message.ToString());
                //else
                //    cmnService.J_UserMessage("Failed: " + resp.Message);
                //objData.Forms = cmbFormNo.Text; ;
                //objList.Add(objData);
                ////-------------------------------------------
                //pgTimer.Start();
                ////-------------------------------------------
                //if (!bgWorker.IsBusy)
                //    bgWorker.RunWorkerAsync(objList);
                #endregion
                //--
                CorrectionData objData = new CorrectionData();
                TracesData objTracess = new TracesData();
                TracesLogin objLogIn = new TracesLogin();

                objLogIn.TAN = txtTANNo.Text;
                objLogIn.Password = txtPassword.Text;

                objTracess.BSRCode = txtBSRCode.Text.Trim();
                //
                DateTime dt = DateTime.Parse(mskDateOfDeposit.Text);
                string ChallanDate = dt.ToString("dd-MMM-yyyy");
                objTracess.FromChallanDepositDate = ChallanDate;
                //
                objTracess.ChallanAmount = txtAmount.Text;
                objTracess.CDRecordNumber = txtSerialNo.Text;
                //
                objTracess.MakeChallanCorrectionRequest = true;
                //
                objData.TracesData = objTracess;
                objData.TracesLogin = objLogIn;
                //
                TrnChallanCorrectionBrowser objBrowser = new TrnChallanCorrectionBrowser(objData);
                objBrowser.MdiParent = TrnChallanPANCorrection.ActiveForm;
                objBrowser.Show();
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(ex.Message);
            }
        }
        #endregion


        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            //this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            //this.pgTimer.Interval = 1000;
            //this.pBar.Step = 5;

            //// Update progress bar
            //if ((pBar.Value + pBar.Step) > pBar.Maximum)
            //{
            //    pBar.Value = pBar.Minimum;
            //}
            //else
            //{
            //    pBar.Value += pBar.Step;
            //}
        }

        #endregion


        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
            }
            else
                if (TdsMan.gTANNoPANNoValidation(txtTANNo, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }

        #endregion

        #region txtTAN_KeyDown
        private void txtTAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms. Keys.Down)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region txtTAN_TextChanged
        private void txtTAN_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (txtTANNo.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                if (blnShowHelp == false)
                    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    //lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) + drdShowDeducteeHelp["USER_PASSWORD"]));
                        //-- ANIK @ 2015/04/20
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                  Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));

                        //--
                        //if (lstDeducteeHelp.Height <= 300)
                        //    lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
                    }
                    //--
                    if (lstDeducteeHelp.Items.Count <= 0)
                        lstDeducteeHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #region txtTAN_Leave
        private void txtTAN_Leave(object sender, EventArgs e)
        {
            if (txtTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion


        #region lstDeducteeHelp_KeyPress
        private void lstDeducteeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstDeducteeHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
                txtTANNo.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            string strlstDeducteeHelp = lstDeducteeHelp.Text;

            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            //-- ANIK @ 2015/04/20
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //
            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion


        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            //ShowHideLoginDetails(enmRequestType.LogOff);
            //ClearControls();
            try
            {
                // 1. Call TRACES official logout (if implemented)
                try
                {
                    objAccount.Logoff();
                }
                catch { }

                // 2. FULL RESET of OneLogin session + cookies
                TracesConnect_OneLogin.ResetOneLoginState();
                TracesSessionManager_OneLogin.ClearSession();

                // 3. Reset UI
                ShowHideLoginDetails(enmRequestType.LogOff);
                ClearControls();

                // 4. Enable fresh login
                txtTANNo.Focus();
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("Error during Logoff:\n" + ex.Message);
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=RSVX8wDawkE");
        }

        #endregion


        #region cmbReason_SelectedIndexChanged

        private void cmbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtspecReasn.Visible = false;
            //if (cmbReason.SelectedIndex > 0)
            //{
            //    if (cmbReason.SelectedIndex == 7)
            //        txtspecReasn.Visible = true;
            //    else
            //        txtspecReasn.Visible = false;
            //}
        }

        #endregion



        #region btnAddStatement_Click

        private void btnAddStatement_Click(object sender, EventArgs e)
        {
            //ShowHideLoginDetails(enmRequestType.AddStatementDetail);
        }
        #endregion


        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            try
            {
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }
                //----------------------------------------------------
                objAccount = new TracesConnect();
                //Stream imgStream = objAccount.MakeInitialRequest();
                //Image img = Image.FromStream(imgStream);
                //this.picCaptcha.Image = img;

                var captcha = objAccount.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;
                this.picCaptcha.Image = captchaImage;
                //-------------------------------------------------------
                txtCaptchaCode.Text = "";
            }
            catch (Exception err)
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region ValidateFields
        bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtTANNo.Text))
            {
                cmnService.J_UserMessage("Please enter TAN");
                txtTANNo.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtUserID.Text))
            //{
            //    cmnService.J_UserMessage("Please enter User ID");
            //    txtUserID.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                cmnService.J_UserMessage("Please enter Password");
                txtPassword.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            //{
            //    cmnService.J_UserMessage("Please enter Captcha Code");
            //    txtCaptchaCode.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion



        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            ArrayList objList = new ArrayList();

            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    //grpDownloadList.Visible = false;
                    grpLoginDetails.Visible = true;
                    // grpProgress.Visible = true;
                    //InitializeCaptcha();
                    //InitializeCaptcha_OneLogin();
                    break;

                case enmRequestType.LogOff:
                    objList = new ArrayList();
                    objList.Add(enmRequestType.LogOff);
                    //-------------------------------------------
                    //pgTimer.Start();
                    ////-------------------------------------------
                    //if (!bgWorker.IsBusy)
                    //    bgWorker.RunWorkerAsync(objList);

                    break;

            }
        }

        #endregion

        #region ClearControls
        public void ClearControls()
        {
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtCaptchaCode.Text = "";
            txtTANNo.Text = "";
            //grpDownloadList.Visible = false;
            grpLoginDetails.Visible = true;
            //grpProgress.Visible = true;
            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            txtTANNo.Select();
        }
        #endregion

        



        private void btnBack_Click(object sender, EventArgs e)
        {
            //grpInputDetails.Visible = false;
            //grpListChallans.Visible = true;
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0092", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }



        #region ParseRequestNumber
        public string ParseRequestNumber(string html)
        {
            // TRACES shows Request Number inside HTML
            Match m = Regex.Match(html, @"Request Number\s*([0-9]+)");

            if (m.Success)
                return m.Groups[1].Value.Trim();

            return "";
        }
        #endregion

        
        

        #region SaveTracesInboxHeader
        public void SaveTracesInboxHeader(DataTable dt, string TAN)
        {
            //using (var con = new OleDbConnection(connStr))   // For MS Access
            //                                                 // using (var con = new SqlConnection(connStr)); // For SQL Server
            //{
            //    con.Open();
            //
            dmlService.J_BeginTransaction();
            foreach (DataRow dr in dt.Rows)
            {
                string commInbId = dr["commInbId"].ToString();

                // CHECK DUPLICATE
                strSQL = "SELECT COUNT(*) FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commInbId + "'";

                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    continue;
                //using (var cmd = new OleDbCommand(checkSql, con))
                //{
                //    cmd.Parameters.AddWithValue("@p1", commInbId);

                //    int count = Convert.ToInt32(cmd.ExecuteScalar());

                //    if (count > 0)
                //        continue;   // Already exists  skip
                //}

                // INSERT NEW ROW
                strSQL =
                        "INSERT INTO TRN_TRACES_INBOX_HEADER_ACTION (" +
                        "    TAN_NO," +
                        "    REFERENCE_NO," +
                        "    RECORD_DATE," +
                        "    RECORD_CATEGORY," +
                        "    DESCRIPTION," +
                        "    FY_YEAR," +
                        "    QUARTER," +
                        "    FORM_TYPE," +
                        "    COMM_ID," +
                        "    HID_FIN_YR," +
                        "    HID_QUAT," +
                        "    DECL_ID," +
                        "    CERT_NUM," +
                        "    COMM_INB_ID," +
                        "    COM_CAT_ID) " +
                        "VALUES (" +
                        "    '" + cmnService.J_ReplaceQuote(TAN) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Reference No"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Date"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Category"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Description"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Financial Year"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Quarter"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Form Type"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["hidfinYr"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["hidquat"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["declId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["certNum"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commInbId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["comcatid"].ToString()) + "')";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
            }
            dmlService.J_Commit();
            //}
        }
        #endregion

        #region SaveTracesInboxDetail
        public void SaveTracesInboxDetail(DataTable dt, string commInbId)
        {
            //using (var con = new OleDbConnection(connStr))   // For MS Access
            //                                                 // using (var con = new SqlConnection(connStr)); // For SQL Server
            //{
            //    con.Open();
            strSQL = @"SELECT TRACES_INBOX_HEADER_ID FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commInbId + "'";
            string strInboxHeaderId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            //
            dmlService.J_BeginTransaction();
            foreach (DataRow dr in dt.Rows)
            {
                //string commInbId = dr["commInbId"].ToString();

                // CHECK DUPLICATE
                strSQL = "SELECT COUNT(*) FROM TRN_TRACES_INBOX_DETAIL_ACTION WHERE TRACES_INBOX_HEADER_ID = " + strInboxHeaderId + "";

                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    continue;

                // INSERT NEW ROW
                strSQL =
                        "INSERT INTO TRN_TRACES_INBOX_DETAIL_ACTION (" +
                        "    TRACES_INBOX_HEADER_ID," +
                        "    REFERENCE_NO," +
                        "    RECORD_CATEGORY," +
                        "    DESCRIPTION," +
                        "    SUBJECT," +
                        "    MESSAGE," +
                        "    COMM_ID) " +
                        "VALUES (" +
                        "     " + cmnService.J_ReturnInt32Value(strInboxHeaderId) + "," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Reference No"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Category"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Description"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Subject"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Message"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commid"].ToString()) + "')";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
            }
            dmlService.J_Commit();
            //}
        }
        #endregion
                
        

        #region LoginToTraces
        public bool LoginToTraces(IWebDriver driver, string userId, string password, string tan)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

                driver.Navigate().GoToUrl("https://www.tdscpc.gov.in/app/login.xhtml");

                // Select Deductor
                IWebElement dedRadio = wait.Until(d => d.FindElement(By.Id("ded")));
                dedRadio.Click();

                // User ID
                IWebElement txtUser = wait.Until(d => d.FindElement(By.Id("userId")));
                txtUser.Clear();
                txtUser.SendKeys(userId);

                // Password
                IWebElement txtPass = driver.FindElement(By.Id("psw"));
                txtPass.Clear();
                txtPass.SendKeys(password);

                // TAN
                IWebElement txtTan = driver.FindElement(By.Id("tanpan"));
                txtTan.Clear();
                txtTan.SendKeys(tan);

                // Wait for captcha entry// Wait for captcha box
                IWebElement txtCaptcha = wait.Until(d => d.FindElement(By.Id("captcha")));

                // Focus cursor inside captcha textbox
                txtCaptcha.Click();

                // Optional: scroll to captcha
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", txtCaptcha);
                
                // Wait until dashboard appears
                wait.Until(d => d.Url.Contains("dashboard") || d.PageSource.Contains("Dashboard"));

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region OpenOLTASCorrection
        public bool OpenOLTASCorrection(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                driver.Navigate().GoToUrl("https://www.tdscpc.gov.in/app/dea/chklstoltascorr.xhtml");

                //btnProceed.Click();
                IWebElement btnProceed = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("proceed")));
                btnProceed.Click();

                //https://www.tdscpc.gov.in/app/dea/reqoltascorr.xhtml
                System.Threading.Thread.Sleep(2000);

                IWebElement btnProceed1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("proceed")));
                btnProceed1.Click();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion


        #region SubmitOLTASChallan
        public bool SubmitOLTASChallan(
                                IWebDriver driver,
                                string bsrCode,
                                string depositDate,
                                string challanSerialNo,
                                string challanAmount)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                IWebElement txtBSR = wait.Until(d => d.FindElement(By.Name("bsrCode")));
                txtBSR.Clear();
                txtBSR.SendKeys(bsrCode);

                DateTime dt = DateTime.Parse(depositDate);
                string ChallanDate = dt.ToString("dd-MMM-yyyy");
                IWebElement txtDate = driver.FindElement(By.Name("dateOfDep"));
                txtDate.Clear();
                txtDate.SendKeys(ChallanDate);

                IWebElement txtSerial = driver.FindElement(By.Name("chlnSNo"));
                txtSerial.Clear();
                txtSerial.SendKeys(challanSerialNo);

                IWebElement txtAmount = driver.FindElement(By.Name("chlnAmt"));
                txtAmount.Clear();
                txtAmount.SendKeys(challanAmount);

                IWebElement btnGo = driver.FindElement(By.Name("clickGo"));
                btnGo.Click();

                wait.Until(d => d.PageSource.Contains("Please enter the new Values"));

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region OpenTrackChallanCorrectionRequest
        public bool OpenTrackChallanCorrectionRequest(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                driver.Navigate().GoToUrl("https://www.tdscpc.gov.in/app/dea/trackoltascorr.xhtml");

                ////btnProceed.Click();
                //IWebElement btnProceed = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("proceed")));
                //btnProceed.Click();

                ////https://www.tdscpc.gov.in/app/dea/reqoltascorr.xhtml
                //System.Threading.Thread.Sleep(2000);

                //IWebElement btnProceed1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("proceed")));
                //btnProceed1.Click();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion


        #region CurrencyControl_KeyPress
        private void CurrencyControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtNumeric = (TextBox)sender;

            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));

        }
        #endregion

        private void TxtSerialNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void BtnViewRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 

                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //--
                #region COMMENTED
                //string strOS = TdsMan.GetOSVersion();
                //string strBrowser = strOS == "XP"
                //                    ? TdsMan.GetSystemDefaultBrowserXP()
                //                    : TdsMan.GetSystemDefaultBrowser();

                //IWebDriver driver = null;
                ////
                //if (strBrowser.Contains("chrome"))
                //{
                //    var service = ChromeDriverService.CreateDefaultService();
                //    service.HideCommandPromptWindow = true;

                //    var options = new ChromeOptions();
                //    options.AddArgument("--start-maximized");
                //    options.AddArgument("--disable-web-security");
                //    options.AddArgument("--no-proxy-server");
                //    options.AddArgument("--no-sandbox");
                //    options.AddArgument("--disable-blink-features=AutomationControlled");
                //    options.AddUserProfilePreference("credentials_enable_service", false);
                //    options.AddUserProfilePreference("profile.password_manager_enabled", false);

                //    // Use a fresh temp profile directory (isolated)
                //    string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                //    options.AddArgument($"--user-data-dir={tempProfileDir}");

                //    driver = new ChromeDriver(service, options);

                //}
                //else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                //{
                //    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                //    serv.HideCommandPromptWindow = true;
                //    var options1 = new InternetExplorerOptions();
                //    driver = new InternetExplorerDriver(serv, options1);
                //}
                //else if (strBrowser.Contains("MSEdge"))
                //{
                //    var service = EdgeDriverService.CreateDefaultService();
                //    service.HideCommandPromptWindow = true;

                //    var options = new EdgeOptions();
                //    options.AddArgument("start-maximized");
                //    options.AddArgument("disable-web-security");
                //    options.AddArgument("no-proxy-server");
                //    options.AddArgument("no-sandbox");
                //    options.AddArgument("disable-blink-features=AutomationControlled");
                //    // Ensure the folder is not already locked or in use
                //    string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                //    if (Directory.Exists(tempProfileDir))
                //    {
                //        try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
                //    }
                //    Directory.CreateDirectory(tempProfileDir); // Forcefully create it

                //    options.AddArgument($"--user-data-dir={tempProfileDir}");

                //    driver = new EdgeDriver(service, options);
                //}
                //else
                //{
                //    cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
                //    return;
                //}
                ////
                //LoginToTraces(driver, txtUserID.Text, txtPassword.Text, txtTANNo.Text);
                ////
                //OpenTrackChallanCorrectionRequest(driver);
                #endregion
                //--
                CorrectionData objData = new CorrectionData();
                TracesData objTracess = new TracesData();
                TracesLogin objLogIn = new TracesLogin();

                objLogIn.TAN = txtTANNo.Text;
                objLogIn.Password = txtPassword.Text;
                //
                objTracess.MakeChallanCorrectionRequest = false;
                //objTracess.BSRCode = txtBSRCode.Text.Trim();
                ////
                //DateTime dt = DateTime.Parse(mskDateOfDeposit.Text);
                //string ChallanDate = dt.ToString("dd-MMM-yyyy");
                //objTracess.FromChallanDepositDate = ChallanDate;
                ////
                //objTracess.ChallanAmount = txtAmount.Text;
                //objTracess.CDRecordNumber = txtSerialNo.Text;

                objData.TracesData = objTracess;
                objData.TracesLogin = objLogIn;
                //
                TrnChallanCorrectionBrowser objBrowser = new TrnChallanCorrectionBrowser(objData);
                objBrowser.MdiParent = TrnChallanPANCorrection.ActiveForm;
                objBrowser.Show();
                //
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(ex.Message);
            }
        }

        private void LnkPost2526_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnTracesChallanCorrection_2526(), J_Var.frmMain, "OLTAS Challan Correction");
        }
    }

}

