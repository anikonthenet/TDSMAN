
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
using System.Diagnostics;
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

//using Newtonsoft.Json;

using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnUploadFVU : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnUploadFVU()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

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
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //txtPAN.Text = "BROPK6848J";
                //lblTitle.Text = "Know Your TAN"; 
                lblTitle.Text = "Upload TDS/TCS";// using Aadhaar (using OTP)";


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
            //grpDetails.Visible = false;

            txtUserID.Text = "";
            txtPassword.Text = "";
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear ) == false) return;
            //-----------
            //-- FORM
            //-----------
            //string[] strForm = { "24Q (Salary)", "26Q (Other than salary)", "27Q (Non Resident-Other than Salary)", "27EQ (TCS)" };
            string[] strForm = { T_FormNo.F138_24Q + " (" + T_FormNo.F24Q + ") Salary", T_FormNo.F140_26Q + " (" + T_FormNo.F26Q + ") Other than salary", T_FormNo.F144_27Q + " (" + T_FormNo.F27Q + ") Non Resident-Other than Salary", T_FormNo.F143_27EQ + " (" + T_FormNo.F27EQ + ") TCS"};
            dmlService.J_PopulateComboBox(strForm, ref cmbForm);
            //-----------
            //-- QUARTER
            //-----------
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //-----------
            //-- COMPANY
            //-----------
            //strSQL = " SELECT COMPANY_ID," +
            //    "             COMPANY_NAME " +
            //    "      FROM   MST_COMPANY " +
            //    "      WHERE  INACTIVE_FLAG = 0 " +
            //    "      ORDER BY COMPANY_NAME";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------
            //-----------
            //-- RETURN TYPE
            //-----------
            string[] strReturnType = { "Regular", "Correction" };
            dmlService.J_PopulateComboBox(strReturnType, ref cmbUploadType, 1);
            //--
            txtOriginalRRR.Text = "";
            txtPreviousRRR.Text = "";
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

        #region OnlineFileReturn_20250704
        private void OnlineFileReturn_20250704()
        {
            string tempUserDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempUserDir);

            string strOS = TdsMan.GetOSVersion();
            //--
            string strBrowser = "";
            if (strOS == "XP")
                strBrowser = TdsMan.GetSystemDefaultBrowserXP();
            else
                strBrowser = TdsMan.GetSystemDefaultBrowser();
            //--
            IWebDriver driver = null;//new ChromeDriver();
            //====================================================
            if (strBrowser.Contains("chrome"))
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                //options.AddAdditionalCapability("useAutomationExtension", false);
                options.AddExcludedArgument("enable-automation");

                //// options.AddArgument("--window-position=-32000,-32000");
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--no-proxy-server");
                options.AddArgument("--no-sandbox");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                options.AddUserProfilePreference("disable-popup-blocking", true);

                options.AddArgument("--disable-blink-features=AutomationControlled");

                options.AddArgument($"--user-data-dir={tempUserDir}");

                driver = new ChromeDriver(service, options);
                ////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////return;
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
                //EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                //serv.HideCommandPromptWindow = true;


                //var options3 = new EdgeOptions();

                //driver = new EdgeDriver(serv, options3);

                var service = EdgeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new EdgeOptions();
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--no-proxy-server");
                options.AddArgument("--no-sandbox");
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--disable-blink-features=AutomationControlled");

                // Add unique user-data-dir
                options.AddArgument($"--user-data-dir={tempUserDir}");

                driver = new EdgeDriver(service, options);
            }
            else
            {
                cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
                return;
            }
            //====================================================
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");
            //driver.Navigate().GoToUrl("https://www.tdsmanonline.com/Login.aspx");
            //System.Threading.Thread.Sleep(10000);

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            //driver.SwitchTo().Alert().Accept();
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

            if (modalDialogs.Count > 2)
            {
                // Look for the 'Login Here' button inside the modal
                var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

                if (loginHereButton.Displayed && loginHereButton.Enabled)
                {
                    loginHereButton.Click();
                }
            }

            System.Threading.Thread.Sleep(3000);
            var SubmenuElement = driver.FindElement(By.XPath("//a[contains(text(),'Go to Dashboard')]"));
            SubmenuElement.Click();
            //@@

            System.Threading.Thread.Sleep(2000);
            //====================================================
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Actions action = new Actions(driver);

            action.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();

            //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File"))); //-- 2023/11/28
            action.MoveToElement(Menu).Build().Perform();

            var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
            //action.MoveToElement(Firstmenu).Build().Perform();
            action.MoveToElement(Firstmenu).Perform();

            var submenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'File Income Tax Forms')]")));
            submenuElement.Click();
            //====================================================          
            //-- 2024/05/01
            //--------Items per page
            System.Threading.Thread.Sleep(2000);
            //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@class='mat-paginator-navigation-last mat-icon-button ng-star-inserted'])"))).Click();
            new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@class='mat-mdc-tooltip-trigger mat-mdc-paginator-navigation-last mdc-icon-button mat-mdc-icon-button mat-unthemed mat-mdc-button-base ng-star-inserted'])"))).Click();
            System.Threading.Thread.Sleep(2000);
            //--
            new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();
            //-- 2024/10/03
            //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[3]"))).Click();
            //
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();
            //
            string strFormName = cmbForm.Text.Split('(', ')')[1];
            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + cmbForm.Text + "')]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'" + strFormName + "')]")).Click();
            //
            //System.Threading.Thread.Sleep(2000);
            //--------Financial Year
            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(),'" + cmbFinancialYear.Text + "')]")).Click();

            //--------Quater
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-3"))).Click();
            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'" + cmbQuarter.Text + "')]")).Click();
            //
            if (cmbUploadType.Text == "Regular")
            {
                // FOR REGULAR 
                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                //
                System.Threading.Thread.Sleep(2000);
                //action.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();

                //System.Threading.Thread.Sleep(2000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("attachfile"))).Click();
            }
            else 
            {
                //FOR CORRECTION
                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-4"))).Click();

                //System.Threading.Thread.Sleep(2000);
                //action.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();

                System.Threading.Thread.Sleep(2000);

                //inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + txtOriginalRRR.Text + "')]"));
                //inputTextBox.SendKeys("RRR No");

                //inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + txtPreviousRRR.Text + "')]"));
                //inputTextBox.SendKeys("Previous RRR No");

                inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @id='origRrrNo')]"));
                inputTextBox.SendKeys(txtOriginalRRR.Text);

                //System.Threading.Thread.Sleep(1000);
                inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @id='prevRrrNo')]"));
                inputTextBox.SendKeys(txtPreviousRRR.Text);

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("attachfile"))).Click();
            }

        }
        #endregion

        #region OnlineFileReturn_
        private void OnlineFileReturn_()
        {
            string strOS = TdsMan.GetOSVersion();
            string strBrowser = "";
            if (strOS == "XP")
                strBrowser = TdsMan.GetSystemDefaultBrowserXP();
            else
                strBrowser = TdsMan.GetSystemDefaultBrowser();

            IWebDriver driver = null; string tempUserDataDir = null;

            try
            {
                if (strBrowser.Contains("chrome"))
                {
                    //driver = new ChromeDriver(service, options);
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    var options = new ChromeOptions();
                    //options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddExcludedArgument("enable-automation");

                    //// options.AddArgument("--window-position=-32000,-32000");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-web-security");
                    options.AddArgument("--no-proxy-server");
                    options.AddArgument("--no-sandbox");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);

                    options.AddUserProfilePreference("disable-popup-blocking", true);

                    options.AddArgument("--disable-blink-features=AutomationControlled");

                    driver = new ChromeDriver(service, options);
                }
                else if (strBrowser.Contains("MSEdge"))
                {
                    EdgeDriverService service = EdgeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;
                    service.EnableVerboseLogging = true; // Enable detailed logs

                    var options = new EdgeOptions();
                    //options.UseChromium = true;
                    options.AddAdditionalOption("useAutomationExtension", false);
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-blink-features=AutomationControlled");
                    options.AddArgument("--disable-notifications");
                    options.AddArgument("--disable-sync");
                    options.AddArgument("--disable-features=SignIn");
                    options.AddArgument("--disable-gpu");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    options.AddUserProfilePreference("disable-popup-blocking", true);
                    options.AddUserProfilePreference("autofill.profile_enabled", false);
                    options.AddUserProfilePreference("browser.signin", 0);
                    options.AddUserProfilePreference("sync.disabled", true);
                    options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");

                    // Generate a unique user data directory
                    tempUserDataDir = Path.Combine(Path.GetTempPath(), "EdgeProfile_" + Guid.NewGuid().ToString());
                    options.AddArgument($"--user-data-dir={tempUserDataDir}");

                    // Terminate any existing Edge or EdgeDriver processes
                    foreach (var process in Process.GetProcessesByName("msedge"))
                    {
                        process.Kill();
                    }
                    foreach (var process in Process.GetProcessesByName("msedgedriver"))
                    {
                        process.Kill();
                    }

                    driver = new EdgeDriver(service, options);
                }
                else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                {
                    InternetExplorerDriverService service = InternetExplorerDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    var options = new InternetExplorerOptions();
                    driver = new InternetExplorerDriver(service, options);
                }
                else
                {
                    cmnService.J_UserMessage("Please set your default browser to 'Chrome', 'Edge', or 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs.");
                    return;
                }

                // Set implicit wait and navigate to the website
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
                driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

                // Login process
                IWebElement inputTextBox = driver.FindElement(By.XPath("//input[@name='panAdhaarUserId']"));
                inputTextBox.SendKeys(txtUserID.Text.Trim());

                System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

                System.Threading.Thread.Sleep(1000); // Consider replacing with WebDriverWait
                new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[@id='passwordCheckBox']"))).Click();

                System.Threading.Thread.Sleep(2000);
                inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
                inputTextBox.SendKeys(txtPassword.Text.Trim());

                System.Threading.Thread.Sleep(4000); // Consider replacing with WebDriverWait
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();
                //@@
                System.Threading.Thread.Sleep(3000);
                var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog modal-dialog-centered')]"));

                if (modalDialogs.Count > 2)
                {
                    // Look for the 'Login Here' button inside the modal
                    var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

                    if (loginHereButton.Displayed && loginHereButton.Enabled)
                    {
                        loginHereButton.Click();
                    }
                }

                System.Threading.Thread.Sleep(3000);
                var SubmenuElement = driver.FindElement(By.XPath("//a[contains(text(),'Go to Dashboard')]"));
                SubmenuElement.Click();
                //@@

                System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                Actions action = new Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();

                var menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File")));
                action.MoveToElement(menu).Build().Perform();

                var firstMenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
                action.MoveToElement(firstMenu).Perform();

                var subMenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'File Income Tax Forms')]")));
                subMenuElement.Click();

                // Additional navigation and form selection
                System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@class='mat-paginator-navigation-last mat-icon-button ng-star-inserted'])"))).Click();
                new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@class='mat-mdc-tooltip-trigger mat-mdc-paginator-navigation-last mdc-icon-button mat-mdc-icon-button mat-unthemed mat-mdc-button-base ng-star-inserted'])"))).Click();

                System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[3]"))).Click();

                System.Threading.Thread.Sleep(3000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();

                string strFormName = cmbForm.Text.Split('(', ')')[1];
                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
                driver.FindElement(By.XPath($"//span[contains(text(),'{strFormName}')]")).Click();

                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
                System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbFinancialYear.Text}')]")).Click();

                //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-3"))).Click();
                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbQuarter.Text}')]")).Click();
                System.Threading.Thread.Sleep(2000);

                if (cmbUploadType.Text == "Regular")
                {
                    new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                    System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                    action.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();
                    System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                }
                else
                {
                    new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                    System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                    action.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();
                    System.Threading.Thread.Sleep(2000); // Consider replacing with WebDriverWait
                    inputTextBox = driver.FindElement(By.XPath($"//input[@id='{txtOriginalRRR.Text}']"));
                    inputTextBox.SendKeys("RRR No");
                    inputTextBox = driver.FindElement(By.XPath($"//input[@id='{txtPreviousRRR.Text}']"));
                    inputTextBox.SendKeys("Previous RRR No");
                }
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage($"An error occurred: {ex.Message}");
            }
            finally
            {
                driver?.Quit(); // Ensure the browser is closed
            }
        }
        #endregion

        #region OnlineFileReturn_old
        private void OnlineFileReturn_old()
        {
            string strOS = TdsMan.GetOSVersion();
            string strBrowser = strOS == "XP"
                                ? TdsMan.GetSystemDefaultBrowserXP()
                                : TdsMan.GetSystemDefaultBrowser();

            IWebDriver driver = null;

            try
            {
                if (strBrowser.Contains("chrome"))
                {
                    ChromeOptions options = new ChromeOptions();

                    options.AddArgument("--incognito"); // Temporary clean session
                    options.AddArgument("--disable-web-security");
                    options.AddArgument("--no-proxy-server");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-blink-features=AutomationControlled");
                    options.AddExcludedArgument("enable-automation");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

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
                    EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                    serv.HideCommandPromptWindow = true;
                    var options3 = new EdgeOptions();
                    driver = new EdgeDriver(serv, options3);
                }
                else
                {
                    cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
                    return;
                }

                // ==== Validate driver ====
                if (driver == null)
                {
                    MessageBox.Show("Browser driver could not be launched.");
                    return;
                }

                // ==== Main automation ====
                driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

                System.Threading.Thread.Sleep(1000);
                IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId"));
                inputTextBox.SendKeys(txtUserID?.Text?.Trim() ?? "");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
                System.Threading.Thread.Sleep(2000);

                inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
                inputTextBox.SendKeys(txtPassword?.Text?.Trim() ?? "");

                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

                System.Threading.Thread.Sleep(3000);
                var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog')]"));
                if (modalDialogs.Count < 6)
                {
                    var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));
                    if (loginHereButton.Displayed && loginHereButton.Enabled)
                        loginHereButton.Click();
                }

                System.Threading.Thread.Sleep(3000);
                Actions action = new Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.PageUp).Perform();

                var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File")));
                action.MoveToElement(Menu).Perform();

                System.Threading.Thread.Sleep(2000);
                var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
                action.MoveToElement(Firstmenu).Perform();

                System.Threading.Thread.Sleep(2000);
                var SubmenuElementNew = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'File Income Tax Forms')]")));
                SubmenuElementNew.Click();

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'mat-mdc-paginator-navigation-last')]"))).Click();

                System.Threading.Thread.Sleep(3000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();

                System.Threading.Thread.Sleep(3000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Proceed')]"))).Click();

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbForm?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbFinancialYear?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbQuarter?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                if (cmbUploadType.Text == "Regular")
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                    System.Threading.Thread.Sleep(2000);
                    action.SendKeys(OpenQA.Selenium.Keys.PageDown).Perform();
                    System.Threading.Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                }
                else
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-4"))).Click();
                    System.Threading.Thread.Sleep(1000);

                    inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='origRrrNo']"));
                    inputTextBox.SendKeys(txtOriginalRRR?.Text ?? "");

                    System.Threading.Thread.Sleep(1000);
                    inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='prevRrrNo']"));
                    inputTextBox.SendKeys(txtPreviousRRR?.Text ?? "");

                    System.Threading.Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during upload:\n" + ex.Message, "Upload TDS/TCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    driver?.Quit();
                }
                catch { /* ignore cleanup errors */ }
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
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                // options.AddAdditionalCapability("useAutomationExtension", false);
                options.AddExcludedArgument("enable-automation");

                //// options.AddArgument("--window-position=-32000,-32000");
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-web-security");
                options.AddArgument("--no-proxy-server");
                options.AddArgument("--no-sandbox");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                driver = new ChromeDriver(service, options);
                ////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////return;
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
                EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                serv.HideCommandPromptWindow = true;

                var options3 = new EdgeOptions();

                driver = new EdgeDriver(serv, options3);
            }
            else
            {
                cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
                return;
            }
            ////====================================================
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            inputTextBox.SendKeys(txtUserID.Text.Trim());

            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            // System.Threading.Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            inputTextBox.SendKeys(txtPassword.Text.Trim());

            driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            System.Threading.Thread.Sleep(1000);
            //====================================================
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Actions action = new Actions(driver);

            var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            action.MoveToElement(Menu).Build().Perform();

            var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
            //action.MoveToElement(Firstmenu).Build().Perform();
            action.MoveToElement(Firstmenu).Perform();

            var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'View Filed Forms')]")));
            SubmenuElement.Click();
            //====================================================          

            //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[10]"))).Click();

            //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();

            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + cmbForm.Text + "')]")).Click();

            ////System.Threading.Thread.Sleep(1000);
            ////--------Financial Year
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + cmbFinancialYear.Text + "')]")).Click();

            ////--------Quater
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-3"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + cmbQuarter.Text + "')]")).Click();

            //if (cmbUploadType.Text == "Regular")
            //{
            //    // FOR REGULAR 
            //    new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
            //}
            //else
            //{
            //    //FOR CORRECTION
            //    new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-4"))).Click();

            //    inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + txtOriginalRRR.Text + "')]"));
            //    inputTextBox.SendKeys("RRR No");

            //    inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + txtPreviousRRR.Text + "')]"));
            //    inputTextBox.SendKeys("Previous RRR No");
            //}

        }
        #endregion

        #region OnlineFileReturn
        private void OnlineFileReturn()
        {
            string strOS = TdsMan.GetOSVersion();
            string strBrowser = strOS == "XP"
                                ? TdsMan.GetSystemDefaultBrowserXP()
                                : TdsMan.GetSystemDefaultBrowser();

            IWebDriver driver = null;

            try
            {
                if (strBrowser.Contains("chrome"))
                {
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
                    ////////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                    ////////serv.HideCommandPromptWindow = true;
                    ////////var options3 = new EdgeOptions();
                    ////////driver = new EdgeDriver(serv, options3);
                    ////var service = EdgeDriverService.CreateDefaultService();
                    ////service.HideCommandPromptWindow = true;

                    ////var options = new EdgeOptions();
                    ////options.AddArgument("start-maximized");
                    ////options.AddArgument("disable-web-security");
                    ////options.AddArgument("no-proxy-server");
                    ////options.AddArgument("no-sandbox");
                    ////options.AddArgument("disable-blink-features=AutomationControlled");

                    //////string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    //////options.AddArgument($"--user-data-dir={tempProfileDir}");
                    ////// Ensure the folder is not already locked or in use
                    ////string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                    ////if (Directory.Exists(tempProfileDir))
                    ////{
                    ////    try { Directory.Delete(tempProfileDir, true); } catch { /* Ignore any cleanup errors */ }
                    ////}
                    ////Directory.CreateDirectory(tempProfileDir); // Forcefully create it

                    ////options.AddArgument($"--user-data-dir={tempProfileDir}");

                    ////driver = new EdgeDriver(service, options);
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
                    cmnService.J_UserMessage("Please set your default browser to 'Chrome' / 'Edge' / 'Internet Explorer'.");
                    return;
                }

                // ==== Validate driver ====
                if (driver == null)
                {
                    MessageBox.Show("Browser driver could not be launched.");
                    return;
                }

                // ==== Main automation ====
                driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

                System.Threading.Thread.Sleep(3000);
                ////////IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId")); //-- 2026/01/26
                IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
                inputTextBox.SendKeys(txtUserID?.Text?.Trim() ?? "");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
                System.Threading.Thread.Sleep(2000);

                inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
                inputTextBox.SendKeys(txtPassword?.Text?.Trim() ?? "");

                System.Threading.Thread.Sleep(3000);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();


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

                System.Threading.Thread.Sleep(3000);
                Actions action = new Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.PageUp).Perform();

                var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File")));
                action.MoveToElement(Menu).Perform();

                System.Threading.Thread.Sleep(2000);
                var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
                action.MoveToElement(Firstmenu).Perform();

                System.Threading.Thread.Sleep(2000);
                var SubmenuElementNew = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'File Income Tax Forms')]")));
                SubmenuElementNew.Click();
                //--
                // FY 2026-27 onwards > New Act 2025
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2026_27ID)
                {
                    var tab2025 = wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("//span[contains(text(),'Forms as per Income Tax Act 2025')]")));

                    tab2025.Click();
                }
                else
                {
                    var tab1961 = wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("//span[contains(text(),'Forms as per Income Tax Act 1961')]")));

                    tab1961.Click();
                }

                System.Threading.Thread.Sleep(2000); // allow tab load
                //--
                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'mat-mdc-paginator-navigation-last')]"))).Click();

                System.Threading.Thread.Sleep(3000);
                ////wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();
                // Wait for list to load after tab selection
                wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[contains(text(),'File Now')]")));

                // Click File Now button
                var fileNowBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//span[contains(text(),'File Now')]")));

                fileNowBtn.Click();
                //System.Threading.Thread.Sleep(3000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Proceed')]"))).Click();

                //System.Threading.Thread.Sleep(3000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();

                System.Threading.Thread.Sleep(3000);
                //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();
                var getStartedBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(@class,'primaryButton') and contains(.,'Get Started')]")));
                getStartedBtn.Click();
                //
                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbForm?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbFinancialYear?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-3"))).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath($"//span[contains(text(),'{cmbQuarter?.Text ?? ""}')]")).Click();

                System.Threading.Thread.Sleep(2000);
                if (cmbUploadType.Text == "Regular")
                {
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-0"))).Click();
                    System.Threading.Thread.Sleep(2000);
                    //action.SendKeys(OpenQA.Selenium.Keys.PageDown).Perform();
                    //System.Threading.Thread.Sleep(2000);
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("attachfile"))).Click();
                }
                else
                {
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-4"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-1"))).Click();
                    System.Threading.Thread.Sleep(1000);

                    inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='origRrrNo']"));
                    inputTextBox.SendKeys(txtOriginalRRR?.Text ?? "");

                    System.Threading.Thread.Sleep(1000);
                    inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='prevRrrNo']"));
                    inputTextBox.SendKeys(txtPreviousRRR?.Text ?? "");

                    System.Threading.Thread.Sleep(2000);
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("attachfile"))).Click();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during upload:\n" + ex.Message, "Upload TDS/TCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    //try
            //    //{
            //    //    driver?.Quit();
            //    //}
            //    //catch { /* ignore cleanup errors */ }
            //}
        }
        #endregion

        #region CmbUploadType_SelectedIndexChanged
        private void CmbUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUploadType.Text == "Regular")
            {
                grpReceiptNo.Enabled = false;
                txtOriginalRRR.Text = "";
                txtPreviousRRR.Text = "";
            }
            else if (cmbUploadType.Text == "Correction")
            {
                grpReceiptNo.Enabled = true;                
            }
            //
            GetOriginalRRR();
        }
        #endregion


        #region BtnGo_Click
        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                BtnExit.Select();
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
            OnlineFileReturn();
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
            //else if (Convert.ToInt64(e.KeyChar) == 27)
            //{
            //    lstUserIdHelpList.Visible = false;
            //}
            //else
             //    if (TdsMan.gTANNoPANNoValidation(txtUserID, e, T_TANPAN.TAN) == false)
                //e.Handled = true;
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

        #region ValidateFields
        public bool ValidateFields()
        {
            // ------------------------
            // -- TAN
            // ------------------------

            if (txtUserID.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid User ID");
                txtUserID.Select();
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid Password");
                txtPassword.Select();
                return false;
            }
            //---------------------------
            // ------------------------
            // -- FINANCIAL YEAR
            // ------------------------
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Financial year");
                cmbFinancialYear.Select();
                return false;
            }
            // ------------------------
            // -- FORM NO
            // ------------------------
            if (cmbForm.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Form No");
                cmbForm.Select();
                return false;
            }
            // ------------------------
            // -- QUARTER
            // ------------------------
            if (cmbQuarter.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Quarter");
                cmbQuarter.Select();
                return false;
            }
            //--
            if (cmbUploadType.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Upload Type");
                cmbUploadType.Select();
                return false;
            }

            if (cmbUploadType.Text == "Correction")
            {
                if (txtOriginalRRR.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter Original RRR");
                    txtOriginalRRR.Select();
                    return false;
                }
                if (txtPreviousRRR.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter Previous RRR");
                    txtPreviousRRR.Select();
                    return false;
                }
            }            
            return true;
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

        #region GetOriginalRRR
        private void GetOriginalRRR()
        {
            //-- 2024/12/19
            txtOriginalRRR.Text = "";
            //
            if (cmbUploadType.Text == "Correction")
            {
                long lngCompanyID = 0;
                if (txtUserID.Text != "")
                {
                    //lngCompanyID = Convert.ToInt64(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM MST_COMPANY WHERE TAN_NO = '" + txtUserID.Text + "'")));
                    object result = dmlService.J_ExecSqlReturnScalar("SELECT COMPANY_ID FROM MST_COMPANY WHERE TAN_NO = '" + txtUserID.Text + "'");
                    if (result != null && result != DBNull.Value && !string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        long.TryParse(result.ToString(), out lngCompanyID);
                    }
                    else
                    {
                        lngCompanyID = 0; // or handle as needed
                    }
                }
                //
                string strQtr = "";
                if (cmbQuarter.Text != "")
                    strQtr = cmbQuarter.Text;
                //
                //string strFormNo = "";
                //if (cmbForm.Text != "")
                //    strFormNo = cmnService.J_Left(cmbForm.Text, 4).Trim();
                string strFormNo = cmbForm.Text.Split('(', ')')[1];
                //
                long lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                strQtr,
                                                lngCompanyID,
                                                strFormNo);
                //
                txtOriginalRRR.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                //--
            }
        }
        #endregion


        #region CmbFinancialYear_SelectedIndexChanged
        private void CmbQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            GetOriginalRRR();
        }
        #endregion

        #region CmbFinancialYear_SelectedIndexChanged
        private void CmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            //
            GetOriginalRRR();
        }
        #endregion

        #region CmbForm_SelectedIndexChanged
        private void CmbForm_SelectedIndexChanged(object sender, EventArgs e)
        {

            //
            GetOriginalRRR();
        }
        #endregion
    }
}

