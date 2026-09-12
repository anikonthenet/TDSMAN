using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.Security;

using TDSMAN.Classes;
using TDSMAN.FormWeb;

namespace TDSMAN.FormTrn
{
    public partial class TrnTINNSDLCaptcha : Form
    {
        #region Default Constructor
        public TrnTINNSDLCaptcha()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Variables Declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strFolderPath = string.Empty;

        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;
        CookieContainer objContainer = new CookieContainer();

        #endregion

        #region User Defined Events

        #region TrnTINNSDLCaptcha_Activated
        private void TrnTINNSDLCaptcha_Activated(object sender, EventArgs e)
        {
            try
            {
                lblBottomMessage.Text = "Security introduced by IT Dept. for CSI file download w.e.f. Dec 2016. Enter correct text & continue.";
                setCaptchaCode();
                //-- 2018/11/15 - ANIK
                //if (TDSMAN.Classes.TDSMAN.T_ENABLE_NSDL_TEXT_CAPS_LOCK == true)
                //    txtCaptchaCode.CharacterCasing = CharacterCasing.Upper;
                //else
                //    txtCaptchaCode.CharacterCasing = CharacterCasing.Normal; 
            }
            catch 
            {
                //setCaptchaCode();
            }
        }
        #endregion      
       
        #region btnDownloadFile_Click
        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            int iC = 0;
            try
            {
                //--
                if (txtCaptchaCode.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter the Captcha as shown.");
                    txtCaptchaCode.Select();
                    return;
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
            CSI_DOWNLOAD:
                //string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=" + TAN +
                //                "&TAN_FROM_DT_DD=" + FromDD +
                //                "&TAN_FROM_DT_MM=" + FromMM +
                //                "&TAN_FROM_DT_YY=" + FromYY +
                //                "&TAN_TO_DT_DD=" + ToDD +
                //                "&HID_IMG_TXT=" + txtCaptchaCode.Text.Trim() +
                //                "&TAN_TO_DT_MM=" + ToMM +
                //                "&TAN_TO_DT_YY=" + ToYY +
                //                "&submit=Download Challan file";
                ////
                //WebClientCookies client = new WebClientCookies(objContainer);
                ////
                //client.CookieContainer = objContainer;
                //client.DownloadFile(new Uri(url), @"" + strCSIDownloadFilePath);
                //client.Dispose();


               // string url = "https://tin.tin.nsdl.com/oltas/servlet/TanSearch";
                //
               // WebClientCookies client = new WebClientCookies(objContainer);
               // //
               // client.CookieContainer = objContainer;
               //string data =  client.DownloadString(url);
               // client.Dispose();

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
                parameters.Add("TAN_NO", TAN);
                parameters.Add("TAN_FROM_DT_DD", FromDD);
                parameters.Add("TAN_FROM_DT_MM", FromMM);
                parameters.Add("TAN_FROM_DT_YY", FromYY);
                parameters.Add("TAN_TO_DT_DD", ToDD);
                parameters.Add("TAN_TO_DT_MM", ToMM);

                parameters.Add("TAN_TO_DT_YY", ToYY);
                parameters.Add("HID_IMG_TXT", txtCaptchaCode.Text.Trim());
                parameters.Add("HIDDEN_TAN_FROM_DT_DD", FromDD);
                parameters.Add("HIDDEN_TAN_FROM_DT_MM", FromMM);
                parameters.Add("HIDDEN_TAN_TO_DT_DD", ToDD);
                parameters.Add("HIDDEN_TAN_TO_DT_MM", ToMM);


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

                File.WriteAllBytes(strCSIDownloadFilePath, response_data);



                // return;

                //--
                long lngTimeOut = 0;
                for (int i = 0; i <= 2; i++)
                {
                    // GET FILE SIZE
                    FileInfo fInfo = new FileInfo(@"" + strCSIDownloadFilePath);
                    long size = fInfo.Length;
                    //
                    fInfo.Refresh();
                    //
                    if (size > 0)
                        goto CHECK_FILE;
                    //
                    lngTimeOut = lngTimeOut + 1;
                    //
                    if (lngTimeOut > 152912)
                        goto CHECK_FILE;

                    i = 0;
                    i++;
                }
            //--
            CHECK_FILE:
                for (int i = iC; i <= 3; i++)
                {
                    iC = iC + 1;
                    //
                    if (iC == 3)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Some error occurred while downloading CSI.\n Please try after some time.");
                        return;
                    }
                    //
                    //err = 1;
                    System.Threading.Thread.Sleep(3000);
                    //
                    string text = System.IO.File.ReadAllText(@"" + strCSIDownloadFilePath);
                    //
                    if (text.Contains("Text does not match.") == true) //-- 2016/12/05
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Captcha entered does not match.\nPlease enter the text click <Continue> again.");
                        txtCaptchaCode.Select();
                        return;
                    }
                    else if (text.Contains("<HTML>") == true)
                    {
                        //if (text.ToUpper().Contains("PLEASE ENTER VALID TAN") == true) //Invalid value entered for From Date
                        if (text.ToUpper().Contains("INVALID VALUE ENTERED FOR FROM DATE") == true)
                        {
                            this.Cursor = Cursors.Default;
                            this.Dispose();
                            this.Close();
                            //cmnService.J_UserMessage("No CSI file was downloaded as the TAN number provided for the company is invalid.\nPlease correct the TAN number and try again.");
                            cmnService.J_UserMessage("Auto download of CSI file has failed, now manual downloader will open.");
                            TrnDownloadCSIFilePopup DownloadCSIFilePopup = new TrnDownloadCSIFilePopup(TAN, strCSIDownloadFilePath);
                            DownloadCSIFilePopup.ShowDialog();
                            this.Refresh();
                            return;
                        }
                        else if (text.ToUpper().Contains("RECORD NOT FOUND") == true)
                        {
                            this.Cursor = Cursors.Default;
                            this.Dispose();
                            this.Close();
                            //cmnService.J_UserMessage("CSI file was not downloaded.\n\nPlease check the following:\n1. All Challan records in the return have correct values.\n2. NSDL website 'www.tin-nsdl.com' is working.");
                            cmnService.J_UserMessage("Auto download of CSI file has failed, now manual downloader will open.");
                            TrnDownloadCSIFilePopup DownloadCSIFilePopup = new TrnDownloadCSIFilePopup(TAN, strCSIDownloadFilePath);
                            DownloadCSIFilePopup.ShowDialog();
                            this.Refresh();
                            return;
                        }
                        else
                        {
                            goto CSI_DOWNLOAD;
                        }
                    }
                    else if (text == "")
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("CSI downloaded does not contain any record. Please check whether all Challan records in the return have correct values.\n\nYou can also manually download the CSI file by visiting NSDL website 'www.tin-nsdl.com'.1" + text);
                        return;
                    }
                    this.Cursor = Cursors.Default; 
                    TDSMAN.Classes.TDSMAN.T_pDownloadPath = strCSIDownloadFilePath;
                    this.Dispose();
                    this.Close();
                    return;
                }
                this.Cursor = Cursors.Default; 
                TDSMAN.Classes.TDSMAN.T_pDownloadPath = strCSIDownloadFilePath;
                this.Dispose();
                this.Close();
                return;
            }
            catch
            {
                setCaptchaCode(); 
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("CSI downloaded does not contain any record. Please check whether all Challan records in the return have correct values.\n\nYou can also manually download the CSI file by visiting NSDL website 'www.tin-nsdl.com'.");
                return;
            }
        }
        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                setCaptchaCode();
            }
            catch
            {

            }
        }
        #endregion


        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pDownloadPath = "!@#";
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region Properties Declaration
        
    
        private string strCSIDownloadFilePath;

        public string CSIDownloadFilePath
        {
            get { return strCSIDownloadFilePath; }
            set { strCSIDownloadFilePath = value; }
        }


        private string strTAN;

        public string TAN
        {
            get { return strTAN; }
            set { strTAN = value; }
        }

        private string strFromDD;

        public string FromDD
        {
            get { return strFromDD; }
            set { strFromDD = value; }
        }

        private string strFromMM;

        public string FromMM
        {
            get { return strFromMM; }
            set { strFromMM = value; }
        }

        private string strFromYY;

        public string FromYY
        {
            get { return strFromYY; }
            set { strFromYY = value; }
        }

        private string strToDD;

        public string ToDD
        {
            get { return strToDD; }
            set { strToDD = value; }
        }
        private string strToMM;

        public string ToMM
        {
            get { return strToMM; }
            set { strToMM = value; }
        }


        private string strToYY;

        public string ToYY
        {
            get { return strToYY; }
            set { strToYY = value; }
        }

        #endregion

        #region ValidateFields
        public bool ValidateFields()
        {
            try
            {
                // **************************************************
                // **** Blank Check
                // **************************************************
                if (txtCaptchaCode.Text == "")
                {
                    cmnService.J_UserMessage("Please select the CSI file to create the FVU file");

                    return false;
                }

                return true;
            }
            catch (Exception err_handler)
            {
                //cmnService.J_UserMessage("File Format incorrect");

                return false;
            }
        }
        #endregion

        #region setCaptchaCode
        public void setCaptchaCode()
        {
            try
            {
                txtCaptchaCode.Text = "";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //request = (HttpWebRequest)WebRequest.Create("https://tin.tin.nsdl.com/oltas/servlet/CaptchaServicetansearch");
                request = (HttpWebRequest)WebRequest.Create("https://tin.tin.proteantech.in/oltas/servlet/CaptchaServicetansearch");
                request.Method = "GET";
                request.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20100101 Firefox/15.0";
                request.ContentType = "text/html; charset=utf-8";
                request.KeepAlive = true;
                request.CookieContainer = objContainer;

                //if (response.Cookies != null && response.Cookies.Count > 0)            
                //    objContainer.Add(response.Cookies);            

                Stream imgStream = request.GetResponse().GetResponseStream();
                Image img = Image.FromStream(imgStream);
                this.picCaptcha.Image = img;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region txtCaptchaCode_KeyPress
        private void txtCaptchaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) btnDownloadFile_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) btnBack_Click(sender, e);
        }
        #endregion

        private void TrnTINNSDLCaptcha_Load(object sender, EventArgs e)
        {

        }




        #endregion


    }
}

#region WebClientCookies


public class WebClientCookies : WebClient
{
    public WebClientCookies(CookieContainer container)
    {
        this.container = container;
    }

    public CookieContainer CookieContainer
    {
        get { return container; }
        set { container = value; }
    }

    private CookieContainer container = new CookieContainer();

    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest r = base.GetWebRequest(address);
        HttpWebRequest request = r as HttpWebRequest;
        if (request != null)
        {
            request.CookieContainer = container;
        }
        return r;
    }

    protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
    {
        WebResponse response = base.GetWebResponse(request, result);
        ReadCookies(response);
        return response;
    }

    protected override WebResponse GetWebResponse(WebRequest request)
    {
        WebResponse response = base.GetWebResponse(request);
        ReadCookies(response);
        return response;
    }

    private void ReadCookies(WebResponse r)
    {
        HttpWebResponse response = r as HttpWebResponse;
        if (response != null)
        {
            CookieCollection cookies = response.Cookies;
            container.Add(cookies);
        }
    }

}
#endregion