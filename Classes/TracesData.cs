using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace TDSMAN.Classes
{
    #region Message
    public enum Message
    {
        Valid,
        Invalid
    }

    #endregion



    #region TRACESData
    public class TracesData
    {
        string strFAYear = "";
        string strQuarter = "";
        string strForms = "";
        string strAuthenCode = "";
        string strPRN_No = "";
        bool bnlNoChallan = false;
        bool bnlNoChallanCheck = false;
        bool bnlBookAdjust = false;
        bool bnlBookAdjustCheck = false;
        bool panAmtVal = false;
        bool panAmtValCheck = false;
        string strBSRCode = "", strChallanClaimedUncliamedStatus = "";
        string strDate = "";
        string strChallanNo = "";
        string strChallanAmount = "";
        string strCDRecordNo = "";
        bool bnlNoPanDeductee = false;

        string strTAN = "";

        string strPAN1 = "";
        string strPAN1Amount = "";

        string strPAN2 = "";
        string strPAN2Amount = "";

        string strPAN3 = "";
        string strPAN3Amount = "";

        string strFromChallanDepositDate = "";
        string strToChallanDepositDate = "";
        string strChallanStatus = "";
        bool bnlAddlConso = false;
        bool bnlAddljustificationRep = false;
        bool bnlAddlForm16A = false;
        bool bnlAddlForm16 = false;
        bool bnlAddlForm27D = false;

        string strDDOSerialNumber = "", strReciptNumber = "", strRecordID = "", strTransferVoucherDate = "";

        bool blMakeChallanCorrectionRequest = false;

        #region MakeChallanCorrectionRequest
        public bool MakeChallanCorrectionRequest
        {
            get
            {
                return blMakeChallanCorrectionRequest;
            }
            set
            {
                blMakeChallanCorrectionRequest = value;
            }
        }

        #endregion

        #region TAN
        public string TAN
        {
            get
            {
                return strTAN;
            }
            set
            {
                strTAN = value;
            }
        }

        #endregion

        #region FAYear
        public string FAYear
        {
            get
            {
                return strFAYear;
            }
            set
            {
                strFAYear = value;
            }
        }

        #endregion

        #region Quarter
        public string Quarter
        {
            get
            {
                return strQuarter;
            }
            set
            {
                strQuarter = value;
            }
        }

        #endregion

        #region Forms
        public string Forms
        {
            get
            {
                return strForms;
            }
            set
            {
                strForms = value;
            }
        }

        #endregion

        #region AuthenticationCode
        public string AuthenticationCode
        {
            get
            {
                return strAuthenCode;
            }
            set
            {
                strAuthenCode = value;
            }
        }

        #endregion

        #region PRN_NO
        public string PRN_NO
        {
            get
            {
                return strPRN_No;
            }
            set
            {
                strPRN_No = value;
            }
        }

        #endregion

        #region IsNoChallan
        /// <summary>
        /// Please select if you have mentioned no challan except NIL challan(s) (Challan(s) with zero challan amount) in the statement. It is mandatory to enter unique PAN-Amount Combination in PART 2 for NIL Challan statement.
        /// </summary>
        public bool IsNoChallan
        {
            get
            {
                return bnlNoChallan;
            }
            set
            {
                bnlNoChallan = value;
            }
        }

        #endregion

        #region IsNoChallanCheck
        /// <summary>
        /// Please select if you have mentioned no challan except NIL challan(s) (Challan(s) with zero challan amount) in the statement. It is mandatory to enter unique PAN-Amount Combination in PART 2 for NIL Challan statement.
        /// </summary>
        public bool IsNoChallanCheck
        {
            get
            {
                return bnlNoChallanCheck;
            }
            set
            {
                bnlNoChallanCheck = value;
            }
        }

        #endregion

        #region IsPaymentByBookAdjustmentCheck
        /// <summary>
        /// Please select if the payment was done by book adjustment (for Government Deductors)
        /// </summary>
        public bool IsPaymentByBookAdjustmentCheck
        {
            get
            {
                return bnlBookAdjustCheck;
            }
            set
            {
                bnlBookAdjustCheck = value;
            }
        }

        #endregion

        #region IsPaymentByBookAdjustment
        /// <summary>
        /// Please select if the payment was done by book adjustment (for Government Deductors)
        /// </summary>
        public bool IsPaymentByBookAdjustment
        {
            get
            {
                return bnlBookAdjust;
            }
            set
            {
                bnlBookAdjust = value;
            }
        }

        #endregion
                

        #region panAmtValueCheck
        /// <summary>
        /// Please select if the payment was done by book adjustment (for Government Deductors)
        /// </summary>
        public bool panAmtValueCheck
        {
            get
            {
                return panAmtValCheck;
            }
            set
            {
                panAmtValCheck = value;
            }
        }

        #endregion

        #region panAmtValue
        /// <summary>
        /// Please select if the payment was done by book adjustment (for Government Deductors)
        /// </summary>
        public bool panAmtValue
        {
            get
            {
                return panAmtVal;
            }
            set
            {
                panAmtVal = value;
            }
        }

        #endregion

        #region BSRCode
        public string BSRCode
        {
            get
            {
                return strBSRCode;
            }
            set
            {
                strBSRCode = value;
            }
        }

        #endregion

        #region TaxDepositedDate
        public string TaxDepositedDate
        {
            get
            {
                return strDate;
            }
            set
            {
                strDate = value;
            }
        }

        #endregion

        #region ChallanSerialNo
        public string ChallanSerialNo
        {
            get
            {
                return strChallanNo;
            }
            set
            {
                strChallanNo = value;
            }
        }

        #endregion

        #region ChallanAmount
        public string ChallanAmount
        {
            get
            {
                return strChallanAmount;
            }
            set
            {
                strChallanAmount = value;
            }
        }

        #endregion


        #region CDRecordNumber
        public string CDRecordNumber
        {
            get
            {
                return strCDRecordNo;
            }
            set
            {
                strCDRecordNo = value;
            }
        }

        #endregion


        #region IsValidPANDeductee
        /// <summary>
        /// Please select if there are no valid PAN deductee rows corresponding to the Challan / Transfer Voucher mentioned above
        /// </summary>
        public bool IsValidPANDeductee
        {
            get
            {
                return bnlNoPanDeductee;
            }
            set
            {
                bnlNoPanDeductee = value;
            }
        }

        #endregion


        #region PAN1
        public string PAN1
        {
            get
            {
                return strPAN1;
            }
            set
            {
                strPAN1 = value;
            }
        }

        #endregion

        #region PAN2
        public string PAN2
        {
            get
            {
                return strPAN2;
            }
            set
            {
                strPAN2 = value;
            }
        }

        #endregion

        #region PAN3
        public string PAN3
        {
            get
            {
                return strPAN3;
            }
            set
            {
                strPAN3 = value;
            }
        }

        #endregion

        #region PAN1Amount
        public string PAN1Amount
        {
            get
            {
                return strPAN1Amount;
            }
            set
            {
                strPAN1Amount = value;
            }
        }

        #endregion

        #region PAN2Amount
        public string PAN2Amount
        {
            get
            {
                return strPAN2Amount;
            }
            set
            {
                strPAN2Amount = value;
            }
        }

        #endregion

        #region PAN3Amount
        public string PAN3Amount
        {
            get
            {
                return strPAN3Amount;
            }
            set
            {
                strPAN3Amount = value;
            }
        }

        #endregion


        #region FromChallanDepositDate
        public string FromChallanDepositDate
        {
            get
            {
                return strFromChallanDepositDate;
            }
            set
            {
                strFromChallanDepositDate = value;
            }
        }

        #endregion

        #region ToChallanDepositDate
        public string ToChallanDepositDate
        {
            get
            {
                return strToChallanDepositDate;
            }
            set
            {
                strToChallanDepositDate = value;
            }
        }

        #endregion

        #region ChallanStatus
        public string ChallanStatus
        {
            get
            {
                return strChallanStatus;
            }
            set
            {
                strChallanStatus = value;
            }
        }

        #endregion


        #region AddlReqConsoFile
        public bool AddlReqConsoFile
        {
            get
            {
                return bnlAddlConso;
            }
            set
            {
                bnlAddlConso = value;
            }
        }

        #endregion


        #region AddlReqJustificationFile
        public bool AddlReqJustificationFile
        {
            get
            {
                return bnlAddljustificationRep;
            }
            set
            {
                bnlAddljustificationRep = value;
            }
        }

        #endregion


        #region AddlReqForm16AFile
        public bool AddlReqForm16AFile
        {
            get
            {
                return bnlAddlForm16A;
            }
            set
            {
                bnlAddlForm16A = value;
            }
        }

        #endregion

        #region AddlReqForm16File
        public bool AddlReqForm16File
        {
            get
            {
                return bnlAddlForm16;
            }
            set
            {
                bnlAddlForm16 = value;
            }
        }

        #endregion

        #region AddlReqForm27DFile
        public bool AddlReqForm27DFile
        {
            get
            {
                return bnlAddlForm27D;
            }
            set
            {
                bnlAddlForm27D = value;
            }
        }

        #endregion

        //-- 2025/10/28
        #region ChallanClaimedUncliamedStatus
        public string ChallanClaimedUncliamedStatus
        {
            get
            {
                return strChallanClaimedUncliamedStatus;
            }
            set
            {
                strChallanClaimedUncliamedStatus = value;
            }
        }

        #endregion

        //-- 2026/02/17

        #region DDOSerialNumber
        public string DDOSerialNumber
        {
            get
            {
                return strDDOSerialNumber;
            }
            set
            {
                strDDOSerialNumber = value;
            }
        }
        #endregion


        #region ReciptNumber
        public string ReciptNumber
        {
            get
            {
                return strReciptNumber;
            }
            set
            {
                strReciptNumber = value;
            }
        }
        #endregion

        #region RecordID
        public string RecordID
        {
            get
            {
                return strRecordID;
            }
            set
            {
                strRecordID = value;
            }
        }
        #endregion


        #region TransferVoucherDate
        public string TransferVoucherDate
        {
            get
            {
                return strTransferVoucherDate;
            }
            set
            {
                strTransferVoucherDate = value;
            }
        }
        #endregion



    }


    #endregion


    #region TracesLogin
    public class TracesLogin
    {
        string strUserID = "";
        string strPassword = "";
        string strTAN = "";
        string strCaptchaCode = "";
        string strCaptchaId = "";

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
            get { return strTAN; }
            set { strTAN = value; }
        }

        public string CaptchaCode
        {
            get { return strCaptchaCode; }
            set { strCaptchaCode = value; }
        }

        public string CaptchaId
        {
            get { return strCaptchaId; }
            set { strCaptchaId = value; }
        }
        
    }

    #endregion

    #region DownloadList
    public class DownloadList
    {
        string strReqDate = "";
        string strReqNo = "";
        string strFinYr = "";
        string strQrtr = "";
        string strFrmType = "";
        string strDntype = "";
        string strStatus = "";
        string strRemarks = "";


        public string ReqDate
        {
            get { return strReqDate; }
            set { strReqDate = value; }
        }

        public string ReqNo
        {
            get { return strReqNo; }
            set { strReqNo = value; }
        }

        public string FinYr
        {
            get { return strFinYr; }
            set { strFinYr = value; }
        }
        public string Qrtr
        {
            get { return strQrtr; }
            set { strQrtr = value; }
        }

        public string FrmType
        {
            get { return strFrmType; }
            set { strFrmType = value; }
        }

        public string Dntype
        {
            get { return strDntype; }
            set { strDntype = value; }
        }
        public string Status
        {
            get { return strStatus; }
            set { strStatus = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
    }

    #endregion


    #region PANDetails
    //public class PANDetails
    //{
    //    Message enmStatus = Message.Invalid;
    //    string strName = "";

    //    public Message Status
    //    {
    //        get { return enmStatus; }
    //        set { enmStatus = value; }
    //    }
    //    public string Name
    //    {
    //        get { return strName; }
    //        set { strName = value; }
    //    }

    //}

    #endregion

    #region ComboBoxItem
    public class ComboBoxItem
    {
        public string Value;
        public string Text;
        public ComboBoxItem(string val, string text)
        {
            Value = val;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    #endregion

    #region ConsolidateFile 
    public class RequestStatus {

        string strAuthenCode = "";
        string strFinalResponse = "";

        public string AuthenticationCode
        {
            get { return strAuthenCode; }
            set { strAuthenCode = value; }
        }
        public string StatusMessage
        {
            get { return strFinalResponse; }
            set { strFinalResponse = value; }
        }

    }

    #endregion

    #region PANVerifierDetails
    public class PANVerifierDetails
    {
        string strSurname = "";
        string strMiddleName = "";
        string strFirstName = "";
        string strAreaCode = "";
        string strAOType = "";
        string strRangeCode = "";
        string strAONumber = "";
        string strJurisdiction = "";
        string strBuildingName = "";


        public string Surname
        {
            get { return strSurname; }
            set { strSurname = value; }
        }
        public string MiddleName
        {
            get { return strMiddleName; }
            set { strMiddleName = value; }
        }


        public string FirstName
        {
            get { return strFirstName; }
            set { strFirstName = value; }
        }
        public string AreaCode
        {
            get { return strAreaCode; }
            set { strAreaCode = value; }
        }

        public string AOType
        {
            get { return strAOType; }
            set { strAOType = value; }
        }

        public string RangeCode
        {
            get { return strRangeCode; }
            set { strRangeCode = value; }
        }
        public string AONumber
        {
            get { return strAONumber; }
            set { strAONumber = value; }
        }

        public string Jurisdiction
        {
            get { return strJurisdiction; }
            set { strJurisdiction = value; }
        }

        public string BuildingName 
        {
            get { return strBuildingName; }
            set { strBuildingName = value; }
        }


    }

    #endregion


    #region Deductor
    public class Deductor
    {
        string strTAN = "";
        string strAssessmentYear ="";
        string strFormType ="";
        string strQuarter="";
        string strRegStatement ="";
        string strCorrectionStatement ="";  
        string strPAN="";
        string strDeductee = "";
        int intNoDetailsRec = 0;
        //-------------------------------

        public string TAN
        {
            get { return strTAN; }
            set { strTAN = value;}
        }
        public string AssessmentYear
        {
            get { return strAssessmentYear; }
            set { strAssessmentYear = value; }
        }
        public string FormType
        {
            get { return strFormType; }
            set { strFormType = value; }
        }
        public string Quarter
        {
            get { return strQuarter; }
            set { strQuarter = value; }
        }

        public string RegularStatement
        {
            get { return strRegStatement; }
            set { strRegStatement = value; }
        }
        public string CorrectionStatement
        {
            get { return strCorrectionStatement; }
            set { strCorrectionStatement = value; }
        }

        public string DeducteePAN
        {
            get { return strPAN; }
            set { strPAN = value; }
        }

        public string Deductee
        {
            get { return strDeductee; }
            set { strDeductee = value; }
        }

        public int NoOfDetailsRecord
        {
            get { return intNoDetailsRec; }
            set { intNoDetailsRec = value; }
        }


    }

    #endregion

    #region Certificate197Data
    public class Certificate197Data
    {
        string strCertificateNo = "";
        string strPAN = "";
        string strFAYear = "";

        public string CertificateNo
        {
            get { return strCertificateNo; }
            set { strCertificateNo = value; }
        }

        public string PAN
        {
            get { return strPAN; }
            set { strPAN = value; }
        }

        public string FinYear
        {
            get { return strFAYear; }
            set { strFAYear = value; }
        }
    }


    #endregion

    #region PANDetails
    public class PANDetails : INotifyPropertyChanged
    {
        public PANDetails() { }

        Message enmStatus = Message.Invalid;
        private string strName;
        private string strPANNO;
        private string strStatus;

        [Browsable(false)]
        public Message Message
        {
            get { return enmStatus; }
            set { enmStatus = value; }
        }
        public string Name
        {
            get { return strName; }

            set
            {
                if (value != strName)
                {
                    strName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
        }


        public string PAN
        {
            get { return strPANNO; }
            set
            {
                if (value != strPANNO)
                {
                    strPANNO = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("PAN"));
                }
            }



        }
        public string Status
        {
            get { return strStatus; }
            set
            {
                if (value != strStatus)
                {
                    strStatus = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Status"));
                }
            }

        }


        #region INotifyPropertyChanged Members

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (null != this.PropertyChanged)
            {
                PropertyChanged(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    #endregion


    #region ChallanQuery
    public class ChallanQuery
    {
        string strTAN;
        string strFromDate;
        string strToDate;
        string strBSRCode;
        string strChallanDate;
        string strChallanSerialNo;
        string strAmouint;
        string strErrorMessage;
        enmChallanStatus strMessage;
        string strAmountID;

        // NEW
        string strReceiptNumber;
        string strAvailableAmount;
        public string TAN
        {
            get { return strTAN; }

            set
            {
                if (value != strTAN)
                {
                    strTAN = value;

                }
            }
        }

        public string FromDate
        {
            get { return strFromDate; }

            set
            {
                if (value != strFromDate)
                {
                    strFromDate = value;

                }
            }
        }

        public string ToDate
        {
            get { return strToDate; }

            set
            {
                if (value != strToDate)
                {
                    strToDate = value;

                }
            }
        }


        public string ChallanDate
        {
            get { return strChallanDate; }

            set
            {
                if (value != strChallanDate)
                {
                    strChallanDate = value;

                }
            }
        }
        public string ChallanNo
        {
            get { return strChallanSerialNo; }

            set
            {
                if (value != strChallanSerialNo)
                {
                    strChallanSerialNo = value;
                }
            }
        }

        public string ChallanAmount
        {
            get { return strAmouint; }

            set
            {
                if (value != strAmouint)
                {
                    strAmouint = value;

                }
            }
        }

        public string BSRCode
        {
            get { return strBSRCode; }

            set
            {
                if (value != strBSRCode)
                {
                    strBSRCode = value;

                }
            }
        }

        public enmChallanStatus Message
        {
            get { return strMessage; }

            set
            {
                if (value != strMessage)
                {
                    strMessage = value;

                }
            }
        }
        public string ErrorMessage
        {
            get { return strErrorMessage; }

            set
            {
                if (value != strErrorMessage)
                {
                    strErrorMessage = value;

                }
            }
        }

        public string AmountID
        {
            get { return strAmountID; }

            set
            {
                if (value != strAmountID)
                {
                    strAmountID = value;

                }
            }
        }
        // ============================================================
        // NEW PROPERTIES
        // ============================================================

        public string ReceiptNumber
        {
            get { return strReceiptNumber; }
            set { strReceiptNumber = value; }
        }

        public string AvailableAmount
        {
            get { return strAvailableAmount; }
            set { strAvailableAmount = value; }
        }


    }

    #endregion


    #region NonFilling


    public class NonFilling
    {
        public string FAYear { get; set; }
        public string Quarter { get; set; }
        public string Forms { get; set; }

        public string Flag { get; set; }

        public string Reason { get; set; }

        public string SpecifyReason { get; set; }

    }
    #endregion

    #region TanDetails     

    public class TanDetails
    {
        public string TAN { get; set; }
        public string DeducteeCategory { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string StatusOfTAN { get; set; }
        public string EmailID1 { get; set; }
        public string EmailID2 { get; set; }

        public string AreaCode { get; set; }
        public string AOType { get; set; }
        public string RangeCode { get; set; }
        public string AONumber { get; set; }

        public string AODescription { get; set; }
        public string BuildingName { get; set; }
        public string EmailID { get; set; }


    }
    #endregion

    #region CorrectionData

    public class CorrectionData
    {
        public int CorrectionType { get; set; }
        public TracesData TracesData { get; set; }
        public TracesLogin TracesLogin { get; set; }

    }
    #endregion

}
