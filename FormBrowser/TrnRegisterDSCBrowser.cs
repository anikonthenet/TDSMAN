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

#endregion

namespace TDSMAN.FormBrowser
{
    public partial class TrnRegisterDSCBrowser : Form
    {

        int i = 1;

        CommonService cmnService = new CommonService();

        #region System Generated Code
        public TrnRegisterDSCBrowser()
        {
            InitializeComponent();
        }
        #endregion

        #region TrnUploadTDS_Load
        private void TrnUploadTDS_Load(object sender, EventArgs e)
        {
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

            if (!WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.incometaxindiaefiling.gov.in/help/");

        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (i == 1)
            {
                try
                {
                    HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("A");
                    foreach (HtmlElement link in links)  // this ex is given another SO post 
                    {
                        if (link.InnerText == "Login")
                        {
                            link.InvokeMember("click");
                            i++;
                        }
                    }
                }
                catch (Exception EX) { }
            }
            else if (i == 2)
            {
                //Thread.Sleep(90000);
                //query
                //webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", "CALP08143C");
                //webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", "Pdsinfo@6");
                webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
                webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
                i++;
            }
            else if (i == 3)
            {
                bool flagloginfail = false;
                HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("li");
                foreach (HtmlElement link in links)  // this ex is given another SO post 
                {
                    HtmlElementCollection linksIN = link.GetElementsByTagName("A");
                    foreach (HtmlElement lin in linksIN)  // this ex is given another SO post 
                    {
                        //MessageBox.Show(lin.InnerText.Trim());
                        if (lin.InnerText.Trim() == "Register Digital Signature Certificate")
                        {
                            lin.InvokeMember("click");
                            i++;
                            flagloginfail = true;
                            break;
                        }
                    }
                }
                if (i == 3)
                {
                    HtmlElementCollection inputs = webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement input in inputs)
                    {
                        String value = input.GetAttribute("value");
                        if (value == "Forced Login")
                        {
                            input.InvokeMember("click");
                            flagloginfail = true;
                            break;
                        }
                    }
                }
                if (flagloginfail == false)
                {
                    webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
                    webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
                }
            }
            else if (i == 4)
            {
                //query

                if (TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling!= "")
                    webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_fvuVersion").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling);
                //
                if (TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling != "")
                    webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_finYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling.Replace("-", ""));
                //
                if (TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling != "")
                    webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_formName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling);
                //
                if (TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling != "")
                    webBrowser1.Document.GetElementById("UploadTdsParamValidate_stmtParamater_period").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling);
                //
                if (TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling != "")
                {
                    //webBrowser1.Document.GetElementById("uploadType").InvokeMember("click");
                    webBrowser1.Document.GetElementById("UploadType").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling);
                }
                //
                if (TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling == "C") //-- for CORRECTION RETURN ONLY
                {
                    webBrowser1.Document.GetElementById("orgRRR").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_OriginalPRNTypeOnlineFilling);
                    webBrowser1.Document.GetElementById("prevRRR").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PreviousPRNTypeOnlineFilling);
                }
                //
                if (TDSMAN.Classes.TDSMAN.T_FVUVersionOnlineFilling != "" &&
                    TDSMAN.Classes.TDSMAN.T_FAYearOnlineFilling != "" &&
                    TDSMAN.Classes.TDSMAN.T_FormNoOnlineFilling != "" &&
                    TDSMAN.Classes.TDSMAN.T_QtrOnlineFilling != "" &&
                    TDSMAN.Classes.TDSMAN.T_UploadTypeOnlineFilling != "")
                    webBrowser1.Document.GetElementById("UploadTdsParamValidate_0").InvokeMember("click");
                i++;
            }
            else if (i == 5)
            {
                TMDILOG.Interval = 50;
                TMDILOG.Start();
                //  file.InvokeMember("Click");
                HtmlElement fileelem = webBrowser1.Document.GetElementById("UploadTdsReturn_fileUploadBean_file");
                if(TDSMAN.Classes.TDSMAN.T_UploadFilePath.Trim() != "")
                    fileelem.SetAttribute("value", TDSMAN.Classes.TDSMAN.T_UploadFilePath);
                fileelem.InvokeMember("Click");

                if (webBrowser1.Document.GetElementById("UploadTdsReturn_fileUploadBean_file").GetAttribute("value") != "")
                {
                    //--
                    webBrowser1.Document.GetElementById("displayEverifyDtls").InvokeMember("click");
                    webBrowser1.Document.GetElementById("generate_aadhaar_otp_forms").InvokeMember("click");
                    webBrowser1.Document.GetElementById("generate_nsubmit_aadhaar_otp_forms").InvokeMember("click");
                    webBrowser1.Document.GetElementById("UpdateContactDtls_2").InvokeMember("click");
                    //UpdateContactDtls_2
                    i++;
                }
            }
            else if (i == 6)
            {
                //HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("A");
                //foreach (HtmlElement link in links)  // this ex is given another SO post 
                //{
                //    if (link.InnerText == "Logout")
                //    {
                //        this.Close();
                //    }
                //}
                webBrowser1.Document.GetElementById("Logout").InvokeMember("click");
            }
        }
        #endregion

        #region TMDILOG_Tick
        private void TMDILOG_Tick(object sender, EventArgs e)
        {
            try
            {
                TMDILOG.Stop();
                SendKeys.SendWait(TDSMAN.Classes.TDSMAN.T_UploadFilePath); // enter the file path, which suppose to upload.
                SendKeys.SendWait("{TAB 2}");
                SendKeys.SendWait("{ENTER}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        //public void doit(object sender, HtmlElementEventArgs e)
    }
}
