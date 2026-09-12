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
    public partial class TrnHealthCheckup : TDSMAN.FormGen.GenForm
    {
        #region System Generated Code
        public TrnHealthCheckup()
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
        DataSet dsetGridClone = new DataSet();
        DataSet dsetChallanGridClone = new DataSet();
        DataSet dsetChallanDetailsGridClone = new DataSet();
        RptDialog rptDialog = new RptDialog();
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
        //--            
        IDataReader drdShowDeducteePAN = null;
        ToolTip tllTip = new ToolTip();
        //--            
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
        //--
        //string strFormNo = ""; string strQuarter = ""; string strCompanyName = "";
        #endregion       

        #region User Defined Events

        #region TrnHealthCheckup_Load
        private void TrnHealthCheckup_Load(object sender, EventArgs e)
        {
            //--
            lblDisclaimer.Text = "Please note: The defaults has been calculated as per the due dates and tax rate " +
                                 "published in Income Tax India website. For further clarification " +
                                 "visit Income Tax India website.";
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
            string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo, 1);
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------
            //cmbFinancialYear.Select();

            //----------------------------------------------------------
            //ADDED BY DHRUB ON 13/01/2014 FOR DEDUCTED DATE VISIBILITY 
            //----------------------------------------------------------
            TdsMan.GetSetup();
            //------------------
            //----------
            //intBookmark = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT SHOW_BOOKMARK FROM MST_SETUP"));
            //if (intBookmark == 0)
            //    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = false;
            //else
            //    TDSMAN.Classes.TDSMAN.T_pBookMarkOption = true;

            //if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == false) return;

            if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
            {
                //strQuery = @" FORM_NAME= '" + strFormNo + "' ";

                if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL") == true)
                {
                    intAsstId = TdsMan.T_GetBookMarkDetail(dmlService.J_pCommand, T_TransactionMode.SHOW.ToString(), 0, "", "", 0, TDSMAN.Classes.TDSMAN.T_MACHINE_ID,out strQuarter, out strCompanyName, out strTAN, out strFormNoBkmark, out strFAYear);
                    //
//                    strSQL = @"SELECT FA_YEAR
//                              FROM MST_ASSESSMENT
//                              WHERE ASST_ID=" + intAsstId + "";
//                    cmbFinancialYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));//Convert.ToInt32(Support.SetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex, intAsstId));
                    cmbQuarter.Text = strQuarter;
                    cmbCompany.Text = strCompanyName + " [" + strTAN + "]";
                    cmbFormNo.Text= strFormNoBkmark;
                    cmbFinancialYear.Text = strFAYear;
                }
            }
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReturnSummary.Visible = false;
            ClearControls();

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

            lblHideTabs.Text = "";
            pnlReturnSummary.Visible = true;
            ControlSummaryBasicInfo(lngBasicInfoID);
            LoadDetails(lngBasicInfoID);
            //--            
            
        }
        #endregion

        #region btnViewShortDeductions_Click
        private void btnViewShortDeductions_Click(object sender, EventArgs e)
        {
            if (lngBasicInfoID == 0) return;
            //
            lblSection.Visible = true;
            cmbSection.Visible = true;
            cmbSection.SelectedIndex = 0;
            ClearSearchControls();
            //
            lblHideTabs.Text = "Short Deductions";
            //
            tbcView.Visible = true;
            tbcView.SelectTab(tbpShortDeductions);
                
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

        #region txtDeducteeName_TextChanged
        private void txtDeducteeName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lngBasicInfoID == 0) return;
                //--
                strSearchConditions = "";
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
                //--
                if (tbcView.SelectedTab == tbpShortDeductions)
                {
                    if (cmbSection.SelectedIndex > 0)
                        strSearchConditions = strSearchConditions + " AND MST_SECTION.SECTION_ID =" + cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex) + " ";
                    PopulateShortDeductions(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strActualAmtCreation, strSearchConditions);
                }
                else if (tbcView.SelectedTab == tbpLatePayments)
                    PopulateLatePayments(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strLastDateCreation, strSearchConditions);
                else if (tbcView.SelectedTab == tbpInvalidPAN)
                    PopulateInvalidPANs(lngBasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);

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
                    rptDialog.PrintShortDeductionRegular(lngBasicInfoID, 
                                             cmbFormNo.Text,
                                             cmbQuarter.Text,
                                             cmbFinancialYear.Text,
                                             Convert.ToInt32(cmnService.J_GetComboBoxItemId(ref cmbSection, cmbSection.SelectedIndex)),
                                             Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                             Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                             Convert.ToString(cmbCompany.Text));
                }
                else if (tbcView.SelectedTab == tbpLatePayments)
                {
                    rptDialog.PrintLatePaymentRegular(lngBasicInfoID,
                                          cmbFormNo.Text,
                                          cmbQuarter.Text,
                                          cmbFinancialYear.Text,
                                          Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteeName.Text)),
                                          Convert.ToString(cmnService.J_ReplaceQuote(txtDeducteePAN.Text)),
                                          Convert.ToString(cmbCompany.Text));
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

        #endregion

        #region User Defined Functions

        #region ClearControls
        private void ClearControls()
        {
            lblShortDeductionsNos.Text = "";
            lblLatePaymentsNos.Text = "";
            lblInvalidPANNos.Text = "";
            txtDeducteeName.Text = "";
            txtDeducteePAN.Text = "";
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
        }
        #endregion

        #region ClearSearchControls
        private void ClearSearchControls()
        {
            //
            strSQL = @"SELECT DISTINCT MST_SECTION.SECTION_ID,
                                  MST_SECTION.SECTION_NO
                           FROM   TRN_DEDUCTEE_DETAILS, 
                                  MST_SECTION,
                                  MST_NONSALARY_TAX_SLAB
                           WHERE  TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                           AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                           AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                           AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL 
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + lngBasicInfoID + " ";
            dmlService.J_PopulateComboBox(strSQL, ref cmbSection);
            txtDeducteeName.Text = "";
            txtDeducteePAN.Text = "";
        }
        #endregion

        #region LoadDetails
        private void LoadDetails(long BasicInfoID)
        {
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
                #endregion
                //-- ANIK @ 2014-05-23
                strActualAmtCreation = @"IIF(" + strTableName + "." + strFieldPAN + " = 'PANNOTAVBL' " +
                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANAPPLIED' " +
                                       @"OR " + strTableName + "." + strFieldPAN + " = 'PANINVALID', " +
                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE ) / 100,0), 
                                    IIF(MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                                       @"ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE ) / 100,0), 
                                    ROUND((TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT * MST_NONSALARY_TAX_SLAB.COMPANY_RATE ) / 100,0)))";
                strSQL = @"SELECT COUNT(*) 
                           FROM   TRN_DEDUCTEE_DETAILS, 
                                  MST_DEDUCTEE, 
                                  MST_NONSALARY_TAX_SLAB 
                           WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID    = " + strTableName + "." + strFieldId + " " +
                         @"AND    TRN_DEDUCTEE_DETAILS.SECTION_ID = MST_NONSALARY_TAX_SLAB.SECTION_ID  
                           AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                         @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + strActualAmtCreation + " " +
                         @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)";
                long lngShortDeductionsNos = cmnService.J_ReturnInt64Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
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
                #endregion
                //--
                #region SUMMARY LATE PAYMENTS
                strLastDateCreation = "IIF(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NULL, TRN_DEDUCTEE_DETAILS.PAYMENT_DATE, " +
                                      "IIF(MONTH(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE) = 3, " +
                                              "CDATE(" + "\"30\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)), " +
                                              "CDATE(" + "\"07\"" + "+" + "RIGHT(CDATE(DATEADD(" + "\"M\"" + ", 1, " +
                                              cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")), 8)) " +
                                            "))";
                strSQL = @"SELECT COUNT(*) 
                           FROM   TRN_CHALLAN, 
                                  TRN_DEDUCTEE_DETAILS 
                           WHERE  TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID 
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                         @"AND    TRN_CHALLAN.DEPOSIT_DATE        > " + strLastDateCreation + " " +
                          "AND    TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE IS NOT NULL";
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
                strSQL = @" SELECT COUNT(*)
                        FROM       TRN_DEDUCTEE_DETAILS, 
                                   " + strTableName + " " +
                       @"WHERE     TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strTableName + "." + strFieldId + "  " +
                       @"AND       IIF(" + strTableName + "." + strFieldPAN + " <> 'PANNOTAVBL' " +
                                         @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANAPPLIED' " +
                                         @"AND             " + strTableName + "." + strFieldPAN + " <> 'PANINVALID' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'C' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'P' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'H' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'F' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'A' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'T' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'B' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'L' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'J' " +
                                         @"AND             MID(" + strTableName + "." + strFieldPAN + ", 4, 1) <> 'G' " +  
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

                //-- GET DETAIL INFORMATION
                PopulateShortDeductions(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strActualAmtCreation, strSearchConditions);
                PopulateLatePayments(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strLastDateCreation, strSearchConditions);
                PopulateInvalidPANs(BasicInfoID, strTableName, strFieldId, strFieldPAN, strFieldName, strSearchConditions);
                //
                ClearSearchControls();            
            }
            catch (Exception ERR)
            {
                cmnService.J_UserMessage(ERR.Message);
            }
        }
        #endregion

        #region PopulateLatePayments
        public void PopulateLatePayments(long BasicInfoID, string TableName, string FieldId, string FieldPAN, string FieldName, string Argumnets, string SearchConditions)
        {       
            //--
            DataSet dsetLatePayments = new DataSet(); 
            try
            {
                //--
                strSQL = @"SELECT  TRN_CHALLAN.SL_NO          AS CHALLAN_SL_NO,
                                   TRN_DEDUCTEE_DETAILS.SL_NO AS DEDUCTEE_SL_NO," +   
                                   TableName + "." + FieldPAN + ", " +
                                   TableName + "." + FieldName + ", " + 
                                  @"TRN_DEDUCTEE_DETAILS.PAYMENT_AMOUNT,
                                   TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT,
                                   TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, " +
                                   Argumnets + "              AS LAST_DATE, " +
                                   "TRN_CHALLAN.DEPOSIT_DATE AS CHALLAN_DEPOSIT_DATE, " +
                                   //" 1 + " +
                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
                                   //"     DAY(" + Argumnets + "),1,0) AS  TOTAL_DELAY, " +
                                   //" TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                                   //" 1+ " +
                                   //" DATEDIFF(" + "\"M\"," + Argumnets + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") + " +
                                   //" IIF(DAY(" + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " ) > " +
                                   //"     DAY(" + Argumnets + "),1,0)) AS  LATE_FEE " +
                                   //-- 2014-05-30
                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM'))) AS TOTAL_DELAY, " +
                                   //-- 2014-06-17
                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ") AS TOTAL_DELAY, " +
                                   //"TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                                   //"1 + (CLNG(FORMAT(TRN_CHALLAN.DEPOSIT_DATE, 'YYYYMM')) - CLNG(FORMAT(TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE, 'YYYYMM')))) AS LATE_FEE " +
                                   //-- 2014-06-17
                                   "TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT * 0.015 * ( " +
                                   "1 + DATEDIFF('m'," + cmnService.J_SQLDBFormat("TRN_DEDUCTEE_DETAILS.DEDUCTED_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "," + cmnService.J_SQLDBFormat("TRN_CHALLAN.DEPOSIT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + ")) AS LATE_FEE " +
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
                                                      {"Interest","60", "0.00", "R", "", "", ""}};
                if (dsetLatePayments != null) dsetLatePayments.Clear();
                dsetLatePayments = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvLatePayments, strSQL + SearchConditions + strOrderBy, strMatrixViewLatePayments);
                //--
                double TotalInterest = 0; string InterestAmountReported = "";
                for (int i = 0; i <= dgvLatePayments.RowCount - 1; i++)
                {
                    TotalInterest += cmnService.J_ReturnDoubleValue(dgvLatePayments.Rows[i].Cells[10].Value);
                }
                lblTotalInterest.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalInterest) == "" ? "0" : Convert.ToString(TotalInterest))); 
                //
                strSQL = "SELECT SUM(INTEREST_ALLOCATED) FROM TRN_CHALLAN, TRN_BASIC_INFO WHERE TRN_CHALLAN.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.BASIC_INFO_ID = " + BasicInfoID + " ";
                lblInterestAmountReported.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))));
                //lblInterestAmountReported.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(InterestAmountReported) == "" ? "0" : Convert.ToString(InterestAmountReported))); 
                //
                if (cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text) > cmnService.J_ReturnDoubleValue(lblTotalInterest.Text))
                    lblInterestPayable.Text = "0.00";
                else
                    lblInterestPayable.Text = string.Format("{0:0.00}", cmnService.J_ReturnDoubleValue(lblTotalInterest.Text) - cmnService.J_ReturnDoubleValue(lblInterestAmountReported.Text));

                //--
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
                                            @"OR " + TableName + "." + FieldPAN + " = 'PANINVALID', " +
                                      @"MST_NONSALARY_TAX_SLAB.INVALID_PAN_RATE,  
                                           IIF(MID(" + TableName + "." + FieldPAN + ", 4, 1) = 'P' OR MID(" + strTableName + "." + strFieldPAN + ", 4, 1) = 'H', " +
                                               @"MST_NONSALARY_TAX_SLAB.NON_COMPANY_RATE, 
                                               MST_NONSALARY_TAX_SLAB.COMPANY_RATE)
                                      )          AS RATE," +
                                  Argumnets + "                                  AS ACTUAL_TAX_AMOUNT," +
                                  Argumnets + " - TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT  AS SHORT_PAYMENT " +
                         @"FROM   TRN_DEDUCTEE_DETAILS," + 
                                   TableName + "," +
                                 @"MST_NONSALARY_TAX_SLAB,
                                  TRN_CHALLAN,
                                  MST_SECTION
                           WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID       = " + TableName + "." + FieldId + "  " +
                         @"AND    TRN_DEDUCTEE_DETAILS.CHALLAN_ID     = TRN_CHALLAN.CHALLAN_ID        
                           AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_NONSALARY_TAX_SLAB.SECTION_ID 
                           AND    TRN_DEDUCTEE_DETAILS.SECTION_ID     = MST_SECTION.SECTION_ID  
                           AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE BETWEEN MST_NONSALARY_TAX_SLAB.FROM_DATE AND MST_NONSALARY_TAX_SLAB.TO_DATE  
                           AND    TRN_DEDUCTEE_DETAILS.PAYMENT_DATE IS NOT NULL 
                           AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID    = " + BasicInfoID + " " +
                         @"AND    TRN_DEDUCTEE_DETAILS.TAX_DEPOSITED_AMOUNT < " + Argumnets + " " +
                         @"AND    TRN_DEDUCTEE_DETAILS.REASON_ID IN (3,7,15,4,12,17,20)" ;
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
                                                  {"TDS Rate",     "70", "0.00", "R", "", "", ""},
                                                  {"TDS To Be Deducted","80", "0.00", "R", "", "", ""},
                                                  {"Short Payment","70", "0.00", "R", "", "", ""}};
                if (dsetShortDeductions != null) dsetShortDeductions.Clear();
                dsetShortDeductions = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvShortDeductions, strSQL + SearchConditions + strOrderBy, strMatrixViewShortDeductions);
                //--
                long TotalShortPayments=0;
                for (int i = 0; i <= dgvShortDeductions.RowCount - 1; i++)
                {
                    TotalShortPayments += cmnService.J_ReturnInt64Value(dgvShortDeductions.Rows[i].Cells[10].Value);
                }
                lblTotalShortPayments.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(TotalShortPayments) == "" ? "0" : Convert.ToString(TotalShortPayments))); 
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
                // Get the details of Grid
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
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'C' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'P' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'H' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'F' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'A' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'T' " +
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'B' " +  
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'L' " +  
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'J' " +  
                                            @"AND             MID(" + TableName + "." + FieldPAN + ", 4, 1) <> 'G' " +  
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

        #endregion

        

    }
}

