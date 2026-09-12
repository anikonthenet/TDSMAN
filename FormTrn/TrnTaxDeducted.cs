#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

//~~~~ User Namespaces ~~~~
//using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnTaxDeducted : Form
    {
        #region Constructor
        public TrnTaxDeducted(string EmployeeName, string EmployeePAN, string Category, string Total, string TotalTaxableAmount, string FinancialYear)
        {
            strEmployeeName = EmployeeName;
            strEmployeePAN = EmployeePAN;
            strCategory = Category;
            strTotal = Total;
            strTotalTaxableAmount = TotalTaxableAmount;
            strFinYear = FinancialYear;

            InitializeComponent();
        }
        #endregion

        #region Private Variables and Class Objects
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //-----------------------------------------------------------------------
        long lngSearchId;					//For Storing the Id
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        string strTempMode;

        int intCountRecords = 0;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;

        string strEmployeeName = "";
        string strEmployeePAN = "";
        string strCategory = "";
        string strTotal = "0.00";
        string strTotalTaxableAmount = "0.00";
        string strFinYear = "";

        #endregion 

        #region User Defined Events

        #region TrnTaxDeducted_Load
        private void TrnTaxDeducted_Load(object sender, EventArgs e)
        {
            GC.Collect();
            
            lblEmployeeName.Text = strEmployeeName;
            lblPAN.Text = strEmployeePAN;
            lblCategory.Text = strCategory;
            lblTotal.Text = strTotal;
            lblTaxPayable.Text = string.Format("{0:0.00}", Convert.ToDouble(strTotalTaxableAmount));
            lblFinYear.Text = strFinYear;
            //Populating Grid
            PopulateGrid();

        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            this.Dispose();
            this.Close();
        }
        #endregion


        #endregion

        #region User Defined Functions

        #region PopulateGrid
        public void PopulateGrid()
        {
            string[,] strMatrixViewDeductee = {{"Qtr", "40", "", "", "", "", ""},
                                               {"Challan Sl.", "60", "", "", "", "", ""},
                                               {"Deposit Date", "90", "d", "", "", "", ""},
                                               {"Challan No.", "80", "S", "", "", "", ""},
                                               {"Deductee Sl.", "60", "", "", "", "", ""},
                                               {"Payment Date", "90", "d", "", "", "", ""},
                                               {"Payment Amount", "70", "0.00", "R", "", "", ""},
                                               {"Tax Deducted", "70", "0.00", "R", "", "", ""},
                                               {"Tax Deposited", "70", "0.00", "R", "", "", ""}};

//            // get employee id
//            strSQL = @"SELECT EMPLOYEE_ID
//                       FROM   MST_EMPLOYEE
//                       WHERE  EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(strEmployeePAN) + @"' 
//                       AND    EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(strEmployeeName) + @"' 
//                       AND    COMPANY_ID    = " + TDSMAN.Classes.TDSMAN.T_pCompanyId;
//            long lngEMPLOYEE_ID = cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL));

            string[,] strCaseMatrix = {{"TRN_CHALLAN.BOOK_ENTRY = 0", "F", "TRN_CHALLAN.CHALLAN_NO", "F"},
                                       {"TRN_CHALLAN.BOOK_ENTRY = 1", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"}};

            strSQL = @"SELECT TRN_BASIC_INFO.QTR                        AS QTR,
                              TRN_CHALLAN.SL_NO                         AS CHALLAN_SL_NO,
                              TRN_CHALLAN.DEPOSIT_DATE                  AS DEPOSIT_DATE,
                              " + cmnService.J_SQLDBFormat(strCaseMatrix, J_SQLColFormat.Case_End) + @" AS CHALLAN_NO,
                              TRN_DEDUCTEE_DETAILS.SL_NO                AS DEDUCTEE_SL_NO,
                              TRN_DEDUCTEE_DETAILS.PAYMENT_DATE         AS PAYMENT_DATE,
                              TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT       AS PAYMENT_AMOUNT,
                              TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT         AS TOTAL_AMOUNT,
                              TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS TAX_DEPOSITED_AMOUNT
                       FROM   TRN_BASIC_INFO,
                              TRN_CHALLAN, 
                              TRN_DEDUCTEE_DETAILS,
                              MST_EMPLOYEE
                       WHERE  TRN_BASIC_INFO.BASIC_INFO_ID  = TRN_CHALLAN.BASIC_INFO_ID
                       AND    TRN_CHALLAN.CHALLAN_ID        = TRN_DEDUCTEE_DETAILS.CHALLAN_ID
                       AND    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID
                       AND    TRN_BASIC_INFO.COMPANY_ID     = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + @" 
                       AND    TRN_BASIC_INFO.ASST_ID        = " + TDSMAN.Classes.TDSMAN.T_pFinancialYearId + @"
                       AND    TRN_BASIC_INFO.FORM_NO        = '" + TDSMAN.Classes.TDSMAN.T_pFormNo + @"' ";

//            if (lngEMPLOYEE_ID == 0)
//            {
//                strSQL += @"
//                       AND    MST_EMPLOYEE.EMPLOYEE_NAME    = '" + cmnService.J_ReplaceQuote(strEmployeeName) + @"'
//                       AND    MST_EMPLOYEE.EMPLOYEE_PAN     = '" + cmnService.J_ReplaceQuote(strEmployeePAN) + @"' ";
//            }
//            else if (lngEMPLOYEE_ID > 0)
//            {
//                strSQL += @"
//                       AND    MST_EMPLOYEE.EMPLOYEE_ID      = " + lngEMPLOYEE_ID + @" ";
            //            }
            //strSQL += @" AND MST_EMPLOYEE.EMPLOYEE_ID = " + lngEMPLOYEE_ID + @" ";//-- Commented By Abhishek Dey On 22/05/2018 --

            strSQL += @" AND MST_EMPLOYEE.EMPLOYEE_PAN  = '" + cmnService.J_ReplaceQuote(strEmployeePAN) + @"' ";  //-- Added By Abhishek Dey On 22/05/2018 --

            strSQL += @"ORDER BY TRN_BASIC_INFO.QTR,
                              TRN_CHALLAN.SL_NO, 
                              TRN_DEDUCTEE_DETAILS.SL_NO";
            
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewDeductee);
            lblNoOfRecords.Text = dsetGridClone.Tables[0].Rows.Count.ToString();
            dgcViewRecords.ClearSelection();
        }
        #endregion

        

        #endregion

    }
}