using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace TDSMAN.Classes
{
    class NSDLData
    {      

    }

    #region NSDLAuthentication
    public class NSDLAuthentication
    {
        string strUserID = "";
        string strPassword = "";
        string StrTanNo = "";

        public string UserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }
        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public string TAN
        {
            get { return StrTanNo; }
            set { StrTanNo = value; }
        }

    }

    #endregion

    #region ConsolidateData
    public class ConsolidateData
    {
        string strPRN_No = "";
        string strQuarter = "";
        string strFinyr = "";
        string strFormNo = "";
        string strbsrCode = "";
        string strChalnSlNo = "";
        string strChalnDate = "";
        string strChalnAmt = "";
        string strkycPan1 = "";
        string strkycAmt1 = "";
        string strkycPan2 = "";
        string strkycAmt2 = "";
        string strkycPan3 = "";
        string strkycAmt3 = "";


        public string PRN_No
        {
            get { return strPRN_No; }
            set { strPRN_No = value; }
        }

        public string FormNo
        {
            get { return strFormNo; }
            set { strFormNo = value; }
        }

        public string Quarter
        {
            get { return strQuarter; }
            set { strQuarter = value; }
        }
        public string FinYear
        {
            get { return strFinyr; }
            set { strFinyr = value; }
        }
        public string BSRCode
        {
            get { return strbsrCode; }
            set { strbsrCode = value; }
        }
        public string ChallanSlNo
        {
            get { return strChalnSlNo; }
            set { strChalnSlNo = value; }
        }

        public string ChallanDate
        {
            get { return strChalnDate; }
            set { strChalnDate = value; }
        }

        public string ChallanAmount
        {
            get { return strChalnAmt; }
            set { strChalnAmt = value; }
        }

        public string KycPan1
        {
            get { return strkycPan1; }
            set { strkycPan1 = value; }
        }


        public string KycAmt1
        {
            get { return strkycAmt1; }
            set { strkycAmt1 = value; }
        }

        public string KycPan2
        {
            get { return strkycPan2; }
            set { strkycPan2 = value; }
        }

        public string KycAmt2
        {
            get { return strkycAmt2; }
            set { strkycAmt2 = value; }
        }

        public string KycPan3
        {
            get { return strkycPan3; }
            set { strkycPan3 = value; }
        }

        public string KycAmt3
        {
            get { return strkycAmt3; }
            set { strkycAmt3 = value; }
        }


    }


    #endregion

}
