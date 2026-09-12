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
    public partial class TrnShortAndLateDeductionSummary : Form
    {
        #region Constructor
        public TrnShortAndLateDeductionSummary( string FormNo,int CompanyId,int AsstId,string Quarter)
        {
            strFormNo = FormNo;
            intCompanyId = CompanyId;
            intAsstId = AsstId;
            strQuarter = Quarter;
            InitializeComponent();
        }

        public TrnShortAndLateDeductionSummary()
        {
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

        string strFormNo = "";
        int    intCompanyId = 0;
        string strCompanyName = "";
        string strCompanyTAN = "";
        int    intAsstId = 0;
        string strFAYear = "";
        string strQuarter = "";

        #endregion 

        #region User Defined Events
    
        #region TrnShortAndLateDeductionSummary_Load
        private void TrnShortAndLateDeductionSummary_Load(object sender, EventArgs e)
        {
            IDataReader drdGetCompanyDetail = null;
            GC.Collect();

            if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.ShortDeduction)
            {
                this.Text = "Deductee Short Deduction";
                cmbSection.Visible = true;
                label12.Visible = true;
                strSQL = @"SELECT DISTINCT MST_SECTION.SECTION_ID AS SECTION_ID,
                                           MST_SECTION.SECTION_NO AS SECTION_NO 
                           FROM            MST_SECTION,
                                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @" 
                                     WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".SECTION_ID = MST_SECTION.SECTION_ID";
                dmlService.J_PopulateComboBox(strSQL, ref cmbSection);
            }
            else if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.LateDeduction)
            {
                this.Text = "Late Deduction";
                cmbSection.Visible = false;
                label12.Visible = false;
            }
            


            //Get the Assessment Id from Financial Year
            strSQL = "SELECT FA_YEAR FROM MST_ASSESSMENT" +
                   "     WHERE  ASST_ID = " + intAsstId + " ";
            //
            strFAYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

            lblFAYear.Text = strFAYear;
            lblFormNo.Text = strFormNo;
            lblQuarter.Text = strQuarter;

            //Get the Company Details from Company_id 
            strSQL = @"SELECT COMPANY_NAME,
                              TAN_NO
                       FROM   MST_COMPANY
                       WHERE  COMPANY_ID = " + intCompanyId + "";
            //
            drdGetCompanyDetail = dmlService.J_ExecSqlReturnReader(strSQL);
            //
            if (drdGetCompanyDetail == null)
                return;
            //
            while (drdGetCompanyDetail.Read())
            {
                strCompanyName = Convert.ToString(drdGetCompanyDetail["COMPANY_NAME"]);
                lblCompanyName.Text = strCompanyName;
                strCompanyTAN  = Convert.ToString(drdGetCompanyDetail["TAN_NO"]);
                lblTAN.Text = strCompanyTAN;
            }
            drdGetCompanyDetail.Close();
            drdGetCompanyDetail.Dispose();

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

        #region cmbSection_SelectedIndexChanged
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSection.SelectedIndex >= 0)
            {
                PopulateGrid();
            }
        }
        #endregion 

        #region txtDeducteeName_TextChanged
        private void txtDeducteeName_TextChanged(object sender, EventArgs e)
        {
           
            PopulateGrid();
        }
        #endregion

        #region txtDeducteePAN_TextChanged
        private void txtDeducteePAN_TextChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        #endregion 

        #region btnPrintData_Click
        private void btnPrintData_Click(object sender, EventArgs e)
        {
            try
            {
                if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.ShortDeduction)
                {
                    this.Cursor = Cursors.Default;
                    RptDialog rptDialog = new RptDialog();
                    //rptDialog.ShortDeduction(strQuarter,
                    //                         strFAYear,
                    //                         Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)),
                    //                         Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                    //                         Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                    //                         Convert.ToString(strCompanyTAN));
                }
                else if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.LateDeduction)
                {
                    this.Cursor = Cursors.Default;
                    RptDialog rptDialog = new RptDialog();
                    //rptDialog.LatePayment( strQuarter,
                    //                       strFAYear,
                    //                       Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                    //                       Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                    //                       Convert.ToString(strCompanyTAN));
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region PopulateGrid
        public void PopulateGrid()
        {
            if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.ShortDeduction)
            {
                IDataReader drdGetTaxCalculateSummary = null;
                int intAsstId = 0;
                double dblSumOfTaxAmt = 0;

                string[,] strMatrixViewTaxSlab = {{"Challan Sl.",   "40", "", "", "", "", ""},
                                                  {"Deductee Sl",  "45", "", "", "", "", ""},
                                                  {"PAN",          "85", "", "", "", "", ""},
                                                  {"Deductee Name","120", "", "", "", "", ""},
                                                  {"Section",      "45", "", "", "", "", ""},
                                                  {"Payment Date", "70", "d", "", "", "", ""},
                                                  {"Paid Amt",     "80", "0.00", "R", "", "", ""},
                                                  {"TDS Deposited","80", "0.00", "R", "", "", ""},
                                                  {"TDS Rate",     "45", "0.00", "R", "", "", ""},
                                                  {"TDS To Be Deducted","80", "0.00", "R", "", "", ""}
                                                 };

                // Get the details of Grid
                strSQL = @" SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".CHALLAN_SL_NO,
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".DEDUCTEE_SL_NO, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".DEDUCTEE_PAN, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".DEDUCTEE_NAME, 
                                   MST_SECTION.SECTION_NO, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".PAYMENT_DATE, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".PAYMENT_AMOUNT, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".TAX_DEPOSITED_AMOUNT, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".RATE, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".ACTUAL_TAX_AMOUNT
                             FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @",
                                   MST_SECTION
                             WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".SECTION_ID=MST_SECTION.SECTION_ID ";

                if (cmbSection.SelectedIndex > 0 )
                    strSQL = strSQL + " AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".SECTION_ID =" + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex) + " ";
                if (txtDeducteeName.Text != "")
                    strSQL = strSQL + " AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                if (txtDeducteePAN.Text != "")
                    strSQL = strSQL + " AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";

                strOrderBy = @" ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".CHALLAN_SL_NO," + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + ".DEDUCTEE_SL_NO ";

                strSQL = strSQL + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewTaxSlab);
            }
            //--------------------------------------------------
            //LATE DEDUCTION 
            //--------------------------------------------------    
            else if (TDSMAN.Classes.TDSMAN.T_pDeductionType == T_DeductionTypeStructure.LateDeduction)
            {
                IDataReader drdGetTaxCalculateSummary = null;
                int intAsstId = 0;
                double dblSumOfTaxAmt = 0;

                string[,] strMatrixViewTaxSlab = {{"Challan Sl",   "40", "", "", "", "", ""},
                                                  {"Deductee Sl",  "45", "", "", "", "", ""},
                                                  {"PAN",          "85", "", "", "", "", ""},
                                                  {"Deductee Name","80", "", "", "", "", ""},
                                                  {"Paid Amt",     "80", "0.00", "", "", "", ""},                                                  
                                                  //{"TDS Deducted", "185", "0.00", "R", "", "", ""},
                                                  {"TDS Deposited","70", "0.00", "R", "", "", ""},
                                                  {"Deduction Date","70", "d", "", "", "", ""},
                                                  {"Due Date","70", "d", "", "", "", ""},
                                                  {"Deposit Date","70", "d", "", "", "", ""},
                                                  {"Delay in Month","80", "", "R", "", "", ""},
                                                  {"Late Fee","60", "0.00", "R", "", "", ""}
                                                 };

                // Get the details of Grid
                strSQL = @" SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".CHALLAN_SL_NO,
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".DEDUCTEE_SL_NO, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".DEDUCTEE_PAN, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".DEDUCTEE_NAME, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".PAYMENT_AMOUNT, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".TAX_DEPOSITED_AMOUNT, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".DEDUCTED_DATE, 
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".LAST_DATE,
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".CHALLAN_DEPOSIT_DATE,
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".TOTAL_DELAY,
                                   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + @".LATE_PAYMENT_INTEREST
                             FROM  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + " ";

                if (txtDeducteeName.Text != "")
                    strSQL = strSQL + " WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + ".DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                if ((txtDeducteePAN.Text != "") && (txtDeducteeName.Text != ""))
                    strSQL = strSQL + " AND " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + ".DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";
                else if (txtDeducteePAN.Text != "")
                    strSQL = strSQL + " WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + ".DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";


                strOrderBy = @" ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + ".CHALLAN_SL_NO," + TDSMAN.Classes.TDSMAN.T_tblTEMP_LATE_DEDUCTION_SUMMARY + ".DEDUCTEE_SL_NO ";

                strSQL = strSQL + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewTaxSlab);
            }
        }
        #endregion


    #endregion
    }

}