#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Dhruba Mukherjee
Module Name		: TrnPrintChallan281
Version			: 
Start Date		: 21-05-2015
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
    public partial class TrnPrintChallan281 : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code

        public TrnPrintChallan281()
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
        
        double dblIncomeTax = 0;
        double dblFeeUnderSec234E = 0;
        double dblSurcharge = 0;
        double dblEduCess = 0;
        double dblInterest = 0;
        double dblPenalty = 0;
        double dblTotalAmt = 0;
        string strDateofPayment = "";
        double dblMaxTotalLimit = 999999999.00;
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
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

        #region TrnPrintChallan281_Load
        private void TrnPrintChallan281_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            try
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
                lblTitle.Text = this.Text;
                //-----------------------------------------------------------
                ClearControls();
                ControlVisible(true);
                //-----------------------------------------------------------
                ViewGrid.Visible = false;
                
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string strAssessmentYear = "";
                string strSectionName = "";

                if (ValidateFields() == false)
                    return;

                //Fetching the Assssment Year from ASST_ID
                strAssessmentYear = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_YEAR FROM MST_ASSESSMENT WHERE ASST_ID = " + cmnService.J_GetComboBoxItemId(ref cmbAssessmentYear, cmbAssessmentYear.SelectedIndex)));

                //Fetching Section Name from Section_Id 
                //strSectionName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_NAME FROM MST_SECTION WHERE SECTION_ID = " + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)));
                //-- 2019/11/04
                strSectionName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_NAME FROM MST_SECTION WHERE SECTION_ID = " + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)));
                if (strSectionName.Length < 3 )
                    strSectionName = cmnService.J_Right(cmbSection.Text,3) + " - " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_ID = " + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)));
                else
                    strSectionName = strSectionName + " - " + Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_ID = " + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)));
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @" SELECT '" + cmbTypeofPayment.Text + @"'     AS TAX_APPLICABLE_TYPE," +
                             @"        '" + strAssessmentYear     + @"'     AS ASSESSMENT_YEAR,
                                       MST_COMPANY.COMPANY_ID, 
                                       MST_COMPANY.COMPANY_NAME, 
                                       MST_COMPANY.TAN_NO, 
                                       IIF(LEN(TRIM(MST_COMPANY.ADDRESS1)) <> 0,        TRIM(MST_COMPANY.ADDRESS1),'')  +
                                       IIF(LEN(TRIM(MST_COMPANY.ADDRESS2)) <> 0, ', ' + TRIM(MST_COMPANY.ADDRESS2),'')  +
                                       IIF(LEN(TRIM(MST_COMPANY.ADDRESS3)) <> 0, ' '  + TRIM(MST_COMPANY.ADDRESS3),'')  +
                                       IIF(LEN(TRIM(MST_COMPANY.ADDRESS4)) <> 0, ' '  + TRIM(MST_COMPANY.ADDRESS4),'')  +
                                       IIF(LEN(TRIM(MST_COMPANY.ADDRESS5)) <> 0, ' '  + TRIM(MST_COMPANY.ADDRESS5),'')  + ', ' + 
                                       TRIM(MST_STATE.STATE_NAME)         AS ADDRESS_WITH_STATE,
                                       IIF(LEN(MST_COMPANY.STD) <> 0 AND LEN( MST_COMPANY.PHONE) <> 0, 
                                           MST_COMPANY.STD +' -  '+ MST_COMPANY.PHONE,
                                           '')                            AS COMPANY_TEL_NO,
                                       MST_COMPANY.PIN_CODE, " +
                                       " '" + strSectionName  + "'        AS SECTION_CODE, " +
                                       cmnService.J_GetComboBoxItemId(ref cmbMinorHead,cmbMinorHead.SelectedIndex) + " AS MINOR_HEAD_ID, " +
                                       "Cdbl(" + dblIncomeTax        + ") AS INCOME_TAX, " +
                                       "Cdbl(" + dblFeeUnderSec234E  + ") AS FEE_UNDER_SEC_234E, " +
                                       "Cdbl(" + dblSurcharge        + ") AS SURCHARGE, " +
                                       "Cdbl(" + dblEduCess          + ") AS EDUCATION_CESS, " +
                                       "Cdbl(" + dblInterest         + ") AS INTEREST, " +
                                       "Cdbl(" + dblPenalty + ")          AS PENALTY, " +
                                       "Cdbl(" + dblTotalAmt + ")         AS TOTAL_AMOUNT," +
                                       //"" + dblTotalAmt + " AS TOTAL_AMOUNT," +
                                       " '" + cmnService.J_Inwords(dblTotalAmt) + "' AS TOTAL_AMOUNT_IN_WORDS, " +
                                       " '" + cmnService.J_ReplaceQuote(txtPaidCashOrDebitOrChequeNo.Text) + "' AS CHEQUE_NO, " +
                                       " '" + strDateofPayment + "' AS DATE_OF_PAYMENT," +
                                       " '" + txtDrawnOn.Text + "' AS DRAWN_ON," +
                                       " '" + txtTotalAmtFormat.Text + "' AS TOTAL_AMOUNT_FORMAT " +
                             @" FROM   MST_COMPANY, 
                                       MST_STATE 
                                WHERE  MST_COMPANY.STATE_ID = MST_STATE.STATE_ID
                                AND    MST_COMPANY.COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCompanyDetails, cmbCompanyDetails.SelectedIndex);
                else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @" SELECT '" + cmbTypeofPayment.Text + @"'     AS TAX_APPLICABLE_TYPE," +
                             @"        '" + strAssessmentYear + @"'         AS ASSESSMENT_YEAR,
                                       MST_COMPANY.COMPANY_ID, 
                                       MST_COMPANY.COMPANY_NAME, 
                                       MST_COMPANY.TAN_NO, 
                                       CASE WHEN LEN(MST_COMPANY.ADDRESS1) <> 0 THEN MST_COMPANY.ADDRESS1 ELSE '' END +
                                       CASE WHEN LEN(MST_COMPANY.ADDRESS2) <> 0 THEN ', ' + MST_COMPANY.ADDRESS2 ELSE '' END +
                                       CASE WHEN LEN(MST_COMPANY.ADDRESS3) <> 0 THEN ' '  + MST_COMPANY.ADDRESS3 ELSE '' END +
                                       CASE WHEN LEN(MST_COMPANY.ADDRESS4) <> 0 THEN ' '  + MST_COMPANY.ADDRESS4 ELSE '' END +
                                       CASE WHEN LEN(MST_COMPANY.ADDRESS5) <> 0 THEN ' '  + MST_COMPANY.ADDRESS5 ELSE '' END + ', ' +
                                       MST_STATE.STATE_NAME         AS ADDRESS_WITH_STATE,
                                       CASE WHEN LEN(MST_COMPANY.STD) <> 0 AND LEN( MST_COMPANY.PHONE) <> 0 THEN 
                                           MST_COMPANY.STD +' -  '+ MST_COMPANY.PHONE ELSE
                                           '' END                            AS COMPANY_TEL_NO,
                                       MST_COMPANY.PIN_CODE, " +
                                       " '" + strSectionName + "'         AS SECTION_CODE, " +
                                       cmnService.J_GetComboBoxItemId(ref cmbMinorHead, cmbMinorHead.SelectedIndex) + " AS MINOR_HEAD_ID, " +
                                       "" + dblIncomeTax + "        AS INCOME_TAX, " +
                                       "" + dblFeeUnderSec234E + "  AS FEE_UNDER_SEC_234E, " +
                                       "" + dblSurcharge + "        AS SURCHARGE, " +
                                       "" + dblEduCess + "          AS EDUCATION_CESS, " +
                                       "" + dblInterest + "         AS INTEREST, " +
                                       "" + dblPenalty + "          AS PENALTY, " +
                                       "" + dblTotalAmt + "         AS TOTAL_AMOUNT," +
                        //"" + dblTotalAmt + " AS TOTAL_AMOUNT," +
                                       " '" + cmnService.J_Inwords(dblTotalAmt) + "' AS TOTAL_AMOUNT_IN_WORDS, " +
                                       " '" + cmnService.J_ReplaceQuote(txtPaidCashOrDebitOrChequeNo.Text) + "'AS CHEQUE_NO, " +
                                       " '" + strDateofPayment + "'                  AS DATE_OF_PAYMENT," +
                                       " '" + txtDrawnOn.Text + "'                   AS DRAWN_ON," +
                                       " '" + txtTotalAmtFormat.Text + "' AS TOTAL_AMOUNT_FORMAT " +
                             @" FROM   MST_COMPANY, 
                                       MST_STATE 
                                WHERE  MST_COMPANY.STATE_ID = MST_STATE.STATE_ID
                                AND    MST_COMPANY.COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCompanyDetails, cmbCompanyDetails.SelectedIndex);

                ReportService rpt1 = new ReportService();
                //--------------------------------------------------------------
                crChallan281 rptChallan281 = new crChallan281();
                rptcls = (ReportClass)rptChallan281;
                //
                //TextObject objtxtValue;
                //objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[2].ReportObjects["txtNarrationText"];
                //objtxtValue.Text = ;
                //
                //rptService.J_SetTextToReport(ref rptcls, 2, "txtDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
                //
                rpt1.J_PreviewReport(ref rptcls, this, strSQL);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion 

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            //dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbForm_SelectedIndexChanged
        private void cmbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbForm.SelectedIndex <= 0)
            {
                cmbSection.Items.Clear();
                return;
            }

            strSQL = " SELECT SECTION_ID," +
                     "        SECTION_NO " +
                     " FROM   MST_SECTION " +
                     " WHERE  FORM_NAME  = '" + cmbForm.Text +"' ";

            strSQL = strSQL + " ORDER BY SECTION_ID";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
        }
        #endregion

        #region txtIncomeTax_TextChanged
        private void txtIncomeTax_TextChanged(object sender, EventArgs e)
        {

            dblIncomeTax = dblFeeUnderSec234E = dblSurcharge = dblEduCess = dblInterest = dblPenalty = 0;

            if (cmnService.J_IsNumeric(txtIncomeTax.Text) == true && txtIncomeTax.Text != "0.00")
                dblIncomeTax = Convert.ToDouble(txtIncomeTax.Text);

            if (cmnService.J_IsNumeric(txtFeeUnderSec234E.Text) == true && txtFeeUnderSec234E.Text != "0.00")
                dblFeeUnderSec234E = Convert.ToDouble(txtFeeUnderSec234E.Text);

            if (cmnService.J_IsNumeric(txtSurcharge.Text) == true && txtSurcharge.Text != "0.00")
                dblSurcharge = Convert.ToDouble(txtSurcharge.Text);

            if (cmnService.J_IsNumeric(txtEduCess.Text) == true && txtEduCess.Text != "0.00")
                dblEduCess = Convert.ToDouble(txtEduCess.Text);

            if (cmnService.J_IsNumeric(txtInterest.Text) == true && txtInterest.Text != "0.00")
                dblInterest = Convert.ToDouble(txtInterest.Text);

            if (cmnService.J_IsNumeric(txtPenalty.Text) == true && txtPenalty.Text != "0.00")
                dblPenalty = Convert.ToDouble(txtPenalty.Text);

            dblTotalAmt = dblIncomeTax + dblFeeUnderSec234E + dblSurcharge + dblEduCess + dblInterest + dblPenalty;
            txtTotalAmt.Text = string.Format("{0:0.00}", dblTotalAmt);
            txtTotalAmtFormat.Text = cmnService.J_Left(txtTotalAmt.Text, txtTotalAmt.Text.Length-3) ;

        }
        #endregion 

        #region mskDateOfPayment_TextChanged
        private void mskDateOfPayment_TextChanged(object sender, EventArgs e)
        {
            if (!dtService.J_IsDateValid(mskDateOfPayment) == true)
            {
                strDateofPayment = "null";
                return;
            }

            if (!dtService.J_IsBlankDateCheck(ref mskDateOfPayment, J_ShowMessage.NO))
                strDateofPayment = mskDateOfPayment.Text;
        }
        #endregion

        #endregion 

        #region User Define Functions

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

        #region NumericControl_Leave
        private void NumericControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "") txtBox.Text = "0";
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            strSQL = " SELECT ASST_ID," +
                     "        FA_YEAR " +
                     " FROM   MST_ASSESSMENT " +
                     " WHERE  VISIBILITY_FLAG = 0 " +
                     " ORDER BY ASST_ID DESC";

            if (dmlService.J_PopulateComboBox(strSQL, ref cmbAssessmentYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-------------------------------------------------------------------------------
            strSQL = " SELECT COMPANY_ID," +
                     "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                     " FROM   MST_COMPANY " +
                     " WHERE  INACTIVE_FLAG = 0 " +
                     " ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyDetails) == false) return;
            //-------------------------------------------------------------------------------
            //strSQL = " SELECT SECTION_ID," +
            //         "        SECTION_NO " +
            //         " FROM   MST_SECTION " +
            //         " WHERE  ASST_ID  <= " + Convert.ToInt32(Support.GetItemData(cmbAssessmentYear, cmbAssessmentYear.SelectedIndex)) + " " +
            //         " ORDER BY SECTION_ID";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbSection) == false) return;
            //-------------------------------------------------------------------------------
            strSQL = " SELECT MINOR_HEAD_ID," +
                     "        MINOR_HEAD_CODE + '-' + MINOR_HEAD_DESC AS MINOR " +
                     " FROM   MST_MINOR_HEAD " +
                     " ORDER BY MINOR_HEAD_ID";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbMinorHead,1, J_ComboBoxSelectedIndex.YES) == false) return;
            //-------------------------------------------------------------------------------
            //-- FORM NO
            string[] strQuarterWiseDeducteeReportForm ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ};
            dmlService.J_PopulateComboBox(strQuarterWiseDeducteeReportForm, ref cmbForm, 0);
            //-------------------------------------------------------------------------------
            //-- TYPE OF PAYMENT
            string[] strTypeofPayment ={ "(0020) Company Deductees", "(0021) Non-Company Deductees"};
            dmlService.J_PopulateComboBox(strTypeofPayment, ref cmbTypeofPayment, 1, J_ComboBoxSelectedIndex.YES);
            //-------------------------------------------------------------------------------
            txtIncomeTax.Text = "0.00";
            txtFeeUnderSec234E.Text = "0.00";
            txtSurcharge.Text = "0.00";
            txtEduCess.Text = "0.00";
            txtInterest.Text = "0.00";
            txtPenalty.Text = "0.00";
            txtTotalAmt.Text = "0.00";
        }
        #endregion

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            //BtnSort.BackColor = Color.LightGray;
            //BtnSort.Enabled = false;

            BtnAdd.BackColor = Color.LightGray;
            BtnAdd.Enabled = false;

            BtnEdit.BackColor = Color.LightGray;
            BtnEdit.Enabled = false;

            BtnCancel.BackColor = Color.LightGray;
            BtnCancel.Enabled = false;

            BtnSearch.BackColor = Color.LightGray;
            BtnSearch.Enabled = false;

            BtnDelete.BackColor = Color.LightGray;
            BtnDelete.Enabled = false;

            BtnRefresh.BackColor = Color.LightGray;
            BtnRefresh.Enabled = false;

            
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //-----------------------------------------------------------------------
                //ASSESSMENT YEAR
                //-----------------------------------------------------------------------
                if (cmbAssessmentYear.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Assessment Year");
                    cmbAssessmentYear.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                // COMPANY DETAILS
                //-----------------------------------------------------------------------
                if (cmbCompanyDetails.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Company");
                    cmbCompanyDetails.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //Form
                //-----------------------------------------------------------------------
                if (cmbForm.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Form");
                    cmbForm.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //Section 
                //-----------------------------------------------------------------------
                if (cmbSection.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Section");
                    cmbSection.Select();
                    return false;
                }
                //-----------------------------------------------------------------------
                //Payment Type
                //-----------------------------------------------------------------------
                if (cmbTypeofPayment.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Payment Type");
                    cmbTypeofPayment.Select();
                    return false;
                }

                //-----------------------------------------------------------------------
                //Payment Type
                //-----------------------------------------------------------------------
                if (cmbMinorHead.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a Minor Head");
                    cmbMinorHead.Select();
                    return false;
                }
                //-----------------------------------------------------------------------

                //***********************************************************************
                //-- DATE VALIDATIONS
                //***********************************************************************
                //----------------------------------------------
                // DATE OF PAYMENT
                //----------------------------------------------
                if (mskDateOfPayment.Text != "  /  /")
                {
                    if (dtService.J_IsDateValid(mskDateOfPayment) == false)
                    {
                        cmnService.J_UserMessage("Please enter a valid Payment Date.");
                        mskDateOfPayment.Select();
                        return false;
                    }
                }
                else
                {
                    cmnService.J_UserMessage("Please enter a Payment Date.");
                    mskDateOfPayment.Select();
                    return false;
                }


                //-------------------------------------------------------------------
                //--CHECKING FOR TOTAl AMOUNT LIMIT
                //-------------------------------------------------------------------
                if (dblTotalAmt > dblMaxTotalLimit)
                {
                    cmnService.J_UserMessage("Total amount cannot be greater than 99 Crores.");
                    txtIncomeTax.Select();
                    return false;
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

        private void txtPaidCashOrDebitOrChequeNo_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        //-- Added By Abhishek Dey On 21/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnPrintChallan281");
            Tan.ShowDialog();
            //--------------
            cmbCompanyDetails.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=x_EBVo-Eq9I");
            //V0012
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0012", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0072", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion
        
        #region txtIncomeTax_KeyPress
        private void txtIncomeTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtIncomeTax, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region txtFeeUnderSec234E_KeyPress
        private void txtFeeUnderSec234E_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtFeeUnderSec234E, "") == false)
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
        
        #region txtEduCess_KeyPress
        private void txtEduCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtEduCess, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region txtInterest_KeyPress
        private void txtInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtInterest, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region txtPenalty_KeyPress
        private void txtPenalty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtPenalty, "") == false)
                e.Handled = true;
        }
        #endregion
        
        #region cmbSection_SelectedIndexChanged
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSection.SelectedIndex<=0)
            {
                lblDisplaySection.Text = "";
                return;
            }
            //
            lblDisplaySection.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SECTION_DESCRIPTION FROM MST_SECTION WHERE SECTION_ID =  " + Convert.ToString(Convert.ToInt32(Support.GetItemData(cmbSection, cmbSection.SelectedIndex)))));
        }
        #endregion
        //-----------------------------------------
    }
}

