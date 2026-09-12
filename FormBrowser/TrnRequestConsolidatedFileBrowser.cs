
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
using System.Threading.Tasks;

using TDSMAN.Classes;

#endregion

namespace TDSMAN.FormBrowser
{
    public partial class TrnRequestConsolidatedFileBrowser : Form
    {
        #region Variable Declaration
        int tabval = 0;
        int i = 1, nextrequest = 0;
        string strFormName = "";
        bool TracesReqJustification = false, TracesReqForm16File = false, TracesReqForm16AFile = false,
                TracesReqForm27DFile = false, TracesReqConsoFile = false, TracesReqFormTANPANFile = false,
                TracesDownloadFiles = false;

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //webBrowser1.ObjectForScripting = new ScriptManager(this);
            //WebBrowser thisWebBrowser = (WebBrowser)sender;
            //e.Cancel = true;
            //TabPage addedTabPage = new TabPage("tab title");
            //WebBrowser addedWebBrowser = new WebBrowser();
            //if (tabval == 0)
            //{

            //    tabControl1.TabPages.Add(addedTabPage);
            //    addedWebBrowser = new WebBrowser()
            //    {
            //        Parent = addedTabPage,
            //        Dock = DockStyle.Fill
            //    };
            //    tabval++;
            //}
            //else
            //{
            //    TabPage TabP = (TabPage)tabControl1.TabPages[tabControl1.SelectedIndex + 1];
            //    tabControl1.TabPages.Remove(TabP);
            //    tabControl1.TabPages.Add(addedTabPage);
            //    addedWebBrowser = new WebBrowser()
            //    {
            //        Parent = addedTabPage,
            //        Dock = DockStyle.Fill
            //    };
            //}

            //addedWebBrowser.Navigate(webBrowser1.StatusText.ToString());
        }

        #endregion

        #region System Generated Code
        public TrnRequestConsolidatedFileBrowser()
        {
            InitializeComponent();
        }
        #endregion              

        #region User Defined Generated Custuctor
        public TrnRequestConsolidatedFileBrowser(string FormType)
        {
            strFormName = FormType;

            InitializeComponent();
        }
        #endregion

        #region TrnRequestConsolidatedFileBrowser_Load
        private void TrnRequestConsolidatedFileBrowser_Load(object sender, EventArgs e)
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

            //  deletecookie();
            //webBrowser1.ScriptErrorsSuppressed = true;
            if (!WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }
            //
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/login.xhtml");
            //webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
        }
        #endregion

        #region TrnRequestConsolidatedFileBrowser_FormClosing
        private void TrnRequestConsolidatedFileBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //strFormName = "";
            //i = 0;
            //InitializeComponent();
        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (i == 1)
            {

                await Task.Delay(1000);
                //querY
                HtmlElement active_server = webBrowser1.Document.GetElementById("captchaDiv").GetElementsByTagName("div")[0];
                var a = active_server.GetElementsByTagName("a")[0];
                a.InvokeMember("click");
                webBrowser1.Document.GetElementById("captchaDiv").Style = "display:block";
                //webBrowser1.Document.GetElementById("userId").SetAttribute("value", "pdsinfo");
                //webBrowser1.Document.GetElementById("psw").SetAttribute("value", "Pdsinfo2018");
                //webBrowser1.Document.GetElementById("tanpan").SetAttribute("value", "CALP08143C");
                webBrowser1.Document.GetElementById("userId").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesUserID);
                webBrowser1.Document.GetElementById("psw").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPassword);
                webBrowser1.Document.GetElementById("tanpan").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesTAN);
                i++;
            }
            else if (i == 3)
            {
                if (TDSMAN.Classes.TDSMAN.T_TracesDownloadFiles == true)
                {
                    await Task.Delay(1000);
                    webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/filedownload.xhtml");
                    //search3
                    //webBrowser1.Document.GetElementById("search3").InvokeMember("click");
                    //TDSMAN.Classes.TDSMAN.T_TracesDownloadFiles = false;
                    TracesDownloadFiles = true;
                    i = 4;
                }
                else if (TracesDownloadFiles == false)
                {
                    if (TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml");
                            TracesReqConsoFile = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml");
                            TracesReqJustification = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm16File == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download16.xhtml");
                            TracesReqForm16File = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16File = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download16a.xhtml");
                            TracesReqForm16AFile = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download27d.xhtml");
                            TracesReqForm27DFile = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (TDSMAN.Classes.TDSMAN.T_TracesReqFormTANPANFile == true)
                    {
                        try
                        {
                            await Task.Delay(1000);
                            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/panverify.xhtml");
                            TracesReqFormTANPANFile = true;
                            TDSMAN.Classes.TDSMAN.T_TracesReqFormTANPANFile = false;
                            i = 4;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            else if (i == 4)
            {
                try
                {
                    if (TDSMAN.Classes.TDSMAN.T_TracesDownloadFiles == true)
                    {
                        await Task.Delay(1000);
                        //search3
                        webBrowser1.Document.GetElementById("search3").InvokeMember("click");
                        TDSMAN.Classes.TDSMAN.T_TracesDownloadFiles = false;
                        // i = 4;
                    }
                    else if (TracesDownloadFiles == false)
                    {
                        if (TracesReqJustification == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option> ||finYr
                            webBrowser1.Document.GetElementById("finYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //qrtr <option value="4">Q2</option> ||qrtr
                            webBrowser1.Document.GetElementById("qrtr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesQuarter);
                            //frmType   <option value="24Q">24Q</option> ||frmType
                            webBrowser1.Document.GetElementById("frmType").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesForm);
                            //download_conso go  || download_justReport go
                            webBrowser1.Document.GetElementById("download_justReport").InvokeMember("click");
                        }
                        else if (TracesReqForm16File == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option>
                            webBrowser1.Document.GetElementById("bulkfinYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //bulkGo go
                            webBrowser1.Document.GetElementById("bulkGo").InvokeMember("click");
                        }
                        else if (TracesReqForm16AFile == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option>
                            webBrowser1.Document.GetElementById("bulkfinYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //bulkquarter
                            webBrowser1.Document.GetElementById("bulkquarter").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesQuarter);
                            //bulkformType
                            webBrowser1.Document.GetElementById("bulkformType").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesForm);
                            //bulkGo go
                            webBrowser1.Document.GetElementById("bulkGo").InvokeMember("click");
                        }
                        else if (TracesReqForm27DFile == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option>
                            webBrowser1.Document.GetElementById("bulkfinYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //bulkquarter
                            webBrowser1.Document.GetElementById("bulkquarter").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesQuarter);
                            //bulkGo go
                            webBrowser1.Document.GetElementById("bulkGo").InvokeMember("click");
                        }
                        else if (TracesReqConsoFile == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option> ||finYr
                            webBrowser1.Document.GetElementById("finYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //qrtr <option value="4">Q2</option> ||qrtr
                            webBrowser1.Document.GetElementById("qrtr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesQuarter);
                            //frmType   <option value="24Q">24Q</option> ||frmType
                            webBrowser1.Document.GetElementById("frmType").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesForm);
                            //download_conso go  || download_justReport go
                            webBrowser1.Document.GetElementById("download_conso").InvokeMember("click");
                        }
                        else if (TracesReqFormTANPANFile == true)
                        {
                            await Task.Delay(1000);
                            //finYr<option value="2017">2017-18</option> ||finYr
                            webBrowser1.Document.GetElementById("finYr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesFAYear);
                            //frmType   <option value="24Q">24Q</option> ||frmType
                            webBrowser1.Document.GetElementById("frmType2").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesForm);
                            //download_conso go  || download_justReport go
                            webBrowser1.Document.GetElementById("clickGo2").InvokeMember("click");
                        }

                        i++;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (i == 5)
            {
                if (TracesReqForm16File == true)
                {
                    if (webBrowser1.DocumentText.Contains("j_id1972728517_7cc7de5f"))
                    {
                        TracesReqForm16File = false;
                        //j_id1972728517_7cc7de5f   Submit 
                        webBrowser1.Document.GetElementById("j_id1972728517_7cc7de5f").InvokeMember("click");
                    }
                }
                else if (TracesReqForm16AFile == true)
                {
                    TracesReqForm16AFile = false;
                    if (webBrowser1.DocumentText.Contains("j_id1972728517_7cc7de5f"))
                    {
                        //j_id1972728517_7cc7de5f   Submit
                        webBrowser1.Document.GetElementById("j_id1972728517_7cc7de5f").InvokeMember("click");
                    }
                }
                else if (TracesReqForm27DFile == true)
                {
                    TracesReqForm27DFile = false;
                    if (webBrowser1.DocumentText.Contains("j_id2143335333_643da170"))
                    {
                        //j_id2143335333_643da170   Submit
                        webBrowser1.Document.GetElementById("j_id2143335333_643da170").InvokeMember("click");
                    }
                    i++;
                }
                else if (TracesReqFormTANPANFile == true)
                {
                    TracesReqFormTANPANFile = false;
                    if (webBrowser1.DocumentText.Contains("search2"))
                    {
                        //search2   Normal KYC Validaton (Without Digital Signature)  ||normalkyc
                        webBrowser1.Document.GetElementById("search2").InvokeMember("click");
                        webBrowser1.Document.GetElementById("normalkyc").InvokeMember("click");
                    }
                    i++;
                }
                else
                {
                    if (TracesReqConsoFile == true)
                    {
                        TracesReqConsoFile = false;
                    }
                    if (webBrowser1.DocumentText.Contains("search2"))
                    {
                        //search2   Normal KYC Validaton (Without Digital Signature)  ||normalkyc
                        webBrowser1.Document.GetElementById("search2").InvokeMember("click");
                        webBrowser1.Document.GetElementById("normalkyc").InvokeMember("click");
                    }
                    i++;
                }


            }

            if (i == 6)
            {
                try
                {
                    await Task.Delay(1000);
                    //cinbinCheck (for NIL Challan statement.)                
                    //webBrowser1.Document.GetElementById("cinbinCheck").SetAttribute("value", "2017");
                    /////webBrowser1.Document.GetElementById("cinbinCheck").InvokeMember("click");
                    //bkEntryCheck ----(was done by book adjustment (for Government Deductors))
                    //webBrowser1.Document.GetElementById("bkEntryCheck").InvokeMember("click");
                    if (webBrowser1.DocumentText.Contains("token"))
                    {
                        //token maxlength=15
                        webBrowser1.Document.GetElementById("token").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPRN_NO);
                        //bsr lanth=7
                        webBrowser1.Document.GetElementById("bsr").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesBSRCode);
                        //dtoftaxdep (dd-mmm-yyyy; e.g., 12-Dec-1980)
                        DateTime dt = Convert.ToDateTime(TDSMAN.Classes.TDSMAN.T_TracesTaxDepositedDate);
                        TreeNode tn = new TreeNode(String.Format("{0:dd-MMM-yyyy}", dt));
                        webBrowser1.Document.GetElementById("dtoftaxdep").SetAttribute("value", tn.Text);//TDSMAN.Classes.TDSMAN.T_TracesTaxDepositedDate);
                                                                                                         //csn  maxlength=5  (Challan Serial Number)
                        webBrowser1.Document.GetElementById("csn").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesChallanSerialNo);
                        //chlnamt   (e.g., 1987.00)
                        webBrowser1.Document.GetElementById("chlnamt").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesChallanAmount);
                        //cdrecnum maxlength =10
                        webBrowser1.Document.GetElementById("cdrecnum").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesCDRecordNumber);
                        //panAmtCheck --e Challan / Transfer Voucher mentioned above
                        //webBrowser1.Document.GetElementById("panAmtCheck").InvokeMember("click");
                        //pan1
                        webBrowser1.Document.GetElementById("pan1").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN1);
                        //amt1


                        webBrowser1.Document.GetElementById("amt1").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN1Amount);
                        //pan2
                        webBrowser1.Document.GetElementById("pan2").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN2);
                        //amt2
                        webBrowser1.Document.GetElementById("amt2").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN2Amount);
                        //pan3
                        webBrowser1.Document.GetElementById("pan3").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN3);
                        //amt3
                        webBrowser1.Document.GetElementById("amt3").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TracesPAN3Amount);


                        ///webBrowser1.Document.GetElementById("pan3").SetAttribute("value", "AOQPB2814K");
                        //amt3
                        // /webBrowser1.Document.GetElementById("amt3").SetAttribute("value", "500.00");
                        //clickKYC after proced button
                        tmrRequestpopup.Interval = 25;
                        tmrRequestpopup.Start();
                        //
                        webBrowser1.Document.GetElementById("clickKYC").InvokeMember("click");

                        i++;
                    }
                }
                catch (Exception ex)
                { }
            }
            else if (i == 7)
            {

                await Task.Delay(1000);
                //clickKYC after proced button
                //System.Threading.Thread.Sleep(10000);
                webBrowser1.Document.GetElementById("redirect").InvokeMember("click");
                //i++;

            }
            else if (i == 8)
            {
                if (TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile == true)
                {
                    try
                    {
                        await Task.Delay(1000);

                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml");
                        TracesReqConsoFile = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile == true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml");
                        TracesReqJustification = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm16File == true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download16.xhtml");
                        TracesReqForm16File = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqForm16File = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile == true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download16a.xhtml");
                        TracesReqForm16AFile = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile == true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/download27d.xhtml");
                        TracesReqForm27DFile = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (TDSMAN.Classes.TDSMAN.T_TracesReqFormTANPANFile == true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        webBrowser1.Navigate("https://www.tdscpc.gov.in/app/ded/panverify.xhtml");
                        TracesReqFormTANPANFile = true;
                        TDSMAN.Classes.TDSMAN.T_TracesReqFormTANPANFile = false;
                        i = 4;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
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

    }
}
