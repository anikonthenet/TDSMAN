

#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.Data;

using System.Drawing;
using System.Windows.Forms;
using TDSMAN.Classes;
using System.Globalization;
using Newtonsoft.Json.Linq;
#endregion



namespace TDSMAN.Classes
{
    public class TracesConnect_OneLogin
    {

        ////public class TracesStatementStatus
        ////{
        ////    public string Quarter { get; set; }        // Q2 (2025-26)
        ////    public string Form { get; set; }           // 24Q / 26Q / 27Q / 27EQ

        ////    //public string RegularStatus { get; set; }  // Filed / Processed / Not Filed
        ////    public int RegularStatus { get; set; }  // Filed / Processed / Not Filed
        ////    public int CorrectionCount { get; set; }
        ////    public int ProcessedCount { get; set; }
        ////}

        public class TracesStatementStatus
        {
            public string Parameter { get; set; }
            public string Form24Q { get; set; }
            public string Form26Q { get; set; }
            public string Form27Q { get; set; }
            public string Form27EQ { get; set; }
            public int RegularStatus { get; internal set; }
        }

        public class TracesQuarterTab
        {
            public string TabDivId { get; set; }      // tab1, tab2...
            public string TabIndex { get; set; }      // '1' in getYearQtrSet(..., ..., '1')
            public string QtrCode { get; set; }       // '4' in getYearQtrSet('4',...)
            public string FinYear { get; set; }       // '2025' in getYearQtrSet(...,'2025',...)
            public string QuarterLabel { get; set; }  // "Q2 (2025-26)"
        }

        //public class TracesTableRow
        //{
        //    public List<string> Cells { get; set; } = new List<string>();
        //}

        public class TracesTableRow
        {
            public string FinYear { get; set; }
            public string Quarter { get; set; }
            public List<string> Cells { get; set; } = new List<string>();
        }


        //private static readonly TracesConnect _client = new TracesConnect();
        private static TracesConnect _client = new TracesConnect();

        private CookieContainer _cookieContainer;

        //string strBaseURL_ = "https://traces61.tdscpc.gov.in/app/"; //-- 2026/04/08
        CookieContainer objContainer = new CookieContainer();
        string strBaseURL = "https://traces61.tdscpc.gov.in/app/"; //-- 2026/04/08
        private bool bnlSessionExists = false;
        // ----------- REFLECTION HELPERS  -----------------------

        #region GetCookieContainer
        private static CookieContainer GetCookieContainer()
        {
            var f = typeof(TracesConnect).GetField("objContainer",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (CookieContainer)f.GetValue(_client);
        }
        #endregion

        #region SetCookieContainer
        private static void SetCookieContainer(CookieContainer cc)
        {
            var f = typeof(TracesConnect).GetField("objContainer",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            f.SetValue(_client, cc);
        }
        #endregion

        #region Call_Post
        private static string Call_Post(string url, StringBuilder data)
        {
            var m = typeof(TracesConnect).GetMethod("makeHTTPPostRequest",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (string)m.Invoke(_client, new object[] { url, data });
        }
        #endregion

        #region Call_Get
        private static string Call_Get(string url)
        {
            var m = typeof(TracesConnect).GetMethod("makeHTTPGetRequest",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (string)m.Invoke(_client, new object[] { url });
        }
        private static string Call_Get_New(string url)
        {
            var m = typeof(TracesConnect).GetMethod(
                "makeHTTPGetRequest",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null,
                new Type[] { typeof(string) },
                null
            );

            if (m == null)
                throw new Exception("makeHTTPGetRequest(string) method not found.");

            return (string)m.Invoke(_client, new object[] { url });
        }
        #endregion

        // ----------- COOKIE SYNC -------------------------------

        #region ApplySharedCookies
        private static void ApplySharedCookies()
        {
            if (TracesSessionManager_OneLogin.SharedCookieContainer == null)
                TracesSessionManager_OneLogin.SharedCookieContainer = new CookieContainer();

            if (_client != null)
            {
                var field = typeof(TracesConnect).GetField("objContainer",
                                BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null)
                {
                    field.SetValue(_client, TracesSessionManager_OneLogin.SharedCookieContainer);
                }
            }
        }
        private static void ApplySharedCookies_New()
        {
            if (TracesSessionManager_OneLogin.SharedCookieContainer == null)
                TracesSessionManager_OneLogin.SharedCookieContainer = new CookieContainer();

            if (_client != null)
            {
                var field = typeof(TracesConnect)
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .FirstOrDefault(f => f.Name == "objContainer");

                if (field != null)
                {
                    field.SetValue(_client, TracesSessionManager_OneLogin.SharedCookieContainer);
                }
            }
        }
        #endregion

        #region SaveSharedCookies
        private static void SaveSharedCookies()
        {
            CookieContainer cc = GetCookieContainer();
            TracesSessionManager_OneLogin.SharedCookieContainer = cc;
        }
        #endregion

        // --------------------------------------------------------
        // STEP 1: INITIAL REQUEST
        // --------------------------------------------------------
        #region MakeInitialRequest_OneLogin
        public static Stream MakeInitialRequest_OneLogin()
        {
            ApplySharedCookies();

            var m = typeof(TracesConnect).GetMethod("MakeInitialRequest",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            Stream s = (Stream)m.Invoke(_client, null);

            SaveSharedCookies();

            return s;
        }
        #endregion

        #region MakeInitialRequest_OneLogin_New
        public static string MakeInitialRequest_OneLogin_New(string accessToken)
        {
            try
            {
                ApplySharedCookies();

                var cookieJar = TracesSessionManager_OneLogin.SharedCookieContainer;

                // =========================
                // STEP 1: BASE DOMAIN HIT
                // =========================
                HttpWebRequest req1 = (HttpWebRequest)WebRequest.Create(
                    "https://traces61.tdscpc.gov.in/");

                req1.Method = "GET";
                req1.CookieContainer = cookieJar;
                req1.UserAgent = "Mozilla/5.0";
                req1.Referer = "https://traces.tdscpc.gov.in/";

                using (HttpWebResponse resp1 = (HttpWebResponse)req1.GetResponse())
                {
                    if (resp1.Cookies != null)
                        cookieJar.Add(resp1.Cookies);
                }

                // =========================
                // STEP 2: PREAUTH (CRITICAL)
                // =========================
                HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml");

                req2.Method = "GET";
                req2.CookieContainer = cookieJar;
                req2.UserAgent = "Mozilla/5.0";
                req2.Referer = "https://traces.tdscpc.gov.in/";

                // 🔥 VERY IMPORTANT
                req2.Headers.Add("Authorization", "Bearer " + accessToken);

                string html = "";

                using (HttpWebResponse resp2 = (HttpWebResponse)req2.GetResponse())
                {
                    if (resp2.Cookies != null)
                        cookieJar.Add(resp2.Cookies);

                    using (var reader = new StreamReader(resp2.GetResponseStream()))
                    {
                        html = reader.ReadToEnd();
                    }
                }

                // =========================
                // STEP 3: SAVE COOKIES
                // =========================
                SaveSharedCookies();

                SyncCookieContainer();

                return html;
            }
            catch (Exception ex)
            {
                throw new Exception("Initial request failed: " + ex.Message);
            }
        }
        #endregion

        public static (Stream imageStream, string captchaId) GetCaptcha_New()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://traces-app.tdscpc.gov.in/loginservice/api/auth/generateCaptcha");

            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = "Mozilla/5.0";

            var response = (HttpWebResponse)request.GetResponse();

            string json;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }

            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            string captchaBase64 = obj.image;
            string captchaId = obj.id;

            byte[] bytes = Convert.FromBase64String(captchaBase64);

            return (new MemoryStream(bytes), captchaId);
        }

        // --------------------------------------------------------
        // STEP 2: LOGIN
        // --------------------------------------------------------
        #region MakeLoginToTRACES_OneLogin
        public static TracesResponse MakeLoginToTRACES_OneLogin(
            string tan, string userid, string password, string captcha)
        {
            ResetOneLoginState();

            ApplySharedCookies();

            TracesLogin loginData = new TracesLogin()
            {
                TAN = tan,
                UserID = userid,
                Password = password,
                CaptchaCode = captcha
            };

            var m = typeof(TracesConnect).GetMethod("makeLoginToTRACES",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            TracesResponse res = (TracesResponse)m.Invoke(_client, new object[] { loginData });

            if (res == null)
            {
                throw new Exception("Login response is NULL. Cookies are not applied.");
            }

            if (res.Respons == enmResponse.Success)
            {
                SaveSharedCookies();
                TracesSessionManager_OneLogin.MarkLoginSuccess();
                // >>> ADD THIS FIX <<< 
                SyncCookieContainer();
            }

            if (res.Respons == enmResponse.Failed)
            {
                throw new Exception("Login failed: " + (res.Message));
            }

            return res;
        }
        #endregion

        #region MakeLoginToTRACES_OneLogin_New
        public static TracesResponse MakeLoginToTRACES_OneLogin_New(
            string tan,
            string userid,
            string password,
            string captcha,
            string captchaId)
        {
            TracesResponse result = new TracesResponse();

            try
            {
                ResetOneLoginState();

                // Ensure shared cookie container exists
                if (TracesSessionManager_OneLogin.SharedCookieContainer == null)
                    TracesSessionManager_OneLogin.SharedCookieContainer = new CookieContainer();

                var cookieJar = TracesSessionManager_OneLogin.SharedCookieContainer;

                // =========================
                // STEP 1: LOGIN API CALL
                // =========================
                var request = (HttpWebRequest)WebRequest.Create(
                    "https://traces-app.tdscpc.gov.in/loginservice/api/auth/login");

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.UserAgent = "Mozilla/5.0";
                request.CookieContainer = cookieJar;
                request.KeepAlive = true;

                request.Headers.Add("Origin", "https://traces.tdscpc.gov.in");
                request.Referer = "https://traces.tdscpc.gov.in/";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                var payload = new
                {
                    userId = tan,
                    password = password,
                    captcha = captcha.Trim(),
                    captchaId = captchaId,
                    userType = "Deductor",
                    subUser = false,
                    subUserPanId = ""
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var response = (HttpWebResponse)request.GetResponse();

                if (response.Cookies != null)
                    cookieJar.Add(response.Cookies);

                string responseText;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText);
                string accessToken = obj.authTokenDto?.accessToken;

                if (string.IsNullOrEmpty(accessToken))
                {
                    result.Respons = enmResponse.Failed;
                    result.Message = "Login failed - token not received";
                    return result;
                }

                // =========================
                // STEP 2: SESSION BRIDGE (PREAUTH)
                // =========================
                HttpWebRequest preauthReq = (HttpWebRequest)WebRequest.Create(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml");

                preauthReq.Method = "GET";
                preauthReq.CookieContainer = cookieJar;
                preauthReq.UserAgent = "Mozilla/5.0";
                preauthReq.Referer = "https://traces.tdscpc.gov.in/";

                // IMPORTANT: pass token
                preauthReq.Headers.Add("Authorization", "Bearer " + accessToken);

                HttpWebResponse preauthResp = (HttpWebResponse)preauthReq.GetResponse();

                if (preauthResp.Cookies != null)
                    cookieJar.Add(preauthResp.Cookies);

                // =========================
                // STEP 3: VALIDATE SESSION
                // =========================
                HttpWebRequest testReq = (HttpWebRequest)WebRequest.Create(
                    "https://traces61.tdscpc.gov.in/app/ded/filedownload.xhtml");

                testReq.Method = "GET";
                testReq.CookieContainer = cookieJar;
                testReq.UserAgent = "Mozilla/5.0";
                testReq.Referer = "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml";

                HttpWebResponse testResp = (HttpWebResponse)testReq.GetResponse();

                string html;
                using (var reader = new StreamReader(testResp.GetResponseStream()))
                {
                    html = reader.ReadToEnd();
                }

                if (!html.Contains("filedownload"))
                {
                    result.Respons = enmResponse.Failed;
                    result.Message = "Session not established after login.";
                    return result;
                }

                // =========================
                // STEP 4: FINALIZE SESSION
                // =========================
                TracesSessionManager_OneLogin.MarkLoginSuccess();

                SyncCookieContainer();

                result.Respons = enmResponse.Success;
                result.Message = "Login successful";

                return result;
            }
            catch (WebException ex)
            {
                string err = "";

                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }

                result.Respons = enmResponse.Failed;
                //result.Message = "Login error: " + err;
                var doc = System.Text.Json.JsonDocument.Parse(err);
                string message = doc.RootElement.GetProperty("message").GetString();
                err = message;
                result.Message = "Login error: " + err;

                return result;
            }
            catch (Exception ex)
            {
                result.Respons = enmResponse.Failed;
                result.Message = ex.Message;
                return result;
            }
        }
        #endregion
        // --------------------------------------------------------
        // RE-USABLE GET/POST WRAPPERS
        // --------------------------------------------------------
        #region MakeHTTPGet_OneLogin
        public static string MakeHTTPGet_OneLogin(string url)
        {
            ApplySharedCookies();
            string html = Call_Get(url);
            SaveSharedCookies();
            return html;
        }

        public static string MakeHTTPGet_OneLogin_New(string url)
        {
            ApplySharedCookies_New();
            string html = Call_Get_New(url);
            SaveSharedCookies();
            return html;
        }
        #endregion

        #region MakeHTTPPost_OneLogin
        public static string MakeHTTPPost_OneLogin(string url, StringBuilder data)
        {
            ApplySharedCookies();
            string html = Call_Post(url, data);
            SaveSharedCookies();
            return html;
        }

        private static string MakeHTTPPost_OneLogin(
                                            string url,
                                            StringBuilder postData,
                                            Dictionary<string, string> headers)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.CookieContainer = _cookieContainer; // existing logged-in cookies
            request.UserAgent = "Mozilla/5.0";

            foreach (var h in headers)
                request.Headers[h.Key] = h.Value;

            byte[] data = Encoding.UTF8.GetBytes(postData.ToString());
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
                stream.Write(data, 0, data.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
                return reader.ReadToEnd();
        }

        #endregion

        #region Logoff_OneLogin
        public static TracesResponse Logoff_OneLogin()
        {
            TracesResponse res = new TracesResponse();
            string strBaseURL = "https://www.tdscpc.gov.in/app/";
            try
            {
                ApplySharedCookies();

                // TRACES logout path
                string html = Call_Get(strBaseURL + "logout.xhtml");

                // Clear shared cookie container
                TracesSessionManager_OneLogin.ClearSession();

                res.Respons = enmResponse.Success;
                res.Message = "Logged out successfully";
            }
            catch (Exception ex)
            {
                res.Respons = enmResponse.Failed;
                res.Message = ex.Message;
            }

            return res;
        }
        #endregion

        #region MakeHTTPPostBytes_OneLogin
        public static byte[] MakeHTTPPostBytes_OneLogin(string url, string data)
        {
            ApplySharedCookies();
            byte[] bytes = Call_PostBytes(url, data);
            SaveSharedCookies();
            return bytes;
        }
        #endregion

        #region Call_PostBytes
        private static byte[] Call_PostBytes(string url, string data)
        {
            byte[] postData = Encoding.UTF8.GetBytes(data);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.CookieContainer = TracesSessionManager_OneLogin.SharedCookieContainer;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postData.Length;
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (Stream s = req.GetRequestStream())
                s.Write(postData, 0, postData.Length);

            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (MemoryStream ms = new MemoryStream())
            {
                res.GetResponseStream().CopyTo(ms);
                return ms.ToArray();
            }
        }
        #endregion

        #region ResetOneLoginState
        public static void ResetOneLoginState()
        {
            try
            {
                // --- 1. Reset Shared Cookie Container fully ---
                TracesSessionManager_OneLogin.SharedCookieContainer = new CookieContainer();

                // --- 2. Reset flags ---
                TracesSessionManager_OneLogin.LastLoginTime = DateTime.MinValue;
                TracesSessionManager_OneLogin.IsLoggedIn = false;

                // --- 3. Reset TracesConnect internal objContainer (very important) ---
                var field = typeof(TracesConnect).GetField("objContainer",
                                BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null && _client != null)
                {
                    field.SetValue(_client, new CookieContainer());
                }

                // --- 4. Delete previously saved cookies from disk ---
                TracesSessionManager_OneLogin.ClearSession();
            }
            catch
            {
                // swallow exceptions
            }
        }
        #endregion

        #region SyncCookieContainer
        private static void SyncCookieContainer()
        {
            try
            {
                if (_client == null) return;

                var field = typeof(TracesConnect)
                    .GetField("objContainer", BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null)
                    field.SetValue(_client, TracesSessionManager_OneLogin.SharedCookieContainer);
            }
            catch { }
        }
        #endregion
        
        #region GetOutstandingDemand_OneLogin
        public static TracesResponse GetOutstandingDemand_OneLogin(out string amount)
        {
            amount = "0.00";
            TracesResponse resp = new TracesResponse();
            //
            try
            {
                // 1️ Load dashboard to ensure session
                MakeHTTPGet_OneLogin("https://www.tdscpc.gov.in/app/ded/dashboard.xhtml");

                // 2️ Call the AJAX endpoint directly
                string html = MakeHTTPGet_OneLogin(
                    "https://www.tdscpc.gov.in/app/ded/dashbdoutdmnd.xhtml"                    
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Outstanding Demand response.";
                    return resp;
                }

                // 3️ Extract numeric value ONLY
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""outsdmndfinyear""[^>]*>\s*([0-9,.]+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    string raw = m.Groups[1].Value.Replace(",", "").Trim();
                    if (decimal.TryParse(raw, out decimal val))
                        amount = val.ToString("0.00");
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion

        #region GetOutstandingDemand_OneLogin_New
        public static TracesResponse GetOutstandingDemand_OneLogin_New(out string amount)
        {
            amount = "0.00";
            TracesResponse resp = new TracesResponse();
            //
            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // 1️ Load dashboard to ensure session
                MakeHTTPGet_OneLogin_New("https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml");


                // 2️ Call the AJAX endpoint directly
                string html = MakeHTTPGet_OneLogin_New(
                    "https://traces61.tdscpc.gov.in/app/ded/dashbdoutdmnd.xhtml"
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Outstanding Demand response.";
                    return resp;
                }

                // 3️ Extract numeric value ONLY
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""outsdmndfinyear""[^>]*>\s*([0-9,.]+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    string raw = m.Groups[1].Value.Replace(",", "").Trim();
                    if (decimal.TryParse(raw, out decimal val))
                        amount = val.ToString("0.00");
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion

        #region makeHTTPGetRequest
        //private string makeHTTPGetRequest(string url, string referer = "")
        //{
        //    SetAllowUnsafeHeaderParsing();

        //    ServicePointManager.ServerCertificateValidationCallback =
        //        new RemoteCertificateValidationCallback(delegate { return true; });

        //    ServicePointManager.Expect100Continue = false;
        //    ServicePointManager.SecurityProtocol =
        //        (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        //    request.Method = "GET";
        //    request.KeepAlive = true;

        //    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36";

        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        //    request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
        //    request.Headers.Add("Cache-Control", "no-cache");

        //    if (!string.IsNullOrEmpty(referer))
        //        request.Referer = referer;

        //    // IMPORTANT
        //    request.CookieContainer = objContainer;

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    objContainer.Add(response.Cookies);

        //    string result = "";

        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        result = reader.ReadToEnd();
        //    }

        //    response.Close();

        //    return result;
        //}
        #endregion

        #region SetAllowUnsafeHeaderParsing
        public static bool SetAllowUnsafeHeaderParsing()
        {
            //Get the assembly that contains the internal class
            Assembly aNetAssembly = Assembly.GetAssembly(
              typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null)
            {
                //Use the assembly in order to get the internal type for 
                // the internal class
                Type aSettingsType = aNetAssembly.GetType(
                  "System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null)
                {
                    //Use the internal static property to get an instance 
                    // of the internal settings class. If the static instance 
                    // isn't created allready the property will create it for us.
                    object anInstance = aSettingsType.InvokeMember("Section",
                      BindingFlags.Static | BindingFlags.GetProperty
                      | BindingFlags.NonPublic, null, null, new object[] { });
                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the 
                        // framework is unsafe header parsing should be 
                        // allowed or not
                        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField(
                          "useUnsafeHeaderParsing",
                          BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region GetInbox_OneLogin

        public static TracesResponse GetInbox_OneLogin(out int inboxCount)
        {
            inboxCount = 0;
            TracesResponse resp = new TracesResponse();

            try
            {
                // 1️ Load Dashboard (this is IMPORTANT)
                string html = MakeHTTPGet_OneLogin(
                    "https://www.tdscpc.gov.in/app/ded/dashbdinboxcount.xhtml"
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Dashboard response.";
                    return resp;
                }

                // 2️ Extract Inbox count
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""dwnldReqNo""[^>]*>\s*(\d+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    int.TryParse(m.Groups[1].Value.Trim(), out inboxCount);
                }
                else
                {
                    // Safe fallback
                    inboxCount = 0;
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion

        #region GetInbox_OneLogin_New

        public static TracesResponse GetInbox_OneLogin_New(out int inboxCount)
        {
            inboxCount = 0;
            TracesResponse resp = new TracesResponse();

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // 1️ Load Dashboard (this is IMPORTANT)
                string html = MakeHTTPGet_OneLogin_New(
                    "https://traces61.tdscpc.gov.in/app/ded/dashbdinboxcount.xhtml"
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Dashboard response.";
                    return resp;
                }

                // 2️ Extract Inbox count
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""dwnldReqNo""[^>]*>\s*(\d+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    int.TryParse(m.Groups[1].Value.Trim(), out inboxCount);
                }
                else
                {
                    // Safe fallback
                    inboxCount = 0;
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion

        #region GetDownloadRequests_OneLogin

        public static TracesResponse GetDownloadRequests_OneLogin(out int inboxCount)
        {
            inboxCount = 0;
            TracesResponse resp = new TracesResponse();

            try
            {
                // 1️ Load Dashboard (this is IMPORTANT)
                string html = MakeHTTPGet_OneLogin(
                    "https://www.tdscpc.gov.in/app/ded/dashbdreqdwnld.xhtml"
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Dashboard response.";
                    return resp;
                }

                // 2️ Extract Inbox count
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""dwnldReqNo""[^>]*>\s*(\d+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    int.TryParse(m.Groups[1].Value.Trim(), out inboxCount);
                }
                else
                {
                    // Safe fallback
                    inboxCount = 0;
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion

        #region GetDownloadRequests_OneLogin_New

        public static TracesResponse GetDownloadRequests_OneLogin_New(out int inboxCount)
        {
            inboxCount = 0;
            TracesResponse resp = new TracesResponse();

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // 1️ Load Dashboard (this is IMPORTANT)
                string html = MakeHTTPGet_OneLogin(
                    "https://traces61.tdscpc.gov.in/app/ded/dashbdreqdwnld.xhtml"
                );

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty Dashboard response.";
                    return resp;
                }

                // 2️ Extract Inbox count
                Match m = Regex.Match(
                    html,
                    @"<span\s+id\s*=\s*""dwnldReqNo""[^>]*>\s*(\d+)\s*</span>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                if (m.Success)
                {
                    int.TryParse(m.Groups[1].Value.Trim(), out inboxCount);
                }
                else
                {
                    // Safe fallback
                    inboxCount = 0;
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }

        #endregion



        #region  GetYourTRACESActivities_OneLogin
        public static TracesResponse GetYourTRACESActivities_OneLogin(out List<string> activities)
        {
            activities = new List<string>();
            TracesResponse resp = new TracesResponse();

            try
            {
                string url = "https://www.tdscpc.gov.in/app/ded/dashboardactivity.xhtml";

                // TRACES AJAX calls do not require any form payload
                string html = MakeHTTPGet_OneLogin(url);

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to load activities. Session may have expired.";
                    return resp;
                }

                // Extract only UL list
                Match ulMatch = Regex.Match(
                    html,
                    "<ul[^>]*class=\"[^\"]*w515[^\"]*\"[^>]*>(.*?)</ul>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (!ulMatch.Success)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "TRACES Activities list not found.";
                    return resp;
                }

                string ulHtml = ulMatch.Groups[1].Value;

                // Extract each <li> item
                MatchCollection liMatches = Regex.Matches(
                    ulHtml,
                    "<li[^>]*>(.*?)</li>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match m in liMatches)
                {
                    string text = Regex.Replace(m.Groups[1].Value, "<.*?>", "").Trim();
                    if (text.Length > 0)
                        activities.Add(text);
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion


        #region  GetYourTRACESActivities_OneLogin_New
        public static TracesResponse GetYourTRACESActivities_OneLogin_New(out List<string> activities)
        {
            activities = new List<string>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                string url = "https://traces61.tdscpc.gov.in/app/ded/dashboardactivity.xhtml";

                // TRACES AJAX calls do not require any form payload
                string html = MakeHTTPGet_OneLogin_New(url);

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to load activities. Session may have expired.";
                    return resp;
                }

                // Extract only UL list
                Match ulMatch = Regex.Match(
                    html,
                    "<ul[^>]*class=\"[^\"]*w515[^\"]*\"[^>]*>(.*?)</ul>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (!ulMatch.Success)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "TRACES Activities list not found.";
                    return resp;
                }

                string ulHtml = ulMatch.Groups[1].Value;

                // Extract each <li> item
                MatchCollection liMatches = Regex.Matches(
                    ulHtml,
                    "<li[^>]*>(.*?)</li>",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match m in liMatches)
                {
                    string text = Regex.Replace(m.Groups[1].Value, "<.*?>", "").Trim();
                    if (text.Length > 0)
                        activities.Add(text);
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region GetAlerts_OneLogin
        public static TracesResponse GetAlerts_OneLogin(out List<string> alerts)
        {
            alerts = new List<string>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // THIS is the real Alerts data URL
                string url = "https://www.tdscpc.gov.in/app/ded/dashboardalert.xhtml";

                // Use your authenticated GET method
                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin(url);

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to load Alerts. Session may have expired.";
                    return resp;
                }

                // Extract <li> elements
                MatchCollection mc = Regex.Matches(
                    html,
                    "<li[^>]*>(.*?)</li>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match m in mc)
                {
                    string clean = Regex.Replace(m.Groups[1].Value, "<.*?>", "").Trim() + "\n";
                    if (clean.Length > 0)
                        alerts.Add(clean);
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region GetAlerts_OneLogin_New
        public static TracesResponse GetAlerts_OneLogin_New(out List<string> alerts)
        {
            alerts = new List<string>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // THIS is the real Alerts data URL
                string url = "https://traces61.tdscpc.gov.in/app/ded/dashboardalert.xhtml";

                // Use your authenticated GET method
                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(url);

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to load Alerts. Session may have expired.";
                    return resp;
                }

                // Extract <li> elements
                MatchCollection mc = Regex.Matches(
                    html,
                    "<li[^>]*>(.*?)</li>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match m in mc)
                {
                    string clean = Regex.Replace(m.Groups[1].Value, "<.*?>", "").Trim() + "\n";
                    if (clean.Length > 0)
                        alerts.Add(clean);
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion


        #region GetStatementStatus_OneLogin (STEP 1 ONLY)
        //public static TracesResponse GetStatementStatus_OneLogin(
        //    out List<TracesStatementStatus> result)
        //    public static TracesResponse GetStatementStatus_OneLogin(
        //out List<TracesConnect_OneLogin.TracesStatementStatus> result)

        //{
        //    result = new List<TracesStatementStatus>();
        //    TracesResponse resp = new TracesResponse();

        //    try
        //    {
        //        // Quarter mapping exactly as TRACES uses
        //        var quarters = new[]
        //        {
        //    new { Q = "Q2", Code = "4" },
        //    new { Q = "Q1", Code = "3" },
        //    new { Q = "Q4", Code = "6" },
        //    new { Q = "Q3", Code = "5" }
        //};

        //        string finYear = DateTime.Now.Month >= 4
        //                            ? DateTime.Now.Year.ToString()
        //                            : (DateTime.Now.Year - 1).ToString();

        //        foreach (var q in quarters)
        //        {
        //            //string postData = $"finYear={finYear}&quarter={q.Code}";
        //            StringBuilder postData = new StringBuilder();
        //            postData.Append($"finYear={finYear}&quarter={q.Code}");
        //            //
        //            string url = "https://www.tdscpc.gov.in/app/ded/dashboarddefault.xhtml";

        //            string html = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(url, postData);
        //            //-- temp change
        //            string fileName = $"C:\\TRACES_STATEMENT_STATUS_{q.Code}.html";
        //            File.WriteAllText(fileName, html);
        //            //--
        //            if (string.IsNullOrWhiteSpace(html))
        //                continue;

        //            // Each FORM column block
        //            string[] forms = { "24Q", "26Q", "27Q", "27EQ" };

        //            foreach (string form in forms)
        //            {
        //                // --- REG STATUS (CSS BASED)
        //                Match regMatch = Regex.Match(
        //                    html,
        //                    $@"<td[^>]*>\s*<div[^>]*class=""([^""]*)""[^>]*>\s*</div>\s*</td>",
        //                    RegexOptions.IgnoreCase);

        //                string regCss = regMatch.Success
        //                                    ? regMatch.Groups[1].Value
        //                                    : "NA";

        //                // --- CORRECTION COUNT
        //                Match corMatch = Regex.Match(
        //                    html,
        //                    $@"<td[^>]*>\s*(\d+)\s*</td>",
        //                    RegexOptions.IgnoreCase);

        //                int corCount = corMatch.Success
        //                                    ? Convert.ToInt32(corMatch.Groups[1].Value)
        //                                    : 0;

        //                // --- PROCESSED COUNT
        //                Match procMatch = Regex.Match(
        //                    html,
        //                    $@"<td[^>]*>\s*(\d+)\s*</td>",
        //                    RegexOptions.IgnoreCase);

        //                int procCount = procMatch.Success
        //                                    ? Convert.ToInt32(procMatch.Groups[1].Value)
        //                                    : 0;

        //                result.Add(new TracesStatementStatus
        //                {
        //                    FinancialYear = finYear,
        //                    Quarter = q.Q,
        //                    FormType = form,
        //                    RegStatusCss = regCss,
        //                    CorrectionCount = corCount,
        //                    ProcessedCount = procCount
        //                });
        //            }
        //        }

        //        resp.Respons = enmResponse.Success;
        //        resp.Message = "OK";
        //        return resp;
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.Respons = enmResponse.Failed;
        //        resp.Message = ex.Message;
        //        return resp;
        //    }
        //}
        #endregion


        #region GetStatementStatus_OneLogin

        public static bool GetStatementStatus_Q2_Only_OneLogin(
            string dashboardHtml,
            out List<TracesStatementStatus> result,
            out string errorMessage)
        {
            result = new List<TracesStatementStatus>();
            errorMessage = "";

            try
            {
                // --------------------------------------------------
                // STEP 1: Extract Q2 quarter + financial year
                // --------------------------------------------------
                string qtr = Regex.Match(
                    dashboardHtml,
                    @"<input[^>]+id=""tb1qtr""[^>]+value=""(\d+)""",
                    RegexOptions.IgnoreCase).Groups[1].Value;

                string finYear = Regex.Match(
                    dashboardHtml,
                    @"<input[^>]+id=""tb1finyear""[^>]+value=""(\d+)""",
                    RegexOptions.IgnoreCase).Groups[1].Value;

                if (string.IsNullOrEmpty(qtr) || string.IsNullOrEmpty(finYear))
                {
                    errorMessage = "Q2 quarter / financial year not found";
                    return false;
                }

                // --------------------------------------------------
                // STEP 2: Call dashboarddefault.xhtml (AJAX)
                // --------------------------------------------------
                var postData = new StringBuilder();
                postData.Append("finYear=").Append(finYear);
                postData.Append("&quarter=").Append(qtr);

                var headers = new Dictionary<string, string>
        {
            { "X-Requested-With", "XMLHttpRequest" }
        };

                string ajaxHtml = MakeHTTPPost_OneLogin(
                    "https://tdscpc.gov.in/app/ded/dashboarddefault.xhtml",
                    postData,
                    headers
                );

                if (string.IsNullOrWhiteSpace(ajaxHtml))
                {
                    errorMessage = "Empty dashboarddefault response";
                    return false;
                }

                // --------------------------------------------------
                // STEP 3: Extract Statement Status table
                // --------------------------------------------------
                var tableMatch = Regex.Match(
                    ajaxHtml,
                    @"<table[^>]*class=""userList""[^>]*>(.*?)</table>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (!tableMatch.Success)
                {
                    errorMessage = "Statement Status table not found";
                    return false;
                }

                string tableHtml = tableMatch.Groups[1].Value;

                // --------------------------------------------------
                // STEP 4: Parse rows
                // --------------------------------------------------
                var rowMatches = Regex.Matches(
                    tableHtml,
                    @"<tr[^>]*>(.*?)</tr>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match row in rowMatches)
                {
                    var colMatches = Regex.Matches(
                        row.Groups[1].Value,
                        @"<td[^>]*>(.*?)</td>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (colMatches.Count < 5)
                        continue;

                    string Clean(string s) =>
                        Regex.Replace(s, "<.*?>", "").Replace("&nbsp;", "").Trim();

                    result.Add(new TracesStatementStatus
                    {
                        Parameter = Clean(colMatches[0].Value),
                        Form24Q = Clean(colMatches[1].Value),
                        Form26Q = Clean(colMatches[2].Value),
                        Form27Q = Clean(colMatches[3].Value),
                        Form27EQ = Clean(colMatches[4].Value)
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


        #endregion

        private static string GetCell(string rowHtml, int index)
        {
            var cells = Regex.Matches(rowHtml, @"<td[^>]*>(.*?)</td>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return cells.Count > (index + 0) ? cells[index + 0].Groups[1].Value : "";
        }

        private static int ParseInt(string html)
        {
            string text = Regex.Replace(html, "<.*?>", "").Trim();
            int.TryParse(text, out int val);
            return val;
        }


        private static List<TracesQuarterTab> ExtractQuarterTabs(string html)
        {
            var list = new List<TracesQuarterTab>();

            // <div id="tab1" ... onclick="getYearQtrSet('4', '2025' , '1')"> ... <a ...>Q2 (2025-26)</a>
            var tabMatches = Regex.Matches(
                html,
                @"<div\s+id=""(?<tabid>tab\d+)""[^>]*onclick\s*=\s*""getYearQtrSet\('(?<qtr>\d+)'\s*,\s*'(?<fy>\d+)'\s*,\s*'(?<idx>\d+)'\)""[^>]*>.*?<a[^>]*>(?<label>Q\d\s*\([^)]+\))</a>",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match m in tabMatches)
            {
                list.Add(new TracesQuarterTab
                {
                    TabDivId = m.Groups["tabid"].Value.Trim(),
                    TabIndex = m.Groups["idx"].Value.Trim(),
                    QtrCode = m.Groups["qtr"].Value.Trim(),
                    FinYear = m.Groups["fy"].Value.Trim(),
                    QuarterLabel = Regex.Replace(m.Groups["label"].Value, @"\s+", " ").Trim()
                });
            }

            return list;
        }

        private static string ExtractViewState(string html)
        {
            // JSF hidden field typically: name="javax.faces.ViewState" value="...."
            var m = Regex.Match(
                html,
                @"name=""javax\.faces\.ViewState""[^>]*value=""(?<vs>[^""]+)""",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            return m.Success ? m.Groups["vs"].Value : "";
        }

        #region GetStatementStatus_FirstTab_OneLogin //-- WORKED FOR FIRST TAB...
        public static TracesResponse GetStatementStatus_FirstTab_OneLogin(
            out List<TracesTableRow> tableRows)
        {
            tableRows = new List<TracesTableRow>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // ------------------------------------------------------------
                // STEP 1: Load dashboard page (shell)
                // ------------------------------------------------------------
                string dashboardHtml =
                    TracesConnect_OneLogin.MakeHTTPGet_OneLogin(
                        "https://www.tdscpc.gov.in/app/ded/dashboard.xhtml");

                if (string.IsNullOrWhiteSpace(dashboardHtml))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Dashboard page not loaded.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 2: Extract default FinYear & Quarter (TAB 1 ONLY)
                // ------------------------------------------------------------
                Match finYearMatch = Regex.Match(
                    dashboardHtml,
                    @"<input[^>]*id=""tb1finyear""[^>]*value=""(\d+)""",
                    RegexOptions.IgnoreCase);

                Match quarterMatch = Regex.Match(
                    dashboardHtml,
                    @"<input[^>]*id=""tb1qtr""[^>]*value=""(\d+)""",
                    RegexOptions.IgnoreCase);

                if (!finYearMatch.Success || !quarterMatch.Success)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Default FinYear / Quarter not found.";
                    return resp;
                }

                string finYear = finYearMatch.Groups[1].Value;   // e.g. 2025
                string quarter = quarterMatch.Groups[1].Value;   // e.g. 4

                // ------------------------------------------------------------
                // STEP 3: POST exactly like browser (AJAX)
                // ------------------------------------------------------------
                StringBuilder postData = new StringBuilder();
                postData.Append($"finYear={finYear}&quarter={quarter}");

                string statusHtml =
                    TracesConnect_OneLogin.MakeHTTPPost_OneLogin(
                        "https://www.tdscpc.gov.in/app/ded/dashboarddefault.xhtml",
                        postData);

                if (string.IsNullOrWhiteSpace(statusHtml))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Statement Status data not received.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 4: Extract Statement Status table
                // ------------------------------------------------------------
                Match tableMatch = Regex.Match(
                    statusHtml,
                    @"<table[^>]*class=""userList[^""]*""[^>]*>(.*?)</table>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (!tableMatch.Success)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Statement Status table not found.";
                    return resp;
                }

                string tableHtml = tableMatch.Groups[1].Value;

                // ------------------------------------------------------------
                // STEP 5: Parse rows & cells (AS-IS)
                // ------------------------------------------------------------
                MatchCollection rowMatches = Regex.Matches(
                    tableHtml,
                    @"<tr[^>]*>(.*?)</tr>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match rowMatch in rowMatches)
                {
                    TracesTableRow row = new TracesTableRow();
                    string rowHtml = rowMatch.Groups[1].Value;

                    MatchCollection cellMatches = Regex.Matches(
                        rowHtml,
                        @"<(th|td)[^>]*>(.*?)</\1>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    foreach (Match cellMatch in cellMatches)
                    {
                        string cellHtml = cellMatch.Groups[2].Value.Trim();
                        string cellValue;

                        // Colorbox handling
                        Match colorMatch = Regex.Match(
                            cellHtml,
                            @"class=""colorbox\s+([a-zA-Z]+)""",
                            RegexOptions.IgnoreCase);

                        if (colorMatch.Success)
                        {
                            cellValue = CultureInfo.InvariantCulture.TextInfo
                                .ToTitleCase(colorMatch.Groups[1].Value.ToLower());
                        }
                        else
                        {
                            cellValue = Regex.Replace(cellHtml, "<.*?>", "")
                                             .Replace("&nbsp;", "")
                                             .Trim();
                        }

                        row.Cells.Add(cellValue);
                    }

                    if (row.Cells.Count > 0)
                        tableRows.Add(row);
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = "Error extracting Statement Status: " + ex.Message;
                return resp;
            }
        }

        //using HtmlAgilityPack;

        //private bool ExtractFirstTabFinYearQuarter(
        //    string html,
        //    out string finYear,
        //    out string quarter)
        //    {
        //        finYear = "";
        //        quarter = "";

        //        var doc = new HtmlDocument();
        //        doc.LoadHtml(html);

        //        // STRICT: First tab hidden fields only
        //        var finYearNode = doc.GetElementbyId("tb1finyear");
        //        var quarterNode = doc.GetElementbyId("tb1qtr");

        //        if (finYearNode == null || quarterNode == null)
        //            return false;

        //        finYear = finYearNode.GetAttributeValue("value", "").Trim();
        //        quarter = quarterNode.GetAttributeValue("value", "").Trim();

        //        return !(string.IsNullOrEmpty(finYear) || string.IsNullOrEmpty(quarter));
        //    }

        //private string FetchDashboardDefault(
        //                    string cookies,
        //                    string finYear,
        //                    string quarter)
        //{
        //    string url = "https://www.tdscpc.gov.in/app/ded/dashboarddefault.xhtml";

        //    StringBuilder postData = new StringBuilder();
        //    postData.Append($"finYear={finYear}&quarter={quarter}");

        //    return MakeHTTPPost_OneLogin(
        //                                "https://www.tdscpc.gov.in/app/ded/dashboarddefault.xhtml",
        //                                postData
        //    );
        //}

        #endregion



        #region GetStatementStatus_OneLogin
        public static TracesResponse GetStatementStatus_OneLogin(
            out List<TracesTableRow> allRows)
        {
            allRows = new List<TracesTableRow>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // ------------------------------------------------------------
                // STEP 1: Load dashboard shell
                // ------------------------------------------------------------
                string dashboardHtml = MakeHTTPGet_OneLogin(
                    "https://www.tdscpc.gov.in/app/ded/dashboard.xhtml");

                if (string.IsNullOrWhiteSpace(dashboardHtml))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Dashboard not loaded.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 2: Discover tabs from onclick=getYearQtrSet(...)
                // ------------------------------------------------------------
                MatchCollection tabMatches = Regex.Matches(
                    dashboardHtml,
                    @"getYearQtrSet\(\s*'(\d+)'\s*,\s*'(\d+)'\s*,\s*'\d+'\s*\)",
                    RegexOptions.IgnoreCase);

                if (tabMatches.Count == 0)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "No Statement Status tabs found.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 3: Iterate each tab
                // ------------------------------------------------------------
                foreach (Match tab in tabMatches)
                {
                    string quarter = tab.Groups[1].Value;
                    string finYear = tab.Groups[2].Value;

                    // Browser-like POST
                    StringBuilder postData = new StringBuilder();
                    postData.Append($"finYear={finYear}&quarter={quarter}");

                    string statusHtml = MakeHTTPPost_OneLogin(
                        "https://www.tdscpc.gov.in/app/ded/dashboarddefault.xhtml",
                        postData);

                    if (string.IsNullOrWhiteSpace(statusHtml))
                        continue;

                    // --------------------------------------------------------
                    // STEP 4: Extract Statement Status table
                    // --------------------------------------------------------
                    Match tableMatch = Regex.Match(
                        statusHtml,
                        @"<table[^>]*class=""userList[^""]*""[^>]*>(.*?)</table>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (!tableMatch.Success)
                        continue;

                    MatchCollection rowMatches = Regex.Matches(
                        tableMatch.Groups[1].Value,
                        @"<tr[^>]*>(.*?)</tr>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    foreach (Match rowMatch in rowMatches)
                    {
                        TracesTableRow row = new TracesTableRow
                        {
                            FinYear = finYear,
                            Quarter = quarter
                        };

                        MatchCollection cellMatches = Regex.Matches(
                            rowMatch.Groups[1].Value,
                            @"<(th|td)[^>]*>(.*?)</\1>",
                            RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        foreach (Match cell in cellMatches)
                        {
                            string html = cell.Groups[2].Value.Trim();
                            string value;

                            Match colorMatch = Regex.Match(
                                html,
                                @"class=""colorbox\s+([a-zA-Z]+)""",
                                RegexOptions.IgnoreCase);

                            if (colorMatch.Success)
                            {
                                value = CultureInfo.InvariantCulture.TextInfo
                                    .ToTitleCase(colorMatch.Groups[1].Value.ToLower());
                            }
                            else
                            {
                                value = Regex.Replace(html, "<.*?>", "")
                                             .Replace("&nbsp;", "")
                                             .Trim();
                            }

                            row.Cells.Add(value);
                        }

                        if (row.Cells.Count > 0)
                            allRows.Add(row);
                    }
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region GetStatementStatus_OneLogin_New
        public static TracesResponse GetStatementStatus_OneLogin_New(
            out List<TracesTableRow> allRows)
        {
            allRows = new List<TracesTableRow>();
            TracesResponse resp = new TracesResponse();

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // ------------------------------------------------------------
                // STEP 1: Load dashboard shell
                // ------------------------------------------------------------
                string dashboardHtml = MakeHTTPGet_OneLogin_New(
                    "https://traces61.tdscpc.gov.in/app/ded/dashboard.xhtml");

                if (string.IsNullOrWhiteSpace(dashboardHtml))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Dashboard not loaded.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 2: Discover tabs from onclick=getYearQtrSet(...)
                // ------------------------------------------------------------
                MatchCollection tabMatches = Regex.Matches(
                    dashboardHtml,
                    @"getYearQtrSet\(\s*'(\d+)'\s*,\s*'(\d+)'\s*,\s*'\d+'\s*\)",
                    RegexOptions.IgnoreCase);

                if (tabMatches.Count == 0)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "No Statement Status tabs found.";
                    return resp;
                }

                // ------------------------------------------------------------
                // STEP 3: Iterate each tab
                // ------------------------------------------------------------
                foreach (Match tab in tabMatches)
                {
                    string quarter = tab.Groups[1].Value;
                    string finYear = tab.Groups[2].Value;

                    // Browser-like POST
                    StringBuilder postData = new StringBuilder();
                    postData.Append($"finYear={finYear}&quarter={quarter}");

                    string statusHtml = MakeHTTPPost_OneLogin(
                        "https://traces61.tdscpc.gov.in/app/ded/dashboarddefault.xhtml",
                        postData);

                    if (string.IsNullOrWhiteSpace(statusHtml))
                        continue;

                    // --------------------------------------------------------
                    // STEP 4: Extract Statement Status table
                    // --------------------------------------------------------
                    Match tableMatch = Regex.Match(
                        statusHtml,
                        @"<table[^>]*class=""userList[^""]*""[^>]*>(.*?)</table>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (!tableMatch.Success)
                        continue;

                    MatchCollection rowMatches = Regex.Matches(
                        tableMatch.Groups[1].Value,
                        @"<tr[^>]*>(.*?)</tr>",
                        RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    foreach (Match rowMatch in rowMatches)
                    {
                        TracesTableRow row = new TracesTableRow
                        {
                            FinYear = finYear,
                            Quarter = quarter
                        };

                        MatchCollection cellMatches = Regex.Matches(
                            rowMatch.Groups[1].Value,
                            @"<(th|td)[^>]*>(.*?)</\1>",
                            RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        foreach (Match cell in cellMatches)
                        {
                            string html = cell.Groups[2].Value.Trim();
                            string value;

                            Match colorMatch = Regex.Match(
                                html,
                                @"class=""colorbox\s+([a-zA-Z]+)""",
                                RegexOptions.IgnoreCase);

                            if (colorMatch.Success)
                            {
                                value = CultureInfo.InvariantCulture.TextInfo
                                    .ToTitleCase(colorMatch.Groups[1].Value.ToLower());
                            }
                            else
                            {
                                value = Regex.Replace(html, "<.*?>", "")
                                             .Replace("&nbsp;", "")
                                             .Trim();
                            }

                            row.Cells.Add(value);
                        }

                        if (row.Cells.Count > 0)
                            allRows.Add(row);
                    }
                }

                resp.Respons = enmResponse.Success;
                resp.Message = "OK";
                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region makeLogoff
        public static TracesResponse Logoff()
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //this.makeHTTPGetRequest(strBaseURL + "logout.xhtml");
                ////===================================================
                //this.bnlSessionExists = false;
                ResetOneLoginState();
                //
                ApplySharedCookies();

                objResponse.Respons = enmResponse.Success;

            }
            catch
            {
                objResponse.Respons = enmResponse.Failed;
            }

            return objResponse;
        }

        #endregion

        #region RequestForUnconsumedChallanList_OneLogin_New
        public TracesResponse RequestForUnconsumedChallanList_OneLogin_New(out DataTable table)
        {
            table = null;
            string json = "";
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/unconschallandetail.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // -------------------------------------------------------------
                // STEP 1: OPEN UNCONSUMED CHALLAN PAGE (SESSION VALIDATION)
                // -------------------------------------------------------------

                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(
                    "https://traces61.tdscpc.gov.in/app/ded/unconschallandetail.xhtml"
                );

                // jqGrid container validation
                //if (!IsStringExists(html, "<table id=\"viewunconchlntab\""))
                if (html == null || html.IndexOf("viewunconchlntab", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    objResponse.Message = "Session Timeout or Invalid TRACES Response";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                // -------------------------------------------------------------
                // STEP 2: FETCH jqGrid DATA
                // -------------------------------------------------------------

                StringBuilder objParam = new StringBuilder();
                objParam.Append("_search=false");
                objParam.Append("&rows=100");
                objParam.Append("&page=1");
                objParam.Append("&sidx=");
                objParam.Append("&sord=asc");

                json = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(
                    "https://traces61.tdscpc.gov.in/app/ded/srv/GetUnconChlnDetailServlet?reqtype=0",
                    objParam
                );

                if (string.IsNullOrEmpty(json))
                {
                    objResponse.Message = "No Data Returned";
                    return objResponse;
                }

                JObject root = JObject.Parse(json);
                JArray rows = (JArray)root["rows"];

                if (rows == null || rows.Count == 0)
                {
                    objResponse.Message = "No Unconsumed Challans Found";
                    return objResponse;
                }

                // -------------------------------------------------------------
                // STEP 3: BUILD DATATABLE (EXACT jqGrid MAPPING)
                // -------------------------------------------------------------

                table = new DataTable();

                table.Columns.Add("BSR Code");
                table.Columns.Add("Date of Deposit");
                table.Columns.Add("Challan Serial No");
                table.Columns.Add("Challan Amount");        // hidden in UI
                table.Columns.Add("Unconsumed Amount");     // View Amount text

                // hidden / technical (needed for Phase-2)
                table.Columns.Add("Receipt No");
                table.Columns.Add("hidChallanAmount");
                table.Columns.Add("hidUnconsumedAmount");

                foreach (JObject item in rows)
                {
                    DataRow dr = table.NewRow();

                    dr["BSR Code"] = item["chlnbsrcode"]?.ToString();
                    dr["Date of Deposit"] = item["dateofdep"]?.ToString();
                    dr["Challan Serial No"] = item["chlnsrno"]?.ToString();

                    // As per UI
                    dr["Challan Amount"] = "";                 // hidden
                    dr["Unconsumed Amount"] = "View Amount";   // clickable text

                    // Preserve server values
                    dr["Receipt No"] = item["recptNo"]?.ToString();
                    dr["hidChallanAmount"] = item["hidchlnamt"]?.ToString();
                    dr["hidUnconsumedAmount"] = item["hidunconschlnamt"]?.ToString();

                    table.Rows.Add(dr);
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                if (Regex.IsMatch(ex.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = ex.Message;
                return objResponse;
            }
        }
        #endregion

        #region IsSessionExists
        public bool IsSessionExists
        {
            get { return bnlSessionExists; }
            set { bnlSessionExists = true; }
        }

        #endregion


        #region IsStringExists
        private bool IsStringExists(string strServerResponse, string strXQuery)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            /*------------------------------------------
              SCRAPPING HTML RESPONSE FOR ANY SERVER ERROR
              ----------------------------------------- */
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strServerResponse);
            HtmlNode node = document.DocumentNode;
            //--------------------------------------------
            //HtmlNodeCollection error = node.SelectNodes("//span[@id=\"err_Summary\"]");

            HtmlNodeCollection error = node.SelectNodes(strXQuery);

            if (error == null)
                return false;
            else
                return true;

        }

        #endregion

        #region RequestForCommunicationInboxAction_OneLogin_New
        public TracesResponse RequestForCommunicationInboxAction_OneLogin_New(out DataTable table)
        {
            table = null;
            string json = "";
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dedinbox.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // -------------------------------------------------------------
                // STEP 1: GET INBOX PAGE (must be logged in)
                // -------------------------------------------------------------

                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(
                    strBaseURL + "ded/dedinbox.xhtml"
                );

                // Validate page
                if (!IsStringExists(html, "//form[@id=\"CommunicationInboxForm\"]"))
                {
                    objResponse.Message = "Session Timeout or Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                // -------------------------------------------------------------
                // STEP 2: POST REQUEST TO FETCH JSON GRID
                // -------------------------------------------------------------

                StringBuilder objParam = new StringBuilder();
                objParam.Append("_search=false");
                objParam.Append("&rows=100");
                objParam.Append("&page=1");
                objParam.Append("&sidx=");
                objParam.Append("&sord=asc");
                objParam.Append("&commCatogory=1");

                json = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(
                    strBaseURL + "ded/srv/CommInboxServlet?reqtype=0&commClassify=1",
                    objParam
                );

                // -------------------------------------------------------------
                // STEP 3: PARSE JSON
                // -------------------------------------------------------------

                if (string.IsNullOrEmpty(json))
                {
                    objResponse.Message = "No Data Returned";
                    objResponse.Respons = enmResponse.Success;
                    return objResponse;
                }

                JObject root = JObject.Parse(json);
                JArray rows = (JArray)root["rows"];

                // -------------------------------------------------------------
                // STEP 4: BUILD DATATABLE
                // -------------------------------------------------------------

                table = new DataTable();
                table.Columns.Add("Reference No");
                table.Columns.Add("Date");
                table.Columns.Add("Category");
                table.Columns.Add("Description");
                table.Columns.Add("Financial Year");
                table.Columns.Add("Quarter");
                table.Columns.Add("Form Type");

                // Hidden columns
                table.Columns.Add("commId");
                table.Columns.Add("hidfinYr");
                table.Columns.Add("hidquat");
                table.Columns.Add("declId");
                table.Columns.Add("certNum");
                table.Columns.Add("commInbId");
                table.Columns.Add("comcatid");

                foreach (JObject item in rows)
                {
                    DataRow dr = table.NewRow();

                    dr["Reference No"] = item["commRefNo"]?.ToString();
                    dr["Date"] = item["date"]?.ToString();
                    dr["Category"] = item["commcat"]?.ToString();
                    dr["Description"] = item["description"]?.ToString();
                    dr["Financial Year"] = item["finYear"]?.ToString();
                    dr["Quarter"] = item["quarter"]?.ToString();
                    dr["Form Type"] = item["formType"]?.ToString();

                    // Hidden fields
                    dr["commId"] = item["commInbId"]?.ToString();
                    dr["hidfinYr"] = item["hidFY"]?.ToString();
                    dr["hidquat"] = item["hidQT"]?.ToString();
                    dr["declId"] = item["commMstrId"]?.ToString();
                    dr["certNum"] = item["certNum"]?.ToString();
                    dr["commInbId"] = item["commInbId"]?.ToString();
                    dr["comcatid"] = item["comcatid"]?.ToString();

                    table.Rows.Add(dr);
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                if (Regex.IsMatch(ex.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = ex.Message;
                return objResponse;
            }
        }
        #endregion

        #region RequestForCommunicationInboxNoAction_OneLogin_New
        public TracesResponse RequestForCommunicationInboxNoAction_OneLogin_New(out DataTable table)
        {
            table = null;
            string json = "";
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            try
            {
                // ================================
                // STEP 1: INIT OLD DOMAIN COOKIE
                // ================================
                //_client.makeHTTPGetRequest("https://traces61.tdscpc.gov.in/",
                //                   "https://traces.tdscpc.gov.in/");

                // ================================
                // STEP 2: SESSION BRIDGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml",
                    "https://traces.tdscpc.gov.in/"
                );

                // ================================
                // STEP 3: OPEN PAGE
                // ================================
                _client.makeHTTPGetRequest(
                    "https://traces61.tdscpc.gov.in/app/ded/dedinbox.xhtml",
                    "https://traces61.tdscpc.gov.in/app/preauthV2.xhtml"
                );
                // -------------------------------------------------------------
                // STEP 1: GET INBOX PAGE (must be logged in)
                // -------------------------------------------------------------

                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(
                    strBaseURL + "ded/dedinbox.xhtml"
                );

                // Validate page
                if (!IsStringExists(html, "//form[@id=\"CommunicationInboxForm\"]"))
                {
                    objResponse.Message = "Session Timeout or Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                // -------------------------------------------------------------
                // STEP 2: POST REQUEST TO FETCH JSON GRID
                // -------------------------------------------------------------

                StringBuilder objParam = new StringBuilder();
                objParam.Append("_search=false");
                objParam.Append("&rows=100");
                objParam.Append("&page=1");
                objParam.Append("&sidx=");
                objParam.Append("&sord=asc");
                objParam.Append("&commCatogory=1");

                json = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(
                    //strBaseURL + "ded/srv/CommInboxServlet?reqtype=0&commClassify=1",
                    strBaseURL + "ded/srv/CommInboxServlet?reqtype=0&commClassify=0",
                    objParam
                );

                // -------------------------------------------------------------
                // STEP 3: PARSE JSON
                // -------------------------------------------------------------

                if (string.IsNullOrEmpty(json))
                {
                    objResponse.Message = "No Data Returned";
                    objResponse.Respons = enmResponse.Success;
                    return objResponse;
                }

                JObject root = JObject.Parse(json);
                JArray rows = (JArray)root["rows"];

                // -------------------------------------------------------------
                // STEP 4: BUILD DATATABLE
                // -------------------------------------------------------------

                table = new DataTable();
                table.Columns.Add("Reference No");
                table.Columns.Add("Date");
                table.Columns.Add("Category");
                table.Columns.Add("Description");
                table.Columns.Add("Financial Year");
                table.Columns.Add("Quarter");
                table.Columns.Add("Form Type");

                // Hidden columns
                table.Columns.Add("commId");
                table.Columns.Add("hidfinYr");
                table.Columns.Add("hidquat");
                table.Columns.Add("declId");
                table.Columns.Add("certNum");
                table.Columns.Add("commInbId");
                table.Columns.Add("comcatid");

                foreach (JObject item in rows)
                {
                    DataRow dr = table.NewRow();

                    dr["Reference No"] = item["commRefNo"]?.ToString();
                    dr["Date"] = item["date"]?.ToString();
                    dr["Category"] = item["commcat"]?.ToString();
                    dr["Description"] = item["description"]?.ToString();
                    dr["Financial Year"] = item["finYear"]?.ToString();
                    dr["Quarter"] = item["quarter"]?.ToString();
                    dr["Form Type"] = item["formType"]?.ToString();

                    // Hidden fields
                    dr["commId"] = item["commInbId"]?.ToString();
                    dr["hidfinYr"] = item["hidFY"]?.ToString();
                    dr["hidquat"] = item["hidQT"]?.ToString();
                    dr["declId"] = item["commMstrId"]?.ToString();
                    dr["certNum"] = item["certNum"]?.ToString();
                    dr["commInbId"] = item["commInbId"]?.ToString();
                    dr["comcatid"] = item["comcatid"]?.ToString();

                    table.Rows.Add(dr);
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                if (Regex.IsMatch(ex.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = ex.Message;
                return objResponse;
            }
        }
        #endregion

        #region RequestCommunicationInboxDetails_OneLogin_New
        public TracesResponse RequestCommunicationInboxDetails_OneLogin_New(
                                    string commId,
                                    string description,
                                    out string jsonResponse)
        {
            jsonResponse = "";
            TracesResponse resp = new TracesResponse();
            resp.Respons = enmResponse.Success;

            try
            {
                //-------------------------------------------------------------------
                // STEP 1: GET THE DETAIL PAGE (MUST fetch ViewState)
                //-------------------------------------------------------------------

                string urlDetailPage = strBaseURL +
                    "ded/dedinboxdetails.xhtml?commId=" + commId;

                string detailHtml = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(urlDetailPage);

                if (string.IsNullOrWhiteSpace(detailHtml))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Empty TRACES response";
                    return resp;
                }

                // Extract javax.faces.ViewState
                string viewState = ExtractViewState(detailHtml);

                if (string.IsNullOrEmpty(viewState))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to extract ViewState";
                    return resp;
                }

                //-------------------------------------------------------------------
                // STEP 2: PREPARE POST DATA FOR reqtype=2 (DETAIL REQUEST)
                //-------------------------------------------------------------------

                StringBuilder postData = new StringBuilder();
                postData.Append("_search=false");
                postData.Append("&rows=50");
                postData.Append("&page=1");
                postData.Append("&sidx=");
                postData.Append("&sord=asc");

                // Must send description encoded EXACTLY like browser
                string encodedDescr = System.Web.HttpUtility.UrlEncode(description);

                string reqUrl = strBaseURL +
                    "ded/srv/CommInboxServlet?reqtype=2&commId=" + commId +
                    "&description=" + encodedDescr;

                // Important: send ViewState same way JSF sends it
                postData.Append("&javax.faces.ViewState=" + System.Web.HttpUtility.UrlEncode(viewState));

                //-------------------------------------------------------------------
                // STEP 3: SEND POST REQUEST
                //-------------------------------------------------------------------

                jsonResponse = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(
                    reqUrl,
                    postData
                );

                //-------------------------------------------------------------------
                // STEP 4: VALIDATE RESPONSE
                //-------------------------------------------------------------------

                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "No data returned";
                }
                else if (jsonResponse.TrimStart().StartsWith("<"))
                {
                    // Means TRACES returned HTML (login page)
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Session Expired - Received HTML instead of JSON";
                }

                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region DownloadCertificate_OneLogin_New
        public TracesResponse DownloadCertificate_OneLogin_New(
                                        string commId,
                                        string finYear,
                                        string formType,
                                        string quarter,
                                        string certNum,
                                        string description,
                                        out byte[] pdfBytes)
        {
            pdfBytes = null;
            TracesResponse resp = new TracesResponse { Respons = enmResponse.Success };

            try
            {
                //-------------------------------------------------------------------
                // STEP 1: OPEN THE DETAILS PAGE TO FETCH VIEWSTATE
                //-------------------------------------------------------------------

                string urlDetail = strBaseURL +
                    "ded/dedinboxdetails.xhtml?commId=" + System.Web.HttpUtility.UrlEncode(commId);

                string html = TracesConnect_OneLogin.MakeHTTPGet_OneLogin_New(urlDetail);

                if (string.IsNullOrWhiteSpace(html))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Session expired while loading detail page.";
                    return resp;
                }

                string viewState = ExtractViewState(html);

                if (string.IsNullOrEmpty(viewState))
                {
                    resp.Respons = enmResponse.SessionTimeout;
                    resp.Message = "Unable to extract ViewState.";
                    return resp;
                }

                //-------------------------------------------------------------------
                // STEP 2: PREPARE reqtype=5 (CERTIFICATE DOWNLOAD)
                //-------------------------------------------------------------------

                StringBuilder post = new StringBuilder();
                post.Append("javax.faces.ViewState=" + System.Web.HttpUtility.UrlEncode(viewState));

                // TRACES URL for certificate download
                string url = strBaseURL +
                    "ded/srv/CommInboxServlet" +
                    "?reqtype=5" +
                    "&commId=" + System.Web.HttpUtility.UrlEncode(commId) +
                    "&finYear=" + System.Web.HttpUtility.UrlEncode(finYear) +
                    "&formType=" + System.Web.HttpUtility.UrlEncode(formType) +
                    "&certNum=" + System.Web.HttpUtility.UrlEncode(certNum) +
                    "&description=" + System.Web.HttpUtility.UrlEncode(description);

                //-------------------------------------------------------------------
                // STEP 3: POST & GET PDF BYTES
                //-------------------------------------------------------------------

                byte[] rawData = TracesConnect_OneLogin.MakeHTTPPostBytes_OneLogin(url, post.ToString());


                if (rawData == null || rawData.Length < 10)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "No PDF returned from TRACES.";
                    return resp;
                }

                // Validate PDF header (%PDF)
                if (rawData[0] == 0x25 && rawData[1] == 0x50 &&
                    rawData[2] == 0x44 && rawData[3] == 0x46)
                {
                    pdfBytes = rawData;
                    return resp;
                }
                else
                {
                    string header = Encoding.ASCII.GetString(rawData, 0, Math.Min(100, rawData.Length));
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Invalid PDF returned.\nHeader: " + header;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

        #region RequestForDownloadIntimation_OneLogin_New
        public TracesResponse RequestForDownloadIntimation_OneLogin_New(
        string commInbId,
        string finYear,
        string quarter,
        string formType,
        string commRefNo,
        string commCatgry)
        {
            TracesResponse resp = new TracesResponse { Respons = enmResponse.Success };

            try
            {
                // -------------------------------
                // STEP 1: Build TRACES request
                // -------------------------------
                string url = strBaseURL +
                             "ded/srv/CommInboxServlet?reqtype=3";

                StringBuilder post = new StringBuilder();
                post.Append("commInbId=" + System.Web.HttpUtility.UrlEncode(commInbId));
                post.Append("&finYear=" + System.Web.HttpUtility.UrlEncode(finYear));
                post.Append("&quarter=" + System.Web.HttpUtility.UrlEncode(quarter));
                post.Append("&formType=" + System.Web.HttpUtility.UrlEncode(formType));
                post.Append("&commRefNo=" + System.Web.HttpUtility.UrlEncode(commRefNo));
                post.Append("&commCatgry=" + System.Web.HttpUtility.UrlEncode(commCatgry));

                // -------------------------------
                // STEP 2: POST using OneLogin
                // -------------------------------
                string result = TracesConnect_OneLogin.MakeHTTPPost_OneLogin(url, post);

                if (string.IsNullOrWhiteSpace(result))
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Empty server response";
                    return resp;
                }

                // -------------------------------
                // STEP 3: Parse TRACES Response
                // -------------------------------
                JArray arr = JArray.Parse(result);

                if (arr.Count == 0)
                {
                    resp.Respons = enmResponse.Failed;
                    resp.Message = "Invalid server response";
                    return resp;
                }

                JObject obj = (JObject)arr[0];

                // NEW REQUEST CREATED
                if (obj["reqid"] != null)
                {
                    resp.CustomTypes = obj["reqid"].ToString();
                    resp.Message = "Request submitted successfully.\nRequest No: " + resp.CustomTypes;
                    resp.Respons = enmResponse.Success;
                    return resp;
                }

                // REQUEST ALREADY EXISTS
                if (obj["hidmsgflg"] != null)
                {
                    resp.Respons = enmResponse.Success;
                    resp.Message =
                        "Request for Download of Intimations has already been submitted.\n" +
                        "A new request can only be submitted once the previous request is processed.";
                    return resp;
                }

                // UNEXPECTED RESPONSE
                resp.Respons = enmResponse.Failed;
                resp.Message = "Unexpected server response.";

                return resp;
            }
            catch (Exception ex)
            {
                resp.Respons = enmResponse.Failed;
                resp.Message = ex.Message;
                return resp;
            }
        }
        #endregion

    }
}
