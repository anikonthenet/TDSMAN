#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnForm24QSalaryDetails
Version			: 1.0
Start Date		: 08-01-2011
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


namespace TDSMAN.FormTrn
{
    public partial class TrnForm24Q194PSalaryDetails : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnForm24Q194PSalaryDetails()
        {
            InitializeComponent();
            ////--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
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
        string strSQLGridViewTabPages;
        long lngBasicInfoID;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngEmployeeDetailID;
        long lngEmployeeID;
        string strSQLShowHelpEmployee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;

        //Added by Shrey Kejriwal on 03-04-2015
        bool blnShowRecord = false; //-- To Stop Text change events during show record

        bool RoundOff = false;
        //--            
        IDataReader drdShowEmployeePAN = null;
        ToolTip tllTip = new ToolTip();

        ToolTip tllTipPANLandlord1 = new ToolTip();
        ToolTip tllTipPANLandlord2 = new ToolTip();
        ToolTip tllTipPANLandlord3 = new ToolTip();
        ToolTip tllTipPANLandlord4 = new ToolTip();

        bool blnRestrictIncomeTaxCalculation = false;
        //
        bool blnVertical = false;
        //--            
        //----
        #region Tabbed_Mode
        private struct Tabbed_Mode
        {
            public const string Basic = "Basic Information";
            public const string Challan = "Challan Details";
            public const string Employee = "Employee Details";
            public const string GenerateTDS = "Generate TDS Return";
        }
        #endregion
        //----
        string strFormNo = T_FormNo.F24QSalaryDetails;
        int intTaxDeductedAtHigherRate = 0;
        double dblTaxableAmount = 0;
        double dblMAXTotalDeductibleAmount80CCE = 100000;
        double dblMAXTotalDeductibleAmount80CCE1415 = 150000;
        double dblMAXTotalDeductibleAmount80CCF1011_1112 = 20000;
        double dblMAXTotalDeductibleAmount80CCG1213 = 25000;
        //
        double dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 = 150000;
        //
        bool blnEnable1819Form16B = false;
        string strFocusTextBox = "";
        //--
        string strTlTpLandLordPAN = "Other than structurally valid PAN,\n it may contain 'GOVERNMENT', 'NONRESDENT' \nand 'OTHERVALUE'.";
        //--
        int intRentGrpY = 0; bool blResize = true;
        #endregion

        #region User Defined Events

        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            if (blResize == true)
                _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion

        #region TrnForm24QSalaryDetails_Load
        private void TrnForm24QSalaryDetails_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            GC.Collect();
            //
            //
            ErrProvider.Clear();
            //
            //intRentGrpY = grpRent.Location.Y;
            //-----------------------------------------------------------
            //lblTitle.Text = "Form " + T_FormNo.F24Q + " Salary Details - Annexure III";
            lblTitle.Text = "Section 194P - Annexure III";
            //-----------------------------------------------------------                
            txtFinancialYear.Text = TDSMAN.Classes.TDSMAN.T_pFinancialYear;
            txtQuarter.Text = TDSMAN.Classes.TDSMAN.T_pQuarter;
            txtCompany.Text = TDSMAN.Classes.TDSMAN.T_pCompanyName;
            //--
            if (TDSMAN.Classes.TDSMAN.T_pTokenNo != "")
                BtnSave.Visible = false;
            else
                BtnSave.Visible = true;
            //--
            ClearControlsHeader();
            //------------------
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            //{
                //-- 2019/05/11
                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId,
                                                    TDSMAN.Classes.TDSMAN.T_pQuarter,
                                                    TDSMAN.Classes.TDSMAN.T_pCompanyId,
                                                    TDSMAN.Classes.TDSMAN.T_pFormNo);
                //strSQL = "SELECT COUNT(*) FROM TRN_SALARY_DETAILS_194P WHERE FORM16B_NEW_FORMAT_18_19 = 0 AND BASIC_INFO_ID = " + lngBasicInfoID;
                //if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                //{
                //    blnEnable1819Form16B = false;
                //    BtnSave.Visible = false;
                //    txtForm16BNewFormatAlert.Visible = true;
                //}
                //else
                //{
                //    blnEnable1819Form16B = true;
                //    BtnSave.Visible = true;
                //    txtForm16BNewFormatAlert.Visible = false;
                //}
                //
                //if (blnEnable1819Form16B == false)
                //    BtnSave.Visible = false;
                //else
                //    BtnSave.Visible = true;
            //}
            //-------------------
            txtEmployeeName.Select();
            //
            ////if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2010_11ID)
            ////{
            ////    //txtSec80CCFGrossAmount.Enabled = true;
            ////    //txtSec80CCFDeductibleAmount.Enabled = true;
            ////    //## ANIK 2013/04/25
            ////    //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2010_11ID ||
            ////    //    TDSMAN.Classes.TDSMAN.T_pFinancialYearId == T_FinancialYearID.F2011_12ID)
            ////    //{
            ////    //    lblBSectionCaption.Text = "(B) Section 80CCF";
            ////    //    lblBSectionAlert.Text = "Section 80CCF Applicable for F.Y. 2010-11 && 2011-12";
            ////    //}
            ////    //else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2012_13ID)
            ////    //{
            ////    //    lblBSectionCaption.Text = "(B) Section 80CCG";
            ////    //    lblBSectionAlert.Text = "Section 80CCG Applicable for F.Y. 2012-13 onwards";
            ////    //}
            ////    //##
            ////}
            ////else
            ////{
            ////    //txtSec80CCFGrossAmount.Enabled = false;
            ////    //txtSec80CCFDeductibleAmount.Enabled = false;
            ////}
            //Added by dhrub on 26/11/2013 for Visibility of Additional Tax fields 
            ////if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
            ////{
            ////    //lblTDSDeduction.Visible = false;
            ////    //lblTaxDeductedInAllQtrs.Visible = false;
            ////    //lnkView.Visible = false;
            ////    ////
            ////    ////lnkTotalAmt.Visible = false;
            ////    ////grpAddedSalaryDetailFields.Visible = true;
            ////    ////grpBreakupBalance.Visible = true;
            ////    ////lnkViewDed.Visible = false;
            ////    //lnkTotalAmt.Visible = false;
            ////    //grpBreakupTDSDeducted.Visible = true;
            ////    ////lnkViewtaxCalculation.Visible = false;
            ////    //lblHigherRateCaption.Visible = true;
            ////    //cmbHigherRate.Visible = true;
            ////    ////-- 2016/09/21
            ////    //grpSuperannuationFund.Visible = true;
            ////    ////-- 2016/11/01
            ////    //grpRent.Visible = false;
            ////    //grpHouseProperty.Visible = false;
            ////    //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2016_17ID)
            ////    //{
            ////    //    grpRent.Visible = true;
            ////    //    grpHouseProperty.Visible = true;
            ////    //}
            ////}
            ////else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
            ////{
            ////    //lblTDSDeduction.Visible = true;
            ////    //lblTaxDeductedInAllQtrs.Visible = true;
            ////    //if (lblTaxDeductedInAllQtrs.Text.Trim() != "0.00")
            ////    //{
            ////    //    lnkView.Visible = true;
            ////    //}
            ////    ////grpAddedSalaryDetailFields.Visible = false;
            ////    ////grpBreakupBalance.Visible = false;
            ////    //grpBreakupTDSDeducted.Visible = false;
            ////    //lblHigherRateCaption.Visible = false;
            ////    //cmbHigherRate.Visible = false;
            ////    //-- REDESIGNING
            ////    grpDeductionsUS16.Location = new Point(grpDeductionsUS16.Location.X, grpDeductionsUS16.Location.Y - 101);
            ////    lblIncomeChargeableDesc.Location = new Point(lblIncomeChargeableDesc.Location.X, lblIncomeChargeableDesc.Location.Y - 101);
            ////    lblIncomeChargeable.Location = new Point(lblIncomeChargeable.Location.X, lblIncomeChargeable.Location.Y - 101);
            ////    //grpAddOtherIncome.Location = new Point(grpAddOtherIncome.Location.X, grpAddOtherIncome.Location.Y - 101);
            ////    lblGrossTotalIncomeDesc.Location = new Point(lblGrossTotalIncomeDesc.Location.X, lblGrossTotalIncomeDesc.Location.Y - 101);
            ////    lblGrossTotalIncome.Location = new Point(lblGrossTotalIncome.Location.X, lblGrossTotalIncome.Location.Y - 101);
            ////    //grpDedcutionsUCVIA.Location = new Point(grpDedcutionsUCVIA.Location.X, grpDedcutionsUCVIA.Location.Y - 101);
            ////    lblTotalIncomeDesc.Location = new Point(lblTotalIncomeDesc.Location.X, lblTotalIncomeDesc.Location.Y - 101);
            ////    lblTotalIncome.Location = new Point(lblTotalIncome.Location.X, lblTotalIncome.Location.Y - 101);
            ////    grpTaxPayable.Location = new Point(grpTaxPayable.Location.X, grpTaxPayable.Location.Y - 101);
            ////    //
            ////    //lblShortfallDesc.Location = new Point(lblShortfallDesc.Location.X, lblShortfallDesc.Location.Y - 78);
            ////    //lblShortfall.Location = new Point(lblShortfall.Location.X, lblShortfall.Location.Y - 78);
            ////    grpTaxPayable.Height = 315;
            ////    //--  2016/09/21
            ////    //grpSuperannuationFund.Visible = false;
            ////    ////-- 2016/11/01
            ////    //grpRent.Visible = false;
            ////    //grpHouseProperty.Visible = false;

            ////}
            //
            //////DISABLE_DESIGN_ANNEXTURE_18_19();
            //////-- 2018/11/12 - Section 16(ia)
            ////if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            ////{
            ////    lblDedTaxSection16iaDesc.Visible = true;
            ////    txtDedTaxSection16ia.Visible = true;
            ////    lblAggregateUnderSec16Desc.Text = "5. Aggregate of 4(a) & (b) & (c)";
            ////    //--
            ////    lblEducationCessDesc.Text = "14. Education Cess @ 4% (on tax computed at Srl No. 12)";
            ////    //-- 2019/07/23
            ////    lblRebateCaption.Visible = true;
            ////    lblRebateCalculated.Visible = true;
            ////    txtRebate.Visible = true;
            ////    lblTaxDeductingRebateCalculatedCaption.Visible = true;
            ////    lblTaxDeductingRebateCalculated.Visible = true;
            ////    lblTaxDeductingRebate.Visible = true;
            ////}
            ////else
            ////{
            ////    lblEducationCessDesc.Text = "14. Education Cess @ 3% (on tax computed at Srl No. 12)";
            ////    //-- 2019/07/23
            ////    lblRebateCaption.Visible = false;
            ////    lblRebateCalculated.Visible = false;
            ////    txtRebate.Visible = false;
            ////    lblTaxDeductingRebateCalculatedCaption.Visible = false;
            ////    lblTaxDeductingRebateCalculated.Visible = false;
            ////    lblTaxDeductingRebate.Visible = false;
            ////    lblTaxTotalIncomeDesc.Text = "12.     Tax on Total Income";
            ////}
            //--
            //lnkView.Visible = false;
            //
            rbnEntryOptions_CheckedChanged(sender, e);
            //
            //mskEmployeeFromDate.Text = TdsMan.T_ReturnStartDateFinancialYear(txtFinancialYear.Text);
            //mskEmployeeToDate.Text = TdsMan.T_ReturnEndDateFinancialYear(txtFinancialYear.Text);
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            btnReport.BackColor = Color.Lavender;
            //
            //-- 2020/07/01
            chkTaxation115BAC.Visible = false;
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2020_21ID)
                chkTaxation115BAC.Visible = true;
            //--
            BtnAdd_Click(sender, e);
            //            
            //-----------------------------------------------
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId,
                                                        TDSMAN.Classes.TDSMAN.T_pQuarter,
                                                        TDSMAN.Classes.TDSMAN.T_pCompanyId,
                                                        TDSMAN.Classes.TDSMAN.T_pFormNo);
            if (lngBasicInfoID > 0)
            {
                LoadSalaryDetailsGrid(lngBasicInfoID);
                //-- Generate Salary Detail Srl No.
                txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
            }
            else
                txtSalaryDetailSrlNo.Text = "1";
            //
            BtnDelete.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            //-- 2015/11/18 @ ANIK GHOSH
            if (TDSMAN.Classes.TDSMAN.T_EnableRoundOffTaxableAmt == true)
            {
                chkRoundOff.Visible = true;
                chkRoundOff.Checked = true;
            }
            else
            {
                chkRoundOff.Visible = false;
                chkRoundOff.Checked = false;
            }
            //-- 2016/09/01
            if (TDSMAN.Classes.TDSMAN.T_EnableLockReturn == true)
            {
                if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT RECEIPT_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID)) != "")
                {
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                }
            }
            //--
            //-- ANIK @ 2020/05/30
            if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
            {
                lblEmpRefNo.Visible = true;
                txtEmpRefNo.Visible = true;
                //
                txtRefernceNoSearch.Visible = true;
                lblRefernceNoSearch.Visible = true;
            }
            else
            {
                lblEmpRefNo.Visible = false;
                txtEmpRefNo.Visible = false;
                //
                txtRefernceNoSearch.Visible = false;
                lblRefernceNoSearch.Visible = false;
            }
            //--
        }
        #endregion

        #region BtnAdd_Click

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                //BackgroundColorChangeChallan(lblMode.Text);
                //---------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                lblMode.ForeColor = Color.Yellow;
                //---------------------------------------------
                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;
                //--
                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;
                //--
                BtnDelete.Enabled = true;
                BtnDelete.BackColor = Color.Lavender;
                //--
                BtnSearch.Enabled = true;
                BtnSearch.BackColor = Color.Lavender;
                //--
                BtnRefresh.Enabled = true;
                BtnRefresh.BackColor = Color.Lavender;
                //---------------------------------------------
                strCheckFields = "";
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnEdit_Click

        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            // Added by Dhrub 30-10-2013
            //lblTaxDeductedInAllQtrs.Text = "0.00";
            try
            {
                if (dgcViewSalaryDetails.CurrentRow.Index >= 0)
                {
                    //--------------------------------------------------
                    ClearControls();
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    //A particular ID wise retriving the data from database
                    //if (ShowRecord(Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails[dgcViewSalaryDetails.CurrentRow.Index, 0]))) == false)
                    if (ShowRecord(Convert.ToInt64(dgcViewSalaryDetails.Rows[dgcViewSalaryDetails.CurrentRow.Index].Cells[0].Value)) == false)
                    {
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                    }
                    blnRestrictIncomeTaxCalculation = false;
                    lblTotalIncome_TextChanged(sender, e);
                    //--------------------------------------------------
                    //lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    //-- 2016/09/01
                    if (TDSMAN.Classes.TDSMAN.T_EnableLockReturn == true)
                    {
                        if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT RECEIPT_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID)) != "")
                        {
                            BtnSave.Enabled = false;
                            BtnSave.BackColor = Color.LightGray;
                        }
                    }
                    //
                    BackgroundColorChangeChallan(lblMode.Text);
                    //--------------------------------------------------
                    strCheckFields = "";
                    //--------------------------------------------------
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                }


                //Added by Dhrub on 30/10/2013

                if (lblTotalTaxableIncome.Text == "") return;
                #region SUM OF DEDUCTION INTO FOUR QUARTERS
                //double dblTaxDeductedInAllQtrs = TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount);
                //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                //{
                //    //lblTaxableAmountfrmDB.Text = string.Format("{0:0.00}", dblTaxableAmount);
                //    lblEmployeeDeductionAmtfrmDB.Text = string.Format("{0:0.00}", dblTaxDeductedInAllQtrs);
                //}
                //else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
                //{
                //    lblTaxDeductedInAllQtrs.Text = string.Format("{0:0.00}", dblTaxDeductedInAllQtrs);
                //}

                #endregion
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSave_Click

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
        }

        #endregion

        #region BtnDelete_Click

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Edit)
            //    return;

            //lblMode.Text = J_Mode.Delete;

            //grpSearch.Visible = false;

            ////Added by Indrajit on 02-03-2013
            //if (dgcViewSalaryDetails.CurrentRowIndex < 0)
            //{
            //    cmnService.J_UserMessage(J_Msg.DataNotFound);
            //    if (dsetGridClone == null) return;
            //    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "SALARY_DETAILS_ID", lngSearchId);
            //    return;
            //}
            ////===============================
            //Insert_Update_Delete_Data();
            //dgcViewSalaryDetails_Click(sender, e);
        }

        #endregion

        #region BtnDelete_MouseClick
        private void BtnDelete_MouseClick(object sender, MouseEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2018_19ID)
            {
                if (lblMode.Text == J_Mode.Edit)
                    return;

                lblMode.Text = J_Mode.Delete;

                grpSearch.Visible = false;

                //Added by Indrajit on 02-03-2013
                if (dgcViewSalaryDetails.CurrentRow.Index < 0)
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                    return;
                }
                //===============================
                Insert_Update_Delete_Data();
                dgcViewSalaryDetails_Click(sender, e);
                return;
            }
            //--
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID && blnEnable1819Form16B == false)
            {
                if (e.Button == MouseButtons.Left)
                    cntxtMnuGrp.Show(BtnDelete, new Point(e.X, e.Y));
            }
            else if (blnEnable1819Form16B == true)
            {
                if (lblMode.Text == J_Mode.Edit)
                    return;

                lblMode.Text = J_Mode.Delete;

                grpSearch.Visible = false;

                //Added by Indrajit on 02-03-2013
                if (dgcViewSalaryDetails.CurrentRow.Index < 0)
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                    return;
                }
                //===============================
                Insert_Update_Delete_Data();
                dgcViewSalaryDetails_Click(sender, e);
            }
        }

        #endregion

        #region tlStrpMnuSingle_Click
        private void tlStrpMnuSingle_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit)
                return;

            lblMode.Text = J_Mode.Delete;

            grpSearch.Visible = false;

            //Added by Indrajit on 02-03-2013
            if (dgcViewSalaryDetails.CurrentRow.Index < 0)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                return;
            }
            //===============================
            Insert_Update_Delete_Data();
            dgcViewSalaryDetails_Click(sender, e);
        }
        #endregion

        #region tlStrpMnuBatch_Click
        private void tlStrpMnuBatch_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit)
                return;
            if (cmnService.J_UserMessage("This will Delete all the records present in this grid. Proceed for Deletion?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                lblMode.Text = J_Mode.Delete;

                grpSearch.Visible = false;

                //Added by Indrajit on 02-03-2013
                if (dgcViewSalaryDetails.CurrentRow.Index < 0)
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                    return;
                }
                //===============================
                //Insert_Update_Delete_Data();
                dmlService.J_BeginTransaction();
                strSQL = "DELETE FROM TRN_SALARY_DETAILS_194P WHERE BASIC_INFO_ID =  " + lngBasicInfoID + "";
                //-----------------------------------------------------------
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    lblMode.Text = J_Mode.Add;
                    dmlService.J_Rollback();
                    return;
                }
                dmlService.J_Commit();
                //
                ////dgcViewSalaryDetails_Click(sender, e);
                LoadSalaryDetailsGrid(lngBasicInfoID);
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                BackgroundColorChangeChallan(lblMode.Text);
                //-- 2016/09/01
                if (TDSMAN.Classes.TDSMAN.T_EnableLockReturn == true)
                {
                    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT RECEIPT_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID)) != "")
                    {
                        BtnSave.Enabled = false;
                        BtnSave.BackColor = Color.LightGray;
                    }
                }
                //-------------------------------------------
                BtnRefresh.Enabled = true;
                BtnRefresh.BackColor = Color.Lavender;
                //
                BtnSearch.Enabled = true;
                BtnSearch.BackColor = Color.Lavender;
                //
                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;
                //--
                BtnDelete.Enabled = false;
                BtnDelete.BackColor = Color.LightGray;
                //--
                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ClearControls();					//Clear all the Controls
                rbnMandatoryOptions.Enabled = true;
                //-------------------------------------------
                txtEmployeeName.Text = "";
                txtEmployeePAN.Text = "";
                txtEmpRefNo.Text = "";
                //--
                string[] strEmployeeCategory = { T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
                dmlService.J_PopulateComboBox(strEmployeeCategory, ref cmbEmployeeCategory, 1, J_ComboBoxSelectedIndex.YES);
                //--------------------            
                //mskEmployeeFromDate.Text = TdsMan.T_ReturnStartDateFinancialYear(txtFinancialYear.Text);
                //mskEmployeeToDate.Text = TdsMan.T_ReturnEndDateFinancialYear(txtFinancialYear.Text);

                strCheckFields = "";
                //-------------------------------------------
                strSQL = strQuery + "order by " + strOrderBy;
                //-------------------------------------------
                //--
                txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                //--
                if (cmnService.J_ReturnInt32Value(txtSalaryDetailSrlNo.Text) > 1)
                {
                    if (dsetGridClone != null) dsetGridClone.Clear();
                    dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                    if (dsetGridClone == null) return;
                }
                //---

                //---
                //if (dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "SALARY_DETAILS_ID", lngSearchId) == false)
                //    BtnAdd.Select();
                txtEmployeeName.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblSearchMode.Text = J_Mode.Searching;
                //-------------------------------------------
                if (ValidateFields() == false) return;
                //-------------------------------------------
                grpSort.Visible = false;
                grpSearch.Visible = true;
                //-------------------------------------------
                txtSlNoSearch.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------------------------------
                if (ValidateFields() == false) return;
                strCheckFields = "";
                //-------------------------------------------------------------------
                //--- Storing the Criteria Fiels & Values ---------------------------
                //-------------------------------------------------------------------
                //-- sl no 
                //-------------------------------------------------------------------
                if (txtSlNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND TRN_SALARY_DETAILS_194P.SL_NO = " + cmnService.J_ReturnInt32Value(txtSlNoSearch.Text.Trim()) + " ";
                //-------------------------------------------------------------------
                //-- name 
                //-------------------------------------------------------------------
                if (txtEmployeeNameSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_EMPLOYEE.EMPLOYEE_NAME like '%" + cmnService.J_ReplaceQuote(txtEmployeeNameSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- pan 
                //-------------------------------------------------------------------
                if (txtPANSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_EMPLOYEE.EMPLOYEE_PAN like '%" + cmnService.J_ReplaceQuote(txtPANSearch.Text.Trim().ToUpper()) + "%' ";
                //-------------------------------------------------------------------
                //-- amount
                //-------------------------------------------------------------------
                if (txtTotalDeductedSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND TRN_SALARY_DETAILS_194P.NET_TAX_PAYABLE = " + cmnService.J_ReturnDoubleValue(txtTotalDeductedSearch.Text.Trim()) + " ";
                //----------------------------------------------------------------------
                //-- REFERENCE NO //-- 2020/05/30
                //---------------------------------------------------------------------
                if (txtRefernceNoSearch.Text.Trim() != "")
                    strCheckFields = strCheckFields + "AND MST_EMPLOYEE.EMPLOYEE_REF LIKE '" + cmnService.J_ReplaceQuote(txtRefernceNoSearch.Text.Trim().ToUpper()) + "%' ";
                //----------------------------------------------------------------------
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                if (dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId) == false)
                {
                    txtSlNoSearch.Select();
                    return;
                }
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                //----------------------------------------------------------------------
                grpSearch.Visible = false;
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                //----------------------------------------------------------------------

                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //----------------------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //----------------------------------------------------------------------
                dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                //----------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.Add;
            //-----------------------------------------------------------
            ClearControls();
            //-----------------------------------------------------------
            //if (dgcViewChallan.Visible == true)
            BackgroundColorChangeChallan(lblMode.Text);

            grpSearch.Visible = false;

            strCheckFields = "";
            strSQL = strQuery + "order by " + strOrderBy;
            //--
            txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
            //--
            if (cmnService.J_ReturnInt32Value(txtSalaryDetailSrlNo.Text) > 1)
            {
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
            }
            //----------------------------------------------------------------------
            //if (dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "SALARY_DETAILS_ID", lngSearchId) == false)
            //{
            //    txtSlNoSearch.Select();
            //    return;
            //}
            //--
            //txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);                
            //-----------------------------
            // Added by Dhrub on 30/10/2013
            #region SUM OF DEDUCTION INTO FOUR QUARTERS
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
            {
                //lblEmployeeDeductionAmtfrmDB.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                //lblTaxableAmountfrmDB.Text = string.Format("{0:0.00}", dblTaxableAmount);
                //txtTaxableAmount.Text = string.Format("{0:0.00}", dblTaxableAmount);
                //txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, out dblTaxableAmount));
            }
            else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
            {
                //lblTaxDeductedInAllQtrs.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
            }
            #endregion

        }
        #endregion

        #region btnReport_Click
        private void btnReport_Click(object sender, EventArgs e)
        {
            //--
            //TDSMAN.Classes.TDSMAN.T_pCompanyId = Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
            //TDSMAN.Classes.TDSMAN.T_pFinancialYearId = Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex));
            //--
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.EmployeesSDNotFound, "Employee(s) - Missing in Salary Details");
        }
        #endregion

        #region dgcViewSalaryDetails_Click

        private void dgcViewSalaryDetails_Click(object sender, EventArgs e)
        {
            if (dgcViewSalaryDetails.CurrentRow == null)
            {
                return;
            }

            if (Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails.CurrentRow.Index)) < 0)
            {
                BtnAdd.Focus();
                return;
            }

            if (lblMode.Text != J_Mode.Edit)
                lngSearchId = Convert.ToInt64(dgcViewSalaryDetails.Rows[dgcViewSalaryDetails.CurrentRow.Index].Cells[0].Value); //Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails[dgcViewSalaryDetails.CurrentRow.Index, 0]));

            if (lngSearchId > 0)
            {
                if (lblMode.Text == J_Mode.Add)
                {
                    //
                    BtnEdit.Enabled = true;
                    BtnEdit.BackColor = Color.Lavender;
                    //
                    BtnDelete.Enabled = true;
                    BtnDelete.BackColor = Color.Lavender;
                }
            }
            else
            {
                BtnEdit.Enabled = false;
                BtnEdit.BackColor = Color.LightGray;
            }
            //dgcViewSalaryDetails.Select(dgcViewSalaryDetails.CurrentRow.Index);
            //dgcViewSalaryDetails.Select();
            //dgcViewSalaryDetails.Focus();
        }

        #endregion

        #region dgcViewSalaryDetails_DoubleClick
        private void dgcViewSalaryDetails_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region dgcViewSalaryDetails_KeyDown
        private void dgcViewSalaryDetails_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (dgcViewSalaryDetails.CurrentRow.Index == -1) return;
                //lngSearchId = Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails[dgcViewSalaryDetails.CurrentRow.Index, 0]));
                lngSearchId = Convert.ToInt64(dgcViewSalaryDetails.Rows[dgcViewSalaryDetails.CurrentRow.Index].Cells[0].Value);
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewSalaryDetails_CurrentCellChanged
        private void dgcViewSalaryDetails_CurrentCellChanged(object sender, System.EventArgs e)
        {
            //if (lblMode.Text != J_Mode.Edit)
            //    lngSearchId = Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails[dgcViewSalaryDetails.CurrentRow.Index, 0]));

            if (lblMode.Text != J_Mode.Edit)
            {
                dgcViewSalaryDetails_Click(sender, e);
            }
        }
        #endregion

        #region dgcViewSalaryDetails_MouseUp
        private void dgcViewSalaryDetails_MouseUp(object sender, MouseEventArgs e)
        {
            dgcViewSalaryDetails_Click(sender, e);
        }
        #endregion

        #region dgcViewSalaryDetails_MouseMove
        private void dgcViewSalaryDetails_MouseMove(object sender, MouseEventArgs e)
        {
            //cmnService.J_GridToolTip(dgcViewSalaryDetails, e.X, e.Y);
        }
        #endregion

        #region dgcViewSalaryDetails_MouseClick
        private void dgcViewSalaryDetails_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewSalaryDetails_Click(sender, e);
        }
        #endregion

        #region txtEmployeeName_TextChanged
        private void txtEmployeeName_TextChanged(object sender, EventArgs e)
        {
            IDataReader drdShowEmployeeHelp = null;
            //--
            try
            {
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
                if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
                {
                    strSQLShowHelpEmployee = "SELECT EMPLOYEE_ID," +
                       "                            EMPLOYEE_NAME," +
                       "                            EMPLOYEE_PAN," +
                       "                            EMPLOYEE_REF " +
                       "                     FROM   MST_EMPLOYEE," +
                       "                            MST_COMPANY " +
                       "                     WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                       "                     AND    MST_COMPANY.COMPANY_ID  = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                       "                     AND    MST_EMPLOYEE.EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "%' " +
                       "                     AND    MST_EMPLOYEE.INACTIVE_FLAG = 0 " +
                       "                     AND    MST_EMPLOYEE.CATEGORY IN ('S','O') " +
                       "                     ORDER BY EMPLOYEE_NAME, EMPLOYEE_ID";
                }
                else
                {
                    strSQLShowHelpEmployee = "SELECT EMPLOYEE_ID," +
                    "                            EMPLOYEE_NAME," +
                    "                            EMPLOYEE_PAN," +
                    "                            " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS CATEGORY " +
                    "                     FROM   MST_EMPLOYEE," +
                    "                            MST_COMPANY " +
                    "                     WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "                     AND    MST_COMPANY.COMPANY_ID  = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                    "                     AND    MST_EMPLOYEE.EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "%' " +
                    "                     AND    MST_EMPLOYEE.INACTIVE_FLAG = 0 " +
                    "                     AND    MST_EMPLOYEE.CATEGORY IN ('S','O') " +
                    "                     ORDER BY EMPLOYEE_NAME, EMPLOYEE_ID";
                }
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
                        if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
                            lstEmployeeHelp.Items.Add(new ListBoxItem(drdShowEmployeeHelp["EMPLOYEE_NAME"].ToString().PadRight(45) + drdShowEmployeeHelp["EMPLOYEE_PAN"].ToString().PadRight(25) + drdShowEmployeeHelp["EMPLOYEE_REF"]));
                        else
                            lstEmployeeHelp.Items.Add(new ListBoxItem(drdShowEmployeeHelp["EMPLOYEE_NAME"].ToString().PadRight(45) + drdShowEmployeeHelp["EMPLOYEE_PAN"].ToString().PadRight(13) + drdShowEmployeeHelp["CATEGORY"]));
                        //--
                        //if (lstEmployeeHelp.Height <= 420)
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

                // Added by Dhrub on 30-10-2013
                #region SUM OF DEDUCTION INTO FOUR QUARTERS
                if (txtEmployeeName.Text == "" || txtEmployeePAN.Text == "") return;
                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                {
                    //lblEmployeeDeductionAmtfrmDB.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                    //lblTaxableAmountfrmDB.Text = string.Format("{0:0.00}", dblTaxableAmount);
                    //txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, out dblTaxableAmount));
                    //txtTaxableAmount.Text = string.Format("{0:0.00}", dblTaxableAmount);
                }
                else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
                {
                    //lblTaxDeductedInAllQtrs.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                }
                #endregion

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
            if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
            {
                txtEmployeePAN.Text = cmnService.J_Mid(strEmployeeHelp, 45, 10);
                txtEmployeeName.Text = cmnService.J_Left(strEmployeeHelp, 45).Trim();
                txtEmpRefNo.Text = strEmployeeHelp.Substring(58).Trim();
            }
            else
            {
                txtEmployeePAN.Text = cmnService.J_Mid(strEmployeeHelp, 45, 10);
                txtEmployeeName.Text = cmnService.J_Left(strEmployeeHelp, 45).Trim();
                cmbEmployeeCategory.Text = strEmployeeHelp.Substring(58).Trim();
            }
            //--
            lstEmployeeHelp.Visible = false;
            //--
            txtEmployeePAN.Select();
        }
        #endregion

        #region lstEmployeeHelp_KeyPress
        private void lstEmployeeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstEmployeeHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstEmployeeHelp.Visible = false;
                txtEmployeeName.Select();
            }

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

        #region txtEmployeePAN_TextChanged
        private void txtEmployeePAN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeePAN.Text != "PANNOTAVBL")
                {
                    //    //grpRemarks.Enabled = false;
                    //    rbnHigherRate.Checked = true;
                    //    lblMsgDisplay.Visible = true;
                    //    lblMsgDisplay.Text = "Higher rate of TDS applicable as PANNOTAVBL.";
                    //}
                    //else
                    //{
                    //grpRemarks.Enabled = true;
                    //rbnHigherRate.Checked = false;
                    //rbnNormal.Checked = true;

                    //lblMsgDisplay.Visible = false;
                    //lblMsgDisplay.Text = "";
                    if (txtEmployeeName.Text.Trim() == "") return; //-- 2019/04/27
                    // TO CHECK THAT DEDUCTEE EXISTS OF THAT SAME VALID PAN
                    if (lstEmployeeHelp.Visible == true) return;
                    if (txtEmployeePAN.Text.Length != 10)
                    {
                        lstPANHelp.Visible = false;
                        return;
                    }
                    if (blnShowPANHelp == false) return;
                    //--
                    //-----------------------
                    //strSQL = "SELECT EMPLOYEE_ID," +
                    //    "            EMPLOYEE_NAME " +
                    //    "     FROM   MST_EMPLOYEE " +
                    //    "     WHERE  EMPLOYEE_PAN   = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "' " +
                    //    "     AND    EMPLOYEE_NAME <> '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "' " +
                    //    "     ORDER BY EMPLOYEE_NAME";
                    //-- 2019/04/27
                    strSQL = "SELECT EMPLOYEE_ID," +
                        "            EMPLOYEE_NAME " +
                        "     FROM   MST_EMPLOYEE," +
                        "            MST_COMPANY " +
                        "     WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                        "     AND    MST_COMPANY.COMPANY_ID  = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                        "     AND    EMPLOYEE_PAN   = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "' " +
                        "     AND    EMPLOYEE_NAME <> '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "' " +
                        "     ORDER BY EMPLOYEE_NAME";
                    drdShowEmployeePAN = dmlService.J_ExecSqlReturnReader(dmlService.J_pCommand, strSQL);
                    //--
                    if (drdShowEmployeePAN == null)
                    {
                        lstPANHelp.Visible = false;
                        drdShowEmployeePAN.Close();
                        drdShowEmployeePAN.Dispose();
                        return;
                    }
                    else
                    {
                        lstPANHelp.Items.Clear();
                        //lstPANHelp.Height = 20;
                        lstPANHelp.Visible = true;
                        lstPANHelp.Items.Add(new ListBoxItem("Employee(s) exist with same PAN - "));
                        while (drdShowEmployeePAN.Read())
                        {
                            lstPANHelp.Items.Add(new ListBoxItem(drdShowEmployeePAN["EMPLOYEE_NAME"].ToString()));
                            //--
                            //if (lstPANHelp.Height <= 260)
                            //    lstPANHelp.Height = lstPANHelp.Height + 19;
                        }
                        //--
                        drdShowEmployeePAN.Close();
                        drdShowEmployeePAN.Dispose();
                        //--
                        if (lstPANHelp.Items.Count <= 1)
                            lstPANHelp.Visible = false;
                    }
                }

                //--------------------------------
                // Added by Dhrub on 30-10-2013
                if (txtEmployeeName.Text == "" || txtEmployeePAN.Text == "") return;

                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                {
                    //lblEmployeeDeductionAmtfrmDB.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                    //lblTaxableAmountfrmDB.Text = string.Format("{0:0.00}", dblTaxableAmount);
                    //txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, out dblTaxableAmount));
                    //txtTaxableAmount.Text = string.Format("{0:0.00}", dblTaxableAmount);
                }
                else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
                {
                    //lblTaxDeductedInAllQtrs.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                }

                //----

            }
            catch (Exception err_handler)
            {
                drdShowEmployeePAN.Close();
                drdShowEmployeePAN.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }

        #endregion

        #region txtEmployeePAN_Enter

        private void txtEmployeePAN_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text.Trim() == "") return; //-- 2019/04/27
                lstEmployeeHelp.Visible = false;
                // TO CHECK THAT DEDUCTEE EXISTS OF THAT SAME VALID PAN
                //if (lstDeducteeHelp.Visible == true) return;
                if (txtEmployeePAN.Text.Length != 10)
                {
                    lstPANHelp.Visible = false;
                    return;
                }
                if (txtEmployeePAN.Text == "PANNOTAVBL") return;
                if (blnShowPANHelp == false) return;
                //--
                //-----------------------
                //strSQL = "SELECT EMPLOYEE_ID," +
                //        "        EMPLOYEE_NAME " +
                //        "FROM    MST_EMPLOYEE " +
                //        "WHERE   EMPLOYEE_PAN   = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "' " +
                //        "AND     EMPLOYEE_NAME <> '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "' " +
                //        "ORDER BY EMPLOYEE_NAME";
                //-- 2019/04/27
                strSQL = "SELECT EMPLOYEE_ID," +
                    "            EMPLOYEE_NAME " +
                    "     FROM   MST_EMPLOYEE," +
                    "            MST_COMPANY " +
                    "     WHERE  MST_EMPLOYEE.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "     AND    MST_COMPANY.COMPANY_ID  = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                    "     AND    EMPLOYEE_PAN   = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "' " +
                    "     AND    EMPLOYEE_NAME <> '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "' " +
                    "     ORDER BY EMPLOYEE_NAME";
                drdShowEmployeePAN = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowEmployeePAN == null)
                {
                    lstPANHelp.Visible = false;
                    drdShowEmployeePAN.Close();
                    drdShowEmployeePAN.Dispose();
                    return;
                }
                else
                {
                    lstPANHelp.Items.Clear();
                    //lstPANHelp.Height = 20;
                    lstPANHelp.Visible = true;
                    lstPANHelp.Items.Add(new ListBoxItem("Employee(s) exist with same PAN - "));
                    while (drdShowEmployeePAN.Read())
                    {
                        lstPANHelp.Items.Add(new ListBoxItem(drdShowEmployeePAN["EMPLOYEE_NAME"].ToString()));
                        //--
                        //if (lstPANHelp.Height <= 260)
                        //    lstPANHelp.Height = lstPANHelp.Height + 19;
                    }
                    //--
                    drdShowEmployeePAN.Close();
                    drdShowEmployeePAN.Dispose();
                    //--
                    if (lstPANHelp.Items.Count <= 1)
                        lstPANHelp.Visible = false;
                }

                // Added by Dhrub on 30-10-2013
                if (txtEmployeeName.Text == "" || txtEmployeePAN.Text == "") return;
                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                {
                    //lblEmployeeDeductionAmtfrmDB.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                    //lblTaxableAmountfrmDB.Text = string.Format("{0:0.00}", dblTaxableAmount);
                    //txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, out dblTaxableAmount));
                    //txtTaxableAmount.Text = string.Format("{0:0.00}", dblTaxableAmount);
                }
                else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2013_14ID)
                {
                    //lblTaxDeductedInAllQtrs.Text = string.Format("{0:0.00}", TdsMan.T_GetDeductedAmount(TDSMAN.Classes.TDSMAN.T_pFormNo, txtEmployeeName.Text, txtEmployeePAN.Text, TDSMAN.Classes.TDSMAN.T_pCompanyId, out dblTaxableAmount));
                }
                //----
            }
            catch (Exception err_handler)
            {
                drdShowEmployeePAN.Close();
                drdShowEmployeePAN.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }

        #endregion

        #region txtEmployeePAN_Leave
        private void txtEmployeePAN_Leave(object sender, EventArgs e)
        {
            if (txtEmployeePAN.Text.Trim() == "") txtEmployeePAN.Text = "PANNOTAVBL";
            lstPANHelp.Visible = false;
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
            {
                //if (txtEmployeePAN.Text == "PANNOTAVBL")
                //{
                //    cmbHigherRate.Text = "Yes";
                //}
                //else if (txtEmployeePAN.Text != "PANNOTAVBL")
                //{
                //    cmbHigherRate.Text = "No";
                //}
            }
        }
        #endregion

        #region cmbEmployeeCategory_KeyPress
        private void cmbEmployeeCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskEmloyeeFromDate_KeyPress
        private void mskEmloyeeFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskEmloyeeToDate_KeyPress
        private void mskEmloyeeToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region rbnEntryOptions_CheckedChanged
        #region MOVING
        //private void rbnEntryOptions_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbnMandatoryOptions.Checked == true)
        //    {
        //        grpTotalSalaryDetails.Visible = false;

        //        lblTotalSalaryBalanceDesc.Text = "Total Salary";
        //        lblTotalSalaryBalanceDesc.ForeColor = Color.Black;
        //        lblTotalSalaryBalanceDesc.Location = new Point(18, 89);

        //        txtTotalSalaryBalance.Location = new Point(125, 89);

        //        grpDeductionsUS16.Location = new Point(16, 114);
        //        grpDeductionsUS16.Text = "Deductions under section 16";

        //        lblAggregate4_a_bDesc.Text = "Aggregate of 4(a) && (b)";

        //        lblIncomeChargeableDesc.Location = new Point(19, 223);
        //        lblIncomeChargeableDesc.Text = "Income chargeable under the head 'Salaries' (3-5)";

        //        lblIncomeChargeable.Location = new Point(328, 223);

        //        grpAddOtherIncome.Visible = false;

        //        lblTotalOtherIncomeDesc.Location = new Point(19, 250);
        //        lblTotalOtherIncomeDesc.Text = "Any Income other than Salary";
        //        lblTotalOtherIncomeDesc.ForeColor = Color.Black;

        //        txtTotalOtherIncome.Location = new Point(197, 250);

        //        lblGrossTotalIncomeDesc.Location = new Point(19, 277);
        //        lblGrossTotalIncomeDesc.Text = "Gross Total Income";

        //        lblGrossTotalIncome.Location = new Point(198, 277);

        //        grpDedcutionsUCVIA.Location = new Point(14, 299);
        //        grpDedcutionsUCVIA.Text = "Deductions under Chapter VI-A";
        //        grpDedcutionsUCVIA.Height = 108;

        //        grpSection80CCE.Visible = false;

        //        lblTotalDeductibleAmount80CCEDesc.Location = new Point(11, 26);
        //        txtTotalDeductibleAmount80CCE.Location = new Point(259, 26);

        //        grpOtherSections.Visible = false;

        //        lblTotalDeductibleAmountOSDesc.Location = new Point(11, 52);
        //        txtTotalDeductibleAmountOS.Location = new Point(259, 52);

        //        lblTotalUCVIADesc.Location = new Point(11, 77);
        //        lblTotalUCVIADesc.Text = "Aggregate of deductible amount under Chapter VI-A";

        //        lblTotalUCVIA.Location = new Point(261, 77);

        //        lblTotalIncomeDesc.Location = new Point(24, 414);
        //        lblTotalIncomeDesc.Text = "Total Income";

        //        lblTotalIncome.Location = new Point(275, 411);

        //        grpTaxPayable.Location = new Point(14, 437);

        //        lblTaxTotalIncomeDesc.Text = "Tax on Total Income";

        //        lblSurcharge.Text = "Surcharge (on tax computed at Srl No. 12)";

        //        lblEducationCessDesc.Text = "Education Cess @ 3% (on tax computed at Srl No. 12)";

        //        lblTaxPaybleDesc.Text = "Tax Payable (12 + 13 + 14)";

        //        lblReliefUS89Desc.Text = "Less: Relief under Section 89";

        //        lblTaxPayableDesc.Text = "Tax Payable";
        //    }
        //    else if (rbnDetailOptions.Checked == true)
        //    {
        //        grpTotalSalaryDetails.Visible = true;

        //        lblTotalSalaryBalanceDesc.Text = "3. Balance (1-2)";
        //        lblTotalSalaryBalanceDesc.ForeColor = Color.Blue;
        //        lblTotalSalaryBalanceDesc.Location = new Point(415, 276);

        //        txtTotalSalaryBalance.Location = new Point(415, 294);

        //        grpDeductionsUS16.Location = new Point(16, 332);
        //        grpDeductionsUS16.Text = "4. Deductions under section 16";

        //        lblAggregate4_a_bDesc.Text = "5. Aggregate of 4(a) && (b)";

        //        lblIncomeChargeableDesc.Location = new Point(107, 438);
        //        lblIncomeChargeableDesc.Text = "6. Income chargeable under the head 'Salaries' (3-5)";

        //        lblIncomeChargeable.Location = new Point(416, 438);

        //        grpAddOtherIncome.Visible = true;

        //        lblTotalOtherIncomeDesc.Location = new Point(415, 546);
        //        lblTotalOtherIncomeDesc.Text = "Total (7)";
        //        lblTotalOtherIncomeDesc.ForeColor = Color.Blue;

        //        txtTotalOtherIncome.Location = new Point(415, 564);

        //        lblGrossTotalIncomeDesc.Location = new Point(238, 594);
        //        lblGrossTotalIncomeDesc.Text = "8. Gross Total Income (6 + 7)";

        //        lblGrossTotalIncome.Location = new Point(416, 594);

        //        grpDedcutionsUCVIA.Location = new Point(14, 617);
        //        grpDedcutionsUCVIA.Text = "9. Deductions under Chapter VI-A";
        //        grpDedcutionsUCVIA.Height = 590;

        //        grpSection80CCE.Visible = true;

        //        lblTotalDeductibleAmount80CCEDesc.Location = new Point(62, 341);
        //        txtTotalDeductibleAmount80CCE.Location = new Point(264, 341);

        //        grpOtherSections.Visible = true;

        //        lblTotalDeductibleAmountOSDesc.Location = new Point(23, 536);
        //        txtTotalDeductibleAmountOS.Location = new Point(271, 536);

        //        lblTotalUCVIADesc.Location = new Point(17, 558);
        //        lblTotalUCVIADesc.Text = "10. Aggregate of deductible amount under Chapter VI-A";

        //        lblTotalUCVIA.Location = new Point(271, 558);

        //        lblTotalIncomeDesc.Location = new Point(257, 1211);
        //        lblTotalIncomeDesc.Text = "11. Total Income (8 - 10)";

        //        lblTotalIncome.Location = new Point(411, 1211);

        //        grpTaxPayable.Location = new Point(14, 1233);

        //        lblTaxTotalIncomeDesc.Text = "12. Tax on Total Income";

        //        lblSurcharge.Text = "13. Surcharge (on tax computed at Srl No. 12)";

        //        lblEducationCessDesc.Text = "14. Education Cess @ 3% (on tax computed at Srl No. 12)";

        //        lblTaxPaybleDesc.Text = "15. Tax Payable (12 + 13 + 14)";

        //        lblReliefUS89Desc.Text = "16. Less: Relief under Section 89";

        //        lblTaxPayableDesc.Text = "17. Tax Payable";
        //    }
        #endregion
        private void rbnEntryOptions_CheckedChanged(object sender, EventArgs e)
        {
            //
            //ClearControls();
            //
            if (rbnMandatoryOptions.Checked == true)
            {
                grpGrossSalary.Enabled = false;
                //grpLessAllowanceUS10.Enabled = false;

                //grpAddOtherIncomeDetails.Enabled = false;

                //grpSection80CCE.Enabled = false;

                //txtSec80CCFGrossAmount.Enabled = false;

                //grpOtherSections.Enabled = false;
                //-- 2021/02/22
                //txtTotalSalaryBalance.TabStop = true;
                //txtTotalSalaryBalance.ReadOnly = false;
                //
                //txtTotalOtherIncome.TabStop = true;
                //txtTotalOtherIncome.ReadOnly = true;
                //
                //txtTotalDeductibleAmount80CCE.TabStop = true;
                //txtTotalDeductibleAmount80CCE.ReadOnly = false;
                ////
                //txtTotalDeductibleAmountOS.TabStop = true;
                //txtTotalDeductibleAmountOS.ReadOnly = false;
                ////-- 2019/04/19
                //grpLessAllowanceUS101819.Enabled = false;
                ////
                ////-- 2021/03/02
                //txtTotalSalaryBalance.ReadOnly = false;
                //txtRebateus87A.Enabled = false;
                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                {
                    //lnkCalcHRA.Visible = false;
                    grpGrossSalary.Enabled = true;
                    //grpLessAllowanceUS101819.Enabled = true;
                    //grpLessAllowanceUS10.Enabled = false;
                    //txtTotalUs10.Visible = true;
                    //lblSec10OtherExemptionlCaption.Visible = true;
                    //txtSec10OtherExemption.Visible = true;
                    //--
                    if (chkTaxation115BAC.Checked == true)
                    {
                        //grpLessAllowanceUS101819.Enabled = false;
                        //lblSec10TravelConcession.Enabled = false;
                        //txtSec10TravelConcession.Enabled = false;
                        //lblSec10HRA.Enabled = false;
                        //txtSec10HRA.Enabled = false;
                    }
                    else if (chkTaxation115BAC.Checked == false)
                    {
                        //grpLessAllowanceUS101819.Enabled = true;
                        //lblSec10TravelConcession.Enabled = true;
                        //txtSec10TravelConcession.Enabled = true;
                        //lblSec10HRA.Enabled = true;
                        //txtSec10HRA.Enabled = true;
                    }
                    //-- 2021/03/02
                    //txtTotalSalaryBalance.ReadOnly = true;
                    //-- VISIBLE TRUE //-- 2019/10/01
                    //-- VISIBLE FALSE 
                    //txtSec80C1819GrossAmount.Enabled = false;
                    //txtSec80CCC1819GrossAmount.Enabled = false;
                    //txtSec80CCD11819GrossAmount.Enabled = false;
                    //txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Enabled = false;
                    //txtSec80CCD1B1819GrossAmount.Enabled = false;
                    //txtSec80CCD21819GrossAmount.Enabled = false;
                    //txtSec80D1819GrossAmount.Enabled = false;
                    //txtSec80E1819GrossAmount.Enabled = false;
                    //txtSec80G1819GrossAmount.Enabled = false;
                    //txtSec80G1819QualifyingAmount.Enabled = false;
                    //txtSec80TTA1819GrossAmount.Enabled = false;
                    //txtSec80TTA1819QualifyingAmount.Enabled = false;
                    //txtSecChVIATotalOthersGrossAmt.Enabled = false;
                    //txtSecChVIATotalOthersQualifyingAmt.Enabled = false;
                    //--
                    //grpSecChVIAOthers.Enabled = false;
                    //txtRebateus87A.Enabled = true;
                }
            }
            else if (rbnDetailOptions.Checked == true)
            {
                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2018_19ID)
                {
                    //lnkCalcHRA.Visible = true;
                    //grpLessAllowanceUS10.Text = "2. Less : Allowance under section 10";
                    //lblTotalCaptionUs10.Visible = true;
                    //lblAllowanceSecAmtTotal.Visible = true;
                    ////txtTotalUs10.Visible = false;
                    //txtRebateus87A.Enabled = false;
                }
                else
                {
                    //lnkCalcHRA.Visible = false;
                    //grpLessAllowanceUS10.Enabled = true;
                    //grpLessAllowanceUS10.Text = "f) Amount of any other exemption under Section 10";
                    ////
                    //lblTotalCaptionUs10.Visible = false;
                    //lblAllowanceSecAmtTotal.Visible = false;
                    //lblSec10OtherExemptionlCaption.Visible = true;
                    //txtSec10OtherExemption.Visible = true;
                    //txtTotalUs10.Visible = true;
                    //
                    txtSec80C1819GrossAmount.Enabled = true;
                    txtSec80CCC1819GrossAmount.Enabled = true;
                    txtSec80CCD11819GrossAmount.Enabled = true;
                    txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Enabled = true;
                    txtSec80CCD1B1819GrossAmount.Enabled = true;
                    //txtSec80CCD21819GrossAmount.Enabled = true;
                    txtSec80D1819GrossAmount.Enabled = true;
                    txtSec80E1819GrossAmount.Enabled = true;
                    txtSec80G1819GrossAmount.Enabled = true;
                    txtSec80G1819QualifyingAmount.Enabled = true;
                    txtSec80TTB1819GrossAmount.Enabled = true;
                    txtSec80TTB1819QualifyingAmount.Enabled = true;
                    //txtSecChVIATotalOthersGrossAmt.Enabled = true;
                    //txtSecChVIATotalOthersGrossAmt.ReadOnly = true;
                    //txtSecChVIATotalOthersQualifyingAmt.Enabled = true;
                    //txtSecChVIATotalOthersQualifyingAmt.ReadOnly = true;
                    //txtSecChVIATotalOthersDeductibleAmt.ReadOnly = true;
                    //grpSecChVIAOthers.Enabled = true;
                    //txtRebateus87A.Enabled = true;
                }
                //
                //-- 2021/03/02
                //txtTotalSalaryBalance.ReadOnly = false;
                //
                grpGrossSalary.Enabled = true;
                //grpLessAllowanceUS10.Enabled = true;

                //grpAddOtherIncomeDetails.Enabled = true;

                //grpSection80CCE.Enabled = true;

                //txtSec80CCFGrossAmount.Enabled = true;

                //grpOtherSections.Enabled = true;
                ////
                //txtTotalSalaryBalance.TabStop = false;
                //txtTotalSalaryBalance.ReadOnly = true;
                ////
                //txtTotalOtherIncome.TabStop = false;
                //txtTotalOtherIncome.ReadOnly = true;
                ////
                //txtTotalDeductibleAmount80CCE.TabStop = false;
                //txtTotalDeductibleAmount80CCE.ReadOnly = true;
                ////
                //txtTotalDeductibleAmountOS.TabStop = false;
                //txtTotalDeductibleAmountOS.ReadOnly = true;
            }
            //--
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            {

                //grpAddOtherIncomeDetails.Enabled = true;
                //txtOtherIncomeDesc1.Text = "INCOME (OR ADMISSIBLE LOSS) FROM HOUSE PROPERTY REPORTED BY EMPLOYEE OFFERED FOR TDS";
                //txtOtherIncomeDesc1.Text = "INCOME / LOSS - HOUSE PROPERTY OFFERED FOR TDS";
                //txtOtherIncomeDesc1.ReadOnly = true;
                //txtOtherIncomeDesc1.TabStop = false;
                //txtOtherIncomeAmt1.Text = "0.00";
                ////txtOtherIncomeDesc2.Text = "INCOME UNDER THE HEAD OTHER SOURCES OFFERED FOR TDS";
                //txtOtherIncomeDesc2.Text = "INCOME-OTHER SOURCES OFFERED FOR TDS";
                //txtOtherIncomeDesc2.ReadOnly = true;
                //txtOtherIncomeDesc2.TabStop = false;
                //txtOtherIncomeAmt2.Text = "0.00";
                //txtOtherIncomeDesc3.Text = "";
                //txtOtherIncomeAmt3.Text = "0.00";
                //txtOtherIncomeDesc4.Text = "";
                //txtOtherIncomeAmt4.Text = "0.00";
                ////
                //txtOtherIncomeDesc3.Enabled=false;
                //txtOtherIncomeAmt3.Enabled = false;
                //txtOtherIncomeDesc4.Enabled = false;
                //txtOtherIncomeAmt4.Enabled = false;
                ////
                //grpDedcutionsUCVIA.Visible = false;
                grpDedcutionsUCVIA1819.Visible = true;
                //grpDedcutionsUCVIA1819.Location = new Point(14, 945);
                //
                //lblBSectionAlert.Visible = false;
            }
            else
            {
                //txtOtherIncomeDesc1.Text = "";
                //txtOtherIncomeDesc1.ReadOnly = false;
                //txtOtherIncomeDesc1.TabStop = true;
                //txtOtherIncomeAmt1.Text = "0.00";
                //txtOtherIncomeDesc2.Text = "";
                //txtOtherIncomeDesc2.ReadOnly = false;
                //txtOtherIncomeDesc2.TabStop = true;
                //txtOtherIncomeAmt2.Text = "0.00";
                //txtOtherIncomeDesc3.Text = "";
                //txtOtherIncomeAmt3.Text = "0.00";
                //txtOtherIncomeDesc4.Text = "";
                //txtOtherIncomeAmt4.Text = "0.00";
                ////
                //txtOtherIncomeDesc3.Enabled = true;
                //txtOtherIncomeAmt3.Enabled = true;
                //txtOtherIncomeDesc4.Enabled = true;
                //txtOtherIncomeAmt4.Enabled = true;
                ////
                //grpDedcutionsUCVIA.Visible = true;
                grpDedcutionsUCVIA1819.Visible = false;
                //
                //lblBSectionAlert.Visible = true;
            }
        }

        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            TrnRegularReturn TrnRegularReturn = new TrnRegularReturn(0);
            //TrnRegularReturn.lblNetTaxableIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS_194P WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == "" ? "0.00" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_INCOME) AS NET_TAXABLE_INCOME FROM TRN_SALARY_DETAILS_194P WHERE BASIC_INFO_ID = " + lngBasicInfoID))));
            //
            if (cmnService.J_ReturnDoubleValue(TrnRegularReturn.lblNetTaxableIncome.Text) > 0)
            {
                //--
                TrnRegularReturn.lblNetTaxableIncomeCaption.Visible = true;
                TrnRegularReturn.lblNetTaxableIncome.Visible = false;
                TrnRegularReturn.lblNetTaxableIncome.Visible = true;
                //--
            }
            else
            {
                //--
                TrnRegularReturn.lblNetTaxableIncomeCaption.Visible = false;
                TrnRegularReturn.lblNetTaxableIncome.Visible = false;
                //--
            }
            //TrnForm24Q.Dispose();
            //--
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region txtGSSec17_Total_TextChanged
        private void txtGSSec17_Total_TextChanged(object sender, EventArgs e)
        {
            //if(lblMode.Text == J_Mode.Add)
            lblTotalGS.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtGSSec17_1.Text)));
        }
        #endregion

        #region txtAllowanceSecAmtTotal_TextChanged
        private void txtAllowanceSecAmtTotal_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            {
                if (rbnDetailOptions.Checked == true)
                    //txtSec10OtherExemption.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt1.Text) +
                    //                        cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt2.Text) +
                    //                        cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt3.Text) +
                    //                        cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt4.Text) +
                    //                        cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt5.Text)));
                    //
                    if (chkTaxation115BAC.Checked == true)
                    {
                        //txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                        //txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10DeathCumRetirement.Text) +
                        //                         cmnService.J_ReturnDoubleValue(txtSec10CommutedValuePension.Text) +
                        //                         cmnService.J_ReturnDoubleValue(txtSec10CashEquivalent.Text) +
                        //                         cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                    }
                //else if (chkTaxation115BAC.Checked == false)
                //txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10TravelConcession.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtSec10DeathCumRetirement.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtSec10CommutedValuePension.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtSec10CashEquivalent.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtSec10HRA.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                //lblAllowanceSecAmtTotal.Text = txtSec10OtherExemption.Text;
                //lblAllowanceSecAmtTotal.Text = txtTotalUs10.Text; //-- 2019/06/20
            }
            else
            {
                //lblAllowanceSecAmtTotal.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt1.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt2.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt3.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt4.Text) +
                //                            cmnService.J_ReturnDoubleValue(txtAllowanceSecAmt5.Text)));
                //txtTotalUs10.Text = lblAllowanceSecAmtTotal.Text;
            }
            //-- 2021/02/09
            //if (chkTaxation115BAC.Checked == true)
            //    txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(txtTotalUs10.Text)));
            //else if (chkTaxation115BAC.Checked == false)
            //    txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(txtTotalUs10.Text)));
        }
        #endregion

        #region txtDedUS16_TextChanged
        private void txtDedUS16_TextChanged(object sender, EventArgs e)
        {
            //lblTotalDeductions.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedEntAllowance.Text) + cmnService.J_ReturnDoubleValue(txtDedTaxEmployment.Text)));
            lblTotalDeductions.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedTaxEmployment.Text) + cmnService.J_ReturnDoubleValue(txtDedTaxSection16ia.Text)));
        }
        #endregion

        #region lblTotalSalary_TextChanged
        private void lblTotalSalary_TextChanged(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit)
                lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)) + cmnService.J_ReturnDoubleValue(txtInterestIncome.Text));
            //txtTotalSalaryBalanceEdit.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblAllowanceSecAmtTotal.Text)));

            if (lblMode.Text == J_Mode.Edit && rbnDetailOptions.Checked == true && rbnMandatoryOptions.Enabled == true)
                return;
            //if (rbnMandatoryOptions.Enabled == false)
            //{
            //if (lblMode.Text == J_Mode.Add)
            //txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblAllowanceSecAmtTotal.Text)));
            //txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(txtTotalUs10.Text)));
            lblIncomeChargeable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)));
            lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)) + cmnService.J_ReturnDoubleValue(txtInterestIncome.Text));
            //}
            //lblIncomeChargeable.Text = "";

        }
        #endregion

        #region txtTotalSalaryBalance_TextChanged
        private void txtTotalSalaryBalance_TextChanged(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 03-04-2015
            // Not changing the values during show record
            if (blnShowRecord == true) return;

            //Copying value of Balance to Current Employer's Salary

            //Copying full amount when Previous Employer's Salary is Zero
            //if (cmnService.J_ReturnDoubleValue(txtReportedTaxableAmount.Text) == 0)
            //    txtTaxableAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text)));

            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)//-- 2019/05/15
            //    txtTaxableAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text)));

            //Added by Shrey Kejriwal on 04-04-2015
            //Further Calculation
            lblIncomeChargeable_TextChanged(sender, e);

        }
        #endregion

        #region lblIncomeChargeable_TextChanged
        private void lblIncomeChargeable_TextChanged(object sender, EventArgs e)
        {
            //Added by Dhrub on 04/11/2013 For Check the String
            //if (cmnService.J_IsNumeric(txtTotalSalaryBalance.Text) == false)
            //{
            //    txtTotalSalaryBalance.Text= "0.00";
            //    return;
            //}
            //if (lblMode.Text == J_Mode.Add)
            //Added by Dhrub on 27/11/2013
            //lblIncomeChargeable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)));
            //-- 2019/05/15
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2021_22ID)
            {
                if (chkTaxation115BAC.Checked == true)
                    lblIncomeChargeable.Text = string.Format("{0:0.00}", ((cmnService.J_ReturnDoubleValue(lblTotalGS.Text))));
                else if (chkTaxation115BAC.Checked == false)
                    lblIncomeChargeable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)));
            }
            //
            lblTotalSalary_TextChanged(sender, e);
        }
        #endregion

        #region txtTotalOtherIncome_TextChanged
        private void txtTotalOtherIncome_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Edit)
            //    txtTotalOtherIncomeEdit.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt2.Text) +
            //cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt3.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt4.Text)));
            if (lblMode.Text == J_Mode.Edit && rbnDetailOptions.Checked == true && rbnMandatoryOptions.Enabled == true)
                return;

            //if (rbnMandatoryOptions.Enabled == false)
            //{
            //if (lblMode.Text == J_Mode.Add)
            //if (chkTaxation115BAC.Checked == true) //-- 2021/03/23
            //    txtTotalOtherIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt2.Text)));
            //else
            //    txtTotalOtherIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt2.Text) +
            //cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt3.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt4.Text)));
            //}
        }
        #endregion

        #region GrossTotalIncome_TextChanged
        private void GrossTotalIncome_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            if (chkTaxation115BAC.Checked == true)
                lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text)));
            else if (chkTaxation115BAC.Checked == false)
                lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text) + cmnService.J_ReturnDoubleValue(txtInterestIncome.Text)));
        }
        #endregion

        #region GrossTotalSec80CAmt_TextChanged
        private void GrossTotalSec80CAmt_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //lblGS80C.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80CAmt1.Text) + cmnService.J_ReturnDoubleValue(txtSec80CAmt2.Text) +
            //                                       cmnService.J_ReturnDoubleValue(txtSec80CAmt3.Text) + cmnService.J_ReturnDoubleValue(txtSec80CAmt4.Text) +
            //                                       cmnService.J_ReturnDoubleValue(txtSec80CAmt5.Text) + cmnService.J_ReturnDoubleValue(txtSec80CAmt6.Text)));
            //                
            //if (lblMode.Text == J_Mode.Add)
            //txtDedTotal80C.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGS80C.Text)));
            //}
        }
        #endregion

        #region txtSec80CCCGrossAmount_TextChanged
        private void txtSec80CCCGrossAmount_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //txtSec80CCCDeductibleAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80CCCGrossAmount.Text)));
        }
        #endregion

        #region txtSec80CCDGrossAmount_TextChanged
        private void txtSec80CCDGrossAmount_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //txtSec80CCDDeductibleAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80CCDGrossAmount.Text)));
        }
        #endregion

        #region lblGS80C_TextChanged
        private void lblGS80C_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //    lblGS80C.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text)));
        }
        #endregion

        #region TotalDeductibleAmount80CCE_TextChanged
        private void TotalDeductibleAmount80CCE_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Edit)
            //    txtTotalDeductibleAmount80CCEEdit.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text) +
            //                                                                cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text) +
            //                                                                cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text)));

            if (lblMode.Text == J_Mode.Edit && rbnDetailOptions.Checked == true && rbnMandatoryOptions.Enabled == true)
                return;

            //if (rbnMandatoryOptions.Enabled == false)
            //{
            //if (lblMode.Text == J_Mode.Add)
            //txtTotalDeductibleAmount80CCE.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text) +
            //                                                            cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text) +
            //                                                            cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text)));
            //}
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2014_15ID && (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text) +
            //                                                                                 cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text) +
            //                                                                                 cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text)) > dblMAXTotalDeductibleAmount80CCE1415)
            //    txtTotalDeductibleAmount80CCE.Text = string.Format("{0:0.00}", dblMAXTotalDeductibleAmount80CCE1415);
            //else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2014_15ID && (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text) +
            //                                                                                 cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text) +
            //                                                                                 cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text)) > dblMAXTotalDeductibleAmount80CCE)
            //    txtTotalDeductibleAmount80CCE.Text = string.Format("{0:0.00}", dblMAXTotalDeductibleAmount80CCE);
            //else
            //    txtTotalDeductibleAmount80CCE.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text) +
            //                                                                cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text) +
            //                                                                cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text)));
        }
        #endregion

        #region txtOSGrossAmount1_TextChanged
        private void txtSec80CCFGrossAmount_TextChanged(object sender, EventArgs e)
        {
            //txtSec80CCFDeductibleAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80CCFGrossAmount.Text)));
        }
        #endregion

        #region txtOSGrossAmount1_TextChanged
        private void txtOSGrossAmount1_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //txtOSQualifyingAmount1.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount1.Text)));
            //txtOSDeductibleAmount1.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount1.Text)));
            //}
        }
        #endregion

        #region txtOSGrossAmount2_TextChanged
        private void txtOSGrossAmount2_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //txtOSQualifyingAmount2.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount2.Text)));
            //txtOSDeductibleAmount2.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount2.Text)));
            //}
        }
        #endregion

        #region txtOSGrossAmount3_TextChanged
        private void txtOSGrossAmount3_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //txtOSQualifyingAmount3.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount3.Text)));
            //txtOSDeductibleAmount3.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount3.Text)));
            //}
        }
        #endregion

        #region txtOSGrossAmount4_TextChanged
        private void txtOSGrossAmount4_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //txtOSQualifyingAmount4.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount4.Text)));
            //txtOSDeductibleAmount4.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount4.Text)));
            //}
        }
        #endregion

        #region txtOSGrossAmount5_TextChanged
        private void txtOSGrossAmount5_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //{
            //txtOSQualifyingAmount5.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount5.Text)));
            //txtOSDeductibleAmount5.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSGrossAmount5.Text)));
            //}
        }
        #endregion

        #region TotalDeductibleAmountOS_TextChanged
        private void TotalDeductibleAmountOS_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Edit)
            //    txtTotalDeductibleAmountOSEdit.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount1.Text) +
            //                                                             cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount2.Text) +
            //                                                             cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount3.Text) +
            //                                                             cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount4.Text) +
            //                                                             cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount5.Text)));

            //if (lblMode.Text == J_Mode.Edit && rbnDetailOptions.Checked == true && rbnMandatoryOptions.Enabled == true)
            //    return;

            ////if (rbnMandatoryOptions.Enabled == false)
            ////{
            //    //if (lblMode.Text == J_Mode.Add)
            //        txtTotalDeductibleAmountOS.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount1.Text) +
            //                                                                 cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount2.Text) +
            //                                                                 cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount3.Text) +
            //                                                                 cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount4.Text) +
            //                                                                 cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount5.Text)));
            ////}
        }
        #endregion

        #region TotalUCVIA_TextChanged
        private void TotalUCVIA_TextChanged(object sender, EventArgs e)
        {
            //if (lblMode.Text == J_Mode.Add)
            //lblTotalUCVIA.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmount80CCE.Text) +
            //                                            cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmountOS.Text) +  cmnService.J_ReturnDoubleValue(txtSec80CCFDeductibleAmount.Text)));
        }
        #endregion

        #region TotalIncome_TextChanged
        private void TotalIncome_TextChanged(object sender, EventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            {
                //-- 2020/07/01 NEW REGIME
                if (chkTaxation115BAC.Checked == true)
                    lblTotalTaxableIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) - cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text)));
                else if (chkTaxation115BAC.Checked == false)
                    lblTotalTaxableIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) - cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text)));
                //
                //lblTotalIncomeRounded.Text = lblTotalIncome.Text;
            }
            else
            {
                //if (lblMode.Text == J_Mode.Add)
                //lblTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) - cmnService.J_ReturnDoubleValue(lblTotalUCVIA.Text)));
                //lblTotalIncomeRounded.Text = lblTotalIncome.Text;
            }
            ////-- 2019/03/18
            //if (cmbYNSuperannuationFund.Text == T_YES_NO.YES)
            //    txtTotalIncomeSuperannuationFund.Text= lblGrossTotalIncome.Text;
            //--
            chkRoundOff_CheckedChanged(sender, e);
        }
        #endregion

        #region GrossTaxPayble_TextChanged
        private void GrossTaxPayble_TextChanged(object sender, EventArgs e)
        {
            //lblGrossTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) + cmnService.J_ReturnDoubleValue(txtSurcharge.Text) + cmnService.J_ReturnDoubleValue(txtEducationCess.Text)));
            //-- 18-19 ONWARDS
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            {
                lblTaxDeductingRebate.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) -
                                                                     cmnService.J_ReturnDoubleValue(txtRebateSec87.Text)));
                if (cmnService.J_ReturnDoubleValue(lblTaxDeductingRebate.Text) < 0)
                    lblTaxDeductingRebate.Text = "0.00";
                //
                lblGrossTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTaxDeductingRebate.Text) +
                                                                     cmnService.J_ReturnDoubleValue(txtSurcharge.Text) +
                                                                     cmnService.J_ReturnDoubleValue(txtEducationCess.Text)));
            }
            else
            {
                //////lblTaxDeductingRebate.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) -
                //////                                                     cmnService.J_ReturnDoubleValue(txtRebate.Text)));
                //
                lblGrossTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) +
                                                                     cmnService.J_ReturnDoubleValue(txtSurcharge.Text) +
                                                                     cmnService.J_ReturnDoubleValue(txtEducationCess.Text)));
            }
            //
            lblNetTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTaxPayable.Text) - cmnService.J_ReturnDoubleValue(txtReliefUS89.Text)));
        }
        #endregion

        #region NetTaxPayble_TextChanged
        private void NetTaxPayble_TextChanged(object sender, EventArgs e)
        {
            lblNetTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTaxPayable.Text) - cmnService.J_ReturnDoubleValue(txtReliefUS89.Text)));
            //
            //lblTaxPayable.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTaxPayable.Text) - cmnService.J_ReturnDoubleValue(txtReliefUS89.Text) - cmnService.J_ReturnDoubleValue(lblRebateCalculated.Text)));
        }
        #endregion

        #region Shortfall_TextChanged
        private void Shortfall_TextChanged(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 03-04-2015
            if (blnShowRecord == true) return;

            // Calculating Shortfall amount
            //lblShortfall.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTaxPayable.Text) - cmnService.J_ReturnDoubleValue(txtTotalTDSDedcuted.Text)));
            ////-- 2016/09/22
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
            //{
            //    lblTotalAmountOfTAX.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtTotalTDSDedcuted.Text) + cmnService.J_ReturnDoubleValue(txtTaxSuperannuationFund.Text));
            //    lblShortfall.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTaxPayable.Text) - cmnService.J_ReturnDoubleValue(lblTotalAmountOfTAX.Text)));                
            //}
            ////Added by Shrey Kejriwal on 03-04-2015
            ////Copying TDS amount to Current Employer's TDS when Previous Employer's TDS is zero
            //if (cmnService.J_ReturnDoubleValue(txtPreviousTaxDeductedAmt.Text) == 0)
            //    txtTotalTaxDeductedAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTotalTDSDedcuted.Text)));
        }
        #endregion

        #region txtGSSec17_1_KeyPress
        private void txtGSSec17_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtGSSec17_1, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtGSSec17_2_KeyPress
        private void txtGSSec17_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtGSSec17_2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtGSSec17_3_KeyPress
        private void txtGSSec17_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2018_19ID)
            {
                if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            }
            else
            {
                //if (Convert.ToInt64(e.KeyChar) == 13) txtSec10TravelConcession.Select();
            }
            //
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtGSSec17_3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtGSSec17_3_Leave
        private void txtGSSec17_3_Leave(object sender, EventArgs e)
        {
            NumericCurrencyControl_Leave(sender, e);
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            {
                //txtSec10TravelConcession.Select();
            }

        }
        #endregion

        #region txtSec10TravelConcession_KeyPress
        private void txtSec10TravelConcession_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10TravelConcession, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec10TravelConcession_KeyPress
        private void txtSec10DeathCumRetirement_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10DeathCumRetirement, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec10CommutedValuePension_KeyPress
        private void txtSec10CommutedValuePension_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10CommutedValuePension, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec10CashEquivalent_KeyPress
        private void txtSec10CashEquivalent_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10CashEquivalent, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec10HRA_KeyPress
        private void txtSec10HRA_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10HRA, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecAmtTotal_KeyPress
        private void txtAllowanceSecAmtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec10OtherExemption, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecDesc1_KeyPress
        //private void txtAllowanceSecDesc1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //}
        #endregion

        #region txtAllowanceSecAmt1_KeyPress
        private void txtAllowanceSecAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAllowanceSecAmt1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecDesc2_KeyPress
        //private void txtAllowanceSecDesc2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //}
        #endregion

        #region txtAllowanceSecAmt2_KeyPress
        private void txtAllowanceSecAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAllowanceSecAmt2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecDesc3_KeyPress
        //private void txtAllowanceSecDesc3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //}
        #endregion

        #region txtAllowanceSecAmt3_KeyPress
        private void txtAllowanceSecAmt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAllowanceSecAmt3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecDesc4_KeyPress
        //private void txtAllowanceSecDesc4_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //}
        #endregion

        #region txtAllowanceSecAmt4_KeyPress
        private void txtAllowanceSecAmt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAllowanceSecAmt4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtAllowanceSecDesc5_KeyPress
        //private void txtAllowanceSecDesc5_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //}
        #endregion

        #region txtAllowanceSecAmt5_KeyPress
        private void txtAllowanceSecAmt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtAllowanceSecAmt5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTotalSalaryBalance_KeyPress
        private void txtTotalSalaryBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalSalaryBalance, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtDedEntAllowance_KeyPress
        private void txtDedEntAllowance_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDedEntAllowance, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtDedTaxEmployment_KeyPress
        private void txtDedTaxEmployment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDedTaxEmployment, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDedTaxSection16ia_KeyPress
        private void txtDedTaxSection16ia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)  SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDedTaxSection16ia, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtOtherIncomeDesc1_KeyPress
        private void txtOtherIncomeDesc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOtherIncomeDesc2_KeyPress
        private void txtOtherIncomeDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOtherIncomeDesc3_KeyPress
        private void txtOtherIncomeDesc3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOtherIncomeDesc4_KeyPress
        private void txtOtherIncomeDesc4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOtherIncome1_KeyPress
        private void txtOtherIncome1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            ////-- 2021/03/23
            //if (chkTaxation115BAC.Checked == true)
            //{
            //    if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt1, "") == false)
            //        e.Handled = true;
            //}
            //else if (chkTaxation115BAC.Checked == false)
            //{
            //    if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt1, "Y") == false)
            //        e.Handled = true;
            //}
        }
        #endregion

        #region txtOtherIncome2_KeyPress
        private void txtOtherIncome2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            ////--
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            //{
            //    if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt2, "") == false)
            //        e.Handled = true;
            //}
            //else
            //{
            //    if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt2, "Y") == false)
            //        e.Handled = true;
            //}
        }
        #endregion

        #region txtOtherIncome3_KeyPress
        private void txtOtherIncome3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt3, "Y") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOtherIncome4_KeyPress
        private void txtOtherIncome4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOtherIncomeAmt4, "Y") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTotalOtherIncome_KeyPress
        private void txtTotalOtherIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
            //    txtSec80C1819GrossAmount.Select();
            //else
            //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            ////
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalOtherIncome, "Y") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CDesc1_KeyPress
        private void txtSec80CDesc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CDesc2_KeyPress
        private void txtSec80CDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CDesc3_KeyPress
        private void txtSec80CDesc3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CDesc4_KeyPress
        private void txtSec80CDesc4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CDesc5_KeyPress
        private void txtSec80CDesc5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CDesc6_KeyPress
        private void txtSec80CDesc6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtSec80CAmt1_KeyPress
        private void txtSec80CAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CAmt2_KeyPress
        private void txtSec80CAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CAmt3_KeyPress
        private void txtSec80CAmt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CAmt4_KeyPress
        private void txtSec80CAmt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CAmt5_KeyPress
        private void txtSec80CAmt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CAmt6_KeyPress
        private void txtSec80CAmt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CAmt6, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtDedTotal80C_KeyPress
        private void txtDedTotal80C_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtDedTotal80C, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCCGrossAmount_KeyPress
        private void txtSec80CCCGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCCGrossAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCCDeductibleAmount_KeyPress
        private void txtSec80CCCDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCCDeductibleAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCDGrossAmount_KeyPress
        private void txtSec80CCDGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCDGrossAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCDDeductibleAmount_KeyPress
        private void txtSec80CCDDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCDDeductibleAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCFGrossAmount_KeyPress
        private void txtSec80CCFGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCFGrossAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCFDeductibleAmount_KeyPress
        private void txtSec80CCFDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCFDeductibleAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion        

        #region txtTotalDeductibleAmount80CCE_KeyPress
        private void txtTotalDeductibleAmount80CCE_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalDeductibleAmount80CCE, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDesc1_KeyPress
        private void txtOSDesc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOSDesc2_KeyPress
        private void txtOSDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOSDesc3_KeyPress
        private void txtOSDesc3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOSDesc4_KeyPress
        private void txtOSDesc4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOSDesc5_KeyPress
        private void txtOSDesc5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtOSGrossAmount1_KeyPress
        private void txtOSGrossAmount1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSGrossAmount1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSQualifyingAmount1_KeyPress
        private void txtOSQualifyingAmount1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSQualifyingAmount1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDeductibleAmount1_KeyPress
        private void txtOSDeductibleAmount1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSDeductibleAmount1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSGrossAmount2_KeyPress
        private void txtOSGrossAmount2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSGrossAmount2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSQualifyingAmount2_KeyPress
        private void txtOSQualifyingAmount2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSQualifyingAmount2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDeductibleAmount2_KeyPress
        private void txtOSDeductibleAmount2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSDeductibleAmount2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSGrossAmount3_KeyPress
        private void txtOSGrossAmount3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSGrossAmount3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSQualifyingAmount3_KeyPress
        private void txtOSQualifyingAmount3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSQualifyingAmount3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDeductibleAmount3_KeyPress
        private void txtOSDeductibleAmount3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSDeductibleAmount3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSGrossAmount4_KeyPress
        private void txtOSGrossAmount4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSGrossAmount4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSQualifyingAmount4_KeyPress
        private void txtOSQualifyingAmount4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSQualifyingAmount4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDeductibleAmount4_KeyPress
        private void txtOSDeductibleAmount4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSDeductibleAmount4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSGrossAmount5_KeyPress
        private void txtOSGrossAmount5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSGrossAmount5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSQualifyingAmount5_KeyPress
        private void txtOSQualifyingAmount5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSQualifyingAmount5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtOSDeductibleAmount5_KeyPress
        private void txtOSDeductibleAmount5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtOSDeductibleAmount5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTotalDeductibleAmountOS_KeyPress
        private void txtTotalDeductibleAmountOS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalDeductibleAmountOS, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTaxTotalIncome_KeyPress
        private void txtTaxTotalIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) txtRebateSec87.Select();// txtSurcharge.Select();
            //--
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTaxTotalIncome, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSurcharge_KeyPress
        private void txtSurcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) txtEducationCess.Select(); //SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSurcharge, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtEducationCess_KeyPress
        private void txtEducationCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) txtReliefUS89.Select();//SendKeys.Send("{tab}");
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
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSave.Select(); //SendKeys.Send("{tab}");
            //if (Convert.ToInt64(e.KeyChar) == 9) BtnSave.Select(); //SendKeys.Send("{tab}");
            //if (Convert.ToInt64(e.KeyChar) == 13 || Convert.ToInt64(e.KeyChar) == 9) cmbHigherRate.Focus();
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalTDSDedcuted, "") == false)
            //e.Handled = true;
        }
        #endregion        

        #region txtTotalTDSDedcuted_KeyDown
        private void txtTotalTDSDedcuted_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyCode) == 9) BtnSave.Select(); //SendKeys.Send("{tab}");
            //if (Convert.ToInt64(e.KeyCode) == 9) SendKeys.Send("{tab}");
            //if (Convert.ToInt64(e.KeyCode) == 13 || Convert.ToInt64(e.KeyCode) == 9) txtTaxableAmount.Select();
        }
        #endregion  

        #region txtTotalTDSDedcuted_Enter
        private void txtTotalTDSDedcuted_Enter(object sender, EventArgs e)
        {
            //pnlSalaryDetailsEntryForm.VerticalScroll.Value = pnlSalaryDetailsEntryForm.VerticalScroll.Maximum;
        }
        #endregion

        #region lblTotalIncome_TextChanged
        private void lblTotalIncome_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (blnRestrictIncomeTaxCalculation == true) return;
                if ((cmbEmployeeCategory.SelectedIndex <= 0) || (lblTotalTaxableIncome.Text == "0.00"))
                {
                    lblTaxDeductingRebateCalculated.Text = "0.00";
                    lblTaxCalculatedECess.Text = "0.00";
                    lblCalculatedSurcharge.Text = "0.00";
                    lblTaxDeductingRebateCalculated.Text = "0.00";
                    lblRebateCalculated.Text = "0.00";
                    lblTaxOnTotalIncomeCalculated.Text = "0.00";
                    //lnkViewOnTax.Visible = false;
                    //lnkViewtaxCalculation.Visible = false;
                    //lnkViewOnSurcharge.Visible = false;
                    //
                    //lnkTotalAmt.Visible = false;
                    //lnkViewDed.Visible = false;
                    return;
                }
                else
                {
                    //lnkViewOnTax.Visible = true;
                    //lnkViewtaxCalculation.Visible = true;
                    //
                    //lnkTotalAmt.Visible = true;
                    //lnkViewDed.Visible = true;
                }
                //lnkViewOnSurcharge.Visible = true;

                if (cmnService.J_IsNumeric(lblTotalTaxableIncome.Text) == false) return;

                int intAsstId = 0;
                string strCategory = "";
                double dblCalculatedTax;
                double dblCalculatedECess;
                double dblCalculatedSurcharge;
                double dblCalculatedTaxCredit;
                double dblTaxonTotalIncomeB4TaxCredit = 0;
                //
                //try
                //{
                strSQL = "SELECT ASST_ID FROM MST_ASSESSMENT " +
                         "WHERE  FA_YEAR  = '" + txtFinancialYear.Text + "' ";
                intAsstId = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                //}
                //catch
                //{
                //}
                //
                if (Convert.ToDouble(lblTotalTaxableIncome.Text) < 0) return;
                //
                strCategory = cmbEmployeeCategory.Text.Substring(0, 1);
                //
                dblCalculatedTax = TdsMan.CalculateIncomeTaxAmount(intAsstId,
                                                                   strCategory,
                                                                   Convert.ToDouble(lblTotalTaxableIncome.Text),
                                                                   chkTaxation115BAC.Checked.ToString(),
                                                                   out dblCalculatedECess,
                                                                   out dblCalculatedSurcharge,
                                                                   out dblCalculatedTaxCredit,
                                                                   out dblTaxonTotalIncomeB4TaxCredit);
                //dblCalculatedTax = Math.Round(dblCalculatedTax, 2);
                dblCalculatedTax = Math.Round(dblCalculatedTax, 0); //-- ANIK @ 2015/12/22
                //dblCalculatedSurcharge = Math.Round(dblCalculatedSurcharge, 0);
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
                    //txtRebate.Text = "0.00";
                }
                //-- 2019/05/04
                //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                //{
                //txtRebate.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                ////lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit)));
                ////lblTaxOnTotalIncomeCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)));
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
                //}
                //if (dmlService.J_IsRecordExist("TEMP_TAX_SLAB_SUMMARY") == true)
                //{
                //    strSQL = "DELETE FROM TEMP_TAX_SLAB_SUMMARY";
                //    dmlService.J_ExecSql(strSQL);
                //}
            }
            catch
            {
            }
        }
        #endregion

        #region txtSec80C1819GrossAmount_TextChanged
        private void txtSec80C1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80C1819GrossAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCC1819GrossAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCD11819GrossAmount.Text)));
            //
            if (txtSec80C1819GrossAmount.Focused == true)
            {
                txtSec80C1819DeductibleAmount.Text = txtSec80C1819GrossAmount.Text;
            }
            //
            if (txtSec80CCC1819GrossAmount.Focused == true)
            {
                txtSec80CCC1819DeductibleAmount.Text = txtSec80CCC1819GrossAmount.Text;
            }
            //
            if (txtSec80CCD11819GrossAmount.Focused == true)
            {
                txtSec80CCD11819DeductibleAmount.Text = txtSec80CCD11819GrossAmount.Text;
            }
        }
        #endregion

        #region txtSec80C1819DeductibleAmount_TextChanged
        private void txtSec80C1819DeductibleAmount_TextChanged(object sender, EventArgs e)
        {
            txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text)));
            //
            if (cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text) > dblMAXTotalDeductibleAmount80C80CCC80CCCD1819)
                txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", dblMAXTotalDeductibleAmount80C80CCC80CCCD1819);
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt1_TextChanged
        private void txtSecChVIAOthersGrossAmt1_TextChanged(object sender, EventArgs e)
        {
            //txtSecChVIATotalOthersGrossAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt1.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt2.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt3.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt4.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt5.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt6.Text)));
            //
            //if (txtSecChVIAOthersGrossAmt1.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt1.Text = txtSecChVIAOthersGrossAmt1.Text;
            //    txtSecChVIAOthersDeductibleAmt1.Text = txtSecChVIAOthersGrossAmt1.Text;
            //}
            ////
            //if (txtSecChVIAOthersGrossAmt2.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt2.Text = txtSecChVIAOthersGrossAmt2.Text;
            //    txtSecChVIAOthersDeductibleAmt2.Text = txtSecChVIAOthersGrossAmt2.Text;
            //}
            ////
            //if (txtSecChVIAOthersGrossAmt3.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt3.Text = txtSecChVIAOthersGrossAmt3.Text;
            //    txtSecChVIAOthersDeductibleAmt3.Text = txtSecChVIAOthersGrossAmt3.Text;
            //}
            ////
            //if (txtSecChVIAOthersGrossAmt4.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt4.Text = txtSecChVIAOthersGrossAmt4.Text;
            //    txtSecChVIAOthersDeductibleAmt4.Text = txtSecChVIAOthersGrossAmt4.Text;
            //}
            ////
            //if (txtSecChVIAOthersGrossAmt5.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt5.Text = txtSecChVIAOthersGrossAmt5.Text;
            //    txtSecChVIAOthersDeductibleAmt5.Text = txtSecChVIAOthersGrossAmt5.Text;
            //}
            ////
            //if (txtSecChVIAOthersGrossAmt6.Focused == true)
            //{
            //    txtSecChVIAOthersQualifyingAmt6.Text = txtSecChVIAOthersGrossAmt6.Text;
            //    txtSecChVIAOthersDeductibleAmt6.Text = txtSecChVIAOthersGrossAmt6.Text;
            //}
        }
        #endregion



        #region txtSec80CCD1B1819GrossAmount_TextChanged
        private void txtSec80CCD1B1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80CCD1B1819DeductibleAmount.Text = txtSec80CCD1B1819GrossAmount.Text;
        }
        #endregion

        #region txtSec80CCD21819GrossAmount_TextChanged
        private void txtSec80CCD21819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            //txtSec80CCD21819DeductibleAmount.Text = txtSec80CCD21819GrossAmount.Text;
        }
        #endregion

        #region txtSec80D1819GrossAmount_TextChanged
        private void txtSec80D1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80D1819DeductibleAmount.Text = txtSec80D1819GrossAmount.Text;
        }
        #endregion

        #region txtSec80E1819GrossAmount_TextChanged
        private void txtSec80E1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80E1819DeductibleAmount.Text = txtSec80E1819GrossAmount.Text;
        }
        #endregion

        #region txtSec80G1819GrossAmount_TextChanged
        private void txtSec80G1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80G1819QualifyingAmount.Text = txtSec80G1819GrossAmount.Text;
            txtSec80G1819DeductibleAmount.Text = txtSec80G1819GrossAmount.Text;
        }
        #endregion

        #region txtSec80TTA1819GrossAmount_TextChanged
        private void txtSec80TTA1819GrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80TTB1819QualifyingAmount.Text = txtSec80TTB1819GrossAmount.Text;
            txtSec80TTB1819DeductibleAmount.Text = txtSec80TTB1819GrossAmount.Text;
        }
        #endregion



        #region txtSecChVIATotalOthersGrossAmt_TextChanged
        private void txtSecChVIATotalOthersGrossAmt_TextChanged(object sender, EventArgs e)
        {
            //txtSecChVIATotalOthersQualifyingAmt.Text = txtSecChVIATotalOthersGrossAmt.Text;
            //txtSecChVIATotalOthersDeductibleAmt.Text = txtSecChVIATotalOthersGrossAmt.Text;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt1_TextChanged
        private void txtSecChVIAOthersQualifyingAmt1_TextChanged(object sender, EventArgs e)
        {
            //txtSecChVIATotalOthersQualifyingAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt1.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt2.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt3.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt4.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt5.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt6.Text)));
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt1_TextChanged
        private void txtSecChVIAOthersDeductibleAmt1_TextChanged(object sender, EventArgs e)
        {
            //txtSecChVIATotalOthersDeductibleAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt1.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt2.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt3.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt4.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt5.Text) + cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt6.Text)));
        }
        #endregion

        #region txtSecChVIATotalOthersDeductibleAmt_TextChanged
        private void txtSecChVIATotalOthersDeductibleAmt_TextChanged(object sender, EventArgs e)
        {
            if (chkTaxation115BAC.Checked == true)
                lblSecChVIATotalAmt.Text = "0,00";// string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtSec80CCD21819DeductibleAmount.Text));
            else if (chkTaxation115BAC.Checked == false)
                lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80E1819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80DDDeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80EEDeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80EEADeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80GGDeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80GGCDeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSec80UDeductibleAmount.Text) +
                                                                      cmnService.J_ReturnDoubleValue(txtSecOthersDeductibleAmount.Text)));
        }
        #endregion

        #region txtRebateus87A_KeyPress
        //private void txtRebateus87A_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        //    if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtRebateus87A, "") == false)
        //        e.Handled = true;
        //}
        #endregion

        #region txtSec80C1819GrossAmount_KeyPress
        private void txtSec80C1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80C1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80C1819DeductibleAmount_KeyPress
        private void txtSec80C1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80C1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCC1819GrossAmount_KeyPress
        private void txtSec80CCC1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCC1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion 

        #region txtSec80CCC1819DeductibleAmount_KeyPress
        private void txtSec80CCC1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCC1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD11819GrossAmount_KeyPress
        private void txtSec80CCD11819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD11819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD11819DeductibleAmount_KeyPress
        private void txtSec80CCD11819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD11819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region lnkView_LinkClicked
        // Added by Dhrub on 30-10-2013
        private void lnkView_LinkClicked(object sender, EventArgs e)
        {
            string strDeductedInAllPrecQtr = "";
            string strTotalTaxableAmount = "";
            if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
            {
                //strDeductedInAllPrecQtr = txtTotalTaxDeductedAmt.Text;
                //strTotalTaxableAmount = txtTaxableAmount.Text;
                //strDeductedInAllPrecQtr = lblEmployeeDeductionAmtfrmDB.Text;
                //strTotalTaxableAmount = lblTaxableAmountfrmDB.Text;
            }
            else
            {
                //strDeductedInAllPrecQtr = lblTaxDeductedInAllQtrs.Text;
                strTotalTaxableAmount = Convert.ToString(dblTaxableAmount);
            }
            TrnTaxDeducted objTrnTaxDeducted = new TrnTaxDeducted(txtEmployeeName.Text, txtEmployeePAN.Text, cmbEmployeeCategory.Text, strDeductedInAllPrecQtr, strTotalTaxableAmount, txtFinancialYear.Text);
            objTrnTaxDeducted.ShowDialog();
            this.Refresh();
        }
        #endregion

        #region lblTaxDeductedInAllQtrs
        private void lblTaxDeductedInAllQtrs_TextChanged(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId  >= T_FinancialYearID.F2013_14ID) return;
            //    if (lblTaxDeductedInAllQtrs.Text.Trim() != "0.00") lnkView.Visible = true;
            //    else lnkView.Visible = false;
        }
        #endregion

        #region cmbEmployeeCategory_SelectedIndexChanged
        private void cmbEmployeeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalIncome_TextChanged(sender, e);
        }
        #endregion 

        #region lnkViewOnTax_LinkClicked

        // Commented by Shrey Kejriwal on 03-04-2015
        //private void lnkViewOnTax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    if (txtTaxTotalIncome.Text != "0.00")
        //        TdsMan.blnFetchData = T_TRUE_FALSE.FALSE;
        //    else
        //        TdsMan.blnFetchData = T_TRUE_FALSE.TRUE;

        //    TrnIncomeTaxCalculateSummary objTrnTaxSummary = new TrnIncomeTaxCalculateSummary(txtFinancialYear.Text, txtEmployeeName.Text, txtEmployeePAN.Text, cmbEmployeeCategory.Text, lblTotalIncome.Text, TdsMan.blnFetchData);
        //    objTrnTaxSummary.ShowDialog();
        //    //if (TdsMan.blnFetchData == true)
        //    //{
        //        //txtTaxTotalIncome.Text = lblTaxCalculated.Text;
        //        //txtEducationCess.Text = lblTaxCalculatedECess.Text;
        //    //}
        //    this.Refresh();
        //}


        // Added by Shrey Kejriwal on 03-04-2015
        private void lnkViewOnTax_LinkClicked(object sender, EventArgs e)
        {
            //--
            lblTotalIncome_TextChanged(sender, e);
            //--
            if (txtTaxTotalIncome.Text != "0.00")
                TdsMan.blnFetchData = T_TRUE_FALSE.FALSE;
            else
                TdsMan.blnFetchData = T_TRUE_FALSE.TRUE;
            //--
            if (txtEmployeeName.Text.Trim() + txtEmployeePAN.Text.Trim() != "")
            {
                TrnIncomeTaxCalculateSummary objTrnTaxSummary = new TrnIncomeTaxCalculateSummary(txtFinancialYear.Text,
                                                                                                 txtEmployeeName.Text,
                                                                                                 txtEmployeePAN.Text,
                                                                                                 cmbEmployeeCategory.Text,
                                                                                                 chkTaxation115BAC.Checked.ToString(),
                                                                                                 lblTotalTaxableIncome.Text,
                                                                                                 TdsMan.blnFetchData);
                objTrnTaxSummary.ShowDialog();
            }
        }



        #endregion

        #region lblTaxCalculatedECess_TextChanged
        private void lblTaxCalculatedECess_TextChanged(object sender, EventArgs e)
        {
            //if (lblTaxCalculatedECess.Text.Trim() != "0.00") lnkViewOnTax.Visible = true;
            //else lnkViewOnTax.Visible = false;
        }
        #endregion

        #region txtTaxableAmount_KeyPress
        private void txtTaxableAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) txtReportedTaxableAmount.Select();//SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTaxableAmount, "") == false)
            //    e.Handled = true;

        }
        #endregion

        #region txtReportedTaxableAmount_KeyPress
        private void txtReportedTaxableAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtReportedTaxableAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTotalTaxDeductedAmt_KeyPress
        private void txtTotalTaxDeductedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) txtPreviousTaxDeductedAmt.Select();//SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtTotalTaxDeductedAmt, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtPreviousTaxDeductedAmt_KeyPress
        private void txtPreviousTaxDeductedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtPreviousTaxDeductedAmt, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtTaxableAmount_KeyDown
        private void txtTaxableAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyCode) == 9) txtReportedTaxableAmount.Select();//SendKeys.Send("{tab}");
        }
        #endregion

        #region txtReportedTaxableAmount_KeyDown
        private void txtReportedTaxableAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt64(e.KeyCode) == 9) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtPreviousTaxDeductedAmt_KeyDown
        private void txtTotalTaxDeductedAmt_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyCode) == 9) txtPreviousTaxDeductedAmt.Select();//SendKeys.Send("{tab}");
        }
        #endregion

        #region txtPreviousTaxDeductedAmt_KeyDown
        private void txtPreviousTaxDeductedAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt64(e.KeyCode) == 9) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTaxableAmount_Leave
        private void txtTaxableAmount_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
            //txtTotalTDSDedcuted.Select();
        }
        #endregion

        #region txtReportedTaxableAmount_Leave
        private void txtReportedTaxableAmount_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region txtTotalTaxDeductedAmt_Leave
        private void txtTotalTaxDeductedAmt_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region txtPreviousTaxDeductedAmt_Leave
        private void txtPreviousTaxDeductedAmt_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
        }
        #endregion

        #region cmbHigherRate_KeyPress
        private void cmbHigherRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) BtnSave.Select();
        }
        #endregion

        #region cmbHigherRate_SelectedIndexChanged
        private void cmbHigherRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbHigherRate.Text == T_YES_NO.YES)
            //{
            //    intTaxDeductedAtHigherRate = 1;
            //}
            //else if (cmbHigherRate.Text == T_YES_NO.NO)
            //{
            //    intTaxDeductedAtHigherRate = 0;
            //}
        }
        #endregion 

        #region cmbHigherRate_KeyDown
        private void cmbHigherRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt64(e.KeyCode) == 13 || Convert.ToInt64(e.KeyCode) == 9) BtnSave.Select();
        }
        #endregion 

        #region txtTaxableAmount_TextChanged
        private void txtTaxableAmount_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_IsNumeric(txtTaxableAmount.Text) == false)
            //{
            //    txtTaxableAmount.Text = "0.00";
            //    return;
            //}
            //if (txtTaxableAmount.Text == "0.00")
            //    lnkViewDed.Visible = false;
            //else
            //    lnkViewDed.Visible = true;
            //
        }
        #endregion 

        #region txtTotalTaxDeductedAmt_TextChanged
        private void txtTotalTaxDeductedAmt_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_IsNumeric(txtTotalTaxDeductedAmt.Text) == false)
            //{
            //    txtTotalTaxDeductedAmt.Text = "0.00";
            //    return;
            //}
            //----
            //if (txtTotalTaxDeductedAmt.Text == "0.00")
            //    lnkTotalAmt.Visible = false;
            //else
            //    lnkTotalAmt.Visible = true;
        }

        #endregion 

        #region txtReportedTaxableAmount_TextChanged
        private void txtReportedTaxableAmount_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_IsNumeric(txtReportedTaxableAmount.Text) == false)
            //{
            //    txtReportedTaxableAmount.Text = "0.00";
            //    return;
            //}
            //txtTaxableAmount_TextChanged(sender, e);
        }
        #endregion

        #region txtPreviousTaxDeductedAmt_TextChanged
        private void txtPreviousTaxDeductedAmt_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_IsNumeric(txtPreviousTaxDeductedAmt.Text) == false)
            //{
            //    txtPreviousTaxDeductedAmt.Text = "0.00";
            //    return;
            //}
        }
        #endregion 

        #region txtReliefUS89_KeyDown
        private void txtReliefUS89_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyCode) == 9) txtTotalTDSDedcuted.Select();
        }
        #endregion 

        #region lblTaxableAmountfrmDB_TextChanged
        private void lblTaxableAmountfrmDB_TextChanged(object sender, EventArgs e)
        {
            //if (lblTaxableAmountfrmDB.Text !="0.00")
            //    lnkViewDed.Visible = true;
            //else
            //    lnkViewDed.Visible = false;
        }
        #endregion 

        #region lblEmployeeDeductionAmtfrmDB_TextChanged
        private void lblEmployeeDeductionAmtfrmDB_TextChanged(object sender, EventArgs e)
        {
            //if (lblEmployeeDeductionAmtfrmDB.Text != "0.00")
            //    lnkTotalAmt.Visible = true;
            //else
            //    lnkTotalAmt.Visible = false;
        }
        #endregion 

        #region rbnDetailOptions_KeyDown
        private void rbnDetailOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt64(e.KeyCode) == 13) SendKeys.Send("{tab}");//txtGSSec17_1.Select();
        }
        #endregion

        #region rbnMandatoryOptions_KeyDown
        private void rbnMandatoryOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt64(e.KeyCode) == 13) SendKeys.Send("{tab}");// txtTotalSalaryBalance.Select();
        }
        #endregion

        #region txtTotalTDSDedcuted_Leave
        private void txtTotalTDSDedcuted_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));
            //txtTaxableAmount.Select();
        }
        #endregion 

        #region cmbHigherRate_Enter
        private void cmbHigherRate_Enter(object sender, EventArgs e)
        {
            pnlSalaryDetailsEntryForm.VerticalScroll.Value = pnlSalaryDetailsEntryForm.VerticalScroll.Maximum;
        }
        #endregion 

        #region lblCalculatedSurcharge_TextChanged
        private void lblCalculatedSurcharge_TextChanged(object sender, EventArgs e)
        {
            //if (lblCalculatedSurcharge.Text.Trim() != "0.00") lnkViewOnSurcharge.Visible = true;
            //else lnkViewOnSurcharge.Visible = false;
        }
        #endregion


        #region btnVerifyPAN_Click
        private void btnVerifyPAN_Click(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_GetPANforVerification = TdsMan.ValidatePAN(txtEmployeePAN.Text);
            TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES = TDSMAN.Classes.TDSMAN.T_pTAN;
            //
            //this.Cursor = Cursors.WaitCursor;
            //-------------------------------------------------------
            //ADDED BY DHRUB ON 15/01/2014 FOR PAN VERICATION LINK
            //-------------------------------------------------------
            //TrnPANVerificationSummary objTrnPANVerificationSummary = new TrnPANVerificationSummary();
            //TrnPANVerificationSummaryTraces objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces();
            //TrnPANVerificationSummaryTraces_SINGLE_NEW objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces_SINGLE_NEW();
            TrnPANVerificationSummaryTraces_SINGLE_NEW_Log objTrnPANVerificationSummary = new TrnPANVerificationSummaryTraces_SINGLE_NEW_Log();
            objTrnPANVerificationSummary.ShowDialog();
            this.Refresh();
        }
        #endregion

        #region btnVerifyPAN_MouseMove
        private void btnVerifyPAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnVerifyPAN, "Verify PAN");
        }
        #endregion

        #region lnkCalcHRA_LinkClicked
        private void lnkCalcHRA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TrnForm24QSalaryDetails SalaryDetailsForm = new TrnForm24QSalaryDetails();
            //SalaryDetailsForm.MdiParent = TrnRegularReturn.ActiveForm;
            //SalaryDetailsForm.Show();
            TrnHRACalculator objTrnHRACalculator = new TrnHRACalculator(txtEmployeePAN.Text, txtEmployeeName.Text, Convert.ToDouble(lblTotalGS.Text));
            objTrnHRACalculator.MdiParent = TrnForm24QSalaryDetails.ActiveForm;
            objTrnHRACalculator.Show();
        }
        #endregion


        #region txtAllowanceSecDesc1_TextChanged
        private void txtAllowanceSecDesc1_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtAllowanceSecDesc1";
            //ShowHelpListUS10(txtAllowanceSecDesc1.Text.Trim(), 40, 152);
        }
        #endregion

        #region txtAllowanceSecDesc2_TextChanged
        private void txtAllowanceSecDesc2_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtAllowanceSecDesc2";
            //ShowHelpListUS10(txtAllowanceSecDesc2.Text.Trim(), 40, 211);
        }
        #endregion

        #region txtAllowanceSecDesc3_TextChanged
        private void txtAllowanceSecDesc3_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtAllowanceSecDesc3";
            //ShowHelpListUS10(txtAllowanceSecDesc3.Text.Trim(), 40, 234);
        }
        #endregion

        #region txtAllowanceSecDesc4_TextChanged
        private void txtAllowanceSecDesc4_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtAllowanceSecDesc4";
            //ShowHelpListUS10(txtAllowanceSecDesc4.Text.Trim(), 40, 257);
        }
        #endregion

        #region txtAllowanceSecDesc5_TextChanged
        private void txtAllowanceSecDesc5_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtAllowanceSecDesc5";
            //ShowHelpListUS10(txtAllowanceSecDesc5.Text.Trim(), 40, 280);
        }
        #endregion

        #region txtOtherIncomeDesc1_TextChanged
        private void txtOtherIncomeDesc1_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtOtherIncomeDesc1";
            ////MessageBox.Show(txtOtherIncomeDesc1.Location.X.ToString());
            ////MessageBox.Show(txtOtherIncomeDesc1.Location.Y.ToString());
            //ShowHelpListOtherIncome(txtOtherIncomeDesc1.Text.Trim(), 37, 643);
        }
        #endregion

        #region txtOtherIncomeDesc2_TextChanged
        private void txtOtherIncomeDesc2_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtOtherIncomeDesc2";
            //ShowHelpListOtherIncome(txtOtherIncomeDesc2.Text.Trim(), 37, 643);
        }
        #endregion

        #region txtOtherIncomeDesc3_TextChanged
        private void txtOtherIncomeDesc3_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtOtherIncomeDesc3";
            //ShowHelpListOtherIncome(txtOtherIncomeDesc3.Text.Trim(), 37, 643);
        }
        #endregion

        #region txtOtherIncomeDesc4_TextChanged
        private void txtOtherIncomeDesc4_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtOtherIncomeDesc4";
            //ShowHelpListOtherIncome(txtOtherIncomeDesc4.Text.Trim(), 37, 643);
        }
        #endregion

        #region txtSec80CDesc1_TextChanged
        private void txtSec80CDesc1_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc1";
            //ShowHelpListSec80(txtSec80CDesc1.Text.Trim(), 49, 862);
        }
        #endregion

        #region txtSec80CDesc2_TextChanged
        private void txtSec80CDesc2_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc2";
            //ShowHelpListSec80(txtSec80CDesc2.Text.Trim(), 49, 887);
        }
        #endregion

        #region txtSec80CDesc3_TextChanged
        private void txtSec80CDesc3_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc3";
            //ShowHelpListSec80(txtSec80CDesc3.Text.Trim(), 49, 912);
        }
        #endregion

        #region txtSec80CDesc4_TextChanged
        private void txtSec80CDesc4_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc4";
            //ShowHelpListSec80(txtSec80CDesc4.Text.Trim(), 49, 937);
        }
        #endregion

        #region txtSec80CDesc5_TextChanged
        private void txtSec80CDesc5_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc5";
            //ShowHelpListSec80(txtSec80CDesc5.Text.Trim(), 49, 962);
        }
        #endregion

        #region txtSec80CDesc6_TextChanged
        private void txtSec80CDesc6_TextChanged(object sender, EventArgs e)
        {
            //strFocusTextBox = "txtSec80CDesc6";
            //ShowHelpListSec80(txtSec80CDesc6.Text.Trim(), 49, 987);
        }
        #endregion

        #region lstHelp_Click
        private void lstHelp_Click(object sender, EventArgs e)
        {
            //string strEmployeeHelp = "";
            //strEmployeeHelp = lstEmployeeHelp.Text;
            ////
            //txtEmployeePAN.Text = cmnService.J_Mid(strEmployeeHelp, 45, 10);
            //txtEmployeeName.Text = cmnService.J_Left(strEmployeeHelp, 45).Trim();
            //cmbEmployeeCategory.Text = strEmployeeHelp.Substring(58).Trim();
            ////--
            //lstEmployeeHelp.Visible = false;
            ////--
            //txtEmployeePAN.Select();
            //
            //Form MyForm = new Form();
            //TrnForm24QSalaryDetails TrnForm24QSalaryDetails = new TrnForm24QSalaryDetails();
            //foreach (Control txtBox in grpLessAllowanceUS10.Controls)
            //foreach (Control txtBox in grpLessAllowanceUS10.Controls)
            //{
            //    //if (txtBox is TextBox)
            //    //{
            //    if (txtBox.Name == strFocusTextBox)
            //    {
            //        txtBox.Text = lstHelp.Text;
            //        lstHelp.Visible = false;
            //        txtBox.Select();
            //        return;
            //    }
            //    //}
            //}
            ////--
            //foreach (Control txtBox in grpAddOtherIncomeDetails.Controls)
            //{
            //    //if (txtBox is TextBox)
            //    //{
            //    if (txtBox.Name == strFocusTextBox)
            //    {
            //        txtBox.Text = lstHelp.Text;
            //        lstHelp.Visible = false;
            //        txtBox.Select();
            //        return;
            //    }
            //    //}
            //}
            ////--
            //foreach (Control txtBox in grpOtherSections.Controls)
            //{
            //    //if (txtBox is TextBox)
            //    //{
            //    if (txtBox.Name == strFocusTextBox)
            //    {
            //        txtBox.Text = lstHelp.Text;
            //        lstHelp.Visible = false;
            //        txtBox.Select();
            //        return;
            //    }
            //    //}
            //}
            ////--
        }
        #endregion

        #region lstHelp_KeyPress
        private void lstHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                //lstHelp.Visible = false;
                TextBox strFocusTextBox = new TextBox();
                strFocusTextBox.Select();
            }

        }
        #endregion

        #region chkRoundOff_CheckedChanged

        private void chkRoundOff_CheckedChanged(object sender, EventArgs e)
        {

            if (RoundOff == true) return;
            //
            if (TDSMAN.Classes.TDSMAN.T_EnableRoundOffTaxableAmt == false) return;
            //
            //double dblTotalIncomeTemp = lblTotalIncome.Text;
            if (chkRoundOff.Checked == true)
            {
                //lblTotalIncomeRounded.Text = lblTotalIncome.Text;
                //lblTotalIncome.Text = string.Format("{0:0.00}", ((Math.Round(Convert.ToDouble(lblTotalIncome.Text) / 10) * 10)));
                lblTotalTaxableIncome.Text = string.Format("{0:0.00}", ((Math.Round(Convert.ToDouble(lblTotalTaxableIncome.Text) / 10, 0, MidpointRounding.AwayFromZero) * 10)));
            }
            else if (chkRoundOff.Checked == false)
            {
                //blnRestrictIncomeTaxCalculation = true;
                //lblTotalIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(lblTotalIncomeRounded.Text)); //-- 2018/05/12
                //blnRestrictIncomeTaxCalculation = false;
                //blnRestrictIncomeTaxCalculation = true;
                //lblTotalIncome.Text = string.Format("{0:0.00}", Convert.ToDouble(lblTotalIncome.Text));
                //blnRestrictIncomeTaxCalculation = false;
            }
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt1_KeyPress
        private void txtSecChVIAOthersGrossAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt2_KeyPress
        private void txtSecChVIAOthersGrossAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt3_KeyPress
        private void txtSecChVIAOthersGrossAmt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt4_KeyPress
        private void txtSecChVIAOthersGrossAmt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt5_KeyPress
        private void txtSecChVIAOthersGrossAmt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersGrossAmt6_KeyPress
        private void txtSecChVIAOthersGrossAmt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersGrossAmt6, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCD1B1819GrossAmount_KeyPress
        private void txtSec80CCD1B1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD1B1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD1B1819DeductibleAmount_KeyPress
        private void txtSec80CCD1B1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            ////
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD1B1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80CCD21819GrossAmount_KeyPress
        private void txtSec80CCD21819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD21819GrossAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80CCD21819DeductibleAmount_KeyPress
        private void txtSec80CCD21819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) txtSec80D1819GrossAmount.Select(); // SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80CCD21819DeductibleAmount, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSec80D1819GrossAmount_KeyPress
        private void txtSec80D1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80D1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80D1819DeductibleAmount_KeyPress
        private void txtSec80D1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80D1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80E1819GrossAmount_KeyPress
        private void txtSec80E1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80E1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80E1819DeductibleAmount_KeyPress
        private void txtSec80E1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80E1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80G1819GrossAmount_KeyPress
        private void txtSec80G1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80G1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80G1819DeductibleAmount_KeyPress
        private void txtSec80G1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80G1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80G1819QualifyingAmount_KeyPress
        private void txtSec80G1819QualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80G1819QualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80TTA1819QualifyingAmount_KeyPress
        private void txtSec80TTA1819QualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80TTB1819QualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80TTA1819GrossAmount_KeyPress
        private void txtSec80TTA1819GrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80TTB1819GrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80TTA1819DeductibleAmount_KeyPress
        private void txtSec80TTA1819DeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80TTB1819DeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt1_KeyPress
        private void txtSecChVIAOthersQualifyingAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt2_KeyPress
        private void txtSecChVIAOthersQualifyingAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt3_KeyPress
        private void txtSecChVIAOthersQualifyingAmt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt4_KeyPress
        private void txtSecChVIAOthersQualifyingAmt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt5_KeyPress
        private void txtSecChVIAOthersQualifyingAmt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersQualifyingAmt6_KeyPress
        private void txtSecChVIAOthersQualifyingAmt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersQualifyingAmt6, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt1_KeyPress
        private void txtSecChVIAOthersDeductibleAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt1, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt2_KeyPress
        private void txtSecChVIAOthersDeductibleAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt2, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt3_KeyPress
        private void txtSecChVIAOthersDeductibleAmt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt3, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt4_KeyPress
        private void txtSecChVIAOthersDeductibleAmt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt4, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt5_KeyPress
        private void txtSecChVIAOthersDeductibleAmt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt5, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIAOthersDeductibleAmt6_KeyPress
        private void txtSecChVIAOthersDeductibleAmt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIAOthersDeductibleAmt6, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIATotalOthersGrossAmt_KeyPress
        private void txtSecChVIATotalOthersGrossAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIATotalOthersGrossAmt, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIATotalOthersQualifyingAmt_KeyPress
        private void txtSecChVIATotalOthersQualifyingAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIATotalOthersQualifyingAmt, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region txtSecChVIATotalOthersDeductibleAmt_KeyPress
        private void txtSecChVIATotalOthersDeductibleAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecChVIATotalOthersDeductibleAmt, "") == false)
            //    e.Handled = true;
        }
        #endregion

        #region pnlSalaryDetailsEntryForm_Scroll
        private void pnlSalaryDetailsEntryForm_Scroll(object sender, ScrollEventArgs e)
        {
            //intRentGrpY = grpRent.Location.Y;
            //lblLocation.Text = grpRent.Location.Y.ToString();
            //if (intRentGrpY < 2118)
            //{
            //    if (cmbYNSuperannuationFund.Text == T_YES_NO.YES)
            //        grpRent.Location = new Point(grpRent.Location.X, intRentGrpY + 230);
            //    else if (cmbYNSuperannuationFund.Text == T_YES_NO.NO)
            //        grpRent.Location = new Point(grpRent.Location.X, intRentGrpY - 250);
            //}
        }
        #endregion

        #region pnlSalaryDetailsEntryForm_Paint
        private void pnlSalaryDetailsEntryForm_Paint(object sender, PaintEventArgs e)
        {
            //intRentGrpY = grpRent.Location.Y;
            //lblLocation.Text = grpRent.Location.Y.ToString();
        }
        #endregion

        #region chkPopulateCalculation_CheckedChanged
        private void chkPopulateCalculation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPopulateCalculation.Checked == true)
            {
                txtTaxTotalIncome.Text = lblTaxOnTotalIncomeCalculated.Text;
                txtRebateSec87.Text = lblRebateCalculated.Text;
                lblTaxDeductingRebate.Text = lblTaxDeductingRebateCalculated.Text;
                txtSurcharge.Text = lblCalculatedSurcharge.Text;
                txtEducationCess.Text = lblTaxCalculatedECess.Text;
                //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                //    txtRebateus87A.Text = lblRebateCalculated.Text;
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=PgW7M0xReK8&t=161s");
        }
        #endregion



        #region txtEmpRefNo_Leave
        private void txtEmpRefNo_Leave(object sender, EventArgs e)
        {
            txtEmpRefNo.Text = string.Format("{0:0000000000}", Convert.ToDouble(cmnService.J_NumericData(txtEmpRefNo)));
        }
        #endregion


        #region chkTaxation115BAC_CheckedChanged
        private void chkTaxation115BAC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaxation115BAC.Checked == true)
            {
                ErrProvider.Clear();
                ////grpLessAllowanceUS101819.Enabled = false;
                //lblSec10TravelConcession.Enabled = false;
                //txtSec10TravelConcession.Enabled = false;
                //lblSec10HRA.Enabled = false;
                //txtSec10HRA.Enabled = false;
                //lblSec10OtherExemptionlCaption.Enabled = false;
                //txtSec10OtherExemption.Enabled = false;
                //grpLessAllowanceUS10.Enabled = false;
                grpDeductionsUS16.Enabled = false;
                ////grpAddOtherIncome.Enabled = false;
                //txtOtherIncomeDesc2.Enabled = false;
                //txtOtherIncomeAmt2.Enabled = false;
                //if(cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) < 0)
                //    txtOtherIncomeAmt1.Text="0.00";
                //txtTotalOtherIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text)));
                //lblTotalCaptionUs101819.Enabled = false;
                //txtSec10OtherExemption.Enabled = false;
                //lblTotalAmountus10Caption.Enabled = false;
                //txtTotalUs10.Enabled = false;
                ////txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                //txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10DeathCumRetirement.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10CommutedValuePension.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10CashEquivalent.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                //
                //txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(txtTotalUs10.Text)));
                //lblIncomeChargeable.Text = string.Format("{0:0.00}", ((cmnService.J_ReturnDoubleValue(txtTaxableAmount.Text) + cmnService.J_ReturnDoubleValue(txtReportedTaxableAmount.Text))));
                //txtTotalOtherIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt2.Text)));
                //lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text) + cmnService.J_ReturnDoubleValue(txtTotalOtherIncome.Text)));
                ////lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text)));
                grpDedcutionsUCVIA1819.Enabled = false;
                //lblSecChVIATotalOthersAmt.Enabled = false;
                //txtSecChVIATotalOthersGrossAmt.Enabled = false;
                //txtSecChVIATotalOthersQualifyingAmt.Enabled = false;
                //txtSecChVIATotalOthersDeductibleAmt.Enabled = false;
                //
                //grpSec80CCD2.Enabled = true;
                //lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtSec80CCD21819DeductibleAmount.Text));
                lblTotalTaxableIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) - cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text)));
            }
            else if (chkTaxation115BAC.Checked == false)
            {
                ////grpLessAllowanceUS101819.Enabled = true;
                //lblSec10TravelConcession.Enabled = true;
                //txtSec10TravelConcession.Enabled = true;
                //lblSec10HRA.Enabled = true;
                //txtSec10HRA.Enabled = true; ;
                //lblSec10OtherExemptionlCaption.Enabled = true;
                //txtSec10OtherExemption.Enabled = true;
                //--
                //txtTotalUs10.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSec10TravelConcession.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10DeathCumRetirement.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10CommutedValuePension.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10CashEquivalent.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10HRA.Text) +
                //                             cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text)));
                //
                //txtTotalSalaryBalance.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblTotalGS.Text) - cmnService.J_ReturnDoubleValue(txtTotalUs10.Text)));
                //lblIncomeChargeable.Text = string.Format("{0:0.00}", ((cmnService.J_ReturnDoubleValue(txtTaxableAmount.Text) + cmnService.J_ReturnDoubleValue(txtReportedTaxableAmount.Text)) - cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text)));
                //grpLessAllowanceUS10.Enabled = true;
                //lblTotalCaptionUs101819.Enabled = true;
                //txtSec10OtherExemption.Enabled = true;
                //lblTotalAmountus10Caption.Enabled = true;
                //txtTotalUs10.Enabled = true;
                grpDeductionsUS16.Enabled = true;
                //lblSecChVIATotalOthersAmt.Enabled = true;
                //txtSecChVIATotalOthersGrossAmt.Enabled = true;
                //txtSecChVIATotalOthersQualifyingAmt.Enabled = true;
                //txtSecChVIATotalOthersDeductibleAmt.Enabled = true;
                ////grpAddOtherIncome.Enabled = true;
                //txtOtherIncomeDesc2.Enabled = true;
                //txtOtherIncomeAmt2.Enabled = true;
                //txtTotalOtherIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt2.Text) +
                //                                                          cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt3.Text) + cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt4.Text)));
                //lblGrossTotalIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text) + cmnService.J_ReturnDoubleValue(txtTotalOtherIncome.Text)));
                grpDedcutionsUCVIA1819.Enabled = true;
                //lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(txtSecChVIATotalOthersDeductibleAmt.Text) + cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCD21819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80E1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80TTA1819DeductibleAmount.Text)));
                lblTotalTaxableIncome.Text = string.Format("{0:0.00}", (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) - cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text)));
            }
            //--
            lblTotalIncome_TextChanged(sender, e);
        }
        #endregion


        #region txtSec80CCD21819GrossAmount_KeyDown
        private void txtSec80CCD21819GrossAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab && e.Modifiers == Keys.Shift) txtSec80CCD1B1819DeductibleAmount.Select();
        }
        #endregion


        #region txtSec80CCD1B1819DeductibleAmount_PreviewKeyDown
        private void txtSec80CCD1B1819DeductibleAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData == Keys.Tab) txtSec80CCD21819GrossAmount.Select();// SendKeys.Send("{tab}");
            //e.IsInputKey = true;
        }
        #endregion


        #region txtSec80CCD21819DeductibleAmount_PreviewKeyDown
        private void txtSec80CCD21819DeductibleAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab) txtSec80D1819GrossAmount.Select(); // SendKeys.Send("{tab}");
            e.IsInputKey = true;
        }
        #endregion


        #region txtSec80CCD21819GrossAmount_PreviewKeyDown
        private void txtSec80CCD21819GrossAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData == Keys.Shift && e.KeyData == Keys.Tab) txtSec80CCD1B1819DeductibleAmount.Select();
            //e.IsInputKey = true;
        }
        #endregion


        #region chkTaxation115BAC_Leave
        private void chkTaxation115BAC_Leave(object sender, EventArgs e)
        {
            txtGSSec17_1.Select();
        }
        #endregion


        #region txtOtherIncomeAmt1_Validating
        private void txtOtherIncomeAmt1_Validating(object sender, CancelEventArgs e)
        {
            //if (cmnService.J_ReturnDoubleValue(txtOtherIncomeAmt1.Text) > TDSMAN.Classes.TDSMAN.T_INCOME_LOSS_HOUSE_PROP_SD_MAX_LIMIT)
            //{
            //    ErrProvider.SetError(txtOtherIncomeAmt1, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_INCOME_LOSS_HOUSE_PROP_SD_MAX_LIMIT)));
            //}
            //else
            //    ErrProvider.SetError(txtOtherIncomeAmt1, "");
        }
        #endregion

        #region txtDedEntAllowance_Validating
        private void txtDedEntAllowance_Validating(object sender, CancelEventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2020_21ID)
            //{
            //    if (cmnService.J_ReturnDoubleValue(txtDedEntAllowance.Text) > TDSMAN.Classes.TDSMAN.T_US_16_EA_SD_MAX_LIMIT)
            //    {
            //        ErrProvider.SetError(txtDedEntAllowance, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_US_16_EA_SD_MAX_LIMIT)));
            //    }
            //    else
            //        ErrProvider.SetError(txtDedEntAllowance, "");
            //}
            //else
            //    ErrProvider.SetError(txtDedTaxEmployment, "");
        }
        #endregion


        #region txtDedTaxSection16ia_Validating
        private void txtDedTaxSection16ia_Validating(object sender, CancelEventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(txtDedTaxSection16ia.Text) > TDSMAN.Classes.TDSMAN.T_US_16_IA_SD_MAX_LIMIT)
            {
                ErrProvider.SetError(txtDedTaxSection16ia, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_US_16_IA_SD_MAX_LIMIT)));
            }
            else
                ErrProvider.SetError(txtDedTaxSection16ia, "");
        }
        #endregion


        #region txtDedTaxEmployment_Validating
        private void txtDedTaxEmployment_Validating(object sender, CancelEventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(txtDedTaxEmployment.Text) > TDSMAN.Classes.TDSMAN.T_US_16_TE_SD_MAX_LIMIT)
            {
                ErrProvider.SetError(txtDedTaxEmployment, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_US_16_TE_SD_MAX_LIMIT)));
            }
            else
                ErrProvider.SetError(txtDedTaxEmployment, "");
        }
        #endregion



        #region txtSec80CCD1B1819DeductibleAmount_Validating
        private void txtSec80CCD1B1819DeductibleAmount_Validating(object sender, CancelEventArgs e)
        {
            //if (cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text) > TDSMAN.Classes.TDSMAN.T_CVIA_SEC80CCD_1B_SD_MAX_LIMIT)
            //{
            //    ErrProvider.SetError(txtSec80CCD1B1819DeductibleAmount, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_CVIA_SEC80CCD_1B_SD_MAX_LIMIT)));
            //}
            //else
            //    ErrProvider.SetError(txtSec80CCD1B1819DeductibleAmount, "");
        }
        #endregion


        #region txtSec80D1819DeductibleAmount_Validating
        private void txtSec80D1819DeductibleAmount_Validating(object sender, CancelEventArgs e)
        {
            if (cmbEmployeeCategory.Text == T_EmployeeCategory.SeniorCitizen)
            {
                if (cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) > TDSMAN.Classes.TDSMAN.T_CVIA_SEC80D_SD_SENIOR_MAX_LIMIT)
                {
                    ErrProvider.SetError(txtSec80D1819DeductibleAmount, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_CVIA_SEC80D_SD_SENIOR_MAX_LIMIT)) + " for senior citizens.\n" +
                            "However it is relaxed for physically handicapped and for other cases...");
                }
                else
                    ErrProvider.SetError(txtSec80D1819DeductibleAmount, "");
            }
            else
            {
                if (cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) > TDSMAN.Classes.TDSMAN.T_CVIA_SEC80D_SD_MAX_LIMIT)
                {
                    ErrProvider.SetError(txtSec80D1819DeductibleAmount, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_CVIA_SEC80D_SD_MAX_LIMIT)) + ".\n" +
                        "However it is relaxed for physically handicapped, senior citizens and for other cases...");
                }
                else
                    ErrProvider.SetError(txtSec80D1819DeductibleAmount, "");
            }
            //else
            //    ErrProvider.SetError(txtSec80D1819DeductibleAmount, "");
        }
        #endregion


        #region txtSec80D1819DeductibleAmount_Validating
        private void txtSec80TTA1819DeductibleAmount_Validating(object sender, CancelEventArgs e)
        {
            //if (cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text) > TDSMAN.Classes.TDSMAN.T_CVIA_SEC80TTA_SD_MAX_LIMIT)
            //{
            //    ErrProvider.SetError(txtSec80TTB1819DeductibleAmount, "Please check...\nMaximum amount limited to " + string.Format("{0:0.00}", Convert.ToDouble(TDSMAN.Classes.TDSMAN.T_CVIA_SEC80TTA_SD_MAX_LIMIT)));
            //}
            //else
            //    ErrProvider.SetError(txtSec80TTB1819DeductibleAmount, "");
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

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
            //Added by Indrajit on 04-03-2013
            TextBox txtsender = (TextBox)sender;
            if (txtsender.Name == "txtSlNoSearch")
            {
                if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,0", txtSlNoSearch, "") == false)
                    e.Handled = true;
            }
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            //if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
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
        #endregion        

        #region ClearControlsHeader
        private void ClearControlsHeader()
        {
            txtEmployeeName.Text = "";
            txtEmployeePAN.Text = "";
            txtEmpRefNo.Text = "";
            //--
            string[] strEmployeeCategory = { T_EmployeeCategory.SeniorCitizen, T_EmployeeCategory.SuperSeniorCitizen };
            dmlService.J_PopulateComboBox(strEmployeeCategory, ref cmbEmployeeCategory, 1, J_ComboBoxSelectedIndex.YES);
            //--------------------            
            //mskEmloyeeFromDate.Text = "";
            //mskEmloyeeToDate.Text = "";
        }

        #endregion

        #region ClearControls

        private void ClearControls()
        {
            //
            ErrProvider.Clear();
            //
            chkTaxation115BAC.Checked = false;
            txtGSSec17_1.Text = "0.00";
            lblTotalGS.Text = "0.00";
            txtDedTaxEmployment.Text = "0.00";
            lblTotalDeductions.Text = "0.00";
            lblIncomeChargeable.Text = "0.00";
            lblGrossTotalIncome.Text = "0.00";
            lblTotalTaxableIncome.Text = "0.00";
            txtTaxTotalIncome.Text = "0.00";
            txtSurcharge.Text = "0.00";
            txtEducationCess.Text = "0.00";
            lblGrossTaxPayable.Text = "0.00";
            txtReliefUS89.Text = "0.00";
            lblNetTaxPayable.Text = "0.00";
            // SEARCH
            txtSlNoSearch.Text = "";
            txtEmployeeNameSearch.Text = "";
            txtTotalDeductedSearch.Text = "";
            txtPANSearch.Text = "";
            txtRefernceNoSearch.Text = "";
            //--
            txtDedTaxSection16ia.Text = "0.00"; //-- 2018/11/12
            //----
            txtSec80C1819GrossAmount.Text = "0.00";
            txtSec80C1819DeductibleAmount.Text = "0.00";
            txtSec80CCC1819GrossAmount.Text = "0.00";
            txtSec80CCC1819DeductibleAmount.Text = "0.00";
            txtSec80CCD11819GrossAmount.Text = "0.00";
            txtSec80CCD11819DeductibleAmount.Text = "0.00";
            txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text = "0.00";
            txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text = "0.00";
            txtSec80CCD1B1819GrossAmount.Text = "0.00";
            txtSec80CCD1B1819DeductibleAmount.Text = "0.00";
            txtSec80D1819GrossAmount.Text = "0.00";
            txtSec80D1819DeductibleAmount.Text = "0.00";
            txtSec80E1819GrossAmount.Text = "0.00";
            txtSec80E1819DeductibleAmount.Text = "0.00";
            txtSec80G1819GrossAmount.Text = "0.00";
            txtSec80G1819QualifyingAmount.Text = "0.00";
            txtSec80G1819DeductibleAmount.Text = "0.00";
            txtSec80TTB1819GrossAmount.Text = "0.00";
            txtSec80TTB1819QualifyingAmount.Text = "0.00";
            txtSec80TTB1819DeductibleAmount.Text = "0.00";
            //
            txtSec80DDGrossAmount.Text = "0.00";
            txtSec80DDQualifyingAmount.Text = "0.00";
            txtSec80DDDeductibleAmount.Text = "0.00";
            //
            txtSec80EEGrossAmount.Text = "0.00";
            txtSec80EEQualifyingAmount.Text = "0.00";
            txtSec80EEDeductibleAmount.Text = "0.00";
            //
            txtSec80EEAGrossAmount.Text = "0.00";
            txtSec80EEAQualifyingAmount.Text = "0.00";
            txtSec80EEADeductibleAmount.Text = "0.00";
            //
            txtSec80GGGrossAmount.Text = "0.00";
            txtSec80GGQualifyingAmount.Text = "0.00";
            txtSec80GGDeductibleAmount.Text = "0.00";
            //
            txtSec80GGCGrossAmount.Text = "0.00";
            txtSec80GGCQualifyingAmount.Text = "0.00";
            txtSec80GGCDeductibleAmount.Text = "0.00";
            //
            txtSec80UGrossAmount.Text = "0.00";
            txtSec80UQualifyingAmount.Text = "0.00";
            txtSec80UDeductibleAmount.Text = "0.00";
            //
            txtSecOthersGrossAmount.Text = "0.00";
            txtSecOthersQualifyingAmount.Text = "0.00";
            txtSecOthersDeductibleAmount.Text = "0.00";
            //
            lblSecChVIATotalAmt.Text = "0.00";
            txtRebateSec87.Text = "0.00";
            txtInterestIncome.Text = "0.00";
        }

        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails.CurrentRow.Index)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "COMPANY_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        //if (Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails.CurrentRow.Index)) < 0)
                        //{
                        //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                        //    if (dsetGridClone == null) return false;
                        //    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "SALARY_DETAILS_ID", lngSearchId);
                        //    dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone, "COMPANY_ID", lngSearchId);
                        //    return false;
                        //}
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtSlNoSearch.Text.Trim() == "" &&
                            txtEmployeeNameSearch.Text.Trim() == "" &&
                            txtTotalDeductedSearch.Text.Trim() == "" &&
                            txtPANSearch.Text.Trim() == "" &&
                            txtRefernceNoSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtSlNoSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- EMPLOYEE NAME
                    //-----------------------------------------------------------------------
                    if (txtEmployeeName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Employee Name - Cannot be Blank");
                        txtEmployeeName.Select();
                        return false;
                    }
                    // CHECK '^'
                    if (TdsMan.T_DetectCaret(txtEmployeeName.Text.Trim(), "^") == true)
                    {
                        cmnService.J_UserMessage("Employee Name - '^' not allowed");
                        txtEmployeeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PAN No. - Cannot be Blank");
                        txtEmployeePAN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("PAN - Cannot be Blank");
                        txtEmployeePAN.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- PAN FORMAT
                    //-----------------------------------------------------------------------
                    if (txtEmployeePAN.Text.Length != 10)
                    {
                        cmnService.J_UserMessage("PAN No. should be of 10 characters");
                        txtEmployeePAN.Select();
                        return false;
                    }
                    if (txtEmployeePAN.Text != "PANNOTAVBL")
                    {
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtEmployeePAN.Text, 5), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtEmployeePAN.Text, 5, 4), J_DataType.Numeric) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                        //---------------------------
                        if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtEmployeePAN.Text, 1), J_DataType.Character) == false)
                        {
                            cmnService.J_UserMessage("Incorrect Format of the PAN");
                            txtEmployeePAN.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- CATEGORY
                    //-----------------------------------------------------------------------
                    if (cmbEmployeeCategory.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("EMPLOYEE CATEGORY - Cannot be Blank");
                        cmbEmployeeCategory.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- EMPLOYEE REFERENCE NO
                    //-----------------------------------------------------------------------
                    if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true) //-- 2020/06/05
                    {
                        if (txtEmpRefNo.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Employee Reference No. - Cannot be Blank");
                            txtEmpRefNo.Select();
                            return false;
                        }
                    }
                    //-----------------------------------------------------------------------
                    //-- DUPLICATE ENTRY
                    //-----------------------------------------------------------------------
                    strSQL = "SELECT TRN_SALARY_DETAILS_194P.EMPLOYEE_ID " +
                        "     FROM   MST_EMPLOYEE," +
                        "            TRN_SALARY_DETAILS_194P," +
                        "            TRN_BASIC_INFO " +
                        "     WHERE  MST_EMPLOYEE.EMPLOYEE_ID         = TRN_SALARY_DETAILS_194P.EMPLOYEE_ID " +
                        "     AND    TRN_SALARY_DETAILS_194P.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID " +
                        "     AND    TRN_BASIC_INFO.ASST_ID           = " + TDSMAN.Classes.TDSMAN.T_pFinancialYearId + " " +
                        "     AND    TRN_BASIC_INFO.COMPANY_ID        = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                        "     AND    TRN_BASIC_INFO.QTR               ='" + T_Qtr.Q4 + "' " +
                        "     AND    TRN_BASIC_INFO.FORM_NO           ='" + T_FormNo.F24Q + "' " +
                        "     AND    MST_EMPLOYEE.EMPLOYEE_NAME       ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "' " +
                        "     AND    MST_EMPLOYEE.EMPLOYEE_PAN        ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "' ";
                    if (lblMode.Text == J_Mode.Edit)
                        strSQL = strSQL + "AND TRN_SALARY_DETAILS_194P.EMPLOYEE_ID <> " + cmnService.J_ReturnInt32Value(txtEmployeeID.Text);
                    //--
                    if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    {
                        cmnService.J_UserMessage("Data of the same Employee exists");
                        txtEmployeeName.Select();
                        return false;
                    }
                    //-----------------------------------------------------------------------
                    //-- TOTAL SALARY BALANCE [-ve]
                    //if (cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text) < 0)
                    //{
                    //    cmnService.J_UserMessage("Total Salary Balance - Cannot be less than zero");
                    //    txtAllowanceSecAmt1.Select();
                    //    return false;
                    //}

                    // Commented by Shrey Kejriwal on 03-04-2015
                    ////-- WARNING MESSAGE
                    //if (cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text) > (cmnService.J_ReturnDoubleValue(txtTaxableAmount.Text) + cmnService.J_ReturnDoubleValue(txtReportedTaxableAmount.Text)))
                    //{
                    //    if (cmnService.J_UserMessage("Balance greater than Current & Previous..\n Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //    {
                    //        txtTaxableAmount.Select();
                    //        return false;
                    //    }
                    //}


                    //Added by Shrey Kejriwal on 03-04-2015
                    //Checking sum of Prev + Curr Employer Salary = Total Salary
                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2013_14ID)
                    {
                        if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2018_19ID) //-- 2019/05/15
                        {
                            //if (cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text) != (cmnService.J_ReturnDoubleValue(txtTaxableAmount.Text) + cmnService.J_ReturnDoubleValue(txtReportedTaxableAmount.Text)))
                            //{
                            //    cmnService.J_UserMessage("Sum of Current & Previous Employer's Salary should be equal to Balance.");
                            //    txtTaxableAmount.Select();
                            //    return false;

                            //}
                        }
                    }

                    ////-- INCOME CHARGEABLE [-ve]
                    if (cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text) < 0)
                    {
                        //cmnService.J_UserMessage("Income Chargeable under the head 'Salaries' - Cannot be less than zero");
                        ////
                        //if (rbnMandatoryOptions.Checked == true)
                        //    txtTotalSalaryBalance.Select();
                        //else if (rbnDetailOptions.Checked == true)
                        //    txtGSSec17_1.Select();
                        //txtDedEntAllowance.Select();
                        return false;
                    }
                    //-- GROSS TOTAL INCOME [-ve]
                    //if (cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) < 0)
                    //{
                    //    cmnService.J_UserMessage("Gross Total Income - Cannot be less than zero");
                    //    txtOtherIncomeAmt1.Select();
                    //    return false;
                    //}
                    // Added by Shrey Kejriwal on 23-09-2014
                    //-- TAXABLE INCOME 
                    //if (cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text) > 999999999.00)//-- COMMENTED ON 2025/07/11
                    if (cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text) > 9999999999.00)
                    {
                        //cmnService.J_UserMessage("Taxable Income - Cannot be more than 99,99,99,999.00");
                        cmnService.J_UserMessage("Taxable Income - Cannot be more than 9,99,99,99,999.00");
                        return false;
                    }
                    //-- TAX ON TOTAL INCOME
                    if (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) > 0)
                    {
                        if (cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) > cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text))
                        {
                            cmnService.J_UserMessage("Tax on Total Income - Cannot be less than Total Taxable Income");
                            txtTaxTotalIncome.Select();
                            return false;
                        }
                    }
                    //-- NEW VALIDATIONS AS PER NEW FVU 3.1 (ADDED BY SHREY)--
                    //--SURCHARGE
                    if (cmnService.J_ReturnDoubleValue(txtSurcharge.Text) > cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text))
                    {
                        cmnService.J_UserMessage("Surcharge on Total Income - Cannot be less than Total Taxable Income");
                        txtSurcharge.Select();
                        return false;
                    }
                    //--EDUCATION CESS
                    if (cmnService.J_ReturnDoubleValue(txtEducationCess.Text) > cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text))
                    {
                        cmnService.J_UserMessage("Education Cess on Total Income - Cannot be less than Total Taxable Income");
                        txtEducationCess.Select();
                        return false;
                    }
                    //-- NET TAX PAYABLE
                    if (cmnService.J_ReturnDoubleValue(lblNetTaxPayable.Text) > cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text))
                    {
                        cmnService.J_UserMessage("Net Tax Payable on Total Income - Cannot be less than Total Taxable Income");
                        txtTaxTotalIncome.Select();
                        return false;
                    }
                    //@@ 2019/04/03 @@@@@@@@
                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                    {
                        strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + TDSMAN.Classes.TDSMAN.T_pFinancialYearId;
                        double dblUS16ia = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                        if (Convert.ToDouble(txtDedTaxSection16ia.Text) > dblUS16ia)
                        {
                            cmnService.J_UserMessage("Section 16(ia) value should not be above " + string.Format("{0:0.00}", dblUS16ia));
                            txtDedTaxSection16ia.Select();
                            return false;
                        }
                    }
                    //@@@@@@@@@@@@@@@@@@@@@@@
                    //-- WARNING MESSAGE
                    //-- ANIK @ 2019/04/24
                    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                    {
                        if (cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text) > dblMAXTotalDeductibleAmount80C80CCC80CCCD1819)
                        {
                            cmnService.J_UserMessage("Aggregate amount of deduction under section 80C,80CCC and 80CCD(1) should not be more than " + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 + ".00");
                            txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Select();
                            return false;
                        }
                        //-- 2019/05/16
                        if ((cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text) + cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text)) > dblMAXTotalDeductibleAmount80C80CCC80CCCD1819)
                        {
                            cmnService.J_UserMessage("Aggregate amount of deduction under section 80C,80CCC and 80CCD(1) should not be more than " + dblMAXTotalDeductibleAmount80C80CCC80CCCD1819 + ".00");
                            if (cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text) > 0)
                                txtSec80C1819DeductibleAmount.Select();
                            else if (cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text) > 0)
                                txtSec80CCC1819DeductibleAmount.Select();
                            else
                                txtSec80CCD11819DeductibleAmount.Select();
                            //
                            return false;
                        }
                        //-- 2019/10/16 FVU 6.4
                        if (cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80C1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80C1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80CCC1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80CCC1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80CCD11819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80CCD11819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80CCD1B1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        //if (cmnService.J_ReturnDoubleValue(txtSec80CCD21819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80CCD21819GrossAmount.Text))
                        //{
                        //    cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                        //    txtSec80CCD21819DeductibleAmount.Select();
                        //    return false;
                        //}
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80D1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80D1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80E1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80E1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80E1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80G1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80G1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80G1819QualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80G1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80G1819QualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80G1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80G1819QualifyingAmount.Select();
                            return false;
                        }
                        //-- 80TTB
                        if (cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80TTB1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80TTB1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80TTB1819QualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80TTB1819DeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80TTB1819QualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80TTB1819GrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80TTB1819QualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80DD
                        if (cmnService.J_ReturnDoubleValue(txtSec80DDDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80DDGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80DDDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80DDDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80DDQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80DDDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80DDQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80DDGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80DDQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80EE
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80EEDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80EEDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80EEQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80EEA
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEADeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEAGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80EEADeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEADeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEAQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80EEADeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80EEAQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80EEAGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80EEAQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80GG
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80GGDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80GGDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80GGQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80GGC
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGCDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGCGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80GGCDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGCDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGCQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80GGCDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80GGCQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80GGCGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80GGCQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- 80U
                        if (cmnService.J_ReturnDoubleValue(txtSec80UDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80UGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSec80UDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80UDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80UQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSec80UDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSec80UQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSec80UGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSec80UQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //-- OTHERS
                        if (cmnService.J_ReturnDoubleValue(txtSecOthersDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSecOthersGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Gross Amount.");
                            txtSecOthersDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSecOthersDeductibleAmount.Text) > cmnService.J_ReturnDoubleValue(txtSecOthersQualifyingAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Deductible Amount should be less than Total Qualifying Amount.");
                            txtSecOthersDeductibleAmount.Select();
                            return false;
                        }
                        //
                        if (cmnService.J_ReturnDoubleValue(txtSecOthersQualifyingAmount.Text) > cmnService.J_ReturnDoubleValue(txtSecOthersGrossAmount.Text))
                        {
                            cmnService.J_UserMessage("Total Qualifying Amount should be less than Total Gross Amount.");
                            txtSecOthersQualifyingAmount.Select();
                            return false;
                        }
                        //--
                        //
                    }
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    // EDIT MODE CHECKS
                    if (lblMode.Text == J_Mode.Edit && rbnDetailOptions.Checked == true && rbnMandatoryOptions.Enabled == true)
                    {
                        //-- 3. BALANCE
                        //if(cmnService.J_ReturnDoubleValue(txtTotalSalaryBalanceEdit.Text) != cmnService.J_ReturnDoubleValue(txtTotalSalaryBalance.Text))
                        //{
                        //    cmnService.J_UserMessage("Mismatch in <Total Salary Balance> provided with Form 16 details");
                        //    txtGSSec17_1.Select();
                        //    return false;
                        //}
                        ////-- 7. TOTAL
                        //if (cmnService.J_ReturnDoubleValue(txtTotalOtherIncomeEdit.Text) != cmnService.J_ReturnDoubleValue(txtTotalOtherIncome.Text))
                        //{
                        //    cmnService.J_UserMessage("Mismatch in <Other Income Details> provided with Form 16 details");
                        //    txtOtherIncomeAmt1.Select();
                        //    return false;
                        //}
                        ////-- TOTAL DEDUCTIBLE AMOUNT 80CCE
                        //if (cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmount80CCEEdit.Text) != cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmount80CCE.Text))
                        //{
                        //    cmnService.J_UserMessage("Mismatch in <Total Deductible Amount 80CCE> provided with Form 16 details");
                        //    txtSec80CAmt1.Select();
                        //    return false;
                        //}
                        ////-- Total Deductible Amount Others
                        //if (cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmountOSEdit.Text) != cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmountOS.Text))
                        //{
                        //    cmnService.J_UserMessage("Mismatch in <Total Deductible Amount Others> provided with Form 16 details");
                        //    txtOSDeductibleAmount1.Select();
                        //    return false;
                        //}                        
                    }
                }
                //-- 2020/02/10
                if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                {
                    //-- 2019/05/14
                    if (Convert.ToDouble(txtRebateSec87.Text) > Convert.ToDouble(txtTaxTotalIncome.Text))
                    {
                        cmnService.J_UserMessage("Rebate can not be more than Tax Total Income", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        txtTaxTotalIncome.Select();
                        return false;
                    }
                }
                //-- 2020/05/25
                if (cmnService.J_ReturnDoubleValue(lblRebateCalculated.Text) > cmnService.J_ReturnDoubleValue(txtRebateSec87.Text))
                {
                    if (cmnService.J_UserMessage("Calculated Rebate : " + lblRebateCalculated.Text + ",\nEntered Rebate : " + txtRebateSec87.Text + "\nAre you sure you want to continue ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        txtRebateSec87.Select();
                        return false;
                    }
                }
            
                    //                    
                    return true;
        }
    
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion   

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                //--------------------------------------------
                long lngEmployeeID = 0;
                int intEntryMode = 0;                
                if (rbnDetailOptions.Checked == true)
                    intEntryMode = 1;
                //
                int intRoundOffTaxableAmount = 0; double dblTotalIncomeRounded = 0;
                //-- SUPERANNUATION FUND
                string strYNSuperannuationFund = ""; string strNameSuperannuationFund = ""; string strFromSuperannuationFund = "NULL"; string strToSuperannuationFund = "NULL";
                string strAmountSuperannuationFund = ""; string strRateSuperannuationFund = ""; string strTaxSuperannuationFund = ""; string strTotalIncomeSuperannuationFund = "";
                //-- LANDLORD
                string strLandlordYN = ""; string strLandlordCount = ""; string strLandlordPAN1 = ""; string strLandlordPAN2 = ""; string strLandlordPAN3 = ""; string strLandlordPAN4 = "";
                string strLandlordName1 = ""; string strLandlordName2 = ""; string strLandlordName3 = ""; string strLandlordName4 = ""; int intLandlordCount = 0;
                //-- LENDER
                string strLenderYN = ""; string strLenderCount = ""; string strLenderPAN1 = ""; string strLenderPAN2 = ""; string strLenderPAN3 = ""; string strLenderPAN4 = "";
                string strLenderName1 = ""; string strLenderName2 = ""; string strLenderName3 = ""; string strLenderName4 = ""; int intLenderCount = 0;
                //-- SEC 80C, 80CCC, 80CCD, OTHERS
                double dblSec80CGrossTotal = 0, dblSec80CDedTotal = 0, dblSec80CCCGrossTotal = 0, dblSec80CCCDedTotal = 0, dblSec80CCDGrossTotal = 0, dblSec80CCDDedTotal = 0, dblSec80C80CCC80CCD1DeductibleAmount = 0;
                double dblChVIAGrossAmount1 = 0, dblChVIAQualAmount1 = 0, dblChVIADedAmount1 = 0; string strChVIADesc1 = "";
                double dblChVIAGrossAmount2 = 0, dblChVIAQualAmount2 = 0, dblChVIADedAmount2 = 0; string strChVIADesc2 = "";
                double dblChVIAGrossAmount3 = 0, dblChVIAQualAmount3 = 0, dblChVIADedAmount3 = 0; string strChVIADesc3 = "";
                double dblChVIAGrossAmount4 = 0, dblChVIAQualAmount4 = 0, dblChVIADedAmount4 = 0; string strChVIADesc4 = "";
                double dblChVIAGrossAmount5 = 0, dblChVIAQualAmount5 = 0, dblChVIADedAmount5 = 0; string strChVIADesc5 = "";
                double dblUS10OthersTotal = 0, dblTotalChVIAOthersAmount = 0, dblTotalChVIAAmount = 0, dblTotalTaxIncome=0;
                //double dblChVIAGrossAmount6 = 0, dblChVIAQualAmount6 = 0, dblChVIADedAmount6 = 0; string strChVIADesc6 = "";
                int intTaxation115BAC = 0;
                //--------------------------------------------
                switch (lblMode.Text)
                {
                    #region ADD
                    case J_Mode.Add:
                        //***For Insert
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        //-----------------------------------------------------------
                        //if (TdsMan.LimitEditions(lngBasicInfoID, false, true, false, "", "") == false)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //--
                        if (ValidateFields() == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref txtEmployeeName) == true)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        //-- GET EMPLOYEE_ID
                        strSQL = "SELECT EMPLOYEE_ID " +
                            "     FROM   MST_EMPLOYEE " +
                            "     WHERE  COMPANY_ID    = " + TDSMAN.Classes.TDSMAN.T_pCompanyId + " " +
                            "     AND    EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                            "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'";
                        //--

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
                        else
                        {
                            strSQL = "UPDATE MST_EMPLOYEE " +
                                   "  SET    CATEGORY    = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "' " +
                                   "  WHERE  EMPLOYEE_ID = " + lngEmployeeID;
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                        }
                        //-----------------------------------------------------------
                        //-----------------------------------------------
                        lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId,
                                            TDSMAN.Classes.TDSMAN.T_pQuarter,
                                            TDSMAN.Classes.TDSMAN.T_pCompanyId,
                                            TDSMAN.Classes.TDSMAN.T_pFormNo);
                        //-----------------------------------------------
                        if (lngBasicInfoID == 0)
                        {
                            if (TdsMan.T_GenerateBasicInfoId(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId,
                                                    TDSMAN.Classes.TDSMAN.T_pQuarter,
                                                    TDSMAN.Classes.TDSMAN.T_pCompanyId,
                                                    TDSMAN.Classes.TDSMAN.T_pFormNo) == true)
                            {
                                //-----------------------------------------------
                                lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId,
                                                    TDSMAN.Classes.TDSMAN.T_pQuarter,
                                                    TDSMAN.Classes.TDSMAN.T_pCompanyId,
                                                    TDSMAN.Classes.TDSMAN.T_pFormNo);
                                //-----------------------------------------------
                                InsertCompanyBasicInfo(TDSMAN.Classes.TDSMAN.T_pCompanyId, lngBasicInfoID);
                            }
                            //-----------------------------------------------
                            else
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                        }
                        //-- 2019/04/25
                        #region 18-19 ONWARDS SEC 80C, 80CCC, 80CCD
                        int intEnable1819Form16B = 0;
                        if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID)
                        {
                            //    dblSec80CGrossTotal = cmnService.J_ReturnDoubleValue(txtSec80C1819GrossAmount.Text.Trim());
                            //    dblSec80CDedTotal = cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text.Trim()); 
                            //    dblSec80CCCGrossTotal = cmnService.J_ReturnDoubleValue(txtSec80CCC1819GrossAmount.Text.Trim()); 
                            //    dblSec80CCCDedTotal = cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text.Trim());
                            //    dblSec80CCDGrossTotal = cmnService.J_ReturnDoubleValue(txtSec80CCD11819GrossAmount.Text.Trim());
                            //    dblSec80CCDDedTotal = cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text.Trim());
                            //    dblSec80C80CCC80CCD1DeductibleAmount = cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text.Trim());
                            //    //dblUS10OthersTotal = cmnService.J_ReturnDoubleValue(txtSec10OtherExemption.Text.Trim());
                            //    //
                            //    //strChVIADesc1 = cmnService.J_ReplaceQuote(txtSecChVIAOthersDesc1.Text.Trim());
                            //    //dblChVIAGrossAmount1 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt1.Text.Trim());
                            //    //dblChVIAQualAmount1 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt1.Text.Trim());
                            //    //dblChVIADedAmount1 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt1.Text.Trim());
                            //    //strChVIADesc2 = cmnService.J_ReplaceQuote(txtSecChVIAOthersDesc2.Text.Trim());
                            //    //dblChVIAGrossAmount2 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt2.Text.Trim());
                            //    //dblChVIAQualAmount2 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt2.Text.Trim());
                            //    //dblChVIADedAmount2 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt2.Text.Trim());
                            //    //strChVIADesc3 = cmnService.J_ReplaceQuote(txtSecChVIAOthersDesc3.Text.Trim());
                            //    //dblChVIAGrossAmount3 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt3.Text.Trim());
                            //    //dblChVIAQualAmount3 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt3.Text.Trim());
                            //    //dblChVIADedAmount3 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt3.Text.Trim());
                            //    //strChVIADesc4 = cmnService.J_ReplaceQuote(txtSecChVIAOthersDesc4.Text.Trim());
                            //    //dblChVIAGrossAmount4 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt4.Text.Trim());
                            //    //dblChVIAQualAmount4 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt4.Text.Trim());
                            //    //dblChVIADedAmount4 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt4.Text.Trim());
                            //    //strChVIADesc5 = cmnService.J_ReplaceQuote(txtSecChVIAOthersDesc5.Text.Trim());
                            //    //dblChVIAGrossAmount5 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersGrossAmt5.Text.Trim());
                            //    //dblChVIAQualAmount5 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersQualifyingAmt5.Text.Trim());
                            //    //dblChVIADedAmount5 = cmnService.J_ReturnDoubleValue(txtSecChVIAOthersDeductibleAmt5.Text.Trim());
                            //    //
                            //    //dblTotalChVIAOthersAmount = cmnService.J_ReturnDoubleValue(txtSecChVIATotalOthersDeductibleAmt.Text);
                            //    dblTotalChVIAAmount = cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text.Trim());
                            //    //
                            //    intEnable1819Form16B = 1;
                            //    //
                            //    dblTotalTaxIncome = cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text.Trim());
                            //}
                            //else
                            //{
                            //    //dblSec80CGrossTotal = cmnService.J_ReturnDoubleValue(lblGS80C.Text.Trim());
                            //    //dblSec80CDedTotal = cmnService.J_ReturnDoubleValue(txtDedTotal80C.Text.Trim());
                            //    //dblSec80CCCGrossTotal = cmnService.J_ReturnDoubleValue(txtSec80CCCGrossAmount.Text.Trim());
                            //    //dblSec80CCCDedTotal = cmnService.J_ReturnDoubleValue(txtSec80CCCDeductibleAmount.Text.Trim());
                            //    //dblSec80CCDGrossTotal = cmnService.J_ReturnDoubleValue(txtSec80CCDGrossAmount.Text.Trim());
                            //    //dblSec80CCDDedTotal = cmnService.J_ReturnDoubleValue(txtSec80CCDDeductibleAmount.Text.Trim());
                            //    //dblSec80C80CCC80CCD1DeductibleAmount = cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmount80CCE.Text.Trim());
                            //    ////
                            //    //dblUS10OthersTotal = cmnService.J_ReturnDoubleValue(lblAllowanceSecAmtTotal.Text.Trim());
                            //    ////
                            //    //strChVIADesc1 = cmnService.J_ReplaceQuote(txtOSDesc1.Text.Trim());
                            //    //dblChVIAGrossAmount1 = cmnService.J_ReturnDoubleValue(txtOSGrossAmount1.Text.Trim());
                            //    //dblChVIAQualAmount1 = cmnService.J_ReturnDoubleValue(txtOSQualifyingAmount1.Text.Trim());
                            //    //dblChVIADedAmount1 = cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount1.Text.Trim());
                            //    //strChVIADesc2 = cmnService.J_ReplaceQuote(txtOSDesc2.Text.Trim());
                            //    //dblChVIAGrossAmount2 = cmnService.J_ReturnDoubleValue(txtOSGrossAmount2.Text.Trim());
                            //    //dblChVIAQualAmount2 = cmnService.J_ReturnDoubleValue(txtOSQualifyingAmount2.Text.Trim());
                            //    //dblChVIADedAmount2 = cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount2.Text.Trim());
                            //    //strChVIADesc3 = cmnService.J_ReplaceQuote(txtOSDesc3.Text.Trim());
                            //    //dblChVIAGrossAmount3 = cmnService.J_ReturnDoubleValue(txtOSGrossAmount3.Text.Trim());
                            //    //dblChVIAQualAmount3 = cmnService.J_ReturnDoubleValue(txtOSQualifyingAmount3.Text.Trim());
                            //    //dblChVIADedAmount3 = cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount3.Text.Trim());
                            //    //strChVIADesc4 = cmnService.J_ReplaceQuote(txtOSDesc4.Text.Trim());
                            //    //dblChVIAGrossAmount4 = cmnService.J_ReturnDoubleValue(txtOSGrossAmount4.Text.Trim());
                            //    //dblChVIAQualAmount4 = cmnService.J_ReturnDoubleValue(txtOSQualifyingAmount4.Text.Trim());
                            //    //dblChVIADedAmount4 = cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount4.Text.Trim());
                            //    //strChVIADesc5 = cmnService.J_ReplaceQuote(txtOSDesc5.Text.Trim());
                            //    //dblChVIAGrossAmount5 = cmnService.J_ReturnDoubleValue(txtOSGrossAmount5.Text.Trim());
                            //    //dblChVIAQualAmount5 = cmnService.J_ReturnDoubleValue(txtOSQualifyingAmount5.Text.Trim());
                            //    //dblChVIADedAmount5 = cmnService.J_ReturnDoubleValue(txtOSDeductibleAmount5.Text.Trim());
                            //    ////
                            //    //dblTotalChVIAOthersAmount = cmnService.J_ReturnDoubleValue(txtTotalDeductibleAmountOS.Text);
                            //    //dblTotalChVIAAmount = cmnService.J_ReturnDoubleValue(lblTotalUCVIA.Text.Trim());
                            //    //
                            //    intEnable1819Form16B = 0;
                            //    //
                            //    dblTotalTaxIncome = cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text.Trim());
                        }
                        #endregion
                        //-------
                        //-- 2020/07/04
                        if (chkTaxation115BAC.Visible == true && chkTaxation115BAC.Checked == true)
                            intTaxation115BAC = 1;
                        else
                            intTaxation115BAC = 0;
                        //--
                        long lngSalarySrl;
                        lngSalarySrl = 0;
                        #region SQL
                        //-----------------------------------------------------------
                        strSQL = @"INSERT INTO TRN_SALARY_DETAILS_194P (BASIC_INFO_ID,
                                                EMPLOYEE_ID,
                                                EMPLOYEE_NAME,
                                                EMPLOYEE_PAN,
                                                EMPLOYEE_CATEGORY,
                                                TS_GS_SEC_17_1,
                                                US_16_III,
                                                US_16_IA,
                                                US_16_TOTAL,
                                                INCOME_CHARGEABLE,
                                                INTEREST_INCOME,
                                                GROSS_TOTAL_INCOME,
                                                CVIA_SEC80C_GROSS_AMOUNT,
                                                CVIA_SEC80C_DED_AMOUNT,
                                                CVIA_SEC80CCC_GROSS_AMOUNT,
                                                CVIA_SEC80CCC_DED_AMOUNT,
                                                CVIA_SEC80CCD_GROSS_AMOUNT,
                                                CVIA_SEC80CCD_DED_AMOUNT,
                                                CVIA_TOTAL_80C_80CCC_80CCD_GROSS_AMOUNT,
                                                CVIA_TOTAL_80C_80CCC_80CCD_DED_AMOUNT,
                                                CVIA_SEC80CCD1B_GROSS_AMOUNT,
                                                CVIA_SEC80CCD1B_DED_AMOUNT,
                                                CVIA_SEC80D_GROSS_AMOUNT,
                                                CVIA_SEC80D_DED_AMOUNT,
                                                CVIA_SEC80E_GROSS_AMOUNT,
                                                CVIA_SEC80E_DED_AMOUNT,
                                                CVIA_SEC80G_QUAL_AMOUNT,
                                                CVIA_SEC80G_GROSS_AMOUNT,
                                                CVIA_SEC80G_DED_AMOUNT,
                                                CVIA_SEC80TTB_QUAL_AMOUNT,
                                                CVIA_SEC80TTB_GROSS_AMOUNT,
                                                CVIA_SEC80TTB_DED_AMOUNT,
                                                CVIA_SEC80DD_QUAL_AMOUNT,
                                                CVIA_SEC80DD_GROSS_AMOUNT,
                                                CVIA_SEC80DD_DED_AMOUNT,
                                                CVIA_SEC80EE_QUAL_AMOUNT,
                                                CVIA_SEC80EE_GROSS_AMOUNT,
                                                CVIA_SEC80EE_DED_AMOUNT,
                                                CVIA_SEC80EEA_QUAL_AMOUNT,
                                                CVIA_SEC80EEA_GROSS_AMOUNT,
                                                CVIA_SEC80EEA_DED_AMOUNT,
                                                CVIA_SEC80GG_QUAL_AMOUNT,
                                                CVIA_SEC80GG_GROSS_AMOUNT,
                                                CVIA_SEC80GG_DED_AMOUNT,
                                                CVIA_SEC80GGC_QUAL_AMOUNT,
                                                CVIA_SEC80GGC_GROSS_AMOUNT,
                                                CVIA_SEC80GGC_DED_AMOUNT,
                                                CVIA_SEC80U_QUAL_AMOUNT,
                                                CVIA_SEC80U_GROSS_AMOUNT,
                                                CVIA_SEC80U_DED_AMOUNT,
                                                CVIA_OTHERS_QUAL_AMOUNT,
                                                CVIA_OTHERS_GROSS_AMOUNT,
                                                CVIA_OTHERS_DED_AMOUNT,
                                                CVIA_AGGREGATE_DED_AMOUNT,
                                                TOTAL_TAXABLE_INCOME,
                                                TAX_TOTAL_INCOME,
                                                REBATE_US_87A_AMOUNT,
                                                SCHG_TOTAL_INCOME,
                                                ECESS_TOTAL_INCOME,
                                                TAX_PAYABLE,
                                                US_89_LESS,
                                                NET_TAX_PAYABLE,
                                                SECTION_115BAC_FLAG)
                                    VALUES( " + lngBasicInfoID + "," +
                                    "       " + lngEmployeeID + "," +
                                    "      '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim()) + "'," +
                                    "      '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                    "      '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "'," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtGSSec17_1.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtDedTaxEmployment.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtDedTaxSection16ia.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtInterestIncome.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80C1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text.Trim()) + "," + 
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCC1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCD11819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80D1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80E1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80E1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80G1819QualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80G1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819QualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819GrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80DDQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80DDGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80DDDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEAQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEAGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80EEADeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGCQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGCGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80GGCDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80UQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80UGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSec80UDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSecOthersQualifyingAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSecOthersGrossAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSecOthersDeductibleAmount.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtRebateSec87.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtSurcharge.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtEducationCess.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblGrossTaxPayable.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(txtReliefUS89.Text.Trim()) + "," +
                                    "       " + cmnService.J_ReturnDoubleValue(lblNetTaxPayable.Text.Trim()) + "," +
                                    "       " + intTaxation115BAC + ")";
                        #endregion
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtEmployeeName.Select();
                            dmlService.J_Rollback();
                            return;
                        }

                        //Added by Indrajit on 19-02-2013 
                        //To update the last challan serial no
                        //============================================================                        
                        if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            strSQL = "UPDATE TRN_SALARY_DETAILS_194P " +
                                     "SET SL_NO = DMAX('SL_NO','TRN_SALARY_DETAILS_194P','BASIC_INFO_ID =" + lngBasicInfoID + "')+1 " +
                                     "WHERE SALARY_DETAILS_194P_ID = DMAX('SALARY_DETAILS_194P_ID','TRN_SALARY_DETAILS_194P','')";

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        {
                            long lngSerialNo = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(SL_NO) FROM TRN_SALARY_DETAILS_194P WHERE BASIC_INFO_ID =" + lngBasicInfoID)));
                            long lngSDID = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(SALARY_DETAILS_194P_ID) FROM TRN_SALARY_DETAILS_194P WHERE BASIC_INFO_ID =" + lngBasicInfoID)));
                            //
                            strSQL = "UPDATE TRN_SALARY_DETAILS_194P " +
                                    "SET SL_NO = " + (lngSerialNo + 1) + " " +
                                    "WHERE SALARY_DETAILS_194P_ID = " + lngSDID;
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }

                        }
                        // RECALCALUTING SRL NO.
                        //if (TdsMan.T_UpdateSrlNo(dmlService.J_pCommand, lngBasicInfoID, Updt_SrlNo.SalaryDetailSrlNo) == false)
                        //{
                        //    dmlService.J_Rollback();
                        //    return;
                        //}
                        //=================================================

                        //-- UPDATE LAST WORKED
                        TdsMan.T_UpdateLastWorked(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId, TDSMAN.Classes.TDSMAN.T_pQuarter, TDSMAN.Classes.TDSMAN.T_pFormNo, TDSMAN.Classes.TDSMAN.T_pCompanyId);
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "TRN_SALARY_DETAILS_194P", "SALARY_DETAILS_194P_ID");
                        if (lngSearchId == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControlsHeader();
                        ClearControls();
                        //--
                        txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                        //--
                        LoadSalaryDetailsGrid(lngBasicInfoID);
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone,  lngSearchId);
                        //--
                        cmnService.J_UserMessage("Record Saved");
                        //--
                        BtnRefresh.Enabled = true;
                        BtnRefresh.BackColor = Color.Lavender;
                        //-----------------------------------------------------------
                        txtEmployeeName.Select();
                        pnlSalaryDetailsEntryForm.VerticalScroll.Value = pnlSalaryDetailsEntryForm.VerticalScroll.Minimum;
                        //-----------------------------------------------------------
                        break;
                    #endregion

                    #region EDIT
                    case J_Mode.Edit:
                        //*****  For Modify
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        //Added by Indrajit on 21-02-2013 to check the existance of Challan
                        //-----------------------------------------------------------
                        if (Check_Record(lngSearchId, "TRN_SALARY_DETAILS_194P", "SALARY_DETAILS_194P_ID", ref dgcViewSalaryDetails) == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        if (ValidateFields() == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref txtEmployeeName) == true)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //QUERY MODIFIED BY SHREY KEJRIWAL ON 08/11/2011
                        strSQL = "SELECT EMPLOYEE_ID " +
                            "     FROM   MST_EMPLOYEE " +
                            "     WHERE  EMPLOYEE_NAME ='" + cmnService.J_ReplaceQuote(txtEmployeeName.Text) + "'" +
                            "     AND    EMPLOYEE_PAN  ='" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text) + "'" +
                            "     AND    COMPANY_ID    = " + TDSMAN.Classes.TDSMAN.T_pCompanyId;

                        lngEmployeeID = cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));
                        //-----------------------------------------------------------
                        //ADDED BY SHREY
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
                        else
                        {
                            strSQL = "UPDATE MST_EMPLOYEE " +
                                   "  SET    CATEGORY    = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "' " +
                                   "  WHERE  EMPLOYEE_ID = " + lngEmployeeID;
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                return;
                            }
                        }
                        //--
                        #region SQL
                        strSQL = "UPDATE TRN_SALARY_DETAILS_194P SET " +
                                "        EMPLOYEE_ID = " + lngEmployeeID + "," +
                                "        EMPLOYEE_NAME = '" + cmnService.J_ReplaceQuote(txtEmployeeName.Text.Trim()) + "'," +
                                "        EMPLOYEE_PAN = '" + cmnService.J_ReplaceQuote(txtEmployeePAN.Text.Trim()) + "'," +
                                "        EMPLOYEE_CATEGORY = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmbEmployeeCategory.Text, 1)) + "'," +
                                "        TS_GS_SEC_17_1 = " + cmnService.J_ReturnDoubleValue(txtGSSec17_1.Text) + "," +
                                "        US_16_III      = " + cmnService.J_ReturnDoubleValue(txtDedTaxEmployment.Text) + "," +
                                "        US_16_IA = " + cmnService.J_ReturnDoubleValue(txtDedTaxSection16ia.Text) + "," +
                                "        US_16_TOTAL = " + cmnService.J_ReturnDoubleValue(lblTotalDeductions.Text) + "," +
                                "        INCOME_CHARGEABLE = " + cmnService.J_ReturnDoubleValue(lblIncomeChargeable.Text) + "," +
                                "        INTEREST_INCOME = " + cmnService.J_ReturnDoubleValue(txtInterestIncome.Text) + "," +
                                "        GROSS_TOTAL_INCOME = " + cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text) + "," +
                                "        CVIA_SEC80C_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80C1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80C_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80C1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80CCC_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCC1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80CCC_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCC1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80CCD_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCD11819GrossAmount.Text) + "," +
                                "        CVIA_SEC80CCD_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCD11819DeductibleAmount.Text) + "," +
                                "        CVIA_TOTAL_80C_80CCC_80CCD_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text) + "," +
                                "        CVIA_TOTAL_80C_80CCC_80CCD_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80CCD1B_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80CCD1B_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80CCD1B1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80D_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80D1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80D_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80D1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80E_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80E1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80E_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80E1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80G_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80G1819QualifyingAmount.Text) + "," +
                                "        CVIA_SEC80G_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80G1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80G_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80G1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80TTB_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819QualifyingAmount.Text) + "," +
                                "        CVIA_SEC80TTB_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819GrossAmount.Text) + "," +
                                "        CVIA_SEC80TTB_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80TTB1819DeductibleAmount.Text) + "," +
                                "        CVIA_SEC80DD_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80DDQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80DD_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80DDGrossAmount.Text) + "," +
                                "        CVIA_SEC80DD_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80DDDeductibleAmount.Text) + "," +
                                "        CVIA_SEC80EE_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80EE_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEGrossAmount.Text) + "," +
                                "        CVIA_SEC80EE_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEDeductibleAmount.Text) + "," +
                                "        CVIA_SEC80EEA_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEAQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80EEA_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEAGrossAmount.Text) + "," +
                                "        CVIA_SEC80EEA_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80EEADeductibleAmount.Text) + "," +
                                "        CVIA_SEC80GG_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80GG_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGGrossAmount.Text) + "," +
                                "        CVIA_SEC80GG_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGDeductibleAmount.Text) + "," +
                                "        CVIA_SEC80GGC_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGCQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80GGC_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGCGrossAmount.Text) + "," +
                                "        CVIA_SEC80GGC_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80GGCDeductibleAmount.Text) + "," +
                                "        CVIA_SEC80U_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80UQualifyingAmount.Text) + "," +
                                "        CVIA_SEC80U_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80UGrossAmount.Text) + "," +
                                "        CVIA_SEC80U_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSec80UDeductibleAmount.Text) + "," +
                                "        CVIA_OTHERS_QUAL_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSecOthersQualifyingAmount.Text) + "," +
                                "        CVIA_OTHERS_GROSS_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSecOthersGrossAmount.Text) + "," +
                                "        CVIA_OTHERS_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtSecOthersDeductibleAmount.Text) + "," +
                                "        CVIA_AGGREGATE_DED_AMOUNT = " + cmnService.J_ReturnDoubleValue(lblSecChVIATotalAmt.Text) + "," +
                                "        TOTAL_TAXABLE_INCOME = " + cmnService.J_ReturnDoubleValue(lblTotalTaxableIncome.Text) + "," +
                                "        TAX_TOTAL_INCOME = " + cmnService.J_ReturnDoubleValue(txtTaxTotalIncome.Text) + "," +
                                "        REBATE_US_87A_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtRebateSec87.Text) + "," +
                                "        SCHG_TOTAL_INCOME = " + cmnService.J_ReturnDoubleValue(txtSurcharge.Text) + "," +
                                "        ECESS_TOTAL_INCOME = " + cmnService.J_ReturnDoubleValue(txtEducationCess.Text) + "," +
                                "        TAX_PAYABLE = " + cmnService.J_ReturnDoubleValue(lblGrossTaxPayable.Text) + "," +
                                "        US_89_LESS = " + cmnService.J_ReturnDoubleValue(txtReliefUS89.Text) + "," +
                                "        NET_TAX_PAYABLE = " + cmnService.J_ReturnDoubleValue(lblNetTaxPayable.Text) + "," +
                                "        SECTION_115BAC_FLAG  = " + intTaxation115BAC + " " +
                                " WHERE  SALARY_DETAILS_194P_ID = " + lngSearchId + " ";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            txtEmployeeName.Select();
                            dmlService.J_Rollback();
                            return;
                        }
                        #endregion
                        //--
                        //-- UPDATE LAST WORKED
                        TdsMan.T_UpdateLastWorked(dmlService.J_pCommand, TDSMAN.Classes.TDSMAN.T_pFinancialYearId, TDSMAN.Classes.TDSMAN.T_pQuarter, TDSMAN.Classes.TDSMAN.T_pFormNo, TDSMAN.Classes.TDSMAN.T_pCompanyId);
                        //-----------------------------------------------------------
                        //..........................................................
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
                        ClearControlsHeader();
                        ClearControls();
                        //--
                        txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.Add;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //--
                        BtnDelete.Enabled = true;
                        BtnDelete.BackColor = Color.Lavender;
                        //--
                        BackgroundColorChangeChallan(lblMode.Text);
                        //
                        BtnSearch.Enabled = true;
                        BtnSearch.BackColor = Color.Lavender;
                        //
                        BtnExit.Enabled = true;
                        BtnExit.BackColor = Color.Lavender;
                        //--
                        BtnRefresh.Enabled = true;
                        BtnRefresh.BackColor = Color.Lavender;
                        //--
                        BtnEdit.Enabled = true;
                        BtnEdit.BackColor = Color.Lavender;
                        //-----------------------------------------------------------
                        //DisableControls();
                        //-----------------------------------------------------------
                        //ControlVisible(false);
                        //-----------------------------------------------------------
                        LoadSalaryDetailsGrid(lngBasicInfoID);
                        //dmlService.J_setGridPosition(ref dgcViewSalaryDetails, dsetGridClone,  lngSearchId);
                        break;
                    #endregion

                    #region DELETE
                    case J_Mode.Delete:
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //
                        //lngSearchId = Convert.ToInt64(Convert.ToString(dgcViewSalaryDetails[dgcViewSalaryDetails.CurrentRow.Index, 0]));
                        lngSearchId = Convert.ToInt64(dgcViewSalaryDetails.Rows[dgcViewSalaryDetails.CurrentRow.Index].Cells[0].Value);

                        //Added by Indrajit on 21-02-2013 to check the existance of Deductee
                        //-----------------------------------------------------------
                        if (Check_Record(lngSearchId, "TRN_SALARY_DETAILS_194P", "SALARY_DETAILS_194P_ID", ref dgcViewSalaryDetails) == 0)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //--------------------------------------------------


                        //-----------------------------------------------------------
                        //-- CHECK THE TRANSACTION
                        //-----------------------------------------------------------------------
                        //..........................................................
                        if (cmnService.J_UserMessage("Proceed with Deletion?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            lblMode.Text = J_Mode.Add;
                            dmlService.J_Rollback();
                            return;
                        }
                        //// RECALCALUTING SRL NO.
                        //if (TdsMan.T_UpdateSrlNo(cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SL_NO FROM TRN_SALARY_DETAILS_194P WHERE  SALARY_DETAILS_194P_ID = " + lngSearchId))),
                        //    lngBasicInfoID, Updt_SrlNo.SalaryDetailSrlNo) == false) return;

                        //Added by Shrey Kejriwal on 05/11/2011

                        int intCount = 0;
                        strSQL = "SELECT SL_NO FROM TRN_SALARY_DETAILS_194P WHERE  SALARY_DETAILS_194P_ID = " + lngSearchId;
                        intCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL));


                        strSQL = "UPDATE TRN_SALARY_DETAILS_194P " +
                                 "SET    SL_NO = SL_NO - 1 " +
                                 "WHERE  SL_NO > " + intCount + " " +
                                 "and    BASIC_INFO_ID = " + lngBasicInfoID;

                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            lblMode.Text = J_Mode.Add;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        strSQL = "DELETE FROM TRN_SALARY_DETAILS_194P WHERE SALARY_DETAILS_194P_ID =  " + lngSearchId + "";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            lblMode.Text = J_Mode.Add;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------

                        lblMode.Text = J_Mode.Add;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //
                        BtnEdit.Enabled = false;
                        BtnEdit.BackColor = Color.LightGray;
                        //--
                        BtnExit.Enabled = true;
                        BtnExit.BackColor = Color.Lavender;
                        //--
                        BtnDelete.Enabled = true;
                        BtnDelete.BackColor = Color.Lavender;
                        //--
                        BtnSearch.Enabled = true;
                        BtnSearch.BackColor = Color.Lavender;
                        //--
                        BtnRefresh.Enabled = true;
                        BtnRefresh.BackColor = Color.Lavender;
                        //--
                        LoadSalaryDetailsGrid(lngBasicInfoID);
                        //--
                        txtSalaryDetailSrlNo.Text = Convert.ToString(dmlService.J_ReturnMaxValue("TRN_SALARY_DETAILS_194P", "SL_NO", "BASIC_INFO_ID = " + lngBasicInfoID + "") + 1);
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.dgcViewSalaryDetails, dsetGridClone, lngSearchId);
                        break;
                        #endregion
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region LoadSalaryDetailsGrid

        #region LoadSalaryDetailsGrid
        private void LoadSalaryDetailsGrid(long BasicInfoID)
        {
            string strEmpNameGridSize = "", strEmpRefNoGridSize = "0", strEmpRefNoGridSizeDisplay = "F";
            //
            if (TDSMAN.Classes.TDSMAN.T_ShareReferenceNoF24Q == true)
            {
                strEmpRefNoGridSize = "80";
                strEmpNameGridSize = "120";
                strEmpRefNoGridSizeDisplay = "";
            }
            else
                strEmpNameGridSize = "200";
            //-----------------------------------------------------------
            string[,] strMatrixSalaryDetails = {{"SalaryDetailsID", "0", "", "", "", "F", ""},
                                                {"EmployeeID", "0", "", "", "", "F", ""},
                                                {"Srl No.", "50", "", "", "", "", "T"},
                                                {"Name", strEmpNameGridSize, "S", "", "", "", "T"},
                                                {"Ref No.", strEmpRefNoGridSize, "", "", "", strEmpRefNoGridSizeDisplay, "T"},
                                                {"Net Tax Payable", "95", "0.00", "R", "", "", "T"}};
            //-----------------------------------------------------------
            //strMatrix = strMatrix1;
            //-----------------------------------------------------------
             //(1) Column Value
             //* (2) Column Data Type
             //* (3) Replace String
             //* (4) Replace String Data Type 
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "TRN_SALARY_DETAILS_194P.SL_NO";
            strQuery = "SELECT TRN_SALARY_DETAILS_194P.SALARY_DETAILS_194P_ID  AS SALARY_DETAILS_194P_ID," +
                       "       MST_EMPLOYEE.EMPLOYEE_ID                        AS EMPLOYEE_ID," +
                       "       TRN_SALARY_DETAILS_194P.SL_NO                   AS SL_NO," +
                       "       MST_EMPLOYEE.EMPLOYEE_NAME                      AS EMPLOYEE_NAME," +
                       "       TRN_SALARY_DETAILS_194P.EMPLOYEE_SERIAL_NO      AS EMPLOYEE_SERIAL_NO," +
                       "       TRN_SALARY_DETAILS_194P.NET_TAX_PAYABLE         AS NET_TAX_PAYABLE " +
                       "FROM   TRN_SALARY_DETAILS_194P," +
                       "       MST_EMPLOYEE " +
                       "WHERE  TRN_SALARY_DETAILS_194P.EMPLOYEE_ID    = MST_EMPLOYEE.EMPLOYEE_ID " +
                       "AND    TRN_SALARY_DETAILS_194P.BASIC_INFO_ID  = " + BasicInfoID + " ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewSalaryDetails, strSQL, strMatrixSalaryDetails);       //Show Data into the Grid
        }
        #endregion

        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            //Making the Reader Blocked until the reader gets closed
            blnRestrictIncomeTaxCalculation = true;

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
                string[,] strShowHelpEmployeeMatrix = {{"TRN_SALARY_DETAILS_194P.EMPLOYEE_CATEGORY = 'S'", "F", T_EmployeeCategory.SeniorCitizen, "T"},
                                                       {"TRN_SALARY_DETAILS_194P.EMPLOYEE_CATEGORY = 'O'", "F", T_EmployeeCategory.SuperSeniorCitizen, "T"},
                                                       {"TRN_SALARY_DETAILS_194P.EMPLOYEE_CATEGORY = ''", "F", "", "T"}};
                ////
                strSQL = "SELECT  TRN_SALARY_DETAILS_194P.SALARY_DETAILS_194P_ID                 AS SALARY_DETAILS_ID," +
                    "             TRN_SALARY_DETAILS_194P.BASIC_INFO_ID                          AS BASIC_INFO_ID," +
                    "             TRN_SALARY_DETAILS_194P.EMPLOYEE_ID                            AS EMPLOYEE_ID," +
                    "             MST_EMPLOYEE.EMPLOYEE_NAME                                     AS EMPLOYEE_NAME," +
                    "             MST_EMPLOYEE.EMPLOYEE_PAN                                      AS EMPLOYEE_PAN," +
                    "             MST_EMPLOYEE.EMPLOYEE_REF                                      AS EMPLOYEE_REF," +
                    "            " + cmnService.J_SQLDBFormat(strShowHelpEmployeeMatrix, J_SQLColFormat.Case_End) + " AS EMPLOYEE_CATEGORY," +
                    "             TRN_SALARY_DETAILS_194P.TS_GS_SEC_17_1                         AS TS_GS_SEC_17_1," +
                    "             TRN_SALARY_DETAILS_194P.US_16_III                              AS US_16_III," +
                    "             TRN_SALARY_DETAILS_194P.US_16_IA                               AS US_16_IA," +
                    "             TRN_SALARY_DETAILS_194P.US_16_TOTAL                            AS US_16_TOTAL," +
                    "             TRN_SALARY_DETAILS_194P.INCOME_CHARGEABLE                      AS INCOME_CHARGEABLE," +
                    "             TRN_SALARY_DETAILS_194P.INTEREST_INCOME                        AS INTEREST_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.GROSS_TOTAL_INCOME                     AS GROSS_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80C_GROSS_AMOUNT               AS CVIA_SEC80C_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80C_DED_AMOUNT                 AS CVIA_SEC80C_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCC_GROSS_AMOUNT             AS CVIA_SEC80CCC_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCC_DED_AMOUNT               AS CVIA_SEC80CCC_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCD_GROSS_AMOUNT             AS CVIA_SEC80CCD_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCD_DED_AMOUNT               AS CVIA_SEC80CCD_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_TOTAL_80C_80CCC_80CCD_GROSS_AMOUNT  AS CVIA_TOTAL_80C_80CCC_80CCD_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_TOTAL_80C_80CCC_80CCD_DED_AMOUNT  AS CVIA_TOTAL_80C_80CCC_80CCD_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCD1B_GROSS_AMOUNT           AS CVIA_SEC80CCD1B_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80CCD1B_DED_AMOUNT             AS CVIA_SEC80CCD1B_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80D_GROSS_AMOUNT               AS CVIA_SEC80D_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80D_DED_AMOUNT                 AS CVIA_SEC80D_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80E_GROSS_AMOUNT               AS CVIA_SEC80E_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80E_DED_AMOUNT                 AS CVIA_SEC80E_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80G_QUAL_AMOUNT                AS CVIA_SEC80G_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80G_GROSS_AMOUNT               AS CVIA_SEC80G_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80G_DED_AMOUNT                 AS CVIA_SEC80G_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80TTB_QUAL_AMOUNT              AS CVIA_SEC80TTB_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80TTB_GROSS_AMOUNT             AS CVIA_SEC80TTB_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80TTB_DED_AMOUNT               AS CVIA_SEC80TTB_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80DD_QUAL_AMOUNT               AS CVIA_SEC80DD_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80DD_GROSS_AMOUNT              AS CVIA_SEC80DD_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80DD_DED_AMOUNT                AS CVIA_SEC80DD_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EE_QUAL_AMOUNT               AS CVIA_SEC80EE_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EE_GROSS_AMOUNT              AS CVIA_SEC80EE_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EE_DED_AMOUNT                AS CVIA_SEC80EE_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EEA_QUAL_AMOUNT              AS CVIA_SEC80EEA_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EEA_GROSS_AMOUNT             AS CVIA_SEC80EEA_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80EEA_DED_AMOUNT               AS CVIA_SEC80EEA_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GG_QUAL_AMOUNT               AS CVIA_SEC80GG_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GG_GROSS_AMOUNT              AS CVIA_SEC80GG_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GG_DED_AMOUNT                AS CVIA_SEC80GG_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GGC_QUAL_AMOUNT              AS CVIA_SEC80GGC_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GGC_GROSS_AMOUNT             AS CVIA_SEC80GGC_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80GGC_DED_AMOUNT               AS CVIA_SEC80GGC_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80U_QUAL_AMOUNT                AS CVIA_SEC80U_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80U_GROSS_AMOUNT               AS CVIA_SEC80U_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_SEC80U_DED_AMOUNT                 AS CVIA_SEC80U_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_OTHERS_QUAL_AMOUNT                AS CVIA_OTHERS_QUAL_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_OTHERS_GROSS_AMOUNT               AS CVIA_OTHERS_GROSS_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_OTHERS_DED_AMOUNT                 AS CVIA_OTHERS_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.CVIA_AGGREGATE_DED_AMOUNT              AS CVIA_AGGREGATE_DED_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.TOTAL_TAXABLE_INCOME                   AS TOTAL_TAXABLE_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.TAX_TOTAL_INCOME                       AS TAX_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.REBATE_US_87A_AMOUNT                   AS REBATE_US_87A_AMOUNT," +
                    "             TRN_SALARY_DETAILS_194P.SCHG_TOTAL_INCOME                      AS SCHG_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.ECESS_TOTAL_INCOME                     AS ECESS_TOTAL_INCOME," +
                    "             TRN_SALARY_DETAILS_194P.TAX_PAYABLE                            AS TAX_PAYABLE," +
                    "             TRN_SALARY_DETAILS_194P.US_89_LESS                             AS US_89_LESS," +
                    "             TRN_SALARY_DETAILS_194P.NET_TAX_PAYABLE                        AS NET_TAX_PAYABLE," +
                    "             TRN_SALARY_DETAILS_194P.SECTION_115BAC_FLAG                    AS SECTION_115BAC_FLAG " +
                    "     FROM    TRN_SALARY_DETAILS_194P," +
                    "             MST_EMPLOYEE " +
                    "     WHERE   TRN_SALARY_DETAILS_194P.EMPLOYEE_ID       = MST_EMPLOYEE.EMPLOYEE_ID " +
                    "     AND     TRN_SALARY_DETAILS_194P.SALARY_DETAILS_194P_ID = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    blnShowRecord = true;
                    lngSearchId = Id;

                    txtEmployeeID.Text = Convert.ToString(drdShowRecord["EMPLOYEE_ID"]);

                    blnShowHelp = false;
                    txtEmployeeName.Text = Convert.ToString(drdShowRecord["EMPLOYEE_NAME"]);
                    blnShowHelp = true;

                    blnShowPANHelp = false;
                    txtEmployeePAN.Text = Convert.ToString(drdShowRecord["EMPLOYEE_PAN"]);
                    blnShowPANHelp = true;

                    if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                        if (Convert.ToString(drdShowRecord["EMPLOYEE_REF"]) != "")
                            txtEmpRefNo.Text = string.Format("{0:0000000000}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["EMPLOYEE_REF"])));

                    cmbEmployeeCategory.Text = Convert.ToString(drdShowRecord["EMPLOYEE_CATEGORY"]);
                    //    //
                    //    if (Convert.ToString(drdShowRecord["ENTRY_MODE"]) == "0")
                    //    {
                    //        rbnMandatoryOptions.Enabled = true;
                    //        rbnMandatoryOptions.Checked = true;
                    //    }
                    //    else if (Convert.ToString(drdShowRecord["ENTRY_MODE"]) == "1")
                    //    {
                    //        rbnDetailOptions.Checked = true;
                    //        rbnMandatoryOptions.Enabled = false;
                    //    }
                    //    //
                    //    //-- 2020/07/02
                    if (Convert.ToString(drdShowRecord["SECTION_115BAC_FLAG"]) == "0")
                        chkTaxation115BAC.Checked = false;
                    else if (Convert.ToString(drdShowRecord["SECTION_115BAC_FLAG"]) == "1")
                        chkTaxation115BAC.Checked = true;
                    //    //--
                    txtGSSec17_1.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TS_GS_SEC_17_1"])));
                    lblTotalGS.Text = txtGSSec17_1.Text;
                    txtDedTaxEmployment.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_III"])));
                    txtDedTaxSection16ia.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_IA"])));
                    lblTotalDeductions.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_16_TOTAL"])));
                    lblIncomeChargeable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["INCOME_CHARGEABLE"])));
                    txtInterestIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["INTEREST_INCOME"])));
                    lblGrossTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["GROSS_TOTAL_INCOME"])));
                    txtSec80C1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_GROSS_AMOUNT"])));
                    txtSec80C1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80C_DED_AMOUNT"])));
                    txtSec80CCC1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_GROSS_AMOUNT"])));
                    txtSec80CCC1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCC_DED_AMOUNT"])));
                    txtSec80CCD11819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_GROSS_AMOUNT"])));
                    txtSec80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD_DED_AMOUNT"])));
                    txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_TOTAL_80C_80CCC_80CCD_GROSS_AMOUNT"])));
                    txtTotalDeductionSec80C80CCC80CCD11819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_TOTAL_80C_80CCC_80CCD_DED_AMOUNT"])));
                    txtSec80CCD1B1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD1B_GROSS_AMOUNT"])));
                    txtSec80CCD1B1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80CCD1B_DED_AMOUNT"])));
                    txtSec80D1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80D_GROSS_AMOUNT"])));
                    txtSec80D1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80D_DED_AMOUNT"])));
                    txtSec80E1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80E_GROSS_AMOUNT"])));
                    txtSec80E1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80E_DED_AMOUNT"])));
                    txtSec80G1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_GROSS_AMOUNT"])));
                    txtSec80G1819QualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_QUAL_AMOUNT"])));
                    txtSec80G1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80G_DED_AMOUNT"])));
                    txtSec80TTB1819GrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTB_GROSS_AMOUNT"])));
                    txtSec80TTB1819QualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTB_QUAL_AMOUNT"])));
                    txtSec80TTB1819DeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80TTB_DED_AMOUNT"])));
                    txtSec80DDGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80DD_GROSS_AMOUNT"])));
                    txtSec80DDQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80DD_QUAL_AMOUNT"])));
                    txtSec80DDDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80DD_DED_AMOUNT"])));
                    txtSec80EEGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EE_GROSS_AMOUNT"])));
                    txtSec80EEQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EE_QUAL_AMOUNT"])));
                    txtSec80EEDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EE_DED_AMOUNT"])));
                    txtSec80EEAGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EEA_GROSS_AMOUNT"])));
                    txtSec80EEAQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EEA_QUAL_AMOUNT"])));
                    txtSec80EEADeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80EEA_DED_AMOUNT"])));
                    txtSec80GGGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GG_GROSS_AMOUNT"])));
                    txtSec80GGQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GG_QUAL_AMOUNT"])));
                    txtSec80GGDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GG_DED_AMOUNT"])));
                    txtSec80GGCGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GGC_GROSS_AMOUNT"])));
                    txtSec80GGCQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GGC_QUAL_AMOUNT"])));
                    txtSec80GGCDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80GGC_DED_AMOUNT"])));
                    txtSec80UGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80U_GROSS_AMOUNT"])));
                    txtSec80UQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80U_QUAL_AMOUNT"])));
                    txtSec80UDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_SEC80U_DED_AMOUNT"])));
                    txtSecOthersGrossAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHERS_GROSS_AMOUNT"])));
                    txtSecOthersQualifyingAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHERS_QUAL_AMOUNT"])));
                    txtSecOthersDeductibleAmount.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_OTHERS_DED_AMOUNT"])));
                    lblSecChVIATotalAmt.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["CVIA_AGGREGATE_DED_AMOUNT"])));
                    txtTaxTotalIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_TOTAL_INCOME"])));
                    txtRebateSec87.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["REBATE_US_87A_AMOUNT"])));
                    txtSurcharge.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["SCHG_TOTAL_INCOME"])));
                    txtEducationCess.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["ECESS_TOTAL_INCOME"])));
                    lblGrossTaxPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TAX_PAYABLE"])));
                    txtReliefUS89.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["US_89_LESS"])));
                    lblNetTaxPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["NET_TAX_PAYABLE"])));
                    //    //
                                                                                                                                                                       //    //-- 2020/05/19
                    lblTotalTaxableIncome.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(drdShowRecord["TOTAL_TAXABLE_INCOME"])));
                    //    //
                    //    //if (cmnService.J_ReturnInt32Value(Convert.ToString(drdShowRecord["FORM16B_NEW_FORMAT_18_19"])) > 0)
                    //    //    FORM_DESIGN(true);
                    //    //else
                    //    //    FORM_DESIGN(false);
                    //    //--
                    //    blnVertical = true;
                    //    RoundOff = false;
                    //    //--
                    //    //--------
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //    //cmbState.Text = strStateName;
                    //    //cmbDistrict.Text = strDistrictName;

                    txtEmployeeName.Select();
                    return true;
                }
                ////-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                ////-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                //-----------------------------------------------------------
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewSalaryDetails, strSQL, strMatrix);       //Show Data into the Grid
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

        #region BackgroundColorChangeChallan
        private void BackgroundColorChangeChallan(string Mode)
        {
            if (Mode.ToUpper() == "ADD MODE")
            {
                txtEmployeeName.BackColor = Color.White;
                txtEmployeePAN.BackColor = Color.White;
                txtEmpRefNo.BackColor = Color.White;
                cmbEmployeeCategory.BackColor = Color.White;
                //mskEmployeeFromDate.BackColor = Color.White;
                //mskEmployeeToDate.BackColor = Color.White;
            }
            else if (Mode.ToUpper() == "EDIT MODE")
            {
                txtEmployeeName.BackColor = Color.Honeydew;
                txtEmployeePAN.BackColor = Color.Honeydew;
                txtEmpRefNo.BackColor = Color.Honeydew;
                cmbEmployeeCategory.BackColor = Color.Honeydew;
                //mskEmployeeFromDate.BackColor = Color.Honeydew;
                //mskEmployeeToDate.BackColor = Color.Honeydew;
            }
        }
        #endregion

        #region InsertCompanyBasicInfo
        private void InsertCompanyBasicInfo(long CompanyId, long BasicInfoId)
        {
            //-----------------------------------------------------------
            strSQL = "INSERT INTO TRN_COMPANY_INFO (" +
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
                     "            D_STATE_ID) " +
                     "     SELECT  COMPANY_ID," +
                     "             GROUP_ID," +
                     "             TAN_NO," +
                     "             PAN_NO," +
                     "             COMPANY_NAME," +
                     "             BRANCH_DIV," +
                     "             D_CATEGORY_ID," +
                     "             MINISTRY_ID," +
                     "             MINISTRY_OTHER," +
                     "             ADDRESS1," +
                     "             ADDRESS2," +
                     "             ADDRESS3," +
                     "             ADDRESS4," +
                     "             ADDRESS5," +
                     "             STATE_ID," +
                     "             PIN_CODE," +
                     "             STD," +
                     "             PHONE," +
                     "             EMAIL," +
                     "             PERSON_NAME," +
                     "             DESIGNATION," +
                     "             FATHER_NAME," +
                     "             P_ADDRESS1," +
                     "             P_ADDRESS2," +
                     "             P_ADDRESS3," +
                     "             P_ADDRESS4," +
                     "             P_ADDRESS5," +
                     "             P_STATE_ID," +
                     "             P_PIN_CODE," +
                     "             P_STD," +
                     "             P_PHONE," +
                     "             P_EMAIL," +
                     "             P_MOBILE," +
                     "             PAO_CODE," +
                     "             PAO_REG_NO," +
                     "             DDO_CODE," +
                     "             DDO_REG_NO," +
                     "             D_STATE_ID " +
                     "       FROM  MST_COMPANY " +
                     "       WHERE COMPANY_ID = " + CompanyId + "";                     
                     //" + BasicInfoId + "," +
                     //"             " + CompanyId + "," +
                     //"             " + TDSMAN.Classes.TDSMAN.T_pGroupId + "," +
                     //"            '" + cmnService.J_ReplaceQuote(txtTANNo.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtPANNo.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtDedEmpColName.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtBranch.Text.Trim()) + "'," +
                     //"             " + cmnService.J_ReturnInt32Value(txtDeductorTypeID.Text) + "," +
                     //"             " + cmnService.J_ReturnInt32Value(txtMinistryId.Text) + "," +
                     //"            '" + cmnService.J_ReplaceQuote(txtOtherMinistry.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtAddress4.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtAddress5.Text.Trim()) + "'," +
                     //"             " + Convert.ToInt32(Support.GetItemData(cmbState, cmbState.SelectedIndex)) + "," +
                     //"            '" + cmnService.J_ReplaceQuote(txtPIN.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtSTD.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtEmail.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPName.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPDesignation.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPFatherName.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPAddress1.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPAddress2.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPAddress3.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPAddress4.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPAddress5.Text.Trim()) + "'," +
                     //"             " + Convert.ToInt32(Support.GetItemData(cmbRPState, cmbRPState.SelectedIndex)) + "," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPPIN.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPSTD.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPPhone.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPEmail.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtRPMobileNo.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtPAOCode.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtPAORegNo.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtDDOCode.Text.Trim()) + "'," +
                     //"            '" + cmnService.J_ReplaceQuote(txtDDORegNo.Text.Trim()) + "'," +
                     //"             " + cmnService.J_ReturnInt32Value(txtDeductorTypeStateID.Text) + ")";
            //-----------------------------------------------------------
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                //txtDedEmpColName.Select();
            }
            strSQL = "UPDATE TRN_COMPANY_INFO SET BASIC_INFO_ID =" + lngBasicInfoID + " WHERE BASIC_INFO_ID = 0";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                //txtDedEmpColName.Select();
            }
        }
        #endregion

        //Added by Indrajit on 21-02-2013
        #region Check_Record
        private long Check_Record(long SrchId, string SrchTable, string SrchColumn, ref DGVControl.DGVControl DGControl)
        {
            if (dmlService.J_IsRecordExist(dmlService.J_pCommand, SrchTable, SrchColumn, SrchId) == true) return SrchId;

            cmnService.J_UserMessage("Record has been deleted.");

            SrchId = 0;
            //-------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
            //-------------------------------------------
            ClearControls();					//Clear all the Controls
            //-------------------------------------------
            strSQL = strQuery + "order by " + strOrderBy;
            //-------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref DGControl, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return 0;
            //-------------------------------------------
            BtnAdd.Select();
            //-------------------------------------------                            
            return 0;
        }
        #endregion       

        #region ShowHelpListUS10
        private void ShowHelpListUS10(string strTEXT,  int LocationX,int LocationY)
        {
//            IDataReader drdShowHelpListUS10 = null;
//            try
//            {
//                if (blnShowHelp == false)
//                    return;
//                //
//                if (strTEXT == "")
//                    return;
//                //--                    
//                strSQL = @"SELECT DISTINCT CVIA_SEC80C_ITEM_1_DESC AS CVIA_SEC80C_ITEM_DESC FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_1_DESC LIKE '" + strTEXT + @"%' AND CVIA_SEC80C_ITEM_1_DESC <> ''
//                            UNION
//                            SELECT DISTINCT CVIA_SEC80C_ITEM_2_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_2_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_2_DESC <> ''
//                            UNION
//                            SELECT DISTINCT CVIA_SEC80C_ITEM_3_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_3_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_3_DESC <> ''
//                            UNION
//                            SELECT DISTINCT CVIA_SEC80C_ITEM_4_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_4_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_4_DESC <> ''
//                            UNION
//                            SELECT DISTINCT CVIA_SEC80C_ITEM_5_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_5_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_5_DESC <> ''";
//                //--
//                drdShowHelpListUS10 = dmlService.J_ExecSqlReturnReader(strSQL);
//                //--
//                if (drdShowHelpListUS10 == null)
//                {
//                    lstHelp.Visible = false;
//                    return;
//                }
//                else
//                {
//                    lstHelp.Items.Clear();
//                    lstHelp.Height = 19;
//                    lstHelp.Visible = true;
//                    lstHelp.Location = new Point(LocationX, LocationY);
//                    while (drdShowHelpListUS10.Read())
//                    {
//                        lstHelp.Items.Add(new ListBoxItem(drdShowHelpListUS10["CVIA_SEC80C_ITEM_DESC"].ToString()));
//                        //--
//                        if (lstHelp.Height <= 420)
//                            lstHelp.Height = lstEmployeeHelp.Height + 19;
//                    }
//                    //--
//                    if (lstHelp.Items.Count <= 0)
//                        lstHelp.Visible = false;
//                    //--
//                    drdShowHelpListUS10.Close();
//                    drdShowHelpListUS10.Dispose();
//                    //
//                }
//            }
//            catch (Exception err)
//            {
//                drdShowHelpListUS10.Close();
//                drdShowHelpListUS10.Dispose();
//            }
        }
        #endregion

        #region ShowHelpListOtherIncome
        private void ShowHelpListOtherIncome(string strTEXT, int LocationX, int LocationY)
        {
//            IDataReader drdShowHelpListOtherIncome = null;
//            try
//            {
//                if (blnShowHelp == false)
//                    return;
//                //
//                if (strTEXT == "")
//                    return;
//                //--                    
//                strSQL = @"SELECT DISTINCT AIS_ITEM_1_DESC AS AIS_ITEM_DESC FROM TRN_SALARY_DETAILS_194P WHERE AIS_ITEM_1_DESC LIKE '" + strTEXT + @"%' AND AIS_ITEM_1_DESC <> ''
//                           UNION
//                           SELECT DISTINCT AIS_ITEM_2_DESC AS AIS_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE AIS_ITEM_2_DESC LIKE '" + strTEXT + @"%' AND  AIS_ITEM_2_DESC <> ''
//                           UNION
//                           SELECT DISTINCT AIS_ITEM_3_DESC AS AIS_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE AIS_ITEM_3_DESC LIKE '" + strTEXT + @"%' AND  AIS_ITEM_3_DESC <> ''
//                           UNION
//                           SELECT DISTINCT AIS_ITEM_4_DESC AS AIS_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE AIS_ITEM_4_DESC LIKE '" + strTEXT + @"%' AND  AIS_ITEM_4_DESC <> ''";
//                //--
//                drdShowHelpListOtherIncome = dmlService.J_ExecSqlReturnReader(strSQL);
//                //--
//                if (drdShowHelpListOtherIncome == null)
//                {
//                    lstHelp.Visible = false;
//                    return;
//                }
//                else
//                {
//                    lstHelp.Items.Clear();
//                    lstHelp.Height = 19;
//                    lstHelp.Visible = true;
//                    lstHelp.Location = new Point(LocationX, LocationY);
//                    while (drdShowHelpListOtherIncome.Read())
//                    {
//                        lstHelp.Items.Add(new ListBoxItem(drdShowHelpListOtherIncome["AIS_ITEM_DESC"].ToString()));
//                        //--
//                        if (lstHelp.Height <= 420)
//                            lstHelp.Height = lstEmployeeHelp.Height + 19;
//                    }
//                    //--
//                    if (lstHelp.Items.Count <= 0)
//                        lstHelp.Visible = false;
//                    //--
//                    drdShowHelpListOtherIncome.Close();
//                    drdShowHelpListOtherIncome.Dispose();
//                    //
//                }
//            }
//            catch (Exception err)
//            {
//                drdShowHelpListOtherIncome.Close();
//                drdShowHelpListOtherIncome.Dispose();
//            }
        }
        #endregion

        #region ShowHelpListSec80
        private void ShowHelpListSec80(string strTEXT, int LocationX, int LocationY)
        {
//            IDataReader drdShowHelpListSec80 = null;
//            try
//            {
//                if (blnShowHelp == false)
//                    return;
//                //
//                if (strTEXT == "")
//                    return;
//                //--                    
//                strSQL = @"SELECT DISTINCT CVIA_SEC80C_ITEM_1_DESC AS CVIA_SEC80C_ITEM_DESC FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_1_DESC LIKE '" + strTEXT + @"%' AND CVIA_SEC80C_ITEM_1_DESC <> ''
//                           UNION
//                           SELECT DISTINCT CVIA_SEC80C_ITEM_2_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_2_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_2_DESC <> ''
//                           UNION
//                           SELECT DISTINCT CVIA_SEC80C_ITEM_3_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_3_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_3_DESC <> ''
//                           UNION
//                           SELECT DISTINCT CVIA_SEC80C_ITEM_4_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_4_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_4_DESC <> ''
//                           UNION
//                           SELECT DISTINCT CVIA_SEC80C_ITEM_5_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_5_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_5_DESC <> ''
//                           UNION
//                           SELECT DISTINCT CVIA_SEC80C_ITEM_6_DESC AS CVIA_SEC80C_ITEM_DESC  FROM TRN_SALARY_DETAILS_194P WHERE CVIA_SEC80C_ITEM_6_DESC LIKE '" + strTEXT + @"%' AND  CVIA_SEC80C_ITEM_6_DESC <> ''";
//                //--
//                drdShowHelpListSec80 = dmlService.J_ExecSqlReturnReader(strSQL);
//                //--
//                if (drdShowHelpListSec80 == null)
//                {
//                    lstHelp.Visible = false;
//                    return;
//                }
//                else
//                {
//                    lstHelp.Items.Clear();
//                    lstHelp.Height = 19;
//                    lstHelp.Visible = true;
//                    lstHelp.Location = new Point(LocationX, LocationY);
//                    while (drdShowHelpListSec80.Read())
//                    {
//                        lstHelp.Items.Add(new ListBoxItem(drdShowHelpListSec80["CVIA_SEC80C_ITEM_DESC"].ToString()));
//                        //--
//                        if (lstHelp.Height <= 420)
//                            lstHelp.Height = lstEmployeeHelp.Height + 19;
//                    }
//                    //--
//                    if (lstHelp.Items.Count <= 0)
//                        lstHelp.Visible = false;
//                    //--
//                    drdShowHelpListSec80.Close();
//                    drdShowHelpListSec80.Dispose();
//                    //
//                }
//            }
//            catch (Exception err)
//            {
//                drdShowHelpListSec80.Close();
//                drdShowHelpListSec80.Dispose();
//            }
        }
        #endregion

        #region txtTextBox_KeyPress
        private void txtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                //if (lstHelp.Visible == true)
                //{
                //    lstHelp.Focus();
                //    lstHelp.SelectedIndex = 0;
                //}
                //else
                //    SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region txtTextBox_KeyDown
        private void txtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                //if (lstHelp.Visible == true)
                //{
                //    lstHelp.Focus();
                //    lstHelp.SelectedIndex = 0;
                //}
            }
        }
        #endregion

        

        #region CalcGrossTotalTaxSuperAnnuation
        //private void CalcGrossTotalTaxSuperAnnuation()
        //{
        //    txtTaxSuperannuationFund.Text = string.Format("{0:0.00}", (Math.Round(cmnService.J_ReturnDoubleValue(cmnService.J_ReturnDoubleValue(txtAmountSuperannuationFund.Text) * (cmnService.J_ReturnDoubleValue(txtRateSuperannuationFund.Text) / 100)), 0, MidpointRounding.AwayFromZero)));
        //    //
        //    txtTotalIncomeSuperannuationFund.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtAmountSuperannuationFund.Text)  + cmnService.J_ReturnDoubleValue(lblGrossTotalIncome.Text));                            
        //    //
        //    //lblTotalAmountOfTAX.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(txtTotalTDSDedcuted.Text) + cmnService.J_ReturnDoubleValue(txtTaxSuperannuationFund.Text));
        //}
        #endregion
        
        #region gTANNoPANNoValidation(TextBox TXTBOX, KeyPressEventArgs e, T_TANPAN TanPan)
        private bool gTANNoPANNoValidation(TextBox TXTBOX, KeyPressEventArgs e, T_TANPAN TanPan)
        {
            if (TanPan == T_TANPAN.TAN)
            {
                //---------------------------------------------------------
                if (TXTBOX.SelectionStart >= 0 && TXTBOX.SelectionStart <= 3)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) < 65 || Convert.ToInt64(e.KeyChar) > 122)
                        return false;
                }
                else if (TXTBOX.SelectionStart >= 4 && TXTBOX.SelectionStart <= 8)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) < 48 || Convert.ToInt64(e.KeyChar) > 57)
                        return false;
                }
                else if (TXTBOX.SelectionStart >= 9 && TXTBOX.SelectionStart <= 9)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) < 65 || Convert.ToInt64(e.KeyChar) > 122)
                        return false;
                }
                else
                    if (Convert.ToInt64(e.KeyChar) != 8)
                        return false;

                return true;
            }
            else if (TanPan == T_TANPAN.PAN)
            {
                //---------------------------------------------------------
                if (TXTBOX.SelectionStart >= 0 && TXTBOX.SelectionStart <= 4)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) < 65 || Convert.ToInt64(e.KeyChar) > 122)
                        return false;
                }
                else if (TXTBOX.SelectionStart >= 5 && TXTBOX.SelectionStart <= 8)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) >= 48 && Convert.ToInt64(e.KeyChar) <= 57)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) == 65 || // A
                             Convert.ToInt64(e.KeyChar) == 68 || // D
                             Convert.ToInt64(e.KeyChar) == 69 || // E
                             Convert.ToInt64(e.KeyChar) == 76 || // L
                             Convert.ToInt64(e.KeyChar) == 77 || // M
                             Convert.ToInt64(e.KeyChar) == 78 || // N
                             Convert.ToInt64(e.KeyChar) == 83 || // S
                             Convert.ToInt64(e.KeyChar) == 85 || // U
                             Convert.ToInt64(e.KeyChar) == 86 || // V
                             Convert.ToInt64(e.KeyChar) == 97 || // A
                             Convert.ToInt64(e.KeyChar) == 100 || // D
                             Convert.ToInt64(e.KeyChar) == 101 || // E
                             Convert.ToInt64(e.KeyChar) == 108 || // L
                             Convert.ToInt64(e.KeyChar) == 109 || // M
                             Convert.ToInt64(e.KeyChar) == 110 || // N
                             Convert.ToInt64(e.KeyChar) == 115 || // S
                             Convert.ToInt64(e.KeyChar) == 117 || // U
                             Convert.ToInt64(e.KeyChar) == 118 // V
                        )
                        return true;
                    else
                        return false;

                    //else if (Convert.ToInt64(e.KeyChar) < 48 || Convert.ToInt64(e.KeyChar) > 57)
                    //    return false;
                }
                else if (TXTBOX.SelectionStart >= 9 && TXTBOX.SelectionStart <= 9)
                {
                    if (Convert.ToInt64(e.KeyChar) == 8)
                        return true;
                    else if (Convert.ToInt64(e.KeyChar) < 65 || Convert.ToInt64(e.KeyChar) > 122)
                        return false;
                }
                else
                    if (Convert.ToInt64(e.KeyChar) != 8)
                        return false;

                return true;
            }
            return false;
        }
        #endregion
        
        #region FORM_DESIGN
        private void FORM_DESIGN(bool blnNewFormat)
        {
            //if (rbnMandatoryOptions.Checked == true)
            //{
            //    grpGrossSalary.Enabled = false;
            //    grpLessAllowanceUS10.Enabled = false;

            //    grpAddOtherIncomeDetails.Enabled = false;

            //    grpSection80CCE.Enabled = false;

            //    txtSec80CCFGrossAmount.Enabled = false;

            //    grpOtherSections.Enabled = false;
            //    //
            //    txtTotalSalaryBalance.TabStop = true;
            //    txtTotalSalaryBalance.ReadOnly = false;
            //    //
            //    txtTotalOtherIncome.TabStop = true;
            //    txtTotalOtherIncome.ReadOnly = true;
            //    //
            //    txtTotalDeductibleAmount80CCE.TabStop = true;
            //    txtTotalDeductibleAmount80CCE.ReadOnly = false;
            //    //
            //    txtTotalDeductibleAmountOS.TabStop = true;
            //    txtTotalDeductibleAmountOS.ReadOnly = false;
            //    //-- 2019/04/19
            //    grpLessAllowanceUS101819.Enabled = false;
            //    //
            //    //txtRebateus87A.Enabled = false;
            //    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID && blnNewFormat == true)
            //    {
            //        lnkCalcHRA.Visible = false;
            //        grpGrossSalary.Enabled = true;
            //        grpLessAllowanceUS101819.Enabled = true;
            //        grpLessAllowanceUS10.Enabled = false;
            //        txtTotalUs10.Visible = true;
            //        lblSec10OtherExemptionlCaption.Visible = true;
            //        txtSec10OtherExemption.Visible = true;
            //        //
            //        if (chkTaxation115BAC.Checked == true)
            //        {
            //            //grpLessAllowanceUS101819.Enabled = false;
            //            lblSec10TravelConcession.Enabled = false;
            //            txtSec10TravelConcession.Enabled = false;
            //            lblSec10HRA.Enabled = false;
            //            txtSec10HRA.Enabled = false;
            //        }
            //        else if (chkTaxation115BAC.Checked == false)
            //        {
            //            //grpLessAllowanceUS101819.Enabled = true;
            //            lblSec10TravelConcession.Enabled = true;
            //            txtSec10TravelConcession.Enabled = true;
            //            lblSec10HRA.Enabled = true;
            //            txtSec10HRA.Enabled = true;
            //        }
            //        //-- ENABLE TRUE //-- 2019/10/01
            //        //-- ENABLE FALSE 
            //        //txtSec80C1819GrossAmount.Enabled = false;
            //        //txtSec80CCC1819GrossAmount.Enabled = false;
            //        //txtSec80CCD11819GrossAmount.Enabled = false;
            //        //txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Enabled = false;
            //        //txtSec80CCD1B1819GrossAmount.Enabled = false;
            //        //txtSec80CCD21819GrossAmount.Enabled = false;
            //        //txtSec80D1819GrossAmount.Enabled = false;
            //        //txtSec80E1819GrossAmount.Enabled = false;
            //        //txtSec80G1819GrossAmount.Enabled = false;
            //        //txtSec80G1819QualifyingAmount.Enabled = false;
            //        //txtSec80TTA1819GrossAmount.Enabled = false;
            //        //txtSec80TTA1819QualifyingAmount.Enabled = false;
            //        //txtSecChVIATotalOthersGrossAmt.Enabled = false;
            //        //txtSecChVIATotalOthersQualifyingAmt.Enabled = false;
            //        grpSecChVIAOthers.Enabled = false;
            //        //txtRebateus87A.Enabled = true;
            //    }
            //}
            //else if (rbnDetailOptions.Checked == true)
            //{
            //    if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId < T_FinancialYearID.F2018_19ID)
            //    {
            //        lnkCalcHRA.Visible = true;
            //        grpLessAllowanceUS10.Text = "2. Less : Allowance under section 10";
            //        lblTotalCaptionUs10.Visible = true;
            //        lblAllowanceSecAmtTotal.Visible = true;
            //        ////txtTotalUs10.Visible = false;
            //        //txtRebateus87A.Enabled = false;
            //    }
            //    else if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID && blnNewFormat == true)
            //    {
            //        lnkCalcHRA.Visible = false;
            //        grpLessAllowanceUS10.Enabled = true;
            //        grpLessAllowanceUS10.Text = "f) Amount of any other exemption under Section 10";
            //        //
            //        lblTotalCaptionUs10.Visible = false;
            //        lblAllowanceSecAmtTotal.Visible = false;
            //        lblSec10OtherExemptionlCaption.Visible = true;
            //        txtSec10OtherExemption.Visible = true;
            //        txtTotalUs10.Visible = true;
            //        //
            //        txtSec80C1819GrossAmount.Enabled = true;
            //        txtSec80CCC1819GrossAmount.Enabled = true;
            //        txtSec80CCD11819GrossAmount.Enabled = true;
            //        txtTotalDeductionSec80C80CCC80CCD11819GrossAmount.Enabled = true;
            //        txtSec80CCD1B1819GrossAmount.Enabled = true;
            //        txtSec80CCD21819GrossAmount.Enabled = true;
            //        txtSec80D1819GrossAmount.Enabled = true;
            //        txtSec80E1819GrossAmount.Enabled = true;
            //        txtSec80G1819GrossAmount.Enabled = true;
            //        txtSec80G1819QualifyingAmount.Enabled = true;
            //        txtSec80TTA1819GrossAmount.Enabled = true;
            //        txtSec80TTA1819QualifyingAmount.Enabled = true;
            //        txtSecChVIATotalOthersGrossAmt.Enabled = true;
            //        txtSecChVIATotalOthersGrossAmt.ReadOnly = true;
            //        txtSecChVIATotalOthersQualifyingAmt.Enabled = true;
            //        txtSecChVIATotalOthersQualifyingAmt.ReadOnly = true;
            //        //txtSecChVIATotalOthersDeductibleAmt.ReadOnly = true;
            //        grpSecChVIAOthers.Enabled = false;
            //        //txtRebateus87A.Enabled = true;
            //    }
            //    //--
            //    grpGrossSalary.Enabled = true;
            //    grpLessAllowanceUS10.Enabled = true;

            //    grpAddOtherIncomeDetails.Enabled = true;

            //    grpSection80CCE.Enabled = true;

            //    txtSec80CCFGrossAmount.Enabled = true;

            //    grpOtherSections.Enabled = true;
            //    //
            //    txtTotalSalaryBalance.TabStop = false;
            //    txtTotalSalaryBalance.ReadOnly = true;
            //    //
            //    txtTotalOtherIncome.TabStop = false;
            //    txtTotalOtherIncome.ReadOnly = true;
            //    //
            //    txtTotalDeductibleAmount80CCE.TabStop = false;
            //    txtTotalDeductibleAmount80CCE.ReadOnly = true;
            //    //
            //    txtTotalDeductibleAmountOS.TabStop = false;
            //    txtTotalDeductibleAmountOS.ReadOnly = true;
            //}
            ////--
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID && blnNewFormat == true)
            //{
            //    grpAddOtherIncomeDetails.Enabled = true;
            //    //txtOtherIncomeDesc1.Text = "INCOME (OR ADMISSIBLE LOSS) FROM HOUSE PROPERTY REPORTED BY EMPLOYEE OFFERED FOR TDS";
            //    txtOtherIncomeDesc1.Text = "INCOME / LOSS - HOUSE PROPERTY OFFERED FOR TDS";
            //    txtOtherIncomeDesc1.ReadOnly = true;
            //    txtOtherIncomeDesc1.TabStop = false;
            //    //txtOtherIncomeAmt1.Text = "0.00";
            //    //txtOtherIncomeDesc2.Text = "INCOME UNDER THE HEAD OTHER SOURCES OFFERED FOR TDS";
            //    txtOtherIncomeDesc2.Text = "INCOME-OTHER SOURCES OFFERED FOR TDS";
            //    txtOtherIncomeDesc2.ReadOnly = true;
            //    txtOtherIncomeDesc2.TabStop = false;
            //    //txtOtherIncomeAmt2.Text = "0.00";
            //    txtOtherIncomeDesc3.Text = "";
            //    txtOtherIncomeAmt3.Text = "0.00";
            //    txtOtherIncomeDesc4.Text = "";
            //    txtOtherIncomeAmt4.Text = "0.00";
            //    //
            //    txtOtherIncomeDesc3.Enabled = false;
            //    txtOtherIncomeAmt3.Enabled = false;
            //    txtOtherIncomeDesc4.Enabled = false;
            //    txtOtherIncomeAmt4.Enabled = false;
            //    //
            //    grpDedcutionsUCVIA.Visible = false;
            //    grpDedcutionsUCVIA1819.Visible = true;
            //    //grpDedcutionsUCVIA1819.Location = new Point(14, 945);
            //    //
            //    lblBSectionAlert.Visible = false;
            //    //-- 2019/07/23
            //    lblRebateCaption.Visible = true;
            //    lblRebateCalculated.Visible = true;
            //    txtRebate.Visible = true;
            //}
            //else
            //{
            //    txtOtherIncomeDesc1.Text = "";
            //    txtOtherIncomeDesc1.ReadOnly = false;
            //    txtOtherIncomeDesc1.TabStop = true;
            //    txtOtherIncomeAmt1.Text = "0.00";
            //    txtOtherIncomeDesc2.Text = "";
            //    txtOtherIncomeDesc2.ReadOnly = false;
            //    txtOtherIncomeDesc2.TabStop = true;
            //    txtOtherIncomeAmt2.Text = "0.00";
            //    txtOtherIncomeDesc3.Text = "";
            //    txtOtherIncomeAmt3.Text = "0.00";
            //    txtOtherIncomeDesc4.Text = "";
            //    txtOtherIncomeAmt4.Text = "0.00";
            //    //
            //    txtOtherIncomeDesc3.Enabled = true;
            //    txtOtherIncomeAmt3.Enabled = true;
            //    txtOtherIncomeDesc4.Enabled = true;
            //    txtOtherIncomeAmt4.Enabled = true;
            //    //
            //    grpDedcutionsUCVIA.Visible = true;
            //    grpDedcutionsUCVIA1819.Visible = false;
            //    //
            //    lblBSectionAlert.Visible = true;
            //    //-- 2019/07/23
            //    lblRebateCaption.Visible = false;
            //    lblRebateCalculated.Visible = false;
            //    txtRebate.Visible = false;
            //}
            ////--
            //if (TDSMAN.Classes.TDSMAN.T_pFinancialYearId >= T_FinancialYearID.F2018_19ID && blnNewFormat == false)
            //{
            //    BtnSave.Visible = false;
            //}
            //else
            //    BtnSave.Visible = true;
            ////
            //DISABLE_DESIGN_ANNEXTURE_18_19();
        }
        #endregion
        
        

        //#endregion

        private void txtSecOthersDeductibleAmount_TextChanged(object sender, EventArgs e)
        {

        }

        #region txtSec80DDGrossAmount_TextChanged
        private void txtSec80DDGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80DDQualifyingAmount.Text = txtSec80DDGrossAmount.Text;
            txtSec80DDDeductibleAmount.Text = txtSec80DDGrossAmount.Text;
        }
        #endregion

        #region txtSec80EEGrossAmount_TextChanged
        private void txtSec80EEGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80EEQualifyingAmount.Text = txtSec80EEGrossAmount.Text;
            txtSec80EEDeductibleAmount.Text = txtSec80EEGrossAmount.Text;
        }
        #endregion

        #region txtSec80EEAGrossAmount_TextChanged
        private void txtSec80EEAGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80EEAQualifyingAmount.Text = txtSec80EEAGrossAmount.Text;
            txtSec80EEADeductibleAmount.Text = txtSec80EEAGrossAmount.Text;
        }
        #endregion

        #region txtSec80GGGrossAmount_TextChanged
        private void txtSec80GGGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80GGQualifyingAmount.Text = txtSec80GGGrossAmount.Text;
            txtSec80GGDeductibleAmount.Text = txtSec80GGGrossAmount.Text;
        }
        #endregion

        #region txtSec80GGCGrossAmount_TextChanged
        private void txtSec80GGCGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80GGCQualifyingAmount.Text = txtSec80GGCGrossAmount.Text;
            txtSec80GGCDeductibleAmount.Text = txtSec80GGCGrossAmount.Text;

        }
        #endregion

        #region txtSecOthersGrossAmount_TextChanged
        private void txtSecOthersGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSecOthersQualifyingAmount.Text = txtSecOthersGrossAmount.Text;
            txtSecOthersDeductibleAmount.Text = txtSecOthersGrossAmount.Text;
        }
        #endregion

        #region txtSec80UGrossAmount_TextChanged
        private void txtSec80UGrossAmount_TextChanged(object sender, EventArgs e)
        {
            txtSec80UQualifyingAmount.Text = txtSec80UGrossAmount.Text;
            txtSec80UDeductibleAmount.Text = txtSec80UGrossAmount.Text;
        }
        #endregion


        #region txtSec80DDDeductibleAmount_KeyPress
        private void txtSec80DDDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80DDDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80DDQualifyingAmount_KeyPress
        private void txtSec80DDQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80DDQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80DDGrossAmount_KeyPress
        private void txtSec80DDGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80DDGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEDeductibleAmount_KeyPress
        private void txtSec80EEDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEQualifyingAmount_KeyPress
        private void txtSec80EEQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEGrossAmount_KeyPress
        private void txtSec80EEGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEADeductibleAmount_KeyPress
        private void txtSec80EEADeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEADeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEAQualifyingAmount_KeyPress
        private void txtSec80EEAQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEAQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80EEAGrossAmount_KeyPress
        private void txtSec80EEAGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80EEAGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGDeductibleAmount_KeyPress
        private void txtSec80GGDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGQualifyingAmount_KeyPress
        private void txtSec80GGQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGGrossAmount_KeyPress
        private void txtSec80GGGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGCDeductibleAmount_KeyPress
        private void txtSec80GGCDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGCDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGCQualifyingAmount_KeyPress
        private void txtSec80GGCQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGCQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80GGCGrossAmount_KeyPress
        private void txtSec80GGCGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80GGCGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80UDeductibleAmount_KeyPress
        private void txtSec80UDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80UDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80UQualifyingAmount_KeyPress
        private void txtSec80UQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80UQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSec80UGrossAmount_KeyPress
        private void txtSec80UGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSec80UGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSecOthersDeductibleAmount_KeyPress
        private void txtSecOthersDeductibleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecOthersDeductibleAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSecOthersQualifyingAmount_KeyPress
        private void txtSecOthersQualifyingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecOthersQualifyingAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSecOthersGrossAmount_KeyPress
        private void txtSecOthersGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtSecOthersGrossAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtInterestIncome_KeyPress
        private void txtInterestIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,12,2", txtInterestIncome, "") == false)
                e.Handled = true;
        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0080", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #endregion
        //private void txtSecOthersGrossAmount_TextChanged(object sender, EventArgs e)
        //{
        //    txtSecOthersQualifyingAmount.Text = txtSecOthersGrossAmount.Text;
        //    txtSecOthersDeductibleAmount.Text = txtSecOthersGrossAmount.Text;
        //}
    }
}

