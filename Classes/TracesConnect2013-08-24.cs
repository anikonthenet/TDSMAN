

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

    public class TracesConnect
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

        string strBaseURL = "https://www.tdscpc.gov.in/app/";
        //string strBaseURL = "https://www.tdscpc.gov.in/app/ded/srv/";
        //-- ANIK 2013-07-13
        string strCaptchBaseURL = "https://www.tdscpc.gov.in/app/srv/";

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

        #region TracesConnect
        public TracesConnect()
        {

        }

        #endregion

        #region IsChallanExistsInConsolidate
        public TracesResponse IsChallanExistsInConsolidate(TracesData objTraceData, TracesLogin objLogin)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            StringBuilder sbParameter;
            /* --------------------------------------------------------------------
              1.> REQUEST FOR LOGIN INTO TRACES SITES
                  URL :: https://www.tdscpc.gov.in/app/login.xhtml
               --------------------------------------------------------------------*/
            if (!this.bnlSessionExists)
            {
                objResponse = this.makeLoginToTRACES(objLogin);
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
            }
            /*--------------------------------------------------------------------
              2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                  URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
              --------------------------------------------------------------------*/
            strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/nsdlconsofile.xhtml");

            //CHECKING ANY ERROR FROM SERVER POINT
            if (!IsStringExists(strServerResponse, "//form[@id=\"requestnsdlconsoForm\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }

            objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            //------------------------------------------------------------
            sbParameter = new StringBuilder();
            sbParameter.Append("finYr=" + objTraceData.FAYear);
            sbParameter.Append("&qrtr=" + objTraceData.Quarter);
            sbParameter.Append("&frmType=" + objTraceData.Forms);
            sbParameter.Append("&download_conso=Go");
            sbParameter.Append("&requestnsdlconsoForm_SUBMIT=1");
            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"requestnsdlconsoForm\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }
            //----------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
            {
                if (pair.Key == "javax.faces.ViewState")
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            }
            /* --------------------------------------------------------------------
              3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
             *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
             * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
               --------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/nsdlconsofile.xhtml", sbParameter);

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
            //fetching hidden field isChlnNil's value
            objResponse.CustomeTypes = objNameval;
            //string strVal = "";
            ////----------------------------------------------------------
            //foreach (KeyValuePair<string, string> pair in objNameval)
            //{
            //    if (pair.Key == "isChlnNil")
            //        strVal = pair.Value;                
            //}
            ////-------------------------------------------------
            //if (!string.IsNullOrEmpty(strVal))
            //{
            //    if (strVal.ToUpper() == "TRUE")                
            //        objResponse.CustomeTypes = true;                
            //    else
            //        objResponse.CustomeTypes = true;   
            //}

            return objResponse;
        }

        #endregion

        #region IsChallanExistsInDefaults
        public TracesResponse IsChallanExistsInDefaults(TracesData objTraceData, TracesLogin objLogin)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            StringBuilder sbParameter;
            /* --------------------------------------------------------------------
              1.> REQUEST FOR LOGIN INTO TRACES SITES
                  URL :: https://www.tdscpc.gov.in/app/login.xhtml
               --------------------------------------------------------------------*/
            objResponse = this.makeLoginToTRACES(objLogin);
            if (objResponse.Respons == enmResponse.Failed)
                return objResponse;
            /*--------------------------------------------------------------------
              2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                  URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
              --------------------------------------------------------------------*/
            strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/justrepdwnld.xhtml");

            //CHECKING ANY ERROR FROM SERVER POINT
            if (!IsStringExists(strServerResponse, "//form[@id=\"justificationForm\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }


            objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            //------------------------------------------------------------
            sbParameter = new StringBuilder();
            sbParameter.Append("finYr=" + objTraceData.FAYear);
            sbParameter.Append("&qrtr=" + objTraceData.Quarter);
            sbParameter.Append("&frmType=" + objTraceData.Forms);
            sbParameter.Append("&download_justReport=Go");
            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"justificationForm\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }
            //----------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
            {
                if (pair.Key == "javax.faces.ViewState")
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            }
            /* --------------------------------------------------------------------
              3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
               --------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/justrepdwnld.xhtml", sbParameter);

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
            //fetching hidden field isChlnNil's value
            objResponse.CustomeTypes = objNameval;

            return objResponse;
        }

        #endregion

        #region IsChallanExistsInForm16A
        public TracesResponse IsChallanExistsInForm16A(TracesData objTraceData, TracesLogin objLogin)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            StringBuilder sbParameter;
            /* --------------------------------------------------------------------
              1.> REQUEST FOR LOGIN INTO TRACES SITES
                  URL :: https://www.tdscpc.gov.in/app/login.xhtml
               --------------------------------------------------------------------*/
            objResponse = this.makeLoginToTRACES(objLogin);
            if (objResponse.Respons == enmResponse.Failed)
                return objResponse;
            /*--------------------------------------------------------------------
              2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                  URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
              --------------------------------------------------------------------*/
            strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

            //CHECKING ANY ERROR FROM SERVER POINT
            if (!IsStringExists(strServerResponse, "//form[@id=\"bulkSearch\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }


            objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            //------------------------------------------------------------
            sbParameter = new StringBuilder();
            sbParameter.Append("dwnldFormBulkType=14");
            sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
            sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
            sbParameter.Append("&bulkformType=" + objTraceData.Forms);
            sbParameter.Append("&bulkGo=Go");
            sbParameter.Append("&bulkSearch_SUBMIT=1");
            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"bulkSearch\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }
            //----------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
            {
                if (pair.Key == "javax.faces.ViewState")
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            }
            /* --------------------------------------------------------------------
              3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
               --------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);

            objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                //objResponse.ErrorMessage = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }
            //------------------------------------------------------------------------- 
            sbParameter = new StringBuilder();
            sbParameter.Append("j_id1972728517_7cc7de5f=submit");

            foreach (KeyValuePair<string, string> pair in objNameval)
                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            //-------------------------------------------------------------------------
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

            if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
            //fetching hidden field isChlnNil's value
            objResponse.CustomeTypes = objNameval;

            return objResponse;
        }

        #endregion


        #region NoValidPANdeductee
        public TracesResponse NoValidPANdeductee(TracesData objTraceData)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            StringBuilder sbParameter;
            /*--------------------------------------------------------------------
              2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                  URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
              --------------------------------------------------------------------*/
            strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/nsdlconsofile.xhtml");

            //CHECKING ANY ERROR FROM SERVER POINT
            if (!IsStringExists(strServerResponse, "//form[@id=\"requestnsdlconsoForm\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }

            objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            //------------------------------------------------------------
            sbParameter = new StringBuilder();
            sbParameter.Append("finYr=" + objTraceData.FAYear);
            sbParameter.Append("&qrtr=" + objTraceData.Quarter);
            sbParameter.Append("&frmType=" + objTraceData.Forms);
            sbParameter.Append("&download_conso=Go");
            sbParameter.Append("&requestnsdlconsoForm_SUBMIT=1");
            /* -----------------------------------------------------------------------
               RETRIEVE VIEWSTATE DATA                
               --------------------------------------------------------------------*/
            Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"requestnsdlconsoForm\"]");
            //CHECKING ANY ERROR
            if (objNameval.Count <= 0)
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.SessionTimeout;
                return objResponse;
            }
            //----------------------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
            {
                if (pair.Key == "javax.faces.ViewState")
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            }
            /* --------------------------------------------------------------------
              3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
             *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
             * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
               --------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/nsdlconsofile.xhtml", sbParameter);

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
            //fetching hidden field isChlnNil's value          
            //-------------------------------------------------          
            objResponse.CustomeTypes = objNameval;

            return objResponse;
        }

        #endregion

        #region RequestForAllDownloadList
        public TracesResponse RequestForAllDownloadList(out DataTable table)
        {
            table = null;
            string strResponse = "";

            TracesResponse objResponse = new TracesResponse();

            try
            {
                //1.> REQUEST FOR LOGIN PAGE
                // strResponse = makeLoginToTRACES1(objLogin);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                //if (objResponse.Respons == enmResponse.Failed)
                //    return objResponse;
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/filedownload.xhtml");

                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //-----------------------------------------------------------------------
                //-- ANIK 2013-07-13
                //strResponse = makeHTTPJSONRequest(strBaseURL + "GetReqListServlet?reqtype=0&rows=100&sord=asc");
                strResponse = makeHTTPJSONRequest(strBaseURL + "srv/GetReqListServlet?reqtype=0&rows=100&sord=asc");
                if (!string.IsNullOrEmpty(strResponse))
                    table = JsonParser(strResponse);


                //strURL = "https://www.tdscpc.gov.in/app/GetReqListServlet?reqtype=2&reqNo=289626&_search=false&nd=1360324346192&rows=10&page=1&sidx=reqNo&sord=asc";
                //strURL = "https://www.tdscpc.gov.in/app/GetReqListServlet?reqtype=2&reqNo=289626";


                //string post = "reqtype=2&reqNo=289626";

                //request = (HttpWebRequest)WebRequest.Create("https://www.tdscpc.gov.in/app/GetReqListServlet?reqtype=0&rows=100&sord=asc");
                //string json = "";


                //request.ServicePoint.Expect100Continue = false;
                //request.Method = "GET";
                //request.ContentType = "application/json";
                //request.Headers.Add("Cache-Control", "no-cache");

                //if (request.CookieContainer == null)
                //    request.CookieContainer = objContainer;

                //response = (HttpWebResponse)request.GetResponse();
                //request.CookieContainer.Add(response.Cookies);
                ////--------------------------------------------------------------------
                //using (Stream responseStream = response.GetResponseStream())
                //{
                //    using (StreamReader responseReader = new StreamReader(responseStream))                    
                //        json = responseReader.ReadToEnd();             
                //}
                //--------------------------------------------------------------------
                //-----------------------------------
                //  return strServerResponse;




                // strResponse = HttpPOST(strBaseURL + "ded/nsdlconsofile.xhtml", sbParameter.ToString());
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                // ---------------------------------------------------------------------

            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }
            return objResponse;
        }

        #endregion

        #region RequestForDownloadListByReqNo
        public TracesResponse RequestForDownloadListByReqNo(string ReqNo, out DataTable table)
        {
            table = null;
            string strResponse = "";

            TracesResponse objResponse = new TracesResponse();

            try
            {
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/filedownload.xhtml");
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //-----------------------------------------------------------------------
                // string s = "https://www.tdscpc.gov.in/app/GetReqListServlet?reqtype=1&frmDate=01-Jan-2013&toDate=12-Feb-2013&rows=10&sord=asc";
                // https://www.tdscpc.gov.in/app/GetReqListServlet?reqtype=2&reqNo=310241&rows=10&sord=asc

                strResponse = makeHTTPJSONRequest(strBaseURL + "GetReqListServlet?reqtype=2&reqNo=" + ReqNo + "&rows=100&sord=asc");
                if (!string.IsNullOrEmpty(strResponse))
                    table = JsonParser(strResponse);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                // ---------------------------------------------------------------------              

            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }
            return objResponse;
        }

        #endregion

        #region RequestForDownloadListByDate
        public TracesResponse RequestForDownloadListByDate(string FromDate, string ToDate, out DataTable table)
        {
            table = null;
            string strResponse = "";

            TracesResponse objResponse = new TracesResponse();

            try
            {
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/filedownload.xhtml");
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //-----------------------------------------------------------------------             
                strResponse = makeHTTPJSONRequest(strBaseURL + "GetReqListServlet?reqtype=1&frmDate=" + FromDate + "&toDate=" + ToDate + "&rows=100&sord=asc");
                if (!string.IsNullOrEmpty(strResponse))
                    table = JsonParser(strResponse);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                // ---------------------------------------------------------------------

                // this.Logoff();

            }
            catch (Exception err)
            {
                this.Logoff();

                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }
            return objResponse;
        }

        #endregion

        #region RequestForDownloadFile
        public TracesResponse RequestForDownloadFile(string strReqNo, string strPath)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            //----------------------------------------------------
            try
            {
                //-- ANIK 2013-07-13
                //string strDownloadLink = "DownloadServlet?reqNo=" + strReqNo;
                string strDownloadLink = "srv/DownloadServlet?reqNo=" + strReqNo;

                //1.> REQUEST FOR LOGIN PAGE
                // strResponse = makeLoginToTRACES1(objLogin);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                //if (objResponse.Respons == enmResponse.Failed)
                //    return objResponse;
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                //  strResponse = makeHTTPGetRequest(strBaseURL + "ded/filedownload.xhtml");
                //-----------------------------------------------------------------------
                //---------------------------------------------------------
                strURL = GetFileLocation(strBaseURL + strDownloadLink);

                if (Regex.IsMatch(strURL, "ibm_security_logout"))
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Message = "Session Timeout Login again !!";
                    return objResponse;
                }
                //--------------------------------------------------
                if (string.IsNullOrEmpty(strURL))
                {
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //------------------------------------------------
                if (!makeHttpDownloadRequest(strURL, strPath))
                    objResponse.Respons = enmResponse.Failed;



            }
            catch (Exception err)
            {
                objResponse.Message = err.Message;
                objResponse.Respons = enmResponse.Failed;
            }
            return objResponse;

        }

        #endregion

        #region RequestForPANValidation
        public TracesResponse RequestForPANValidation(string strPAN, string FormNo)
        {
            TracesResponse objResponse = new TracesResponse();
            string strResponse = "";
            //----------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //------------------------------------------------------------------------
                StringBuilder sbParameter = new StringBuilder();
                sbParameter.Append("pannumber=" + strPAN);
                sbParameter.Append("&frmType1=" + FormNo);
                sbParameter.Append("&clickGo1=Go");
                sbParameter.Append("&pandetailsForm1_SUBMIT=1");
                /* -----------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"pandetailsForm1\"]");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if ("javax.faces.ViewState" == pair.Key)
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                /* --------------------------------------------------------------------
                  3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //------------------------------------------------------------------------
                objNameval = this.RetievePANStatus(strResponse);

                PANDetails objPan = new PANDetails();

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key.ToString().ToUpper() == "STATUS")
                    {
                        if (pair.Value.ToString().ToUpper() == "VALID")
                            objPan.Status = Message.Valid;
                        else
                            objPan.Status = Message.Invalid;
                    }

                    if (pair.Key.ToString().ToUpper() == "NAME")
                        objPan.Name = pair.Value;

                }
                //----------------------------------------------------
                objResponse.CustomeTypes = objPan;


            }
            catch (Exception err)
            {
                objResponse.Message = err.Message;
                objResponse.Respons = enmResponse.Failed;
            }

            //----------------------------------------
            return objResponse;
        }


        #endregion

        #region RequestForStatusofStatementFile
        public TracesResponse RequestForStatusofStatementFile(TracesData objData, out DataTable table)
        {
            string json = "";
            table = null;
            //MAKE GET REQUEST
            //--------------------------------------------------
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            //--------------------------------------------------
            try
            {
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/stmtstatus.xhtml");
                //-------------------------------------------------- 
                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strServerResponse, "//form[@id=\"stmtFiledStatus\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //--------------------------------------------------------------------
                StringBuilder objParam = new StringBuilder();
                objParam.Append("_search=false");
                objParam.Append("&rows=100");
                objParam.Append("&page=1");
                objParam.Append("&sidx=");
                objParam.Append("&sord=asc");
                //--------------------------------------------------
                //-- ANIK 2013-07-13
                //json = makeHTTPPostRequest(strBaseURL + "DedStmtStatusServlet?financialYear=" + objData.FAYear + "&quarter=" + objData.Quarter + "&formType=" + objData.Forms, objParam);
                json = makeHTTPPostRequest(strBaseURL + "ded/srv/DedStmtStatusServlet?financialYear=" + objData.FAYear + "&quarter=" + objData.Quarter + "&formType=" + objData.Forms, objParam);
                //--------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE

                if (!string.IsNullOrEmpty(json))
                {
                    table = new DataTable();
                    table.Columns.Add("Token Number");
                    table.Columns.Add("Finnancial Year");
                    table.Columns.Add("Statement Type");
                    table.Columns.Add("Form Type");
                    table.Columns.Add("Quarter");
                    table.Columns.Add("Date of Filling");
                    table.Columns.Add("Date of Processing");
                    table.Columns.Add("Status");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
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
                                    //------------------------------------------------------
                                    switch (strLastToken)
                                    {
                                        case "finyear":
                                            dRow = table.NewRow();
                                            dRow["Finnancial Year"] = reader.Value.ToString();
                                            break;
                                        case "quarter":
                                            dRow["Quarter"] = reader.Value.ToString();
                                            break;
                                        case "formtype":
                                            dRow["Form Type"] = reader.Value.ToString();
                                            break;
                                        case "tokenno":
                                            dRow["Token Number"] = reader.Value.ToString();
                                            break;
                                        case "dtoffiling":
                                            dRow["Date of Filling"] = reader.Value.ToString();
                                            break;
                                        case "status":
                                            dRow["Status"] = reader.Value.ToString();
                                            break;
                                        case "dtofprcng":
                                            dRow["Date of Processing"] = reader.Value.ToString();
                                            break;
                                        case "stmnttype":
                                            dRow["Statement Type"] = Convert.ToString(reader.Value);
                                            table.Rows.Add(dRow);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = err.Message;
            }

            return objResponse;
        }

        #endregion


        #region RequestForChallanStatusQuery1
        public TracesResponse RequestForChallanStatusQuery1(TracesData objData)
        {
            DataTable table = new DataTable();
            //MAKE GET REQUEST
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            //------------------------------------------------
            try
            {
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/challanstatusquery.xhtml");
                //----------------------------------------------------------------------------------
                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strServerResponse, "//form[@id=\"chlnStatusForm1\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------
                StringBuilder objBuilder = new StringBuilder();

                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=");
                objBuilder.Append("&sord=");
                //------------------------------------------------
                // MAKE JSON REQUEST  requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //-- ANIK 2013-07-13
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Date of Deposit");
                    table.Columns.Add("Challan Serial Number");
                    table.Columns.Add("Challan Status");

                    table.Columns.Add("chllan Amount");
                    table.Columns.Add("Recipt Number");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
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
                                    //------------------------------------------------------
                                    switch (strLastToken)
                                    {
                                        case "dateOfDep":
                                            dRow = table.NewRow();
                                            dRow["Date of Deposit"] = reader.Value.ToString();
                                            break;
                                        case "chlnSNo":
                                            dRow["Challan Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;
                                        case "chlnAmt":
                                            dRow["chllan Amount"] = reader.Value.ToString();
                                            break;
                                        case "recptNum":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            table.Rows.Add(dRow);
                                            break;

                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
                //-----------------------------------------------
                objResponse.CustomeTypes = table;
            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = err.Message;
            }
            return objResponse;

        }

        #endregion

        #region RequestForChallanStatusQuery2
        public TracesResponse RequestForChallanStatusQuery2(TracesData objData)
        {
            StringBuilder objBuilder = new StringBuilder();
            DataTable table = new DataTable();
            //MAKE GET REQUEST
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;


            //MAKE GET REQUEST
            try
            {
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/challanstatusquery.xhtml");
                //----------------------------------------------------------------------------------
                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strServerResponse, "//form[@id=\"chlnStatusForm2\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------
                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=");
                objBuilder.Append("&sord=");
                //------------------------------------------------
                //https://www.tdscpc.gov.in/app/ChlnStatusServlet?reqtype=1&bsrCode=2906779&chlnSNo=93583&chlnAmt=794.00&dateOfDep=31-Mar-2013

                // MAKE JSON REQUEST  requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //-- ANIK 2013-07-13
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=1&bsrCode=" + objData.BSRCode + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=1&bsrCode=" + objData.BSRCode + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Date of Deposit");
                    table.Columns.Add("Challan Serial Number");
                    table.Columns.Add("Challan Status");

                    table.Columns.Add("chllan Amount");
                    table.Columns.Add("Recipt Number");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
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
                                    //------------------------------------------------------
                                    switch (strLastToken)
                                    {
                                        case "dateOfDep":
                                            dRow = table.NewRow();
                                            dRow["Date of Deposit"] = reader.Value.ToString();
                                            break;
                                        case "chlnSNo":
                                            dRow["Challan Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;
                                        case "chlnAmt":
                                            dRow["chllan Amount"] = reader.Value.ToString();
                                            break;
                                        case "recptNum":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            table.Rows.Add(dRow);
                                            break;

                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
                //-----------------------------------------------
                objResponse.CustomeTypes = table;

            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = err.Message;
            }

            return objResponse;

        }


        #endregion


        #region RequestForConsumptionDetails
        public TracesResponse RequestForConsumptionDetails(TracesData objData)
        {
            StringBuilder objBuilder = new StringBuilder();
            DataTable table = new DataTable();
            //MAKE GET REQUEST
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;


            //MAKE GET REQUEST
            try
            {
                //strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/challanstatusquery.xhtml");
                ////----------------------------------------------------------------------------------
                ////CHECKING ANY ERROR FROM SERVER POINT
                //if (!IsStringExists(strServerResponse, "//form[@id=\"chlnStatusForm2\"]"))
                //{
                //    objResponse.Message = "Server Error";
                //    objResponse.Respons = enmResponse.SessionTimeout;
                //    return objResponse;
                //}
                //------------------------------------------------
                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=tokenNum");
                objBuilder.Append("&sord=desc");
                //------------------------------------------------
                //https://www.tdscpc.gov.in/app/ChlnStatusServlet?reqtype=2&recptNum=290705784&chlnSNo=93554&chlnAmt=217426.00

                // MAKE JSON REQUEST  requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount , objBuilder);
                //-- ANIK 2013-07-13
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    int intRowCount = 0;
                    table = new DataTable();
                    table.Columns.Add("Token Number");
                    table.Columns.Add("Finnancial Year");
                    table.Columns.Add("Quarter");

                    table.Columns.Add("Form Type");
                    table.Columns.Add("Claimed Amount");
                    table.Columns.Add("Challan Status");
                    table.Columns.Add("Excess Amount Claimed");
                    table.Columns.Add("Available Amount");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
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
                                    //------------------------------------------------------
                                    switch (strLastToken)
                                    {
                                        case "rowCount":
                                            intRowCount = Convert.ToInt32(reader.Value);
                                            break;

                                        case "tokenNum":
                                            dRow = table.NewRow();
                                            dRow["Token Number"] = reader.Value.ToString();
                                            break;
                                        case "finYr":
                                            dRow["Finnancial Year"] = reader.Value.ToString();
                                            break;
                                        case "qtr":
                                            dRow["Quarter"] = reader.Value.ToString();
                                            break;
                                        case "formType":
                                            dRow["Form Type"] = reader.Value.ToString();
                                            break;
                                        case "claimAmt":
                                            dRow["Claimed Amount"] = reader.Value.ToString();

                                            break;

                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;

                                        case "excessAmt":
                                            dRow["Excess Amount Claimed"] = reader.Value.ToString();
                                            break;

                                        case "availAmt":

                                            if (intRowCount > 0)
                                            {
                                                dRow["Available Amount"] = reader.Value.ToString();
                                                table.Rows.Add(dRow);
                                            }

                                            break;

                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
                //-----------------------------------------------
                objResponse.CustomeTypes = table;

            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                    objResponse.Respons = enmResponse.SessionTimeout;
                else
                    objResponse.Respons = enmResponse.Failed;

                objResponse.Message = err.Message;
            }

            return objResponse;

        }


        #endregion


        #region RequestForDeductionDetailsForDeductee
        public TracesResponse RequestForDeductionDetailsForDeductee(TracesData objData)
        {
            TracesResponse objResponse = new TracesResponse();
            Deductor objDeductor = new Deductor();

            //MAKE GET REQUEST

            try
            {

                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/tdstcscredit.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strServerResponse, "//form[@id=\"viewTdsTcsCredit\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                StringBuilder sbParameter = new StringBuilder();
                sbParameter.Append("pan=" + objData.PAN1);
                sbParameter.Append("&financialYear=" + objData.FAYear);
                sbParameter.Append("&quarter=" + objData.Quarter);
                sbParameter.Append("&formType=" + objData.Forms);
                sbParameter.Append("&clickGo=1");

                objDeductor.DeducteePAN = objData.PAN1;
                /* -----------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"viewTdsTcsCredit\"]");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    switch (pair.Key)
                    {
                        case "tan":
                            sbParameter.Append("&tan=" + pair.Value);
                            objDeductor.TAN = pair.Value;
                            break;

                        case "currYear":
                            sbParameter.Append("&currYear=" + pair.Value);
                            break;

                        case "currQtr":
                            sbParameter.Append("&currQtr=" + pair.Value);
                            break;

                        case "viewTdsTcsCredit_SUBMIT":
                            sbParameter.Append("&viewTdsTcsCredit_SUBMIT=" + pair.Value);
                            break;

                        case "javax.faces.ViewState":
                            sbParameter.Append("&javax.faces.ViewState=" + HttpUtility.UrlEncode(pair.Value));
                            break;
                    }

                }
                //-------------------------------------------------------------------------------------------
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/tdstcscredit.xhtml", sbParameter);
                //-----------------------------------------------------------------------
                //CHECKING ANY ERROR
                objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //--------------------------------------------------------------          
                objDeductor.AssessmentYear = RetrieveElementValue(strServerResponse, "//form[@id=\"tabStmtChlnData\"]", "//span[@id=\"stmtDetail_assyear\"]", enmElementType.InnerText);
                objDeductor.RegularStatement = RetrieveElementValue(strServerResponse, "//form[@id=\"tabStmtChlnData\"]", "//span[@id=\"stmtDetail_tokNoRegStmt\"]", enmElementType.InnerText);
                objDeductor.CorrectionStatement = RetrieveElementValue(strServerResponse, "//form[@id=\"tabStmtChlnData\"]", "//span[@id=\"stmtDetail_tokNoLstStmt\"]", enmElementType.InnerText);
                string strStmtMstrId = RetrieveElementValue(strServerResponse, "//form[@id=\"dispMsg\"]", "//input[@id=\"stmtMstrId\"]", enmElementType.Value);
                //----------------------------------------------------------------------------
                //2.>> 2nd REQUEST TO FILL GRID
                //------------------------------------------------
                StringBuilder objBuilder = new StringBuilder();

                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=");
                objBuilder.Append("&sord=asc");
                //------------------------------------------------
                // MAKE JSON REQUEST  requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/DedTdsTcsSevlet?pan=" + objData.PAN1 + "&stmtMstrId=" + strStmtMstrId + "&finYear=" + objData.FAYear + "&quarter=" + objData.Quarter, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                DataTable table = null;
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Deductee Detail Record Number");
                    table.Columns.Add("Section Code");
                    table.Columns.Add("Rate of Deduction (%)");

                    table.Columns.Add("Transaction Amount");
                    table.Columns.Add("Date of Transaction");

                    table.Columns.Add("Tax Deducted / Collected");
                    table.Columns.Add("Date of Deduction");
                    table.Columns.Add("Tax Deposited");
                    table.Columns.Add("Status of Booking");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
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
                                    //------------------------------------------------------
                                    switch (strLastToken)
                                    {
                                        case "dedNam":
                                            objDeductor.Deductee = reader.Value.ToString();

                                            break;
                                        case "rowCount":
                                            objDeductor.NoOfDetailsRecord = Convert.ToInt32(reader.Value);
                                            break;
                                        case "dedDetRNo":
                                            dRow = table.NewRow();
                                            dRow["Deductee Detail Record Number"] = reader.Value.ToString();
                                            break;
                                        case "secCode":
                                            dRow["Section Code"] = reader.Value.ToString();
                                            break;
                                        case "dedRate":
                                            dRow["Rate of Deduction (%)"] = reader.Value.ToString();

                                            break;

                                        case "transAmnt":
                                            dRow["Transaction Amount"] = reader.Value.ToString();

                                            break;

                                        case "transDate":
                                            dRow["Date of Transaction"] = reader.Value.ToString();

                                            break;

                                        case "taxDeducted":
                                            dRow["Tax Deducted / Collected"] = reader.Value.ToString();

                                            break;

                                        case "deductionDate":
                                            dRow["Date of Deduction"] = reader.Value.ToString();

                                            break;

                                        case "taxDeposited":
                                            dRow["Tax Deposited"] = reader.Value.ToString();

                                            break;
                                        case "status":
                                            dRow["Status of Booking"] = reader.Value.ToString();
                                            table.Rows.Add(dRow);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
                //-----------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(objDeductor);
                objList.Add(table);
                //-----------------------------------------------
                objResponse.CustomeTypes = objList;
                //-----------------------------------------------
            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }
            return objResponse;

        }

        #endregion

        #region RequestNSDLConsoFile
        public TracesResponse RequestForNSDLConsoFile(TracesLogin objLogin, TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();

            try
            {
                /* --------------------------------------------------------------------
                  1.> REQUEST FOR LOGIN INTO TRACES SITES
                      URL :: https://www.tdscpc.gov.in/app/login.xhtml
                   --------------------------------------------------------------------*/
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTRACES(objLogin);
                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/nsdlconsofile.xhtml");

                //string strErrMessage = RetrieveElementValue(strResponse, "//div[@class=\"padLeft5 margintop20\"]", "//span[@class=\"boldFont\"]", enmElementType.InnerText);

                //if (!string.IsNullOrEmpty(strErrMessage))
                //{
                //    objResponse.Message = strErrMessage;
                //    objResponse.Respons = enmResponse.Failed;
                //    return objResponse;
                //}

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"requestnsdlconsoForm\"]"))
                {
                    string strErrMessage = RetrieveElementValue(strResponse, "//div[@class=\"padLeft5 margintop20\"]", "//span[@class=\"boldFont\"]", enmElementType.InnerText);

                    if (!string.IsNullOrEmpty(strErrMessage))
                    {
                        objResponse.Message = strErrMessage;
                        objResponse.Respons = enmResponse.Failed;
                        return objResponse;
                    }
                    else
                    {
                        //--
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                }

                objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    this.Logoff();
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("finYr=" + objTraceData.FAYear);
                sbParameter.Append("&qrtr=" + objTraceData.Quarter);
                sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&download_conso=Go");
                //sbParameter.Append("&requestnsdlconsoForm_SUBMIT=1");
                /* -----------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"requestnsdlconsoForm\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    this.Logoff();
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //----------------------------------------------------------

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                /* --------------------------------------------------------------------
                  3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/nsdlconsofile.xhtml", sbParameter);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    this.Logoff();
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    this.Logoff();
                    this.bnlSessionExists = false;
                    objResponse.Message = "Please Enter Valid Finnancial Details";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                //if (objResponse.Respons == enmResponse.Failed)
                //    return objResponse;
                /* --------------------------------------------------------------------
                 4.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
                *    CHALLAN DETAILS PARAMETER PREPARATION
                * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                  --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
                sbParameter.Append("&stmtSpecKyc=1");
                // sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&bforeLogin=3");
                //sbParameter.Append("&finYr=" + objTraceData.FAYear);
                //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

                //sbParameter.Append("&tan=" );

                sbParameter.Append("&token=" + objTraceData.PRN_NO);

                // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
                //sbParameter.Append("&dedCount=2");

                //FOR NILL RETURNS
                if (objTraceData.IsNoChallanCheck)
                    sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
                sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

                // FOR BOOK ADJUSTMENT
                sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
                sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

                //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
                if (objTraceData.panAmtValueCheck)
                    sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
                sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

                if (!objTraceData.IsNoChallanCheck)
                {
                    sbParameter.Append("&bsr=" + objTraceData.BSRCode);
                    sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
                    sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
                    sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
                }

                if (!objTraceData.panAmtValueCheck)
                {
                    sbParameter.Append("&pan1=" + objTraceData.PAN1);
                    sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
                    sbParameter.Append("&pan2=" + objTraceData.PAN2);
                    sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
                    sbParameter.Append("&pan3=" + objTraceData.PAN3);
                    sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
                }
                //--------------------------------------------------------------
                sbParameter.Append("&clickKYC=Proceed");
                sbParameter.Append("&dedkyc_SUBMIT=1");
                //--------------------------------------------------------------------------------------------------------------
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "qrtr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "frmType")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "tan")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "isChlnNil")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "dedCount")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "bkEntryValue")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                    //else
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);
                }
                //---------------------------------------------------------------
                if (objNameval.Count <= 0)
                {
                    this.Logoff();
                    this.bnlSessionExists = false;
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //---------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {

                    this.bnlSessionExists = false;
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    this.Logoff();
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //---------------------------------------------------------------------------------------------------------
                string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
                sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
                sbParameter.Append("&dedkyc_SUBMIT=1");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //-------------------------------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("fy=" + pair.Value);

                    if (pair.Key == "qrtr")
                    {
                        if (pair.Value == "")
                            sbParameter.Append("&qr=0");
                        else
                            sbParameter.Append("&qr=" + pair.Value);

                    }

                    if (pair.Key == "formType")
                        sbParameter.Append("&ft=" + pair.Value);

                    if (pair.Key == "dwldType")
                        sbParameter.Append("&dt=" + pair.Value);

                }
                //-------------------------------------------------------------------------------------------------
                //-- ANIK 2013-07-13
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                //-- ANIK 2013-07-27
                //strResponse = makeHTTPGetRequest(strBaseURL + "ded/srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                strResponse = makeHTTPGetRequest(strBaseURL + "srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    RequestStatus conso = new RequestStatus();
                    conso.AuthenticationCode = strAuthenCode;
                    conso.StatusMessage = strMessage;

                    objResponse.CustomeTypes = conso;
                    objResponse.Respons = enmResponse.Success;
                }
                //---------------------------------------------------
                Logoff();

            }
            catch (Exception err)
            {
                this.Logoff();
                this.bnlSessionExists = false;
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
            }
            return objResponse;
        }

        #endregion

        #region RequestForJustificationReportDownload
        public TracesResponse RequestForJustificationReportDownload(TracesLogin objLogin, TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                 1.> REQUEST FOR LOGIN INTO TRACES SITES
                     URL :: https://www.tdscpc.gov.in/app/login.xhtml
                  --------------------------------------------------------------------*/
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTRACES(objLogin);
                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/justrepdwnld.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"justificationForm\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();
                    return objResponse;
                }
                objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    this.Logoff();
                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("finYr=" + objTraceData.FAYear);
                sbParameter.Append("&qrtr=" + objTraceData.Quarter);
                sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&download_justReport=Go");
                /* -----------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"justificationForm\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();
                    return objResponse;
                }
                //----------------------------------------------------------

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                /* --------------------------------------------------------------------
                  3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/justrepdwnld.xhtml", sbParameter);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    //objResponse.ErrorMessage = "Server Error";
                    this.Logoff();
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    this.Logoff();
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
               *    CHALLAN DETAILS PARAMETER PREPARATION
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
                sbParameter.Append("&stmtSpecKyc=1");
                // sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&bforeLogin=3");
                //sbParameter.Append("&finYr=" + objTraceData.FAYear);
                //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

                //sbParameter.Append("&tan=" );

                sbParameter.Append("&token=" + objTraceData.PRN_NO);

                // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
                //sbParameter.Append("&dedCount=2");

                //FOR NILL RETURNS
                if (objTraceData.IsNoChallanCheck)
                    sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
                sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

                // FOR BOOK ADJUSTMENT
                sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
                sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

                //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
                if (objTraceData.panAmtValueCheck)
                    sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
                sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

                if (!objTraceData.IsNoChallanCheck)
                {
                    sbParameter.Append("&bsr=" + objTraceData.BSRCode);
                    sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
                    sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
                    sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
                }

                if (!objTraceData.panAmtValueCheck)
                {
                    sbParameter.Append("&pan1=" + objTraceData.PAN1);
                    sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
                    sbParameter.Append("&pan2=" + objTraceData.PAN2);
                    sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
                    sbParameter.Append("&pan3=" + objTraceData.PAN3);
                    sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
                }
                //--------------------------------------------------------------
                sbParameter.Append("&clickKYC=Proceed");
                sbParameter.Append("&dedkyc_SUBMIT=1");
                //--------------------------------------------------------------------------------------------------------------
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "qrtr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "frmType")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "tan")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "isChlnNil")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "dedCount")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "bkEntryValue")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //---------------------------------------------------------------
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                //---------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();
                    return objResponse;
                }

                objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    // objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    this.Logoff();
                    return objResponse;
                }
                //---------------------------------------------------------------------------------------------------------
                string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
                sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
                sbParameter.Append("&dedkyc_SUBMIT=1");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //-------------------------------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("fy=" + pair.Value);

                    if (pair.Key == "qrtr")
                    {
                        if (pair.Value == "")
                            sbParameter.Append("&qr=0");
                        else
                            sbParameter.Append("&qr=" + pair.Value);
                    }

                    if (pair.Key == "formType")
                        sbParameter.Append("&ft=" + pair.Value);

                    if (pair.Key == "dwldType")
                        sbParameter.Append("&dt=" + pair.Value);

                }
                //-------------------------------------------------------------------------------------------------
                //-- ANIK 2013-07-13
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                //-- ANIK 2013-07-28
                //strResponse = makeHTTPGetRequest(strBaseURL + "ded/srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                strResponse = makeHTTPGetRequest(strBaseURL + "srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    RequestStatus conso = new RequestStatus();
                    conso.AuthenticationCode = strAuthenCode;
                    conso.StatusMessage = strMessage;

                    objResponse.CustomeTypes = conso;
                    objResponse.Respons = enmResponse.Success;
                }
                //==============================================
                this.Logoff();


            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
                this.Logoff();

            }
            return objResponse;

        }

        #endregion

        #region RequestForDownloadForm16A
        public TracesResponse RequestForDownloadForm16A(TracesLogin objLogin, TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                1.> REQUEST FOR LOGIN INTO TRACES SITES
                    URL :: https://www.tdscpc.gov.in/app/login.xhtml
                 --------------------------------------------------------------------*/
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTRACES(objLogin);
                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("dwnldFormBulkType=14");
                sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
                sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
                sbParameter.Append("&bulkformType=" + objTraceData.Forms);
                sbParameter.Append("&bulkGo=Go");
                sbParameter.Append("&bulkSearch_SUBMIT=1");
                /* --------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkSearch\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                    {
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                        break;
                    }
                }
                /* --------------------------------------------------------------------
                  3.> REQUEST FOR FORM 16A( justrepdwnld.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml 
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    Logoff();
                    return objResponse;
                }
                if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
               *    Details To Be Printed On Form 16A
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                //------------------------------------------------------------------------- 
                sbParameter = new StringBuilder();
                sbParameter.Append("j_id1972728517_7cc7de5f=submit");

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                //-------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
               *    CHALLAN DETAILS PARAMETER PREPARATION
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
                sbParameter.Append("&stmtSpecKyc=1");
                // sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&bforeLogin=3");
                //sbParameter.Append("&finYr=" + objTraceData.FAYear);
                //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

                //sbParameter.Append("&tan=" );

                sbParameter.Append("&token=" + objTraceData.PRN_NO);

                // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
                //sbParameter.Append("&dedCount=2");

                //FOR NILL RETURNS
                if (objTraceData.IsNoChallanCheck)
                    sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
                sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

                // FOR BOOK ADJUSTMENT
                sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
                sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

                //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
                if (objTraceData.panAmtValueCheck)
                    sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
                sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

                if (!objTraceData.IsNoChallanCheck)
                {
                    sbParameter.Append("&bsr=" + objTraceData.BSRCode);
                    sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
                    sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
                    sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
                }

                if (!objTraceData.panAmtValueCheck)
                {
                    sbParameter.Append("&pan1=" + objTraceData.PAN1);
                    sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
                    sbParameter.Append("&pan2=" + objTraceData.PAN2);
                    sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
                    sbParameter.Append("&pan3=" + objTraceData.PAN3);
                    sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
                }
                //--------------------------------------------------------------
                sbParameter.Append("&clickKYC=Proceed");
                sbParameter.Append("&dedkyc_SUBMIT=1");
                //--------------------------------------------------------------------------------------------------------------
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "qrtr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "frmType")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "tan")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "isChlnNil")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "dedCount")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "bkEntryValue")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //---------------------------------------------------------------
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //---------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    // objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //---------------------------------------------------------------------------------------------------------
                string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
                sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
                sbParameter.Append("&dedkyc_SUBMIT=1");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //-------------------------------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

                ////objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
                //////CHECKING ANY ERROR
                ////sbParameter = new StringBuilder();
                //////----------------------------------------------------------
                ////foreach (KeyValuePair<string, string> pair in objNameval)
                ////{
                ////    if (pair.Key == "finYr")
                ////        sbParameter.Append("fy=" + pair.Value);

                ////    if (pair.Key == "qrtr")
                ////    {
                ////        if (pair.Value == "")
                ////            sbParameter.Append("&qr=0");
                ////        else
                ////            sbParameter.Append("&qr=" + pair.Value);

                ////    }

                ////    if (pair.Key == "formType")
                ////        sbParameter.Append("&ft=" + pair.Value);

                ////    if (pair.Key == "dwldType")
                ////        sbParameter.Append("&dt=" + pair.Value);

                ////}
                //////-------------------------------------------------------------------------------------------------
                ////strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    RequestStatus conso = new RequestStatus();
                    conso.AuthenticationCode = strAuthenCode;
                    conso.StatusMessage = strMessage;

                    objResponse.CustomeTypes = conso;
                    objResponse.Respons = enmResponse.Success;
                }
                //------------------------------------------------------------
                Logoff();

            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
                this.Logoff();

            }
            return objResponse;

        }

        #endregion


        #region RequestForDownloadForm16
        public TracesResponse RequestForDownloadForm16(TracesLogin objLogin, TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                1.> REQUEST FOR LOGIN INTO TRACES SITES
                    URL :: https://www.tdscpc.gov.in/app/login.xhtml
                 --------------------------------------------------------------------*/
                if (!IsSessionExists)
                {
                    objResponse = this.makeLoginToTRACES(objLogin);
                    if (objResponse.Respons == enmResponse.Failed)
                        return objResponse;
                }
                /* --------------------------------------------------------------------
                  2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/download16.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"bulkPan\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();

                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("dwnldFormBulkType=13");
                sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);

                sbParameter.Append("&bulkGo=Go");
                sbParameter.Append("&bulkPan_SUBMIT=1");
                /* --------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkPan\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();

                    return objResponse;
                }
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                    {
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                        break;
                    }
                }
                /* --------------------------------------------------------------------
                  3.> REQUEST FOR FORM 16( justrepdwnld.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/download16.xhtml 
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16.xhtml", sbParameter);
                /*---------------------------------------------------------------------
                  CHECKING ANY ERROR FROM SERVER
                  ---------------------------------------------------------------------*/
                objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
               *    Details To Be Printed On Form 16A
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------------------------------- 
                sbParameter = new StringBuilder();
                sbParameter.Append("j_id1972728517_7cc7de5f=submit");

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                //-------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
               *    CHALLAN DETAILS PARAMETER PREPARATION
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
                sbParameter.Append("&stmtSpecKyc=1");
                // sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&bforeLogin=3");
                //sbParameter.Append("&finYr=" + objTraceData.FAYear);
                //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

                //sbParameter.Append("&tan=" );

                sbParameter.Append("&token=" + objTraceData.PRN_NO);

                // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
                //sbParameter.Append("&dedCount=2");

                //FOR NILL RETURNS
                if (objTraceData.IsNoChallanCheck)
                    sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
                sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

                // FOR BOOK ADJUSTMENT
                sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
                sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

                //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
                if (objTraceData.panAmtValueCheck)
                    sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
                sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

                if (!objTraceData.IsNoChallanCheck)
                {
                    sbParameter.Append("&bsr=" + objTraceData.BSRCode);
                    sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
                    sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
                    sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
                }

                if (!objTraceData.panAmtValueCheck)
                {
                    sbParameter.Append("&pan1=" + objTraceData.PAN1);
                    sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
                    sbParameter.Append("&pan2=" + objTraceData.PAN2);
                    sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
                    sbParameter.Append("&pan3=" + objTraceData.PAN3);
                    sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
                }
                //--------------------------------------------------------------
                sbParameter.Append("&clickKYC=Proceed");
                sbParameter.Append("&dedkyc_SUBMIT=1");
                //--------------------------------------------------------------------------------------------------------------
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "finYr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "qrtr")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "frmType")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "tan")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "isChlnNil")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "dedCount")
                        sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "bkEntryValue")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //---------------------------------------------------------------
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }
                //---------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    Logoff();
                    return objResponse;
                }

                objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    // objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    Logoff();
                    return objResponse;
                }
                //---------------------------------------------------------------------------------------------------------
                string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                sbParameter = new StringBuilder();
                //----------------------------------------------------------
                sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
                sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
                sbParameter.Append("&dedkyc_SUBMIT=1");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "javax.faces.ViewState")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //-------------------------------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

                ////objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
                //////CHECKING ANY ERROR
                ////sbParameter = new StringBuilder();
                //////----------------------------------------------------------
                ////foreach (KeyValuePair<string, string> pair in objNameval)
                ////{
                ////    if (pair.Key == "finYr")
                ////        sbParameter.Append("fy=" + pair.Value);

                ////    if (pair.Key == "qrtr")
                ////    {
                ////        if (pair.Value == "")
                ////            sbParameter.Append("&qr=0");
                ////        else
                ////            sbParameter.Append("&qr=" + pair.Value);

                ////    }

                ////    if (pair.Key == "formType")
                ////        sbParameter.Append("&ft=" + pair.Value);

                ////    if (pair.Key == "dwldType")
                ////        sbParameter.Append("&dt=" + pair.Value);

                ////}
                //////-------------------------------------------------------------------------------------------------
                ////strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    RequestStatus conso = new RequestStatus();
                    conso.AuthenticationCode = strAuthenCode;
                    conso.StatusMessage = strMessage;

                    objResponse.CustomeTypes = conso;
                    objResponse.Respons = enmResponse.Success;
                }
                this.Logoff();

            }
            catch (Exception err)
            {
                objResponse.Respons = enmResponse.Failed;
                objResponse.Message = err.Message;
                this.Logoff();

            }
            return objResponse;

        }

        #endregion


        #region ServerMessageRepository
        private void ServerMessageRepository()
        {
            //SERVER MESSAGE DATABASE

            objMsgDictionary.Clear();

            /* CREATE SERVER ERROR DATABASE AS
            1. ERROR_ID
            2. SERVER ERROR
            3. CUSTOM ERROR
            ---------------------------------- */
            objMsgDictionary.Add(new ErrorDB<int, string, string>(1, "Invalid details", "Invalid details"));
            objMsgDictionary.Add(new ErrorDB<int, string, string>(4, "You have 3 attempts left", "You have 3 attempts left"));
            objMsgDictionary.Add(new ErrorDB<int, string, string>(6, "You have 2 attempts left", "You have 2 attempts left"));
            objMsgDictionary.Add(new ErrorDB<int, string, string>(8, "You have 1 attempts left", "You have 1 attempts left"));
            objMsgDictionary.Add(new ErrorDB<int, string, string>(10, "Statement is not available with TRACES at present", "Statement is not available with TRACES at present"));

            //objMsgDictionary.Add(new ErrorDB<int, string, string>(12, "Conso file will not be available temporarily. Inconvenience is regretted"));


            objMsgDictionary.Add(new ErrorDB<int, string, string>(12, "Conso file will not be available temporarily. Inconvenience is regretted", "Conso file will not be available temporarily. Inconvenience is regretted"));

            //objMsgDictionary.Add(new ErrorDB<int, string, string>(10, "JavaScript is disabled in your browser", "JavaScript is disabled in your browser"));


            //---------------------------------------



        }

        #endregion

        #region IsServerError1
        private TracesResponse IsServerError1(string strServerResponse)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            ServerMessageRepository();

            foreach (ErrorDB<int, string, string> liError in objMsgDictionary)
            {
                if (Regex.IsMatch(strServerResponse, liError.KEY2, RegexOptions.IgnoreCase))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = liError.KEY3;
                }
            }

            return objResponse;
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
            if (!string.IsNullOrEmpty(strErrorText))
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
                if (!string.IsNullOrEmpty(strErrorText))
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

        #region makeLoginToTRACES1
        private string makeLoginToTRACES1(TracesLogin login)
        {
            request = (HttpWebRequest)HttpWebRequest.Create(strBaseURL + "login.xhtml");
            //-------------------------------------
            request.CookieContainer = objContainer;
            request.KeepAlive = false;
            request.Method = "GET";
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            // request.ContentType = "application/x-www-form-urlencoded";
            //request.Headers.Add("Accept-Encoding: gzip,deflate,sdch");
            //request.Headers.Add("Accept-Language: en-US,en;q=0.8");
            //request.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.3");

            //            request.Headers.GetType().InvokeMember("ChangeInternal",
            //BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
            //Type.DefaultBinder, request.Headers, new object[] { "Connection", "Keep-Alive" }
            //);

            //-------------------------------------
            response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);


            ////----------------------------------------------------------
            //for (int i = 0; i < response.Cookies.Count; i++)
            //{
            //    response.Cookies[i].Path = String.Empty;
            //}
            ////----------------------------------------------------------
            //if (request.CookieContainer == null)
            //    request.CookieContainer = objContainer;
            //request.CookieContainer.Add(new Uri("https://www.tdscpc.gov.in/app/"), response.Cookies);

            //request.CookieContainer.Add(response.Cookies);
            //BugFix_CookieDomain(objContainer);



            //--------------------------------------
            strServerResponse = reader.ReadToEnd();

            //----------------------------------------------------
            //CHECKING TRACES OPEN OR CLOSE
            //TRACESResponse objresponse = IsServerError(strServerResponse, "//span[@class=\"infoMsg w775\"]");           
            //if (objresponse.Respons == enmResponse.Failed)            
            //    throw new Exception(objresponse.ErrorMessage); 
            //----------------------------------------
            reader.Close();
            dataStream.Close();
            response.Close();
            //--------------------------------------------
            //CREATE DATA FOR LOGIN FORM
            StringBuilder sbParameter = new StringBuilder();
            sbParameter.Append("j_username=" + login.UserID);
            sbParameter.Append("&j_password=" + login.Password);

            //MAKE REQUEST TO LOGIN FORM
            strServerResponse = makeHTTPPostRequest(strBaseURL + "j_security_check", sbParameter);




            return strServerResponse;
        }

        #endregion

        #region IsConditionMatch
        private bool IsConditionMatch(string strResponse, string strPattern)
        {
            return Regex.IsMatch(strResponse, strPattern, RegexOptions.IgnoreCase);
        }

        #endregion

        #region makeLoginToTRACES
        public TracesResponse makeLoginToTRACES(TracesLogin login)
        {
            TracesResponse objResponse = new TracesResponse();

            //CREATE DATA FOR LOGIN FORM
            StringBuilder sbParameter = new StringBuilder();
            try
            {
                sbParameter.Append("username=" + login.UserID);
                sbParameter.Append("&j_username=" + login.UserID + "^" + login.TAN);
                sbParameter.Append("&j_password=" + HttpUtility.UrlEncode(login.Password));
                sbParameter.Append("&j_tanPan=" + login.TAN);
                sbParameter.Append("&j_captcha=" + login.CaptchaCode);

                //MAKE REQUEST TO LOGIN FORM
                strServerResponse = makeHTTPPostRequest(strBaseURL + "j_security_check", sbParameter);

                if (string.IsNullOrEmpty(strServerResponse))
                {
                    IsSessionExists = false;
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Login Failed or Server Error";
                    return objResponse;
                }

                //CHECKING ANY ERROR FROM SERVER
                objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    IsSessionExists = false;
                    //objResponse.ErrorMessage = "Login Failed or Server Error";
                    return objResponse;
                }
                if (!IsConditionMatch(strServerResponse, "You have logged"))
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Login Failed or Server Error";
                    return objResponse;
                }
                else
                {
                    IsSessionExists = true;
                    objResponse.Respons = enmResponse.Success;
                    objResponse.Message = "";
                    return objResponse;
                }

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

        #region makeHTTPPostRequest1
        private string makeHTTPPostRequest1(string strURL, StringBuilder sbData)
        {
            //            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            //delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            //                        System.Security.Cryptography.X509Certificates.X509Chain chain,
            //                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
            //{
            //    return true; // **** Always accept
            //};


            request = (HttpWebRequest)WebRequest.Create(strURL);

            request.Method = WebRequestMethods.Http.Post;
            request.Credentials = CredentialCache.DefaultCredentials;
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            //request.KeepAlive = false;
            request.ServicePoint.Expect100Continue = false;
            // ServicePointManager.MaxServicePointIdleTime = 2000;
            //ServicePointManager.DefaultConnectionLimit = 1;
            //----------------------------------------------------------
            // CREATE POST DATA AND CONVERT IT TO A BYTE ARRAY.
            //----------------------------------------------------------    
            //byte[] byteArray = null;
            //string postData = "";

            //if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            //{
            //    postData = sbData.ToString();
            //    byteArray = Encoding.UTF8.GetBytes(postData);
            //}

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteArray = encoding.GetBytes(sbData.ToString());
            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;
            //----------------------------------------------------------
            // SET THE CONTENTLENGTH PROPERTY OF THE WEBREQUEST.
            //----------------------------------------------------------




            if (sbData != null)
            {

                request.Headers.GetType().InvokeMember("ChangeInternal",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null,
                    request.Headers, new object[] { "Host", "www.tdscpc.gov.in" });
                //---------------------------------------------------------------------------
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.152 Safari/537.22";

                request.Headers.Add("Accept-Encoding: gzip,deflate,sdch");
                request.Headers.Add("Accept-Language: en-US,en;q=0.8");
                request.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.3");
                request.Headers.Add("Cache-Control: max-age=0");
                request.Headers.Add("Origin: https://www.tdscpc.gov.in");
                // request.AllowAutoRedirect = false;
                request.Referer = "https://www.tdscpc.gov.in/app/ded/panverify.xhtml";
                // request.ProtocolVersion = HttpVersion.Version10;


                //request.CookieContainer = objContainer;

                //request.Referer = "https://www.tdscpc.gov.in/app/";
                //// request.Timeout = 10000;
                request.Timeout = 100000; // fix 3
                request.ReadWriteTimeout = 1000000000; // fix 4

                request.Headers.GetType().InvokeMember("ChangeInternal",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
            Type.DefaultBinder, request.Headers, new object[] { "Connection", "Keep-Alive" });

            }


            //----------------------------------------------------------
            if (request.CookieContainer == null)
                request.CookieContainer = objContainer;

            request.CookieContainer.Add(new Uri("https://www.tdscpc.gov.in/app/"), response.Cookies);



            // request.CookieContainer.Add(response.Cookies);
            //BugFix_CookieDomain(objContainer);
            // SetAllowUnsafeHeaderParsing();

            // Uri uri = new Uri(strURL);
            // objContainer.SetCookies(uri, response.Headers[HttpResponseHeader.SetCookie]);

            //----------------------------------------------------------
            // GET THE REQUEST STREAM.
            //----------------------------------------------------------

            Stream swOut = request.GetRequestStream();
            swOut.Write(byteArray, 0, byteArray.Length);


            //if (!string.IsNullOrEmpty(Convert.ToString(sbData)))
            //{



            //    dataStream = request.GetRequestStream();
            //    //----------------------------------------------------------
            //    // WRITE THE DATA TO THE REQUEST STREAM.
            //    //----------------------------------------------------------
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    //----------------------------------------------------------
            //    // CLOSE THE STREAM OBJECT.
            //    //----------------------------------------------------------
            //    dataStream.Close();
            //}
            //----------------------------------------------------------
            // GET THE RESPONSE.
            //----------------------------------------------------------
            response = (HttpWebResponse)request.GetResponse();

            string strResponeurl = response.ResponseUri.ToString();

            if (!string.IsNullOrEmpty(strResponeurl))
                strResponeurl = strResponeurl.Substring(strResponeurl.LastIndexOf("/") + 1);

            if (strResponeurl == "login.xhtml")
            {
                strServerResponse = strResponeurl;
                return strServerResponse;
            }

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

        #region makeHTTPPostRequest
        private string makeHTTPPostRequest(string strURL, StringBuilder sbData)
        {

            request = (HttpWebRequest)WebRequest.Create(strURL);
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            request.KeepAlive = false;
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
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
                request.ContentType = "application/x-www-form-urlencoded";

                //  request.Headers.Add("Accept-Encoding: gzip,deflate,sdch");
                //request.Headers.Add("Accept-Language: en-US,en;q=0.8");
                //request.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.3");

                //  request.ProtocolVersion = HttpVersion.Version10;
                // request.AllowAutoRedirect = true;

                //// request.Timeout = 10000;
                request.Timeout = 1000000000; // fix 3
                //request.ReadWriteTimeout = 1000000000; // fix 4
                // SetAllowUnsafeHeaderParsing();
                //request.Credentials = CredentialCache.DefaultCredentials;

                //request.Headers.GetType().InvokeMember("ChangeInternal",
                // BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                //Type.DefaultBinder, request.Headers, new object[] { "Connection", "Keep-Alive" });
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

        #region makeHTTPGetRequest
        private string makeHTTPGetRequest(string strURL)
        {
            SetAllowUnsafeHeaderParsing();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

            request = (HttpWebRequest)HttpWebRequest.Create(strURL);
            //-------------------------------------
            // request.CookieContainer = objContainer;
            request.KeepAlive = false;
            request.Method = "GET";

            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            request.Accept = "text/html";

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

        #region getCaptchaCode
        public Stream getCaptchaCode()
        {

            this.bnlSessionExists = false;
            //request = (HttpWebRequest)WebRequest.Create(strBaseURL + "GetCaptchaImg");
            //-- ANIK 2013-07-13
            request = (HttpWebRequest)WebRequest.Create(strCaptchBaseURL + "GetCaptchaImg");
            request.Method = "GET";
            request.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20100101 Firefox/15.0";
            request.ContentType = "text/html; charset=utf-8";
            //request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //request.Referer = location_from_req_2;
            request.KeepAlive = true;
            //request.AllowAutoRedirect = false;
            //request.Timeout = TimeOut;

            //request.Proxy = proxy;

            //cookieJar = new CookieContainer();
            request.CookieContainer = objContainer;

            //response = (HttpWebResponse)request.GetResponse();
            //txtLog.Text += ((HttpWebresponseponse)response).StatusDescription;
            //txtLog.Text += "\r\n----------------------------------------------------------\r\n";

            //string[] cookies = response.Headers.GetValues("Set-Cookie");
            //string[] cookies_ = cookies;

            if (response.Cookies != null && response.Cookies.Count > 0)
            {
                objContainer.Add(response.Cookies);
            }
            foreach (Cookie cookie in response.Cookies)
            {
                objContainer.Add(new Cookie(cookie.Name.Trim(), cookie.Value.Trim(), "/", cookie.Domain));
            }
            if (!string.IsNullOrEmpty(response.Headers["Set-Cookie"]))
            {
                //ArrayList al = ConvertCookieHeaderToArrayList(response.Headers["Set-Cookie"]);
                //CookieCollection cc = ConvertCookieArraysToCookieCollection(al, "empty.com");
                //cookieJar.Add(cc);
            }
            for (int i = 0; i < objContainer.GetCookies(request.RequestUri).Count; i++)
            {
                Cookie cookie = objContainer.GetCookies(request.RequestUri)[i];
                //txtLog.Text += "#" + (i + 1).ToString() + "; name: " + cookie.Name.ToString() + "; value: " + cookie.Value.ToString() + "; path: " + cookie.Path.ToString() + "; domain: " + cookie.Domain.ToString() + "; version: " + cookie.Version.ToString();
                //txtLog.Text += "\r\n++++++++++++++++++\r\n";
            }
            //txtLog.Text += "cookieJar Count: " + cookieJar.Count.ToString() + "; req 5: " + request.RequestUri;
            //txtLog.Text += "\r\n----------------------------------------------------------\r\n";

            //string location_from_req_5 = response.Headers["Location"];

            //Stream reader = response.GetResponseStream();
            //reader = new StreamReader(Stream);
            //reader_str = reader.ReadToEnd();

            //var image = Image.FromStream(Stream);
            //imgCaptcha.Image = image;

            //reader.Close();
            //Stream.Close();
            //response.Close();

            //Stream stream = request.GetResponse().GetResponseStream();
            //Image img = Image.FromStream(stream);
            //this.picCaptcha.Image = img;

            return request.GetResponse().GetResponseStream();

        }

        #endregion

        #region makeHTTPJSONRequest
        private string makeHTTPJSONRequest(string strURL)
        {
            request = (HttpWebRequest)WebRequest.Create(strURL);
            string json = "";
            request.ServicePoint.Expect100Continue = false;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            request.ContentType = "application/json";
            request.Headers.Add("Cache-Control", "no-cache");

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

        #region makeHttpDownloadRequest
        private bool makeHttpDownloadRequest(string strURL, string strPath)
        {
            bool DownloadStatus = true;
            string strFilename = "";
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.KeepAlive = true;
                request.Timeout = 300000;
                request.AllowWriteStreamBuffering = false;
                request.AllowAutoRedirect = false;
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
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

        #region GetFileLocation
        private string GetFileLocation(string strURL)
        {
            string strLocation;


            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.KeepAlive = true;
            request.Timeout = 300000;
            request.AllowWriteStreamBuffering = false;
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            //---------------------------------------------------           
            request.CookieContainer = objContainer;
            request.CookieContainer.Add(response.Cookies);
            //-------------------------------------------------------
            response = (HttpWebResponse)request.GetResponse();

            strLocation = response.Headers["Location"];



            return strLocation;
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
                    HtmlNodeCollection childCollection = child.SelectNodes("//input[@type='hidden']");

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

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode;
            HtmlNodeCollection hncHiddenField = node.SelectNodes(xPathQuery);

            if (hncHiddenField == null) return objNameVal;
            objNameVal.Clear();
            //---------------------------------------------------------------------------           
            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {
                foreach (HtmlNode child in hncHiddenField)
                {
                    HtmlNodeCollection childCollection = child.SelectNodes("//input[@type='hidden']");

                    if (childCollection != null && childCollection.Count > 0)
                    {
                        for (int i = 0; i < childCollection.Count; i++)
                        {
                            //if (childCollection[i].Attributes["name"].Value == "javax.faces.ViewState")
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

        #region RetrieveElementValue
        private string RetrieveElementValue(string strHTML, string xPathQuery,string strNode,enmElementType enmElement)
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

        #region BugFix_CookieDomain
        private void BugFix_CookieDomain(CookieContainer cookieContainer)
        {
            System.Type _ContainerType = typeof(CookieContainer);
            Hashtable table = (Hashtable)_ContainerType.InvokeMember("m_domainTable",
                                       System.Reflection.BindingFlags.NonPublic |
                                       System.Reflection.BindingFlags.GetField |
                                       System.Reflection.BindingFlags.Instance,
                                       null,
                                       cookieContainer,
                                       new object[] { });
            ArrayList keys = new ArrayList(table.Keys);
            foreach (string keyObj in keys)
            {
                string key = (keyObj as string);
                if (key[0] == '.')
                {
                    string newKey = key.Remove(0, 1);
                    table[newKey] = table[keyObj];
                }
            }
        }


        #endregion

        #region MakeInitialRequest
        public Stream MakeInitialRequest()
        {
            makeHTTPGetRequest("https://www.tdscpc.gov.in/app/login.xhtml");

            return getCaptchaCode();


        }

        #endregion

        #region RetievePANStatus
        private Dictionary<string, string> RetievePANStatus(string strHTML)
        {
            //
            Dictionary<string, string> objNameVal = new Dictionary<string, string>();

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode;
            //--------------------------------------------------------------------
            HtmlNodeCollection hncHiddenField = node.SelectNodes("//table[@class=\"userList w990 marginTop10\"]");

            if (hncHiddenField == null) return objNameVal;
            objNameVal.Clear();
            //---------------------------------------------------------------------------           
            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {
                //-------------------------------------------------------------------
                foreach (HtmlNode child in hncHiddenField)
                {
                    HtmlNodeCollection childCollection = child.SelectNodes("//td[@id='status']");

                    if (childCollection != null && childCollection.Count > 0)
                    {
                        for (int i = 0; i < childCollection.Count; i++)
                        {
                            objNameVal.Add("Status", childCollection[i].InnerText);

                        }
                    }
                    //------------------------------------------------------------------
                    childCollection = child.SelectNodes("//td[@id='name']");

                    if (childCollection != null && childCollection.Count > 0)
                    {
                        for (int i = 0; i < childCollection.Count; i++)
                        {
                            objNameVal.Add("name", childCollection[i].InnerText);

                        }
                    }




                }
            }



            return objNameVal;



        }

        #endregion

        #region GetCaptchaForPANVerify
        public Stream GetCaptchaForPANVerify()
        {
            makeHTTPGetRequest("https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html");
            //--------------------------------------------------------------------------------------------------------
            request = (HttpWebRequest)WebRequest.Create("https://incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do");
            request.Method = "GET";
            request.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20100101 Firefox/15.0";
            request.ContentType = "text/html; charset=utf-8";

            request.KeepAlive = true;
            request.CookieContainer = objContainer;


            if (response.Cookies != null && response.Cookies.Count > 0)
            {
                objContainer.Add(response.Cookies);
            }
            foreach (Cookie cookie in response.Cookies)
            {
                objContainer.Add(new Cookie(cookie.Name.Trim(), cookie.Value.Trim(), "/", cookie.Domain));
            }
            for (int i = 0; i < objContainer.GetCookies(request.RequestUri).Count; i++)
            {
                Cookie cookie = objContainer.GetCookies(request.RequestUri)[i];
            }


            return request.GetResponse().GetResponseStream();

        }


        #endregion

        #region VerifyPAN
        public TracesResponse VerifyPAN(string strPan, string strCaptchaCode)
        {
            StringBuilder strBuilder = new StringBuilder();
            TracesResponse objResponse = new TracesResponse();
            PANVerifierDetails objPanVan = new PANVerifierDetails();
            //------------------------------------------------
            strBuilder.Append("requestId=");
            strBuilder.Append("&panOfDeductee=" + strPan);
            strBuilder.Append("&captchaCode=" + strCaptchaCode);
            //-------------------------------------------------
            string strRes = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html ", strBuilder);

            objResponse = IsServerError(strRes, "//div[@class=\"error\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                //objResponse.ErrorMessage = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                return objResponse;
            }
            else
            {
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(strRes);

                if (Regex.IsMatch(strRes, "PAN does not exist"))
                {
                    objResponse.Message = "PAN does not exist";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //-------------------------------------------------------------------------------------------
                HtmlNode node = document.DocumentNode;
                string strTDKey = "";
                string strTDVal = "";
                objResponse.Respons = enmResponse.Success;
                //-------------------------------------------------------------------------------------------
                foreach (HtmlNode td in document.DocumentNode.SelectNodes("//table[@class='grid']//tr//td"))
                {

                    if (strTDKey != "")
                    {
                        strTDVal = td.InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "");

                        if (strTDKey.ToUpper() == "SURNAME")
                            objPanVan.Surname = strTDVal;

                        if (strTDKey.ToUpper() == "MIDDLE NAME")
                            objPanVan.MiddleName = strTDVal;

                        if (strTDKey.ToUpper() == "FIRST NAME")
                            objPanVan.FirstName = strTDVal;

                        if (strTDKey.ToUpper() == "AREA CODE")
                            objPanVan.AreaCode = strTDVal;

                        if (strTDKey.ToUpper() == "AO TYPE")
                            objPanVan.AOType = strTDVal;

                        if (strTDKey.ToUpper() == "RANGE CODE")
                            objPanVan.RangeCode = strTDVal;

                        if (strTDKey.ToUpper() == "AO NUMBER")
                            objPanVan.AONumber = strTDVal;

                        if (strTDKey.ToUpper() == "JURISDICTION")
                            objPanVan.Jurisdiction = strTDVal;

                        if (strTDKey.ToUpper() == "BUILDING NAME")
                            objPanVan.BuildingName = strTDVal;
                    }

                    strTDKey = td.InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "");

                }
                //---------------------------------------------------------------------------------
                objResponse.CustomeTypes = objPanVan;

            }

            return objResponse;

        }

        #endregion





    }


}
