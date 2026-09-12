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

using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System.Threading.Tasks;

#endregion

namespace TDSMAN.FormBrowser
{
    //[ComVisibleAttribute(true)]
    public partial class TrnChallanCorrectionBrowser : Form
    {
        #region DECLARATIONS
        int i = 1;

        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        public CorrectionData CorrectionData { get; set; }

        private CorrectionData _objData;
        private bool _loginFilled = false;
        private bool _openCorrectionRequestDone = false;
        private bool _complianceClicked = false;
        private bool _dashboardReached = false;
        private bool _proceedClicked = false;
        private bool _proceedEntryClicked = false;
        private bool _kycProceedDone = false;
        private bool _kycDetailsFilled = false;
        private bool _redirectClicked = false;
        private bool _correctionTypeSelected = false;

        bool isCapsOn = false;// Control.IsKeyLocked(Keys.CapsLock);


        private bool _correctionSearchDone = false;
        private bool _corrDownloadOpened = false;
        private bool _searchClicked = false;
        private bool _kycStarted = false;

        private WebView2 webView;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void mouse_event(
            uint dwFlags,
            uint dx,
            uint dy,
            uint dwData,
            UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        #endregion

        #region System Generated Code
        public TrnChallanCorrectionBrowser()
        {
            //SetBrowserFeatureControl();
            InitializeComponent();
        }

        //public TrnChallan_PANCorrectionBrowserNew(CorrectionData objData)
        //{
        //    //CorrectionData = objData;

        //    //InitializeComponent();
        //    InitializeComponent();
        //    this.Load += TrnUploadTDS_Load;

        //}

        public TrnChallanCorrectionBrowser(CorrectionData objData)
        {
            InitializeComponent();

            _objData = objData;
        }
        #endregion



        #region FORM LOAD - COMMENTED
        //private async void TrnUploadTDS_Load(object sender, EventArgs e)
        //{
        //    // Panel wrapper (important for UI stability)
        //    Panel pnl = new Panel();
        //    pnl.Dock = DockStyle.Fill;
        //    pnl.AutoScroll = true;
        //    this.Controls.Add(pnl);

        //    // WebView2 init
        //    webView = new WebView2();
        //    webView.Dock = DockStyle.Fill;
        //    pnl.Controls.Add(webView);

        //    //await webView.EnsureCoreWebView2Async(null);

        //    // Settings
        //    webView.CoreWebView2.Settings.IsZoomControlEnabled = true;
        //    webView.CoreWebView2.Settings.AreDevToolsEnabled = true;
        //    webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;

        //    // Prevent new window issues
        //    webView.CoreWebView2.NewWindowRequested += (s, args) =>
        //    {
        //        webView.CoreWebView2.Navigate(args.Uri);
        //        args.Handled = true;
        //    };

        //    // Navigation event
        //    webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

        //    // Scroll fix
        //    webView.MouseWheel += (s, ev) => webView.Focus();

        //    // Open TRACES login 
        //    webView.CoreWebView2.Navigate("https://traces.tdscpc.gov.in/auth/login/loginScreen");
        //}
        #endregion

        #region FORM LOAD

        private async void TrnUploadTDS_Load(object sender, EventArgs e)
        {
            await webView2.EnsureCoreWebView2Async(null);

            webView2.Dock = DockStyle.Fill;

            webView2.CoreWebView2.Settings.IsZoomControlEnabled = true;
            webView2.CoreWebView2.Settings.AreDevToolsEnabled = true;
            webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;

            webView2.CoreWebView2.NewWindowRequested += (s, args) =>
            {
                webView2.CoreWebView2.Navigate(args.Uri);
                args.Handled = true;
            };

            webView2.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;


            webView2.CoreWebView2.Navigate("https://traces.tdscpc.gov.in/auth/login/loginScreen");
        }

        #endregion

        #region NAVIGATION LOGIC
        //private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        //{
        //    try
        //    {
        //        string url = webView.Source.ToString();
        //        if (url.Contains("/auth"))
        //        {
        //            await AutoFillLogin();
        //        }
        //        //// Ensure scroll works (important for Flutter pages)
        //        //await EnableScroll();

        //        //// Handle popups always
        //        //await HandlePopups();

        //        //// Step 1: Detect dashboard after login
        //        //if (url.Contains("deductorDashboard"))
        //        //{
        //        //    await Task.Delay(2000);

        //        //    // Navigate to old TRACES site
        //        //    webView.CoreWebView2.Navigate("https://traces61.tdscpc.gov.in/app/ded/corrdownload.xhtml");
        //        //}

        //        //// Step 2: When old page loads → fill data
        //        //else if (url.Contains("corrdownload.xhtml"))
        //        //{
        //        //    await Task.Delay(2000);
        //        //    await FillKYCData();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}
        private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                string url = webView2.Source.ToString();

                // LOGIN PAGE
                if (!_loginFilled &&
                    url.Contains("/auth/login"))
                {
                    _loginFilled = true;                    
                    await AutoFillLogin();
                    return;
                }

                // DASHBOARD PAGE
                if (!_dashboardReached && url.Contains("deductorDashboard"))
                {
                    _dashboardReached = true;

                    //bool result = await ClickComplianceMenu();

                    //MessageBox.Show(result.ToString());

                    cmnService.J_UserMessage("Please click 'Compliance under Income-tax Act, 1961'.\n\n" +
                        "to continue using TRACES.");
                    return;
                }

                //if (url.Contains("traces61.tdscpc.gov.in"))
                //{
                //    //MessageBox.Show("Old TRACES Opened");

                //    // Your PAN Verification code here

                //    return;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
               
        #region ENABLE SCROLL
        private async Task EnableScroll()
        {
            string script = @"
            document.body.style.overflow = 'auto';
            document.documentElement.style.overflow = 'auto';

            var flt = document.querySelector('flt-glass-pane');
            if (flt) flt.style.overflow = 'auto';
        ";

            await webView.CoreWebView2.ExecuteScriptAsync(script);
        }
        #endregion

        #region FILL KYC DATA
        private async Task FillKYCData()
        {
            try
            {
                DateTime dt = Convert.ToDateTime(CorrectionData.TracesData.FromChallanDepositDate);
                string formattedDate = dt.ToString("dd-MMM-yyyy");

                string script = $@"
                document.getElementById('token').value = '{CorrectionData.TracesData.PRN_NO}';
                document.getElementById('bsr').value = '{CorrectionData.TracesData.BSRCode}';
                document.getElementById('csn').value = '{CorrectionData.TracesData.ChallanSerialNo}';
                document.getElementById('chlnamt').value = '{CorrectionData.TracesData.ChallanAmount}';
                document.getElementById('dtoftaxdep').value = '{formattedDate}';

                document.getElementById('pan1').value = '{CorrectionData.TracesData.PAN1}';
                document.getElementById('amt1').value = '{CorrectionData.TracesData.PAN1Amount}';

                document.getElementById('pan2').value = '{CorrectionData.TracesData.PAN2}';
                document.getElementById('amt2').value = '{CorrectionData.TracesData.PAN2Amount}';

                document.getElementById('pan3').value = '{CorrectionData.TracesData.PAN3}';
                document.getElementById('amt3').value = '{CorrectionData.TracesData.PAN3Amount}';
            ";

                await webView.CoreWebView2.ExecuteScriptAsync(script);

                // Click KYC
                await webView.CoreWebView2.ExecuteScriptAsync("document.getElementById('clickKYC').click();");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill Error: " + ex.Message);
            }
        }
        #endregion

        #region HANDLE POPUPS
        private async Task HandlePopups()
        {
            string script = @"
            var buttons = document.getElementsByTagName('button');
            for (var i = 0; i < buttons.length; i++) {
                if (buttons[i].innerText.includes('Continue') || 
                    buttons[i].innerText.includes('Proceed') || 
                    buttons[i].innerText.includes('Leave')) {
                    buttons[i].click();
                }
            }
        ";

            await webView.CoreWebView2.ExecuteScriptAsync(script);
        }
        #endregion

        #region AutoFillLogin
        private async Task AutoFillLogin()
        {
            string oldClipboard = "";
            try
            {
                webView2.Focus();
                Application.DoEvents();
                await Task.Delay(1000);
                //
                for (int i = 0; i < 20; i++)
                    SendKeys.SendWait("{TAB}");
                //
                SendKeys.SendWait(_objData.TracesLogin.TAN);
                //
                for (int i = 0; i < 2; i++)
                    SendKeys.SendWait("{TAB}");
                //
                SendKeys.SendWait(_objData.TracesLogin.Password);
                //
                for (int i = 0; i < 4; i++)
                    SendKeys.SendWait("{TAB}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TmrPageDetect_Tick
        private async void TmrPageDetect_Tick(object sender, EventArgs e)
        {
            try
            {
                string url = webView2.Source.ToString();
                //
                if (!_complianceClicked && url.Contains("deductorDashboard"))
                {
                    _complianceClicked = true;

                    cmnService.J_UserMessage("Please click 'Compliance under Income-tax Act, 1961'.\n\n" +
                        "to continue using TRACES.");

                    return;
                }
                //
                //if (url.Contains("traces61.tdscpc.gov.in/app/ded/dashboard.xhtml"))
                //{
                //    await Task.Delay(2000);
                //    await OpenCorrectionRequest();
                //}
                if (!_openCorrectionRequestDone && url.Contains("traces61.tdscpc.gov.in/app/ded/dashboard.xhtml"))
                {
                    _openCorrectionRequestDone = true;
                    await Task.Delay(1000);
                    if (_objData.TracesData.MakeChallanCorrectionRequest == true)
                        await OpenOLTASChallanCorrectionRequest();
                    else
                        await TrackRequestOLTASChallanCorrection();
                    return;
                }
                //
                if (_objData.TracesData.MakeChallanCorrectionRequest == true)
                {
                    #region MAKE REQUEST
                    if (!_correctionSearchDone && url.Contains("chklstoltascorr.xhtml"))
                    {
                        _correctionSearchDone = true;
                        await ClickProceedPopup();
                        await Task.Delay(1000);
                        await OpenRequestOLTASChallanCorrection();
                        //await Task.Delay(1000);
                        //await ClickProceedPopup();
                    }
                    //
                    if (url.Contains("reqoltascorr.xhtml"))
                    {
                        if (!_proceedClicked)
                        {
                            _proceedClicked = true;
                            //
                            await ClickProceed();
                        }
                        return;
                    }
                    //
                    if (url.Contains("oltaschlndetl.xhtml"))
                    {
                        if (!_proceedEntryClicked)
                        {
                            _proceedEntryClicked = true;

                            await Task.Delay(1000);

                            await FillOLTASChallanDetails();
                        }
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region TRACK REQUEST
                    if (!_correctionSearchDone && url.Contains("trackoltascorr.xhtml"))
                    {
                        _correctionSearchDone = true;
                        await ClickViewAllRadio();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private async Task ClickViewAllRadio()
        {
            await webView2.CoreWebView2.ExecuteScriptAsync(@"
        document.getElementById('search3').click();
    ");
        }

        private async Task FillOLTASChallanDetails()
        {
            string bsrCode = _objData.TracesData.BSRCode;
            string depositDate = _objData.TracesData.FromChallanDepositDate; // dd-MMM-yyyy
            string challanSerialNo = _objData.TracesData.CDRecordNumber;
            string challanAmount = _objData.TracesData.ChallanAmount;
            //
            string script = @"
                (function(){

                    var bsr =
                        document.getElementById('bsrCode');

                    var dt =
                        document.getElementById('dateOfDep');

                    var csn =
                        document.getElementById('chlnSNo');

                    var amt =
                        document.getElementById('chlnAmt');

                    if(!bsr || !dt || !csn || !amt)
                        return 'FIELD_NOT_FOUND';

                    bsr.value = '" + bsrCode + @"';
                    dt.value = '" + depositDate + @"';
                    csn.value = '" + challanSerialNo + @"';
                    amt.value = '" + challanAmount + @"';

                    bsr.dispatchEvent(new Event('change', { bubbles:true }));
                    dt.dispatchEvent(new Event('change', { bubbles:true }));
                    csn.dispatchEvent(new Event('change', { bubbles:true }));
                    amt.dispatchEvent(new Event('change', { bubbles:true }));

                    var btn =
                        document.getElementById('clickGo');

                    if(!btn)
                        return 'GO_BUTTON_NOT_FOUND';

                    btn.click();

                    return 'SUCCESS';

                })();
                ";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            //MessageBox.Show(result);
        }

        private async Task ClickProceed()
        {
            string script = @"
(function(){

    var btn =
        document.getElementById('proceed');

    if(!btn)
        return 'PROCEED_NOT_FOUND';

    btn.click();

    return 'CLICKED';

})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            //MessageBox.Show(result);
        }

        #region FillKYCDetails
        private async Task FillKYCDetails()
        {
            DateTime dt =
            Convert.ToDateTime(
            _objData.TracesData.FromChallanDepositDate);


            string depositDate =
                dt.ToString("dd-MMM-yyyy");

            string script = @"
            (function(){

                document.getElementById('token').value='" +
                    _objData.TracesData.PRN_NO + @"';

                document.getElementById('bsr').value='" +
                    _objData.TracesData.BSRCode + @"';

                document.getElementById('dtoftaxdep').value='" +
                    depositDate + @"';

                document.getElementById('csn').value='" +
                    _objData.TracesData.ChallanSerialNo + @"';

                document.getElementById('chlnamt').value='" +
                    _objData.TracesData.ChallanAmount + @"';

                document.getElementById('cdrecnum').value='" +
                    _objData.TracesData.CDRecordNumber + @"';

                document.getElementById('pan1').value='" +
                    _objData.TracesData.PAN1 + @"';

                document.getElementById('amt1').value='" +
                    _objData.TracesData.PAN1Amount + @"';

                document.getElementById('pan2').value='" +
                    _objData.TracesData.PAN2 + @"';

                document.getElementById('amt2').value='" +
                    _objData.TracesData.PAN2Amount + @"';

                document.getElementById('pan3').value='" +
                    _objData.TracesData.PAN3 + @"';

                document.getElementById('amt3').value='" +
                    _objData.TracesData.PAN3Amount + @"';

                document.getElementById('clickKYC').click();

            })();
            ";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            //MessageBox.Show(result);


        }
        #endregion

        #region ClickProceedKYC
        private async Task ClickProceedKYC()
        {
            string result =
            await webView2.CoreWebView2.ExecuteScriptAsync(@"
    (function(){

        var btn = document.getElementById('nxtSrcn');


        btn.click();

    })();
    ");

            //MessageBox.Show(result);
        }
        #endregion

        #region ClickSearch3
        private async Task ClickSearch3()
        {
            await webView2.CoreWebView2.ExecuteScriptAsync(@"
        document.getElementById('search3').click();
    ");
        }
        #endregion
               

        #region OpenCorrectionRequest
        private async Task OpenOLTASChallanCorrectionRequest()
        {
            webView2.CoreWebView2.Navigate("https://traces61.tdscpc.gov.in/app/dea/chklstoltascorr.xhtml");
        }
        #endregion

        #region OpenRequestOLTASChallanCorrection
        private async Task OpenRequestOLTASChallanCorrection()
        {
            webView2.CoreWebView2.Navigate("https://traces61.tdscpc.gov.in/app/dea/reqoltascorr.xhtml");
        }
        #endregion
        
        #region TrackRequestOLTASChallanCorrection
        private async Task TrackRequestOLTASChallanCorrection()
        {
            webView2.CoreWebView2.Navigate("https://traces61.tdscpc.gov.in/app/dea/trackoltascorr.xhtml");
        }
        #endregion
        


        #region OpenCorrectionWelcomeRequest
        private async Task OpenCorrectionWelcomeRequest()
        {
            webView2.CoreWebView2.Navigate("https://traces61.tdscpc.gov.in/app/ded/corrdownload.xhtml");
        }
        #endregion

        #region ClickProceedPopup
        private async Task ClickProceedPopup()
        {
            try
            {
                await Task.Delay(1000);

                string script = @"
            (function() {
                var buttons = document.querySelectorAll('button');

                for (var i = 0; i < buttons.length; i++) {
                    if (buttons[i].innerText &&
                        buttons[i].innerText.trim().toLowerCase() === 'proceed') {
                        buttons[i].click();
                        return true;
                    }
                }

                return false;
            })();
        ";

                await webView2.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region SelectLatestAvailableRowAndClickStatus
        private async Task SelectLatestAvailableRowAndClickStatus(string fyForTraces, string qtrForTraces)
        {
            string fy = fyForTraces;          // 2025-26
            string qtr = qtrForTraces;        // Q4
            string form = _objData.TracesData.Forms; // 24Q

            string script = $@"
(function(){{

    var rows =
        document.querySelectorAll('#reqList tbody tr');

    for(var i=0;i<rows.length;i++)
    {{
        var row = rows[i];

        var cells = row.getElementsByTagName('td');

        if(cells.length < 7)
            continue;

        var rowFY =
            cells[2].innerText.trim();

        var rowQTR =
            cells[3].innerText.trim();

        var rowFORM =
            cells[4].innerText.trim();

        var rowSTATUS =
            cells[6].innerText.trim();

        if(
            rowFY == '{fy}'
            &&
            rowQTR == '{qtr}'
            &&
            rowFORM == '{form}'
            &&
            (
                rowSTATUS == 'Available'
                ||
                rowSTATUS == 'In Progress'
            )
        )
        {{
            row.click();

            cells[6].click();

            cells[6].dispatchEvent(
                new MouseEvent(
                    'click',
                    {{
                        bubbles:true,
                        cancelable:true
                    }}
                )
            );

            return;
        }}
    }}

    return;

}})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            //MessageBox.Show(result);
        }
        #endregion

        private async Task SelectCorrectionTypeAndViewDetails()
        {
            //string correctionType = Convert.ToString(CorrectionData.CorrectionType);
            string correctionType = Convert.ToString(_objData.CorrectionType);            
            // 3 = Challan Correction
            // 8 = Pay 220, LP, LD, Interest, Late Filing Levy
            // 9 = Add Challan To Statement

            string script = @"
    (function(){

        var ddl = document.getElementById('correctionType');
        if(!ddl)
            return true;

        ddl.value = '" + correctionType + @"';

        ddl.dispatchEvent(new Event('change', { bubbles:true }));

        var btn = document.getElementById('clickcorrnTyp');
        if(!btn)
            return false;

        btn.click();

        return true;

    })();
    ";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            // MessageBox.Show(result); // keep only for testing
        }

    }


}
