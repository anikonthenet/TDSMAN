
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
    public partial class TrnViewSDErr : Form
    {

        #region TrnViewSDErr
        public TrnViewSDErr(long BasicInfoID, long SalaryID, string Field)
        {
            InitializeComponent();
            lngBasicInfoID = BasicInfoID;
            lngSalaryID = SalaryID;
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
        string strSQLReturnUnderProcess = "", strSQLReturnsReadyForFiling = "", strSQLFiledReturns = "", strSQLFilingStatus = "", strField = "";

        bool blResize = true;
        long lngBasicInfoID = 0, lngSalaryID = 0;
        #endregion




        #region TrnViewCDErr_Activated
        private void TrnViewCDErr_Activated(object sender, EventArgs e)
        {
            
        }
        #endregion


        #region TrnViewCDErr_Activated
        private void TrnViewCDErr_Load(object sender, EventArgs e)
        {
            //
            ShowRecord(lngSalaryID);
            //
            //
            if (strField.ToUpper().Contains("EMPLOYEE / PARTY PAN") == true)
            {
                lblPartyPAN.ForeColor = Color.Red;
                txtEmployeePAN.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("CATEGORY") == true)
            {
                lblCategory.ForeColor = Color.Red;
                cmbEmployeeCategory.ForeColor = Color.Red;
            }
            else if (strField.ToUpper().Contains("PERIOD OF EMPLOYMENT FROM") == true)
            {
                lblToDate.ForeColor = Color.Red;
                mskEmployeeToDate.ForeColor = Color.Red;
            }
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //else if (strField.ToUpper().Contains("") == true)
            //{
            //    lblDOP.ForeColor = Color.Red;
            //    mskDateOfPayment.ForeColor = Color.Red;
            //}
            //
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            //Making the Reader Blocked until the reader gets closed
            //blnRestrictIncomeTaxCalculation = true;

            IDataReader drdShowRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQL = "SELECT  TRN_SALARY_DETAILS.SALARY_DETAILS_ID               AS SALARY_DETAILS_ID," +
                    "             TRN_SALARY_DETAILS.BASIC_INFO_ID                   AS BASIC_INFO_ID," +
                    "             TRN_SALARY_DETAILS.EMPLOYEE_ID                     AS EMPLOYEE_ID," +
                    "             MST_EMPLOYEE.EMPLOYEE_NAME                         AS EMPLOYEE_NAME," +
                    "             MST_EMPLOYEE.EMPLOYEE_PAN                          AS EMPLOYEE_PAN," +
                    "             MST_EMPLOYEE.EMPLOYEE_REF                          AS EMPLOYEE_REF," +
                    "            " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS EMPLOYEE_CATEGORY," +
                    "            " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS FROM_DATE," +
                    "            " + cmnService.J_SQLDBFormat("TRN_SALARY_DETAILS.TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS TO_DATE," +
                    "             TRN_SALARY_DETAILS.ENTRY_MODE                      AS ENTRY_MODE," +
                    "             TRN_SALARY_DETAILS.TS_GS_SEC_17_1                  AS TS_GS_SEC_17_1," +
                    "             TRN_SALARY_DETAILS.TS_GS_SEC_17_2                  AS TS_GS_SEC_17_2," +
                    "             TRN_SALARY_DETAILS.TS_GS_SEC_17_3                  AS TS_GS_SEC_17_3," +
                    "             TRN_SALARY_DETAILS.TS_GS_TOTAL                     AS TS_GS_TOTAL," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_1_DESC               AS TS_LA_ITEM_1_DESC," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_1                    AS TS_LA_ITEM_1," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_2_DESC               AS TS_LA_ITEM_2_DESC," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_2                    AS TS_LA_ITEM_2," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_3_DESC               AS TS_LA_ITEM_3_DESC," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_3                    AS TS_LA_ITEM_3," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_4_DESC               AS TS_LA_ITEM_4_DESC," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_4                    AS TS_LA_ITEM_4," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_5_DESC               AS TS_LA_ITEM_5_DESC," +
                    "             TRN_SALARY_DETAILS.TS_LA_ITEM_5                    AS TS_LA_ITEM_5," +
                    "             TRN_SALARY_DETAILS.TS_LA_TOTAL                     AS TS_LA_TOTAL," +
                    "             TRN_SALARY_DETAILS.TS_BALANCE                      AS TS_BALANCE," +
                    "             TRN_SALARY_DETAILS.US_16_EA                        AS US_16_EA," +
                    "             TRN_SALARY_DETAILS.US_16_TE                        AS US_16_TE," +
                    "             TRN_SALARY_DETAILS.US_16_AGGREGATE                 AS US_16_AGGREGATE," +
                    "             TRN_SALARY_DETAILS.INCOME_CHARGEABLE               AS INCOME_CHARGEABLE," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_1_DESC                 AS AIS_ITEM_1_DESC," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_1                      AS AIS_ITEM_1," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_2_DESC                 AS AIS_ITEM_2_DESC," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_2                      AS AIS_ITEM_2," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_3_DESC                 AS AIS_ITEM_3_DESC," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_3                      AS AIS_ITEM_3," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_4_DESC                 AS AIS_ITEM_4_DESC," +
                    "             TRN_SALARY_DETAILS.AIS_ITEM_4                      AS AIS_ITEM_4," +
                    "             TRN_SALARY_DETAILS.AIS_Total                       AS AIS_Total," +
                    "             TRN_SALARY_DETAILS.GROSS_TOTAL_INCOME              AS GROSS_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1_DESC         AS CVIA_SEC80C_ITEM_1_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_1              AS CVIA_SEC80C_ITEM_1," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2_DESC         AS CVIA_SEC80C_ITEM_2_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_2              AS CVIA_SEC80C_ITEM_2," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3_DESC         AS CVIA_SEC80C_ITEM_3_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_3              AS CVIA_SEC80C_ITEM_3," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4_DESC         AS CVIA_SEC80C_ITEM_4_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_4              AS CVIA_SEC80C_ITEM_4," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5_DESC         AS CVIA_SEC80C_ITEM_5_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_5              AS CVIA_SEC80C_ITEM_5," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6_DESC         AS CVIA_SEC80C_ITEM_6_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_ITEM_6              AS CVIA_SEC80C_ITEM_6," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_TOTAL         AS CVIA_SEC80C_GROSS_TOTAL," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80C_DED_TOTAL           AS CVIA_SEC80C_DED_TOTAL," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCC_GROSS_AMOUNT      AS CVIA_SEC80CCC_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCC_DED_AMOUNT        AS CVIA_SEC80CCC_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_GROSS_AMOUNT      AS CVIA_SEC80CCD_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_DED_AMOUNT        AS CVIA_SEC80CCD_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_DED_AMOUNT  AS CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DESC            AS CVIA_OTH_ITEM_1_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_GROSS_AMOUNT    AS CVIA_OTH_ITEM_1_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_QUAL_AMOUNT     AS CVIA_OTH_ITEM_1_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_1_DED_AMOUNT      AS CVIA_OTH_ITEM_1_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DESC            AS CVIA_OTH_ITEM_2_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_GROSS_AMOUNT    AS CVIA_OTH_ITEM_2_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_QUAL_AMOUNT     AS CVIA_OTH_ITEM_2_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_2_DED_AMOUNT      AS CVIA_OTH_ITEM_2_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DESC            AS CVIA_OTH_ITEM_3_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_GROSS_AMOUNT    AS CVIA_OTH_ITEM_3_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_QUAL_AMOUNT     AS CVIA_OTH_ITEM_3_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_3_DED_AMOUNT      AS CVIA_OTH_ITEM_3_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DESC            AS CVIA_OTH_ITEM_4_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_GROSS_AMOUNT    AS CVIA_OTH_ITEM_4_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_QUAL_AMOUNT     AS CVIA_OTH_ITEM_4_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_4_DED_AMOUNT      AS CVIA_OTH_ITEM_4_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DESC            AS CVIA_OTH_ITEM_5_DESC," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_GROSS_AMOUNT    AS CVIA_OTH_ITEM_5_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_QUAL_AMOUNT     AS CVIA_OTH_ITEM_5_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_5_DED_AMOUNT      AS CVIA_OTH_ITEM_5_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_DED_TOTAL              AS CVIA_OTH_DED_TOTAL," +
                    "             TRN_SALARY_DETAILS.CVIA_DED_TOTAL                  AS CVIA_DED_TOTAL," +
                    "             TRN_SALARY_DETAILS.TOTAL_INCOME                    AS TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS.TAX_TOTAL_INCOME                AS TAX_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS.SCHG_TOTAL_INCOME               AS SCHG_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS.ECESS_TOTAL_INCOME              AS ECESS_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS.TAX_PAYABLE_AGGREGATE           AS TAX_PAYABLE_AGGREGATE," +
                    "             TRN_SALARY_DETAILS.US_89_LESS                      AS US_89_LESS," +
                    "             TRN_SALARY_DETAILS.TAX_PAYABLE                     AS TAX_PAYABLE," +
                    "             TRN_SALARY_DETAILS.TOTAL_TDS_DEDUCTED              AS TOTAL_TDS_DEDUCTED," +
                    "             TRN_SALARY_DETAILS.SHORTFALL_TAX                   AS SHORTFALL_TAX," +
                    "             TRN_SALARY_DETAILS.SL_NO                           AS SL_NO," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCF_GROSS_AMOUNT      AS CVIA_SEC80CCF_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCF_DED_AMOUNT        AS CVIA_SEC80CCF_DED_AMOUNT," +
                    //## Anik 2013/04/25                            
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCG_GROSS_AMOUNT      AS CVIA_SEC80CCG_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCG_DED_AMOUNT        AS CVIA_SEC80CCG_DED_AMOUNT, " +
                    //ADDED By Dhrub on 11/11/2013 
                    "             TRN_SALARY_DETAILS.TAXABLE_AMOUNT                  AS TAXABLE_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.REPORTED_TAXABLE_AMOUNT         AS REPORTED_TAXABLE_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT       AS TOTAL_TAX_DEDUCTED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL     AS PREVIOUS_TAX_DEDUCTED_TOTAL, " +
                    "             TRN_SALARY_DETAILS.TAX_DEDUCTED_HIGHER_RATE        AS TAX_DEDUCTED_HIGHER_RATE, " +
                    "             TRN_SALARY_DETAILS.ROUND_OFF_TAXABLE_AMOUNT        AS ROUND_OFF_TAXABLE_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.TOTAL_INCOME_ROUND_OFF          AS TOTAL_INCOME_ROUND_OFF, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_YN                    AS SUPER_ANN_YN, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_NAME                  AS SUPER_ANN_NAME, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_FROM_DATE             AS SUPER_ANN_FROM_DATE, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_TO_DATE               AS SUPER_ANN_TO_DATE, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_AMOUNT                AS SUPER_ANN_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_RATE                  AS SUPER_ANN_RATE, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_TAX                   AS SUPER_ANN_TAX, " +
                    "             TRN_SALARY_DETAILS.SUPER_ANN_INCOME                AS SUPER_ANN_INCOME, " +
                    "             TRN_SALARY_DETAILS.RENT_EXCEEDING_YN               AS RENT_EXCEEDING_YN, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_PAN_COUNT              AS LANDLORD_PAN_COUNT, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_1_PAN                  AS LANDLORD_1_PAN, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_1_NAME                 AS LANDLORD_1_NAME, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_2_PAN                  AS LANDLORD_2_PAN, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_2_NAME                 AS LANDLORD_2_NAME, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_3_PAN                  AS LANDLORD_3_PAN, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_3_NAME                 AS LANDLORD_3_NAME, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_4_PAN                  AS LANDLORD_4_PAN, " +
                    "             TRN_SALARY_DETAILS.LANDLORD_4_NAME                 AS LANDLORD_4_NAME, " +
                    "             TRN_SALARY_DETAILS.INTEREST_PAID_TO_LENDER         AS INTEREST_PAID_TO_LENDER, " +
                    "             TRN_SALARY_DETAILS.LENDER_PAN_COUNT                AS LENDER_PAN_COUNT, " +
                    "             TRN_SALARY_DETAILS.LENDER_1_PAN                    AS LENDER_1_PAN, " +
                    "             TRN_SALARY_DETAILS.LENDER_1_NAME                   AS LENDER_1_NAME, " +
                    "             TRN_SALARY_DETAILS.LENDER_2_PAN                    AS LENDER_2_PAN, " +
                    "             TRN_SALARY_DETAILS.LENDER_2_NAME                   AS LENDER_2_NAME, " +
                    "             TRN_SALARY_DETAILS.LENDER_3_PAN                    AS LENDER_3_PAN, " +
                    "             TRN_SALARY_DETAILS.LENDER_3_NAME                   AS LENDER_3_NAME, " +
                    "             TRN_SALARY_DETAILS.LENDER_4_PAN                    AS LENDER_4_PAN, " +
                    "             TRN_SALARY_DETAILS.LENDER_4_NAME                   AS LENDER_4_NAME, " +
                    "             TRN_SALARY_DETAILS.US_16_IA                        AS US_16_IA, " +
                    //-- 18-19 ON WARDS
                    "             TRN_SALARY_DETAILS.SEC10_5_AMOUNT                        AS SEC10_5_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SEC10_10_AMOUNT                        AS SEC10_10_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SEC10_10A_AMOUNT                        AS SEC10_10A_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SEC10_10AA_AMOUNT                        AS SEC10_10AA_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SEC10_13A_AMOUNT                        AS SEC10_13A_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.SEC10_OTHRS_TOTAL_AMT                     AS SEC10_OTHRS_TOTAL_AMT, " +
                    //"             TRN_SALARY_DETAILS.SEC192_2B_INCOME_HOUSE_PROPERTY_AMT       AS SEC192_2B_INCOME_HOUSE_PROPERTY_AMT, " +
                    //"             TRN_SALARY_DETAILS.SEC192_2B_INCOME_OTHER_SOURCES_AMT        AS SEC192_2B_INCOME_OTHER_SOURCES_AMT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_SEC80C_GROSS_AMOUNT                  AS CVIA_SEC80C_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_SEC80C_DEDUCTIBLE_AMOUNT             AS CVIA_SEC80C_DEDUCTIBLE_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_TOTAL_80C_80CCC_80CCD_1_GROSS_AMOUNT AS CVIA_TOTAL_80C_80CCC_80CCD_1_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT   AS CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_GROSS_AMOUNT             AS CVIA_SEC80CCD_1B_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_1B_DED_AMOUNT               AS CVIA_SEC80CCD_1B_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_GROSS_AMOUNT              AS CVIA_SEC80CCD_2_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCD_2_DED_AMOUNT                AS CVIA_SEC80CCD_2_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80D_GROSS_AMOUNT                  AS CVIA_SEC80D_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80D_DED_AMOUNT                    AS CVIA_SEC80D_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80E_GROSS_AMOUNT                  AS CVIA_SEC80E_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80E_DED_AMOUNT                    AS CVIA_SEC80E_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80G_GROSS_AMOUNT                  AS CVIA_SEC80G_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80G_QUAL_AMOUNT                   AS CVIA_SEC80G_QUAL_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80G_DED_AMOUNT                    AS CVIA_SEC80G_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80TTA_GROSS_AMOUNT                AS CVIA_SEC80TTA_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80TTA_QUAL_AMOUNT                 AS CVIA_SEC80TTA_QUAL_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80TTA_DED_AMOUNT                  AS CVIA_SEC80TTA_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS1_GROSS_AMOUNT                  AS CVIA_OTHRS1_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS1_QUAL_AMOUNT                   AS CVIA_OTHRS1_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS1_DED_AMOUNT                    AS CVIA_OTHRS1_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS1_DESC                          AS CVIA_OTHRS1_DESC, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS2_GROSS_AMOUNT                  AS CVIA_OTHRS2_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS2_QUAL_AMOUNT                   AS CVIA_OTHRS2_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS2_DED_AMOUNT                    AS CVIA_OTHRS2_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS2_DESC                          AS CVIA_OTHRS2_DESC, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS3_GROSS_AMOUNT                  AS CVIA_OTHRS3_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS3_QUAL_AMOUNT                   AS CVIA_OTHRS3_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS3_DED_AMOUNT                    AS CVIA_OTHRS3_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS3_DESC                          AS CVIA_OTHRS3_DESC, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS4_GROSS_AMOUNT                  AS CVIA_OTHRS4_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS4_QUAL_AMOUNT                   AS CVIA_OTHRS4_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS4_DED_AMOUNT                    AS CVIA_OTHRS4_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS4_DESC                          AS CVIA_OTHRS4_DESC, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS5_GROSS_AMOUNT                  AS CVIA_OTHRS5_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS5_QUAL_AMOUNT                   AS CVIA_OTHRS5_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS5_DED_AMOUNT                    AS CVIA_OTHRS5_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS5_DESC                          AS CVIA_OTHRS5_DESC, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS6_GROSS_AMOUNT                  AS CVIA_OTHRS6_GROSS_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS6_QUAL_AMOUNT                   AS CVIA_OTHRS6_QUAL_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS6_DED_AMOUNT                    AS CVIA_OTHRS6_DED_AMOUNT, " +
                    //"             TRN_SALARY_DETAILS.CVIA_OTHRS6_DESC                          AS CVIA_OTHRS6_DESC, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_GROSS_AMOUNT              AS CVIA_OTH_ITEM_6_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_QUAL_AMOUNT               AS CVIA_OTH_ITEM_6_QUAL_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DED_AMOUNT                AS CVIA_OTH_ITEM_6_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_ITEM_6_DESC                      AS CVIA_OTH_ITEM_6_DESC, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_GROSS_TOTAL                      AS CVIA_OTH_GROSS_TOTAL, " +
                    "             TRN_SALARY_DETAILS.CVIA_OTH_QUAL_TOTAL                       AS CVIA_OTH_QUAL_TOTAL, " +
                    "             TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT                      AS REBATE_US_87A_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.FORM16B_NEW_FORMAT_18_19                  AS FORM16B_NEW_FORMAT_18_19, " +
                    "             TRN_SALARY_DETAILS.TAX_TOTAL_INCOME_B4_REBATE                AS TAX_TOTAL_INCOME_B4_REBATE, " +
                    "             TRN_SALARY_DETAILS.SEC10_TOTAL_AMOUNT                        AS SEC10_TOTAL_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SECTION_115BAC_FLAG                       AS SECTION_115BAC_FLAG, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCH_GROSS_AMOUNT                AS CVIA_SEC80CCH_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCH_DED_AMOUNT                  AS CVIA_SEC80CCH_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_GROSS_AMOUNT              AS CVIA_SEC80CCH_1_GROSS_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.CVIA_SEC80CCH_1_DED_AMOUNT                AS CVIA_SEC80CCH_1_DED_AMOUNT, " +
                    "             TRN_SALARY_DETAILS.SEC10_14_AMOUNT                           AS SEC10_14_AMOUNT " +
                    "     FROM    TRN_SALARY_DETAILS," +
                    "             MST_EMPLOYEE " +
                    "     WHERE   TRN_SALARY_DETAILS.EMPLOYEE_ID       = MST_EMPLOYEE.EMPLOYEE_ID " +
                    "     AND     TRN_SALARY_DETAILS.SALARY_DETAILS_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    //blnShowRecord = true;
                    //lngSearchId = Id;

                    txtEmployeeID.Text = Convert.ToString(drdShowRecord["EMPLOYEE_ID"]);

                    //blnShowHelp = false;
                    txtEmployeeName.Text = Convert.ToString(drdShowRecord["EMPLOYEE_NAME"]);
                    //blnShowHelp = true;

                    //blnShowPANHelp = false;
                    txtEmployeePAN.Text = Convert.ToString(drdShowRecord["EMPLOYEE_PAN"]);
                    //blnShowPANHelp = true;

                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                        if (Convert.ToString(drdShowRecord["EMPLOYEE_REF"]) != "")
                            txtEmpRefNo.Text = string.Format("{0:0000000000}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["EMPLOYEE_REF"])));

                    cmbEmployeeCategory.Text = Convert.ToString(drdShowRecord["EMPLOYEE_CATEGORY"]);
                    mskEmployeeFromDate.Text = Convert.ToString(drdShowRecord["FROM_DATE"]);
                    mskEmployeeToDate.Text = Convert.ToString(drdShowRecord["TO_DATE"]);
                    //
                    if (Convert.ToString(drdShowRecord["ENTRY_MODE"]) == "0")
                    {
                        rbnMandatoryOptions.Enabled = true;
                        rbnMandatoryOptions.Checked = true;
                    }
                    else if (Convert.ToString(drdShowRecord["ENTRY_MODE"]) == "1")
                    {
                        rbnDetailOptions.Checked = true;
                        rbnMandatoryOptions.Enabled = false;
                    }
                    //
                    //-- 2020/07/02
                    if (Convert.ToString(drdShowRecord["SECTION_115BAC_FLAG"]) == "0")
                    {
                        chkTaxation115BAC.Checked = false;
                    }
                    else if (Convert.ToString(drdShowRecord["SECTION_115BAC_FLAG"]) == "1")
                    {
                        chkTaxation115BAC.Checked = true;
                    }
                    //--
                    txtGSSec17_1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_GS_SEC_17_1"])));
                    txtGSSec17_2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_GS_SEC_17_2"])));
                    txtGSSec17_3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_GS_SEC_17_3"])));
                    lblTotalGS.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_GS_TOTAL"])));
                    txtAllowanceSecDesc1.Text = Convert.ToString(drdShowRecord["TS_LA_ITEM_1_DESC"]);
                    txtAllowanceSecAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_ITEM_1"])));
                    txtAllowanceSecDesc2.Text = Convert.ToString(drdShowRecord["TS_LA_ITEM_2_DESC"]);
                    txtAllowanceSecAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_ITEM_2"])));
                    txtAllowanceSecDesc3.Text = Convert.ToString(drdShowRecord["TS_LA_ITEM_3_DESC"]);
                    txtAllowanceSecAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_ITEM_3"])));
                    txtAllowanceSecDesc4.Text = Convert.ToString(drdShowRecord["TS_LA_ITEM_4_DESC"]);
                    txtAllowanceSecAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_ITEM_4"])));
                    txtAllowanceSecDesc5.Text = Convert.ToString(drdShowRecord["TS_LA_ITEM_5_DESC"]);
                    txtAllowanceSecAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_ITEM_5"])));
                    lblAllowanceSecAmtTotal.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_TOTAL"])));

                    txtTotalSalaryBalance.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_BALANCE"])));
                    txtTotalSalaryBalanceEdit.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_BALANCE"])));

                    txtDedEntAllowance.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_EA"])));
                    txtDedTaxEmployment.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_TE"])));
                    lblTotalDeductions.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_AGGREGATE"])));
                    lblIncomeChargeable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["INCOME_CHARGEABLE"])));
                    txtOtherIncomeDesc1.Text = Convert.ToString(drdShowRecord["AIS_ITEM_1_DESC"]);
                    txtOtherIncomeAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_1"])));
                    txtOtherIncomeDesc2.Text = Convert.ToString(drdShowRecord["AIS_ITEM_2_DESC"]);
                    txtOtherIncomeAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_2"])));
                    txtOtherIncomeDesc3.Text = Convert.ToString(drdShowRecord["AIS_ITEM_3_DESC"]);
                    txtOtherIncomeAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_3"])));
                    txtOtherIncomeDesc4.Text = Convert.ToString(drdShowRecord["AIS_ITEM_4_DESC"]);
                    txtOtherIncomeAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_4"])));

                    txtTotalOtherIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_Total"])));
                    txtTotalOtherIncomeEdit.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_Total"])));

                    lblGrossTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["GROSS_TOTAL_INCOME"])));
                    txtSec80CDesc1.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_1_DESC"]);
                    txtSec80CAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_1"])));
                    txtSec80CDesc2.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_2_DESC"]);
                    txtSec80CAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_2"])));
                    txtSec80CDesc3.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_3_DESC"]);
                    txtSec80CAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_3"])));
                    txtSec80CDesc4.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_4_DESC"]);
                    txtSec80CAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_4"])));
                    txtSec80CDesc5.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_5_DESC"]);
                    txtSec80CAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_5"])));
                    txtSec80CDesc6.Text = Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_6_DESC"]);
                    txtSec80CAmt6.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_ITEM_6"])));
                    lblGS80C.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_GROSS_TOTAL"])));
                    txtDedTotal80C.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_DED_TOTAL"])));
                    txtSec80CCCGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_GROSS_AMOUNT"])));
                    txtSec80CCCDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_DED_AMOUNT"])));
                    txtSec80CCDGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_GROSS_AMOUNT"])));
                    txtSec80CCDDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_DED_AMOUNT"])));

                    txtTotalDeductibleAmount80CCE.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCE_TOTAL_DED_AMOUNT"])));
                    txtTotalDeductibleAmount80CCEEdit.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCE_TOTAL_DED_AMOUNT"])));

                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                    {
                        txtSecChVIAOthersDesc1.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_DESC"]);
                        txtSecChVIAOthersGrossAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_GROSS_AMOUNT"])));
                        txtSecChVIAOthersQualifyingAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_QUAL_AMOUNT"])));
                        txtSecChVIAOthersDeductibleAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_DED_AMOUNT"])));
                        txtSecChVIAOthersDesc2.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_DESC"]);
                        txtSecChVIAOthersGrossAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_GROSS_AMOUNT"])));
                        txtSecChVIAOthersQualifyingAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_QUAL_AMOUNT"])));
                        txtSecChVIAOthersDeductibleAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_DED_AMOUNT"])));
                        txtSecChVIAOthersDesc3.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_DESC"]);
                        txtSecChVIAOthersGrossAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_GROSS_AMOUNT"])));
                        txtSecChVIAOthersQualifyingAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_QUAL_AMOUNT"])));
                        txtSecChVIAOthersDeductibleAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_DED_AMOUNT"])));
                        txtSecChVIAOthersDesc4.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_DESC"]);
                        txtSecChVIAOthersGrossAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_GROSS_AMOUNT"])));
                        txtSecChVIAOthersQualifyingAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_QUAL_AMOUNT"])));
                        txtSecChVIAOthersDeductibleAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_DED_AMOUNT"])));
                        txtSecChVIAOthersDesc5.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_DESC"]);
                        txtSecChVIAOthersGrossAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_GROSS_AMOUNT"])));
                        txtSecChVIAOthersQualifyingAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_QUAL_AMOUNT"])));
                        txtSecChVIAOthersDeductibleAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_DED_AMOUNT"])));
                    }
                    else
                    {
                        txtOSDesc1.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_DESC"]);
                        txtOSGrossAmount1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_GROSS_AMOUNT"])));
                        txtOSQualifyingAmount1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_QUAL_AMOUNT"])));
                        txtOSDeductibleAmount1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_1_DED_AMOUNT"])));
                        txtOSDesc2.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_DESC"]);
                        txtOSGrossAmount2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_GROSS_AMOUNT"])));
                        txtOSQualifyingAmount2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_QUAL_AMOUNT"])));
                        txtOSDeductibleAmount2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_2_DED_AMOUNT"])));
                        txtOSDesc3.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_DESC"]);
                        txtOSGrossAmount3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_GROSS_AMOUNT"])));
                        txtOSQualifyingAmount3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_QUAL_AMOUNT"])));
                        txtOSDeductibleAmount3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_3_DED_AMOUNT"])));
                        txtOSDesc4.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_DESC"]);
                        txtOSGrossAmount4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_GROSS_AMOUNT"])));
                        txtOSQualifyingAmount4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_QUAL_AMOUNT"])));
                        txtOSDeductibleAmount4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_4_DED_AMOUNT"])));
                        txtOSDesc5.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_DESC"]);
                        txtOSGrossAmount5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_GROSS_AMOUNT"])));
                        txtOSQualifyingAmount5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_QUAL_AMOUNT"])));
                        txtOSDeductibleAmount5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_5_DED_AMOUNT"])));
                    }
                    //
                    txtTotalDeductibleAmountOS.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_DED_TOTAL"])));
                    txtTotalDeductibleAmountOSEdit.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_DED_TOTAL"])));

                    lblTotalUCVIA.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_DED_TOTAL"])));
                    lblTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_INCOME"])));
                    //txtTaxTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_TOTAL_INCOME"]))); //-- 2019/05/14
                    lblTaxDeductingRebate.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_TOTAL_INCOME"])));
                    txtSurcharge.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SCHG_TOTAL_INCOME"])));
                    txtEducationCess.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["ECESS_TOTAL_INCOME"])));
                    lblGrossTaxPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_PAYABLE_AGGREGATE"])));
                    txtReliefUS89.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_89_LESS"])));
                    lblTaxPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_PAYABLE"])));
                    txtTotalTDSDedcuted.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_TDS_DEDUCTED"])));
                    lblShortfall.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SHORTFALL_TAX"])));

                    txtSalaryDetailSrlNo.Text = Convert.ToString(drdShowRecord["SL_NO"]);

                    //blnShowHelp = true;
                    //## ANIK 2013/04/25
                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2010_11ID ||
                    TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2011_12ID)
                    {
                        txtSec80CCFGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCF_GROSS_AMOUNT"])));
                        txtSec80CCFDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCF_DED_AMOUNT"])));
                    }
                    else //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2012_13ID)
                    {
                        txtSec80CCFGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCG_GROSS_AMOUNT"])));
                        txtSec80CCFDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCG_DED_AMOUNT"])));
                    }
                    //##
                    //--------
                    //Added By Dhrub on 11/11/2013 []
                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                    {
                        txtTaxableAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAXABLE_AMOUNT"])));
                        txtReportedTaxableAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["REPORTED_TAXABLE_AMOUNT"])));
                        txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_TAX_DEDUCTED_AMOUNT"])));
                        txtPreviousTaxDeductedAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["PREVIOUS_TAX_DEDUCTED_TOTAL"])));
                        if (Convert.ToInt32(drdShowRecord["TAX_DEDUCTED_HIGHER_RATE"]) == 0)
                        {
                            cmbHigherRate.Text = "No";
                        }
                        else
                        {
                            cmbHigherRate.Text = "YES";
                        }

                        //blnShowRecord = false;

                    }
                    //blnShowRecord = false; //-- ANIK @ 2015-04-16
                    ////--
                    //RoundOff = true;
                    //-- ANIK @ 2015-11-18
                    //if (Convert.ToString(drdShowRecord["ROUND_OFF_TAXABLE_AMOUNT"]) == "1")
                    //{
                    //    chkRoundOff.Checked = true;
                        lblTotalIncomeRounded.Text = Convert.ToString(drdShowRecord["TOTAL_INCOME_ROUND_OFF"]);
                    //}
                    //else
                    //{
                    //    chkRoundOff.Checked = false;
                    //    lblTotalIncomeRounded.Text = "0";
                    //    //lblTotalIncomeRounded.Text = Convert.ToString(drdShowRecord["TOTAL_INCOME"]);
                    //}
                    //--
                    //blnVertical = false;
                    //
                    if (Convert.ToString(drdShowRecord["SUPER_ANN_YN"]) == "Y")
                        cmbYNSuperannuationFund.Text = T_YES_NO.YES;
                    else
                        cmbYNSuperannuationFund.Text = T_YES_NO.NO;
                    //
                    txtNameSuperannuationFund.Text = Convert.ToString(drdShowRecord["SUPER_ANN_NAME"]);
                    mskFromSuperannuationFund.Text = Convert.ToString(drdShowRecord["SUPER_ANN_FROM_DATE"]);
                    mskToSuperannuationFund.Text = Convert.ToString(drdShowRecord["SUPER_ANN_TO_DATE"]);
                    txtAmountSuperannuationFund.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SUPER_ANN_AMOUNT"])));
                    txtRateSuperannuationFund.Text = string.Format("{0:0.0000}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SUPER_ANN_RATE"])));
                    txtTaxSuperannuationFund.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SUPER_ANN_TAX"])));
                    txtTotalIncomeSuperannuationFund.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SUPER_ANN_INCOME"])));
                    lblTotalAmountOfTAX.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_TDS_DEDUCTED"])) + cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SUPER_ANN_TAX"])));
                    //--
                    if (Convert.ToString(drdShowRecord["RENT_EXCEEDING_YN"]) == "Y")
                        cmbRentYN.Text = T_YES_NO.YES;
                    else
                        cmbRentYN.Text = T_YES_NO.NO;
                    //
                    txtLandlordPan1.Text = Convert.ToString(drdShowRecord["LANDLORD_1_PAN"]);
                    txtLandlordName1.Text = Convert.ToString(drdShowRecord["LANDLORD_1_NAME"]);
                    txtLandlordPan2.Text = Convert.ToString(drdShowRecord["LANDLORD_2_PAN"]);
                    txtLandlordName2.Text = Convert.ToString(drdShowRecord["LANDLORD_2_NAME"]);
                    txtLandlordPan3.Text = Convert.ToString(drdShowRecord["LANDLORD_3_PAN"]);
                    txtLandlordName3.Text = Convert.ToString(drdShowRecord["LANDLORD_3_NAME"]);
                    txtLandlordPan4.Text = Convert.ToString(drdShowRecord["LANDLORD_4_PAN"]);
                    txtLandlordName4.Text = Convert.ToString(drdShowRecord["LANDLORD_4_NAME"]);
                    //--
                    if (Convert.ToString(drdShowRecord["INTEREST_PAID_TO_LENDER"]) == "Y")
                        cmbIncomeYN.Text = T_YES_NO.YES;
                    else
                        cmbIncomeYN.Text = T_YES_NO.NO;
                    //
                    txtLenderPan1.Text = Convert.ToString(drdShowRecord["LENDER_1_PAN"]);
                    txtLenderName1.Text = Convert.ToString(drdShowRecord["LENDER_1_NAME"]);
                    txtLenderPan2.Text = Convert.ToString(drdShowRecord["LENDER_2_PAN"]);
                    txtLenderName2.Text = Convert.ToString(drdShowRecord["LENDER_2_NAME"]);
                    txtLenderPan3.Text = Convert.ToString(drdShowRecord["LENDER_3_PAN"]);
                    txtLenderName3.Text = Convert.ToString(drdShowRecord["LENDER_3_NAME"]);
                    txtLenderPan4.Text = Convert.ToString(drdShowRecord["LENDER_4_PAN"]);
                    txtLenderName4.Text = Convert.ToString(drdShowRecord["LENDER_4_NAME"]);
                    //--
                    //--
                    txtDedTaxSection16ia.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_IA"])));
                    //--
                    //-- 18-19 ON WARDS 2019/04/23 
                    txtSec10TravelConcession.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_5_AMOUNT"])));
                    txtSec10DeathCumRetirement.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_10_AMOUNT"])));
                    txtSec10CommutedValuePension.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_10A_AMOUNT"])));
                    txtSec10CashEquivalent.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_10AA_AMOUNT"])));
                    txtSec10HRA.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_13A_AMOUNT"])));
                    //
                    //txtSec10OtherExemption.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_TOTAL_AMOUNT"])));  //-- Added By Abhishek Dey On 06/06/2019 --
                    //txtAllowanceSecAmtTotal.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_TOTAL"])));  //-- Commented By Abhishek Dey On 06/06/2019 --
                    //lblAllowanceSecAmtTotal.Text      = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_OTHRS_TOTAL_AMT"])));
                    //txtAllowanceSecAmtTotal.Text      = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_OTHRS_TOTAL_AMT"])));
                    txtSec10OtherExemption.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_LA_TOTAL"]))); //-- 2019/06/20
                    txtOtherIncomeDesc1.Text = Convert.ToString(drdShowRecord["AIS_ITEM_1_DESC"]);
                    txtOtherIncomeAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_1"])));
                    txtOtherIncomeDesc2.Text = Convert.ToString(drdShowRecord["AIS_ITEM_2_DESC"]);
                    txtOtherIncomeAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["AIS_ITEM_2"])));
                    //
                    txtTotalSalaryBalance.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_BALANCE"])));
                    txtTotalSalaryBalanceEdit.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_BALANCE"])));

                    //txtTotalUs10.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["XXXXX"])));

                    txtSec80C1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_GROSS_TOTAL"])));
                    txtSec80C1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_DED_TOTAL"])));
                    txtSec80CCC1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_GROSS_AMOUNT"])));
                    txtSec80CCC1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_DED_AMOUNT"])));
                    txtSec80CCD11819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_GROSS_AMOUNT"])));
                    txtSec80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_DED_AMOUNT"])));
                    txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT"])));
                    txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCE_TOTAL_DED_AMOUNT"])));
                    txtSec80CCD1B1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_1B_GROSS_AMOUNT"])));
                    txtSec80CCD1B1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_1B_DED_AMOUNT"])));
                    txtSec80CCD21819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_2_GROSS_AMOUNT"])));
                    txtSec80CCD21819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_2_DED_AMOUNT"])));
                    txtSec80D1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80D_GROSS_AMOUNT"])));
                    txtSec80D1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80D_DED_AMOUNT"])));
                    txtSec80E1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80E_GROSS_AMOUNT"])));
                    txtSec80E1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80E_DED_AMOUNT"])));
                    txtSec80G1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_GROSS_AMOUNT"])));
                    txtSec80G1819QualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_QUAL_AMOUNT"])));
                    txtSec80G1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_DED_AMOUNT"])));
                    txtSec80TTA1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTA_GROSS_AMOUNT"])));
                    txtSec80TTA1819QualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTA_QUAL_AMOUNT"])));
                    txtSec80TTA1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTA_DED_AMOUNT"])));
                    //txtSecChVIAOthersGrossAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS1_GROSS_AMOUNT"])));
                    //txtSecChVIAOthersGrossAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS2_GROSS_AMOUNT"])));
                    //txtSecChVIAOthersGrossAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS3_GROSS_AMOUNT"])));
                    //txtSecChVIAOthersGrossAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS4_GROSS_AMOUNT"])));
                    //txtSecChVIAOthersGrossAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS5_GROSS_AMOUNT"])));
                    txtSecChVIAOthersGrossAmt6.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_6_GROSS_AMOUNT"])));
                    //txtSecChVIAOthersQualifyingAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS1_QUAL_AMOUNT"])));
                    //txtSecChVIAOthersQualifyingAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS2_QUAL_AMOUNT"])));
                    //txtSecChVIAOthersQualifyingAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS3_QUAL_AMOUNT"])));
                    //txtSecChVIAOthersQualifyingAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS4_QUAL_AMOUNT"])));
                    //txtSecChVIAOthersQualifyingAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS5_QUAL_AMOUNT"])));
                    txtSecChVIAOthersQualifyingAmt6.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_6_QUAL_AMOUNT"])));
                    //txtSecChVIAOthersDeductibleAmt1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS1_DED_AMOUNT"])));
                    //txtSecChVIAOthersDeductibleAmt2.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS2_DED_AMOUNT"])));
                    //txtSecChVIAOthersDeductibleAmt3.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS3_DED_AMOUNT"])));
                    //txtSecChVIAOthersDeductibleAmt4.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS4_DED_AMOUNT"])));
                    //txtSecChVIAOthersDeductibleAmt5.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHRS5_DED_AMOUNT"])));
                    txtSecChVIAOthersDeductibleAmt6.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_6_DED_AMOUNT"])));
                    //txtSecChVIAOthersDesc1.Text = Convert.ToString(drdShowRecord["CVIA_OTHRS1_DESC"]);
                    //txtSecChVIAOthersDesc2.Text = Convert.ToString(drdShowRecord["CVIA_OTHRS2_DESC"]);
                    //txtSecChVIAOthersDesc3.Text = Convert.ToString(drdShowRecord["CVIA_OTHRS3_DESC"]);
                    //txtSecChVIAOthersDesc4.Text = Convert.ToString(drdShowRecord["CVIA_OTHRS4_DESC"]);
                    //txtSecChVIAOthersDesc5.Text = Convert.ToString(drdShowRecord["CVIA_OTHRS5_DESC"]);
                    txtSecChVIAOthersDesc6.Text = Convert.ToString(drdShowRecord["CVIA_OTH_ITEM_6_DESC"]);
                    lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_DED_TOTAL"])));
                    txtSecChVIATotalOthersGrossAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_GROSS_TOTAL"])));
                    txtSecChVIATotalOthersQualifyingAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_QUAL_TOTAL"])));
                    txtSecChVIATotalOthersDeductibleAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTH_DED_TOTAL"])));
                    lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_DED_TOTAL"])));
                    txtRebate.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["REBATE_US_87A_AMOUNT"])));
                    //
                    txtTaxTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_TOTAL_INCOME_B4_REBATE"]))); //-- 2019/05/14
                    //-- 2020/05/19
                    lblTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_INCOME"])));
                    //-- FVU 8.3 2023/08/17
                    txtSec80CCH1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCH_GROSS_AMOUNT"])));
                    txtSec80CCH1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCH_DED_AMOUNT"])));
                    txtSec80CCH11819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCH_1_GROSS_AMOUNT"])));
                    txtSec80CCH11819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCH_1_DED_AMOUNT"])));
                    txtSec1014.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SEC10_14_AMOUNT"])));
                    //--
                    //if (cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["FORM16B_NEW_FORMAT_18_19"])) > 0)
                    //    FORM_DESIGN(true);
                    //else
                    //    FORM_DESIGN(false);
                    ////--
                    //blnVertical = true;
                    //RoundOff = false;
                    //--
                    //--------
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //cmbState.Text = strStateName;
                    //cmbDistrict.Text = strDistrictName;

                    //txtEmployeeName.Select();
                    return true;
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                //lngSearchId = 0;
                ////-----------------------------------------------------------
                //if (strCheckFields == "")
                //    strSQL = strQuery + "order by " + strOrderBy;
                //else
                //    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                ////-----------------------------------------------------------
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

    }
}
