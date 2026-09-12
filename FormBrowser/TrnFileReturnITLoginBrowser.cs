
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
    public partial class TrnFileReturnITLoginBrowser : Form
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
        public TrnFileReturnITLoginBrowser()
        {
            InitializeComponent();
        }
        #endregion              

        #region User Defined Generated Custuctor
        public TrnFileReturnITLoginBrowser(string FormType)
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
            //webBrowser1.Navigate("https://www.tdscpc.gov.in/app/login.xhtml");
            webBrowser1.Navigate("https://eportal.incometax.gov.in/iec/foservices/#/login");
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
