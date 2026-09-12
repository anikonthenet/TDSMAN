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

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnBulkPANVerification : Form
    {
        #region System Generated Code
        public TrnBulkPANVerification()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strPAN = "";
        long lngBasicInfoID = 0;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start verifying";
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        //
        int intPANId = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        int j = 0; 
        #endregion

        #region User Defined Events

        #region TrnBulkPANVerification_Load
        private void TrnBulkPANVerification_Load(object sender, EventArgs e)
        {
            string strFileDate = "2011";
            try
            {
                lblDisclaimer.Text = "The PAN verification is from the database of Income Tax Department through their web gateways.";
                //--
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
                //--
                TdsMan.GetSetup();
                //--
                #region COMMENT
                //-----------
                //-- FINANCIAL YEAR
                //-----------
                //strSQL = " SELECT ASST_ID," +
                //    "             FA_YEAR " +
                //    "      FROM   MST_ASSESSMENT " +
                //    "      WHERE  VISIBILITY_FLAG = 0 " +
                //    "      ORDER BY ASST_ID DESC";
                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
                ////-----------
                ////-- QUARTER
                ////-----------
                //string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                //dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                ////-----------
                ////-- FORM NO
                ////-----------
                //string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                //dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo, 1);
                ////-----------
                ////-- COMPANY
                ////-----------
                //strSQL = " SELECT COMPANY_ID," +
                //    "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                //    "      FROM   MST_COMPANY " +
                //    "      ORDER BY COMPANY_NAME";
                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                ////-----------
                //cmbFinancialYear.Select();
                #endregion
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN;
                dmlService.J_ExecSql(strSQL);
                //--
                #region INSERT INTO MST_VERIFIED_PAN

//                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN)
//                            SELECT DISTINCT DEDUCTEE_PAN 
//                            FROM   COR_HDR_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN
//                            ON     COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN = MST_VERIFIED_PAN.PAN
//                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
//                            AND    COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN = 0
//                            AND    COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')";
                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN)
                            SELECT DISTINCT DEDUCTEE_PAN 
                            FROM ((COR_HDR_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN
                            ON     COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN = MST_VERIFIED_PAN.PAN)
                                   INNER JOIN COR_HDR_BATCH
                            ON     COR_HDR_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                            AND    COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN = 0
                            AND    COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')
                            AND    RIGHT(COR_HDR_BATCH.FILE_DATE, 4) > " + strFileDate + " ";
                dmlService.J_ExecSql(strSQL); 
                //--
                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN)
                            SELECT DISTINCT EMPLOYEE_PAN 
                            FROM ((COR_HDR_SALARY_DETAILS LEFT JOIN MST_VERIFIED_PAN
                            ON     COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN = MST_VERIFIED_PAN.PAN)
                                   INNER JOIN COR_HDR_BATCH
                            ON     COR_HDR_SALARY_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                            AND    COR_HDR_SALARY_DETAILS.INVALID_PAN = 0
                            AND    COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')
                            AND    RIGHT(COR_HDR_BATCH.FILE_DATE, 4) > " + strFileDate + " ";
                dmlService.J_ExecSql(strSQL);

                #endregion
                //--
                LoadDeducteeGrid();
                ClearFields();
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                {
                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                    //--
                    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                        strEmpDed = "Employee";
                    else
                        strEmpDed = "Deductee";
                    //--
                    intPANId = 1;
                    intStatusId = 2;
                    intVerifyId = 3;
                    //
                    blRegular = false;
                }
                else
                {
                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                    //
                    if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q)
                        strEmpDed = "Employee";
                    else
                        strEmpDed = "Deductee";
                    //--
                    intPANId = 2;
                    intStatusId = 3;
                    intVerifyId = 4;
                    //
                    blRegular = true;
                }
                //--
                if (lngBasicInfoID == 0)
                {
                    //if (dgvDeductees.Rows.Count > 0)
                    //    dgvDeductees.Rows.Clear();
                    return;
                }
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                    LoadDeducteeGridCorr(lngBasicInfoID);
                else
                    LoadDeducteeGrid(lngBasicInfoID);
                //
                this.Cursor = Cursors.Default;
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
                btnVerification.Select();
                //-----------
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region cmbMain_SelectedIndexChanged
        private void cmbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDeducteeGrid();
            //ClearFields();
            //--
            //if (cmbFinancialYear.SelectedIndex <= 0)
            //{
            //    if(dgvDeductees.Rows.Count > 0)
            //        dgvDeductees.Rows.Clear();
            //    return;
            //}
            ////--
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    if (dgvDeductees.Rows.Count > 0)
            //        dgvDeductees.Rows.Clear();
            //    return;
            //}
            ////--
            //if (cmbFormNo.Text == "")
            //{
            //    if (dgvDeductees.Rows.Count > 0)
            //        dgvDeductees.Rows.Clear();
            //    return;
            //}
            ////--
            //if (cmbQuarter.Text == "")
            //{
            //    if (dgvDeductees.Rows.Count > 0)
            //        dgvDeductees.Rows.Clear();
            //    return;
            //}
            //--
            //lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
            //                                            cmbQuarter.Text,
            //                                            Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
            //                                            cmbFormNo.Text);
            ////--
            //if (cmbFormNo.Text == T_FormNo.F24Q)
            //    strEmpDed = "Employee";
            //else
            //    strEmpDed = "Deductee";
            ////--
            //if (lngBasicInfoID == 0)
            //{
            //    //if (dgvDeductees.Rows.Count > 0)
            //    //    dgvDeductees.Rows.Clear();
            //    return;
            //}
            ////--
            //LoadDeducteeGrid(lngBasicInfoID);
            //--
        }
        #endregion

        #region btnVerification_Click
        private void btnVerification_Click(object sender, EventArgs e)
        {
            if (btnVerification.Text == strbtnVerification)
            {
                if (ValidateFields() == false) return;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                {
                    if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
                    {
                        cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
                        return;
                    }
                }
                //--
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //
                btnVerification.Text = "Stop verifying";
                btnVerification.ForeColor = Color.Red;
                btnPrintInvalidPAN.Enabled = false;
                //grpMain.Enabled = false;
                //
                //BackgroundWorker bgwPANVerification = new Backg   roundWorker();
                //if(bgwPANVerification.IsBusy == true)
                    bgwPANVerification.RunWorkerAsync();
                //ClearFields();
            }
            else
            {
                bgwPANVerification.CancelAsync();
                bgwPANVerification.Dispose();
                //bgwPANVerification = null;
                //
                GC.Collect();
                this.Cursor = Cursors.Default;
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
                blnVerificationComplete = false;
                //grpMain.Enabled = true;
            }
        }
        #endregion

        //-- COMMENTED
        #region bgwPANVerification_DoWork
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
            //if (btnVerification.Text != strbtnVerification)
            //{
            //    //--
            //    strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
            //            "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
            //            "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
            //            "         MST_VERIFIED_PAN.VERIFIED_PAN_ID             AS STATUS " +
            //            "FROM    (TRN_DEDUCTEE_DETAILS INNER JOIN MST_" + strEmpDed + "  " +
            //            "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
            //            "         LEFT JOIN MST_VERIFIED_PAN " +
            //            "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN " +
            //            "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
            //            "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
            //            "ORDER BY " + strEmpDed + "_NAME," +
            //            "         " + strEmpDed + "_PAN";
            //    //--
            //    IDataReader drdPanVerify = null; 
            //    drdPanVerify = dmlService.J_ExecSqlReturnReader(strSQL);
            //    //--
            //    if (drdPanVerify == null)
            //        return;
            //    //--
            //    int i = 0;
            //    while (drdPanVerify.Read())
            //    {
            //        if (Convert.ToString(drdPanVerify["STATUS"]) != "")
            //        {
            //            dgvDeductees.Rows[i].Cells[3].Style.BackColor = Color.LawnGreen;
            //            dgvDeductees.Rows[i].Cells[3].ToolTipText = "Verified";
            //        }
            //        else
            //        {
            //            if (objTracesConnect.IsValidPAN(Convert.ToString(drdPanVerify["DEDUCTEE_PAN"])) == true)
            //            {
            //                dgvDeductees.Rows[i].Cells[3].Style.BackColor = Color.LawnGreen;
            //                dgvDeductees.Rows[i].Cells[3].ToolTipText = "Verified";
            //                strPAN = strPAN + "," + Convert.ToString(drdPanVerify["DEDUCTEE_PAN"]);
            //            }
            //            else
            //            {
            //                if (TdsMan.T_CheckInternetConnectivty() == false)
            //                {
            //                    dgvDeductees.Rows[i].Cells[3].Style.BackColor = Color.Silver;
            //                    dgvDeductees.Rows[i].Cells[3].ToolTipText = "Internet Connectivity not found";
            //                }
            //                else
            //                {
            //                    //--
            //                    dgvDeductees.Rows[i].Cells[3].Style.BackColor = Color.Red;
            //                    dgvDeductees.Rows[i].Cells[3].ToolTipText = "Invalid PAN";
            //                }
            //            }
            //        }
            //        //
            //        i = i + 1;
            //        //
            //        if (bgwPANVerification.CancellationPending)//checks for cancel request
            //        {
            //            break;
            //        }
            //    }
            //    drdPanVerify.Close();
            //    drdPanVerify.Dispose();
            //    //--
            //    string[] strPANVerification = strPAN.Split(',');
            //    for (int j= 1; j <= strPANVerification.Length - 1; j++)
            //    {
            //        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + strPANVerification[j] + "'") == false)
            //        {
            //            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN) VALUES('" + strPANVerification[j] + "')");
            //        }
            //    }
            //    blnVerificationComplete = true;
            //}
            ////--
            ////if (bgwPANVerification.CancellationPending)
            ////{
            ////    e.Cancel = true;
            ////    return;
            ////}
        //}
        #endregion
        //------------

        #region bgwPANVerification_DoWork
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            if (btnVerification.Text != strbtnVerification)
            {
                //--
                for (int i = j; i <= dgvDeductees.RowCount-1 ; i++)
                {
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dgvDeductees.Rows[i].Cells[intVerifyId].Value)) > 0)
                    {
                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";
                        //
                        lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
                    }
                    else if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified\n[Internet Connectivity not found]";
                        //
                        lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
                    }
                    else
                    {
                        if (objTracesConnect.IsValidPAN(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value)) == true)
                        {
                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";
                            //
                            lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
                            //--
                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                            {
                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN) VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                            }
                        }
                        else
                        {
                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";
                            //
                            lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                            //--
                            dmlService.J_BeginTransaction();
                            //
                            if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                            {
                                dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " + 
                                                     "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId-1].Value)) + "'," +
                                                     "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                            }
                            //
                            dmlService.J_Commit();
                            //--                
                            strPAN = strPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
                        }
                    }
                    //--
                    j = j + 1;
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    {
                        //--
                        strSQL = "UPDATE MST_SETUP SET TRIAL_PAN_VERIFY_RECORDS = TRIAL_PAN_VERIFY_RECORDS + 1 ";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
                        {
                            cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
                            return;
                        }
                        //--
                        if (j == 10)
                        {
                            //cmnService.J_UserMessage("Only 10 PANs can be verified per return using the Trial Version");
                            //--
                            TrnTrialMessageBox TrialMessageBox = new TrnTrialMessageBox();
                            TrialMessageBox.StartPosition = FormStartPosition.CenterScreen;
                            TrialMessageBox.lblMessage2.Text = "It supports verification of only " + TDSMAN.Classes.TDSMAN.T_MaxLRTimesConsoImport * TDSMAN.Classes.TDSMAN.T_MaxDeducteeDetailsCountTrialEditions + " PANs.";
                            TrialMessageBox.Show();
                            //
                            blnVerificationComplete = false;
                            break;
                        }
                    }
                    //--
                    if (bgwPANVerification.CancellationPending)//checks for cancel request
                    {
                        blnVerificationComplete = false;
                        break;
                    }
                    //
                    if (i > 18)
                        dgvDeductees.FirstDisplayedScrollingRowIndex = dgvDeductees.FirstDisplayedScrollingRowIndex + 1;
                }
                //
                blnVerificationComplete = true;
                
            }
            //--
            if (bgwPANVerification.CancellationPending)
            {
                e.Cancel = true;
                blnVerificationComplete = false;
                return;
            }
        }
        #endregion

        #region bgwPANVerification_RunWorkerCompleted
        private void bgwPANVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
            }
            //--
            //if(bgwPANVerification.CancellationPending==false)
            if (blnVerificationComplete == true)
            {
                cmnService.J_UserMessage("Verification completed");
                btnVerification.Enabled = false;
                btnVerification.BackColor = Color.LightGray;
                //grpMain.Enabled = true;
                //bgwPANVerification.CancelAsync();
                //bgwPANVerification.Dispose();
                //bgwPANVerification = null;
            }
            else
            {
                cmnService.J_UserMessage("Verification stopped", MessageBoxIcon.Exclamation);
                //bgwPANVerification.CancelAsync();
                //bgwPANVerification.Dispose();
                //bgwPANVerification = null;
            }
            //
            btnVerification.Text = strbtnVerification;
            btnVerification.ForeColor = Color.Black;
            blnVerificationComplete = false;
            //
            //--
            if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                btnPrintInvalidPAN.Enabled = true;
            else
                btnPrintInvalidPAN.Enabled = false;
            //--
        }
        #endregion

        #region bgwPANVerification_ProgressChanged
        private void bgwPANVerification_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //btnVerification.Text = btnVerification.Text + "(" + e.ProgressPercentage.ToString() + "%)";
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            blnVerificationComplete = false; 
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #region dgvDeductees_ColumnAdded
        private void dgvDeductees_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvDeductees.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        #endregion

        #region lblInvalidNo_TextChanged
        private void lblInvalidNo_TextChanged(object sender, EventArgs e)
        {
            //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
            //    btnPrintInvalidPAN.Enabled = true;
            //else
            //    btnPrintInvalidPAN.Enabled = false;
        }
        #endregion

        #region btnPrintInvalidPAN_Click
        private void btnPrintInvalidPAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (strPAN == "") { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print of the Invalid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                rptDialog.PrintInvalidPAN(lngBasicInfoID, blRegular.ToString().ToUpper(), "");
                //}
            }
            catch
            {
            }
        }
        #endregion

        #region btnPrintInvalidPAN_MouseMove
        private void btnPrintInvalidPAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnPrintInvalidPAN, "Print invalid PAN(s)"); 
        }
        #endregion
        
        #endregion

        #region User Define Functions

        #region PanVerify
        private void PanVerify(long BasicInfoID)
        {
            IDataReader drdPanVerify = null;
            try
            {
                strSQL = "SELECT   DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                         "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                         "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                         "         ''                                       AS STATUS " +
                         "FROM     TRN_DEDUCTEE_DETAILS," +
                         "         MST_" + strEmpDed + "  " +
                         "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_" + strEmpDed + "." + strEmpDed + "_ID " +
                         "AND      TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                         "ORDER BY " + strEmpDed + "_NAME," +
                         "         " + strEmpDed + "_PAN";
                //--
                drdPanVerify = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdPanVerify == null)
                    return;
                //--
                DataGridViewCellFormattingEventArgs e;//new DataGridViewCellFormattingEventArgs(3, 1, "", null, "");
                //--
                while (drdPanVerify.Read())
                {
                    if (objTracesConnect.IsValidPAN(Convert.ToString(drdPanVerify["DEDUCTEE_PAN"])) == true)
                    {
                        //if (dgvDeductees.Columns[3].DataPropertyName == "STATUS")
                        //{
                        //    dgvDeductees CellStyle.BackColor = Color.Green;
                        //}
                    }
                    else
                    {
                        //if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "STATUS")
                        //{
                        //    e.CellStyle.BackColor = Color.Red;
                        //}
                    }
                }
                drdPanVerify.Close();
                drdPanVerify.Dispose();
                //
                #region COMMENT
                //EXECUTE QUERY STRING FOR INCOME TAX AMOUNT CALCULATION 
                //ds = dmlService.J_ExecSqlReturnDataSet(strSQL);

                //if (ds == null) return;
                ////------------
                //for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
                //{
                //    strPAN = strPAN + "," + ds.Tables[0].Rows[i - 1]["PAN_NO"];
                //}
                //string[] strPANVerification = strPAN.Split(',');

                //Debug.Print(Convert.ToString(DateTime.Now));
                //label2.Text = Convert.ToString(DateTime.Now);
                //for (int j = 0; j <= strPANVerification.Length - 1; j++)
                //{
                //    if (objAccount.IsValidPAN(strPANVerification[j]) == true)
                //        Debug.Print(strPANVerification[j] + " - TRUE");
                //    else
                //        Debug.Print(strPANVerification[j] + " - FALSE");

                //    this.Refresh();
                //    label1.Text = Convert.ToString(j + 1) + "/ " + Convert.ToString(strPANVerification.Length);
                //}
                //label2.Text = label2.Text + "      " + Convert.ToString(DateTime.Now);
                //MessageBox.Show("COMPLETED !");
                //Debug.Print(Convert.ToString(DateTime.Now));
                //Debug.Print("COMPLETED !");
                #endregion
            }
            catch(Exception err)
            {
                
            }
        }

        #endregion

        #region LoadDeducteeGrid

        #region LoadDeducteeGrid()
        private void LoadDeducteeGrid()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {strEmpDed + " Name", "80", "S", "", "", "", "T"},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {"Status", "44", "S", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //            
                strSQL = "SELECT   DISTINCT MST_EMPLOYEE.EMPLOYEE_ID   AS DEDUCTEE_ID," +
                         "         MST_EMPLOYEE.EMPLOYEE_NAME AS DEDUCTEE_NAME," +
                         "         MST_EMPLOYEE.EMPLOYEE_PAN  AS DEDUCTEE_PAN," +
                         "         ''                         AS STATUS," +
                         "         ''                         AS VERIFIED " +
                         "FROM     TRN_DEDUCTEE_DETAILS," +
                         "         MST_EMPLOYEE  " +
                         "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                         "AND      1=2 " +
                         "ORDER BY EMPLOYEE_NAME," +
                         "         EMPLOYEE_PAN";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGrid(long BasicInfoID)
        private void LoadDeducteeGrid(long BasicInfoID)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {strEmpDed + " Name", "80", "S", "", "", "", "T"},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {"Status", "44", "S", "", "", "", ""},
                                        {"Verified", "0", "", "", "Right", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //           
                #region COMMENT
                //strSQL = "SELECT   DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                //         "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                //         "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                //         "         ''                                       AS STATUS " +
                //         "FROM     TRN_DEDUCTEE_DETAILS," +
                //         "         MST_" + strEmpDed + "  " +
                //         "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_" + strEmpDed + "." + strEmpDed + "_ID " +
                //         "AND      TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + BasicInfoID + " " +
                //         "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
                //         "ORDER BY " + strEmpDed + "_NAME," +
                //         "         " + strEmpDed + "_PAN";
                //---------------------------------------
                //strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                //        "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                //        "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                //        "         ''                                           AS STATUS," +
                //        "         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                //        "FROM    ((TRN_DEDUCTEE_DETAILS INNER JOIN MST_" + strEmpDed + "  " +
                //        "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                //        "         LEFT JOIN MST_VERIFIED_PAN " +
                //        "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN) " +
                //        "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                //        "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
                //        "ORDER BY " + strEmpDed + "_NAME," +
                //        "         " + strEmpDed + "_PAN";
                #endregion
                //
                if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q &&
                    TDSMAN.Classes.TDSMAN.T_pQuarter == T_Qtr.Q4)
                {
                    #region COMMENT
                    //strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                    //        "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                    //        "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                    //        "         ''                                           AS STATUS," +
                    //        "         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                    //        "FROM    (((MST_" + strEmpDed + "  " +
                    //        "         LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                    //        "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                    //        "         LEFT JOIN TRN_SALARY_DETAILS " +
                    //        "      ON TRN_SALARY_DETAILS.EMPLOYEE_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                    //        "         LEFT JOIN MST_VERIFIED_PAN " +
                    //        "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN) " +
                    //        "WHERE   (TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + "  " +
                    //        "      OR TRN_SALARY_DETAILS.BASIC_INFO_ID   = " + lngBasicInfoID + ")" +
                    //        "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
                    //        "ORDER BY " + strEmpDed + "_NAME," +
                    //        "         " + strEmpDed + "_PAN";
                    #endregion
                    //-- ANIK @ 2014-04-28
                    strSQL = @"SELECT DISTINCT MST_EMPLOYEE.EMPLOYEE_ID           AS EMPLOYEE_ID,         
                                      MST_EMPLOYEE.EMPLOYEE_NAME                   AS EMPLOYEE_NAME,         
                                      MST_EMPLOYEE.EMPLOYEE_PAN                    AS EMPLOYEE_PAN,         
                                      ''                                           AS STATUS,         
                                      IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED 
                              FROM    ((MST_EMPLOYEE           
                                        LEFT JOIN TRN_DEDUCTEE_DETAILS     
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                                        LEFT JOIN MST_VERIFIED_PAN       
                                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                              WHERE   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                              @"AND     MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL'
                              UNION
                              SELECT  DISTINCT MST_EMPLOYEE.EMPLOYEE_ID   AS EMPLOYEE_ID,         
                                      MST_EMPLOYEE.EMPLOYEE_NAME AS EMPLOYEE_NAME,         
                                      MST_EMPLOYEE.EMPLOYEE_PAN  AS EMPLOYEE_PAN,         
                                      ''                                           AS STATUS,         
                                      IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED 
                              FROM    ((MST_EMPLOYEE           
                                        LEFT JOIN TRN_SALARY_DETAILS       
                                        ON TRN_SALARY_DETAILS.EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                                        LEFT JOIN MST_VERIFIED_PAN      
                                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                              WHERE   TRN_SALARY_DETAILS.BASIC_INFO_ID   = " + lngBasicInfoID + " " +
                              @"AND      MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL' 
                              ORDER BY EMPLOYEE_NAME,        EMPLOYEE_PAN";
                }
                else
                    strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                            "         MST_" + strEmpDed + "." + strEmpDed + "_NAME AS DEDUCTEE_NAME," +
                            "         MST_" + strEmpDed + "." + strEmpDed + "_PAN  AS DEDUCTEE_PAN," +
                            "         ''                                           AS STATUS," +
                            "         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                            "FROM    ((TRN_DEDUCTEE_DETAILS " +
                            "         INNER JOIN MST_" + strEmpDed + "  " +
                            "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                            "         LEFT JOIN MST_VERIFIED_PAN " +
                            "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN) " +
                            "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                            "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
                            "ORDER BY " + strEmpDed + "_NAME," +
                            "         " + strEmpDed + "_PAN";

                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGridCorr(long BasicInfoID)
        private void LoadDeducteeGridCorr(long BasicInfoID)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{strEmpDed + " Name", "80", "S", "", "", "", "T"},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {"Status", "44", "S", "", "", "", ""},
                                        {"Verified", "0", "", "", "Right", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                strSQL = "SELECT  DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                        "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN           AS DEDUCTEE_PAN," +
                        "         ''                                              AS STATUS," +
                        "         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                        "FROM    (COR_TRN_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN) " +
                        "WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                        "AND      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN <> 'PANNOTAVBL' ";
                if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
                    strSQL = strSQL + "AND (COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
                        "OR      COR_TRN_DEDUCTEE_DETAILS.MODE IN ('A','O')) ";
                strSQL = strSQL + "UNION " +
                        "SELECT  DISTINCT COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME AS DEDUCTEE_NAME," + 
                        "        COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN  AS DEDUCTEE_PAN," +
                        "        ''                                     AS STATUS," +
                        "         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                        "FROM   (COR_TRN_SALARY_DETAILS " +
                        "        LEFT JOIN MST_VERIFIED_PAN " +
                        "               ON MST_VERIFIED_PAN.PAN = COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN) " +
                        "WHERE   COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                        "AND     COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN <> 'PANNOTAVBL' ";
                if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
                    strSQL = strSQL + "AND (COR_TRN_SALARY_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
                        "OR      COR_TRN_SALARY_DETAILS.MODE IN ('A','O')) ";
                strSQL = strSQL + "ORDER BY DEDUCTEE_NAME," +
                        "         DEDUCTEE_PAN ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
            }
            catch
            {

            }
        }
        #endregion

        #endregion

        #region ClearFields
        private void ClearFields()
        {
            lblInvalidNo.Text = "0";
            lblVerifiedNo.Text = "0";
            lblNotVerifiedNo.Text = "0"; 
            btnVerification.Enabled = true;
            btnVerification.BackColor = Color.Lavender;                
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            //if (cmbFinancialYear.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Financial Year - Cannot be blank");
            //    cmbFinancialYear.Select();
            //    return false;
            //}
            //if (cmbFormNo.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Form No. - Cannot be blank");
            //    cmbFormNo.Select();
            //    return false;
            //}
            //if (cmbQuarter.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Quarter - Cannot be blank");
            //    cmbQuarter.Select();
            //    return false;
            //}
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Company - Cannot be blank");
            //    cmbCompany.Select();
            //    return false;
            //}
            //if (lngBasicInfoID == 0)
            //{
            //    cmnService.J_UserMessage("No deductee records found");
            //    cmbCompany.Select();
            //    return false;
            //}
            ////
            //if (dgvDeductees.RowCount <= 0)
            //{
            //    cmnService.J_UserMessage("No deductee records found");
            //    cmbCompany.Select();
            //    return false;
            //}
            //
            //if (TdsMan.T_CheckInternetConnectivty() == false)
            //{
            //    cmnService.J_UserMessage("Internet Connectivity not found");
            //    cmbCompany.Select();
            //    return false;
            //}
            //--
            return true;
        }
        #endregion
        
        #endregion




    }

}