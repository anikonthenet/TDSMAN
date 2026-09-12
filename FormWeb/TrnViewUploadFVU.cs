
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

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewUploadFVU : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnViewUploadFVU()
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

        #region TrnPanVarification_Load
        private void TrnPanVarification_Load(object sender, EventArgs e)
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //txtPAN.Text = "BROPK6848J";
                //lblTitle.Text = "Know Your TAN"; 
                //lblTitle.Text = "View uploded return";
                lblTitle.Text = "View Filed TDS/TCS";
                //
                if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
                {
                    txtPassword.UseSystemPasswordChar = true;
                }

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
            txtUserID.Text = "";
            txtPassword.Text = "";
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //objAccount.RequestForFillingStatus();
        }

        
        #region OnlineViewReturn
        private void OnlineViewReturn()
        {
            string strOS = TdsMan.GetOSVersion();
            //--
            string strBrowser = "";
            if (strOS == "XP")
                strBrowser = TdsMan.GetSystemDefaultBrowserXP();
            else
                strBrowser = TdsMan.GetSystemDefaultBrowser();
            //--
            IWebDriver driver = null;
            //====================================================
            if (strBrowser.Contains("chrome"))
            {
                #region COMMENTED
                ////ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                ////service.HideCommandPromptWindow = true;

                ////var options = new ChromeOptions();
                ////// options.AddAdditionalCapability("useAutomationExtension", false);
                ////options.AddExcludedArgument("enable-automation");

                //////// options.AddArgument("--window-position=-32000,-32000");
                ////options.AddArgument("--start-maximized");
                ////options.AddArgument("--disable-web-security");
                ////options.AddArgument("--no-proxy-server");
                ////options.AddArgument("--no-sandbox");
                ////options.AddUserProfilePreference("credentials_enable_service", false);
                ////options.AddUserProfilePreference("profile.password_manager_enabled", false);

                ////options.AddUserProfilePreference("disable-popup-blocking", true);

                ////options.AddArgument("--disable-blink-features=AutomationControlled");

                ////driver = new ChromeDriver(service, options);
                ////////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////////return;
                #endregion
                //--
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--no-proxy-server");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                // Use a fresh temp profile directory (isolated)
                string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                options.AddArgument($"--user-data-dir={tempProfileDir}");

                driver = new ChromeDriver(service, options);
            }
            else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
            {
                InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                serv.HideCommandPromptWindow = true;

                var options1 = new InternetExplorerOptions();

                driver = new InternetExplorerDriver(serv, options1);
            }
            else if (strBrowser.Contains("MSEdge"))
            {
                ////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                ////serv.HideCommandPromptWindow = true;

                ////var options3 = new EdgeOptions();

                ////driver = new EdgeDriver(serv, options3);
                //var service = EdgeDriverService.CreateDefaultService();
                //service.HideCommandPromptWindow = true;

                //var options = new EdgeOptions();
                //options.AddArgument("start-maximized");
                //options.AddArgument("disable-web-security");
                //options.AddArgument("no-proxy-server");
                //options.AddArgument("no-sandbox");
                //options.AddArgument("disable-blink-features=AutomationControlled");

                ////string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                ////options.AddArgument($"--user-data-dir={tempProfileDir}");
                //// Ensure the folder is not already locked or in use
                //string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                //if (Directory.Exists(tempProfileDir))
                //{
                //    try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
                //}
                //Directory.CreateDirectory(tempProfileDir); // Forcefully create it

                //options.AddArgument($"--user-data-dir={tempProfileDir}");

                //driver = new EdgeDriver(service, options);

                string driverPath = Path.Combine(Application.StartupPath);//, "Drivers");

                var service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe");
                service.HideCommandPromptWindow = true;

                var options = new EdgeOptions();

                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--no-proxy-server");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-blink-features=AutomationControlled");

                string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempProfileDir);

                options.AddArgument($"--user-data-dir={tempProfileDir}");

                driver = new EdgeDriver(service, options);
            }
            else
            {
                cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
                return;
            }
            ////====================================================
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

            ////IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            ////inputTextBox.SendKeys(txtUserID.Text.Trim());

            ////new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            ////// System.Threading.Thread.Sleep(1000);
            ////new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            ////inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            ////inputTextBox.SendKeys(txtPassword.Text.Trim());

            ////driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            ////System.Threading.Thread.Sleep(1000);

            ///
            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            inputTextBox.SendKeys(txtUserID.Text.Trim());

            System.Threading.Thread.Sleep(2000);
            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            System.Threading.Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            inputTextBox.SendKeys(txtPassword.Text.Trim());

            //driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            System.Threading.Thread.Sleep(4000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();
            //@@
            System.Threading.Thread.Sleep(3000);
            var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog modal-dialog-centered')]"));

            if (modalDialogs.Count > 6)
            {
                // Look for the 'Login Here' button inside the modal
                var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

                if (loginHereButton.Displayed && loginHereButton.Enabled)
                {
                    loginHereButton.Click();
                }
            }

            ////System.Threading.Thread.Sleep(3000);
            ////var submenuElement = driver.FindElement(By.XPath("//a[contains(text(),'Go to Dashboard')]"));
            ////submenuElement.Click();
            //@@

            System.Threading.Thread.Sleep(2000);
            //====================================================
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Actions action = new Actions(driver);

            //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File"))); //-- 2023/11/28
            //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("//span[mat-button-wrapper]"))); //-- 2023/11/28
            action.MoveToElement(Menu).Build().Perform();

            var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
            //action.MoveToElement(Firstmenu).Build().Perform();
            action.MoveToElement(Firstmenu).Perform();

            var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'View Filed Forms')]")));
            SubmenuElement.Click();
            // FY 2026-27 onwards > New Act 2025
            //if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2026_27ID)
            //{
            //    var tab2025 = wait.Until(ExpectedConditions.ElementToBeClickable(
            //        By.XPath("//span[contains(text(),'Forms as per Income Tax Act 2025')]")));

            //    tab2025.Click();
            //}
            //else
            //{
            //    var tab1961 = wait.Until(ExpectedConditions.ElementToBeClickable(
            //        By.XPath("//span[contains(text(),'Forms as per Income Tax Act 1961')]")));

            //    tab1961.Click();
            //}
            //==================================================== 
        }
        #endregion

        #region CmbUploadType_SelectedIndexChanged
        private void CmbUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbUploadType.Text == "Regular")
            //{
            //    grpReceiptNo.Enabled = false;
            //    txtOriginalRRR.Text = "";
            //    txtPreviousRRR.Text = "";
            //}
            //else
            //{
            //    grpReceiptNo.Enabled = true;
            //}
        }
        #endregion


        



        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstUserIdHelpList.Visible == true)
                {
                    lstUserIdHelpList.Focus();
                    lstUserIdHelpList.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstUserIdHelpList.Visible = false;
            }
            //else
            //    //if (TdsMan.gTANNoPANNoValidation(txtUserID, e, T_TANPAN.TAN) == false)
            //    e.Handled = true;
        }

        #endregion

        #region txtTAN_KeyDown
        private void txtTAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                if (lstUserIdHelpList.Visible == true)
                {
                    lstUserIdHelpList.Focus();
                    lstUserIdHelpList.SelectedIndex = 0;
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
                if (txtUserID.Text.Trim() == "")
                {
                    lstUserIdHelpList.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_AADHAAR_ID," +
                         "       TAN_NO," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_AADHAAR " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_AADHAAR_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstUserIdHelpList.Visible = false;
                    return;
                }
                else
                {
                    lstUserIdHelpList.Items.Clear();
                    //lstUserIdHelpList.Height = 15;
                    lstUserIdHelpList.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstUserIdHelpList.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                  Convert.ToInt32(drdShowDeducteeHelp["TAN_AADHAAR_ID"])));
                        //--
                        //if (lstUserIdHelpList.Height <= 300)
                        //    lstUserIdHelpList.Height = lstUserIdHelpList.Height + 19;
                    }
                    //--
                    if (lstUserIdHelpList.Items.Count <= 0)
                        lstUserIdHelpList.Visible = false;
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
            //cmbFAYear_SelectedIndexChanged(sender, e);

            //if (txtTAN.Text.Trim() == "") return;
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
                lstUserIdHelpList.Visible = false;
                txtUserID.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //--
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstUserIdHelpList, lstUserIdHelpList.SelectedIndex));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_AADHAAR_ID = " + lngDeducteeId));
            //--
            lstUserIdHelpList.Visible = false;
            //--
        }
        #endregion


        #region BtnView_Click
        private void BtnView_Click(object sender, EventArgs e)
        {
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                BtnExit.Select();
                return;
            }
            if (txtUserID.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter TAN");
                txtUserID.Select();
                return ;
            }
            if (txtPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid Password");
                txtPassword.Select();
                return ;
            }
            //--
            strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";
            int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            if (iCount == 0)
            {
                //insering new record in the tan login master
                strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
                         "VALUES( '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                         "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                dmlService.J_ExecSql(strSQL);
                //--
            }
            else
            {
                //updating the existing record in the master

                strSQL = "UPDATE MST_TAN_AADHAAR " +
                         "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                         "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                         "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "' ";

                dmlService.J_ExecSql(strSQL);
            }
            //--
            OnlineViewReturn();
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0107", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

