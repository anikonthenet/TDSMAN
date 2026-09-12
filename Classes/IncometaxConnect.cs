#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using System.IO;

using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Linq;

using System.Net.Sockets;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
//using System.Web.Script.Serialization;
#endregion

namespace TDSMAN.Classes
{      

    public class IncomeTaxConnect2025
    {
        #region Variable Declaration
        string strURL = "";

        //LAVEL 1. HTTP REQUEST WITH LOGIN PAGE ONLY FOR GETTING SESSION VALUES.  

        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;

        string strServerResponse = "";
        CookieContainer objContainer = new CookieContainer();
        List<ErrorDB<int, string, string>> objMsgDictionary = new List<ErrorDB<int, string, string>>();

        string strBaseURL = "https://eportal.incometax.gov.in/iec/";
        private bool bnlSessionExists = false;

        StringBuilder objParam = new StringBuilder();

        private string RefId = "";
        private string ReQId = "";
        enum enmElementType
        {
            InnerText,
            InnerHTML,
            Value,
            Name
        }
        //===========================================================
        // CONSTANTS
        //===========================================================
        private const string INCOME_TAX_BASE_URL = "https://eportal.incometax.gov.in";
        //===========================================================
        // SESSION VARIABLES
        //===========================================================
        private CookieContainer IncomeTaxCookieJar =
            new CookieContainer();
        private bool IncomeTaxSessionExists = false;
        private string IncomeTaxUserID = "";
        #endregion

        public IncomeTaxConnect2025()
        {
            IncomeTaxCookieJar = new CookieContainer();

            IncomeTaxSessionExists = false;

            IncomeTaxUserID = "";
        }
        public string LoggedInUserID
        {
            get
            {
                return IncomeTaxUserID;
            }
        }

        public void ResetSession()
        {
            IncomeTaxCookieJar =
                new CookieContainer();

            IncomeTaxSessionExists =
                false;

            IncomeTaxUserID =
                "";
        }


        #region ConvertBase64

        public string ConvertBase64(string strText)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strText);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        #endregion


        #region IsSessionExists
        public bool IsSessionExists
        {
            get { return bnlSessionExists; }
            set { bnlSessionExists = true; }
        }

        #endregion

        #region IncometaxConnect
        //public IncometaxConnect()
        //{

        //}

        #endregion

        #region IsServerError
        private TracesResponse IsServerError(string strServerResponse, string strXQuery)
        {
            string strErrorText = "";
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
            {
                // error = node.SelectNodes("//span[@id=\"err_Summary\"]");
                return objResponse;
            }
            //-------------------------------------------
            for (int i = 0; i < error.Count; i++)
            {
                strErrorText = error[i].InnerText;
                strErrorText = strErrorText.Replace("\t", "");
            }
            //---------------------------------------------
            if (!string.IsNullOrEmpty(strErrorText.Trim()))
            {
                objResponse.Message = strErrorText;
                objResponse.Respons = enmResponse.Failed;
            }
            //---------------------------------------------
            return objResponse;
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

        #region IsConditionMatch
        private bool IsConditionMatch(string strResponse, string strPattern)
        {
            return Regex.IsMatch(strResponse, strPattern, RegexOptions.IgnoreCase);
        }

        #endregion

        #region makeLoginToIncomeTax       
        public TracesResponse makeLoginToIncomeTax(string Tan,string Pwd)
        {
            TracesResponse objResponse = new TracesResponse();

            //CREATE DATA FOR LOGIN FORM
            StringBuilder sbParameter = new StringBuilder();
            try
            {

                strServerResponse = makeHTTPGetRequest(strBaseURL+ "foservices/#/login");

                //1st Level of TAN Checking 
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", "{\"entity\":\"" + Tan + "\",\"serviceName\":\"loginService\"}");
                //strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", "{\"entity\":\"" + Tan + "\",\"serviceName\":\"wLoginService\"}");

                if (string.IsNullOrEmpty(strServerResponse))
                {
                    IsSessionExists = false;
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Login Failed or Server Error";
                    return objResponse;
                }
                //-------------------------------------------------------------------------
                Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
                //--------------------------------------------------------------------------------------
                if (objRoot.messages.Count > 0)
                {
                    foreach(Incometax.Message msg in objRoot.messages)
                    {
                        if(msg.type == "ERROR")
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                            //break;
                        }
                        //
                        if(msg.desc.ToString().ToUpper().Contains("LOCKED") == true)
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                        }
                    }
                }
                //2nd level of password checking
                //--------------------------------------------------------------------------------------
                string strPass = ConvertBase64(Pwd);                

                string strData = "{\"errors\":[],\"reqId\":\"" + objRoot.reqId + "\",\"entity\":\"" + Tan + "\",\"entityType\":\"User ID\",\"pass\":\"" + strPass + "\",\"role\":\"TDS\",\"uidValdtnFlg\":\"true\",\"passValdtnFlg\":null,\"aadhaarMobileValidated\":\"false\",\"secAccssMsg\":\"\",\"secLoginOptions\":\"\",\"exemptedPan\":\"false\",\"userConsent\":\"\",\"imagePath\":null,\"imgByte\":null,\"serviceName\":\"loginService\"}";

                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);

                objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
                //--------------------------------------------------------------------------------------

                bool isSessionExists = false;
                if (objRoot.messages.Count > 0)
                {
                    foreach (Incometax.Message msg in objRoot.messages)
                    {
                        if (msg.type == "INFO" && msg.desc== "Invalid Password, Please retry.")
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                            //break;
                        }else if(msg.code== "EF00177" &&  msg.type == "ERROR" && msg.desc == "Session already active")
                        {
                            isSessionExists = true;
                        }
                    }
                }
                //=============================================
                //FORCE LOGIN IF SESSION EXISTS
                //============================================
                if (isSessionExists)
                {
                   // strData = "{\"errors\":[],\"reqId\":\"" + objRoot.reqId + "\",\"entity\":\"" + Tan + "\",\"entityType\":\"User ID\",\"pass\":\"" + strPass + "\",\"role\":\"TDS\",\"uidValdtnFlg\":\"true\",\"passValdtnFlg\":null,\"aadhaarMobileValidated\":\"false\",\"secAccssMsg\":\"\",\"secLoginOptions\":\"\",\"exemptedPan\":\"false\",\"userConsent\":\"\",\"imagePath\":null,\"imgByte\":null,\"serviceName\":\"loginService\"}";
                    strData = "{\"errors\":[],\"reqId\":\"" + objRoot.reqId + "\",\"entity\":\"" + Tan + "\",\"entityType\":\""+ objRoot.entityType + "\",\"role\":\""+ objRoot.role + "\",\"userType\":\""+ objRoot.userType + "\",\"uidValdtnFlg\":\""+ objRoot.uidValdtnFlg +"\",\"passValdtnFlg\":\""+ objRoot.passValdtnFlg + "\",\"mobileNo\":\""+ objRoot.mobileNo +"\",\"email\":\""+ objRoot.email+"\",\"aadhaarMobileValidated\":\""+ objRoot.aadhaarMobileValidated +"\",\"secAccssMsg\":\"\",\"secLoginOptions\":\"\",\"contactPan\":\""+ objRoot.contactPan +"\",\"contactEmail\":\""+ objRoot.contactEmail +" \",\"contactMobile\":\""+ objRoot.contactMobile +"\",\"lastLoginSuccessFlag\":\""+ objRoot.lastLoginSuccessFlag + "\",\"clientIp\":\""+ objRoot.clientIp + "\",\"exemptedPan\":\""+ objRoot.exemptedPan + "\",\"userConsent\":\"\",\"pass\":null,\"otpGenerationFlag\":\"true\",\"otpValdtnFlg\":\"true\",\"remark\":\"Continue\",\"serviceName\":\"loginService\"}";


                    strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);
                }

                //--------------------------------------------------------------------------------------
                objResponse.Respons = enmResponse.Success;
                objResponse.CustomeTypes = objRoot;
                //--------------------------------------------------------------------------------------
            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
                return objResponse;
            }
            return objResponse;
        }

        #endregion

        #region makeLogoff
        public TracesResponse Logoff(string strPan)
        {
            TracesResponse objRespoce = new TracesResponse();

            try
            {
                string  strData = "{\"serviceName\":\"logoutService\",\"entity\":\""+ strPan +"\",\"userType\":\"TDS\"}";
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);
                //===================================================
                this.bnlSessionExists = false;

                objRespoce.Respons = enmResponse.Success;

            }
            catch
            {
                objRespoce.Respons = enmResponse.Failed;
            }

            return objRespoce;
        }

        #endregion

        #region makeHTTPPostRequest
        private string makeHTTPPostRequest(string strURL, StringBuilder sbData)
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072; //-- ARUP //-- 2019/01/25

            request = (HttpWebRequest)WebRequest.Create(strURL);
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            // request.KeepAlive = false;
            request.KeepAlive = true;
            byte[] byteArray = null;
            string postData = "";

            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            {
                postData = sbData.ToString();
                byteArray = Encoding.UTF8.GetBytes(postData);
            }
            //----------------------------------------------------------
            // SET THE CONTENTLENGTH PROPERTY OF THE WEBREQUEST.
            //----------------------------------------------------------
            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
                request.ContentLength = byteArray.Length;

            if (sbData != null)
            {
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Accept-Language: en-US,en;q=0.9,bn;q=0.8");
                request.Timeout = 1000000000;
            }
            //----------------------------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;
            //----------------------------------------------------------
            if (response != null)
                request.CookieContainer.Add(response.Cookies);
            //----------------------------------------------------------
            // GET THE REQUEST STREAM.
            //----------------------------------------------------------
            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            {
                dataStream = request.GetRequestStream();
                //----------------------------------------------------------
                // WRITE THE DATA TO THE REQUEST STREAM.
                //----------------------------------------------------------
                dataStream.Write(byteArray, 0, byteArray.Length);
                //----------------------------------------------------------
                // CLOSE THE STREAM OBJECT.
                //----------------------------------------------------------
                dataStream.Close();
            }
            //----------------------------------------------------------
            // GET THE RESPONSE.
            //----------------------------------------------------------          
            response = (HttpWebResponse)request.GetResponse();
            //----------------------------------------------------------
            // GET THE STREAM CONTAINING CONTENT RETURNED BY THE SERVER.
            //----------------------------------------------------------
            dataStream = response.GetResponseStream();
            //----------------------------------------------------------
            // OPEN THE STREAM USING A STREAMREADER FOR EASY ACCESS.
            reader = new StreamReader(dataStream);
            //----------------------------------------------------------
            HttpStatusCode st = response.StatusCode;

            //if (response.StatusCode == HttpStatusCode.Found)
            //{
            //    string redirectLocation = response.Headers["Location"].ToString();
            //    string dsdd = makeHTTPGetRequest1(redirectLocation);

            //    string sdd = makeHTTPGetRequest("https://www.tdscpc.gov.in/app/login.xhtml");

            //}

            // Read the content.
            //----------------------------------------------------------
            strServerResponse = reader.ReadToEnd();
            //----------------------------------------------------------
            // Clean up the streams.
            //----------------------------------------------------------
            reader.Close();
            dataStream.Close();
            response.Close();
            //----------------------------------------------------------

            return strServerResponse;

        }

        #endregion

        #region makeHTTPGetRequest
        private string makeHTTPGetRequest(string strURL)
        {
            SetAllowUnsafeHeaderParsing();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            //--
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072; //-- ARUP //-- 2019/01/25
            request = (HttpWebRequest)HttpWebRequest.Create(strURL);
            //-------------------------------------           
            request.KeepAlive = true;
            request.Method = "GET";
            //--
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            request.Timeout = 1000000000;
            //-------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;

            response = (HttpWebResponse)request.GetResponse();
            request.CookieContainer.Add(response.Cookies);

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            //--------------------------------------
            strServerResponse = reader.ReadToEnd();
            //----------------------------------------
            reader.Close();
            dataStream.Close();
            response.Close();
            //-----------------------------------
            return strServerResponse;

        }

        #endregion


        #region makeHTTPJSONRequest
        private string makeHTTPJSONRequest(string strURL)
        {
            request = (HttpWebRequest)WebRequest.Create(strURL);
            string json = "";
            request.ServicePoint.Expect100Continue = false;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
            request.ContentType = "application/json";
            request.Headers.Add("Cache-Control", "no-cache");
            //
            request.KeepAlive = true; //wasim
            //
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;

            response = (HttpWebResponse)request.GetResponse();
            request.CookieContainer.Add(response.Cookies);
            //--------------------------------------------------------------------
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader responseReader = new StreamReader(responseStream))
                    json = responseReader.ReadToEnd();
            }
            //--------------------------------------------------------------------
            return json;
        }

        #endregion

        #region makeHTTPJSONPostRequest
        private string makeHTTPJSONPostRequest(string strURL, string sbData)
        {

            request = (HttpWebRequest)WebRequest.Create(strURL);
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            //request.KeepAlive = false;
            request.KeepAlive = true;

            byte[] byteArray = null;
            string postData = "";

            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            {
                postData = sbData.ToString();
                byteArray = Encoding.UTF8.GetBytes(postData);
            }
            //----------------------------------------------------------
            // SET THE CONTENTLENGTH PROPERTY OF THE WEBREQUEST.
            //----------------------------------------------------------
            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
                request.ContentLength = byteArray.Length;

            if (sbData != null)
            {
                request.Method = "POST";
                request.Accept = "application/json, text/plain, */*";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                request.ContentType = "application/json";               
                request.Headers.Add("Accept-Language: en-US,en;q=0.9,bn;q=0.8");
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.GetType().InvokeMember("ChangeInternal",
                   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null,
                   request.Headers, new object[] { "Host", "eportal.incometax.gov.in" });

                request.Timeout = 1000000000;
            }
            //----------------------------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;

            request.CookieContainer.Add(response.Cookies);
            //----------------------------------------------------------
            // GET THE REQUEST STREAM.
            //----------------------------------------------------------
            if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            {
                dataStream = request.GetRequestStream();
                //----------------------------------------------------------
                // WRITE THE DATA TO THE REQUEST STREAM.
                //----------------------------------------------------------
                dataStream.Write(byteArray, 0, byteArray.Length);
                //----------------------------------------------------------
                // CLOSE THE STREAM OBJECT.
                //----------------------------------------------------------
                dataStream.Close();
            }
            System.Threading.Thread.Sleep(5000);
            //----------------------------------------------------------
            // GET THE RESPONSE.
            //----------------------------------------------------------          
            response = (HttpWebResponse)request.GetResponse();
            //----------------------------------------------------------
            // GET THE STREAM CONTAINING CONTENT RETURNED BY THE SERVER.
            //----------------------------------------------------------
            dataStream = response.GetResponseStream();
            //----------------------------------------------------------
            // OPEN THE STREAM USING A STREAMREADER FOR EASY ACCESS.
            reader = new StreamReader(dataStream);
            //----------------------------------------------------------
            // Read the content.
            //----------------------------------------------------------
            strServerResponse = reader.ReadToEnd();
            //----------------------------------------------------------
            // Clean up the streams.
            //----------------------------------------------------------
            reader.Close();
            dataStream.Close();
            response.Close();
            //----------------------------------------------------------

            return strServerResponse;

        }

        #endregion

        #region makeHttpDownloadRequest
        private bool makeHttpDownloadRequest(string strURL, string strPath)
        {
            bool DownloadStatus = true;
            string strFilename = "";
            try
            {
                //-- 2014-01-25
                SetAllowUnsafeHeaderParsing();

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                //--
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.KeepAlive = true;
                request.Timeout = 300000;
                request.AllowWriteStreamBuffering = false;
                //request.AllowAutoRedirect = false;
                request.AllowAutoRedirect = true; //-- 2019/06/18
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
                //-- ARUP @ 2016/02/20
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                //---------------------------------------------------
                //for (int i = 0; i < response.Cookies.Count; i++)
                //{
                //    response.Cookies[i].Path = String.Empty;
                //}

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(response.Cookies);

                response = (HttpWebResponse)request.GetResponse();

                Int64 iSize = 120000;

                dataStream = response.GetResponseStream();

                strFilename = strURL.Substring(strURL.LastIndexOf("/") + 1);

                if (string.IsNullOrEmpty(strFilename))
                    strFilename = "File.zip";

                FileStream fs = new FileStream(strPath + "\\" + strFilename, FileMode.Create);

                byte[] read = new byte[iSize];

                int count = dataStream.Read(read, 0, read.Length);

                while (count > 0)
                {
                    fs.Write(read, 0, count);
                    count = dataStream.Read(read, 0, read.Length);
                }
                response.Close();
                fs.Close();
                dataStream.Close();
            }
            catch (Exception err)
            {
                DownloadStatus = false;
                throw err;

            }

            return DownloadStatus;
        }

        #endregion

        #region JsonParser
        private DataTable JsonParser(string s)
        {

            DataTable table = new DataTable();
            //------------------------------------------------------
            table.Columns.Add("Request Date");
            table.Columns.Add("Request Number");
            table.Columns.Add("Finnancial Number");
            table.Columns.Add("Quarter");

            table.Columns.Add("Form Type");
            table.Columns.Add("File Processed");
            table.Columns.Add("Status");
            table.Columns.Add("Remarks");

            string strLastToken = "Test";
            DataRow dRow = null;
            // DataColumn column1

            using (JsonTextReader reader = new JsonTextReader(new StringReader(s)))
            {
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.StartObject:
                            // Console.Write("Start object: ");
                            break;
                        case JsonToken.StartArray:
                            // Console.Write("Start array: ");
                            break;
                        case JsonToken.PropertyName:
                            //Console.WriteLine(reader.Value.ToString());

                            strLastToken = reader.Value.ToString();
                            break;
                        case JsonToken.EndArray:
                            // Console.WriteLine("End array");
                            break;
                        case JsonToken.EndObject:
                            // Console.WriteLine("End object");
                            break;
                        case JsonToken.String:
                        case JsonToken.Integer:
                        case JsonToken.Null:
                        case JsonToken.Float:
                            // Console.WriteLine(reader.Value.ToString());


                            switch (strLastToken)
                            {
                                case "reqDate":
                                    dRow = table.NewRow();
                                    dRow["Request Date"] = reader.Value.ToString();
                                    break;
                                case "reqNo":
                                    dRow["Request Number"] = reader.Value.ToString();
                                    break;
                                case "finYr":
                                    dRow["Finnancial Number"] = reader.Value.ToString();
                                    break;
                                case "qrtr":
                                    dRow["Quarter"] = reader.Value.ToString();
                                    break;
                                case "frmType":
                                    dRow["Form Type"] = reader.Value.ToString();
                                    break;
                                case "dntype":
                                    dRow["File Processed"] = reader.Value.ToString();
                                    break;

                                case "status":
                                    dRow["Status"] = reader.Value.ToString();
                                    break;

                                case "remarks":
                                    dRow["Remarks"] = Convert.ToString(reader.Value);
                                    table.Rows.Add(dRow);
                                    break;
                            }
                            break;
                    }
                }
            }


            return table;

        }


        #endregion

        #region JsonParserForCertificateValidation
        private DataTable JsonParserForCertificateValidation(string s, out string strRowCount)
        {
            strRowCount = "";
            DataTable table = new DataTable();
            //------------------------------------------------------
            table.Columns.Add("Sr.No.");
            table.Columns.Add("Certificate Number");
            table.Columns.Add("Financial Year");
            table.Columns.Add("PAN of the Deductee");
            table.Columns.Add("Name of Deductee");
            table.Columns.Add("Valid From");
            table.Columns.Add("Valid To");
            table.Columns.Add("Section Code");
            table.Columns.Add("Nature of Payment");
            table.Columns.Add("Rate of TDS as per Certificate");
            table.Columns.Add("Certificate Limit (Amount)(Rs.)");
            table.Columns.Add("Amount Consumed(Rs.)");
            table.Columns.Add("Date of Issue");
            //table.Columns.Add("Certid");

            string strLastToken = "Test";
            DataRow dRow = null;
            // DataColumn column1

            using (JsonTextReader reader = new JsonTextReader(new StringReader(s)))
            {
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.StartObject:
                            // Console.Write("Start object: ");
                            break;
                        case JsonToken.StartArray:
                            // Console.Write("Start array: ");
                            break;
                        case JsonToken.PropertyName:
                            //Console.WriteLine(reader.Value.ToString());

                            strLastToken = reader.Value.ToString();
                            break;
                        case JsonToken.EndArray:
                            // Console.WriteLine("End array");
                            break;
                        case JsonToken.EndObject:
                            // Console.WriteLine("End object");
                            break;
                        case JsonToken.String:
                        case JsonToken.Integer:
                        case JsonToken.Null:
                        case JsonToken.Float:
                            // Console.WriteLine(reader.Value.ToString());


                            switch (strLastToken)
                            {
                                case "rowCount":
                                    strRowCount = reader.Value.ToString();
                                    break;

                                case "serialno":
                                    dRow = table.NewRow();
                                    dRow["Sr.No."] = reader.Value.ToString();
                                    break;
                                case "certino":
                                    dRow["Certificate Number"] = reader.Value.ToString();
                                    break;
                                case "finyear":
                                    dRow["Financial Year"] = reader.Value.ToString();
                                    break;
                                case "deducteepan":
                                    dRow["PAN of the Deductee"] = reader.Value.ToString();
                                    break;
                                case "dedname":
                                    dRow["Name of Deductee"] = reader.Value.ToString();
                                    break;
                                case "validfrm":
                                    dRow["Valid From"] = reader.Value.ToString();
                                    break;

                                case "validto":
                                    dRow["Valid To"] = reader.Value.ToString();
                                    break;

                                case "seccode":
                                    dRow["Section Code"] = Convert.ToString(reader.Value);
                                    //table.Rows.Add(dRow);
                                    break;

                                case "natofpayment":
                                    dRow["Nature of Payment"] = Convert.ToString(reader.Value);
                                    // table.Rows.Add(dRow);
                                    break;

                                case "rateoftds":
                                    dRow["Rate of TDS as per Certificate"] = Convert.ToString(reader.Value);
                                    // table.Rows.Add(dRow);
                                    break;

                                case "certilimit":
                                    dRow["Certificate Limit (Amount)(Rs.)"] = Convert.ToString(reader.Value);
                                    //    table.Rows.Add(dRow);
                                    break;

                                case "amtconsume":
                                    dRow["Amount Consumed(Rs.)"] = Convert.ToString(reader.Value);
                                    // table.Rows.Add(dRow);
                                    break;

                                case "issuedate":
                                    dRow["Date of Issue"] = Convert.ToString(reader.Value);
                                    table.Rows.Add(dRow);
                                    break;

                                    //case "certid":
                                    //    dRow["Certid"] = Convert.ToString(reader.Value);
                                    //    table.Rows.Add(dRow);
                                    //    break;


                            }
                            break;
                    }
                }
            }


            return table;

        }


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

        #region TraceViewStateData
        private Dictionary<string, string> TraceViewStateData(string strHTML, string xPathQuery)
        {
            Dictionary<string, string> objNameVal = new Dictionary<string, string>();
            HtmlNode.ElementsFlags.Remove("form");
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode.SelectSingleNode(xPathQuery);
            //-- Arup 2017/01/03
            if (node == null) return objNameVal;

            document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(node.InnerHtml);

            //if (node == null) return objNameVal;
            objNameVal.Clear();
            //---------------------------------------------------------------------------           
            if (node != null)
            {
                foreach (HtmlNode child in document.DocumentNode.SelectNodes("//input[@type='hidden']"))
                //foreach (HtmlNode child in node.SelectNodes("//input[@type='hidden']"))
                {

                    if (!objNameVal.ContainsKey(child.Attributes["name"].Value))
                        objNameVal.Add(child.Attributes["name"].Value, child.Attributes["value"].Value);
                }
            }
            return objNameVal;
        }

        #endregion

        #region RetrieveElementValue
        private string RetrieveElementValue(string strHTML, string xPathQuery, string strNode, enmElementType enmElement)
        {
            string objNameVal = "";

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode;
            HtmlNodeCollection hncHiddenField = node.SelectNodes(xPathQuery);

            if (hncHiddenField == null) return objNameVal;

            //---------------------------------------------------------------------------           
            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {
                foreach (HtmlNode child in hncHiddenField)
                {
                    HtmlNodeCollection childCollection = child.SelectNodes(strNode);

                    if (childCollection != null && childCollection.Count > 0)
                    {
                        for (int i = 0; i < childCollection.Count; i++)
                        {
                            switch (enmElement)
                            {
                                case enmElementType.InnerHTML:
                                    objNameVal = childCollection[i].InnerHtml;
                                    break;

                                case enmElementType.InnerText:
                                    objNameVal = childCollection[i].InnerText;
                                    break;

                                case enmElementType.Name:
                                    objNameVal = childCollection[i].Attributes["name"].Value;
                                    break;

                                case enmElementType.Value:
                                    objNameVal = childCollection[i].Attributes["value"].Value;
                                    break;
                            }


                        }

                    }
                }
            }
            return objNameVal;
        }

        #endregion

        #region HTMLTagAttributeValue
        string HTMLTagAttributeValue(string strHTML, string xPathQuery, string strAttName)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(strHTML);
            HtmlNode node = doc.DocumentNode.SelectNodes(xPathQuery)[0];
            string val = node.Attributes[strAttName].Value;
            return val;
        }

        #endregion

        #region TraceallInputFields
        private Dictionary<string, string> TraceallInputFields(string strHTML, string xPathQuery)
        {
            Dictionary<string, string> objNameVal = new Dictionary<string, string>();

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode;
            //---------------------------------------------------------------------------
            HtmlNodeCollection hncHiddenField = node.SelectNodes(xPathQuery);

            if (hncHiddenField == null) return objNameVal;
            objNameVal.Clear();
            //---------------------------------------------------------------------------           
            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {

                foreach (HtmlNode child in hncHiddenField)
                {
                    HtmlNodeCollection childCollection = child.SelectNodes("//input");

                    if (childCollection != null && childCollection.Count > 0)
                    {
                        for (int i = 0; i < childCollection.Count; i++)
                        {
                            //if (childCollection[i].Attributes["name"].Value = "javax.faces.ViewState")
                            //{
                            if (!objNameVal.ContainsKey(childCollection[i].Attributes["name"].Value))
                                objNameVal.Add(childCollection[i].Attributes["name"].Value, childCollection[i].Attributes["value"].Value);
                            //}
                        }

                    }
                }
            }

            return objNameVal;



        }

        #endregion

        #region AttributeValie
        private string AttributeValie(string strHTML, string strXPath, string strAttrib)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode.SelectSingleNode(strXPath);

            return node.Attributes[strAttrib].Value;
        }

        #endregion

        #region ProcessHtmlData

        private void ProcessHtmlData(string strHTML, ref DataTable dTable)
        {

            DataRow dRow = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(strHTML);

            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            if (tables != null)
            {
                HtmlNodeCollection rows = tables[0].SelectNodes(".//tr[@class='tabledetails']");
                for (int i = 0; i < rows.Count; ++i)
                {
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                    if (cols != null)
                    {
                        dRow = dTable.NewRow();
                        // dRow["FA Year Code"] = strFACode;
                        // dRow["Quarter Code"] = QtrCode;
                        // dRow["FA Year"] = strFAYear;
                        dRow["ReceiptNumber"] = Convert.ToString(cols[3].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["DDOSerialNo"] = Convert.ToString(cols[4].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Date"] = Convert.ToString(cols[5].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");

                        dTable.Rows.Add(dRow);
                    }
                }
            }
            ///return dTable;
        }
        #endregion

        #region getInnerText



        //private enmChallanStatus getInnerText(string html, string strQuery)
        //{
        //    enmChallanStatus Status = enmChallanStatus.RECORD_NOT_FOUND;
        //    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        //    htmlDoc.LoadHtml(html);
        //    HtmlNodeCollection tdOfInterests = htmlDoc.DocumentNode.SelectNodes("//tr[td/input[@name=\"" + strQuery + "\"]]/following-sibling::tr[position() <= 1]/td");

        //    if (tdOfInterests == null)
        //        return enmChallanStatus.RECORD_NOT_FOUND;

        //    foreach (HtmlNode td in tdOfInterests)
        //    {
        //        if (td.InnerText.ToUpper().Trim() == "AMOUNT MATCHED")
        //        {
        //            Status = enmChallanStatus.AMOUNT_MATCHED;

        //        }
        //        else if (td.InnerText.ToUpper().Trim() == "AMOUNT NOT MATCHED")
        //            Status = enmChallanStatus.AMOUNT_NOT_MATCHED;

        //    }

        //    return Status;

        //}

        private enmChallanStatus getInnerText(string html, string strQuery)
        {
            enmChallanStatus Status = enmChallanStatus.RECORD_NOT_FOUND;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection tdOfInterests = htmlDoc.DocumentNode.SelectNodes(strQuery);

            if (tdOfInterests == null)
                return enmChallanStatus.RECORD_NOT_FOUND;

            foreach (HtmlNode td in tdOfInterests)
            {
                if (td.InnerText.ToUpper().Trim() == "AMOUNT MATCHED")
                {
                    Status = enmChallanStatus.AMOUNT_MATCHED;

                }
                else if (td.InnerText.ToUpper().Trim() == "MISMATCH IN AMOUNT")
                    Status = enmChallanStatus.AMOUNT_NOT_MATCHED;
                else if (td.InnerText.ToUpper().Trim() == "AMOUNT NOT MATCHED")
                    Status = enmChallanStatus.AMOUNT_NOT_MATCHED;

            }

            return Status;

        }
        #endregion


        #region DownloadCSI
        public TracesResponse DownloadCSI(string strPan,string Pwd,string strFromDate,string strTodate)
        {           
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //1.  Login to Incometax site ================================
                objResponse = makeLoginToIncomeTax(strPan, Pwd);

                if (objResponse.Respons == enmResponse.Failed) return objResponse;

                //string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //-- ANIK 2023/01/27
                string strData = "{\"header\":{\"formName\":\"mat-tab-label-0-3\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";

                //System.Threading.Thread.Sleep(50000);
                //Request for download csi details
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "PaymentAPI/auth/challan/downloadCSI", strData);

                Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
                //=======================================================
                if (objRoot.messages.Count > 0)
                {
                    foreach (Incometax.Message msg in objRoot.messages)
                    {
                        if (msg.type == "ERROR")
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                            //break;
                        }

                    }
                }
                //=======================================================
                objResponse.Respons = enmResponse.Success;
                objResponse.Message = objRoot.csiResponse;
                //=======================================================
                //  LOGOUT 
                strData = "{\"serviceName\":\"logoutService\",\"entity\":\""+ strPan +"\",\"userType\":\"TDS\"}";
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);

                //
            }
            catch(Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = "Server Error";
            }
            return objResponse;
        }
        #endregion

        #region DownloadCSI2025
        public TracesResponse DownloadCSI2025(string strPan, string Pwd, string strFromDate, string strTodate)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //1.  Login to Incometax site ================================
                objResponse = makeLoginToIncomeTax(strPan, Pwd);

                if (objResponse.Respons == enmResponse.Failed) return objResponse;

                //string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //-- ANIK 2023/01/27
                //string strData = "{\"header\":{\"formName\":\"mat-tab-label-0-3\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //{ "header":{ "formName":"PO-03-PYMNT"},"formData":{ "pan":"CALP08143C","fromDate":"2026-04-01","toDate":"2026-07-01","actType":"N","loggedInUserID":"CALP08143C","loggedInUserType":"TDS"} }
                string strData = $@"{{
                                    ""header"": {{
                                        ""formName"": ""PO-03-PYMNT""
                                    }},
                                    ""formData"": {{
                                        ""pan"": ""{strPan}"",
                                        ""fromDate"": ""{strFromDate}"",
                                        ""toDate"": ""{strTodate}"",
                                        ""actType"": ""N"",
                                        ""loggedInUserID"": ""{strPan}"",
                                        ""loggedInUserType"": ""TDS""
                                    }}
                                }}";
                //System.Threading.Thread.Sleep(50000);
                //Request for download csi details
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "PaymentAPI/auth/challan/downloadCSI", strData);

                Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
                //=======================================================
                if (objRoot.messages.Count > 0)
                {
                    foreach (Incometax.Message msg in objRoot.messages)
                    {
                        if (msg.type == "ERROR")
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                            //break;
                        }

                    }
                }
                //=======================================================
                objResponse.Respons = enmResponse.Success;
                objResponse.Message = objRoot.csiResponse;
                //=======================================================
                //  LOGOUT 
                strData = "{\"serviceName\":\"logoutService\",\"entity\":\"" + strPan + "\",\"userType\":\"TDS\"}";
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);

                //
            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = "Server Error";
            }
            return objResponse;
        }
        #endregion

        #region DownloadCSINoPassword
        public TracesResponse DownloadCSINoPassword(string strPan,  string strFromDate, string strTodate)
        {
            TracesResponse objResponse = new TracesResponse();

            try
            {
                //1.  Login to Incometax site ================================
                //objResponse = makeLoginToIncomeTax(strPan, Pwd);

                if (objResponse.Respons == enmResponse.Failed) return objResponse;

                //string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                //-- ANIK 2023/01/27
                //string strData = "{\"header\":{\"formName\":\"mat-tab-label-0-3\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";
                string strData = "{\"header\":{\"formName\":\"PO-03-PYMNT\"},\"formData\":{\"pan\":\"" + strPan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"loggedInUserID\":\"" + strPan + "\",\"loggedInUserType\":\"TDS\"}}";

                //Request for download csi details
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "paymentAPI/auth/challan/downloadCSI", strData);

                Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
                //=======================================================
                if (objRoot.messages.Count > 0)
                {
                    foreach (Incometax.Message msg in objRoot.messages)
                    {
                        if (msg.type == "ERROR")
                        {
                            objResponse.Respons = enmResponse.Failed;
                            objResponse.Message = msg.desc;
                            return objResponse;
                            //break;
                        }

                    }
                }
                //=======================================================
                objResponse.Respons = enmResponse.Success;
                objResponse.Message = objRoot.csiResponse;
                //=======================================================
                //  LOGOUT 
                strData = "{\"serviceName\":\"logoutService\",\"entity\":\"" + strPan + "\",\"userType\":\"TDS\"}";
                strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "loginapi/login", strData);

                //
            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = "Server Error";
            }
            return objResponse;
        }
        #endregion


        #region DownloadCSIOTP
        public TracesResponse DownloadCSIOTP(string strTan, string MobileNo)
        {
            strServerResponse = makeHTTPGetRequest(strBaseURL + "foservices/#/download-csi-file/tan-user-details");
            //
            TracesResponse objResponse = new TracesResponse();
            string strData = "{\"tnNum\":\"" + strTan + "\",\"mbl\":\"" + MobileNo + "\",\"areaCd\":\"91\",\"name\":\"tan\",\"serviceName\":\"knowYourTanService\",\"formName\":\"PO-03-PYMNT\"}";
            //Request for download csi details
            strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "guestservicesapi/saveEntity", strData);
            Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
            //=======================================================
            if (objRoot.messages.Count > 0)
            {
                foreach (Incometax.Message msg in objRoot.messages)
                {
                    if (msg.type == "ERROR")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = msg.desc;
                        return objResponse;
                        //break;
                    }

                }
            }
            RefId = objRoot.userId;
            ReQId = objRoot.transactionNo;
            //=======================================================
            objResponse.Respons = enmResponse.Success;
            objResponse.Message = objRoot.csiResponse;
            //=======================================================
            return objResponse;
        }
        #endregion

        #region LoginForPayTaxUsingOTP
        //-- ANIK
        public TracesResponse LoginForPayTaxUsingOTP(string strTan, string MobileNo)
        {
            //-- https://eportal.incometax.gov.in/iec/foservices/#/e-pay-tax-prelogin/user-details
            strServerResponse = makeHTTPGetRequest(strBaseURL + "foservices/#/e-pay-tax-prelogin/user-details");
            //
            TracesResponse objResponse = new TracesResponse();
            //string strData = "{\"tnNum\":\"" + strTan + "\",\"mbl\":\"" + MobileNo + "\",\"areaCd\":\"91\",\"name\":\"tan\",\"serviceName\":\"knowYourTanService\",\"formName\":\"PO-03-PYMNT\"}";
            string strData = "{\"tnNum\":\"" + strTan + "\",\"mbl\":\"" + MobileNo + "\",\"areaCd\":\"91\",\"name\":\"tan\",\"serviceName\":\"knowYourTanService\",\"formName\":\"PO-03-PYMNT\"}";
            //Request for download csi details
            strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "guestservicesapi/saveEntity", strData);
            Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
            //=======================================================
            if (objRoot.messages.Count > 0)
            {
                foreach (Incometax.Message msg in objRoot.messages)
                {
                    if (msg.type == "ERROR")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = msg.desc;
                        return objResponse;
                        //break;
                    }

                }
            }
            RefId = objRoot.userId;
            ReQId = objRoot.transactionNo;
            //=======================================================
            objResponse.Respons = enmResponse.Success;
            objResponse.Message = objRoot.csiResponse;
            //=======================================================
            return objResponse;
        }
        #endregion

        #region DownloadCSIOTPValidation
        public TracesResponse DownloadCSIOTPValidation(string strOTP, string strTan)
        {
            TracesResponse objResponse = new TracesResponse();
            string strData = "{\"tnNum\":\"" + strTan + "\",\"serviceName\":\"knowYourTanService\",\"refId\":\"" + RefId + "\",\"otpValue\":\"" + strOTP + "\",\"formName\":\"PO-03-PYMNT\",\"reqId\":\"" + ReQId + "\"}";

            //Request for download csi details
            strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "paymentapi/commapi/validateOtp", strData);
            Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
            //=======================================================
            if (objRoot.messages.Count > 0)
            {
                foreach (Incometax.Message msg in objRoot.messages)
                {
                    if (msg.type == "ERROR")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = msg.desc;
                        return objResponse;
                        //break;
                    }

                }
            }
            //=======================================================
            objResponse.Respons = enmResponse.Success;
            objResponse.Message = objRoot.reqId;
            //=======================================================
            return objResponse;
        }
        #endregion

        #region DownloadCSIFileByOTP
        public TracesResponse DownloadCSIFileByOTP(string strTan, string strFromDate, string strTodate)
        {
            TracesResponse objResponse = new TracesResponse();

            string strData = "{\"formData\":{\"pan\":\"" + strTan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\"},\"header\":{\"reqId\":\"" + ReQId + "\"}}";

            //Request for download csi details
            strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "paymentapi/challan/downloadCSI", strData);
            Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
            //=======================================================
            if (objRoot.messages.Count > 0)
            {
                foreach (Incometax.Message msg in objRoot.messages)
                {
                    if (msg.type == "ERROR")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = msg.desc;
                        return objResponse;
                        //break;
                    }

                }
            }
            //=======================================================
            objResponse.Respons = enmResponse.Success;
            objResponse.Message = objRoot.csiResponse;
            //=======================================================

            return objResponse;

        }
        #endregion

        #region DownloadCSIFileByOTP2025
        public TracesResponse DownloadCSIFileByOTP2025(string strTan, string strFromDate, string strTodate)
        {
            TracesResponse objResponse = new TracesResponse();

            //string strData = "{\"formData\":{\"pan\":\"" + strTan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\"},\"header\":{\"reqId\":\"" + ReQId + "\"}}";
            string strData = "{\"formData\":{\"pan\":\"" + strTan + "\",\"fromDate\":\"" + strFromDate + "\",\"toDate\":\"" + strTodate + "\",\"actType\":\"N\"},\"header\":{\"reqId\":\"" + ReQId + "\"}}";
            //Request for download csi details
            strServerResponse = makeHTTPJSONPostRequest(strBaseURL + "paymentapi/challan/downloadCSI", strData);
            Incometax.Root objRoot = JsonConvert.DeserializeObject<Incometax.Root>(strServerResponse);
            //=======================================================
            if (objRoot.messages.Count > 0)
            {
                foreach (Incometax.Message msg in objRoot.messages)
                {
                    if (msg.type == "ERROR")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = msg.desc;
                        return objResponse;
                        //break;
                    }

                }
            }
            //=======================================================
            objResponse.Respons = enmResponse.Success;
            objResponse.Message = objRoot.csiResponse;
            //=======================================================

            return objResponse;

        }
        #endregion


        #region IncomeTaxPostJSON

        private string IncomeTaxPostJSON(
            string url,
            string json,
            string sn)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;

            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);

            request.Method =
                "POST";

            request.KeepAlive =
                true;

            request.CookieContainer =
                IncomeTaxCookieJar;

            request.Accept =
                "application/json, text/plain, */*";

            request.ContentType =
                "application/json";

            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
                + "AppleWebKit/537.36 (KHTML, like Gecko) "
                + "Chrome/151.0.0.0 Safari/537.36";

            request.Headers.Add(
                "Accept-Language",
                "en-US,en;q=0.9");

            request.Headers.Add(
                "Origin",
                INCOME_TAX_BASE_URL);

            request.Referer =
                INCOME_TAX_BASE_URL
                + "/iec/foservices/";

            if (!string.IsNullOrWhiteSpace(sn))
            {
                request.Headers.Add(
                    "sn",
                    sn);
            }

            request.Timeout =
                60000;

            byte[] buffer =
                Encoding.UTF8.GetBytes(json);

            request.ContentLength =
                buffer.Length;

            using (Stream stream =
                request.GetRequestStream())
            {
                stream.Write(
                    buffer,
                    0,
                    buffer.Length);
            }

            using (HttpWebResponse response =
                (HttpWebResponse)request.GetResponse())
            {
                if (response.Cookies != null)
                {
                    try
                    {
                        IncomeTaxCookieJar.Add(
                            response.Cookies);
                    }
                    catch
                    {
                    }
                }

                using (StreamReader reader =
                    new StreamReader(
                        response.GetResponseStream(),
                        Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion


        //===========================================================
        // BTOA
        //===========================================================

        #region IncomeTaxBtoa

        private string IncomeTaxBtoa(
            string value)
        {
            if (value == null)
            {
                value = "";
            }

            Encoding encoding =
                Encoding.GetEncoding(
                    "ISO-8859-1");

            byte[] bytes =
                encoding.GetBytes(value);

            return Convert.ToBase64String(
                bytes);
        }

        #endregion


        //===========================================================
        // LOGIN
        //===========================================================

        #region MakeLoginToIncomeTaxPortal

        public TracesResponse MakeLoginToIncomeTaxPortal(
            TracesLogin objLogin)
        {
            TracesResponse objResponse =
                new TracesResponse();

            try
            {
                //---------------------------------------------------
                // VALIDATION
                //---------------------------------------------------

                if (objLogin == null)
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Income Tax login information is not available.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(
                    objLogin.TAN))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "TAN/User ID is not available.";

                    return objResponse;
                }

                if (string.IsNullOrWhiteSpace(
                    objLogin.Password))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "Income Tax portal password is not available.";

                    return objResponse;
                }

                string strUserID =
                    objLogin.TAN.Trim();


                //---------------------------------------------------
                // START FRESH SESSION
                //---------------------------------------------------

                ResetSession();

                //---------------------------------------------------
                // INITIALIZE PORTAL SESSION
                //---------------------------------------------------

                InitializeIncomeTaxSession();


                //---------------------------------------------------
                // STEP 1 : USER ID VALIDATION
                //---------------------------------------------------

                string strLoginURL =
                    INCOME_TAX_BASE_URL
                    + "/iec/loginapi/login";

                JObject objStep1Payload =
                    new JObject();

                objStep1Payload["entity"] =
                    strUserID;

                objStep1Payload["serviceName"] =
                    "wLoginService";

                string strStep1Response =
                    IncomeTaxPostJSON(
                        strLoginURL,
                        objStep1Payload.ToString(
                            Formatting.None),
                        null);


                //---------------------------------------------------
                // CHECK STEP 1
                //---------------------------------------------------

                if (string.IsNullOrWhiteSpace(
                    strStep1Response))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "No response received while validating Income Tax User ID.";

                    return objResponse;
                }

                JObject objStep1 =
                    JObject.Parse(
                        strStep1Response);


                //---------------------------------------------------
                // REQUEST ID
                //---------------------------------------------------

                string strReqID =
                    Convert.ToString(
                        objStep1["reqId"]);

                if (string.IsNullOrWhiteSpace(
                    strReqID))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        GetIncomeTaxMessage(
                            objStep1,
                            "Request ID was not returned by Income Tax portal.");

                    return objResponse;
                }


                //---------------------------------------------------
                // VALUES RETURNED BY PORTAL
                //---------------------------------------------------

                string strRole =
                    Convert.ToString(
                        objStep1["role"]);

                string strEntityType =
                    Convert.ToString(
                        objStep1["entityType"]);

                if (string.IsNullOrWhiteSpace(
                    strRole))
                {
                    strRole =
                        "TDS";
                }

                if (string.IsNullOrWhiteSpace(
                    strEntityType))
                {
                    strEntityType =
                        "User ID";
                }


                //---------------------------------------------------
                // PASSWORD
                //---------------------------------------------------

                string strEncodedPassword =
                    IncomeTaxBtoa(
                        objLogin.Password);


                //---------------------------------------------------
                // STEP 2 : PASSWORD LOGIN
                //---------------------------------------------------

                JObject objStep2Payload =
                    new JObject();

                objStep2Payload["errors"] =
                    new JArray();

                objStep2Payload["reqId"] =
                    strReqID;

                objStep2Payload["entity"] =
                    strUserID;

                objStep2Payload["entityType"] =
                    strEntityType;

                objStep2Payload["role"] =
                    strRole;

                objStep2Payload["uidValdtnFlg"] =
                    "true";

                objStep2Payload["aadhaarMobileValidated"] =
                    "false";

                objStep2Payload["secAccssMsg"] =
                    "";

                objStep2Payload["secLoginOptions"] =
                    "";

                objStep2Payload["dtoService"] =
                    "LOGIN";

                objStep2Payload["exemptedPan"] =
                    "false";

                objStep2Payload["userConsent"] =
                    "";

                objStep2Payload["imgByte"] =
                    null;

                objStep2Payload["pass"] =
                    strEncodedPassword;

                objStep2Payload["passValdtnFlg"] =
                    null;

                objStep2Payload["otpGenerationFlag"] =
                    null;

                objStep2Payload["otp"] =
                    null;

                objStep2Payload["otpValdtnFlg"] =
                    null;

                objStep2Payload["otpSourceFlag"] =
                    null;

                objStep2Payload["contactPan"] =
                    null;

                objStep2Payload["contactMobile"] =
                    null;

                objStep2Payload["contactEmail"] =
                    null;

                objStep2Payload["email"] =
                    null;

                objStep2Payload["mobileNo"] =
                    null;

                objStep2Payload["forgnDirEmailId"] =
                    null;

                objStep2Payload["imagePath"] =
                    null;

                objStep2Payload["serviceName"] =
                    "loginService";


                string strStep2Response =
                    IncomeTaxPostJSON(
                        strLoginURL,
                        objStep2Payload.ToString(
                            Formatting.None),
                        "dashboardMenuService");


                //---------------------------------------------------
                // PARSE LOGIN RESPONSE
                //---------------------------------------------------

                if (string.IsNullOrWhiteSpace(
                    strStep2Response))
                {
                    objResponse.Respons =
                        enmResponse.Failed;

                    objResponse.Message =
                        "No response received while logging in to Income Tax portal.";

                    return objResponse;
                }

                JObject objStep2 =
                    JObject.Parse(
                        strStep2Response);


                //---------------------------------------------------
                // MESSAGE
                //---------------------------------------------------

                string strCode =
                    "";

                string strDescription =
                    "";

                JArray arrMessages =
                    objStep2["messages"]
                    as JArray;

                if (arrMessages != null &&
                    arrMessages.Count > 0)
                {
                    strCode =
                        Convert.ToString(
                            arrMessages[0]["code"]);

                    strDescription =
                        Convert.ToString(
                            arrMessages[0]["desc"]);
                }


                //---------------------------------------------------
                // SUCCESS
                //---------------------------------------------------

                if (strCode ==
                    "EF00000")
                {
                    IncomeTaxSessionExists =
                        true;

                    IncomeTaxUserID =
                        strUserID;

                    objResponse.Respons =
                        enmResponse.Success;

                    objResponse.Message =
                        "Income Tax portal login successful.";

                    objResponse.Data =
                        strStep2Response;

                    return objResponse;
                }


                //---------------------------------------------------
                // FAILED
                //---------------------------------------------------

                objResponse.Respons =
                    enmResponse.Failed;

                if (!string.IsNullOrWhiteSpace(
                    strDescription))
                {
                    objResponse.Message =
                        strDescription;
                }
                else
                {
                    objResponse.Message =
                        "Income Tax portal login failed."
                        + Environment.NewLine
                        + Environment.NewLine
                        + strStep2Response;
                }

                return objResponse;
            }
            catch (WebException webEx)
            {
                IncomeTaxSessionExists =
                    false;

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
                            string strServerResponse =
                                sr.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(
                                strServerResponse))
                            {
                                strError +=
                                    Environment.NewLine
                                    + Environment.NewLine
                                    + "Income Tax Response:"
                                    + Environment.NewLine
                                    + strServerResponse;
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
                IncomeTaxSessionExists =
                    false;

                objResponse.Respons =
                    enmResponse.Failed;

                objResponse.Message =
                    ex.Message;

                return objResponse;
            }
        }

        #endregion

        #region InitializeIncomeTaxSession

        private void InitializeIncomeTaxSession()
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;

            string url =
                INCOME_TAX_BASE_URL
                + "/iec/foservices/";

            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.KeepAlive = true;

            // SAME COOKIE CONTAINER USED BY LOGIN
            request.CookieContainer =
                IncomeTaxCookieJar;

            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
                + "AppleWebKit/537.36 (KHTML, like Gecko) "
                + "Chrome/151.0.0.0 Safari/537.36";

            request.Accept =
                "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            request.Headers.Add(
                "Accept-Language",
                "en-US,en;q=0.9");

            request.Timeout = 60000;

            using (HttpWebResponse response =
                (HttpWebResponse)request.GetResponse())
            {
                if (response.Cookies != null)
                {
                    try
                    {
                        IncomeTaxCookieJar.Add(
                            response.Cookies);
                    }
                    catch
                    {
                    }
                }

                // IMPORTANT:
                // Read response completely.
                using (StreamReader sr =
                    new StreamReader(
                        response.GetResponseStream(),
                        Encoding.UTF8))
                {
                    sr.ReadToEnd();
                }
            }
        }

        #endregion

        //===========================================================
        // MESSAGE HELPER
        //===========================================================

        #region GetIncomeTaxMessage

        private string GetIncomeTaxMessage(
            JObject objJSON,
            string defaultMessage)
        {
            try
            {
                if (objJSON == null)
                {
                    return defaultMessage;
                }

                JArray arrMessages =
                    objJSON["messages"]
                    as JArray;

                if (arrMessages != null &&
                    arrMessages.Count > 0)
                {
                    string strCode =
                        Convert.ToString(
                            arrMessages[0]["code"]);

                    string strDescription =
                        Convert.ToString(
                            arrMessages[0]["desc"]);

                    if (!string.IsNullOrWhiteSpace(
                        strDescription))
                    {
                        return strDescription;
                    }

                    if (!string.IsNullOrWhiteSpace(
                        strCode))
                    {
                        return defaultMessage
                            + Environment.NewLine
                            + "Code: "
                            + strCode;
                    }
                }
            }
            catch
            {
            }

            return defaultMessage;
        }

        #endregion

    }

}

namespace TDSMAN.Classes.Incometax
{

    public class Header
    {
        public object formName { get; set; }
    }

    public class Message
    {
        public string code { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public object fieldName { get; set; }
    }

    public class Root
    {
        public Header header { get; set; }
        public List<Message> messages { get; set; }
        public List<object> errors { get; set; }
        public string reqId { get; set; }
        public string entity { get; set; }
        public string entityType { get; set; }
        public string role { get; set; }
        public string userType { get; set; }
        public string uidValdtnFlg { get; set; }
        public string passValdtnFlg { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string aadhaarMobileValidated { get; set; }
        public string secAccssMsg { get; set; }
        public string secLoginOptions { get; set; }
        public string contactPan { get; set; }
        public string contactEmail { get; set; }
        public string contactMobile { get; set; }
        public string lastLoginSuccessFlag { get; set; }
        public string clientIp { get; set; }
        public string exemptedPan { get; set; }
        public string userConsent { get; set; }
        public string imgByte { get; set; }

       public string csiResponse { get; set; }



        public string userId { get; set; }
        public string isActive { get; set; }
        public string transactionNo { get; set; }
        public string type { get; set; }


        public string RefId { get; set; }
    }
}