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
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;



#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnAutoFillingChallan : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnAutoFillingChallan()
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
        string strFolderPath = "", strSQL = "";
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
        string strAssessmentYear = "";
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
            //
            lblTitle.Text = "Pay Tax Online (Challan 281) - Auto Fill";
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
            //-- SECTON
            //-----------
            //strSQL = " SELECT DISTINCT 1, SECTION_NO " +
            //    "      FROM   MST_SECTION " +
            //    //"      WHERE  FORM_NAME = '" + FormNo + "' " + Conditions +
            //    //"      ORDER BY SECTION_ID";
            //    @"      WHERE SECTION_NO IN ('192','193','194','194A','194B','194BB','194DA','194LA','194LB','194LC','194LD',
            //    '194C','194D','194E','194EE','194F','194G','194H','194I','194IC','194J',
            //    '194LBA','194LBB','194LBC','194N','194O','195','196A','206C','194K',
            //    '196B','196C','196D','194P','194Q','196D(1A)','194R','194S',
            //    '194BA','194BA(2)','194B-P','194R-P','194S-P')" +
            //    "      ORDER BY SECTION_NO";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            if (rbnIncometaxAct1961.Checked == true)
            {
                string[] strSection = { "", "192","193","194","194A","194B","194BB","194DA","194LA","194LB","194LC","194LD",
                "194C","194D","194E","194EE","194F","194G","194H","194I","194IC","194J",
                "194LBA","194LBB","194LBC","194N","194O","195","196A","206C","194K",
                "196B","196C","196D","194P","194Q","196D(1A)","194R","194S",
                "194BA","194BA(2)","194B-P","194R-P","194S-P" };
                dmlService.J_PopulateComboBox(strSection, ref cmbSection, 1);
            }
            else
            {
                //string[,] strLoadSectionMatrix = {{"SECTION_NAME_2 <> ''", "F", "SECTION_NO + ' [' + SECTION_NAME_2 + ']'", "F"},
                //                              {"SECTION_NAME_2 = ''", "F", "", "T"}};
                //strSQL = " SELECT SECTION_ID," +
                ////"             SECTION_NO " +
                //"       " + cmnService.J_SQLDBFormat(strLoadSectionMatrix, J_SQLColFormat.Case_End) + " AS SECTION_NO " +
                //"      FROM   MST_SECTION " +
                //"      WHERE  ASST_ID <= " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
                //"      AND (VALID_UPTO_ASST_ID >= " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " OR VALID_UPTO_ASST_ID = 0) " +
                //"      ORDER BY SECTION_NAME_2, SECTION_NO";
                //dmlService.J_PopulateComboBox(strSQL, ref cmbSection, 1);
                LoadSection(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)));
            }
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

        #region BtnSave_MouseClick
        private void BtnSave_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region mnuFromNSDLDownload_Click
        private void mnuFromNSDLDownload_Click(object sender, EventArgs e)
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
                            ////EdgeDriverService serv = EdgeDriverService.CreateDefaultService();
                            ////serv.HideCommandPromptWindow = true;

                            ////var options3 = new EdgeOptions();

                            ////driver = new EdgeDriver(serv, options3);

                            //string driverPath = Path.Combine(Application.StartupPath);//, "Drivers");

                            //var service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe");
                            //service.HideCommandPromptWindow = true;

                            //var options = new EdgeOptions();

                            //options.AddArgument("--start-maximized");
                            //options.AddArgument("--disable-web-security");
                            //options.AddArgument("--no-proxy-server");
                            //options.AddArgument("--no-sandbox");
                            //options.AddArgument("--disable-blink-features=AutomationControlled");

                            //string tempProfileDir = Path.Combine(Path.GetTempPath(), "TDS_" + Guid.NewGuid().ToString());
                            //Directory.CreateDirectory(tempProfileDir);

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
                        //--
                        //Navigate to google page
                        driver.Navigate().GoToUrl("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");

                        //Find the Search text box UI Element
                        //IWebElement element = driver.FindElement(By.Name("q"));
                        //driver.FindElement(By.LinkText("CHALLAN NO./ITNS 281")).Click();
                        IWebElement lnkLink = driver.FindElement(By.LinkText("CHALLAN NO./ITNS 281"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", lnkLink);
                        //
                        System.Threading.Thread.Sleep(5000);

                        //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='radio' and @value='0020' and @name='MajorHead')]"));
                        //inputTextBox.Click();
                        //System.Threading.Thread.Sleep(5000);
                        //
                        IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='TAN')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["TAN_NO"]));
                        //
                        //inputTextBox = driver.FindElement(By.XPath("//select[(@name='AssessYear')]"));
                        ////inputTextBox.SendKeys(cmbFinancialYear.Text.Trim());
                        //inputTextBox.SendKeys(strAssessmentYear);
                        new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-15"))).Click();
                        driver.FindElement(By.XPath("//span[contains(text(),'" + strAssessmentYear + "')]")).Click();
                        //
                        System.Threading.Thread.Sleep(3000);
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var Proceed = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='largervf proceed']")));
                        Proceed.Click();
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
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='email' and @name='Add_EMAIL')]"));
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

                        if (strBrowser.Contains("chrome"))
                        {
                            //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                            //service.HideCommandPromptWindow = true;

                            //var options = new ChromeOptions();
                            //// options.AddArgument("--window-position=-32000,-32000");
                            //options.AddArgument("--start-maximized");

                            //driver = new ChromeDriver(service, options);
                            //cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                            //return;
                            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                            service.HideCommandPromptWindow = true;

                            var options = new ChromeOptions();
                            //// options.AddArgument("--window-position=-32000,-32000");
                            //options.AddArgument("--start-maximized");

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
                            //cmnService.J_UserMessage("Please make your default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                            cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
                            return;
                        }
                        //--
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
                        IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@type='text' and @name='TAN')]"));
                        inputTextBox.SendKeys(Convert.ToString(drdGetCompanyDetails["TAN_NO"]));
                        //
                        inputTextBox = driver.FindElement(By.XPath("//select[(@name='AssessYear')]"));
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
                        inputTextBox = driver.FindElement(By.XPath("//input[(@type='email' and @name='Add_EMAIL')]"));
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


        #region mnuFromITDownload_Click
        private void mnuFromITDownload_Click(object sender, EventArgs e)
        {
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
            //--
            grpIncomeTaxLoginPassword.Visible = true;
            txtPassword.Text = "";
            cmbSection.Select();
            //--
            if (cmbCompany.SelectedIndex > 0)
                txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO ='" + cmnService.J_Right(cmbCompany.Text.Trim(), 10) + "'"));
            else
                txtPassword.Text = "";
            //--
            BtnSave.Enabled = false;
            BtnSave.BackColor = Color.LightGray;
            //--
        }
        #endregion

        #region btnGoITView_Click
        private void btnGoITView_Click(object sender, EventArgs e)
        {
            IWebDriver driver = null;
            string tempUserDir = "";

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
                //if (cmbSection.SelectedIndex <= 0)
                if (cmbSection.Text == "")
                {
                    cmnService.J_UserMessage("Select the Section");
                    cmbSection.Select();
                    return;
                }
                //
                if (txtPassword.Text == "")
                {
                    cmnService.J_UserMessage("Enter the Password");
                    cmbSection.Select();
                    return;
                }
                //--
                if (cmnService.J_UserMessage("This will take you to a webpage outside TDSMAN, for any query regarding the contents of the linked page,\n please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                //--
                string strFAYear = "", strTAN = "", strPassword = "", strSection = "";
                //
                strFAYear = cmbFinancialYear.Text;
                strTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 10);
                strPassword = txtPassword.Text;
                strSection = cmbSection.Text;
                if (strSection != "") //-- 2025/10/27
                {
                    if (strSection.Length > 3)
                    {
                        if (cmnService.J_Left(strSection, 4) == "206C")
                        {
                            strSection = "206C";
                        }
                        else if (cmnService.J_Left(strSection, 4) == "194J")
                        {
                            strSection = "194J";
                        }
                        else if (cmnService.J_Left(strSection, 4) == "194I")
                        {
                            if (cmnService.J_Left(strSection, 4) == "194IC")
                            { }
                            else
                                strSection = "194I";
                        }
                    }
                }
                //--
                SaveTANData(strTAN, strPassword);
                //--
                string strOS = TdsMan.GetOSVersion();
                //--
                string strBrowser = "";
                if (strOS == "XP")
                    strBrowser = TdsMan.GetSystemDefaultBrowserXP();
                else
                    strBrowser = TdsMan.GetSystemDefaultBrowser();
                //--
                tempUserDir = Path.Combine(Path.GetTempPath(), "TM_" + Guid.NewGuid().ToString());

                //IWebDriver driver = null;
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

                    ////options.AddArgument("--headless=new"); // newer headless mode
                    ////options.AddArgument("--disable-gpu");


                    ////// Add this block to use a temporary profile directory
                    //////tempUserDir = Path.Combine(Path.GetTempPath(), "ChromeUserData_" + Guid.NewGuid().ToString());
                    //////options.AddArgument("--user-data-dir=" + tempUserDir);

                    //////tempUserDir = Path.Combine(Path.GetTempPath(), "ChromeProfile_" + Guid.NewGuid().ToString());
                    ////options.AddArgument("--user-data-dir=" + tempUserDir);



                    ////driver = new ChromeDriver(service, options);
                    ////////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                    ////////////return;
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
                    ////string edgeDriverPath = Application.StartupPath; // where msedgedriver.exe is located
                    ////var service = EdgeDriverService.CreateDefaultService(edgeDriverPath);
                    ////service.HideCommandPromptWindow = true;


                    ////var options = new EdgeOptions();
                    ////options.AddArgument("--user-data-dir=" + tempUserDir);
                    ////options.AddArgument("--start-maximized");
                    ////options.AddArgument("--disable-web-security");
                    ////options.AddArgument("--no-sandbox");

                    ////driver = new EdgeDriver(service, options);
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
                ////System.Threading.Thread.Sleep(1000);
                System.Threading.Thread.Sleep(3000);
                ////////IWebElement inputTextBox = driver.FindElement(By.Name("panAdhaarUserId")); //-- 2026/01/26
                IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
                //inputTextBox.SendKeys(txtUserID?.Text?.Trim() ?? "");
                inputTextBox.SendKeys(strTAN);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//mat-checkbox[@id='passwordCheckBox']")).Click();
                System.Threading.Thread.Sleep(2000);

                inputTextBox = driver.FindElement(By.XPath("//input[@type='password' and @name='loginPasswordField']"));
                //inputTextBox.SendKeys(txtPassword?.Text?.Trim() ?? "");
                inputTextBox.SendKeys(strPassword);

                System.Threading.Thread.Sleep(3000);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();


                System.Threading.Thread.Sleep(3000);
                var modalDialogs = driver.FindElements(By.XPath("//div[contains(@class,'modal-dialog modal-dialog-centered')]"));

                if (modalDialogs.Count < 6)
                {
                    // Look for the 'Login Here' button inside the modal
                    var loginHereButton = driver.FindElement(By.XPath("//button[contains(text(),'Login Here')]"));

                    if (loginHereButton.Displayed && loginHereButton.Enabled)
                    {
                        loginHereButton.Click();
                    }
                }

                //====================================================
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                Actions action = new Actions(driver);

                //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
                var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("e-File"))); //-- 2023/11/28
                action.MoveToElement(Menu).Build().Perform();
                //action.MoveToElement(Menu).Build().Perform();


                var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'e-Pay Tax')]")));
                SubmenuElement.Click();
                //====================================================   
                System.Threading.Thread.Sleep(2000);
                WebDriverWait actWait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                if (rbnIncometaxAct1961.Checked == true)
                {
                    // Select Income-tax Act, 1961
                    IWebElement act1961 = driver.FindElement(By.XPath("(//input[@type='radio'])[2]"));

                    ((IJavaScriptExecutor)driver)
                        .ExecuteScript("arguments[0].click();", act1961);
                }
                else
                {
                    // Select Income-tax Act, 2025
                    IWebElement act2025 = driver.FindElement(
                        By.XPath("(//input[@type='radio'])[1]"));

                    ((IJavaScriptExecutor)driver)
                        .ExecuteScript("arguments[0].click();", act2025);
                }
                IWebElement continueBtn = actWait.Until(driverObj =>
                {
                    try
                    {
                        var ele = driverObj.FindElement(
                            By.XPath("//button[contains(@class,'nextIcon')]"));

                        return (ele.Displayed && ele.Enabled) ? ele : null;
                    }
                    catch
                    {
                        return null;
                    }
                });

                // Scroll into view
                ((IJavaScriptExecutor)driver)
                    .ExecuteScript("arguments[0].scrollIntoView(true);", continueBtn);

                System.Threading.Thread.Sleep(1000);

                // JS click (more reliable on Angular pages)
                ((IJavaScriptExecutor)driver)
                    .ExecuteScript("arguments[0].click();", continueBtn);

                System.Threading.Thread.Sleep(4000);

                System.Threading.Thread.Sleep(1000);
                //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-tab-label-0-2"))).Click();

                //var Filter = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ml-auto defaultButton filterButton filterMobile overflow-auto justify-content-end ng-star-inserted']")));
                //var NewPayment = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='large-button-secondary defualtButtonGap newPaymentbuttonRight ng-star-inserted']")));
                var NewPayment = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='large-button-secondary defualtButtonGap newPaymentbuttonRight']")));
                NewPayment.Click();

                //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[starts-with(@class,'ml-auto large-button-secondary defualtButtonGap newPaymentbuttonRight ng-star-inserted')][text()=' New Payment ']"))).Click();
                //System.Threading.Thread.Sleep(5000);
                //========================================================================
                //---ASSESSMENT YEAR
                //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-7"))).Click();
                //driver.FindElement(By.XPath("//span[contains(text(),'" + strFAYear + "')]")).Click();

                //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-15"))).Click();
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-value-7"))).Click();
                driver.FindElement(By.XPath("//span[contains(text(),'" + strAssessmentYear + "')]")).Click();

                var Proceed = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='largervf proceed']")));
                Proceed.Click();

                //////System.Threading.Thread.Sleep(4000);

                //////var Filter = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='defaultButton filterButton close-btn mat-stroked-button']")));
                //////Filter.Click();

                ////System.Threading.Thread.Sleep(2000);
                //////--- Section 
                ////new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-10"))).Click();
                ////driver.FindElement(By.XPath("//span[contains(text(),'" + strSection + "')]")).Click();

                ////System.Threading.Thread.Sleep(2000);

                ////var SearchSection = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='defaultButton primaryButton']")));
                ////SearchSection.Click();
                System.Threading.Thread.Sleep(2000);
                var FilterClicked = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='defaultButton filterButton close-btn mdc-button mdc-button--outlined mat-mdc-outlined-button mat-unthemed mat-mdc-button-base']")));
                FilterClicked.Click();

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-10"))).Click();
                //driver.FindElement(By.XPath("//span[contains(text(),'" + AFC_SECTION_CODE + "')]")).Click(); 

                //IWebElement option = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[.//span[normalize-space()='" + cmbSection.Text + "']]")));
                IWebElement option = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[.//span[normalize-space()='" + cmbSection.Text + "']]")));
                option.Click();

                var FilterButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='defaultButton primaryButton']")));
                FilterButton.Click();

                IWebElement radio = driver.FindElement(By.XPath("//input[@class='mdc-radio__native-control']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", radio);

                System.Threading.Thread.Sleep(3000);
                var ButtonContinue = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='large-button-primary iconsAfter nextIcon']")));
                ButtonContinue.Click();
                //-- Download 
                //==================================================
                //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[starts-with(@class,'normal-button-secondary iconBefore downloadIcon ng-star-inserted')][text()='Download Challan File']"))).Click();
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("Error: " + ex.Message);
            }
            //finally
            //{
            //    try
            //    {
            //        if (driver != null)
            //        {
            //            driver.Quit(); // Ensure browser is closed
            //        }

            //        // Clean up temp Chrome profile
            //        if (!string.IsNullOrEmpty(tempUserDir) && Directory.Exists(tempUserDir))
            //        {
            //            Directory.Delete(tempUserDir, true);
            //        }
            //    }
            //    catch (Exception cleanupEx)
            //    {
            //        // Optionally log cleanupEx
            //    }
            //}
        }
        #endregion

        #region TxtPassword_TextChanged
        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
                txtPassword.UseSystemPasswordChar = true;
            else
                txtPassword.UseSystemPasswordChar = false;
        }
        #endregion

        #region SaveTANData
        private void SaveTANData(string TAN, string Password)
        {
            // --
            string strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(TAN) + "' ";
            int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            if (iCount == 0)
            {
                //insering new record in the tan login master
                strSQL = "INSERT INTO MST_TAN_AADHAAR(TAN_NO, USER_PASSWORD) " +
                         "VALUES( '" + cmnService.J_ReplaceQuote(TAN) + "', " +
                         "        '" + cmnService.J_ReplaceQuote(Password) + "')";

                dmlService.J_ExecSql(strSQL);
                //--
            }
            else
            {
                //updating the existing record in the master

                strSQL = "UPDATE MST_TAN_AADHAAR " +
                         "SET    USER_PASSWORD = '" + cmnService.J_ReplaceQuote(Password) + "' " +
                         "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(TAN) + "' ";

                dmlService.J_ExecSql(strSQL);
            }
            //--
        }
        #endregion


        #region cmbSection_SelectedIndexChanged
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDisplaySection.Visible = true;
            //if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2026_27ID)
            //    lblDisplaySection.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_NO + ' [' + SECTION_NAME_2 + ']' ='" + cmbSection.Text + "'")));
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2026_27ID)
                lblDisplaySection.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_NO ='" + cmbSection.Text + "'")));
            else
            {
                if (rbnIncometaxAct1961.Checked == true)
                    lblDisplaySection.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_NO ='" + cmbSection.Text + "'")));
                else if (rbnIncometaxAct2025.Checked == true)
                    lblDisplaySection.Text = TdsMan.T_ReplaceAmpersand(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_NO + ' [' + SECTION_NAME_2 + ']' ='" + cmbSection.Text + "'")));

            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--
            if (cmbCompany.SelectedIndex > 0)
                txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO ='" + cmnService.J_Right(cmbCompany.Text.Trim(), 10) + "'"));
            else
                txtPassword.Text = "";
            //--
            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.Lavender;
            grpIncomeTaxLoginPassword.Visible = false;
        }
        #endregion


        #region cmbFinancialYear_KeyPress
        private void cmbFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbSection_KeyPress
        private void cmbSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtPassword_KeyPress
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region btnGetOTP_Click
        private void btnGetOTP_Click(object sender, EventArgs e)
        {
            IncomeTaxConnect2025 objIncometaxConnect = new IncomeTaxConnect2025();
            TracesResponse objTracesResponse = new TracesResponse();
            string strSection = "";
            try
            {
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                strSection = cmbSection.Text;
                if (strSection == "")
                {
                    cmnService.J_UserMessage("Select the Section");
                    cmbSection.Select();
                    return;
                }
                //--
                if (txtMobileNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter the Mobile No.");
                    txtMobileNo.Select();
                    return;
                }
                //--
                if (txtMobileNo.Text.Trim().Length != 10)
                {
                    cmnService.J_UserMessage("Invalid Mobile No.");
                    txtMobileNo.Select();
                    return;
                }
                //--
                string strFAYear = "", strTAN = "", strPassword = "", strMobile = "";
                //
                //strFAYear = cmbFinancialYear.Text;
                strTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 10);
                strMobile = txtMobileNo.Text.Trim();
                //strPassword = txtPassword.Text;
                //strSection = cmbSection.Text;
                if (cmnService.J_Left(strSection, 4) == "206C")
                {
                    strSection = "206C";
                }
                else if (cmnService.J_Left(strSection, 4) == "194J")
                {
                    strSection = "194J";
                }
                else if (cmnService.J_Left(strSection, 4) == "194I")
                {
                    if (cmnService.J_Left(strSection, 4) == "194IC")
                    { }
                    else
                        strSection = "194I";
                }
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

                    options.AddUserProfilePreference("disable-popup-blocking", true);

                    options.AddArgument("--disable-blink-features=AutomationControlled");

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
                ////====================================================
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(2);
                driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/e-pay-tax-prelogin/user-details");

                IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@id='pantan')]"));
                inputTextBox.SendKeys(strTAN);
                //--
                inputTextBox = driver.FindElement(By.XPath("//input[(@id='mat-input-1')]"));
                inputTextBox.SendKeys(strTAN);
                //--
                inputTextBox = driver.FindElement(By.XPath("//input[(@id='phone')]"));
                inputTextBox.SendKeys(strMobile);
                //--
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();
                new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();
                ////btnGoOTP.Enabled = true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnGoOTP_Click
        private void btnGoOTP_Click(object sender, EventArgs e)
        {
            IncomeTaxConnect2025 objIncometaxConnect = new IncomeTaxConnect2025();
            TracesResponse objTracesResponse = new TracesResponse();
            try
            {
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                if (txtOTP.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter the OTP");
                    txtOTP.Select();
                    return;
                }
                //--
                string strTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 10);
                //
                objTracesResponse = objIncometaxConnect.DownloadCSIOTPValidation(txtOTP.Text, strTAN);
                if (objTracesResponse.Respons == enmResponse.Failed)
                {
                    txtOTP.Text = "";
                    cmnService.J_UserMessage(objTracesResponse.Message);
                    return;
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnAutoFillingChallan");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        private void CmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            strAssessmentYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))));
            //
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2026_27ID)
            {
                rbnIncometaxAct1961.Visible = true;
                rbnIncometaxAct2025.Visible = true;
                rbnIncometaxAct2025.Checked = true;
                LoadSection(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)));
            }
            else
            {
                string[] strSection = { "", "192","193","194","194A","194B","194BB","194DA","194LA","194LB","194LC","194LD",
                "194C","194D","194E","194EE","194F","194G","194H","194I","194IC","194J",
                "194LBA","194LBB","194LBC","194N","194O","195","196A","206C","194K",
                "196B","196C","196D","194P","194Q","196D(1A)","194R","194S",
                "194BA","194BA(2)","194B-P","194R-P","194S-P" };
                dmlService.J_PopulateComboBox(strSection, ref cmbSection, 1);
                //
                rbnIncometaxAct1961.Visible = false;
                rbnIncometaxAct2025.Visible = false;
            }
        }

        private void RbnIncometaxAct2025_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnIncometaxAct2025.Checked == true)
            {
                LoadSection(T_FinancialYearID.F2026_27ID);
            }
            else if (rbnIncometaxAct1961.Checked == true)
            {
                string[] strSection = { "", "192","193","194","194A","194B","194BB","194DA","194LA","194LB","194LC","194LD",
                "194C","194D","194E","194EE","194F","194G","194H","194I","194IC","194J",
                "194LBA","194LBB","194LBC","194N","194O","195","196A","206C","194K",
                "196B","196C","196D","194P","194Q","196D(1A)","194R","194S",
                "194BA","194BA(2)","194B-P","194R-P","194S-P" };
                dmlService.J_PopulateComboBox(strSection, ref cmbSection, 1);
                //
            }
        }


        #region LoadSections
        private void LoadSection(long FaYearId)
        {
            string[,] strLoadSectionMatrix = {{"SECTION_NAME_2 <> ''", "F", "SECTION_NO + ' [' + SECTION_NAME_2 + ']'", "F"},
                                              {"SECTION_NAME_2 = ''", "F", "", "T"}};
            strSQL = " SELECT SECTION_ID," +
            //"             SECTION_NO " +
            "       " + cmnService.J_SQLDBFormat(strLoadSectionMatrix, J_SQLColFormat.Case_End) + " AS SECTION_NO " +
            "      FROM   MST_SECTION " +
            "      WHERE  ASST_ID <= " + FaYearId + " " + // Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
            "      AND (VALID_UPTO_ASST_ID >= " + FaYearId + " OR VALID_UPTO_ASST_ID = 0) " +
            "      ORDER BY SECTION_NAME_2, SECTION_NO";
            dmlService.J_PopulateComboBox(strSQL, ref cmbSection, 1);
        }
        #endregion
    }
}

