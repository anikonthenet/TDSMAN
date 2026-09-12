using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;


using TDSMAN.Classes;

namespace TDSMAN.FormBrowser
{
    public partial class TrnViewUploadedTDSBrowser : Form
    {
        int i = 1, tabval=0;

        string linkref = "";
        #region System Generated Code
        public TrnViewUploadedTDSBrowser()
        {
            InitializeComponent();
        }
        #endregion

        #region TrnUploadTDS_Load
        private void TrnViewUploadedTDS_Load(object sender, EventArgs e)
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
            //webBrowser1.Navigate("https://www.incometaxindiaefiling.gov.in/help/");
            webBrowser1.Navigate("https://www.tdscpc.gov.in/app/login.xhtml");
        }
        #endregion

        #region webBrowser1_DocumentCompleted
        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (i == 1)
            {
                try
                {
                    await Task.Delay(1000);
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
                await Task.Delay(1000);
                //query
                //webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", "CALP08143C ");
                //webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", "Pdsinfo@6");
                webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
                webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
                i++;
            }
            else if (i == 3)
            {
                await Task.Delay(1000);
                bool flagloginfail = false;
                HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("li");
                foreach (HtmlElement link in links)  // this ex is given another SO post 
                {
                    HtmlElementCollection linksIN = link.GetElementsByTagName("A");
                    foreach (HtmlElement lin in linksIN)  // this ex is given another SO post 
                    {
                        //MessageBox.Show(lin.InnerText.Trim());
                        if (lin.InnerText.Trim() == "View Filed TDS")
                        {
                            linkref = Convert.ToString(lin.OuterHtml.Trim());
                            lin.InvokeMember("click");
                            i++;
                            flagloginfail = true;
                            break;
                        }
                    }
                }
                if (i == 3)
                {
                    await Task.Delay(1000);
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
                    await Task.Delay(1000);
                    webBrowser1.Document.GetElementById("Login_userName").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_TANOnlineFilling);
                    webBrowser1.Document.GetElementById("Login_password").SetAttribute("value", TDSMAN.Classes.TDSMAN.T_PasswordOnlineFilling);
                }
            }
            else if (i == 4)
            {
                await Task.Delay(1000);
                webBrowser1.Document.GetElementById("ViewFiledTds_0").InvokeMember("click");
                i++;
            }

        }
        #endregion

        #region webBrowser1_NewWindow
        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Please wait... the receipt will open in 'Receipt tab'.");
            WebBrowser thisWebBrowser = (WebBrowser)sender;
            e.Cancel = true;
            TabPage addedTabPage = new TabPage("Receipt tab");
            WebBrowser addedWebBrowser = new WebBrowser();
            if (tabval == 0)
            {
                
                tabControl1.TabPages.Add(addedTabPage);
                 addedWebBrowser = new WebBrowser()
                {
                    Parent = addedTabPage,
                    Dock = DockStyle.Fill
                };
                tabval++;
            }
            else
            {
                TabPage TabP = (TabPage)tabControl1.TabPages[tabControl1.SelectedIndex+1];
                tabControl1.TabPages.Remove(TabP);
                tabControl1.TabPages.Add(addedTabPage);
                addedWebBrowser = new WebBrowser()
                {
                    Parent = addedTabPage,
                    Dock = DockStyle.Fill
                };
            }
            
             addedWebBrowser.Navigate(webBrowser1.StatusText.ToString());
        }
        #endregion
    }
}
