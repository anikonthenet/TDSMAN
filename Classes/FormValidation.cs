using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


namespace TDSMAN.Classes
{
    class FormValidation
    {

        #region IsValidStatement
        public static bool IsValidStatement(ConsolidateData objData, out string strMessage)
        {
            strMessage = "";
            // VALIDATE Provisional Receipt Number of accepted Regular statement
            //================================================================
            if (string.IsNullOrEmpty(objData.PRN_No))
            {
                strMessage = "Please enter valid 15 digit Provisional Receipt Number";
                return false;
            }
            if (objData.PRN_No.Length != 15)
            {
                strMessage = "Please enter valid 15 digit Provisional Receipt Number";
                return false;
            }
            else
            {
                if (!IsValidPRN_No(objData.PRN_No))
                {
                    strMessage = "Please enter valid 15 digit Provisional Receipt Number";
                    return false;
                }
            }
            // VALIDATE Quarter 
            //================================================================
            if (string.IsNullOrEmpty(objData.Quarter))
            {
                strMessage = "Please enter Quarter";
                return false;
            }
            // VALIDATE Financial Year	
            //================================================================
            if (string.IsNullOrEmpty(objData.FinYear))
            {
                strMessage = "Please enter Tax Year";
                return false;
               
            }
            //VALIDATE Bank Branch (BSR) Code 
            if (string.IsNullOrEmpty(objData.BSRCode))
            {
                //strMessage = "Please enter Bank Branch (BSR) Code";
                //return false;
            }
            else
            {
                if(!IsValidBSRCode(objData.BSRCode,out strMessage))                                   
                return false;
                
            }
            //VALIDATE Challan Serial Number/ Transfer Voucher Number/ DDO Serial Number
            if (string.IsNullOrEmpty(objData.ChallanSlNo))
            {
                //strMessage = "Please enter Challan Serial Number/ Transfer Voucher Number";
                //return false;
            }
            else
            {
                if (!isValidChallanSerialNo(objData.ChallanSlNo, out strMessage))                  
                    return false;
            }

            //VALIDATE Date of Deposit (DD/MM/YYYY)
            if (string.IsNullOrEmpty(objData.ChallanDate))
            {
                strMessage = "Please enter Date of Deposit (DD/MM/YYYY)";
                return false;
            }
            else
            {
                if (!IsValidDate(objData.ChallanDate, out strMessage))                  
                    return false;                
            }

            //VALIDATE Challan Deposit / Transfer Voucher Amount.
            if (string.IsNullOrEmpty(objData.ChallanAmount))
            {
                strMessage = "Please enter Challan Deposit / Transfer Voucher Amount ";
                return false;
            }
            else
            {
                if (!IsValidChallanAmount(objData.ChallanAmount, out strMessage))
                    return false;

            }

            //VALIDATE Challan Deposit / Transfer Voucher Amount 
            if (string.IsNullOrEmpty(objData.KycPan1))
            {
                //strMessage = "Please enter the PAN of 1st Deductee";
                //return false;
            }
            else
            {
                if (!IsValidPAN(objData.KycPan1, out strMessage))
                {                   
                    return false;
                }

                if (string.IsNullOrEmpty(objData.KycAmt1))
                {
                    strMessage = "Enter the TDS Deducted Amount for 1st Deductee";
                    return false;
                }
                else
                {
                    if (!IsValidAmount(objData.KycAmt1, out strMessage))
                    {                       
                        return false;
                    }
                }

            }

            //VALIDATE PAN of deductee/collectee
            if (string.IsNullOrEmpty(objData.KycPan2))
            {
                //strMessage = "Please enter the PAN of 2nd Deductee";
                //return false;
            }
            else
            {
                if (!IsValidPAN(objData.KycPan2, out strMessage))
                {                  
                    return false;
                }

                if (string.IsNullOrEmpty(objData.KycAmt2))
                {
                    strMessage = "Enter the TDS Deducted Amount for 2nd Deductee";
                    return false;
                }
                else
                {
                    if (!IsValidAmount(objData.KycAmt2, out strMessage))
                    {                       
                        return false;
                    }
                }
            }

            //VALIDATE PAN of deductee/collectee
            if (string.IsNullOrEmpty(objData.KycPan3))
            {
                //strMessage = "Please enter the PAN of 3rd Deductee";
                //return false;
            }
            else
            {
                if (!IsValidPAN(objData.KycPan3, out strMessage))
                {                   
                    return false;
                }

                if (string.IsNullOrEmpty(objData.KycAmt3))
                {
                    strMessage = "Enter the TDS Deducted Amount for 3rd Deductee";
                    return false;
                }
                else
                {
                    if (!IsValidAmount(objData.KycAmt3, out strMessage))
                    {
                       
                        return false;
                    }
                }

            }
            //VALIDATE PAN of deductee/collectee 
            return true;
        }

        #endregion

        #region IsValidPRN_No
        public static bool IsValidPRN_No(string strRRRNo)
        {
            if (Convert.ToInt64(strRRRNo) == 0) return false;


            int IntLastNos = Convert.ToInt32(strRRRNo.Substring(14));
            long lngRem = Convert.ToInt64(strRRRNo.Substring(0, 14));


            if (lngRem % 7 != IntLastNos)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region IsValidBSRCode
        public static bool IsValidBSRCode(string strBSRCode, out string strErrMessage)
        {
            strErrMessage = "";

            if (strBSRCode.Length == 0) return false;

            if (strBSRCode.Length != 7)
            {
                strErrMessage = "Please enter 7 digit BSR code of the bank branch through which remittance is made";
                return false;
            }

            if (strBSRCode.Length == 7)
            {
                string strPattern = @"^\d{7}$";
                if (!Regex.IsMatch(strBSRCode, strPattern))
                {
                    strErrMessage = "Please enter 7 digit BSR code of the bank branch through which remittance is made";
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region isValidChallanSerialNo
        public static bool isValidChallanSerialNo(string strChallanSlNo, out string strErrMessage)
        {
            strErrMessage = "";

            if (strChallanSlNo.Length == 0) return false;

            char[] chrArray = new char[strChallanSlNo.Length];
            StringReader strBuilder = new StringReader(strChallanSlNo);

            strBuilder.Read(chrArray, 0, chrArray.Length);


            for (int i = 0; i < chrArray.Length; i++)
            {
                if (!(Convert.ToInt32(chrArray[i]) >= '0' && Convert.ToInt32(chrArray[i]) <= '9'))
                {
                    strErrMessage = "Please enter valid Challan serial no./Transfer Voucher no.";

                    return false;

                }
            }

            return true;
        }

        #endregion

        #region IsValidDate
        private static bool IsValidDate(string strDate, out string strErrMessage)
        {
            DateService dtService = new DateService();
            strErrMessage = "";

            if (string.IsNullOrEmpty(strDate) || strDate.Length == 0)
            {
                strErrMessage = "Please enter valid Date of deposit in DD/MM/YYYY format";
                return false;
            }

            if (strDate[2] != '/' || strDate[5] != '/')
            {
                strErrMessage = "Please enter valid Date of deposit in DD/MM/YYYY format";
                return false;
            }

            if (dtService.J_IsDateValid(strDate) == false)
            {
                strErrMessage = "Please enter valid Date of deposit in DD/MM/YYYY format";
                return false;
            }


            

            //------------------------------------------------------
            int day = Convert.ToInt32(strDate.Substring(0, 2));
            int month = Convert.ToInt32(strDate.Substring(3, 2));
            int year = Convert.ToInt32(strDate.Substring(6,4));
            //------------------------------------------------------
            if (!(day >= 1 && day <= 31) || !(month >= 1 && month <= 12) || !(year > 2000))
            {                              
                    strErrMessage ="Please enter valid Date of deposit in DD/MM/YYYY format";
                    return false;                
            }

            return true;
        }

        #endregion

        #region IsValidAmount
        private static bool IsValidAmount(string strAmount, out string strErrMessage)
        {
            strErrMessage = "";

            if (string.IsNullOrEmpty(strAmount) || strAmount.Length == 0)
            {
                strErrMessage = "Invalid Amount";
                return false;
            }
            //APPEND DECIMAL VALUE
            if (strAmount.IndexOf(".") == -1)
                strAmount = strAmount + ".00";

            if (strAmount.Length > 16)
            {
                strErrMessage = "Invalid Amount";
                return false;
            }

            for (int i = 0; i < strAmount.Length; i++)
            {
                if (i == strAmount.Length - 3)
                    continue; // skipping .(dot)

                if (!(strAmount[i] >= '0' && strAmount[i] <= '9'))
                {
                    strErrMessage = "Invalid Amount";
                    return false;
                }
            }
            if (strAmount.IndexOf('.') != strAmount.Length - 3)
            {
                strErrMessage = "Invalid Amount";
                return false;
            }


            return true;
        }


        #endregion

        #region IsValidChallanAmount
        private static bool IsValidChallanAmount(string strAmmount, out string strErrMessage)
        {
            strErrMessage = "";

            if (!IsValidAmount(strAmmount, out strErrMessage))
            {
                strErrMessage = "Please enter the Challan/Transfer voucher deposit Amount (in Rs.)";
                return false;
            }

            return true;
        }

        #endregion        

        #region IsValidPAN
        public static bool IsValidPAN(string strPAN, out string strErrMessage)
        {
            strErrMessage = "";
            if (strPAN.Length == 0 || strPAN.Length != 10)
            {
                strErrMessage = "Invalid PAN";
                return false;
            }
            if (strPAN == "PANNOTAVBL" || strPAN == "PANAPPLIED" || strPAN == "PANINVALID")
            {
                //strErrMessage = "Invalid PAN";
                return true;
            }

            for (int i = 0; i < strPAN.Length; i++)
            {
                if ((i >= 0 && i <= 4) || i == 9)
                {
                    if ((i == 3) && (strPAN[3] != 'P' && strPAN[3] != 'H' && strPAN[3] != 'C' && strPAN[3] != 'J' && strPAN[3] != 'F' &&
                        strPAN[3] != 'A' && strPAN[3] != 'T' && strPAN[3] != 'B' && strPAN[3] != 'L' && strPAN[3] != 'G'))
                    {
                        strErrMessage = "Invalid PAN";
                        return false;
                    }

                    if (!(strPAN[i] >= 'A' && strPAN[i] <= 'Z'))
                    {
                        strErrMessage = "Invalid PAN";
                        return false;
                    }

                }
                else
                {
                    if (!(strPAN[i] >= '0' && strPAN[i] <= '9'))
                    {
                        strErrMessage = "Invalid PAN";
                        return false;
                    }
                }

            }

            return true;
        }


        #endregion

    }

}
