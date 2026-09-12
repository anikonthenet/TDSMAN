#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

using System.Collections;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormBrowser;


#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnRequestConsolidatedFile : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region Auto Generated Custuctor
        public TrnRequestConsolidatedFile()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region User Defined Generated Custuctor
        public TrnRequestConsolidatedFile(string FormType)
        {
            strFormName = FormType;

            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        //###
        public TrnRequestConsolidatedFile(string FormType, string TAN, string UserID, string Password, string FaYear, string Form, string Qtr)
        {
            strFormName = FormType;
            //###
            strTAN = TAN; strUserID = UserID; strPassword = Password; strFaYear = FaYear; strForm = Form; strQtr = Qtr;
            //###
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            ////--
        }
        #endregion

        #region Objects & Variables decleration

        //--
        string strFolderPath = "";
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();

        TracesConnect objConnect = new TracesConnect();
        TracesData objTraceData = new TracesData();
        TracesLogin objLogin = new TracesLogin();
        CorrectionData objData = new CorrectionData();

        string strOutputMessage = string.Empty;
        string strFileReferenceNo = string.Empty;

        //--            
        ToolTip tllTip = new ToolTip();

        bool blnShowHelp = false;

        string strFormName = "";
        string strRequestType = "";

        string strSQL = string.Empty;
        int iCount = 0;

        bool blnDataEnteredbyUser = false;
        ToolTip tllTipVideoDemo = new ToolTip();
        ToolTip tllTipManual = new ToolTip();
        private string CurrentCaptchaId = "";
        //--            
        string strChallanIDQueue = "";
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        string strTAN = string.Empty, strUserID = string.Empty, strPassword = string.Empty, strFaYear = string.Empty, strForm = string.Empty, strQtr = string.Empty;
        //----
        #endregion

        #region struct T_RequestButton
        public struct T_RequestButton
        {
            public const string Request = "Request Now";
            public const string Cancel = "Cancel Request";

            public const string Download = "Download File";

            //public const string RequestAll = "Request (All)";
        }
        #endregion

        #region User Defined Events
        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnRequestConsolidatedFile_Activated
        private void TrnRequestConsolidatedFile_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 20/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
            //
        }
        #endregion
        //-----------------------------------------
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

        #region TrnViewReturnStatusOnline_Load
        private void TrnViewReturnStatusOnline_Load(object sender, EventArgs e)
        {
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //--
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.Expect100Continue = false;
            //--
            lblTitle.Text = strFormName;
            //-- 2018/09/04
            if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
            {
                grpProgress.Visible = false;
                btnCaptcha.Visible = false;
                lblCaptcha.Visible = false;
                txtCaptcha.Visible = false;
                grpTextMessage.Visible = true;
                //
                picCaptcha.Visible = false;
            }
            else
                grpSelection.Visible = true;
            //--
            if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
            {
                chkConsofile.Visible = false;
                strRequestType = "Consolidated Statement";

                chkJustificationReport.Location = new Point(81, 14);
                chkForm16A.Location = new Point(275, 11);
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                ClearControls();
                lnkPost2526.Visible = true;
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                chkForm16A.Visible = false;
                strRequestType = "Form 16A";
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                //--
                lnkRequestPAN.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                ClearControls();
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
            {
                strRequestType = "Form 16";
               // grpSelection.Visible = false;
                chkForm16A.Visible = false;
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                //--
                lnkRequestPAN.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                ClearControls();
            }
            else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
            {
                strRequestType = "TAN - PAN File";
                grpSelection.Visible = false;

                //////grpChallanDeductee.Location = new Point(167, 145);
                //////grpReturnInfo.Location = new Point(167, 20);
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                ClearControls();
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
            {
                strRequestType = "Form 27D";
                chkForm16A.Visible = false;
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                //--
                lnkRequestPAN.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                ClearControls();
            }
            else
            {
                chkJustificationReport.Visible = false;
                strRequestType = "Defaults";

                chkConsofile.Location = new Point(81, 14);
                chkForm16A.Location = new Point(275, 11);                
                //--
                pctVideoDemo.Visible = true;
                pctUserManual.Visible = true;
                BtnSave.Text = T_RequestButton.Request;
                //###
                ClearControls();
                if (strTAN != "" && strUserID != "" && strPassword != "" && strFaYear != "" && strForm != "" && strQtr != "" )
                {
                    txtTAN.Text = strTAN;
                    txtUserId.Text = strUserID;
                    txtPassword.Text = strPassword;
                    cmbFAYear.Text = strFaYear;
                    cmbFormNo.Text = strForm;
                    cmbQtr.Text = strQtr;
                }
                cmbFAYear_SelectedIndexChanged(sender, e);
                //###
            }

            //BtnSave.Text = T_RequestButton.Request;

            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            blnDataEnteredbyUser = false;
            //
            //InitializeCaptcha();
            picCaptcha.Image = Properties.Resources.captcha_loading;
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();
            //--
            //--
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //--

        }
        #endregion

        #region txtTokenNo_KeyPress
        private void txtTokenNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericControl_KeyPress(sender, e, 15);
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
                if (txtTAN.Text.Trim() == "")
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

        #region txtTAN_Leave
        private void txtTAN_Leave(object sender, EventArgs e)
        {
            cmbFAYear_SelectedIndexChanged(sender, e);

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

        #region txtChallanNo_TextChanged
        private void txtChallanNo_TextChanged(object sender, EventArgs e)
        {
            blnDataEnteredbyUser = true;
        }
        #endregion

        #region txtSlNo_TextChanged
        private void txtSlNo_TextChanged(object sender, EventArgs e)
        {
            blnDataEnteredbyUser = true;
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
            //string strlstDeducteeHelp = lstDeducteeHelp.Text;
            //txtTAN.Text = cmnService.J_Left(strlstDeducteeHelp, 10);
            //txtUserId.Text = cmnService.J_Mid(strlstDeducteeHelp, strlstDeducteeHelp.IndexOf(' '),
            //                                  strlstDeducteeHelp.LastIndexOf(' ') - strlstDeducteeHelp.IndexOf(' ')).Trim();
            //txtPassword.Text = cmnService.J_Right(strlstDeducteeHelp, strlstDeducteeHelp.Length - strlstDeducteeHelp.LastIndexOf(' ')).Trim();
            //--
            long lngDeducteeId = Convert.ToInt32(Support.GetItemData(lstDeducteeHelp, lstDeducteeHelp.SelectedIndex));
            txtTAN.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT TAN_NO FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtUserId.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT LOGIN_ID FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            txtPassword.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT USER_PASSWORD FROM MST_TAN_ACCOUNT WHERE TAN_ACCOUNT_ID = " + lngDeducteeId));
            //--
            lstDeducteeHelp.Visible = false;
            //--
            cmbFAYear.Select();
        }
        #endregion

        #region txtPANNo_KeyPress

        private void txtPANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtPAN = (TextBox)sender;

            if (Convert.ToInt64(e.KeyChar) == 13)
                SendKeys.Send("{tab}");
            //else
            //    if (TdsMan.gTANNoPANNoValidation(txtPAN, e, T_TANPAN.PAN) == false)
            //        e.Handled = true;
        }

        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
                    return;
                }
                //--
                 if (BtnSave.Text == T_RequestButton.Request)// || BtnSave.Text == T_RequestButton.RequestAll)
                {
                    if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
                    {
                        #region TRACES - BROWSER

                        //PROVIDING LOGIN DETAILS
                        TDSMAN.Classes.TDSMAN.T_TracesUserID = txtUserId.Text.Trim();
                        //TDSMAN.Classes.TDSMAN.T_TracesPassword = txtPassword.Text.Trim();
                        TDSMAN.Classes.TDSMAN.T_TracesPassword = txtPassword.Text;
                        TDSMAN.Classes.TDSMAN.T_TracesTAN = txtTAN.Text.Trim();
                        //objLogin.CaptchaCode = txtCaptcha.Text;

                        //PROVIDING RETURN DETAILS
                        if (cmbFAYear.SelectedIndex > 0)
                            TDSMAN.Classes.TDSMAN.T_TracesFAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));

                        TDSMAN.Classes.TDSMAN.T_TracesPRN_NO = txtTokenNo.Text.Trim();

                        if (cmbQtr.SelectedIndex > 0)
                        {
                            switch (cmbQtr.Text)
                            {
                                case "Q1":
                                    TDSMAN.Classes.TDSMAN.T_TracesQuarter = "3";
                                    break;
                                case "Q2":
                                    TDSMAN.Classes.TDSMAN.T_TracesQuarter = "4";
                                    break;
                                case "Q3":
                                    TDSMAN.Classes.TDSMAN.T_TracesQuarter = "5";
                                    break;
                                case "Q4":
                                    TDSMAN.Classes.TDSMAN.T_TracesQuarter = "6";
                                    break;
                            }
                        }
                        //--
                        if (cmbFormNo.SelectedIndex > 0)
                            TDSMAN.Classes.TDSMAN.T_TracesForm = cmbFormNo.Text;

                        //PROVDING CHALLAN DETAILS
                        TDSMAN.Classes.TDSMAN.T_TracesChallanSerialNo = txtChallanNo.Text.Trim();
                        TDSMAN.Classes.TDSMAN.T_TracesBSRCode = txtBSRCode.Text.Trim();
                        TDSMAN.Classes.TDSMAN.T_TracesTaxDepositedDate = mskChallanDate.Text;
                        TDSMAN.Classes.TDSMAN.T_TracesChallanAmount = txtChallanTax.Text;
                        TDSMAN.Classes.TDSMAN.T_TracesCDRecordNumber = txtSlNo.Text;

                        //PROVIDING DEDUCTEE DETAILS
                        TDSMAN.Classes.TDSMAN.T_TracesPAN1 = txtDeducteePAN1.Text;
                        TDSMAN.Classes.TDSMAN.T_TracesPAN2 = txtDeducteePAN2.Text;
                        TDSMAN.Classes.TDSMAN.T_TracesPAN3 = txtDeducteePAN3.Text;

                        if (txtDeducteePAN1.Text.Trim() != "")
                            TDSMAN.Classes.TDSMAN.T_TracesPAN1Amount = txtTDSDeducted1.Text;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesPAN1Amount = "";


                        if (txtDeducteePAN2.Text.Trim() != "")
                            TDSMAN.Classes.TDSMAN.T_TracesPAN2Amount = txtTDSDeducted2.Text;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesPAN2Amount = "";


                        if (txtDeducteePAN3.Text.Trim() != "")
                            TDSMAN.Classes.TDSMAN.T_TracesPAN3Amount = txtTDSDeducted3.Text;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesPAN3Amount = "";
                        //----------------------------------------------
                        if (chkNilStatement.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesIsNoChallan = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesIsNoChallan = false;
                        //----------------------------------------------
                        if (chkBookAdjustment.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesIsPaymentByBookAdjustment = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesIsPaymentByBookAdjustment = false;
                        //----------------------------------------------
                        if (chkNoValidPAN.Checked)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesPanAmtValueCheck = true;
                            TDSMAN.Classes.TDSMAN.T_TracesPanAmtValue = true;
                        }
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesPanAmtValue = false;

                        if (chkConsofile.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile = false;

                        if (chkJustificationReport.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile = false;

                        if (chkForm16A.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = false;

                        if (chkForm16.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16File = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16File = false;

                        if (chk27D.Checked)
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile = true;
                        else
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile = false;
                        #endregion
                        //
                        if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqConsoFile = true;
                        }
                        else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16AFile = true;
                        }
                        else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm16File = true;
                        }
                        else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqFormTANPANFile = true;
                        }
                        else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqForm27DFile = true;
                        }
                        else
                        {
                            TDSMAN.Classes.TDSMAN.T_TracesReqJustificationFile = true;
                        }
                    }
                    else
                    {
                        if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        //--   
                        #region TRACES - TDSMAN
                        //PROVIDING LOGIN DETAILS
                        objLogin.UserID = txtUserId.Text.Trim();
                        //objLogin.Password = txtPassword.Text.Trim();
                        objLogin.Password = txtPassword.Text;
                        objLogin.TAN = txtTAN.Text.Trim();
                        objLogin.CaptchaCode = txtCaptcha.Text;
                        objLogin.CaptchaId = this.CurrentCaptchaId; //-- 2026/04/09

                        //PROVIDING RETURN DETAILS
                        if (cmbFAYear.SelectedIndex > 0)
                            objTraceData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));

                        objTraceData.PRN_NO = txtTokenNo.Text.Trim();

                        if (cmbQtr.SelectedIndex > 0)
                        {
                            switch (cmbQtr.Text)
                            {
                                case "Q1":
                                    objTraceData.Quarter = "3";
                                    break;
                                case "Q2":
                                    objTraceData.Quarter = "4";
                                    break;
                                case "Q3":
                                    objTraceData.Quarter = "5";
                                    break;
                                case "Q4":
                                    objTraceData.Quarter = "6";
                                    break;
                            }
                        }
                        //--
                        if (cmbFormNo.SelectedIndex > 0)
                            objTraceData.Forms = cmbFormNo.Text;
                        //PROVDING CHALLAN DETAILS
                        objTraceData.ChallanSerialNo = txtChallanNo.Text.Trim();
                        objTraceData.BSRCode = txtBSRCode.Text.Trim();
                        objTraceData.TaxDepositedDate = mskChallanDate.Text;
                        objTraceData.ChallanAmount = txtChallanTax.Text;
                        objTraceData.CDRecordNumber = txtSlNo.Text;
                        //PROVIDING DEDUCTEE DETAILS
                        objTraceData.PAN1 = txtDeducteePAN1.Text;
                        objTraceData.PAN2 = txtDeducteePAN2.Text;
                        objTraceData.PAN3 = txtDeducteePAN3.Text;
                        //
                        if (txtDeducteePAN1.Text.Trim() != "")
                            objTraceData.PAN1Amount = txtTDSDeducted1.Text;
                        else
                            objTraceData.PAN1Amount = "";


                        if (txtDeducteePAN2.Text.Trim() != "")
                            objTraceData.PAN2Amount = txtTDSDeducted2.Text;
                        else
                            objTraceData.PAN2Amount = "";


                        if (txtDeducteePAN3.Text.Trim() != "")
                            objTraceData.PAN3Amount = txtTDSDeducted3.Text;
                        else
                            objTraceData.PAN3Amount = "";
                        //----------------------------------------------
                        if (chkNilStatement.Checked)
                            objTraceData.IsNoChallan = true;
                        else
                            objTraceData.IsNoChallan = false;
                        //----------------------------------------------
                        if (chkBookAdjustment.Checked)
                            objTraceData.IsPaymentByBookAdjustment = true;
                        else
                            objTraceData.IsPaymentByBookAdjustment = false;
                        //----------------------------------------------
                        if (chkNoValidPAN.Checked)
                        {
                            objTraceData.panAmtValueCheck = true;
                            objTraceData.panAmtValue = true;
                        }
                        else
                            objTraceData.panAmtValueCheck = false;

                        if (chkConsofile.Checked)
                            objTraceData.AddlReqConsoFile = true;
                        else
                            objTraceData.AddlReqConsoFile = false;

                        if (chkJustificationReport.Checked)
                            objTraceData.AddlReqJustificationFile = true;
                        else
                            objTraceData.AddlReqJustificationFile = false;

                        if (chkForm16A.Checked)
                            objTraceData.AddlReqForm16AFile = true;
                        else
                            objTraceData.AddlReqForm16AFile = false;

                        if (chkForm16.Checked)
                            objTraceData.AddlReqForm16File = true;
                        else
                            objTraceData.AddlReqForm16File = false;

                        if (chk27D.Checked)
                            objTraceData.AddlReqForm27DFile = true;
                        else
                            objTraceData.AddlReqForm27DFile = false;
                        #endregion

                        objData.TracesData = objTraceData;
                        objData.TracesLogin = objLogin ;
                    }
                    // -----------------------------
                    if (ValidateFields() == false)
                        return;
                    //-------------------------------------------------------
                    #region UPDATE LAST USED USER ID AND PASSWORD
                    //UPDATE LAST USED USER ID AND PASSWORD
                    if (chkRememberMe.Checked == true)
                    {
                        strSQL = "UPDATE TRN_LAST_NSDL " +
                                 "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                                 "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserId.Text) + "', " +
                                 "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";

                        dmlService.J_ExecSql(strSQL);
                    }

                    //NOW ADDING THE USER ID AND PASSWORD IN MASTER 
                    //ADDED BY SHREY KEJRIWAL ON 25/10/2012

                    //CHEKCING IF TRN_NSDL_DOWNLOAD TABLE IS HAVING THE DATA FOR THIS RETURN

                    strSQL = "SELECT COUNT(*) " +
                             "FROM   TRN_NSDL_DOWNLOAD " +
                             "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "' " +
                             "AND   ASST_ID =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND   FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND   QTR     = '" + cmbQtr.Text + "'";

                    iCount = Convert.ToInt16(dmlService.J_ExecSqlReturnScalar(strSQL));

                    if (chkNilStatement.Checked == false && chkNoValidPAN.Checked == false)
                    {
                        if (iCount == 0)
                        {
                            // INSERTING THE RECORD IN TRN_NSDL_DOWNLOAD

                            strSQL = @"INSERT INTO TRN_NSDL_DOWNLOAD (
                                        TAN_NO,
                                        ASST_ID,
                                        FORM_NO,
                                        QTR,
                                        PREVIOUS_RRR_NO,
                                        CHALLAN_NO,
                                        BSR_CODE,
                                        DEPOSIT_DATE,
                                        TOT_TAX,
                                        DEDUCTEE_PAN1,
                                        DEDUCTEE_AMT1,
                                        DEDUCTEE_PAN2,
                                        DEDUCTEE_AMT2,
                                        DEDUCTEE_PAN3,
                                        DEDUCTEE_AMT3,
                                        DEDUCTEE_PAN_INVALID,
                                        BOOK_ENTRY,
                                        NIL_CHALLAN,
                                        CHALLAN_SRL_NO) " +
                                     "VALUES( '" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "', " +
                                     "         " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + ", " +
                                     "        '" + cmbFormNo.Text + "', " +
                                     "        '" + cmbQtr.Text + "', " +
                                     "        '" + cmnService.J_ReplaceQuote(txtTokenNo.Text.Trim()) + "', " +
                                     "        '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "', " +
                                     "        '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "', " +
                                     "         " + (mskChallanDate.Text.Trim() == "/  /" ? "NULL" : cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskChallanDate) + cmnService.J_DateOperator()) + ", " +
                                     "         " + Convert.ToDouble(txtChallanTax.Text.Trim()) + ", " +
                                     "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN1.Text.Trim()) + "', " +
                                     "         " + Convert.ToDouble(txtTDSDeducted1.Text.Trim()) + ", " +
                                     "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN2.Text.Trim()) + "', " +
                                     "         " + Convert.ToDouble(txtTDSDeducted2.Text.Trim()) + ", " +
                                     "        '" + cmnService.J_ReplaceQuote(txtDeducteePAN3.Text.Trim()) + "', " +
                                     "         " + Convert.ToDouble(txtTDSDeducted3.Text.Trim()) + "," +
                                     "         " + (chkNoValidPAN.Checked == true ? "1" : "0") + "," +
                                     "         " + (chkBookAdjustment.Checked == true ? "1" : "0") + "," +
                                     "         " + (chkNilStatement.Checked == true ? "1" : "0") + "," +
                                     "         " + cmnService.J_ReturnInt32Value(txtSlNo.Text) + ")";

                            dmlService.J_ExecSql(strSQL);
                        }
                        else
                        {
                            //UPDATING THE RETURN RECORD IN TRN_NSDL_DOWNLOAD

                            strSQL = "UPDATE TRN_NSDL_DOWNLOAD " +
                                     "SET TAN_NO               = '" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "', " +
                                     "    ASST_ID              =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + ", " +
                                     "    FORM_NO              = '" + cmbFormNo.Text + "', " +
                                     "    QTR                  = '" + cmbQtr.Text + "', " +
                                     "    PREVIOUS_RRR_NO      = '" + cmnService.J_ReplaceQuote(txtTokenNo.Text.Trim()) + "', " +
                                     "    CHALLAN_NO           = '" + cmnService.J_ReplaceQuote(txtChallanNo.Text.Trim()) + "', " +
                                     "    BSR_CODE             = '" + cmnService.J_ReplaceQuote(txtBSRCode.Text.Trim()) + "', " +
                                     "    DEPOSIT_DATE         =  " + (mskChallanDate.Text.Trim() == "/  /" ? "NULL" : cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskChallanDate) + cmnService.J_DateOperator()) + ", " +
                                     "    TOT_TAX              =  " + Convert.ToDouble(txtChallanTax.Text.Trim()) + ", " +
                                     "    DEDUCTEE_PAN1        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN1.Text.Trim()) + "', " +
                                     "    DEDUCTEE_AMT1        =  " + Convert.ToDouble(txtTDSDeducted1.Text.Trim()) + ", " +
                                     "    DEDUCTEE_PAN2        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN2.Text.Trim()) + "', " +
                                     "    DEDUCTEE_AMT2        =  " + Convert.ToDouble(txtTDSDeducted2.Text.Trim()) + ", " +
                                     "    DEDUCTEE_PAN3        = '" + cmnService.J_ReplaceQuote(txtDeducteePAN3.Text.Trim()) + "', " +
                                     "    DEDUCTEE_AMT3        =  " + Convert.ToDouble(txtTDSDeducted3.Text.Trim()) + "," +
                                     "    DEDUCTEE_PAN_INVALID =  " + (chkNoValidPAN.Checked == true ? "1" : "0") + "," +
                                     "    BOOK_ENTRY           =  " + (chkBookAdjustment.Checked == true ? "1" : "0") + "," +
                                     "    NIL_CHALLAN          =  " + (chkNilStatement.Checked == true ? "1" : "0") + "," +
                                     "    CHALLAN_SRL_NO       =  " + cmnService.J_ReturnInt32Value(txtSlNo.Text) + " " + //-- 2015/07/31
                                     "WHERE TAN_NO             = '" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "' " +
                                     "AND   ASST_ID            =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                                     "AND   FORM_NO            = '" + cmbFormNo.Text + "' " +
                                     "AND   QTR                = '" + cmbQtr.Text + "'";

                            dmlService.J_ExecSql(strSQL);

                        }

                    }
                    //--
                    strSQL = "SELECT COUNT(*) FROM MST_TAN_ACCOUNT WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";
                    iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                    if (iCount == 0)
                    {
                        //insering new record in the tan login master
                        strSQL = "INSERT INTO MST_TAN_ACCOUNT(TAN_NO, LOGIN_ID, USER_PASSWORD) " +
                                 "VALUES( '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                                 "        '" + cmnService.J_ReplaceQuote(txtUserId.Text) + "', " +
                                 "        '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "')";

                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "INNER JOIN MST_COMPANY " +
                                 "ON    MST_TAN_ACCOUNT.TAN_NO       = MST_COMPANY.TAN_NO " +
                                 "SET   MST_TAN_ACCOUNT.COMPANY_NAME = MST_COMPANY.COMPANY_NAME " +
                                 "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "INNER JOIN COR_HDR_COMPANY " +
                                 "ON    MST_TAN_ACCOUNT.TAN_NO       = COR_HDR_COMPANY.TAN_NO " +
                                 "SET   MST_TAN_ACCOUNT.COMPANY_NAME = COR_HDR_COMPANY.COMPANY_NAME " +
                                 "WHERE MST_TAN_ACCOUNT.COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                        //--
                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "SET    COMPANY_NAME = '<NOT AVAILABLE>' " +
                                 "WHERE  COMPANY_NAME = ''";
                        dmlService.J_ExecSql(strSQL);
                    }
                    else
                    {
                        //updating the existing record in the master

                        strSQL = "UPDATE MST_TAN_ACCOUNT " +
                                 "SET    TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "', " +
                                 "       LOGIN_ID      = '" + cmnService.J_ReplaceQuote(txtUserId.Text) + "', " +
                                 "       USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' " +
                                 "WHERE  TAN_NO        = '" + cmnService.J_ReplaceQuote(txtTAN.Text) + "' ";

                        dmlService.J_ExecSql(strSQL);
                    }
                    #endregion
                    //-------------------------------------------------------
                    if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
                    {
                        //BtnSave.Enabled = false;
                        //BtnSave.BackColor = Color.LightGray;

                        //BtnExit.Enabled = false;
                        //BtnExit.BackColor = Color.LightGray;
                        //--
                        //
                        //this.Close();
                        if (cmnService.J_UserMessage("This will take you to 'tdscpc.gov.in' webpage, where you have to enter the captcha/verification code for login, for any query regarding the\n contents of the linked page please contact the concerned website.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        //--
                        TrnRequestConsolidatedFileBrowser TrnRequestConsolidatedFileBrowser = new TrnRequestConsolidatedFileBrowser();
                        //TrnRequestConsolidatedFileBrowser.Show();
                        TrnRequestConsolidatedFileBrowser.MdiParent = TrnRequestConsolidatedFile.ActiveForm;
                        TrnRequestConsolidatedFileBrowser.Show();
                    }
                    else
                    {
                        if (objTraceData.IsPaymentByBookAdjustment == false)
                        {
                            bgwWorker.RunWorkerAsync(BtnSave.Text);

                            BtnSave.Enabled = false;
                            BtnSave.BackColor = Color.LightGray;

                            BtnExit.Enabled = false;
                            BtnExit.BackColor = Color.LightGray;

                            lblFileRequest.Text = "Please wait while the " + strRequestType + " is requested...";
                            lblFileRequest.Visible = true;

                            progressTimer.Start();
                        }
                    }
                }
                else if (BtnSave.Text == T_RequestButton.Download)
                {
                    TrnSelectDownloadFilePath frmSelectDownloadPath = new TrnSelectDownloadFilePath();
                    frmSelectDownloadPath.ShowDialog();

                    if (TDSMAN.Classes.TDSMAN.T_pDownloadPath == "")
                        return;

                    lblFileRequest.Text = "Please wait while the " + strRequestType + " is Downloaded...";
                    lblFileRequest.Visible = true;

                    bgwWorker.RunWorkerAsync(BtnSave.Text);
                    this.progressTimer.Start();

                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;

                    BtnExit.Enabled = false;
                    BtnExit.BackColor = Color.LightGray;
                }
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbFAYear_SelectedIndexChanged
        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDataReader reader = null;
            //--
            try
            {
                //
                btnChangeCDDDDetails.Visible = false;
                //
                strChallanIDQueue = "";
                //THIS SECTION IS FOR ADDL. REQUEST
                lblTitle.Text = strFormName;
                if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement || strFormName == T_NSDL_FORM_TYPE.Defaults)
                {
                    chkForm16.Visible = false;
                    chkForm16A.Visible = false;
                    chk27D.Visible = false;
                    chkForm16.Checked = false;
                    chkForm16A.Checked = false;
                    chkJustificationReport.Checked = false;
                    chk27D.Checked = false;
                    if (cmbFormNo.Text == T_FormNo.F24Q)
                    {                        
                         if (cmbQtr.Text == T_Qtr.Q4)
                         {
                             chkForm16.Visible = true;
                             chkForm16.Location = new Point(275, 11);
                         }
                         else
                             chkForm16.Visible = false;
                     }
                     else if (cmbFormNo.Text == T_FormNo.F27EQ)
                     {
                         chk27D.Visible = true;
                         chk27D.Location = new Point(275, 11);
                     }
                     else
                     {
                         chkForm16A.Visible = true;
                     }

                }
                //else if (strFormName == T_NSDL_FORM_TYPE.Form16A)
                //{

                //}
                //else if (strFormName == T_NSDL_FORM_TYPE.Form16)
                //{
                //}
                //else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
                //{

                //}
                //else if (strFormName == T_NSDL_FORM_TYPE.Form27D)
                //{

                //}



                //POPULATING CHALLAN AND DEDUCTEE DETAILS IF FOUND IN THE DB

                if (blnDataEnteredbyUser == false)
                    ClearChallanDeducteeDetails();

                //CHECKING IF THE TAN NO HAS BEEN SELECTED
                if (txtTAN.Text == "" || txtTAN.Text.Length != 10)
                {
                    ClearChallanDeducteeDetails();
                    return;
                }

                //NOW CHECKING IF THE FINANCIAL YEAR, QUARTER AND THE FORM HAS BEEN SELECTED

                if (cmbFAYear.SelectedIndex <= 0 || cmbFAYear.SelectedIndex <= 0 || cmbQtr.SelectedIndex <= 0)
                {
                    ClearChallanDeducteeDetails();
                    return;
                }

                #region TRN_NSDL_DOWNLOAD
                //ADDED BY SHREY KEJRIWAL ON 25/10/2012

                //CHECKING TRN_NSDL_DOWNLOAD TABLE IF IT IS HAVING THE DATA FOR THIS RETURN

                strSQL = "SELECT COUNT(*) " +
                         "FROM   TRN_NSDL_DOWNLOAD " +
                         "WHERE  TAN_NO  ='" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "' " +
                         "AND    ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                         "AND    FORM_NO ='" + cmbFormNo.Text + "' " +
                         "AND    QTR     ='" + cmbQtr.Text + "'";

                iCount = Convert.ToInt16(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (iCount > 0)
                {
                    strSQL = "SELECT PREVIOUS_RRR_NO, " +
                             "       CHALLAN_SRL_NO," +  //-- 2014/12/06
                             "       CHALLAN_NO, " +
                             "       BSR_CODE, " +
                             "       DEPOSIT_DATE, " +
                             "       TOT_TAX, " +
                             "       DEDUCTEE_PAN1, " +
                             "       DEDUCTEE_AMT1, " +
                             "       DEDUCTEE_PAN2, " +
                             "       DEDUCTEE_AMT2, " +
                             "       DEDUCTEE_PAN3, " +
                             "       DEDUCTEE_AMT3 " +
                             "FROM   TRN_NSDL_DOWNLOAD " +
                             "WHERE TAN_NO  = '" + cmnService.J_ReplaceQuote(txtTAN.Text.Trim()) + "' " +
                             "AND   ASST_ID =  " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND   FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND   QTR     = '" + cmbQtr.Text + "'";

                    reader = dmlService.J_ExecSqlReturnReader(strSQL);

                    while (reader.Read())
                    {
                        txtTokenNo.Text = reader["PREVIOUS_RRR_NO"].ToString();
                        txtChallanNo.Text = reader["CHALLAN_NO"].ToString();
                        //
                        if (reader["CHALLAN_SRL_NO"].ToString() != "0")  //-- 2014/12/06
                            txtSlNo.Text = reader["CHALLAN_SRL_NO"].ToString();
                        else
                            txtSlNo.Text = "";
                        //
                        txtBSRCode.Text = reader["BSR_CODE"].ToString();
                        mskChallanDate.Text = reader["DEPOSIT_DATE"].ToString();
                        txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["TOT_TAX"]));
                        txtDeducteePAN1.Text = reader["DEDUCTEE_PAN1"].ToString();
                        txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT1"]));
                        txtDeducteePAN2.Text = reader["DEDUCTEE_PAN2"].ToString();
                        txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT2"]));
                        txtDeducteePAN3.Text = reader["DEDUCTEE_PAN3"].ToString();
                        txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["DEDUCTEE_AMT3"]));
                    }
                    //
                    reader.Close();
                    reader.Dispose();
                    //
                    blnDataEnteredbyUser = false;
                    //
                    btnChangeCDDDDetails.Visible = true;
                    //

                }
                #endregion
                else
                {
                    FetchCDDDDetails(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex), cmbFormNo.Text, cmbQtr.Text, txtTAN.Text);
                }
            }
            catch (Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                return;
            }
        }

        #endregion

        #region bgwWorker_DoWork
        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string strButtonText = Convert.ToString(e.Argument);
            bool blnStatus;
            string strServerMessage;
            ArrayList objResult = new ArrayList();
            TracesResponse response = new TracesResponse();

            if (strButtonText == T_RequestButton.Request)
            {
                //nsdlaccount = new NSDLAccount();

                if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
                {
                    //response = objConnect.RequestForNSDLConsoFile(objLogin, objTraceData);
                    response = objConnect.RequestForNSDLConsoFile_New(objLogin, objTraceData);
                    // blnStatus = nsdlaccount.Request_Consolidated_Statement(nsdlauthentication, consolidatedata, out strServerMessage);
                }
                else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)  
                {
                    if (grpPANLists.Visible == true)
                    {
                        List<string> mylist = new List<string>();

                        //foreach (int indexChecked in chklstPANs.CheckedIndices)
                        //{
                        //    if (chklstPANs.GetItemChecked(indexChecked) == true)
                        //    {
                        //        mylist.Add(cmnService.J_Left(chklstPANs.Items[indexChecked].ToString(), 10));
                        //    }                            
                        //}
                        //-- 2023/08/25
                        foreach (var item in chklstPANs.CheckedItems)
                        {
                            var row = (item as DataRowView).Row;
                            mylist.Add(cmnService.J_Left(row["Item"].ToString(), 10));
                        }
                        //strPANs = cmnService.J_Mid(strPANs, 0, strPANs.Length - 1);
                        if (mylist.Count == 0)
                        {
                            cmnService.J_UserMessage("No PAN selected!!!");
                            return;
                        }

                        //response = objConnect.RequestForDownloadForm16ASearchPAN(objLogin, objTraceData, mylist);
                        response = objConnect.RequestForDownloadForm16ASearchPAN_New(objLogin, objTraceData, mylist);
                    }
                    else
                    {
                        //response = objConnect.RequestForDownloadForm16A(objLogin, objTraceData);
                        response = objConnect.RequestForDownloadForm16A_New(objLogin, objTraceData);
                        //response = objConnect.RequestForDownloadForm16APANWise(objLogin, objTraceData);
                    }
                }
                else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
                {
                    if (grpPANLists.Visible == true)
                    {
                        List<string> mylist = new List<string>();

                        //foreach (int indexChecked in chklstPANs.CheckedIndices)
                        //{
                        //    if (chklstPANs.GetItemChecked(indexChecked) == true)
                        //    {
                        //        mylist.Add(cmnService.J_Left(chklstPANs.Items[indexChecked].ToString(),10) );
                        //    }
                        //}
                        //-- 2023/08/25
                        foreach (var item in chklstPANs.CheckedItems)
                        {
                            var row = (item as DataRowView).Row;
                            mylist.Add(cmnService.J_Left(row["Item"].ToString(), 10));
                        }
                        //strPANs = cmnService.J_Mid(strPANs, 0, strPANs.Length - 1);
                        if (mylist.Count == 0)
                        {
                            cmnService.J_UserMessage("No PAN selected!!!");
                            return;
                        }


                        //response = objConnect.RequestForDownloadForm16SearchPAN(objLogin, objTraceData, mylist);
                        response = objConnect.RequestForDownloadForm16SearchPAN_New(objLogin, objTraceData, mylist);
                    }
                    else
                    {
                        //response = objConnect.RequestForDownloadForm16(objLogin, objTraceData);
                        response = objConnect.RequestForDownloadForm16_New(objLogin, objTraceData);
                    }
                }
                else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
                {
                    //response = objConnect.DownloadConsoTAN_PANFile(objLogin, objTraceData);
                    response = objConnect.DownloadConsoTAN_PANFile_New(objLogin, objTraceData);
                }
                else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
                {
                    if (grpPANLists.Visible == true)
                    {
                        List<string> mylist = new List<string>();

                        //foreach (int indexChecked in chklstPANs.CheckedIndices)
                        //{
                        //    if (chklstPANs.GetItemChecked(indexChecked) == true)
                        //    {
                        //        mylist.Add(cmnService.J_Left(chklstPANs.Items[indexChecked].ToString(), 10));
                        //    }
                        //}
                        //-- 2023/08/25
                        foreach (var item in chklstPANs.CheckedItems)
                        {
                            var row = (item as DataRowView).Row;
                            mylist.Add(cmnService.J_Left(row["Item"].ToString(), 10));
                        }
                        //strPANs = cmnService.J_Mid(strPANs, 0, strPANs.Length - 1);
                        if (mylist.Count == 0)
                        {
                            cmnService.J_UserMessage("No PAN selected!!!");
                            return;
                        }


                        //response = objConnect.RequestForDownloadForm27DSearchPAN(objLogin, objTraceData, mylist);
                        response = objConnect.RequestForDownloadForm27DSearchPAN_New(objLogin, objTraceData, mylist);
                    }
                    else
                    {
                        //response = objConnect.RequestForDownloadForm27D(objLogin, objTraceData);
                        response = objConnect.RequestForDownloadForm27D_New(objLogin, objTraceData);
                    }
                }
                else
                {
                    //response = objConnect.RequestForJustificationReportDownload(objLogin, objTraceData);
                    response = objConnect.RequestForJustificationReportDownload_New(objLogin, objTraceData);
                }

                objResult.Add(strButtonText);
                //objResult.Add(blnStatus);
                objResult.Add(response);
            }
            else if (strButtonText == T_RequestButton.Download)
            {
                //nsdlaccount = new NSDLAccount();

                //string strPar = "";
                //if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
                //    strPar = "Consolidated TDS/ TCS file";
                //else if (strFormName == T_NSDL_FORM_TYPE.Form16A)
                //    strPar = "Form 16A";
                //else if (strFormName == T_NSDL_FORM_TYPE.Defaults)
                //    strPar = "Default Details";

                //blnStatus = nsdlaccount.DownloadOnSearchCriteria(nsdlauthentication, TDSMAN.Classes.TDSMAN.T_pDownloadPath, "ReferenceNo= '" + strFileReferenceNo + "' AND Filerequested = '" + strPar + "'", out strServerMessage);

                //objResult.Add(strButtonText);
                //objResult.Add(blnStatus);
                //objResult.Add(strServerMessage);
            }

            e.Result = objResult;
        }
        #endregion

        #region bgwWorker_RunWorkerCompleted
        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //RETRIEVING THE RESULT
            ArrayList objResult = (ArrayList)e.Result;


            pBar.Value = 100;
            this.progressTimer.Stop();
            this.progressTimer.Interval = 1000;

            BtnSave.Enabled = true;
            BtnSave.BackColor = Color.Lavender;

            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            string strButtonText = objResult[0].ToString();
            //bool blnStatus = (bool)objResult[1];
            //string strServerMessage = objResult[2].ToString();
            TracesResponse objResponse = (TracesResponse)objResult[1];

            if (e.Cancelled == true)
            {
                cmnService.J_UserMessage("Request Cancelled");
                grpPANLists.Visible = false;
                BtnSave.Text = T_RequestButton.Request;

                return;
            }

            if (strButtonText == T_RequestButton.Request)
            {
                if (objResponse.Respons == enmResponse.Success)
                {
                    // strFileReferenceNo = strServerMessage;

                    // lblFileRequest.Text = strRequestType + " has been Requested Successfully";
                    //lblFileRequest.Visible = true;

                    //BtnSave.Text = T_RequestButton.Downlaod;
                    // cmnService.J_UserMessage(strRequestType + " has been Requested successfully. \nClick on " + T_RequestButton.Downlaod + " button to download the file");
                    // RequestStatus status = (RequestStatus)objResponse.CustomeTypes;

                    cmnService.J_UserMessage(objResponse.Message);
                    EnableControls(true);
                    //--
                    if (cmnService.J_UserMessage("Do you want to view the available list of requests made at TRACES ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Dispose();
                        this.Close();
                        //
                        cmnService.J_ShowChildForm(new TrnDownloadFilesTraces(), J_Var.frmMain, "Download Requested Files");
                    }
                    else
                        InitializeCaptcha();


                    //BtnSave.Select();
                    //lblFileRequest.Visible = true;
                }
                else
                {
                    if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
                    {
                        if (objResponse.Message.Replace("\n", "") == "Token Number is not valid for Regular Statement")
                        {
                            List<string> strretval = (List<string>)objResponse.CustomeTypes;
                            string strFYear = strretval[0];
                            string strForm = strretval[1];
                            string strQty = strretval[2];
                            //----------------------------------------
                            cmbFAYear.SelectedValue = strFYear;
                            cmbFormNo.SelectedValue = strForm;
                            cmbQtr.SelectedValue = strQty;
                            //---------------------------------------------
                            if (strQty == "3")
                                strQty = "Q1";
                            else if (strQty == "4")
                                strQty = "Q2";
                            else if (strQty == "5")
                                strQty = "Q3";
                            else if (strQty == "6")
                                strQty = "Q4";
                            objResponse.Message += "Please Enter Token Number of Regular Statement Filed for Financial Year - " + strFYear + ", Quarter - " + strQty + " and Form Type - " + strForm;

                        }
                    }

                    BtnSave.Text = T_RequestButton.Request;
                    cmnService.J_UserMessage(objResponse.Message);
                    BtnSave.Select();
                    InitializeCaptcha();

                    //lblFileRequest.Text = "There was some problem in requesting the " + strRequestType + " file. Please try again later..";
                    //lblFileRequest.Visible = true;

                    //   cmnService.J_UserMessage(strServerMessage);
                    BtnSave.Text = T_RequestButton.Request;

                }

            }
            else if (strButtonText == T_RequestButton.Download)
            {
                //if (blnStatus == true)
                //{
                //    lblFileRequest.Text = "File was successfully downloaded";
                //    lblFileRequest.Visible = true;

                //    EnableControls(true);

                //    if (cmnService.J_UserMessage("File has been successfully downloaded at " + TDSMAN.Classes.TDSMAN.T_pDownloadPath + "\n Do you want to open the Download Folder", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                //        Process.Start(TDSMAN.Classes.TDSMAN.T_pDownloadPath);

                //    ClearControls();
                //    BtnSave.Text = T_RequestButton.Request;

                //}
                //else
                //{
                //    cmnService.J_UserMessage(strServerMessage);
                //    BtnSave.Text = T_RequestButton.Downlaod;
                //}
            }
        }
        #endregion

        #region progressTimer_Tick
        private void progressTimer_Tick(object sender, EventArgs e)
        {
            //to show the progress
            // SLOW DOWN THE INTERVAL
            this.progressTimer.Interval = 1000;
            this.pBar.Step = 5;

            // UPDATE PROGRESS BAR
            if ((this.pBar.Value + this.pBar.Step) > this.pBar.Maximum)
            {
                this.pBar.Value = this.pBar.Minimum;
            }
            else
            {
                this.pBar.Value = this.pBar.Value + this.pBar.Step;
            }
        }
        #endregion

        #region btnCaptcha_Click
        private void btnCaptcha_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }

        #endregion

        #region btnCaptcha_MouseMove
        private void btnCaptcha_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.Show("Click to refresh image", btnCaptcha);
        }
        #endregion

        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region chkBookAdjustment_CheckedChanged
        private void chkBookAdjustment_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkBookAdjustment.Checked == true)
            //{
            //    //-- 2013/08/30
            //    txtChallanNo.Text = "";
            //   txtChallanNo.Text = "";
            //    txtChallanNo.ReadOnly = true;
            //    txtBSRCode.ReadOnly = true;
            //}
            //else
            //{
            //    chkBookAdjustment.Checked = false;
            //    txtChallanNo.ReadOnly = false;
            //    txtBSRCode.ReadOnly = false;
            //}
            if (chkBookAdjustment.Checked == true)
            {
                txtChallanNo.Text = "";
                txtBSRCode.Text = "";
                txtSlNo.Text = "";
                //
                txtChallanNo.Enabled = false;
                txtBSRCode.Enabled = false;
                txtSlNo.Enabled = false;
                //
                picCaptcha.Visible = false;
                btnCaptcha.Visible = false;
                lblCaptcha.Visible = false;
                txtCaptcha.Visible = false;
                //
                grpSelection.Visible = false;
            }
            else
            {
                //
                txtChallanNo.Enabled = true;
                txtBSRCode.Enabled = true;
                txtSlNo.Enabled = true;
                //
                picCaptcha.Visible = true;
                btnCaptcha.Visible = true;
                lblCaptcha.Visible = true;
                txtCaptcha.Visible = true;
                grpSelection.Visible = true;
            }
        }
        #endregion

        #endregion

        #region User Defined Functions


        #region NumericControl_KeyPress
        private void NumericControl_KeyPress(object sender, KeyPressEventArgs e, int MaxLength)
        {
            TextBox txtNumeric = (TextBox)sender;
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N," + MaxLength + ",0", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region CurrencyControl_KeyPress
        private void CurrencyControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtNumeric = (TextBox)sender;

            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtNumeric, "") == false)
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

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region GetTCS_TDS_Statement
        public static string GetTCS_TDS_Statement()
        {

            int intFirstIndex = 0;
            string strServerResponse = "";
            int intLast = 0;
            string strID = "";
            //--------------------------------------
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://onlineservices.tin.nsdl.com/TIN/JSP/tds/linktoUnAuthorizedInput.jsp");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            //--------------------------------------
            strServerResponse = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
            //--------------------------------------
            intFirstIndex = strServerResponse.IndexOf("TIN/UnAuthorizedView.do");

            if (intFirstIndex >= 0)
                intLast = strServerResponse.IndexOf("\"", intFirstIndex);

            if (intLast > 0)
                strID = strServerResponse.Substring(intFirstIndex, intLast - intFirstIndex);

            strID = strID.Substring(strID.LastIndexOf("=") + 1);
            //--------------------------------------

            return strID;

        }
        #endregion

        #region ClearControls
        public void ClearControls()
        {
            txtTAN.Text = "";
            txtUserId.Text = "";
            txtPassword.Text = "";

            // ----------------------------
            // -- POPULATE FORM COMBO BOXES
            // ----------------------------

            //-----------
            //-- FINANCIAL YEAR
            //-----------
            strSQL = " SELECT ASST_ID," +
                "             FA_YEAR " +
                "      FROM   MST_ASSESSMENT " +
                "      WHERE  VISIBILITY_FLAG = 0 " +
                "      AND    FLAG_26_27_ONWARDS = 0 " +
                "      ORDER BY ASST_ID DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxSelectedIndex.YES) == false) return;
            //-----------
            //string[] strQFAYear ={ "2008-09", "2009-10", "2010-11", "2011-12", "2012-13" };
            //dmlService.J_PopulateComboBox(strQFAYear, ref cmbFAYear);

            ////-----------
            ////-- QUARTER
            ////-----------
            //string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            //dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
            ////-----------

            ////-----------
            ////-- FORM NO.
            ////-----------
            //string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            //dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
            //-----------
            //string[] strFORMNO = null;
            //string[] strQUARTER = null;
            //-----------
            if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                //-- QUARTER
                string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
                //-- FORM NO.
                string[] strFormNo ={ T_FormNo.F26Q, T_FormNo.F27Q };
                dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
            {
                //-- QUARTER
                string[] strQtr ={ T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
                cmbQtr.SelectedIndex = 1;
                //-- FORM NO.
                string[] strFormNo ={ T_FormNo.F24Q };
                dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
                cmbFormNo.SelectedIndex = 1;
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
            {
                //-- QUARTER
                string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
                //-- FORM NO.
                string[] strFormNo ={ T_FormNo.F27EQ };
                dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
                cmbFormNo.SelectedIndex = 1;
            }
            else
            {
                //-- QUARTER
                string[] strQtr ={ T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
                dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
                //-- FORM NO.
                string[] strFormNo ={ T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
                dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);
            }
            //--
            txtTokenNo.Text = "";

        }
        #endregion

        #region ValidateFields
        public bool ValidateFields()
        {
            // ------------------------
            // -- TAN
            // ------------------------

            if (txtTAN.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter a valid TAN");
                txtTAN.Select();
                return false;
            }
            if (txtTAN.Text.Length != 10)
            {
                cmnService.J_UserMessage("TAN No. should be of 10 characters");
                txtTAN.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Left(txtTAN.Text, 4), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTAN.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Mid(txtTAN.Text, 4, 5), J_DataType.Numeric) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTAN.Select();
                return false;
            }
            //---------------------------
            if (cmnService.J_CheckAlphabetsNumeric(cmnService.J_Right(txtTAN.Text, 1), J_DataType.Character) == false)
            {
                cmnService.J_UserMessage("Incorrect Format of the TAN No.");
                txtTAN.Select();
                return false;
            }

            // ------------------------
            // -- USER ID
            // ------------------------

            //if (txtUserId.Text.Trim() == "")
            //{
            //    cmnService.J_UserMessage("Please enter the User Id for TAN No. -" + txtTAN.Text.Trim());
            //    txtUserId.Select();
            //    return false;
            //}

            // ------------------------
            // -- PASSWORD
            // ------------------------

            if (txtPassword.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please enter the Password for TAN No. -" + txtTAN.Text.Trim());
                txtPassword.Select();
                return false;
            }

            // ------------------------
            // -- FINANCIAL YEAR
            // ------------------------

            if (cmbFAYear.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Financial year of the return for which the TDS file has to be downloaded");
                cmbFAYear.Select();
                return false;
            }

            // ------------------------
            // -- FORM NO
            // ------------------------

            if (cmbFormNo.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Form No of the return for which the TDS file has to be downloaded");
                cmbFormNo.Select();
                return false;
            }

            // ------------------------
            // -- QUARTER
            // ------------------------

            if (cmbQtr.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Quarter of the return for which the TDS file has to be downloaded");
                cmbQtr.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtTokenNo.Text))
            {
                cmnService.J_UserMessage("Token Number / Provisional Receipt Number (PRN) is mandatory");
                txtTokenNo.Focus();
                return false;
            }
            /*---------------------------------------------
              VALIDATION CAPTCHA CODE
             ---------------------------------------------- */
            try
            {
                if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == false)
                {                    
                    //-----------------------------
                    if (objTraceData.IsPaymentByBookAdjustment)
                    {
                        TrnRequestTRACES_BINBrowser objBrowser = new TrnRequestTRACES_BINBrowser(objData, strFormName);
                        objBrowser.MdiParent = TrnRequestTRACES_BINBrowser.ActiveForm;
                        objBrowser.Show();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtCaptcha.Text))
                        {
                            cmnService.J_UserMessage("Enter Captcha Code");
                            txtCaptcha.Focus();
                            return false;
                        }

                        string strVal = "";
                        Dictionary<string, string> objNameval = null;

                        //RETRIEVE PARAMETER
                        if (objTraceData.IsNoChallan || objTraceData.IsPaymentByBookAdjustment || objTraceData.panAmtValueCheck)
                        {
                            TracesResponse response = null;

                            if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
                                response = objConnect.IsChallanExistsInForm16(objTraceData, objLogin);
                            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
                                response = objConnect.IsChallanExistsInForm16A(objTraceData, objLogin);
                            else if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
                                response = objConnect.IsChallanExistsInConsolidate(objTraceData, objLogin);
                            else if (strFormName == T_NSDL_FORM_TYPE.Defaults)
                                response = objConnect.IsChallanExistsInDefaults(objTraceData, objLogin);
                            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest) //-- 2019/07/19
                                response = objConnect.IsChallanExistsIn27d(objTraceData, objLogin);
                            //---------------------------------------------------
                            //TracesResponse response = objConnect.IsChallanExistsInConsolidate(objTraceData, objLogin);
                            if (response.Respons == enmResponse.Failed)
                            {
                                cmnService.J_UserMessage(response.Message);
                                InitializeCaptcha();
                                txtCaptcha.Focus();
                                return false;
                            }

                            objNameval = (Dictionary<string, string>)response.CustomeTypes;

                        }

                        //CHECKING FOR NIL CHALLAN AMOUNT
                        if (objTraceData.IsNoChallan)
                        {
                            objTraceData.IsNoChallanCheck = true;
                            if (objNameval == null)
                            {
                                return false;
                            }
                            foreach (KeyValuePair<string, string> pair in objNameval)
                            {
                                if (pair.Key == "isChlnNil")
                                {
                                    strVal = pair.Value;
                                    break;
                                }
                            }
                            //---------------------------------------------------------
                            if (!string.IsNullOrEmpty(strVal))
                            {
                                if (strVal.ToUpper() == "TRUE")
                                {
                                    objTraceData.IsNoChallan = false;
                                    cmnService.J_UserMessage("This statement has challan(s) with non-zero challan amount, please enter details of such challan");
                                    InitializeCaptcha();
                                    chkNilStatement.Checked = false;
                                    return false;
                                }
                                else
                                {
                                    objTraceData.BSRCode = "";
                                    objTraceData.TaxDepositedDate = "";
                                    objTraceData.ChallanSerialNo = "";
                                    objTraceData.ChallanAmount = "";

                                    objTraceData.IsPaymentByBookAdjustment = false;
                                    //objTraceData.panAmtValueCheck = false;
                                }
                            }
                        }
                        else
                        {
                            //-- 2013-08-30
                            if (chkBookAdjustment.Checked == false)
                            {
                                if (string.IsNullOrEmpty(objTraceData.ChallanSerialNo))
                                {
                                    cmnService.J_UserMessage("Challan Serial Number / DDO Serial Number is mandatory");
                                    txtChallanNo.Focus();
                                    return false;
                                }
                                if (string.IsNullOrEmpty(objTraceData.BSRCode))
                                {
                                    cmnService.J_UserMessage("Enter BSR Code");
                                    txtBSRCode.Focus();
                                    return false;
                                }
                            }

                            if (string.IsNullOrEmpty(objTraceData.TaxDepositedDate) || objTraceData.TaxDepositedDate.Trim() == "/  /")
                            {
                                cmnService.J_UserMessage("Date on which Tax Deposited is mandatory");
                                mskChallanDate.Focus();
                                return false;
                            }
                            else
                            {
                                objTraceData.TaxDepositedDate = dtService.J_ConvertddMMyyyy(mskChallanDate).ToString("dd-MMM-yyyy");
                            }

                            if (string.IsNullOrEmpty(objTraceData.ChallanAmount))
                            {
                                cmnService.J_UserMessage("Challan Serial Number / DDO Serial Number is mandatory");
                                txtChallanTax.Focus();
                                return false;
                            }
                            else
                            {
                                double dblAmt = Convert.ToDouble(objTraceData.ChallanAmount);

                                if (dblAmt <= 0)
                                {
                                    cmnService.J_UserMessage("Amount should be entered in two decimal places");
                                    txtChallanTax.Focus();
                                    return false;
                                }
                            }
                        }
                        //--------------------------------------------------
                        //VALIDATION FOR BOOK ADJUSTMENT CHECK TRUE
                        //--------------------------------------------------
                        if (objTraceData.IsPaymentByBookAdjustment)
                        {
                            objTraceData.IsPaymentByBookAdjustmentCheck = true;
                            foreach (KeyValuePair<string, string> pair in objNameval)
                            {
                                if (pair.Key == "bkEntryValue")
                                {
                                    strVal = pair.Value;
                                    break;
                                }
                            }
                            //-------------------------------------------------
                            if (!string.IsNullOrEmpty(strVal))
                            {
                                if (strVal.ToUpper() == "FALSE")
                                {
                                    objTraceData.IsPaymentByBookAdjustment = false;
                                    cmnService.J_UserMessage("For this statement, tax has been paid through challan only. Please enter details of such challan");
                                    //
                                    InitializeCaptcha();
                                    chkBookAdjustment.Checked = false;
                                    return false;
                                }
                                else
                                {
                                    objTraceData.BSRCode = "";
                                    objTraceData.ChallanSerialNo = "";
                                    objTraceData.IsNoChallan = false;
                                }
                            }
                        }
                        //--------------------------------------------------
                        //VALIDATION FOR NO PAN  CHECK TRUE
                        //--------------------------------------------------
                        if (objTraceData.panAmtValueCheck)
                        {
                            string dedCount = "";
                            string bkEntryValue = "";
                            //-------------------------------------------
                            foreach (KeyValuePair<string, string> pair in objNameval)
                            {
                                if (pair.Key == "dedCount")
                                    dedCount = pair.Value;

                                if (pair.Key == "bkEntryValue")
                                    bkEntryValue = pair.Value;
                            }
                            //-------------------------------------------
                            if (string.IsNullOrEmpty(dedCount))
                                dedCount = "0";
                            //-------------------------------------------
                            if (Convert.ToInt32(dedCount) > 0)
                            {
                                objTraceData.panAmtValueCheck = false;
                                cmnService.J_UserMessage("This statement has Challan(s) / Transfer Voucher with deductee rows corresponding to it. Please enter details of such Challan / Transfer Voucher and corresponding deductee rows");
                                InitializeCaptcha();
                                chkNoValidPAN.Checked = false;
                                return false;
                            }
                            else
                            {
                                if (bkEntryValue.ToUpper() == "FALSE")
                                    objTraceData.IsPaymentByBookAdjustment = false;
                                //
                                objTraceData.PAN1 = "";
                                objTraceData.PAN1Amount = "";
                                objTraceData.PAN2 = "";
                                objTraceData.PAN2Amount = "";
                                objTraceData.PAN3 = "";
                                objTraceData.PAN3Amount = "";

                                //objTraceData.IsNoChallan = false;
                            }
                        }
                    }
                }
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
            return true;
        }
        #endregion


        #region ClearChallanDeducteeDetails
        public void ClearChallanDeducteeDetails()
        {
            if (blnDataEnteredbyUser == false)
            {
                txtSlNo.Text = "";
                txtChallanNo.Text = "";
                txtBSRCode.Text = "";
                mskChallanDate.Text = "";
                txtChallanTax.Text = "0.00";
                txtChallanID.Text = "";

                txtDeducteePAN1.Text = "";
                txtDeducteePAN2.Text = "";
                txtDeducteePAN3.Text = "";

                txtTDSDeducted1.Text = "0.00";
                txtTDSDeducted2.Text = "0.00";
                txtTDSDeducted3.Text = "0.00";
                //
                chkBookAdjustment.Checked = false;
                chkNilStatement.Checked = false;
                chkNoValidPAN.Checked = false;
                //
                txtTokenNo.Text = "";
            }
        }
        #endregion

        #region ClearChallanDeducteeDetailsCDDDDetails
        public void ClearChallanDeducteeDetailsCDDDDetails()
        {
            if (blnDataEnteredbyUser == false)
            {
                txtSlNo.Text = "";
                txtChallanNo.Text = "";
                txtBSRCode.Text = "";
                mskChallanDate.Text = "";
                txtChallanTax.Text = "0.00";
                txtChallanID.Text = "";

                txtDeducteePAN1.Text = "";
                txtDeducteePAN2.Text = "";
                txtDeducteePAN3.Text = "";

                txtTDSDeducted1.Text = "0.00";
                txtTDSDeducted2.Text = "0.00";
                txtTDSDeducted3.Text = "0.00";
                //
                //chkBookAdjustment.Checked = false;
                //chkNilStatement.Checked = false;
                //chkNoValidPAN.Checked = false;
                ////
                //txtTokenNo.Text = "";
            }
        }
        #endregion

        #region EnableControls
        public void EnableControls(bool EnableStatus)
        {
            if (EnableStatus == true)
            {
                txtTAN.ReadOnly = false;
                txtUserId.ReadOnly = false;
                txtPassword.ReadOnly = false;

                cmbFormNo.Enabled = true;
                cmbQtr.Enabled = true;
                cmbFAYear.Enabled = true;

                txtTokenNo.ReadOnly = false;

                txtChallanNo.ReadOnly = false;
                txtBSRCode.ReadOnly = false;

                mskChallanDate.ReadOnly = false;
                mskChallanDate.BackColor = Color.White;

                txtChallanTax.ReadOnly = false;

                txtDeducteePAN1.ReadOnly = false;
                txtDeducteePAN2.ReadOnly = false;
                txtDeducteePAN3.ReadOnly = false;

                txtTDSDeducted1.ReadOnly = false;
                txtTDSDeducted2.ReadOnly = false;
                txtTDSDeducted3.ReadOnly = false;
            }
            else
            {
                txtTAN.ReadOnly = true;
                txtUserId.ReadOnly = true;
                txtPassword.ReadOnly = true;

                cmbFormNo.Enabled = false;
                cmbQtr.Enabled = false;
                cmbFAYear.Enabled = false;

                txtTokenNo.ReadOnly = true;

                txtChallanNo.ReadOnly = true;
                txtBSRCode.ReadOnly = true;

                mskChallanDate.ReadOnly = true;
                mskChallanDate.BackColor = Color.FromArgb(236, 233, 216);

                txtChallanTax.ReadOnly = true;

                txtDeducteePAN1.ReadOnly = true;
                txtDeducteePAN2.ReadOnly = true;
                txtDeducteePAN3.ReadOnly = true;

                txtTDSDeducted1.ReadOnly = true;
                txtTDSDeducted2.ReadOnly = true;
                txtTDSDeducted3.ReadOnly = true;
            }
        }
        #endregion

        #region InitializeCaptcha
        private void InitializeCaptcha()
        {
            //--
            if (TDSMAN.Classes.TDSMAN.T_BROWSER_LOGIN_TRACES == true)
                return;
            //--
            if (!TdsMan.T_CheckInternetConnectivty()) { picCaptcha.Image = Properties.Resources.captcha_loading_failed; return; }
            //----------------------------------------------------
            try
            {
                //Stream imgStream = objConnect.MakeInitialRequest();
                //Image img = Image.FromStream(imgStream);

                var captcha = objConnect.MakeInitialRequest_NEW();
                this.CurrentCaptchaId = captcha.CaptchaId;
                Image captchaImage = captcha.CaptchaImage;

                this.picCaptcha.Image = captchaImage;// img;
                //-------------------------------------------------------     
                txtCaptcha.Text = "";
            }
            catch (Exception err)
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }

        }

        #endregion

        #region CheckSelection
        private bool CheckSelection()
        {
            if (cmbFAYear.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select Financial Year");
                cmbFAYear.Focus();
                return false;
            }

            if (cmbFormNo.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select Forms Type");
                cmbFormNo.Focus();
                return false;
            }
            if (cmbQtr.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select Quarter");
                cmbQtr.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region SetFormFATypes


        private void SetFormFATypes()
        {
            objTraceData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));
            switch (cmbQtr.Text)
            {
                case "Q1":
                    objTraceData.Quarter = "3";
                    break;
                case "Q2":
                    objTraceData.Quarter = "4";
                    break;
                case "Q3":
                    objTraceData.Quarter = "5";
                    break;
                case "Q4":
                    objTraceData.Quarter = "6";
                    break;
            }

            objTraceData.Forms = cmbFormNo.Text;
        }
        #endregion

        
        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=NDQC4ZkDVbY"); 
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0008", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0008", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");  
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0008", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U"); 
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0008", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=KW48nAh9AB4");                
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("V0001", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
        }
        #endregion
        
        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=NDQC4ZkDVbY");
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0049", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));                
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0138", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0138", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U");  
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0054", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=abi49vWFg0U"); 
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0138", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
            else
            {
                //System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=KW48nAh9AB4");                
                TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
                System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0053", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
            }
        }

        #endregion
        
        #region pctVideoDemo_MouseMove
        private void pctVideoDemo_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipVideoDemo.SetToolTip(pctVideoDemo, pctVideoDemo.Tag.ToString());
        }
        #endregion

        #region pctUserManual_MouseMove
        private void pctUserManual_MouseMove(object sender, MouseEventArgs e)
        {
            tllTipManual.SetToolTip(pctUserManual, pctUserManual.Tag.ToString());
        }

        #endregion

        #region FetchCDDDDetails
        public void FetchCDDDDetails(long FaYearID, string FormNo, string Qtr, string TAN)
        {
            IDataReader rdFetchCDDDDetails = null;
            try
            {
                if (txtChallanID.Text == "")
                {
                    strChallanIDQueue = "";
                    //if (btnChangeCDDDDetails.Visible == true)
                    //{
                    //    cmnService.J_UserMessage("Please click once again...");
                    //strChallanIDQueue = txtChallanID.Text;
                    //return;
                    //}
                    //strSQL = @"SELECT HDR_CHALLAN_ID 
                    //           FROM   COR_HDR_CHALLAN, 
                    //                  COR_HDR_BATCH,
                    //                  COR_HDR_COMPANY 
                    //           WHERE  COR_HDR_CHALLAN.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID 
                    //           AND    COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID 
                    //           AND    COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                    //          "AND    COR_HDR_BATCH.FORM_NO            ='" + FormNo + "' " +
                    //          "AND    COR_HDR_BATCH.QTR                ='" + Qtr + "' " +
                    //          "AND    COR_HDR_COMPANY.TAN_NO           ='" + TAN + "' " +
                    //          "AND    COR_HDR_CHALLAN.SL_NO            = " + txtSlNo.Text + @"
                    //           AND    COR_HDR_CHALLAN.BSR_CODE         ='" + txtBSRCode.Text + @"'
                    //           AND    COR_HDR_CHALLAN.CHALLAN_NO       ='" + txtChallanNo.Text + @"'
                    //           AND    COR_HDR_CHALLAN.TOT_TAX          = " + cmnService.J_ReturnDoubleValue(txtChallanTax.Text);
                    //strChallanIDQueue = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //strChallanIDQueue = txtChallanID.Text;
                    //@@@@@@@@@@@@@@@@@@@@
                    strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + FormNo + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + Qtr + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + TAN + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //DATA NOT FOUND IN CORRECTION RETURN DATA
                        ClearChallanDeducteeDetailsCDDDDetails();

                        //DATA IS AVAILABLE IN CORRECTION RETURN

                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                               "FROM  COR_HDR_CHALLAN, " +
                               "      COR_HDR_DEDUCTEE_DETAILS " +
                               "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                               "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " ";
                        //if (strChallanIDQueue != "")
                        //    strSQL = strSQL + " AND COR_HDR_CHALLAN.HDR_CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                               "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //--
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO, " + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   COR_HDR_CHALLAN " +
                                 "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                                 "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                        if (rdFetchCDDDDetails == null)
                            return;

                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            ////if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            ////{
                            ////    chkBookAdjustment.Checked = true;
                            ////    //-- 2013/08/30
                            ////    //txtChallanNo.Text = "";
                            ////    //txtBSRCode.Text = "";
                            ////    //txtChallanNo.ReadOnly = true;
                            ////    //txtBSRCode.ReadOnly = true;
                            ////}
                            ////else
                            ////{
                            ////    chkBookAdjustment.Checked = false;
                            ////    //txtChallanNo.ReadOnly = false;
                            ////    //txtBSRCode.ReadOnly = false;
                            ////}
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();

                        ////-- NIL CHALLAN STATEMENT
                        if (txtChallanTax.Text == "0.00")
                        {
                            if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID = " + iCount))) == 0)
                                chkNilStatement.Checked = true;
                            else
                                chkNilStatement.Checked = false;
                        }
                        else
                            chkNilStatement.Checked = false;

                        strSQL = "SELECT DISTINCT TOP 3 DEDUCTEE_PAN," +
                                 "                      TOTAL_AMOUNT," +
                                 "                      INVALID_PAN " +
                                 "FROM COR_HDR_DEDUCTEE_DETAILS " +
                                 "WHERE BATCH_HEADER_ID = " + iCount + " " +
                            "AND   HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //" ";

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                        int intInvalidPAN = 0;

                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        if (intInvalidPAN == 3)
                            chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        ////////cmnService.J_UserMessage("Please click once again...");
                        return;
                        //
                    }
                    #region REGULAR RETURN
                    //now checking if the RETURN EXISTS IN THE ORIGINAL RETURN TABLE

                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTAN.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        //strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                        //       "FROM  TRN_CHALLAN, " +
                        //       "      TRN_DEDUCTEE_DETAILS " +
                        //       "WHERE TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        //       "AND   TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                        //       "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                        //       "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        //-- ANIK @ 2015/12/16
                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " ";
                        //if (strChallanIDQueue != "")
                        //    strSQL = strSQL + " AND TRN_CHALLAN.CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                                 "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--                            
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE
                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};
                        //
                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO," + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   TRN_CHALLAN " +
                                 "WHERE  BASIC_INFO_ID = " + iCount + " " +
                                 "AND    CHALLAN_ID    = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //
                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (rdFetchCDDDDetails == null)
                            return;
                        //
                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            ////if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            ////    chkBookAdjustment.Checked = true;
                            ////else
                            ////    chkBookAdjustment.Checked = false;
                        }
                        //
                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        ////-- NIL CHALLAN STATEMENT
                        if (txtChallanTax.Text == "0.00")
                        {
                            if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + iCount))) == 0)
                                chkNilStatement.Checked = true;
                            else
                                chkNilStatement.Checked = false;
                        }
                        else
                            chkNilStatement.Checked = false;

                        //NOW SELECTING ANY 3 DEDUCTEE RECORD FOR THAT RETURN

                        string strDeducteeTableName = "MST_DEDUCTEE";
                        string strDeducteeIdName = "DEDUCTEE_ID";
                        string strDeducteePANName = "DEDUCTEE_PAN";

                        if (cmbFormNo.Text == T_FormNo.F24Q)
                        {
                            strDeducteeTableName = "MST_EMPLOYEE";
                            strDeducteeIdName = "EMPLOYEE_ID";
                            strDeducteePANName = "EMPLOYEE_PAN";
                        }

                        //strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                        //         "             TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                        //         "FROM TRN_DEDUCTEE_DETAILS, " +
                        //         "     " + strDeducteeTableName + " " +
                        //         "WHERE TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                 "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        int intInvalidPAN = 0;
                        //
                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        if (intInvalidPAN == 3)
                            chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        //

                    }
                    #endregion
                    //@@@@@@@@@@@@@@@@@@@@
                    if (btnChangeCDDDDetails.Visible == true)
                    {
                        cmnService.J_UserMessage("Please click once again...");
                        return;
                    }
                }
                else if (strChallanIDQueue == "")
                    strChallanIDQueue = txtChallanID.Text;
                else
                    strChallanIDQueue = strChallanIDQueue + "," + txtChallanID.Text;
                //
                #region CHECK FROM CORRECTION
                //FIRST CHECKING THE RECORD TO BE PRESENT IN THE CORRECTION RETURN TABLE

                strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "FROM  COR_HDR_BATCH, " +
                         "      COR_HDR_COMPANY " +
                         "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                         "AND   COR_HDR_BATCH.ASST_ID           = " + FaYearID + " " +
                         "AND   COR_HDR_BATCH.FORM_NO           = '" + FormNo + "' " +
                         "AND   COR_HDR_BATCH.QTR               = '" + Qtr + "' " +
                         "AND   COR_HDR_COMPANY.TAN_NO          = '" + TAN + "' " +
                         "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";

                iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                if (iCount > 0)
                {
                    //DATA NOT FOUND IN CORRECTION RETURN DATA
                    ClearChallanDeducteeDetailsCDDDDetails();

                    //DATA IS AVAILABLE IN CORRECTION RETURN

                    //SELECTING THE PROVISIONAL RECEIPT NO

                    //strSQL = "SELECT ORIGINAL_RRR_NO FROM COR_HDR_BATCH WHERE BATCH_HEADER_ID = " + iCount;
                    //txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                    strSQL = "SELECT TOP 1 COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "FROM  COR_HDR_CHALLAN, " +
                           "      COR_HDR_DEDUCTEE_DETAILS " +
                           "WHERE COR_HDR_CHALLAN.HDR_CHALLAN_ID = COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID " +
                           "AND   COR_HDR_CHALLAN.BATCH_HEADER_ID = " + iCount + " ";
                    if (strChallanIDQueue != "")
                        strSQL = strSQL + " AND COR_HDR_CHALLAN.HDR_CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                    strSQL = strSQL + " GROUP BY COR_HDR_CHALLAN.HDR_CHALLAN_ID " +
                           "ORDER BY COUNT(COR_HDR_DEDUCTEE_DETAILS.HDR_CHALLAN_ID) DESC";
                    txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                    //--
                    //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE

                    string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  2" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};

                    strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                             "       SL_NO, " + //-- 2014/12/06
                             "       BSR_CODE, " +
                             "       DEPOSIT_DATE, " +
                             "       TOT_TAX," +
                             "       BOOK_ENTRY " +
                             "FROM   COR_HDR_CHALLAN " +
                             "WHERE  BATCH_HEADER_ID = " + iCount + " " +
                             "AND    HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                    rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                    if (rdFetchCDDDDetails == null)
                        return;

                    while (rdFetchCDDDDetails.Read())
                    {
                        txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                        txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                        txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                        mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                        txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                        //-- for BOOK ENTRY
                        ////if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                        ////{
                        ////    chkBookAdjustment.Checked = true;
                        ////    //-- 2013/08/30
                        ////    //txtChallanNo.Text = "";
                        ////    //txtBSRCode.Text = "";
                        ////    //txtChallanNo.ReadOnly = true;
                        ////    //txtBSRCode.ReadOnly = true;
                        ////}
                        ////else
                        ////{
                        ////    chkBookAdjustment.Checked = false;
                        ////    //txtChallanNo.ReadOnly = false;
                        ////    //txtBSRCode.ReadOnly = false;
                        ////}
                    }

                    rdFetchCDDDDetails.Close();
                    rdFetchCDDDDetails.Dispose();

                    ////-- NIL CHALLAN STATEMENT
                    if (txtChallanTax.Text == "0.00")
                    {
                        if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM COR_HDR_CHALLAN WHERE BATCH_HEADER_ID = " + iCount))) == 0)
                            chkNilStatement.Checked = true;
                        else
                            chkNilStatement.Checked = false;
                    }
                    else
                        chkNilStatement.Checked = false;

                    strSQL = "SELECT DISTINCT TOP 3 DEDUCTEE_PAN," +
                             "                      TOTAL_AMOUNT," +
                             "                      INVALID_PAN " +
                             "FROM COR_HDR_DEDUCTEE_DETAILS " +
                             "WHERE BATCH_HEADER_ID = " + iCount + " " +
                        "AND   HDR_CHALLAN_ID  = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                    //" ";

                    rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);

                    int intInvalidPAN = 0;

                    for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                    {
                        if (i == 0)
                        {
                            txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 1)
                        {
                            txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                        else if (i == 2)
                        {
                            txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                            txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                            //
                            if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["INVALID_PAN"])) > 0)
                                intInvalidPAN = intInvalidPAN + 1;
                        }
                    }

                    rdFetchCDDDDetails.Close();
                    rdFetchCDDDDetails.Dispose();
                    //--
                    if (intInvalidPAN == 3)
                        chkNoValidPAN.Checked = true;
                    //--
                    blnDataEnteredbyUser = false;

                    this.Cursor = Cursors.Default;
                    //
                    btnChangeCDDDDetails.Visible = true;
                    //
                }
                #endregion
                else
                {
                    #region REGULAR RETURN
                    //now checking if the RETURN EXISTS IN THE ORIGINAL RETURN TABLE

                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO = '" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     = '" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO = '" + txtTAN.Text + "' ";

                    iCount = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));

                    if (iCount > 0)
                    {
                        //SELECTING THE PROVISIONAL RECEIPT NO

                        ////strSQL = "SELECT PRN_NO FROM TRN_BASIC_INFO WHERE BASIC_INFO_ID = " + iCount;

                        ////txtTokenNo.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //-- GETTING CHALLAN NO OF MAX DEDUCTEES
                        //strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                        //       "FROM  TRN_CHALLAN, " +
                        //       "      TRN_DEDUCTEE_DETAILS " +
                        //       "WHERE TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                        //       "AND   TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " " +
                        //       "GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                        //       "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        //-- ANIK @ 2015/12/16
                        strSQL = "SELECT TOP 1 TRN_CHALLAN.CHALLAN_ID " +
                                 "FROM  TRN_CHALLAN LEFT JOIN TRN_DEDUCTEE_DETAILS " +
                                 "ON    TRN_CHALLAN.CHALLAN_ID    = TRN_DEDUCTEE_DETAILS.CHALLAN_ID " +
                                 "WHERE TRN_CHALLAN.BASIC_INFO_ID = " + iCount + " ";
                        if (strChallanIDQueue != "")
                            strSQL = strSQL + " AND TRN_CHALLAN.CHALLAN_ID NOT IN (" + strChallanIDQueue + " )";
                        strSQL = strSQL + " GROUP BY TRN_CHALLAN.CHALLAN_ID " +
                                 "ORDER BY COUNT(TRN_DEDUCTEE_DETAILS.CHALLAN_ID) DESC";
                        txtChallanID.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        //--                            
                        //NOW SELECTING THE CHALLAN RECORD FROM THE REGULAR RETURN TABLE
                        string[,] strTVoucherChallanNo = {{"BOOK_ENTRY =  0" , "F", "CHALLAN_NO", "F"},
                                                      {"BOOK_ENTRY =  1" , "F", "TRANSFER_VOUCHER_NO", "F"}};
                        //
                        strSQL = "SELECT TOP 1 " + cmnService.J_SQLDBFormat(strTVoucherChallanNo, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CHALLAN_NO, " +
                                 "       SL_NO," + //-- 2014/12/06
                                 "       BSR_CODE, " +
                                 "       DEPOSIT_DATE, " +
                                 "       TOT_TAX," +
                                 "       BOOK_ENTRY " +
                                 "FROM   TRN_CHALLAN " +
                                 "WHERE  BASIC_INFO_ID = " + iCount + " " +
                                 "AND    CHALLAN_ID    = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        //
                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        //
                        if (rdFetchCDDDDetails == null)
                            return;
                        //
                        while (rdFetchCDDDDetails.Read())
                        {
                            txtSlNo.Text = rdFetchCDDDDetails["SL_NO"].ToString(); //-- 2014/12/06
                            txtChallanNo.Text = rdFetchCDDDDetails["CHALLAN_NO"].ToString();
                            txtBSRCode.Text = rdFetchCDDDDetails["BSR_CODE"].ToString();
                            mskChallanDate.Text = rdFetchCDDDDetails["DEPOSIT_DATE"].ToString();
                            txtChallanTax.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOT_TAX"]));
                            //-- for BOOK ENTRY
                            ////if (cmnService.J_ReturnInt32Value(Convert.ToString(rdFetchCDDDDetails["BOOK_ENTRY"])) > 0)
                            ////    chkBookAdjustment.Checked = true;
                            ////else
                            ////    chkBookAdjustment.Checked = false;
                        }
                        //
                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        ////-- NIL CHALLAN STATEMENT
                        if (txtChallanTax.Text == "0.00")
                        {
                            if (cmnService.J_ReturnDoubleValue(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUM(TOT_TAX) FROM TRN_CHALLAN WHERE BASIC_INFO_ID = " + iCount))) == 0)
                                chkNilStatement.Checked = true;
                            else
                                chkNilStatement.Checked = false;
                        }
                        else
                            chkNilStatement.Checked = false;

                        //NOW SELECTING ANY 3 DEDUCTEE RECORD FOR THAT RETURN

                        string strDeducteeTableName = "MST_DEDUCTEE";
                        string strDeducteeIdName = "DEDUCTEE_ID";
                        string strDeducteePANName = "DEDUCTEE_PAN";

                        if (cmbFormNo.Text == T_FormNo.F24Q)
                        {
                            strDeducteeTableName = "MST_EMPLOYEE";
                            strDeducteeIdName = "EMPLOYEE_ID";
                            strDeducteePANName = "EMPLOYEE_PAN";
                        }

                        //strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                        //         "             TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                        //         "FROM TRN_DEDUCTEE_DETAILS, " +
                        //         "     " + strDeducteeTableName + " " +
                        //         "WHERE TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                        //         "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));
                        strSQL = "SELECT DISTINCT TOP 3  " + strDeducteeTableName + "." + strDeducteePANName + " AS DEDUCTEE_PAN, " +
                                 "       TRN_DEDUCTEE_DETAILS.TOTAL_AMOUNT AS TOTAL_AMOUNT " +
                                 "FROM   TRN_DEDUCTEE_DETAILS, " +
                                 "       " + strDeducteeTableName + " " +
                                 "WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = " + strDeducteeTableName + "." + strDeducteeIdName + " " +
                                 "AND    TRN_DEDUCTEE_DETAILS.BASIC_INFO_ID = " + iCount + " " +
                                 "AND   TRN_DEDUCTEE_DETAILS.CHALLAN_ID = " + Convert.ToInt32(cmnService.J_NullToZero(txtChallanID.Text));

                        rdFetchCDDDDetails = dmlService.J_ExecSqlReturnReader(strSQL);
                        int intInvalidPAN = 0;
                        //
                        for (int i = 0; rdFetchCDDDDetails.Read(); i++)
                        {
                            if (i == 0)
                            {
                                txtDeducteePAN1.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted1.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 1)
                            {
                                txtDeducteePAN2.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted2.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                            else if (i == 2)
                            {
                                txtDeducteePAN3.Text = rdFetchCDDDDetails["DEDUCTEE_PAN"].ToString();
                                txtTDSDeducted3.Text = string.Format("{0:0.00}", Convert.ToDouble(rdFetchCDDDDetails["TOTAL_AMOUNT"]));
                                //
                                if (txtDeducteePAN1.Text == "PANNOTAVBL")
                                    intInvalidPAN = intInvalidPAN + 1;
                            }
                        }

                        rdFetchCDDDDetails.Close();
                        rdFetchCDDDDetails.Dispose();
                        //--
                        if (intInvalidPAN == 3)
                            chkNoValidPAN.Checked = true;
                        //--
                        blnDataEnteredbyUser = false;

                        this.Cursor = Cursors.Default;
                        //
                        btnChangeCDDDDetails.Visible = true;
                        //

                    }
                    #endregion
                    else
                    {
                        #region DATA DOES NOT EXIST
                        //DATA DOES NOT EXIST IN BOTH CORRECTION AS WELL AS REGULAR
                        ClearChallanDeducteeDetailsCDDDDetails();
                        //
                        btnChangeCDDDDetails.Visible = false;
                        //

                        #endregion
                    }
                }
                //--
                if (strChallanIDQueue != "" && txtSlNo.Text == "")
                {
                    FetchCDDDDetails(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex), cmbFormNo.Text, cmbQtr.Text, txtTAN.Text);
                    //cmnService.J_UserMessage("Please click once again...");
                    return;
                }
            }
            catch (Exception err)
            {

            }
        }
        #endregion

        #endregion

        #region chkNilStatement_CheckedChanged
        private void chkNilStatement_CheckedChanged(object sender, EventArgs e)
        {
            if(chkNilStatement.Checked==true)
            {
                //grpChallan.Enabled = false;
                txtSlNo.Text = "";
                txtSlNo.Enabled = false;
                txtChallanID.Text = "";
                txtChallanID.Enabled = false;
                txtChallanNo.Text = "";
                txtChallanNo.Enabled = false;
                txtBSRCode.Text = "";
                txtBSRCode.Enabled = false;
                mskChallanDate.Text = "";
                txtChallanTax.Text = "";
                txtChallanTax.Enabled = false;
            }
            else if (chkNilStatement.Checked == false)
            {
                //grpChallan.Enabled = true;
                txtSlNo.Enabled = true;
                txtChallanID.Enabled = true;
                txtChallanNo.Enabled = true;
                txtBSRCode.Enabled = true;
                mskChallanDate.Text = "";
                txtChallanTax.Enabled = true;
            }
        }
        #endregion

        #region chkNoValidPAN_CheckedChanged
        private void chkNoValidPAN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoValidPAN.Checked == true)
            {
                txtDeducteePAN1.Enabled = false;
                txtTDSDeducted1.Enabled = false;
                txtDeducteePAN2.Enabled = false;
                txtTDSDeducted2.Enabled = false;
                txtDeducteePAN3.Enabled = false;
                txtTDSDeducted3.Enabled = false;
                //-- 2026/02/12
                txtDeducteePAN1.Text = "";
                txtTDSDeducted1.Text = "0.00";
                txtDeducteePAN2.Text = "";
                txtTDSDeducted2.Text = "0.00";
                txtDeducteePAN3.Text = "";
                txtTDSDeducted3.Text = "0.00";
                //
                lnkRequestPAN.Enabled = false;
            }
            else
            {
                txtDeducteePAN1.Enabled = true;
                txtTDSDeducted1.Enabled = true;
                txtDeducteePAN2.Enabled = true;
                txtTDSDeducted2.Enabled = true;
                txtDeducteePAN3.Enabled = true;
                txtTDSDeducted3.Enabled = true;
                //
                lnkRequestPAN.Enabled = true;
            }
        }
        #endregion

        #region btnChangeCDDDDetails_Click
        private void btnChangeCDDDDetails_Click(object sender, EventArgs e)
        {
            FetchCDDDDetails(Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex), cmbFormNo.Text, cmbQtr.Text, txtTAN.Text);
        }
        #endregion

        private void btnChangeCDDDDetails_MouseMove(object sender, MouseEventArgs e)
        {

        }

        #region LnkRequestPAN_LinkClicked
        private void LnkRequestPAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            long lngHeaderID = 0;
            bool blCorr = false;
            try
            {
                strSQL = "SELECT TOP 1 COR_HDR_BATCH.BATCH_HEADER_ID " +
                            "FROM  COR_HDR_BATCH, " +
                            "      COR_HDR_COMPANY " +
                            "WHERE COR_HDR_COMPANY.BATCH_HEADER_ID = COR_HDR_BATCH.BATCH_HEADER_ID " +
                            "AND   COR_HDR_BATCH.ASST_ID           = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                            "AND   COR_HDR_BATCH.FORM_NO           = '" + cmbFormNo.Text + "' " +
                            "AND   COR_HDR_BATCH.QTR               = '" + cmbQtr.Text + "' " +
                            "AND   COR_HDR_COMPANY.TAN_NO          = '" + txtTAN.Text + "' " +
                            "ORDER BY COR_HDR_BATCH.BATCH_HEADER_ID DESC";
                lngHeaderID = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));
                if(lngHeaderID==0)
                {
                    strSQL = "SELECT BASIC_INFO_ID " +
                             "FROM   TRN_BASIC_INFO, " +
                             "       MST_COMPANY " +
                             "WHERE  MST_COMPANY.COMPANY_ID = TRN_BASIC_INFO.COMPANY_ID " +
                             "AND    TRN_BASIC_INFO.FORM_NO ='" + cmbFormNo.Text + "' " +
                             "AND    TRN_BASIC_INFO.QTR     ='" + cmbQtr.Text + "' " +
                             "AND    TRN_BASIC_INFO.ASST_ID = " + Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex) + " " +
                             "AND    MST_COMPANY.TAN_NO     ='" + txtTAN.Text + "' ";

                    lngHeaderID = Convert.ToInt32(cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)));
                    if (lngHeaderID == 0)
                    {
                        cmnService.J_UserMessage("Data not found !!");
                        grpPANLists.Visible = false;
                        return;
                    }
                }
                else
                    blCorr = true;
                //--
                if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
                {
                    if(blCorr==true)
                    {
                        strSQL = @"SELECT DISTINCT 1, 
                                          DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME AS DEDUCTEE
                                   FROM   COR_TRN_DEDUCTEE_DETAILS 
                                   WHERE  BATCH_HEADER_ID = " + lngHeaderID + @"
                                   ORDER BY DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME ";
                    }
                    else
                    {
                        strSQL = @"SELECT DISTINCT 1, 
                                          EMPLOYEE_PAN + ' - ' + EMPLOYEE_NAME AS DEDUCTEE
                                   FROM   TRN_DEDUCTEE_DETAILS, MST_EMPLOYEE 
                                   WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_EMPLOYEE.EMPLOYEE_ID 
                                   AND    BASIC_INFO_ID = " + lngHeaderID + @"
                                   ORDER BY EMPLOYEE_PAN + ' - ' + EMPLOYEE_NAME";

                    }
                }
                else// if (strFormName == T_NSDL_FORM_TYPE.Form16A)
                {
                    if (blCorr == true)
                    {
                        strSQL = @"SELECT DISTINCT 1, DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME AS DEDUCTEE
                                   FROM   COR_TRN_DEDUCTEE_DETAILS 
                                   WHERE  BATCH_HEADER_ID = " + lngHeaderID + @"
                                   ORDER BY DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME";
                    }
                    else
                    {
                        strSQL = @"SELECT DISTINCT 1, 
                                          DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME AS DEDUCTEE
                                   FROM   TRN_DEDUCTEE_DETAILS, MST_DEDUCTEE 
                                   WHERE  TRN_DEDUCTEE_DETAILS.PARTY_ID = MST_DEDUCTEE.DEDUCTEE_ID 
                                   AND    BASIC_INFO_ID = " + lngHeaderID + @"
                                   ORDER BY DEDUCTEE_PAN + ' - ' + DEDUCTEE_NAME";
                    }
                }
                grpPANLists.Visible = true;
                //BtnSave.Enabled = false;
                //
                chklstPANs.DisplayMember = "Item";
                chklstPANs.ValueMember = "Item";
                if (T_PopulateListBox(dmlService.J_pCommand, strSQL, ref chklstPANs) == false) return;
            }
            catch(Exception err)
            {

            }
        }

        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            grpPANLists.Visible = false;
            //BtnSave.Enabled = true;
        }

        private void ChklstPANs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("SelectedIndexChanged");
        }


        #region ChklstPANs_SelectedValueChanged
        private void ChklstPANs_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (chklstPANs.CheckedIndices.Count > 0)
            //    lblCounter.Text = chklstPANs.CheckedIndices.Count.ToString() + "/10 selected";
            //else
            //    lblCounter.Text = "";
            //MessageBox.Show("SelectedValueChanged");
            //foreach (int indexChecked in chklstPANs.CheckedIndices)
            //{
            //    // The indexChecked variable contains the index of the item.
            //    //MessageBox.Show("Index: " + indexChecked.ToString() + ", is checked. Checked state is:" +
            //    //                chklstPANs.GetItemCheckState(indexChecked).ToString() + ".");
            //    chklstPANs.GetItemCheckState(indexChecked).ToString();
            //}
        }
        #endregion

        private void LnkPost2526_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //--
            //TdsMan.CloseChildForm(new TrnRequestConsolidatedFile(""), this);
            ////--
            //cmnService.J_ShowChildForm(new TrnRequestConsolidatedFile(T_NSDL_FORM_TYPE.Form16A), this, "Request Form 16A");
            TdsMan.CloseChildForm(new TrnRequestTracesITAct2025(""), this);
            //--
            if (strFormName == T_NSDL_FORM_TYPE.ConsolidatedStatement)
            {
                cmnService.J_ShowChildForm(new TrnRequestTracesITAct2025(T_NSDL_FORM_TYPE.ConsolidatedStatement), J_Var.frmMain, "Request Consolidated File");
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16ARequest)
            {
                cmnService.J_ShowChildForm(new TrnRequestTracesITAct2025(T_NSDL_FORM_TYPE.Form16ARequest), J_Var.frmMain, "Request Form 131 (16A)");
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form16Request)
            {
                cmnService.J_ShowChildForm(new TrnRequestTracesITAct2025(T_NSDL_FORM_TYPE.Form16Request), J_Var.frmMain, "Request Form 130 (16)");
            }
            else if (strFormName == T_NSDL_FORM_TYPE.TANPANFile)
            {
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Form27DRequest)
            {
                cmnService.J_ShowChildForm(new TrnRequestTracesITAct2025(T_NSDL_FORM_TYPE.Form27DRequest), J_Var.frmMain, "Request Form 133 (27D)");
            }
            else if (strFormName == T_NSDL_FORM_TYPE.Defaults)
            {
                cmnService.J_ShowChildForm(new TrnRequestTracesITAct2025(T_NSDL_FORM_TYPE.Defaults), J_Var.frmMain, "Request for Defaults/Justification Report");
            }
            else
            {
            }
            //
            GC.Collect();
            //
            this.Close();
            this.Dispose();
        }

        private void ChklstPANs_Click(object sender, EventArgs e)
        {
            //if (e.NewValue == CheckState.Checked)
            //{
            //    MessageBox.Show(itemText + " checked");
            //    if (chklstPANs.CheckedIndices.Count == 10)
            //    {
            //        e.NewValue = CheckState.Unchecked;

            //        cmnService.J_UserMessage("Maximum 10 PANs can be selected");
            //        return;
            //    }
            //}
            //--
            if (chklstPANs.CheckedIndices.Count > 0)
                lblCounter.Text = chklstPANs.CheckedIndices.Count.ToString() + "/10 selected";
            else
                lblCounter.Text = "";
        }


        #region ChklstPANs_ItemCheck
        private void ChklstPANs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var dv = chklstPANs.DataSource as DataView;
            var drv = dv[e.Index];
            drv["Checked"] = e.NewValue == CheckState.Checked ? true : false;
            //string itemText = chklstPANs.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                //MessageBox.Show(itemText + " checked");
                if (chklstPANs.CheckedIndices.Count == 10)
                {
                    e.NewValue = CheckState.Unchecked;
                    //
                    cmnService.J_UserMessage("Maximum 10 PANs can be selected");
                    return;
                }
            }
            //
        }
        #endregion

        #region TxtSearchPAN_TextChanged
        private void TxtSearchPAN_TextChanged(object sender, EventArgs e)
        {
            var dv = chklstPANs.DataSource as DataView;
            var filter = txtSearchPAN.Text.Trim().Length > 0
                ? $" Item LIKE '*{txtSearchPAN.Text}*'"
                : null;

            dv.RowFilter = filter;

            for (var i = 0; i < chklstPANs.Items.Count; i++)
            {
                var drv = chklstPANs.Items[i] as DataRowView;
                var chk = Convert.ToBoolean(drv["Checked"]);
                chklstPANs.SetItemChecked(i, chk);
            }
        }
        #endregion

        #region POPULATE LIST BOX
        public bool T_PopulateListBox(IDbCommand command, string SqlText, ref CheckedListBox checkedlistbox)
        {
            IDataReader reader = null;

            try
            {
                if (command == null)
                    reader = dmlService.J_ExecSqlReturnReader(SqlText);
                else if (command != null)
                    reader = dmlService.J_ExecSqlReturnReader(command, SqlText);

                if (reader == null)
                {
                    reader.Close();
                    reader.Dispose();
                    return false;
                }

                //cmnService.J_ClearComboBox(ref combobox, DefaultText, ComboBoxDefaultText, J_ComboBoxSelectedIndex.NO);
                checkedlistbox.Items.Clear();

                var dt = new DataTable();

                dt.Columns.Add("Item", typeof(string));
                dt.Columns.Add("Checked", typeof(bool));

                while(reader.Read()) dt.Rows.Add(reader.GetString(1), false);

                dt.AcceptChanges();

                checkedlistbox.DataSource = dt.DefaultView;
                checkedlistbox.DisplayMember = "Item";
                checkedlistbox.ValueMember = "Item";


                //while (reader.Read())
                //    //listbox.Items.Add(new Listbo (reader.GetString(1).ToString(), Convert.ToInt32(reader.GetValue(0))));
                //    checkedlistbox.Items.Add(reader.GetString(1).ToString());

                reader.Close();
                reader.Dispose();

                return true;
            }
            catch
            {
                reader.Close();
                reader.Dispose();

                //dmlService.J_CloseConnection();
                //commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return false;
            }
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
    }

}
