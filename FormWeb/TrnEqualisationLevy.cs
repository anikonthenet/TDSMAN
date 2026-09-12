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

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
//
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

#endregion

namespace TDSMAN.FormWeb
{

    public partial class TrnEqualisationLevy : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnEqualisationLevy()
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
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--            
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //----
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
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
            lblTitle.Text = "Equalisation Levy - Challan no. / ITNS 285";
            //
            //-----------
            //-- COMPANY
            //-----------
            string strSQL = @"SELECT MAX(ID),
                                     COMPANY 
                              FROM (
                                     SELECT COMPANY_ID AS ID,
                                            COMPANY_NAME + ' - ' + TAN_NO  AS COMPANY
                                     FROM   MST_COMPANY                                      
                                     WHERE  INACTIVE_FLAG = 0
                                     UNION 
                                     SELECT COR_TRN_COMPANY.BATCH_HEADER_ID AS ID,
                                            COR_TRN_COMPANY.COMPANY_NAME + ' - ' + COR_TRN_COMPANY.TAN_NO  AS COMPANY
                                     FROM COR_TRN_COMPANY) AS ALL_COMPANIES
                              GROUP BY COMPANY
                              ORDER BY COMPANY";
            //--
            strSQL = @"SELECT COMPANY_ID AS ID,
                              COMPANY_NAME + ' - ' + TAN_NO  AS COMPANY
                       FROM   MST_COMPANY                                      
                       WHERE  INACTIVE_FLAG = 0
                       ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
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
            IDataReader drdGetCompanyDetails = null;
            string strSQL = "";
            try
            {
                //----------------------------
                #region VALIDATE FIELDS
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
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select the Financial Year");
                    cmbFinancialYear.Select();
                    return;
                }
                //
                //string url = "https://onlineservices.tin.nsdl.com/TIN/UnAuthorizedView.do?ID=1068688145&TAN=" + cmnService.J_Right(cmbCompany.Text, 10) +
                //             "&PRN=" + txtTokenNo.Text + "";
                //System.Diagnostics.Process.Start(url);      
                #endregion
                //----------------------------
                if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN, for any query regarding the contents of the linked page,\n please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //-- GET ASSESSMENT YEAR
                string strAssessmentYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))));
                //--
                string strTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 10);
                //
                if (strTAN == Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)))))
                {
                    #region REGULAR
                    strSQL = "SELECT  MST_COMPANY.COMPANY_ID            AS COMPANY_ID," +
                    "             MST_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
                    "             MST_COMPANY.TAN_NO                AS TAN_NO," +
                    "             MST_COMPANY.PAN_NO                AS PAN_NO," +
                    "             MST_COMPANY.BRANCH_DIV            AS BRANCH_DIV," +
                    "             MST_COMPANY.D_CATEGORY_ID         AS D_CATEGORY_ID," +
                    "             MST_CATEGORY.CATEGORY_DESCRIPTION AS CATEGORY_DESCRIPTION," +
                    "             MST_CATEGORY.CATEGORY_CODE        AS CATEGORY_CODE," +
                    "             MST_COMPANY.FILE_PREFIX           AS FILE_PREFIX," +
                    "             MST_COMPANY.ADDRESS1              AS ADDRESS1," +
                    "             MST_COMPANY.ADDRESS2              AS ADDRESS2," +
                    "             MST_COMPANY.ADDRESS3              AS ADDRESS3," +
                    "             MST_COMPANY.ADDRESS4              AS ADDRESS4," +
                    "             MST_COMPANY.ADDRESS5              AS ADDRESS5," +
                    "             MST_COMPANY.STATE_ID              AS STATE_ID," +
                    "             MST_STATE.STATE_NAME              AS STATE_NAME," +
                    "             MST_COMPANY.PIN_CODE              AS PIN_CODE," +
                    "             MST_COMPANY.STD                   AS STD," +
                    "             MST_COMPANY.PHONE                 AS PHONE," +
                    "             MST_COMPANY.EMAIL                 AS EMAIL," +
                    "             MST_COMPANY.PERSON_NAME           AS PERSON_NAME," +
                    "             MST_COMPANY.DESIGNATION           AS DESIGNATION," +
                    "             MST_COMPANY.FATHER_NAME           AS FATHER_NAME," +
                    "             MST_COMPANY.P_ADDRESS1            AS P_ADDRESS1," +
                    "             MST_COMPANY.P_ADDRESS2            AS P_ADDRESS2," +
                    "             MST_COMPANY.P_ADDRESS3            AS P_ADDRESS3," +
                    "             MST_COMPANY.P_ADDRESS4            AS P_ADDRESS4," +
                    "             MST_COMPANY.P_ADDRESS5            AS P_ADDRESS5," +
                    "             MST_COMPANY.P_STATE_ID            AS P_STATE_ID," +
                    "             RP_STATE.STATE_NAME               AS RP_STATE_NAME," +
                    "             MST_COMPANY.P_PIN_CODE            AS P_PIN_CODE," +
                    "             MST_COMPANY.P_PHONE               AS P_PHONE," +
                    "             MST_COMPANY.P_STD                 AS P_STD," +
                    "             MST_COMPANY.P_EMAIL               AS P_EMAIL," +
                    "             MST_COMPANY.P_MOBILE              AS P_MOBILE," +
                    "             MST_COMPANY.PAO_CODE              AS PAO_CODE," +
                    "             MST_COMPANY.PAO_REG_NO            AS PAO_REG_NO," +
                    "             MST_COMPANY.DDO_CODE              AS DDO_CODE," +
                    "             MST_COMPANY.DDO_REG_NO            AS DDO_REG_NO," +
                    "             MST_COMPANY.D_STATE_ID            AS D_STATE_ID," +
                    "             D_STATE.STATE_NAME                AS D_STATE_NAME," +
                    "             MST_COMPANY.MINISTRY_ID           AS MINISTRY_ID," +
                    "             MST_MINISTRY.MINISTRY_NAME        AS MINISTRY_NAME," +
                    "             MST_COMPANY.MINISTRY_OTHER        AS MINISTRY_OTHER," +
                    "             MST_COMPANY.CIT_TDS_ADDRESS       AS CIT_TDS_ADDRESS," +
                    "             MST_COMPANY.CIT_TDS_CITY          AS CIT_TDS_CITY," +
                    "             MST_COMPANY.CIT_TDS_PINCODE       AS CIT_TDS_PINCODE," +
                    "             MST_COMPANY.ALT_STD               AS ALT_STD," +
                    "             MST_COMPANY.ALT_PHONE             AS ALT_PHONE," +
                    "             MST_COMPANY.ALT_EMAIL             AS ALT_EMAIL," +
                    "             MST_COMPANY.P_ALT_STD             AS P_ALT_STD," +
                    "             MST_COMPANY.P_ALT_PHONE           AS P_ALT_PHONE," +
                    "             MST_COMPANY.P_ALT_EMAIL           AS P_ALT_EMAIL," +
                    "             MST_COMPANY.AIN_NO                AS AIN_NO," +
                    "             MST_COMPANY.TAN_REG_NO            AS TAN_REG_NO," +
                    "             MST_COMPANY.INACTIVE_FLAG         AS INACTIVE_FLAG," +
                    "             MST_COMPANY.P_PAN                 AS P_PAN " +
                    "     FROM    (((((MST_COMPANY INNER JOIN MST_CATEGORY " +
                    "             ON MST_COMPANY.D_CATEGORY_ID     = MST_CATEGORY.CATEGORY_ID) " +
                    "     INNER JOIN MST_STATE " +
                    "             ON MST_COMPANY.STATE_ID    = MST_STATE.STATE_ID) " +
                    "     INNER JOIN MST_STATE AS RP_STATE " +
                    "             ON MST_COMPANY.P_STATE_ID = RP_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_STATE AS D_STATE " +
                    "             ON MST_COMPANY.D_STATE_ID        = D_STATE.STATE_ID) " +
                    "     LEFT JOIN  MST_MINISTRY " +
                    "             ON MST_COMPANY.MINISTRY_ID       = MST_MINISTRY.MINISTRY_ID) " +
                    "     WHERE   MST_COMPANY.COMPANY_ID      = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    drdGetCompanyDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (drdGetCompanyDetails == null)
                    {
                        return;
                    }
                    while (drdGetCompanyDetails.Read())
                    {
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
                        #region COMMENT
                        //if (strBrowser.Contains("chrome"))
                        //{
                        //    #region COMMENT
                        //    //    //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        //    //    //service.HideCommandPromptWindow = true;

                        //    //    //var options = new ChromeOptions();
                        //    //    //// options.AddArgument("--window-position=-32000,-32000");
                        //    //    //options.AddArgument("--start-maximized");

                        //    //    //driver = new ChromeDriver(service, options);
                        //    //    //cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                        //    //    //return;
                        //    //    var driverService = ChromeDriverService.CreateDefaultService();
                        //    //    driverService.HideCommandPromptWindow = true;

                        //    //    var options = new ChromeOptions();

                        //    //    options.AddExcludedArgument("enable-automation");
                        //    //    options.AddAdditionalCapability("useAutomationExtension", false);
                        //    //    // options.AddArguments("disable-infobars");

                        //    //    var ChromeDriver = new ChromeDriver(driverService, options);

                        //    //    // IWebDriver driver     = new ChromeDriver(chromeDriverService, option);

                        //    //    ChromeDriver.Manage().Window.Maximize();
                        //    //    ChromeDriver.Navigate().GoToUrl("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
                        //    //    //
                        //    //    IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriver;
                        //    //    js.ExecuteScript("sendRequest(285)");

                        //    //    //--------------------------------------------
                        //    //    //  1.---FOR ***Category****
                        //    //    //--------------------------------------------
                        //    //    IWebElement query = ChromeDriver.FindElement(By.Id("01"));
                        //    //    query.Click();
                        //    //    //driver.FindElement(By.Id("02"));
                        //    //    //query.Click();

                        //    //    //--------------------------------------------
                        //    //    //  2.---FOR ***Nature of Payment****
                        //    //    //--------------------------------------------

                        //    //    query = ChromeDriver.FindElement(By.Id("L"));
                        //    //    query.Click();

                        //    //    //query = driver.FindElement(By.Id("D"));
                        //    //    //query.Click();


                        //    //    //query = driver.FindElement(By.Id("S"));
                        //    //    //query.Click();

                        //    //    //--------------------------------------------
                        //    //    //  3.---FOR ***Permanent Account No****
                        //    //    //--------------------------------------------
                        //    //    query = ChromeDriver.FindElement(By.Id("PanId"));
                        //    //    query.SendKeys("PANNOTAVAILABLE");

                        //    //    //--------------------------------------------
                        //    //    // 4.---FOR ***Financial Year****
                        //    //    //--------------------------------------------

                        //    //    SelectElement oSelect = new SelectElement(ChromeDriver.FindElement(By.Id("FinancialYearId")));
                        //    //    //  s.selectByVisibleText("Selenium");

                        //    //    // select by text
                        //    //    oSelect.SelectByText("2017-18");


                        //    //    //--------------------------------------------
                        //    //    // 5.---FOR ***Flat/Door/Block No.****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_Line1"));
                        //    //    query.SendKeys("6");

                        //    //    //--------------------------------------------
                        //    //    // 6.---FOR ***Name of premises/Building/Village****
                        //    //    //--------------------------------------------

                        //    //    query = driver.FindElement(By.Name("Add_Line2"));
                        //    //    query.SendKeys("Regency Building");

                        //    //    //--------------------------------------------
                        //    //    // 7.---FOR ***Road/Street/Lane****
                        //    //    //--------------------------------------------

                        //    //    query = driver.FindElement(By.Name("Add_Line3"));
                        //    //    query.SendKeys("6 Hungerford Street");
                        //    //    //--------------------------------------------
                        //    //    // 8.---FOR ***Area/Locality****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_Line4"));
                        //    //    query.SendKeys("Mintu Park");

                        //    //    //--------------------------------------------
                        //    //    // 9.---City/District****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_Line5"));
                        //    //    query.SendKeys("Kolkata");
                        //    //    //--------------------------------------------
                        //    //    // 10.---FOR ***State****
                        //    //    //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Name("Add_State")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("WEST BENGAL");

                        //    //    //--------------------------------------------
                        //    //    // 11.---FOR ***Pin Code****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_PIN"));
                        //    //    query.SendKeys("7000018");
                        //    //    //--------------------------------------------
                        //    //    // 12.---FOR ***Email ID****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_EMAIL"));
                        //    //    query.SendKeys("info@pdsinfotech.com");

                        //    //    //--------------------------------------------
                        //    //    // 13.---FOR ***Mobile****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("Add_MOBILE"));
                        //    //    query.SendKeys("9809890987");

                        //    //    //--------------------------------------------
                        //    //    // 14.---FOR ***Equalization Levy (Basic tax)****
                        //    //    //--------------------------------------------

                        //    //    query = driver.FindElement(By.Name("Equalization_Levy"));
                        //    //    query.SendKeys("10000");


                        //    //    //--------------------------------------------
                        //    //    // 15.---FOR ***Interest****
                        //    //    //--------------------------------------------

                        //    //    query = driver.FindElement(By.Name("Interest"));
                        //    //    query.SendKeys("500");
                        //    //    //--------------------------------------------
                        //    //    // 16.---FOR ***Penalty****
                        //    //    //--------------------------------------------

                        //    //    query = driver.FindElement(By.Name("Penalty"));
                        //    //    query.SendKeys("100");
                        //    //    //--------------------------------------------
                        //    //    // 17.---FOR **Others****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Name("OtherTax"));
                        //    //    query.SendKeys("90");

                        //    //    //--------------------------------------------
                        //    //    // 18.---FOR ***Crores****
                        //    //    //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Crores")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("10");  //  0-999
                        //    //                                 //--------------------------------------------
                        //    //                                 // 19.---FOR ***Lakhs****
                        //    //                                 //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Lakh")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("50");  //0-99

                        //    //    //--------------------------------------------
                        //    //    // 20.---FOR ***Thousands****
                        //    //    //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Thousands")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("80");  //0-99

                        //    //    //--------------------------------------------
                        //    //    // 21.---FOR ***Hundreds****
                        //    //    //--------------------------------------------

                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Hundreds")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("7");  //0-9

                        //    //    //--------------------------------------------
                        //    //    // 22.---FOR ***Tens****
                        //    //    //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Tens")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("5");   //0-9

                        //    //    //--------------------------------------------
                        //    //    // 23.---FOR ***Ones****
                        //    //    //--------------------------------------------
                        //    //    oSelect = new SelectElement(driver.FindElement(By.Id("Ones")));

                        //    //    // select by text
                        //    //    oSelect.SelectByText("7");   //0-9


                        //    //    //--------------------------------------------
                        //    //    // 23.---FOR ***pymtMode****
                        //    //    //--------------------------------------------
                        //    //    query = driver.FindElement(By.Id("onlineRadio"));
                        //    //    query.Click();
                        //    #endregion
                        //    cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                        //    return;
                        //}
                        //else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                        //{
                        //    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                        //    serv.HideCommandPromptWindow = true;

                        //    var options1 = new InternetExplorerOptions();

                        //    driver = new InternetExplorerDriver(serv, options1);
                        //}
                        //else
                        //{
                        //    cmnService.J_UserMessage("Please make your default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                        //    return;
                        //}
                        #endregion
                        //
                        if (strBrowser.Contains("chrome"))
                        {
                            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                            service.HideCommandPromptWindow = true;

                            var options = new ChromeOptions();
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
                        //Navigate to google page
                        driver.Navigate().GoToUrl("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
                        
                        //Find the Search text box UI Element
                        //IWebElement element = driver.FindElement(By.Name("q"));
                        //driver.FindElement(By.LinkText("CHALLAN NO./ITNS 281")).Click();
                        IWebElement lnkLink = driver.FindElement(By.LinkText("CHALLAN NO./ITNS 285"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", lnkLink);
                        //
                        System.Threading.Thread.Sleep(5000);

                        //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='radio' and @value='0020' and @name='MajorHead')]"));
                        //inputTextBox.Click();
                        //System.Threading.Thread.Sleep(5000);
                        //
                        IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='PAN')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["PAN_NO"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//select[(@name='FinancialYear')]"));
                        //inputTextBox.SendKeys(cmbFinancialYear.Text.Trim());
                        inputTextBox.SendKeys(strAssessmentYear);
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line1')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS1"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line2')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS4"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line3')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS2"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line4')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS5"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line5')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS3"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//select[(@name='Add_State')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["STATE_NAME"]));
                        //
                        //inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_PIN')]"));
                        //inputTextBox = driver.FindElement(By.XPath("//input[(@type='number' and @name='Add_PIN')]"));
                        //inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["PIN_CODE"]));
                        ////
                        //inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_EMAIL')]"));
                        //inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["EMAIL"]));
                        ////
                        //inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_MOBILE')]"));
                        //inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["P_MOBILE"]));

                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='number' and @name='Add_PIN')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["PIN_CODE"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_EMAIL')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["EMAIL"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='number' and @name='Add_MOBILE')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["P_MOBILE"]));
                        //
                        #region COMMENTED
                        //lngSearchId = Id;

                        //txtDedEmpColName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                        //txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                        //txtPANNo.Text = Convert.ToString(drdShowRecord["PAN_NO"]);
                        //cmbDeductorType.Text = Convert.ToString(drdShowRecord["CATEGORY_CODE"]) + " - " + Convert.ToString(drdShowRecord["CATEGORY_DESCRIPTION"]);
                        //txtBranch.Text = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                        //txtAddress1.Text = Convert.ToString(drdShowRecord["ADDRESS1"]);
                        //txtAddress2.Text = Convert.ToString(drdShowRecord["ADDRESS2"]);
                        //txtAddress3.Text = Convert.ToString(drdShowRecord["ADDRESS3"]);
                        //txtAddress4.Text = Convert.ToString(drdShowRecord["ADDRESS4"]);
                        //txtAddress5.Text = Convert.ToString(drdShowRecord["ADDRESS5"]);
                        //cmbState.Text = Convert.ToString(drdShowRecord["STATE_NAME"]);
                        //txtPIN.Text = Convert.ToString(drdShowRecord["PIN_CODE"]);
                        //txtSTD.Text = Convert.ToString(drdShowRecord["STD"]);
                        //txtPhone.Text = Convert.ToString(drdShowRecord["PHONE"]);
                        //txtEmail.Text = Convert.ToString(drdShowRecord["EMAIL"]);
                        //txtRPName.Text = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                        //txtRPDesignation.Text = Convert.ToString(drdShowRecord["DESIGNATION"]);
                        //txtRPFatherName.Text = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                        //txtRPAddress1.Text = Convert.ToString(drdShowRecord["P_ADDRESS1"]);
                        //txtRPAddress2.Text = Convert.ToString(drdShowRecord["P_ADDRESS2"]);
                        //txtRPAddress3.Text = Convert.ToString(drdShowRecord["P_ADDRESS3"]);
                        //txtRPAddress4.Text = Convert.ToString(drdShowRecord["P_ADDRESS4"]);
                        //txtRPAddress5.Text = Convert.ToString(drdShowRecord["P_ADDRESS5"]);
                        //cmbRPState.Text = Convert.ToString(drdShowRecord["RP_STATE_NAME"]);
                        //txtRPPIN.Text = Convert.ToString(drdShowRecord["P_PIN_CODE"]);
                        //txtRPSTD.Text = Convert.ToString(drdShowRecord["P_STD"]);
                        //txtRPPhone.Text = Convert.ToString(drdShowRecord["P_PHONE"]);
                        //txtRPMobileNo.Text = Convert.ToString(drdShowRecord["P_MOBILE"]);
                        //txtRPEmail.Text = Convert.ToString(drdShowRecord["P_EMAIL"]);
                        //txtPAOCode.Text = Convert.ToString(drdShowRecord["PAO_CODE"]);
                        //txtPAORegNo.Text = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                        //txtDDOCode.Text = Convert.ToString(drdShowRecord["DDO_CODE"]);
                        //txtDDORegNo.Text = Convert.ToString(drdShowRecord["DDO_REG_NO"]);
                        //cmbGovtDedState.Text = Convert.ToString(drdShowRecord["D_STATE_NAME"]);
                        //cmbMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_NAME"]);
                        //txtOtherMinistry.Text = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                        //txtCITAddress.Text = Convert.ToString(drdShowRecord["CIT_TDS_ADDRESS"]);
                        //txtCITCity.Text = Convert.ToString(drdShowRecord["CIT_TDS_CITY"]);
                        //txtCITPin.Text = Convert.ToString(drdShowRecord["CIT_TDS_PINCODE"]);
                        //txtAltSTD.Text = Convert.ToString(drdShowRecord["ALT_STD"]);
                        //txtAltPhone.Text = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                        //txtAltEmail.Text = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                        //txtRPAltSTD.Text = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                        //txtRPAltPhone.Text = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                        //txtRPAltEmail.Text = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);
                        //txtAIN.Text = Convert.ToString(drdShowRecord["AIN_NO"]);
                        //txtTANRegNo.Text = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                        //if (Convert.ToString(drdShowRecord["INACTIVE_FLAG"]) == "1")
                        //    chkInactiveCompany.Checked = true;
                        ////
                        //txtRPPAN.Text = Convert.ToString(drdShowRecord["P_PAN"]);
                        #endregion
                        //
                        drdGetCompanyDetails.Close();
                        drdGetCompanyDetails.Dispose();
                        //--
                        return;
                    }
                    //-----------------------------------------------------------
                    drdGetCompanyDetails.Close();
                    drdGetCompanyDetails.Dispose();
                    //-------------------------
                    #endregion
                }
                else
                {
                    #region CORRECTION
                    strSQL = "SELECT  COR_TRN_COMPANY.HDR_COMPANY_ID        AS HDR_COMPANY_ID," +
                        "             COR_TRN_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
                        "             COR_TRN_COMPANY.TAN_NO                AS TAN_NO," +
                        "             COR_TRN_COMPANY.PAN_NO                AS PAN_NO," +
                        "             COR_TRN_COMPANY.MODE                  AS MODE," +
                        "             COR_TRN_COMPANY.BRANCH_DIV            AS BRANCH_DIV," +
                        "             COR_TRN_COMPANY.D_CATEGORY_ID         AS D_CATEGORY_ID," +
                        "             MST_CATEGORY.CATEGORY_DESCRIPTION     AS CATEGORY_DESCRIPTION," +
                        "             MST_CATEGORY.CATEGORY_CODE            AS CATEGORY_CODE," +
                        "             COR_TRN_COMPANY.FILE_PREFIX           AS FILE_PREFIX," +
                        "             COR_TRN_COMPANY.ADDRESS1              AS ADDRESS1," +
                        "             COR_TRN_COMPANY.ADDRESS2              AS ADDRESS2," +
                        "             COR_TRN_COMPANY.ADDRESS3              AS ADDRESS3," +
                        "             COR_TRN_COMPANY.ADDRESS4              AS ADDRESS4," +
                        "             COR_TRN_COMPANY.ADDRESS5              AS ADDRESS5," +
                        "             COR_TRN_COMPANY.STATE_ID              AS STATE_ID," +
                        "             MST_STATE.STATE_CODE                  AS STATE_CODE," +
                        "             MST_STATE.STATE_NAME                  AS STATE_NAME," +
                        "             COR_TRN_COMPANY.PIN_CODE              AS PIN_CODE," +
                        "             COR_TRN_COMPANY.STD                   AS STD," +
                        "             COR_TRN_COMPANY.PHONE                 AS PHONE," +
                        "             COR_TRN_COMPANY.EMAIL                 AS EMAIL," +
                        "             COR_TRN_COMPANY.PERSON_NAME           AS PERSON_NAME," +
                        "             COR_TRN_COMPANY.DESIGNATION           AS DESIGNATION," +
                        "             COR_TRN_COMPANY.FATHER_NAME           AS FATHER_NAME," +
                        "             COR_TRN_COMPANY.P_ADDRESS1            AS P_ADDRESS1," +
                        "             COR_TRN_COMPANY.P_ADDRESS2            AS P_ADDRESS2," +
                        "             COR_TRN_COMPANY.P_ADDRESS3            AS P_ADDRESS3," +
                        "             COR_TRN_COMPANY.P_ADDRESS4            AS P_ADDRESS4," +
                        "             COR_TRN_COMPANY.P_ADDRESS5            AS P_ADDRESS5," +
                        "             COR_TRN_COMPANY.P_STATE_ID            AS P_STATE_ID," +
                        "             RP_STATE.STATE_CODE                   AS RP_STATE_CODE," +
                        "             RP_STATE.STATE_NAME                   AS RP_STATE_NAME," +
                        "             COR_TRN_COMPANY.P_PIN_CODE            AS P_PIN_CODE," +
                        "             COR_TRN_COMPANY.P_PHONE               AS P_PHONE," +
                        "             COR_TRN_COMPANY.P_STD                 AS P_STD," +
                        "             COR_TRN_COMPANY.P_EMAIL               AS P_EMAIL," +
                        "             COR_TRN_COMPANY.P_MOBILE              AS P_MOBILE," +
                        "             COR_TRN_COMPANY.PAO_CODE              AS PAO_CODE," +
                        "             COR_TRN_COMPANY.PAO_REG_NO            AS PAO_REG_NO," +
                        "             COR_TRN_COMPANY.DDO_CODE              AS DDO_CODE," +
                        "             COR_TRN_COMPANY.DDO_REG_NO            AS DDO_REG_NO," +
                        "             COR_TRN_COMPANY.D_STATE_ID            AS D_STATE_ID," +
                        "             D_STATE.STATE_CODE                    AS D_STATE_CODE," +
                        "             D_STATE.STATE_NAME                    AS D_STATE_NAME," +
                        "             COR_TRN_COMPANY.MINISTRY_ID           AS MINISTRY_ID," +
                        "             MST_MINISTRY.MINISTRY_CODE            AS MINISTRY_CODE," +
                        "             MST_MINISTRY.MINISTRY_NAME            AS MINISTRY_NAME," +
                        "             COR_TRN_COMPANY.MINISTRY_OTHER        AS MINISTRY_OTHER," +
                        "             COR_TRN_COMPANY.ADDRESS_CHANGE        AS ADDRESS_CHANGE," +
                        "             COR_TRN_COMPANY.P_ADDRESS_CHANGE      AS P_ADDRESS_CHANGE," +
                        "             COR_TRN_COMPANY.BATCH_UPDATION_INDICATOR AS BATCH_UPDATION_INDICATOR," +
                        "             COR_HDR_BATCH.ORIGINAL_RRR_NO         AS ORIGINAL_RRR_NO," +
                        "             COR_HDR_BATCH.PREVIOUS_RRR_NO         AS PREVIOUS_RRR_NO," +
                        "             COR_TRN_COMPANY.AIN_NO                AS AIN_NO," +
                        "             COR_TRN_COMPANY.TAN_REG_NO            AS TAN_REG_NO," +
                        "             COR_TRN_COMPANY.ALT_STD               AS ALT_STD," +
                        "             COR_TRN_COMPANY.ALT_PHONE             AS ALT_PHONE," +
                        "             COR_TRN_COMPANY.ALT_EMAIL             AS ALT_EMAIL," +
                        "             COR_TRN_COMPANY.P_ALT_STD             AS P_ALT_STD," +
                        "             COR_TRN_COMPANY.P_ALT_PHONE           AS P_ALT_PHONE," +
                        "             COR_TRN_COMPANY.P_ALT_EMAIL           AS P_ALT_EMAIL," +
                        "             COR_TRN_COMPANY.P_PAN                 AS P_PAN " +
                        "     FROM    ((((((COR_TRN_COMPANY LEFT JOIN MST_CATEGORY " +
                        "             ON COR_TRN_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID) " +
                        "     LEFT JOIN MST_STATE " +
                        "             ON COR_TRN_COMPANY.STATE_ID      = MST_STATE.STATE_ID) " +
                        "     LEFT JOIN MST_STATE AS RP_STATE " +
                        "             ON COR_TRN_COMPANY.P_STATE_ID    = RP_STATE.STATE_ID) " +
                        "     INNER JOIN COR_HDR_BATCH " +
                        "             ON COR_TRN_COMPANY.BATCH_HEADER_ID= COR_HDR_BATCH.BATCH_HEADER_ID) " +
                        "     LEFT JOIN  MST_STATE AS D_STATE " +
                        "             ON COR_TRN_COMPANY.D_STATE_ID    = D_STATE.STATE_ID) " +
                        "     LEFT JOIN  MST_MINISTRY " +
                        "             ON COR_TRN_COMPANY.MINISTRY_ID   = MST_MINISTRY.MINISTRY_ID) " +
                        "     WHERE   COR_TRN_COMPANY.BATCH_HEADER_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    drdGetCompanyDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (drdGetCompanyDetails == null)
                    {
                        return;
                    }
                    while (drdGetCompanyDetails.Read())
                    {
                        #region COMMENT
                        //string str = TdsMan.GetSystemDefaultBrowser();
                        //IWebDriver driver = null;

                        //if (str.Contains("chrome"))
                        //{
                        //    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        //    service.HideCommandPromptWindow = true;

                        //    var options = new ChromeOptions();
                        //    // options.AddArgument("--window-position=-32000,-32000");
                        //    options.AddArgument("--start-maximized");

                        //    driver = new ChromeDriver(service, options);
                        //}
                        //else
                        //{
                        //    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                        //    serv.HideCommandPromptWindow = true;

                        //    var options1 = new InternetExplorerOptions();

                        //    driver = new InternetExplorerDriver(serv, options1);
                        //}
                        #endregion
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
                        #region COMMENT
                        //if (strBrowser.Contains("chrome"))
                        //{
                        //    cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                        //    return;
                        //}
                        //else if (strBrowser.Contains("IE") || strBrowser.Contains("iexplore.exe"))
                        //{
                        //    InternetExplorerDriverService serv = InternetExplorerDriverService.CreateDefaultService();
                        //    serv.HideCommandPromptWindow = true;

                        //    var options1 = new InternetExplorerOptions();

                        //    driver = new InternetExplorerDriver(serv, options1);
                        //}
                        //else
                        //{
                        //    cmnService.J_UserMessage("Please make your default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                        //    return;
                        //}
                        //--
                        //--
                        #endregion
                        //
                        if (strBrowser.Contains("chrome"))
                        {
                            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                            service.HideCommandPromptWindow = true;

                            var options = new ChromeOptions();
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
                        //Navigate to google page
                        driver.Navigate().GoToUrl("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");


                        //Find the Search text box UI Element
                        //IWebElement element = driver.FindElement(By.Name("q"));

                        // driver.FindElement(By.LinkText("CHALLAN NO./ITNS 281")).Click();


                        IWebElement lnkLink = driver.FindElement(By.LinkText("CHALLAN NO./ITNS 281"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", lnkLink);
                        

                        //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='radio' and @value='0020' and @name='MajorHead')]"));
                        //inputTextBox.Click();
                        System.Threading.Thread.Sleep(5000);
                        //
                            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='PAN')]"));
                               inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["PAN_NO"]));
                        //
                              inputTextBox = driver.FindElement(By.XPath("//select[(@name='FinancialYear')]"));
                             //inputTextBox.SendKeys(cmbFinancialYear.Text.Trim());
                             inputTextBox.SendKeys(strAssessmentYear);
                             //
                             inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line1')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS1"]));
                             //
                             inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line2')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS4"]));
                             //
                             inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line3')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS2"]));
                        //
                            inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line4')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS5"]));
                             //
                             inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_Line5')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["ADDRESS3"]));
                             //
                             inputTextBox = driver.FindElement(By.XPath("//select[(@name='Add_State')]"));
                             inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["STATE_NAME"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='number' and @name='Add_PIN')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["PIN_CODE"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='Add_EMAIL')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["EMAIL"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='number' and @name='Add_MOBILE')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["P_MOBILE"]));
                        //
                        drdGetCompanyDetails.Close();
                        drdGetCompanyDetails.Dispose();
                        //--
                        return;
                    }
                    //-----------------------------------------------------------
                    drdGetCompanyDetails.Close();
                    drdGetCompanyDetails.Dispose();
                    //-------------------------
                    #endregion
                }
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
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

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=RO9adNeBGKE");                      
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0015", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));            
        }
        #endregion


        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0071", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion
    }
}

