
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
    using TDSMAN.FormTrnCalcMonthlyTDS;
//--
using Excel = Microsoft.Office.Interop.Excel.Worksheet;
    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrnCalcMonthlyTDS
{
    #region STRUCTURE

    //#region T_Error_Type
    //public struct T_Error_Type
    //{
    //    public const string BLANK_NULL_CHECK = "Blank/NULL value";
    //    public const string MANADATORY_CHECK = "Mandatory value";
    //    public const string NUMERIC_CHECK = "Should be Numeric";
    //    public const string DUPLICATE_CHECK = "Duplicate Value";
    //    public const string LENGTH_CHECK = "Length Check";
    //    public const string FORMAT_CHECK = "Format Check";
    //    public const string MISMATCH_CHECK = "Data not matched";
    //    public const string SEQUENCE_CHECK = "Not in Sequence";
    //    public const string VALIDITY_CHECK = "Invalid value";
    //    //-- 2014/09/18
    //    public const string DUPLICATE_EXCEL_DATABASE = "Duplicate Value in Data";
    //    public const string VALUE_NOT_REQD = "Value not required";
    //    //
    //    public const string MISC = "MISC";
    //}
    //#endregion

    //#region T_Error_Type_Color
    //public struct T_Error_Type_Color
    //{
    //    public const string BLANK_NULL_CHECK = "Red";
    //    public const string MANADATORY_CHECK = "Salmon";
    //    public const string NUMERIC_CHECK = "SpringGreen";
    //    public const string DUPLICATE_CHECK = "SteelBlue";
    //    public const string LENGTH_CHECK = "Tan";
    //    public const string FORMAT_CHECK = "Tomato";
    //    public const string MISMATCH_CHECK = "Yellow";
    //    public const string SEQUENCE_CHECK = "Beige";
    //    public const string VALIDITY_CHECK = "Chocolate";
    //    public const string VALUE_NOT_REQD = "Blue";
    //    //
    //    public const string MISC = "Fuchsia";
    //}
    //#endregion

    //#region T_Sheet_Name
    //public struct T_Sheet_Name
    //{
    //    public const string CHALLAN_DETAILS = "Salary Details";
    //    public const string DEDUCTEE_DETAILS = "Deductee Details";
    //    public const string EMPLOYEE_DETAILS = "Employee Details";
    //    public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
    //    public const string SALARY_DETAILS = "Salary Details";
    //    public const string SALARY_DETAILS_F16 = "Form16-Details";
    //    public const string COMPANY_DETAILS = "Company Details";
    //    //-- Added By Abhishek Dey On 05/07/2018 --
    //    public const string PAN_ERROR = "PAN Error";
    //    public const string PAN_ERROR_ANNEX_II = "PAN Error SD";
    //    //-- Added By Abhishek Dey On 18/09/2018 --
    //    public const string DATA_FOR_BULK_DELETION_DD = "Data for Bulk Deletion DD";
    //    public const string DATA_FOR_BULK_DELETION_SD = "Data for Bulk Deletion SD";
    //    //
    //    public const string BULK_EMPLOYEE_GROUP_TAG = "Employee Data for Group Tagging";
    //}
    //#endregion

    //#region T_SaveTag
    //public struct T_SaveTag
    //{
    //    public const string IMPORT = "IMPORT";
    //    public const string VALIDATE = "VALIDATE";
    //}
    //#endregion

    #endregion   

    public partial class TrnCalcTDSImportExcel : TDSMAN.FormGen.GenForm
    {

        #region System Generated Code
        public TrnCalcTDSImportExcel()
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
        string strNewDeducteeWorkSheetName = "New Deductees Found";
        string strWorkingSheetName = "Monthly Data";
        long lngRowCount;
        long lngNewDeducteesCreated;
        //
        string strErrorMessage = "";

        string strTemporaryfileChallanPath = "";
        string strTemporaryfileDeducteePath = "";
        string strTemporaryfileSalaryPath = "";
        string strTemporaryfileForm16SalaryPath = "";

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
        long lngSalaryDetailsMonthlyDataProcessingHeaderId = 0;
        string strDefaultMonthBatchNoTextCombo = "New Month Batch";
        int intMONTH_ID = 0;
        #endregion

        #region set ENUM

        #region T_CHALLAN_DETAILS_COLUMN

        public enum T_CHALLAN_DETAILS_COLUMN
        {
            CHALLAN_DETAILS_ID = 0,
            SERIAL_NO_401 = 1,
            SERIAL_NO_401_CELL = 2,
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
            SERIAL_NO_401 = 0,
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
            TEMP_SERIAL_NO = 15
        }

        #endregion
        
        #region T_DEDUCTEE_DETAILS_COLUMN

        public enum T_DEDUCTEE_DETAILS_COLUMN
        {
            DEDUCTEE_DETAILS_ID = 0,
            DEDUCTEE_SERIAL_NO_414 = 1, 
            DEDUCTEE_SERIAL_NO_414_CELL = 2, 
            CHALLAN_SERIAL_NO_401 = 3, 
            CHALLAN_SERIAL_NO_401_CELL =4 , 
            DEDUCTEE_CODE_415 = 5, 
            DEDUCTEE_CODE_415_CELL = 6, 
            EMPLOYEE_PAN_416 = 7, 
            EMPLOYEE_PAN_416_CELL = 8, 
            EMPLOYEE_NAME_417 = 9, 
            EMPLOYEE_NAME_417_CELL = 10, 
            PAYMENT_DATE_418 = 11, 
            PAYMENT_DATE_418_CELL = 12, 
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
            EMPLOYEE_PAN_416 = 3,
            EMPLOYEE_NAME_417 = 4,
            PAYMENT_DATE_418 = 5,
            AMOUNT_PAID_419 = 6,
            TDS_421 = 7,
            SURCHARGE_422 = 8,
            EDUCATION_CESS_423 = 9,
            TOTAL_TAX_DEDUCTED_424 = 10,
            TOTAL_TAX_DEPOSITED_425 = 11,
            RATE_427 = 12,
            REASON_428 = 13,
            SERIAL_NO_ORDER = 14,
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
            PERIOD_FROM_DATE = 4,
            PERIOD_TO_DATE = 5,

            TS_GS_SEC_17_1 = 6,
            TS_GS_SEC_17_2 = 7,
            TS_GS_SEC_17_3 = 8,

            TOTAL_SALARY = 9,

            SEC10_5_AMOUNT = 10,
            SEC10_10_AMOUNT = 11,
            SEC10_10A_AMOUNT = 12,
            SEC10_10AA_AMOUNT = 13,
            SEC10_13A_AMOUNT = 14,
            SEC10_OTHERS_AMOUNT = 15,
            BALANCE_AMOUNT = 16,
            //
            GROSS_DEDUCTION_16ii = 17,
            GROSS_DEDUCTION_16iii = 18,
            GROSS_DEDUCTION_16ia = 19,
            GROSS_TOTAL_DEDUCTION_16iii = 20,
            INCOME_CHARGEABLE_SALARIES = 21,
            //
            INCOME_OR_LOSS_HOUSE_PROPERTY = 22,
            INCOME_OTHER_SOURCES = 23,
            //INCOME_OTHER_SALARY = 23,
            //
            GROSS_TOTAL_INCOME = 24,
            //
            SEC_80C = 25,
            SEC_80CCC = 26,
            SEC_80CCD_1 = 27,
            SEC_80C_CCC_CCD_1 = 28,
            SEC_80CCD_1B = 29,
            SEC_80CCD_2 = 30,
            SEC_80D = 31,
            SEC_80E = 32,
            SEC_80G = 33,
            SEC_80TTA = 34,
            ////
            //DED_CHVIA_80CCE = 35,
            //DED_CHVIA_80CCF = 36,
            DED_CHVIA_OTHER_SECTIONS = 35,
            GROSS_TOTAL_DED_CHVIA = 36,
            TOTAL_TAXABLE_INCOME = 37,
            INCOME_TAX_ON_TOTAL_INCOME = 38,
            //
            REBATE_US_87A_AMOUNT = 39,
            //
            SURCHARGE = 40,
            EDUCATION_CESS = 41,
            INCOME_TAX_RELIEF = 42,
            NET_TAX_PAYABLE = 43,
            TOTAL_TDS_DEDUCTED = 44,
            SHORTFALL_EXCESS = 45,
            TAXABLE_AMOUNT = 46,
            REPORTED_TAXABLE_AMOUNT = 47,
            TOTAL_TAX_DEDUCTED_AMOUNT = 48,
            PREVIOUS_TAX_DEDUCTED_TOTAL = 49,
            TAX_DEDUCTED_HIGHER_RATE = 50,
            //
            TDS_SUPERANN = 51,
            CONTRIBUTIONS_SUPERANN_YN = 52,
            NAME_SUPERANN = 53,
            SUPERANN_FROM_DATE = 54,
            SUPERANN_TO_DATE = 55,
            AMT_REPAID = 56,
            RATE_DED = 57,
            AMT_TAX = 58,
            GROSS_TOTAL_INC = 59,
            //
            RENT_PAYMENT_YN = 60,
            PAN_LANDLORD1 = 61,
            NAME_LANDLORD1 = 62,
            PAN_LANDLORD2 = 63,
            NAME_LANDLORD2 = 64,
            PAN_LANDLORD3 = 65,
            NAME_LANDLORD3 = 66,
            PAN_LANDLORD4 = 67,
            NAME_LANDLORD4 = 68,
            INT_PAID_YN = 69,
            PAN_LENDER1 = 70,
            NAME_LENDER1 = 71,
            PAN_LENDER2 = 72,
            NAME_LENDER2 = 73,
            PAN_LENDER3 = 74,
            NAME_LENDER3 = 75,
            PAN_LENDER4 = 76,
            NAME_LENDER4 = 77
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
            lngSalaryDetailsMonthlyDataProcessingHeaderId = 0;
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
            string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
            //-- FORM NO
            //string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails };
            string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q};
            dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            cmbFormNo.Enabled = false;
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
                LoadChallanGrid();
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
            string strValidationAskMessage = "Proceed Validation of Excel for Monthly Data ?", strValidationImportedDatTimeMessage = "";
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
                    //if (cmnService.J_UserMessage("Proceed Validation of Excel for Monthly Data ?", MessageBoxButtons.YesNo) == DialogResult.No)
                    //    return;
                    if(cmbMonthSerialNo.Visible==true)
                    {
                        if(cmbMonthSerialNo.Text == "") //-- 1st UPDATE BATCH
                        {
                            strSQL= @" SELECT " + cmnService.J_SQLDBFormat("DATA_IMPORTED_DATE_TIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) +
                                    @" FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                       WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                                       AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                       AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                                       AND    MONTH_BATCH_NO = 0";
                            strValidationImportedDatTimeMessage = Convert.ToString( dmlService.J_ExecSqlReturnScalar(strSQL));
                            //
                            strValidationAskMessage = "You have imported data to the same Batch on : " + strValidationImportedDatTimeMessage + ".\nIt will repalce the existing batch.\n" + strValidationAskMessage;
                            //
                        }
                        else if(cmbMonthSerialNo.Text == strDefaultMonthBatchNoTextCombo) //-- CREATE NEW BATCH
                        {
                            //strSQL = @"SELECT " + cmnService.J_SQLDBFormat("DATA_IMPORTED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY) +
                            //        @" FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                            //           WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                            //           AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                            //           AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                            //           AND    MONTH_BATCH_NO = 0";// + cmnService.J_ReturnInt64Value(cmbMonthSerialNo.Text);
                            //strValidationImportedDatTimeMessage = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                            //
                            strValidationAskMessage = "You have previously imported data to the same Batch.\nIt will be imported as a new batch.\n" + strValidationAskMessage;
                            //
                        }
                        else //-- UPDATE ANY BATCH
                        {
                            strSQL = @"SELECT " + cmnService.J_SQLDBFormat("DATA_IMPORTED_DATE_TIME", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) +
                                    @" FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                       WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                                       AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                       AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                                       AND    MONTH_BATCH_NO = " + cmnService.J_ReturnInt64Value(cmbMonthSerialNo.Text);
                            strValidationImportedDatTimeMessage = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                            //
                            strValidationAskMessage = "You have imported data to the same Batch on : " + strValidationImportedDatTimeMessage + ".\nIt will repalce the existing batch.\n" + strValidationAskMessage;
                            //
                        }

                    }
                    //else //-- 1st TIME IMPORT
                    //{
                        if (cmnService.J_UserMessage(strValidationAskMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    //}
                    
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
                    if (CheckExcelStructure(cmbFormNo.Text, txtExcelPath.Text) == false)
                    {
                        this.Cursor = Cursors.Default;
                        //--
                        //if (chkCalcTDS.Checked == true)
                        //{
                            cmnService.J_UserMessage("Selected Excel file is invalid.\n" +
                                                 "Please get the latest Excel file format from the button just right of Financial Year.", MessageBoxIcon.Exclamation);
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
                    if (VALIDATE_DATA_TDS_CALC() == false)
                    {
                        //--
                        TdsMan.SHRINK_DATABASE();
                        //--
                        cmnService.J_UserMessage("Validation for TDS Calculation failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        return;
                    }
                    //
                    //if (VALIDATE_DATA() == false)
                    //{
                    //    //--
                    //    TdsMan.SHRINK_DATABASE();
                    //    //--
                    //    cmnService.J_UserMessage("Data Validation failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    prgBar.Value = 0;
                    //    return;
                    //}
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
                        //cmnService.J_UserMessage("Excel Data Validated Sucessfully...", MessageBoxIcon.Information);
                        //this.Cursor = Cursors.Default;
                        //prgBar.Value = 0;
                        //return;
                    }
                    #endregion
                    //
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
                    //lblProgressDisplayMessage.Visible = true;
                    //lblProgressDisplayMessage.Text = "Error sheet writing started";
                    //
                    if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails
                        || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                    {
                        if (WRITE_ERROR_WORKSHEET_SD(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
                    else
                    {
                        if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                        {
                            cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                    }
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
                    //lblProgressDisplayMessage.Visible = false;
                    //
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + ""))) == 0)
                    {
                        if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false) this.Cursor = Cursors.Default;
                        //
                        if (NEW_DEDUCTEE_MASTER_CREATED() == false)
                        {
                            cmnService.J_UserMessage("Deductee Master list creation failed", MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            prgBar.Value = 0;
                            return;
                        }
                        if (lngNewDeducteesCreated > 0)
                        {
                            if (lngNewDeducteesCreated > TDSMAN.Classes.TDSMAN.T_MAX_REC_EXCEL_IMPORT_ALERT) //-- 2019/08/01
                            {
                                cmnService.J_UserMessage("Total No. of new deductees being added : " + lngNewDeducteesCreated + ".\n\n"+
                                                         "Not being able to display these new deductees in Excel.\nShowing this list " +
                                                         "in excel slows down the Import process.\nYou have opted not to display.\n\n" +
                                                         "You can modify from [Utilities > Preferences (Sl No. 27)]");                                
                                //
                            }
                            else
                            {
                                //--
                                if (CREATE_NEW_DEDUCTEE_WORKSHEET(txtExcelPath.Text) == false)
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
                    //if (cmbFormNo.Text == T_FormNo.F24Q || cmbFormNo.Text == T_FormNo.F26Q || cmbFormNo.Text == T_FormNo.F27Q || cmbFormNo.Text == T_FormNo.F27EQ)
                    //{
                    //    //strSQL = "SELECT CHALLAN_DETAILS_ID " +
                    //    //         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    //    //         "WHERE  (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) - " +
                    //    //         "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX) < 0 ";
                    //    if (chkEnterDeducteeDetailsOnly.Checked == false) //-- 2016/08/19
                    //    {
                    //        //-- ANIK 2014/01/21
                    //        strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " WHERE ERR_DESC <> ''";
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
                        //if (cmbFormNo.Text == T_FormNo.F24Q)
                        //{
                        //    if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails ||
                        //        cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                        //        strMessage = "All existing Salary details data will be deleted";
                        //    else
                        //        strMessage = "All existing Challan and Employee details data will be deleted";
                        //}
                        //else
                        //    strMessage = "All existing Challan and Deductee details data will be deleted";
                        //strMessage = "All existing Data will be deleted";
                    }
                    //--
                    #region F24Q-F26Q-F27Q-F27EQ
                        //--
                        //if (chkEnterDeducteeDetailsOnly.Checked == false)
                        //{
                        //    strSQL = "SELECT CHALLAN_DETAILS_ID " +
                        //             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        //             "WHERE  (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " +
                        //             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX) < 0 ";
                        //    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                        //    {
                        //        cmnService.J_UserMessage("Import not possible", MessageBoxIcon.Error);
                        //        return;
                        //    }
                        //}
                        //--
                        //if (cmbFormNo.Text == T_FormNo.F24Q)
                        //{
                            if (cmnService.J_UserMessage("Proceed Import - " + "\n Based on \n " +
                                "FINANCIAL YEAR \t: " + cmbFinancialYear.Text + " \n " +
                                "COMPANY    \t: " + cmbCompany.Text + " \n " +
                                "MONTH      \t: " + cmbMonth.Text + " \n " +
                                "Proceed??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                        //    cmbFormNo.Text);
                        ////--
                        //if (lngBasicInfoID == 0)
                        //{
                        //    if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                        //                                    cmbQuarter.Text,
                        //                                    Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                        //                                    cmbFormNo.Text) == true)
                        //    {
                        //        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                        //                            cmbQuarter.Text,
                        //                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                        //                            cmbFormNo.Text);
                        //        InsertCompanyBasicInfo(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)), lngBasicInfoID);
                        //    }
                        //}
                        //--
                        TdsMan.SHRINK_DATABASE();                        
                        //--###########################
                        //if (TdsMan.LimitEditions(lngBasicInfoID, rbnIncremental.Checked, false,false, "TRN_DEDUCTEE_DETAILS", "BASIC_INFO_ID", TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC, "") == false)
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
                        ////
                        if (rbnNewImport.Checked == true)
                        {
                            //if (TdsMan.T_DeleteDeducteeDetails(lngBasicInfoID) == false)
                            //{
                            //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                            //    this.Cursor = Cursors.Default;
                            //    return;
                            //}
                            ////
                            //prgImportBar.Value = prgImportBar.Value + 5;
                            //this.Refresh();
                            ////
                            //if (TdsMan.T_DeleteChallanDetails(lngBasicInfoID) == false)
                            //{
                            //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                            //    this.Cursor = Cursors.Default;
                            //    return;
                            //}
                            ////
                            //prgImportBar.Value = prgImportBar.Value + 5;
                            //this.Refresh();
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

                    prgImportBar.Value = prgImportBar.Value + 5;
                    this.Refresh();
                    ////--                
                    //if (INSERT_MASTER_DATA() == false)
                    //{
                    //    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                    this.Refresh();
                    //--
                    long lngMonthBatchNo = 0;
                    if(cmbMonthSerialNo.Text == "" ) //strDefaultMonthBatchNoTextCombo)
                    {
                        lngMonthBatchNo = 0;
                    }
                    else if (cmbMonthSerialNo.Text == strDefaultMonthBatchNoTextCombo) //)
                    {
                        strSQL = @"SELECT MAX(MONTH_BATCH_NO) 
                                   FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                   WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                                   AND    ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                   AND    MONTH_ID   = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                        lngMonthBatchNo = cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL)) + 1;
                    }
                    else
                    {
                        lngMonthBatchNo = Convert.ToInt32(cmbMonthSerialNo.Text);// Convert.ToInt32(Support.GetItemData(cmbMonthSerialNo, cmbMonthSerialNo.SelectedIndex));
                    }
                    //--
                    if (INSERT_MONTHLY_SALARY_DATA(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), 
                                                   Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                   Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)),
                                                   lngMonthBatchNo) == false)
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
                    //if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                    //{
                    //    for (int i = prgSDBar.Minimum; i <= prgSDBar.Maximum; i++)
                    //    {
                    //        prgSDBar.PerformStep();
                    //    }
                    //}
                    //else
                    //{
                        for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                        {
                            prgImportBar.PerformStep();
                        }
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
                    //    //strQuery = @"FORM_NAME= '" + cmbFormNo.Text + "' ";
                    //    //-- 2014/11/14
                    //    strQuery = @" FORM_NAME= '" + cmbFormNo.Text + "' AND SETUPID = " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID;
                    //    //
                    //    string strFormNo = "";
                    //    if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails
                    //        || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                    //        strFormNo = cmnService.J_Left(cmbFormNo.Text, 4).Trim();
                    //    else
                    //        strFormNo = cmbFormNo.Text;
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
                    //if (lblFormNo.Text == "")
                    //    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = lblSDMonthBatch.Text;
                    //else
                    //    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = lblFormNo.Text;
                    //
                    string strFaYear = cmbFinancialYear.Text;
                    string strCompany = cmbCompany.Text;
                    string strQtr = cmbQuarter.Text;
                    string strMonth = cmbMonth.Text;
                    string strMonthSerialNo = lngMonthBatchNo.ToString();// cmbMonthSerialNo.Text;
                    //
                    this.Close();
                    this.Dispose();
                    //--
                    //Do you want to check the data imported?
                    if (cmnService.J_UserMessage("Do you want to view the data imported??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //if (chkEnterDeducteeDetailsOnly.Checked == true)
                        //{
                        //    TdsMan.CloseChildForm(new TrnRegularReturn(T_DEDUCTEE_FIRST_MODULES.DEDUCTEE), J_Var.frmMain);
                        //    //--
                        //    cmnService.J_ShowChildForm(new TrnRegularReturn(T_DEDUCTEE_FIRST_MODULES.DEDUCTEE), J_Var.frmMain, "Form  "+ lblFormNo.Text + " - Deductee entry without Challan");
                        //}
                        //else
                        //{
                        //    //
                        //    TdsMan.CloseChildForm(new TrnRegularReturn(0), J_Var.frmMain);
                        //    //
                        //    cmnService.J_ShowChildForm(new TrnRegularReturn(0), J_Var.frmMain, "Form " + lblFormNo.Text);
                        //}
                        TrnProcessMonthlyData objTrnProcessMonthlyData = new TrnProcessMonthlyData(strFaYear, strCompany, strQtr, strMonth, strMonthSerialNo);
                        objTrnProcessMonthlyData.MdiParent = TrnRegularReturn.ActiveForm;
                        objTrnProcessMonthlyData.Show();
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
            cmbMonthBatchNo_SelectedIndexChanged(sender, e);
            //
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
            if(dgcViewChallan.Visible == false) return;
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
            if (cmnService.J_ReturnInt64Value(lblSDNewDeductees.Text) == 0)
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
            if (cmbQuarter.Text == T_Qtr.Q1)
            {
                strSQL = " SELECT MONTH_ORDER," +
                        "         MONTH_DESC " +
                        "  FROM   MST_MONTH " +
                        "  WHERE MONTH_ORDER BETWEEN 1 AND 3 " +
                        "  ORDER BY MONTH_ORDER";
            }
            else if (cmbQuarter.Text == T_Qtr.Q2)
            {
                strSQL = " SELECT MONTH_ORDER," +
                        "         MONTH_DESC " +
                        "  FROM   MST_MONTH " +
                        "  WHERE MONTH_ORDER BETWEEN 4 AND 6 " +
                        "  ORDER BY MONTH_ORDER";
            }
            else if (cmbQuarter.Text == T_Qtr.Q3)
            {
                strSQL = " SELECT MONTH_ORDER," +
                        "         MONTH_DESC " +
                        "  FROM   MST_MONTH " +
                        "  WHERE MONTH_ORDER BETWEEN 7 AND 9 " +
                        "  ORDER BY MONTH_ORDER";
            }
            else if (cmbQuarter.Text == T_Qtr.Q4)
            {
                strSQL = " SELECT MONTH_ORDER," +
                        "         MONTH_DESC " +
                        "  FROM   MST_MONTH " +
                        "  WHERE MONTH_ORDER BETWEEN 10 AND 12 " +
                        "  ORDER BY MONTH_ORDER";
            }
            else
            {
                strSQL = " SELECT MONTH_ORDER," +
                        "         MONTH_DESC " +
                        "  FROM   MST_MONTH " +
                        "  ORDER BY MONTH_ORDER";                
            }
            //
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbMonth, 0, J_ComboBoxSelectedIndex.YES) == false) return;
            //--
            cmbMonthBatchNo_SelectedIndexChanged(sender, e);

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

        #region btnBulkPANValidation_Click
        private void btnBulkPANValidation_Click(object sender, EventArgs e)
        {
            //--
            if (cmnService.J_ReturnInt32Value(lblNewDeducteesFound.Text) > 0)
            {
                //-----------------------------------------------
                if (lngBasicInfoID == 0)
                {
                    string strForm = "";
                    if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                        strForm = T_FormNo.F24Q;
                    else
                        strForm = cmbFormNo.Text;
                    //
                    lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                        cmbQuarter.Text,
                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                        strForm);
                    //    
                }
                //--
                TDSMAN.Classes.TDSMAN.T_pBasicInfoId = lngBasicInfoID;
                TDSMAN.Classes.TDSMAN.T_pQuarter = lblQtr.Text;
                TDSMAN.Classes.TDSMAN.T_pFormNo = lblFormNo.Text;
                TDSMAN.Classes.TDSMAN.T_pTAN = lblTANNo.Text;
                TDSMAN.Classes.TDSMAN.T_FromModule = T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR;
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                TrnBulkPANNameValidation TrnBulkPANNameValidation = new TrnBulkPANNameValidation();
                TrnBulkPANNameValidation.ShowDialog();
                //--
                this.Cursor = Cursors.Default;

            }
            else
                cmnService.J_UserMessage("No records available");
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
        private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pctVideoDemo.Visible = false;
            ////
            //if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails || cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
            //{
            //    chkEnterDeducteeDetailsOnly.Checked = false;
            //    chkEnterDeducteeDetailsOnly.Enabled = false;
            //    //
            //    lblNoteAdd.Visible = false;
            //    //
            //    if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
            //        pctVideoDemo.Visible = true;
            //    else
            //        pctVideoDemo.Visible = false;
            //    //--                
            //}
            //else
            //{
            //    pctVideoDemo.Visible = true;
            //    //
            //    lblNoteAdd.Visible = true;
            //    //
            //    if (chkEnterDeducteeDetailsOnly.Checked != true)
            //    {
            //        chkEnterDeducteeDetailsOnly.Checked = false;
            //        chkEnterDeducteeDetailsOnly.Enabled = true;
            //    }
            //    //--
            //    //grpCalcTDS.Visible = true;
            //}
            ////--
            ////if (cmbFormNo.Text == T_FormNo.F24Q)
            ////{
            ////    grpCalcTDS.Visible = true;
            ////}
            ////else
            ////{
            ////    grpCalcTDS.Visible = false;
            ////}
        }
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

        #region btnExportData_MouseMove
        private void btnExportData_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip1.SetToolTip(btnDownloadExcelFormatCalcTDS, "Click to create Blank Excel Format");
        }
        #endregion

        #region btnExportData_Click
        private void btnExportData_Click(object sender, EventArgs e)
        {
            if (cmnService.J_UserMessage("Get Excel file structure ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //--
            string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder...");
            
            //-- FILE NAME FOR EXCEL
            string strExcelFileName = "";
            //if (cmbMonth.SelectedIndex <= 0 )
                strExcelFileName = "24Q-SD_MONTHLY_DATA-V1.XLSX";
            //else
            //    strExcelFileName = "24Q_TDS_CALC_" + cmbMonth.Text + ".XLSX";
            //--
            //--DESTINATION FILE
            string strDestFile = Path.Combine(strExcelFolder, strExcelFileName);
            //
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("It needs Internet Connectivity (currently absent) to download the excel format.");
                return;
            }
            //
            this.Cursor = Cursors.WaitCursor;
            using (WebClient wc = new WebClient())
                wc.DownloadFile("http://www.tdsman.com/Downloads/24Q-SD_MONTHLY_DATA-V1.XLSX", strDestFile);
            //wc.DownloadFile("http://www.jayasoftwares.co.in/anik/24Q-SD_MONTHLY_DATA-V1.XLSX", strDestFile);
            //--
            System.Threading.Thread.Sleep(100);
            //--
            this.Cursor = Cursors.Default;
            if (cmnService.J_IsFileExist(strDestFile) == true)
            {
                cmnService.J_UserMessage("Blank Excel File Created");
                //--
                System.Diagnostics.Process.Start(strDestFile);
            }
            else
                cmnService.J_UserMessage("Blank Excel File Creation Failed");
            //--
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
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {                    
                    return true;
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
                    if (cmbQuarter.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Quarter - Cannot be Blank");
                        cmbQuarter.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- FORM NO
                    //-----------------------------------------------------------------------
                    if (cmbFormNo.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Form No. - Cannot be Blank");
                        cmbFormNo.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- MONTH NO
                    //-----------------------------------------------------------------------
                    if (cmbMonth.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Month - Cannot be Blank");
                        cmbMonth.Select();
                        return false;
                    }
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
                    if (txtExcelPath.Text.Trim()== "")
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
                    if(cmbMonth.SelectedIndex<=0)
                    {
                        cmnService.J_UserMessage("Month - Cannot be Blank as you have opted for TDS Calculation");
                        cmbMonth.Select();
                        return false;
                    }
                    //}
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

        //-- COMMENTED 2016/02/02
        #region CREATE_TEMP_TABLES 
        //private bool CREATE_TEMP_TABLES()
        //{
        //    try
        //    {
        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }

        //        #region COMMENT
        //        //Blocked by INDRAJIT on 16-03-2012
        //        //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == false)
        //        //{
        //        //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " (" +
        //        //         "                  CHALLAN_DETAILS_ID       COUNTER," +
        //        //         "                  SERIAL_NO_ORDER  NUMBER    DEFAULT 0," +
        //        //         "                  SERIAL_NO        TEXT(255) DEFAULT \"\"," +
        //        //         "                  SERIAL_NO_CELL   TEXT(10)  DEFAULT \"\"," +
        //        //         "                  SECTION_NAME             TEXT(255) DEFAULT \"\"," +
        //        //         "                  SECTION_NAME_CELL        TEXT(10)  DEFAULT \"\"," +
        //        //         "                  TDS                      TEXT(255) DEFAULT \"\"," +
        //        //         "                  TDS_CELL                 TEXT(10)  DEFAULT \"\"," +
        //        //         "                  SURCHARGE                TEXT(255) DEFAULT \"\"," +
        //        //         "                  SURCHARGE_CELL           TEXT(10)  DEFAULT \"\"," +
        //        //         "                  EDUCATION_CESS           TEXT(255) DEFAULT \"\"," +
        //        //         "                  EDUCATION_CESS_CELL      TEXT(10)  DEFAULT \"\"," +
        //        //         "                  INTEREST                 TEXT(255) DEFAULT \"\"," +
        //        //         "                  INTEREST_CELL            TEXT(10)  DEFAULT \"\"," +
        //        //         "                  OTHERS                   TEXT(255) DEFAULT \"\"," +
        //        //         "                  OTHERS_CELL              TEXT(10)  DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEPOSITED      TEXT(255) DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEPOSITED_CELL TEXT(10)  DEFAULT \"\"," +
        //        //         "                  CTRL_TOT_TAX             CURRENCY  DEFAULT 0," +
        //        //         "                  NO_OF_DEDUCTEES          INTEGER   DEFAULT 0," +
        //        //         "                  CHEQUE_NO                TEXT(255) DEFAULT \"\"," +
        //        //         "                  CHEQUE_NO_CELL           TEXT(10)  DEFAULT \"\"," +
        //        //         "                  BSR_CODE                 TEXT(255) DEFAULT \"\"," +
        //        //         "                  BSR_CODE_CELL            TEXT(10)  DEFAULT \"\"," +
        //        //         "                  DATE_TAX_DEPOSITED       TEXT(255) DEFAULT \"\"," +
        //        //         "                  DATE_TAX_DEPOSITED_CELL  TEXT(10)  DEFAULT \"\"," +
        //        //         "                  TRF_VCH_CHLN_NO          TEXT(255) DEFAULT \"\"," +
        //        //         "                  TRF_VCH_CHLN_NO_CELL     TEXT(10)  DEFAULT \"\"," +
        //        //         "                  BOOK_ENTRY               TEXT(255) DEFAULT \"\"," +
        //        //         "                  BOOK_ENTRY_CELL          TEXT(10)  DEFAULT \"\")";
        //        //    dmlService.J_ExecSql(strSQL);
        //        //}
        //        ////
        //        //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
        //        //dmlService.J_ExecSql(strSQL);
        //        //--

        //        //TEXT(50) DEFAULT \"\"," +
        //        //"Code     MEMO," +
        //        //"Active   Bit      DEFAULT 0," +
        //        //"Hits     int      DEFAULT 0," +
        //        //"Rotation long     DEFAULT 0)";
        //        #endregion

        //        if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC) == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC;
        //            dmlService.J_ExecSql(strSQL);
        //        }

        //        #region COMMENT
        //        //Blocked by INDRAJIT on 16-03-2012
        //        //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == false)
        //        //{
        //        //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " (" +
        //        //         "                  DEDUCTEE_DETAILS_ID      COUNTER," +
        //        //         "                  DEDUCTEE_SERIAL_NO_ORDER NUMBER    DEFAULT 0," +
        //        //         "                  DEDUCTEE_SERIAL_NO       TEXT(255) DEFAULT \"\"," +
        //        //         "                  DEDUCTEE_SERIAL_NO_CELL  TEXT(10) DEFAULT \"\"," +
        //        //         "                  CHALLAN_SERIAL_NO        TEXT(255) DEFAULT \"\"," +
        //        //         "                  CHALLAN_SERIAL_NO_CELL   TEXT(10) DEFAULT \"\"," +
        //        //         "                  DEDUCTEE_CODE            TEXT(255) DEFAULT \"\"," +
        //        //         "                  DEDUCTEE_CODE_CELL       TEXT(10) DEFAULT \"\"," +
        //        //         "                  EMPLOYEE_PAN             TEXT(255) DEFAULT \"\"," +
        //        //         "                  EMPLOYEE_PAN_CELL        TEXT(10) DEFAULT \"\"," +
        //        //         "                  EMPLOYEE_NAME            TEXT(255) DEFAULT \"\"," +
        //        //         "                  EMPLOYEE_NAME_CELL       TEXT(10) DEFAULT \"\"," +
        //        //         "                  PAYMENT_DATE             TEXT(255) DEFAULT \"\"," +
        //        //         "                  PAYMENT_DATE_CELL        TEXT(10) DEFAULT \"\"," +
        //        //         "                  AMOUNT_PAID              TEXT(255) DEFAULT \"\"," +
        //        //         "                  AMOUNT_PAID_CELL         TEXT(10) DEFAULT \"\"," +
        //        //         "                  TDS                      TEXT(255) DEFAULT \"\"," +
        //        //         "                  TDS_CELL                 TEXT(10) DEFAULT \"\"," +
        //        //         "                  SURCHARGE                TEXT(255) DEFAULT \"\"," +
        //        //         "                  SURCHARGE_CELL           TEXT(10) DEFAULT \"\"," +
        //        //         "                  EDUCATION_CESS           TEXT(255) DEFAULT \"\"," +
        //        //         "                  EDUCATION_CESS_CELL      TEXT(10) DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEDUCTED       TEXT(255) DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEDUCTED_CELL  TEXT(10) DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEPOSITED      TEXT(255) DEFAULT \"\"," +
        //        //         "                  TOTAL_TAX_DEPOSITED_CELL TEXT(10) DEFAULT \"\"," +
        //        //         "                  RATE                     TEXT(255) DEFAULT \"\"," +
        //        //         "                  RATE_CELL                TEXT(10) DEFAULT \"\"," +
        //        //         "                  REASON                   TEXT(255) DEFAULT \"\"," +
        //        //         "                  REASON_CELL              TEXT(10) DEFAULT \"\"," +
        //        //         "                  GROSSING_UP_TOT_VALUE_PUR TEXT(255) DEFAULT \"\"," +
        //        //         "                  GROSSING_UP_TOT_VALUE_PUR_CELL TEXT(10) DEFAULT \"\"," +
        //        //         "                  DEDUCTEE_MASTER_ID       NUMBER  DEFAULT 0)";
        //        //    dmlService.J_ExecSql(strSQL);
        //        //}
        //        //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "", "DEDUCTEE_MASTER_ID") == false)
        //        //{
        //        //    strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD COLUMN DEDUCTEE_MASTER_ID NUMBER  DEFAULT 0";
        //        //    dmlService.J_ExecSql(strSQL);
        //        //}
        //        ////
        //        //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
        //        //dmlService.J_ExecSql(strSQL);
        //        #endregion

        //        //Added by INDRAJIT on 16-03-2012
        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_SUMM + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_SUMM + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }
        //        //--
        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }

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

        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }

        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == false)
        //        {
        //            strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
        //                 "                  DEDUCTEE_MASTER_ID COUNTER," +
        //                 "                  EMPLOYEE_NAME      TEXT(75) DEFAULT \"\"," +
        //                 "                  EMPLOYEE_PAN       TEXT(10) DEFAULT \"\"," +
        //                 "                  DEDUCTEE_CODE      TEXT(5) DEFAULT \"\"," +
        //                 "                  COMPANY_ID         NUMBER  DEFAULT 0)";
        //            dmlService.J_ExecSql(strSQL);
        //        }

        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "", "COMPANY_ID") == false)
        //        {
        //            strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " ADD COLUMN COMPANY_ID NUMBER DEFAULT 0";
        //            dmlService.J_ExecSql(strSQL);
        //        }
        //        //
        //        strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
        //        dmlService.J_ExecSql(strSQL);

        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "") == true)
        //        {
        //            strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "";
        //            dmlService.J_ExecSql(strSQL);
        //        }

        //        if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "") == false)
        //        {
        //            strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + " (" +
        //                 "                  CHALLAN_CONTROL_TOTALS_ID COUNTER," +
        //                 "                  CHALLAN_ID                NUMBER  DEFAULT 0," +
        //                 "                  SUM_TAX_AMOUNT            MONEY   DEFAULT 0," +
        //                 "                  SUM_SURCHARGE_AMOUNT      MONEY   DEFAULT 0," +
        //                 "                  SUM_CESS_AMOUNT           MONEY   DEFAULT 0," +
        //                 "                  SUM_TOTAL_AMOUNT          MONEY   DEFAULT 0," +
        //                 "                  SUM_TAX_DEPOSITED_AMOUNT  MONEY   DEFAULT 0)";
        //            dmlService.J_ExecSql(strSQL);
        //        }
        //        //
        //        strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "";
        //        dmlService.J_ExecSql(strSQL);
        //        //
        //        if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
        //        {
        //            if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "") == true)
        //            {
        //                strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "";
        //                dmlService.J_ExecSql(strSQL);
        //            }

        //            //Blocked by INDRAJIT on 16-03-2012
        //            //if (dmlService.J_IsDatabaseObjectExist("TEMP_SALARY_DETAILS") == false)
        //            //{
        //            //    strSQL = "CREATE TABLE TEMP_SALARY_DETAILS (" +
        //            //         "                  SALARY_DETAILS_ID                   COUNTER," +
        //            //         "                  SERIAL_NO_ORDER             NUMBER    DEFAULT 0," +
        //            //         "                  EMPLOYEE_PAN                        TEXT(255) DEFAULT \"\"," +
        //            //         "                  EMPLOYEE_PAN_CELL                   TEXT(10)  DEFAULT \"\"," +
        //            //         "                  EMPLOYEE_ID                         NUMBER    DEFAULT 0," +
        //            //         "                  EMPLOYEE_NAME                       TEXT(255) DEFAULT \"\"," +
        //            //         "                  EMPLOYEE_NAME_CELL                  TEXT(10)  DEFAULT \"\"," +
        //            //         "                  CATEGORY                            TEXT(255) DEFAULT \"\"," +
        //            //         "                  CATEGORY_CELL                       TEXT(10)  DEFAULT \"\"," +
        //            //         "                  FROM_DATE                           TEXT(255) DEFAULT \"\"," +
        //            //         "                  FROM_DATE_CELL                      TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TO_DATE                             TEXT(255) DEFAULT \"\"," +
        //            //         "                  TO_DATE_CELL                        TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TS_BALANCE                          TEXT(255) DEFAULT \"\"," +
        //            //         "                  TS_BALANCE_CELL                     TEXT(10)  DEFAULT \"\"," +
        //            //         "                  US_16_EA                            TEXT(255) DEFAULT \"\"," +
        //            //         "                  US_16_EA_CELL                       TEXT(10)  DEFAULT \"\"," +
        //            //         "                  US_16_TE                            TEXT(255) DEFAULT \"\"," +
        //            //         "                  US_16_TE_CELL                       TEXT(10)  DEFAULT \"\"," +
        //            //         "                  US_16_AGGREGATE                     TEXT(255) DEFAULT \"\"," +
        //            //         "                  US_16_AGGREGATE_CELL                TEXT(10)  DEFAULT \"\"," +
        //            //         "                  INCOME_CHARGEABLE                   TEXT(255) DEFAULT \"\"," +
        //            //         "                  INCOME_CHARGEABLE_CELL              TEXT(10)  DEFAULT \"\"," +
        //            //         "                  AIS_Total                           TEXT(255) DEFAULT \"\"," +
        //            //         "                  AIS_Total_CELL                      TEXT(10)  DEFAULT \"\"," +
        //            //         "                  GROSS_TOTAL_INCOME                  TEXT(255) DEFAULT \"\"," +
        //            //         "                  GROSS_TOTAL_INCOME_CELL             TEXT(10)  DEFAULT \"\"," +
        //            //         "                  CVIA_SEC80CCE_TOTAL_DED_AMOUNT      TEXT(255) DEFAULT \"\"," +
        //            //         "                  CVIA_SEC80CCE_TOTAL_DED_AMOUNT_CELL TEXT(10)  DEFAULT \"\"," +
        //            //         "                  CVIA_SEC80CCF_DED_AMOUNT            TEXT(255) DEFAULT \"\"," +
        //            //         "                  CVIA_SEC80CCF_DED_AMOUNT_CELL       TEXT(10)  DEFAULT \"\"," +
        //            //         "                  CVIA_OTH_DED_TOTAL                  TEXT(255) DEFAULT \"\"," +
        //            //         "                  CVIA_OTH_DED_TOTAL_CELL             TEXT(10)  DEFAULT \"\"," +
        //            //         "                  CVIA_DED_TOTAL                      TEXT(255) DEFAULT \"\"," +
        //            //         "                  CVIA_DED_TOTAL_CELL                 TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TOTAL_INCOME                        TEXT(255) DEFAULT \"\"," +
        //            //         "                  TOTAL_INCOME_CELL                   TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TAX_TOTAL_INCOME                    TEXT(255) DEFAULT \"\"," +
        //            //         "                  TAX_TOTAL_INCOME_CELL               TEXT(10)  DEFAULT \"\"," +
        //            //         "                  SCHG_TOTAL_INCOME                   TEXT(255) DEFAULT \"\"," +
        //            //         "                  SCHG_TOTAL_INCOME_CELL              TEXT(10)  DEFAULT \"\"," +
        //            //         "                  ECESS_TOTAL_INCOME                  TEXT(255) DEFAULT \"\"," +
        //            //         "                  ECESS_TOTAL_INCOME_CELL             TEXT(10)  DEFAULT \"\"," +
        //            //         "                  US_89_LESS                          TEXT(255) DEFAULT \"\"," +
        //            //         "                  US_89_LESS_CELL                     TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TAX_PAYABLE                         TEXT(255) DEFAULT \"\"," +
        //            //         "                  TAX_PAYABLE_CELL                    TEXT(10)  DEFAULT \"\"," +
        //            //         "                  TOTAL_TDS_DEDUCTED                  TEXT(255) DEFAULT \"\"," +
        //            //         "                  TOTAL_TDS_DEDUCTED_CELL             TEXT(10)  DEFAULT \"\"," +
        //            //         "                  SHORTFALL_TAX                       TEXT(255) DEFAULT \"\"," +
        //            //         "                  SHORTFALL_TAX_CELL                  TEXT(10)  DEFAULT \"\")";
        //            //    dmlService.J_ExecSql(strSQL);
        //            //}
        //        }
        //        //
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region CREATE_TEMP_TABLES_SQL
        private bool CREATE_TEMP_TABLES_SQL()
        {
            try
            {
                #region T_tblTEMP_MONTHLY_DATA_TDS_CALC
                dmlService.J_BeginTransaction();
                //MessageBox.Show("2");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == true)
                {
                    //MessageBox.Show("2.1");
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                    dmlService.J_ExecSql(strSQL);
                }
                //MessageBox.Show("3");
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == false)
                {
                    //MessageBox.Show("3.1");
                    strSQL = @"CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" (
                                            " + cmnService.J_GetDataType("MONTHLY_DATA_TDS_CALC_ID", J_Identity.YES) + @",
                                            " + cmnService.J_GetDataType("SERIAL_NO", J_ColumnType.Integer) + @",
                                            " + cmnService.J_GetDataType("SERIAL_NO_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Integer) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_PAN_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_REFERENCE_NO", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("EMPLOYEE_REFERENCE_NO_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("AMOUNT_PAID", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("AMOUNT_PAID_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("PAYMENT_DATE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("PAYMENT_DATE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("DEDUCTED_DATE", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("DEDUCTED_DATE_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("SECTION_NAME", J_ColumnType.String, 255) + @",
                                            " + cmnService.J_GetDataType("SECTION_NAME_CELL", J_ColumnType.Char) + @",
                                            " + cmnService.J_GetDataType("TDS_PAID", J_ColumnType.String) + @",
                                            " + cmnService.J_GetDataType("TDS_PAID_CELL", J_ColumnType.Char) + @")";
                    //MessageBox.Show(strSQL);
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                #endregion
                //
                //Added by INDRAJIT on 16-03-2012
                dmlService.J_BeginTransaction();
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
                    //strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                    //     "                  ERR_VALIDATION_ID " + TDSMAN.Classes.TDSMAN.T_pCounterIdentity + " ," +
                    //     "                  ERR_TYPE          TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_CELL          TEXT(10) DEFAULT \"\"," +
                    //     "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
                    //     "                  ERR_SHEET         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                    //     "                  ERR_FORM_NO       TEXT(25) DEFAULT \"\")";
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " (" +
                         "                  " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("MODE", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255) + "," +
                         "                  " + cmnService.J_GetDataType("ERR_FORM_NO", J_ColumnType.String, 25) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                //dmlService.J_ExecSql(strSQL);
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
                    //     "                  EMPLOYEE_NAME      TEXT(75) DEFAULT \"\"," +
                    //     "                  EMPLOYEE_PAN       TEXT(10) DEFAULT \"\"," +
                    //     "                  DEDUCTEE_CODE      TEXT(5) DEFAULT \"\"," +
                    //     "                  COMPANY_ID         NUMBER  DEFAULT 0)"; 
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_MASTER_ID", J_Identity.YES) + "," +
                         "                  " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 75) + "," +
                         "                  " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 10) + "," +
                         "                  " + cmnService.J_GetDataType("DEDUCTEE_CODE", J_ColumnType.String, 5) + "," +
                         "                  " + cmnService.J_GetDataType("COMPANY_ID", J_ColumnType.Long) + ")";
                    dmlService.J_ExecSql(strSQL);
                }
                dmlService.J_Commit();
                //
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
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                    dmlService.J_ExecSql(strSQL);
                }

                #region COMMENT
                //Blocked by INDRAJIT on 16-03-2012
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == false)
                //{
                //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " (" +
                //         "                  CHALLAN_DETAILS_ID       COUNTER," +
                //         "                  SERIAL_NO_ORDER  NUMBER    DEFAULT 0," +
                //         "                  SERIAL_NO        TEXT(255) DEFAULT \"\"," +
                //         "                  SERIAL_NO_CELL   TEXT(10)  DEFAULT \"\"," +
                //         "                  SECTION_NAME             TEXT(255) DEFAULT \"\"," +
                //         "                  SECTION_NAME_CELL        TEXT(10)  DEFAULT \"\"," +
                //         "                  TDS                      TEXT(255) DEFAULT \"\"," +
                //         "                  TDS_CELL                 TEXT(10)  DEFAULT \"\"," +
                //         "                  SURCHARGE                TEXT(255) DEFAULT \"\"," +
                //         "                  SURCHARGE_CELL           TEXT(10)  DEFAULT \"\"," +
                //         "                  EDUCATION_CESS           TEXT(255) DEFAULT \"\"," +
                //         "                  EDUCATION_CESS_CELL      TEXT(10)  DEFAULT \"\"," +
                //         "                  INTEREST                 TEXT(255) DEFAULT \"\"," +
                //         "                  INTEREST_CELL            TEXT(10)  DEFAULT \"\"," +
                //         "                  OTHERS                   TEXT(255) DEFAULT \"\"," +
                //         "                  OTHERS_CELL              TEXT(10)  DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEPOSITED      TEXT(255) DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEPOSITED_CELL TEXT(10)  DEFAULT \"\"," +
                //         "                  CTRL_TOT_TAX             CURRENCY  DEFAULT 0," +
                //         "                  NO_OF_DEDUCTEES          INTEGER   DEFAULT 0," +
                //         "                  CHEQUE_NO                TEXT(255) DEFAULT \"\"," +
                //         "                  CHEQUE_NO_CELL           TEXT(10)  DEFAULT \"\"," +
                //         "                  BSR_CODE                 TEXT(255) DEFAULT \"\"," +
                //         "                  BSR_CODE_CELL            TEXT(10)  DEFAULT \"\"," +
                //         "                  DATE_TAX_DEPOSITED       TEXT(255) DEFAULT \"\"," +
                //         "                  DATE_TAX_DEPOSITED_CELL  TEXT(10)  DEFAULT \"\"," +
                //         "                  TRF_VCH_CHLN_NO          TEXT(255) DEFAULT \"\"," +
                //         "                  TRF_VCH_CHLN_NO_CELL     TEXT(10)  DEFAULT \"\"," +
                //         "                  BOOK_ENTRY               TEXT(255) DEFAULT \"\"," +
                //         "                  BOOK_ENTRY_CELL          TEXT(10)  DEFAULT \"\")";
                //    dmlService.J_ExecSql(strSQL);
                //}
                ////
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //dmlService.J_ExecSql(strSQL);
                //--

                //TEXT(50) DEFAULT \"\"," +
                //"Code     MEMO," +
                //"Active   Bit      DEFAULT 0," +
                //"Hits     int      DEFAULT 0," +
                //"Rotation long     DEFAULT 0)";
                #endregion

                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC;
                    dmlService.J_ExecSql(strSQL);
                }

                #region COMMENT
                //Blocked by INDRAJIT on 16-03-2012
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == false)
                //{
                //    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " (" +
                //         "                  DEDUCTEE_DETAILS_ID      COUNTER," +
                //         "                  DEDUCTEE_SERIAL_NO_ORDER NUMBER    DEFAULT 0," +
                //         "                  DEDUCTEE_SERIAL_NO       TEXT(255) DEFAULT \"\"," +
                //         "                  DEDUCTEE_SERIAL_NO_CELL  TEXT(10) DEFAULT \"\"," +
                //         "                  CHALLAN_SERIAL_NO        TEXT(255) DEFAULT \"\"," +
                //         "                  CHALLAN_SERIAL_NO_CELL   TEXT(10) DEFAULT \"\"," +
                //         "                  DEDUCTEE_CODE            TEXT(255) DEFAULT \"\"," +
                //         "                  DEDUCTEE_CODE_CELL       TEXT(10) DEFAULT \"\"," +
                //         "                  EMPLOYEE_PAN             TEXT(255) DEFAULT \"\"," +
                //         "                  EMPLOYEE_PAN_CELL        TEXT(10) DEFAULT \"\"," +
                //         "                  EMPLOYEE_NAME            TEXT(255) DEFAULT \"\"," +
                //         "                  EMPLOYEE_NAME_CELL       TEXT(10) DEFAULT \"\"," +
                //         "                  PAYMENT_DATE             TEXT(255) DEFAULT \"\"," +
                //         "                  PAYMENT_DATE_CELL        TEXT(10) DEFAULT \"\"," +
                //         "                  AMOUNT_PAID              TEXT(255) DEFAULT \"\"," +
                //         "                  AMOUNT_PAID_CELL         TEXT(10) DEFAULT \"\"," +
                //         "                  TDS                      TEXT(255) DEFAULT \"\"," +
                //         "                  TDS_CELL                 TEXT(10) DEFAULT \"\"," +
                //         "                  SURCHARGE                TEXT(255) DEFAULT \"\"," +
                //         "                  SURCHARGE_CELL           TEXT(10) DEFAULT \"\"," +
                //         "                  EDUCATION_CESS           TEXT(255) DEFAULT \"\"," +
                //         "                  EDUCATION_CESS_CELL      TEXT(10) DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEDUCTED       TEXT(255) DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEDUCTED_CELL  TEXT(10) DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEPOSITED      TEXT(255) DEFAULT \"\"," +
                //         "                  TOTAL_TAX_DEPOSITED_CELL TEXT(10) DEFAULT \"\"," +
                //         "                  RATE                     TEXT(255) DEFAULT \"\"," +
                //         "                  RATE_CELL                TEXT(10) DEFAULT \"\"," +
                //         "                  REASON                   TEXT(255) DEFAULT \"\"," +
                //         "                  REASON_CELL              TEXT(10) DEFAULT \"\"," +
                //         "                  GROSSING_UP_TOT_VALUE_PUR TEXT(255) DEFAULT \"\"," +
                //         "                  GROSSING_UP_TOT_VALUE_PUR_CELL TEXT(10) DEFAULT \"\"," +
                //         "                  DEDUCTEE_MASTER_ID       NUMBER  DEFAULT 0)";
                //    dmlService.J_ExecSql(strSQL);
                //}
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "", "DEDUCTEE_MASTER_ID") == false)
                //{
                //    strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD COLUMN DEDUCTEE_MASTER_ID NUMBER  DEFAULT 0";
                //    dmlService.J_ExecSql(strSQL);
                //}
                ////
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //dmlService.J_ExecSql(strSQL);
                #endregion

                //Added by INDRAJIT on 16-03-2012
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
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "";
                dmlService.J_ExecSql(strSQL);

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " (" +
                         "                  DEDUCTEE_MASTER_ID COUNTER," +
                         "                  EMPLOYEE_NAME      TEXT(75) DEFAULT \"\"," +
                         "                  EMPLOYEE_PAN       TEXT(10) DEFAULT \"\"," +
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

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "") == false)
                {
                    strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + " (" +
                         "                  CHALLAN_CONTROL_TOTALS_ID COUNTER," +
                         "                  CHALLAN_ID                NUMBER  DEFAULT 0," +
                         "                  SUM_TAX_AMOUNT            MONEY   DEFAULT 0," +
                         "                  SUM_SURCHARGE_AMOUNT      MONEY   DEFAULT 0," +
                         "                  SUM_CESS_AMOUNT           MONEY   DEFAULT 0," +
                         "                  SUM_TOTAL_AMOUNT          MONEY   DEFAULT 0," +
                         "                  SUM_TAX_DEPOSITED_AMOUNT  MONEY   DEFAULT 0)";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + "";
                dmlService.J_ExecSql(strSQL);
                //
                if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                {
                    if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "") == true)
                    {
                        strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "";
                        dmlService.J_ExecSql(strSQL);
                    }
                    
                }
                //-- 2016/11/17
                if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                {
                    if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "") == true)
                    {
                        strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "";
                        dmlService.J_ExecSql(strSQL);
                    }
                }
                //
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
                //
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC) == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC;
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "") == true)
                {
                    strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
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
                // Salary Details
                int intLineNumberSD = 0;
                string strSerialNo = "", strEmployeeName = "", strEmployeePAN = "", strEmployeeCategory = "", strEmployeeReferenceNo = "";
                string strAmountPaid = "", strPayemntDate = "", strDeductedDate = "", strSectionName = "", strTDSPaid = "";
                //
                //
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
                    strTemporaryfileForm16SalaryPath = Path.Combine(strStartupPath, TDSMAN.Classes.TDSMAN.T_pProductSerial + "_Form16SD.txt");
                    if (File.Exists(strTemporaryfileForm16SalaryPath) == true)
                        File.Delete(strTemporaryfileForm16SalaryPath);
                    //
                }
                else
                {
                    strTemporaryfileForm16SalaryPath = Path.Combine(strStartupPath, "Form16SD.txt");
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
                #region INSERT Salary Details

                #region TRANSFERRING DATA FROM EXCEL TO TEXT FILE

                // INSERT Salary Details

                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                //
                myCommand = new OleDbDataAdapter("SELECT * FROM [Monthly Data$]", con);
                myCommand.Fill(myDataSet);
                StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(strTemporaryfileForm16SalaryPath);
                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessage.Visible = true;
                    //
                    //Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;
                    //
                    intLineNumberSD = intLineNumberSD + 1;
                    int intColumnValue = 64;
                    //
                    strSerialNo = Convert.ToString(cells[0]).ToUpper();
                    strEmployeePAN = Convert.ToString(cells[1]).ToUpper();
                    strEmployeeName = Convert.ToString(cells[2]).ToUpper();
                    //strEmployeeCategory = Convert.ToString(cells[3]).ToUpper();
                    strEmployeeReferenceNo = Convert.ToString(cells[3]).ToUpper();
                    strAmountPaid = Convert.ToString(cells[4]).ToUpper();
                    strPayemntDate = Convert.ToString(cells[5]).ToUpper();
                    strDeductedDate = Convert.ToString(cells[6]).ToUpper();
                    strSectionName = Convert.ToString(cells[7]).ToUpper();
                    strTDSPaid = Convert.ToString(cells[8]).ToUpper();                        
                    //--
                    // CHECK BLANK ROW TO EXIT
                    if (strSerialNo == "" &&
                            strEmployeePAN == "" &&
                              strEmployeeName == "" &&
                                strEmployeeCategory == "" &&
                                    strEmployeeReferenceNo == "" &&
                                        strAmountPaid == "" &&
                                            strPayemntDate == "" &&
                                                strDeductedDate == "" &&
                                                    strSectionName == "" &&
                                                        strTDSPaid == "")
                        break;

                    cmnService.J_WriteLine(ref StreamWriter, TdsMan.T_WriteField(intLineNumberSD.ToString()) + 
                                                                TdsMan.T_WriteField(strSerialNo) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField("0") +
                                                                TdsMan.T_WriteField(strEmployeePAN) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strEmployeeName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strEmployeeReferenceNo) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 4)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strAmountPaid) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 5)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strPayemntDate) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 6)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strDeductedDate) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 7)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strSectionName) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 8)) + (Convert.ToString(intLineNumberSD + 1)))) +
                                                                TdsMan.T_WriteField(strTDSPaid) + TdsMan.T_WriteField((Convert.ToString((char)(intColumnValue + 9)) + (Convert.ToString(intLineNumberSD + 1)))));
                    

                }
                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriter.Flush();
                StreamWriter.Close();

                #endregion

                #region TRANSFERING DATA FROM TEXT TO ACCESS
                //Added by INDRAJIT on 14-03-2012

                //TABLE NAME TO BE CREATED
                //tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                tableName = TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC;

                //TEXT FILE NAME
                textfileName = "Form16SD";

                TdsMan.T_ReplaceDoubleQuotesinFile(strTemporaryfileForm16SalaryPath, true);


                //IMPORTING THE DATA FROM THE TEXT FILE DIRECTLY TO THE 
                ImportTextToTables(tableName, textfileName, strTemporaryfileForm16SalaryPath, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTemporaryfileForm16SalaryPath) == true)
                    File.Delete(strTemporaryfileForm16SalaryPath);

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
            string strFolderPath = cmnService.J_GetDirectoryName(strTemporaryfileForm16SalaryPath);

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
            #region " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "
            StreamWriter.WriteLine(@"Col1=MONTHLY_DATA_TDS_CALC_ID Integer
                                         Col2=SERIAL_NO Char
                                         Col3=SERIAL_NO_CELL Char
                                         Col4=EMPLOYEE_ID Char
                                         Col5=EMPLOYEE_PAN Char
                                         Col6=EMPLOYEE_PAN_CELL Char
                                         Col7=EMPLOYEE_NAME Char
                                         Col8=EMPLOYEE_NAME_CELL Char
                                         Col9=EMPLOYEE_REFERENCE_NO Char
                                         Col10=EMPLOYEE_REFERENCE_NO_CELL Char
                                         Col11=AMOUNT_PAID Char
                                         Col12=AMOUNT_PAID_CELL Char
                                         Col13=PAYMENT_DATE Char
                                         Col14=PAYMENT_DATE_CELL Char
                                         Col15=DEDUCTED_DATE Char
                                         Col16=DEDUCTED_DATE_CELL Char
                                         Col17=SECTION_NAME Char
                                         Col18=SECTION_NAME_CELL Char
                                         Col19=TDS_PAID Char
                                         Col20=TDS_PAID_CELL Char");
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
                       
        #region VALIDATE_DATA_TDS_CALC
        private bool VALIDATE_DATA_TDS_CALC()
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
                //
                #region  SERIAL_NO

                /*
                    *  ------------LIST OF CHECKS FOR SERIAL NO
                    *  1. Duplicate Check 
                    *  2. Blank/Zero Check
                    *  3. Numeric Check
                    *  4. Negative Check
                    *  5. Fraction Check
                    *  6. Sequential Check
                    *  
                */
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET SERIAL_NO = '' WHERE SERIAL_NO IS NULL";
                dmlService.J_ExecSql(strSQL);


                //duplicate check
                strSQL = "SELECT COUNT(SERIAL_NO)" +
                            "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                            "GROUP BY SERIAL_NO " +
                            "HAVING COUNT(SERIAL_NO) > 1";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL AS ERROR_CELL," +
                            "             'SERIAL_NO_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'," +
                            "             '" + cmbFormNo.Text + "'" +
                            "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " INNER JOIN " +
                            "            (SELECT SERIAL_NO " +
                            "             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                            "             GROUP BY SERIAL_NO " +
                            "             HAVING COUNT(SERIAL_NO) > 1) AS RUN " +
                            "      ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO = RUN.SERIAL_NO ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                //BLANK OR 0 CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    "     WHERE  (SERIAL_NO = ''" +
                    "     OR     SERIAL_NO = '0')" +
                    "     AND    SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 24-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL AS ERROR_CELL," +
                        "             'SERIAL_NO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "      ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                        "     WHERE   ERR_V.ERR_CELL               IS NULL " +
                        "     AND    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO = ''" +
                        "     OR     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO  = '0') ";


                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //is numeric check
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    "     WHERE  ISNUMERIC(SERIAL_NO) = 0" +
                    "     AND    SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                     // Modified by Ripan Paul on 24-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL AS ERROR_CELL," +
                        "             'SERIAL_NO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "      ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                        "     WHERE   ERR_V.ERR_CELL               IS NULL " +
                        "     AND     ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO) = 0 ";


                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //Negative Check
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    "     WHERE  " + cmnService.J_SQLDBFormat("SERIAL_NO", J_SQLColFormat.ConvertToMoney) + " < 0" +
                    "     AND    SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {                    
                    // Modified by Ripan Paul on 24-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL AS ERROR_CELL," +
                        "             'SERIAL_NO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "      ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                        "      WHERE  ERR_V.ERR_CELL               IS NULL " +
                        "      AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + " < 0 ";


                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //Fraction Check
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    "     WHERE  (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + ") > 0" +
                    "     AND    SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {                    
                    // Modified by Ripan Paul on 24-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.FORMAT_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL AS ERROR_CELL," +
                        "             'SERIAL_NO_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.FORMAT_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL               IS NULL " +
                        "     AND    (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + ") > 0 ";


                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                //NOW CHECKING IF THERE IS NO ERROR FOUND IN SERIAL NO FIELD
                //strSQL = "SELECT COUNT(*) " +
                //            "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                //            "WHERE  ERR_COLUMN = 'SERIAL_NO_CELL'";

                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount == 0)
                //{
                //    //NOW CHECKING THE SERIAL NO SEQUENCING

                //    strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //    long lngChallanCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //    //strSQL = "SELECT MAX(CINT(SERIAL_NO)) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //    //strSQL = "SELECT MAX(CLNG(SERIAL_NO)) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //    //strSQL = "SELECT MAX(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToMoney) + ") FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //    //-- 2018/04/17 - ANIK
                //    strSQL = "SELECT MAX(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO", J_SQLColFormat.ConvertToNumeric) + ") FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //    long lngMaxSerialNo = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //    if (lngChallanCount != lngMaxSerialNo)
                //    {
                //        //SEQUENCING PROBLEM || SOME SERIAL NO IS MISSING IN BETWEEN

                //        //NOW INSERTING ALL SERIAL NOS AS ERROR 
                //        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                //            "      SELECT '" + T_Error_Type.SEQUENCE_CHECK + "'," +
                //            "             SERIAL_NO_CELL AS ERROR_CELL," +
                //            "             'SERIAL_NO_CELL'," +
                //            "             '" + strSheetName + "'," +
                //            "             '" + T_Error_Type_Color.SEQUENCE_CHECK + "'," +
                //            "             '" + cmbFormNo.Text + "'" +
                //            "      FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                //        //                
                //        if (dmlService.J_ExecSql(strSQL) == false)
                //            return false;
                //    }
                //}


                #endregion
                //

                #region DEDUCTEE PAN

                /*
                    *  ------------LIST OF CHECKS FOR Deductee PAN
                    *  1. Blank Check
                    *  2. Length Check (10)
                    *  3. PAN Strucure Check if not (PANNOTAVBL)
                    *  4. 
                */
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET EMPLOYEE_PAN = '' WHERE EMPLOYEE_PAN IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK OR 0 CHECK
                //
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                    "     AND    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN     = '' " +
                    "     OR      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN    IS NULL) ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN     = '' " +
                        "     OR      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN    IS NULL) ";

                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL  = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                    "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN) <> 10 ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL  = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN) <> 10 ";

                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //PAN STRUCTURE CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                "           (SELECT ERR_CELL " +
                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 1))   <> 0 " +
                "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 2, 1)) <> 0 " +
                "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 3, 1)) <> 0 " +
                "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 4, 1)) <> 0 " +
                "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 5, 1)) <> 0 " +
                "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 6, 4))  = 0 " +
                "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 1))   = -1) " +
                "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN     <> 'PANNOTAVBL' ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (ISNUMERIC(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 1))   <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 2, 1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 3, 1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 4, 1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 5, 1)) <> 0 " +
                        "     OR     ISNUMERIC(" + strMidSubString + "(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 6, 4))  = 0 " +
                        "     OR     ISNUMERIC(RIGHT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN, 1))   = -1) " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN     <> 'PANNOTAVBL' ";

                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    "     WHERE  EMPLOYEE_PAN_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "                                      WHERE ERR_SHEET = '" + strSheetName + @"') 
                          AND    EMPLOYEE_PAN NOT IN (SELECT EMPLOYEE_PAN 
                                                     FROM   TRN_SALARY_DETAILS_PROJECTED_FORM16 
                                                     WHERE  ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                                     AND    COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.MISMATCH_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN_CELL AS ERROR_CELL," +
                        "             'EMPLOYEE_PAN_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.MISMATCH_CHECK + "' " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN_CELL  = ERR_V.ERR_CELL
                              WHERE  ERR_V.ERR_CELL                        IS NULL 
                              AND    EMPLOYEE_PAN NOT IN(SELECT EMPLOYEE_PAN
                                                     FROM   TRN_SALARY_DETAILS_PROJECTED_FORM16
                                                     WHERE  ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                                     AND    COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";

                    //              
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region DEDUCTEE NAME

                /*
                    *  ------------LIST OF CHECKS FOR Deductee Name
                    *  1. Blank Check
                    *  2. Length Check (75 Max)
                */

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET EMPLOYEE_NAME = '' WHERE EMPLOYEE_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                    "     AND    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME     = ''" +
                    "     OR      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME    IS NULL) ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                        "            'EMPLOYEE_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME     = ''" +
                        "     OR      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME    IS NULL) ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                    "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME) > 75 ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL AS ERROR_CELL," +
                        "            'EMPLOYEE_NAME_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.LENGTH_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL            IS NULL " +
                        "     AND    LEN(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME) > 75 ";

                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region EMPLOYEE REFRENCE NO
                #endregion

                #region AMOUNT PAID / CREDITED

                /*
                    *  ------------LIST OF CHECKS FOR Payment Amount
                    *  1. Zero Check
                    *  2. Numeric Check
                    *  3. Negative Check
                */
                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET AMOUNT_PAID = '0' WHERE AMOUNT_PAID IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                    "     AND   (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID      = '' " +
                    "     OR     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID      = '0') ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL AS ERROR_CELL," +
                        "            'AMOUNT_PAID_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND   (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID      = '' " +
                        "     OR     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID      = '0') ";

                    //
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // NUMERIC CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                    "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID) = 0 ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL AS ERROR_CELL," +
                        "            'AMOUNT_PAID_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID) = 0 ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //Negative Check
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                    "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + " < 0 ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL AS ERROR_CELL," +
                        "             'AMOUNT_PAID_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + " < 0 ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                #endregion

                #region DATE OF PAYMENT

                /*
                    *  ------------LIST OF CHECKS FOR Payment Date
                    *  1. Blank Check
                    *  2. Valid Date Check
                    *  3. Date Should be within Qtr
                    *  4. DATE SHOULD Be within Financial Year upto that Qtr for Reason Y
                */

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET PAYMENT_DATE = '' WHERE PAYMENT_DATE IS NULL";
                dmlService.J_ExecSql(strSQL);

                ////UPDATE ALL NULL RECORDS TO ''
                //strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET REASON = '' WHERE REASON IS NULL";
                //dmlService.J_ExecSql(strSQL);

                //BLANK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                    "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE      = '' ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                        "            'PAYMENT_DATE_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE      = '' ";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // VALID DATE CHECK
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL    = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                        "     AND    ISDATE(SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4)) = 0";// +
                //"     AND    ISDATE(SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4)) = 0 ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL    = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                        "     AND    ISDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE) = 0 ";
                //" + cmnService.J_SQLDBFormat("FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL    = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                            "     AND    ISDATE(SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4)) = 0";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL    = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                            "     AND    ISDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE) = 0 ";

                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // CHECKING DATE TO BE WITHIN SELECTED FA YEAR
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), true) + cmnService.J_DateOperator() + " " +
                        "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnEndDateFinancialYear(cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ) " ;
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            <" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)))
                                        + cmnService.J_DateOperator() + " " +
                        "            OR CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnEndDateFinancialYear(cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " ) ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), true) + cmnService.J_DateOperator() + " " +
                            "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnEndDateFinancialYear(cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ) ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            <" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)))
                                            + cmnService.J_DateOperator() + " " +
                            "            OR CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnEndDateFinancialYear(cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " ) ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                // CHECKING DATE TO BE WITHIN THE QUARTER END DATE
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + "  ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + "  ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + "  ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND    CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + "  ";
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                // CHECKING DATE TO BE WITHIN SELECTED QUARTER
                //
                string strSQLWhere = "";
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC, "DEDUCTED_DATE") == true)
                    strSQLWhere = " " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE" + " IS NULL ";
                
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " " +
                        "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ) " +
                        "     AND    " + strSQLWhere;
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            <" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text))
                                        + cmnService.J_DateOperator() + " " +
                        "            OR CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " ) " +
                        "     AND    " + strSQLWhere;
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " " +
                            "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ) " +
                            "     AND    " +  strSQLWhere;
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            <" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text))
                                            + cmnService.J_DateOperator() + " " +
                            "            OR CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            > " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " ) " +
                            "     AND    " +  strSQLWhere;
                    //
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " " +
                        "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) >" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + ") ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND   (CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " " +
                        "            OR CDATE(" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                        "            >" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + ") " ;
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " " +
                            "            OR CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(PAYMENT_DATE,4,2) + '/' + SUBSTRING(PAYMENT_DATE,1,2) + '/' + SUBSTRING(PAYMENT_DATE,7,4), 102), 112) >" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + ") " ;
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   (CDATE(" + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnStartDateFinancialYear(cmbFinancialYear.Text)) + cmnService.J_DateOperator() + " " +
                            "            OR CDATE(" + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            >" + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterEndDate(cmbQuarter.Text, cmbFinancialYear.Text)) + cmnService.J_DateOperator() + ") " ;
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //-- UNDER SELECTED MONTH
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    MONTH(PAYMENT_DATE) <> " + intMONTH_ID;
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL AS ERROR_CELL," +
                            "            'PAYMENT_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND    MONTH(PAYMENT_DATE) <> " + intMONTH_ID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                #endregion

                #region DEDUCTED DATE
                if (TDSMAN.Classes.TDSMAN.T_DeductedDateExcel == true)
                {
                    /*
                     *  ------------LIST OF CHECKS FOR Payment Date
                     *  1. Blank Check
                     *  2. Valid Date Check
                     *  3. Date Should be within Qtr
                     *  4. DATE SHOULD Be within Financial Year upto that Qtr for Reason Y
                    */

                    //UPDATE ALL NULL RECORDS TO ''
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET DEDUCTED_DATE = '' WHERE DEDUCTED_DATE IS NULL";
                    dmlService.J_ExecSql(strSQL);

                    //BLANK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE      = '' " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " > 0 ";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        // Modified by Ripan Paul on 25-06-2013
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "     SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                            "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL AS ERROR_CELL," +
                            "            'DEDUCTED_DATE_CELL'," +
                            "            '" + strSheetName + "'," +
                            "            '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                            "            '" + cmbFormNo.Text + "'" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE      = '' " +
                            "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " > 0 ";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    // VALID DATE CHECK
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL    = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                            "     AND    DEDUCTED_DATE <> '' " +
                            "     AND    ISDATE(SUBSTRING(DEDUCTED_DATE,4,2) + '/' + SUBSTRING(DEDUCTED_DATE,1,2) + '/' + SUBSTRING(DEDUCTED_DATE,7,4)) = 0";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL    = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                            "     AND    DEDUCTED_DATE <> '' " +
                            "     AND    ISDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE) = 0 ";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                                "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL AS ERROR_CELL," +
                                "            'DEDUCTED_DATE_CELL'," +
                                "            '" + strSheetName + "'," +
                                "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                                "            '" + cmbFormNo.Text + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                                "           (SELECT ERR_CELL " +
                                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL    = ERR_V.ERR_CELL " +
                                "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                                "     AND    DEDUCTED_DATE <> '' " +
                                "     AND    ISDATE(SUBSTRING(DEDUCTED_DATE,4,2) + '/' + SUBSTRING(DEDUCTED_DATE,1,2) + '/' + SUBSTRING(DEDUCTED_DATE,7,4)) = 0";
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                                "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL AS ERROR_CELL," +
                                "            'DEDUCTED_DATE_CELL'," +
                                "            '" + strSheetName + "'," +
                                "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                                "            '" + cmbFormNo.Text + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                                "           (SELECT ERR_CELL " +
                                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL    = ERR_V.ERR_CELL " +
                                "     WHERE  ERR_V.ERR_CELL              IS NULL " +
                                "     AND    DEDUCTED_DATE <> '' " +
                                "     AND    ISDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE) = 0 ";
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }

                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND   CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(DEDUCTED_DATE,4,2) + '/' + SUBSTRING(DEDUCTED_DATE,1,2) + '/' + SUBSTRING(DEDUCTED_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                            "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                            "     AND    DEDUCTED_DATE <> '' " +
                            "     AND    (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                            "            <'" + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)) + "' ";
                    //
                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        // Modified by Ripan Paul on 25-06-2013
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                                "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL AS ERROR_CELL," +
                                "            'DEDUCTED_DATE_CELL'," +
                                "            '" + strSheetName + "'," +
                                "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                                "            '" + cmbFormNo.Text + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                                "           (SELECT ERR_CELL " +
                                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                                "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                                "     AND    DEDUCTED_DATE <> '' " +
                                "     AND    CONVERT(CHAR(8),CONVERT(DATETIME,SUBSTRING(DEDUCTED_DATE,4,2) + '/' + SUBSTRING(DEDUCTED_DATE,1,2) + '/' + SUBSTRING(DEDUCTED_DATE,7,4), 102), 112) < " + cmnService.J_DateOperator() + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text), true) + cmnService.J_DateOperator() + " ";
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                                "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL AS ERROR_CELL," +
                                "            'DEDUCTED_DATE_CELL'," +
                                "            '" + strSheetName + "'," +
                                "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                                "            '" + cmbFormNo.Text + "'" +
                                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                                "           (SELECT ERR_CELL " +
                                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE_CELL = ERR_V.ERR_CELL " +
                                "     WHERE  ERR_V.ERR_CELL           IS NULL " +
                                "     AND    DEDUCTED_DATE <> '' " +
                                "     AND   (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + ") " +
                                "            <'" + dtService.J_ConvertyyyyMMdd(TdsMan.T_ReturnQuarterStartDate(cmbQuarter.Text, cmbFinancialYear.Text)) + "' ";
                        //
                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;
                    }
                    //
                }
                #endregion

                #region SECTION_NAME
                /*
                    *  ------------LIST OF CHECKS FOR CHALLAN SECTION_NAME
                    *  1. Blank Check
                    *  2. Valid SECTION_NAME Check
                    *  
                */

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET [SECTION_NAME] = '' WHERE SECTION_NAME IS NULL";
                dmlService.J_ExecSql(strSQL);

                //BLANK CHECK FOR SECTION_NAME
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                    "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = '' ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL AS ERROR_CELL," +
                        "             'SECTION_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]     = '' ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //
                //VALID SECTION_NAME CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    "     LEFT JOIN " +
                    "           (SELECT SECTION_NO " +
                    "            FROM   MST_SECTION " +
                    "            WHERE  FORM_NAME = '" + cmbFormNo.Text + "') AS SEC " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = SEC.SECTION_NO) " +
                    "     LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                    "     WHERE  SEC.SECTION_NO                        IS NULL " +
                    //"     AND    SEC.FORM_NAME  ='" + cmbFormNo.Text + "' " +
                    "     AND    ERR_V.ERR_CELL                        IS NULL ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL AS ERROR_CELL," +
                        "             'SECTION_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "     LEFT JOIN " +
                        "           (SELECT SECTION_NO " +
                        "            FROM   MST_SECTION " +
                        "            WHERE  FORM_NAME = '" + cmbFormNo.Text + "') AS SEC " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = SEC.SECTION_NO) " +
                        "     LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                        "     WHERE  SEC.SECTION_NO                        IS NULL " +
                        //"     AND    SEC.FORM_NAME  ='" + cmbFormNo.Text + "' " +
                        "     AND    ERR_V.ERR_CELL                        IS NULL ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                //CHECKING IF THE SECTION_NAME IS VALID FOR SELECTED FINANCIAL YEAR
                //VALID SECTION_NAME CHECK
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    "     INNER JOIN MST_SECTION " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                    "     LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                    "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                    "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                    "     AND    MST_SECTION.ASST_ID     > " + Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) + " ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL AS ERROR_CELL," +
                        "             'SECTION_NAME_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             '" + cmbFormNo.Text + "'" +
                        "     FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "     INNER JOIN MST_SECTION " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                        "     LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                        "     AND    MST_SECTION.ASST_ID                    > " + Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) + " ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                if (cmbFormNo.Text == T_FormNo.F26Q &&
                        Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= (int)T_FinancialYearID.F2013_14ID)
                {
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM     ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "     INNER JOIN MST_SECTION " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                        "     LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                        "     AND    MST_SECTION.SECTION_NO = '194I' ";

                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        // Modified by Ripan Paul on 25-06-2013
                        strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                            "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                            "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL AS ERROR_CELL," +
                            "             'SECTION_NAME_CELL'," +
                            "             '" + strSheetName + "'," +
                            "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                            "             '" + cmbFormNo.Text + "'" +
                            "     FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                            "     INNER JOIN MST_SECTION " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                            "     LEFT JOIN " +
                            "           (SELECT ERR_CELL " +
                            "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                            "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                            "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                            "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                            "     AND    MST_SECTION.SECTION_NO = '194I' ";

                        //                
                        if (dmlService.J_ExecSql(strSQL) == false)
                            return false;

                    }
                }
                //-- ANIK @ 2013/09/26 for FVU 4.0
                else if (cmbFormNo.Text == T_FormNo.F27Q)
                {
                    //VALID SECTION_NAME CHECK
                    strSQL = "SELECT COUNT(*)" +
                        "     FROM   ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "     INNER JOIN MST_SECTION " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                        "     LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                        "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                        "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                        "     AND    MST_SECTION.SECTION_NO = '194LC' ";

                    lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                    if (lngRowCount > 0)
                    {
                        if (txtDeductorType.Text != "K" && txtDeductorType.Text != "M")
                        {
                            // Modified by Ripan Paul on 25-06-2013
                            strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                                "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                                "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL AS ERROR_CELL," +
                                "             'SECTION_NAME_CELL'," +
                                "             '" + strSheetName + "'," +
                                "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                                "             '" + cmbFormNo.Text + "'" +
                                "     FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                                "     INNER JOIN MST_SECTION " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]      = MST_SECTION.SECTION_NO) " +
                                "     LEFT JOIN " +
                                "           (SELECT ERR_CELL " +
                                "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                                "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                                "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME_CELL = ERR_V.ERR_CELL) " +
                                "     WHERE  ERR_V.ERR_CELL          IS NULL " +
                                "     AND    MST_SECTION.FORM_NAME   ='" + cmbFormNo.Text + "' " +
                                "     AND    MST_SECTION.SECTION_NO  = '194LC' ";

                            //                
                            if (dmlService.J_ExecSql(strSQL) == false)
                                return false;
                        }
                    }
                }

                #endregion
                
                #region TDS PAID TILL DATE

                /*
                 *  ------------LIST OF CHECKS FOR TDS Amount
                 *  1. Numeric Check
                 *  2. Negative Check
                */


                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET TDS_PAID = '0.00' WHERE TDS_PAID IS NULL";
                dmlService.J_ExecSql(strSQL);
                //--- numeric check when not blank
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                //    "     WHERE  TDS <> ''" +
                //    "     AND    ISNUMERIC(TDS) = 0" +
                //    "     AND    TDS_PAID_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                //    "                             WHERE  ERR_SHEET = '" + strSheetName + "')";
                strSQL = "SELECT COUNT(*)" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL        IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID           <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID) = 0 ";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL AS ERROR_CELL," +
                        "            'TDS_PAID_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                        "            ''" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL       = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL        IS NULL " +
                        "     AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID           <> '' " +
                        "     AND    ISNUMERIC(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID) = 0 ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //Negative Check
                //strSQL = "SELECT COUNT(*)" +
                //"     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                //"     WHERE  VAL(TDS_PAID) < 0" +
                //"     AND    TDS_PAID_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                //"                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                    "           (SELECT ERR_CELL " +
                    "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                    "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                    "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL = ERR_V.ERR_CELL " +
                    "     WHERE  ERR_V.ERR_CELL  IS NULL " +
                    "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID", J_SQLColFormat.ConvertToMoney) + " < 0 ";

                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 25-06-2013
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL AS ERROR_CELL," +
                        "             'TDS_PAID_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "             ''" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL  IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID", J_SQLColFormat.ConvertToMoney) + " < 0 ";
                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                #endregion
                //                
                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                return false;
            }
        }
        #endregion   
        
        #region CALCULATE_SUM_F16
        private bool CALCULATE_SUM_F16()
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
                string strSheetName = T_Sheet_Name.SALARY_DETAILS_F16;
                //--
                #region TOTAL SALARY
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TOTAL_SALARY = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_3", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region ALLOWANCE TOTAL
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   LESS_ALLOWANCE_TOTAL = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT3", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT4", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT5", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region BALANCE
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   BALANCE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY", J_SQLColFormat.ConvertToMoney) + " - " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_ALLOWANCE_TOTAL", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region BALANCE < CURRENT SALARY + PREVIOUS SALARY
                strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "" +
                            "     WHERE  " + cmnService.J_SQLDBFormat("BALANCE", J_SQLColFormat.ConvertToMoney) + " <> " +
                            "            " + cmnService.J_SQLDBFormat("CURR_SALARY", J_SQLColFormat.ConvertToMoney) + " + " +
                            "            " + cmnService.J_SQLDBFormat("PREV_SALARY", J_SQLColFormat.ConvertToMoney) + " " +
                            "     AND    CURR_SALARY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_SALARY_CELL AS ERROR_CELL," +
                        "            'CURR_SALARY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_SALARY_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat("BALANCE", J_SQLColFormat.ConvertToMoney) + " <> " +
                        "            " + cmnService.J_SQLDBFormat("CURR_SALARY", J_SQLColFormat.ConvertToMoney) + " + " +
                        "            " + cmnService.J_SQLDBFormat("PREV_SALARY", J_SQLColFormat.ConvertToMoney) + " ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TOTAL SALARY < ALLOWANCE TOTAL
                strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "" +
                            "     WHERE  " + cmnService.J_SQLDBFormat("TOTAL_SALARY", J_SQLColFormat.ConvertToMoney) + " < " +
                            "            " + cmnService.J_SQLDBFormat("LESS_ALLOWANCE_TOTAL", J_SQLColFormat.ConvertToMoney) + " " +
                            "     AND    TOTAL_SALARY_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY_CELL AS ERROR_CELL," +
                        "            'TOTAL_SALARY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat("TOTAL_SALARY", J_SQLColFormat.ConvertToMoney) + " < " +
                        "            " + cmnService.J_SQLDBFormat("LESS_ALLOWANCE_TOTAL", J_SQLColFormat.ConvertToMoney);

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region AGGREGATE OF 4(a) & (b) & (c)
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                             @" SET   AGGREGATE_AMOUNT = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ii", J_SQLColFormat.ConvertToMoney) + " + " +
                                                             cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ia", J_SQLColFormat.ConvertToMoney) + " + " +
                                                             cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16iii", J_SQLColFormat.ConvertToMoney);
                else
                    strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                             @" SET   AGGREGATE_AMOUNT = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ii", J_SQLColFormat.ConvertToMoney) + " + " +
                                                             cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16iii", J_SQLColFormat.ConvertToMoney);
                //--
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region INCOME CHARGEABLE UNDER THE HEAD SALARIES
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   INCOME_CHARGEABLE_SALARIES = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".BALANCE", J_SQLColFormat.ConvertToMoney) + " - " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AGGREGATE_AMOUNT", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region BALANCE < INCOME CHARGEABLE UNDER THE HEAD SALARIES
                strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "" +
                            "     WHERE  " + cmnService.J_SQLDBFormat("BALANCE", J_SQLColFormat.ConvertToMoney) + " < " +
                            "            " + cmnService.J_SQLDBFormat("INCOME_CHARGEABLE_SALARIES", J_SQLColFormat.ConvertToMoney) + " " +
                            "     AND    BALANCE_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY_CELL AS ERROR_CELL," +
                        "            'TOTAL_SALARY_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat("BALANCE", J_SQLColFormat.ConvertToMoney) + " < " +
                        "            " + cmnService.J_SQLDBFormat("INCOME_CHARGEABLE_SALARIES", J_SQLColFormat.ConvertToMoney);

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TOTAL SALARY WITH OTHER INCOME
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TOTAL_INCOME_OTHER_SALARY = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT3", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT4", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region GROSS TOTAL INCOME
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   GROSS_TOTAL_INCOME = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_INCOME_OTHER_SALARY", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_CHARGEABLE_SALARIES", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region GROSS TOTAL 80C
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   GROSS_TOTAL_80C = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT3", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT4", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT5", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT6", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region TOTAL DEDUCTIBLE AMOUNT 80CCE
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TOT_DED_AMT_80CCE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DEDUCTIBLE_TOTAL_80C", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCC", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCD", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region TOTAL DEDUCTIBLE AMOUNT - OTHER SECTIONS
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TOT_DED_AMT_OTHER_SEC = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED3", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED4", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED5", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region AGGREGATE DEDUCTIBLE AMOUNT UNDER CHAPTER VI-A
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   GROSS_TOTAL_DED_CHVIA = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_80CCE", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCG", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_OTHER_SEC", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region TOTAL TAXABLE INCOME
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TOTAL_TAXABLE_INCOME = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " - " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_DED_CHVIA", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region TAX PAYABLE
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   TAX_PAYABLE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_ON_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " + " +
                                                    cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SURCHARGE", J_SQLColFormat.ConvertToMoney) + " + " +
                                                    cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EDUCATION_CESS", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region NET TAX PAYABLE
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   NET_TAX_PAYABLE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_RELIEF", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion

                #region BALANCE < CURRENT SALARY + PREVIOUS SALARY
                strSQL = "SELECT COUNT(*)" +
                            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "" +
                            "     WHERE  " + cmnService.J_SQLDBFormat("TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " <> " +
                            "            " + cmnService.J_SQLDBFormat("CURR_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney) + " + " +
                            "            " + cmnService.J_SQLDBFormat("PREV_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney) + " " +
                            "     AND    TOTAL_TDS_DEDUCTED_CELL NOT IN (SELECT ERR_CELL FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                            "                                      WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    // OTHER_SEC_DESC2
                    strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + "(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                        "     SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "            " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED_CELL AS ERROR_CELL," +
                        "            'TOTAL_TDS_DEDUCTED_CELL'," +
                        "            '" + strSheetName + "'," +
                        "            '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                        "            '" + cmbFormNo.Text + "'" +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_ERR_VALIDATION + " " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    " + cmnService.J_SQLDBFormat("TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " <> " +
                        "            " + cmnService.J_SQLDBFormat("CURR_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney) + " + " +
                        "            " + cmnService.J_SQLDBFormat("PREV_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney) + " ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }
                #endregion

                #region TDS INCLUDING SUPERANNUATION
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                         @" SET   GROSS_TOTAL_INC = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " " +
                          " WHERE  " + strUpper + "(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN, 1)) = 'Y'";
                dmlService.J_ExecSql(strSQL);

                #endregion

                #region SHORTFALL EXCESS / DEDUCTION OF TAX
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                @" SET   SHORTFALL_EXCESS = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NET_TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                                           " " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney) + " - " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney);
                //strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                //         @" SET   SHORTFALL_EXCESS = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NET_TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - GROSS_TOTAL_INC ";
                dmlService.J_ExecSql(strSQL);

                #endregion

                #region LANDLORD_PAN_COUNT
                #region T_tblTEMP_F16_SALARY_DETAILS (LANDLORD_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS, "LANDLORD_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS, "LANDLORD_PAN_COUNT", "NUMBER", "", "NOT NULL", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = 0 WHERE LANDLORD_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion

                #region LENDER_PAN_COUNT
                #region T_tblTEMP_F16_SALARY_DETAILS (LENDER_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS, "LENDER_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS, "LENDER_PAN_COUNT", "NUMBER", "", "NOT NULL", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " SET LENDER_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " SET LENDER_PAN_COUNT = 0 WHERE LENDER_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //--


                return true;
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
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
                    //if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                    //{
                    blnOpenTabPage = true; 
                    tbcExcelImport.SelectTab(tbpImportSalaryDetails);
                    //
                    lblSDFinancialYear.Text = cmbFinancialYear.Text;
                    lblSDCompany.Text       = cmbCompany.Text;
                    lblSDMonth.Text         = cmbMonth.Text;
                    //
                    if(cmbMonthSerialNo.Visible = false)
                    {
                        lblSDMonthBatchLabel.Visible = false;
                        lblSDMonthBatch.Visible = false;
                    }
                    else
                    {
                        if (cmbMonthSerialNo.Text == "")
                        {
                            lblSDMonthBatchLabel.Visible = false;
                            lblSDMonthBatch.Visible = false;
                        }
                        else
                        {
                            lblSDMonthBatchLabel.Visible = true;
                            lblSDMonthBatch.Visible = true;
                            if (cmbMonthSerialNo.Text == strDefaultMonthBatchNoTextCombo)
                            {
                                //lblSDMonthBatch.Text = "1";
                                strSQL = @"SELECT MAX(MONTH_BATCH_NO) 
                                   FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                   WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                                   AND    ASST_ID    = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                                   AND    MONTH_ID   = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                                lblSDMonthBatch.Text = Convert.ToString(cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL)) + 1);
                            }
                            else
                            {
                                //lblSDMonthBatch.Text = Convert.ToString(cmnService.J_ReturnInt32Value(cmbMonthSerialNo.Text) + 1);
                                lblSDMonthBatch.Text = cmbMonthSerialNo.Text;
                            }
                        }
                    }

                    //
                    //lblSDMonthBatch.Text = "24Q";
                    //
                    //strSQL = "SELECT TAN_NO " +
                    //    "     FROM   MST_COMPANY " +
                    //    "     WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                    //lblSDMonth.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    strSQL = "SELECT COUNT(*) " +
                        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
                    lblTotalEmployeeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    if (lngNewDeducteesCreated > 0)
                        lblSDNewDeductees.BackColor = Color.LightGreen;
                    else
                        lblSDNewDeductees.BackColor = System.Drawing.Color.Cornsilk;
                    //
                    lblSDNewDeductees.Text = Convert.ToString(lngNewDeducteesCreated);
                    //--
                    if (lngNewDeducteesCreated > TDSMAN.Classes.TDSMAN.T_MAX_REC_EXCEL_IMPORT_ALERT )
                        btnOpenNewDeducteesFound.Visible = false;
                    //--
                    LoadMonthlyDataGrid();
                    //
                    #region COMMENT
                    //}
                    //else if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails) //-- 2017/01/09
                    //{
                    //    blnOpenTabPage = true;
                    //    tbcExcelImport.SelectTab(tbpImportSalaryDetails);
                    //    //
                    //    lblSDFinancialYear.Text = cmbFinancialYear.Text;
                    //    lblSDCompany.Text = cmbCompany.Text;
                    //    //
                    //    lblFormNoSD.Text = "24Q";
                    //    //
                    //    strSQL = "SELECT TAN_NO " +
                    //        "     FROM   MST_COMPANY " +
                    //        "     WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                    //    lblSDTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //    //
                    //    strSQL = "SELECT COUNT(*) " +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + "";
                    //    lblTotalEmployeeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //    //
                    //    if (lngNewDeducteesCreated > 0)
                    //        lblSDNewDeductees.BackColor = Color.LightGreen;
                    //    else
                    //        lblSDNewDeductees.BackColor = System.Drawing.Color.Cornsilk;
                    //    //
                    //    lblSDNewDeductees.Text = Convert.ToString(lngNewDeducteesCreated);
                    //    //--
                    //    LoadSD16Grid();
                    //    //
                    //}
                    //else
                    //{
                    //    //--
                    //    //tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                    //    blnOpenTabPage = true; 
                    //    tbcExcelImport.SelectTab(tbpImportExcelFile);
                    //    //
                    //    lblFinancialYear.Text = cmbFinancialYear.Text ;
                    //    lblQtr.Text = cmbQuarter.Text;
                    //    lblFormNo.Text = cmbFormNo.Text;
                    //    lblCompanyName.Text = cmbCompany.Text;
                    //    lblTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
                    //    txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAILS_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ""));
                    //    //
                    //    if (chkEnterDeducteeDetailsOnly.Checked == false)
                    //    {
                    //        txtTotalChallanRecords.Enabled = true;
                    //        txtTotalChallanAmount.Enabled = true;
                    //        //
                    //        txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_DETAILS_ID) AS COUNT_CHALLAN_ID FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ""));
                    //        //
                    //        txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " ) AS SUM_TOT_TAX FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "")) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " ) AS SUM_TOT_TAX FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ""))));
                    //    }
                    //    else
                    //    {
                    //        txtTotalChallanRecords.Text = "0";
                    //        txtTotalChallanAmount.Text = "0.00";
                    //        txtTotalChallanRecords.Enabled = false;
                    //        txtTotalChallanAmount.Enabled = false;
                    //    }
                    //    //
                    //    txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " ) AS SUM_TOTAL_AMOUNT FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "")) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + ") AS SUM_TOTAL_AMOUNT FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ""))));
                    //    txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + ") AS PAYMENT_AMOUNT FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "")) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(" + cmnService.J_SQLDBFormat("AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + ") AS PAYMENT_AMOUNT FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ""))));
                    //    //----
                    //    if (lngNewDeducteesCreated > 0)
                    //        lblNewDeducteesFound.BackColor = Color.LightGreen;
                    //    else
                    //        lblNewDeducteesFound.BackColor = System.Drawing.Color.Cornsilk;
                    //    //
                    //    lblNewDeducteesFound.Text = Convert.ToString(lngNewDeducteesCreated);
                    //    //--
                    //    if (lngNewDeducteesCreated > TDSMAN.Classes.TDSMAN.T_MAX_REC_EXCEL_IMPORT_ALERT)
                    //        btnOpenNewDeducteesFound.Visible = false;
                    //    //--
                    //    if (chkEnterDeducteeDetailsOnly.Checked == true)
                    //    {
                    //        dgcViewChallan.Visible = false;
                    //        BtnExit.Text = "Exit";
                    //        //--
                    //        dgcViewDeductee.Visible = true;
                    //        //--------------------------------------------------
                    //        //A particular ID wise retriving the data from database
                    //        if (LoadDeducteeDetailsGrid(0) == false)
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //        LoadChallanGrid();
                    //
                    //}
                    #endregion
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

        #region LoadMonthlyDataGrid
        private void LoadMonthlyDataGrid()
        {
            //----------------------------------------------------------
            string[,] strMatrixSD =  {{"DeducteeDetailsID", "0",   "", "Right", "", "", ""},
                                      {"Srl No.",           "50",  "S", "", "", "", ""},
                                      {"PAN",               "110", "S", "", "", "", ""},
                                      {"Employee Name",     "250", "S", "", "", "", ""},
                                      {"Payment Date",      "100", "dd/MM/yyyy", "", "", "", ""},
                                      {"Paymnet Amount",    "200", "0.00", "R", "", "", "T"},
                                      {"TDS Paid",          "200", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //string[,] strCategoryMatrix = {{"CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
            //                               {"CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
            //                               {"CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
            //                               {"CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
            //                               {"CATEGORY = ''", "F", "", "T"}};
            //
            //-----------------------------------------------------------
            strOrderBy = "MONTHLY_DATA_TDS_CALC_ID";
            strQuery = "SELECT MONTHLY_DATA_TDS_CALC_ID AS DETAILS_ID," +
                      "       SERIAL_NO                 AS SERIAL_NO," +
                      "       EMPLOYEE_PAN              AS PAN," +
                      "       EMPLOYEE_NAME             AS NAME," +
                      "     " + cmnService.J_SQLDBFormat("PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS FROM_D," +
                      "     " + cmnService.J_SQLDBFormat("AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_SALARY," +
                      "     " + cmnService.J_SQLDBFormat("TDS_PAID", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TDS " +
                      "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ";
            
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewSDMonthlData, strSQL, strMatrixSD);       //Show Data into the Grid
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
            strOrderBy = "SD_F16_ID";
            strQuery = "SELECT SD_F16_ID                AS DETAILS_ID," +
                      "       SD_F16_ID   AS SERIAL_NO," +
                      "       EMPLOYEE_PAN              AS PAN," +
                      "       EMPLOYEE_NAME             AS NAME," +
                      "     " + cmnService.J_SQLDBFormat(strCategoryMatrix, J_SQLColFormat.Case_End) + " AS CAT," +
                      "     " + cmnService.J_SQLDBFormat("FROM_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS FROM_D," +
                      "     " + cmnService.J_SQLDBFormat("TO_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS TO_D," +
                      "     " + cmnService.J_SQLDBFormat("TOTAL_SALARY", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_SALARY," +
                      "     " + cmnService.J_SQLDBFormat("TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TDS," +
                      "     " + cmnService.J_SQLDBFormat("SHORTFALL_EXCESS", J_SQLColFormat.ConvertToMoney) + " AS SHORTFALL " +
                      "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " ";

            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewSDMonthlData, strSQL, strMatrixSD);       //Show Data into the Grid
        }
        #endregion

        #region LoadChallanGrid
        private void LoadChallanGrid()
        {
            try
            {
                //--
                #region ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "(ERR_DESC)
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "", "ERR_DESC") == false)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD ERR_DESC VARCHAR(255) DEFAULT ''";                    
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD COLUMN ERR_DESC TEXT(255) DEFAULT \"\"";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET ERR_DESC =''";
                dmlService.J_ExecSql(strSQL);
                //--                
                //string strErr = "Challan total is greater than Deductee total";
                string strErr = "Deductee total is greater than Challan total"; //-- 2016/01/19
                if (TDSMAN.Classes.TDSMAN.T_CopyInterestAllocated == true)
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY = 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY <> 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + "- " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                               //AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                else
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY = 'Y'
                               AND    (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("CTRL_TOT_TAX", J_SQLColFormat.ConvertToMoney) + ") < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET
                                      ERR_DESC= '" + strErr + " ' " +
                             @"WHERE  BOOK_ENTRY <> 'Y' 
                               AND    ((" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + ") - CTRL_TOT_TAX) < 0";
                               //AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                #endregion
                //--
                #region ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "(TOT_TAX)
                if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "", "TOT_TAX") == false)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD TOT_TAX INT DEFAULT 0";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = "ALTER TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ADD COLUMN TOT_TAX NUMBER DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET TOT_TAX = 0";
                dmlService.J_ExecSql(strSQL);
                //--
                if (TDSMAN.Classes.TDSMAN.T_CopyInterestAllocated == true)
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET " +
                              "        TOT_TAX = (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") " +
                              "WHERE  BOOK_ENTRY = 'Y'";
//                               AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET " +
                              "        TOT_TAX = (" + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("FEE", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("INTEREST", J_SQLColFormat.ConvertToMoney) + " - " + cmnService.J_SQLDBFormat("OTHERS", J_SQLColFormat.ConvertToMoney) + ") " +
                              " WHERE  BOOK_ENTRY <> 'Y'";
//                               AND    ((CDBL(TOTAL_TAX_DEPOSITED) - CDBL(FEE) - CDBL(INTEREST) - CDBL(OTHERS)) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                else
                {
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET " +
                               "       TOT_TAX = " + cmnService.J_SQLDBFormat("TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " " +
                               "WHERE  BOOK_ENTRY = 'Y'";
//                               AND    (CDBL(TOTAL_TAX_DEPOSITED) - CTRL_TOT_TAX) < 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" SET " +
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
                                        {"SECTION_NAME No.", strSectionSize, "S", "", "", "", ""},
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
                //string[,] strError = {{"(CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) - " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX) < 0 ", "F", "Error!!", "T"},
                //                        {"JAYA", "F", "", "T"}};
                string[,] strError = {{TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".ERR_DESC <> '' ", "F", "Error!! <Click to know more>", "T"},
                                      {TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".ERR_DESC = '' ", "F", "", "T"}};

                strOrderBy = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_DETAILS_ID";

                #region Commented by Shrey Kejriwal on 05/07/2013
                //strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_DETAILS_ID AS CHALLAN_ID," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO   AS SL_NO," +
                //          "       MST_SECTION.SECTION_NO                   AS SECTION_NO," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TRF_VCH_CHLN_NO     AS CHALLAN_TRF_NO," +
                //          "     " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DATE_TAX_DEPOSITED", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".BSR_CODE            AS BSR_CODE," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".NO_OF_DEDUCTEES     AS NO_OF_DEDUCTEES," +
                //          "       CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) AS TOT_TAX," +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                //          "      (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) - " +
                //          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX)       AS DIFF," +
                //          "    " + cmnService.J_SQLDBFormat(strError, J_SQLColFormat.Case_End) + " AS STATUS " +
                //          "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ", " +
                //          "       MST_SECTION " +
                //          "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME = MST_SECTION.SECTION_NO ";
                #endregion

                strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_DETAILS_ID AS CHALLAN_ID," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO   AS SL_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME        AS SECTION_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TRF_VCH_CHLN_NO     AS CHALLAN_TRF_NO," +
                          "     " + cmnService.J_SQLDBFormat("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DATE_TAX_DEPOSITED", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEPOSIT_DATE," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".BSR_CODE            AS BSR_CODE," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".NO_OF_DEDUCTEES     AS NO_OF_DEDUCTEES," +
                          //"       CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) AS TOT_TAX," +
                          "       TOT_TAX                                  AS TOT_TAX," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                    //"      (CDBL(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED) - " +
                    //"       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX)       AS DIFF," +
                          "      (TOT_TAX - " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CTRL_TOT_TAX)       AS DIFF," +
                          //"       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".ERR_DESC            AS STATUS " +
                          "    " + cmnService.J_SQLDBFormat(strError, J_SQLColFormat.Case_End) + " AS STATUS, " +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".ERR_DESC AS ERR_DESC " +
                          "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ";

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
                                        {"SECTION_NAME", strSectionSize, "S", "", "", "", ""},
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
                strOrderBy = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_DETAILS_ID";
                strQuery = "SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_DETAILS_ID      AS DEDUCTEE_DETAILS_ID," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO        AS SERIAL_NO," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN              AS PAN," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME             AS EMPLOYEE_NAME," +
                          "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[SECTION_NAME]                 AS SECTION_NAME," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS PAYMENT_DATE," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney) + " AS AMOUNT_PAID," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TAX_DEDUCTED," +
                          "       " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney) + " AS TOTAL_TAX_DEPOSITED ";
                if (ChallanId > 0)
                {
                    strQuery = strQuery + "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ", " +
                    "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SERIAL_NO " +
                    "AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_DETAILS_ID = " + ChallanId;
                }
                else
                {
                    strQuery = strQuery + "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ";
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

        #region INSERT_MONTHLY_SALARY_DATA
        public bool INSERT_MONTHLY_SALARY_DATA(long FaYearId, long CompanyId, long MonthId, long MonthBatchNo)
        {
            try
            {
                dmlService.J_BeginTransaction();
                strSQL = @"SELECT COUNT(*) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                           WHERE  COMPANY_ID     = " + CompanyId + @"
                           AND    ASST_ID        = " + FaYearId + @"
                           AND    MONTH_ID       = " + MonthId + @"
                           AND    MONTH_BATCH_NO = " + MonthBatchNo + " ";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) == 0)
                {                    
                    #region INSERT HEADER
                    strSQL = @"INSERT INTO TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                                (COMPANY_ID,
                                                 ASST_ID,
                                                 MONTH_ID,
                                                 MONTH_BATCH_NO,
                                                 DATA_IMPORTED_DATE_TIME)
                                VALUES (" + CompanyId + @",
                                        " + FaYearId + @",
                                        " + MonthId + @",
                                        " + MonthBatchNo + @",
                                        " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + ")";
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                    #endregion
                }
                //--
                #region GET HEADER ID
                strSQL = @"SELECT MAX(SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                               WHERE  COMPANY_ID     = " + CompanyId + @"
                               AND    ASST_ID        = " + FaYearId + @"
                               AND    MONTH_ID       = " + MonthId + @"
                               AND    MONTH_BATCH_NO = " + MonthBatchNo;
                lngSalaryDetailsMonthlyDataProcessingHeaderId = cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL));
                #endregion
                //--
                #region UPDATE NO_OF_RECORDS HEADER
                //long lngNoOfDetailRecords = dmlService.J_ReturnNoOfRows(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC);
                ////
                //strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                //           SET    NO_OF_RECORDS = " + lngNoOfDetailRecords + @" 
                //           WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngSalaryDetailsMonthlyDataProcessingHeaderId;
                //if (dmlService.J_ExecSql(strSQL) == false)
                //{
                //    return false;
                //}
                #endregion
                //--
                #region INSERT DETAIL
                //
                #region UPDATE EMPLOYEE_ID - T_tblTEMP_MONTHLY_DATA_TDS_CALC
                //FOR VALID PAN -- LINKING PAN WITH MASTER
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    "         INNER JOIN MST_EMPLOYEE " +
                    "                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                    "       SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_ID        = MST_EMPLOYEE.EMPLOYEE_ID " +
                    "       WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       <> 'PANNOTAVBL'" +
                    "       AND    MST_EMPLOYEE.COMPANY_ID             = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                if (dmlService.J_ExecSql(strSQL) == false)
                    return false;
                #endregion
                //-- DELETE EXISTING DETAIL
                strSQL = "DELETE FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngSalaryDetailsMonthlyDataProcessingHeaderId;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //-- INSERT
                string[,] strAMOUNT_PAID = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID = ''" , "F", "0", "F"}};
                //
                string[,] strTDS_PAID = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_PAID = ''" , "F", "0", "F"}};
                //
                strSQL = @"INSERT INTO TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL 
                                        (SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID,
                                            DETAIL_SL_NO,
                                            EMPLOYEE_ID,
                                            EMPLOYEE_PAN,
                                            EMPLOYEE_NAME,
                                            EMPLOYEE_REF_NO,
                                            PAYMENT_AMOUNT,
                                            PAYMENT_DATE,
                                            DEDUCTION_DATE,
                                            SECTION_ID,
                                            TDS_PAID_TILL_DATE)
                                     SELECT " + lngSalaryDetailsMonthlyDataProcessingHeaderId + @",
                                            SERIAL_NO, 
                                            EMPLOYEE_ID, 
                                            EMPLOYEE_PAN, 
                                            EMPLOYEE_NAME, 
                                            EMPLOYEE_REFERENCE_NO, 
                                            " + cmnService.J_SQLDBFormat(strAMOUNT_PAID, J_SQLColFormat.Case_End) + @",";
                                            //
                                            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                                            {
                                                strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS PAYMENT_DATE,";
                                                strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE, 103) AS PAYMENT_DATE,";

                                            }
                                            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                                            {
                                                strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE),";
                                                strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE),";
                                            }
                                    strSQL = strSQL + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + @",
                                            " + cmnService.J_SQLDBFormat(strTDS_PAID, J_SQLColFormat.Case_End) + @" 
                                     FROM  (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
                                            LEFT JOIN MST_SECTION
                                            ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".SECTION_NAME = MST_SECTION.SECTION_NO) 
                                     WHERE  MST_SECTION.FORM_NAME ='" + T_FormNo.F24Q + "' ";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                #endregion
                //--
                #region UPDATE HEADER
                // IMPORT_COUNT NO_OF_RECORDS
                strSQL = @"SELECT COUNT(*) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngSalaryDetailsMonthlyDataProcessingHeaderId;
                long lngDetailCount = cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                           SET    DATA_IMPORTED_DATE_TIME = " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + @",
                                  NO_OF_RECORDS           = " + lngDetailCount + @",
                                  IMPORT_COUNT            = " + MonthBatchNo + @",
                                  NO_OF_RECORDS_VALID_PROCESSED   = 0,
                                  NO_OF_RECORDS_INVALID_PROCESSED = 0,
                                  PROCESSING_COUNTER      = 0
                           WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngSalaryDetailsMonthlyDataProcessingHeaderId;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                #endregion
                //--
                dmlService.J_Commit();
                //
                return true;
            }
            catch (Exception err)
            {
                dmlService.J_Rollback();
                return false;
            }
        }
        #endregion



        #region INSERT_CHALLAN_DATA
        private bool INSERT_CHALLAN_DATA(long BasicInfoID)
        {
            IDataReader drdShowRecord = null;
            int intBookEntry = 0;
            long lngSectionID = 0;
            string strFieldName = "";
            string strFieldValue = "";
            long lngMinorHeadID = 0;
            //-- %%%%%%%%%%%%%%%%
            int intSlNoIncremental = 0; 
            if (rbnIncremental.Checked == true)
            {
                //strSQL = "SELECT MAX(SL_NO) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID;
                intSlNoIncremental = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_CHALLAN", "SL_NO", "BASIC_INFO_ID = " + BasicInfoID)));
            }
            //-- %%%%%%%%%%%%%%%%            
            //-- CHALLAN_DETAILS
            strSQL = "SELECT COUNT(*) AS COUNT_TEMP_CHALLAN_DETAILS FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";
            lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
            //
            string[,] strArray;
            strArray = new string[lngRowCount, 17];
            //
            strSQL = "SELECT CHALLAN_DETAILS_ID," +
                "            SERIAL_NO," +
                "            SECTION_NAME," +
                "            TDS," +
                "            SURCHARGE," +
                "            EDUCATION_CESS," +
                "            INTEREST," +
                "            FEE," +
                "            OTHERS," +
                "            TOTAL_TAX_DEPOSITED," +
                "            CHEQUE_NO," +
                "            BSR_CODE," +
                "            " + cmnService.J_SQLDBFormat("DATE_TAX_DEPOSITED", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_TAX_DEPOSITED," +
                "            TRF_VCH_CHLN_NO," +
                "            BOOK_ENTRY, " +
                "            MINOR_HEAD " +
                "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                "     ORDER BY CHALLAN_DETAILS_ID";
            //                
            drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
            //-------------------------------------------------------
            long lngArrayCounter = 0;
            //-------------------------------------------------------
            if (drdShowRecord == null)
                return false;
            //--
            while (drdShowRecord.Read())
            {
                //strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.CHALLAN_DETAILS_ID] = drdShowRecord["CHALLAN_DETAILS_ID"].ToString();
                if (rbnIncremental.Checked == true)
                {
                    strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SERIAL_NO_401] = Convert.ToString(intSlNoIncremental + 1);
                    intSlNoIncremental = intSlNoIncremental + 1;
                }
                else
                    strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SERIAL_NO_401] = drdShowRecord["SERIAL_NO"].ToString();
                //
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TEMP_SERIAL_NO] = drdShowRecord["SERIAL_NO"].ToString();                
                //
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SECTION_NAME_402] = drdShowRecord["SECTION_NAME"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TDS_403] = drdShowRecord["TDS"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SURCHARGE_404] = drdShowRecord["SURCHARGE"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.EDUCATION_CESS_405] = drdShowRecord["EDUCATION_CESS"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.INTEREST_406] = drdShowRecord["INTEREST"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.OTHERS_407] = drdShowRecord["OTHERS"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TOTAL_TAX_DEPOSITED_408] = drdShowRecord["TOTAL_TAX_DEPOSITED"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.CHEQUE_NO_409] = drdShowRecord["CHEQUE_NO"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.BSR_CODE_410] = drdShowRecord["BSR_CODE"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.DATE_TAX_DEPOSITED_411] = drdShowRecord["DATE_TAX_DEPOSITED"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TRF_VCH_CHLN_NO_412] = drdShowRecord["TRF_VCH_CHLN_NO"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.BOOK_ENTRY_413] = drdShowRecord["BOOK_ENTRY"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.FEE] = drdShowRecord["FEE"].ToString();
                strArray[lngArrayCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.MINOR_HEAD] = drdShowRecord["MINOR_HEAD"].ToString();

                lngArrayCounter++;
            }
            drdShowRecord.Close();
            drdShowRecord.Dispose();
            //-- %%%%%%%%%%%%%%%%%%%%%%%
            if (dmlService.J_IsDatabaseObjectExist("TRN_CHALLAN", "TEMP_SL_NO") == false)
            {
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)                    
                    strSQL = "ALTER TABLE TRN_CHALLAN ADD TEMP_SL_NO INT DEFAULT 0";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = "ALTER TABLE TRN_CHALLAN ADD COLUMN TEMP_SL_NO NUMBER DEFAULT 0";                    
                dmlService.J_ExecSql(strSQL);
            }
            //
            strSQL = "UPDATE TRN_CHALLAN SET TEMP_SL_NO = 0";
            dmlService.J_ExecSql(strSQL);
            //-- %%%%%%%%%%%%%%%%%%%%%%%%
            //-----------------------------------------------------------
            if (lngArrayCounter > 0)
            {
                //-------------------------------------------------------------------------
                for (long lngCounter = 0; lngCounter <= lngArrayCounter - 1; lngCounter++)
                {
                    //strArray[lngCounter, (int)T_CHALLAN_DETAILS_COLUMN.SERIAL_NO_401
                    //
                    if (strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.BOOK_ENTRY_413] == "Y")
                        intBookEntry = 1;
                    else
                        intBookEntry = 0;
                    //--
                    if (intBookEntry == 1)
                    {
                        strFieldName = "TRANSFER_VOUCHER_NO";
                        strFieldValue = cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TRF_VCH_CHLN_NO_412].Trim());
                    }
                    else
                    {
                        strFieldName = "CHALLAN_NO";
                        strFieldValue = cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TRF_VCH_CHLN_NO_412].Trim());
                    }
                    //--
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                        lngSectionID = 0;
                    else
                        lngSectionID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_ID FROM MST_SECTION WHERE SECTION_NO ='" + cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SECTION_NAME_402].Trim()) + "'")));
                    //--
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2013_14ID)
                    {
                        //if(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.MINOR_HEAD].Trim() == "")
                        //    lngMinorHeadID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINOR_HEAD_ID FROM MST_MINOR_HEAD WHERE MINOR_HEAD_CODE ='200'")));
                        //else                
                            lngMinorHeadID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MINOR_HEAD_ID FROM MST_MINOR_HEAD WHERE MINOR_HEAD_CODE ='" + cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.MINOR_HEAD].Trim()) + "'")));
                    }
                    else
                        lngMinorHeadID = 0;
                    //-----------------------------------------------------------
                    strSQL = "INSERT INTO TRN_CHALLAN (" +
                             "            BASIC_INFO_ID," +
                             "            SL_NO," +
                             "            SECTION_ID," +
                             "            DEPOSIT_DATE," +
                             "            BSR_CODE," +
                             "        " + strFieldName + "," +
                             "            CHEQUE_NO," +
                             "            TDS," +
                             "            SURCHARGE," +
                             "            EDUCATION_CESS," +
                             "            INTEREST," +
                             "            LATE_FEE," +
                             "            OTHERS," +
                             "            TOT_TAX," +
                             "            BOOK_ENTRY," +
                             "            MINOR_HEAD_ID," +
                             "            TEMP_SL_NO) " +
                             "     VALUES(" + BasicInfoID + "," +
                             "            " + Convert.ToInt32(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SERIAL_NO_401].Trim()) + "," +
                             "            " + lngSectionID + "," +
                             "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.DATE_TAX_DEPOSITED_411].Trim()) + cmnService.J_DateOperator() + "," +
                             "           '" + cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.BSR_CODE_410].Trim()) + "'," +
                             "           '" + strFieldValue + "'," +
                             "           '" + cmnService.J_ReplaceQuote(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.CHEQUE_NO_409].Trim()) + "'," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TDS_403].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.SURCHARGE_404].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.EDUCATION_CESS_405].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.INTEREST_406].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.FEE].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.OTHERS_407].Trim()) + "," +
                             "            " + cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TOTAL_TAX_DEPOSITED_408]) + "," +
                             "            " + intBookEntry + "," +
                             "            " + lngMinorHeadID + "," +
                             "            " + Convert.ToInt32(strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.TEMP_SERIAL_NO].Trim()) + ")";
                    //-----------------------------------------------------------
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                }
            }
            strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET SERIAL_NO_ORDER = SERIAL_NO";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                return false;
            }
            //--
            //-- %%%%%%%%%%%%%%%%%%%%%%
            if (rbnIncremental.Checked == true)
            {
               //
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                         "INNER JOIN TRN_CHALLAN " +
                         "ON     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO = " + cmnService.J_SQLDBFormat("TRN_CHALLAN.TEMP_SL_NO", J_SQLColFormat.ConvertToString) + " " +
                         "SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO = " + cmnService.J_SQLDBFormat("TRN_CHALLAN.SL_NO", J_SQLColFormat.ConvertToString) + " " +
                         "WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
            }
            //-- %%%%%%%%%%%%%%%%%%%%%%
            //----------------------------------------------------------------------------------------------
            //ADDED BY DHRUB ON 30/12/2013 FOR INITIALIZE INTEREST ALLOCATED AND OTHER INTEREST ALLOCATED 
            //----------------------------------------------------------------------------------------------
//            strSQL = @"SELECT FIELD_VALUE 
//                       FROM   MST_PREFERENCES 
//                       WHERE  FIELD_NAME = 'IMPORT_VALUE_TO_ALLOCATED'";

//            strImportValuesToAllocated = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
//            if (Convert.ToInt32(strImportValuesToAllocated) > 0)
//            {
//                strSQL = @"UPDATE TRN_CHALLAN SET TRN_CHALLAN.INTEREST_ALLOCATED = TRN_CHALLAN.INTEREST,
//                                              TRN_CHALLAN.OTHERS_ALLOCATED = TRN_CHALLAN.OTHERS 
//                           WHERE  BASIC_INFO_ID =" + BasicInfoID + "";
//                if (dmlService.J_ExecSql(strSQL) == false)
//                {
//                    return false;
//                }
//            }
            //-- ADDED BY DHRUV ON 11/01/2014 FOR INITIALIZE INTEREST ALLOCATED AND OTHER INTEREST ALLOCATED 
            if (TDSMAN.Classes.TDSMAN.T_CopyInterestAllocated == true)
            {                
                strSQL = @"UPDATE TRN_CHALLAN SET
                                  INTEREST_ALLOCATED = INTEREST,
                                  OTHERS_ALLOCATED   = OTHERS 
                           WHERE  BASIC_INFO_ID =" + BasicInfoID + " ";
                if (rbnIncremental.Checked == true)
                    strSQL = strSQL + " AND TEMP_SL_NO > 0";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
            }
            //--
            //----------------------------------------------------------------------------------------------
            //
            //----------------------------------------------------------------------------------------------

            return true;
        }
        #endregion

        #region INSERT_DEDUCTEE_DATA
        public bool INSERT_DEDUCTEE_DATA(long BasicInfoID)
        {
            try
            {
                //--
                string[,] strAmtPaid = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".AMOUNT_PAID = ''" , "F", "0", "F"}};
                //
                string[,] strRate = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".RATE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".RATE", J_SQLColFormat.ConvertToMoney), "F"},
                                     {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".RATE = ''" , "F", "0", "F"}};
                //
                string[,] strTDS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS", J_SQLColFormat.ConvertToMoney), "F"},
                                    {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS = ''" , "F", "0", "F"}};
                //
                string[,] strSurcharge = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SURCHARGE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SURCHARGE", J_SQLColFormat.ConvertToMoney), "F"},
                                          {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SURCHARGE = ''" , "F", "0", "F"}};
                //
                string[,] strEducationCess = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EDUCATION_CESS <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EDUCATION_CESS", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EDUCATION_CESS = ''" , "F", "0", "F"}};
                //
                string[,] strTaxDed = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEDUCTED = ''" , "F", "0", "F"}};
                //
                string[,] strTaxDep = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TOTAL_TAX_DEPOSITED = ''" , "F", "0", "F"}};
                //
                string[,] strTotValPur = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR = ''" , "F", "0", "F"}};

                #region COMMENT
                //strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                //                 "           (CHALLAN_ID," +
                //                 "            BASIC_INFO_ID," +
                //                 "            SL_NO," +
                //                 "            PARTY_ID," +
                //                 "            PAYMENT_DATE," +
                //                 "            DEDUCTED_DATE," +
                //                 "            PAYMENT_AMOUNT," +
                //                 "            RATE," +
                //                 "            TAX_AMOUNT," +
                //                 "            SURCHARGE_AMOUNT," +
                //                 "            CESS_AMOUNT," +
                //                 "            TOTAL_AMOUNT," +
                //                 "            TAX_DEPOSITED_AMOUNT," +
                //                 "            NON_DEDUCTION_FLAG ";
                //if (cmbFormNo.Text == T_FormNo.F27Q)
                //    strSQL = strSQL + ",GROSSING_UP_INDICATOR)";
                //else if (cmbFormNo.Text == T_FormNo.F27EQ)
                //    strSQL = strSQL + ",TOT_VALUE_PURCHASE)";
                //else
                //    strSQL = strSQL + ")";
                //strSQL = strSQL + " SELECT TRN_CHALLAN.CHALLAN_ID," +
                //       "                   " + BasicInfoID + "," +
                //       //"                   CINT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO)," +
                //       "                   CLNG(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO)," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID," +
                //       "                   CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                //       "                   CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                //       "                  " + cmnService.J_SQLDBFormat(strAmtPaid, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strRate, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTDS, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strSurcharge, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strEducationCess, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTaxDed, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTaxDep, J_SQLColFormat.Case_End) + "," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON";

                //if (cmbFormNo.Text == T_FormNo.F27Q)
                //    strSQL = strSQL + "," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR";
                //else if (cmbFormNo.Text == T_FormNo.F27EQ)
                //    strSQL = strSQL + "," + cmnService.J_SQLDBFormat(strTotValPur, J_SQLColFormat.Case_End) + " ";
                //else
                //    strSQL = strSQL + " ";

                //strSQL = strSQL + " FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "," +
                //    "                    TRN_CHALLAN" +
                //    //"               WHERE CINT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO) = TRN_CHALLAN.SL_NO" +
                //    "               WHERE CLNG(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO) = TRN_CHALLAN.SL_NO" +
                //    "               AND   TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " " +
                //    "               ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO";
                #endregion

                #region COMMENT //--2015/07/23 @@DHRUB
                //strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                //                 "           (CHALLAN_ID," +
                //                 "            BASIC_INFO_ID," +
                //                 "            SL_NO," +
                //                 "            PARTY_ID," +
                //                 "            PAYMENT_DATE," +
                //                 "            DEDUCTED_DATE," +
                //                 "            PAYMENT_AMOUNT," +
                //                 //"            RATE," +
                //                 "            TAX_AMOUNT," +
                //                 "            SURCHARGE_AMOUNT," +
                //                 "            CESS_AMOUNT," +
                //                 "            TOTAL_AMOUNT," +
                //                 "            TAX_DEPOSITED_AMOUNT," +
                //                 "            NON_DEDUCTION_FLAG," +
                //                 "            REASON_ID," +
                //                 "            SECTION_ID," +
                //                 "            CERTIFICATE_NO," +
                //                 "            TDS_APPLICABILITY_ID," +
                //                 "            REMITTANCE_ID," +
                //                 "            UNIQUE_ACKN," +
                //                 "            COUNTRY_ID";
                //if (cmbFormNo.Text == T_FormNo.F27Q)
                //    strSQL = strSQL + ",GROSSING_UP_INDICATOR)";
                //else if (cmbFormNo.Text == T_FormNo.F27EQ)
                //    strSQL = strSQL + ",TOT_VALUE_PURCHASE)";
                //else
                //    strSQL = strSQL + ")";
                //strSQL = strSQL + " SELECT TRN_CHALLAN.CHALLAN_ID," +
                //       "                   " + BasicInfoID + "," +
                //    //"                   CINT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO)," +
                //       "                   CLNG(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO)," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID," +
                //       "                   CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                //       "                   CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                //       "                  " + cmnService.J_SQLDBFormat(strAmtPaid, J_SQLColFormat.Case_End) + "," +
                //       //"                  " + cmnService.J_SQLDBFormat(strRate, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTDS, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strSurcharge, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strEducationCess, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTaxDed, J_SQLColFormat.Case_End) + "," +
                //       "                  " + cmnService.J_SQLDBFormat(strTaxDep, J_SQLColFormat.Case_End) + "," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON," +
                //       "                  " + cmnService.J_SQLDBFormat("REASON_ASPER_FORM.REASON_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                //       "                  " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CERTIFICATE_NO," +
                //       "                  " + cmnService.J_SQLDBFormat("MST_TDS_APPLICABILITY.TDS_APPLICABILITY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                //       "                  " + cmnService.J_SQLDBFormat("MST_REMITTANCE.REMITTANCE_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                //       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".UNIQUE_ACKN," +
                //       "                  " + cmnService.J_SQLDBFormat("MST_COUNTRY.COUNTRY_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck);

                //if (cmbFormNo.Text == T_FormNo.F27Q)
                //    strSQL = strSQL + "," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR";
                //else if (cmbFormNo.Text == T_FormNo.F27EQ)
                //    strSQL = strSQL + "," + cmnService.J_SQLDBFormat(strTotValPur, J_SQLColFormat.Case_End) + " ";
                //else
                //    strSQL = strSQL + " ";

                //strSQL = strSQL + " FROM ((((((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                //    "               INNER JOIN TRN_CHALLAN " +
                //    "                       ON CLNG(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO) = TRN_CHALLAN.SL_NO)" +
                //    "               LEFT  JOIN (SELECT REASON_ID, " +
                //    "                              REASON " +
                //    "                       FROM MST_REASON " +
                //    "                       WHERE FORM_NO = '" + cmbFormNo.Text + "') AS REASON_ASPER_FORM " +
                //    "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON = REASON_ASPER_FORM.REASON) " +
                //    "               LEFT  JOIN MST_SECTION " +
                //    "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME = MST_SECTION.SECTION_NO) " +
                //    "               LEFT  JOIN MST_COUNTRY " +
                //    "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".COUNTRY = MST_COUNTRY.COUNTRY_CODE) " +
                //    "               LEFT  JOIN MST_TDS_APPLICABILITY " +
                //    "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".TDS_APPLICABILITY = MST_TDS_APPLICABILITY.TDS_APPLICABILITY_CODE) " +
                //    "               LEFT  JOIN MST_REMITTANCE " +
                //    "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REMITTANCE = MST_REMITTANCE.REMITTANCE_CODE) " +
                //    "               WHERE TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";
                //if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) > T_FinancialYearID.F2012_13ID) //-- ANIK @ 2015/07/10
                //    strSQL = strSQL + " AND   MST_SECTION.FORM_NAME ='" + lblFormNo.Text + "' ";
                //strSQL = strSQL + "ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO";
                #endregion

                strSQL = "INSERT INTO TRN_DEDUCTEE_DETAILS " +
                                 "           (CHALLAN_ID," +
                                 "            BASIC_INFO_ID," +
                                 "            SL_NO," +
                                 "            PARTY_ID," +
                                 "            PAYMENT_DATE," +
                                 "            DEDUCTED_DATE," +
                                 "            PAYMENT_AMOUNT,";
                //-- 2015-07-23 @@DHRUB
                if (cmbFormNo.Text != T_FormNo.F24Q && TDSMAN.Classes.TDSMAN.T_EnableAutoCalculateTdsRate == false)
                    strSQL = strSQL + "            RATE,";
                strSQL = strSQL + "           TAX_AMOUNT," +
                                 "            SURCHARGE_AMOUNT," +
                                 "            CESS_AMOUNT," +
                                 "            TOTAL_AMOUNT," +
                                 "            TAX_DEPOSITED_AMOUNT," +
                                 "            NON_DEDUCTION_FLAG," +
                                 "            REASON_ID," +
                                 "            SECTION_ID," +
                                 "            CERTIFICATE_NO "; 
                if (cmbFormNo.Text == T_FormNo.F27Q)
                    strSQL = strSQL + ",GROSSING_UP_INDICATOR";
                else if (cmbFormNo.Text == T_FormNo.F27EQ)
                {
                    strSQL = strSQL + ",TOT_VALUE_PURCHASE " +
                                      ",NON_RESIDENT " +
                                      ",PERMANENT_ESTABLISHMENT ";
                }
                //else
                //    strSQL = strSQL + ")";
                strSQL = strSQL + ",PARTY_PAN" +
                                  ",PARTY_NAME)";
                if (chkEnterDeducteeDetailsOnly.Checked == true)
                    strSQL = strSQL + " SELECT 0,";
                else
                    strSQL = strSQL + " SELECT TRN_CHALLAN.CHALLAN_ID,";

                strSQL = strSQL + "        " + BasicInfoID + "," +
                    //"                   CINT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO)," +
                       "                   " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO", J_SQLColFormat.ConvertToNumeric) + "," +
                       "                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID,";
                //--
                //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //    strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS PAYMENT_DATE," +
                //                      " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS DEDUCTED_DATE,";
                //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                //    strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                //                      " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE),";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    //--
                    //strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS PAYMENT_DATE," +
                    //                  " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS DEDUCTED_DATE,";
                    //-- 2017/10/17
                    strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS PAYMENT_DATE,";
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_DeductedDateExcel == true)
                        strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE, 103) AS DEDUCTED_DATE,";
                    else
                        strSQL = strSQL + " CONVERT(DATETIME," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE, 103) AS DEDUCTED_DATE,";
                    //--
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //--
                    //strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE)," +
                    //                  " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE),";
                    //--2017/10/17
                    strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE),";
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_DeductedDateExcel == true)
                        //strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE),";
                        strSQL = strSQL + " " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTED_DATE,";
                    else
                        strSQL = strSQL + " CDATE(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PAYMENT_DATE),";
                    //--
                }
                //--
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strAmtPaid, J_SQLColFormat.Case_End) + ",";
                //-- 2015-07-23 @@ DHRUB
                if (cmbFormNo.Text != T_FormNo.F24Q && TDSMAN.Classes.TDSMAN.T_EnableAutoCalculateTdsRate == false)
                    strSQL = strSQL + "              " + cmnService.J_SQLDBFormat(strRate, J_SQLColFormat.Case_End) + ",";
                strSQL = strSQL + "                  " + cmnService.J_SQLDBFormat(strTDS, J_SQLColFormat.Case_End) + "," +
                                  "                  " + cmnService.J_SQLDBFormat(strSurcharge, J_SQLColFormat.Case_End) + "," +
                                  "                  " + cmnService.J_SQLDBFormat(strEducationCess, J_SQLColFormat.Case_End) + "," +
                                  "                  " + cmnService.J_SQLDBFormat(strTaxDed, J_SQLColFormat.Case_End) + "," +
                                  "                  " + cmnService.J_SQLDBFormat(strTaxDep, J_SQLColFormat.Case_End) + "," +
                                  "                  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON," +
                                  "                  " + cmnService.J_SQLDBFormat("REASON_ASPER_FORM.REASON_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                                  "                  " + cmnService.J_SQLDBFormat("MST_SECTION.SECTION_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                                  "                  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CERTIFICATE_NO ";

                if (cmbFormNo.Text == T_FormNo.F27Q)
                    strSQL = strSQL + "," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".GROSSING_UP_TOT_VALUE_PUR";
                else if (cmbFormNo.Text == T_FormNo.F27EQ)
                {
                    strSQL = strSQL + "," + cmnService.J_SQLDBFormat(strTotValPur, J_SQLColFormat.Case_End) + 
                                      ", " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".NON_RESIDENT " +
                                      ", " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".PERMANENT_ESTABLISHMENT ";
                }
                //else
                //    strSQL = strSQL + " ";
                strSQL = strSQL + "," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN" +
                                  "," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME ";

                if (chkEnterDeducteeDetailsOnly.Checked == true)
                {
                    strSQL = strSQL + " FROM ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "               LEFT  JOIN (SELECT REASON_ID, " +
                        "                              REASON " +
                        "                       FROM MST_REASON " +
                        "                       WHERE FORM_NO = '" + cmbFormNo.Text + "') AS REASON_ASPER_FORM " +
                        "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON = REASON_ASPER_FORM.REASON) " +
                        "               LEFT  JOIN MST_SECTION " +
                        "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME = MST_SECTION.SECTION_NO) " ;// +
                                                                                                                                                           //"               WHERE TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) > T_FinancialYearID.F2012_13ID) //-- ANIK @ 2015/07/10
                        strSQL = strSQL + " WHERE   MST_SECTION.FORM_NAME ='" + lblFormNo.Text + "' ";
                }
                else
                {
                    strSQL = strSQL + " FROM (((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                        "               INNER JOIN TRN_CHALLAN " +
                        "                       ON " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO", J_SQLColFormat.ConvertToNumeric) + " = TRN_CHALLAN.SL_NO)" +
                        "               LEFT  JOIN (SELECT REASON_ID, " +
                        "                              REASON " +
                        "                       FROM MST_REASON " +
                        "                       WHERE FORM_NO = '" + cmbFormNo.Text + "') AS REASON_ASPER_FORM " +
                        "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".REASON = REASON_ASPER_FORM.REASON) " +
                        "               LEFT  JOIN MST_SECTION " +
                        "                       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".SECTION_NAME = MST_SECTION.SECTION_NO) " +
                        "               WHERE TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";
                    if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) > T_FinancialYearID.F2012_13ID) //-- ANIK @ 2015/07/10
                        strSQL = strSQL + " AND   MST_SECTION.FORM_NAME ='" + lblFormNo.Text + "' ";
                }
                strSQL = strSQL + "ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO";

                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //-- POPULATING RATE
                if (cmbFormNo.Text != T_FormNo.F24Q && TDSMAN.Classes.TDSMAN.T_EnableAutoCalculateTdsRate == true)
                {
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        strSQL = "UPDATE TRN_DEDUCTEE_DETAILS " +
                                 "SET    RATE = FORMAT(((TAX_AMOUNT/PAYMENT_AMOUNT)*100), '0.0000')" +
                                 "WHERE  BASIC_INFO_ID = " + BasicInfoID;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                    }
                    else //-- 2016/07/30
                    {
                        //strSQL = "UPDATE TRN_DEDUCTEE_DETAILS " +
                        //         "SET    RATE = CONVERT(DECIMAL(15,4),((CAST(TAX_AMOUNT AS DECIMAL)/CAST(PAYMENT_AMOUNT AS DECIMAL))*100)) " +
                        //         "WHERE  BASIC_INFO_ID = " + BasicInfoID;
                        //-- 2017/03/27
                        strSQL = "UPDATE TRN_DEDUCTEE_DETAILS " +
                                 //"SET    RATE = CONVERT(DECIMAL(15,4),((CAST(TAX_AMOUNT AS DECIMAL(10,2))/CAST(PAYMENT_AMOUNT AS DECIMAL(10,2))) * 100)) " +
                                 "SET    RATE = CONVERT(DECIMAL(15,4),((CAST(TAX_AMOUNT AS DECIMAL(15,2))/CAST(PAYMENT_AMOUNT AS DECIMAL(15,2))) * 100)) " +
                                 "WHERE  BASIC_INFO_ID = " + BasicInfoID ;
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            return false;
                        }
                    }
                }

                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) < T_FinancialYearID.F2013_14ID)
                {
                    strSQL = "UPDATE TRN_DEDUCTEE_DETAILS " +
                             "INNER JOIN TRN_CHALLAN " +
                             "ON     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID " +
                             "SET    TRN_DEDUCTEE_DETAILS.SECTION_ID = TRN_CHALLAN.SECTION_ID " +
                             "WHERE  TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }
                }



                //UPDATE DEDUCTED DATE TO BLANK IF TOTAL DEDUCTION IS ZERO
                strSQL = "UPDATE TRN_DEDUCTEE_DETAILS SET DEDUCTED_DATE = NULL WHERE TOTAL_AMOUNT = 0";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }

                // INSERTING CONTROL TOTALS OF CHALLAN IN TEMP_TABLE

                strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + " " +
                    "                  (CHALLAN_ID, " +
                    "                  SUM_TAX_AMOUNT,  " +
                    "                  SUM_SURCHARGE_AMOUNT, " +
                    "                  SUM_CESS_AMOUNT,  " +
                    "                  SUM_TOTAL_AMOUNT,  " +
                    "                  SUM_TAX_DEPOSITED_AMOUNT)  " +
                    "      SELECT CHALLAN_ID,  " +
                    "             SUM(TAX_AMOUNT), " +
                    "             SUM(SURCHARGE_AMOUNT),  " +
                    "             SUM(CESS_AMOUNT),  " +
                    "             SUM(TOTAL_AMOUNT),  " +
                    "             SUM(TAX_DEPOSITED_AMOUNT)  " +
                    "      FROM   TRN_DEDUCTEE_DETAILS  " +
                    "      WHERE  BASIC_INFO_ID = " + BasicInfoID + "" +
                    "      GROUP  BY CHALLAN_ID";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE CONTROL TOTALS OF CHALLAN TABLE USING TEMP TABLE
                strSQL = "UPDATE TRN_CHALLAN" +
                "                INNER JOIN  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + " " +
                "                  ON TRN_CHALLAN.CHALLAN_ID = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".CHALLAN_ID " +
                "         SET    TRN_CHALLAN.CTRL_TDS       = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".SUM_TAX_AMOUNT," +
                "                TRN_CHALLAN.CTRL_SURCHARGE = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".SUM_SURCHARGE_AMOUNT," +
                "                TRN_CHALLAN.CTRL_EDU_CESS  = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".SUM_CESS_AMOUNT," +
                "                TRN_CHALLAN.CTRL_TOT       = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".SUM_TOTAL_AMOUNT," +
                "                TRN_CHALLAN.CTRL_TOT_TAX   = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_CONTROL_TOTALS + ".SUM_TAX_DEPOSITED_AMOUNT" +
                "         WHERE TRN_CHALLAN.BASIC_INFO_ID = " + BasicInfoID + " ";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //
                return true;
            }
            catch (Exception e)
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
                if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                {
                    #region F24QSalaryDetails
                    //INSERTING NEW EMPLOYEES FOUND IN EXCEL TO THE MASTER
                    strSQL = "INSERT INTO MST_EMPLOYEE " +
                       "                 (EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  GROUP_ID) " +
                       "      SELECT      EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  '" + TDSMAN.Classes.TDSMAN.T_pGroupId + "'" +
                       "      FROM        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    //ADD EMPLOYEE_MASTER_ID IN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " FOR LINKING WITH EMPLOYEE MASTER

                    //FOR VALID PAN -- LINKING PAN WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                      "       SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_ID     = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "       WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN    <> 'PANNOTAVBL'" +
                      "       AND    MST_EMPLOYEE.COMPANY_ID             = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // FOR PANNOTAVBL -- LINKING NAME WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // UPDATE CATEGORY
                    // FOR  PANNOTAVBL
                    strSQL = "UPDATE MST_EMPLOYEE " +
                      "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                      "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CATEGORY " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // FOR  PANAVBL
                    strSQL = "UPDATE MST_EMPLOYEE " +
                      "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN  = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                      "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CATEGORY " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN     <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    #endregion
                }
                else if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                {
                    #region F24QSalaryDetailsForm16
                    //INSERTING NEW EMPLOYEES FOUND IN EXCEL TO THE MASTER
                    strSQL = "INSERT INTO MST_EMPLOYEE " +
                       "                 (EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  GROUP_ID) " +
                       "      SELECT      EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  '" + TDSMAN.Classes.TDSMAN.T_pGroupId + "'" +
                       "      FROM        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    //ADD EMPLOYEE_MASTER_ID IN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " FOR LINKING WITH EMPLOYEE MASTER

                    //FOR VALID PAN -- LINKING PAN WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                      "       SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_ID     = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "       WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN    <> 'PANNOTAVBL'" +
                      "       AND    MST_EMPLOYEE.COMPANY_ID             = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // FOR PANNOTAVBL -- LINKING NAME WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // UPDATE CATEGORY
                    // FOR  PANNOTAVBL
                    strSQL = "UPDATE MST_EMPLOYEE " +
                      "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                      "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CATEGORY " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN     = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // FOR  PANAVBL
                    strSQL = "UPDATE MST_EMPLOYEE " +
                      "         INNER JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN  = MST_EMPLOYEE.EMPLOYEE_PAN  " +
                      "  SET    MST_EMPLOYEE.CATEGORY                = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CATEGORY " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN     <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN            <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID              = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    #endregion
                }
                else if (cmbFormNo.Text == T_FormNo.F24Q)
                {
                    #region F24Q
                    //INSERTING NEW EMPLOYEES FOUND IN EXCEL TO THE MASTER
                    strSQL = "INSERT INTO MST_EMPLOYEE " +
                       "                 (EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  GROUP_ID) " +
                       "      SELECT      EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  COMPANY_ID," +
                       "                  '" + TDSMAN.Classes.TDSMAN.T_pGroupId + "'" +
                       "      FROM        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    //ADD EMPLOYEE_MASTER_ID IN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " FOR LINKING WITH EMPLOYEE MASTER

                    //FOR VALID PAN -- LINKING PAN WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN  = MST_EMPLOYEE.EMPLOYEE_PAN  AND " +
                                       TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME = MST_EMPLOYEE.EMPLOYEE_NAME  " + //-- MODIFIED ON 2018/05/25 - ANIK
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";
                    //"  AND    MST_EMPLOYEE.INACTIVE_FLAG IN ( 0, 1) ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    //FOR VALID PAN -- LINKING PAN WITH MASTER
                    //-- MODIFIED ON 2018/05/30 - ANIK
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = 0 " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID IS NULL ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //--
                    //-- MODIFIED ON 2018/05/28 - ANIK
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN    = MST_EMPLOYEE.EMPLOYEE_PAN   " + 
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       <> 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                      "  AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = 0 ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

                    // FOR PANNOTAVBL -- LINKING NAME WITH MASTER
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                      "         INNER JOIN MST_EMPLOYEE " +
                      "         ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME   = MST_EMPLOYEE.EMPLOYEE_NAME  " +
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.EMPLOYEE_PAN                = 'PANNOTAVBL'" +
                      "  AND    MST_EMPLOYEE.COMPANY_ID                  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //--
                    strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " WHERE DEDUCTEE_MASTER_ID = 0";
                    if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                        return true;
                    else
                        return false;
                    #endregion
                }
                else
                {
                    #region F26Q 27Q 27EQ
                    //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)                    
                    //    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " " + 
                    //             "SET DEDUCTEE_CODE = FORMAT(DEDUCTEE_CODE, \"0#\")";
                    //else 
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    {
                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " " +
                                 "SET DEDUCTEE_CODE = FORMAT(DEDUCTEE_CODE, \"0#\")";

                        dmlService.J_ExecSql(strSQL, J_SQLType.DML);
                    }

                    //INSERT TEMPORARY MASTER CREATED FOR EXCEL IN MAIN MASTER
                    strSQL = "INSERT INTO MST_DEDUCTEE " +
                       "                 (EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  DEDUCTEE_CODE," +
                       "                  GROUP_ID) " +
                       "      SELECT      EMPLOYEE_NAME," +
                       "                  EMPLOYEE_PAN," +
                       "                  DEDUCTEE_CODE," +
                       "                  '" + TDSMAN.Classes.TDSMAN.T_pGroupId + "'" +
                       "      FROM        " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

//                    //ADD DEDUCTEE_MASTER_ID IN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " FOR LINKING WITH DEDUCTEE MASTER
//                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
//                               INNER JOIN MST_DEDUCTEE 
//                                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN  = MST_DEDUCTEE.EMPLOYEE_PAN   
//                               SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".DEDUCTEE_MASTER_ID = MST_DEDUCTEE.DEDUCTEE_ID 
//                               WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       <> 'PANNOTAVBL'";
                    //-- MODIFIED ON 2018/05/25 - ANIK
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
                               INNER JOIN MST_DEDUCTEE 
                                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN  = MST_DEDUCTEE.EMPLOYEE_PAN   AND " +
                                       TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_NAME   = MST_DEDUCTEE.EMPLOYEE_NAME
                               SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".DEDUCTEE_MASTER_ID = MST_DEDUCTEE.DEDUCTEE_ID 
                               WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       <> 'PANNOTAVBL'";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //-- MODIFIED ON 2018/05/30 - ANIK
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                      "  SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = 0 " +
                      "  WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID IS NULL ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //--
                    //-- MODIFIED ON 2018/05/28 - ANIK
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
                               INNER JOIN MST_DEDUCTEE 
                                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN  = MST_DEDUCTEE.EMPLOYEE_PAN  
                               SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".DEDUCTEE_MASTER_ID = MST_DEDUCTEE.DEDUCTEE_ID 
                               WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN       <> 'PANNOTAVBL' 
                               AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".DEDUCTEE_MASTER_ID = 0 ";
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;

//                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " 
//                               INNER JOIN MST_DEDUCTEE 
//                                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME = MST_DEDUCTEE.EMPLOYEE_NAME 
//                        SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_MASTER_ID = MST_DEDUCTEE.DEDUCTEE_ID 
//                        WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN       = 'PANNOTAVBL'";

                    //-- MODIFIED BY ANIK.G @ 2013/05/06
                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
                               INNER JOIN MST_DEDUCTEE 
                                 ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_NAME = MST_DEDUCTEE.EMPLOYEE_NAME 
                        SET    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".DEDUCTEE_MASTER_ID = MST_DEDUCTEE.DEDUCTEE_ID 
                        WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @".EMPLOYEE_PAN       = 'PANNOTAVBL'
                        AND    MST_DEDUCTEE.EMPLOYEE_PAN                = 'PANNOTAVBL'";

                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                    //--
                    strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " WHERE DEDUCTEE_MASTER_ID = 0";
                    if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                        return true;
                    else
                        return false;
                    #endregion
                }
                return true;
                //
                //if (dmlService.J_IsDatabaseObjectExist("" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "") == true)
                //{
                    //strSQL = "SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " WHERE DEDUCTEE_MASTER_ID = 0";
                    //if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                    //    return true;
                    //else
                    //    return false;
                //}
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
        
        #region INSERT_SALARY_DATA
        public bool INSERT_SALARY_DATA(long BasicInfoID)
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
                #region TDS INCLUDING SUPERANNUATION
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                         @" SET    GROSS_TOTAL_INC = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " " +
                          " WHERE  " + strUpper + "(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN, 1)) = 'Y'";
                dmlService.J_ExecSql(strSQL);

                #endregion
                //
                #region SHORTFALL EXCESS / DEDUCTION OF TAX
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET AMT_TAX = 0 WHERE AMT_TAX IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET TOTAL_TDS_DEDUCTED = 0 WHERE TOTAL_TDS_DEDUCTED IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET CONTRIBUTIONS_SUPERANN_YN = '' WHERE CONTRIBUTIONS_SUPERANN_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET RENT_PAYMENT_YN = '' WHERE RENT_PAYMENT_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET INT_PAID_YN = '' WHERE INT_PAID_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--               
                //strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                //@" SET   SHORTFALL_TAX = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                //                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS", J_SQLColFormat.ConvertToMoney) + ") - " +
                //                          " " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney) + " - " +
                //                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney);
                //-- ANIK @ 2017/06/02
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                @" SET   SHORTFALL_TAX = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                                       " " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney) + " - " +
                                             cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);

                #endregion
                //
                #region LANDLORD_PAN_COUNT

                #region T_tblTEMP_SALARY_DETAILS (LANDLORD_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LANDLORD_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LANDLORD_PAN_COUNT", "NUMBER", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = 0 WHERE LANDLORD_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //
                #region LENDER_PAN_COUNT
                #region T_tblTEMP_SALARY_DETAILS (LENDER_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LENDER_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LENDER_PAN_COUNT", "NUMBER", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LENDER_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LENDER_PAN_COUNT = 0 WHERE LENDER_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //--
                string[,] strTS_BALANCE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_EA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_TE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE = ''" , "F", "0", "F"}};
                //-- 2018/12/24
                string[,] strUS_16_IA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_AGGREGATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_CHARGEABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE = ''" , "F", "0", "F"}};
                //
                string[,] strAIS_Total = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AIS_Total <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AIS_Total", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AIS_Total = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME <> ''" , "F", "ROUND( " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + ",2)", "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCE_TOTAL_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCE_TOTAL_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCE_TOTAL_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCE_TOTAL_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCF_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCF_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCF_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCF_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_OTH_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strSCHG_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strECESS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strUS_89_LESS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_PAYABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TDS_DEDUCTED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED = ''" , "F", "0", "F"}};
                //
                string[,] strSHORTFALL_TAX = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX = ''" , "F", "0", "F"}};
                //------------------------------------------------------------------------------
                //ADDED BY DHRUB ON 13/12/2013 FOR ADDING FIVE FIELDS INTO SALARY DETAILS GRID 
                //------------------------------------------------------------------------------
                string[,] strTAXABLE_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strREPORTED_TAXABLE_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                                        {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TAX_DEDUCTED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                                          {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strPREVIOUS_TAX_DEDUCTED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL", J_SQLColFormat.ConvertToMoney) , "F"},
                                                            {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_DEDUCTED_HIGHER_RATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'Y'" , "F", "1", "F"},
                                                         {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'N'" , "F", "0", "F"}};
                //
                string[,] strAMT_REPAID = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID = ''" , "F", "0", "F"}};
                //
                string[,] strRATE_DED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED = ''" , "F", "0", "F"}};
                //
                string[,] strAMT_TAX = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC = ''" , "F", "0", "F"}};
                //--------------------------------------------
                strSQL = "INSERT INTO TRN_SALARY_DETAILS (" +
                        "             BASIC_INFO_ID," +
                        "             EMPLOYEE_ID," +
                        "             FROM_DATE," +
                        "             TO_DATE," +
                        "             TS_BALANCE," +
                        "             US_16_EA," +
                        "             US_16_TE,";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "US_16_IA,";
                strSQL = strSQL + "   US_16_AGGREGATE," +
                        "             INCOME_CHARGEABLE," +
                        "             AIS_Total," +
                        "             GROSS_TOTAL_INCOME," +
                        "             CVIA_SEC80CCE_TOTAL_DED_AMOUNT,";
                ////## Anik 2013/04/25
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) == T_FinancialYearID.F2010_11ID ||
                    Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) == T_FinancialYearID.F2011_12ID)
                    strSQL = strSQL + "CVIA_SEC80CCF_DED_AMOUNT,";
                else //if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2012_13ID)
                    strSQL = strSQL + "CVIA_SEC80CCG_DED_AMOUNT,";
                //--
                strSQL = strSQL + "CVIA_OTH_DED_TOTAL," +
                    "             CVIA_DED_TOTAL," +
                    "             TOTAL_INCOME," +
                    "             TAX_TOTAL_INCOME," +
                    "             SCHG_TOTAL_INCOME," +
                    "             ECESS_TOTAL_INCOME," +
                    "             US_89_LESS," +
                    "             TAX_PAYABLE," +
                    "             TOTAL_TDS_DEDUCTED," +
                    "             SHORTFALL_TAX," +
                    "             TAXABLE_AMOUNT," +
                    "             REPORTED_TAXABLE_AMOUNT," +
                    "             TOTAL_TAX_DEDUCTED_AMOUNT," +
                    "             PREVIOUS_TAX_DEDUCTED_TOTAL," +
                    "             TAX_DEDUCTED_HIGHER_RATE," +
                    "             SL_NO," +
                    "             PARTY_PAN," +
                    "             PARTY_NAME," +
                    "             SUPER_ANN_YN, " +
                    "             SUPER_ANN_NAME, " +
                    "             SUPER_ANN_FROM_DATE, " +
                    "             SUPER_ANN_TO_DATE, " +
                    "             SUPER_ANN_AMOUNT, " +
                    "             SUPER_ANN_RATE, " +
                    "             SUPER_ANN_TAX, " +
                    "             SUPER_ANN_INCOME ";
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2016_17ID)
                    strSQL = strSQL + " ,RENT_EXCEEDING_YN, " +
                    "             LANDLORD_PAN_COUNT, " +
                    "             LANDLORD_1_PAN, " +
                    "             LANDLORD_1_NAME, " +
                    "             LANDLORD_2_PAN, " +
                    "             LANDLORD_2_NAME, " +
                    "             LANDLORD_3_PAN, " +
                    "             LANDLORD_3_NAME, " +
                    "             LANDLORD_4_PAN, " +
                    "             LANDLORD_4_NAME, " +
                    "             INTEREST_PAID_TO_LENDER, " +
                    "             LENDER_PAN_COUNT, " +
                    "             LENDER_1_PAN, " +
                    "             LENDER_1_NAME, " +
                    "             LENDER_2_PAN, " +
                    "             LENDER_2_NAME, " +
                    "             LENDER_3_PAN, " +
                    "             LENDER_3_NAME, " +
                    "             LENDER_4_PAN, " +
                    "             LENDER_4_NAME ";
                strSQL = strSQL + ") " +
                    "   SELECT    " + lngBasicInfoID + "," +
                    "             EMPLOYEE_ID,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME, FROM_DATE, 103)," +
                        "             CONVERT(DATETIME, TO_DATE, 103),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "FROM_DATE," +
                        "             TO_DATE,";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strTS_BALANCE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_EA, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_TE, J_SQLColFormat.Case_End) + ",";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_IA, J_SQLColFormat.Case_End) + ",";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_AGGREGATE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_CHARGEABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strAIS_Total, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCE_TOTAL_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCF_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_OTH_DED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCVIA_DED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSCHG_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strECESS_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_89_LESS, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_PAYABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TDS_DEDUCTED, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSHORTFALL_TAX, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAXABLE_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strREPORTED_TAXABLE_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TAX_DEDUCTED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strPREVIOUS_TAX_DEDUCTED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_DEDUCTED_HIGHER_RATE, J_SQLColFormat.Case_End) + "," +
                        "             SERIAL_NO_ORDER," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                        "             LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN,1) AS SUPERANN_YN," +
                        "             " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_SUPERANN", J_ColumnType.String, J_SQLColFormat.NullCheck) + ",";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME, SUPERANN_FROM_DATE, 103)," +
                        "             CONVERT(DATETIME, SUPERANN_TO_DATE, 103),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "SUPERANN_FROM_DATE," +
                        "             SUPERANN_TO_DATE,";
                strSQL = strSQL + " " + cmnService.J_SQLDBFormat(strAMT_REPAID, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strRATE_DED, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strAMT_TAX, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INC, J_SQLColFormat.Case_End) + " ";
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2016_17ID)
                    strSQL = strSQL + " ,LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RENT_PAYMENT_YN, 1) AS RENT_YN," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".LANDLORD_PAN_COUNT", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INT_PAID_YN, 1) AS INT_YN," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".LENDER_PAN_COUNT", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " ";
                strSQL = strSQL + "   FROM      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                       "   ORDER BY  SERIAL_NO_ORDER";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                // UPDATE TAX_PAYABLE_AGGREGATE
                #region UPDATE
                strSQL = "UPDATE TRN_SALARY_DETAILS " +
                    "     SET    TAX_PAYABLE_AGGREGATE = (TAX_TOTAL_INCOME + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME)";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    RENT_EXCEEDING_YN = 'N' 
                           WHERE  RENT_EXCEEDING_YN = ''  
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    INTEREST_PAID_TO_LENDER = 'N' 
                           WHERE  INTEREST_PAID_TO_LENDER = ''  
                           AND    BASIC_INFO_ID           = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_YN  = 'N' 
                           WHERE  SUPER_ANN_YN  = ''  
                           AND    BASIC_INFO_ID = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                #endregion
                //-- 2017/05/05
                //-- UPDATE SUPER_ANN_FROM_DATE & SUPER_ANN_TO_DATE
                #region UPDATE SUPER_ANN_FROM_DATE & SUPERANN_TO_DATE
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_FROM_DATE = NULL                                   
                           WHERE  SUPER_ANN_FROM_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @"   
                           AND    BASIC_INFO_ID       = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_TO_DATE = NULL                                   
                           WHERE  SUPER_ANN_TO_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @" 
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                #endregion
                //--
                return true;
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
                #region TOTAL SALARY
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                         @" SET   TS_BALANCE = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion
                //
                #region BALANCE
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                         @" SET   BALANCE_AMOUNT = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE", J_SQLColFormat.ConvertToMoney) + " - (" +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                               cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) + ")";
                dmlService.J_ExecSql(strSQL);
                #endregion
                //
                #region SEC10_TOTAL_AMOUNT
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "SEC10_TOTAL_AMOUNT") == false)
                {
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "SEC10_TOTAL_AMOUNT", "MONEY", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET SEC10_TOTAL_AMOUNT = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                         @" SET   SEC10_TOTAL_AMOUNT = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) + " + " +
                                                     cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //
                #region TAX_TOTAL_INCOME_B4_REBATE
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "TAX_TOTAL_INCOME_B4_REBATE") == false)
                {
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "TAX_TOTAL_INCOME_B4_REBATE", "MONEY", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET TAX_TOTAL_INCOME_B4_REBATE = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //
                #region TDS INCLUDING SUPERANNUATION
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                         @" SET    GROSS_TOTAL_INC = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney) + " + " +
                                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " " +
                          " WHERE  " + strUpper + "(LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN, 1)) = 'Y'";
                dmlService.J_ExecSql(strSQL);

                #endregion
                //
                #region SHORTFALL EXCESS / DEDUCTION OF TAX
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET AMT_TAX = 0 WHERE AMT_TAX IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET TOTAL_TDS_DEDUCTED = 0 WHERE TOTAL_TDS_DEDUCTED IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET CONTRIBUTIONS_SUPERANN_YN = '' WHERE CONTRIBUTIONS_SUPERANN_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET RENT_PAYMENT_YN = '' WHERE RENT_PAYMENT_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //UPDATE TO 0 IF 'NULL'                
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + "  SET INT_PAID_YN = '' WHERE INT_PAID_YN IS NULL";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--               
                //strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                //@" SET   SHORTFALL_TAX = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                //                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS", J_SQLColFormat.ConvertToMoney) + ") - " +
                //                          " " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney) + " - " +
                //                                cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney);
                //-- ANIK @ 2017/06/02
                strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                @" SET   SHORTFALL_TAX = " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) + " - " +
                                       " " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney) + " - " +
                                             cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney);
                dmlService.J_ExecSql(strSQL);
                #endregion
                //
                #region LANDLORD_PAN_COUNT

                #region T_tblTEMP_SALARY_DETAILS (LANDLORD_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LANDLORD_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LANDLORD_PAN_COUNT", "NUMBER", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LANDLORD_PAN_COUNT = 0 WHERE LANDLORD_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LANDLORD_PAN_COUNT = LANDLORD_PAN_COUNT + 1 
                           WHERE  PAN_LANDLORD4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //
                #region LENDER_PAN_COUNT
                #region T_tblTEMP_SALARY_DETAILS (LENDER_PAN_COUNT)
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LENDER_PAN_COUNT") == false)
                {
                    //strSQL = "ALTER TABLE TRN_SALARY_DETAILS ADD COLUMN LANDLORD_PAN_COUNT TEXT(2) NOT NULL DEFAULT \"\"";
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "LENDER_PAN_COUNT", "NUMBER", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LENDER_PAN_COUNT = '0'";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //UPDATE ALL NULL RECORDS TO 0
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET LENDER_PAN_COUNT = 0 WHERE LENDER_PAN_COUNT IS NULL";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER1 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER2 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER3 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                        @" SET    LENDER_PAN_COUNT = LENDER_PAN_COUNT + 1 
                           WHERE  PAN_LENDER4 <> ''";
                dmlService.J_ExecSql(strSQL);
                //
                #endregion
                //
                #region AIS_TOTAL
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "AIS_TOTAL") == false)
                {
                    strSQL = dmlService.ReturnALTERSyntaxSequelServer(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "AIS_TOTAL", "MONEY", "", "", "0");
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET AIS_TOTAL = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS, "AIS_TOTAL") == true)
                {
                    strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " SET AIS_TOTAL = " + cmnService.J_SQLDBFormat("INCOME_LOSS_HOUSE_PROPERTY", J_SQLColFormat.ConvertToMoney) + " + " + cmnService.J_SQLDBFormat("INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney) + " ";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //--
                //
                string[,] strGS_SEC17_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_1 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_1", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_1 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_2 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_2", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_2 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_3 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_3", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_GS_SEC_17_3 = ''" , "F", "0", "F"}};
                //
                string[,] strTS_BALANCE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TS_BALANCE = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_5_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_5_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_5_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_5_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10A_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10A_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10A_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10A_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_10AA_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10AA_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10AA_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_10AA_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_13A_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_13A_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_13A_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_13A_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSEC10_OTHERS_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_OTHERS_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_OTHERS_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SEC10_OTHERS_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_EA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_EA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_TE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_TE = ''" , "F", "0", "F"}};
                //-- 2018/12/24
                string[,] strUS_16_IA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_IA = ''" , "F", "0", "F"}};
                //
                string[,] strUS_16_AGGREGATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_16_AGGREGATE = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_CHARGEABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_CHARGEABLE = ''" , "F", "0", "F"}};
                //
                string[,] strAIS_ITEM_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_LOSS_HOUSE_PROPERTY <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_LOSS_HOUSE_PROPERTY", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_LOSS_HOUSE_PROPERTY = ''" , "F", "0", "F"}};
                //
                string[,] strAIS_ITEM_2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_OTHER_SOURCES <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_OTHER_SOURCES", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INCOME_OTHER_SOURCES = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME <> ''" , "F", "ROUND( " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + ",2)", "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80C_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCC_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCC_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCC_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCC_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_1_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80C_CCC_CCD_1_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_1B_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1B_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1B_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_1B_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80CCD_2_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_2_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_2_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80CCD_2_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80D_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80D_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80D_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80D_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80E_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80E_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80E_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80E_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80G_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80G_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80G_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80G_DED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_SEC80TTA_DED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80TTA_DED_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80TTA_DED_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_SEC80TTA_DED_AMOUNT = ''" , "F", "0", "F"}};
                //                
                string[,] strCVIA_OTH_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_OTH_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strCVIA_DED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CVIA_DED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                //string[,] strTAX_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                //                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_TOTAL_INCOME_B4_REBATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strREBATE_US_87A_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REBATE_US_87A_AMOUNT <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REBATE_US_87A_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REBATE_US_87A_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strSCHG_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strECESS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strUS_89_LESS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_PAYABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_PAYABLE = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TDS_DEDUCTED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED = ''" , "F", "0", "F"}};
                //
                string[,] strSHORTFALL_TAX = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SHORTFALL_TAX = ''" , "F", "0", "F"}};
                //------------------------------------------------------------------------------
                //ADDED BY DHRUB ON 13/12/2013 FOR ADDING FIVE FIELDS INTO SALARY DETAILS GRID 
                //------------------------------------------------------------------------------
                string[,] strTAXABLE_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAXABLE_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strREPORTED_TAXABLE_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                                        {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REPORTED_TAXABLE_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TAX_DEDUCTED_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT", J_SQLColFormat.ConvertToMoney) , "F"},
                                                          {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TOTAL_TAX_DEDUCTED_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strPREVIOUS_TAX_DEDUCTED_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL", J_SQLColFormat.ConvertToMoney) , "F"},
                                                            {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PREVIOUS_TAX_DEDUCTED_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_DEDUCTED_HIGHER_RATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'Y'" , "F", "1", "F"},
                                                         {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'N'" , "F", "0", "F"}};
                //
                string[,] strAMT_REPAID = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_REPAID = ''" , "F", "0", "F"}};
                //
                string[,] strRATE_DED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RATE_DED = ''" , "F", "0", "F"}};
                //
                string[,] strAMT_TAX = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".AMT_TAX = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".GROSS_TOTAL_INC = ''" , "F", "0", "F"}};
                //--------------------------------------------
                strSQL = "INSERT INTO TRN_SALARY_DETAILS (" +
                        "             BASIC_INFO_ID," +
                        "             EMPLOYEE_ID," +
                        "             FROM_DATE," +
                        "             TO_DATE," +
                        "             TS_GS_SEC_17_1," +
                        "             TS_GS_SEC_17_2," +
                        "             TS_GS_SEC_17_3," +
                        "             TS_GS_TOTAL," +
                        "             SEC10_5_AMOUNT," +
                        "             SEC10_10_AMOUNT," +
                        "             SEC10_10A_AMOUNT," +
                        "             SEC10_10AA_AMOUNT," +
                        "             SEC10_13A_AMOUNT," +
                        "             TS_LA_TOTAL," +
                        "             SEC10_TOTAL_AMOUNT," +
                        "             TS_BALANCE," +
                        "             US_16_EA," +
                        "             US_16_TE,";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "US_16_IA,";
                strSQL = strSQL + "   US_16_AGGREGATE," +
                        "             INCOME_CHARGEABLE," +
                        "             AIS_ITEM_1_DESC," +
                        "             AIS_ITEM_1," +
                        "             AIS_ITEM_2_DESC," +
                        "             AIS_ITEM_2," +
                        "             AIS_TOTAL," +
                        "             GROSS_TOTAL_INCOME," +
                        "             CVIA_SEC80C_DED_TOTAL," +
                        "             CVIA_SEC80CCC_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_DED_AMOUNT," +
                        //"             CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT," +
                        "             CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_1B_DED_AMOUNT," +
                        "             CVIA_SEC80CCD_2_DED_AMOUNT," +
                        "             CVIA_SEC80D_DED_AMOUNT," +
                        "             CVIA_SEC80E_DED_AMOUNT," +
                        "             CVIA_SEC80G_DED_AMOUNT," +
                        "             CVIA_SEC80TTA_DED_AMOUNT," +
                        "             CVIA_OTH_DED_TOTAL," +
                        "             CVIA_DED_TOTAL," +
                        "             TOTAL_INCOME," +
                        //"             TAX_TOTAL_INCOME," +
                        "             TAX_TOTAL_INCOME_B4_REBATE," +
                        "             REBATE_US_87A_AMOUNT," +
                        "             SCHG_TOTAL_INCOME," +
                        "             ECESS_TOTAL_INCOME," +
                        "             US_89_LESS," +
                        "             TAX_PAYABLE," +
                        "             TOTAL_TDS_DEDUCTED," +
                        "             SHORTFALL_TAX," +
                        "             TAXABLE_AMOUNT," +
                        "             REPORTED_TAXABLE_AMOUNT," +
                        "             TOTAL_TAX_DEDUCTED_AMOUNT," +
                        "             PREVIOUS_TAX_DEDUCTED_TOTAL," +
                        "             TAX_DEDUCTED_HIGHER_RATE," +
                        "             SL_NO," +
                        "             PARTY_PAN," +
                        "             PARTY_NAME," +
                        "             SUPER_ANN_YN, " +
                        "             SUPER_ANN_NAME, " +
                        "             SUPER_ANN_FROM_DATE, " +
                        "             SUPER_ANN_TO_DATE, " +
                        "             SUPER_ANN_AMOUNT, " +
                        "             SUPER_ANN_RATE, " +
                        "             SUPER_ANN_TAX, " +
                        "             SUPER_ANN_INCOME ";
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2016_17ID)
                    strSQL = strSQL + " ,RENT_EXCEEDING_YN, " +
                        "             LANDLORD_PAN_COUNT, " +
                        "             LANDLORD_1_PAN, " +
                        "             LANDLORD_1_NAME, " +
                        "             LANDLORD_2_PAN, " +
                        "             LANDLORD_2_NAME, " +
                        "             LANDLORD_3_PAN, " +
                        "             LANDLORD_3_NAME, " +
                        "             LANDLORD_4_PAN, " +
                        "             LANDLORD_4_NAME, " +
                        "             INTEREST_PAID_TO_LENDER, " +
                        "             LENDER_PAN_COUNT, " +
                        "             LENDER_1_PAN, " +
                        "             LENDER_1_NAME, " +
                        "             LENDER_2_PAN, " +
                        "             LENDER_2_NAME, " +
                        "             LENDER_3_PAN, " +
                        "             LENDER_3_NAME, " +
                        "             LENDER_4_PAN, " +
                        "             LENDER_4_NAME, " +
                        "             CVIA_SEC80C_GROSS_TOTAL, " +
                        "             CVIA_SEC80CCC_GROSS_AMOUNT, " +
                        "             CVIA_SEC80CCD_GROSS_AMOUNT," +
                        "             CVIA_SEC80CCE_TOTAL_GROSS_AMOUNT," +
                        "             CVIA_SEC80CCD_1B_GROSS_AMOUNT, " +
                        "             CVIA_SEC80CCD_2_GROSS_AMOUNT, " +
                        "             CVIA_SEC80D_GROSS_AMOUNT, " +
                        "             CVIA_SEC80E_GROSS_AMOUNT, " +
                        "             CVIA_SEC80G_GROSS_AMOUNT, " +
                        "             CVIA_SEC80G_QUAL_AMOUNT, " +
                        "             CVIA_SEC80TTA_GROSS_AMOUNT, " +
                        "             CVIA_SEC80TTA_QUAL_AMOUNT, " +
                        "             CVIA_OTH_GROSS_TOTAL, " +
                        "             CVIA_OTH_QUAL_TOTAL ";
                strSQL = strSQL + ") " +
                    "   SELECT    " + lngBasicInfoID + "," +
                    "             EMPLOYEE_ID,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME, FROM_DATE, 103)," +
                        "             CONVERT(DATETIME, TO_DATE, 103),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "FROM_DATE," +
                        "             TO_DATE,";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strGS_SEC17_1, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_2, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_3, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTS_BALANCE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_5_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10A_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_10AA_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_13A_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSEC10_OTHERS_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             SEC10_TOTAL_AMOUNT," +
                        "             BALANCE_AMOUNT," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_EA, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_16_TE, J_SQLColFormat.Case_End) + ",";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_IA, J_SQLColFormat.Case_End) + ",";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strUS_16_AGGREGATE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_CHARGEABLE, J_SQLColFormat.Case_End) + "," +
                        "             'INCOME / LOSS - HOUSE PROPERTY OFFERED FOR TDS'," +
                        "             " + cmnService.J_SQLDBFormat(strAIS_ITEM_1, J_SQLColFormat.Case_End) + "," +
                        "             'INCOME-OTHER SOURCES OFFERED FOR TDS'," +
                        "             " + cmnService.J_SQLDBFormat(strAIS_ITEM_2, J_SQLColFormat.Case_End) + "," +
                        "             AIS_TOTAL," +
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
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_TOTAL_INCOME_B4_REBATE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strREBATE_US_87A_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSCHG_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strECESS_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strUS_89_LESS, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_PAYABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TDS_DEDUCTED, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSHORTFALL_TAX, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAXABLE_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strREPORTED_TAXABLE_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TAX_DEDUCTED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strPREVIOUS_TAX_DEDUCTED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_DEDUCTED_HIGHER_RATE, J_SQLColFormat.Case_End) + "," +
                        "             SERIAL_NO_ORDER," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                        "             LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN,1) AS SUPERANN_YN," +
                        "             " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_SUPERANN", J_ColumnType.String, J_SQLColFormat.NullCheck) + ",";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME, SUPERANN_FROM_DATE, 103)," +
                        "             CONVERT(DATETIME, SUPERANN_TO_DATE, 103),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "SUPERANN_FROM_DATE," +
                        "             SUPERANN_TO_DATE,";
                strSQL = strSQL + " " + cmnService.J_SQLDBFormat(strAMT_REPAID, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strRATE_DED, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strAMT_TAX, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INC, J_SQLColFormat.Case_End) + " ";
                if (Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex) >= T_FinancialYearID.F2016_17ID)
                    strSQL = strSQL + " ,LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".RENT_PAYMENT_YN, 1) AS RENT_YN," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".LANDLORD_PAN_COUNT", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LANDLORD4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LANDLORD4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".INT_PAID_YN, 1) AS INT_YN," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".LENDER_PAN_COUNT", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER1", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER2", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER3", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".PAN_LENDER4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".NAME_LENDER4", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80C_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCC_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_1_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80C_CCC_CCD_1_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_1B_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80CCD_2_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80D_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80E_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80G_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80G_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80TTA_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_SEC80TTA_DED_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_OTH_DED_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "           " + cmnService.J_SQLDBFormat(strCVIA_OTH_DED_TOTAL, J_SQLColFormat.Case_End) + " ";
                strSQL = strSQL + "   FROM      " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                       "   ORDER BY  SERIAL_NO_ORDER";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                // UPDATE TAX_PAYABLE_AGGREGATE
                #region UPDATE
                strSQL = "UPDATE TRN_SALARY_DETAILS " +
                    "     SET    TAX_TOTAL_INCOME = TAX_TOTAL_INCOME_B4_REBATE - REBATE_US_87A_AMOUNT";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = "UPDATE TRN_SALARY_DETAILS " +
                    "     SET    TAX_PAYABLE_AGGREGATE = (TAX_TOTAL_INCOME + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME) - US_89_LESS";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                
                //
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    RENT_EXCEEDING_YN = 'N' 
                           WHERE  RENT_EXCEEDING_YN = ''  
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    INTEREST_PAID_TO_LENDER = 'N' 
                           WHERE  INTEREST_PAID_TO_LENDER = ''  
                           AND    BASIC_INFO_ID           = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_YN  = 'N' 
                           WHERE  SUPER_ANN_YN  = ''  
                           AND    BASIC_INFO_ID = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                #endregion
                //-- 2017/05/05
                //-- UPDATE SUPER_ANN_FROM_DATE & SUPER_ANN_TO_DATE
                #region UPDATE SUPER_ANN_FROM_DATE & SUPERANN_TO_DATE
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_FROM_DATE = NULL                                   
                           WHERE  SUPER_ANN_FROM_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @"   
                           AND    BASIC_INFO_ID       = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_TO_DATE = NULL                                   
                           WHERE  SUPER_ANN_TO_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @" 
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                #endregion
                //-- 2019/05/11
                #region UPDATE > FORM16B_NEW_FORMAT_18_19
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                {
                    //-- CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT
                    strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    CVIA_TOTAL_80C_80CCC_80CCD_1_DED_AMOUNT = CVIA_SEC80C_DED_TOTAL +  CVIA_SEC80CCC_DED_AMOUNT + CVIA_SEC80CCD_DED_AMOUNT
                           WHERE  BASIC_INFO_ID     = " + lngBasicInfoID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }

                    //-- FORM16B_NEW_FORMAT_18_19
                    strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    FORM16B_NEW_FORMAT_18_19 = 1 
                           WHERE  BASIC_INFO_ID     = " + lngBasicInfoID;
                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        return false;
                    }

                    //-- 2019/05/15
                    if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    {
                        strSQL = @" UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS +
                                @" SET   SHORTFALL_TAX = (" + cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".TAX_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " + " +
                                                              cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".SCHG_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + " + " +
                                                              cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".ECESS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney) + ") - (" +
                                                              cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".US_89_LESS", J_SQLColFormat.ConvertToMoney) + " + " +
                                                              cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".REBATE_US_87A_AMOUNT", J_SQLColFormat.ConvertToMoney) + ") ";
                        dmlService.J_ExecSql(strSQL);
                    }
                }
                #endregion
                //--
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region INSERT_SALARYF16_DATA
        public bool INSERT_SALARYF16_DATA(long BasicInfoID)
        {
            try
            {//--
                string strUpper = "";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    strUpper = "UPPER";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strUpper = "UCASE";
                }
                //
                string[,] strGS_SEC17_1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_1 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_1", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_1 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_2 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_2", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_2 = ''" , "F", "0", "F"}};
                //
                string[,] strGS_SEC17_3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_3 <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_3", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GS_SEC17_3 = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_SALARY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY <> ''" , "F", cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_SALARY = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_US10_AMT1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT1", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT1 = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_US10_AMT2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT2", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT2 = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_US10_AMT3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT3", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT3 = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_US10_AMT4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT4", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT4 = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_US10_AMT5 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT5 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT5", J_SQLColFormat.ConvertToMoney) , "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_AMT5 = ''" , "F", "0", "F"}};
                //
                string[,] strLESS_ALLOWANCE_TOTAL = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_ALLOWANCE_TOTAL <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_ALLOWANCE_TOTAL", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_ALLOWANCE_TOTAL = ''" , "F", "0", "F"}};
                //
                string[,] strBALANCE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".BALANCE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".BALANCE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".BALANCE = ''" , "F", "0", "F"}};
                //
                string[,] strCURR_SALARY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_SALARY <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_SALARY", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_SALARY = ''" , "F", "0", "F"}};
                //
                string[,] strPREV_SALARY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_SALARY <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_SALARY", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_SALARY = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_DEDUCTION_16ii = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ii <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ii", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ii = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_DEDUCTION_16iii = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16iii <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16iii", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16iii = ''" , "F", "0", "F"}};
                //-- 2018/12/24
                string[,] strGROSS_DEDUCTION_16ia = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ia <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ia", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_DEDUCTION_16ia = ''" , "F", "0", "F"}};
                //
                string[,] strAGGREGATE_AMOUNT = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AGGREGATE_AMOUNT <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AGGREGATE_AMOUNT", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AGGREGATE_AMOUNT = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_CHARGEABLE_SALARIES = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_CHARGEABLE_SALARIES <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_CHARGEABLE_SALARIES", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_CHARGEABLE_SALARIES = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_OTHER_SALARY_AMT1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT1", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT1 = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_OTHER_SALARY_AMT2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT2", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT2 = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_OTHER_SALARY_AMT3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT3", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT3 = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_OTHER_SALARY_AMT4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT4", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_AMT4 = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_INCOME_OTHER_SALARY = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_INCOME_OTHER_SALARY <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_INCOME_OTHER_SALARY", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_INCOME_OTHER_SALARY = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INCOME <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT1", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT1 = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT2", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT2 = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT3", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT3 = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT4", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT4 = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT5 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT5 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT5", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT5 = ''" , "F", "0", "F"}};
                //
                string[,] strDED_CHVIA_80C_AMT6 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT6 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT6", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_AMT6 = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_80C = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_80C <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_80C", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_80C = ''" , "F", "0", "F"}};
                //
                string[,] strDEDUCTIBLE_TOTAL_80C = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DEDUCTIBLE_TOTAL_80C <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DEDUCTIBLE_TOTAL_80C", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DEDUCTIBLE_TOTAL_80C = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_AMT_SEC80CCC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCC = ''" , "F", "0", "F"}};
                //
                string[,] strDED_AMT_SEC80CCC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCC = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_AMT_SEC80CCD = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCD <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCD", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCD = ''" , "F", "0", "F"}};
                //
                string[,] strDED_AMT_SEC80CCD = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCD <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCD", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCD = ''" , "F", "0", "F"}};
                //
                string[,] strTOT_DED_AMT_80CCE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_80CCE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_80CCE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_80CCE = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_AMT_SEC80CCG = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCG <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCG", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_AMT_SEC80CCG = ''" , "F", "0", "F"}};
                //
                string[,] strDED_AMT_SEC80CCG = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCG <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCG", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_AMT_SEC80CCG = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_GROSS1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS1", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS1 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_QUAL1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL1", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL1 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_DED1 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED1 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED1", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED1 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_GROSS2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS2", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS2 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_QUAL2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL2", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL2 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_DED2 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED2 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED2", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED2 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_GROSS3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS3", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS3 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_QUAL3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL3", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL3 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_DED3 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED3 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED3", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED3 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_GROSS4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS4", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS4 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_QUAL4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL4", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL4 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_DED4 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED4 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED4", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED4 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_GROSS5 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS5 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS5", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_GROSS5 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_QUAL5 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL5 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL5", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_QUAL5 = ''" , "F", "0", "F"}};
                //
                string[,] strOTHER_SEC_DED5 = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED5 <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED5", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DED5 = ''" , "F", "0", "F"}};
                //
                string[,] strTOT_DED_AMT_OTHER_SEC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_OTHER_SEC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_OTHER_SEC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOT_DED_AMT_OTHER_SEC = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_DED_CHVIA = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_DED_CHVIA <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_DED_CHVIA", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_DED_CHVIA = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TAXABLE_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TAXABLE_INCOME <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TAXABLE_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TAXABLE_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_TAX_ON_TOTAL_INCOME = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_ON_TOTAL_INCOME <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_ON_TOTAL_INCOME", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_ON_TOTAL_INCOME = ''" , "F", "0", "F"}};
                //
                string[,] strSURCHARGE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SURCHARGE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SURCHARGE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SURCHARGE = ''" , "F", "0", "F"}};
                //
                string[,] strEDUCATION_CESS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EDUCATION_CESS <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EDUCATION_CESS", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EDUCATION_CESS = ''" , "F", "0", "F"}};
                //
                string[,] strTAX_PAYABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_PAYABLE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_PAYABLE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_PAYABLE = ''" , "F", "0", "F"}};
                //
                string[,] strINCOME_TAX_RELIEF = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_RELIEF <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_RELIEF", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_TAX_RELIEF = ''" , "F", "0", "F"}};
                //
                string[,] strNET_TAX_PAYABLE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NET_TAX_PAYABLE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NET_TAX_PAYABLE", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NET_TAX_PAYABLE = ''" , "F", "0", "F"}};
                //
                string[,] strTOTAL_TDS_DEDUCTED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TOTAL_TDS_DEDUCTED = ''" , "F", "0", "F"}};
                //
                string[,] strCURR_EMPLOYER_TDS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_EMPLOYER_TDS <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CURR_EMPLOYER_TDS = ''" , "F", "0", "F"}};
                //
                string[,] strPREV_EMPLOYER_TDS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_EMPLOYER_TDS <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_EMPLOYER_TDS", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PREV_EMPLOYER_TDS = ''" , "F", "0", "F"}};
                //
                string[,] strSHORTFALL_EXCESS = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SHORTFALL_EXCESS  <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SHORTFALL_EXCESS ", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".SHORTFALL_EXCESS  = ''" , "F", "0", "F"}};
                //
                //string[,] strTAX_DEDUCTED_HIGHER_RATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE", J_SQLColFormat.ConvertToMoney), "F"},
                //                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = ''" , "F", "0", "F"}};
                //-- ANIK 2018/05/25
                string[,] strTAX_DEDUCTED_HIGHER_RATE = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'Y'" , "F", "1", "F"},
                                                         {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TAX_DEDUCTED_HIGHER_RATE = 'N'" , "F", "0", "F"}};
                //
                string[,] strTDS_SUPERANN = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TDS_SUPERANN <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TDS_SUPERANN", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".TDS_SUPERANN = ''" , "F", "0", "F"}};
                //
                string[,] strAMT_REPAID = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_REPAID <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_REPAID", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_REPAID = ''" , "F", "0", "F"}};
                //
                string[,] strRATE_DED = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".RATE_DED <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".RATE_DED", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".RATE_DED = ''" , "F", "0", "F"}};
                //
                string[,] strAMT_TAX = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_TAX <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_TAX", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".AMT_TAX = ''" , "F", "0", "F"}};
                //
                string[,] strGROSS_TOTAL_INC = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INC <> ''" , "F",cmnService.J_SQLDBFormat(TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INC", J_SQLColFormat.ConvertToMoney), "F"},
                                      {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".GROSS_TOTAL_INC = ''" , "F", "0", "F"}};
                //--------------------------------------------
                strSQL = "INSERT INTO TRN_SALARY_DETAILS (" +
                                "             BASIC_INFO_ID," +
                                "             EMPLOYEE_ID," +
                                "             FROM_DATE," +
                                "             TO_DATE," +
                                "             ENTRY_MODE," +
                                "             TS_GS_SEC_17_1," +
                                "             TS_GS_SEC_17_2," +
                                "             TS_GS_SEC_17_3," +
                                "             TS_GS_TOTAL," +
                                "             TS_LA_ITEM_1_DESC," +
                                "             TS_LA_ITEM_1," +
                                "             TS_LA_ITEM_2_DESC," +
                                "             TS_LA_ITEM_2," +
                                "             TS_LA_ITEM_3_DESC," +
                                "             TS_LA_ITEM_3," +
                                "             TS_LA_ITEM_4_DESC," +
                                "             TS_LA_ITEM_4," +
                                "             TS_LA_ITEM_5_DESC," +
                                "             TS_LA_ITEM_5," +
                                "             TS_LA_TOTAL," +
                                "             TS_BALANCE," +
                                "             TAXABLE_AMOUNT," +
                                "             REPORTED_TAXABLE_AMOUNT, " +
                                "             US_16_EA," +
                                "             US_16_TE,";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "       US_16_IA,";
                strSQL = strSQL + "           US_16_AGGREGATE," +
                                "             INCOME_CHARGEABLE," +
                                "             AIS_ITEM_1_DESC," +
                                "             AIS_ITEM_1," +
                                "             AIS_ITEM_2_DESC," +
                                "             AIS_ITEM_2," +
                                "             AIS_ITEM_3_DESC," +
                                "             AIS_ITEM_3," +
                                "             AIS_ITEM_4_DESC," +
                                "             AIS_ITEM_4," +
                                "             AIS_Total," +
                                "             GROSS_TOTAL_INCOME," +
                                "             CVIA_SEC80C_ITEM_1_DESC," +
                                "             CVIA_SEC80C_ITEM_1," +
                                "             CVIA_SEC80C_ITEM_2_DESC," +
                                "             CVIA_SEC80C_ITEM_2," +
                                "             CVIA_SEC80C_ITEM_3_DESC," +
                                "             CVIA_SEC80C_ITEM_3," +
                                "             CVIA_SEC80C_ITEM_4_DESC," +
                                "             CVIA_SEC80C_ITEM_4," +
                                "             CVIA_SEC80C_ITEM_5_DESC," +
                                "             CVIA_SEC80C_ITEM_5," +
                                "             CVIA_SEC80C_ITEM_6_DESC," +
                                "             CVIA_SEC80C_ITEM_6," +
                                "             CVIA_SEC80C_GROSS_TOTAL," +
                                "             CVIA_SEC80C_DED_TOTAL," +
                                "             CVIA_SEC80CCC_GROSS_AMOUNT," +
                                "             CVIA_SEC80CCC_DED_AMOUNT," +
                                "             CVIA_SEC80CCD_GROSS_AMOUNT," +
                                "             CVIA_SEC80CCD_DED_AMOUNT," +
                                "             CVIA_SEC80CCE_TOTAL_DED_AMOUNT," +
                                "             CVIA_SEC80CCG_GROSS_AMOUNT," +
                                "             CVIA_SEC80CCG_DED_AMOUNT, " +
                                "             CVIA_OTH_ITEM_1_DESC," +
                                "             CVIA_OTH_ITEM_1_GROSS_AMOUNT," +
                                "             CVIA_OTH_ITEM_1_QUAL_AMOUNT," +
                                "             CVIA_OTH_ITEM_1_DED_AMOUNT," +
                                "             CVIA_OTH_ITEM_2_DESC," +
                                "             CVIA_OTH_ITEM_2_GROSS_AMOUNT," +
                                "             CVIA_OTH_ITEM_2_QUAL_AMOUNT," +
                                "             CVIA_OTH_ITEM_2_DED_AMOUNT," +
                                "             CVIA_OTH_ITEM_3_DESC," +
                                "             CVIA_OTH_ITEM_3_GROSS_AMOUNT," +
                                "             CVIA_OTH_ITEM_3_QUAL_AMOUNT," +
                                "             CVIA_OTH_ITEM_3_DED_AMOUNT," +
                                "             CVIA_OTH_ITEM_4_DESC," +
                                "             CVIA_OTH_ITEM_4_GROSS_AMOUNT," +
                                "             CVIA_OTH_ITEM_4_QUAL_AMOUNT," +
                                "             CVIA_OTH_ITEM_4_DED_AMOUNT," +
                                "             CVIA_OTH_ITEM_5_DESC," +
                                "             CVIA_OTH_ITEM_5_GROSS_AMOUNT," +
                                "             CVIA_OTH_ITEM_5_QUAL_AMOUNT," +
                                "             CVIA_OTH_ITEM_5_DED_AMOUNT," +
                                "             CVIA_OTH_DED_TOTAL," +
                                "             CVIA_DED_TOTAL," +
                                "             TOTAL_INCOME," +
                                "             TAX_TOTAL_INCOME," +
                                "             SCHG_TOTAL_INCOME," +
                                "             ECESS_TOTAL_INCOME," +
                                "             TAX_PAYABLE_AGGREGATE," +
                                "             US_89_LESS," +
                                "             TAX_PAYABLE," +
                                "             TOTAL_TDS_DEDUCTED," +
                                "             SHORTFALL_TAX," +
                                "             SL_NO," +
                    //## Anik 2013/04/25
                    //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2010_11ID ||
                    //    TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2011_12ID)
                    //{
                    //    strSQL = strSQL + " CVIA_SEC80CCF_GROSS_AMOUNT," +
                    //          "             CVIA_SEC80CCF_DED_AMOUNT, ";
                    //}
                    //else //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2012_13ID)
                    //{
                    //    strSQL = strSQL + " CVIA_SEC80CCG_GROSS_AMOUNT," +
                    //           "            CVIA_SEC80CCG_DED_AMOUNT, ";
                    //}
                    ////Added by Dhrub on 11/09/2013 [Addition of new fields in Salary details as per the changes announced]
                         "              TOTAL_TAX_DEDUCTED_AMOUNT, " +
                         "              PREVIOUS_TAX_DEDUCTED_TOTAL, " +
                         "              TAX_DEDUCTED_HIGHER_RATE, " +
                    //"              ROUND_OFF_TAXABLE_AMOUNT, " +
                    //"              TOTAL_INCOME_ROUND_OFF, " +
                         "              SUPER_ANN_YN, " +
                         "              SUPER_ANN_NAME, " +
                         "              SUPER_ANN_FROM_DATE, " +
                         "              SUPER_ANN_TO_DATE, " +
                         "              SUPER_ANN_AMOUNT, " +
                         "              SUPER_ANN_RATE, " +
                         "              SUPER_ANN_TAX, " +
                         "              SUPER_ANN_INCOME, " +
                         "              RENT_EXCEEDING_YN, " +
                         "              LANDLORD_PAN_COUNT, " +
                         "              LANDLORD_1_PAN, " +
                         "              LANDLORD_1_NAME, " +
                         "              LANDLORD_2_PAN, " +
                         "              LANDLORD_2_NAME, " +
                         "              LANDLORD_3_PAN, " +
                         "              LANDLORD_3_NAME, " +
                         "              LANDLORD_4_PAN, " +
                         "              LANDLORD_4_NAME, " +
                         "              INTEREST_PAID_TO_LENDER, " +
                         "              LENDER_PAN_COUNT, " +
                         "              LENDER_1_PAN, " +
                         "              LENDER_1_NAME, " +
                         "              LENDER_2_PAN, " +
                         "              LENDER_2_NAME, " +
                         "              LENDER_3_PAN, " +
                         "              LENDER_3_NAME, " +
                         "              LENDER_4_PAN, " +
                         "              LENDER_4_NAME," +
                         "              PARTY_PAN," +
                         "              PARTY_NAME) " +
                    "   SELECT    " + lngBasicInfoID + "," +
                    "             EMPLOYEE_ID,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME,FROM_DATE, 103)," +
                        "             CONVERT(DATETIME,TO_DATE, 103),";
                //strSQL = strSQL + " CONVERT(DATETIME,SUBSTRING(FROM_DATE,4,2) + '/' + SUBSTRING(FROM_DATE,1,2) + '/' + SUBSTRING(FROM_DATE,7,4), 102)," +
                //    "             CONVERT(DATETIME,SUBSTRING(TO_DATE,4,2) + '/' + SUBSTRING(TO_DATE,1,2) + '/' + SUBSTRING(TO_DATE,7,4), 102),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "FROM_DATE," +
                    "             TO_DATE,";
                strSQL = strSQL + " 1," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_1, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_2, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGS_SEC17_3, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_SALARY, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_DESC1," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_US10_AMT1, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_DESC2," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_US10_AMT2, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_DESC3," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_US10_AMT3, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_DESC4," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_US10_AMT4, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LESS_US10_DESC5," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_US10_AMT5, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strLESS_ALLOWANCE_TOTAL, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strBALANCE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strCURR_SALARY, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strPREV_SALARY, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_DEDUCTION_16ii, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_DEDUCTION_16iii, J_SQLColFormat.Case_End) + ",";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
                    strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strGROSS_DEDUCTION_16ia, J_SQLColFormat.Case_End) + ",";
                strSQL = strSQL + "   " + cmnService.J_SQLDBFormat(strAGGREGATE_AMOUNT, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_CHARGEABLE_SALARIES, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_DESC1," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_OTHER_SALARY_AMT1, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_DESC2," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_OTHER_SALARY_AMT2, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_DESC3," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_OTHER_SALARY_AMT3, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INCOME_OTHER_SALARY_DESC4," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_OTHER_SALARY_AMT4, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_INCOME_OTHER_SALARY, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC1," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT1, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC2," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT2, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC3," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT3, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC4," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT4, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC5," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT5, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".DED_CHVIA_80C_DESC6," +
                        "             " + cmnService.J_SQLDBFormat(strDED_CHVIA_80C_AMT6, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_80C, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strDEDUCTIBLE_TOTAL_80C, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_AMT_SEC80CCC, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strDED_AMT_SEC80CCC, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_AMT_SEC80CCD, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strDED_AMT_SEC80CCD, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOT_DED_AMT_80CCE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_AMT_SEC80CCG, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strDED_AMT_SEC80CCG, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DESC1," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_GROSS1, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_QUAL1, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_DED1, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DESC2," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_GROSS2, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_QUAL2, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_DED2, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DESC3," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_GROSS3, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_QUAL3, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_DED3, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DESC4," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_GROSS4, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_QUAL4, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_DED4, J_SQLColFormat.Case_End) + "," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".OTHER_SEC_DESC5," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_GROSS5, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_QUAL5, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strOTHER_SEC_DED5, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOT_DED_AMT_OTHER_SEC, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_DED_CHVIA, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TAXABLE_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_TAX_ON_TOTAL_INCOME, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSURCHARGE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strEDUCATION_CESS, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_PAYABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strINCOME_TAX_RELIEF, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strNET_TAX_PAYABLE, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTOTAL_TDS_DEDUCTED, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strSHORTFALL_EXCESS, J_SQLColFormat.Case_End) + "," +
                        "             SD_F16_ID," +
                        "             " + cmnService.J_SQLDBFormat(strCURR_EMPLOYER_TDS, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strPREV_EMPLOYER_TDS, J_SQLColFormat.Case_End) + "," +
                        "             " + cmnService.J_SQLDBFormat(strTAX_DEDUCTED_HIGHER_RATE, J_SQLColFormat.Case_End) + "," +
                    //"             " + cmnService.J_SQLDBFormat(strTDS_SUPERANN, J_SQLColFormat.Case_End) + "," +
                        "             LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".CONTRIBUTIONS_SUPERANN_YN,1) AS SUPERANN_YN," +
                        "             " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_SUPERANN,";
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = strSQL + " CONVERT(DATETIME,SUPERANN_FROM_DATE, 103)," +
                        "             CONVERT(DATETIME,SUPERANN_TO_DATE, 103),";
                //strSQL = strSQL + " CONVERT(DATETIME,SUBSTRING(SUPERANN_FROM_DATE,4,2) + '/' + SUBSTRING(SUPERANN_FROM_DATE,1,2) + '/' + SUBSTRING(SUPERANN_FROM_DATE,7,4), 102)," +
                //    "             CONVERT(DATETIME,SUBSTRING(SUPERANN_TO_DATE,4,2) + '/' + SUBSTRING(SUPERANN_TO_DATE,1,2) + '/' + SUBSTRING(SUPERANN_TO_DATE,7,4), 102),";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = strSQL + "SUPERANN_FROM_DATE," +
                        "             SUPERANN_TO_DATE,";
                strSQL = strSQL + " " + cmnService.J_SQLDBFormat(strAMT_REPAID, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strRATE_DED, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strAMT_TAX, J_SQLColFormat.Case_End) + ", " +
                        "           " + cmnService.J_SQLDBFormat(strGROSS_TOTAL_INC, J_SQLColFormat.Case_End) + ", " +
                        "           LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".RENT_PAYMENT_YN, 1) AS RENT_YN," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LANDLORD_PAN_COUNT," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LANDLORD1," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LANDLORD1," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LANDLORD2," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LANDLORD2," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LANDLORD3," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LANDLORD3," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LANDLORD4," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LANDLORD4," +
                        "           LEFT(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".INT_PAID_YN, 1) AS INT_YN," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".LENDER_PAN_COUNT," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LENDER1," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LENDER1," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LENDER2," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LENDER2," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LENDER3," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LENDER3," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".PAN_LENDER4," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".NAME_LENDER4," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                        "           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME " +
                        "   FROM    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                        "   ORDER BY  SERIAL_NO_ORDER";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                // UPDATE TAX_PAYABLE_AGGREGATE
                //strSQL = "UPDATE TRN_SALARY_DETAILS " +
                //    "     SET    TAX_PAYABLE_AGGREGATE = (TAX_TOTAL_INCOME + SCHG_TOTAL_INCOME + ECESS_TOTAL_INCOME)";
                ////-----------------------------------------------------------
                //if (dmlService.J_ExecSql(strSQL) == false)
                //{
                //    return false;
                //}
                #region UPDATE
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    RENT_EXCEEDING_YN = 'N' 
                           WHERE  RENT_EXCEEDING_YN = ''  
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    INTEREST_PAID_TO_LENDER = 'N' 
                           WHERE  INTEREST_PAID_TO_LENDER = ''  
                           AND    BASIC_INFO_ID           = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_YN  = 'N' 
                           WHERE  SUPER_ANN_YN  = ''  
                           AND    BASIC_INFO_ID = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                #endregion
                //-- 2017/05/05
                //-- UPDATE SUPER_ANN_FROM_DATE & SUPER_ANN_TO_DATE
                #region UPDATE SUPER_ANN_FROM_DATE & SUPERANN_TO_DATE
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_FROM_DATE = NULL                                   
                           WHERE  SUPER_ANN_FROM_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @"   
                           AND    BASIC_INFO_ID       = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS 
                           SET    SUPER_ANN_TO_DATE = NULL                                   
                           WHERE  SUPER_ANN_TO_DATE = " + cmnService.J_DateOperator() + "1900/01/01" + cmnService.J_DateOperator() + @"   
                           AND    BASIC_INFO_ID     = " + lngBasicInfoID;
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }
                #endregion
                //
                return true;
            }
            catch (Exception e)
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
                //-- 2016/11/23
                if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                {
                    #region F24QForm16SalaryDetails

                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                             "LEFT JOIN (SELECT EMPLOYEE_PAN " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN = EMPLOYEE_MASTER.EMPLOYEE_PAN " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    dmlService.J_ExecSql(strSQL);

                    //FOR PANNOTAVBL
                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + " " +
                             "LEFT JOIN (SELECT EMPLOYEE_NAME " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "           AND    EMPLOYEE_PAN = 'PANNOTAVBL') AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_NAME = EMPLOYEE_MASTER.EMPLOYEE_NAME " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_F16_SALARY_DETAILS + ".EMPLOYEE_PAN = 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_NAME IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    dmlService.J_ExecSql(strSQL);
                    #endregion
                } 
                else if (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                {
                    #region F24QSalaryDetails
                    //
                    #region COMMENT
                    //COMMENTED BY SHREY KEJRIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID)" +
                    //       "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //       "            EMPLOYEE_PAN," +
                    //       "            " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    //       "     FROM   TEMP_SALARY_DETAILS" +
                    //       "     WHERE  EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                    //       "     AND    EMPLOYEE_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE" +
                    //       "                                 WHERE COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";
                    //                
                    #endregion
                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                             "LEFT JOIN (SELECT EMPLOYEE_PAN " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN = EMPLOYEE_MASTER.EMPLOYEE_PAN " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    
                    dmlService.J_ExecSql(strSQL);

                    #region COMMENT

                    //FOR PANNOTAVBL
                    //COMMENTED BY SHREY KEJRIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID)" +
                    //        "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //        "            EMPLOYEE_PAN," +
                    //        "            " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    //        "     FROM   TEMP_SALARY_DETAILS" +
                    //        "     WHERE  EMPLOYEE_PAN = 'PANNOTAVBL'" +
                    //        "     AND    EMPLOYEE_NAME NOT IN (SELECT EMPLOYEE_NAME FROM MST_EMPLOYEE " +
                    //        "                                  WHERE EMPLOYEE_PAN = 'PANNOTAVBL' " +
                    //        "                                  AND   COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";
                    ////                
                    #endregion

                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " " +
                             "LEFT JOIN (SELECT EMPLOYEE_NAME " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             "           WHERE  COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "           AND    EMPLOYEE_PAN = 'PANNOTAVBL') AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME = EMPLOYEE_MASTER.EMPLOYEE_NAME " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN = 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_NAME IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    
                    dmlService.J_ExecSql(strSQL);
                    #endregion
                }
                else if (cmbFormNo.Text != T_FormNo.F24Q)
                {
                    #region != T_FormNo.F24Q
                    #region COMMENT

                    //dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET DEDUCTEE_CODE = '02' WHERE DEDUCTEE_CODE = '2'");
                    //dmlService.J_ExecSql("UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " SET DEDUCTEE_CODE = '01' WHERE DEDUCTEE_CODE = '1'");
                    //FOR VALID PAN
                    
                    //COMMENTED BY SHREY KEJRIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE)" +
                    //        "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //        "            EMPLOYEE_PAN," +
                    //        "            DEDUCTEE_CODE" +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    //        "     WHERE  EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                    //        "     AND    EMPLOYEE_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_DEDUCTEE WHERE  EMPLOYEE_PAN <> 'PANNOTAVBL')";
                    #endregion
                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_CODE " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                             "LEFT JOIN MST_DEDUCTEE " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN = MST_DEDUCTEE.EMPLOYEE_PAN " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                             "AND    MST_DEDUCTEE.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    //                
                    dmlService.J_ExecSql(strSQL);
                    //-----------------------
                    #region COMMENT

                    //FOR PANNOTAVBL
                    
                    //COMMENTED BY SHREY KERJIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE)" +
                    //        "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //        "            EMPLOYEE_PAN, " +
                    //        "            DEDUCTEE_CODE" +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    //        "     WHERE  EMPLOYEE_PAN = 'PANNOTAVBL'" +
                    //        "     AND    EMPLOYEE_NAME NOT IN (SELECT EMPLOYEE_NAME FROM MST_DEDUCTEE WHERE EMPLOYEE_PAN = 'PANNOTAVBL')";


                    //ADDED BY SHREY KEJRIWAL ON 27/12/2012
                    //strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE) " +
                    //         "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME," +
                    //         "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN," +
                    //         "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_CODE " +
                    //         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                    //         "LEFT JOIN MST_DEDUCTEE " +
                    //         "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME = MST_DEDUCTEE.EMPLOYEE_NAME " +
                    //         "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN = 'PANNOTAVBL' " +
                    //         "AND    MST_DEDUCTEE.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER
                    #endregion
                    //-- MODIFIED BY ANIK.G @ 2013/05/06
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_CODE " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                             "LEFT JOIN (SELECT TOP 1 EMPLOYEE_NAME, EMPLOYEE_PAN, DEDUCTEE_CODE FROM MST_DEDUCTEE WHERE EMPLOYEE_PAN = 'PANNOTAVBL' ORDER BY INACTIVE_FLAG) AS MAST_DEDUCTEE " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME = MAST_DEDUCTEE.EMPLOYEE_NAME " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN     = 'PANNOTAVBL' " +
                             "AND    MAST_DEDUCTEE.EMPLOYEE_PAN IS NULL"; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER

                    //                
                    dmlService.J_ExecSql(strSQL);
                    #endregion
                }
                else
                {
                    #region T_FormNo.F24Q
                    //
                    #region COMMENT

                    //COMMENTED BY SHREY KEJRIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID)" +
                    //        "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //        "            EMPLOYEE_PAN," +
                    //        "            " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    //        "     WHERE  EMPLOYEE_PAN <> 'PANNOTAVBL'" +
                    //        "     AND    EMPLOYEE_PAN NOT IN (SELECT EMPLOYEE_PAN FROM MST_EMPLOYEE" +
                    //        "                                 WHERE COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";
                    #endregion               
                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                             "LEFT JOIN (SELECT TOP 1 EMPLOYEE_PAN " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             //"           WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ") AS EMPLOYEE_MASTER " +
                             "           WHERE  COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "           ORDER BY INACTIVE_FLAG) AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN = EMPLOYEE_MASTER.EMPLOYEE_PAN " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN <> 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_PAN IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER

                    dmlService.J_ExecSql(strSQL);

                    #region COMMENT

                    //FOR PANNOTAVBL
                    //COMMENTED BY SHREY KEJRIWAL ON 27/04/2012
                    //strSQL = " INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_MASTER + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID)" +
                    //        "     SELECT DISTINCT EMPLOYEE_NAME," +
                    //        "            EMPLOYEE_PAN," +
                    //        "            " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    //        "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "" +
                    //        "     WHERE  EMPLOYEE_PAN = 'PANNOTAVBL'" +
                    //        "     AND    EMPLOYEE_NAME NOT IN (SELECT EMPLOYEE_NAME FROM MST_EMPLOYEE " +
                    //        "                                  WHERE EMPLOYEE_PAN = 'PANNOTAVBL' " +
                    //        "                                  AND   COMPANY_ID =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + ")";
                    ////                

                    #endregion
                    //ADDED BY SHREY KEJRIWAL ON 27/04/2012
                    strSQL = "INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "(EMPLOYEE_NAME, EMPLOYEE_PAN, COMPANY_ID) " +
                             "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME," +
                             "       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN," +
                             "       " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                             "LEFT JOIN (SELECT TOP 1 EMPLOYEE_NAME " +      //CREATING TABLE WITH EMPLOYEES OF SELECTED COMPANY ONLY
                             "           FROM   MST_EMPLOYEE " +
                             "           WHERE  COMPANY_ID   =" + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                             "           AND    EMPLOYEE_PAN = 'PANNOTAVBL' " +
                             "           ORDER BY INACTIVE_FLAG) AS EMPLOYEE_MASTER " +
                             "       ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_NAME = EMPLOYEE_MASTER.EMPLOYEE_NAME " +
                             "WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".EMPLOYEE_PAN = 'PANNOTAVBL' " +
                             "AND    EMPLOYEE_MASTER.EMPLOYEE_NAME IS NULL "; //ONLY FILTERING THOSE DEDUCTEES I.E NOT FOUND IN MASTER

                    dmlService.J_ExecSql(strSQL);
                    //
                    #endregion
                }
                strSQL = " SELECT COUNT(*) FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + "";
                lngNewDeducteesCreated = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                return true;
            }
            catch
            {
                return false;
            }
        }

        
        #endregion

        #region CREATE NEW DEDUCTEE MASTER WORKSHEET
        private bool CREATE_NEW_DEDUCTEE_WORKSHEET(string ExcelFilePath)
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

                wsnewdeductee.Name = strNewDeducteeWorkSheetName;

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
                if (cmbFormNo.Text == T_FormNo.F24Q || cmbFormNo.Text == T_FormNo.F24QSalaryDetails || cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                    strDedEmp = "Employee";
                else
                    strDedEmp = "Deductee";
                //wsnew.get_Range("B:B", m).ColumnWidth = 40;
                wsnew.get_Range("B2", "D2").MergeCells = true;
                wsnew.get_Range("B2", m).Value2 = "List of " + strDedEmp + "s not found in Master";
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
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " ";
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

        #region InsertCompanyBasicInfo
        private bool InsertCompanyBasicInfo(long CompanyId, long BasicInfoId)
        {
            #region VARIABLE DECLARATION
            IDataReader drdShowRecord = null;
            string strTAN = "";
            string strPAN = "";
            string strCompanyName = "";
            string strBranchDiv = "";
            long lngCategoryID = 0;
            long lngMinistryID = 0;
            string strMinistryOther = "";
            string strAddress1 = "";
            string strAddress2 = "";
            string strAddress3 = "";
            string strAddress4 = "";
            string strAddress5 = "";
            long lngStateID = 0;
            string strPIN = "";
            string strSTD = "";
            string strPhone = "";
            string strEmail = "";
            string strPersonName = "";
            string strDesignation = "";
            string strFatherName = "";
            string strPAddress1 = "";
            string strPAddress2 = "";
            string strPAddress3 = "";
            string strPAddress4 = "";
            string strPAddress5 = "";
            long lngPStateID = 0;
            string strPPIN = "";
            string strPSTD = "";
            string strPPhone = "";
            string strPEmail = "";
            string strPMobile = "";
            string strPAOCode = "";
            string strPAORegNo = "";
            string strDDOCode = "";
            string strDDORegNo = "";
            long lngDStateID = 0;
            string strAIN = "";
            string strTanRegNo = "";
            string strAltSTD = "";
            string strAltPhone = "";
            string strAltEmail = "";
            string strAltPSTD = "";
            string strAltPPhone = "";
            string strAltPEmail = "";
            string strAltPPan = "";
            
            #endregion
            //--
            strSQL = "SELECT  MST_COMPANY.COMPANY_ID            AS COMPANY_ID," +
               "             MST_COMPANY.COMPANY_NAME          AS COMPANY_NAME," +
               "             MST_COMPANY.TAN_NO                AS TAN_NO," +
               "             MST_COMPANY.PAN_NO                AS PAN_NO," +
               "             MST_COMPANY.BRANCH_DIV            AS BRANCH_DIV," +
               "             MST_COMPANY.D_CATEGORY_ID         AS D_CATEGORY_ID," +
               "             MST_CATEGORY.CATEGORY_DESCRIPTION AS CATEGORY_DESCRIPTION," +
               "             MST_CATEGORY.CATEGORY_CODE        AS CATEGORY_CODE," +
               "             MST_COMPANY.FILE_PREFIX           AS FILE_PREFIX," +
               "             MST_COMPANY.ADDRESS1              AS ADDRESS1," +
               "             MST_COMPANY.ADDRESS2              AS ADDRESS2," +
               "             MST_COMPANY.ADDRESS3              AS ADDRESS3," +
               "             MST_COMPANY.ADDRESS4              AS ADDRESS4," +
               "             MST_COMPANY.ADDRESS5              AS ADDRESS5," +
               "             MST_COMPANY.STATE_ID              AS STATE_ID," +
               "             MST_STATE.STATE_NAME              AS STATE_NAME," +
               "             MST_COMPANY.PIN_CODE              AS PIN_CODE," +
               "             MST_COMPANY.STD                   AS STD," +
               "             MST_COMPANY.PHONE                 AS PHONE," +
               "             MST_COMPANY.EMAIL                 AS EMAIL," +
               "             MST_COMPANY.PERSON_NAME           AS PERSON_NAME," +
               "             MST_COMPANY.DESIGNATION           AS DESIGNATION," +
               "             MST_COMPANY.FATHER_NAME           AS FATHER_NAME," +
               "             MST_COMPANY.P_ADDRESS1            AS P_ADDRESS1," +
               "             MST_COMPANY.P_ADDRESS2            AS P_ADDRESS2," +
               "             MST_COMPANY.P_ADDRESS3            AS P_ADDRESS3," +
               "             MST_COMPANY.P_ADDRESS4            AS P_ADDRESS4," +
               "             MST_COMPANY.P_ADDRESS5            AS P_ADDRESS5," +
               "             MST_COMPANY.P_STATE_ID            AS P_STATE_ID," +
               "             RP_STATE.STATE_NAME               AS RP_STATE_NAME," +
               "             MST_COMPANY.P_PIN_CODE            AS P_PIN_CODE," +
               "             MST_COMPANY.P_PHONE               AS P_PHONE," +
               "             MST_COMPANY.P_STD                 AS P_STD," +
               "             MST_COMPANY.P_EMAIL               AS P_EMAIL," +
               "             MST_COMPANY.P_MOBILE              AS P_MOBILE," +
               "             MST_COMPANY.PAO_CODE              AS PAO_CODE," +
               "             MST_COMPANY.PAO_REG_NO            AS PAO_REG_NO," +
               "             MST_COMPANY.DDO_CODE              AS DDO_CODE," +
               "             MST_COMPANY.DDO_REG_NO            AS DDO_REG_NO," +
               "             MST_COMPANY.D_STATE_ID            AS D_STATE_ID," +
               "             D_STATE.STATE_NAME                AS D_STATE_NAME," +
               "             MST_COMPANY.MINISTRY_ID           AS MINISTRY_ID," +
               "             MST_MINISTRY.MINISTRY_NAME        AS MINISTRY_NAME," +
               "             MST_COMPANY.MINISTRY_OTHER        AS MINISTRY_OTHER," +
               "             MST_COMPANY.CIT_TDS_ADDRESS       AS CIT_TDS_ADDRESS," +
               "             MST_COMPANY.CIT_TDS_CITY          AS CIT_TDS_CITY," +
               "             MST_COMPANY.CIT_TDS_PINCODE       AS CIT_TDS_PINCODE," +
               "             MST_COMPANY.AIN_NO                AS AIN_NO," +
               "             MST_COMPANY.TAN_REG_NO            AS TAN_REG_NO," +
               "             MST_COMPANY.ALT_STD               AS ALT_STD," +
               "             MST_COMPANY.ALT_PHONE             AS ALT_PHONE," +
               "             MST_COMPANY.ALT_EMAIL             AS ALT_EMAIL," +
               "             MST_COMPANY.P_ALT_STD             AS P_ALT_STD," +
               "             MST_COMPANY.P_ALT_PHONE           AS P_ALT_PHONE," +
               "             MST_COMPANY.P_ALT_EMAIL           AS P_ALT_EMAIL," +
               "             MST_COMPANY.P_PAN                 AS P_PAN " +
               "     FROM    (((((MST_COMPANY INNER JOIN MST_CATEGORY " +
               "             ON MST_COMPANY.D_CATEGORY_ID     = MST_CATEGORY.CATEGORY_ID) " +
               "     INNER JOIN MST_STATE " +
               "             ON MST_COMPANY.STATE_ID    = MST_STATE.STATE_ID) " +
               "     INNER JOIN MST_STATE AS RP_STATE " +
               "             ON MST_COMPANY.P_STATE_ID = RP_STATE.STATE_ID) " +
               "     LEFT JOIN  MST_STATE AS D_STATE " +
               "             ON MST_COMPANY.D_STATE_ID        = D_STATE.STATE_ID) " +
               "     LEFT JOIN  MST_MINISTRY " +
               "             ON MST_COMPANY.MINISTRY_ID       = MST_MINISTRY.MINISTRY_ID) " +
               "     WHERE   MST_COMPANY.COMPANY_ID      = " + CompanyId + " ";

            drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
            if (drdShowRecord == null)
            {
                return false;
            }
            while (drdShowRecord.Read())
            {
                  strTAN = Convert.ToString(drdShowRecord["TAN_NO"]);
                  strPAN = Convert.ToString(drdShowRecord["PAN_NO"]);
                  strCompanyName = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                  strBranchDiv = Convert.ToString(drdShowRecord["BRANCH_DIV"]);
                  lngCategoryID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["D_CATEGORY_ID"]));
                  lngMinistryID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["MINISTRY_ID"]));
                  strMinistryOther = Convert.ToString(drdShowRecord["MINISTRY_OTHER"]);
                  strAddress1 = Convert.ToString(drdShowRecord["ADDRESS1"]);
                  strAddress2 = Convert.ToString(drdShowRecord["ADDRESS2"]); 
                  strAddress3 = Convert.ToString(drdShowRecord["ADDRESS3"]); 
                  strAddress4 = Convert.ToString(drdShowRecord["ADDRESS4"]); 
                  strAddress5 = Convert.ToString(drdShowRecord["ADDRESS5"]); 
                  lngStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["STATE_ID"]));
                  strPIN = Convert.ToString(drdShowRecord["PIN_CODE"]);
                  strSTD = Convert.ToString(drdShowRecord["STD"]);
                  strPhone = Convert.ToString(drdShowRecord["PHONE"]); 
                  strEmail = Convert.ToString(drdShowRecord["EMAIL"]); 
                  strPersonName = Convert.ToString(drdShowRecord["PERSON_NAME"]);
                  strDesignation = Convert.ToString(drdShowRecord["DESIGNATION"]);
                  strFatherName = Convert.ToString(drdShowRecord["FATHER_NAME"]);
                  strPAddress1 = Convert.ToString(drdShowRecord["P_ADDRESS1"]); 
                  strPAddress2 = Convert.ToString(drdShowRecord["P_ADDRESS2"]); 
                  strPAddress3 = Convert.ToString(drdShowRecord["P_ADDRESS3"]); 
                  strPAddress4 = Convert.ToString(drdShowRecord["P_ADDRESS4"]); 
                  strPAddress5 = Convert.ToString(drdShowRecord["P_ADDRESS5"]); 
                  lngPStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["P_STATE_ID"])); 
                  strPPIN = Convert.ToString(drdShowRecord["P_PIN_CODE"]); 
                  strPSTD = Convert.ToString(drdShowRecord["P_STD"]); 
                  strPPhone = Convert.ToString(drdShowRecord["P_PHONE"]); 
                  strPEmail = Convert.ToString(drdShowRecord["P_EMAIL"]); 
                  strPMobile = Convert.ToString(drdShowRecord["P_MOBILE"]); 
                  strPAOCode = Convert.ToString(drdShowRecord["PAO_CODE"]); 
                  strPAORegNo = Convert.ToString(drdShowRecord["PAO_REG_NO"]);
                  strDDOCode = Convert.ToString(drdShowRecord["DDO_CODE"]); 
                  strDDORegNo = Convert.ToString(drdShowRecord["DDO_REG_NO"]); 
                  lngDStateID = cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["D_STATE_ID"]));
                  //
                  strAIN = Convert.ToString(drdShowRecord["AIN_NO"]);
                  strTanRegNo = Convert.ToString(drdShowRecord["TAN_REG_NO"]);
                  strAltSTD = Convert.ToString(drdShowRecord["ALT_STD"]);
                  strAltPhone = Convert.ToString(drdShowRecord["ALT_PHONE"]);
                  strAltEmail = Convert.ToString(drdShowRecord["ALT_EMAIL"]);
                  strAltPSTD = Convert.ToString(drdShowRecord["P_ALT_STD"]);
                  strAltPPhone = Convert.ToString(drdShowRecord["P_ALT_PHONE"]);
                  strAltPEmail = Convert.ToString(drdShowRecord["P_ALT_EMAIL"]);
                  strAltPPan = Convert.ToString(drdShowRecord["P_PAN"]);

            }
            drdShowRecord.Close();
            drdShowRecord.Dispose();
            //--
            strSQL = "INSERT INTO TRN_COMPANY_INFO (" +
                     "            BASIC_INFO_ID," +
                     "            COMPANY_ID," +
                     "            GROUP_ID," +
                     "            TAN_NO," +
                     "            PAN_NO," +
                     "            COMPANY_NAME," +
                     "            BRANCH_DIV," +
                     "            D_CATEGORY_ID," +
                     "            MINISTRY_ID," +
                     "            MINISTRY_OTHER," +
                     "            ADDRESS1," +
                     "            ADDRESS2," +
                     "            ADDRESS3," +
                     "            ADDRESS4," +
                     "            ADDRESS5," +
                     "            STATE_ID," +
                     "            PIN_CODE," +
                     "            STD," +
                     "            PHONE," +
                     "            EMAIL," +
                     "            PERSON_NAME," +
                     "            DESIGNATION," +
                     "            FATHER_NAME," +
                     "            P_ADDRESS1," +
                     "            P_ADDRESS2," +
                     "            P_ADDRESS3," +
                     "            P_ADDRESS4," +
                     "            P_ADDRESS5," +
                     "            P_STATE_ID," +
                     "            P_PIN_CODE," +
                     "            P_STD," +
                     "            P_PHONE," +
                     "            P_EMAIL," +
                     "            P_MOBILE," +
                     "            PAO_CODE," +
                     "            PAO_REG_NO," +
                     "            DDO_CODE," +
                     "            DDO_REG_NO," +
                     "            D_STATE_ID," +
                     "            AIN_NO," +
                     "            TAN_REG_NO," +
                     "            ALT_STD," +
                     "            ALT_PHONE," +
                     "            ALT_EMAIL," +
                     "            P_ALT_STD," +
                     "            P_ALT_PHONE," +
                     "            P_ALT_EMAIL," +
                     "            P_PAN) " +
                     "     VALUES( " + BasicInfoId + "," +
                     "             " + CompanyId + "," +
                     "             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                     "            '" + cmnService.J_ReplaceQuote(strTAN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strCompanyName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strBranchDiv) + "'," +
                     "             " + lngCategoryID + "," +
                     "             " + lngMinistryID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strMinistryOther) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress1) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress2) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress3) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress4) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAddress5) + "'," +
                     "             " + lngStateID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strPIN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPersonName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDesignation) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strFatherName) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress1) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress2) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress3) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress4) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAddress5) + "'," +
                     "             " + lngPStateID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strPPIN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPMobile) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAOCode) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strPAORegNo) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDDOCode) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strDDORegNo) + "'," +
                     "             " + lngDStateID + "," +
                     "            '" + cmnService.J_ReplaceQuote(strAIN) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strTanRegNo) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltPSTD) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltPPhone) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltPEmail) + "'," +
                     "            '" + cmnService.J_ReplaceQuote(strAltPPan) + "')";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }           
            //--
            return true;
            //-----------------------------------------------------------
        }
        #endregion

        #region CheckExcelStructure
        private bool CheckExcelStructure(string FormNo, string ExcelFilePath)
        {
            try
            {
                #region F24Q
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, null, con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Employee Serial No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "PAN of the Employee", con) == false)
                {
                    con.Close();
                    con.Dispose();
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
                //if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Category of the Employee", con) == false)
                //{
                //    con.Close();
                //    con.Dispose();
                //    return false;
                //}
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Employee Reference No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }

                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Amount Paid/Credited", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Payment/Credit Date (dd/mm/yyyy)", con) == false)
                {
                    con.Close();
                    con.Dispose(); 
                    return false;                        
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Deduction Date (dd/mm/yyyy)", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "Section Code", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (TdsMan.T_IsExcelDatabaseObjectExist(strWorkingSheetName, "TDS Paid upto Date", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                #endregion

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
//                        FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "
//                        GROUP BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO
//                        HAVING (((Count(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".[DEDUCTEE_SERIAL_NO]))>1))";

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
//                             "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
//                             "INNER JOIN (SELECT CHALLAN_SERIAL_NO " +
//                             "            FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
//                             "            GROUP BY CHALLAN_SERIAL_NO, DEDUCTEE_SERIAL_NO " +
//                             "            HAVING COUNT(DEDUCTEE_SERIAL_NO) > 1) AS TEMP " +
//                             "        ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO =  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO " +  
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

//                        strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
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
                         "        FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " AS TEMP " +
                         "        WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".CHALLAN_SERIAL_NO = TEMP.CHALLAN_SERIAL_NO " +
                         "        AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_DETAILS_ID > " +
                         "                TEMP.DEDUCTEE_DETAILS_ID) + 1 AS DEDUCTEE_SERIAL_NO " +
                         "FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + "";

                dmlService.J_ExecSql(strSQL);



                //UPDATING THE SERIAL NOS IN THE DEDUCTEE DETAILS IMPORT TABLE
                strSQL = "UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
                         "INNER JOIN TEMP_UPDATE_SERIAL " +
                         "       ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_DETAILS_ID = TEMP_UPDATE_SERIAL.DEDUCTEE_DETAILS_ID " +
                         "SET " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + ".DEDUCTEE_SERIAL_NO = TEMP_UPDATE_SERIAL.DEDUCTEE_SERIAL_NO";

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
        
        #region GenerateSerialNoSD
        private void GenerateSerialNoSD(long BasicInfoID)
        {
            DataSet dsetDeducteeDetails = new DataSet();
            //-- Stores info in Datarow into an array
            //Object[] cells = myDataRow.ItemArray;
            //--
            strSQL = @"SELECT SALARY_DETAILS_ID FROM TRN_SALARY_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID + " ORDER BY SALARY_DETAILS_ID";
            dsetDeducteeDetails = dmlService.J_ExecSqlReturnDataSet(strSQL);
            //
            long lngSerialNo = 1;
            //
            foreach (DataRow myDataRow in dsetDeducteeDetails.Tables[0].Rows)
            {
                //-- Stores info in Datarow into an array
                //Object[] cells1 = myDataRow1.ItemArray;
                //--
                strSQL = @"UPDATE TRN_SALARY_DETAILS SET SL_NO = " + lngSerialNo + " WHERE SALARY_DETAILS_ID = " + myDataRow[0];
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
            if (cmbFormNo.Text == T_FormNo.F24QForm16SalaryDetails)
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=c_kK97ReKuc&t=");
            else //if  (cmbFormNo.Text == T_FormNo.F24QSalaryDetails)
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?time_continue=5&v=DGXmnvmZmp0");
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
        //                            EMPLOYEE_NAME, 
        //                            CATEGORY, 
        //                            EMPLOYEE_PAN, 
        //                            TAXABLE_INCOME, 
        //                            TDS_PAID, 
        //                            TDS_TO_BE_PAID, 
        //                            MONTH_YEAR, 
        //                            TRACKING_ID 
        //                   FROM     " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " ORDER BY DEDUCTEE_DETAILS_ID ";
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
        //                lngPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
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
        //                double dblNetTaxToBePaid = dblCumulativeTax - cmnService.J_ReturnDoubleValue(dr["TDS_PAID"]);
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
        //                strSQL = @"INSERT INTO TRN_CALC_SD_LOG_HEADER (
        //                                  EMPLOYEE_NAME,
        //                                  EMPLOYEE_PAN,
        //                                  MONTH_ID,
        //                                  MONTH_DESC,
        //                                  ASST_ID,
        //                                  CATEGORY,
        //                                  TAXABLE_INCOME,
        //                                  TAX_TOTAL_INCOME,
        //                                  NET_TAX_PAYABLE_AMOUNT,
        //                                  MONTHLY_TDS_AMOUNT,
        //                                  TOTAL_TDS_PAID_AMOUNT,
        //                                  TOTAL_TDS_CALC,
        //                                  CREATE_SETUP_ID,
        //                                  CREATE_DATE_TIME) 
        //                      VALUES (
        //                                  '" + dr["EMPLOYEE_NAME"].ToString() + @"',
        //                                  '" + dr["EMPLOYEE_PAN"].ToString() + @"',
        //                                  " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @",
        //                                  '" + cmbMonth.Text + @"',
        //                                  " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
        //                                  '" + dr["CATEGORY"].ToString() + @"',
        //                                  " + Convert.ToDouble(dr["TAXABLE_INCOME"].ToString()) + @",
        //                                  " + Convert.ToDouble(dblCalcTax) + @",
        //                                  " + Convert.ToDouble(dblCalculatedTax) + @",
        //                                  " + Convert.ToDouble(dblMonthlyTDS) + @",
        //                                  " + Convert.ToDouble(dr["TDS_PAID"]) + @",
        //                                  " + Convert.ToDouble(dblTaxToBeDeducted) + @",
        //                                  " + Convert.ToDouble(dblTaxToBeDeducted) + @",
        //                                  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(J_ReturnServerDate()) + cmnService.J_DateOperator() + @")";
        //                dmlService.J_ExecSql(strSQL);
        //                //
        //                lngTrackingID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_CALC_SD_LOG_HEADER", "CALC_SD_LOG_HEADER_ID");
        //                //--
        //                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + @" 
        //                        SET     TDS                 = " + dblTaxToBeDeducted + @",
        //                                TOTAL_TAX_DEDUCTED  = " + dblTaxToBeDeducted + @",
        //                                TOTAL_TAX_DEPOSITED = " + dblTaxToBeDeducted + @",
        //                                TDS_TO_BE_PAID      = " + dblTaxToBeDeducted + @",
        //                                MONTH_YEAR          ='" + cmbMonth.Text + " - " + cmbFinancialYear.Text + @"',
        //                                TRACKING_ID         = " + lngTrackingID + @" 
        //                        WHERE DEDUCTEE_DETAILS_ID  = " + dr["DEDUCTEE_DETAILS_ID"].ToString();
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
                    "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MONTHLY_DATA_TDS_CALC + " " +
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

        #region cmbMonthBatchNo_SelectedIndexChanged
        private void cmbMonthBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    cmbMonthSerialNo.Visible = false;
                    lblMonthBatchNo.Visible = false;
                    pbxMonthBatchNo.Visible = false;
                    return;
                }
                //
                if (cmbCompany.SelectedIndex <= 0)
                {
                    cmbMonthSerialNo.Visible = false;
                    lblMonthBatchNo.Visible = false;
                    pbxMonthBatchNo.Visible = false;
                    return;
                }
                //
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    cmbMonthSerialNo.Visible = false;
                    lblMonthBatchNo.Visible = false;
                    pbxMonthBatchNo.Visible = false;
                    return;
                }
                //--
                intMONTH_ID = 0;
                intMONTH_ID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MONTH_ID FROM MST_MONTH WHERE MONTH_DESC ='" + cmbMonth.Text + "'") ));
                //--
                long lngMonthBatchNo = 0;
                if (cmbMonthSerialNo.Text == strDefaultMonthBatchNoTextCombo)
                    lngMonthBatchNo = 0;
                else
                    lngMonthBatchNo = cmnService.J_ReturnInt64Value(cmbMonthSerialNo.Text.Trim());
                //
                strSQL = @"SELECT COUNT(*) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                           WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                           AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                           AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                           AND    MONTH_BATCH_NO = " + cmnService.J_ReturnInt64Value(lngMonthBatchNo) + @"
                           AND    DELETE_DATE_TIME IS NULL";
                if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) > 0)
                {
                    //
                    cmbMonthSerialNo.Visible = true;
                    lblMonthBatchNo.Visible = true;
                    pbxMonthBatchNo.Visible = true;
                    //
                    string[,] strMONTH_BATCH_NO = {{"MONTH_BATCH_NO > 0" , "F", cmnService.J_SQLDBFormat("MONTH_BATCH_NO", J_SQLColFormat.ConvertToString), "F"},
                                      {"MONTH_BATCH_NO = 0" , "F", "''", "F"}};
                    //
                    strSQL = " SELECT SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID," +
                        //" " + cmnService.J_SQLDBFormat("MONTH_BATCH_NO", J_SQLColFormat.ConvertToString) + " " +
                              " " + cmnService.J_SQLDBFormat(strMONTH_BATCH_NO, J_SQLColFormat.Case_End) + @" " +
                        "      FROM   TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER " +
                        "      WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                               AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                               AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @" 
                               AND    DELETE_DATE_TIME IS NULL
                               ORDER BY SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID";
                               //AND    MONTH_BATCH_NO > 0";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbMonthSerialNo, strDefaultMonthBatchNoTextCombo) == false) return;
                }
                else
                {
                    cmbMonthSerialNo.Visible = false;
                    lblMonthBatchNo.Visible = false;
                    pbxMonthBatchNo.Visible = false;
                }

            }
            catch (Exception err)
            {

            }
        }

        #endregion

        #region J_ReturnServerDateTimeMMDDYYYYHHMMSS
        public string J_ReturnServerDateTimeMMDDYYYYHHMMSS()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),120)";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "SELECT FORMAT(NOW(),'MM/dd/yyyy HH:MM:SS')";
            else
                strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
            //--
            return cmnService.J_NullToText(dmlService.J_ExecSqlReturnScalar(strSQL));
        }
        #endregion
    }
}