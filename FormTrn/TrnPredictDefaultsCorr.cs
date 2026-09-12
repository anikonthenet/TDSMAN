#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnForm24Q
Version			: 1.0
Start Date		: 30-01-2014
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
    public partial class TrnPredictDefaultsCorr : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnPredictDefaultsCorr()
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
        TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
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
        long lngBasicInfoID=0;
        long lngChallanID;
        //long lngChallanDetailID;
        long lngDeducteeDetailID;
        long lngDeducteeID;
        string strSQLShowHelpDeductee;
        string strSQLShowHelpPAN;

        string strStateCode;
        string strRPStateCode;
        string strDStateCode;
        string strMinistryCode;

        string strBatFile = "";
        string strFVUFile = "";
        string strCSIDownloadFilePath = "";
        //Added by Dhrub Mukherjee On 08/11/2013
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string     strFAYear= "";
        string strEmpDed = "";

        string strFromModule = "";

        //Added by Shrey Kejriwal on 19/01/2011
        string strConsolidatedStatementPath = "";

        string newOutputFileName = "";

        string[,] strArray;

        bool blnShowHelp = true;
        bool blnShowPANHelp = true;
        bool blnChkChanged = true;
        bool blnSectionDisplay = true;
        bool blnSectionDDDisplay = true;
        bool blnRegularStatemnt = true;
        bool blnExit = false;
        bool blnSelectComboExit = false;
        bool blnRadioExit = false;
        bool blnGridExit = false;
        bool blActivate = true;
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--            
        long lngPrevBasicInfoID = 0;
        //----
        #region Tabbed_Mode
        private struct Tabbed_Mode
        {
            public const string Basic = "Basic Information";
            public const string Challan = "Challan Details";
            public const string Deductee = "Deductee Details";
            public const string GenerateTDS = "Generate TDS Return";
        }
        #endregion
        //----
        string strFormNo = "";//T_FormNo.F26Q;

        int intBookmark = 0;
        //--
        string strLastDateCreation = "", strActualAmtCreation = "", strTableName = "", strFieldPAN = "", strFieldName = "", strFieldId = "";
        string strSearchConditions = "";
        string strDeducteeDetailsTable = ""; string strChallanDetailsTable = ""; string strChallanID = ""; string strBasicInfoID = "";
        //--
        //string strFormNo = ""; string strQuarter = ""; string strCompanyName = "";
        double TotalInterestLP = 0; double TotalInterestLD = 0; 
        //-- Added By Abhishek Dey On 15/05/2018 --
        string strPayableFee = string.Empty;
        string strDateofFiling = string.Empty;
        //--
        string strCompanyDeductorType = "";
        int intCOMPLIANCE_HIGHER_RATE_FLAG = 0;
        //
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        //-----------------------------------------
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

        #region TrnHealthCheckup_Load
        private void TrnHealthCheckup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--
            lblDisclaimer.Text = "Please note: The defaults has been calculated as per the due dates and tax rate " +
                                 "published in Income Tax India website. For further clarification " +
                                 "visit Income Tax India website.";
            //--
            blnRadioExit = true;
            strFromModule = TDSMAN.Classes.TDSMAN.T_FromModule;
            TDSMAN.Classes.TDSMAN.T_FromModule = "";
            if (strFromModule == T_OTHERMODULENAME.REGULAR_FORM)
                rbnRegularReturn.Checked = true;
            else if (strFromModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                rbnCorrectionReturn.Checked = true;
            blnRadioExit = false;
            //--
            //btnPrintGrid.Enabled = false;
            //btnPrintGrid.BackColor = Color.LightGray;
            //rbnRegularCorrectionReturn_CheckedChanged(sender, e);
        }
        #endregion

        #region TrnHealthCheckupRegCorr_Activated
        private void TrnHealthCheckupRegCorr_Activated(object sender, EventArgs e)
        {
            if (blActivate == false)
            {
                blActivate = true;
                return;
            }
            //--
            rbnRegularCorrectionReturn_CheckedChanged(sender, e);
        }
        #endregion

        #region rbnRegularCorrectionReturn_CheckedChanged
        private void rbnRegularCorrectionReturn_CheckedChanged(object sender, EventArgs e)
        {
            //ClearFields();
            if (blnRadioExit == true)
                return;
            //--
            lblFilingDateLabel.Visible = false;
            mskFilingDate.Visible = false;
            btnViewLateFine.Visible = false;
            //--
            if (rbnRegularReturn.Checked == true)
            {
                //--
                strDeducteeDetailsTable = "TRN_DEDUCTEE_DETAILS";
                strBasicInfoID = "BASIC_INFO_ID";
                //--
                #region CLEAR CONTROLS
                //-----------
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
                string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                //-----------
                //-- FORM NO
                //-----------
                //string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                //dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo, 1);
                strSQL = "SELECT DISTINCT FORM_NO AS FORM_ID, " +
                        "IIF(FORM_NO = '24Q', '138 (24Q)', " +
                        "IIF(FORM_NO = '26Q', '140 (26Q)', " +
                        "IIF(FORM_NO = '27Q', '144 (27Q)', " +
                        "IIF(FORM_NO = '27EQ', '143 (27EQ)', '')))) AS FORM_DISPLAY " +
                        "FROM TRN_BASIC_INFO " +
                        "WHERE FORM_NO IN ('24Q', '26Q', '27Q', '27EQ')";
                //dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3, 1);
                if (J_PopulateComboBox(strSQL, ref cmbFormNo) == false) return;
                if (cmbFormNo.Items.Count > 0)
                    cmbFormNo.SelectedIndex = 1;
                //-----------
                //-- COMPANY
                //-----------
                strSQL = " SELECT COMPANY_ID," +
                    "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                    "      FROM   MST_COMPANY " +
                    "      ORDER BY COMPANY_NAME";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                //-----------
                #endregion
                //--
                #region BOOKMARK
                if (TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId > 0)
                {
                    IDataReader drdGetDetails = null;
                    strSQL = @"SELECT MST_COMPANY.COMPANY_NAME, 
                                      MST_COMPANY.TAN_NO, 
                                      MST_ASSESSMENT.FA_YEAR, 
                                      TRN_BASIC_INFO.QTR, 
                                      TRN_BASIC_INFO.FORM_NO 
                               FROM   TRN_BASIC_INFO, 
                                      MST_COMPANY, 
                                      MST_ASSESSMENT 
                               WHERE  TRN_BASIC_INFO.COMPANY_ID = MST_COMPANY.COMPANY_ID 
                               AND    TRN_BASIC_INFO.ASST_ID = MST_ASSESSMENT.ASST_ID 
                               AND    TRN_BASIC_INFO.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId;
                    drdGetDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (drdGetDetails == null)
                        return;
                    //
                    string FAYear = "";
                    while (drdGetDetails.Read())
                    {
                        blnSelectComboExit = true;
                        cmbQuarter.Text = Convert.ToString(drdGetDetails["QTR"]);
                        cmbCompany.Text = Convert.ToString(drdGetDetails["COMPANY_NAME"]) + " [" + Convert.ToString(drdGetDetails["TAN_NO"]) + "]";
                        FAYear = Convert.ToString(drdGetDetails["FA_YEAR"]);
                        cmbFormNo.Text = Convert.ToString(drdGetDetails["FORM_NO"]);
                        blnSelectComboExit = false;                        
                    }
                    drdGetDetails.Close();
                    drdGetDetails.Dispose();
                    //
                    cmbFinancialYear.Text = FAYear;
                    cmbMain_SelectedIndexChanged(sender, e);
                    //grpReturnSelection.Enabled = false;
                    //grpRegularReturn.Enabled = false;
                }
                else
                {
                    if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                    {
                        if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL") == true)
                        {
                            intAsstId = TdsMan.T_GetBookMarkDetail(dmlService.J_pCommand, T_TransactionMode.SHOW.ToString(), 0, "", "", 0, TDSMAN.Classes.TDSMAN.T_MACHINE_ID, out strQuarter, out strCompanyName, out strTAN, out strFormNoBkmark, out strFAYear);
                            //
                            //                    strSQL = @"SELECT FA_YEAR
                            //                              FROM MST_ASSESSMENT
                            //                              WHERE ASST_ID=" + intAsstId + "";
                            //                    cmbFinancialYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));//Convert.ToInt32(Support.SetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex, intAsstId));
                            blnSelectComboExit = true;
                            cmbQuarter.Text = strQuarter;
                            cmbCompany.Text = strCompanyName + " [" + strTAN + "]";
                            cmbFinancialYear.Text = strFAYear;
                            blnSelectComboExit = false;
                            cmbFormNo.Text = strFormNoBkmark;
                        }
                    }
                }
                #endregion
                //
                grpRegularReturn.Visible = true;
                grpCorrectionReturn.Visible = false;
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                grpRegularReturn.Visible = false;
                grpCorrectionReturn.Visible = true;
                //
                strDeducteeDetailsTable = "COR_TRN_DEDUCTEE_DETAILS";
                strBasicInfoID = "BATCH_HEADER_ID"; 
                //
                this.Cursor = Cursors.WaitCursor;
                //
                //--
                LoadBatch();
                //            
                #region COMMENT
                //if (TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId > 0)
                //{
                //    //grpCorrectionReturn.Enabled = false;
                //    //grpReturnSelection.Enabled = false; 
                //    //
                //    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId;
                //    //-- SELECT TO THE DESIRED ROW
                //    //cmnService.J_UserMessage(dgcViewBatch.CurrentRow.Index.ToString());
                //    for (int i = 0; i <= dgcViewBatch.RowCount - 1; i++)
                //    {
                //        if(i%3==0 && i>0)
                //            dgcViewBatch.FirstDisplayedScrollingRowIndex = dgcViewBatch.FirstDisplayedScrollingRowIndex + 3;
                //        //
                //        if (dgcViewBatch.Rows[i].Cells[0].Value.ToString() == lngBasicInfoID.ToString())
                //        {
                //            dgcViewBatch.Rows[i].Selected = true;
                //            break;
                //        }
                //    }
                //    //
                //    LoadDetailsCorr(lngBasicInfoID);
                //    ControlSummaryBasicInfoCorr(lngBasicInfoID);
                //    //
                //    //grpCorrectionReturn.Enabled = true;
                //}
                //else
                #endregion
                //
                dgcViewBatch_Click(sender, e);
                //--
                if (TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId > 0 && strFromModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                {
                    for (int i = 0; i <= dgcViewBatch.RowCount - 1; i++)
                    {
                        if (i % 3 == 0 && i > 0)
                            dgcViewBatch.FirstDisplayedScrollingRowIndex = dgcViewBatch.FirstDisplayedScrollingRowIndex + 3;
                        //
                        if (dgcViewBatch.Rows[i].Cells[0].Value.ToString() == TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId.ToString())
                        {
                            dgcViewBatch.Rows[i].Selected = true;
                            dgcViewBatch.CurrentCell = dgcViewBatch[1, i];
                            break;
                        }
                    }
                }
                //--
                this.Cursor = Cursors.Default;
            }
            //
            //LoadDeducteeGrid();
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //            
            if (lngPrevBasicInfoID > 0 && lngPrevBasicInfoID == lngBasicInfoID)
                return;
            //
            pnlReturnSummary.Visible = false;
            ClearControls();
            //
            if (blnSelectComboExit == true)
                return;
            //
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                return;
            }
            if (cmbFormNo.Text == "")
            {
                return;
            }
            if (cmbQuarter.Text == "")
            {
                return;
            }
            //
            ClearControls();
            //--
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        cmbFormNo.Text);

            if (lngBasicInfoID == 0)
            {
                lblHideTabs.Text = "No records found for the selected return";
                return;
            }
            //-- 2019/03/14
            strCompanyDeductorType = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_CATEGORY.CATEGORY_CODE FROM MST_COMPANY, MST_CATEGORY WHERE MST_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID AND MST_COMPANY.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))));
            //
            lblHideTabs.Text = "";
            pnlReturnSummary.Visible = true;
            ControlSummaryBasicInfo(lngBasicInfoID);
            LoadDetailsRegular(lngBasicInfoID);
            //
            lblTotalLabelLatePayments.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP, 2)) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD, 2)) + "]";
            ////lblTotalLabelLatePayments.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP/100, 0)*100) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD/100, 0)*100) + "]";
            //lblTotalInterest.Text = Convert.ToString(Math.Round(TotalInterestLP + TotalInterestLD, 2));
            lblTotalInterest.Text = string.Format("{0:0.00}", Math.Round(TotalInterestLP + TotalInterestLD, 2));
            //lblTotalInterest.Text = string.Format("{0:0.00}", Math.Round((TotalInterestLP + TotalInterestLD) / 100, 0) * 100);
            //
            lblTotalLabelLateDeductions.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP , 2)) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD , 2)) + "]";
            lblTotalInterestLD.Text = Convert.ToString(Math.Round(TotalInterestLP + TotalInterestLD, 2));
            //lblTotalLabelLateDeductions.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP / 100, 0) * 100) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD / 100, 0) * 100) + "]";
            //lblTotalInterestLD.Text = Convert.ToString(Math.Round((TotalInterestLP + TotalInterestLD) / 100, 0) * 100);
            lblTotalInterestLD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(lblTotalInterestLD.Text) == "" ? "0" : Convert.ToString(lblTotalInterestLD.Text)));
            //
            if (cmnService.J_ReturnDoubleValue(lblInterestAmountReportedLD.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text))
                lblInterestPayableLD.Text = "0.00";
            else
                lblInterestPayableLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
            //
            if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                lblInterestPayable.Text = "0.00";
            else
                lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));                
            //--
            lngPrevBasicInfoID = lngBasicInfoID;
        }
        #endregion

        #region btnViewShortDeductions_Click
        private void btnViewShortDeductions_Click(object sender, EventArgs e)
        {
            string strFormNo = "";
            string strQtr = "";
            long lngFAYearId = 0;
            if (lngBasicInfoID == 0) return;
            //
            lblHideTabs.Text = "Short Deductions";
            //
            tbcView.Visible = true;
            //
            if (rbnCorrectionReturn.Checked == true)
            {
                strFormNo =Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                strQtr = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT QTR     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                lngFAYearId = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID)));
                //--
                //if(lngFAYearId >= T_FinancialYearID.F2014_15ID && strFormNo == T_FormNo.F24Q && strQtr == T_Qtr.Q4)
                //{
                //    tbcView.SelectTab(tbpShortDeductionsSD);
                //    lblSection.Visible = false;
                //    cmbSection.Visible = false;
                //}
            }
            //
            if (rbnRegularReturn.Checked == true && 
                Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2014_15ID && 
                cmbFormNo.Text == T_FormNo.F24Q && 
                cmbQuarter.Text == T_Qtr.Q4)
            {
                tbcView.SelectTab(tbpShortDeductionsSD);
                lblSection.Visible = false;
                cmbSection.Visible = false;
            }
            else if (rbnCorrectionReturn.Checked == true && lngFAYearId >= T_FinancialYearID.F2014_15ID && strFormNo == T_FormNo.F24Q && strQtr == T_Qtr.Q4)
            {
                tbcView.SelectTab(tbpShortDeductionsSD);
                lblSection.Visible = false;
                cmbSection.Visible = false;
            }
            else
            {
                lblSection.Visible = true;
                cmbSection.Visible = true;
                cmbSection.SelectedIndex = 0;
                ClearSearchControls();
                //
                tbcView.SelectTab(tbpShortDeductions);
            }
            //if (lngBasicInfoID == 0) return;
            ////
            //lblSection.Visible = true;
            //cmbSection.Visible = true;
            //cmbSection.SelectedIndex = 0;
            //ClearSearchControls();
            ////
            //lblHideTabs.Text = "Short Deductions";
            ////
            //tbcView.Visible = true;
            //tbcView.SelectTab(tbpShortDeductions);
            ////
            btnViewShortDeductions.ForeColor = Color.Blue;
            lblShortDeductionsNos.ForeColor = Color.Blue;
            lblShortDeductionsLabel.ForeColor = Color.Blue;
            btnViewLatePayments.ForeColor = Color.Black;
            lblLatePaymentsNos.ForeColor = Color.Black;
            lblLatePaymentsLabel.ForeColor = Color.Black;
            btnViewLateDeductions.ForeColor = Color.Black;
            lblLateDeductionsNos.ForeColor = Color.Black;
            lblLateDeductionsLabel.ForeColor = Color.Black;
            lblFilingDateLabel.ForeColor = Color.Black;
            mskFilingDate.ForeColor = Color.Black;
            btnViewLateFine.ForeColor = Color.Black;             
        }
        #endregion

        #region btnViewLatePayments_Click
        private void btnViewLatePayments_Click(object sender, EventArgs e)
        {
            if (lngBasicInfoID == 0) return;
            //
            lblSection.Visible = false;
            cmbSection.Visible = false;
            ClearSearchControls();
            //
            lblHideTabs.Text = "Late Payments";
            //
            tbcView.Visible = true;
            tbcView.SelectTab(tbpLatePayments);
            //
            btnViewShortDeductions.ForeColor = Color.Black;
            lblShortDeductionsNos.ForeColor = Color.Black;
            lblShortDeductionsLabel.ForeColor = Color.Black;
            btnViewLatePayments.ForeColor = Color.Blue;
            lblLatePaymentsNos.ForeColor = Color.Blue;
            lblLatePaymentsLabel.ForeColor = Color.Blue;
            btnViewLateDeductions.ForeColor = Color.Black;
            lblLateDeductionsNos.ForeColor = Color.Black;
            lblLateDeductionsLabel.ForeColor = Color.Black;
            lblFilingDateLabel.ForeColor = Color.Black;
            mskFilingDate.ForeColor = Color.Black;
            btnViewLateFine.ForeColor = Color.Black; 
        }
        #endregion

        #region btnViewInvalidPAN_Click
        private void btnViewInvalidPAN_Click(object sender, EventArgs e)
        {
            if (lngBasicInfoID == 0) return;
            //
            lblSection.Visible = false;
            cmbSection.Visible = false;
            ClearSearchControls();
            //
            lblHideTabs.Text = "Invalid PANs";
            //            
            tbcView.Visible = true;
            tbcView.SelectTab(tbpInvalidPAN);

        }
        #endregion

        #region btnViewLateDeductions_Click
        private void btnViewLateDeductions_Click(object sender, EventArgs e)
        {
            if (lngBasicInfoID == 0) return;
            //
            lblSection.Visible = false;
            cmbSection.Visible = false;
            ClearSearchControls();
            //
            lblHideTabs.Text = "Late Deductions";
            //
            tbcView.Visible = true;
            tbcView.SelectTab(tbpLateDeductions);
            //
            btnViewShortDeductions.ForeColor = Color.Black;
            lblShortDeductionsNos.ForeColor = Color.Black;
            lblShortDeductionsLabel.ForeColor = Color.Black;
            btnViewLatePayments.ForeColor = Color.Black;
            lblLatePaymentsNos.ForeColor = Color.Black;
            lblLatePaymentsLabel.ForeColor = Color.Black;
            btnViewLateDeductions.ForeColor = Color.Blue;
            lblLateDeductionsNos.ForeColor = Color.Blue;
            lblLateDeductionsLabel.ForeColor = Color.Blue;
            lblFilingDateLabel.ForeColor = Color.Black;
            mskFilingDate.ForeColor = Color.Black;
            btnViewLateFine.ForeColor = Color.Black; 
        }
        #endregion

        #region btnViewLateDeductions_Click
        private void btnViewLateFine_Click(object sender, EventArgs e)
        {
            if (lngBasicInfoID == 0) return;
            //--
            if (dtService.J_IsBlankDateCheck(ref mskFilingDate, "Filing Date - Cannot be Blank") == true) //-- 2017/12/20
                return;
            //--
            if (dtService.J_IsDateValid(mskFilingDate) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the Date");
                mskFilingDate.Select();
                return;
            }
            //--
            if (dtService.J_ConvertToIntYYYYMMDD(mskFilingDate.Text) < dtService.J_ConvertToIntYYYYMMDD(J_ReturnServerDate()) && mskFilingDate.ReadOnly==false)
            {
                cmnService.J_UserMessage("Only current or future date is allowed !!");
                mskFilingDate.Select();
                return;
            }
            //--
            lblSection.Visible = false;
            cmbSection.Visible = false;
            lblDeducteeNameCaption.Visible = false;
            txtDeducteeName.Visible = false;
            lblDeducteePANCaption.Visible = false;
            txtDeducteePAN.Visible = false;
            //
            ClearSearchControls();
            //
            lblScheduleLastDate.Text = "";
            lblLateFee.Text = "";
            lblFeeAlreadyPaid.Text = "";
            ////-- Added By Abhishek Dey On 10/05/2018 --
            strPayableFee = string.Empty;
            strDateofFiling = string.Empty;
            //-----------------------------------------
            lblHideTabs.Text = "Late Fine";
            //
            //tbcView.Visible = true;
            //tbcView.SelectTab(tbpLateFine);
            //-- CALCULATE LATE FINE ---------------------
            if (rbnRegularReturn.Checked == true)
            {
                //-- CALCULATED @  200/- DAY OF DELAY OR 100% VALUE OF TDS, WHICHEVER IS LOWER
                double dblFinePerDay = 200;
                //--
                //strSQL = "SELECT " + cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                //string strReturnLastDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //if (strReturnLastDate == "")
                //{
                    strSQL = "UPDATE TRN_BASIC_INFO SET RETURN_LAST_DATE ='" + TdsMan.T_ReturnFilingLastDate(cmbQuarter.Text, TdsMan.ReturnFinancialYear(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))), TdsMan.GetDeductorType(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))), Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbFormNo.Text) + "' WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "SELECT " + cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    string strReturnLastDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //}
                //--
                //Convert.ToString(date.Substring(0, 2) + date.Substring(3, 2) + date.Substring(6, 4))
                //string strFilingDate = mskFilingDate.Text.Replace("/","");
                //strFilingDate = cmnService.J_Right(strFilingDate, 4) + cmnService.J_Mid(strFilingDate, 2, 2) + cmnService.J_Left(strFilingDate, 2);
                //
                if (dtService.J_ConvertToIntYYYYMMDD(mskFilingDate.Text) > dtService.J_ConvertToIntYYYYMMDD(strReturnLastDate))
                {
                    DateTime dtpFilingDate = DateTime.Parse(mskFilingDate.Text);
                    DateTime dtpReturnLastDate = DateTime.Parse(strReturnLastDate);
                    TimeSpan intNoOfDays = dtpFilingDate - dtpReturnLastDate;
                    //int intNoOfDays = Convert.ToInt32(strFilingDate) - Convert.ToInt32(strReturnLastDate);
                    string strActualFine = "";
                    double dblActualFine = 0;
                    //
                    double dblTotalFine = Convert.ToInt64(intNoOfDays.Days) * dblFinePerDay;
                    //
                    double dblAmountPaid = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID )) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + lngBasicInfoID)));
                    //
                    if (dblTotalFine < dblAmountPaid)
                    {
                        dblActualFine = dblTotalFine;
                        strActualFine = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTotalFine) == "" ? "0" : Convert.ToString(dblTotalFine)));
                    }
                    else
                    {
                        dblActualFine = dblAmountPaid;
                        strActualFine = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblAmountPaid) == "" ? "0" : Convert.ToString(dblAmountPaid)));
                    }
                    //--
                    lblScheduleLastDate.Text = strReturnLastDate;
                    lblNoOfDelayDays.Text = intNoOfDays.Days.ToString();
                    lblLateFee.Text = strActualFine;
                    //
                    double dblFeeAlreadyPaid = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(LATE_FEE) AS SUM_TOTAL_LATE_FEE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(LATE_FEE) AS SUM_TOTAL_LATE_FEE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID)));
                    lblFeeAlreadyPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblFeeAlreadyPaid) == "" ? "0" : Convert.ToString(dblFeeAlreadyPaid)));
                    //
                    lblPayableFee.Text = "";
                    if (dblActualFine > dblFeeAlreadyPaid)
                    {
                        lblPayableFee.Text = "Fee payable : " + string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblActualFine - dblFeeAlreadyPaid) == "" ? "0" : Convert.ToString(dblActualFine - dblFeeAlreadyPaid)));
                        strPayableFee = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblActualFine - dblFeeAlreadyPaid) == "" ? "0" : Convert.ToString(dblActualFine - dblFeeAlreadyPaid)));  //-- Added By Abhishek Dey On 10/05/2018 --
                    }
                    //
                    lblDateofFiling.Text = "Date of Filing : " + mskFilingDate.Text;
                    strDateofFiling = mskFilingDate.Text;  //-- Added By Abhishek Dey On 10/05/2018 --
                    //
                    tbcView.Visible = true;
                    tbcView.SelectTab(tbpLateFine);
                }
                else
                {
                    tbcView.Visible = false;
                    //tbcView.SelectTab(tbpLateFine);
                    //
                    cmnService.J_UserMessage("No Late Fee", MessageBoxIcon.Information);
                    return;
                }
            }
            else if (rbnCorrectionReturn.Checked == true)
            {

            }
            //--------------------------------------------
            btnViewShortDeductions.ForeColor = Color.Black;
            lblShortDeductionsNos.ForeColor = Color.Black;
            lblShortDeductionsLabel.ForeColor = Color.Black;
            btnViewLatePayments.ForeColor = Color.Black;
            lblLatePaymentsNos.ForeColor = Color.Black;
            lblLatePaymentsLabel.ForeColor = Color.Black;
            btnViewLateDeductions.ForeColor = Color.Black;
            lblLateDeductionsNos.ForeColor = Color.Black;
            lblLateDeductionsLabel.ForeColor = Color.Black;
            //
            lblFilingDateLabel.ForeColor = Color.Blue;
            mskFilingDate.ForeColor = Color.Blue;
            btnViewLateFine.ForeColor = Color.Blue; 
        }
        #endregion
        
        #region txtDeducteeName_TextChanged
        private void txtDeducteeName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lngBasicInfoID == 0) return;
                //--
                if (blnExit == true) return;
                //--
                strSearchConditions = "";
                //
                if (rbnRegularReturn.Checked == true)
                {
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                    {
                        if (txtDeducteeName.Text != "")
                            strSearchConditions = strSearchConditions + " AND EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                        if (txtDeducteePAN.Text != "")
                            strSearchConditions = strSearchConditions + " AND EMPLOYEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";
                    }
                    else
                    {
                        if (txtDeducteeName.Text != "")
                            strSearchConditions = strSearchConditions + " AND DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                        if (txtDeducteePAN.Text != "")
                            strSearchConditions = strSearchConditions + " AND DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";
                    }
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    string strFormNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                    if (strFormNo == T_FormNo.F24Q)
                    {
                        if (txtDeducteeName.Text != "")
                            strSearchConditions = strSearchConditions + " AND EMPLOYEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                        if (txtDeducteePAN.Text != "")
                            strSearchConditions = strSearchConditions + " AND EMPLOYEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";
                    }
                    else
                    {
                        if (txtDeducteeName.Text != "")
                            strSearchConditions = strSearchConditions + " AND DEDUCTEE_NAME LIKE '" + cmnService.J_ReplaceQuote(txtDeducteeName.Text) + "%'";
                        if (txtDeducteePAN.Text != "")
                            strSearchConditions = strSearchConditions + " AND DEDUCTEE_PAN  LIKE '" + cmnService.J_ReplaceQuote(txtDeducteePAN.Text) + "%'";
                    }
                }
                //--
                if (tbcView.SelectedTab == tbpShortDeductions)
                {
                    if (cmbSection.SelectedIndex > 0)
                        strSearchConditions = strSearchConditions + " AND MST_SECTION.SECTION_ID =" + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex) + " ";
                    //
                    if (rbnRegularReturn.Checked == true)
                    {
                        //if(cmbQuarter.Text == T_Qtr.Q4)
                        //    PopulateShortDeductionsSD(lngBasicInfoID, strSearchConditions);
                        //else
                            PopulateShortDeductions(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strActualAmtCreation, strSearchConditions);
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                        PopulateShortDeductionsCorr(lngBasicInfoID, strActualAmtCreation, strSearchConditions);
                }
                else if (tbcView.SelectedTab == tbpLatePayments)
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        PopulateLatePayments(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strLastDateCreation, strSearchConditions, cmbFormNo.Text);
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                    {
                        //--
                        string strFormNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                        //--                
                        PopulateLatePaymentsCorr(lngBasicInfoID, strLastDateCreation, strSearchConditions, strFormNo);
                    }
                }
                else if (tbcView.SelectedTab == tbpInvalidPAN)
                {
                    if (rbnRegularReturn.Checked == true)
                        PopulateInvalidPANs(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);
                    else if (rbnCorrectionReturn.Checked == true)
                        PopulateInvalidPANsCorr(lngBasicInfoID,  strSearchConditions);
                }
                else if (tbcView.SelectedTab == tbpLateDeductions) //-- 2015/07/20
                {
                    if (rbnRegularReturn.Checked == true)
                        PopulateLateDeductions(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);
                    else if (rbnCorrectionReturn.Checked == true)
                        PopulateLateDeductionsCorr(lngBasicInfoID, strSearchConditions);
                }
                else if (tbcView.SelectedTab == tbpShortDeductionsSD)
                {
                    if (rbnRegularReturn.Checked == true)
                        PopulateShortDeductionsSD(lngBasicInfoID, strSearchConditions);
                    else
                        PopulateShortDeductionsCorrSD(lngBasicInfoID, strSearchConditions);
                }

            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region btnPrintGrid_Click
        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            RptDialog rptDialog = new RptDialog();
            try
            {
                if (tbcView.Visible == false)
                {
                    return;
                }
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this; 
                //
                this.Cursor = Cursors.WaitCursor;
                //
                if (tbcView.SelectedTab == tbpShortDeductions)
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        rptDialog.PrintShortDeductionRegular(lngBasicInfoID,
                                                 cmbFormNo.Text,
                                                 cmbQuarter.Text,
                                                 cmbFinancialYear.Text,
                                                 Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                                 Convert.ToString(cmbCompany.Text));
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                    {
                        blActivate = false;
                        rptDialog.PrintShortDeductionCorr(lngBasicInfoID,
                                                 Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)));
                    }
                }
                else if (tbcView.SelectedTab == tbpLatePayments)
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        rptDialog.PrintLatePaymentRegular(lngBasicInfoID,
                                              cmbFormNo.Text,
                                              cmbQuarter.Text,
                                              cmbFinancialYear.Text,
                                              Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                              Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                              Convert.ToString(cmbCompany.Text),
                                              lblTotalLabelLatePayments.Text + " Interest Amount Reported : " + lblInterestAmountReported.Text + " Interest Payable : " + lblInterestPayable.Text);
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                    {
                        blActivate = false;
                        rptDialog.PrintLatePaymentCorr(lngBasicInfoID,
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                                 lblTotalLabelLatePayments.Text + " Interest Amount Reported : " + lblInterestAmountReported.Text + " Interest Payable : " + lblInterestPayable.Text);
                    }
                }
                else if (tbcView.SelectedTab == tbpInvalidPAN)
                {
                    rptDialog.PrintInvalidPANChecking(lngBasicInfoID, 
                                                 strFormNo,
                                                 Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex)),
                                                 intAsstId,
                                                 cmbFinancialYear.Text,
                                                 strQuarter,
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)));
                }
                else if (tbcView.SelectedTab == tbpLateDeductions) //-- 2015/07/20
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        rptDialog.PrintLateDeductionRegular(lngBasicInfoID,
                                              cmbFormNo.Text,
                                              cmbQuarter.Text,
                                              cmbFinancialYear.Text,
                                              Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                              Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                              Convert.ToString(cmbCompany.Text),
                                              lblTotalLabelLateDeductions.Text + " Interest Amount Reported : " + lblInterestAmountReportedLD.Text + " Interest Payable : " + lblInterestPayableLD.Text); 
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                    {
                        blActivate = false;
                        rptDialog.PrintLateDeductionCorr(lngBasicInfoID,
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                                 Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                                   lblTotalLabelLateDeductions.Text + " Interest Amount Reported : " + lblInterestAmountReportedLD.Text + " Interest Payable : " + lblInterestPayableLD.Text);
                    }
                } 
                //-- 2016/04/11
                else if (tbcView.SelectedTab == tbpShortDeductionsSD)
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        rptDialog.PrintShortDeductionRegularSD(lngBasicInfoID,
                                                 cmbFormNo.Text,
                                                 cmbQuarter.Text,
                                                 cmbFinancialYear.Text,
                                                 Convert.ToString(cmbCompany.Text));
                    }
                    else if (rbnCorrectionReturn.Checked == true)
                    {
                        //string strFormNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                        //string strQtr = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT QTR     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));
                        //long lngFAYearId = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID)));
                        //string strCompanyName = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT QTR     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID));

                        rptDialog.PrintShortDeductionCorrSD(lngBasicInfoID);//,
                                                 //strFormNo,
                                                 //strQtr,
                                                 //cmbFinancialYear.Text,
                                                 //Convert.ToString(cmbCompany.Text));
                    }
                }
                //-- Added By Abhishek Dey On 07/05/2018 --
                // LATE FINE
                else if (tbcView.SelectedTab == tbpLateFine)
                {
                    if (rbnRegularReturn.Checked == true)
                    {
                        rptDialog.PrintLateFineRegularReturn(cmbFormNo.Text,
                                                 cmbQuarter.Text,
                                                 cmbFinancialYear.Text,
                                                 Convert.ToString(cmbCompany.Text),
                                                 lblScheduleLastDate.Text.Trim(),
                                                 lblNoOfDelayDays.Text.Trim(),
                                                 lblLateFee.Text.Trim(),
                                                 lblFeeAlreadyPaid.Text.Trim(),
                                                 //lblPayableFee.Text.Trim(),
                                                //lblDateofFiling.Text.Trim());
                                                 strPayableFee,
                                                 strDateofFiling);  //-- Modify By Abhishek Dey On 10/05/2018 --
                    }
                }
                //
                this.Cursor = Cursors.Default;
                //
            }
            catch //(Exception err)
            {
                this.Cursor = Cursors.Default;
                //
            }
        }
        #endregion

        #region Batch

        #region dgcViewBatch_Click
        private void dgcViewBatch_MouseClick(object sender, MouseEventArgs e)
        {
            dgcViewBatch_Click(sender, e);
        }
        #endregion

        #region dgcViewBatch_Click
        private void dgcViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dgcViewBatch.CurrentRow == null)
                //{
                //    lngBasicInfoID = 0;
                //    //ClearFields();
                //    LoadDeducteeGrid();
                //    return;
                //}
                //if (TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId > 0)
                //    return;
                //if (grpCorrectionReturn.Enabled == false)
                //    return;
                //
                //if (dgcViewBatch.CurrentRow.Index < 0)
                if (dgcViewBatch.RowCount < 0)
                {
                    lngBasicInfoID = 0;
                    ClearControls();
                    return;
                }
                //
                ClearControls();
                //
                if (TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId > 0 && lngBasicInfoID == 0)
                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId;
                else
                {
                    if (dgcViewBatch.CurrentRow == null) return;
                    lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                }
                //-- 2019/03/14
                strCompanyDeductorType = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MST_CATEGORY.CATEGORY_CODE FROM COR_TRN_COMPANY, MST_CATEGORY WHERE COR_TRN_COMPANY.D_CATEGORY_ID = MST_CATEGORY.CATEGORY_ID AND COR_TRN_COMPANY.BATCH_HEADER_ID = " + lngBasicInfoID));
                //
                LoadDetailsCorr(lngBasicInfoID);
                ControlSummaryBasicInfoCorr(lngBasicInfoID);
                //
                lblTotalLabelLatePayments.Text = "Total Interest : [Late Payments : " +string.Format("{0:0.00}",  TotalInterestLP )+ " + Late Deductions : " + string.Format("{0:0.00}", TotalInterestLD) + "]";
                //lblTotalInterest.Text = Convert.ToString(TotalInterestLP + TotalInterestLD);
                lblTotalInterest.Text = string.Format("{0:0.00}", TotalInterestLP + TotalInterestLD);
                lblTotalLabelLateDeductions.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", TotalInterestLP) + " + Late Deductions : " + string.Format("{0:0.00}", TotalInterestLD) + "]";
                //lblTotalInterestLD.Text = Convert.ToString(TotalInterestLP + TotalInterestLD);
                lblTotalInterestLD.Text = string.Format("{0:0.00}", TotalInterestLP + TotalInterestLD);
                //
                if (cmnService.J_ReturnDoubleValue(lblInterestAmountReportedLD.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text))
                    lblInterestPayableLD.Text = "0.00";
                else
                    lblInterestPayableLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                //
                if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                    lblInterestPayable.Text = "0.00";
                else
                    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                //
                //--
                if (cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text) > 0)
                {
                    if (lblInterestPayableLD.Text == "0.00")
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                    else
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                }
            }
            catch(Exception err)
            {
            }
        }
        #endregion

        #region dgcViewBatch_KeyDown
        private void dgcViewBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow.Index < 0) return;
                //
                //ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                //LoadDeducteeGridCorr(lngBasicInfoID);
                //strTempMode = lblMode.Text;
            }
            catch
            {
                //cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region dgcViewBatch_CurrentCellChanged
        private void dgcViewBatch_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dgcViewBatch.CurrentRow == null) return;
            //if (blnGridExit == true) return;
            //dgcViewBatch_Click(sender, e);
        }
        #endregion


        #region cmbQuarter_SelectedIndexChanged
        private void cmbQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngPrevBasicInfoID = 0;
            cmbMain_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region cmbFormNo_SelectedIndexChanged
        private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngPrevBasicInfoID = 0;
            cmbMain_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region cmbFinancialYear_SelectedIndexChanged
        private void cmbFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngPrevBasicInfoID = 0;
            cmbMain_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            lngPrevBasicInfoID = 0;
            cmbMain_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=LhFil1twLU8&list=PLy1JUN9HgGMxsogzvr_QrNOW-pyAmb-e0");
            //System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0039", txtEnterSerialNoOnline.Text, TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0043", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #endregion

        #region TrnHealthCheckupRegCorr_Deactivate
        private void TrnHealthCheckupRegCorr_Deactivate(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pPredictDefaultsId = 0;            
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ClearControls
        private void ClearControls()
        {
            lblShortDeductionsNos.Text = "";
            lblLatePaymentsNos.Text = "";
            lblInvalidPANNos.Text = "";
            blnExit = true;
            txtDeducteeName.Text = "";
            txtDeducteePAN.Text = "";
            blnExit = false;
            //
            strSQL = @"SELECT DISTINCT MST_SECTION.SECTION_ID AS SECTION_ID,
                                       MST_SECTION.SECTION_NO AS SECTION_NO 
                       FROM            MST_SECTION,
                                       " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @" 
                       WHERE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SHORT_DEDUCTION_SUMMARY + @".SECTION_ID = MST_SECTION.SECTION_ID";
            //dmlService.J_PopulateComboBox(strSQL, ref cmbSection);
            //
            tbcView.Visible = false;
            strSearchConditions = "";
            lblHideTabs.Text = "";
            //
            btnViewLatePayments.Enabled = false;
            btnViewLatePayments.BackColor = Color.LightGray;
            btnViewShortDeductions.Enabled = false;
            btnViewShortDeductions.BackColor = Color.LightGray;
            btnViewInvalidPAN.Enabled = false;
            btnViewInvalidPAN.BackColor = Color.LightGray;
            //
            btnViewShortDeductions.ForeColor = Color.Black;
            lblShortDeductionsNos.ForeColor = Color.Black;
            lblShortDeductionsLabel.ForeColor = Color.Black;
            btnViewLatePayments.ForeColor = Color.Black;
            lblLatePaymentsNos.ForeColor = Color.Black;
            lblLatePaymentsLabel.ForeColor = Color.Black;
            btnViewLateDeductions.ForeColor = Color.Black;
            lblLateDeductionsNos.ForeColor = Color.Black;
            lblLateDeductionsLabel.ForeColor = Color.Black;
            lblFilingDateLabel.ForeColor = Color.Black;
            mskFilingDate.ForeColor = Color.Black;
            btnViewLateFine.ForeColor = Color.Black; 
        }
        #endregion

        #region ClearSearchControls
        private void ClearSearchControls()
        {
            //
            if (rbnRegularReturn.Checked == true)
            {
                strSQL = @"SELECT DISTINCT MST_SECTION.SECTION_ID,
                                  MST_SECTION.SECTION_NO
                           FROM   " + strDeducteeDetailsTable + ", " +
                                     @"MST_SECTION,
                                  MST_NONSALARY_TAX_SLAB
                           WHERE  " + strDeducteeDetailsTable + ".SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID " +
                             @"AND    " + strDeducteeDetailsTable + ".SECTION_ID     = MST_SECTION.SECTION_ID  " +
                             @"AND    " + strDeducteeDetailsTable + ".DEDUCTED_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  " +
                             @"AND    " + strDeducteeDetailsTable + ".DEDUCTED_DATE IS NOT NULL " +
                             @"AND    " + strDeducteeDetailsTable + "." + strBasicInfoID + " = " + lngBasicInfoID + " ";
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                strSQL = @"SELECT DISTINCT MST_SECTION.SECTION_ID,
                                  MST_SECTION.SECTION_NO
                           FROM   COR_TRN_DEDUCTEE_DETAILS,
                                  MST_SECTION,
                                  MST_NONSALARY_TAX_SLAB
                           WHERE  COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                           AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                           AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                           AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                           AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " ";
            }
            blnExit = true;
            dmlService.J_PopulateComboBox(strSQL, ref cmbSection);
            txtDeducteeName.Text = "";
            txtDeducteePAN.Text = "";
            blnExit = false;
        }
        #endregion

        #region LoadDetailsRegular
        private void LoadDetailsRegular(long BasicInfoID)
        {
            long lngShortDeductionsNos = 0; 
            try
            {
                //--
                if (BasicInfoID == 0) return;
                //-- GET SUMMARY INFORMATION
                if (cmbFormNo.Text == T_FormNo.F24Q)
                {
                    strTableName = "MST_EMPLOYEE"; strFieldName = "EMPLOYEE_NAME"; strFieldPAN = "EMPLOYEE_PAN"; strFieldId = "EMPLOYEE_ID";
                }
                else
                {
                    strTableName = "MST_DEDUCTEE"; strFieldName = "DEDUCTEE_NAME"; strFieldPAN = "DEDUCTEE_PAN"; strFieldId = "DEDUCTEE_ID";
                }
                //--
                #region SUMMARY SHORT DEDUCTIONS
                //--
                #region COMMENT
                //                strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
//                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
//                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID', " +
//                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0), 
//                                    IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'C', " +
//                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0), 
//                                    ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0)))";
                //-- ANIK @ 2014-04-01
//                strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
//                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
//                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID', " +
//                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0), 
//                                    IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'C' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'F', " +
//                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0), 
                //                                    ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0)))";
                
//                //-- ANIK @ 2015-04-08
//                if (cmbFormNo.Text == T_FormNo.F24Q)
//                {
////                    strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
////                                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
////                                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID', " +
////                                           @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0), 
////                                    IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
////                                           @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0), 
////                                    ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0)))";
////                    //--
////                    strSQL = @"SELECT COUNT(*) 
////                               FROM   TRN_DEDUCTEE_DETAILS, 
////                                      " + strTableName + @",
////                                       MST_NONSALARY_TAX_SLAB 
////                               WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + " " +
////                             @"AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID  
////                               AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE
////                               AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
////                             @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
////                             @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
//                    strSQL = @"SELECT COUNT(*) 
//                               FROM   TRN_SALARY_DETAILS 
//                               WHERE  TRN_SALARY_DETAILS.BASIC_INFO_ID = " + BasicInfoID + @" 
//                               AND    TRN_SALARY_DETAILS
                //               ";
                //}
                //else
                //{
                #endregion
                //--
                //-- ANIK @ 2015-04-08
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2014_15ID && cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                {
                    #region Short Deductions for SALARY DETAILS
                    //
                    #region CREATE TABLE tblTEMP_PREDICT_SHORT_DED_SD
                    DMLService dmlServiceSD1 = new DMLService();
                    //if (CREATE_TEMP_TABLE(dmlServiceSD.J_pCommand, TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD) == false)
                    //{
                    //    return;
                    //}
                    dmlServiceSD1.J_BeginTransaction();
                    //
                    if (dmlServiceSD1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD) == true)
                    {
                        strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD;
                        dmlServiceSD1.J_ExecSql(dmlServiceSD1.J_pCommand, strSQL);
                    }
                    //
                    dmlServiceSD1.J_Commit();
                    dmlServiceSD1.Dispose();
                    //--
                    DMLService dmlServiceSD2 = new DMLService();
                    //
                    dmlServiceSD2.J_BeginTransaction();
                    //
                    if (dmlServiceSD2.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD) == false)
                    {
                        #region COMMENT
                        //strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + " (" +
                        //      "                  PREDICT_SHORT_DED_SD_ID   COUNTER," +
                        //      "                  BASIC_INFO_ID             NUMBER    DEFAULT 0," +
                        //      "                  ASST_ID                   NUMBER    DEFAULT 0," +
                        //      "                  SL_NO                     NUMBER    DEFAULT 0," +
                        //      "                  EMPLOYEE_ID               NUMBER    DEFAULT 0," +
                        //      "                  EMPLOYEE_PAN              TEXT(10)  DEFAULT \"\"," +
                        //      "                  EMPLOYEE_NAME             TEXT(255) DEFAULT \"\"," +
                        //      "                  CATEGORY                  TEXT(4)  DEFAULT \"\"," +
                        //      "                  TAXABLE_INCOME            MONEY     DEFAULT 0," +
                        //      "                  CURRENT_SALARY            MONEY     DEFAULT 0," +
                        //      "                  RELIEF_AMOUNT             MONEY     DEFAULT 0," +
                        //      "                  TAX_TO_PAY                MONEY     DEFAULT 0," +
                        //      "                  TDS_ENTERED               MONEY     DEFAULT 0," +
                        //      "                  SHORT_DEDUCTION_AMOUNT    MONEY     DEFAULT 0)"; 
                        #endregion
                        //
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" (
                             " + cmnService.J_GetDataType("PREDICT_SHORT_DED_SD_ID", J_Identity.YES) + @",
                             " + cmnService.J_GetDataType("BASIC_INFO_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("ASST_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SECTION_115BAC_FLAG", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAXABLE_INCOME", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CURRENT_SALARY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("RELIEF_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAX_TO_PAY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TDS_ENTERED", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SHORT_DEDUCTION_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("REBATE_US_87A_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @")";
                        dmlServiceSD2.J_ExecSql(dmlServiceSD2.J_pCommand, strSQL);
                    }
                    //
                    dmlServiceSD2.J_Commit();
                    //
                    dmlServiceSD2.Dispose();
                    //
                    #endregion
                    //
                    DMLService dmlServiceSD = new DMLService();
                    //System.Threading.Thread.Sleep(2000);
                    //
                    #region COMMENTED 2016/06/22
//                    strSQL = @"INSERT INTO  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" 
//                                          (BASIC_INFO_ID,
//                                           ASST_ID,
//                                           SL_NO,
//                                           EMPLOYEE_ID, 
//                                           EMPLOYEE_PAN, 
//                                           EMPLOYEE_NAME, 
//                                           CATEGORY,
//                                           TAXABLE_INCOME,
//                                           RELIEF_AMOUNT,
//                                           CURRENT_SALARY,
//                                           TDS_ENTERED) 
//                               SELECT      TRN_SALARY_DETAILS.BASIC_INFO_ID             AS BASIC_INFO_ID, 
//                                          " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
//                                           SL_NO,
//                                           MST_EMPLOYEE.EMPLOYEE_ID                     AS EMPLOYEE_ID,  
//                                           MST_EMPLOYEE.EMPLOYEE_PAN                    AS EMPLOYEE_PAN, 
//                                           MST_EMPLOYEE.EMPLOYEE_NAME                   AS EMPLOYEE_NAME, 
//                                           MST_EMPLOYEE.CATEGORY                        AS CATEGORY,  
//                                           -- COMMENTED IN NEXT SECTION ON 2016/06/22 {TOTAL_INCOME AS TAXABLE_INCOME,}
//                                          (TRN_SALARY_DETAILS.TAXABLE_AMOUNT + AIS_TOTAL) - (US_16_AGGREGATE + CVIA_DED_TOTAL) AS TAXABLE_INCOME,
//                                           TRN_SALARY_DETAILS.US_89_LESS                AS RELIEF_AMOUNT,
//                                           TRN_SALARY_DETAILS.TAXABLE_AMOUNT            AS CURRENT_SALARY,
//                                           -- COMMENTED IN NEXT SECTION ON 2016/06/22 {TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT + TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL}
//                                           TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT AS TDS_ENTERED
//                               FROM        TRN_SALARY_DETAILS, 
//                                           MST_EMPLOYEE
//                               WHERE       TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID 
//                               AND         TRN_SALARY_DETAILS.BASIC_INFO_ID = " + BasicInfoID;
                    
                    #endregion
                    //-- 2016/06/22
                    strSQL = @"INSERT INTO  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" 
                                          (BASIC_INFO_ID,
                                           ASST_ID,
                                           SL_NO,
                                           EMPLOYEE_ID, 
                                           EMPLOYEE_PAN, 
                                           EMPLOYEE_NAME, 
                                           CATEGORY,
                                           SECTION_115BAC_FLAG,
                                           TAXABLE_INCOME,
                                           RELIEF_AMOUNT,
                                           CURRENT_SALARY,
                                           TDS_ENTERED,
                                           REBATE_US_87A_AMOUNT) 
                               SELECT      TRN_SALARY_DETAILS.BASIC_INFO_ID             AS BASIC_INFO_ID, 
                                          " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) + @",
                                           SL_NO,
                                           MST_EMPLOYEE.EMPLOYEE_ID                     AS EMPLOYEE_ID,  
                                           MST_EMPLOYEE.EMPLOYEE_PAN                    AS EMPLOYEE_PAN, 
                                           MST_EMPLOYEE.EMPLOYEE_NAME                   AS EMPLOYEE_NAME, 
                                           MST_EMPLOYEE.CATEGORY                        AS CATEGORY,  
                                           TRN_SALARY_DETAILS.SECTION_115BAC_FLAG       AS SECTION_115BAC_FLAG,
                                           TRN_SALARY_DETAILS.TOTAL_INCOME              AS TAXABLE_INCOME,
                                           TRN_SALARY_DETAILS.US_89_LESS                AS RELIEF_AMOUNT,
                                           TRN_SALARY_DETAILS.TAXABLE_AMOUNT            AS CURRENT_SALARY,
                                           TRN_SALARY_DETAILS.TOTAL_TAX_DEDUCTED_AMOUNT + TRN_SALARY_DETAILS.PREVIOUS_TAX_DEDUCTED_TOTAL + TRN_SALARY_DETAILS.SUPER_ANN_TAX AS TDS_ENTERED,
                                           TRN_SALARY_DETAILS.REBATE_US_87A_AMOUNT      AS REBATE_US_87A_AMOUNT
                               FROM        TRN_SALARY_DETAILS, 
                                           MST_EMPLOYEE
                               WHERE       TRN_SALARY_DETAILS.EMPLOYEE_ID = MST_EMPLOYEE.EMPLOYEE_ID 
                               AND         TRN_SALARY_DETAILS.BASIC_INFO_ID = " + BasicInfoID;
                    //
                    if (dmlServiceSD.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD) == false)
                    {
                        //cmnService.J_UserMessage("tblTEMP_PREDICT_SHORT_DED_SD NOT FOUND 2");
                        return;
                    }
                    //
                    dmlServiceSD.J_ExecSql(dmlServiceSD.J_pCommand, strSQL);
                    //################################################################
                    #region COMMENTED
                    //#############################################################
//                    strSQL = "SELECT COUNT(*) AS COUNT_PREDICT_SHORT_DED_SD FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD;
//                    long lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
//                    //
//                    strSQL = @"SELECT PREDICT_SHORT_DED_SD_ID, 
//                                      BASIC_INFO_ID, 
//                                      ASST_ID, 
//                                      EMPLOYEE_ID, 
//                                      EMPLOYEE_PAN, 
//                                      EMPLOYEE_NAME,
//                                      CATEGORY, 
//                                      TAXABLE_INCOME, 
//                                      RELIEF_AMOUNT, 
//                                      TDS_ENTERED,
//                                      CURRENT_SALARY 
//                               FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" ";
//                    //
//                    IDataReader drdGetRecord = null;
//                    drdGetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
//                    //
//                    long lngArrayCounter = 0;
//                    //
//                    if (drdGetRecord == null)
//                        return;
//                    //--
//                    string[,] strArray;
//                    strArray = new string[lngRowCount, 17];
//                    //
//                    while (drdGetRecord.Read())
//                    {
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.PREDICT_SHORT_DED_SD_ID] = drdGetRecord["PREDICT_SHORT_DED_SD_ID"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.BASIC_INFO_ID] = drdGetRecord["BASIC_INFO_ID"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.ASST_ID] = drdGetRecord["ASST_ID"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.EMPLOYEE_ID] = drdGetRecord["EMPLOYEE_ID"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.EMPLOYEE_PAN] = drdGetRecord["EMPLOYEE_PAN"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.EMPLOYEE_NAME] = drdGetRecord["EMPLOYEE_NAME"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.CATEGORY] = drdGetRecord["CATEGORY"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.TAXABLE_INCOME] = drdGetRecord["TAXABLE_INCOME"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.RELIEF_AMOUNT] = drdGetRecord["RELIEF_AMOUNT"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.TDS_ENTERED] = drdGetRecord["TDS_ENTERED"].ToString();
//                        strArray[lngArrayCounter, (int)T_PREDICT_SHORT_DED_SD.CURRENT_SALARY] = drdGetRecord["CURRENT_SALARY"].ToString();
//                        //
//                        lngArrayCounter++;
//                    }
//                    drdGetRecord.Close();
//                    drdGetRecord.Dispose();
//                    //
//                    if (lngArrayCounter > 0)
//                    {
//                        double dblTaxTobePaid = 0; double dblTotalTaxTobePaid = 0; double dblTaxPaid = 0;
//                        double dblCalculatedECess = 0; double dblCalculatedSurcharge = 0; double dblCalculatedTaxCredit = 0;
//                        double dblShortDeductionsAmt = 0;
//                        //-------------------------------------------------------------------------
//                        for (long lngCounter = 0; lngCounter <= lngArrayCounter - 1; lngCounter++)
//                        {
//                            ////strArray[lngCounter, (int)T_GET_CHALLAN_DATA_FROM_EXCEL.BOOK_ENTRY_413]
//                            dblTaxTobePaid = TdsMan.CalculateIncomeTaxAmount(
//                                                cmnService.J_ReturnInt32Value(strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.ASST_ID]), 
//                                                Convert.ToString(strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.CATEGORY]),
//                                                cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.TAXABLE_INCOME]), 
//                                                out dblCalculatedECess, 
//                                                out dblCalculatedSurcharge, 
//                                                out dblCalculatedTaxCredit);
//                            //dblTotalTaxTobePaid = dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge;
//                            //-- ANIK @ 2015/04/17
//                            dblTotalTaxTobePaid = (dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge)
//                                                  - cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.RELIEF_AMOUNT]);
//                            // ------------------------------------------------------
//                            dblTaxPaid = cmnService.J_ReturnDoubleValue(strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.TDS_ENTERED]);
//                            // Now comparing Tax Paid with Tax to be paid.
//                            if (dblTaxPaid < dblTotalTaxTobePaid)
//                            {
//                                dblShortDeductionsAmt = dblTotalTaxTobePaid - dblTaxPaid;
//                                //
//                                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" 
//                                           SET   SHORT_DEDUCTION_AMOUNT  = " + dblShortDeductionsAmt + @" 
//                                           WHERE PREDICT_SHORT_DED_SD_ID = " + strArray[lngCounter, (int)T_PREDICT_SHORT_DED_SD.PREDICT_SHORT_DED_SD_ID];
//                                dmlService.J_ExecSql(strSQL);
//                            }
//                        }
//                    }
                    #endregion
                    //################################################################
                    DataSet ds = new DataSet();
                    //--
                    System.Threading.Thread.Sleep(5000);  //-- Added By Abhishek Dey On 16/05/2018 --
                    //--
                    strSQL = @"SELECT PREDICT_SHORT_DED_SD_ID, 
                                      BASIC_INFO_ID, 
                                      ASST_ID, 
                                      EMPLOYEE_ID, 
                                      EMPLOYEE_PAN, 
                                      EMPLOYEE_NAME,
                                      CATEGORY, 
                                      SECTION_115BAC_FLAG,
                                      TAXABLE_INCOME, 
                                      RELIEF_AMOUNT, 
                                      TDS_ENTERED,
                                      CURRENT_SALARY,
                                      REBATE_US_87A_AMOUNT 
                               FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" ";
                    //
                    ds = dmlService.J_ExecSqlReturnDataSet(dmlServiceSD.J_pCommand, strSQL);
                    //
                    double dblTaxTobePaid = 0; double dblTotalTaxTobePaid = 0; double dblTaxPaid = 0;
                    double dblCalculatedECess = 0; double dblCalculatedSurcharge = 0; double dblCalculatedTaxCredit = 0; double dblTaxonTotalIncomeB4TaxCredit = 0, dblTaxonTotalIncomeB4TaxCreditMarginalRelief = 0;
                    double dblShortDeductionsAmt = 0;
                    //                        
                    foreach (DataTable DtTable in ds.Tables)
                    {
                        foreach (DataRow dr in DtTable.Rows)
                        {
                            long lngVERIFIED_PAN_STATUS = 0;
                            lngVERIFIED_PAN_STATUS = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT VERIFIED_STATUS FROM MST_VERIFIED_PAN WHERE PAN ='" + dr["EMPLOYEE_PAN"].ToString() + "'")));
                            if (dr["EMPLOYEE_PAN"].ToString() == "PANNOTAVBL" ||
                                dr["EMPLOYEE_PAN"].ToString() == "PANAPPLIED" ||
                                dr["EMPLOYEE_PAN"].ToString() == "PANINVALID" ||
                                Convert.ToString(lngVERIFIED_PAN_STATUS) == "1" ||
                               Convert.ToString(lngVERIFIED_PAN_STATUS) == "2")
                            {
                                //Math.Round((dblAmtDifference * Convert.ToDouble(ds.Tables[0].Rows[i - 1]["TAX_RATE"]) / 100), 0, MidpointRounding.AwayFromZero)
                                dblTaxTobePaid = Math.Round((cmnService.J_ReturnDoubleValue(dr["TAXABLE_INCOME"].ToString()) * 20 / 100), 0, MidpointRounding.AwayFromZero);
                                dblShortDeductionsAmt = dblTaxTobePaid - cmnService.J_ReturnDoubleValue(dr["TDS_ENTERED"].ToString());
                                //
                                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" 
                                                                       SET   SHORT_DEDUCTION_AMOUNT = " + dblShortDeductionsAmt + @",
                                                                             TAX_TO_PAY             = " + dblTaxTobePaid + @",
                                                                             VERIFIED_PAN_STATUS    ='" + lngVERIFIED_PAN_STATUS + @"'
                                                                       WHERE PREDICT_SHORT_DED_SD_ID = " + dr["PREDICT_SHORT_DED_SD_ID"].ToString();
                                dmlService.J_ExecSql(dmlServiceSD.J_pCommand, strSQL);
                            }
                            else
                            {
                                //var ParentId = dr["ParentId"].ToString();
                                dblTaxTobePaid = TdsMan.CalculateIncomeTaxAmount(
                                                cmnService.J_ReturnInt32Value(dr["ASST_ID"].ToString()),
                                                dr["CATEGORY"].ToString(),
                                                cmnService.J_ReturnDoubleValue(dr["TAXABLE_INCOME"].ToString()),
                                                dr["SECTION_115BAC_FLAG"].ToString(),
                                                out dblCalculatedECess,
                                                out dblCalculatedSurcharge,
                                                out dblCalculatedTaxCredit,
                                                out dblTaxonTotalIncomeB4TaxCredit,
                                                    out dblTaxonTotalIncomeB4TaxCreditMarginalRelief);
                                //dblTotalTaxTobePaid = dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge;
                                dblTaxTobePaid = dblTaxonTotalIncomeB4TaxCredit;  //-- 2019/07/03 //-- COMMENTED ON 2021/03/04
                                                                                  //-- 2021/03/04
                                double dblActualRebate = 0;
                                if (cmnService.J_ReturnInt32Value(dr["ASST_ID"].ToString()) >= T_FinancialYearID.F2018_19ID)
                                {
                                    dblTaxTobePaid = dblTaxonTotalIncomeB4TaxCredit;
                                    if (cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString()) <= cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit))
                                        dblActualRebate = cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString());
                                    else
                                        dblActualRebate = cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit);
                                    //-- ANIK @ 2015/04/17
                                    dblTotalTaxTobePaid = (dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge)
                                                          - cmnService.J_ReturnDoubleValue(dr["RELIEF_AMOUNT"].ToString())
                                                          - dblActualRebate; //-- 2020/06/11
                                                                             //-- 2024/04/25
                                    if (cmnService.J_ReturnInt32Value(dr["ASST_ID"].ToString()) >= T_FinancialYearID.F2023_24ID)
                                    {
                                        if (dblTaxonTotalIncomeB4TaxCreditMarginalRelief > 0)
                                        {
                                            dblTaxTobePaid = dblTaxonTotalIncomeB4TaxCreditMarginalRelief;
                                            //lblRebateCalculated.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit)));
                                            dblTotalTaxTobePaid = (dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge)
                                                          - (dblTaxonTotalIncomeB4TaxCreditMarginalRelief - dblTaxonTotalIncomeB4TaxCredit)
                                                          - dblActualRebate;
                                        }
                                    }
                                }
                                else
                                {
                                    if (dblCalculatedTaxCredit <= dblTaxonTotalIncomeB4TaxCredit)
                                        dblTaxTobePaid = Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)) - Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit));
                                    //}
                                    //-- 2020/06/11
                                    //double dblActualRebate = 0;
                                    if (cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString()) <= cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit))
                                        dblActualRebate = cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString());
                                    else
                                        dblActualRebate = cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit);
                                    //-- ANIK @ 2015/04/17
                                    dblTotalTaxTobePaid = (dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge)
                                                          - cmnService.J_ReturnDoubleValue(dr["RELIEF_AMOUNT"].ToString())
                                                          - dblActualRebate; //-- 2020/06/11
                                                                             // ------------------------------------------------------
                                }
                                dblTaxPaid = cmnService.J_ReturnDoubleValue(dr["TDS_ENTERED"].ToString());
                                // Now comparing Tax Paid with Tax to be paid.
                                if (dblTaxPaid < dblTotalTaxTobePaid)
                                {
                                    dblShortDeductionsAmt = dblTotalTaxTobePaid - dblTaxPaid;
                                    //
                                    strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @" 
                                                                       SET   SHORT_DEDUCTION_AMOUNT = " + dblShortDeductionsAmt + @",
                                                                             TAX_TO_PAY             = " + dblTotalTaxTobePaid + @"
                                                                       WHERE PREDICT_SHORT_DED_SD_ID = " + dr["PREDICT_SHORT_DED_SD_ID"].ToString();
                                    dmlService.J_ExecSql(dmlServiceSD.J_pCommand, strSQL);
                                }
                            }
                        }
                    }
                    //
                    strSQL = "SELECT COUNT(*) AS COUNT_PREDICT_SHORT_DED_SD FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + " WHERE SHORT_DEDUCTION_AMOUNT > 0";
                    lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlServiceSD.J_pCommand, strSQL)));
                    if (lngShortDeductionsNos == 0)
                    {
                        btnViewShortDeductions.Enabled = false;
                        btnViewShortDeductions.BackColor = Color.LightGray;
                    }
                    else
                    {
                        btnViewShortDeductions.Enabled = true;
                        btnViewShortDeductions.BackColor = Color.Lavender;
                    }
                    //
                    dmlServiceSD.Dispose();
                    //
                    lblShortDeductionsNos.Text = Convert.ToString(lngShortDeductionsNos) + " nos.";
                    //------------------------------------------------
                    //System.Threading.Thread.Sleep(2000);
                    //
                    //PopulateShortDeductionsSD(lngBasicInfoID, strSearchConditions);
                    //return;
                    #endregion
                }
                else if (cmbFormNo.Text != T_FormNo.F27Q)
                {
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                    {
                        //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        //    strActualAmtCreation = @"CASE WHEN " + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID'  " +
                        //                           @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)  " +
                        //                           @"THEN ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * 20 ) / 100,0) END";
                        //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        //    //-- ANIK @ 2023/10/30
                        //    strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID' " +
                        //                           @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2), " +
                        //                           @"ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * 20) / 100, '0')),0))";
                        //
                        //strSQL = @"SELECT COUNT(*) 
                        //       FROM   ((TRN_DEDUCTEE_DETAILS 
                        //       INNER JOIN " + strTableName + @"
                        //           ON TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + @") 
                        //       LEFT JOIN MST_VERIFIED_PAN
                        //           ON  " + strTableName + "." + strFieldPAN + @" = MST_VERIFIED_PAN.PAN)
                        //       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                        //     @"AND    (TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
                        //     @"OR    MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2))";
                        strSQL = @"SELECT COUNT(*) 
                               FROM   ((TRN_DEDUCTEE_DETAILS 
                               INNER JOIN " + strTableName + @"
                                   ON TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + @") 
                               LEFT JOIN MST_VERIFIED_PAN
                                   ON  " + strTableName + "." + strFieldPAN + @" = MST_VERIFIED_PAN.PAN)
                               WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + @"
                               AND(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                               @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                               @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID' " +
                               @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)) ";
                    }
                    else
                    {
                        //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        //    strActualAmtCreation = @"CASE WHEN " + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID'  " +
                        //                           @"THEN ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0)
                        //                       ELSE  CASE
                        //                   WHEN " + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= '07/01/2021' THEN Round(( trn_deductee_details.payment_amount *
                        //                                                     mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE ) / 100, 0)
                        //                       ELSE CASE WHEN SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H'  " +
                        //                           @" THEN ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0)
                        //                       ELSE ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0) END END END";
                        //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        //    //-- ANIK @ 2014-05-23
                        //    strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                        //                           @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID', " +
                        //                           @"ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100, '0')),0), 
                        //                       iif(" + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= " + cmnService.J_DateOperator() + "07/01/2021" + cmnService.J_DateOperator() + @", Round(Val(Format(( trn_deductee_details.payment_amount * mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE) / 100, '0')), 0),
                        //                        IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                        //                           @"ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100, '0')),0), 
                        //                        ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100, '0')),0))))";
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                            strActualAmtCreation = @"CASE WHEN " + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                                                   @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                                                   @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID'  " +
                                                   @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)  " +
                                                   @"THEN ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0)
                                               ELSE  CASE
			                                        WHEN " + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= '07/01/2021' THEN Round(( trn_deductee_details.payment_amount *
                                                                             mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE ) / 100, 0)
                                               ELSE CASE WHEN SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H'  " +
                                                   @" THEN ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0)
                                               ELSE ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0) END END END";
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                            //-- ANIK @ 2014-05-23
                            strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                                                   @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                                                   @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID' " +
                                                   @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2), " +
                                                   @"ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100, '0')),0), 
                                           iif(" + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= " + cmnService.J_DateOperator() + "07/01/2021" + cmnService.J_DateOperator() + @", Round(Val(Format(( trn_deductee_details.payment_amount * mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE) / 100, '0')), 0),
                                            IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                                                   @"ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100, '0')),0), 
                                            ROUND(VAL(FORMAT((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100, '0')),0))))";
                        //
                        //
                        //strSQL = @"SELECT COUNT(*) 
                        //       FROM   TRN_DEDUCTEE_DETAILS, 
                        //              " + strTableName + "," +
                        //                    @"MST_NONSALARY_TAX_SLAB 
                        //       WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + " " +
                        //         @"AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID  
                        //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE
                        //       AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                        //         @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
                        //         @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                        //}
                        strSQL = @"SELECT COUNT(*) 
                               FROM   (((TRN_DEDUCTEE_DETAILS 
                               INNER JOIN " + strTableName + @"
                                   ON TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + @") 
                               INNER JOIN MST_NONSALARY_TAX_SLAB
                                   ON  TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID)  
                               LEFT JOIN MST_VERIFIED_PAN
                                   ON  " + strTableName + "." + strFieldPAN + @" = MST_VERIFIED_PAN.PAN)
                               WHERE  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE
                               AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                                 @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
                                 @"AND    (MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2) OR TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20))";
                    }
                    lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    if (lngShortDeductionsNos == 0)
                    {
                        btnViewShortDeductions.Enabled = false;
                        btnViewShortDeductions.BackColor = Color.LightGray;
                    }
                    else
                    {
                        btnViewShortDeductions.Enabled = true;
                        btnViewShortDeductions.BackColor = Color.Lavender;
                    }
                    //
                    lblShortDeductionsNos.Text = Convert.ToString(lngShortDeductionsNos) + " nos.";
                }
                #endregion
                //--
                #region SUMMARY LATE PAYMENTS
                //-- 2014/10/08
                #region COMMENT
                //strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                //                      "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                //                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                //                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                //                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                //                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                //                            "))";
//               strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
//                      "IIF((MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " + 
//                                 "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
//                      "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
//                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
//                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
//                            ")))";
//                //
//                strSQL = @"SELECT COUNT(*) 
//                           FROM   TRN_CHALLAN, 
//                                  TRN_DEDUCTEE_DETAILS 
//                           WHERE  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
//                           AND      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
//                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
//                         @"AND    TRN_CHALLAN.DEPOSIT_DATE        > " + strLastDateCreation + " " +
                //                          "AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                #endregion
                //
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    #region COMMENT
                    //                    strLastDateCreation = @"CONVERT(DATETIME,
                    //                                            CASE WHEN TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                    //                                                 THEN CAST(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                    //                                                 ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014 
                    //                                                           THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                    //                                                           ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 
                    //                                                                     THEN CONVERT(CHAR(10),'30' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                    //                                                                     ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                    //                                                                END 
                    //                                                      END 
                    //                                            END,103) ";

                    //                    //
                    //                    strSQL = @"SELECT COUNT(*) 
                    //                           FROM   TRN_CHALLAN, 
                    //                                  TRN_DEDUCTEE_DETAILS 
                    //                           WHERE  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                    //                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                    //                         @"AND    CONVERT(CHAR(8),TRN_CHALLAN.DEPOSIT_DATE,112) > CONVERT(CHAR(8)," + strLastDateCreation + ",112) " +
                    //                         " AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                    #endregion
                    if (cmbQuarter.Text == T_Qtr.Q4 && (strCompanyDeductorType == "A" || strCompanyDeductorType == "S")) //-- 2019/03/14
                        strLastDateCreation = @"CONVERT(DATETIME,
                                            CASE WHEN TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                                                 THEN CAST(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                                                 ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014 
                                                           THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                           ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND TRN_BASIC_INFO.FORM_NO <>'" + T_FormNo.F27EQ + @"'
                                                                     THEN CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                                     ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                                                                END 
                                                      END 
                                            END,103) ";
                    else
                        //-- 2018/05/14
                        strLastDateCreation = @"CONVERT(DATETIME,
                                            CASE WHEN TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                                                 THEN CAST(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                                                 ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014 
                                                           THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                           ELSE CASE WHEN MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND TRN_BASIC_INFO.FORM_NO <>'" + T_FormNo.F27EQ + @"'
                                                                     THEN CONVERT(CHAR(10),'30' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                                     ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                                                                END 
                                                      END 
                                            END,103) ";
                    //
                    strSQL = @"SELECT COUNT(*) 
                           FROM   TRN_BASIC_INFO, 
                                  TRN_CHALLAN, 
                                  TRN_DEDUCTEE_DETAILS 
                           WHERE  TRN_BASIC_INFO.BASIC_INFO_ID       = TRN_CHALLAN.BASIC_INFO_ID
                           AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                         @"AND    CONVERT(CHAR(8),TRN_CHALLAN.DEPOSIT_DATE,112) > CONVERT(CHAR(8)," + strLastDateCreation + ",112) " +
                         " AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    #region COMMENT
                    //                    strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                    //                      "IIF((MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                    //                                 "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                      "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                    //                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                    //                            ")))";
                    //                    //
                    //                    strSQL = @"SELECT COUNT(*) 
                    //                               FROM   TRN_CHALLAN, 
                    //                                      TRN_DEDUCTEE_DETAILS 
                    //                               WHERE  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                    //                               AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                    //                             @"AND    TRN_CHALLAN.DEPOSIT_DATE        > " + strLastDateCreation + " " +
                    //                              "AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                    #endregion
                    //--
                    if (cmbQuarter.Text == T_Qtr.Q4 && (strCompanyDeductorType == "A" || strCompanyDeductorType == "S")) //-- 2019/03/14
                        strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                          "IIF((MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                                     "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                      cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                          "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND TRN_BASIC_INFO.FORM_NO <>'" + T_FormNo.F27EQ + "', " +
                                  "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                          "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                                ")))";
                    else
                        strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                          "IIF((MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                                     "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                      cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                          "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND TRN_BASIC_INFO.FORM_NO <>'" + T_FormNo.F27EQ + "', " +
                                  "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                          "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                  cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                                ")))";
                    strSQL = @"SELECT COUNT(*) " +
                             @"FROM   TRN_BASIC_INFO, 
                                          TRN_CHALLAN, 
                                          TRN_DEDUCTEE_DETAILS 
                                   WHERE  TRN_BASIC_INFO.BASIC_INFO_ID       = TRN_CHALLAN.BASIC_INFO_ID
                                   AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID    = TRN_CHALLAN.CHALLAN_ID 
                                   AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                             @"AND    TRN_CHALLAN.DEPOSIT_DATE   > " + strLastDateCreation + " " +
                              "AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                    //--
                }
                long lngLatePaymentsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngLatePaymentsNos == 0)
                {
                    btnViewLatePayments.Enabled = false;
                    btnViewLatePayments.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewLatePayments.Enabled = true;
                    btnViewLatePayments.BackColor = Color.Lavender;
                }
                //
                lblLatePaymentsNos.Text = Convert.ToString(lngLatePaymentsNos) + " nos.";
                #endregion
                //--
                string strMidSubString = "";
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strMidSubString = "SUBSTRING";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strMidSubString = "MID";
                //--                
                #region SUMMARY INVALID PANs
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @" SELECT COUNT(*)
                            FROM       TRN_DEDUCTEE_DETAILS, 
                                       " + strTableName + " " +
                           @"WHERE     TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strTableName + "." + strFieldId + "  " +
                           @"AND       CASE WHEN " + strTableName + "." + strFieldPAN + " <> 'PANNOTAVBL' " +
                                             @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANAPPLIED' " +
                                             @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANINVALID' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'C' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'P' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'H' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'F' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'A' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'T' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'B' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'L' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'J' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'G' " +  
                                             @" THEN 'INVALID'
                                             ELSE 'VALID' END = 'INVALID'  
                                AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @" SELECT COUNT(*)
                            FROM       TRN_DEDUCTEE_DETAILS, 
                                       " + strTableName + " " +
                           @"WHERE     TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strTableName + "." + strFieldId + "  " +
                           @"AND       IIF(" + strTableName + "." + strFieldPAN + " <> 'PANNOTAVBL' " +
                                             @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANAPPLIED' " +
                                             @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANINVALID' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'C' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'P' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'H' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'F' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'A' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'T' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'B' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'L' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'J' " +
                                             @"AND             " + strMidSubString + "(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'G' " +
                                             @",'INVALID'
                                             ,'VALID') ='INVALID'  
                                AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";
                long lngInvalidPANNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngInvalidPANNos == 0)
                {
                    btnViewInvalidPAN.Enabled = false;
                    btnViewInvalidPAN.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewInvalidPAN.Enabled = true;
                    btnViewInvalidPAN.BackColor = Color.Lavender;
                }
                //
                lblInvalidPANNos.Text = Convert.ToString(lngInvalidPANNos) + " nos.";
                #endregion
                //--
                #region SUMMARY LATE DEDUCTIONS
                //-- ANIK @ 2015/07/20
                strSQL = @"SELECT COUNT(*) AS LATE_DEDUCTIONS 
                            FROM   TRN_DEDUCTEE_DETAILS , MST_REASON
                            WHERE TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID
                            AND TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + @"
                            AND TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                            AND TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                            AND MST_REASON.REASON <> 'Y'";
                long lngLateDeductions = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngLateDeductions == 0)
                {
                    btnViewLateDeductions.Enabled = false;
                    btnViewLateDeductions.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewLateDeductions.Enabled = true;
                    btnViewLateDeductions.BackColor = Color.Lavender;
                }
                //
                lblLateDeductionsNos.Text = Convert.ToString(lngLateDeductions) + " nos.";
                #endregion
                //-- GET DETAIL INFORMATION
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2014_15ID && cmbFormNo.Text == T_FormNo.F24Q && cmbQuarter.Text == T_Qtr.Q4)
                    PopulateShortDeductionsSD(lngBasicInfoID, strSearchConditions);
                else
                    PopulateShortDeductions(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strActualAmtCreation, strSearchConditions);
                //
                PopulateLatePayments(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strLastDateCreation, strSearchConditions, cmbFormNo.Text);
                PopulateInvalidPANs(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);
                //-- 2015/07/20
                PopulateLateDeductions(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);
                //--
                lblTotalLabelLatePayments.Text = "Total Interest :";
                //
                lblTotalLabelLateDeductions.Text = "Total Interest :";
                //-- 2017/12/21
                PopulateLateFilingDetailsRegular(BasicInfoID);
                //--
                ClearSearchControls();

                lblTotalLabelLatePayments.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP, 2, MidpointRounding.AwayFromZero)) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD, 2)) + "]";
                //lblTotalLabelLatePayments.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP / 100, 0) * 100) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD / 100, 0) * 100) + "]";
                //lblTotalInterest.Text = Convert.ToString(Math.Round(TotalInterestLP + TotalInterestLD, 2));
                lblTotalInterest.Text = string.Format("{0:0.00}", Math.Round(TotalInterestLP + TotalInterestLD, 2));
                //
                lblTotalLabelLateDeductions.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP, 2)) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD, 2)) + "]";
                //lblTotalLabelLateDeductions.Text = "Total Interest : [Late Payments : " + string.Format("{0:0.00}", Math.Round(TotalInterestLP/100,0)*100) + " + Late Deductions : " + string.Format("{0:0.00}", Math.Round(TotalInterestLD/100, 0)*100) + "]";
                lblTotalInterestLD.Text = Convert.ToString(Math.Round(TotalInterestLP + TotalInterestLD,2 ));
                lblTotalInterestLD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(lblTotalInterestLD.Text) == "" ? "0" : Convert.ToString(lblTotalInterestLD.Text)));
                //
                if (cmnService.J_ReturnDoubleValue(lblInterestAmountReportedLD.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text))
                    lblInterestPayableLD.Text = "0.00";
                else
                    lblInterestPayableLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                //
                if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                    lblInterestPayable.Text = "0.00";
                else
                    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                //--
                if (lngShortDeductionsNos > 0)
                    TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                else if (lngLatePaymentsNos > 0)
                {
                    if (lblInterestPayable.Text == "0.00")
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                    else
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                }
                else if (lngLateDeductions > 0)
                {
                    if (lblInterestPayableLD.Text == "0.00")
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                    else
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                }
                else
                    TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, false, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                //--       
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region LoadDetailsCorr
        private void LoadDetailsCorr(long BasicInfoID)
        {
            string strFormNo = "";
            string strQtr = "";
            long lngFAYearId = 0;
            long lngShortDeductionsNos = 0;
            try
            {
                //--
                if (BasicInfoID == 0) return;
                //--
                strFormNo =Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + BasicInfoID));
                strFormNo = TdsMan.GetFormNoIT1961(strFormNo);
                strQtr = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT QTR     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + BasicInfoID));
                lngFAYearId = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ASST_ID     FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + BasicInfoID)));
                //--
                #region SUMMARY SHORT DEDUCTIONS
                //--
                //-- ANIK @ 2015-04-08
                if (lngFAYearId >= T_FinancialYearID.F2014_15ID && strFormNo == T_FormNo.F24Q && strQtr == T_Qtr.Q4)
                {
                    #region Short Deductions for SALARY DETAILS
                    //
                    #region CREATE TABLE tblTEMP_PREDICT_SHORT_DED_SD
                    DMLService dmlServiceSD1 = new DMLService();
                    //if (CREATE_TEMP_TABLE(dmlServiceSD.J_pCommand, TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD) == false)
                    //{
                    //    return;
                    //}
                    dmlServiceSD1.J_BeginTransaction();
                    //
                    if (dmlServiceSD1.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD) == true)
                    {
                        strSQL = "DROP TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD;
                        dmlServiceSD1.J_ExecSql(dmlServiceSD1.J_pCommand, strSQL);
                    }
                    //
                    dmlServiceSD1.J_Commit();
                    dmlServiceSD1.Dispose();
                    //--
                    DMLService dmlServiceSD2 = new DMLService();
                    //MessageBox.Show("1");
                    //
                    dmlServiceSD2.J_BeginTransaction();
                    //
                    if (dmlServiceSD2.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD) == false)
                    {
                        //
                        strSQL = "CREATE TABLE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + @" (
                             " + cmnService.J_GetDataType("PREDICT_SHORT_DED_SD_ID", J_Identity.YES) + @",
                             " + cmnService.J_GetDataType("BATCH_HEADER_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("ASST_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SECTION_115BAC_FLAG", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAXABLE_INCOME", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CURRENT_SALARY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("RELIEF_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAX_TO_PAY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TDS_ENTERED", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SHORT_DEDUCTION_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("REBATE_US_87A_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @")";
                        dmlServiceSD2.J_ExecSql(dmlServiceSD2.J_pCommand, strSQL);
                    }
                    //
                    dmlServiceSD2.J_Commit();
                    //
                    //MessageBox.Show("2");
                    dmlServiceSD2.Dispose();
                    //
                    #endregion
                    //
                    DMLService dmlServiceSD = new DMLService();
                    //-- 2016/06/22
                    strSQL = @"INSERT INTO  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + @" 
                                          (BATCH_HEADER_ID,
                                           ASST_ID,
                                           SL_NO,
                                           EMPLOYEE_ID, 
                                           EMPLOYEE_PAN, 
                                           EMPLOYEE_NAME, 
                                           CATEGORY,
                                           SECTION_115BAC_FLAG,
                                           TAXABLE_INCOME,
                                           RELIEF_AMOUNT,
                                           CURRENT_SALARY,
                                           TDS_ENTERED,
                                           REBATE_US_87A_AMOUNT) 
                               SELECT      BATCH_HEADER_ID           AS BATCH_HEADER_ID, 
                                          " + lngFAYearId + @"       AS ASST_ID,    
                                           SL_NO                     AS SL_NO,
                                           TRN_SALARY_DETAILS_ID     AS EMPLOYEE_ID,  
                                           EMPLOYEE_PAN              AS EMPLOYEE_PAN, 
                                           EMPLOYEE_NAME             AS EMPLOYEE_NAME, 
                                           EMPLOYEE_CATEGORY         AS CATEGORY,  
                                           SECTION_115BAC_FLAG       AS SECTION_115BAC_FLAG,
                                           TOTAL_INCOME              AS TAXABLE_INCOME,
                                           US_89_LESS                AS RELIEF_AMOUNT,
                                           TAXABLE_AMOUNT            AS CURRENT_SALARY,
                                           TOTAL_TAX_DEDUCTED_AMOUNT + PREVIOUS_TAX_DEDUCTED_TOTAL + SUPER_ANN_TAX AS TDS_ENTERED,
                                           REBATE_US_87A_AMOUNT      AS REBATE_US_87A_AMOUNT
                               FROM        COR_TRN_SALARY_DETAILS
                               WHERE       COR_TRN_SALARY_DETAILS.MODE <>'" + T_CorrectionMode.Delete + @"'
                               AND         COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + BasicInfoID;
                    //
                    if (dmlServiceSD.J_IsDatabaseObjectExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD) == false)
                    {
                        //cmnService.J_UserMessage("tblTEMP_PREDICT_SHORT_DED_SD NOT FOUND 2");
                        return;
                    }
                    //
                    //MessageBox.Show("3");
                    dmlServiceSD.J_BeginTransaction();
                    dmlServiceSD.J_ExecSql(dmlServiceSD.J_pCommand, strSQL);
                    dmlServiceSD.J_Commit();
                    //
                    DataSet ds = new DataSet();
                    //--
                    //System.Threading.Thread.Sleep(5000);  //-- Added By Abhishek Dey On 16/05/2018 --
                    //--
                    strSQL = @"SELECT PREDICT_SHORT_DED_SD_ID, 
                                      BATCH_HEADER_ID, 
                                      ASST_ID, 
                                      EMPLOYEE_ID, 
                                      EMPLOYEE_PAN, 
                                      EMPLOYEE_NAME,
                                      CATEGORY, 
                                      SECTION_115BAC_FLAG,
                                      TAXABLE_INCOME, 
                                      RELIEF_AMOUNT, 
                                      TDS_ENTERED,
                                      CURRENT_SALARY,
                                      REBATE_US_87A_AMOUNT 
                               FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + @" ORDER BY PREDICT_SHORT_DED_SD_ID";
                    //
                    ds = dmlService.J_ExecSqlReturnDataSet(dmlServiceSD.J_pCommand, strSQL);
                    //
                    //MessageBox.Show("4");
                    double dblTaxTobePaid = 0; double dblTotalTaxTobePaid = 0; double dblTaxPaid = 0;
                    double dblCalculatedECess = 0; double dblCalculatedSurcharge = 0; double dblCalculatedTaxCredit = 0; double dblTaxonTotalIncomeB4TaxCredit = 0;
                    double dblShortDeductionsAmt = 0;
                    //                        
                    foreach (DataTable DtTable in ds.Tables)
                    {
                        foreach (DataRow dr in DtTable.Rows)
                        {
                            if (dr["EMPLOYEE_NAME"].ToString() == "BISWAJIT GHOSAL")
                            {

                            }
                            //var ParentId = dr["ParentId"].ToString();
                            dblTaxTobePaid = TdsMan.CalculateIncomeTaxAmount(
                                                cmnService.J_ReturnInt32Value(dr["ASST_ID"].ToString()),
                                                dr["CATEGORY"].ToString(),
                                                cmnService.J_ReturnDoubleValue(dr["TAXABLE_INCOME"].ToString()),
                                                dr["SECTION_115BAC_FLAG"].ToString(),
                                                out dblCalculatedECess,
                                                out dblCalculatedSurcharge,
                                                out dblCalculatedTaxCredit,
                                                out dblTaxonTotalIncomeB4TaxCredit);

                            //MessageBox.Show("4.1");

                            //dblTotalTaxTobePaid = dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge;
                            //////dblTaxTobePaid = dblTaxonTotalIncomeB4TaxCredit;  //-- 2019/07/03 //-- COMMENTED ON 2021/03/04
                            //-- 2021/03/04
                            if (cmnService.J_ReturnInt32Value(dr["ASST_ID"].ToString()) >= T_FinancialYearID.F2018_19ID)
                            {
                                dblTaxTobePaid = dblTaxonTotalIncomeB4TaxCredit;
                            }
                            else
                            {
                                if (dblCalculatedTaxCredit <= dblTaxonTotalIncomeB4TaxCredit)
                                    dblTaxTobePaid =  Convert.ToDouble(Convert.ToString(dblTaxonTotalIncomeB4TaxCredit)) - Convert.ToDouble(Convert.ToString(dblCalculatedTaxCredit));
                            }
                            //-- 2020/06/11
                            double dblActualRebate = 0;
                            if (cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString()) <= cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit))
                                dblActualRebate = cmnService.J_ReturnDoubleValue(dr["REBATE_US_87A_AMOUNT"].ToString());
                            else
                                dblActualRebate = cmnService.J_ReturnDoubleValue(dblCalculatedTaxCredit);
                            //-- ANIK @ 2015/04/17
                            dblTotalTaxTobePaid = (dblTaxTobePaid + dblCalculatedECess + dblCalculatedSurcharge)
                                                  - cmnService.J_ReturnDoubleValue(dr["RELIEF_AMOUNT"].ToString())
                                                  - dblActualRebate; //-- 2020/06/11
                            // ------------------------------------------------------
                            dblTaxPaid = cmnService.J_ReturnDoubleValue(dr["TDS_ENTERED"].ToString());
                            // Now comparing Tax Paid with Tax to be paid.
                            if (dblTaxPaid < dblTotalTaxTobePaid)
                            {
                                dblShortDeductionsAmt = dblTotalTaxTobePaid - dblTaxPaid;
                                //
                                strSQL = @"UPDATE " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + @" 
                                                                       SET   SHORT_DEDUCTION_AMOUNT = " + dblShortDeductionsAmt + @",
                                                                             TAX_TO_PAY             = " + dblTotalTaxTobePaid + @"
                                                                       WHERE PREDICT_SHORT_DED_SD_ID = " + dr["PREDICT_SHORT_DED_SD_ID"].ToString();
                                dmlServiceSD.J_BeginTransaction();
                                dmlService.J_ExecSql(dmlServiceSD.J_pCommand, strSQL);
                                dmlServiceSD.J_Commit();
                                //MessageBox.Show("4.2");
                            }
                        }
                    }
                    //
                    //MessageBox.Show("5");
                    strSQL = "SELECT COUNT(*) AS COUNT_PREDICT_SHORT_DED_SD FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + " WHERE SHORT_DEDUCTION_AMOUNT > 0";
                    lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(dmlServiceSD.J_pCommand, strSQL)));
                    //MessageBox.Show("6");
                    if (lngShortDeductionsNos == 0)
                    {
                        btnViewShortDeductions.Enabled = false;
                        btnViewShortDeductions.BackColor = Color.LightGray;
                    }
                    else
                    {
                        btnViewShortDeductions.Enabled = true;
                        btnViewShortDeductions.BackColor = Color.Lavender;
                    }
                    //
                    dmlServiceSD.Dispose();
                    //
                    lblShortDeductionsNos.Text = Convert.ToString(lngShortDeductionsNos) + " nos.";
                    //------------------------------------------------
                    //System.Threading.Thread.Sleep(2000);
                    //
                    //PopulateShortDeductionsSD(lngBasicInfoID, strSearchConditions);
                    //return;
                    #endregion
                }
                else if (strFormNo != T_FormNo.F27Q)
                {
                    #region SHORT DEDUCTION OTHER THAN SALARY DETAILS
                    //                    strActualAmtCreation = @"IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' " +
                    //                                           @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN  = 'PANAPPLIED' " +
                    //                                           @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN  = 'PANINVALID', " +
                    //                                           @"ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0), 
                    //                                    IIF(MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H', " +
                    //                                           @"ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0), 
                    //                                    ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0)))";
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strActualAmtCreation = @"CASE WHEN      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' " +
                                                           @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANAPPLIED' " +
                                                           @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANINVALID' " +
                                                    @"THEN ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0) 
                                                      ELSE CASE WHEN SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H' " +
                                                              @"THEN ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0)
                                                                ELSE ROUND((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0) 
                                                           END 
                                                END";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strActualAmtCreation = @"IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' " +
                                       @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN  = 'PANAPPLIED' " +
                                       @"OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN  = 'PANINVALID', " +
                                       @"ROUND(VAL(FORMAT((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100, '0')),0), 
                                    IIF(MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H', " +
                                       @"ROUND(VAL(FORMAT((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100, '0')),0), 
                                    ROUND(VAL(FORMAT((COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100, '0')),0)))";
                    strSQL = @"SELECT COUNT(*) 
                           FROM   COR_TRN_DEDUCTEE_DETAILS, 
                                  MST_NONSALARY_TAX_SLAB 
                           WHERE  COR_TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID  
                           AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE
                           AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                             @"AND    COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
                             @"AND    COR_TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                    //long lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                    if (lngShortDeductionsNos == 0)
                    {
                        btnViewShortDeductions.Enabled = false;
                        btnViewShortDeductions.BackColor = Color.LightGray;
                    }
                    else
                    {
                        btnViewShortDeductions.Enabled = true;
                        btnViewShortDeductions.BackColor = Color.Lavender;
                    }
                    //
                    lblShortDeductionsNos.Text = Convert.ToString(lngShortDeductionsNos) + " nos.";
                    //
                    #endregion
                }
                #endregion
                //--
                #region SUMMARY LATE PAYMENTS
                #region COMMENT
                //strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                //                      "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                //                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                //                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                //                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                //                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                //                            "))";
                //-- 2014-12-02
//                strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
//                                  "IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
//                                             "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
//                                  "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
//                                          "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                                          cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
//                                          "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
//                                          cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
//                                        ")))";
//                strSQL = @"SELECT COUNT(*) 
//                           FROM   COR_TRN_CHALLAN, 
//                                  COR_TRN_DEDUCTEE_DETAILS 
//                           WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID  
//                           AND    COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
//                           AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
//                         @"AND    COR_TRN_CHALLAN.DEPOSIT_DATE        > " + strLastDateCreation + " " +
                //                          "AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                #endregion
                //
                 if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    #region COMMENT
                    //ADDED BY DHRUBA ON 12/06/2015
                    //                    strLastDateCreation = @"CONVERT(DATETIME, 
                    //                                            CASE WHEN COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                    //                                                 THEN CAST(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                    //                                                 ELSE CASE WHEN (MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014)
                    //                                                           THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                    //                                                           ELSE CASE WHEN MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 
                    //                                                                     THEN CONVERT(CHAR(10),'30' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                    //                                                                     ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                    //                                                                END 
                    //                                                      END 
                    //                                            END,103)";

                    //                    strSQL = @"SELECT COUNT(*) 
                    //                               FROM   COR_TRN_CHALLAN, 
                    //                                      COR_TRN_DEDUCTEE_DETAILS 
                    //                               WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                    //                               AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                    //                            @" AND    CONVERT(CHAR(8),COR_TRN_CHALLAN.DEPOSIT_DATE,112) > CONVERT(CHAR(8)," + strLastDateCreation + ",112) " +
                    //                            @" AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                    #endregion
                    //--
                    if (strQtr == T_Qtr.Q4 && (strCompanyDeductorType == "A" || strCompanyDeductorType == "S")) //-- 2019/03/14
                        strLastDateCreation = @"CONVERT(DATETIME, 
                                                CASE WHEN COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                                                     THEN CAST(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                                                     ELSE CASE WHEN (MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014)
                                                               THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                               ELSE CASE WHEN MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND COR_HDR_BATCH.FORM_NO <>'" + T_FormNo.F27EQ + @"' 
                                                                         THEN CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                                         ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                                                                    END 
                                                          END 
                                                END,103)";
                    else
                        strLastDateCreation = @"CONVERT(DATETIME, 
                                            CASE WHEN COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL 
                                                 THEN CAST(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS CHAR)
                                                 ELSE CASE WHEN (MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014)
                                                           THEN CONVERT(CHAR(10),'10' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                           ELSE CASE WHEN MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND COR_HDR_BATCH.FORM_NO <>'" + T_FormNo.F27EQ + @"' 
                                                                     THEN CONVERT(CHAR(10),'30' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103)
                                                                     ELSE CONVERT(CHAR(10),'07' + RIGHT(CONVERT(CHAR(10),DATEADD(MONTH, 1,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)),103),8),103) 
                                                                END 
                                                      END 
                                            END,103)";

                    strSQL = @"SELECT COUNT(*) 
                               FROM   COR_TRN_CHALLAN, 
                                      COR_TRN_DEDUCTEE_DETAILS,
                                      COR_HDR_BATCH  
                               WHERE  COR_HDR_BATCH.BATCH_HEADER_ID            = COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID 
                               AND    COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                               AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                            @" AND    CONVERT(CHAR(8),COR_TRN_CHALLAN.DEPOSIT_DATE,112) > CONVERT(CHAR(8)," + strLastDateCreation + ",112) " +
                            @" AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                }
                else  if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    #region COMMENT
                    //                    strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                    //                                      "IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                    //                                                 "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                                                  cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                                      "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                    //                                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                    //                                            ")))";

                    //                    strSQL = @"SELECT COUNT(*) 
                    //                               FROM   COR_TRN_CHALLAN, 
                    //                                      COR_TRN_DEDUCTEE_DETAILS 
                    //                               WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                    //                               AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                    //                            @" AND    COR_TRN_CHALLAN.DEPOSIT_DATE             > " + strLastDateCreation + " " +
                    //                            @" AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                    //strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                    //                  "IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                    //                             "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                              cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                  "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                    //                          "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                          cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                    //                          "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                    //                          cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                    //                        ")))";
                    #endregion
                    //
                    if (strQtr == T_Qtr.Q4 && (strCompanyDeductorType == "A" || strCompanyDeductorType == "S")) //-- 2019/03/14
                        strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                                        "IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                                                   "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                    cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                                        "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND COR_HDR_BATCH.FORM_NO <>'" + T_FormNo.F27EQ + "', " +
                                                "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                                        "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                                              ")))";
                    else
                        strLastDateCreation = "IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                                        "IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 9 and YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2014) , " +
                                                   "CDATE(" + "\"10\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                    cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                                        "IIF(MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND COR_HDR_BATCH.FORM_NO <>'" + T_FormNo.F27EQ + "', " +
                                                "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                                        "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                                cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                                              ")))";

                    strSQL = @"SELECT COUNT(*) 
                               FROM   COR_TRN_CHALLAN, 
                                      COR_TRN_DEDUCTEE_DETAILS,
                                      COR_HDR_BATCH 
                               WHERE  COR_HDR_BATCH.BATCH_HEADER_ID            = COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID 
                               AND    COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID  = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                               AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                            @" AND    COR_TRN_CHALLAN.DEPOSIT_DATE             > " + strLastDateCreation + " " +
                            @" AND    COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
                }               
                //
                long lngLatePaymentsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngLatePaymentsNos == 0)
                {
                    btnViewLatePayments.Enabled = false;
                    btnViewLatePayments.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewLatePayments.Enabled = true;
                    btnViewLatePayments.BackColor = Color.Lavender;
                }
                //
                lblLatePaymentsNos.Text = Convert.ToString(lngLatePaymentsNos) + " nos.";
                #endregion
                //--
                #region SUMMARY INVALID PANs
                #region COMMENT
                //                strSQL = @" SELECT COUNT(*)
//                        FROM       COR_TRN_DEDUCTEE_DETAILS " +
//                       @"WHERE     IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL' " +
//                                         @"AND             COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANAPPLIED' " +
//                                         @"AND             COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANINVALID' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'C' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'P' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'H' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'F' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'A' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'T' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'B' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'L' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'J' " +
//                                         @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'G' " +
//                                         @",'INVALID'
//                                         ,'VALID') ='INVALID'  
                //                            AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " ";
                #endregion
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @" SELECT COUNT(*)
                                FROM       COR_TRN_DEDUCTEE_DETAILS
                                WHERE     CASE WHEN COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL' " +
                                             @"AND  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANAPPLIED' " +
                                             @"AND  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANINVALID' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'C' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'P' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'H' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'F' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'A' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'T' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'B' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'L' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'J' " +
                                             @"AND             SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'G' " +
                                             @" THEN 'INVALID'
                                             ELSE 'VALID' END = 'INVALID'  
                                AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @" SELECT COUNT(*)
                            FROM       COR_TRN_DEDUCTEE_DETAILS " +
                           @"WHERE     IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL' " +
                                             @"AND             COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANAPPLIED' " +
                                             @"AND             COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANINVALID' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'C' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'P' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'H' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'F' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'A' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'T' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'B' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'L' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'J' " +
                                             @"AND             MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'G' " +
                                             @",'INVALID'
                                             ,'VALID') ='INVALID'  
                                AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " ";
                
                long lngInvalidPANNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngInvalidPANNos == 0)
                {
                    btnViewInvalidPAN.Enabled = false;
                    btnViewInvalidPAN.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewInvalidPAN.Enabled = true;
                    btnViewInvalidPAN.BackColor = Color.Lavender;
                }
                //
                lblInvalidPANNos.Text = Convert.ToString(lngInvalidPANNos) + " nos.";
                #endregion
                //--
                #region SUMMARY LATE DEDUCTIONS

                //-- ANIK @ 2015/07/20
                strSQL = @"SELECT COUNT(*) AS LATE_DEDUCTIONS 
                            FROM   COR_TRN_DEDUCTEE_DETAILS , MST_REASON
                            WHERE COR_TRN_DEDUCTEE_DETAILS.REASON_ID = MST_REASON.REASON_ID
                            AND COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + @"
                            AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                            AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                            AND MST_REASON.REASON <> 'Y'";
                long lngLateDeductions = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                if (lngLateDeductions == 0)
                {
                    btnViewLateDeductions.Enabled = false;
                    btnViewLateDeductions.BackColor = Color.LightGray;
                }
                else
                {
                    btnViewLateDeductions.Enabled = true;
                    btnViewLateDeductions.BackColor = Color.Lavender;
                }
                //
                lblLateDeductionsNos.Text = Convert.ToString(lngLateDeductions) + " nos.";
                #endregion
                //-- GET DETAIL INFORMATION
                if (lngFAYearId >= T_FinancialYearID.F2014_15ID && strFormNo == T_FormNo.F24Q && strQtr == T_Qtr.Q4)
                    PopulateShortDeductionsCorrSD(BasicInfoID, strSearchConditions);
                else
                    PopulateShortDeductionsCorr(BasicInfoID, strActualAmtCreation, strSearchConditions);
                //--
                PopulateLatePaymentsCorr(BasicInfoID, strLastDateCreation, strSearchConditions, strFormNo);
                PopulateInvalidPANsCorr(BasicInfoID,  strSearchConditions);
                //-- 2015/07/20
                PopulateLateDeductionsCorr(BasicInfoID, strSearchConditions);
                //
                ClearSearchControls();
                //--
                if ((lngShortDeductionsNos + lngLatePaymentsNos) > 0)
                    TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                else if (lngLateDeductions > 0)
                {
                    if (lblInterestPayableLD.Text == "0.00")
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                    else
                        TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.HAS_DEFAULT_STATUS);
                }
                else
                    TdsMan.T_Insert_Update_PredictDefaultsAccess_Datetime(dmlService.J_pCommand, lngBasicInfoID, true, T_PREDICT_DEFAULTS_STATUS.NO_DEFAULT_STATUS);
                //--
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region PopulateLatePayments
//        public void PopulateLatePayments(long BasicInfoID, string TableName, string FieldId, string FieldPAN, string FieldName, string Argumnets, string SearchConditions, string FormNo)
//        {       
//            //--
//            DataSet dsetLatePayments = new DataSet();
//            double dblInterest = 0;
//            string strInterest = "";
//            try
//            {
//                if (FormNo == T_FormNo.F27EQ)
//                {
//                    dblInterest = 0.01;
//                    strInterest = "1%";
//                }
//                else
//                {
//                    dblInterest = 0.015;
//                    strInterest = "1.5%";
//                }
//                //--
//                #region COMMENT
//                //                strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
////                                   TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +   
////                                   TableName + "." + FieldPAN + ", " +
////                                   TableName + "." + FieldName + ", " + 
////                                  @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
////                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
////                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
////                                   Argumnets + "              AS LAST_DATE, " +
////                                   "TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
////                                   //" 1 + " +
////                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
////                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
////                                   //"     DAY(" + Argumnets + "),1,0) AS  TOTAL_DELAY, " +
////                                   //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
////                                   //" 1+ " +
////                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
////                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
////                                   //"     DAY(" + Argumnets + "),1,0)) AS  LATE_FEE " +
////                                   //-- 2014-05-30
////                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
////                                   //-- 2014-06-17
////                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
////                                   //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
////                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM')))) AS LATE_FEE " +
////                                   //-- 2014-06-17
////                                   "TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
////                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
////                         @"FROM    TRN_DEDUCTEE_DETAILS,                         
////                                   TRN_CHALLAN, 
////                                   TRN_BASIC_INFO," + 
////                                   TableName + " " +
////                         @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
////                         @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
////                          AND      TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
////                          AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
////                          AND      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
////                          AND      TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
//                //                         @"AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
//                #endregion
//                //--
//                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
//                {
//                    strSQL = @"SELECT TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
//                                      TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
//                                      TableName + "." + FieldPAN + ", " +
//                                      TableName + "." + FieldName + ", " +
//                                   @" TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
//                                      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 
//                                      CONVERT(CHAR(10)," + Argumnets + ",103)        AS LAST_DATE, " +
//                                   @" TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
//                        //-- 2014-05-30
//                        //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
//                        //-- 2014-06-17
//                                   " 1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103)) AS TOTAL_DELAY, " +
//                        //-- 2014-06-17
//                                   " TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
//                                   " 1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103))) AS LATE_FEE " +
//                         @"FROM      TRN_DEDUCTEE_DETAILS,                         
//                                     TRN_CHALLAN, 
//                                     TRN_BASIC_INFO," +
//                                     TableName + " " +
//                         @"WHERE     TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
//                         @"AND       TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
//                           AND       TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
//                           AND       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
//                           AND       CONVERT(CHAR(8),TRN_CHALLAN.DEPOSIT_DATE,112)  > CONVERT(CHAR(8)," + Argumnets + ",112) " +
//                         @"AND       TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
//                }
//                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
//                {
//                    strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
//                                                       TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
//                                       TableName + "." + FieldPAN + ", " +
//                                       TableName + "." + FieldName + ", " +
//                                      @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
//                                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
//                                       Argumnets + "             AS LAST_DATE, " +
//                                       "TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
//                        //-- 2014-05-30
//                        //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
//                        //-- 2014-06-17
//                                       "1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
//                        //-- 2014-06-17
//                                       "TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
//                                       "1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
//                             @"FROM    TRN_DEDUCTEE_DETAILS,                         
//                                                       TRN_CHALLAN, 
//                                                       TRN_BASIC_INFO," +
//                                       TableName + " " +
//                             @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
//                             @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
//                                              AND      TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
//                                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
//                                              AND      TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
//                             @"AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";

//                }
//                //
//                strOrderBy = " ORDER  BY TRN_CHALLAN.SL_NO, TRN_DEDUCTEE_DETAILS.SL_NO ";
//                //--
//                string[,] strMatrixViewLatePayments = {{"Challan Sl",   "60", "", "", "", "", ""},
//                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
//                                                      {"PAN",          "100", "", "", "", "", ""},
//                                                      {"Deductee Name","200", "S", "", "", "", "T"},
//                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
//                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
//                                                      {"Deduction Date","70", "d", "", "", "", ""},
//                                                      {"Due Date","70", "d", "", "", "", ""},
//                                                      {"Deposit Date","70", "d", "", "", "", ""},
//                                                      {"Delay in Month","80", "", "R", "", "", ""},
//                                                      {"Interest @" + strInterest,"60", "0.00", "R", "", "", ""}};
//                if (dsetLatePayments != null) dsetLatePayments.Clear();
//                dsetLatePayments = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLatePayments, strSQL + SearchConditions + strOrderBy, strMatrixViewLatePayments);
//                //--
//                double TotalInterest = 0; string InterestAmountReported = "";
//                for (int i = 0; i <= dgvLatePayments.RowCount - 1; i++)
//                {
//                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLatePayments.Rows[i].Cells[10].Value);
//                }
//                //
//                TotalInterestLP = TotalInterest;
//                //
//                lblTotalInterest.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0.00" : Convert.ToString(TotalInterest))); 
//                //
//                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM TRN_CHALLAN, TRN_BASIC_INFO WHERE TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfoID + " ";
//                lblInterestAmountReported.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
//                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
//                ////
//                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
//                //    lblInterestPayable.Text = "0.00";
//                //else
//                //    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
//                ////--
//                //lblTotalLabelLatePayments.Text = "Total Interest :";
//            }
//            catch (Exception err)
//            {
//                cmnService.J_UserMessage(err.Message);
//            }
//        }
        #endregion

        #region PopulateLatePayments
        public void PopulateLatePayments(long BasicInfoID, string TableName, string FieldId, string FieldPAN, string FieldName, string Argumnets, string SearchConditions, string FormNo)
        {
            //--
            DataSet dsetLatePayments = new DataSet();
            double dblInterest = 0;
            string strInterest = "", strLatePaymentIntCalc="";
            try
            {
                if (FormNo == T_FormNo.F27EQ)
                {
                    dblInterest = 0.01;
                    strInterest = "1%";
                }
                else
                {
                    dblInterest = 0.015;
                    strInterest = "1.5%";
                }
                //--
                #region COMMENT
                //                strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                //                                   TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +   
                //                                   TableName + "." + FieldPAN + ", " +
                //                                   TableName + "." + FieldName + ", " + 
                //                                  @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                //                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                //                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                //                                   Argumnets + "              AS LAST_DATE, " +
                //                                   "TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                //                                   //" 1 + " +
                //                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
                //                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
                //                                   //"     DAY(" + Argumnets + "),1,0) AS  TOTAL_DELAY, " +
                //                                   //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                //                                   //" 1+ " +
                //                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
                //                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
                //                                   //"     DAY(" + Argumnets + "),1,0)) AS  LATE_FEE " +
                //                                   //-- 2014-05-30
                //                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
                //                                   //-- 2014-06-17
                //                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                //                                   //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                //                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM')))) AS LATE_FEE " +
                //                                   //-- 2014-06-17
                //                                   "TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                //                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                //                         @"FROM    TRN_DEDUCTEE_DETAILS,                         
                //                                   TRN_CHALLAN, 
                //                                   TRN_BASIC_INFO," + 
                //                                   TableName + " " +
                //                         @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                //                         @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                //                          AND      TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                //                          AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                //                          AND      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
                //                          AND      TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
                //                         @"AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
                #endregion
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                {
                    //-- COVID
                    //strLatePaymentIntCalc = @" CASE WHEN (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '07/07/2020')
                    //                                OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 4 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '07/07/2020')
                    //                                OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 5 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '07/07/2020') THEN " + dblInterest / 2 + " ELSE " + dblInterest + " END ";
                    strLatePaymentIntCalc = @" CASE WHEN (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '06/30/2020')
                                                    OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 4 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '06/30/2020')
                                                    OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 5 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= '06/30/2020') THEN " + dblInterest / 2 + " ELSE " + dblInterest + " END ";
                    //--
                    strSQL = @"SELECT TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                      TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
                                      TableName + "." + FieldPAN + ", " +
                                      TableName + "." + FieldName + ", " +
                                   @" TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 
                                      CONVERT(CHAR(10)," + Argumnets + ",103)        AS LAST_DATE, " +
                                   @" TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                        //-- 2014-05-30
                        //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
                        //-- 2014-06-17
                        //" 1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103)) AS TOTAL_DELAY, " +
                        //-- 2018/05/11
                                    "case when TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) THEN " +
                                    " DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103))  " +
                                    " else  1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103)) " +
                                    " end AS TOTAL_DELAY, " +
                                  //-- 2014-06-17
                                  //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
                                  //" 1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103))) AS LATE_FEE " +
                                  //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( case when TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) " +
                                  //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + strLatePaymentIntCalc + " * ( case when TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) " +
                                  " (ROUND(CAST(TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS INTEGER)/100, 0) * 100) * " + strLatePaymentIntCalc + " * ( case when TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) " +
                                  "                                         THEN (DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103), CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103)))  " +
                                  "                                         else 1 +  DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103), CONVERT(DATETIME, TRN_CHALLAN.DEPOSIT_DATE, 103)) end) AS LATE_FEE " +
                         @"FROM      TRN_DEDUCTEE_DETAILS,                         
                                     TRN_CHALLAN, 
                                     TRN_BASIC_INFO," +
                                     TableName + " " +
                         @"WHERE     TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                         @"AND       TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                           AND       TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                           AND       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                           AND       CONVERT(CHAR(8),TRN_CHALLAN.DEPOSIT_DATE,112) > CONVERT(CHAR(8)," + Argumnets + ",112) " +
                         @"AND       TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //-- COVID
                    strLatePaymentIntCalc = @"(IIF((MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)
                                                OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 4 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)
                                                OR (MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 5 AND YEAR(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)," + dblInterest / 2 + "," + dblInterest + "))";
                    //--
                    strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                                       TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
                                       TableName + "." + FieldPAN + ", " +
                                       TableName + "." + FieldName + ", " +
                                      @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                       Argumnets + "             AS LAST_DATE, " +
                                       "TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                        //-- 2014-05-30
                        //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
                        //-- 2014-06-17
                        //"1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                        //-- 2018-05-11
                                       "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DateSerial(Year(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE), Month(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) + 1, 1) - 1, DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "), 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS TOTAL_DELAY, " +
                                       //-- 2014-06-17
                                       //-- 2018-05-11
                                       //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * (" +
                                       //-- 2020/04/08
                                       //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + strLatePaymentIntCalc + " * (" +
                                       //-- 2022/08/23
                                       "(INT(TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT / 100) * 100) * " + strLatePaymentIntCalc + " * (" +
                                       "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DateSerial(Year(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE), Month(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) + 1, 1) - 1, DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "), 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "))) AS LATE_FEE " +
                             @"FROM    TRN_DEDUCTEE_DETAILS,                         
                                                       TRN_CHALLAN, 
                                                       TRN_BASIC_INFO," +
                                       TableName + " " +
                             @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                             @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                                              AND      TRN_CHALLAN.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                                              AND      TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
                             @"AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";

                }
                //
                strOrderBy = " ORDER  BY TRN_CHALLAN.SL_NO, TRN_DEDUCTEE_DETAILS.SL_NO ";
                //--
                string[,] strMatrixViewLatePayments = {{"Challan Sl",   "60", "", "", "", "", ""},
                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
                                                      {"PAN",          "100", "", "", "", "", ""},
                                                      {"Deductee Name","200", "S", "", "", "", "T"},
                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
                                                      {"Deduction Date","70", "d", "", "", "", ""},
                                                      {"Due Date","70", "d", "", "", "", ""},
                                                      {"Deposit Date","70", "d", "", "", "", ""},
                                                      {"Delay in Month","80", "", "R", "", "", ""},
                                                      {"Interest @" + strInterest,"60", "0.00", "R", "", "", ""}};
                if (dsetLatePayments != null) dsetLatePayments.Clear();
                dsetLatePayments = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLatePayments, strSQL + SearchConditions + strOrderBy, strMatrixViewLatePayments);
                //--
                double TotalInterest = 0; string InterestAmountReported = "";
                for (int i = 0; i <= dgvLatePayments.RowCount - 1; i++)
                {
                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLatePayments.Rows[i].Cells[10].Value);
                }
                //
                TotalInterestLP = TotalInterest;
                //
                lblTotalInterest.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0.00" : Convert.ToString(TotalInterest)));
                //
                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM TRN_CHALLAN, TRN_BASIC_INFO WHERE TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfoID + " ";
                lblInterestAmountReported.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
                ////
                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                //    lblInterestPayable.Text = "0.00";
                //else
                //    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                ////--
                //lblTotalLabelLatePayments.Text = "Total Interest :";
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateShortDeductions
        public void PopulateShortDeductions(long BasicInfoID, string TableName, string FieldId, string FieldPAN,  string FieldName, string Argumnets, string SearchConditions)
        {
            //--
            DataSet dsetShortDeductions = new DataSet();
            try
            {
                if (cmbFormNo.Text != T_FormNo.F27Q)
                {
                    #region COMMENT
                    //                    strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                    //                                  TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                    //                                      TableName + "." + FieldPAN + ", " +
                    //                                      TableName + "." + FieldName + ", " +
                    //                                    @"MST_SECTION.SECTION_NO,
                    //                                  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                    //                                  TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                    //                                  TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                    //                                  IIF(" + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                    //                                                @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                    //                                                @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID', " +
                    //                                          @"MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
                    //                                           IIF(MID(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                    //                                                   @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
                    //                                               MST_NONSALARY_TAX_SLAB.COMPANY_RATE)
                    //                                      )          AS RATE," +
                    //                                      Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                    //                                      Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                    //                             @"FROM   TRN_DEDUCTEE_DETAILS," +
                    //                                       TableName + "," +
                    //                                     @"MST_NONSALARY_TAX_SLAB,
                    //                                  TRN_CHALLAN,
                    //                                  MST_SECTION
                    //                           WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID       = " + TableName + "." + FieldId + "  " +
                    //                             @"AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID        
                    //                           AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                    //                           AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                    //                           AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                    //                           AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                    //                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                    //                             @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                    //                             @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                    #endregion
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                    {
                        //iTDSRate = "0"; iTDStobeDeducted = "0"; iShortPayment = "0"; iV = "F";
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        {
                            strSQL = @"SELECT TRN_CHALLAN.SL_NO                                AS CHALLAN_SL_NO,
                                      TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                                              TableName + "." + FieldPAN + ", " +
                                              TableName + "." + FieldName + ", " +
                                            @"MST_SECTION.SECTION_NO,
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      '' AS RATE," +
                                      "''                                  AS ACTUAL_TAX_AMOUNT," +
                                      "''  AS SHORT_PAYMENT," +
                                        TableName + ".COMPLIANCE_HIGHER_RATE_FLAG  AS COMPLIANCE_HIGHER_RATE_FLAG," +
                                        "MST_VERIFIED_PAN.VERIFIED_STATUS AS VERIFIED_PAN_STATUS " +
                                     @"FROM  ((((TRN_DEDUCTEE_DETAILS 
                                   INNER JOIN " + TableName + @"
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + @") 
                                   INNER JOIN TRN_CHALLAN
                                        ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
                                   LEFT JOIN MST_VERIFIED_PAN
                                        ON  " + TableName + "." + FieldPAN + @" = MST_VERIFIED_PAN.PAN)
                                   INNER JOIN MST_SECTION
                                        ON TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID ) 
                               WHERE  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                               AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + @"
                               AND(" + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                               @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                               @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID' " +
                               @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)) ";
                            //@"AND    (TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                            //@"OR   MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)) ";
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                                      TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                                              TableName + "." + FieldPAN + " AS DEDUCTEE_PAN, " +
                                              TableName + "." + FieldName + ", " +
                                            @"MST_SECTION.SECTION_NO,
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      ''         AS RATE," +
                                    "''                                  AS ACTUAL_TAX_AMOUNT," +
                                    "''   AS SHORT_PAYMENT," +
                                              TableName + ".COMPLIANCE_HIGHER_RATE_FLAG  AS COMPLIANCE_HIGHER_RATE_FLAG," +
                                             "MST_VERIFIED_PAN.VERIFIED_STATUS  AS VERIFIED_PAN_STATUS " +
                                     @"FROM  ((((TRN_DEDUCTEE_DETAILS 
                                   INNER JOIN " + TableName + @"
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + @") 
                                   INNER JOIN TRN_CHALLAN
                                        ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID)
                                   LEFT JOIN MST_VERIFIED_PAN
                                        ON  " + TableName + "." + FieldPAN + @" = MST_VERIFIED_PAN.PAN) 
                                   INNER JOIN MST_SECTION
                                        ON TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID ) 
                                   WHERE  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                                   AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + @"
                                   AND(" + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                                   @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                                   @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID' " +
                                   @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2)) ";
                            //@"AND    (TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                            //@"OR   MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2))";
                        }
                        //--
                    }
                    else
                    {
                        //if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        //    strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                        //              TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                        //                      TableName + "." + FieldPAN + ", " +
                        //                      TableName + "." + FieldName + ", " +
                        //                    @"MST_SECTION.SECTION_NO,
                        //              TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                        //              TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                        //              TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                        //              CASE WHEN " + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                        //                                @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                        //                                @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID'  " +
                        //                          @"THEN MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE  
                        //                      ELSE  CASE
                        //                   WHEN " + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= '07/01/2021' THEN MST_NONSALARY_TAX_SLAB.COMPLIANCE_HIGHER_RATE
                        //                                                     ELSE 
                        //                       CASE WHEN SUBSTRING(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H' THEN " +
                        //                                   @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE
                        //                            ELSE
                        //                            MST_NONSALARY_TAX_SLAB.COMPANY_RATE END END END
                        //                            AS RATE," +
                        //                      Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                        //                      Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                        //             @"FROM   TRN_DEDUCTEE_DETAILS," +
                        //                       TableName + "," +
                        //                     @"MST_NONSALARY_TAX_SLAB,
                        //              TRN_CHALLAN,
                        //              MST_SECTION
                        //       WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID       = " + TableName + "." + FieldId + "  " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID        
                        //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                        //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                        //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                        //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                        //       AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                        //else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        //    strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                        //              TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                        //                      TableName + "." + FieldPAN + ", " +
                        //                      TableName + "." + FieldName + ", " +
                        //                    @"MST_SECTION.SECTION_NO,
                        //              TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                        //              TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                        //              TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                        //              IIF(" + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                        //                                @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                        //                                @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID', " +
                        //                          @"MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
                        //                      iif(" + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= " + cmnService.J_DateOperator() + "07/01/2021" + cmnService.J_DateOperator() + @", mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE,
                        //                       IIF(MID(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                        //                                   @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
                        //                           MST_NONSALARY_TAX_SLAB.COMPANY_RATE))
                        //                  )          AS RATE," +
                        //                      Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                        //                      Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                        //             @"FROM   TRN_DEDUCTEE_DETAILS," +
                        //                       TableName + "," +
                        //                     @"MST_NONSALARY_TAX_SLAB,
                        //              TRN_CHALLAN,
                        //              MST_SECTION
                        //       WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID       = " + TableName + "." + FieldId + "  " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID        
                        //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                        //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                        //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                        //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                        //       AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                        //             @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        {
                            #region COMMENT
                            //strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                            //              TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                            //                  TableName + "." + FieldPAN + ", " +
                            //                  TableName + "." + FieldName + ", " +
                            //                @"MST_SECTION.SECTION_NO,
                            //              TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                            //              TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                            //              TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                            //              CASE WHEN " + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                            //                            @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                            //                            @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID'  " +
                            //                      @"THEN MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE  
                            //                      ELSE  CASE
                            //                   WHEN " + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= '07/01/2021' THEN MST_NONSALARY_TAX_SLAB.COMPLIANCE_HIGHER_RATE
                            //                                                     ELSE 
                            //                       CASE WHEN SUBSTRING(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H' THEN " +
                            //                               @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE
                            //                            ELSE
                            //                            MST_NONSALARY_TAX_SLAB.COMPANY_RATE END END END
                            //                            AS RATE," +
                            //                  Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                            //                  Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT," +
                            //                  TableName + ".COMPLIANCE_HIGHER_RATE_FLAG  AS COMPLIANCE_HIGHER_RATE_FLAG " +
                            //         @"FROM   TRN_DEDUCTEE_DETAILS," +
                            //                   TableName + "," +
                            //                 @"MST_NONSALARY_TAX_SLAB,
                            //              TRN_CHALLAN,
                            //              MST_SECTION
                            //       WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID       = " + TableName + "." + FieldId + "  " +
                            //         @"AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID        
                            //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                            //       AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                            //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                            //       AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                            //       AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                            //         @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                            //         @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                            #endregion
                            strSQL = @"SELECT TRN_CHALLAN.SL_NO                                AS CHALLAN_SL_NO,
                                      TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                                              TableName + "." + FieldPAN + ", " +
                                              TableName + "." + FieldName + ", " +
                                            @"MST_SECTION.SECTION_NO,
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      CASE WHEN " + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                                                @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                                                @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID'  " +
                                                @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2) " +
                                            @"THEN MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE  
                                        ELSE  CASE
			                                WHEN " + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= '07/01/2021' THEN MST_NONSALARY_TAX_SLAB.COMPLIANCE_HIGHER_RATE
                                                                        ELSE 
                                        CASE WHEN SUBSTRING(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR SUBSTRING(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H' THEN " +
                                                    @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE
                                            ELSE
                                            MST_NONSALARY_TAX_SLAB.COMPANY_RATE END END END
                                            AS RATE," +
                                        Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                                        Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT," +
                                        TableName + ".COMPLIANCE_HIGHER_RATE_FLAG  AS COMPLIANCE_HIGHER_RATE_FLAG," +
                                        "MST_VERIFIED_PAN.VERIFIED_STATUS AS VERIFIED_PAN_STATUS " +
                                     @"FROM  (((((TRN_DEDUCTEE_DETAILS 
                                   INNER JOIN " + TableName + @"
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + @") 
                                   INNER JOIN TRN_CHALLAN
                                        ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID)
                                   INNER JOIN MST_NONSALARY_TAX_SLAB
                                        ON  TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID)  
                                   INNER JOIN MST_SECTION
                                        ON TRN_DEDUCTEE_DETAILS.SECTION_ID  = MST_SECTION.SECTION_ID ) 
                                   LEFT JOIN MST_VERIFIED_PAN
                                        ON  " + TableName + "." + FieldPAN + @" = MST_VERIFIED_PAN.PAN)
                               WHERE  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                               AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                               AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                             @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                             @"AND   (MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2) OR  TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20))";
                        }
                        else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        {
                            strSQL = @"SELECT TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                                      TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO," +
                                              TableName + "." + FieldPAN + ", " +
                                              TableName + "." + FieldName + ", " +
                                            @"MST_SECTION.SECTION_NO,
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      IIF(" + TableName + "." + FieldPAN + " = 'PANNOTAVBL' " +
                                                        @"OR " + TableName + "." + FieldPAN + " = 'PANAPPLIED' " +
                                                        @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID' " +
                                                        @"OR MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2), " +
                                                  @"MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
                                              iif(" + strTableName + @".COMPLIANCE_HIGHER_RATE_FLAG = 1 AND TRN_DEDUCTEE_DETAILS.PAYMENT_DATE >= " + cmnService.J_DateOperator() + "07/01/2021" + cmnService.J_DateOperator() + @", mst_nonsalary_tax_slab.COMPLIANCE_HIGHER_RATE,
                                               IIF(MID(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                                                           @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
                                                   MST_NONSALARY_TAX_SLAB.COMPANY_RATE))
                                          )          AS RATE," +
                                              Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                                              Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT," +
                                              TableName + ".COMPLIANCE_HIGHER_RATE_FLAG  AS COMPLIANCE_HIGHER_RATE_FLAG," +
                                             "MST_VERIFIED_PAN.VERIFIED_STATUS  AS VERIFIED_PAN_STATUS " +
                                     @"FROM  (((((TRN_DEDUCTEE_DETAILS 
                                   INNER JOIN " + TableName + @"
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + @") 
                                   INNER JOIN TRN_CHALLAN
                                        ON  TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID)
                                   INNER JOIN MST_NONSALARY_TAX_SLAB
                                        ON  TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID)  
                                   INNER JOIN MST_SECTION
                                        ON TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID ) 
                                   LEFT JOIN MST_VERIFIED_PAN
                                        ON  " + TableName + "." + FieldPAN + @" = MST_VERIFIED_PAN.PAN)
                                   WHERE  TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                                   AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                                   AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                                     @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                                     @"AND   (MST_VERIFIED_PAN.VERIFIED_STATUS IN(1, 2) OR TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20))";
                        }
                    }
                    //--
                    strOrderBy = " ORDER  BY TRN_CHALLAN.SL_NO, TRN_DEDUCTEE_DETAILS.SL_NO ";
                    //--
                    string[,] strMatrixViewShortDeductions = {{"Challan Sl.",   "60", "", "", "", "", ""},
                                                  {"Deductee Sl",  "60", "", "", "", "", ""},
                                                  {"PAN",          "90", "", "", "", "", ""},
                                                  {"Deductee Name","230", "S", "", "", "", "T"},
                                                  {"Section",      "45", "", "", "", "", ""},
                                                  {"Payment Date", "70", "d", "", "", "", ""},
                                                  {"Amount",     "90", "0.00", "R", "", "", ""},
                                                  {"TDS Deposited","80", "0.00", "R", "", "", ""},
                                                  {"TDS Rate",     "70", "0.0000", "R", "", "", ""},
                                                  {"TDS To Be Deducted","80", "0.00", "R", "", "", ""},
                                                  {"Short Payment","70", "0.00", "R", "", "", ""}};
                    if (dsetShortDeductions != null) dsetShortDeductions.Clear();
                    dsetShortDeductions = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvShortDeductions, strSQL + SearchConditions + strOrderBy, strMatrixViewShortDeductions);
                    //--
                    double TotalShortPayments = 0;
                    for (int i = 0; i <= dgvShortDeductions.RowCount - 1; i++)
                    {
                        //TotalShortPayments += cmnService.J_ReturnInt64Value(dgvShortDeductions.Rows[i].Cells[10].Value);
                        TotalShortPayments += cmnService.J_ReturnDoubleValue(dgvShortDeductions.Rows[i].Cells[10].Value);
                    }
                    lblTotalShortPayments.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalShortPayments) == "" ? "0" : Convert.ToString(TotalShortPayments)));
                    //--
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateShortDeductionsSD
        public void PopulateShortDeductionsSD( long BasicInfoID, string SearchConditions)
        {
            //--
            DataSet dsetShortDeductionsSD = new DataSet();
            try
            {
                strSQL = @"SELECT SL_NO,
                                  EMPLOYEE_PAN,
                                  EMPLOYEE_NAME,
                                  CATEGORY,
                                  CURRENT_SALARY,
                                  TAXABLE_INCOME,
                                  TAX_TO_PAY,
                                  RELIEF_AMOUNT,
                                  TDS_ENTERED,
                                  SHORT_DEDUCTION_AMOUNT
                           FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_SD + @"
                           WHERE  SHORT_DEDUCTION_AMOUNT > 0";
                //--
                strOrderBy = " ORDER BY SL_NO";
                //--
                string[,] strMatrixViewShortDeductionsSD = {{"Sl No.",   "60", "", "", "", "", ""},
                                                          {"PAN",          "90", "", "", "", "", ""},
                                                          {"Employee Name","230", "S", "", "", "", "T"},
                                                          {"Category",      "45", "", "", "", "", ""},
                                                          {"Current Employer's Salary",     "90", "0.00", "R", "", "", ""},
                                                          {"Taxable Income as per Salary","80", "0.00", "R", "", "", ""},
                                                          {"Total Tax as per Salary",     "70", "0.00", "R", "", "", ""},
                                                          {"Relief u/s 89","80", "0.00", "R", "", "", ""},
                                                          {"Current Employer's TDS","80", "0.00", "R", "", "", ""},
                                                          {"Short Payment","70", "0.00", "R", "", "", ""}};
                if (dsetShortDeductionsSD != null) dsetShortDeductionsSD.Clear();
                dsetShortDeductionsSD = dmlService.J_ShowDataInGrid(ref dgvShortDeductionsSD, strSQL + SearchConditions + strOrderBy, strMatrixViewShortDeductionsSD);
                //--
                //MessageBox.Show(dgvShortDeductionsSD.RowCount.ToString() + " " +  strSQL + SearchConditions + strOrderBy );
                //
                //--
                double TotalShortPaymentsSD = 0;
                for (int i = 0; i <= dgvShortDeductionsSD.RowCount - 1; i++)
                {
                    //TotalShortPayments += cmnService.J_ReturnInt64Value(dgvShortDeductions.Rows[i].Cells[10].Value);
                    TotalShortPaymentsSD += cmnService.J_ReturnDoubleValue(dgvShortDeductionsSD.Rows[i].Cells[9].Value);
                }
                lblTotalShortPaymentsSD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalShortPaymentsSD) == "" ? "0" : Convert.ToString(TotalShortPaymentsSD)));
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateShortDeductionsCorrSD
        public void PopulateShortDeductionsCorrSD(long BasicInfoID, string SearchConditions)
        {
            //--
            DataSet dsetShortDeductionsSD = new DataSet();
            try
            {
                strSQL = @"SELECT SL_NO,
                                  EMPLOYEE_PAN,
                                  EMPLOYEE_NAME,
                                  CATEGORY,
                                  CURRENT_SALARY,
                                  TAXABLE_INCOME,
                                  TAX_TO_PAY,
                                  RELIEF_AMOUNT,
                                  TDS_ENTERED,
                                  SHORT_DEDUCTION_AMOUNT
                           FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_PREDICT_SHORT_DED_CORR_SD + @"
                           WHERE  SHORT_DEDUCTION_AMOUNT > 0";
                //--
                strOrderBy = " ORDER BY SL_NO";
                //--
                string[,] strMatrixViewShortDeductionsSD = {{"Sl No.",   "60", "", "", "", "", ""},
                                                          {"PAN",          "90", "", "", "", "", ""},
                                                          {"Employee Name","230", "S", "", "", "", "T"},
                                                          {"Category",      "45", "", "", "", "", ""},
                                                          {"Current Employer's Salary",     "90", "0.00", "R", "", "", ""},
                                                          {"Taxable Income as per Salary","80", "0.00", "R", "", "", ""},
                                                          {"Total Tax as per Salary",     "70", "0.00", "R", "", "", ""},
                                                          {"Relief u/s 89","80", "0.00", "R", "", "", ""},
                                                          {"Current Employer's TDS","80", "0.00", "R", "", "", ""},
                                                          {"Short Payment","70", "0.00", "R", "", "", ""}};
                if (dsetShortDeductionsSD != null) dsetShortDeductionsSD.Clear();
                dsetShortDeductionsSD = dmlService.J_ShowDataInGrid(ref dgvShortDeductionsSD, strSQL + SearchConditions + strOrderBy, strMatrixViewShortDeductionsSD);
                //--
                //MessageBox.Show(dgvShortDeductionsSD.RowCount.ToString() + " " +  strSQL + SearchConditions + strOrderBy );
                //
                //--
                double TotalShortPaymentsSD = 0;
                for (int i = 0; i <= dgvShortDeductionsSD.RowCount - 1; i++)
                {
                    //TotalShortPayments += cmnService.J_ReturnInt64Value(dgvShortDeductions.Rows[i].Cells[10].Value);
                    TotalShortPaymentsSD += cmnService.J_ReturnDoubleValue(dgvShortDeductionsSD.Rows[i].Cells[9].Value);
                }
                lblTotalShortPaymentsSD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalShortPaymentsSD) == "" ? "0" : Convert.ToString(TotalShortPaymentsSD)));
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateInvalidPANs
        public void PopulateInvalidPANs(long BasicInfoID, string TableName, string FieldId, string FieldPAN, string FieldName, string SearchConditions)
        {
            //--
            DataSet dsetInvalidPANs = new DataSet();
            try
            {
                string strMidSubString = "";
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strMidSubString = "SUBSTRING";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strMidSubString = "MID";
                //--                        
                // Get the details of Grid
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @" SELECT    TRN_CHALLAN.SL_NO,
                                          TRN_DEDUCTEE_DETAILS.SL_NO," +
                                          TableName + "." + FieldPAN + ", " +
                                          TableName + "." + FieldName + " " +
                               @"FROM     TRN_BASIC_INFO,
                                          TRN_CHALLAN,
                                          TRN_DEDUCTEE_DETAILS, " +
                                          TableName + " " +
                               @"WHERE    TRN_BASIC_INFO.BASIC_INFO_ID = TRN_CHALLAN.BASIC_INFO_ID
                                AND       TRN_CHALLAN.CHALLAN_ID        = TRN_DEDUCTEE_DETAILS.CHALLAN_ID 
                                AND       TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + "  " +
                               @"AND      CASE WHEN " + TableName + "." + FieldPAN + " <> 'PANNOTAVBL'  " +
                                                @"AND             " + TableName + "." + FieldPAN + " <> 'PANAPPLIED'  " +
                                                @"AND             " + TableName + "." + FieldPAN + " <> 'PANINVALID'  " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'C' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'P' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'H' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'F' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'A' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'T' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'B' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'L' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'J' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'G' " +
                                                @" THEN 'INVALID'
                                                 ELSE 'VALID' END ='INVALID'  
                                AND       TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @" SELECT    TRN_CHALLAN.SL_NO,
                                          TRN_DEDUCTEE_DETAILS.SL_NO," +
                                          TableName + "." + FieldPAN + ", " +
                                          TableName + "." + FieldName + " " +
                               @"FROM     TRN_BASIC_INFO,
                                          TRN_CHALLAN,
                                          TRN_DEDUCTEE_DETAILS, " +
                                          TableName + " " +
                               @"WHERE    TRN_BASIC_INFO.BASIC_INFO_ID = TRN_CHALLAN.BASIC_INFO_ID
                                AND       TRN_CHALLAN.CHALLAN_ID        = TRN_DEDUCTEE_DETAILS.CHALLAN_ID 
                                AND       TRN_DEDUCTEE_DETAILS.PARTY_ID = " + TableName + "." + FieldId + "  " +
                               @"AND      IIF(" + TableName + "." + FieldPAN + " <> 'PANNOTAVBL'  " +
                                                @"AND             " + TableName + "." + FieldPAN + " <> 'PANAPPLIED'  " +
                                                @"AND             " + TableName + "." + FieldPAN + " <> 'PANINVALID'  " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'C' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'P' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'H' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'F' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'A' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'T' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'B' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'L' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'J' " +
                                                @"AND             " + strMidSubString + "(" + TableName + "." + FieldPAN + ", 4, 1) <> 'G' " +
                                                @",'INVALID'
                                                 ,'VALID') ='INVALID'  
                                AND       TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " ";

                strOrderBy = " ORDER  BY " + TableName + "." + FieldName + " ";
                //--
                string[,] strMatrixViewInvalidPANs = {{"Challan Srl.No.","100", "", "", "", "", ""},
                                              {"Deductee Srl.No","100", "", "", "", "", ""},
                                              {"PAN","200", "", "", "", "", ""},
                                              {"Deductee Name","500", "", "", "", "", ""}};

                if (dsetInvalidPANs != null) dsetInvalidPANs.Clear();
                dsetInvalidPANs = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvInvalidPAN, strSQL + SearchConditions + strOrderBy, strMatrixViewInvalidPANs);
                //-----
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateLateDeductions
        public void PopulateLateDeductions(long BasicInfoID, string TableName, string FieldId, string FieldPAN, string FieldName, string SearchConditions)
        {
            //--
            DataSet dsetLateDeductions = new DataSet();
            try
            {
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                       TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
                                       TableName + "." + FieldPAN + ", " +
                                       TableName + "." + FieldName + ", " +
                                      @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS DED_DUE_DATE, 
                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                       //"1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  AS TOTAL_DELAY, " +
                                       "CASE WHEN TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DATEADD(D,-1,DATEADD(MM, DATEDIFF(M,0,TRN_DEDUCTEE_DETAILS.PAYMENT_DATE)+1,0)) THEN " +
                                        " DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  " +
                                        " ELSE  1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)) " +
                                        " END AS TOTAL_DELAY, " +
                                      //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.01 * ( " +
                                      "(ROUND(CAST(TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS INTEGER)/100, 0) * 100) * 0.01 * ( " + //-- 2022/08/23
                                      "CASE WHEN TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DATEADD(D,-1,DATEADD(MM, DATEDIFF(M,0,TRN_DEDUCTEE_DETAILS.PAYMENT_DATE)+1,0)) THEN " +
                                        " DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  " +
                                        " ELSE  1 + DATEDIFF(MONTH,CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)) " +
                                        " END)  AS LATE_FEE " +
                             @"FROM    TRN_DEDUCTEE_DETAILS,                       
                                       TRN_CHALLAN, 
                                       TRN_BASIC_INFO," +
                                       TableName + " " +
                             @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                             @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                              AND     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                              AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    #region comment
                    //strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                    //                   TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
                    //                   TableName + "." + FieldPAN + ", " +
                    //                   TableName + "." + FieldName + ", " +
                    //                  @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                    //                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                    //                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                    //                   TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS DED_DUE_DATE, 
                    //                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                    //                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                    //                   "TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.01 * ( " +
                    //                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                    //         @"FROM    TRN_DEDUCTEE_DETAILS,                       
                    //                   TRN_CHALLAN, 
                    //                   TRN_BASIC_INFO," +
                    //                   TableName + " " +
                    //         @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                    //         @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                    //          AND     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                    //          AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                    //          AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                    //          AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
                    #endregion
                    strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                       TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +
                                       TableName + "." + FieldPAN + ", " +
                                       TableName + "." + FieldName + ", " +
                                      @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                       TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS DED_DUE_DATE, 
                                       TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                       //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                                      " IIF(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DateSerial(Year(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE), Month(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE) + 1, 1) - 1 " +
                                      ", DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")"+
                                      ", 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS TOTAL_DELAY, " +
                                      //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.01 * ( " +
                                      "(INT(TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT / 100) * 100) * 0.01 * ( " + //-- 2022/08/23
                                       //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                                       " IIF(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DateSerial(Year(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE), Month(TRN_DEDUCTEE_DETAILS.PAYMENT_DATE) + 1, 1) - 1 " +
                                      ", DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")" +
                                      ", 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "))) AS LATE_FEE " +
                             @"FROM    TRN_DEDUCTEE_DETAILS,                       
                                       TRN_CHALLAN, 
                                       TRN_BASIC_INFO," +
                                       TableName + " " +
                             @"WHERE   TRN_DEDUCTEE_DETAILS.PARTY_ID   = " + TableName + "." + FieldId + "  " +
                             @"AND     TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                              AND     TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID       = TRN_BASIC_INFO.BASIC_INFO_ID 
                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                              AND      TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                              AND     TRN_BASIC_INFO.BASIC_INFO_ID    = " + BasicInfoID + " ";
                }
                strOrderBy = " ORDER  BY TRN_CHALLAN.SL_NO, TRN_DEDUCTEE_DETAILS.SL_NO ";
                //--
                string[,] strMatrixViewLateDeductions = {{"Challan Sl",   "60", "", "", "", "", ""},
                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
                                                      {"PAN",          "100", "", "", "", "", ""},
                                                      {"Deductee Name","200", "S", "", "", "", "T"},
                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
                                                      {"Payment Date","70", "d", "", "", "", ""},
                                                      {"Deduction Due Date","70", "d", "", "", "", ""},
                                                      {"Deducted Date","70", "d", "", "", "", ""},
                                                      {"Delay in Month","80", "", "R", "", "", ""},
                                                      {"Interest @1%","60", "0.00", "R", "", "", ""}};
                if (dsetLateDeductions != null) dsetLateDeductions.Clear();
                dsetLateDeductions = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLateDeductions, strSQL + SearchConditions + strOrderBy, strMatrixViewLateDeductions);
                //--
                double TotalInterest = 0; string InterestAmountReportedSD = "";
                for (int i = 0; i <= dgvLateDeductions.RowCount - 1; i++)
                {
                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLateDeductions.Rows[i].Cells[10].Value);
                }
                //
                TotalInterestLD = TotalInterest;
                //
                lblTotalInterestLD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0" : Convert.ToString(TotalInterest)));
                //
                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM TRN_CHALLAN, TRN_BASIC_INFO WHERE TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfoID + " ";
                lblInterestAmountReportedLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
                //
                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReportedLD.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text))
                //    lblInterestPayableLD.Text = "0.00";
                //else
                //    lblInterestPayableLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                ////--
                //lblTotalLabelLateDeductions.Text = "Total Interest :";
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateLatePaymentsCorr
//        public void PopulateLatePaymentsCorr(long BasicInfoID, string Argumnets, string SearchConditions, string FormNo)
//        {
//            //--
//            DataSet dsetLatePayments = new DataSet();
//            double dblInterest = 0;
//            string strInterest = "";
//            try
//            {
//                if (FormNo == T_FormNo.F27EQ)
//                {
//                    dblInterest = 0.01;
//                    strInterest = "1%";
//                }
//                else
//                {
//                    dblInterest = 0.015;
//                    strInterest = "1.5%";
//                }
//                //--
//                #region COMMENT
//                //                strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
////                                   COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
////                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
////                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
////                                   COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
////                                   COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
////                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
////                                   Argumnets + "              AS LAST_DATE, " +
////                                   "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
////                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
////                                   "COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
////                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
////                         @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
////                                   COR_TRN_CHALLAN
////                           WHERE   COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
////                           AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
////                           AND      COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
////                           AND     COR_TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
//                //                         @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
//                #endregion
//                //--
//                if(J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
//                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
//                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
//                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 
//                                       " + Argumnets + @"         AS LAST_DATE, " +
//                                       "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
//                                       "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
//                                       "COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
//                                       "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
//                             @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
//                                       COR_TRN_CHALLAN,                         
//                                       COR_HDR_BATCH
//                               WHERE   COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID 
//                               AND     COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
//                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
//                               AND     COR_TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
//                             @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";


//                else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
//                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
//                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
//                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
//                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
//                                       Argumnets + "              AS LAST_DATE, " +
//                                       "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
//                                       "1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103))  AS TOTAL_DELAY, " +
//                                       "COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
//                                       "1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103))) AS LATE_FEE " +
//                             @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
//                                       COR_TRN_CHALLAN,                         
//                                       COR_HDR_BATCH
//                               WHERE   COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID 
//                               AND     COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
//                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
//                               AND     CONVERT(CHAR(8),COR_TRN_CHALLAN.DEPOSIT_DATE,112)  > CONVERT(CHAR(8)," + Argumnets + ",112) " +
//                             @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
//                strOrderBy = " ORDER BY COR_TRN_CHALLAN.SL_NO, COR_TRN_DEDUCTEE_DETAILS.SL_NO ";
//                //--
//                string[,] strMatrixViewLatePayments = {{"Challan Sl",   "60", "", "", "", "", ""},
//                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
//                                                      {"PAN",          "100", "", "", "", "", ""},
//                                                      {"Deductee Name","200", "S", "", "", "", "T"},
//                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
//                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
//                                                      {"Deduction Date","70", "d", "", "", "", ""},
//                                                      {"Due Date","70", "d", "", "", "", ""},
//                                                      {"Deposit Date","70", "d", "", "", "", ""},
//                                                      {"Delay in Month","80", "", "R", "", "", ""},
//                                                      {"Interest @" + strInterest,"60", "0.00", "R", "", "", ""}};
//                if (dsetLatePayments != null) dsetLatePayments.Clear();
//                dsetLatePayments = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLatePayments, strSQL + SearchConditions + strOrderBy, strMatrixViewLatePayments);
//                //--
//                double TotalInterest = 0; string InterestAmountReported = "";
//                for (int i = 0; i <= dgvLatePayments.RowCount - 1; i++)
//                {
//                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLatePayments.Rows[i].Cells[10].Value);
//                }
//                //
//                TotalInterestLP = TotalInterest;
//                //
//                lblTotalInterest.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0" : Convert.ToString(TotalInterest)));
//                //
//                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM COR_TRN_CHALLAN  WHERE COR_TRN_CHALLAN.BATCH_HEADER_ID = " + BasicInfoID + " ";
//                lblInterestAmountReported.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
//                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
//                //
//                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
//                //    lblInterestPayable.Text = "0.00";
//                //else
//                //    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
//                ////--
//                //lblTotalLabelLatePayments.Text = "Total Interest :";
//            }
//            catch (Exception err)
//            {
//                cmnService.J_UserMessage(err.Message);
//            }
//        }
        #endregion

        #region PopulateLatePaymentsCorr
        public void PopulateLatePaymentsCorr(long BasicInfoID, string Argumnets, string SearchConditions, string FormNo)
        {
            //--
            DataSet dsetLatePayments = new DataSet();
            double dblInterest = 0;
            string strInterest = "", strLatePaymentIntCalc = ""; 
            try
            {
                if (FormNo == T_FormNo.F27EQ)
                {
                    dblInterest = 0.01;
                    strInterest = "1%";
                }
                else
                {
                    dblInterest = 0.015;
                    strInterest = "1.5%";
                }
                //--
                #region COMMENT
                //                strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                //                                   COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
                //                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                //                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
                //                                   COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                //                                   COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                //                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                //                                   Argumnets + "              AS LAST_DATE, " +
                //                                   "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                //                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                //                                   "COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                //                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                //                         @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
                //                                   COR_TRN_CHALLAN
                //                           WHERE   COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                //                           AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                //                           AND      COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT > 0 
                //                           AND     COR_TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
                //                         @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
                #endregion
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    //-- 2020/06/15 -- COVID
                    strLatePaymentIntCalc = @"(IIF((MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND COR_TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)
                                                    OR (MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 4 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND COR_TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)
                                                    OR (MONTH(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 5 AND YEAR(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 2020 AND COR_TRN_CHALLAN.DEPOSIT_DATE <= #30/06/2020#)," + dblInterest / 2 + "," + dblInterest + "))";

                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 
                                       " + Argumnets + @"         AS LAST_DATE, " +
                                       "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                                       //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                                       //-- 2018/05/11
                                       "iif(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DateSerial(Year(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE), Month(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) + 1, 1) - 1, DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "), 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS TOTAL_DELAY, " +
                                       //"COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
                                       //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                                       //-- 2018/05/11
                                       //"COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + strLatePaymentIntCalc + " * ( " +
                                       "(INT(COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT / 100) * 100) * " + strLatePaymentIntCalc + " * ( " +
                                       "iif(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DateSerial(Year(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE), Month(COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) + 1, 1) - 1, DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "), 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "))) AS LATE_FEE " +
                             @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
                                       COR_TRN_CHALLAN,                         
                                       COR_HDR_BATCH
                               WHERE   COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID 
                               AND     COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                               AND     COR_TRN_CHALLAN.DEPOSIT_DATE        > " + Argumnets + " " +
                             @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
                }
                else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                       Argumnets + "              AS LAST_DATE, " +
                                       "COR_TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                                       //"1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103))  AS TOTAL_DELAY, " +
                                       //-- 2018/05/11
                                       "case when COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) THEN " +
                                       " DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103)) " +
                                       " else 1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103)) " +
                                       " end AS TOTAL_DELAY, " +
                                      //"COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( " +
                                      //"1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103),CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103))) AS LATE_FEE " +
                                      //" COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * " + dblInterest + " * ( case when COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) " +
                                      " (ROUND(CAST(COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS INTEGER)/100, 0) * 100) * " + dblInterest + " * ( case when COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE = DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE)+1,0)) " +
                                      " THEN  (DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103), CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103))) " +
                                      " else 1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103), CONVERT(DATETIME, COR_TRN_CHALLAN.DEPOSIT_DATE, 103)) end) AS LATE_FEE " +
                             @"FROM    COR_TRN_DEDUCTEE_DETAILS,                         
                                       COR_TRN_CHALLAN,                         
                                       COR_HDR_BATCH
                               WHERE   COR_HDR_BATCH.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID 
                               AND     COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID 
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                               AND     CONVERT(CHAR(8),COR_TRN_CHALLAN.DEPOSIT_DATE,112)  > CONVERT(CHAR(8)," + Argumnets + ",112) " +
                             @"AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
                strOrderBy = " ORDER BY COR_TRN_CHALLAN.SL_NO, COR_TRN_DEDUCTEE_DETAILS.SL_NO ";
                //--
                string[,] strMatrixViewLatePayments = {{"Challan Sl",   "60", "", "", "", "", ""},
                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
                                                      {"PAN",          "100", "", "", "", "", ""},
                                                      {"Deductee Name","200", "", "", "", "", "T"},
                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
                                                      {"Deduction Date","70", "d", "", "", "", ""},
                                                      {"Due Date","70", "d", "", "", "", ""},
                                                      {"Deposit Date","70", "d", "", "", "", ""},
                                                      {"Delay in Month","80", "", "R", "", "", ""},
                                                      {"Interest @" + strInterest,"60", "0.00", "R", "", "", ""}};
                if (dsetLatePayments != null) dsetLatePayments.Clear();
                dsetLatePayments = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLatePayments, strSQL + SearchConditions + strOrderBy, strMatrixViewLatePayments);
                //--
                double TotalInterest = 0; string InterestAmountReported = "";
                for (int i = 0; i <= dgvLatePayments.RowCount - 1; i++)
                {
                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLatePayments.Rows[i].Cells[10].Value);
                }
                //
                TotalInterestLP = TotalInterest;
                //
                lblTotalInterest.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0" : Convert.ToString(TotalInterest)));
                //
                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM COR_TRN_CHALLAN  WHERE COR_TRN_CHALLAN.BATCH_HEADER_ID = " + BasicInfoID + " ";
                lblInterestAmountReported.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
                //
                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                //    lblInterestPayable.Text = "0.00";
                //else
                //    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                ////--
                //lblTotalLabelLatePayments.Text = "Total Interest :";
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateShortDeductionsCorr
        public void PopulateShortDeductionsCorr(long BasicInfoID, string Argumnets, string SearchConditions)
        {
            //--
            DataSet dsetShortDeductions = new DataSet();
            try
            {
                //---------------------------------------------------------
                //ADDED BY DHRUB ON 2015-10-06 TO BY PASS WHILE FORM NO 27Q
                //---------------------------------------------------------
                if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + BasicInfoID)) != T_FormNo.F27Q)
                {
                    #region COMMENT
                    //                    strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
//                                  COR_TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO,
//                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
//                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME, 
//                                  MST_SECTION.SECTION_NO,
//                                  COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
//                                  COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
//                                  COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
//                                  IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' 
//                                  OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANAPPLIED' 
//                                  OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANINVALID', 
//                                   MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
//                                           IIF(MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H', 
//                                               MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
//                                               MST_NONSALARY_TAX_SLAB.COMPANY_RATE)
//                                      )          AS RATE," +
//                                      Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
//                                      Argumnets + " - COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
//                             @"FROM   COR_TRN_DEDUCTEE_DETAILS,
//                                  MST_NONSALARY_TAX_SLAB,
//                                  COR_TRN_CHALLAN,
//                                  MST_SECTION
//                           WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID        
//                           AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
//                           AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
//                           AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
//                           AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
//                           AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
//                             @"AND    COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                    //                             @"AND    COR_TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                    #endregion
                    if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                        strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME, 
                                      MST_SECTION.SECTION_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' 
                                          OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANAPPLIED' 
                                          OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANINVALID', 
                                      MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
                                               IIF(MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H', 
                                                   MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
                                                   MST_NONSALARY_TAX_SLAB.COMPANY_RATE)
                                          )          AS RATE," +
                                          Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                                          Argumnets + " - COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                                 @"FROM   COR_TRN_DEDUCTEE_DETAILS,
                                      MST_NONSALARY_TAX_SLAB,
                                      COR_TRN_CHALLAN,
                                      MST_SECTION
                               WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID        
                               AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                               AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                               AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                               AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                               AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                                 @"AND    COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                                 @"AND    COR_TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                    else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO                                        AS CHALLAN_SL_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.SL_NO                               AS DEDUCTEE_SL_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME, 
                                      MST_SECTION.SECTION_NO,
                                      COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                      COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                      COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT, 
                                      CASE WHEN (COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANNOTAVBL' 
                                                 OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANAPPLIED' 
                                                 OR COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN = 'PANINVALID')
                                           THEN MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE
                                           ELSE CASE WHEN (SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'P' OR SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) = 'H') 
                                                     THEN MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE 
                                                     ELSE MST_NONSALARY_TAX_SLAB.COMPANY_RATE
                                                END 
                                      END                                                            AS RATE," +
                                          Argumnets + "                                                  AS ACTUAL_TAX_AMOUNT," +
                                          Argumnets + " - COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                                 @"FROM   COR_TRN_DEDUCTEE_DETAILS,
                                                          MST_NONSALARY_TAX_SLAB,
                                                          COR_TRN_CHALLAN,
                                                          MST_SECTION
                                                   WHERE  COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID        
                                                   AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                                                   AND    COR_TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                                                   AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                                                   AND    COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                                                   AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " " +
                                 @"AND    COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                                 @"AND    COR_TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                    //--
                    strOrderBy = " ORDER  BY COR_TRN_CHALLAN.SL_NO, COR_TRN_DEDUCTEE_DETAILS.SL_NO ";
                    //--
                    string[,] strMatrixViewShortDeductions = {{"Challan Sl.",   "60", "", "", "", "", ""},
                                                  {"Deductee Sl",  "60", "", "", "", "", ""},
                                                  {"PAN",          "90", "", "", "", "", ""},
                                                  {"Deductee Name","230", "S", "", "", "", "T"},
                                                  {"Section",      "45", "", "", "", "", ""},
                                                  {"Payment Date", "70", "d", "", "", "", ""},
                                                  {"Amount",     "90", "0.00", "R", "", "", ""},
                                                  {"TDS Deposited","80", "0.00", "R", "", "", ""},
                                                  {"TDS Rate",     "70", "0.0000", "R", "", "", ""},
                                                  {"TDS To Be Deducted","80", "0.00", "R", "", "", ""},
                                                  {"Short Payment","70", "0.00", "R", "", "", ""}};
                    if (dsetShortDeductions != null) dsetShortDeductions.Clear();
                    dsetShortDeductions = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvShortDeductions, strSQL + SearchConditions + strOrderBy, strMatrixViewShortDeductions);
                    //--
                    double TotalShortPayments = 0;
                    for (int i = 0; i <= dgvShortDeductions.RowCount - 1; i++)
                    {
                        TotalShortPayments += cmnService.J_ReturnDoubleValue(dgvShortDeductions.Rows[i].Cells[10].Value);
                    }
                    lblTotalShortPayments.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalShortPayments) == "" ? "0" : Convert.ToString(TotalShortPayments)));
                    //--
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateInvalidPANsCorr
        public void PopulateInvalidPANsCorr(long BasicInfoID, string SearchConditions)
        {
            //--
            DataSet dsetInvalidPANs = new DataSet();
            try
            {
                string strMidSubString = "";
                //--
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strMidSubString = "SUBSTRING";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strMidSubString = "MID";
                //--    
                // Get the details of Grid
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO,
                                  COR_TRN_DEDUCTEE_DETAILS.SL_NO,
                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME
                           FROM   COR_TRN_CHALLAN,
                                  COR_TRN_DEDUCTEE_DETAILS
                           WHERE  COR_TRN_CHALLAN.TRN_CHALLAN_ID        = COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID 
                           AND    CASE WHEN COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL'  
                                 AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANAPPLIED'  
                                 AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANINVALID'  
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'C' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'P' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'H' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'F' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'A' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'T' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'B' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'L' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'J' 
                                 AND SUBSTRING(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'G'
                                 THEN 'INVALID'
                                 ELSE 'VALID' END ='INVALID'  
                            AND  COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @"SELECT COR_TRN_CHALLAN.SL_NO,
                                  COR_TRN_DEDUCTEE_DETAILS.SL_NO,
                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN,
                                  COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME
                           FROM   COR_TRN_CHALLAN,
                                  COR_TRN_DEDUCTEE_DETAILS
                           WHERE  COR_TRN_CHALLAN.TRN_CHALLAN_ID        = COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID 
                           AND    IIF(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL'  
                                 AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANAPPLIED'  
                                 AND COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANINVALID'  
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'C' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'P' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'H' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'F' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'A' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'T' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'B' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'L' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'J' 
                                 AND MID(COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 4, 1) <> 'G'
                                 ,'INVALID'
                                 ,'VALID') ='INVALID'  
                            AND  COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoID + " ";

                strOrderBy = " ORDER  BY COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME ";
                //--
                string[,] strMatrixViewInvalidPANs = {{"Challan Srl.No.","100", "", "", "", "", ""},
                                              {"Deductee Srl.No","100", "", "", "", "", ""},
                                              {"PAN","200", "", "", "", "", ""},
                                              {"Deductee Name","500", "", "", "", "", ""}};

                if (dsetInvalidPANs != null) dsetInvalidPANs.Clear();
                dsetInvalidPANs = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvInvalidPAN, strSQL + SearchConditions + strOrderBy, strMatrixViewInvalidPANs);
                //-----
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateLateDeductionsCorr
        public void PopulateLateDeductionsCorr(long BasicInfoID,string SearchConditions)
        {
            //--
            DataSet dsetLateDeductions = new DataSet();
            try
            {
                if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO         AS CHALLAN_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS DED_DUE_DATE, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                      //"1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  AS TOTAL_DELAY, " +
                                      "CASE WHEN COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DATEADD(D,-1,DATEADD(MM, DATEDIFF(M,0,COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE)+1,0)) THEN " +
                                        " DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  " +
                                        " ELSE  1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)) " +
                                        " END AS TOTAL_DELAY, " +
                                      //"COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.01 * ( " +
                                      "(ROUND(CAST(COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT AS INTEGER) / 100, 0)  * 100) * 0.01 * ( " +
                                       //"1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)))  AS LATE_FEE " +
                                       "CASE WHEN COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DATEADD(D,-1,DATEADD(MM, DATEDIFF(M,0,COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE)+1,0)) THEN " +
                                        " DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103))  " +
                                        " ELSE  1 + DATEDIFF(MONTH,CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 103),CONVERT(DATETIME, COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 103)) " +
                                        " END)  AS LATE_FEE " +
                             @"FROM    COR_TRN_DEDUCTEE_DETAILS, 
                                       COR_TRN_CHALLAN
                               WHERE   COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                               AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";
                else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                    strSQL = @"SELECT  COR_TRN_CHALLAN.SL_NO         AS CHALLAN_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO,
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME,                                  
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, 
                                       COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE AS DED_DUE_DATE, 
                                       COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                        //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                                        " IIF(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DateSerial(Year(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE), Month(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE) + 1, 1) - 1 " +
                                       ", DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")" +
                                       ", 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS TOTAL_DELAY, " +
                                       //"COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.01 * ( " +
                                       "(INT(COR_TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT / 100) * 100)* 0.01 * ( " +
                                       //"1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
                                       " IIF(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE = DateSerial(Year(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE), Month(COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE) + 1, 1) - 1 " +
                                       ", DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")" +
                                       ", 1 + DATEDIFF(" + "\"M\"" + @"," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "))) AS LATE_FEE " +
                             @"FROM    COR_TRN_DEDUCTEE_DETAILS, 
                                       COR_TRN_CHALLAN
                               WHERE   COR_TRN_DEDUCTEE_DETAILS.TRN_CHALLAN_ID = COR_TRN_CHALLAN.TRN_CHALLAN_ID
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                               AND     COR_TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE  > COR_TRN_DEDUCTEE_DETAILS.PAYMENT_DATE
                               AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID    = " + BasicInfoID + " ";

                strOrderBy = " ORDER BY COR_TRN_CHALLAN.SL_NO, COR_TRN_DEDUCTEE_DETAILS.SL_NO ";
                //--
                string[,] strMatrixViewLateDeductions = {{"Challan Sl",   "60", "", "", "", "", ""},
                                                      {"Deductee Sl",  "60", "", "", "", "", ""},
                                                      {"PAN",          "100", "", "", "", "", ""},
                                                      {"Deductee Name","200", "S", "", "", "", "T"},
                                                      {"Amount",     "80", "0.00", "R", "", "", ""},
                                                      {"TDS Deposited","70", "0.00", "R", "", "", ""},
                                                      {"Payment Date","70", "d", "", "", "", ""},
                                                      {"Deduction Due Date","70", "d", "", "", "", ""},
                                                      {"Deducted Date","70", "d", "", "", "", ""},
                                                      {"Delay in Month","80", "", "R", "", "", ""},
                                                      {"Interest @1%","60", "0.00", "R", "", "", ""}};
                if (dsetLateDeductions != null) dsetLateDeductions.Clear();
                dsetLateDeductions = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLateDeductions, strSQL + SearchConditions + strOrderBy, strMatrixViewLateDeductions);
                //--
                double TotalInterest = 0; string InterestAmountReported = "";
                for (int i = 0; i <= dgvLateDeductions.RowCount - 1; i++)
                {
                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLateDeductions.Rows[i].Cells[10].Value);
                }
                //
                TotalInterestLD = TotalInterest;
                //
                lblTotalInterestLD.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0" : Convert.ToString(TotalInterest)));
                //
                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM COR_TRN_CHALLAN  WHERE COR_TRN_CHALLAN.BATCH_HEADER_ID = " + BasicInfoID + " ";
                lblInterestAmountReportedLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
                //
                //if (cmnService.J_ReturnDoubleValue(lblInterestAmountReportedLD.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterestLD.Text))
                //    lblInterestPayableLD.Text = "0.00";
                //else
                //    lblInterestPayableLD.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));
                ////--
                //lblTotalLabelLateDeductions.Text = "Total Interest :";
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PopulateLateFilingDetailsRegular
        public void PopulateLateFilingDetailsRegular(long BasicInfoID)
        {
            //--
            IDataReader drdShowRecord = null;
            try
            {
                //--
                //strSQL = "SELECT " + cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                //string strReturnLastDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //if (strReturnLastDate == "")
                //{
                    strSQL = "UPDATE TRN_BASIC_INFO SET RETURN_LAST_DATE ='" + TdsMan.T_ReturnFilingLastDate(cmbQuarter.Text, TdsMan.ReturnFinancialYear(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex))), TdsMan.GetDeductorType(Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex))), Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbFormNo.Text) + "' WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "SELECT " + cmnService.J_SQLDBFormat("RETURN_LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID;
                    string strReturnLastDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //}
                //--
                if (dtService.J_ConvertToIntYYYYMMDD(J_ReturnServerDate()) > dtService.J_ConvertToIntYYYYMMDD(strReturnLastDate))
                {
                    lblFilingDateLabel.Visible = true;
                    mskFilingDate.Visible = true;
                    btnViewLateFine.Visible = true;
                }
                else
                {
                    lblFilingDateLabel.Visible = false;
                    mskFilingDate.Visible = false;
                    btnViewLateFine.Visible = false;
                }
                //--
                strSQL = "SELECT TRN_BASIC_INFO.BASIC_INFO_ID     AS BASIC_INFO_ID," +
                    "            " + cmnService.J_SQLDBFormat("TRN_BASIC_INFO.DATE_OF_FILING", J_SQLColFormat.DateFormatDDMMYYYY) + " AS DATE_OF_FILING " +
                    "       FROM   TRN_BASIC_INFO " +
                    "       WHERE  BASIC_INFO_ID = " + BasicInfoID + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    mskFilingDate.ReadOnly = true;
                    return;
                }
                while (drdShowRecord.Read())
                {
                    //lngSearchId = Id;
                    //
                    mskFilingDate.Text = Convert.ToString(drdShowRecord["DATE_OF_FILING"]);
                }
                //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //
                if (dtService.J_IsBlankDateCheck(ref mskFilingDate, J_ShowMessage.NO) == true)
                    mskFilingDate.ReadOnly = false;
                else
                    mskFilingDate.ReadOnly = true;

                //
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0066", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }        
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long BasicInfoID)
        {
            if (BasicInfoID == 0)
            {

                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                return;
            }
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";

            // CONTROL SUMMARY VALUES
            txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(CHALLAN_ID) AS COUNT_CHALLAN_ID FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID));
            txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM TRN_DEDUCTEE_DETAILS WHERE BASIC_INFO_ID = " + BasicInfoID))));
            //
            //if (Convert.ToDouble(txtTotalChallanRecords.Text) > 0 && txtTotalChallanRecords.Visible == true)
            //{
            //    BtnCancel.Enabled = true;
            //    BtnCancel.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnCancel.Enabled = false;
            //    BtnCancel.BackColor = Color.LightGray;
            //}
        }
        #endregion

        #region ControlSummaryBasicInfoCorr
        private void ControlSummaryBasicInfoCorr(long BasicInfoID)
        {
            if (BasicInfoID == 0)
            {

                txtTotalChallanRecords.Text = "";
                txtTotalDeducteeRecords.Text = "";
                txtTotalChallanAmount.Text = "";
                txtTotalDeducteeTDS.Text = "";
                txtAmountPaid.Text = "";
                return;
            }
            txtTotalChallanRecords.Text = "0";
            txtTotalDeducteeRecords.Text = "0";
            txtTotalChallanAmount.Text = "0.00";
            txtTotalDeducteeTDS.Text = "0.00";
            txtAmountPaid.Text = "0.00";

            // CONTROL SUMMARY VALUES
            txtTotalChallanRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(TRN_CHALLAN_ID) AS COUNT_CHALLAN_ID FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID));
            txtTotalDeducteeRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(TRN_DEDUCTEE_DETAIL_ID) AS COUNT_DEDUCTEE_DETAIL_ID FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID));
            txtTotalChallanAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) AS SUM_TOT_TAX FROM COR_TRN_CHALLAN WHERE BATCH_HEADER_ID = " + BasicInfoID))));
            txtTotalDeducteeTDS.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOTAL_AMOUNT) AS SUM_TOTAL_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID))));
            txtAmountPaid.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID)) == "" ? "0" : Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM COR_TRN_DEDUCTEE_DETAILS WHERE BATCH_HEADER_ID = " + BasicInfoID))));
            //
            //if (Convert.ToDouble(txtTotalChallanRecords.Text) > 0 && txtTotalChallanRecords.Visible == true)
            //{
            //    BtnCancel.Enabled = true;
            //    BtnCancel.BackColor = Color.Lavender;
            //}
            //else
            //{
            //    BtnCancel.Enabled = false;
            //    BtnCancel.BackColor = Color.LightGray;
            //}
        }
        #endregion

        #region LoadBatch
        private void LoadBatch()
        {
            string[,] strMatrixBatchGrid = null; string strOrderBy = ""; string strQuery = ""; DataSet dsetGridClone = new DataSet();

            //-----------------------------------------------------------
            string[,] strMatrixBatch = {{"BATCH_HEADER_ID", "0", "", "Right", "", "F", ""},
                                        {"FA Year", "70", "S", "", "", "", ""},
                                        {"Form No", "75", "S", "", "", "", ""},
                                        {"Qtr", "35", "", "", "", "", ""},
                                        {"Company Name", "250", "", "", "", "", "T"},
                                        {"TAN No.", "90", "", "", "", "", ""},
                                        {"Imported date & Time", "140", "dd/MM/yyyy", "", "", "", ""},
                                        {"Total Corrections", "100", "", "Right", "", "", ""}};
            //-----------------------------------------------------------
            strMatrixBatchGrid = strMatrixBatch;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------

            string[,] strLoadCorrectionMatrix = {{"COR_TRN_COMPANY.MODE = '" + T_CorrectionMode.TANUpdation + "'", "F", "Cancelled", "T"},
                                                 {"COR_TRN_COMPANY.MODE <> '" + T_CorrectionMode.TANUpdation + "'", "F", "COR_HDR_BATCH.TOTAL_CORRECTION", "F"}};

            strOrderBy = "COR_HDR_BATCH.BATCH_HEADER_ID DESC";
            strQuery = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID  AS BATCH_HEADER_ID," +
                      "        MST_ASSESSMENT.FA_YEAR         AS FA_YEAR," +
                      "        COR_HDR_BATCH.FORM_NO          AS FORM_NO," +
                      "        COR_HDR_BATCH.QTR              AS QTR," +
                      "        COR_TRN_COMPANY.COMPANY_NAME   AS COMPANY_NAME," +
                      "        COR_TRN_COMPANY.TAN_NO         AS TAN_NO,";
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                strQuery = strQuery + " SUBSTRING(CONVERT(VARCHAR,COR_HDR_BATCH.IMPORTED_DATE, 120),1,LEN(CONVERT(VARCHAR,COR_HDR_BATCH.IMPORTED_DATE, 120)) - 6) + ' ' + RIGHT(CONVERT(VARCHAR,COR_HDR_BATCH.IMPORTED_DATE, 109),2) AS IMPORTED_DATE,";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strQuery = strQuery + " FORMAT(COR_HDR_BATCH.IMPORTED_DATE, \"dd/MM/yyyy h:m AMPM\")   AS IMPORTED_DATE,";
            strQuery = strQuery + "  " + cmnService.J_SQLDBFormat(strLoadCorrectionMatrix, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TOTAL_CORRECTION " +
                "FROM    COR_HDR_BATCH," +
                "        MST_ASSESSMENT," +
                "        COR_TRN_COMPANY " +
                "WHERE   COR_HDR_BATCH.ASST_ID            = MST_ASSESSMENT.ASST_ID " +
                "AND     COR_HDR_BATCH.BATCH_HEADER_ID    = COR_TRN_COMPANY.BATCH_HEADER_ID ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            //blnGridExit = true;
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewBatch, strSQL, strMatrixBatchGrid);       //Show Data into the Grid
            //blnGridExit = false;
        }
        #endregion

        #region CREATE_TEMP_TABLE
        private bool CREATE_TEMP_TABLE(IDbCommand Command, string TempTable)
        {
            try
            {
                //DMLService dmlServiceSD = new DMLService();
                dmlService.J_BeginTransaction();
                //
                if (dmlService.J_IsDatabaseObjectExist(TempTable) == true)
                {
                    strSQL = "DROP TABLE " + TempTable;
                    dmlService.J_ExecSql(Command, strSQL);
                }
                //--
                if (dmlService.J_IsDatabaseObjectExist(TempTable) == false)
                {
                    //strSQL = "CREATE TABLE " + TempTable + " (" +
                    //      "                  PREDICT_SHORT_DED_SD_ID   COUNTER," +
                    //      "                  BASIC_INFO_ID             NUMBER    DEFAULT 0," +
                    //      "                  ASST_ID                   NUMBER    DEFAULT 0," +
                    //      "                  SL_NO                     NUMBER    DEFAULT 0," +
                    //      "                  EMPLOYEE_ID               NUMBER    DEFAULT 0," +
                    //      "                  EMPLOYEE_PAN              TEXT(10)  DEFAULT \"\"," +
                    //      "                  EMPLOYEE_NAME             TEXT(255) DEFAULT \"\"," +
                    //      "                  CATEGORY                  TEXT(4)  DEFAULT \"\"," +
                    //      "                  TAXABLE_INCOME            MONEY     DEFAULT 0," +
                    //      "                  CURRENT_SALARY            MONEY     DEFAULT 0," +
                    //      "                  RELIEF_AMOUNT             MONEY     DEFAULT 0," +
                    //      "                  TAX_TO_PAY                MONEY     DEFAULT 0," +
                    //      "                  TDS_ENTERED               MONEY     DEFAULT 0," +
                    //      "                  SHORT_DEDUCTION_AMOUNT    MONEY     DEFAULT 0)";
                    strSQL = "CREATE TABLE " + TempTable + @" (
                             " + cmnService.J_GetDataType("PREDICT_SHORT_DED_SD_ID", J_Identity.YES) + @",
                             " + cmnService.J_GetDataType("BASIC_INFO_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("ASST_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SL_NO", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_ID", J_ColumnType.Integer, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_PAN", J_ColumnType.String, 10, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("EMPLOYEE_NAME", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CATEGORY", J_ColumnType.String, 255, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAXABLE_INCOME", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("CURRENT_SALARY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("RELIEF_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TAX_TO_PAY", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("TDS_ENTERED", J_ColumnType.Double, J_DefaultValue.YES) + @",
                             " + cmnService.J_GetDataType("SHORT_DEDUCTION_AMOUNT", J_ColumnType.Double, J_DefaultValue.YES) + @")";
                    dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                }
                //
                dmlService.J_Commit();
                //
                return true;
            }
            catch (Exception err)
            {
                return false;
                cmnService.J_UserMessage(err.Message);
            }
        }
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

        #region dgvShortDeductions_CellFormatting
        private void dgvShortDeductions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            #region CellFormatting(COMPLIANCE_HIGHER_RATE_FLAG)

            if (dgvShortDeductions.Columns[e.ColumnIndex].DataPropertyName == "DEDUCTEE_PAN")
            {
                //CHECKING THE INVALID PAN FLAG FOR THIS PAN
                if (strFromModule == T_OTHERMODULENAME.REGULAR_FORM)
                {
                    if (Convert.ToString(dgvShortDeductions.Rows[e.RowIndex].Cells["COMPLIANCE_HIGHER_RATE_FLAG"].Value) == "1")
                    {
                        //NOW HIGHLIGHLING THE CELL 
                        //highlighting the cell
                        //e.CellStyle.BackColor = Color.FromArgb(231, 206, 248); //PURPLE COLOR
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        dgvShortDeductions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Short Deductions under Section 206AB/206CCA";
                        intCOMPLIANCE_HIGHER_RATE_FLAG = 1;
                    }
                    //
                    if (intCOMPLIANCE_HIGHER_RATE_FLAG > 0)
                        lblPANMarkedRed.Visible = true;
                    else
                        lblPANMarkedRed.Visible = false;
                }
                //-- 2023/10/16
                if (Convert.ToString(dgvShortDeductions.Rows[e.RowIndex].Cells["VERIFIED_PAN_STATUS"].Value) == "1")
                {
                    //NOW HIGHLIGHLING THE CELL 
                    //highlighting the cell
                    //e.CellStyle.BackColor = Color.FromArgb(231, 206, 248); //PURPLE COLOR
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                    dgvShortDeductions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Invalid PAN";
                    //intCOMPLIANCE_HIGHER_RATE_FLAG = 1;
                }
                //
                if (Convert.ToString(dgvShortDeductions.Rows[e.RowIndex].Cells["VERIFIED_PAN_STATUS"].Value) == "2")
                {
                    //NOW HIGHLIGHLING THE CELL 
                    //highlighting the cell
                    //e.CellStyle.BackColor = Color.FromArgb(231, 206, 248); //PURPLE COLOR
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                    dgvShortDeductions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Inoperative PAN";
                    //intCOMPLIANCE_HIGHER_RATE_FLAG = 1;
                }
                //
            }
            #endregion
        }
        #endregion

        #region POPULATE COMBO BOX
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox)
        {
            IDataReader reader = null;

            try
            {
                //if (command == null)
                reader = dmlService.J_ExecSqlReturnReader(SqlText);
                //else if (command != null)
                //    reader = dmlService.J_ExecSqlReturnReader(command, SqlText);

                if (reader == null) return false;

                //cmnService.J_ClearComboBox(ref combobox, DefaultText, ComboBoxDefaultText, J_ComboBoxSelectedIndex.NO);
                cmnService.J_ClearComboBox(ref combobox);
                while (reader.Read())
                    combobox.Items.Add(new ComboBoxItem(reader.GetString(0).ToString(), reader.GetValue(1).ToString()));

                // reader object is closed & disposed
                reader.Close();
                reader.Dispose();

                //if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.YES)
                //    combobox.SelectedIndex = SelectedIndex;
                //else if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.NO)
                //{
                //    if (DefaultText == null || DefaultText == "")
                //    {
                //        combobox.Text = "";
                //        combobox.SelectedText = "";
                //    }
                //    else
                //    {
                //        combobox.Text = DefaultText;
                //        combobox.SelectedText = DefaultText;
                //    }
                //}
                return true;
            }
            catch (Exception ERR)
            {
                reader.Close();
                reader.Dispose();

                //this.J_CloseConnection();
                //
                //if (File.Exists(Path.Combine(Application.StartupPath, strErrFile)) == true)
                //    cmnService.J_UserMessage(ERR.Message);
                //else
                cmnService.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return false;
            }
        }
        #endregion

        #endregion

    }
}
