#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;



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
    public partial class TrnViewReturnStatusOnline : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnViewReturnStatusOnline()
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

        //--
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TracesConnect objAccount = new TracesConnect();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--            
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //----
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

        #region TrnViewReturnStatusOnline_Load
        private void TrnViewReturnStatusOnline_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            lblTitle.Text = "View your Submitted Return Online";
            //
            //-----------
            //-- COMPANY
            //-----------
            string strSQL = @"SELECT MAX(ID),
                                     COMPANY 
                              FROM (
                                     SELECT COMPANY_ID AS ID,
                                            COMPANY_NAME + ' [' + TAN_NO + ']' AS COMPANY
                                     FROM   MST_COMPANY                                      
                                     WHERE  INACTIVE_FLAG = 0
                                     UNION 
                                     SELECT COR_TRN_COMPANY.BATCH_HEADER_ID AS ID,
                                            COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']' AS COMPANY
                                     FROM COR_TRN_COMPANY) AS ALL_COMPANIES
                              GROUP BY COMPANY
                              ORDER BY COMPANY";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;

            txtTokenNo.Text = "";
            //InitializeCaptcha();
            //-----------

        }
        #endregion

        #region txtTokenNo_KeyPress
        private void txtTokenNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericControl_KeyPress(sender, e, 15);
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            //----------------------------
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                BtnExit.Select();
                return;
            }
            //
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select the Company");
                cmbCompany.Select();
                return;
            }
            //
            if (txtTokenNo.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Enter the Token No. of the return for which you want to view the status");
                txtTokenNo.Select();
                return;
            }
            //
            //if (txtCaptchaCode.Text.Trim() == "")
            //{
            //    cmnService.J_UserMessage("Enter the Captcha");
            //    txtCaptchaCode.Select();
            //    return;
            //}
            //

            //////string url = "https://onlineservices.tin.nsdl.com/TIN/UnAuthorizedView.do?ID=1068688145&TAN=" + cmnService.J_Right(cmbCompany.Text, 10) +
            //////             "&PRN=" + txtTokenNo.Text + "";
            //string url = "https://onlineservices.tin.egov-nsdl.com/TIN/UnAuthorizedView.do?ID=1068688145&TAN=" + cmnService.J_Right(cmbCompany.Text, 10) +
            //             "&PRN=" + txtTokenNo.Text + "";

            //System.Diagnostics.Process.Start(url);

            try
            {
                #region COMMENT
                //string tan = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text, 11),10);
                //string prn = txtTokenNo.Text.Trim();
                //string captcha = txtCaptchaCode.Text.Trim();

                //string html = objAccount.SubmitProteanRequest(tan, prn, captcha);

                //if (html.Contains("Invalid Captcha") || html.Contains("Please enter"))
                //{
                //    cmnService.J_UserMessage("Invalid Captcha. Try again.");
                //    InitializeCaptcha();
                //    return;
                //}

                //string filePath = Path.Combine(Path.GetTempPath(), "TDSStatus.html");
                //File.WriteAllText(filePath, html);

                //Process.Start(new ProcessStartInfo
                //{
                //    FileName = filePath,
                //    UseShellExecute = true
                //});

                //string url = "https://onlineservices.tin.egov.proteantech.in/TIN/UnAuthorizedView.do?ID=1068688145"
                //     + "&TAN=" + tan
                //     + "&PRN=" + prn;

                //Process.Start(new ProcessStartInfo
                //{
                //    FileName = url,
                //    UseShellExecute = true
                //});
                #endregion
                //
                string strTAN = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text, 11), 10);
                string strPRN = txtTokenNo.Text.Trim();
                string strOS = TdsMan.GetOSVersion();
                string strBrowser = strOS == "XP"
                                    ? TdsMan.GetSystemDefaultBrowserXP()
                                    : TdsMan.GetSystemDefaultBrowser();

                IWebDriver driver = null;
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
                    //////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                    //////serv.HideCommandPromptWindow = true;
                    //////var options3 = new EdgeOptions();
                    //////driver = new EdgeDriver(serv, options3);
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
                driver.Navigate().GoToUrl("https://onlineservices.tin.egov.proteantech.in/TIN/UnAuthorizedView.do");
                
                System.Threading.Thread.Sleep(1000);
                //--
                IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='TAN')]"));
                inputTextBox.SendKeys(strTAN?.Trim() ?? "");

                //--
                inputTextBox = driver.FindElement(By.XPath("//input[(@name='PRN')]"));
                inputTextBox.SendKeys(strPRN?.Trim() ?? "");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                IWebElement captchaBox = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.Name("captchaText"))
                );

                captchaBox.Click();   // Cursor will blink there
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(ex.Message);
            }
            //----------------------------
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region GetTCS_TDS_Statement
        public static string GetTCS_TDS_Statement()
        {

            int intFirstIndex = 0;
            string strServerResponse = "";
            int intLast = 0;
            string strID = "";
            //--------------------------------------
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://onlineservices.tin.nsdl.com/TIN/JSP/tds/linktoUnAuthorizedInput.jsp");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            //--------------------------------------
            strServerResponse = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
            //--------------------------------------
            intFirstIndex = strServerResponse.IndexOf("TIN/UnAuthorizedView.do");

            if (intFirstIndex >= 0)
                intLast = strServerResponse.IndexOf("\"", intFirstIndex);

            if (intLast > 0)
                strID = strServerResponse.Substring(intFirstIndex, intLast - intFirstIndex);

            strID = strID.Substring(strID.LastIndexOf("=") + 1);
            //--------------------------------------

            return strID;

        }
        #endregion


        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnViewChallanInformationOnline");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region lnkSearchByTAN_MouseMove
        private void lnkSearchByTAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(lnkSearchByTAN, "Click here to search the Company by TAN");
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0121", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        

        private void BtnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            //InitializeCaptcha();
        }
    }
}

