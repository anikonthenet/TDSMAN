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
    public partial class TrnBulk197CertificateValidation : Form
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public TrnBulk197CertificateValidation()
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

        #region Objects & Variables decleration

        TracesConnect objTracesConnect = new TracesConnect();
        Certificate197Data objData = new Certificate197Data();
        //
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        string strExcelFileNameWithPath = "";
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
        int intPANId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0, intVALID_FROM = 0, intVALID_TO = 0, intSECTION_CODE = 0, intNATURE_OF_PAYMENT = 0, intRATE_OF_TDS = 0, intCREDIT_LIMIT = 0, intAMOUNT_CONSUMED = 0, intISSUE_DATE = 0;
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
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //
        enum enmRequestType
        {
            Login,
            PanValidation,
            List,
            LogOff
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        //--
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        #endregion


        #region set ENUM

        #region T_GRID_COLUMN

        public enum T_GRID_COLUMN
        {
            PAN_NO = 0,
            DED_EMP_NAME = 1,
            CERTIFICATE_NO = 2,
            STATUS_ID = 3,
            VALID_FROM = 4,
            VALID_TO = 5,
            SECTION_CODE = 6,
            NATURE_OF_PAYMENT = 7,
            RATE_OF_TDS = 8,
            CREDIT_LIMIT = 9,
            AMOUNT_CONSUMED = 10,
            ISSUE_DATE = 11,
            none
        }
        #endregion

        #region T_WEB_GRID_COLUMN

        public enum T_WEB_GRID_COLUMN
        {
            SERIAL_NO = 0,
            CERTIFICATE_NO = 1,
            FA_YEAR = 2,
            PAN_NO = 3,
            DED_NAME = 4,
            VALID_FROM = 5,
            VALID_TO = 6,
            SECTION_CODE = 7,
            NATURE_OF_PAYMENT = 8,
            RATE_OF_TDS = 9,
            CREDIT_LIMIT =10,
            AMOUNT_CONSUMED = 11,
            ISSUE_DATE = 12,
            CANCEL_DATE = 13,
            CERTIFICATE_ID = 14,
            none
        }
        #endregion    

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
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                this.ClientSize = new Size(w, h);
                //
                ClearFields(); 
                //--
                strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "";
                dmlService.J_ExecSql(strSQL);
                //--
                if (TDSMAN.Classes.TDSMAN.T_FromModule == T_OTHERMODULENAME.IMPORT_EXCEL_REGULAR)
                {
                    LoadFormTools();
                    //
                    grpReturnSelection.Visible = false;
                    grpRegularReturn.Visible = false;
                    grpCorrectionReturn.Visible = false;
                    pnlLine.Visible = false;
                    //
                    dgvDeductees.Location = new Point(12, 47);
                    dgvDeductees.Height = 541;
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
                    intVALID_FROM = 5; intVALID_TO = 6; intSECTION_CODE = 7; intNATURE_OF_PAYMENT = 8; intRATE_OF_TDS = 9; intCREDIT_LIMIT = 10; intAMOUNT_CONSUMED = 11; intISSUE_DATE = 12;
                    intVerifyId = 5;
                    //--
                    LoadDeducteeGridExcelImport();
                }
                else
                {
                    //LoadBatch();
                    //
                    rbnRegularCorrectionReturn_CheckedChanged(sender, e);
                }
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
            //-----------------------------------------------
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        cmbFormNo.Text);
            //
            if (cmbFormNo.Text == T_FormNo.F24Q)
                strEmpDed = "Employee";
            else
                strEmpDed = "Deductee";
            //--
            intPANId = 1;
            intNameEntered = 2;
            intNameVerified = 3;
            intStatusId = 4;
            intVALID_FROM = 5; intVALID_TO = 6; intSECTION_CODE = 7; intNATURE_OF_PAYMENT = 8; intRATE_OF_TDS = 9; intCREDIT_LIMIT = 10; intAMOUNT_CONSUMED = 11; intISSUE_DATE = 12;
            intVerifyId = 5;
            //
            blRegular = false;
            //
            if(lngBasicInfoID==0)
            {
                LoadDeducteeGrid();
                return;
            }
            //--
            LoadDeducteeGrid(lngBasicInfoID, chkBoxNewEntriesOnly.Checked);
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
                LoadDeducteeGridCorr(lngBasicInfoID, chkBoxNewEntriesOnly.Checked);
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
                LoadDeducteeGridCorr(lngBasicInfoID, chkBoxNewEntriesOnly.Checked);
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
                //if (ValidateFields() == false) return;
                //--
                if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                {
                    if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_CERTIFICATE_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial)
                    {
                        //cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial.ToString() + " records permitted", MessageBoxIcon.Exclamation);
                        cmnService.J_UserMessage("Bulk Certificate Verification exhausted for Trial Version... maximum " + TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial.ToString() + " records permitted", MessageBoxIcon.Exclamation);
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
                    cmnService.J_UserMessage("No Certificate found", MessageBoxIcon.Exclamation);
                    return;
                }
                //--
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //
                //--
                //strSQL = "DELETE FROM " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "";
                //dmlService.J_ExecSql(strSQL);
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
        
        #region bgwPANVerification_DoWork
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList objRetval = new ArrayList();
            TracesResponse objResponse = new TracesResponse();
            Label.CheckForIllegalCrossThreadCalls = false;
            string strSTATUS = ""; string strVerifiedNAME = "";
            if (btnVerification.Text != strbtnVerification)
            {
                this.Cursor = Cursors.WaitCursor;
                //pctGreenDownArrow.Visible = true;
                //--
                for (int i = j; i <= dgvDeductees.RowCount - 1; i++)
                {
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        this.Cursor = Cursors.Default;
                        //
                        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
                        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified\n[Internet Connectivity not found]";
                        //
                        //lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);
                    }
                    else
                    {
                        ////////TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value),
                        ////////                                                                   out strVerifiedNAME);

                        ArrayList objList = new ArrayList();
                        objList.Add(enmRequestType.List);

                        objData.CertificateNo = dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.CERTIFICATE_NO].Value.ToString();
                        objData.PAN = dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.PAN_NO].Value.ToString();
                        if (rbnRegularReturn.Checked == true)
                        {
                            if (cmbFinancialYear.SelectedIndex > 0)
                                objData.FinYear = cmbFinancialYear.Text.Substring(0, cmbFinancialYear.Text.IndexOf("-"));
                        }
                        else if(rbnCorrectionReturn.Checked==true)
                        {
                            objData.FinYear = Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value).Substring(0, Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[1].Value).IndexOf("-"));
                        }
                        //
                        //TracesResponse response = objTracesConnect.RequestForCertificate197(objData);
                        TracesResponse response = objTracesConnect.RequestForCertificate197_New(objData);
                        objRetval.Add(enmRequestType.List);
                        objRetval.Add(response);
                        e.Result = objRetval;
                        //--
                        this.Cursor = Cursors.Default;
                        //
                        if (response.Respons == enmResponse.Success)
                        {
                            //dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Green;
                            //dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Verified";

                            //ArrayList objMessage = (ArrayList)e.Result;
                            //enmRequestType enmReqType = (enmRequestType)objMessage[0];
                            //TracesResponse objResponse = (TracesResponse)objMessage[1];

                            ArrayList objMesg = (ArrayList)response.CustomeTypes;
                            if (Convert.ToInt32(objMesg[0].ToString()) > 0)
                            {                                
                                //
                                DataTable dt = (DataTable)objMesg[1];
                                if (dt.Rows.Count == 0)
                                {
                                    dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Red;
                                    dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Not Verified";
                                    lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                                }
                                else
                                {
                                    //
                                    if (dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.CERTIFICATE_NO].Value.ToString() != Convert.ToString(dt.Rows[0].ItemArray[1]))
                                    {
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Red;
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Not Verified";
                                        lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                                    }
                                    else
                                    {
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.VALID_FROM].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.VALID_FROM]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.VALID_TO].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.VALID_TO]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.SECTION_CODE].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.SECTION_CODE]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.NATURE_OF_PAYMENT].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.NATURE_OF_PAYMENT]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.RATE_OF_TDS].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.RATE_OF_TDS]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.CREDIT_LIMIT].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.CREDIT_LIMIT]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.AMOUNT_CONSUMED].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.AMOUNT_CONSUMED]);
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.ISSUE_DATE].Value = Convert.ToString(dt.Rows[0].ItemArray[(int)T_WEB_GRID_COLUMN.ISSUE_DATE]);
                                        //--
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Green;
                                        dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Verified";
                                        lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
                                    }
                                }
                                //dgvDeductees.Rows[i].Cells[intAMOUNT_CONSUMED].Value = Convert.ToString(dt.Rows[0].ItemArray[intAMOUNT_CONSUMED]);
                                //Convert.ToString(dt.Rows[0].ItemArray[1]);
                                dt.Clear();
                            }
                            else
                            {
                                //InitializeCaptcha();
                                //cmnService.J_UserMessage("No data available for the specified search criteria");
                                dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Red;
                                dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Not Verified";
                                lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                            }
                            //dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
                            //--
                            //dgvDeductees.Rows[i].Cells[intNameVerified].Value = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
                            //--
                            //if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == "") //-- NO NAME
                            //{
                            //    this.Cursor = Cursors.Default;
                            //    //
                            //    dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Silver;
                            //    dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
                            //    //
                            //    lblNotVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNo.Text) + 1);

                            //}
                            //else
                            //{
                            //    if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) == strNOTAVAILABLE) //-- NOT AVAILABLE / 
                            //    {
                            //        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                            //        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Invalid";
                            //        lblInvalidNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblInvalidNo.Text) + 1);
                            //        //--                
                            //        strInvalidPAN = strInvalidPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
                            //        //
                            //        if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                            //        {
                            //            dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN) " +
                            //                                 "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value) + "'," +
                            //                                 "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "')");
                            //        }
                            //    }
                            //    else //-- NAME PRESENT / ACTIVE
                            //    {
                            //        dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor = Color.LawnGreen;
                            //        dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active";
                            //        lblVerifiedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedNo.Text) + 1);
                            //        //--
                            //        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                            //        {
                            //            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                            //                "VALUES('" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                            //                "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                            //        }
                            //        else
                            //        {
                            //            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "' " +
                            //                " WHERE PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'");
                            //        }
                            //        //-- NAME NOT MATCHED
                            //        if (Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value) != Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value))
                            //        {
                            //            if (dgvDeductees.Rows[i].Cells[intStatusId].Style.BackColor != Color.Red)
                            //            {
                            //                dgvDeductees.Rows[i].Cells[intNameEntered].Style.BackColor = Color.Tan;
                            //                dgvDeductees.Rows[i].Cells[intStatusId].ToolTipText = "Active\nbut name unmatched with the entered value";
                            //                lblUnmatchedNo.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedNo.Text) + 1);
                            //                //--                
                            //                strUnmatchedPAN = strUnmatchedPAN + "," + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value);
                            //                //--
                            //                if (dmlService.J_IsRecordExist(TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN, " PAN ='" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'") == false)
                            //                {
                            //                    dmlService.J_ExecSql("INSERT INTO " + TDSMAN.Classes.TDSMAN.T_tblTEMP_INVALID_PAN + "(NAME_DESC, PAN, VERIFIED_NAME) " +
                            //                                             "VALUES('" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameEntered].Value)) + "'," +
                            //                                             "'" + Convert.ToString(dgvDeductees.Rows[i].Cells[intPANId].Value) + "'," +
                            //                                             "'" + cmnService.J_ReplaceQuote(Convert.ToString(dgvDeductees.Rows[i].Cells[intNameVerified].Value)) + "')");
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            //--
                        }
                        else if (response.Respons == enmResponse.Failed)
                        {
                            dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Red;
                            dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Not Verified";
                        }
                        //-- 2019/12/23
                        else if (response.Respons == enmResponse.Failed && response.Message.Contains("Internal Server Error") == true)
                        {
                            dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].Style.BackColor = Color.Gray;
                            dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.STATUS_ID].ToolTipText = "Internal Server Error (returned from TRACES)";
                        }
                        //--
                        if (response.Respons == enmResponse.SessionTimeout)
                        {
                            objResponse.Respons = enmResponse.SessionTimeout;
                            objRetval.Add(enmRequestType.PanValidation);
                            objRetval.Add(objTracesConnect);
                            e.Result = objTracesConnect;
                            break;
                        }
                        //--

                    }
                    //--
                    j = j + 1;
                    //--
                    if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                    {
                        //--
                        strSQL = "UPDATE MST_SETUP SET TRIAL_CERTIFICATE_VERIFY_RECORDS = TRIAL_CERTIFICATE_VERIFY_RECORDS + 1 ";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_CERTIFICATE_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial)
                        {
                            //cmnService.J_UserMessage("Bulk PAN Validation exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial.ToString() + " records permitted");
                            cmnService.J_UserMessage("Bulk Certificate Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxCertificateVerifyTrial.ToString() + " records permitted");
                            return;
                        }
                        //--
                        //if (j == 10)
                        //{
                        //    cmnService.J_UserMessage("Only 10 PANs can be validated per return using the Trial Version");
                        //    blnVerificationComplete = false;
                        //    break;
                        //}
                    }
                    //--
                    if (bgwPANVerification.CancellationPending)//checks for cancel request
                    {
                        blnVerificationComplete = false;
                        //pctGreenDownArrow.Visible = false;
                        break;
                    }
                    //
                    if (i > 18)
                        dgvDeductees.FirstDisplayedScrollingRowIndex = dgvDeductees.FirstDisplayedScrollingRowIndex + 1;
                }
                //
                blnVerificationComplete = true;
                //pctGreenDownArrow.Visible = false;
            }
            //--
            if (bgwPANVerification.CancellationPending)
            {
                e.Cancel = true;
                blnVerificationComplete = false;
                //pctGreenDownArrow.Visible = false;
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
            //--
            if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                btnPrintInvalidPAN.Enabled = true;
            else
                btnPrintInvalidPAN.Enabled = false;
            //--
            if (cmnService.J_ReturnInt32Value(lblVerifiedNo.Text) > 0)
            {
                btnPrintVerified.Enabled = true;
                //if (rbnRegularReturn.Checked == true)
                //    btnUpdateMasterData.Visible = true;
            }
            else
            {
                btnPrintVerified.Enabled = false;
                //if (rbnRegularReturn.Checked == true)
                //    btnUpdateMasterData.Visible = false;
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
            string strExcelFilePath = "";
            try
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
                    strExcelFilePath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                if (strExcelFilePath.Trim() == "") return;
                //
                if (rbnRegularReturn.Checked == true)
                {
                    strExcelFilePath = Path.Combine(strExcelFilePath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text, 11), 10) + "_INVALID_CERTIFICATE(S).XLSX");
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    strExcelFilePath = Path.Combine(strExcelFilePath, Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value) + "_INVALID_CERTIFICATE(S).XLSX");
                }
                //strExcelFilePath =Path.Combine(strExcelFilePath,"VALID_CERTIFICATE(S).XLSX");
                // CHECK IF SAME NAME FILE EXIST
                if (File.Exists(strExcelFilePath))  //-- 01/01/2017 --
                {
                    cmnService.J_UserMessage("File exists with same name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //@@@@@@@@@@@@@@@@@@@@@@@
                // creating Excel Application  
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                // changing the name of active sheet  
                object m = Type.Missing;
                worksheet.Name = "Invalid Certificate(s)";
                int x = 0, y = 0;
                // storing header part in Excel  
                for (int i = 0; i < 3; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvDeductees.Columns[i].HeaderText;
                }
                //-- storing Each row and column value to excel sheet  
                //for (int i = 0; i < dgvDeductees.Rows.Count - 1; i++)
                for (int i = 0; i < dgvDeductees.Rows.Count; i++)
                {
                    if (dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.VALID_FROM].Value.ToString().Trim() == "")
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            //worksheet.Cells[i + 2, j + 1] = dgvDeductees.Rows[i].Cells[j].Value.ToString();
                            if (j == (int)T_GRID_COLUMN.STATUS_ID)
                                worksheet.Cells[x + 2, y + 1] = "Invalid";
                            else
                                worksheet.Cells[x + 2, y + 1] = dgvDeductees.Rows[i].Cells[j].Value.ToString();
                            y++;
                        }
                        x++; y = 0;
                    }
                }
                // save the application  
                workbook.SaveAs(strExcelFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application 

                //workbook.Save();

                //workbook = null;
                ////
                //worksheet = null;
                //app.Quit();
            }
            catch
            {
            }
        }
        #endregion

        #region btnPrintInvalidPAN_MouseClick
        private void btnPrintInvalidPAN_MouseClick(object sender, MouseEventArgs e)
        {
            //if (strInvalidPAN == "") { cmnService.J_UserMessage("No invalid PAN to print!!"); return; }
            ////--
            //if (e.Button == MouseButtons.Left)
            //    cntxtMnuGrpInvalidPAN.Show(btnPrintInvalidPAN, new Point(e.X, e.Y));
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
                    rptDialog.PrintInvalidPAN(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null,"");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintInvalidPAN(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintInvalidPAN(lngBasicInfoID, "TRUE", "");
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
                if(string.IsNullOrEmpty(txtExcelPath.Text.Trim()))
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
                        rptDialog.PrintInvalidPAN(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintInvalidPAN(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintInvalidPAN(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
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
                        rptDialog.PrintUnmatchedNames(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                    }
                    else
                    {
                        if (rbnCorrectionReturn.Checked == true)
                            rptDialog.PrintUnmatchedNames(lngBasicInfoID, "FALSE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
                        else if (rbnRegularReturn.Checked == true)
                            rptDialog.PrintUnmatchedNames(lngBasicInfoID, "TRUE", Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text));
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
            //if (strUnmatchedPAN == "") { cmnService.J_UserMessage("No unmatched name(s) to print!!"); return; }
            ////--
            //if (e.Button == MouseButtons.Left)
            //    cntxtMnuGrpNameDifference.Show(btnPrintVerified, new Point(e.X, e.Y));
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
                    rptDialog.PrintUnmatchedNames(TDSMAN.Classes.TDSMAN.T_pBasicInfoId, null, "");
                }
                else
                {
                    if (rbnCorrectionReturn.Checked == true)
                        rptDialog.PrintUnmatchedNames(lngBasicInfoID, "FALSE", "");
                    else if (rbnRegularReturn.Checked == true)
                        rptDialog.PrintUnmatchedNames(lngBasicInfoID, "TRUE", "");
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
                if (txtUserID.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Enter User ID");
                    txtUserID.Select();
                    return;
                }
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
                if (rbnRegularReturn.Checked == true)
                {
                    if (txtTANNo.Text.Trim() != cmnService.J_Left(cmnService.J_Right(cmbCompany.Text, 11), 10))
                    {
                        if (cmnService.J_UserMessage("The TAN of the return is different from the TAN enterd in the Traces Login, this will lead in wrong verification.\nDo you still want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    if (txtTANNo.Text.Trim() != Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value))
                    {
                        if(cmnService.J_UserMessage("The TAN of the return is different from the TAN enterd in the Traces Login, this will lead in wrong verification.\nDo you still want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
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
                    //
                    dmlService.J_ExecSql(strSQL);
                }
                //#################
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTANNo.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
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
            string strExcelFilePath = "";
            try
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
                    strExcelFilePath = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                if (strExcelFilePath.Trim() == "") return;
                //
                if (rbnRegularReturn.Checked == true)
                {
                    strExcelFilePath = Path.Combine(strExcelFilePath, cmnService.J_Left(cmnService.J_Right(cmbCompany.Text,11),10) + "_VALID_CERTIFICATE(S).XLSX");
                }
                else if (rbnCorrectionReturn.Checked == true)
                {
                    strExcelFilePath = Path.Combine(strExcelFilePath, Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value) + "_VALID_CERTIFICATE(S).XLSX");
                }
                //strExcelFilePath =Path.Combine(strExcelFilePath,"VALID_CERTIFICATE(S).XLSX");
                // CHECK IF SAME NAME FILE EXIST
                if (File.Exists(strExcelFilePath))  //-- 01/01/2017 --
                {
                    cmnService.J_UserMessage("File exists with same name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //@@@@@@@@@@@@@@@@@@@@@@@
                // creating Excel Application  
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet =(Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet =(Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                // changing the name of active sheet  
                object m = Type.Missing;
                worksheet.Name = "Valid Certificate(s)";
                int x = 0, y = 0;
                // storing header part in Excel  
                for (int i = 0; i < dgvDeductees.Columns.Count ; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvDeductees.Columns[i].HeaderText;
                }
                //-- storing Each row and column value to excel sheet  
                //for (int i = 0; i < dgvDeductees.Rows.Count - 1; i++)
                for (int i = 0; i < dgvDeductees.Rows.Count; i++)
                {
                    if (dgvDeductees.Rows[i].Cells[(int)T_GRID_COLUMN.VALID_FROM].Value.ToString().Trim() != "")
                    {
                        for (int j = 0; j < dgvDeductees.Columns.Count; j++)
                        {
                            //worksheet.Cells[i + 2, j + 1] = dgvDeductees.Rows[i].Cells[j].Value.ToString();
                            if (j == (int)T_GRID_COLUMN.STATUS_ID)
                                worksheet.Cells[x + 2, y + 1] = "Valid";
                            else
                                worksheet.Cells[x + 2, y + 1] = dgvDeductees.Rows[i].Cells[j].Value.ToString();
                            y++;
                        }
                        x++; y = 0;
                    }
                }
                // save the application  
                workbook.SaveAs(strExcelFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application 

                //workbook.Save();

                //workbook = null;
                ////
                //worksheet = null;
                //app.Quit();
            }
            catch
            {
            }
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
                    objResponse = objTracesConnect.makeLoginToTRACES((TracesLogin)objList[1]);
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    //---------------------------------------------------
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

        #endregion

        #region bgWorker_RunWorkerCompleted
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList objMessage = (ArrayList)e.Result;
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
                        if (objResponse.Respons == enmResponse.Success)
                        {
                            ShowHideLoginDetails(enmRequestType.PanValidation);
                            //grdPANValidate.DataSource = objBindingSource;
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
        
        #region LoadDeducteeGrid

        #region LoadDeducteeGrid()

        //private void LoadDeducteeGrid()
        //{
        //    DataSet dsetGridClone = new DataSet();
        //    try
        //    {
        //        //-----------------------------------------------------------
        //        string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
        //                                           {"Challan No.", "85", "", "Right", "", "", ""},
        //                                           {"Deductee No.", "85", "", "Right", "", "", ""},
        //                                           {"PAN No.", "85", "S", "", "", "", ""},
        //                                           {strEmpDed + " Name", "80", "S", "", "", "", "T"},
        //                                           {"Certificate No.", "85", "S", "", "", "", ""},
        //                                           {"Status  ", "44", "S", "", "", "", ""},
        //                                           {"VALID_FROM", "0", "S", "", "", "F", ""},
        //                                           {"VALID_TO", "0", "S", "", "", "F", ""},
        //                                           {"SECTION_CODE", "0", "S", "", "", "F", ""},
        //                                           {"NATURE_OF_PAYMENT", "0", "S", "", "", "F", ""},
        //                                           {"RATE_OF_TDS", "0", "S", "", "", "F", ""},
        //                                           {"CREDIT_LIMIT", "0", "S", "", "", "F", ""},
        //                                           {"AMOUNT_CONSUMED", "0", "S", "", "", "F", ""},
        //                                           {"ISSUE_DATE", "0", "S", "", "", "F", ""},
        //                                           {"TDS_VALUE", "0", "S", "", "", "F", ""}};
        //        //-----------------------------------------------------------
        //        //strMatrix = strMatrix1;
        //        //-----------------------------------------------------------
        //        /* (1) Column Value
        //         * (2) Column Data Type
        //         * (3) Replace String
        //         * (4) Replace String Data Type */
        //        //-----------------------------------------------------------
        //        //-----------------------------------------------------------
        //        //            
        //        strSQL = "SELECT   DISTINCT MST_EMPLOYEE.EMPLOYEE_ID   AS DEDUCTEE_ID," +
        //                 "         ' '                         AS CHALLAN_SL_NO," +
        //                 "         ' '                         AS DEDUCTEE_SL_NO," +
        //                 "         MST_EMPLOYEE.EMPLOYEE_PAN  AS DEDUCTEE_PAN," +
        //                 "         MST_EMPLOYEE.EMPLOYEE_NAME AS DEDUCTEE_NAME," +
        //                 "         ' '                         AS CERTIFICATE_NO," +
        //                 "         ' '                         AS STATUS," +
        //                 "         ' '                         AS VALID_FROM," +
        //                 "         ' '                         AS VALID_TO," +
        //                 "         ' '                         AS SECTION_CODE," +
        //                 "         ' '                         AS NATURE_OF_PAYMENT," +
        //                 "         ' '                         AS RATE_OF_TDS," +
        //                 "         ' '                         AS CREDIT_LIMIT," +
        //                 "         ' '                         AS AMOUNT_CONSUMED," +
        //                 "         ' '                         AS ISSUE_DATE," +
        //                 "         ' '                         AS TDS_VALUE " +
        //                 "FROM     TRN_DEDUCTEE_DETAILS," +
        //                 "         MST_EMPLOYEE  " +
        //                 "WHERE    TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID " +
        //                 "AND      1=2 ";
        //        //-----------------------------------------------------------
        //        if (dsetGridClone != null) dsetGridClone.Clear();
        //        dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
        //        dgvDeductees.ClearSelection();
        //        //
        //        grpStatus.Text = " Total Record";//(s): " + dgvDeductees.Rows.Count + " ";
        //        //
        //    }
        //    catch
        //    {

        //    }
        //}

        private void LoadDeducteeGrid()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"PAN No.", "85", "S", "", "", "", ""},
                                                   {strEmpDed + " Name", "80", "S", "", "", "", "T"},
                                                   {"Certificate No.", "85", "S", "", "", "", ""},
                                                   {"Status  ", "44", "S", "", "", "", ""},
                                                   {"Valid From", "0", "", "", "", "F", ""},
                                                   {"Valid To", "0", "", "", "", "F", ""},
                                                   {"Section Code", "0", "", "", "", "F", ""},
                                                   {"Nature of Payment", "0", "", "", "", "F", ""},
                                                   {"Rate of TDS", "0", "", "", "", "F", ""},
                                                   {"Credit Limit", "0", "", "", "", "F", ""},
                                                   {"Amount Consumed", "0", "", "", "", "F", ""},
                                                   {"Issue Date", "0", "", "", "", "F", ""}};
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
                strSQL = "SELECT   DISTINCT MST_EMPLOYEE.EMPLOYEE_PAN  AS DEDUCTEE_PAN," +
                         "         MST_EMPLOYEE.EMPLOYEE_NAME AS DEDUCTEE_NAME," +
                         "         ' '                         AS CERTIFICATE_NO," +
                         "         ' '                         AS STATUS," +
                         "         ' '                         AS VALID_FROM," +
                         "         ' '                         AS VALID_TO," +
                         "         ' '                         AS SECTION_CODE," +
                         "         ' '                         AS NATURE_OF_PAYMENT," +
                         "         ' '                         AS RATE_OF_TDS," +
                         "         ' '                         AS CREDIT_LIMIT," +
                         "         ' '                         AS AMOUNT_CONSUMED," +
                         "         ' '                         AS ISSUE_DATE " +
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

        #region LoadDeducteeGrid(long BasicInfoID)
        //private void LoadDeducteeGrid(long BasicInfoID, bool NewEntriesOnly)
        //{
        //    DataSet dsetGridClone = new DataSet();
        //    try
        //    {
        //        //-----------------------------------------------------------
        //        string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
        //                                           {"Challan No.", "85", "", "Right", "", "", ""},
        //                                           {"Deductee No.", "85", "", "Right", "", "", ""},
        //                                           {"PAN No.", "85", "S", "", "", "", ""},
        //                                           {strEmpDed + " Name", "80", "S", "", "", "", "T"},
        //                                           {"Certificate No.", "85", "S", "", "", "", ""},
        //                                           {"Status  ", "44", "S", "", "", "", ""},
        //                                           {"VALID_FROM", "0", "", "", "", "F", ""},
        //                                           {"VALID_TO", "0", "", "", "", "F", ""},
        //                                           {"SECTION_CODE", "0", "", "", "", "F", ""},
        //                                           {"NATURE_OF_PAYMENT", "0", "", "", "", "F", ""},
        //                                           {"RATE_OF_TDS", "0", "", "", "", "F", ""},
        //                                           {"CREDIT_LIMIT", "0", "", "", "", "F", ""},
        //                                           {"AMOUNT_CONSUMED", "0", "", "", "", "F", ""},
        //                                           {"ISSUE_DATE", "0", "", "", "", "F", ""}};
        //        //-----------------------------------------------------------
        //        //strMatrix = strMatrix1;
        //        //-----------------------------------------------------------
        //        /* (1) Column Value
        //         * (2) Column Data Type
        //         * (3) Replace String
        //         * (4) Replace String Data Type */
        //        //-----------------------------------------------------------
        //        //-----------------------------------------------------------
        //        if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == "")
        //        {
        //            TDSMAN.Classes.TDSMAN.T_pTabFormCaption = cmbFormNo.Text;
        //            TDSMAN.Classes.TDSMAN.T_pQuarter = cmbQuarter.Text;
        //        }
        //        //
        //        strSQL = "SELECT  TRN_DEDUCTEE_DETAILS.DEDUCTEE_DETAIL_ID               AS DEDUCTEE_DETAIL_ID," +
        //                "         TRN_CHALLAN.SL_NO                                     AS CHALLAN_SL_NO," +
        //                "         TRN_DEDUCTEE_DETAILS.SL_NO                            AS DEDUCTEE_SL_NO," +
        //                "         MST_" + strEmpDed + "." + strEmpDed + "_PAN           AS DEDUCTEE_PAN," +
        //                "         MST_" + strEmpDed + "." + strEmpDed + "_NAME          AS DEDUCTEE_NAME," +
        //                "         TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO                   AS CERTIFICATE_NO, " +
        //                "         ' '                                                    AS STATUS," +
        //                "         ' '                                                    AS VALID_FROM," +
        //                "         ' '                                                    AS VALID_TO," +
        //                "         ' '                                                    AS SECTION_CODE," +
        //                "         ' '                                                    AS NATURE_OF_PAYMENT," +
        //                "         ' '                                                    AS RATE_OF_TDS," +
        //                "         ' '                                                    AS CREDIT_LIMIT," +
        //                "         ' '                                                    AS AMOUNT_CONSUMED," +
        //                "         ' '                                                    AS ISSUE_DATE " +
        //                "FROM    ((TRN_DEDUCTEE_DETAILS " +
        //                "         INNER JOIN MST_" + strEmpDed + "  " +
        //                "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
        //                "         INNER JOIN TRN_CHALLAN " +
        //                "      ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID) " +
        //                "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
        //                "AND      TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO <> '' ";
        //        //
        //        strSQL = strSQL + "ORDER BY " + strEmpDed + "_NAME, TRN_CHALLAN.SL_NO , TRN_DEDUCTEE_DETAILS.SL_NO";                
        //        //-----------------------------------------------------------
        //        if (dsetGridClone != null) dsetGridClone.Clear();
        //        dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
        //        dgvDeductees.ClearSelection();
        //        //
        //        grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
        //        //
        //    }
        //    catch
        //    {

        //    }
        //}
        private void LoadDeducteeGrid(long BasicInfoID, bool NewEntriesOnly)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"PAN No.", "100", "S", "", "", "", ""},
                                                   {strEmpDed + " Name", "80", "S", "", "", "", ""},
                                                   {"Certificate No.", "100", "S", "", "", "", ""},
                                                   {"Status  ", "44", "S", "", "", "", "T"},
                                                   {"Valid From", "0", "", "", "", "F", ""},
                                                   {"Valid To", "0", "", "", "", "F", ""},
                                                   {"Section Code", "0", "", "", "", "F", ""},
                                                   {"Nature of Payment", "0", "", "", "", "F", ""},
                                                   {"Rate of TDS", "0", "", "", "", "F", ""},
                                                   {"Credit Limit", "0", "", "", "", "F", ""},
                                                   {"Amount Consumed", "0", "", "", "", "F", ""},
                                                   {"Issue Date", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                if (TDSMAN.Classes.TDSMAN.T_pTabFormCaption == "")
                {
                    TDSMAN.Classes.TDSMAN.T_pTabFormCaption = cmbFormNo.Text;
                    TDSMAN.Classes.TDSMAN.T_pQuarter = cmbQuarter.Text;
                }
                //
                strSQL = "SELECT  DISTINCT MST_" + strEmpDed + "." + strEmpDed + "_PAN   AS DEDUCTEE_PAN," +
                        "         MST_" + strEmpDed + "." + strEmpDed + "_NAME           AS DEDUCTEE_NAME," +
                        "         TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO                    AS CERTIFICATE_NO, " +
                        "         ' '                                                    AS STATUS," +
                        "         ' '                                                    AS VALID_FROM," +
                        "         ' '                                                    AS VALID_TO," +
                        "         ' '                                                    AS SECTION_CODE," +
                        "         ' '                                                    AS NATURE_OF_PAYMENT," +
                        "         ' '                                                    AS RATE_OF_TDS," +
                        "         ' '                                                    AS CREDIT_LIMIT," +
                        "         ' '                                                    AS AMOUNT_CONSUMED," +
                        "         ' '                                                    AS ISSUE_DATE " +
                        "FROM    ((TRN_DEDUCTEE_DETAILS " +
                        "         INNER JOIN MST_" + strEmpDed + "  " +
                        "      ON TRN_DEDUCTEE_DETAILS.PARTY_ID      = MST_" + strEmpDed + "." + strEmpDed + "_ID) " +
                        "         INNER JOIN TRN_CHALLAN " +
                        "      ON TRN_DEDUCTEE_DETAILS.CHALLAN_ID = TRN_CHALLAN.CHALLAN_ID) " +
                        "WHERE    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + lngBasicInfoID + " " +
                        "AND      TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO <> '' ";
                //
                strSQL = strSQL + "ORDER BY " + strEmpDed + "_NAME," + strEmpDed + "_PAN";//, TRN_CHALLAN.SL_NO , TRN_DEDUCTEE_DETAILS.SL_NO";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadDeducteeGridCorr(long BasicInfoID, bool NewEntriesOnly)
        //private void LoadDeducteeGridCorr(long BasicInfoID, bool NewEntriesOnly)
        //{
        //    DataSet dsetGridClone = new DataSet();
        //    try
        //    {
        //        //--
        //        if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
        //            strEmpDed = "Employee";
        //        else
        //            strEmpDed = "Deductee";
        //        //--
        //        intPANId = 0;
        //        intNameEntered = 1;
        //        intNameVerified = 2;
        //        intStatusId = 3;
        //        intStatusId = 6;
        //        intVALID_FROM = 7; intVALID_TO = 8; intSECTION_CODE = 9; intNATURE_OF_PAYMENT = 10; intRATE_OF_TDS = 11; intCREDIT_LIMIT = 12; intAMOUNT_CONSUMED = 13; intISSUE_DATE = 14;

        //        intVerifyId = 4;
        //        //
        //        blRegular = false;
        //        //-----------------------------------------------------------
        //        string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
        //                                           {"Challan No.", "85", "", "Right", "", "", ""},
        //                                           {"Deductee No.", "85", "", "Right", "", "", ""},
        //                                           {"PAN No.", "85", "S", "", "", "", ""},
        //                                            {strEmpDed + " Name", "80", "S", "", "", "", "T"},
        //                                            {"Certificate No.", "85", "S", "", "", "", ""},
        //                                            {"Status  ", "44", "S", "", "", "", ""},
        //                                           {"VALID_FROM", "0", "", "", "", "F", ""},
        //                                           {"VALID_TO", "0", "", "", "", "F", ""},
        //                                           {"SECTION_CODE", "0", "", "", "", "F", ""},
        //                                           {"NATURE_OF_PAYMENT", "0", "", "", "", "F", ""},
        //                                           {"RATE_OF_TDS", "0", "", "", "", "F", ""},
        //                                           {"CREDIT_LIMIT", "0", "", "", "", "F", ""},
        //                                           {"AMOUNT_CONSUMED", "0", "", "", "", "F", ""},
        //                                           {"ISSUE_DATE", "0", "", "", "", "F", ""}};
        //        //-----------------------------------------------------------
        //        //strMatrix = strMatrix1;
        //        //-----------------------------------------------------------
        //        /* (1) Column Value
        //         * (2) Column Data Type
        //         * (3) Replace String
        //         * (4) Replace String Data Type */
        //        //-----------------------------------------------------------
        //        strSQL = @"SELECT  COR_TRN_DEDUCTEE_DETAILS.TRN_DEDUCTEE_DETAIL_ID AS TRN_DEDUCTEE_DETAIL_ID,
        //                           COR_TRN_CHALLAN.SL_NO                           AS CHALLAN_SL_NO,
        //                           COR_TRN_DEDUCTEE_DETAILS.SL_NO                  AS DEDUCTEE_SL_NO,
        //                           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN           AS DEDUCTEE_PAN,
        //                           COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME          AS DEDUCTEE_NAME,
        //                           COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO         AS CERTIFICATE_NO,
        //                           ''                                              AS STATUS ,
        //                           ''                                              AS VALID_FROM,
        //                           ''                                              AS VALID_TO,
        //                           ''                                              AS SECTION_CODE,
        //                           ''                                              AS NATURE_OF_PAYMENT,
        //                           ''                                              AS RATE_OF_TDS,
        //                           ''                                              AS CREDIT_LIMIT,
        //                           ''                                              AS AMOUNT_CONSUMED,
        //                           ''                                              AS ISSUE_DATE
        //                   FROM    COR_TRN_DEDUCTEE_DETAILS, COR_TRN_CHALLAN
        //                   WHERE   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID
        //                   AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + @"
        //                   AND     COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO <> '' ";
        //        if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
        //            strSQL = strSQL + "AND (COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
        //                "OR      COR_TRN_DEDUCTEE_DETAILS.MODE IN ('A','O')) ";
        //        //if (NewEntriesOnly == true)
        //        //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";                
        //        strSQL = strSQL + "ORDER BY DEDUCTEE_NAME," +
        //                "         DEDUCTEE_PAN ";
        //        //-----------------------------------------------------------
        //        if (dsetGridClone != null) dsetGridClone.Clear();
        //        dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
        //        dgvDeductees.ClearSelection();
        //        //--
        //        //
        //        grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
        //        //
        //    }
        //    catch
        //    {

        //    }
        //}

        private void LoadDeducteeGridCorr(long BasicInfoID, bool NewEntriesOnly)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //--
                if (Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT FORM_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID =" + lngBasicInfoID)) == T_FormNo.F24Q)
                    strEmpDed = "Employee";
                else
                    strEmpDed = "Deductee";
                //--
                intPANId = 0;
                intNameEntered = 1;
                intNameVerified = 2;
                intStatusId = 3;
                //intStatusId = 6;
                //intVALID_FROM = 7; intVALID_TO = 8; intSECTION_CODE = 9; intNATURE_OF_PAYMENT = 10; intRATE_OF_TDS = 11; intCREDIT_LIMIT = 12; intAMOUNT_CONSUMED = 13; intISSUE_DATE = 14;
                intStatusId = 4;
                intVALID_FROM = 5; intVALID_TO = 6; intSECTION_CODE = 7; intNATURE_OF_PAYMENT = 8; intRATE_OF_TDS = 9; intCREDIT_LIMIT = 10; intAMOUNT_CONSUMED = 11; intISSUE_DATE = 12;

                intVerifyId = 4;
                //
                blRegular = false;
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"PAN No.", "100", "S", "", "", "", ""},
                                                   {strEmpDed + " Name", "80", "S", "", "", "", ""},
                                                   {"Certificate No.", "100", "S", "", "", "", ""},
                                                   {"Status  ", "44", "S", "", "", "", "T"},
                                                   {"Valid From", "0", "", "", "", "F", ""},
                                                   {"Valid To", "0", "", "", "", "F", ""},
                                                   {"Section Code", "0", "", "", "", "F", ""},
                                                   {"Nature of Payment", "0", "", "", "", "F", ""},
                                                   {"Rate of TDS", "0", "", "", "", "F", ""},
                                                   {"Credit Limit", "0", "", "", "", "F", ""},
                                                   {"Amount Consumed", "0", "", "", "", "F", ""},
                                                   {"Issue Date", "0", "", "", "", "F", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                strSQL = @"SELECT  DISTINCT COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_PAN           AS DEDUCTEE_PAN,
                                   COR_TRN_DEDUCTEE_DETAILS.DEDUCTEE_NAME          AS DEDUCTEE_NAME,
                                   COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO         AS CERTIFICATE_NO,
                                   ''                                              AS STATUS ,
                                   ''                                              AS VALID_FROM,
                                   ''                                              AS VALID_TO,
                                   ''                                              AS SECTION_CODE,
                                   ''                                              AS NATURE_OF_PAYMENT,
                                   ''                                              AS RATE_OF_TDS,
                                   ''                                              AS CREDIT_LIMIT,
                                   ''                                              AS AMOUNT_CONSUMED,
                                   ''                                              AS ISSUE_DATE
                           FROM    COR_TRN_DEDUCTEE_DETAILS, COR_TRN_CHALLAN
                           WHERE   COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = COR_TRN_CHALLAN.BATCH_HEADER_ID
                           AND     COR_TRN_DEDUCTEE_DETAILS.BATCH_HEADER_ID = " + lngBasicInfoID + @"
                           AND     COR_TRN_DEDUCTEE_DETAILS.CERTIFICATE_NO <> '' ";
                if (TDSMAN.Classes.TDSMAN.T_pVerifyPANNewDeductee == true)
                    strSQL = strSQL + "AND (COR_TRN_DEDUCTEE_DETAILS.PAN_UPDATION_INDICATOR = " + T_UPDATION_INDICATOR.ON + " " +
                        "OR      COR_TRN_DEDUCTEE_DETAILS.MODE IN ('A','O')) ";
                //if (NewEntriesOnly == true)
                //    strSQL = strSQL + " AND " + cmnService.J_SQLDBFormat("MST_VERIFIED_PAN.VERIFIED_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " = '' ";                
                strSQL = strSQL + "ORDER BY DEDUCTEE_NAME," +
                        "         DEDUCTEE_PAN ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
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

        #region LoadDeducteeGridExcelImport()
        private void LoadDeducteeGridExcelImport()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                //-----------------------------------------------------------
                string[,] strMatrixViewDeductee = {{"DEDUCTEE_ID", "0", "", "Right", "", "F", ""},
                                        {"PAN No.", "100", "S", "", "", "", ""},
                                        {strEmpDed + " Name (entered)", "80", "S", "", "", "", "T"},
                                        {strEmpDed + " Name (extracted)", "80", "S", "", "", "", "T"},
                                        {"Status", "44", "S", "", "", "", "T"},
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
                string[,] strverifiedPAN = { { "MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL", "F", "0", "T" } };
                                      //{TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + ".ERR_DESC = '' ", "F", "", "T"}};
                //
                strSQL = "SELECT  DISTINCT " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_MASTER_ID AS DEDUCTEE_ID," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN                AS DEDUCTEE_PAN," +
                        "         " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME               AS DEDUCTEE_NAME," +
                        "         ''                                               AS DEDUCTEE_NAME_V," +
                        "         ''                                               AS STATUS," +
                        //"         IIF(MST_VERIFIED_PAN.VERIFIED_PAN_ID IS NULL, 0, MST_VERIFIED_PAN.VERIFIED_PAN_ID) AS VERIFIED," +
                        "         " + cmnService.J_SQLDBFormat(strverifiedPAN, J_SQLColFormat.Case_End) + " AS VERIFIED," +
                        "         MST_VERIFIED_PAN.VERIFIED_NAME                   AS VERIFIED_NAME " +
                        "FROM    (" + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + " LEFT JOIN MST_VERIFIED_PAN " +
                        "      ON MST_VERIFIED_PAN.PAN = " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN) " +
                        "WHERE    " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN <> 'PANNOTAVBL' " +
                        "ORDER BY " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_PAN, " + TDSMAN.Classes.TDSMAN.T_tblTEMP_MST_DEDUCTEE + ".DEDUCTEE_NAME";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvDeductees, strSQL, strMatrixViewDeductee);
                dgvDeductees.ClearSelection();
                //
                //
                grpStatus.Text = " Total Record(s): " + dgvDeductees.Rows.Count + " ";
                //
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
            btnPrintVerified.Enabled = false;
            lblVerifiedNo.Text = "0";
            //lblNotVerifiedNo.Text = "0"; 
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
                    Stream imgStream = objTracesConnect.MakeInitialRequest();
                    Image img = Image.FromStream(imgStream);
                    this.picCaptcha.Image = img;
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
            dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewBatch, strSQL, strMatrixBatchGrid);       //Show Data into the Grid
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }

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
                    //
                    grpStatus.Enabled = true;
                    grpButtons.Enabled = true;
                    dgvDeductees.Enabled = true;
                    //
                    btnVerification.Text = "Stop validating";
                    btnVerification.ForeColor = Color.Red;
                    btnPrintInvalidPAN.Enabled = false;
                    btnPrintVerified.Enabled = false;
                    //
                    bgwPANVerification.RunWorkerAsync();
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
            this.Width = 739;
            this.Height = 666;
            //
            lblTitle.Width = 375;
            pnlTitle.Width = 375;
            lblSearchMode.Location = new Point(553,8);
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
            grpButtons.Location = new Point(513, 597);
            grpStatus.Location = new Point(11, 597);
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
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnBulk197CertificateValidation");
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
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0095", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
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

        #region ExportToExcelFromSQL
        private bool ExportToExcelFromSQL(string strSQL, string SheetName)
        {
            //-- 20/02/2018 --
            //GC.Collect();
            GC.WaitForPendingFinalizers();
            DataSet myDataSet;
            //----------------
            //
            try
            {
                //if (rbnCSVOption.Checked == true)  //-- 2019/01/22
                //{
                //    ExportToCSV(strSQL, Path.Combine(Path.Combine(txtExcelPath.Text, txtDestinationFileName.Text), SheetName + ".csv"));
                //    return true;
                //}
                //else
                //{
                    myDataSet = new DataSet();
                    myDataSet = dmlService.J_ExecSqlReturnDataSet(strSQL);
                    //
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 --
                    Microsoft.Office.Interop.Excel.Workbooks workbook = app.Workbooks;
                    prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    //
                    object m = Type.Missing;
                    Microsoft.Office.Interop.Excel.Workbook wb = workbook.Open(strExcelFileNameWithPath,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                    wsnew.Name = SheetName;
                    //

                    int colIndex = 0;
                    int rowIndex = 1;

                    foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                    {
                        colIndex++;
                        wsnew.Cells[1, colIndex] = dc.ColumnName;
                    }
                    //prgBar.Value = prgBar.Value + 5; //-- 01/01/2018 -
                    foreach (DataRow dr in myDataSet.Tables[0].Rows)
                    {
                        rowIndex++;
                        colIndex = 0;

                        foreach (DataColumn dc in myDataSet.Tables[0].Columns)
                        {
                            colIndex++;
                            //wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                            //if (dr[dc.ColumnName].ToString().Length == 10 && dr[dc.ColumnName].ToString().Contains("/") == true)
                            if (dr[dc.ColumnName].ToString().Length == 10
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 2, 1) == "-")
                                && (cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "/" || cmnService.J_Mid(dr[dc.ColumnName].ToString(), 5, 1) == "-"))
                                wsnew.Cells[rowIndex, colIndex] = "'" + dr[dc.ColumnName];
                            else
                                wsnew.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                        }
                    }

                    wsnew.Columns.AutoFit();

                    wb.Save();
                    wb.Close(m, m, m);

                    wb = null;
                    workbook = null;
                    //
                    wsnew = null;

                    //Marshal.ReleaseComObject(wsnew);
                    //Marshal.ReleaseComObject(wsnew);

                    //wsnew.Delete(); //-- 20/02/2018 --

                    app.Quit();
                    app = null;
                //}
                //--
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
            return true;
        }
        #endregion
        //-----------------------------------------

        #endregion









    }

}