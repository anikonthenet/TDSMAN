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
using System.Threading;

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

namespace TDSMAN.FormBrowser
{
    public partial class TrnUploadTDSBrowser : Form
    {

        int i = 1;

        //CommonService cmnService = new CommonService();

        #region System Generated Code
        public TrnUploadTDSBrowser()
        {
            InitializeComponent();
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

        #region TrnUploadTDS_Load
        private void TrnUploadTDS_Load(object sender, EventArgs e)
        {
            //int BrowserVer, RegVal;
            //// get the installed IE version
            //using (WebBrowser Wb = new WebBrowser())
            //    BrowserVer = Wb.Version.Major;

            //// set the appropriate IE version
            //if (BrowserVer >= 11)
            //    RegVal = 11001;
            //else if (BrowserVer == 10)
            //    RegVal = 10001;
            //else if (BrowserVer == 9)
            //    RegVal = 9999;
            //else if (BrowserVer == 8)
            //    RegVal = 8888;
            //else
            //    RegVal = 7000;

            //// set the actual key
            //using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree))
            //    if (Key.GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe") == null)
            //        Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);

            //if (!WBEmulator.IsBrowserEmulationSet())
            //{
            //    WBEmulator.SetBrowserEmulationVersion();
            //}
            ////
            //webBrowser1.ScriptErrorsSuppressed = true;
            //webBrowser1.Navigate("https://www.incometaxindiaefiling.gov.in/help/");
            OnlineFileReturn();
        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (i == 1)
            //{
            //    try
            //    {
            //        await Task.Delay(1000);
            //        HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("A");
            //        foreach (HtmlElement link in links)  // this ex is given another SO post 
            //        {
            //            if (link.InnerText == "Login")
            //            {
            //                link.InvokeMember("click");
            //                i++;
            //            }
            //        }
            //    }
            //    catch (Exception EX) { }
            //}
            //else    if (i == 2)
            //{
            //    await Task.Delay(1000);
            //    //Thread.Sleep(90000);
            //    //query
            //    //webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", "CALP08143C");
            //    //webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", "Pdsinfo@6");
            //    webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
            //    webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
            //    i++;
            //}
            //else if (i == 3)
            //{
            //    await Task.Delay(1000);
            //    bool flagloginfail = false;
            //    HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("li");
            //    foreach (HtmlElement link in links)  // this ex is given another SO post 
            //    {
            //        HtmlElementCollection linksIN = link.GetElementsByTagName("A");
            //        foreach (HtmlElement lin in linksIN)  // this ex is given another SO post 
            //        {
            //            //MessageBox.Show(lin.InnerText.Trim());
            //            if (lin.InnerText.Trim() == "Upload TDS")
            //            {
            //                lin.InvokeMember("click");
            //                i++;
            //                flagloginfail = true;
            //                break;
            //            }
            //        }
            //    }
            //    if (i == 3)
            //    {
            //        await Task.Delay(1000);
            //        HtmlElementCollection inputs = webBrowser1.Document.GetElementsByTagName("input");
            //        foreach (HtmlElement input in inputs)
            //        {
            //            String value = input.GetAttribute("value");
            //            if (value == "Forced Login")
            //            {
            //                input.InvokeMember("click");
            //                flagloginfail = true;
            //                break;
            //            }
            //        }
            //    }
            //    if (flagloginfail == false)
            //    {
            //        await Task.Delay(1000);
            //        webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
            //        webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
            //    }
            //}
            //else if (i == 4)
            //{
            //    await Task.Delay(1000);
            //    //query
            //    //webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_fvuVersion").SetAttribute("value", "FVU 5.8");
            //    //webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_finYr").SetAttribute("value", "201718");
            //    //webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_formName").SetAttribute("value", "26Q");
            //    //webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_period").SetAttribute("value", "Q4");
            //    //webBrowser1.Document.GetElementById("UploadType").SetAttribute("value", "R");
            //    if (TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling!= "")
            //        webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_fvuVersion").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling);
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling != "")
            //        webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_finYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling.Replace("-", ""));
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling != "")
            //        webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_formName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling);
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling != "")
            //        webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_period").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling);
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling != "")
            //    {
            //        //webBrowser1.Document.GetElementById("uploadType").InvokeMember("click");
            //        webBrowser1.Document.GetElementById("UploadType").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling);
            //    }
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling == "C") //-- for CORRECTION RETURN ONLY
            //    {
            //        webBrowser1.Document.GetElementById("orgRRR").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_OriginalPRNTypeOnlineFilling);
            //        webBrowser1.Document.GetElementById("prevRRR").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PreviousPRNTypeOnlineFilling);
            //    }
            //    //
            //    if (TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling != "" &&
            //        TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling != "" &&
            //        TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling != "" &&
            //        TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling != "" &&
            //        TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling != "")
            //        webBrowser1.Document.GetElementById("UploadTdsParamValidate_0").InvokeMember("click");
            //    i++;
            //}
            //else if (i == 5)
            //{
            //    await Task.Delay(1000);
            //    TMDILOG.Interval = 50;
            //    TMDILOG.Start();
            //    //  file.InvokeMember("Click");
            //    HtmlElement fileelem = webBrowser1.Document.GetElementById("UploadTdsReturn_fileUploadBean_file");
            //    if(TDSMAN.Classes.TDSMAN.T_UploadFilePath.Trim() != "")
            //        fileelem.SetAttribute("value", TDSMAN.Classes.TDSMAN.T_UploadFilePath);
            //    fileelem.InvokeMember("Click");

            //    if (webBrowser1.Document.GetElementById("UploadTdsReturn_fileUploadBean_file").GetAttribute("value") != "")
            //    {
            //        //--
            //        webBrowser1.Document.GetElementById("displayEverifyDtls").InvokeMember("click");
            //        webBrowser1.Document.GetElementById("generate_aadhaar_otp_forms").InvokeMember("click");
            //        webBrowser1.Document.GetElementById("generate_nsubmit_aadhaar_otp_forms").InvokeMember("click");
            //        webBrowser1.Document.GetElementById("UpdateContactDtls_2").InvokeMember("click");
            //        //UpdateContactDtls_2
            //        i++;
            //    }
            //}
            //else if (i == 6)
            //{
            //    await Task.Delay(1000);
            //    //HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("A");
            //    //foreach (HtmlElement link in links)  // this ex is given another SO post 
            //    //{
            //    //    if (link.InnerText == "Logout")
            //    //    {
            //    //        this.Close();
            //    //    }
            //    //}
            //    webBrowser1.Document.GetElementById("Logout").InvokeMember("click");
            //}
        }
        #endregion

        #region TMDILOG_Tick
        private void TMDILOG_Tick(object sender, EventArgs e)
        {
            try
            {
                TMDILOG.Stop();
                SendKeys.SendWait(TDSMAN.Classes.TDSMAN.T_UploadFilePath); // enter the file path, which suppose to upload.
                SendKeys.SendWait("{TAB 2}");
                SendKeys.SendWait("{ENTER}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region TrnUploadTDSBrowser_FormClosing
        private void TrnUploadTDSBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cmnService.J_UserMessage("It is recommended to [Logout] first if not, then close the window.\nDo you want to still close it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }
        #endregion

        #region OnlineFileReturn
        private void OnlineFileReturn()
        {
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
            //if (strBrowser.Contains("chrome"))
            //{
            //    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            //    service.HideCommandPromptWindow = true;

            //    var options = new ChromeOptions();
            //    //options.AddAdditionalCapability("useAutomationExtension", false);
            //    options.AddExcludedArgument("enable-automation");

            //    //// options.AddArgument("--window-position=-32000,-32000");
            //    options.AddArgument("--start-maximized");
            //    options.AddArgument("--disable-web-security");
            //    options.AddArgument("--no-proxy-server");
            //    options.AddArgument("--no-sandbox");
            //    options.AddUserProfilePreference("credentials_enable_service", false);
            //    options.AddUserProfilePreference("profile.password_manager_enabled", false);

            //    options.AddUserProfilePreference("disable-popup-blocking", true);

            //    options.AddArgument("--disable-blink-features=AutomationControlled");

            //    driver = new ChromeDriver(service, options);
            //    ////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
            //    ////////return;
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
            //    //EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
            //    //serv.HideCommandPromptWindow = true;


            //    //var options3 = new EdgeOptions();

            //    //driver = new EdgeDriver(serv, options3);

            //    string driverPath = Path.Combine(Application.StartupPath);//, "Drivers");

            //    var service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe");
            //    service.HideCommandPromptWindow = true;

            //    var options = new EdgeOptions();

            //    options.AddArgument("--start-maximized");
            //    options.AddArgument("--disable-web-security");
            //    options.AddArgument("--no-proxy-server");
            //    options.AddArgument("--no-sandbox");
            //    options.AddArgument("--disable-blink-features=AutomationControlled");

            //    string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
            //    Directory.CreateDirectory(tempProfileDir);

            //    options.AddArgument($"--user-data-dir={tempProfileDir}");

            //    driver = new EdgeDriver(service, options);
            //}
            //else
            //{
            //    cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
            //    return;
            //}
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

            //====================================================
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");
            //driver.Navigate().GoToUrl("https://www.tdsmanonline.com/Login.aspx");
            //System.Threading.Thread.Sleep(10000);

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            //driver.SwitchTo().Alert().Accept();
            //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            //inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_TANOnlineFilling.Trim());

            //System.Threading.Thread.Sleep(2000);
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            //System.Threading.Thread.Sleep(1000);
            //new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            //inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            //inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling.Trim());

            ////driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            //System.Threading.Thread.Sleep(4000);
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ////wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();

            //System.Threading.Thread.Sleep(2000);
            ////====================================================
            ////WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //Actions action = new Actions(driver);

            //action.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();

            //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            //action.MoveToElement(Menu).Build().Perform();

            //var Firstmenu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Income Tax Forms')]")));
            ////action.MoveToElement(Firstmenu).Build().Perform();
            //action.MoveToElement(Firstmenu).Perform();

            //var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'File Income Tax Forms')]")));
            //SubmenuElement.Click();
            ////====================================================          

            //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[10]"))).Click();

            //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();

            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling + "')]")).Click();

            ////System.Threading.Thread.Sleep(2000);
            ////--------Financial Year
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
            //System.Threading.Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling.Replace("-", "") + "')]")).Click();

            ////--------Quater
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-3"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling + "')]")).Click();

            System.Threading.Thread.Sleep(3000);
            ////////IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId")); //-- 2026/01/26
            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            //inputTextBox.SendKeys(txtUserID?.Text?.Trim() ?? "");
            inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_TANOnlineFilling.Trim());

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
            System.Threading.Thread.Sleep(2000);

            inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
            //inputTextBox.SendKeys(txtPassword?.Text?.Trim() ?? "");
            inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling.Trim());

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
            // FY 2026-27 onwards > New Act 2025
            long lngFAYearId = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID FROM MST_ASSESSMENT WHERE FA_YEAR = '" + TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling + "'")));
            
            if (lngFAYearId >= T_FinancialYearID.F2026_27ID)
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

            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'mat-mdc-paginator-navigation-last')]"))).Click();

            System.Threading.Thread.Sleep(3000);
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();

            //System.Threading.Thread.Sleep(3000);
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Proceed')]"))).Click();

            //System.Threading.Thread.Sleep(3000);
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[@class='textFileList'])[2]"))).Click();
            // Wait for list to load after tab selection
            wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//span[contains(text(),'File Now')]")));

            // Click File Now button
            var fileNowBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(),'File Now')]")));

            fileNowBtn.Click();
            System.Threading.Thread.Sleep(3000);
            //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='largeButton primaryButton endButtons mrgnBottom mt-4']"))).Click();
            var getStartedBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(@class,'primaryButton') and contains(.,'Get Started')]")));
            getStartedBtn.Click();

            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("formTypeCode"))).Click();
            driver.FindElement(By.XPath($"//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling + "')]")).Click();

            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("financialYear"))).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling + "')]")).Click();

            System.Threading.Thread.Sleep(2000);
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-3"))).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[contains(text(),'" + TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling + "')]")).Click();

            if (TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling == "R")
            {
                //// FOR REGULAR 
                //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-3"))).Click();
                ////
                //System.Threading.Thread.Sleep(2000);
                //action.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();

                //System.Threading.Thread.Sleep(2000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
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
                ////FOR CORRECTION
                //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-4"))).Click();

                //inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + TDSMAN.Classes.TDSMAN.T_OriginalPRNTypeOnlineFilling + "')]"));
                //inputTextBox.SendKeys("RRR No");

                //inputTextBox = driver.FindElement(By.XPath("//input[(@id='" + TDSMAN.Classes.TDSMAN.T_PreviousPRNTypeOnlineFilling + "')]"));
                //inputTextBox.SendKeys("Previous RRR No");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-radio-1"))).Click();
                System.Threading.Thread.Sleep(1000);

                inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='origRrrNo']"));
                inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_OriginalPRNTypeOnlineFilling);

                System.Threading.Thread.Sleep(1000);
                inputTextBox = driver.FindElement(By.XPath("//input[@type='text' and @id='prevRrrNo']"));
                inputTextBox.SendKeys(TDSMAN.Classes.TDSMAN.T_PreviousPRNTypeOnlineFilling);

                System.Threading.Thread.Sleep(2000);
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Attach file')]"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("attachfile"))).Click();
            }

        }
        #endregion

        //public void doit(object sender, HtmlElementEventArgs e)
    }
}
