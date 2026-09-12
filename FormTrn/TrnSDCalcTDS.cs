
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnSDCalcTDS
Version			: 
Start Date		: 01/11/2019
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
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.Reports.Transaction;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnSDCalcTDS : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnSDCalcTDS()
        {
            InitializeComponent();
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
        RptDialog rptDialog = new RptDialog();
        ReportService rptService = null;
        // report file object
        ReportClass rptcls;
        //-----------------------------------------------------------------------
        string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //-----------------------------------------------------------------------
        string[,] strMatrix = null;
        //-----------------------------------------------------------------------
        long lngEmployeeID = 0;
        long lngCompanyID = 0;
        double dblIncomeTax = 0;
        double dblFeeUnderSec234E = 0;
        double dblSurcharge = 0;
        double dblEduCess = 0;
        double dblInterest = 0;
        double dblPenalty = 0;
        double dblTotalAmt = 0;
        string strDateofPayment = "";
        double dblMaxTotalLimit = 999999999.00;
        //
        ToolTip tllTip = new ToolTip();
        bool blnShowHelp = true;
        //
        #endregion

        #region User Defined Events

        #region TrnSDCalcTDS_Load
        private void TrnSDCalcTDS_Load(object sender, EventArgs e)
        {
            //-----------------------------------------------------------
            GC.Collect();
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //-----------------------------------------------------------
            lblSearchMode.Text = J_Mode.General;
            //-----------------------------------------------------------
            //ViewGrid.Height = 557;
            //-----------------------------------------------------------
            lblTitle.Text = "Calculate Salary TDS (Form 24Q - SD)";
            //-----------------------------------------------------------
            ClearControls();
            //ControlVisible(true);
            //-----------------------------------------------------------
            ViewGrid.Visible = false;
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEmployeeName.Text = "";
            txtEmployeePAN.Text = "";
        }
        #endregion

        #region txtProfessionalTax_KeyPress
        private void txtProfessionalTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtProfessionalTax, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDeductionus10_KeyPress
        private void txtDeductionus10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDeductionus10, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtStdDeduction_KeyPress
        private void txtStdDeduction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtStdDeduction, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOtherEmployer_KeyPress
        private void txtOtherEmployer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherEmployer, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtIncomeLossProperty_KeyPress
        private void txtIncomeLossProperty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtIncomeLossProperty, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOtherIncome_KeyPress
        private void txtOtherIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncome, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80C_KeyPress
        private void txtSec80C_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80C, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCC_KeyPress
        private void txtSec80CCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCC, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD1_KeyPress
        private void txtSec80CCD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD1, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD1B_KeyPress
        private void txtSec80CCD1B_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD1B, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD2_KeyPress
        private void txtSec80CCD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD2, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80D_KeyPress
        private void txtSec80D_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80D, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80E_KeyPress
        private void txtSec80E_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80E, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80G_KeyPress
        private void txtSec80G_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80G, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80TTA_KeyPress
        private void txtSec80TTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80TTA, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOtherSections_KeyPress
        private void txtOtherSections_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherSections, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtTaxTotalIncome_KeyPress
        private void txtTaxTotalIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTaxTotalIncome, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtRebate_KeyPress
        private void txtRebate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtRebate, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSurcharge_KeyPress
        private void txtSurcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSurcharge, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtEducationCess_KeyPress
        private void txtEducationCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtEducationCess, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtReliefUS89_KeyPress
        private void txtReliefUS89_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtReliefUS89, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtTotalTDSDedcuted_KeyPress
        private void txtTotalTDSDedcuted_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", MonthlyTDS, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTDSPaidTillDate_KeyPress
        private void txtTDSPaidTillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTDSPaidTillDate, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtAnnualGrossSalary_KeyPress
        private void txtAnnualGrossSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAnnualGrossSalary, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtEmployeePAN_KeyPress
        private void txtEmployeePAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                cmbEmployeeCategory.Select();
            else
                if (TdsMan.gTANNoPANNoValidation(txtEmployeePAN, e, T_TANPAN.PAN) == false)
                e.Handled = true;
        }
        #endregion

        #region txtEmployeePAN_Leave
        private void txtEmployeePAN_Leave(object sender, EventArgs e)
        {
            if (txtEmployeePAN.Text.Trim() == "") txtEmployeePAN.Text = "PANNOTAVBL";
        }
        #endregion
                
        #region cmbFinancialYear_SelectedIndexChanged
        private void cmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2018_19ID)
            {
                txtRebate.Visible = true;
                lblRebateCalculated.Visible = true;
                //--
                txtAnnualGrossSalary.Text = "0.00";
                txtProfessionalTax.Text = "0.00";
                txtDeductionus10.Text = "0.00";
                txtStdDeduction.Text = "0.00";
                txtOtherEmployer.Text = "0.00";
                txtIncomeLossProperty.Text = "0.00";
                txtOtherIncome.Text = "0.00";
                txtSec80C.Text = "0.00";
                txtSec80CCC.Text = "0.00";
                txtSec80CCD1.Text = "0.00";
                txtSec80CCD1B.Text = "0.00";
                txtSec80CCD2.Text = "0.00";
                txtSec80D.Text = "0.00";
                txtSec80E.Text = "0.00";
                txtSec80G.Text = "0.00";
                txtSec80TTA.Text = "0.00";
                txtOtherSections.Text = "0.00";
                txtTaxableIncome.Text = "0.00";
                lblTaxOnTotalIncomeCalculated.Text = "0.00";
                txtTaxTotalIncome.Text = "0.00";
                lblRebateCalculated.Text = "0.00";
                txtRebate.Text = "0.00";
                lblTaxDeductingRebate.Text = "0.00";
                lblCalculatedSurcharge.Text = "0.00";
                txtSurcharge.Text = "0.00";
                lblTaxCalculatedECess.Text = "0.00";
                txtEducationCess.Text = "0.00";
                lblGrossTaxPayable.Text = "0.00";
                txtReliefUS89.Text = "0.00";
                lblTaxPayable.Text = "0.00";
                lblMonthlyTDS.Text = "0.00";
                txtTDSPaidTillDate.Text = "0.00";
                //--
                txtTDSToBeDeducted.Text = "0.00";
                lblRebateCaption.Visible = true;
                lblEducationCessDesc.Text = "3. Edu Cess@4% (on tax computed at Srl No. 1)";
            }
            else
            {
                txtRebate.Visible = false;
                lblRebateCalculated.Visible = false;
                //--
                txtAnnualGrossSalary.Text = "0.00";
                txtProfessionalTax.Text = "0.00";
                txtDeductionus10.Text = "0.00";
                txtStdDeduction.Text = "0.00";
                txtOtherEmployer.Text = "0.00";
                txtIncomeLossProperty.Text = "0.00";
                txtOtherIncome.Text = "0.00";
                txtSec80C.Text = "0.00";
                txtSec80CCC.Text = "0.00";
                txtSec80CCD1.Text = "0.00";
                txtSec80CCD1B.Text = "0.00";
                txtSec80CCD2.Text = "0.00";
                txtSec80D.Text = "0.00";
                txtSec80E.Text = "0.00";
                txtSec80G.Text = "0.00";
                txtSec80TTA.Text = "0.00";
                txtOtherSections.Text = "0.00";
                txtTaxableIncome.Text = "0.00";
                lblTaxOnTotalIncomeCalculated.Text = "0.00";
                txtTaxTotalIncome.Text = "0.00";
                lblRebateCalculated.Text = "0.00";
                txtRebate.Text = "0.00";
                lblTaxDeductingRebate.Text = "0.00";
                lblCalculatedSurcharge.Text = "0.00";
                txtSurcharge.Text = "0.00";
                lblTaxCalculatedECess.Text = "0.00";
                txtEducationCess.Text = "0.00";
                lblGrossTaxPayable.Text = "0.00";
                txtReliefUS89.Text = "0.00";
                lblTaxPayable.Text = "0.00";
                lblMonthlyTDS.Text = "0.00";
                txtTDSPaidTillDate.Text = "0.00";
                //--
                txtTDSToBeDeducted.Text = "0.00";
                lblRebateCaption.Visible = false;
                lblEducationCessDesc.Text = "3. Edu Cess@3% (on tax computed at Srl No. 1)";
            }
        }
        #endregion

        #region chkPopulateCalculation_CheckStateChanged
        private void chkPopulateCalculation_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkPopulateCalculation.Checked == true)
            {
                txtTaxTotalIncome.Text = lblTaxOnTotalIncomeCalculated.Text;
                txtRebate.Text         = lblRebateCalculated.Text;
                txtSurcharge.Text      = lblCalculatedSurcharge.Text;
                txtEducationCess.Text  = lblTaxCalculatedECess.Text;
            }
            else
            {
                txtTaxTotalIncome.Text = "0.00";
                txtRebate.Text         = "0.00";
                txtSurcharge.Text      = "0.00";
                txtEducationCess.Text  = "0.00";
            }
        }
        #endregion

        #region btnCalcSD_Click
        private void btnCalcSD_Click(object sender, EventArgs e)
        {
            //-- string.Format("{0:0.00}", (Math.Round(cmnService.J_ReturnDoubleValue(cmnService.J_ReturnDoubleValue(txtAmountSuperannuationFund.Text) * (cmnService.J_ReturnDoubleValue(txtRateSuperannuationFund.Text) / 100)), 0, MidpointRounding.AwayFromZero)));
            //////##### CALC TDS FROM THE SELECTED MONTH
            ////double dblNetTaxToBePaid = cmnService.J_ReturnDoubleValue(lblTaxPayable.Text) - cmnService.J_ReturnDoubleValue(txtTDSPaidTillDate.Text);
            //////
            ////long lngPaidMonth = 0;
            ////lngPaidMonth = (12 - Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex))) + 1;
            //////
            ////txtTDSToBeDeducted.Text = string.Format("{0:0.00}", Math.Round(dblNetTaxToBePaid / lngPaidMonth, 0, MidpointRounding.AwayFromZero));
            //////########
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
            //###################### INSERT DATA TO LOG #######################
            lngCompanyID = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
            //-- GET EMPLOYEE_ID
            strSQL = "SELECT EMPLOYEE_ID " +
                "     FROM   MST_EMPLOYEE " +
                "     WHERE  COMPANY_ID    = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                "     AND    EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'";                        //--

            //Modified by Indrajit on 22-02-2013
            //lngEmployeeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
            DMLService dml = new DMLService();
            lngEmployeeID = cmnService.J_NullToZero(dml.J_ExecSqlReturnScalar(strSQL));
            //--
            if (lngEmployeeID == 0)
            {
                strSQL = "INSERT INTO MST_EMPLOYEE " +
                        "            (EMPLOYEE_NAME," +
                        "             EMPLOYEE_PAN," +
                        "             CATEGORY," +
                        "             COMPANY_ID) " +
                        "     VALUES ('" + cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim()) + "'," +
                        "             '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                        "             '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "'," +
                        "              " + TDSMAN.Classes.TDSMAN.T_pCompanyId + ")";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
                //--
                lngEmployeeID = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_EMPLOYEE", "EMPLOYEE_ID");
            }
            //--
            strSQL = @"INSERT INTO TRN_CALC_SD_LOG_HEADER (
                              EMPLOYEE_ID,
                              SETUP_ID,
                              COMPANY_ID,
                              MONTH_ID,
                              ASST_ID,
                              CATEGORY,
                              GROSS_SALARY,
                              TAXABLE_INCOME,
                              TAX_TOTAL_INCOME,
                              REBATE_AMOUNT,
                              SURCHARGE_AMOUNT,
                              EDU_CESS_AMOUNT,
                              TAX_PAYABLE_AMOUNT,
                              RELIEF_US89_AMOUNT,
                              NET_TAX_PAYABLE_AMOUNT,
                              MONTHLY_TDS_AMOUNT,
                              TOTAL_TDS_PAID_AMOUNT,
                              TOTAL_TDS_CALC) 
                       VALUES (
                              " + lngEmployeeID + @",
                              " + TDSMAN.Classes.TDSMAN.T_MACHINE_ID + @",
                              " + lngCompanyID + @",
                              " + Convert.ToInt32(Support.GetItemData(cmbMonth, cmbMonth.SelectedIndex)) + @",
                              " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
                              '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + @"',
                              " + Convert.ToDouble(txtAnnualGrossSalary.Text) + @",
                              " + Convert.ToDouble(txtTaxableIncome.Text) + @",
                              " + Convert.ToDouble(txtTaxTotalIncome.Text) + @",
                              " + Convert.ToDouble(txtRebate.Text) + @",
                              " + Convert.ToDouble(txtSurcharge.Text) + @",
                              " + Convert.ToDouble(txtEducationCess.Text) + @",
                              " + Convert.ToDouble(lblGrossTaxPayable.Text) + @",
                              " + Convert.ToDouble(txtReliefUS89.Text) + @",
                              " + Convert.ToDouble(lblTaxPayable.Text) + @",
                              " + Convert.ToDouble(lblMonthlyTDS.Text) + @",
                              " + Convert.ToDouble(txtTDSPaidTillDate.Text) + @",
                              " + Convert.ToDouble(txtTDSToBeDeducted.Text) + @")";
            dmlService.J_ExecSql(strSQL);
            //#################################################################
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            //--
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbMonth_SelectedIndexChanged
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkPopulateCalculation.Checked = false;
            txtTaxTotalIncome.Text = "0.00";
            //lblRebateCalculated.Text = "0.00";
            txtRebate.Text = "0.00";
            //lblTaxDeductingRebate.Text = "0.00";
            //lblCalculatedSurcharge.Text = "0.00";
            txtSurcharge.Text = "0.00";
            //lblTaxCalculatedECess.Text = "0.00";
            txtEducationCess.Text = "0.00";
            lblGrossTaxPayable.Text = "0.00";
            txtReliefUS89.Text = "0.00";
            lblTaxPayable.Text = "0.00";
            lblMonthlyTDS.Text = "0.00";
            txtTDSPaidTillDate.Text = "0.00";
            txtTDSToBeDeducted.Text = "0.00";
        }
        #endregion

        #region txtEmployeeName_TextChanged
        private void txtEmployeeName_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowEmployeeHelp = null;
            string strSQLShowHelpEmployee = "";
            //--
            try
            {
                if(cmbCompany.SelectedIndex<=0)
                {
                    cmnService.J_UserMessage("Please select the Company...");
                    //txtEmployeeName.Text = "";
                    lstEmployeeHelp.Visible = false;
                    cmbCompany.Select();
                    return;
                }
                //--
                if (txtEmployeeName.Text.Trim() == "")
                {
                    lstEmployeeHelp.Visible = false;
                    return;
                }

                if (blnShowHelp == false)
                    return;
                //-----------------------
                string[,] strShowHelpEmployeeMatrix = {{"MST_EMPLOYEE.CATEGORY = 'G'", "F", T_EmployeeCategory.General, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'W'", "F", T_EmployeeCategory.Woman, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"MST_EMPLOYEE.CATEGORY = ''", "F", "", "T"}};
                //
                strSQLShowHelpEmployee = "SELECT EMPLOYEE_ID," +
                    "                            EMPLOYEE_NAME," +
                    "                            EMPLOYEE_PAN," +
                    "                            " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY " +
                    "                     FROM   MST_EMPLOYEE," +
                    "                            MST_COMPANY " +
                    "                     WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "                     AND    MST_COMPANY.COMPANY_ID  = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)) + " " +
                    "                     AND    MST_EMPLOYEE.EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "%' " +
                    "                     AND    MST_EMPLOYEE.INACTIVE_FLAG = 0 " +
                    "                     ORDER BY EMPLOYEE_NAME, EMPLOYEE_ID";

                drdShowEmployeeHelp = dmlService.J_ExecSqlReturnReader(strSQLShowHelpEmployee);
                //--
                if (drdShowEmployeeHelp == null)
                {
                    lstEmployeeHelp.Visible = false;
                    return;
                }
                else
                {
                    lstEmployeeHelp.Items.Clear();
                    //lstEmployeeHelp.Height = 19;
                    lstEmployeeHelp.Visible = true;
                    while (drdShowEmployeeHelp.Read())
                    {
                        lstEmployeeHelp.Items.Add(new ListBoxItem(drdShowEmployeeHelp["EMPLOYEE_NAME"].ToString().PadRight(45) + drdShowEmployeeHelp["EMPLOYEE_PAN"].ToString().PadRight(13) + drdShowEmployeeHelp["CATEGORY"]));
                        //--
                        //if (lstEmployeeHelp.Height <= 100)
                        //    lstEmployeeHelp.Height = lstEmployeeHelp.Height + 19;
                    }
                    //--
                    if (lstEmployeeHelp.Items.Count <= 0)
                        lstEmployeeHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShowEmployeeHelp.Close();
                drdShowEmployeeHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowEmployeeHelp.Close();
                drdShowEmployeeHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #region txtEmployeeName_KeyPress
        private void txtEmployeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstEmployeeHelp.Visible == true)
                {
                    lstEmployeeHelp.Focus();
                    lstEmployeeHelp.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region txtEmployeeName_KeyDown
        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstEmployeeHelp.Visible == true)
                {
                    lstEmployeeHelp.Focus();
                    lstEmployeeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region txtEmployeeName_Leave
        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            //if (lstEmployeeHelp.Visible == true) lstEmployeeHelp.Visible = false;
        }
        #endregion

        #region txtEmployeeName_MouseMove
        private void txtEmployeeName_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(txtEmployeeName, txtEmployeeName.Text);
        }
        #endregion

        #region lstEmployeeHelp_Click
        private void lstEmployeeHelp_Click(object sender, EventArgs e)
        {
            string strEmployeeHelp = "";
            strEmployeeHelp = lstEmployeeHelp.Text;
            //
            txtEmployeePAN.Text = cmnService.J_Mid(strEmployeeHelp, 45, 10);
            txtEmployeeName.Text = cmnService.J_Left(strEmployeeHelp, 45).Trim();
            cmbEmployeeCategory.Text = strEmployeeHelp.Substring(58).Trim();
            //--
            lstEmployeeHelp.Visible = false;
            //--
            txtEmployeePAN.Select();
        }
        #endregion

        #region lstEmployeeHelp_KeyPress
        private void lstEmployeeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13)
            //    lstEmployeeHelp_Click(sender, e);
            //else if (Convert.ToInt64(e.KeyChar) == 27)
            //{
            //    lstEmployeeHelp.Visible = false;
            //    txtEmployeeName.Select();
            //}

        }
        #endregion


        #region txtCalc_TextChanged
        private void txtCalc_TextChanged(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
                return;
            //-- string.Format("{0:0.00}", (Math.Round(cmnService.J_ReturnDoubleValue(cmnService.J_ReturnDoubleValue(txtAmountSuperannuationFund.Text) * (cmnService.J_ReturnDoubleValue(txtRateSuperannuationFund.Text) / 100)), 0, MidpointRounding.AwayFromZero)));
            txtTaxableIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtAnnualGrossSalary.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtProfessionalTax.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtDeductionus10.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtStdDeduction.Text)
                                                             + cmnService.J_ReturnDoubleValue(txtOtherEmployer.Text)
                                                             + cmnService.J_ReturnDoubleValue(txtIncomeLossProperty.Text)
                                                             + cmnService.J_ReturnDoubleValue(txtOtherIncome.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80C.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80CCC.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80CCD1.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80CCD1B.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80CCD2.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80D.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80E.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80G.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtSec80TTA.Text)
                                                             - cmnService.J_ReturnDoubleValue(txtOtherSections.Text));
            #region CALCULATE TAX
            double dblCalculatedECess = 0, dblCalculatedSurcharge = 0, dblCalculatedTaxCredit = 0, dblTaxonTotalIncomeB4TaxCredit = 0;
            int intAsstId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            string strCategory = "";
            if (cmbEmployeeCategory.SelectedIndex <= 0)
                strCategory = "G";
            else
                strCategory = cmbEmployeeCategory.Text.Substring(0, 1);
            //
            double dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId, 
                                                                      strCategory, 
                                                                      Convert.ToDouble(txtTaxableIncome.Text),
                                                                      "", 
                                                                      out dblCalculatedECess, 
                                                                      out dblCalculatedSurcharge, 
                                                                      out dblCalculatedTaxCredit, 
                                                                      out dblTaxonTotalIncomeB4TaxCredit);
            //--
            dblCalculatedTax = Math.Round(dblCalculatedTax, 0);
            lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", dblCalculatedTax);
            if (dblCalculatedTax >= 0)
            {
                //lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                lblTaxDeductingRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTax)));
                lblTaxCalculatedECess.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedECess)));
                lblCalculatedSurcharge.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedSurcharge)));
                //
                lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", dblTaxonTotalIncomeB4TaxCredit);
            }
            else
            {
                lblTaxDeductingRebateCalculated.Text = "0.00";
                lblTaxCalculatedECess.Text = "0.00";
                lblCalculatedSurcharge.Text = "0.00";
            }
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
            #endregion
            //
            lblTaxDeductingRebate.Text = string.Format("{0:0.00}", Convert.ToDouble(txtTaxTotalIncome.Text) - Convert.ToDouble(txtRebate.Text));
            lblGrossTaxPayable.Text = string.Format("{0:0.00}", Convert.ToDouble(lblTaxDeductingRebate.Text) + Convert.ToDouble(txtSurcharge.Text) + Convert.ToDouble(txtEducationCess.Text));
            lblTaxPayable.Text = string.Format("{0:0.00}", Convert.ToDouble(lblGrossTaxPayable.Text) - Convert.ToDouble(txtReliefUS89.Text));
            lblMonthlyTDS.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDouble(Convert.ToDouble(lblTaxPayable.Text) / 12) + 0.5, 0, MidpointRounding.AwayFromZero));
        }







        #endregion

        #endregion

        #region User Defined Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
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

        #region ClearControls
        private void ClearControls()
        {
            //-------------------------------------------------------------------------------
            txtEmployeeName.Text = "";
            txtEmployeePAN.Text = "";
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //--
            cmbCompany.Select();
            //--
            string[] strEmployeeCategory = { T_EmployeeCategory.General, T_EmployeeCategory.Woman, T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategory, ref cmbEmployeeCategory, 1, J_ComboBoxSelectedIndex.YES);
            //--
            strSQL = " SELECT ASST_ID," +
                    "         FA_YEAR " +
                    "  FROM   MST_ASSESSMENT " +
                    "  WHERE  VISIBILITY_FLAG = 0 " +
                    "  ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //--
            strSQL = " SELECT MONTH_ORDER," +
                    "         MONTH_DESC " +
                    "  FROM   MST_MONTH " +
                    "  ORDER BY MONTH_ORDER";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbMonth, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //--
            txtAnnualGrossSalary.Text = "0.00";
            txtProfessionalTax.Text = "0.00";
            txtDeductionus10.Text = "0.00";
            txtStdDeduction.Text = "0.00";
            txtOtherEmployer.Text = "0.00";
            txtIncomeLossProperty.Text = "0.00";
            txtOtherIncome.Text = "0.00";
            txtSec80C.Text = "0.00";
            txtSec80CCC.Text = "0.00";
            txtSec80CCD1.Text = "0.00";
            txtSec80CCD1B.Text = "0.00";
            txtSec80CCD2.Text = "0.00";
            txtSec80D.Text = "0.00";
            txtSec80E.Text = "0.00";
            txtSec80G.Text = "0.00";
            txtSec80TTA.Text = "0.00";
            txtOtherSections.Text = "0.00";
            txtTaxableIncome.Text = "0.00";
            chkPopulateCalculation.Checked = false;
            lblTaxOnTotalIncomeCalculated.Text = "0.00";
            txtTaxTotalIncome.Text = "0.00";
            lblRebateCalculated.Text = "0.00";
            txtRebate.Text = "0.00";
            lblTaxDeductingRebate.Text = "0.00";
            lblCalculatedSurcharge.Text = "0.00";
            txtSurcharge.Text = "0.00";
            lblTaxCalculatedECess.Text = "0.00";
            txtEducationCess.Text = "0.00";
            lblGrossTaxPayable.Text = "0.00";
            txtReliefUS89.Text = "0.00";
            lblTaxPayable.Text = "0.00";
            lblMonthlyTDS.Text = "0.00";
            txtTDSPaidTillDate.Text = "0.00";
            //
            txtTDSToBeDeducted.Text = "0.00";
            //-------------------------------------------------------------------------------
        }

        #endregion


        #endregion
    }
}
