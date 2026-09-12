#region Refered Namespaces & Classes

using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

using TDSMAN.Classes;

using Microsoft.Web.WebView2.Core;

#endregion

namespace TDSMAN.FormBrowser
{
    public partial class TrnChallanCorrectionBrowser_2627 : Form
    {
        #region DECLARATIONS

        private readonly CorrectionData _objData;

        private bool _loginFilled = false;
        private bool _trackPageOpened = false;
        private bool _raiseRequestClicked = false;
        private bool _thingsToKnowProceedClicked = false;
        private bool _correctionTypeProceedClicked = false;
        private bool _challanSearchDone = false;
        private bool _oltasProceedSequenceStarted = false;

        private bool _timerBusy = false;
        private int _oltasStep = 0;

        #endregion

        #region CONSTRUCTOR

        public TrnChallanCorrectionBrowser_2627()
        {
            InitializeComponent();
        }

        public TrnChallanCorrectionBrowser_2627(CorrectionData objData)
        {
            InitializeComponent();
            _objData = objData;
        }

        #endregion

        #region FORM LOAD

        private async void TrnUploadTDS_Load(object sender, EventArgs e)
        {
            try
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

                webView2.CoreWebView2.Navigate(
                    "https://traces.tdscpc.gov.in/auth/login/loginScreen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region PAGE DETECTION / WORKFLOW

        private async void TmrPageDetect_Tick(object sender, EventArgs e)
        {
            if (_timerBusy)
                return;

            if (webView2 == null ||
                webView2.CoreWebView2 == null ||
                webView2.Source == null)
                return;

            _timerBusy = true;

            try
            {
                string url = webView2.Source.ToString();

                // ---------------------------------------------------------
                // STEP 1 - LOGIN PAGE
                // Fill TAN + Password only.
                // CAPTCHA and Login remain MANUAL.
                // ---------------------------------------------------------
                if (url.Contains("/auth/login"))
                {
                    if (!_loginFilled)
                    {
                        bool filled = await AutoFillLogin();

                        if (filled)
                            _loginFilled = true;
                    }

                    return;
                }

                // ---------------------------------------------------------
                // STEP 2 - DASHBOARD
                // Instead of depending on the menu/dropdown DOM, go to
                // the exact new TRACES OLTAS Track/Raise page.
                // Same authenticated WebView2 session is retained.
                // ---------------------------------------------------------
                if (url.Contains("deductorDashboard"))
                {
                    if (!_trackPageOpened)
                    {
                        _trackPageOpened = true;

                        webView2.CoreWebView2.Navigate(
                            "https://traces.tdscpc.gov.in/auth/oltas/trackRequestScreen");
                    }

                    return;
                }

                // ---------------------------------------------------------
                // STEP 3 - TRACK / RAISE REQUEST PAGE
                // Click "Raise Challan Correction Request"
                // ---------------------------------------------------------
                if (url.Contains("/auth/oltas/trackRequestScreen"))
                {
                    if (_objData != null &&
                        _objData.TracesData != null &&
                        _objData.TracesData.MakeChallanCorrectionRequest == true)
                    {
                        if (!_raiseRequestClicked)
                        {
                            _raiseRequestClicked = true;

                            // This is the page opened by
                            // "Raise Challan Correction Request".
                            // Direct navigation is more reliable than
                            // depending on the Angular button DOM.
                            webView2.CoreWebView2.Navigate(
                                "https://traces.tdscpc.gov.in/auth/oltas/oltasScreen");
                        }
                    }

                    // For Track/View mode, intentionally stop here for now.
                    return;
                }

                // ---------------------------------------------------------
                // STEP 4 / 5 / 6
                // All these screens currently use /auth/oltas/oltasScreen.
                // Therefore detect the visible heading instead of URL only.
                // ---------------------------------------------------------
                if (url.Contains("/auth/oltas/oltasScreen"))
                {
                    // STEP 0
                    // Things to Know -> Proceed
                    if (_oltasStep == 0)
                    {
                        await Task.Delay(2000);

                        // First bring the Flutter page to the bottom
                        await ScrollFlutterDown();

                        await Task.Delay(1000);

                        double x = webView2.ClientSize.Width - 110;
                        double y = 95;

                        await ClickFlutterPoint(x, y);

                        _oltasStep = 1;
                        return;
                    }

                    // STEP 1
                    // Select first Correction Type radio
                    if (_oltasStep == 1)
                    {
                        await Task.Delay(1500);

                        // First option:
                        // Tax Year or Financial Year / Major Head / Minor Head
                        await ClickFlutterPoint(323, 433);

                        await Task.Delay(700);

                        _oltasStep = 2;
                        return;
                    }

                    // STEP 2
                    // Scroll down and click Proceed
                    if (_oltasStep == 2)
                    {
                        await Task.Delay(1000);

                        // Bring bottom buttons into view
                        await ScrollFlutterDown();

                        await Task.Delay(1000);

                        // After scrolling, Proceed appears near top-right
                        double x = webView2.ClientSize.Width - 110;
                        double y = 95;

                        await ClickFlutterPoint(x, y);

                        _oltasStep = 3;

                        // STOP HERE for this test
                        //TmrPageDetect.Stop();

                        return;
                    }
                    // STEP 3
                    // Enter BSR Code
                    if (_oltasStep == 3)
                    {
                        await Task.Delay(1800);

                        await ClickFlutterPoint(780, 360);

                        await Task.Delay(300);

                        await InsertTextIntoFocusedFlutterField(
                            _objData.TracesData.BSRCode);

                        _oltasStep = 4;
                        return;
                    }


                    // STEP 4
                    // Enter Challan Serial Number
                    if (_oltasStep == 4)
                    {
                        await Task.Delay(500);

                        await ClickFlutterPoint(1105, 360);

                        await Task.Delay(300);

                        await InsertTextIntoFocusedFlutterField(
                            _objData.TracesData.CDRecordNumber);

                        _oltasStep = 5;
                        return;
                    }


                    // STEP 5
                    // Enter Challan Amount
                    if (_oltasStep == 5)
                    {
                        await Task.Delay(500);

                        await ClickFlutterPoint(780, 435);

                        await Task.Delay(300);

                        await InsertTextIntoFocusedFlutterField(
                            _objData.TracesData.ChallanAmount);

                        _oltasStep = 6;
                        return;
                    }


                    // STEP 6
                    // Select Date of Deposit from Flutter calendar
                    if (_oltasStep == 6)
                    {
                        await Task.Delay(500);

                        DateTime dt =
                            Convert.ToDateTime(
                                _objData.TracesData.FromChallanDepositDate);

                        // Open date picker
                        await ClickFlutterPoint(455, 435);

                        await Task.Delay(800);

                        // Select required date
                        await SelectFlutterDate(dt);

                        await Task.Delay(800);

                        _oltasStep = 7;
                        return;
                    }


                    // STEP 7
                    // Click Search
                    if (_oltasStep == 7)
                    {
                        await Task.Delay(1200);

                        await ClickFlutterPoint(1005, 435);

                        _oltasStep = 8;

                        // Stop here and inspect result
                        TmrPageDetect.Stop();

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _timerBusy = false;
            }
        }

        #endregion

        #region LOGIN

        private async Task<bool> AutoFillLogin()
        {
            try
            {
                if (_objData == null || _objData.TracesLogin == null)
                    return false;
    //            MessageBox.Show(
    //"WebView Width : " + webView2.ClientSize.Width +
    //"\nWebView Height : " + webView2.ClientSize.Height +
    //"\nDPI : " + this.DeviceDpi);

                // TRACES login is Flutter Web. The visible fields are painted
                // by Flutter, so normal document.querySelector() is not useful.
                //
                // Also, TAB navigation is not dependable until Flutter's
                // accessibility/semantics layer has keyboard focus.
                //
                // For the current TRACES login layout, click the actual Flutter
                // field through Chromium's DevTools input channel and then use
                // Input.insertText. These are viewport coordinates inside WebView2.

                webView2.Focus();
                Application.DoEvents();
                await Task.Delay(1200);

                // TAN field - current TRACES login page.
                //await ClickFlutterPoint(385, 435);
                await ClickFlutterPoint(
                                         AdjustX(385),
                                         435);
                await Task.Delay(250);
                await InsertTextIntoFocusedFlutterField(
                    _objData.TracesLogin.TAN);

                await Task.Delay(300);

                // Password field - current TRACES login page.
                //await ClickFlutterPoint(385, 610);
                await ClickFlutterPoint(
                                        AdjustX(385),
                                        610);
                await Task.Delay(250);
                await InsertTextIntoFocusedFlutterField(
                    _objData.TracesLogin.Password);

                // CAPTCHA is intentionally left completely manual.
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AutoFillLogin: " + ex.Message);
                return false;
            }
        }
        private double AdjustX(double baseX)
        {
            const double baseWebViewWidth = 1356.0;

            double extraWidth =
                webView2.ClientSize.Width - baseWebViewWidth;

            return baseX + (extraWidth / 3.0);
        }

        private async Task ClickFirstCorrectionTypeRadio()
        {
            // Flutter paints the radio control. There is no dependable HTML
            // input element to query.
            //
            // Coordinates are relative to the WebView2 viewport. On this
            // TRACES page the first radio is just to the right of the
            // 283-pixel left workflow panel.
            double x = 323;
            double y = 335;

            await ClickFlutterPoint(x, y);

            // Small second click on the label area gives tolerance for DPI /
            // Flutter hit-testing differences.
            await Task.Delay(120);
            await ClickFlutterPoint(360, y);
        }

        private async Task ClickCorrectionTypeProceed()
        {
            // On "Select Correction Type" the Proceed button is aligned at the
            // extreme bottom-right of the Flutter viewport, lower than the
            // Proceed button on "Things to Know".
            double x = Math.Max(20, webView2.ClientSize.Width - 110);
            double y = Math.Max(20, webView2.ClientSize.Height - 18);

            await ClickFlutterPoint(x, y);

            await Task.Delay(150);

            // Small tolerance click slightly upward.
            await ClickFlutterPoint(
                Math.Max(20, webView2.ClientSize.Width - 105),
                Math.Max(20, webView2.ClientSize.Height - 25));
        }

        private async Task ClickBottomRightProceed()
        {
            // Coordinates are relative to the WebView2 viewport.
            // On the maximized TDSMAN window the Proceed button sits
            // approximately 50-120 px from the right and 70-110 px
            // from the bottom. Click the center area of the button.
            double x = Math.Max(20, webView2.ClientSize.Width - 110);
            double y = Math.Max(20, webView2.ClientSize.Height - 90);

            await ClickFlutterPoint(x, y);

            // A second click a fraction lower acts as a small tolerance
            // for DPI / WebView layout variation without touching any
            // other control on this screen.
            await Task.Delay(150);

            await ClickFlutterPoint(
                Math.Max(20, webView2.ClientSize.Width - 105),
                Math.Max(20, webView2.ClientSize.Height - 88));
        }

        private async Task ClickFlutterPoint(double x, double y)
        {
            string sx = x.ToString(
                CultureInfo.InvariantCulture);

            string sy = y.ToString(
                CultureInfo.InvariantCulture);

            string common =
                "\"x\":" + sx +
                ",\"y\":" + sy +
                ",\"button\":\"left\"" +
                ",\"clickCount\":1";

            await webView2.CoreWebView2.CallDevToolsProtocolMethodAsync(
                "Input.dispatchMouseEvent",
                "{\"type\":\"mouseMoved\"," +
                "\"x\":" + sx + "," +
                "\"y\":" + sy + "}");

            await webView2.CoreWebView2.CallDevToolsProtocolMethodAsync(
                "Input.dispatchMouseEvent",
                "{\"type\":\"mousePressed\"," +
                common + "}");

            await Task.Delay(60);

            await webView2.CoreWebView2.CallDevToolsProtocolMethodAsync(
                "Input.dispatchMouseEvent",
                "{\"type\":\"mouseReleased\"," +
                common + "}");
        }


        private async Task SelectFlutterDate(DateTime targetDate)
        {
            //-------------------------------------------------------
            // MONTH DROPDOWN
            //-------------------------------------------------------

            // Open Month dropdown
            await ClickFlutterPoint(415, 160);

            await Task.Delay(500);

            // Move mouse inside month list and force it to TOP
            // so January becomes the first visible month.
            await ScrollMonthListToTop();

            await Task.Delay(500);

            //-------------------------------------------------------
            // SELECT MONTH
            //-------------------------------------------------------

            // After scrolling to top:
            //
            // January  = Y 202
            // February = Y 242
            // March    = Y 282
            // April    = Y 322
            // May      = Y 362
            // June     = Y 402
            //
            // For Jul-Dec we scroll to bottom separately.

            double monthX = 415;
            double monthY;

            if (targetDate.Month <= 6)
            {
                monthY =
                    202 + ((targetDate.Month - 1) * 40);
            }
            else
            {
                // Bring July-Dec into view
                await ScrollMonthListToBottom();

                await Task.Delay(400);

                // After scrolling to bottom:
                //
                // July      = Y 202
                // August    = Y 242
                // September = Y 282
                // October   = Y 322
                // November  = Y 362
                // December  = Y 402

                monthY =
                    202 + ((targetDate.Month - 7) * 40);
            }

            await ClickFlutterPoint(monthX, monthY);

            await Task.Delay(800);

            //-------------------------------------------------------
            // SELECT DAY
            //-------------------------------------------------------

            DateTime firstDay =
                new DateTime(
                    targetDate.Year,
                    targetDate.Month,
                    1);

            // Monday = column 0
            int firstColumn =
                ((int)firstDay.DayOfWeek + 6) % 7;

            int position =
                firstColumn + targetDate.Day - 1;

            int row =
                position / 7;

            int column =
                position % 7;

            // First calendar date row
            double startX = 366;
            double startY = 241;

            double columnGap = 36;
            double rowGap = 36;

            double dayX =
                startX + (column * columnGap);

            double dayY =
                startY + (row * rowGap);

            await ClickFlutterPoint(dayX, dayY);

            await Task.Delay(800);
        }

        private async Task ScrollMonthListToTop()
        {
            double x = 420;
            double y = 300;

            string sx =
                x.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

            string sy =
                y.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

            // Strong upward scroll inside the MONTH dropdown
            for (int i = 0; i < 4; i++)
            {
                await webView2.CoreWebView2
                    .CallDevToolsProtocolMethodAsync(
                    "Input.dispatchMouseEvent",
                    "{"
                    + "\"type\":\"mouseWheel\","
                    + "\"x\":" + sx + ","
                    + "\"y\":" + sy + ","
                    + "\"deltaX\":0,"
                    + "\"deltaY\":-800"
                    + "}"
                );

                await Task.Delay(150);
            }
        }
        private async Task ScrollMonthListToBottom()
        {
            double x = 420;
            double y = 300;

            string sx =
                x.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

            string sy =
                y.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

            // Strong downward scroll inside the MONTH dropdown
            for (int i = 0; i < 4; i++)
            {
                await webView2.CoreWebView2
                    .CallDevToolsProtocolMethodAsync(
                    "Input.dispatchMouseEvent",
                    "{"
                    + "\"type\":\"mouseWheel\","
                    + "\"x\":" + sx + ","
                    + "\"y\":" + sy + ","
                    + "\"deltaX\":0,"
                    + "\"deltaY\":800"
                    + "}"
                );

                await Task.Delay(150);
            }
        }

        private async Task SelectFlutterMonth(int targetMonth)
        {
            int currentMonth = DateTime.Today.Month;

            int difference = targetMonth - currentMonth;

            // Current selected month (September in your screenshot)
            // is approximately at this Y position after dropdown opens.
            double selectedMonthY = 361;

            // Each month row is approximately 40 pixels apart.
            double rowGap = 40;

            double targetY =
                selectedMonthY + (difference * rowGap);

            double targetX = 420;

            await ClickFlutterPoint(targetX, targetY);
        }

        private async Task SendFlutterKey(
    string key,
    int virtualKey)
        {
            string jsonDown =
                "{"
                + "\"type\":\"keyDown\","
                + "\"key\":\"" + key + "\","
                + "\"windowsVirtualKeyCode\":" + virtualKey
                + "}";

            await webView2.CoreWebView2
                .CallDevToolsProtocolMethodAsync(
                    "Input.dispatchKeyEvent",
                    jsonDown);

            string jsonUp =
                "{"
                + "\"type\":\"keyUp\","
                + "\"key\":\"" + key + "\","
                + "\"windowsVirtualKeyCode\":" + virtualKey
                + "}";

            await webView2.CoreWebView2
                .CallDevToolsProtocolMethodAsync(
                    "Input.dispatchKeyEvent",
                    jsonUp);
        }
        private async Task ScrollFlutterDown()
        {
            double x = webView2.ClientSize.Width / 2;
            double y = webView2.ClientSize.Height / 2;

            string sx = x.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string sy = y.ToString(System.Globalization.CultureInfo.InvariantCulture);

            // Scroll down several times
            for (int i = 0; i < 5; i++)
            {
                await webView2.CoreWebView2.CallDevToolsProtocolMethodAsync(
                    "Input.dispatchMouseEvent",
                    "{"
                    + "\"type\":\"mouseWheel\","
                    + "\"x\":" + sx + ","
                    + "\"y\":" + sy + ","
                    + "\"deltaX\":0,"
                    + "\"deltaY\":700"
                    + "}"
                );

                await Task.Delay(200);
            }
        }
        private async Task InsertTextIntoFocusedFlutterField(string value)
        {
            // Input.insertText is sent by Chromium itself to whichever Flutter
            // text-editing control currently has focus. This avoids SendKeys
            // treating password characters such as +, ^, %, {, } etc. specially.
            string json = "{\"text\":\"" + JsonEscape(value ?? "") + "\"}";

            await webView2.CoreWebView2.CallDevToolsProtocolMethodAsync(
                "Input.insertText",
                json);
        }

        private static string JsonEscape(string value)
        {
            if (value == null)
                return "";

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }

        #endregion

        #region THINGS TO KNOW / CORRECTION TYPE

        private async Task<bool> SelectTaxYearCorrectionTypeAndProceed()
        {
            string script = @"
(function () {

    var bodyText =
        (document.body.innerText || document.body.textContent || '')
        .toLowerCase();

    if (bodyText.indexOf('select correction type') < 0)
        return false;

    // Current page defaults to the required first option.
    // Still explicitly select the first radio button.
    var radios = Array.from(
        document.querySelectorAll(""input[type='radio']"")
    );

    if (radios.length > 0) {
        var radio = radios[0];

        if (!radio.checked) {
            radio.click();
            radio.dispatchEvent(
                new Event('change', { bubbles: true })
            );
        }
    }

    var candidates = Array.from(
        document.querySelectorAll(
            ""button, input[type='button'], input[type='submit'], a""
        )
    );

    for (var i = 0; i < candidates.length; i++) {
        var el = candidates[i];

        var text =
            ((el.innerText || el.textContent || el.value || ''))
            .trim()
            .toLowerCase();

        if (text === 'proceed') {
            el.click();
            return true;
        }
    }

    return false;

})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            return ScriptResultIsTrue(result);
        }

        #endregion

        #region FILL CHALLAN DETAILS

        private async Task<bool> FillNewOLTASChallanDetails()
        {
            if (_objData == null || _objData.TracesData == null)
                return false;

            string bsrCode =
                JsEscape(_objData.TracesData.BSRCode);

            string challanSerialNo =
                JsEscape(_objData.TracesData.CDRecordNumber);

            string challanAmount =
                JsEscape(_objData.TracesData.ChallanAmount);

            DateTime dt =
                Convert.ToDateTime(
                    _objData.TracesData.FromChallanDepositDate);

            // New TRACES date input is expected in normal Indian date form.
            string depositDate =
                JsEscape(dt.ToString("dd/MM/yyyy"));

            string script = @"
(function () {

    function visible(el) {
        if (!el) return false;
        var r = el.getBoundingClientRect();
        var s = window.getComputedStyle(el);
        return r.width > 0 &&
               r.height > 0 &&
               s.display !== 'none' &&
               s.visibility !== 'hidden';
    }

    function setValue(el, value) {
        if (!el) return false;

        var proto = Object.getPrototypeOf(el);
        var desc = Object.getOwnPropertyDescriptor(proto, 'value');

        if (desc && desc.set)
            desc.set.call(el, value);
        else
            el.value = value;

        el.dispatchEvent(new Event('input',  { bubbles: true }));
        el.dispatchEvent(new Event('change', { bubbles: true }));
        el.dispatchEvent(new Event('blur',   { bubbles: true }));

        return true;
    }

    function byPlaceholder(text) {
        text = text.toLowerCase();

        var inputs = Array.from(
            document.querySelectorAll('input')
        );

        for (var i = 0; i < inputs.length; i++) {
            var p =
                (inputs[i].getAttribute('placeholder') || '')
                .toLowerCase();

            if (p.indexOf(text) >= 0 && visible(inputs[i]))
                return inputs[i];
        }

        return null;
    }

    function inputFromLabel(text) {
        text = text.toLowerCase();

        var labels = Array.from(
            document.querySelectorAll('label')
        );

        for (var i = 0; i < labels.length; i++) {
            var lbl = labels[i];

            var t =
                (lbl.innerText || lbl.textContent || '')
                .trim()
                .toLowerCase();

            if (t.indexOf(text) < 0)
                continue;

            if (lbl.htmlFor) {
                var byFor = document.getElementById(lbl.htmlFor);
                if (byFor) return byFor;
            }

            var parent = lbl.parentElement;
            if (parent) {
                var inParent = parent.querySelector('input');
                if (inParent) return inParent;
            }
        }

        return null;
    }

    var bsr =
        byPlaceholder('bsr code') ||
        inputFromLabel('bsr code');

    var deposit =
        byPlaceholder('date of deposit') ||
        inputFromLabel('date of deposit');

    var serial =
        byPlaceholder('challan serial number') ||
        inputFromLabel('challan serial number');

    var amount =
        byPlaceholder('challan amount') ||
        inputFromLabel('challan amount');

    if (!bsr || !deposit || !serial || !amount)
        return false;

    setValue(bsr,     '" + bsrCode + @"');
    setValue(deposit, '" + depositDate + @"');
    setValue(serial,  '" + challanSerialNo + @"');
    setValue(amount,  '" + challanAmount + @"');

    return true;

})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            return ScriptResultIsTrue(result);
        }

        #endregion

        #region GENERIC PAGE HELPERS

        private async Task<bool> PageContainsText(string text)
        {
            string findText = JsEscape(text.ToLowerInvariant());

            string script = @"
(function () {
    var txt =
        (document.body.innerText || document.body.textContent || '')
        .toLowerCase();

    return txt.indexOf('" + findText + @"') >= 0;
})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            return ScriptResultIsTrue(result);
        }

        private async Task<bool> ClickButtonByText(string buttonText)
        {
            string findText =
                JsEscape(buttonText.ToLowerInvariant());

            string script = @"
(function () {

    function isVisible(el) {
        if (!el) return false;

        var r = el.getBoundingClientRect();
        var s = window.getComputedStyle(el);

        return r.width > 0 &&
               r.height > 0 &&
               s.display !== 'none' &&
               s.visibility !== 'hidden';
    }

    var candidates = Array.from(
        document.querySelectorAll(
            ""button, a, input[type='button'], input[type='submit'], [role='button']""
        )
    );

    for (var i = 0; i < candidates.length; i++) {
        var el = candidates[i];

        if (!isVisible(el))
            continue;

        var text =
            ((el.innerText || el.textContent || el.value || ''))
            .replace(/\s+/g, ' ')
            .trim()
            .toLowerCase();

        if (text === '" + findText + @"' ||
            text.indexOf('" + findText + @"') >= 0) {

            if (el.disabled)
                return false;

            el.scrollIntoView({
                block: 'center',
                inline: 'center'
            });

            el.click();

            return true;
        }
    }

    // Fallback - text may be inside a span/div nested in
    // an Angular/Material clickable control.
    var all = Array.from(document.querySelectorAll('span, div'));

    for (var j = 0; j < all.length; j++) {
        var node = all[j];

        if (!isVisible(node))
            continue;

        var nodeText =
            ((node.innerText || node.textContent || ''))
            .replace(/\s+/g, ' ')
            .trim()
            .toLowerCase();

        if (nodeText !== '" + findText + @"')
            continue;

        var clickable =
            node.closest(
                ""button, a, [role='button'], .mat-mdc-button-base, .mat-button, .btn""
            );

        if (clickable) {
            clickable.scrollIntoView({
                block: 'center',
                inline: 'center'
            });

            clickable.click();
            return true;
        }
    }

    return false;

})();
";

            string result =
                await webView2.CoreWebView2.ExecuteScriptAsync(script);

            return ScriptResultIsTrue(result);
        }

        private static bool ScriptResultIsTrue(string result)
        {
            return string.Equals(
                (result ?? "").Trim(),
                "true",
                StringComparison.OrdinalIgnoreCase);
        }

        private static string JsEscape(string value)
        {
            if (value == null)
                return "";

            return value
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        #endregion
    }
}
