using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace TDSMAN.Classes
{
    //class TracesConnect_IT2025
    public partial class TracesConnect
    {
        #region DECLARATIONS
        //private CookieContainer objContainer = new CookieContainer();

        //private HttpWebRequest request;
        //private HttpWebResponse response;
        //private Stream dataStream;
        //private StreamReader reader;

        //private string strServerResponse = "";
        private const string BASE_URL = "https://traces-app.tdscpc.gov.in";

        private const string CONSO_SERVICE = BASE_URL + "/tanconsolidatedrepservice";

        private const string VERIFY_SERVICE = BASE_URL + "/everificationservice";
        //private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private const string CERTIFICATE_SERVICE = BASE_URL + "/tdscertificatesservice";

        public class ConsoAadhaarInfo
        {
            public string AuthorizedPersonName { get; set; }

            public string AuthorizedPersonPAN { get; set; }

            public string MaskedAadhaar { get; set; }

            public string Message { get; set; }

            public string TransactionId { get; set; }
        }

        #endregion

        #region makeHTTPGetRequest_IT2025
        private string makeHTTPGetRequest_IT2025(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.KeepAlive = true;
            request.CookieContainer = objContainer;

            request.Accept = "application/json";
            request.ContentType = "application/json";

            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/138.0 Safari/537.36";

            request.Timeout = 60000;

            response = (HttpWebResponse)request.GetResponse();

            objContainer.Add(response.Cookies);

            using (StreamReader sr =
                new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        #endregion

        #region makeHTTPGetRequest_ITOTP

        private string makeHTTPGetRequest_ITOTP(
            string url)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;

            HttpWebRequest req =
                (HttpWebRequest)WebRequest.Create(
                    url);

            req.Method = "GET";
            req.KeepAlive = true;

            req.CookieContainer =
                TracesSession.CookieJar;

            req.Accept =
                "application/json";

            req.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
                + "AppleWebKit/537.36 Chrome/138.0 Safari/537.36";

            req.Headers.Add(
                "Authorization",
                "Bearer " + tracesAuthToken);

            req.Headers.Add(
                "Origin",
                "https://traces.tdscpc.gov.in");

            req.Referer =
                "https://traces.tdscpc.gov.in/";

            req.Timeout = 60000;

            using (HttpWebResponse resp =
                (HttpWebResponse)req.GetResponse())
            {
                if (resp.Cookies != null)
                {
                    TracesSession.CookieJar.Add(
                        resp.Cookies);
                }

                using (StreamReader sr =
                    new StreamReader(
                        resp.GetResponseStream(),
                        Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        #endregion

        #region makeHTTPPostJSONRequest_IT2025

        private string makeHTTPPostJSONRequest_IT2025(string url, string json)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.KeepAlive = true;

            //---------------------------------------------------------
            // IMPORTANT:
            // Use the SAME cookie container used by the new login.
            //---------------------------------------------------------
            request.CookieContainer = TracesSession.CookieJar;

            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=utf-8";

            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/138.0 Safari/537.36";

            //---------------------------------------------------------
            // IMPORTANT:
            // New TRACES APIs require Bearer authentication.
            //
            // tracesAuthToken was populated during:
            // makeLoginToTraces_New()
            //---------------------------------------------------------
            if (string.IsNullOrEmpty(tracesAuthToken))
            {
                throw new Exception(
                    "TRACES authentication token is not available. Please login again.");
            }

            request.Headers[HttpRequestHeader.Authorization] =
                "Bearer " + tracesAuthToken;

            //---------------------------------------------------------
            // Match browser request headers
            //---------------------------------------------------------
            request.Headers.Add(
                "Origin",
                "https://traces.tdscpc.gov.in");

            request.Referer =
                "https://traces.tdscpc.gov.in/";

            request.Headers.Add(
                "Accept-Language",
                "en-US,en;q=0.9");

            request.Timeout = 60000;

            //---------------------------------------------------------
            // JSON PAYLOAD
            //---------------------------------------------------------
            byte[] buffer =
                Encoding.UTF8.GetBytes(json);

            request.ContentLength = buffer.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(
                    buffer,
                    0,
                    buffer.Length);
            }

            //---------------------------------------------------------
            // GET RESPONSE
            //---------------------------------------------------------
            response =
                (HttpWebResponse)request.GetResponse();

            //---------------------------------------------------------
            // Preserve any cookies returned by TRACES
            //---------------------------------------------------------
            if (response.Cookies != null &&
                response.Cookies.Count > 0)
            {
                TracesSession.CookieJar.Add(
                    response.Cookies);
            }

            //---------------------------------------------------------
            // READ JSON RESPONSE
            //---------------------------------------------------------
            using (StreamReader sr =
                new StreamReader(
                    response.GetResponseStream(),
                    Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        #endregion

        #region RequestForNSDLConsoFile_NewITAct
        public TracesResponse RequestForNSDLConsoFile_NewITAct(TracesLogin objLogin, TracesData objTraceData)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTraces_New(objLogin);

                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }

                // New IT Act APIs will be called here
            }
            catch (Exception err)
            {
                this.bnlSessionExists = false;
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }

            return objResponse;
        }
        #endregion

        #region GetFinancialYears
        private string GetFinancialYears()
        {
            string url = CONSO_SERVICE +
                         "/api/conso/getFinYear?itActFlag=IT_ACT_2025";

            return makeHTTPGetRequest(url);
        }

        #endregion

        #region GetFormTypes
        private string GetFormTypes(string financialYear)
        {
            string url = CONSO_SERVICE +
                         "/api/conso/getFormType?financialYear=" +
                         financialYear +
                         "&itActFlag=IT_ACT_2025";

            return makeHTTPGetRequest(url);
        }

        #endregion

        #region GetQuarter
        private string GetQuarter(string financialYear)
        {
            string url = CONSO_SERVICE +
                         "/api/conso/getQuarter?financialYear=" +
                         financialYear +
                         "&itActFlag=IT_ACT_2025";

            return makeHTTPGetRequest(url);
        }

        #endregion

        #region SubmitRequest
        //private string SubmitRequest(string userId,
        //                     string tan,
        //                     string financialYear,
        //                     string quarter,
        //                     string formType)
        //{
            //string url = CONSO_SERVICE + "/tds/conso/request";

            //string json =
            //    "{"
            //    + "\"userId\":\"" + userId + "\","
            //    + "\"tan\":\"" + tan + "\","
            //    + "\"financialYear\":\"" + financialYear + "\","
            //    + "\"quarter\":\"" + quarter + "\","
            //    + "\"formType\":\"" + formType + "\""
            //    + "}";

            //return makeHTTPPostJSONRequest(url, json);
        //}

        #endregion

        #region GetRequestStatus
        private string GetRequestStatus(string userId)
        {
            string url =
                CONSO_SERVICE +
                "/tds/conso/new-requests?userId=" +
                userId +
                "&fetchAll=false&page=0&size=10";

            return makeHTTPGetRequest(url);
        }

        #endregion

        #region GetDownloadStatus
        private string GetDownloadStatus(string requestId)
        {
            string url =
                CONSO_SERVICE +
                "/tds/conso/downloadStatus?requestId=" +
                requestId;

            return makeHTTPGetRequest(url);
        }

        #endregion


        #region InitiateCertificateRequest
        private string InitiateCertificateRequest(
            string userId,
            string tan,
            string financialYear,
            string quarter,
            string formType,
            int downloadType)
        {
            string url = CERTIFICATE_SERVICE + "/initiate";

            string json =
                "{"
                + "\"userId\":\"" + userId + "\","
                + "\"tan\":\"" + tan + "\","
                + "\"financialYear\":\"" + financialYear + "\","
                + "\"quarter\":\"" + quarter + "\","
                + "\"formType\":\"" + formType + "\","
                + "\"downloadType\":" + downloadType
                + "}";

            return makeHTTPPostJSONRequest_IT2025(url, json);
        }
        #endregion

        #region GetCertificateDownloadStatus
        private string GetCertificateDownloadStatus(string requestId)
        {
            string url =
                CERTIFICATE_SERVICE +
                "/getDownloadStatus?requestId=" +
                HttpUtility.UrlEncode(requestId);

            return makeHTTPGetRequest_IT2025(url);
        }
        #endregion

        #region GetActiveCertificateRequests
        private string GetActiveCertificateRequests(string userId)
        {
            string url =
                CERTIFICATE_SERVICE +
                "/getActiveRequests?userId=" +
                HttpUtility.UrlEncode(userId) +
                "&fetchAll=false&page=0&size=10";

            return makeHTTPGetRequest_IT2025(url);
        }
        #endregion

        #region RequestForTDSCertificate_NewITAct - COMMENTED

        //public TracesResponse RequestForTDSCertificateForm16A_131(
        //    TracesLogin objLogin,
        //    TracesData objTraceData)
        //{
        //    TracesResponse objResponse = new TracesResponse();

        //    try
        //    {
        //        //---------------------------------------------------------
        //        // STEP 1 : LOGIN TO TRACES IF SESSION DOES NOT EXIST
        //        //---------------------------------------------------------
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTraces_New(objLogin);

        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }

        //        //---------------------------------------------------------
        //        // STEP 2 : VALIDATE LOGIN / TAN
        //        //---------------------------------------------------------
        //        if (objLogin == null)
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "TRACES login information is not available.";
        //            return objResponse;
        //        }

        //        if (string.IsNullOrEmpty(objLogin.UserID))
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "TRACES User ID is not available.";
        //            return objResponse;
        //        }

        //        if (string.IsNullOrEmpty(objLogin.TAN))
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "TAN is not available.";
        //            return objResponse;
        //        }

        //        //---------------------------------------------------------
        //        // STEP 3 :
        //        // THESE VALUES WILL TEMPORARILY BE USED FOR TESTING.
        //        //
        //        // Later we will take Financial Year / Quarter / Form
        //        // dynamically from objTraceData.
        //        //---------------------------------------------------------

        //        string strUserID = objLogin.UserID.Trim();
        //        string strTAN = objLogin.TAN.Trim();

        //        string strFinancialYear = "2026-27";
        //        string strQuarter = "Q1";

        //        // IMPORTANT:
        //        // Certificate Form Code - not Conso Form Code.
        //        //
        //        // Current captured API request:
        //        // Form 131
        //        //---------------------------------------------------------
        //        string strFormType = "131";

        //        // Captured from TRACES API request.
        //        int intDownloadType = 14;

        //        //---------------------------------------------------------
        //        // STEP 4 : INITIATE CERTIFICATE REQUEST
        //        //---------------------------------------------------------
        //        string strInitiateResponse =
        //            InitiateCertificateRequest(
        //                strUserID,
        //                strTAN,
        //                strFinancialYear,
        //                strQuarter,
        //                strFormType,
        //                intDownloadType);

        //        if (string.IsNullOrEmpty(strInitiateResponse))
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message =
        //                "No response received from TRACES while initiating certificate request.";

        //            return objResponse;
        //        }

        //        //---------------------------------------------------------
        //        // STEP 5 :
        //        // FOR THIS PHASE WE ONLY CONFIRM THAT TRACES ACCEPTED
        //        // AND RETURNED A RESPONSE.
        //        //
        //        // Next phase:
        //        // Parse requestId from JSON response.
        //        //---------------------------------------------------------

        //        objResponse.Respons = enmResponse.Success;

        //        objResponse.Message =
        //            "TDS/TCS Certificate request submitted successfully."
        //            + Environment.NewLine
        //            + Environment.NewLine
        //            + "TRACES Response:"
        //            + Environment.NewLine
        //            + strInitiateResponse;

        //        return objResponse;
        //    }
        //    catch (WebException webEx)
        //    {
        //        this.bnlSessionExists = false;

        //        string strError = webEx.Message;

        //        try
        //        {
        //            if (webEx.Response != null)
        //            {
        //                using (StreamReader sr =
        //                    new StreamReader(webEx.Response.GetResponseStream()))
        //                {
        //                    string strServerError = sr.ReadToEnd();

        //                    if (!string.IsNullOrEmpty(strServerError))
        //                        strError +=
        //                            Environment.NewLine +
        //                            "TRACES Response: " +
        //                            strServerError;
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }

        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = strError;

        //        return objResponse;
        //    }
        //    catch (Exception err)
        //    {
        //        this.bnlSessionExists = false;

        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;

        //        return objResponse;
        //    }
        //}
        #endregion

        #region RequestForTDSCertificatesIT2025

        public TracesResponse RequestForTDSCertificatesIT2025(
            TracesLogin objLogin,
            TracesData objTraceData,
            string FormNo)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //---------------------------------------------------------
                // STEP 1 : LOGIN TO TRACES IF SESSION DOES NOT EXIST
                //---------------------------------------------------------
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTraces_New(objLogin);

                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }

                //---------------------------------------------------------
                // STEP 2 : VALIDATE LOGIN / TAN
                //---------------------------------------------------------
                if (objLogin == null)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TRACES login information is not available.";

                    return objResponse;
                }

                if (string.IsNullOrEmpty(objLogin.UserID))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TRACES User ID is not available.";

                    return objResponse;
                }

                if (string.IsNullOrEmpty(objLogin.TAN))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TAN is not available.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 3 : PREPARE REQUEST VALUES
                //
                // Currently hard-coded for testing.
                // Later Financial Year / Quarter can be taken
                // dynamically from objTraceData.
                //---------------------------------------------------------

                string strUserID = objLogin.UserID.Trim();
                string strTAN = objLogin.TAN.Trim();

                string strFinancialYear = objTraceData.FAYear;// "2026-27";
                string strQuarter = objTraceData.Quarter;// "Q1";

                //---------------------------------------------------------
                // Certificate Form Code
                //
                // Form 131 = Form 16A Certificate
                //---------------------------------------------------------
                //string strFormType = "131";
                string strFormType = FormNo;

                //---------------------------------------------------------
                // Download Type captured from TRACES API
                //---------------------------------------------------------
                int intDownloadType = 14;

                //---------------------------------------------------------
                // STEP 4 : INITIATE CERTIFICATE REQUEST
                //---------------------------------------------------------
                string strInitiateResponse =
                    InitiateCertificateRequest(
                        strUserID,
                        strTAN,
                        strFinancialYear,
                        strQuarter,
                        strFormType,
                        intDownloadType);

                //---------------------------------------------------------
                // STEP 5 : CHECK RESPONSE
                //---------------------------------------------------------
                if (string.IsNullOrEmpty(strInitiateResponse))
                {
                    objResponse.Respons = enmResponse.Failed;

                    objResponse.Message =
                        "No response received from TRACES while "
                        + "initiating certificate request.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 6 : PARSE JSON RESPONSE
                //
                // Example:
                //
                // {
                //   "certDownloadStatus":"P",
                //   "requestId":250016710,
                //   "transactionId":null,
                //   "status":"1",
                //   "fileError":null,
                //   "invalidRequests":null,
                //   "validRequests":[]
                // }
                //---------------------------------------------------------

                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strInitiateResponse);

                //---------------------------------------------------------
                // STEP 7 : VALIDATE REQUEST ID
                //---------------------------------------------------------
                if (objJSON == null ||
                    objJSON.requestId == null)
                {
                    objResponse.Respons = enmResponse.Failed;

                    objResponse.Message =
                        "Certificate request was submitted but "
                        + "Request ID was not returned by TRACES."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "TRACES Response:"
                        + Environment.NewLine
                        + strInitiateResponse;

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 8 : EXTRACT REQUEST ID
                //---------------------------------------------------------
                string strRequestId =
                    Convert.ToString(objJSON.requestId);

                if (string.IsNullOrEmpty(strRequestId))
                {
                    objResponse.Respons = enmResponse.Failed;

                    objResponse.Message =
                        "Certificate request was submitted but "
                        + "Request ID returned by TRACES is blank."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "TRACES Response:"
                        + Environment.NewLine
                        + strInitiateResponse;

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 9 : SUCCESS
                //
                // IMPORTANT:
                // Store Request ID in Data.
                //
                // Calling form will retrieve it using:
                //
                // strRequestNo =
                //     Convert.ToString(response.Data);
                //---------------------------------------------------------

                objResponse.Respons = enmResponse.Success;

                objResponse.Data = strRequestId;

                objResponse.Message =
                    "TDS/TCS Certificate request submitted successfully."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Request ID: "
                    + strRequestId
                    + Environment.NewLine
                    + Environment.NewLine
                    + "TRACES Response:"
                    + Environment.NewLine
                    + strInitiateResponse;

                return objResponse;
            }

            //-------------------------------------------------------------
            // WEB EXCEPTION
            //-------------------------------------------------------------
            catch (WebException webEx)
            {
                this.bnlSessionExists = false;

                string strError = webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream()))
                        {
                            string strServerError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrEmpty(strServerError))
                            {
                                strError +=
                                    Environment.NewLine
                                    + "TRACES Response: "
                                    + strServerError;
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = strError;

                return objResponse;
            }

            //-------------------------------------------------------------
            // GENERAL EXCEPTION
            //-------------------------------------------------------------
            catch (Exception err)
            {
                this.bnlSessionExists = false;

                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;

                return objResponse;
            }
        }

        #endregion

        #region DownloadCertificateFile_IT2025

        private string DownloadCertificateFile_IT2025(
            long requestId,
            string downloadFolder,
            string FormNo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url =
                CERTIFICATE_SERVICE +
                "/downloadFile?requestId=" +
                requestId.ToString();

            HttpWebRequest downloadRequest =
                (HttpWebRequest)WebRequest.Create(url);

            downloadRequest.Method = "GET";
            downloadRequest.KeepAlive = true;

            // Same authenticated session
            downloadRequest.CookieContainer =
                TracesSession.CookieJar;

            downloadRequest.Accept =
                "application/json, text/plain, */*";

            downloadRequest.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/138.0 Safari/537.36";

            //---------------------------------------------------------
            // BEARER TOKEN
            //---------------------------------------------------------
            if (string.IsNullOrEmpty(tracesAuthToken))
            {
                throw new Exception(
                    "TRACES authentication token is not available. Please login again.");
            }

            downloadRequest.Headers[HttpRequestHeader.Authorization] =
                "Bearer " + tracesAuthToken;

            //---------------------------------------------------------
            // SAME HEADERS AS BROWSER
            //---------------------------------------------------------
            downloadRequest.Headers.Add(
                "Origin",
                "https://traces.tdscpc.gov.in");

            downloadRequest.Referer =
                "https://traces.tdscpc.gov.in/";

            downloadRequest.Headers.Add(
                "Accept-Language",
                "en-US,en;q=0.9");

            downloadRequest.Timeout = 60000;

            //---------------------------------------------------------
            // DOWNLOAD
            //---------------------------------------------------------
            using (HttpWebResponse downloadResponse =
                (HttpWebResponse)downloadRequest.GetResponse())
            {
                //-----------------------------------------------------
                // GET FILE NAME FROM Content-Disposition
                //-----------------------------------------------------
                string contentDisposition =
                    downloadResponse.Headers["Content-Disposition"];

                string fileName = "";

                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    int fileNameIndex =
                        contentDisposition.IndexOf(
                            "filename=",
                            StringComparison.OrdinalIgnoreCase);

                    if (fileNameIndex >= 0)
                    {
                        fileName =
                            contentDisposition.Substring(
                                fileNameIndex + 9).Trim();

                        fileName =
                            fileName.Trim('"');
                    }
                }

                //-----------------------------------------------------
                // FALLBACK FILE NAME
                //-----------------------------------------------------
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName =
                        "Form" + FormNo + "_" +
                        requestId.ToString() +
                        ".zip";
                }

                //-----------------------------------------------------
                // MAKE SURE DIRECTORY EXISTS
                //-----------------------------------------------------
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }

                string fullPath =
                    Path.Combine(
                        downloadFolder,
                        fileName);

                //-----------------------------------------------------
                // SAVE BINARY RESPONSE DIRECTLY
                //-----------------------------------------------------
                using (Stream responseStream =
                    downloadResponse.GetResponseStream())
                using (FileStream fileStream =
                    new FileStream(
                        fullPath,
                        FileMode.Create,
                        FileAccess.Write))
                {
                    responseStream.CopyTo(fileStream);
                }

                //-----------------------------------------------------
                // PRESERVE COOKIES IF ANY
                //-----------------------------------------------------
                if (downloadResponse.Cookies != null &&
                    downloadResponse.Cookies.Count > 0)
                {
                    TracesSession.CookieJar.Add(
                        downloadResponse.Cookies);
                }

                return fullPath;
            }
        }

        #endregion

        #region DownloadTDSCertificatesIT2025

        public TracesResponse DownloadTDSCertificatesIT2025(
            long requestId,
            string downloadFolder,
            string FormNo)
        {
            TracesResponse objResponse =
                new TracesResponse();

            try
            {
                if (requestId <= 0)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Invalid TRACES Request ID.";

                    return objResponse;
                }

                if (string.IsNullOrEmpty(downloadFolder))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Download folder is not available.";

                    return objResponse;
                }

                string downloadedFile =
                    DownloadCertificateFile_IT2025(
                        requestId,
                        downloadFolder,
                        FormNo);

                if (!File.Exists(downloadedFile))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Certificate file could not be downloaded.";

                    return objResponse;
                }

                objResponse.Respons =
                    enmResponse.Success;

                objResponse.Message =
                    downloadedFile;

                return objResponse;
            }
            catch (WebException webEx)
            {
                string strError =
                    webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream()))
                        {
                            string serverError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrEmpty(serverError))
                            {
                                strError +=
                                    Environment.NewLine +
                                    serverError;
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    strError;

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region RequestForNSDLConsoFileIT2025

        public TracesResponse RequestForNSDLConsoFileIT2025(
            TracesLogin objLogin,
            TracesData objTraceData)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //---------------------------------------------------------
                // STEP 1 : VALIDATE INPUT
                //---------------------------------------------------------
                if (objLogin == null)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TRACES login information is not available.";

                    return objResponse;
                }

                if (objTraceData == null)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Statement information is not available.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 2 : LOGIN TO TRACES IF SESSION DOES NOT EXIST
                //---------------------------------------------------------
                if (!IsSessionExists)
                {
                    objResponse =
                        this.makeLoginToTraces_New(objLogin);

                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }

                //---------------------------------------------------------
                // STEP 3 : VALIDATE LOGIN DETAILS
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(objLogin.UserID))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TRACES User ID is not available.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(objLogin.TAN))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "TAN is not available.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 4 : PREPARE REQUEST VALUES
                //---------------------------------------------------------
                string strUserID =
                    objLogin.UserID.Trim();

                string strTAN =
                    objLogin.TAN.Trim();

                //---------------------------------------------------------
                // Tax Year:
                //
                // TDSMAN : 2026-27
                // TRACES : 2026
                //---------------------------------------------------------
                string strFinancialYear =
                    GetConsoFinancialYearIT2025(
                        objTraceData.FAYear);

                //---------------------------------------------------------
                // Quarter:
                //
                // TRACES expects:
                // Q1 / Q2 / Q3 / Q4
                //---------------------------------------------------------
                string strQuarter =
                    GetConsoQuarterIT2025(
                        objTraceData.Quarter);

                //---------------------------------------------------------
                // Form Type:
                //
                // Already passed directly from calling code:
                //
                // 24Q  = 138
                // 26Q  = 140
                // 27EQ = 143
                // 27Q  = 144
                //---------------------------------------------------------
                string strFormType =
                    Convert.ToString(objTraceData.Forms).Trim();

                //---------------------------------------------------------
                // STEP 5 : VALIDATE REQUEST VALUES
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(strFinancialYear))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Invalid Tax Year.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(strQuarter))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Invalid Quarter.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(strFormType))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Invalid Form Type.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 6 : INITIATE CONSO FILE REQUEST
                //---------------------------------------------------------
                string strInitiateResponse =
                    InitiateConsoRequestIT2025(
                        strUserID,
                        strTAN,
                        strFinancialYear,
                        strQuarter,
                        strFormType);

                //---------------------------------------------------------
                // STEP 7 : VALIDATE RESPONSE
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(strInitiateResponse))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "No response received from TRACES while "
                        + "initiating the Conso File request.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 8 : PARSE JSON RESPONSE
                //
                // SUCCESS RESPONSE EXAMPLE:
                //
                // {
                //     "requestIds":"300253255",
                //     "status":"Initiated",
                //     "message":
                //       "Request acknowledged. Generation in progress."
                // }
                //
                // NORMAL ERROR RESPONSE EXAMPLE:
                //
                // {
                //     "requestIds":null,
                //     "status":"error",
                //     "message":
                //       "No statement available for the given search criteria"
                // }
                //
                // DUPLICATE RESPONSE EXAMPLE:
                //
                // {
                //     "requestIds":null,
                //     "status":"error",
                //     "message":
                //       "Duplicate request found. No new request created. 300256235"
                // }
                //---------------------------------------------------------
                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strInitiateResponse);

                if (objJSON == null)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Invalid response received from TRACES."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "TRACES Response:"
                        + Environment.NewLine
                        + strInitiateResponse;

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 9 : READ STATUS / MESSAGE FIRST
                //---------------------------------------------------------
                string strStatus = "";
                string strMessage = "";

                if (objJSON.status != null)
                {
                    strStatus =
                        Convert.ToString(
                            objJSON.status);
                }

                if (objJSON.message != null)
                {
                    strMessage =
                        Convert.ToString(
                            objJSON.message);
                }

                //---------------------------------------------------------
                // STEP 10 : TRAP TRACES ERROR RESPONSE
                //---------------------------------------------------------
                if (!string.IsNullOrWhiteSpace(strStatus) &&
                    strStatus.Equals(
                        "error",
                        StringComparison.OrdinalIgnoreCase))
                {
                    //-----------------------------------------------------
                    // SPECIAL CASE:
                    // DUPLICATE REQUEST FOUND
                    //
                    // Example:
                    //
                    // "Duplicate request found.
                    //  No new request created. 300256235"
                    //
                    // In this situation, do NOT treat it as failure.
                    // Extract the already existing Request ID and
                    // continue with the download process.
                    //-----------------------------------------------------
                    if (!string.IsNullOrWhiteSpace(strMessage) &&
                        strMessage.IndexOf(
                            "Duplicate request found",
                            StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string strExistingRequestID = "";

                        //-------------------------------------------------
                        // Extract Request ID from TRACES message.
                        //
                        // Request IDs are numeric and normally
                        // 9 digits, but \d{6,} keeps this flexible.
                        //-------------------------------------------------
                        System.Text.RegularExpressions.Match objMatch =
                            System.Text.RegularExpressions.Regex.Match(
                                strMessage,
                                @"\b\d{6,}\b");

                        if (objMatch.Success)
                        {
                            strExistingRequestID =
                                objMatch.Value;
                        }

                        //-------------------------------------------------
                        // Existing Request ID successfully obtained.
                        //-------------------------------------------------
                        if (!string.IsNullOrWhiteSpace(
                            strExistingRequestID))
                        {
                            objResponse.Respons =
                                enmResponse.Success;

                            objResponse.Data =
                                strExistingRequestID;

                            objResponse.Message =
                                "A Conso File request already exists."
                                + Environment.NewLine
                                + Environment.NewLine
                                + "Existing Request ID: "
                                + strExistingRequestID;

                            return objResponse;
                        }

                        //-------------------------------------------------
                        // Duplicate reported but Request ID could
                        // not be extracted.
                        //-------------------------------------------------
                        objResponse.Respons =
                            enmResponse.Failed;

                        objResponse.Message =
                            !string.IsNullOrWhiteSpace(strMessage)
                                ? strMessage
                                : "A duplicate Conso File request exists, "
                                  + "but its Request ID could not be obtained.";

                        return objResponse;
                    }

                    //-----------------------------------------------------
                    // NORMAL TRACES ERROR
                    //-----------------------------------------------------
                    objResponse.Respons =
                        enmResponse.Failed;

                    if (!string.IsNullOrWhiteSpace(strMessage))
                    {
                        //-------------------------------------------------
                        // Example:
                        //
                        // "No statement available for the given
                        //  search criteria"
                        //-------------------------------------------------
                        objResponse.Message =
                            strMessage;
                    }
                    else
                    {
                        objResponse.Message =
                            "Conso File is not available for "
                            + "the selected statement.";
                    }

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 11 : EXTRACT REQUEST ID FOR NORMAL SUCCESS
                //---------------------------------------------------------
                string strRequestID = "";

                if (objJSON.requestIds != null)
                {
                    strRequestID =
                        Convert.ToString(
                            objJSON.requestIds);
                }

                //---------------------------------------------------------
                // If Request ID is blank but TRACES returned a message,
                // show the TRACES message instead of a technical message.
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(strRequestID))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    if (!string.IsNullOrWhiteSpace(strMessage))
                    {
                        objResponse.Message =
                            strMessage;
                    }
                    else
                    {
                        objResponse.Message =
                            "Conso File request could not be initiated.";
                    }

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 12 : SUCCESS
                //---------------------------------------------------------
                objResponse.Respons =
                    enmResponse.Success;

                //---------------------------------------------------------
                // Calling form can retrieve request number using:
                //
                // strRequestNo =
                // Convert.ToString(response.Data);
                //---------------------------------------------------------
                objResponse.Data =
                    strRequestID;

                objResponse.Message =
                    "Conso File request submitted successfully."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Request ID: "
                    + strRequestID;

                if (!string.IsNullOrWhiteSpace(strStatus))
                {
                    objResponse.Message +=
                        Environment.NewLine
                        + "Status: "
                        + strStatus;
                }

                if (!string.IsNullOrWhiteSpace(strMessage))
                {
                    objResponse.Message +=
                        Environment.NewLine
                        + strMessage;
                }

                return objResponse;
            }

            //-------------------------------------------------------------
            // WEB EXCEPTION
            //-------------------------------------------------------------
            catch (WebException webEx)
            {
                this.bnlSessionExists = false;

                string strError =
                    webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream()))
                        {
                            string strServerError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(
                                strServerError))
                            {
                                //-------------------------------------------------
                                // If TRACES returns JSON in WebException,
                                // try to extract its message.
                                //-------------------------------------------------
                                try
                                {
                                    dynamic objErrorJSON =
                                        Newtonsoft.Json.JsonConvert.DeserializeObject(
                                            strServerError);

                                    if (objErrorJSON != null &&
                                        objErrorJSON.message != null)
                                    {
                                        string strTRACESMessage =
                                            Convert.ToString(
                                                objErrorJSON.message);

                                        if (!string.IsNullOrWhiteSpace(
                                            strTRACESMessage))
                                        {
                                            //-------------------------------------------------
                                            // ALSO HANDLE DUPLICATE REQUEST HERE
                                            //-------------------------------------------------
                                            if (strTRACESMessage.IndexOf(
                                                "Duplicate request found",
                                                StringComparison.OrdinalIgnoreCase) >= 0)
                                            {
                                                System.Text.RegularExpressions.Match objMatch =
                                                    System.Text.RegularExpressions.Regex.Match(
                                                        strTRACESMessage,
                                                        @"\b\d{6,}\b");

                                                if (objMatch.Success)
                                                {
                                                    objResponse.Respons =
                                                        enmResponse.Success;

                                                    objResponse.Data =
                                                        objMatch.Value;

                                                    objResponse.Message =
                                                        "A Conso File request already exists."
                                                        + Environment.NewLine
                                                        + Environment.NewLine
                                                        + "Existing Request ID: "
                                                        + objMatch.Value;

                                                    return objResponse;
                                                }
                                            }

                                            //-------------------------------------------------
                                            // Normal TRACES error message
                                            //-------------------------------------------------
                                            objResponse.Respons =
                                                enmResponse.Failed;

                                            objResponse.Message =
                                                strTRACESMessage;

                                            return objResponse;
                                        }
                                    }
                                }
                                catch
                                {
                                    // If JSON parsing fails,
                                    // continue with normal error handling.
                                }

                                strError +=
                                    Environment.NewLine
                                    + "TRACES Response: "
                                    + strServerError;
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    strError;

                return objResponse;
            }

            //-------------------------------------------------------------
            // GENERAL EXCEPTION
            //-------------------------------------------------------------
            catch (Exception err)
            {
                this.bnlSessionExists = false;

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    err.Message;

                return objResponse;
            }
        }

        #endregion


        #region InitiateConsoRequestIT2025

        private string InitiateConsoRequestIT2025(
            string userId,
            string tan,
            string financialYear,
            string quarter,
            string formType)
        {
            string strURL =
                "https://traces-app.tdscpc.gov.in/"
                + "tanconsolidatedrepservice/tds/conso/request";

            //---------------------------------------------------------
            // TRACES Payload Example:
            //
            // {
            //   "userId":"CALP04665D",
            //   "tan":"CALP04665D",
            //   "financialYear":"2026",
            //   "quarter":"Q1",
            //   "formType":"140"
            // }
            //---------------------------------------------------------
            var objPayload = new
            {
                userId = userId,
                tan = tan,
                financialYear = financialYear,
                quarter = quarter,
                formType = formType
            };

            string strJSON =
                Newtonsoft.Json.JsonConvert.SerializeObject(
                    objPayload);

            return makeHTTPPostJSONRequest_IT2025(
                strURL,
                strJSON);
        }

        #endregion


        #region GetConsoFinancialYearIT2025

        private string GetConsoFinancialYearIT2025(
            string financialYear)
        {
            if (string.IsNullOrWhiteSpace(financialYear))
                return "";

            string strYear =
                financialYear.Trim();

            //---------------------------------------------------------
            // 2026-27 --> 2026
            //---------------------------------------------------------
            if (strYear.Contains("-"))
            {
                string[] arrYear =
                    strYear.Split('-');

                if (arrYear.Length > 0)
                    strYear = arrYear[0];
            }

            if (strYear.Length >= 4)
            {
                strYear =
                    strYear.Substring(0, 4);
            }

            int intYear;

            if (!int.TryParse(strYear, out intYear))
                return "";

            return intYear.ToString();
        }

        #endregion


        #region GetConsoQuarterIT2025

        private string GetConsoQuarterIT2025(
            string quarter)
        {
            if (string.IsNullOrWhiteSpace(quarter))
                return "";

            string strQuarter =
                quarter.Trim().ToUpper();

            //---------------------------------------------------------
            // If already coming as Q1 / Q2 / Q3 / Q4
            //---------------------------------------------------------
            if (strQuarter == "Q1" ||
                strQuarter == "Q2" ||
                strQuarter == "Q3" ||
                strQuarter == "Q4")
            {
                return strQuarter;
            }

            //---------------------------------------------------------
            // Also support TDSMAN's internal quarter codes
            // if required:
            //
            // 3 = Q1
            // 4 = Q2
            // 5 = Q3
            // 6 = Q4
            //---------------------------------------------------------
            switch (strQuarter)
            {
                case "3":
                case "1":
                    return "Q1";

                case "4":
                case "2":
                    return "Q2";

                case "5":
                    return "Q3";

                case "6":
                    return "Q4";

                default:
                    return "";
            }
        }

        #endregion

        #region GetConsoVerificationMethodsIT2025

        public TracesResponse GetConsoVerificationMethodsIT2025(
            string formType)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                string strURL =
                    "https://traces-app.tdscpc.gov.in/"
                    + "everificationservice/api/authentication/getverificationmethod";

                var objPayload = new
                {
                    formName = formType
                };

                string strJSON =
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        objPayload);

                string strResponse =
                    makeHTTPPostJSONRequest_IT2025(
                        strURL,
                        strJSON);

                if (string.IsNullOrWhiteSpace(strResponse))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "No response received from TRACES while fetching verification methods.";

                    return objResponse;
                }

                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strResponse);

                if (objJSON == null)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Invalid response received from TRACES.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // Expected:
                //
                // {
                //   "status":200,
                //   "message":"Verification methods retrieved successfully",
                //   "data":["AadhaarOTP","DSC"]
                // }
                //---------------------------------------------------------

                bool blnAadhaarAvailable = false;

                if (objJSON.data != null)
                {
                    foreach (var item in objJSON.data)
                    {
                        if (Convert.ToString(item)
                            .Equals(
                                "AadhaarOTP",
                                StringComparison.OrdinalIgnoreCase))
                        {
                            blnAadhaarAvailable = true;
                            break;
                        }
                    }
                }

                if (!blnAadhaarAvailable)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Aadhaar OTP verification is not available for this request.";

                    return objResponse;
                }

                objResponse.Respons = enmResponse.Success;
                objResponse.Message =
                    "Aadhaar OTP verification is available.";

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region GetConsoAadhaarDetailsIT2025
        public TracesResponse GetConsoAadhaarDetailsIT2025(
            string tan)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //https://traces-app.tdscpc.gov.in/everificationservice/api/authentication/aadhaar/check-pan-linkage/CALS10294E
                string strURL =
                    "https://traces-app.tdscpc.gov.in/"
                    + "everificationservice/api/authentication/"
                    + "aadhaar/check-pan-linkage/"
                    + tan;

                string strResponse =
                    makeHTTPGetRequest_ITOTP(
                        strURL);

                if (string.IsNullOrWhiteSpace(strResponse))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "No response received from TRACES while fetching Aadhaar details.";

                    return objResponse;
                }

                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strResponse);

                if (objJSON == null ||
                    objJSON.data == null)
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message =
                        "Invalid Aadhaar verification response received from TRACES.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // Expected fields:
                //
                // authorizedPersonName
                // authorizedPersonPan
                // maskedAadhaar
                // message
                //---------------------------------------------------------

                ConsoAadhaarInfo objInfo =
                    new ConsoAadhaarInfo();

                objInfo.AuthorizedPersonName =
                    Convert.ToString(
                        objJSON.data.authorizedPersonName);

                objInfo.AuthorizedPersonPAN =
                    Convert.ToString(
                        objJSON.data.authorizedPersonPan);

                objInfo.MaskedAadhaar =
                    Convert.ToString(
                        objJSON.data.maskedAadhaar);

                objInfo.Message =
                    Convert.ToString(
                        objJSON.data.message);

                if (string.IsNullOrWhiteSpace(
                    objInfo.AuthorizedPersonPAN))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Authorised Person PAN was not returned by TRACES.";

                    return objResponse;
                }

                objResponse.Respons =
                    enmResponse.Success;

                objResponse.CustomeTypes =
                    objInfo;

                objResponse.Message =
                    objInfo.Message;

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region SendConsoAadhaarOTPIT2025

        public TracesResponse SendConsoAadhaarOTPIT2025(
            string authorizedPersonPAN)
        {
            TracesResponse objResponse =
                new TracesResponse();

            try
            {
                string strURL =
                    "https://traces-app.tdscpc.gov.in/"
                    + "everificationservice/api/authentication/"
                    + "aadhaar/send-otp/"
                    + authorizedPersonPAN
                    + "?consentGiven=true";

                //---------------------------------------------------------
                // TRACES sends an empty JSON object {}
                //---------------------------------------------------------
                string strJSON = "{}";

                string strResponse =
                    makeHTTPPostJSONRequest_IT2025(
                        strURL,
                        strJSON);

                if (string.IsNullOrWhiteSpace(strResponse))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "No response received from TRACES while sending Aadhaar OTP.";

                    return objResponse;
                }

                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strResponse);

                if (objJSON == null)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Invalid response received from TRACES while sending OTP.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // Response example:
                //
                // {
                //   "data":{
                //      "message":
                //        "OTP sent successfully to registered mobile number",
                //      "status":200,
                //      "attemptsRemaining":0,
                //      "transactionId":"....",
                //      "valid":true
                //   },
                //   "status":200,
                //   "message":
                //     "OTP sent successfully to registered mobile number"
                // }
                //---------------------------------------------------------

                string strMessage = "";
                string strTransactionID = "";
                bool blnValid = false;

                if (objJSON.message != null)
                {
                    strMessage =
                        Convert.ToString(
                            objJSON.message);
                }

                if (objJSON.data != null)
                {
                    if (objJSON.data.transactionId != null)
                    {
                        strTransactionID =
                            Convert.ToString(
                                objJSON.data.transactionId);
                    }

                    if (objJSON.data.valid != null)
                    {
                        blnValid =
                            Convert.ToBoolean(
                                objJSON.data.valid);
                    }

                    if (string.IsNullOrWhiteSpace(strMessage) &&
                        objJSON.data.message != null)
                    {
                        strMessage =
                            Convert.ToString(
                                objJSON.data.message);
                    }
                }

                if (!blnValid ||
                    string.IsNullOrWhiteSpace(strTransactionID))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        !string.IsNullOrWhiteSpace(strMessage)
                            ? strMessage
                            : "Aadhaar OTP could not be sent.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // IMPORTANT:
                // Save transactionId in Data.
                //---------------------------------------------------------
                objResponse.Respons =
                    enmResponse.Success;

                objResponse.Data =
                    strTransactionID;

                objResponse.Message =
                    !string.IsNullOrWhiteSpace(strMessage)
                        ? strMessage
                        : "OTP sent successfully to registered mobile number.";

                return objResponse;
            }
            catch (WebException webEx)
            {
                string strError =
                    webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream()))
                        {
                            string strServerError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(strServerError))
                            {
                                try
                                {
                                    dynamic objError =
                                        Newtonsoft.Json.JsonConvert.DeserializeObject(
                                            strServerError);

                                    if (objError != null &&
                                        objError.message != null)
                                    {
                                        strError =
                                            Convert.ToString(
                                                objError.message);
                                    }
                                }
                                catch
                                {
                                    strError +=
                                        Environment.NewLine
                                        + strServerError;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    strError;

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region makeHTTPGetRequest_IT2025

        //private string makeHTTPGetRequest_IT2025(
        //    string url)
        //{
        //    ServicePointManager.SecurityProtocol =
        //        SecurityProtocolType.Tls12;

        //    HttpWebRequest req =
        //        (HttpWebRequest)WebRequest.Create(
        //            url);

        //    req.Method = "GET";
        //    req.KeepAlive = true;

        //    req.CookieContainer =
        //        TracesSession.CookieJar;

        //    req.Accept =
        //        "application/json";

        //    req.UserAgent =
        //        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
        //        + "AppleWebKit/537.36 Chrome/138.0 Safari/537.36";

        //    req.Headers.Add(
        //        "Authorization",
        //        "Bearer " + tracesAuthToken);

        //    req.Headers.Add(
        //        "Origin",
        //        "https://traces.tdscpc.gov.in");

        //    req.Referer =
        //        "https://traces.tdscpc.gov.in/";

        //    req.Timeout = 60000;

        //    using (HttpWebResponse resp =
        //        (HttpWebResponse)req.GetResponse())
        //    {
        //        if (resp.Cookies != null)
        //        {
        //            TracesSession.CookieJar.Add(
        //                resp.Cookies);
        //        }

        //        using (StreamReader sr =
        //            new StreamReader(
        //                resp.GetResponseStream(),
        //                Encoding.UTF8))
        //        {
        //            return sr.ReadToEnd();
        //        }
        //    }
        //}

        #endregion

        #region VerifyConsoAadhaarOTPIT2025

        public TracesResponse VerifyConsoAadhaarOTPIT2025(
            string pan,
            string otp)
        {
            TracesResponse objResponse =
                new TracesResponse();

            try
            {
                //---------------------------------------------------------
                // STEP 1 : VALIDATE INPUT
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(pan))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "PAN is not available.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(otp))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Please enter Aadhaar OTP.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 2 : VERIFY OTP
                //---------------------------------------------------------
                string strURL =
                    "https://traces-app.tdscpc.gov.in/"
                    + "everificationservice/api/authentication/"
                    + "aadhaar/verify-otp";

                var objPayload = new
                {
                    pan = pan.Trim(),
                    otp = otp.Trim()
                };

                string strJSON =
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        objPayload);

                string strResponse =
                    makeHTTPPostJSONRequest_IT2025(
                        strURL,
                        strJSON);

                //---------------------------------------------------------
                // STEP 3 : CHECK RESPONSE
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(strResponse))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "No response received from TRACES while verifying Aadhaar OTP.";

                    return objResponse;
                }

                dynamic objJSON =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(
                        strResponse);

                if (objJSON == null)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Invalid response received from TRACES while verifying Aadhaar OTP.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // Expected:
                //
                // {
                //   "data":{
                //      "message":
                //       "Verification using Aadhaar OTP Successful!",
                //      "status":200,
                //      "attemptsRemaining":0,
                //      "transactionId":null,
                //      "valid":true
                //   },
                //   "status":200,
                //   "message":
                //      "Verification using Aadhaar OTP Successful!"
                // }
                //---------------------------------------------------------

                bool blnValid = false;
                string strMessage = "";
                int intStatus = 0;

                if (objJSON.status != null)
                {
                    int.TryParse(
                        Convert.ToString(objJSON.status),
                        out intStatus);
                }

                if (objJSON.message != null)
                {
                    strMessage =
                        Convert.ToString(
                            objJSON.message);
                }

                if (objJSON.data != null)
                {
                    if (objJSON.data.valid != null)
                    {
                        bool.TryParse(
                            Convert.ToString(objJSON.data.valid),
                            out blnValid);
                    }

                    if (string.IsNullOrWhiteSpace(strMessage) &&
                        objJSON.data.message != null)
                    {
                        strMessage =
                            Convert.ToString(
                                objJSON.data.message);
                    }
                }

                //---------------------------------------------------------
                // STEP 4 : SUCCESS
                //---------------------------------------------------------
                if (intStatus == 200 &&
                    blnValid)
                {
                    objResponse.Respons =
                        enmResponse.Success;

                    objResponse.Message =
                        !string.IsNullOrWhiteSpace(strMessage)
                            ? strMessage
                            : "Aadhaar OTP verified successfully.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 5 : FAILED OTP
                //---------------------------------------------------------
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    !string.IsNullOrWhiteSpace(strMessage)
                        ? strMessage
                        : "Aadhaar OTP verification failed.";

                return objResponse;
            }

            catch (WebException webEx)
            {
                string strError =
                    webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream()))
                        {
                            string strServerError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(strServerError))
                            {
                                try
                                {
                                    dynamic objError =
                                        Newtonsoft.Json.JsonConvert.DeserializeObject(
                                            strServerError);

                                    if (objError != null &&
                                        objError.message != null)
                                    {
                                        strError =
                                            Convert.ToString(
                                                objError.message);
                                    }
                                    else
                                    {
                                        strError +=
                                            Environment.NewLine
                                            + strServerError;
                                    }
                                }
                                catch
                                {
                                    strError +=
                                        Environment.NewLine
                                        + strServerError;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    strError;

                return objResponse;
            }

            catch (Exception ex)
            {
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region DownloadConsoFileIT2025

        public TracesResponse DownloadConsoFileIT2025(
            int requestId,
            string downloadFolder)
        {
            TracesResponse objResponse =
                new TracesResponse();

            try
            {
                //---------------------------------------------------------
                // STEP 1 : VALIDATE
                //---------------------------------------------------------
                if (requestId <= 0)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Invalid Conso File Request ID.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(downloadFolder))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Download folder is not available.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 2 : CREATE FOLDER IF REQUIRED
                //---------------------------------------------------------
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(
                        downloadFolder);
                }

                //---------------------------------------------------------
                // STEP 3 : VERIFY AND DOWNLOAD
                //
                // Confirmed TRACES API:
                //
                // PUT
                // /tanconsolidatedrepservice/tds/conso/
                // verify-and-download?requestId=300227148
                //
                // Payload:
                //
                // {
                //   "verification":"A"
                // }
                //---------------------------------------------------------
                string strURL =
                    "https://traces-app.tdscpc.gov.in/"
                    + "tanconsolidatedrepservice/tds/conso/"
                    + "verify-and-download?requestId="
                    + requestId;

                HttpWebRequest req =
                    (HttpWebRequest)WebRequest.Create(
                        strURL);

                req.Method =
                    "PUT";

                req.KeepAlive =
                    true;

                //---------------------------------------------------------
                // IMPORTANT:
                // Use the SAME authenticated TRACES session.
                //---------------------------------------------------------
                req.CookieContainer =
                    TracesSession.CookieJar;

                req.ContentType =
                    "application/json";

                req.Accept =
                    "*/*";

                req.UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
                    + "AppleWebKit/537.36 Chrome/138.0 Safari/537.36";

                //---------------------------------------------------------
                // Bearer Token
                //---------------------------------------------------------
                if (!string.IsNullOrWhiteSpace(tracesAuthToken))
                {
                    req.Headers.Add(
                        "Authorization",
                        "Bearer " + tracesAuthToken);
                }

                req.Headers.Add(
                    "Origin",
                    "https://traces.tdscpc.gov.in");

                req.Referer =
                    "https://traces.tdscpc.gov.in/"
                    + "auth/consolidatedTraces/consolidatedPath";

                req.Timeout =
                    60000;

                //---------------------------------------------------------
                // STEP 4 : PAYLOAD
                //---------------------------------------------------------
                var objPayload = new
                {
                    verification = "A"
                };

                string strJSON =
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        objPayload);

                byte[] arrData =
                    Encoding.UTF8.GetBytes(
                        strJSON);

                req.ContentLength =
                    arrData.Length;

                using (Stream stream =
                    req.GetRequestStream())
                {
                    stream.Write(
                        arrData,
                        0,
                        arrData.Length);
                }

                //---------------------------------------------------------
                // STEP 5 : GET RESPONSE
                //---------------------------------------------------------
                HttpWebResponse resp =
                    (HttpWebResponse)req.GetResponse();

                if (resp.Cookies != null)
                {
                    TracesSession.CookieJar.Add(
                        resp.Cookies);
                }

                //---------------------------------------------------------
                // STEP 6 : CHECK IF TRACES RETURNED JSON ERROR
                //---------------------------------------------------------
                string strContentType =
                    Convert.ToString(
                        resp.ContentType);

                if (!string.IsNullOrWhiteSpace(strContentType) &&
                    strContentType.IndexOf(
                        "application/json",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string strJSONResponse = "";

                    using (StreamReader sr =
                        new StreamReader(
                            resp.GetResponseStream(),
                            Encoding.UTF8))
                    {
                        strJSONResponse =
                            sr.ReadToEnd();
                    }

                    string strMessage =
                        "Conso File could not be downloaded.";

                    try
                    {
                        dynamic objJSON =
                            Newtonsoft.Json.JsonConvert.DeserializeObject(
                                strJSONResponse);

                        if (objJSON != null)
                        {
                            if (objJSON.message != null)
                            {
                                strMessage =
                                    Convert.ToString(
                                        objJSON.message);
                            }
                            else if (objJSON.data != null &&
                                     objJSON.data.message != null)
                            {
                                strMessage =
                                    Convert.ToString(
                                        objJSON.data.message);
                            }
                        }
                    }
                    catch
                    {
                    }

                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        strMessage;

                    resp.Close();

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 7 : GET FILE NAME FROM CONTENT-DISPOSITION
                //---------------------------------------------------------
                string strFileName = "";

                string strDisposition =
                    resp.Headers[
                        "Content-Disposition"];

                if (!string.IsNullOrWhiteSpace(
                    strDisposition))
                {
                    //-----------------------------------------------------
                    // filename="ConsolidatedFile.zip"
                    //-----------------------------------------------------
                    System.Text.RegularExpressions.Match objMatch =
                        System.Text.RegularExpressions.Regex.Match(
                            strDisposition,
                            @"filename\*=UTF-8''(?<filename>[^;]+)|filename=""?(?<filename>[^"";]+)""?",
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    if (objMatch.Success)
                    {
                        strFileName =
                            objMatch.Groups["filename"].Value;
                    }
                }

                //---------------------------------------------------------
                // STEP 8 : FALLBACK FILE NAME
                //---------------------------------------------------------
                if (string.IsNullOrWhiteSpace(
                    strFileName))
                {
                    strFileName =
                        "ConsolidatedFile_"
                        + requestId
                        + ".zip";
                }

                //---------------------------------------------------------
                // Remove any path portion returned by server
                //---------------------------------------------------------
                strFileName =
                    Path.GetFileName(
                        strFileName);

                string strFullPath =
                    Path.Combine(
                        downloadFolder,
                        strFileName);

                //---------------------------------------------------------
                // STEP 9 : SAVE FILE
                //---------------------------------------------------------
                using (Stream inputStream =
                    resp.GetResponseStream())
                {
                    using (FileStream fs =
                        new FileStream(
                            strFullPath,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None))
                    {
                        byte[] buffer =
                            new byte[8192];

                        int intRead = 0;

                        while ((intRead =
                            inputStream.Read(
                                buffer,
                                0,
                                buffer.Length)) > 0)
                        {
                            fs.Write(
                                buffer,
                                0,
                                intRead);
                        }
                    }
                }

                resp.Close();

                //---------------------------------------------------------
                // STEP 10 : VALIDATE FILE
                //---------------------------------------------------------
                if (!File.Exists(strFullPath))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "TRACES returned the Conso File but it could not be saved.";

                    return objResponse;
                }

                FileInfo objFileInfo =
                    new FileInfo(
                        strFullPath);

                if (objFileInfo.Length == 0)
                {
                    try
                    {
                        File.Delete(
                            strFullPath);
                    }
                    catch
                    {
                    }

                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "The Conso File returned by TRACES is empty.";

                    return objResponse;
                }

                //---------------------------------------------------------
                // STEP 11 : SUCCESS
                //---------------------------------------------------------
                objResponse.Respons =
                    enmResponse.Success;

                objResponse.Data =
                    strFullPath;

                objResponse.Message =
                    "Conso File downloaded successfully.";

                return objResponse;
            }

            //-------------------------------------------------------------
            // WEB EXCEPTION
            //-------------------------------------------------------------
            catch (WebException webEx)
            {
                string strError =
                    webEx.Message;

                try
                {
                    if (webEx.Response != null)
                    {
                        using (StreamReader sr =
                            new StreamReader(
                                webEx.Response.GetResponseStream(),
                                Encoding.UTF8))
                        {
                            string strServerError =
                                sr.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(
                                strServerError))
                            {
                                try
                                {
                                    dynamic objError =
                                        Newtonsoft.Json.JsonConvert.DeserializeObject(
                                            strServerError);

                                    if (objError != null)
                                    {
                                        if (objError.message != null)
                                        {
                                            strError =
                                                Convert.ToString(
                                                    objError.message);
                                        }
                                        else if (objError.data != null &&
                                                 objError.data.message != null)
                                        {
                                            strError =
                                                Convert.ToString(
                                                    objError.data.message);
                                        }
                                        else
                                        {
                                            strError +=
                                                Environment.NewLine
                                                + strServerError;
                                        }
                                    }
                                }
                                catch
                                {
                                    strError +=
                                        Environment.NewLine
                                        + strServerError;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    strError;

                return objResponse;
            }

            //-------------------------------------------------------------
            // GENERAL EXCEPTION
            //-------------------------------------------------------------
            catch (Exception ex)
            {
                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

    }
}
