

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
    #region enmChallanStatus
    public enum enmChallanStatus
    {
        AMOUNT_MATCHED =1,
        AMOUNT_NOT_MATCHED=2,
        RECORD_NOT_FOUND = 3,
        PROCESSING_FAILED=4,
        WRONG_CAPTCHA = 5,
        SERVER_MAINTENANCE_ERROR = 6
    }
    #endregion

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
                //if (pair.Key == "javax.faces.ViewState") //-- ANIK 2015-01-28
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
            }
            /* --------------------------------------------------------------------
              3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
             *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
             * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
               --------------------------------------------------------------------*/
            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/nsdlconsofile.xhtml", sbParameter);
                       
            objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                Logoff();
                return objResponse;
            }

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
            if (!this.bnlSessionExists)
            {

                objResponse = this.makeLoginToTRACES(objLogin);
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
            }
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

            objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                Logoff();
                return objResponse;
            }

            //objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
            //-- ARUP @ 2015/03/24
            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"justificationForm\"]"); 
            //fetching hidden field isChlnNil's value
            objResponse.CustomeTypes = objNameval;

            return objResponse;
        }

        #endregion


        #region IsChallanExistsInForm16
        //public TracesResponse IsChallanExistsInForm16(TracesData objTraceData, TracesLogin objLogin)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;
        //    StringBuilder sbParameter;
        //    /* --------------------------------------------------------------------
        //      1.> REQUEST FOR LOGIN INTO TRACES SITES
        //          URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //       --------------------------------------------------------------------*/
        //    if (!this.bnlSessionExists)
        //    {
        //        objResponse = this.makeLoginToTRACES(objLogin);
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //    }
        //    /*--------------------------------------------------------------------
        //      2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //          URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //      --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/download16.xhtml");

        //    //CHECKING ANY ERROR FROM SERVER POINT
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"bulkPan\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("dwnldFormBulkType=13");
        //    sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //    //sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //    //sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //    sbParameter.Append("&bulkGo=Go");
        //    sbParameter.Append("&bulkPan_SUBMIT=1");
        //    /* -----------------------------------------------------------------------
        //       RETRIEVE VIEWSTATE DATA                
        //       --------------------------------------------------------------------*/
        //    Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"bulkPan\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //----------------------------------------------------------
        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //    {
        //        if (pair.Key == "javax.faces.ViewState")
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    }
        //    /* --------------------------------------------------------------------
        //      3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
        //       --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/download16.xhtml", sbParameter);

        //    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        //objResponse.ErrorMessage = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------------------- 
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    //-------------------------------------------------------------------------
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);s

        //    if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc1\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        Logoff();
        //        return objResponse;
        //    }

        //    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        Logoff();
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc1\"]");
        //    //fetching hidden field isChlnNil's value
        //    objResponse.CustomeTypes = objNameval;

        //    return objResponse;
        //}

        #endregion

        #region IsChallanExistsInForm16
        //public TracesResponse IsChallanExistsInForm16(TracesData objTraceData, TracesLogin objLogin)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;
        //    StringBuilder sbParameter;
        //    /* --------------------------------------------------------------------
        //      1.> REQUEST FOR LOGIN INTO TRACES SITES
        //          URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //       --------------------------------------------------------------------*/
        //    if (!this.bnlSessionExists)
        //    {
        //        objResponse = this.makeLoginToTRACES(objLogin);
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //    }
        //    /*--------------------------------------------------------------------
        //      2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //          URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //      --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/download16.xhtml");

        //    //CHECKING ANY ERROR FROM SERVER POINT
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"bulkPan\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("dwnldFormBulkType=13");
        //    sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //    //sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //    //sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //    sbParameter.Append("&bulkGo=Go");
        //    sbParameter.Append("&bulkPan_SUBMIT=1");
        //    /* -----------------------------------------------------------------------
        //       RETRIEVE VIEWSTATE DATA                
        //       --------------------------------------------------------------------*/
        //    Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"bulkPan\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    //----------------------------------------------------------
        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //    {
        //        if (pair.Key == "javax.faces.ViewState")
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    }
        //    /* --------------------------------------------------------------------
        //      3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
        //       --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/download16.xhtml", sbParameter);

        //    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        //objResponse.ErrorMessage = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }

        //    if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //    {
        //        if (!IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //    }

        //    if (IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //    {
        //        objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        //-------------------------------------------------------------------------
        //        strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

        //        if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc1\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            Logoff();
        //            return objResponse;
        //        }

        //        objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc1\"]");
        //        //fetching hidden field isChlnNil's value
        //        objResponse.CustomeTypes = objNameval;

        //    }
        //    else if (IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
        //    {
        //        //

        //        objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"tabContentForm\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        string strParam = "qr=6";
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            //sbParameter.Append("&qr=" + HttpUtility.UrlEncode(pair.Value));


        //            if (pair.Key == "frmType")
        //                //sbParameter.Append("&ft=" + HttpUtility.UrlEncode(pair.Value));
        //                strParam += "&ft=" + HttpUtility.UrlEncode(pair.Value);
        //            else if (pair.Key == "finYear")
        //                //sbParameter.Append("&fy=" + HttpUtility.UrlEncode(pair.Value));
        //                strParam += "&fy=" + HttpUtility.UrlEncode(pair.Value);
        //            else if (pair.Key == "dwnldType")
        //                // sbParameter.Append("&download=" + HttpUtility.UrlEncode(pair.Value));
        //                strParam += "&download=" + HttpUtility.UrlEncode(pair.Value);

        //            //else if (pair.Key == "javax.faces.ViewState")
        //            //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));


        //        }

        //        //-------------------------------------------------------------------------
        //        strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?" + strParam);
        //        //strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16unmatch.xhtml", sbParameter);

        //        if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            Logoff();
        //            return objResponse;
        //        }

        //        objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
        //        //fetching hidden field isChlnNil's value
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();

        //        sbParameter.Append("j_id1972728517_7cc7de5f=Submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            //if(pair.Key =="Qtr")
        //            //    sbParameter.Append("&Qtr=6");
        //            //else
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //        //-------------------------------------------------------------------------
        //        strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

        //        if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc1\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            Logoff();
        //            return objResponse;
        //        }

        //        objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc1\"]");
        //        //fetching hidden field isChlnNil's value
        //        objResponse.CustomeTypes = objNameval;



        //    }

        //    return objResponse;
        //}

        #endregion

        #region IsChallanExistsInForm16
        public TracesResponse IsChallanExistsInForm16(TracesData objTraceData, TracesLogin objLogin)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            StringBuilder sbParameter;
            /* --------------------------------------------------------------------
              1.> REQUEST FOR LOGIN INTO TRACES SITES
                  URL :: https://www.tdscpc.gov.in/app/login.xhtml
               --------------------------------------------------------------------*/
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
            string strResponse = "";
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
                if (!IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
            }
            //ADDED ON 10-10-15
            if (IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
            {


                objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"tabContentForm\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                string strParam = "qr=6";
                //------------------------------------------------------------------------- 
                sbParameter = new StringBuilder();
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "frmType")
                        strParam += "&ft=" + HttpUtility.UrlEncode(pair.Value);
                    else if (pair.Key == "finYear")
                        strParam += "&fy=" + HttpUtility.UrlEncode(pair.Value);
                    else if (pair.Key == "dwnldType")
                        strParam += "&download=" + HttpUtility.UrlEncode(pair.Value);
                }


                //-------------------------------------------------------------------------
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?" + strParam);
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16unmatch.xhtml", sbParameter);

                if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    Logoff();
                    return objResponse;
                }

                objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");
                strResponse = strServerResponse;
                if (objResponse.Respons == enmResponse.Failed)
                {
                    Logoff();
                    return objResponse;
                }
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
            //---------------------------------------
            if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
            {
                if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                {
                    this.Logoff();
                    this.bnlSessionExists = false;
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                else
                {
                    objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                    if (objNameval.Count == 0)
                    {
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    //---------------------------------------------------
                    sbParameter = new StringBuilder();


                    foreach (KeyValuePair<string, string> pair in objNameval)
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                    sbParameter.Append("&search2=on");
                    sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                    sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                    sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                    sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                    strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                    if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }

                }
            }

            //-------------------------------------------------------------------------
            // strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

            if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc1\"]"))
            {
                objResponse.Message = "Server Error";
                objResponse.Respons = enmResponse.Failed;
                Logoff();
                return objResponse;
            }

            objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

            if (objResponse.Respons == enmResponse.Failed)
            {
                Logoff();
                return objResponse;
            }

            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc1\"]");
            //fetching hidden field isChlnNil's value
            objResponse.CustomeTypes = objNameval;





            return objResponse;
        }

        #endregion

        #region IsChallanExistsInForm16A
        //public TracesResponse IsChallanExistsInForm16A(TracesData objTraceData, TracesLogin objLogin)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;
        //    StringBuilder sbParameter;
        //    /* --------------------------------------------------------------------
        //      1.> REQUEST FOR LOGIN INTO TRACES SITES
        //          URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //       --------------------------------------------------------------------*/
        //    if (!this.bnlSessionExists)
        //    {
        //        objResponse = this.makeLoginToTRACES(objLogin);
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //    }
        //    /*--------------------------------------------------------------------
        //      2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //          URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //      --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

        //    //CHECKING ANY ERROR FROM SERVER POINT
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"bulkSearch\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("dwnldFormBulkType=14");
        //    sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //    sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //    sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //    sbParameter.Append("&bulkGo=Go");
        //    sbParameter.Append("&bulkSearch_SUBMIT=1");
        //    /* -----------------------------------------------------------------------
        //       RETRIEVE VIEWSTATE DATA                
        //       --------------------------------------------------------------------*/
        //    Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"bulkSearch\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //----------------------------------------------------------
        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //    {
        //        if (pair.Key == "javax.faces.ViewState")
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    }
        //    /* --------------------------------------------------------------------
        //      3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
        //       --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);

        //    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        //objResponse.ErrorMessage = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------------------- 
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    //-------------------------------------------------------------------------
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

        //    if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        Logoff();
        //        return objResponse;
        //    }
        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        Logoff();
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
        //    //fetching hidden field isChlnNil's value
        //    objResponse.CustomeTypes = objNameval;

        //    return objResponse;
        //}

        #endregion

        #region IsChallanExistsInForm16A
        //public TracesResponse IsChallanExistsInForm16A(TracesData objTraceData, TracesLogin objLogin)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;
        //    StringBuilder sbParameter;
        //    /* --------------------------------------------------------------------
        //      1.> REQUEST FOR LOGIN INTO TRACES SITES
        //          URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //       --------------------------------------------------------------------*/
        //    if (!this.bnlSessionExists)
        //    {
        //        objResponse = this.makeLoginToTRACES(objLogin);
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //    }
        //    /*--------------------------------------------------------------------
        //      2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //          URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //      --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

        //    //CHECKING ANY ERROR FROM SERVER POINT
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"bulkSearch\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("dwnldFormBulkType=14");
        //    sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //    sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //    sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //    sbParameter.Append("&bulkGo=Go");
        //    sbParameter.Append("&bulkSearch_SUBMIT=1");
        //    /* -----------------------------------------------------------------------
        //       RETRIEVE VIEWSTATE DATA                
        //       --------------------------------------------------------------------*/
        //    Dictionary<string, string> objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"bulkSearch\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //----------------------------------------------------------
        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //    {
        //        if (pair.Key == "javax.faces.ViewState")
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    }
        //    /* --------------------------------------------------------------------
        //      3.> REQUEST FOR JUSTIFICATION REPORT( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml 
        //       --------------------------------------------------------------------*/
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);

        //    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");

        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        //objResponse.ErrorMessage = "Server Error";
        //        objResponse.Respons = enmResponse.Failed;
        //        return objResponse;
        //    }
        //    if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"deducteeDetails\"]");
        //    //CHECKING ANY ERROR
        //    if (objNameval.Count <= 0)
        //    {
        //        objResponse.Message = "Server Error";
        //        objResponse.Respons = enmResponse.SessionTimeout;
        //        return objResponse;
        //    }
        //    //------------------------------------------------------------------------- 
        //    sbParameter = new StringBuilder();
        //    sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //    foreach (KeyValuePair<string, string> pair in objNameval)
        //        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //    //-------------------------------------------------------------------------
        //    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);

        //    if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc\"]"))
        //    {

        //        if (!IsStringExists(strServerResponse, "//form[@id=\"kycformdsc\"]"))
        //        {
        //            this.Logoff();
        //            this.bnlSessionExists = false;
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        else
        //        {
        //            objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"kycformdsc\"]");

        //            if (objNameval.Count == 0)
        //            {
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //            //---------------------------------------------------
        //            sbParameter = new StringBuilder();


        //            foreach (KeyValuePair<string, string> pair in objNameval)
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //            sbParameter.Append("&search2=on");
        //            sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
        //            sbParameter.Append("&kycFormType=" + objTraceData.Forms);
        //            sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
        //            sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


        //            strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

        //            if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc\"]"))
        //            {
        //                this.Logoff();
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }

        //        }





        //        //objResponse.Message = "Server Error";
        //        //objResponse.Respons = enmResponse.SessionTimeout;
        //        //Logoff();
        //        //return objResponse;
        //    }
        //    if (objResponse.Respons == enmResponse.Failed)
        //    {
        //        Logoff();
        //        return objResponse;
        //    }

        //    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"dedkyc\"]");
        //    //fetching hidden field isChlnNil's value
        //    objResponse.CustomeTypes = objNameval;

        //    return objResponse;
        //}

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
            if (!this.bnlSessionExists)
            {
                objResponse = this.makeLoginToTRACES(objLogin);
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
            }
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

                if (!IsStringExists(strServerResponse, "//form[@id=\"kycformdsc\"]"))
                {
                    this.Logoff();
                    this.bnlSessionExists = false;
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                else
                {
                    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"kycformdsc\"]");

                    if (objNameval.Count == 0)
                    {
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    //---------------------------------------------------
                    sbParameter = new StringBuilder();


                    foreach (KeyValuePair<string, string> pair in objNameval)
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                    sbParameter.Append("&search2=on");
                    sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                    sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                    sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                    sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                    strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                    if (!IsStringExists(strServerResponse, "//form[@id=\"dedkyc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }

                }





                //objResponse.Message = "Server Error";
                //objResponse.Respons = enmResponse.SessionTimeout;
                //Logoff();
                //return objResponse;
            }
            if (objResponse.Respons == enmResponse.Failed)
            {
                Logoff();
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



        #region RequestForDownloadFile //-- COMMENTED ON 2016/08/27
        //public TracesResponse RequestForDownloadFile(string strReqNo, string strPath)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;
        //    string strResponse = "";
        //    //----------------------------------------------------
        //    try
        //    {
        //        //-- ANIK 2013-07-13
        //        //string strDownloadLink = "DownloadServlet?reqNo=" + strReqNo;
        //        string strDownloadLink = "srv/DownloadServlet?reqNo=" + strReqNo;

        //        //1.> REQUEST FOR LOGIN PAGE
        //        // strResponse = makeLoginToTRACES1(objLogin);
        //        /*---------------------------------------------------------------------
        //          CHECKING ANY ERROR FROM SERVER
        //          ---------------------------------------------------------------------*/
        //        //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

        //        //if (objResponse.Respons == enmResponse.Failed)
        //        //    return objResponse;
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //         * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = CheckDownloadURL(strBaseURL + strDownloadLink);
        //        //-----------------------------------------------------------------------
        //        List<string> strlink = new List<string>();
        //        //if (Regex.IsMatch(strResponse, "downloadmultiple.xhtml?")) //-- 2016/08/19
        //        if (Regex.IsMatch(strResponse, "downloadmultiple.xhtml?") || Regex.IsMatch(strResponse, "success"))
        //        {
        //            strResponse = makeHTTPGetRequest(strBaseURL + "ded/downloadmultiple.xhtml?reqNo=" + strReqNo);

        //            strlink = getDownloadAnchorLink(strResponse);
        //        }
        //        //---------------------------------------------------------
        //        string strDWNLink = strBaseURL + strDownloadLink;

        //        if (strlink.Count != 0)
        //        {
        //            foreach (string strAllLink in strlink)
        //            {
        //                //strDownloadLink = strlink[0];
        //                strDownloadLink = strAllLink;
        //                strDWNLink = "https://www.tdscpc.gov.in" + strDownloadLink;
        //                //---------------------------------------------------------
        //                strURL = GetFileLocation(strDWNLink);

        //                if (String.IsNullOrEmpty(strURL))
        //                {
        //                    objResponse.Respons = enmResponse.Failed;
        //                    objResponse.Message = "due to Traces server failure !!";
        //                    return objResponse;
        //                }
        //                if (Regex.IsMatch(strURL, "ibm_security_logout"))
        //                {
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    objResponse.Message = "Session Timeout Login again !!";
        //                    return objResponse;
        //                }
        //                //--------------------------------------------------
        //                //if (string.IsNullOrEmpty(strURL))
        //                //{
        //                //    objResponse.Respons = enmResponse.Failed;
        //                //    return objResponse;
        //                //}
        //                //------------------------------------------------
        //                if (!makeHttpDownloadRequest(strURL, strPath))
        //                    objResponse.Respons = enmResponse.Failed;

        //                //ADDED BY DHRUB ON 20/03/2014
        //                TDSMAN.T_DownloadedFileNameFromTraces = strURL.Substring(strURL.LastIndexOf("/") + 1, (strURL.Length - 1) - (strURL.LastIndexOf("/")));
        //            }
        //        }
        //        else
        //        {
        //            strURL = GetFileLocation(strDWNLink);

        //            if (String.IsNullOrEmpty(strURL))
        //            {
        //                objResponse.Respons = enmResponse.Failed;
        //                objResponse.Message = "due to Traces server failure !!";
        //                return objResponse;
        //            }
        //            if (Regex.IsMatch(strURL, "ibm_security_logout"))
        //            {
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                objResponse.Message = "Session Timeout Login again !!";
        //                return objResponse;
        //            }
        //            //--------------------------------------------------
        //            //if (string.IsNullOrEmpty(strURL))
        //            //{
        //            //    objResponse.Respons = enmResponse.Failed;
        //            //    return objResponse;
        //            //}
        //            //------------------------------------------------
        //            if (!makeHttpDownloadRequest(strURL, strPath))
        //                objResponse.Respons = enmResponse.Failed;

        //            //ADDED BY DHRUB ON 20/03/2014
        //            TDSMAN.T_DownloadedFileNameFromTraces = strURL.Substring(strURL.LastIndexOf("/") + 1, (strURL.Length - 1) - (strURL.LastIndexOf("/")));
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Message = err.Message;
        //        objResponse.Respons = enmResponse.Failed;
        //    }
        //    return objResponse;

        //}

        #endregion

        #region RequestForDownloadFile
        public TracesResponse RequestForDownloadFile_bak(string strReqNo, string strPath)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            string strResponse = "";
            //----------------------------------------------------
            try
            {

                string strDownloadLink = "srv/DownloadServlet";

                StringBuilder param = new StringBuilder();
                param.Append("reqNo=" + strReqNo);

                strResponse = GetFileLocation(strBaseURL + strDownloadLink, param.ToString());

                objResponse = IsServerError(strResponse, "//span[@id=\"infoMsg\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    this.Logoff();
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                string strURLPath = "";
                JsonTextReader reader2 = new JsonTextReader(new StringReader(strServerResponse));
                while (reader2.Read())
                {

                    if (reader2.TokenType == JsonToken.String)
                    {
                        strURLPath = Convert.ToString(reader2.Value);
                        break;
                    }
                    if (reader2.TokenType == JsonToken.Integer)
                    {
                        strURLPath = Convert.ToString(reader2.Value);
                        break;
                    }
                }
                //--------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strURLPath))
                {
                    if (strURLPath == "2")
                    {
                        objResponse.Respons = enmResponse.Failed;
                        objResponse.Message = "Multiple File Download not available !!";
                        return objResponse;
                    }
                    else
                    {

                        if (!makeHttpDownloadRequest(strURLPath, strPath))
                            objResponse.Respons = enmResponse.Failed;

                        //ADDED BY DHRUB ON 20/03/2014
                        TDSMAN.T_DownloadedFileNameFromTraces = strURLPath.Substring(strURLPath.LastIndexOf("/") + 1, (strURLPath.Length - 1) - (strURLPath.LastIndexOf("/")));
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "due to Traces server failure !!";
                    return objResponse;
                }

            }
            catch (Exception err)
            {
                objResponse.Message = err.Message;
                objResponse.Respons = enmResponse.Failed;
            }
            return objResponse;

        }

        #endregion


        #region RequestForDownloadFile //-- 2016/09/10
        public TracesResponse RequestForDownloadFile(string strReqNo, string strPath)
        {
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;
            string strResponse = "";
            //----------------------------------------------------
            try
            {

                string strDownloadLink = "srv/DownloadServlet";

                StringBuilder param = new StringBuilder();
                param.Append("reqNo=" + strReqNo);

                strResponse = GetFileLocation(strBaseURL + strDownloadLink, param.ToString());

                objResponse = IsServerError(strResponse, "//span[@id=\"infoMsg\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    this.Logoff();
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }

                string strURLPath = "";
                JsonTextReader reader2 = new JsonTextReader(new StringReader(strServerResponse));
                while (reader2.Read())
                {

                    if (reader2.TokenType == JsonToken.String)
                    {
                        strURLPath = Convert.ToString(reader2.Value);
                        break;
                    }
                    if (reader2.TokenType == JsonToken.Integer)
                    {
                        strURLPath = Convert.ToString(reader2.Value);
                        break;
                    }
                }
                //--------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strURLPath))
                {
                    if (strURLPath == "2")
                    {
                        List<string> strlink = new List<string>();

                        strResponse = makeHTTPGetRequest(strBaseURL + "ded/downloadmultiple.xhtml?reqNo=" + strReqNo);
                        strlink = getDownloadAnchorLink(strResponse);
                        string strDWNLink = "";
                        foreach (string strAllLink in strlink)
                        {
                            //strDownloadLink = strlink[0];
                            //strDownloadLink = strAllLink;
                            strDWNLink = "https://www.tdscpc.gov.in" + strAllLink;
                            //---------------------------------------------------------
                            strURL = GetFileLocation(strDWNLink);

                            if (String.IsNullOrEmpty(strURL))
                            {
                                objResponse.Respons = enmResponse.Failed;
                                objResponse.Message = "due to Traces server failure !!";
                                return objResponse;
                            }
                            if (Regex.IsMatch(strURL, "ibm_security_logout"))
                            {
                                objResponse.Respons = enmResponse.SessionTimeout;
                                objResponse.Message = "Session Timeout Login again !!";
                                return objResponse;
                            }
                            // --------------------------------------------------
                            if (string.IsNullOrEmpty(strURL))
                            {
                                objResponse.Respons = enmResponse.Failed;
                                return objResponse;
                            }
                            //------------------------------------------------
                            if (!makeHttpDownloadRequest(strURL, strPath))
                                objResponse.Respons = enmResponse.Failed;

                            //ADDED BY DHRUB ON 20/03/2014
                            TDSMAN.T_DownloadedFileNameFromTraces = strURL.Substring(strURL.LastIndexOf("/") + 1, (strURL.Length - 1) - (strURL.LastIndexOf("/")));
                        }



                        //objResponse.Respons = enmResponse.Failed;
                        //objResponse.Message = "Multiple File Download not available !!";
                        //return objResponse;
                    }
                    else
                    {

                        if (!makeHttpDownloadRequest(strURLPath, strPath))
                            objResponse.Respons = enmResponse.Failed;

                        //ADDED BY DHRUB ON 20/03/2014
                        TDSMAN.T_DownloadedFileNameFromTraces = strURLPath.Substring(strURLPath.LastIndexOf("/") + 1, (strURLPath.Length - 1) - (strURLPath.LastIndexOf("/")));
                    }
                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "due to Traces server failure !!";
                    return objResponse;
                }

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
        //public TracesResponse RequestForPANValidation(string strPAN, string FormNo)
        //{
        //    TracesResponse objResponse = new TracesResponse();
        //    string strResponse = "";
        //    //----------------------------------------
        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //         * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");
        //        //-----------------------------------------------------------------------
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //        //------------------------------------------------------------------------
        //        StringBuilder sbParameter = new StringBuilder();
        //        sbParameter.Append("pannumber=" + strPAN);
        //        sbParameter.Append("&frmType1=" + FormNo);
        //        sbParameter.Append("&clickGo1=Go");
        //        sbParameter.Append("&pandetailsForm1_SUBMIT=1");
        //        /* -----------------------------------------------------------------------
        //           RETRIEVE VIEWSTATE DATA                
        //           --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"pandetailsForm1\"]");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if ("javax.faces.ViewState" == pair.Key)
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        /* --------------------------------------------------------------------
        //          3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
        //        //-----------------------------------------------------------------------
        //        if (objResponse.Respons == enmResponse.Failed)
        //            return objResponse;
        //        //------------------------------------------------------------------------
        //        objNameval = this.RetievePANStatus(strResponse);

        //        PANDetails objPan = new PANDetails();

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key.ToString().ToUpper() == "STATUS")
        //            {
        //                if (pair.Value.ToString().ToUpper() == "VALID")
        //                    objPan.Status = Message.Valid;
        //                else
        //                    objPan.Status = Message.Invalid;
        //            }

        //            if (pair.Key.ToString().ToUpper() == "NAME")
        //                objPan.Name = pair.Value;

        //        }
        //        //----------------------------------------------------
        //        objResponse.CustomeTypes = objPan;


        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Message = err.Message;
        //        objResponse.Respons = enmResponse.Failed;
        //    }

        //    //----------------------------------------
        //    return objResponse;
        //}


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
                json = makeHTTPPostRequest(strBaseURL + "ded/srv/DedStmtStatusServlet?financialYear=" + objData.FAYear + "&quarter=" + objData.Quarter + "&formType=" + objData.Forms + "&reqType=1", objParam);
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
                // MAKE JSON REQUEST requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //-- ANIK 2013-07-13
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Bank Code");
                    table.Columns.Add("Branch Code");
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
                                        case "bankCode":
                                            dRow["Bank Code"] = reader.Value.ToString();
                                            break;
                                        case "branchCode":
                                            dRow["Branch Code"] = reader.Value.ToString();
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

                // MAKE JSON REQUEST requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //-- ANIK 2013-07-13
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=1&bsrCode=" + objData.BSRCode + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=1&bsrCode=" + objData.BSRCode + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Bank Code");
                    table.Columns.Add("Branch Code");
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
                                        case "bankCode":
                                            dRow["Bank Code"] = reader.Value.ToString();
                                            break;
                                        case "branchCode":
                                            dRow["Branch Code"] = reader.Value.ToString();
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
        //public TracesResponse RequestForConsumptionDetails(TracesData objData)
        //{
        //    StringBuilder objBuilder = new StringBuilder();
        //    DataTable table = new DataTable();
        //    //MAKE GET REQUEST
        //    TracesResponse objResponse = new TracesResponse();
        //    objResponse.Respons = enmResponse.Success;


        //    //MAKE GET REQUEST
        //    try
        //    {
        //        //strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/challanstatusquery.xhtml");
        //        ////----------------------------------------------------------------------------------
        //        ////CHECKING ANY ERROR FROM SERVER POINT
        //        //if (!IsStringExists(strServerResponse, "//form[@id=\"chlnStatusForm2\"]"))
        //        //{
        //        //    objResponse.Message = "Server Error";
        //        //    objResponse.Respons = enmResponse.SessionTimeout;
        //        //    return objResponse;
        //        //}
        //        //------------------------------------------------
        //        objBuilder.Append("_search=false");
        //        objBuilder.Append("&rows=2000");
        //        objBuilder.Append("&page=1");
        //        objBuilder.Append("&sidx=tokenNum");
        //        objBuilder.Append("&sord=desc");
        //        //------------------------------------------------
        //        //https://www.tdscpc.gov.in/app/ChlnStatusServlet?reqtype=2&recptNum=290705784&chlnSNo=93554&chlnAmt=217426.00

        //        // MAKE JSON REQUEST  requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
        //        //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount , objBuilder);
        //        //-- ANIK 2013-07-13
        //        strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount, objBuilder);
        //        //------------------------------------------------
        //        //PROCESSING JSON DATA & PUT INTO DATATABLE
        //        //------------------------------------------------
        //        if (!string.IsNullOrEmpty(strServerResponse))
        //        {
        //            int intRowCount = 0;
        //            table = new DataTable();
        //            table.Columns.Add("Token Number");
        //            table.Columns.Add("Finnancial Year");
        //            table.Columns.Add("Quarter");

        //            table.Columns.Add("Form Type");
        //            table.Columns.Add("Claimed Amount");
        //            table.Columns.Add("Challan Status");
        //            table.Columns.Add("Excess Amount Claimed");
        //            table.Columns.Add("Available Amount");

        //            string strLastToken = "Test";
        //            DataRow dRow = null;
        //            // DataColumn column1
        //            //--------------------------------------------------------------------------
        //            using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
        //            {
        //                while (reader.Read())
        //                {
        //                    switch (reader.TokenType)
        //                    {
        //                        case JsonToken.StartObject:
        //                            // Console.Write("Start object: ");
        //                            break;
        //                        case JsonToken.StartArray:
        //                            // Console.Write("Start array: ");
        //                            break;
        //                        case JsonToken.PropertyName:
        //                            //Console.WriteLine(reader.Value.ToString());

        //                            strLastToken = reader.Value.ToString();
        //                            break;
        //                        case JsonToken.EndArray:
        //                            // Console.WriteLine("End array");
        //                            break;
        //                        case JsonToken.EndObject:
        //                            // Console.WriteLine("End object");
        //                            break;
        //                        case JsonToken.String:
        //                        case JsonToken.Integer:
        //                        case JsonToken.Null:
        //                        case JsonToken.Float:
        //                            //------------------------------------------------------
        //                            switch (strLastToken)
        //                            {
        //                                case "rowCount":
        //                                    intRowCount = Convert.ToInt32(reader.Value);
        //                                    break;

        //                                case "tokenNum":
        //                                    dRow = table.NewRow();
        //                                    dRow["Token Number"] = reader.Value.ToString();
        //                                    break;
        //                                case "finYr":
        //                                    dRow["Finnancial Year"] = reader.Value.ToString();
        //                                    break;
        //                                case "qtr":
        //                                    dRow["Quarter"] = reader.Value.ToString();
        //                                    break;
        //                                case "formType":
        //                                    dRow["Form Type"] = reader.Value.ToString();
        //                                    break;
        //                                case "claimAmt":
        //                                    dRow["Claimed Amount"] = reader.Value.ToString();

        //                                    break;

        //                                case "chlnStatus":
        //                                    dRow["Challan Status"] = reader.Value.ToString();
        //                                    break;

        //                                case "excessAmt":
        //                                    dRow["Excess Amount Claimed"] = reader.Value.ToString();
        //                                    break;

        //                                case "availAmt":

        //                                    if (intRowCount > 0)
        //                                    {
        //                                        dRow["Available Amount"] = reader.Value.ToString();
        //                                        table.Rows.Add(dRow);
        //                                    }

        //                                    break;

        //                            }
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "Server error";
        //        }
        //        //-----------------------------------------------
        //        objResponse.CustomeTypes = table;

        //    }
        //    catch (Exception err)
        //    {
        //        if (Regex.IsMatch(err.Message, "410"))
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //        else
        //            objResponse.Respons = enmResponse.Failed;

        //        objResponse.Message = err.Message;
        //    }

        //    return objResponse;

        //}


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
                // objResponse.Message = "Server Error";
                // objResponse.Respons = enmResponse.SessionTimeout;
                // return objResponse;
                //}
                //------------------------------------------------
                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=tokenNum");
                objBuilder.Append("&sord=desc");
                //------------------------------------------------
                //https://www.tdscpc.gov.in/app/ChlnStatusServlet?reqtype=2&recptNum=290705784&chlnSNo=93554&chlnAmt=217426.00

                // MAKE JSON REQUEST requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount , objBuilder);
                //-- ANIK 2013-07-13
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=2&recptNum=" + objData.PRN_NO + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.FromChallanDepositDate + "&bsrCode=" + objData.BSRCode, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (IsStringExists(strServerResponse, "//form[@id=\"loginForm\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }



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
                    double dblAmt = 0;
                    // DataColumn column1
                    //--------------------------------------------------------------------------
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(strServerResponse)))
                    {
                        while (reader.Read())
                        {
                            dblAmt = 0;
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
                                            // dRow["Claimed Amount"] = reader.Value.ToString();


                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                            dRow["Claimed Amount"] = String.Format("{0:0.00}", dblAmt);


                                            break;

                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;

                                        case "excessAmt":
                                            // dRow["Excess Amount Claimed"] = reader.Value.ToString();

                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());

                                            dRow["Excess Amount Claimed"] = String.Format("{0:0.00}", dblAmt);


                                            break;

                                        case "availAmt":

                                            if (intRowCount > 0)
                                            {
                                                if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                    dblAmt = Convert.ToDouble(reader.Value.ToString());

                                                dRow["Available Amount"] = String.Format("{0:0.00}", dblAmt);

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
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Message = err.Message;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "500"))
                {
                    objResponse.Message = "System has encountered some technical problem. Please try after some time";
                    objResponse.Respons = enmResponse.Failed;
                }


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
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/DedTdsTcsSevlet?pan=" + objData.PAN1 + "&stmtMstrId=" + strStmtMstrId + "&finYear=" + objData.FAYear + "&quarter=" + objData.Quarter, objBuilder);
                //-- ARUP @ 2015/06/17
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/DedTdsTcsSevlet?pan=" + objData.PAN1 + "&stmtMstrId=" + strStmtMstrId + "&finYear=" + objData.FAYear + "&quarter=" + objData.Quarter + "&formType=" + objData.Forms, objBuilder);
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
                //---------------------------------------
                string strFormID = "dedkyc";
                //---------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        string strErr = AttributeValie(strResponse, "//div[@id='umchcase']", "Style");

                        this.Logoff();
                        this.bnlSessionExists = false;
                        if (!string.IsNullOrEmpty(strErr))
                            objResponse.Message = "There are unmatched challans in the selected statement";
                        else
                            objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        strFormID = "dedkyc";

                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc1\"]"))
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }

                    }
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
                    sbParameter.Append("&cdrecnum=" + objTraceData.CDRecordNumber);
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
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"" + strFormID + "\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    //-- Arup 2017/01/03
                    if (pair.Key == "bkEntryValue")
                    {
                        if (Convert.ToBoolean(pair.Value) != objTraceData.IsPaymentByBookAdjustment)
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Invalid Details";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;

                        }

                    } 
                    //--
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                    //-- 2013/10/13 by anik
                    //if (pair.Key == "finYr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "qrtr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "frmType")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);
                    //-- 2013/10/13 BY ARUP
                    //if (pair.Key == "finYr" || pair.Key == "finYr0")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "qrtr" || pair.Key == "qrtr0")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "frmType" || pair.Key == "frmType0")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);
                    ////--
                    //if (pair.Key == "tan")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "isChlnNil")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "dedCount")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    ////if (pair.Key == "bkEntryValue")
                    ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "javax.faces.ViewState")
                    //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

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

                TracesResponse resp1 = new TracesResponse();
                //TracesResponse resp2 = new TracesResponse();
                int intIndex = 0;

                if (!string.IsNullOrEmpty(strMessage))
                {
                    if (objTraceData.AddlReqJustificationFile)
                    {
                        resp1 = RequestForAddlJustificationReport(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            strMessage = "Consolidated Statement, Justification Report";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm16AFile)
                    {
                        resp1 = RequestForAddlForm16A(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Form 16A";
                            else
                                strMessage = "Consolidated Statement, Form 16A";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm16File)
                    {
                        resp1 = RequestForAddlForm16(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Form 16 - Part A";
                            else
                                strMessage = "Consolidated Statement, Form 16 - Part A";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm27DFile)
                    {
                        resp1 = RequestForAddlForm27D(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Form 27D";
                            else
                                strMessage = "Consolidated Statement, Form 27D";
                            intIndex++;
                        }
                    }
                    //------------------------------------------------
                    //RequestStatus conso = new RequestStatus();
                    //conso.AuthenticationCode = strAuthenCode;

                    //strMessage += Environment.NewLine + resp1.Message;
                    //strMessage += Environment.NewLine + resp2.Message;

                    if (intIndex > 0)
                        strMessage += " has been requested successfully";

                    // objResponse.CustomeTypes = conso;
                    objResponse.Message = strMessage;
                    objResponse.Respons = enmResponse.Success;
                }



                //if (!string.IsNullOrEmpty(strMessage))
                //{
                //    RequestStatus conso = new RequestStatus();
                //    conso.AuthenticationCode = strAuthenCode;
                //    conso.StatusMessage = strMessage;

                //    objResponse.CustomeTypes = conso;
                //    objResponse.Respons = enmResponse.Success;
                //}
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
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }

                    }
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
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                    //if (pair.Key == "finYr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "qrtr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "frmType")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "tan")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "isChlnNil")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "dedCount")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    ////if (pair.Key == "bkEntryValue")
                    ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "javax.faces.ViewState")
                    //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
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
               TracesResponse resp1 = new TracesResponse();
               //TracesResponse resp2 = new TracesResponse();
               int intIndex = 0;
                if (!string.IsNullOrEmpty(strMessage))
                {
                    if (objTraceData.AddlReqConsoFile)
                    {                       
                        resp1 = RequestForAddlConsoFile(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            strMessage = "Justification Report, Conso file";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm16AFile)
                    {
                        resp1 = RequestForAddlForm16A(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)                            
                                strMessage += ", Form 16A";
                            else
                                strMessage = "Justification Report, Form 16A";                            
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm16File)
                    {
                        resp1 = RequestForAddlForm16(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Form 16 - Part A";
                            else
                                strMessage = "Justification Report, Form 16 - Part A";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqForm27DFile)
                    {
                        resp1 = RequestForAddlForm27D(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Form 27D";
                            else
                                strMessage = "Justification Report, Form 27D";
                            intIndex++;
                        }
                    }
                    //------------------------------------------------
                    //RequestStatus conso = new RequestStatus();
                    //conso.AuthenticationCode = strAuthenCode;

                    //strMessage += Environment.NewLine + resp1.Message;
                    //strMessage += Environment.NewLine + resp2.Message;

                    if(intIndex>0)
                    strMessage += " has been requested successfully";

                    //conso.StatusMessage = strMessage;

                   // objResponse.CustomeTypes = conso;
                    objResponse.Message = strMessage;
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
        //public TracesResponse RequestForDownloadForm16A(TracesLogin objLogin, TracesData objTraceData)
        //{
        //    string strResponse = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();
        //    //-------------------------------------------------
        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //        1.> REQUEST FOR LOGIN INTO TRACES SITES
        //            URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //         --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //              URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER POINT
        //        if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("dwnldFormBulkType=14");
        //        sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //        sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //        sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //        sbParameter.Append("&bulkGo=Go");
        //        sbParameter.Append("&bulkSearch_SUBMIT=1");
        //        /* --------------------------------------------------------------------
        //           RETRIEVE VIEWSTATE DATA                
        //           --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkSearch\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //            {
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //                break;
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //          3.> REQUEST FOR FORM 16A( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml 
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);
        //        /*---------------------------------------------------------------------
        //          CHECKING ANY ERROR FROM SERVER
        //          ---------------------------------------------------------------------*/
        //        objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }
        //        if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
        //       *    Details To Be Printed On Form 16A
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        //-------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);



        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
        //            {
        //                this.Logoff();
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

        //                if (objNameval.Count == 0)
        //                {
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }
        //                //---------------------------------------------------
        //                sbParameter = new StringBuilder();


        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //                sbParameter.Append("&search2=on");
        //                sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
        //                sbParameter.Append("&kycFormType=" + objTraceData.Forms);
        //                sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
        //                sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


        //                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

        //                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //                {
        //                    this.Logoff();
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }

        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
        //       *    CHALLAN DETAILS PARAMETER PREPARATION
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
        //        sbParameter.Append("&stmtSpecKyc=1");
        //        // sbParameter.Append("&frmType=" + objTraceData.Forms);
        //        sbParameter.Append("&bforeLogin=3");
        //        //sbParameter.Append("&finYr=" + objTraceData.FAYear);
        //        //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

        //        //sbParameter.Append("&tan=" );

        //        sbParameter.Append("&token=" + objTraceData.PRN_NO);

        //        // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
        //        //sbParameter.Append("&dedCount=2");

        //        //FOR NILL RETURNS
        //        if (objTraceData.IsNoChallanCheck)
        //            sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
        //        sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

        //        // FOR BOOK ADJUSTMENT
        //        sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
        //        sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

        //        //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
        //        if (objTraceData.panAmtValueCheck)
        //            sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
        //        sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

        //        if (!objTraceData.IsNoChallanCheck)
        //        {
        //            sbParameter.Append("&bsr=" + objTraceData.BSRCode);
        //            sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
        //            sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
        //            sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
        //        }

        //        if (!objTraceData.panAmtValueCheck)
        //        {
        //            sbParameter.Append("&pan1=" + objTraceData.PAN1);
        //            sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
        //            sbParameter.Append("&pan2=" + objTraceData.PAN2);
        //            sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
        //            sbParameter.Append("&pan3=" + objTraceData.PAN3);
        //            sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
        //        }
        //        //--------------------------------------------------------------
        //        sbParameter.Append("&clickKYC=Proceed");
        //        sbParameter.Append("&dedkyc_SUBMIT=1");
        //        //--------------------------------------------------------------------------------------------------------------
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //            //if (pair.Key == "finYr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "qrtr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "frmType")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "tan")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "isChlnNil")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "dedCount")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            ////if (pair.Key == "bkEntryValue")
        //            ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "javax.faces.ViewState")
        //            //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //---------------------------------------------------------------
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            // objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------------------------------------------------
        //        string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
        //        sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
        //        sbParameter.Append("&dedkyc_SUBMIT=1");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

        //        ////objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
        //        //////CHECKING ANY ERROR
        //        ////sbParameter = new StringBuilder();
        //        //////----------------------------------------------------------
        //        ////foreach (KeyValuePair<string, string> pair in objNameval)
        //        ////{
        //        ////    if (pair.Key == "finYr")
        //        ////        sbParameter.Append("fy=" + pair.Value);

        //        ////    if (pair.Key == "qrtr")
        //        ////    {
        //        ////        if (pair.Value == "")
        //        ////            sbParameter.Append("&qr=0");
        //        ////        else
        //        ////            sbParameter.Append("&qr=" + pair.Value);

        //        ////    }

        //        ////    if (pair.Key == "formType")
        //        ////        sbParameter.Append("&ft=" + pair.Value);

        //        ////    if (pair.Key == "dwldType")
        //        ////        sbParameter.Append("&dt=" + pair.Value);

        //        ////}
        //        //-------------------------------------------------------------------------------------------------
        //        //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
        //        string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
        //        //-------------------------------------------------------------------------------------------------
        //        TracesResponse resp1 = new TracesResponse();
        //        int intIndex = 0;               
                
        //        if (!string.IsNullOrEmpty(strMessage))
        //        {
        //            if (objTraceData.AddlReqConsoFile)
        //            {
        //                resp1 = RequestForAddlConsoFile(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    strMessage = "Form 16A, Conso file";
        //                    intIndex++;
        //                }
        //            }

        //            if (objTraceData.AddlReqJustificationFile)
        //            {
        //                resp1 = RequestForAddlJustificationReport(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    if (intIndex == 1)
        //                        strMessage += ", Justification Report";
        //                    else
        //                        strMessage = "Form 16A, Justification Report";
        //                    intIndex++;
        //                }
        //            }

        //            if (intIndex > 0)
        //                strMessage += " has been requested successfully";

        //            objResponse.Message = strMessage;
        //            objResponse.Respons = enmResponse.Success;                                

        //        }
        //        //------------------------------------------------------------
        //        Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //        this.Logoff();

        //    }
        //    return objResponse;

        //}

        #endregion

        #region RequestForDownloadForm16A
        //public TracesResponse RequestForDownloadForm16A(TracesLogin objLogin, TracesData objTraceData)
        //{
        //    string strResponse = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();
        //    //-------------------------------------------------
        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //        1.> REQUEST FOR LOGIN INTO TRACES SITES
        //            URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //         --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //              URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER POINT
        //        if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("dwnldFormBulkType=14");
        //        sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //        sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //        sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //        sbParameter.Append("&bulkGo=Go");
        //        sbParameter.Append("&bulkSearch_SUBMIT=1");
        //        /* --------------------------------------------------------------------
        //           RETRIEVE VIEWSTATE DATA                
        //           --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkSearch\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //            {
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //                break;
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //          3.> REQUEST FOR FORM 16A( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml 
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);
        //        /*---------------------------------------------------------------------
        //          CHECKING ANY ERROR FROM SERVER
        //          ---------------------------------------------------------------------*/
        //        objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }
        //        if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"tabContentForm\"]"))
        //            {
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                Logoff();
        //                return objResponse;
        //            }
        //            else
        //            {

        //                //form16adetls.xhtml?qr="+quarter+"&ft="+formType+"&fy="+finYear+"&download="+downlType;


        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"tabContentForm\"]");
        //                string strDwnl = "";
        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    if (pair.Key == "dwnldType")
        //                    {
        //                        strDwnl = pair.Value;
        //                        break;
        //                    }
        //                //-------------------------------------------------------------------------
        //                strResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?qr=" + objTraceData.Quarter + "&ft=" + objTraceData.Forms + "&fy=" + objTraceData.FAYear + "&download=" + strDwnl);
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
        //       *    Details To Be Printed On Form 16A
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        //-------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);



        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
        //            {
        //                this.Logoff();
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

        //                if (objNameval.Count == 0)
        //                {
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }
        //                //---------------------------------------------------
        //                sbParameter = new StringBuilder();


        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //                sbParameter.Append("&search2=on");
        //                sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
        //                sbParameter.Append("&kycFormType=" + objTraceData.Forms);
        //                sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
        //                sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


        //                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

        //                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //                {
        //                    this.Logoff();
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }

        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
        //       *    CHALLAN DETAILS PARAMETER PREPARATION
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
        //        sbParameter.Append("&stmtSpecKyc=1");
        //        // sbParameter.Append("&frmType=" + objTraceData.Forms);
        //        sbParameter.Append("&bforeLogin=3");
        //        //sbParameter.Append("&finYr=" + objTraceData.FAYear);
        //        //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

        //        //sbParameter.Append("&tan=" );

        //        sbParameter.Append("&token=" + objTraceData.PRN_NO);

        //        // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
        //        //sbParameter.Append("&dedCount=2");

        //        //FOR NILL RETURNS
        //        if (objTraceData.IsNoChallanCheck)
        //            sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
        //        sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

        //        // FOR BOOK ADJUSTMENT
        //        sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
        //        sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

        //        //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
        //        if (objTraceData.panAmtValueCheck)
        //            sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
        //        sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

        //        if (!objTraceData.IsNoChallanCheck)
        //        {
        //            sbParameter.Append("&bsr=" + objTraceData.BSRCode);
        //            sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
        //            sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
        //            sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
        //        }

        //        if (!objTraceData.panAmtValueCheck)
        //        {
        //            sbParameter.Append("&pan1=" + objTraceData.PAN1);
        //            sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
        //            sbParameter.Append("&pan2=" + objTraceData.PAN2);
        //            sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
        //            sbParameter.Append("&pan3=" + objTraceData.PAN3);
        //            sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
        //        }
        //        //--------------------------------------------------------------
        //        sbParameter.Append("&clickKYC=Proceed");
        //        sbParameter.Append("&dedkyc_SUBMIT=1");
        //        //--------------------------------------------------------------------------------------------------------------
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            //-- Arup 2017/01/03
        //            if (pair.Key == "bkEntryValue")
        //            {
        //                if (Convert.ToBoolean(pair.Value) != objTraceData.IsPaymentByBookAdjustment)
        //                {
        //                    this.Logoff();
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Invalid Details";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;

        //                }
        //            }
        //            //--
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //            //if (pair.Key == "finYr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "qrtr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "frmType")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "tan")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "isChlnNil")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "dedCount")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            ////if (pair.Key == "bkEntryValue")
        //            ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "javax.faces.ViewState")
        //            //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //---------------------------------------------------------------
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            // objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------------------------------------------------
        //        string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
        //        sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
        //        sbParameter.Append("&dedkyc_SUBMIT=1");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

        //        ////objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
        //        //////CHECKING ANY ERROR
        //        ////sbParameter = new StringBuilder();
        //        //////----------------------------------------------------------
        //        ////foreach (KeyValuePair<string, string> pair in objNameval)
        //        ////{
        //        ////    if (pair.Key == "finYr")
        //        ////        sbParameter.Append("fy=" + pair.Value);

        //        ////    if (pair.Key == "qrtr")
        //        ////    {
        //        ////        if (pair.Value == "")
        //        ////            sbParameter.Append("&qr=0");
        //        ////        else
        //        ////            sbParameter.Append("&qr=" + pair.Value);

        //        ////    }

        //        ////    if (pair.Key == "formType")
        //        ////        sbParameter.Append("&ft=" + pair.Value);

        //        ////    if (pair.Key == "dwldType")
        //        ////        sbParameter.Append("&dt=" + pair.Value);

        //        ////}
        //        //-------------------------------------------------------------------------------------------------
        //        //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
        //        string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
        //        //-------------------------------------------------------------------------------------------------
        //        TracesResponse resp1 = new TracesResponse();
        //        int intIndex = 0;

        //        if (!string.IsNullOrEmpty(strMessage))
        //        {
        //            if (objTraceData.AddlReqConsoFile)
        //            {
        //                resp1 = RequestForAddlConsoFile(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    strMessage = "Form 16A, Conso file";
        //                    intIndex++;
        //                }
        //            }

        //            if (objTraceData.AddlReqJustificationFile)
        //            {
        //                resp1 = RequestForAddlJustificationReport(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    if (intIndex == 1)
        //                        strMessage += ", Justification Report";
        //                    else
        //                        strMessage = "Form 16A, Justification Report";
        //                    intIndex++;
        //                }
        //            }

        //            if (intIndex > 0)
        //                strMessage += " has been requested successfully";

        //            objResponse.Message = strMessage;
        //            objResponse.Respons = enmResponse.Success;

        //        }
        //        //------------------------------------------------------------
        //        Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //        this.Logoff();

        //    }
        //    return objResponse;

        //}

        #endregion

        #region RequestForDownloadForm16A
        //public TracesResponse RequestForDownloadForm16A(TracesLogin objLogin, TracesData objTraceData)
        //{
        //    string strResponse = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();
        //    //-------------------------------------------------
        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //        1.> REQUEST FOR LOGIN INTO TRACES SITES
        //            URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //         --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //              URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER POINT
        //        if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("dwnldFormBulkType=14");
        //        sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
        //        sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
        //        sbParameter.Append("&bulkformType=" + objTraceData.Forms);
        //        sbParameter.Append("&bulkGo=Go");
        //        sbParameter.Append("&bulkSearch_SUBMIT=1");
        //        /* --------------------------------------------------------------------
        //           RETRIEVE VIEWSTATE DATA                
        //           --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkSearch\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //            {
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //                break;
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //          3.> REQUEST FOR FORM 16A( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml 
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16a.xhtml", sbParameter);
        //        /*---------------------------------------------------------------------
        //          CHECKING ANY ERROR FROM SERVER
        //          ---------------------------------------------------------------------*/
        //        objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }
        //        if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"tabContentForm\"]"))
        //            {
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                Logoff();
        //                return objResponse;
        //            }
        //            else
        //            {

        //                //form16adetls.xhtml?qr="+quarter+"&ft="+formType+"&fy="+finYear+"&download="+downlType;


        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"tabContentForm\"]");
        //                string strDwnl = "";
        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    if (pair.Key == "dwnldType")
        //                    {
        //                        strDwnl = pair.Value;
        //                        break;
        //                    }
        //                //-------------------------------------------------------------------------
        //                strResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?qr=" + objTraceData.Quarter + "&ft=" + objTraceData.Forms + "&fy=" + objTraceData.FAYear + "&download=" + strDwnl);
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
        //       *    Details To Be Printed On Form 16A
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        //-------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);



        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
        //            {
        //                this.Logoff();
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

        //                if (objNameval.Count == 0)
        //                {
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }
        //                //---------------------------------------------------
        //                sbParameter = new StringBuilder();


        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //                sbParameter.Append("&search2=on");
        //                sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
        //                sbParameter.Append("&kycFormType=" + objTraceData.Forms);
        //                sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
        //                sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


        //                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

        //                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //                {
        //                    this.Logoff();
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }

        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
        //       *    CHALLAN DETAILS PARAMETER PREPARATION
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
        //        sbParameter.Append("&stmtSpecKyc=1");
        //        // sbParameter.Append("&frmType=" + objTraceData.Forms);
        //        sbParameter.Append("&bforeLogin=3");
        //        //sbParameter.Append("&finYr=" + objTraceData.FAYear);
        //        //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

        //        //sbParameter.Append("&tan=" );

        //        sbParameter.Append("&token=" + objTraceData.PRN_NO);

        //        // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
        //        //sbParameter.Append("&dedCount=2");

        //        //FOR NILL RETURNS
        //        if (objTraceData.IsNoChallanCheck)
        //            sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
        //        sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

        //        // FOR BOOK ADJUSTMENT
        //        sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
        //        sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

        //        //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
        //        if (objTraceData.panAmtValueCheck)
        //            sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
        //        sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

        //        if (!objTraceData.IsNoChallanCheck)
        //        {
        //            sbParameter.Append("&bsr=" + objTraceData.BSRCode);
        //            sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
        //            sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
        //            sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
        //        }

        //        if (!objTraceData.panAmtValueCheck)
        //        {
        //            sbParameter.Append("&pan1=" + objTraceData.PAN1);
        //            sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
        //            sbParameter.Append("&pan2=" + objTraceData.PAN2);
        //            sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
        //            sbParameter.Append("&pan3=" + objTraceData.PAN3);
        //            sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
        //        }
        //        //--------------------------------------------------------------
        //        sbParameter.Append("&clickKYC=Proceed");
        //        sbParameter.Append("&dedkyc_SUBMIT=1");
        //        //--------------------------------------------------------------------------------------------------------------
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            //-- Arup 2017/01/03
        //            /*  if (pair.Key == "bkEntryValue")
        //              {
        //                  if (Convert.ToBoolean(pair.Value) != objTraceData.IsPaymentByBookAdjustment)
        //                  {
        //                      this.Logoff();
        //                      this.bnlSessionExists = false;
        //                      objResponse.Message = "Invalid Details";
        //                      objResponse.Respons = enmResponse.SessionTimeout;
        //                      return objResponse;

        //                  }
        //              } */
        //            //--
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //            //if (pair.Key == "finYr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "qrtr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "frmType")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "tan")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "isChlnNil")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "dedCount")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            ////if (pair.Key == "bkEntryValue")
        //            ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "javax.faces.ViewState")
        //            //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //---------------------------------------------------------------
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            // objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------------------------------------------------
        //        string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
        //        sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
        //        sbParameter.Append("&dedkyc_SUBMIT=1");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

        //        ////objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");
        //        //////CHECKING ANY ERROR
        //        ////sbParameter = new StringBuilder();
        //        //////----------------------------------------------------------
        //        ////foreach (KeyValuePair<string, string> pair in objNameval)
        //        ////{
        //        ////    if (pair.Key == "finYr")
        //        ////        sbParameter.Append("fy=" + pair.Value);

        //        ////    if (pair.Key == "qrtr")
        //        ////    {
        //        ////        if (pair.Value == "")
        //        ////            sbParameter.Append("&qr=0");
        //        ////        else
        //        ////            sbParameter.Append("&qr=" + pair.Value);

        //        ////    }

        //        ////    if (pair.Key == "formType")
        //        ////        sbParameter.Append("&ft=" + pair.Value);

        //        ////    if (pair.Key == "dwldType")
        //        ////        sbParameter.Append("&dt=" + pair.Value);

        //        ////}
        //        //-------------------------------------------------------------------------------------------------
        //        //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
        //        string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
        //        //-------------------------------------------------------------------------------------------------
        //        TracesResponse resp1 = new TracesResponse();
        //        int intIndex = 0;

        //        if (!string.IsNullOrEmpty(strMessage))
        //        {
        //            if (objTraceData.AddlReqConsoFile)
        //            {
        //                resp1 = RequestForAddlConsoFile(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    strMessage = "Form 16A, Conso file";
        //                    intIndex++;
        //                }
        //            }

        //            if (objTraceData.AddlReqJustificationFile)
        //            {
        //                resp1 = RequestForAddlJustificationReport(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    if (intIndex == 1)
        //                        strMessage += ", Justification Report";
        //                    else
        //                        strMessage = "Form 16A, Justification Report";
        //                    intIndex++;
        //                }
        //            }

        //            if (intIndex > 0)
        //                strMessage += " has been requested successfully";

        //            objResponse.Message = strMessage;
        //            objResponse.Respons = enmResponse.Success;

        //        }
        //        //------------------------------------------------------------
        //        Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //        this.Logoff();

        //    }
        //    return objResponse;

        //}

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
                    if (!IsStringExists(strResponse, "//form[@id=\"tabContentForm\"]"))
                    {
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        Logoff();
                        return objResponse;
                    }
                    else
                    {

                        //form16adetls.xhtml?qr="+quarter+"&ft="+formType+"&fy="+finYear+"&download="+downlType;


                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"tabContentForm\"]");
                        string strDwnl = "";
                        foreach (KeyValuePair<string, string> pair in objNameval)
                            if (pair.Key == "dwnldType")
                            {
                                strDwnl = pair.Value;
                                break;
                            }
                        //-------------------------------------------------------------------------
                        strResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?qr=" + objTraceData.Quarter + "&ft=" + objTraceData.Forms + "&fy=" + objTraceData.FAYear + "&download=" + strDwnl);
                    }
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
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }

                    }
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
                    //-- Arup 2017/01/03
                    /*  if (pair.Key == "bkEntryValue")
                      {
                          if (Convert.ToBoolean(pair.Value) != objTraceData.IsPaymentByBookAdjustment)
                          {
                              this.Logoff();
                              this.bnlSessionExists = false;
                              objResponse.Message = "Invalid Details";
                              objResponse.Respons = enmResponse.SessionTimeout;
                              return objResponse;

                          }
                      } */
                    //--
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                    //if (pair.Key == "finYr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "qrtr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "frmType")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "tan")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "isChlnNil")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "dedCount")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    ////if (pair.Key == "bkEntryValue")
                    ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "javax.faces.ViewState")
                    //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
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
                //-------------------------------------------------------------------------------------------------
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                TracesResponse resp1 = new TracesResponse();
                int intIndex = 0;

                if (!string.IsNullOrEmpty(strMessage))
                {
                    if (objTraceData.AddlReqConsoFile)
                    {
                        resp1 = RequestForAddlConsoFile(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            strMessage = "Form 16A, Conso file";
                            intIndex++;
                        }
                    }

                    if (objTraceData.AddlReqJustificationFile)
                    {
                        resp1 = RequestForAddlJustificationReport(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Justification Report";
                            else
                                strMessage = "Form 16A, Justification Report";
                            intIndex++;
                        }
                    }

                    if (intIndex > 0)
                        strMessage += " has been requested successfully";

                    objResponse.Message = strMessage;
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
        //public TracesResponse RequestForDownloadForm16(TracesLogin objLogin, TracesData objTraceData)
        //{
        //    string strResponse = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();
        //    //-------------------------------------------------
        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //        1.> REQUEST FOR LOGIN INTO TRACES SITES
        //            URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //         --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //              URL :: https://www.tdscpc.gov.in/app/ded/download16.xhtml
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER POINT
        //        if (!IsStringExists(strResponse, "//form[@id=\"bulkPan\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            this.Logoff();

        //            return objResponse;
        //        }
        //        //------------------------------------------------------------
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("dwnldFormBulkType=13");
        //        sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);

        //        sbParameter.Append("&bulkGo=Go");
        //        sbParameter.Append("&bulkPan_SUBMIT=1");
        //        /* --------------------------------------------------------------------
        //           RETRIEVE VIEWSTATE DATA                
        //           --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkPan\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            this.Logoff();

        //            return objResponse;
        //        }
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //            {
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //                break;
        //            }
        //        }
        //        /* --------------------------------------------------------------------
        //          3.> REQUEST FOR FORM 16( justrepdwnld.xhtml file) WITH 
        //         *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //         * URL :: https://www.tdscpc.gov.in/app/ded/download16.xhtml 
        //           --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/download16.xhtml", sbParameter);
        //        /*---------------------------------------------------------------------
        //          CHECKING ANY ERROR FROM SERVER
        //          ---------------------------------------------------------------------*/
        //        objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FORM 16A (ded/form16adetls.xhtml file) WITH 
        //       *    Details To Be Printed On Form 16A
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //------------------------------------------------------------------------- 
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("j_id1972728517_7cc7de5f=submit");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        //-------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/form16adetls.xhtml", sbParameter);               
        //        //---------------------------------------
        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
        //            {
        //                this.Logoff();
        //                this.bnlSessionExists = false;
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

        //                if (objNameval.Count == 0)
        //                {
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }
        //                //---------------------------------------------------
        //                sbParameter = new StringBuilder();


        //                foreach (KeyValuePair<string, string> pair in objNameval)
        //                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //                sbParameter.Append("&search2=on");
        //                sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
        //                sbParameter.Append("&kycFormType=" + objTraceData.Forms);
        //                sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
        //                sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


        //                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

        //                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //                {
        //                    this.Logoff();
        //                    this.bnlSessionExists = false;
        //                    objResponse.Message = "Server Error";
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    return objResponse;
        //                }

        //            }
        //        }

        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
        //       *    CHALLAN DETAILS PARAMETER PREPARATION
        //       * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
        //         --------------------------------------------------------------------*/
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
        //        sbParameter.Append("&stmtSpecKyc=1");
        //        // sbParameter.Append("&frmType=" + objTraceData.Forms);
        //        sbParameter.Append("&bforeLogin=3");
        //        //sbParameter.Append("&finYr=" + objTraceData.FAYear);
        //        //sbParameter.Append("&qrtr=" + objTraceData.Quarter);

        //        //sbParameter.Append("&tan=" );

        //        sbParameter.Append("&token=" + objTraceData.PRN_NO);

        //        // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
        //        //sbParameter.Append("&dedCount=2");

        //        //FOR NILL RETURNS
        //        if (objTraceData.IsNoChallanCheck)
        //            sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
        //        sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

        //        // FOR BOOK ADJUSTMENT
        //        sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
        //        sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

        //        //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
        //        if (objTraceData.panAmtValueCheck)
        //            sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
        //        sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

        //        if (!objTraceData.IsNoChallanCheck)
        //        {
        //            sbParameter.Append("&bsr=" + objTraceData.BSRCode);
        //            sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
        //            sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
        //            sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
        //        }

        //        if (!objTraceData.panAmtValueCheck)
        //        {
        //            sbParameter.Append("&pan1=" + objTraceData.PAN1);
        //            sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
        //            sbParameter.Append("&pan2=" + objTraceData.PAN2);
        //            sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
        //            sbParameter.Append("&pan3=" + objTraceData.PAN3);
        //            sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
        //        }
        //        //--------------------------------------------------------------
        //        sbParameter.Append("&clickKYC=Proceed");
        //        sbParameter.Append("&dedkyc_SUBMIT=1");
        //        //--------------------------------------------------------------------------------------------------------------
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //            //if (pair.Key == "finYr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "qrtr")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "frmType")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "tan")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "isChlnNil")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "dedCount")
        //            //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            ////if (pair.Key == "bkEntryValue")
        //            ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

        //            //if (pair.Key == "javax.faces.ViewState")
        //            //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //---------------------------------------------------------------
        //        if (objNameval.Count <= 0)
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            Logoff();
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            // objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            Logoff();
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------------------------------------------------
        //        string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
        //        sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
        //        sbParameter.Append("&dedkyc_SUBMIT=1");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "javax.faces.ViewState")
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);
        //        //-------------------------------------------------------------------------------------------------
        //        //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
        //        string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
        //        //-------------------------------------------------------------------------------------------------
        //        TracesResponse resp1 = new TracesResponse();
        //        int intIndex = 0;
        //        if (!string.IsNullOrEmpty(strMessage))
        //        {
        //            if (objTraceData.AddlReqConsoFile)
        //            {
        //                resp1 = RequestForAddlConsoFile(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    strMessage = "Form 16 - Part A, Conso file";
        //                    intIndex++;
        //                }
        //            }
        //            if (objTraceData.AddlReqJustificationFile)
        //            {
        //                resp1 = RequestForAddlJustificationReport(objTraceData);
        //                if (resp1.Respons == enmResponse.Success)
        //                {
        //                    if (intIndex == 1)
        //                        strMessage += ", Justification Report";
        //                    else
        //                        strMessage = "Form 16 - Part A, Justification Report";
        //                    intIndex++;
        //                }
        //            }


        //            if (intIndex > 0)
        //                strMessage += " has been requested successfully";

        //            objResponse.Message = strMessage;
        //            objResponse.Respons = enmResponse.Success;

        //        }
        //        this.Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //        this.Logoff();

        //    }
        //    return objResponse;

        //}

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
                    if (!IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
                    {
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.Failed;
                        return objResponse;
                    }
                }
                //ADDED ON 10-10-15
                if (IsStringExists(strServerResponse, "//form[@id=\"tabContentForm\"]"))
                {


                    objNameval = TraceViewStateData(strServerResponse, "//form[@id=\"tabContentForm\"]");
                    //CHECKING ANY ERROR
                    if (objNameval.Count <= 0)
                    {
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.Failed;
                        return objResponse;
                    }
                    string strParam = "qr=6";
                    //------------------------------------------------------------------------- 
                    sbParameter = new StringBuilder();
                    foreach (KeyValuePair<string, string> pair in objNameval)
                    {
                        if (pair.Key == "frmType")
                            strParam += "&ft=" + HttpUtility.UrlEncode(pair.Value);
                        else if (pair.Key == "finYear")
                            strParam += "&fy=" + HttpUtility.UrlEncode(pair.Value);
                        else if (pair.Key == "dwnldType")
                            strParam += "&download=" + HttpUtility.UrlEncode(pair.Value);
                    }
                    //-------------------------------------------------------------------------
                    strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/form16adetls.xhtml?" + strParam);
                    //strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/form16unmatch.xhtml", sbParameter);

                    if (!IsStringExists(strServerResponse, "//form[@id=\"deducteeDetails\"]"))
                    {
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.Failed;
                        Logoff();
                        return objResponse;
                    }

                    objResponse = IsServerError(strServerResponse, "//ul[@id=\"err_Summary\"]");
                    strResponse = strServerResponse;
                    if (objResponse.Respons == enmResponse.Failed)
                    {
                        Logoff();
                        return objResponse;
                    }
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
                //---------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }

                    }
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
                    //-- Arup 2017/01/03
                    if (pair.Key == "bkEntryValue")
                    {
                        if (Convert.ToBoolean(pair.Value) != objTraceData.IsPaymentByBookAdjustment)
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Invalid Details";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;

                        }
                    }
                    //--
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                    //if (pair.Key == "finYr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "qrtr")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "frmType")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "tan")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "isChlnNil")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "dedCount")
                    //    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    ////if (pair.Key == "bkEntryValue")
                    ////    sbParameter.Append("&" + pair.Key + "=" + pair.Value);

                    //if (pair.Key == "javax.faces.ViewState")
                    //    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
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
                //-------------------------------------------------------------------------------------------------
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                TracesResponse resp1 = new TracesResponse();
                int intIndex = 0;
                if (!string.IsNullOrEmpty(strMessage))
                {
                    if (objTraceData.AddlReqConsoFile)
                    {
                        resp1 = RequestForAddlConsoFile(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            strMessage = "Form 16 - Part A, Conso file";
                            intIndex++;
                        }
                    }
                    if (objTraceData.AddlReqJustificationFile)
                    {
                        resp1 = RequestForAddlJustificationReport(objTraceData);
                        if (resp1.Respons == enmResponse.Success)
                        {
                            if (intIndex == 1)
                                strMessage += ", Justification Report";
                            else
                                strMessage = "Form 16 - Part A, Justification Report";
                            intIndex++;
                        }
                    }


                    if (intIndex > 0)
                        strMessage += " has been requested successfully";

                    objResponse.Message = strMessage;
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

        #region RequestForDownloadForm27D
        public TracesResponse RequestForDownloadForm27D(TracesLogin objLogin, TracesData objTraceData)
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
                  2.> REQUEST FOR DOWNLOAD FORM 27D( download27d.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/download27d.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
                {
                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    this.Logoff();

                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("dwnldFormBulkType=19");
                sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
                sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
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
                  3.> REQUEST FOR FORM 27D( download27d.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml 
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/download27d.xhtml", sbParameter);
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
                4.> REQUEST FORM 27D (ded/download27d.xhtml file) WITH 
               *    Details To Be Printed On Form 27D
               * URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml
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
                sbParameter.Append("j_id2143335333_643da170=submit");

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                //-------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/form27ddetls.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"dedkyc1\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        {
                            this.Logoff();
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                    }
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
               *    CHALLAN DETAILS PARAMETER PREPARATION
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                //sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
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
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc1\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
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

               //-------------------------------------------------------------------------------------------------
               //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                
                TracesResponse resp1 = new TracesResponse();
               //TracesResponse resp2 = new TracesResponse();
               int intIndex = 0;
               if (!string.IsNullOrEmpty(strMessage))
               {
                   if (objTraceData.AddlReqConsoFile)
                   {
                       resp1 = RequestForAddlConsoFile(objTraceData);
                       if (resp1.Respons == enmResponse.Success)
                       {
                           strMessage = "Form 27D, Conso file";
                           intIndex++;
                       }
                   }
                   if (objTraceData.AddlReqJustificationFile)
                   {
                       resp1 = RequestForAddlJustificationReport(objTraceData);
                       if (resp1.Respons == enmResponse.Success)
                       {
                           if (intIndex == 1)
                               strMessage += ", Justification Report";
                           else
                               strMessage = "Form 27D, Justification Report";
                           intIndex++;
                       }
                   }

                   if (intIndex > 0)
                       strMessage += " has been requested successfully";

                   objResponse.Message = strMessage;
                   objResponse.Respons = enmResponse.Success;          

               }
                    //RequestStatus conso = new RequestStatus();
                    //conso.AuthenticationCode = strAuthenCode;
                    //conso.StatusMessage = strMessage;

                    //objResponse.CustomeTypes = conso;
                    //objResponse.Respons = enmResponse.Success;
                
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




        #region RequestForCertificate197
        //public TracesResponse RequestForCertificate197(TracesLogin objLogin, Certificate197Data objCertData)
        //{
        //    string strResponse = "";
        //    string strLink = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();
        //    DataTable table = new DataTable();

        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //          1.> REQUEST FOR LOGIN INTO TRACES SITES
        //              URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //           --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //          2.> REQUEST FOR NSDL Validate 197 Certificate FILE( 197certiverfication.xhtml )
        //              URL :: https://www.tdscpc.gov.in/app/ded/197certiverfication.xhtml
        //           --------------------------------------------------------------------*/

        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/197certiverfication.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER END
        //        if (!IsStringExists(strResponse, "//form[@id=\"certiValidation\"]"))
        //        {
        //            string strErrMessage = RetrieveElementValue(strResponse, "//div[@class=\"padLeft5 margintop20\"]", "//span[@class=\"boldFont\"]", enmElementType.InnerText);

        //            if (!string.IsNullOrEmpty(strErrMessage))
        //            {
        //                objResponse.Message = strErrMessage;
        //                objResponse.Respons = enmResponse.Failed;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                //--
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //        }

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            this.Logoff();
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //--------------------------------------------------------------------------------
        //        string postData = "_search=false&nd=1396609417964&rows=100&page=1&sidx=&sord=asc";
        //        //--------------------------------------------------------------------------------
        //        strResponse = makeHTTPJSONPostRequest(strBaseURL + "ded/srv/CertiVerifyServlet?certiNo=" + objCertData.CertificateNo + "&deducteePan=" + objCertData.PAN + "&financialYear=" + objCertData.FinYear + "&reqType=1", postData);
        //        //--------------------------------------------------------------------------------
        //        string strRowCount = "0";
        //        if (!string.IsNullOrEmpty(strResponse))
        //            table = JsonParserForCertificateValidation(strResponse, out strRowCount);
        //        //--------------------------------------------------------------------------------
        //        ArrayList objList = new ArrayList();
        //        objList.Add(strRowCount);
        //        objList.Add(table);

        //        objResponse.CustomeTypes = objList;
        //        objResponse.Respons = enmResponse.Success;

        //        //Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        this.Logoff();
        //        this.bnlSessionExists = false;
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //    }
        //    return objResponse;
        //}

        #endregion

        #region RequestForCertificate197
        public TracesResponse RequestForCertificate197(Certificate197Data objCertData)
        {
            string strResponse = "";
            string strLink = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            DataTable table = new DataTable();

            try
            {
                /* --------------------------------------------------------------------
                1.> REQUEST FOR LOGIN INTO TRACES SITES
                URL :: https://www.tdscpc.gov.in/app/login.xhtml
                --------------------------------------------------------------------*/
                //if (!IsSessionExists)
                //{
                //objResponse = this.makeLoginToTRACES(objLogin);
                //if (objResponse.Respons == enmResponse.Failed)
                // return objResponse;
                //}
                /* --------------------------------------------------------------------
                2.> REQUEST FOR NSDL Validate 197 Certificate FILE( 197certiverfication.xhtml )
                URL :: https://www.tdscpc.gov.in/app/ded/197certiverfication.xhtml
                --------------------------------------------------------------------*/

                strResponse = makeHTTPGetRequest(strBaseURL + "ded/197certiverfication.xhtml");

                //CHECKING ANY ERROR FROM SERVER END
                if (!IsStringExists(strResponse, "//form[@id=\"certiValidation\"]"))
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
                        objResponse.Message = "Session Timeout";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                }

                if (objResponse.Respons == enmResponse.Failed)
                {
                    // this.Logoff();
                    //objResponse.ErrorMessage = "Server Error";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //--------------------------------------------------------------------------------
                string postData = "_search=false&nd=1396609417964&rows=100&page=1&sidx=&sord=asc";
                //--------------------------------------------------------------------------------
                strResponse = makeHTTPJSONPostRequest(strBaseURL + "ded/srv/CertiVerifyServlet?certiNo=" + objCertData.CertificateNo + "&deducteePan=" + objCertData.PAN + "&financialYear=" + objCertData.FinYear + "&reqType=1", postData);
                //--------------------------------------------------------------------------------
                string strRowCount = "0";
                if (!string.IsNullOrEmpty(strResponse))
                    table = JsonParserForCertificateValidation(strResponse, out strRowCount);
                //--------------------------------------------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(strRowCount);
                objList.Add(table);

                objResponse.CustomeTypes = objList;
                objResponse.Respons = enmResponse.Success;

                //Logoff();

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




        
        #region RequestForAddlConsoFile
        public TracesResponse RequestForAddlConsoFile(TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();

            try
            {                
                /* --------------------------------------------------------------------
                  1.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/nsdlconsofile.xhtml");
                               
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
                //---------------------------------------
                string strFormID = "dedkyc";
                //---------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"downloadreqspec\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        string strErr = AttributeValie(strResponse, "//div[@id='umchcase']", "Style");

                        this.Logoff();
                        this.bnlSessionExists = false;
                        if (!string.IsNullOrEmpty(strErr))
                            objResponse.Message = "There are unmatched challans in the selected statement";
                        else
                            objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {                       

                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();

                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=");
                        sbParameter.Append("&kycFormType=");
                        sbParameter.Append("&kycQrtr=");
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");
                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);
                                               

                    }
                }
                    objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreqspec\"]");                   

                    if (objNameval.Count == 0)
                    {
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }

                    sbParameter = new StringBuilder();
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

                    strResponse = makeHTTPGetRequest(strBaseURL + "srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                    string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                    if (!string.IsNullOrEmpty(strMessage))
                    {
                        objResponse.Message = strMessage;
                        objResponse.Respons = enmResponse.Success;
                    }               

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

        #region RequestForAddlForm16A
        public TracesResponse RequestForAddlForm16A(TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                  1.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/download16a.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/download16a.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
                {
                    objResponse.Message = "Form16A Submission failed";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    //Logoff();
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
                    objResponse.Message = "Form16A Submission failed";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    //Logoff();
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
                   // Logoff();
                    return objResponse;
                }
                if (!IsStringExists(strResponse, "//form[@id=\"deducteeDetails\"]"))
                {
                    objResponse.Message = "Form16A Submission failed";
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
                    objResponse.Message = "Form16A Submission failed";
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

                if (!IsStringExists(strResponse, "//form[@id=\"requestConfirm\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Form16A Submission failed";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        //if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        //{
                        //    this.Logoff();
                        //    this.bnlSessionExists = false;
                        //    objResponse.Message = "Server Error";
                        //    objResponse.Respons = enmResponse.SessionTimeout;
                        //    return objResponse;
                        //}

                    }
                }
               
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    //RequestStatus conso = new RequestStatus();
                    //conso.AuthenticationCode = strAuthenCode;
                   // conso.StatusMessage = strMessage;

                    //objResponse.CustomeTypes = conso;
                    objResponse.Message = strMessage;
                    objResponse.Respons = enmResponse.Success;

                }
                //------------------------------------------------------------
                //Logoff();

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

        #region RequestForAddlJustificationReport
        public TracesResponse RequestForAddlJustificationReport(TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                  1.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
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

                if (!IsStringExists(strResponse, "//form[@id=\"downloadreqspec\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        //if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        //{
                        //    this.Logoff();
                        //    this.bnlSessionExists = false;
                        //    objResponse.Message = "Server Error";
                        //    objResponse.Respons = enmResponse.SessionTimeout;
                        //    return objResponse;
                        //}

                    }
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR JUSTIFICATION REPORT ( nsdlconsofile.xhtml file) WITH 
               *    CHALLAN DETAILS PARAMETER PREPARATION
               * URL :: https://www.tdscpc.gov.in/app/ded/justrepdwnld.xhtml
                 --------------------------------------------------------------------*/
               
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
                strResponse = makeHTTPGetRequest(strBaseURL + "srv/CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                   objResponse.Message = strMessage;
                   objResponse.Respons = enmResponse.Success;
                }
                //==============================================
                
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



        #region RequestForAddlForm16
        public TracesResponse RequestForAddlForm16(TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                 1.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
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
                //---------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"requestConfirm\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        this.Logoff();
                        this.bnlSessionExists = false;
                        objResponse.Message = "Server Error";
                        objResponse.Respons = enmResponse.SessionTimeout;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            this.bnlSessionExists = false;
                            objResponse.Message = "Server Error";
                            objResponse.Respons = enmResponse.SessionTimeout;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        ////if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        ////{
                        ////    this.Logoff();
                        ////    this.bnlSessionExists = false;
                        ////    objResponse.Message = "Server Error";
                        ////    objResponse.Respons = enmResponse.SessionTimeout;
                        ////    return objResponse;
                        ////}

                    }
                }               
                //-------------------------------------------------------------------------------------------------
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    objResponse.Message = strMessage;
                    objResponse.Respons = enmResponse.Success;
                }
               
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

        #region RequestForAddlForm27D
        public TracesResponse RequestForAddlForm27D(TracesData objTraceData)
        {
            string strResponse = "";
            StringBuilder sbParameter;
            TracesResponse objResponse = new TracesResponse();
            //-------------------------------------------------
            try
            {
               
                /* --------------------------------------------------------------------
                  1.> REQUEST FOR DOWNLOAD FORM 27D( download27d.xhtml file )
                      URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/download27d.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"bulkSearch\"]"))
                {
                    objResponse.Message = "Form 27D Request Failed";
                    objResponse.Respons = enmResponse.Failed;
                    return objResponse;
                }
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("dwnldFormBulkType=19");
                sbParameter.Append("&bulkfinYr=" + objTraceData.FAYear);
                sbParameter.Append("&bulkquarter=" + objTraceData.Quarter);
                sbParameter.Append("&bulkGo=Go");
                sbParameter.Append("&bulkSearch_SUBMIT=1");
                /* --------------------------------------------------------------------
                   RETRIEVE VIEWSTATE DATA                
                   --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"bulkSearch\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Form 27D Request Failed";
                    objResponse.Respons = enmResponse.Failed;                 

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
                  3.> REQUEST FOR FORM 27D( download27d.xhtml file) WITH 
                 *    FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                 * URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml 
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/download27d.xhtml", sbParameter);
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
                    objResponse.Message = "Form 27D Request Failed";
                    objResponse.Respons = enmResponse.Failed;
                   
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FORM 27D (ded/download27d.xhtml file) WITH 
               *    Details To Be Printed On Form 27D
               * URL :: https://www.tdscpc.gov.in/app/ded/download27d.xhtml
                 --------------------------------------------------------------------*/
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"deducteeDetails\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    objResponse.Message = "Form 27D Request Failed";
                    objResponse.Respons = enmResponse.Failed;                    
                    return objResponse;
                }
                //------------------------------------------------------------------------- 
                sbParameter = new StringBuilder();
                sbParameter.Append("j_id2143335333_643da170=submit");

                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                //-------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/form27ddetls.xhtml", sbParameter);

                if (!IsStringExists(strResponse, "//form[@id=\"requestConfirm\"]"))
                {
                    if (!IsStringExists(strResponse, "//form[@id=\"kycformdsc\"]"))
                    {
                        objResponse.Message = "Form 27D Request Failed";
                        objResponse.Respons = enmResponse.Failed;
                        return objResponse;
                    }
                    else
                    {
                        objNameval = TraceViewStateData(strResponse, "//form[@id=\"kycformdsc\"]");

                        if (objNameval.Count == 0)
                        {
                            objResponse.Message = "Form 27D Request Failed";
                            objResponse.Respons = enmResponse.Failed;
                            return objResponse;
                        }
                        //---------------------------------------------------
                        sbParameter = new StringBuilder();


                        foreach (KeyValuePair<string, string> pair in objNameval)
                            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

                        sbParameter.Append("&search2=on");
                        sbParameter.Append("&kycFinYear=" + objTraceData.FAYear);
                        sbParameter.Append("&kycFormType=" + objTraceData.Forms);
                        sbParameter.Append("&kycQrtr=" + objTraceData.Quarter);
                        sbParameter.Append("&kycformdsc:_idcl=nxtSrcn");


                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3formdsc.xhtml", sbParameter);

                        //if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
                        //{
                        //    objResponse.Message = "Form 27D Request Failed";
                        //    objResponse.Respons = enmResponse.Failed;
                        //    return objResponse;
                        //}
                    }
                }               
                //-------------------------------------------------------------------------------------------------
                //strResponse = makeHTTPGetRequest(strBaseURL + "CreateDwnldReqServlet?" + sbParameter.ToString());
                string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
                //-------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(strMessage))
                {
                    RequestStatus conso = new RequestStatus();
                    //conso.AuthenticationCode = strAuthenCode;
                    conso.StatusMessage = strMessage;

                    objResponse.CustomeTypes = conso;
                    objResponse.Respons = enmResponse.Success;
                }
                

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
        //public TracesResponse makeLoginToTRACES(TracesLogin login)
        //{
        //    TracesResponse objResponse = new TracesResponse();

        //    //CREATE DATA FOR LOGIN FORM
        //    StringBuilder sbParameter = new StringBuilder();
        //    try
        //    {
        //        sbParameter.Append("username=" + login.UserID);
        //        sbParameter.Append("&j_username=" + login.UserID + "^" + login.TAN);
        //        sbParameter.Append("&j_password=" + HttpUtility.UrlEncode(login.Password));
        //        sbParameter.Append("&j_tanPan=" + login.TAN);
        //        sbParameter.Append("&j_captcha=" + login.CaptchaCode);

        //        //MAKE REQUEST TO LOGIN FORM
        //        strServerResponse = makeHTTPPostRequest(strBaseURL + "j_security_check", sbParameter);

        //        if (string.IsNullOrEmpty(strServerResponse))
        //        {
        //            IsSessionExists = false;
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "Login Failed or Server Error";
        //            return objResponse;
        //        }

        //        //CHECKING ANY ERROR FROM SERVER
        //        objResponse = IsServerError(strServerResponse, "//span[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            IsSessionExists = false;
        //            //objResponse.ErrorMessage = "Login Failed or Server Error";
        //            return objResponse;
        //        }
        //        if (!IsConditionMatch(strServerResponse, "You have logged"))
        //        {
        //            objResponse.Respons = enmResponse.Failed;
        //            objResponse.Message = "Login Failed or Server Error";
        //            return objResponse;
        //        }
        //        else
        //        {
        //            IsSessionExists = true;
        //            objResponse.Respons = enmResponse.Success;
        //            objResponse.Message = "";
        //            return objResponse;
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //        return objResponse;
        //    }


        //}

        #endregion

        #region makeLoginToTRACES
        //-- ARUP @ 2014-05-08
        public TracesResponse makeLoginToTRACES(TracesLogin login)
        {
            TracesResponse objResponse = new TracesResponse();

            //CREATE DATA FOR LOGIN FORM
            StringBuilder sbParameter = new StringBuilder();
            try
            {
                //sbParameter.Append("username=" + login.UserID);
                //sbParameter.Append("&j_username=" + login.UserID + "^" + login.TAN);
                //sbParameter.Append("&j_password=" + HttpUtility.UrlEncode(login.Password));
                //sbParameter.Append("&j_tanPan=" + login.TAN);
                //sbParameter.Append("&j_captcha=" + login.CaptchaCode);
                //-- ARUP - 2016/07/19
                sbParameter.Append("username=" + HttpUtility.UrlEncode(login.UserID));
                sbParameter.Append("&j_username=" + HttpUtility.UrlEncode(login.UserID) + "^" + login.TAN);
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

                //
                //form[@id=\"dedkyc\"]
                objResponse = IsServerError(strServerResponse, "//form[@id=\"surveyForm\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    IsSessionExists = false;
                    objResponse.Message = "Please log into your account by visiting www.tdscpc.gov.in and fill the survey form.";
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
                if (!IsConditionMatch(strServerResponse, login.TAN))
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
            //-- ANIK @ 2015/12/03
            if(response != null)
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

        #region makeHTTPJSONPostRequest
        private string makeHTTPJSONPostRequest(string strURL, string sbData)
        {

            request = (HttpWebRequest)WebRequest.Create(strURL);
            //----------------------------------------------------------
            // SET THE METHOD PROPERTY OF THE REQUEST TO POST.
            //----------------------------------------------------------            
            request.KeepAlive = false;

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
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Timeout = 1000000000; // fix 3

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
                request.AllowAutoRedirect = false;
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

        #region GetFileLocation //-- 2016/08/27
        //private string GetFileLocation(string strURL)
        //{
        //    string strLocation;
        //    request = (HttpWebRequest)WebRequest.Create(strURL);
        //    request.KeepAlive = true;
        //    request.Timeout = 300000;
        //    request.AllowWriteStreamBuffering = false;
        //    request.AllowAutoRedirect = false;
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
        //    //---------------------------------------------------           
        //    request.CookieContainer = objContainer;
        //    request.CookieContainer.Add(response.Cookies);
        //    //-------------------------------------------------------
        //    response = (HttpWebResponse)request.GetResponse();

        //    strLocation = response.Headers["Location"];



        //    return strLocation;
        //}

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

        #region GetFileLocation
        private string GetFileLocation(string strURL, string strReqNo)
        {
            string strURLPath = "";
            SetAllowUnsafeHeaderParsing();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.KeepAlive = false;
            // request.Timeout = 300000;
            // request.AllowWriteStreamBuffering = false;
            // request.AllowAutoRedirect = false;
            // request.ServicePoint.Expect100Continue = false;

            byte[] byteArray = null;
            string postData = "";

            try
            {
                if (!string.IsNullOrEmpty(strReqNo))
                {
                    postData = strReqNo.ToString();
                    byteArray = Encoding.UTF8.GetBytes(strReqNo);
                }
                //----------------------------------------------------------
                // SET THE CONTENTLENGTH PROPERTY OF THE WEBREQUEST.
                //----------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(strReqNo)))
                    request.ContentLength = byteArray.Length;

                if (strReqNo != null)
                {
                    request.Method = "POST";
                    //  request.Accept = "*/*";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    //  request.Timeout = 1000000000; // fix 3

                }

                //----------------------------------------------------------
                if (request.CookieContainer == null)
                    request.CookieContainer = objContainer;
                //-- ANIK @ 2015/12/03
                request.CookieContainer.Add(response.Cookies);
                //----------------------------------------------------------
                // GET THE REQUEST STREAM.
                //----------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(strReqNo)))
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
                //---------------------------------------------------           
                //request.CookieContainer = objContainer;
                //request.CookieContainer.Add(response.Cookies);
                //-------------------------------------------------------

                response = (HttpWebResponse)request.GetResponse();

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



            }
            catch (Exception err)
            {
                throw err;
                //if (intcount <= 3)
                //{
                //    intcount++;
                //    GetFileLocation(strURL, strReqNo);
                //}

            }
            return strServerResponse;

        }

        #endregion

      

        #region CheckDownloadURL
        private string CheckDownloadURL(string strURL)
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

        #region TraceViewStateData_Old
        private Dictionary<string, string> TraceViewStateData_Old(string strHTML, string xPathQuery)
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

        #region RetrievePANName
        private string RetrievePANName(string strHTML)
        {
            string Name = "NOT AVAILABLE";
            //
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);

            HtmlNode node = document.DocumentNode;
            //--------------------------------------------------------------------
            HtmlNodeCollection hncHiddenField;

            hncHiddenField = node.SelectNodes("//td[@id='name']");

            if (hncHiddenField != null && hncHiddenField.Count > 0)
            {
                for (int i = 0; i < hncHiddenField.Count; i++)
                {
                    Name = hncHiddenField[i].InnerText;
                }
            }

            return Name;
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
            //-- ANIK @ 2016/03/18
            //strBuilder.Append("requestId=");
            //strBuilder.Append("&panOfDeductee=" + strPan);
            //strBuilder.Append("&captchaCode=" + strCaptchaCode);
            //-------------------------------------------------
            string strURL ="https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html?requestId=&panOfDeductee="+ strPan +"&captchaCode=" + strCaptchaCode;
            string strRes = makeHTTPGetRequest(strURL);

            //-- ANIK @ 2016/03/18
            //string strRes = makeHTTPPostRequest("https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html", strBuilder);

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


        #region DownloadConsoTAN_PANFile
        //public TracesResponse DownloadConsoTAN_PANFile(TracesLogin objLogin, TracesData objTraceData)
        //{
        //    string strResponse = "";
        //    string strLink = "";
        //    StringBuilder sbParameter;
        //    TracesResponse objResponse = new TracesResponse();

        //    try
        //    {
        //        /* --------------------------------------------------------------------
        //        1.> REQUEST FOR LOGIN INTO TRACES SITES
        //        URL :: https://www.tdscpc.gov.in/app/login.xhtml
        //        --------------------------------------------------------------------*/
        //        if (!IsSessionExists)
        //        {
        //            objResponse = this.makeLoginToTRACES(objLogin);
        //            if (objResponse.Respons == enmResponse.Failed)
        //                return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //        2.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file )
        //        URL :: https://www.tdscpc.gov.in/app/ded/panverify.xhtml
        //        --------------------------------------------------------------------*/
        //        strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");

        //        //CHECKING ANY ERROR FROM SERVER POINT
        //        if (!IsStringExists(strResponse, "//form[@id=\"pandetailsForm2\"]"))
        //        {
        //            string strErrMessage = RetrieveElementValue(strResponse, "//div[@class=\"padLeft5 margintop20\"]", "//span[@class=\"boldFont\"]", enmElementType.InnerText);

        //            if (!string.IsNullOrEmpty(strErrMessage))
        //            {
        //                objResponse.Message = strErrMessage;
        //                objResponse.Respons = enmResponse.Failed;
        //                return objResponse;
        //            }
        //            else
        //            {
        //                //--
        //                objResponse.Message = "Server Error";
        //                objResponse.Respons = enmResponse.SessionTimeout;
        //                return objResponse;
        //            }
        //        }

        //        //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

        //        //if (objResponse.Respons == enmResponse.Failed)
        //        //{
        //        // this.Logoff();
        //        // //objResponse.ErrorMessage = "Server Error";
        //        // objResponse.Respons = enmResponse.Failed;
        //        // return objResponse;
        //        //}
        //        //------------------------------------------------------------
        //        sbParameter = new StringBuilder();
        //        sbParameter.Append("finYr=" + objTraceData.FAYear);
        //        sbParameter.Append("&qrtr=" + objTraceData.Quarter);
        //        sbParameter.Append("&frmType=" + objTraceData.Forms);
        //        sbParameter.Append("&clickGo2=Go");
        //        /* -----------------------------------------------------------------------
        //        RETRIEVE VIEWSTATE DATA
        //        --------------------------------------------------------------------*/
        //        Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"pandetailsForm2\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            this.Logoff();
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            if (pair.Key != "pandetailsForm1_SUBMIT")
        //                sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        /* --------------------------------------------------------------------
        //        3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH
        //        * FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
        //        * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
        //        --------------------------------------------------------------------*/
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
        //        /*---------------------------------------------------------------------
        //        CHECKING ANY ERROR FROM SERVER
        //        ---------------------------------------------------------------------*/
        //        objResponse = IsServerError(strResponse, "//ul[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            this.Logoff();
        //            //objResponse.ErrorMessage = "Server Error";
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {
        //            this.Logoff();
        //            this.bnlSessionExists = false;
        //            objResponse.Message = "Please Enter Valid Finnancial Details";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        /* --------------------------------------------------------------------
        //        4.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH
        //        * CHALLAN DETAILS PARAMETER PREPARATION
        //        * URL :: https://www.tdscpc.gov.in/app/ded/kyc3form.xhtml
        //        --------------------------------------------------------------------*/
        //        sbParameter = new StringBuilder();
        //        // sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
        //        sbParameter.Append("&stmtSpecKyc=1");
        //        sbParameter.Append("&bforeLogin=3");
        //        sbParameter.Append("&token=" + objTraceData.PRN_NO);

        //        // sbParameter.Append("&isChlnNil=" + objTraceData.IsNoChallan);
        //        //sbParameter.Append("&dedCount=2");

        //        //FOR NILL RETURNS
        //        if (objTraceData.IsNoChallanCheck)
        //            sbParameter.Append("&cinbinCheck=" + objTraceData.IsNoChallanCheck);
        //        sbParameter.Append("&cinbinValue=" + objTraceData.IsNoChallan);

        //        // FOR BOOK ADJUSTMENT
        //        sbParameter.Append("&bkEntryFlgChk=" + objTraceData.IsPaymentByBookAdjustmentCheck);
        //        sbParameter.Append("&bkEntryValue=" + objTraceData.IsPaymentByBookAdjustment);

        //        //FOR PAN & AMOUNT EMPTY (WHEN THERE IS A NILL RETURNS )
        //        if (objTraceData.panAmtValueCheck)
        //            sbParameter.Append("&panAmtCheck=" + objTraceData.panAmtValueCheck);
        //        sbParameter.Append("&panAmtValue=" + objTraceData.panAmtValue);

        //        if (!objTraceData.IsNoChallanCheck)
        //        {
        //            sbParameter.Append("&bsr=" + objTraceData.BSRCode);
        //            sbParameter.Append("&dtoftaxdep=" + objTraceData.TaxDepositedDate);
        //            sbParameter.Append("&csn=" + objTraceData.ChallanSerialNo);
        //            sbParameter.Append("&chlnamt=" + objTraceData.ChallanAmount);
        //        }

        //        if (!objTraceData.panAmtValueCheck)
        //        {
        //            sbParameter.Append("&pan1=" + objTraceData.PAN1);
        //            sbParameter.Append("&amt1=" + objTraceData.PAN1Amount);
        //            sbParameter.Append("&pan2=" + objTraceData.PAN2);
        //            sbParameter.Append("&amt2=" + objTraceData.PAN2Amount);
        //            sbParameter.Append("&pan3=" + objTraceData.PAN3);
        //            sbParameter.Append("&amt3=" + objTraceData.PAN3Amount);
        //        }
        //        //--------------------------------------------------------------
        //        sbParameter.Append("&clickKYC=Proceed");
        //        //sbParameter.Append("&dedkyc_SUBMIT=1");
        //        //--------------------------------------------------------------------------------------------------------------
        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

        //        //---------------------------------------------------------------
        //        if (objNameval.Count <= 0)
        //        {
        //            this.Logoff();
        //            this.bnlSessionExists = false;

        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------

        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3form.xhtml", sbParameter);

        //        if (!IsStringExists(strResponse, "//form[@id=\"dedkyc\"]"))
        //        {

        //            this.bnlSessionExists = false;
        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }

        //        objResponse = IsServerError(strResponse, "//div[@id=\"err_Summary\"]");

        //        if (objResponse.Respons == enmResponse.Failed)
        //        {
        //            this.Logoff();
        //            objResponse.Respons = enmResponse.Failed;
        //            return objResponse;
        //        }
        //        //---------------------------------------------------------------------------------------------------------
        //        string strAuthenCode = RetrieveElementValue(strResponse, "//form[@id=\"dedkyc\"]", "//input[@id=\"authcode\"]", enmElementType.Value);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
        //        //CHECKING ANY ERROR
        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        sbParameter.Append("authcode=" + HttpUtility.UrlEncode(strAuthenCode.Trim()));
        //        sbParameter.Append("&redirect=" + HttpUtility.UrlEncode("Proceed with Transaction"));
        //        sbParameter.Append("&dedkyc_SUBMIT=1");

        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            //if (pair.Key == "javax.faces.ViewState")
        //            sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

        //        objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreq\"]");
        //        //CHECKING ANY ERROR
        //        if (objNameval.Count <= 0)
        //        {
        //            this.Logoff();
        //            this.bnlSessionExists = false;

        //            objResponse.Message = "Server Error";
        //            objResponse.Respons = enmResponse.SessionTimeout;
        //            return objResponse;
        //        }


        //        sbParameter = new StringBuilder();
        //        //----------------------------------------------------------
        //        foreach (KeyValuePair<string, string> pair in objNameval)
        //        {
        //            if (pair.Key == "finYr")
        //                sbParameter.Append("fy=" + pair.Value);

        //            if (pair.Key == "qrtr")
        //            {
        //                if (pair.Value == "")
        //                    sbParameter.Append("&qr=0");
        //                else
        //                    sbParameter.Append("&qr=" + pair.Value);

        //            }

        //            if (pair.Key == "formType")
        //                sbParameter.Append("&ft=" + pair.Value);

        //            if (pair.Key == "dwldType")
        //                sbParameter.Append("&dt=" + pair.Value);

        //        }
        //        //-------------------------------------------------------------------------------------------------
        //        strResponse = makeHTTPGetRequest(strBaseURL + "srv/CreateDwnldReqServlet?" + sbParameter.ToString());
        //        string strMessage = RetrieveElementValue(strResponse, "//div[@class='margintop20']", "//h5", enmElementType.InnerText);
        //        //-------------------------------------------------------------------------------------------------
        //        if (!string.IsNullOrEmpty(strMessage))
        //        {
        //            RequestStatus conso = new RequestStatus();
        //            conso.AuthenticationCode = strAuthenCode;
        //            conso.StatusMessage = strMessage;

        //            objResponse.CustomeTypes = conso;
        //            objResponse.Respons = enmResponse.Success;
        //        }
        //        //---------------------------------------------------
        //        Logoff();

        //    }
        //    catch (Exception err)
        //    {
        //        this.Logoff();
        //        this.bnlSessionExists = false;
        //        objResponse.Respons = enmResponse.Failed;
        //        objResponse.Message = err.Message;
        //    }
        //    return objResponse;
        //}

        #endregion

        #region DownloadConsoTAN_PANFile
        public TracesResponse DownloadConsoTAN_PANFile(TracesLogin objLogin, TracesData objTraceData)
        {
            string strResponse = "";
            string strLink = "";
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
                URL :: https://www.tdscpc.gov.in/app/ded/panverify.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");

                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strResponse, "//form[@id=\"pandetailsForm2\"]"))
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

                //objResponse = IsServerError(strResponse, "//span[@id=\"err_Summary\"]");

                //if (objResponse.Respons == enmResponse.Failed)
                //{
                // this.Logoff();
                // //objResponse.ErrorMessage = "Server Error";
                // objResponse.Respons = enmResponse.Failed;
                // return objResponse;
                //}
                //------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("finYr=" + objTraceData.FAYear);
                sbParameter.Append("&qrtr=" + objTraceData.Quarter);
                sbParameter.Append("&frmType=" + objTraceData.Forms);
                sbParameter.Append("&frmType2=" + objTraceData.Forms);
                sbParameter.Append("&clickGo2=Go");
                /* -----------------------------------------------------------------------
                RETRIEVE VIEWSTATE DATA
                --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"pandetailsForm2\"]");
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
                    if (pair.Key != "pandetailsForm1_SUBMIT")
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                /* --------------------------------------------------------------------
                3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH
                * FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
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
                    objResponse.Message = "Please Enter Valid Financial Details";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                /* --------------------------------------------------------------------
                4.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH
                * CHALLAN DETAILS PARAMETER PREPARATION
                * URL :: https://www.tdscpc.gov.in/app/ded/kyc3form.xhtml
                --------------------------------------------------------------------*/
                sbParameter = new StringBuilder();
                // sbParameter.Append("authcode=" + objTraceData.AuthenticationCode);
                sbParameter.Append("&stmtSpecKyc=1");
                sbParameter.Append("&bforeLogin=3");
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
                //sbParameter.Append("&dedkyc_SUBMIT=1");
                //--------------------------------------------------------------------------------------------------------------
                objNameval = TraceViewStateData(strResponse, "//form[@id=\"dedkyc\"]");
                //CHECKING ANY ERROR
                //----------------------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));

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
                    string strForType = HTMLTagAttributeValue(strServerResponse, "//input[@id=\"frmType0\"]", "value");
                    string strFinYr = HTMLTagAttributeValue(strServerResponse, "//input[@id=\"finYr0\"]", "value");
                    string strQtr = HTMLTagAttributeValue(strServerResponse, "//input[@id=\"qrtr0\"]", "value");

                    if (objResponse.Message.Replace("\n","") == "Token Number is not valid for Regular Statement")
                    {   
                        List<string> strRet = new List<string>();
                        strRet.Add(strFinYr);
                        strRet.Add(strForType);
                        strRet.Add(strQtr);

                        objResponse.CustomeTypes = strRet;

                    }
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
                    //if (pair.Key == "javax.faces.ViewState")
                    sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                }
                //-------------------------------------------------------------------------------------------------
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/kyc3confirm.xhtml", sbParameter);

                objNameval = TraceViewStateData(strResponse, "//form[@id=\"downloadreq\"]");
                //CHECKING ANY ERROR
                if (objNameval.Count <= 0)
                {
                    this.Logoff();
                    this.bnlSessionExists = false;

                    objResponse.Message = "Server Error";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }


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


        #region IsValidPAN
        public bool IsValidPAN(string PAN_No)
        {
            try
            {
                strServerResponse = makeHTTPGetRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");

                if (!IsStringExists(strServerResponse, "//form[@name=\"selectform\"]"))
                    return false;

                //RETRIEVE FORM'S ACTION VALUES FOR NEXT PAGE REDIRECTION

                string strPageName = HTMLTagAttributeValue(strServerResponse, "//form[@name=\"selectform\"]", "action");

                if (!string.IsNullOrEmpty(strPageName))
                {
                    StringBuilder objPANData = new StringBuilder();

                    objPANData.Append("browser_type=IE");
                    objPANData.Append("&from_tdsnontds=Y");
                    objPANData.Append("&R2=280");

                    strServerResponse = makeHTTPPostRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/" + strPageName, objPANData);

                    objPANData = new StringBuilder();

                    objPANData.Append("AssessYear=");
                    objPANData.Append("&Add_State=");
                    objPANData.Append("&Name=");
                    objPANData.Append("&Add_PIN=");
                    objPANData.Append("&Add_Line1=");
                    objPANData.Append("&Add_Line2=");
                    objPANData.Append("&Add_Line3=");
                    objPANData.Append("&Add_Line4=");
                    objPANData.Append("&Add_Line5=");
                    objPANData.Append("&PAN=" + PAN_No);
                    objPANData.Append("&ChallanNo=");
                    objPANData.Append("&MinorHeadRadio=");
                    objPANData.Append("&MajorHeadRadio=");

                    strServerResponse = makeHTTPPostRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/PopulateBankServlet", objPANData);

                    //$Invalid PAN$

                    if (Regex.IsMatch(strServerResponse, "Invalid PAN", RegexOptions.IgnoreCase)) return false;
                    else
                        return true;
                }
                else
                return false;
                //--
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region IsValidPAN
        public bool IsValidPAN(string strPan, string strCaptchaCode, out bool IsError)
        {
            IsError = false;
            StringBuilder strBuilder = new StringBuilder();
            TracesResponse objResponse = new TracesResponse();

            try
            {
                string strURL = "https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html?requestId=&panOfDeductee=" + strPan + "&captchaCode=" + strCaptchaCode;
                string strRes = makeHTTPGetRequest(strURL);
                objResponse = IsServerError(strRes, "//div[@class=\"error\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    if (Regex.IsMatch(strRes, "Invalid Code"))
                        IsError = true;
                    else if (Regex.IsMatch(strRes, "Invalid PAN"))
                        IsError = true;
                    else
                        IsError = true;
                    return false;
                }
                else
                {
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(strRes);

                    if (Regex.IsMatch(strRes, "PAN does not exist"))
                        return false;

                    return true;
                }

            }
            catch (Exception err)
            {
                IsError = true;
                return false;
            }

        }

        #endregion


        #region IsValidCaptcha
        public bool IsValidCaptcha(string strPan, string strCaptchaCode, out bool IsError)
        {
            IsError = false;
            StringBuilder strBuilder = new StringBuilder();
            TracesResponse objResponse = new TracesResponse();

            try
            {
                string strURL = "https://incometaxindiaefiling.gov.in/e-Filing/Services/KnowYourJurisdiction.html?requestId=&panOfDeductee=" + strPan + "&captchaCode=" + strCaptchaCode;
                string strRes = makeHTTPGetRequest(strURL);
                objResponse = IsServerError(strRes, "//div[@class=\"error\"]");

                if (objResponse.Respons == enmResponse.Failed)
                {
                    if (Regex.IsMatch(strRes, "Invalid Code"))
                        return false;
                    else if (Regex.IsMatch(strRes, "Invalid PAN"))
                        return false;

                    return false;

                }

            }
            catch (Exception err)
            {
                IsError = true;
                return false;
            }

            return true;

        }

        #endregion

        #region getDownloadAnchorLink
        private List<string> getDownloadAnchorLink(string strHTML)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);

            List<string> strList = new List<string>(); 

            HtmlNodeCollection nodeCollection = document.DocumentNode.SelectNodes("//a");

            if (nodeCollection != null)
            {
                foreach (HtmlAgilityPack.HtmlNode nodeWebsiteLink in nodeCollection)
                {
                    HtmlAgilityPack.HtmlAttribute hrefAttribute = nodeWebsiteLink.Attributes["href"];                                       
                   strList.Add(hrefAttribute.Value.ToString().Replace("&amp;", "&").Replace("\r\n",""));
                }
            }

            return strList;
        }

        #endregion

        #region RequestForPANValidation
        public TracesResponse RequestForPANValidation(string strPAN)
        {
            TracesResponse objResponse = new TracesResponse();
            string strResponse = "";
            //----------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                2.> REQUEST FOR TRACES PAN VALIDATION FILE( panverify.xhtml file )
                * URL :: https://www.tdscpc.gov.in/app/ded/panverify.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");
                //-----------------------------------------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"pandetailsForm1\"]"))
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------------------------------
                StringBuilder sbParameter = new StringBuilder();
                sbParameter.Append("pannumber=" + strPAN.Trim());
                sbParameter.Append("&frmType1=24Q");
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
                * FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;
                //------------------------------------------------------------------------


                //------------------------------------------------------------------------------
                //objNameval = this.RetievePANStatus(strResponse);
                //------------------------------------------------------------------------------

                PANDetails objPan = new PANDetails();

                objPan.PAN = strPAN;
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key.ToString().ToUpper() == "STATUS")
                        objPan.Status = pair.Value;

                    if (pair.Key.ToString().ToUpper() == "NAME")
                        objPan.Name = pair.Value;

                }
                //----------------------------------------------------
                objResponse.CustomeTypes = objPan;
                objResponse.Respons = enmResponse.Success;
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

        #region RequestForPANValidation
        //-- ANIK
        public TracesResponse RequestForPANValidation(string strPAN, out string strName)
        {
            TracesResponse objResponse = new TracesResponse();
            string strResponse = "";
            strName = "";
            //----------------------------------------
            try
            {
                /* --------------------------------------------------------------------
                2.> REQUEST FOR TRACES PAN VALIDATION FILE( panverify.xhtml file )
                * URL :: https://www.tdscpc.gov.in/app/ded/panverify.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/panverify.xhtml");
                //-----------------------------------------------------------------------
                if (!IsStringExists(strResponse, "//form[@id=\"pandetailsForm1\"]"))
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------------------------------
                StringBuilder sbParameter = new StringBuilder();
                sbParameter.Append("pannumber=" + strPAN.Trim());
                sbParameter.Append("&frmType1=24Q");
                sbParameter.Append("&clickGo1=Go");
                sbParameter.Append("&pandetailsForm1_SUBMIT=1");
                /* -----------------------------------------------------------------------
                RETRIEVE VIEWSTATE DATA
                --------------------------------------------------------------------*/
                Dictionary<string, string> objNameval = TraceViewStateData(strResponse, "//form[@id=\"pandetailsForm1\"]");

                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if ("javax.faces.ViewState" == pair.Key)
                    {
                        sbParameter.Append("&" + pair.Key + "=" + HttpUtility.UrlEncode(pair.Value));
                        break;
                    }
                }
                /* --------------------------------------------------------------------
                3.> REQUEST FOR NSDL CONSO FILE( nsdlconsofile.xhtml file) WITH
                * FINNANCIAL YEAR/QUARTER/FORM SELECTION WITH PARAMETER PREPARATION
                * URL :: https://www.tdscpc.gov.in/app/ded/nsdlconsofile.xhtml
                --------------------------------------------------------------------*/
                strResponse = makeHTTPPostRequest(strBaseURL + "ded/panverify.xhtml", sbParameter);
                //-----------------------------------------------------------------------
                if (objResponse.Respons == enmResponse.Failed)
                    return objResponse;

                strName = this.RetrievePANName(strResponse);                
                //------------------------------------------------------------------------
                //objNameval = this.RetievePANStatus(strResponse);
                //------------------------------------------------------------------------
                
                //PANDetails objPan = new PANDetails();

                //objPan.PAN = strPAN;
                //foreach (KeyValuePair<string, string> pair in objNameval)
                //{
                //    if (pair.Key.ToString().ToUpper() == "STATUS")
                //        strStatus= pair.Value;
                //        //objPan.Status = pair.Value;

                //    if (pair.Key.ToString().ToUpper() == "NAME")
                //        strName=pair.Value;
                //        //objPan.Name = pair.Value;

                //}
                //----------------------------------------------------
                //objResponse.CustomeTypes = objPan;
                objResponse.Respons = enmResponse.Success;
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

        #region CIN_Period_Payment
        public TracesResponse CIN_Period_Payment(TracesData objData)
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
                // MAKE JSON REQUEST requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                //-- ANIK 2013-07-13
                //strServerResponse = makeHTTPPostRequest(strBaseURL + "ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=0&sdate=" + objData.FromChallanDepositDate + "&edate=" + objData.ToChallanDepositDate + "&cstatus=" + objData.ChallanStatus, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Bank Code");
                    table.Columns.Add("Branch Code");
                    table.Columns.Add("Date of Deposit");
                    table.Columns.Add("Challan Serial Number");
                    table.Columns.Add("Challan Status");
                    table.Columns.Add("Recipt Number");
                    table.Columns.Add("Chllan Amount");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    double dblAmt = 0;
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
                                        case "bankCode":
                                            dRow["Bank Code"] = reader.Value.ToString();
                                            break;
                                        case "branchCode":
                                            dRow["Branch Code"] = reader.Value.ToString();
                                            break;

                                        case "chlnSNo":
                                            dRow["Challan Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;
                                        case "recptNum":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            table.Rows.Add(dRow);
                                            break;

                                        case "chlnAmt":
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                            {
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                                dRow["chllan Amount"] = String.Format("{0:0.00}", dblAmt);
                                            }
                                            else
                                            {
                                                dRow["chllan Amount"] = "";
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
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }

                objResponse.Message = err.Message;
            }
            return objResponse;

        }

        #endregion

        #region CIN_CIN_BINParticulars
        public TracesResponse CIN_CIN_BINParticulars(TracesData objData)
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

                // MAKE JSON REQUEST requestParam ='reqtype=0&sdate=' +sdate+ '&edate='+edate+'&cstatus='+cstatus
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=1&bsrCode=" + objData.BSRCode + "&chlnSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Bank Code");
                    table.Columns.Add("Branch Code");
                    table.Columns.Add("Date of Deposit");
                    table.Columns.Add("Challan Serial Number");
                    table.Columns.Add("Challan Status");
                    table.Columns.Add("Recipt Number");
                    table.Columns.Add("Chllan Amount");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    double dblAmt = 0;
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
                                        case "bankCode":
                                            dRow["Bank Code"] = reader.Value.ToString();
                                            break;
                                        case "branchCode":
                                            dRow["Branch Code"] = reader.Value.ToString();
                                            break;

                                        case "chlnSNo":
                                            dRow["Challan Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Challan Status"] = reader.Value.ToString();
                                            break;
                                        case "recptNum":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            table.Rows.Add(dRow);
                                            break;

                                        case "chlnAmt":
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                            {
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                                dRow["chllan Amount"] = String.Format("{0:0.00}", dblAmt);
                                            }
                                            else
                                            {
                                                dRow["chllan Amount"] = "";
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
                    objResponse.Message = "No data available for the specified search criteria";
                }
                //-----------------------------------------------
                objResponse.CustomeTypes = table;

            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }

                objResponse.Message = err.Message;
            }

            return objResponse;

        }


        #endregion



        #region BIN_Period_Payment
        public TracesResponse BIN_Period_Payment(TracesData objData)
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
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=6&tvsdate=" + objData.FromChallanDepositDate + "&tvedate=" + objData.ToChallanDepositDate + "&tvstatus=" + objData.ChallanStatus, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Transfer Voucher Date");
                    table.Columns.Add("DDO Serial Number");
                    table.Columns.Add("Status");

                    table.Columns.Add("Chllan Amount");
                    table.Columns.Add("Recipt Number");
                    table.Columns.Add("Record ID");
                    string strLastToken = "Test";
                    DataRow dRow = null;
                    double dblAmt = 0;
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
                                            dRow["Transfer Voucher Date"] = reader.Value.ToString();
                                            break;
                                        case "ddoSlNum":
                                            dRow["DDO Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Status"] = reader.Value.ToString();
                                            break;

                                        case "chlnAmt":
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                            {
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                                dRow["chllan Amount"] = String.Format("{0:0.00}", dblAmt);
                                            }
                                            else
                                            {
                                                dRow["chllan Amount"] = "";
                                            }

                                            break;
                                        case "rcptNo":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            break;
                                        case "recordId":
                                            dRow["Record ID"] = reader.Value.ToString();
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
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }

                objResponse.Message = err.Message;
            }
            return objResponse;

        }

        #endregion



        #region BIN_Particulars
        public TracesResponse BIN_Particulars(TracesData objData)
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
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=4&rcptNo=" + objData.BSRCode + "&ddoSNo=" + objData.ChallanSerialNo + "&chlnAmt=" + objData.ChallanAmount + "&dateOfDep=" + objData.TaxDepositedDate, objBuilder);
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    table = new DataTable();
                    table.Columns.Add("Transfer Voucher Date");
                    table.Columns.Add("DDO Serial Number");
                    table.Columns.Add("Status");

                    table.Columns.Add("Transfer Voucher Amount");
                    table.Columns.Add("Recipt Number");
                    table.Columns.Add("Record ID");
                    string strLastToken = "Test";
                    DataRow dRow = null;
                    double dblAmt = 0;
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
                                            dRow["Transfer Voucher Date"] = reader.Value.ToString();
                                            break;
                                        case "ddoSlNum":
                                            dRow["DDO Serial Number"] = reader.Value.ToString();
                                            break;
                                        case "chlnStatus":
                                            dRow["Status"] = reader.Value.ToString();
                                            break;

                                        case "chlnAmt":
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                            dRow["Transfer Voucher Amount"] = String.Format("{0:0.00}", dblAmt);


                                            break;
                                        case "rcptNo":
                                            dRow["Recipt Number"] = reader.Value.ToString();
                                            break;
                                        case "recordId":
                                            dRow["Record ID"] = reader.Value.ToString();
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
                    objResponse.Message = "No data available for the specified search criteria";
                }
                //-----------------------------------------------
                objResponse.CustomeTypes = table;
            }
            catch (Exception err)
            {
                if (Regex.IsMatch(err.Message, "410"))
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }

                objResponse.Message = err.Message;
            }
            return objResponse;

        }

        #endregion


        

        #region ChallanStatusQuery
        public enmChallanStatus ChallanStatusQuery(ChallanQuery objData)
        {
            try
            {
                string strResponse = "";
                string strPageName = "";
                //STEP 1. TO PASSING PARAMETER TO OPEN SEARCH PAGE
                string strReturnVal = "Record not found";
                enmChallanStatus Status = enmChallanStatus.PROCESSING_FAILED;

                StringBuilder sbParameter = new StringBuilder();
                sbParameter.Append("firstTime=yes");
                sbParameter.Append("&submit=TAN Based View");

                strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
                strPageName = HTMLTagAttributeValue(strServerResponse, "//form[@name=\"TanSearch\"]", "action");

                if (string.IsNullOrEmpty(strPageName))
                {
                    return Status;
                }
                //FETCHING HIDDEN FIELD VALUES FOR NEXT SUBMISSION TO SERVER 
                //------------------------------------------------------------------------------------------------------------------
                sbParameter = new StringBuilder();
                sbParameter.Append("TAN_NO=" + objData.TAN);
                sbParameter.Append("&TAN_FROM_DT_DD=" + objData.FromDate.Substring(0, 2));
                sbParameter.Append("&TAN_FROM_DT_MM=" + objData.FromDate.Substring(3, 2));
                sbParameter.Append("&TAN_FROM_DT_YY=" + objData.FromDate.Substring(6, 4));

                sbParameter.Append("&TAN_TO_DT_DD=" + objData.ToDate.Substring(0, 2));
                sbParameter.Append("&TAN_TO_DT_MM=" + objData.ToDate.Substring(3, 2));
                sbParameter.Append("&TAN_TO_DT_YY=" + objData.ToDate.Substring(6, 4));

                sbParameter.Append("&HIDDEN_TAN_FROM_DT_DD=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_FROM_DT_DD\"]", "value"));
                sbParameter.Append("&HIDDEN_TAN_FROM_DT_MM=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_FROM_DT_MM\"]", "value"));
                sbParameter.Append("&HIDDEN_TAN_TO_DT_DD=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_DD\"]", "value"));
                sbParameter.Append("&HIDDEN_TAN_TO_DT_MM=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_MM\"]", "value"));
                sbParameter.Append("&HIDDEN_TAN_TO_DT_YY=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_YY\"]", "value"));

                sbParameter.Append("&submit=" + HTMLTagAttributeValue(strServerResponse, "//input[@id=\"VIEWDETAILS\"]", "value"));
                sbParameter.Append("&appUser=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"appUser\"]", "value"));
                sbParameter.Append("&appUser=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"appUser\"]", "value"));
                //------------------------------------------------------------------------------------------------------------------
                //STEP 2. PASSING DATE RANGE & TAN WITH OTHER HIDDEN FIELD VALUES 

                strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
                //----------------------------------------------------------------------------------------------------
                if (Regex.IsMatch(strResponse, "Record Not Found", RegexOptions.IgnoreCase))
                {
                    Status = enmChallanStatus.RECORD_NOT_FOUND;
                    return Status;
                }
                //----------------------------------------------------------------------------------------------------
                Dictionary<string, string> objNameval = TraceallInputFields(strResponse, "//form[@name=\"TanSearch\"]");

                sbParameter = new StringBuilder();

                string strCurrVal = "";
                int intOut;
                string strChallanDate = objData.ChallanDate.Substring(0, 2) + objData.ChallanDate.Substring(3, 2) + objData.ChallanDate.Substring(8, 2);
                bool bnlStatus = false;
                //RETRIEVE ALL HIDDEN FIELD FOR NEXT SUBMIT TO SERVER
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    strCurrVal = pair.Key.Substring(pair.Key.LastIndexOf("_") + 1);
                    if (int.TryParse(strCurrVal, out intOut))
                    {
                        // if (strLastVal != strCurrVal)
                        if (strChallanDate == pair.Value)
                        {
                            bnlStatus = true;
                        }

                        if (pair.Key != "RS_CHK_" + strCurrVal)
                            sbParameter.Append(pair.Key + "=" + pair.Value + "&");

                        if (pair.Key == "RS_CHALLAN_SQ_NO_" + strCurrVal)
                        {
                            if (Convert.ToInt32(objData.ChallanNo) == Convert.ToInt32(pair.Value) && bnlStatus)
                            {
                                sbParameter.Append("RS_CHK_" + strCurrVal + "=1&");
                                sbParameter.Append("RS_AMT_" + strCurrVal + "=" + objData.ChallanAmount + "&");

                                bnlStatus = false;
                            }
                        }
                    }
                    else
                    {
                        sbParameter.Append(pair.Key + "=" + pair.Value + "&");
                    }

                }
                sbParameter.Append("submit=Confirm Amount");
                strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);

                if (Regex.IsMatch(strResponse, "Amount Matched",RegexOptions.IgnoreCase))
                {
                    Status = enmChallanStatus.AMOUNT_MATCHED;                    
                }
                else if (Regex.IsMatch(strResponse, "Amount Not Matched"))
                {
                    Status = enmChallanStatus.AMOUNT_NOT_MATCHED;                    
                }
                else
                {
                    Status = enmChallanStatus.RECORD_NOT_FOUND;    
                }




                //STEP 3. FETCHING FINAL RECORDS 
              /*  HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(strResponse);

                // Get all tables in the document
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");
                // Iterate all columns in this row
                HtmlNodeCollection cols = rows[5].SelectNodes(".//td");
                strReturnVal = cols[0].InnerText;
                //
                if (strReturnVal.Trim() == "")
                    strReturnVal = "Record not found";
                //
                //return strReturnVal; */

                return Status;
            }
            catch (Exception err)
            {
                throw err;
            }



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



        #region RequestForBIN_Details
        public TracesResponse RequestForBIN_Details(List<string> strVal)
        {
            StringBuilder objBuilder = new StringBuilder();
            DataTable table = new DataTable();
            //MAKE GET REQUEST
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;


            //MAKE GET REQUEST
            try
            {
                //------------------------------------------------
                objBuilder.Append("_search=false");
                objBuilder.Append("&rows=2000");
                objBuilder.Append("&page=1");
                objBuilder.Append("&sidx=tokenNum");
                objBuilder.Append("&sord=desc");
                //------------------------------------------------
                strServerResponse = makeHTTPPostRequest(strBaseURL + "ded/srv/ChlnStatusServlet?reqtype=5&recordId=" + strVal[0] + "&rcptNo=" + strVal[1] + "&ddoSNo=" + strVal[2] + "&chlnAmt=" + strVal[3] + "&dateOfDep=" + strVal[4], objBuilder);

                // CHECKING SERVER TIMEOUT
                if (IsStringExists(strServerResponse, "//form[@id=\"loginForm\"]"))
                {
                    objResponse.Message = "Session Timeout";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
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
                    table.Columns.Add("Status");
                    table.Columns.Add("Excess Amount Claimed");
                    table.Columns.Add("Available Amount");

                    string strLastToken = "Test";
                    DataRow dRow = null;
                    double dblAmt = 0;
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
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                            dRow["Claimed Amount"] = String.Format("{0:0.00}", dblAmt);

                                            break;

                                        case "chlnStatus":
                                            dRow["Status"] = reader.Value.ToString();
                                            break;

                                        case "excessAmt":
                                            if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                dblAmt = Convert.ToDouble(reader.Value.ToString());
                                            dRow["Excess Amount Claimed"] = String.Format("{0:0.00}", dblAmt);
                                            break;

                                        case "availAmt":

                                            if (intRowCount > 0)
                                            {
                                                if (!string.IsNullOrEmpty(reader.Value.ToString()))
                                                    dblAmt = Convert.ToDouble(reader.Value.ToString());

                                                dRow["Available Amount"] = String.Format("{0:0.00}", dblAmt);
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
                {
                    objResponse.Respons = enmResponse.SessionTimeout;
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "406"))
                {
                    objResponse.Message = "Invalid Challan Amount";
                    objResponse.Respons = enmResponse.Failed;
                }
                else if (Regex.IsMatch(err.Message, "500"))
                {
                    objResponse.Message = "System has encountered some technical problem. Please try after some time";
                    objResponse.Respons = enmResponse.Failed;
                }
            }

            return objResponse;

        }


        #endregion


        #region RequestForDefaultSummary
        public TracesResponse RequestForDefaultSummary(TracesLogin objLogin)
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
                  2.> REQUEST FOR DEFAULT SUMMARY( viewdemandsum.xhtml )
                      URL :: https://www.tdscpc.gov.in/app/ded/viewdemandsum.xhtml
                   --------------------------------------------------------------------*/
                strResponse = makeHTTPGetRequest(strBaseURL + "ded/viewdemandsum.xhtml");

                if (!IsStringExists(strResponse, "//form[@id=\"viewdemandsumForm\"]"))
                {

                }

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                HtmlNode.ElementsFlags.Remove("option");
                doc.LoadHtml(strResponse);

                SortedDictionary<string, string> htFaYearValText = new SortedDictionary<string, string>();
                SortedDictionary<string, string> htQtrValText = new SortedDictionary<string, string>();

                //Hashtable htFaYearValText = new Hashtable();
                //Hashtable htQtrValText = new Hashtable();

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//select[@id='financialYear']//option"))
                {
                    htFaYearValText.Add(node.Attributes["value"].Value, node.InnerText);
                    //Console.WriteLine("InnerText=" + node.InnerText);
                    //Console.WriteLine();
                }

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//select[@id='quarter']//option"))
                {
                    htQtrValText.Add(node.Attributes["value"].Value, node.InnerText);
                    //Console.WriteLine("InnerText=" + node.InnerText);
                    //Console.WriteLine();
                }
                //--------------------------------------------------------------------
                DataTable table = new DataTable();
                //------------------------------------------------------
                table.Columns.Add("FA Year Code");
                table.Columns.Add("Quarter Code");
                table.Columns.Add("FA Year");
                table.Columns.Add("Quarter");
                table.Columns.Add("Form Type");
                table.Columns.Add("Net Payable(Rounded-Off)");
                //--------------------------------------------------------------------
                //
                foreach (KeyValuePair<string, string> kvp in htFaYearValText)
                {
                    //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    if (string.IsNullOrEmpty(kvp.Key)) continue;
                    foreach (KeyValuePair<string, string> qtrText in htQtrValText)
                    {
                        if (string.IsNullOrEmpty(qtrText.Key)) continue;
                        sbParameter = new StringBuilder();
                        sbParameter.Append("finyear=" + kvp.Key);
                        sbParameter.Append("&quarter=" + qtrText.Key);
                        sbParameter.Append("&finYr=" + kvp.Value);
                        //--------------------------------------------------------------------*/
                        strResponse = makeHTTPPostRequest(strBaseURL + "ded/demandsum.xhtml", sbParameter);

                        getDefaultSummaryDetails(strResponse, kvp.Key, qtrText.Key, kvp.Value, qtrText.Value, ref table);



                    }

                }
                //----------------------------------------
                objResponse.Respons = enmResponse.Success;
                objResponse.CustomeTypes = table;
                //----------------------------------------


                // Logoff();
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

        #region getDefaultSummaryDetails
        private void getDefaultSummaryDetails(string strResp, string strFACode, string QtrCode, string strFAYear, string Qtr, ref DataTable dTable)
        {
            //https://www.tdscpc.gov.in/app/ded/demandsum.xhtml 
            DataRow dRow = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(strResp);

            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
            if (tables != null)
            {
                HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");
                for (int i = 1; i < rows.Count; ++i)
                {
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                    if (cols != null)
                    {
                        dRow = dTable.NewRow();
                        dRow["FA Year Code"] = strFACode;
                        dRow["Quarter Code"] = QtrCode;
                        dRow["FA Year"] = strFAYear;
                        dRow["Quarter"] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Form Type"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                        dRow["Net Payable(Rounded-Off)"] = Convert.ToString(cols[2].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");

                        dTable.Rows.Add(dRow);
                    }


                }

            }
            //else
            //{
            //    dRow = dTable.NewRow();
            //    dRow["FA Year Code"] = strFACode;
            //    dRow["Quarter Code"] = QtrCode;
            //    dRow["FA Year"] = strFAYear;
            //    dRow["Quarter"] = Qtr;
            //    dRow["Form Type"] = "No data available";
            //    dRow["Net Payable(Rounded-Off)"] = "No data available";

            //    dTable.Rows.Add(dRow);
            //}

        }

        #endregion

        #region RequestDefaultSummaryDetails
        public TracesResponse RequestDefaultSummaryDetails(ArrayList objData)
        {
            DataTable dTable = new DataTable();
            DataTable dTableStatement = new DataTable();
            DataTable dTableCorrStatement = new DataTable();
            DataTable dTableErrorPan = new DataTable();
            DataRow dRow;
            //MAKE GET REQUEST
            TracesResponse objResponse = new TracesResponse();
            objResponse.Respons = enmResponse.Success;

            //MAKE GET REQUEST
            try
            {
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/viewdemandsum.xhtml");
                //----------------------------------------------------------------------------------
                //CHECKING ANY ERROR FROM SERVER POINT
                if (!IsStringExists(strServerResponse, "//form[@id=\"viewdemandsumForm\"]"))
                {
                    objResponse.Message = "Session Timeout";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //------------------------------------------------
                strServerResponse = makeHTTPGetRequest(strBaseURL + "ded/demsummary.xhtml?fy=" + Convert.ToString(objData[0]) + "&quarterdesc=" + Convert.ToString(objData[3]) + "&qr=" + Convert.ToString(objData[1]) + "&ft=" + Convert.ToString(objData[4]) + "&finyrdesc=" + Convert.ToString(objData[2]));
                //------------------------------------------------
                //PROCESSING JSON DATA & PUT INTO DATATABLE
                //------------------------------------------------
                if (!IsStringExists(strServerResponse, "//form[@id=\"demandsumForm\"]"))
                {
                    objResponse.Message = "Session Timeout";
                    objResponse.Respons = enmResponse.SessionTimeout;
                    return objResponse;
                }
                //-------------------------------------------------
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(strServerResponse);


                //1. STATEMENT DETAILS
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table[@class=\"userList w550\"]");
                HtmlNodeCollection rowHead;
                HtmlNodeCollection cols;

                if (tables != null)
                {
                    rowHead = tables[0].SelectNodes(".//tr");



                    dTableStatement.Columns.Add("Statement");
                    dTableStatement.Columns.Add("Token Number");
                    dTableStatement.Columns.Add("Order Passed Date");

                    for (int i = 0; i < rowHead.Count; ++i)
                    {
                        cols = rowHead[i].SelectNodes(".//td");

                        if (cols != null)
                        {
                            dRow = dTableStatement.NewRow();
                            dRow["Statement"] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Token Number"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            dRow["Order Passed Date"] = Convert.ToString(cols[2].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            dTableStatement.Rows.Add(dRow);
                        }
                    }

                    //---------------------------------------
                    dTableCorrStatement.Columns.Add("Count of Correction Statement(s)");
                    dTableCorrStatement.Columns.Add("Net Payable (Rounded-Off)(Rs.)");

                    rowHead = tables[1].SelectNodes(".//tr");
                    for (int i = 0; i < rowHead.Count; ++i)
                    {
                        cols = rowHead[i].SelectNodes(".//td");

                        if (cols != null)
                        {
                            dRow = dTableCorrStatement.NewRow();
                            dRow["Count of Correction Statement(s)"] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Net Payable (Rounded-Off)(Rs.)"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            dTableCorrStatement.Rows.Add(dRow);
                        }
                    }
                }

                //2. SUMMARY OF PAN ERRORS
                //HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table[@class=\"userList w750\"]");
                //if (tables != null)
                //{

                //}
                //3. SUMMARY OF PAN ERRORS
                tables = doc.DocumentNode.SelectNodes("//table[@class=\"userList w750\"]");
                if (tables != null)
                {
                    dTable.Columns.Add("Sr.No.");
                    dTable.Columns.Add("Type of Default");
                    dTable.Columns.Add("Default Amount");
                    dTable.Columns.Add("Amount Reported As 'Interest / Others' Claimed in the Statement(Rs.)");
                    dTable.Columns.Add("Payable(Rs.)");


                    rowHead = tables[0].SelectNodes(".//tr");

                    for (int i = 1; i < rowHead.Count; ++i)
                    {
                        //1.GETTING TABLE HEADER
                        //HtmlNodeCollection cols = rowHead[i].SelectNodes(".//th");
                        //if (cols != null)
                        //{
                        //    for (int k = 1; k < cols.Count; ++i)
                        //    {
                        //        dTable.Columns.Add(Convert.ToString(cols[k].InnerText));
                        //    }
                        //}

                        //------------------------------------------
                        //2.GETTING ROW DETAILS
                        cols = rowHead[i].SelectNodes(".//td");

                        if (cols != null)
                        {
                            //for (int j = 0; j < cols.Count; ++j)
                            //{
                            //    dRow = dTable.NewRow();
                            //    dRow[j] = Convert.ToString(cols[j].InnerText); //.Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            //    dTable.Rows.Add(dRow);
                            //}
                            dRow = dTable.NewRow();
                            dRow["Sr.No."] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Type of Default"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Default Amount"] = Convert.ToString(cols[2].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Amount Reported As 'Interest / Others' Claimed in the Statement(Rs.)"] = Convert.ToString(cols[3].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");
                            dRow["Payable(Rs.)"] = Convert.ToString(cols[4].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", "");

                            dTable.Rows.Add(dRow);
                        }

                    }

                }

                //4. SUMMARY OF PAN ERRORS
                tables = doc.DocumentNode.SelectNodes("//table[@class=\"userList marginTop10 w398\"]");
                if (tables != null)
                {
                    rowHead = tables[0].SelectNodes(".//tr");

                    dTableErrorPan.Columns.Add("Deductees Without PAN");
                    dTableErrorPan.Columns.Add("Deductees With Invalid PAN");

                    for (int i = 1; i < rowHead.Count; ++i)
                    {
                        cols = rowHead[i].SelectNodes(".//td");


                        if (cols != null)
                        {
                            dRow = dTableErrorPan.NewRow();
                            dRow["Deductees Without PAN"] = Convert.ToString(cols[0].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;
                            dRow["Deductees With Invalid PAN"] = Convert.ToString(cols[1].InnerText).Replace("\t", "").Replace("\t", "").Replace("\n", ""); ;

                            dTableErrorPan.Rows.Add(dRow);
                        }

                    }

                }

                DataSet dsRecords = new DataSet();

                dsRecords.Tables.Add(dTableStatement);
                dsRecords.Tables.Add(dTableCorrStatement);
                dsRecords.Tables.Add(dTable);
                dsRecords.Tables.Add(dTableErrorPan);

                objResponse.Respons = enmResponse.Success;
                objResponse.CustomeTypes = dsRecords;



                if (!string.IsNullOrEmpty(strServerResponse))
                {
                    //table = new DataTable();
                    //table.Columns.Add("Bank Code");
                    //table.Columns.Add("Branch Code");
                    //table.Columns.Add("Date of Deposit");
                    //table.Columns.Add("Challan Serial Number");
                    //table.Columns.Add("Challan Status");

                    //table.Columns.Add("chllan Amount");
                    //table.Columns.Add("Recipt Number");

                    //string strLastToken = "Test";
                    //DataRow dRow = null;
                    // DataColumn column1
                    //--------------------------------------------------------------------------




                }
                else
                {
                    objResponse.Respons = enmResponse.Failed;
                    objResponse.Message = "Server error";
                }
                //-----------------------------------------------
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

        #region AttributeValie
        private string AttributeValie(string strHTML, string strXPath, string strAttrib)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(strHTML);
            HtmlNode node = document.DocumentNode.SelectSingleNode(strXPath);

            return node.Attributes[strAttrib].Value;
        }

        #endregion

        #region GetCaptchaforBulkPAN
        public Stream GetCaptchaforBulkPAN()
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create("https://incometaxindiaefiling.gov.in/e-Filing/CreateCaptcha.do");
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36";
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



        #region getTINNSDLCaptcha
        public Stream getTINNSDLCaptcha()
        {
            Stream strCaptcha = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create("https://tin.tin.nsdl.com/oltas/servlet/CaptchaServicetansearch");
                request.Method = "GET";
                request.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20100101 Firefox/15.0";
                request.ContentType = "text/html; charset=utf-8";
                request.KeepAlive = true;
                request.Timeout = 1000000000;
                request.CookieContainer = objContainer;

                //if (response.Cookies != null && response.Cookies.Count > 0)            
                //    objContainer.Add(response.Cookies);            

                strCaptcha = request.GetResponse().GetResponseStream();

            }
            catch (Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
            }
            return strCaptcha;
        }
        #endregion

        #region ChallanStatusQueryInBulk
        ////public List<ChallanQuery> ChallanStatusQueryInBulk(List<ChallanQuery> objData, string strCaptchaCode)
        ////{
        ////    try
        ////    {
        ////        string strResponse = "";

        ////        //STEP 1. TO PASSING PARAMETER TO OPEN SEARCH PAGE
        ////        enmChallanStatus Status = enmChallanStatus.PROCESSING_FAILED;

        ////        StringBuilder sbParameter = new StringBuilder();

        ////        //FETCHING HIDDEN FIELD VALUES FOR NEXT SUBMISSION TO SERVER 
        ////        //------------------------------------------------------------------------------------------------------------------
        ////        //sbParameter = new StringBuilder();

        ////        sbParameter.Append("TAN_NO=" + objData[0].TAN.ToString());

        ////        sbParameter.Append("&TAN_FROM_DT_DD=" + objData[0].FromDate.Substring(0, 2));
        ////        sbParameter.Append("&TAN_FROM_DT_MM=" + objData[0].FromDate.Substring(3, 2));
        ////        sbParameter.Append("&TAN_FROM_DT_YY=" + objData[0].FromDate.Substring(6, 4));

        ////        sbParameter.Append("&TAN_TO_DT_DD=" + objData[objData.Count - 1].FromDate.Substring(0, 2));
        ////        sbParameter.Append("&TAN_TO_DT_MM=" + objData[objData.Count - 1].FromDate.Substring(3, 2));
        ////        sbParameter.Append("&TAN_TO_DT_YY=" + objData[objData.Count - 1].FromDate.Substring(6, 4));

        ////        sbParameter.Append("&HID_IMG_TXT=" + strCaptchaCode);

        ////        sbParameter.Append("&HIDDEN_TAN_FROM_DT_DD=01");
        ////        sbParameter.Append("&HIDDEN_TAN_FROM_DT_MM=04");
        ////        sbParameter.Append("&HIDDEN_TAN_TO_DT_DD=31");
        ////        sbParameter.Append("&HIDDEN_TAN_TO_DT_MM=03");
        ////        sbParameter.Append("&HIDDEN_TAN_TO_DT_YY=");

        ////        sbParameter.Append("&submit=View Challan details");
        ////        sbParameter.Append("&appUser=T");
        ////        sbParameter.Append("&appUser=T");


        ////        //sbParameter.Append("&HIDDEN_TAN_FROM_DT_DD=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_FROM_DT_DD\"]", "value"));
        ////        //sbParameter.Append("&HIDDEN_TAN_FROM_DT_MM=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_FROM_DT_MM\"]", "value"));
        ////        //sbParameter.Append("&HIDDEN_TAN_TO_DT_DD=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_DD\"]", "value"));
        ////        //sbParameter.Append("&HIDDEN_TAN_TO_DT_MM=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_MM\"]", "value"));
        ////        //sbParameter.Append("&HIDDEN_TAN_TO_DT_YY=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"HIDDEN_TAN_TO_DT_YY\"]", "value"));

        ////        //sbParameter.Append("&submit=" + HTMLTagAttributeValue(strServerResponse, "//input[@id=\"VIEWDETAILS\"]", "value"));
        ////        //sbParameter.Append("&appUser=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"appUser\"]", "value"));
        ////        //sbParameter.Append("&appUser=" + HTMLTagAttributeValue(strServerResponse, "//input[@name=\"appUser\"]", "value"));
        ////        //------------------------------------------------------------------------------------------------------------------
        ////        //STEP 2. PASSING DATE RANGE & TAN WITH OTHER HIDDEN FIELD VALUES 

        ////        strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
        ////        //----------------------------------------------------------------------------------------------------
        ////        if (Regex.IsMatch(strResponse, "Record Not Found", RegexOptions.IgnoreCase))
        ////        {
        ////            Status = enmChallanStatus.RECORD_NOT_FOUND;
        ////            return null;
        ////        }
        ////        //----------------------------------------------------------------------------------------------------
        ////        Dictionary<string, string> objNameval = TraceallInputFields(strResponse, "//form[@name=\"TanSearch\"]");

        ////        // List<ChallanQuery> objChlQuery = new List<ChallanQuery>();
        ////        //----------------------------------------------------------------------------------------------------
        ////        sbParameter = new StringBuilder();
        ////        int intCount = 0;
        ////        bool bnlIsRecordExists = false;

        ////        //CHECKING IF RECORD EXISTS ON SELECTED DATE
        ////        if (objNameval.ContainsKey("NO_OF_ROWS"))
        ////        {
        ////            intCount = Convert.ToInt32(objNameval["NO_OF_ROWS"]);

        ////            for (int i = 1; i <= intCount; i++)
        ////            {
        ////                foreach (ChallanQuery dataVal in objData)
        ////                {
        ////                    string strChallanDate = dataVal.ChallanDate.Substring(0, 2) + dataVal.ChallanDate.Substring(3, 2) + dataVal.ChallanDate.Substring(8, 2);
        ////                    if (Convert.ToString(objNameval["RS_CHALLAN_DATE_" + i]) == strChallanDate &&
        ////                        Convert.ToString(objNameval["RS_BSR_CD_" + i]) == dataVal.BSRCode &&
        ////                        Convert.ToString(objNameval["RS_CHALLAN_SQ_NO_" + i]) == dataVal.ChallanNo)
        ////                    {
        ////                        bnlIsRecordExists = true;
        ////                        objNameval["RS_AMT_" + i] = dataVal.ChallanAmount;
        ////                        dataVal.AmountID = "RS_AMT_" + i;

        ////                    }
        ////                }

        ////            }
        ////        }

        ////        //------------------------------------------------------------------------------------------------
        ////        if (!bnlIsRecordExists)
        ////        {
        ////            // Status = enmChallanStatus.RECORD_NOT_FOUND;
        ////            return null;
        ////        }
        ////        //ADD PARAMETER TO NEXT REQUEST
        ////        foreach (KeyValuePair<string, string> pair in objNameval)
        ////            sbParameter.Append(pair.Key + "=" + pair.Value + "&");
        ////        //------------------------------------------------------------------------------------------------
        ////        sbParameter.Append("submit=Confirm Amount");
        ////        strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
        ////        //-------------------------------------------------------------------------------------------------
        ////        //ADD STATUS OF RETURING OBJECT


        ////        foreach (ChallanQuery dataVal in objData)
        ////        {
        ////            if (dataVal.AmountID != "")
        ////            {
        ////                Status = getInnerText(strResponse, dataVal.AmountID);

        ////                if (Status == enmChallanStatus.AMOUNT_MATCHED)
        ////                    dataVal.Message = enmChallanStatus.AMOUNT_MATCHED;
        ////                else if (Status == enmChallanStatus.AMOUNT_NOT_MATCHED)
        ////                    dataVal.Message = enmChallanStatus.AMOUNT_NOT_MATCHED;
        ////                else
        ////                    dataVal.Message = enmChallanStatus.RECORD_NOT_FOUND;
        ////            }


        ////        }


        ////        //if (Regex.IsMatch(strResponse, "Amount Matched", RegexOptions.IgnoreCase))
        ////        //{
        ////        //    Status = enmChallanStatus.AMOUNT_MATCHED;
        ////        //}
        ////        //else if (Regex.IsMatch(strResponse, "Amount Not Matched"))
        ////        //{
        ////        //    Status = enmChallanStatus.AMOUNT_NOT_MATCHED;
        ////        //}
        ////        //else
        ////        //{
        ////        //    Status = enmChallanStatus.RECORD_NOT_FOUND;
        ////        //}




        ////        //STEP 3. FETCHING FINAL RECORDS 
        ////        /*  HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        ////          doc.LoadHtml(strResponse);

        ////          // Get all tables in the document
        ////          HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
        ////          HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");
        ////          // Iterate all columns in this row
        ////          HtmlNodeCollection cols = rows[5].SelectNodes(".//td");
        ////          strReturnVal = cols[0].InnerText;
        ////          //
        ////          if (strReturnVal.Trim() == "")
        ////              strReturnVal = "Record not found";
        ////          //
        ////          //return strReturnVal; */

        ////        return objData;
        ////    }
        ////    catch (Exception err)
        ////    {
        ////        throw err;
        ////    }



        ////}

        #endregion        
        
        #region ChallanStatusQueryInBulk
        //-- 2017/01/19
        public List<ChallanQuery> ChallanStatusQueryInBulk(List<ChallanQuery> objData, string strCaptchaCode)
        {
            try
            {
                string strResponse = "";

                //STEP 1. TO PASSING PARAMETER TO OPEN SEARCH PAGE
                enmChallanStatus Status = enmChallanStatus.PROCESSING_FAILED;

                StringBuilder sbParameter = new StringBuilder();

                //FETCHING HIDDEN FIELD VALUES FOR NEXT SUBMISSION TO SERVER 
                //------------------------------------------------------------------------------------------------------------------
                //sbParameter = new StringBuilder();

                sbParameter.Append("TAN_NO=" + objData[0].TAN.ToString());

                sbParameter.Append("&TAN_FROM_DT_DD=" + objData[0].FromDate.Substring(0, 2));
                sbParameter.Append("&TAN_FROM_DT_MM=" + objData[0].FromDate.Substring(3, 2));
                sbParameter.Append("&TAN_FROM_DT_YY=" + objData[0].FromDate.Substring(6, 4));

                sbParameter.Append("&TAN_TO_DT_DD=" + objData[objData.Count - 1].ToDate.Substring(0, 2));
                sbParameter.Append("&TAN_TO_DT_MM=" + objData[objData.Count - 1].ToDate.Substring(3, 2));
                sbParameter.Append("&TAN_TO_DT_YY=" + objData[objData.Count - 1].ToDate.Substring(6, 4));

                sbParameter.Append("&HID_IMG_TXT=" + strCaptchaCode);

                sbParameter.Append("&HIDDEN_TAN_FROM_DT_DD=01");
                sbParameter.Append("&HIDDEN_TAN_FROM_DT_MM=04");
                sbParameter.Append("&HIDDEN_TAN_TO_DT_DD=31");
                sbParameter.Append("&HIDDEN_TAN_TO_DT_MM=03");
                sbParameter.Append("&HIDDEN_TAN_TO_DT_YY=");

                sbParameter.Append("&submit=View Challan details");
                sbParameter.Append("&appUser=T");
                sbParameter.Append("&appUser=T");

                //------------------------------------------------------------------------------------------------------------------
                //STEP 2. PASSING DATE RANGE & TAN WITH OTHER HIDDEN FIELD VALUES 

                strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
                //----------------------------------------------------------------------------------------------------
                //CAPTCHA CODE VALIDATION
                if (Regex.IsMatch(strResponse, "Error - Text does not match. Please enter new text.", RegexOptions.IgnoreCase))
                {
                    List<ChallanQuery> listErr = new List<ChallanQuery>();

                    ChallanQuery chlm = new ChallanQuery();
                    chlm.Message = enmChallanStatus.WRONG_CAPTCHA;

                    listErr.Add(chlm);

                    return listErr;
                }

                if (Regex.IsMatch(strResponse, "SITE UNDER MAINTENANCE", RegexOptions.IgnoreCase))
                {
                    List<ChallanQuery> listErr = new List<ChallanQuery>();

                    ChallanQuery chlm = new ChallanQuery();
                    chlm.Message = enmChallanStatus.SERVER_MAINTENANCE_ERROR;

                    listErr.Add(chlm);

                    return listErr;
                }

                //IF NO RECORDS FOUND
                if (Regex.IsMatch(strResponse, "Record Not Found", RegexOptions.IgnoreCase))
                {
                    List<ChallanQuery> listErr = new List<ChallanQuery>();

                    ChallanQuery chlm = new ChallanQuery();
                    chlm.Message = enmChallanStatus.RECORD_NOT_FOUND;

                    listErr.Add(chlm);
                    return null;
                }



                //----------------------------------------------------------------------------------------------------
                Dictionary<string, string> objNameval = TraceallInputFields(strResponse, "//form[@name=\"TanSearch\"]");

                // List<ChallanQuery> objChlQuery = new List<ChallanQuery>();
                //----------------------------------------------------------------------------------------------------
                sbParameter = new StringBuilder();
                int intCount = 0;
                bool bnlIsRecordExists = false;

                //CHECKING IF RECORD EXISTS ON SELECTED DATE
                if (objNameval.ContainsKey("NO_OF_ROWS"))
                {
                    intCount = Convert.ToInt32(objNameval["NO_OF_ROWS"]);

                    for (int i = 1; i <= intCount; i++)
                    {
                        foreach (ChallanQuery dataVal in objData)
                        {
                            string strChallanDate = dataVal.ChallanDate.Substring(0, 2) + dataVal.ChallanDate.Substring(3, 2) + dataVal.ChallanDate.Substring(8, 2);
                            if (Convert.ToString(objNameval["RS_CHALLAN_DATE_" + i]) == strChallanDate &&
                                Convert.ToString(objNameval["RS_BSR_CD_" + i]) == dataVal.BSRCode &&
                                Convert.ToString(objNameval["RS_CHALLAN_SQ_NO_" + i]) == dataVal.ChallanNo)
                            {
                                bnlIsRecordExists = true;
                                objNameval["RS_AMT_" + i] = dataVal.ChallanAmount;
                                dataVal.AmountID = "RS_AMT_" + i;

                            }
                        }

                    }
                }

                //------------------------------------------------------------------------------------------------
                if (!bnlIsRecordExists)
                {
                    // Status = enmChallanStatus.RECORD_NOT_FOUND;
                    return null;
                }
                //ADD PARAMETER TO NEXT REQUEST
                foreach (KeyValuePair<string, string> pair in objNameval)
                    sbParameter.Append(pair.Key + "=" + pair.Value + "&");
                //------------------------------------------------------------------------------------------------
                sbParameter.Append("submit=Confirm Amount");
                strResponse = makeHTTPPostRequest("https://tin.tin.nsdl.com/oltas/servlet/TanSearch", sbParameter);
                //-------------------------------------------------------------------------------------------------
                //ADD STATUS OF RETURING OBJECT


                foreach (ChallanQuery dataVal in objData)
                {
                    if (dataVal.AmountID != "")
                    {
                        Status = getInnerText(strResponse, dataVal.AmountID);

                        if (Status == enmChallanStatus.AMOUNT_MATCHED)
                            dataVal.Message = enmChallanStatus.AMOUNT_MATCHED;
                        else if (Status == enmChallanStatus.AMOUNT_NOT_MATCHED)
                            dataVal.Message = enmChallanStatus.AMOUNT_NOT_MATCHED;
                        else
                            dataVal.Message = enmChallanStatus.RECORD_NOT_FOUND;
                    }


                }

                return objData;
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion


        #region getInnerText



        private enmChallanStatus getInnerText(string html, string strQuery)
        {
            enmChallanStatus Status = enmChallanStatus.RECORD_NOT_FOUND;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNodeCollection tdOfInterests = htmlDoc.DocumentNode.SelectNodes("//tr[td/input[@name=\"" + strQuery + "\"]]/following-sibling::tr[position() <= 1]/td");

            if (tdOfInterests == null)
                return enmChallanStatus.RECORD_NOT_FOUND;

            foreach (HtmlNode td in tdOfInterests)
            {
                if (td.InnerText.ToUpper().Trim() == "AMOUNT MATCHED")
                {
                    Status = enmChallanStatus.AMOUNT_MATCHED;

                }
                else if (td.InnerText.ToUpper().Trim() == "AMOUNT NOT MATCHED")
                    Status = enmChallanStatus.AMOUNT_NOT_MATCHED;

            }

            return Status;

        }


        #endregion

        #region GetCaptchaForPANNAme1
        public Stream GetCaptchaForPANNAme1()
        {
            makeHTTPGetRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");

            if (!IsStringExists(strServerResponse, "//form[@name=\"selectform\"]"))
                return null;
            //--------------------------------------------------------------------------------------------------------
            StringBuilder objPANData = new StringBuilder();
            objPANData.Append("browser_type=IE");
            objPANData.Append("&from_tdsnontds=Y");
            objPANData.Append("&R2=280");
            string strPageName = HTMLTagAttributeValue(strServerResponse, "//form[@name=\"selectform\"]", "action");
            strServerResponse = makeHTTPPostRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/" + strPageName, objPANData);



            request = (HttpWebRequest)WebRequest.Create("https://onlineservices.tin.egov-nsdl.com/etaxnew/Captcha1Servlet");
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

        
        #region GetCaptchaForPANNAme
        public Stream GetCaptchaForPANNAme()
        {
           //  makeHTTPGetRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
            SetAllowUnsafeHeaderParsing();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; }); 
         
           /// request = (HttpWebRequest)HttpWebRequest.Create("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
           //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls || SecurityProtocolType.Ssl3

           // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            request = (HttpWebRequest)HttpWebRequest.Create("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
            //-------------------------------------
            // request.CookieContainer = objContainer;
            request.KeepAlive = false;
            request.Method = "GET";

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            
           // request.Headers.Add("Accept-Encoding: gzip, deflate, sdch, br");
          //  request.Headers.Add("Accept-Language: en-GB,en-US;q=0.8,en;q=0.6");
            //request.Headers.Add("Host: onlineservices.tin.egov-nsdl.com");
         //   request.Headers.Add("Cache-Control: max-age=0");
         //   request.Headers.Add("Upgrade-Insecure-Requests", "1");
         //   request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
           // request.ContentType = "application/x-www-form-urlencoded";

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

            if (!IsStringExists(strServerResponse, "//form[@name=\"selectform\"]"))
                return null;
            //--------------------------------------------------------------------------------------------------------
            StringBuilder objPANData = new StringBuilder();
            objPANData.Append("browser_type=IE");
            objPANData.Append("&from_tdsnontds=Y");
            objPANData.Append("&R2=280");
            string strPageName = HTMLTagAttributeValue(strServerResponse, "//form[@name=\"selectform\"]", "action");
            strServerResponse = makeHTTPPostRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/" + strPageName, objPANData);



            request = (HttpWebRequest)WebRequest.Create("https://onlineservices.tin.egov-nsdl.com/etaxnew/Captcha1Servlet");
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


        #region ExtractPANName

        public bool ExtractPANName(string PAN_No, string Captcha, out string strName)
        {
            strName = "";
            string strFourthLetterPAN = "";
            bool bnlStatus = true;
            try
            {
                //-- ANIK - 2017/02/28
                if (PAN_No.Trim().Length == 10)
                {
                    strFourthLetterPAN = PAN_No.Substring(3, 1);
                }
                //--

                StringBuilder objPANData = new StringBuilder();

                objPANData = new StringBuilder();

                objPANData.Append("AssessYear=2016-17");
                objPANData.Append("&AssessYear_1=2016-17");
                objPANData.Append("&Add_Line1=");
                objPANData.Append("&Add_Line2=");
                objPANData.Append("&Add_Line3=");
                objPANData.Append("&Add_Line4=");
                objPANData.Append("&Add_Line5=city");
                objPANData.Append("&Add_State=KARNATAKA");
                objPANData.Append("&Add_State_1=KARNATAKA");
                objPANData.Append("&Add_PIN=111000");
                objPANData.Append("&Add_EMAIL=");
                objPANData.Append("&Add_MOBILE=");
                objPANData.Append("&PAN=" + PAN_No);
                objPANData.Append("&captchaText=" + Captcha);
                objPANData.Append("&MinorHead_1=300");
                objPANData.Append("&MinorHead=300");
                objPANData.Append("&BankName_c=State Bank of India|https://merchant.onlinesbi.com/merchant/merchantprelogin.htm?merchant_code=OLTAS");
                objPANData.Append("&Submit=Proceed");
                objPANData.Append("&errorMsg=");
                objPANData.Append("&actualAmt=");
                objPANData.Append("&browser_type=IE");
                objPANData.Append("&date=");
                objPANData.Append("&flag=280");
                objPANData.Append("&flag_var=null");
                objPANData.Append("&inputarray=");
                objPANData.Append("&inputrequest=Y");
                //--
                if (strFourthLetterPAN.ToUpper() == "C")
                {
                    objPANData.Append("&MajorHead=0020");
                    objPANData.Append("&MajorHead_1=0020");
                }
                else
                {
                    objPANData.Append("&MajorHead=0021");
                    objPANData.Append("&MajorHead_1=0021");
                }
                //--
                objPANData.Append("&PDF=online");
                objPANData.Append("&Submit=Proceed");

                strServerResponse = makeHTTPPostRequest("https://onlineservices.tin.egov-nsdl.com/etaxnew/SubmitTdsn", objPANData);

                //CHECKING IS VALID CAPTCHA OR NOT
                if (Regex.IsMatch(strServerResponse, "Text does not match. Please enter new text", RegexOptions.IgnoreCase))
                {
                    strName = "Invalid Captcha Code";
                    return false;
                }

                //CHECKING IS VALID PAN OR NOT
                if (Regex.IsMatch(strServerResponse, "ERROR -Invalid PAN", RegexOptions.IgnoreCase))
                {
                    strName = "Invalid PAN Number";
                    return false;
                }


                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(strServerResponse);
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//tr[td[1]='Full Name']/td[2]");
                if (node != null)
                {
                    strName = node.InnerText;
                    if (!string.IsNullOrEmpty(strName))
                        bnlStatus = true;
                    else
                        bnlStatus = false;
                }
                else
                    bnlStatus = false;


                //string strForType = HTMLTagAttributeValue(strServerResponse, "//input[@id=\"frmType0\"]", "value");

                //$Invalid PAN$

                //if (Regex.IsMatch(strServerResponse, "IERROR -Text does not match. Please enter new text", RegexOptions.IgnoreCase)) return false;
                //else
                //    return true;
                //}
                //else
                //    return false;
                //--

            }
            catch
            {
                strName = "Server Error::";
                bnlStatus = false;
            }

            return bnlStatus;
        }

        #endregion

    }


}
