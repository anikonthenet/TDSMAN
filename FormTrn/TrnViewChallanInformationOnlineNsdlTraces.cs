

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

using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewChallanInformationOnlineNsdlTraces : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnViewChallanInformationOnlineNsdlTraces()
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
        string strSQL = "";
        //--            
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

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
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
        #endregion

        //-- DOWNLOAD CSI FILE
        #region BtnRefresh_Click 
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (BtnRefresh.Text == "&Download")
            //    {
            //        BtnSave.Enabled = false;
            //        grpCaptcha.Visible = true;
            //        BtnRefresh.Text = "Continue";
            //        setCaptchaCode();
            //    }
            //    else
            //    {
            //        int iC = 0;
            //        string strCSIFileDownloadFileMessage = "";
            //        try
            //        {
            //            //----------------------------
            //            if (TdsMan.T_CheckInternetConnectivty() == false)
            //            {
            //                cmnService.J_UserMessage("Internet Connectivity not found");
            //                BtnExit.Select();
            //                return;
            //            }
            //            //
            //            if (cmbCompany.SelectedIndex <= 0)
            //            {
            //                cmnService.J_UserMessage("Select the Company");
            //                cmbCompany.Select();
            //                return;
            //            }
            //            //
            //            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
            //                return;
            //            //
            //            if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
            //            {
            //                cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
            //                mskViewFileDownloadFrom.Select();
            //                return;
            //            }
            //            //
            //            if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
            //                return;
            //            //
            //            if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
            //            {
            //                cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
            //                mskViewFileDownloadTo.Select();
            //                return;
            //            }
            //            //
            //            if (dtService.J_IsDateGreater(ref mskViewFileDownloadFrom, ref mskViewFileDownloadTo, J_ShowMessage.YES) == false)
            //                return;
            //            // FROM DATE & TO DATE DURATION 24 MONTHS
            //            if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
            //            {
            //                cmnService.J_UserMessage("Period selected should be within 24 months");
            //                mskViewFileDownloadFrom.Select();
            //                return;
            //            }
            //            //--
            //            if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            //            {
            //                cmnService.J_UserMessage("Enter Captcha Code");
            //                txtCaptchaCode.Select();
            //                return;

            //            }
            //            //--
            //            string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
            //            if (strCSIDownloadPath == "")
            //            {
            //                MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                BtnExit.Select();
            //                return;
            //            }
            //            //--
            //            if (TdsMan.T_CheckInternetConnectivty() == false)
            //            {
            //                cmnService.J_UserMessage("Internet Connectivity not found");
            //                BtnExit.Select();
            //                return;
            //            }
            //            //--
            //            // CSI_DOWNLOAD:
            //            //string strCSIFileName = Path.Combine(Path.GetDirectoryName(strCSIDownloadPath), cmnService.J_Right(cmbCompany.Text.Trim(), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
            //            string strCSIFileName = Path.Combine(strCSIDownloadPath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
            //            //--
            //            if (cmnService.J_IsFileExist(strCSIFileName) == true)
            //            {
            //                if (cmnService.J_UserMessage(Path.GetFileName(strCSIFileName) + " already exist.\nDo you want to replace it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //                {
            //                    BtnExit.Select();
            //                    return;
            //                }
            //            }
            //            //--
            //            //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) +
            //            //              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2) +
            //            //              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +
            //            //              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) +
            //            //              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2) +
            //            //              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +
            //            //              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewFileDownloadTo.Text, 4) +
            //            //              "&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
            //            //              "&submit=Download Challan file";
            //            //
            //            //WebClientCookies client = new WebClientCookies(objContainer);
            //            ////
            //            //client.DownloadFile(new Uri(url), @"" + strCSIFileName); //-- ANIK 2016/01/11
            //            //client.Dispose();
            //            //                    

            //            var url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
            //            var client = new WebClientCookies(objContainer);
            //            var method = "POST"; // If your endpoint expects a GET then do it.
            //            client.CookieContainer = objContainer;
            //            var parameters = new System.Collections.Specialized.NameValueCollection();
            //            //
            //            parameters.Add("firstTime", "yes");
            //            parameters.Add("submit", "     TAN Based View      ");
            //            // parameters.Add("parameter3", "parameter 3 value.");
            //            /* Always returns a byte[] array data as a response. */
            //            var response_data = client.UploadValues(url, method, parameters);
            //            var responseString = UnicodeEncoding.UTF8.GetString(response_data);

            //            parameters.Clear();
            //            parameters.Add("TAN_NO", cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
            //            parameters.Add("TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
            //            parameters.Add("TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
            //            parameters.Add("TAN_FROM_DT_YY", cmnService.J_Right(mskViewFileDownloadFrom.Text, 4));
            //            parameters.Add("TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
            //            parameters.Add("TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));
            //            parameters.Add("TAN_TO_DT_YY", cmnService.J_Right(mskViewFileDownloadTo.Text, 4));
            //            parameters.Add("HID_IMG_TXT", txtCaptchaCode.Text.Trim());
            //            parameters.Add("HIDDEN_TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
            //            parameters.Add("HIDDEN_TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
            //            parameters.Add("HIDDEN_TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
            //            parameters.Add("HIDDEN_TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));

            //            parameters.Add("HIDDEN_TAN_TO_DT_YY", "");
            //            parameters.Add("appUser", "T");
            //            parameters.Add("submit", "Download Challan file");// "Download Challan file");


            //            // client.DownloadFile(new Uri(url), @"" + strCSIDownloadFilePath);

            //            //parameters.Add("firstTime", "yes");
            //            //parameters.Add("submit", "     TAN Based View      ");
            //            // parameters.Add("parameter3", "parameter 3 value.");

            //            /* Always returns a byte[] array data as a response. */
            //            response_data = client.UploadValues(url, method, parameters);
            //            client.Dispose();
            //            // Parse the returned data (if any) if needed.
            //            // var responseString = UnicodeEncoding.UTF8.GetString(response_data);

            //            File.WriteAllBytes(strCSIFileName, response_data);
            //            FileInfo fInfo = new FileInfo(@"" + strCSIFileName);
            //            if (fInfo.Exists)
            //            {
            //                long size = fInfo.Length;
            //                //
            //                if (size <= 0)
            //                {


            //                }
            //                //--------------------------------------------------------
            //                string text = System.IO.File.ReadAllText(@"" + strCSIFileName);
            //                if (text.Contains("<HTML>") == true)
            //                {
            //                    if (text.ToUpper().Contains("ERROR - TEXT DOES NOT MATCH. PLEASE ENTER NEW TEXT") == true)
            //                    {
            //                        strCSIFileDownloadFileMessage = "Text does not match. Please enter new text";
            //                        setCaptchaCode();

            //                    }
            //                    else if (text.ToUpper().Contains("RECORD NOT FOUND") == true)
            //                    {
            //                        strCSIFileDownloadFileMessage = "No Record Found.";
            //                        setCaptchaCode();
            //                    }
            //                    else if (text.ToUpper().Contains("SITE UNDER MAINTENANCE") == true)
            //                    {
            //                        strCSIFileDownloadFileMessage = "Site Under Maintenance";
            //                        setCaptchaCode();
            //                    }
            //                    else if (text.ToUpper().Contains("PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE") == true)
            //                    {
            //                        strCSIFileDownloadFileMessage = "Site Under Maintenance";
            //                        setCaptchaCode();
            //                    }
            //                    else
            //                    {
            //                        strCSIFileDownloadFileMessage = "File Download Failed. Please Try again";
            //                        setCaptchaCode();
            //                    }
            //                    //----------------------------------------
            //                    File.Delete(strCSIFileName);
            //                    //----------------------------------------
            //                }
            //                else
            //                {
            //                    cmnService.J_UserMessage("CSI file downloaded");
            //                    System.Diagnostics.Process.Start(strCSIDownloadPath);
            //                    setCaptchaCode();
            //                    BtnExit.Select();
            //                }

            //            }
            //            else
            //            {
            //                strCSIFileDownloadFileMessage = "File Download Failed. Please try again";
            //                setCaptchaCode();
            //            }
            //            //
            //            cmnService.J_UserMessage(strCSIFileDownloadFileMessage);
            //            //--
            //        }
            //        catch (Exception err)
            //        {
            //            cmnService.J_UserMessage(err.Message);
            //            BtnExit.Select();
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    BtnSave.Enabled = false;
            //    grpCaptcha.Visible = true;
            //    BtnRefresh.Text = "Continue";
            //    setCaptchaCode();
            //}
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (BtnRefresh.Text.ToUpper().Trim() == "CONTINUE")
            {
                grpNSDLCaptcha.Visible = false;
                grpIncomeTaxLoginPassword.Visible = false;
                lblBottomMessage.Text = "Security introduced by IT Dept. for CSI file download w.e.f. from Dec 2016. Enter correct text & continue.";            
                BtnSave.Enabled = true;
                BtnRefresh.Text = "&Download";
                //txtCaptchaCode.Text = "";
                ////
                //txtCaptchaCode.Select();                 
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

        #region BtnRefresh_MouseClick
        private void BtnRefresh_MouseClick(object sender, MouseEventArgs e)
        {
            if (BtnRefresh.Text == "&Download")
            {
                if (e.Button == MouseButtons.Left)
                    ctxtMnuStripDownload.Show(BtnRefresh, new Point(e.X, e.Y));
            }
            else if (BtnRefresh.Text == "Continue")
            {
                string strCSIFileDownloadFileMessage = "";
                if (grpIncomeTaxLoginPassword.Visible==true)
                {
                    try
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
                        if (txtPassword.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Please enter a valid Password");
                            txtPassword.Select();
                            return;
                        }
                        string strTAN = "", strPassword="", strFromDate="", strToDate="";
                        //
                        strTAN      = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10);
                        strPassword = txtPassword.Text;
                        strFromDate = cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) + "-" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +"-" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2);
                        strToDate = cmnService.J_Right(mskViewFileDownloadTo.Text, 4) + "-" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +"-" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2);
                        //strToDate = string.Format("{0:yyyy-mm-dd}", mskViewFileDownloadTo.Text);
                        SaveTANData(strTAN, strPassword);
                        //
                        string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
                        if (strCSIDownloadPath == "")
                        {
                            //MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmnService.J_UserMessage("Select folder to save CSI file", MessageBoxIcon.Information);
                            BtnExit.Select();
                            return;
                        }
                        //
                        //
                        // CSI_DOWNLOAD:
                        //string strCSIFileName = Path.Combine(strCSIDownloadPath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                        string strCSIFileName = Path.Combine(strCSIDownloadPath, strTAN + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";

                        IncomeTaxConnect2025 objcon = new IncomeTaxConnect2025();
                        TracesResponse objRes = objcon.DownloadCSI(strTAN, strPassword, strFromDate, strToDate);

                        if (objRes.Respons == enmResponse.Failed)
                        {
                            MessageBox.Show(objRes.Message);
                            return;
                        }

                        //===============================================================================================================
                        using (FileStream fs = new FileStream(strCSIFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                        {
                            StreamWriter write = new StreamWriter(fs);
                            write.Write(objRes.Message);
                            write.Flush();
                            write.Close();
                            fs.Close();
                        }
                        //--
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
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                else if (grpNSDLCaptcha.Visible == true)
                {
                    int iC = 0;
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
                            //MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmnService.J_UserMessage("Select folder to save CSI file", MessageBoxIcon.Information);
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

                        //var url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
                        var url = "https://tin.tin.proteantech.in/oltas/servlet/TanSearch";
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
        }
        #endregion

        #region MnuFromNSDL_Click
        private void MnuFromNSDL_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnRefresh.Text == "&Download")
                {
                    BtnSave.Enabled = false;
                    grpNSDLCaptcha.Visible = true;
                    BtnRefresh.Text = "Continue";
                    setCaptchaCode();
                }
                else
                {
                    //int iC = 0;
                    //string strCSIFileDownloadFileMessage = "";
                    //try
                    //{
                    //    //----------------------------
                    //    if (TdsMan.T_CheckInternetConnectivty() == false)
                    //    {
                    //        cmnService.J_UserMessage("Internet Connectivity not found");
                    //        BtnExit.Select();
                    //        return;
                    //    }
                    //    //
                    //    if (cmbCompany.SelectedIndex <= 0)
                    //    {
                    //        cmnService.J_UserMessage("Select the Company");
                    //        cmbCompany.Select();
                    //        return;
                    //    }
                    //    //
                    //    if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadFrom, "Challan Date From - Cannot be Blank") == true)
                    //        return;
                    //    //
                    //    if (dtService.J_IsDateValid(mskViewFileDownloadFrom) == false)
                    //    {
                    //        cmnService.J_UserMessage("Incorrect Format of the Challan Date From");
                    //        mskViewFileDownloadFrom.Select();
                    //        return;
                    //    }
                    //    //
                    //    if (dtService.J_IsBlankDateCheck(ref mskViewFileDownloadTo, "Challan Date To - Cannot be Blank") == true)
                    //        return;
                    //    //
                    //    if (dtService.J_IsDateValid(mskViewFileDownloadTo) == false)
                    //    {
                    //        cmnService.J_UserMessage("Incorrect Format of the Challan Date To");
                    //        mskViewFileDownloadTo.Select();
                    //        return;
                    //    }
                    //    //
                    //    if (dtService.J_IsDateGreater(ref mskViewFileDownloadFrom, ref mskViewFileDownloadTo, J_ShowMessage.YES) == false)
                    //        return;
                    //    // FROM DATE & TO DATE DURATION 24 MONTHS
                    //    if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
                    //    {
                    //        cmnService.J_UserMessage("Period selected should be within 24 months");
                    //        mskViewFileDownloadFrom.Select();
                    //        return;
                    //    }
                    //    //--
                    //    if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                    //    {
                    //        cmnService.J_UserMessage("Enter Captcha Code");
                    //        txtCaptchaCode.Select();
                    //        return;

                    //    }
                    //    //--
                    //    string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
                    //    if (strCSIDownloadPath == "")
                    //    {
                    //        MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        BtnExit.Select();
                    //        return;
                    //    }
                    //    //--
                    //    if (TdsMan.T_CheckInternetConnectivty() == false)
                    //    {
                    //        cmnService.J_UserMessage("Internet Connectivity not found");
                    //        BtnExit.Select();
                    //        return;
                    //    }
                    //    //--
                    //    // CSI_DOWNLOAD:
                    //    //string strCSIFileName = Path.Combine(Path.GetDirectoryName(strCSIDownloadPath), cmnService.J_Right(cmbCompany.Text.Trim(), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                    //    string strCSIFileName = Path.Combine(strCSIDownloadPath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                    //    //--
                    //    if (cmnService.J_IsFileExist(strCSIFileName) == true)
                    //    {
                    //        if (cmnService.J_UserMessage(Path.GetFileName(strCSIFileName) + " already exist.\nDo you want to replace it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    //        {
                    //            BtnExit.Select();
                    //            return;
                    //        }
                    //    }
                    //    //--
                    //    //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) +
                    //    //              "&TAN_FROM_DT_DD=" + cmnService.J_Left(mskViewFileDownloadFrom.Text, 2) +
                    //    //              "&TAN_FROM_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2) +
                    //    //              "&TAN_FROM_DT_YY=" + cmnService.J_Right(mskViewFileDownloadFrom.Text, 4) +
                    //    //              "&TAN_TO_DT_DD=" + cmnService.J_Left(mskViewFileDownloadTo.Text, 2) +
                    //    //              "&TAN_TO_DT_MM=" + cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2) +
                    //    //              "&TAN_TO_DT_YY=" + cmnService.J_Right(mskViewFileDownloadTo.Text, 4) +
                    //    //              "&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
                    //    //              "&submit=Download Challan file";
                    //    //
                    //    //WebClientCookies client = new WebClientCookies(objContainer);
                    //    ////
                    //    //client.DownloadFile(new Uri(url), @"" + strCSIFileName); //-- ANIK 2016/01/11
                    //    //client.Dispose();
                    //    //                    

                    //    var url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
                    //    var client = new WebClientCookies(objContainer);
                    //    var method = "POST"; // If your endpoint expects a GET then do it.
                    //    client.CookieContainer = objContainer;
                    //    var parameters = new System.Collections.Specialized.NameValueCollection();
                    //    //
                    //    parameters.Add("firstTime", "yes");
                    //    parameters.Add("submit", "     TAN Based View      ");
                    //    // parameters.Add("parameter3", "parameter 3 value.");
                    //    /* Always returns a byte[] array data as a response. */
                    //    var response_data = client.UploadValues(url, method, parameters);
                    //    var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                    //    parameters.Clear();
                    //    parameters.Add("TAN_NO", cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
                    //    parameters.Add("TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
                    //    parameters.Add("TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
                    //    parameters.Add("TAN_FROM_DT_YY", cmnService.J_Right(mskViewFileDownloadFrom.Text, 4));
                    //    parameters.Add("TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
                    //    parameters.Add("TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));
                    //    parameters.Add("TAN_TO_DT_YY", cmnService.J_Right(mskViewFileDownloadTo.Text, 4));
                    //    parameters.Add("HID_IMG_TXT", txtCaptchaCode.Text.Trim());
                    //    parameters.Add("HIDDEN_TAN_FROM_DT_DD", cmnService.J_Left(mskViewFileDownloadFrom.Text, 2));
                    //    parameters.Add("HIDDEN_TAN_FROM_DT_MM", cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2));
                    //    parameters.Add("HIDDEN_TAN_TO_DT_DD", cmnService.J_Left(mskViewFileDownloadTo.Text, 2));
                    //    parameters.Add("HIDDEN_TAN_TO_DT_MM", cmnService.J_Mid(mskViewFileDownloadTo.Text, 3, 2));

                    //    parameters.Add("HIDDEN_TAN_TO_DT_YY", "");
                    //    parameters.Add("appUser", "T");
                    //    parameters.Add("submit", "Download Challan file");// "Download Challan file");


                    //    // client.DownloadFile(new Uri(url), @"" + strCSIDownloadFilePath);

                    //    //parameters.Add("firstTime", "yes");
                    //    //parameters.Add("submit", "     TAN Based View      ");
                    //    // parameters.Add("parameter3", "parameter 3 value.");

                    //    /* Always returns a byte[] array data as a response. */
                    //    response_data = client.UploadValues(url, method, parameters);
                    //    client.Dispose();
                    //    // Parse the returned data (if any) if needed.
                    //    // var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                    //    File.WriteAllBytes(strCSIFileName, response_data);
                    //    FileInfo fInfo = new FileInfo(@"" + strCSIFileName);
                    //    if (fInfo.Exists)
                    //    {
                    //        long size = fInfo.Length;
                    //        //
                    //        if (size <= 0)
                    //        {


                    //        }
                    //        //--------------------------------------------------------
                    //        string text = System.IO.File.ReadAllText(@"" + strCSIFileName);
                    //        if (text.Contains("<HTML>") == true)
                    //        {
                    //            if (text.ToUpper().Contains("ERROR - TEXT DOES NOT MATCH. PLEASE ENTER NEW TEXT") == true)
                    //            {
                    //                strCSIFileDownloadFileMessage = "Text does not match. Please enter new text";
                    //                setCaptchaCode();

                    //            }
                    //            else if (text.ToUpper().Contains("RECORD NOT FOUND") == true)
                    //            {
                    //                strCSIFileDownloadFileMessage = "No Record Found.";
                    //                setCaptchaCode();
                    //            }
                    //            else if (text.ToUpper().Contains("SITE UNDER MAINTENANCE") == true)
                    //            {
                    //                strCSIFileDownloadFileMessage = "Site Under Maintenance";
                    //                setCaptchaCode();
                    //            }
                    //            else if (text.ToUpper().Contains("PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE PLEASE SELECT VALID DATE, YOU HAVE SELECTED FUTURE DATE") == true)
                    //            {
                    //                strCSIFileDownloadFileMessage = "Site Under Maintenance";
                    //                setCaptchaCode();
                    //            }
                    //            else
                    //            {
                    //                strCSIFileDownloadFileMessage = "File Download Failed. Please Try again";
                    //                setCaptchaCode();
                    //            }
                    //            //----------------------------------------
                    //            File.Delete(strCSIFileName);
                    //            //----------------------------------------
                    //        }
                    //        else
                    //        {
                    //            cmnService.J_UserMessage("CSI file downloaded");
                    //            System.Diagnostics.Process.Start(strCSIDownloadPath);
                    //            setCaptchaCode();
                    //            BtnExit.Select();
                    //        }

                    //    }
                    //    else
                    //    {
                    //        strCSIFileDownloadFileMessage = "File Download Failed. Please try again";
                    //        setCaptchaCode();
                    //    }
                    //    //
                    //    cmnService.J_UserMessage(strCSIFileDownloadFileMessage);
                    //    //--
                    //}
                    //catch (Exception err)
                    //{
                    //    cmnService.J_UserMessage(err.Message);
                    //    BtnExit.Select();
                    //}
                }
            }
            catch (Exception err)
            {
                BtnSave.Enabled = false;
                grpNSDLCaptcha.Visible = true;
                BtnRefresh.Text = "Continue";
                setCaptchaCode();
            }
        }
        #endregion


        #region MnuFromTraces_Click
        private void MnuFromTraces_Click(object sender, EventArgs e)
        {
            if (BtnRefresh.Text == "&Download")
            {
                BtnSave.Enabled = false;
                grpIncomeTaxLoginPassword.Visible = true;
                btnGoITView.Visible = false;
                txtPassword.Select();
                BtnRefresh.Text = "Continue";
                //setCaptchaCode();
                //-- GET SAVED PASSWORD
                if (cmbCompany.SelectedIndex > 0)
                    txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO ='" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + "'"));
                else
                    txtPassword.Text = "";
                //--
            }
        }
        #endregion


        #region DownloadCSIFiles
        private void DownloadCSIFiles()
        {
            try
            {


                string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
                if (strCSIDownloadPath == "")
                {
                    //MessageBox.Show("Select folder to save CSI file", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmnService.J_UserMessage("Select folder to save CSI file",MessageBoxIcon.Information);
                    BtnExit.Select();
                    return;
                }
                //
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //
                // CSI_DOWNLOAD:
                //string strCSIFileName = Path.Combine(strCSIDownloadPath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                string strCSIFileName = Path.Combine(strCSIDownloadPath, "CALP08143C" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";

                IncomeTaxConnect2025 objcon = new IncomeTaxConnect2025();
                TracesResponse objRes = objcon.DownloadCSI("CALP08143C", "Pdsinfo@6", "2022-12-01", "2022-12-31");

                if (objRes.Respons == enmResponse.Failed)
                {
                    MessageBox.Show(objRes.Message);
                    return;
                }

                //===============================================================================================================
                using (FileStream fs = new FileStream(strCSIFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    StreamWriter write = new StreamWriter(fs);
                    write.Write(objRes.Message);
                    write.Flush();
                    write.Close();
                    fs.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }
        #endregion

        #region DownloadCSIFilesBrowser
        private void DownloadCSIFilesBrowser()
        {
            //string strOS = TdsMan.GetOSVersion();
            ////--
            //string strBrowser = "";
            //if (strOS == "XP")
            //    strBrowser = TdsMan.GetSystemDefaultBrowserXP();
            //else
            //    strBrowser = TdsMan.GetSystemDefaultBrowser();
            ////--
            //IWebDriver driver = null;
            ////====================================================
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
            //else
            //{
            //    cmnService.J_UserMessage("Please make your default browser to 'Chrome'/'Edge'/'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose as per choice and Set the program as default");
            //    return;
            //}
            //////====================================================
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            //driver.Navigate().GoToUrl("https://eportal.incometax.gov.in/iec/foservices/#/login");

            //IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            //inputTextBox.SendKeys("CALP08143C");

            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            //// System.Threading.Thread.Sleep(1000);
            //new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            //inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            //inputTextBox.SendKeys("Pdsinfo@6");

            //driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            //System.Threading.Thread.Sleep(1000);
            ////====================================================
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //Actions action = new Actions(driver);

            //var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            //action.MoveToElement(Menu).Build().Perform();


            //var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'e-Pay Tax')]")));
            //SubmenuElement.Click();
            ////====================================================   
            //System.Threading.Thread.Sleep(5000);

            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-tab-label-0-2"))).Click();

            //var Filter = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ml-auto defaultButton filterButton filterMobile overflow-auto justify-content-end ng-star-inserted']")));
            //Filter.Click();

            ////========================================================================
            ////---ASSESSMENT YEAR
            ////new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-7"))).Click();
            ////driver.FindElement(By.XPath("//span[contains(text(),'2022-23')]")).Click();


            //////--------TYPE OF PAYMENT
            ////new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-8"))).Click();
            ////driver.FindElement(By.XPath("//span[contains(text(),'200')]")).Click();

            //////--------PAYMENT DATE
            //// FROM
            ////new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("frompayment"))).Click();
            ////driver.FindElement(By.XPath("//span[contains(text(),'" + cmbQuarter.Text + "')]")).Click();

            ////===========================================================================             
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("document.getElementById('frompayment').removeAttribute('readonly',0);");

            //inputTextBox = driver.FindElement(By.Id("frompayment"));
            //inputTextBox.SendKeys("01-Dec-2022");

            ////TO           
            //js.ExecuteScript("document.getElementById('topayment').removeAttribute('readonly',0);");
            //inputTextBox = driver.FindElement(By.Id("topayment"));
            //inputTextBox.SendKeys("31-Dec-2022");
            ////============================================================================
            ////FilterS button
            //var Filterr = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='filter-section mt-3 mr-3 ng-star-inserted']/form[1]/div[1]/div[3]/div[2]/button")));
            //Filterr.Click();

            ////-- Download 
            ////==================================================
            //new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[starts-with(@class,'normal-button-secondary iconBefore downloadIcon ng-star-inserted')][text()='Download Challan File']"))).Click();

        }
        #endregion



        #region MnuFromNSDLView_Click
        private void MnuFromNSDLView_Click(object sender, EventArgs e)
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
                //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                //service.HideCommandPromptWindow = true;

                //var options = new ChromeOptions();
                //// options.AddAdditionalCapability("useAutomationExtension", false);
                //options.AddExcludedArgument("enable-automation");
                ////options.AddArgument("--incognito");
                ////// options.AddArgument("--window-position=-32000,-32000");
                ////options.AddArgument("--start-maximized");

                //driver = new ChromeDriver(service, options);
                ////////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////////return;
                Environment.SetEnvironmentVariable("SE_MANAGER_DISABLE", "1");

                string driverPath = Application.StartupPath.ToString();// @"D:\Running Projects\NETProj TDSMAN - F.Y 2024-25 - VS2019\TDSMAN (nxt updt)\bin\Debug\";

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
                service.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-popup-blocking");
                options.AddArgument("--disable-notifications");
                options.AddArgument("--disable-first-run-ui");
                options.AddArgument("--no-default-browser-check");
                options.AddArgument("--remote-debugging-port=0");
                options.AddArgument("--headless=new");      //critical line
                options.AddArgument("--window-size=1920,1080");

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
                //Environment.SetEnvironmentVariable("SE_MANAGER_DISABLE", "1");
                //string driverPath = Application.StartupPath.ToString();// @"D:\Running Projects\NETProj TDSMAN - F.Y 2024-25 - VS2019\TDSMAN (nxt updt)\bin\Debug\";

                //EdgeDriverService service = EdgeDriverService.CreateDefaultService(driverPath);
                //service.HideCommandPromptWindow = true;

                //EdgeOptions options = new EdgeOptions();
                //options.AddArgument("--no-sandbox");
                //options.AddArgument("--disable-dev-shm-usage");
                //options.AddArgument("--disable-extensions");
                //options.AddArgument("--disable-popup-blocking");
                //options.AddArgument("--disable-notifications");
                //options.AddArgument("--disable-first-run-ui");
                //options.AddArgument("--no-default-browser-check");

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
            //driver.Navigate().GoToUrl("https://tin.tin.nsdl.com/oltas/servlet/TanSearch"); 

            //driver.Navigate().GoToUrl("https://tin.tin.nsdl.com/oltas/index.html"); 
            driver.Navigate().GoToUrl("https://tin.tin.proteantech.in/oltas/index.html");

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

        }

        #endregion


        #region MnuFromITView_Click
        private void MnuFromITView_Click(object sender, EventArgs e)
        {
            grpIncomeTaxLoginPassword.Visible = true;
            btnGoITView.Visible = true;
            txtPassword.Select();
            //-- GET SAVED PASSWORD
            if (cmbCompany.SelectedIndex > 0)
            {
                txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_AADHAAR WHERE TAN_NO ='" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10) + "'"));
                btnGoITView.Select();
            }
            else
                txtPassword.Text = "";
            //--
        }
        #endregion


        #region BtnSave_MouseClick
        private void BtnSave_MouseClick(object sender, MouseEventArgs e)
        {
            grpIncomeTaxLoginPassword.Visible = false;
            if (e.Button == MouseButtons.Left)
                ctxtMnuStripView.Show(BtnSave, new Point(e.X, e.Y));
        }
        #endregion

        #region BtnGoITView_Click
        private void BtnGoITView_Click(object sender, EventArgs e)
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
            int intFAYear2025 = 0;
            if(cmnService.J_ReturnInt16Value(cmnService.J_Right(mskViewFileDownloadFrom.Text,4)) >= 2026 
                & cmnService.J_ReturnInt16Value(cmnService.J_Mid(mskViewFileDownloadFrom.Text, 3, 2)) >= 4)
            {
                intFAYear2025 = 1;
            }
            // FROM DATE & TO DATE DURATION 24 MONTHS
            if (TdsMan.T_DateDiff(T_DATE_DIFF.MONTH, dtService.J_ConvertddMMyyyy(mskViewFileDownloadFrom), dtService.J_ConvertddMMyyyy(mskViewFileDownloadTo)) > 24)
            {
                cmnService.J_UserMessage("Period selected should be within 24 months");
                mskViewFileDownloadFrom.Select();
                return;
            }
            //
            if (txtPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid Password");
                txtPassword.Select();
                return;
            }
            string strTAN = "", strPassword = "", strFromDate = "", strToDate = "";
            //
            strTAN = cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10);
            strPassword = txtPassword.Text;
            strFromDate = string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(mskViewFileDownloadFrom.Text)).ToUpper();
            strToDate = string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(mskViewFileDownloadTo.Text)).ToUpper();
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

                ////options.AddUserProfilePreference("disable-popup-blocking", true);

                ////options.AddArgument("--disable-blink-features=AutomationControlled");

                ////driver = new ChromeDriver(service, options);
                ////////////cmnService.J_UserMessage("Your default browser is set to 'Chrome', please set the default browser to 'Internet Explorer'.\nGo to Control Panel > Programs > Default Programs > Set your default programs > Choose Internet Explorer and Set this program as default");
                ////////////return;
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
                //Environment.SetEnvironmentVariable("SE_MANAGER_DISABLE", "1");
                //string driverPath = Application.StartupPath.ToString();// @"D:\Running Projects\NETProj TDSMAN - F.Y 2024-25 - VS2019\TDSMAN (nxt updt)\bin\Debug\";

                //EdgeDriverService service = EdgeDriverService.CreateDefaultService(driverPath);
                //service.HideCommandPromptWindow = true;

                //EdgeOptions options = new EdgeOptions();
                //options.AddArgument("--no-sandbox");
                //options.AddArgument("--disable-dev-shm-usage");
                //options.AddArgument("--disable-extensions");
                //options.AddArgument("--disable-popup-blocking");
                //options.AddArgument("--disable-notifications");
                //options.AddArgument("--disable-first-run-ui");
                //options.AddArgument("--no-default-browser-check");

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
            
            IWebElement inputTextBox = driver.FindElement(By.XPath("//input[(@name='panAdhaarUserId')]"));
            inputTextBox.SendKeys(strTAN);

            System.Threading.Thread.Sleep(2000);
            new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Continue')]"))).Click();

            System.Threading.Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-checkbox[(@id='passwordCheckBox')]"))).Click();

            inputTextBox = driver.FindElement(By.XPath("//input[(@type='password' and @name='loginPasswordField')]"));
            inputTextBox.SendKeys(strPassword);

            //driver.FindElement(By.XPath("//span[contains(text(),'Continue')]")).Click();

            System.Threading.Thread.Sleep(4000);
            //====================================================
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[span[contains(text(),'Continue')]]"))).Click();

            System.Threading.Thread.Sleep(2000);
            ////Actions action = new Actions(driver);

            ////action.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();

            ////System.Threading.Thread.Sleep(2000);
            ////var Menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navBar- 1")));
            ////action.MoveToElement(Menu).Build().Perform();

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
            var SubmenuElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'e-Pay Tax')]")));
            SubmenuElement.Click();
            //====================================================   
            System.Threading.Thread.Sleep(2000);
            // FY 2026-27 onwards > New Act 2025
            if (intFAYear2025 == 1)
            {
                var act2025 = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//mat-radio-button[.//div[contains(text(),'Income-tax Act, 2025')]]//input")));

                act2025.Click();
            }
            else
            {
                var act1961 = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//mat-radio-button[.//div[contains(text(),'Income-tax Act, 1961')]]//input")));

                act1961.Click();
            }
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-tab-label-0-2"))).Click();
            System.Threading.Thread.Sleep(2000);
            var continueBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Continue')]")));

            continueBtn.Click();
            System.Threading.Thread.Sleep(2000);
            var Filter = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ml-auto defaultButton filterButton filterMobile overflow-auto justify-content-end ng-star-inserted']")));
            Filter.Click();

            //========================================================================
            //---ASSESSMENT YEAR
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-7"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'2022-23')]")).Click();


            ////--------TYPE OF PAYMENT
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("mat-select-8"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'200')]")).Click();

            ////--------PAYMENT DATE
            // FROM
            //new WebDriverWait(driver, TimeSpan.FromMinutes(5)).Until(ExpectedConditions.ElementToBeClickable(By.Id("frompayment"))).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'" + cmbQuarter.Text + "')]")).Click();

            //===========================================================================             
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('frompayment').removeAttribute('readonly',0);");

            inputTextBox = driver.FindElement(By.Id("frompayment"));
            inputTextBox.SendKeys(strFromDate);

            //TO           
            js.ExecuteScript("document.getElementById('topayment').removeAttribute('readonly',0);");
            inputTextBox = driver.FindElement(By.Id("topayment"));
            inputTextBox.SendKeys(strToDate);
            //============================================================================
            //FilterS button
            var Filterr = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='filter-section mt-3 mr-3 ng-star-inserted']/form[1]/div[1]/div[3]/div[2]/button")));
            Filterr.Click();

            //-- Download 
            //==================================================
            new WebDriverWait(driver, TimeSpan.FromMinutes(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[starts-with(@class,'normal-button-secondary iconBefore downloadIcon ng-star-inserted')][text()='Download Challan File']"))).Click();

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
            strSQL = "SELECT COUNT(*) FROM MST_TAN_AADHAAR WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(TAN) + "' ";
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

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0120", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}

