using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TDSMAN.Classes
{
    public static class TracesValidation
    {

        
        #region IsValidChallanStatusQuery1
        public static bool IsValidChallanStatusQuery1(ref TracesData objData, out string strResponse)
        {
            strResponse = "";

            if (string.IsNullOrEmpty(objData.FromChallanDepositDate.Trim()) || objData.FromChallanDepositDate == "  /  /")
            {
                strResponse = "Challan Deposit From Date is mandatory";
                return false;
            }
            

            if (string.IsNullOrEmpty(objData.ToChallanDepositDate.Trim()) || objData.ToChallanDepositDate == "  /  /")
            {
                strResponse = "Challan Deposit To Date is mandatory";
                return false;
            }
           
            
            //-----------------------------------------------------------
            int FromDate = ConvertUserDate(objData.FromChallanDepositDate);
            int ToDate = ConvertUserDate(objData.ToChallanDepositDate);
            int intCurrentDate = CurrentDate();
            //------------------------------------------------------------            

            //if (string.IsNullOrEmpty(objData.FromChallanDepositDate) && string.IsNullOrEmpty(objData.ToChallanDepositDate))
            //{
            //    strResponse = "Challan Deposit Date is mandatory";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(objData.FromChallanDepositDate) || string.IsNullOrEmpty(objData.ToChallanDepositDate))
            //{
            //    strResponse = "Both From & To Dates are mandatory";
            //    return false;
            //}
            if (!IsValidDate(objData.FromChallanDepositDate) || !IsValidDate(objData.ToChallanDepositDate))
            {
                strResponse = "Invalid Challan Deposit Date";
                return false;
            }

            if (FromDate > intCurrentDate || ToDate > intCurrentDate)
            {
                strResponse = "Invalid Challan Deposit Date";
                return false;
            }

            if (FromDate > ToDate)
            {
                strResponse = "Invalid Challan Deposit Date";
                return false;
            }

            if (!ValidateStartDate(objData.FromChallanDepositDate))
            {
                strResponse = "Invalid Challan Deposit Date";
                return false;
            }

            if (!validateYearRange(objData.FromChallanDepositDate, objData.ToChallanDepositDate))
            {
                strResponse = "Date range should be within the same financial year.";
                return false;
            }


            return true;
        }


        #endregion

        #region IsValidChallanStatusQuery2
        public static bool IsValidChallanStatusQuery2(TracesData objData, out string strResponse)
        {
            strResponse = "";

            if (string.IsNullOrEmpty(objData.BSRCode))
            {
                strResponse = "BSR Code is mandatory";
                return false;
            }
            else
            {
                if (!IsNumeric(objData.BSRCode))
                {
                    strResponse = "Invalid BSR Code";
                    return false;
                }
            }


            if (string.IsNullOrEmpty(objData.TaxDepositedDate) && objData.TaxDepositedDate.Trim() == "/  /")
            {
                strResponse = "Date of Deposit is mandatory";
                return false;
            }
            //-------------------------------------------------------------
            if (string.IsNullOrEmpty(objData.ChallanSerialNo))
            {
                strResponse = "Challan Serial Number is mandatory";
                return false;
            }
            else
            {
                if (!IsNumeric(objData.ChallanSerialNo))
                {
                    strResponse = "Invalid Challan Serial Number";
                    return false;
                }
            }
            //-------------------------------------------------------------
            if (string.IsNullOrEmpty(objData.ChallanAmount))
            {
                strResponse = "Challan Amount is mandatory";
                return false;
            }
            else
            {
                if (!IsNumeric(objData.ChallanAmount))
                {
                    strResponse = "Invalid Challan Amount";
                    return false;
                }
                int intCount = objData.ChallanAmount.IndexOf(".");

                if (intCount == 0)
                {
                    strResponse = "Amount should be entered in two decimal places";
                    return false;
                }


            }
            //-------------------------------------------------------------




            return true;
        }


        #endregion

        #region IsValidDeductionsDetails
        public static bool IsValidDeductionsDetails(TracesData objData, out string strResponse)
        {
            strResponse = "";
            string strPAN = objData.PAN1 = objData.PAN2 = objData.PAN3;

            if (string.IsNullOrEmpty(strPAN))
            {
                strResponse = "PAN is mandatory";
                return false;
            }
            else
            {
                if (!IsValidPAN(strPAN))
                {
                    strResponse = "Invalid PAN No";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(objData.FAYear))
            {
                strResponse = "Financial Year is mandatory";
                return false;
            }

            if (string.IsNullOrEmpty(objData.FAYear))
            {
                strResponse = "Quarter is mandatory";
                return false;
            }
            if (string.IsNullOrEmpty(objData.Forms))
            {
                strResponse = "Form Type is mandatory";
                return false;
            }


            return true;
        }


        #endregion

        #region IsValidPANInformation
        public static bool IsValidPANInformation(TracesData objData, out string strResponse)
        {
            strResponse = "";

            string strPAN = objData.PAN1 = objData.PAN2 = objData.PAN3;

            if (string.IsNullOrEmpty(strPAN))
            {
                strResponse = "PAN is mandatory";
                return false;
            }
            else
            {
                if (!IsValidPAN(strPAN))
                {
                    strResponse = "Invalid PAN No";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(objData.Forms))
            {
                strResponse = "Form Type is mandatory";
                return false;
            }
            return true;
        }

        #endregion

        #region IsValidRequestedForms
        public static bool IsValidRequestedForms(ref TracesConnect objCon, ref TracesLogin objLogin, TracesData objData, out string strResponse)
        {
            strResponse = "";
            string strVal = "";
            Dictionary<string, string> objNameval = null;

            if (string.IsNullOrEmpty(objData.FAYear))
            {
                strResponse = "Financial Year is mandatory";
                return false;
            }

            if (string.IsNullOrEmpty(objData.Quarter))
            {
                strResponse = "Quarter is mandatory";
                return false;
            }

            if (string.IsNullOrEmpty(objData.Forms))
            {
                strResponse = "Form Type is mandatory";
                return false;
            }

            if (string.IsNullOrEmpty(objData.PRN_NO))
            {
                strResponse = "Token Number / Provisional Receipt Number (PRN) is mandatory";
                return false;
            }
            else
            {
                if (!IsNumeric(objData.PRN_NO))
                {
                    strResponse = "Token Number / PRN is a numerical field. Please ensure you enter only numerals";
                    return false;
                }
            }

            //RETRIEVE PARAMETER
            if (objData.IsNoChallan || objData.IsPaymentByBookAdjustment || objData.IsValidPANDeductee)
            {
                //TracesResponse response = objCon.IsChallanExists(objData, objLogin);
                //objNameval = (Dictionary<string, string>)response.CustomeTypes;
            }

            //CHECKING FOR NIL CHALLAN AMOUNT
            if (objData.IsNoChallan)
            {
                string strStatus = ""; // IsNilChallan(ref objCon, objData);
                if (!string.IsNullOrEmpty(strStatus))
                {
                    strResponse = strStatus;
                    return false;
                }
                else
                {
                    foreach (KeyValuePair<string, string> pair in objNameval)
                    {
                        if (pair.Key == "isChlnNil")
                        {
                            strVal = pair.Value;
                            break;
                        }
                    }
                    //---------------------------------------------------------
                    if (!string.IsNullOrEmpty(strVal))
                    {
                        if (strVal.ToUpper() == "TRUE")
                        {
                            strResponse = "This statement has challan(s) with non-zero challan amount, please enter details of such challan";
                            return false;
                        }
                    }
                }
            }
            else
            {

                if (string.IsNullOrEmpty(objData.BSRCode))
                {
                    strResponse = "Either CIN or Transfer Voucher details are mandatory";
                    return false;
                }
                else
                {
                    if (!IsNumeric(objData.BSRCode))
                    {
                        strResponse = "Invalid BSR Code";
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(objData.TaxDepositedDate))
                {
                    strResponse = "Date on which Tax Deposited is mandatory";
                    return false;
                }
                //else
                //{
                //    if (!IsValidDate(objData.TaxDepositedDate))
                //    {
                //        strResponse = "Enter Date As dd-M-yyyy format";
                //        return false;
                //    }
                //}

                if (string.IsNullOrEmpty(objData.TaxDepositedDate))
                {
                    strResponse = "Challan Serial Number / DDO Serial Number is mandatory";
                    return false;
                }
               

                if (string.IsNullOrEmpty(objData.ChallanSerialNo))
                {
                    strResponse = "Challan Serial Number / DDO Serial Number is mandatory";
                    return false;
                }
                else
                {
                    if (!IsNumeric(objData.ChallanSerialNo))
                    {
                        strResponse = "Invalid Challan Serial Number";
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(objData.ChallanAmount))
                {
                    strResponse = "Challan Amount is mandatory";
                    return false;
                }
                else
                {
                    if (!IsNumeric(objData.ChallanAmount))
                    {
                        strResponse = "Invalid Challan Amount";
                        return false;
                    }
                    int intCount = objData.ChallanAmount.IndexOf(".");

                    if (intCount == 0)
                    {
                        strResponse = "Amount should be entered in two decimal places";
                        return false;
                    }
                }
            }
            //--------------------------------------------------
            //VALIDATION FOR BOOK ADJUSTMENT CHECK TRUE
            //--------------------------------------------------
            if (objData.IsPaymentByBookAdjustment)
            {
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "bkEntryValue")
                    {
                        strVal = pair.Value;
                        break;
                    }
                }
                //-------------------------------------------------
                if (!string.IsNullOrEmpty(strVal))
                {
                    if (strVal.ToUpper() == "TRUE")
                    {
                        strResponse = "For this statement, tax has been paid through challan only. Please enter details of such challan";
                        return false;
                    }
                }
            }
            //--------------------------------------------------
            //VALIDATION FOR NO PAN  CHECK TRUE
            //--------------------------------------------------
            if (objData.IsValidPANDeductee)
            {

                string dedCount = "";
                string bkEntryValue = "";
                //-------------------------------------------
                foreach (KeyValuePair<string, string> pair in objNameval)
                {
                    if (pair.Key == "dedCount")
                        dedCount = pair.Value;

                    if (pair.Key == "bkEntryValue")
                        bkEntryValue = pair.Value;
                }
                //-------------------------------------------
                if (string.IsNullOrEmpty(dedCount))
                    dedCount = "0";
                //-------------------------------------------
                if (Convert.ToInt32(dedCount) > 0)
                {
                    strResponse = "This statement has Challan(s) / Transfer Voucher with deductee rows corresponding to it. Please enter details of such Challan / Transfer Voucher and corresponding deductee rows";
                    return false;                
                }
            }





            return true;
        }
        #endregion

      

        


        #region IsNoPANDeductee
        public static string IsNoPANDeductee(ref TracesConnect objConect, TracesData objData, out string bkEntryValue)
        {
            string strMessage = "";
            //-------------------------------------------
            TracesResponse objResponse = objConect.NoValidPANdeductee(objData);
            Dictionary<string, string> objNameval =(Dictionary<string,string>)objResponse.CustomeTypes;
            //-------------------------------------------
            string dedCount = "";
            bkEntryValue = "";
            //-------------------------------------------
            foreach (KeyValuePair<string, string> pair in objNameval)
            {
                if (pair.Key == "dedCount")
                    dedCount = pair.Value;

                if (pair.Key == "bkEntryValue")
                    bkEntryValue = pair.Value;
            }
            //-------------------------------------------
            if (string.IsNullOrEmpty(dedCount))
                dedCount = "0";
            //-------------------------------------------
            if (Convert.ToInt32(dedCount) > 0)
            {              
                    strMessage = "This statement has Challan(s) / Transfer Voucher with deductee rows corresponding to it. Please enter details of such Challan / Transfer Voucher and corresponding deductee rows";
            }
            //-------------------------------------------
            return strMessage;
        }

        #endregion



        #region IsValidDate
        public static bool IsValidDate(string strDate)
        {
            string pattern = "^((0[1-9]||[12][0-9]||3[01])(-)(jan||mar||may||jul||aug||oct||dec)(-)[1-9][0-9]{3}||(0[1-9]||[12][0-9]||30)(-)(apr||jun||sep||nov)(-)[1-9][0-9]{3}||(0[1-9]||1[0-9]||2[0-8])(-)feb(-)[1-9][0-9]{3}||29(-)feb(-)((0[48]||[2468][048]||[13579][26])00||[0-9]{2}(0[48]||[2468][048]||[13579][26])))";

            return Regex.IsMatch(strDate, pattern);

        }

        #endregion
        
        #region IsValidPAN
        public static bool IsValidPAN(string strPAN)
        {
            return Regex.IsMatch(strPAN, "^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}");
        }

        #endregion

        #region ValidateStartDate
        public static bool ValidateStartDate(string strDate)
        {
            int intFirstDate = 20070401;
            //---------------------------------------------
            if (ConvertUserDate(strDate) < intFirstDate) return false;

            return true;
        }

        #endregion

        #region ConvertUserDate
        public static int ConvertUserDate(string strDate)
        {
            string strValDate = "";

            string[] MonthString = strDate.Split('-');
            string MonthVal = "";
            //---------------------------------------------------------
            foreach (KeyValuePair<string, string> keyVal in MonthNames())
            {
                if (keyVal.Value == MonthString[1])
                {
                    MonthVal = keyVal.Key;
                    break;
                }
            }
            //----------------------------------------------
            strValDate = MonthString[2] + MonthVal + MonthString[0];

            return Convert.ToInt32(strValDate);

        }

        #endregion

        #region CurrentDate
        public static int CurrentDate()
        {
            string strDate = "";

            DateTime dt = DateTime.Now;
            strDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            strDate = strDate.Substring(6, 4) + strDate.Substring(3, 2) + strDate.Substring(0, 2);   

            return Convert.ToInt32(strDate);
        }

        #endregion

        #region MonthNames
        public static List<KeyValuePair<string, string>> MonthNames()
        {
            List<KeyValuePair<string, string>> objMonth = new List<KeyValuePair<string, string>>();


            objMonth.Add(new KeyValuePair<string, string>("01", "Jan"));
            objMonth.Add(new KeyValuePair<string, string>("02", "Feb"));
            objMonth.Add(new KeyValuePair<string, string>("03", "Mar"));
            objMonth.Add(new KeyValuePair<string, string>("04", "Apr"));
            objMonth.Add(new KeyValuePair<string, string>("05", "May"));
            objMonth.Add(new KeyValuePair<string, string>("06", "Jun"));
            objMonth.Add(new KeyValuePair<string, string>("07", "Jul"));
            objMonth.Add(new KeyValuePair<string, string>("08", "Aug"));
            objMonth.Add(new KeyValuePair<string, string>("09", "Sep"));
            objMonth.Add(new KeyValuePair<string, string>("10", "Oct"));
            objMonth.Add(new KeyValuePair<string, string>("11", "Nov"));
            objMonth.Add(new KeyValuePair<string, string>("12", "Dec"));

            return objMonth;

        }

        #endregion

        #region validateYearRange
        public static bool validateYearRange(string FromDate, string ToDate)
        {
            int intyearEnd = 0;
            string[] strFromDate = FromDate.Split('-');
            string[] strTodate = ToDate.Split('-');

            int intStartDate = 0;
            int intEndDate = 0;

            string FinYearStart = "0401";
            string FinYearEnd = "0331";

            string startMonth = strFromDate[1];

            //Creating date for string comparison for start date within Fin Year
            //Fixed DEF253: 1-Jan-2011 to 31-Mar-2011 is within a financial year 
            if (startMonth.ToLower() == "jan" || startMonth.ToLower() == "feb" || startMonth.ToLower() == "mar")
                intyearEnd = Convert.ToInt32(strFromDate[2]) - 1;
            else
                intyearEnd = Convert.ToInt32(strFromDate[2]);
            //-----------------------------------------
            FinYearStart = Convert.ToString(intyearEnd) + FinYearStart;
            FinYearEnd = Convert.ToString(intyearEnd + 1) + FinYearEnd;

            intStartDate = ConvertUserDate(FromDate);
            intEndDate = ConvertUserDate(ToDate);

            if (intStartDate >= Convert.ToInt32(FinYearStart) &&
               intEndDate <= Convert.ToInt32(FinYearEnd))
                return true;
            else
                return false;


        }

        #endregion

        #region IsNumeric
       public static bool IsNumeric(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        #endregion

        

    }
}
