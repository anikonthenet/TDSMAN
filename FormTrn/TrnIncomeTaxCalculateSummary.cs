
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
    public partial class TrnIncomeTaxCalculateSummary : Form
    {
        #region Constructor
        public TrnIncomeTaxCalculateSummary(string FinancialYear,
                                            string EmployeeName, 
                                            string EmployeePAN,
                                            string Category,
                                            string Taxation115BAC,
                                            string TaxableAmount,
                                            bool FetchData)
        {
            strEmployeeName = EmployeeName;
            strEmployeePAN = EmployeePAN;
            strCategory = Category;
            //
            strTaxation115BAC = Taxation115BAC;
            //
            strFinancialYear = FinancialYear;
            strTaxableAmount = TaxableAmount;
            //blnFetchData = FetchData;
            blnFetchData = false;
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
        string strCategory = "", strTaxation115BAC = "";
        string strFinancialYear = "";
        string strTaxableAmount = "";
        bool blnFetchData = false;
        //-------------------------------------------------


        #endregion 

        #region User Defined Events

        #region TrnIncomeTaxCalculateSummary_Load
        private void TrnIncomeTaxCalculateSummary_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //--
            lblTotalTaxableAmt.Text = "0.00";
            lblSumOfTaxAmt.Text = "0.00";
            lblTaxCreditAmt.Text = "0.00";
            lblTotalTax.Text = "0.00";
            lblSurchargeAmt.Text = "0.00";
            lblTotalCessAmt.Text = "0.00";
            lblGrossTotal.Text = "0.00";
            //--
            lblTaxation115BAC.Visible = false;
            if(strTaxation115BAC == T_TRUE_FALSE.TRUE.ToString() )
            {
                lblTaxation115BAC.Visible = true;
            }
            //--
            lblEmployeeName.Text = strEmployeeName;
            lblPAN.Text = strEmployeePAN;
            lblCategory.Text = strCategory;
            lblFinYear.Text = strFinancialYear;
            lblTotalTaxableAmt.Text = strTaxableAmount;
            //--

            //--
            if (blnFetchData == false)
            {
                btnFetchData.Visible = false;
            }
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

        #region btnFetchData_Click
        private void btnFetchData_Click(object sender, EventArgs e)
        {
            btnExit_Click(sender, e);
        }
        #endregion 

        #region lnkTaxCalculation_LinkClicked
        private void lnkTaxCalculation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //---------------------------------------------------------
            //-- DHRUB ON  2014/01/02
            //---------------------------------------------------------
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }
            //System.Diagnostics.Process.Start("http://law.incometaxindia.gov.in/DIT/Xtras/taxcalc.aspx");
            //System.Diagnostics.Process.Start("https://www.incometaxindia.gov.in/Pages/tools/tax-calculator.aspx");
            System.Diagnostics.Process.Start("https://eportal.incometax.gov.in/iec/foservices/#/TaxCalc/calculator");
        }
        #endregion 

        #endregion

        #region User Defined Functions

        #region PopulateGrid
        public void PopulateGrid()
        {
            //strTaxableAmount = "10000001";


            IDataReader drdGetTaxCalculateSummary = null;
            int intAsstId = 0; bool blnEscapetMarginalRelief = false;
            double dblSumOfTaxAmt = 0, dblSumOfTaxAmtMarginalRelief=0;

            #region COMMENT
            //            string[,] strMatrixViewDeductee = {{"Income From",      "70", "0.00", "R", "", "", ""},
//                                               {"Income To",        "70", "0.00", "R", "", "", ""},
//                                               {"Tax Rate",         "10", "0.00", "R", "", "", ""},
//                                               {"Amount Exceeding", "70", "0.00", "R", "", "", ""},
//                                               {"Add Amount",       "70", "0.00", "R", "", "", ""},
//                                               {"Tax Credit Rate",  "10", "0.00", "R", "", "", ""},
//                                               {"Amount Exceeding", "70", "0.00", "R", "", "", ""},
//                                               {"Education Cess Rate","70", "0.00", "R", "", "", ""}};

//            // Get the details of 
//            strSQL = @"SELECT INCOME_FORM,
//                              INCOME_TO,
//                              TAX_RATE,
//                              AMOUNT_EXCEEDING,
//                              ADD_AMOUNT,
//                              EDUCATION_CESS_RATE,
//                              TAX_CREDIT_RATE,
//                              MAX_TAX_CREDIT
//                       FROM   MST_INCOME_TAX_SLAB
//                       WHERE  EMPLOYEE_PAN = '" + cmnService.J_ReplaceQuote(strEmployeePAN) + "'" + @"
//                       AND    MST_INCOME_TAX_SLAB.ASST_ID = " + intAsstId + @" 
//                       AND    MST_INCOME_TAX_SLAB.INCOME_FORM <=" + dblTaxableAmount + @"
//                       AND    MST_INCOME_TAX_SLAB.INCOME_TO   >=" + dblTaxableAmount + @" 
//                       ORDER BY TRN_BASIC_INFO.QTR,
//                                TRN_CHALLAN.SL_NO, 
            //                                TRN_DEDUCTEE_DETAILS.SL_NO ";
            #endregion

            string[,] strMatrixViewTaxSlab = {{"Income Greater Than", "90", "0.00", "R", "", "", "T"},
                                               {"Income To","90", "0.00", "R", "", "", "T"},
                                               {"Tax Rate", "50", "0.00", "R", "", "", "T"},
                                               {"Income Slab", "90", "0.00", "R", "", "", "T"},
                                               {"Tax Amount", "100", "0.00", "R", "", "", "T"},
                                               {"SECTION_115BAC_MARGINAL_RELIEF", "0", "", "", "", "F", ""}};
            //
            string[,] strSQLToAmountMatrix = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO = 99999999999999.00", "F", "& ABOVE", "T"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO != 99999999999999.00", "F", "CAST(" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO AS VARCHAR)", "F"}};
            
            string[,] strToAmountMatrix = {{"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO = 99999999999999.00", "F", "& ABOVE", "T"},
                                               {"" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO != 99999999999999.00", "F", "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO", "F"}};
                        
                        
            //Get the Assessment Id from Financial Year
            strSQL = "SELECT ASST_ID FROM MST_ASSESSMENT" +
                   "     WHERE  FA_YEAR  = '" + strFinancialYear + "' ";
            intAsstId = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

            // Get the details of Grid
//            strSQL = @"SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_FROM, " +
//                              //TEMP_TAX_SLAB_SUMMARY.INCOME_TO, " +
//                      "  " + cmnService.J_SQLDBFormat(strToAmountMatrix, J_SQLColFormat.Case_End) + @" AS INCOME_TO, 
//                              " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".TAX_RATE,
//                              " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".SLAB_WISE_TAX_AMOUNT
//                       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + " ";

            strSQL = @"SELECT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".INCOME_FROM,";
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strSQL = strSQL + cmnService.J_SQLDBFormat(strSQLToAmountMatrix, J_SQLColFormat.Case_End) + " AS INCOME_TO,";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strSQL = strSQL + cmnService.J_SQLDBFormat(strToAmountMatrix, J_SQLColFormat.Case_End) + " AS INCOME_TO,";
            strSQL = strSQL + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".TAX_RATE,
                              " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".AMOUNT_TAX,
                              " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".SLAB_WISE_TAX_AMOUNT,
                              " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + @".SECTION_115BAC_MARGINAL_RELIEF
                       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + " ";
            strOrderBy = @" ORDER BY INCOME_FROM ";
            //
            strSQL = strSQL + strOrderBy;
            //-----------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgcViewRecords, strSQL, strMatrixViewTaxSlab);
            //-----------
            //For loop 
            //dblSumOfTaxAmt = 0;
            for (int i = 0; i < dgcViewRecords.Rows.Count; i++)
            {
                //dblSumOfTaxAmt = dblSumOfTaxAmt + Convert.ToDouble(dgcViewRecords.Rows[i].Cells[3].Value.ToString());
                //2024/01/29
                dblSumOfTaxAmt = dblSumOfTaxAmt + Convert.ToDouble(dgcViewRecords.Rows[i].Cells[4].Value.ToString());
                //-- 2024/04/10
                if ((i == dgcViewRecords.Rows.Count - 1) && dgcViewRecords.Rows[i].Cells[5].Value.ToString() == "1")
                {
                    //dblSumOfTaxAmt = Convert.ToDouble(dgcViewRecords.Rows[i].Cells[3].Value.ToString());
                    dblSumOfTaxAmtMarginalRelief = Convert.ToDouble(dgcViewRecords.Rows[i].Cells[3].Value.ToString());
                    blnEscapetMarginalRelief = true;
                }
                //--
            }
            //-----------
            //SUM OF THE TOTAL TAX AMOUNT SLAB WISE
            if (dblSumOfTaxAmt > 0)
            {
                lblSumOfTaxAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblSumOfTaxAmt));
                if (blnEscapetMarginalRelief == true)
                {
                    if (dblSumOfTaxAmtMarginalRelief < dblSumOfTaxAmt)
                        lblTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblSumOfTaxAmt - dblSumOfTaxAmtMarginalRelief));
                    else
                        lblTaxCreditAmt.Text = "0.00";
                    //--
                    lblTotalTax.Text = string.Format("{0:0.00}", Convert.ToDouble(lblSumOfTaxAmt.Text) - Convert.ToDouble(lblTaxCreditAmt.Text));
                    dblSumOfTaxAmt = Convert.ToDouble(lblSumOfTaxAmt.Text) - Convert.ToDouble(lblTaxCreditAmt.Text);
                }
            }
            else
                lblSumOfTaxAmt.Text = "0.00";
            //-----------
            // Taking Out the Education Cess Amount From the Temp Table 
            strSQL = @"SELECT EDUCATION_CESS_RATE,
                              EDUCATION_CESS_AMOUNT,
                              TAX_CREDIT_RATE,
                              TAX_CREDIT_MAX_LIMIT,
                              TAX_CREDIT_AMOUNT,
                              SURCHARGE_AMOUNT,
                              SECTION_115BAC_MARGINAL_RELIEF 
                       FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + " " +
                     @"WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_FROM   < " + strTaxableAmount + " "+
                     @"AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_TAX_SLAB_SUMMARY + ".INCOME_TO     >=" + strTaxableAmount + " "; 
            drdGetTaxCalculateSummary = dmlService.J_ExecSqlReturnReader(strSQL);
            //
            if (drdGetTaxCalculateSummary == null)
                return;
            //
            while (drdGetTaxCalculateSummary.Read())
            {
                //Deduct the Tax Credit Amount
                if (Convert.ToDouble(dblSumOfTaxAmt) > Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]))
                    dblSumOfTaxAmt = Convert.ToDouble(dblSumOfTaxAmt - Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]));
                else
                    dblSumOfTaxAmt = 0;

                //-----------
                //Initialize the Total Tax Amount Slab Wise into the Variable 
                if(blnEscapetMarginalRelief == false)
                    lblTotalTax.Text = string.Format("{0:0.00}",Convert.ToDouble(dblSumOfTaxAmt));

                //------------EDUCATION_CESS_AMOUNT
                if (intAsstId >= T_FinancialYearID.F2013_14ID)
                {
                    if (Convert.ToDouble(drdGetTaxCalculateSummary["EDUCATION_CESS_AMOUNT"]) > 0)
                        //-- 2015/03/27
                        //lblEduCessText.Text = lblEduCessText.Text + " " + Convert.ToString(drdGetTaxCalculateSummary["EDUCATION_CESS_RATE"]) + "% On " + string.Format("{0:0.00}", dblSumOfTaxAmt + Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                        if (intAsstId >= T_FinancialYearID.F2018_19ID)
                            lblEduCessText.Text = lblEduCessText.Text + " 4% On " + string.Format("{0:0.00}", dblSumOfTaxAmt + Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                        else
                            lblEduCessText.Text = lblEduCessText.Text + " 3% On " + string.Format("{0:0.00}", dblSumOfTaxAmt + Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                    else
                        lblEduCessText.Text = "Add: Edu Cess ";
                }
                else
                {
                    //if (Convert.ToDouble(drdGetTaxCalculateSummary["EDUCATION_CESS_AMOUNT"]) > 0)
                    //    //-- 2015/03/27
                    //    //lblEduCessText.Text = lblEduCessText.Text + " " + Convert.ToString(drdGetTaxCalculateSummary["EDUCATION_CESS_RATE"]) + "% On " + string.Format("{0:0.00}", dblSumOfTaxAmt);
                    //    lblEduCessText.Text = lblEduCessText.Text + " 3% On " + string.Format("{0:0.00}", Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                    //else
                    lblEduCessText.Text = "Add: Edu Cess (3%) ";
                }
                //
                if (Convert.ToDouble(drdGetTaxCalculateSummary["EDUCATION_CESS_AMOUNT"]) > 0)
                    lblTotalCessAmt.Text = string.Format("{0:0.00}",Convert.ToDouble(drdGetTaxCalculateSummary["EDUCATION_CESS_AMOUNT"]));
                else
                    lblTotalCessAmt.Text = "0.00";

                //------------TAX_CREDIT_AMOUNT

                //if (Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]) > 0)
                //    lblTaxCreditText.Text = lblTaxCreditText.Text + " " + Convert.ToString(drdGetTaxCalculateSummary["TAX_CREDIT_RATE"]) + "% On " + string.Format("{0:0.00}", Convert.ToDouble(strTaxableAmount));
                //else //-- 2015/03/27
                    lblTaxCreditText.Text = "Less: Rebate u/s 87A ";

                if (Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]) > 0 && blnEscapetMarginalRelief == false)
                {
                    //-- COMMENTED ON 2024/04/23 lblTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]));
                    if(cmnService.J_ReturnDoubleValue(lblSumOfTaxAmt.Text) < Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]))
                        lblTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(lblSumOfTaxAmt.Text));
                    else
                        lblTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(drdGetTaxCalculateSummary["TAX_CREDIT_AMOUNT"]));
                }
                else if (blnEscapetMarginalRelief == false)
                    lblTaxCreditAmt.Text = "0.00";                
                
                //-------------------SURCHARGE_AMOUNT
                if (Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]) > 0)
                    lblSurchargeAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                else
                    lblSurchargeAmt.Text = "0.00";
                //-------------------
                //GROSS TOTAL
                if (Convert.ToDouble(dblSumOfTaxAmt) > 0)
                    lblGrossTotal.Text = string.Format("{0:0.00}", Convert.ToDouble(dblSumOfTaxAmt) + Convert.ToDouble(drdGetTaxCalculateSummary["EDUCATION_CESS_AMOUNT"]) + Convert.ToDouble(drdGetTaxCalculateSummary["SURCHARGE_AMOUNT"]));
                else
                    lblGrossTotal.Text = "0.00";

                //Highlight the row 
                for (int i = 0; i < dgcViewRecords.Rows.Count; i++)
                {
                    if ((Convert.ToDouble(dgcViewRecords.Rows[i].Cells[0].Value.ToString()) < Convert.ToDouble(strTaxableAmount)) &&
                        (Convert.ToDouble(dgcViewRecords.Rows[i].Cells[1].Value.ToString().Replace(dgcViewRecords.Rows[i].Cells[1].Value.ToString(), "99999999999999.00")) >= Convert.ToDouble(strTaxableAmount)))
                    {
                        //dgcViewRecords.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                        dgcViewRecords.Rows[i].Selected = true;
                    }
                    if (cmnService.J_IsNumeric(dgcViewRecords.Rows[i].Cells[1].Value.ToString())==true )
                    {
                        dgcViewRecords.Rows[i].Cells[1].Value = string.Format("{0:0.00}",Convert.ToDouble(dgcViewRecords.Rows[i].Cells[1].Value));
                    }
                }                
            }
            drdGetTaxCalculateSummary.Close();
            drdGetTaxCalculateSummary.Dispose();   
        }
        #endregion

        #endregion
    }

}