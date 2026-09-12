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

using TDSMAN.Classes;
#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnDownloadCSIFilePopup : Form
    {

        #region System Generated Code
        public TrnDownloadCSIFilePopup(string TAN, string CSIFilePath)
        {
            strTAN = TAN;
            strCSIFilePath = CSIFilePath;
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        //--
        string strTAN = "", strCSIFilePath = "";
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


        #region BtnRefresh_Click 
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //if (BtnRefresh.Text == "&Download")
                //{
                //    BtnSave.Enabled = false;
                //    grpCaptcha.Visible = true;
                //    BtnRefresh.Text = "Continue";
                //    setCaptchaCode();
                //}
                //else
                //{
                    int iC = 0;
                    string strCSIFileDownloadFileMessage = "";
                    try
                    {
                        //----------------------------
                        if (TdsMan.T_CheckInternetConnectivty() == false)
                        {
                            cmnService.J_UserMessage("Internet Connectivity not found");
                            //BtnExit.Select();
                            return;
                        }
                        //
                        //if (cmbCompany.SelectedIndex <= 0)
                        //{
                        //    cmnService.J_UserMessage("Select the Company");
                        //    cmbCompany.Select();
                        //    return;
                        //}
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

                        if (string.IsNullOrEmpty(txtCaptchaCode.Text))
                        {
                            cmnService.J_UserMessage("Enter Captcha Code");
                            txtCaptchaCode.Select();
                            return;

                        }

                        //--
                        //string strCSIDownloadPath = cmnService.J_OpenFolderDialog();
                        //if (strCSIDownloadPath == "")
                        //{
                        //    cmnService.J_UserMessage("Select folder to save CSI file");
                        //    //BtnExit.Select();
                        //    return;
                        //}
                        //
                        if (TdsMan.T_CheckInternetConnectivty() == false)
                        {
                            cmnService.J_UserMessage("Internet Connectivity not found");
                            //BtnExit.Select();
                            return;
                        }
                        //
                        // CSI_DOWNLOAD:
                        //string strCSIFileName = Path.Combine(Path.GetDirectoryName(strCSIDownloadPath), cmnService.J_Right(cmbCompany.Text.Trim(), 10) + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                        //string strCSIFileName = Path.Combine(strCSIFilePath, strTAN.Trim() + string.Format("{0:ddMMyy}", System.DateTime.Now.Date)) + ".csi";
                        //--
                        //if (cmnService.J_IsFileExist(strCSIFileName) == true)
                        //{
                        //    if (cmnService.J_UserMessage(Path.GetFileName(strCSIFileName) + " already exist.\nDo you want to replace it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //    {
                        //        //BtnExit.Select();
                        //        return;
                        //    }
                        //}
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


                        parameters.Add("firstTime", "yes");
                        parameters.Add("submit", "     TAN Based View      ");
                        // parameters.Add("parameter3", "parameter 3 value.");

                        /* Always returns a byte[] array data as a response. */
                        var response_data = client.UploadValues(url, method, parameters);
                        var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                        parameters.Clear();
                        parameters.Add("TAN_NO", strTAN);
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
                        parameters.Add("submit", "Download Challan file");


                        // client.DownloadFile(new Uri(url), @"" + strCSIDownloadFilePath);

                        //parameters.Add("firstTime", "yes");
                        //parameters.Add("submit", "     TAN Based View      ");
                        // parameters.Add("parameter3", "parameter 3 value.");

                        /* Always returns a byte[] array data as a response. */
                        response_data = client.UploadValues(url, method, parameters);
                        client.Dispose();
                        // Parse the returned data (if any) if needed.
                        // var responseString = UnicodeEncoding.UTF8.GetString(response_data);

                        File.WriteAllBytes(strCSIFilePath, response_data);
                        FileInfo fInfo = new FileInfo(@"" + strCSIFilePath);
                        if (fInfo.Exists)
                        {
                            long size = fInfo.Length;
                            //
                            if (size <= 0)
                            {


                            }
                            //--------------------------------------------------------
                            string text = System.IO.File.ReadAllText(@"" + strCSIFilePath);
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
                                File.Delete(strCSIFilePath);
                                //----------------------------------------
                            }
                            else
                            {
                            //cmnService.J_UserMessage("CSI file downloaded");
                            //System.Diagnostics.Process.Start(strCSIDownloadPath);
                            //setCaptchaCode();
                            //BtnExit.Select();

                            this.Cursor = Cursors.Default;
                            TDSMAN.Classes.TDSMAN.T_pDownloadPath = strCSIFilePath;
                            this.Dispose();
                            this.Close();
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
                        //BtnExit.Select();
                    }
                //}
            }
            catch (Exception err)
            {
                //BtnSave.Enabled = false;
                //grpCaptcha.Visible = true;
                //BtnRefresh.Text = "Continue";
                cmnService.J_UserMessage("CSI file download FAILED !!");
                setCaptchaCode();
            }
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
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);

            }



        }
        #endregion

        #region TrnDownloadCSIFilePopup_Activated
        private void TrnDownloadCSIFilePopup_Activated(object sender, EventArgs e)
        {
            setCaptchaCode();
            //
            mskViewFileDownloadFrom.Select();
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            setCaptchaCode();
        }
        #endregion
        

    }
}
