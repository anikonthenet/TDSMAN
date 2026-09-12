#region Refered Namespaces & Classes

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TDSMAN.Classes;
using TDSMAN.FormRpt;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnBulkPANVerificationTraces_New_Log : Form
    {
        #region System Generated Code
        public TrnBulkPANVerificationTraces_New_Log()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables declaration

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
        bool blnExit = true;
        //
        int intPANId = 0;
        int intStatusId = 0;
        int intVerifyId = 0;
        //
        int j = 0;
        //
        enum enmRequestType
        {
            Login,
            UploadCSVFile,
            CheckCSVStatus,
            GetTokenDetails,
            DownloadCSVFile,
            PanValidation,
            LogOff
        }
        //
        int intCSVStatusRetry = 0;
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        string filePathCSV = "", filePathCSVTokenNo = "", UploadedfilePath = "", strSaveDownloadedCSVFilePath = "", strLogTokenNo = "", strLogTokenNoDateTime = "";
        //
        private const uint NERR_Success = 0;

        [DllImport("netapi32.dll", SetLastError = false)]
        private static extern uint NetApiBufferFree(IntPtr Buffer);

        [DllImport("netapi32.dll", CharSet = CharSet.Unicode,
        SetLastError = false)]
        private static extern uint NetRemoteTOD(string
        UncServerName, ref IntPtr BufferPtr);

        [StructLayout(LayoutKind.Sequential)]
        private struct TIME_OF_DAY_INFO
        {
            public uint tod_elapsedt;
            public uint tod_msecs;
            public uint tod_hours;
            public uint tod_mins;
            public uint tod_secs;
            public uint tod_hunds;
            public uint tod_timezone;
            public uint tod_tinterval;
            public uint tod_day;
            public uint tod_month;
            public uint tod_year;
            public uint tod_weekday;
        }
        //
        private string CurrentCaptchaId = "";

        public class PANInfo
        {
            public string Name { get; set; }
            public string Status { get; set; }
            //public string AadhaarLinkDate { get; set; }
        }
        public class PANVerifyInfo
        {
            public string CheckedDate { get; set; }
            public int VerifiedStatus { get; set; }
        }
        #endregion

        #region User Defined Events

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnBulkPANVerificationTraces_Activated
        private void TrnBulkPANVerificationTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 21/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion
        //-----------------------------------------

        #region TrnBulkPANVerification_Load
        private void TrnBulkPANVerification_Load(object sender, EventArgs e)
        {
            string strFileDate = "2011";
            try
            {
                //lblDisclaimer.Text = "The PAN verification is from the database of Income Tax Department through their web gateways.";
                lblDisclaimer.Text = "The PAN verification is from the database of TRACES through their web gateways.";
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
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN;
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

                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                            SELECT DISTINCT DEDUCTEE_PAN , DEDUCTEE_NAME
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
                    strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                            SELECT DISTINCT EMPLOYEE_PAN, EMPLOYEE_NAME 
                            FROM ((COR_HDR_SALARY_DETAILS LEFT JOIN MST_VERIFIED_PAN
                            ON     COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN = MST_VERIFIED_PAN.PAN)
                                   INNER JOIN COR_HDR_BATCH
                            ON     COR_HDR_SALARY_DETAILS.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID)
                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                            AND    COR_HDR_SALARY_DETAILS.INVALID_PAN = 0
                            AND    COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')
                            AND    RIGHT(COR_HDR_BATCH.FILE_DATE, 4) > " + strFileDate + " ";
                    dmlService.J_ExecSql(strSQL);
                }
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
                //////-- CREATE THE CSV FOR UPLOADING TO TRACES...
                ////ExportGridToCSV(dgvDeductees);
                if (dmlService.J_IsDatabaseObjectExist("TRN_TRACES_PAN_VERIFICATION_LOG") == true)
                    strLogTokenNo = FetchTokenNo(lngBasicInfoID, TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES, 0, out strLogTokenNoDateTime);
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
            IDataReader drdShowTracesDetails = null;
            //
            try
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
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    //btnVerification.Text = "Stop verifying";
                    //btnVerification.ForeColor = Color.Red;
                    //btnPrintInvalidPAN.Enabled = false;
                    ////grpMain.Enabled = false;
                    ////
                    ////BackgroundWorker bgwPANVerification = new Backg   roundWorker();
                    ////if(bgwPANVerification.IsBusy == true)
                    //bgwPANVerification.RunWorkerAsync();
                    //ClearFields();
                    //
                    this.Cursor = Cursors.WaitCursor;
                    //
                    bool blnNeedTraces = false;
                    //
                    #region COMMENT
                    //try
                    //{
                    //    // Fetch all verified PANs once
                    //    DataTable dtPAN = dmlService.J_ExecSqlReturnDataTable("SELECT PAN, CHECKED_DATE_TIME FROM MST_VERIFIED_PAN");

                    //    HashSet<string> panSet = new HashSet<string>();

                    //    foreach (DataRow row in dtPAN.Rows)
                    //    {
                    //        panSet.Add(row["PAN"].ToString().Trim().ToUpper());
                    //        panSet.Add(row["CHECKED_DATE_TIME"].ToString().Trim().ToUpper());
                    //    }

                    //    // Check grid PANs
                    //    for (int i = 0; i < dgvDeductees.RowCount; i++)
                    //    {
                    //        string pan = Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value).Trim().ToUpper();

                    //        if (!string.IsNullOrEmpty(pan))
                    //        {
                    //            if (!panSet.Contains(pan))
                    //            {
                    //                blnNeedTraces = true;
                    //                break; // even 1 PAN is enough
                    //            }
                    //            else
                    //            {
                    //                // OPTIONAL: show existing status immediately
                    //                //dgvDeductees.Rows[i].Cells[intStatusId].Value 
                    //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Already Verified";
                    //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    this.Cursor = Cursors.Default;
                    //    cmnService.J_UserMessage("PAN check failed: " + ex.Message);
                    //    return;
                    //}
                    #endregion
                    //
                    Dictionary<string, PANVerifyInfo> panDict = new Dictionary<string, PANVerifyInfo>();
                    //
                    try
                    {
                        // Step 1: Fetch PAN + Checked Date
                        DataTable dtPAN = dmlService.J_ExecSqlReturnDataTable("SELECT PAN, CHECKED_DATE_TIME, VERIFIED_STATUS FROM MST_VERIFIED_PAN ORDER BY PAN");

                        // Step 2: Create Dictionary (PAN > Date)
                        //Dictionary<string, string> panDict = new Dictionary<string, string>();
                        foreach (DataRow row in dtPAN.Rows)
                        {
                            string pan = Convert.ToString(row["PAN"]).Trim().ToUpper();

                            string checkedDate = "";
                            if (row["CHECKED_DATE_TIME"] != DBNull.Value)
                            {
                                checkedDate = Convert.ToDateTime(row["CHECKED_DATE_TIME"])
                                                .ToString("dd/MM/yyyy");
                            }

                            int status = 0;
                            if (row["VERIFIED_STATUS"] != DBNull.Value)
                            {
                                int.TryParse(row["VERIFIED_STATUS"].ToString(), out status);
                            }

                            if (!string.IsNullOrEmpty(pan))
                            {
                                if (!panDict.ContainsKey(pan))
                                {
                                    panDict.Add(pan, new PANVerifyInfo
                                    {
                                        CheckedDate = checkedDate,
                                        VerifiedStatus = status
                                    });
                                }
                            }
                        }

                        //Step 3: Loop through grid
                        for (int i = 0; i < dgvDeductees.RowCount; i++)
                        {
                            string pan = Convert.ToString(
                                dgvDeductees.Rows[i].Cells[intPANId].Value)
                                .Trim().ToUpper();

                            if (!string.IsNullOrEmpty(pan))
                            {
                                if (!panDict.ContainsKey(pan))
                                {
                                    // PAN not found > need TRACES
                                    blnNeedTraces = true;
                                    //break;
                                }
                                else
                                {
                                    // PAN already verified
                                    var info = panDict[pan];

                                    string checkedDate = info.CheckedDate;
                                    string status = info.VerifiedStatus.ToString(); 

                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Already Verified";

                                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;

                                    dgvDeductees.Rows[i].Cells[intStatusId].Value = checkedDate;

                                    // Color based on VERIFIED_STATUS
                                    if (status == T_PANVerificationStatusId.VALID_OPERATIVE)
                                    {
                                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                                        lblVerifiedNo.Text = (Convert.ToInt64(lblVerifiedNo.Text) + 1).ToString();
                                    }
                                    else if (status == T_PANVerificationStatusId.VALID_INOPERATIVE)
                                    {
                                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
                                        lblInOperativeNo.Text = (Convert.ToInt64(lblInOperativeNo.Text) + 1).ToString();
                                        //-- 
                                        if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                        {
                                            dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "(NAME_DESC, PAN) " +
                                                                     "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
                                                                     "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                        }
                                    }
                                    else
                                    {
                                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                        lblInvalidNo.Text = (Convert.ToInt64(lblInvalidNo.Text) + 1).ToString();
                                        //
                                        if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                        {
                                            dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                        }
                                        //
                                    }
                                }
                            }
                        }
                        //--
                        if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                            btnPrintInvalidPAN.Enabled = true;
                        else
                            btnPrintInvalidPAN.Enabled = false;
                        //--
                        if (cmnService.J_ReturnInt32Value(lblInOperativeNo.Text) > 0)
                            btnPrintInoperativePAN.Enabled = true;
                        else
                            btnPrintInoperativePAN.Enabled = false;
                        //--
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("PAN check failed: " + ex.Message);
                        return;
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                    //-- CREATE THE CSV FOR UPLOADING TO TRACES...
                    if (strLogTokenNo == "")
                        ExportGridToCSV(dgvDeductees, panDict);
                    //--
                    //InitializeCaptcha();
                    if (blnNeedTraces == true)
                    {
                        picCaptcha.Image = Properties.Resources.captcha_loading;
                        if (!bgWorkerLoadCaptcha.IsBusy)
                            bgWorkerLoadCaptcha.RunWorkerAsync();
                        //
                        grpLoginDetails.Visible = true;
                        //
                        if (TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES != "")
                        {
                            //-- IF TAN DETAILS FOUND FROM DATABASE
                            strSQL = "SELECT TAN_ACCOUNT_ID," +
                             "       TAN_NO," +
                             "       LOGIN_ID," +
                             "       USER_PASSWORD," +
                             "       COMPANY_NAME " +
                             "FROM   MST_TAN_ACCOUNT " +
                             "WHERE  TAN_NO ='" + cmnService.J_ReplaceQuote(TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES) + "'";

                            drdShowTracesDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                            //--
                            if (drdShowTracesDetails == null)
                            {
                                txtTAN.Select();
                                blnExit = true;
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            //
                            while (drdShowTracesDetails.Read())
                            {
                                blnExit = false;
                                txtTAN.Text = Convert.ToString(drdShowTracesDetails["TAN_NO"]);
                                txtUserID.Text = Convert.ToString(drdShowTracesDetails["LOGIN_ID"]);
                                txtPassword.Text = Convert.ToString(drdShowTracesDetails["USER_PASSWORD"]);
                                blnExit = true;
                                //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                //                                        + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                                //                                        + drdShowDeducteeHelp["USER_PASSWORD"].ToString().PadRight(10)
                                //                                        + " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                //                                        Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));                            
                            }
                            //--
                            //-----------------------------------------------------------
                            drdShowTracesDetails.Close();
                            drdShowTracesDetails.Dispose();
                            //--
                            txtCaptchaCode.Select();
                        }
                        else
                            txtTAN.Select();
                        //--
                        this.Cursor = Cursors.Default;
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
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
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
        //            dgvDeductees.Rows[i].Cells[3].Style.BackColor = Color.Green;
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

        #region bgwPANVerification_DoWork - COMMENTED
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    if (btnVerification.Text != strbtnVerification)
        //    {
        //        //--
        //        for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
        //        {
        //            if(chkIgnore.Checked == true)// || cmnService.J_ReturnInt32Value(Convert.ToString(dgvDeductees.Rows[i].Cells[intVerifyId].Value)) == 0)
        //            {
        //                string strVerifiedNAME = "", strPANStatus = "";
        //                TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
        //                                                                                       out strVerifiedNAME, out strPANStatus);
        //                //--
        //                if (response.Respons == enmResponse.Success)
        //                {
        //                    //if (objTracesConnect.IsValidPAN(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value)) == true)
        //                    if (strVerifiedNAME.ToUpper() != strNOTAVAILABLE)
        //                    {
        //                        if (strPANStatus.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE.ToString() || strPANStatus.ToUpper() == T_PANVerificationStatus.ACTIVE.ToString())// "VALID AND OPERATIVE")
        //                        {
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
        //                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";
        //                            //
        //                            lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
        //                            //--
        //                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME) VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," + GetServerDateTime() + ")");
        //                            }
        //                            else
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_STATUS = " + T_PANVerificationStatusId.VALID_OPERATIVE + ", CHECKED_DATE_TIME = " + GetServerDateTime() + @"
        //                                    WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                        }
        //                        else
        //                        {
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
        //                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Inoperative";
        //                            //
        //                            lblInOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInOperativeNo.Text) + 1);
        //                            //--
        //                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME, VERIFIED_STATUS) VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," + GetServerDateTime() + "," + T_PANVerificationStatusId.VALID_INOPERATIVE + ")");
        //                            }
        //                            else
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET CHECKED_DATE_TIME = " + GetServerDateTime() + ", VERIFIED_STATUS = " + T_PANVerificationStatusId.VALID_INOPERATIVE + " " +
        //                                    " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                            //-- 2023/10/12
        //                            if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "(NAME_DESC, PAN) " +
        //                                                         "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
        //                                                         "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
        //                            }
        //                        }
        //                        ////--
        //                        //if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                        //{
        //                        //    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN) VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
        //                        //}
        //                    }
        //                    else if (strVerifiedNAME.ToUpper() == strNOTAVAILABLE)
        //                    {
        //                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";
        //                        //
        //                        lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
        //                        //--
        //                        dmlService.J_BeginTransaction();
        //                        //
        //                        if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
        //                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
        //                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
        //                        }
        //                        //-- 2023/10/30
        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME, VERIFIED_STATUS) VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," + GetServerDateTime() + "," + T_PANVerificationStatusId.INVALID + ")");
        //                        }
        //                        else
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET CHECKED_DATE_TIME = " + GetServerDateTime() + ", VERIFIED_STATUS = " + T_PANVerificationStatusId.INVALID + " " +
        //                                " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                        //
        //                        dmlService.J_Commit();
        //                        //--                
        //                        strPAN = strPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                    }
        //                }
        //            }
        //            else if (cmnService.J_ReturnInt32Value(Convert.ToString(dgvDeductees.Rows[i].Cells[intVerifyId].Value)) > 0 )
        //            {
        //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
        //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";
        //                //
        //                lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
        //            }
        //            else if (TdsMan.T_CheckInternetConnectivty() == false)
        //            {
        //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
        //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified\n[Internet Connectivity not found]";
        //                //
        //                lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
        //            }
        //            //--
        //            j = j + 1;
        //            //--
        //            if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
        //            {
        //                //--
        //                strSQL = "UPDATE MST_SETUP SET TRIAL_PAN_VERIFY_RECORDS = TRIAL_PAN_VERIFY_RECORDS + 1 ";
        //                dmlService.J_ExecSql(strSQL);
        //                //--
        //                if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
        //                {
        //                    cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
        //                    return;
        //                }
        //                //--
        //                if (j == 10)
        //                {
        //                    cmnService.J_UserMessage("Only 10 PANs can be verified per return using the Trial Version");
        //                    blnVerificationComplete = false;
        //                    break;
        //                }
        //            }
        //            //--
        //            if (bgwPANVerification.CancellationPending)//checks for cancel request
        //            {
        //                blnVerificationComplete = false;
        //                break;
        //            }
        //            //
        //            if (i > 18)
        //                dgvDeductees.FirstDisplayedScrollingRowIndex = dgvDeductees.FirstDisplayedScrollingRowIndex + 1;
        //        }
        //        //
        //        blnVerificationComplete = true;
        //        //
        //    }
        //    //--
        //    if (bgwPANVerification.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        blnVerificationComplete = false;
        //        return;
        //    }
        //}
        #endregion

        #region bgwPANVerification_DoWork
        //
        #region COMMENTED
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    //  LOAD CSV ONCE
        //    string csvPath = strSaveDownloadedCSVFilePath;// @"D:\PAN_Data.csv"; // <-- change path
        //    Dictionary<string, string> panStatusDict = ReadPANStatus(csvPath);

        //    if (btnVerification.Text != strbtnVerification)
        //    {
        //        for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
        //        {
        //            if (chkIgnore.Checked == true)
        //            {
        //                string strVerifiedNAME = "";
        //                string strPANStatus = "";

        //                string pan = Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value)
        //                                .Trim().ToUpper();

        //                // REPLACE API CALL WITH DICTIONARY
        //                if (panStatusDict.ContainsKey(pan))
        //                {
        //                    strPANStatus = panStatusDict[pan];

        //                    if (strPANStatus.ToUpper().Contains("VALID"))
        //                        strPANStatus = T_PANVerificationStatus.VALID_OPERATIVE.ToString();
        //                    else if (strPANStatus.ToUpper().Contains("INOPERATIVE"))
        //                        strPANStatus = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();
        //                    else
        //                        strPANStatus = T_PANVerificationStatus.INVALID.ToString();

        //                    strVerifiedNAME = "AVAILABLE";
        //                }
        //                else
        //                {
        //                    strVerifiedNAME = strNOTAVAILABLE;
        //                }

        //                // KEEP YOUR EXISTING LOGIC SAME
        //                if (strVerifiedNAME.ToUpper() != strNOTAVAILABLE)
        //                {
        //                    if (strPANStatus.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE.ToString()
        //                        || strPANStatus.ToUpper() == T_PANVerificationStatus.ACTIVE.ToString())
        //                    {
        //                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
        //                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";

        //                        lblVerifiedNo.Text = Convert.ToString(
        //                            cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);

        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME) VALUES('"
        //                                + pan + "'," + GetServerDateTime() + ")");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_STATUS = "
        //                                + T_PANVerificationStatusId.VALID_OPERATIVE + ", CHECKED_DATE_TIME = "
        //                                + GetServerDateTime() + " WHERE PAN ='" + pan + "'");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
        //                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Inoperative";

        //                        lblInOperativeNo.Text = Convert.ToString(
        //                            cmnService.J_ReturnInt64Value(lblInOperativeNo.Text) + 1);

        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME, VERIFIED_STATUS) VALUES('"
        //                                + pan + "'," + GetServerDateTime() + "," + T_PANVerificationStatusId.VALID_INOPERATIVE + ")");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET CHECKED_DATE_TIME = "
        //                                + GetServerDateTime() + ", VERIFIED_STATUS = "
        //                                + T_PANVerificationStatusId.VALID_INOPERATIVE + " WHERE PAN ='" + pan + "'");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";

        //                    lblInvalidNo.Text = Convert.ToString(
        //                        cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);

        //                    dmlService.J_BeginTransaction();

        //                    if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
        //                    {
        //                        dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, CHECKED_DATE_TIME, VERIFIED_STATUS) VALUES('"
        //                            + pan + "'," + GetServerDateTime() + "," + T_PANVerificationStatusId.INVALID + ")");
        //                    }
        //                    else
        //                    {
        //                        dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET CHECKED_DATE_TIME = "
        //                            + GetServerDateTime() + ", VERIFIED_STATUS = "
        //                            + T_PANVerificationStatusId.INVALID + " WHERE PAN ='" + pan + "'");
        //                    }

        //                    dmlService.J_Commit();
        //                }
        //            }

        //            j = j + 1;

        //            if (bgwPANVerification.CancellationPending)
        //            {
        //                blnVerificationComplete = false;
        //                break;
        //            }

        //            if (i > 18)
        //                dgvDeductees.FirstDisplayedScrollingRowIndex++;
        //        }

        //        blnVerificationComplete = true;
        //    }

        //    if (bgwPANVerification.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        blnVerificationComplete = false;
        //    }
        //}
        #endregion
        //
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;

            string csvPath = strSaveDownloadedCSVFilePath;
            //var panStatusDict = ReadPANStatus(csvPath);
            var panStatusDict = ReadPANNameStatus(csvPath);

            // BULK COLLECTION
            DataTable dtBulk = new DataTable();
            dtBulk.Columns.Add("PAN");
            dtBulk.Columns.Add("NAME");
            dtBulk.Columns.Add("STATUS", typeof(int));

            dgvDeductees.SuspendLayout();   // UI performance boost

            if (btnVerification.Text != strbtnVerification)
            {
                for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
                {
                    if (chkIgnore.Checked)
                    {
                        string pan = Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value)
                                        .Trim().ToUpper();
                        //
                        string statusRaw = "";
                        string statusMapped = "", nameFromCSV= "";
                        //int statusId = 0; 
                        string statusId = "";
                        //
                        if (panStatusDict.ContainsKey(pan))
                        {
                            //statusRaw = panStatusDict[pan];
                            var panInfo = panStatusDict[pan];

                            statusRaw = panInfo.Status;
                            nameFromCSV = panInfo.Name;
                            //--
                            if (statusRaw.Contains("INOPERATIVE"))
                            {
                                statusMapped = "Inoperative";
                                statusId = T_PANVerificationStatusId.VALID_INOPERATIVE.ToString();

                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Inoperative";

                                lblInOperativeNo.Text = (Convert.ToInt64(lblInOperativeNo.Text) + 1).ToString();
                                //-- 
                                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "(NAME_DESC, PAN) " +
                                                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
                                                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                }
                            }
                            else if (statusRaw == "VALID")
                            {
                                statusMapped = "Verified";
                                statusId = T_PANVerificationStatusId.VALID_OPERATIVE.ToString();

                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";

                                lblVerifiedNo.Text = (Convert.ToInt64(lblVerifiedNo.Text) + 1).ToString();

                            }
                            else if (statusRaw.Contains("VALID") && statusRaw.Contains("OPERATIVE"))
                            {
                                statusMapped = "Verified";
                                statusId = T_PANVerificationStatusId.VALID_OPERATIVE.ToString();

                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";

                                lblVerifiedNo.Text = (Convert.ToInt64(lblVerifiedNo.Text) + 1).ToString();

                            }
                            else if (statusRaw.Contains("INVALID"))
                            {
                                statusMapped = "Invalid";
                                statusId = T_PANVerificationStatusId.INVALID.ToString();

                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";

                                lblInvalidNo.Text = (Convert.ToInt64(lblInvalidNo.Text) + 1).ToString();

                                strPAN = strPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
                                //
                                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
                                                         "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId - 1].Value)) + "'," +
                                                         "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                }
                                //
                            }

                            // ADD TO BULK TABLE
                            dtBulk.Rows.Add(pan, nameFromCSV, statusId);
                        }
                        else
                        {
                            if (dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor != Color.Green)
                            {
                                statusMapped = "Invalid";
                                statusId = T_PANVerificationStatusId.INVALID.ToString();

                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";

                                lblInvalidNo.Text = (Convert.ToInt64(lblInvalidNo.Text) + 1).ToString();
                            }
                        }

                        //// ADD TO BULK TABLE
                        //dtBulk.Rows.Add(pan, statusId);
                    }

                    j++;

                    if (bgwPANVerification.CancellationPending)
                    {
                        blnVerificationComplete = false;
                        break;
                    }
                }

                blnVerificationComplete = true;
            }

            dgvDeductees.ResumeLayout();

            // BULK DB UPDATE (ONE SHOT)
            BulkUpdatePANStatus(dtBulk);

            if (bgwPANVerification.CancellationPending)
            {
                e.Cancel = true;
                blnVerificationComplete = false;
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
            if (File.Exists(strSaveDownloadedCSVFilePath))
            {
                File.Delete(strSaveDownloadedCSVFilePath);
            }
            //
            if (File.Exists(filePathCSV))
            {
                File.Delete(filePathCSV);
            }
            //
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
            if (cmnService.J_ReturnInt32Value(lblInOperativeNo.Text) > 0)
                btnPrintInoperativePAN.Enabled = true;
            else
                btnPrintInoperativePAN.Enabled = false;
            //--

        }
        #endregion

        #region bgwPANVerification_ProgressChanged
        //private void bgwPANVerification_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    //btnVerification.Text = btnVerification.Text + "(" + e.ProgressPercentage.ToString() + "%)";
        //}
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
                //if (cmnService.J_ReturnInt32Value(lblInOperativeNo.Text) == 0) { cmnService.J_UserMessage("No Inoperative PAN to print!!"); return; }
                //
                //if (strPAN == "") { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
                if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) == 0) { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print of the Invalid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                //rptDialog.PrintInvalidPAN(lngBasicInfoID, blRegular.ToString().ToUpper(), "");
                rptDialog.PrintInvalidPAN_RDLC(lngBasicInfoID, blRegular.ToString().ToUpper(), "");
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
            tllTip.SetToolTip(btnPrintInvalidPAN, "Print Invalid PAN(s)");
        }
        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }

        #endregion


        #region txtTANNo_KeyPress
        private void txtTANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
                else
                    SendKeys.Send("{tab}");
            }
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
            }
            else
                if (TdsMan.gTANNoPANNoValidation(txtTAN, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }

        #endregion

        #region txtTAN_KeyDown
        private void txtTAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstDeducteeHelp.Visible == true)
                {
                    lstDeducteeHelp.Focus();
                    lstDeducteeHelp.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region txtTAN_TextChanged
        private void txtTAN_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (blnExit == false)
                    return;
                //                
                if (txtTAN.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                //if (blnShowHelp == false)
                //    return;
                //-----------------------
                strSQL = "SELECT TAN_ACCOUNT_ID," +
                         "       TAN_NO," +
                         "       LOGIN_ID," +
                         "       USER_PASSWORD," +
                         "       COMPANY_NAME " +
                         "FROM   MST_TAN_ACCOUNT " +
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "%' " +
                         "ORDER BY TAN_NO, TAN_ACCOUNT_ID";

                drdShowDeducteeHelp = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowDeducteeHelp == null)
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }
                else
                {
                    lstDeducteeHelp.Items.Clear();
                    lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                                                + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10)
                                                                + " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));
                        //--
                        if (lstDeducteeHelp.Height <= 300)
                            lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
                    }
                    //--
                    if (lstDeducteeHelp.Items.Count <= 0)
                        lstDeducteeHelp.Visible = false;
                }
                //-----------------------------------------------------------
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                //-----------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                drdShowDeducteeHelp.Close();
                drdShowDeducteeHelp.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
            }
            //-----------------------
        }
        #endregion

        #region txtTAN_Leave
        private void txtTAN_Leave(object sender, EventArgs e)
        {
            if (txtTAN.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region txtUserId_Enter
        private void txtUserId_Enter(object sender, EventArgs e)
        {
            //DISABLE LIST VIEW WHEN CURSOR ENTERS ANY OTHER CONTROL
            lstDeducteeHelp.Visible = false;
        }
        #endregion

        #region lstDeducteeHelp_KeyPress
        private void lstDeducteeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                lstDeducteeHelp_Click(sender, e);
            else if (Convert.ToInt64(e.KeyChar) == 27)
            {
                lstDeducteeHelp.Visible = false;
                txtTAN.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //--
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            grpLoginDetails.Visible = false;
            //txtPAN.Select();
            //--

        }
        #endregion

        #region btnLogging_Click
        private void btnLogging_Click(object sender, EventArgs e)
        {
            try
            {
                //if (btnLogging.Text == strLogOff)
                //{
                //    ShowHideLoginDetails(enmRequestType.LogOff);
                //    return;
                //}
                pBar.Value = 0;
                //--
                #region VALIDATE LOGIN DETAILS
                if (txtTAN.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter TAN");
                    txtTAN.Select();
                    return;
                }
                //
                //if (txtUserID.Text.Trim() == "")
                //{
                //    cmnService.J_UserMessage("Enter User ID");
                //    txtUserID.Select();
                //    return;
                //}
                //
                if (txtPassword.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter Password");
                    txtPassword.Select();
                    return;
                }
                //
                if (txtCaptchaCode.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter Captcha Code");
                    txtCaptchaCode.Select();
                    return;
                }
                //
                #endregion
                //--
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    //BtnExit.Select();
                    return;
                }
                //--
                //if (!ValidateFields()) return;
                //--
                //############ NOW ADDING THE USER ID AND PASSWORD IN MASTER
                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTAN.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                blnVerificationComplete = false;
                //-------------------------------------------
                btnVerification.Text = "Stop verifying";
                btnVerification.ForeColor = Color.Red;
                btnPrintInvalidPAN.Enabled = false;
                btnPrintInoperativePAN.Enabled = false;
                //--------------------------------------------
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);
                //--
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return;
            }

        }
        #endregion

        #region bgwPANVerification_RunWorkerCompleted
        //private void bgwPANVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {

        //        if (e.Cancelled)
        //        {
        //            //btnVerification.Text = strbtnVerification;
        //            //btnVerification.ForeColor = Color.Black;
        //            blnVerificationComplete = false;
        //        }
        //        //--
        //        //if(bgwPANVerification.CancellationPending==false)
        //        if (blnVerificationComplete == true)
        //        {
        //            //cmnService.J_UserMessage("Validation completed");
        //            //btnVerification.Enabled = true;
        //            //btnVerification.BackColor = Color.Lavender;
        //            ////
        //            ////--
        //            //grpStatus.Enabled = true;
        //            ////
        //            //btnVerification.Enabled = false;
        //            ////
        //            //dgvDeductees.Enabled = true;
        //            //grpReturnSelection.Enabled = true;
        //            //grpRegularReturn.Enabled = true;
        //            //grpCorrectionReturn.Enabled = true;
        //            //j = 0;
        //            //                
        //        }
        //        else
        //        {
        //            //cmnService.J_UserMessage("Validation stopped", MessageBoxIcon.Exclamation);
        //            blnVerificationComplete = false;
        //            //grpReturnSelection.Enabled = true;
        //            //grpRegularReturn.Enabled = true;
        //            //grpCorrectionReturn.Enabled = true;
        //            //bgwPANVerification.CancelAsync();
        //            //bgwPANVerification.Dispose();
        //            //bgwPANVerification = null;
        //        }
        //        //
        //        //btnVerification.Text = strbtnVerification;
        //        //btnVerification.ForeColor = Color.Black;
        //        ////blnVerificationComplete = false;
        //        ////
        //        ////--
        //        //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
        //        //    btnPrintInvalidPAN.Enabled = true;
        //        //else
        //        //    btnPrintInvalidPAN.Enabled = false;
        //        ////--
        //        //if (cmnService.J_ReturnInt32Value(lblUnmatchedNo.Text) > 0)
        //        //    btnPrintUnmatched.Enabled = true;
        //        //else
        //        //    btnPrintUnmatched.Enabled = false;
        //        //--            
        //    }
        //    catch (Exception err)
        //    {
        //        blnVerificationComplete = false;
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}
        #endregion

        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
         {
            try
            {
                ArrayList objList = (ArrayList)e.Argument;
                ArrayList objRetval = new ArrayList();
                //-------------------------------------------------------
                enmRequestType enReqType = (enmRequestType)objList[0];
                TracesResponse objResponse = new TracesResponse();
                //-------------------------------------------------------
                switch (enReqType)
                {
                    // LOGIN REQUEST
                    case enmRequestType.Login:
                        //objResponse = objTracesConnect.makeLoginToTRACES((TracesLogin)objList[1]);
                        objResponse = objTracesConnect.makeLoginToTraces_New((TracesLogin)objList[1]);
                        //---------------------------------------------------
                        objRetval.Add(enmRequestType.Login);
                        objRetval.Add(objResponse);
                        //---------------------------------------------------
                        e.Result = objRetval;
                        //---------------------------------------------------
                        break;
                    case enmRequestType.UploadCSVFile:
                        objResponse = objTracesConnect.UploadCSVPANFile(filePathCSV, out filePathCSVTokenNo, out UploadedfilePath);
                        objRetval.Add(enmRequestType.UploadCSVFile);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        System.Threading.Thread.Sleep(4000);
                        break;
                    case enmRequestType.GetTokenDetails:
                        //DataTable dtTokens = objTracesConnect.GetPANVerificationTokens(3);
                        string status = objTracesConnect.CheckPANVerificationTokenStatus(strLogTokenNo);
                        //objRetval.Add(enmRequestType.GetTokenDetails);
                        //objRetval.Add(objResponse);
                        //e.Result = objRetval;
                        objRetval.Add(enmRequestType.GetTokenDetails);
                        objRetval.Add(objResponse);
                        objRetval.Add(status);
                        e.Result = objRetval;
                        break;
                    case enmRequestType.CheckCSVStatus:
                        bool blnReady = objTracesConnect.IsPANFileReady(filePathCSVTokenNo);
                        if (blnReady)
                        {
                            objResponse.Respons = enmResponse.Success;
                        }
                        else
                        {
                            objResponse.Respons = enmResponse.Failed;
                        }
                        objRetval.Add(enmRequestType.CheckCSVStatus);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        break;
                    case enmRequestType.DownloadCSVFile:
                        strSaveDownloadedCSVFilePath = Path.Combine(Application.StartupPath, "PAN_Data_" + filePathCSVTokenNo + ".csv");

                        objResponse = objTracesConnect.DownloadCSVPANResult(filePathCSVTokenNo, Application.StartupPath);
                        objRetval.Add(enmRequestType.DownloadCSVFile);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        break;
                    // LIST OF STATEMENT STATUS FILES
                    case enmRequestType.PanValidation:
                        bool bnlSuccess = false;
                        string strPAN = "";
                        DataGridViewRowCollection rowcoll = (DataGridViewRowCollection)objList[1];

                        foreach (DataGridViewRow row in rowcoll)
                        {
                            strPAN = row.Cells[0].Value.ToString();
                            TracesResponse response = objTracesConnect.RequestForPANValidation(strPAN);

                            if (response.Respons == enmResponse.Success)
                            {
                                bnlSuccess = true;
                                PANDetails objDetails = (PANDetails)response.CustomeTypes;
                                this.bgWorker.ReportProgress(0, objDetails);

                            }

                            if (response.Respons == enmResponse.SessionTimeout)
                            {
                                objResponse.Respons = enmResponse.SessionTimeout;
                                objRetval.Add(enmRequestType.PanValidation);
                                objRetval.Add(objResponse);
                                e.Result = objRetval;
                                break;
                            }
                        }

                        if (bnlSuccess)
                        {
                            objResponse = new TracesResponse();

                            objResponse.Respons = enmResponse.Success;
                            objRetval.Add(enmRequestType.PanValidation);
                            objRetval.Add(objResponse);
                            e.Result = objRetval;
                        }
                        break;
                    //REQUEST FOR LOG OFF
                    case enmRequestType.LogOff:
                        objResponse = objTracesConnect.Logoff();
                        objRetval.Add(enmRequestType.LogOff);
                        objRetval.Add(objResponse);
                        e.Result = objRetval;
                        break;
                }

            }
            catch (Exception err)
            {
                //pnlTracesMessage.Visible = false;
                blnVerificationComplete = false;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region bgWorker_RunWorkerCompleted
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList objMessage = (ArrayList)e.Result;
                ArrayList objList = new ArrayList();
                enmRequestType enmReqType = (enmRequestType)objMessage[0];
                TracesResponse objResponse = (TracesResponse)objMessage[1];
                //---------------------------------------------------------
                switch (enmReqType)
                {
                    case enmRequestType.Login:
                        //pgTimer.Stop();
                        //pgTimer.Interval = 1000;
                        //pBar.Value = 99;
                        //---------------------------------------------------
                        pnlTracesMessage.Visible = false;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.PanValidation);
                            //grdPANValidate.DataSource = objBindingSource;
                            //objList.Add(enmRequestType.UploadCSVFile);
                            if (strLogTokenNo == "")
                                objList.Add(enmRequestType.UploadCSVFile);
                            else
                                objList.Add(enmRequestType.GetTokenDetails);
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            cmnService.J_UserMessage(objResponse.Message);
                            InitializeCaptcha();
                            return;
                        }
                        else
                        {
                            //---------------------------------------------------
                            pBar.Value = 0;
                            pgTimer.Stop();
                            //---------------------------------------------------
                        }
                        break;

                    case enmRequestType.UploadCSVFile:
                        //ArrayList objList = new ArrayList();
                        pnlTracesMessage.Visible = false;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //System.Threading.Thread.Sleep(2000);
                            //objList.Add(enmRequestType.DownloadCSVFile); 
                            //SaveLogData(lngBasicInfoID, txtTANNo.Text, 0, filePathCSVTokenNo, dgvDeductees.RowCount);
                            SaveLogData(lngBasicInfoID, TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES, 0, filePathCSVTokenNo, dgvDeductees.RowCount);
                            strLogTokenNo = FetchTokenNo(lngBasicInfoID, TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES, 0, out strLogTokenNoDateTime);
                            //lblStatus.Visible = true;
                            //lblStatus.Text = "CSV file uploaded...";
                            intCSVStatusRetry = 0;
                            objList.Add(enmRequestType.CheckCSVStatus);
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        else
                        {
                            btnVerification.Text = strbtnVerification;
                            btnVerification.ForeColor = Color.Black;
                            txtCaptchaCode.Text = "";
                            //if (File.Exists(filePathCSV))
                            //{
                            //    File.Delete(filePathCSV);
                            //}
                            //cmnService.J_UserMessage("Failed - Upload");
                            if (filePathCSVTokenNo != "")
                            {
                                //cmnService.J_UserMessage("We have uploaded " + dgvDeductees.RowCount + " PAN(s) for verification to TRACES, which is still pending at their end vide Token No. " + filePathCSVTokenNo + "." +
                                //                     "\nPlease check back after 15 minutes or earlier.");
                                cmnService.J_UserMessage(dgvDeductees.RowCount + " PAN(s) have been uploaded successfully to TRACES for verification vide Token No. " + filePathCSVTokenNo + "." +
                                                     "\nThe validated response is currently pending at the TRACES end due to high processing load." +
                                                     "\nPlease check back after sometime.");
                                btnVerification.Enabled = true;
                                btnVerification.BackColor = Color.Lavender;
                                //
                                //--
                                grpStatus.Enabled = true;
                                //
                                btnVerification.Enabled = false;
                                //
                                dgvDeductees.Enabled = true;
                                //grpReturnSelection.Enabled = true;
                                //grpRegularReturn.Enabled = true;
                                //grpCorrectionReturn.Enabled = true;
                            }
                            else
                                cmnService.J_UserMessage("Data parsing to TRACES failed.\nPlease try later.");
                            //cmnService.J_UserMessage("Data parsing to TRACES failed.\nPlease try later.");
                        }
                        break;

                    case enmRequestType.GetTokenDetails:
                        #region COMMENTED
                        //DataTable dtTokens = (DataTable)objMessage[2];
                        //lstTRACESDownloadAvailableList.Items.Clear();
                        ////
                        //if (dtTokens != null && dtTokens.Rows.Count > 0)
                        //{
                        //    foreach (DataRow row in dtTokens.Rows)
                        //    {
                        //        lstTRACESDownloadAvailableList.Items.Add(
                        //            row["TOKENNO"].ToString() +
                        //            " | " +
                        //            row["SUBMITTEDDATE"].ToString());
                        //    }
                        //    pnlTracesMessage.Visible = false;
                        //    grpTRACESDownloadAvailableList.Visible = true;
                        //}
                        //else
                        //{
                        //    grpTRACESDownloadAvailableList.Visible = false;
                        //    cmnService.J_UserMessage("No downloadable PAN verification tokens found.");
                        //}
                        #endregion
                        //
                        string tokenStatus = Convert.ToString(objMessage[2]);
                        //
                        if (tokenStatus == "AVAILABLE")
                        {
                            cmnService.J_UserMessage("Token No. " + strLogTokenNo + " (" + DateTime.Parse(strLogTokenNoDateTime).ToString("dd/MM/yyyy HH:mm") + ") with " + dgvDeductees.RowCount + " PAN(s) is now processed by TRACES." +
                                                     "\nClick Ok to proceed.");
                            objList.Add(enmRequestType.DownloadCSVFile);

                            filePathCSVTokenNo = strLogTokenNo;
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        else if (tokenStatus == "PENDING")
                        {
                            pnlTracesMessage.Visible = false;
                            //Token No. XXXXXXXXX(dd / mm / yyyy – hh: mm) with XXX PANs is still pending with TRACES. Please check back after sometime.
                            cmnService.J_UserMessage("Token No. " + strLogTokenNo + " (" + DateTime.Parse(strLogTokenNoDateTime).ToString("dd/MM/yyyy HH:mm") + ") with " + dgvDeductees.RowCount + " PAN(s) is still pending with TRACES." +
                                                     "\nPlease check back after sometime.");
                            btnVerification.Enabled = true;
                            btnVerification.BackColor = Color.Lavender;
                            //
                            //--
                            grpStatus.Enabled = true;
                            //
                            btnVerification.Enabled = false;
                            //
                            dgvDeductees.Enabled = true;
                            //grpReturnSelection.Enabled = true;
                            //grpRegularReturn.Enabled = true;
                            //grpCorrectionReturn.Enabled = true;
                            return;
                        }
                        break;
                    case enmRequestType.CheckCSVStatus:
                        //lblStatus.Visible = true;
                        //lblStatus.Text = "Token No. " + filePathCSVTokenNo;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            objList.Add(enmRequestType.DownloadCSVFile);

                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        else
                        {
                            intCSVStatusRetry++;
                            //lblStatus.Visible = true;
                            //lblStatus.Text = "Trying Token No. " + filePathCSVTokenNo + "... " + intCSVStatusRetry + " times....";
                            //
                            //if (intCSVStatusRetry <= 100)
                            if (intCSVStatusRetry <= 5)
                            {
                                //System.Threading.Thread.Sleep(20000);
                                System.Threading.Thread.Sleep(2000);
                                objList.Add(enmRequestType.CheckCSVStatus);
                                if (!bgWorker.IsBusy)
                                    bgWorker.RunWorkerAsync(objList);
                            }
                            else
                            {
                                btnVerification.Text = strbtnVerification;
                                btnVerification.ForeColor = Color.Black;
                                //
                                pnlTracesMessage.Visible = false; //lblStatus.Visible = false;
                                //
                                //cmnService.J_UserMessage("TRACES is still processing the PAN file.\nPlease retry after some time.");
                                //cmnService.J_UserMessage("We have uploaded " + dgvDeductees.RowCount + " PAN(s) for verification to TRACES, which is still pending at their end vide Token No. " + filePathCSVTokenNo + "." +
                                //                     "\nPlease check back after 15 minutes or earlier.");
                                cmnService.J_UserMessage(dgvDeductees.RowCount + " PAN(s) have been uploaded successfully to TRACES for verification vide Token No. " + filePathCSVTokenNo + "." +
                                                     "\nThe validated response is currently pending at the TRACES end due to high processing load." +
                                                     "\nPlease check back after sometime.");

                                return;
                            }
                        }
                        break;
                    case enmRequestType.DownloadCSVFile:
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //objList.Add(enmRequestType.ExtractZIPFile);
                            //-------------------------------------------
                            //if (!bgWorker.IsBusy)
                            //    bgWorker.RunWorkerAsync(objList);
                            //cmnService.J_UserMessage("Downloaded");
                            //
                            UpdateLogTokenNO(lngBasicInfoID, filePathCSVTokenNo, 0);
                            //
                            if (!bgwPANVerification.IsBusy)
                                bgwPANVerification.RunWorkerAsync();
                        }
                        else
                        {
                            btnVerification.Text = strbtnVerification;
                            btnVerification.ForeColor = Color.Black;
                            txtCaptchaCode.Text = "";
                            //if (File.Exists(filePathCSV))
                            //{
                            //    File.Delete(filePathCSV);
                            //}
                            //cmnService.J_UserMessage("Failed - Download");
                            cmnService.J_UserMessage("Error encountered at TRACES.\nPlease try later.");
                        }
                        break;
                    case enmRequestType.PanValidation:

                        this.pgTimer.Stop();
                        this.pgTimer.Interval = 1000;
                        pBar.Value = 100;

                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.Login);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Success)
                        {

                            //PANDetails objDetails = (PANDetails)objResponse.CustomeTypes;

                            //// grdPANValidate.Rows.Add(objDetails.Name, objDetails.PAN, objDetails.Status);

                            //DataGridViewRow PANrow = new DataGridViewRow();
                            //PANrow.CreateCells(grdPANValidate);

                            //PANrow.Cells[0].Value = objDetails.Name;
                            //PANrow.Cells[1].Value = objDetails.PAN;
                            //PANrow.Cells[2].Value = objDetails.Status;

                            //grdPANValidate.Rows.Add(PANrow);
                        }
                        break;
                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTAN.Text = "";
                        txtCaptchaCode.Text = "";
                        //grpDownloadList.Visible = false;
                        grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        //BtnSave.Enabled = true;
                        //BtnSave.BackColor = Color.Lavender;
                        InitializeCaptcha();
                        pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch (Exception err)
            {
                pnlTracesMessage.Visible = false;
                blnVerificationComplete = false;
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            }
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
            catch (Exception err)
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
                                        {"Status", "80", "S", "", "", "", ""},
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
                                        {"Status", "80", "S", "", "", "", ""},
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
                string[,] strVERIFIED_PAN = {{"MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL" , "F", "0" , "F"},
                                                 {"MST_VERIFIED_PAN.VERIFIED_PAN_ID > 0" , "F", "MST_VERIFIED_PAN.VERIFIED_PAN_ID", "F"}};
                //--
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
                                      ''                                           AS STATUS,";
                    //IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED 
                    strSQL = strSQL + @"  " + cmnService.J_SQLDBFormat(strVERIFIED_PAN, J_SQLColFormat.Case_End) + @"  
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
                                      ''                                           AS STATUS, ";
                    //IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED 
                    strSQL = strSQL + @"  " + cmnService.J_SQLDBFormat(strVERIFIED_PAN, J_SQLColFormat.Case_End) + @"  
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
                            "         ''                                           AS STATUS, " +
                            //"         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                            "  " + cmnService.J_SQLDBFormat(strVERIFIED_PAN, J_SQLColFormat.Case_End) + " " +
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
                                        {"Status", "80", "S", "", "", "", ""},
                                        {"Verified", "0", "", "", "Right", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------                
                string[,] strVERIFIED_PAN = {{"MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL" , "F", "0" , "F"},
                                             {"MST_VERIFIED_PAN.VERIFIED_PAN_ID > 0" , "F", "MST_VERIFIED_PAN.VERIFIED_PAN_ID", "F"}};
                //-----------------------------------------------------------
                strSQL = "SELECT  DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME AS DEDUCTEE_NAME," +
                        "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN           AS DEDUCTEE_PAN," +
                        "         ''                                              AS STATUS," +
                        //"         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                        " " + cmnService.J_SQLDBFormat(strVERIFIED_PAN, J_SQLColFormat.Case_End) + "  AS VERIFIED " +
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
                        //"         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED " +
                        " " + cmnService.J_SQLDBFormat(strVERIFIED_PAN, J_SQLColFormat.Case_End) + "  AS VERIFIED " +
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
            lblInOperativeNo.Text = "0";
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

        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            try
            {
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }
                try
                {
                    //----------------------------------------------------
                    objTracesConnect = new TracesConnect();
                    //Stream imgStream = objTracesConnect.MakeInitialRequest();
                    //Image img = Image.FromStream(imgStream);
                    //this.picCaptcha.Image = img;
                    var captcha = objTracesConnect.MakeInitialRequest_NEW();
                    this.CurrentCaptchaId = captcha.CaptchaId;
                    Image captchaImage = captcha.CaptchaImage;
                    this.picCaptcha.Image = captchaImage;
                    //-------------------------------------------------------
                    txtCaptchaCode.Text = "";
                }
                catch (Exception err)
                {
                    txtCaptchaCode.Text = "";
                    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTAN.Text = "";
                    grpEnterLoginDetails.Enabled = true;
                    grpCaptcha.Enabled = true;
                    //btnLogging.Text = strLogOn;
                    //LoadDeducteeGrid();
                    //
                    InitializeCaptcha();
                    //
                    break;
                case enmRequestType.PanValidation:

                    //grpDownloadList.Visible = true;
                    //grpEnterLoginDetails.Enabled = false;
                    //grpCaptcha.Enabled = false;
                    //--
                    grpLoginDetails.Visible = false;
                    pnlTracesMessage.Visible = true;
                    ////
                    //grpStatus.Enabled = true;
                    //grpButtons.Enabled = true;
                    //dgvDeductees.Enabled = true;
                    ////
                    //btnVerification.Text = "Stop validating";
                    //btnVerification.ForeColor = Color.Red;
                    //btnPrintInvalidPAN.Enabled = false;
                    //btnPrintUnmatched.Enabled = false;
                    ////
                    //if (!bgwPANVerification.IsBusy)
                    //    bgwPANVerification.RunWorkerAsync();
                    //
                    //--
                    break;

                case enmRequestType.LogOff:
                    ArrayList objList = new ArrayList();
                    objList.Add(enmRequestType.LogOff);
                    //-------------------------------------------
                    pgTimer.Start();
                    //-------------------------------------------
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync(objList);

                    break;

            }
        }

        #endregion

        #region GetServerName
        public string GetServerName()
        {
            DataSet dtSet = null;
            string ServerName = "";
            try
            {
                if (dtSet != null) dtSet.Clear();
                dtSet = dmlService.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                if (dtSet == null)
                {
                    dmlService.Dispose();
                    return "";
                }
                //
                //string strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                //-- 2021/03/11
                string strPath = "";
                if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.STANDARD_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.PROFESSIONAL_EDITION)
                    strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                else
                    strPath = cmnService.J_Decode(dtSet.Tables[0].Rows[0][cmnService.J_Encode("SERVERPATH")].ToString());
                //
                System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
                XMLDoc.Load(strPath + "/" + TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer);
                //
                System.Xml.XmlNode Root = XMLDoc.SelectSingleNode(T_XML.MULTIUSERCONNECTION + "/" + T_XML.SERVERNAME);
                ServerName = cmnService.J_Decode(Root.InnerText);
                //
                if (ServerName == "")
                    return "";
                else
                    return ServerName;
            }
            catch (Exception err)
            {
                return "";
            }
        }
        #endregion

        #region GetServerDateTime 
        //-- 2016/01/29
        public string GetServerDateTime()
        {
            string strServerDT = "";
            if (TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_LITE_EDITION || TDSMAN.Classes.TDSMAN.T_pEditionType == T_EDITION_TYPE.ENTERPRISE_ULTIMATE_EDITION)
            {
                strServerDT = dmlService.J_ReturnServerDate();
                //
                if (strServerDT == "")
                    strServerDT = "null";
                else
                    strServerDT = cmnService.J_DateOperator() + strServerDT + cmnService.J_DateOperator();
            }
            else
            {
                if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.MULTI_USER)
                {
                    //Added by Indrajit on 26-09-2012 to get SERVER DATETIME
                    //string strServerDT = ExecuteCommandSync("net time \\\\Pds1");
                    //Added by Indrajit on 03-10-2012
                    if (GetServerName() != "")
                        strServerDT = cmnService.J_DateOperator() + RemoteDateTime(GetServerName()) + cmnService.J_DateOperator();

                    if (strServerDT == "")
                        strServerDT = "null";
                    //else
                    //{
                    //    strServerDT = "CDATE('" + strServerDT + "')";
                    //    strServerDT = strServerDT.Replace("AM", "");
                    //    strServerDT = strServerDT.Replace("PM", "");
                    //}
                }
                else if (TDSMAN.Classes.TDSMAN.T_PackageType == T_PACKAGE_TYPE.SINGLE_USER)
                {
                    if (J_Var.J_pDBPath.Substring(0, 2) == "\\\\")
                    {
                        string svr = J_Var.J_pDBPath.Substring(0, J_Var.J_pDBPath.IndexOf("\\", 2)).Substring(2);
                        strServerDT = RemoteDateTime(svr);
                    }
                    else
                        strServerDT = DateTime.Now.ToString();
                    //--
                    if (strServerDT == "")
                        strServerDT = "null";
                    else
                        strServerDT = "CDATE('" + strServerDT + "')";
                }
            }
            //--
            return strServerDT;
        }

        #endregion

        #region RemoteDateTime
        public string RemoteDateTime(string ServerAddress)
        {
            string rt = "";
            //
            IntPtr handle = IntPtr.Zero;
            if (NetRemoteTOD("\\\\" + ServerAddress, ref handle) == NERR_Success)
            {
                TIME_OF_DAY_INFO time = (TIME_OF_DAY_INFO)Marshal.PtrToStructure(handle, typeof(TIME_OF_DAY_INFO));
                // new date time. The hours are in utc, so you have to use the timezone offset.
                DateTime dt = new DateTime((int)time.tod_year,
                (int)time.tod_month, (int)time.tod_day, (int)(time.tod_hours)
                , (int)time.tod_mins, (int)time.tod_secs);
                //bb
                dt = DateTime.Parse(dt.ToString());
                dt = dt.AddMinutes(330); //-- 2014/11/18 ADDDED 5 HRS 30 MIN TO THE TIME TO GET 'INDIAWALE' TIME...
                //
                rt = Convert.ToString(dt.ToString("dd/MMM/yyyy HH:mm:ss"));
                //
                uint result = NetApiBufferFree(handle);
                //
                if (result != NERR_Success)
                    MessageBox.Show("Memory cleanup failed");
            }
            return rt;
        }
        #endregion

        #endregion


        #region btnPrintInoperativePAN_Click
        private void btnPrintInoperativePAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_ReturnInt32Value(lblInOperativeNo.Text) == 0) { cmnService.J_UserMessage("No Inoperative PAN to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print of the Inoperative PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                //rptDialog.PrintValidInOperativePans(lngBasicInfoID, blRegular.ToString().ToUpper(), "");
                rptDialog.PrintValidInOperativePans_RDLC(lngBasicInfoID, blRegular.ToString().ToUpper(), "");
                //}
            }
            catch
            {
            }
        }
        #endregion

        #region btnPrintInoperativePAN_MouseMove
        private void btnPrintInoperativePAN_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.SetToolTip(btnPrintInoperativePAN, "Print Inoperative PAN(s)");
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

        #region ExportGridToCSV
        private void ExportGridToCSV(DataGridView dgv)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                filePathCSV = Path.Combine(Application.StartupPath, TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                // HEADER
                //sb.AppendLine("Deductee Name,PAN No");
                sb.AppendLine("PAN");

                // ROWS
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        //string name = row.Cells["DeducteeName"].Value?.ToString().Trim();
                        string pan = row.Cells[intPANId].Value?.ToString().Trim();

                        // Optional: skip blank PANs
                        if (!string.IsNullOrEmpty(pan))
                        {
                            // Handle commas safely
                            //name = EscapeCSV(name);
                            pan = EscapeCSV(pan);

                            //sb.AppendLine($"{name},{pan}");
                            sb.AppendLine($"{pan}");
                        }
                    }
                }

                // WRITE FILE
                File.WriteAllText(filePathCSV, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception err)
            {

            }
        }
        #endregion

        #region ExportGridToCSV
        private void ExportGridToCSV(DataGridView dgv, Dictionary<string, string> panDict)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                filePathCSV = Path.Combine(
                    Application.StartupPath,
                    TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES + "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                // HEADER
                sb.AppendLine("PAN");

                HashSet<string> addedPAN = new HashSet<string>(); // avoid duplicates

                // ROWS
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string pan = row.Cells[intPANId].Value?.ToString().Trim().ToUpper();

                        if (!string.IsNullOrEmpty(pan))
                        {
                            // ONLY UNVERIFIED PAN
                            if (!panDict.ContainsKey(pan))
                            {
                                if (!addedPAN.Contains(pan)) // avoid duplicates
                                {
                                    addedPAN.Add(pan);

                                    pan = EscapeCSV(pan);
                                    sb.AppendLine(pan);
                                }
                            }
                        }
                    }
                }

                // WRITE FILE
                File.WriteAllText(filePathCSV, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("CSV Export failed: " + err.Message);
            }
        }
        #endregion

        #region ExportGridToCSV
        private void ExportGridToCSV(DataGridView dgv, Dictionary<string, PANVerifyInfo> panDict)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                filePathCSV = Path.Combine(
                    Application.StartupPath,
                    TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES + "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");

                sb.AppendLine("PAN");

                HashSet<string> addedPAN = new HashSet<string>();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        bool isSelected = true; //row.Cells[0].Value != null &&
                                          //Convert.ToBoolean(row.Cells[0].Value);
                        if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.MAKE_CORRECTION)
                        {
                            isSelected = row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value);
                        }
                            if (!isSelected) continue;

                        string pan = row.Cells[intPANId].Value?.ToString().Trim().ToUpper();

                        if (!string.IsNullOrEmpty(pan))
                        {
                            // KEY CONDITION: ONLY UNVERIFIED PAN
                            if (!panDict.ContainsKey(pan))
                            {
                                if (!addedPAN.Contains(pan))
                                {
                                    addedPAN.Add(pan);
                                    sb.AppendLine(EscapeCSV(pan));
                                }
                            }
                        }
                    }
                }

                if (addedPAN.Count == 0)
                {
                    //cmnService.J_UserMessage("No unverified PANs found for export.");
                    return;
                }

                File.WriteAllText(filePathCSV, sb.ToString(), Encoding.UTF8);

                //cmnService.J_UserMessage(addedPAN.Count + " PAN(s) exported successfully.");
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("CSV Export failed: " + err.Message);
            }
        }
        #endregion 

        #region TrnBulkPANVerificationTraces_New_FormClosing
        private void TrnBulkPANVerificationTraces_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(strSaveDownloadedCSVFilePath))
            {
                File.Delete(strSaveDownloadedCSVFilePath);
            }
            //
            if (File.Exists(filePathCSV))
            {
                File.Delete(filePathCSV);
            }
        }
        #endregion

        #region EscapeCSV
        private string EscapeCSV(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
            {
                input = input.Replace("\"", "\"\"");
                input = "\"" + input + "\"";
            }

            return input;
        }
        #endregion


        #region ReadCSVPANStatus
        //public Dictionary<string, string> ReadPANStatus(string filePath)
        //{
        //    var dict = new Dictionary<string, string>();

        //    using (var reader = new StreamReader(filePath))
        //    {
        //        string line;
        //        bool isHeader = true;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            if (isHeader)
        //            {
        //                isHeader = false;
        //                continue;
        //            }

        //            if (string.IsNullOrWhiteSpace(line))
        //                continue;

        //            var parts = line.Split(',');

        //            if (parts.Length < 3)
        //                continue;

        //            string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
        //            string status = parts[2].Trim().ToUpper();

        //            if (!dict.ContainsKey(pan))
        //                dict.Add(pan, status);
        //        }
        //    }

        //    return dict;
        //}
        #endregion


        #region UpdateGridPANStatus
        public void UpdateGridPANStatus(Dictionary<string, string> panStatusDict)
        {
            foreach (DataGridViewRow row in dgvDeductees.Rows)
            {
                if (row.Cells["PANNo"].Value == null)
                    continue;

                string pan = row.Cells["PANNo"].Value.ToString().Trim();

                if (panStatusDict.ContainsKey(pan))
                {
                    row.Cells["Status"].Value = panStatusDict[pan];

                    // Optional coloring
                    string status = panStatusDict[pan];

                    if (status.Contains("VALID"))
                        row.Cells["Status"].Style.BackColor = Color.LightGreen;
                    else if (status.Contains("INVALID"))
                        row.Cells["Status"].Style.BackColor = Color.LightCoral;
                    else
                        row.Cells["Status"].Style.BackColor = Color.LightYellow;
                }
            }
        }
        #endregion

        #region BulkUpdatePANStatus
        public void BulkUpdatePANStatus(DataTable dt)
        {
            dmlService.J_BeginTransaction();
            int rows = 0;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    string pan = row["PAN"].ToString();
                    string name = row["NAME"].ToString();
                    int statusId = 0;// Convert.ToInt32(row["STATUS"]);
                    if (row["STATUS"] != DBNull.Value)
                    {
                        int.TryParse(row["STATUS"].ToString(), out statusId);
                    }

                    //int rows = 
                    //if (dmlService.J_ExecSql(
                    //    "UPDATE MST_VERIFIED_PAN SET VERIFIED_STATUS = " + statusId +
                    //    ", CHECKED_DATE_TIME = " + GetServerDateTime() +
                    //    " WHERE PAN = '" + pan + "'")== true)
                    //    rows = 1;
                    //else
                    //    rows = 0;
                    //
                    //if (rows == 0)
                    //{
                        dmlService.J_ExecSql(
                            "INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, VERIFIED_STATUS, CHECKED_DATE_TIME) VALUES('" +
                            pan + "','" + name + "'," + statusId + "," + GetServerDateTime() + ")");
                    //}
                }

                dmlService.J_Commit();
            }
            catch
            {
                dmlService.J_Rollback();
                throw;
            }
        }
        #endregion


        #region ReadPANStatus
        //public Dictionary<string, string> ReadPANStatus(string filePath)
        //{
        //    var dict = new Dictionary<string, string>();

        //    using (var reader = new StreamReader(filePath))
        //    {
        //        string line;
        //        bool isHeader = true;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            if (isHeader)
        //            {
        //                isHeader = false;
        //                continue;
        //            }

        //            if (string.IsNullOrWhiteSpace(line))
        //                continue;

        //            var parts = line.Split(',');

        //            if (parts.Length < 3)
        //                continue;

        //            string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
        //            string status = parts[2].Trim().ToUpper();

        //            if (!dict.ContainsKey(pan))
        //                dict.Add(pan, status);
        //        }
        //    }

        //    return dict;
        //}
        public Dictionary<string, PANInfo> ReadPANStatus(string filePath)
        {
            var dict = new Dictionary<string, PANInfo>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');

                    if (parts.Length < 3)
                        continue;

                    string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
                    string name = parts[1].Trim().ToUpper();
                    string status = parts[2].Trim().ToUpper();

                    if (!dict.ContainsKey(pan))
                    {
                        dict.Add(pan, new PANInfo
                        {
                            Name = name,
                            Status = status
                        });
                    }
                }
            }

            return dict;
        }
        #endregion

        #region ReadPANNameStatus
        public Dictionary<string, PANInfo> ReadPANNameStatus(string filePath)
        {
            var dict = new Dictionary<string, PANInfo>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Handles commas inside quotes
                    var parts = Regex.Matches(line, "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)")
                                     .Cast<Match>()
                                     .Select(m => m.Value.Trim().Trim('"'))
                                     .ToArray();

                    if (parts.Length < 3)
                        continue;

                    string pan = parts[0].Replace("\uFEFF", "").Trim().ToUpper();
                    string name = parts[1].Trim().ToUpper();
                    string status = parts[2].Trim().ToUpper();

                    if (!dict.ContainsKey(pan))
                    {
                        dict.Add(pan, new PANInfo
                        {
                            Name = name,
                            Status = status
                        });
                    }
                }
            }

            return dict;
        }
        #endregion

        #region ApplyStatusToGrid
        private void ApplyStatusToGrid(int rowIndex, string statusId, DateTime? checkedDate)
        {
            var cell = dgvDeductees.Rows[rowIndex].Cells[intStatusId];

            string statusText = "";
            string dateText = "";

            if (checkedDate != null)
                dateText = " (" + checkedDate.Value.ToString("dd/MM/yyyy") + ")";

            if (statusId == T_PANVerificationStatusId.VALID_OPERATIVE.ToString())
            {
                statusText = "Verified";
                cell.Style.BackColor = Color.Green;
            }
            else if (statusId == T_PANVerificationStatusId.VALID_INOPERATIVE.ToString())
            {
                statusText = "Inoperative";
                cell.Style.BackColor = Color.Yellow;
            }
            else
            {
                statusText = "Invalid";
                cell.Style.BackColor = Color.Red;
            }

            // THIS IS THE LINE YOU ASKED ABOUT
            cell.Value = dateText;
        }
        #endregion

        private string NormalizePAN(string pan)
        {
            if (string.IsNullOrWhiteSpace(pan))
                return "";

            return pan.Trim().ToUpper();
        }


        #region SAVE THE DATA IN LOG
        private void SaveLogData(long BasicInfoID, string TANNo, int CorrFlag, string TokenNo, int NoOfPANs)
        {
            //-- CREATE THE TABLE...
            #region TRN_PAN_VERIFICATION_TRACES_LOG
            if (dmlService.J_IsDatabaseObjectExist("TRN_PAN_VERIFICATION_LOG") == true)
            {
                strSQL = "DROP TABLE TRN_PAN_VERIFICATION_LOG";
                dmlService.J_ExecSql(strSQL);
            }
            //
            if (dmlService.J_IsDatabaseObjectExist("TRN_TRACES_PAN_VERIFICATION_LOG") == false)
            {
                //--
                strSQL = "CREATE TABLE TRN_TRACES_PAN_VERIFICATION_LOG (" +
                        " " + cmnService.J_GetDataType("PAN_VERIFICATION_LOG_ID", J_Identity.YES) + "," +
                        " " + cmnService.J_GetDataType("BASIC_INFO_ID", J_ColumnType.Long, J_DefaultValue.YES) + "," +
                        " " + cmnService.J_GetDataType("TAN_NO", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                        " " + cmnService.J_GetDataType("TOKEN_NO", J_ColumnType.String, 100, J_DefaultValue.YES) + "," +
                        " " + cmnService.J_GetDataType("NO_OF_PANS", J_ColumnType.Long, J_DefaultValue.YES) + "," +
                        " " + cmnService.J_GetDataType("CORR_FLAG", J_ColumnType.Integer, J_DefaultValue.YES) + "," +
                        " " + cmnService.J_GetDataType("CREATE_DATE_TIME", J_ColumnType.DateTime) + "," +
                        " " + cmnService.J_GetDataType("END_DATE_TIME", J_ColumnType.DateTime) + ")";
                dmlService.J_ExecSql(strSQL);
                //--
            }
            #endregion
            //-- INSERT DATA...
            strSQL = @"INSERT INTO TRN_TRACES_PAN_VERIFICATION_LOG ( 
                                                            BASIC_INFO_ID,
                                                            TAN_NO,
                                                            TOKEN_NO,
                                                            NO_OF_PANS,
                                                            CORR_FLAG,
                                                            CREATE_DATE_TIME
                                                            ) 
                                                VALUES (" + BasicInfoID + ",'"
                                                        + TANNo + "','"
                                                        + TokenNo + "',"
                                                        + NoOfPANs + ","
                                                        + CorrFlag + ","
                                                        + cmnService.J_DateOperator() + DateTime.Now.ToString() + cmnService.J_DateOperator() + ")";
            dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);

        }
        #endregion

        #region FetchTokenNo
        private string FetchTokenNo(long BasicInfoID, string TANNo, int CorrFlag, out string TokenNODateTime)
        {
            TokenNODateTime = "";
            if (CorrFlag > 0)
                strSQL = @"SELECT TOKEN_NO FROM TRN_TRACES_PAN_VERIFICATION_LOG WHERE BASIC_INFO_ID =" + BasicInfoID + " AND CORR_FLAG = " + CorrFlag + " AND END_DATE_TIME IS NULL";
            else
                strSQL = @"SELECT TOKEN_NO FROM TRN_TRACES_PAN_VERIFICATION_LOG WHERE BASIC_INFO_ID =" + BasicInfoID + " AND END_DATE_TIME IS NULL";
            //
            string strTokenNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (strTokenNo != "")
            {
                if (CorrFlag > 0)
                    strSQL = @"SELECT CREATE_DATE_TIME FROM TRN_TRACES_PAN_VERIFICATION_LOG WHERE BASIC_INFO_ID =" + BasicInfoID + " AND CORR_FLAG = " + CorrFlag + " AND END_DATE_TIME IS NULL";
                else
                    strSQL = @"SELECT CREATE_DATE_TIME FROM TRN_TRACES_PAN_VERIFICATION_LOG WHERE BASIC_INFO_ID =" + BasicInfoID + " AND END_DATE_TIME IS NULL";
                TokenNODateTime = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                return strTokenNo;
            }
            return "";
        }
        #endregion

        #region UpdateLogTokenNO
        private string UpdateLogTokenNO(long BasicInfoID, string TokenNo, int CorrFlag)
        {
            if (CorrFlag > 0)
                strSQL = @"UPDATE TRN_TRACES_PAN_VERIFICATION_LOG SET END_DATE_TIME = " + cmnService.J_DateOperator() + DateTime.Now.ToString() + cmnService.J_DateOperator() + " WHERE BASIC_INFO_ID =" + BasicInfoID + "AND CORR_FLAG = " + CorrFlag + " AND TOKEN_NO ='" + TokenNo + "' AND END_DATE_TIME IS NULL";
            else
                strSQL = @"UPDATE TRN_TRACES_PAN_VERIFICATION_LOG SET END_DATE_TIME = " + cmnService.J_DateOperator() + DateTime.Now.ToString() + cmnService.J_DateOperator() + " WHERE BASIC_INFO_ID =" + BasicInfoID + " AND TOKEN_NO ='" + TokenNo + "' AND END_DATE_TIME IS NULL";
            //
            string strTokenNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (strTokenNo != "")
                return strTokenNo;
            return "";
        }
        #endregion
    }

}