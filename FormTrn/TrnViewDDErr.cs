#region Referred Namespaces
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
//~~~~ User Namespaces ~~~~
using TDSMAN;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;
using TDSMAN.FormMst;
using TDSMAN.FormPar;
using TDSMAN.FormUtl;
//------------
using System.Reflection;

using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnViewDDErr : Form
    {


        #region InitializeComponent
        public TrnViewDDErr(long BasicInfoID, long DeducteeID, string Form, long FaYearID, string Field)
        {
            InitializeComponent();
            lngBasicInfoID = BasicInfoID;
            lngDeducteeID = DeducteeID;
            strForm = Form;
            lngFaYearID = FaYearID;
            strField = Field;
        }
        #endregion

        #region Objects & Variables declaration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();

        ToolTip tllTip = new ToolTip();

        mdiTDSMAN mdiTDSMAN = new mdiTDSMAN();

        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        string strSearchText = "-- Search here (min 3 chars) --";
        string strFilingStatusSearchText = "-- Search TAN - Name here (min 3 chars) --";
        //--
        string strSQL = "";
        //
        //long lngNoOfRecordsSQL = 5, lngNoOfReturnDays = 120;
        string strQuery = "", strQuarter = "", strCompanyName = ""; int intAsstId = 0;
        string strSortReturnsUnderProcess = "";
        bool blExit = true;
        //
        string strSQLReturnUnderProcess = "", strSQLReturnsReadyForFiling = "", strSQLFiledReturns = "", strSQLFilingStatus = "", strField="";

        

        bool blResize = true;

        

        long lngBasicInfoID = 0, lngDeducteeID = 0, lngFaYearID = 0;

        

        string strForm = "";
        #endregion

        #region ShowDeducteeRecord
        private bool ShowDeducteeRecord(long DeducteeID, string FormNo, long FAYearID)
        {
            IDataReader drdShowDeducteeRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            string strPrefix = "";
            try
            {
                if (FormNo == T_FormNo.F24Q)
                    strPrefix = "MST_EMPLOYEE.EMPLOYEE";
                else
                    strPrefix = "MST_DEDUCTEE.DEDUCTEE";
                //
                string[,] strGrossingUpIndicatorMatrix = {{"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'Y' ", "F", "Yes", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = 'N'", "F", "No", "T"},
                                                {"TRN_DEDUCTEE_DETAILS.GROSSING_UP_INDICATOR = ''", "F", "", "T"}};

                //string[,] strShowDeducteeCodeMatrix = null;
                //string[,] strShowDeducteeCodeMatrix = {{"MST_DEDUCTEE.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                //                                {"MST_DEDUCTEE.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"}};
                string[,] strShowDeducteeCodeMatrix = null;
                //
                if (FormNo == T_FormNo.F26Q)
                {
                    string[,] strShowDeducteeCodeMatrixF26Q = {{"MST_DEDUCTEE.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                                                 {"MST_DEDUCTEE.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"}};
                    strShowDeducteeCodeMatrix = strShowDeducteeCodeMatrixF26Q;
                }
                else if (FormNo == T_FormNo.F27Q || FormNo == T_FormNo.F27EQ)
                {
                    if (FAYearID >= T_FinancialYearID.F2023_24ID)
                    {
                        string[,] strShowDeducteeCodeMatrixF27EQ = {{ "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"},
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.HinduUndividedFamily + "'", "F", T_DeducteeCodeDesc.HinduUndividedFamily, "T"},
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.AOPExceptAOPOnlyCompaniesAsitsMembers + "'", "F", T_DeducteeCodeDesc.AOPExceptAOPOnlyCompaniesAsitsMembers, "T" },
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.AOPConsistingAsitsMembers + "'", "F", T_DeducteeCodeDesc.AOPConsistingAsitsMembers, "T" },
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.Co_operativeSociety + "'", "F", T_DeducteeCodeDesc.Co_operativeSociety, "T" },
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.Firm + "'", "F", T_DeducteeCodeDesc.Firm, "T" },
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.BodyOfIndividuals + "'", "F", T_DeducteeCodeDesc.BodyOfIndividuals, "T" },
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.ArtificialJuridicalPerson + "'", "F", T_DeducteeCodeDesc.ArtificialJuridicalPerson, "T"},
                                                 { "TRN_DEDUCTEE_DETAILS.DEDUCTEE_CODE = '" + T_DeducteeCode.Others + "'", "F", T_DeducteeCodeDesc.Others, "T"}};
                        strShowDeducteeCodeMatrix = strShowDeducteeCodeMatrixF27EQ;
                    }
                    else
                    {
                        string[,] strShowDeducteeCodeMatrixF27EQ = {{"MST_DEDUCTEE.DEDUCTEE_CODE ='" + T_DeducteeCode.Company + "'" , "F", T_DeducteeCodeDesc.Company, "T"},
                                                 {"MST_DEDUCTEE.DEDUCTEE_CODE = '" + T_DeducteeCode.NonCompany + "'", "F", T_DeducteeCodeDesc.NonCompany, "T"}};
                        strShowDeducteeCodeMatrix = strShowDeducteeCodeMatrixF27EQ;
                    }
                }
                //
                strSQL = "SELECT TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID   AS DEDUCTEE_DETAIL_ID," +
                    "            TRN_DEDUCTEE_DETAILS.SL_NO                AS SL_NO," +
                    "            " + strPrefix + "_ID                      AS PARTY_ID," +
                    "            " + strPrefix + "_NAME                    AS PARTY_NAME," +
                    "            " + strPrefix + "_PAN                     AS PARTY_PAN,";
                if (FormNo == T_FormNo.F27Q)
                    strSQL = strSQL + " TRN_DEDUCTEE_DETAILS.EMAIL            AS EMAIL, " +
                                      " TRN_DEDUCTEE_DETAILS.MOBILE_NO        AS MOBILE_NO, " +
                                      " TRN_DEDUCTEE_DETAILS.DEDUCTEE_TAX_ID  AS DEDUCTEE_TAX_ID, " +
                                      " TRN_DEDUCTEE_DETAILS.DEDUCTEE_ADDRESS AS DEDUCTEE_ADDRESS, ";
                if (FormNo != T_FormNo.F24Q)
                {
                    strSQL = strSQL + "  " + cmnService.J_SQLDBFormat(strShowDeducteeCodeMatrix, J_SQLColFormat.Case_End) + " AS DEDUCTEE_CODE,";
                }

                if (FormNo == T_FormNo.F24Q)
                {
                    strSQL = strSQL + "  MST_EMPLOYEE.EMPLOYEE_REF AS EMPLOYEE_REF,";
                }
                strSQL = strSQL + " " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS PAYMENT_DATE," +
                    "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEDUCTED_DATE," +
                    "            TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.RATE                 AS RATE," +
                    "            TRN_DEDUCTEE_DETAILS.TAX_AMOUNT           AS TAX_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.SURCHARGE_AMOUNT     AS SURCHARGE_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.CESS_AMOUNT          AS CESS_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.NON_DEDUCTION_FLAG   AS NON_DEDUCTION_FLAG," +
                    "            TRN_DEDUCTEE_DETAILS.TOT_VALUE_PURCHASE   AS TOT_VALUE_PURCHASE," +
                    "            TRN_DEDUCTEE_DETAILS.CASH_BOOK_ENTRY      AS CASH_BOOK_ENTRY," +
                    "            REASON + ' ' + MST_REASON.DESCRIPTION     AS REASON_DESCRIPTION," +
                    "            MST_SECTION.SECTION_NO                    AS SECTION_NO," +
                    "            MST_SECTION.SECTION_DESCRIPTION           AS SECTION_DESCRIPTION," +
                    "            TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO       AS CERTIFICATE_NO," +
                    "            " + cmnService.J_SQLDBFormat(strGrossingUpIndicatorMatrix, J_SQLColFormat.Case_End) + " AS GROSSING_UP_INDICATOR," +
                    "            REMITTANCE_DESC + ' ' + REMITTANCE_CODE   AS REMITTANCE_DESC," +
                    "            TDS_APPLICABILITY_DESC + ' ' + TDS_APPLICABILITY_CODE AS TDS_APPLICABILITY_DESC," +
                    "            COUNTRY_DESC + ' ' + COUNTRY_CODE         AS COUNTRY_DESC," +
                    "            TRN_DEDUCTEE_DETAILS.UNIQUE_ACKN          AS UNIQUE_ACKN," +
                    "            TRN_DEDUCTEE_DETAILS.REFERENCE_NO         AS REFERENCE_NO," +
                    "            TRN_DEDUCTEE_DETAILS.NON_RESIDENT         AS NON_RESIDENT," +
                    "            TRN_DEDUCTEE_DETAILS.PERMANENT_ESTABLISHMENT AS PERMANENT_ESTABLISHMENT," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194N_EXCESS_1CRORE_AMOUNT AS SEC194N_EXCESS_1CRORE_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.EMPLOYEE_SERIAL_NO   AS EMPLOYEE_SERIAL_NO," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT   AS SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT  AS SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT," +
                    "            TRN_DEDUCTEE_DETAILS.TDS_CLAUSE_CHALLAN_NO     AS TDS_CLAUSE_CHALLAN_NO," +
                    "            " + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.TDS_CLAUSE_PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS TDS_CLAUSE_PAYMENT_DATE," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194NC_EXCESS_3CRORE    AS SEC194NC_EXCESS_3CRORE," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194NFT_20L_3CRORE      AS SEC194NFT_20L_3CRORE," +
                    "            TRN_DEDUCTEE_DETAILS.SEC194NFT_EXCESS_3CRORE   AS SEC194NFT_EXCESS_3CRORE," +
                    "            TRN_DEDUCTEE_DETAILS.SECTION_115BAC_FLAG       AS SECTION_115BAC_FLAG " +
                    "     FROM ((((((TRN_DEDUCTEE_DETAILS INNER JOIN " + strPrefix.Substring(0, 12) + " " +
                    "            ON  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strPrefix + "_ID) " +
                    "            INNER JOIN MST_REASON " +
                    "            ON TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID) " +
                    "            INNER JOIN MST_SECTION " +
                    "            ON TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_SECTION.SECTION_ID) " +
                    "            LEFT JOIN MST_REMITTANCE " +
                    "            ON TRN_DEDUCTEE_DETAILS.REMITTANCE_ID = MST_REMITTANCE.REMITTANCE_ID) " +
                    "            LEFT JOIN MST_TDS_APPLICABILITY " +
                    "            ON TRN_DEDUCTEE_DETAILS.TDS_APPLICABILITY_ID = MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID) " +
                    "            LEFT JOIN MST_COUNTRY " +
                    "            ON TRN_DEDUCTEE_DETAILS.COUNTRY_ID = MST_COUNTRY.COUNTRY_ID) " +
                    "     WHERE  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID = " + DeducteeID + " ";
                //
                drdShowDeducteeRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowDeducteeRecord == null)
                    return false;
                //--
                while (drdShowDeducteeRecord.Read())
                {
                    //lngDeducteeDetailID = Id;

                    //blnShowHelp = false;
                    //blnShowPANHelp = false;
                    //blnShowCertificateNoHelp = false;       // ADDED BY DHRUB ON @2015-11-20

                    txtDeducteeSrlNo.Text = Convert.ToString(drdShowDeducteeRecord["SL_NO"]);

                    txtDeducteeName.Text = Convert.ToString(drdShowDeducteeRecord["PARTY_NAME"]);
                    txtDeducteeName.SelectionStart = 0;

                    txtOldDeducteeName.Text = Convert.ToString(drdShowDeducteeRecord["PARTY_NAME"]);
                    txtDeducteePAN.Text = Convert.ToString(drdShowDeducteeRecord["PARTY_PAN"]);
                    //-- 2020/06/04
                    ////if (strFormNo == T_FormNo.F24Q || strFormNo == T_FormNo.F26Q || strFormNo == T_FormNo.F27Q)
                    ////{
                    ////    //txtEmpRefNo.Text = Convert.ToString(drdShowDeducteeRecord["EMPLOYEE_REF"]);
                    ////    //-- 2020/07/27
                    ////    txtEmpRefNo.Text = Convert.ToString(drdShowDeducteeRecord["EMPLOYEE_SERIAL_NO"]);
                    ////} COMMENTED ON 2021/10/22
                    //-- 2021/10/22
                    if (FormNo == T_FormNo.F24Q)
                        txtEmpRefNo.Text = Convert.ToString(drdShowDeducteeRecord["EMPLOYEE_SERIAL_NO"]);
                    else if (FormNo == T_FormNo.F26Q || FormNo == T_FormNo.F27Q)
                        txtRefernceNo.Text = Convert.ToString(drdShowDeducteeRecord["REFERENCE_NO"]); //-- 2023/09/07
                                                                                                      //-- COMMENTED txtEmpRefNo.Text = Convert.ToString(drdShowDeducteeRecord["REFERENCE_NO"]);
                                                                                                      //--
                    if (FormNo != T_FormNo.F24Q)
                    {
                        if (FormNo == T_FormNo.F26Q)
                            cmbDeducteeCode.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_CODE"]);
                        else
                        {
                            if (FAYearID >= T_FinancialYearID.F2023_24ID)
                            {
                                cmbDeducteeCode.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_CODE"]);
                            }
                            else
                                cmbDeducteeCode.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_CODE"]);
                        }

                    }
                    mskDeducteeDate.Text = Convert.ToString(drdShowDeducteeRecord["PAYMENT_DATE"]);
                    txtDeducteeAmountOfPayment.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["PAYMENT_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["PAYMENT_AMOUNT"])));
                    txtDeducteeRate.Text = string.Format("{0:0.0000}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["RATE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["RATE"])));
                    txtDeducteeIncometax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"])));
                    txtOldDeducteeIncometax.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_AMOUNT"])));

                    txtDeducteeSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"])));
                    txtOldDeducteeSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SURCHARGE_AMOUNT"])));

                    txtDeducteeCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"])));
                    txtOldDeducteeCess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["CESS_AMOUNT"])));

                    txtDeducteeTotal.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"])));
                    txtOldDeducteeTotal.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TOTAL_AMOUNT"])));

                    txtDeducteeTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"])));
                    txtOldDeducteeTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TAX_DEPOSITED_AMOUNT"])));

                    cmbRemarks.Text = Convert.ToString(drdShowDeducteeRecord["REASON_DESCRIPTION"]);
                    //txtDeducteeTotalTaxDeposited.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowChallanRecord["TAX_DEPOSITED_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowChallanRecord["TDS"])));
                    txtTotalValueOfPurchase.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["TOT_VALUE_PURCHASE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["TOT_VALUE_PURCHASE"])));
                    // ANIK 2011-08-04
                    if (Convert.ToString(drdShowDeducteeRecord["CASH_BOOK_ENTRY"]) == "1")
                        chkCashBookEntry.Checked = true;
                    else
                        chkCashBookEntry.Checked = false;

                    //Added by Shrey Kejriwal on 26/03/2012
                    mskDeductedDate.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTED_DATE"]);
                    //
                    if (lngFaYearID >= T_FinancialYearID.F2013_14ID)
                    {
                        //blnSectionDDDisplay = false;
                        txtSection.Text = Convert.ToString(drdShowDeducteeRecord["SECTION_NO"]);
                        lblDDSectionDisplay.Visible = true;
                        lblDDSectionDisplay.Text = Convert.ToString(drdShowDeducteeRecord["SECTION_DESCRIPTION"]);
                        //blnSectionDDDisplay = true;
                    }
                    //
                    txtCertificateNo.Text = Convert.ToString(drdShowDeducteeRecord["CERTIFICATE_NO"]);
                    cmbGrossingUpIndicator.Text = Convert.ToString(drdShowDeducteeRecord["GROSSING_UP_INDICATOR"]);
                    cmbRemittance.Text = Convert.ToString(drdShowDeducteeRecord["REMITTANCE_DESC"]);
                    cmbTDSRate.Text = Convert.ToString(drdShowDeducteeRecord["TDS_APPLICABILITY_DESC"]);
                    cmbCountry.Text = Convert.ToString(drdShowDeducteeRecord["COUNTRY_DESC"]);
                    txtUniqueAckn.Text = Convert.ToString(drdShowDeducteeRecord["UNIQUE_ACKN"]);
                    //--
                    if (FormNo == T_FormNo.F27Q)
                    {
                        txtF27QEmail.Text = Convert.ToString(drdShowDeducteeRecord["EMAIL"]);
                        txtF27QContactNo.Text = Convert.ToString(drdShowDeducteeRecord["MOBILE_NO"]);
                        txtF27QTaxIdentificationNumber.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_TAX_ID"]);
                        txtF27QDeducteeAddress.Text = Convert.ToString(drdShowDeducteeRecord["DEDUCTEE_ADDRESS"]);
                    }
                    //--
                    //--
                    txtRefernceNo.Text = Convert.ToString(drdShowDeducteeRecord["REFERENCE_NO"]); //-- 2017-02-13
                    //-- 2017/12/05
                    if (Convert.ToString(drdShowDeducteeRecord["NON_RESIDENT"]) == "Y")
                    {
                        cmbNonResident.Text = T_YES_NO.YES;
                        //strNonResident = Convert.ToString(drdShowDeducteeRecord["NON_RESIDENT"]);
                    }
                    else if (Convert.ToString(drdShowDeducteeRecord["NON_RESIDENT"]) == "N")
                        cmbNonResident.Text = T_YES_NO.NO;
                    else
                        cmbNonResident.Text = "";
                    //
                    if (Convert.ToString(drdShowDeducteeRecord["PERMANENT_ESTABLISHMENT"]) == "Y")
                    {
                        cmbPermanentEstablishment.Text = T_YES_NO.YES;
                        //strPermanentEstablishment = Convert.ToString(drdShowDeducteeRecord["PERMANENT_ESTABLISHMENT"]);
                    }
                    else if (Convert.ToString(drdShowDeducteeRecord["PERMANENT_ESTABLISHMENT"]) == "N")
                        cmbPermanentEstablishment.Text = T_YES_NO.NO;
                    else
                        cmbPermanentEstablishment.Text = "";
                    //
                    txtAmountOfCashWithdrawlOneCrore.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194N_EXCESS_1CRORE_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194N_EXCESS_1CRORE_AMOUNT"])));
                    //-- 2020/10/16
                    ////////if (Convert.ToDouble(drdShowDeducteeRecord["SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT"]) > 0)
                    ////////{
                    ////////    //chkBox194NNotFiled.Visible = true;
                    ////////    //chkBox194NNotFiled.Checked = true;
                    ////////    //grp194NFOptions.Visible = true;
                    ////////    //rbn194N20L1Cr.Checked = true;
                    ////////}
                    ////////else if (Convert.ToDouble(drdShowDeducteeRecord["SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT"]) > 0)
                    ////////{
                    ////////    //chkBox194NNotFiled.Visible = true;
                    ////////    //chkBox194NNotFiled.Checked = true;
                    ////////    //grp194NFOptions.Visible = true;
                    ////////    //rbn194NGreater1Cr.Checked = true;
                    ////////}
                    ////////else
                    ////////{
                    ////////    chkBox194NNotFiled.Checked = false;
                    ////////    //rbn194N20L1Cr.Checked = false;
                    ////////    //rbn194NGreater1Cr.Checked = false;
                    ////////}
                    //--2023/08/11
                    txtAmount194NCForm26Q27Q.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194NC_EXCESS_3CRORE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194NC_EXCESS_3CRORE"])));
                    //txtAmount20L1Cr194NF.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194N_20LAKH_1CRORE_PAYMENT_AMOUNT"])));
                    //txtAmountExcess1Cr194NF.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194N_GREATER_1CRORE_PAYMENT_AMOUNT"])));
                    //txtAmount20L3Cr194NFTForm26Q27Q.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194NFT_20L_3CRORE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194NFT_20L_3CRORE"])));
                    //txtAmountExcess3Cr194NFTForm26Q27Q.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowDeducteeRecord["SEC194NFT_EXCESS_3CRORE"]) == "" ? "0" : Convert.ToString(drdShowDeducteeRecord["SEC194NFT_EXCESS_3CRORE"])));
                    //////if (Convert.ToDouble(drdShowDeducteeRecord["SEC194NFT_20L_3CRORE"]) > 0)
                    //////{
                    //////    grp194NFTForm26Q27Q.Visible = true;
                    //////    rbn194NFT20L3Cr.Checked = true;
                    //////}
                    //////else if (Convert.ToDouble(drdShowDeducteeRecord["SEC194NFT_EXCESS_3CRORE"]) > 0)
                    //////{
                    //////    grp194NFTForm26Q27Q.Visible = true;
                    //////    rbn194NFTExcess3Cr.Checked = true;
                    //////}
                    //////else
                    //////{
                    //////    grp194NFTForm26Q27Q.Visible = false;
                    //////    rbn194NFT20L3Cr.Checked = false;
                    //////    rbn194NFTExcess3Cr.Checked = false;
                    //////}
                    //-- 2020/12/30
                    txtForm27EQChallanNo.Text = Convert.ToString(drdShowDeducteeRecord["TDS_CLAUSE_CHALLAN_NO"]);
                    mskForm27EQDateOfPayment.Text = Convert.ToString(drdShowDeducteeRecord["TDS_CLAUSE_PAYMENT_DATE"]);
                    //-- 2023/08/14
                    if (FormNo == T_FormNo.F27Q || FormNo == T_FormNo.F27EQ)
                    {
                        if (Convert.ToString(drdShowDeducteeRecord["SECTION_115BAC_FLAG"]) == "1")
                            cmbOptingOutOf115BAC.Text = T_YES_NO.YES.ToString();
                        else if (Convert.ToString(drdShowDeducteeRecord["SECTION_115BAC_FLAG"]) == "2")
                            cmbOptingOutOf115BAC.Text = T_YES_NO.NO.ToString();

                    }
                    //--
                    //blnShowHelp = true;
                    //blnShowPANHelp = true;
                    //blnShowCertificateNoHelp = true;   // ADDED BY DHRUB ON @2015-11-20

                    drdShowDeducteeRecord.Close();
                    drdShowDeducteeRecord.Dispose();
                    //
                    //NO_VALUE_26Q(Convert.ToInt32(Support.GetItemData(cmbDDSection, cmbDDSection.SelectedIndex)));
                    //
                    txtDeducteeName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowDeducteeRecord.Close();
                drdShowDeducteeRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                //lngChallanID = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetChallanGridClone != null) dsetChallanGridClone.Clear();
                //dsetChallanGridClone = dmlService.J_ShowDataInGrid(ref dgcViewDeductee, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeRecord.Close();
                drdShowDeducteeRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region TrnViewDDErr_Load
        private void TrnViewDDErr_Load(object sender, EventArgs e)
        {
            ShowDeducteeRecord(lngDeducteeID, strForm, lngFaYearID);
            //
            #region DESIGN
            if (cmnService.J_ReturnDoubleValue(txtAmount194NCForm26Q27Q.Text) > 0 )
            {
                pnlDeducteeEntry.Width = 734;
                this.Width = 755;
            }
            //
            if (cmnService.J_ReturnDoubleValue(txtAmountOfCashWithdrawlOneCrore.Text) > 0)
            {
                pnlDeducteeEntry.Width = 734;
                this.Width = 755;
            }
            //
            if (txtForm27EQChallanNo.Text != "" )
            {
                if (cmnService.J_ReturnInt32Value(txtForm27EQChallanNo.Text) > 0)
                {
                    pnlDeducteeEntry.Width = 734;
                    this.Width = 755;
                }
            }
            //
            if (txtF27QContactNo.Text != "" ||
               txtF27QEmail.Text != "" ||
               txtF27QDeducteeAddress.Text != "" ||
               txtF27QTaxIdentificationNumber.Text != ""
                )
            {
                this.Width = 755;
                pnlDeducteeEntry.Width = 734;
            }
            //
            if (cmbNonResident.Text != "" ||
                cmbPermanentEstablishment.Text != "")
            {
                this.Width = 755;
                pnlDeducteeEntry.Width = 734;
            }
            #endregion
            //
            if (strField.ToUpper().Contains("EMPLOYEE / PARTY  PAN") == true
                || strField.ToUpper().Contains("EMPLOYEE PAN") == true)
            {
                lblPan.ForeColor = Color.Red;
                txtDeducteePAN.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("DATE ON WHICH AMOUNT PAID / CREDITED / DEBITED") == true)
            {
                lblPaymentDate.ForeColor = Color.Red;
                mskDeducteeDate.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("TOTAL INCOME TAX DEDUCTED AT SOURCE") == true)
            {
                lblIncomeTax.ForeColor = Color.Red;
                txtDeducteeIncometax.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("AMOUNT OF PAYMENT / CREDIT / DEBITED") == true)
            {
                lblAmountOfPayment.ForeColor = Color.Red;
                txtDeducteeAmountOfPayment.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("SURCHARGE") == true)
            {
                lblDeducteeSurcharge.ForeColor = Color.Red;
                txtDeducteeSurcharge.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("EDUCATION CESS") == true)
            {
                lblDeducteeCess.ForeColor = Color.Red;
                txtDeducteeCess.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("RATE AT WHICH TAX") == true)
            {
                lblRate.ForeColor = Color.Red;
                txtDeducteeRate.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("SECTION CODE") == true)
            {
                lblSection.ForeColor = Color.Red;
                txtSection.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("CERTIFICATE") == true)
            {
                lblCertificateNo.ForeColor = Color.Red;
                txtCertificateNo.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("") == true)
            {
                lblPaymentDate.ForeColor = Color.Red;
                mskDeducteeDate.ForeColor = Color.Red;
            }
        }
        #endregion

        #region TrnViewDDErr_KeyDown
        private void TrnViewDDErr_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e== Keys.Escape)
            //{

            //}
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        private void TrnViewDDErr_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e == Keys.Escape)
            //{

            //}


        }

        private void TrnViewDDErr_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

    }
}
