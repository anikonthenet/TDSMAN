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
    #region T_CHALLAN_STATUS
    public struct T_CHALLAN_STATUS
    {
        public const int MATCHED = 1;
        public const int UNMATCHED = 2;
        public const int NOTVERIFIED = 3;
        public const int OTHERERR = 0;
    }
    #endregion

    public partial class TrnBulkChallanVerification : Form
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnBulkChallanVerification()
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

        TracesConnect objTracesConnect = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        //RptDialog rptDialog = new RptDialog();

        string strSQL = string.Empty;
        string strInvalidPAN = "";
        string strUnmatchedPAN = "";
        long lngBasicInfoID = 0;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();
        string strEmpDed = "";
        string strbtnVerification = "&Start Verifying";
        //string strLogOff = "Log off";
        //string strLogOn = "Log on";
        //
        bool blnStatus = false;
        bool blnVerificationComplete = false;
        long lngDeducteeID = 0;
        bool blRegular = true;
        //
        int intChallanId = 0;
        int intNameEntered = 0;
        int intNameVerified = 0;
        int intStatusId = 0;
        int intStatus = 0;
        int intVerifyId = 0;
        //
        string strQuarter = "";
        int intAsstId = 0;
        string strCompanyName = "";
        string strTAN = "";
        string strFormNoBkmark = "";
        string strFAYear = "";
        //
        string strChallanFromDate = "";
        string strChallanToDate = "";
        string strChallanFromDateEntered = "";
        string strChallanToDateEntered = "";
        //
        bool blnSelectComboExit = false;
        //
        int j = 0, J=0;
        //
        BindingSource objBindingSource = new BindingSource();
        private BindingList<PANDetails> _results = new BindingList<PANDetails>();
        //


        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff,
            CINPP,
            CINParticulars,
            ConsumptionDetails,
            BINPP,
            BINParticulars,
            BINPP_Details
        }
        //
        string strNOTAVAILABLE = "NOT AVAILABLE";
        private string CurrentCaptchaId = "";
        //--
        enum enmValidationType
        {
            BinView,
            CinView
        }
        //
        bool blnFromToDateCrossMaxMonthPermitted = false;



        bool blResize = false;

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

        #region TrnBulkChallanVerification_Load
        private void TrnBulkChallanVerification_Load(object sender, EventArgs e)
        {
            blResize = true;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //-
            lblFooterCaption.Text = "Currently, only challan(s) through NSDL can be verified.";
            //
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
                "      ORDER BY COMPANY_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany) == false) return;
            //-----------

            #endregion
            //--
            if(TDSMAN.Classes.TDSMAN.T_pVerifyChallanId > 0)
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
                            AND    TRN_BASIC_INFO.BASIC_INFO_ID = " + TDSMAN.Classes.TDSMAN.T_pVerifyChallanId;
                drdGetDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdGetDetails == null)
                    return;
                //
                string FAYear = "", Company = "", Quarter = "", FormNo = "";
                while (drdGetDetails.Read())
                {
                    blnSelectComboExit = true;
                    Quarter = Convert.ToString(drdGetDetails["QTR"]);
                    Company = Convert.ToString(drdGetDetails["COMPANY_NAME"]) + " [" + Convert.ToString(drdGetDetails["TAN_NO"]) + "]";
                    FAYear = Convert.ToString(drdGetDetails["FA_YEAR"]);
                    FormNo = Convert.ToString(drdGetDetails["FORM_NO"]);
                    blnSelectComboExit = false;
                }
                drdGetDetails.Close();
                drdGetDetails.Dispose();
                //
                cmbFinancialYear.Text = FAYear;
                cmbCompany.Text = Company;
                cmbQuarter.Text = Quarter;
                cmbFormNo.Text = TdsMan.GetFormNoIT2025(FormNo) + " (" + FormNo + ")"; ;
            }
            //--
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //--
            cmbLoadGrid_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region cmbLoadGrid_SelectedIndexChanged
        private void cmbLoadGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFinancialYear.SelectedIndex <= 0)
            {
                LoadChallanGrid();
                ClearFields();
                return;
            }
            if (cmbCompany.SelectedIndex <= 0)
            {
                LoadChallanGrid();
                ClearFields();
                return;
            }
            else
            {
                strSQL = "SELECT MST_CATEGORY.CATEGORY_CODE FROM MST_COMPANY, MST_CATEGORY WHERE MST_COMPANY.D_CATEGORY_ID = CATEGORY_ID AND MST_COMPANY.COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
                lblCategoryCode.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                
            }
            //
            if (cmbQuarter.Text == "")
            {
                LoadChallanGrid();
                ClearFields();
                return;
            }
            //
            if (cmbFormNo.Text == "")
            {
                LoadChallanGrid();
                ClearFields();
                return;
            }
            //-----------------------------------------------
            ClearFields();
            //-----------------------------------------------
            ComboBoxItem selectedItem = (ComboBoxItem)cmbFormNo.SelectedItem;
            string strFormNo = selectedItem.Value.ToString();
            //
            lngBasicInfoID = TdsMan.T_ReturnBasicInfoId(dmlService.J_pCommand, Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)),
                                                        cmbQuarter.Text,
                                                        Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex)),
                                                        strFormNo);
            //
            //-- 2018/12/13
            strSQL = "SELECT AIN_NO FROM MST_COMPANY WHERE COMPANY_ID = " + Convert.ToInt32(Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex));
            lblAIN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            //if (cmbFormNo.Text == T_FormNo.F24Q)
            //    strEmpDed = "Employee";
            //else
            //    strEmpDed = "Deductee";
            //--
            intChallanId = 1;
            intNameEntered = 2;
            intNameVerified = 3;
            intStatusId = 9;
            intVerifyId = 5;
            intStatus = 10;
            //
            blRegular = false;
            //
            if (lngBasicInfoID == 0)
            {
                LoadChallanGrid();
                return;
            }
            //--
            //--
            strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE BOOK_ENTRY > 0 AND BASIC_INFO_ID = " + lngBasicInfoID;
            if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
            {
                chkValidationType.Visible = true;
                //--
                strSQL = "SELECT COUNT(*) FROM TRN_CHALLAN WHERE BOOK_ENTRY = 0 AND BASIC_INFO_ID = " + lngBasicInfoID;
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                {
                    chkValidationType.Checked = false;
                    chkValidationType.Enabled  = true;
                }
                else
                {
                    chkValidationType.Checked = true;
                    chkValidationType.Enabled = false;
                }
            }
            else
            {
                chkValidationType.Checked = false;
                chkValidationType.Visible = false;
            }
            //--
            LoadChallanGrid(lngBasicInfoID);
            //
            if (cmbFinancialYear.SelectedIndex > 0 &&
                cmbQuarter.SelectedIndex > 0 &&
                cmbFormNo.SelectedIndex > 0 &&
                cmbCompany.SelectedIndex > 0)
                LOAD_IT_BSR_CODE();
            //--
        }
        #endregion

        #region btnXit_Click
        private void btnXit_Click(object sender, EventArgs e)
        {
            //bgwPANVerification.CancelAsync();
            //bgwPANVerification.Dispose();
            //
            GC.Collect();
            //
            //blnVerificationComplete = false;
            //--
            this.Dispose();
            this.Close();
        }
        #endregion

        #region btnVerification_Click
        private void btnVerification_Click(object sender, EventArgs e)
        {
            if (dgvChallanDetails.RowCount <= 0) { return; }
            pBar.Value = 0;

            if (btnVerification.Text == strbtnVerification)
            {
                //if (ValidateFields() == false) return;
                ////--
                //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
                //{
                //    if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
                //    {
                //        cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
                //        return;
                //    }
                //}
                ////-- FOR FROM DATE - TO DATE = GREATER THAN 24 MONTHS
                //System.TimeSpan diff = Convert.ToDateTime(strChallanToDate).Subtract(Convert.ToDateTime(strChallanFromDate));
                //double dblTimeSpanMonth = (diff.TotalDays/365) * 12;
                //double dblMaxMonthPermitted = 24;
                //if(dblTimeSpanMonth > dblMaxMonthPermitted)
                //{
                //    if(blnFromToDateCrossMaxMonthPermitted == false)
                //    {
                //        cmnService.J_UserMessage("The difference between the least and greatest Challan Date is more than " + dblMaxMonthPermitted + " months..." +
                //            "\nwhich is not permitted by the Department.\nSo the From and To date needs to be entered based on which the verification will takes place.");
                //        grpFromToDate.Visible = true;
                //        //mskFromDate.Select();
                //        mskFromDate.Text = strChallanFromDate;
                //        mskToDate.Text = Convert.ToString(Convert.ToDateTime(strChallanFromDate).AddMonths(24));
                //        blnFromToDateCrossMaxMonthPermitted = true;
                //        return;
                //    }                    
                //}
                //--
                #region COMMENT
                //if(blnFromToDateCrossMaxMonthPermitted == true)
                //{
                //    grpFromToDate.Visible = true;
                //    mskFromDate.Select();
                //    //--
                //    //-- FROM DATE
                //    if (dtService.J_IsBlankDateCheck(ref mskFromDate, J_ShowMessage.NO) == true)
                //    {
                //        cmnService.J_UserMessage("Cannot be Blank...", MessageBoxIcon.Exclamation);
                //        mskFromDate.Select();
                //        return;
                //    }
                //    if (dtService.J_IsDateValid(mskFromDate) == false)
                //    {
                //        cmnService.J_UserMessage("Incorrect Format of the Date...", MessageBoxIcon.Exclamation);
                //        mskFromDate.Select();
                //        return;
                //    }
                //    //-- TO DATE
                //    if (dtService.J_IsBlankDateCheck(ref mskToDate, J_ShowMessage.NO) == true)
                //    {
                //        cmnService.J_UserMessage("Cannot be Blank...", MessageBoxIcon.Exclamation);
                //        mskToDate.Select();
                //        return;
                //    }
                //    if (dtService.J_IsDateValid(mskToDate) == false)
                //    {
                //        cmnService.J_UserMessage("Incorrect Format of the Date...", MessageBoxIcon.Exclamation);
                //        mskToDate.Select();
                //        return ;
                //    }
                //    //-- FROM DATE GREATER THAN TO DATE
                //    if (dtService.J_IsDateGreater(ref mskFromDate, ref mskToDate, J_ShowMessage.NO) == false)
                //    {
                //        cmnService.J_UserMessage("From Date could not be greater than To Date...", MessageBoxIcon.Exclamation);
                //        mskFromDate.Select();
                //        return ;
                //    }
                //    //-- FROM DATE SHOULD BE WITHIN CHALLAN MAX & MIN DATE                    
                //    //-- 
                //    //-- TO DATE SHOULD BE WITHIN CHALLAN MAX & MIN DATE
                //    //-- 
                //    //-- FROM DATE - TO DATE SHOULD BE < 24 MONTHS
                //    System.TimeSpan diffCheck = Convert.ToDateTime(mskToDate.Text).Subtract(Convert.ToDateTime(mskFromDate.Text));
                //    double dblTimeSpanMonthCheck = (diffCheck.TotalDays / 365) * 12;
                //    if (dblTimeSpanMonthCheck > dblMaxMonthPermitted)
                //    {
                //        cmnService.J_UserMessage("The difference between the From and To Date is more than " + dblMaxMonthPermitted + " months..." +
                //            "\nwhich is not permitted by the Department.", MessageBoxIcon.Exclamation);
                //        mskFromDate.Select();
                //        return;
                //    }
                //}
                //System.TimeSpan diff1 = secondDate - firstDate;

                //String diff2 = (secondDate - firstDate).TotalDays.ToString();
                #endregion
                //-- 2021/04/30
                if(chkValidationType.Checked == true)
                {
                    if(lblAIN.Text == "")
                    {
                        cmnService.J_UserMessage("AIN is mandatory.\nPlease enter the AIN in Company master and re-verify.");
                        return;
                    }
                }
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("No internet connectivity found !!", MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    if (blnFromToDateCrossMaxMonthPermitted == true)
                    {
                        mskFromDate.Text = Convert.ToString(Convert.ToDateTime(mskToDate.Text).AddDays(1));
                        //mskToDate.Text = Convert.ToString(Convert.ToDateTime(mskFromDate.Text).AddMonths(24));
                        if (dtService.J_ConvertToIntYYYYMMDD(Convert.ToString(Convert.ToDateTime(mskFromDate.Text).AddMonths(24))) > dtService.J_ConvertToIntYYYYMMDD(System.DateTime.Now.ToString()))
                            mskToDate.Text = System.DateTime.Now.ToString();
                        else
                            mskToDate.Text = Convert.ToString(Convert.ToDateTime(strChallanFromDate).AddMonths(24));
                        blnFromToDateCrossMaxMonthPermitted = true;
                    }
                    else
                    { 
                        //-- FOR FROM DATE - TO DATE = GREATER THAN 24 MONTHS
                        System.TimeSpan diff = Convert.ToDateTime(strChallanToDate).Subtract(Convert.ToDateTime(strChallanFromDate));
                        double dblTimeSpanMonth = (diff.TotalDays / 365) * 12;
                        double dblMaxMonthPermitted = 24;
                        if (dblTimeSpanMonth > dblMaxMonthPermitted)
                        {
                            if (blnFromToDateCrossMaxMonthPermitted == false)
                            {
                                //cmnService.J_UserMessage("The difference between the least and greatest Challan Date is more than " + dblMaxMonthPermitted + " months..." +
                                //    "\nwhich is not permitted by the Department.\nSo the From and To date needs to be entered based on which the verification will takes place.");
                                cmnService.J_UserMessage("Since the Challan dates are for more than two years (24 months), while verifying" +
                                                        "the 'security code' (CAPTCHA) will be asked more than once...");
                                //grpFromToDate.Visible = true;
                                //mskFromDate.Select();
                                mskFromDate.Text = strChallanFromDate;
                                mskToDate.Text = Convert.ToString(Convert.ToDateTime(strChallanFromDate).AddMonths(24));
                                
                                //
                                blnFromToDateCrossMaxMonthPermitted = true;
                                //return;
                            }
                        }
                        else if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                btnVerification.Enabled = false;
                btnXit.Enabled = false;
                //
                grpRegularReturn.Enabled = false;
                //
                if (blnFromToDateCrossMaxMonthPermitted == false)
                {
                    lblUnmatchedChallanNos.Text = "0";
                    btnPrintUnmatchedChallan.Enabled = false;
                    lblUnmatchedNo.Text = "0";
                    btnPrintUnmatched.Enabled = false;
                    lblVerifiedChallanNos.Text = "0";
                    lblNotVerifiedNos.Text = "0";
                    //
                }
                //InitializeCaptcha();
                //if (chkValidationType.Checked)
                //    InitializeCaptcha(enmValidationType.BinView);
                //else
                //    InitializeCaptcha(enmValidationType.CinView);
                //
                //grpLoginDetails.Visible = true;
                this.Cursor = Cursors.WaitCursor;
                //if (chkValidationType.Checked)
                //{
                //    InitializeNSDLCaptcha(enmValidationType.BinView);
                //    txtNSDLCaptchaCode.Text = "";
                //    this.Cursor = Cursors.Default;
                //    grpNSDLLoginDetails.Visible = true;
                //}
                //else
                //{
                    txtTracesCaptcha.Select();
                    //
                    strSQL = "SELECT TAN_NO, LOGIN_ID, USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10)) + "'";
                    IDataReader drdLoadTan = null;
                    drdLoadTan = dmlService.J_ExecSqlReturnReader(strSQL);
                    //--
                    blnShowHelp = false;
                    txtTANNo.Text = cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
                    blnShowHelp = true;
                    //--
                    if (drdLoadTan == null)
                    {
                        blnShowHelp = false;
                        txtTANNo.Text = cmnService.J_ReplaceQuote(cmnService.J_Left(cmnService.J_Right(cmbCompany.Text.Trim(), 11), 10));
                        blnShowHelp = true;
                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        return;
                    }
                    else
                    {
                        while (drdLoadTan.Read())
                        {
                            blnShowHelp = false;
                            txtTANNo.Text = drdLoadTan["TAN_NO"].ToString();
                            blnShowHelp = true;
                            txtUserID.Text = drdLoadTan["LOGIN_ID"].ToString();
                            //txtPassword.Text = TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdLoadTan["USER_PASSWORD"].ToString());
                            txtPassword.Text = drdLoadTan["USER_PASSWORD"].ToString();
                            //
                            txtTracesCaptcha.Select();
                        }
                    }
                    drdLoadTan.Close();
                    drdLoadTan.Dispose();
                    //
                    //InitializeTracesCaptcha();
                    pctTracesCaptcha.Image = Properties.Resources.captcha_loading;
                    //////if (!bgWorkerLoadCaptcha.IsBusy)
                    //////    bgWorkerLoadCaptcha.RunWorkerAsync();
                    InitializeTracesCaptcha();
                    //--
                    txtTracesCaptcha.Text = "";
                    this.Cursor = Cursors.Default;
                    grpTraces.Visible = true;
                    txtTANNo.Select();
                //}
                //
                //
                //txtCaptchaCode.Select();
                //
                //btnVerification.Text = "Stop verifying";
                //btnVerification.ForeColor = Color.Red;
                //btnPrintInvalidPAN.Enabled = false;
                ////
                //bgwChallanVerification.RunWorkerAsync();
                //
            }
            else
            {
                //bgwChallanVerification.CancelAsync();
                //bgwChallanVerification.Dispose();
                bgwTracesChallanVerification.CancelAsync();
                bgwTracesChallanVerification.Dispose();
                //
                GC.Collect();
                this.Cursor = Cursors.Default; ;
                //
                grpRegularReturn.Enabled = true;
                //
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
                blnVerificationComplete = false;
                //
            }
        }
        #endregion

        #endregion

        #region User Defined Functions


        #region LoadChallanGrid

        #region LoadChallanGrid()
        private void LoadChallanGrid()
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                string strBSRCodeGridColumn = "";
                string strChallanGridColumn = "";
                //--
                strChallanFromDate = "";
                strChallanToDate = "";
                //
                if (lblCategoryCode.Text == "A" || lblCategoryCode.Text == "S")
                {
                    strBSRCodeGridColumn = "BSR/24G No.";
                    strChallanGridColumn = "Challan/Trf Vch No.";
                }
                else
                {
                    strBSRCodeGridColumn = "BSR Code";
                    strChallanGridColumn = "Challan No.";
                }
                string strSectionSize = "0", strSectionTF = "";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                {
                    strSectionSize = "0"; strSectionTF = "F";
                }
                else
                    strSectionSize = "100";
                //-----------------------------------------------------------
                string[,] strMatrixChallanDetails = {{"ChallanID", "0", "", "R", "", "F", ""},
                                        {"Sl", "30", "", "", "", "", ""},
                                        {"Section", strSectionSize, "", "", "", strSectionTF, ""},
                                        {strChallanGridColumn, "70", "", "", "", "", ""},
                                        {"Deposit Dt", "70", "d", "", "", "", "T"},
                                        {strBSRCodeGridColumn, "70", "", "", "", "", "T"},
                                        {"Tax", "80", "0.00", "R", "", "", "T"},
                                        {"Deductee Total", "80", "0.00", "R", "", "", "T"},
                                        {"Allocated Value", "80", "0.00", "R", "", "", "T"},
                                        {"Difference", "80", "0.00", "R", "", "", "T"},
                                        {"Status", "150", "", "", "", "", "T"},
                                        {" ", "80", "", "", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                string[,] strLoadChallanGridMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                                  {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

                //-----------------------------------------------------------------
                string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                    {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};

                //-----------------------------------------------------------------
                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID           AS CHALLAN_ID," +
                         "       TRN_CHALLAN.SL_NO               AS SL_NO," +
                         "       MST_SECTION.SECTION_NO          AS SECTION_NO," +
                         "       " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_TRF_NO," +
                         "       TRN_CHALLAN.DEPOSIT_DATE        AS DEPOSIT_DATE," +
                         "       TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                         //"       " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                         "       TRN_CHALLAN.TOT_TAX             AS TOT_TAX," +
                         "       TRN_CHALLAN.CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                         "       TRN_CHALLAN.INTEREST_ALLOCATED + TRN_CHALLAN.OTHERS_ALLOCATED AS ALLOCATED_VALUE," +
                         "      (" + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - TRN_CHALLAN.CTRL_TOT_TAX) as DIFF, " +
                         "      '' AS STATUS," +
                         "      '' AS STATUS_COMMENT " +
                         "FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                         "       ON  TRN_CHALLAN.SECTION_ID = MST_SECTION.SECTION_ID) " +
                         "WHERE  1 = 2 " +
                         "ORDER BY TRN_CHALLAN.CHALLAN_ID ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvChallanDetails, strSQL, strMatrixChallanDetails);
                dgvChallanDetails.ClearSelection();
                //
                grpStatus.Text = " Total Record :";//(s): " + dgvDeductees.Rows.Count + " ";
                //
            }
            catch
            {

            }
        }
        #endregion

        #region LoadChallanGrid(long BasicInfoID)
        private void LoadChallanGrid(long BasicInfoID)
        {
            DataSet dsetGridClone = new DataSet();
            try
            {
                string strBSRCodeGridColumn = "";
                string strChallanGridColumn = "";
                //--
                strChallanFromDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MIN(DEPOSIT_DATE) AS MIN_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                strChallanToDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT MAX(DEPOSIT_DATE) AS MAX_DEPOSIT_DATE FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + lngBasicInfoID));
                //--
                if (lblCategoryCode.Text == "A" || lblCategoryCode.Text == "S")
                {
                    strBSRCodeGridColumn = "BSR/24G No.";
                    strChallanGridColumn = "Challan/Trf Vch No.";
                }
                else
                {
                    strBSRCodeGridColumn = "BSR Code";
                    strChallanGridColumn = "Challan No.";
                }
                string strSectionSize = "0", strSectionTF = "";
                if (Convert.ToInt32(Support.GetItemData(cmbFinancialYear, cmbFinancialYear.SelectedIndex)) >= T_FinancialYearID.F2013_14ID)
                {
                    strSectionSize = "0"; strSectionTF = "F";
                }
                else
                    strSectionSize = "100";
                //-----------------------------------------------------------
                string[,] strMatrixChallanDetails = {{"ChallanID", "0", "", "R", "", "F", ""},
                                        {"Sl", "30", "", "", "", "", ""},
                                        {"Section", strSectionSize, "", "", "", strSectionTF, ""},
                                        {strChallanGridColumn, "70", "", "", "", "", ""},
                                        {"Deposit Dt", "70", "d", "", "", "", "T"},
                                        {strBSRCodeGridColumn, "70", "", "", "", "", "T"},
                                        {"Tax", "80", "0.00", "R", "", "", "T"},
                                        {"Deductee Total", "80", "0.00", "R", "", "", "T"},
                                        {"Allocated Value", "80", "0.00", "R", "", "", "T"},
                                        {"Difference", "80", "0.00", "R", "", "", "T"},
                                        {"Status", "150", "", "", "", "", "T"},
                                        {" ", "80", "", "", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------
                //-----------------------------------------------------------
                string[,] strLoadChallanGridMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TRANSFER_VOUCHER_NO", "F"},
                                                  {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.CHALLAN_NO", "F"}};

                //-----------------------------------------------------------------
                string[,] strLoadChallanTotTaxMatrix = {{"TRN_CHALLAN.CHALLAN_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED", "F"},
                                                    {"TRN_CHALLAN.TRANSFER_VOUCHER_NO = ''", "F", "TRN_CHALLAN.TOT_TAX - TRN_CHALLAN.INTEREST_ALLOCATED - TRN_CHALLAN.OTHERS_ALLOCATED- TRN_CHALLAN.LATE_FEE", "F"}};

                //-----------------------------------------------------------------
                strSQL = "SELECT TRN_CHALLAN.CHALLAN_ID           AS CHALLAN_ID," +
                          "       TRN_CHALLAN.SL_NO               AS SL_NO," +
                          "       MST_SECTION.SECTION_NO          AS SECTION_NO," +
                          "       " + cmnService.J_SQLDBFormat(strLoadChallanGridMatrix, J_SQLColFormat.Case_End) + " AS CHALLAN_TRF_NO," +
                          "       TRN_CHALLAN.DEPOSIT_DATE        AS DEPOSIT_DATE," +
                          "       TRN_CHALLAN.BSR_CODE            AS BSR_CODE," +
                          //"       " + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " AS TOT_TAX," +
                          "       TRN_CHALLAN.TOT_TAX             AS TOT_TAX," +
                          "       TRN_CHALLAN.CTRL_TOT_TAX        AS DEDUCTEE_TOTAL," +
                          "       TRN_CHALLAN.INTEREST_ALLOCATED + TRN_CHALLAN.OTHERS_ALLOCATED AS ALLOCATED_VALUE," +
                          "      (" + cmnService.J_SQLDBFormat(strLoadChallanTotTaxMatrix, J_SQLColFormat.Case_End) + " - TRN_CHALLAN.CTRL_TOT_TAX) as DIFF, " +
                          "      '' AS STATUS," +
                          "      '' AS STATUS_COMMENT " +
                          "FROM  (TRN_CHALLAN LEFT JOIN MST_SECTION " +
                          "       ON  TRN_CHALLAN.SECTION_ID = MST_SECTION.SECTION_ID) " +
                          "WHERE  TRN_CHALLAN.BASIC_INFO_ID  = " + BasicInfoID + " ";// +
                          //"AND    TRN_CHALLAN.BOOK_ENTRY     = 0 " + //-- 2018/05/11
                          //"AND    TRN_CHALLAN.BOOK_ENTRY     = 1 " +
                          //"ORDER BY TRN_CHALLAN.CHALLAN_ID ";
                if (chkValidationType.Checked == true)
                    strSQL = strSQL + "AND    TRN_CHALLAN.BOOK_ENTRY     = 1 ";
                else
                    strSQL = strSQL + "AND    TRN_CHALLAN.BOOK_ENTRY     = 0 ";
                strSQL = strSQL + "ORDER BY TRN_CHALLAN.CHALLAN_ID ";
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(dmlService.J_pCommand, ref dgvChallanDetails, strSQL, strMatrixChallanDetails);
                dgvChallanDetails.ClearSelection();
                //
                if (dgvChallanDetails.Rows.Count == 0)
                    grpStatus.Text = " Total Record :";
                else
                    grpStatus.Text = " Total Record(s): " + dgvChallanDetails.Rows.Count + " ";
                //--
                ////if(chkValidationType)
                //if (dgvChallanDetails.RowCount > 0)
                //{
                //    for (int i = 0; i <= dgvChallanDetails.RowCount - 1; i++)
                //    {
                //        strSQL = "SELECT COUNT(*) FROM MST_IT_BSR_CODES WHERE LEFT(BSR_CODE,3) ='" + Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Substring(0,3) + "'";
                //        if(cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                //        {
                //            dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SlateGray;
                //            dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan deposited through Incometaxindia portal.";
                //        }
                //    }
                //}
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
            lblCategoryCode.Text = "";
            lblUnmatchedChallanNos.Text = "0";
            btnPrintUnmatchedChallan.Enabled = false;
            lblUnmatchedNo.Text = "0";
            btnPrintUnmatched.Enabled = false;
            lblVerifiedChallanNos.Text = "0";
            lblNotVerifiedNos.Text = "0";
            btnVerification.Enabled = true;
            btnVerification.BackColor = Color.Lavender;
            grpButtons.Enabled = true;
            //
            chkValidationType.Visible = false;
            lblAIN.Text = "";
            //
            mskFromDate.Text = "";
            mskToDate.Text = "";
            grpFromToDate.Visible = false;
            blnFromToDateCrossMaxMonthPermitted = false;
            //
            blnShowHelp = true;
        }
        #endregion

        #region bgwChallanVerification_DoWork
        //private void bgwChallanVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Label.CheckForIllegalCrossThreadCalls = false;
        //    if (btnVerification.Text != strbtnVerification)
        //    {

        //        //--
        //        for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
        //        {
        //            //if (i == 23)
        //            //    cmnService.J_UserMessage("aaa");
        //            if (TdsMan.T_CheckInternetConnectivty() == false)
        //            {
        //                dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
        //                dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
        //                dgvChallanDetails.Rows[i].Cells[intStatus].Value = "No internet";
        //                dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Tan;
        //                //
        //                lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
        //            }
        //            else
        //            {
        //                ChallanQuery ChallanQuery = new ChallanQuery();
        //                //
        //                ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
        //                ChallanQuery.FromDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
        //                ChallanQuery.ToDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);

        //                ChallanQuery.ChallanDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
        //                ChallanQuery.ChallanNo = Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value);
        //                ChallanQuery.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);




        //                enmChallanStatus Status = objTracesConnect.ChallanStatusQuery(ChallanQuery);

        //             if (Status == enmChallanStatus.AMOUNT_MATCHED)
        //             {
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SpringGreen;
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Matched";
        //                 //
        //                 lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
        //             }
        //             else if (Status == enmChallanStatus.AMOUNT_NOT_MATCHED)
        //             {
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
        //                 dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Amount not matched";
        //                 dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
        //                 //
        //                 lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);

        //             }
        //             else if (Status == enmChallanStatus.RECORD_NOT_FOUND)
        //             {
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
        //                 dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Record Not Found";
        //                 dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
        //                 //
        //                 lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
        //             }
        //             else
        //             {
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
        //                 dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
        //                 //
        //                 lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
        //             }


        //                //--
        //               /* if (VerifyChallan(cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10),
        //                                  strChallanFromDate,
        //                                  strChallanToDate,
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value),
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value),
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value)) == T_CHALLAN_STATUS.MATCHED)
        //                {
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SpringGreen;
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Matched";
        //                    //
        //                    lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
        //                    //--
        //                }
        //                else if (VerifyChallan(cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10),
        //                                  strChallanFromDate,
        //                                  strChallanToDate,
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value),
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value),
        //                                  Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value)) == T_CHALLAN_STATUS.UNMATCHED)
        //                {
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
        //                    dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Amount not matched";
        //                    dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
        //                    //
        //                    lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
        //                    //--
        //                }
        //                else if (VerifyChallan(cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10),
        //                             strChallanFromDate,
        //                             strChallanToDate,
        //                             Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value),
        //                             Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value),
        //                             Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value)) == T_CHALLAN_STATUS.NOTVERIFIED)
        //                {
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
        //                    dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Challan information wrong";
        //                    dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
        //                    //
        //                    lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
        //                    //--
        //                }
        //                else
        //                {
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
        //                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
        //                    //
        //                    lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
        //                    //--
        //                } */
        //            }
        //            //--
        //            //j = j + 1;
        //            ////--
        //            //if (TDSMAN.Classes.TDSMAN.T_pVersionType == T_VERSION_TYPE.TRIAL_VERSION)
        //            //{
        //            //    //--
        //            //    strSQL = "UPDATE MST_SETUP SET TRIAL_PAN_VERIFY_RECORDS = TRIAL_PAN_VERIFY_RECORDS + 1 ";
        //            //    dmlService.J_ExecSql(strSQL);
        //            //    //--
        //            //    if (cmnService.J_ReturnInt16Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TRIAL_PAN_VERIFY_RECORDS FROM MST_SETUP"))) >= TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial)
        //            //    {
        //            //        cmnService.J_UserMessage("Bulk PAN Verification exhausted for Trial Version ... maximum " + TDSMAN.Classes.TDSMAN.T_MaxPanVerifyTrial.ToString() + " records permitted");
        //            //        return;
        //            //    }
        //            //    //--
        //            //    if (j == 10)
        //            //    {
        //            //        cmnService.J_UserMessage("Only 10 PANs can be verified per return using the Trial Version");
        //            //        blnVerificationComplete = false;
        //            //        break;
        //            //    }
        //            //}
        //            //--
        //            if (bgwChallanVerification.CancellationPending)//checks for cancel request
        //            {
        //                blnVerificationComplete = false;
        //                break;
        //            }
        //            //
        //            //if (i > 18)
        //            //    dgvChallanDetails.FirstDisplayedScrollingRowIndex = dgvChallanDetails.FirstDisplayedScrollingRowIndex + 1;
        //        }
        //        //
        //        blnVerificationComplete = true;

        //    }
        //    //--
        //    if (bgwChallanVerification.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        blnVerificationComplete = false;
        //        return;
        //    }
        //} 
        #endregion
            
        #region bgwChallanVerification_DoWork
        private void bgwChallanVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            //if (btnVerification.Text != strbtnVerification)
            //{
            List<ChallanQuery> listChlnQuery = new List<ChallanQuery>();
            //
            #region COMMENT
            //--
            /*   for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
               {
                   //if (i == 23)
                   //    cmnService.J_UserMessage("aaa");
                   if (TdsMan.T_CheckInternetConnectivty() == false)
                   {
                       dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
                       dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
                       dgvChallanDetails.Rows[i].Cells[intStatus].Value = "No internet";
                       dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Tan;
                       //
                       lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
                   }
                   else
                   {
                       ChallanQuery ChallanQuery = new ChallanQuery();
                       //
                       ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
                       ChallanQuery.FromDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                       ChallanQuery.ToDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);

                       ChallanQuery.ChallanDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                       ChallanQuery.ChallanNo = Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value);
                       ChallanQuery.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);




                       enmChallanStatus Status = objTracesConnect.ChallanStatusQuery(ChallanQuery);

                       if (Status == enmChallanStatus.AMOUNT_MATCHED)
                       {
                           dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SpringGreen;
                           dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Matched";
                           //
                           lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
                       }
                       else if (Status == enmChallanStatus.AMOUNT_NOT_MATCHED)
                       {
                           dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                           dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
                           dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Amount not matched";
                           dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
                           //
                           lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);

                       }
                       else if (Status == enmChallanStatus.RECORD_NOT_FOUND)
                       {
                           dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                           dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Challan Unmatched";
                           dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Record Not Found";
                           dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
                           //
                           lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                       }
                       else
                       {
                           dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
                           dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
                           //
                           lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
                       }


                   }

                   if (bgwChallanVerification.CancellationPending)//checks for cancel request
                   {
                       blnVerificationComplete = false;
                       break;
                   }

               }*/
            #endregion
            //
            for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
            {
                if (blnFromToDateCrossMaxMonthPermitted == true)
                {
                    if (dtService.J_ConvertToIntYYYYMMDD(Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value)) >= dtService.J_ConvertToIntYYYYMMDD(mskFromDate.Text)
                        && dtService.J_ConvertToIntYYYYMMDD(Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value)) <= dtService.J_ConvertToIntYYYYMMDD(mskToDate.Text))
                    {
                        ChallanQuery ChallanQuery = new ChallanQuery();
                        //
                        ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
                        //--
                        if (blnFromToDateCrossMaxMonthPermitted == true)
                        {
                            ChallanQuery.FromDate = mskFromDate.Text;
                            ChallanQuery.ToDate = mskToDate.Text;
                        }
                        //else
                        //{
                        //    ChallanQuery.FromDate = strChallanFromDate; // Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                        //    ChallanQuery.ToDate = strChallanToDate;   // Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                        //}
                        //
                        ChallanQuery.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value);
                        ChallanQuery.ChallanDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                        //ChallanQuery.ChallanNo = Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value);
                        //-- 2018/01/10
                        ChallanQuery.ChallanNo = Convert.ToString(Convert.ToInt64(dgvChallanDetails.Rows[i].Cells[3].Value));
                        //-- 2019/10/14
                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                            ChallanQuery.ChallanAmount = cmnService.J_Mid(Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value), 0, Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                        else
                            ChallanQuery.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);
                        //--
                        listChlnQuery.Add(ChallanQuery);
                        //
                    }
                }
                else
                {
                    ChallanQuery ChallanQuery = new ChallanQuery();
                    //
                    ChallanQuery.TAN = cmbCompany.Text.Substring(cmbCompany.Text.Length - 11, 10);
                    //--
                    //if (blnFromToDateCrossMaxMonthPermitted == true)
                    //{
                    //    ChallanQuery.FromDate = mskFromDate.Text;
                    //    ChallanQuery.ToDate = mskToDate.Text;
                    //}
                    //else
                    //{
                    ChallanQuery.FromDate = strChallanFromDate; // Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                    ChallanQuery.ToDate = strChallanToDate;   // Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                    //}
                    //
                    ChallanQuery.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value);
                    ChallanQuery.ChallanDate = Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value);
                    //ChallanQuery.ChallanNo = Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value);
                    //-- 2018/01/10
                    ChallanQuery.ChallanNo = Convert.ToString(Convert.ToInt64(dgvChallanDetails.Rows[i].Cells[3].Value));
                    //-- 2019/10/14
                    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                        ChallanQuery.ChallanAmount = cmnService.J_Mid(Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value), 0, Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                    else
                    {
                        ChallanQuery.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);
                        //-- 2024/05/08
                        if (ChallanQuery.ChallanAmount == "0.0000")
                            ChallanQuery.ChallanAmount = "0";
                    }
                    //--
                    listChlnQuery.Add(ChallanQuery);
                    //
                }
            }
            //
            if (listChlnQuery.Count == 0)
            {
                e.Cancel = true;
                blnVerificationComplete = false;
                return;
            }
            //-------------------------------------
            List<ChallanQuery> objChallan;
            //
            ComboBoxItem selectedItem = (ComboBoxItem)cmbFormNo.SelectedItem;
            string strFormNo = selectedItem.Value.ToString();
            //
            if (chkValidationType.Checked)
                objChallan = objTracesConnect.BIN_ChallanStatusQueryInBulk(listChlnQuery, strFormNo, lblAIN.Text, txtNSDLCaptchaCode.Text);
            else
                objChallan = objTracesConnect.ChallanStatusQueryInBulk(listChlnQuery, txtNSDLCaptchaCode.Text);
            //---------------------------------------------------
            if (bgwChallanVerification.CancellationPending)
            {
                e.Cancel = true;
                blnVerificationComplete = false;
                return;
            }
            else
            {
                blnVerificationComplete = true;
                e.Result = objChallan;
            }


        }
        #endregion

        #region bgwChallanVerification_RunWorkerCompleted
        private void bgwChallanVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                btnVerification.Text = strbtnVerification;
                btnVerification.ForeColor = Color.Black;
            }
            //--
            pgTimer.Stop();
            pBar.Value = 100;
            //--
            //if(bgwPANVerification.CancellationPending==false)
            if (blnVerificationComplete == true)
            {
                List<ChallanQuery> chlnQuery = (List<ChallanQuery>)e.Result;

                if (chlnQuery != null)
                {
                    //-------- CHECKKING CAPTCHA ERROR MSG                  

                    foreach (ChallanQuery chln in chlnQuery)
                    {
                        if (chln.Message == enmChallanStatus.WRONG_CAPTCHA)
                        {
                            cmnService.J_UserMessage("Invalid Captcha Code");

                            if (chkValidationType.Checked)
                                InitializeNSDLCaptcha(enmValidationType.BinView);
                            else
                                InitializeNSDLCaptcha(enmValidationType.CinView);

                            //InitializeCaptcha();
                            return;
                        }
                        if (chln.Message == enmChallanStatus.SERVER_MAINTENANCE_ERROR)
                        {
                            cmnService.J_UserMessage("TIN-NSDL site seems to be under maintenance as such Challan being verified. Please try again.");

                            // InitializeCaptcha();

                            if (chkValidationType.Checked)
                                InitializeNSDLCaptcha(enmValidationType.BinView);
                            else
                                InitializeNSDLCaptcha(enmValidationType.CinView);

                            return;
                        }
                        //
                        //if (chln.Message == enmChallanStatus.RECORD_NOT_FOUND)
                        //{
                        //    cmnService.J_UserMessage("Record not Found");

                        //    // InitializeCaptcha();

                        //    if (chkValidationType.Checked)
                        //        InitializeCaptcha(enmValidationType.BinView);
                        //    else
                        //        InitializeCaptcha(enmValidationType.CinView);

                        //    return;
                        //}
                        if (chln.Message == enmChallanStatus.PROCESSING_FAILED)
                        {
                            cmnService.J_UserMessage(chln.ErrorMessage);
                            grpRegularReturn.Enabled = false;
                            //-- InitializeCaptcha();
                            if (chkValidationType.Checked)
                                InitializeNSDLCaptcha(enmValidationType.BinView);
                            else
                                InitializeNSDLCaptcha(enmValidationType.CinView);
                            //--
                            return;
                        }
                        //-----------------------------------------------------------------
                        for (int i = 0; i <= dgvChallanDetails.RowCount - 1; i++)
                        {
                            //if (chln.ChallanNo.Trim() == Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value).Trim() &&
                            //    chln.ChallanDate.Trim() == Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value))
                            //if (dgvChallanDetails.Rows[i].Cells[11].Style.BackColor != Color.SlateGray)
                            //{
                            //if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM MST_IT_BSR_CODES WHERE BSR_CODE ='" + Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Substring(0, 3) + "'"))) > 0)
                            //{
                            //    dgvChallanDetails.Rows[i].Cells[11].Style.BackColor = Color.SlateGray;
                            //    dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Non NSDL challan";
                            //}
                            //else
                            //{
                            //-- ANIK 2018/04/11
                            if (chln.ChallanNo.Trim() == Convert.ToString(cmnService.J_ReturnInt32Value(Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value).Trim())) &&
                                chln.ChallanDate.Trim() == Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value))
                            {
                                if (chln.Message == enmChallanStatus.AMOUNT_MATCHED)
                                {
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SpringGreen;
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Matched";
                                    //
                                    lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
                                }
                                else if (chln.Message == enmChallanStatus.AMOUNT_NOT_MATCHED)
                                {
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Unmatched";
                                    dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Amount not matched";
                                    dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
                                    //
                                    lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                                }
                                else if (chln.Message == enmChallanStatus.RECORD_NOT_FOUND)
                                {
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                                    dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Unmatched";
                                    dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Record Not Found";
                                    dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
                                    //
                                    lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                                }
                                else
                                {
                                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM MST_IT_BSR_CODES WHERE BSR_CODE ='" + Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Substring(0, 3) + "'"))) > 0)
                                    {
                                        dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SlateGray;
                                        dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Non NSDL challan";
                                        dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Non NSDL challan";
                                        dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.SlateGray;
                                    }
                                    else
                                    {
                                        dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Tan;
                                        dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Not Verified";
                                        //
                                        lblNotVerifiedNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblNotVerifiedNos.Text) + 1);
                                    }
                                }
                            }
                            //}
                            //}
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= dgvChallanDetails.RowCount - 1; i++)
                    {
                        if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM MST_IT_BSR_CODES WHERE BSR_CODE ='" + Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Substring(0, 3) + "'"))) > 0)
                        {
                            dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.SlateGray;
                            dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Non NSDL challan";
                            dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Non NSDL challan";
                            dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.SlateGray;
                        }
                        else
                        {
                            dgvChallanDetails.Rows[i].Cells[intStatusId].Style.BackColor = Color.Red;
                            dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText = "Unmatched";
                            dgvChallanDetails.Rows[i].Cells[intStatus].Value = "Record Not Found";
                            dgvChallanDetails.Rows[i].Cells[intStatus].Style.ForeColor = Color.Red;
                            //
                        }
                        lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                    }
                }

                if (blnFromToDateCrossMaxMonthPermitted == true)
                {
                    bool blnMessage = false;
                    for (int i = 0; i <= dgvChallanDetails.RowCount - 1; i++)
                    {
                        if (dgvChallanDetails.Rows[i].Cells[intStatusId].ToolTipText == "")
                        {
                            blnMessage = true;
                        }
                    }
                    //
                    if (blnMessage == true)
                    {
                        //cmnService.J_UserMessage("Verification completed within the date range.\nTo verify rest of the challans change the From and To date an Rerun verification.");
                        cmnService.J_UserMessage("Verification completed.\nTo verify rest of the challans re-run 'Start Verifying'.");
                        btnVerification.Enabled = false;
                        btnVerification.BackColor = Color.LightGray;
                    }
                    else
                    {
                        cmnService.J_UserMessage("Verification completed");
                        btnVerification.Enabled = false;
                        grpFromToDate.Visible = false;
                        btnVerification.BackColor = Color.LightGray;
                        blnFromToDateCrossMaxMonthPermitted = false;
                    }
                }
                else
                {
                    cmnService.J_UserMessage("Verification completed");
                    btnVerification.Enabled = false;
                    btnVerification.BackColor = Color.LightGray;
                }
            }
            else
            {
                cmnService.J_UserMessage("Verification stopped", MessageBoxIcon.Exclamation);
            }
            //
            grpNSDLLoginDetails.Visible = false;
            txtNSDLCaptchaCode.Text = "";
            btnVerification.Enabled = true;
            btnXit.Enabled = true;
            btnVerification.Text = strbtnVerification;
            btnVerification.ForeColor = Color.Black;
            blnVerificationComplete = false;
            //
            //--
            //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
            //    btnPrintInvalidPAN.Enabled = true;
            //else
            //    btnPrintInvalidPAN.Enabled = false;
            //--
        }
        #endregion


        #region chkValidationType_CheckedChanged
        private void chkValidationType_CheckedChanged(object sender, EventArgs e)
        {
            lblUnmatchedChallanNos.Text = "0";
            btnPrintUnmatchedChallan.Enabled = false;
            lblUnmatchedNo.Text = "0";
            btnPrintUnmatched.Enabled = false;
            lblVerifiedChallanNos.Text = "0";
            lblNotVerifiedNos.Text = "0";
            LoadChallanGrid(lngBasicInfoID);
            LOAD_IT_BSR_CODE();
        }
        #endregion

        #region VerifyChallan
        private int VerifyChallan(string TAN, string ChallanFromDate, string ChallanToDate, string ChallanDate, string ChallanNo, string ChallanAmount)
        {
            try
            {
                //--
                ChallanQuery ChallanQuery = new ChallanQuery();
                //
                ChallanQuery.TAN = TAN;
                //Commented by arup on 21-07-16
                //ChallanQuery.FromDate = ChallanFromDate;
                //ChallanQuery.ToDate = ChallanToDate;

                ChallanQuery.FromDate = ChallanDate;
                ChallanQuery.ToDate = ChallanDate;

                ChallanQuery.ChallanDate = ChallanDate;
                ChallanQuery.ChallanNo = ChallanNo;
                ChallanQuery.ChallanAmount = ChallanAmount;

                //--
                TracesConnect TracesConnect = new TracesConnect();
                string strStatus = "";
                // strStatus = TracesConnect.ChallanStatusQuery(ChallanQuery);
                //--
                if (strStatus.Trim().ToUpper() == "AMOUNT MATCHED")
                {
                    return T_CHALLAN_STATUS.MATCHED;
                }
                else if (strStatus.Trim().ToUpper() == "AMOUNT NOT MATCHED")
                {
                    return T_CHALLAN_STATUS.UNMATCHED;
                }
                else if (strStatus.Trim().ToUpper() == "ERR")
                {
                    return T_CHALLAN_STATUS.OTHERERR;
                }
                else
                {
                    return T_CHALLAN_STATUS.NOTVERIFIED;
                }
                //--
            }
            catch (Exception err)
            {
                return 0;
            }
        }
        #endregion



        #region InitializeCaptcha
        //private void InitializeCaptcha(enmValidationType enType)
        //{
        //    try
        //    {
        //        //--
        //        if (TdsMan.T_CheckInternetConnectivty() == false)
        //        {
        //            cmnService.J_UserMessage("Internet Connectivity not found");
        //            return;
        //        }
        //        //----------------------------------------------------
        //        objTracesConnect = new TracesConnect();
        //        Stream imgStream = null;
        //        //-----------------------------------------------------
        //        if (enType == enmValidationType.CinView)
        //            imgStream = objTracesConnect.getTINNSDLCaptcha();
        //        else
        //            imgStream = objTracesConnect.getTINNSDLCaptchaForBin();
        //        //-----------------------------------------------------
        //        Image img = Image.FromStream(imgStream);
        //        this.picCaptcha.Image = img;
        //        //-------------------------------------------------------
        //        txtCaptchaCode.Text = "";
        //        txtCaptchaCode.Select();
        //    }
        //    catch (Exception err)
        //    {
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}

        #endregion

        #region InitializeTracesCaptcha
        private void InitializeTracesCaptcha()
        {
            //--
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                pctTracesCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage("Internet Connectivity not found");
                return;
            }
            try
            {
                //----------------------------------------------------
                objTracesConnect = new TracesConnect();
                //Stream imgStream = objTracesConnect.MakeInitialRequest();
                //Image img = Image.FromStream(imgStream);
                //this.pctTracesCaptcha.Image = img;

                var captcha = objTracesConnect.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;
                this.pctTracesCaptcha.Image = captchaImage;
                //-------------------------------------------------------
                txtNSDLCaptchaCode.Text = "";
                txtTANNo.Select();
            }
            catch (Exception err)
            {
                pctTracesCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region InitializeNSDLCaptcha
        private void InitializeNSDLCaptcha(enmValidationType enType)
        {
            try
            {
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }
                //----------------------------------------------------
                objTracesConnect = new TracesConnect();
                Stream imgStream = null;
                //-----------------------------------------------------
                if (enType == enmValidationType.CinView)
                    imgStream = objTracesConnect.getTINNSDLCaptcha();
                else
                    imgStream = objTracesConnect.getTINNSDLCaptchaForBin();
                //-----------------------------------------------------
                Image img = Image.FromStream(imgStream);
                this.picNSDLCaptcha.Image = img;
                //-------------------------------------------------------
                txtNSDLCaptchaCode.Text = "";
                txtNSDLCaptchaCode.Select();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            if (chkValidationType.Checked)
                InitializeNSDLCaptcha(enmValidationType.BinView);
            //else
            //    InitializeCaptcha(enmValidationType.CinView);
        }
        #endregion


        
        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            grpNSDLLoginDetails.Visible = false;
            //
            grpRegularReturn.Enabled = true;
            //
            btnVerification.Enabled = true;
            btnXit.Enabled = true;
        }
        #endregion

        #region btnLogging_Click
        private void btnLogging_Click(object sender, EventArgs e)
        {

            if (txtNSDLCaptchaCode.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Enter Captcha Code");
                txtNSDLCaptchaCode.Select();
                return;
            }
            if (TdsMan.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connectivity not found");
                //BtnExit.Select();
                return;
            }
            //-------------------------------------------
            if (!bgwChallanVerification.IsBusy)
            {
                pBar.Value = 0;
                pgTimer.Start();
                bgwChallanVerification.RunWorkerAsync();

            }
            //
            grpRegularReturn.Enabled = true;
            //
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

        //-- Added By Abhishek Dey On 21/05/2018 --
        #region lnkSearchByTAN_LinkClicked
        private void lnkSearchByTAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTrn.TrnTANSearch Tan = new TrnTANSearch("TrnBulkChallanVerification");
            Tan.ShowDialog();
            //--------------
            if (TDSMAN.Classes.TDSMAN.T_pTAN != "")
            cmbCompany.Text = TDSMAN.Classes.TDSMAN.T_pTAN;
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=e9EZ_i1pOKM&t=52s");
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0010", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion
        //-----------------------------------------


        #region LOAD_IT_BSR_CODE
        private void LOAD_IT_BSR_CODE()
        {
            if (dgvChallanDetails.RowCount > 1)
            {
                for (int i = 0; i <= dgvChallanDetails.RowCount - 1; i++)
                {
                    strSQL = "SELECT COUNT(*) FROM MST_IT_BSR_CODES WHERE BSR_CODE ='" + Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Substring(0, 3) + "'";
                    if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    {
                        //dgvChallanDetails.Rows[i].Cells[11].Style.BackColor = Color.SlateGray;
                        //dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Non NSDL challan";
                    }
                }
            }
        }
        #endregion


        #endregion

        private void TrnBulkChallanVerification_Deactivate(object sender, EventArgs e)
        {
            TDSMAN.Classes.TDSMAN.T_pVerifyChallanId = 0;
        }

        private void TrnBulkChallanVerification_Activated(object sender, EventArgs e)
        {
            blResize = false;
        }

        #region btnCloseTraces_Click
        private void btnCloseTraces_Click(object sender, EventArgs e)
        {
            grpTraces.Visible = false;
            //
            grpRegularReturn.Enabled = true;
            //
            btnVerification.Enabled = true;
            btnXit.Enabled = true;
        }
        #endregion

        #region btnTracesCaptchaRefresh_Click
        private void btnTracesCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeTracesCaptcha();
        }
        #endregion

        #region btnGoTraces_Click
        private void btnGoTraces_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    btnCloseTraces.Select();
                    return;
                }
                //--
                if (!ValidateFields()) return;
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
                this.Cursor = Cursors.WaitCursor;
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTANNo.Text;
                objLogin.CaptchaCode = txtTracesCaptcha.Text;
                objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/08
                //---------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                //-------------------------------------------
                //if (!bgWorker.IsBusy)
                //    bgWorker.RunWorkerAsync(objList);
                //if (!bgwChallanVerification.IsBusy)
                //    bgwChallanVerification.RunWorkerAsync(objList);
                if (!bgwTracesChallanVerification.IsBusy)
                    bgwTracesChallanVerification.RunWorkerAsync(objList);
                //
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
                
            }
        }
        #endregion

        #region ValidateFields
        bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtTANNo.Text))
            {
                cmnService.J_UserMessage("Please enter TAN");
                txtTANNo.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtUserID.Text))
            //{
            //    cmnService.J_UserMessage("Please enter User ID");
            //    txtUserID.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                cmnService.J_UserMessage("Please enter Password");
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTracesCaptcha.Text))
            {
                cmnService.J_UserMessage("Please enter Captcha Code");
                txtNSDLCaptchaCode.Focus();
                return false;
            }

            return true;
        }

        #endregion


        #region btnCaptchaRefresh_MouseMove
        private void btnCaptchaRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.Show("Click to refresh image", btnNSDLCaptchaRefresh);
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
                if (txtTANNo.Text.Trim() == "")
                {
                    lstDeducteeHelp.Visible = false;
                    return;
                }

                if (blnShowHelp == false)
                    return;
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
                    lstDeducteeHelp.Height = 15;
                    lstDeducteeHelp.Visible = true;
                    while (drdShowDeducteeHelp.Read())
                    {
                        //lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) + drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) + drdShowDeducteeHelp["USER_PASSWORD"]));
                        //-- ANIK @ 2015/04/20
                        lstDeducteeHelp.Items.Add(new ListBoxItem(drdShowDeducteeHelp["TAN_NO"].ToString().PadRight(12) +
                                                                  drdShowDeducteeHelp["LOGIN_ID"].ToString().PadRight(15) +
                                                                  TdsMan.HidePasswordText(TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD, drdShowDeducteeHelp["USER_PASSWORD"].ToString()).PadRight(10) +
                                                                  " " + drdShowDeducteeHelp["COMPANY_NAME"].ToString(),
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
            if (txtTANNo.Text.Trim() == "") return;
            //INITIALIZE CAPTCHA CODE
            //InitializeCaptcha();

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
                txtTANNo.Select();
            }
        }
        #endregion

        #region lstDeducteeHelp_Click
        private void lstDeducteeHelp_Click(object sender, EventArgs e)
        {
            string strlstDeducteeHelp = lstDeducteeHelp.Text;

            //txtTANNo.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserID.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            //-- ANIK @ 2015/04/20
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTANNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
            txtNSDLCaptchaCode.Select();
        }

        #endregion

        #region bgwTracesChallanVerification_DoWork
        private void bgwTracesChallanVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList objList = (ArrayList)e.Argument;
            ArrayList objRetval = new ArrayList();
            //-------------------------------------------------------
            enmRequestType enReqType = (enmRequestType)objList[0];
            TracesResponse objResponse;
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
                // LIST OF CHALLAN ENQUIRY CIN - Period Payment 
                case enmRequestType.CINPP:
                    //TracesResponse response = objTracesConnect.CIN_Period_Payment((TracesData)objList[1]);
                    //TracesResponse response = objTracesConnect.CIN_Period_Payment_New((TracesData)objList[1]);
                    TracesResponse response = objTracesConnect.CIN_Period_Payment_API((TracesData)objList[1]); //-- 2026/07/04
                    objRetval.Add(enmRequestType.CINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;
                case enmRequestType.BINPP:
                    //TracesResponse response = objTracesConnect.CIN_Period_Payment((TracesData)objList[1]);
                    //TracesResponse response = objTracesConnect.CIN_Period_Payment_New((TracesData)objList[1]);
                    TracesResponse response1 = objTracesConnect.BIN_Period_Payment_API((TracesData)objList[1]); //-- 2026/07/04
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response1);
                    e.Result = objRetval;
                    break;
                // LIST OF CHALLAN ENQUIRY CIN - CIN/BIN Particulars
                case enmRequestType.CINParticulars:
                    //response = objTracesConnect.CIN_CIN_BINParticulars((TracesData)objList[1]);
                    response = objTracesConnect.CIN_CIN_BINParticulars_New((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.CINParticulars);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;
                //-- LOOPING CHALLAN
                #region COMMENTED
                //case enmRequestType.ConsumptionDetails:
                //    //--
                //    //List<ChallanQuery> listChlnQuery = new List<ChallanQuery>();
                //    ArrayList objList1 = new ArrayList();
                //    TracesData objData = new TracesData();
                //    //---------------------------------
                //    //objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                //    //objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
                //    //objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                //    //objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
                //    //objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
                //    //
                //    bool ExitLoop = false;
                //    for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
                //    {                        
                //        //
                //        ExitLoop = false;
                //        //
                //        objData.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value);
                //        objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value)).ToString("dd-MMM-yyyy");
                //        objData.ChallanSerialNo = String.Format("{0:00000}", Convert.ToDouble(Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value)));
                //        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //            objData.ChallanAmount = cmnService.J_Mid(Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value), 0, Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                //        else
                //            objData.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);
                //        //
                //        objData.ChallanAmount = string.Format("{0:0.00}", Convert.ToDouble(objData.ChallanAmount));
                //        //--
                //        for (int I = j; I <= dgvTracesData.RowCount - 1; I++)
                //        {
                //            ExitLoop = false;
                //            if(objData.BSRCode == Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)
                //                && objData.ChallanSerialNo == Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)
                //                && objData.FromChallanDepositDate == Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //            {
                //                objData.PRN_NO = Convert.ToString(dgvTracesData.Rows[I].Cells[5].Value);
                //                ExitLoop = true;
                //            }
                //            else
                //            {
                //                if (objData.BSRCode != Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value))
                //                {
                //                    //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "BSR code unmatched";
                //                    dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Value = "BSR code unmatched";
                //                }
                //                //
                //                if(objData.ChallanSerialNo != Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value))
                //                {
                //                    //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "Serial No. unmatched";
                //                    dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Value = "Sl No. unmatched";
                //                }
                //                //
                //                if (objData.FromChallanDepositDate != Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //                {
                //                    //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "Date unmatched";
                //                    dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Value = "Date unmatched";
                //                }
                //                //
                //                if (objData.BSRCode != Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)
                //                && objData.ChallanSerialNo != Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)
                //                && objData.FromChallanDepositDate != Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //                {
                //                    dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                    dgvChallanDetails.Rows[i].Cells[11].Value = "Record not found";
                //                }
                //            }
                //            //
                //            if(ExitLoop==true)
                //                break;
                //        }
                //        //--
                //        //
                //        if (ExitLoop == true)
                //        {
                //            objList1.Add(enmRequestType.ConsumptionDetails);
                //            objList1.Add(objData);
                //            //
                //            //if(chkValidationType.Checked==true)
                //            //    response = objTracesConnect.RequestForBIN_Details((List<string>)objList1[1]);
                //            //else
                //                response = objTracesConnect.RequestForConsumptionDetailsCIN((TracesData)objList1[1]);
                //            objRetval.Clear();
                //            objRetval.Add(enmRequestType.ConsumptionDetails);
                //            objRetval.Add(response);
                //            e.Result = objRetval;
                //            //
                //            if (response.Respons.ToString() == enmResponse.Success.ToString())
                //            {
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                //                dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Matched";
                //                dgvChallanDetails.Rows[i].Cells[11].Value = "";
                //                //
                //                //lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
                //            }
                //            else if (response.Respons.ToString() == enmResponse.Failed.ToString())
                //            {
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Amount Unmatched";
                //                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;

                //                //lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                //            }
                //        }
                //        //}
                //    }
                //
                #endregion
                //
                //-- LOOPING CHALLAN
                #region COMMENTED
                //case enmRequestType.ConsumptionDetails:
                //    ArrayList objList1 = new ArrayList();
                //    TracesData objData = new TracesData();
                //    bool ExitLoop = false;
                //    //
                //    for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
                //    {
                //        ExitLoop = false;
                //        objData = new TracesData();

                //        objData.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Trim();

                //        objData.FromChallanDepositDate =
                //            dtService.J_ConvertddMMyyyy(Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value))
                //            .ToString("dd-MMM-yyyy");

                //        objData.ChallanSerialNo =
                //            Convert.ToInt64(Convert.ToDouble(Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value))).ToString();

                //        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //            objData.ChallanAmount = cmnService.J_Mid(
                //                Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value),
                //                0,
                //                Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                //        else
                //            objData.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);

                //        objData.ChallanAmount = string.Format("{0:0.00}", Convert.ToDouble(objData.ChallanAmount));

                //        for (int I = 0; I <= dgvTracesData.RowCount - 1; I++)
                //        {
                //            string tracesBsr =
                //                (Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) +
                //                 Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)).Trim();

                //            string tracesDate = Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value).Trim();

                //            string tracesChallanNo = "";
                //            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)))
                //                tracesChallanNo = Convert.ToInt64(Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)).ToString();

                //            if (objData.BSRCode == tracesBsr
                //                && objData.ChallanSerialNo == tracesChallanNo
                //                && objData.FromChallanDepositDate == tracesDate)
                //            {
                //                objData.PRN_NO = Convert.ToString(dgvTracesData.Rows[I].Cells[5].Value);
                //                ExitLoop = true;
                //                break;
                //            }
                //        }
                //        if (ExitLoop == true)
                //        {
                //            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                //            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Matched";
                //            dgvChallanDetails.Rows[i].Cells[11].Value = "";
                //        }
                //        else
                //        {
                //            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //            dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //            dgvChallanDetails.Rows[i].Cells[11].Value = "Record not found";
                //        }
                //        //e.Result = objRetval;
                //        //if (ExitLoop == true)
                //        //{
                //        //    objList1 = new ArrayList();
                //        //    objList1.Add(enmRequestType.ConsumptionDetails);
                //        //    objList1.Add(objData);

                //        //    response = objTracesConnect.RequestForConsumptionDetailsCIN((TracesData)objList1[1]);

                //        //    objRetval.Clear();
                //        //    objRetval.Add(enmRequestType.ConsumptionDetails);
                //        //    objRetval.Add(response);
                //        //    e.Result = objRetval;

                //        //    if (response.Respons.ToString() == enmResponse.Success.ToString())
                //        //    {
                //        //        dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                //        //        dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Matched";
                //        //        dgvChallanDetails.Rows[i].Cells[11].Value = "";
                //        //    }
                //        //    else if (response.Respons.ToString() == enmResponse.Failed.ToString())
                //        //    {
                //        //        dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //        //        dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Amount Unmatched";
                //        //        dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //        //        dgvChallanDetails.Rows[i].Cells[11].Value = "Amount unmatched";
                //        //    }
                //        //}
                //        //else
                //        //{
                //        //    dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //        //    dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //        //    dgvChallanDetails.Rows[i].Cells[11].Value = "Record not found";
                //        //}
                //    }
                //    TracesResponse finalResponse = new TracesResponse();
                //    finalResponse.Respons = enmResponse.Success;
                //    finalResponse.Message = "Verification completed";

                //    objRetval.Clear();
                //    objRetval.Add(enmRequestType.ConsumptionDetails);
                //    objRetval.Add(finalResponse);

                //    e.Result = objRetval;

                //    break;
                //    //if(objRetval.Count == 0)
                //    //{
                //    //    objRetval.Add(enmRequestType.ConsumptionDetails);
                //    //    objRetval.Add(response);
                //    //}
                //    //-------------------------------------
                //    //break;

                #endregion

                #region COMMENTED
                //case enmRequestType.BINPP:
                //response = objTracesConnect.BIN_Period_Payment((TracesData)objList[1]);
                //objRetval.Add(enmRequestType.BINPP);
                //objRetval.Add(response);
                //e.Result = objRetval;
                //ArrayList objList1 = new ArrayList();
                //TracesData objData = new TracesData();
                ////---------------------------------
                ////objData.BSRCode = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[0].Value) + Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[1].Value);
                ////objData.FromChallanDepositDate = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[2].Value);
                ////objData.ChallanSerialNo = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[3].Value);
                ////objData.ChallanAmount = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[6].Value);
                ////objData.PRN_NO = Convert.ToString(dgvStatementList.Rows[e.RowIndex].Cells[5].Value);
                ////
                //bool ExitLoop = false;
                //for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
                //{
                //    //
                //    ExitLoop = false;
                //    //
                //    objData.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value);
                //    objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value)).ToString("dd-MMM-yyyy");
                //    objData.ChallanSerialNo = Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value);
                //    if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                //        objData.ChallanAmount = cmnService.J_Mid(Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value), 0, Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                //    else
                //        objData.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);
                //    //
                //    objData.ChallanAmount = string.Format("{0:0.00}", Convert.ToDouble(objData.ChallanAmount));
                //    //--
                //    for (int I = j; I <= dgvTracesData.RowCount - 1; I++)
                //    {
                //        ExitLoop = false;
                //        if (objData.BSRCode == Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)
                //            && objData.ChallanSerialNo == Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)
                //            && objData.FromChallanDepositDate == Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //        {
                //            objData.PRN_NO = Convert.ToString(dgvTracesData.Rows[I].Cells[5].Value);
                //            ExitLoop = true;
                //        }
                //        else
                //        {
                //            if (objData.BSRCode != Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value))
                //            {
                //                //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "BSR code unmatched";
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Value = "BSR code unmatched";
                //            }
                //            //
                //            if (objData.ChallanSerialNo != Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value))
                //            {
                //                //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "Serial No. unmatched";
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Value = "Sl No. unmatched";
                //            }
                //            //
                //            if (objData.FromChallanDepositDate != Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //            {
                //                //dgvChallanDetails.Rows[i].Cells[10].ToolTipText = "Date unmatched";
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Value = "Date unmatched";
                //            }
                //            //
                //            if (objData.BSRCode != Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) + Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)
                //            && objData.ChallanSerialNo != Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)
                //            && objData.FromChallanDepositDate != Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value))
                //            {
                //                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                //                dgvChallanDetails.Rows[i].Cells[11].Value = "Record not found";
                //            }
                //        }
                //        //
                //        if (ExitLoop == true)
                //            break;
                //    }
                //    //--
                //    //
                //    if (ExitLoop == true)
                //    {
                //        //objList1.Add(enmRequestType.ConsumptionDetails);
                //        //objList1.Add(objData);
                //        //
                //        //if(chkValidationType.Checked==true)
                //        //    response = objTracesConnect.RequestForBIN_Details((List<string>)objList1[1]);
                //        //else
                //        response = objTracesConnect.RequestForConsumptionDetails((TracesData)objList1[1]);
                //        objRetval.Clear();
                //        objRetval.Add(enmRequestType.ConsumptionDetails);
                //        objRetval.Add(response);
                //        e.Result = objRetval;
                //        //
                //        if (response.Respons.ToString() == enmResponse.Success.ToString())
                //        {
                //            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                //            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Matched";
                //            dgvChallanDetails.Rows[i].Cells[11].Value = "";
                //            //
                //            //lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
                //        }
                //        else if (response.Respons.ToString() == enmResponse.Failed.ToString())
                //        {
                //            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                //            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Amount Unmatched";
                //            dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;

                //            //lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                //        }
                //    }
                //    //}
                //}
                ////
                ////if(objRetval.Count == 0)
                ////{
                ////    objRetval.Add(enmRequestType.Consumption/Details);
                ////    objRetval.Add(response);
                ////}
                ////-------------------------------------

                //break;
                #endregion
                case enmRequestType.ConsumptionDetails:
                    ArrayList objList1 = new ArrayList();
                    TracesData objData = new TracesData();
                    bool ExitLoop = false;
                    bool AmountMismatch = false;

                    for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
                    {
                        ExitLoop = false;
                        AmountMismatch = false;
                        objData = new TracesData();

                        objData.BSRCode = Convert.ToString(dgvChallanDetails.Rows[i].Cells[5].Value).Trim();

                        objData.FromChallanDepositDate =
                            dtService.J_ConvertddMMyyyy(
                                Convert.ToString(dgvChallanDetails.Rows[i].Cells[4].Value)
                            ).ToString("dd-MMM-yyyy");

                        objData.ChallanSerialNo =
                            Convert.ToInt64(
                                Convert.ToDouble(
                                    Convert.ToString(dgvChallanDetails.Rows[i].Cells[3].Value)
                                )
                            ).ToString();

                        if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                            objData.ChallanAmount = cmnService.J_Mid(
                                Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value),
                                0,
                                Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value).IndexOf("."));
                        else
                            objData.ChallanAmount = Convert.ToString(dgvChallanDetails.Rows[i].Cells[6].Value);

                        objData.ChallanAmount = string.Format("{0:0.00}", Convert.ToDouble(objData.ChallanAmount));

                        for (int I = 0; I <= dgvTracesData.RowCount - 1; I++)
                        {
                            string tracesBsr =
                                (Convert.ToString(dgvTracesData.Rows[I].Cells[0].Value) +
                                 Convert.ToString(dgvTracesData.Rows[I].Cells[1].Value)).Trim();

                            string tracesDate = Convert.ToString(dgvTracesData.Rows[I].Cells[2].Value).Trim();

                            string tracesChallanNo = "";
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)))
                                tracesChallanNo = Convert.ToInt64(
                                    Convert.ToString(dgvTracesData.Rows[I].Cells[3].Value)
                                ).ToString();

                            string tracesAmount = "0.00";
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvTracesData.Rows[I].Cells[6].Value)))
                                tracesAmount = string.Format(
                                    "{0:0.00}",
                                    Convert.ToDouble(dgvTracesData.Rows[I].Cells[6].Value)
                                );

                            // First match BSR + Challan No + Date
                            if (objData.BSRCode == tracesBsr
                                && objData.ChallanSerialNo == tracesChallanNo
                                && objData.FromChallanDepositDate == tracesDate)
                            {
                                // Then check amount also
                                if (objData.ChallanAmount == tracesAmount)
                                {
                                    objData.PRN_NO = Convert.ToString(dgvTracesData.Rows[I].Cells[5].Value);
                                    ExitLoop = true;
                                    AmountMismatch = false;
                                    break;
                                }
                                else
                                {
                                    AmountMismatch = true;
                                    objData.PRN_NO = Convert.ToString(dgvTracesData.Rows[I].Cells[5].Value);
                                    // do not break here if you want to allow another exact match row,
                                    // but usually one row per challan is enough
                                    break;
                                }
                            }
                        }

                        if (ExitLoop)
                        {
                            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Matched";
                            dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Black;
                            dgvChallanDetails.Rows[i].Cells[11].Value = "";
                        }
                        else if (AmountMismatch)
                        {
                            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Amount Unmatched";
                            dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                            dgvChallanDetails.Rows[i].Cells[11].Value = "Amount unmatched";
                        }
                        else
                        {
                            dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                            dgvChallanDetails.Rows[i].Cells[11].ToolTipText = "Record not found";
                            dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                            dgvChallanDetails.Rows[i].Cells[11].Value = "Record not found";
                        }
                    }

                    TracesResponse finalResponse = new TracesResponse();
                    finalResponse.Respons = enmResponse.Success;
                    finalResponse.Message = "Verification completed";

                    objRetval.Clear();
                    objRetval.Add(enmRequestType.ConsumptionDetails);
                    objRetval.Add(finalResponse);

                    e.Result = objRetval;
                    break;
                case enmRequestType.BINParticulars:
                    response = objTracesConnect.BIN_Particulars((TracesData)objList[1]);
                    objRetval.Add(enmRequestType.BINPP);
                    objRetval.Add(response);
                    e.Result = objRetval;
                    break;


                ////// VIEW CONSUMPTION DETAILS
                ////case enmRequestType.ConsumptionDetails:

                ////    response = objTracesConnect.RequestForConsumptionDetails((TracesData)objList[1]);
                ////    objRetval.Add(enmRequestType.ConsumptionDetails);
                ////    objRetval.Add(response);
                ////    e.Result = objRetval;
                ////    break;

                // VIEW CONSUMPTION DETAILS
                case enmRequestType.BINPP_Details:

                    response = objTracesConnect.RequestForBIN_Details((List<string>)objList[1]);
                    objRetval.Add(enmRequestType.BINPP_Details);
                    objRetval.Add(response);
                    e.Result = objRetval;
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

        #region bgwTracesChallanVerification_RunWorkerCompleted
        private void bgwTracesChallanVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList objMessage = (ArrayList)e.Result;

                if (objMessage == null || objMessage.Count < 2)
                {
                    cmnService.J_UserMessage("No Records Found");
                    this.Cursor = Cursors.Default;
                    btnCloseTraces_Click(sender, e);
                    return;
                }

                if (e.Result == null) 
                { 
                    cmnService.J_UserMessage("No Records Found"); 
                    this.Cursor = Cursors.Default;
                    btnCloseTraces_Click(sender, e);
                    return; 
                }
                //--
                enmRequestType enmReqType = (enmRequestType)objMessage[0];
                TracesResponse objResponse = (TracesResponse)objMessage[1];
                ArrayList objRetval = new ArrayList();
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
                            //ShowHideLoginDetails(enmRequestType.CINPP);
                            grpTraces.Visible = false;
                            //if (!bgwTracesChallanVerification.IsBusy)
                            //    bgwTracesChallanVerification.RunWorkerAsync(enmRequestType.CINPP);
                            pgTimerChallanVerification.Start();
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            InitializeTracesCaptcha();
                            txtTracesCaptcha.Text = "";
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

                    case enmRequestType.CINPP:
                    case enmRequestType.CINParticulars:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        //-------------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        //--------------------------------------------------------
                        DataTable dTable = (DataTable)objResponse.CustomeTypes;
                        //--
                        PopulateDatagridView(dTable, enmReqType);
                        //
                        pgTimerMatchingRecords.Start();
                        
                        break;

                    case enmRequestType.BINPP:
                    case enmRequestType.BINParticulars:
                        this.pgTimer.Stop();
                        pBar.Value = 100;
                        //-------------------------------------------------------
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            //ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }

                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            this.Cursor = Cursors.Default;
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        //--------------------------------------------------------
                        dTable = (DataTable)objResponse.CustomeTypes;

                        PopulateDatagridView(dTable, enmReqType);
                        break;

                    case enmRequestType.BINPP_Details:
                        //pgTimerGrid.Stop();
                        //dgvStatementList.Rows[intRowIndex].Cells[7].Value = 100;
                        ////-----------------------------------------------------
                        //if (objResponse.Respons == enmResponse.SessionTimeout)
                        //{
                        //    ShowHideLoginDetails(enmRequestType.LogOff);
                        //    return;
                        //}

                        //if (objResponse.Respons == enmResponse.Failed)
                        //{
                        //    cmnService.J_UserMessage(objResponse.Message);

                        //    dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3].Selected = true;
                        //    dgvStatementList.CurrentCell = dgvStatementList.Rows[intRowIndex].Cells[intColumnIndex - 3];
                        //    dgvStatementList.BeginEdit(true);
                        //    return;
                        //}
                        ////-----------------------------------------------------
                        //dTable = (DataTable)objResponse.CustomeTypes;
                        ////------------------------------------------------------
                        //dgvConsumption.Columns.Clear();
                        //dgvConsumption.DataSource = null;
                        //if (dTable.Rows.Count <= 0)
                        //{
                        //    cmnService.J_UserMessage("No Data Available. Please check after 3 working days from the date of filing of the statement at TIN-FC");
                        //    return;
                        //}
                        //dgvConsumption.DataSource = dTable;
                        //dgvConsumption.Columns[0].Width = 110;
                        //dgvConsumption.Columns[1].Width = 110;
                        //dgvConsumption.Columns[2].Width = 60;
                        //dgvConsumption.Columns[3].Width = 90;
                        //dgvConsumption.Columns[6].Width = 150;
                        //dgvConsumption.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //dgvConsumption.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgvConsumption.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //dgvConsumption.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //dgvConsumption.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgvConsumption.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //dgvConsumption.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //dgvConsumption.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgvConsumption.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        break;

                    case enmRequestType.ConsumptionDetails:

                        for (int i = j; i <= dgvChallanDetails.RowCount - 1; i++)
                        {
                            if (dgvChallanDetails.Rows[i].Cells[11].ToolTipText == "Matched")
                            {
                                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.SpringGreen;
                                //
                                lblVerifiedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblVerifiedChallanNos.Text) + 1);
                            }
                            else if (dgvChallanDetails.Rows[i].Cells[10].ToolTipText == "Amount Unmatched")
                            {
                                dgvChallanDetails.Rows[i].Cells[10].Style.BackColor = Color.Red;
                                dgvChallanDetails.Rows[i].Cells[11].Value = "Amount not matched";
                                dgvChallanDetails.Rows[i].Cells[11].Style.ForeColor = Color.Red;
                                //
                                lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                            }
                            else
                            {
                                //dgvChallanDetails.Rows[i].Cells[9].Style.BackColor = Color.Red;
                                //dgvChallanDetails.Rows[i].Cells[9].ToolTipText = "Unmatched";
                                //dgvChallanDetails.Rows[i].Cells[10].Value = "Record Not Found";
                                //dgvChallanDetails.Rows[i].Cells[10].Style.ForeColor = Color.Red;
                                //
                                lblUnmatchedChallanNos.Text = Convert.ToString(cmnService.J_ReturnInt64Value(lblUnmatchedChallanNos.Text) + 1);
                            }
                        }
                        //
                        grpRegularReturn.Enabled = true;
                        //
                        btnVerification.Enabled = true;
                        btnXit.Enabled = true;
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Verification completed");
                        //
                        break;

                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtNSDLCaptchaCode.Text = "";
                        ////grpDownloadList.Visible = false;
                        grpNSDLLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        ////BtnSave.Enabled = true;
                        ////BtnSave.BackColor = Color.Lavender;
                        this.Cursor = Cursors.Default;
                        InitializeTracesCaptcha();
                        txtTracesCaptcha.Text = "";
                        pBar.Value = 0;
                        break;
                }
                //-------------------------------------------------------
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dsRecords, enmRequestType enmRecType)
        {
            // DATA BIND TO GRIDVIEW CONTROL
            dgvTracesData.Columns.Clear();
            dgvTracesData.DataSource = null;
            dgvTracesData.DataSource = dsRecords;
            //---------------------------------------
            //CHECKING IF RECORD EXISTS OR NOT

            if (dsRecords == null)
            {
                cmnService.J_UserMessage("No data available for the specified search criteria");
                pgTimerMatchingRecords.Stop();
                return;
            }
            else
                pgTimerMatchingRecords.Start();
            //---------------------------------------
            // CREATE DOWNLOAD BUTTON & PROGRESSBAR CONTROL COLUMNS 
            //------------------------------------------------------------
            if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
            {
                dgvTracesData.Columns[0].Width = 0;
                dgvTracesData.Columns[0].Visible = false;
                dgvTracesData.Columns[0].ReadOnly = true;
                dgvTracesData.Columns[1].Width = 0;
                dgvTracesData.Columns[1].Visible = false;
                dgvTracesData.Columns[1].ReadOnly = true;
                dgvTracesData.Columns[2].Width = 130;
                dgvTracesData.Columns[2].ReadOnly = true;

                dgvTracesData.Columns[3].Width = 150;
                dgvTracesData.Columns[3].ReadOnly = true;
                dgvTracesData.Columns[4].ReadOnly = true;
                dgvTracesData.Columns[5].ReadOnly = true;
                dgvTracesData.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTracesData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTracesData.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvTracesData.Columns[6].ReadOnly = false;



            }

            if (enmRecType == enmRequestType.BINPP)
            {
                dgvTracesData.Columns[0].Width = 150;
                dgvTracesData.Columns[1].Width = 150;
                //dgvTracesData.Columns[3].Visible = false;
                dgvTracesData.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTracesData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTracesData.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvTracesData.Columns[4].Visible = false;
                dgvTracesData.Columns[5].Visible = false;

                dgvTracesData.Columns[1].ReadOnly = true;
                dgvTracesData.Columns[2].ReadOnly = true;
                dgvTracesData.Columns[3].ReadOnly = false;
                dgvTracesData.Columns[4].ReadOnly = true;
                dgvTracesData.Columns[5].ReadOnly = true;


            }


            DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
            dgvTracesData.Columns.Add(btn);
            btn.HeaderText = "";
            //btn.Text = "View Details";
            btn.Text = "Match Challan Amount";
            btn.Name = "lnkDetails";
            btn.Width = 150;
            //------------------------------------------------------------
            btn.UseColumnTextForLinkValue = true;
            //------------------------------------------------------------
            DataGridViewProgressColumn prg = new DataGridViewProgressColumn();
            dgvTracesData.Columns.Add(prg);
            prg.Name = "";
            prg.ProgressBarColor = Color.LightGreen;
            //------------------------------------------------------------
            // SET FOCUS 
            if (dsRecords.Rows.Count > 0)
            {
                if (enmRecType == enmRequestType.CINPP || enmRecType == enmRequestType.CINParticulars)
                {
                    dgvTracesData.Rows[0].Cells[6].Selected = true;

                    dgvTracesData.CurrentCell = dgvTracesData.Rows[0].Cells[6];
                    dgvTracesData.BeginEdit(true);
                }
                else
                {
                    dgvTracesData.Rows[0].Cells[3].Selected = true;

                    dgvTracesData.CurrentCell = dgvTracesData.Rows[0].Cells[3];
                    dgvTracesData.BeginEdit(true);
                }
            }


            //-- ARUP @ 2014/09/16
            //foreach (DataGridViewRow gridRow in dgvTracesData.Rows)
            //{
            //    if (gridRow.Cells["Challan Status"] != null)
            //    {
            //        DataGridViewCell BookButtonCell = gridRow.Cells["Challan Status"];
            //        //
            //        if (Convert.ToString(BookButtonCell.Value).Trim().ToUpper() == "UNCLAIMED")
            //            gridRow.Cells["lnkDetails"] = new DataGridViewTextBoxCell();
            //    }
            //}
            //--
        }

        #endregion

        #region IsValidCINPeriodPayment
        public bool IsValidCINPeriodPayment(ref TracesData objData)
        {
            if (string.IsNullOrEmpty(objData.FromChallanDepositDate.Trim()) || objData.FromChallanDepositDate == "  /  /")
            {
                cmnService.J_UserMessage("Challan Deposit From Date is mandatory");
                //mskChallanFromDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.FromChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Challan Deposit From Date ");
                    //mskChallanFromDate.Focus();
                    return false;
                }
                else
                {
                    objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(objData.FromChallanDepositDate).ToString("dd-MMM-yyyy");
                }
            }

            if (string.IsNullOrEmpty(objData.ToChallanDepositDate.Trim()) || objData.ToChallanDepositDate == "  /  /")
            {
                cmnService.J_UserMessage("Challan Deposit To Date is mandatory");
                //mskChallanToDate.Focus();
                return false;
            }
            else
            {
                if (!dtService.J_IsDateValid(objData.ToChallanDepositDate.Trim()))
                {
                    cmnService.J_UserMessage("Enter a Valid Challan Deposit From Date ");
                    //mskChallanToDate.Focus();
                    return false;
                }
                else
                {
                    objData.ToChallanDepositDate = dtService.J_ConvertddMMyyyy(objData.ToChallanDepositDate).ToString("dd-MMM-yyyy");
                }
            }

            //-----------------------------------------------------------
            int FromDate = TracesValidation.ConvertUserDate(objData.FromChallanDepositDate);
            int ToDate = TracesValidation.ConvertUserDate(objData.ToChallanDepositDate);
            int intCurrentDate = TracesValidation.CurrentDate();
            //------------------------------------------------------------            
            if (!TracesValidation.IsValidDate(objData.FromChallanDepositDate) || !TracesValidation.IsValidDate(objData.ToChallanDepositDate))
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                //mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > intCurrentDate || ToDate > intCurrentDate)
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                //mskChallanFromDate.Focus();
                return false;
            }

            if (FromDate > ToDate)
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                //mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.ValidateStartDate(objData.FromChallanDepositDate))
            {
                cmnService.J_UserMessage("Invalid Challan Deposit Date");
                //mskChallanFromDate.Focus();
                return false;
            }

            if (!TracesValidation.validateYearRange(objData.FromChallanDepositDate, objData.ToChallanDepositDate))
            {
                cmnService.J_UserMessage("Date range should be within the same financial year.");
                //mskChallanFromDate.Focus();
                return false;
            }


            return true;
        }


        #endregion

        #region pgTimerChallanVerification_Tick
        private void pgTimerChallanVerification_Tick(object sender, EventArgs e)
        {

            dgvTracesData.Columns.Clear();
            dgvTracesData.DataSource = null;

            TracesData objData = new TracesData();
            //-------------------------------------
            //objData.FromChallanDepositDate = strChallanFromDate;
            //objData.ToChallanDepositDate = strChallanToDate;
            objData.FromChallanDepositDate = dtService.J_ConvertddMMyyyy(strChallanFromDate).ToString("dd-MMM-yyyy"); 
            objData.ToChallanDepositDate = dtService.J_ConvertddMMyyyy(strChallanToDate).ToString("dd-MMM-yyyy");  
            objData.ChallanStatus = "A";
            //-- VALIDATION -----------------------
            ////if (!IsValidCINPeriodPayment(ref objData)) return;
            ////------------------------------------------------
            ArrayList objList = new ArrayList();
            //
            if (chkValidationType.Checked == true)
            {
                objList.Add(enmRequestType.BINPP);
                objList.Add(objData);
            }
            else
            {
                objList.Add(enmRequestType.CINPP);
                objList.Add(objData);
            }
            //
            if (!bgwTracesChallanVerification.IsBusy)
                bgwTracesChallanVerification.RunWorkerAsync(objList);
            pgTimerChallanVerification.Stop();
        }
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0103", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeTracesCaptcha();
        }

        #region pgTimerMatchingRecords_Tick
        private void pgTimerMatchingRecords_Tick(object sender, EventArgs e)
        {
            ArrayList objList = new ArrayList();
            //TracesData objData = new TracesData();
            if (chkValidationType.Checked == true)
            {
                //objList.Add(enmRequestType.BINPP_Details);
                objList.Add(enmRequestType.ConsumptionDetails);
            }
            else
                objList.Add(enmRequestType.ConsumptionDetails);
            //objList.Add(objData);
            //
            if (!bgwTracesChallanVerification.IsBusy)
                bgwTracesChallanVerification.RunWorkerAsync(objList);
            pgTimerMatchingRecords.Stop();
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