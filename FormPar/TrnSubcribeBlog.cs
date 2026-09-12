#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
//using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormWeb;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormSys;
//using TDSMAN.FormPar;
using TDSMAN.FormBrowser;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

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

namespace TDSMAN.FormPar
{
    public partial class TrnSubcribeBlog : Form
    {
        #region System Generated Code
        public TrnSubcribeBlog()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
        //
        mdiTDSMAN mdiTDSMANForm = new mdiTDSMAN();
        #endregion

        #region btnSubmit_Click
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // CHECK '^'
            if (TdsMan.T_DetectCaret(txtEmailId.Text.Trim(), "^") == true)
            {
                cmnService.J_UserMessage("Email id - '^' not allowed");
                txtEmailId.Select();
                return;
            }
            //--
            if (txtEmailId.ToString().Trim() == "")
            {
                cmnService.J_UserMessage("Email id - can not be Blank");
                txtEmailId.Select();
                return;
            }
            //
            if (txtEmailId.ToString().Trim() != "")
            {
                if (TdsMan.T_CheckEmailFormat(txtEmailId.Text) == false)
                {
                    txtEmailId.Select();
                    return;
                }
            }
            //--
            //if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN.\nA link will open in your default browser, please respond to complete subscription request.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            if (cmnService.J_UserMessage("Subscribe to TDSMAN Blog.\nProceed ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //
            //--https://feedburner.google.com/fb/a/mailverify?uri=Tdsman&email=bhowmikarup@gmail.com
            //System.Diagnostics.Process.Start("https://feedburner.google.com/fb/a/mailverify?uri=Tdsman&email=" + txtEmailId.Text.Trim());
            //TrnSubscribeBlogMimibrowser objBrow = new TrnSubscribeBlogMimibrowser();
            TrnSubscribeBlogSendy objBrow = new TrnSubscribeBlogSendy(); //-- 2026/02/16
            objBrow.Email = txtEmailId.Text;
            objBrow.Show();
            //--
            #region OPERATION - commented

            //IWebDriver driver = null;
            ////TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
            //string strBrowser = TdsMan.GetSystemDefaultBrowser();
            //if (strBrowser.Contains("chrome"))
            //{
            //    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            //    service.HideCommandPromptWindow = true;

            //    var options = new ChromeOptions();
            //    // options.AddAdditionalCapability("useAutomationExtension", false);
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
            //    EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
            //    serv.HideCommandPromptWindow = true;

            //    var options3 = new EdgeOptions();

            //    driver = new EdgeDriver(serv, options3);
            //}

            ////driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            //driver.Navigate().GoToUrl("https://f05baafb.sibforms.com/serve/MUIFAKnCSEhdwLD9QjsnFVVinQK6WLqLSHM3IHlCEcVwrVxI6ATpA215H-XYVetkNNwteDMAy85d9SEjBKlh_6AI2D5mD4Owl_tzxGt33Zwe4JDTTVZioHW3pMtLSyjpfRqt7DFgjvFczquS1oOJvG8LjLe4WP6P7kII0POaLULnAmdW5D6cAfWGoAgJUbBNKFmKLWMAhCxbZLqi");
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));


            //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='EMAIL')]"));
            //inputTextBox.SendKeys(txtEmailId.Text);

            //var NewPayment = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='sib-form-block__button sib-form-block__button-with-loader']")));
            //NewPayment.Click();

            ////var subscribeButton = driver.FindElement(By.CssSelector("button[type='submit']")); // Adjust the selector as necessary
            ////subscribeButton.Click();

            //// Optional: Wait for some time to observe the result
            //System.Threading.Thread.Sleep(2000); // Wait for 5 seconds

            //// Close the browser
            ////driver.Quit();



            ////}

            //driver.Close(); driver.Dispose();


            //this.Close();
            //this.Dispose();
            #endregion
            //--
            Registration.SaveBlogDetails(TDSMAN.Classes.TDSMAN.T_pVersionType, TDSMAN.Classes.TDSMAN.T_pTDSMANSerialNo.ToString(), txtEmailId.Text.Trim());
            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
            {
                //--
                string strTrialVersionRegistryPath = TdsMan.GetRegistryFolder() + "(Trial)";
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, strTrialVersionRegistryPath,
                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                T_YES_NO.YES.ToString());
            }
            else if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
            {
                cmnService.J_CreateRegistryKey(TDSMAN.Classes.TDSMAN.T_pCompanyName, TdsMan.GetRegistryFolder(),
                                                T_RegistrationInfo.Blog_email_flag.ToString(),
                                                T_YES_NO.YES.ToString());
            }
            //--
            cmnService.J_UserMessage("Thank You for Subscribing.");
            //--
            this.Close();
            //--
        }
        #endregion


        #region lnkVisitBlog_LinkClicked
        private void lnkVisitBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://blog.tdsman.com/");
        }
        #endregion

        private void TrnSubcribeBlog_Activated(object sender, EventArgs e)
        {
            //wbrSubscribeBlog.Navigate("https://madmimi.com/signups/e1750fa434344ab1b1bd3efb4772bc8c/join");
        }

        private void TrnSubcribeBlog_Load(object sender, EventArgs e)
        {
            //txtEmailId.Text = "sumanta.ghosh@pdsinfotech.com";
            //txtEmailId.Text = "anikonthenet@gmail.com";
            //txtEmailId.Text = "sgupta@jayasoftwares.com";
        }
    }
}
