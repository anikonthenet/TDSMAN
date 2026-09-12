#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Dhrub Mukherjee
Module Name		: TrnMonthlyTDSCalculatorSummary
Version			: 1.0
Start Date		: 20/01/2014
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
//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace TDSMAN.FormSys
{
    public partial class TrnMonthlyTDSCalculatorSummary : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnMonthlyTDSCalculatorSummary()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables decleration
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
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        //RptDialog rptDialog = new RptDialog();
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------


        string strCategory = "";
        int intAsstId = 0;
        double dblTaxableAmount = 0;
        double dblCalculatedTax = 0;
        double dblCalculatedECess = 0;
        double dblCalculatedSurcharge = 0;
        double dblCalculateTaxCreditAmt = 0;
        double dblTaxCredit = 0;
        double dblTaxonTotalIncomeB4TaxCredit = 0;

        #endregion

        #region User Defined Events

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region TrnMonthlyTDSCalculatorSummary_Load
        private void TrnMonthlyTDSCalculatorSummary_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //----
            GC.Collect();

            lblTotTaxableAmt.Text = "0.00";
            //-------------------------------------------
            //-- FINANCIAL YEAR
            //-------------------------------------------
            
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  ASST_ID >=" + T_FinancialYearID.F2008_09ID + " " +
                "      ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1) == false) 
                return;
            //--------------------            
            
            //---------------------------
            //-- Category
            //---------------------------            
            string[] strEmployeeCategory ={ T_EmployeeCategory.General, T_EmployeeCategory.Woman, T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategory, ref cmbEmployeeCategory, 1);
            //--------------------            

            //Load and clear controls
            ClearControls();

            //Selecting Income textbox
            txtTotalIncomeAmt.Select();

        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            this.Close();
            this.Dispose();
            //
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Financial year Check
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                grpTaxCalculation.Visible = false;
                chkSection115BAC.Visible = false;
                return;
            }

            //-- 2020/07/09
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2020_21ID)
            {
                chkSection115BAC.Visible = true;
            }
            else
            {
                chkSection115BAC.Visible = false;
            }
            //Category check
            if (cmbEmployeeCategory.SelectedIndex <= 0)
            {
                grpTaxCalculation.Visible = false;
                return;
            }

            //Amount check
            if (lblTotTaxableAmt.Text == "0.00")
            {
                grpTaxCalculation.Visible = false;
                return;
            }
            //--
            //Calculating tax now
            CalculateTax();
        }
        #endregion

        #region cmbEmployeeCategory_KeyPress
        private void cmbEmployeeCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbFinancialYear_KeyPress
        private void cmbFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTotalIncomeAmt_KeyPress
        private void txtTotalIncomeAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,16,2", txtTotalIncomeAmt, "") == false)
                e.Handled = true;
        }
        #endregion 

        #region txtDeductionAmt_KeyPress
        private void txtDeductionAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnExit.Focus();//SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,16,2", txtDeductionAmt, "") == false)
                e.Handled = true;

        }
        #endregion 

        #region txtTotalIncomeAmt_TextChanged
        private void txtTotalIncomeAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dblTaxableAmount = (cmnService.J_ReturnDoubleValue(txtTotalIncomeAmt.Text) - cmnService.J_ReturnDoubleValue(txtDeductionAmt.Text));
            }
            catch
            {
                dblTaxableAmount = 0;
            }

            //Assigning calculated value to taxable amount
            lblTotTaxableAmt.Text = string.Format("{0:0.00}", dblTaxableAmount);

            if (cmnService.J_ReturnDoubleValue(lblTotTaxableAmt.Text) > 0)
                CalculateTax();
            else
                grpTaxCalculation.Visible = false;
        }
        #endregion

        #region txtTotTaxableAmt_TextChanged
        private void txtTotTaxableAmt_TextChanged(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(lblTotTaxableAmt.Text) > 0)
            {
                if (ValidateFields() == false) return;
                    CalculateTax();
            }
        }
        #endregion 

        
        #region lnkTaxCalculationVisitSite_LinkClicked
        private void lnkTaxCalculationVisitSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

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

        #region ClearControls
        private void ClearControls()
        {
            
            //grpTaxCalculation.Visible = false;
            //cmbFinancialYear.SelectedIndex = 1;
            //cmbEmployeeCategory.SelectedIndex = 1;
            
            txtTotalIncomeAmt.Text ="0.00";
            txtDeductionAmt.Text ="0.00";
            lblTotTaxableAmt.Text ="0.00";
            lblLessTaxCreditAmt.Text = "0.00";
            lblITaxOnTaxableAmt.Text = "0.00";
            lblSurchaargeAmt.Text = "0.00";
            lblEducationCessAmt.Text = "0.00";
            lblTotTaxAmt.Text = "0.00";
            lblMonthlyTDSAmt.Text = "0.00";

        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //TAXABLE AMOUNT -DEDUCTION
                if ((cmnService.J_ReturnDoubleValue(txtTotalIncomeAmt.Text)<= 0) && (cmnService.J_ReturnDoubleValue(txtDeductionAmt.Text)<= 0))
                {
                    grpTaxCalculation.Visible = false;
                    return false;
                }

                return true;

            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion 

        #region CalculateTax
        private void CalculateTax()
        {
            grpTaxCalculation.Visible = true;
            if (cmnService.J_ReturnDoubleValue(txtTotalIncomeAmt.Text) < cmnService.J_ReturnDoubleValue(txtDeductionAmt.Text))
            {
                lblTotTaxableAmt.Text = lblLessTaxCreditAmt.Text = lblITaxOnTaxableAmt.Text = lblSurchaargeAmt.Text = lblEducationCessAmt.Text = lblTotTaxAmt.Text = lblMonthlyTDSAmt.Text = "0.00";
                return;
            }
            if (dblTaxableAmount > 0)
            {
                //INITIALIZATION 
                strCategory = cmbEmployeeCategory.Text.Substring(0, 1);
                intAsstId = Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbFinancialYear, cmbFinancialYear.SelectedIndex));

                //-- upto fayear 10-11 -> SuperSeniorCitizen will be SeniorCitizen
                if (intAsstId <= T_FinancialYearID.F2010_11ID && cmbEmployeeCategory.Text == T_EmployeeCategory.SuperSeniorCitizen)
                    strCategory = T_EmployeeCategory.SeniorCitizen;

                
                //TAX CALCULATION
                dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId, 
                                                                   strCategory, 
                                                                   dblTaxableAmount, 
                                                                   chkSection115BAC.Checked.ToString(), 
                                                                   out dblCalculatedECess, 
                                                                   out dblCalculatedSurcharge, 
                                                                   out dblTaxCredit , 
                                                                   out dblTaxonTotalIncomeB4TaxCredit);
                
                if (dblCalculatedTax > 0)
                {
                    lblLessTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblTaxCredit));
                    lblITaxOnTaxableAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedTax));
                    lblEducationCessAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedECess));
                    lblSurchaargeAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedSurcharge));
                    lblTotTaxAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculatedTax + dblCalculatedECess + dblCalculatedSurcharge));
                    lblMonthlyTDSAmt.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDouble((dblCalculatedTax + dblCalculatedECess + dblCalculatedSurcharge) / 12), 0));
                    
                }
                else
                {
                    lblLessTaxCreditAmt.Text = "0.00";
                    lblITaxOnTaxableAmt.Text = "0.00";
                    lblSurchaargeAmt.Text = "0.00";
                    lblEducationCessAmt.Text = "0.00";
                    lblTotTaxAmt.Text = "0.00";
                    lblMonthlyTDSAmt.Text = "0.00";
                }


                //dblCalculateTaxCreditAmt = 0;
                //if (intAsstId == T_FinancialYearID.F2013_14ID)
                ////if (Category == "G" && AsstId == 9 && Convert.ToDouble(ds.Tables[0].Rows[i - 1]["INCOME_FROM"]) == 200000 && Convert.ToDouble(ds.Tables[0].Rows[i - 1]["INCOME_TO"]) == 500000)
                //{
                //    if ((dblTaxableAmount >= 200000 && dblTaxableAmount <= 500000) && (strCategory == "G" || strCategory == "W"))
                //    {
                //        //------------
                //        //Calculating Tax Credit
                //        dblCalculateTaxCreditAmt = (dblTaxableAmount * 10.00) / 100;
                //        //------------
                //        //Checking the threshold limit and assigning value
                //        if (dblCalculateTaxCreditAmt > 2000)
                //            dblCalculateTaxCreditAmt = 2000;
                //    }
                //}
                //if (dblCalculateTaxCreditAmt>0)
                //    lblLessTaxCreditAmt.Text = string.Format("{0:0.00}", Convert.ToDouble(dblCalculateTaxCreditAmt));
                //else
                //    lblLessTaxCreditAmt.Text = "0.00";
            }
        }
        #endregion 
        
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=GkYeCj-ompQ&t=2s");
        }
        #endregion

        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0101", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
    }
}