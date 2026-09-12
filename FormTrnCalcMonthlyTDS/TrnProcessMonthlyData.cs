#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnProcessMonthlyData
Version			: 1.0
Start Date		: 03-05-2020
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
    public partial class TrnProcessMonthlyData : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        //public TrnProcessMonthlyData()
        //{
        //    InitializeComponent();
        //}
        #endregion

        #region Constructor
        public TrnProcessMonthlyData(string FAYear, string Company, string Qtr, string Month, string MonthBatchNo)
        {
            strFAYear = FAYear;
            strCompany = Company;
            strQtr = Qtr;
            strMonth = Month;
            strMonthBatchNo = MonthBatchNo;
            //
            InitializeComponent();
        }
        #endregion
        //
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
        OleDbDataAdapter myCommand;
        OleDbConnection con;
        //
        bool blnOpenTabPage = false;

        //Added by Dhrub Mukherjee On 07/01/2014
        //---------------------------------------
        //---------------------------------------
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        ToolTip tllTip = new ToolTip();
        ToolTip ToolTip1 = new ToolTip();

        double dblTotalExcelRecords = 0; //-- 2015/07/31
        int intMonthBatchNo = 0;
        int j = 0;
        long lngHeaderId = 0; int intPaidMonth = 0, intCOMPANY_ID = 0,  intASST_ID = 0;

        string strFAYear = "", strCompany = "", strQtr="", strMonth="", strMonthBatchNo = "";
        int intIT_CALCULATION_FLAG = 0;
        bool blHideThisProcess = false;
        #endregion


        #region User Defined Events

        #region TrnProcessMonthlyData_Load
        private void TrnProcessMonthlyData_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //
            //Added by Indrajit on 23-02-2013
            //tmrLoginRefresh.Interval = (int)TDSMAN.Classes.TDSMAN.T_pLockInterval * 60000;
            //tmrLoginRefresh.Start();
            //-----
            BtnSave.BackColor = Color.Gray;
            BtnSave.Enabled = false;
            //
            chkHideThisProcess.Enabled = false;
            //-----------
            lblTitle.Text = "Process Monthly Data";
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
            string[] strQtrCombo = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtrCombo, ref cmbQuarter, 1);
            //-- FORM NO
            //string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ, T_FormNo.F24QSalaryDetails };
            string[] strQuarterWiseDeducteeReportForm = { T_FormNo.F24Q };
            dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbFormNo, 1);
            cmbFormNo.Enabled = false;
            //--
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']'" +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            if (strFAYear != "" && strCompany != "" && strQtr != "" && strMonth != "")
            {
                cmbFinancialYear.Text = strFAYear;
                cmbCompany.Text = strCompany;
                cmbQuarter.Text = strQtr;
                cmbMonth.Text = strMonth;
                cmbMonthSerialNo.Text = strMonthBatchNo;
                //
                btnLoadEmployees_Click(sender, e);
            }

        }
        #endregion

        #region cmbQuarter_SelectedIndexChanged
        private void cmbQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        #region cmbMonthBatchNo_SelectedIndexChanged
        private void cmbMonthBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    return;
                }
                //
                if (cmbCompany.SelectedIndex <= 0)
                {
                    return;
                }
                //
                if (cmbFinancialYear.SelectedIndex <= 0)
                {
                    return;
                }
                //
                if (cmbMonth.SelectedIndex <= 0)
                {
                    return;
                }
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
                            AND    MONTH_BATCH_NO > 0";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbMonthSerialNo) == false) return;                

            }
            catch (Exception err)
            {

            }
        }

        #endregion
        //

        #region btnLoadEmployees_Click
        private void btnLoadEmployees_Click(object sender, EventArgs e)
        {
            long lngMonthSerialNo = 0;
            try
            {
                if (ValidateFields() == false) return;
                //--
                if (cmbMonthSerialNo.Text == "")
                    lngMonthSerialNo = 0;
                else
                    lngMonthSerialNo = cmnService.J_ReturnInt32Value(cmbMonthSerialNo.Text);
                //--
                strSQL = @"SELECT SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                           WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                           AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                           AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                           AND    MONTH_BATCH_NO = " + lngMonthSerialNo + " ";
                lngHeaderId = cmnService.J_ReturnInt64Value(dmlService.J_ExecSqlReturnScalar(strSQL));
                if (LoadEmployeeGrid(lngHeaderId) == true)
                {
                    btnLoadEmployees.BackColor = Color.Gray;
                    grpMain.Enabled = false;
                    //
                    //BtnSave.Enabled = true;
                    //BtnSave.BackColor = Color.Lavender;
                    //
                    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DELETE_DATE_TIME FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId)) != "")
                    {                        
                        BtnSave.Enabled = false;
                        BtnSave.BackColor = Color.Gray;
                    }
                    else
                    {
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                    }
                    //
                    BtnCancel.Text = "Cancel";
                }
                else
                {
                    btnLoadEmployees.BackColor = Color.Lavender;
                    grpMain.Enabled = true;
                    //
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.Gray;
                    //
                    BtnCancel.Text = "E&xit";
                }
            }
            catch (Exception ERR)
            {

            }

        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (grpMain.Enabled == false)
            {
                if (LoadEmployeeGrid(0) == false)
                {

                }
                btnLoadEmployees.BackColor = Color.Lavender;
                grpMain.Enabled = true;
                //
                BtnSave.BackColor = Color.Gray;
                BtnSave.Enabled = false;
                BtnCancel.Text = "E&xit";
                //
                pnlProcess.Visible = false;
            }
            else
            {
                //--
                GC.Collect();
                //
                dmlService.Dispose();
                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {            
            //
            //int j = 0;
            try
            {
                if (cmnService.J_UserMessage("Do you want to Start Processing??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Cursor.Current = Cursors.WaitCursor;
                    grpButton.Enabled = false;
                    pnlProcess.Visible = true;
                    //
                    if (cmbMonthSerialNo.Text == "")
                        intMonthBatchNo = 0;
                    else
                        intMonthBatchNo = cmnService.J_ReturnInt32Value(cmbMonthSerialNo.Text);
                    //
                    intPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                    intCOMPANY_ID = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                    intASST_ID = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    //
                    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL SET TOT_TAXABLE_INCOME = 0, MONTHLY_TDS = 0, PROCESS_DATE_TIME = null WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER SET NO_OF_RECORDS_VALID_PROCESSED = 0 WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
                    dmlService.J_ExecSql(strSQL);
                    //
                    LoadEmployeeGrid(lngHeaderId);
                    //
                    bgwProcessMonthlyTDS.RunWorkerAsync();
                    ////-- GET THE GRID IN A LOOP
                    ////
                    //for (int i = j; i <= dgvEmployeeList.RowCount - 1; i++)
                    //{
                    //    dblCalculatedECess = 0; dblCalculatedSurcharge = 0; dblCalculatedTaxCredit = 0; dblTaxonTotalIncomeB4TaxCredit = 0;
                    //    //--
                    //    lngDETAIL_ID = cmnService.J_ReturnInt64Value(Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridDETAIL_ID].Value));
                    //    //-- GET PAN
                    //    strEMPLOYEE_PAN = Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridEMPLOYEE_PAN].Value);
                    //    dblTDS_PAID_TILL_DATE = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridTDS_PAID_TILL_DATE].Value));
                    //    //-- GET CATEGORY, TAXABLE TOTAL INCOME FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 
                    //    strSQL = @"SELECT   CATEGORY
                    //               FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                    //               WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                    //               AND      COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                    //               AND      ASST_ID      = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    //    strCATEGORY = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //    //
                    //    strSQL = @"SELECT   TOT_TAXABLE_INCOME
                    //               FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                    //               WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                    //               AND      COMPANY_ID   = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                    //               AND      ASST_ID      = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    //    dblTOT_TAXABLE_INCOME = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //    //--
                    //    //-- CALC TAX
                    //    dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                    //                                                       strCATEGORY,
                    //                                                       dblTOT_TAXABLE_INCOME,
                    //                                                       out dblCalculatedECess,
                    //                                                       out dblCalculatedSurcharge,
                    //                                                       out dblCalculatedTaxCredit,
                    //                                                       out dblTaxonTotalIncomeB4TaxCredit);
                    //    dblCalculatedTax = Math.Round(dblCalculatedTax, 0);
                    //    //
                    //    if (dblCalculatedTaxCredit > dblTaxonTotalIncomeB4TaxCredit)
                    //        dblRebate = dblTaxonTotalIncomeB4TaxCredit;
                    //    //
                    //    dblNetTaxPayable = (dblCalculatedTax - dblRebate) + dblCalculatedECess + dblCalculatedSurcharge;
                    //    //--long lngPaidMonth = 0;
                    //    //lngPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                    //    dblTaxPerMonth = cmnService.J_ReturnDoubleValue(dblNetTaxPayable) / 12;
                    //    //double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblMonthlyTDS.Text);
                    //    //
                    //    dblCumulativeTax = 0;
                    //    for (int l = 0; l < intPaidMonth; l++)
                    //    {
                    //        dblCumulativeTax = dblCumulativeTax + dblTaxPerMonth;
                    //    }
                    //    //
                    //    double dblNetTaxToBePaid = dblCumulativeTax - dblTDS_PAID_TILL_DATE + 0.5;
                    //    //--
                    //    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL 
                    //               SET    TOT_TAXABLE_INCOME = " + dblTOT_TAXABLE_INCOME + @",
                    //                      MONTHLY_TDS        = " + dblNetTaxToBePaid + @",
                    //                      PROCESS_DATE_TIME  = " + cmnService.J_DateOperator() + J_ReturnServerDateTimeYYYYMMDDHHMMSS() + cmnService.J_DateOperator() + @"
                    //               WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL_ID = " + lngDETAIL_ID;
                    //    dmlService.J_ExecSql(strSQL);
                    //    //
                    //    j = j + 1;
                    //}
                    //////--
                    //long lngMonthSerialNo = 0;
                    //if (cmbMonthSerialNo.Text == "")
                    //    lngMonthSerialNo = 0;
                    //else
                    //    lngMonthSerialNo = cmnService.J_ReturnInt32Value(cmbMonthSerialNo.Text);
                    ////--
                    //strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                    //            SET    NO_OF_RECORDS_VALID_PROCESSED   = " + j + @",
                    //                   NO_OF_RECORDS_INVALID_PROCESSED = " + (dgvEmployeeList.RowCount - j) + @",
                    //                   PROCESSING_COUNTER              = " + dgvEmployeeList.RowCount + @"
                    //            WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
                    //dmlService.J_ExecSql(strSQL);
                    ////--
                    ////strSQL = @"SELECT SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                    ////           WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
                    ////           AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
                    ////           AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
                    ////           AND    MONTH_BATCH_NO = " + lngMonthSerialNo + " ";
                    //LoadEmployeeGrid(lngHeaderId);
                    //--
                    #region CALCULATE TAX
                    /*
                    // * double dblCalculatedECess = 0, dblCalculatedSurcharge = 0, dblCalculatedTaxCredit = 0, dblTaxonTotalIncomeB4TaxCredit = 0;
                    //int intAsstId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
                    //string strCategory = "";
                    //if (cmbEmployeeCategory.SelectedIndex <= 0)
                    //    strCategory = "G";
                    //else
                    //    strCategory = cmbEmployeeCategory.Text.Substring(0, 1);
                    ////
                    //double dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId, strCategory, Convert.ToDouble(txtTaxableIncome.Text), out dblCalculatedECess, out dblCalculatedSurcharge, out dblCalculatedTaxCredit, out dblTaxonTotalIncomeB4TaxCredit);
                    //dblCalculatedTax = Math.Round(dblCalculatedTax, 0);
                    //lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", dblCalculatedTax);
                    //if (dblCalculatedTax >= 0)
                    //{
                    //    //lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                    //    lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                    //    lblTaxCalculatedECess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedECess)));
                    //    lblCalculatedSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedSurcharge)));
                    //    //
                    //    lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", dblTaxonTotalIncomeB4TaxCredit);
                    //}
                    //else
                    //{
                    //    lblTaxDeductingRebateCalculated.Text = "0.00";
                    //    lblTaxCalculatedECess.Text = "0.00";
                    //    lblCalculatedSurcharge.Text = "0.00";
                    //}
                    //-- 
                    if (Convert.ToInt32(TDSMAN.Classes.TDSMAN.T_pFinancialYearId) >= T_FinancialYearID.F2018_19ID)
                    {
                        lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                        lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)));
                    }
                    else
                    {
                        if (dblCalculatedTaxCredit <= dblTaxonTotalIncomeB4TaxCredit)
                            lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)) - Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                    }
                    //
                    //
                    if (dblCalculatedTaxCredit > dblTaxonTotalIncomeB4TaxCredit)
                    {
                        //txtRebate.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)));
                        lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)));
                    }
                    //
                    lblTaxDeductingRebate.Text = string.Format("{0:0.00}", Convert.ToDouble(txtTaxTotalIncome.Text) - Convert.ToDouble(txtRebate.Text));
                    lblGrossTaxPayable.Text = string.Format("{0:0.00}", Convert.ToDouble(lblTaxDeductingRebate.Text) + Convert.ToDouble(txtSurcharge.Text) + Convert.ToDouble(txtEducationCess.Text));
                    lblTaxPayable.Text = string.Format("{0:0.00}", Convert.ToDouble(lblGrossTaxPayable.Text) - Convert.ToDouble(txtReliefUS89.Text));
                    lblMonthlyTDS.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDouble(Convert.ToDouble(lblTaxPayable.Text) / 12) + 0.5, 0, MidpointRounding.AwayFromZero));
                    //@@@@@@@@@@@@
                    //-- CALC TDS OF THE MONTH 
                    long lngPaidMonth = 0;
                    lngPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                    //double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblTaxPayable.Text) / 12; 
                    double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblMonthlyTDS.Text);
                    //
                    double dblCumulativeTax = 0;
                    for (int i = 0; i < lngPaidMonth; i++)
                    {
                        dblCumulativeTax = dblCumulativeTax + dblTaxPerMonth;
                    }
                    //
                    double dblNetTaxToBePaid = dblCumulativeTax - cmnService.J_ReturnDoubleValue(txtTDSPaidTillDate.Text) + 0.5;
                    if (dblNetTaxToBePaid < 0)
                        txtTDSToBeDeducted.Text = "0.00";
                    else
                        txtTDSToBeDeducted.Text = string.Format("{0:0.00}", Decimal.Ceiling(Convert.ToInt64(dblNetTaxToBePaid)));
                    */
                    #endregion
                    //
                    //Cursor.Current = Cursors.Default;
                }
             }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                pnlProcess.Visible = false;
                grpButton.Enabled = true;
            }
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
                    //{
                    if (cmbMonth.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Month - Cannot be Blank as you have opted for TDS Calculation");
                        cmbMonth.Select();
                        return false;
                    }
                    //}
                    //--
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

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //-- EXPORT TO EXCEL/CSV
            TrnProcessMonthlyDataExport objProcessMonthlyDataExport = new TrnProcessMonthlyDataExport(cmbFinancialYear.Text.Trim(), cmbCompany.Text.Trim(), cmbQuarter.Text.Trim(), cmbFormNo.Text.Trim(), cmbMonth.Text.Trim(), lngHeaderId);
            //objProcessMonthlyDataExport.Show();
            objProcessMonthlyDataExport.MdiParent = TrnProcessMonthlyData.ActiveForm;
            objProcessMonthlyDataExport.Show();

        }
        #endregion

        #region chkHideThisProcess_CheckedChanged
        private void chkHideThisProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (blHideThisProcess == true)
                return;
            //--
            if (chkHideThisProcess.Checked == true)
            {
                if (dgvEmployeeList.RowCount <= 0)
                {
                    chkHideThisProcess.Checked = false;
                    cmnService.J_UserMessage("Load records to the grid to Hide the Process...");
                    btnLoadEmployees.Select();
                }
                //--
                if (dgvEmployeeList.RowCount > 0)
                {
                    if (cmnService.J_UserMessage("Are you sure to hide this process ??") == DialogResult.No)
                        return;
                    //--
                    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER SET DELETE_DATE_TIME = " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + " WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
                    dmlService.J_ExecSql(strSQL);
                    //
                    LoadEmployeeGrid(lngHeaderId);
                    //
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.Gray;
                }
            }
            else
            { //--
                if (dgvEmployeeList.RowCount > 0)
                {
                    if (cmnService.J_UserMessage("Are you sure to un-hide this process ??") == DialogResult.No)
                        return;
                    //--
                    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER SET DELETE_DATE_TIME = NULL WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
                    dmlService.J_ExecSql(strSQL);
                    //
                    LoadEmployeeGrid(lngHeaderId);
                    //
                    BtnSave.Enabled = true;
                    BtnSave.BackColor = Color.Lavender;
                }
            }
        }
        #endregion


        #region chkHideThisProcess_MouseMove
        private void chkHideThisProcess_MouseMove(object sender, MouseEventArgs e)
        {
            //if (chkHideThisProcess.Enabled == false)
            //    tllTip.SetToolTip(chkHideThisProcess, "Hiding this process is not possible as some or all data has been processed...");
            //else
            //    tllTip.SetToolTip(chkHideThisProcess, "");
        }

        private void pnlHideProcess_MouseMove(object sender, MouseEventArgs e)
        {
            if (chkHideThisProcess.Enabled == false)
                tllTip.SetToolTip(pnlHideProcess, "Hiding this process is not possible as some or all data has been processed...");
        }
        #endregion

        #endregion

        #region LoadDeducteeGrid
        private bool LoadEmployeeGrid(long HeaderID)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //-----------------------------------------------------------
                string[,] strMatrixViewMonthlyData = {{"SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL_ID", "0", "", "Right", "", "F", ""},
                                                        {"SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID", "0", "", "Right", "", "F", ""},
                                                        {"Sl No", "40", "", "", "", "", ""},
                                                        {"EMPLOYEE_ID", "0", "", "Right", "", "F", ""},
                                                        {"PAN No.", "85", "S", "", "", "", ""},
                                                        {"Employee Name", "120", "S", "", "", "", ""},
                                                        {"Reference No.", "85", "S", "", "", "", ""},
                                                        {"Payment Amount", "70", "0.00", "R", "", "", ""},
                                                        {"Payment Date", "70", "d", "", "", "", ""},
                                                        {"Deducted Date", "70", "d", "", "", "", ""},
                                                        {"Section", "70", "", "L", "", "", ""},
                                                        {"TDS Paid Till Date", "100", "0.00", "R", "", "", ""},
                                                        {"Total Taxable Income", "100", "0.00", "R", "", "", ""},
                                                        {"Monthly TDS", "100", "0.00", "R", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //            
                strOrderBy = "DETAIL.DETAIL_SL_NO";
                strQuery = "SELECT DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL_ID   AS DETAIL_ID," +
                           "       DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID   AS HEADER_ID," +
                           "       DETAIL.DETAIL_SL_NO                                       AS DETAIL_SL_NO," +
                           "       DETAIL.EMPLOYEE_ID                                        AS EMPLOYEE_ID," +
                           "       DETAIL.EMPLOYEE_PAN                                       AS EMPLOYEE_PAN," +
                           "       DETAIL.EMPLOYEE_NAME                                      AS EMPLOYEE_NAME," +
                           "       DETAIL.EMPLOYEE_REF_NO                                    AS EMPLOYEE_REF_NO," +
                           "       DETAIL.PAYMENT_AMOUNT                                     AS PAYMENT_AMOUNT," +
                           "   " + cmnService.J_SQLDBFormat(" DETAIL.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS PAYMENT_DATE," +
                           "   " + cmnService.J_SQLDBFormat("DETAIL.DEDUCTION_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DEDUCTION_DATE," +
                           "       MST_SECTION.SECTION_NO                                    AS SECTION_NO," +
                           "       DETAIL.TDS_PAID_TILL_DATE                                 AS TDS_PAID_TILL_DATE," +
                           "       DETAIL.TOT_TAXABLE_INCOME                                 AS TOT_TAXABLE_INCOME," +
                           "       DETAIL.MONTHLY_TDS                                        AS MONTHLY_TDS " +
                           "FROM   ((TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL AS DETAIL INNER JOIN TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER AS HEADER " +
                           "       ON DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = HEADER.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID) " +
                           "       INNER JOIN MST_SECTION " +
                           "       ON DETAIL.SECTION_ID             = MST_SECTION.SECTION_ID) " +
                           "WHERE  DETAIL.SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID            = " + HeaderID + " ";
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvEmployeeList, strSQL, strMatrixViewMonthlyData);
                dgvEmployeeList.ClearSelection();
                //-- LOAD SUMMARY FIELDS
                j = 0; //-- grid loop variable
                lblTotalPaymentAmount.Text = "Total Payment Amount - " + string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID  = " + HeaderID))));
                lblTotalTDS.Text = "Total TDS - " + string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(MONTHLY_TDS) FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID  = " + HeaderID))));
                //

                //
                if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT NO_OF_RECORDS_VALID_PROCESSED FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID  = " + HeaderID))) > 0)
                {
                    lblProcessedRecords.Visible = true;
                    lblProcessedRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT NO_OF_RECORDS_VALID_PROCESSED FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID  = " + HeaderID)) + " processed out of " + dgvEmployeeList.RowCount.ToString();
                    //
                    chkHideThisProcess.Enabled = false;
                }
                else
                {
                    lblProcessedRecords.Visible = false;
                    chkHideThisProcess.Enabled = true;
                    //
                    if(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT DELETE_DATE_TIME FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER WHERE SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + HeaderID)) != "")
                    {
                        blHideThisProcess = true;
                        chkHideThisProcess.Checked = true;
                        blHideThisProcess = false;
                        //
                        //BtnSave.Enabled = false;
                        //BtnSave.BackColor = Color.Gray;
                    }
                    else
                    {
                        chkHideThisProcess.Checked = false;
                        //
                        //BtnSave.Enabled = true;
                        //BtnSave.BackColor = Color.Lavender;
                    }
                }
                //--
                this.Cursor = Cursors.Default;
                //--
                if (dgvEmployeeList.RowCount > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Default;
                return false;
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

        #endregion

        #region bgwProcessMonthlyTDS_DoWork
        private void bgwProcessMonthlyTDS_DoWork(object sender, DoWorkEventArgs e)
        {
            int intGridDETAIL_ID = 0, intGridEMPLOYEE_PAN = 4, intGridEMPLOYEE_NAME = 5, intGridEMPLOYEE_REF_NO =6, intGridAMOUNT_PAID = 7, intGridTOT_TAXABLE_INCOME = 12, intGridMONTHLY_TDS = 13, intGridTDS_PAID_TILL_DATE = 11;
            string strEMPLOYEE_PAN = "", strEMPLOYEE_REF_NO = "", strCATEGORY = "", strSECTION_115BAC_FLAG = ""; double dblTOT_TAXABLE_INCOME = 0, dblTDS_PAID_TILL_DATE = 0;
            double dblCalculatedECess = 0, dblCalculatedSurcharge = 0, dblCalculatedTaxCredit = 0, dblTaxonTotalIncomeB4TaxCredit = 0, dblCalculatedTax = 0;
            double dblRebate = 0, dblNetTaxPayable = 0, dblTaxPerMonth = 0, dblCumulativeTax = 0;
            long lngDETAIL_ID = 0;
            try
            {                
                //
                Cursor.Current = Cursors.WaitCursor;
                //-- GET THE GRID IN A LOOP
                //int intPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                //
                for (int i = j; i <= dgvEmployeeList.RowCount - 1; i++)
                {
                    dblCalculatedECess = 0; dblCalculatedSurcharge = 0; dblCalculatedTaxCredit = 0; dblTaxonTotalIncomeB4TaxCredit = 0;
                    //--
                    lngDETAIL_ID = cmnService.J_ReturnInt64Value(Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridDETAIL_ID].Value));
                    //-- GET PAN
                    strEMPLOYEE_PAN = Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridEMPLOYEE_PAN].Value);
                    strEMPLOYEE_REF_NO = Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridEMPLOYEE_REF_NO].Value);
                    dblTDS_PAID_TILL_DATE = cmnService.J_ReturnDoubleValue(Convert.ToString(dgvEmployeeList.Rows[i].Cells[intGridTDS_PAID_TILL_DATE].Value));
                    //-- GET CATEGORY, TAXABLE TOTAL INCOME FROM TRN_SALARY_DETAILS_PROJECTED_FORM16 
                    strSQL = @"SELECT   CATEGORY
                               FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                               WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                               AND      COMPANY_ID   = " + intCOMPANY_ID + @"
                               AND      ASST_ID      = " + intASST_ID;
                    strCATEGORY = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //
                    strSQL = @"SELECT   TOT_TAXABLE_INCOME
                               FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                               WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                               AND      COMPANY_ID   = " + intCOMPANY_ID + @"
                               AND      ASST_ID      = " + intASST_ID;
                    dblTOT_TAXABLE_INCOME = cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    //-- 2020/07/03
                    strSQL = @"SELECT   SECTION_115BAC_FLAG
                               FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                               WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                               AND      COMPANY_ID   = " + intCOMPANY_ID + @"
                               AND      ASST_ID      = " + intASST_ID;
                    strSECTION_115BAC_FLAG = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //--
                    //-- CALC TAX
                    dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intASST_ID,
                                                                       strCATEGORY,
                                                                       dblTOT_TAXABLE_INCOME,
                                                                       strSECTION_115BAC_FLAG,
                                                                       out dblCalculatedECess,
                                                                       out dblCalculatedSurcharge,
                                                                       out dblCalculatedTaxCredit,
                                                                       out dblTaxonTotalIncomeB4TaxCredit);
                    dblCalculatedTax = Math.Round(dblCalculatedTax, 0);
                    //
                    if (dblCalculatedTaxCredit > dblTaxonTotalIncomeB4TaxCredit)
                        dblRebate = dblTaxonTotalIncomeB4TaxCredit;
                    //
                    dblNetTaxPayable = (dblCalculatedTax - dblRebate) + dblCalculatedECess + dblCalculatedSurcharge;
                    //--long lngPaidMonth = 0;
                    //lngPaidMonth = Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex));
                    dblTaxPerMonth = cmnService.J_ReturnDoubleValue(dblNetTaxPayable) / 12;
                    //double dblTaxPerMonth = cmnService.J_ReturnDoubleValue(lblMonthlyTDS.Text);
                    //
                    dblCumulativeTax = 0;
                    for (int l = 0; l < intPaidMonth; l++)
                    {
                        dblCumulativeTax = dblCumulativeTax + dblTaxPerMonth;
                    }
                    //
                    //double dblNetTaxToBePaid = dblCumulativeTax - dblTDS_PAID_TILL_DATE + 0.5;
                    double dblNetTaxToBePaid = dblCumulativeTax - dblTDS_PAID_TILL_DATE;
                    //
                    if (dblNetTaxToBePaid < 0)
                        dblNetTaxToBePaid = 0;
                    else
                        dblNetTaxToBePaid = cmnService.J_ReturnDoubleValue(Decimal.Ceiling(Convert.ToInt64(dblNetTaxToBePaid)));
                    //@@@@@@@@@@
                    dmlService.J_BeginTransaction();
                    //
                    strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL 
                                   SET    TOT_TAXABLE_INCOME = " + dblTOT_TAXABLE_INCOME + @",
                                          MONTHLY_TDS        = " + dblNetTaxToBePaid + @",
                                          PROCESS_DATE_TIME  = " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + @"
                                   WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_DETAIL_ID = " + lngDETAIL_ID;
                    dmlService.J_ExecSql(strSQL);
                    //-- LOG DATA
                    strSQL = @"INSERT INTO LOG_SALARY_DETAILS_MONTHLY_DATA_PROCESSING 
                                                (COMPANY_ID,
                                                 ASST_ID,
                                                 SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID,
                                                 EMPLOYEE_ID,
                                                 EMPLOYEE_PAN,
                                                 EMPLOYEE_NAME, 
                                                 EMPLOYEE_REF_NO,
                                                 CATEGORY,
                                                 IT_CALCULATION_FLAG,
                                                 TOTAL_SALARY,
                                                 SEC10_TOTAL_AMOUNT,
                                                 OTHER_INCOME,
                                                 CVIA_TOTAL_DED_AMOUNT,
                                                 PREV_EMPLOYER_SALARY,
                                                 TOT_TAXABLE_INCOME,
                                                 TAX_TOTAL_INCOME,
                                                 LOG_DATE_TIME)
                                        SELECT " + intCOMPANY_ID + @",
                                               " + intAsstId + @",
                                               " + lngHeaderId + @",
                                                 EMPLOYEE_ID,
                                                 EMPLOYEE_PAN,
                                                 EMPLOYEE_NAME,'" +
                                                 strEMPLOYEE_REF_NO + @"',
                                                 CATEGORY," +
                                                 intIT_CALCULATION_FLAG + @",                                                 
                                                 TOTAL_SALARY,
                                                (SEC10_5_AMOUNT + SEC10_10_AMOUNT + SEC10_10A_AMOUNT + SEC10_10AA_AMOUNT + SEC10_13A_AMOUNT + SEC10_OTHER_AMOUNT),
                                                 OTHER_INCOME,
                                                 CVIA_TOTAL_DED_AMOUNT,
                                                 PREV_EMPLOYER_SALARY,
                                                 TOT_TAXABLE_INCOME," +
                                                 dblNetTaxPayable +@",
                                               " + cmnService.J_DateOperator() + J_ReturnServerDateTimeMMDDYYYYHHMMSS() + cmnService.J_DateOperator() + @"
                                        FROM     TRN_SALARY_DETAILS_PROJECTED_FORM16 
                                        WHERE    EMPLOYEE_PAN ='" + strEMPLOYEE_PAN + @"'
                                        AND      COMPANY_ID   = " + intCOMPANY_ID + @"
                                        AND      ASST_ID      = " + intASST_ID;
                    dmlService.J_ExecSql(strSQL);
                    //
                    dmlService.J_Commit();
                    //@@@@@@@@@@
                    dgvEmployeeList.Rows[i].Cells[intGridTOT_TAXABLE_INCOME].Value = string.Format("{0:0.00}", dblTOT_TAXABLE_INCOME);
                    dgvEmployeeList.Rows[i].Cells[intGridMONTHLY_TDS].Value = string.Format("{0:0.00}", dblNetTaxToBePaid);
                    //
                    j = j + 1;
                    //
                    //
                    if (bgwProcessMonthlyTDS.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    //double dblTotal = (double)byteBuffer.Length;
                    //double dblProgressPercentage = ( j/ dgvEmployeeList.RowCount);
                    //int intProgressPercentage = (int)(dblProgressPercentage * 100);
                    bgwProcessMonthlyTDS.ReportProgress(j);
                    //lblProcessCounter.Visible = true;
                    //lblProcessCounter.Text = i.ToString() + " out of " + dgvEmployeeList.RowCount.ToString();
                }
                //--
                Cursor.Current = Cursors.Default;
                ////
                //if (bgwProcessMonthlyTDS.CancellationPending)
                //{
                //    e.Cancel = true;
                //    return;
                //}
            }
            catch(Exception ERR)
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region btnStopProcessing_Click
        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            bgwProcessMonthlyTDS.CancelAsync();
            //bgwProcessMonthlyTDS.Dispose();
            //-- 
            //if(cmnService.J_UserMessage("Are you sure to stop the Processing??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    bgwProcessMonthlyTDS.RunWorkerAsync();
            //}
            ////--
            //strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
            //                    SET    NO_OF_RECORDS_VALID_PROCESSED   = " + j + @",
            //                           NO_OF_RECORDS_INVALID_PROCESSED = " + (dgvEmployeeList.RowCount - j) + @",
            //                           PROCESSING_COUNTER              = " + dgvEmployeeList.RowCount + @"
            //                    WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
            //dmlService.J_ExecSql(strSQL);
            //LoadEmployeeGrid(lngHeaderId);
            ////
            //pnlProcess.Visible = false;
            //grpButton.Enabled = true;
            ////
            //cmnService.J_UserMessage("Processing Stopped...", MessageBoxIcon.Error);
            ////
            //GC.Collect();
            //this.Cursor = Cursors.Default;
        }
        #endregion
        
        #endregion

        #region bgwProcessMonthlyTDS_ProgressChanged
        private void bgwProcessMonthlyTDS_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProcessCounter.Visible = true;
            //lblProcessCounter.Text = e.ProgressPercentage.ToString() + "%";
            lblProcessCounter.Text = e.ProgressPercentage.ToString() + " out of " + dgvEmployeeList.RowCount.ToString() + " records";
        }
        #endregion

        #region bgwProcessMonthlyTDS_RunWorkerCompleted
        private void bgwProcessMonthlyTDS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //--
            long lngMonthSerialNo = 0;
            if (cmbMonthSerialNo.Text == "")
                lngMonthSerialNo = 0;
            else
                lngMonthSerialNo = cmnService.J_ReturnInt32Value(cmbMonthSerialNo.Text);
            //--
            strSQL = @"UPDATE TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
                                SET    NO_OF_RECORDS_VALID_PROCESSED   = " + j + @",
                                       NO_OF_RECORDS_INVALID_PROCESSED = " + (dgvEmployeeList.RowCount - j) + @",
                                       PROCESSING_COUNTER              = " + dgvEmployeeList.RowCount + @"
                                WHERE  SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID = " + lngHeaderId;
            dmlService.J_ExecSql(strSQL);
            //--
            //strSQL = @"SELECT SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER_ID FROM TRN_SALARY_DETAILS_MONTHLY_DATA_PROCESSING_HEADER 
            //           WHERE  COMPANY_ID     = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + @"
            //           AND    ASST_ID        = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @"
            //           AND    MONTH_ID       = " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @"
            //           AND    MONTH_BATCH_NO = " + lngMonthSerialNo + " ";
            LoadEmployeeGrid(lngHeaderId);
            //
            pnlProcess.Visible = false;
            grpButton.Enabled = true;
            ////
            if (e.Cancelled == true)
                cmnService.J_UserMessage("Processing Stopped...", MessageBoxIcon.Error);
            else
                cmnService.J_UserMessage("Processing Completed...", MessageBoxIcon.Information);
        }
        #endregion
    }
}
