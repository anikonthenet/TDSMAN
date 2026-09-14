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

using System.Xml;




//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;
using System.Text.RegularExpressions;
using System.Linq;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnBulkPANNameValidationImprovedVersion_New_Log_Latest : Form
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnBulkPANNameValidationImprovedVersion_New_Log_Latest()
        {
            InitializeComponent();
            objBindingSource.DataSource = new PANDetails();
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
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        bool blnExit = true;
        //
        int intID = 0;
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intVerifyId = 0, intVerifiedName = 0,intClicked = 0;
        //;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        int intLoadGridPAN = 500;
        long lngSelectedGrid = 0;
        //
        int j = 0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        private string CurrentCaptchaId = "";
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
        int intCSVStatusRetry = 0; //string strLogTokenNo = "";
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();

        string filePathCSV = "", filePathCSVTokenNo = "", UploadedfilePath = "", strSaveDownloadedCSVFilePath = "", strLogTokenNo = "", strLogTokenNoDateTime = "";

        //Added by Indrajit on 21-02-2013 to get DateTime of a Remote Computer
        #region Decleration for Remote DateTime

        private const uint NERR_Success = 0;



        /// Free the buffer allocated by the NetRemoteTOD function
        /// <param name="Buffer">pointer to the buffer</param>
        /// <returns>NERR_Success on success, else the system error code.</returns>
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
        #endregion
        public class PANInfo
        {
            public string Name { get; set; }
            public string Status { get; set; }
            //public string AadhaarLinkDate { get; set; }
        }
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

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnBulkPANNameValidation_Activated
        private void TrnBulkPANNameValidation_Activated(object sender, EventArgs e)
        {
            //--
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //----
            //-- Added By Abhishek Dey On 22/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
                txtPassword.UseSystemPasswordChar = false;
            //-----------------------------------------
        }
        #endregion
        //----------------------------------------------

        #region TrnBulkPANNameValidation_Load
        private void TrnBulkPANNameValidation_Load(object sender, EventArgs e)
        {
            try
            {
                //--
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //----
                GC.Collect();
                //
                ClearFields();
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "";
                dmlService.J_ExecSql(strSQL);
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "";
                dmlService.J_ExecSql(strSQL);
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "";
                dmlService.J_ExecSql(strSQL);
                //--
                #region COMMENT
                //                LoadDeducteeGrid();
                //                //--
                //                if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                //                {
                //                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                //                    //--
                //                    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                //                        strEmpDed = "Employee";
                //                    else
                //                        strEmpDed = "Deductee";
                //                    //--
                //                    intPANId = 0;
                //                    intNameEntered = 1;
                //                    intNameVerified = 2;
                //                    intStatusId = 3;
                //                    intVerifyId = 4;
                //                    //
                //                    blRegular = false;
                //                }
                //                else
                //                {
                //                    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                //                    //
                //                    if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q)
                //                        strEmpDed = "Employee";
                //                    else
                //                        strEmpDed = "Deductee";
                //                    //--
                //                    intPANId = 1;
                //                    intNameEntered = 2;
                //                    intNameVerified = 3;
                //                    intStatusId = 4;
                //                    intVerifyId = 5;
                //                    //
                //                    blRegular = true;
                //                }
                //                //--
                //                if (lngBasicInfoID == 0)
                //                {
                //                    //if (dgvDeductees.Rows.Count > 0)
                //                    //    dgvDeductees.Rows.Clear();
                //                    return;
                //                }
                //                //--
                //                if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                //                    LoadDeducteeGridCorr(lngBasicInfoID);
                //                else
                //                    LoadDeducteeGrid(lngBasicInfoID);
                //                //
                //                //this.Cursor = Cursors.Default;
                //                //
                //                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //                btnVerification.Text = strbtnVerification;
                //                btnVerification.ForeColor = Color.Black;
                //                //--
                //                TdsMan.GetSetup();
                //                //--
                //                #region COMMENT
                //                //-----------
                //                //-- FINANCIAL YEAR
                //                //-----------
                //                //strSQL = " SELECT ASST_ID," +
                //                //    "             FA_YEAR " +
                //                //    "      FROM   MST_ASSESSMENT " +
                //                //    "      WHERE  VISIBILITY_FLAG = 0 " +
                //                //    "      ORDER BY ASST_ID DESC";
                //                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFinancialYear, 1, J_ComboBoxSelectedIndex.YES) == false) return;
                //                ////-----------
                //                ////-- QUARTER
                //                ////-----------
                //                //string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                //                //dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                //                ////-----------
                //                ////-- FORM NO
                //                ////-----------
                //                //string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                //                //dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo, 1);
                //                ////-----------
                //                ////-- COMPANY
                //                ////-----------
                //                //strSQL = " SELECT COMPANY_ID," +
                //                //    "             COMPANY_NAME + ' [' + TAN_NO + ']' " +
                //                //    "      FROM   MST_COMPANY " +
                //                //    "      ORDER BY COMPANY_NAME";
                //                //if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                //                ////-----------
                //                //cmbFinancialYear.Select();
                //                #endregion
                //                //--
                //                strSQL = "DELETE FROM TEMP_INVALID_PAN";
                //                dmlService.J_ExecSql(strSQL);
                //                //--
                //                #region INSERT INTO MST_VERIFIED_PAN

                //                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                //                            SELECT DISTINCT DEDUCTEE_PAN, DEDUCTEE_NAME 
                //                            FROM   COR_HDR_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN
                //                            ON     COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN = MST_VERIFIED_PAN.PAN
                //                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                //                            AND    COR_HDR_DEDUCTEE_DETAILS.INVALID_PAN = 0
                //                            AND    COR_HDR_DEDUCTEE_DETAILS.DEDUCTEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')";
                //                dmlService.J_ExecSql(strSQL);
                //                //--
                //                strSQL = @"INSERT INTO MST_VERIFIED_PAN (PAN, VERIFIED_NAME)
                //                            SELECT DISTINCT EMPLOYEE_PAN, EMPLOYEE_NAME 
                //                            FROM   COR_HDR_SALARY_DETAILS LEFT JOIN MST_VERIFIED_PAN
                //                            ON     COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN = MST_VERIFIED_PAN.PAN
                //                            WHERE  MST_VERIFIED_PAN.PAN IS NULL
                //                            AND    COR_HDR_SALARY_DETAILS.INVALID_PAN = 0
                //                            AND    COR_HDR_SALARY_DETAILS.EMPLOYEE_PAN NOT IN ('PANNOTAVBL', 'PANAPPLIED', 'PANINVALID')";
                //                dmlService.J_ExecSql(strSQL);

                //                #endregion
                #endregion
                //--
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                {
                    LoadFormTools();
                    //
                    grpReturnSelection.Visible = false;
                    grpRegularReturn.Visible = false;
                    grpCorrectionReturn.Visible = false;
                    pnlLine.Visible = false;
                    rbnReVerifyInoperativePANs.Visible = false;
                    //
                    dgvDeductees.Location = new Point(12, 47);
                    //dgvDeductees.Location = new Point(12, 75);
                    dgvDeductees.Height = 535;
                    //
                    ////chkSelectAll.Location = new Point(84, 47);
                    ////rbnOnlyUnVerifiedPANs.Location = new Point(237, 47);
                    ////rbnAllPANs.Location = new Point(411, 47);
                    ////rbnReVerifyValidPANs.Location = new Point(512, 47);
                    chkSelectAll.Visible = false;
                    rbnOnlyUnVerifiedPANs.Visible = false;
                    rbnAllPANs.Visible = false;
                    rbnReVerifyValidPANs.Visible = false;
                    //
                    if (TDSMAN.Classes.TDSMAN.T_pFormNo == T_FormNo.F24Q)
                        strEmpDed = "Employee";
                    else
                        strEmpDed = "Deductee";
                    //--
                    intPANId = 1;
                    intNameEntered = 2;
                    intNameVerified = 3;
                    intStatusId = 4;
                    intVerifyId = 5;
                    //--
                    //LoadDeducteeGridExcelImport();
                    if(TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS)
                        LoadAllPANsDeducteeGridExcelImport();
                    else if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS) //-- 2024/05/22
                        LoadAllPANsDeducteeGridExcelImportSD();
                    else if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS) //-- 2024/05/22
                        LoadNewPANsDeducteeGridExcelImportSD();
                    else if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR) //-- 2024/05/22
                        LoadNewPANsDeducteeGridExcelImport();
                }
                else
                {
                    //LoadBatch();
                    //
                    rbnRegularCorrectionReturn_CheckedChanged(sender, e);
                }
                //
                //chkSelectAll_CheckedChanged(sender, e);
                System.Threading.Thread.Sleep(300);
                CheckAllGridRows();
                //
                this.Cursor = Cursors.Default;
                //-----------
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region rbnRegularCorrectionReturn_CheckedChanged
        private void rbnRegularCorrectionReturn_CheckedChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (rbnRegularReturn.Checked == true)
            {
                TDSMAN.Classes.TDSMAN.T_pTAN = "";
                grpRegularReturn.Visible = true;
                grpCorrectionReturn.Visible = false;
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
                string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQuarter, 1);
                //-----------
                //-- FORM NO
                //-----------
                //string[] strFormNo1 = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
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
                    "      WHERE  INACTIVE_FLAG = 0 " +
                    "      ORDER BY COMPANY_NAME";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
                //-----------

                #endregion
                //--
                if (TDSMAN.Classes.TDSMAN.T_pBookMarkOption == true)
                {
                    //strQuery = @" FORM_NAME= '" + strFormNo + "' ";

                    if (dmlService.J_IsRecordExist("MST_BOOKMARK_DETAIL") == true)
                    {
                        intAsstId = TdsMan.T_GetBookMarkDetail(dmlService.J_pCommand, T_TransactionMode.SHOW.ToString(), 0, "", "", 0, TDSMAN.Classes.TDSMAN.T_MACHINE_ID, out strQuarter, out strCompanyName, out strTAN, out strFormNoBkmark, out strFAYear);
                        //
                        //                    strSQL = @"SELECT FA_YEAR
                        //                              FROM MST_ASSESSMENT
                        //                              WHERE ASST_ID=" + intAsstId + "";
                        //                    cmbFinancialYear.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));//Convert.ToInt32(Support.SetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex, intAsstId));
                        cmbQuarter.Text = strQuarter;
                        cmbCompany.Text = strCompanyName + " [" + strTAN + "]";
                        cmbFormNo.Text = strFormNoBkmark;
                        cmbFinancialYear.Text = strFAYear;
                    }
                }
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                TDSMAN.Classes.TDSMAN.T_pTAN = "";
                grpRegularReturn.Visible = false;
                grpCorrectionReturn.Visible = true;
                //
                this.Cursor = Cursors.WaitCursor;
                //
                #region CLEAR CONTROLS
                //--
                LoadBatch();
                //
                //LoadDeducteeGridCorr(lngBasicInfoID);
                dgcViewBatch_Click(sender, e);
                //
                #endregion
                //--
                this.Cursor = Cursors.Default;
            }
            //
            //LoadDeducteeGrid();
        }
        #endregion

        #region cmbLoadGrid_SelectedIndexChanged
        private void cmbLoadGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                LoadDeducteeGrid();
                ClearFields();
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                LoadDeducteeGrid();
                ClearFields();
                return;
            }
            if (cmbQuarter.Text == "")
            {
                LoadDeducteeGrid();
                ClearFields();
                return;
            }
            if (cmbFormNo.Text == "")
            {
                LoadDeducteeGrid();
                ClearFields();
                return;
            }
            //-----------------------------------------------
            ClearFields();
            ComboBoxItem selectedItem = (ComboBoxItem)cmbFormNo.SelectedItem;
            string strFormNo = selectedItem.Value.ToString();
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //
            if (strFormNo == T_FormNo.F24Q)
                strEmpDed = "Employee";
            else
                strEmpDed = "Deductee";
            //--
            intPANId = 1;
            intNameEntered = 2;
            intNameVerified = 3;
            intStatusId = 4;
            intVerifyId = 5;
            //
            blRegular = false;
            //
            if (lngBasicInfoID == 0)
            {
                strLogTokenNo = "";
                LoadDeducteeGrid();
                return;
            }
            //--
            if (dmlService.J_IsDatabaseObjectExist("WT_TRN_TRACES_PAN_VERIFICATION_LOG") == true)
                strLogTokenNo = FetchTokenNo(lngBasicInfoID, txtTANNo.Text,0, out strLogTokenNoDateTime);
            //--
            rbnOnlyUnVerifiedPANs.Checked = true;
            rbnAllPANs_CheckedChanged(sender, e);
            //LoadDeducteeGrid(lngBasicInfoID);
            //--
        }
        #endregion

        #region Batch

        #region dgcViewBatch_Click
        private void dgcViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgcViewBatch.CurrentRow == null)
                {
                    lngBasicInfoID = 0;
                    ClearFields();
                    LoadDeducteeGrid();
                    return;
                }

                if (dgcViewBatch.CurrentRow.Index < 0)
                {
                    lngBasicInfoID = 0;
                    ClearFields();
                    return;
                }
                //
                ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                LoadDeducteeGridCorr(lngBasicInfoID);//, chkBoxNewEntriesOnly.Checked);
                //
                //TDSMAN.Classes.TDSMAN.T_FromModule = T_OTHERMODULENAME.MAKE_CORRECTION;
            }
            catch
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
                ClearFields();
                //
                lngBasicInfoID = Convert.ToInt64(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[0].Value);
                //
                LoadDeducteeGridCorr(lngBasicInfoID);//, chkBoxNewEntriesOnly.Checked);
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

        #endregion

        #region btnVerification_Click
        private void btnVerification_Click(object sender, EventArgs e)
        {
            if (btnVerification.Text == strbtnVerification)
            {
                //--
                if (ValidateFields() == false) return;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                {
                    if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
                    {
                        cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted", MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found", MessageBoxIcon.Exclamation);
                    //BtnExit.Select();
                    return;
                }
                //--
                if (dgvDeductees.Rows.Count == 0)
                {
                    cmnService.J_UserMessage("No deductees loaded", MessageBoxIcon.Exclamation);
                    return;
                }
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS)
                {
                    int i = 0;
                    foreach (DataGridViewRow row in dgvDeductees.Rows)
                    {
                        if (row.Cells[0].Value != null)
                            if ((bool)row.Cells[0].Value == true)
                            i = i + 1;
                    }
                    if (i == 0)
                    {
                        cmnService.J_UserMessage("No record selected", MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                //--
                if (TDSMAN.Classes.TDSMAN.T_SHOW_TRACES_ERR_MESSAGE == true)
                {
                    cmnService.J_UserMessage(TDSMAN.Classes.TDSMAN.T_SHOW_TRACES_ERR_MESSAGE_TEXT.ToString());
                    return;
                }
                //--
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //
                //-- CREATE THE CSV FOR UPLOADING TO TRACES...
                if(strLogTokenNo == "")
                    ExportSelectedGridToCSV(dgvDeductees);
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "";
                dmlService.J_ExecSql(strSQL);
                //--
                btnUpdateMasterData.Visible = false;
                //if(objresp
                this.Cursor = Cursors.WaitCursor;
                //
                if (TDSMAN.Classes.TDSMAN.T_FromModule == "")
                    TDSMAN.Classes.TDSMAN.T_pTAN = "";
                //--
                if (TDSMAN.Classes.TDSMAN.T_pTAN == "")
                {
                    if (rbnRegularReturn.Checked == true)
                        TDSMAN.Classes.TDSMAN.T_pTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 11).Replace("]", "").Trim();
                    else if (rbnCorrectionReturn.Checked == true)
                        TDSMAN.Classes.TDSMAN.T_pTAN = Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value);
                }
                //-- GET TAN DETILS IF AVAILABLE
                strSQL = @"SELECT  TOP 1 MST_TAN_ACCOUNT.TAN_NO,
                                   MST_TAN_ACCOUNT.LOGIN_ID,
                                   MST_TAN_ACCOUNT.USER_PASSWORD                                   
                           FROM    MST_TAN_ACCOUNT 
                           WHERE   MST_TAN_ACCOUNT.TAN_NO = '" + TDSMAN.Classes.TDSMAN.T_pTAN + "'";
                //
                IDataReader drdShowRecord = null;
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);

                if (drdShowRecord == null)
                {
                    blnExit = true; //-- Added By Abhishek Dey On 20/08/2018 --
                    return;
                }
                //
                while (drdShowRecord.Read())
                {
                    blnExit = false; //-- Added By Abhishek Dey On 20/08/2018 --
                    txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                    txtUserID.Text = Convert.ToString(drdShowRecord["LOGIN_ID"]);
                    txtPassword.Text = Convert.ToString(drdShowRecord["USER_PASSWORD"]);
                    txtCaptchaCode.Select();
                    blnExit = true;  //-- Added By Abhishek Dey On 20/08/2018 --
                }
                //
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //--
                //InitializeCaptcha();
                picCaptcha.Image = Properties.Resources.captcha_loading;
                if (!bgWorkerLoadCaptcha.IsBusy)
                    bgWorkerLoadCaptcha.RunWorkerAsync();
                //
                grpLoginDetails.Visible = true;
                //
                if(txtTANNo.Text=="")
                    txtTANNo.Select();
                else
                    txtCaptchaCode.Select();
                //--
                grpStatus.Enabled = false;
                grpButtons.Enabled = false;
                dgvDeductees.Enabled = false;
                grpReturnSelection.Enabled = false;
                grpRegularReturn.Enabled = false;
                grpCorrectionReturn.Enabled = false;
                //
                this.Cursor = Cursors.Default;
                //
            }
            else
            {
                bgwPANVerification.CancelAsync();
                bgwPANVerification.Dispose();
                //
                GC.Collect();
                this.Cursor = Cursors.Default;
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
                blnVerificationComplete = false;
                //
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
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    ArrayList objRetval = new ArrayList();
        //    TracesResponse objResponse = new TracesResponse();
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    string strSTATUS = ""; string strVerifiedNAME = "";
        //    if (btnVerification.Text != strbtnVerification)
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        //pctGreenDownArrow.Visible = true;
        //        //--
        //        for (int i = j; i <= dgvDeductees.RowCount-1 ; i++)
        //        {
        //            if (Convert.ToString(dgvDeductees.Rows[i].Cells[intStatusId+2].Value) != "")
        //            {
        //                this.Cursor = Cursors.Default;
        //                //
        //                dgvDeductees.Rows[i].Cells[intNameVerified].Value = dgvDeductees.Rows[i].Cells[intStatusId + 2].Value;
        //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //                lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
        //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active";
        //                //
        //                if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value))
        //                {
        //                    dgvDeductees.Rows[i].Cells[intNameEntered].Style.BackColor = Color.Tan;
        //                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active\nbut name unmatched with the entered value";
        //                    lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);
        //                    strUnmatchedPAN = strUnmatchedPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                    //
        //                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                    {
        //                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                                                 "VALUES('" +  cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                                 "'" +  cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                    }                      
        //                }
        //                //
        //            }
        //            else if (TdsMan.T_CheckInternetConnectivty() == false)
        //            {
        //                this.Cursor = Cursors.Default;
        //                //
        //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
        //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified\n[Internet Connectivity not found]";
        //                //
        //                lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
        //            }
        //            else
        //            {
        //                TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
        //                                                                                   out strVerifiedNAME);
        //                //--
        //                this.Cursor = Cursors.Default;
        //                //
        //                if (response.Respons == enmResponse.Success)
        //                {
        //                    //dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //                    //--
        //                    dgvDeductees.Rows[i].Cells[intNameVerified].Value = strVerifiedNAME.ToUpper().Trim().Replace("AMP;","").Replace("  "," ");
        //                    //--
        //                    if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == "") //-- NO NAME
        //                    {
        //                        this.Cursor = Cursors.Default;
        //                        //
        //                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
        //                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
        //                        //
        //                        lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);

        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == strNOTAVAILABLE) //-- NOT AVAILABLE / 
        //                        {
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid";
        //                            lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
        //                            //--                
        //                            strInvalidPAN = strInvalidPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                            //
        //                            if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
        //                                                     "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
        //                                                     "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
        //                            }
        //                        }
        //                        else //-- NAME PRESENT / ACTIVE
        //                        {
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active"; 
        //                            lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
        //                            //--
        //                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                                    "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                    "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) + "')");
        //                            }
        //                            else
        //                            {
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) + "' " +
        //                                    " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                            }
        //                            //-- NAME NOT MATCHED
        //                            if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value))
        //                            {
        //                                if (dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor != Color.Red)
        //                                {
        //                                    dgvDeductees.Rows[i].Cells[intNameEntered].Style.BackColor = Color.Tan;
        //                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active\nbut name unmatched with the entered value"; 
        //                                    lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);
        //                                    //--                
        //                                    strUnmatchedPAN = strUnmatchedPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                                    //--
        //                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                                    {
        //                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                                                                 "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
        //                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) + "')");
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //--
        //                }
        //                //--
        //                if (response.Respons == enmResponse.SessionTimeout)
        //                {
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    objRetval.Add(enmRequestType.PanValidation);
        //                    objRetval.Add(objTracesConnect);
        //                    e.Result = objTracesConnect;
        //                    break;
        //                }
        //                //--

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
        //                    cmnService.J_UserMessage("Bulk PAN Validation exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
        //                    return;
        //                }
        //                //--
        //                if (j == 10)
        //                {
        //                    cmnService.J_UserMessage("Only 10 PANs can be validated per return using the Trial Version");
        //                    blnVerificationComplete = false;
        //                    break;
        //                }
        //            }
        //            //--
        //            if (bgwPANVerification.CancellationPending)//checks for cancel request
        //            {
        //                blnVerificationComplete = false;
        //                //pctGreenDownArrow.Visible = false;
        //                break;
        //            }
        //            //
        //            if (i > 18)
        //                dgvDeductees.FirstDisplayedScrollingRowIndex = dgvDeductees.FirstDisplayedScrollingRowIndex + 1;
        //        }
        //        //
        //        blnVerificationComplete = true;
        //        //pctGreenDownArrow.Visible = false;
        //    }
        //    //--
        //    if (bgwPANVerification.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        blnVerificationComplete = false; 
        //        //pctGreenDownArrow.Visible = false;
        //        return;
        //    }
        //}
        #endregion



        #region bgwPANVerification_DoWork
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    ArrayList objRetval = new ArrayList();
        //    TracesResponse objResponse = new TracesResponse();
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    string strSTATUS = ""; string strVerifiedNAME = "", strPANStatus = "";
        //    //--
        //    //--
        //    intClicked = 0;
        //    if (btnVerification.Text != strbtnVerification)
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        //pctGreenDownArrow.Visible = true;
        //        //--
        //        for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
        //        {
        //            //
        //            #region COMMENT
        //            //if (Convert.ToString(dgvDeductees.Rows[i].Cells[0].Value).ToUpper() == "TRUE")
        //            //{
        //            //    this.Cursor = Cursors.Default;
        //            //    //
        //            //    dgvDeductees.Rows[i].Cells[intNameVerified].Value = dgvDeductees.Rows[i].Cells[intStatusId + 2].Value;
        //            //    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //            //    lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
        //            //    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active";
        //            //    //
        //            //    if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value))
        //            //    {
        //            //        dgvDeductees.Rows[i].Cells[intNameEntered].Style.BackColor = Color.Tan;
        //            //        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active\nbut name unmatched with the entered value";
        //            //        lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);
        //            //        strUnmatchedPAN = strUnmatchedPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //            //        //
        //            //        if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //            //        {
        //            //            dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //            //                                     "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //            //                                     "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //            //                                     "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //            //        }
        //            //    }
        //            //    //
        //            //}
        //            //else
        //            #endregion
        //            //
        //            if (TdsMan.T_CheckInternetConnectivty() == false)
        //            {
        //                this.Cursor = Cursors.Default;
        //                //
        //                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
        //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified\n[Internet Connectivity not found]";
        //                //
        //                lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
        //            }
        //            else if (Convert.ToString(dgvDeductees.Rows[i].Cells[0].Value).ToUpper() == "TRUE")
        //            {
        //                intClicked = intClicked + 1;
        //                //
        //                dgvDeductees.Rows[i].Cells[intStatusId].Value = "";
        //                dgvDeductees.Rows[i].Cells[intNameVerified].Value = "";
        //                //
        //                //TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
        //                //                                                                   out strVerifiedNAME);
        //                //-- 2023/10/11
        //                //TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
        //                //                                                                   out strVerifiedNAME, out strPANStatus);
        //                //-- 2026/04/09
        //                TracesResponse response = objTracesConnect.RequestForPANValidation_New(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
        //                                                                                   out strVerifiedNAME, out strPANStatus);
        //                //--
        //                this.Cursor = Cursors.Default;
        //                //
        //                if (response.Respons == enmResponse.Success)
        //                {
        //                    //dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //                    //--
        //                    dgvDeductees.Rows[i].Cells[intNameVerified].Value = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
        //                    //--
        //                    if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == "") //-- NO NAME
        //                    {
        //                        this.Cursor = Cursors.Default;
        //                        //
        //                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
        //                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
        //                        //
        //                        lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);

        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == strNOTAVAILABLE) //-- NOT AVAILABLE / 
        //                        {
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.OrangeRed;

        //                            dgvDeductees.Rows[i].Cells[intStatusId].Value = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));

        //                            dgvDeductees.Rows[i].Cells[intVerifyId].Value = "1";
        //                            dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid";
        //                            lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
        //                            //--                
        //                            strInvalidPAN = strInvalidPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                            //
        //                            if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
        //                                                     "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
        //                                                     "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
        //                            }
        //                            //
        //                            //--
        //                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME, VERIFIED_STATUS) " +
        //                                    "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                    "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                     GetServerDateTime() + ", " + T_PANVerificationStatusId.INVALID + ")");
        //                            }
        //                            else
        //                            {
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                    "CHECKED_DATE_TIME = " + GetServerDateTime() + ", VERIFIED_STATUS = " + T_PANVerificationStatusId.INVALID +
        //                                    " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                            }
        //                            //-- 2022/06/07
        //                            //if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            //{
        //                            //    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                            //                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                            //                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                            //                             "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                            //}
        //                            //
        //                        }
        //                        else //-- NAME PRESENT / ACTIVE
        //                        {
        //                            if (strPANStatus.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE.ToString() || strPANStatus.ToUpper() == T_PANVerificationStatus.ACTIVE.ToString())// "VALID AND OPERATIVE")
        //                            {
        //                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
        //                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = T_PANVerificationStatus.VALID_OPERATIVE.ToString();// "VALID AND OPERATIVE";
        //                                dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.VALID_OPERATIVE;// "0";
        //                                //
        //                                lblVerifiedOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedOperativeNo.Text) + 1);
        //                            }
        //                            else //if (strSTATUS.ToUpper() == "VALID AND INOPERATIVE")
        //                            {
        //                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
        //                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();// "VALID AND INOPERATIVE";
        //                                dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.VALID_INOPERATIVE;
        //                                //
        //                                lblVerifiedInOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedInOperativeNo.Text) + 1);
        //                            }
        //                            //
        //                            //
        //                            dgvDeductees.Rows[i].Cells[intStatusId].Value = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        //                            //
        //                            //lblVerifiedOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedOperativeNo.Text) + 1);
        //                            //--
        //                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            {
        //                                if (strPANStatus.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE.ToString())
        //                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME) " +
        //                                    "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                    "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                     GetServerDateTime() + ")");
        //                                else
        //                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME, VERIFIED_STATUS) " +
        //                                    "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                    "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                     GetServerDateTime() + "," + T_PANVerificationStatusId.VALID_INOPERATIVE + ")");
        //                            }
        //                            //else
        //                            //{
        //                            if (strPANStatus.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE.ToString() || strPANStatus.ToUpper() == T_PANVerificationStatus.ACTIVE.ToString())
        //                            {
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                "CHECKED_DATE_TIME = " + GetServerDateTime() + ", VERIFIED_STATUS = " + T_PANVerificationStatusId.VALID_OPERATIVE +
        //                                " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                                //-- 2022/06/07
        //                                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                                {
        //                                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                                                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                                                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                                             "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "'," +
        //                                "CHECKED_DATE_TIME = " + GetServerDateTime() + ", VERIFIED_STATUS = " + T_PANVerificationStatusId.VALID_INOPERATIVE +
        //                                " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
        //                                //-- 2023/10/12
        //                                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                                {
        //                                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                                                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                                                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                                             "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                                }
        //                            }
        //                            //}
        //                            //////-- 2022/06/07
        //                            //if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                            //{
        //                            //    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                            //                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                            //                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                            //                             "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                            //}
        //                            //
        //                            //-- NAME NOT MATCHED
        //                            //if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value))
        //                            if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != GenMaskedName(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)))
        //                            {
        //                                if (dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor != Color.OrangeRed)
        //                                {
        //                                    dgvDeductees.Rows[i].Cells[intNameVerified].Style.BackColor = Color.Tan;

        //                                    ////dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
        //                                    ////dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active\nbut name unmatched with the entered value";
        //                                    lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);
        //                                    //--                
        //                                    strUnmatchedPAN = strUnmatchedPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
        //                                    //--
        //                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
        //                                    {
        //                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
        //                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
        //                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
        //                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //--
        //                }
        //                //-- 2019/12/23
        //                else if (response.Respons == enmResponse.Failed && response.Message.Contains("Internal Server Error") == true)
        //                {
        //                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Gray;
        //                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Internal Server Error (returned from TRACES)";
        //                }
        //                //--
        //                if (response.Respons == enmResponse.SessionTimeout)
        //                {
        //                    objResponse.Respons = enmResponse.SessionTimeout;
        //                    objRetval.Add(enmRequestType.PanValidation);
        //                    objRetval.Add(objTracesConnect);
        //                    e.Result = objTracesConnect;
        //                    break;
        //                }
        //                //--

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
        //                    cmnService.J_UserMessage("Bulk PAN Validation exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
        //                    return;
        //                }
        //                //--
        //                if (j == 10)
        //                {
        //                    cmnService.J_UserMessage("Only 10 PANs can be validated per return using the Trial Version");
        //                    blnVerificationComplete = false;
        //                    break;
        //                }
        //            }
        //            //--
        //            if (bgwPANVerification.CancellationPending)//checks for cancel request
        //            {
        //                blnVerificationComplete = false;
        //                //pctGreenDownArrow.Visible = false;
        //                break;
        //            }
        //            //
        //            if (i > 18)
        //                dgvDeductees.FirstDisplayedScrollingRowIndex = dgvDeductees.FirstDisplayedScrollingRowIndex + 1;
        //        }
        //        //
        //        blnVerificationComplete = true;
        //        //pctGreenDownArrow.Visible = false;
        //    }
        //    //--
        //    if (bgwPANVerification.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        blnVerificationComplete = false;
        //        //pctGreenDownArrow.Visible = false;
        //        return;
        //    }
        //}
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList objRetval = new ArrayList();
            Label.CheckForIllegalCrossThreadCalls = false;

            string strVerifiedNAME = "", strPANStatus = "";
            intClicked = 0;

            // LOAD CSV DATA ONCE
            string csvPath = strSaveDownloadedCSVFilePath;
            //var panStatusDict = ReadPANStatus(csvPath);
            var panStatusDict = ReadPANNameStatus(csvPath);

            if (btnVerification.Text != strbtnVerification)
            {
                this.Cursor = Cursors.WaitCursor;

                for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
                {
                    if (Convert.ToString(dgvDeductees.Rows[i].Cells[0].Value).ToUpper() == "TRUE")
                    {
                        intClicked++;

                        string pan = Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value)
                                        .Trim().ToUpper();

                        dgvDeductees.Rows[i].Cells[intStatusId].Value = "";
                        dgvDeductees.Rows[i].Cells[intNameVerified].Value = "";

                        // CHECK FROM CSV
                        if (panStatusDict.ContainsKey(pan))
                        {
                            var panInfo = panStatusDict[pan];

                            strVerifiedNAME = panInfo.Name.ToUpper().Trim()
                                                    .Replace("AMP;", "")
                                                    .Replace("  ", " ");

                            strPANStatus = panInfo.Status.ToUpper();

                            dgvDeductees.Rows[i].Cells[intNameVerified].Value = strVerifiedNAME;

                            //  INVALID CASE
                            if (string.IsNullOrEmpty(strVerifiedNAME) || strVerifiedNAME == strNOTAVAILABLE)
                            {
                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.OrangeRed;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid";
                                dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.INVALID;

                                lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);

                                strInvalidPAN += "," + pan;
                                //
                                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
                                                         "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
                                                         "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                }
                                dgvDeductees.Rows[i].Cells[intStatusId].Value = DateTime.Now.ToString("dd/MM/yyyy");
                                // DB UPDATE
                                if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME, VERIFIED_STATUS) " +
                                        "VALUES('" + pan + "','" +
                                        cmnService.J_ReplaceQuote(strVerifiedNAME) + "'," +
                                        GetServerDateTime() + "," +
                                        dgvDeductees.Rows[i].Cells[intVerifyId].Value + ")");
                                }
                                else
                                {
                                    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" +
                                        cmnService.J_ReplaceQuote(strVerifiedNAME) + "'," +
                                        "CHECKED_DATE_TIME = " + GetServerDateTime() +
                                        ", VERIFIED_STATUS = " + dgvDeductees.Rows[i].Cells[intVerifyId].Value +
                                        " WHERE PAN ='" + pan + "'");
                                }
                            }
                            else
                            {
                                // VALID OPERATIVE
                                if (strPANStatus == "VALID")
                                {
                                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "VALID";
                                    dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.VALID_OPERATIVE;

                                    lblVerifiedOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedOperativeNo.Text) + 1);
                                    //
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                                    }
                                    //
                                }
                                // VALID INOPERATIVE
                                else if (strPANStatus.Contains("INOPERATIVE"))
                                {
                                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Yellow;
                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "VALID AND INOPERATIVE";
                                    dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.VALID_INOPERATIVE;

                                    lblVerifiedInOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedInOperativeNo.Text) + 1);
                                    //
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                                    }
                                    //
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                                    }
                                }
                                // VALID OPERATIVE
                                else if (strPANStatus.Contains("VALID") && strPANStatus.Contains("OPERATIVE"))
                                {
                                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "VALID AND OPERATIVE";
                                    dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.VALID_OPERATIVE;

                                    lblVerifiedOperativeNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedOperativeNo.Text) + 1);
                                    //
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                                    }
                                    //
                                }
                                else
                                {
                                    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid PAN";
                                    dgvDeductees.Rows[i].Cells[intVerifyId].Value = T_PANVerificationStatusId.INVALID;

                                    lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                                    //
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
                                                             "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
                                                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                                    }

                                }

                                dgvDeductees.Rows[i].Cells[intStatusId].Value = DateTime.Now.ToString("dd/MM/yyyy");

                                // NAME MATCH CHECK (MASKED)
                                if (strVerifiedNAME != GenMaskedName(
                                        Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)))
                                {
                                    dgvDeductees.Rows[i].Cells[intNameVerified].Style.BackColor = Color.Tan;

                                    lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);

                                    strUnmatchedPAN += "," + pan;
                                    //--
                                    if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                                    {
                                        dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                                                                 "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                                                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                                                                 "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                                    }
                                    //--
                                }

                                // DB UPDATE
                                if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + pan + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, CHECKED_DATE_TIME, VERIFIED_STATUS) " +
                                        "VALUES('" + pan + "','" +
                                        cmnService.J_ReplaceQuote(strVerifiedNAME) + "'," +
                                        GetServerDateTime() + "," +
                                        dgvDeductees.Rows[i].Cells[intVerifyId].Value + ")");
                                }
                                else
                                {
                                    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" +
                                        cmnService.J_ReplaceQuote(strVerifiedNAME) + "'," +
                                        "CHECKED_DATE_TIME = " + GetServerDateTime() +
                                        ", VERIFIED_STATUS = " + dgvDeductees.Rows[i].Cells[intVerifyId].Value +
                                        " WHERE PAN ='" + pan + "'");
                                }
                            }
                        }
                        else
                        {
                            if (chkViewTokenDetails.Checked == false)
                            {
                                // NOT FOUND IN CSV
                                dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Gray;
                                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Found in CSV";

                                lblNotVerifiedNo.Text =
                                    Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
                            }
                        }
                    }

                    j++;

                    // TRIAL LIMIT LOGIC (UNCHANGED)
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    {
                        strSQL = "UPDATE MST_SETUP SET TRIAL_PAN_VERIFY_RECORDS = TRIAL_PAN_VERIFY_RECORDS + 1 ";
                        dmlService.J_ExecSql(strSQL);

                        if (cmnService.J_ReturnInt16Value(Convert.ToString(
                            dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP")))
                            >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
                        {
                            cmnService.J_UserMessage("Bulk PAN Validation exhausted..."); this.Cursor = Cursors.Default;
                            return;
                        }

                        if (j == 10)
                        {
                            cmnService.J_UserMessage("Only 10 PANs allowed in trial"); this.Cursor = Cursors.Default;
                            blnVerificationComplete = false;
                            break;
                        }
                    }

                    if (bgwPANVerification.CancellationPending)
                    {
                        blnVerificationComplete = false;
                        break;
                    }

                    if (i > 18)
                        dgvDeductees.FirstDisplayedScrollingRowIndex++;
                }

                blnVerificationComplete = true;
            }
            this.Cursor = Cursors.Default;

            if (bgwPANVerification.CancellationPending)
            {
                this.Cursor = Cursors.Default;
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
            if (File.Exists(strSaveDownloadedCSVFilePath))
            {
                File.Delete(strSaveDownloadedCSVFilePath);
            }
            //
            if (File.Exists(filePathCSV))
            {
                File.Delete(filePathCSV);
            }
            //if(bgwPANVerification.CancellationPending==false)
            if (blnVerificationComplete == true)
            {
                if(intClicked==0)
                    cmnService.J_UserMessage("No record selected");
                else
                    cmnService.J_UserMessage("Validation completed");
                btnVerification.Enabled = true;
                btnVerification.BackColor = Color.Lavender;
                //
                //--
                grpStatus.Enabled = true;
                //
                btnVerification.Enabled = false;
                //
                dgvDeductees.Enabled = true;
                grpReturnSelection.Enabled = true;
                grpRegularReturn.Enabled = true;
                grpCorrectionReturn.Enabled = true;
                j = 0;
                //                
            }
            else
            {
                cmnService.J_UserMessage("Validation stopped", MessageBoxIcon.Exclamation);
                blnVerificationComplete = false;
                grpReturnSelection.Enabled = true;
                grpRegularReturn.Enabled = true;
                grpCorrectionReturn.Enabled = true;
                //bgwPANVerification.CancelAsync();
                //bgwPANVerification.Dispose();
                //bgwPANVerification = null;
            }
            //
            btnVerification.Text = strbtnVerification;
            btnVerification.ForeColor = Color.Black;
            //blnVerificationComplete = false;
            //
            if (cmnService.J_ReturnInt32Value(lblVerifiedOperativeNo.Text) > 0)
                btnPrintValidOperativePAN.Enabled = true;
            else
                btnPrintValidOperativePAN.Enabled = false;
            //--
            if (cmnService.J_ReturnInt32Value(lblVerifiedInOperativeNo.Text) > 0)
                btnPrintValidInOperativePAN.Enabled = true;
            else
                btnPrintValidInOperativePAN.Enabled = false;
            //--
            if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                btnPrintInvalidPAN.Enabled = true;
            else
                btnPrintInvalidPAN.Enabled = false;
            //--
            if (cmnService.J_ReturnInt32Value(lblUnmatchedNo.Text) > 0)
            {
                btnPrintUnmatched.Enabled = true;
                //if (rbnRegularReturn.Checked == true)
                //    btnUpdateMasterData.Visible = true;
            }
            else
            {
                btnPrintUnmatched.Enabled = false;
                if (rbnRegularReturn.Checked == true)
                    btnUpdateMasterData.Visible = false;
            }
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
            bgwPANVerification.CancelAsync();
            bgwPANVerification.Dispose();
            //
            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = "";
            TDSMAN.Classes.TDSMAN.T_pQuarter = "";
            //
            GC.Collect();
            //
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
            //btnPrintInvalidPAN_MouseClick(sender, (MouseEventArgs)e);
            //try
            //{
            //    if (strInvalidPAN == "") { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
            //    //
            //    if (cmnService.J_UserMessage("Do you want to take print of the Invalid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;
            //    //
            //    //TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
            //    //
            //    RptDialog rptDialog = new RptDialog();
            //    if (grpReturnSelection.Visible == false)
            //    {
            //        TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
            //        rptDialog.PrintInvalidPAN(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null);
            //    }
            //    else
            //    {
            //        if (rbnCorrectionReturn.Checked == true)
            //            rptDialog.PrintInvalidPAN(lngBasicInfoID, "FALSE");
            //        else if (rbnRegularReturn.Checked == true)
            //            rptDialog.PrintInvalidPAN(lngBasicInfoID, "TRUE");
            //    }
            //    //}
            //}
            //catch
            //{
            //}
        }
        #endregion

        #region btnPrintInvalidPAN_MouseClick
        private void btnPrintInvalidPAN_MouseClick(object sender, MouseEventArgs e)
        {
            if (strInvalidPAN == "") { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
            //--
            if (e.Button == MouseButtons.Left)
            {
                cntxtMnuGrpInvalidPAN.Show(btnPrintInvalidPAN, new Point(e.X, e.Y));
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                   TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS)
                {
                    if (TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath != "")
                    {
                        tlStrp.Visible = true;
                        mnuMarkExcelInvalid.Visible = true;
                    }
                }
            }
        }
        #endregion


        #region mnuCntxtMenuPrint_Click
        private void mnuCntxtMenuPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Do you want to take print of the Invalid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                //TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                if (grpReturnSelection.Visible == false)
                {
                    TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                    if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                        rptDialog.PrintInvalidPAN_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, "SD", "");
                    else
                        rptDialog.PrintInvalidPAN_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, "");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintInvalidPAN_RDLC(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintInvalidPAN_RDLC(lngBasicInfoID, "TRUE", "");
                }
            }
            catch
            {

            }
        }
        #endregion


        #region btnExitExportPanel_Click
        private void btnExitExportPanel_Click(object sender, EventArgs e)
        {
            grpExport.Visible = false;
        }
        #endregion

        #region btnSelectExcelPath_Click

        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            //Show FolderBrowserDialog
            DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult.Equals(DialogResult.OK))
            {
                //Show selected folder path in textbox1.
                txtExcelPath.Text = folderBrowserDlg.SelectedPath;
                //Browsing start from root folder.
                Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            }
        }

        #endregion


        #region mnuCntxtMenuExport_Click
        private void mnuCntxtMenuExport_Click(object sender, EventArgs e)
        {
            grpExport.Visible = true;
            if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                grpExport.Location = new Point(0, grpExport.Location.Y);
            txtDestinationFileName.Text = "InvalidPANList.csv";
            txtDestinationFileNameHidden.Text = "InvalidPANList.csv";
        }
        #endregion

        #region btnExportToExcel_Click
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
                {
                    cmnService.J_UserMessage("Please select specific folder for export");
                    btnExportToExcel.Select();
                    return;
                }
                //
                if (string.IsNullOrEmpty(txtDestinationFileName.Text.Trim()))
                {
                    cmnService.J_UserMessage("Please specific the File Name");
                    txtDestinationFileName.Select();
                    return;
                }
                //
                if (File.Exists(Path.Combine(txtExcelPath.Text.Trim(), txtDestinationFileName.Text.Trim())))
                {
                    cmnService.J_UserMessage("File exists with same name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDestinationFileName.Select();
                    return;
                }
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                if (txtDestinationFileNameHidden.Text == "InvalidPANList.csv")
                {
                    if (cmnService.J_UserMessage("Do you want to export list of Invalid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    //TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                    //
                    RptDialog rptDialog = new RptDialog();
                    if (grpReturnSelection.Visible == false)
                    {
                        TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                        rptDialog.PrintInvalidPAN_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintInvalidPAN_RDLC(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintInvalidPAN_RDLC(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                }
                else if (txtDestinationFileNameHidden.Text == "NameDifferenceList.csv")
                {
                    if (cmnService.J_UserMessage("Do you want to export list of unmatched name(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                    //
                    RptDialog rptDialog = new RptDialog();
                    //
                    if (grpReturnSelection.Visible == false)
                    {
                        rptDialog.PrintUnmatchedNames_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintUnmatchedNames_RDLC(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintUnmatchedNames_RDLC(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                }
                else if (txtDestinationFileNameHidden.Text == "ValidPANOperative.csv") //-- 2022/06/07
                {
                    if (cmnService.J_UserMessage("Do you want to export list of Valid & Operative PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                    //
                    RptDialog rptDialog = new RptDialog();
                    //
                    if (grpReturnSelection.Visible == false)
                    {
                        rptDialog.PrintValidOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintValidOperativePans_RDLC(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintValidOperativePans_RDLC(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                }
                else if (txtDestinationFileNameHidden.Text == "ValidPANInoperative.csv") //-- 2023/10/12
                {
                    if (cmnService.J_UserMessage("Do you want to export list of Valid & Inoperative PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                    //
                    RptDialog rptDialog = new RptDialog();
                    //
                    if (grpReturnSelection.Visible == false)
                    {
                        rptDialog.PrintValidInOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintValidInOperativePans_RDLC(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintValidInOperativePans_RDLC(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                }
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Export Completed");
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region btnPrintUnmatched_MouseClick
        private void btnPrintUnmatched_MouseClick(object sender, MouseEventArgs e)
        {
            if (strUnmatchedPAN == "") { cmnService.J_UserMessage("No unmatched name(s) to print!!"); return; }
            //--
            if (e.Button == MouseButtons.Left)
                cntxtMnuGrpNameDifference.Show(btnPrintUnmatched, new Point(e.X, e.Y));
        }
        #endregion

        #region mnuCntxtMenuPrintNameDifference_Click
        private void mnuCntxtMenuPrintNameDifference_Click(object sender, EventArgs e)
        {
            try
            {
                if (strUnmatchedPAN == "") { cmnService.J_UserMessage("No unmatched name(s) to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print of the unmatched name(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                //
                if (grpReturnSelection.Visible == false)
                {
                    if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                        rptDialog.PrintUnmatchedNames_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, "SD", "");
                    else
                        rptDialog.PrintUnmatchedNames_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, "");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintUnmatchedNames_RDLC(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintUnmatchedNames_RDLC(lngBasicInfoID, "TRUE", "");
                }
                //
            }
            catch
            {
            }
        }
        #endregion

        #region mnuCntxtMenuExportNameDifference_Click
        private void mnuCntxtMenuExportNameDifference_Click(object sender, EventArgs e)
        {
            grpExport.Visible = true;
            if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                grpExport.Location = new Point(0, grpExport.Location.Y);
            txtDestinationFileName.Text = "NameDifferenceList.csv";
            txtDestinationFileNameHidden.Text = "NameDifferenceList.csv";
        }
        #endregion


        #region btnPrintInvalidPAN_MouseMove
        private void btnPrintInvalidPAN_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTip.SetToolTip(btnPrintInvalidPAN, "Print inactive PAN(s)"); 
        }
        #endregion

        #region btnPrintUnmatched_MouseMove
        private void btnPrintUnmatched_MouseMove(object sender, MouseEventArgs e)
        {
            //tllTip.SetToolTip(btnPrintUnmatched, "Print unmatched name(s)");
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
                if (txtTANNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter TAN");
                    txtTANNo.Select();
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
                strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";
                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount == 0)
                {
                    //insering new record in the tan login master
                    strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                             "VALUES( '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                    dmlService.J_ExecSql(strSQL);
                }
                else
                {
                    //updating the existing record in the master

                    strSQL = "UPDATE MST_TAN_ACCOUNT " +
                             "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "', " +
                             "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserID.Text) + "', " +
                             "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                             "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' ";

                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                {
                    //--
                    intPANId = 1;
                    intNameEntered = 2;
                    intNameVerified = 3;
                    intStatusId = 4;
                    intVerifyId = 5;
                    //--
                }
                else if(rbnRegularReturn.Checked==true)
                {
                    //--
                    intID = 1;
                    intPANId = 2;
                    intNameEntered = 3;
                    intNameVerified = 4;
                    intStatusId = 5;
                    intVerifyId = 6;
                    //--
                }
                else if (rbnCorrectionReturn.Checked==true)
                {
                    //--
                    intPANId = 1;
                    intNameEntered = 2;
                    intNameVerified = 3;
                    intStatusId = 4;
                    intVerifyId = 5;
                    //--
                }
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTANNo.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                //-------------------------------------------
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);
                //--
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
            }
        }
        #endregion

        #region btnPrintUnmatched_Click
        private void btnPrintUnmatched_Click(object sender, EventArgs e)
        {

            //btnPrintUnmatched_MouseClick(sender, (MouseEventArgs)e);
            //try
            //{
            //    if (strUnmatchedPAN == "") { cmnService.J_UserMessage("No unmatched name(s) to print!!"); return; }
            //    //
            //    if (cmnService.J_UserMessage("Do you want to take print of the unmatched name(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;
            //    //
            //    TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
            //    //
            //    RptDialog rptDialog = new RptDialog();
            //    //
            //    if (grpReturnSelection.Visible == false)
            //    {
            //        rptDialog.PrintUnmatchedNames(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null);
            //    }
            //    else
            //    {
            //        if (rbnCorrectionReturn.Checked == true)
            //            rptDialog.PrintUnmatchedNames(lngBasicInfoID, "FALSE");
            //        else if (rbnRegularReturn.Checked == true)
            //            rptDialog.PrintUnmatchedNames(lngBasicInfoID, "TRUE");
            //    }
            //    //
            //}
            //catch
            //{
            //}
        }
        #endregion


        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
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
                    //System.Threading.Thread.Sleep(8000);
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
                    //
                    foreach (DataGridViewRow row in rowcoll)
                    {
                        strPAN = row.Cells[0].Value.ToString();
                        //TracesResponse response = objTracesConnect.RequestForPANValidation(strPAN);
                        TracesResponse response = objTracesConnect.RequestForPANValidation_New(strPAN);
                        //
                        if (response.Respons == enmResponse.Success)
                        {
                            bnlSuccess = true;
                            PANDetails objDetails = (PANDetails)response.CustomeTypes;
                            this.bgWorker.ReportProgress(0, objDetails);

                        }
                        //
                        if (response.Respons == enmResponse.SessionTimeout)
                        {
                            objResponse.Respons = enmResponse.SessionTimeout;
                            objRetval.Add(enmRequestType.PanValidation);
                            objRetval.Add(objResponse);
                            e.Result = objRetval;
                            break;
                        }
                    }
                    //
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
                        //pnlTracesMessage.Visible = false;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //////System.Threading.Thread.Sleep(2000);
                            //////objList.Add(enmRequestType.DownloadCSVFile);
                            SaveLogData(lngBasicInfoID, txtTANNo.Text, 0, filePathCSVTokenNo, dgvDeductees.RowCount);
                            strLogTokenNo = FetchTokenNo(lngBasicInfoID, txtTANNo.Text, 0, out strLogTokenNoDateTime);
                            //lblStatus.Visible = true;
                            lblStatus.Text = "CSV file uploaded...";
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
                            //cmnService.J_UserMessage("Data parsing to TRACES failed.\nPlease try later.");
                            //We have uploaded XXX PANs for verification to TRACES, which is still pending at their end vide Token No.XXXXXXXX.Please check back after 15 minutes or earlier.
                            if (filePathCSVTokenNo != "")
                            {
                                //cmnService.J_UserMessage("We have uploaded " + dgvDeductees.RowCount + " PAN(s) for verification to TRACES, which is still pending at their end vide Token No. " + filePathCSVTokenNo + "." +
                                //                     "\nPlease check back after 15 minutes or earlier.");
                                cmnService.J_UserMessage( + dgvDeductees.RowCount + " PAN(s) have been uploaded successfully to TRACES for verification vide Token No. " + filePathCSVTokenNo + "." +
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
                                grpReturnSelection.Enabled = true;
                                grpRegularReturn.Enabled = true;
                                grpCorrectionReturn.Enabled = true;
                            }
                            else
                                cmnService.J_UserMessage("Data parsing to TRACES failed.\nPlease try later.");
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
                            cmnService.J_UserMessage("Token No. " + strLogTokenNo + " (" + DateTime.Parse(strLogTokenNoDateTime).ToString("dd/MM/yyyy HH:mm") + ") with "  + dgvDeductees.RowCount + " PAN(s) is still pending with TRACES." +
                                                     "\nPlease check back after sometime.");
                            btnVerification.Enabled = true;
                            //btnVerification.BackColor = Color.Lavender;
                            btnVerification.Text = strbtnVerification;
                            btnVerification.ForeColor = Color.Black;
                            //
                            //--
                            grpStatus.Enabled = true;
                            //
                            btnVerification.Enabled = false;
                            //
                            dgvDeductees.Enabled = true;
                            grpReturnSelection.Enabled = true;
                            grpRegularReturn.Enabled = true;
                            grpCorrectionReturn.Enabled = true;
                            return;
                        }
                        break;
                    case enmRequestType.CheckCSVStatus:
                        //lblStatus.Visible = true;
                        lblStatus.Text = "Token No. " + filePathCSVTokenNo;
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
                            lblStatus.Text = "Trying Token No. " + filePathCSVTokenNo + "... " + intCSVStatusRetry + " times....";
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
                                pnlTracesMessage.Visible = false; lblStatus.Visible = false;
                                //
                                //cmnService.J_UserMessage("TRACES is still processing the PAN file.\nPlease retry after some time.");
                                //cmnService.J_UserMessage("We have uploaded " + dgvDeductees.RowCount + " PAN(s) for verification to TRACES, which is still pending at their end vide Token No. " + filePathCSVTokenNo + "." +
                                //                     "\nPlease check back after 15 minutes or earlier.");
                                //cmnService.J_UserMessage("Token No. " + strLogTokenNo + " (" + DateTime.Parse(strLogTokenNoDateTime).ToString("dd/MM/yyyy HH:mm") + ") with " + dgvDeductees.RowCount + " PAN(s) is still pending with TRACES." +
                                //                     "\nPlease check back after sometime.");

                                cmnService.J_UserMessage(dgvDeductees.RowCount + " PAN(s) have been uploaded successfully to TRACES for verification vide Token No. " + strLogTokenNo + "." +
                                                     "\nThe validated response is currently pending at the TRACES end due to high processing load." +
                                                     "\nPlease check back after sometime.");

                                return;
                            }
                        }
                        break;
                    case enmRequestType.DownloadCSVFile:
                        pnlTracesMessage.Visible = false;
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            //objList.Add(enmRequestType.ExtractZIPFile);
                            //-------------------------------------------
                            //if (!bgWorker.IsBusy)
                            //    bgWorker.RunWorkerAsync(objList);
                            //cmnService.J_UserMessage("Downloaded");
                            //-- 
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
                        //
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
                        txtTANNo.Text = "";
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

        #region bgWorker_ProgressChanged
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //PANDetails objpanDetails = e.UserState as PANDetails;
            //_results.Add(objpanDetails);
        }
        #endregion

        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            //this.pgTimer.Interval = 1000;
            //this.pBar.Step = 5;

            // Update progress bar
            if ((pBar.Value + pBar.Step) > pBar.Maximum)
            {
                pBar.Value = pBar.Minimum;
            }
            else
            {
                pBar.Value += pBar.Step;
            }
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }

        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            grpLoginDetails.Visible = false;
            //--
            grpStatus.Enabled = true;
            grpButtons.Enabled = true;
            dgvDeductees.Enabled = true;
            grpReturnSelection.Enabled = true;
            grpRegularReturn.Enabled = true;
            grpCorrectionReturn.Enabled = true;
            //

        }
        #endregion

        //-- Added By Abhishek Dey On 20/08/2018 --
        #region lstDeducteeHelp_KeyPress
        private void lstDeducteeHelp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            //--
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
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
                if (TdsMan.gTANNoPANNoValidation(txtTANNo, e, T_TANPAN.TAN) == false)
                e.Handled = true;
        }

        #endregion

        #region txtTANNo_KeyDown
        private void txtTANNo_KeyDown(object sender, KeyEventArgs e)
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

        #region txtTANNo_TextChanged
        private void txtTANNo_TextChanged(object sender, EventArgs e)
        {
            //cmbFAYear_SelectedIndexChanged(sender, e);

            IDataReader drdShowDeducteeHelp = null;
            //--
            try
            {
                if (blnExit == false)
                    return;

                if (txtTANNo.Text.Trim() == "")
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
                         "WHERE  TAN_NO LIKE '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "%' " +
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
                    //lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12)
                                                                + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15)
                                                                + TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10)
                                                                + " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
                                                                Convert.ToInt32(drdShowDeducteeHelp["TAN_ACCOUNT_ID"])));
                        //--
                        //if (lstDeducteeHelp.Height <= 300)
                        //    lstDeducteeHelp.Height = lstDeducteeHelp.Height + 19;
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

        #region txtTANNo_Leave
        private void txtTANNo_Leave(object sender, EventArgs e)
        {
            if (txtTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

        }
        #endregion

        #region chkBoxNewEntriesOnly_CheckedChanged
        private void chkBoxNewEntriesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnRegularReturn.Checked == true)
            {
                cmbLoadGrid_SelectedIndexChanged(sender, e);
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                dgcViewBatch_Click(sender, e);
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
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "80", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "80", "S", "", "", "", "T"},
                                        {"Status", "50", "S", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
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
                         "         MST_EMPLOYEE.EMPLOYEE_PAN  AS DEDUCTEE_PAN," +
                         "         MST_EMPLOYEE.EMPLOYEE_NAME AS DEDUCTEE_NAME," +
                         "         ''                         AS DEDUCTEE_NAME_V," +
                         "         ''                         AS STATUS," +
                         "         ''                         AS VERIFIED," +
                         "         ''                         AS VERIFIED_NAME " +
                         "FROM     TRN_DEDUCTEE_DETAILS," +
                         "         MST_EMPLOYEE  " +
                         "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
                         "AND      1=2 ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                grpStatus.Text = " Total Record";//(s): " + dgvDeductees.Rows.Count + " ";
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGrid(long BasicInfoID, string FormNo)
        private void LoadDeducteeGrid(long BasicInfoID, string FormNo)
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                if (rbnAllPANs.Checked == true)
                    strFilter = "";
                else if (rbnReVerifyValidPANs.Checked == true)
                    strFilter = "0";
                else if (rbnOnlyUnVerifiedPANs.Checked == true)
                    strFilter = "1";
                else if (rbnReVerifyInoperativePANs.Checked == true)
                    strFilter = "2";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status  ", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
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
                //string[,] strVerifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T"},
                //                             { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_PAN_ID", "F"} };
                //
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == "")
                {
                    //TDSMAN.Classes.TDSMAN.T_pTabFormCaption = cmbFormNo.Text;
                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = FormNo;
                    TDSMAN.Classes.TDSMAN.T_pQuarter = cmbQuarter.Text;
                }
                //
                #region COMMENT
                //if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q &&
                //    TDSMAN.Classes.TDSMAN.T_pQuarter == T_Qtr.Q4)
                //{                    
                //    //-- ANIK @ 2014-04-28
                //    strSQL = @"SELECT  DISTINCT MST_EMPLOYEE.EMPLOYEE_ID   AS EMPLOYEE_ID,         
                //                      MST_EMPLOYEE.EMPLOYEE_PAN            AS EMPLOYEE_PAN,          
                //                      MST_EMPLOYEE.EMPLOYEE_NAME           AS EMPLOYEE_NAME,           
                //                      ''                                   AS EMPLOYEE_NAME_V,        
                //                      ''                                   AS STATUS,         
                //                      IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED,
                //                      MST_VERIFIED_PAN.VERIFIED_NAME       AS VERIFIED_NAME 
                //              FROM    ((MST_EMPLOYEE           
                //                        LEFT JOIN TRN_DEDUCTEE_DETAILS     
                //                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                //                        LEFT JOIN MST_VERIFIED_PAN       
                //                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                //              WHERE   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                //              @"AND   MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL'
                //              UNION
                //              SELECT  DISTINCT MST_EMPLOYEE.EMPLOYEE_ID AS EMPLOYEE_ID,         
                //                      MST_EMPLOYEE.EMPLOYEE_PAN         AS EMPLOYEE_PAN,          
                //                      MST_EMPLOYEE.EMPLOYEE_NAME        AS EMPLOYEE_NAME,           
                //                      ''                                AS EMPLOYEE_NAME_V,         
                //                      ''                                AS STATUS,         
                //                      " + cmnService.J_SQLDBFormat(strverifiedPAN, J_SQLColFormat.Case_End) + @" AS VERIFIED,
                //                      MST_VERIFIED_PAN.VERIFIED_NAME    AS VERIFIED_NAME 
                //              FROM    ((MST_EMPLOYEE           
                //                        LEFT JOIN TRN_SALARY_DETAILS       
                //                        ON TRN_SALARY_DETAILS.EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                //                        LEFT JOIN MST_VERIFIED_PAN      
                //                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                //              WHERE   TRN_SALARY_DETAILS.BASIC_INFO_ID   = " + lngBasicInfoID + " " +
                //              @"AND   MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL' 
                //              ORDER BY EMPLOYEE_NAME,        EMPLOYEE_PAN";
                //}
                //else
                //{
                //    strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_ID   AS DEDUCTEE_ID," +
                //            "         MST_" + strEmpDed + "." + strEmpDed + "_PAN           AS DEDUCTEE_PAN," +
                //            "         MST_" + strEmpDed + "." + strEmpDed + "_NAME          AS DEDUCTEE_NAME," +
                //            "         ''                                                    AS DEDUCTEE_NAME_V," +
                //            "         ''                                                    AS STATUS," +
                //            "         " + cmnService.J_SQLDBFormat(strverifiedPAN, J_SQLColFormat.Case_End) + " AS VERIFIED," +
                //            "         MST_VERIFIED_PAN.VERIFIED_NAME                        AS VERIFIED_NAME " +
                //            "FROM    ((TRN_DEDUCTEE_DETAILS " +
                //            "         INNER JOIN MST_" + strEmpDed + "  " +
                //            "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                //            "         LEFT JOIN MST_VERIFIED_PAN " +
                //            "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN) " +
                //            "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                //            "AND      MST_" + strEmpDed + "." + strEmpDed + "_PAN <> 'PANNOTAVBL' " +
                //            "ORDER BY " + strEmpDed + "_PAN," + strEmpDed + "_NAME";
                //}
                #endregion
                //
                if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q &&
                    TDSMAN.Classes.TDSMAN.T_pQuarter == T_Qtr.Q4)
                {
                    //-- ANIK @ 2014-04-28
                    strSQL = @"SELECT  DISTINCT MST_EMPLOYEE.EMPLOYEE_ID   AS EMPLOYEE_ID,         
                                      MST_EMPLOYEE.EMPLOYEE_PAN            AS EMPLOYEE_PAN,          
                                      MST_EMPLOYEE.EMPLOYEE_NAME           AS EMPLOYEE_NAME, 
                                      " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V, 
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS, 
                                      MST_VERIFIED_PAN.VERIFIED_STATUS     AS VERIFIED_STATUS,
                                      " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME
                              FROM    ((MST_EMPLOYEE           
                                        LEFT JOIN TRN_DEDUCTEE_DETAILS     
                                        ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                                        LEFT JOIN MST_VERIFIED_PAN       
                                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                              WHERE   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                              @"AND   MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL'";
                    if(strFilter == "0")
                        strSQL = strSQL + "AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + ")";
                    else if (strFilter == "1")
                        strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                    //if (NewEntriesOnly == true)
                    //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";
                    strSQL = strSQL + @" UNION
                              SELECT  DISTINCT MST_EMPLOYEE.EMPLOYEE_ID AS EMPLOYEE_ID,         
                                      MST_EMPLOYEE.EMPLOYEE_PAN         AS EMPLOYEE_PAN,          
                                      MST_EMPLOYEE.EMPLOYEE_NAME        AS EMPLOYEE_NAME,           
                                      " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V, 
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS, 
                                      MST_VERIFIED_PAN.VERIFIED_STATUS  AS VERIFIED_STATUS,
                                      " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME
                              FROM    ((MST_EMPLOYEE           
                                        LEFT JOIN TRN_SALARY_DETAILS       
                                        ON TRN_SALARY_DETAILS.EMPLOYEE_ID      = MST_EMPLOYEE.EMPLOYEE_ID)          
                                        LEFT JOIN MST_VERIFIED_PAN      
                                        ON MST_VERIFIED_PAN.PAN = MST_EMPLOYEE.EMPLOYEE_PAN) 
                              WHERE   TRN_SALARY_DETAILS.BASIC_INFO_ID   = " + lngBasicInfoID + " " +
                              @"AND   MST_EMPLOYEE.EMPLOYEE_PAN <> 'PANNOTAVBL'";
                    if(strFilter == "0")
                        strSQL = strSQL + " AND   MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                    else if (strFilter == "1" || strFilter == "2")
                        strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";
                    //if (NewEntriesOnly == true)
                    //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";
                    strSQL = strSQL + @" ORDER BY VERIFIED_STATUS , EMPLOYEE_NAME,        EMPLOYEE_PAN";
                }
                else
                {
                    strSQL = "SELECT  DISTINCT  MST_" + strEmpDed + "." + strEmpDed + "_ID   AS EMPLOYEE_ID," +
                            "         MST_" + strEmpDed + "." + strEmpDed + "_PAN            AS EMPLOYEE_PAN," +
                            "         MST_" + strEmpDed + "." + strEmpDed + "_NAME           AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                       AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                            "FROM    ((TRN_DEDUCTEE_DETAILS " +
                            "         INNER JOIN MST_" + strEmpDed + "  " +
                            "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                            "         LEFT JOIN MST_VERIFIED_PAN " +
                            "      ON MST_VERIFIED_PAN.PAN = MST_" + strEmpDed + "." + strEmpDed + "_PAN) " +
                            "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                            @"AND     MST_" + strEmpDed + "." + strEmpDed + @"_PAN <> 'PANNOTAVBL' ";
                    if (strFilter == "0" || strFilter == "2")
                        strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                    else if (strFilter == "1")
                        strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";
                    //if (NewEntriesOnly == true)
                    //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";
                    strSQL = strSQL + "ORDER BY VERIFIED_STATUS , " + strEmpDed + "_PAN, " + strEmpDed + "_NAME";
                }
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                TdsMan.PopulateGridView(dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //                
                chkSelectAll.Checked = true;
                //CheckAllGridRows();
                //foreach (DataGridViewRow row in dgvDeductees.Rows)
                //{
                //    //if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                //    //{
                //    //    row.Cells[0].Value = true;
                //    //}
                //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                //    chk.Value = chk.TrueValue;
                //}
                //dgvDeductees.EndEdit();
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    dgvDeductees.Rows[0].SetValues(true);
                }
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGridCorr(long BasicInfoID)//, bool NewEntriesOnly)
        private void LoadDeducteeGridCorr(long BasicInfoID)//, bool NewEntriesOnly)
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                if (rbnAllPANs.Checked == true)
                    strFilter = "";
                else if (rbnReVerifyValidPANs.Checked == true)
                    strFilter = "0";
                else if (rbnOnlyUnVerifiedPANs.Checked == true)
                    strFilter = "1";
                else if (rbnReVerifyInoperativePANs.Checked == true)
                    strFilter = "2";
                //--
                if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                    strEmpDed = "Employee";
                else
                    strEmpDed = "Deductee";
                //--
                intPANId = 1;
                intNameEntered = 2;
                intNameVerified = 3;
                intStatusId = 4;
                intVerifyId = 5;
                intVerifiedName = 6;
                //
                blRegular = false;
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                string[,] strverifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T" } };
                //
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                //-----------------------------------------------------------
                strSQL = "SELECT  DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN AS EMPLOYEE_PAN," +
                        "         COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME         AS EMPLOYEE_NAME,"
                                 + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V," +
                                  cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS,
                                  MST_VERIFIED_PAN.VERIFIED_STATUS              AS VERIFIED_STATUS,
                                      " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME
                        FROM    (COR_TRN_DEDUCTEE_DETAILS LEFT JOIN MST_VERIFIED_PAN 
                              ON MST_VERIFIED_PAN.PAN                     = COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN) 
                        WHERE    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + @"
                        AND      COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN    <> 'PANNOTAVBL' ";
                if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
                    strSQL = strSQL + "AND (COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
                        "OR      COR_TRN_DEDUCTEE_DETAILS.MODE IN ('A','O')) ";
                //if (NewEntriesOnly == true)
                //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";

                if (strFilter == "0" || strFilter == "2")
                    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                else if (strFilter == "1")
                    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                strSQL = strSQL + "UNION " +
                        "SELECT  DISTINCT COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN AS EMPLOYEE_PAN," +
                        "         COR_TRN_SALARY_DETAILS.EMPLOYEE_NAME        AS EMPLOYEE_NAME," +
                                  cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V," +
                                  cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS,
                                  MST_VERIFIED_PAN.VERIFIED_STATUS              AS VERIFIED_STATUS," +
                                      cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME
                        FROM   (COR_TRN_SALARY_DETAILS 
                                LEFT JOIN MST_VERIFIED_PAN 
                                       ON MST_VERIFIED_PAN.PAN         = COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN) 
                         WHERE   COR_TRN_SALARY_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + " " +
                        "AND     COR_TRN_SALARY_DETAILS.EMPLOYEE_PAN    <> 'PANNOTAVBL' ";
                if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
                    strSQL = strSQL + "AND (COR_TRN_SALARY_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
                        "OR      COR_TRN_SALARY_DETAILS.MODE IN ('A','O')) ";
                //if (NewEntriesOnly == true)
                //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";

                if (strFilter == "0" || strFilter == "2")
                    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                else if (strFilter == "1")
                    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";


                strSQL = strSQL + "ORDER BY VERIFIED_STATUS, EMPLOYEE_NAME," +
                        "         EMPLOYEE_PAN ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                TdsMan.PopulateGridView(dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //--
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadNewPANsDeducteeGridExcelImport()
        private void LoadNewPANsDeducteeGridExcelImport()
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                //if (rbnAllPANs.Checked == true)
                //    strFilter = "";
                //else if (rbnReVerifyValidPANs.Checked == true)
                //    strFilter = "0";
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    strFilter = "1";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //string[,] strverifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T" } };
                //{TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC = '' ", "F", "", "T"}};
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                // -- " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_MASTER_ID
                strSQL = "SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN                AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME               AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                       AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN <> 'PANNOTAVBL' ";
                //if (strFilter == "0")
                //    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                //else if (strFilter == "1")
                //    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                strSQL = strSQL + " ORDER BY VERIFIED_STATUS ," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
                chkSelectAll.Checked = true;
                //
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    dgvDeductees.Rows[0].SetValues(true);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region LoadAllPANsDeducteeGridExcelImport()
        private void LoadAllPANsDeducteeGridExcelImport()
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                //if (rbnAllPANs.Checked == true)
                //    strFilter = "";
                //else if (rbnReVerifyValidPANs.Checked == true)
                //    strFilter = "0";
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    strFilter = "1";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //string[,] strverifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T" } };
                //{TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC = '' ", "F", "", "T"}};
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                // -- " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_MASTER_ID
                strSQL = "SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN                AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_NAME               AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                       AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN <> 'PANNOTAVBL' ";

                strSQL = strSQL + " UNION SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN                AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME               AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                       AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN <> 'PANNOTAVBL' ";
                //if (strFilter == "0")
                //    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                //else if (strFilter == "1")
                //    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                //strSQL = strSQL + " ORDER BY VERIFIED_STATUS ," + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_NAME";
                strSQL = strSQL + " ORDER BY VERIFIED_STATUS ,EMPLOYEE_PAN, EMPLOYEE_NAME";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
                chkSelectAll.Checked = true;
                //
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    dgvDeductees.Rows[0].SetValues(true);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region LoadAllPANsDeducteeGridExcelImportSD()
        private void LoadAllPANsDeducteeGridExcelImportSD()
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                //if (rbnAllPANs.Checked == true)
                //    strFilter = "";
                //else if (rbnReVerifyValidPANs.Checked == true)
                //    strFilter = "0";
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    strFilter = "1";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                //string[,] strverifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T" } };
                //{TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC = '' ", "F", "", "T"}};
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                // -- " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_MASTER_ID
                strSQL = "SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN             AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_NAME            AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @"    AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                                            AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".EMPLOYEE_PAN <> 'PANNOTAVBL' ";

                strSQL = strSQL + " UNION SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN               AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME              AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @"    AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                                            AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN <> 'PANNOTAVBL' ";
                //if (strFilter == "0")
                //    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                //else if (strFilter == "1")
                //    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                //strSQL = strSQL + " ORDER BY VERIFIED_STATUS ," + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_SALARY_DETAILS + ".DEDUCTEE_NAME";
                strSQL = strSQL + " ORDER BY VERIFIED_STATUS ,EMPLOYEE_PAN, EMPLOYEE_NAME";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
                chkSelectAll.Checked = true;
                //
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    dgvDeductees.Rows[0].SetValues(true);
                }
            }
            catch
            {

            }
        }
        #endregion


        #region LoadNewPANsDeducteeGridExcelImportSD()
        private void LoadNewPANsDeducteeGridExcelImportSD()
        {
            DataSet dsetGridClone = new DataSet();
            string strFilter = "";
            try
            {
                //if (rbnAllPANs.Checked == true)
                //    strFilter = "";
                //else if (rbnReVerifyValidPANs.Checked == true)
                //    strFilter = "0";
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    strFilter = "1";
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN No.", "85", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "100", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "100", "S", "", "", "", "T"},
                                        {"Status", "80", "", "", "", "", ""},
                                        {"Verified", "0", "", "", "", "F", ""},
                                        {"Verified Name", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                string[,] strVerifiedName = { { "MST_VERIFIED_PAN.VERIFIED_NAME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.VERIFIED_NAME IS NOT NULL", "F", "MST_VERIFIED_PAN.VERIFIED_NAME", "F"} };
                //
                string[,] strStatusPAN = { { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NULL", "F", "", "T"},
                                             { "MST_VERIFIED_PAN.CHECKED_DATE_TIME IS NOT NULL", "F", cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.CHECKED_DATE_TIME", J_SQLColFormat.DateFormatDDMMYYYY), "F"} };
                //
                // -- " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_MASTER_ID
                strSQL = "SELECT  DISTINCT 'TRUE' AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN                AS EMPLOYEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME               AS EMPLOYEE_NAME," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS EMPLOYEE_NAME_V,
                                      " + cmnService.J_SQLDBFormat(strStatusPAN, J_SQLColFormat.Case_End) + @" AS STATUS," +
                            "         MST_VERIFIED_PAN.VERIFIED_STATUS                       AS VERIFIED_STATUS," +
                            "         " + cmnService.J_SQLDBFormat(strVerifiedName, J_SQLColFormat.Case_End) + @" AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN <> 'PANNOTAVBL' ";
                //if (strFilter == "0")
                //    strSQL = strSQL + " AND MST_VERIFIED_PAN.VERIFIED_STATUS IN (" + strFilter + @")";
                //else if (strFilter == "1")
                //    strSQL = strSQL + " AND VERIFIED_STATUS IS NULL ";

                strSQL = strSQL + " ORDER BY VERIFIED_STATUS ," + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
                chkSelectAll.Checked = true;
                //
                foreach (DataGridViewRow row in dgvDeductees.Rows)
                {
                    dgvDeductees.Rows[0].SetValues(true);
                }
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
            txtTANNo.Text = "";
            txtUserID.Text = "";
            txtPassword.Text = "";
            lblInvalidNo.Text = "0";
            btnPrintInvalidPAN.Enabled = false;
            lblUnmatchedNo.Text = "0";
            btnPrintUnmatched.Enabled = false;
            lblVerifiedOperativeNo.Text = "0";
            lblVerifiedInOperativeNo.Text = "0";
            btnPrintValidOperativePAN.Enabled = false;
            lblNotVerifiedNo.Text = "0";
            btnVerification.Enabled = true;
            btnVerification.BackColor = Color.Lavender;
            grpButtons.Enabled = true;
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

        #region LoadBatch
        private void LoadBatch()
        {
            string[,] strMatrixBatchGrid = null; string strOrderBy = ""; string strQuery = ""; DataSet dsetGridClone = new DataSet();

            //-----------------------------------------------------------
            string[,] strMatrixBatch = {{"BATCH_HEADER_ID", "0", "", "Right", "", "F", ""},
                                        {"Tax Year", "70", "S", "", "", "", ""},
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
                      "        " + cmnService.J_SQLDBFormat(strLoadCorrectionMatrix, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS TOTAL_CORRECTION " +
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

        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    grpEnterLoginDetails.Enabled = true;
                    grpCaptcha.Enabled = true;
                    //btnLogging.Text = strLogOn;
                    LoadDeducteeGrid();
                    //
                    InitializeCaptcha();
                    break;
                case enmRequestType.PanValidation:

                    //grpDownloadList.Visible = true;
                    //grpEnterLoginDetails.Enabled = false;
                    //grpCaptcha.Enabled = false;
                    //--
                    grpLoginDetails.Visible = false;
                    pnlTracesMessage.Visible = true;
                    //
                    grpStatus.Enabled = true;
                    grpButtons.Enabled = true;
                    dgvDeductees.Enabled = true;
                    //
                    btnVerification.Text = "Stop validating";
                    btnVerification.ForeColor = Color.OrangeRed;
                    btnPrintInvalidPAN.Enabled = false;
                    btnPrintUnmatched.Enabled = false;
                    btnPrintValidOperativePAN.Enabled = false;
                    //
                    //bgwPANVerification.RunWorkerAsync();
                    //
                    //btnLogging.Text = strLogOff;
                    ////--
                    //LoadDeducteeGrid();
                    ////--
                    //if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                    //{
                    //    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBatchId;
                    //    //--
                    //    if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                    //        strEmpDed = "Employee";
                    //    else
                    //        strEmpDed = "Deductee";
                    //    //--
                    //    intPANId = 0;
                    //    intNameEntered = 1;
                    //    intNameVerified = 2;
                    //    intStatusId = 3;
                    //    intVerifyId = 4;
                    //    //
                    //    blRegular = false;
                    //}
                    //else
                    //{
                    //    lngBasicInfoID = TDSMAN.Classes.TDSMAN.T_pBasicInfoId;
                    //    //
                    //    if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == T_FormNo.F24Q)
                    //        strEmpDed = "Employee";
                    //    else
                    //        strEmpDed = "Deductee";
                    //    //--
                    //    intPANId = 1; 
                    //    intNameEntered = 2;
                    //    intNameVerified = 3;
                    //    intStatusId = 4;
                    //    intVerifyId = 5;
                    //    //
                    //    blRegular = true;
                    //}
                    ////--
                    //if (lngBasicInfoID == 0)
                    //{
                    //    //if (dgvDeductees.Rows.Count > 0)
                    //    //    dgvDeductees.Rows.Clear();
                    //    return;
                    //}
                    ////--
                    //if (TDSMAN.Classes.TDSMAN.T_PANVerificationModule == T_OTHERMODULENAME.MAKE_CORRECTION)
                    //    LoadDeducteeGridCorr(lngBasicInfoID);
                    //else
                    //    LoadDeducteeGrid(lngBasicInfoID);
                    ////
                    //this.Cursor = Cursors.Default;
                    ////
                    //grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                    ////
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

        #region LoadFormTools
        private void LoadFormTools()
        {
            //this.Width = 739;
            //this.Height = 666;
            this.Width = 745;
            this.Height = 680;
            //
            lblTitle.Width = 375;
            pnlTitle.Width = 375;
            lblSearchMode.Location = new Point(553, 8);
            pnlHeader.Width = 721;
            //
            grpReturnSelection.Location = new Point(182, 38);
            grpCorrectionReturn.Location = new Point(12, 73);
            grpRegularReturn.Location = new Point(12, 73);
            pnlLine.Location = new Point(12, 196);
            //
            grpLoginDetails.Location = new Point(11, 255);
            dgvDeductees.Location = new Point(12, 196);
            pnlBottomLine.Location = new Point(12, 595);
            //grpButtons.Location = new Point(513, 597);
            grpButtons.Location = new Point(530, 597);
            grpStatus.Location = new Point(11, 597);
            //
            //grpExport.Location = new Point(1, 308);
            //
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //
        }
        #endregion

        //-- Added By Abhishek Dey On 22/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnBulkPANNameValidationImprovedVersion");
            Tan.ShowDialog();
            //--------------
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=9C84W1Lfp6A");

        }
        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0048", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }
        #endregion

        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }

        #endregion

        //-----------------------------------------

        #endregion

        #region btnUpdateMasterData_Click
        private void btnUpdateMasterData_Click(object sender, EventArgs e)
        {
            grpUpdateMaster.Visible = true;
            grpUpdateMaster.Height = 520;
            //
            chkNewSelectDeselect.Checked = false;
            //-- LOAD DATA ON GRID
            string[,] strMatrixBatchGrid = {{"PARTY_ID", "0", "", "", "", "F", ""},
                                        {"PAN", "85", "S", "", "", "", "T"},
                                        {"Name (already there)", "280", "S", "", "", "", "T"},
                                        {"Name (extracted)", "280", "S", "", "", "", "T"}};
            //--
            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + lngBasicInfoID)) == T_FormNo.F24Q)
            {
                grpUpdateMaster.Text = "Employee Data";
                strSQL = "SELECT         MST_EMPLOYEE.EMPLOYEE_ID                                          AS PARTY_ID," +
                        "                MST_EMPLOYEE.EMPLOYEE_PAN                                         AS PAN," +
                        "                MST_EMPLOYEE.EMPLOYEE_NAME                                        AS NAME_DESC," +
                        "                " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME AS VERIFIED_NAME " +
                        "         FROM   TRN_DEDUCTEE_DETAILS," +
                        "                TRN_CHALLAN," +
                        "                " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "," +
                        "                MST_EMPLOYEE " +
                        "         WHERE  MST_EMPLOYEE.EMPLOYEE_PAN          = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".PAN " +
                        "         AND    TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_EMPLOYEE.EMPLOYEE_ID " +
                        "         AND    TRN_CHALLAN.CHALLAN_ID             = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        "         AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME    <> '' " +
                        "         AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                        "         UNION " +
                        "         SELECT MST_EMPLOYEE.EMPLOYEE_ID       AS PARTY_ID," +
                        "                MST_EMPLOYEE.EMPLOYEE_PAN      AS PAN," +
                        "                MST_EMPLOYEE.EMPLOYEE_NAME     AS NAME_DESC," +
                        "                " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME AS VERIFIED_NAME " +
                        "         FROM   TRN_SALARY_DETAILS," +
                        "                " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "," +
                        "                MST_EMPLOYEE " +
                        "         WHERE  MST_EMPLOYEE.EMPLOYEE_PAN        = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".PAN " +
                        "         AND    TRN_SALARY_DETAILS.EMPLOYEE_ID   = MST_EMPLOYEE.EMPLOYEE_ID " +
                        "         AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME    <> '' " +
                        "         AND    TRN_SALARY_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                        "         ORDER BY PAN";
            }
            else
            {
                grpUpdateMaster.Text = "Deductee Data";
                strSQL = "SELECT         MST_DEDUCTEE.DEDUCTEE_ID       AS PARTY_ID," +
                        "                MST_DEDUCTEE.DEDUCTEE_PAN      AS PAN," +
                    "                    MST_DEDUCTEE.DEDUCTEE_NAME     AS NAME_DESC," +
                        "                " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME AS VERIFIED_NAME " +
                    "             FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "," +
                    "                    MST_DEDUCTEE " +
                    "             WHERE  MST_DEDUCTEE.DEDUCTEE_PAN          = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".PAN " +
                    "             AND    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME     <> '' " +
                    "             ORDER BY MST_DEDUCTEE.DEDUCTEE_PAN";
            }

            TdsMan.PopulateGridView(grdvMasterData, dmlService.J_pCommand, strSQL, strMatrixBatchGrid);
        }
        #endregion

        #region btnCloseUpadateMaster_Click
        private void btnCloseUpadateMaster_Click(object sender, EventArgs e)
        {
            grpUpdateMaster.Visible = false;
        }
        #endregion

        #region chkNewSelectDeselect_CheckedChanged
        private void chkNewSelectDeselect_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                //if (grdvMasterData.Visible == false) { chkNewSelectDeselect.Checked = false; return; }
                //if (blnSelectDeselect == false) return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //blnDeleteTempGridRecord = true;
                foreach (DataGridViewRow row in grdvMasterData.Rows)
                {
                    if (chkNewSelectDeselect.Checked == true)//checked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                    else//Unchecked all checkbox
                    {
                        if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        {
                            //blnExitGrid = false;
                            row.Cells[0].Value = false;
                            //blnExitGrid = false;
                        }
                        //strSelectedLabel = "";
                    }
                }
                //blnDeleteTempGridRecord = false;

                //
                this.Cursor = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region btnStartUpdation_Click
        private void btnStartUpdation_Click(object sender, EventArgs e)
        {
            if (grdvMasterData.RowCount <= 0)
            {
                cmnService.J_UserMessage("No Data to Update !!!");
                return;
            }
            //--
            string strPartyIds = "", strPartyPAN = ""; long lngPartyCount = 0;
            //
            foreach (DataGridViewRow row in grdvMasterData.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    if (row.Cells[1].Value.ToString() != "")
                    {
                        lngPartyCount = lngPartyCount + 1;
                        strPartyPAN = row.Cells[2].Value.ToString();
                        strPartyIds = strPartyIds + row.Cells[1].Value.ToString() + ",";
                    }
                }
            }
            //
            if (Convert.ToString(strPartyIds) == "")
            {
                cmnService.J_UserMessage("No record selected for Updating !!!");
                return;
            }
            strPartyIds = cmnService.J_Mid(strPartyIds, 0, strPartyIds.Length - 1);
            //
            if (strPartyIds.Contains(",") == true)
            {
                if (cmnService.J_UserMessage("Update Name of " + lngPartyCount.ToString() + " PANs ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            else
            {
                if (cmnService.J_UserMessage("Update Name of PAN : " + strPartyPAN + " ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            //--
            this.Cursor = Cursors.WaitCursor;
            foreach (DataGridViewRow row in grdvMasterData.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    if (row.Cells[1].Value.ToString() != "")
                    {
                        //strBatchIds = strBatchIds + row.Cells[1].Value.ToString() + ",";
                        if (grpUpdateMaster.Text == "Employee Data")
                            strSQL = "UPDATE MST_EMPLOYEE SET NAME_UPDT = EMPLOYEE_NAME, NAME_UPDT_DATE = " + cmnService.J_DateOperator() + System.DateTime.Now + cmnService.J_DateOperator() + ", EMPLOYEE_NAME ='" + row.Cells[4].Value.ToString() + "' WHERE EMPLOYEE_ID = " + cmnService.J_ReturnInt64Value(row.Cells[1].Value.ToString()) + " ";
                        else
                            strSQL = "UPDATE MST_DEDUCTEE SET NAME_UPDT = DEDUCTEE_NAME, NAME_UPDT_DATE = " + cmnService.J_DateOperator() + System.DateTime.Now + cmnService.J_DateOperator() + ", DEDUCTEE_NAME ='" + row.Cells[4].Value.ToString() + "' WHERE DEDUCTEE_ID = " + cmnService.J_ReturnInt64Value(row.Cells[1].Value.ToString()) + " ";
                        //--
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
            }
            cmnService.J_UserMessage("Updation Completed...");
            grpUpdateMaster.Visible = false;
            this.Cursor = Cursors.Default;
            //--
        }

        #endregion

        #region rbnAllPANs_CheckedChanged
        private void rbnAllPANs_CheckedChanged(object sender, EventArgs e)
        {
            //TDSMAN.Classes.TDSMAN.T_FromModule = "";
            if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
            {
                //LoadDeducteeGridExcelImport();
                LoadNewPANsDeducteeGridExcelImport();
            }
            else if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS) //-- 2024/05/22
            {
                LoadAllPANsDeducteeGridExcelImportSD();
            }
            else if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS)
            {
                //LoadDeducteeGridExcelImport();
                LoadAllPANsDeducteeGridExcelImport();
            }
            else  if (rbnRegularReturn.Checked == true)
            {
                //if (rbnAllPANs.Checked == true)
                //    LoadDeducteeGrid(lngBasicInfoID);
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    LoadDeducteeGrid(lngBasicInfoID);
                //else if (rbnReVerifyValidPANs.Checked == true)
                ComboBoxItem selectedItem = (ComboBoxItem)cmbFormNo.SelectedItem;
                string strFormNo = selectedItem.Value.ToString();
                //
                LoadDeducteeGrid(lngBasicInfoID, strFormNo);
            }
            else if (rbnCorrectionReturn.Checked == true)
            {
                //if (rbnAllPANs.Checked == true)
                //    LoadDeducteeGridCorr(lngBasicInfoID);
                //else if (rbnOnlyUnVerifiedPANs.Checked == true)
                //    LoadDeducteeGridCorr(lngBasicInfoID);
                //else if (rbnReVerifyValidPANs.Checked == true)
                    LoadDeducteeGridCorr(lngBasicInfoID);
                //TDSMAN.Classes.TDSMAN.T_FromModule = T_OTHERMODULENAME.MAKE_CORRECTION;
            }
            //else
            //{
            //    LoadDeducteeGridExcelImport();
            //}

                //
                chkSelectAll.Checked = true;
            chkSelectAll_CheckedChanged(sender, e);
        }
        #endregion


        #region chkSelectAll_CheckedChanged
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //--
            CheckAllGridRows();
        }

        #endregion

        #region dgvDeductees_CurrentCellDirtyStateChanged

        private void dgvDeductees_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDeductees.IsCurrentCellDirty)
            {
                dgvDeductees.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        #endregion

        #region dgvDeductees_CellValueChanged
        private void dgvDeductees_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR &&
                TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS &&
                TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS &&
                TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvDeductees.Rows[e.RowIndex].Cells[0];
                    //DataGridViewCell cellChildMenuID = (DataGridViewCell)dgvDeductees.Rows[e.RowIndex].Cells[1];
                    //if (cell.Value == null || (bool)cell.Value == false)
                    if ((bool)cell.Value == false)
                    {
                        //grdvNewDescription.MultiSelect = false;
                        //grdvNewDescription.Rows[e.RowIndex].Selected = true;
                        //cell.Value = true;
                        //cmnService.J_UserMessage("UNCHECK"); 
                        //strSQL = "DELETE FROM TRN_USER_MENU_ACCESS WHERE SETUP_ID =" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + " AND CHILD_MENU_ID = " + Convert.ToInt32(cellChildMenuID.Value.ToString());
                        //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                        lngSelectedGrid = lngSelectedGrid - 1;
                    }
                    else if ((bool)cell.Value == true)
                    {
                        //grdvNewDescription.Rows[e.RowIndex].Selected = false;
                        //cell.Value = false;
                        //cmnService.J_UserMessage("CHECK");
                        //strSQL = "INSERT INTO TRN_USER_MENU_ACCESS (SETUP_ID, CHILD_MENU_ID) VALUES (" + Convert.ToInt32(Support.GetItemData(cmbSelectUser, cmbSelectUser.SelectedIndex)) + "," + Convert.ToInt32(cellChildMenuID.Value.ToString()) + ")";
                        //dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
                        lngSelectedGrid = lngSelectedGrid + 1;
                    }
                }
            }
        }

        #endregion

        #region dgvDeductees_CellContentClick
        private void dgvDeductees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                if (TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS &&
                    TDSMAN.Classes.TDSMAN.T_FromModule != T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                {
                    if (e.RowIndex != -1)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvDeductees.Rows[e.RowIndex].Cells[0];
                        //if (cell.Value == null) return;
                        if (cell.Value == null || (bool)cell.Value == true)
                        {
                            cell.Value = false;
                        }
                        else if ((bool)cell.Value == false)
                        {
                            cell.Value = true;
                        }

                    }
                }
            }
            catch (Exception ERR)
            {

            }
        }

        #endregion

        #region dgvDeductees_CellFormatting
        private void dgvDeductees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "STATUS")
            {
                if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == T_PANVerificationStatusId.VALID_OPERATIVE.ToString())// "0")
                {
                    //highlighting the cell
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                    dgvDeductees.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = T_PANVerificationStatus.VALID_OPERATIVE.ToString();// "Valid PAN";
                }
                else if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == T_PANVerificationStatusId.VALID_INOPERATIVE.ToString())// "0")
                {
                    //highlighting the cell
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                    dgvDeductees.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();// "Valid PAN";
                }
                else if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == T_PANVerificationStatusId.INVALID.ToString())//
                {
                    e.CellStyle.BackColor = Color.OrangeRed;
                    e.CellStyle.ForeColor = Color.Black;
                    dgvDeductees.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = T_PANVerificationStatus.INVALID.ToString();//"Invalid PAN";
                }
                else if (dgvDeductees.Rows[e.RowIndex].Cells["VERIFIED_STATUS"].Value.ToString() == "")
                {
                    e.CellStyle.BackColor = Color.White;
                }
            }
            //--
            if (dgvDeductees.Columns[e.ColumnIndex].DataPropertyName == "EMPLOYEE_NAME_V")
            {
                if (dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME_V"].Value.ToString() != "")
                {
                    if (dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME"].Value.ToString().Trim() != dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME_V"].Value.ToString().Trim())
                    {
                        if (GenMaskedName(dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME"].Value.ToString().Trim()) != dgvDeductees.Rows[e.RowIndex].Cells["EMPLOYEE_NAME_V"].Value.ToString().Trim())
                            e.CellStyle.BackColor = Color.Tan;
                    }
                }
            }
        }
        #endregion

        #region CheckAllGridRows
        private void CheckAllGridRows()
        {
            this.Cursor = Cursors.WaitCursor;
            //blnDeleteTempGridRecord = true;
            foreach (DataGridViewRow row in dgvDeductees.Rows)
            {
                if (chkSelectAll.Checked == true)//checked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                    {
                        row.Cells[0].Value = true;
                    }
                }
                else
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                    {
                        row.Cells[0].Value = false;
                    }
                }
            }
            //--
            this.Cursor = Cursors.Default;
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
                XmlDocument XMLDoc = new XmlDocument();
                XMLDoc.Load(strPath + "/" + TDSMAN.Classes.TDSMAN.T_XmlConnectionFileNameServer);
                //
                XmlNode Root = XMLDoc.SelectSingleNode(T_XML.MULTIUSERCONNECTION + "/" + T_XML.SERVERNAME);
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

        #region btnPrintValidPAN_Click
        private void btnPrintValidPAN_Click(object sender, EventArgs e)
        {
            //btnPrintValidPAN_MouseClick(sender, (MouseEventArgs)e);
        }
        #endregion

        #region btnPrintValidPAN_MouseClick
        private void btnPrintValidPAN_MouseClick(object sender, MouseEventArgs e)
        {
            if (lblVerifiedOperativeNo.Text == "" || cmnService.J_ReturnInt32Value(lblVerifiedOperativeNo.Text) == 0 ) { cmnService.J_UserMessage("No Valid & Operative PAN to print!!"); return; }
            //--
            if (e.Button == MouseButtons.Left)
                cntxtMnuGrpValidOperativePAN.Show(btnPrintValidOperativePAN, new Point(e.X, e.Y));
        }
        #endregion


        #region mnuCntxtMenuPrintValidPAN_Click
        private void mnuCntxtMenuPrintValidPAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVerifiedOperativeNo.Text == "" || cmnService.J_ReturnInt32Value(lblVerifiedOperativeNo.Text) == 0) { cmnService.J_UserMessage("No Valid PAN to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print of the Valid PAN(s) ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                //
                if (grpReturnSelection.Visible == false)
                {
                    if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                        rptDialog.PrintValidOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, "SD", "");
                    else
                        rptDialog.PrintValidOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, "");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintValidOperativePans_RDLC(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintValidOperativePans_RDLC(lngBasicInfoID, "TRUE", "");
                }
                //
            }
            catch
            {
            }
        }
        #endregion

        #region btnPrintValidInOperativePAN_MouseClick
        private void btnPrintValidInOperativePAN_MouseClick(object sender, MouseEventArgs e)
        {
            if (lblVerifiedInOperativeNo.Text == "" || cmnService.J_ReturnInt32Value(lblVerifiedInOperativeNo.Text) == 0) { cmnService.J_UserMessage("No Valid PAN & Inoperative to print!!"); return; }
            //--
            if (e.Button == MouseButtons.Left)
            {
                cntxtMnuGrpValidInOperativePAN.Show(btnPrintValidInOperativePAN, new Point(e.X, e.Y));
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                   TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS)
                {
                    if (TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath != "")
                    {
                        tlStrp2.Visible = true;
                        mnuMarkExcelInOperative.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region mnuCntxtMenuPrintValidInOperativePAN_Click
        private void mnuCntxtMenuPrintValidInOperativePAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVerifiedInOperativeNo.Text == "" || cmnService.J_ReturnInt32Value(lblVerifiedInOperativeNo.Text) == 0) { cmnService.J_UserMessage("No Valid & Inoperative PAN to print!!"); return; }
                //
                if (cmnService.J_UserMessage("Do you want to take print ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //
                TDSMAN.Classes.TDSMAN.T_pCurrentForm = this;
                //
                RptDialog rptDialog = new RptDialog();
                //
                if (grpReturnSelection.Visible == false)
                {
                    if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                        rptDialog.PrintValidInOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, "SD", "");
                    else
                        rptDialog.PrintValidInOperativePans_RDLC(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, "");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintValidInOperativePans_RDLC(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintValidInOperativePans_RDLC(lngBasicInfoID, "TRUE", "");
                }
                //
            }
            catch
            {
            }
        }
        #endregion

        #region mnuCntxtMenuExportValidInOperativePAN_Click
        private void mnuCntxtMenuExportValidInOperativePAN_Click(object sender, EventArgs e)
        {
            grpExport.Visible = true;
            if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
                grpExport.Location = new Point(0, grpExport.Location.Y);
            txtDestinationFileName.Text = "ValidPANInoperative.csv";
            txtDestinationFileNameHidden.Text = "ValidPANInoperative.csv";
        }
        #endregion

        private void cntxtMnuGrpNameDifference_Opening(object sender, CancelEventArgs e)
        {

        }


        #region mnuMarkExcel_Click
        private void mnuMarkExcel_Click(object sender, EventArgs e)
        {
            //strQueryString = "SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_NAME AS NAME_DESC," +
            //            "                    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN           AS PAN," +
            //            "                    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO       AS SL_NO," +
            //            "                    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".TRF_VCH_CHLN_NO         AS CHALLAN_NO " +
            //            "             FROM  ((" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + " LEFT JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + " " +
            //            "                    ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".CHALLAN_SERIAL_NO) " +
            //            "                    LEFT JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + " " +
            //            "                    ON  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN     = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".PAN) " +
            //            "             WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + ".VERIFIED_NAME         = '' " +
            //            //"             AND    COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + BasicInfoBatchID + " " +
            //            "             ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_NAME, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".RUNNING_SERIAL_NO";
            //strSQL = "SELECT DEDUCTEE_NAME," +
            //            "            DEDUCTEE_PAN," +
            //            "            EMPLOYEE_SERIAL_NO " +
            //            "     FROM   " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " ";
            ////--
            IDataReader drdGetSheetRecord = null;
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //MessageBox.Show("1");
                //--
                string filename = @"" + TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath.ToString();
                //MessageBox.Show("2 " + filename);
                object m = Type.Missing;
                //MessageBox.Show("3");
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
                //MessageBox.Show("4");

                if (excelapp == null) return;
                //MessageBox.Show("5");

                excelapp.DisplayAlerts = false;
                //MessageBox.Show("6");

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;
                //MessageBox.Show("7");

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                //MessageBox.Show("8");
                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                //MessageBox.Show("9");
                //
                strSQL = @"SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_NAME     AS NAME_DESC,
                                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN      AS PAN,
                                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN_CELL AS PAN_CELL 
                           FROM  (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @" LEFT JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + @" 
                               ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + @".PAN) 
                           WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + @".VERIFIED_NAME     = '' 
                           ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN ";

                //MessageBox.Show("10");
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //MessageBox.Show("11");
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    //drdGetSheetRecord.Close();
                    //drdGetSheetRecord.Dispose();
                    return;
                }
                int i = 0;
                while (drdGetSheetRecord.Read())
                {
                    //MessageBox.Show("12");

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[T_Sheet_Name.DEDUCTEE_DETAILS.ToString()];
                    //
                    //MessageBox.Show("13");
                    wsnew.get_Range(drdGetSheetRecord["PAN_CELL"].ToString(), m).Interior.Color = Color.OrangeRed; //System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName(drdGetSheetRecord["ERR_COLOR"].ToString()));
                                                                                                                   //
                    //MessageBox.Show("14");
                    wb.Save();
                    //

                    //MessageBox.Show("15");
                    i = i + 1;
                }

                //MessageBox.Show("16");
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@
                //MessageBox.Show("17");
                wb.Save();
                wb.Close(m, m, m);
                //MessageBox.Show("18");
                //
                wb = null;
                wbs = null;
                //
                //MessageBox.Show("19");
                excelapp.Quit();
                excelapp = null;
                //MessageBox.Show("20");
                //--
                if (i>0)
                {
                    //MessageBox.Show("21");
                    cmnService.J_UserMessage("The Invalid PANs in the Excel sheet has been marked.\nTotal : " + i + " Nos.");
                    System.Diagnostics.Process.Start(TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath.ToString());
                }
                //MessageBox.Show("22");
                //--
            }
            catch (Exception ERR)
            {

            }
        }
        #endregion

        #region mnuMarkExcelInOperative_Click
        private void mnuMarkExcelInOperative_Click(object sender, EventArgs e)
        {
            IDataReader drdGetSheetRecord = null;
            try
            {
                //if (KILL_EXCEL() == false)
                //    return false;
                //--
                string filename = @"" + TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath.ToString();
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                //
                strSQL = @"SELECT DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_NAME     AS NAME_DESC,
                                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN      AS PAN,
                                           " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN_CELL AS PAN_CELL 
                           FROM  (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @" LEFT JOIN " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + @" 
                               ON " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + @".PAN) 
                           WHERE  " + TDSMAN.Classes.TDSMAN.T_tblTEMP_VALID_INOPERATIVE_PAN + @".VERIFIED_NAME <> '' 
                           ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_DEDUCTEE_DETAILS + @".DEDUCTEE_PAN ";

                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    //drdGetSheetRecord.Close();
                    //drdGetSheetRecord.Dispose();
                    return;
                }
                int i = 0;
                while (drdGetSheetRecord.Read())
                {

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[T_Sheet_Name.DEDUCTEE_DETAILS.ToString()];
                    //
                    wsnew.get_Range(drdGetSheetRecord["PAN_CELL"].ToString(), m).Interior.Color = Color.Yellow; //System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName(drdGetSheetRecord["ERR_COLOR"].ToString()));
                    //
                    wb.Save();
                    //
                    i = i + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@
                wb.Save();
                wb.Close(m, m, m);
                //
                wb = null;
                wbs = null;
                //
                excelapp.Quit();
                excelapp = null;
                //--
                if (i > 0)
                {
                    cmnService.J_UserMessage("The Inoperative PANs in the Excel sheet has been marked.\nTotal : " + i + " Nos.");
                    System.Diagnostics.Process.Start(TDSMAN.Classes.TDSMAN.T_ExcelImportFilePath.ToString());
                }
                //--
            }
            catch (Exception ERR)
            {

            }
        }

        #endregion

        #region mnuCntxtMenuExportValidPAN_Click
        private void mnuCntxtMenuExportValidPAN_Click(object sender, EventArgs e)
        {
            grpExport.Visible = true;
            if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR ||
                TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS)
            {
                grpExport.Location = new Point(0, grpExport.Location.Y);
                //grpExport.Visible = true;
                //grpExport.BringToFront();
                //grpExport.Anchor = AnchorStyles.None;
                //grpExport.Location = new Point(
                //    (this.Width - grpExport.Width) / 2,
                //    (this.Height - grpExport.Height) / 2
                //);
                ////grpExport.Left = 10;   // small margin instead of 0
                ////grpExport.Top = grpExport.Top;
                ////grpExport.BringToFront();
            }
                //
            txtDestinationFileName.Text = "ValidPANOperative.csv";
            txtDestinationFileNameHidden.Text = "ValidPANOperative.csv";
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

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

        #region ExportSelectedGridToCSV
        private void ExportSelectedGridToCSV(DataGridView dgv)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int intPANId = 2;
                //filePathCSV = Path.Combine(
                //    Application.StartupPath,
                //    TDSMAN.Classes.TDSMAN.T_GetTANLoadTRACES + "_" +
                //    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                //
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_ALL_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_SD_NEW_PANS ||
                    TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
                {
                    filePathCSV = Path.Combine(
                    Application.StartupPath,
                    TDSMAN.Classes.TDSMAN.T_pTAN + "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                    intPANId = 1;
                }
                else if (rbnRegularReturn.Checked == true)
                {
                    filePathCSV = Path.Combine(
                    Application.StartupPath,
                    System.Text.RegularExpressions.Regex.Match(cmbCompany.Text, @"\[(.*?)\]").Groups[1].Value + "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                    intPANId = 2;
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    filePathCSV = Path.Combine(
                    Application.StartupPath,
                    System.Text.RegularExpressions.Regex.Match(cmbCompany.Text, @"\[(.*?)\]").Groups[1].Value + "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                    intPANId = 1;
                }
                //System.Text.RegularExpressions.Regex.Match(input, @"\[(.*?)\]").Groups[1].Value;
                // HEADER
                sb.AppendLine("PAN");

                HashSet<string> addedPAN = new HashSet<string>(); // avoid duplicates

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        //CHECKBOX COLUMN (assumed index 0)
                        //bool isSelected = Convert.ToBoolean(row.Cells[0].Value);
                        bool isSelected = true; //row.Cells[0].Value != null &&
                                                //Convert.ToBoolean(row.Cells[0].Value);
                        //if (rbnCorrectionReturn.Checked == false)
                        //{
                            isSelected = row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value);
                        //}

                        if (!isSelected)
                            continue;

                        string pan = row.Cells[intPANId].Value?.ToString().Trim().ToUpper();

                        if (!string.IsNullOrEmpty(pan))
                        {
                            if (!addedPAN.Contains(pan))
                            {
                                addedPAN.Add(pan);

                                pan = EscapeCSV(pan);
                                sb.AppendLine(pan);
                            }
                        }
                    }
                }

                // WRITE FILE
                File.WriteAllText(filePathCSV, sb.ToString(), Encoding.UTF8);

                //cmnService.J_UserMessage("CSV created successfully.");
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage("CSV Export failed: " + err.Message);
            }
        }
        #endregion


        #region BtnCloseTRACESDownloadAvailableList_Click
        private void BtnCloseTRACESDownloadAvailableList_Click(object sender, EventArgs e)
        {
            grpTRACESDownloadAvailableList.Visible = false;
        }
        #endregion

        private void BtnVerifyTokenNo_Click(object sender, EventArgs e)
        {
            if (lstTRACESDownloadAvailableList.SelectedIndex < 0)
            {
                cmnService.J_UserMessage(
                    "Please select a token.");
                return;
            }

            //-------------------------------------------------
            // FETCH SELECTED TOKEN
            //-------------------------------------------------
            string selectedText =  lstTRACESDownloadAvailableList.SelectedItem.ToString();

            string selectedToken =
                selectedText.Split('|')[0].Trim();

            //-------------------------------------------------
            // STORE TOKEN
            //-------------------------------------------------
            filePathCSVTokenNo = selectedToken;
            grpTRACESDownloadAvailableList.Visible = false;
            //-------------------------------------------------
            // START DOWNLOAD PROCESS
            //-------------------------------------------------
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.DownloadCSVFile);
            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync(objList);
            }

        }

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

        #region GenMaskedName
        private string GenMaskedName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "";

            // Normalize
            name = name.ToUpper().Trim();
            name = Regex.Replace(name, @"\s+", " ");       // remove extra spaces
            name = Regex.Replace(name, @"[^A-Z ]", "");    // remove special chars

            var words = name.Split(' ');
            List<string> maskedWords = new List<string>();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (word.Length == 1)
                {
                    maskedWords.Add(word);
                }
                else if (word.Length == 2)
                {
                    maskedWords.Add(word); // usually unchanged
                }
                else
                {
                    string masked =
                        word[0] +
                        new string('X', word.Length - 2) +
                        word[word.Length - 1];

                    maskedWords.Add(masked);
                }
            }

            return string.Join(" ", maskedWords);
        }
        #endregion

        #region ReadPANStatus
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

        #region SAVE THE DATA IN LOG
        private void SaveLogData(long BasicInfoID, string TANNo, int CorrFlag, string TokenNo, int NoOfPANs )
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
            string strDate = "";
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strDate = DateTime.Now.ToString();
            else
                strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //
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
                                                        + cmnService.J_DateOperator() + strDate + cmnService.J_DateOperator() + ")";
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
            string strDate = "";
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                strDate = DateTime.Now.ToString();
            else
                strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //
            if (CorrFlag > 0)
                strSQL = @"UPDATE TRN_TRACES_PAN_VERIFICATION_LOG SET END_DATE_TIME = " + cmnService.J_DateOperator() + strDate + cmnService.J_DateOperator() + " WHERE BASIC_INFO_ID =" + BasicInfoID + "AND CORR_FLAG = " + CorrFlag + " AND TOKEN_NO ='" + TokenNo + "' AND END_DATE_TIME IS NULL";
            else
                strSQL = @"UPDATE TRN_TRACES_PAN_VERIFICATION_LOG SET END_DATE_TIME = " + cmnService.J_DateOperator() + strDate + cmnService.J_DateOperator() + " WHERE BASIC_INFO_ID =" + BasicInfoID + " AND TOKEN_NO ='" + TokenNo + "' AND END_DATE_TIME IS NULL";
            //
            string strTokenNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (strTokenNo != "")
                return strTokenNo;
            return "";
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
                //cmnService.J_ClearComboBox(ref combobox);
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
    }
}