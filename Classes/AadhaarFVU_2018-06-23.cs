

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
using System.Net.Sockets;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Web;

#endregion

namespace TDSMAN.Classes
{


    public class FVUSubmissionByAdhar
    {

        #region Variable Declaration


        string strURL = "";

        //LAVEL 1. HTTP REQUEST WITH LOGIN PAGE ONLY FOR GETTING SESSION VALUES.   

        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;

        string strServerResponse = "";
        string strServerHtml="";
        CookieContainer objContainer = new CookieContainer();
        List<ErrorDB<int, string, string>> objMsgDictionary = new List<ErrorDB<int, string, string>>();

        //string strBaseURL = "https://incometaxindiaefiling.gov.in/e-Filing/";
        string strBaseURL = "https://portal.incometaxindiaefiling.gov.in/e-Filing/";
        //string strCaptchURL = "https://incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do";
        string strCaptchURL = "https://portal.incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do";

        string strUploadLink = "";
        string strViewTDSLink = "";

        private bool bnlSessionExists = false;

        enum enmElementType
        {
            InnerText,
            InnerHTML,
            Value,
            Name
        }


        #endregion

        #region IsSessionExists
        public bool IsSessionExists
        {
            get { return bnlSessionExists; }
            set { bnlSessionExists = true; }
        }

        #endregion

        #region FVUSubmissionByAdhar
        public FVUSubmissionByAdhar()
        {

        }

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

        #region IsServerError
        private TracesResponse IsServerError(string strServerResponse, List<string> strXQuery)
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

            foreach (string strVal in strXQuery)
            {
                HtmlNodeCollection error = node.SelectNodes(strVal);

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
            }
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



        #region makeLogin

        public TracesResponse makeLogin(string UserID, string Pwd, string strCaptcha)
        {
            TracesResponse objResponse = new TracesResponse();

            //CREATE DATA FOR LOGIN FORM
            StringBuilder sbParameter = new StringBuilder();
            try
            {
                sbParameter.Append("hindi=");
                sbParameter.Append("&requestId=");
                sbParameter.Append("&nextPage=");
                sbParameter.Append("&userName=" + HttpUtility.UrlEncode(UserID));
                sbParameter.Append("&userPan=");
                sbParameter.Append("&password=" + HttpUtility.UrlEncode(Pwd));
                sbParameter.Append("&dob=");
                sbParameter.Append("&rsaToken=");
                sbParameter.Append("&captchaCode=" + strCaptcha);

                //MAKE REQUEST TO LOGIN FORM
                //strServerResponse = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/UserLogin/Login.html", sbParameter);
                strServerResponse = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in/e-Filing/UserLogin/Login.html", sbParameter);

                if (string.IsNullOrEmpty(strServerResponse))
                {
                    IsSessionExists = false;
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Login Failed or Server Error";
                    return objResponse;
                }

                //CHECKING ANY ERROR FROM SERVER
                //objResponse = IsServerError(strServerResponse, "//ul[@class=\"nonclickBulletedList\"]");

                //CHECKING ANY ERROR FROM SERVER
                //objResponse = IsServerError(strServerResponse, "//div[@errorFor=\"Login_captchaCode\"]");

                //if (objResponse.Respons == enmResponse.Failed)
                //{
                //    IsSessionExists = false;
                //    //objResponse.ErrorMessage = "Login Failed or Server Error";
                //    return objResponse;
                //}

                if (IsConditionMatch(strServerResponse, "User ID or Password is invalid."))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "User ID or Password is invalid.";
                    return objResponse;
                }


                if (IsConditionMatch(strServerResponse, "Invalid Code. Please enter the code as appearing in the Image"))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Invalid Captcha Code";
                    return objResponse;
                }


                if (IsConditionMatch(strServerResponse, "You are already logged in"))
                {
                    sbParameter.Clear();
                    sbParameter.Append("requestId=");
                    sbParameter.Append("&nextPage=");
                    sbParameter.Append("&buttonType=" + HttpUtility.UrlEncode("Forced Login"));

                    //strServerResponse = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/UserLogin/ForcedLogin.html", sbParameter);
                    strServerResponse = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in/e-Filing/UserLogin/ForcedLogin.html", sbParameter);
                }

                if (!IsConditionMatch(strServerResponse, "Last Login:") )
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Login Failed or Server Error";
                    return objResponse;
                }

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(strServerResponse);
                    var aTags = doc.DocumentNode.SelectNodes("//a[contains(text(), 'Upload TDS')]");
                    //------------------------------------------------------------------------------                   
                    if (aTags != null)
                        foreach (var aTag in aTags)
                        {
                            strUploadLink = aTag.Attributes["href"].Value;
                        }

                    var aTags2 = doc.DocumentNode.SelectNodes("//a[contains(text(), 'View Filed TDS')]");
                    //------------------------------------------------------------------------------                   
                    if (aTags2 != null)
                        foreach (var aTag in aTags2)
                        {
                            strViewTDSLink = aTag.Attributes["href"].Value;
                        }
                    //----------------------------------------------------------
                    IsSessionExists = true;
                    objResponse.Respons = enmResponse.Success;
                    objResponse.Message = "";
                    //-----------------------------------------------------------
                    return objResponse;
                



            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
                return objResponse;
            }

        }

        #endregion

        #region makeLogoff
        public TracesResponse Logoff()
        {
            TracesResponse objRespoce = new TracesResponse();

            try
            {
                this.makeHTTPGetRequest(strBaseURL + "logout.xhtml");
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

        #region Logoff_bak
        public TracesResponse Logoff_bak()
        {
            TracesResponse objRespoce = new TracesResponse();

            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "/logout.xhtml");
                //-------------------------------------
                request.CookieContainer = objContainer;
                request.KeepAlive = true;
                //-------------------------------------
                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                //--------------------------------------
                strServerResponse = reader.ReadToEnd();
                //----------------------------------------
                reader.Close();
                dataStream.Close();
                response.Close();
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

            request = (HttpWebRequest)WebRequest.Create(strURL);
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            //request.KeepAlive = false;
            request.KeepAlive = true;
            // ServicePointManager.Expect100Continue = false;
            //ServicePointManager.MaxServicePointIdleTime = 2000;
            //----------------------------------------------------------
            // CREATE POST DATA AND CONVERT IT TO A BYTE ARRAY.
            //----------------------------------------------------------   
            //request.Headers.GetType().InvokeMember("ChangeInternal",
            //    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null,
            //    request.Headers, new object[] { "Host", "www.tdscpc.gov.in" });

            //request.Headers.GetType().InvokeMember("ChangeInternal",
            //     BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null,
            //     request.Headers, new object[] { "Connection", "keep-alive" });



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
                //request.Headers.Add("Cache-Control: max-age=0");
                //request.Headers.Add("Host:www.tdscpc.gov.in");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                // request.Headers.Add("Origin: https://www.tdscpc.gov.in");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";

                //  request.Headers.Add("Accept-Encoding: gzip,deflate,sdch");
                //request.Headers.Add("Accept-Language: en-US,en;q=0.8");
                //request.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.3");

                //  request.ProtocolVersion = HttpVersion.Version10;
                // request.AllowAutoRedirect = true;

                //// request.Timeout = 10000;
                request.Timeout = 1000000000; // fix 3

            }

            //----------------------------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;
            //-- ANIK @ 2015/12/03
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

            if (response != null)
                request.CookieContainer.Add(response.Cookies);


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

        #region makeHTTPGetRequest
        private string makeHTTPGetRequest(string strURL)
        {
            //    SetAllowUnsafeHeaderParsing();

            //   ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

            request = (HttpWebRequest)HttpWebRequest.Create(strURL);
            //-------------------------------------
            // request.CookieContainer = objContainer;
            //request.KeepAlive = false;
            request.KeepAlive = true;
            request.Method = "GET";

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 1000000000; // fix 3
            //  request.Headers.GetType().InvokeMember("ChangeInternal",
            //BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
            //Type.DefaultBinder, request.Headers, new object[] { "Connection", "Keep-Alive" });
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



        #region IsConnected
        private HttpStatusCode IsConnected(string strURL)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strURL);
                webRequest.AllowAutoRedirect = false;
                response = (HttpWebResponse)webRequest.GetResponse();
                //Returns "MovedPermanently", not 301 which is what I want.
                return response.StatusCode;

            }
            catch
            {
                return HttpStatusCode.Forbidden;
            }

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

        #region TraceHiddenField
        private Dictionary<string, string> TraceHiddenField(string strHTML, string xPathQuery)
        {
            Dictionary<string, string> objNameVal = new Dictionary<string, string>();

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode;
            //---------------------------------------------------------------------------
            //HtmlNodeCollection dds = node.SelectNodes("//dd"); //Select all dd tags
            //HtmlNodeCollection anchors = node.SelectNodes("//b/a[@href]"); //Select all 'a' tags that contais href attribute err_Summary
            //HtmlNodeCollection error = node.SelectNodes("//span[@id=\"err_Summary\"]");
            //HtmlNodeCollection doub = node.SelectNodes("//span[@id=\"err_Summary\"] | //input[@type='hidden']");
            //---------------------------------------------------------------------------
            // HtmlNodeCollection hncHiddenField = node.SelectNodes("/form[@id=\"requestnsdlconsoForm\"]/input[@type='hidden']");
            //HtmlNodeCollection hncHiddenField = node.SelectNodes("//form[@id=\"requestnsdlconsoForm\"]");
            HtmlNodeCollection hncHiddenField = node.SelectNodes(xPathQuery);

            if (hncHiddenField == null) return objNameVal;
            objNameVal.Clear();
            //---------------------------------------------------------------------------           
            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {

                foreach (HtmlNode child in hncHiddenField)
                {
                    //HtmlNodeCollection childCollection = child.SelectNodes("//input[@type='hidden']");
                    HtmlNodeCollection childCollection = child.SelectNodes("//INPUT[@TYPE='HIDDEN']");

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


                    //if (!objNameVal.ContainsKey(hncHiddenField[i].Attributes["name"].Value))
                    //    objNameVal.Add(hncHiddenField[i].Attributes["name"].Value, hncHiddenField[i].Attributes["value"].Value);
                    //string strval = hidden[i].GetAttributeValue("href", atributteValue);
                    //string strval1 = hncHiddenField[i].Attributes["value"].Value;
                    //string strval2 = hncHiddenField[i].Attributes["name"].Value;

                    //string atributteValue = null,
                    //Text = error[i].InnerText,
                    //Url = error[i].GetAttributeValue("href", atributteValue),
                    //AnchorText = anchors[i].InnerText;
                }
            }

            //for (int i = 0; i < error.Count; i++)
            //{
            //    string atributteValue = null,
            //    Text = error[i].InnerText,
            //    Url = error[i].GetAttributeValue("href", atributteValue),
            //    AnchorText = anchors[i].InnerText;               
            //}



            return objNameVal;



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



        #region MakeInitialRequest
        public Stream MakeInitialRequest()
        {
            //makeHTTPGetRequest("https://incometaxindiaefiling.gov.in/e-Filing/UserLogin/LoginHome.html");
            makeHTTPGetRequest("https://portal.incometaxindiaefiling.gov.in/e-Filing/UserLogin/LoginHome.html");

            return GetCaptcha();


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

        #region GetCaptcha
        public Stream GetCaptcha()
        {
            try
            {
                //request = (HttpWebRequest)WebRequest.Create("https://incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do");
                //-- ANIK @ 2018/03/26
                request = (HttpWebRequest)WebRequest.Create("https://portal.incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do");
                request.Method = "GET";
                request.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
                request.ContentType = "text/html; charset=utf-8";

                request.KeepAlive = true;
                request.CookieContainer = objContainer;

                return request.GetResponse().GetResponseStream();

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion

        #region getOTPfromIncomeTaxSite

        public TracesResponse getOTPfromIncomeTaxSite(ParamAdhar paramData)
        {
            TracesResponse respo = new TracesResponse();
            if (paramData == null)
            {
                respo.Message = "Parameter Required";
                respo.Respons = enmResponse.Failed;
                return respo;
            }
            //---------------------------------------------

            StringBuilder ss = new StringBuilder();
            //strServerResponse = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in" + strUploadLink, ss);
            strServerResponse = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in" + strUploadLink, ss);

            // strServerResponse = makeHTTPGetRequest("https://incometaxindiaefiling.gov.in" + strUploadLink);
            //  strServerResponse = makeHTTPGetRequest(strBaseURL + "Services/UploadTdsLink.html");

            if (!IsStringExists(strServerResponse, "//form[@id=\"UploadTdsParamValidate\"]"))
            {
                respo.Message = "";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }

            StringBuilder sbParameter = new StringBuilder();
            sbParameter.Append("dedtrTan=" + paramData.TanNo);
            sbParameter.Append("&stmtParamater.fvuVersion=" + paramData.FVUVersion);
            sbParameter.Append("&stmtParamater.finYr=" + paramData.FAYear);
            sbParameter.Append("&stmtParamater.formName=" + paramData.Forms);

            sbParameter.Append("&stmtParamater.period=" + paramData.Quarter);
            sbParameter.Append("&stmtParamater.uploadType=" + paramData.UploadType);

            sbParameter.Append("&stmtParamater.originalRrrNum=" + paramData.OriginalRRRNo);
            sbParameter.Append("&stmtParamater.previousRrrNum=" + paramData.PreviousRRRNo);
            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"UploadTdsParamValidate\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                //this.Logoff();
                respo.Message = "Server Error";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }
            //----------------------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            //--------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "Services/UploadTdsParamValidate.html", sbParameter);

            //-----------------------------------------------------------------------
            //- CHECKING ANY ERROR FROM SERVER SIDE  
            //-----------------------------------------------------------------------
            HtmlAgilityPack.HtmlDocument resultStat = new HtmlAgilityPack.HtmlDocument();
            resultStat.LoadHtml(strServerResponse);

            //  var val = resultStat.DocumentNode.SelectSingleNode("//div[@errorfor='UploadTdsParamValidate_dedtrTan']").InnerText;
            var val = resultStat.DocumentNode.SelectSingleNode("//div[@errorfor='UploadTdsParamValidate_dedtrTan']");

            if (val != null)
            {
                if (val.InnerText != "")
                {
                    respo.Message = val.InnerText.Replace("\r\n", "");
                    respo.Respons = enmResponse.Failed;
                    return respo;
                }
            }

            //-----------------------------------------------------------------------






            /* -----------------------------------------------------------------------
              RETRIEVE VIEWSTATE DATA                
              --------------------------------------------------------------------*/
            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"UploadTdsReturn\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
               // this.Logoff();
                respo.Message = "Server Error";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }
            //--------------------------------------------------------------------*/
            string strSessionID = "";
            foreach (KeyValuePair<string, string> pair in objNameval)
                if (pair.Key == "requestId")
                {
                    strSessionID = pair.Value;
                    break;
                }


            //strServerResponse = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/OnlineForms/GenerateAadhaarOtpForms.html?ID=" + strSessionID, ss);
            strServerResponse = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in/e-Filing/OnlineForms/GenerateAadhaarOtpForms.html?ID=" + strSessionID, ss);

            Match match = Regex.Match(strServerResponse, "Aadhaar OTP has been generated successfully", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                respo.Message = "Aadhaar OTP has been generated successfully";
                respo.Respons = enmResponse.Success;
                respo.CustomeTypes = strSessionID;
                //--------------------------------------------------------------------------------------
                return respo;
            }
            else //-- 2018/05/04
            {
                respo.Message = "Technical Error. Please try again";
                respo.Respons = enmResponse.Failed;

            }


            return respo;
        }

        #endregion

        #region UploadfvuWithAadhaar


        public TracesResponse UploadfvuWithAadhaar(string strOTP, string strReqID, string strFilePath, string strFileName)
        {
            TracesResponse respo = new TracesResponse();
            
            //---------------------------------------------
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("formNameEVC", "#UploadTdsReturn");
            nvc.Add("requestId", strReqID);
            nvc.Add("aadhaarSeeded", "Y");
            nvc.Add("evc", "");
            nvc.Add("aadhaarOtp", strOTP);
            //----------------------------------------------------------------------
            //strServerResponse = HttpUploadFileWithAadhaar("https://incometaxindiaefiling.gov.in/e-Filing/Services/UploadTdsReturn.html",
            //                   strFilePath, strFileName, "application/zip", nvc);
            strServerResponse = HttpUploadFileWithAadhaar("https://portal.incometaxindiaefiling.gov.in/e-Filing/Services/UploadTdsReturn.html",
                               strFilePath, strFileName, "application/zip", nvc);
            //----------------------------------------------------------------------
            if (IsConditionMatch(strServerResponse, "Your TDS return have been uploaded successfully"))
            {
                HtmlAgilityPack.HtmlDocument result = new HtmlAgilityPack.HtmlDocument();
                result.LoadHtml(strServerResponse);

                var varText = result.DocumentNode.SelectSingleNode("//div[@class='whitebg mar5']");

                if (varText != null)
                {
                    if (varText.InnerText != "")
                    {
                        respo.Message = varText.InnerText.Replace("\r\n", " ").Replace("\t", " ").Replace("\n", " ");
                        respo.Respons = enmResponse.Success;
                        return respo;
                    }
                }
                else
                {
                    respo.Message = "Session Timeout. Please log in again";
                    respo.Respons = enmResponse.SessionTimeout;
                    return respo;
                }
            }
            //--------------------------------------------------------------------------
            //THIS SECTION FOR HANDLING UNSUCCESSFULL UPLOAD
            //--------------------------------------------------------------------------
            if (!IsStringExists(strServerResponse, "//form[@id=\"UploadTdsReturn\"]"))
            {
                respo.Message = "Session Timeout. Please log in again";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }
            //----------------------------------------------------------------------
            HtmlAgilityPack.HtmlDocument resultStat = new HtmlAgilityPack.HtmlDocument();
            resultStat.LoadHtml(strServerResponse);

            var val = resultStat.DocumentNode.SelectSingleNode("//ul[@class='nonclickBulletedList']");

            if (val != null)
            {
                if (val.InnerText != "")
                {
                    respo.Message = val.InnerText.Replace("\r\n", " ").Replace("\t", " ").Replace("\n", " ");
                    respo.Respons = enmResponse.Success;
                    return respo;
                }
            }
            else
            {
                respo.Message = "Server Error";
                respo.Respons = enmResponse.Failed;
                return respo;

            }

            return respo;
        }



        #endregion

        #region HttpUploadFile

        private string HttpUploadFileWithAadhaar(string url, string file, string FileName, string contentType, NameValueCollection nvc)
        {
            //log.Debug(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //request.KeepAlive = false;
            request.KeepAlive = true;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";

            request.ContentType = "multipart/form-data; boundary=" + boundary;

            // request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //----------------------------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;

            //if (response != null)
            //    request.CookieContainer.Add(response.Cookies);

            Stream rs = request.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "fileUploadBean.file", FileName, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = request.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);

                strServerResponse = reader2.ReadToEnd();

            }
            catch (Exception ex)
            {
                // log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                request = null;
            }

            return strServerResponse;
        }

        #endregion




        #region ViewFiledTDSIncomeTaxSite

        public TracesResponse ViewFiledTDSIncomeTaxSite(ParamAdhar paramData)
        {
            TracesResponse respo = new TracesResponse();
          
            if (paramData == null)
            {
                respo.Message = "Parameter Required";
                respo.Respons = enmResponse.Failed;
                return respo;
            }
            //---------------------------------------------

            StringBuilder ss = new StringBuilder();

            if (paramData.Quarter == "-1")
            {
                //strServerHtml = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in" + strViewTDSLink, ss);
                strServerHtml = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in" + strViewTDSLink, ss);

                if (!IsStringExists(strServerHtml, "//form[@id=\"ViewFiledTds\"]"))
                {
                    respo.Message = "";
                    respo.Respons = enmResponse.SessionTimeout;
                    return respo;
                }
            }

            //else
            //{
            //    HtmlAgilityPack.HtmlDocument hdoc = new HtmlAgilityPack.HtmlDocument();
            //    hdoc.LoadHtml(strServerResponse);
             
            //    var aTags2 = hdoc.DocumentNode.SelectNodes("//a[contains(text(), 'View Filed TDS')]");
            //    //------------------------------------------------------------------------------                   
            //    if (aTags2 != null)
            //        foreach (var aTag in aTags2)
            //        {
            //            strViewTDSLink = aTag.Attributes["href"].Value;
            //        }

            //}

            StringBuilder sbParameter = new StringBuilder();
            sbParameter.Append("pagniationParam.showAdvSearch=Y");
            sbParameter.Append("&dedtrTan=" + paramData.TanNo);
            sbParameter.Append("&searchCriteria.finYr=" + paramData.FAYear);
            sbParameter.Append("&searchCriteria.formName=" + paramData.Forms);
            sbParameter.Append("&searchCriteria.period=" + paramData.Quarter);
            sbParameter.Append("&searchCriteria.uploadType=" + paramData.UploadType);

            /* -----------------------------------------------------------------------
               RETRIEVE SESSION DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerHtml, "//form[@id=\"ViewFiledTds\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                //this.Logoff();
                respo.Message = "Server Error";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }
            //----------------------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            //--------------------------------------------------------------------*/
            strServerHtml = makeHTTPPostRequest(strBaseURL + "Services/ViewFiledTds.html", sbParameter);

            if (!IsStringExists(strServerHtml, "//form[@id=\"ViewFiledTds\"]"))
            {
                respo.Message = "";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }

            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            objNameval = TraceViewStateData(strServerHtml, "//form[@id=\"ViewFiledTds\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                this.Logoff();
                respo.Message = "Server Error";
                respo.Respons = enmResponse.SessionTimeout;
                return respo;
            }
            //--------------------------------------------------------------------*/
            string strSessionID = "";
            foreach (KeyValuePair<string, string> pair in objNameval)
                if (pair.Key == "ID")
                {
                    strSessionID = pair.Value;
                    break;
                }
            //-----------------------------------------------------------------------
            DataTable dTable = new DataTable();
            dTable.Columns.Add("ID");
            dTable.Columns.Add("S.No.");
            dTable.Columns.Add("Transaction No");
            dTable.Columns.Add("Tan No");
            dTable.Columns.Add("Form Name");
            dTable.Columns.Add("Finnancial Year");
            dTable.Columns.Add("Quarter");
            dTable.Columns.Add("Filed On");
            dTable.Columns.Add("Upload Type");
            dTable.Columns.Add("Token Number");
            dTable.Columns.Add("Status");
            //-------------------------------------------------
            DataRow dRow = null;
            //-------------------------------------------------
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(strServerHtml);

            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            if (tables != null)
            {
                HtmlNodeCollection rows = tables[2].SelectNodes(".//tr");
                for (int i = 1; i < rows.Count; ++i)
                {
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                    if (cols != null)
                    {
                        dRow = dTable.NewRow();
                        dRow["ID"] = strSessionID;
                        dRow["S.No."] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Transaction No"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Tan No"] = Convert.ToString(cols[2].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Form Name"] = Convert.ToString(cols[3].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Finnancial Year"] = Convert.ToString(cols[4].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Quarter"] = Convert.ToString(cols[5].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Filed On"] = Convert.ToString(cols[6].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Upload Type"] = Convert.ToString(cols[7].InnerText).Replace("\t", "").Replace("r", "").Replace("\n", "");
                        dRow["Token Number"] = Convert.ToString(cols[8].InnerText).Replace("\t", "").Replace("\r", "").Replace("\n", "");
                        dRow["Status"] = Convert.ToString(cols[9].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");

                        dTable.Rows.Add(dRow);
                    }
                }
            }
            //--------------------------------------------------------------------------------------
            respo.CustomeTypes = dTable;
            respo.Respons = enmResponse.Success;
            //--------------------------------------------------------------------------------------
            return respo;
        }

        #endregion


        public TracesResponse ViewDetailsAcknowledgement(string strID,string ackNo)
        {
            TracesResponse respo = new TracesResponse();
            
            //---------------------------------------------
            StringBuilder ss = new StringBuilder();
            //string strResponse = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/Services/TdsReturnStatusView.html?ID=" + strID + "&ackNo=" + ackNo, ss);
            string strResponse = makeHTTPPostRequest("https://portal.incometaxindiaefiling.gov.in/e-Filing/Services/TdsReturnStatusView.html?ID=" + strID + "&ackNo=" + ackNo, ss);

            if(!IsConditionMatch(strResponse, "Click here to download Provisional Receipt"))
            {
                if (IsConditionMatch(strResponse, "Rejected"))
                {
                    //respo.Message = "Return Rejected";
                    respo.Respons = enmResponse.ReturnRejected;
                    //--
                    DataTable dTableRejected = new DataTable();
                    dTableRejected.Columns.Add("ID");
                    dTableRejected.Columns.Add("Status");
                    //
                    DataRow dRowRejected = null;
                    //-------------------------------------------------
                    string strStatusID = "";
                    string strStatus = "";

                    HtmlAgilityPack.HtmlDocument docRejected = new HtmlAgilityPack.HtmlDocument();
                    docRejected.LoadHtml(strResponse);

                    HtmlNodeCollection tablesRejected = docRejected.DocumentNode.SelectNodes("//table");
                    if (tablesRejected != null)
                    {
                        //-----------------------------------------------------------------------
                        // PROCESSING 1ST TABLE 
                        //-----------------------------------------------------------------------
                        HtmlNodeCollection rowsRejected = tablesRejected[0].SelectNodes(".//tr");
                        for (int i = 1; i < rowsRejected.Count; ++i)
                        {
                            HtmlNodeCollection colsRejected = rowsRejected[i].SelectNodes(".//td");

                            if (colsRejected != null)
                            {
                                strStatus = Convert.ToString(colsRejected[4].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                                break;
                            }
                        }
                        //-----------------------------------------------------------------------
                        // PROCESSING 2ND TABLE
                        //-----------------------------------------------------------------------
                        rowsRejected = tablesRejected[1].SelectNodes(".//tr");
                        for (int i = 1; i < rowsRejected.Count; ++i)
                        {
                            HtmlNodeCollection colsRejected = rowsRejected[i].SelectNodes(".//td");

                            //if (colsRejected != null && i == 2)
                            //{
                            //    dRowRejected = dTableRejected.NewRow();
                            //    dRowRejected["ID"] = strStatusID;
                            //    dRowRejected["Status"] = strStatus;

                            //    dTableRejected.Rows.Add(dRowRejected);
                            //}
                            if (i == 2)
                            {
                            //    strStatusID = Convert.ToString(cols[0].InnerHtml).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            //    strStatusID = strStatusID.Substring(strStatusID.IndexOf("prcsdId=") + 8);
                            //    strStatusID = strStatusID.Substring(0, strStatusID.IndexOf("&"));
                                strStatus = strStatus + " - " + Convert.ToString(colsRejected[2].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                                dRowRejected = dTableRejected.NewRow();
                                dRowRejected["ID"] = strStatusID;
                                dRowRejected["Status"] = strStatus;

                                dTableRejected.Rows.Add(dRowRejected);
                                break;
                            }

                        }
                    }
                    respo.CustomeTypes = dTableRejected;
                    respo.Respons = enmResponse.ReturnRejected;
                }
                else
                {
                    respo.Message = "Server Error";
                    respo.Respons = enmResponse.SessionTimeout;
                }
                return respo;
            }

            DataTable dTable = new DataTable();
            dTable.Columns.Add("ID");
            dTable.Columns.Add("Process Id");
            dTable.Columns.Add("S.No.");
            dTable.Columns.Add("Batch No");
            dTable.Columns.Add("Transaction Type");
            dTable.Columns.Add("RRR Number");
            dTable.Columns.Add("Status");
            //-------------------------------------------------
            DataRow dRow = null;
            //-------------------------------------------------
            string strprcsID = "";
            string strAcceptType = "";
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(strResponse);

            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            if (tables != null)
            {
                //-----------------------------------------------------------------------
                // PROCESSING 1ST TABLE 
                //-----------------------------------------------------------------------
                HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");
                for (int i = 1; i < rows.Count; ++i)
                {
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                    if (cols != null)
                    {
                        strAcceptType = Convert.ToString(cols[4].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        break;
                    }
                }
                //-----------------------------------------------------------------------
                // PROCESSING 2ND TABLE
                //-----------------------------------------------------------------------
                rows = tables[1].SelectNodes(".//tr");
                for (int i = 1; i < rows.Count; ++i)
                {
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                    
                    if (cols != null && i==2)
                    {
                        dRow = dTable.NewRow();
                        dRow["ID"] = strID;
                        dRow["Process Id"] = "";
                        dRow["S.No."] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Batch No"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Transaction Type"] = Convert.ToString(cols[2].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["RRR Number"] = Convert.ToString(cols[3].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Status"] = strAcceptType;

                        dTable.Rows.Add(dRow);
                    }
                    if (i == 3)
                    {
                        strprcsID = Convert.ToString(cols[0].InnerHtml).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        //strprcsID = strprcsID.Substring(strprcsID.IndexOf("Process Id=")+8);
                        strprcsID = strprcsID.Substring(strprcsID.IndexOf("prcsdId=") + 8);
                        strprcsID = strprcsID.Substring(0,strprcsID.IndexOf("&"));

                        dRow["Process Id"] = strprcsID;
                        break;
                    }

                }
            }
            //--------------------------------------------------------------------------------------
            respo.CustomeTypes = dTable;
            respo.Respons = enmResponse.Success;
            //--------------------------------------------------------------------------------------
            return respo;
                                   
        }


        public TracesResponse RequestForDownloadFile(string SessionID, string pdfID, string TranType, string strPath )
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            string strResponse = "";
            //----------------------------------------------------
            try
            {

                //string strURL = "https://incometaxindiaefiling.gov.in/e-Filing/Services/ViewTdsAckPdf.html?ID=" + SessionID + "&prcsdId=" + pdfID + "&transactionType=" + TranType;
                string strURL = "https://portal.incometaxindiaefiling.gov.in/e-Filing/Services/ViewTdsAckPdf.html?ID=" + SessionID + "&prcsdId=" + pdfID + "&transactionType=" + TranType;

                if (!HttpDownloadRequest(strURL, strPath))
                    objResponse.Respons = enmResponse.Failed;

            }
            catch (Exception err)
            {
                objResponse.Message = err.Message;
                objResponse.Respons = enmResponse.Failed;
            }
            return objResponse;

        }

        #region HttpDownloadRequest
        private bool HttpDownloadRequest(string strURL, string strPath)
        {
            bool DownloadStatus = true;
            string strFilename = "";
            try
            {
                //-- 2014-01-25
              //  SetAllowUnsafeHeaderParsing();

            //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                //--
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.KeepAlive = true;
                request.Timeout = 300000;
                request.AllowWriteStreamBuffering = false;
                request.AllowAutoRedirect = false;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                //-- ARUP @ 2016/02/20
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
        //        request.Headers.Add("Upgrade-Insecure-Requests", "1");
         //       request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //    request.Headers.Add("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
                //---------------------------------------------------
                //for (int i = 0; i < response.Cookies.Count; i++)
                //{
                //    response.Cookies[i].Path = String.Empty;
                //}

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(response.Cookies);

               // response = (HttpWebResponse)request.GetResponse();
                using (HttpWebResponse response1 = (HttpWebResponse)request.GetResponse())
                {
                    var fn = response1.Headers["Content-Disposition"].Split(new string[] { "=" }, StringSplitOptions.None)[1];
                    var responseStream = response1.GetResponseStream();
                    using (var fileStream = File.Create(Path.Combine(strPath, fn)))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }             


            }
            catch (Exception err)
            {
                DownloadStatus = false;
                throw err;

            }

            return DownloadStatus;
        }

        #endregion




    }

    #region ParamAdhar

    public class ParamAdhar
    {
        public string TanNo { get; set; }
        public string FVUVersion { get; set; }
        public string FAYear { get; set; }
        public string Forms { get; set; }
        public string Quarter { get; set; }
        public string UploadType { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public string OTP { get; set; }
        public string RequestID { get; set; }

        public string OriginalRRRNo { get; set; }
        public string PreviousRRRNo { get; set; }

    }


    #endregion






}
