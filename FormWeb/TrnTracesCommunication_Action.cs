
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
using System.Text.RegularExpressions;
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

using TDSMAN.Classes;
using TDSMAN.FormTrn;
using TDSMAN.FormSys;

#endregion

namespace TDSMAN.FormWeb
{
    public partial class TrnTracesCommunication_Action : TDSMAN.FormGen.GenForm
    {
        ResizeForm _form_resize;

        #region System Generated Code
        public TrnTracesCommunication_Action()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
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

        #region Objects & Variables declaration

        TracesConnect objAccount = new TracesConnect();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();


        DMLService dmlService = new DMLService();

        string strSQL = string.Empty;
        bool blnShowHelp = false;
        //--            
        ToolTip tllTip = new ToolTip();

        enum enmRequestType
        {
            Login,
            ListofFiliing,
            AddStatementDetail,
            CommuncationDetailGrid1,
            CommuncationDetailGrid2,
            LogOff
        }

        int intRowIndex = 0;
        int intColumnIndex = 0;

        private DataTable _dtOriginal;   // backup copy
        //private TracesConnect_OneLogin oneLogin;

        private string CurrentCaptchaId = "";
        #endregion

        //-- Added By Abhishek Dey On 22/08/2018 --
        #region TrnStatementStatusTraces_Activated
        private void TrnStatementStatusTraces_Activated(object sender, EventArgs e)
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

        #region TrnStatementStatusTraces_Load
        private void TrnStatementStatusTraces_Load(object sender, EventArgs e)
        {
            ////int h = Screen.PrimaryScreen.WorkingArea.Height;
            ////int w = Screen.PrimaryScreen.WorkingArea.Width;
            ////this.ClientSize = new Size(w, h);

            ////lblTitle.Text = "Request For Submit Declaration of non-filling";
            ////lblTitle.Text = "Communication - Inbox (Action)";

            ////InitializeCaptcha();
            ////picCaptcha.Image = Properties.Resources.captcha_loading;
            ////if (!bgWorkerLoadCaptcha.IsBusy)
            ////    bgWorkerLoadCaptcha.RunWorkerAsync();
            //////--

            ////ClearControls();
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);

            lblTitle.Text = "Communication - Inbox (Action)";

            //-------------------------------------------------------
            // NEW: OneLogin check
            //-------------------------------------------------------
            if (TracesSessionManager_OneLogin.IsSessionActive())
            {
                // Hide login controls (captcha, TAN, userId, password, login button)
                grpLoginDetails.Visible = false;   // <-- create GroupBox of login UI

                //MessageBox.Show("TRACES session active -> Login not required");
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.CommuncationDetailGrid1);
                //-------------------------------------------
                pgTimer.Start();
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);
                // LATER we will call:
                // LoadInboxAction_OneLogin();

                return;
            }

            //-------------------------------------------------------
            // If no session, show login UI
            //-------------------------------------------------------
            grpLoginDetails.Visible = true;

            // Load captcha (existing logic)
            if (!bgWorkerLoadCaptcha.IsBusy)
                bgWorkerLoadCaptcha.RunWorkerAsync();

            ClearControls();
        }
        #endregion              

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //--
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    BtnExit.Select();
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
                //--
                TracesLogin objLogin = new TracesLogin();
                objLogin.UserID = txtUserID.Text;
                objLogin.Password = txtPassword.Text;
                objLogin.TAN = txtTANNo.Text;
                objLogin.CaptchaCode = txtCaptchaCode.Text;
                lblCaptchaId.Text = this.CurrentCaptchaId; //-- 2026/04/08
                //--------------------------------------------
                ArrayList objList = new ArrayList();
                objList.Add(enmRequestType.Login);
                objList.Add(objLogin);
                //-------------------------------------------
                pgTimer.Start();
                //-------------------------------------------
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync(objList);
                //
            }
            catch //(Exception err)
            {
                //cmnService.J_UserMessage(err.Message);
                cmnService.J_UserMessage("We are not able to receive the response from the TRACES webiste.\n It is requested to check your details at TRACES website with the given parameters");
            }
        }

        #endregion

        #region btnLoginCancel_Click
        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region btnCaptchaRefresh_Click
        private void btnCaptchaRefresh_Click(object sender, EventArgs e)
        {
            //InitializeCaptcha();
            InitializeCaptcha_OneLogin();
        }

        #endregion

        #region btnCaptchaRefresh_MouseMove
        private void btnCaptchaRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            tllTip.Show("Click to refresh image", btnCaptchaRefresh);
        }
        #endregion

        #region btnGo_Click
        private void btnGo_Click(object sender, EventArgs e)
        {

            //if (cmbFAYear.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Select FA Year");
            //    cmbFAYear.Select();
            //    return;
            //}
            //if (cmbFormNo.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Select Form");
            //    cmbFormNo.Select();
            //    return;
            //}
            //if (cmbQtr.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Select FA Year");
            //    cmbQtr.Select();
            //    return;
            //}
            //if (cmbReason.SelectedIndex <= 0)
            //{
            //    cmnService.J_UserMessage("Select Reason");
            //    cmbReason.Select();
            //    return;
            //}
            //if (cmbReason.SelectedIndex == 7)
            //{
            //    if (string.IsNullOrWhiteSpace(txtspecReasn.Text))
            //    {
            //        cmnService.J_UserMessage("Specify the exact reason is mandatory");
            //        return;
            //    }
            //}



            //NonFilling objData = new NonFilling();
            //objData.Flag = "X";
            //objData.SpecifyReason = "BLANK";

            //if (cmbReason.SelectedIndex == 7)
            //    objData.SpecifyReason = txtspecReasn.Text;

            ////------------------------------------------------
            //if (cmbReason.SelectedIndex == 4 || cmbReason.SelectedIndex == 6)
            //{
            //    DialogResult result = cmnService.J_UserMessage("Please confirm, whether you have requested jurisdictional assessing officer for closure/surrender of TAN.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //    if (result == DialogResult.Yes)
            //    {
            //        objData.Flag = "Y";
            //    }
            //    else if (result == DialogResult.No)
            //    {
            //        objData.Flag = "N";
            //    }

            //}


            ////------------------------------------------------
            //ArrayList objList = new ArrayList();
            //objList.Add(enmRequestType.AddStatementDetail);


            //objData.FAYear = cmbFAYear.Text.Substring(0, cmbFAYear.Text.IndexOf("-"));
            //if (cmbQtr.SelectedIndex > 0)
            //{
            //    switch (cmbQtr.Text)
            //    {
            //        case "Q1":
            //            objData.Quarter = "3";
            //            break;
            //        case "Q2":
            //            objData.Quarter = "4";
            //            break;
            //        case "Q3":
            //            objData.Quarter = "5";
            //            break;
            //        case "Q4":
            //            objData.Quarter = "6";
            //            break;
            //    }
            //}


            //if (cmbReason.SelectedIndex > 0)
            //{
            //    if (cmbReason.SelectedIndex == 6)
            //        objData.Reason = "7";
            //    else if (cmbReason.SelectedIndex == 7)
            //        objData.Reason = "6";
            //    else
            //        objData.Reason = Convert.ToString(cmbReason.SelectedIndex);
            //}


            //objData.Forms = cmbFormNo.Text; ;
            //objList.Add(objData);
            ////-------------------------------------------
            //pgTimer.Start();
            ////-------------------------------------------
            //if (!bgWorker.IsBusy)
            //    bgWorker.RunWorkerAsync(objList);
        }
        #endregion

        #region bgWorker_DoWork
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
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
                    //objResponse = objAccount.makeLoginToTRACES((TracesLogin)objList[1]);
                    //objResponse = TracesConnect_OneLogin.MakeLoginToTRACES_OneLogin(
                    //        txtTANNo.Text.Trim(),
                    //        txtUserID.Text.Trim(),
                    //        txtPassword.Text.Trim(),
                    //        txtCaptchaCode.Text.Trim()
                    //    );
                    objResponse = TracesConnect_OneLogin.MakeLoginToTRACES_OneLogin_New(
                            txtTANNo.Text.Trim(),
                            txtUserID.Text.Trim(),
                            txtPassword.Text.Trim(),
                            txtCaptchaCode.Text.Trim(), lblCaptchaId.Text.Trim()
                        );
                    //---------------------------------------------------
                    objRetval.Add(enmRequestType.Login);
                    objRetval.Add(objResponse);
                    //---------------------------------------------------
                    e.Result = objRetval;
                    break;

                // LIST OF STATEMENT STATUS FILES
                case enmRequestType.CommuncationDetailGrid1:
                    DataTable table;
                    //TracesResponse response = objAccount.RequestForCommunicationInboxAction_OneLogin(out table); 
                    TracesConnect_OneLogin obj = new TracesConnect_OneLogin();
                    objResponse= obj.RequestForCommunicationInboxAction_OneLogin_New(out table);
                    objRetval.Add(enmRequestType.CommuncationDetailGrid1);
                    objRetval.Add(objResponse);
                    objRetval.Add(table);
                    e.Result = objRetval;

                    break;
                case enmRequestType.AddStatementDetail:

                    NonFilling objdata = (NonFilling)objList[1];
                    objResponse = objAccount.RequestForAddStatementDetails(objdata, out table);
                    objRetval.Add(enmRequestType.AddStatementDetail);
                    objRetval.Add(objResponse);
                    objRetval.Add(table);
                    e.Result = objRetval;


                    break;

                //REQUEST FOR LOG OFF
                case enmRequestType.LogOff:
                    //objResponse = objAccount.Logoff();
                    //objRetval.Add(enmRequestType.LogOff);
                    //objRetval.Add(objResponse);
                    //e.Result = objRetval;
                    //objResponse = TracesConnect_OneLogin.Logoff_OneLogin();
                    objResponse = TracesConnect_OneLogin.Logoff();
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
                            BtnExit.Visible = false;
                            //####
                            //TracesSession.IsLoggedIn = true;
                            //TracesSession.LoginTime = DateTime.Now;
                            //TracesSession.TAN = txtTANNo.Text;// objLogin.TAN;
                            //####
                            ArrayList objList = new ArrayList();
                            //objList.Add(enmRequestType.ListofFiliing); 
                            objList.Add(enmRequestType.CommuncationDetailGrid1); 
                            //-------------------------------------------
                            pgTimer.Start();
                            //-------------------------------------------
                            if (!bgWorker.IsBusy)
                                bgWorker.RunWorkerAsync(objList);
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            pgTimer.Stop();
                            pBar.Value = 100;
                            cmnService.J_UserMessage(objResponse.Message);
                            //InitializeCaptcha();
                            InitializeCaptcha_OneLogin();
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
                    case enmRequestType.CommuncationDetailGrid1:
                        //##
                        //if (objResponse.Respons == enmResponse.Success)
                        //{
                        //    ArrayList objList = new ArrayList();
                        //    objList.Add(enmRequestType.CommuncationDetailGrid2);
                        //    //-------------------------------------------
                        //    pgTimer.Start();
                        //    //-------------------------------------------
                        //    if (!bgWorker.IsBusy)
                        //        bgWorker.RunWorkerAsync(objList);
                        //}
                        this.pgTimer.Stop();
                        //this.pgTimer.Interval = 1000;
                        pBar.Value = 100;
                        if (objResponse.Respons == enmResponse.SessionTimeout)
                        {
                            ShowHideLoginDetails(enmRequestType.LogOff);
                            return;
                        }
                        if (objResponse.Respons == enmResponse.Failed)
                        {
                            cmnService.J_UserMessage(objResponse.Message);
                            return;
                        }

                        DataTable dTable = (DataTable)objMessage[2];
                        PopulateDatagridView(dTable);
                        SaveTracesInboxHeader(dTable, txtTANNo.Text);
                        //##
                        break;
                    //case enmRequestType.ListofFiliing:
                    //    this.pgTimer.Stop();
                    //    //this.pgTimer.Interval = 1000;
                    //    pBar.Value = 100;

                    //    if (objResponse.Respons == enmResponse.SessionTimeout)
                    //    {
                    //        ShowHideLoginDetails(enmRequestType.LogOff);
                    //        return;
                    //    }
                    //    if (objResponse.Respons == enmResponse.Failed)
                    //    {
                    //        cmnService.J_UserMessage(objResponse.Message);
                    //        return;
                    //    }

                    //    DataTable dTable = (DataTable)objMessage[2];
                    //    PopulateDatagridView(dTable);

                    //    break;

                    //case enmRequestType.AddStatementDetail:

                    //    this.pgTimer.Stop();
                    //    //this.pgTimer.Interval = 1000;
                    //    pBar.Value = 100;

                    //    if (objResponse.Respons == enmResponse.SessionTimeout)
                    //    {
                    //        ShowHideLoginDetails(enmRequestType.LogOff);
                    //        return;
                    //    }
                    //    if (objResponse.Respons == enmResponse.Failed)
                    //    {
                    //        cmnService.J_UserMessage(objResponse.Message);
                    //        return;
                    //    }
                    //    //----------------------------------
                    //   // dTable = (DataTable)objMessage[2];
                    //    cmnService.J_UserMessage(objResponse.Message);

                    //    //  PopulateNonFilingGrid(dTable);
                    //    //----------------------------------
                    //    break;
                    case enmRequestType.LogOff:
                        this.pgTimer.Stop();
                        pBar.Value = 100;

                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtTANNo.Text = "";
                        txtCaptchaCode.Text = "";
                        grpDownloadList.Visible = false;
                        grpListStatement.Visible = false;
                        grpLoginDetails.Visible = true;
                        // grpProgress.Visible = true;
                        BtnSave.Enabled = true;
                        BtnSave.BackColor = Color.Lavender;
                        //InitializeCaptcha();
                        InitializeCaptcha_OneLogin();
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

        #region pgTimer_Tick
        private void pgTimer_Tick(object sender, EventArgs e)
        {
            // Slow down
            //this.pgTimer.Interval = (this.pgTimer.Interval * 2);

            // SLOW DOWN THE INTERVAL
            this.pgTimer.Interval = 1000;
            this.pBar.Step = 5;

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
                    //lstDeducteeHelp.Height = 15;
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
            //
            lstDeducteeHelp.Visible = false;
            //--
            txtCaptchaCode.Select();
        }
        #endregion


        #region lnkLogOff_Click
        private void lnkLogOff_Click(object sender, EventArgs e)
        {
            //ShowHideLoginDetails(enmRequestType.LogOff);
            //ClearControls();
            try
            {
                // 1. Call TRACES official logout (if implemented)
                try
                {
                    objAccount.Logoff();
                }
                catch { }

                // 2. FULL RESET of OneLogin session + cookies
                TracesConnect_OneLogin.ResetOneLoginState();
                TracesSessionManager_OneLogin.ClearSession();

                // 3. Reset UI
                ShowHideLoginDetails(enmRequestType.LogOff);
                ClearControls();
                BtnExit.Visible = true;
                // 4. Enable fresh login
                txtTANNo.Focus();
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage("Error during Logoff:\n" + ex.Message);
            }
        }
        #endregion

        #region pctVideoDemo_Click
        private void pctVideoDemo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=RSVX8wDawkE");
        }

        #endregion


        #region cmbReason_SelectedIndexChanged

        private void cmbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtspecReasn.Visible = false;
            //if (cmbReason.SelectedIndex > 0)
            //{
            //    if (cmbReason.SelectedIndex == 7)
            //        txtspecReasn.Visible = true;
            //    else
            //        txtspecReasn.Visible = false;
            //}
        }

        #endregion

        #region dgvStatementList_CellValueChanged

        private void dgvStatementList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion




        #region dgvStatementList_CellClick
        private void dgvStatementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool bnlStatus = false;
            foreach (DataGridViewRow Row in dgvStatementList.Rows)
            {
                if (Row.Cells["chkDetails"].Value != null)
                {

                    if ((bool)(Row.Cells["chkDetails"].Value) == true)
                    {
                        this.dgvStatementList.Rows[Row.Index].Selected = true;

                        btnChangeFilling.Enabled = true;
                        bnlStatus = true;
                    }
                    else
                    {
                        this.dgvStatementList.Rows[Row.Index].Selected = false;

                        if (!bnlStatus)
                            btnChangeFilling.Enabled = false;

                    }
                }
            }
        }

        #endregion




        #region btnAddStatement_Click

        private void btnAddStatement_Click(object sender, EventArgs e)
        {
            ShowHideLoginDetails(enmRequestType.AddStatementDetail);
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
                objAccount = new TracesConnect();
                Stream imgStream = objAccount.MakeInitialRequest();
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
            if (string.IsNullOrEmpty(txtCaptchaCode.Text))
            {
                cmnService.J_UserMessage("Please enter Captcha Code");
                txtCaptchaCode.Focus();
                return false;
            }

            return true;
        }

        #endregion



        #region ShowHideLoginDetails
        void ShowHideLoginDetails(enmRequestType enmStatus)
        {
            ArrayList objList = new ArrayList();

            switch (enmStatus)
            {
                case enmRequestType.Login:
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtCaptchaCode.Text = "";
                    txtTANNo.Text = "";
                    grpDownloadList.Visible = false;
                    grpLoginDetails.Visible = true;
                    // grpProgress.Visible = true;
                    //InitializeCaptcha();
                    InitializeCaptcha_OneLogin();
                    break;

                case enmRequestType.ListofFiliing:
                    grpListStatement.Visible = true;
                    grpDownloadList.Visible = true;
                    grpLoginDetails.Visible = false;
                    //grpInputDetails.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;

                    //ArrayList objList = new ArrayList();
                    //objList.Add(enmRequestType.ListofFiliing);
                    ////-------------------------------------------
                    //pgTimer.Start();
                    ////-------------------------------------------
                    //if (!bgWorker.IsBusy)
                    //    bgWorker.RunWorkerAsync(objList);


                    break;

                case enmRequestType.AddStatementDetail:

                    grpDownloadList.Visible = true;
                    //grpInputDetails.Visible = true;
                    grpListStatement.Visible = false;
                    grpLoginDetails.Visible = false;

                    // grpProgress.Visible = false;
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    break;

                case enmRequestType.LogOff:
                    objList = new ArrayList();
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

        #region ClearControls
        public void ClearControls()
        {
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtCaptchaCode.Text = "";
            txtTANNo.Text = "";
            grpDownloadList.Visible = false;
            grpLoginDetails.Visible = true;
            grpProgress.Visible = true;
            //For Showing the helg grid when tan text is changed
            blnShowHelp = true;
            //
            txtTANNo.Select();

            // ----------------------------
            // -- POPULATE FORM COMBO BOXES
            // ----------------------------

            //-----------
            //-- FINANCIAL YEAR
            //-----------
            //strSQL = " SELECT ASST_ID," +
            //    "             FA_YEAR " +
            //    "      FROM   MST_ASSESSMENT " +
            //    "      WHERE  VISIBILITY_FLAG = 0 " +
            //    "      AND    ASST_ID > 2 " +
            //    "      ORDER BY ASST_ID DESC";
            //if (dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxSelectedIndex.YES) == false) return;
            ////-----------

            ////-- QUARTER
            //string[] strQtr = { T_Qtr.Q1, T_Qtr.Q2, T_Qtr.Q3, T_Qtr.Q4 };
            //dmlService.J_PopulateComboBox(strQtr, ref cmbQtr);
            ////-- FORM NO.
            //string[] strFormNo = { T_FormNo.F24Q, T_FormNo.F26Q, T_FormNo.F27Q, T_FormNo.F27EQ };
            //dmlService.J_PopulateComboBox(strFormNo, ref cmbFormNo);

            //-- REASON.

            //string[] strReason = { "Not Liable to deduct for the selected statement period",
            //                        "No Payment made / Credited to Deductee",
            //                        "Temporarily Business Closed",
            //                        "Permanently Business Closed",
            //                        "Payment Below Threshold to Deductee",
            //                        "Branch Shifted",
            //                        "Any Other Reason" };

            //dmlService.J_PopulateComboBox(strReason, ref cmbReason);



        }
        #endregion

        #region PopulateDatagridView
        void PopulateDatagridView(DataTable dt)
        {
            // -----------------------------
            // CHECK IF RECORDS EXIST
            // -----------------------------
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowHideLoginDetails(enmRequestType.AddStatementDetail);
                dgvStatementList.DataSource = null;
                return;
            }
            else
            {
                ShowHideLoginDetails(enmRequestType.ListofFiliing);
            }

            // -----------------------------
            // BIND DATA
            // -----------------------------
            dgvStatementList.Columns.Clear();
            dgvStatementList.DataSource = dt;

            _dtOriginal = dt.Copy();

            // Apply TRACES-style formatting
            FormatMainGrid();

            // Select first row
            if (dt.Rows.Count > 0)
            {
                dgvStatementList.ClearSelection();
                dgvStatementList.Rows[0].Selected = true;
            }
        }


        void HideInternalColumns()
        {
            string[] hiddenCols = { "commId", "hidfinYr", "hidquat", "declId", "certNum", "commInbId", "comcatid" };

            foreach (string colName in hiddenCols)
            {
                if (dgvStatementList.Columns.Contains(colName))
                    dgvStatementList.Columns[colName].Visible = false;
            }
        }


        void AdjustColumnWidths()
        {
            foreach (DataGridViewColumn col in dgvStatementList.Columns)
            {
                if (!col.Visible) continue;

                string h = col.HeaderText.ToLower();

                if (h == "description")
                    col.FillWeight = 250;
                else if (h == "reference no")
                    col.FillWeight = 90;
                else if (h == "date")
                    col.FillWeight = 70;
                else
                    col.FillWeight = 110;

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = true;
            }
        }

        private void FormatMainGrid()
        {
            if (dgvStatementList.DataSource == null) return;

            // Base settings
            dgvStatementList.ReadOnly = true;
            dgvStatementList.RowHeadersVisible = false;
            dgvStatementList.AllowUserToResizeColumns = false;
            dgvStatementList.AllowUserToResizeRows = false;
            dgvStatementList.MultiSelect = false;
            dgvStatementList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvStatementList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStatementList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvStatementList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // -------------------------------
            // HEADER STYLE (TRACES LOOK)
            // -------------------------------
            DataGridViewCellStyle header = new DataGridViewCellStyle();
            header.BackColor = Color.FromArgb(224, 236, 255);     // Light blue header (TRACES)
            header.ForeColor = Color.Black;
            header.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            header.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvStatementList.EnableHeadersVisualStyles = false;
            dgvStatementList.ColumnHeadersDefaultCellStyle = header;

            // -------------------------------
            // ALTERNATE ROW SHADING
            // -------------------------------
            dgvStatementList.RowsDefaultCellStyle.BackColor = Color.White;
            dgvStatementList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvStatementList.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            // -------------------------------
            // HIDE unwanted internal columns
            // -------------------------------
            HideInternalColumns();

            // -------------------------------
            // COLUMN WIDTH RULES
            // -------------------------------
            foreach (DataGridViewColumn col in dgvStatementList.Columns)
            {
                if (!col.Visible) continue;

                string h = col.HeaderText.ToLower();

                if (h.Contains("reference no"))
                    col.FillWeight = 90;
                else if (h.Contains("category"))
                    col.FillWeight = 90;
                else if (h == "description")
                    col.FillWeight = 240;        // TRACES wide description
                else if (h == "financial year")
                    col.FillWeight = 70;
                else if (h == "quarter")
                    col.FillWeight = 55;
                else if (h == "form type")
                    col.FillWeight = 60;
                else if (h == "date")
                    col.FillWeight = 70;
                else
                    col.FillWeight = 100;

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //
            dgvSubGrid.ClearSelection();
            dgvSubGrid.CurrentCell = null;
        }

        #endregion



        private void btnBack_Click(object sender, EventArgs e)
        {

            //grpInputDetails.Visible = false;
            grpListStatement.Visible = true;
        }

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0092", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void bgWorkerLoadCaptcha_DoWork(object sender, DoWorkEventArgs e)
        {
            //InitializeCaptcha();
            InitializeCaptcha_OneLogin();
        }

        #region BtnViewDetails_Click
        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvStatementList.SelectedRows.Count == 0) return;

            string commId = dgvStatementList.SelectedRows[0].Cells["commId"].Value.ToString();
            string commInbId = dgvStatementList.SelectedRows[0].Cells["commInbId"].Value.ToString();//--
            string descr = dgvStatementList.SelectedRows[0].Cells["Description"].Value.ToString();

            string json;
            //TracesResponse response = objAccount.RequestCommunicationInboxDetails(commId, descr, out json);
            //TracesResponse response = objAccount.RequestCommunicationInboxDetails_OneLogin(commId, descr, out json);
            TracesConnect_OneLogin obj = new TracesConnect_OneLogin();
            TracesResponse response = obj.RequestCommunicationInboxDetails_OneLogin_New(commId, descr, out json);

            if (response.Respons == enmResponse.Success)
            {
                DataTable dt = objAccount.ParseCommunicationDetails(json);
                dgvSubGrid.DataSource = dt;   // show in new grid or popup
                FormatSubGrid();
                //
                SaveTracesInboxDetail(dt, commInbId);
            }
            else
            {
                cmnService.J_UserMessage(response.Message);
            }
        }
        #endregion

        #region  FormatSubGrid
        private void FormatSubGrid()
        {
            if (dgvSubGrid.DataSource == null) return;

            // -------------------------------
            // BASIC GRID SETTINGS  
            // -------------------------------
            dgvSubGrid.ReadOnly = true;
            dgvSubGrid.RowHeadersVisible = false;
            dgvSubGrid.AllowUserToResizeColumns = false;
            dgvSubGrid.AllowUserToResizeRows = false;
            dgvSubGrid.MultiSelect = false;
            dgvSubGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvSubGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSubGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvSubGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // -------------------------------
            // HEADER STYLE (TRACES-LIKE)  
            // -------------------------------
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(224, 236, 255);     // light blue TRACES style
            headerStyle.ForeColor = Color.Black;
            headerStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvSubGrid.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvSubGrid.EnableHeadersVisualStyles = false;

            // -------------------------------
            // ALTERNATE ROWS (TRACES-LIKE)
            // -------------------------------
            dgvSubGrid.RowsDefaultCellStyle.BackColor = Color.White;
            dgvSubGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // -------------------------------
            // CELL STYLE
            // -------------------------------
            dgvSubGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvSubGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // HIDE unwanted internal columns
            // -------------------------------
            HideInternalColumnsSubGrid();

            // -------------------------------
            // COLUMN WIDTH MANAGEMENT
            // -------------------------------
            foreach (DataGridViewColumn col in dgvSubGrid.Columns)
            {
                string h = col.HeaderText.ToLower();

                if (h.Contains("reference"))
                    col.FillWeight = 70;
                else if (h == "category")
                    col.FillWeight = 80;
                else if (h == "description")
                    col.FillWeight = 150;
                else if (h == "subject")
                    col.FillWeight = 90;
                else if (h == "message")
                    col.FillWeight = 200;
                else
                    col.FillWeight = 100;

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        void HideInternalColumnsSubGrid()
        {
            string[] hiddenCols = { "commid" };

            foreach (string colName in hiddenCols)
            {
                if (dgvSubGrid.Columns.Contains(colName))
                    dgvSubGrid.Columns[colName].Visible = false;
            }
        }
        #endregion


        #region BtnDownloadCertificate_Click
        private void BtnDownloadCertificate_Click(object sender, EventArgs e)
        {
            if (dgvStatementList.SelectedRows.Count == 0)
            {
                //cmnService.J_UserMessage("Please select a row first.");
                cmnService.J_UserMessage("Please select a row first.");
                return;
            }

            DataGridViewRow row = dgvStatementList.SelectedRows[0];

            // Get visible values
            string refNo = row.Cells["Reference No"].Value.ToString();
            string finYear = row.Cells["Financial Year"].Value.ToString();
            string formType = row.Cells["Form Type"].Value.ToString();
            string quarter = row.Cells["Quarter"].Value?.ToString() ?? "NA";
            string category = row.Cells["Category"].Value.ToString();
            string description = row.Cells["Description"].Value.ToString();

            // Only allow certificate downloads
            if (!category.Contains("Certificate"))
            {
                //cmnService.J_UserMessage("This row does not contain a certificate.");
                cmnService.J_UserMessage("This row does not contain a certificate.");
                return;
            }

            // Try to get hidden commId field first
            string commId = "";
            string certNum = "";

            // Check if we have hidden columns
            try
            {
                if (dgvStatementList.Columns.Contains("commId"))
                    commId = row.Cells["commId"].Value?.ToString();
                else if (dgvStatementList.Columns.Contains("commInbId"))
                    commId = row.Cells["commInbId"].Value?.ToString();

                if (dgvStatementList.Columns.Contains("certNum"))
                    certNum = row.Cells["certNum"].Value?.ToString();
                else if (dgvStatementList.Columns.Contains("declId"))
                    certNum = row.Cells["declId"].Value?.ToString();
            }
            catch { }

            // If no hidden commId, derive it from Reference Number
            // Based on the URL pattern: 730301 -> 73030
            if (string.IsNullOrEmpty(commId))
            {
                if (refNo.Length >= 5)
                {
                    // Try removing last digit
                    commId = refNo.Substring(0, refNo.Length - 1);
                }
                else
                {
                    commId = refNo;
                }
            }

            // If no certNum found, try using part of reference number
            if (string.IsNullOrEmpty(certNum))
            {
                certNum = refNo; // or some other derivation
            }
            //
            if (cmnService.J_UserMessage("Proceed with download?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            byte[] pdfBytes;
            //TracesResponse resp = objAccount.DownloadCertificate(
            //    refNo,
            //    finYear,
            //    formType,
            //    quarter,
            //    certNum,
            //    description,
            //    out pdfBytes
            //);
            //
            TracesConnect_OneLogin obj = new TracesConnect_OneLogin();
            TracesResponse resp = obj.DownloadCertificate_OneLogin_New(
                refNo,
                finYear,
                formType,
                quarter,
                certNum,
                description,
                out pdfBytes
            );

            if (resp.Respons != enmResponse.Success)
            {
                //cmnService.J_UserMessage("Download failed:\n\n" + resp.Message, "Error",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmnService.J_UserMessage("Download failed:\n\n" + resp.Message, MessageBoxIcon.Error);
                return;
            }
            // ----------------------------------------------
            // ASK USER WHERE TO SAVE THE FILE
            // ----------------------------------------------
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Title = "Save Certificate PDF";
            //dlg.Filter = "PDF Files (*.pdf)|*.pdf";
            //dlg.FileName = "Certificate_" + refNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            //if (dlg.ShowDialog() != DialogResult.OK)
            //    return;

            //string userPath = dlg.FileName;

            string userPath = cmnService.J_OpenFolderDialog("Select Destination Folder");
            string FileName = "Certificate_" + refNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            userPath = Path.Combine(userPath, FileName);

            //string savePath = @"C:\Certificate_" + refNo + "_" +
            //                  DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            File.WriteAllBytes(userPath, pdfBytes);
            //####
            string subPath = Path.Combine(Application.StartupPath, "Certificate"); // Your code goes here
            if (!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }
            File.Copy(userPath, Path.Combine(subPath, FileName));
            //-- UPDATE THE PATH...
            strSQL = @"SELECT TRACES_INBOX_HEADER_ID FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commId + "'";
            string strInboxHeaderId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            //
            strSQL = @"UPDATE TRN_TRACES_INBOX_HEADER_ACTION SET CERT_PATH ='" + Path.Combine(subPath, FileName) + "' WHERE TRACES_INBOX_HEADER_ID = " + strInboxHeaderId;
            dmlService.J_BeginTransaction();
            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
            {
                dmlService.J_Rollback();
                return;
            }
            dmlService.J_Commit();
            //
            cmnService.J_UserMessage("Certificate downloaded successfully!\nSaved to: " + userPath);
            //
            System.Diagnostics.Process.Start(userPath);
        }
        #endregion


        #region BtnRequestforDownloadIntimation_Click
        private void BtnRequestforDownloadIntimation_Click(object sender, EventArgs e)
        {
            if (dgvStatementList.SelectedRows.Count == 0)
            {
                cmnService.J_UserMessage("Please select a row first.");
                return;
            }

            DataGridViewRow row = dgvStatementList.SelectedRows[0];

            string commId = row.Cells["commId"].Value.ToString();
            string commInbId = row.Cells["commInbId"].Value.ToString();
            string comcatid = row.Cells["comcatid"].Value.ToString();
            string finYear = row.Cells["hidfinYr"].Value.ToString();
            string quarter = row.Cells["hidquat"].Value.ToString(); 
            string formType = row.Cells["Form Type"].Value.ToString(); 
            string commRefNo = row.Cells["reference no"].Value.ToString();

            //var resp = objAccount.RequestForDownloadIntimation(
            //                commInbId,
            //                finYear,
            //                quarter,
            //                formType,
            //                commRefNo,
            //                comcatid);
            TracesConnect_OneLogin obj = new TracesConnect_OneLogin();
            var resp = obj.RequestForDownloadIntimation_OneLogin_New(
                            commInbId,
                            finYear,
                            quarter,
                            formType,
                            commRefNo,
                            comcatid);

            if (resp.Respons != enmResponse.Success)
            {
                cmnService.J_UserMessage("Request failed: " + resp.Message);
                return;
            }


            if (resp.Respons == enmResponse.Success)
                cmnService.J_UserMessage(resp.Message.ToString());
            else
                cmnService.J_UserMessage("Failed: " + resp.Message);
        }
        #endregion


        #region ParseRequestNumber
        public string ParseRequestNumber(string html)
        {
            // TRACES shows Request Number inside HTML
            Match m = Regex.Match(html, @"Request Number\s*([0-9]+)");

            if (m.Success)
                return m.Groups[1].Value.Trim();

            return "";
        }
        #endregion

        #region DgvStatementList_SelectionChanged
        private void DgvStatementList_SelectionChanged(object sender, EventArgs e)
        {
            ApplyButtonRules();
        }
        #endregion


        #region BtnRequestforJustificationReport_Click
        private void ApplyButtonRules()
        {
            if (dgvStatementList.SelectedRows.Count == 0)
            {
                DisableAllButtons();
                return;
            }

            string category = dgvStatementList.SelectedRows[0].Cells["Category"].Value?.ToString() ?? "";

            // Normalize comparison (case-insensitive)
            bool isCertificate = category.IndexOf("certificate", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isCertificate)
            {
                // CERTIFICATE FOUND Enable Certificate Download
                btnDownloadCertificate.Enabled = true;
                btnDownloadCertificate.BackColor = Color.Lavender;
                // Disable request buttons
                btnRequestforDownloadIntimation.Enabled = false;
                btnRequestforDownloadIntimation.BackColor = Color.LightGray;
                btnRequestforJustificationReport.Enabled = false;
                btnRequestforJustificationReport.BackColor = Color.LightGray;
            }
            else
            {
                // NOT certificate Disable certificate download
                btnDownloadCertificate.Enabled = false;
                btnDownloadCertificate.BackColor = Color.LightGray;
                // Enable request buttons
                btnRequestforDownloadIntimation.Enabled = true;
                btnRequestforDownloadIntimation.BackColor = Color.Lavender;
                btnRequestforJustificationReport.Enabled = true;
                btnRequestforJustificationReport.BackColor = Color.Lavender;
            }
        }
        #endregion



        #region BtnRequestforJustificationReport_Click
        private void DisableAllButtons()
        {
            btnDownloadCertificate.Enabled = false;
            btnDownloadCertificate.BackColor = Color.LightGray;
            btnRequestforDownloadIntimation.Enabled = false;
            btnRequestforDownloadIntimation.BackColor = Color.LightGray;
            btnRequestforJustificationReport.Enabled = false;
            btnRequestforJustificationReport.BackColor = Color.LightGray;
        }
        #endregion

        #region BtnRequestforJustificationReport_Click
        private void BtnRequestforJustificationReport_Click(object sender, EventArgs e)
        {
            if (cmnService.J_UserMessage("Current module will close and 'Request for Defaults/Justification Report' module will open,\nContinue ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //--
            TdsMan.CloseChildForm(new TrnRequestConsolidatedFile(""), J_Var.frmMain);
            //--
            DataGridViewRow row = dgvStatementList.SelectedRows[0];
            string FYear = row.Cells["financial year"].Value.ToString();
            string Qtr = row.Cells["quarter"].Value.ToString();
            string Form = row.Cells["form type"].Value.ToString();
            //--
            cmnService.J_ShowChildForm(new TrnRequestConsolidatedFile(T_NSDL_FORM_TYPE.Defaults, txtTANNo.Text, txtUserID.Text, txtPassword.Text, FYear, Form, Qtr), J_Var.frmMain, "Request for Defaults/Justification Report");
        }
        #endregion



        #region SaveTracesInboxHeader
        public void SaveTracesInboxHeader(DataTable dt, string TAN)
        {
            //using (var con = new OleDbConnection(connStr))   // For MS Access
            //                                                 // using (var con = new SqlConnection(connStr)); // For SQL Server
            //{
            //    con.Open();
            //
            dmlService.J_BeginTransaction();
            foreach (DataRow dr in dt.Rows)
            {
                string commInbId = dr["commInbId"].ToString();

                // CHECK DUPLICATE
                strSQL = "SELECT COUNT(*) FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commInbId + "'";

                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    continue;
                //using (var cmd = new OleDbCommand(checkSql, con))
                //{
                //    cmd.Parameters.AddWithValue("@p1", commInbId);

                //    int count = Convert.ToInt32(cmd.ExecuteScalar());

                //    if (count > 0)
                //        continue;   // Already exists  skip
                //}

                // INSERT NEW ROW
                strSQL =
                        "INSERT INTO TRN_TRACES_INBOX_HEADER_ACTION (" +
                        "    TAN_NO," +
                        "    REFERENCE_NO," +
                        "    RECORD_DATE," +
                        "    RECORD_CATEGORY," +
                        "    DESCRIPTION," +
                        "    FY_YEAR," +
                        "    QUARTER," +
                        "    FORM_TYPE," +
                        "    COMM_ID," +
                        "    HID_FIN_YR," +
                        "    HID_QUAT," +
                        "    DECL_ID," +
                        "    CERT_NUM," +
                        "    COMM_INB_ID," +
                        "    COM_CAT_ID) " +
                        "VALUES (" +
                        "    '" + cmnService.J_ReplaceQuote(TAN) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Reference No"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Date"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Category"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Description"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Financial Year"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Quarter"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Form Type"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["hidfinYr"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["hidquat"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["declId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["certNum"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commInbId"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["comcatid"].ToString()) + "')";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
            }
            dmlService.J_Commit();
            //}
        }
        #endregion

        #region SaveTracesInboxDetail
        public void SaveTracesInboxDetail(DataTable dt, string commInbId)
        {
            //using (var con = new OleDbConnection(connStr))   // For MS Access
            //                                                 // using (var con = new SqlConnection(connStr)); // For SQL Server
            //{
            //    con.Open();
            strSQL = @"SELECT TRACES_INBOX_HEADER_ID FROM TRN_TRACES_INBOX_HEADER_ACTION WHERE TAN_NO = '" + cmnService.J_ReplaceQuote(txtTANNo.Text) + "' AND COMM_INB_ID = '" + commInbId + "'";
            string strInboxHeaderId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            //
            dmlService.J_BeginTransaction();
            foreach (DataRow dr in dt.Rows)
            {
                //string commInbId = dr["commInbId"].ToString();

                // CHECK DUPLICATE
                strSQL = "SELECT COUNT(*) FROM TRN_TRACES_INBOX_DETAIL_ACTION WHERE TRACES_INBOX_HEADER_ID = " + strInboxHeaderId + "";

                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL))) > 0)
                    continue;

                // INSERT NEW ROW
                strSQL =
                        "INSERT INTO TRN_TRACES_INBOX_DETAIL_ACTION (" +
                        "    TRACES_INBOX_HEADER_ID," +
                        "    REFERENCE_NO," +
                        "    RECORD_CATEGORY," +
                        "    DESCRIPTION," +
                        "    SUBJECT," +
                        "    MESSAGE," +
                        "    COMM_ID) " +
                        "VALUES (" +
                        "     " + cmnService.J_ReturnInt32Value(strInboxHeaderId) + "," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Reference No"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Category"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Description"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Subject"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["Message"].ToString()) + "'," +
                        "    '" + cmnService.J_ReplaceQuote(dr["commid"].ToString()) + "')";
                if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
            }
            dmlService.J_Commit();
            //}
        }
        #endregion

        #region  ApplySearchFilter
        private void ApplySearchFilter(string searchText)
        {
            try
            {
                if (_dtOriginal == null || _dtOriginal.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvStatementList.DataSource = _dtOriginal;
                    FormatMainGrid();
                    return;
                }

                searchText = searchText.Trim().Replace("'", "''");

                // Build filter for ALL columns
                List<string> filters = new List<string>();

                foreach (DataColumn col in _dtOriginal.Columns)
                {
                    filters.Add($"[{col.ColumnName}] LIKE '%{searchText}%'");
                }

                string finalFilter = string.Join(" OR ", filters);

                DataView dv = new DataView(_dtOriginal);
                dv.RowFilter = finalFilter;

                dgvStatementList.DataSource = dv;
                FormatMainGrid();
            }
            catch
            {
                // silently ignore filter errors
            }
        }
        #endregion

        #region TxtSearch_TextChanged
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter(txtSearch.Text);
        }
        #endregion

        private void LoadModuleAfterLogin()
        {
            // This code should run AFTER login or when session is active
            // Example: load inbox, challan, statements etc.
            ArrayList objList = new ArrayList();
            objList.Add(enmRequestType.CommuncationDetailGrid1);
            //-------------------------------------------
            pgTimer.Start();
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(objList);
        }

        private void LoadCaptcha_OneLogin()
        {
            //oneLogin = new TracesConnect_OneLogin();
            //Stream img = oneLogin.MakeInitialRequest_OneLogin();
            //picCaptcha.Image = Image.FromStream(img);
        }
        private void InitializeCaptcha_OneLogin()
        {
            try
            {
                if (TdsMan.T_CheckInternetConnectivty() == false)
                {
                    picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                    cmnService.J_UserMessage("Internet Connectivity not found");
                    return;
                }

                Stream imgStream = TracesConnect_OneLogin.MakeInitialRequest_OneLogin();
                //Stream imgStream = TracesConnect_OneLogin.MakeInitialRequest_OneLogin_New();
                //Image img = Image.FromStream(imgStream);
                //picCaptcha.Image = img;

                var captchaData = TracesConnect_OneLogin.GetCaptcha_New();

                Image img = Image.FromStream(captchaData.imageStream);
                picCaptcha.Image = img;
                this.CurrentCaptchaId = captchaData.captchaId;

                txtCaptchaCode.Text = "";
            }
            catch (Exception err)
            {
                picCaptcha.Image = Properties.Resources.captcha_loading_failed;
                cmnService.J_UserMessage(err.Message);
            }
        }

        private void LnkLogOff_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }

}

