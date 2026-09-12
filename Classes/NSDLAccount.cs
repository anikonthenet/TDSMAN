
#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;


#endregion


namespace TDSMAN.Classes
{
    class NSDLAccount
    {
        //UPDATED ON 16/04/2012

        #region Variable Declaration
        string strURL = "";

        //LAVEL 1. HTTP REQUEST WITH LOGIN PAGE ONLY FOR GETTING SESSION VALUES.   

        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;

        string strServerResponse = "";
        CookieContainer objContainer = new CookieContainer();
        NameValueCollection objServerMessage = new NameValueCollection();

        string strUrl = "";
        string strBaseURL = "https://onlineservices.tin.nsdl.com/TIN/";
        string strLogOffLink = "";

        string strRetString = "";

        string strServerError = "Due to some technical problem at tin-nsdl.com we were not able to process the request. Please try again later.";

        #endregion

        #region TanAccount
        public NSDLAccount()
        {
            InitializeServerMessage();
        }

        #endregion

        #region Request_Consolidated_Statement
        public bool Request_Consolidated_Statement(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        {
            try
            {
                strServerMessage = "";
                //-----------------------------------     
                // Create a request using a URL that can receive a post.       
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = objContainer;
                request.KeepAlive = true;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();


                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);


                // Read the content.

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    if (strServerMessage == "Your Password has been expired.Please change your password.")
                        strServerMessage = "Your Password has been expired.Please change your password by visiting tin-nsdl.com";

                    return false;
                }
                //================================================================================
                /*2. LEVEL 2
                 *   AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
                 *   FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER  */
                //----------------------------------------------------------
                //strUrl = GetUrlString(strServerResponse, "CommonKYC.do?ID=", "',");

                strUrl = GetUrlStringFromMenu(strServerResponse, "Request Consolidated TDS/TCS Statement", "ConsolidatedFileRequest");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Invalid UserId and Password provided";
                    return false;
                }
                //-------------------------------------------------------   

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                //request.CookieContainer = objContainer;
                request.KeepAlive = true;
                // request.Method = "POST";


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 *  CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    strServerMessage = "Internal error";
                    return false;
                }
                //-------------------------------------------------------   
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
                strLinkBuilder.Append("&stmtPeriod=" + objConsolidate.Quarter);
                strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteData, 0, byteData.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    //  IF ALREADY REQUESTED THEN RETRIEVE ONLY REFERENCE NO.
                    if (Regex.IsMatch(strServerResponse, "AlreadyRequested"))
                    {

                        strUrl = GetUrlString(strServerResponse, "Your request for consolidated file", "Download the file");

                        if (strUrl.IndexOf(".") > 0)
                            strServerMessage = strUrl.Substring(strUrl.IndexOf(".") + 1).Replace(".", "").Trim();

                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                        if (!string.IsNullOrEmpty(strUrl))
                            LogOff(strBaseURL + strUrl);

                        return true;
                    }
                    //===============================================================


                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }


                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

                //IF THIS REQUEST IS NIL RETURN THEN THIS LINK WILL BE BLANK.
                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = GetReferenceNo(strServerResponse, "Your request for '<b>Consolidated", ".<br>");

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!String.IsNullOrEmpty(strServerMessage))
                    {
                        if (!string.IsNullOrEmpty(strUrl))
                        {
                            LogOff(strBaseURL + strUrl);
                        }
                        return true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strUrl))
                        {
                            LogOff(strBaseURL + strUrl);
                        }

                        strServerMessage = "Internal error";
                        return false;
                    }
                }
                //-------------------------------------------------------  
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
                strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
                strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
                strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
                strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
                strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
                strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
                strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
                strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
                strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);



                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);

                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                response = (HttpWebResponse)request.GetResponse();


                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();
                //=====================================================================================
                reader.Close();
                dataStream.Close();
                response.Close();
                //=====================================================================================

                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }


                //==================================================
                // CHECKING IF IT IS LAST STEP IF YES THEN GO ONE LEVEL AGAIN
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYCRequest.do?ID=", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    //FETCHING REFERENCE NO
                    strServerMessage = GetReferenceNo(strServerResponse, "Your request for", "<br>Download the file(s)");


                    if (string.IsNullOrEmpty(strServerMessage))
                    {
                        strServerMessage = "Internal error";

                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                        if (!string.IsNullOrEmpty(strUrl))
                        {
                            LogOff(strBaseURL + strUrl);
                        }

                        return false;
                    }
                    else
                    {

                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                        LogOff(strBaseURL + strUrl);
                        return true;
                    }
                }

                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("&form16ACheck=on");

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();


                reader.Close();
                dataStream.Close();
                response.Close();
                //FETCHING REFERENCE NO. 
                //==================================================
                strServerMessage = GetReferenceNo(strServerResponse, "Your request for '<b>Consolidated", ".<br>");

                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);

                return true;
            }
            catch (Exception err_handler)
            {
                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);
                strServerMessage = strServerError;
                return false;
            }
        }

        #endregion

        #region Request_Form_16A

        public bool Request_Form_16A(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        {
            try
            {
                strServerMessage = "";
                //-----------------------------------     
                // Create a request using a URL that can receive a post.       
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = objContainer;
                request.KeepAlive = true;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();


                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);


                // Read the content.

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    if (strServerMessage == "Your Password has been expired.Please change your password.")
                        strServerMessage = "Your Password has been expired.Please change your password by visiting tin-nsdl.com"; 
                    
                    return false;
                }
                //================================================================================
                /*2. LEVEL 2
                 *   AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
                 *   FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER  */
                //----------------------------------------------------------
                strUrl = GetUrlStringFromMenu(strServerResponse, "Request Form 16A", "Form16AFileRequest");
                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Invalid User Id or Password provided";
                    return false;
                }
                //-------------------------------------------------------   

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                //request.CookieContainer = objContainer;
                request.KeepAlive = true;
                // request.Method = "POST";


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 *  CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------   
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
                strLinkBuilder.Append("&stmtPeriod=" + objConsolidate.Quarter);
                strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);
                //-------------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteData, 0, byteData.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }
                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------  
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
                strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
                strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
                strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
                strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
                strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
                strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
                strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
                strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
                strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);



                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);

                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                response = (HttpWebResponse)request.GetResponse();


                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();
                //=====================================================================================
                reader.Close();
                dataStream.Close();
                response.Close();
                //=====================================================================================

                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //------------------------------------------------------

                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYCRequest.do?ID=", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }

                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("&form16ACheck=on");

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();


                reader.Close();
                dataStream.Close();
                response.Close();
                //FETCHING REFERENCE NO. 
                //==================================================
                strServerMessage = GetReferenceNo(strServerResponse, "Your request for", "<br>Download the file(s)");

                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);

                return true;
            }
            catch (Exception err_handler)
            {
                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);
                strServerMessage = strServerError;
                return false;
            }

        }





        #endregion

        #region Request_Form_16

        public bool Request_Form_16(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        {
            try
            {
                strServerMessage = "";
                //-----------------------------------     
                // Create a request using a URL that can receive a post.       
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = objContainer;
                request.KeepAlive = true;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();


                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);


                // Read the content.

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    if (strServerMessage == "Your Password has been expired.Please change your password.")
                        strServerMessage = "Your Password has been expired.Please change your password by visiting tin-nsdl.com"; 
                    
                    return false;
                }
                //================================================================================
                /*2. LEVEL 2
                 *   AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
                 *   FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER  */
                //----------------------------------------------------------
                strUrl = GetUrlStringFromMenu(strServerResponse, "Request Form 16'", "Form16Request");
                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Invalid User Id or Password provided";
                    return false;
                }
                //-------------------------------------------------------   

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                //request.CookieContainer = objContainer;
                request.KeepAlive = true;
                // request.Method = "POST";


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 *  CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------   
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
                strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);
                //-------------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteData, 0, byteData.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }
                    return false;
                }
                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------  
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
                strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
                strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
                strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
                strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
                strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
                strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
                strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
                strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
                strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);



                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);

                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                response = (HttpWebResponse)request.GetResponse();


                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();
                //=====================================================================================
                reader.Close();
                dataStream.Close();
                response.Close();
                //=====================================================================================

                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //------------------------------------------------------

                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYCRequest.do?ID=", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }

                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("&form16ACheck=on");

                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();


                reader.Close();
                dataStream.Close();
                response.Close();
                //FETCHING REFERENCE NO. 
                //==================================================
                strServerMessage = GetReferenceNo(strServerResponse, "Your request for", "<br>Download the file(s)");

                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);

                return true;
            }
            catch (Exception err_handler)
            {
                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);
                strServerMessage = strServerError;
                return false;
            }

        }





        #endregion

        #region Request_Defaults


        public bool Request_Defaults(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        {
            strServerMessage = "";

            try
            {
                //-----------------------------------     
                // Create a request using a URL that can receive a post.       
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = new CookieContainer();
                request.KeepAlive = true;
                request.Timeout = 10000;

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Timeout = 10000;
                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                /* WE CAN'T JUST SET THE COOKIECONTAINER TO HAVE THE COOKIES FROM THE RESPONSE, 
                 * SINCE IT'S A NEW REQUEST WE NEED TO SET THE DOMAIN THAT THE COOKIES BELONG TO
                 * AS WELL (.NET IS NOT ASSUMING THAT THE DOMAIN IS THE ONE FROM THE URI IN THE REQUEST OBJECT) */

                request.CookieContainer = objContainer;
                // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                // Read the content.
                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    if (strServerMessage == "Your Password has been expired.Please change your password.")
                        strServerMessage = "Your Password has been expired.Please change your password by visiting tin-nsdl.com";
                    return false;
                }
                //================================================================================
                /*2. LEVEL 2
                 *   AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
                 *   FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER  */
                //----------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "Defaults.do?ID=", "'");
                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal Error";
                    return false;
                }

                string strDefalLink = strUrl;


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);
                request.Method = "GET";
                request.Timeout = 10000;
                request.KeepAlive = true;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();


                /* *************************************************
                 *  CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return false;
                }
                else
                {
                    if (Regex.IsMatch(strServerResponse, "No Records Found"))
                    {
                        strServerMessage = "Defaulter List Not Found";

                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                        if (!string.IsNullOrEmpty(strUrl))
                            LogOff(strBaseURL + strUrl);

                        return false;
                    }
                }

                //***********************************************************
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return false;
                }


                //GETTING DEFAULTER LIST URL WITH SOME SEARCH CRITERIA
                string strSearchString = "FinancialYear='" + objConsolidate.FinYear.Substring(0, 4) + "-" + objConsolidate.FinYear.Substring(4, 2) + "' AND FormNo='" + objConsolidate.FormNo + "' AND Quarter='" + objConsolidate.Quarter + "'";

                string strLink = get_Default_Link(strServerResponse, strSearchString, out strServerMessage);

                if (string.IsNullOrEmpty(strLink))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    strServerMessage = "No Records Found";

                    return false;

                }
                //==========================================================
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strLink);
                request.Timeout = 10000;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.AllowWriteStreamBuffering = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";

                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }
                request.CookieContainer = objContainer;
                //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);
                request.CookieContainer.Add(response.Cookies);


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 *  CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return false;
                }
                strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID=", "\"");
                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------   
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
                strLinkBuilder.Append("&stmtPeriod=" + objConsolidate.Quarter);
                strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);
                //-------------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";

                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteData, 0, byteData.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }
                    return false;
                }

                // IF ALREADY GENERATED ------------------------

                if (Regex.IsMatch(strServerResponse, "Your request for '<b>Defaults</b>'"))
                {
                    strServerMessage = GetReferenceNo(strServerResponse, "Your request for", ".<br>");


                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                    if (!string.IsNullOrEmpty(strUrl))
                        LogOff(strBaseURL + strUrl);

                    return true;
                }


                //------------------------------------------------------
                strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

                if (string.IsNullOrEmpty(strUrl))
                {
                    strServerMessage = "Internal error";

                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return false;
                }
                //-------------------------------------------------------  
                strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
                strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
                strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
                strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
                strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
                strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
                strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
                strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
                strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
                strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                postData = strLinkBuilder.ToString();

                byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteFinalData.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteFinalData, 0, byteFinalData.Length);

                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                response = (HttpWebResponse)request.GetResponse();


                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                strServerResponse = reader.ReadToEnd();
                //=====================================================================================
                reader.Close();
                dataStream.Close();
                response.Close();
                //=====================================================================================

                /* *************************************************
                * //CHECKING ANY ERROR FROM SERVER END 
                * ************************************************** */
                if (IsServerError(strServerResponse, out strServerMessage))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);

                    }

                    return false;
                }

                //FETCHING REFERENCE NO. 
                //==================================================
                strServerMessage = GetReferenceNo(strServerResponse, "Your request for", ".<br>");

                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);

            }
            catch
            {
                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                LogOff(strBaseURL + strUrl);
                return false;
            }

            return true;

        }


        //public bool Request_Defaults(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        //{
        //    strServerMessage = "";

        //    try
        //    {
        //        //-----------------------------------
        //        // Create a request using a URL that can receive a post.
        //        //-----------------------------------------------------------
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

        //        request.CookieContainer = new CookieContainer();
        //        request.KeepAlive = true;
        //        request.Timeout = 10000;

        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        //=======================================================================
        //        StringBuilder strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("userid=" + objAuth.UserID);
        //        strLinkBuilder.Append("&password=" + objAuth.Password);
        //        strLinkBuilder.Append("&tan=" + objAuth.TAN);
        //        strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");
        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;
        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";
        //        request.Timeout = 10000;
        //        // Create POST data and convert it to a byte array.
        //        string postData = strLinkBuilder.ToString();
        //        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteArray.Length;

        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        /* WE CAN'T JUST SET THE COOKIECONTAINER TO HAVE THE COOKIES FROM THE RESPONSE,
        //        * SINCE IT'S A NEW REQUEST WE NEED TO SET THE DOMAIN THAT THE COOKIES BELONG TO
        //        * AS WELL (.NET IS NOT ASSUMING THAT THE DOMAIN IS THE ONE FROM THE URI IN THE REQUEST OBJECT) */

        //        request.CookieContainer = objContainer;
        //        // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

        //        request.CookieContainer.Add(response.Cookies);

        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteArray, 0, byteArray.Length);

        //        // Close the Stream object.
        //        dataStream.Close();

        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();

        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();

        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);

        //        // Read the content.
        //        strServerResponse = reader.ReadToEnd();

        //        // Clean up the streams.
        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            return false;
        //        }
        //        //================================================================================
        //        /*2. LEVEL 2
        //        * AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
        //        * FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER */
        //        //----------------------------------------------------------
        //        strUrl = GetUrlString(strServerResponse, "Defaults.do?ID=", "'");
        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Internal error";
        //            return false;
        //        }

        //        string strDefalLink = strUrl;


        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);
        //        request.Method = "GET";
        //        request.Timeout = 10000;
        //        request.KeepAlive = true;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

        //        request.CookieContainer.Add(response.Cookies);

        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();


        //        /* *************************************************
        //        * CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            return false;
        //        }
        //        else
        //        {
        //            if (Regex.IsMatch(strServerResponse, "No Records Found"))
        //            {
        //                strServerMessage = "Defaulter List Not Found";

        //                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //                if (!string.IsNullOrEmpty(strUrl))
        //                    LogOff(strBaseURL + strUrl);

        //                return false;
        //            }
        //        }

        //        //***********************************************************
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            return false;
        //        }


        //        //GETTING DEFAULTER LIST URL WITH SOME SEARCH CRITERIA
        //        string strSearchString = "FinancialYear='" + objConsolidate.FinYear.Substring(0, 4) + "-" + objConsolidate.FinYear.Substring(4, 2) + "' AND FormNo='" + objConsolidate.FormNo + "' AND Quarter='" + objConsolidate.Quarter + "'";

        //        string strLink = get_Default_Link(strServerResponse, strSearchString, out strServerMessage);

        //        if (string.IsNullOrEmpty(strLink))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            strServerMessage = "No Records Found";

        //            return false;

        //        }
        //        //==========================================================
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strLink);
        //        request.Timeout = 10000;
        //        request.KeepAlive = true;
        //        request.AllowAutoRedirect = true;
        //        request.AllowWriteStreamBuffering = true;
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.Method = "GET";

        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }
        //        request.CookieContainer = objContainer;
        //        //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);
        //        request.CookieContainer.Add(response.Cookies);


        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        /* *************************************************
        //        * CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            return false;
        //        }
        //        strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID=", "\"");
        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Internal error";

        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }

        //            return false;
        //        }
        //        //-------------------------------------------------------
        //        strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
        //        strLinkBuilder.Append("&stmtPeriod=" + objConsolidate.Quarter);
        //        strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);
        //        //-------------------------------------------------------------
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";

        //        // Create POST data and convert it to a byte array.
        //        postData = strLinkBuilder.ToString();

        //        byte[] byteData = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;

        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteData.Length;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        request.CookieContainer.Add(response.Cookies);

        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteData, 0, byteData.Length);

        //        // Close the Stream object.
        //        dataStream.Close();

        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();

        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();

        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();
        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }
        //            return false;
        //        }
        //        //------------------------------------------------------
        //        strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Internal error";

        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }

        //            return false;
        //        }
        //        //-------------------------------------------------------
        //        strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
        //        strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
        //        strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
        //        strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
        //        strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
        //        strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
        //        strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
        //        strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
        //        strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
        //        strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";


        //        // Create POST data and convert it to a byte array.
        //        postData = strLinkBuilder.ToString();

        //        byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;


        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteFinalData.Length;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        request.CookieContainer.Add(response.Cookies);

        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteFinalData, 0, byteFinalData.Length);

        //        // Close the Stream object.
        //        dataStream.Close();


        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();


        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();

        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);
        //        // Read the content.
        //        strServerResponse = reader.ReadToEnd();
        //        //=====================================================================================
        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();
        //        //=====================================================================================

        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);

        //            }

        //            return false;
        //        }

        //        //FETCHING REFERENCE NO.
        //        //==================================================
        //        strServerMessage = GetReferenceNo(strServerResponse, "Your request for", ".<br>");

        //        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //        LogOff(strBaseURL + strUrl);

        //    }
        //    catch
        //    {
        //        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //        LogOff(strBaseURL + strUrl);
        //        return false;
        //    }

        //    return true;

        //}

        #endregion

        #region enmServerMessage
        //THIS ENUM IS USED FOR INDEXING COLLECTION OF SERVER MESSAGE.
        enum enmServerMessage
        {
            CookiesSupport,
            InValidUserID,
            WrongUserIDPwd,
            AlreadyLogged,
            AccountLocked,
            SessionExpire,
            InvalidStatement1,
            InvalidStatement2,
            InvalidPRN,
            InvalidFY,
            InvalidBSRCode,
            InvalidTDSAmount,
            InvalidChallanNo,
            InValidPAN,
            DatabaseProblem,
            TANMandatory,
            TANPRNNotValid,
            TANNotValid,
            RecordNotFound,
            InvalidTaxAmount,
            ConsolidateFileGenerated,
            DeducteePAN2,
            NoOfAttemps,
            For16ANotReq,
            DistinctPAN,
            InvFileReq,
            Form1624Q,
            FileCannotbeRequested,
            PasswordExpired,
            PasswordRequired,
            StatementRequestedIsRejected,
            Form16Acannotberequested
        }

        #endregion

        #region InitializeServerMessage
        private void InitializeServerMessage()
        {

            objServerMessage.Clear();

            //ADD ALL SERVER MESSAGE
            objServerMessage.Add(Convert.ToString(enmServerMessage.CookiesSupport), "Either Cookies are disabled");
            objServerMessage.Add(Convert.ToString(enmServerMessage.AlreadyLogged), "You have Already Logged Into the System");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InValidUserID), "Invalid User ID Or Password");
            objServerMessage.Add(Convert.ToString(enmServerMessage.WrongUserIDPwd), "Incorrect combination of user ID, password and TAN");
            objServerMessage.Add(Convert.ToString(enmServerMessage.SessionExpire), "Your Session Has Expired.Please Login Again");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidStatement1), "Statement details do not match with the details available at TIN central system");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidPRN), "Invalid Provisional Receipt Number");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidFY), "Invalid Financial Year");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidChallanNo), "Challan/ Transfer voucher details do not match");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InValidPAN), "PAN and corresponding amount do not match");
            objServerMessage.Add(Convert.ToString(enmServerMessage.AccountLocked), "On next incorrect password attempt,your account will be locked");
            objServerMessage.Add(Convert.ToString(enmServerMessage.DatabaseProblem), "Problem with Database.Try later");
            objServerMessage.Add(Convert.ToString(enmServerMessage.TANMandatory), "TAN is mandatory. Provisional receipt number is mandatory");
            objServerMessage.Add(Convert.ToString(enmServerMessage.TANNotValid), "TAN is not  valid.Please enter the valid value");
            objServerMessage.Add(Convert.ToString(enmServerMessage.TANPRNNotValid), "Provisional receipt number is not  valid.Please enter the valid value");
            //objServerMessage.Add(Convert.ToString(enmServerMessage.RecordNotFound), "No Records Found");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidBSRCode), "Please enter 7 digit BSR code of the bank branch through which remittance is made");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidTaxAmount), "Please enter valid Tax Deducted Amount for deductee record 3");
            objServerMessage.Add(Convert.ToString(enmServerMessage.ConsolidateFileGenerated), "Your request for consolidated file has been generated successfully");
            objServerMessage.Add(Convert.ToString(enmServerMessage.DeducteePAN2), "Please enter Valid 10 digit deductee PAN");
            objServerMessage.Add(Convert.ToString(enmServerMessage.NoOfAttemps), "You have exceeded the attempts for the day. You may request for the said statement/ file tomorrow.");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvalidStatement2), "TAN of the statement requested do not match with details available at TIN central system");
            objServerMessage.Add(Convert.ToString(enmServerMessage.For16ANotReq), "Form16A File Cannot be Requested");
            objServerMessage.Add(Convert.ToString(enmServerMessage.DistinctPAN), "Please enter 3 distinct PAN's and corresponding amount");
            objServerMessage.Add(Convert.ToString(enmServerMessage.InvFileReq), "Invalid File Requested");
            objServerMessage.Add(Convert.ToString(enmServerMessage.Form1624Q), "Form 16 can be requested only for Form 24Q");
            objServerMessage.Add(Convert.ToString(enmServerMessage.FileCannotbeRequested), "Consolidated File Cannot be Requested");
            objServerMessage.Add(Convert.ToString(enmServerMessage.PasswordExpired), "Your Password has been expired.Please change your password.");
            objServerMessage.Add(Convert.ToString(enmServerMessage.PasswordRequired), "Password Is Required");
            objServerMessage.Add(Convert.ToString(enmServerMessage.StatementRequestedIsRejected), "Statement requested is rejected, provide Provisional Receipt /Token No. of accepted regular statement.");
            objServerMessage.Add(Convert.ToString(enmServerMessage.Form16Acannotberequested), "Form 16A can be requested only for Form 26Q and 27Q.");

        }


        #endregion

        #region IsServerError
        public bool IsServerError(string strServerResponse, out string strMessage)
        {
            strMessage = "";

            // CHECKING IF ANY SERVER ERROR MSG THAT WAS SEND IN RESPONSE WITH ANY MATCHED ENTRY IN COLLECTION.
            foreach (string key in objServerMessage.AllKeys)
            {
                if (Regex.IsMatch(strServerResponse, objServerMessage[key], RegexOptions.IgnoreCase))
                {
                    strMessage = objServerMessage[key];
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region GetUrlString
        string GetUrlString(string strServerReturnHTML, string strFirstString, string strLastString)
        {
            string strURL = "";
            int intFirstIndex = 0;
            int intLast = 0;

            if (strServerReturnHTML.Length > 0)
            {
                intFirstIndex = strServerReturnHTML.IndexOf(strFirstString);

                if (intFirstIndex >= 0)
                    intLast = strServerReturnHTML.IndexOf(strLastString, intFirstIndex);

                if (intLast > 0)
                    strURL = strServerReturnHTML.Substring(intFirstIndex, intLast - intFirstIndex);
            }

            return strURL;

        }

        #endregion

        #region GetNextUrlString
        string GetNextUrlString(string strServerReturnHTML, string strFirstString, string strLastString)
        {
            string strURL = "";
            int intFirstIndex = 0;
            int intLast = 0;

            if (strServerReturnHTML.Length > 0)
            {
                intFirstIndex = strServerReturnHTML.LastIndexOf(strFirstString);

                if (intFirstIndex >= 0)
                    intLast = strServerReturnHTML.IndexOf(strLastString, intFirstIndex);

                if (intLast > 0)
                    strURL = strServerReturnHTML.Substring(intFirstIndex, intLast - intFirstIndex);
            }

            return strURL;

        }

        #endregion

        #region GetUrlStringFromMenu
        public string GetUrlStringFromMenu(string strServerReturnHTML, string strFirstString, string strLastString)
        {
            string strURL = "";
            int intFirstIndex = 0;
            int intLast = 0;

            if (strServerReturnHTML.Length > 0)
            {
                intFirstIndex = strServerReturnHTML.IndexOf(strFirstString);

                if (intFirstIndex >= 0)
                    intLast = strServerReturnHTML.IndexOf(strLastString, intFirstIndex);

                if (intLast > 0)
                {
                    strURL = strServerReturnHTML.Substring(intFirstIndex, intLast - intFirstIndex);
                    strURL = strURL.Substring(strURL.IndexOf("','/") + 4);
                    strURL = strURL + strLastString;
                }
            }

            return strURL;

        }

        #endregion

        #region GetReferenceNo
        private string GetReferenceNo(string strResponse, string strStart, string strEnd)
        {

            string strURL = "";
            int intFirstIndex = 0;
            int intLast = 0;

            if (strResponse.Length > 0)
            {
                intFirstIndex = strResponse.IndexOf(strStart);

                if (intFirstIndex >= 0)
                    intLast = strResponse.IndexOf(strEnd, intFirstIndex);

                if (intLast > 0)
                {
                    strURL = strResponse.Substring(intFirstIndex, intLast - intFirstIndex);
                    strURL = strURL.Substring(strURL.IndexOf("reference no.") + 13).Replace(".", "").Replace("\r\n", "").Trim();
                }
            }

            return strURL;

        }

        #endregion

        #region LogOff
        private void LogOff(string strUrl)
        {
            try
            {
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strUrl);

                request.CookieContainer = objContainer;
                request.KeepAlive = true;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch
            {
            }

        }


        #endregion

        #region RetrieveHTMLTableData
        private DataTable RetrieveHTMLTableData(string strResponse, List<string> strMatchList, bool IsTagSuppressed)
        {
            DataTable dtStatement = null;
            DataRow rowStatement = null;

            String TableExpression = "<table[^>]*>(.*?)</table>";
            String RowExpression = "<tr[^>]*>(.*?)</tr>";
            String ColumnExpression = "<td[^>]*>(.*?)</td>";
            String strTableStructure = ""; ;


            int intIsHeader = 0;


            //' Get a match for all the tables in the HTML
            MatchCollection Tables = Regex.Matches(strResponse, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Loop through each table element

            foreach (Match mTable in Tables)
            {

                // Create the relevant amount of columns for this table (use the headers if they exist, otherwise use default names);
                if (mTable.Value.Contains("<tr"))
                {
                    if (mTable.Value.Contains(strMatchList[0]) && mTable.Value.Contains(strMatchList[1]))
                    {
                        strTableStructure = mTable.Value;

                        MatchCollection matches1 = Regex.Matches(mTable.Value, "<tr[^>]*>");
                        MatchCollection matches2 = Regex.Matches(mTable.Value, "</tr>");

                        int intTRCount = matches1.Count;
                        int intTREndCount = matches2.Count;

                        /* ---------------------------------------------------
                            CHECKING IF NO OF START TR TAG IS EQUAL TO END TR TAG 
                            IF NOT THEN ADD END TAG 
                         * --------------------------------------------------- */
                        if (intTRCount != intTREndCount)
                            strTableStructure = strTableStructure.Replace("<tr", "</tr><tr").Replace("</table>", "</tr></table>");

                        //Add a new table to the DataSet
                        dtStatement = new DataTable();

                        MatchCollection Headers = Regex.Matches(strTableStructure, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);


                        foreach (Match header in Headers)
                        {

                            MatchCollection columns = Regex.Matches(header.Value, ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            int intColumnIndex = 0;

                            if (intIsHeader > 0)
                                rowStatement = dtStatement.NewRow();

                            foreach (Match column in columns)
                            {
                                //dt.Columns.Add(header.Groups[1].ToString());

                                if (intIsHeader == 0)
                                {
                                    string strColumnName = column.Groups[1].ToString().Replace("<br>\r\n", "").Replace("<br>", "");

                                    strColumnName = Regex.Replace(strColumnName, @"\s", "").Replace(".", "");

                                    DataColumn myDataColumn;

                                    myDataColumn = new DataColumn();
                                    myDataColumn.DataType = Type.GetType("System.String");
                                    myDataColumn.ColumnName = strColumnName;
                                    dtStatement.Columns.Add(myDataColumn);

                                }
                                else
                                {
                                    string strRowVal = column.Groups[1].ToString().Replace("<br>\r\n", "").Replace("<br>", "");

                                    if (IsTagSuppressed == true)
                                        strRowVal = Regex.Replace(strRowVal, @"<[^>]*>", String.Empty);

                                    rowStatement[intColumnIndex] = strRowVal;

                                    intColumnIndex++;

                                }
                            }

                            if (intIsHeader > 0)
                                dtStatement.Rows.Add(rowStatement);

                            intIsHeader++;
                        }
                    }
                }
            }

            return dtStatement;

        }

        #endregion

        #region Start_Download
        private bool Start_Download(string strUrl, string strDestFilePath)
        {
            bool bnlReturn = true;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.KeepAlive = true;
                request.Timeout = 300000;
                request.AllowWriteStreamBuffering = false;
                request.AllowAutoRedirect = false;

                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

                response = (HttpWebResponse)request.GetResponse();

                Int64 iSize = 120000;

                dataStream = response.GetResponseStream();

                string strFilename = response.Headers["Content-Disposition"];

                strFilename = strFilename.Substring(strFilename.IndexOf("=") + 1);


                FileStream fs = new FileStream(strDestFilePath + "\\" + strFilename, FileMode.Create);

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
            catch
            {
                bnlReturn = false;

            }
            return bnlReturn;
        }

        #endregion

        #region DownloadFile_OLD
        private bool DownloadFile_OLD(NSDLAuthentication objAuth, string strHTTPResponse, string strSaveDirectory, string strSearchString, int intRowIndex, out string strServerError)
        {
            DataTable dTable = null;
            DataTable dtTable = null;
            DataRow dLastRow;
            DataRow[] dSelectedRow;
            bool bnlStatus = true;
            strServerError = "";
            string strRetString = "";

            try
            {
                dTable = View_Status_AllDownload_List(objAuth, false, strHTTPResponse, out strServerError);
                //============================================================

                if (dTable != null)
                {
                    // search datadow by Reference No
                    //================================================
                    if (!string.IsNullOrEmpty(strSearchString))
                    {

                        dtTable = dTable.Clone();

                        dSelectedRow = dTable.Select(strSearchString);
                        //=================================================
                        foreach (DataRow row in dSelectedRow)
                            dtTable.ImportRow(row);
                    }
                    else
                        dtTable = dTable.Copy();
                    //====================================================
                    if (dtTable.Rows.Count > 0)
                    {
                        if (dtTable.Rows.Count > intRowIndex)
                            dLastRow = dtTable.Rows[intRowIndex];
                        else
                            dLastRow = dtTable.Rows[0];

                        strRetString = Convert.ToString(dLastRow[9]);

                        if (strRetString != "")
                        {
                            if (strRetString.Contains("Under Process at NSDL"))
                            {
                                strServerError = "The file requested is under process at NSDL. Please try downloading after some time.";
                                bnlStatus = false;
                            }
                            else
                            {

                                strRetString = GetUrlString(strRetString, @"reqDownloadFile.do?ID=", "\" onclick");
                            }

                        }
                    }
                }
                else
                {
                    bnlStatus = false;
                }
                //========================================================
                if (!string.IsNullOrEmpty(strRetString) && bnlStatus == true)
                {

                    bnlStatus = Start_Download(strBaseURL + strRetString, strSaveDirectory);
                }

            }
            catch
            {
                bnlStatus = false;
                dTable = null;
            }
            finally
            {
                if (string.IsNullOrEmpty(strServerError))
                {
                    if (dtTable == null)
                        strServerError = "No Records Found";
                    else
                    {
                        if (dtTable.Rows.Count == 0)
                        {
                            dtTable = null;
                            strServerError = "No Records Found";
                        }
                    }
                }

                LogOff(strLogOffLink);
            }

            return bnlStatus;

        }

        #endregion

        #region DownloadFile
        private bool DownloadFile(NSDLAuthentication objAuth, string strHTTPResponse, string strSaveDirectory, string strSearchString, int intRowIndex, out string strServerError)
        {           
            bool bnlStatus = true;
            strServerError = "";
            string strRetString = "";

            try
            {               

                //GETTING DOWNLOAD LINKS
                strRetString = getDownload_Link(objAuth, strSearchString, strHTTPResponse, out strServerError);
                //============================================================
                if (!string.IsNullOrEmpty(strRetString))
                {
                    if (strRetString.Contains("Under Process at NSDL"))
                    {
                        strServerError = "The file requested is under process at NSDL. Please try downloading after some time.";
                        bnlStatus = false;
                    }                    
                }             
                else
                {
                    if(string.IsNullOrEmpty(strServerError))
                    strServerError = "No Records Found";
                   
                    bnlStatus = false;
                }
                //========================================================
                if (!string.IsNullOrEmpty(strRetString) && bnlStatus == true)
                {
                    bnlStatus = Start_Download(strBaseURL + strRetString, strSaveDirectory);
                }

            }
            catch
            {
                bnlStatus = false;
                strServerError = "There are some problem in connecting to the server please try later";  
            }
            finally
            {
                LogOff(strLogOffLink);
            }

            return bnlStatus;

        }

        #endregion

        #region DownloadLastFile
        public bool DownloadLastFile(string strHTTPResponse, string strSaveDirectory, out string strServerError)
        {
            return DownloadFile(null, strHTTPResponse, strSaveDirectory, "", 0, out strServerError);

        }

        #endregion

        #region DownloadLastFile
        public bool DownloadLastFile(NSDLAuthentication objAuth, string strSaveDirectory, out string strServerError)
        {
            return DownloadFile(objAuth, "", strSaveDirectory, "", 0, out strServerError);

        }

        #endregion

        #region get_Default_Link

        private string get_Default_Link(string strServerResponse, string strSearchString, out string strServerError)
        {
            strServerError = "";
            string strRetString = "";
            //---------------------------------------------------------------------------------------------
            List<string> ltContainKey = new List<string>();

            ltContainKey.Add("Sr. No.");
            ltContainKey.Add("Quarter");

            strLogOffLink = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
            strRetString = GetDataFromPagedTable("", strServerResponse, strSearchString, out strServerError);


            return strRetString;

            #region OLD Function
            //    DataTable dTable = null;
            //    DataTable dtTable = null;
            //    DataRow dLastRow;
            //    DataRow[] dSelectedRow;
            //    strServerError = "";
            //    string strRetString = "";

            //    //---------------------------------------------------------------------------------------------

            //    List<string> ltContainKey = new List<string>();

            //    ltContainKey.Add("Sr. No.");
            //    ltContainKey.Add("Quarter");

            //    dTable = RetrieveHTMLTableData(strServerResponse, ltContainKey, false);
            //    strLogOffLink = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
            //    //---------------------------------------------------------------------------------------------
            //    if (dTable != null)
            //    {
            //        // search datadow by Reference No
            //        //================================================

            //        dtTable = dTable.Clone();

            //        dSelectedRow = dTable.Select(strSearchString);
            //        //=================================================
            //        foreach (DataRow row in dSelectedRow)
            //            dtTable.ImportRow(row);

            //        //====================================================
            //        if (dtTable.Rows.Count > 0)
            //        {
            //            dLastRow = dtTable.Rows[0];

            //            strRetString = Convert.ToString(dLastRow[8]);

            //            if (strRetString != "")
            //                strRetString = GetUrlString(strRetString, @"DefSelect.do?ID=", "'>");
            //        }
            //    }

            //    return strRetString;
            //}

            #endregion

        }
        #endregion

        #region View_Status_AllDownload_List
        public DataTable View_Status_AllDownload_List(NSDLAuthentication objAuth, bool isLogOff, string strHTTPResponse, out string strServerError)
        {

            DataTable dTable = null;

            strServerError = "";

            strServerResponse = strHTTPResponse;

            if (string.IsNullOrEmpty(strHTTPResponse))
            {

                //-----------------------------------     
                // Create a request using a URL that can receive a post.       
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = objContainer;
                request.KeepAlive = true;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();


                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);


                // Read the content.

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerError))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return null;
                }
            }

            //================================================================

            strUrl = GetUrlString(strServerResponse, "requestsAll.do?ID", "',");

            if (string.IsNullOrEmpty(strUrl))
            {
                strServerError = "Server error";
                return null;
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);
                request.KeepAlive = true;

                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }


            List<string> ltContainKey = new List<string>();

            ltContainKey.Add("Sr. No.");
            ltContainKey.Add("Quarter");

            dTable = RetrieveHTMLTableData(strServerResponse, ltContainKey, false);

            strLogOffLink = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

            strLogOffLink = strBaseURL + strUrl;

            if (isLogOff)
                LogOff(strLogOffLink);

            return dTable;

        }

        #endregion

        #region getDownload_Link
        private string getDownload_Link(NSDLAuthentication objAuth, string strSearchString, string strHTTPResponse, out string strServerError)
        {
            string strLink = null;
            strServerError = "";
            strServerResponse = strHTTPResponse;

            //CHECKING IF IT IS FIRST TIME HITTING WEBSITE
            if (string.IsNullOrEmpty(strHTTPResponse))
            {
                //-----------------------------------     
                //SETP 1. Create a request to get login page url & cookies collection for on going request    
                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

                request.CookieContainer = new CookieContainer();
                request.KeepAlive = true;
                request.Timeout = 10000;


                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

               // STEP 2:: LOGIN INTO THE SITE WITH LOGIN CREDENTIALS
                //=======================================================================
                StringBuilder strLinkBuilder = new StringBuilder();

                strLinkBuilder.Append("userid=" + objAuth.UserID);
                strLinkBuilder.Append("&password=" + objAuth.Password);
                strLinkBuilder.Append("&tan=" + objAuth.TAN);
                strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Timeout = 10000;
                // Create POST data and convert it to a byte array.
                string postData = strLinkBuilder.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                /* WE CAN'T JUST SET THE COOKIECONTAINER TO HAVE THE COOKIES FROM THE RESPONSE, 
                 * SINCE IT'S A NEW REQUEST WE NEED TO SET THE DOMAIN THAT THE COOKIES BELONG TO
                 * AS WELL (.NET IS NOT ASSUMING THAT THE DOMAIN IS THE ONE FROM THE URI IN THE REQUEST OBJECT) */

                request.CookieContainer = objContainer;
                // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);

                // Read the content.
                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                /* *************************************************
                 * //CHECKING ANY ERROR FROM SERVER END 
                 * ************************************************** */
                if (IsServerError(strServerResponse, out strServerError))
                {
                    strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                    if (!string.IsNullOrEmpty(strUrl))
                    {
                        LogOff(strBaseURL + strUrl);
                    }

                    return null;
                }
            }

            //================================================================
            //RETRIEVE DOWNLOAD MENU'S LINK
            strUrl = GetUrlString(strServerResponse, "requestsAll.do?ID", "',");

            if (string.IsNullOrEmpty(strUrl))
            {
                strServerError = "Server error";
                return null;
            }
            else
            {
                // RETRIEVE DOWNLOAD PAGE 
                request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);
                request.Method = "GET";
                request.Timeout = 10000;
                request.KeepAlive = true;


                for (int i = 0; i < response.Cookies.Count; i++)
                {
                    response.Cookies[i].Path = String.Empty;
                }

                request.CookieContainer = objContainer;
                //request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com/"), response.Cookies);

                request.CookieContainer.Add(response.Cookies);

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                strServerResponse = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

            }

            /* *************************************************
             * //CHECKING ANY ERROR FROM SERVER END 
             * ************************************************** */
            if (IsServerError(strServerResponse, out strServerError))
            {
                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
                if (!string.IsNullOrEmpty(strUrl))
                {
                    LogOff(strBaseURL + strUrl);
                }

                return null;
            }

            // THIS METHOD FOR PAGED DOWNLOAD LINK TABLE. IT WILL SEARCH VALUES RECURSIVELY UNTIL LAST PAGE 

            strLink = GetDownloadLinkFromPagedTable("", strServerResponse, strSearchString, out strServerError);

            
            return strLink;

        }

        #endregion

        #region DownloadOnSearchCriteria
        public bool DownloadOnSearchCriteria(NSDLAuthentication objAuth, string strSaveDirectory, string strSearchCriteria, out string strServerError)
        {
            return DownloadFile(objAuth, "", strSaveDirectory, strSearchCriteria, 0, out strServerError);
        }

        #endregion

        #region TDS_TCS_Statement

        #region get_Status_TDS_TCS_Statement
        public bool get_Status_TDS_TCS_Statement(string strPRN, string strTAN)
        {

            string strRetString = "";

            string strAccceptedReturnBatch = "";
            
            string strSortOrder = "ProvisionalReceiptDate, BatchNo";

            DataTable dtTable = Retrieve_TDS_TCS_Statement(strPRN, strTAN, out strRetString);

            if (dtTable != null)
            {
                //Commented by Shrey Kejriwal on 23/07/2012
                // search datadow by Accepted by TIN
                //DataRow[] dSelectedRow = dtTable.Select("Status <> 'Rejected by TIN'", strSortOrder);
                //DataRow dLastRow = dSelectedRow[dSelectedRow.Length - 1];

                //strRetString = Convert.ToString(dLastRow[2]);

                //Added by Shrey Kejriwal on 23/07/2012

                for (int i = dtTable.Rows.Count; i > 0; i--)
                {
                    object[] cells = dtTable.Rows[i - 1].ItemArray;
                    //-- modified by ANIK @ 2013/04/29
                    if (cells[6].ToString() != "Rejected by TIN" && cells[6].ToString() != "Rejected by CPC")
                    {
                        //Checking the batch of last accepted return
                        strAccceptedReturnBatch = cells[0].ToString();
                        break;
                    }
                    else
                        continue;
                }

                DataRow[] drs = dtTable.Select("FileRefNo = " + strAccceptedReturnBatch);

                foreach (DataRow dr in drs)
                {
                    if (dr.ItemArray[2].ToString() == strPRN)
                        return true;
                    else
                        continue;
                }

                //for (int i = dtTable.Rows.Count; i > 0; i--)
                //{
                //    object[] cells = dtTable.Rows[i - 1].ItemArray;
                //    //-- modified by ANIK @ 2013/04/29
                //    if (cells[0].ToString() == strAccceptedReturnBatch)
                //    {
                //        strLastTokenNo[0] = "aasda";
                //    }
                //    else
                //        continue;
                //}
            }
            return false;
        }

        #endregion

        #region get_Status_TDS_TCS_Statement
        //public DataRow[] get_Status_TDS_TCS_Statement(string strPRN, string strTAN, string strSearchCriteria)
        //{

        //    string strRetString = "";
        //    DataRow[] dSelectedRow = null;

        //    DataTable dtTable = Retrieve_TDS_TCS_Statement(strPRN, strTAN, out strRetString);

        //    if (dtTable != null)
        //    {
        //        // search datadow by Accepted by TIN
        //        dSelectedRow = dtTable.Select(strSearchCriteria);

        //    }


        //    return dSelectedRow;
        //}


        #endregion       

        #region get_Status_TDS_TCS_Statement
        public bool get_Status_TDS_TCS_Statement(string strPRN, string strTAN, string strPrevReceiptNo)
        {

            string strRetString = "";

            string strSortOrder = "ProvisionalReceiptDate, BatchNo";

            DataTable dtTable = Retrieve_TDS_TCS_Statement(strPRN, strTAN, out strRetString);

            
            //if (dtTable != null)
            //{
                //Commented by Shrey Kejriwal on 23/07/2012
                // search datadow by Accepted by TIN
                //DataRow[] dSelectedRow = dtTable.Select("Status <> 'Rejected by TIN'", strSortOrder);
                //DataRow dLastRow = dSelectedRow[dSelectedRow.Length - 1];

                //strRetString = Convert.ToString(dLastRow[2]);

                //string strLastFileNo = dtTable.Rows[dtTable.Rows.Count - 1].ItemArray[0].ToString();


                //foreach (DataRow row in dtTable.Rows)
                //{
                //    if (Convert.ToString(row[0]) == strLastFileNo)
                //    {

                //    }
                //    else
                //        continue;
                //}

                //Added by Shrey Kejriwal on 23/07/2012

                //for (int i = dtTable.Rows.Count; i > 0; i--)
                //{
                //    object[] cells = dtTable.Rows[i - 1].ItemArray;

                //    if (cells[6].ToString() != "Rejected by TIN")
                //    {
                //        strRetString = cells[2].ToString();
                //        break;
                //    }
                //    else
                //        continue;



                //}

            //}


            return true;
        }

        #endregion

        #region Retrieve_TDS_TCS_Statement
        public DataTable Retrieve_TDS_TCS_Statement(string strPRN, string strTAN, out string strServerError)
        {

            strServerError = "";
            DataTable dStatus = null;

            try
            {

                strURL = "https://onlineservices.tin.nsdl.com/TIN/UnAuthorizedView.do";


                //=======================================================================
                StringBuilder strPostData = new StringBuilder();

                strPostData.Append("TAN=" + strTAN);
                strPostData.Append("&PRN=" + strPRN);


                //CONNECT TO SERVER WITH HTTP REQUEST 

                /*  CREATE COOKIES COLLECTION FOR ALL LEVEL HTTP REQUEST
                    CREATING COOKIES CONTAINER OBJECT  */
                CookieContainer cookieContainer = new CookieContainer();

                //-----------------------------------------------------------
                request = (HttpWebRequest)HttpWebRequest.Create(strURL);

                // Set the Method property of the request to POST.
                request.Method = "POST";


                // Create POST data and convert it to a byte array.
                string postData = strPostData.ToString();

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;


                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;



                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                response = (HttpWebResponse)request.GetResponse();


                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();


                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);


                // Read the content.

                strServerResponse = reader.ReadToEnd();


                //CHECKING ANY ERROR FROM SERVER END
                if (IsServerError(strServerResponse, out strServerError))
                {
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    return null;
                }
                //--
                List<string> ltString = new List<string>();
                ltString.Add("(PRN)");
                ltString.Add("Status");
                //--
                dStatus = RetrieveHTMLTableData(strServerResponse, ltString, true);
                
                //--
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch
            {
                throw new Exception();
            }
            //--
            return dStatus;
        }

        #endregion
        
        #endregion

        #region Request_Defaults OLD

        //public bool Request_Defaults(NSDLAuthentication objAuth, ConsolidateData objConsolidate, out string strServerMessage)
        //{
        //    strServerMessage = "";

        //    try
        //    {

        //        //-----------------------------------
        //        // Create a request using a URL that can receive a post.
        //        //-----------------------------------------------------------
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "JSP/security/TanLogin.jsp");

        //        request.CookieContainer = objContainer;
        //        request.KeepAlive = true;


        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        //=======================================================================
        //        StringBuilder strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("userid=" + objAuth.UserID);
        //        strLinkBuilder.Append("&password=" + objAuth.Password);
        //        strLinkBuilder.Append("&tan=" + objAuth.TAN);
        //        strLinkBuilder.Append("&tanuser=yes&usecertificate=false&signature=&userType=USR2");


        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "LogonTAN.do");

        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";


        //        // Create POST data and convert it to a byte array.
        //        string postData = strLinkBuilder.ToString();

        //        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;


        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteArray.Length;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteArray, 0, byteArray.Length);

        //        // Close the Stream object.
        //        dataStream.Close();

        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();

        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();


        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);


        //        // Read the content.

        //        strServerResponse = reader.ReadToEnd();

        //        // Clean up the streams.
        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            return false;
        //        }
        //        //================================================================================
        //        /*2. LEVEL 2
        //        * AFTER SUCCESSFULL LOGIN TRYING TO GET ACCESS REQUEST CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM.
        //        * FETCHING DYNAMIC CREATED CONSOLIDATE TDS/TCS STATEMENT FILL UP FORM'S LINK & MADE REQUEST TO THE SERVER */
        //        //----------------------------------------------------------
        //        strUrl = GetUrlString(strServerResponse, "Defaults.do?ID=", "'");
        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Invalid User Id or Password Provided";
        //            return false;
        //        }

        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

        //        request.KeepAlive = true;
        //        // request.Method = "POST";

        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        /* *************************************************
        //        * CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            return false;
        //        }
        //        else
        //        {
        //            if (Regex.IsMatch(strServerResponse, "No Records Found"))
        //            {
        //                strServerMessage = "Default for this return not found";

        //                strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //                if (!string.IsNullOrEmpty(strUrl))
        //                    LogOff(strBaseURL + strUrl);

        //                return false;
        //            }
        //        }

        //        //GETTING DEFAULTER LIST URL WITH SOME SEARCH CRITERIA
        //        string strSearchString = "FinancialYear='" + objConsolidate.FinYear.Substring(0, 4) + "-" + objConsolidate.FinYear.Substring(4, 2) + "' AND FormNo='" + objConsolidate.FormNo + "' AND Quarter='" + objConsolidate.Quarter + "'";
        //        string strLink = get_Default_Link(strServerResponse, strSearchString, out strServerMessage);

        //        if (string.IsNullOrEmpty(strLink))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            strServerMessage = "No Records Found";

        //            return false;
        //        }
        //        //==========================================================
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strLink);

        //        request.CookieContainer = objContainer;
        //        request.KeepAlive = true;

        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }
        //        request.CookieContainer = objContainer;
        //        request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        response = (HttpWebResponse)request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();

        //        /* *************************************************
        //        * CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

        //            if (!string.IsNullOrEmpty(strUrl))
        //                LogOff(strBaseURL + strUrl);

        //            return false;
        //        }
        //        strUrl = GetUrlString(strServerResponse, "ValidateKYCDtls.do?ID=", "\"");
        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Internal error";

        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }

        //            return false;
        //        }
        //        //-------------------------------------------------------
        //        strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("stmtRRRNo=" + objConsolidate.PRN_No);
        //        strLinkBuilder.Append("&stmtPeriod=" + objConsolidate.Quarter);
        //        strLinkBuilder.Append("&stmtFinyr=" + objConsolidate.FinYear);
        //        //-------------------------------------------------------------
        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";

        //        // Create POST data and convert it to a byte array.
        //        postData = strLinkBuilder.ToString();

        //        byte[] byteData = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;

        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteData.Length;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteData, 0, byteData.Length);

        //        // Close the Stream object.
        //        dataStream.Close();

        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();

        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();

        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);

        //        strServerResponse = reader.ReadToEnd();

        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();
        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }
        //            return false;
        //        }
        //        //------------------------------------------------------
        //        strUrl = GetUrlString(strServerResponse, "SubmitKYC.do?ID", "\"");

        //        if (string.IsNullOrEmpty(strUrl))
        //        {
        //            strServerMessage = "Internal error";

        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }

        //            return false;
        //        }
        //        //-------------------------------------------------------
        //        strLinkBuilder = new StringBuilder();

        //        strLinkBuilder.Append("bsrCode=" + objConsolidate.BSRCode);
        //        strLinkBuilder.Append("&chalnSlNo=" + objConsolidate.ChallanSlNo);
        //        strLinkBuilder.Append("&chalnDate=" + objConsolidate.ChallanDate);
        //        strLinkBuilder.Append("&chalnAmt=" + objConsolidate.ChallanAmount);
        //        strLinkBuilder.Append("&kycPan1=" + objConsolidate.KycPan1);
        //        strLinkBuilder.Append("&kycAmt1=" + objConsolidate.KycAmt1);
        //        strLinkBuilder.Append("&kycPan2=" + objConsolidate.KycPan2);
        //        strLinkBuilder.Append("&kycAmt2=" + objConsolidate.KycAmt2);
        //        strLinkBuilder.Append("&kycPan3=" + objConsolidate.KycPan3);
        //        strLinkBuilder.Append("&kycAmt3=" + objConsolidate.KycAmt3);


        //        request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strUrl);

        //        // Set the Method property of the request to POST.
        //        request.Method = "POST";


        //        // Create POST data and convert it to a byte array.
        //        postData = strLinkBuilder.ToString();

        //        byte[] byteFinalData = Encoding.UTF8.GetBytes(postData);

        //        // Set the ContentType property of the WebRequest.
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.KeepAlive = true;


        //        // Set the ContentLength property of the WebRequest.
        //        request.ContentLength = byteFinalData.Length;


        //        for (int i = 0; i < response.Cookies.Count; i++)
        //        {
        //            response.Cookies[i].Path = String.Empty;
        //        }

        //        request.CookieContainer = objContainer;
        //        request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);

        //        // Get the request stream.
        //        dataStream = request.GetRequestStream();
        //        // Write the data to the request stream.
        //        dataStream.Write(byteFinalData, 0, byteFinalData.Length);

        //        // Close the Stream object.
        //        dataStream.Close();


        //        // Get the response.
        //        response = (HttpWebResponse)request.GetResponse();


        //        // Get the stream containing content returned by the server.
        //        dataStream = response.GetResponseStream();

        //        // Open the stream using a StreamReader for easy access.
        //        reader = new StreamReader(dataStream);
        //        // Read the content.
        //        strServerResponse = reader.ReadToEnd();
        //        //=====================================================================================
        //        reader.Close();
        //        dataStream.Close();
        //        response.Close();
        //        //=====================================================================================

        //        /* *************************************************
        //        * //CHECKING ANY ERROR FROM SERVER END
        //        * ************************************************** */
        //        if (IsServerError(strServerResponse, out strServerMessage))
        //        {
        //            strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //            if (!string.IsNullOrEmpty(strUrl))
        //            {
        //                LogOff(strBaseURL + strUrl);
        //            }

        //            return false;
        //        }

        //        //FETCHING REFERENCE NO.
        //        //==================================================
        //        strServerMessage = GetReferenceNo(strServerResponse, "Your request for", ".<br>");

        //        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //        LogOff(strBaseURL + strUrl);

        //    }
        //    catch
        //    {
        //        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");
        //        LogOff(strBaseURL + strUrl);
        //        strServerMessage = strServerError;
        //        return false;
        //    }

        //    return true;
        //}

        #endregion

        #region GetDataFromPagedTable
        private string GetDataFromPagedTable(string strLink, string strResponse, string strSearchString, out string strServerMessage)
        {
            strServerMessage = "";
            DataTable dtTable = null;
            DataRow dLastRow;
            DataRow[] dSelectedRow;


            try
            {

                if (!string.IsNullOrEmpty(strLink) && string.IsNullOrEmpty(strResponse))
                {

                    request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strLink);
                    request.Timeout = 10000;
                    request.Method = "GET";
                    request.KeepAlive = true;
                    request.AllowAutoRedirect = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    //-----------------------------------------------------
                    for (int i = 0; i < response.Cookies.Count; i++)
                    {
                        response.Cookies[i].Path = String.Empty;
                    }
                    //-----------------------------------------------------
                    request.CookieContainer = objContainer;
                    request.CookieContainer.Add(response.Cookies);

                    // request.CookieContainer.Add(new Uri("https://onlineservices.tin.nsdl.com"), response.Cookies);


                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);

                    strServerResponse = reader.ReadToEnd();
                    //-----------------------------------------------------
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //-----------------------------------------------------
                    //if (IsServerError(strServerResponse, out strServerMessage))
                    //{
                    //    LogOff(strLogOffLink);

                    //}

                    strResponse = strServerResponse;

                    if (IsServerError(strServerResponse, out strServerMessage))
                    {
                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                        if (!string.IsNullOrEmpty(strUrl))
                            LogOff(strBaseURL + strUrl);
                    }
                }

                strLogOffLink = strBaseURL + GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                List<string> ltContainKey = new List<string>();

                ltContainKey.Add("Sr. No.");
                ltContainKey.Add("Quarter");

                DataTable dRTable = RetrieveHTMLTableData(strResponse, ltContainKey, false);

                if (dRTable != null)
                {
                    // search datadow by Reference No
                    //================================================

                    dtTable = dRTable.Clone();

                    dSelectedRow = dRTable.Select(strSearchString);
                    //=================================================
                    foreach (DataRow row in dSelectedRow)
                        dtTable.ImportRow(row);

                    //====================================================
                    if (dtTable.Rows.Count > 0)
                    {
                        dLastRow = dtTable.Rows[0];

                        strRetString = Convert.ToString(dLastRow[8]);

                        if (strRetString != "")
                            strRetString = GetUrlString(strRetString, @"DefSelect.do?ID=", "'>");

                        return strRetString;

                    }
                    else
                    {
                        //=====================================================
                        if (Regex.IsMatch(strServerResponse, "NEXT"))
                        {
                            string strRetrieveLink = GetUrlString(strServerResponse, "href='/TIN/Defaults.do", "</a>");
                            strRetrieveLink = GetUrlString(strRetrieveLink, "Defaults.do", "'>NEXT");

                            GetDataFromPagedTable(strRetrieveLink, "", strSearchString, out strServerMessage);

                        }

                    }
                }

            }
            catch (Exception err)
            {
                LogOff(strLogOffLink);

            }

            return strRetString;

        }

        #endregion

        #region GetDownloadLinkFromPagedTable
        private string GetDownloadLinkFromPagedTable(string strLink, string strResponse, string strSearchString, out string strServerMessage)
        {
            strServerMessage = "";
            DataTable dtTable = null;
            DataRow dLastRow;
            DataRow[] dSelectedRow;
            //--------------------------------------
            try
            {
                if (!string.IsNullOrEmpty(strLink) && string.IsNullOrEmpty(strResponse))
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + strLink);
                    request.Timeout = 10000;
                    request.Method = "GET";
                    request.KeepAlive = true;
                    //request.AllowAutoRedirect = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    //-----------------------------------------------------
                    for (int i = 0; i < response.Cookies.Count; i++)
                    {
                        response.Cookies[i].Path = String.Empty;
                    }
                    //-----------------------------------------------------       

                    request.CookieContainer = objContainer;
                    request.CookieContainer.Add(response.Cookies);
                    //----------------------------------------------------                   
                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);

                    strServerResponse = reader.ReadToEnd();
                    //-----------------------------------------------------
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                   //-----------------------------------------------------
                    //CHEKING IF ANY PRE-DEFINED SERVER ERROR FROM THIS RETURN SERVER RESPONSE
                    if (IsServerError(strServerResponse, out strServerMessage))
                    {
                        strUrl = GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                        if (!string.IsNullOrEmpty(strUrl))
                            LogOff(strBaseURL + strUrl);

                        return "";
                    }

                    strResponse = strServerResponse;
                }
                               

                // RETRIEVE LOG OFF LINK FROM RESPONSE STRING
                strLogOffLink = strBaseURL + GetUrlString(strServerResponse, "Log-Off.do?ID=", "',");

                // COLUMN NAME OF HTML TABLE FOR SEARCHING OUR DOWNLOADABLE LINK TABLES.
                List<string> ltContainKey = new List<string>();

                ltContainKey.Add("Sr. No.");
                ltContainKey.Add("Quarter");


                //POPULATE HTML TABLE INTO DATA TABLE FOR SEARCHING DOWNLOAD LINK BY REFERENCE NO & FORM NO
                DataTable dRTable = RetrieveHTMLTableData(strResponse, ltContainKey, false);

                if (dRTable != null)
                {
                    // search datadow by Reference No
                    //================================================

                    dtTable = dRTable.Clone();  // MAKING A CLONE TABLE STUCTURE
                    dSelectedRow = dRTable.Select(strSearchString);
                    //=================================================
                    foreach (DataRow row in dSelectedRow)
                        dtTable.ImportRow(row);

                    //IF SEARCHING DOWNLOAD LINK IS FOUND ELSE LOOKING FOR ANY PAGINATION 
                    //====================================================
                    if (dtTable.Rows.Count > 0)
                    {
                        dLastRow = dtTable.Rows[0];

                        strRetString = Convert.ToString(dLastRow[9]);

                        if (strRetString != "")
                        {
                            if (strRetString.Contains("Under Process at NSDL"))
                            {
                                return strRetString;
                            }

                            strRetString = GetUrlString(strRetString, @"reqDownloadFile.do?ID=", "\" onclick");
                        }
                        return strRetString;

                    }
                    else
                    {   // MATCHING NEXT KEYWORDS IF ANY PAGE AVAILABLE
                        //=====================================================
                        if (Regex.IsMatch(strServerResponse, "NEXT"))
                        {
                            //FINDING NEXT BUTTON LINK IF FOUND THEN IT CALL TO ITSELF RECURSIVELY UNTIL LAST PAGE.

                            string strRetrieveLink = GetUrlString(strServerResponse, "href='/TIN/requestsAll.do", "NEXT</a>");
                            strRetrieveLink = GetNextUrlString(strRetrieveLink, "requestsAll.do", "'>");

                            //strRetrieveLink = strRetrieveLink.Substring(strRetrieveLink.LastIndexOf("requestsAll.do"

                            //CALL TO ITSELF WITH NEXT PAGE LINK
                            GetDownloadLinkFromPagedTable(strRetrieveLink, "", strSearchString, out strServerMessage);

                        }

                    }
                }

            }
            catch (Exception err)
            {
                LogOff(strLogOffLink);
            }

            return strRetString;

        }

        #endregion

    }

}
