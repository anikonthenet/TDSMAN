

#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using TDSMAN.Classes;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewChallanInformationOnline : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnViewChallanInformationOnline()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        //--
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--
        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;
        CookieContainer objContainer = new CookieContainer();

        //--            
        //----
        #endregion

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "View/Download Challan Information Online";
            //
            BtnRefresh.Visible = true;
            //-----------
            //-- COMPANY
            //-----------
//            string strSQL = @"SELECT MAX(ID),
//                                     COMPANY 
//                              FROM (
//                                     SELECT COMPANY_ID AS ID,
//                                            COMPANY_NAME + ' - ' + TAN_NO AS COMPANY
//                                     FROM   MST_COMPANY 
//                                     WHERE  INACTIVE_FLAG = 0
//                                     UNION 
//                                     SELECT COR_TRN_COMPANY.BATCH_HEADER_ID AS ID,
//                                            COR_TRN_COMPANY.COMPANY_NAME + ' - ' + COR_TRN_COMPANY.TAN_NO  AS COMPANY
//                                     FROM COR_TRN_COMPANY) AS ALL_COMPANIES
//                              GROUP BY COMPANY
//                              ORDER BY COMPANY";
            string strSQL = @"SELECT MAX(ID),
                                     COMPANY 
                              FROM (SELECT COMPANY_ID AS ID,
                                            COMPANY_NAME + ' [' + TAN_NO + ']' AS COMPANY
                                     FROM   MST_COMPANY 
                                     WHERE  INACTIVE_FLAG = 0
                                     UNION 
                                     SELECT COR_TRN_COMPANY.BATCH_HEADER_ID AS ID,
                                            COR_TRN_COMPANY.COMPANY_NAME + ' [' + COR_TRN_COMPANY.TAN_NO + ']'  AS COMPANY
                                     FROM COR_TRN_COMPANY) AS ALL_COMPANIES
                              GROUP BY COMPANY
                              ORDER BY COMPANY";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------
            //--
            //-- 2018/11/15 - ANIK
            //if (TDSMAN.Classes.TDSMAN.T_ENABLE_NSDL_TEXT_CAPS_LOCK == true)
            //    txtCaptchaCode.CharacterCasing = CharacterCasing.Upper;      
            //else
            //    txtCaptchaCode.CharacterCasing = CharacterCasing.Normal; 
            //--
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
            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
                return;
            //
            if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
                mskViewFileDownloadFrom.Select();
                return;
            }
            //
            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
                return;
            //
            if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
                mskViewFileDownloadTo.Select();
                return;
            }
            //--
            if (dtService.J_IsDateGreater(ref mskViewFileDownloadFrom, ref mskViewFileDownloadTo, J_ShowMessage.YES) == false)
                return;
            //--
            // FROM DATE & TO DATE DURATION 24 MONTHS
            if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
            {
                cmnService.J_UserMessage("Period selected should be within 24 months");
                mskViewFileDownloadFrom.Select();
                return;
            }
            //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) +
            //              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2) +
            //              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +
            //              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) +
            //              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2) +
            //              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +
            //              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewFileDownloadTo.Text, 4) +
            //              //"&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
            //              "&submit=View Challan details";
            //System.Diagnostics.Process.Start(url);

            ////mskViewCSIFileDownloadFrom.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MIN(DEPOSIT_DATE) - 1 AS MIN_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
            ////mskViewCSIFileDownloadTo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(DEPOSIT_DATE) + 1 AS MAX_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
            //### -- ANIK - 2021/12/14
            //--
            string strOS = TdsMan.GetOSVersion();
            //--
            string strBrowser = "";
            if (strOS == "XP")
                strBrowser = TdsMan.GetSystemDefaultBrowserXP();
            else
                strBrowser = TdsMan.GetSystemDefaultBrowser();
            //--
            IWebDriver driver = null;
            //
            if (strBrowser.Contains("chrome"))
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
               // options.AddAdditionalCapability("useAutomationExtension", false);
                options.AddExcludedArgument("enable-automation");

                //// options.AddArgument("--window-position=-32000,-32000");
                //options.AddArgument("--start-maximized");

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
            //--
            //driver.Navigate().GoToUrl("https://tin.tin.nsdl.com/oltas/servlet/TanSearch");
         
            driver.Navigate().GoToUrl("https://tin.tin.nsdl.com/oltas/index.html");
                     
            driver.FindElement(By.XPath("//input[@type='submit' and @value='     TAN Based View      ']")).Click();

           // System.Threading.Thread.Sleep(5000);

            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='TEXT' and @name='TAN_NO')]"));
            inputTextBox.SendKeys(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_FROM_DT_DD')]"));
            inputTextBox.SendKeys(cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));

           // System.Threading.Thread.Sleep(1000);

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_FROM_DT_MM')]"));
            inputTextBox.SendKeys(getMonthName(cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2)));

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_FROM_DT_YY')]"));
            inputTextBox.SendKeys(cmnService.J_Right(mskViewFileDownloadFrom.Text, 4));

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_TO_DT_DD')]"));
            inputTextBox.SendKeys(cmnService.J_Left(mskViewFileDownloadTo.Text, 2));

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_TO_DT_MM')]"));
            inputTextBox.SendKeys(getMonthName(cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2)));

            inputTextBox = driver.FindElement(By.XPath("//select[(@name='TAN_TO_DT_YY')]"));
            inputTextBox.SendKeys(cmnService.J_Right(mskViewFileDownloadTo.Text, 4));

            #region Commented Code



            ////###
            //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11),10) +
            //              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2) +
            //              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +
            //              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) +
            //              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2) +
            //              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +
            //              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewFileDownloadTo.Text, 4) +
            //              //"&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
            //              "&submit=View Challan details";
            //System.Diagnostics.Process.Start(url);

            //var url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
            //var client = new WebClientCookies(objContainer);
            //var method = "POST"; // If your endpoint expects a GET then do it.
            //client.CookieContainer = objContainer;
            //var parameters = new System.Collections.Specialized.NameValueCollection();


            //parameters.Add("firstTime", "yes");
            //parameters.Add("submit", "     TAN Based View      ");
            //// parameters.Add("parameter3", "parameter 3 value.");

            ///* Always returns a byte[] array data as a response. */
            //var response_data = client.UploadValues(url, method, parameters);
            //var responseString = UnicodeEncoding.UTF8.GetString(response_data);
            ////
            //parameters.Clear();
            //parameters.Add("TAN_NO", cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
            //parameters.Add("TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
            //parameters.Add("TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
            //parameters.Add("TAN_FROM_DT_YY", cmnService.J_Right(mskViewFileDownloadFrom.Text, 4));
            //parameters.Add("TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
            //parameters.Add("TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));
            //parameters.Add("TAN_TO_DT_YY", cmnService.J_Right(mskViewFileDownloadTo.Text, 4));
            //parameters.Add("HID_IMG_TXT", txtCaptchaCode.Text.Trim());
            //parameters.Add("HIDDEN_TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
            //parameters.Add("HIDDEN_TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
            //parameters.Add("HIDDEN_TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
            //parameters.Add("HIDDEN_TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));
            ////
            //parameters.Add("HIDDEN_TAN_TO_DT_YY", "");
            ////--
            //parameters.Add("appUser", "T");
            //parameters.Add("submit", "View Challan details");

            //response_data = client.UploadValues(url, method, parameters);
            //client.Dispose();
            //----------------------------

            #endregion

        }
        #endregion

        //-- DOWNLOAD CSI FILE
        #region BtnRefresh_Click 
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnRefresh.Text == "&Download")
                {
                    BtnSave.Enabled = false;
                    grpCaptcha.Visible = true;
                    BtnRefresh.Text = "Continue";
                    setCaptchaCode();
                }
                else
                {
                    int iC = 0;
                    string strCSIFileDownloadFileMessage = "";
                    try
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
                        if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
                            return;
                        //
                        if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
                            mskViewFileDownloadFrom.Select();
                            return;
                        }
                        //
                        if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
                            return;
                        //
                        if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
                            mskViewFileDownloadTo.Select();
                            return;
                        }
                        //
                        if (dtService.J_IsDateGreater(ref mskViewFileDownloadFrom, ref mskViewFileDownloadTo, J_ShowMessage.YES) == false)
                            return;
                        // FROM DATE & TO DATE DURATION 24 MONTHS
                        if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
                        {
                            cmnService.J_UserMessage("Period selected should be within 24 months");
                            mskViewFileDownloadFrom.Select();
                            return;
                        }
                        //--
                        if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                        {
                            cmnService.J_UserMessage("Enter Captcha Code");
                            txtCaptchaCode.Select();
                            return;

                        }
                        //--
                        string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
                        if (strCSIDownloadPath == "")
                        {
                            MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            BtnExit.Select();
                            return;
                        }
                        //--
                        if (TdsMan.T_CheckInternetConnectivty() == false)
                        {
                            cmnService.J_UserMessage("Internet Connectivity not found");
                            BtnExit.Select();
                            return;
                        }
                        //--
                        // CSI_DOWNLOAD:
                        //string strCSIFileName = Path.Combine(Path.GetDirectoryName(strCSIDownloadPath), cmnService.J_Right(cmbCompany.Text.Trim(), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                        string strCSIFileName = Path.Combine(strCSIDownloadPath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                        //--
                        if (cmnService.J_IsFileExist(strCSIFileName) == true)
                        {
                            if (cmnService.J_UserMessage(Path.GetFileName(strCSIFileName) + " already exist.\nDo you want to replace it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                BtnExit.Select();
                                return;
                            }
                        }
                        //--
                        //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) +
                        //              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2) +
                        //              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +
                        //              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) +
                        //              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2) +
                        //              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +
                        //              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewFileDownloadTo.Text, 4) +
                        //              "&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
                        //              "&submit=Download Challan file";
                        //
                        //WebClientCookies client = new WebClientCookies(objContainer);
                        ////
                        //client.DownloadFile(new Uri(url), @"" + strCSIFileName); //-- ANIK 2016/01/11
                        //client.Dispose();
                        //                    

                        var url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
                        var client = new WebClientCookies(objContainer);
                        var method = "POST"; // If your endpoint expects a GET then do it.
                        client.CookieContainer = objContainer;
                        var parameters = new System.Collections.Specialized.NameValueCollection();
                        //
                        parameters.Add("firstTime", "yes");
                        parameters.Add("submit", "     TAN Based View      ");
                        // parameters.Add("parameter3", "parameter 3 value.");
                        /* Always returns a byte[] array data as a response. */
                        var response_data = client.UploadValues(url, method, parameters);
                        var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                        parameters.Clear();
                        parameters.Add("TAN_NO", cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
                        parameters.Add("TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
                        parameters.Add("TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
                        parameters.Add("TAN_FROM_DT_YY", cmnService.J_Right(mskViewFileDownloadFrom.Text, 4));
                        parameters.Add("TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
                        parameters.Add("TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));
                        parameters.Add("TAN_TO_DT_YY", cmnService.J_Right(mskViewFileDownloadTo.Text, 4));
                        parameters.Add("HID_IMG_TXT", txtCaptchaCode.Text.Trim());
                        parameters.Add("HIDDEN_TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
                        parameters.Add("HIDDEN_TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
                        parameters.Add("HIDDEN_TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
                        parameters.Add("HIDDEN_TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));

                        parameters.Add("HIDDEN_TAN_TO_DT_YY", "");
                        parameters.Add("appUser", "T");
                        parameters.Add("submit", "Download Challan file");// "Download Challan file");


                        // client.DownloadFile(new Uri(url), @"" + strCSIDownloadFilePath);

                        //parameters.Add("firstTime", "yes");
                        //parameters.Add("submit", "     TAN Based View      ");
                        // parameters.Add("parameter3", "parameter 3 value.");

                        /* Always returns a byte[] array data as a response. */
                        response_data = client.UploadValues(url, method, parameters);
                        client.Dispose();
                        // Parse the returned data (if any) if needed.
                        // var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                        File.WriteAllBytes(strCSIFileName, response_data);
                        FileInfo fInfo = new FileInfo(@"" + strCSIFileName);
                        if (fInfo.Exists)
                        {
                            long size = fInfo.Length;
                            //
                            if (size <= 0)
                            {


                            }
                            //--------------------------------------------------------
                            string text = System.IO.File.ReadAllText(@"" + strCSIFileName);
                            if (text.Contains("<HTML>") == true)
                            {
                                if (text.ToUpper().Contains("ERROR - TEXT DOES NOT MATCH. PLEASE ENTER NEW TEXT") == true)
                                {
                                    strCSIFileDownloadFileMessage = "Text does not match. Please enter new text";
                                    setCaptchaCode();

                                }
                                else if (text.ToUpper().Contains("RECORD NOT FOUND") == true)
                                {
                                    strCSIFileDownloadFileMessage = "No Record Found.";
                                    setCaptchaCode();
                                }
                                else if (text.ToUpper().Contains("SITE UNDER MAINTENANCE") == true)
                                {
                                    strCSIFileDownloadFileMessage = "Site Under Maintenance";
                                    setCaptchaCode();
                                }
                                else if (text.ToUpper().Contains("PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE") == true)
                                {
                                    strCSIFileDownloadFileMessage = "Site Under Maintenance";
                                    setCaptchaCode();
                                }
                                else
                                {
                                    strCSIFileDownloadFileMessage = "File Download Failed. Please Try again";
                                    setCaptchaCode();
                                }
                                //----------------------------------------
                                File.Delete(strCSIFileName);
                                //----------------------------------------
                            }
                            else
                            {
                                cmnService.J_UserMessage("CSI file downloaded");
                                System.Diagnostics.Process.Start(strCSIDownloadPath);
                                setCaptchaCode();
                                BtnExit.Select();
                            }

                        }
                        else
                        {
                            strCSIFileDownloadFileMessage = "File Download Failed. Please try again";
                            setCaptchaCode();
                        }
                        //
                        cmnService.J_UserMessage(strCSIFileDownloadFileMessage);
                        //--
                    }
                    catch (Exception err)
                    {
                        cmnService.J_UserMessage(err.Message);
                        BtnExit.Select();
                    }
                }
            }
            catch (Exception err)
            {
                BtnSave.Enabled = false;
                grpCaptcha.Visible = true;
                BtnRefresh.Text = "Continue";
                setCaptchaCode();
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (BtnRefresh.Text.ToUpper().Trim() == "CONTINUE")
            {
                grpCaptcha.Visible = false;
                lblBottomMessage.Text = "Security introduced by IT Dept. for CSI file download w.e.f. from Dec 2016. Enter correct text & continue.";            
                BtnSave.Enabled = true;
                BtnRefresh.Text = "&Download";
                txtCaptchaCode.Text = "";
                //
                txtCaptchaCode.Select();                 
            }
            else 
            {
              this.Close();
              this.Dispose();
            }
            
            
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskViewFileDownloadFrom_KeyPress
        private void mskViewFileDownloadFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskViewFileDownloadTo_KeyPress
        private void mskViewFileDownloadTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
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

        #region setCaptchaCode

        public void setCaptchaCode()
        {
            try
            {
                txtCaptchaCode.Text = "";
                //--
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //request = (HttpWebRequest)WebRequest.Create("https://tin.tin.nsdl.com/oltas/servlet/CaptchaServicetansearch");
                request = (HttpWebRequest)WebRequest.Create("https://tin.tin.proteantech.in/oltas/servlet/CaptchaServicetansearch");
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
                request.KeepAlive = true;
                request.Timeout = 100000;
                request.CookieContainer = objContainer;            
                //
                Stream imgStream = request.GetResponse().GetResponseStream();
                Image img = Image.FromStream(imgStream);
                this.picCaptcha.Image = img;
                //
                txtCaptchaCode.Select();
            }
            catch( Exception e)
            {
                //MessageBox.Show(e.Message);

            }



        }
        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            setCaptchaCode();
        }
        #endregion
        
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=fEDSUM29LU8&list=PLy1JUN9HgGMxvrbc8Zf2dVCTkf4FQEEjT");
        }
        #endregion


        #region getMonthName

       
        private string getMonthName(string strMonthCode)
        {
          string  strValue = "";

            Dictionary<string, string> _Months = new Dictionary<string, string>();
            _Months.Add("01", "JAN");
            _Months.Add("02", "FEB");
            _Months.Add("03", "MAR");
            _Months.Add("04", "APR");
            _Months.Add("05", "MAY");
            _Months.Add("06", "JUN");
            _Months.Add("07", "JUL");
            _Months.Add("08", "AUG");
            _Months.Add("09", "SEP");
            _Months.Add("10", "OCT");
            _Months.Add("11", "NOV");
            _Months.Add("12", "DEC");

            if (_Months.ContainsKey(strMonthCode))
            {
                strValue = _Months[strMonthCode];                
            }

            return strValue;
        }

        #endregion


    }
}

