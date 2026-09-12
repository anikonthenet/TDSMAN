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
using System.Threading;


using TDSMAN.Classes;
using TDSMAN.FormBrowser;

//
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Diagnostics;
using System.Threading.Tasks;

#endregion

namespace TDSMAN.FormBrowser
{
    //[ComVisibleAttribute(true)]
    public partial class TrnChallan_PANCorrectionBrowser : Form
    {

        int i = 1;

        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        public CorrectionData CorrectionData { get; set; }


        #region System Generated Code
        public TrnChallan_PANCorrectionBrowser()
        {
            SetBrowserFeatureControl();
            InitializeComponent();
        }

        public TrnChallan_PANCorrectionBrowser(CorrectionData objData)
        {
            CorrectionData = objData;

            InitializeComponent();

        }


        #endregion



        #region TrnUploadTDS_Load
        private void TrnUploadTDS_Load(object sender, EventArgs e)
        {
            //-- 2021/11/13
            #region COMMENT
            //int BrowserVer, RegVal;
            ////// get the installed IE version
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
            #endregion

            if (!WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }

            webBrowser1.ScriptErrorsSuppressed = true;
            
            //webBrowser1.Navigate("https://www.tdscpc.gov.in/app/login.xhtml"); 
            webBrowser1.Navigate("https://traces.tdscpc.gov.in/auth/login/loginScreen");
        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (i == 1)
            {
                try
                {

                    HtmlElement active_server = webBrowser1.Document.GetElementById("captchaDiv").GetElementsByTagName("div")[0];
                    var a = active_server.GetElementsByTagName("a")[0];
                    a.InvokeMember("click");

                    //querY
                    await Task.Delay(1000);

                    webBrowser1.Document.GetElementById("captchaDiv").Style = "display:block";
                    webBrowser1.Document.GetElementById("userId").SetAttribute("value", CorrectionData.TracesLogin.UserID);
                                  
                    webBrowser1.Document.GetElementById("psw").SetAttribute("value", CorrectionData.TracesLogin.Password);
                    webBrowser1.Document.GetElementById("tanpan").SetAttribute("value", CorrectionData.TracesLogin.TAN);

                                     

                    i = 2;
                }
                catch (Exception ex) { }
            }
            else if (i == 2)
            {
                //await Task.Delay(1000);
                webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/crrctnwelcome.xhtml");
                i = 3;
            }
            else if (i == 3)
            {
                await Task.Delay(1000);

                webBrowser1.Document.GetElementById("finYr").SetAttribute("value", CorrectionData.TracesData.FAYear);
                //qrtr <option value="4">Q2</option> ||qrtr
                webBrowser1.Document.GetElementById("qrtr").SetAttribute("value", CorrectionData.TracesData.Quarter);
                //frmType   <option value="24Q">24Q</option> ||frmType
                webBrowser1.Document.GetElementById("frmType").SetAttribute("value", CorrectionData.TracesData.Forms);

                webBrowser1.Document.GetElementById("status").SetAttribute("value", "1");

                //download_conso go  || download_justReport go
                webBrowser1.Document.GetElementById("clickfilecorrn").InvokeMember("click");

                i = 4;

            }
            else if (i == 4)
            {
               // await Task.Delay(1000);
                webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/corrdownload.xhtml");
                i = 5;

            }
            else if (i == 5)
            {
               // await Task.Delay(1000);
                webBrowser1.Document.GetElementById("search3").InvokeMember("click");
                i = 6;
            }

            else if (i == 6)
            {
                await Task.Delay(1000);                
                
                if (webBrowser1.DocumentText.Contains("Normal KYC Validaton (Without Digital Signature)"))
                {
                    webBrowser1.Document.GetElementById("search2").InvokeMember("click");

                    tmrRequestpopup.Interval = 25;
                    tmrRequestpopup.Start();

                    webBrowser1.Document.GetElementById("normalkyc").InvokeMember("click");
                }
                else if (webBrowser1.DocumentText.Contains("token"))
                {
                    //token maxlength=15
                    webBrowser1.Document.GetElementById("token").SetAttribute("value", CorrectionData.TracesData.PRN_NO);
                    //bsr lanth=7
                    webBrowser1.Document.GetElementById("bsr").SetAttribute("value", CorrectionData.TracesData.BSRCode);
                    //dtoftaxdep (dd-mmm-yyyy; e.g., 12-Dec-1980)
                    DateTime dt = Convert.ToDateTime(CorrectionData.TracesData.FromChallanDepositDate);
                    TreeNode tn = new TreeNode(String.Format("{0:dd-MMM-yyyy}", dt));
                    webBrowser1.Document.GetElementById("dtoftaxdep").SetAttribute("value", tn.Text);//TDSMAN.Classes.TDSMAN.T_TracesTaxDepositedDate);
                                                                                                     //csn  maxlength=5  (Challan Serial Number)
                    webBrowser1.Document.GetElementById("csn").SetAttribute("value", CorrectionData.TracesData.ChallanSerialNo);
                    //chlnamt   (e.g., 1987.00)
                    webBrowser1.Document.GetElementById("chlnamt").SetAttribute("value", CorrectionData.TracesData.ChallanAmount);
                    //cdrecnum maxlength =10
                    webBrowser1.Document.GetElementById("cdrecnum").SetAttribute("value", CorrectionData.TracesData.CDRecordNumber);
                    //panAmtCheck --e Challan / Transfer Voucher mentioned above
                    //webBrowser1.Document.GetElementById("panAmtCheck").InvokeMember("click");
                    //pan1
                    webBrowser1.Document.GetElementById("pan1").SetAttribute("value", CorrectionData.TracesData.PAN1);
                    //amt1
                    webBrowser1.Document.GetElementById("amt1").SetAttribute("value", CorrectionData.TracesData.PAN1Amount);
                    //pan2
                    webBrowser1.Document.GetElementById("pan2").SetAttribute("value", CorrectionData.TracesData.PAN2);
                    //amt2
                    webBrowser1.Document.GetElementById("amt2").SetAttribute("value", CorrectionData.TracesData.PAN2Amount);
                    //pan3
                    webBrowser1.Document.GetElementById("pan3").SetAttribute("value", CorrectionData.TracesData.PAN3);
                    //amt3
                    webBrowser1.Document.GetElementById("amt3").SetAttribute("value", CorrectionData.TracesData.PAN3Amount);


                    ///webBrowser1.Document.GetElementById("pan3").SetAttribute("value", "AOQPB2814K");
                    //amt3
                    // /webBrowser1.Document.GetElementById("amt3").SetAttribute("value", "500.00");
                    //clickKYC after proced button
                    tmrRequestpopup.Interval = 25;
                    tmrRequestpopup.Start();
                    //
                    webBrowser1.Document.GetElementById("clickKYC").InvokeMember("click");

                    i = 7;
                }
            }
            else if (i == 7)
            {
               // await Task.Delay(1000);

                if (webBrowser1.DocumentText.Contains("Proceed with Transaction"))
                {

                    webBrowser1.Document.GetElementById("redirect").InvokeMember("click");
                    i = 8;
                }

            }
            else if (i == 8)
            {
               // await Task.Delay(1000);
                if (
                    
                    webBrowser1.DocumentText.Contains("correctionType"))
                {
                    webBrowser1.Document.GetElementById("correctionType").SetAttribute("value",Convert.ToString(CorrectionData.CorrectionType));
                    tmrRequestpopup.Interval = 25;
                    tmrRequestpopup.Start();

                   webBrowser1.Document.GetElementById("clickcorrnTyp").InvokeMember("click");

                    i = 9;

                }
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

        #region tmrRequestpopup_Tick
        private void tmrRequestpopup_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrRequestpopup.Stop();
                SendKeys.Send("{ENTER}{ENTER}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        // Browser feature conntrol
        void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
        }

        void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }


        UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode. Default value for Internet Explorer 10.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                default:
                    // use IE10 mode by default
                    break;
            }

            return mode;
        }
    }
}
