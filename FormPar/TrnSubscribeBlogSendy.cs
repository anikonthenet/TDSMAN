
#region Refered Namespaces & Classes 
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Http;


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
    public partial class TrnSubscribeBlogSendy : Form
    {
        int i = 0;
        public string Email { get; set; }


        #region TrnSubscribeBlogMimibrowser
        public TrnSubscribeBlogSendy()
        {
            InitializeComponent();
        }
        #endregion


        #region Mimibrowser_Load
        private async void Mimibrowser_Load(object sender, EventArgs e)
        {
            #region COMMENT
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
            ////
            //webBrowser1.ScriptErrorsSuppressed = true;
            //////////webBrowser1.Navigate("https://madmimi.com/signups/e1750fa434344ab1b1bd3efb4772bc8c/join");
            ////webBrowser1.Navigate("https://f05baafb.sibforms.com/serve/MUIFAKnCSEhdwLD9QjsnFVVinQK6WLqLSHM3IHlCEcVwrVxI6ATpA215H-XYVetkNNwteDMAy85d9SEjBKlh_6AI2D5mD4Owl_tzxGt33Zwe4JDTTVZioHW3pMtLSyjpfRqt7DFgjvFczquS1oOJvG8LjLe4WP6P7kII0POaLULnAmdW5D6cAfWGoAgJUbBNKFmKLWMAhCxbZLqi"); //-- 2024/08/09
            ////webBrowser1.Navigate(Application.StartupPath + "/brevo-form.html");
            //webBrowser1.Navigate(Application.StartupPath + "/mailerlite.html");
            //
            #region OPERATION

            //IWebDriver driver = null;
            //TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
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
            //inputTextBox.SendKeys(this.Email);

            //var NewPayment = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='sib-form-block__button sib-form-block__button-with-loader']")));
            //NewPayment.Click();

            ////var subscribeButton = driver.FindElement(By.CssSelector("button[type='submit']")); // Adjust the selector as necessary
            ////subscribeButton.Click();

            //// Optional: Wait for some time to observe the result
            //System.Threading.Thread.Sleep(1500); // Wait for 5 seconds

            //// Close the browser
            ////driver.Quit();



            ////}

            //driver.Close();driver.Dispose();


            //this.Close();
            //this.Dispose();
            #endregion
            //
            #endregion
            //
            var client = new HttpClient();
            //var request = new HttpRequestMessage(
            //    HttpMethod.Post,
            //    "https://sendy.pdsinfotech-india.com/subscribe"
            //);
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://tdsmanemail.com/newsletter/subscribe"
            );           

            var collection = new List<KeyValuePair<string, string>>();

            collection.Add(new KeyValuePair<string, string>(
                "api_key", "Ul6t8D3bLp45C3F07D37"
            ));

            collection.Add(new KeyValuePair<string, string>(
                "email", this.Email//  "anikghosh@pdsinfotech.com"
            ));

            //collection.Add(new KeyValuePair<string, string>(
            //    "list", "IeG763j2YDSv3RB892O1UfvFCw"
            //));
            collection.Add(new KeyValuePair<string, string>(
                            "list", "jyY763rDZfBkuN6f9AoRllSQ"
                        ));

            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (i == 0)
            //{
            //    webBrowser1.Document.GetElementById("signup_email").SetAttribute("value", this.Email);
            //    webBrowser1.Document.GetElementById("webform_submit_button").InvokeMember("click");
            //    System.Threading.Thread.Sleep(1500);
            //}
            //this.Close();
        }
        #endregion


    }
}
