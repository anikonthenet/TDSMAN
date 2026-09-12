#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

using System.Runtime.InteropServices;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using TDSMAN.FormTrn; //-- Added By Abhishek Dey On 22/05/2018--

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysShowConsumption : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public SysShowConsumption()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Objects & Variables declaration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Validation";
        string strTitle = "Total Deductee / Collectee Records for FA year : ";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnSelectComboExit = false;
        //
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int j = 0;
        //
        string strNoRecordFound = "No record found";
        //--
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

        #region SysShowUsage_Activated
        private void SysShowUsage_Activated(object sender, EventArgs e)
        {
            //-
            lblTotalRecords.Text = strTitle;
            //-
            ClearControls();
            //--
            //LoadControls();
            //--
            RbnRegular_CheckedChanged(sender, e);
        }
        #endregion
        
        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (blnSelectComboExit == true)
            //    return;
            //
            ClearControls();
            //
            lngBasicInfoID = 0;
            //
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                //grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                lblTotalRecords.Text = strTitle;
                ClearControls();
                grpControlSummary.Enabled = false;
                //lblTotalRecordsReturn.Visible = false;
                //lblTotalRecords.Visible = false;
                //btnNext.Enabled = false;
                //btnNext.BackColor = Color.LightGray;
                return;
            }
            else if (cmbFinancialYear.SelectedIndex > 0)
            {
                //
                long lngTotalRecords = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM TRN_DEDUCTEE_DETAILS, TRN_BASIC_INFO WHERE TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID AND TRN_BASIC_INFO.ASST_ID = " + Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), J_QueryType.DirectQuery);
                lblTotalRecords.Text = strTitle + cmbFinancialYear.Text;
                //
                grpControlSummary.Enabled = true;
                //if (lngTotalRecords > 0)
                //{
                //    lblTotalRecords.Visible = true;
                //    lblTotalRecords.Text = "Total Records for FA year [" + cmbFinancialYear.Text + "] : " + Convert.ToString(lngTotalRecords);
                //}
                //else
                //    lblTotalRecords.Visible = false;
            }
            //--
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    //grpControlSummary.Visible = false;
            //    //pnlLine1.Visible = false;
            //    //pnlLine2.Visible = false;
            //    //grpReturnFilingStatus.Visible = false;
            //    ClearControls();
            //    lblTotalRecordsReturn.Visible = false;
            //    //btnNext.Enabled = false;
            //    //btnNext.BackColor = Color.LightGray;
            //    return;
            //}
            if (cmbFormNo.Text == "")
            {
                //grpControlSummary.Visible = false;
                //pnlLine1.Visible = false;
                //pnlLine2.Visible = false;
                //grpReturnFilingStatus.Visible = false;
                ClearControls();
                //lblTotalRecordsReturn.Visible = false;
                //btnNext.Enabled = false;
                //btnNext.BackColor = Color.LightGray;
                lblForm24Q.Visible = true;
                lblForm24QCount.Visible = true;
                lblForm26Q.Visible = true;
                lblForm26QCount.Visible = true;
                lblForm27Q.Visible = true;
                lblForm27QCount.Visible = true;
                lblForm27EQ.Visible = true;
                lblForm27EQCount.Visible = true;
                //return;
            }
            else
            {
                if (cmbFormNo.Text == T_FormNo.F24Q)
                {
                    lblForm24Q.Visible = true;
                    lblForm24QCount.Visible = true;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                }
                else if (cmbFormNo.Text == T_FormNo.F26Q)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = true;
                    lblForm26QCount.Visible = true;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                } 
                else if (cmbFormNo.Text == T_FormNo.F27Q)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = true;
                    lblForm27QCount.Visible = true;
                    lblForm27EQ.Visible = false;
                    lblForm27EQCount.Visible = false;
                } 
                else if (cmbFormNo.Text == T_FormNo.F27EQ)
                {
                    lblForm24Q.Visible = false;
                    lblForm24QCount.Visible = false;
                    lblForm26Q.Visible = false;
                    lblForm26QCount.Visible = false;
                    lblForm27Q.Visible = false;
                    lblForm27QCount.Visible = false;
                    lblForm27EQ.Visible = true;
                    lblForm27EQCount.Visible = true;
                }
            }
            //--
            //if (cmbQuarter.Text == "")
            //{
            //    //grpControlSummary.Visible = false;
            //    //pnlLine1.Visible = false;
            //    //pnlLine2.Visible = false;
            //    //grpReturnFilingStatus.Visible = false;
            //    ClearControls();
            //    lblTotalRecordsReturn.Visible = false;
            //    //btnNext.Enabled = false;
            //    //btnNext.BackColor = Color.LightGray;
            //    return;
            //}
            //--
            //lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
            //                                            cmbQuarter.Text,
            //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
            //                                            cmbFormNo.Text);
            //--
            ControlSummaryBasicInfo(Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)), cmbFormNo.Text, cmbQuarter.Text, Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)));
            //-- 
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //
            GC.Collect();
            //
            blnVerificationComplete = false;
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region ClearControls
        private void ClearControls()
        {
            //grpButtons.Enabled = true;
            //
            //txtTotalChallanRecords.Text = "0";
            //txtTotalDeducteeRecords.Text = "0";
            //txtTotalChallanAmount.Text = "0.00";
            //txtTotalDeducteeTDS.Text = "0.00";
            //txtAmountPaid.Text = "0.00";
            ////
            //txtReceiptNo.Text = "";
            //txtDateofFiling.Text = "";
            //txtTokenNo.Text = "";
        }
        #endregion

        #region LoadRegularControls
        private void LoadRegularControls()
        {
            //
            blnSelectComboExit = true;
            ////-----------
            ////-- FINANCIAL YEAR
            ////-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, J_ComboBoxSelectedIndex.YES) == false) return;
            ////-----------
            //-- QUARTER
            //-----------
            string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter);
            //-----------
            //-- FORM NO
            //-----------
            string[] strFormNo1 ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            dmlService.J_PopulateComboBox(strFormNo1, ref cmbFormNo);
            //-----------
            //-- COMPANY
            //-----------
            strSQL = " SELECT COMPANY_ID," +
                "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                "      FROM   MST_COMPANY " +
                "      WHERE  INACTIVE_FLAG = 0 " +
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //
            blnSelectComboExit = false;
            //-- 2026/02/20
            if (!bgCalcTotalConsumption.IsBusy)
                bgCalcTotalConsumption.RunWorkerAsync();
            //-----------
            lblRegularCapacity.Text = " - Capacity : " + TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions.ToString("N0") + " - "; 
        }
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfoCorr()
        {
            //--
            #region CHECK LINE RECORDS OF CONSO FILE IMPORT (4 times OF MAX ALLOWED LINE RECORDS) //-- 
            //
            string strConsolidatedStatementPath = dmlService.J_ExecSqlReturnScalar("SELECT TDS_FILE_PATH FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBasicInfoID).ToString();

            if (strConsolidatedStatementPath != "")
            {
                if (File.Exists(strConsolidatedStatementPath) == false)
                    strConsolidatedStatementPath = "";
            }
            int intCountConsoDD = 0, intCountConsoSD = 0, intCountConso194P = 0;
                
            if (strConsolidatedStatementPath != "")
            {
                #region GET LINE OF RECORDS OF CONSO FILE
                //-- DD
                foreach (string line in File.ReadLines(strConsolidatedStatementPath))
                {
                    if (line.Contains("^DD^"))
                        intCountConsoDD++;
                }
                //-- SD
                //int intCountConsoSD = 0;
                foreach (string line in File.ReadLines(strConsolidatedStatementPath))
                {
                    if (line.Contains("^SD^"))
                        intCountConsoSD++;
                }
                //-- 94P
                //int intCountConso194P = 0;
                foreach (string line in File.ReadLines(strConsolidatedStatementPath))
                {
                    if (line.Contains("^94P^"))
                        intCountConso194P++;
                }
                //--
                #endregion
            }
                //
                #region FETCHING EXISTING LINE OF RECORDS FROM EXISTING DATA BASED ON 'SoftwareVersion'
                //### GETTING THE BATCH ID(s) FOR THIS 'SoftwareVersion' ###
                IDataReader drdGetBasicInfoIDs = null;
                List<int> BatchHeaderIds = new List<int>();
                string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                //
                strSQL = "SELECT BATCH_HEADER_ID FROM COR_HDR_BATCH WHERE VERSION_NO = " + SoftwareVersion;
                drdGetBasicInfoIDs = dmlService.J_ExecSqlReturnReader(strSQL);
                //if (drdGetBasicInfoIDs == null)
                //    return false;
                //--
                while (drdGetBasicInfoIDs.Read())
                {
                    BatchHeaderIds.Add(Convert.ToInt32(drdGetBasicInfoIDs["BATCH_HEADER_ID"]));
                }
                drdGetBasicInfoIDs.Close();
                long lngDDCount = 0, lngSDCount = 0, lng194PCount = 0;
                foreach (int BatchHeaderId in BatchHeaderIds) //-- DD
                {
                    lngDDCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_DEDUCTEE_DETAILS WHERE MODE <> '' AND BATCH_HEADER_ID = " + lngBasicInfoID, J_QueryType.DirectQuery);
                    //
                }
                foreach (int BatchHeaderId in BatchHeaderIds) //-- SD
                {
                    lngSDCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS WHERE MODE <> '' AND BATCH_HEADER_ID = " + lngBasicInfoID, J_QueryType.DirectQuery);
                    //
                }
                foreach (int BatchHeaderId in BatchHeaderIds) //-- 94P
                {
                    lng194PCount = dmlService.J_ReturnNoOfRows("SELECT COUNT(*) FROM COR_TRN_SALARY_DETAILS_194P WHERE MODE <> '' AND BATCH_HEADER_ID = " + lngBasicInfoID, J_QueryType.DirectQuery);
                    //
                }
                #endregion
                //--
                lblDDImportCount.Text = "Import Deductee Details Count   : " + intCountConsoDD.ToString();
                lblDDCount.Text = "Add/Modify/Delete Deductee Details Count   : " + lngDDCount.ToString();
                //
                if (intCountConsoSD > 0 || lngSDCount > 0)
                {
                    lblSDImportCount.Visible = true; lblSDCount.Visible = true;
                    lblSDImportCount.Text = "Import Salary Details Count   : " + intCountConsoSD.ToString();
                    lblSDCount.Text = "Add/Modify/Delete Salary Details Count   : " + lngSDCount.ToString();
                }
                else
                {
                    lblSDImportCount.Visible = false; lblSDCount.Visible = false;
                }
                //
                if (intCountConso194P > 0 || lng194PCount > 0)
                {
                    lbl194PImportCount.Visible = true; lbl194PCount.Visible = true;
                    lbl194PImportCount.Text = "Import 194P Count   : " + intCountConso194P.ToString();
                    lbl194PCount.Text = "Add/Modify/Delete 194P Count   : " + lng194PCount.ToString();
                }
                else
                {
                    lbl194PImportCount.Visible = false; lbl194PCount.Visible = false;
                }
                //--
                #region COMMENT
                //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.LICENSED_VERSION)
                //{
                //    if (intCountConsoDD + intCountConsoSD + intCountConso94P + lngCorrectionLineOfRecords > TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions)
                //    {
                //        //cmnService.J_UserMessage("Exceeding the conso import limit (" + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions + ") line records");
                //        TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                //        TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                //        //
                //        if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION)
                //            TrialMessageBox.lblMessage1.Text = "You are using the Standard version.";
                //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                //            TrialMessageBox.lblMessage1.Text = "You are using the Professional version.";
                //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION)
                //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise version.";
                //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION)
                //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise (Lite) version.";
                //        else if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
                //            TrialMessageBox.lblMessage1.Text = "You are using the Enterprise (Ultimate) version.";
                //        //
                //        TrialMessageBox.lblMessage2.Text = "It supports only upto " + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions + " records in the conso file.";
                //        TrialMessageBox.Show();
                //        return;
                //    }
                //}
                //else
                //{
                //    if (intCountConsoDD + intCountConsoSD + intCountConso94P + lngCorrectionLineOfRecords > TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions)
                //    {
                //        //cmnService.J_UserMessage("Exceeding the conso import limit (" + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions + ") line records");
                //        TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                //        TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                //        TrialMessageBox.lblMessage2.Text = "It supports only upto " + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions + " records in the conso file.";
                //        TrialMessageBox.Show();
                //        return;
                //    }
                //}
                #endregion
                //################################
            
            //else
            //{
            //    lblDDImportCount.Visible = false; lblDDCount.Visible = false;
            //    lblSDImportCount.Visible = false; lblSDCount.Visible = false;
            //    lbl194PImportCount.Visible = false; lbl194PCount.Visible = false;

            //}
            #endregion
            //--
        }
        #endregion

        #region ControlSummaryBasicInfo
        private void ControlSummaryBasicInfo(long FaYearID, string FormNo, string Qtr, long CompanyID)
        {
            //--
            pnlLine1.Visible = true;
            pnlLine2.Visible = true;
            //lblTotalRecordsReturn.Visible = true;
            //--
            #region F24Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F24Q)
            //{
            strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F24Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm24QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm24QCount.Text == "")
            {
                lblForm24Q.Visible = false;
                lblForm24QCount.Visible = false;
            }
            #endregion
            //--
            #region F26Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F26Q)
            //{
            strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F26Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm26QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm26QCount.Text == "")
            {
                lblForm26Q.Visible = false;
                lblForm26QCount.Visible = false;
            }
            #endregion
            //--
            #region F27Q
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F27Q)
            //{
            strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F27Q + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm27QCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm27QCount.Text == "")
            {
                lblForm27Q.Visible = false;
                lblForm27QCount.Visible = false;
            }
            #endregion
            //--
            #region F27EQ
            strSQL = @"SELECT COUNT(*) 
                       FROM   TRN_DEDUCTEE_DETAILS,
                              TRN_BASIC_INFO
                       WHERE  TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = TRN_BASIC_INFO.BASIC_INFO_ID 
                       AND    TRN_BASIC_INFO.ASST_ID = " + FaYearID;
            //if (FormNo == T_FormNo.F27EQ)
            //{
            strSQL = strSQL + " AND TRN_BASIC_INFO.FORM_NO ='" + T_FormNo.F27EQ + "' ";
            //}
            //
            if (Qtr != "")
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.QTR ='" + Qtr + "' ";
            }
            //
            if (CompanyID > 0)
            {
                strSQL = strSQL + " AND TRN_BASIC_INFO.COMPANY_ID = " + CompanyID + " ";
            }
            lblForm27EQCount.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (lblForm27EQCount.Text == "")
            {
                lblForm27EQ.Visible = false;
                lblForm27EQCount.Visible = false;
            }
            #endregion
            //--
            #region TOTAL
            if (FormNo == T_FormNo.F24Q)
            {
                lblForm26QCount.Text = "0";
                lblForm27QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F26Q)
            {
                lblForm24QCount.Text = "0";
                lblForm27QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F27Q)
            {
                lblForm24QCount.Text = "0";
                lblForm26QCount.Text = "0";
                lblForm27EQCount.Text = "0";
            }
            else if (FormNo == T_FormNo.F27EQ)
            {
                lblForm24QCount.Text = "0";
                lblForm26QCount.Text = "0";
                lblForm27QCount.Text = "0";
            }
            //--
            lblTotalCount.Text =(cmnService.J_ReturnInt64Value(lblForm24QCount.Text) + cmnService.J_ReturnInt64Value(lblForm26QCount.Text) + cmnService.J_ReturnInt64Value(lblForm27QCount.Text) + cmnService.J_ReturnInt64Value(lblForm27EQCount.Text)).ToString("N0");
            //--
            if (lblForm24QCount.Text == "0")
                lblForm24QCount.Text = strNoRecordFound;
            //
            if (lblForm26QCount.Text == "0")
                lblForm26QCount.Text = strNoRecordFound;
            //
            if (lblForm27QCount.Text == "0")
                lblForm27QCount.Text = strNoRecordFound;
            //
            if (lblForm27EQCount.Text == "0")
                lblForm27EQCount.Text = strNoRecordFound;
            //
            #endregion
            //
        }
        #endregion


        #region LoadCorrectionBatch
        private void LoadCorrectionBatch()
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
                                        {"Total Corrections", "100", "", "Right", "", "", ""},
                                        {"FA Year Id", "0", "", "", "", "F", ""}};
            //-----------------------------------------------------------
            strMatrixBatchGrid = strMatrixBatch;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------

            string[,] strLoadCorrectionMatrix = {{"COR_TRN_COMPANY.MODE = '" + T_CorrectionMode.TANUpdation + "'", "F", "Cancelled", "T"},
                                                 {"JAYA", "F", "COR_HDR_BATCH.TOTAL_CORRECTION", "F"}};

            strOrderBy = "COR_HDR_BATCH.BATCH_HEADER_ID DESC";
            strQuery = "SELECT COR_HDR_BATCH.BATCH_HEADER_ID  AS BATCH_HEADER_ID," +
                      "        MST_ASSESSMENT.FA_YEAR         AS FA_YEAR," +
                      "        COR_HDR_BATCH.FORM_NO          AS FORM_NO," +
                      "        COR_HDR_BATCH.QTR              AS QTR," +
                      "        COR_TRN_COMPANY.COMPANY_NAME   AS COMPANY_NAME," +
                      "        COR_TRN_COMPANY.TAN_NO         AS TAN_NO," +
                      //"        FORMAT(COR_HDR_BATCH.IMPORTED_DATE, \"dd/MM/yyyy h:m AMPM\")   AS IMPORTED_DATE," +
                      "        " + cmnService.J_SQLDBFormat("COR_HDR_BATCH.IMPORTED_DATE", J_SQLColFormat.DateTimeFormatDDMMYYYYHHMMSS) + " AS IMPORTED_DATE, " +
                      "        " + cmnService.J_SQLDBFormat(strLoadCorrectionMatrix, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TOTAL_CORRECTION," +
                      "        MST_ASSESSMENT.ASST_ID         AS ASST_ID " +
                      "FROM    COR_HDR_BATCH," +
                      "        MST_ASSESSMENT," +
                      "        COR_TRN_COMPANY " +
                      "WHERE   COR_HDR_BATCH.ASST_ID            = MST_ASSESSMENT.ASST_ID " +
                      "AND     COR_HDR_BATCH.BATCH_HEADER_ID    = COR_TRN_COMPANY.BATCH_HEADER_ID ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgcViewBatch, strSQL, strMatrixBatchGrid);       //Show Data into the Grid
        }


        #endregion

        #region LoadCorrectionControls
        private void LoadCorrectionControls()
        {
            //-- 2026/02/20
            if (!bgCalcTotalConsumption.IsBusy)
                bgCalcTotalConsumption.RunWorkerAsync();
            //
            blnSelectComboExit = true;
            //--
            LoadCorrectionBatch();
            //--
            blnSelectComboExit = false;
            //-----------
            lblCorrectionCapacity.Text = " - Capacity : " + (Convert.ToInt64(TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport) * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountEditions).ToString("N0") + " - ";
        }
        #endregion

        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("SysShowUsage");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }

        #endregion
        //-----------------------------------------



        #region dgcViewBatch_Click
        private void dgcViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow == null)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    //LoadDeducteeGrid();
                    return;
                }

                if (dgcViewBatch.CurrentRow.Index < 0)
                {
                    lngBasicInfoID = 0;
                    //ClearFields();
                    return;
                }
                //
                //ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                ControlSummaryBasicInfoCorr();
                //
            }
            catch (Exception err)
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
                //LoadDeducteeGridCorr(lngBasicInfoID, chkBoxNewEntriesOnly.Checked);
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
            if (dgcViewBatch.CurrentRow == null) return;
            dgcViewBatch_Click(sender, e);
        }
        #endregion


        #region ShowCorrectionRecords
        private void ShowCorrectionRecords()
        {
            //--

            //--
        }
        #endregion

        #endregion


        #region SysShowUsage_Load
        private void SysShowUsage_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0112", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void RbnRegular_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnRegular.Checked == true)
            {
                pnlRegular.Visible = true;
                pnlCorrection.Visible = false;
                //--
                LoadRegularControls();
                //--
            }
            else if (rbnCorrection.Checked == true)
            {
                pnlRegular.Visible = false;
                pnlCorrection.Visible = true;
                //--
                LoadCorrectionControls();
                //--
            }
        }

        private void BgCalcTotalConsumption_DoWork(object sender, DoWorkEventArgs e)
        {
            long lngTotalRecords = 0;
            if (rbnRegular.Checked == true)
            {
                #region GET CONSUMPTION COUNT //-- 2025/07/17
                string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                long lngRegDeducteeCount = 0, lngRegSalaryCount = 0, lngReg194PCount = 0;
                long lngCorrDeducteeAddCount = 0, lngCorrDeducteeModifyNullifyCount = 0, lngCorrDeducteePANChangeCount = 0, lngCorrSalaryChangeCount = 0, lngCorrSalaryPANChangeCount = 0, lngCorr194PChangeCount = 0, lngCorr194PPANChangeCount = 0;
                //
                TdsMan.Total_Consumption_Count(Convert.ToDouble(SoftwareVersion),
                                        out lngRegDeducteeCount,
                                        out lngRegSalaryCount,
                                        out lngReg194PCount,
                                        out lngCorrDeducteeAddCount,
                                        out lngCorrDeducteeModifyNullifyCount,
                                        out lngCorrDeducteePANChangeCount,
                                        out lngCorrSalaryChangeCount,
                                        out lngCorrSalaryPANChangeCount,
                                        out lngCorr194PChangeCount,
                                        out lngCorr194PPANChangeCount);
                lngTotalRecords = lngRegDeducteeCount + lngRegSalaryCount + lngReg194PCount + lngCorrDeducteeAddCount + lngCorrDeducteeModifyNullifyCount +
                                    lngCorrDeducteePANChangeCount + lngCorrSalaryChangeCount + lngCorrSalaryPANChangeCount + lngCorr194PChangeCount +
                                    lngCorr194PPANChangeCount;
                //
                //e.Result = lngTotalRecords;
                //
                #endregion
            }
            else if (rbnCorrection.Checked == true)
            {
                IDataReader drdGetRecord = null; long lngBatchId = 0; string strConsolidatedStatementPath = "";
                 //
                 string SoftwareVersion = TDSMAN.Classes.TDSMAN.T_pEditionType + "." + TDSMAN.Classes.TDSMAN.T_pPackageFAYear;
                //DataTable dtBatchIds = dmlService.J_ExecSqlReturnDataTable("SELECT BATCH_HEADER_ID, TDS_FILE_PATH FROM COR_HDR_BATCH WHERE VERSION_NO = " + SoftwareVersion);
                string strSeQuel = "SELECT BATCH_HEADER_ID, TDS_FILE_PATH FROM COR_HDR_BATCH WHERE VERSION_NO = " + SoftwareVersion;
                drdGetRecord = dmlService.J_ExecSqlReturnReader(strSeQuel);
                if (drdGetRecord == null)
                    return ;
                //--
                while (drdGetRecord.Read())
                {
                    lngBatchId = Convert.ToInt64(Convert.ToString(drdGetRecord["BATCH_HEADER_ID"]));
                    strConsolidatedStatementPath = Convert.ToString(drdGetRecord["TDS_FILE_PATH"]);
                    //--
                    //    if (dtBatchIds != null && dtBatchIds.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtBatchIds.Rows)
                    //    {
                    //long lngBatchId = Convert.ToInt64(row["BATCH_HEADER_ID"]);
                    //// Get file path
                    //string strConsolidatedStatementPath = row["TDS_FILE_PATH"].ToString();

                    //string strConsolidatedStatementPath =
                    //    dmlService.J_ExecSqlReturnScalar(
                    //        "SELECT TDS_FILE_PATH FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + lngBatchId
                    //    )?.ToString();

                    if (!string.IsNullOrEmpty(strConsolidatedStatementPath) &&
                        File.Exists(strConsolidatedStatementPath))
                    {
                        #region GET LINE COUNTS FROM FILE

                        int intCountConsoDD = 0;
                        int intCountConsoSD = 0;
                        int intCountConso94P = 0;

                        foreach (string line in File.ReadLines(strConsolidatedStatementPath))
                        {
                            if (line.Contains("^DD^"))
                                intCountConsoDD++;
                            else if (line.Contains("^SD^"))
                                intCountConsoSD++;
                            else if (line.Contains("^94P^"))
                                intCountConso94P++;
                        }

                        #endregion

                        lngTotalRecords = intCountConsoDD + intCountConsoSD + intCountConso94P;

                        // Console.WriteLine($"Batch: {lngBatchId} | DD: {intCountConsoDD} | SD: {intCountConsoSD} | 94P: {intCountConso94P}");
                    }
                    //    }
                    //}
                }
                drdGetRecord.Close(); drdGetRecord.Dispose();
                //
            }
                e.Result = lngTotalRecords;
                //
        }

        private void BgCalcTotalConsumption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region SHOW CONSUMPTION COUNT //-- 2026/02/19
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            long lngTotalRecords = Convert.ToInt64(e.Result);
            if (rbnRegular.Checked == true)
            {
                if (lngTotalRecords > 0)
                {
                    lblTotalConsumed.Visible = true;
                    lblTotalConsumed.Text = "Total Consumption : " + lngTotalRecords.ToString("N0");
                }
                else
                    lblTotalConsumed.Visible = false;
            }
            else if (rbnCorrection.Checked == true)
            {
                if (lngTotalRecords > 0)
                {
                    lblTotalConsumed.Visible = true;
                    lblTotalConsumed.Text = "Total tds file imported : " + lngTotalRecords.ToString("N0");
                }
                else
                    lblTotalConsumed.Visible = false;
            }
            #endregion
        }
    }
}