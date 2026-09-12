
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnFVUImport
Version			: 1.0
Start Date		: 30-12-2010
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

    //~~~~ System Namespaces ~~~~
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

    //using Microsoft.Office.Interop.Access;

    //using System.Runtime.InteropServices;
    using System.Data.OleDb;
    //~~~~ User Namespaces ~~~~
    using TDSMAN.FormTrn;
    using TDSMAN.FormRpt;
    using TDSMAN.Classes;
    //--
    using Excel = Microsoft.Office.Interop.Excel.Worksheet;
    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrnCalcMonthlyTDS
{
    #region STRUCTURE
    
    #region T_GET_SALARY_DATA_FROM_EXCEL_18_19

    public enum T_GET_SALARY_DATA_FROM_EXCEL_18_19
    {
        EMPLOYEE_SERIAL_NO = 0,
        EMPLOYEE_PAN = 1,
        EMPLOYEE_NAME = 2,
        EMPLOYEE_CATEGORY = 3,

        TS_GS_SEC_17_1 = 4,
        TS_GS_SEC_17_2 = 5,
        TS_GS_SEC_17_3 = 6,

        TOTAL_SALARY = 7,

        SEC10_5_AMOUNT = 8,
        SEC10_10_AMOUNT = 9,
        SEC10_10A_AMOUNT = 10,
        SEC10_10AA_AMOUNT = 11,
        SEC10_13A_AMOUNT = 12,
        SEC10_OTHERS_AMOUNT = 13,
        BALANCE_AMOUNT = 14,
        //
        GROSS_DEDUCTION_16ii = 15,
        GROSS_DEDUCTION_16iii = 16,
        GROSS_DEDUCTION_16ia = 17,
        GROSS_TOTAL_DEDUCTION_16iii = 18,
        INCOME_CHARGEABLE_SALARIES = 19,
        //
        INCOME_OR_LOSS_HOUSE_PROPERTY = 20,
        INCOME_OTHER_SOURCES = 21,
        //INCOME_OTHER_SALARY = 23,
        //
        GROSS_TOTAL_INCOME = 22,
        //
        SEC_80C = 23,
        SEC_80CCC = 24,
        SEC_80CCD_1 = 25,
        SEC_80C_CCC_CCD_1 = 26,
        SEC_80CCD_1B = 27,
        SEC_80CCD_2 = 28,
        SEC_80D = 29,
        SEC_80E = 30,
        SEC_80G = 31,
        SEC_80TTA = 32,
        ////
        DED_CHVIA_OTHER_SECTIONS = 33,
        GROSS_TOTAL_DED_CHVIA = 34,
        PREV_EMPLOYER_SALARY = 35
        //
    }
    #endregion

    #endregion

    public partial class TrnCalcTotalTaxableIncome : TDSMAN.FormGen.GenForm
    {

        #region System Generated Code
        public TrnCalcTotalTaxableIncome()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ExcelService ExcelService = new ExcelService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        //string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //Microsoft.Office.Interop.Access.Application Access = new Microsoft.Office.Interop.Access.Application();
        //-----------------------------------------------------------------------
        int intCaratPosition = 0;
        string strFVUPath = "";
        long lngBasicInfoID = 0;
        string strErrorWorksheetName = "Validation Error";
        //-----------------------------------------------------------------------
        string strNewDeducteeWorkSheetName = "New Employees Found";
        string strEmployeesModifiedWorkSheetName = "Employees to be modified";
        string strWorkingSheetName = "Annual Salary";
        //
        long lngRowCount;
        long lngNewDeducteesCreated = 0;
        long lngEmployeesToBeModified = 0;
        //
        string strErrorMessage = "";

        //string strTemporaryfileChallanPath = "";
        //string strTemporaryfileDeducteePath = "";
        //string strTemporaryfileSalaryPath = "";
        //string strTemporaryfileForm16SalaryPath = "";
        string strTemporaryfileCalcTotalTaxableIncome = "";

        OleDbDataAdapter myCommand;
        string strConnectionString = "";
        OleDbConnection con;
        //
        bool blnOpenTabPage = false;

        //ADDED BY DHRUB FOR ADDING NEW FIELDS INTO TEXT
        //      FOR SALARY DETAILS 2013-14  AND ONWARDS 
        int intInitialColumnAfterZ = 0;
        //int intInitialColumnNumbering = 1;
        string strExcelFieldAsciiAfterZ = "";
        string strAAsciiAfterZ = "";
        string strBAsciiAfterZ = "";
        string strCAsciiAfterZ = "";
        string strDAsciiAfterZ = "";
        string strEAsciiAfterZ = "";

        //ADDED BY DHRUB ON 30/12/2013 FOR ADDING THE EXCEL IMPORT VALUES 
        //     FOR INTEREST ALLOCATED AND OTHER INTEREST ALLOCATED 
        string strImportValuesToAllocated = "";

        //Added by Dhrub Mukherjee On 07/01/2014
        //---------------------------------------
        //---------------------------------------
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        ToolTip tllTip = new ToolTip();
        ToolTip ToolTip1 = new ToolTip();

        double dblTotalExcelRecords = 0; //-- 2015/07/31
        #endregion

        #region set ENUM

        #region T_CHALLAN_DETAILS_COLUMN

        public enum T_CHALLAN_DETAILS_COLUMN
        {
            CHALLAN_DETAILS_ID = 0,
            RUNNING_SERIAL_NO_401 = 1,
            RUNNING_SERIAL_NO_401_CELL = 2,
            SECTION_NAME_402 = 3,
            SECTION_NAME_402_CELL = 4,
            TDS_403 = 5,
            TDS_403_CELL = 6,
            SURCHARGE_404 = 7,
            SURCHARGE_404_CELL = 8,
            EDUCATION_CESS_405 = 9,
            EDUCATION_CESS_405_CELL = 10,
            INTEREST_406 = 11,
            INTEREST_406_CELL = 12,
            OTHERS_407 = 13,
            OTHERS_407_CELL = 14,
            TOTAL_TAX_DEPOSITED_408 = 15,
            TOTAL_TAX_DEPOSITED_408_CELL = 16,
            CHEQUE_NO_409 = 17,
            CHEQUE_NO_409_CELL = 18,
            BSR_CODE_410 = 19,
            BSR_CODE_410_CELL = 20,
            DATE_TAX_DEPOSITED_411 = 21,
            DATE_TAX_DEPOSITED_411_CELL = 22,
            TRF_VCH_CHLN_NO_412 = 23,
            TRF_VCH_CHLN_NO_412_CELL = 24,
            BOOK_ENTRY_413 = 25,
            BOOK_ENTRY_413_CELL = 26,
            none
        }
        #endregion

        #region T_GET_CHALLAN_DATA_FROM_EXCEL

        public enum T_GET_CHALLAN_DATA_FROM_EXCEL
        {
            RUNNING_SERIAL_NO_401 = 0,
            SECTION_NAME_402 = 1,
            TDS_403 = 2,
            SURCHARGE_404 = 3,
            EDUCATION_CESS_405 = 4,
            INTEREST_406 = 5,
            OTHERS_407 = 6,
            TOTAL_TAX_DEPOSITED_408 = 7,
            CHEQUE_NO_409 = 8,
            BSR_CODE_410 = 9,
            DATE_TAX_DEPOSITED_411 = 10,
            TRF_VCH_CHLN_NO_412 = 11,
            BOOK_ENTRY_413 = 12,
            FEE = 13,
            MINOR_HEAD = 14,
            TEMP_RUNNING_SERIAL_NO = 15
        }

        #endregion

        #region T_DEDUCTEE_DETAILS_COLUMN

        public enum T_DEDUCTEE_DETAILS_COLUMN
        {
            DEDUCTEE_DETAILS_ID = 0,
            DEDUCTEE_SERIAL_NO_414 = 1,
            DEDUCTEE_SERIAL_NO_414_CELL = 2,
            CHALLAN_SERIAL_NO_401 = 3,
            CHALLAN_SERIAL_NO_401_CELL = 4,
            DEDUCTEE_CODE_415 = 5,
            DEDUCTEE_CODE_415_CELL = 6,
            DEDUCTEE_PAN_416 = 7,
            DEDUCTEE_PAN_416_CELL = 8,
            DEDUCTEE_NAME_417 = 9,
            DEDUCTEE_NAME_417_CELL = 10,
            DATE_PAYMENT_418 = 11,
            DATE_PAYMENT_418_CELL = 12,
            AMOUNT_PAID_419 = 13,
            AMOUNT_PAID_419_CELL = 14,
            TDS_421 = 15,
            TDS_421_CELL = 16,
            SURCHARGE_422 = 17,
            SURCHARGE_422_CELL = 18,
            EDUCATION_CESS_423 = 19,
            EDUCATION_CESS_423_CELL = 20,
            TOTAL_TAX_DEDUCTED_424 = 21,
            TOTAL_TAX_DEDUCTED_424_CELL = 22,
            TOTAL_TAX_DEPOSITED_425 = 23,
            TOTAL_TAX_DEPOSITED_425_CELL = 24,
            RATE_427 = 25,
            RATE_427_CELL = 26,
            REASON_428 = 27,
            REASON_428_CELL = 28,
            GROSSING_UP_TOT_VALUE_PUR = 29,
            GROSSING_UP_TOT_VALUE_PUR_CELL = 30
        }

        #endregion

        #region T_GET_DEDUCTEE_DATA_FROM_EXCEL

        public enum T_GET_DEDUCTEE_DATA_FROM_EXCEL
        {
            DEDUCTEE_SERIAL_NO_414 = 0,
            CHALLAN_SERIAL_NO_401 = 1,
            DEDUCTEE_CODE_415 = 2,
            DEDUCTEE_PAN_416 = 3,
            DEDUCTEE_NAME_417 = 4,
            DATE_PAYMENT_418 = 5,
            AMOUNT_PAID_419 = 6,
            TDS_421 = 7,
            SURCHARGE_422 = 8,
            EDUCATION_CESS_423 = 9,
            TOTAL_TAX_DEDUCTED_424 = 10,
            TOTAL_TAX_DEPOSITED_425 = 11,
            RATE_427 = 12,
            REASON_428 = 13,
            RUNNING_SERIAL_NO_ORDER = 14,
            GROSSING_UP_TOT_VALUE_PUR = 15
        }
        #endregion

        #region T_GET_SALARY_DATA_FROM_EXCEL

        public enum T_GET_SALARY_DATA_FROM_EXCEL
        {
            EMPLOYEE_SERIAL_NO = 0,
            EMPLOYEE_PAN = 1,
            EMPLOYEE_NAME = 2,
            EMPLOYEE_CATEGORY = 3,
            PERIOD_FROM_DATE = 4,
            PERIOD_TO_DATE = 5,
            TOTAL_SALARY = 6,
            GROSS_DEDUCTION_16ii = 7,
            GROSS_DEDUCTION_16iii = 8,
            GROSS_DEDUCTION_16ia = 9,
            GROSS_TOTAL_DEDUCTION_16iii = 10,
            INCOME_CHARGEABLE_SALARIES = 11,
            INCOME_OTHER_SALARY = 12,
            GROSS_TOTAL_INCOME = 13,
            DED_CHVIA_80CCE = 14,
            DED_CHVIA_80CCF = 15,
            DED_CHVIA_OTHER_SECTIONS = 16,
            GROSS_TOTAL_DED_CHVIA = 17,
            TOTAL_TAXABLE_INCOME = 18,
            INCOME_TAX_ON_TOTAL_INCOME = 19,
            SURCHARGE = 20,
            EDUCATION_CESS = 21,
            INCOME_TAX_RELIEF = 22,
            NET_TAX_PAYABLE = 23,
            TOTAL_TDS_DEDUCTED = 24,
            SHORTFALL_EXCESS = 25,
            TAXABLE_AMOUNT = 26,
            REPORTED_TAXABLE_AMOUNT = 27,
            TOTAL_TAX_DEDUCTED_AMOUNT = 28,
            PREVIOUS_TAX_DEDUCTED_TOTAL = 29,
            TAX_DEDUCTED_HIGHER_RATE = 30,
            //
            TDS_SUPERANN = 31,
            CONTRIBUTIONS_SUPERANN_YN = 32,
            NAME_SUPERANN = 33,
            SUPERANN_FROM_DATE = 34,
            SUPERANN_TO_DATE = 35,
            AMT_REPAID = 36,
            RATE_DED = 37,
            AMT_TAX = 38,
            GROSS_TOTAL_INC = 39,
            //
            RENT_PAYMENT_YN = 40,
            PAN_LANDLORD1 = 41,
            NAME_LANDLORD1 = 42,
            PAN_LANDLORD2 = 43,
            NAME_LANDLORD2 = 44,
            PAN_LANDLORD3 = 45,
            NAME_LANDLORD3 = 46,
            PAN_LANDLORD4 = 47,
            NAME_LANDLORD4 = 48,
            INT_PAID_YN = 49,
            PAN_LENDER1 = 50,
            NAME_LENDER1 = 51,
            PAN_LENDER2 = 52,
            NAME_LENDER2 = 53,
            PAN_LENDER3 = 54,
            NAME_LENDER3 = 55,
            PAN_LENDER4 = 56,
            NAME_LENDER4 = 57
        }
        #endregion

        #region T_GET_SALARY_DATA_FROM_EXCEL_18_19

        public enum T_GET_SALARY_DATA_FROM_EXCEL_18_19
        {
            EMPLOYEE_SERIAL_NO = 0,
            EMPLOYEE_PAN = 1,
            EMPLOYEE_NAME = 2,
            EMPLOYEE_CATEGORY = 3,

            TS_GS_SEC_17_1 = 4,
            TS_GS_SEC_17_2 = 5,
            TS_GS_SEC_17_3 = 6,

            TOTAL_SALARY = 7,

            SEC10_5_AMOUNT = 8,
            SEC10_10_AMOUNT = 9,
            SEC10_10A_AMOUNT = 10,
            SEC10_10AA_AMOUNT = 11,
            SEC10_13A_AMOUNT = 12,
            SEC10_OTHERS_AMOUNT = 13,
            BALANCE_AMOUNT = 14,
            //
            GROSS_DEDUCTION_16ii = 15,
            GROSS_DEDUCTION_16iii = 16,
            GROSS_DEDUCTION_16ia = 17,
            GROSS_TOTAL_DEDUCTION_16iii = 18,
            INCOME_CHARGEABLE_SALARIES = 19,
            //
            INCOME_OR_LOSS_HOUSE_PROPERTY = 20,
            INCOME_OTHER_SOURCES = 21,
            //INCOME_OTHER_SALARY = 23,
            //
            GROSS_TOTAL_INCOME = 22,
            //
            SEC_80C = 23,
            SEC_80CCC = 24,
            SEC_80CCD_1 = 25,
            SEC_80C_CCC_CCD_1 = 26,
            SEC_80CCD_1B = 27,
            SEC_80CCD_2 = 28,
            SEC_80D = 29,
            SEC_80E = 30,
            SEC_80G = 31,
            SEC_80TTA = 32,
            ////
            DED_CHVIA_OTHER_SECTIONS = 33,
            GROSS_TOTAL_DED_CHVIA = 34,
            PREV_EMPLOYER_SALARY = 35
            //TOTAL_TAXABLE_INCOME = 35,
            //INCOME_TAX_ON_TOTAL_INCOME = 36,
            ////
            //REBATE_US_87A_AMOUNT = 39,
            ////
            //SURCHARGE = 40,
            //EDUCATION_CESS = 41,
            //INCOME_TAX_RELIEF = 42,
            //NET_TAX_PAYABLE = 43,
            //TOTAL_TDS_DEDUCTED = 44,
            //SHORTFALL_EXCESS = 45,
            //TAXABLE_AMOUNT = 46,
            //REPORTED_TAXABLE_AMOUNT = 47,
            //TOTAL_TAX_DEDUCTED_AMOUNT = 48,
            //PREVIOUS_TAX_DEDUCTED_TOTAL = 49,
            //TAX_DEDUCTED_HIGHER_RATE = 50,
            ////
            //TDS_SUPERANN = 51,
            //CONTRIBUTIONS_SUPERANN_YN = 52,
            //NAME_SUPERANN = 53,
            //SUPERANN_FROM_DATE = 54,
            //SUPERANN_TO_DATE = 55,
            //AMT_REPAID = 56,
            //RATE_DED = 57,
            //AMT_TAX = 58,
            //GROSS_TOTAL_INC = 59,
            ////
            //RENT_PAYMENT_YN = 60,
            //PAN_LANDLORD1 = 61,
            //NAME_LANDLORD1 = 62,
            //PAN_LANDLORD2 = 63,
            //NAME_LANDLORD2 = 64,
            //PAN_LANDLORD3 = 65,
            //NAME_LANDLORD3 = 66,
            //PAN_LANDLORD4 = 67,
            //NAME_LANDLORD4 = 68,
            //INT_PAID_YN = 69,
            //PAN_LENDER1 = 70,
            //NAME_LENDER1 = 71,
            //PAN_LENDER2 = 72,
            //NAME_LENDER2 = 73,
            //PAN_LENDER3 = 74,
            //NAME_LENDER3 = 75,
            //PAN_LENDER4 = 76,
            //NAME_LENDER4 = 77
        }
        #endregion

        #region T_GET_F16SALARY_DATA_FROM_EXCEL

        public enum T_GET_F16SALARY_DATA_FROM_EXCEL
        {
            EMPLOYEE_SERIAL_NO = 0,
            EMPLOYEE_PAN = 1,
            EMPLOYEE_NAME = 2,
            EMPLOYEE_CATEGORY = 3,
            PERIOD_FROM_DATE = 4,
            PERIOD_TO_DATE = 5,
            GS_SEC17_1 = 6,
            GS_SEC17_2 = 7,
            GS_SEC17_3 = 8,
            TOTAL_SALARY = 9,
            LESS_US10_DESC1 = 10,
            LESS_US10_AMT1 = 11,
            LESS_US10_DESC2 = 12,
            LESS_US10_AMT2 = 13,
            LESS_US10_DESC3 = 14,
            LESS_US10_AMT3 = 15,
            LESS_US10_DESC4 = 16,
            LESS_US10_AMT4 = 17,
            LESS_US10_DESC5 = 18,
            LESS_US10_AMT5 = 19,
            LESS_ALLOWANCE_TOTAL = 20,
            BALANCE = 21,
            CURR_SALARY = 22,
            PREV_SALARY = 23,
            GROSS_DEDUCTION_16ii = 24,
            GROSS_DEDUCTION_16iii = 25,
            GROSS_DEDUCTION_16ia = 26,
            AGGREGATE_AMOUNT = 27,
            INCOME_CHARGEABLE_SALARIES = 28,
            INCOME_OTHER_SALARY_DESC1 = 29,
            INCOME_OTHER_SALARY_AMT1 = 30,
            INCOME_OTHER_SALARY_DESC2 = 31,
            INCOME_OTHER_SALARY_AMT2 = 32,
            INCOME_OTHER_SALARY_DESC3 = 33,
            INCOME_OTHER_SALARY_AMT3 = 34,
            INCOME_OTHER_SALARY_DESC4 = 35,
            INCOME_OTHER_SALARY_AMT4 = 36,
            TOTAL_INCOME_OTHER_SALARY = 37,
            GROSS_TOTAL_INCOME = 38,
            DED_CHVIA_80C_DESC1 = 39,
            DED_CHVIA_80C_AMT1 = 40,
            DED_CHVIA_80C_DESC2 = 41,
            DED_CHVIA_80C_AMT2 = 42,
            DED_CHVIA_80C_DESC3 = 43,
            DED_CHVIA_80C_AMT3 = 44,
            DED_CHVIA_80C_DESC4 = 45,
            DED_CHVIA_80C_AMT4 = 46,
            DED_CHVIA_80C_DESC5 = 47,
            DED_CHVIA_80C_AMT5 = 48,
            DED_CHVIA_80C_DESC6 = 49,
            DED_CHVIA_80C_AMT6 = 50,
            GROSS_TOTAL_80C = 51,
            DEDUCTIBLE_TOTAL_80C = 52,
            GROSS_AMT_SEC80CCC = 53,
            DED_AMT_SEC80CCC = 54,
            GROSS_AMT_SEC80CCD = 55,
            DED_AMT_SEC80CCD = 56,
            TOT_DED_AMT_80CCE = 57,
            GROSS_AMT_SEC80CCG = 58,
            DED_AMT_SEC80CCG = 59,
            OTHER_SEC_DESC1 = 60,
            OTHER_SEC_GROSS1 = 61,
            OTHER_SEC_QUAL1 = 62,
            OTHER_SEC_DED1 = 63,
            OTHER_SEC_DESC2 = 64,
            OTHER_SEC_GROSS2 = 65,
            OTHER_SEC_QUAL2 = 66,
            OTHER_SEC_DED2 = 67,
            OTHER_SEC_DESC3 = 68,
            OTHER_SEC_GROSS3 = 69,
            OTHER_SEC_QUAL3 = 70,
            OTHER_SEC_DED3 = 71,
            OTHER_SEC_DESC4 = 72,
            OTHER_SEC_GROSS4 = 73,
            OTHER_SEC_QUAL4 = 74,
            OTHER_SEC_DED4 = 75,
            OTHER_SEC_DESC5 = 76,
            OTHER_SEC_GROSS5 = 77,
            OTHER_SEC_QUAL5 = 78,
            OTHER_SEC_DED5 = 79,
            TOT_DED_AMT_OTHER_SEC = 80,
            GROSS_TOTAL_DED_CHVIA = 81,
            TOTAL_TAXABLE_INCOME = 82,
            INCOME_TAX_ON_TOTAL_INCOME = 83,
            SURCHARGE = 84,
            EDUCATION_CESS = 85,
            TAX_PAYABLE = 86,
            INCOME_TAX_RELIEF = 87,
            NET_TAX_PAYABLE = 88,
            TOTAL_TDS_DEDUCTED = 89,
            CURR_EMPLOYER_TDS = 90,
            PREV_EMPLOYER_TDS = 91,
            SHORTFALL_EXCESS = 92,
            TAX_DEDUCTED_HIGHER_RATE = 93,
            TDS_SUPERANN = 94,
            CONTRIBUTIONS_SUPERANN_YN = 95,
            NAME_SUPERANN = 96,
            SUPERANN_FROM_DATE = 97,
            SUPERANN_TO_DATE = 98,
            AMT_REPAID = 99,
            RATE_DED = 100,
            AMT_TAX = 101,
            GROSS_TOTAL_INC = 102,
            RENT_PAYMENT_YN = 103,
            PAN_LANDLORD1 = 104,
            NAME_LANDLORD1 = 105,
            PAN_LANDLORD2 = 106,
            NAME_LANDLORD2 = 107,
            PAN_LANDLORD3 = 108,
            NAME_LANDLORD3 = 109,
            PAN_LANDLORD4 = 110,
            NAME_LANDLORD4 = 111,
            INT_PAID_YN = 112,
            PAN_LENDER1 = 113,
            NAME_LENDER1 = 114,
            PAN_LENDER2 = 115,
            NAME_LENDER2 = 116,
            PAN_LENDER3 = 117,
            NAME_LENDER3 = 118,
            PAN_LENDER4 = 119,
            NAME_LENDER4 = 120
        }
        #endregion

        #region T_GET_F16SALARY_DATA_FROM_EXCEL_18-19

        public enum T_GET_F16SALARY_DATA_FROM_EXCEL_18_19
        {
            EMPLOYEE_SERIAL_NO = 0,
            EMPLOYEE_PAN = 1,
            EMPLOYEE_NAME = 2,
            EMPLOYEE_CATEGORY = 3,
            FROM_DATE = 4,
            TO_DATE = 5,
            TS_GS_SEC_17_1 = 6,
            TS_GS_SEC_17_2 = 7,
            TS_GS_SEC_17_3 = 8,
            TOTAL_SALARY = 9,
            SEC10_5_AMOUNT = 10,
            SEC10_10_AMOUNT = 11,
            SEC10_10A_AMOUNT = 12,
            SEC10_10AA_AMOUNT = 13,
            SEC10_13A_AMOUNT = 14,
            TS_LA_ITEM_1_DESC = 15,
            TS_LA_ITEM_1 = 16,
            TS_LA_ITEM_2_DESC = 17,
            TS_LA_ITEM_2 = 18,
            TS_LA_ITEM_3_DESC = 19,
            TS_LA_ITEM_3 = 20,
            TS_LA_ITEM_4_DESC = 21,
            TS_LA_ITEM_4 = 22,
            TS_LA_ITEM_5_DESC = 23,
            TS_LA_ITEM_5 = 24,

            //SEC10_OTHERS_AMOUNT = 25,
            LESS_ALLOWANCE_TOTAL = 25,

            BALANCE = 26,
            CURR_SALARY = 27,
            PREV_SALARY = 28,
            GROSS_DEDUCTION_16ii = 29,
            GROSS_DEDUCTION_16iii = 30,
            GROSS_DEDUCTION_16ia = 31,
            AGGREGATE_AMOUNT = 32,

            INCOME_CHARGEABLE_SALARIES = 33,
            INCOME_LOSS_HOUSE_PROPERTY = 34,
            INCOME_OTHER_SOURCES = 35,
            GROSS_TOTAL_INCOME = 36,

            CVIA_SEC80C_GROSS_AMOUNT = 37,
            CVIA_SEC80C_DED_AMOUNT = 38,
            CVIA_SEC80CCC_GROSS_AMOUNT = 39,
            CVIA_SEC80CCC_DED_AMOUNT = 40,
            CVIA_SEC80CCD_1_GROSS_AMOUNT = 41,
            CVIA_SEC80CCD_1_DED_AMOUNT = 42,
            CVIA_SEC80C_CCC_CCD_1_GROSS_AMOUNT = 43,
            CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = 44,
            CVIA_SEC80CCD_1B_GROSS_AMOUNT = 45,
            CVIA_SEC80CCD_1B_DED_AMOUNT = 46,
            CVIA_SEC80CCD_2_GROSS_AMOUNT = 47,
            CVIA_SEC80CCD_2_DED_AMOUNT = 48,
            CVIA_SEC80D_GROSS_AMOUNT = 49,
            CVIA_SEC80D_DED_AMOUNT = 50,
            CVIA_SEC80E_GROSS_AMOUNT = 51,
            CVIA_SEC80E_DED_AMOUNT = 52,
            CVIA_SEC80G_GROSS_AMOUNT = 53,
            CVIA_SEC80G_QUAL_AMOUNT = 54,
            CVIA_SEC80G_DED_AMOUNT = 55,
            CVIA_SEC80TTA_GROSS_AMOUNT = 56,
            CVIA_SEC80TTA_QUAL_AMOUNT = 57,
            CVIA_SEC80TTA_DED_AMOUNT = 58,

            CVIA_OTH_ITEM_1_DESC = 59,
            CVIA_OTH_ITEM_1_GROSS_AMOUNT = 60,
            CVIA_OTH_ITEM_1_QUAL_AMOUNT = 61,
            CVIA_OTH_ITEM_1_DED_AMOUNT = 62,

            CVIA_OTH_ITEM_2_DESC = 63,
            CVIA_OTH_ITEM_2_GROSS_AMOUNT = 64,
            CVIA_OTH_ITEM_2_QUAL_AMOUNT = 65,
            CVIA_OTH_ITEM_2_DED_AMOUNT = 66,

            CVIA_OTH_ITEM_3_DESC = 67,
            CVIA_OTH_ITEM_3_GROSS_AMOUNT = 68,
            CVIA_OTH_ITEM_3_QUAL_AMOUNT = 69,
            CVIA_OTH_ITEM_3_DED_AMOUNT = 70,

            CVIA_OTH_ITEM_4_DESC = 71,
            CVIA_OTH_ITEM_4_GROSS_AMOUNT = 72,
            CVIA_OTH_ITEM_4_QUAL_AMOUNT = 73,
            CVIA_OTH_ITEM_4_DED_AMOUNT = 74,

            CVIA_OTH_ITEM_5_DESC = 75,
            CVIA_OTH_ITEM_5_GROSS_AMOUNT = 76,
            CVIA_OTH_ITEM_5_QUAL_AMOUNT = 77,
            CVIA_OTH_ITEM_5_DED_AMOUNT = 78,

            CVIA_OTH_ITEM_6_DESC = 79,
            CVIA_OTH_ITEM_6_GROSS_AMOUNT = 80,
            CVIA_OTH_ITEM_6_QUAL_AMOUNT = 81,
            CVIA_OTH_ITEM_6_DED_AMOUNT = 82,

            CVIA_OTH_GROSS_TOTAL = 83,
            CVIA_OTH_QUAL_TOTAL = 84,
            CVIA_OTH_DED_TOTAL = 85,

            TOTAL_TAXABLE_INCOME = 86,
            INCOME_TAX_ON_TOTAL_INCOME = 87,

            REBATE_US_87A_AMOUNT = 88,
            SURCHARGE = 89,
            EDUCATION_CESS = 90,
            TAX_PAYABLE = 91,
            INCOME_TAX_RELIEF = 92,
            NET_TAX_PAYABLE = 93,
            TOTAL_TDS_DEDUCTED = 94,
            CURR_EMPLOYER_TDS = 95,
            PREV_EMPLOYER_TDS = 96,
            SHORTFALL_EXCESS = 97,
            TAX_DEDUCTED_HIGHER_RATE = 98,
            TDS_SUPERANN = 99,
            CONTRIBUTIONS_SUPERANN_YN = 100,
            NAME_SUPERANN = 101,
            SUPERANN_FROM_DATE = 102,
            SUPERANN_TO_DATE = 103,
            AMT_REPAID = 104,
            RATE_DED = 105,
            AMT_TAX = 106,
            GROSS_TOTAL_INC = 107,
            RENT_PAYMENT_YN = 108,
            PAN_LANDLORD1 = 109,
            NAME_LANDLORD1 = 110,
            PAN_LANDLORD2 = 111,
            NAME_LANDLORD2 = 112,
            PAN_LANDLORD3 = 113,
            NAME_LANDLORD3 = 114,
            PAN_LANDLORD4 = 115,
            NAME_LANDLORD4 = 116,
            INT_PAID_YN = 117,
            PAN_LENDER1 = 118,
            NAME_LENDER1 = 119,
            PAN_LENDER2 = 120,
            NAME_LENDER2 = 121,
            PAN_LENDER3 = 122,
            NAME_LENDER3 = 123,
            PAN_LENDER4 = 124,
            NAME_LENDER4 = 125
        }
        #endregion

        #endregion

        #region User Defined Events

        #region TrnExcelImport_Load

        private void TrnExcelImport_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //
            //Added by Indrajit on 23-02-2013
            tmrLoginRefresh.Interval = (int)TDSMAN.Classes.TDSMAN.T_pLockInterval * 60000;
            tmrLoginRefresh.Start();
            //-----
            //-----------
            lblTitle.Text = "Excel Validate";
            cmbFinancialYear.Select();
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //-- QUARTER
            //-----------
            //string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            //dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
            //-- FORM NO
            //string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails };
            //string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q};
            //dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            //-----------
            //-- COMPANY
            //-----------
            //Modified by Shrey Kejriwal on 17/08/2011
            //strSQL = " SELECT COMPANY_ID," +
            //    "             COMPANY_NAME " +
            //    "      FROM   MST_COMPANY " +
            //    "      ORDER BY COMPANY_NAME";

            //--
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']'" +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            BtnSave.Tag = T_SaveTag.VALIDATE;
            // 
            //-----------------------------------------------------------
            //ADDED BY DHRUB FOR BOOKMARK HISTORY on 07/01/2014
            //-----------------------------------------------------------
            //if(Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT SHOW_BOOKMARK FROM MST_SETUP"))> 0)
            //    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = true;
            //else
            //    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = false;
            //if (TDSMAN.Classes.TDSMAN.T_EnableDeducteeFirst == true)
            //    grpEnterDeducteeDetailsOnly.Visible = true;
            //else
            //    grpEnterDeducteeDetailsOnly.Visible = false;
            ////--
            //chkEnterDeducteeDetailsOnly.Checked = true;
            //grpEnterDeducteeDetailsOnly.Enabled = false;
        }

        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xlsx", "Excel File | *.xlsx", "Choose the Excel File to import");
            else
                strFVUPath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            //--
            if (strFVUPath != "")
                txtExcelPath.Text = strFVUPath;
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            if (BtnExit.Text == "Back")
            {
                dgcViewDeductee.Visible = false;
                dgcViewChallan.Visible = true;
                //--
                //LoadChallanGrid();
                BtnExit.Text = "Exit";
            }
            else
            {
                dmlService.Dispose();
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
                {
                    #region VALIDATE
                    //--
                    if (ValidateFields() == false) return;
                    //--
                    //-- %%%%%%%%%%%%%%%%%%%%%%%
                    if (cmnService.J_UserMessage("Proceed Validation of Excel for calculation of \nTotal Taxable Income ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    //-- %%%%%%%%%%%%%%%%%%%%%%%
                    //--
                    this.Cursor = Cursors.WaitCursor;

                    string strConnectionString = "";

                    //MAKING CONNECTION TO THE EXCEL FILE
                    if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xls")
                        strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    else if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xlsx")
                        strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";

                    con = new OleDbConnection(strConnectionString);
                    //                
                    con.Open();
                    //--
                    if (CheckExcelStructure(txtExcelPath.Text) == false)
                    {
                        this.Cursor = Cursors.Default;
                        //--
                        //if (chkCalcTDS.Checked == true)
                        //{
                        cmnService.J_UserMessage("Selected Excel file is invalid.\n" +
                                             "Please get the latest Excel file format from the button just right of FA Year.", MessageBoxIcon.Exclamation);
                        prgBar.Value = 0;
                        btnDownloadExcelFormatCalcTDS.Select();
                        return;
                        //}
                        //else
                        //    cmnService.J_UserMessage("Selected Excel file is invalid.\n" +
                        //                         "Please get the latest Excel file from : Regular Return > Import from Excel > Create Blank Excel sheet", MessageBoxIcon.Exclamation);
                        //--
                        prgBar.Value = 0;
                        btnSelectExcelPath.Select();
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "Process Started";
                    //--
                    //
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    // CREATE TEMP TABLES 
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    {
                        //MessageBox.Show("1");
                        if (CREATE_TEMP_TABLES_SQL() == false)
                        {
                            cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        if (CREATE_TEMP_TABLES() == false)
                        {
                            cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    // GET DATA FROM EXCEL ACCESS       
                    //
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "Transferring Data";
                    //
                    //start do work
                    if (GET_DATA_FROM_EXCEL_ACCESS() == false)
                    {
                        cmnService.J_UserMessage(strErrorMessage);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //
                    // VALIDATE DATA     
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "Validation for TDS Calculation Started";
                    //
                    //
                    if (VALIDATE_DATA_TAXABLE_INCOME_CALC() == false)
                    {
                        //--
                        TdsMan.SHRINK_DATABASE();
                        //--
                        cmnService.J_UserMessage("Data Validation failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //
                    Delete_TMP_Files(txtExcelPath.Text);
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "Temporary files deleted";
                    //--
                    #region CHECK ERR
                    strSQL = "SELECT COUNT(*) FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION;
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        //--
                        lblProgressDisplayMessage.Visible = true;
                        lblProgressDisplayMessage.Text = "Some Validation error found";
                        //--
                        TdsMan.SHRINK_DATABASE();
                        //--
                        if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false)
                        {
                            cmnService.J_UserMessage("Error Sheet Deletion failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        //
                        lblProgressDisplayMessage.Visible = false;
                        //
                        prgBar.Value = prgBar.Value + 5;
                        this.Refresh();
                        //--
                        if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        prgBar.Value = prgBar.Value + 5;
                        this.Refresh();
                        lblProgressDisplayMessage.Visible = false;
                        lblProgressDisplayMessage.Visible = true;
                        lblProgressDisplayMessage.Text = "Creating the Excel Sheet for shownig Validaion Error";
                        //
                        if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        prgBar.Value = prgBar.Value + 5;
                        this.Refresh();
                        lblProgressDisplayMessage.Visible = false;
                        //--
                        cmnService.J_UserMessage("Excel File Validation failed \n Click OK to open the Excel file ?", MessageBoxIcon.Error);
                        if (File.Exists(txtExcelPath.Text) == true)
                        {
                            System.Diagnostics.Process.Start(txtExcelPath.Text);
                        }
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        lblProgressDisplayMessage.Visible = false;
                        return;
                    }
                    else
                    {
                        //--
                        lblProgressDisplayMessage.Visible = false;
                        //--
                        if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false)
                        {
                            cmnService.J_UserMessage("Error Sheet Deletion failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        //--
                        //--
                        TdsMan.SHRINK_DATABASE();
                        //--
                        //cmnService.J_UserMessage("Excel Data Validated Sucessfully...\nNow it will Calculate TDS...", MessageBoxIcon.Information);
                        //this.Cursor = Cursors.Default;
                        //prgBar.Value = 0;
                        //return;
                    }
                    #endregion
                    //
                    #region COMMENTED
                    #region CALC TDS
                    //if (CALC_TDS() == false)
                    //{//--
                    //    TdsMan.SHRINK_DATABASE();
                    //    //--
                    //    lblProgressDisplayMessage.Visible = false;
                    //    cmnService.J_UserMessage("Calculation failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}
                    #endregion
                    //--------
                    #region WRITE CALC TO EXCEL
                    //if (WRITE_CALC_WORKSHEET(txtExcelPath.Text) == false)
                    //{
                    //    lblProgressDisplayMessage.Visible = false;
                    //    cmnService.J_UserMessage("Writing Calc Sheet failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}
                    //else
                    //{
                    //    //lblProgressDisplayMessage.Visible = false;
                    //    //this.Cursor = Cursors.Default;
                    //    //prgBar.Value = 0;
                    //    //cmnService.J_UserMessage("TDS Calculated Sucessfully...\nPress OK to open the Excel file...", MessageBoxIcon.Information);
                    //    cmnService.J_UserMessage("TDS Calculated Sucessfully...", MessageBoxIcon.Information);
                    //    //if (File.Exists(txtExcelPath.Text) == true)
                    //    //{
                    //    //    System.Diagnostics.Process.Start(txtExcelPath.Text);
                    //    //}
                    //    //this.Cursor = Cursors.Default;
                    //    //prgBar.Value = 0;
                    //    //return;
                    //}
                    #endregion
                    //--
                    //if (VALIDATE_DATA() == false)
                    //{
                    //    //--
                    //    TdsMan.SHRINK_DATABASE();
                    //    //--
                    //    cmnService.J_UserMessage("Data Validation for Excel import failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}
                    #endregion
                    //
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //
                    if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false)
                    {
                        cmnService.J_UserMessage("Error Sheet Deletion failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //--
                    if (DELETE_WORKSHEET(txtExcelPath.Text, strNewDeducteeWorkSheetName) == false)
                    {
                        cmnService.J_UserMessage("New Deductee Sheet Deletion failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //--
                    if (DELETE_WORKSHEET(txtExcelPath.Text, strEmployeesModifiedWorkSheetName) == false)
                    {
                        cmnService.J_UserMessage("New Deductee Sheet Deletion failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //

                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "New Worksheet created";
                    //
                    //--
                    if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //--
                    //
                    if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //}
                    prgBar.Value = prgBar.Value + 5;
                    this.Refresh();
                    //--                
                    if (chkColorCodingExcelsheet.Checked == true)
                    {
                        //
                        lblProgressDisplayMessage.Visible = true;
                        lblProgressDisplayMessage.Text = "EXCEL file coloring";
                        //
                        if (COLOR_ERROR_CELLS(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Coloring Error Cells failed", MessageBoxIcon.Exclamation);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
                    Delete_TMP_Files(txtExcelPath.Text);
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "Temporary files deleted";
                    //--------
                    //
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                    {
                        if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false) this.Cursor = Cursors.Default;
                        //--
                        if (NEW_DEDUCTEE_MASTER_CREATED() == false)
                        {
                            cmnService.J_UserMessage("Deductee Master list creation failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        //--
                        #region TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC - EXCEL_DELETE_FLAG
                        if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC, "EXCEL_DELETE_FLAG") == false)
                        {
                            strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC, "EXCEL_DELETE_FLAG", "NUMBER", "", "", "0");
                            dmlService.J_ExecSql(strSQL);
                            //
                            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET EXCEL_DELETE_FLAG = 0";
                            dmlService.J_ExecSql(strSQL);
                        }
                        //
                        strSQL = "UPDATE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                        "         INNER JOIN  TRN_SALARY_DETAILS_PROJECTED_FORM16 " +
                        "                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN = TRN_SALARY_DETAILS_PROJECTED_FORM16.EMPLOYEE_PAN " +
                        "       SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EXCEL_DELETE_FLAG  = 1 ";

                        if (dmlService.J_ExecSql(strSQL) == false)
                            return;
                        //
                        #endregion
                        //--
                        //lngNewDeducteesCreated = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 0")));
                        lngNewDeducteesCreated = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE)));
                        //                        
                        if (lngNewDeducteesCreated > 0)
                        {
                            if (lngNewDeducteesCreated > TDSMAN.Classes.TDSMAN.T_MAX_REC_EXCEL_IMPORT_ALERT) //-- 2019/08/01
                            {
                                cmnService.J_UserMessage("Total No. of new deductees being added : " + lngNewDeducteesCreated + ".\n\n" +
                                                         "Not being able to display these new deductees in Excel.\nShowing this list " +
                                                         "in excel slows down the Import process.\nYou have opted not to display.\n\n" +
                                                         "You can modify from [Utilities > Preferences (Sl No. 27)]");
                                //
                            }
                            else
                            {
                                //--
                                if (CREATE_NEW_DEDUCTEE_WORKSHEET(txtExcelPath.Text, strNewDeducteeWorkSheetName) == false)
                                {
                                    cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBar.Value = 0;
                                    return;
                                }
                                prgBar.Value = prgBar.Value + 5;
                                this.Refresh();
                                //--
                                //
                                if (WRITE_NEW_DEDUCTEE_MASTER_SHEET(txtExcelPath.Text) == false)
                                {
                                    cmnService.J_UserMessage("Writing Deductee Master Sheet failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBar.Value = 0;
                                    return;
                                }
                                prgBar.Value = prgBar.Value + 5;
                                this.Refresh();
                            }

                        }
                        //--
                        lngEmployeesToBeModified = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 1")));
                        //
                        if (lngEmployeesToBeModified > 0)
                        {
                            if (lngEmployeesToBeModified > TDSMAN.Classes.TDSMAN.T_MAX_REC_EXCEL_IMPORT_ALERT) //-- 2019/08/01
                            {
                                cmnService.J_UserMessage("Total No. of deductees being modified : " + lngEmployeesToBeModified + ".\n\n" +
                                                         "Not being able to display these deductees in Excel.\nShowing this list " +
                                                         "in excel slows down the Import process.\nYou have opted not to display.\n\n" +
                                                         "You can modify from [Utilities > Preferences (Sl No. 27)]");
                                //
                            }
                            else
                            {
                                //--
                                if (CREATE_NEW_DEDUCTEE_WORKSHEET(txtExcelPath.Text, strEmployeesModifiedWorkSheetName) == false)
                                {
                                    cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBar.Value = 0;
                                    return;
                                }
                                prgBar.Value = prgBar.Value + 5;
                                this.Refresh();
                                //--
                                //
                                if (WRITE_MODIFY_DEDUCTEE_MASTER_SHEET(txtExcelPath.Text) == false)
                                {
                                    cmnService.J_UserMessage("Writing Modify Employee Master Sheet failed", MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    prgBar.Value = 0;
                                    return;
                                }
                                prgBar.Value = prgBar.Value + 5;
                                this.Refresh();
                            }
                        }
                        //
                        //-- CALCULATE TOTAL TAXABLE INCOME
                        #region CALCULATE TOTAL TAXABLE INCOME
                            //
                            #region TOTAL_TAXABLE_INCOME
                            if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC, "TOTAL_TAXABLE_INCOME") == false)
                        {
                            strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC, "TOTAL_TAXABLE_INCOME", "MONEY", "", "", "0");
                            dmlService.J_ExecSql(strSQL);
                            //
                            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET TOTAL_TAXABLE_INCOME = 0";
                            dmlService.J_ExecSql(strSQL);
                        }
                        #endregion
                        //
                        // TOTAL_TAXABLE_INCOME = GROSS_TOTAL_INCOME - CVIA_DED_TOTAL
                        strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                                 @" SET   TOTAL_TAXABLE_INCOME = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " - " +
                                               cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_DED_TOTAL", J_SQLColFormat.ConvertToMoney) + " ";
                        dmlService.J_ExecSql(strSQL);
                        //
                        strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                                 @" SET   TOTAL_TAXABLE_INCOME = 0 
                                    WHERE TOTAL_TAXABLE_INCOME < 0 ";
                        dmlService.J_ExecSql(strSQL);
                        #endregion
                        //
                        this.Refresh();
                        // ------------------------------------------
                        //--
                        for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                        {
                            prgBar.PerformStep();
                        }
                        this.Cursor = Cursors.Default;
                        //
                        con.Close();
                        //
                        cmnService.J_UserMessage("Excel File Validation is completed \n     Proceed to Import ", MessageBoxIcon.Information);
                    }
                    else
                    {
                        //if (DROP_TEMP_TABLES() == false)
                        //{
                        //    //cmnService.J_UserMessage("", MessageBoxIcon.Error);
                        //    this.Cursor = Cursors.Default;
                        //    //return;
                        //}
                        //--
                        TdsMan.SHRINK_DATABASE();
                        //--
                        for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                        {
                            prgBar.PerformStep();
                        }
                        this.Cursor = Cursors.Default;

                        con.Close();

                        //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                        if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            if (File.Exists(txtExcelPath.Text) == true)
                            {
                                System.Diagnostics.Process.Start(txtExcelPath.Text);
                            }
                        }
                        prgBar.Value = 0;
                        return;
                    }
                    //--
                    if (LOAD_IMPORT_INTERFACE() == false)
                    {
                        cmnService.J_UserMessage("Loading Import Interface failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //LoadChallanGrid();
                    //--
                    BtnSave.Text = "&Import Data";
                    BtnSave.Tag = T_SaveTag.IMPORT;
                    lblTitle.Text = "Excel Import";
                    //--
                    //--
                    //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q || T_FormNo.F24QSalaryDetails == T_FormNo.F26Q || T_FormNo.F24QSalaryDetails == T_FormNo.F27Q || T_FormNo.F24QSalaryDetails == T_FormNo.F27EQ)
                    //{
                    //    //strSQL = "SELECT CHALLAN_DETAILS_ID " +
                    //    //         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " " +
                    //    //         "WHERE  (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) - " +
                    //    //         "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX) < 0 ";
                    //    if (chkEnterDeducteeDetailsOnly.Checked == false) //-- 2016/08/19
                    //    {
                    //        //-- ANIK 2014/01/21
                    //        strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " WHERE ERR_DESC <> ''";
                    //        if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    //        {
                    //            BtnSave.Enabled = false;
                    //            BtnSave.BackColor = Color.LightGray;
                    //        }
                    //        else
                    //        {
                    //            BtnSave.Enabled = true;
                    //            BtnSave.BackColor = Color.Lavender;
                    //        }
                    //    }
                    //}
                    #endregion
                }
                else if (Convert.ToString(BtnSave.Tag) == T_SaveTag.IMPORT)
                {
                    #region IMPORT
                    //--
                    string strMessage = "";
                    if (rbnIncremental.Checked == true)
                        strMessage = "Incremental import";
                    else if (rbnNewImport.Checked == true)
                    {
                        //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q)
                        //{
                        //    if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails ||
                        //        T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
                        //        strMessage = "All existing Salary details data will be deleted";
                        //    else
                        //        strMessage = "All existing Challan and Employee details data will be deleted";
                        //}
                        //else
                        //    strMessage = "All existing Challan and Deductee details data will be deleted";
                    }
                    //--
                    #region F24Q-F26Q-F27Q-F27EQ
                    //--
                    //if (chkEnterDeducteeDetailsOnly.Checked == false)
                    //{
                    //    strSQL = "SELECT CHALLAN_DETAILS_ID " +
                    //             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " " +
                    //             "WHERE  (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " +
                    //             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX) < 0 ";
                    //    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    //    {
                    //        cmnService.J_UserMessage("Import not possible", MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                    //--
                    //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q)
                    //{
                    //    if (cmnService.J_UserMessage(strMessage + "\n based on \n " +
                    //        "FINANCIAL YEAR \t: " + cmbFinancialYear.Text + " \n " +
                    //        "QUARTER \t: " + cmbQuarter.Text + " \n " +
                    //        "FORM NO. \t: " + T_FormNo.F24QSalaryDetails + " \n " +
                    //        "COMPANY  \t: " + cmbCompany.Text + " \n " +
                    //        "Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                    //        return;
                    //}
                    //else
                    //{
                    //    if (cmnService.J_UserMessage(strMessage + "\n based on \n " +
                    //        "FINANCIAL YEAR \t: " + cmbFinancialYear.Text + " \n " +
                    //        "QUARTER \t: " + cmbQuarter.Text + " \n " +
                    //        "FORM NO. \t: " + T_FormNo.F24QSalaryDetails + " \n " +
                    //        "COMPANY \t: " + cmbCompany.Text + " \n \n" +
                    //        "Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                    //        return;
                    //}
                    //--
                    if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    //--
                    prgImportBar.Value = 0;
                    //--
                    this.Cursor = Cursors.WaitCursor;
                    //--                
                    prgImportBar.Value = prgImportBar.Value + 5;
                    this.Refresh();
                    //--
                    //lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //    cmbQuarter.Text,
                    //    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //    T_FormNo.F24QSalaryDetails);
                    ////--
                    //if (lngBasicInfoID == 0)
                    //{
                    //    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                    cmbQuarter.Text,
                    //                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                    T_FormNo.F24QSalaryDetails) == true)
                    //    {
                    //        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                            cmbQuarter.Text,
                    //                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                            T_FormNo.F24QSalaryDetails);
                    //        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                    //    }
                    //}
                    //--
                    TdsMan.SHRINK_DATABASE();
                    //--###########################
                    //if (TdsMan.LimitEditions(lngBasicInfoID, rbnIncremental.Checked, false, false, "TRN_DEDUCTEE_DETAILS", "BASIC_INFO_ID", TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC, "") == false)
                    //{
                    //    prgImportBar.Value = 0;
                    //    this.Refresh();
                    //    this.Cursor = Cursors.Default;
                    //    BtnSave.Enabled = false;
                    //    return;
                    //}
                    //--###########################
                    //--
                    //Added by Shrey Kejriwal on 20/01/2012
                    //strSQL = "UPDATE TRN_BASIC_INFO SET NIL_RETURN = 0 WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    //dmlService.J_ExecSql(strSQL);
                    //
                    if (rbnNewImport.Checked == true)
                    {
                        //if (TdsMan.T_DeleteDeducteeDetails(lngBasicInfoID) == false)
                        //{
                        //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        //    this.Cursor = Cursors.Default;
                        //    return;
                        //}
                        //
                        //prgImportBar.Value = prgImportBar.Value + 5;
                        //this.Refresh();
                        //
                        //if (TdsMan.T_DeleteChallanDetails(lngBasicInfoID) == false)
                        //{
                        //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        //    this.Cursor = Cursors.Default;
                        //    return;
                        //}
                        //
                        prgImportBar.Value = prgImportBar.Value + 5;
                        this.Refresh();
                        //
                    }
                    //--

                    //--
                    //if (TdsMan.T_DeleteReceiptNo(lngBasicInfoID) == false)
                    //{
                    //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                    //
                    prgImportBar.Value = prgImportBar.Value + 5;
                    this.Refresh();
                    //--
                    //if (chkEnterDeducteeDetailsOnly.Checked == false)
                    //{
                    //    if (INSERT_CHALLAN_DATA(lngBasicInfoID) == false)
                    //    {
                    //        cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                    //        this.Cursor = Cursors.Default;
                    //        return;
                    //    }
                    //}
                    //
                    prgImportBar.Value = prgImportBar.Value + 5;
                    this.Refresh();
                    //--                
                    if (INSERT_MASTER_DATA() == false)
                    {
                        cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    this.Refresh();
                    //--
                    //-- DELETE COMMON RECORDS ONLY...
                    #region EXCEL_DELETE_FLAG
                    if (dmlService.J_IsDatabaseObjectExist("TRN_SALARY_DETAILS_PROJECTED_FORM16", "EXCEL_DELETE_FLAG") == false)
                    {
                        strSQL = dmlService.ReturnALTERSyntaxSequelServer("TRN_SALARY_DETAILS_PROJECTED_FORM16", "EXCEL_DELETE_FLAG", "NUMBER", "", "", "0");
                        dmlService.J_ExecSql(strSQL);
                    }
                    //
                    strSQL = "UPDATE TRN_SALARY_DETAILS_PROJECTED_FORM16 SET EXCEL_DELETE_FLAG = 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    #endregion
                    //
                    strSQL = "UPDATE TRN_SALARY_DETAILS_PROJECTED_FORM16 " + 
                    "         INNER JOIN  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "                 ON TRN_SALARY_DETAILS_PROJECTED_FORM16.EMPLOYEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN  " +
                    "       SET    TRN_SALARY_DETAILS_PROJECTED_FORM16.EXCEL_DELETE_FLAG  = 1 " +
                    "       WHERE  TRN_SALARY_DETAILS_PROJECTED_FORM16.ASST_ID            = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + " " +
                    "       AND    TRN_SALARY_DETAILS_PROJECTED_FORM16.COMPANY_ID         = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return;
                    //
                    strSQL = "DELETE FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 WHERE EXCEL_DELETE_FLAG  = 1";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return;
                    //--
                    if (INSERT_SALARY_DATA_18_19(lngBasicInfoID) == false)
                    {
                        cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    this.Refresh();
                    //
                    #endregion
                    //--
                    if (DROP_TEMP_TABLES() == false)
                    {
                        cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    this.Refresh();
                    //
                    //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails)
                    //{
                    //    for (int i = prgSDBar.Minimum; i <= prgSDBar.Maximum; i++)
                    //    {
                    //        prgSDBar.PerformStep();
                    //    }
                    //}
                    //else
                    //{
                    //    for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                    //    {
                    //        prgImportBar.PerformStep();
                    //    }
                    //}
                    //
                    BtnSave.Enabled = false;
                    //
                    this.Cursor = Cursors.Default;
                    //--
                    BtnExit.Text = "E&xit";
                    //---------------------------------------------------------
                    //---------------------------------------------------------
                    //ADDED BY DHRUB ON 07/01/2014 FOR BOOKMARK
                    //---------------------------------------------------------
                    //if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    //{
                    //    //strQuery = @"FORM_NAME= '" + T_FormNo.F24QSalaryDetails + "' ";
                    //    //-- 2014/11/14
                    //    //strQuery = @" FORM_NAME= '" + T_FormNo.F24QSalaryDetails + "' AND SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID;
                    //    ////
                    //    //string strFormNo = "";
                    //    //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails
                    //    //    || T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
                    //    //    strFormNo = cmnService.J_Left(T_FormNo.F24QSalaryDetails, 4).Trim();
                    //    //else
                    //    //    strFormNo = T_FormNo.F24QSalaryDetails;
                    //    //
                    //    if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL", strQuery) == true)
                    //    {
                    //        //Inserting the Form BookMarkDetail
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.UPDATE.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //    else
                    //    {
                    //        //Inserting the Form BookMarkDetail
                    //        intAsstId = TdsMan.T_GetBookMarkDetail(T_TransactionMode.INSERT.ToString(),
                    //                                            Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                            cmbQuarter.Text,
                    //                                            strFormNo,
                    //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                    //                                            TDSMAN.Classes.TDSMAN.T_MACHINE_ID,
                    //                                            out strQuarter,
                    //                                            out strCompanyName
                    //                                           );
                    //    }
                    //}
                    //---------------------------------------------------------
                    //---------------------------------------------------------
                    cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                    prgImportBar.Value = 0;
                    prgSDBar.Value = 0;
                    //
                    string strFaYear = cmbFinancialYear.Text;
                    string strCompany = cmbCompany.Text;
                    //if (lblFormNo.Text == "")
                    //    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = lblFormNoSD.Text;
                    //else
                    //    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = lblFormNo.Text;
                    //
                    this.Close();
                    this.Dispose();
                    //--
                    //Do you want to check the data imported?
                    if (cmnService.J_UserMessage("Do you want to View the records ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {//
                        double dblFinancialYearId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                        double dblCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                        //
                        TrnForm16 objTrnForm16 = new TrnForm16(dblFinancialYearId, dblCompanyId, strFaYear, strCompany);
                        objTrnForm16.MdiParent = TrnRegularReturn.ActiveForm;
                        objTrnForm16.Show();
                    }
                    //--
                    //
                    #endregion
                }
                //------------------------
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return;
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                txtDeductorType.Text = "";
                return;
            }
            txtDeductorType.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_CATEGORY.CATEGORY_CODE AS CATEGORY_CODE FROM MST_COMPANY, MST_CATEGORY WHERE MST_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID AND MST_COMPANY.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
        }
        #endregion

        #region dgcViewChallan_DoubleClick

        private void dgcViewChallan_DoubleClick(object sender, EventArgs e)
        {
            if (dgcViewChallan.CurrentRowIndex >= 0)
            {
                dgcViewChallan.Visible = false;
                BtnExit.Text = "Back";
                //--
                dgcViewDeductee.Visible = true;
                //--------------------------------------------------
                //A particular ID wise retriving the data from database
                if (LoadDeducteeDetailsGrid(Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]))) == false)
                {
                    return;
                }
            }
            lblToolTip.Visible = false;
        }

        #endregion

        #region dgcViewChallan_KeyDown

        private void dgcViewChallan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) dgcViewChallan_DoubleClick(sender, e);
        }

        #endregion

        #region dgcViewChallan_MouseClick

        private void dgcViewChallan_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgcViewChallan.Visible == false) return;
            //
            dgcViewChallan.Select(dgcViewChallan.CurrentRowIndex);
            dgcViewChallan.Select();
            dgcViewChallan.Focus();
            //
            lblToolTip.Text = Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 11]);
            if (lblToolTip.Text.Trim() != "")
                lblToolTip.Visible = true;
            else
                lblToolTip.Visible = false;
        }

        #endregion

        #region dgcViewDeductee_MouseClick

        private void dgcViewDeductee_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewDeductee.Select(dgcViewDeductee.CurrentRowIndex);
            dgcViewDeductee.Select();
            dgcViewDeductee.Focus();
        }

        #endregion

        #region dgcViewChallan_MouseMove
        private void dgcViewChallan_MouseMove(object sender, MouseEventArgs e)
        {

            //var cell = dataGridView1.CurrentCell;
            //var cellDisplayRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            //toolTip1.Show(string.Format("this is cell {0},{1}", e.ColumnIndex, e.RowIndex),
            //              dataGridView1,
            //              cellDisplayRect.X + cell.Size.Width / 2,
            //              cellDisplayRect.Y + cell.Size.Height / 2,
            //              2000);
            //dataGridView1.ShowCellToolTips = false;
        }
        #endregion


        #region btnOpenNewDeducteesFound_Click
        private void btnOpenNewDeducteesFound_Click(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnInt64Value(lblNewDeducteesFound.Text) == 0)
            {
                cmnService.J_UserMessage("No new deductees");
                return;
            }
            //
            if (File.Exists(txtExcelPath.Text) == true)
            {
                System.Diagnostics.Process.Start(txtExcelPath.Text);
            }
            //-----------------------------------------------

        }

        #endregion

        #region btnSDNewDeducteesFound_Click
        private void btnSDNewDeducteesFound_Click(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnInt64Value(lblNewRecordsToBeAdded.Text) == 0)
            {
                cmnService.J_UserMessage("No new Employees");
                return;
            }
            //
            if (File.Exists(txtExcelPath.Text) == true)
            {
                System.Diagnostics.Process.Start(txtExcelPath.Text);
            }
            //-----------------------------------------------

        }

        #endregion

        #region cmbFinancialYear_KeyPress
        private void cmbFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbQuarter_KeyPress
        private void cmbQuarter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbFormNo_KeyPress
        private void cmbFormNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbQuarter_SelectedIndexChanged
        private void cmbQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-- COMMENTED TEMPORARILY ON 2019/05/14
            //MODIFIED BY SHREY KEJRIWAL

            //int intPrevSelectedIndex = cmbFormNo.SelectedIndex;

            //if (cmbQuarter.Text == T_Qtr.Q4)
            //{
            //    //-- FORM NO
            //    //-- 2017/01/09
            //    if (TDSMAN.Classes.TDSMAN.T_pEditionType == (int)T_EDITION_TYPE.STANDARD_EDITION)
            //    {
            //        string[] strQuarterWiseDeducteeReportFormSD = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails };
            //        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportFormSD, ref cmbFormNo, intPrevSelectedIndex);
            //    }
            //    else
            //    {
            //        string[] strQuarterWiseDeducteeReportFormSD = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails, T_FormNo.F24QForm16SalaryDetails };
            //        dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportFormSD, ref cmbFormNo, intPrevSelectedIndex);
            //    }
            //}
            //else
            //{
            //    if (intPrevSelectedIndex == 5)
            //    intPrevSelectedIndex = 1;
            //else
            //    intPrevSelectedIndex = 0;

            //    //-- FORM NO
            //    string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            //    dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, intPrevSelectedIndex);
            //}
            ////-- 
            //if (cmbQuarter.Text == T_Qtr.Q1)
            //{
            //    strSQL = " SELECT MONTH_ORDER," +
            //            "         MONTH_DESC " +
            //            "  FROM   MST_MONTH " +
            //            "  WHERE MONTH_ORDER BETWEEN 1 AND 3 " +
            //            "  ORDER BY MONTH_ORDER";
            //}
            //else if (cmbQuarter.Text == T_Qtr.Q2)
            //{
            //    strSQL = " SELECT MONTH_ORDER," +
            //            "         MONTH_DESC " +
            //            "  FROM   MST_MONTH " +
            //            "  WHERE MONTH_ORDER BETWEEN 4 AND 6 " +
            //            "  ORDER BY MONTH_ORDER";
            //}
            //else if (cmbQuarter.Text == T_Qtr.Q3)
            //{
            //    strSQL = " SELECT MONTH_ORDER," +
            //            "         MONTH_DESC " +
            //            "  FROM   MST_MONTH " +
            //            "  WHERE MONTH_ORDER BETWEEN 7 AND 9 " +
            //            "  ORDER BY MONTH_ORDER";
            //}
            //else if (cmbQuarter.Text == T_Qtr.Q4)
            //{
            //    strSQL = " SELECT MONTH_ORDER," +
            //            "         MONTH_DESC " +
            //            "  FROM   MST_MONTH " +
            //            "  WHERE MONTH_ORDER BETWEEN 10 AND 12 " +
            //            "  ORDER BY MONTH_ORDER";
            //}
            //else
            //{
            //    strSQL = " SELECT MONTH_ORDER," +
            //            "         MONTH_DESC " +
            //            "  FROM   MST_MONTH " +
            //            "  ORDER BY MONTH_ORDER";
            ////}
            ////
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbMonth, 0, J_ComboBoxSelectedIndex.YES) == false) return;
            //--


        }
        #endregion

        //Added by Indrajit on 23-02-2013
        #region TrnExcelImport_FormClosing
        private void TrnExcelImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TdsMan.FreeBasicInfoEntry() == false)
                return;
        }
        #endregion

        #region tmrLoginRefresh_Tick
        private void tmrLoginRefresh_Tick(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pBasicInfoId > 0)
            {
                strSQL = "UPDATE TEMP_STACK_BASIC_INFO " +
                         "SET    LAST_UPDATED_TIME = " + TdsMan.GetServerDateTime() + " " +
                         "WHERE  BASIC_INFO_ID     = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
                         "AND    USER_SERIAL       ='" + TDSMAN.Classes.TDSMAN.T_pProductSerial + "'";
                dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            }
        }
        #endregion

        #region tbcExcelImport_Selecting
        private void tbcExcelImport_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action == TabControlAction.Selecting)
            {
                if (blnOpenTabPage == false)
                {
                    e.Cancel = true;
                }
                else
                    blnOpenTabPage = false;
            }
        }
        #endregion

        #region btnOpenNewDeducteesFound_MouseMove
        private void btnOpenNewDeducteesFound_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnOpenNewDeducteesFound, "View new deductee(s)");
        }
        #endregion

        #region btnBulkPANValidation_MouseMove
        private void btnBulkPANValidation_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnBulkPANValidation, "PAN Name extractor");
        }
        #endregion

        #region cmbComboBox_SelectedIndexChanged
        private void cmbComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbFinancialYear.SelectedIndex > 0 && cmbQuarter.SelectedIndex > 0 && cmbFormNo.SelectedIndex > 0 && cmbCompany.SelectedIndex > 0)
            //{
            //    //--
            //    if (TdsMan.LockBasicInfoEntry(T_MODULENAME.IMPORT_EXCEL) == false)
            //    {
            //        //cmbStartingChequeNo.SelectedIndex = 0;
            //        //lblChequeLeavesAvailable.Visible = false;
            //        BtnExit.Select();
            //        return;
            //    }
            //}
        }
        #endregion

        #region chkEnterDeducteeDetailsOnly_CheckedChanged
        private void chkEnterDeducteeDetailsOnly_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkEnterDeducteeDetailsOnly.Checked == true)
            //{
            //    if (cmnService.J_UserMessage("Enter Deductee without Challan\nAre you Sure??", MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        chkEnterDeducteeDetailsOnly.Checked = false;
            //        grpImportType.Enabled = true;
            //        //rbnNewImport.Checked = true;
            //    }
            //    else
            //    {
            //        chkEnterDeducteeDetailsOnly.Checked = true;
            //        grpImportType.Enabled = false;
            //        rbnNewImport.Checked = true;
            //    }
            //}
            //else
            //{
            //    grpImportType.Enabled = true;
            //}
        }
        #endregion

        #region cmbFormNo_SelectedIndexChanged
        //private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pctVideoDemo.Visible = false;
        //    //
        //    if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails || T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails)
        //    {
        //        chkEnterDeducteeDetailsOnly.Checked = false;
        //        chkEnterDeducteeDetailsOnly.Enabled = false;
        //        //
        //        lblNoteAdd.Visible = false;
        //        //
        //        if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
        //            pctVideoDemo.Visible = true;
        //        else
        //            pctVideoDemo.Visible = false;
        //        //--                
        //    }
        //    else
        //    {
        //        pctVideoDemo.Visible = true;
        //        //
        //        lblNoteAdd.Visible = true;
        //        //
        //        if (chkEnterDeducteeDetailsOnly.Checked != true)
        //        {
        //            chkEnterDeducteeDetailsOnly.Checked = false;
        //            chkEnterDeducteeDetailsOnly.Enabled = true;
        //        }
        //        //--
        //        //grpCalcTDS.Visible = true;
        //    }
        //    //--
        //    //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q)
        //    //{
        //    //    grpCalcTDS.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    grpCalcTDS.Visible = false;
        //    //}
        //}
        #endregion


        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnViewChallanInformationOnline");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region lnkSearchByTAN_MouseMove
        private void lnkSearchByTAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(lnkSearchByTAN, "Click here to search the Company by TAN");
        }
        #endregion


        #region btnDownloadExcelFormatCalcTDS_Click
        private void btnDownloadExcelFormatCalcTDS_Click(object sender, EventArgs e)
        {
            int intIncrement = 0;
            //--
            if (cmbFinancialYear.Text == "")
            {
                cmnService.J_UserMessage("Select the relevant Financial year");
                cmbFinancialYear.Select();
                return;
            }
            //--
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Select the Company");
                cmbCompany.Select();
                return;
            }
            //--
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the Form 16 excel format.");
                return;
            }
            //-- INITIALIZE THE DESTINATION PATH OF OUTPUT EXCEL 
            string strDestFilePath = cmnService.J_OpenFolderDialog();
            //--
            string strDestFileNamePath = "";
            string strTempDestFileNamePath = Path.Combine(strDestFilePath, cmbFinancialYear.Text + "-" + cmnService.J_Left(cmnService.J_Right(cmbCompany.Text, 11), 10) + ".XLSX");
            //
            do
            {
                if (cmnService.J_IsFileExist(strTempDestFileNamePath) == true)
                {
                    intIncrement++;
                    strTempDestFileNamePath = strTempDestFileNamePath + " (" + intIncrement + ")";
                }

            } while (cmnService.J_IsFileExist(strTempDestFileNamePath) == true);
            //--
            strDestFileNamePath = strTempDestFileNamePath; //Default Filename
            //--
            using (WebClient wc = new WebClient())
                wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-SD_CALC_TOTAL_TAXABLE_INCOME-V1.XLSX", strDestFileNamePath);
            //--
            System.Threading.Thread.Sleep(100);
            //--
            if (cmnService.J_IsFileExist(strDestFileNamePath) == true)
            {
                cmnService.J_UserMessage("Blank Excel File Created");
                //--
                System.Diagnostics.Process.Start(strDestFileNamePath);
            }
            else
                cmnService.J_UserMessage("Blank Excel File Creation Failed");
            //--
            //txtDestinationFileName.Text = "";
            //--
            return;
            //}
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //int V =  FileVersionInfo.GetVersionInfo(;
                //V.ToString();
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    //if (grpSearch.Visible == false)
                    //{
                    //    if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //        if (dsetGridClone == null) return false;
                    //        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //        return false;
                    //    }
                    //}
                    //else if (grpSearch.Visible == true)
                    //{
                    //    if (txtPANSearch.Text.Trim() == "" &&
                    //        txtEmployeeNameSearch.Text.Trim() == "" &&
                    //        txtCompanyNameSearch.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.SearchingValues);
                    //        txtPANSearch.Select();
                    //        return false;
                    //    }
                    //}
                    //return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- FINANCILAL YEAR
                    //-----------------------------------------------------------------------
                    if (cmbFinancialYear.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Financial Year - Cannot be Blank");
                        cmbFinancialYear.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- QTR
                    //-----------------------------------------------------------------------
                    //if (cmbQuarter.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Quarter - Cannot be Blank");
                    //    cmbQuarter.Select();
                    //    return false;
                    //}
                    ////-----------------------------------------------------------------------
                    ////-- FORM NO
                    ////-----------------------------------------------------------------------
                    //if (cmbFormNo.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Form No. - Cannot be Blank");
                    //    cmbFormNo.Select();
                    //    return false;
                    //}
                    //-----------------------------------------------------------------------
                    //-- COMPANY NAME
                    //-----------------------------------------------------------------------
                    if (cmbCompany.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Company - Cannot be Blank");
                        cmbCompany.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FVU FILE SELECTED
                    //-----------------------------------------------------------------------
                    if (txtExcelPath.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Excel file not selected");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    // FILE SHOULD BE FVU
                    if (Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLSX")
                    {
                        cmnService.J_UserMessage("Selected file should be a Excel file");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    // FILE EXIST
                    if (cmnService.J_IsFileExist(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Selected Excel file not found");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    // FILE OPEN
                    //-- ANIK 2011-09-09
                    string strPath = txtExcelPath.Text.ToString();
                    //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
                    if (TdsMan.T_isFileOpenOrReadOnly(ref strPath) == true)
                    {
                        cmnService.J_UserMessage("Selected Excel file is open");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    //-- Added By Abhishek Dey On 05/04/2018 --
                    //MAKING CONNECTION TO THE EXCEL FILE
                    //if(TdsMan.Is64Bit()== true)
                    //{

                    //}
                    //--
                    //if(chkCalcTDS.Checked==true)
                    //{
                    //if (cmbMonth.SelectedIndex <= 0)
                    //{
                    //    cmnService.J_UserMessage("Month - Cannot be Blank as you have opted for TDS Calculation");
                    //    cmbMonth.Select();
                    //    return false;
                    //}
                    //}
                    //--
                    #region TAN NO - COMPANY CHECK
                    if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xls")
                        strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    else if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xlsx")
                        strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";

                    con = new OleDbConnection(strConnectionString);
                    //                
                    con.Open();
                    if (TdsMan.T_IsExcelDatabaseObjectExist("Company Details", null, con) == true)
                    {
                        string strImportCompanyTan = string.Empty;
                        //// READ EXCEL FILE
                        DataSet myDataSet;
                        OleDbDataAdapter myCommand;
                        //
                        //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                        myDataSet = new DataSet();
                        //
                        myCommand = new OleDbDataAdapter("SELECT * FROM [Company Details$]", con);
                        myCommand.Fill(myDataSet);
                        //
                        con.Close();
                        con.Dispose();
                        //
                        int i = 0;
                        foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                        {
                            i = i + 1;
                            //Stores info in Datarow into an array
                            if (i == 2)
                            {
                                Object[] cells = myDataRow.ItemArray;
                                //
                                strImportCompanyTan = Convert.ToString(cells[0]).ToUpper();
                            }

                        }
                        if (string.IsNullOrEmpty(strImportCompanyTan))
                        {
                            cmnService.J_UserMessage("In 'Company Details' sheet, the TAN is mandatory.");
                            return false;
                        }
                        //--
                        if (Convert.ToInt32(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) AS COUNT1 FROM MST_COMPANY WHERE TAN_NO = '" + strImportCompanyTan + "'"))) == 0)
                        {
                            cmnService.J_UserMessage(" The TAN entered in the 'Company Details' sheet is not available in Company Master.");
                            return false;
                        }
                        //--
                        if (strImportCompanyTan.Equals(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))))))
                            return true;
                        else
                        {
                            cmnService.J_UserMessage("The TAN selected and the TAN entered in the 'Company Details' sheet does not matches.");
                            return false;
                        }
                        //--

                    }
                    else
                    {
                        con.Close();
                        con.Dispose();
                    }
                    #endregion
                    //-----------------------------------------
                    return true;
                }
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion
        
        #region CREATE_TEMP_TABLES_SQL
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                #region T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC
                //
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC;
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "") == false)
                {

                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + @" (
                            " + cmnService.J_GetDataType("SALARY_DETAILS_TAXABLE_INCOME_CALC_ID", J_Identity.YES) + @",
                            " + cmnService.J_GetDataType("RUNNING_SERIAL_NO_ORDER", J_ColumnType.Integer) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_PAN_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Integer) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("EMPLOYEE_NAME_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CATEGORY_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_1", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_1_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_2", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_2_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_3", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_GS_SEC_17_3_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_BALANCE", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("TS_BALANCE_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_5_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_5_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10A_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10A_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10AA_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_10AA_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_13A_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_13A_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_OTHERS_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("SEC10_OTHERS_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("BALANCE_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("BALANCE_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_EA", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_EA_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_TE", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_TE_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_IA", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_IA_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_AGGREGATE", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("US_16_AGGREGATE_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_CHARGEABLE", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_CHARGEABLE_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_LOSS_HOUSE_PROPERTY", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_LOSS_HOUSE_PROPERTY_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_OTHER_SOURCES", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("INCOME_OTHER_SOURCES_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("GROSS_TOTAL_INCOME", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("GROSS_TOTAL_INCOME_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80C_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80C_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCC_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCC_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_1_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_1_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_1B_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_1B_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_2_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80CCD_2_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80D_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80D_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80E_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80E_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80G_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80G_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80TTA_DED_AMOUNT", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_SEC80TTA_DED_AMOUNT_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_OTH_DED_TOTAL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_OTH_DED_TOTAL_CELL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_DED_TOTAL", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("CVIA_DED_TOTAL_CELL", J_ColumnType.String) + @", 
                            " + cmnService.J_GetDataType("PREV_EMPLOYER_SALARY", J_ColumnType.String) + @",
                            " + cmnService.J_GetDataType("PREV_EMPLOYER_SALARY_CELL", J_ColumnType.String) + @")";
                    //--
                    dmlService.J_ExecSql(strSQL);
                    //
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    {
                        //-- INDEX
                        strSQL = "CREATE INDEX IDX_DEDUCTEE_DETAILS_ID ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " (SALARY_DETAILS_TAXABLE_INCOME_CALC_ID)";
                        dmlService.J_ExecSql(strSQL);
                        //
                        strSQL = "CREATE INDEX IDX_DEDUCTEE_PAN ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " (EMPLOYEE_PAN)";
                        dmlService.J_ExecSql(strSQL);
                        //
                    }
                }
                //--
                //--
                dmlService.J_Commit();
                #endregion
                //Added by INDRAJIT on 16-03-2012
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_SUMM + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_SUMM + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //--
                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //            

                dmlService.J_BeginTransaction();
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == false)
                {
                    //strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
                    //     "                  DEDUCTEE_MASTER_ID COUNTER," +
                    //     "                  DEDUCTEE_NAME      TEXT(75) DEFAULT \"\"," +
                    //     "                  DEDUCTEE_PAN       TEXT(10) DEFAULT \"\"," +
                    //     "                  DEDUCTEE_CODE      TEXT(5) DEFAULT \"\"," +
                    //     "                  COMPANY_ID         NUMBER  DEFAULT 0)"; 
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_MASTER_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_NAME", J_ColumnType.String, 75) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_PAN", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_CODE", J_ColumnType.String, 5) + "," +
                         "                  " + cmnService.J_GetDataType("COMPANY_ID", J_ColumnType.Long) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();

                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC;
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  ERR_VALIDATION_ID COUNTER," +
                         "                  ERR_TYPE          TEXT(255) DEFAULT \"\"," +
                         "                  ERR_CELL          TEXT(255) DEFAULT \"\"," +
                         "                  ERR_COLUMN        TEXT(255) DEFAULT \"\"," +
                         "                  ERR_SHEET         TEXT(255) DEFAULT \"\"," +
                         "                  ERR_COLOR         TEXT(255) DEFAULT \"\"," +
                         "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                         "                  ERR_FORM_NO       TEXT(255) DEFAULT \"\")";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                dmlService.J_ExecSql(strSQL);
                //--
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
                         "                  DEDUCTEE_MASTER_ID COUNTER," +
                         "                  DEDUCTEE_NAME      TEXT(75) DEFAULT \"\"," +
                         "                  DEDUCTEE_PAN       TEXT(10) DEFAULT \"\"," +
                         "                  DEDUCTEE_CODE      TEXT(5) DEFAULT \"\"," +
                         "                  COMPANY_ID         NUMBER  DEFAULT 0)";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "", "COMPANY_ID") == false)
                {
                    strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " ADD COLUMN COMPANY_ID NUMBER DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                dmlService.J_ExecSql(strSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        //-- COMMENTED 2016/02/02
        #region CREATE_TEMP_ERR_TABLES
        //private bool CREATE_TEMP_ERR_TABLES()
        //{
        //    try
        //    {
        //        //
        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }
        //        //
        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
        //        {
        //            strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
        //                 "                  ERR_VALIDATION_ID COUNTER," +
        //                 "                  ERR_TYPE          TEXT(25) DEFAULT \"\"," +
        //                 "                  ERR_CELL          TEXT(10) DEFAULT \"\"," +
        //                 "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
        //                 "                  ERR_SHEET         TEXT(25) DEFAULT \"\"," +
        //                 "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
        //                 "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
        //                 "                  ERR_FORM_NO       TEXT(25) DEFAULT \"\")";
        //            dmlService.J_ExecSql(strSQL);
        //        }
        //        //
        //        strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
        //        dmlService.J_ExecSql(strSQL);

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region CREATE_TEMP_ERR_TABLES
        private bool CREATE_TEMP_ERR_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 255) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                dmlService.J_ExecSql(strSQL);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DROP_TEMP_TABLES
        private bool DROP_TEMP_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC;
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                    dmlService.J_ExecSql(strSQL);
                }
                ////
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                TdsMan.SHRINK_DATABASE();
                //
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region GET_DATA_FROM_EXCEL_ACCESS
        private bool GET_DATA_FROM_EXCEL_ACCESS()
        {
            int intLN = 0;
            try
            {
                #region VARIABLE_DECLARATION
                //
                string strEmployeeSerialNo = "";
                string strEmployeePAN = "";
                string strEmployeeName = "";
                string strEmployeeCategory = "";
                string strPeriodFromDate = "";
                string strPeriodToDate = "";
                string strTotalSalary = "";
                string strGrossDeduction16ii = "";
                string strGrossDeduction16iii = "";
                string strGrossDeduction16ia = "";
                string strGrossTotalDeduction16iii = "";
                string strIncomeChargeableSalaries = "";
                string strIncomeOtherSalary = "";
                string strGrossTotalIncome = "";
                string strDedChVIA80CCE = "";
                string strDedChVIA80CCF = "";
                string strDedChVIAOtherSections = "";
                string strGrossTotalDedChVIA = ""; int intLineNumberChallan = 0;
                string strSec17_1 = "", strSec17_2 = "", strSec17_3 = "", strSec10_5 = "", strSec10_10 = "", strSec10_10A = "", strSec10_10AA = "", strSec10_13A = "", strSec10_OTHERS = "";
                string strBalanceAmount = "";
                string strIncomeOrLossHouseProperty = "", strIncomOtherSources = "";
                string strSec80C = "", strSec80CCC = "", strSec80CCD_1 = "", strSec80CTOTAL = "", strSec80CCD_1B = "", strSec80CCD_2 = "", strSec80D = "", strSec80E = "", strSec80G = "", strSec80TTA = "";
                string strSerialNo = "";
                string strPrevEmployerSalary = "";
                // CREATE THE SUBFOLDER.
                string strStartupPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION) 
                    strStartupPath = Path.Combine(J_Var.J_pEnterpriseServerPath, "TMP FOLDER");
                else
                    strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //
                //string strTemporaryfileChallanPath = Path.Combine(strStartupPath, "ChalllanDetails.txt");
                //string strTemporaryfileDeducteePath = Path.Combine(strStartupPath, "DeducteeDetails.txt");
                //string strTemporaryfileSalaryPath = Path.Combine(strStartupPath, "SalaryDetails.txt");
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                {
                    strTemporaryfileCalcTotalTaxableIncome = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_CalcTotalTaxableIncome.txt");
                    if (File.Exists(strTemporaryfileCalcTotalTaxableIncome) == true)
                        File.Delete(strTemporaryfileCalcTotalTaxableIncome);
                    //
                }
                else
                {
                    strTemporaryfileCalcTotalTaxableIncome = Path.Combine(strStartupPath, "CalcTotalTaxableIncome.txt");
                }
                string tableName = "";
                string textfileName = "";

                #endregion

                if (KILL_EXCEL() == false)
                    return false;

                //// READ EXCEL FILE
                DataSet myDataSet;
                OleDbDataAdapter myCommand;
                //
                #region INSERT DETAILS

                // DEDUCTEE DETAILS
                #region TRANSFERRING DATA FROM EXCEL TO TEXT FILE
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                //
                myCommand = new OleDbDataAdapter("SELECT * FROM [Annual Salary$]", con);
                //
                myCommand.Fill(myDataSet);

                int intLineNumber = 0;

                clsPopulateSerial PopulateSerial = new clsPopulateSerial();

                StreamWriter StreamWriterSDCalcTaxableIncome= cmnService.J_ReturnStreamWriter(strTemporaryfileCalcTotalTaxableIncome);
                long intDeducteeSerialNo = 0;

                long intLineNumberSalary = 0;
                
                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessage.Visible = true;
                    //
                    //Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;
                    //
                    intLineNumberSalary = intLineNumberSalary + 1;
                    //intLN = intLineNumber;
                    //if (intLN == 320584)
                    //    cmnService.J_UserMessage(intLN.ToString());
                    //Added by INDRAJIT on 16-03-2012
                    int intColumnValue = 64;
                    int intColumnValueA = 64;
                    //
                    //strDeducteeSerialNo_414 = Convert.ToString(cells[0]).ToUpper();
                    strSerialNo = cmnService.J_NullToText(Convert.ToString(cells[1]).ToUpper());
                    //
                    intDeducteeSerialNo = PopulateSerial.NextSerial(strSerialNo);
                    //checking if this challan exists

                    strSerialNo = Convert.ToString(intDeducteeSerialNo);

                    //if (chkEnterDeducteeDetailsOnly.Checked == false)
                    //{
                    //    if (strChallanSerialNo_401 == "")
                    //        strDeducteeSerialNo_414 = "";
                    //}
                    //--
                    long lngColumnIndex = 2;
                    //
                    #region F24Q

                    strEmployeeSerialNo = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.EMPLOYEE_SERIAL_NO]).ToUpper();
                    strEmployeePAN = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.EMPLOYEE_PAN]).ToUpper();
                    strEmployeeName = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.EMPLOYEE_NAME]).ToUpper();
                    strEmployeeCategory = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.EMPLOYEE_CATEGORY]).ToUpper();
                    //--
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2018_19ID) //-- 2019/05/01
                    //{
                    strSec17_1 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.TS_GS_SEC_17_1]).ToUpper();
                    strSec17_2 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.TS_GS_SEC_17_2]).ToUpper();
                    strSec17_3 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.TS_GS_SEC_17_3]).ToUpper();
                    //}
                    //--
                    strTotalSalary = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.TOTAL_SALARY]).ToUpper();
                    //--
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2018_19ID) //-- 2019/05/01
                    //{
                    strSec10_5 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_5_AMOUNT]).ToUpper();
                    strSec10_10 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_10_AMOUNT]).ToUpper();
                    strSec10_10A = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_10A_AMOUNT]).ToUpper();
                    strSec10_10AA = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_10AA_AMOUNT]).ToUpper();
                    strSec10_13A = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_13A_AMOUNT]).ToUpper();
                    strSec10_OTHERS = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC10_OTHERS_AMOUNT]).ToUpper();
                    strBalanceAmount = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.BALANCE_AMOUNT]).ToUpper();
                    //}
                    //--
                    strGrossDeduction16ii = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_DEDUCTION_16ii]).ToUpper();
                    strGrossDeduction16iii = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_DEDUCTION_16iii]).ToUpper();
                    strGrossDeduction16ia = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_DEDUCTION_16ia]).ToUpper(); //-- 2018/12/24
                    strGrossTotalDeduction16iii = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_TOTAL_DEDUCTION_16iii]).ToUpper();
                    strIncomeChargeableSalaries = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.INCOME_CHARGEABLE_SALARIES]).ToUpper();
                    //--
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2018_19ID) //-- 2019/05/01
                    //{
                    strIncomeOrLossHouseProperty = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.INCOME_OR_LOSS_HOUSE_PROPERTY]).ToUpper();
                    strIncomOtherSources = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.INCOME_OTHER_SOURCES]).ToUpper();
                    //}
                    //else
                    //{
                    //    strIncomeOtherSalary = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.INCOME_OTHER_SALARY]).ToUpper();
                    //}
                    strGrossTotalIncome = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_TOTAL_INCOME]).ToUpper();
                    //--
                    //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2018_19ID) //-- 2019/05/01
                    //{
                    strSec80C = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80C]).ToUpper();
                    strSec80CCC = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80CCC]).ToUpper();
                    strSec80CCD_1 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80CCD_1]).ToUpper();
                    strSec80CTOTAL = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80C_CCC_CCD_1]).ToUpper();
                    strSec80CCD_1B = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80CCD_1B]).ToUpper();
                    strSec80CCD_2 = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80CCD_2]).ToUpper();
                    strSec80D = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80D]).ToUpper();
                    strSec80E = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80E]).ToUpper();
                    strSec80G = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80G]).ToUpper();
                    strSec80TTA = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.SEC_80TTA]).ToUpper();
                    //}
                    //else
                    //{
                    //    strDedChVIA80CCE = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.DED_CHVIA_80CCE]).ToUpper();
                    //    strDedChVIA80CCF = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.DED_CHVIA_80CCF]).ToUpper();
                    //}

                    strDedChVIAOtherSections = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.DED_CHVIA_OTHER_SECTIONS]).ToUpper();
                    strGrossTotalDedChVIA = Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.GROSS_TOTAL_DED_CHVIA]).ToUpper();
                    //
                    strPrevEmployerSalary= Convert.ToString(cells[(int)T_GET_SALARY_DATA_FROM_EXCEL_18_19.PREV_EMPLOYER_SALARY]).ToUpper();
                    //--
                    if (strEmployeeSerialNo == "" &&
                              strEmployeePAN == "" &&
                                strEmployeeName == "" &&
                                    strEmployeeCategory == "" &&
                                        strPeriodFromDate == "" &&
                                            strPeriodToDate == "" &&
                                                strTotalSalary == "" &&
                                                  strGrossDeduction16ii == "" &&
                                                    strGrossDeduction16iii == "" &&
                                                    strGrossDeduction16ia == "" &&
                                                        strGrossTotalDeduction16iii == "" &&
                                                            strIncomeChargeableSalaries == "" &&
                                                                strIncomeOtherSalary == "" &&
                                                                    strGrossTotalIncome == "" &&
                                                                        strDedChVIA80CCE == "" &&
                                                                            strDedChVIA80CCF == "" &&
                                                                                strDedChVIAOtherSections == "" &&
                                                                                  strGrossTotalDedChVIA == "")
                        break;
                    //
                    #region 18-19 ONWARDS

                    intInitialColumnAfterZ = 64;
                    strExcelFieldAsciiAfterZ = Convert.ToString(Convert.ToChar(intInitialColumnAfterZ += 1));
                    //--
                    strBAsciiAfterZ = Convert.ToString(Convert.ToChar(intInitialColumnAfterZ += 1));
                    strCAsciiAfterZ = Convert.ToString(Convert.ToChar(intInitialColumnAfterZ += 1));
                    strDAsciiAfterZ = Convert.ToString(Convert.ToChar(intInitialColumnAfterZ += 1));

                    cmnService.J_WriteLine(ref StreamWriterSDCalcTaxableIncome, TdsMan.T_WriteField(intLineNumberSalary.ToString()) +
                                                             TdsMan.T_WriteField(intLineNumberSalary.ToString()) +
                                                             TdsMan.T_WriteField(strEmployeePAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField("0") +
                                                             TdsMan.T_WriteField(strEmployeeName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strEmployeeCategory) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             
                                                             TdsMan.T_WriteField(strSec17_1) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec17_2) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec17_3) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 7)) + (Convert.ToString(intLineNumberSalary + 1)))) +

                                                             TdsMan.T_WriteField(strTotalSalary) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 8)) + (Convert.ToString(intLineNumberSalary + 1)))) +

                                                             TdsMan.T_WriteField(strSec10_5) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 9)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec10_10) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 10)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec10_10A) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 11)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec10_10AA) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 12)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec10_13A) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 13)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec10_OTHERS) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 14)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strBalanceAmount) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 15)) + (Convert.ToString(intLineNumberSalary + 1)))) +

                                                             TdsMan.T_WriteField(strGrossDeduction16ii) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 16)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strGrossDeduction16iii) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 17)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strGrossDeduction16ia) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 18)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strGrossTotalDeduction16iii) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 19)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strIncomeChargeableSalaries) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 20)) + (Convert.ToString(intLineNumberSalary + 1)))) +

                                                             TdsMan.T_WriteField(strIncomeOrLossHouseProperty) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 21)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strIncomOtherSources) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 22)) + (Convert.ToString(intLineNumberSalary + 1)))) +


                                                             //TdsMan.T_WriteField(strIncomeOtherSalary) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 24)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strGrossTotalIncome) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 23)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             //TdsMan.T_WriteField(strDedChVIA80CCE) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 26)) + (Convert.ToString(intLineNumberSalary + 1)))) +

                                                             //TdsMan.T_WriteField(strDedChVIA80CCF) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 1)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80C) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 24)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80CCC) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 25)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80CCD_1) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 26)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80CTOTAL) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 1)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80CCD_1B) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 2)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80CCD_2) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 3)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80D) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 4)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80E) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 5)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80G) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 6)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strSec80TTA) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 7)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strDedChVIAOtherSections) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 8)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strGrossTotalDedChVIA) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 9)) + (Convert.ToString(intLineNumberSalary + 1)))) +
                                                             TdsMan.T_WriteField(strPrevEmployerSalary) + TdsMan.T_WriteField((strExcelFieldAsciiAfterZ + Convert.ToString((char)(intColumnValueA + 9)) + (Convert.ToString(intLineNumberSalary + 1)))));
                    #endregion

                    #endregion
                }
                //
                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriterSDCalcTaxableIncome.Flush();
                StreamWriterSDCalcTaxableIncome.Close();

                #endregion

                #region TRANSFERRING TO DATA FROM TEXT TO ACCESS

                //TABLE NAME TO HOLD DATA
                tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC;

                //TEXT FILE NAME
                textfileName = "CalcTotalTaxableIncome";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTemporaryfileCalcTotalTaxableIncome, true);

                ImportTextToTables(tableName, textfileName, strTemporaryfileCalcTotalTaxableIncome, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTemporaryfileCalcTotalTaxableIncome) == true)
                    File.Delete(strTemporaryfileCalcTotalTaxableIncome);

                #endregion
                    
                #endregion
                //
                myDataSet.Dispose();
                myCommand.Dispose();

                con.Close();
                con.Dispose();
                //
                intLN = 0;
                return true;
                //############################################
            }
            catch (Exception e)
            {
                //strErrorMessage = "Invalid data format in Excel file, please check. " + intLN;
                //-- 2015/10/14
                cmnService.J_UserMessage(e.Message);
                con.Close();
                con.Dispose();
                return false;
            }
        }
        #endregion        

        #region ImportTextToTables

        private void ImportTextToTables(string tbl, string txtfile, string FilePath, bool hdr)
        {
            //Added by INDRAJIT on 14-03-2012

            //Check 'n Create SCHEMA file for Temp Tables
            string strFolderPath = cmnService.J_GetDirectoryName(strTemporaryfileCalcTotalTaxableIncome);

            if (File.Exists(strFolderPath + "\\schema.ini") == true)
            {
                File.Delete(strFolderPath + "\\schema.ini");
            }
            StreamWriter StreamWriter = new StreamWriter(strFolderPath + "\\schema.ini");

            StreamWriter.WriteLine("[" + txtfile + ".txt]");
            StreamWriter.WriteLine("ColNameHeader=" + (hdr == true ? "True" : "False") + "");
            StreamWriter.WriteLine("Format=Delimited(^)");
            StreamWriter.WriteLine("MaxScanRows=0");
            StreamWriter.WriteLine("CharacterSet=ANSI");
            //
            #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "
                StreamWriter.WriteLine(@"Col1=SALARY_DETAILS_ID Integer
                                         Col2=RUNNING_SERIAL_NO_ORDER Integer
                                         Col3=EMPLOYEE_PAN Char
                                         Col4=EMPLOYEE_PAN_CELL Char
                                         Col5=EMPLOYEE_ID Integer
                                         Col6=EMPLOYEE_NAME Char
                                         Col7=EMPLOYEE_NAME_CELL Char
                                         Col8=CATEGORY Char
                                         Col9=CATEGORY_CELL Char
                                         Col10=TS_GS_SEC_17_1 Char
                                         Col11=TS_GS_SEC_17_1_CELL Char 
                                         Col12=TS_GS_SEC_17_2 Char
                                         Col13=TS_GS_SEC_17_2_CELL Char
                                         Col14=TS_GS_SEC_17_3 Char
                                         Col15=TS_GS_SEC_17_3_CELL Char  
                                         Col16=TS_BALANCE Char
                                         Col17=TS_BALANCE_CELL Char
                                         Col18=SEC10_5_AMOUNT Char
                                         Col19=SEC10_5_AMOUNT_CELL Char
                                         Col20=SEC10_10_AMOUNT Char
                                         Col21=SEC10_10_AMOUNT_CELL Char
                                         Col22=SEC10_10A_AMOUNT Char
                                         Col23=SEC10_10A_AMOUNT_CELL Char
                                         Col24=SEC10_10AA_AMOUNT Char
                                         Col25=SEC10_10AA_AMOUNT_CELL Char
                                         Col26=SEC10_13A_AMOUNT Char
                                         Col27=SEC10_13A_AMOUNT_CELL Char
                                         Col28=SEC10_OTHERS_AMOUNT Char
                                         Col29=SEC10_OTHERS_AMOUNT_CELL Char
                                         Col30=BALANCE_AMOUNT Char
                                         Col31=BALANCE_AMOUNT_CELL Char
                                         Col32=US_16_EA Char
                                         Col33=US_16_EA_CELL Char
                                         Col34=US_16_TE Char
                                         Col35=US_16_TE_CELL Char
                                         Col36=US_16_IA Char
                                         Col37=US_16_IA_CELL Char
                                         Col38=US_16_AGGREGATE Char
                                         Col39=US_16_AGGREGATE_CELL Char
                                         Col40=INCOME_CHARGEABLE Char
                                         Col41=INCOME_CHARGEABLE_CELL Char
                                         Col42=INCOME_LOSS_HOUSE_PROPERTY Char
                                         Col43=INCOME_LOSS_HOUSE_PROPERTY_CELL Char
                                         Col44=INCOME_OTHER_SOURCES Char
                                         Col45=INCOME_OTHER_SOURCES_CELL Char
                                         Col46=GROSS_TOTAL_INCOME Char
                                         Col47=GROSS_TOTAL_INCOME_CELL Char
                                         Col48=CVIA_SEC80C_DED_AMOUNT Char
                                         Col49=CVIA_SEC80C_DED_AMOUNT_CELL Char
                                         Col50=CVIA_SEC80CCC_DED_AMOUNT Char
                                         Col51=CVIA_SEC80CCC_DED_AMOUNT_CELL Char
                                         Col52=CVIA_SEC80CCD_1_DED_AMOUNT Char
                                         Col53=CVIA_SEC80CCD_1_DED_AMOUNT_CELL Char
                                         Col54=CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT Char
                                         Col55=CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL Char
                                         Col56=CVIA_SEC80CCD_1B_DED_AMOUNT Char
                                         Col57=CVIA_SEC80CCD_1B_DED_AMOUNT_CELL Char
                                         Col58=CVIA_SEC80CCD_2_DED_AMOUNT Char
                                         Col59=CVIA_SEC80CCD_2_DED_AMOUNT_CELL Char
                                         Col60=CVIA_SEC80D_DED_AMOUNT Char
                                         Col61=CVIA_SEC80D_DED_AMOUNT_CELL Char
                                         Col62=CVIA_SEC80E_DED_AMOUNT Char
                                         Col63=CVIA_SEC80E_DED_AMOUNT_CELL Char
                                         Col64=CVIA_SEC80G_DED_AMOUNT Char
                                         Col65=CVIA_SEC80G_DED_AMOUNT_CELL Char
                                         Col66=CVIA_SEC80TTA_DED_AMOUNT Char
                                         Col67=CVIA_SEC80TTA_DED_AMOUNT_CELL Char
                                         Col68=CVIA_OTH_DED_TOTAL Char
                                         Col69=CVIA_OTH_DED_TOTAL_CELL Char
                                         Col70=CVIA_DED_TOTAL Char
                                         Col71=CVIA_DED_TOTAL_CELL Char
                                         Col72=PREV_EMPLOYER_SALARY Char
                                         Col73=PREV_EMPLOYER_SALARY_CELL Char");
                #endregion            
            //--
            //
            StreamWriter.Close();
            //
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                //Check 'n Create Temp Tables
                if (dmlService.J_IsDatabaseObjectExist(tbl) == true)
                {
                    strSQL = "DELETE FROM [" + tbl + "]";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                //if (Convert.ToString(File.ReadAllText(FilePath)) != "")
                if (Convert.ToString(File.ReadAllLines(FilePath)) != "")
                {
                    strSQL = @"BULK INSERT [" + tbl + "] FROM '" + FilePath + "' WITH (fieldterminator = '^', rowterminator = '\n')";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                TdsMan.SHRINK_DATABASE();
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                //Check 'n Create Temp Tables
                if (dmlService.J_IsDatabaseObjectExist(tbl) == true)
                {
                    strSQL = "DROP TABLE [" + tbl + "]";

                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "SELECT * INTO [" + tbl + "] FROM " +
                         @"[Text; DATABASE=" + strFolderPath + "].[" + txtfile + ".txt]";

                dmlService.J_ExecSql(strSQL);
            }
        }
        #endregion

        #region VALIDATE_DATA_TAXABLE_INCOME_CALC
        private bool VALIDATE_DATA_TAXABLE_INCOME_CALC()
        {
            string strSheetName = "";
            string strMidSubString = "";
            string strUpper = "";
            try
            {
                if (CREATE_TEMP_ERR_TABLES() == false)
                    return false;
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    strMidSubString = "SUBSTRING";
                    strUpper = "UPPER";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strMidSubString = "MID";
                    strUpper = "UCASE";
                }
                //
                strSheetName = strWorkingSheetName;
                //
                #region EMPLOYEE PAN

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET EMPLOYEE_PAN = '' WHERE EMPLOYEE_PAN IS NULL";
                dmlService.J_ExecSql(strSQL);


                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "     WHERE  EMPLOYEE_PAN = ''" +
                    "     AND    EMPLOYEE_PAN NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                 WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                       IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN      = '' ";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "     WHERE  LEN(EMPLOYEE_PAN) <> 10" +
                    "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL  = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                        IS NULL " +
                        "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN) <> 10 ";

                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //PAN STRUCTURE CHECK
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                         "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                         "     WHERE  (ISNUMERIC(LEFT(EMPLOYEE_PAN,1)) <> 0" +
                         "     OR     ISNUMERIC(SUBSTRING(EMPLOYEE_PAN,2,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(EMPLOYEE_PAN,3,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(EMPLOYEE_PAN,4,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(EMPLOYEE_PAN,5,1)) <> 0 " +
                         "     OR     ISNUMERIC(SUBSTRING(EMPLOYEE_PAN,6,4)) = 0 " +
                         "     OR     ISNUMERIC(RIGHT(EMPLOYEE_PAN,1)) = -1)" +
                         "     AND    EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                         "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                         "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                     "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                     "     WHERE  (ISNUMERIC(LEFT(EMPLOYEE_PAN,1)) <> 0" +
                     "     OR     ISNUMERIC(MID(EMPLOYEE_PAN,2,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(EMPLOYEE_PAN,3,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(EMPLOYEE_PAN,4,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(EMPLOYEE_PAN,5,1)) <> 0 " +
                     "     OR     ISNUMERIC(MID(EMPLOYEE_PAN,6,4)) = 0 " +
                     "     OR     ISNUMERIC(RIGHT(EMPLOYEE_PAN,1)) = -1)" +
                     "     AND    EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                     "     AND    EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                     "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "' " +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL                                         IS NULL " +
                            "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 1))   <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 2, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 3, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 4, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 5, 1)) <> 0 " +
                            "     OR     ISNUMERIC(SUBSTRING(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 6, 4))  = 0 " +
                            "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 1))  = -1) " +
                            "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN <> 'PANNOTAVBL' ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                            "             'EMPLOYEE_PAN_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "' " +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL                                         IS NULL " +
                            "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 1))   <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 2, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 3, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 4, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 5, 1)) <> 0 " +
                            "     OR     ISNUMERIC(MID(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 6, 4))  = 0 " +
                            "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN, 1))  = -1) " +
                            "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN <> 'PANNOTAVBL' ";
                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                
                #endregion
                //
                #region EMPLOYEE NAME

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET EMPLOYEE_NAME = '' WHERE EMPLOYEE_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);


                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "     WHERE  EMPLOYEE_NAME = ''" +
                    "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                       WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                        "            'EMPLOYEE_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET          = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                        IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME      = '' ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "     WHERE  LEN(EMPLOYEE_NAME) > 75" +
                    "     AND    EMPLOYEE_NAME_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                       WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                        "            'EMPLOYEE_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                        IS NULL " +
                        "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME) > 75 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                //
                #region EMPLOYEE CATEGORY

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CATEGORY = '' WHERE CATEGORY IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK

                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CATEGORY    = '' " +
                    "     AND    CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             CATEGORY_CELL AS ERROR_CELL," +
                        "             'CATEGORY_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CATEGORY = ''" +
                        "     AND    CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // VALIDITY CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  " + strUpper + "(CATEGORY) <> 'G'" +
                    "     AND    " + strUpper + "(CATEGORY) <> 'W'" +
                    "     AND    " + strUpper + "(CATEGORY) <> 'S'" +
                    "     AND    " + strUpper + "(CATEGORY) <> 'O'" +
                    "     AND    CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             CATEGORY_CELL AS ERROR_CELL," +
                        "             'CATEGORY_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + strUpper + "(CATEGORY) <> 'G'" +
                        "     AND    " + strUpper + "(CATEGORY) <> 'W'" +
                        "     AND    " + strUpper + "(CATEGORY) <> 'S'" +
                        "     AND    " + strUpper + "(CATEGORY) <> 'O'" +
                        "     AND    CATEGORY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ")";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                //
                #region GROSS SALARY a)SEC 17(1)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET TS_GS_SEC_17_1 = '0.00' WHERE TS_GS_SEC_17_1 IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  TS_GS_SEC_17_1 <> ''" +
                    "     AND    ISNUMERIC(TS_GS_SEC_17_1) = 0" +
                    "     AND    TS_GS_SEC_17_1_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1_CELL AS ERROR_CELL," +
                        "            'TS_GS_SEC_17_1_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_1", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    TS_GS_SEC_17_1_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1_CELL AS ERROR_CELL," +
                        "            'TS_GS_SEC_17_1_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS SALARY a)SEC 17(2)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET TS_GS_SEC_17_2 = '0.00' WHERE TS_GS_SEC_17_2 IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  TS_GS_SEC_17_2 <> ''" +
                    "     AND    ISNUMERIC(TS_GS_SEC_17_2) = 0" +
                    "     AND    TS_GS_SEC_17_2_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2_CELL AS ERROR_CELL," +
                        "            'TS_GS_SEC_17_2_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_2", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    TS_GS_SEC_17_2_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2_CELL AS ERROR_CELL," +
                        "            'GS_SEC17_1_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS SALARY a)SEC 17(3)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET TS_GS_SEC_17_3 = '0.00' WHERE TS_GS_SEC_17_3 IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  TS_GS_SEC_17_3 <> ''" +
                    "     AND    ISNUMERIC(TS_GS_SEC_17_3) = 0" +
                    "     AND    TS_GS_SEC_17_3_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3_CELL AS ERROR_CELL," +
                        "            'TS_GS_SEC_17_3_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_3", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    TS_GS_SEC_17_3_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3_CELL AS ERROR_CELL," +
                        "            'TS_GS_SEC_17_3_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TS_BALANCE

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET TS_BALANCE = '0.00' WHERE TS_BALANCE IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + @" SET TS_BALANCE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney) + @"
                          WHERE TS_GS_SEC_17_1_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + @"
                                                            WHERE ERR_SHEET = '" + strSheetName + @"')
                          AND   TS_GS_SEC_17_2_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + @"
                                                            WHERE ERR_SHEET = '" + strSheetName + @"')
                          AND   TS_GS_SEC_17_3_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + @"
                                                            WHERE ERR_SHEET = '" + strSheetName + "')";
                dmlService.J_ExecSql(strSQL);

                
                #endregion

                //
                #region SEC10 (5)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_5_AMOUNT = '0.00' WHERE SEC10_5_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_5_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_5_AMOUNT) = 0" +
                    "     AND    SEC10_5_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_5_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_5_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_5_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_5_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region SEC10 (10)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_10_AMOUNT = '0.00' WHERE SEC10_10_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_10_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_10_AMOUNT) = 0" +
                    "     AND    SEC10_10_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_10_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_10_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region SEC10 (10A)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_10A_AMOUNT = '0.00' WHERE SEC10_10A_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_10A_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_10A_AMOUNT) = 0" +
                    "     AND    SEC10_10A_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10A_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_10A_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10A_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region SEC10 (10AA)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_10AA_AMOUNT = '0.00' WHERE SEC10_10AA_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_10AA_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_10AA_AMOUNT) = 0" +
                    "     AND    SEC10_10AA_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10AA_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_10AA_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_10AA_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region SEC10 (13A)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_13A_AMOUNT = '0.00' WHERE SEC10_13A_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_13A_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_13A_AMOUNT) = 0" +
                    "     AND    SEC10_13A_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_13A_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_13A_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_13A_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region SEC10 (OTHERS)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET SEC10_OTHERS_AMOUNT = '0.00' WHERE SEC10_OTHERS_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  SEC10_OTHERS_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(SEC10_OTHERS_AMOUNT) = 0" +
                    "     AND    SEC10_OTHERS_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // 
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_OTHERS_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL             IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    SEC10_OTHERS_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT_CELL AS ERROR_CELL," +
                        "            'SEC10_OTHERS_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToString) + " <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region BALANCE_AMOUNT
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                         @" SET   BALANCE_AMOUNT = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_BALANCE", J_SQLColFormat.ConvertToMoney) + " - (" +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                               cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);
                //
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  BALANCE_AMOUNT <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("BALANCE_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    BALANCE_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".BALANCE_AMOUNT_CELL AS ERROR_CELL," +
                        "            'BALANCE_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".BALANCE_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".BALANCE_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".BALANCE_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                
                #region PREV_EMPLOYER_SALARY

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET PREV_EMPLOYER_SALARY = '0.00' WHERE PREV_EMPLOYER_SALARY IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  PREV_EMPLOYER_SALARY <> ''" +
                    "     AND    ISNUMERIC(PREV_EMPLOYER_SALARY) = 0" +
                    "     AND    PREV_EMPLOYER_SALARY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                           WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 22-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY_CELL AS ERROR_CELL," +
                        "            'PREV_EMPLOYER_SALARY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY     <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  PREV_EMPLOYER_SALARY <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    PREV_EMPLOYER_SALARY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {

                    // Modified by Ripan Paul on 22-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY_CELL AS ERROR_CELL," +
                        "            'PREV_EMPLOYER_SALARY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY     <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) + " < 0 ";// +
                                                                                                                                                                                                         //"     AND    FORMAT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY,0.00)     < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS DEDUCTION UNDER SECTION 16(II)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET US_16_EA = '0.00' WHERE US_16_EA IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  US_16_EA <> ''" +
                    //"     AND    " + cmnService.J_SQLDBFormat("US_16_EA", J_SQLColFormat.ConvertToMoney) + " = 0 " +
                    "     AND    ISNUMERIC(US_16_EA) = 0" +
                    "     AND    US_16_EA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                             WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA_CELL AS ERROR_CELL," +
                        "            'US_16_EA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA           <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  US_16_EA <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_EA", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    US_16_EA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA_CELL AS ERROR_CELL," +
                        "            'US_16_EA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS DEDUCTION UNDER SECTION 16(III)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET US_16_TE = '0.00' WHERE US_16_TE IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  US_16_TE <> ''" +
                    "     AND    ISNUMERIC(US_16_TE) = 0" +
                    "     AND    US_16_TE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                             WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE_CELL AS ERROR_CELL," +
                        "            'US_16_TE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  US_16_TE      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_TE", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    US_16_TE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE_CELL AS ERROR_CELL," +
                        "            'US_16_TE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_TE", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS DEDUCTION UNDER SECTION 16(ia)

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET US_16_IA = '0.00' WHERE US_16_IA IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  US_16_IA <> ''" +
                    "     AND    ISNUMERIC(US_16_IA) = 0" +
                    "     AND    US_16_IA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                             WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL AS ERROR_CELL," +
                        "            'US_16_IA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA           <> ''" +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  US_16_IA      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_IA", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    US_16_IA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL AS ERROR_CELL," +
                        "            'US_16_IA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_IA", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //---------------
                // SHOULD NOT BE MORE THAN 40K
                strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex);
                double dblUS16ia = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //--
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  US_16_IA      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_IA", J_SQLColFormat.ConvertToMoney) + " > " + dblUS16ia + " " +
                        "     AND    US_16_IA_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL AS ERROR_CELL," +
                        "            'US_16_IA_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA <> ''  " +
                        "     AND    " + cmnService.J_SQLDBFormat("US_16_IA", J_SQLColFormat.ConvertToMoney) + " > " + dblUS16ia + " ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                
                #region GROSS TOTAL DEDUCTION UNDER SECTION 16

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET US_16_AGGREGATE = '0.00' WHERE US_16_AGGREGATE IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                         @" SET   US_16_AGGREGATE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA", J_SQLColFormat.ConvertToMoney) + " ";
                dmlService.J_ExecSql(strSQL);
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  US_16_AGGREGATE <> ''" +
                        "     AND    (" + cmnService.J_SQLDBFormat("US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney) + ") > (" + cmnService.J_SQLDBFormat("BALANCE_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " + cmnService.J_SQLDBFormat("PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) + ")" +
                        "     AND    US_16_AGGREGATE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE_CELL AS ERROR_CELL," +
                        "            'US_16_AGGREGATE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney) + " > (" + cmnService.J_SQLDBFormat("BALANCE_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " + cmnService.J_SQLDBFormat("PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) + ") ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion


                #region INCOME CHARGEABLE UNDER HEAD SALARIES

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET INCOME_CHARGEABLE = '0.00' WHERE INCOME_CHARGEABLE IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET PREV_EMPLOYER_SALARY = '0.00' WHERE PREV_EMPLOYER_SALARY IS NULL";
                dmlService.J_ExecSql(strSQL);

                // NUMERIC CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  INCOME_CHARGEABLE           <> ''" +
                    "     AND    ISNUMERIC(INCOME_CHARGEABLE) = 0" +
                    "     AND    INCOME_CHARGEABLE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                    WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE_CELL AS ERROR_CELL," +
                        "            'INCOME_CHARGEABLE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                    IS NULL " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                 @" SET   INCOME_CHARGEABLE = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".BALANCE_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                        cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) + ") - (" +
                                        cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion

                #region INCOME OR LOSS FROM HOUSE PROPERTY
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET INCOME_LOSS_HOUSE_PROPERTY = '0.00' WHERE INCOME_LOSS_HOUSE_PROPERTY IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  INCOME_LOSS_HOUSE_PROPERTY <> ''" +
                    "     AND    ISNUMERIC(INCOME_LOSS_HOUSE_PROPERTY) = 0" +
                    "     AND    INCOME_LOSS_HOUSE_PROPERTY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY_CELL AS ERROR_CELL," +
                        "            'INCOME_LOSS_HOUSE_PROPERTY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY           <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion
                //
                #region INCOME FROM OTHER SOURCES

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET INCOME_OTHER_SOURCES = '0.00' WHERE INCOME_OTHER_SOURCES IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  INCOME_OTHER_SOURCES <> ''" +
                    "     AND    ISNUMERIC(INCOME_OTHER_SOURCES) = 0" +
                    "     AND    INCOME_OTHER_SOURCES_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES_CELL AS ERROR_CELL," +
                        "            'INCOME_OTHER_SOURCES_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES           <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  INCOME_OTHER_SOURCES <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    INCOME_OTHER_SOURCES_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                    WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 20-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES_CELL AS ERROR_CELL," +
                        "            'INCOME_OTHER_SOURCES_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL     IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region GROSS TOTAL INCOME

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET GROSS_TOTAL_INCOME = '0.00' WHERE GROSS_TOTAL_INCOME IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                @" SET   GROSS_TOTAL_INCOME = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);
                //                
                #endregion

                #region CHAPTER VIA UNDER SECTION 80C

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80C_DED_AMOUNT = '0.00' WHERE CVIA_SEC80C_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80C_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80C_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80C_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80C_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80C_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80C_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80C_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80C_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80CCC

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80CCC_DED_AMOUNT = '0.00' WHERE CVIA_SEC80CCC_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80CCC_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80CCC_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80CCC_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCC_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80CCC_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80CCC_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80CCC_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCC_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80CCD_1

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80CCD_1_DED_AMOUNT = '0.00' WHERE CVIA_SEC80CCD_1_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80CCD_1_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80CCD_1_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80CCD_1_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_1_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80CCD_1_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80CCD_1_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_1_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA TOTAL SECTION 80C & 80CCC & 80CCD_1

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = '0.00' WHERE CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //--
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                @" SET   CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);
                //--
                //-- LIMIT CHECK
                //strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex);
                //double dblUS16ia = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                double dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 = 150000;
                //--
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                        @"  SET     CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = '" + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 + "' " +
                        "   WHERE  " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " > " + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819;
                dmlService.J_ExecSql(strSQL);
                //--
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  " + cmnService.J_SQLDBFormat("CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " > " + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 + " " +
                        "     AND    CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                            WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " > " + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819;
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80CCD_1B

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80CCD_1B_DED_AMOUNT = '0.00' WHERE CVIA_SEC80CCD_1B_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80CCD_1B_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80CCD_1B_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80CCD_1B_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_1B_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80CCD_1B_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80CCD_1B_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80CCD_1B_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_1B_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80CCD_2

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80CCD_2_DED_AMOUNT = '0.00' WHERE CVIA_SEC80CCD_2_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80CCD_2_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80CCD_2_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80CCD_2_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_2_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80CCD_2_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80CCD_2_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80CCD_2_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80CCD_2_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80D

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80D_DED_AMOUNT = '0.00' WHERE CVIA_SEC80D_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80D_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80D_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80D_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80D_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80D_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80D_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80D_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80D_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80E

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80E_DED_AMOUNT = '0.00' WHERE CVIA_SEC80E_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80E_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80E_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80E_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80E_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80E_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80E_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80E_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80E_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80G

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80G_DED_AMOUNT = '0.00' WHERE CVIA_SEC80G_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80G_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80G_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80G_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80G_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80G_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80G_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80G_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80G_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER SECTION 80TTA

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_SEC80TTA_DED_AMOUNT = '0.00' WHERE CVIA_SEC80TTA_DED_AMOUNT IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_SEC80TTA_DED_AMOUNT <> ''" +
                    "     AND    ISNUMERIC(CVIA_SEC80TTA_DED_AMOUNT) = 0" +
                    "     AND    CVIA_SEC80TTA_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80TTA_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_SEC80TTA_DED_AMOUNT      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_SEC80TTA_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_SEC80TTA_DED_AMOUNT_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT_CELL AS ERROR_CELL," +
                        "            'CVIA_SEC80TTA_DED_AMOUNT_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region CHAPTER VIA UNDER OTHERS SECTION

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_OTH_DED_TOTAL = '0.00' WHERE CVIA_OTH_DED_TOTAL IS NULL";
                dmlService.J_ExecSql(strSQL);

                //--- numeric check when not blank
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                    "     WHERE  CVIA_OTH_DED_TOTAL <> ''" +
                    "     AND    ISNUMERIC(CVIA_OTH_DED_TOTAL) = 0" +
                    "     AND    CVIA_OTH_DED_TOTAL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                         WHERE  ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL_CELL AS ERROR_CELL," +
                        "            'CVIA_OTH_DED_TOTAL_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL     <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // SHOULD BE +ve
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "" +
                        "     WHERE  CVIA_OTH_DED_TOTAL      <> ''" +
                        "     AND    " + cmnService.J_SQLDBFormat("CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney) + " < 0 " +
                        "     AND    CVIA_OTH_DED_TOTAL_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "                                                        WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 21-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL_CELL AS ERROR_CELL," +
                        "            'CVIA_OTH_DED_TOTAL_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + T_FormNo.F24QSalaryDetails + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL                           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL     <> '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion


                #region GROSS TOTAL DEDUCTION UNDER CHAPTER VIA 

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " SET CVIA_DED_TOTAL = '0.00' WHERE CVIA_DED_TOTAL IS NULL";
                dmlService.J_ExecSql(strSQL);

                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC +
                @" SET   CVIA_DED_TOTAL = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                       cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);

                #endregion



                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                return false;
            }
        }
        #endregion 


        #region SAVE_ERR
        private bool SAVE_ERR(string ErrType, string ErrCell, string ErrColumn, string ErrColor, string ErrSheet, string ErrFormNo)
        {
            try
            {
                //
                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (ERR_TYPE," +
                    "                                      ERR_CELL," +
                    "                                      ERR_COLUMN," +
                    "                                      ERR_COLOR," +
                    "                                      ERR_SHEET," +
                    "                                      ERR_FORM_NO) " +
                    "     VALUES                          ('" + ErrType + "'," +
                    "                                      '" + ErrCell + "'," +
                    "                                      '" + ErrColumn + "'," +
                    "                                      '" + ErrColor + "'," +
                    "                                      '" + ErrSheet + "'," +
                    "                                      '" + ErrFormNo + "')";
                //
                dmlService.J_ExecSql(strSQL);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region KILL EXCEL
        private bool KILL_EXCEL()
        {
            try
            {
                //--
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                //{
                //    if (process.MainModule.ModuleName.ToUpper().Equals("EXCEL.EXE"))
                //    {
                //        process.Kill();
                //        //process.Close();
                //        //process.Dispose();
                //        break;
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    if (ws.Name.ToString().Trim() == ExcelSheet)
                    {
                        ws.Delete();
                        break;
                    }
                } 

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                
                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet ;
                
                wsnew.Name = strErrorWorksheetName;
               
                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion

        #region WRITE ERROR WORKSHEET
        private bool WRITE_ERROR_WORKSHEET(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Sheet_Name.CHALLAN_DETAILS;
            int intSkipIF = 0;
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;
                
                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                wsnew.get_Range("B:B", m).ColumnWidth = 150;
                wsnew.get_Range("B2", m).Value2 = "Error Validations";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                wsnew.get_Range("B2", m).Font.Size = 15;
                //
                wsnew.get_Range("B4", m).Value2 = T_Sheet_Name.CHALLAN_DETAILS;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);               
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET," +
                    "            ERR_VALIDATION_ID";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    if (intSkipIF == 0)
                    {
                        strMatchSheetName = drdGetErrorSheetRecord["ERR_SHEET"].ToString();
                        if (strMatchSheetName != T_Sheet_Name.CHALLAN_DETAILS)
                        {
                            lngErrorSheetRow = lngErrorSheetRow + 2;
                            //
                            wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Sheet_Name.DEDUCTEE_DETAILS;
                            wsnew.get_Range("B" + lngErrorSheetRow, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                            //
                            lngErrorSheetRow = lngErrorSheetRow + 1;
                            intSkipIF = 1;
                        }
                    }
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    //wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                    //
                    //prgBar.Value = prgBar.Value + 1;
                    //this.Refresh();
                    //
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region WRITE ERROR WORKSHEET SD
        private bool WRITE_ERROR_WORKSHEET_SD(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Sheet_Name.SALARY_DETAILS;
            //int intSkipIF = 0;
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                wsnew.get_Range("B:B", m).ColumnWidth = 150;
                wsnew.get_Range("B2", m).Value2 = "Error Validations";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                wsnew.get_Range("B2", m).Font.Size = 15;
                //
                wsnew.get_Range("B4", m).Value2 = strMatchSheetName;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET," +
                    "            ERR_VALIDATION_ID";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    //if (intSkipIF == 0)
                    //{
                    //    strMatchSheetName = drdGetErrorSheetRecord["ERR_SHEET"].ToString();
                    //    if (strMatchSheetName != T_Sheet_Name.CHALLAN_DETAILS)
                    //    {
                    //        lngErrorSheetRow = lngErrorSheetRow + 2;
                    //        //
                    //        wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Sheet_Name.DEDUCTEE_DETAILS;
                    //        wsnew.get_Range("B" + lngErrorSheetRow, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                    //        //
                    //        lngErrorSheetRow = lngErrorSheetRow + 1;
                    //        intSkipIF = 1;
                    //    }
                    //}
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region COLOR ERROR CELLS
        private bool COLOR_ERROR_CELLS(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            //--
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_COLOR," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[drdGetErrorSheetRecord["ERR_SHEET"].ToString()];
                    //
                    wsnew.get_Range(drdGetErrorSheetRecord["ERR_CELL"].ToString(), m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName(drdGetErrorSheetRecord["ERR_COLOR"].ToString()));
                    //
                    wb.Save();
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        #endregion

        #region INITIALIZE COLOR ERROR CELLS
        private bool INITIALIZE_COLOR_ERROR_CELLS(string ExcelFilePath, string FormNo)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            //--
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_COLOR," +
                    "            ERR_SHEET," +
                    "            ERR_DESC," +
                    "            ERR_FORM_NO " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    if (FormNo == drdGetErrorSheetRecord["ERR_FORM_NO"].ToString())
                    {
                        Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[drdGetErrorSheetRecord["ERR_SHEET"].ToString()];
                        //
                        wsnew.get_Range(drdGetErrorSheetRecord["ERR_CELL"].ToString(), m).Interior.ColorIndex = -4142;
                        //
                        wb.Save();
                    }
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region LOAD IMPORT INTERFACE
        private bool LOAD_IMPORT_INTERFACE()
        {
            try
            {
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                {
                    //
                    blnOpenTabPage = true;
                    tbcExcelImport.SelectTab(tbpImportSalaryDetails);
                    //
                    lblSDFinancialYear.Text = cmbFinancialYear.Text;
                    lblSDCompany.Text = cmbCompany.Text;
                    //
                    lblFormNoSD.Text = "24Q";
                    //
                    strSQL = "SELECT TAN_NO " +
                        "     FROM   MST_COMPANY " +
                        "     WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                    lblSDTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //-- EXISTING RECORDS
                    strSQL = "SELECT COUNT(*) FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " AND ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    lblExistingRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    //-- TOTAL RECORDS
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "";
                    lblTotalEmployeeRecordsInExcel.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    if (cmnService.J_ReturnInt64Value(lblTotalEmployeeRecordsInExcel.Text) > 0)
                        lblTotalEmployeeRecordsInExcel.BackColor = Color.LightGreen;
                    else
                        lblTotalEmployeeRecordsInExcel.BackColor = System.Drawing.Color.Cornsilk;
                    //
                    //-- TOTAL RECORDS TO BE ADDED
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 0";
                    lblNewRecordsToBeAdded.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    if (cmnService.J_ReturnInt64Value(lblNewRecordsToBeAdded.Text) > 0)
                        lblNewRecordsToBeAdded.BackColor = Color.LightGreen;
                    else
                        lblNewRecordsToBeAdded.BackColor = System.Drawing.Color.Cornsilk;
                    //
                    //lblNewRecordsToBeAdded.Text = Convert.ToString(lngNewDeducteesCreated);
                    //-- TOTAL RECORDS TO BE MODIFIED
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 1";
                    lblRecordsToBeModified.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    if (cmnService.J_ReturnInt64Value(lblRecordsToBeModified.Text) > 0)
                        lblRecordsToBeModified.BackColor = Color.LightGreen;
                    else
                        lblRecordsToBeModified.BackColor = System.Drawing.Color.Cornsilk;
                    //
                    //lblRecordsToBeModified.Text = Convert.ToString(lngNewDeducteesCreated);
                    //--
                    BtnAdd.Visible = false;
                    BtnExit.Location = new Point(502, 13);
                    //--
                    LoadSD16Grid();
                    //
                    
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region LoadSDGrid
        private void LoadSDGrid()
        {
            //----------------------------------------------------------
            string[,] strMatrixSD =  {{"DeducteeDetailsID", "0", "", "Right", "", "", ""},
                                  {"Srl No.", "25", "S", "", "", "", ""},
                                  {"PAN", "85", "S", "", "", "", ""},
                                  {"Employee Name", "240", "S", "", "", "", ""},
                                  {"Category", "100", "S", "", "", "", ""},
                                  {"From Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                  {"To Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                  {"Total Salary", "100", "0.00", "R", "", "", "T"},
                                  {"Total TDS Deducted", "100", "0.00", "R", "", "", "T"},
                                  {"Shortfall/Excess", "100", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            string[,] strCategoryMatrix = {{"CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                           {"CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                           {"CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                           {"CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                           {"CATEGORY = ''", "F", "", "T"}};
            //
            //-----------------------------------------------------------
            strOrderBy = "RUNNING_SERIAL_NO_ORDER";
            strQuery = "SELECT SALARY_DETAILS_ID        AS DETAILS_ID," +
                      "       RUNNING_SERIAL_NO_ORDER   AS SERIAL_NO," +
                      "       EMPLOYEE_PAN              AS PAN," +
                      "       EMPLOYEE_NAME             AS NAME," +
                      "     " + cmnService.J_SQLDBFormat(strCategoryMatrix, J_SQLColFormat.Case_End) + " AS CAT," +
                      "     " + cmnService.J_SQLDBFormat("FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS FROM_D," +
                      "     " + cmnService.J_SQLDBFormat("TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS TO_D," +
                      "     " + cmnService.J_SQLDBFormat("TS_BALANCE", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_SALARY," +
                      "     " + cmnService.J_SQLDBFormat("TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TDS," +
                      "     " + cmnService.J_SQLDBFormat("SHORTFALL_TAX", J_SQLColFormat.ConvertToMoney) + " AS SHORTFALL " +
                      "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " ";
            
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewSD, strSQL, strMatrixSD);       //Show Data into the Grid
        }
        #endregion

        #region LoadSD16Grid
        private void LoadSD16Grid()
        {
            //----------------------------------------------------------
            string[,] strMatrixSD =  {{"DeducteeDetailsID", "0", "", "Right", "", "", ""},
                                  {"Srl No.", "25", "S", "", "", "", ""},
                                  {"PAN", "85", "S", "", "", "", ""},
                                  {"Employee Name", "240", "S", "", "", "", ""},
                                  {"Category", "100", "S", "", "", "", ""},
                                  {"Total Salary", "150", "0.00", "R", "", "", "T"},
                                  {"Gross Total Income", "150", "0.00", "R", "", "", "T"},
                                  {"Total Taxable Income", "150", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            string[,] strCategoryMatrix = {{"CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                           {"CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                           {"CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                           {"CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                           {"CATEGORY = ''", "F", "", "T"}};
            //
            //-----------------------------------------------------------
            strOrderBy = "SALARY_DETAILS_ID";
            strQuery = "SELECT SALARY_DETAILS_ID                AS DETAILS_ID," +
                      "       ''   AS SERIAL_NO," +
                      "       EMPLOYEE_PAN              AS PAN," +
                      "       EMPLOYEE_NAME             AS NAME," +
                      "     " + cmnService.J_SQLDBFormat(strCategoryMatrix, J_SQLColFormat.Case_End) + " AS CAT," +
                      "     " + cmnService.J_SQLDBFormat("TS_BALANCE", J_SQLColFormat.ConvertToMoney) + " AS TS_BALANCE, " +
                      "     " + cmnService.J_SQLDBFormat("GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " AS GROSS_TOTAL_INCOME, " +
                      "     " + cmnService.J_SQLDBFormat("TOTAL_TAXABLE_INCOME", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TAXABLE_INCOME  " +
                      "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " ";

            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewSD, strSQL, strMatrixSD);       //Show Data into the Grid
        }
        #endregion

        #region LoadChallanGrid
        private void LoadChallanGrid()
        {
            try
            {
                //--
                #region ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "(ERR_DESC)
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "", "ERR_DESC") == false)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " ADD ERR_DESC VARCHAR(255) DEFAULT ''";                    
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " ADD COLUMN ERR_DESC TEXT(255) DEFAULT \"\"";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " SET ERR_DESC =''";
                dmlService.J_ExecSql(strSQL);
                //--                
                //string strErr = "Challan total is greater than Deductee total";
                string strErr = "Deductee total is greater than Challan total"; //-- 2016/01/19
                if (TDSMAN.Classes.TDSMAN.T_CopyInterestAllocated == true)
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY = 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY <> 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + "- " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                               //AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                else
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY = 'Y'
                               AND    (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("CTRL_TOT_TAX", J_SQLColFormat.ConvertToMoney) + ") < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY <> 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                               //AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                #endregion
                //--
                #region ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "(TOT_TAX)
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "", "TOT_TAX") == false)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " ADD TOT_TAX INT DEFAULT 0";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " ADD COLUMN TOT_TAX NUMBER DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET TOT_TAX = 0";
                dmlService.J_ExecSql(strSQL);
                //--
                if (TDSMAN.Classes.TDSMAN.T_CopyInterestAllocated == true)
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET " +
                              "        TOT_TAX = (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") " +
                              "WHERE  BOOK_ENTRY = 'Y'";
//                               AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET " +
                              "        TOT_TAX = (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") " +
                              " WHERE  BOOK_ENTRY <> 'Y'";
//                               AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                else
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET " +
                               "       TOT_TAX = " + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " " +
                               "WHERE  BOOK_ENTRY = 'Y'";
//                               AND    (CDBL(TOTAL_TAX_DEPOSITED) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + @" SET " +
                              "        TOT_TAX = (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + ") " +
                              "WHERE  BOOK_ENTRY <> 'Y'"; 
//                               AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                //--
                #endregion
                //--
                string strSectionSize = "0";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                    strSectionSize = "0";
                else
                    strSectionSize = "55";
                //-----------------------------------------------------------
                string[,] strMatrixChallanDetails = {{"ChallanID", "0", "", "Right", "", "", ""},
                                        {"Sl No.", "50", "S", "", "", "", ""},
                                        {"Section No.", strSectionSize, "S", "", "", "", ""},
                                        {"Challan/Transfer Voucher No.", "110", "S", "", "", "", ""},
                                        {"Deposit Date", "100", "dd/MM/yyyy", "", "", "", ""},
                                        {"BSR Code/24G No.", "100", "S", "", "", "", ""},
                                        {"No of Deductees", "100", "0", "R", "", "", "T"},
                                        {"Tax", "100", "0.00", "R", "", "", "T"},
                                        {"Deductee Total", "100", "0.00", "R", "", "", "T"},
                                        {"Difference", "100", "0.00", "R", "", "", "T"},
                                        {"Status", "150", "S", "L", "", "", "T"},
                                        {"", "0", "", "R", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //-- by ANIK 2014/01/21
                //string[,] strError = {{"(CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) - " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX) < 0 ", "F", "Error!!", "T"},
                //                        {"JAYA", "F", "", "T"}};
                string[,] strError = {{TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC <> '' ", "F", "Error!! <Click to know more>", "T"},
                                      {TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC = '' ", "F", "", "T"}};

                strOrderBy = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CHALLAN_DETAILS_ID";

                #region Commented by Shrey Kejriwal on 05/07/2013
                //strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CHALLAN_DETAILS_ID AS CHALLAN_ID," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO   AS SL_NO," +
                //          "       MST_SECTION.SECTION_NO                   AS SECTION_NO," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TRF_VCH_CHLN_NO     AS CHALLAN_TRF_NO," +
                //          "     " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".DATE_TAX_DEPOSITED", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".BSR_CODE            AS BSR_CODE," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".NO_OF_DEDUCTEES     AS NO_OF_DEDUCTEES," +
                //          "       CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) AS TOT_TAX," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                //          "      (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) - " +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX)       AS DIFF," +
                //          "    " + cmnService.J_SQLDBFormat(strError, J_SQLColFormat.Case_End) + " AS STATUS " +
                //          "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ", " +
                //          "       MST_SECTION " +
                //          "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".SECTION_NAME = MST_SECTION.SECTION_NO ";
                #endregion

                strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CHALLAN_DETAILS_ID AS CHALLAN_ID," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO   AS SL_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".SECTION_NAME        AS SECTION_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TRF_VCH_CHLN_NO     AS CHALLAN_TRF_NO," +
                          "     " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".DATE_TAX_DEPOSITED", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".BSR_CODE            AS BSR_CODE," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".NO_OF_DEDUCTEES     AS NO_OF_DEDUCTEES," +
                          //"       CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) AS TOT_TAX," +
                          "       TOT_TAX                                  AS TOT_TAX," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                    //"      (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TOTAL_TAX_DEPOSITED) - " +
                    //"       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX)       AS DIFF," +
                          "      (TOT_TAX - " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CTRL_TOT_TAX)       AS DIFF," +
                          //"       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC            AS STATUS " +
                          "    " + cmnService.J_SQLDBFormat(strError, J_SQLColFormat.Case_End) + " AS STATUS, " +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC AS ERR_DESC " +
                          "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " ";

                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
                dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQL, strMatrixChallanDetails);       //Show Data into the Grid
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion
        
        #region LoadDeducteeDetailsGrid
        private bool LoadDeducteeDetailsGrid(long ChallanId)
        {
            try
            {

                string strSectionSize = "0";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) < T_FinancialYearID.F2013_14ID)
                    strSectionSize = "0";
                else
                    strSectionSize = "55";

                //-----------------------------------------------------------
                string[,] strMatrixDeducteeDetails = {{"DeducteeDetailsID", "0", "", "Right", "", "", ""},
                                        {"Srl No.", "100", "S", "", "", "", ""},
                                        {"PAN", "100", "S", "", "", "", ""},
                                        {"Name", "250", "S", "", "", "", ""},
                                        {"Section", strSectionSize, "S", "", "", "", ""},
                                        {"Date", "100", "dd/MM/yyyy", "", "", "", ""},
                                        {"Amount Paid", "100", "0.00", "R", "", "", "T"},
                                        {"Total TDS", "100", "0.00", "R", "", "", "T"},
                                        {"TDS Deposited", "100", "0.00", "R", "", "", "T"}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                strOrderBy = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_DETAILS_ID";
                strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_DETAILS_ID      AS DEDUCTEE_DETAILS_ID," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_SERIAL_NO        AS SERIAL_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_PAN              AS PAN," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_NAME             AS DEDUCTEE_NAME," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".[SECTION]                 AS SECTION_NAME," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DATE_PAYMENT", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_PAYMENT," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + " AS AMOUNT_PAID," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TAX_DEDUCTED," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TAX_DEPOSITED ";
                if (ChallanId > 0)
                {
                    strQuery = strQuery + "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ", " +
                    "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " " +
                    "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CHALLAN_SERIAL_NO = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO " +
                    "AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".CHALLAN_DETAILS_ID = " + ChallanId;
                }
                else
                {
                    strQuery = strQuery + "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " ";
                }
                //-----------------------------------------------------------
                strSQL = strQuery + " ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewDeductee, strSQL, strMatrixDeducteeDetails);       //Show Data into the Grid
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region NEW DEDUCTEE MASTER CREATED
        private bool NEW_DEDUCTEE_MASTER_CREATED()
        {
            try
            {
                #region F24QForm16SalaryDetails
                //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(DEDUCTEE_NAME, DEDUCTEE_PAN, COMPANY_ID) " +
                            "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME," +
                            "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN," +
                            "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                            "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                            "LEFT JOIN (SELECT EMPLOYEE_PAN " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                            "           FROM   MST_EMPLOYEE " +
                            "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") AS EMPLOYEE_MASTER " +
                            "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN = EMPLOYEE_MASTER.EMPLOYEE_PAN " +
                            "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                            "AND    EMPLOYEE_MASTER.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                dmlService.J_ExecSql(strSQL);

                //FOR PANNOTAVBL
                //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(DEDUCTEE_NAME, DEDUCTEE_PAN, COMPANY_ID) " +
                            "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME," +
                            "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN," +
                            "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                            "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                            "LEFT JOIN (SELECT EMPLOYEE_NAME " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                            "           FROM   MST_EMPLOYEE " +
                            "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                            "           AND    EMPLOYEE_PAN = 'PANNOTAVBL') AS EMPLOYEE_MASTER " +
                            "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME = EMPLOYEE_MASTER.EMPLOYEE_NAME " +
                            "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN = 'PANNOTAVBL' " +
                            "AND    EMPLOYEE_MASTER.EMPLOYEE_NAME IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                dmlService.J_ExecSql(strSQL);
                #endregion
                //
                strSQL = " SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                lngNewDeducteesCreated = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                return true;
            }
            catch
            {
                return false;
            }
        }


        #endregion

        #region INSERT_MASTER_DATA
        public bool INSERT_MASTER_DATA()
        {
            try
            {                
                #region F24QSalaryDetailsForm16
                //INSERTING NEW EMPLOYEES FOUND IN EXCEL TO THE MASTER
                strSQL = "INSERT INTO MST_EMPLOYEE " +
                    "                 (EMPLOYEE_NAME," +
                    "                  EMPLOYEE_PAN," +
                    "                  COMPANY_ID," +
                    "                  GROUP_ID) " +
                    "      SELECT      DEDUCTEE_NAME," +
                    "                  DEDUCTEE_PAN," +
                    "                  COMPANY_ID," +
                    "                  '" + TDSMAN.Classes.TDSMAN.T_pGroupId + "'" +
                    "      FROM        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;

                //ADD EMPLOYEE_MASTER_ID IN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + " FOR LINKING WITH EMPLOYEE MASTER

                //FOR VALID PAN -- LINKING PAN WITH MASTER
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "         INNER JOIN MST_EMPLOYEE " +
                    "                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                    "       SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_ID     = MST_EMPLOYEE.EMPLOYEE_ID " +
                    "       WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN    <> 'PANNOTAVBL'" +
                    "       AND    MST_EMPLOYEE.COMPANY_ID             = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;

                // FOR PANNOTAVBL -- LINKING NAME WITH MASTER
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "         INNER JOIN MST_EMPLOYEE " +
                    "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                    "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID " +
                    "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;

                // UPDATE CATEGORY
                // FOR  PANNOTAVBL
                strSQL = "UPDATE MST_EMPLOYEE " +
                    "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                    "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CATEGORY " +
                    "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;

                // FOR  PANAVBL
                strSQL = "UPDATE MST_EMPLOYEE " +
                    "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN  = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                    "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CATEGORY " +
                    "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN     <> 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            <> 'PANNOTAVBL'" +
                    "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;
                #endregion
                                
                return true;
                //
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region INSERT_SALARY_DATA_18-19
        public bool INSERT_SALARY_DATA_18_19(long BasicInfoID)
        {
            try
            {
                //--
                string strUpper = "";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    strUpper = "UPPER";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strUpper = "UCASE";
                }
                //--
                //
                string[,] strGS_SEC17_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_1 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_2 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_GS_SEC_17_3 = ''" , "F", "0", "F"}};
                //
                string[,] strTS_BALANCE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_BALANCE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_BALANCE", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TS_BALANCE = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_5_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_5_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10A_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10A_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10AA_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_10AA_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_13A_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_13A_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_OTHERS_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".SEC10_OTHERS_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_EA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_EA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_TE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_TE = ''" , "F", "0", "F"}};
                //-- 2018/12/24
                string[,] strUS_16_IA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_IA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_AGGREGATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".US_16_AGGREGATE = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_CHARGEABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_CHARGEABLE = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_LOSS_HOUSE_PROPERTY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_LOSS_HOUSE_PROPERTY = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_OTHER_SOURCES = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".INCOME_OTHER_SOURCES = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".GROSS_TOTAL_INCOME <> ''" , "F", "ROUND( " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + ",2)", "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".GROSS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80C_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCC_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCC_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_1_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_1B_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_1B_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_2_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80CCD_2_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80D_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80D_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80E_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80E_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80G_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80G_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80TTA_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_SEC80TTA_DED_AMOUNT = ''" , "F", "0", "F"}};
                //                
                string[,] strCVIA_OTH_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_OTH_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_DED_TOTAL", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CVIA_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                //string[,] strTOT_TAXABLE_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TOTAL_TAXABLE_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TOTAL_TAXABLE_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                //                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".TOTAL_TAXABLE_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strPREV_EMPLOYER_SALARY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".PREV_EMPLOYER_SALARY = ''" , "F", "0", "F"}};
                //--------------------------------------------
                strSQL = "INSERT INTO TRN_SALARY_DETAILS_PROJECTED_FORM16 (" +
                        "             SL_NO," +
                        "             COMPANY_ID," +
                        "             ASST_ID," +
                        "             EMPLOYEE_ID," +
                        "             TS_GS_SEC_17_1," +
                        "             TS_GS_SEC_17_2," +
                        "             TS_GS_SEC_17_3," +
                        "             TOTAL_SALARY," +
                        "             SEC10_5_AMOUNT," +
                        "             SEC10_10_AMOUNT," +
                        "             SEC10_10A_AMOUNT," +
                        "             SEC10_10AA_AMOUNT," +
                        "             SEC10_13A_AMOUNT," +
                        "             SEC10_OTHER_AMOUNT," +
                        "             BALANCE_AMOUNT," +
                        "             US_16_EA," +
                        "             US_16_TE," +
                        "             US_16_IA," +
                        "             US_16_AGGREGATE," +
                        "             INCOME_SALARY," +
                        "             INCOME_LOSS_HOUSE_PROPERTY," +
                        "             OTHER_INCOME," +
                        "             GROSS_TOTAL_INCOME," +
                        "             CVIA_SEC80C_DED_TOTAL," +
                        "             CVIA_SEC80CCC_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_1_DED_AMOUNT," +
                        "             CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_1B_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_2_DED_AMOUNT," +
                        "             CVIA_SEC80D_DED_AMOUNT," +
                        "             CVIA_SEC80E_DED_AMOUNT," +
                        "             CVIA_SEC80G_DED_AMOUNT," +
                        "             CVIA_SEC80TTA_DED_AMOUNT," +
                        "             CVIA_OTHER_SEC_DED_AMOUNT," +
                        "             CVIA_TOTAL_DED_AMOUNT," +
                        "             PREV_EMPLOYER_SALARY," +
                        "             TOT_TAXABLE_INCOME," +
                        "             CREATE_DATE_TIME," +
                        "             CREATE_SETUP_ID," +
                        "             ENTRY_MODE," +
                        "             EMPLOYEE_PAN," +
                        "             EMPLOYEE_NAME," +
                        "             CATEGORY";
                strSQL = strSQL + ") " +
                    "   SELECT    SALARY_DETAILS_ID," +
                    "            " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + "," +
                    "            " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + "," +
                    "             EMPLOYEE_ID," +
                    "   " + cmnService.J_SQLDBFormat(strGS_SEC17_1, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_2, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_3, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTS_BALANCE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_5_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10A_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10AA_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_13A_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_OTHERS_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             BALANCE_AMOUNT," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_EA, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_TE, J_SQLColFormat.Case_End) + ",";
                    strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_IA, J_SQLColFormat.Case_End) + ",";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_AGGREGATE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_CHARGEABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_LOSS_HOUSE_PROPERTY, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_OTHER_SOURCES, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80C_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCC_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_1_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80C_CCC_CCD_1_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_1B_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_2_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80D_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80E_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80G_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80TTA_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_OTH_DED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_DED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strPREV_EMPLOYER_SALARY, J_SQLColFormat.Case_End) + "," +
                        "             TOTAL_TAXABLE_INCOME," +
                        "             " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + "," +
                        "             0," +
                        "             'E'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_PAN," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".EMPLOYEE_NAME," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CATEGORY ";
                strSQL = strSQL + "   FROM      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                       "   ORDER BY  RUNNING_SERIAL_NO_ORDER";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                GenerateSerialNoProjectedFORM16(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear .SelectedIndex)) , Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
                //--
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region CREATE NEW DEDUCTEE MASTER WORKSHEET
        private bool CREATE_NEW_DEDUCTEE_WORKSHEET(string ExcelFilePath, string ExcelSheetName)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnewdeductee = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                //wsnewdeductee.Name = strNewDeducteeWorkSheetName;
                wsnewdeductee.Name = ExcelSheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnewdeductee = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch(Exception e)
            {

                return false;
            }
        }
        #endregion

        #region WRITE NEW DEDUCTEE MASTER SHEET
        private bool WRITE_NEW_DEDUCTEE_MASTER_SHEET(string ExcelFilePath)
        {
            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                string strDedEmp = "";
                //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q || T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails || T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
                    strDedEmp = "Employee";
                //else
                //    strDedEmp = "Deductee";
                //wsnew.get_Range("B:B", m).ColumnWidth = 40;
                wsnew.get_Range("B2", "D2").MergeCells = true;
                wsnew.get_Range("B2", m).Value2 = "List of " + strDedEmp + "(s) will be added";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSteelBlue);
                wsnew.get_Range("B2", m).Font.Size = 11;
                //
                wsnew.get_Range("B4", m).Value2 = "Srl No.";
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                
                wsnew.get_Range("C4", m).Value2 = strDedEmp + " Name";
                wsnew.get_Range("C4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("C:C", m).ColumnWidth = 80;

                wsnew.get_Range("D4", m).Value2 = strDedEmp + " PAN";
                wsnew.get_Range("D4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("D:D", m).ColumnWidth = 15;
                //
                //strSQL = "SELECT DEDUCTEE_NAME," +
                //    "            DEDUCTEE_PAN" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " ";
                strSQL = "SELECT EMPLOYEE_NAME," +
                    "            EMPLOYEE_PAN" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 0 ";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                int intSrlNo = 1;
                while (drdGetErrorSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = intSrlNo++;
                    wsnew.get_Range("C" + lngErrorSheetRow, m).Value2 = drdGetErrorSheetRecord["EMPLOYEE_NAME"].ToString();
                    wsnew.get_Range("D" + lngErrorSheetRow, m).Value2 = drdGetErrorSheetRecord["EMPLOYEE_PAN"].ToString();
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                    //
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region WRITE MODIFY DEDUCTEE MASTER SHEET
        private bool WRITE_MODIFY_DEDUCTEE_MASTER_SHEET(string ExcelFilePath)
        {
            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                string strDedEmp = "";
                //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24Q || T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails || T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
                strDedEmp = "Employee";
                //else
                //    strDedEmp = "Deductee";
                //wsnew.get_Range("B:B", m).ColumnWidth = 40;
                wsnew.get_Range("B2", "D2").MergeCells = true;
                wsnew.get_Range("B2", m).Value2 = "List of " + strDedEmp + "(s) will be modified";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSteelBlue);
                wsnew.get_Range("B2", m).Font.Size = 11;
                //
                wsnew.get_Range("B4", m).Value2 = "Srl No.";
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);

                wsnew.get_Range("C4", m).Value2 = strDedEmp + " Name";
                wsnew.get_Range("C4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("C:C", m).ColumnWidth = 80;

                wsnew.get_Range("D4", m).Value2 = strDedEmp + " PAN";
                wsnew.get_Range("D4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("D:D", m).ColumnWidth = 15;
                //
                strSQL = "SELECT EMPLOYEE_NAME," +
                    "            EMPLOYEE_PAN" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " WHERE EXCEL_DELETE_FLAG = 1 ";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                int intSrlNo = 1;
                while (drdGetErrorSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = intSrlNo++;
                    wsnew.get_Range("C" + lngErrorSheetRow, m).Value2 = drdGetErrorSheetRecord["EMPLOYEE_NAME"].ToString();
                    wsnew.get_Range("D" + lngErrorSheetRow, m).Value2 = drdGetErrorSheetRecord["EMPLOYEE_PAN"].ToString();
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                    //
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CheckExcelStructure
        private bool CheckExcelStructure(string ExcelFilePath)
        {
            try
            {
                #region F24Q
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, null, con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //-----------------
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Employee Serial No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "PAN of the Employee", con) == false)
                {
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Name of the Employee", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Category of the Employee", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec 17(1)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec 17(2)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec 17(3)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Total Salary", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec10(5)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec10(10)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec10(10A)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec10(10AA)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Sec10(13A)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Any Other exemption u/s Sec10", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Balance", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Deduction under section 16(ii)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Deduction under section 16(iii)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Deduction under section 16(ia)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Total Deduction under section 16(ii), 16(iii) & 16(ia)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Income Chargeable under head Salaries", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Income or loss - House Property offered for TDS", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Income-Other Sources offered for TDS", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Total Income", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80C", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80CCC", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80CCD(1)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Total deduction u/s 80C, 80CCC & 80CCD(1)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80CCD(1B)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80CCD(2)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80D", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80E", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80G", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under sec 80TTA", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction under Chapter VIA under Other sections", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Gross Total Deduction under chapter VIA", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Previous employer salary", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                #endregion
                //--
                return true;
            }
            catch(Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                con.Close();
                return false;
            }
        }
        #endregion

        #region Delete_TMP_Files
        private void Delete_TMP_Files(string FilePath)
        {
            try
            {
                foreach (string sFile in System.IO.Directory.GetFiles(Path.GetDirectoryName(FilePath)))
                {
                    if (cmnService.J_IsProcessOpen(Path.Combine(Path.GetDirectoryName(FilePath), Convert.ToString(sFile))) == false)
                        if (sFile.ToUpper().EndsWith(".TMP"))
                            System.IO.File.Delete(sFile);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region GenerateSerial
        public void GenerateSerial()
        {
            //Added by Shrey Kejriwal on 21/05/2012
            try
            {

                //Checking if duplicate serial exists

//                strSQL = @"SELECT count(DEDUCTEE_DETAILS_ID)
//                        FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "
//                        GROUP BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CHALLAN_SERIAL_NO, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_SERIAL_NO
//                        HAVING (((Count(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".[DEDUCTEE_SERIAL_NO]))>1))";

//                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

//                if (iCount > 0)
//                {
//                    lblProgressDisplayMessage.Text = "Rectifying deductee Serial Nos";

//                    // ---- DUPLICATE SERIAL NO EXISTS -----

//                    // -- RECTIFYING THE SERIAL NOS OF THOSE CHALLAN IN WHICH DUPLICATE RECORD EXISTS

//                    IDataReader reader;

//                    DMLService innerSQLDML = new DMLService();

//                    strSQL = "SELECT DEDUCTEE_DETAILS_ID, " +
//                             "       CHALLAN_SERIAL_NO " +
//                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
//                             "INNER JOIN (SELECT CHALLAN_SERIAL_NO " +
//                             "            FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
//                             "            GROUP BY CHALLAN_SERIAL_NO, DEDUCTEE_SERIAL_NO " +
//                             "            HAVING COUNT(DEDUCTEE_SERIAL_NO) > 1) AS TEMP " +
//                             "        ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CHALLAN_SERIAL_NO =  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CHALLAN_SERIAL_NO " +  
//                             "ORDER BY CHALLAN_SERIAL_NO";

//                    reader = dmlService.J_ExecSqlReturnReader(strSQL);

//                    string strPrevChallanNo = "";

//                    int intSerial = 0;

//                    while (reader.Read())
//                    {
//                        string strChallanNo = Convert.ToString(reader["CHALLAN_SERIAL_NO"]);
//                        long lngRecordID = Convert.ToInt64(reader["DEDUCTEE_DETAILS_ID"]);

//                        if (strPrevChallanNo == strChallanNo)
//                            intSerial++;
//                        else
//                            intSerial = 1;

//                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
//                                 "SET DEDUCTEE_SERIAL_NO = '" + intSerial + "' " +
//                                 "WHERE DEDUCTEE_DETAILS_ID = " + lngRecordID;

//                        innerSQLDML.J_ExecSql(strSQL);

//                        strPrevChallanNo = Convert.ToString(reader["CHALLAN_SERIAL_NO"]);
//                    }

//                    reader.Dispose();
//                    reader.Close();

//                }


                dmlService.J_BeginTransaction();

                //CREATING TEMP TABLE TO STORE THE SERIAL NOS FOR THE IDS

                if (dmlService.J_IsDatabaseObjectExist("TEMP_UPDATE_SERIAL") == true)
                {
                    strSQL = "DROP TABLE TEMP_UPDATE_SERIAL";
                    dmlService.J_ExecSql(strSQL);
                }

                strSQL = "CREATE TABLE TEMP_UPDATE_SERIAL(" +
                         "              DEDUCTEE_DETAILS_ID NUMBER DEFAULT 0, " +
                         "              DEDUCTEE_SERIAL_NO  NUMBER DEFAULT 0)";

                dmlService.J_ExecSql(strSQL, J_SQLType.DDL);

                //INSERTING THE UPDATED SERIAL NOS FOR THE DEDUCTEE IDS 

                strSQL = "INSERT INTO TEMP_UPDATE_SERIAL(DEDUCTEE_DETAILS_ID, DEDUCTEE_SERIAL_NO) " +
                         "SELECT DEDUCTEE_DETAILS_ID, " +
                         "       (SELECT COUNT(*) " +
                         "        FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " AS TEMP " +
                         "        WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".CHALLAN_SERIAL_NO = TEMP.CHALLAN_SERIAL_NO " +
                         "        AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_DETAILS_ID > " +
                         "                TEMP.DEDUCTEE_DETAILS_ID) + 1 AS DEDUCTEE_SERIAL_NO " +
                         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + "";

                dmlService.J_ExecSql(strSQL);



                //UPDATING THE SERIAL NOS IN THE DEDUCTEE DETAILS IMPORT TABLE
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                         "INNER JOIN TEMP_UPDATE_SERIAL " +
                         "       ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_DETAILS_ID = TEMP_UPDATE_SERIAL.DEDUCTEE_DETAILS_ID " +
                         "SET " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + ".DEDUCTEE_SERIAL_NO = TEMP_UPDATE_SERIAL.DEDUCTEE_SERIAL_NO";

                dmlService.J_ExecSql(strSQL);

                strSQL = "DROP TABLE TEMP_UPDATE_SERIAL";
                dmlService.J_ExecSql(strSQL);

                dmlService.J_Commit();

            }
            catch (Exception err)
            {
                dmlService.J_Rollback();
                return;
            }
        }
        #endregion

        #region GenerateSerialNoProjectedFORM16
        private void GenerateSerialNoProjectedFORM16(long FaYearId, long CompanyId)
        {
            DataSet dsetDeducteeDetails = new DataSet();
            //-- Stores info in Datarow into an array
            //Object[] cells = myDataRow.ItemArray;
            //--
            strSQL = @"SELECT SALARY_DETAILS_PROJECTED_FORM16_ID FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 WHERE ASST_ID = " + FaYearId + " AND COMPANY_ID = " + CompanyId + " ORDER BY SALARY_DETAILS_PROJECTED_FORM16_ID";
            dsetDeducteeDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
            //
            long lngSerialNo = 1;
            //
            foreach (DataRow myDataRow in dsetDeducteeDetails.Tables[0].Rows)
            {
                //-- Stores info in Datarow into an array
                //Object[] cells1 = myDataRow1.ItemArray;
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS_PROJECTED_FORM16 SET SL_NO = " + lngSerialNo + " WHERE SALARY_DETAILS_PROJECTED_FORM16_ID = " + myDataRow[0];
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    //--
                    // this.Cursor = Cursors.Default;
                    //--
                }
                lngSerialNo = lngSerialNo + 1;
            }
            //
            dsetDeducteeDetails.Dispose();
            //--
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //if (T_FormNo.F24QSalaryDetails == T_FormNo.F24QForm16SalaryDetails)
            //    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=c_kK97ReKuc&t=");
            //else //if  (T_FormNo.F24QSalaryDetails == T_FormNo.F24QSalaryDetails)
            //    System.Diagnostics.Process.Start("https://www.youtube.com/watch?time_continue=5&v=DGXmnvmZmp0");
        }
        #endregion
        
        #region CALC_TDS
        //private bool CALC_TDS()
        //{
        //    double dblCalculatedTax = 0;
        //    double dblCalculatedECess = 0, dblCalcTax = 0, dblCalculatedSurcharge = 0, dblCalculatedTaxCredit = 0, dblTaxonTotalIncomeB4TaxCredit = 0;
        //    long lngAsstID = 0, lngTrackingID = 0;
        //    double dblMonthlyTDS = 0, dblTaxToBeDeducted = 0;
        //    try
        //    {
        //        strSQL = @"SELECT   DEDUCTEE_DETAILS_ID, 
        //                            DEDUCTEE_SERIAL_NO, 
        //                            DEDUCTEE_NAME, 
        //                            CATEGORY, 
        //                            DEDUCTEE_PAN, 
        //                            TAXABLE_INCOME, 
        //                            TDS_PAID_TILL_DATE, 
        //                            TDS_TO_BE_PAID, 
        //                            MONTH_YEAR, 
        //                            TRACKING_ID 
        //                   FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " ORDER BY DEDUCTEE_DETAILS_ID ";
        //        DataSet ds = new DataSet();
        //        ds = dmlService.J_ExecSqlReturnDataSet(dmlService.J_pCommand, strSQL);
        //        //-- LOAD IN ARRAY               
        //        foreach (DataTable DtTable in ds.Tables)
        //        {
        //            foreach (DataRow dr in DtTable.Rows)
        //            {
        //                dblCalcTax = TdsMan.CalculateIncomeTaxAmount(
        //                                        Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex),
        //                                        dr["CATEGORY"].ToString(),
        //                                        cmnService.J_ReturnDoubleValue(dr["TAXABLE_INCOME"].ToString()),
        //                                        out dblCalculatedECess,
        //                                        out dblCalculatedSurcharge,
        //                                        out dblCalculatedTaxCredit,
        //                                        out dblTaxonTotalIncomeB4TaxCredit);
        //                dblCalculatedTax = Math.Round(dblCalcTax + dblCalculatedECess + dblCalculatedSurcharge, 0);
        //                long lngPaidMonth = 0;
        //                //lngPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
        //                //double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblTaxPayable.Text) / 12; 
        //                dblMonthlyTDS = Math.Round(Convert.ToDouble(Convert.ToDouble(dblCalculatedTax) / 12), 0, MidpointRounding.AwayFromZero);
        //                //--
        //                if (Convert.ToDouble(Convert.ToDouble(dblCalculatedTax) / 12) > Math.Round(Convert.ToDouble(Convert.ToDouble(dblCalculatedTax) / 12), 0, MidpointRounding.AwayFromZero))
        //                    dblMonthlyTDS = Math.Round(Convert.ToDouble(Convert.ToDouble(dblCalculatedTax) / 12) + 0.5, 0, MidpointRounding.AwayFromZero);
        //                //--
        //                //double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblMonthlyTDS.Text);
        //                //
        //                double dblCumulativeTax = 0;
        //                for (int i = 0; i < lngPaidMonth; i++)
        //                {
        //                    dblCumulativeTax = dblCumulativeTax + dblMonthlyTDS;
        //                }
        //                //
        //                double dblNetTaxToBePaid = dblCumulativeTax - cmnService.J_ReturnDoubleValue(dr["TDS_PAID_TILL_DATE"]);
        //                if (dblNetTaxToBePaid < 0)
        //                    dblTaxToBeDeducted = 0;
        //                else
        //                    dblTaxToBeDeducted = Convert.ToInt64(dblNetTaxToBePaid);
        //                //-- INSERT TO LOG 
        //                #region COMMENT
        //                // strSQL = @"INSERT INTO TRN_CALC_SD_LOG_HEADER (
        //                //       EMPLOYEE_ID,
        //                //       SETUP_ID,
        //                //       COMPANY_ID,
        //                //       MONTH_ID,
        //                //       ASST_ID,
        //                //       CATEGORY,
        //                //       GROSS_SALARY,
        //                //       TAXABLE_INCOME,
        //                //       TAX_TOTAL_INCOME,
        //                //       REBATE_AMOUNT,
        //                //       SURCHARGE_AMOUNT,
        //                //       EDU_CESS_AMOUNT,
        //                //       TAX_PAYABLE_AMOUNT,
        //                //       RELIEF_US89_AMOUNT,
        //                //       NET_TAX_PAYABLE_AMOUNT,
        //                //       MONTHLY_TDS_AMOUNT,
        //                //       TOTAL_TDS_PAID_AMOUNT,
        //                //       TOTAL_TDS_CALC) 
        //                //VALUES (
        //                //       " + lngEmployeeID + @",
        //                //       " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + @",
        //                //       " + lngCompanyID + @",
        //                //       " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @",
        //                //       " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
        //                //       '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + @"',
        //                //       " + Convert.ToDouble(txtAnnualGrossSalary.Text) + @",
        //                //       " + Convert.ToDouble(txtTaxableIncome.Text) + @",
        //                //       " + Convert.ToDouble(txtTaxTotalIncome.Text) + @",
        //                //       " + Convert.ToDouble(txtRebate.Text) + @",
        //                //       " + Convert.ToDouble(txtSurcharge.Text) + @",
        //                //       " + Convert.ToDouble(txtEducationCess.Text) + @",
        //                //       " + Convert.ToDouble(lblGrossTaxPayable.Text) + @",
        //                //       " + Convert.ToDouble(txtReliefUS89.Text) + @",
        //                //       " + Convert.ToDouble(lblTaxPayable.Text) + @",
        //                //       " + Convert.ToDouble(lblMonthlyTDS.Text) + @",
        //                //       " + Convert.ToDouble(txtTDSPaidTillDate.Text) + @",
        //                //       " + Convert.ToDouble(txtTDSToBeDeducted.Text) + @")";
        //                #endregion
        //                //--
        //                dmlService.J_BeginTransaction();
        //                //
        //                //strSQL = @"INSERT INTO TRN_CALC_SD_LOG_HEADER (
        //                //                  EMPLOYEE_NAME,
        //                //                  EMPLOYEE_PAN,
        //                //                  MONTH_ID,
        //                //                  MONTH_DESC,
        //                //                  ASST_ID,
        //                //                  CATEGORY,
        //                //                  TAXABLE_INCOME,
        //                //                  TAX_TOTAL_INCOME,
        //                //                  NET_TAX_PAYABLE_AMOUNT,
        //                //                  MONTHLY_TDS_AMOUNT,
        //                //                  TOTAL_TDS_PAID_AMOUNT,
        //                //                  TOTAL_TDS_CALC,
        //                //                  CREATE_SETUP_ID,
        //                //                  CREATE_DATE_TIME) 
        //                //      VALUES (
        //                //                  '" + dr["DEDUCTEE_NAME"].ToString() + @"',
        //                //                  '" + dr["DEDUCTEE_PAN"].ToString() + @"',
        //                //                  " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @",
        //                //                  '" + cmbMonth.Text + @"',
        //                //                  " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
        //                //                  '" + dr["CATEGORY"].ToString() + @"',
        //                //                  " + Convert.ToDouble(dr["TAXABLE_INCOME"].ToString()) + @",
        //                //                  " + Convert.ToDouble(dblCalcTax) + @",
        //                //                  " + Convert.ToDouble(dblCalculatedTax) + @",
        //                //                  " + Convert.ToDouble(dblMonthlyTDS) + @",
        //                //                  " + Convert.ToDouble(dr["TDS_PAID_TILL_DATE"]) + @",
        //                //                  " + Convert.ToDouble(dblTaxToBeDeducted) + @",
        //                //                  " + Convert.ToDouble(dblTaxToBeDeducted) + @",
        //                //                  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + @")";
        //                dmlService.J_ExecSql(strSQL);
        //                //
        //                lngTrackingID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_CALC_SD_LOG_HEADER", "CALC_SD_LOG_HEADER_ID");
        //                //--
        //                //strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + @" 
        //                //        SET     TDS                 = " + dblTaxToBeDeducted + @",
        //                //                TOTAL_TAX_DEDUCTED  = " + dblTaxToBeDeducted + @",
        //                //                TOTAL_TAX_DEPOSITED = " + dblTaxToBeDeducted + @",
        //                //                TDS_TO_BE_PAID      = " + dblTaxToBeDeducted + @",
        //                //                MONTH_YEAR          ='" + cmbMonth.Text + " - " + cmbFinancialYear.Text + @"',
        //                //                TRACKING_ID         = " + lngTrackingID + @" 
        //                //        WHERE DEDUCTEE_DETAILS_ID  = " + dr["DEDUCTEE_DETAILS_ID"].ToString();
        //                dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
        //                //
        //                dmlService.J_Commit();
        //                //--
        //            }
        //        }

        //        //double dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId, strCategory, Convert.ToDouble(txtTaxableIncome.Text), out dblCalculatedECess, out dblCalculatedSurcharge, out dblCalculatedTaxCredit, out dblTaxonTotalIncomeB4TaxCredit);

        //        return true;
        //    }
        //    catch (Exception ERR)
        //    {
        //        cmnService.J_UserMessage(ERR.Message);
        //        return false;
        //    }
        //}
        #endregion


        #region J_ReturnServerDate 
        public string J_ReturnServerDate()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }
#endregion
        
        #region WRITE CALC WORKSHEET
        private bool WRITE_CALC_WORKSHEET(string ExcelFilePath)
        {
            IDataReader drdGetCalcSD = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Sheet_Name.CHALLAN_DETAILS;
            int intSkipIF = 0;
            long lngWriteROW = 2;
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                //wb.Worksheets = T_Sheet_Name.EMPLOYEE_DETAILS;
                //sheets = (Microsoft.Office.Interop.Excel.Worksheet)T_Sheet_Name.EMPLOYEE_DETAILS;
                //wsnew.Name = strExcelSheet;
                //wsnew.Name = T_Sheet_Name.EMPLOYEE_DETAILS;

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "")));
                // 
                //wsnew.get_Range("H:H", m).ColumnWidth = 14;
                //wsnew.get_Range("H:H", m).Font.Bold = true;
                ////wsnew.get_Range("H1", m).Value2 = "TDS TO BE PAID";
                //wsnew.get_Range("H1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //wsnew.get_Range("H1", m).Font.Size = 10;
                ////
                //wsnew.get_Range("K:K", m).ColumnWidth = 14;
                //wsnew.get_Range("K:K", m).Font.Bold = true;
                ////wsnew.get_Range("H1", m).Value2 = "TDS TO BE PAID";
                //wsnew.get_Range("K1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //wsnew.get_Range("K1", m).Font.Size = 10;
                ////
                //wsnew.get_Range("L:L", m).ColumnWidth = 14;
                //wsnew.get_Range("L:L", m).Font.Bold = true;
                ////wsnew.get_Range("H1", m).Value2 = "TDS TO BE PAID";
                //wsnew.get_Range("L1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //wsnew.get_Range("L1", m).Font.Size = 10;
                //
                wsnew.get_Range("R:R", m).ColumnWidth = 20;
                wsnew.get_Range("R:R", m).Font.Bold = true;
                wsnew.get_Range("R1", m).Value2 = "MONTH-YEAR";
                wsnew.get_Range("R1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("R1", m).Font.Size = 10;
                //
                wsnew.get_Range("S:S", m).ColumnWidth = 10;
                wsnew.get_Range("S:S", m).Font.Bold = true;
                wsnew.get_Range("S1", m).Value2 = "TRACKING ID";
                wsnew.get_Range("S1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                wsnew.get_Range("S1", m).Font.Size = 10;
                //
                //wsnew.get_Range("B4", m).Value2 = T_Sheet_Name.CHALLAN_DETAILS;
                //wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //
                strSQL = "SELECT DEDUCTEE_DETAILS_ID," +
                    "            TDS_TO_BE_PAID," +
                    "            MONTH_YEAR," +
                    "            TRACKING_ID " +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS_TAXABLE_INCOME_CALC + " " +
                    "     ORDER BY DEDUCTEE_DETAILS_ID";
                //
                drdGetCalcSD = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetCalcSD == null)
                {
                    return false;
                }
                while (drdGetCalcSD.Read())
                {
                    //--
                    #region COMMENT
                    //if (intSkipIF == 0)
                    //{
                    //    strMatchSheetName = drdGetErrorSheetRecord["ERR_SHEET"].ToString();
                    //    if (strMatchSheetName != T_Sheet_Name.CHALLAN_DETAILS)
                    //    {
                    //        lngErrorSheetRow = lngErrorSheetRow + 2;
                    //        //
                    //        wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = T_Sheet_Name.DEDUCTEE_DETAILS;
                    //        wsnew.get_Range("B" + lngErrorSheetRow, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                    //        //
                    //        lngErrorSheetRow = lngErrorSheetRow + 1;
                    //        intSkipIF = 1;
                    //    }
                    //}
                    ////
                    //wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    //wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    //lngErrorSheetRow = lngErrorSheetRow + 1;
                    //
                    //prgBar.Value = prgBar.Value + 1;
                    //this.Refresh();
                    #endregion
                    //--
                    wsnew.get_Range("H" + lngWriteROW, m).Value2 = drdGetCalcSD["TDS_TO_BE_PAID"].ToString();
                    wsnew.get_Range("K" + lngWriteROW, m).Value2 = drdGetCalcSD["TDS_TO_BE_PAID"].ToString();
                    wsnew.get_Range("L" + lngWriteROW, m).Value2 = drdGetCalcSD["TDS_TO_BE_PAID"].ToString();
                    wsnew.get_Range("R" + lngWriteROW, m).Value2 = drdGetCalcSD["MONTH_YEAR"].ToString();
                    wsnew.get_Range("S" + lngWriteROW, m).Value2 = drdGetCalcSD["TRACKING_ID"].ToString();
                    //--
                    this.Refresh();
                    //--
                    lngWriteROW = lngWriteROW + 1;
                    //--
                }
                drdGetCalcSD.Close();
                drdGetCalcSD.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        #endregion

        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------------------
                //-- FINANCILAL YEAR
                //-----------------------------------------------------------------------
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select Financial Year to View the Records");
                    cmbFinancialYear.Select();
                    return;
                }
                //-----------------------------------------------------------------------
                //-- COMPANY NAME
                //-----------------------------------------------------------------------
                if (cmbCompany.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Select Company to View the Records");
                    cmbCompany.Select();
                    return;
                }
                //--
                //-- FETCH RECORDS
                double dblFinancialYearId = 0;
                double dblCompanyId = 0;
                //
                dblFinancialYearId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                dblCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                //
                if (dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 WHERE COMPANY_ID = " + dblCompanyId + " AND ASST_ID = " + dblFinancialYearId, J_QueryType.DirectQuery) == 0)
                {
                    cmnService.J_UserMessage("No records found under - \nFA Year : " + cmbFinancialYear.Text + " \nand \nCompany : " + cmbCompany.Text);
                    return;
                }
                else
                {
                    TrnForm16 objTrnForm16 = new TrnForm16(dblFinancialYearId, dblCompanyId, cmbFinancialYear.Text,cmbCompany.Text);
                    objTrnForm16.MdiParent = TrnRegularReturn.ActiveForm;
                    objTrnForm16.Show();
                    //objTrnForm16.ShowDialog();
                    this.Refresh();
                }
            }
            catch(Exception ERR)
            {

            }
        }
        #endregion


        #region btnRecordsToBeModified_Click
        private void btnRecordsToBeModified_Click(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnInt64Value(lblRecordsToBeModified.Text) == 0)
            {
                cmnService.J_UserMessage("No Employee(s) to be Modified");
                return;
            }
            //
            if (File.Exists(txtExcelPath.Text) == true)
            {
                System.Diagnostics.Process.Start(txtExcelPath.Text);
            }
            //-----------------------------------------------
        }
        #endregion


        #region btnDownloadExcelFormatCalcTDS_MouseMove
        private void btnDownloadExcelFormatCalcTDS_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnDownloadExcelFormatCalcTDS, "Get Blank Excel structure");
        }
        #endregion


        #region btnRecordsToBeAdded_MouseMove
        private void btnRecordsToBeAdded_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnRecordsToBeAdded, "View Employee(s) records that will be added");
        }
        #endregion

        #region btnRecordsToBeModified_MouseMove
        private void btnRecordsToBeModified_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnRecordsToBeModified, "View Employee(s) records that will be modified");
        }
        #endregion
    }
}