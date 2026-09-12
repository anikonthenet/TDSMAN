
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
using TDSMAN.FormBrowser;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;

using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnDirectLogin : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnDirectLogin()
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
                lblTitle.Text = "Direct Login";
                //
                if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
                {
                    txtPassword.UseSystemPasswordChar = true;
                    txtTracesPassword.UseSystemPasswordChar = true;
                }
                //--
                Clearcontrols();
                //
                txtTracesTANNo.Select();
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
            //
            txtTracesTANNo.Text = "";
            txtTracesUserId.Text = "";
            txtTracesPassword.Text = "";
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

                ////options.AddArgument("--disable-blink-features=AutomationControlled");

                ////driver = new ChromeDriver(service, options);
                ////////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////////return;
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

            //////IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            //////inputTextBox.SendKeys(txtUserID.Text.Trim());

            //////new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            //////new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            //////inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            //////inputTextBox.SendKeys(txtPassword.Text.Trim());

            ////////driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            //////System.Threading.Thread.Sleep(3000);
            //////WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ////////wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();
            //////wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();

            //////System.Threading.Thread.Sleep(10000);
            ////////
            ////////driver.Close();
            ////////driver.Dispose();
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

            System.Threading.Thread.Sleep(2000);

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

        #region LoginTraces
        private void LoginTraces()
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
                //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                //service.HideCommandPromptWindow = true;

                //var options = new ChromeOptions();
                //// options.AddAdditionalCapability("useAutomationExtension", false);
                //options.AddExcludedArgument("enable-automation");

                ////// options.AddArgument("--window-position=-32000,-32000");
                //options.AddArgument("--start-maximized");
                //options.AddArgument("--disable-web-security");
                //options.AddArgument("--no-proxy-server");
                //options.AddArgument("--no-sandbox");
                //options.AddUserProfilePreference("credentials_enable_service", false);
                //options.AddUserProfilePreference("profile.password_manager_enabled", false);

                //driver = new ChromeDriver(service, options);
                //////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                //////////return;
                #endregion
                //
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
            //#region COMMENTED
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            driver.Navigate().GoToUrl("https://www.tdscpc.gov.in/app/login.xhtml");
            //driver.Navigate().GoToUrl("https://traces.tdscpc.gov.in/auth/login/loginScreen");

            IWebElement inputTextBoxuserId = driver.FindElement(By.XPath("//input[(@name='username')]"));
            inputTextBoxuserId.SendKeys(txtTracesUserId.Text.Trim());

            IWebElement inputTextBoxpsw = driver.FindElement(By.XPath("//input[(@name='j_password')]"));
            inputTextBoxpsw.SendKeys(txtTracesPassword.Text.Trim());

            IWebElement inputTextBoxtanpan = driver.FindElement(By.XPath("//input[(@name='j_tanPan')]"));
            inputTextBoxtanpan.SendKeys(txtTracesTANNo.Text.Trim());

            IWebElement inputTextBoxCaptcha = driver.FindElement(By.XPath("//input[(@name='j_captcha')]"));
            inputTextBoxCaptcha.SendKeys("");
            //driver.FindElement(By.XPath("//span[contains(text(),'clickLogin')]")).Click();
            //
            //driver.Close();
            //driver.Dispose();
            //#endregion
            //driver.Navigate().GoToUrl("https://traces.tdscpc.gov.in/auth/login/loginScreen");
            //System.Threading.Thread.Sleep(7000);

            //Actions act = new Actions(driver);
            //IWebElement body = driver.FindElement(By.TagName("body"));

            //// Step 1: Click near USER ID area (adjust coordinates if needed)
            //act.MoveToElement(body, 600, 350).Click().Perform();
            //System.Threading.Thread.Sleep(1000);

            //// Step 2: Enter User ID (TAN)
            //act.SendKeys(txtTracesTANNo.Text.Trim()).Perform();
            //System.Threading.Thread.Sleep(800);

            //// Step 3: TAB > Password
            //act.SendKeys(OpenQA.Selenium.Keys.Tab).Perform();
            //System.Threading.Thread.Sleep(500);

            //// Password
            //act.SendKeys(txtTracesPassword.Text.Trim()).Perform();
            //System.Threading.Thread.Sleep(800);

            //// Step 4: TAB > Captcha (skip radio if needed)
            //act.SendKeys(OpenQA.Selenium.Keys.Tab).Perform();
            //System.Threading.Thread.Sleep(500);
            //act.SendKeys(OpenQA.Selenium.Keys.Tab).Perform();
            //System.Threading.Thread.Sleep(500);

            //// Now captcha
            //MessageBox.Show("Enter CAPTCHA manually and press Login");
        }
        #endregion


        #region txtTracesTANNo_KeyPress
        private void txtTracesTANNo_KeyPress(object sender, KeyPressEventArgs e)
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
                if (TdsMan.gTANNoPANNoValidation(txtTracesTANNo, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }

        #endregion

        #region txtTracesTANNo_KeyDown
        private void txtTracesTANNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region txtTracesTANNo_TextChanged
        private void txtTracesTANNo_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (txtTracesTANNo.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    drdShowDeducteeHelp.Close();
                    drdShowDeducteeHelp.Dispose();
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    //lstDeducteeHelp.Height = 15;
                    //lstDeducteeHelp.Visible = true;
                    ShowTANHelp();
                    while (drdShowDeducteeHelp.Read())
                    {
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

        #region txtTracesTANNo_Leave
        private void txtTracesTANNo_Leave(object sender, EventArgs e)
        {
            if (txtTracesTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region ShowTANHelp
        private void ShowTANHelp()
        {
            Point screenPoint = txtTracesTANNo.PointToScreen(
                new Point(0, txtTracesTANNo.Height));

            Point parentPoint =
                lstDeducteeHelp.Parent.PointToClient(screenPoint);

            lstDeducteeHelp.Location = parentPoint;

            // Keep the help list aligned with your UI
            lstDeducteeHelp.Width = Math.Min(
                700,
                lstDeducteeHelp.Parent.ClientSize.Width - parentPoint.X - 10);

            lstDeducteeHelp.Height = 100;

            lstDeducteeHelp.BringToFront();
            lstDeducteeHelp.Visible = true;
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
                txtTracesTANNo.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;
            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTracesTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtTracesUserId.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtTracesPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
            //txtCaptchaCode.Select();
        }
        #endregion

        #region lstDeducteeHelp_KeyPress
        private void lstUserIdHelpList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstUserIdHelpList_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstUserIdHelpList.Visible = false;
                txtUserID.Select();
            }
        }
        #endregion

        #region lstUserIdHelpList_Click
        private void lstUserIdHelpList_Click(object sender, EventArgs e)
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


        #region txtUserID_KeyPress
        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
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

        #region txtUserID_KeyDown
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
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

        #region txtUserID_TextChanged
        private void txtUserID_TextChanged(object sender, EventArgs e)
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



        #region BtnView_Click
        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                if (rbnIT.Checked == true)
                {
                    #region IT
                    if (txtUserID.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter TAN");
                        txtUserID.Select();
                        return;
                    }
                    if (txtPassword.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter a valid Password");
                        txtPassword.Select();
                        return;
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
                    //
                    return;
                    #endregion
                }
                else if (rbnTraces.Checked == true)
                {
                    #region Traces
                    if (string.IsNullOrEmpty(txtTracesTANNo.Text))
                    {
                        cmnService.J_UserMessage("Please enter TAN");
                        txtTracesTANNo.Focus();
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtTracesUserId.Text))
                    //{
                    //    cmnService.J_UserMessage("Please enter User ID");
                    //    txtTracesUserId.Focus();
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtTracesPassword.Text))
                    {
                        cmnService.J_UserMessage("Please enter Password");
                        txtTracesPassword.Focus();
                        return;
                    }
                    //--
                    //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER 
                    strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "' ";
                    int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    if (iCount == 0)
                    {
                        //insering new record in the tan login master
                        strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                                 "VALUES( '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "', " +
                                 "        '" + cmnService.J_ReplaceQuote(txtTracesUserId.Text) + "', " +
                                 "        '" + cmnService.J_ReplaceQuote(txtTracesPassword.Text) + "')";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "INNER JOIN MST_COMPANY " +
                                 "ON    MST_TAN_ACCOUNT.TAN_NO       = MST_COMPANY.TAN_NO " +
                                 "SET   MST_TAN_ACCOUNT.COMPANY_NAME = MST_COMPANY.COMPANY_NAME " +
                                 "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "INNER JOIN COR_HDR_COMPANY " +
                                 "ON    MST_TAN_ACCOUNT.TAN_NO       = COR_HDR_COMPANY.TAN_NO " +
                                 "SET   MST_TAN_ACCOUNT.COMPANY_NAME = COR_HDR_COMPANY.COMPANY_NAME " +
                                 "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "SET    COMPANY_NAME = '<NOT AVAILABLE>' " +
                                 "WHERE  COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                    }
                    else
                    {
                        //-- Updating the existing record in the master
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "', " +
                                 "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtTracesUserId.Text) + "', " +
                                 "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtTracesPassword.Text) + "' " +
                                 "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTracesTANNo.Text) + "' ";
                        dmlService.J_ExecSql(strSQL);
                    }
                    //#################
                    //--
                    //LoginTraces();
                    TracesLogin objLogIn = new TracesLogin();
                    CorrectionData objData = new CorrectionData();
                    objLogIn.TAN = txtTracesTANNo.Text;
                    objLogIn.Password = txtTracesPassword.Text;
                    //===========================================================
                    objData.TracesLogin = objLogIn;
                    TrnTracesDirectLoginBrowser objBrowser = new TrnTracesDirectLoginBrowser(objData);
                    objBrowser.MdiParent = TrnTracesDirectLoginBrowser.ActiveForm;
                    objBrowser.Show();
                    //--
                    return;
                    #endregion
                }
            }
            catch(Exception err)
            {
                if (rbnIT.Checked == true)
                {

                }
                else if (rbnTraces.Checked == true)
                {

                }

            }
        }
        #endregion

        #region rbnTraces_CheckedChanged
        private void rbnTraces_CheckedChanged(object sender, EventArgs e)
        {
            Clearcontrols();
            if (rbnTraces.Checked==true)
            {
                grpTraces.Visible = true;
                grpIT.Visible = false;
                txtTracesTANNo.Select();
            }
            else if (rbnIT.Checked==true)
            {
                grpTraces.Visible = false;
                grpIT.Visible = true;
                txtUserID.Select();
            }
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0094", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            // Quick test in your Main method
            //var apiClient = new ChallanApiClient("https://www.tdsman.com");

            //// Test API health first
            //bool isHealthy = await apiClient.CheckHealthAsync();
            //Console.WriteLine($"API Health: {(isHealthy ? "OK" : "Failed")}");

            //if (isHealthy)
            //{
            //    var request = new ChallanRequest
            //    {
            //        Uid = "YOUR_TAN",
            //        Password = "YOUR_PASSWORD",
            //        FromDate = "2025-07-04",
            //        ToDate = "2025-07-06",
            //        MaxRecords = 15
            //    };

            //    var response = await apiClient.ExtractChallanDataAsync(request);
            //    Console.WriteLine($"Success: {response.Success}");
            //    Console.WriteLine($"Records: {response.RecordsExtracted}");
            //}
        }
    }
}

