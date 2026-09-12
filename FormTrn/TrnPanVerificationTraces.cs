
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

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormSys;
using TDSMAN.FormRpt;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnPanVerificationTraces : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnPanVerificationTraces()
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

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        bool blnVerificationComplete = false;
        //--            
        string strNOTAVAILABLE = "NOT AVAILABLE";
        ToolTip tllTip = new ToolTip();
        //
        enum enmState
        {
            Captcha,
            PanNameExtract
        }
        //
        enum enmRequestType
        {
            Login,
            PanValidation,
            LogOff
        }
        //
        string strSTATUS = ""; string strVerifiedNAME = "";
        #endregion

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
        #region TrnPanVerificationTraces_Activated
        private void TrnPanVerificationTraces_Activated(object sender, EventArgs e)
        {
            //-- Added By Abhishek Dey On 22/08/2018 --
            if (TDSMAN.Classes.TDSMAN.T_ENABLE_HIDE_PASSWORD == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            //-----------------------------------------
        }
        #endregion
        //-----------------------------------------

        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPAN.Text = "";
            //ShowCaptcha();
            Clearcontrols();
            //
            grpLoginDetails.Visible = false;
           //           
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Clearcontrols();
                //
                if (IsValidField())
                {
                    this.Cursor = Cursors.WaitCursor;
                    //--
                    #region COMMENT

                    /*string strname = "";
                    bool bnlStatus = objAccount.ExtractPANName(txtPAN.Text, txtCaptchaCode.Text, out strname);

                    if (bnlStatus==false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage(strname);
                        txtPAN.Select();
                        ShowCaptcha();
                        return;
                    }
                    else
                    {
                      //  PANVerifierDetails objPan = (PANVerifierDetails)response.CustomeTypes;
                        
                        grpDetails.Visible = true;
                        
                        lblDetails.Text = txtPAN.Text;
                        lblSurname.Text = strname;
                        //lblMiddleName.Text = objPan.MiddleName;
                        //lblFirstName.Text = objPan.FirstName;
                        //lblAreaCode.Text = objPan.AreaCode;
                        //lblAOType.Text = objPan.AOType;
                        //lblRangeCode.Text = objPan.RangeCode;
                        //lblAONumber.Text = objPan.AONumber;
                        //lblJurisdiction.Text = objPan.Jurisdiction;
                        //lblBuildingName.Text = objPan.BuildingName;

                        ShowCaptcha();

                        txtCaptchaCode.Text = "";
                    } */


                    //ArrayList objData = new ArrayList();
                    //objData.Add(enmState.PanNameExtract);
                    //objData.Add(txtPAN.Text);
                    //objData.Add(txtCaptchaCode.Text);

                    //bgWorker.RunWorkerAsync(objData);


                    //this.Cursor = Cursors.Default; 

                    #endregion
                    //--
                    //--
                    if (TdsMan.T_CheckInternetConnectivty() == false)
                    {
                        cmnService.J_UserMessage("Internet Connectivity not found", MessageBoxIcon.Exclamation);
                        //BtnExit.Select();
                        return;
                    }
                    //--
                    if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    if (blnVerificationComplete == true)
                    {
                        blnVerificationComplete = false;
                        bgwPANVerification.RunWorkerAsync();
                    }
                    else
                    {
                        //
                        this.Cursor = Cursors.WaitCursor;
                        //
                        #region COMMENT

                        //if (TDSMAN.Classes.TDSMAN.T_FromModule == "")
                        //                        TDSMAN.Classes.TDSMAN.T_pTAN = "";
                        //                    //--
                        //                    if (TDSMAN.Classes.TDSMAN.T_pTAN == "")
                        //                    {
                        //                        if (rbnRegularReturn.Checked == true)
                        //                            TDSMAN.Classes.TDSMAN.T_pTAN = cmnService.J_Right(cmbCompany.Text.Trim(), 11).Replace("]", "").Trim();
                        //                        else if (rbnCorrectionReturn.Checked == true)
                        //                            TDSMAN.Classes.TDSMAN.T_pTAN = Convert.ToString(dgcViewBatch.Rows[dgcViewBatch.CurrentRow.Index].Cells[5].Value);
                        //                    }
                        //                    //-- GET TAN DETILS IF AVAILABLE
                        //                    strSQL = @"SELECT  TOP 1 MST_TAN_ACCOUNT.TAN_NO,
                        //                                   MST_TAN_ACCOUNT.LOGIN_ID,
                        //                                   MST_TAN_ACCOUNT.USER_PASSWORD                                   
                        //                           FROM    MST_TAN_ACCOUNT 
                        //                           WHERE   MST_TAN_ACCOUNT.TAN_NO = '" + TDSMAN.Classes.TDSMAN.T_pTAN + "'";
                        //                    //
                        //                    IDataReader drdShowRecord = null;
                        //                    drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                        //                    if (drdShowRecord == null)
                        //                        return;
                        //                    //
                        //                    while (drdShowRecord.Read())
                        //                    {
                        //                        txtTANNo.Text = Convert.ToString(drdShowRecord["TAN_NO"]);
                        //                        txtUserID.Text = Convert.ToString(drdShowRecord["LOGIN_ID"]);
                        //                        txtPassword.Text = Convert.ToString(drdShowRecord["USER_PASSWORD"]);
                        //                        txtCaptchaCode.Select();
                        //                    }
                        //                    //
                        //                    drdShowRecord.Close();
                        //                    drdShowRecord.Dispose();
                        #endregion
                        //--
                        //InitializeCaptcha();
                        picCaptcha.Image = Properties.Resources.captcha_loading;
                        if (!bgWorkerLoadCaptcha.IsBusy)
                            bgWorkerLoadCaptcha.RunWorkerAsync();
                        //
                        //
                        grpLoginDetails.Visible = true;
                        //--
                        txtTAN.Select();
                        //--
                        this.Cursor = Cursors.Default;
                    }                    
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message);
            }

        }
        #endregion

        #region ShowCaptcha
        //private void ShowCaptcha()
        //{
        //    //--
        //    if (TdsMan.T_CheckInternetConnectivty() == false)
        //    {
        //        cmnService.J_UserMessage("Internet Connectivity not found");
        //        //BtnExit.Select();
        //        return;
        //    }
        //    //
        //    objAccount = new TracesConnect();

        //    Stream imgStream = objAccount.GetCaptchaForPANNAme();
        //    Image img = Image.FromStream(imgStream);
        //    this.picCaptcha.Image = img;
        //    //-------------------------------------------------------
        //    txtCaptchaCode.Text = "";

        //}

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            InitializeCaptcha();
        }

        #endregion

        #region TrnPanVarification_Load
        private void TrnPanVarification_Load(object sender, EventArgs e)
        {
            //--
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);
            //--
            try
            {
                //txtPAN.Text = "BROPK6848J";
                lblTitle.Text = "PAN Verification";
                
                //--            
                //ArrayList objResult = new ArrayList();
                //objResult.Add(enmState.Captcha);
                //bgWorker.RunWorkerAsync(objResult);

                //ShowCaptcha();
                //--
                Clearcontrols();
            }
            catch (Exception err)
            {
                grpDetails.Visible = false;
                //
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region IsValidField
        private bool IsValidField()
        {
            if (string.IsNullOrEmpty(txtPAN.Text.Trim()))
            {
                cmnService.J_UserMessage("Please enter the PAN");
                txtPAN.Focus();
                return false;
            }
            else
            {
                string strMessage = "";
                if (!FormValidation.IsValidPAN(txtPAN.Text, out strMessage))
                {
                    cmnService.J_UserMessage("Please enter a valid PAN");
                    txtPAN.Focus();
                    return false;
                }
            }
            //----------------------------------------------------------
            //if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            //{
            //    cmnService.J_UserMessage("Please enter the Captcha Code");
            //    txtPAN.Focus();
            //    return false;

            //}


            return true;

        }


        #endregion


        #region Clearcontrols
        private void Clearcontrols()
        {
           grpDetails.Visible = false;
           //lblDetails.Text = "";
           lblSurname.Text = "";
           lblMiddleName.Text = "";
           lblFirstName.Text = "";
           lblAreaCode.Text = "";
           lblAOType.Text = "";
           lblRangeCode.Text = "";
           lblAONumber.Text = "";
           lblJurisdiction.Text = "";
           lblBuildingName.Text = "";
        }
        #endregion        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnPrintPan_Click
        private void btnPrintPan_Click(object sender, EventArgs e)
        {
            try
            {
                RptDialog rptDialog = new RptDialog();
                rptDialog.PANVerification_RDLC(txtPAN.Text,
                                          lblSurname.Text,
                                          lblMiddleName.Text,
                                          lblFirstName.Text,
                                          lblAreaCode.Text,
                                          lblAOType.Text,
                                          lblRangeCode.Text,
                                          lblAONumber.Text,
                                          lblJurisdiction.Text,
                                          lblBuildingName.Text);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
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
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
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
            txtPAN.Select();
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
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                blnVerificationComplete = false;
                //-------------------------------------------
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
                    break;
                case enmRequestType.PanValidation:

                    //grpDownloadList.Visible = true;
                    //grpEnterLoginDetails.Enabled = false;
                    //grpCaptcha.Enabled = false;
                    //--
                    grpLoginDetails.Visible = false;
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
                    if (!bgwPANVerification.IsBusy)
                        bgwPANVerification.RunWorkerAsync();
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


        #region bgwPANVerification_DoWork
        //private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        //
        //        ArrayList objRetval = new ArrayList();
        //        TracesResponse objResponse = new TracesResponse();
        //        Label.CheckForIllegalCrossThreadCalls = false;
        //        string strSTATUS = ""; string strVerifiedNAME = "";
        //        if (blnVerificationComplete == false)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            //pctGreenDownArrow.Visible = true;
        //            //--
        //            TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(txtPAN.Text.Trim()),
        //                                                                                       out strVerifiedNAME, out strSTATUS);
        //            //--
        //            grpLoginDetails.Visible = false;
        //            //
        //            this.Cursor = Cursors.Default;
        //            //
        //            if (response.Respons == enmResponse.Success)
        //            {
        //                if (strVerifiedNAME.ToUpper() == strNOTAVAILABLE)
        //                {
        //                    grpLoginDetails.Visible = false;
        //                    txtPAN.Select();
        //                    cmnService.J_UserMessage("Invalid PAN");
        //                    //--
        //                    if (bgwPANVerification.CancellationPending)
        //                    {
        //                        e.Cancel = true;
        //                        blnVerificationComplete = false;
        //                        //pctGreenDownArrow.Visible = false;
        //                        //return;
        //                    }
        //                    return;
        //                }
        //                else
        //                {
        //                    grpDetails.Visible = true;
        //                    //
        //                    //lblDetails.Text = txtPAN.Text;
        //                    lblSurname.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
        //                    //--
        //                    //-- 2023/10/12
        //                    if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE || strSTATUS.ToUpper() == T_PANVerificationStatus.ACTIVE)
        //                    {
        //                        cmnService.J_UserMessage("PAN is valid and operative");
        //                        lblStatus.Visible = true;
        //                        lblStatus.ForeColor = Color.Green;
        //                        lblStatus.Text = T_PANVerificationStatus.VALID_OPERATIVE.ToString();
        //                        this.Cursor = Cursors.Default;
        //                        //--
        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                                "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                                "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
        //                                " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                        }
        //                        blnVerificationComplete = true;
        //                        return;
        //                    }
        //                    else if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_INOPERATIVE)
        //                    {
        //                        cmnService.J_UserMessage("PAN is valid and inoperative");
        //                        lblStatus.Visible = true;
        //                        lblStatus.ForeColor = Color.Yellow;
        //                        lblStatus.BackColor = Color.Black;
        //                        lblStatus.Text = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();
        //                        this.Cursor = Cursors.Default;
        //                        //--
        //                        if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                        {
        //                            dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME, VERIFIED_STATUS) " +
        //                                "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                                "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "'," +
        //                                "" + T_PANVerificationStatusId.VALID_INOPERATIVE + ")");
        //                        }
        //                        else
        //                        {
        //                            dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "', " +
        //                                " VERIFIED_STATUS = " + T_PANVerificationStatusId.VALID_INOPERATIVE + " " +
        //                                " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                        }
        //                        blnVerificationComplete = true;
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        blnVerificationComplete = true;
        //                        return;
        //                    }
        //                    ////if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
        //                    ////{
        //                    ////    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
        //                    ////        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
        //                    ////        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
        //                    ////}
        //                    ////else
        //                    ////{
        //                    ////    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
        //                    ////        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
        //                    ////}
        //                    //--
        //                }
        //            }
        //            else
        //            {
        //                txtPAN.Select();
        //                cmnService.J_UserMessage("Please try again !!");
        //                return;
        //            }
        //            blnVerificationComplete = true;
        //            //--
        //            //bgwPANVerification.CancelAsync();
        //            //bgwPANVerification.Dispose();
        //            //
        //            GC.Collect();
        //            this.Cursor = Cursors.Default;
        //            //--
        //        }
        //        //--
        //        if (bgwPANVerification.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            blnVerificationComplete = false;
        //            this.Cursor = Cursors.Default;
        //            //pctGreenDownArrow.Visible = false;
        //            return;
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        blnVerificationComplete = false;
        //        cmnService.J_UserMessage(err.Message);
        //    }
        //}
        #endregion

        #region bgwPANVerification_RunWorkerCompleted
        //private void bgwPANVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        this.pgTimer.Stop();
        //        pBar.Value = 100;
        //        bgwPANVerification.CancelAsync();
        //        bgwPANVerification.Dispose();
        //        //this.Close();
        //    }
        //    catch (Exception err)
        //    {
        //        //blnVerificationComplete = false;
        //        this.Cursor = Cursors.Default;
        //        //cmnService.J_UserMessage(err.Message);
        //    }

        //}
        #endregion


        #region bgwPANVerification_DoWork
        private void bgwPANVerification_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //
                ArrayList objRetval = new ArrayList();
                TracesResponse objResponse = new TracesResponse();
                Label.CheckForIllegalCrossThreadCalls = false;
                //string strSTATUS = ""; string strVerifiedNAME = "";
                if (blnVerificationComplete == false)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //pctGreenDownArrow.Visible = true;
                    //--
                    TracesResponse response = objTracesConnect.RequestForPANValidation(Convert.ToString(txtPAN.Text.Trim()),
                                                                                               out strVerifiedNAME, out strSTATUS);
                    //--
                    grpLoginDetails.Visible = false;
                    //
                    this.Cursor = Cursors.Default;
                    //
                    if (response.Respons == enmResponse.Success)
                    {
                        if (strVerifiedNAME.ToUpper() == strNOTAVAILABLE)
                        {
                            grpLoginDetails.Visible = false;
                            txtPAN.Select();
                            cmnService.J_UserMessage("Invalid PAN");
                            return;
                        }
                        else
                        {
                            grpDetails.Visible = true;
                            //
                            //lblDetails.Text = txtPAN.Text;
                            //txtPANName.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
                            lblSurname.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
                            //-- 2023/10/12
                            if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE || strSTATUS.ToUpper() == T_PANVerificationStatus.ACTIVE)
                            {
                                lblStatus.Visible = true;
                                lblStatus.ForeColor = Color.Green;
                                lblStatus.Text = T_PANVerificationStatus.VALID_OPERATIVE.ToString();
                                //--
                                if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                                        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                                        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                                }
                                else
                                {
                                    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                                        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                                }
                            }
                            else if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_INOPERATIVE)
                            {
                                lblStatus.Visible = true;
                                lblStatus.ForeColor = Color.Yellow;
                                lblStatus.BackColor = Color.Black;
                                lblStatus.Text = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();
                                //--
                                if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                                {
                                    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                                        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                                        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                                }
                                else
                                {
                                    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                                        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                                }
                            }
                            ////--
                            //if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                            //{
                            //    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                            //        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                            //        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                            //}
                            //else
                            //{
                            //    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                            //        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                            //}
                            //--
                        }
                    }
                    else
                    {
                        txtPAN.Select();
                        cmnService.J_UserMessage("Please try again !!");
                        return;
                    }
                    blnVerificationComplete = true;
                    //--
                    //bgwPANVerification.CancelAsync();
                    //bgwPANVerification.Dispose();
                    //
                    GC.Collect();
                    this.Cursor = Cursors.Default;
                    //--
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
            catch (Exception err)
            {
                blnVerificationComplete = false;
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region bgwPANVerification_RunWorkerCompleted
        private void bgwPANVerification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                if (e.Cancelled)
                {
                    //btnVerification.Text = strbtnVerification;
                    //btnVerification.ForeColor = Color.Black;
                    blnVerificationComplete = false;
                }
                //--
                //if(bgwPANVerification.CancellationPending==false)
                if (blnVerificationComplete == true)
                {
                    //cmnService.J_UserMessage("Validation completed");
                    //btnVerification.Enabled = true;
                    //btnVerification.BackColor = Color.Lavender;
                    ////
                    ////--
                    //grpStatus.Enabled = true;
                    ////
                    //btnVerification.Enabled = false;
                    ////
                    //dgvDeductees.Enabled = true;
                    //grpReturnSelection.Enabled = true;
                    //grpRegularReturn.Enabled = true;
                    //grpCorrectionReturn.Enabled = true;
                    //j = 0;
                    //                
                }
                else
                {
                    //cmnService.J_UserMessage("Validation stopped", MessageBoxIcon.Exclamation);
                    blnVerificationComplete = false;
                    //grpReturnSelection.Enabled = true;
                    //grpRegularReturn.Enabled = true;
                    //grpCorrectionReturn.Enabled = true;
                    //bgwPANVerification.CancelAsync();
                    //bgwPANVerification.Dispose();
                    //bgwPANVerification = null;
                }
                //
                //btnVerification.Text = strbtnVerification;
                //btnVerification.ForeColor = Color.Black;
                ////blnVerificationComplete = false;
                ////
                ////--
                //if (cmnService.J_ReturnInt32Value(lblInvalidNo.Text) > 0)
                //    btnPrintInvalidPAN.Enabled = true;
                //else
                //    btnPrintInvalidPAN.Enabled = false;
                ////--
                //if (cmnService.J_ReturnInt32Value(lblUnmatchedNo.Text) > 0)
                //    btnPrintUnmatched.Enabled = true;
                //else
                //    btnPrintUnmatched.Enabled = false;
                //--            
                //if (response.Respons == enmResponse.Success)
                //{
                    if (strVerifiedNAME.ToUpper() == strNOTAVAILABLE)
                    {
                        grpLoginDetails.Visible = false;
                        txtPAN.Select();
                        cmnService.J_UserMessage("Invalid PAN");
                        return;
                    }
                    else
                    {
                        grpDetails.Visible = true;
                        //
                        //lblDetails.Text = txtPAN.Text;
                        //txtPANName.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
                        lblSurname.Text = strVerifiedNAME.ToUpper().Trim().Replace("AMP;", "").Replace("  ", " ");
                        //-- 2023/10/12
                        if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_OPERATIVE || strSTATUS.ToUpper() == T_PANVerificationStatus.ACTIVE)
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            lblStatus.Text = T_PANVerificationStatus.VALID_OPERATIVE.ToString();
                            //--
                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                            {
                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                                    "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                                    "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                            }
                            else
                            {
                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                                    " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                            }
                        }
                        else if (strSTATUS.ToUpper() == T_PANVerificationStatus.VALID_INOPERATIVE)
                        {
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Yellow;
                            lblStatus.BackColor = Color.Black;
                            lblStatus.Text = T_PANVerificationStatus.VALID_INOPERATIVE.ToString();
                            //--
                            if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                            {
                                dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                                    "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                                    "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                            }
                            else
                            {
                                dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                                    " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                            }
                        }
                        ////--
                        //if (dmlService.J_IsRecordExist("MST_VERIFIED_PAN", " PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'") == false)
                        //{
                        //    dmlService.J_ExecSql("INSERT INTO MST_VERIFIED_PAN(PAN, VERIFIED_NAME) " +
                        //        "VALUES('" + Convert.ToString(txtPAN.Text.Trim()) + "'," +
                        //        "'" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "')");
                        //}
                        //else
                        //{
                        //    dmlService.J_ExecSql("UPDATE MST_VERIFIED_PAN SET VERIFIED_NAME ='" + cmnService.J_ReplaceQuote(Convert.ToString(strVerifiedNAME)) + "' " +
                        //        " WHERE PAN ='" + Convert.ToString(txtPAN.Text.Trim()) + "'");
                        //}
                        //--
                    }
                //}
            }
            catch (Exception err)
            {
                blnVerificationComplete = false;
                cmnService.J_UserMessage(err.Message);
            }
        }
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
                this.Cursor = Cursors.WaitCursor;
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
            catch (Exception err)
            {
                blnVerificationComplete = false;
                this.Cursor = Cursors.Default;
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
                enmRequestType enmReqType = (enmRequestType)objMessage[0];
                TracesResponse objResponse = (TracesResponse)objMessage[1];
                //---------------------------------------------------------
                this.Cursor = Cursors.WaitCursor;
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
                            this.Cursor = Cursors.Default;
                            //grdPANValidate.DataSource = objBindingSource;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            cmnService.J_UserMessage(objResponse.Message);
                            this.Cursor = Cursors.Default;
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
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            this.Cursor = Cursors.Default;
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
                blnVerificationComplete = false;
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.Message, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region pctUserManual_Click
        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0064", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }
        #endregion

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeCaptcha();
        }
    }
}

